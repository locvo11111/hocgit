
namespace PhanMemQuanLyThiCong
{
    partial class FormBangCongViecCacNhanVien
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
            DevExpress.XtraCharts.GanttDiagram ganttDiagram1 = new DevExpress.XtraCharts.GanttDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.OverlappedGanttSeriesView overlappedGanttSeriesView1 = new DevExpress.XtraCharts.OverlappedGanttSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBangCongViecCacNhanVien));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BNC_DS_STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BNC_DS_TenNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.spsheet_BangChiTietGiaoViecNhanVien = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ganttDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 516);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 30);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(188, 516);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(188, 516);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DANH SÁCH NHÂN VIÊN";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BNC_DS_STT,
            this.BNC_DS_TenNhanVien});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(2, 15);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(184, 499);
            this.dataGridView1.TabIndex = 0;
            // 
            // BNC_DS_STT
            // 
            this.BNC_DS_STT.HeaderText = "STT";
            this.BNC_DS_STT.MinimumWidth = 6;
            this.BNC_DS_STT.Name = "BNC_DS_STT";
            this.BNC_DS_STT.Width = 50;
            // 
            // BNC_DS_TenNhanVien
            // 
            this.BNC_DS_TenNhanVien.HeaderText = "Tên nhân viên";
            this.BNC_DS_TenNhanVien.MinimumWidth = 6;
            this.BNC_DS_TenNhanVien.Name = "BNC_DS_TenNhanVien";
            this.BNC_DS_TenNhanVien.Width = 200;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(664, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(364, 516);
            this.panel4.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chartControl1);
            this.groupBox3.Controls.Add(this.panel5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(364, 516);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "BẢNG TIẾN ĐỘ 1 NHÂN VIÊN";
            // 
            // chartControl1
            // 
            this.chartControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(213)))), ((int)(((byte)(181)))));
            ganttDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            ganttDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = ganttDiagram1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(2, 50);
            this.chartControl1.Margin = new System.Windows.Forms.Padding(2);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "Series 1";
            series1.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series1.View = overlappedGanttSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.Size = new System.Drawing.Size(360, 464);
            this.chartControl1.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(2, 18);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(360, 32);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhân viên:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(188, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(476, 516);
            this.panel3.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.spsheet_BangChiTietGiaoViecNhanVien);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(476, 516);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BẢNG CHI TIẾT GIAO VIỆC NHÂN VIÊN (12.gBangTienDoGiaoViecChiTietNhanVien)";
            // 
            // spsheet_BangChiTietGiaoViecNhanVien
            // 
            this.spsheet_BangChiTietGiaoViecNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spsheet_BangChiTietGiaoViecNhanVien.Location = new System.Drawing.Point(2, 18);
            this.spsheet_BangChiTietGiaoViecNhanVien.Margin = new System.Windows.Forms.Padding(2);
            this.spsheet_BangChiTietGiaoViecNhanVien.Name = "spsheet_BangChiTietGiaoViecNhanVien";
            this.spsheet_BangChiTietGiaoViecNhanVien.Options.Import.Csv.Encoding = ((System.Text.Encoding)(resources.GetObject("spsheet_BangChiTietGiaoViecNhanVien.Options.Import.Csv.Encoding")));
            this.spsheet_BangChiTietGiaoViecNhanVien.Options.Import.Txt.Encoding = ((System.Text.Encoding)(resources.GetObject("spsheet_BangChiTietGiaoViecNhanVien.Options.Import.Txt.Encoding")));
            this.spsheet_BangChiTietGiaoViecNhanVien.Options.TabSelector.Visibility = DevExpress.XtraSpreadsheet.SpreadsheetElementVisibility.Hidden;
            this.spsheet_BangChiTietGiaoViecNhanVien.Size = new System.Drawing.Size(472, 496);
            this.spsheet_BangChiTietGiaoViecNhanVien.TabIndex = 0;
            this.spsheet_BangChiTietGiaoViecNhanVien.Text = "spreadsheetControl1";
            // 
            // FormBangCongViecCacNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 546);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBangCongViecCacNhanVien";
            this.Text = "Bảng công việc các nhân viên";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormBangCongViecCacNhanVien_FormClosed);
            this.Load += new System.EventHandler(this.FormBangCongViecCacNhanVien_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(ganttDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spsheet_BangChiTietGiaoViecNhanVien;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BNC_DS_STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn BNC_DS_TenNhanVien;
    }
}