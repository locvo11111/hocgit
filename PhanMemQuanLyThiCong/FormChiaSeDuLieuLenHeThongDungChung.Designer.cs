
namespace PhanMemQuanLyThiCong
{
    partial class FormChiaSeDuLieuLenHeThongDungChung
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
            this.btnChiaSeDuLieuLenHeThongDungChung = new System.Windows.Forms.Button();
            this.btnThoatDeSau = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnChiaSeDuLieuLenHeThongDungChung
            // 
            this.btnChiaSeDuLieuLenHeThongDungChung.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChiaSeDuLieuLenHeThongDungChung.ForeColor = System.Drawing.Color.Red;
            this.btnChiaSeDuLieuLenHeThongDungChung.Location = new System.Drawing.Point(41, 28);
            this.btnChiaSeDuLieuLenHeThongDungChung.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChiaSeDuLieuLenHeThongDungChung.Name = "btnChiaSeDuLieuLenHeThongDungChung";
            this.btnChiaSeDuLieuLenHeThongDungChung.Size = new System.Drawing.Size(319, 39);
            this.btnChiaSeDuLieuLenHeThongDungChung.TabIndex = 0;
            this.btnChiaSeDuLieuLenHeThongDungChung.Text = "Chia sẻ dữ liệu lên hệ thống dùng chung";
            this.btnChiaSeDuLieuLenHeThongDungChung.UseVisualStyleBackColor = true;
            // 
            // btnThoatDeSau
            // 
            this.btnThoatDeSau.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoatDeSau.ForeColor = System.Drawing.Color.Blue;
            this.btnThoatDeSau.Location = new System.Drawing.Point(391, 28);
            this.btnThoatDeSau.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThoatDeSau.Name = "btnThoatDeSau";
            this.btnThoatDeSau.Size = new System.Drawing.Size(225, 39);
            this.btnThoatDeSau.TabIndex = 2;
            this.btnThoatDeSau.Text = "Để sau";
            this.btnThoatDeSau.UseVisualStyleBackColor = true;
            this.btnThoatDeSau.Click += new System.EventHandler(this.btnThoatDeSau_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 85);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(478, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Khi bạn chia sẻ nội bộ thì mọi người có thể sử dụng dữ liệu này để làm việc";
            // 
            // FormChiaSeDuLieuLenHeThongDungChung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 114);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnThoatDeSau);
            this.Controls.Add(this.btnChiaSeDuLieuLenHeThongDungChung);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormChiaSeDuLieuLenHeThongDungChung";
            this.Text = "Chia sẻ dữ liệu lên hệ thống dùng chung";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChiaSeDuLieuLenHeThongDungChung;
        private System.Windows.Forms.Button btnThoatDeSau;
        private System.Windows.Forms.Label label1;
    }
}