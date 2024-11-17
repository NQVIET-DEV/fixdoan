using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
namespace chess
{
    public partial class ChessBoardForm : Form
    {
        private TCPClient client;
        private string playerColor; // Màu của người chơi ("WHITE" hoặc "BLACK")
        private PieceColor currentPlayer;
        private PictureBox[,] pictureBoxes;
        private (int, int)? selectedCell = null;
        private bool isPlayerTurn;
        private ChessBoard chessBoard;
        private int turn = 0;
        private int timeLeft; // Thời gian còn lại cho người chơi hiện tại (tính bằng giây)
        private int opponentTimeLeft; // Thời gian còn lại cho đối thủ
        private Timer timer;
      //  private CancellationTokenSource _cancellationTokenSource;
        public ChessBoardForm(TCPClient client, string playerColor)
        {
            InitializeComponent();
            this.client = client;
            this.playerColor = playerColor.ToLower();
            this.currentPlayer = this.playerColor == "white" ? PieceColor.White : PieceColor.Black;
            this.isPlayerTurn = playerColor == "WHITE";
            pictureBoxes = new PictureBox[8, 8];
            chessBoard = new ChessBoard();
            InitializeBoard();
            timeLeft = 600;
            opponentTimeLeft = 600;

            timer = new Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;
            timer.Start();
            UpdateTimeLabels();
           // MessageBox.Show(playerColor);
            Task.Run(() => ListenForInformation());
          //  Task.Run(() => ListenForOpponentChat());
        }

        private void InitializeBoard()
        {
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.ColumnCount = 8;
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < 8; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));

                for (int j = 0; j < 8; j++)
                {
                    PictureBox picBox = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = (i, j)
                    };
                    picBox.BackColor = (i + j) % 2 == 0 ? Color.White : Color.Gray;
                    picBox.Click += OnCellClick;
                    pictureBoxes[i, j] = picBox;
                    tableLayoutPanel1.Controls.Add(picBox, j, i);
                    UpdatePictureBox(i, j); // Cập nhật hình ảnh quân cờ ban đầu
                }
            }
        }

        private async void ChessBoardForm_Load(object sender, EventArgs e)
        {
            
            // Bắt đầu lắng nghe nước đi của đối thủ
             //  await ListenForOpponentMove();
            //    await ListenForOpponentChat();

            //while (true)
            // { await ListenForOpponentMove(); }
        }
        private void UpdateTimeLabels()
        {
            int minutes = timeLeft / 60;
            int seconds = timeLeft % 60;
            timelabel.Text = $"Thời gian: {minutes:D2}:{seconds:D2}";

            minutes = opponentTimeLeft / 60;
            seconds = opponentTimeLeft % 60;
            opponentTimeLabel.Text = $"Thời gian đối thủ: {minutes:D2}:{seconds:D2}";
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Giảm thời gian cho người chơi hiện tại
            timeLeft--;
            UpdateTimeLabels(); // Cập nhật label mỗi giây

            if (timeLeft <= 0)
            {
                timer.Stop();
                MessageBox.Show("Hết thời gian! Đến lượt của đối thủ.");
                SwitchTurn(); // Chuyển lượt cho đối thủ
            }
        }
        private async Task ListenForOpponentChat()
        {
            isPlayerTurn = true;
            while (true)
            {
                string respone = await client.ReceiveResponseAsync();
                if (respone.StartsWith("CHAT"))
                {
                    rtb_historychat.Invoke((MethodInvoker)delegate
                    {
                        rtb_historychat.AppendText("Đối thủ: " + respone.Substring(5) + "\n");
                    });
                }    
            }
        }
        /* private async Task ListenForOpponentMove()
         {
            // isPlayerTurn = true;
             while (true)
             {
                 string response = await client.ReceiveResponseAsync();
              //   MessageBox.Show(response);
                 if (response.StartsWith("MOVE"))
                 {
                  //   MessageBox.Show("Đối thủ đã đánh nước đi của mình!");
                     string[] parts = response.Split(' ');
                     if (parts.Length == 3)
                     {
                         string from = parts[1];
                         string to = parts[2];
                         ApplyMove(from, to); // Cập nhật bàn cờ với nước đi của đối thủ
                         timeLeft = 600; // Đặt lại thời gian cho người chơi hiện tại
                         opponentTimeLeft = 600; // Đặt lại thời gian cho đối thủ
                         UpdateTimeLabels();
                         if (IsKingCaptured(playerColor))
                         {
                             MessageBox.Show("Quân vua của bạn đã bị bắt! Trò chơi kết thúc.");
                             this.Close(); // Hoặc sử dụng cách khác để kết thúc trò chơi
                         }
                         isPlayerTurn = true; // Chuyển lượt cho người chơi
                     }
                 }
                 isPlayerTurn = true;
                 if (response.StartsWith("CHAT"))
                 {

                         rtb_historychat.AppendText("Đối thủ: " + response.Substring(5) + "\n");

                 }
             }
         }*/
        private async Task ListenForOpponentMove()
        {
            while (true)
            {
                string response = await client.ReceiveResponseAsync();
                if (response.StartsWith("MOVE"))
                {
                    string[] parts = response.Split(' ');
                    if (parts.Length == 3)
                    {
                        string from = parts[1];
                        string to = parts[2];
                        ApplyMove(from, to); // Gọi ApplyMove để xử lý nước đi và kiểm tra vua bị bắt
                        timeLeft = 600; // Đặt lại thời gian cho người chơi hiện tại
                        opponentTimeLeft = 600; // Đặt lại thời gian cho đối thủ
                        UpdateTimeLabels();
                        isPlayerTurn = true; // Chuyển lượt cho người chơi
                    }
                }

                if (response.StartsWith("CHAT"))
                {
                    rtb_historychat.AppendText("Đối thủ: " + response.Substring(5) + "\n");
                }
            }
        }
        /* private async Task ListenForInformation()
         {
             while (true)
             {
                 string response = await client.ReceiveResponseAsync();
                 if (response.StartsWith("MOVE"))
                 {
                     //   MessageBox.Show("Đối thủ đã đánh nước đi của mình!");
                     string[] parts = response.Split(' ');
                     if (parts.Length == 3)
                     {
                         string from = parts[1];
                         string to = parts[2];
                         ApplyMove(from, to); // Cập nhật bàn cờ với nước đi của đối thủ
                         timeLeft = 600; // Đặt lại thời gian cho người chơi hiện tại
                         opponentTimeLeft = 600; // Đặt lại thời gian cho đối thủ
                         UpdateTimeLabels();
                         if (IsKingCaptured(playerColor))
                         {
                             MessageBox.Show("Quân vua của bạn đã bị bắt! Trò chơi kết thúc.");
                             this.Close(); // Hoặc sử dụng cách khác để kết thúc trò chơi
                         }
                         isPlayerTurn = true; // Chuyển lượt cho người chơi
                     }
                 }

                 if (response.StartsWith("CHAT"))
                 {
                     isPlayerTurn = true;
                     rtb_historychat.AppendText("Đối thủ: " + response.Substring(5) + "\n");

                 }
             }
         }*/
        private async Task ListenForInformation()
        {
            while (true)
            {
                string response = await client.ReceiveResponseAsync();
                if (response.StartsWith("MOVE"))
                {
                    string[] parts = response.Split(' ');
                    if (parts.Length == 3)
                    {
                        string from = parts[1];
                        string to = parts[2];
                        ApplyMove(from, to); // Gọi ApplyMove để xử lý nước đi và kiểm tra vua bị bắt
                        timeLeft = 600; // Đặt lại thời gian cho người chơi hiện tại
                        opponentTimeLeft = 600; // Đặt lại thời gian cho đối thủ
                        UpdateTimeLabels();
                        isPlayerTurn = true; // Chuyển lượt cho người chơi
                    }
                }

                if (response.StartsWith("CHAT"))
                {
                    isPlayerTurn = true;
                    rtb_historychat.AppendText("Đối thủ: " + response.Substring(5) + "\n");
                }
            }
        }

        private async void OnCellClick(object sender, EventArgs e)
        {
            // Kiểm tra xem có phải lượt của người chơi hay không
            if (!isPlayerTurn)
            {
                MessageBox.Show("Chưa tới lượt của bạn!");
                return;
            }

            
            PictureBox clickedPictureBox = sender as PictureBox;
            var (row, col) = ((int, int))clickedPictureBox.Tag;

            // Kiểm tra việc chọn ô
            if (selectedCell == null)
            {
                // Chọn ô cờ
                //ChessPiece piece = chessBoard.Board[row, col];
                if (chessBoard.Board[row, col] != null && chessBoard.Board[row, col].Color == currentPlayer)
                {
                    selectedCell = (row, col);
                    pictureBoxes[row, col].BackColor = Color.LightBlue;
                    HighlightValidMoves(row, col); // Đánh dấu các ô cờ hợp lệ
                }
                else
                {
                    MessageBox.Show("Bạn không thể chọn ô này! Ô trống hoặc không phải quân cờ của bạn.");
                }
            }
            else
            {
                turn++;
                
                var (startX, startY) = selectedCell.Value;
                if (chessBoard.IsValidMove(startX, startY, row, col))
                {
                    string from = $"{startX}{startY}";
                    string to = $"{row}{col}";

                    // ApplyMove(from, to);
                    // Gửi nước đi tới server
                 
                    ApplyMove(from, to);
                    if (IsKingCaptured(playerColor))
                    {
                        MessageBox.Show("Quân vua của bạn đã bị bắt! Trò chơi kết thúc.");
                        this.Close(); // Hoặc sử dụng cách khác để kết thúc trò chơi
                    }
                    

                    
                    client.SendMove(from, to);
                   
                    timeLeft = 600; // Đặt lại thời gian cho người chơi hiện tại
                    opponentTimeLeft = 600;
                    isPlayerTurn = false; // Chuyển lượt cho đối thủ
                    lbl_luot.Text = "Lượt của đối thủ";
                }
                else
                {
                    MessageBox.Show("Nước đi không hợp lệ!");
                }
               
                selectedCell = null;
                ResetBoardColors(); // Khôi phục màu sắc bàn cờ
            }
        }

        /* private void ApplyMove(string from, string to)
         {
             int startX = int.Parse(from[0].ToString());
             int startY = int.Parse(from[1].ToString());
             int endX = int.Parse(to[0].ToString());
             int endY = int.Parse(to[1].ToString());

             // Thực hiện di chuyển quân cờ
             chessBoard.Board[endX, endY] = chessBoard.Board[startX, startY];

             chessBoard.Board[startX, startY] = null;
             UpdatePictureBox(startX, startY);
             UpdatePictureBox(endX, endY);

         }*/
        private bool isGameOver = false;
        private void ApplyMove(string from, string to)
        {
            int startX = int.Parse(from[0].ToString());
            int startY = int.Parse(from[1].ToString());
            int endX = int.Parse(to[0].ToString());
            int endY = int.Parse(to[1].ToString());

            // Di chuyển quân cờ
            chessBoard.Board[endX, endY] = chessBoard.Board[startX, startY];
            chessBoard.Board[startX, startY] = null;

            // Cập nhật hình ảnh quân cờ
            UpdatePictureBox(startX, startY);
            UpdatePictureBox(endX, endY);

            // Kiểm tra xem quân vua của người chơi có bị bắt không
           if (!isGameOver && IsKingCaptured(playerColor))  // Kiểm tra nếu trò chơi chưa kết thúc
            {
                MessageBox.Show("Quân vua của bạn đã bị bắt! Trò chơi kết thúc.");
                isGameOver = true;  // Đánh dấu trò chơi đã kết thúc
                this.Close();  // Kết thúc trò chơi, hoặc bạn có thể thay thế bằng hành động khác
            }
        }


        private void UpdatePictureBox(int row, int col)
        {
            ChessPiece piece = chessBoard.Board[row, col];
            if (piece != null)
            {
                string imagePath = GetPieceImagePath(piece);
                if (!string.IsNullOrEmpty(imagePath))
                {
                    pictureBoxes[row, col].Image = Image.FromFile(imagePath);
                }
            }
            else
            {
                pictureBoxes[row, col].Image = null;
            }
        }

        private string GetPieceImagePath(ChessPiece piece)
        {
            string color = piece.Color == PieceColor.White ? "white" : "black";
            string type = piece.Type.ToString().ToLower();
            string imagePath = $"{color}_{type}.png";

            if (!System.IO.File.Exists(imagePath))
            {
                MessageBox.Show($"Không tìm thấy hình ảnh: {imagePath}");
                return null;
            }
            return imagePath;
        }

        private void HighlightValidMoves(int startX, int startY)
        {
            ChessPiece currentPiece = chessBoard.Board[startX, startY];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Kiểm tra nếu nước đi là hợp lệ
                    if (chessBoard.IsValidMove(startX, startY, i, j))
                    {
                        pictureBoxes[i, j].BackColor = Color.LightGreen; // Đánh dấu ô hợp lệ
                    }
                }
            }
        }

        private void ResetBoardColors()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    pictureBoxes[i, j].BackColor = (i + j) % 2 == 0 ? Color.White : Color.Gray;
                }
            }
        }
         private bool IsKingCaptured(string playerColor)
          {
              bool whiteKingExists = false;
              bool blackKingExists = false;

              foreach (var piece in chessBoard.Board)
              {
                  if (piece != null)
                  {
                      if (piece.Type == PieceType.King)
                      {
                          if (piece.Color == PieceColor.White)
                              whiteKingExists = true;
                          else if (piece.Color == PieceColor.Black)
                              blackKingExists = true;
                      }
                  }
              }

              return !whiteKingExists || !blackKingExists;
          }
      

        private bool IsOpponentKingCaptured()
        {
            PieceColor opponentColor = currentPlayer == PieceColor.White ? PieceColor.Black : PieceColor.White;

            foreach (var piece in chessBoard.Board)
            {
                if (piece != null && piece.Type == PieceType.King && piece.Color == opponentColor)
                {
                    return false; // Nếu tìm thấy quân vua của đối thủ, vua chưa bị bắt
                }
            }
            return true; // Nếu không tìm thấy quân vua của đối thủ, vua đã bị bắt
        }
        private bool CheckVictory(PieceColor playerColor)
        {
            if (playerColor == PieceColor.White && !IsKingExists(PieceColor.Black))
            {
                MessageBox.Show("Bạn đã chiến thắng!");
                return true;
            }
            else if (playerColor == PieceColor.Black && !IsKingExists(PieceColor.White))
            {
                MessageBox.Show("Bạn đã chiến thắng!");
                return true;
            }
            return false;
        }

        private bool IsKingExists(PieceColor kingColor)
        {
            foreach (var piece in chessBoard.Board)
            {
                if (piece != null && piece.Type == PieceType.King && piece.Color == kingColor)
                {
                    return true;
                }
            }
            return false;
        }


        private void lbl_luot_Click(object sender, EventArgs e)
        {
           
        }
        private void SwitchTurn()
        {
            // Chuyển lượt
            isPlayerTurn = !isPlayerTurn;

            // Đặt lại thời gian
            timeLeft = isPlayerTurn ? 600 : 600; // Thời gian cho người chơi hiện tại
            opponentTimeLeft = !isPlayerTurn ? 600 : 600; // Thời gian cho đối thủ

            // Cập nhật lại label thời gian
            UpdateTimeLabels();

            // Bắt đầu lại timer cho người chơi hiện tại
            timer.Start();
        }


        private async void btn_send_Click(object sender, EventArgs e)
        {
           // for (int i = 1; i <= 20; i++) ListenForOpponentChat();
            string message = txb_message.Text.Trim();
            if (message !="")
            {
                await client.SendMessageAsync("CHAT " + message);
                rtb_historychat.Text += "Bạn: " +message+'\n';
                txb_message.Text = "";
            }
         //   for (int i=1; i<=20; i++) ListenForOpponentChat();


        }
    }
}
