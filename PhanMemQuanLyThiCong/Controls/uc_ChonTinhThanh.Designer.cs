namespace PhanMemQuanLyThiCong.Controls
{
    partial class uc_ChonTinhThanh
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
            this.slke_TinhThanh = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ce_DonGiaTheoTinh = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.slke_TinhThanh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_DonGiaTheoTinh.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // slke_TinhThanh
            // 
            this.slke_TinhThanh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slke_TinhThanh.Enabled = false;
            this.slke_TinhThanh.Location = new System.Drawing.Point(262, 0);
            this.slke_TinhThanh.Name = "slke_TinhThanh";
            this.slke_TinhThanh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slke_TinhThanh.Properties.DisplayMember = "name";
            this.slke_TinhThanh.Properties.NullText = "[Chọn tỉnh thành]";
            this.slke_TinhThanh.Properties.PopupView = this.searchLookUpEdit1View;
            this.slke_TinhThanh.Properties.ValueMember = "TenKhongDau";
            this.slke_TinhThanh.Size = new System.Drawing.Size(169, 20);
            this.slke_TinhThanh.TabIndex = 5;
            this.slke_TinhThanh.EditValueChanged += new System.EventHandler(this.slke_TinhThanh_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Tỉnh thành";
            this.gridColumn6.FieldName = "name";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // ce_DonGiaTheoTinh
            // 
            this.ce_DonGiaTheoTinh.Dock = System.Windows.Forms.DockStyle.Left;
            this.ce_DonGiaTheoTinh.Location = new System.Drawing.Point(0, 0);
            this.ce_DonGiaTheoTinh.Name = "ce_DonGiaTheoTinh";
            this.ce_DonGiaTheoTinh.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ce_DonGiaTheoTinh.Properties.Appearance.Options.UseFont = true;
            this.ce_DonGiaTheoTinh.Properties.Caption = "Đơn giá tham khảo VẬT TƯ theo tỉnh thành";
            this.ce_DonGiaTheoTinh.Size = new System.Drawing.Size(262, 27);
            this.ce_DonGiaTheoTinh.TabIndex = 4;
            this.ce_DonGiaTheoTinh.CheckedChanged += new System.EventHandler(this.ce_DonGiaTheoTinh_CheckedChanged);
            // 
            // uc_ChonTinhThanh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.slke_TinhThanh);
            this.Controls.Add(this.ce_DonGiaTheoTinh);
            this.Name = "uc_ChonTinhThanh";
            this.Size = new System.Drawing.Size(431, 27);
            ((System.ComponentModel.ISupportInitialize)(this.slke_TinhThanh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_DonGiaTheoTinh.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit slke_TinhThanh;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.CheckEdit ce_DonGiaTheoTinh;
    }
}
