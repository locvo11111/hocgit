using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Ctrl_ChartGanttAndKhoiLuongThanhTien : UserControl
    {
        public Ctrl_ChartGanttAndKhoiLuongThanhTien()
        {
            InitializeComponent();
        }

        public void pushDataKhoiLuongThanhTien(DataTable dt)
        {
            //ctrl_ChartKhoiLuongThanhTien1.PushData(dt);
        }

        public void pushDataGantt(DataTable dt)
        {
            //DataTable dtUniqueVL = dt.DefaultView.ToTable(true, "MaVatLieu", "VatTu", "DonVi");
            //Dictionary<string, string> lsCbbSource = new Dictionary<string, string>();


            //List<Chart_Gantt> chartItemsKeHoach = new List<Chart_Gantt>();
            //List<Chart_Gantt> chartItemsThiCong = new List<Chart_Gantt>();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    //var dtDistinc = dt.AsEnumerable().Where(x => x["MaVatLieu"].ToString() == dr["MaVatLieu"].ToString() &&
            //    //x["VatTu"].ToString() == dr["VatTu"].ToString()
            //    //&& x["DonVi"].ToString() == dr["DonVi"].ToString()).CopyToDataTable();

            //    //string lsCodeVL = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            //    //lsCbbSource.Add(lsCodeVL, dr["VatTu"].ToString());
            //    dt.DefaultView.Sort = $"[NgayBatDau] ASC";
            //    DateTime dateBD = DateTime.Parse(dt.DefaultView.ToTable().Rows[0]["NgayBatDau"].ToString());

            //    dt.DefaultView.Sort = $"[NgayKetThuc] DESC";
            //    DateTime dateKT = DateTime.Parse(dt.DefaultView.ToTable().Rows[0]["NgayKetThuc"].ToString());


            //    DateTime crDateBD = dateBD;
            //    chartItemsKeHoach.AddRange(MyFunction.fcn_TDKH_getListSourceSeries(dateBD, dateKT, lsCodeVL, dr["VatTu"].ToString(), TDKH.COL_KhoiLuong));

            //    var rowsDistinc1 = dt.AsEnumerable().Where(x => ValidateHelper.IsDateTime(x["NgayBatDauThiCong"]) && ValidateHelper.IsDateTime(x["NgayKetThucThiCong"]));

            //    if (rowsDistinc1.Any())
            //    {
            //        var dtDistinc1 = rowsDistinc1.CopyToDataTable();
            //        dtDistinc1.DefaultView.Sort = $"[NgayBatDauThiCong] ASC";

            //        DateTime dateBDTC = DateTime.Parse(dtDistinc1.DefaultView.ToTable().Rows[0]["NgayBatDauThiCong"].ToString());

            //        dt.DefaultView.Sort = $"[NgayKetThucThiCong] DESC";
            //        DateTime dateKTTC = DateTime.Parse(dtDistinc1.DefaultView.ToTable().Rows[0]["NgayKetThucThiCong"].ToString());
            //        chartItemsThiCong.AddRange(MyFunction.fcn_TDKH_getListSourceSeries(dateBDTC, dateKTTC, lsCodeVL, dr["VatTu"].ToString(), TDKH.COL_KhoiLuongThiCong));
            //    }
            //}

            //cc_Gantt.Series["Kế hoạch"].DataSource = chartItemsKeHoach;
            //cc_Gantt.Series["Thi công"].DataSource = chartItemsThiCong;
            //cbb_VatTu.DataSource = lsCbbSource.ToList();
        }

        private void cbb_VatTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbb_VatTu.SelectedIndex < 0)
            //    return;

            //string lscodeVT = cbb_VatTu.SelectedValue.ToString();

            //string dbString = $"SELECT * FROM {TDKH.TBL_KHVT_KhoiLuongHangNgay} WHERE \"CodeVatTu\" IN ({lscodeVT})";
            //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //ctrl_ChartKhoiLuongThanhTien1.PushData(dt);
        }
    }
}
