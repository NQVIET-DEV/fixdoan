using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private async void btn_singup_Click(object sender, EventArgs e)
        {
            string username = txt_tk.Text;
            string password = txt_mk.Text;

            // Kết nối tới server TCP
            using (TcpClient client = new TcpClient("127.0.0.1", 8080)) // Thay 5000 bằng cổng của server
            {
                NetworkStream stream = client.GetStream();
                string registerCommand = $"REGISTER {username} {password}";
                byte[] data = Encoding.UTF8.GetBytes(registerCommand);

                await stream.WriteAsync(data, 0, data.Length);
                data = new byte[1024];
                int bytesRead = await stream.ReadAsync(data, 0, data.Length);
                string response = Encoding.UTF8.GetString(data, 0, bytesRead);

                MessageBox.Show(response);
            this.Close();
                Login_2_ dangnhaplai = new Login_2_();
                dangnhaplai.Show();
            }
        }
    }
}
