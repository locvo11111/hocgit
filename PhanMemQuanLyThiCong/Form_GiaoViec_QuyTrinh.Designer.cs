
namespace PhanMemQuanLyThiCong
{
    partial class Form_GiaoViec_QuyTrinh
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
            this.cbb_TenCongViecConHienTai = new System.Windows.Forms.ComboBox();
            this.label376 = new System.Windows.Forms.Label();
            this.gr_CVTT = new System.Windows.Forms.GroupBox();
            this.gr_xemTruocNgayCVTT = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_NgayKTCVTT = new System.Windows.Forms.DateTimePicker();
            this.dtp_NgayBDCVTT = new System.Windows.Forms.DateTimePicker();
            this.nud_SoNgayThucHienCVTT = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nud_soNgaySoVoiCongTacTruoc = new System.Windows.Forms.NumericUpDown();
            this.cbb_CVChaTiepTheo = new System.Windows.Forms.ComboBox();
            this.cbb_GV_NTT_TrangThai = new System.Windows.Forms.ComboBox();
            this.label383 = new System.Windows.Forms.Label();
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc = new System.Windows.Forms.ComboBox();
            this.label379 = new System.Windows.Forms.Label();
            this.cbb_CVConTiepTheo = new System.Windows.Forms.ComboBox();
            this.label378 = new System.Windows.Forms.Label();
            this.label377 = new System.Windows.Forms.Label();
            this.label373 = new System.Windows.Forms.Label();
            this.dtp_GV_TGKetThuc = new System.Windows.Forms.DateTimePicker();
            this.label372 = new System.Windows.Forms.Label();
            this.label371 = new System.Windows.Forms.Label();
            this.cbb_TenCongViecChaHienTai = new System.Windows.Forms.ComboBox();
            this.label368 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pn_thoiGianCVHT = new System.Windows.Forms.Panel();
            this.nud_soNgayCVHT = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_HoanTat = new System.Windows.Forms.Button();
            this.bt_Huy = new System.Windows.Forms.Button();
            this.dtp_GV_TGBatDau = new System.Windows.Forms.DateTimePicker();
            this.gr_CVTT.SuspendLayout();
            this.gr_xemTruocNgayCVTT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_SoNgayThucHienCVTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_soNgaySoVoiCongTacTruoc)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pn_thoiGianCVHT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_soNgayCVHT)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbb_TenCongViecConHienTai
            // 
            this.cbb_TenCongViecConHienTai.DisplayMember = "Value";
            this.cbb_TenCongViecConHienTai.FormattingEnabled = true;
            this.cbb_TenCongViecConHienTai.Location = new System.Drawing.Point(22, 88);
            this.cbb_TenCongViecConHienTai.Name = "cbb_TenCongViecConHienTai";
            this.cbb_TenCongViecConHienTai.Size = new System.Drawing.Size(269, 21);
            this.cbb_TenCongViecConHienTai.TabIndex = 16;
            this.cbb_TenCongViecConHienTai.ValueMember = "Key";
            this.cbb_TenCongViecConHienTai.SelectedIndexChanged += new System.EventHandler(this.cbb_TenCongViecConHienTai_SelectedIndexChanged);
            // 
            // label376
            // 
            this.label376.AutoSize = true;
            this.label376.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label376.Location = new System.Drawing.Point(22, 73);
            this.label376.Name = "label376";
            this.label376.Size = new System.Drawing.Size(78, 15);
            this.label376.TabIndex = 15;
            this.label376.Text = "Công tác con";
            // 
            // gr_CVTT
            // 
            this.gr_CVTT.Controls.Add(this.gr_xemTruocNgayCVTT);
            this.gr_CVTT.Controls.Add(this.nud_SoNgayThucHienCVTT);
            this.gr_CVTT.Controls.Add(this.label1);
            this.gr_CVTT.Controls.Add(this.nud_soNgaySoVoiCongTacTruoc);
            this.gr_CVTT.Controls.Add(this.cbb_CVChaTiepTheo);
            this.gr_CVTT.Controls.Add(this.cbb_GV_NTT_TrangThai);
            this.gr_CVTT.Controls.Add(this.label383);
            this.gr_CVTT.Controls.Add(this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc);
            this.gr_CVTT.Controls.Add(this.label379);
            this.gr_CVTT.Controls.Add(this.cbb_CVConTiepTheo);
            this.gr_CVTT.Controls.Add(this.label378);
            this.gr_CVTT.Controls.Add(this.label377);
            this.gr_CVTT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr_CVTT.Enabled = false;
            this.gr_CVTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gr_CVTT.Location = new System.Drawing.Point(403, 3);
            this.gr_CVTT.Name = "gr_CVTT";
            this.gr_CVTT.Size = new System.Drawing.Size(394, 335);
            this.gr_CVTT.TabIndex = 12;
            this.gr_CVTT.TabStop = false;
            this.gr_CVTT.Text = "Công việc tiếp theo";
            // 
            // gr_xemTruocNgayCVTT
            // 
            this.gr_xemTruocNgayCVTT.Controls.Add(this.label3);
            this.gr_xemTruocNgayCVTT.Controls.Add(this.label2);
            this.gr_xemTruocNgayCVTT.Controls.Add(this.dtp_NgayKTCVTT);
            this.gr_xemTruocNgayCVTT.Controls.Add(this.dtp_NgayBDCVTT);
            this.gr_xemTruocNgayCVTT.Enabled = false;
            this.gr_xemTruocNgayCVTT.Location = new System.Drawing.Point(6, 251);
            this.gr_xemTruocNgayCVTT.Name = "gr_xemTruocNgayCVTT";
            this.gr_xemTruocNgayCVTT.Size = new System.Drawing.Size(379, 59);
            this.gr_xemTruocNgayCVTT.TabIndex = 27;
            this.gr_xemTruocNgayCVTT.TabStop = false;
            this.gr_xemTruocNgayCVTT.Text = "Ngày thực hiện ước tính";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(204, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "Đến";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(21, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Từ";
            // 
            // dtp_NgayKTCVTT
            // 
            this.dtp_NgayKTCVTT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_NgayKTCVTT.Location = new System.Drawing.Point(239, 19);
            this.dtp_NgayKTCVTT.Name = "dtp_NgayKTCVTT";
            this.dtp_NgayKTCVTT.Size = new System.Drawing.Size(103, 20);
            this.dtp_NgayKTCVTT.TabIndex = 11;
            // 
            // dtp_NgayBDCVTT
            // 
            this.dtp_NgayBDCVTT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_NgayBDCVTT.Location = new System.Drawing.Point(60, 19);
            this.dtp_NgayBDCVTT.Name = "dtp_NgayBDCVTT";
            this.dtp_NgayBDCVTT.Size = new System.Drawing.Size(110, 20);
            this.dtp_NgayBDCVTT.TabIndex = 10;
            // 
            // nud_SoNgayThucHienCVTT
            // 
            this.nud_SoNgayThucHienCVTT.Location = new System.Drawing.Point(229, 202);
            this.nud_SoNgayThucHienCVTT.Margin = new System.Windows.Forms.Padding(2);
            this.nud_SoNgayThucHienCVTT.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nud_SoNgayThucHienCVTT.Name = "nud_SoNgayThucHienCVTT";
            this.nud_SoNgayThucHienCVTT.Size = new System.Drawing.Size(90, 20);
            this.nud_SoNgayThucHienCVTT.TabIndex = 25;
            this.nud_SoNgayThucHienCVTT.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nud_SoNgayThucHienCVTT.ValueChanged += new System.EventHandler(this.nud_SoNgayThucHienCVTT_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(226, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Số ngày thực hiện";
            // 
            // nud_soNgaySoVoiCongTacTruoc
            // 
            this.nud_soNgaySoVoiCongTacTruoc.Location = new System.Drawing.Point(126, 202);
            this.nud_soNgaySoVoiCongTacTruoc.Margin = new System.Windows.Forms.Padding(2);
            this.nud_soNgaySoVoiCongTacTruoc.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nud_soNgaySoVoiCongTacTruoc.Name = "nud_soNgaySoVoiCongTacTruoc";
            this.nud_soNgaySoVoiCongTacTruoc.Size = new System.Drawing.Size(62, 20);
            this.nud_soNgaySoVoiCongTacTruoc.TabIndex = 17;
            this.nud_soNgaySoVoiCongTacTruoc.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nud_soNgaySoVoiCongTacTruoc.ValueChanged += new System.EventHandler(this.nud_soNgaySoVoiCongTacTruoc_ValueChanged);
            // 
            // cbb_CVChaTiepTheo
            // 
            this.cbb_CVChaTiepTheo.DisplayMember = "Value";
            this.cbb_CVChaTiepTheo.FormattingEnabled = true;
            this.cbb_CVChaTiepTheo.Location = new System.Drawing.Point(15, 50);
            this.cbb_CVChaTiepTheo.Name = "cbb_CVChaTiepTheo";
            this.cbb_CVChaTiepTheo.Size = new System.Drawing.Size(269, 21);
            this.cbb_CVChaTiepTheo.TabIndex = 24;
            this.cbb_CVChaTiepTheo.ValueMember = "Key";
            this.cbb_CVChaTiepTheo.SelectedIndexChanged += new System.EventHandler(this.cbb_CVChaTiepTheo_SelectedIndexChanged);
            // 
            // cbb_GV_NTT_TrangThai
            // 
            this.cbb_GV_NTT_TrangThai.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_GV_NTT_TrangThai.ForeColor = System.Drawing.Color.Silver;
            this.cbb_GV_NTT_TrangThai.FormattingEnabled = true;
            this.cbb_GV_NTT_TrangThai.Items.AddRange(new object[] {
            "Đang thực hiện",
            "Đang xét duyệt",
            "Đề nghị kiểm tra",
            "Hoàn thành"});
            this.cbb_GV_NTT_TrangThai.Location = new System.Drawing.Point(13, 147);
            this.cbb_GV_NTT_TrangThai.Name = "cbb_GV_NTT_TrangThai";
            this.cbb_GV_NTT_TrangThai.Size = new System.Drawing.Size(132, 23);
            this.cbb_GV_NTT_TrangThai.TabIndex = 23;
            this.cbb_GV_NTT_TrangThai.Text = "Đang thực hiện";
            // 
            // label383
            // 
            this.label383.AutoSize = true;
            this.label383.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label383.ForeColor = System.Drawing.Color.Green;
            this.label383.Location = new System.Drawing.Point(12, 129);
            this.label383.Name = "label383";
            this.label383.Size = new System.Drawing.Size(62, 15);
            this.label383.TabIndex = 22;
            this.label383.Text = "Trạng thái";
            // 
            // cbb_GV_NTT_BatDauSoVoiCongTacTruoc
            // 
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.ForeColor = System.Drawing.Color.Silver;
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.FormattingEnabled = true;
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.Items.AddRange(new object[] {
            "Bắt đầu",
            "Kết thúc",
            "Trước so với kết thúc"});
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.Location = new System.Drawing.Point(15, 202);
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.Name = "cbb_GV_NTT_BatDauSoVoiCongTacTruoc";
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.Size = new System.Drawing.Size(90, 23);
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.TabIndex = 20;
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.Text = "Kết thúc";
            this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc.SelectedIndexChanged += new System.EventHandler(this.cbb_GV_NTT_BatDauSoVoiCongTacTruoc_SelectedIndexChanged);
            // 
            // label379
            // 
            this.label379.AutoSize = true;
            this.label379.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label379.ForeColor = System.Drawing.Color.Green;
            this.label379.Location = new System.Drawing.Point(12, 185);
            this.label379.Name = "label379";
            this.label379.Size = new System.Drawing.Size(164, 15);
            this.label379.TabIndex = 19;
            this.label379.Text = "Bắt đầu so với công tác trước";
            // 
            // cbb_CVConTiepTheo
            // 
            this.cbb_CVConTiepTheo.DisplayMember = "Value";
            this.cbb_CVConTiepTheo.FormattingEnabled = true;
            this.cbb_CVConTiepTheo.Location = new System.Drawing.Point(15, 92);
            this.cbb_CVConTiepTheo.Name = "cbb_CVConTiepTheo";
            this.cbb_CVConTiepTheo.Size = new System.Drawing.Size(269, 21);
            this.cbb_CVConTiepTheo.TabIndex = 18;
            this.cbb_CVConTiepTheo.ValueMember = "Key";
            this.cbb_CVConTiepTheo.SelectedIndexChanged += new System.EventHandler(this.cbb_CVConTiepTheo_SelectedIndexChanged);
            // 
            // label378
            // 
            this.label378.AutoSize = true;
            this.label378.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label378.ForeColor = System.Drawing.Color.Green;
            this.label378.Location = new System.Drawing.Point(12, 74);
            this.label378.Name = "label378";
            this.label378.Size = new System.Drawing.Size(85, 15);
            this.label378.TabIndex = 15;
            this.label378.Text = "Bước tiếp theo";
            // 
            // label377
            // 
            this.label377.AutoSize = true;
            this.label377.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label377.ForeColor = System.Drawing.Color.Green;
            this.label377.Location = new System.Drawing.Point(12, 35);
            this.label377.Name = "label377";
            this.label377.Size = new System.Drawing.Size(111, 15);
            this.label377.TabIndex = 14;
            this.label377.Text = "Công việc tiếp theo";
            // 
            // label373
            // 
            this.label373.AutoSize = true;
            this.label373.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label373.Location = new System.Drawing.Point(130, 13);
            this.label373.Name = "label373";
            this.label373.Size = new System.Drawing.Size(51, 15);
            this.label373.TabIndex = 10;
            this.label373.Text = "Số ngày";
            // 
            // dtp_GV_TGKetThuc
            // 
            this.dtp_GV_TGKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_GV_TGKetThuc.Location = new System.Drawing.Point(200, 36);
            this.dtp_GV_TGKetThuc.Name = "dtp_GV_TGKetThuc";
            this.dtp_GV_TGKetThuc.Size = new System.Drawing.Size(103, 20);
            this.dtp_GV_TGKetThuc.TabIndex = 9;
            this.dtp_GV_TGKetThuc.ValueChanged += new System.EventHandler(this.dtp_GV_TGKetThuc_ValueChanged);
            // 
            // label372
            // 
            this.label372.AutoSize = true;
            this.label372.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label372.Location = new System.Drawing.Point(201, 13);
            this.label372.Name = "label372";
            this.label372.Size = new System.Drawing.Size(104, 15);
            this.label372.TabIndex = 7;
            this.label372.Text = "Thời gian kết thúc";
            // 
            // label371
            // 
            this.label371.AutoSize = true;
            this.label371.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label371.Location = new System.Drawing.Point(6, 13);
            this.label371.Name = "label371";
            this.label371.Size = new System.Drawing.Size(101, 15);
            this.label371.TabIndex = 6;
            this.label371.Text = "Thời gian bắt đầu";
            // 
            // cbb_TenCongViecChaHienTai
            // 
            this.cbb_TenCongViecChaHienTai.DisplayMember = "Value";
            this.cbb_TenCongViecChaHienTai.FormattingEnabled = true;
            this.cbb_TenCongViecChaHienTai.Location = new System.Drawing.Point(22, 50);
            this.cbb_TenCongViecChaHienTai.Name = "cbb_TenCongViecChaHienTai";
            this.cbb_TenCongViecChaHienTai.Size = new System.Drawing.Size(269, 21);
            this.cbb_TenCongViecChaHienTai.TabIndex = 1;
            this.cbb_TenCongViecChaHienTai.ValueMember = "Key";
            this.cbb_TenCongViecChaHienTai.SelectedIndexChanged += new System.EventHandler(this.cbb_TenCongViecChaHienTai_SelectedIndexChanged);
            // 
            // label368
            // 
            this.label368.AutoSize = true;
            this.label368.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label368.Location = new System.Drawing.Point(22, 35);
            this.label368.Name = "label368";
            this.label368.Size = new System.Drawing.Size(99, 15);
            this.label368.TabIndex = 0;
            this.label368.Text = "Tên công tác cha";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gr_CVTT, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 341);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pn_thoiGianCVHT);
            this.groupBox1.Controls.Add(this.label368);
            this.groupBox1.Controls.Add(this.cbb_TenCongViecChaHienTai);
            this.groupBox1.Controls.Add(this.cbb_TenCongViecConHienTai);
            this.groupBox1.Controls.Add(this.label376);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 335);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Công việc hiện tại";
            // 
            // pn_thoiGianCVHT
            // 
            this.pn_thoiGianCVHT.Controls.Add(this.dtp_GV_TGBatDau);
            this.pn_thoiGianCVHT.Controls.Add(this.dtp_GV_TGKetThuc);
            this.pn_thoiGianCVHT.Controls.Add(this.nud_soNgayCVHT);
            this.pn_thoiGianCVHT.Controls.Add(this.label373);
            this.pn_thoiGianCVHT.Controls.Add(this.label372);
            this.pn_thoiGianCVHT.Controls.Add(this.label371);
            this.pn_thoiGianCVHT.Enabled = false;
            this.pn_thoiGianCVHT.Location = new System.Drawing.Point(22, 129);
            this.pn_thoiGianCVHT.Name = "pn_thoiGianCVHT";
            this.pn_thoiGianCVHT.Size = new System.Drawing.Size(347, 71);
            this.pn_thoiGianCVHT.TabIndex = 18;
            // 
            // nud_soNgayCVHT
            // 
            this.nud_soNgayCVHT.Location = new System.Drawing.Point(133, 36);
            this.nud_soNgayCVHT.Margin = new System.Windows.Forms.Padding(2);
            this.nud_soNgayCVHT.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nud_soNgayCVHT.Name = "nud_soNgayCVHT";
            this.nud_soNgayCVHT.Size = new System.Drawing.Size(62, 20);
            this.nud_soNgayCVHT.TabIndex = 17;
            this.nud_soNgayCVHT.ValueChanged += new System.EventHandler(this.nud_soNgayCVHT_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_HoanTat);
            this.panel1.Controls.Add(this.bt_Huy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 413);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 37);
            this.panel1.TabIndex = 20;
            // 
            // bt_HoanTat
            // 
            this.bt_HoanTat.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_HoanTat.Enabled = false;
            this.bt_HoanTat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_HoanTat.Location = new System.Drawing.Point(614, 0);
            this.bt_HoanTat.Name = "bt_HoanTat";
            this.bt_HoanTat.Size = new System.Drawing.Size(93, 37);
            this.bt_HoanTat.TabIndex = 0;
            this.bt_HoanTat.Text = "Hoàn thành";
            this.bt_HoanTat.UseVisualStyleBackColor = true;
            this.bt_HoanTat.Click += new System.EventHandler(this.bt_HoanTat_Click);
            // 
            // bt_Huy
            // 
            this.bt_Huy.BackColor = System.Drawing.Color.Red;
            this.bt_Huy.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_Huy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Huy.ForeColor = System.Drawing.Color.White;
            this.bt_Huy.Location = new System.Drawing.Point(707, 0);
            this.bt_Huy.Name = "bt_Huy";
            this.bt_Huy.Size = new System.Drawing.Size(93, 37);
            this.bt_Huy.TabIndex = 1;
            this.bt_Huy.Text = "Hủy";
            this.bt_Huy.UseVisualStyleBackColor = false;
            this.bt_Huy.Click += new System.EventHandler(this.bt_Huy_Click);
            // 
            // dtp_GV_TGBatDau
            // 
            this.dtp_GV_TGBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_GV_TGBatDau.Location = new System.Drawing.Point(9, 36);
            this.dtp_GV_TGBatDau.Name = "dtp_GV_TGBatDau";
            this.dtp_GV_TGBatDau.Size = new System.Drawing.Size(103, 20);
            this.dtp_GV_TGBatDau.TabIndex = 9;
            this.dtp_GV_TGBatDau.ValueChanged += new System.EventHandler(this.dtp_GV_TGKetThuc_ValueChanged);
            // 
            // Form_GiaoViec_QuyTrinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form_GiaoViec_QuyTrinh";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_GiaoViec_QuyTrinh_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gr_CVTT.ResumeLayout(false);
            this.gr_CVTT.PerformLayout();
            this.gr_xemTruocNgayCVTT.ResumeLayout(false);
            this.gr_xemTruocNgayCVTT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_SoNgayThucHienCVTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_soNgaySoVoiCongTacTruoc)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pn_thoiGianCVHT.ResumeLayout(false);
            this.pn_thoiGianCVHT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_soNgayCVHT)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbb_TenCongViecConHienTai;
        private System.Windows.Forms.Label label376;
        private System.Windows.Forms.GroupBox gr_CVTT;
        private System.Windows.Forms.ComboBox cbb_GV_NTT_TrangThai;
        private System.Windows.Forms.Label label383;
        private System.Windows.Forms.ComboBox cbb_GV_NTT_BatDauSoVoiCongTacTruoc;
        private System.Windows.Forms.Label label379;
        private System.Windows.Forms.ComboBox cbb_CVConTiepTheo;
        private System.Windows.Forms.Label label378;
        private System.Windows.Forms.Label label377;
        private System.Windows.Forms.Label label373;
        private System.Windows.Forms.DateTimePicker dtp_GV_TGKetThuc;
        private System.Windows.Forms.Label label372;
        private System.Windows.Forms.Label label371;
        private System.Windows.Forms.ComboBox cbb_TenCongViecChaHienTai;
        private System.Windows.Forms.Label label368;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbb_CVChaTiepTheo;
        private System.Windows.Forms.Button bt_HoanTat;
        private System.Windows.Forms.NumericUpDown nud_soNgayCVHT;
        private System.Windows.Forms.NumericUpDown nud_soNgaySoVoiCongTacTruoc;
        private System.Windows.Forms.Panel pn_thoiGianCVHT;
        private System.Windows.Forms.GroupBox gr_xemTruocNgayCVTT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_NgayKTCVTT;
        private System.Windows.Forms.DateTimePicker dtp_NgayBDCVTT;
        private System.Windows.Forms.NumericUpDown nud_SoNgayThucHienCVTT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_Huy;
        private System.Windows.Forms.DateTimePicker dtp_GV_TGBatDau;
    }
}