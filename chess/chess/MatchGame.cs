using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess
{
    public partial class MatchGame : Form
    {
        private TCPClient client;
        private bool isFindingMatch = false;

        public MatchGame(TCPClient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private async void btn_find_Click(object sender, EventArgs e)
        {
            if (isFindingMatch)
            {
                MessageBox.Show("Đang tìm trận. Vui lòng chờ...");
                return;
            }

            isFindingMatch = true;
            lblStatus.Text = "Đang tìm trận...";

            // Gửi yêu cầu tìm trận
            await FindMatchAsync();
        }

        private async Task FindMatchAsync()
        {
            try
            {
                // Gửi yêu cầu tìm trận tới server
                string response = await client.FindMatchAsync();

                // Kiểm tra phản hồi từ server
                if (response.StartsWith("WAITING"))
                {
                    lblStatus.Text = "Chờ đợi người chơi khác...";
                    await ListenForMatchAsync(); // Lắng nghe thông báo tìm thấy trận
                }
                else if (response.StartsWith("MATCH_FOUND"))
                {
                    OpenChessBoard(response); // Mở bàn cờ nếu tìm thấy trận
                }
                else
                {
                    lblStatus.Text = "Lỗi: " + response;
                    isFindingMatch = false;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi: " + ex.Message;
                isFindingMatch = false;
            }
        }

        private async Task ListenForMatchAsync()
        {
            while (true)
            {
                // Đọc phản hồi từ server về việc tìm trận
                string response = await client.WaitForMatchAsync();

                if (response.StartsWith("MATCH_FOUND"))
                {
                    OpenChessBoard(response); // Mở bàn cờ khi trận đấu được tìm thấy
                    break;
                }
                else
                {
                    lblStatus.Text = response;
                    await Task.Delay(1000); // Đợi một chút rồi kiểm tra lại
                }
            }
        }

        private void OpenChessBoard(string matchResponse)
        {
            if (string.IsNullOrEmpty(matchResponse) || !matchResponse.StartsWith("MATCH_FOUND"))
            {
                MessageBox.Show("Lỗi: Phản hồi trận đấu không hợp lệ.");
                return;
            }

            string[] parts = matchResponse.Split(' ');

            if (parts.Length < 2)
            {
                MessageBox.Show("Lỗi: Định dạng phản hồi trận đấu không hợp lệ.");
                return;
            }

            string playerColor = parts[1]; // "WHITE" hoặc "BLACK"
            MessageBox.Show($"Trận đấu đã tìm thấy! Bạn đang chơi với màu: {playerColor}");

            // Mở bàn cờ không chặn giao diện
            ChessBoardForm chessBoard = new ChessBoardForm(client, playerColor);
            this.Hide();
            chessBoard.Show();
            chessBoard.FormClosed += (s, e) => this.Show();
        }

        private void MatchGame_Load(object sender, EventArgs e)
        {
            // Thêm logic nếu cần khi form tải
        }
        private void label1_Click(object sender, EventArgs e)
        {
            // Thêm logic nếu cần khi nhấp vào label
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
          //  form1.FormClosed += (s, e) => this.Show();
        }
        private void FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            client.Close();
        }
    }
}