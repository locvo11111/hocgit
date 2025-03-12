using DevExpress.Utils;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Report;
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
    public partial class Form_XuatBaoCaoDuAn : DevExpress.XtraEditors.XtraForm
    {
        DateTime _nbd = DateTime.Now.Date;
        DateTime _nkt = DateTime.Now.Date;
        public Form_XuatBaoCaoDuAn()
        {
            InitializeComponent();
            DuAnHelper.GetDonViThucHiens(ctrl_DonViThucHienDuAn);
            dv_BaoCao.Hide();
            de_Date.DateTime = DateTime.Now;
            string dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} WHERE \"Code\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable dtDA = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtDA.Rows.Count != 0)
            {
                _nbd = DateTime.Parse(dtDA.Rows[0]["NgayBatDau"].ToString());
                _nkt = DateTime.Parse(dtDA.Rows[0]["NgayKetThuc"].ToString());

                //de_Begin.DateTime 
                //    = de_Begin.Properties.MinValue
                //    = de_End.Properties.MinValue
                //    = DateTime.Parse(dtDA.Rows[0]["NgayBatDau"].ToString());
                
                //de_End.DateTime 
                //    = de_Begin.Properties.MaxValue
                //    = de_End.Properties.MaxValue
                //    = DateTime.Parse(dtDA.Rows[0]["NgayKetThuc"].ToString());

                //de_Begin.DateTime = DateTime.Parse(dtDA.Rows[0]["NgayBatDau"].ToString());
                //de_End.DateTime = DateTime.Parse(dtDA.Rows[0]["NgayKetThuc"].ToString());
            }
            //else
            //    de_Begin.DateTime=de_End.DateTime= DateTime.Now;
        }

        private void sb_xuatbaocao_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu, Vui lòng chờ!");
            string dbString = $"DELETE FROM Tbl_BaoCaoDuAn";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            dbString = $"DELETE FROM Tbl_PhanTramTrangThai";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            dbString = $"DELETE FROM Tbl_PhanTram_PLTCBG";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            dbString = $"DELETE FROM {MyConstant.TBL_THONGTINCONGTRINH}";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            dbString = $"DELETE FROM {MyConstant.TBL_THONGTINHANGMUC}";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);

            //dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} WHERE \"Code\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            //DataTable dtDA = DataProvider.InstanceTHDA.ExecuteQuery(dbString);            
            double songay = (_nkt - _nbd).Days + 1;
            double songaythuchien=(de_Date.DateTime.Date - _nbd).TotalDays;
            //songay = songay < 0 ? 0 : songay;
            songaythuchien = songaythuchien < 0 ? 0 : songaythuchien;
            BaoCaoDuAn Baocao = new BaoCaoDuAn();
            Chart TongHopBieuDo = new Chart();
            //if (ce_DuAn.Checked)
            //{
            //    dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
            //                    $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
            //                    $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
            //                    $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND " +
            //                    $" {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThau IS NULL AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeToDoi IS NULL AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThauPhu IS NULL ORDER BY \"Row\" ASC";
            //    Baocao.Fcn_Update("DỰ ÁN");
            //}
            DonViThucHien DVTH = new DonViThucHien();
            if (ctrl_DonViThucHienDuAn.EditValue!= "Chọn đơn vị thực hiện")
            {
                DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
                dbString = $"SELECT COALESCE(dmct.CodeHangMuc, cttk.CodeHangMuc) AS CodeHangMuc,cttk.* FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                                $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
                                $"ON dmct.Code = cttk.CodeCongTac " +
                                $"WHERE cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                                $" AND cttk.'{DVTH.ColCodeFK}'='{DVTH.Code}' ORDER BY \"Row\" ASC";
                Baocao.Fcn_Update(DVTH.Ten.ToUpper());
            }
            else
            {
                MessageShower.ShowError("Vui lòng chọn Dự án hoặc đơn vị thực hiện!");
                WaitFormHelper.CloseWaitForm();
                return;
            }
            Dictionary<string, double> TrangThai = new Dictionary<string, double>();
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string ngayketthuc = de_Date.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dbString = $"SELECT {GiaoViec.TBL_CONGVIECCHA}.TrangThai FROM {GiaoViec.TBL_CONGVIECCHA} "+
            $"LEFT JOIN {GiaoViec.TBL_DauViecNho} " +
            $"ON {GiaoViec.TBL_DauViecNho}.Code = {GiaoViec.TBL_CONGVIECCHA}.CodeDauMuc " +    
            $"LEFT JOIN {GiaoViec.TBL_DauViecLon} " +
            $"ON {GiaoViec.TBL_DauViecLon}.Code = {GiaoViec.TBL_DauViecNho}.CodeDauViecLon " +   
            $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
            $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {GiaoViec.TBL_CONGVIECCHA}.CodeCongTacTheoGiaiDoan " +
            $"LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
            $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code " +
            $"LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
            $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code " +
            $"WHERE {GiaoViec.TBL_DauViecLon}.CodeDuAn ='{SharedControls.slke_ThongTinDuAn.EditValue}' OR {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn ='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_GiaoViec = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            double CTH = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Chưa thực hiện").ToArray().Count();
            double DTH = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Đang thực hiện").ToArray().Count();
            double DXD = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Đang xét duyệt").ToArray().Count();
            double KT = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Đề nghị kiểm tra").ToArray().Count();
            double HT = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Hoàn thành").ToArray().Count();
            double DHD = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Dừng hoạt động").ToArray().Count();
            double Sum = (CTH + DTH + DXD + KT + HT + DHD)==0?1: (CTH + DTH + DXD + KT + HT + DHD);
            TrangThai.Add("Chưa thực hiện", CTH / Sum * 100);
            TrangThai.Add("Đang thực hiện", DTH / Sum * 100);
            TrangThai.Add("Đang xét duyệt", DXD / Sum * 100);
            TrangThai.Add("Đề nghị kiểm tra", KT / Sum * 100);
            TrangThai.Add("Hoàn thành", HT / Sum * 100);
            TrangThai.Add("Dừng hoạt động", DHD / Sum * 100);
            foreach (KeyValuePair<string, double> item in TrangThai)
            {
                dbString = $"INSERT INTO Tbl_PhanTram_PLTCBG (\"Code\",\"Ten\",\"Count\") VALUES ('{Guid.NewGuid()}',@Ten,'{item.Value}')";
                DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString, parameter: new object[] { item.Key });
                dbString = $"INSERT INTO Tbl_PhanTramTrangThai (\"Code\",\"Ten\",\"Count\") VALUES ('{Guid.NewGuid()}',@Ten,'{item.Value}')";
                DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString, parameter: new object[] { item.Key });
            }
            TrangThai.Clear();
//            dbString = $"SELECT \"TrangThai\" FROM {GiaoViec.TBL_CONGVIECCHA} " +
//$"LEFT JOIN {GiaoViec.TBL_DAUVIECNho} " +
//$"ON {GiaoViec.TBL_DAUVIECNho}.Code = {GiaoViec.TBL_CONGVIECCHA}.CodeDauMuc " +
//$"INNER JOIN {GiaoViec.TBL_DAUVIECLON} " +
//$"ON {GiaoViec.TBL_DAUVIECLON}.Code = {GiaoViec.TBL_DAUVIECNho}.CodeDauViecLon " +
//$"WHERE {GiaoViec.TBL_DAUVIECLON}.CodeDuAn ='{SharedControls.slke_ThongTinDuAn.EditValue}'";
//            dt_GiaoViec = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //CTH = (double)dtCongTacTheoKy.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Chưa thực hiện").ToArray().Count();
            //DTH = (double)dtCongTacTheoKy.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Đang thực hiện").ToArray().Count();
            //DXD = (double)dtCongTacTheoKy.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Đang xét duyệt").ToArray().Count();
            //KT = (double)dtCongTacTheoKy.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Đề nghị kiểm tra").ToArray().Count();
            //HT = (double)dtCongTacTheoKy.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Hoàn thành").ToArray().Count();
            //DHD = (double)dtCongTacTheoKy.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Dừng hoạt động").ToArray().Count();
            //Sum = (CTH + DTH + DXD + KT + HT + DHD) == 0 ? 1 : (CTH + DTH + DXD + KT + HT + DHD);
            //TrangThai.Add("Chưa thực hiện", CTH / Sum * 100);
            //TrangThai.Add("Đang thực hiện", DTH / Sum * 100);
            //TrangThai.Add("Đang xét duyệt", DXD / Sum * 100);
            //TrangThai.Add("Đề nghị kiểm tra", KT / Sum * 100);
            //TrangThai.Add("Hoàn thành", HT / Sum * 100);
            //TrangThai.Add("Dừng hoạt động", DHD / Sum * 100);
            //foreach (KeyValuePair<string, double> item in TrangThai)
            //{
            //    dbString = $"INSERT INTO Tbl_PhanTramTrangThai (\"Code\",\"Ten\",\"Count\") VALUES ('{Guid.NewGuid()}','{item.Key}','{item.Value}')";
            //    DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            //}
            //dbString = $"SELECT * FROM Tbl_PhanTramTrangThai ";
            //DataTable dt_TrangThai= DataProvider.InstanceBaoCao.ExecuteQuery(dbString);

            //dbString = $"SELECT * FROM {TDKH.TBL_KhoiLuongCongViecHangNgay}";
            //DataTable dt_KLHN= DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            int stt = 1;
            foreach (DataRow row in dtCT.Rows)
            {
                dbString = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"TenCongTrinh\") VALUES ('{row["Code"]}',@Ten)";
                DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString, parameter: new object[] { row["Ten"] });
            }
            foreach (DataRow row in dtHM.Rows)
            {
                dbString = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"TenHangMuc\",\"CodeCongTrinh\") VALUES ('{row["Code"]}','{row["Ten"]}','{row["CodeCongTrinh"]}')";
                DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            }
            double TongTC = 0, TongKH = 0;
            string[] lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            var dtKLHN = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac);
            //DataTable dtKLHNCon = new DataTable();
            //if (DVTH.IsGiaoThau)
            //{
            //    dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeNhaThau\" IS NULL ";
            //    DataTable dtcon= DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //    lsCodeCongTac = dtcon.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            //    dtKLHNCon = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac,ignoreKLKH:true);
            //}
            foreach (DataRow row in dtCongTacTheoKy.Rows)
            {
                string Code = Guid.NewGuid().ToString();
                double KLTC = 0;
                double KLKH = 0;

                    var crRow = dtKLHN.AsEnumerable().Where(x => x.CodeCongTacTheoGiaiDoan == row["Code"].ToString() && x.Ngay <= DateTime.Parse(ngayketthuc)).ToArray();
                    KLKH = crRow.Sum(x => x.KhoiLuongKeHoach) ?? 0;

                    KLTC = crRow.Sum(x => x.KhoiLuongThiCong) ?? 0;

 
                TongTC += KLTC;
                TongKH += KLKH;
                string tencongtac = row["TenCongTac"].ToString();
                dbString = $"INSERT INTO Tbl_BaoCaoDuAn (\"Code\",\"TenCongViec\",\"TrangThai\",\"BatDau\",\"KetThuc\",\"STT\",\"CodeHangMuc\"," +
                    $"\"KhoiLuongHopDong\",\"KhoiLuongThiCong\",\"KhoiLuongKeHoach\",\"KhoiLuongThanhToan\") VALUES" +
                    $" ('{Code}',@Ten,'{row["TrangThai"]}'," +
                    $"'{DateTime.Parse(row["NgayBatDau"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{DateTime.Parse(row["NgayKetThuc"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'" +
                    $",'{stt++}','{row["CodeHangMuc"]}','{row["KhoiLuongHopDongChiTiet"]}','{KLTC}','{KLKH}','{0}') ";
                DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString, parameter: new object[] { row["TenCongTac"] });
            }
            TongKH = TongKH == 0 ? 1 : TongKH;
            TongTC = TongTC == 0 ? 1 : TongTC;
            TongHopBieuDo.Fcn_Update($"{CTH} công việc,chiếm tỷ trọng {Math.Round(CTH / Sum * 100,2)}%",$"{DTH} công việc,chiếm tỷ trọng {Math.Round(DTH / Sum * 100,2)}%",$"{DXD} công việc,chiếm tỷ trọng {Math.Round(DXD / Sum * 100,2)}%"
                , $"{KT} công việc,chiếm tỷ trọng {Math.Round(KT / Sum * 100,2)}%", $"{HT} công việc,chiếm tỷ trọng {Math.Round(HT / Sum * 100,2)}%", $"{DHD} công việc,chiếm tỷ trọng {Math.Round(DHD / Sum * 100,2)}%",$"{Math.Round(songaythuchien / songay * 100, 2)}%",$"{songaythuchien}/{songay}",$"{Math.Round(TongTC /TongKH*100,2)}%", $"{dtCongTacTheoKy.Rows.Count}");
            Baocao.Fcn_UpdateSub(TongHopBieuDo);
            dv_BaoCao.UseAsyncDocumentCreation = DefaultBoolean.True;
            dv_BaoCao.RequestDocumentCreation = true;
            dv_BaoCao.DocumentSource = Baocao;
            dv_BaoCao.InitiateDocumentCreation();
            dv_BaoCao.Visible = true;
            WaitFormHelper.CloseWaitForm();
            MessageShower.ShowInformation("Xuất báo cáo thành công!", "Thông tin");
            //dv_BaoCao.Refresh();
            //dv_BaoCao.DocumentSource = Baocao;
        }
    }
}