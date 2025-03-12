
namespace PhanMemQuanLyThiCong
{
    partial class FormCapNhatDuLieuHienThoi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCapNhatDuLieuHienThoi));
            this.btnCapNhatDanhSachDuAn = new System.Windows.Forms.Button();
            this.btn_CapNhatDuAnHientai = new System.Windows.Forms.Button();
            this.bt_Upload2Server = new System.Windows.Forms.Button();
            this.cb_OnlyChanged = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCapNhatDanhSachDuAn
            // 
            this.btnCapNhatDanhSachDuAn.Enabled = false;
            this.btnCapNhatDanhSachDuAn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatDanhSachDuAn.ForeColor = System.Drawing.Color.Red;
            this.btnCapNhatDanhSachDuAn.Location = new System.Drawing.Point(156, 12);
            this.btnCapNhatDanhSachDuAn.Name = "btnCapNhatDanhSachDuAn";
            this.btnCapNhatDanhSachDuAn.Size = new System.Drawing.Size(162, 28);
            this.btnCapNhatDanhSachDuAn.TabIndex = 0;
            this.btnCapNhatDanhSachDuAn.Text = "Cập nhật danh sách dự án";
            this.btnCapNhatDanhSachDuAn.UseVisualStyleBackColor = true;
            this.btnCapNhatDanhSachDuAn.Click += new System.EventHandler(this.btnCapNhatDanhSachDuAn_Click);
            // 
            // btn_CapNhatDuAnHientai
            // 
            this.btn_CapNhatDuAnHientai.Enabled = false;
            this.btn_CapNhatDuAnHientai.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CapNhatDuAnHientai.ForeColor = System.Drawing.Color.Blue;
            this.btn_CapNhatDuAnHientai.Location = new System.Drawing.Point(324, 12);
            this.btn_CapNhatDuAnHientai.Name = "btn_CapNhatDuAnHientai";
            this.btn_CapNhatDuAnHientai.Size = new System.Drawing.Size(189, 28);
            this.btn_CapNhatDuAnHientai.TabIndex = 1;
            this.btn_CapNhatDuAnHientai.Text = "Cập nhật dự án hiện tại về máy";
            this.btn_CapNhatDuAnHientai.UseVisualStyleBackColor = true;
            this.btn_CapNhatDuAnHientai.Click += new System.EventHandler(this.btnThoatDeSau_Click);
            // 
            // bt_Upload2Server
            // 
            this.bt_Upload2Server.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Upload2Server.ForeColor = System.Drawing.Color.Red;
            this.bt_Upload2Server.Location = new System.Drawing.Point(9, 12);
            this.bt_Upload2Server.Name = "bt_Upload2Server";
            this.bt_Upload2Server.Size = new System.Drawing.Size(141, 28);
            this.bt_Upload2Server.TabIndex = 0;
            this.bt_Upload2Server.Text = "Tải lên dự án hiện tại";
            this.bt_Upload2Server.UseVisualStyleBackColor = true;
            this.bt_Upload2Server.Click += new System.EventHandler(this.bt_Upload2Server_Click);
            // 
            // cb_OnlyChanged
            // 
            this.cb_OnlyChanged.AutoSize = true;
            this.cb_OnlyChanged.Checked = true;
            this.cb_OnlyChanged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_OnlyChanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.cb_OnlyChanged.ForeColor = System.Drawing.Color.Fuchsia;
            this.cb_OnlyChanged.Location = new System.Drawing.Point(13, 47);
            this.cb_OnlyChanged.Name = "cb_OnlyChanged";
            this.cb_OnlyChanged.Size = new System.Drawing.Size(234, 21);
            this.cb_OnlyChanged.TabIndex = 2;
            this.cb_OnlyChanged.Text = "Chỉ tải dữ liệu có sự thay đổi";
            this.cb_OnlyChanged.UseVisualStyleBackColor = true;
            // 
            // FormCapNhatDuLieuHienThoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 78);
            this.Controls.Add(this.cb_OnlyChanged);
            this.Controls.Add(this.btn_CapNhatDuAnHientai);
            this.Controls.Add(this.bt_Upload2Server);
            this.Controls.Add(this.btnCapNhatDanhSachDuAn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCapNhatDuLieuHienThoi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật dữ liệu hiện thời";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCapNhatDanhSachDuAn;
        private System.Windows.Forms.Button btn_CapNhatDuAnHientai;
        private System.Windows.Forms.Button bt_Upload2Server;
        private System.Windows.Forms.CheckBox cb_OnlyChanged;
    }
}