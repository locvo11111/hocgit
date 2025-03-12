
namespace PhanMemQuanLyThiCong
{
    partial class FormLuachon_HopDong
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bt_themfile = new System.Windows.Forms.Button();
            this.lnkb_duongdan = new System.Windows.Forms.LinkLabel();
            this.dtg_luachon = new System.Windows.Forms.DataGridView();
            this.Xóa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ThayThế = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Xemtrước = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_luachon)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_themfile
            // 
            this.bt_themfile.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.bt_themfile.Location = new System.Drawing.Point(757, 1);
            this.bt_themfile.Name = "bt_themfile";
            this.bt_themfile.Size = new System.Drawing.Size(243, 83);
            this.bt_themfile.TabIndex = 5;
            this.bt_themfile.Text = "THÊM FILE";
            this.bt_themfile.UseVisualStyleBackColor = false;
            this.bt_themfile.Click += new System.EventHandler(this.bt_themfile_Click);
            // 
            // lnkb_duongdan
            // 
            this.lnkb_duongdan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lnkb_duongdan.Location = new System.Drawing.Point(-3, 1);
            this.lnkb_duongdan.Name = "lnkb_duongdan";
            this.lnkb_duongdan.Size = new System.Drawing.Size(754, 83);
            this.lnkb_duongdan.TabIndex = 7;
            this.lnkb_duongdan.TabStop = true;
            this.lnkb_duongdan.Text = "Đường Dẫn File";
            // 
            // dtg_luachon
            // 
            this.dtg_luachon.AllowUserToAddRows = false;
            this.dtg_luachon.AllowUserToDeleteRows = false;
            this.dtg_luachon.AllowUserToOrderColumns = true;
            this.dtg_luachon.BackgroundColor = System.Drawing.Color.White;
            this.dtg_luachon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_luachon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Xóa,
            this.ThayThế,
            this.Xemtrước});
            this.dtg_luachon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtg_luachon.Location = new System.Drawing.Point(0, 90);
            this.dtg_luachon.Name = "dtg_luachon";
            this.dtg_luachon.RowHeadersWidth = 51;
            this.dtg_luachon.RowTemplate.Height = 24;
            this.dtg_luachon.Size = new System.Drawing.Size(998, 412);
            this.dtg_luachon.TabIndex = 8;
            this.dtg_luachon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtg_luachon_CellContentClick);
            // 
            // Xóa
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Red;
            this.Xóa.DefaultCellStyle = dataGridViewCellStyle10;
            this.Xóa.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Xóa.HeaderText = "Xóa";
            this.Xóa.MinimumWidth = 6;
            this.Xóa.Name = "Xóa";
            this.Xóa.Text = "Xóa";
            this.Xóa.UseColumnTextForButtonValue = true;
            this.Xóa.Width = 125;
            // 
            // ThayThế
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.LightGray;
            this.ThayThế.DefaultCellStyle = dataGridViewCellStyle11;
            this.ThayThế.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ThayThế.HeaderText = "Thay thế";
            this.ThayThế.MinimumWidth = 6;
            this.ThayThế.Name = "ThayThế";
            this.ThayThế.Text = "Thay thế";
            this.ThayThế.UseColumnTextForButtonValue = true;
            this.ThayThế.Width = 125;
            // 
            // Xemtrước
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Aqua;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.Xemtrước.DefaultCellStyle = dataGridViewCellStyle12;
            this.Xemtrước.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Xemtrước.HeaderText = "Xem trước";
            this.Xemtrước.MinimumWidth = 6;
            this.Xemtrước.Name = "Xemtrước";
            this.Xemtrước.Text = "Xem trước";
            this.Xemtrước.UseColumnTextForButtonValue = true;
            this.Xemtrước.Width = 125;
            // 
            // FormLuachon_HopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 502);
            this.Controls.Add(this.dtg_luachon);
            this.Controls.Add(this.lnkb_duongdan);
            this.Controls.Add(this.bt_themfile);
            this.Name = "FormLuachon_HopDong";
            this.Text = "FormLuachon_HopDong";
            this.Load += new System.EventHandler(this.FormLuachon_HopDong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg_luachon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_themfile;
        private System.Windows.Forms.LinkLabel lnkb_duongdan;
        private System.Windows.Forms.DataGridView dtg_luachon;
        private System.Windows.Forms.DataGridViewButtonColumn Xóa;
        private System.Windows.Forms.DataGridViewButtonColumn ThayThế;
        private System.Windows.Forms.DataGridViewButtonColumn Xemtrước;
    }
}