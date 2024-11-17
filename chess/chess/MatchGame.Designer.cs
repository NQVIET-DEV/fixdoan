namespace chess
{
    partial class MatchGame
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
            this.btn_find = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_find
            // 
            this.btn_find.Location = new System.Drawing.Point(81, 383);
            this.btn_find.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_find.Name = "btn_find";
            this.btn_find.Size = new System.Drawing.Size(256, 56);
            this.btn_find.TabIndex = 0;
            this.btn_find.Text = "Find";
            this.btn_find.UseVisualStyleBackColor = true;
            this.btn_find.Click += new System.EventHandler(this.btn_find_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(173, 53);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(112, 29);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "label1";
            this.lblStatus.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(81, 461);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(256, 56);
            this.button1.TabIndex = 2;
            this.button1.Text = "Play with AI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MatchGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 562);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btn_find);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MatchGame";
            this.Text = "MatchGame";
            this.Load += new System.EventHandler(this.MatchGame_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_find;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button button1;
    }
}