
namespace PhanMemQuanLyThiCong.Controls
{
    partial class Ctrl_ChartThanhTienHopDongDuAn
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.GanttDiagram ganttDiagram1 = new DevExpress.XtraCharts.GanttDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.OverlappedGanttSeriesView overlappedGanttSeriesView1 = new DevExpress.XtraCharts.OverlappedGanttSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.OverlappedGanttSeriesView overlappedGanttSeriesView2 = new DevExpress.XtraCharts.OverlappedGanttSeriesView();
            this.gc_BieuDoTaiChinh = new DevExpress.XtraGrid.GridControl();
            this.gv_BieuDoTaiChinh = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.DVTH = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ColChart = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.cc_HopDong = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.gc_BieuDoTaiChinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_BieuDoTaiChinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cc_HopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ganttDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView2)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_BieuDoTaiChinh
            // 
            this.gc_BieuDoTaiChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_BieuDoTaiChinh.Location = new System.Drawing.Point(0, 0);
            this.gc_BieuDoTaiChinh.MainView = this.gv_BieuDoTaiChinh;
            this.gc_BieuDoTaiChinh.Name = "gc_BieuDoTaiChinh";
            this.gc_BieuDoTaiChinh.Size = new System.Drawing.Size(1152, 607);
            this.gc_BieuDoTaiChinh.TabIndex = 1;
            this.gc_BieuDoTaiChinh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_BieuDoTaiChinh});
            // 
            // gv_BieuDoTaiChinh
            // 
            this.gv_BieuDoTaiChinh.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.gv_BieuDoTaiChinh.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.DVTH,
            this.ColChart});
            this.gv_BieuDoTaiChinh.GridControl = this.gc_BieuDoTaiChinh;
            this.gv_BieuDoTaiChinh.GroupCount = 1;
            this.gv_BieuDoTaiChinh.Name = "gv_BieuDoTaiChinh";
            this.gv_BieuDoTaiChinh.OptionsCustomization.AllowRowSizing = true;
            this.gv_BieuDoTaiChinh.OptionsView.ColumnAutoWidth = true;
            this.gv_BieuDoTaiChinh.OptionsView.ShowColumnHeaders = false;
            this.gv_BieuDoTaiChinh.OptionsView.ShowGroupPanel = false;
            this.gv_BieuDoTaiChinh.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.DVTH, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gv_BieuDoTaiChinh.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gv_BieuDoTaiChinh_CustomUnboundColumnData);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.gridBand1.AppearanceHeader.Options.UseFont = true;
            this.gridBand1.Caption = "Biểu đồ thành tiền hợp đồng";
            this.gridBand1.Columns.Add(this.DVTH);
            this.gridBand1.Columns.Add(this.ColChart);
            this.gridBand1.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.changechartseriestype_16x163;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 2000;
            // 
            // DVTH
            // 
            this.DVTH.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DVTH.Caption = "Đơn vị thực hiện";
            this.DVTH.FieldName = "Ten";
            this.DVTH.Name = "DVTH";
            // 
            // ColChart
            // 
            this.ColChart.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.ColChart.AppearanceHeader.Options.UseFont = true;
            this.ColChart.Caption = "Biểu đồ tài chính";
            this.ColChart.FieldName = "BieuDo";
            this.ColChart.MinWidth = 10;
            this.ColChart.Name = "ColChart";
            this.ColChart.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColChart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColChart.OptionsColumn.ShowCaption = false;
            this.ColChart.UnboundDataType = typeof(object);
            this.ColChart.Visible = true;
            this.ColChart.Width = 2000;
            // 
            // cc_HopDong
            // 
            ganttDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            constantLine1.AxisValueSerializable = "04/24/2023 06:48:36.923";
            constantLine1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            constantLine1.Name = "Tiến độ hiện tại";
            constantLine1.ShowInLegend = false;
            constantLine1.Title.Alignment = DevExpress.XtraCharts.ConstantLineTitleAlignment.Far;
            constantLine1.Title.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            constantLine1.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            ganttDiagram1.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine1});
            ganttDiagram1.AxisY.DateTimeScaleOptions.AutoGrid = false;
            ganttDiagram1.AxisY.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Day;
            ganttDiagram1.AxisY.DateTimeScaleOptions.GridSpacing = 7D;
            ganttDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            ganttDiagram1.EnableAxisXScrolling = true;
            ganttDiagram1.EnableAxisYScrolling = true;
            ganttDiagram1.EnableAxisYZooming = true;
            this.cc_HopDong.Diagram = ganttDiagram1;
            this.cc_HopDong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cc_HopDong.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.cc_HopDong.Location = new System.Drawing.Point(0, 607);
            this.cc_HopDong.Name = "cc_HopDong";
            series1.ArgumentDataMember = "TenGhepSoHopDong";
            series1.Name = "Kế hoạch";
            series1.ValueDataMembersSerializable = "NgayBatDau;NgayKetThuc";
            series1.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series1.View = overlappedGanttSeriesView1;
            series2.ArgumentDataMember = "TenGhepSoHopDong";
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series2.Name = "Hoàn thành";
            series2.ValueDataMembersSerializable = "NgayBatDau;NgayHoanThanh";
            series2.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            overlappedGanttSeriesView2.BarWidth = 0.3D;
            overlappedGanttSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series2.View = overlappedGanttSeriesView2;
            this.cc_HopDong.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            this.cc_HopDong.SeriesTemplate.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            this.cc_HopDong.Size = new System.Drawing.Size(1152, 26);
            this.cc_HopDong.TabIndex = 2;
            this.cc_HopDong.Visible = false;
            this.cc_HopDong.ConstantLineMoved += new DevExpress.XtraCharts.ConstantLineMovedEventHandler(this.cc_HopDong_ConstantLineMoved);
            // 
            // Ctrl_ChartThanhTienHopDongDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gc_BieuDoTaiChinh);
            this.Controls.Add(this.cc_HopDong);
            this.Name = "Ctrl_ChartThanhTienHopDongDuAn";
            this.Size = new System.Drawing.Size(1152, 633);
            ((System.ComponentModel.ISupportInitialize)(this.gc_BieuDoTaiChinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_BieuDoTaiChinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(ganttDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cc_HopDong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc_BieuDoTaiChinh;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gv_BieuDoTaiChinh;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn DVTH;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn ColChart;
        private DevExpress.XtraCharts.ChartControl cc_HopDong;
    }
}
