
namespace PhanMemQuanLyThiCong
{
    partial class Form_LayDauViecTuTDKHSangGiaoViec
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.bt_OK = new System.Windows.Forms.Button();
            this.bt_uncheckAll = new System.Windows.Forms.Button();
            this.bt_checkAll = new System.Windows.Forms.Button();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbb_DonViThucHien = new System.Windows.Forms.ComboBox();
            this.cbb_DoiTuong = new System.Windows.Forms.ComboBox();
            this.gc_CongTacDaLay = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc_CongTacChuaLay = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_CongTacDaLay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_CongTacChuaLay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_cancel);
            this.panel1.Controls.Add(this.bt_OK);
            this.panel1.Controls.Add(this.bt_uncheckAll);
            this.panel1.Controls.Add(this.bt_checkAll);
            this.panel1.Location = new System.Drawing.Point(12, 515);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(947, 76);
            this.panel1.TabIndex = 3;
            // 
            // bt_cancel
            // 
            this.bt_cancel.Location = new System.Drawing.Point(579, 6);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(103, 23);
            this.bt_cancel.TabIndex = 0;
            this.bt_cancel.Text = "Hủy bỏ";
            this.bt_cancel.UseVisualStyleBackColor = true;
            // 
            // bt_OK
            // 
            this.bt_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_OK.Location = new System.Drawing.Point(221, 6);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(103, 23);
            this.bt_OK.TabIndex = 0;
            this.bt_OK.Text = "Đồng ý";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // bt_uncheckAll
            // 
            this.bt_uncheckAll.Location = new System.Drawing.Point(112, 6);
            this.bt_uncheckAll.Name = "bt_uncheckAll";
            this.bt_uncheckAll.Size = new System.Drawing.Size(103, 23);
            this.bt_uncheckAll.TabIndex = 0;
            this.bt_uncheckAll.Text = "Bỏ chọn tất cả";
            this.bt_uncheckAll.UseVisualStyleBackColor = true;
            // 
            // bt_checkAll
            // 
            this.bt_checkAll.Location = new System.Drawing.Point(3, 6);
            this.bt_checkAll.Name = "bt_checkAll";
            this.bt_checkAll.Size = new System.Drawing.Size(103, 23);
            this.bt_checkAll.TabIndex = 0;
            this.bt_checkAll.Text = "Chọn tất cả";
            this.bt_checkAll.UseVisualStyleBackColor = true;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbb_DonViThucHien);
            this.layoutControl1.Controls.Add(this.cbb_DoiTuong);
            this.layoutControl1.Controls.Add(this.gc_CongTacDaLay);
            this.layoutControl1.Controls.Add(this.gc_CongTacChuaLay);
            this.layoutControl1.Controls.Add(this.panel1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(971, 603);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbb_DonViThucHien
            // 
            this.cbb_DonViThucHien.DisplayMember = "Ten";
            this.cbb_DonViThucHien.FormattingEnabled = true;
            this.cbb_DonViThucHien.Location = new System.Drawing.Point(487, 32);
            this.cbb_DonViThucHien.Name = "cbb_DonViThucHien";
            this.cbb_DonViThucHien.Size = new System.Drawing.Size(472, 21);
            this.cbb_DonViThucHien.TabIndex = 15;
            this.cbb_DonViThucHien.ValueMember = "Code";
            this.cbb_DonViThucHien.SelectedIndexChanged += new System.EventHandler(this.cbb_DonViThucHien_SelectedIndexChanged);
            // 
            // cbb_DoiTuong
            // 
            this.cbb_DoiTuong.DisplayMember = "Value";
            this.cbb_DoiTuong.FormattingEnabled = true;
            this.cbb_DoiTuong.Location = new System.Drawing.Point(12, 32);
            this.cbb_DoiTuong.Name = "cbb_DoiTuong";
            this.cbb_DoiTuong.Size = new System.Drawing.Size(471, 21);
            this.cbb_DoiTuong.TabIndex = 14;
            this.cbb_DoiTuong.ValueMember = "Key";
            this.cbb_DoiTuong.SelectedIndexChanged += new System.EventHandler(this.cbb_DoiTuong_SelectedIndexChanged);
            // 
            // gc_CongTacDaLay
            // 
            this.gc_CongTacDaLay.Location = new System.Drawing.Point(12, 324);
            this.gc_CongTacDaLay.MainView = this.gridView2;
            this.gc_CongTacDaLay.Name = "gc_CongTacDaLay";
            this.gc_CongTacDaLay.Size = new System.Drawing.Size(947, 187);
            this.gc_CongTacDaLay.TabIndex = 5;
            this.gc_CongTacDaLay.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gc_CongTacDaLay;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gc_CongTacChuaLay
            // 
            this.gc_CongTacChuaLay.Location = new System.Drawing.Point(12, 76);
            this.gc_CongTacChuaLay.MainView = this.gridView1;
            this.gc_CongTacChuaLay.Name = "gc_CongTacChuaLay";
            this.gc_CongTacChuaLay.Size = new System.Drawing.Size(947, 224);
            this.gc_CongTacChuaLay.TabIndex = 4;
            this.gc_CongTacChuaLay.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.gc_CongTacChuaLay;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Chọn";
            this.gridColumn1.FieldName = "Chon";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên công tác";
            this.gridColumn2.FieldName = "TenCongTac";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Khối lượng toàn bộ";
            this.gridColumn3.FieldName = "KhoiLuongToanBo";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Khối lượng hợp đồng";
            this.gridColumn4.FieldName = "KhoiLuongHopDong";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
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
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(971, 603);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.gc_CongTacChuaLay;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 45);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(951, 247);
            this.layoutControlItem1.Text = "Công tác chưa thêm";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(139, 16);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.gc_CongTacDaLay;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 292);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(951, 211);
            this.layoutControlItem2.Text = "Công tác đã lấy";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(139, 17);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panel1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 503);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(951, 80);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.cbb_DonViThucHien;
            this.layoutControlItem4.Location = new System.Drawing.Point(475, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(476, 45);
            this.layoutControlItem4.Text = "Đơn vị thực hiện";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(139, 17);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.cbb_DoiTuong;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(475, 45);
            this.layoutControlItem5.Text = "Đối tượng thực hiện";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(139, 17);
            // 
            // Form_LayDauViecTuTDKHSangGiaoViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 603);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Form_LayDauViecTuTDKHSangGiaoViec";
            this.Text = "Form_LayDauViecTuTDKH";
            this.Load += new System.EventHandler(this.Form_LayDauViecTuCSDL_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_CongTacDaLay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_CongTacChuaLay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.Button bt_uncheckAll;
        private System.Windows.Forms.Button bt_checkAll;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gc_CongTacDaLay;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gc_CongTacChuaLay;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.ComboBox cbb_DonViThucHien;
        private System.Windows.Forms.ComboBox cbb_DoiTuong;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}