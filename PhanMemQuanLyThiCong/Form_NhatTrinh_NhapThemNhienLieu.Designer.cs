
namespace PhanMemQuanLyThiCong
{
    partial class Form_NhatTrinh_NhapThemNhienLieu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_NhatTrinh_NhapThemNhienLieu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Nhap = new System.Windows.Forms.Button();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NTNL_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NTNL_STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Nhap);
            this.panel1.Controls.Add(this.monthCalendar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 223);
            this.panel1.TabIndex = 0;
            // 
            // btn_Nhap
            // 
            this.btn_Nhap.Location = new System.Drawing.Point(115, 78);
            this.btn_Nhap.Name = "btn_Nhap";
            this.btn_Nhap.Size = new System.Drawing.Size(126, 71);
            this.btn_Nhap.TabIndex = 2;
            this.btn_Nhap.Text = "Nhập nhiên liệu";
            this.btn_Nhap.UseVisualStyleBackColor = true;
            this.btn_Nhap.Click += new System.EventHandler(this.btn_Nhap_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(551, 10);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NTNL_Code,
            this.NTNL_STT});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 223);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(789, 360);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // NTNL_Code
            // 
            this.NTNL_Code.HeaderText = "Code";
            this.NTNL_Code.MinimumWidth = 6;
            this.NTNL_Code.Name = "NTNL_Code";
            this.NTNL_Code.Width = 125;
            // 
            // NTNL_STT
            // 
            this.NTNL_STT.HeaderText = "STT";
            this.NTNL_STT.MinimumWidth = 6;
            this.NTNL_STT.Name = "NTNL_STT";
            this.NTNL_STT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.NTNL_STT.Width = 40;
            // 
            // Form_NhatTrinh_NhapThemNhienLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 583);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_NhatTrinh_NhapThemNhienLieu";
            this.Text = "Fom nhập thêm nhiên liệu dầu -xăng - nhớt";
            this.Load += new System.EventHandler(this.Form_NhatTrinh_NhapThemNhienLieu_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Nhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn NTNL_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn NTNL_STT;
    }
}