using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace chess
{
    internal class TCPServer
    {
        public enum PieceColor
        {
            White,
            Black
        }

        private TcpListener tcpListener;
        private Queue<TcpClient> waitingPlayers = new Queue<TcpClient>();
        private const string connectionString = "Data Source=players.db;Version=3;";
        private List<GameRoom> gameRooms = new List<GameRoom>();

        public TCPServer(int localPort)
        {
            tcpListener = new TcpListener(IPAddress.Any, localPort);
            CreateDatabase();
        }

        public async Task StartAsync()
        {
            tcpListener.Start();
            Console.WriteLine("Server started, waiting for connections...");

            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");
                _ = HandleClientAsync(client); // Xử lý mỗi client trong một task riêng biệt
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (client.Connected)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    Console.WriteLine($"Received: {request}");
                    string response = ProcessRequest(request, client);

                    byte[] responseData = Encoding.UTF8.GetBytes(response + "\n");
                    await stream.WriteAsync(responseData, 0, responseData.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (client.Connected)
                {
                    client.Close();
                    RemovePlayer(client);
                }
                Console.WriteLine("Client disconnected.");
            }
        }

        private string ProcessRequest(string request, TcpClient client)
        {
            string[] parts = request.Split(' ');
            string command = parts[0].ToUpper(); // Chuyển sang chữ hoa để so sánh không phân biệt chữ hoa chữ thường

            switch (command)
            {
                case "REGISTER":
                    return parts.Length >= 3 ? RegisterPlayer(parts[1], parts[2]) : "ERROR: Invalid request format";
                case "LOGIN":
                    return parts.Length >= 3 ? LoginPlayer(parts[1], parts[2]) : "ERROR: Invalid request format";
                case "FIND_MATCH":
                    return FindMatch(client);
                case "MOVE":
                    return parts.Length >= 3 ? HandleMove(parts[1], parts[2], client) : "ERROR: Invalid move format";
                case "CHAT":
                    return parts.Length >= 2 ? HandleChat(string.Join(" ", parts.Skip(1)), client) : "ERROR: Empty message";
                default:
                    return "ERROR: Unknown command";
            }
        }

        private string RegisterPlayer(string username, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO players (username, password) VALUES (@username, @password)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    try
                    {
                        command.ExecuteNonQuery();
                        return "SUCCESS: Registered";
                    }
                    catch (SQLiteException ex)
                    {
                        // Lỗi khóa duy nhất (unique constraint)
                        if (ex.ResultCode == SQLiteErrorCode.Constraint)
                        {
                            return "ERROR: Username already exists";
                        }
                        return "ERROR: Database error: " + ex.Message;
                    }
                }
            }
        }


        private string LoginPlayer(string username, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM players WHERE username = @username AND password = @password";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    long count = (long)command.ExecuteScalar();
                    return count > 0 ? "SUCCESS: Logged in" : "ERROR: Invalid username or password";
                }
            }
        }

        private string FindMatch(TcpClient client)
        {
            lock (waitingPlayers)
            {
                // Kiểm tra xem client đã có trong danh sách người chơi hay chưa
                if (waitingPlayers.Contains(client))
                {
                    return "ERROR: Already in a match";
                }

                waitingPlayers.Enqueue(client);
                if (waitingPlayers.Count >= 2)
                {
                    TcpClient player1 = waitingPlayers.Dequeue();
                    TcpClient player2 = waitingPlayers.Dequeue();
                    GameRoom gameRoom = new GameRoom(player1, player2);
                    gameRooms.Add(gameRoom);

                    SendMessage(player1, "MATCH_FOUND WHITE");
                    SendMessage(player2, "MATCH_FOUND BLACK");
                    SendMessage(player1, "MATCH_FOUND WHITE");
                    //  SendMessageBoardState(player1, gameRoom);
                    // SendMessageBoardState(player2, gameRoom);

                    return "SUCCESS: Match started";
                }
                else
                {
                    return "WAITING: Finding match...";
                }
            }
        }


        private string HandleMove(string from, string to, TcpClient client)
        {
            var room = gameRooms.FirstOrDefault(g => g.HasPlayer(client));
            if (room == null)
            {
                return "ERROR: Not in a game room.";
            }

            if (!room.IsCurrentPlayer(client))
                return "ERROR: Not your turn";

            room.SendMove(from, to, client);
            return "SUCCESS: Move sent";
        }
        private string HandleChat(string message, TcpClient client)
        {
            var room = gameRooms.FirstOrDefault(g => g.HasPlayer(client));
            if (room == null)
            {
                return "ERROR: Not in a game room.";
            }

            room.SendChatMessage(message, client);
            return "SUCCESS: Message sent";
        }

        private void RemovePlayer(TcpClient client)
        {
            lock (waitingPlayers)
            {
                if (waitingPlayers.Contains(client))
                {
                    waitingPlayers = new Queue<TcpClient>(waitingPlayers.Where(p => p != client));
                }

                var room = gameRooms.FirstOrDefault(g => g.HasPlayer(client));
                if (room != null)
                {
                    room.RemovePlayer(client);
                    gameRooms.Remove(room);
                }
            }
        }

        private async void SendMessage(TcpClient client, string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        private void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                     CREATE TABLE IF NOT EXISTS players (
                         id INTEGER PRIMARY KEY AUTOINCREMENT,
                         username TEXT NOT NULL UNIQUE,
                         password TEXT NOT NULL
                     );";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                AddAdminUserIfNotExists(connection);
            }
        }


        private void AddAdminUserIfNotExists(SQLiteConnection connection)
        {
            string checkQuery = "SELECT COUNT(*) FROM players WHERE username = 'admin'";
            using (var command = new SQLiteCommand(checkQuery, connection))
            {
                long count = (long)command.ExecuteScalar();
                if (count == 0)
                {
                    string insertQuery = "INSERT INTO players (username, password) VALUES ('admin', '123')";
                    using (var insertCommand = new SQLiteCommand(insertQuery, connection))
                    {
                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine("Admin user added.");
                    }
                }
            }
        }


    }


    internal class GameRoom
    {
        private TcpClient player1;
        private TcpClient player2;
        private int currentPlayerIndex = 0;

        public GameRoom(TcpClient player1, TcpClient player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        public bool HasPlayer(TcpClient client)
        {
            return player1 == client || player2 == client;
        }

        public bool IsCurrentPlayer(TcpClient client)
        {
            return (currentPlayerIndex == 0 && player1 == client) || (currentPlayerIndex == 1 && player2 == client);
        }

        public void SendMove(string from, string to, TcpClient client)
        {
            TcpClient opponent = (client == player1) ? player2 : player1;
            // Gửi nước đi cho cả hai người chơi
            SendMessage(opponent, $"MOVE {from} {to}");
            // SendMessage(client, $"MOVE {from} {to}");
            Console.WriteLine($"Player {(currentPlayerIndex + 1)} moved from {from} to {to}");
            UpdateCurrentPlayer();
        }

        private void UpdateCurrentPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % 2; // Chuyển lượt giữa hai người chơi
        }

        private async void SendMessage(TcpClient client, string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
        public void SendChatMessage(string message, TcpClient client)
        {
            TcpClient opponent = (client == player1) ? player2 : player1;
            SendMessage(opponent, $"CHAT {message}");
            SendMessage(client, $"CHAT {message}");
            Console.WriteLine($"Chat message from player {(currentPlayerIndex + 1)}: {message}");
        }

        public void RemovePlayer(TcpClient client)
        {
            if (player1 == client)
                player1 = null;
            else if (player2 == client)
                player2 = null;

            // Kiểm tra xem có còn người chơi nào trong phòng không
            if (player1 == null && player2 == null)
            {
                // Xóa phòng khi không còn người chơi
                Console.WriteLine("Game room is empty and will be removed.");
            }
        }
    }
}