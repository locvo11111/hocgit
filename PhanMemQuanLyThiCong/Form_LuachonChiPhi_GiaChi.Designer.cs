namespace PhanMemQuanLyThiCong
{
    partial class Form_LuachonChiPhi_GiaChi
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
            this.spread_Thuchitamung_guiduyet = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_capnhap = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_timkiem = new System.Windows.Forms.Button();
            this.txt_duan = new System.Windows.Forms.TextBox();
            this.txt_tenhopdong = new System.Windows.Forms.TextBox();
            this.txt_Tenchiphi = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // spread_Thuchitamung_guiduyet
            // 
            this.spread_Thuchitamung_guiduyet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spread_Thuchitamung_guiduyet.Location = new System.Drawing.Point(0, 0);
            this.spread_Thuchitamung_guiduyet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.spread_Thuchitamung_guiduyet.Name = "spread_Thuchitamung_guiduyet";
            this.spread_Thuchitamung_guiduyet.Size = new System.Drawing.Size(1090, 480);
            this.spread_Thuchitamung_guiduyet.TabIndex = 0;
            this.spread_Thuchitamung_guiduyet.Text = "spreadsheetControl1";
            this.spread_Thuchitamung_guiduyet.CellValueChanged += new DevExpress.XtraSpreadsheet.CellValueChangedEventHandler(this.spread_Thuchitamung_guiduyet_CellValueChanged);
            this.spread_Thuchitamung_guiduyet.Click += new System.EventHandler(this.spread_Thuchitamung_guiduyet_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_capnhap);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_timkiem);
            this.panel1.Controls.Add(this.txt_duan);
            this.panel1.Controls.Add(this.txt_tenhopdong);
            this.panel1.Controls.Add(this.txt_Tenchiphi);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1090, 81);
            this.panel1.TabIndex = 1;
            // 
            // btn_capnhap
            // 
            this.btn_capnhap.AutoSize = true;
            this.btn_capnhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_capnhap.Location = new System.Drawing.Point(150, 22);
            this.btn_capnhap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_capnhap.Name = "btn_capnhap";
            this.btn_capnhap.Size = new System.Drawing.Size(136, 45);
            this.btn_capnhap.TabIndex = 3;
            this.btn_capnhap.Text = "Cập nhập tất cả";
            this.btn_capnhap.UseVisualStyleBackColor = false;
            this.btn_capnhap.Click += new System.EventHandler(this.btn_capnhap_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(712, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "DA/CT/HM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(540, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nhập hợp đồng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(319, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nhập tên chi phí";
            // 
            // btn_timkiem
            // 
            this.btn_timkiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_timkiem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_timkiem.Location = new System.Drawing.Point(935, 42);
            this.btn_timkiem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_timkiem.Name = "btn_timkiem";
            this.btn_timkiem.Size = new System.Drawing.Size(95, 24);
            this.btn_timkiem.TabIndex = 1;
            this.btn_timkiem.Text = "Tìm kiếm";
            this.btn_timkiem.UseVisualStyleBackColor = false;
            this.btn_timkiem.Click += new System.EventHandler(this.btn_timkiem_Click);
            // 
            // txt_duan
            // 
            this.txt_duan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_duan.Location = new System.Drawing.Point(715, 42);
            this.txt_duan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_duan.Name = "txt_duan";
            this.txt_duan.Size = new System.Drawing.Size(114, 26);
            this.txt_duan.TabIndex = 0;
            // 
            // txt_tenhopdong
            // 
            this.txt_tenhopdong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_tenhopdong.Location = new System.Drawing.Point(543, 42);
            this.txt_tenhopdong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_tenhopdong.Name = "txt_tenhopdong";
            this.txt_tenhopdong.Size = new System.Drawing.Size(114, 26);
            this.txt_tenhopdong.TabIndex = 0;
            // 
            // txt_Tenchiphi
            // 
            this.txt_Tenchiphi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_Tenchiphi.Location = new System.Drawing.Point(322, 42);
            this.txt_Tenchiphi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Tenchiphi.Name = "txt_Tenchiphi";
            this.txt_Tenchiphi.Size = new System.Drawing.Size(173, 26);
            this.txt_Tenchiphi.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.spread_Thuchitamung_guiduyet);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 81);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1090, 480);
            this.panel3.TabIndex = 2;
            // 
            // Form_LuachonChiPhi_GiaChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1090, 561);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form_LuachonChiPhi_GiaChi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_LuachonChiPhi_GiaChi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_LuachonChiPhi_GiaChi_FormClosed);
            this.Load += new System.EventHandler(this.Form_LuachonChiPhi_GiaChi_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraSpreadsheet.SpreadsheetControl spread_Thuchitamung_guiduyet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_duan;
        private System.Windows.Forms.TextBox txt_tenhopdong;
        private System.Windows.Forms.TextBox txt_Tenchiphi;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_timkiem;
        private System.Windows.Forms.Button btn_capnhap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}