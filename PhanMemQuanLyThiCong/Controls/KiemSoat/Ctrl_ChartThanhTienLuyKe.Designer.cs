
namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    partial class Ctrl_ChartThanhTienLuyKe
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY2 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView3 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView4 = new DevExpress.XtraCharts.LineSeriesView();
            this.gc_BieuDoTaiChinh = new DevExpress.XtraGrid.GridControl();
            this.gv_BieuDoTaiChinh = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.DVTH = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ColChart = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.cc_VatTu = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.gc_BieuDoTaiChinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_BieuDoTaiChinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cc_VatTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView4)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_BieuDoTaiChinh
            // 
            this.gc_BieuDoTaiChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_BieuDoTaiChinh.Location = new System.Drawing.Point(0, 0);
            this.gc_BieuDoTaiChinh.MainView = this.gv_BieuDoTaiChinh;
            this.gc_BieuDoTaiChinh.Name = "gc_BieuDoTaiChinh";
            this.gc_BieuDoTaiChinh.Size = new System.Drawing.Size(967, 541);
            this.gc_BieuDoTaiChinh.TabIndex = 0;
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
            this.gv_BieuDoTaiChinh.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gv_BieuDoTaiChinh_PopupMenuShowing);
            this.gv_BieuDoTaiChinh.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(this.gv_BieuDoTaiChinh_CalcRowHeight);
            this.gv_BieuDoTaiChinh.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gv_BieuDoTaiChinh_CustomUnboundColumnData);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.gridBand1.AppearanceHeader.Options.UseFont = true;
            this.gridBand1.Caption = "Biểu đồ tài chính-Thành tiền";
            this.gridBand1.Columns.Add(this.DVTH);
            this.gridBand1.Columns.Add(this.ColChart);
            this.gridBand1.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.changechartseriestype_16x164;
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
            this.ColChart.Caption = "Biểu đồ tài chính-Thành tiền";
            this.ColChart.FieldName = "BieuDo";
            this.ColChart.MinWidth = 10;
            this.ColChart.Name = "ColChart";
            this.ColChart.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColChart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColChart.UnboundDataType = typeof(object);
            this.ColChart.Visible = true;
            this.ColChart.Width = 2000;
            // 
            // cc_VatTu
            // 
            this.cc_VatTu.CrosshairOptions.LinesMode = DevExpress.XtraCharts.CrosshairLinesMode.Free;
            this.cc_VatTu.CrosshairOptions.ShowOnlyInFocusedPane = false;
            xyDiagram1.AxisX.DateTimeScaleOptions.AutoGrid = false;
            xyDiagram1.AxisX.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Day;
            xyDiagram1.AxisX.Title.Text = "Ngày thực hiện";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Title.Text = "";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.DefaultPane.Title.Text = "Khối lượng";
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisYScrolling = true;
            xyDiagram1.EnableAxisYZooming = true;
            xyDiagram1.RuntimePaneResize = true;
            secondaryAxisY1.Alignment = DevExpress.XtraCharts.AxisAlignment.Near;
            secondaryAxisY1.AxisID = 0;
            secondaryAxisY1.Label.TextPattern = "{V:N0}";
            secondaryAxisY1.Name = "Thành tiền";
            secondaryAxisY1.Visibility = DevExpress.Utils.DefaultBoolean.True;
            secondaryAxisY1.VisibleInPanesSerializable = "-1";
            secondaryAxisY2.AxisID = 1;
            secondaryAxisY2.Label.TextPattern = "{V:N0}";
            secondaryAxisY2.Name = "Lũy kế thành tiền";
            secondaryAxisY2.VisibleInPanesSerializable = "-1";
            xyDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1,
            secondaryAxisY2});
            this.cc_VatTu.Diagram = xyDiagram1;
            this.cc_VatTu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cc_VatTu.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.cc_VatTu.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
            this.cc_VatTu.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.cc_VatTu.Location = new System.Drawing.Point(0, 541);
            this.cc_VatTu.Name = "cc_VatTu";
            series1.ArgumentDataMember = "Date";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series1.CrosshairLabelPattern = "{S}: {V:N0}";
            sideBySideBarSeriesLabel1.TextPattern = "{V:N0}";
            series1.Label = sideBySideBarSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.LegendTextPattern = "{V:N0}";
            series1.Name = "Thành tiền kế hoạch";
            series1.ValueDataMembersSerializable = "ThanhTienKeHoach";
            sideBySideBarSeriesView1.AxisYName = "Thành tiền";
            sideBySideBarSeriesView1.BarWidth = 1D;
            series1.View = sideBySideBarSeriesView1;
            series2.ArgumentDataMember = "Date";
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series2.CrosshairLabelPattern = "{S}: {V:N0}";
            sideBySideBarSeriesLabel2.TextPattern = "{V:N0}";
            series2.Label = sideBySideBarSeriesLabel2;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series2.LegendTextPattern = "{V:N0}";
            series2.Name = "Thành tiền thi công";
            series2.ValueDataMembersSerializable = "ThanhTienThiCong";
            sideBySideBarSeriesView2.AxisYName = "Thành tiền";
            sideBySideBarSeriesView2.BarWidth = 1D;
            series2.View = sideBySideBarSeriesView2;
            series3.ArgumentDataMember = "Date";
            series3.CrosshairLabelPattern = "{S}: {V:N0}";
            pointSeriesLabel1.TextPattern = "{V:N0}";
            series3.Label = pointSeriesLabel1;
            series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series3.LegendTextPattern = "{V:N0}";
            series3.Name = "Lũy kế thành tiền kế hoạch";
            series3.ValueDataMembersSerializable = "LuyKeThanhTienKeHoach";
            lineSeriesView1.AxisYName = "Lũy kế thành tiền";
            series3.View = lineSeriesView1;
            series4.ArgumentDataMember = "Date";
            series4.CrosshairLabelPattern = "{S}: {V:N0}";
            pointSeriesLabel2.TextPattern = "{V:N0}";
            series4.Label = pointSeriesLabel2;
            series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series4.LegendTextPattern = "{V:N0}";
            series4.Name = "Lũy kế thành tiền thi công";
            series4.ValueDataMembersSerializable = "LuyKeThanhTienThiCong";
            lineSeriesView2.AxisYName = "Lũy kế thành tiền";
            series4.View = lineSeriesView2;
            series5.ArgumentDataMember = "Date";
            series5.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series5.Name = "Xu hướng Kế hoạch";
            series5.ValueDataMembersSerializable = "XuHuongKeHoach";
            lineSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            lineSeriesView3.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series5.View = lineSeriesView3;
            series5.Visible = false;
            series6.ArgumentDataMember = "Date";
            series6.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series6.Name = "Xu hướng Thi công";
            series6.ValueDataMembersSerializable = "XuHuongThiCong";
            lineSeriesView4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lineSeriesView4.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series6.View = lineSeriesView4;
            series6.Visible = false;
            this.cc_VatTu.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3,
        series4,
        series5,
        series6};
            this.cc_VatTu.SeriesTemplate.LegendTextPattern = "{V:N0}";
            this.cc_VatTu.Size = new System.Drawing.Size(967, 33);
            this.cc_VatTu.TabIndex = 8;
            this.cc_VatTu.Visible = false;
            // 
            // Ctrl_ChartThanhTienLuyKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gc_BieuDoTaiChinh);
            this.Controls.Add(this.cc_VatTu);
            this.Name = "Ctrl_ChartThanhTienLuyKe";
            this.Size = new System.Drawing.Size(967, 574);
            ((System.ComponentModel.ISupportInitialize)(this.gc_BieuDoTaiChinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_BieuDoTaiChinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cc_VatTu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gv_BieuDoTaiChinh;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn DVTH;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn ColChart;
        private DevExpress.XtraGrid.GridControl gc_BieuDoTaiChinh;
        private DevExpress.XtraCharts.ChartControl cc_VatTu;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}
