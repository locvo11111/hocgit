
namespace PhanMemQuanLyThiCong.Controls
{
    partial class Ctrl_ThuChiTamUng_KhoanThu
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
            this.components = new System.ComponentModel.Container();
            this.tL_KhoanThu = new DevExpress.XtraTreeList.TreeList();
            this.ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rILUE_TenNguoi = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rIDE_NgayThu = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rILUE_ToChucCaNhan = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn11 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rICBE_TrangThai = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.svgImageCollection = new DevExpress.Utils.SvgImageCollection(this.components);
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rICE_CheckNgay = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.treeListColumn12 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rHpLE_File = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tL_KhoanThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rILUE_TenNguoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_NgayThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_NgayThu.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rILUE_ToChucCaNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rICBE_TrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rICE_CheckNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rHpLE_File)).BeginInit();
            this.SuspendLayout();
            // 
            // tL_KhoanThu
            // 
            this.tL_KhoanThu.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.ID,
            this.ParentID,
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn5,
            this.treeListColumn6,
            this.treeListColumn7,
            this.treeListColumn9,
            this.treeListColumn10,
            this.treeListColumn11,
            this.treeListColumn8,
            this.treeListColumn12,
            this.treeListColumn4});
            this.tL_KhoanThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tL_KhoanThu.Location = new System.Drawing.Point(0, 0);
            this.tL_KhoanThu.Name = "tL_KhoanThu";
            this.tL_KhoanThu.OptionsPrint.AutoWidth = false;
            this.tL_KhoanThu.OptionsPrint.UsePrintStyles = false;
            this.tL_KhoanThu.OptionsView.AutoWidth = false;
            this.tL_KhoanThu.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.tL_KhoanThu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rILUE_TenNguoi,
            this.rILUE_ToChucCaNhan,
            this.rIDE_NgayThu,
            this.rHpLE_File,
            this.rICBE_TrangThai,
            this.rICE_CheckNgay});
            this.tL_KhoanThu.Size = new System.Drawing.Size(1261, 646);
            this.tL_KhoanThu.TabIndex = 0;
            this.tL_KhoanThu.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.tL_KhoanThu_CustomNodeCellEdit);
            this.tL_KhoanThu.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tL_KhoanThu_NodeCellStyle);
            this.tL_KhoanThu.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.tL_KhoanThu_BeforeFocusNode);
            this.tL_KhoanThu.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.tL_KhoanThu_CustomDrawNodeCell);
            this.tL_KhoanThu.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.tL_KhoanThu_CellValueChanged);
            this.tL_KhoanThu.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.tL_KhoanThu_ShowingEditor);
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            // 
            // ParentID
            // 
            this.ParentID.Caption = "ParentID";
            this.ParentID.FieldName = "ParentID";
            this.ParentID.Name = "ParentID";
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Nội dung thu";
            this.treeListColumn1.FieldName = "NoiDungThu";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 2;
            this.treeListColumn1.Width = 212;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Theo thực hiện";
            this.treeListColumn2.FieldName = "TheoThucHien";
            this.treeListColumn2.Format.FormatString = "n0";
            this.treeListColumn2.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 3;
            this.treeListColumn2.Width = 88;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Thực tế thu";
            this.treeListColumn3.FieldName = "ThucTeThu";
            this.treeListColumn3.Format.FormatString = "n0";
            this.treeListColumn3.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 4;
            this.treeListColumn3.Width = 69;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Người giao";
            this.treeListColumn5.ColumnEdit = this.rILUE_TenNguoi;
            this.treeListColumn5.FieldName = "NguoiGiao";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 5;
            this.treeListColumn5.Width = 158;
            // 
            // rILUE_TenNguoi
            // 
            this.rILUE_TenNguoi.AutoHeight = false;
            this.rILUE_TenNguoi.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.rILUE_TenNguoi.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rILUE_TenNguoi.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Code", "Code", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Decription", "Mã nhân viên"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên nhân viên")});
            this.rILUE_TenNguoi.DisplayMember = "TenGhep";
            this.rILUE_TenNguoi.Name = "rILUE_TenNguoi";
            this.rILUE_TenNguoi.NullText = "";
            this.rILUE_TenNguoi.ValueMember = "Code";
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "Người nhận";
            this.treeListColumn6.ColumnEdit = this.rILUE_TenNguoi;
            this.treeListColumn6.FieldName = "NguoiNhan";
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.Visible = true;
            this.treeListColumn6.VisibleIndex = 6;
            this.treeListColumn6.Width = 167;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Ngày thu";
            this.treeListColumn7.ColumnEdit = this.rIDE_NgayThu;
            this.treeListColumn7.FieldName = "NgayThangThucHien";
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 7;
            this.treeListColumn7.Width = 111;
            // 
            // rIDE_NgayThu
            // 
            this.rIDE_NgayThu.AutoHeight = false;
            this.rIDE_NgayThu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rIDE_NgayThu.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rIDE_NgayThu.MaskSettings.Set("mask", "d");
            this.rIDE_NgayThu.Name = "rIDE_NgayThu";
            // 
            // treeListColumn9
            // 
            this.treeListColumn9.Caption = "Tên cá nhân/Tổ chức";
            this.treeListColumn9.ColumnEdit = this.rILUE_ToChucCaNhan;
            this.treeListColumn9.FieldName = "ToChucCaNhanNhanChiPhiTamUng";
            this.treeListColumn9.Name = "treeListColumn9";
            this.treeListColumn9.Visible = true;
            this.treeListColumn9.VisibleIndex = 8;
            this.treeListColumn9.Width = 142;
            // 
            // rILUE_ToChucCaNhan
            // 
            this.rILUE_ToChucCaNhan.AutoHeight = false;
            this.rILUE_ToChucCaNhan.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.rILUE_ToChucCaNhan.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rILUE_ToChucCaNhan.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Code", "Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Decription", "Vai trò"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Detail", "Tổ chức/Cá nhân"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên")});
            this.rILUE_ToChucCaNhan.DisplayMember = "TenGhep";
            this.rILUE_ToChucCaNhan.Name = "rILUE_ToChucCaNhan";
            this.rILUE_ToChucCaNhan.NullText = "";
            this.rILUE_ToChucCaNhan.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rILUE_ToChucCaNhan.ValueMember = "Code";
            // 
            // treeListColumn10
            // 
            this.treeListColumn10.Caption = "STT";
            this.treeListColumn10.FieldName = "STT";
            this.treeListColumn10.Name = "treeListColumn10";
            this.treeListColumn10.Visible = true;
            this.treeListColumn10.VisibleIndex = 0;
            this.treeListColumn10.Width = 48;
            // 
            // treeListColumn11
            // 
            this.treeListColumn11.Caption = "Nguồn phát sinh";
            this.treeListColumn11.ColumnEdit = this.rICBE_TrangThai;
            this.treeListColumn11.FieldName = "TrangThai";
            this.treeListColumn11.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.fullstackedbar_16x16;
            this.treeListColumn11.Name = "treeListColumn11";
            this.treeListColumn11.Visible = true;
            this.treeListColumn11.VisibleIndex = 1;
            this.treeListColumn11.Width = 118;
            // 
            // rICBE_TrangThai
            // 
            this.rICBE_TrangThai.AutoHeight = false;
            this.rICBE_TrangThai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rICBE_TrangThai.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Chưa gửi duyệt", 1D, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Đề xuất", 2D, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Người dùng", 3D, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Đang chờ duyệt", 5, 3)});
            this.rICBE_TrangThai.Name = "rICBE_TrangThai";
            this.rICBE_TrangThai.SmallImages = this.svgImageCollection;
            // 
            // svgImageCollection
            // 
            this.svgImageCollection.Add("actions_deletecircled", "image://svgimages/icon builder/actions_deletecircled.svg");
            this.svgImageCollection.Add("actions_checkcircled", "image://svgimages/icon builder/actions_checkcircled.svg");
            this.svgImageCollection.Add("bo_customer", "image://svgimages/business objects/bo_customer.svg");
            this.svgImageCollection.Add("bo_audit_changehistory", "image://svgimages/business objects/bo_audit_changehistory.svg");
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.Caption = "Xác nhận đã thu";
            this.treeListColumn8.ColumnEdit = this.rICE_CheckNgay;
            this.treeListColumn8.FieldName = "CheckDaThu";
            this.treeListColumn8.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.apply_16x166;
            this.treeListColumn8.Name = "treeListColumn8";
            this.treeListColumn8.Visible = true;
            this.treeListColumn8.VisibleIndex = 9;
            this.treeListColumn8.Width = 113;
            // 
            // rICE_CheckNgay
            // 
            this.rICE_CheckNgay.AutoHeight = false;
            this.rICE_CheckNgay.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Custom;
            this.rICE_CheckNgay.ImageOptions.ImageChecked = global::PhanMemQuanLyThiCong.Properties.Resources.apply_16x164;
            this.rICE_CheckNgay.ImageOptions.ImageUnchecked = global::PhanMemQuanLyThiCong.Properties.Resources.apply_16x165;
            this.rICE_CheckNgay.Name = "rICE_CheckNgay";
            this.rICE_CheckNgay.CheckedChanged += new System.EventHandler(this.rICE_CheckNgay_CheckedChanged);
            // 
            // treeListColumn12
            // 
            this.treeListColumn12.Caption = "Ghi Chú";
            this.treeListColumn12.FieldName = "GhiChu";
            this.treeListColumn12.Name = "treeListColumn12";
            this.treeListColumn12.Visible = true;
            this.treeListColumn12.VisibleIndex = 10;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "File đính kèm";
            this.treeListColumn4.ColumnEdit = this.rHpLE_File;
            this.treeListColumn4.FieldName = "FileDinhKem";
            this.treeListColumn4.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.projectfile_16x161;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.UnboundDataType = typeof(string);
            this.treeListColumn4.UnboundExpression = "\'Xem File\'";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 11;
            this.treeListColumn4.Width = 92;
            // 
            // rHpLE_File
            // 
            this.rHpLE_File.AutoHeight = false;
            this.rHpLE_File.Image = global::PhanMemQuanLyThiCong.Properties.Resources.hyperlink_16x161;
            this.rHpLE_File.Name = "rHpLE_File";
            this.rHpLE_File.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rHpLE_File.Click += new System.EventHandler(this.rHpLE_File_Click);
            // 
            // Ctrl_ThuChiTamUng_KhoanThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tL_KhoanThu);
            this.Name = "Ctrl_ThuChiTamUng_KhoanThu";
            this.Size = new System.Drawing.Size(1261, 646);
            ((System.ComponentModel.ISupportInitialize)(this.tL_KhoanThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rILUE_TenNguoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_NgayThu.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rIDE_NgayThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rILUE_ToChucCaNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rICBE_TrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rICE_CheckNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rHpLE_File)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tL_KhoanThu;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ParentID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rILUE_TenNguoi;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rILUE_ToChucCaNhan;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rIDE_NgayThu;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rHpLE_File;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rICBE_TrangThai;
        private DevExpress.Utils.SvgImageCollection svgImageCollection;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rICE_CheckNgay;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn12;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
    }
}
