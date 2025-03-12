using DevExpress.Mvvm.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSpreadsheet.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_LayThongTinTuTDKH : DevExpress.XtraEditors.XtraForm
    {
        //public class GiaoViecWithTDKH
        //{
        //    public bool? Chon { get; set; } = false;
        //    public string Code { get; set; }
        //    public string DonVi { get; set; }
        //    public DateTime NgayBatDau { get; set; }
        //    public DateTime NgayKetThuc { get; set; }
        //    public double NgayBatDau { get; set; }
        //}

        private static Dictionary<string, KeyValuePair<string, string>> dic = new Dictionary<string, KeyValuePair<string, string>>()
        {
            {"Khối lượng kế hoạch", new KeyValuePair<string, string>("KhoiLuongKeHoach", "KLKHTDKH")},
            {"Đơn giá", new KeyValuePair<string, string>("DonGia", "DGTDKH")},
            {"Đơn giá thi công", new KeyValuePair<string, string>("DonGiaThiCong", "DGTCTDKH")},
            {"Ngày bắt đầu", new KeyValuePair<string, string>("NgayBatDau", "NBDTDKH")},
            {"Ngày kết thúc", new KeyValuePair<string, string>("NgayKetThuc", "NKTTDKH")},
        };
        public XtraForm_LayThongTinTuTDKH()
        {
            InitializeComponent();
            ccbe_DuLieuCanLay.Properties.DataSource = dic.ToList();
            DialogResult = DialogResult.Cancel;
             
        }

        private void XtraForm_LayThongTinTuTDKH_Load(object sender, EventArgs e)
        {
            var DVTH = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH;
            
            if (DVTH is null)
            {
                Close();
            }

            string dbString = $"SELECT cvc.*, cttk.NgayBatDau AS NBDTDKH, cttk.NgayKetThuc AS NKTTDKH,\r\n" +
                $"cttk.DonGia AS DGTDKH, cttk.DonGiaThiCong AS DGTCTDKH, cttk.KhoiLuongToanBo AS KLKHTDKH\r\n" +
                $"FROM {Server.Tbl_GiaoViec_CongViecCha} cvc\r\n" +
                $"JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk\r\n" +
                $"ON cvc.CodeCongTacTheoGiaiDoan = cttk.Code\r\n" +
                $"WHERE cvc.{DVTH.ColCodeFK} = '{DVTH.Code}'";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dt.Columns.Add("Chon", typeof(bool));
            //dt.Columns("Chon")

            tl_Data.DataSource = dt;
        }

        private void sB_ok_Click(object sender, EventArgs e)
        {
            var dt = tl_Data.DataSource as DataTable;
            
            var nodes = tl_Data.GetAllCheckedNodes();

            if (!nodes.Any())
            {
                MessageShower.ShowWarning("Vui lòng chọn công tác!");
                return;
            }

            var items = (from CheckedListBoxItem item in ccbe_DuLieuCanLay.Properties.Items
                       where item.CheckState == CheckState.Checked
                       select (KeyValuePair<string, string>)item.Value).ToArray();

            if (!items.Any())
            {
                MessageShower.ShowWarning("Vui lòng chọn dữ liệu cần lấy!");
                return;
            }

            if (MessageShower.ShowYesNoQuestion($"Bạn có muốn cập nhật \"{ccbe_DuLieuCanLay.Text}\" không?") != DialogResult.Yes)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                if (!(bool)dr["Chon"])
                    continue;

                foreach (var item in items)
                    dr[item.Key] = dr[item.Value];
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, Server.Tbl_GiaoViec_CongViecCha);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sB_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sb_CheckAll_Click(object sender, EventArgs e)
        {
            tl_Data.CheckAll();

        }

        private void sb_uncheckAll_Click(object sender, EventArgs e)
        {
            tl_Data.UncheckAll();

        }
    }
}