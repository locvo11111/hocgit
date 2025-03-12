namespace PhanMemQuanLyThiCong.Controls.Approval
{
    partial class uc_ApprovalDetails
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_ApprovalDetails));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.xtraTabControl_Approval = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage_DoiDuyet = new DevExpress.XtraTab.XtraTabPage();
            this.tl_DoiDuyet = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn11 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlCol_Duyet = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repobt_Duyet = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.xtraTabPage_DaDuyet = new DevExpress.XtraTab.XtraTabPage();
            this.tl_DaDuyet = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbb_ChonQuyTrinh = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl_Approval)).BeginInit();
            this.xtraTabControl_Approval.SuspendLayout();
            this.xtraTabPage_DoiDuyet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_DoiDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_Duyet)).BeginInit();
            this.xtraTabPage_DaDuyet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_DaDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbb_ChonQuyTrinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl_Approval
            // 
            this.xtraTabControl_Approval.Location = new System.Drawing.Point(12, 36);
            this.xtraTabControl_Approval.Name = "xtraTabControl_Approval";
            this.xtraTabControl_Approval.SelectedTabPage = this.xtraTabPage_DoiDuyet;
            this.xtraTabControl_Approval.Size = new System.Drawing.Size(875, 477);
            this.xtraTabControl_Approval.TabIndex = 0;
            this.xtraTabControl_Approval.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage_DoiDuyet,
            this.xtraTabPage_DaDuyet});
            this.xtraTabControl_Approval.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl_Approval_SelectedPageChanged);
            // 
            // xtraTabPage_DoiDuyet
            // 
            this.xtraTabPage_DoiDuyet.Controls.Add(this.tl_DoiDuyet);
            this.xtraTabPage_DoiDuyet.Name = "xtraTabPage_DoiDuyet";
            this.xtraTabPage_DoiDuyet.Size = new System.Drawing.Size(873, 452);
            this.xtraTabPage_DoiDuyet.Text = "Đợi bạn duyệt";
            // 
            // tl_DoiDuyet
            // 
            this.tl_DoiDuyet.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn5,
            this.treeListColumn11,
            this.treeListColumn4,
            this.tlCol_Duyet});
            this.tl_DoiDuyet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tl_DoiDuyet.KeyFieldName = "Id";
            this.tl_DoiDuyet.Location = new System.Drawing.Point(0, 0);
            this.tl_DoiDuyet.Name = "tl_DoiDuyet";
            this.tl_DoiDuyet.ParentFieldName = "ParentId";
            this.tl_DoiDuyet.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repobt_Duyet});
            this.tl_DoiDuyet.Size = new System.Drawing.Size(873, 452);
            this.tl_DoiDuyet.TabIndex = 0;
            this.tl_DoiDuyet.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tl_DoiDuyet_NodeCellStyle);
            this.tl_DoiDuyet.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.tl_DoiDuyet_CustomDrawNodeCell);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "STT";
            this.treeListColumn1.FieldName = "STT";
            this.treeListColumn1.MaxWidth = 50;
            this.treeListColumn1.MinWidth = 50;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.FixedWidth = true;
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 50;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Mã hiệu";
            this.treeListColumn2.FieldName = "MaCongTac";
            this.treeListColumn2.MaxWidth = 100;
            this.treeListColumn2.MinWidth = 100;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.FixedWidth = true;
            this.treeListColumn2.OptionsColumn.ReadOnly = true;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 100;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Công tác";
            this.treeListColumn3.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn3.FieldName = "TenCongTac";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.OptionsColumn.ReadOnly = true;
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 2;
            this.treeListColumn3.Width = 102;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Khối lượng";
            this.treeListColumn5.FieldName = "KhoiLuongKeHoach";
            this.treeListColumn5.Format.FormatString = "n2";
            this.treeListColumn5.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn5.MaxWidth = 150;
            this.treeListColumn5.MinWidth = 150;
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.OptionsColumn.FixedWidth = true;
            this.treeListColumn5.OptionsColumn.ReadOnly = true;
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 3;
            this.treeListColumn5.Width = 150;
            // 
            // treeListColumn11
            // 
            this.treeListColumn11.Caption = "Ghi chú";
            this.treeListColumn11.FieldName = "Ghi chú";
            this.treeListColumn11.MaxWidth = 200;
            this.treeListColumn11.MinWidth = 200;
            this.treeListColumn11.Name = "treeListColumn11";
            this.treeListColumn11.OptionsColumn.FixedWidth = true;
            this.treeListColumn11.Visible = true;
            this.treeListColumn11.VisibleIndex = 4;
            this.treeListColumn11.Width = 200;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Dự án";
            this.treeListColumn4.FieldName = "TenDuAn";
            this.treeListColumn4.MaxWidth = 200;
            this.treeListColumn4.MinWidth = 200;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.FixedWidth = true;
            this.treeListColumn4.OptionsColumn.ReadOnly = true;
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 5;
            this.treeListColumn4.Width = 200;
            // 
            // tlCol_Duyet
            // 
            this.tlCol_Duyet.Caption = "Duyệt";
            this.tlCol_Duyet.ColumnEdit = this.repobt_Duyet;
            this.tlCol_Duyet.FieldName = "Duyệt";
            this.tlCol_Duyet.MaxWidth = 70;
            this.tlCol_Duyet.MinWidth = 70;
            this.tlCol_Duyet.Name = "tlCol_Duyet";
            this.tlCol_Duyet.OptionsColumn.FixedWidth = true;
            this.tlCol_Duyet.Visible = true;
            this.tlCol_Duyet.VisibleIndex = 6;
            this.tlCol_Duyet.Width = 70;
            // 
            // repobt_Duyet
            // 
            this.repobt_Duyet.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.repobt_Duyet.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repobt_Duyet.Name = "repobt_Duyet";
            this.repobt_Duyet.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repobt_Duyet.Click += new System.EventHandler(this.repobt_Duyet_Click);
            // 
            // xtraTabPage_DaDuyet
            // 
            this.xtraTabPage_DaDuyet.Controls.Add(this.tl_DaDuyet);
            this.xtraTabPage_DaDuyet.Name = "xtraTabPage_DaDuyet";
            this.xtraTabPage_DaDuyet.Size = new System.Drawing.Size(873, 452);
            this.xtraTabPage_DaDuyet.Text = "Đã duyệt";
            // 
            // tl_DaDuyet
            // 
            this.tl_DaDuyet.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn6,
            this.treeListColumn7,
            this.treeListColumn8,
            this.treeListColumn9,
            this.treeListColumn10});
            this.tl_DaDuyet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tl_DaDuyet.Location = new System.Drawing.Point(0, 0);
            this.tl_DaDuyet.Name = "tl_DaDuyet";
            this.tl_DaDuyet.OptionsBehavior.ReadOnly = true;
            this.tl_DaDuyet.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit2});
            this.tl_DaDuyet.Size = new System.Drawing.Size(873, 452);
            this.tl_DaDuyet.TabIndex = 1;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "STT";
            this.treeListColumn6.FieldName = "STT";
            this.treeListColumn6.MaxWidth = 50;
            this.treeListColumn6.MinWidth = 50;
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.OptionsColumn.FixedWidth = true;
            this.treeListColumn6.Visible = true;
            this.treeListColumn6.VisibleIndex = 0;
            this.treeListColumn6.Width = 50;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Mã hiệu";
            this.treeListColumn7.FieldName = "MaCongTac";
            this.treeListColumn7.MaxWidth = 100;
            this.treeListColumn7.MinWidth = 100;
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 1;
            this.treeListColumn7.Width = 100;
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.Caption = "Công tác";
            this.treeListColumn8.ColumnEdit = this.repositoryItemMemoEdit2;
            this.treeListColumn8.FieldName = "TenCongTac";
            this.treeListColumn8.Name = "treeListColumn8";
            this.treeListColumn8.Visible = true;
            this.treeListColumn8.VisibleIndex = 2;
            this.treeListColumn8.Width = 372;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // treeListColumn9
            // 
            this.treeListColumn9.Caption = "Khối lượng";
            this.treeListColumn9.FieldName = "KhoiLuongKeHoach";
            this.treeListColumn9.Format.FormatString = "n2";
            this.treeListColumn9.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn9.MaxWidth = 150;
            this.treeListColumn9.MinWidth = 150;
            this.treeListColumn9.Name = "treeListColumn9";
            this.treeListColumn9.Visible = true;
            this.treeListColumn9.VisibleIndex = 3;
            this.treeListColumn9.Width = 150;
            // 
            // treeListColumn10
            // 
            this.treeListColumn10.Caption = "Dự án";
            this.treeListColumn10.FieldName = "TenDuAn";
            this.treeListColumn10.MaxWidth = 200;
            this.treeListColumn10.MinWidth = 200;
            this.treeListColumn10.Name = "treeListColumn10";
            this.treeListColumn10.Visible = true;
            this.treeListColumn10.VisibleIndex = 4;
            this.treeListColumn10.Width = 200;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbb_ChonQuyTrinh);
            this.layoutControl1.Controls.Add(this.xtraTabControl_Approval);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(899, 525);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbb_ChonQuyTrinh
            // 
            this.cbb_ChonQuyTrinh.Location = new System.Drawing.Point(95, 12);
            this.cbb_ChonQuyTrinh.Name = "cbb_ChonQuyTrinh";
            this.cbb_ChonQuyTrinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbb_ChonQuyTrinh.Size = new System.Drawing.Size(792, 20);
            this.cbb_ChonQuyTrinh.StyleController = this.layoutControl1;
            this.cbb_ChonQuyTrinh.TabIndex = 5;
            this.cbb_ChonQuyTrinh.SelectedIndexChanged += new System.EventHandler(this.cbb_ChonQuyTrinh_SelectedIndexChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(899, 525);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraTabControl_Approval;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(879, 481);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cbb_ChonQuyTrinh;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(879, 24);
            this.layoutControlItem2.Text = "Chọn quy trình";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(71, 13);
            // 
            // uc_ApprovalDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "uc_ApprovalDetails";
            this.Size = new System.Drawing.Size(899, 525);
            this.Load += new System.EventHandler(this.uc_ApprovalDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl_Approval)).EndInit();
            this.xtraTabControl_Approval.ResumeLayout(false);
            this.xtraTabPage_DoiDuyet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_DoiDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_Duyet)).EndInit();
            this.xtraTabPage_DaDuyet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_DaDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbb_ChonQuyTrinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl_Approval;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage_DoiDuyet;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage_DaDuyet;
        private DevExpress.XtraTreeList.TreeList tl_DoiDuyet;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraTreeList.TreeList tl_DaDuyet;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn11;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlCol_Duyet;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repobt_Duyet;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.ComboBoxEdit cbb_ChonQuyTrinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
