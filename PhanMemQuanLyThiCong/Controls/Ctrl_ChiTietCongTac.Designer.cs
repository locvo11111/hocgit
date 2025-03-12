
namespace PhanMemQuanLyThiCong.Controls
{
    partial class Ctrl_ChiTietCongTac
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
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule2 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule3 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleDataBar formatConditionRuleDataBar1 = new DevExpress.XtraEditors.FormatConditionRuleDataBar();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_ChiTietCongTac = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tl_ChiTietCongTac)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Giá trị";
            this.treeListColumn3.FieldName = "GiaTri";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
            this.treeListColumn3.Width = 276;
            // 
            // tl_ChiTietCongTac
            // 
            this.tl_ChiTietCongTac.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3});
            this.tl_ChiTietCongTac.Dock = System.Windows.Forms.DockStyle.Fill;
            treeListFormatRule1.Column = this.treeListColumn3;
            treeListFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue1.PredefinedName = "Red Bold Text";
            formatConditionRuleValue1.Value1 = "0";
            treeListFormatRule1.Rule = formatConditionRuleValue1;
            treeListFormatRule2.Column = this.treeListColumn3;
            treeListFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue2.PredefinedName = "Green Bold Text";
            formatConditionRuleValue2.Value1 = "0";
            treeListFormatRule2.Rule = formatConditionRuleValue2;
            treeListFormatRule3.Column = this.treeListColumn3;
            treeListFormatRule3.Name = "Format2";
            formatConditionRuleDataBar1.PredefinedName = "Green";
            treeListFormatRule3.Rule = formatConditionRuleDataBar1;
            this.tl_ChiTietCongTac.FormatRules.Add(treeListFormatRule1);
            this.tl_ChiTietCongTac.FormatRules.Add(treeListFormatRule2);
            this.tl_ChiTietCongTac.FormatRules.Add(treeListFormatRule3);
            this.tl_ChiTietCongTac.KeyFieldName = "Code";
            this.tl_ChiTietCongTac.Location = new System.Drawing.Point(0, 0);
            this.tl_ChiTietCongTac.Name = "tl_ChiTietCongTac";
            this.tl_ChiTietCongTac.ParentFieldName = "ParentCode";
            this.tl_ChiTietCongTac.Size = new System.Drawing.Size(755, 602);
            this.tl_ChiTietCongTac.TabIndex = 0;
            this.tl_ChiTietCongTac.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tl_ChiTietCongTac_NodeCellStyle);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Code";
            this.treeListColumn1.FieldName = "Code";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Width = 94;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Tên công tác";
            this.treeListColumn2.FieldName = "TenCongTac";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 454;
            // 
            // Ctrl_ChiTietCongTac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tl_ChiTietCongTac);
            this.Name = "Ctrl_ChiTietCongTac";
            this.Size = new System.Drawing.Size(755, 602);
            ((System.ComponentModel.ISupportInitialize)(this.tl_ChiTietCongTac)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tl_ChiTietCongTac;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
    }
}
