
namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    partial class Ctrl_KiemSoatSoLieuDocVao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ctrl_KiemSoatSoLieuDocVao));
            this.spread_ThamDinh = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sb_Export = new DevExpress.XtraEditors.SimpleButton();
            this.sb_CapNhap = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spread_ThamDinh
            // 
            this.spread_ThamDinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spread_ThamDinh.Location = new System.Drawing.Point(0, 31);
            this.spread_ThamDinh.Name = "spread_ThamDinh";
            this.spread_ThamDinh.Size = new System.Drawing.Size(1004, 530);
            this.spread_ThamDinh.TabIndex = 0;
            this.spread_ThamDinh.Text = "spreadsheetControl1";
            this.spread_ThamDinh.ActiveSheetChanged += new DevExpress.Spreadsheet.ActiveSheetChangedEventHandler(this.spread_ThamDinh_ActiveSheetChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sb_Export);
            this.panelControl1.Controls.Add(this.sb_CapNhap);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1004, 31);
            this.panelControl1.TabIndex = 3;
            // 
            // sb_Export
            // 
            this.sb_Export.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.sb_Export.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sb_Export.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_Export.Appearance.Options.UseBackColor = true;
            this.sb_Export.Appearance.Options.UseBorderColor = true;
            this.sb_Export.Appearance.Options.UseFont = true;
            this.sb_Export.Dock = System.Windows.Forms.DockStyle.Left;
            this.sb_Export.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sb_Export.ImageOptions.Image")));
            this.sb_Export.Location = new System.Drawing.Point(213, 2);
            this.sb_Export.Name = "sb_Export";
            this.sb_Export.Size = new System.Drawing.Size(124, 27);
            this.sb_Export.TabIndex = 1;
            this.sb_Export.Text = "Xuất File";
            this.sb_Export.Click += new System.EventHandler(this.sb_Export_Click);
            // 
            // sb_CapNhap
            // 
            this.sb_CapNhap.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sb_CapNhap.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sb_CapNhap.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_CapNhap.Appearance.Options.UseBackColor = true;
            this.sb_CapNhap.Appearance.Options.UseBorderColor = true;
            this.sb_CapNhap.Appearance.Options.UseFont = true;
            this.sb_CapNhap.Dock = System.Windows.Forms.DockStyle.Left;
            this.sb_CapNhap.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sb_CapNhap.ImageOptions.Image")));
            this.sb_CapNhap.Location = new System.Drawing.Point(2, 2);
            this.sb_CapNhap.Name = "sb_CapNhap";
            this.sb_CapNhap.Size = new System.Drawing.Size(211, 27);
            this.sb_CapNhap.TabIndex = 0;
            this.sb_CapNhap.Text = "Cập nhập dữ liệu theo dự án";
            this.sb_CapNhap.Click += new System.EventHandler(this.sb_CapNhap_Click);
            // 
            // Ctrl_KiemSoatSoLieuDocVao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spread_ThamDinh);
            this.Controls.Add(this.panelControl1);
            this.Name = "Ctrl_KiemSoatSoLieuDocVao";
            this.Size = new System.Drawing.Size(1004, 561);
            this.Load += new System.EventHandler(this.Ctrl_KiemSoatSoLieuDocVao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraSpreadsheet.SpreadsheetControl spread_ThamDinh;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sb_CapNhap;
        private DevExpress.XtraEditors.SimpleButton sb_Export;
    }
}
