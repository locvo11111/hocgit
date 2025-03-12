using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace PhanMemQuanLyThiCong.Report
{
    public partial class ChartTaiChinhDuAn : DevExpress.XtraReports.UI.XtraReport
    {
        public ChartTaiChinhDuAn()
        {
            InitializeComponent();
        }
        public void Fcn_Setting(int Ngay,/*DateTime Min,DateTime Max,*/ DefaultBoolean Legend)
        {
            XYDiagram diagram = (XYDiagram)cc_VatTu.Diagram;
            if (Ngay >= 1)
            {
                diagram.AxisX.WholeRange.SideMarginsValue = 1;
                //diagram.AxisX.WholeRange.SetMinMaxValues(Min, Max);
                diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
                diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
                diagram.AxisX.DateTimeScaleOptions.GridSpacing = Ngay;
            }
            else if (Ngay == 0)
            {
                diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                diagram.AxisX.DateTimeScaleOptions.GridSpacing = 1;
                diagram.AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Manual;
                diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month;
            }
            cc_VatTu.Legend.Visibility = Legend;
        }
    }
}
