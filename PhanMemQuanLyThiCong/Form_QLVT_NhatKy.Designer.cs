
namespace PhanMemQuanLyThiCong
{
    partial class Form_QLVT_NhatKy
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
            this.Tree_NhatKy = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Tree_NhatKy)).BeginInit();
            this.SuspendLayout();
            // 
            // Tree_NhatKy
            // 
            this.Tree_NhatKy.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn7,
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn5,
            this.treeListColumn4,
            this.treeListColumn6,
            this.treeListColumn8});
            this.Tree_NhatKy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tree_NhatKy.Location = new System.Drawing.Point(0, 0);
            this.Tree_NhatKy.Name = "Tree_NhatKy";
            this.Tree_NhatKy.Size = new System.Drawing.Size(1118, 499);
            this.Tree_NhatKy.TabIndex = 0;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Tên Vật Tư";
            this.treeListColumn7.FieldName = "TenVatTu";
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 0;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Tài xế";
            this.treeListColumn1.FieldName = "TaiXe";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 1;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Biển số xe";
            this.treeListColumn2.FieldName = "BienSoXe";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 2;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Kích thước thùng xe";
            this.treeListColumn3.FieldName = "KichThuocThungXe";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 3;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Tổng số lượng chuyến";
            this.treeListColumn5.FieldName = "TongSoLuongChuyen";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 5;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Khối lượng một chuyến";
            this.treeListColumn4.FieldName = "KhoiLuong_1Chuyen";
            this.treeListColumn4.Format.FormatString = "n2";
            this.treeListColumn4.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 4;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "Thực thế vận chuyển";
            this.treeListColumn6.FieldName = "ThucTeVanChuyen";
            this.treeListColumn6.Format.FormatString = "n2";
            this.treeListColumn6.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.Visible = true;
            this.treeListColumn6.VisibleIndex = 6;
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.Caption = "Đơn giá vận chuyển";
            this.treeListColumn8.FieldName = "DonGia";
            this.treeListColumn8.Name = "treeListColumn8";
            this.treeListColumn8.Visible = true;
            this.treeListColumn8.VisibleIndex = 7;
            // 
            // Form_QLVT_NhatKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 499);
            this.Controls.Add(this.Tree_NhatKy);
            this.Name = "Form_QLVT_NhatKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhật ký vật liệu";
            this.Load += new System.EventHandler(this.Form_QLVT_NhatKy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tree_NhatKy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList Tree_NhatKy;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
    }
}