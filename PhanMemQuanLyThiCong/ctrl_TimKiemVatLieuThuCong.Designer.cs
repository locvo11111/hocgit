
namespace PhanMemQuanLyThiCong
{
    partial class ctrl_TimKiemVatLieuThuCong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrl_TimKiemVatLieuThuCong));
            this.gc_LoadVatLieu = new DevExpress.XtraGrid.GridControl();
            this.gv_LoadVatLieu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaVatLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenVatLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sb_ChonAll = new DevExpress.XtraEditors.SimpleButton();
            this.sb_DongY = new DevExpress.XtraEditors.SimpleButton();
            this.sb_HuyChon = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gc_LoadVatLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_LoadVatLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_LoadVatLieu
            // 
            this.gc_LoadVatLieu.Location = new System.Drawing.Point(12, 12);
            this.gc_LoadVatLieu.MainView = this.gv_LoadVatLieu;
            this.gc_LoadVatLieu.Name = "gc_LoadVatLieu";
            this.gc_LoadVatLieu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gc_LoadVatLieu.Size = new System.Drawing.Size(838, 501);
            this.gc_LoadVatLieu.TabIndex = 5;
            this.gc_LoadVatLieu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_LoadVatLieu});
            // 
            // gv_LoadVatLieu
            // 
            this.gv_LoadVatLieu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chon,
            this.Code,
            this.MaVatLieu,
            this.gridColumn1,
            this.TenVatLieu,
            this.DonVi});
            this.gv_LoadVatLieu.GridControl = this.gc_LoadVatLieu;
            this.gv_LoadVatLieu.Name = "gv_LoadVatLieu";
            this.gv_LoadVatLieu.OptionsView.ShowGroupPanel = false;
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
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã TXHiện Trường";
            this.gridColumn1.FieldName = "MaTXHienTruong";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            // 
            // TenVatLieu
            // 
            this.TenVatLieu.Caption = "Tên Vật Liệu";
            this.TenVatLieu.FieldName = "VatTu";
            this.TenVatLieu.Name = "TenVatLieu";
            this.TenVatLieu.OptionsColumn.ReadOnly = true;
            this.TenVatLieu.Visible = true;
            this.TenVatLieu.VisibleIndex = 3;
            // 
            // DonVi
            // 
            this.DonVi.Caption = "Đơn vị";
            this.DonVi.FieldName = "DonVi";
            this.DonVi.Name = "DonVi";
            this.DonVi.OptionsColumn.ReadOnly = true;
            this.DonVi.Visible = true;
            this.DonVi.VisibleIndex = 4;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_ChonAll);
            this.layoutControl1.Controls.Add(this.sb_DongY);
            this.layoutControl1.Controls.Add(this.sb_HuyChon);
            this.layoutControl1.Controls.Add(this.gc_LoadVatLieu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(862, 567);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sb_ChonAll
            // 
            this.sb_ChonAll.Appearance.BackColor = System.Drawing.Color.Lime;
            this.sb_ChonAll.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_ChonAll.Appearance.Options.UseBackColor = true;
            this.sb_ChonAll.Appearance.Options.UseFont = true;
            this.sb_ChonAll.Location = new System.Drawing.Point(293, 517);
            this.sb_ChonAll.Name = "sb_ChonAll";
            this.sb_ChonAll.Size = new System.Drawing.Size(276, 38);
            this.sb_ChonAll.StyleController = this.layoutControl1;
            this.sb_ChonAll.TabIndex = 8;
            this.sb_ChonAll.Text = "Chọn tất cả";
            this.sb_ChonAll.Click += new System.EventHandler(this.sb_ChonAll_Click);
            // 
            // sb_DongY
            // 
            this.sb_DongY.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.sb_DongY.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_DongY.Appearance.Options.UseBackColor = true;
            this.sb_DongY.Appearance.Options.UseFont = true;
            this.sb_DongY.Location = new System.Drawing.Point(12, 517);
            this.sb_DongY.Name = "sb_DongY";
            this.sb_DongY.Size = new System.Drawing.Size(277, 38);
            this.sb_DongY.StyleController = this.layoutControl1;
            this.sb_DongY.TabIndex = 7;
            this.sb_DongY.Text = "Đồng ý";
            this.sb_DongY.Click += new System.EventHandler(this.sb_DongY_Click);
            // 
            // sb_HuyChon
            // 
            this.sb_HuyChon.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sb_HuyChon.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_HuyChon.Appearance.Options.UseBackColor = true;
            this.sb_HuyChon.Appearance.Options.UseFont = true;
            this.sb_HuyChon.Location = new System.Drawing.Point(573, 517);
            this.sb_HuyChon.Name = "sb_HuyChon";
            this.sb_HuyChon.Size = new System.Drawing.Size(277, 38);
            this.sb_HuyChon.StyleController = this.layoutControl1;
            this.sb_HuyChon.TabIndex = 6;
            this.sb_HuyChon.Text = "Thoát";
            this.sb_HuyChon.Click += new System.EventHandler(this.sb_HuyChon_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(862, 567);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gc_LoadVatLieu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(842, 505);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_HuyChon;
            this.layoutControlItem2.Location = new System.Drawing.Point(561, 505);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(281, 42);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_DongY;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 505);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(281, 42);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sb_ChonAll;
            this.layoutControlItem4.Location = new System.Drawing.Point(281, 505);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(280, 42);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // ctrl_TimKiemVatLieuThuCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ctrl_TimKiemVatLieuThuCong";
            this.Size = new System.Drawing.Size(862, 567);
            ((System.ComponentModel.ISupportInitialize)(this.gc_LoadVatLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_LoadVatLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc_LoadVatLieu;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_LoadVatLieu;
        private DevExpress.XtraGrid.Columns.GridColumn Chon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn Code;
        private DevExpress.XtraGrid.Columns.GridColumn MaVatLieu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn TenVatLieu;
        private DevExpress.XtraGrid.Columns.GridColumn DonVi;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton sb_ChonAll;
        private DevExpress.XtraEditors.SimpleButton sb_DongY;
        private DevExpress.XtraEditors.SimpleButton sb_HuyChon;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}
