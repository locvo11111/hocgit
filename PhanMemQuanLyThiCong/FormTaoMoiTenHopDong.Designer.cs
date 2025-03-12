
namespace PhanMemQuanLyThiCong
{
    partial class FormTaoMoiTenHopDong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaoMoiTenHopDong));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btnTaoMoiHopDong = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Công trình:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nhà thầu/CC/TĐ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tạo mới tên hợp đồng:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(133, 74);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(329, 20);
            this.textBox3.TabIndex = 5;
            // 
            // btnTaoMoiHopDong
            // 
            this.btnTaoMoiHopDong.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoMoiHopDong.ForeColor = System.Drawing.Color.Blue;
            this.btnTaoMoiHopDong.Location = new System.Drawing.Point(387, 14);
            this.btnTaoMoiHopDong.Name = "btnTaoMoiHopDong";
            this.btnTaoMoiHopDong.Size = new System.Drawing.Size(75, 51);
            this.btnTaoMoiHopDong.TabIndex = 6;
            this.btnTaoMoiHopDong.Text = "Tạo mới hợp đồng";
            this.btnTaoMoiHopDong.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(133, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(242, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "Chọn công trình";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(133, 44);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(242, 21);
            this.comboBox2.TabIndex = 8;
            this.comboBox2.Text = "Chọn nhà thầu/ Nhà cung cấp/ Tổ đội";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(134, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(316, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Chú ý: Nếu bạn không chọn \"Công trình\" và \" Nhà thầu/CC/TĐ\"";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(167, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(292, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "thì hợp đồng này đưa vào mục \"Hợp đồng khác\" của dự án";
            // 
            // FormTaoMoiTenHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 142);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnTaoMoiHopDong);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTaoMoiTenHopDong";
            this.Text = "Tạo mới tên hợp đồng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btnTaoMoiHopDong;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}