
namespace PhanMemQuanLyThiCong.Controls.MTC
{
    partial class XtraFormXuatNhatTrinhMay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormXuatNhatTrinhMay));
            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.slue_TenMay = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sb_XuatBaoCao = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.SheetNhatTrinh = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ce_XuatTheoNgay = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.de_NgayKetThuc = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.de_NgayBatDau = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slue_TenMay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_XuatTheoNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayKetThuc.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayKetThuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayBatDau.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayBatDau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.de_NgayBatDau);
            this.layoutControl1.Controls.Add(this.de_NgayKetThuc);
            this.layoutControl1.Controls.Add(this.ce_XuatTheoNgay);
            this.layoutControl1.Controls.Add(this.SheetNhatTrinh);
            this.layoutControl1.Controls.Add(this.sb_XuatBaoCao);
            this.layoutControl1.Controls.Add(this.slue_TenMay);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1463, 585);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1463, 585);
            this.Root.TextVisible = false;
            // 
            // slue_TenMay
            // 
            this.slue_TenMay.EditValue = "";
            this.slue_TenMay.Location = new System.Drawing.Point(115, 21);
            this.slue_TenMay.Name = "slue_TenMay";
            this.slue_TenMay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.slue_TenMay.Properties.Appearance.Options.UseFont = true;
            this.slue_TenMay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slue_TenMay.Properties.DisplayMember = "Ten";
            this.slue_TenMay.Properties.PopupView = this.searchLookUpEdit1View;
            this.slue_TenMay.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1});
            this.slue_TenMay.Properties.ValueMember = "Code";
            this.slue_TenMay.Size = new System.Drawing.Size(439, 26);
            this.slue_TenMay.StyleController = this.layoutControl1;
            this.slue_TenMay.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem1.Control = this.slue_TenMay;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(157, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(546, 43);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Tên máy:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(91, 17);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // sb_XuatBaoCao
            // 
            this.sb_XuatBaoCao.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sb_XuatBaoCao.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_XuatBaoCao.Appearance.Options.UseBackColor = true;
            this.sb_XuatBaoCao.Appearance.Options.UseFont = true;
            this.sb_XuatBaoCao.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.sb_XuatBaoCao.Location = new System.Drawing.Point(1226, 12);
            this.sb_XuatBaoCao.Name = "sb_XuatBaoCao";
            this.sb_XuatBaoCao.Size = new System.Drawing.Size(225, 39);
            this.sb_XuatBaoCao.StyleController = this.layoutControl1;
            this.sb_XuatBaoCao.TabIndex = 5;
            this.sb_XuatBaoCao.Text = "Xuất báo cáo";
            this.sb_XuatBaoCao.Click += new System.EventHandler(this.sb_XuatBaoCao_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_XuatBaoCao;
            this.layoutControlItem2.Location = new System.Drawing.Point(1214, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(109, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(229, 43);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // SheetNhatTrinh
            // 
            this.SheetNhatTrinh.Location = new System.Drawing.Point(12, 55);
            this.SheetNhatTrinh.Name = "SheetNhatTrinh";
            this.SheetNhatTrinh.Size = new System.Drawing.Size(1439, 518);
            this.SheetNhatTrinh.TabIndex = 6;
            this.SheetNhatTrinh.Text = "spreadsheetControl1";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.SheetNhatTrinh;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 43);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1443, 522);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // ce_XuatTheoNgay
            // 
            this.ce_XuatTheoNgay.Location = new System.Drawing.Point(558, 20);
            this.ce_XuatTheoNgay.Name = "ce_XuatTheoNgay";
            this.ce_XuatTheoNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.ce_XuatTheoNgay.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.ce_XuatTheoNgay.Properties.Appearance.Options.UseFont = true;
            this.ce_XuatTheoNgay.Properties.Appearance.Options.UseForeColor = true;
            this.ce_XuatTheoNgay.Properties.Caption = "Xuất theo ngày";
            this.ce_XuatTheoNgay.Size = new System.Drawing.Size(148, 23);
            this.ce_XuatTheoNgay.StyleController = this.layoutControl1;
            this.ce_XuatTheoNgay.TabIndex = 7;
            this.ce_XuatTheoNgay.CheckedChanged += new System.EventHandler(this.ce_XuatTheoNgay_CheckedChanged);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem4.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem4.Control = this.ce_XuatTheoNgay;
            this.layoutControlItem4.Location = new System.Drawing.Point(546, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(118, 27);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(152, 43);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // de_NgayKetThuc
            // 
            this.de_NgayKetThuc.EditValue = null;
            this.de_NgayKetThuc.Enabled = false;
            this.de_NgayKetThuc.Location = new System.Drawing.Point(1081, 21);
            this.de_NgayKetThuc.Name = "de_NgayKetThuc";
            this.de_NgayKetThuc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.de_NgayKetThuc.Properties.Appearance.Options.UseFont = true;
            this.de_NgayKetThuc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_NgayKetThuc.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_NgayKetThuc.Size = new System.Drawing.Size(141, 26);
            this.de_NgayKetThuc.StyleController = this.layoutControl1;
            this.de_NgayKetThuc.TabIndex = 8;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem5.Control = this.de_NgayKetThuc;
            this.layoutControlItem5.Location = new System.Drawing.Point(966, 0);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(157, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(248, 43);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Ngày kết thúc";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(91, 17);
            // 
            // de_NgayBatDau
            // 
            this.de_NgayBatDau.EditValue = null;
            this.de_NgayBatDau.Enabled = false;
            this.de_NgayBatDau.Location = new System.Drawing.Point(813, 21);
            this.de_NgayBatDau.Name = "de_NgayBatDau";
            this.de_NgayBatDau.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.de_NgayBatDau.Properties.Appearance.Options.UseFont = true;
            this.de_NgayBatDau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_NgayBatDau.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_NgayBatDau.Size = new System.Drawing.Size(161, 26);
            this.de_NgayBatDau.StyleController = this.layoutControl1;
            this.de_NgayBatDau.TabIndex = 9;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem6.Control = this.de_NgayBatDau;
            this.layoutControlItem6.Location = new System.Drawing.Point(698, 0);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(157, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(268, 43);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "Ngày bắt đầu";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(91, 17);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Code";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên máy";
            this.gridColumn2.FieldName = "Ten";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã máy";
            this.gridColumn3.FieldName = "No";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ngày mua máy";
            repositoryItemDateEdit2.AutoHeight = false;
            repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemDateEdit2.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.gridColumn4.ColumnEdit = repositoryItemDateEdit2;
            this.gridColumn4.FieldName = "NgayMuaMay";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // XtraFormXuatNhatTrinhMay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1463, 585);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraFormXuatNhatTrinhMay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xuất báo cáo nhật trình máy";
            this.Load += new System.EventHandler(this.XtraFormXuatNhatTrinhMay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slue_TenMay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_XuatTheoNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayKetThuc.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayKetThuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayBatDau.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayBatDau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.DateEdit de_NgayBatDau;
        private DevExpress.XtraEditors.DateEdit de_NgayKetThuc;
        private DevExpress.XtraEditors.CheckEdit ce_XuatTheoNgay;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl SheetNhatTrinh;
        private DevExpress.XtraEditors.SimpleButton sb_XuatBaoCao;
        private DevExpress.XtraEditors.SearchLookUpEdit slue_TenMay;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}