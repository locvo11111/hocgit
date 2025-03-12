using DevExpress.XtraGrid.Columns;

namespace PhanMemQuanLyThiCong.ChatBox.Views
{
    partial class BusinessTableView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessTableView));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Export = new DevExpress.XtraEditors.SimpleButton();
            this.giaoViecViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl = new DevExpress.XtraTreeList.TreeList();
            this.colMaDinhMuc1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTenCongViec1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colDonVi1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNgayBatDau1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNgayKetThuc1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colKhoiLuongHopDong1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colKhoiLuongKeHoach1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGhiChu1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTrangThai1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTenHangMuc1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTenCongTrinh1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTenDauViecNho1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTenDauViecLon1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTenDuAn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTotalFile1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTotalApprove1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFullNameSend1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNgayGuiDuyet1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFullNameApprove1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNgayDuyet1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGhiChuDuyet1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.giaoViecViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btn_Export);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 464);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1173, 33);
            this.panelControl1.TabIndex = 0;
            // 
            // btn_Export
            // 
            this.btn_Export.Appearance.BackColor = System.Drawing.Color.Blue;
            this.btn_Export.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Appearance.Options.UseBackColor = true;
            this.btn_Export.Appearance.Options.UseFont = true;
            this.btn_Export.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Export.ImageOptions.Image")));
            this.btn_Export.Location = new System.Drawing.Point(29, 5);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(187, 23);
            this.btn_Export.TabIndex = 2;
            this.btn_Export.Text = "Xuất báo cáo hàng ngày";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // giaoViecViewModelBindingSource
            // 
            this.giaoViecViewModelBindingSource.DataSource = typeof(PhanMemQuanLyThiCong.Model.GiaoViecExtensionViewModel);
            // 
            // gridControl
            // 
            this.gridControl.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridControl.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Green;
            this.gridControl.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridControl.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridControl.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colMaDinhMuc1,
            this.colTenCongViec1,
            this.colDonVi1,
            this.colNgayBatDau1,
            this.colNgayKetThuc1,
            this.colKhoiLuongHopDong1,
            this.colKhoiLuongKeHoach1,
            this.colGhiChu1,
            this.colTrangThai1,
            this.colTenHangMuc1,
            this.colTenCongTrinh1,
            this.colTenDauViecNho1,
            this.colTenDauViecLon1,
            this.colTenDuAn1,
            this.colTotalFile1,
            this.colTotalApprove1,
            this.colFullNameSend1,
            this.colNgayGuiDuyet1,
            this.colFullNameApprove1,
            this.colNgayDuyet1,
            this.colGhiChuDuyet1});
            this.gridControl.DataSource = this.giaoViecViewModelBindingSource;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.KeyFieldName = "IdTreelist";
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.Name = "gridControl";
            this.gridControl.OptionsBehavior.ReadOnly = true;
            this.gridControl.ParentFieldName = "ParentIdTreelist";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gridControl.Size = new System.Drawing.Size(1173, 464);
            this.gridControl.TabIndex = 2;
            // 
            // colMaDinhMuc1
            // 
            this.colMaDinhMuc1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colMaDinhMuc1.AppearanceHeader.Options.UseFont = true;
            this.colMaDinhMuc1.Caption = "Mã công tác";
            this.colMaDinhMuc1.FieldName = "MaDinhMuc";
            this.colMaDinhMuc1.MinWidth = 90;
            this.colMaDinhMuc1.Name = "colMaDinhMuc1";
            this.colMaDinhMuc1.Visible = true;
            this.colMaDinhMuc1.VisibleIndex = 0;
            this.colMaDinhMuc1.Width = 90;
            // 
            // colTenCongViec1
            // 
            this.colTenCongViec1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTenCongViec1.AppearanceHeader.Options.UseFont = true;
            this.colTenCongViec1.Caption = "Tên công tác";
            this.colTenCongViec1.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colTenCongViec1.FieldName = "TenCongViec";
            this.colTenCongViec1.MinWidth = 200;
            this.colTenCongViec1.Name = "colTenCongViec1";
            this.colTenCongViec1.Visible = true;
            this.colTenCongViec1.VisibleIndex = 1;
            this.colTenCongViec1.Width = 200;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colDonVi1
            // 
            this.colDonVi1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDonVi1.AppearanceHeader.Options.UseFont = true;
            this.colDonVi1.Caption = "Đơn vị";
            this.colDonVi1.FieldName = "DonVi";
            this.colDonVi1.MinWidth = 70;
            this.colDonVi1.Name = "colDonVi1";
            this.colDonVi1.Visible = true;
            this.colDonVi1.VisibleIndex = 2;
            this.colDonVi1.Width = 70;
            // 
            // colNgayBatDau1
            // 
            this.colNgayBatDau1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNgayBatDau1.AppearanceHeader.Options.UseFont = true;
            this.colNgayBatDau1.Caption = "Ngày bắt đầu";
            this.colNgayBatDau1.FieldName = "NgayBatDau";
            this.colNgayBatDau1.MinWidth = 90;
            this.colNgayBatDau1.Name = "colNgayBatDau1";
            this.colNgayBatDau1.Visible = true;
            this.colNgayBatDau1.VisibleIndex = 3;
            this.colNgayBatDau1.Width = 90;
            // 
            // colNgayKetThuc1
            // 
            this.colNgayKetThuc1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNgayKetThuc1.AppearanceHeader.Options.UseFont = true;
            this.colNgayKetThuc1.Caption = "Ngày kết thúc";
            this.colNgayKetThuc1.FieldName = "NgayKetThuc";
            this.colNgayKetThuc1.MinWidth = 90;
            this.colNgayKetThuc1.Name = "colNgayKetThuc1";
            this.colNgayKetThuc1.Visible = true;
            this.colNgayKetThuc1.VisibleIndex = 4;
            this.colNgayKetThuc1.Width = 90;
            // 
            // colKhoiLuongHopDong1
            // 
            this.colKhoiLuongHopDong1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colKhoiLuongHopDong1.AppearanceHeader.Options.UseFont = true;
            this.colKhoiLuongHopDong1.Caption = "KL hợp đồng";
            this.colKhoiLuongHopDong1.FieldName = "KhoiLuongHopDong";
            this.colKhoiLuongHopDong1.MinWidth = 90;
            this.colKhoiLuongHopDong1.Name = "colKhoiLuongHopDong1";
            this.colKhoiLuongHopDong1.Visible = true;
            this.colKhoiLuongHopDong1.VisibleIndex = 5;
            this.colKhoiLuongHopDong1.Width = 90;
            // 
            // colKhoiLuongKeHoach1
            // 
            this.colKhoiLuongKeHoach1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colKhoiLuongKeHoach1.AppearanceHeader.Options.UseFont = true;
            this.colKhoiLuongKeHoach1.Caption = "KL kế hoạch";
            this.colKhoiLuongKeHoach1.FieldName = "KhoiLuongKeHoach";
            this.colKhoiLuongKeHoach1.MinWidth = 90;
            this.colKhoiLuongKeHoach1.Name = "colKhoiLuongKeHoach1";
            this.colKhoiLuongKeHoach1.Visible = true;
            this.colKhoiLuongKeHoach1.VisibleIndex = 6;
            this.colKhoiLuongKeHoach1.Width = 90;
            // 
            // colGhiChu1
            // 
            this.colGhiChu1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGhiChu1.AppearanceHeader.Options.UseFont = true;
            this.colGhiChu1.Caption = "Ghi chú";
            this.colGhiChu1.FieldName = "GhiChu";
            this.colGhiChu1.MinWidth = 70;
            this.colGhiChu1.Name = "colGhiChu1";
            this.colGhiChu1.Visible = true;
            this.colGhiChu1.VisibleIndex = 7;
            this.colGhiChu1.Width = 70;
            // 
            // colTrangThai1
            // 
            this.colTrangThai1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTrangThai1.AppearanceHeader.Options.UseFont = true;
            this.colTrangThai1.Caption = "Trạng thái";
            this.colTrangThai1.FieldName = "TrangThai";
            this.colTrangThai1.MinWidth = 100;
            this.colTrangThai1.Name = "colTrangThai1";
            this.colTrangThai1.Visible = true;
            this.colTrangThai1.VisibleIndex = 8;
            this.colTrangThai1.Width = 100;
            // 
            // colTenHangMuc1
            // 
            this.colTenHangMuc1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTenHangMuc1.AppearanceHeader.Options.UseFont = true;
            this.colTenHangMuc1.Caption = "KL thanh toán";
            this.colTenHangMuc1.FieldName = "TenHangMuc";
            this.colTenHangMuc1.MinWidth = 90;
            this.colTenHangMuc1.Name = "colTenHangMuc1";
            this.colTenHangMuc1.Visible = true;
            this.colTenHangMuc1.VisibleIndex = 9;
            this.colTenHangMuc1.Width = 90;
            // 
            // colTenCongTrinh1
            // 
            this.colTenCongTrinh1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTenCongTrinh1.AppearanceHeader.Options.UseFont = true;
            this.colTenCongTrinh1.Caption = "Tên công trình";
            this.colTenCongTrinh1.FieldName = "TenCongTrinh";
            this.colTenCongTrinh1.MinWidth = 120;
            this.colTenCongTrinh1.Name = "colTenCongTrinh1";
            this.colTenCongTrinh1.Visible = true;
            this.colTenCongTrinh1.VisibleIndex = 10;
            this.colTenCongTrinh1.Width = 120;
            // 
            // colTenDauViecNho1
            // 
            this.colTenDauViecNho1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTenDauViecNho1.AppearanceHeader.Options.UseFont = true;
            this.colTenDauViecNho1.Caption = "Tên đầu việc nhỏ";
            this.colTenDauViecNho1.FieldName = "TenDauViecNho";
            this.colTenDauViecNho1.MinWidth = 120;
            this.colTenDauViecNho1.Name = "colTenDauViecNho1";
            this.colTenDauViecNho1.Visible = true;
            this.colTenDauViecNho1.VisibleIndex = 11;
            this.colTenDauViecNho1.Width = 120;
            // 
            // colTenDauViecLon1
            // 
            this.colTenDauViecLon1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTenDauViecLon1.AppearanceHeader.Options.UseFont = true;
            this.colTenDauViecLon1.Caption = "Tên đầu việc lớn";
            this.colTenDauViecLon1.FieldName = "TenDauViecLon";
            this.colTenDauViecLon1.MinWidth = 120;
            this.colTenDauViecLon1.Name = "colTenDauViecLon1";
            this.colTenDauViecLon1.Visible = true;
            this.colTenDauViecLon1.VisibleIndex = 12;
            this.colTenDauViecLon1.Width = 120;
            // 
            // colTenDuAn1
            // 
            this.colTenDuAn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTenDuAn1.AppearanceHeader.Options.UseFont = true;
            this.colTenDuAn1.Caption = "Tên dự án";
            this.colTenDuAn1.FieldName = "TenDuAn";
            this.colTenDuAn1.MinWidth = 120;
            this.colTenDuAn1.Name = "colTenDuAn1";
            this.colTenDuAn1.Visible = true;
            this.colTenDuAn1.VisibleIndex = 13;
            this.colTenDuAn1.Width = 120;
            // 
            // colTotalFile1
            // 
            this.colTotalFile1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTotalFile1.AppearanceHeader.Options.UseFont = true;
            this.colTotalFile1.Caption = "Tổng file";
            this.colTotalFile1.FieldName = "TotalFile";
            this.colTotalFile1.MinWidth = 120;
            this.colTotalFile1.Name = "colTotalFile1";
            this.colTotalFile1.Visible = true;
            this.colTotalFile1.VisibleIndex = 14;
            this.colTotalFile1.Width = 120;
            // 
            // colTotalApprove1
            // 
            this.colTotalApprove1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTotalApprove1.AppearanceHeader.Options.UseFont = true;
            this.colTotalApprove1.Caption = "Tổng file đã duyệt";
            this.colTotalApprove1.FieldName = "TotalApprove";
            this.colTotalApprove1.MinWidth = 120;
            this.colTotalApprove1.Name = "colTotalApprove1";
            this.colTotalApprove1.Visible = true;
            this.colTotalApprove1.VisibleIndex = 15;
            this.colTotalApprove1.Width = 120;
            // 
            // colFullNameSend1
            // 
            this.colFullNameSend1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFullNameSend1.AppearanceHeader.Options.UseFont = true;
            this.colFullNameSend1.Caption = "Người gửi duyệt";
            this.colFullNameSend1.FieldName = "FullNameSend";
            this.colFullNameSend1.MinWidth = 120;
            this.colFullNameSend1.Name = "colFullNameSend1";
            this.colFullNameSend1.Visible = true;
            this.colFullNameSend1.VisibleIndex = 16;
            this.colFullNameSend1.Width = 120;
            // 
            // colNgayGuiDuyet1
            // 
            this.colNgayGuiDuyet1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNgayGuiDuyet1.AppearanceHeader.Options.UseFont = true;
            this.colNgayGuiDuyet1.Caption = "Ngày gửi duyệt";
            this.colNgayGuiDuyet1.FieldName = "NgayGuiDuyet";
            this.colNgayGuiDuyet1.MinWidth = 120;
            this.colNgayGuiDuyet1.Name = "colNgayGuiDuyet1";
            this.colNgayGuiDuyet1.Visible = true;
            this.colNgayGuiDuyet1.VisibleIndex = 17;
            this.colNgayGuiDuyet1.Width = 120;
            // 
            // colFullNameApprove1
            // 
            this.colFullNameApprove1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFullNameApprove1.AppearanceHeader.Options.UseFont = true;
            this.colFullNameApprove1.Caption = "Người duyệt";
            this.colFullNameApprove1.FieldName = "FullNameApprove";
            this.colFullNameApprove1.MinWidth = 120;
            this.colFullNameApprove1.Name = "colFullNameApprove1";
            this.colFullNameApprove1.Visible = true;
            this.colFullNameApprove1.VisibleIndex = 18;
            this.colFullNameApprove1.Width = 120;
            // 
            // colNgayDuyet1
            // 
            this.colNgayDuyet1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNgayDuyet1.AppearanceHeader.Options.UseFont = true;
            this.colNgayDuyet1.Caption = "Ngày duyệt";
            this.colNgayDuyet1.FieldName = "NgayDuyet";
            this.colNgayDuyet1.MinWidth = 120;
            this.colNgayDuyet1.Name = "colNgayDuyet1";
            this.colNgayDuyet1.Visible = true;
            this.colNgayDuyet1.VisibleIndex = 19;
            this.colNgayDuyet1.Width = 120;
            // 
            // colGhiChuDuyet1
            // 
            this.colGhiChuDuyet1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGhiChuDuyet1.AppearanceHeader.Options.UseFont = true;
            this.colGhiChuDuyet1.Caption = "Ghi chú duyệt";
            this.colGhiChuDuyet1.FieldName = "GhiChuDuyet";
            this.colGhiChuDuyet1.MinWidth = 120;
            this.colGhiChuDuyet1.Name = "colGhiChuDuyet1";
            this.colGhiChuDuyet1.Visible = true;
            this.colGhiChuDuyet1.VisibleIndex = 20;
            this.colGhiChuDuyet1.Width = 1272;
            // 
            // BusinessTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 497);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.panelControl1);
            this.Name = "BusinessTableView";
            this.Text = "Chi tiết các công tác";
            this.Load += new System.EventHandler(this.BusinessTableView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.giaoViecViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Export;
        private System.Windows.Forms.BindingSource giaoViecViewModelBindingSource;
        private DevExpress.XtraTreeList.TreeList gridControl;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMaDinhMuc1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTenCongViec1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDonVi1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNgayBatDau1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNgayKetThuc1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colKhoiLuongHopDong1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colKhoiLuongKeHoach1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGhiChu1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTrangThai1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTenHangMuc1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTenCongTrinh1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTenDauViecNho1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTenDauViecLon1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTenDuAn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTotalFile1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTotalApprove1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFullNameSend1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNgayGuiDuyet1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFullNameApprove1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNgayDuyet1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGhiChuDuyet1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}