namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_TestChatBox
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
            this.uc_ChatBox1 = new PhanMemQuanLyThiCong.Urcs.Uc_ChatBox();
            this.SuspendLayout();
            // 
            // uc_ChatBox1
            // 
            this.uc_ChatBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_ChatBox1.Location = new System.Drawing.Point(0, 0);
            this.uc_ChatBox1.Name = "uc_ChatBox1";
            this.uc_ChatBox1.Size = new System.Drawing.Size(1078, 525);
            this.uc_ChatBox1.TabIndex = 0;
            // 
            // XtraForm_TestChatBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 525);
            this.Controls.Add(this.uc_ChatBox1);
            this.Name = "XtraForm_TestChatBox";
            this.Text = "XtraForm_TestChatBox";
            this.ResumeLayout(false);

        }

        #endregion

        private Urcs.Uc_ChatBox uc_ChatBox1;
    }
}