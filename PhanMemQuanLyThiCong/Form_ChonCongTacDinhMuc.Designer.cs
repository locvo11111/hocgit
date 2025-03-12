
namespace PhanMemQuanLyThiCong
{
    partial class Form_ChonCongTacDinhMuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ChonCongTacDinhMuc));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.Chon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_MaVatTu = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_TenVatTu = new System.Windows.Forms.TextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_LoaiVatTu = new System.Windows.Forms.ComboBox();
            this.bt_boLoc = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.bt_OK = new System.Windows.Forms.Button();
            this.spn_LocVatTu = new DevExpress.Utils.Layout.StackPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uc_ChonTinhThanh1 = new PhanMemQuanLyThiCong.Controls.uc_ChonTinhThanh();
            this.gc_LoadVatLieu = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColMaVatLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColTenVatLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_VatTuKhongDau = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_LoaiVatLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ce_VatLieuMoi = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spn_LocVatTu)).BeginInit();
            this.spn_LocVatTu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_LoadVatLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_VatLieuMoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Chon
            // 
            this.Chon.AppearanceHeader.Options.UseTextOptions = true;
            this.Chon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Chon.Caption = "Chọn";
            this.Chon.ColumnEdit = this.repositoryItemCheckEdit1;
            this.Chon.FieldName = "Chon";
            this.Chon.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.Chon.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Chon.ImageOptions.Image")));
            this.Chon.MinWidth = 19;
            this.Chon.Name = "Chon";
            this.Chon.OptionsColumn.FixedWidth = true;
            this.Chon.OptionsColumn.ShowCaption = false;
            this.Chon.Visible = true;
            this.Chon.VisibleIndex = 0;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.CheckBox;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã vật tư";
            // 
            // txt_MaVatTu
            // 
            this.txt_MaVatTu.Location = new System.Drawing.Point(63, 3);
            this.txt_MaVatTu.Name = "txt_MaVatTu";
            this.txt_MaVatTu.Size = new System.Drawing.Size(100, 21);
            this.txt_MaVatTu.TabIndex = 1;
            this.txt_MaVatTu.TextChanged += new System.EventHandler(this.txt_MaVatTu_TextChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(169, 12);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(20, 3);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên vật tư";
            // 
            // txt_TenVatTu
            // 
            this.txt_TenVatTu.Location = new System.Drawing.Point(259, 3);
            this.txt_TenVatTu.Name = "txt_TenVatTu";
            this.txt_TenVatTu.Size = new System.Drawing.Size(100, 21);
            this.txt_TenVatTu.TabIndex = 3;
            this.txt_TenVatTu.TextChanged += new System.EventHandler(this.txt_TenVatTu_TextChanged);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(365, 12);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(20, 3);
            this.splitter2.TabIndex = 5;
            this.splitter2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(391, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Loại vật tư";
            // 
            // cbb_LoaiVatTu
            // 
            this.cbb_LoaiVatTu.FormattingEnabled = true;
            this.cbb_LoaiVatTu.Items.AddRange(new object[] {
            "Tất cả",
            "Vật liệu",
            "Nhân công",
            "Máy thi công"});
            this.cbb_LoaiVatTu.Location = new System.Drawing.Point(456, 3);
            this.cbb_LoaiVatTu.Name = "cbb_LoaiVatTu";
            this.cbb_LoaiVatTu.Size = new System.Drawing.Size(121, 21);
            this.cbb_LoaiVatTu.TabIndex = 7;
            this.cbb_LoaiVatTu.SelectedIndexChanged += new System.EventHandler(this.cbb_LoaiVatTu_SelectedIndexChanged);
            // 
            // bt_boLoc
            // 
            this.bt_boLoc.Location = new System.Drawing.Point(583, 2);
            this.bt_boLoc.Name = "bt_boLoc";
            this.bt_boLoc.Size = new System.Drawing.Size(75, 23);
            this.bt_boLoc.TabIndex = 8;
            this.bt_boLoc.Text = "Bỏ lọc";
            this.bt_boLoc.UseVisualStyleBackColor = true;
            this.bt_boLoc.Click += new System.EventHandler(this.bt_boLoc_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.BackColor = System.Drawing.Color.Red;
            this.bt_cancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.bt_cancel.ForeColor = System.Drawing.Color.White;
            this.bt_cancel.Location = new System.Drawing.Point(840, 0);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(103, 30);
            this.bt_cancel.TabIndex = 0;
            this.bt_cancel.Text = "Hủy bỏ";
            this.bt_cancel.UseVisualStyleBackColor = false;
            // 
            // bt_OK
            // 
            this.bt_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.bt_OK.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_OK.Location = new System.Drawing.Point(737, 0);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(103, 30);
            this.bt_OK.TabIndex = 0;
            this.bt_OK.Text = "Đồng ý";
            this.bt_OK.UseVisualStyleBackColor = false;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // spn_LocVatTu
            // 
            this.spn_LocVatTu.Controls.Add(this.label1);
            this.spn_LocVatTu.Controls.Add(this.txt_MaVatTu);
            this.spn_LocVatTu.Controls.Add(this.splitter1);
            this.spn_LocVatTu.Controls.Add(this.label2);
            this.spn_LocVatTu.Controls.Add(this.txt_TenVatTu);
            this.spn_LocVatTu.Controls.Add(this.splitter2);
            this.spn_LocVatTu.Controls.Add(this.label3);
            this.spn_LocVatTu.Controls.Add(this.cbb_LoaiVatTu);
            this.spn_LocVatTu.Controls.Add(this.bt_boLoc);
            this.spn_LocVatTu.Dock = System.Windows.Forms.DockStyle.Left;
            this.spn_LocVatTu.Location = new System.Drawing.Point(2, 2);
            this.spn_LocVatTu.Name = "spn_LocVatTu";
            this.spn_LocVatTu.Size = new System.Drawing.Size(661, 28);
            this.spn_LocVatTu.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uc_ChonTinhThanh1);
            this.panel1.Controls.Add(this.bt_OK);
            this.panel1.Controls.Add(this.bt_cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(943, 30);
            this.panel1.TabIndex = 5;
            // 
            // uc_ChonTinhThanh1
            // 
            this.uc_ChonTinhThanh1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uc_ChonTinhThanh1.Location = new System.Drawing.Point(0, 0);
            this.uc_ChonTinhThanh1.Name = "uc_ChonTinhThanh1";
            this.uc_ChonTinhThanh1.Size = new System.Drawing.Size(431, 30);
            this.uc_ChonTinhThanh1.TabIndex = 1;
            this.uc_ChonTinhThanh1.ValueChanged += new System.EventHandler(this.uc_ChonTinhThanh1_ValueChanged);
            // 
            // gc_LoadVatLieu
            // 
            this.gc_LoadVatLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_LoadVatLieu.Location = new System.Drawing.Point(0, 32);
            this.gc_LoadVatLieu.MainView = this.gridView1;
            this.gc_LoadVatLieu.Name = "gc_LoadVatLieu";
            this.gc_LoadVatLieu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gc_LoadVatLieu.Size = new System.Drawing.Size(943, 385);
            this.gc_LoadVatLieu.TabIndex = 8;
            this.gc_LoadVatLieu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chon,
            this.Code,
            this.ColMaVatLieu,
            this.gridColumn1,
            this.ColTenVatLieu,
            this.DonVi,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.col_VatTuKhongDau,
            this.col_LoaiVatLieu});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.Chon;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = true;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.GridControl = this.gc_LoadVatLieu;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // Code
            // 
            this.Code.Caption = "Code";
            this.Code.FieldName = "Code";
            this.Code.Name = "Code";
            this.Code.OptionsColumn.ReadOnly = true;
            // 
            // ColMaVatLieu
            // 
            this.ColMaVatLieu.Caption = "Mã Vật Liệu";
            this.ColMaVatLieu.FieldName = "MaVatLieu";
            this.ColMaVatLieu.Name = "ColMaVatLieu";
            this.ColMaVatLieu.OptionsColumn.ReadOnly = true;
            this.ColMaVatLieu.Visible = true;
            this.ColMaVatLieu.VisibleIndex = 1;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã TXHiện Trường";
            this.gridColumn1.FieldName = "MaTXHienTruong";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 5;
            // 
            // ColTenVatLieu
            // 
            this.ColTenVatLieu.Caption = "Tên Vật Liệu";
            this.ColTenVatLieu.FieldName = "VatTu";
            this.ColTenVatLieu.Name = "ColTenVatLieu";
            this.ColTenVatLieu.OptionsColumn.ReadOnly = true;
            this.ColTenVatLieu.Visible = true;
            this.ColTenVatLieu.VisibleIndex = 2;
            // 
            // DonVi
            // 
            this.DonVi.Caption = "Đơn vị";
            this.DonVi.FieldName = "DonVi";
            this.DonVi.Name = "DonVi";
            this.DonVi.OptionsColumn.ReadOnly = true;
            this.DonVi.Visible = true;
            this.DonVi.VisibleIndex = 6;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Đơn giá hiện trường";
            this.gridColumn2.FieldName = "DonGia";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 7;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Hệ số";
            this.gridColumn3.FieldName = "HeSo";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Định mức";
            this.gridColumn4.FieldName = "DinhMuc";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // col_VatTuKhongDau
            // 
            this.col_VatTuKhongDau.Caption = "VatTuKhongDau";
            this.col_VatTuKhongDau.FieldName = "VatTu_KhongDau";
            this.col_VatTuKhongDau.Name = "col_VatTuKhongDau";
            this.col_VatTuKhongDau.OptionsColumn.ReadOnly = true;
            // 
            // col_LoaiVatLieu
            // 
            this.col_LoaiVatLieu.Caption = "VatTuKhongDau";
            this.col_LoaiVatLieu.FieldName = "LoaiVatLieu";
            this.col_LoaiVatLieu.Name = "col_LoaiVatLieu";
            this.col_LoaiVatLieu.OptionsColumn.ReadOnly = true;
            // 
            // ce_VatLieuMoi
            // 
            this.ce_VatLieuMoi.Dock = System.Windows.Forms.DockStyle.Left;
            this.ce_VatLieuMoi.Location = new System.Drawing.Point(663, 2);
            this.ce_VatLieuMoi.Name = "ce_VatLieuMoi";
            this.ce_VatLieuMoi.Properties.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.ce_VatLieuMoi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ce_VatLieuMoi.Properties.Appearance.Options.UseBackColor = true;
            this.ce_VatLieuMoi.Properties.Appearance.Options.UseFont = true;
            this.ce_VatLieuMoi.Properties.Caption = "Vật liệu mới";
            this.ce_VatLieuMoi.Size = new System.Drawing.Size(98, 28);
            this.ce_VatLieuMoi.TabIndex = 9;
            this.ce_VatLieuMoi.CheckedChanged += new System.EventHandler(this.ce_VatLieuMoi_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ce_VatLieuMoi);
            this.panelControl1.Controls.Add(this.spn_LocVatTu);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(943, 32);
            this.panelControl1.TabIndex = 9;
            // 
            // Form_ChonCongTacDinhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 447);
            this.Controls.Add(this.gc_LoadVatLieu);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "Form_ChonCongTacDinhMuc";
            this.Text = "Chọn vật liệu";
            this.Load += new System.EventHandler(this.Form_ChonCongTacDinhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spn_LocVatTu)).EndInit();
            this.spn_LocVatTu.ResumeLayout(false);
            this.spn_LocVatTu.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_LoadVatLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_VatLieuMoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_MaVatTu;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_TenVatTu;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbb_LoaiVatTu;
        private System.Windows.Forms.Button bt_boLoc;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.Button bt_OK;
        private DevExpress.Utils.Layout.StackPanel spn_LocVatTu;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gc_LoadVatLieu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn Chon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn Code;
        private DevExpress.XtraGrid.Columns.GridColumn ColMaVatLieu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn ColTenVatLieu;
        private DevExpress.XtraGrid.Columns.GridColumn DonVi;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn col_VatTuKhongDau;
        private DevExpress.XtraGrid.Columns.GridColumn col_LoaiVatLieu;
        private Controls.uc_ChonTinhThanh uc_ChonTinhThanh1;
        private DevExpress.XtraEditors.CheckEdit ce_VatLieuMoi;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}