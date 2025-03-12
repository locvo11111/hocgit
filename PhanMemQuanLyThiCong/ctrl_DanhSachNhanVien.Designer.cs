
namespace PhanMemQuanLyThiCong
{
    partial class ctrl_DanhSachNhanVien
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
            this.gc_DanhSachNhanVien = new DevExpress.XtraGrid.GridControl();
            this.gv_DanhSachNhanVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_Thoat = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_All = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gc_DanhSachNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_DanhSachNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_DanhSachNhanVien
            // 
            this.gc_DanhSachNhanVien.Location = new System.Drawing.Point(12, 12);
            this.gc_DanhSachNhanVien.MainView = this.gv_DanhSachNhanVien;
            this.gc_DanhSachNhanVien.Name = "gc_DanhSachNhanVien";
            this.gc_DanhSachNhanVien.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
            this.gc_DanhSachNhanVien.Size = new System.Drawing.Size(658, 374);
            this.gc_DanhSachNhanVien.TabIndex = 0;
            this.gc_DanhSachNhanVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_DanhSachNhanVien});
            // 
            // gv_DanhSachNhanVien
            // 
            this.gv_DanhSachNhanVien.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gv_DanhSachNhanVien.GridControl = this.gc_DanhSachNhanVien;
            this.gv_DanhSachNhanVien.Name = "gv_DanhSachNhanVien";
            this.gv_DanhSachNhanVien.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Code";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 109;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "CodeNhanVien";
            this.gridColumn2.FieldName = "CodeNhanVien";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 109;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã nhân viên";
            this.gridColumn3.FieldName = "MaNhanVien";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 78;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tên nhân viên";
            this.gridColumn4.FieldName = "TenNhanVien";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 342;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Chức vụ";
            this.gridColumn5.FieldName = "ChucVu";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 81;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Loại nhân viên";
            this.gridColumn6.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumn6.FieldName = "LoaiNhanVien";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 103;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "Nhân viên",
            "Không thuộc Cty"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Chọn";
            this.gridColumn7.FieldName = "Chon";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_All);
            this.layoutControl1.Controls.Add(this.sb_Huy);
            this.layoutControl1.Controls.Add(this.sb_Ok);
            this.layoutControl1.Controls.Add(this.sb_Thoat);
            this.layoutControl1.Controls.Add(this.gc_DanhSachNhanVien);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(682, 436);
            this.layoutControl1.TabIndex = 1;
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
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(682, 436);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gc_DanhSachNhanVien;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(662, 378);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // sb_Thoat
            // 
            this.sb_Thoat.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sb_Thoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_Thoat.Appearance.Options.UseBackColor = true;
            this.sb_Thoat.Appearance.Options.UseFont = true;
            this.sb_Thoat.Location = new System.Drawing.Point(508, 390);
            this.sb_Thoat.Name = "sb_Thoat";
            this.sb_Thoat.Size = new System.Drawing.Size(162, 34);
            this.sb_Thoat.StyleController = this.layoutControl1;
            this.sb_Thoat.TabIndex = 4;
            this.sb_Thoat.Text = "Thoát";
            this.sb_Thoat.Click += new System.EventHandler(this.sb_Thoat_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_Thoat;
            this.layoutControlItem2.Location = new System.Drawing.Point(496, 378);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(166, 38);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // sb_Ok
            // 
            this.sb_Ok.Appearance.BackColor = System.Drawing.Color.Lime;
            this.sb_Ok.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_Ok.Appearance.Options.UseBackColor = true;
            this.sb_Ok.Appearance.Options.UseFont = true;
            this.sb_Ok.Location = new System.Drawing.Point(12, 390);
            this.sb_Ok.Name = "sb_Ok";
            this.sb_Ok.Size = new System.Drawing.Size(161, 34);
            this.sb_Ok.StyleController = this.layoutControl1;
            this.sb_Ok.TabIndex = 5;
            this.sb_Ok.Text = "Đồng ý";
            this.sb_Ok.Click += new System.EventHandler(this.sb_Ok_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Ok;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 378);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(46, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(165, 38);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // sb_Huy
            // 
            this.sb_Huy.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.sb_Huy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_Huy.Appearance.Options.UseBackColor = true;
            this.sb_Huy.Appearance.Options.UseFont = true;
            this.sb_Huy.Location = new System.Drawing.Point(343, 390);
            this.sb_Huy.Name = "sb_Huy";
            this.sb_Huy.Size = new System.Drawing.Size(161, 34);
            this.sb_Huy.StyleController = this.layoutControl1;
            this.sb_Huy.TabIndex = 6;
            this.sb_Huy.Text = "Hủy chọn";
            this.sb_Huy.Click += new System.EventHandler(this.sb_Huy_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sb_Huy;
            this.layoutControlItem4.Location = new System.Drawing.Point(331, 378);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(165, 38);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // sb_All
            // 
            this.sb_All.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.sb_All.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_All.Appearance.Options.UseBackColor = true;
            this.sb_All.Appearance.Options.UseFont = true;
            this.sb_All.Location = new System.Drawing.Point(177, 390);
            this.sb_All.Name = "sb_All";
            this.sb_All.Size = new System.Drawing.Size(162, 34);
            this.sb_All.StyleController = this.layoutControl1;
            this.sb_All.TabIndex = 7;
            this.sb_All.Text = "Chọn tất cả";
            this.sb_All.Click += new System.EventHandler(this.sb_All_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sb_All;
            this.layoutControlItem5.Location = new System.Drawing.Point(165, 378);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(166, 38);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // ctrl_DanhSachNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ctrl_DanhSachNhanVien";
            this.Size = new System.Drawing.Size(682, 436);
            ((System.ComponentModel.ISupportInitialize)(this.gc_DanhSachNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_DanhSachNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc_DanhSachNhanVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_DanhSachNhanVien;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton sb_All;
        private DevExpress.XtraEditors.SimpleButton sb_Huy;
        private DevExpress.XtraEditors.SimpleButton sb_Ok;
        private DevExpress.XtraEditors.SimpleButton sb_Thoat;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}
