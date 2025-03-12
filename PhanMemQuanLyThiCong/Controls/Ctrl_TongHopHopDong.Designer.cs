
namespace PhanMemQuanLyThiCong.Controls
{
    partial class Ctrl_TongHopHopDong
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
            this.Tl_TongHopDong = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn160 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn161 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn162 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.treeListColumn163 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn164 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumn165 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn166 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rIDE_Ngay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.treeListColumn167 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn168 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn169 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn170 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn171 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn172 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn173 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn174 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn175 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rihp_File = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.treeListColumn176 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn177 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Tl_TongHopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_Ngay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_Ngay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rihp_File)).BeginInit();
            this.SuspendLayout();
            // 
            // Tl_TongHopDong
            // 
            this.Tl_TongHopDong.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn160,
            this.treeListColumn161,
            this.treeListColumn162,
            this.treeListColumn163,
            this.treeListColumn164,
            this.treeListColumn165,
            this.treeListColumn166,
            this.treeListColumn167,
            this.treeListColumn168,
            this.treeListColumn169,
            this.treeListColumn170,
            this.treeListColumn171,
            this.treeListColumn172,
            this.treeListColumn173,
            this.treeListColumn174,
            this.treeListColumn175,
            this.treeListColumn176,
            this.treeListColumn177,
            this.treeListColumn1,
            this.treeListColumn2});
            this.Tl_TongHopDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tl_TongHopDong.KeyFieldName = "Code";
            this.Tl_TongHopDong.Location = new System.Drawing.Point(0, 0);
            this.Tl_TongHopDong.Name = "Tl_TongHopDong";
            this.Tl_TongHopDong.OptionsBehavior.PopulateServiceColumns = true;
            this.Tl_TongHopDong.OptionsView.AutoWidth = false;
            this.Tl_TongHopDong.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.Tl_TongHopDong.ParentFieldName = "ParentCode";
            this.Tl_TongHopDong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rihp_File,
            this.rIDE_Ngay,
            this.repositoryItemMemoEdit1,
            this.repositoryItemComboBox1});
            this.Tl_TongHopDong.Size = new System.Drawing.Size(1130, 525);
            this.Tl_TongHopDong.TabIndex = 18;
            this.Tl_TongHopDong.GetCustomSummaryValue += new DevExpress.XtraTreeList.GetCustomSummaryValueEventHandler(this.Tl_TongHopDong_GetCustomSummaryValue);
            this.Tl_TongHopDong.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.Tl_TongHopDong_NodeCellStyle);
            this.Tl_TongHopDong.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.Tl_TongHopDong_CustomDrawNodeCell);
            this.Tl_TongHopDong.CellValueChanging += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.Tl_TongHopDong_CellValueChanging);
            this.Tl_TongHopDong.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.Tl_TongHopDong_CellValueChanged);
            this.Tl_TongHopDong.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.Tl_TongHopDong_ShowingEditor);
            // 
            // treeListColumn160
            // 
            this.treeListColumn160.Caption = "Code";
            this.treeListColumn160.FieldName = "Code";
            this.treeListColumn160.Name = "treeListColumn160";
            this.treeListColumn160.OptionsColumn.AllowSort = true;
            this.treeListColumn160.Width = 67;
            // 
            // treeListColumn161
            // 
            this.treeListColumn161.Caption = "CodeHopDong";
            this.treeListColumn161.FieldName = "CodeHopDong";
            this.treeListColumn161.Name = "treeListColumn161";
            this.treeListColumn161.OptionsColumn.AllowSort = true;
            this.treeListColumn161.Width = 67;
            // 
            // treeListColumn162
            // 
            this.treeListColumn162.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.treeListColumn162.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn162.Caption = "Trạng thái";
            this.treeListColumn162.ColumnEdit = this.repositoryItemComboBox1;
            this.treeListColumn162.FieldName = "TrangThai";
            this.treeListColumn162.Name = "treeListColumn162";
            this.treeListColumn162.OptionsColumn.AllowSort = true;
            this.treeListColumn162.Visible = true;
            this.treeListColumn162.VisibleIndex = 3;
            this.treeListColumn162.Width = 90;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "Hoàn thành",
            "Đang thực hiện",
            "Sửa tiền"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // treeListColumn163
            // 
            this.treeListColumn163.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.treeListColumn163.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn163.Caption = "Số hợp đồng";
            this.treeListColumn163.FieldName = "SoHopDong";
            this.treeListColumn163.Name = "treeListColumn163";
            this.treeListColumn163.OptionsColumn.AllowSort = true;
            this.treeListColumn163.OptionsColumn.ReadOnly = true;
            this.treeListColumn163.Visible = true;
            this.treeListColumn163.VisibleIndex = 1;
            this.treeListColumn163.Width = 118;
            // 
            // treeListColumn164
            // 
            this.treeListColumn164.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.treeListColumn164.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn164.Caption = "Tên hợp đồng";
            this.treeListColumn164.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn164.FieldName = "TenHopDong";
            this.treeListColumn164.Name = "treeListColumn164";
            this.treeListColumn164.OptionsColumn.AllowSort = true;
            this.treeListColumn164.OptionsColumn.ReadOnly = true;
            this.treeListColumn164.Visible = true;
            this.treeListColumn164.VisibleIndex = 0;
            this.treeListColumn164.Width = 233;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // treeListColumn165
            // 
            this.treeListColumn165.AllNodesSummary = true;
            this.treeListColumn165.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.treeListColumn165.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn165.Caption = "Giá trị hợp đồng";
            this.treeListColumn165.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn165.FieldName = "GiaTriHopDong";
            this.treeListColumn165.Format.FormatString = "n0";
            this.treeListColumn165.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn165.Name = "treeListColumn165";
            this.treeListColumn165.OptionsColumn.AllowSort = true;
            this.treeListColumn165.OptionsColumn.ReadOnly = true;
            this.treeListColumn165.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.treeListColumn165.Visible = true;
            this.treeListColumn165.VisibleIndex = 2;
            this.treeListColumn165.Width = 104;
            // 
            // treeListColumn166
            // 
            this.treeListColumn166.Caption = "Ngày bắt đầu";
            this.treeListColumn166.ColumnEdit = this.rIDE_Ngay;
            this.treeListColumn166.FieldName = "NgayBatDau";
            this.treeListColumn166.Name = "treeListColumn166";
            this.treeListColumn166.OptionsColumn.AllowSort = true;
            this.treeListColumn166.Visible = true;
            this.treeListColumn166.VisibleIndex = 10;
            this.treeListColumn166.Width = 93;
            // 
            // rIDE_Ngay
            // 
            this.rIDE_Ngay.AutoHeight = false;
            this.rIDE_Ngay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rIDE_Ngay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rIDE_Ngay.DisplayFormat.FormatString = "";
            this.rIDE_Ngay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.rIDE_Ngay.EditFormat.FormatString = "dd/MM/yyyy";
            this.rIDE_Ngay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.rIDE_Ngay.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.rIDE_Ngay.Name = "rIDE_Ngay";
            this.rIDE_Ngay.UseMaskAsDisplayFormat = true;
            // 
            // treeListColumn167
            // 
            this.treeListColumn167.Caption = "Ngày kết thúc";
            this.treeListColumn167.ColumnEdit = this.rIDE_Ngay;
            this.treeListColumn167.FieldName = "NgayKetThuc";
            this.treeListColumn167.Name = "treeListColumn167";
            this.treeListColumn167.OptionsColumn.AllowSort = true;
            this.treeListColumn167.Visible = true;
            this.treeListColumn167.VisibleIndex = 11;
            this.treeListColumn167.Width = 121;
            // 
            // treeListColumn168
            // 
            this.treeListColumn168.Caption = "Số tiền đã tạm ứng";
            this.treeListColumn168.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn168.FieldName = "SoTienDaTamUng";
            this.treeListColumn168.Format.FormatString = "n0";
            this.treeListColumn168.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn168.Name = "treeListColumn168";
            this.treeListColumn168.OptionsColumn.AllowSort = true;
            this.treeListColumn168.Visible = true;
            this.treeListColumn168.VisibleIndex = 6;
            this.treeListColumn168.Width = 92;
            // 
            // treeListColumn169
            // 
            this.treeListColumn169.Caption = "Số tiền thanh toán";
            this.treeListColumn169.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn169.FieldName = "SoTienThanhToan";
            this.treeListColumn169.Format.FormatString = "n0";
            this.treeListColumn169.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn169.Name = "treeListColumn169";
            this.treeListColumn169.OptionsColumn.AllowSort = true;
            this.treeListColumn169.Visible = true;
            this.treeListColumn169.VisibleIndex = 7;
            this.treeListColumn169.Width = 95;
            // 
            // treeListColumn170
            // 
            this.treeListColumn170.Caption = "Số tiền còn lại";
            this.treeListColumn170.FieldName = "SoTienConLai";
            this.treeListColumn170.Format.FormatString = "n0";
            this.treeListColumn170.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn170.Name = "treeListColumn170";
            this.treeListColumn170.OptionsColumn.AllowSort = true;
            this.treeListColumn170.OptionsColumn.ReadOnly = true;
            this.treeListColumn170.Visible = true;
            this.treeListColumn170.VisibleIndex = 8;
            this.treeListColumn170.Width = 82;
            // 
            // treeListColumn171
            // 
            this.treeListColumn171.Caption = "Tiền bảo lãnh";
            this.treeListColumn171.FieldName = "TienBaoLanh";
            this.treeListColumn171.Format.FormatString = "n0";
            this.treeListColumn171.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn171.Name = "treeListColumn171";
            this.treeListColumn171.OptionsColumn.AllowSort = true;
            this.treeListColumn171.OptionsColumn.ReadOnly = true;
            this.treeListColumn171.Visible = true;
            this.treeListColumn171.VisibleIndex = 12;
            this.treeListColumn171.Width = 105;
            // 
            // treeListColumn172
            // 
            this.treeListColumn172.Caption = "Thời gian bảo lãnh";
            this.treeListColumn172.ColumnEdit = this.rIDE_Ngay;
            this.treeListColumn172.FieldName = "ThoiGianBaoLanh";
            this.treeListColumn172.Name = "treeListColumn172";
            this.treeListColumn172.OptionsColumn.AllowSort = true;
            this.treeListColumn172.OptionsColumn.ReadOnly = true;
            this.treeListColumn172.Visible = true;
            this.treeListColumn172.VisibleIndex = 13;
            this.treeListColumn172.Width = 112;
            // 
            // treeListColumn173
            // 
            this.treeListColumn173.Caption = "Tiền bảo hành";
            this.treeListColumn173.FieldName = "TienBaoHanh";
            this.treeListColumn173.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn173.Name = "treeListColumn173";
            this.treeListColumn173.OptionsColumn.AllowSort = true;
            this.treeListColumn173.OptionsColumn.ReadOnly = true;
            this.treeListColumn173.Visible = true;
            this.treeListColumn173.VisibleIndex = 14;
            this.treeListColumn173.Width = 84;
            // 
            // treeListColumn174
            // 
            this.treeListColumn174.Caption = "Thời gian bảo hành";
            this.treeListColumn174.ColumnEdit = this.rIDE_Ngay;
            this.treeListColumn174.FieldName = "ThoiGianBaoHanh";
            this.treeListColumn174.Name = "treeListColumn174";
            this.treeListColumn174.OptionsColumn.AllowSort = true;
            this.treeListColumn174.OptionsColumn.ReadOnly = true;
            this.treeListColumn174.Visible = true;
            this.treeListColumn174.VisibleIndex = 15;
            this.treeListColumn174.Width = 101;
            // 
            // treeListColumn175
            // 
            this.treeListColumn175.Caption = "Thêm File";
            this.treeListColumn175.ColumnEdit = this.rihp_File;
            this.treeListColumn175.FieldName = "ThemFile";
            this.treeListColumn175.Name = "treeListColumn175";
            this.treeListColumn175.OptionsColumn.AllowSort = true;
            this.treeListColumn175.UnboundDataType = typeof(string);
            this.treeListColumn175.UnboundExpression = "\'Thêm File\'";
            this.treeListColumn175.Visible = true;
            this.treeListColumn175.VisibleIndex = 16;
            this.treeListColumn175.Width = 109;
            // 
            // rihp_File
            // 
            this.rihp_File.AutoHeight = false;
            this.rihp_File.Image = global::PhanMemQuanLyThiCong.Properties.Resources.delete_hyperlink_16x16;
            this.rihp_File.Name = "rihp_File";
            this.rihp_File.Click += new System.EventHandler(this.rihp_File_Click);
            // 
            // treeListColumn176
            // 
            this.treeListColumn176.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.treeListColumn176.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn176.Caption = "Loại hợp đồng";
            this.treeListColumn176.FieldName = "LoaiHopDong";
            this.treeListColumn176.Name = "treeListColumn176";
            this.treeListColumn176.OptionsColumn.AllowSort = true;
            this.treeListColumn176.OptionsColumn.ReadOnly = true;
            this.treeListColumn176.Visible = true;
            this.treeListColumn176.VisibleIndex = 4;
            this.treeListColumn176.Width = 88;
            // 
            // treeListColumn177
            // 
            this.treeListColumn177.Caption = "STT";
            this.treeListColumn177.FieldName = "STT";
            this.treeListColumn177.Name = "treeListColumn177";
            this.treeListColumn177.OptionsColumn.AllowSort = true;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Sản lượng thi công";
            this.treeListColumn1.FieldName = "SanLuongThiCong";
            this.treeListColumn1.Format.FormatString = "n0";
            this.treeListColumn1.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowSort = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 5;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Số tiền thực nhận";
            this.treeListColumn2.FieldName = "SoTienThucNhan";
            this.treeListColumn2.Format.FormatString = "n0";
            this.treeListColumn2.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowSort = true;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 9;
            // 
            // Ctrl_TongHopHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Tl_TongHopDong);
            this.Name = "Ctrl_TongHopHopDong";
            this.Size = new System.Drawing.Size(1130, 525);
            ((System.ComponentModel.ISupportInitialize)(this.Tl_TongHopDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_Ngay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_Ngay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rihp_File)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList Tl_TongHopDong;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn160;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn161;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn162;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn163;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn164;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn165;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn166;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rIDE_Ngay;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn167;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn168;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn169;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn170;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn171;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn172;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn173;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn174;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn175;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rihp_File;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn176;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn177;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
    }
}
