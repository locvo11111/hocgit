using DevExpress.Mvvm.Native;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
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
    public partial class Form_LayDauViecTuTDKHSangGiaoViec : Form
    {
        //DataProvider m_db = new DataProvider("");
        //int _type = -1;
        //string  _codeKy;
        string _codeDA;
        public Form_LayDauViecTuTDKHSangGiaoViec(string codeDA)
        {
            InitializeComponent();
            _codeDA = codeDA;
        }

        public delegate void DE_TRUYENDATABANGCONGTAC(DataTable dt);//, string colCodeDonViThucHien, string CodeDonViThucHien);
        public DE_TRUYENDATABANGCONGTAC m_truyenData;
        private void Form_LayDauViecTuCSDL_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> dicDoiTuong = new Dictionary<string, string>()
            {
                {MyConstant.TBL_THONGTINNHATHAU, "Nhà Thầu" },
                {MyConstant.TBL_THONGTINNHATHAUPHU, "Nhà Thầu Phụ" },
                {MyConstant.TBL_THONGTINTODOITHICONG, "Tổ Đội" },
            };

            cbb_DoiTuong.DataSource = dicDoiTuong.ToList();
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            string colCodeDonViThucHien = "Code" + MyFunction.fcn_RemoveAccents(cbb_DoiTuong.Text).Replace(" ", "");

            DataTable dtSoure = (gc_CongTacChuaLay.DataSource as DataTable);
            DataTable dtTable = dtSoure.Clone();
            
            foreach (DataRow row in dtSoure.Rows)
            {
                if ((bool)row["Chon"])
                    dtTable.Rows.Add(row.ItemArray);
            }
            m_truyenData(dtTable);//, colCodeDonViThucHien, (string)cbb_DonViThucHien.SelectedValue);
            this.Close();
        }

        private void cbb_DoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_DoiTuong.SelectedIndex < 0)
                return;

            string dbString = $"SELECT \"Code\", \"Ten\" FROM {cbb_DoiTuong.SelectedValue} WHERE \"CodeDuAn\" = '{_codeDA}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            cbb_DonViThucHien.DataSource = dt;
            //cbb_DonViThucHien.SelectedValue = _dr[$"Code{MyFunction.fcn_RemoveAccents(cbb_DoiTuong.Text).Replace(" ", "")}"];
        }

        private void cbb_DonViThucHien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string colCodeDonViThucHien = "Code" + MyFunction.fcn_RemoveAccents(cbb_DoiTuong.Text).Replace(" ", "");
            string dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.Code, {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac, " +
                $"{GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha, {TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong, {TDKH.TBL_DanhMucCongTac}.TenCongTac, {TDKH.TBL_DanhMucCongTac}.CodeHangMuc, {TDKH.TBL_DanhMucCongTac}.MaHieuCongTac, {TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongToanBo, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.NgayBatDau, {TDKH.TBL_ChiTietCongTacTheoKy}.NgayKetThuc, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThau, {TDKH.TBL_ChiTietCongTacTheoKy}.CodeToDoi, {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThauPhu, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongToanBo, {TDKH.TBL_ChiTietCongTacTheoKy}.TrangThai " +
                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
                $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                $"LEFT JOIN {GiaoViec.TBL_CONGVIECCHA} " +
                $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeCongTacTheoGiaiDoan = {TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                $"AND {GiaoViec.TBL_CONGVIECCHA}.{colCodeDonViThucHien} = {TDKH.TBL_ChiTietCongTacTheoKy}.{colCodeDonViThucHien} " +
                $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.{colCodeDonViThucHien} = '{cbb_DonViThucHien.SelectedValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dt.Columns.Add("Chon", typeof(bool));
            dt.AsEnumerable().ForEach(x => x["Chon"] = false);

            DataTable dtSource = dt.Clone();

            foreach (DataRow dr in dt.Rows)
            {
                if (dtSource.AsEnumerable().Where(x => (string)x["CodeCongTac"] == (string)dr["CodeCongTac"]).Any())
                    continue;

                var Enum = dt.AsEnumerable().Where(x => (string)x["CodeCongTac"] == (string)dr["CodeCongTac"]);
                dr["NgayBatDau"] = Enum.OrderBy(x => x["NgayBatDau"]).First()["NgayBatDau"] as string;
                dr["NgayKetThuc"] = Enum.OrderBy(x => x["NgayKetThuc"]).Last()["NgayKetThuc"] as string;
                dtSource.Rows.Add(dr.ItemArray);
            }

            try
            {
                gc_CongTacChuaLay.DataSource = dtSource.AsEnumerable().Where(x => x["CodeCongViecCha"] == DBNull.Value).CopyToDataTable();
            }
            catch { gc_CongTacChuaLay.DataSource = null; }

            try
            {
                gc_CongTacDaLay.DataSource = dtSource.AsEnumerable().Where(x => x["CodeCongViecCha"] != DBNull.Value).CopyToDataTable();
            }
            catch { gc_CongTacDaLay.DataSource = null; }
        }
    }
}
