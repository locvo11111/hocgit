
namespace PhanMemQuanLyThiCong
{
    partial class Formtest
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
            this.messagesView1 = new PhanMemQuanLyThiCong.Views.MessagesView();
            this.SuspendLayout();
            // 
            // messagesView1
            // 
            this.messagesView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesView1.Location = new System.Drawing.Point(0, 0);
            this.messagesView1.Margin = new System.Windows.Forms.Padding(0);
            this.messagesView1.Name = "messagesView1";
            this.messagesView1.Size = new System.Drawing.Size(800, 450);
            this.messagesView1.TabIndex = 0;
            // 
            // Formtest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.messagesView1);
            this.Name = "Formtest";
            this.Text = "Formtest";
            this.Load += new System.EventHandler(this.Formtest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PhanMemQuanLyThiCong.Views.MessagesView messagesView1;
    }
}