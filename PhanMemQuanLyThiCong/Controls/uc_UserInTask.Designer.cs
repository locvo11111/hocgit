namespace PhanMemQuanLyThiCong.Controls
{
    partial class uc_UserInTask
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lb_notice = new DevExpress.XtraEditors.LabelControl();
            this.sle_Users = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tl_NhomQuyen = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repoCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.tree_add = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tree_edit = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tree_delete = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.sle_DVTH = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lke_LoaiCongTac = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lci_DonViThucHien = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sle_Users.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tl_NhomQuyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sle_DVTH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lke_LoaiCongTac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lci_DonViThucHien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lb_notice);
            this.layoutControl1.Controls.Add(this.sle_Users);
            this.layoutControl1.Controls.Add(this.tl_NhomQuyen);
            this.layoutControl1.Controls.Add(this.sle_DVTH);
            this.layoutControl1.Controls.Add(this.lke_LoaiCongTac);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1083, 575);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lb_notice
            // 
            this.lb_notice.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lb_notice.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lb_notice.Appearance.Options.UseFont = true;
            this.lb_notice.Appearance.Options.UseForeColor = true;
            this.lb_notice.Location = new System.Drawing.Point(12, 550);
            this.lb_notice.Name = "lb_notice";
            this.lb_notice.Size = new System.Drawing.Size(75, 13);
            this.lb_notice.StyleController = this.layoutControl1;
            this.lb_notice.TabIndex = 30;
            this.lb_notice.Text = "labelControl1";
            // 
            // sle_Users
            // 
            this.sle_Users.EditValue = "[EditValxue is null]";
            this.sle_Users.Location = new System.Drawing.Point(106, 60);
            this.sle_Users.Name = "sle_Users";
            this.sle_Users.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sle_Users.Properties.DisplayMember = "FullName";
            this.sle_Users.Properties.NullText = "[Chọn người dùng]";
            this.sle_Users.Properties.PopupView = this.gridView1;
            this.sle_Users.Size = new System.Drawing.Size(965, 20);
            this.sle_Users.StyleController = this.layoutControl1;
            this.sle_Users.TabIndex = 28;
            this.sle_Users.EditValueChanged += new System.EventHandler(this.sle_Users_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Họ và tên";
            this.gridColumn11.FieldName = "FullName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Email";
            this.gridColumn12.FieldName = "Email";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 1;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Số điện thoại";
            this.gridColumn13.FieldName = "PhoneNumber";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            // 
            // tl_NhomQuyen
            // 
            this.tl_NhomQuyen.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.tree_add,
            this.tree_edit,
            this.tree_delete,
            this.treeListColumn3});
            this.tl_NhomQuyen.KeyFieldName = "Id";
            this.tl_NhomQuyen.Location = new System.Drawing.Point(12, 100);
            this.tl_NhomQuyen.Name = "tl_NhomQuyen";
            this.tl_NhomQuyen.OptionsBehavior.PopulateServiceColumns = true;
            this.tl_NhomQuyen.OptionsView.AutoWidth = false;
            this.tl_NhomQuyen.ParentFieldName = "ParentId";
            this.tl_NhomQuyen.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoCheckEdit,
            this.repositoryItemMemoEdit1});
            this.tl_NhomQuyen.Size = new System.Drawing.Size(1059, 446);
            this.tl_NhomQuyen.TabIndex = 7;
            this.tl_NhomQuyen.CellValueChanging += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.tl_NhomQuyen_CellValueChanging);
            this.tl_NhomQuyen.DataSourceChanged += new System.EventHandler(this.tl_NhomQuyen_DataSourceChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Công tác";
            this.treeListColumn1.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn1.FieldName = "Name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 455;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Xem/Theo dõi";
            this.treeListColumn2.ColumnEdit = this.repoCheckEdit;
            this.treeListColumn2.FieldName = "View";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 85;
            // 
            // repoCheckEdit
            // 
            this.repoCheckEdit.AutoHeight = false;
            this.repoCheckEdit.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            this.repoCheckEdit.CheckBoxOptions.SvgColorChecked = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.repoCheckEdit.CheckBoxOptions.SvgColorUnchecked = DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            this.repoCheckEdit.Name = "repoCheckEdit";
            // 
            // tree_add
            // 
            this.tree_add.Caption = "Thêm";
            this.tree_add.ColumnEdit = this.repoCheckEdit;
            this.tree_add.FieldName = "Add";
            this.tree_add.Name = "tree_add";
            this.tree_add.Visible = true;
            this.tree_add.VisibleIndex = 2;
            // 
            // tree_edit
            // 
            this.tree_edit.Caption = "Sửa/Thực hiện";
            this.tree_edit.ColumnEdit = this.repoCheckEdit;
            this.tree_edit.FieldName = "Edit";
            this.tree_edit.Name = "tree_edit";
            this.tree_edit.Visible = true;
            this.tree_edit.VisibleIndex = 3;
            this.tree_edit.Width = 107;
            // 
            // tree_delete
            // 
            this.tree_delete.Caption = "Xóa";
            this.tree_delete.ColumnEdit = this.repoCheckEdit;
            this.tree_delete.FieldName = "Delete";
            this.tree_delete.Name = "tree_delete";
            this.tree_delete.Visible = true;
            this.tree_delete.VisibleIndex = 4;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Duyệt";
            this.treeListColumn3.ColumnEdit = this.repoCheckEdit;
            this.treeListColumn3.FieldName = "Approve";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 5;
            // 
            // sle_DVTH
            // 
            this.sle_DVTH.Location = new System.Drawing.Point(106, 36);
            this.sle_DVTH.Name = "sle_DVTH";
            this.sle_DVTH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sle_DVTH.Properties.DisplayMember = "Ten";
            this.sle_DVTH.Properties.NullText = "[Chọn đơn vị thực hiện]";
            this.sle_DVTH.Properties.PopupView = this.searchLookUpEdit1View;
            this.sle_DVTH.Size = new System.Drawing.Size(965, 20);
            this.sle_DVTH.StyleController = this.layoutControl1;
            this.sle_DVTH.TabIndex = 6;
            this.sle_DVTH.Popup += new System.EventHandler(this.sle_DVTH_Popup);
            this.sle_DVTH.EditValueChanged += new System.EventHandler(this.sle_DVTH_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.GroupCount = 1;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn10, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Tên";
            this.gridColumn8.FieldName = "Ten";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Mã số thuế";
            this.gridColumn9.FieldName = "MaSoThue";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Phân loại";
            this.gridColumn10.FieldName = "DisplayType";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            // 
            // lke_LoaiCongTac
            // 
            this.lke_LoaiCongTac.Location = new System.Drawing.Point(106, 12);
            this.lke_LoaiCongTac.Name = "lke_LoaiCongTac";
            this.lke_LoaiCongTac.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lke_LoaiCongTac.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Tên"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Mô tả")});
            this.lke_LoaiCongTac.Properties.DisplayMember = "Name";
            this.lke_LoaiCongTac.Properties.NullText = "[Chọn công tác]";
            this.lke_LoaiCongTac.Properties.PopupSizeable = false;
            this.lke_LoaiCongTac.Size = new System.Drawing.Size(965, 20);
            this.lke_LoaiCongTac.StyleController = this.layoutControl1;
            this.lke_LoaiCongTac.TabIndex = 4;
            this.lke_LoaiCongTac.EditValueChanged += new System.EventHandler(this.lke_LoaiCongTac_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lci_DonViThucHien,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1083, 575);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lke_LoaiCongTac;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1063, 24);
            this.layoutControlItem1.Text = "Loại công tác";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(82, 13);
            // 
            // lci_DonViThucHien
            // 
            this.lci_DonViThucHien.Control = this.sle_DVTH;
            this.lci_DonViThucHien.Location = new System.Drawing.Point(0, 24);
            this.lci_DonViThucHien.Name = "lci_DonViThucHien";
            this.lci_DonViThucHien.Size = new System.Drawing.Size(1063, 24);
            this.lci_DonViThucHien.Text = "Đơn vị thực hiện";
            this.lci_DonViThucHien.TextSize = new System.Drawing.Size(82, 13);
            this.lci_DonViThucHien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseBackColor = true;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.tl_NhomQuyen;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1063, 466);
            this.layoutControlItem3.Text = "Phân quyền";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(82, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sle_Users;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1063, 24);
            this.layoutControlItem2.Text = "Chọn người dùng";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(82, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lb_notice;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 538);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1063, 17);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // uc_UserInTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "uc_UserInTask";
            this.Size = new System.Drawing.Size(1083, 575);
            this.Load += new System.EventHandler(this.uc_UserInTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sle_Users.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tl_NhomQuyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sle_DVTH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lke_LoaiCongTac.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lci_DonViThucHien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SearchLookUpEdit sle_DVTH;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraLayout.LayoutControlItem lci_DonViThucHien;
        private DevExpress.XtraTreeList.TreeList tl_NhomQuyen;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repoCheckEdit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tree_add;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tree_edit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tree_delete;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LookUpEdit lke_LoaiCongTac;
        private DevExpress.XtraEditors.SearchLookUpEdit sle_Users;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.LabelControl lb_notice;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}
