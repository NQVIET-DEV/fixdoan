using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.SQLite;
using System.Net.Sockets;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace chess
{
    public partial class Login_2_ : Form
    {
        private TCPClient client;
        private bool islogin = false;
        public Login_2_()
        {
            InitializeComponent();
        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                if (client == null)
                {
                    client = new TCPClient("127.0.0.1", 8080);
                }

                string username = txt_tk.Text;
                string password = txt_mk.Text;
                string response = await client.LoginAsync(username, password);

                // Cập nhật UI sau khi hoàn tất đăng nhập
                this.Invoke(new Action(() =>
                {
                    if (response.StartsWith("SUCCESS"))
                    {
                        MessageBox.Show("Đăng nhập thành công!");
                        MatchGame matchGame = new MatchGame(client);

                        matchGame.FormClosed += (s, args) =>
                        {
                            client.Close(); // Ngắt kết nối tới server
                            Application.Exit(); // Đóng hoàn toàn ứng dụng
                        };
                        this.Hide();
                        matchGame.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(response);
                    }
                }));
            });
        }

        private void txt_tk_Enter(object sender, EventArgs e)
        {
            if (txt_tk.Text == "Username")
            {
                txt_tk.Text = "";  // Xóa văn bản "username" khi người dùng nhấp vào
                txt_tk.ForeColor = Color.Black; // Đặt lại màu chữ nếu cần
            }
        }

        private void txt_tk_Leave(object sender, EventArgs e)
        {
            if (txt_tk.Text == "")
            {
                txt_tk.Text = "Username";  // Đặt lại văn bản mặc định nếu người dùng không nhập gì
                txt_tk.ForeColor = Color.LightGray; // Đổi màu chữ cho văn bản mặc định
            }
        }

        private void txt_mk_Enter(object sender, EventArgs e)
        {
            if (txt_mk.Text == "Password")
            {
                txt_mk.Text = "";  // Xóa văn bản "username" khi người dùng nhấp vào
                txt_mk.ForeColor = Color.Black; // Đặt lại màu chữ nếu cần
            }
        }

        private void txt_mk_Leave(object sender, EventArgs e)
        {
            if (txt_mk.Text == "")
            {
                txt_mk.Text = "Password";  // Đặt lại văn bản mặc định nếu người dùng không nhập gì
                txt_mk.ForeColor = Color.LightGray; // Đổi màu chữ cho văn bản mặc định
            }
        }

        private void Login_2__Load(object sender, EventArgs e)
        {
            txt_tk.Text = "Username"; // Gán giá trị mặc định khi form load
            txt_tk.ForeColor = Color.LightGray; // Đổi màu chữ khi là placeholder
            txt_mk.Text = "Password"; // Gán giá trị mặc định khi form load
            txt_mk.ForeColor = Color.LightGray; // Đổi màu chữ khi là placeholder
        }

        private void linkLabelSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SignUp dangki = new SignUp();
            dangki.Show();
        }
    }
}
