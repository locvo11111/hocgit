namespace PhanMemQuanLyThiCong.Controls.DrainageControls.CumDuAn
{
    partial class uc_CumDuAn
    {
        /// <summary> 
        /// designer variable.
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
        /// method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.bt_refresh = new DevExpress.XtraEditors.SimpleButton();
            this.bt_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.bt_Add = new DevExpress.XtraEditors.SimpleButton();
            this.tl_data = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.col_Action = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_refresh);
            this.layoutControl1.Controls.Add(this.bt_cancel);
            this.layoutControl1.Controls.Add(this.bt_Add);
            this.layoutControl1.Controls.Add(this.tl_data);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(755, 361);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bt_refresh
            // 
            this.bt_refresh.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.bt_refresh.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bt_refresh.Appearance.Options.UseBackColor = true;
            this.bt_refresh.Appearance.Options.UseFont = true;
            this.bt_refresh.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.bt_refresh.ImageOptions.SvgImage = global::PhanMemQuanLyThiCong.Properties.Resources.actions_refresh;
            this.bt_refresh.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.bt_refresh.Location = new System.Drawing.Point(409, 329);
            this.bt_refresh.Name = "bt_refresh";
            this.bt_refresh.Size = new System.Drawing.Size(73, 28);
            this.bt_refresh.StyleController = this.layoutControl1;
            this.bt_refresh.TabIndex = 23;
            this.bt_refresh.Text = "Tải lại";
            this.bt_refresh.Click += new System.EventHandler(this.bt_refresh_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bt_cancel.Appearance.Options.UseFont = true;
            this.bt_cancel.ImageOptions.SvgImage = global::PhanMemQuanLyThiCong.Properties.Resources.actions_deletecircled;
            this.bt_cancel.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.bt_cancel.Location = new System.Drawing.Point(656, 329);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(95, 28);
            this.bt_cancel.StyleController = this.layoutControl1;
            this.bt_cancel.TabIndex = 14;
            this.bt_cancel.Text = "Hủy bỏ";
            // 
            // bt_Add
            // 
            this.bt_Add.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.bt_Add.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bt_Add.Appearance.Options.UseBackColor = true;
            this.bt_Add.Appearance.Options.UseFont = true;
            this.bt_Add.ImageOptions.SvgImage = global::PhanMemQuanLyThiCong.Properties.Resources.actions_add;
            this.bt_Add.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.bt_Add.Location = new System.Drawing.Point(486, 329);
            this.bt_Add.Name = "bt_Add";
            this.bt_Add.Size = new System.Drawing.Size(166, 28);
            this.bt_Add.StyleController = this.layoutControl1;
            this.bt_Add.TabIndex = 12;
            this.bt_Add.Text = "Thêm cụm dự án";
            this.bt_Add.Click += new System.EventHandler(this.bt_Add_Click);
            // 
            // tl_data
            // 
            this.tl_data.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tl_data.Appearance.HeaderPanel.Options.UseFont = true;
            this.tl_data.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.tl_data.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tl_data.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.col_Action});
            this.tl_data.Location = new System.Drawing.Point(4, 4);
            this.tl_data.Name = "tl_data";
            this.tl_data.OptionsBehavior.ReadOnly = true;
            this.tl_data.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2});
            this.tl_data.Size = new System.Drawing.Size(747, 321);
            this.tl_data.TabIndex = 4;
            this.tl_data.Load += new System.EventHandler(this.uc_CumDuAn_Load);
            this.tl_data.DataSourceChanged += new System.EventHandler(this.tl_data_DataSourceChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "STT";
            this.treeListColumn1.FieldName = "STT";
            this.treeListColumn1.MaxWidth = 50;
            this.treeListColumn1.MinWidth = 50;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.FixedWidth = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 50;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Tên";
            this.treeListColumn2.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn2.FieldName = "Name";
            this.treeListColumn2.MaxWidth = 300;
            this.treeListColumn2.MinWidth = 300;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.FixedWidth = true;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 2;
            this.treeListColumn2.Width = 300;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Ghi chú";
            this.treeListColumn3.ColumnEdit = this.repositoryItemMemoEdit2;
            this.treeListColumn3.FieldName = "Description";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 3;
            this.treeListColumn3.Width = 281;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // col_Action
            // 
            this.col_Action.Caption = "Thao tác";
            this.col_Action.FieldName = "Thao tác";
            this.col_Action.MaxWidth = 75;
            this.col_Action.MinWidth = 75;
            this.col_Action.Name = "col_Action";
            this.col_Action.OptionsColumn.FixedWidth = true;
            this.col_Action.Visible = true;
            this.col_Action.VisibleIndex = 1;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(755, 361);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tl_data;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(751, 325);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.bt_Add;
            this.layoutControlItem5.Location = new System.Drawing.Point(482, 325);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(170, 0);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(170, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(170, 32);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_cancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(652, 325);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(99, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(99, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(99, 32);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 325);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(405, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.bt_refresh;
            this.layoutControlItem6.Location = new System.Drawing.Point(405, 325);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(77, 32);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(77, 32);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(77, 32);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // uc_CumDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "uc_CumDuAn";
            this.Size = new System.Drawing.Size(755, 361);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList tl_data;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton bt_cancel;
        private DevExpress.XtraEditors.SimpleButton bt_Add;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_Action;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.SimpleButton bt_refresh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
