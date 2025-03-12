
namespace PhanMemQuanLyThiCong
{
    partial class FomChiTietNem_TenDuongDan_ChenBienBan
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
            this.dgv_ChiTietNemDuongDanChenFile = new System.Windows.Forms.DataGridView();
            this.ChiTietNemTenDuongDanSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiTietNemTenDuongDanChon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiTietNemTenDuongDanTenNem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiTietNemTenDuongDanBienBanHienTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiTietNemTenDuongDanKyHieuNem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiTietNemTenDuongDanGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChiTietNemDuongDanChenFile)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ChiTietNemDuongDanChenFile
            // 
            this.dgv_ChiTietNemDuongDanChenFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ChiTietNemDuongDanChenFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChiTietNemTenDuongDanSTT,
            this.ChiTietNemTenDuongDanChon,
            this.ChiTietNemTenDuongDanTenNem,
            this.ChiTietNemTenDuongDanBienBanHienTai,
            this.Column1,
            this.ChiTietNemTenDuongDanKyHieuNem,
            this.ChiTietNemTenDuongDanGhiChu});
            this.dgv_ChiTietNemDuongDanChenFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ChiTietNemDuongDanChenFile.Location = new System.Drawing.Point(0, 0);
            this.dgv_ChiTietNemDuongDanChenFile.Name = "dgv_ChiTietNemDuongDanChenFile";
            this.dgv_ChiTietNemDuongDanChenFile.Size = new System.Drawing.Size(1003, 505);
            this.dgv_ChiTietNemDuongDanChenFile.TabIndex = 4;
            // 
            // ChiTietNemTenDuongDanSTT
            // 
            this.ChiTietNemTenDuongDanSTT.HeaderText = "STT";
            this.ChiTietNemTenDuongDanSTT.Name = "ChiTietNemTenDuongDanSTT";
            this.ChiTietNemTenDuongDanSTT.Width = 50;
            // 
            // ChiTietNemTenDuongDanChon
            // 
            this.ChiTietNemTenDuongDanChon.HeaderText = "Chọn";
            this.ChiTietNemTenDuongDanChon.Name = "ChiTietNemTenDuongDanChon";
            this.ChiTietNemTenDuongDanChon.Width = 40;
            // 
            // ChiTietNemTenDuongDanTenNem
            // 
            this.ChiTietNemTenDuongDanTenNem.HeaderText = "Tên nêm - Đường dẫn";
            this.ChiTietNemTenDuongDanTenNem.Name = "ChiTietNemTenDuongDanTenNem";
            this.ChiTietNemTenDuongDanTenNem.Width = 250;
            // 
            // ChiTietNemTenDuongDanBienBanHienTai
            // 
            this.ChiTietNemTenDuongDanBienBanHienTai.HeaderText = "Tên biên bản hiện tại";
            this.ChiTietNemTenDuongDanBienBanHienTai.Name = "ChiTietNemTenDuongDanBienBanHienTai";
            this.ChiTietNemTenDuongDanBienBanHienTai.Width = 250;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Tên bảng chứa đường dẫn";
            this.Column1.Name = "Column1";
            // 
            // ChiTietNemTenDuongDanKyHieuNem
            // 
            this.ChiTietNemTenDuongDanKyHieuNem.HeaderText = "Ký hiệu nêm";
            this.ChiTietNemTenDuongDanKyHieuNem.Name = "ChiTietNemTenDuongDanKyHieuNem";
            this.ChiTietNemTenDuongDanKyHieuNem.Width = 150;
            // 
            // ChiTietNemTenDuongDanGhiChu
            // 
            this.ChiTietNemTenDuongDanGhiChu.HeaderText = "Ghi chú";
            this.ChiTietNemTenDuongDanGhiChu.Name = "ChiTietNemTenDuongDanGhiChu";
            this.ChiTietNemTenDuongDanGhiChu.Width = 150;
            // 
            // FomChiTietNem_TenDuongDan_ChenBienBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 505);
            this.Controls.Add(this.dgv_ChiTietNemDuongDanChenFile);
            this.Name = "FomChiTietNem_TenDuongDan_ChenBienBan";
            this.Text = "Chi tiết đường dẫn tạo biên bản";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChiTietNemDuongDanChenFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ChiTietNemDuongDanChenFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiTietNemTenDuongDanSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiTietNemTenDuongDanChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiTietNemTenDuongDanTenNem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiTietNemTenDuongDanBienBanHienTai;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiTietNemTenDuongDanKyHieuNem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiTietNemTenDuongDanGhiChu;
    }
}