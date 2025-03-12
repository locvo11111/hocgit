namespace PhanMemQuanLyThiCong
{
    partial class Frm_FormBase
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
            this.panelData = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelData
            // 
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelData.Name = "panelData";
            this.panelData.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.panelData.Size = new System.Drawing.Size(881, 297);
            this.panelData.TabIndex = 0;
            // 
            // Frm_FormBase
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 297);
            this.Controls.Add(this.panelData);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_FormBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_FormBase";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_FormBase_FormClosed);
            this.Load += new System.EventHandler(this.Frm_FormBase_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelData;
    }
}