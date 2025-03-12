namespace PhanMemQuanLyThiCong.Controls
{
    partial class Ctrl_CongViecHangNgay
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
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule1 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule2 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule3 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule4 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule5 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleDataBar formatConditionRuleDataBar1 = new DevExpress.XtraEditors.FormatConditionRuleDataBar();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule6 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleDataBar formatConditionRuleDataBar2 = new DevExpress.XtraEditors.FormatConditionRuleDataBar();
            this.col_ChenhLechKhoiLuong = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_Progress = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_CongViecDangThucHien = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTenCongTac = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colNgayBatDau = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNgayKetThuc = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_NgayHienTai = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_KhoiLuongKeHoach = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_KhoiLuongThiCong = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn125 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn126 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_DonGiaKeHoach = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_ThanhTienKeHoach = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_DonGiaThiCong = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_ThanhTienThiCong = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tl_CongViecDangThucHien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            this.SuspendLayout();
            // 
            // col_ChenhLechKhoiLuong
            // 
            this.col_ChenhLechKhoiLuong.Caption = "Thành tiền nhanh chậm";
            this.col_ChenhLechKhoiLuong.FieldName = "Chênh lệch khối lượng";
            this.col_ChenhLechKhoiLuong.Format.FormatString = "n2";
            this.col_ChenhLechKhoiLuong.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col_ChenhLechKhoiLuong.MinWidth = 120;
            this.col_ChenhLechKhoiLuong.Name = "col_ChenhLechKhoiLuong";
            this.col_ChenhLechKhoiLuong.UnboundDataType = typeof(double);
            this.col_ChenhLechKhoiLuong.UnboundExpression = "[KhoiLuongThiCong] - [KhoiLuongKeHoach]";
            this.col_ChenhLechKhoiLuong.Visible = true;
            this.col_ChenhLechKhoiLuong.VisibleIndex = 2;
            this.col_ChenhLechKhoiLuong.Width = 150;
            // 
            // col_Progress
            // 
            this.col_Progress.Caption = "Tiến độ";
            this.col_Progress.FieldName = "ProgressTC_KH";
            this.col_Progress.Format.FormatString = "p";
            this.col_Progress.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col_Progress.MinWidth = 100;
            this.col_Progress.Name = "col_Progress";
            this.col_Progress.Visible = true;
            this.col_Progress.VisibleIndex = 1;
            this.col_Progress.Width = 100;
            // 
            // tl_CongViecDangThucHien
            // 
            this.tl_CongViecDangThucHien.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.colTenCongTac,
            this.colNgayBatDau,
            this.colNgayKetThuc,
            this.col_NgayHienTai,
            this.col_ChenhLechKhoiLuong,
            this.col_KhoiLuongKeHoach,
            this.col_KhoiLuongThiCong,
            this.col_Progress,
            this.treeListColumn125,
            this.treeListColumn126,
            this.treeListColumn2,
            this.treeListColumn3,
            this.col_DonGiaKeHoach,
            this.col_ThanhTienKeHoach,
            this.col_DonGiaThiCong,
            this.col_ThanhTienThiCong});
            this.tl_CongViecDangThucHien.Dock = System.Windows.Forms.DockStyle.Fill;
            treeListFormatRule1.Column = this.col_ChenhLechKhoiLuong;
            treeListFormatRule1.Name = "KhoiLuongCham";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue1.PredefinedName = "Red Bold Text";
            formatConditionRuleValue1.Value1 = "0";
            treeListFormatRule1.Rule = formatConditionRuleValue1;
            treeListFormatRule2.Name = "KhoiLuongNhanh";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.GreaterOrEqual;
            formatConditionRuleValue2.PredefinedName = "Green Bold Text";
            formatConditionRuleValue2.Value1 = "0";
            treeListFormatRule2.Rule = formatConditionRuleValue2;
            treeListFormatRule3.Column = this.col_Progress;
            treeListFormatRule3.Name = "TienDoCham";
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue3.PredefinedName = "Red Bold Text";
            formatConditionRuleValue3.Value1 = "0";
            treeListFormatRule3.Rule = formatConditionRuleValue3;
            treeListFormatRule4.Name = "TienDoNhanh";
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.GreaterOrEqual;
            formatConditionRuleValue4.PredefinedName = "Green Bold Text";
            formatConditionRuleValue4.Value1 = "0";
            treeListFormatRule4.Rule = formatConditionRuleValue4;
            treeListFormatRule5.Column = this.col_Progress;
            treeListFormatRule5.Name = "FormatTienDo";
            formatConditionRuleDataBar1.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            formatConditionRuleDataBar1.MaximumType = DevExpress.XtraEditors.FormatConditionValueType.Percent;
            formatConditionRuleDataBar1.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            formatConditionRuleDataBar1.PredefinedName = "Mint Gradient";
            treeListFormatRule5.Rule = formatConditionRuleDataBar1;
            treeListFormatRule6.Column = this.col_ChenhLechKhoiLuong;
            treeListFormatRule6.Name = "Format0";
            formatConditionRuleDataBar2.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            formatConditionRuleDataBar2.MaximumType = DevExpress.XtraEditors.FormatConditionValueType.Percent;
            formatConditionRuleDataBar2.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            formatConditionRuleDataBar2.PredefinedName = "Mint Gradient";
            treeListFormatRule6.Rule = formatConditionRuleDataBar2;
            this.tl_CongViecDangThucHien.FormatRules.Add(treeListFormatRule1);
            this.tl_CongViecDangThucHien.FormatRules.Add(treeListFormatRule2);
            this.tl_CongViecDangThucHien.FormatRules.Add(treeListFormatRule3);
            this.tl_CongViecDangThucHien.FormatRules.Add(treeListFormatRule4);
            this.tl_CongViecDangThucHien.FormatRules.Add(treeListFormatRule5);
            this.tl_CongViecDangThucHien.FormatRules.Add(treeListFormatRule6);
            this.tl_CongViecDangThucHien.KeyFieldName = "Code";
            this.tl_CongViecDangThucHien.Location = new System.Drawing.Point(0, 0);
            this.tl_CongViecDangThucHien.MinimumSize = new System.Drawing.Size(150, 0);
            this.tl_CongViecDangThucHien.Name = "tl_CongViecDangThucHien";
            this.tl_CongViecDangThucHien.OptionsMenu.ShowConditionalFormattingItem = true;
            this.tl_CongViecDangThucHien.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.tl_CongViecDangThucHien.ParentFieldName = "ParentCode";
            this.tl_CongViecDangThucHien.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit4});
            this.tl_CongViecDangThucHien.Size = new System.Drawing.Size(1292, 462);
            this.tl_CongViecDangThucHien.TabIndex = 12;
            this.tl_CongViecDangThucHien.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tl_CongViecDangThucHien_NodeCellStyle);
            this.tl_CongViecDangThucHien.Load += new System.EventHandler(this.tl_CongViecDangThucHien_Load);
            this.tl_CongViecDangThucHien.DataSourceChanged += new System.EventHandler(this.tl_CongViecDangThucHien_DataSourceChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Mã định mức";
            this.treeListColumn1.FieldName = "MaDinhMuc";
            this.treeListColumn1.MinWidth = 75;
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // colTenCongTac
            // 
            this.colTenCongTac.Caption = "Tên công việc";
            this.colTenCongTac.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colTenCongTac.FieldName = "TenCongTac";
            this.colTenCongTac.MaxWidth = 200;
            this.colTenCongTac.MinWidth = 150;
            this.colTenCongTac.Name = "colTenCongTac";
            this.colTenCongTac.Visible = true;
            this.colTenCongTac.VisibleIndex = 0;
            this.colTenCongTac.Width = 150;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // colNgayBatDau
            // 
            this.colNgayBatDau.Caption = "Ngày bắt đầu";
            this.colNgayBatDau.FieldName = "NgayBatDau";
            this.colNgayBatDau.MinWidth = 75;
            this.colNgayBatDau.Name = "colNgayBatDau";
            this.colNgayBatDau.Visible = true;
            this.colNgayBatDau.VisibleIndex = 9;
            // 
            // colNgayKetThuc
            // 
            this.colNgayKetThuc.Caption = "Ngày kết thúc";
            this.colNgayKetThuc.FieldName = "NgayKetThuc";
            this.colNgayKetThuc.MinWidth = 75;
            this.colNgayKetThuc.Name = "colNgayKetThuc";
            this.colNgayKetThuc.Visible = true;
            this.colNgayKetThuc.VisibleIndex = 10;
            // 
            // col_NgayHienTai
            // 
            this.col_NgayHienTai.Caption = "Ngày hiện tại";
            this.col_NgayHienTai.FieldName = "Ngay";
            this.col_NgayHienTai.MinWidth = 75;
            this.col_NgayHienTai.Name = "col_NgayHienTai";
            this.col_NgayHienTai.Visible = true;
            this.col_NgayHienTai.VisibleIndex = 11;
            // 
            // col_KhoiLuongKeHoach
            // 
            this.col_KhoiLuongKeHoach.Caption = "Khối lượng kế hoạch";
            this.col_KhoiLuongKeHoach.FieldName = "KhoiLuongKeHoach";
            this.col_KhoiLuongKeHoach.Format.FormatString = "n2";
            this.col_KhoiLuongKeHoach.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col_KhoiLuongKeHoach.MinWidth = 120;
            this.col_KhoiLuongKeHoach.Name = "col_KhoiLuongKeHoach";
            this.col_KhoiLuongKeHoach.Visible = true;
            this.col_KhoiLuongKeHoach.VisibleIndex = 6;
            this.col_KhoiLuongKeHoach.Width = 150;
            // 
            // col_KhoiLuongThiCong
            // 
            this.col_KhoiLuongThiCong.Caption = "Khối lượng thi công";
            this.col_KhoiLuongThiCong.FieldName = "KhoiLuongThiCong";
            this.col_KhoiLuongThiCong.Format.FormatString = "n2";
            this.col_KhoiLuongThiCong.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col_KhoiLuongThiCong.MinWidth = 120;
            this.col_KhoiLuongThiCong.Name = "col_KhoiLuongThiCong";
            this.col_KhoiLuongThiCong.Visible = true;
            this.col_KhoiLuongThiCong.VisibleIndex = 3;
            this.col_KhoiLuongThiCong.Width = 150;
            // 
            // treeListColumn125
            // 
            this.treeListColumn125.Caption = "Code";
            this.treeListColumn125.FieldName = "Code";
            this.treeListColumn125.MinWidth = 75;
            this.treeListColumn125.Name = "treeListColumn125";
            // 
            // treeListColumn126
            // 
            this.treeListColumn126.Caption = "ParentCode";
            this.treeListColumn126.FieldName = "ParentCode";
            this.treeListColumn126.MinWidth = 75;
            this.treeListColumn126.Name = "treeListColumn126";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn2.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.treeListColumn2.AppearanceCell.Options.UseFont = true;
            this.treeListColumn2.AppearanceCell.Options.UseForeColor = true;
            this.treeListColumn2.Caption = "Đơn vị thực hiện";
            this.treeListColumn2.FieldName = "TenDonViThucHien";
            this.treeListColumn2.MinWidth = 75;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 12;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn3.AppearanceCell.ForeColor = System.Drawing.Color.Lime;
            this.treeListColumn3.AppearanceCell.Options.UseFont = true;
            this.treeListColumn3.AppearanceCell.Options.UseForeColor = true;
            this.treeListColumn3.Caption = "Dự án";
            this.treeListColumn3.FieldName = "TenDuAn";
            this.treeListColumn3.MinWidth = 75;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 13;
            // 
            // col_DonGiaKeHoach
            // 
            this.col_DonGiaKeHoach.Caption = "Đơn giá kế hoạch";
            this.col_DonGiaKeHoach.FieldName = "DonGiaKeHoach";
            this.col_DonGiaKeHoach.Format.FormatString = "n0";
            this.col_DonGiaKeHoach.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col_DonGiaKeHoach.MinWidth = 120;
            this.col_DonGiaKeHoach.Name = "col_DonGiaKeHoach";
            this.col_DonGiaKeHoach.Visible = true;
            this.col_DonGiaKeHoach.VisibleIndex = 7;
            this.col_DonGiaKeHoach.Width = 150;
            // 
            // col_ThanhTienKeHoach
            // 
            this.col_ThanhTienKeHoach.Caption = "Thành Tiền Kế Hoạch";
            this.col_ThanhTienKeHoach.FieldName = "ThanhTienKeHoach";
            this.col_ThanhTienKeHoach.Format.FormatString = "n0";
            this.col_ThanhTienKeHoach.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col_ThanhTienKeHoach.MinWidth = 120;
            this.col_ThanhTienKeHoach.Name = "col_ThanhTienKeHoach";
            this.col_ThanhTienKeHoach.Visible = true;
            this.col_ThanhTienKeHoach.VisibleIndex = 8;
            this.col_ThanhTienKeHoach.Width = 558;
            // 
            // col_DonGiaThiCong
            // 
            this.col_DonGiaThiCong.Caption = "Đơn giá thi công";
            this.col_DonGiaThiCong.FieldName = "DonGiaThiCong";
            this.col_DonGiaThiCong.Format.FormatString = "n0";
            this.col_DonGiaThiCong.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col_DonGiaThiCong.MinWidth = 120;
            this.col_DonGiaThiCong.Name = "col_DonGiaThiCong";
            this.col_DonGiaThiCong.Visible = true;
            this.col_DonGiaThiCong.VisibleIndex = 4;
            this.col_DonGiaThiCong.Width = 150;
            // 
            // col_ThanhTienThiCong
            // 
            this.col_ThanhTienThiCong.Caption = "Thành tiền thi công";
            this.col_ThanhTienThiCong.FieldName = "ThanhTienThiCong";
            this.col_ThanhTienThiCong.Format.FormatString = "n0";
            this.col_ThanhTienThiCong.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col_ThanhTienThiCong.MinWidth = 120;
            this.col_ThanhTienThiCong.Name = "col_ThanhTienThiCong";
            this.col_ThanhTienThiCong.Visible = true;
            this.col_ThanhTienThiCong.VisibleIndex = 5;
            this.col_ThanhTienThiCong.Width = 150;
            // 
            // Ctrl_CongViecHangNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tl_CongViecDangThucHien);
            this.Name = "Ctrl_CongViecHangNgay";
            this.Size = new System.Drawing.Size(1292, 462);
            this.Load += new System.EventHandler(this.Ctrl_CongViecHangNgay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tl_CongViecDangThucHien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tl_CongViecDangThucHien;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTenCongTac;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNgayBatDau;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNgayKetThuc;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_NgayHienTai;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_ChenhLechKhoiLuong;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_KhoiLuongKeHoach;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_KhoiLuongThiCong;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_Progress;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn125;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn126;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_DonGiaKeHoach;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_ThanhTienKeHoach;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_DonGiaThiCong;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_ThanhTienThiCong;
    }
}
