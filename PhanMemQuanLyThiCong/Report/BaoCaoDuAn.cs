using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace PhanMemQuanLyThiCong.Report
{
    public partial class BaoCaoDuAn : DevExpress.XtraReports.UI.XtraReport
    {
        public BaoCaoDuAn()
        {
            InitializeComponent();
        }
        public void Fcn_Update(string tennhathau)
        {
            this.BeginUpdate();
            lb_TenNhaThau.Text = $"BÁO CÁO {tennhathau}";
            //string v = "lb_TenNhaThau.Text=" +"\""+"BÁO CÁO"+ tennhathau+ "\"" + ";}";
            //this.ScriptsSource += "private void lb_TenNhaThau_BeforePrint(object sender, " +
            //                        "System.ComponentModel.CancelEventArgs e) { " +
            //                        v;
            //this.lb_TenNhaThau.Scripts.OnBeforePrint = "lb_TenNhaThau_BeforePrint";
            this.EndUpdate();
        }
        public void Fcn_UpdateSub(XtraReport SubRePort)
        {
            Sub_Chart.ReportSource = SubRePort;
        }
    }
}
