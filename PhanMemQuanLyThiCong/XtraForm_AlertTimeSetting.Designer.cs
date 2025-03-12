namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_AlertTimeSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraForm_AlertTimeSetting));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.bt_refresh = new DevExpress.XtraEditors.SimpleButton();
            this.bt_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.bt_Save = new DevExpress.XtraEditors.SimpleButton();
            this.bt_Add = new DevExpress.XtraEditors.SimpleButton();
            this.tl_QuyTrinh = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repobt_Del = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gr_actionButton = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_QuyTrinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_Del)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gr_actionButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_refresh);
            this.layoutControl1.Controls.Add(this.bt_cancel);
            this.layoutControl1.Controls.Add(this.bt_Save);
            this.layoutControl1.Controls.Add(this.bt_Add);
            this.layoutControl1.Controls.Add(this.tl_QuyTrinh);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(448, 384);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bt_refresh
            // 
            this.bt_refresh.Appearance.BackColor = System.Drawing.Color.Lime;
            this.bt_refresh.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_refresh.Appearance.Options.UseBackColor = true;
            this.bt_refresh.Appearance.Options.UseFont = true;
            this.bt_refresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_refresh.ImageOptions.Image")));
            this.bt_refresh.Location = new System.Drawing.Point(5, 5);
            this.bt_refresh.MaximumSize = new System.Drawing.Size(250, 0);
            this.bt_refresh.Name = "bt_refresh";
            this.bt_refresh.Size = new System.Drawing.Size(75, 36);
            this.bt_refresh.StyleController = this.layoutControl1;
            this.bt_refresh.TabIndex = 12;
            this.bt_refresh.Text = "Tải lại";
            this.bt_refresh.Click += new System.EventHandler(this.bt_refresh_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Appearance.BackColor = System.Drawing.Color.Red;
            this.bt_cancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bt_cancel.Appearance.Options.UseBackColor = true;
            this.bt_cancel.Appearance.Options.UseFont = true;
            this.bt_cancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_cancel.ImageOptions.Image")));
            this.bt_cancel.Location = new System.Drawing.Point(345, 337);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(92, 36);
            this.bt_cancel.StyleController = this.layoutControl1;
            this.bt_cancel.TabIndex = 9;
            this.bt_cancel.Text = "Hủy bỏ";
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // bt_Save
            // 
            this.bt_Save.Appearance.BackColor = System.Drawing.Color.Cyan;
            this.bt_Save.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bt_Save.Appearance.Options.UseBackColor = true;
            this.bt_Save.Appearance.Options.UseFont = true;
            this.bt_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_Save.ImageOptions.Image")));
            this.bt_Save.Location = new System.Drawing.Point(254, 337);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(87, 36);
            this.bt_Save.StyleController = this.layoutControl1;
            this.bt_Save.TabIndex = 8;
            this.bt_Save.Text = "Lưu lại";
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // bt_Add
            // 
            this.bt_Add.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.bt_Add.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bt_Add.Appearance.Options.UseBackColor = true;
            this.bt_Add.Appearance.Options.UseFont = true;
            this.bt_Add.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_Add.ImageOptions.Image")));
            this.bt_Add.Location = new System.Drawing.Point(11, 337);
            this.bt_Add.Name = "bt_Add";
            this.bt_Add.Size = new System.Drawing.Size(145, 36);
            this.bt_Add.StyleController = this.layoutControl1;
            this.bt_Add.TabIndex = 7;
            this.bt_Add.Text = "Thêm thời gian";
            this.bt_Add.Click += new System.EventHandler(this.bt_Add_Click);
            // 
            // tl_QuyTrinh
            // 
            this.tl_QuyTrinh.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tl_QuyTrinh.Appearance.HeaderPanel.Options.UseFont = true;
            this.tl_QuyTrinh.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.tl_QuyTrinh.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tl_QuyTrinh.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn4,
            this.treeListColumn5});
            this.tl_QuyTrinh.Location = new System.Drawing.Point(5, 45);
            this.tl_QuyTrinh.Name = "tl_QuyTrinh";
            this.tl_QuyTrinh.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.tl_QuyTrinh.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repobt_Del,
            this.repositoryItemTimeEdit1});
            this.tl_QuyTrinh.Size = new System.Drawing.Size(438, 282);
            this.tl_QuyTrinh.TabIndex = 6;
            this.tl_QuyTrinh.InitNewRow += new DevExpress.XtraTreeList.TreeListInitNewRowEventHandler(this.tl_QuyTrinh_InitNewRow);
            this.tl_QuyTrinh.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.tl_QuyTrinh_ValidatingEditor);
            this.tl_QuyTrinh.ShownEditor += new System.EventHandler(this.tl_QuyTrinh_ShownEditor);
            this.tl_QuyTrinh.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.tl_QuyTrinh_ShowingEditor);
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
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Thời gian";
            this.treeListColumn4.FieldName = "Time";
            this.treeListColumn4.MinWidth = 95;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 1;
            this.treeListColumn4.Width = 95;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Xóa";
            this.treeListColumn5.ColumnEdit = this.repobt_Del;
            this.treeListColumn5.FieldName = "Xóa";
            this.treeListColumn5.MaxWidth = 100;
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 2;
            // 
            // repobt_Del
            // 
            this.repobt_Del.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.repobt_Del.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repobt_Del.Name = "repobt_Del";
            this.repobt_Del.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repobt_Del.Click += new System.EventHandler(this.repobt_Del_Click);
            // 
            // repositoryItemTimeEdit1
            // 
            this.repositoryItemTimeEdit1.AutoHeight = false;
            this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.gr_actionButton,
            this.layoutControlItem2,
            this.emptySpaceItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.Root.Size = new System.Drawing.Size(448, 384);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tl_QuyTrinh;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(442, 286);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // gr_actionButton
            // 
            this.gr_actionButton.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.gr_actionButton.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem6});
            this.gr_actionButton.Location = new System.Drawing.Point(0, 326);
            this.gr_actionButton.Name = "gr_actionButton";
            this.gr_actionButton.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.gr_actionButton.Size = new System.Drawing.Size(442, 52);
            this.gr_actionButton.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(149, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(94, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.bt_Save;
            this.layoutControlItem5.Location = new System.Drawing.Point(243, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(91, 40);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.bt_Add;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(149, 40);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.bt_cancel;
            this.layoutControlItem6.Location = new System.Drawing.Point(334, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(96, 40);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.bt_refresh;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(79, 40);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(79, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(363, 40);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XtraForm_AlertTimeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 384);
            this.Controls.Add(this.layoutControl1);
            this.MaximumSize = new System.Drawing.Size(450, 1000);
            this.Name = "XtraForm_AlertTimeSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thời gian thông báo hàng ngày";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_QuyTrinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_Del)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gr_actionButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton bt_refresh;
        private DevExpress.XtraEditors.SimpleButton bt_cancel;
        private DevExpress.XtraEditors.SimpleButton bt_Save;
        private DevExpress.XtraEditors.SimpleButton bt_Add;
        private DevExpress.XtraTreeList.TreeList tl_QuyTrinh;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repobt_Del;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup gr_actionButton;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}