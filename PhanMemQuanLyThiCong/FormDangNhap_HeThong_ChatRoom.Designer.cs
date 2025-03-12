
namespace PhanMemQuanLyThiCong
{
    partial class FormDangNhap_HeThong_ChatRoom
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
            this.bt_DangNhap = new System.Windows.Forms.Button();
            this.txt_Pass = new System.Windows.Forms.TextBox();
            this.txt_Email = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bt_Thoat = new System.Windows.Forms.Button();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.bt_TaoTaiKhoan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_DangNhap
            // 
            this.bt_DangNhap.BackColor = System.Drawing.Color.Blue;
            this.bt_DangNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_DangNhap.ForeColor = System.Drawing.Color.Transparent;
            this.bt_DangNhap.Location = new System.Drawing.Point(85, 73);
            this.bt_DangNhap.Name = "bt_DangNhap";
            this.bt_DangNhap.Size = new System.Drawing.Size(100, 37);
            this.bt_DangNhap.TabIndex = 55;
            this.bt_DangNhap.Text = "Đăng nhập";
            this.bt_DangNhap.UseVisualStyleBackColor = false;
            this.bt_DangNhap.Click += new System.EventHandler(this.btn_DN_DangNhap_Click);
            // 
            // txt_Pass
            // 
            this.txt_Pass.Location = new System.Drawing.Point(85, 47);
            this.txt_Pass.Name = "txt_Pass";
            this.txt_Pass.Size = new System.Drawing.Size(171, 20);
            this.txt_Pass.TabIndex = 52;
            // 
            // txt_Email
            // 
            this.txt_Email.Location = new System.Drawing.Point(85, 18);
            this.txt_Email.Name = "txt_Email";
            this.txt_Email.Size = new System.Drawing.Size(171, 20);
            this.txt_Email.TabIndex = 51;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(12, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 50;
            this.label10.Text = "Mật khẩu:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(12, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 49;
            this.label9.Text = "Email:";
            // 
            // bt_Thoat
            // 
            this.bt_Thoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.bt_Thoat.Location = new System.Drawing.Point(191, 73);
            this.bt_Thoat.Name = "bt_Thoat";
            this.bt_Thoat.Size = new System.Drawing.Size(65, 37);
            this.bt_Thoat.TabIndex = 48;
            this.bt_Thoat.Text = "Thoát";
            this.bt_Thoat.UseVisualStyleBackColor = false;
            this.bt_Thoat.Click += new System.EventHandler(this.btn_DN_Thoat_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(82, 113);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(80, 13);
            this.linkLabel2.TabIndex = 57;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Quên mật khẩu";
            // 
            // bt_TaoTaiKhoan
            // 
            this.bt_TaoTaiKhoan.BackColor = System.Drawing.Color.SeaGreen;
            this.bt_TaoTaiKhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_TaoTaiKhoan.ForeColor = System.Drawing.Color.Transparent;
            this.bt_TaoTaiKhoan.Location = new System.Drawing.Point(85, 129);
            this.bt_TaoTaiKhoan.Name = "bt_TaoTaiKhoan";
            this.bt_TaoTaiKhoan.Size = new System.Drawing.Size(171, 30);
            this.bt_TaoTaiKhoan.TabIndex = 58;
            this.bt_TaoTaiKhoan.Text = "Tạo tài khoản mới";
            this.bt_TaoTaiKhoan.UseVisualStyleBackColor = false;
            this.bt_TaoTaiKhoan.Click += new System.EventHandler(this.bt_TaoTaiKhoan_Click);
            // 
            // FormDangNhap_HeThong_ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 171);
            this.Controls.Add(this.bt_TaoTaiKhoan);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.bt_DangNhap);
            this.Controls.Add(this.txt_Pass);
            this.Controls.Add(this.txt_Email);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.bt_Thoat);
            this.Name = "FormDangNhap_HeThong_ChatRoom";
            this.Text = "Đăng nhập hệ thống";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_DangNhap;
        private System.Windows.Forms.TextBox txt_Pass;
        private System.Windows.Forms.TextBox txt_Email;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bt_Thoat;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button bt_TaoTaiKhoan;
    }
}