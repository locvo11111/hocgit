namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    partial class GridTenGiaTri
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
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule1 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleDataBar formatConditionRuleDataBar1 = new DevExpress.XtraEditors.FormatConditionRuleDataBar();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule2 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule3 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_TienNhaThau = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tl_TienNhaThau)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Giá trị";
            this.treeListColumn2.FieldName = "GiaTri";
            this.treeListColumn2.Format.FormatString = "n2";
            this.treeListColumn2.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            // 
            // tl_TienNhaThau
            // 
            this.tl_TienNhaThau.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.tl_TienNhaThau.Dock = System.Windows.Forms.DockStyle.Fill;
            treeListFormatRule1.Column = this.treeListColumn2;
            treeListFormatRule1.Name = "Format0";
            formatConditionRuleDataBar1.PredefinedName = "Green";
            treeListFormatRule1.Rule = formatConditionRuleDataBar1;
            treeListFormatRule2.Column = this.treeListColumn2;
            treeListFormatRule2.Name = "Format1";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue1.PredefinedName = "Red Bold Text";
            formatConditionRuleValue1.Value1 = "0";
            treeListFormatRule2.Rule = formatConditionRuleValue1;
            treeListFormatRule3.Column = this.treeListColumn2;
            treeListFormatRule3.Name = "Format2";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue2.PredefinedName = "Green Bold Text";
            formatConditionRuleValue2.Value1 = "0";
            treeListFormatRule3.Rule = formatConditionRuleValue2;
            this.tl_TienNhaThau.FormatRules.Add(treeListFormatRule1);
            this.tl_TienNhaThau.FormatRules.Add(treeListFormatRule2);
            this.tl_TienNhaThau.FormatRules.Add(treeListFormatRule3);
            this.tl_TienNhaThau.KeyFieldName = "Code";
            this.tl_TienNhaThau.Location = new System.Drawing.Point(0, 0);
            this.tl_TienNhaThau.Name = "tl_TienNhaThau";
            this.tl_TienNhaThau.ParentFieldName = "ParentCode";
            this.tl_TienNhaThau.Size = new System.Drawing.Size(739, 419);
            this.tl_TienNhaThau.TabIndex = 1;
            this.tl_TienNhaThau.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tl_TienNhaThau_NodeCellStyle);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Tên công tác";
            this.treeListColumn1.FieldName = "TenCongTac";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // GridTenGiaTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tl_TienNhaThau);
            this.Name = "GridTenGiaTri";
            this.Size = new System.Drawing.Size(739, 419);
            ((System.ComponentModel.ISupportInitialize)(this.tl_TienNhaThau)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tl_TienNhaThau;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
    }
}
