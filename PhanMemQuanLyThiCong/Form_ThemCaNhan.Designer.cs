
namespace PhanMemQuanLyThiCong
{
    partial class Form_ThemCaNhan
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
            this.dgv_thanhPhanChuaThamGia = new System.Windows.Forms.DataGridView();
            this.Them = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgv_thanhPhanThamGia = new System.Windows.Forms.DataGridView();
            this.Xoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_LuuThayDoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_thanhPhanChuaThamGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_thanhPhanThamGia)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_thanhPhanChuaThamGia
            // 
            this.dgv_thanhPhanChuaThamGia.AllowUserToAddRows = false;
            this.dgv_thanhPhanChuaThamGia.AllowUserToDeleteRows = false;
            this.dgv_thanhPhanChuaThamGia.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_thanhPhanChuaThamGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_thanhPhanChuaThamGia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Them});
            this.dgv_thanhPhanChuaThamGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_thanhPhanChuaThamGia.Location = new System.Drawing.Point(3, 16);
            this.dgv_thanhPhanChuaThamGia.Name = "dgv_thanhPhanChuaThamGia";
            this.dgv_thanhPhanChuaThamGia.ReadOnly = true;
            this.dgv_thanhPhanChuaThamGia.RowHeadersVisible = false;
            this.dgv_thanhPhanChuaThamGia.Size = new System.Drawing.Size(467, 482);
            this.dgv_thanhPhanChuaThamGia.TabIndex = 0;
            this.dgv_thanhPhanChuaThamGia.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_thanhPhanChuaThamGia_CellContentClick);
            // 
            // Them
            // 
            this.Them.HeaderText = "Thêm";
            this.Them.Name = "Them";
            this.Them.ReadOnly = true;
            this.Them.Text = "Thêm";
            this.Them.UseColumnTextForButtonValue = true;
            // 
            // dgv_thanhPhanThamGia
            // 
            this.dgv_thanhPhanThamGia.AllowUserToAddRows = false;
            this.dgv_thanhPhanThamGia.AllowUserToDeleteRows = false;
            this.dgv_thanhPhanThamGia.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgv_thanhPhanThamGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_thanhPhanThamGia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Xoa});
            this.dgv_thanhPhanThamGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_thanhPhanThamGia.Location = new System.Drawing.Point(3, 16);
            this.dgv_thanhPhanThamGia.Name = "dgv_thanhPhanThamGia";
            this.dgv_thanhPhanThamGia.ReadOnly = true;
            this.dgv_thanhPhanThamGia.RowHeadersVisible = false;
            this.dgv_thanhPhanThamGia.Size = new System.Drawing.Size(466, 482);
            this.dgv_thanhPhanThamGia.TabIndex = 1;
            this.dgv_thanhPhanThamGia.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_thanhPhanThamGia_CellContentClick);
            // 
            // Xoa
            // 
            this.Xoa.HeaderText = "Xóa";
            this.Xoa.Name = "Xoa";
            this.Xoa.ReadOnly = true;
            this.Xoa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Xoa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Xoa.Text = "Xóa";
            this.Xoa.UseColumnTextForButtonValue = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.875F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 507F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(957, 507);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_thanhPhanThamGia);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(482, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 501);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thành phần đang tham gia";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_thanhPhanChuaThamGia);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 501);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thành phần không tham gia";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_LuuThayDoi);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 507);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(957, 36);
            this.panel1.TabIndex = 3;
            // 
            // bt_LuuThayDoi
            // 
            this.bt_LuuThayDoi.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_LuuThayDoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_LuuThayDoi.Location = new System.Drawing.Point(858, 0);
            this.bt_LuuThayDoi.Name = "bt_LuuThayDoi";
            this.bt_LuuThayDoi.Size = new System.Drawing.Size(99, 36);
            this.bt_LuuThayDoi.TabIndex = 0;
            this.bt_LuuThayDoi.Text = "Lưu thay đổi";
            this.bt_LuuThayDoi.UseVisualStyleBackColor = true;
            this.bt_LuuThayDoi.Click += new System.EventHandler(this.bt_LuuThayDoi_Click);
            // 
            // Form_ThemCaNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 543);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "Form_ThemCaNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn thành phần tham gia";
            this.Load += new System.EventHandler(this.Form_TTCT_ThanhPhanThamGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_thanhPhanChuaThamGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_thanhPhanThamGia)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_thanhPhanChuaThamGia;
        private System.Windows.Forms.DataGridView dgv_thanhPhanThamGia;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridViewButtonColumn Them;
        private System.Windows.Forms.DataGridViewButtonColumn Xoa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_LuuThayDoi;
    }
}