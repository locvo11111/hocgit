
namespace PhanMemQuanLyThiCong
{
    partial class FormCapNhatLenHeThongKhiTatChuongTrinh
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCapNhatSau = new System.Windows.Forms.Button();
            this.btnCapNhatNgayDuLieuMoi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(511, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bạn cần cập nhật lên hệ thống trước khi tắt chương trình để người khác sử dụng";
            // 
            // btnCapNhatSau
            // 
            this.btnCapNhatSau.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatSau.ForeColor = System.Drawing.Color.Blue;
            this.btnCapNhatSau.Location = new System.Drawing.Point(304, 26);
            this.btnCapNhatSau.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCapNhatSau.Name = "btnCapNhatSau";
            this.btnCapNhatSau.Size = new System.Drawing.Size(244, 36);
            this.btnCapNhatSau.TabIndex = 6;
            this.btnCapNhatSau.Text = "Cập nhật sau";
            this.btnCapNhatSau.UseVisualStyleBackColor = true;
            // 
            // btnCapNhatNgayDuLieuMoi
            // 
            this.btnCapNhatNgayDuLieuMoi.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatNgayDuLieuMoi.ForeColor = System.Drawing.Color.Red;
            this.btnCapNhatNgayDuLieuMoi.Location = new System.Drawing.Point(39, 26);
            this.btnCapNhatNgayDuLieuMoi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCapNhatNgayDuLieuMoi.Name = "btnCapNhatNgayDuLieuMoi";
            this.btnCapNhatNgayDuLieuMoi.Size = new System.Drawing.Size(237, 36);
            this.btnCapNhatNgayDuLieuMoi.TabIndex = 5;
            this.btnCapNhatNgayDuLieuMoi.Text = "Cập nhật dữ liệu lên hệ thống";
            this.btnCapNhatNgayDuLieuMoi.UseVisualStyleBackColor = true;
            // 
            // FormCapNhatLenHeThongKhiTatChuongTrinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 127);
            this.Controls.Add(this.btnCapNhatSau);
            this.Controls.Add(this.btnCapNhatNgayDuLieuMoi);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormCapNhatLenHeThongKhiTatChuongTrinh";
            this.Text = "Cập nhật dữ liệu lên hệ thống dùng chung";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCapNhatSau;
        private System.Windows.Forms.Button btnCapNhatNgayDuLieuMoi;
    }
}