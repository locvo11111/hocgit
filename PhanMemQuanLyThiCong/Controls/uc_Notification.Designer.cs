namespace PhanMemQuanLyThiCong.Controls
{
    partial class uc_Notification
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_noti = new DevExpress.XtraGrid.GridControl();
            this.gv_noti = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_Type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn88 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridColumn_IsNewNoti = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn100 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn101 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView13 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rg_noti = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.gc_noti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_noti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_noti.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // colState
            // 
            this.colState.Caption = "Trạng thái";
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            // 
            // gc_noti
            // 
            this.gc_noti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_noti.Location = new System.Drawing.Point(0, 45);
            this.gc_noti.MainView = this.gv_noti;
            this.gc_noti.Name = "gc_noti";
            this.gc_noti.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit3});
            this.gc_noti.Size = new System.Drawing.Size(945, 492);
            this.gc_noti.TabIndex = 0;
            this.gc_noti.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_noti,
            this.gridView13});
            // 
            // gv_noti
            // 
            this.gv_noti.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_Type,
            this.gridColumn88,
            this.gridColumn_IsNewNoti,
            this.gridColumn100,
            this.colState,
            this.gridColumn101});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.colState;
            gridFormatRule1.Name = "FormatNewNoti";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = "NEW";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.gv_noti.FormatRules.Add(gridFormatRule1);
            this.gv_noti.GridControl = this.gc_noti;
            this.gv_noti.GroupFormat = "[#image]{1} {2}";
            this.gv_noti.Name = "gv_noti";
            this.gv_noti.OptionsBehavior.ReadOnly = true;
            this.gv_noti.OptionsScrollAnnotations.ShowFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.gv_noti.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gv_noti.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gv_noti.OptionsView.RowAutoHeight = true;
            this.gv_noti.OptionsView.ShowColumnHeaders = false;
            this.gv_noti.OptionsView.ShowGroupPanel = false;
            this.gv_noti.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn101, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // col_Type
            // 
            this.col_Type.Caption = "Phân loại";
            this.col_Type.FieldName = "Type";
            this.col_Type.Name = "col_Type";
            this.col_Type.Visible = true;
            this.col_Type.VisibleIndex = 2;
            // 
            // gridColumn88
            // 
            this.gridColumn88.Caption = "Nội dung";
            this.gridColumn88.ColumnEdit = this.repositoryItemMemoEdit3;
            this.gridColumn88.FieldName = "Content";
            this.gridColumn88.Name = "gridColumn88";
            this.gridColumn88.Visible = true;
            this.gridColumn88.VisibleIndex = 0;
            this.gridColumn88.Width = 236;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // gridColumn_IsNewNoti
            // 
            this.gridColumn_IsNewNoti.Caption = "gridColumn88";
            this.gridColumn_IsNewNoti.FieldName = "IsNewNoti";
            this.gridColumn_IsNewNoti.Name = "gridColumn_IsNewNoti";
            // 
            // gridColumn100
            // 
            this.gridColumn100.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn100.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn100.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn100.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn100.Caption = "Thời gian";
            this.gridColumn100.FieldName = "RelativeTime";
            this.gridColumn100.Name = "gridColumn100";
            this.gridColumn100.Visible = true;
            this.gridColumn100.VisibleIndex = 1;
            this.gridColumn100.Width = 90;
            // 
            // gridColumn101
            // 
            this.gridColumn101.Caption = "Thời gian tạo";
            this.gridColumn101.FieldName = "CreatedOn";
            this.gridColumn101.Name = "gridColumn101";
            this.gridColumn101.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            // 
            // gridView13
            // 
            this.gridView13.GridControl = this.gc_noti;
            this.gridView13.Name = "gridView13";
            // 
            // rg_noti
            // 
            this.rg_noti.Dock = System.Windows.Forms.DockStyle.Top;
            this.rg_noti.Location = new System.Drawing.Point(0, 0);
            this.rg_noti.Name = "rg_noti";
            this.rg_noti.Properties.Columns = 4;
            this.rg_noti.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả", true, null, "ALL"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Phân quyền", true, null, "0"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tiến độ", true, null, "1"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Duyệt/Gửi duyệt", true, null, "2")});
            this.rg_noti.Size = new System.Drawing.Size(945, 45);
            this.rg_noti.TabIndex = 1;
            this.rg_noti.SelectedIndexChanged += new System.EventHandler(this.rg_noti_SelectedIndexChanged);
            // 
            // uc_Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gc_noti);
            this.Controls.Add(this.rg_noti);
            this.Name = "uc_Notification";
            this.Size = new System.Drawing.Size(945, 537);
            ((System.ComponentModel.ISupportInitialize)(this.gc_noti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_noti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_noti.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gc_noti;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_noti;
        private DevExpress.XtraGrid.Columns.GridColumn col_Type;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn88;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_IsNewNoti;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn100;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn101;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView13;
        private DevExpress.XtraEditors.RadioGroup rg_noti;
    }
}
