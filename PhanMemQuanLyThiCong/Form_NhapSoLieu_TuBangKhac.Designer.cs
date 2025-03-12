
namespace PhanMemQuanLyThiCong
{
    partial class Form_NhapSoLieu_TuBangKhac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_NhapSoLieu_TuBangKhac));
            this.btn_DocTuExcel = new System.Windows.Forms.Button();
            this.btn_LayFileTuBangDoBocKeHoach = new System.Windows.Forms.Button();
            this.btn_Thoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_DocTuExcel
            // 
            this.btn_DocTuExcel.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DocTuExcel.Location = new System.Drawing.Point(19, 29);
            this.btn_DocTuExcel.Name = "btn_DocTuExcel";
            this.btn_DocTuExcel.Size = new System.Drawing.Size(160, 44);
            this.btn_DocTuExcel.TabIndex = 0;
            this.btn_DocTuExcel.Text = "Đọc từ file excel";
            this.btn_DocTuExcel.UseVisualStyleBackColor = true;
            this.btn_DocTuExcel.Click += new System.EventHandler(this.btn_DocTuExcel_Click);
            // 
            // btn_LayFileTuBangDoBocKeHoach
            // 
            this.btn_LayFileTuBangDoBocKeHoach.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LayFileTuBangDoBocKeHoach.Location = new System.Drawing.Point(204, 30);
            this.btn_LayFileTuBangDoBocKeHoach.Name = "btn_LayFileTuBangDoBocKeHoach";
            this.btn_LayFileTuBangDoBocKeHoach.Size = new System.Drawing.Size(231, 42);
            this.btn_LayFileTuBangDoBocKeHoach.TabIndex = 1;
            this.btn_LayFileTuBangDoBocKeHoach.Text = "Lấy từ bảng đo bóc kế hoạch";
            this.btn_LayFileTuBangDoBocKeHoach.UseVisualStyleBackColor = true;
            // 
            // btn_Thoat
            // 
            this.btn_Thoat.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Thoat.Location = new System.Drawing.Point(336, 95);
            this.btn_Thoat.Name = "btn_Thoat";
            this.btn_Thoat.Size = new System.Drawing.Size(99, 42);
            this.btn_Thoat.TabIndex = 2;
            this.btn_Thoat.Text = "Thoát";
            this.btn_Thoat.UseVisualStyleBackColor = true;
            this.btn_Thoat.Click += new System.EventHandler(this.btn_Thoat_Click);
            // 
            // Form_NhapSoLieu_TuBangKhac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 146);
            this.Controls.Add(this.btn_Thoat);
            this.Controls.Add(this.btn_LayFileTuBangDoBocKeHoach);
            this.Controls.Add(this.btn_DocTuExcel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_NhapSoLieu_TuBangKhac";
            this.Text = "Nhập số liệu tiến đô từ bảng khác";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DocTuExcel;
        private System.Windows.Forms.Button btn_LayFileTuBangDoBocKeHoach;
        private System.Windows.Forms.Button btn_Thoat;
    }
}