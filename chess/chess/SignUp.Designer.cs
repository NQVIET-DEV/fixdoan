namespace chess
{
    partial class SignUp
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
            this.btn_singup = new System.Windows.Forms.Button();
            this.txt_mk = new System.Windows.Forms.TextBox();
            this.txt_tk = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_singup
            // 
            this.btn_singup.Location = new System.Drawing.Point(196, 370);
            this.btn_singup.Name = "btn_singup";
            this.btn_singup.Size = new System.Drawing.Size(97, 35);
            this.btn_singup.TabIndex = 20;
            this.btn_singup.Text = "SIGN UP";
            this.btn_singup.UseVisualStyleBackColor = true;
            this.btn_singup.Click += new System.EventHandler(this.btn_singup_Click);
            // 
            // txt_mk
            // 
            this.txt_mk.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mk.ForeColor = System.Drawing.Color.Black;
            this.txt_mk.Location = new System.Drawing.Point(122, 306);
            this.txt_mk.Multiline = true;
            this.txt_mk.Name = "txt_mk";
            this.txt_mk.Size = new System.Drawing.Size(249, 35);
            this.txt_mk.TabIndex = 19;
            // 
            // txt_tk
            // 
            this.txt_tk.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tk.ForeColor = System.Drawing.Color.Black;
            this.txt_tk.Location = new System.Drawing.Point(122, 249);
            this.txt_tk.Multiline = true;
            this.txt_tk.Name = "txt_tk";
            this.txt_tk.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_tk.Size = new System.Drawing.Size(249, 35);
            this.txt_tk.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(170, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 43);
            this.label1.TabIndex = 16;
            this.label1.Text = "SIGN UP";
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::chess.Properties.Resources.backgroundgamechesss;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(473, 649);
            this.Controls.Add(this.btn_singup);
            this.Controls.Add(this.txt_mk);
            this.Controls.Add(this.txt_tk);
            this.Controls.Add(this.label1);
            this.Name = "SignUp";
            this.Text = "SignUp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_singup;
        private System.Windows.Forms.TextBox txt_mk;
        private System.Windows.Forms.TextBox txt_tk;
        private System.Windows.Forms.Label label1;
    }
}