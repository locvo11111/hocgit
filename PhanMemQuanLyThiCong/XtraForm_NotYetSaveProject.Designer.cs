namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_NotYetSaveProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraForm_NotYetSaveProject));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.bt_DeleteAll = new DevExpress.XtraEditors.SimpleButton();
            this.bt_Open = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tl_Unsaved = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repoBt_Xoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_Unsaved)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBt_Xoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_DeleteAll);
            this.layoutControl1.Controls.Add(this.bt_Open);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.tl_Unsaved);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1032, 437);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bt_DeleteAll
            // 
            this.bt_DeleteAll.Appearance.BackColor = System.Drawing.Color.Red;
            this.bt_DeleteAll.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_DeleteAll.Appearance.Options.UseBackColor = true;
            this.bt_DeleteAll.Appearance.Options.UseFont = true;
            this.bt_DeleteAll.Location = new System.Drawing.Point(727, 403);
            this.bt_DeleteAll.Name = "bt_DeleteAll";
            this.bt_DeleteAll.Size = new System.Drawing.Size(293, 22);
            this.bt_DeleteAll.StyleController = this.layoutControl1;
            this.bt_DeleteAll.TabIndex = 7;
            this.bt_DeleteAll.Text = "Xóa tất cả";
            this.bt_DeleteAll.Click += new System.EventHandler(this.bt_DeleteAll_Click);
            // 
            // bt_Open
            // 
            this.bt_Open.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bt_Open.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_Open.Appearance.Options.UseBackColor = true;
            this.bt_Open.Appearance.Options.UseFont = true;
            this.bt_Open.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_Open.ImageOptions.Image")));
            this.bt_Open.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.bt_Open.Location = new System.Drawing.Point(12, 403);
            this.bt_Open.Name = "bt_Open";
            this.bt_Open.Size = new System.Drawing.Size(711, 22);
            this.bt_Open.StyleController = this.layoutControl1;
            this.bt_Open.TabIndex = 6;
            this.bt_Open.Text = "Mở File";
            this.bt_Open.Click += new System.EventHandler(this.bt_Open_Click);
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
            this.labelControl1.Size = new System.Drawing.Size(1008, 19);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Các tổng dự án chưa được lưu lại";
            // 
            // tl_Unsaved
            // 
            this.tl_Unsaved.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4});
            this.tl_Unsaved.Location = new System.Drawing.Point(12, 35);
            this.tl_Unsaved.MinWidth = 30;
            this.tl_Unsaved.Name = "tl_Unsaved";
            this.tl_Unsaved.OptionsBehavior.ReadOnly = true;
            this.tl_Unsaved.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repoBt_Xoa});
            this.tl_Unsaved.Size = new System.Drawing.Size(1008, 364);
            this.tl_Unsaved.TabIndex = 4;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Tên File";
            this.treeListColumn1.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn1.FieldName = "FileName";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 1;
            this.treeListColumn1.Width = 765;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Thời gian";
            this.treeListColumn2.FieldName = "Modified";
            this.treeListColumn2.Format.FormatString = "g";
            this.treeListColumn2.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.treeListColumn2.MaxWidth = 150;
            this.treeListColumn2.MinWidth = 150;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.SortOrder = System.Windows.Forms.SortOrder.Descending;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 2;
            this.treeListColumn2.Width = 150;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "STT";
            this.treeListColumn3.FieldName = "STT";
            this.treeListColumn3.MaxWidth = 30;
            this.treeListColumn3.MinWidth = 30;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 0;
            this.treeListColumn3.Width = 30;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Xóa";
            this.treeListColumn4.ColumnEdit = this.repoBt_Xoa;
            this.treeListColumn4.FieldName = "Xóa";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 3;
            // 
            // repoBt_Xoa
            // 
            this.repoBt_Xoa.AutoHeight = false;
            editorButtonImageOptions1.Image = global::PhanMemQuanLyThiCong.Properties.Resources.cancel_16x1610;
            this.repoBt_Xoa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repoBt_Xoa.Name = "repoBt_Xoa";
            this.repoBt_Xoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repoBt_Xoa.Click += new System.EventHandler(this.repoBt_Xoa_Click);
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
            this.Root.Size = new System.Drawing.Size(1032, 437);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tl_Unsaved;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1012, 368);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.labelControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1012, 23);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_Open;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 391);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(715, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.bt_DeleteAll;
            this.layoutControlItem4.Location = new System.Drawing.Point(715, 391);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(297, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // XtraForm_NotYetSaveProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 437);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_NotYetSaveProject";
            this.Text = "Dự án chưa lưu";
            this.Load += new System.EventHandler(this.XtraForm_NotYetSaveProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_Unsaved)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBt_Xoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraTreeList.TreeList tl_Unsaved;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.SimpleButton bt_DeleteAll;
        private DevExpress.XtraEditors.SimpleButton bt_Open;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repoBt_Xoa;
    }
}