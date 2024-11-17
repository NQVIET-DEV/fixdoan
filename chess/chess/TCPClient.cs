using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess
{
    public class TCPClient
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        public event Action<bool> MatchFound;
        public event Action<string, string> OpponentMoved;
        public TCPClient(string serverIP, int serverPort)
        {
            tcpClient = new TcpClient();
            ConnectToServer(serverIP, serverPort).Wait();
        }

        private async Task ConnectToServer(string serverIP, int serverPort)
        {
            try
            {
                await tcpClient.ConnectAsync(IPAddress.Parse(serverIP), serverPort);
                stream = tcpClient.GetStream();
                // _ = ListenForOpponentMovesAsync();
                Console.WriteLine("Connected to the server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection error: {ex.Message}");
            }
        }

        public async Task<string> RegisterAsync(string username, string password)
        {
            string request = $"REGISTER {username} {password}";
            return await SendRequestAsync(request);
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            string request = $"LOGIN {username} {password}";
            return await SendRequestAsync(request);
        }

        public async Task<string> FindMatchAsync()
        {
            string response = await SendRequestAsync("FIND_MATCH");

            if (response.StartsWith("WAITING"))
            {
                Console.WriteLine("Waiting for a match...");
                await ListenForMatchFoundAsync(); // Lắng nghe thông báo tìm thấy trận đấu
            }

            return response;
        }

        public async Task<string> SendMoveAsync(string from, string to)
        {
            string request = $"MOVE {from} {to}";
            return await SendRequestAsync(request);
        }

        private async Task<string> SendRequestAsync(string request)
        {
            try
            {
                if (!tcpClient.Connected)
                {
                    throw new InvalidOperationException("Client is not connected to the server.");
                }

                byte[] data = Encoding.UTF8.GetBytes(request + "\n");
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();

                return await ReceiveResponseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
                return "ERROR";
            }
        }

        public async Task<string> ReceiveResponseAsync()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving response: {ex.Message}");
                return "ERROR";
            }
        }
        public async Task<string> WaitForMatchAsync()
        {
            try
            {
                // Liên tục đọc phản hồi từ server đến khi trận đấu có sẵn
                return await ReadResponseAsync();
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }
        private async Task<string> ReadResponseAsync()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
        }

        public async Task ListenForMatchFoundAsync()
        {
            try
            {
                while (true)
                {
                    string message = await ReceiveResponseAsync();
                    if (message.StartsWith("MATCH_FOUND"))
                    {
                        bool isWhite = message.Contains("WHITE");
                        // Mở form bàn cờ trên UI thread
                        //OpenChessBoardForm(isWhite);
                        MatchFound?.Invoke(isWhite);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving match found: {ex.Message}");
            }
        }
        public async Task ListenForOpponentMovesAsync()
        {
            try
            {
                while (true)
                {
                    string message = await ReceiveResponseAsync();
                    if (message.StartsWith("MOVE"))
                    {
                        var parts = message.Split(' ');
                        if (parts.Length == 3)
                        {
                            string from = parts[1];
                            string to = parts[2];
                            OnOpponentMoved(from, to); // Cập nhật bàn cờ cho nước đi của đối thủ
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving opponent move: {ex.Message}");
            }
        }
        public async Task ListenForMoves()
        {
            while (true)
            {
                byte[] buffer = new byte[256];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // Giả sử định dạng nước đi từ server là "MOVE from to"
                if (message.StartsWith("MOVE"))
                {
                    var parts = message.Split(' ');
                    if (parts.Length == 3)
                    {
                        string from = parts[1];
                        string to = parts[2];
                        OpponentMoved?.Invoke(from, to);
                    }
                }
            }
        }
        public async Task SendMove(string from, string to)
        {
            // Chuyển nước đi thành dạng tin nhắn và gửi tới server
            // Ví dụ: "MOVE <từ> <đến>"
            string message = $"MOVE {from} {to}";
            await SendMessageToServer(message); // Gọi phương thức gửi tin nhắn tới server
        }
        private async Task SendMessageToServer(string message)
        {
            try
            {
                if (!tcpClient.Connected)
                {
                    throw new InvalidOperationException("Client is not connected to the server.");
                }

                byte[] data = Encoding.UTF8.GetBytes(message + "\n"); // Thêm ký tự xuống dòng để đánh dấu kết thúc tin nhắn
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync(); // Đảm bảo dữ liệu được gửi ngay lập tức
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message to server: {ex.Message}");
            }
        }
        public async Task<string> ReceiveMoveAsync()
        {
            while (true)
            {
                string message = await ReceiveResponseAsync();
                if (message.StartsWith("MOVE"))
                {
                    var parts = message.Split(' ');
                    if (parts.Length == 3)
                    {
                        string from = parts[1];
                        string to = parts[2];
                        return $"MOVE {from} {to}"; // Trả về nước đi đã nhận
                    }
                }
            }
        }
        public async Task<string> ReceiveChatAsync()
        {
            while (true)
            {
                string message = await ReceiveResponseAsync();
                if (message.StartsWith("CHAT"))
                {
                   
                    return message;
                }    
            }
        }
        protected virtual void OnOpponentMoved(string from, string to)
        {
            OpponentMoved?.Invoke(from, to);
        }
        public async Task SendMessageAsync(string message)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);
        }
        public void Close()
        {
            stream?.Close();
            tcpClient?.Close();
            Console.WriteLine("Disconnected from server.");
        }
    }
}