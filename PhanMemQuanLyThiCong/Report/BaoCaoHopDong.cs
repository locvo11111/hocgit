using DevExpress.Utils;
using DevExpress.XtraReports.UI;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.HopDong;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

namespace PhanMemQuanLyThiCong.Report
{
    public partial class BaoCaoHopDong : DevExpress.XtraReports.UI.XtraReport
    {
        public BaoCaoHopDong()
        {
            InitializeComponent();
        }
        public void Fcn_LoadChart(string TenDuAn, DefaultBoolean Legend,long HopDongThu,long HopDongChi,long Thu,long Chi,long TamUngThu,long TamungChi)
        {
            //lb_TenDuAn.Text = $"BÁO CÁO DỰ ÁN {TenDuAn.ToUpper()}";
            lb_TenDuAnMau.Text = $"{TenDuAn.ToUpper()}";
            cc_DuAn.Legend.Visibility = Legend;
            Tc_HopDongThu.Text = $"Tổng giá trị hợp đồng thu: {HopDongThu}";
            Tc_HopDongChi.Text = $"Tổng giá trị hợp đồng chi: {HopDongChi}";
            Tc_Thu.Text = $"Tổng thu thực tế: {Thu}";
            Tc_Chi.Text = $"Tổng chi thực tế: {Chi}";
            Tc_TamUngThu.Text = $"Tổng giá tạm ứng thu: {TamUngThu}";
            Tc_TamUngChi.Text = $"Tổng giá trị tạm ứng chi: {TamungChi}";
            //Fcn_UpdateDuAn(TypeThuChi.HopDongThu, TypeThuChi.TamUngThu);
            //Fcn_UpdateDuAn(TypeThuChi.HopDongChi, TypeThuChi.TamUngChi);
            //Fcn_UpdateThuChi();
        }


    }
}
