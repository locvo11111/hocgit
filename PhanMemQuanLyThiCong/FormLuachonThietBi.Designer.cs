
namespace PhanMemQuanLyThiCong
{
    partial class FormLuachonThietBi
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
            this.btn_thoat = new System.Windows.Forms.Button();
            this.btn_chon = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.luachon_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_thoat);
            this.panel1.Controls.Add(this.btn_chon);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(890, 450);
            this.panel1.TabIndex = 0;
            // 
            // btn_thoat
            // 
            this.btn_thoat.Location = new System.Drawing.Point(426, 368);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(108, 53);
            this.btn_thoat.TabIndex = 1;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.UseVisualStyleBackColor = true;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // btn_chon
            // 
            this.btn_chon.Location = new System.Drawing.Point(218, 368);
            this.btn_chon.Name = "btn_chon";
            this.btn_chon.Size = new System.Drawing.Size(108, 53);
            this.btn_chon.TabIndex = 1;
            this.btn_chon.Text = "Ok";
            this.btn_chon.UseVisualStyleBackColor = true;
            this.btn_chon.Click += new System.EventHandler(this.btn_chon_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.luachon_code});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(890, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // check
            // 
            this.check.HeaderText = "Chọn";
            this.check.MinimumWidth = 6;
            this.check.Name = "check";
            this.check.Width = 125;
            // 
            // luachon_code
            // 
            this.luachon_code.HeaderText = "Code";
            this.luachon_code.MinimumWidth = 6;
            this.luachon_code.Name = "luachon_code";
            this.luachon_code.Width = 125;
            // 
            // FormLuachonThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FormLuachonThietBi";
            this.Text = "Lựa ChọnThiết Bị";
            this.Load += new System.EventHandler(this.FormLuachonThietBi_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_thoat;
        private System.Windows.Forms.Button btn_chon;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn luachon_code;
    }
}