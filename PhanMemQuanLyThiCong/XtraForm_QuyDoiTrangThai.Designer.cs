namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_QuyDoiTrangThai
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.bt_OK = new DevExpress.XtraEditors.SimpleButton();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.tl_QuyDoiTrangThai = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repoCbb_TrangThaiHeThong = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCbb_TrangThaiHeThong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_OK);
            this.layoutControl1.Controls.Add(this.treeList1);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(837, 420);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bt_OK
            // 
            this.bt_OK.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bt_OK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_OK.Appearance.Options.UseBackColor = true;
            this.bt_OK.Appearance.Options.UseFont = true;
            this.bt_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_OK.Location = new System.Drawing.Point(12, 386);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(813, 22);
            this.bt_OK.StyleController = this.layoutControl1;
            this.bt_OK.TabIndex = 6;
            this.bt_OK.Text = "Hoàn tất";
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tl_QuyDoiTrangThai,
            this.treeListColumn2});
            this.treeList1.Location = new System.Drawing.Point(12, 35);
            this.treeList1.Name = "treeList1";
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoCbb_TrangThaiHeThong});
            this.treeList1.Size = new System.Drawing.Size(813, 347);
            this.treeList1.TabIndex = 5;
            // 
            // tl_QuyDoiTrangThai
            // 
            this.tl_QuyDoiTrangThai.Caption = "Trạng thái file đọc vào";
            this.tl_QuyDoiTrangThai.FieldName = "TenGoc";
            this.tl_QuyDoiTrangThai.Name = "tl_QuyDoiTrangThai";
            this.tl_QuyDoiTrangThai.Visible = true;
            this.tl_QuyDoiTrangThai.VisibleIndex = 0;
            this.tl_QuyDoiTrangThai.Width = 339;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Trạng thái hệ thống";
            this.treeListColumn2.ColumnEdit = this.repoCbb_TrangThaiHeThong;
            this.treeListColumn2.FieldName = "TenHeThong";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 449;
            // 
            // repoCbb_TrangThaiHeThong
            // 
            this.repoCbb_TrangThaiHeThong.AutoHeight = false;
            this.repoCbb_TrangThaiHeThong.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoCbb_TrangThaiHeThong.Items.AddRange(new object[] {
            "Chưa thực hiện",
            "Đang thực hiện",
            "Đang xét duyệt",
            "Đề nghị kiểm tra",
            "Hoành thành",
            "Dừng hoạt động"});
            this.repoCbb_TrangThaiHeThong.Name = "repoCbb_TrangThaiHeThong";
            this.repoCbb_TrangThaiHeThong.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(813, 19);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Quy đổi trạng thái";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(837, 420);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.labelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(817, 23);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.treeList1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(817, 351);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_OK;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 374);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(817, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // XtraForm_QuyDoiTrangThai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 420);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_QuyDoiTrangThai";
            this.Text = "XtraForm_QuyDoiTrangThai";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraForm_QuyDoiTrangThai_FormClosing);
            this.Load += new System.EventHandler(this.XtraForm_QuyDoiTrangThai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCbb_TrangThaiHeThong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tl_QuyDoiTrangThai;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repoCbb_TrangThaiHeThong;
        private DevExpress.XtraEditors.SimpleButton bt_OK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}