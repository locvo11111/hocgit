namespace PhanMemQuanLyThiCong.Controls.PhanQuyen
{
    partial class uc_PhanCapQuanLy
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.bt_XapXep = new DevExpress.XtraEditors.SimpleButton();
            this.lb_notice = new DevExpress.XtraEditors.LabelControl();
            this.bt_ThemNhomVaoCapQuanTri = new DevExpress.XtraEditors.SimpleButton();
            this.bt_AddCapQuanTri = new DevExpress.XtraEditors.SimpleButton();
            this.tl_Role = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repobt_Xoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_Role)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_Xoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_XapXep);
            this.layoutControl1.Controls.Add(this.lb_notice);
            this.layoutControl1.Controls.Add(this.bt_ThemNhomVaoCapQuanTri);
            this.layoutControl1.Controls.Add(this.bt_AddCapQuanTri);
            this.layoutControl1.Controls.Add(this.tl_Role);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(989, 453);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bt_XapXep
            // 
            this.bt_XapXep.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.bt_XapXep.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_XapXep.Appearance.Options.UseBackColor = true;
            this.bt_XapXep.Appearance.Options.UseFont = true;
            this.bt_XapXep.Appearance.Options.UseTextOptions = true;
            this.bt_XapXep.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bt_XapXep.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.arrangegroups_16x16;
            this.bt_XapXep.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.bt_XapXep.Location = new System.Drawing.Point(12, 419);
            this.bt_XapXep.Margin = new System.Windows.Forms.Padding(1);
            this.bt_XapXep.Name = "bt_XapXep";
            this.bt_XapXep.Size = new System.Drawing.Size(178, 22);
            this.bt_XapXep.StyleController = this.layoutControl1;
            this.bt_XapXep.TabIndex = 32;
            this.bt_XapXep.Text = "Xắp xếp lại cấp quản trị";
            this.bt_XapXep.Click += new System.EventHandler(this.bt_XapXep_Click);
            // 
            // lb_notice
            // 
            this.lb_notice.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lb_notice.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lb_notice.Appearance.Options.UseFont = true;
            this.lb_notice.Appearance.Options.UseForeColor = true;
            this.lb_notice.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lb_notice.Location = new System.Drawing.Point(519, 419);
            this.lb_notice.Name = "lb_notice";
            this.lb_notice.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lb_notice.Size = new System.Drawing.Size(458, 13);
            this.lb_notice.StyleController = this.layoutControl1;
            this.lb_notice.TabIndex = 31;
            this.lb_notice.Text = "Bạn đang có quyền Admin";
            // 
            // bt_ThemNhomVaoCapQuanTri
            // 
            this.bt_ThemNhomVaoCapQuanTri.Appearance.BackColor = System.Drawing.Color.Blue;
            this.bt_ThemNhomVaoCapQuanTri.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_ThemNhomVaoCapQuanTri.Appearance.Options.UseBackColor = true;
            this.bt_ThemNhomVaoCapQuanTri.Appearance.Options.UseFont = true;
            this.bt_ThemNhomVaoCapQuanTri.Appearance.Options.UseTextOptions = true;
            this.bt_ThemNhomVaoCapQuanTri.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bt_ThemNhomVaoCapQuanTri.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.bt_ThemNhomVaoCapQuanTri.Location = new System.Drawing.Point(338, 419);
            this.bt_ThemNhomVaoCapQuanTri.Margin = new System.Windows.Forms.Padding(1);
            this.bt_ThemNhomVaoCapQuanTri.MaximumSize = new System.Drawing.Size(220, 0);
            this.bt_ThemNhomVaoCapQuanTri.Name = "bt_ThemNhomVaoCapQuanTri";
            this.bt_ThemNhomVaoCapQuanTri.Size = new System.Drawing.Size(177, 22);
            this.bt_ThemNhomVaoCapQuanTri.StyleController = this.layoutControl1;
            this.bt_ThemNhomVaoCapQuanTri.TabIndex = 6;
            this.bt_ThemNhomVaoCapQuanTri.Text = "Thêm Nhóm/Phòng Ban";
            this.bt_ThemNhomVaoCapQuanTri.Click += new System.EventHandler(this.bt_ThemNhomVaoCapQuanTri_Click);
            // 
            // bt_AddCapQuanTri
            // 
            this.bt_AddCapQuanTri.Appearance.BackColor = System.Drawing.Color.Green;
            this.bt_AddCapQuanTri.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_AddCapQuanTri.Appearance.Options.UseBackColor = true;
            this.bt_AddCapQuanTri.Appearance.Options.UseFont = true;
            this.bt_AddCapQuanTri.Appearance.Options.UseTextOptions = true;
            this.bt_AddCapQuanTri.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bt_AddCapQuanTri.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.addnewdatasource_16x16;
            this.bt_AddCapQuanTri.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.bt_AddCapQuanTri.Location = new System.Drawing.Point(194, 419);
            this.bt_AddCapQuanTri.Margin = new System.Windows.Forms.Padding(1);
            this.bt_AddCapQuanTri.MaximumSize = new System.Drawing.Size(170, 0);
            this.bt_AddCapQuanTri.Name = "bt_AddCapQuanTri";
            this.bt_AddCapQuanTri.Size = new System.Drawing.Size(140, 22);
            this.bt_AddCapQuanTri.StyleController = this.layoutControl1;
            this.bt_AddCapQuanTri.TabIndex = 5;
            this.bt_AddCapQuanTri.Text = "Thêm cấp quản lý";
            this.bt_AddCapQuanTri.Click += new System.EventHandler(this.bt_AddCapQuanTri_Click);
            // 
            // tl_Role
            // 
            this.tl_Role.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.colName,
            this.treeListColumn2});
            this.tl_Role.KeyFieldName = "Id";
            this.tl_Role.Location = new System.Drawing.Point(12, 12);
            this.tl_Role.Name = "tl_Role";
            this.tl_Role.ParentFieldName = "ParentId";
            this.tl_Role.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repobt_Xoa});
            this.tl_Role.Size = new System.Drawing.Size(965, 403);
            this.tl_Role.TabIndex = 4;
            this.tl_Role.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tl_Role_NodeCellStyle);
            this.tl_Role.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.tl_Role_ValidatingEditor);
            this.tl_Role.PopupMenuShowing += new DevExpress.XtraTreeList.PopupMenuShowingEventHandler(this.tl_Role_PopupMenuShowing);
            this.tl_Role.DataSourceChanged += new System.EventHandler(this.tl_Role_DataSourceChanged);
            this.tl_Role.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tl_Role_MouseDown);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Cấp quản trị";
            this.treeListColumn1.FieldName = "LevelString";
            this.treeListColumn1.MaxWidth = 100;
            this.treeListColumn1.MinWidth = 100;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 1;
            this.treeListColumn1.Width = 100;
            // 
            // colName
            // 
            this.colName.Caption = "Tên chi tiết";
            this.colName.FieldName = "Ten";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 790;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Xóa";
            this.treeListColumn2.ColumnEdit = this.repobt_Xoa;
            this.treeListColumn2.FieldName = "Xóa";
            this.treeListColumn2.MaxWidth = 50;
            this.treeListColumn2.MinWidth = 50;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 2;
            this.treeListColumn2.Width = 50;
            // 
            // repobt_Xoa
            // 
            this.repobt_Xoa.AutoHeight = false;
            editorButtonImageOptions1.Image = global::PhanMemQuanLyThiCong.Properties.Resources.cancel_16x1610;
            this.repobt_Xoa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repobt_Xoa.Name = "repobt_Xoa";
            this.repobt_Xoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repobt_Xoa.Click += new System.EventHandler(this.repobt_Xoa_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(989, 453);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tl_Role;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(969, 407);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_ThemNhomVaoCapQuanTri;
            this.layoutControlItem3.Location = new System.Drawing.Point(326, 407);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(181, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.bt_AddCapQuanTri;
            this.layoutControlItem2.Location = new System.Drawing.Point(182, 407);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(144, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lb_notice;
            this.layoutControlItem4.Location = new System.Drawing.Point(507, 407);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(462, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.bt_XapXep;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 407);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(182, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // uc_PhanCapQuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "uc_PhanCapQuanLy";
            this.Size = new System.Drawing.Size(989, 453);
            this.Load += new System.EventHandler(this.uc_PhanCapQuanLy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_Role)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repobt_Xoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList tl_Role;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton bt_AddCapQuanTri;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton bt_ThemNhomVaoCapQuanTri;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lb_notice;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton bt_XapXep;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repobt_Xoa;
    }
}
