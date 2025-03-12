
namespace PhanMemQuanLyThiCong
{
    partial class FormDocFileExcelDuNguyenDuongDan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDocFileExcelDuNguyenDuongDan));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_DuongDan_LayExcelThuCong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(613, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Đọc file";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn đường dẫn file excel:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(212, 30);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(392, 22);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(45, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(576, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "File sau khi đọc vào sẻ dữ nguyên nội dung dùng để làm phụ lục hợp đồng, thanh to" +
    "án và lưu";
            // 
            // btn_DuongDan_LayExcelThuCong
            // 
            this.btn_DuongDan_LayExcelThuCong.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DuongDan_LayExcelThuCong.Location = new System.Drawing.Point(248, 95);
            this.btn_DuongDan_LayExcelThuCong.Margin = new System.Windows.Forms.Padding(4);
            this.btn_DuongDan_LayExcelThuCong.Name = "btn_DuongDan_LayExcelThuCong";
            this.btn_DuongDan_LayExcelThuCong.Size = new System.Drawing.Size(287, 35);
            this.btn_DuongDan_LayExcelThuCong.TabIndex = 4;
            this.btn_DuongDan_LayExcelThuCong.Text = "Lấy thủ công chi tiết từng bảng";
            this.btn_DuongDan_LayExcelThuCong.UseVisualStyleBackColor = true;
            this.btn_DuongDan_LayExcelThuCong.Click += new System.EventHandler(this.btn_DuongDan_LayExcelThuCong_Click_1);
            // 
            // FormDocFileExcelDuNguyenDuongDan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 143);
            this.Controls.Add(this.btn_DuongDan_LayExcelThuCong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormDocFileExcelDuNguyenDuongDan";
            this.Text = "Đọc file excel dữ nguyên nội dung";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_DuongDan_LayExcelThuCong;
    }
}