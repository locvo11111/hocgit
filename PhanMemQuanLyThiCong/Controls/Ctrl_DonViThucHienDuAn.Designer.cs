namespace PhanMemQuanLyThiCong.Controls
{
    partial class Ctrl_DonViThucHienDuAn
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
            this.slke = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn68 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn69 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.slke.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // slke
            // 
            this.slke.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slke.EditValue = "Chọn đơn vị thực hiện";
            this.slke.Location = new System.Drawing.Point(0, 0);
            this.slke.Name = "slke";
            this.slke.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slke.Properties.DisplayMember = "TenGhep";
            this.slke.Properties.NullText = "";
            this.slke.Properties.PopupView = this.searchLookUpEdit1View;
            this.slke.Properties.ValueMember = "CodeFk";
            this.slke.Size = new System.Drawing.Size(277, 20);
            this.slke.TabIndex = 80;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn68,
            this.gridColumn69,
            this.gridColumn1});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.GroupCount = 2;
            this.searchLookUpEdit1View.GroupFormat = "[#image]{1} {2}";
            this.searchLookUpEdit1View.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "LoaiDonVi", null, "")});
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsBehavior.AutoExpandAllGroups = true;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn69, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn68
            // 
            this.gridColumn68.Caption = "Tên đơn vị";
            this.gridColumn68.FieldName = "Ten";
            this.gridColumn68.Name = "gridColumn68";
            this.gridColumn68.Visible = true;
            this.gridColumn68.VisibleIndex = 0;
            // 
            // gridColumn69
            // 
            this.gridColumn69.Caption = "Loại DVTH";
            this.gridColumn69.FieldName = "LoaiDVTH";
            this.gridColumn69.Name = "gridColumn69";
            this.gridColumn69.Visible = true;
            this.gridColumn69.VisibleIndex = 1;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Phân loại";
            this.gridColumn1.FieldName = "LoaiGiaoNhanThau";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // Ctrl_DonViThucHienDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.slke);
            this.MaximumSize = new System.Drawing.Size(1000, 20);
            this.MinimumSize = new System.Drawing.Size(0, 20);
            this.Name = "Ctrl_DonViThucHienDuAn";
            this.Size = new System.Drawing.Size(277, 20);
            ((System.ComponentModel.ISupportInitialize)(this.slke.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SearchLookUpEdit slke;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn68;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn69;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}
