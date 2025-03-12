using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace PhanMemQuanLyThiCong.Report
{
    public partial class BaoCaoVatLieu : DevExpress.XtraReports.UI.XtraReport
    {
        public BaoCaoVatLieu()
        {
            InitializeComponent();
        }
        public void Fcn_Setting(DefaultBoolean Legend,DateTime begin,DateTime End)
        {
            XYDiagram diagram = (XYDiagram)cc_DuAn.Diagram;
            cc_DuAn.Legend.Visibility = Legend;
            lb_BatDau.Text = $"Ngày bắt đầu: {begin.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}";
            lb_KetThuc.Text = $"Ngày kết thúc: {End.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}";
        }
    }
}
