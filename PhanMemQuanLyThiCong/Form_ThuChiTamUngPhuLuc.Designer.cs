
namespace PhanMemQuanLyThiCong
{
    partial class Form_ThuChiTamUngPhuLuc
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gc_PhuLucThuChi = new DevExpress.XtraGrid.GridControl();
            this.gv_PhuLucThuChi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.STT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenCongViec = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KhoiLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenNoiDungUng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rIDE_Date = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_PhuLucThuChi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PhuLucThuChi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_Date)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_Date.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gc_PhuLucThuChi);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1303, 500);
            this.panelControl1.TabIndex = 0;
            // 
            // gc_PhuLucThuChi
            // 
            this.gc_PhuLucThuChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_PhuLucThuChi.Location = new System.Drawing.Point(2, 2);
            this.gc_PhuLucThuChi.MainView = this.gv_PhuLucThuChi;
            this.gc_PhuLucThuChi.Name = "gc_PhuLucThuChi";
            this.gc_PhuLucThuChi.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rIDE_Date});
            this.gc_PhuLucThuChi.Size = new System.Drawing.Size(1299, 496);
            this.gc_PhuLucThuChi.TabIndex = 5;
            this.gc_PhuLucThuChi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_PhuLucThuChi});
            // 
            // gv_PhuLucThuChi
            // 
            this.gv_PhuLucThuChi.Appearance.FooterPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gv_PhuLucThuChi.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.gv_PhuLucThuChi.Appearance.FooterPanel.Options.UseFont = true;
            this.gv_PhuLucThuChi.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gv_PhuLucThuChi.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_PhuLucThuChi.AppearancePrint.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.gv_PhuLucThuChi.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.gv_PhuLucThuChi.AppearancePrint.FooterPanel.Options.UseForeColor = true;
            this.gv_PhuLucThuChi.AppearancePrint.FooterPanel.Options.UseTextOptions = true;
            this.gv_PhuLucThuChi.AppearancePrint.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_PhuLucThuChi.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.STT,
            this.TenCongViec,
            this.KhoiLuong,
            this.DonGia,
            this.ThanhTien,
            this.TenNoiDungUng,
            this.Code,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gv_PhuLucThuChi.GridControl = this.gc_PhuLucThuChi;
            this.gv_PhuLucThuChi.Name = "gv_PhuLucThuChi";
            this.gv_PhuLucThuChi.OptionsMenu.EnableFooterMenu = false;
            this.gv_PhuLucThuChi.OptionsView.ColumnAutoWidth = false;
            this.gv_PhuLucThuChi.OptionsView.ShowFooter = true;
            this.gv_PhuLucThuChi.OptionsView.ShowGroupPanel = false;
            this.gv_PhuLucThuChi.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gv_PhuLucThuChi_ShowingEditor);
            this.gv_PhuLucThuChi.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gv_PhuLucThuChi_CellValueChanged);
            this.gv_PhuLucThuChi.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gv_PhuLucThuChi_KeyUp);
            // 
            // STT
            // 
            this.STT.Caption = "STT";
            this.STT.FieldName = "STT";
            this.STT.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.STT.Name = "STT";
            this.STT.OptionsColumn.ReadOnly = true;
            this.STT.Visible = true;
            this.STT.VisibleIndex = 0;
            // 
            // TenCongViec
            // 
            this.TenCongViec.Caption = "Tên công việc";
            this.TenCongViec.FieldName = "TenCongViec";
            this.TenCongViec.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.TenCongViec.Name = "TenCongViec";
            this.TenCongViec.OptionsColumn.ReadOnly = true;
            this.TenCongViec.Visible = true;
            this.TenCongViec.VisibleIndex = 2;
            this.TenCongViec.Width = 294;
            // 
            // KhoiLuong
            // 
            this.KhoiLuong.Caption = "Khối lượng";
            this.KhoiLuong.DisplayFormat.FormatString = "n2";
            this.KhoiLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.KhoiLuong.FieldName = "KhoiLuong";
            this.KhoiLuong.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.KhoiLuong.Name = "KhoiLuong";
            this.KhoiLuong.Visible = true;
            this.KhoiLuong.VisibleIndex = 4;
            this.KhoiLuong.Width = 121;
            // 
            // DonGia
            // 
            this.DonGia.Caption = "Đơn giá";
            this.DonGia.DisplayFormat.FormatString = "n2";
            this.DonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DonGia.FieldName = "DonGia";
            this.DonGia.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.DonGia.Name = "DonGia";
            this.DonGia.Visible = true;
            this.DonGia.VisibleIndex = 5;
            this.DonGia.Width = 159;
            // 
            // ThanhTien
            // 
            this.ThanhTien.Caption = "Thành tiền";
            this.ThanhTien.DisplayFormat.FormatString = "n0";
            this.ThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ThanhTien.FieldName = "ThanhTien";
            this.ThanhTien.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.ThanhTien.Name = "ThanhTien";
            this.ThanhTien.OptionsColumn.ReadOnly = true;
            this.ThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThanhTien", "Tổng Thành Tiền= {0:c2}")});
            this.ThanhTien.Visible = true;
            this.ThanhTien.VisibleIndex = 6;
            this.ThanhTien.Width = 210;
            // 
            // TenNoiDungUng
            // 
            this.TenNoiDungUng.Caption = "Tên nội dung ứng";
            this.TenNoiDungUng.FieldName = "TenNoiDungUng";
            this.TenNoiDungUng.Name = "TenNoiDungUng";
            this.TenNoiDungUng.OptionsColumn.ReadOnly = true;
            this.TenNoiDungUng.Visible = true;
            this.TenNoiDungUng.VisibleIndex = 7;
            this.TenNoiDungUng.Width = 59;
            // 
            // Code
            // 
            this.Code.Caption = "Code";
            this.Code.FieldName = "Code";
            this.Code.Name = "Code";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ngày bắt đầu";
            this.gridColumn1.ColumnEdit = this.rIDE_Date;
            this.gridColumn1.FieldName = "NgayBD";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Width = 132;
            // 
            // rIDE_Date
            // 
            this.rIDE_Date.AutoHeight = false;
            this.rIDE_Date.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rIDE_Date.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rIDE_Date.MinValue = new System.DateTime(2023, 3, 15, 21, 48, 55, 0);
            this.rIDE_Date.Name = "rIDE_Date";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ngày kết thúc";
            this.gridColumn2.ColumnEdit = this.rIDE_Date;
            this.gridColumn2.FieldName = "NgayKT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Width = 117;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã hiệu";
            this.gridColumn3.FieldName = "MaHieu";
            this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Đơn vị";
            this.gridColumn4.FieldName = "DonVi";
            this.gridColumn4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 108;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Thành tiền";
            this.gridColumn5.FieldName = "ThanhTienThuCong";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThanhTienThuCong", "Tổng Thành Tiền= {0:c2}")});
            this.gridColumn5.Width = 168;
            // 
            // Form_ThuChiTamUngPhuLuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 500);
            this.Controls.Add(this.panelControl1);
            this.Name = "Form_ThuChiTamUngPhuLuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phụ lục Thu chi";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_ThuChiTamUngPhuLuc_FormClosed);
            this.Load += new System.EventHandler(this.Form_ThuChiTamUngPhuLuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_PhuLucThuChi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PhuLucThuChi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_Date.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_Date)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gc_PhuLucThuChi;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_PhuLucThuChi;
        private DevExpress.XtraGrid.Columns.GridColumn STT;
        private DevExpress.XtraGrid.Columns.GridColumn TenCongViec;
        private DevExpress.XtraGrid.Columns.GridColumn KhoiLuong;
        private DevExpress.XtraGrid.Columns.GridColumn DonGia;
        private DevExpress.XtraGrid.Columns.GridColumn ThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn TenNoiDungUng;
        private DevExpress.XtraGrid.Columns.GridColumn Code;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rIDE_Date;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}