using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.HopDong;
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
    public partial class Ctrl_TongHopHopDong : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_TongHopHopDong()
        {
            InitializeComponent();
        }
        public void Fcn_HopDongMeCon()
        {
            string dbString = $"SELECT {MyConstant.TBL_Tonghopdanhsachhopdong}.*,{MyConstant.Tbl_TAOMOIHOPDONG}.GiaTriHopDong,{MyConstant.Tbl_TAOMOIHOPDONG}.TrangThai,{MyConstant.Tbl_TAOMOIHOPDONG}.SoHopDong," +
$"{MyConstant.Tbl_TAOMOIHOPDONG}.CodeNhaThau,{MyConstant.Tbl_TAOMOIHOPDONG}.CodeDonViThucHien,{MyConstant.Tbl_TAOMOIHOPDONG}.TenHopDong,{MyConstant.Tbl_TAOMOIHOPDONG}.NgayBatDau,{MyConstant.Tbl_TAOMOIHOPDONG}.NgayKetThuc,{MyConstant.Tbl_HopDong_LoaiHopDong}.Ten as LoaiHopDong  " +
$"FROM {MyConstant.TBL_Tonghopdanhsachhopdong} " +
$"INNER JOIN {MyConstant.Tbl_TAOMOIHOPDONG} " +
$"ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code = {MyConstant.TBL_Tonghopdanhsachhopdong}.CodeHopDong " +
$"INNER JOIN {MyConstant.Tbl_HopDong_LoaiHopDong} " +
$"ON {MyConstant.Tbl_TAOMOIHOPDONG}.CodeLoaiHopDong = {MyConstant.Tbl_HopDong_LoaiHopDong}.Code" +
$" WHERE {MyConstant.Tbl_TAOMOIHOPDONG}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count == 0)
                return;
            List<TongHopHopDong> CTHD = DuAnHelper.ConvertToList<TongHopHopDong>(dt);
            List<TongHopHopDong> HDMe = CTHD.Where(x => x.CodeNhaThau==x.CodeDonViThucHien).ToList();
            List<TongHopHopDong> HDCon = CTHD.Where(x => x.CodeNhaThau != x.CodeDonViThucHien).ToList();
            string lstCode = MyFunction.fcn_Array2listQueryCondition(CTHD.Select(x => x.CodeHopDong).ToArray());
            dbString = $"SELECT {MyConstant.Tbl_CHITIETHOPDONG}.*,{MyConstant.Tbl_TAOMOIHOPDONG}.GiaTriHopDong " +
            $"FROM {MyConstant.Tbl_CHITIETHOPDONG} " +
            $"INNER JOIN {MyConstant.Tbl_TAOMOIHOPDONG} ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code={MyConstant.Tbl_CHITIETHOPDONG}.CodeHopDong" +
            $" WHERE {MyConstant.Tbl_CHITIETHOPDONG}.CodeHopDong IN ({lstCode}) AND ({MyConstant.Tbl_CHITIETHOPDONG}.Loai=3 OR {MyConstant.Tbl_CHITIETHOPDONG}.Loai=6)";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<ChiTietHopDong> ChiTiet = DuAnHelper.ConvertToList<ChiTietHopDong>(dt);
            int STT = 1;
            dbString = $"SELECT {MyConstant.TBL_hopdongAB_HT}.*,{TDKH.TBL_DanhMucCongTac}.CodeHangMuc,{TDKH.TBL_DanhMucCongTac}.MaHieuCongTac,{TDKH.TBL_DanhMucCongTac}.TenCongTac,{TDKH.TBL_DanhMucCongTac}.DonVi," +
                $"{MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan,{MyConstant.TBL_HopDong_DoBoc}.IsEdit,{MyConstant.TBL_HopDong_PhuLuc}.DonGia,{TDKH.TBL_DanhMucCongTac}.Code as CodeCongTac " +
                $"FROM {MyConstant.TBL_hopdongAB_HT} " +
                $"INNER JOIN {MyConstant.TBL_HopDong_DotHopDong} " +
                $"ON {MyConstant.TBL_HopDong_DotHopDong}.Code = {MyConstant.TBL_hopdongAB_HT}.CodeDot " +
                    $"INNER JOIN {MyConstant.TBL_HopDong_DoBoc} " +
                    $"ON {MyConstant.TBL_HopDong_DoBoc}.Code = {MyConstant.TBL_hopdongAB_HT}.CodeDB " +
                    $"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.Code = {MyConstant.TBL_HopDong_DoBoc}.CodePL " +
                    $"INNER JOIN {MyConstant.TBL_ThongtinphulucHD} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodePl = {MyConstant.TBL_ThongtinphulucHD}.Code " +
                    $"INNER JOIN {MyConstant.TBL_tonghopdanhsachhopdong} " +
                    $"ON {MyConstant.TBL_ThongtinphulucHD}.CodeHd = {MyConstant.TBL_tonghopdanhsachhopdong}.Code " +
                    $"INNER JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan = {TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                    $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
                    $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                    $" WHERE {MyConstant.TBL_tonghopdanhsachhopdong}.CodeHopDong IN ({lstCode}) " +
                    $"AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
            DataTable dtHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"CodeHd\" IN ({lstCode})";
            DataTable dtHDDot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<TongHopHopDong> CTHDCon = new List<TongHopHopDong>();

            foreach (var item in HDMe)
            {
                item.ParentCode = "0";
                item.STT = STT++;
                List<ChiTietHopDong> ChiTietCon = new List<ChiTietHopDong>();
                if (ChiTiet.Count() != 0)
                {
                    ChiTietCon = ChiTiet.Where(x => x.CodeHopDong == item.CodeHopDong).ToList();
                    foreach (ChiTietHopDong crow in ChiTietCon)
                    {
                        if (crow.Loai == 3)
                        {
                            item.ThoiGianBaoLanh = DateTime.Parse(crow.Ngay);
                            item.TienBaoLanh = crow.SoTienCal;
                        }
                        else
                        {
                            item.ThoiGianBaoHanh = DateTime.Parse(crow.Ngay);
                            item.TienBaoHanh = crow.SoTienCal;
                        }
                    }
                }
                dbString = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG_MECON} WHERE \"CodeMe\"='{item.CodeHopDong}'";
                DataTable dtHDCon = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                lstCode = MyFunction.fcn_Array2listQueryCondition(dtHDCon.AsEnumerable().Select(x => x["CodeCon"].ToString()).ToArray());
                List<TongHopHopDong> _HDCon = HDCon.Where(x => lstCode.Contains(x.CodeHopDong)).ToList();
                foreach(var _itemcon in _HDCon)
                {
                    _itemcon.ParentCode = item.Code;
                    if (ChiTiet.Count() != 0)
                    {
                        ChiTietCon = ChiTiet.Where(x => x.CodeHopDong == _itemcon.CodeHopDong).ToList();
                        foreach (ChiTietHopDong crow in ChiTietCon)
                        {
                            if (crow.Loai == 3)
                            {
                                _itemcon.ThoiGianBaoLanh = DateTime.Parse(crow.Ngay);
                                _itemcon.TienBaoLanh = crow.SoTienCal;
                            }
                            else
                            {
                                _itemcon.ThoiGianBaoHanh = DateTime.Parse(crow.Ngay);
                                _itemcon.TienBaoHanh = crow.SoTienCal;
                            }
                        }
                    }
                    DataRow[] CrowDot = dtHDDot.AsEnumerable().Where(x => x["CodeHd"].ToString() == _itemcon.CodeHopDong).ToArray();
                    foreach (var row in CrowDot)
                    {
                        DataRow[] CrowHD = dtHD.AsEnumerable().Where(x => x["CodeDot"].ToString() == row["Code"].ToString()).ToArray();
                        long ThanhTien = CrowHD.Where(x => x["ThucHienKyNay"] != DBNull.Value && x["DonGia"] != DBNull.Value).Sum(x => (long)Math.Round(double.Parse(x["ThucHienKyNay"].ToString()) * double.Parse(x["DonGia"].ToString())));
                        if (ChiTietCon.Count() != 0)
                            ChiTietCon.ForEach(x => x.GiaTriHopDong = ThanhTien);
                        CTHDCon.Add(new TongHopHopDong()
                        {
                            Code = row["Code"].ToString(),
                            ParentCode = _itemcon.Code,
                            TenHopDong = row["Ten"].ToString(),
                            GiaTriHopDong = ThanhTien,
                            NgayBatDau = DateTime.Parse(row["NgayBatDau"].ToString()),
                            NgayKetThuc = DateTime.Parse(row["NgayKetThuc"].ToString()),
                            TienBaoLanh = ChiTietCon.Where(x => x.Loai == 3).Any() ? ChiTietCon.Where(x => x.Loai == 3).FirstOrDefault().SoTienCal : 0,
                            TienBaoHanh = ChiTietCon.Where(x => x.Loai == 6).Any() ? ChiTietCon.Where(x => x.Loai == 6).FirstOrDefault().SoTienCal : 0,
                        });

                    }

                }

            }

            if (CTHDCon != null)
                CTHD.AddRange(CTHDCon);
            Tl_TongHopDong.DataSource = CTHD;
            Tl_TongHopDong.RefreshDataSource();
            Tl_TongHopDong.Refresh();

        }
        public void Fcn_Update()
        {
            string dbString = $"SELECT DA.Code as CodeDuAn,DA.TenDuAn,{MyConstant.TBL_Tonghopdanhsachhopdong}.*,{MyConstant.Tbl_TAOMOIHOPDONG}.GiaTriHopDong,{MyConstant.Tbl_TAOMOIHOPDONG}.TrangThai,{MyConstant.Tbl_TAOMOIHOPDONG}.SoHopDong," +
  $"{MyConstant.Tbl_TAOMOIHOPDONG}.CodeNhaThau,{MyConstant.Tbl_TAOMOIHOPDONG}.CodeBenGiao,{MyConstant.Tbl_TAOMOIHOPDONG}.TenHopDong,{MyConstant.Tbl_TAOMOIHOPDONG}.NgayBatDau,{MyConstant.Tbl_TAOMOIHOPDONG}.NgayKetThuc,{MyConstant.Tbl_HopDong_LoaiHopDong}.Ten as LoaiHopDong  " +
  $"FROM {MyConstant.TBL_Tonghopdanhsachhopdong} " +
  $"LEFT JOIN {MyConstant.Tbl_TAOMOIHOPDONG} " +
  $"ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code = {MyConstant.TBL_Tonghopdanhsachhopdong}.CodeHopDong " +
  $"LEFT JOIN {MyConstant.Tbl_HopDong_LoaiHopDong} " +
  $"ON {MyConstant.Tbl_TAOMOIHOPDONG}.CodeLoaiHopDong = {MyConstant.Tbl_HopDong_LoaiHopDong}.Code " +
  $"LEFT JOIN {MyConstant.TBL_THONGTINDUAN} DA " +
  $"ON {MyConstant.Tbl_TAOMOIHOPDONG}.CodeDuAn = DA.Code";
  //$" WHERE {MyConstant.Tbl_TAOMOIHOPDONG}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count == 0)
                return;
            List<TongHopHopDong> CTHD = DuAnHelper.ConvertToList<TongHopHopDong>(dt);
            List<TongHopHopDong> CTHDCon = new List<TongHopHopDong>();
            string lstCode = MyFunction.fcn_Array2listQueryCondition(CTHD.Select(x => x.CodeHopDong).ToArray());
            dbString = $"SELECT {MyConstant.Tbl_CHITIETHOPDONG}.*,{MyConstant.Tbl_TAOMOIHOPDONG}.GiaTriHopDong " +
            $"FROM {MyConstant.Tbl_CHITIETHOPDONG} " +
            $"INNER JOIN {MyConstant.Tbl_TAOMOIHOPDONG} ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code={MyConstant.Tbl_CHITIETHOPDONG}.CodeHopDong" +
            $" WHERE {MyConstant.Tbl_CHITIETHOPDONG}.CodeHopDong IN ({lstCode}) AND ({MyConstant.Tbl_CHITIETHOPDONG}.Loai=3 OR {MyConstant.Tbl_CHITIETHOPDONG}.Loai=6)";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<ChiTietHopDong> ChiTiet = DuAnHelper.ConvertToList<ChiTietHopDong>(dt);
            int STT = 1;
            dbString = $"SELECT {MyConstant.TBL_hopdongAB_HT}.*,{TDKH.TBL_DanhMucCongTac}.CodeHangMuc,{TDKH.TBL_DanhMucCongTac}.MaHieuCongTac,{TDKH.TBL_DanhMucCongTac}.TenCongTac,{TDKH.TBL_DanhMucCongTac}.DonVi," +
                $"{MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan,{MyConstant.TBL_HopDong_DoBoc}.IsEdit,{MyConstant.TBL_HopDong_PhuLuc}.DonGia,{TDKH.TBL_DanhMucCongTac}.Code as CodeCongTac " +
                $"FROM {MyConstant.TBL_hopdongAB_HT} " +
                $"INNER JOIN {MyConstant.TBL_HopDong_DotHopDong} " +
                $"ON {MyConstant.TBL_HopDong_DotHopDong}.Code = {MyConstant.TBL_hopdongAB_HT}.CodeDot " +
                    $"INNER JOIN {MyConstant.TBL_HopDong_DoBoc} " +
                    $"ON {MyConstant.TBL_HopDong_DoBoc}.Code = {MyConstant.TBL_hopdongAB_HT}.CodeDB " +
                    $"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.Code = {MyConstant.TBL_HopDong_DoBoc}.CodePL " +
                    $"INNER JOIN {MyConstant.TBL_ThongtinphulucHD} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodePl = {MyConstant.TBL_ThongtinphulucHD}.Code " +
                    $"INNER JOIN {MyConstant.TBL_tonghopdanhsachhopdong} " +
                    $"ON {MyConstant.TBL_ThongtinphulucHD}.CodeHd = {MyConstant.TBL_tonghopdanhsachhopdong}.Code " +
                    $"INNER JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan = {TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                    $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
                    $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                    $" WHERE {MyConstant.TBL_tonghopdanhsachhopdong}.CodeHopDong IN ({lstCode}) " +
                    $"AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
            DataTable dtHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"CodeHd\" IN ({lstCode})";
            DataTable dtHDDot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<TongHopHopDong> Source = new List<TongHopHopDong>();
            var GroupDuAn = CTHD.GroupBy(x => new { x.CodeDuAn, x.TenDuAn });
            string CodeThu = "", CodeChi = "", Parent="";
            long SLTC = 0, TTU = 0, TTT = 0, TTN = 0;
            foreach(var DA in GroupDuAn)
            {
                Source.Add(new TongHopHopDong
                {
                    ParentCode = "0",
                    Code=DA.Key.CodeDuAn,
                    TenHopDong=DA.Key.TenDuAn
                }) ;
                CodeThu = Guid.NewGuid().ToString();
                CodeChi = Guid.NewGuid().ToString();
                TongHopHopDong HDTHU = new TongHopHopDong();
                HDTHU.ParentCode = DA.Key.CodeDuAn;
                HDTHU.Code = CodeThu;
                HDTHU.TenHopDong = "HỢP ĐỒNG THU";         
                
                TongHopHopDong HDChi= new TongHopHopDong();
                HDChi.ParentCode = DA.Key.CodeDuAn;
                HDChi.Code = CodeChi;
                HDChi.TenHopDong = "HỢP ĐỒNG CHI";

                //Source.Add(new TongHopHopDong
                //{
                //    ParentCode =DA.Key.CodeDuAn,
                //    Code = CodeThu,
                //    TenHopDong = "HỢP ĐỒNG THU"
                //});
                //Source.Add(new TongHopHopDong
                //{
                //    ParentCode = DA.Key.CodeDuAn,
                //    Code = CodeChi,
                //    TenHopDong ="HỢP ĐỒNG CHI"
                //});
                var GroupHD = DA.OrderBy(x=>x.HDChinh).GroupBy(x => new { x.Code, x.TenHopDong });
                foreach(var HD in GroupHD)
                {
                    SLTC = 0; TTU = 0; TTT = 0; TTN = 0;
                    Parent = HD.FirstOrDefault().HDChinh ? CodeThu : CodeChi;
                    var FirstHD = HD.FirstOrDefault();
                    TongHopHopDong NewHD = new TongHopHopDong();
                    List<ChiTietHopDong> ChiTietCon = new List<ChiTietHopDong>();
                    if (ChiTiet.Count() != 0)
                    {
                        ChiTietCon = ChiTiet.Where(x => x.CodeHopDong == FirstHD.CodeHopDong).ToList();
                        foreach (ChiTietHopDong crow in ChiTietCon)
                        {
                            if (crow.Loai == 3)
                            {
                                NewHD.ThoiGianBaoLanh = DateTime.Parse(crow.Ngay);
                                NewHD.TienBaoLanh = crow.SoTienCal;
                            }
                            else
                            {
                                NewHD.ThoiGianBaoHanh = DateTime.Parse(crow.Ngay);
                                NewHD.TienBaoHanh = crow.SoTienCal;
                            }
                        }
                    }
                    NewHD.ParentCode = Parent;
                    NewHD.Code = HD.Key.Code;
                    NewHD.TenHopDong = HD.Key.TenHopDong;
                    NewHD.SoHopDong = FirstHD.SoHopDong;
                    NewHD.TrangThai = FirstHD.TrangThai;
                    NewHD.GiaTriHopDong = FirstHD.GiaTriHopDong;
                    NewHD.LoaiHopDong = FirstHD.LoaiHopDong;
                    NewHD.NgayBatDau = FirstHD.NgayBatDau;
                    NewHD.NgayKetThuc = FirstHD.NgayKetThuc;
                    DataRow[] CrowDot = dtHDDot.AsEnumerable().Where(x => x["CodeHd"].ToString() == FirstHD.CodeHopDong).ToArray();
                    foreach (var row in CrowDot)
                    {
                        DataRow[] CrowHD = dtHD.AsEnumerable().Where(x => x["CodeDot"].ToString() == row["Code"].ToString()).ToArray();
                        long ThanhTien = CrowHD.Where(x => x["ThucHienKyNay"] != DBNull.Value && x["DonGia"] != DBNull.Value).Sum(x => (long)Math.Round(double.Parse(x["ThucHienKyNay"].ToString()) * double.Parse(x["DonGia"].ToString())));
                        if (ChiTietCon.Count() != 0)
                            ChiTietCon.ForEach(x => x.GiaTriHopDong = ThanhTien);
                        CTHDCon.Add(new TongHopHopDong()
                        {
                            Code = $"{row["Code"]}_Dot",
                            ParentCode = HD.Key.Code,
                            TenHopDong = row["Ten"].ToString(),
                            //GiaTriHopDong = ThanhTien,
                            NgayBatDau = DateTime.Parse(row["NgayBatDau"].ToString()),
                            NgayKetThuc = DateTime.Parse(row["NgayKetThuc"].ToString()),
                            TienBaoLanh = ChiTietCon.Where(x => x.Loai == 3).Any() ? ChiTietCon.Where(x => x.Loai == 3).FirstOrDefault().SoTienCal : 0,
                            TienBaoHanh = ChiTietCon.Where(x => x.Loai == 6).Any() ? ChiTietCon.Where(x => x.Loai == 6).FirstOrDefault().SoTienCal : 0,
                            TrangThai= row["TrangThai"].ToString(),
                            SanLuongThiCong=row["SanLuongThiCong"]!=DBNull.Value?long.Parse(row["SanLuongThiCong"].ToString()):0,
                            SoTienThucNhan=row["SoTienThucNhan"] !=DBNull.Value?long.Parse(row["SoTienThucNhan"].ToString()): ThanhTien,
                            SoTienThanhToan= ThanhTien,
                            SoTienDaTamUng=row["SoTienDaTamUng"] !=DBNull.Value?long.Parse(row["SoTienDaTamUng"].ToString()):0,
                        });
                        SLTC += row["SanLuongThiCong"] != DBNull.Value ? long.Parse(row["SanLuongThiCong"].ToString()) : 0;
                        TTU += row["SoTienDaTamUng"] != DBNull.Value ? long.Parse(row["SoTienDaTamUng"].ToString()) : 0;
                        TTT += ThanhTien;
                        TTN += row["SoTienThucNhan"] != DBNull.Value ? long.Parse(row["SoTienThucNhan"].ToString()) : ThanhTien;
                    }
                    NewHD.SoTienThanhToan = TTT;
                    NewHD.SoTienDaTamUng = TTU;
                    NewHD.SanLuongThiCong = SLTC;
                    NewHD.SoTienThucNhan = TTN;
                    Source.Add(NewHD);
                }
                if (Source.Where(x => x.ParentCode == CodeThu).Any())
                {
                    HDTHU.GiaTriHopDong = Source.Where(x => x.ParentCode == CodeThu).Sum(x => x.GiaTriHopDong);
                    HDTHU.SoTienDaTamUng = Source.Where(x => x.ParentCode == CodeThu).Sum(x => x.SoTienDaTamUng);
                    HDTHU.SoTienThanhToan = Source.Where(x => x.ParentCode == CodeThu).Sum(x => x.SoTienThanhToan);
                    HDTHU.SanLuongThiCong = Source.Where(x => x.ParentCode == CodeThu).Sum(x => x.SanLuongThiCong);
                    HDTHU.SoTienThucNhan = Source.Where(x => x.ParentCode == CodeThu).Sum(x => x.SoTienThucNhan);
                }     
                if(Source.Where(x => x.ParentCode == CodeChi).Any())
                {
                    HDChi.GiaTriHopDong = Source.Where(x => x.ParentCode == CodeChi).Sum(x => x.GiaTriHopDong);
                    HDChi.SoTienDaTamUng = Source.Where(x => x.ParentCode == CodeChi).Sum(x => x.SoTienDaTamUng);
                    HDChi.SoTienThanhToan = Source.Where(x => x.ParentCode == CodeChi).Sum(x => x.SoTienThanhToan);
                    HDChi.SanLuongThiCong = Source.Where(x => x.ParentCode == CodeChi).Sum(x => x.SanLuongThiCong);
                    HDChi.SoTienThucNhan = Source.Where(x => x.ParentCode == CodeChi).Sum(x => x.SoTienThucNhan);
                }
                Source.Add(HDTHU);
                Source.Add(HDChi);
            }
            if (CTHDCon != null)
                Source.AddRange(CTHDCon);
            Tl_TongHopDong.DataSource = Source;
            Tl_TongHopDong.RefreshDataSource();
            Tl_TongHopDong.Refresh();
            Tl_TongHopDong.ExpandAll();
        }

        private void Tl_TongHopDong_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Black;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                TongHopHopDong HD = Tl_TongHopDong.GetRow(e.Node.Id) as TongHopHopDong;
                if(HD.TenHopDong=="HỢP ĐỒNG THU")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                    e.Appearance.ForeColor = Color.Blue;
            }
        }

        private void Tl_TongHopDong_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (long)0))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
            if (e.Column.FieldName == "ThoiGianBaoHanh"|| e.Column.FieldName == "ThoiGianBaoLanh"|| e.Column.FieldName == "ThemFile"
                || e.Column.FieldName == "NgayKetThuc" || e.Column.FieldName == "NgayBatDau")
            {
                if(e.Node.Level == 2)
                {

                    if (e.Column.FieldName == "ThemFile" || e.Column.FieldName == "NgayKetThuc" || e.Column.FieldName == "NgayBatDau")
                        return;
                    TongHopHopDong HD = Tl_TongHopDong.GetRow(e.Node.Id) as TongHopHopDong;
                    if(HD.ThoiGianBaoHanh==default)
                    {
                        e.Handled = true;
                        e.Appearance.FillRectangle(e.Cache, e.Bounds);
                    }    
                    else if(HD.ThoiGianBaoLanh == default)
                    {
                        e.Handled = true;
                        e.Appearance.FillRectangle(e.Cache, e.Bounds);
                    }
                }
                else
                {
                    if (e.Node.Level==3&&(e.Column.FieldName == "NgayKetThuc" || e.Column.FieldName == "NgayBatDau"))
                        return;
                    e.Handled = true;
                    e.Appearance.FillRectangle(e.Cache, e.Bounds);
                }
            }
        }

        private void rihp_File_Click(object sender, EventArgs e)
        {
            TongHopHopDong CTHD=Tl_TongHopDong.GetFocusedRow() as TongHopHopDong;
            //if (CTHD.ParentCode != "0")
            //    return;
            if (Tl_TongHopDong.FocusedNode.Level != 2)
                return;
            FormLuaChon luachon = new FormLuaChon(CTHD.Code, FileManageTypeEnum.TongHopDanhSachHopDong,CTHD.TenHopDong);
            luachon.ShowDialog();
        }

        private void Tl_TongHopDong_GetCustomSummaryValue(object sender, DevExpress.XtraTreeList.GetCustomSummaryValueEventArgs e)
        {
            
        }

        private void Tl_TongHopDong_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string Code = e.Node.GetValue("Code").ToString().Replace("_Dot", "");
            if (e.Column.FieldName== "SoTienThucNhan")
            {
                if(long.TryParse(e.Value.ToString(),out long TTN))
                {
                    TreeListNode Parent = e.Node.ParentNode;
                    TreeListNodes Child = Parent.Nodes;
                    long ThanhTien = Child.Where(x => x.GetValue("SoTienThucNhan") != null).Sum(x => long.Parse(x.GetValue("SoTienThucNhan").ToString()));
                    Tl_TongHopDong.SetRowCellValue(Parent, "SoTienThucNhan", ThanhTien);
                    string dbString = $"UPDATE {MyConstant.TBL_HopDong_DotHopDong} SET \"SoTienThucNhan\"='{e.Value}'" +
                   $"WHERE \"Code\"='{Code}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
                else
                {
                    MessageShower.ShowError("Vui lòng nhập định dạng số !!!!!!");
                    Tl_TongHopDong.CancelCurrentEdit();
                    return;
                }
            }
            else if (e.Column.FieldName == "TrangThai")
            {
                if(e.Value.ToString()=="Hoàn thành")
                {
                    DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có chắc hoàn thành trạng thái đợt thanh toán này không???? Nếu hoàn thành thì chỉ quyền Admin mới được sửa đổi lại!!!!!!");
                    if (rs == DialogResult.No)
                    {
                        Tl_TongHopDong.CancelCurrentEdit();
                        
                        return;
                    }
                }
                string dbString = $"UPDATE {MyConstant.TBL_HopDong_DotHopDong} SET \"TrangThai\"='{e.Value}'" +
   $"WHERE \"Code\"='{Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            }
        }

        private void Tl_TongHopDong_ShowingEditor(object sender, CancelEventArgs e)
        {
            TongHopHopDong CTHD = Tl_TongHopDong.GetFocusedRow() as TongHopHopDong;
            //if (!CTHD.Code.EndsWith("_Dot"))
            //    e.Cancel = true;
            //else
            //if (Tl_TongHopDong.FocusedColumn.FieldName != "TrangThai" && Tl_TongHopDong.FocusedColumn.FieldName != "SoTienThucNhan")
            //    e.Cancel = true;
            //else
            if(Tl_TongHopDong.FocusedColumn.FieldName == "TrangThai")
            {
                if (CTHD.TrangThai == "Hoàn thành")
                {
                    if (!BaseFrom.allPermission.HaveInitProjectPermission)
                    {
                        MessageShower.ShowError("Bạn không có quyền Admin để chỉnh sửa trạng thái này!!! Vui lòng chuyển tài khoản sang chế độ Admin!!!!");
                        e.Cancel = true;
                    }

                }
            }
        }

        private void Tl_TongHopDong_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            
        }
    }
}
