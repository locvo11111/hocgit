
namespace PhanMemQuanLyThiCong
{
    partial class FormTaiFile_HopDong_XemTruoc_HopDong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaiFile_HopDong_XemTruoc_HopDong));
            this.btnTaiFileHopDong = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDuongDanTaiFileHopDong = new System.Windows.Forms.TextBox();
            this.btnTai_XemTruocHopDong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTaiFileHopDong
            // 
            this.btnTaiFileHopDong.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaiFileHopDong.Location = new System.Drawing.Point(391, 23);
            this.btnTaiFileHopDong.Name = "btnTaiFileHopDong";
            this.btnTaiFileHopDong.Size = new System.Drawing.Size(75, 23);
            this.btnTaiFileHopDong.TabIndex = 0;
            this.btnTaiFileHopDong.Text = "Tải file";
            this.btnTaiFileHopDong.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn đường dẫn";
            // 
            // txtDuongDanTaiFileHopDong
            // 
            this.txtDuongDanTaiFileHopDong.Location = new System.Drawing.Point(104, 24);
            this.txtDuongDanTaiFileHopDong.Name = "txtDuongDanTaiFileHopDong";
            this.txtDuongDanTaiFileHopDong.Size = new System.Drawing.Size(268, 20);
            this.txtDuongDanTaiFileHopDong.TabIndex = 2;
            // 
            // btnTai_XemTruocHopDong
            // 
            this.btnTai_XemTruocHopDong.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTai_XemTruocHopDong.Location = new System.Drawing.Point(481, 23);
            this.btnTai_XemTruocHopDong.Name = "btnTai_XemTruocHopDong";
            this.btnTai_XemTruocHopDong.Size = new System.Drawing.Size(75, 23);
            this.btnTai_XemTruocHopDong.TabIndex = 3;
            this.btnTai_XemTruocHopDong.Text = "Xem trước";
            this.btnTai_XemTruocHopDong.UseVisualStyleBackColor = true;
            // 
            // FormTaiFile_HopDong_XemTruoc_HopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 68);
            this.Controls.Add(this.btnTai_XemTruocHopDong);
            this.Controls.Add(this.txtDuongDanTaiFileHopDong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTaiFileHopDong);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTaiFile_HopDong_XemTruoc_HopDong";
            this.Text = "Tải file và xem trước hợp đồng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTaiFileHopDong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDuongDanTaiFileHopDong;
        private System.Windows.Forms.Button btnTai_XemTruocHopDong;
    }
}