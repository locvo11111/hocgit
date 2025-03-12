
namespace PhanMemQuanLyThiCong
{
    partial class Form_QLMay_LuaChonCongViec
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_CongViecThucHien = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Ngaythuchien = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_ChonAll = new System.Windows.Forms.Button();
            this.btn_HuyChon = new System.Windows.Forms.Button();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(1, 167);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1069, 367);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1069, 367);
            this.dataGridView1.TabIndex = 0;
            // 
            // txt_CongViecThucHien
            // 
            this.txt_CongViecThucHien.Location = new System.Drawing.Point(1, 65);
            this.txt_CongViecThucHien.Multiline = true;
            this.txt_CongViecThucHien.Name = "txt_CongViecThucHien";
            this.txt_CongViecThucHien.Size = new System.Drawing.Size(275, 34);
            this.txt_CongViecThucHien.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(22, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên Công Việc Thực Hiện";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(331, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ngày Thực Hiện";
            // 
            // txt_Ngaythuchien
            // 
            this.txt_Ngaythuchien.Location = new System.Drawing.Point(336, 65);
            this.txt_Ngaythuchien.Multiline = true;
            this.txt_Ngaythuchien.Name = "txt_Ngaythuchien";
            this.txt_Ngaythuchien.Size = new System.Drawing.Size(136, 34);
            this.txt_Ngaythuchien.TabIndex = 3;
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(902, 53);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(114, 47);
            this.btn_Search.TabIndex = 5;
            this.btn_Search.Text = "Tìm Kiếm";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(193, 554);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(112, 59);
            this.btn_ok.TabIndex = 6;
            this.btn_ok.Text = "Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_ChonAll
            // 
            this.btn_ChonAll.Location = new System.Drawing.Point(385, 554);
            this.btn_ChonAll.Name = "btn_ChonAll";
            this.btn_ChonAll.Size = new System.Drawing.Size(112, 59);
            this.btn_ChonAll.TabIndex = 6;
            this.btn_ChonAll.Text = "Chọn Tất Cả";
            this.btn_ChonAll.UseVisualStyleBackColor = true;
            this.btn_ChonAll.Click += new System.EventHandler(this.btn_ChonAll_Click);
            // 
            // btn_HuyChon
            // 
            this.btn_HuyChon.Location = new System.Drawing.Point(609, 554);
            this.btn_HuyChon.Name = "btn_HuyChon";
            this.btn_HuyChon.Size = new System.Drawing.Size(112, 59);
            this.btn_HuyChon.TabIndex = 6;
            this.btn_HuyChon.Text = "Hủy";
            this.btn_HuyChon.UseVisualStyleBackColor = true;
            this.btn_HuyChon.Click += new System.EventHandler(this.btn_HuyChon_Click);
            // 
            // check
            // 
            this.check.HeaderText = "Chọn";
            this.check.MinimumWidth = 6;
            this.check.Name = "check";
            this.check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.check.Width = 125;
            // 
            // Form_QLMay_LuaChonCongViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 625);
            this.Controls.Add(this.btn_HuyChon);
            this.Controls.Add(this.btn_ChonAll);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Ngaythuchien);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_CongViecThucHien);
            this.Controls.Add(this.panel1);
            this.Name = "Form_QLMay_LuaChonCongViec";
            this.Text = "Lựa Chọn Công Việc Thực Hiện";
            this.Load += new System.EventHandler(this.Form_QLMay_LuaChonCongViec_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_CongViecThucHien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Ngaythuchien;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_ChonAll;
        private System.Windows.Forms.Button btn_HuyChon;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
    }
}