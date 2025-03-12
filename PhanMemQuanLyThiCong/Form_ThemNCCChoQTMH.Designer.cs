
namespace PhanMemQuanLyThiCong
{
    partial class Form_ThemNCCChoQTMH
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
            this.dgv_listNCC = new System.Windows.Forms.DataGridView();
            this.dgv_NCCDaChon = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NCC = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.VT = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bt_HuyThayDoiNCC = new System.Windows.Forms.Button();
            this.bt_LuuThayDoiNCC = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listNCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_NCCDaChon)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_listNCC
            // 
            this.dgv_listNCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listNCC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_listNCC.Location = new System.Drawing.Point(3, 16);
            this.dgv_listNCC.Name = "dgv_listNCC";
            this.dgv_listNCC.RowHeadersVisible = false;
            this.dgv_listNCC.Size = new System.Drawing.Size(668, 177);
            this.dgv_listNCC.TabIndex = 0;
            // 
            // dgv_NCCDaChon
            // 
            this.dgv_NCCDaChon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_NCCDaChon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.NCC,
            this.VT,
            this.DV,
            this.KL});
            this.dgv_NCCDaChon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_NCCDaChon.Location = new System.Drawing.Point(3, 16);
            this.dgv_NCCDaChon.Name = "dgv_NCCDaChon";
            this.dgv_NCCDaChon.RowHeadersVisible = false;
            this.dgv_NCCDaChon.Size = new System.Drawing.Size(794, 232);
            this.dgv_NCCDaChon.TabIndex = 1;
            this.dgv_NCCDaChon.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_NCCDaChon_CellValueChanged);
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            // 
            // NCC
            // 
            this.NCC.HeaderText = "Nhà cung cấp";
            this.NCC.Name = "NCC";
            this.NCC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NCC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // VT
            // 
            this.VT.HeaderText = "Vật tư";
            this.VT.Name = "VT";
            // 
            // DV
            // 
            this.DV.HeaderText = "Đơn Vị";
            this.DV.Name = "DV";
            this.DV.ReadOnly = true;
            // 
            // KL
            // 
            this.KL.HeaderText = "Khối lượng";
            this.KL.Name = "KL";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.dgv_NCCDaChon);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(800, 251);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nhà cung cấp đã chọn";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 196);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(800, 3);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_listNCC);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 196);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhập thêm nhà cung cấp đã báo giá";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bt_HuyThayDoiNCC);
            this.panel2.Controls.Add(this.bt_LuuThayDoiNCC);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(671, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(126, 177);
            this.panel2.TabIndex = 2;
            // 
            // bt_HuyThayDoiNCC
            // 
            this.bt_HuyThayDoiNCC.BackColor = System.Drawing.Color.Red;
            this.bt_HuyThayDoiNCC.Dock = System.Windows.Forms.DockStyle.Top;
            this.bt_HuyThayDoiNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_HuyThayDoiNCC.Location = new System.Drawing.Point(0, 23);
            this.bt_HuyThayDoiNCC.Name = "bt_HuyThayDoiNCC";
            this.bt_HuyThayDoiNCC.Size = new System.Drawing.Size(126, 33);
            this.bt_HuyThayDoiNCC.TabIndex = 1;
            this.bt_HuyThayDoiNCC.Text = "Hủy thay đổi";
            this.bt_HuyThayDoiNCC.UseVisualStyleBackColor = false;
            // 
            // bt_LuuThayDoiNCC
            // 
            this.bt_LuuThayDoiNCC.Dock = System.Windows.Forms.DockStyle.Top;
            this.bt_LuuThayDoiNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_LuuThayDoiNCC.Location = new System.Drawing.Point(0, 0);
            this.bt_LuuThayDoiNCC.Name = "bt_LuuThayDoiNCC";
            this.bt_LuuThayDoiNCC.Size = new System.Drawing.Size(126, 23);
            this.bt_LuuThayDoiNCC.TabIndex = 1;
            this.bt_LuuThayDoiNCC.Text = "Lưu thay đổi";
            this.bt_LuuThayDoiNCC.UseVisualStyleBackColor = true;
            this.bt_LuuThayDoiNCC.Click += new System.EventHandler(this.bt_LuuThayDoiNCC_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 196);
            this.panel1.TabIndex = 8;
            // 
            // Form_ThemNCCChoQTMH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "Form_ThemNCCChoQTMH";
            this.Text = "Quản lý danh sách nhà cung cấp đã báo giá";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ThemNCCChoQTMH_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listNCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_NCCDaChon)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_listNCC;
        private System.Windows.Forms.DataGridView dgv_NCCDaChon;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bt_HuyThayDoiNCC;
        private System.Windows.Forms.Button bt_LuuThayDoiNCC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewComboBoxColumn NCC;
        private System.Windows.Forms.DataGridViewComboBoxColumn VT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DV;
        private System.Windows.Forms.DataGridViewTextBoxColumn KL;
    }
}