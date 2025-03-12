
namespace PhanMemQuanLyThiCong
{
    partial class FormTestXuatTienDo
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
            this.spreadsheetControl1 = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // spreadsheetControl1
            // 
            this.spreadsheetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetControl1.Location = new System.Drawing.Point(0, 0);
            this.spreadsheetControl1.Name = "spreadsheetControl1";
            this.spreadsheetControl1.Size = new System.Drawing.Size(989, 539);
            this.spreadsheetControl1.TabIndex = 0;
            this.spreadsheetControl1.Text = "spreadsheetControl1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButton1.Location = new System.Drawing.Point(0, 492);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(989, 47);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.SvgImage = global::PhanMemQuanLyThiCong.Properties.Resources.actions_user;
            this.simpleButton2.Location = new System.Drawing.Point(198, 111);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "simpleButton2";
            // 
            // FormTestXuatTienDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 539);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.spreadsheetControl1);
            this.Name = "FormTestXuatTienDo";
            this.Text = "FormTestXuatTienDo";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}