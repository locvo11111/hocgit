
namespace PhanMemQuanLyThiCong
{
    partial class Form_ThemDauViecTuQTMH
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
            this.dgv_ListQTMH = new System.Windows.Forms.DataGridView();
            this.dgv_listVatTuDeXuat = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.Chon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bt_Huy = new System.Windows.Forms.Button();
            this.bt_Thêm = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_TenCongTac = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ListQTMH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listVatTuDeXuat)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_ListQTMH
            // 
            this.dgv_ListQTMH.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgv_ListQTMH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ListQTMH.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgv_ListQTMH.GridColor = System.Drawing.Color.White;
            this.dgv_ListQTMH.Location = new System.Drawing.Point(0, 0);
            this.dgv_ListQTMH.Name = "dgv_ListQTMH";
            this.dgv_ListQTMH.Size = new System.Drawing.Size(240, 420);
            this.dgv_ListQTMH.TabIndex = 0;
            this.dgv_ListQTMH.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ListQTMH_CellClick);
            // 
            // dgv_listVatTuDeXuat
            // 
            this.dgv_listVatTuDeXuat.AllowUserToAddRows = false;
            this.dgv_listVatTuDeXuat.AllowUserToDeleteRows = false;
            this.dgv_listVatTuDeXuat.BackgroundColor = System.Drawing.Color.White;
            this.dgv_listVatTuDeXuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listVatTuDeXuat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Chon});
            this.dgv_listVatTuDeXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_listVatTuDeXuat.Location = new System.Drawing.Point(240, 0);
            this.dgv_listVatTuDeXuat.Name = "dgv_listVatTuDeXuat";
            this.dgv_listVatTuDeXuat.RowHeadersVisible = false;
            this.dgv_listVatTuDeXuat.Size = new System.Drawing.Size(560, 420);
            this.dgv_listVatTuDeXuat.TabIndex = 1;
            this.dgv_listVatTuDeXuat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_listVatTuDeXuat_CellContentClick);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(240, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 420);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // Chon
            // 
            this.Chon.HeaderText = "Chọn";
            this.Chon.Name = "Chon";
            // 
            // bt_Huy
            // 
            this.bt_Huy.BackColor = System.Drawing.Color.Red;
            this.bt_Huy.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_Huy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Huy.Location = new System.Drawing.Point(725, 0);
            this.bt_Huy.Name = "bt_Huy";
            this.bt_Huy.Size = new System.Drawing.Size(75, 30);
            this.bt_Huy.TabIndex = 0;
            this.bt_Huy.Text = "Hủy";
            this.bt_Huy.UseVisualStyleBackColor = false;
            this.bt_Huy.Click += new System.EventHandler(this.bt_Huy_Click);
            // 
            // bt_Thêm
            // 
            this.bt_Thêm.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_Thêm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Thêm.Location = new System.Drawing.Point(650, 0);
            this.bt_Thêm.Name = "bt_Thêm";
            this.bt_Thêm.Size = new System.Drawing.Size(75, 30);
            this.bt_Thêm.TabIndex = 0;
            this.bt_Thêm.Text = "Thêm";
            this.bt_Thêm.UseVisualStyleBackColor = true;
            this.bt_Thêm.Click += new System.EventHandler(this.bt_Thêm_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nhập tên công tác:";
            // 
            // txt_TenCongTac
            // 
            this.txt_TenCongTac.Location = new System.Drawing.Point(140, 5);
            this.txt_TenCongTac.Name = "txt_TenCongTac";
            this.txt_TenCongTac.Size = new System.Drawing.Size(289, 20);
            this.txt_TenCongTac.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_TenCongTac);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.bt_Thêm);
            this.panel1.Controls.Add(this.bt_Huy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 420);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 30);
            this.panel1.TabIndex = 2;
            // 
            // Form_ThemDauViecTuQTMH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.dgv_listVatTuDeXuat);
            this.Controls.Add(this.dgv_ListQTMH);
            this.Controls.Add(this.panel1);
            this.Name = "Form_ThemDauViecTuQTMH";
            this.Text = "Thêm đầu việc từ quy trình mua hàng";
            this.Load += new System.EventHandler(this.Form_ThemDauViecTuQTMH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ListQTMH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listVatTuDeXuat)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ListQTMH;
        private System.Windows.Forms.DataGridView dgv_listVatTuDeXuat;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chon;
        private System.Windows.Forms.Button bt_Huy;
        private System.Windows.Forms.Button bt_Thêm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_TenCongTac;
        private System.Windows.Forms.Panel panel1;
    }
}