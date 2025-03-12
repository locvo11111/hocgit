
namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    partial class TongHopCongTacVatLieu
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
            this.TL_TongHop = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TL_TongHop)).BeginInit();
            this.SuspendLayout();
            // 
            // TL_TongHop
            // 
            this.TL_TongHop.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3});
            this.TL_TongHop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TL_TongHop.Location = new System.Drawing.Point(0, 0);
            this.TL_TongHop.Name = "TL_TongHop";
            this.TL_TongHop.OptionsPrint.UsePrintStyles = false;
            this.TL_TongHop.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.TL_TongHop.Size = new System.Drawing.Size(343, 386);
            this.TL_TongHop.TabIndex = 0;
            this.TL_TongHop.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.TL_TongHop_NodeCellStyle);
            this.TL_TongHop.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.TL_TongHop_CustomDrawNodeCell);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.treeListColumn1.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn1.Caption = "Tên nhà thầu/Chi tiết";
            this.treeListColumn1.FieldName = "TenNhaThau";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 167;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.treeListColumn2.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn2.Caption = "Thành tiền kế hoạch";
            this.treeListColumn2.FieldName = "ThanhTienKeHoach";
            this.treeListColumn2.Format.FormatString = "N0";
            this.treeListColumn2.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.treeListColumn3.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn3.Caption = "Thành tiền thi công";
            this.treeListColumn3.FieldName = "ThanhTienThiCong";
            this.treeListColumn3.Format.FormatString = "N0";
            this.treeListColumn3.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 2;
            this.treeListColumn3.Width = 76;
            // 
            // TongHopCongTacVatLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TL_TongHop);
            this.Name = "TongHopCongTacVatLieu";
            this.Size = new System.Drawing.Size(343, 386);
            ((System.ComponentModel.ISupportInitialize)(this.TL_TongHop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList TL_TongHop;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
    }
}
