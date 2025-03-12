namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_XapXepCapQuanTri
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraForm_XapXepCapQuanTri));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.tl_CapQuanTri = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repobt_moveDown = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repobt_moveup = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.bt_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.bt_OK = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.tl_CapQuanTri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_moveDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_moveup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // tl_CapQuanTri
            // 
            this.tl_CapQuanTri.Appearance.Caption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tl_CapQuanTri.Appearance.Caption.Options.UseFont = true;
            this.tl_CapQuanTri.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tl_CapQuanTri.Appearance.HeaderPanel.Options.UseFont = true;
            this.tl_CapQuanTri.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4});
            this.tl_CapQuanTri.Location = new System.Drawing.Point(5, 5);
            this.tl_CapQuanTri.Name = "tl_CapQuanTri";
            this.tl_CapQuanTri.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.tl_CapQuanTri.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Standard;
            this.tl_CapQuanTri.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repobt_moveDown,
            this.repobt_moveup});
            this.tl_CapQuanTri.Size = new System.Drawing.Size(896, 468);
            this.tl_CapQuanTri.TabIndex = 0;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Tên";
            this.treeListColumn1.FieldName = "Name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 351;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Cấp quản trị hiện tại";
            this.treeListColumn2.FieldName = "Level";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 350;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Chuyển xuống";
            this.treeListColumn3.ColumnEdit = this.repobt_moveDown;
            this.treeListColumn3.FieldName = "Chuyển xuống";
            this.treeListColumn3.MaxWidth = 90;
            this.treeListColumn3.MinWidth = 90;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 2;
            this.treeListColumn3.Width = 90;
            // 
            // repobt_moveDown
            // 
            this.repobt_moveDown.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.repobt_moveDown.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repobt_moveDown.Name = "repobt_moveDown";
            this.repobt_moveDown.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repobt_moveDown.Click += new System.EventHandler(this.repobt_moveDown_Click);
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Chuyển lên";
            this.treeListColumn4.ColumnEdit = this.repobt_moveup;
            this.treeListColumn4.FieldName = "Chuyển lên";
            this.treeListColumn4.MaxWidth = 90;
            this.treeListColumn4.MinWidth = 90;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 3;
            this.treeListColumn4.Width = 90;
            // 
            // repobt_moveup
            // 
            this.repobt_moveup.AutoHeight = false;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.repobt_moveup.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repobt_moveup.Name = "repobt_moveup";
            this.repobt_moveup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repobt_moveup.Click += new System.EventHandler(this.repobt_moveup_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_Huy);
            this.layoutControl1.Controls.Add(this.bt_OK);
            this.layoutControl1.Controls.Add(this.tl_CapQuanTri);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(906, 504);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bt_Huy
            // 
            this.bt_Huy.Appearance.BackColor = System.Drawing.Color.Red;
            this.bt_Huy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_Huy.Appearance.Options.UseBackColor = true;
            this.bt_Huy.Appearance.Options.UseFont = true;
            this.bt_Huy.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.cancel_16x1610;
            this.bt_Huy.Location = new System.Drawing.Point(829, 477);
            this.bt_Huy.Name = "bt_Huy";
            this.bt_Huy.Size = new System.Drawing.Size(72, 22);
            this.bt_Huy.StyleController = this.layoutControl1;
            this.bt_Huy.TabIndex = 5;
            this.bt_Huy.Text = "Hủy";
            // 
            // bt_OK
            // 
            this.bt_OK.Appearance.BackColor = System.Drawing.Color.Blue;
            this.bt_OK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_OK.Appearance.Options.UseBackColor = true;
            this.bt_OK.Appearance.Options.UseFont = true;
            this.bt_OK.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.apply_16x1610;
            this.bt_OK.Location = new System.Drawing.Point(730, 477);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(95, 22);
            this.bt_OK.StyleController = this.layoutControl1;
            this.bt_OK.TabIndex = 4;
            this.bt_OK.Text = "Hoàn tất";
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.Root.Size = new System.Drawing.Size(906, 504);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tl_CapQuanTri;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(900, 472);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.bt_OK;
            this.layoutControlItem2.Location = new System.Drawing.Point(725, 472);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(99, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 472);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(725, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_Huy;
            this.layoutControlItem3.Location = new System.Drawing.Point(824, 472);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // XtraForm_XapXepCapQuanTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 504);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_XapXepCapQuanTri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xắp xếp cấp quản trị";
            ((System.ComponentModel.ISupportInitialize)(this.tl_CapQuanTri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_moveDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_moveup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tl_CapQuanTri;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repobt_moveDown;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repobt_moveup;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton bt_Huy;
        private DevExpress.XtraEditors.SimpleButton bt_OK;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}