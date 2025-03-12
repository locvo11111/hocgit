
namespace PhanMemQuanLyThiCong
{
    partial class Form_ThonTinHopDong
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.bt_LuuThayDoi = new System.Windows.Forms.Button();
            this.bt_LuuVaThoat = new System.Windows.Forms.Button();
            this.bt_Huy = new System.Windows.Forms.Button();
            this.dtg_luachon = new System.Windows.Forms.DataGridView();
            this.m_openDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_TenCV = new System.Windows.Forms.Label();
            this.lb_KLTong = new System.Windows.Forms.Label();
            this.lb_DonVi = new System.Windows.Forms.Label();
            this.lb_CanhBao = new System.Windows.Forms.Label();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Xemtrước = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ThayThế = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Xóa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_luachon)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_CanhBao);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.bt_LuuThayDoi);
            this.panel1.Controls.Add(this.bt_LuuVaThoat);
            this.panel1.Controls.Add(this.bt_Huy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 419);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 31);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(427, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 31);
            this.button2.TabIndex = 2;
            this.button2.Text = "Hủy thay đổi";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // bt_LuuThayDoi
            // 
            this.bt_LuuThayDoi.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_LuuThayDoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_LuuThayDoi.Location = new System.Drawing.Point(531, 0);
            this.bt_LuuThayDoi.Name = "bt_LuuThayDoi";
            this.bt_LuuThayDoi.Size = new System.Drawing.Size(93, 31);
            this.bt_LuuThayDoi.TabIndex = 1;
            this.bt_LuuThayDoi.Text = "Lưu thay đổi";
            this.bt_LuuThayDoi.UseVisualStyleBackColor = true;
            this.bt_LuuThayDoi.Click += new System.EventHandler(this.bt_LuuThayDoi_Click);
            // 
            // bt_LuuVaThoat
            // 
            this.bt_LuuVaThoat.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_LuuVaThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_LuuVaThoat.Location = new System.Drawing.Point(624, 0);
            this.bt_LuuVaThoat.Name = "bt_LuuVaThoat";
            this.bt_LuuVaThoat.Size = new System.Drawing.Size(101, 31);
            this.bt_LuuVaThoat.TabIndex = 2;
            this.bt_LuuVaThoat.Text = "Lưu và thoát";
            this.bt_LuuVaThoat.UseVisualStyleBackColor = true;
            this.bt_LuuVaThoat.Click += new System.EventHandler(this.bt_LuuVaThoat_Click);
            // 
            // bt_Huy
            // 
            this.bt_Huy.BackColor = System.Drawing.Color.Red;
            this.bt_Huy.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_Huy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Huy.Location = new System.Drawing.Point(725, 0);
            this.bt_Huy.Name = "bt_Huy";
            this.bt_Huy.Size = new System.Drawing.Size(75, 31);
            this.bt_Huy.TabIndex = 0;
            this.bt_Huy.Text = "Hủy";
            this.bt_Huy.UseVisualStyleBackColor = false;
            this.bt_Huy.Click += new System.EventHandler(this.bt_Huy_Click);
            // 
            // dtg_luachon
            // 
            this.dtg_luachon.AllowUserToDeleteRows = false;
            this.dtg_luachon.AllowUserToOrderColumns = true;
            this.dtg_luachon.BackgroundColor = System.Drawing.Color.White;
            this.dtg_luachon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_luachon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.Xemtrước,
            this.ThayThế,
            this.Xóa});
            this.dtg_luachon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtg_luachon.Location = new System.Drawing.Point(3, 16);
            this.dtg_luachon.Name = "dtg_luachon";
            this.dtg_luachon.RowHeadersVisible = false;
            this.dtg_luachon.RowHeadersWidth = 51;
            this.dtg_luachon.RowTemplate.Height = 24;
            this.dtg_luachon.Size = new System.Drawing.Size(794, 337);
            this.dtg_luachon.TabIndex = 4;
            this.dtg_luachon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtg_HopDong_CellContentClick);
            this.dtg_luachon.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtg_luachon_CellValueChanged);
            this.dtg_luachon.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dtg_luachon_RowsAdded);
            // 
            // m_openDialog
            // 
            this.m_openDialog.FileName = "openFileDialog1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtg_luachon);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(800, 356);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi tiết hợp đồng";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lb_DonVi);
            this.panel2.Controls.Add(this.lb_KLTong);
            this.panel2.Controls.Add(this.lb_TenCV);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 63);
            this.panel2.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 13);
            this.label9.TabIndex = 50;
            this.label9.Text = "Tên công việc:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Khối lượng tổng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(366, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Đơn vị:";
            // 
            // lb_TenCV
            // 
            this.lb_TenCV.AutoSize = true;
            this.lb_TenCV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_TenCV.ForeColor = System.Drawing.Color.Blue;
            this.lb_TenCV.Location = new System.Drawing.Point(118, 7);
            this.lb_TenCV.Name = "lb_TenCV";
            this.lb_TenCV.Size = new System.Drawing.Size(41, 15);
            this.lb_TenCV.TabIndex = 51;
            this.lb_TenCV.Text = "label3";
            // 
            // lb_KLTong
            // 
            this.lb_KLTong.AutoSize = true;
            this.lb_KLTong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_KLTong.ForeColor = System.Drawing.Color.Blue;
            this.lb_KLTong.Location = new System.Drawing.Point(118, 36);
            this.lb_KLTong.Name = "lb_KLTong";
            this.lb_KLTong.Size = new System.Drawing.Size(41, 15);
            this.lb_KLTong.TabIndex = 51;
            this.lb_KLTong.Text = "label3";
            // 
            // lb_DonVi
            // 
            this.lb_DonVi.AutoSize = true;
            this.lb_DonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_DonVi.ForeColor = System.Drawing.Color.Blue;
            this.lb_DonVi.Location = new System.Drawing.Point(421, 36);
            this.lb_DonVi.Name = "lb_DonVi";
            this.lb_DonVi.Size = new System.Drawing.Size(41, 15);
            this.lb_DonVi.TabIndex = 51;
            this.lb_DonVi.Text = "label3";
            // 
            // lb_CanhBao
            // 
            this.lb_CanhBao.AutoSize = true;
            this.lb_CanhBao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_CanhBao.ForeColor = System.Drawing.Color.Blue;
            this.lb_CanhBao.Location = new System.Drawing.Point(12, 7);
            this.lb_CanhBao.Name = "lb_CanhBao";
            this.lb_CanhBao.Size = new System.Drawing.Size(41, 15);
            this.lb_CanhBao.TabIndex = 52;
            this.lb_CanhBao.Text = "label3";
            // 
            // STT
            // 
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            // 
            // Xemtrước
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Aqua;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.Xemtrước.DefaultCellStyle = dataGridViewCellStyle1;
            this.Xemtrước.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Xemtrước.HeaderText = "Xem trước";
            this.Xemtrước.Name = "Xemtrước";
            this.Xemtrước.Text = "Xem trước";
            this.Xemtrước.UseColumnTextForButtonValue = true;
            // 
            // ThayThế
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray;
            this.ThayThế.DefaultCellStyle = dataGridViewCellStyle2;
            this.ThayThế.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ThayThế.HeaderText = "Thay thế File";
            this.ThayThế.Name = "ThayThế";
            this.ThayThế.Text = "Thay thế File";
            // 
            // Xóa
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Red;
            this.Xóa.DefaultCellStyle = dataGridViewCellStyle3;
            this.Xóa.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Xóa.HeaderText = "Xóa hợp đồng";
            this.Xóa.Name = "Xóa";
            this.Xóa.Text = "Xóa hợp đồng";
            this.Xóa.UseColumnTextForButtonValue = true;
            // 
            // Form_ThonTinHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "Form_ThonTinHopDong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form_ThonTinHopDong";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ThonTinHopDong_FormClosing);
            this.Load += new System.EventHandler(this.Form_ThonTinHopDong_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_luachon)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_LuuThayDoi;
        private System.Windows.Forms.Button bt_Huy;
        private System.Windows.Forms.DataGridView dtg_luachon;
        private System.Windows.Forms.OpenFileDialog m_openDialog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button bt_LuuVaThoat;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lb_DonVi;
        private System.Windows.Forms.Label lb_KLTong;
        private System.Windows.Forms.Label lb_TenCV;
        private System.Windows.Forms.Label lb_CanhBao;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewButtonColumn Xemtrước;
        private System.Windows.Forms.DataGridViewButtonColumn ThayThế;
        private System.Windows.Forms.DataGridViewButtonColumn Xóa;
    }
}