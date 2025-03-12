
namespace PhanMemQuanLyThiCong.Controls.CongVanDiDen
{
    partial class Ctrl_CongVanDiDen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spsheet_CongVanDiDen = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.SuspendLayout();
            // 
            // spsheet_CongVanDiDen
            // 
            this.spsheet_CongVanDiDen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spsheet_CongVanDiDen.Location = new System.Drawing.Point(0, 0);
            this.spsheet_CongVanDiDen.Margin = new System.Windows.Forms.Padding(2);
            this.spsheet_CongVanDiDen.Name = "spsheet_CongVanDiDen";
            this.spsheet_CongVanDiDen.Options.Culture = new System.Globalization.CultureInfo("vi-VN");
            this.spsheet_CongVanDiDen.Size = new System.Drawing.Size(1098, 611);
            this.spsheet_CongVanDiDen.TabIndex = 2;
            this.spsheet_CongVanDiDen.Text = "spreadsheetControl2";
            this.spsheet_CongVanDiDen.PopupMenuShowing += new DevExpress.XtraSpreadsheet.PopupMenuShowingEventHandler(this.spsheet_CongVanDiDen_PopupMenuShowing);
            this.spsheet_CongVanDiDen.CellBeginEdit += new DevExpress.XtraSpreadsheet.CellBeginEditEventHandler(this.spsheet_CongVanDiDen_CellBeginEdit);
            this.spsheet_CongVanDiDen.CellValueChanged += new DevExpress.XtraSpreadsheet.CellValueChangedEventHandler(this.spsheet_CongVanDiDen_CellValueChanged);
            this.spsheet_CongVanDiDen.HyperlinkClick += new DevExpress.XtraSpreadsheet.HyperlinkClickEventHandler(this.spsheet_CongVanDiDen_HyperlinkClick);
            // 
            // Ctrl_CongVanDiDen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spsheet_CongVanDiDen);
            this.Name = "Ctrl_CongVanDiDen";
            this.Size = new System.Drawing.Size(1098, 611);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraSpreadsheet.SpreadsheetControl spsheet_CongVanDiDen;
    }
}
