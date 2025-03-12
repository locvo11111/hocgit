
namespace PhanMemQuanLyThiCong
{
    partial class FormKeHoachVatTu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKeHoachVatTu));
            this.spsheet_XemFile = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkduongdan = new System.Windows.Forms.LinkLabel();
            this.btn_VL_Hoantat = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtp_end = new System.Windows.Forms.DateTimePicker();
            this.dtp_begin = new System.Windows.Forms.DateTimePicker();
            this.nud_VL_End = new System.Windows.Forms.NumericUpDown();
            this.nud_VL_Begin = new System.Windows.Forms.NumericUpDown();
            this.label212 = new System.Windows.Forms.Label();
            this.label213 = new System.Windows.Forms.Label();
            this.cbb_VL_tensheet = new System.Windows.Forms.ComboBox();
            this.label218 = new System.Windows.Forms.Label();
            this.tb_DonGiaKeHoach = new System.Windows.Forms.TextBox();
            this.txt_VL_Dongiagoc = new System.Windows.Forms.TextBox();
            this.txt_VL_Khoiluong = new System.Windows.Forms.TextBox();
            this.txt_VL_TenVL = new System.Windows.Forms.TextBox();
            this.txt_VL_Donvi = new System.Windows.Forms.TextBox();
            this.txt_VL_Mahieu = new System.Windows.Forms.TextBox();
            this.txt_VT_STT = new System.Windows.Forms.TextBox();
            this.label215 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label217 = new System.Windows.Forms.Label();
            this.label219 = new System.Windows.Forms.Label();
            this.label220 = new System.Windows.Forms.Label();
            this.label221 = new System.Windows.Forms.Label();
            this.label222 = new System.Windows.Forms.Label();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_VL_End)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_VL_Begin)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spsheet_XemFile
            // 
            this.spsheet_XemFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spsheet_XemFile.Location = new System.Drawing.Point(2, 123);
            this.spsheet_XemFile.Margin = new System.Windows.Forms.Padding(2);
            this.spsheet_XemFile.Name = "spsheet_XemFile";
            this.spsheet_XemFile.Options.Import.Csv.Encoding = ((System.Text.Encoding)(resources.GetObject("spsheet_XemFile.Options.Import.Csv.Encoding")));
            this.spsheet_XemFile.Options.Import.Txt.Encoding = ((System.Text.Encoding)(resources.GetObject("spsheet_XemFile.Options.Import.Txt.Encoding")));
            this.spsheet_XemFile.Size = new System.Drawing.Size(1211, 484);
            this.spsheet_XemFile.TabIndex = 0;
            this.spsheet_XemFile.Text = "spreadsheetControl1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.linkduongdan);
            this.panel3.Controls.Add(this.btn_VL_Hoantat);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(982, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(227, 115);
            this.panel3.TabIndex = 4;
            // 
            // linkduongdan
            // 
            this.linkduongdan.AutoSize = true;
            this.linkduongdan.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkduongdan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkduongdan.Location = new System.Drawing.Point(0, 0);
            this.linkduongdan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkduongdan.Name = "linkduongdan";
            this.linkduongdan.Size = new System.Drawing.Size(104, 17);
            this.linkduongdan.TabIndex = 0;
            this.linkduongdan.TabStop = true;
            this.linkduongdan.Text = "Đường dẫn File";
            this.linkduongdan.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkduongdan_LinkClicked);
            // 
            // btn_VL_Hoantat
            // 
            this.btn_VL_Hoantat.BackColor = System.Drawing.Color.Yellow;
            this.btn_VL_Hoantat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_VL_Hoantat.Image = global::PhanMemQuanLyThiCong.Properties.Resources.xls;
            this.btn_VL_Hoantat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_VL_Hoantat.Location = new System.Drawing.Point(0, 77);
            this.btn_VL_Hoantat.Margin = new System.Windows.Forms.Padding(2);
            this.btn_VL_Hoantat.Name = "btn_VL_Hoantat";
            this.btn_VL_Hoantat.Size = new System.Drawing.Size(227, 38);
            this.btn_VL_Hoantat.TabIndex = 144;
            this.btn_VL_Hoantat.Text = "Đọc vào";
            this.btn_VL_Hoantat.UseVisualStyleBackColor = false;
            this.btn_VL_Hoantat.Click += new System.EventHandler(this.btn_VL_Hoantat_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtp_end);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.dtp_begin);
            this.panel2.Controls.Add(this.nud_VL_End);
            this.panel2.Controls.Add(this.nud_VL_Begin);
            this.panel2.Controls.Add(this.label212);
            this.panel2.Controls.Add(this.label213);
            this.panel2.Controls.Add(this.cbb_VL_tensheet);
            this.panel2.Controls.Add(this.label218);
            this.panel2.Controls.Add(this.tb_DonGiaKeHoach);
            this.panel2.Controls.Add(this.txt_VL_Dongiagoc);
            this.panel2.Controls.Add(this.txt_VL_Khoiluong);
            this.panel2.Controls.Add(this.txt_VL_TenVL);
            this.panel2.Controls.Add(this.txt_VL_Donvi);
            this.panel2.Controls.Add(this.txt_VL_Mahieu);
            this.panel2.Controls.Add(this.txt_VT_STT);
            this.panel2.Controls.Add(this.label215);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label217);
            this.panel2.Controls.Add(this.label219);
            this.panel2.Controls.Add(this.label220);
            this.panel2.Controls.Add(this.label221);
            this.panel2.Controls.Add(this.label222);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1209, 115);
            this.panel2.TabIndex = 5;
            // 
            // dtp_end
            // 
            this.dtp_end.CalendarFont = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_end.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_end.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_end.Location = new System.Drawing.Point(787, 72);
            this.dtp_end.Name = "dtp_end";
            this.dtp_end.Size = new System.Drawing.Size(177, 26);
            this.dtp_end.TabIndex = 145;
            // 
            // dtp_begin
            // 
            this.dtp_begin.CalendarFont = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_begin.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_begin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_begin.Location = new System.Drawing.Point(787, 27);
            this.dtp_begin.Name = "dtp_begin";
            this.dtp_begin.Size = new System.Drawing.Size(177, 26);
            this.dtp_begin.TabIndex = 145;
            // 
            // nud_VL_End
            // 
            this.nud_VL_End.Location = new System.Drawing.Point(461, 78);
            this.nud_VL_End.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nud_VL_End.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_VL_End.Name = "nud_VL_End";
            this.nud_VL_End.Size = new System.Drawing.Size(91, 20);
            this.nud_VL_End.TabIndex = 142;
            this.nud_VL_End.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nud_VL_Begin
            // 
            this.nud_VL_Begin.Location = new System.Drawing.Point(272, 78);
            this.nud_VL_Begin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_VL_Begin.Name = "nud_VL_Begin";
            this.nud_VL_Begin.Size = new System.Drawing.Size(91, 20);
            this.nud_VL_Begin.TabIndex = 143;
            this.nud_VL_Begin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label212
            // 
            this.label212.AutoSize = true;
            this.label212.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label212.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label212.Location = new System.Drawing.Point(383, 80);
            this.label212.Name = "label212";
            this.label212.Size = new System.Drawing.Size(71, 16);
            this.label212.TabIndex = 141;
            this.label212.Text = "Đến dòng: ";
            // 
            // label213
            // 
            this.label213.AutoSize = true;
            this.label213.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label213.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label213.Location = new System.Drawing.Point(181, 80);
            this.label213.Name = "label213";
            this.label213.Size = new System.Drawing.Size(84, 16);
            this.label213.TabIndex = 140;
            this.label213.Text = "Đọc từ dòng: ";
            // 
            // cbb_VL_tensheet
            // 
            this.cbb_VL_tensheet.FormattingEnabled = true;
            this.cbb_VL_tensheet.Location = new System.Drawing.Point(56, 78);
            this.cbb_VL_tensheet.Name = "cbb_VL_tensheet";
            this.cbb_VL_tensheet.Size = new System.Drawing.Size(106, 21);
            this.cbb_VL_tensheet.TabIndex = 139;
            this.cbb_VL_tensheet.SelectedIndexChanged += new System.EventHandler(this.cbb_VL_tensheet_SelectedIndexChanged_1);
            // 
            // label218
            // 
            this.label218.AutoSize = true;
            this.label218.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label218.Location = new System.Drawing.Point(12, 82);
            this.label218.Name = "label218";
            this.label218.Size = new System.Drawing.Size(40, 13);
            this.label218.TabIndex = 138;
            this.label218.Text = "Sheet";
            // 
            // tb_DonGiaKeHoach
            // 
            this.tb_DonGiaKeHoach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tb_DonGiaKeHoach.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tb_DonGiaKeHoach.Location = new System.Drawing.Point(552, 31);
            this.tb_DonGiaKeHoach.Name = "tb_DonGiaKeHoach";
            this.tb_DonGiaKeHoach.Size = new System.Drawing.Size(35, 20);
            this.tb_DonGiaKeHoach.TabIndex = 88;
            // 
            // txt_VL_Dongiagoc
            // 
            this.txt_VL_Dongiagoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_VL_Dongiagoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_VL_Dongiagoc.Location = new System.Drawing.Point(461, 31);
            this.txt_VL_Dongiagoc.Name = "txt_VL_Dongiagoc";
            this.txt_VL_Dongiagoc.Size = new System.Drawing.Size(35, 20);
            this.txt_VL_Dongiagoc.TabIndex = 88;
            // 
            // txt_VL_Khoiluong
            // 
            this.txt_VL_Khoiluong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_VL_Khoiluong.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_VL_Khoiluong.Location = new System.Drawing.Point(357, 31);
            this.txt_VL_Khoiluong.Name = "txt_VL_Khoiluong";
            this.txt_VL_Khoiluong.Size = new System.Drawing.Size(54, 20);
            this.txt_VL_Khoiluong.TabIndex = 87;
            // 
            // txt_VL_TenVL
            // 
            this.txt_VL_TenVL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_VL_TenVL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_VL_TenVL.Location = new System.Drawing.Point(271, 31);
            this.txt_VL_TenVL.Name = "txt_VL_TenVL";
            this.txt_VL_TenVL.Size = new System.Drawing.Size(53, 20);
            this.txt_VL_TenVL.TabIndex = 86;
            // 
            // txt_VL_Donvi
            // 
            this.txt_VL_Donvi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_VL_Donvi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_VL_Donvi.Location = new System.Drawing.Point(186, 31);
            this.txt_VL_Donvi.Name = "txt_VL_Donvi";
            this.txt_VL_Donvi.Size = new System.Drawing.Size(37, 20);
            this.txt_VL_Donvi.TabIndex = 85;
            // 
            // txt_VL_Mahieu
            // 
            this.txt_VL_Mahieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_VL_Mahieu.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_VL_Mahieu.Location = new System.Drawing.Point(91, 31);
            this.txt_VL_Mahieu.Name = "txt_VL_Mahieu";
            this.txt_VL_Mahieu.Size = new System.Drawing.Size(37, 20);
            this.txt_VL_Mahieu.TabIndex = 84;
            // 
            // txt_VT_STT
            // 
            this.txt_VT_STT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_VT_STT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_VT_STT.Location = new System.Drawing.Point(9, 31);
            this.txt_VT_STT.Name = "txt_VT_STT";
            this.txt_VT_STT.Size = new System.Drawing.Size(37, 20);
            this.txt_VT_STT.TabIndex = 83;
            // 
            // label215
            // 
            this.label215.AutoSize = true;
            this.label215.Location = new System.Drawing.Point(268, 13);
            this.label215.Name = "label215";
            this.label215.Size = new System.Drawing.Size(56, 13);
            this.label215.TabIndex = 82;
            this.label215.Text = "Tên vật tư";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(681, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 19);
            this.label2.TabIndex = 81;
            this.label2.Text = "Thời gian đến";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(681, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 19);
            this.label1.TabIndex = 81;
            this.label1.Text = "Thời gian từ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(533, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "Đơn giá kế hoạch";
            // 
            // label217
            // 
            this.label217.AutoSize = true;
            this.label217.Location = new System.Drawing.Point(445, 13);
            this.label217.Name = "label217";
            this.label217.Size = new System.Drawing.Size(65, 13);
            this.label217.TabIndex = 81;
            this.label217.Text = "Đơn giá gốc";
            // 
            // label219
            // 
            this.label219.AutoSize = true;
            this.label219.Location = new System.Drawing.Point(354, 13);
            this.label219.Name = "label219";
            this.label219.Size = new System.Drawing.Size(57, 13);
            this.label219.TabIndex = 80;
            this.label219.Text = "Khối lượng";
            // 
            // label220
            // 
            this.label220.AutoSize = true;
            this.label220.Location = new System.Drawing.Point(186, 13);
            this.label220.Name = "label220";
            this.label220.Size = new System.Drawing.Size(38, 13);
            this.label220.TabIndex = 79;
            this.label220.Text = "Đơn vị";
            // 
            // label221
            // 
            this.label221.AutoSize = true;
            this.label221.Location = new System.Drawing.Point(88, 13);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(45, 13);
            this.label221.TabIndex = 78;
            this.label221.Text = "Mã hiệu";
            // 
            // label222
            // 
            this.label222.AutoSize = true;
            this.label222.Location = new System.Drawing.Point(9, 13);
            this.label222.Name = "label222";
            this.label222.Size = new System.Drawing.Size(37, 13);
            this.label222.TabIndex = 77;
            this.label222.Text = "Số TT";
            // 
            // m_openFileDialog
            // 
            this.m_openFileDialog.FileName = "openFileDialog1";
            this.m_openFileDialog.Filter = "EXCEL (*.xls, *.xlsx,*.csv)|*.xls;*.xlsx;*.csv";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.spsheet_XemFile, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1215, 609);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // FormKeHoachVatTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 609);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormKeHoachVatTu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormKeHoachVatTu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormKeHoachVatTu_FormClosed);
            this.Load += new System.EventHandler(this.FormKeHoachVatTu_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_VL_End)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_VL_Begin)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spsheet_XemFile;
        private System.Windows.Forms.LinkLabel linkduongdan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_VL_Dongiagoc;
        private System.Windows.Forms.TextBox txt_VL_Khoiluong;
        private System.Windows.Forms.TextBox txt_VL_TenVL;
        private System.Windows.Forms.TextBox txt_VL_Donvi;
        private System.Windows.Forms.TextBox txt_VL_Mahieu;
        private System.Windows.Forms.TextBox txt_VT_STT;
        private System.Windows.Forms.Label label215;
        private System.Windows.Forms.Label label217;
        private System.Windows.Forms.Label label219;
        private System.Windows.Forms.Label label220;
        private System.Windows.Forms.Label label221;
        private System.Windows.Forms.Label label222;
        private System.Windows.Forms.NumericUpDown nud_VL_End;
        private System.Windows.Forms.NumericUpDown nud_VL_Begin;
        private System.Windows.Forms.Label label212;
        private System.Windows.Forms.Label label213;
        private System.Windows.Forms.ComboBox cbb_VL_tensheet;
        private System.Windows.Forms.Label label218;
        private System.Windows.Forms.Button btn_VL_Hoantat;
        private System.Windows.Forms.DateTimePicker dtp_end;
        private System.Windows.Forms.DateTimePicker dtp_begin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog m_openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tb_DonGiaKeHoach;
        private System.Windows.Forms.Label label3;
    }
}