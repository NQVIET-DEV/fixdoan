namespace chess
{
    partial class Login_2_
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_2_));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_tk = new System.Windows.Forms.TextBox();
            this.txt_mk = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelSignUp = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(137, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "CHESS GAME";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(215, 173);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(113, 92);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // txt_tk
            // 
            this.txt_tk.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tk.ForeColor = System.Drawing.Color.Black;
            this.txt_tk.Location = new System.Drawing.Point(145, 320);
            this.txt_tk.Multiline = true;
            this.txt_tk.Name = "txt_tk";
            this.txt_tk.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_tk.Size = new System.Drawing.Size(249, 35);
            this.txt_tk.TabIndex = 11;
            this.txt_tk.Enter += new System.EventHandler(this.txt_tk_Enter);
            this.txt_tk.Leave += new System.EventHandler(this.txt_tk_Leave);
            // 
            // txt_mk
            // 
            this.txt_mk.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mk.ForeColor = System.Drawing.Color.Black;
            this.txt_mk.Location = new System.Drawing.Point(145, 374);
            this.txt_mk.Multiline = true;
            this.txt_mk.Name = "txt_mk";
            this.txt_mk.Size = new System.Drawing.Size(249, 35);
            this.txt_mk.TabIndex = 12;
            this.txt_mk.Enter += new System.EventHandler(this.txt_mk_Enter);
            this.txt_mk.Leave += new System.EventHandler(this.txt_mk_Leave);
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(236, 438);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(81, 35);
            this.btn_login.TabIndex = 13;
            this.btn_login.Text = "LOGIN";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 497);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Chưa có tài khoản?";
            // 
            // linkLabelSignUp
            // 
            this.linkLabelSignUp.AutoSize = true;
            this.linkLabelSignUp.Location = new System.Drawing.Point(329, 497);
            this.linkLabelSignUp.Name = "linkLabelSignUp";
            this.linkLabelSignUp.Size = new System.Drawing.Size(67, 20);
            this.linkLabelSignUp.TabIndex = 15;
            this.linkLabelSignUp.TabStop = true;
            this.linkLabelSignUp.Text = "Đăng ký";
            this.linkLabelSignUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSignUp_LinkClicked);
            // 
            // Login_2_
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = global::chess.Properties.Resources.backgroundgamechesss;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(526, 684);
            this.Controls.Add(this.linkLabelSignUp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_mk);
            this.Controls.Add(this.txt_tk);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "Login_2_";
            this.Text = "ChessLogin";
            this.Load += new System.EventHandler(this.Login_2__Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_tk;
        private System.Windows.Forms.TextBox txt_mk;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelSignUp;
    }
}