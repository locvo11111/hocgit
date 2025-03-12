using DevExpress.Utils;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.HopDong;
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
using static PhanMemQuanLyThiCong.Common.Helper.DuAnHelper;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_XuatBaoCaoHopDong : DevExpress.XtraEditors.XtraForm
    {
        static long HopDongThu = 0, Hopdongchi = 0, Thu = 0, Chi = 0, TamUngThu = 0, TamUngChi = 0;
        public Form_XuatBaoCaoHopDong()
        {
            InitializeComponent();
            dv_BaoCao.Hide();
        }

        public int Check
        {
            get
            {
                if (ce_ChiTiet.Checked)
                    return 0;
                else return 1;
            }
            set
            {
                if (value == 0) ce_ChiTiet.Checked = true;
                else ce_ChiTiet.Checked = false;
            }
        }
        private void sb_xuatbaocao_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhập dữ liệu hợp đồng", "Vui Lòng chờ!");
            string dbString = $"DELETE FROM Tbl_ChartTaiChinh";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            dbString = $"DELETE FROM Tbl_BaoCaoTaiChinhDuAn";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);      
            dbString = $"DELETE FROM Tbl_TenHopDong";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);       
            //dbString = $"DELETE FROM Tbl_LoaiHopDong";
            //DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);

            dbString = $"SELECT * FROM Tbl_BaoCaoTaiChinhDuAn";
            DataTable dt_NhaThau = DataProvider.InstanceBaoCao.ExecuteQuery(dbString);

            dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINNHATHAU} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_TenDuAn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string Code = Guid.NewGuid().ToString();
            DataRow newRow = dt_NhaThau.NewRow();
            newRow["Code"] = Code;
            newRow["Ten"] = dt_TenDuAn.Rows[0]["Ten"];
            dt_NhaThau.Rows.Add(newRow);
            DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dt_NhaThau, "Tbl_BaoCaoTaiChinhDuAn");

            Fcn_UpdateThuChi(Code);
            Fcn_UpdateDuAnChi(Code);
            Fcn_UpdateDuAnThu(Code);
            BaoCaoHopDong Baocao = new BaoCaoHopDong();
            Baocao.Fcn_LoadChart(dt_TenDuAn.Rows[0]["Ten"].ToString(), (DefaultBoolean)Check,HopDongThu,Hopdongchi,Thu,Chi,TamUngThu,TamUngChi);
            List<DonViThucHien> DVTH = DuAnHelper.GetDonViThucHiens();            
            List<BaoCaoHopDongTongHop> HD = new List<BaoCaoHopDongTongHop>();
            double LuyKe = 0;
            DataTable dt_TenHD;
            DataTable dt_CT;
            foreach (DonViThucHien item in DVTH)
            {
                dbString = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"CodeDonViThucHien\"='{item.Code}'";
                dt_TenHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dt_TenHD.Rows.Count == 0)
                    continue;
                dbString = $"SELECT {MyConstant.TBL_hopdongAB_HT}.LuyKeDenHetKyNay,{MyConstant.TBL_HopDong_PhuLuc}.DonGia " +
                    $"FROM {MyConstant.TBL_hopdongAB_HT} " +
                    $"INNER JOIN {MyConstant.TBL_HopDong_DoBoc} " +
                    $"ON {MyConstant.TBL_HopDong_DoBoc}.Code={MyConstant.TBL_hopdongAB_HT}.CodeDB " +
                    $"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                    $"ON {MyConstant.TBL_HopDong_DoBoc}.CodePL={MyConstant.TBL_HopDong_PhuLuc}.Code " +
                    $"INNER JOIN {MyConstant.TBL_ThongtinphulucHD} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodePl={MyConstant.TBL_ThongtinphulucHD}.Code " +
                    $"INNER JOIN {MyConstant.TBL_tonghopdanhsachhopdong} " +
                    $"ON {MyConstant.TBL_ThongtinphulucHD}.CodeHd={MyConstant.TBL_tonghopdanhsachhopdong}.Code " +
                    $"WHERE {MyConstant.TBL_tonghopdanhsachhopdong}.CodeHopDong='{dt_TenHD.Rows[0]["Code"]}'";
                dt_CT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                LuyKe = dt_CT.Rows.Count == 0 ? 0 : dt_CT.AsEnumerable().Where(x=> x["LuyKeDenHetKyNay"]!=DBNull.Value&&x["DonGia"] !=DBNull.Value).Sum(x => double.Parse(x["LuyKeDenHetKyNay"].ToString())*double.Parse(x["DonGia"].ToString()));
                if (item.Table == MyConstant.TBL_THONGTINNHATHAU||item.LoaiDVTH==MyConstant.LoaiDVTH_TuThucHien)
                {
                    HD.Add(new BaoCaoHopDongTongHop
                    {
                        Code = Guid.NewGuid().ToString(),
                        TenHopDong = dt_TenHD.Rows[0]["TenHopDong"].ToString(),
                        CodeLoaiHopDong = "1",
                        GiaTriHopDong=double.Parse(dt_TenHD.Rows[0]["GiaTriHopDong"].ToString()),
                        GiaTriThucHien= LuyKe,
                        TenNhaThau="Hợp đồng Nhà thầu chính",
                    }) ;

                }
                else 
                {
                    HD.Add(new BaoCaoHopDongTongHop
                    {
                        Code = Guid.NewGuid().ToString(),
                        TenHopDong = dt_TenHD.Rows[0]["TenHopDong"].ToString(),
                        CodeLoaiHopDong = "2",
                        GiaTriHopDong = double.Parse(dt_TenHD.Rows[0]["GiaTriHopDong"].ToString()),
                        GiaTriThucHien = LuyKe,
                        TenNhaThau = $"Hợp đồng {item.LoaiDVTH}"
                    });
                }              
            }
            dbString = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"CodeDonViThucHien\"=\"CodeNcc\"";
            dt_TenHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt_TenHD.Rows.Count != 0)
            {
                foreach(DataRow row in dt_TenHD.Rows)
                {
                    dbString = $"SELECT {MyConstant.TBL_hopdongNCC_TT}.KhoiLuongThanhToan as LuyKeDaThanhToan " +
                   $"FROM {MyConstant.TBL_hopdongNCC_TT} " +
                   $"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                   $"ON {MyConstant.TBL_hopdongNCC_TT}.CodePhuLuc={MyConstant.TBL_HopDong_PhuLuc}.Code " +
                   $"INNER JOIN {MyConstant.TBL_ThongtinphulucHD} " +
                   $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodePl={MyConstant.TBL_ThongtinphulucHD}.Code " +
                   $"INNER JOIN {MyConstant.TBL_tonghopdanhsachhopdong} " +
                   $"ON {MyConstant.TBL_ThongtinphulucHD}.CodeHd={MyConstant.TBL_tonghopdanhsachhopdong}.Code " +
                   $"WHERE {MyConstant.TBL_tonghopdanhsachhopdong}.CodeHopDong='{dt_TenHD.Rows[0]["Code"]}'";
                    dt_CT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    LuyKe = dt_CT.Rows.Count == 0 ? 0 : dt_CT.AsEnumerable().Where(x=>x["LuyKeDaThanhToan"]!=DBNull.Value).Sum(x => double.Parse(x["LuyKeDaThanhToan"].ToString()));
                    HD.Add(new BaoCaoHopDongTongHop
                    {
                        Code = Guid.NewGuid().ToString(),
                        TenHopDong = row["TenHopDong"].ToString(),
                        CodeLoaiHopDong = "2",
                        GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()),
                        GiaTriThucHien = LuyKe,
                        TenNhaThau = $"Hợp đồng nhà cung cấp"
                    });
                }
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtTongHop = converter.ToDataTableList<BaoCaoHopDongTongHop>(HD);
            DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dtTongHop, "Tbl_TenHopDong");
            dv_BaoCao.UseAsyncDocumentCreation = DefaultBoolean.True;
            dv_BaoCao.RequestDocumentCreation = true;
            dv_BaoCao.DocumentSource = Baocao;
            dv_BaoCao.InitiateDocumentCreation();
            dv_BaoCao.Visible = true;
            WaitFormHelper.CloseWaitForm();
            MessageShower.ShowInformation("Xuất báo cáo thành công!", "Thông tin");
        }
        private void Fcn_UpdateThuChi(string Code)
        {
            string db_String = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(db_String);
            if (dt.Rows.Count == 0)
                return;
            DateTime Max = dt.AsEnumerable().Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
            DateTime Min = dt.AsEnumerable().Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));

            string lstcodeHD = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            db_String = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GiaTriGiaiChi as Chi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.DateXacNhanDaChi as Date FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
$"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} " +
$"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CodeDeXuat " +
$"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.CodeHd IN ({lstcodeHD})";
            DataTable dt_kc = DataProvider.InstanceTHDA.ExecuteQuery(db_String);

            db_String = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NgayThangThucHien as Date,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.ThucTeThu as Thu  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
    $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} " +
    $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CodeDeXuat " +
    $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.CodeHd IN ({lstcodeHD})";
            DataTable dt_kt = DataProvider.InstanceTHDA.ExecuteQuery(db_String);

            List<Chart_KhoiLuongThanhTien> SourceThu = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kt);
            List<Chart_KhoiLuongThanhTien> SourceChi = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kc);
            List<Chart_KhoiLuongThanhTien> lsoutThu = new List<Chart_KhoiLuongThanhTien>();
            List<Chart_KhoiLuongThanhTien> lsoutChi = new List<Chart_KhoiLuongThanhTien>();
            var crDateThu = SourceThu.GroupBy(x => x.Date).OrderBy(x => x.Key);
            var crDateChi = SourceChi.GroupBy(x => x.Date).OrderBy(x => x.Key);
            long luykeThu = 0, luykeChi = 0;
            foreach (var item in crDateThu)
            {
                long ThanhTien = item.Sum(x => x.Thu);
                luykeThu += ThanhTien;
                lsoutThu.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    Thu = luykeThu
                });
            }
            foreach (var item in crDateChi)
            {
                long ThanhTien = item.Sum(x => x.Thu);
                luykeChi += ThanhTien;
                lsoutChi.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    Chi = luykeChi
                });
            }
            if (SourceThu.Count != 0)
            {
                if (crDateThu.Min(x => x.Key) != Min)
                {
                    lsoutThu.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = Min,
                        Thu = 0
                    });
                }
                if (crDateThu.Max(x => x.Key) != Max)
                {
                    lsoutThu.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = Max,
                        Thu = luykeThu
                    });
                }
            }
            if (SourceChi.Count() != 0)
            {
                if (crDateChi.Min(x => x.Key) != Min)
                {
                    lsoutChi.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = Min,
                        Chi = 0
                    });
                }
                if (crDateChi.Max(x => x.Key) != Max)
                {
                    lsoutChi.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = Max,
                        Chi = luykeChi

                    });
                }
            }
            Thu = lsoutThu.Count==0?0: lsoutThu.Max(x => x.Thu);
            Chi = lsoutChi.Count==0?0: lsoutChi.Max(x => x.Chi);
            lsoutThu.ForEach(x => x.ID = Code);
            lsoutThu.ForEach(x => x.Code = Guid.NewGuid().ToString());
            lsoutChi.ForEach(x => x.ID = Code);
            lsoutChi.ForEach(x => x.Code = Guid.NewGuid().ToString());
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtThu = converter.ToDataTableList<Chart_KhoiLuongThanhTien>(lsoutThu);
            DataTable dtTaiChi = converter.ToDataTableList<Chart_KhoiLuongThanhTien>(lsoutChi);
            foreach (DataRow row in dtThu.Rows)
                row["Date"] = DateTime.Parse(row["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);       
            foreach (DataRow row in dtTaiChi.Rows)
                row["Date"] = DateTime.Parse(row["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dtThu, "Tbl_ChartTaiChinh");
            DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dtTaiChi, "Tbl_ChartTaiChinh");
            //cc_DuAn.Series[2].ArgumentDataMember = "Date";
            //cc_DuAn.Series[2].ValueDataMembers.AddRange(new string[] { "Thu" });
            //cc_DuAn.Series[3].ArgumentDataMember = "Date";
            //cc_DuAn.Series[3].ValueDataMembers.AddRange(new string[] { "Chi" });
            //cc_DuAn.Series[2].DataSource = lsoutThu;
            //cc_DuAn.Series[3].DataSource = lsoutChi;
        }
        private void Fcn_UpdateDuAnThu(string Code)
        {
            string db_String = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE (\"CodeDonViThucHien\"=\"CodeNhaThau\" OR \"CodeDonViThucHien\"=\"CodeDuAn\") AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            //if (typehopdong == TypeThuChi.HopDongChi)
            //    db_String = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE (\"CodeDonViThucHien\"!=\"CodeNhaThau\" AND \"CodeDonViThucHien\"!=\"CodeDuAn\") AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(db_String);
            if (dt.Rows.Count == 0)
                return;
            DateTime MaxDuAn = dt.AsEnumerable().Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
            DateTime MinDuAn = dt.AsEnumerable().Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
            string lstcodeHD = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            db_String = $"SELECT * FROM {MyConstant.Tbl_CHITIETHOPDONG} WHERE \"CodeHopDong\" IN ({lstcodeHD})";
            DataTable dt_chitiet = DataProvider.InstanceTHDA.ExecuteQuery(db_String);

            List<DateTime> KeHoach = new List<DateTime>();
            Dictionary<DateTime, long> Thuchien = new Dictionary<DateTime, long>();
            Dictionary<string, List<Chart_KhoiLuongThanhTien>> TongHoplst = new Dictionary<string, List<Chart_KhoiLuongThanhTien>>();

            List<ChiTietHopDong> CTHD = DuAnHelper.ConvertToList<ChiTietHopDong>(dt_chitiet);
            List<ChiTietHopDong> CTHDNotChuKy = CTHD.FindAll(x => x.Loai == 0);
            List<ChiTietHopDong> CTHDTheoChuKy = CTHD.FindAll(x => x.Loai == 2);
            List<ChiTietHopDong> CTHDGiaiNgan = CTHD.FindAll(x => x.Loai == 1);
            List<Chart_KhoiLuongThanhTien> lsTong = new List<Chart_KhoiLuongThanhTien>();
            List<Chart_KhoiLuongThanhTien> lsTT = new List<Chart_KhoiLuongThanhTien>();
            List<Chart_KhoiLuongThanhTien> lsTamUng = new List<Chart_KhoiLuongThanhTien>();
            List<Chart_KhoiLuongThanhTien> lsTamUngout = new List<Chart_KhoiLuongThanhTien>();

            var grTheoHopDong = CTHDNotChuKy.GroupBy(x => x.CodeHopDong);
            long luyKeTTKeHoach = 0;
            foreach (DataRow row in dt.Rows)
            {
                List<Chart_KhoiLuongThanhTien> lstHD = new List<Chart_KhoiLuongThanhTien>();
                //List<Chart_KhoiLuongThanhTien> lstTamUng = new List<Chart_KhoiLuongThanhTien>();
                List<ChiTietHopDong> CTHDNotChuKycon = CTHDNotChuKy.FindAll(x => x.CodeHopDong == row["Code"].ToString());
                List<ChiTietHopDong> CTHDChuKycon = CTHDTheoChuKy.FindAll(x => x.CodeHopDong == row["Code"].ToString());
                List<ChiTietHopDong> CTHDGiaiNganCon = CTHDGiaiNgan.FindAll(x => x.CodeHopDong == row["Code"].ToString());
                CTHDNotChuKycon.ForEach(x => x.GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()));
                CTHDChuKycon.ForEach(x => x.GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()));
                CTHDGiaiNganCon.ForEach(x => x.GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()));
                bool check = false;
                long SoTienGiaiNgan = CTHDGiaiNganCon.Sum(x => x.SoTienCal);

                DateTime Min = DateTime.Parse(row["NgayBatDau"].ToString());
                DateTime Max = DateTime.Parse(row["NgayKetThuc"].ToString());
                Thuchien.Clear();
                db_String = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                    $"INNER JOIN {MyConstant.TBL_ThongtinphulucHD} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodePl={MyConstant.TBL_ThongtinphulucHD}.Code " +
                    $"INNER JOIN {MyConstant.TBL_tonghopdanhsachhopdong} " +
                    $"ON {MyConstant.TBL_ThongtinphulucHD}.CodeHd={MyConstant.TBL_tonghopdanhsachhopdong}.Code " +
                    $"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan={TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                    $" WHERE {MyConstant.TBL_tonghopdanhsachhopdong}.CodeHopDong='{row["Code"]}'";
                DataTable dt_CTTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(db_String);
                if (dt_CTTheoKy.Rows.Count == 0)
                    continue;
                string[] lsCodeCongTac = dt_CTTheoKy.AsEnumerable().Select(x =>x["CodeCongTacTheoGiaiDoan"].ToString()).ToArray();
                //db_String = $"SELECT *  FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" IN ({string.Join(", ", lsCodeCongTac)}) ";
                var lsSourcete = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac);

                List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();///list 

                var grsDate = lsSourcete.FindAll(x => x.Ngay <= Max && x.Ngay >= Min).GroupBy(x => x.Ngay).OrderBy(x => x.Key);
                List<DateTime> Point = new List<DateTime>();
                bool CheckPoint = false;
                int j = 1;
                //Tính lũy kế kế hoạch và ngày hoàn thành tiền giải ngân
                foreach (var item in grsDate)
                {
                    long ThanhTienKH =(long) item.Sum(x => x.ThanhTienKeHoach);
                    luyKeTTKeHoach += ThanhTienKH;
                    lsOut.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = item.Key,
                        LuyKeThanhTienKeHoach = luyKeTTKeHoach,
                        ThanhTienKeHoach = ThanhTienKH
                    });
                    if (SoTienGiaiNgan * j <= luyKeTTKeHoach && SoTienGiaiNgan != 0)
                    {
                        Point.Add(item.Key);
                        j++;
                        CheckPoint = true;
                    }


                }
                if (!lsOut.Any())
                    continue;
                //tính tiền tạm ứng và max min kế hoạch
                foreach (ChiTietHopDong item in CTHDNotChuKycon)
                {
                    if (Thuchien.ContainsKey(DateTime.Parse(item.Ngay)))
                    {
                        Thuchien[DateTime.Parse(item.Ngay)] = Thuchien[DateTime.Parse(item.Ngay)] + item.SoTienCal;
                    }
                    else
                        Thuchien.Add(DateTime.Parse(item.Ngay), item.SoTienCal);
                }
                DateTime MaxDic = Thuchien.Count() == 0 ? DateTime.Now : Thuchien.Keys.Max();
                DateTime MinDic = Thuchien.Count() == 0 ? DateTime.Now : Thuchien.Keys.Min();

                ///Max min ngày bắt đầu kết thúc hợp đồng
                DateTime MaxTong = MaxDic > Max ? MaxDic : Max;
                DateTime MinTong = Min > MinDic ? MinDic : Min;

                KeHoach.Clear();
                foreach (ChiTietHopDong item in CTHDChuKycon)
                {
                    if (!item.TheoThang)
                        KeHoach.Add(DateTime.Parse(item.Ngay));
                    else
                    {
                        int tongThang = (Max.Year - Min.Year) * 12 + Max.Month - Min.Month;
                        for (int i = 0; i <= tongThang; i++)
                        {
                            DateTime Now = Min.AddMonths(i);
                            DateTime Check = new DateTime(Now.Year, Now.Month, int.Parse(item.Ngay));
                            if (Check >= Min && Check <= Max)
                                KeHoach.Add(Check);
                        }
                    }
                }
                if (!KeHoach.Contains(MinTong))
                    KeHoach.Add(MinTong);
                if (!KeHoach.Contains(MaxTong))
                    KeHoach.Add(MaxTong);
                long ThanhTienLuyKe = 0;
                long subThanhTien = 0;
                int checkgiaingan = Point.Count();
                int k = 0;
                DateTime MaxKH = lsOut.Max(x => x.Date);
                DateTime MinKH = lsOut.Min(x => x.Date);
                foreach (DateTime item in KeHoach.OrderBy(x => x.Date))
                {
                    if (item >= MaxKH)
                        ThanhTienLuyKe = lsOut.FindAll(x => x.Date == MaxKH).FirstOrDefault().LuyKeThanhTienKeHoach;
                    else if (lsOut.FindAll(x => x.Date == item).FirstOrDefault() == null)
                    {
                        if (item < MinKH)
                            ThanhTienLuyKe = 0;
                        else
                        {
                            ThanhTienLuyKe = lsOut.FindAll(x => x.Date < item).Sum(x => x.ThanhTienKeHoach);
                        }
                    }

                    else
                        ThanhTienLuyKe = lsOut.FindAll(x => x.Date == item).FirstOrDefault().LuyKeThanhTienKeHoach;
                    lstHD.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = item,
                        ThanhTienHopDongThu = ThanhTienLuyKe,
                        CodeHopDong = row["Code"].ToString()
                    });
                    if (k >= Point.Count())
                        continue;
                    if (item >= Point[k])
                    {
                        if (Point[k] != item)
                        {
                            subThanhTien = lsOut.FindAll(x => x.Date <= item && x.Date > Point[k]).Sum(x => x.ThanhTienKeHoach);
                            lstHD.Add(new Chart_KhoiLuongThanhTien
                            {
                                Date = Point[k],
                                ThanhTienHopDongThu = ThanhTienLuyKe - subThanhTien,
                                CodeHopDong = row["Code"].ToString()
                            });
                        }
                        if (k + 1 <= Point.Count())
                        {
                            for (int m = k + 1; m < Point.Count(); m++)
                            {
                                if (item > Point[m])
                                {
                                    subThanhTien = lsOut.FindAll(x => x.Date <= item && x.Date > Point[m]).Sum(x => x.ThanhTienKeHoach);
                                    lstHD.Add(new Chart_KhoiLuongThanhTien
                                    {
                                        Date = Point[m],
                                        ThanhTienHopDongThu = ThanhTienLuyKe - subThanhTien,
                                        CodeHopDong = row["Code"].ToString()
                                    });
                                    k++;
                                }
                            }
                        }
                        check = true;
                        k++;
                    }
                }
                foreach (var item in Thuchien.OrderBy(x => x.Key.Date))
                {
                    lsTamUng.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = item.Key,
                        TamUngThu = item.Value,
                        CodeHopDong = row["Code"].ToString()
                    });
                }
                lsTong.AddRange(lstHD);
                TongHoplst.Add(row["Code"].ToString(), lstHD);
            }
            var crDateTong = lsTong.GroupBy(x => x.Date).OrderBy(x => x.Key);
            var crDateTamUng = lsTamUng.GroupBy(x => x.Date).OrderBy(x => x.Key);

            long LuykeTamUng = 0;

            foreach (var item in crDateTong)
            {
                long ThanhTien = lsTong.FindAll(x => x.Date == item.Key).Sum(x => x.ThanhTienHopDongThu);
                string[] codeHD = lsTong.FindAll(x => x.Date == item.Key).Select(x => x.CodeHopDong).Distinct().ToArray();
                List<Chart_KhoiLuongThanhTien> lstFirts = lsTong.FindAll(x => x.Date < item.Key && !codeHD.Contains(x.CodeHopDong));
                long ThanhTienPre = 0;
                var CrMax = lstFirts.GroupBy(x => x.CodeHopDong);
                foreach (var crow in CrMax)
                {
                    ThanhTienPre += crow.Max(x => x.ThanhTienHopDongThu);
                }
                //long ThanhTienPre= lsTong.FindAll(x => x.Date<item.Key&&!codeHD.Contains(x.CodeHopDong)).Sum(x => x.ThanhTienHopDongThu);
                lsTT.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    ThanhTienHopDongThu = ThanhTienPre + ThanhTien
                });

            }
            if (lsTT.Count() != 0)
            {
                if (crDateTong.Max(x => x.Key) != MaxDuAn)
                    lsTT.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = MaxDuAn,
                        ThanhTienHopDongThu = lsTT.Max(x => x.ThanhTienHopDongThu)
                    });
                if (crDateTong.Min(x => x.Key) != MinDuAn)
                    lsTT.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = MinDuAn,
                        ThanhTienHopDongThu = 0
                    });
            }
            foreach (var item in crDateTamUng)
            {
                long ThanhTien = item.Sum(x => x.TamUngThu);
                LuykeTamUng += ThanhTien;
                lsTamUngout.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    TamUngThu = LuykeTamUng
                });
            }
            if (lsTamUngout.Count() != 0)
            {
                if (crDateTamUng.Max(x => x.Key) != MaxDuAn)
                    lsTamUngout.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = MaxDuAn,
                        TamUngThu = LuykeTamUng
                    });
                if (crDateTamUng.Min(x => x.Key) != MinDuAn)
                    lsTamUngout.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = MinDuAn,
                        TamUngThu = 0
                    });
            }
            HopDongThu = lsTT.Count==0?0: lsTT.Max(x => x.ThanhTienHopDongThu);
            TamUngThu =lsTamUng.Count==0?0: lsTamUngout.Max(x => x.TamUngThu);
            lsTT.ForEach(x => x.ID = Code);
            lsTT.ForEach(x => x.Code = Guid.NewGuid().ToString());
            lsTamUngout.ForEach(x => x.ID = Code);
            lsTamUngout.ForEach(x => x.Code = Guid.NewGuid().ToString());
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtThu = converter.ToDataTableList<Chart_KhoiLuongThanhTien>(lsTT);
            DataTable dtTaiChi = converter.ToDataTableList<Chart_KhoiLuongThanhTien>(lsTamUngout);
            foreach (DataRow row in dtThu.Rows)
                row["Date"] = DateTime.Parse(row["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            foreach (DataRow row in dtTaiChi.Rows)
                row["Date"] = DateTime.Parse(row["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dtThu, "Tbl_ChartTaiChinh");
            DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dtTaiChi, "Tbl_ChartTaiChinh");
            //cc_DuAn.Series[(int)typehopdong].ArgumentDataMember = "Date";
            //cc_DuAn.Series[(int)typehopdong].ValueDataMembers.AddRange(new string[] { "ThanhTienHopDongThu" });
            //cc_DuAn.Series[(int)typetamung].ArgumentDataMember = "Date";
            //cc_DuAn.Series[(int)typetamung].ValueDataMembers.AddRange(new string[] { "Thu" });
            //cc_DuAn.Series[(int)typehopdong].DataSource = lsTT;
            //cc_DuAn.Series[(int)typetamung].DataSource = lsTamUngout;
        }     
        private void Fcn_UpdateDuAnChi(string Code)
        {
            //string db_String = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE (\"CodeDonViThucHien\"!=\"CodeNhaThau\" AND \"CodeDonViThucHien\"!=\"CodeDuAn\") AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";  
            //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(db_String);
            //if (dt.Rows.Count == 0)
            //    return;
            //DateTime MaxDuAn = dt.AsEnumerable().Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
            //DateTime MinDuAn = dt.AsEnumerable().Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
            //string lstcodeHD = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            //db_String = $"SELECT * FROM {MyConstant.Tbl_CHITIETHOPDONG} WHERE \"CodeHopDong\" IN ({lstcodeHD})";
            //DataTable dt_chitiet = DataProvider.InstanceTHDA.ExecuteQuery(db_String);

            //List<DateTime> KeHoach = new List<DateTime>();
            //Dictionary<DateTime, long> Thuchien = new Dictionary<DateTime, long>();
            //Dictionary<string, List<Chart_KhoiLuongThanhTien>> TongHoplst = new Dictionary<string, List<Chart_KhoiLuongThanhTien>>();

            //List<ChiTietHopDong> CTHD = DuAnHelper.ConvertToList<ChiTietHopDong>(dt_chitiet);
            //List<ChiTietHopDong> CTHDNotChuKy = CTHD.FindAll(x => x.Loai == 0);
            //List<ChiTietHopDong> CTHDTheoChuKy = CTHD.FindAll(x => x.Loai == 2);
            //List<ChiTietHopDong> CTHDGiaiNgan = CTHD.FindAll(x => x.Loai == 1);
            //List<Chart_KhoiLuongThanhTien> lsTong = new List<Chart_KhoiLuongThanhTien>();
            //List<Chart_KhoiLuongThanhTien> lsTT = new List<Chart_KhoiLuongThanhTien>();
            //List<Chart_KhoiLuongThanhTien> lsTamUng = new List<Chart_KhoiLuongThanhTien>();
            //List<Chart_KhoiLuongThanhTien> lsTamUngout = new List<Chart_KhoiLuongThanhTien>();

            //var grTheoHopDong = CTHDNotChuKy.GroupBy(x => x.CodeHopDong);
            //long luyKeTTKeHoach = 0;
            //foreach (DataRow row in dt.Rows)
            //{
            //    List<Chart_KhoiLuongThanhTien> lstHD = new List<Chart_KhoiLuongThanhTien>();
            //    //List<Chart_KhoiLuongThanhTien> lstTamUng = new List<Chart_KhoiLuongThanhTien>();
            //    List<ChiTietHopDong> CTHDNotChuKycon = CTHDNotChuKy.FindAll(x => x.CodeHopDong == row["Code"].ToString());
            //    List<ChiTietHopDong> CTHDChuKycon = CTHDTheoChuKy.FindAll(x => x.CodeHopDong == row["Code"].ToString());
            //    List<ChiTietHopDong> CTHDGiaiNganCon = CTHDGiaiNgan.FindAll(x => x.CodeHopDong == row["Code"].ToString());
            //    CTHDNotChuKycon.ForEach(x => x.GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()));
            //    CTHDChuKycon.ForEach(x => x.GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()));
            //    CTHDGiaiNganCon.ForEach(x => x.GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()));
            //    bool check = false;
            //    long SoTienGiaiNgan = CTHDGiaiNganCon.Sum(x => x.SoTienCal);

            //    DateTime Min = DateTime.Parse(row["NgayBatDau"].ToString());
            //    DateTime Max = DateTime.Parse(row["NgayKetThuc"].ToString());
            //    Thuchien.Clear();
            //    db_String = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
            //        $"INNER JOIN {MyConstant.TBL_ThongtinphulucHD} " +
            //        $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodePl={MyConstant.TBL_ThongtinphulucHD}.Code " +
            //        $"INNER JOIN {MyConstant.TBL_tonghopdanhsachhopdong} " +
            //        $"ON {MyConstant.TBL_ThongtinphulucHD}.CodeHd={MyConstant.TBL_tonghopdanhsachhopdong}.Code " +
            //        $"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
            //        $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan={TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
            //        $" WHERE {MyConstant.TBL_tonghopdanhsachhopdong}.CodeHopDong='{row["Code"]}'";
            //    DataTable dt_CTTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(db_String);
            //    if (dt_CTTheoKy.Rows.Count == 0)
            //        continue;
            //    string[] lsCodeCongTac = dt_CTTheoKy.AsEnumerable().Select(x => x["CodeCongTacTheoGiaiDoan"].ToString()).ToArray();
            //    //db_String = $"SELECT *  FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" IN ({string.Join(", ", lsCodeCongTac)}) ";
            //    DataTable dtHangNgay = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac);
            //    //DataTable dtHangNgay = MyFunction.Fcn_CalKLKH(dt_CTTheoKy, "CodeCongTacTheoGiaiDoan");
            //    dtHangNgay.Columns["Ngay"].ColumnName = "Date";
            //    List<Chart_KhoiLuongThanhTien> lsSourcete = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dtHangNgay);
            //    List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();///list 

            //    var grsDate = lsSourcete.FindAll(x => x.Date <= Max && x.Date >= Min).GroupBy(x => x.Date).OrderBy(x => x.Key);
            //    if (grsDate.Count() == 0)
            //        continue;
            //    List<DateTime> Point = new List<DateTime>();
            //    //bool CheckPoint = false;
            //    int j = 1;
            //    ////Tính lũy kế kế hoạch và ngày hoàn thành tiền giải ngân
            //    foreach (var item in grsDate)
            //    {
            //        long ThanhTienKH = item.Sum(x => x.ThanhTienKeHoach);
            //        luyKeTTKeHoach += ThanhTienKH;
            //        lsOut.Add(new Chart_KhoiLuongThanhTien
            //        {
            //            Date = item.Key,
            //            LuyKeThanhTienKeHoach = luyKeTTKeHoach,
            //            ThanhTienHopDongChi = ThanhTienKH
            //        });
            //        if (SoTienGiaiNgan * j <= luyKeTTKeHoach && SoTienGiaiNgan != 0)
            //        {
            //            Point.Add(item.Key);
            //            j++;
            //            //CheckPoint = true;
            //        }


            //    }
            //    if (!lsOut.Any())
            //        continue;
            //    ////////tính tiền tạm ứng và max min kế hoạch
            //    foreach (ChiTietHopDong item in CTHDNotChuKycon)
            //    {
            //        if (Thuchien.ContainsKey(DateTime.Parse(item.Ngay)))
            //        {
            //            Thuchien[DateTime.Parse(item.Ngay)] = Thuchien[DateTime.Parse(item.Ngay)] + item.SoTienCal;
            //        }
            //        else
            //            Thuchien.Add(DateTime.Parse(item.Ngay), item.SoTienCal);
            //    }
            //    DateTime MaxDic = Thuchien.Count() == 0 ? DateTime.Now : Thuchien.Keys.Max();
            //    DateTime MinDic = Thuchien.Count() == 0 ? DateTime.Now : Thuchien.Keys.Min();

            //    ///Max min ngày bắt đầu kết thúc hợp đồng
            //    DateTime MaxTong = MaxDic > Max ? MaxDic : Max;
            //    DateTime MinTong = Min > MinDic ? MinDic : Min;

            //    KeHoach.Clear();
            //    foreach (ChiTietHopDong item in CTHDChuKycon)
            //    {
            //        if (!item.TheoThang)
            //            KeHoach.Add(DateTime.Parse(item.Ngay));
            //        else
            //        {
            //            int tongThang = (Max.Year - Min.Year) * 12 + Max.Month - Min.Month;
            //            for (int i = 0; i <= tongThang; i++)
            //            {
            //                DateTime Now = Min.AddMonths(i);
            //                DateTime Check = new DateTime(Now.Year, Now.Month, int.Parse(item.Ngay));
            //                if (Check >= Min && Check <= Max)
            //                    KeHoach.Add(Check);
            //            }
            //        }
            //    }
            //    if (!KeHoach.Contains(MinTong))
            //        KeHoach.Add(MinTong);
            //    if (!KeHoach.Contains(MaxTong))
            //        KeHoach.Add(MaxTong);
            //    long ThanhTienLuyKe = 0;
            //    long subThanhTien = 0;
            //    //int checkgiaingan = Point.Count();
            //    int k = 0;
            //    DateTime MaxKH = lsOut.Max(x => x.Date); 
            //    DateTime MinKH = lsOut.Min(x => x.Date);
            //    foreach (DateTime item in KeHoach.OrderBy(x => x.Date))
            //    {
            //        if (item >= MaxKH)
            //            ThanhTienLuyKe = lsOut.FindAll(x => x.Date == MaxKH).FirstOrDefault().LuyKeThanhTienKeHoach;
            //        else if (lsOut.FindAll(x => x.Date == item).FirstOrDefault() == null)
            //        {
            //            if (item < MinKH)
            //                ThanhTienLuyKe = 0;
            //            else
            //            {
            //                ThanhTienLuyKe = lsOut.FindAll(x => x.Date < item).Sum(x => x.ThanhTienKeHoach);
            //            }
            //        }
            //        else
            //            ThanhTienLuyKe = lsOut.FindAll(x => x.Date == item).FirstOrDefault().LuyKeThanhTienKeHoach;
            //        lstHD.Add(new Chart_KhoiLuongThanhTien
            //        {
            //            Date = item,
            //            ThanhTienHopDongChi = ThanhTienLuyKe,
            //            CodeHopDong = row["Code"].ToString()
            //        });
            //        if (k >= Point.Count())
            //            continue;
            //        if (item >= Point[k] /*&&k<= Point.Count()*/)
            //        {
            //            if (Point[k] != item)
            //            {
            //                subThanhTien = lsOut.FindAll(x => x.Date <= item && x.Date > Point[k]).Sum(x => x.ThanhTienKeHoach);
            //                lstHD.Add(new Chart_KhoiLuongThanhTien
            //                {
            //                    Date = Point[k],
            //                    ThanhTienHopDongChi = ThanhTienLuyKe - subThanhTien,
            //                    CodeHopDong = row["Code"].ToString()
            //                });
            //            }
            //            if (k + 1 <= Point.Count())
            //            {
            //                for (int m = k + 1; m < Point.Count(); m++)
            //                {
            //                    if (item > Point[m])
            //                    {
            //                        subThanhTien = lsOut.FindAll(x => x.Date <= item && x.Date > Point[m]).Sum(x => x.ThanhTienKeHoach);
            //                        lstHD.Add(new Chart_KhoiLuongThanhTien
            //                        {
            //                            Date = Point[m],
            //                            ThanhTienHopDongChi = ThanhTienLuyKe - subThanhTien,
            //                            CodeHopDong = row["Code"].ToString()
            //                        });
            //                        k++;
            //                    }
            //                }
            //            }
            //            //check = true;
            //            k++;
            //        }
            //    }
            //    foreach (var item in Thuchien.OrderBy(x => x.Key.Date))
            //    {
            //        lsTamUng.Add(new Chart_KhoiLuongThanhTien
            //        {
            //            Date = item.Key,
            //            TamUngChi = item.Value,
            //            CodeHopDong = row["Code"].ToString()
            //        });
            //    }
            //    lsTong.AddRange(lstHD);
            //    TongHoplst.Add(row["Code"].ToString(), lstHD);
            //}
            //var crDateTong = lsTong.GroupBy(x => x.Date).OrderBy(x => x.Key);
            //var crDateTamUng = lsTamUng.GroupBy(x => x.Date).OrderBy(x => x.Key);

            //long LuykeTamUng = 0;

            ////List<Chart_KhoiLuongThanhTien> lstFirts = TongHoplst.FirstOrDefault().Value;
            //foreach (var item in crDateTong)
            //{
            //    long ThanhTien = lsTong.FindAll(x => x.Date == item.Key).Sum(x => x.ThanhTienHopDongChi);
            //    Chart_KhoiLuongThanhTien lstFirts = lsTong.FindAll(x => x.Date == item.Key).FirstOrDefault();
            //    long ThanhTienPre = lsTong.FindAll(x => x.Date < item.Key && x.CodeHopDong != lstFirts.CodeHopDong).Sum(x => x.ThanhTienHopDongChi);
            //    lsTT.Add(new Chart_KhoiLuongThanhTien
            //    {
            //        Date = item.Key,
            //        ThanhTienHopDongChi = ThanhTienPre + ThanhTien
            //    });

            //}
            //if (lsTT.Count() != 0)
            //{
            //    if (crDateTong.Max(x => x.Key) != MaxDuAn)
            //        lsTT.Add(new Chart_KhoiLuongThanhTien
            //        {
            //            Date = MaxDuAn,
            //            ThanhTienHopDongChi = lsTT.Max(x => x.ThanhTienHopDongChi)
            //        });
            //    if (crDateTong.Min(x => x.Key) != MinDuAn)
            //        lsTT.Add(new Chart_KhoiLuongThanhTien
            //        {
            //            Date = MinDuAn,
            //            ThanhTienHopDongChi = 0
            //        });
            //}
            //foreach (var item in crDateTamUng)
            //{
            //    long ThanhTien = item.Sum(x => x.TamUngChi);
            //    LuykeTamUng += ThanhTien;
            //    lsTamUngout.Add(new Chart_KhoiLuongThanhTien
            //    {
            //        Date = item.Key,
            //        TamUngChi = LuykeTamUng
            //    });
            //}
            //if (lsTamUngout.Count() != 0)
            //{
            //    if (crDateTamUng.Max(x => x.Key) != MaxDuAn)
            //        lsTamUngout.Add(new Chart_KhoiLuongThanhTien
            //        {
            //            Date = MaxDuAn,
            //            TamUngChi = LuykeTamUng
            //        });
            //    if (crDateTamUng.Min(x => x.Key) != MinDuAn)
            //        lsTamUngout.Add(new Chart_KhoiLuongThanhTien
            //        {
            //            Date = MinDuAn,
            //            TamUngChi = 0
            //        });
            //}
            //Hopdongchi = lsTT.Count == 0 ? 0 : lsTT.Max(x => x.ThanhTienHopDongChi);
            //TamUngChi = lsTamUng.Count == 0 ? 0 : lsTamUngout.Max(x => x.TamUngChi);
            //lsTT.ForEach(x => x.ID = Code);
            //lsTT.ForEach(x => x.Code = Guid.NewGuid().ToString());
            //lsTamUngout.ForEach(x => x.ID = Code);
            //lsTamUngout.ForEach(x => x.Code = Guid.NewGuid().ToString());
            //ListtoDataTableConverter converter = new ListtoDataTableConverter();
            //DataTable dtThu = converter.ToDataTableList<Chart_KhoiLuongThanhTien>(lsTT);
            //DataTable dtTaiChi = converter.ToDataTableList<Chart_KhoiLuongThanhTien>(lsTamUngout);
            //foreach (DataRow row in dtThu.Rows)
            //    row["Date"] = DateTime.Parse(row["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            //foreach (DataRow row in dtTaiChi.Rows)
            //    row["Date"] = DateTime.Parse(row["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            //DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dtThu, "Tbl_ChartTaiChinh");
            //DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dtTaiChi, "Tbl_ChartTaiChinh");
            ////cc_DuAn.Series[(int)typehopdong].ArgumentDataMember = "Date";
            ////cc_DuAn.Series[(int)typehopdong].ValueDataMembers.AddRange(new string[] { "ThanhTienHopDongThu" });
            ////cc_DuAn.Series[(int)typetamung].ArgumentDataMember = "Date";
            ////cc_DuAn.Series[(int)typetamung].ValueDataMembers.AddRange(new string[] { "Thu" });
            ////cc_DuAn.Series[(int)typehopdong].DataSource = lsTT;
            ////cc_DuAn.Series[(int)typetamung].DataSource = lsTamUngout;
        }
    }
}