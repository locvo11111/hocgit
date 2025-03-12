namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_LayHaoPhiMacDinh
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
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule1 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule2 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.bt_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.bt_UpDate = new DevExpress.XtraEditors.SimpleButton();
            this.tl_CongTac = new DevExpress.XtraTreeList.TreeList();
            this.col_MaHieu = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_TenCongTac = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rg_CongTac = new DevExpress.XtraEditors.RadioGroup();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_CongTac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_CongTac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_Huy);
            this.layoutControl1.Controls.Add(this.bt_UpDate);
            this.layoutControl1.Controls.Add(this.tl_CongTac);
            this.layoutControl1.Controls.Add(this.rg_CongTac);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(938, 585);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bt_Huy
            // 
            this.bt_Huy.Appearance.BackColor = System.Drawing.Color.Red;
            this.bt_Huy.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bt_Huy.Appearance.Options.UseBackColor = true;
            this.bt_Huy.Appearance.Options.UseFont = true;
            this.bt_Huy.Location = new System.Drawing.Point(794, 551);
            this.bt_Huy.Name = "bt_Huy";
            this.bt_Huy.Size = new System.Drawing.Size(132, 22);
            this.bt_Huy.StyleController = this.layoutControl1;
            this.bt_Huy.TabIndex = 14;
            this.bt_Huy.Text = "Hủy";
            // 
            // bt_UpDate
            // 
            this.bt_UpDate.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bt_UpDate.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bt_UpDate.Appearance.Options.UseBackColor = true;
            this.bt_UpDate.Appearance.Options.UseFont = true;
            this.bt_UpDate.Location = new System.Drawing.Point(12, 551);
            this.bt_UpDate.Name = "bt_UpDate";
            this.bt_UpDate.Size = new System.Drawing.Size(778, 22);
            this.bt_UpDate.StyleController = this.layoutControl1;
            this.bt_UpDate.TabIndex = 13;
            this.bt_UpDate.Text = "Cập nhật";
            this.bt_UpDate.Click += new System.EventHandler(this.bt_UpDate_Click);
            // 
            // tl_CongTac
            // 
            this.tl_CongTac.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.col_MaHieu,
            this.col_TenCongTac,
            this.treeListColumn3,
            this.treeListColumn5,
            this.treeListColumn7});
            treeListFormatRule1.Name = "ChenhLechTC";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Appearance.Options.UseFont = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.NotEqual;
            formatConditionRuleValue1.Value1 = "0";
            treeListFormatRule1.Rule = formatConditionRuleValue1;
            treeListFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue2.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue2.Appearance.Options.UseFont = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.NotEqual;
            formatConditionRuleValue2.Value1 = "0";
            treeListFormatRule2.Rule = formatConditionRuleValue2;
            this.tl_CongTac.FormatRules.Add(treeListFormatRule1);
            this.tl_CongTac.FormatRules.Add(treeListFormatRule2);
            this.tl_CongTac.Location = new System.Drawing.Point(12, 66);
            this.tl_CongTac.Name = "tl_CongTac";
            this.tl_CongTac.OptionsBehavior.ReadOnly = true;
            this.tl_CongTac.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tl_CongTac.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.tl_CongTac.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFocus;
            this.tl_CongTac.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.tl_CongTac.Size = new System.Drawing.Size(914, 481);
            this.tl_CongTac.TabIndex = 9;
            // 
            // col_MaHieu
            // 
            this.col_MaHieu.Caption = "Mã hiệu công tác";
            this.col_MaHieu.FieldName = "MaHieuCongTac";
            this.col_MaHieu.MaxWidth = 110;
            this.col_MaHieu.MinWidth = 110;
            this.col_MaHieu.Name = "col_MaHieu";
            this.col_MaHieu.OptionsColumn.AllowSort = true;
            this.col_MaHieu.Visible = true;
            this.col_MaHieu.VisibleIndex = 0;
            this.col_MaHieu.Width = 110;
            // 
            // col_TenCongTac
            // 
            this.col_TenCongTac.Caption = "Tên công tác";
            this.col_TenCongTac.ColumnEdit = this.repositoryItemMemoEdit1;
            this.col_TenCongTac.FieldName = "TenCongTac";
            this.col_TenCongTac.Name = "col_TenCongTac";
            this.col_TenCongTac.OptionsColumn.AllowSort = true;
            this.col_TenCongTac.Visible = true;
            this.col_TenCongTac.VisibleIndex = 1;
            this.col_TenCongTac.Width = 309;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Đơn giá kế hoạch";
            this.treeListColumn3.FieldName = "DonGia";
            this.treeListColumn3.Format.FormatString = "n0";
            this.treeListColumn3.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn3.MaxWidth = 120;
            this.treeListColumn3.MinWidth = 120;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.OptionsColumn.AllowSort = true;
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 2;
            this.treeListColumn3.Width = 120;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Đơn giá thi công";
            this.treeListColumn5.FieldName = "DonGiaThiCong";
            this.treeListColumn5.Format.FormatString = "n0";
            this.treeListColumn5.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn5.MaxWidth = 120;
            this.treeListColumn5.MinWidth = 120;
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.OptionsColumn.AllowSort = true;
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 3;
            this.treeListColumn5.Width = 120;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Hạng mục";
            this.treeListColumn7.FieldName = "TenHangMuc";
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.OptionsColumn.AllowSort = true;
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 4;
            this.treeListColumn7.Width = 230;
            // 
            // rg_CongTac
            // 
            this.rg_CongTac.Location = new System.Drawing.Point(12, 28);
            this.rg_CongTac.Name = "rg_CongTac";
            this.rg_CongTac.Properties.Columns = 3;
            this.rg_CongTac.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Công tác chuột phải", true, null, "CongTacChuotPhai"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Công tác đã chọn", true, null, "CongTacDaChon"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Toàn bộ Hạng mục đã chọn", true, null, "AllHM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Toàn bộ công tác trong dự án", true, null, "All")});
            this.rg_CongTac.Size = new System.Drawing.Size(914, 34);
            this.rg_CongTac.StyleController = this.layoutControl1;
            this.rg_CongTac.TabIndex = 8;
            this.rg_CongTac.SelectedIndexChanged += new System.EventHandler(this.rg_CongTac_SelectedIndexChanged);
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
            this.Root.Size = new System.Drawing.Size(938, 585);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.rg_CongTac;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(918, 54);
            this.layoutControlItem1.Text = "Chọn công tác";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(79, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tl_CongTac;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(918, 485);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_UpDate;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 539);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(782, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.bt_Huy;
            this.layoutControlItem4.Location = new System.Drawing.Point(782, 539);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(136, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(136, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(136, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // XtraForm_LayHaoPhiMacDinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 585);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_LayHaoPhiMacDinh";
            this.Text = "Lấy hao phí mặc định";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_CongTac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_CongTac.Properties)).EndInit();
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
        private DevExpress.XtraEditors.RadioGroup rg_CongTac;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTreeList.TreeList tl_CongTac;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_MaHieu;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_TenCongTac;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton bt_UpDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton bt_Huy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}