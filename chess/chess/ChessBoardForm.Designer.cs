namespace chess
{
    partial class ChessBoardForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_luot = new System.Windows.Forms.Label();
            this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
            this.timelabel = new System.Windows.Forms.Label();
            this.opponentTimeLabel = new System.Windows.Forms.Label();
            this.rtb_historychat = new System.Windows.Forms.RichTextBox();
            this.Chat = new System.Windows.Forms.Label();
            this.txb_message = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 15);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(873, 588);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_luot
            // 
            this.lbl_luot.Font = new System.Drawing.Font("MS Reference Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_luot.Location = new System.Drawing.Point(909, 561);
            this.lbl_luot.Name = "lbl_luot";
            this.lbl_luot.Size = new System.Drawing.Size(180, 59);
            this.lbl_luot.TabIndex = 1;
            this.lbl_luot.Text = "Lượt của: ";
            this.lbl_luot.Click += new System.EventHandler(this.lbl_luot_Click);
            // 
            // sqLiteCommand1
            // 
            this.sqLiteCommand1.CommandText = null;
            // 
            // timelabel
            // 
            this.timelabel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timelabel.Location = new System.Drawing.Point(909, 498);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(231, 59);
            this.timelabel.TabIndex = 3;
            this.timelabel.Text = "Thời gian còn lại:";
            // 
            // opponentTimeLabel
            // 
            this.opponentTimeLabel.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opponentTimeLabel.Location = new System.Drawing.Point(909, 474);
            this.opponentTimeLabel.Name = "opponentTimeLabel";
            this.opponentTimeLabel.Size = new System.Drawing.Size(180, 59);
            this.opponentTimeLabel.TabIndex = 2;
            // 
            // rtb_historychat
            // 
            this.rtb_historychat.Location = new System.Drawing.Point(907, 84);
            this.rtb_historychat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtb_historychat.Name = "rtb_historychat";
            this.rtb_historychat.Size = new System.Drawing.Size(417, 325);
            this.rtb_historychat.TabIndex = 4;
            this.rtb_historychat.Text = "";
            // 
            // Chat
            // 
            this.Chat.Font = new System.Drawing.Font("MS Reference Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chat.Location = new System.Drawing.Point(962, 34);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(237, 46);
            this.Chat.TabIndex = 5;
            this.Chat.Text = "Chat với đối thủ";
            // 
            // txb_message
            // 
            this.txb_message.Location = new System.Drawing.Point(914, 445);
            this.txb_message.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txb_message.Name = "txb_message";
            this.txb_message.Size = new System.Drawing.Size(271, 26);
            this.txb_message.TabIndex = 6;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(1221, 441);
            this.btn_send.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(92, 36);
            this.btn_send.TabIndex = 7;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // ChessBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 631);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txb_message);
            this.Controls.Add(this.Chat);
            this.Controls.Add(this.rtb_historychat);
            this.Controls.Add(this.timelabel);
            this.Controls.Add(this.opponentTimeLabel);
            this.Controls.Add(this.lbl_luot);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChessBoardForm";
            this.Text = "ChessBoardForm";
            this.Load += new System.EventHandler(this.ChessBoardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_luot;
        private System.Data.SQLite.SQLiteCommand sqLiteCommand1;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.Label opponentTimeLabel;
        private System.Windows.Forms.RichTextBox rtb_historychat;
        public System.Windows.Forms.Label Chat;
        private System.Windows.Forms.TextBox txb_message;
        private System.Windows.Forms.Button btn_send;
    }
}