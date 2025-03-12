using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace PhanMemQuanLyThiCong.Report
{
    public partial class Chart : DevExpress.XtraReports.UI.XtraReport
    {
        public Chart()
        {
            InitializeComponent();
        }
        public void Fcn_Update(string CTH,string DTH,string XD,string KT,string HT,string TD,string ThoiLuong,string NgayTH,string KLDat,string SoCongViec)
        {
            this.BeginUpdate();
            lb_ChuaThucHien.Text = CTH;
            lb_Dangthuchien.Text = DTH;
            lb_DungHD.Text = TD;
            lb_XetDuyet.Text = XD;
            lb_kiemtra.Text = KT;
            lb_HoanThanh.Text = HT;
            lb_KhoiLuongDatDuoc.Text = KLDat;
            lb_ThoiLuong.Text = ThoiLuong;
            lb_NgayThucHien.Text = NgayTH;
            lb_SoCongViec.Text = SoCongViec;
            //string v = "lb_TenNhaThau.Text=" + "\"" + "BÁO CÁO" + tennhathau + "\"" + ";}";
            //this.ScriptsSource += "private void lb_TenNhaThau_BeforePrint(object sender, " +
            //                        "System.ComponentModel.CancelEventArgs e) { " +
            //                        v;
            //this.lb_TenNhaThau.Scripts.OnBeforePrint = "lb_TenNhaThau_BeforePrint";
            this.EndUpdate();
        }
    }
}
