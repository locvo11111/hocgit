
namespace PhanMemQuanLyThiCong
{
    partial class ctrlTimKiemVatLieu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlTimKiemVatLieu));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_ChonYeuCau = new System.Windows.Forms.Button();
            this.btn_ChonAll = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_BoChonAll = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.gc_LoadVatLieu = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaVatLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenVatLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_LoadVatLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_ChonYeuCau);
            this.layoutControl1.Controls.Add(this.btn_ChonAll);
            this.layoutControl1.Controls.Add(this.btn_Ok);
            this.layoutControl1.Controls.Add(this.btn_BoChonAll);
            this.layoutControl1.Controls.Add(this.bt_cancel);
            this.layoutControl1.Controls.Add(this.gc_LoadVatLieu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1054, 289, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(756, 403);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_ChonYeuCau
            // 
            this.btn_ChonYeuCau.BackColor = System.Drawing.Color.Lime;
            this.btn_ChonYeuCau.Location = new System.Drawing.Point(306, 363);
            this.btn_ChonYeuCau.Name = "btn_ChonYeuCau";
            this.btn_ChonYeuCau.Size = new System.Drawing.Size(144, 28);
            this.btn_ChonYeuCau.TabIndex = 11;
            this.btn_ChonYeuCau.Text = "Chọn từ đề xuất";
            this.btn_ChonYeuCau.UseVisualStyleBackColor = false;
            this.btn_ChonYeuCau.Click += new System.EventHandler(this.btn_ChonYeuCau_Click);
            // 
            // btn_ChonAll
            // 
            this.btn_ChonAll.BackColor = System.Drawing.Color.Aqua;
            this.btn_ChonAll.Location = new System.Drawing.Point(159, 363);
            this.btn_ChonAll.Name = "btn_ChonAll";
            this.btn_ChonAll.Size = new System.Drawing.Size(143, 28);
            this.btn_ChonAll.TabIndex = 11;
            this.btn_ChonAll.Text = "Chọn tất cả";
            this.btn_ChonAll.UseVisualStyleBackColor = false;
            this.btn_ChonAll.Click += new System.EventHandler(this.btn_ChonAll_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.BackColor = System.Drawing.Color.Yellow;
            this.btn_Ok.Location = new System.Drawing.Point(601, 363);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(143, 28);
            this.btn_Ok.TabIndex = 10;
            this.btn_Ok.Text = "Đồng ý";
            this.btn_Ok.UseVisualStyleBackColor = false;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_BoChonAll
            // 
            this.btn_BoChonAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_BoChonAll.Location = new System.Drawing.Point(454, 363);
            this.btn_BoChonAll.Name = "btn_BoChonAll";
            this.btn_BoChonAll.Size = new System.Drawing.Size(143, 28);
            this.btn_BoChonAll.TabIndex = 9;
            this.btn_BoChonAll.Text = "Bỏ chọn tất cả";
            this.btn_BoChonAll.UseVisualStyleBackColor = false;
            this.btn_BoChonAll.Click += new System.EventHandler(this.btn_BoChonAll_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.bt_cancel.Location = new System.Drawing.Point(12, 363);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(143, 28);
            this.bt_cancel.TabIndex = 5;
            this.bt_cancel.Text = "Hủy bỏ";
            this.bt_cancel.UseVisualStyleBackColor = false;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // gc_LoadVatLieu
            // 
            this.gc_LoadVatLieu.Location = new System.Drawing.Point(12, 12);
            this.gc_LoadVatLieu.MainView = this.gridView1;
            this.gc_LoadVatLieu.Name = "gc_LoadVatLieu";
            this.gc_LoadVatLieu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gc_LoadVatLieu.Size = new System.Drawing.Size(732, 347);
            this.gc_LoadVatLieu.TabIndex = 4;
            this.gc_LoadVatLieu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chon,
            this.Code,
            this.MaVatLieu,
            this.gridColumn1,
            this.TenVatLieu,
            this.DonVi,
            this.gridColumn2});
            this.gridView1.GridControl = this.gc_LoadVatLieu;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
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
            // Code
            // 
            this.Code.Caption = "Code";
            this.Code.FieldName = "Code";
            this.Code.Name = "Code";
            this.Code.OptionsColumn.ReadOnly = true;
            // 
            // MaVatLieu
            // 
            this.MaVatLieu.Caption = "Mã Vật Liệu";
            this.MaVatLieu.FieldName = "MaVatLieu";
            this.MaVatLieu.Name = "MaVatLieu";
            this.MaVatLieu.OptionsColumn.ReadOnly = true;
            this.MaVatLieu.Visible = true;
            this.MaVatLieu.VisibleIndex = 1;
            this.MaVatLieu.Width = 85;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã TXHiện Trường";
            this.gridColumn1.FieldName = "MaTXHienTruong";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // TenVatLieu
            // 
            this.TenVatLieu.Caption = "Tên Vật Liệu";
            this.TenVatLieu.FieldName = "VatTu";
            this.TenVatLieu.Name = "TenVatLieu";
            this.TenVatLieu.OptionsColumn.ReadOnly = true;
            this.TenVatLieu.Visible = true;
            this.TenVatLieu.VisibleIndex = 2;
            this.TenVatLieu.Width = 369;
            // 
            // DonVi
            // 
            this.DonVi.Caption = "Đơn vị";
            this.DonVi.FieldName = "DonVi";
            this.DonVi.Name = "DonVi";
            this.DonVi.OptionsColumn.ReadOnly = true;
            this.DonVi.Visible = true;
            this.DonVi.VisibleIndex = 3;
            this.DonVi.Width = 70;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Đơn giá hiện trường";
            this.gridColumn2.FieldName = "DonGia";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 108;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(756, 403);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gc_LoadVatLieu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(736, 351);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.bt_cancel;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 351);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(147, 32);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_BoChonAll;
            this.layoutControlItem6.Location = new System.Drawing.Point(442, 351);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(147, 32);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Ok;
            this.layoutControlItem3.Location = new System.Drawing.Point(589, 351);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(147, 32);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_ChonAll;
            this.layoutControlItem4.Location = new System.Drawing.Point(147, 351);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(147, 32);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_ChonYeuCau;
            this.layoutControlItem5.Location = new System.Drawing.Point(294, 351);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(148, 32);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // ctrlTimKiemVatLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ctrlTimKiemVatLieu";
            this.Size = new System.Drawing.Size(756, 403);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_LoadVatLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gc_LoadVatLieu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.Button btn_ChonAll;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_BoChonAll;
        private System.Windows.Forms.Button bt_cancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn Chon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn Code;
        private DevExpress.XtraGrid.Columns.GridColumn MaVatLieu;
        private DevExpress.XtraGrid.Columns.GridColumn TenVatLieu;
        private DevExpress.XtraGrid.Columns.GridColumn DonVi;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Button btn_ChonYeuCau;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}
