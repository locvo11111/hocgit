using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGantt;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.HopDong;
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

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Ctrl_BaoCaoDongTienHopDong : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_BaoCaoDongTienHopDong()
        {
            InitializeComponent();
        }
        public void LoadDongTien()
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Toàn dự án, Vui lòng chờ!");
            cc_DuAn.Legend.Visibility = (DefaultBoolean)Check;
            Fcn_UpdateDuAn(TypeThuChi.HopDongThu,TypeThuChi.TamUngThu);
            Fcn_UpdateDuAn(TypeThuChi.HopDongChi,TypeThuChi.TamUngChi);
            Fcn_UpdateThuChi();
            Fcn_UpdateHD();
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_UpdateThuChi()
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
            foreach(var item in crDateThu)
            {
                long ThanhTien = item.Sum(x => x.Thu);
                luykeThu += ThanhTien;
                lsoutThu.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    Thu = luykeThu
                });
            }     
            foreach(var item in crDateChi)
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
                if(crDateThu.Max(x => x.Key) != Max)
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
            cc_DuAn.Series[2].DataSource = lsoutThu;
            cc_DuAn.Series[3].DataSource = lsoutChi;
        }
        private void Fcn_UpdateDuAn(TypeThuChi typehopdong, TypeThuChi typetamung)
        {
            string db_String = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE (\"CodeDonViThucHien\"=\"CodeNhaThau\" OR \"CodeDonViThucHien\"=\"CodeDuAn\") AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            if (typehopdong == TypeThuChi.HopDongChi)
                db_String = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE (\"CodeDonViThucHien\"!=\"CodeNhaThau\" AND \"CodeDonViThucHien\"!=\"CodeDuAn\") AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(db_String);
            if (dt.Rows.Count == 0)
            {
                cc_DuAn.Series[(int)typehopdong].DataSource = null;
                cc_DuAn.Series[(int)typetamung].DataSource = null;
                return;
            }

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
                List<ChiTietHopDong> CTHDNotChuKycon = CTHDNotChuKy.FindAll(x => x.CodeHopDong == row["Code"].ToString());
                List<ChiTietHopDong> CTHDChuKycon = CTHDTheoChuKy.FindAll(x => x.CodeHopDong == row["Code"].ToString());
                List<ChiTietHopDong> CTHDGiaiNganCon = CTHDGiaiNgan.FindAll(x => x.CodeHopDong == row["Code"].ToString());
                CTHDNotChuKycon.ForEach(x => x.GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()));
                CTHDChuKycon.ForEach(x => x.GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()));
                CTHDGiaiNganCon.ForEach(x => x.GiaTriHopDong = double.Parse(row["GiaTriHopDong"].ToString()));
                bool check = false;
                long SoTienGiaiNgan = CTHDGiaiNganCon.Sum(x => x.SoTienCal);
                List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();///list 
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
                DateTime CheckDate = DateTime.Now;
                if (dt_CTTheoKy.Rows.Count == 0)
                {
                    foreach (ChiTietHopDong item in CTHDNotChuKycon)
                    {
                        if (Thuchien.ContainsKey(DateTime.Parse(item.Ngay)))
                        {
                            Thuchien[DateTime.Parse(item.Ngay)] = Thuchien[DateTime.Parse(item.Ngay)] + item.SoTienCal;
                        }
                        else
                            Thuchien.Add(DateTime.Parse(item.Ngay), item.SoTienCal);
                    }
                    DateTime MaxDicNew = Thuchien.Count() == 0 ? DateTime.Now : Thuchien.Keys.Max();
                    DateTime MinDicNew = Thuchien.Count() == 0 ? DateTime.Now : Thuchien.Keys.Min();
                    ///Max min ngày bắt đầu kết thúc hợp đồng
                    DateTime MaxTongNew = MaxDicNew > Max ? MaxDicNew : Max;
                    DateTime MinTongNew = Min > MinDicNew ? MinDicNew : Min;
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
                                CheckDate = new DateTime(Now.Year, Now.Month, int.Parse(item.Ngay));
                                if (CheckDate >= Min && CheckDate <= Max)
                                    KeHoach.Add(CheckDate);
                            }
                        }
                    }
                    if (!KeHoach.Contains(MinTongNew))
                        KeHoach.Add(MinTongNew);
                    if (!KeHoach.Contains(MaxTongNew))
                        KeHoach.Add(MaxTongNew);
                    long ThanhTienLuyKeNew = 0;
             
                    DateTime MaxKHNew = lsOut.Count()==0?DateTime.Now:lsOut.Max(x => x.Date);
                    DateTime MinKHNew = lsOut.Count() == 0 ? DateTime.Now : lsOut.Min(x => x.Date);
                    foreach (DateTime item in KeHoach.OrderBy(x => x.Date))
                    {
                        if (lsOut.Count() == 0)
                        {
                            ThanhTienLuyKeNew = long.Parse(row["GiaTriHopDong"].ToString());
                            goto Label;
                        }
                        if (item >= MaxKHNew)
                            ThanhTienLuyKeNew = lsOut.FindAll(x => x.Date == MaxKHNew).FirstOrDefault().LuyKeThanhTienKeHoach;
                        else if (lsOut.FindAll(x => x.Date == item).FirstOrDefault() == null)
                        {
                            if (item < MinKHNew)
                                ThanhTienLuyKeNew = 0;
                            else
                            {
                                ThanhTienLuyKeNew = lsOut.FindAll(x => x.Date < item).Sum(x => x.ThanhTienKeHoach);
                            }
                        }

                        else
                            ThanhTienLuyKeNew = lsOut.FindAll(x => x.Date == item).FirstOrDefault().LuyKeThanhTienKeHoach;
                        Label:
                        lstHD.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = item,
                            ThanhTienHopDongThu = ThanhTienLuyKeNew,
                            CodeHopDong = row["Code"].ToString()
                        });
                    }
                    foreach (var item in Thuchien.OrderBy(x => x.Key.Date))
                    {
                        lsTamUng.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = item.Key,
                            Thu = item.Value,
                            CodeHopDong = row["Code"].ToString()
                        });
                    }
                    lsTong.AddRange(lstHD);
                    TongHoplst.Add(row["Code"].ToString(), lstHD);
                    continue;
                }

                string[] lstcode = dt_CTTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                var lsSourcete = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lstcode, Min, Max);


                var grsDate = lsSourcete.GroupBy(x => x.Ngay).OrderBy(x => x.Key);
                if (grsDate.Count() == 0)
                    continue;
                List<DateTime> Point = new List<DateTime>();
                //bool CheckPoint = false;
                int j = 1;
                ////Tính lũy kế kế hoạch và ngày hoàn thành tiền giải ngân
                foreach (var item in grsDate)
                {
                    long ThanhTienKH = (long)item.Sum(x => x.ThanhTienKeHoach);
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
                    }


                }
                if (!lsOut.Any())
                    continue;
                ////////tính tiền tạm ứng và max min kế hoạch
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
                            DateTime CheckTonTai = new DateTime(Now.Year, Now.Month,1);
                            int daysinfeb2 = DateTime.DaysInMonth(CheckTonTai.Year, CheckTonTai.Month);
                            if(int.Parse(item.Ngay) <= daysinfeb2)
                                CheckDate = new DateTime(Now.Year, Now.Month, int.Parse(item.Ngay));
                            else
                            {

                                CheckDate = new DateTime(Now.Year, Now.Month,daysinfeb2);
                            }

                            if (CheckDate >= Min && CheckDate <= Max)
                                KeHoach.Add(CheckDate);
                        }
                    }
                }
                if (!KeHoach.Contains(MinTong))
                    KeHoach.Add(MinTong);
                if (!KeHoach.Contains(MaxTong))
                    KeHoach.Add(MaxTong);
                long ThanhTienLuyKe = 0;
                long subThanhTien = 0;
                //int checkgiaingan = Point.Count();
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
                    if (item >= Point[k] /*&&k<= Point.Count()*/)
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
                        k++;
                    }
                }
                foreach (var item in Thuchien.OrderBy(x => x.Key.Date))
                {
                    lsTamUng.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = item.Key,
                        Thu = item.Value,
                        CodeHopDong = row["Code"].ToString()
                    });
                }
                lsTong.AddRange(lstHD);
                TongHoplst.Add(row["Code"].ToString(), lstHD);
            }
            var crDateTong = lsTong.GroupBy(x => x.Date).OrderBy(x => x.Key);
            var crDateTamUng = lsTamUng.GroupBy(x => x.Date).OrderBy(x => x.Key);

            long LuykeTamUng = 0;

            //List<Chart_KhoiLuongThanhTien> lstFirts = TongHoplst.FirstOrDefault().Value;
            foreach (var item in crDateTong)
            {
                long ThanhTien = lsTong.FindAll(x => x.Date == item.Key).Sum(x => x.ThanhTienHopDongThu);
                //Chart_KhoiLuongThanhTien lstFirts = lsTong.FindAll(x => x.Date == item.Key).FirstOrDefault();
                Chart_KhoiLuongThanhTien lstFirts = item.FirstOrDefault();
                long ThanhTienPre = lsTong.FindAll(x => x.Date < item.Key ).Sum(x => x.ThanhTienHopDongThu);
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
                long ThanhTien = item.Sum(x => x.Thu);
                LuykeTamUng += ThanhTien;
                lsTamUngout.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    Thu = LuykeTamUng
                });
            }
            if (lsTamUngout.Count() != 0)
            {
                if (crDateTamUng.Max(x => x.Key) != MaxDuAn)
                    lsTamUngout.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = MaxDuAn,
                        Thu = LuykeTamUng
                    });
                if (crDateTamUng.Min(x => x.Key) != MinDuAn)
                    lsTamUngout.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = MinDuAn,
                        Thu = 0
                    });
            }
            cc_DuAn.Series[(int)typehopdong].DataSource = lsTT;
            cc_DuAn.Series[(int)typetamung].DataSource = lsTamUngout;
        }
        private void cbb_ChonCuaSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:

                    scc_Main23.PanelVisibility = SplitPanelVisibility.Both;
                    break;
                case 1:
                    scc_Main23.PanelVisibility = SplitPanelVisibility.Panel1;

                    break;
                case 2:
                    scc_Main23.PanelVisibility = SplitPanelVisibility.Panel2;

                    break;
                default:
                    break;
            }
        }
        private void Fcn_UpdateHD()
        {
            string dbString = $"SELECT HD.NgayBatDau,HD.NgayKetThuc,PLHD.*,HD.*,HD.Code as CodeHopDong  " +
                $"FROM Tbl_HopDong_PhuLucHD PLHD " +
               $" LEFT JOIN Tbl_HopDong_ThongtinphulucHD TenPL ON PLHD.CodePl=TenPL.Code " +
               $" LEFT JOIN Tbl_HopDong_TongHop ON Tbl_HopDong_TongHop.Code=TenPL.CodeHd  " +          
               $" LEFT JOIN {MyConstant.TBL_TaoHopDongMoi} HD ON HD.Code=Tbl_HopDong_TongHop.CodeHopDong  " +
               $" LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt ON nt.Code=HD.CodeNhaThau " +
               $" LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp ON ntp.Code=HD.CodeNhaThauPhu " +
               $" LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} td ON td.Code=HD.CodeToDoi " +
               $" LEFT JOIN {MyConstant.TBL_THONGTINNHACUNGCAP} ncc ON ncc.Code=HD.CodeNcc " +
               $"WHERE HD.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_ChiTiet = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<DonViThucHien> DVTH = DuAnHelper.GetDonViThucHiens(NhaCungCap:true);
            double KLHD = 0, KLKyNay = 0;
            List<BarChartViewModel> source = new List<BarChartViewModel>();
            foreach (var item in DVTH.OrderBy(x => x.IsGiaoThau))
            {
 
                if (item.Table == MyConstant.TBL_THONGTINNHACUNGCAP)
                {
                    DataRow[] NewRow = dt_ChiTiet.AsEnumerable().Where(x => x["CodeNcc"].ToString() == item.Code).ToArray();
                    if (!NewRow.Any())
                        continue;
                    source.Add(new BarChartViewModel()
                    {
                        TenCongViec = item.Ten.ToUpper(),
                        ParentUID = "0",
                        UID = item.Code,
                        TaskType = 0

                    });
                    var HD = NewRow.GroupBy(x => x["CodeHopDong"].ToString());
                    foreach (var CrowHD in HD)
                    {
                        KLHD = CrowHD.Sum(x => double.Parse(x["KhoiLuong"].ToString()));
                        dbString = $"SELECT {MyConstant.TBL_hopdongNCC_TT}.* FROM {MyConstant.TBL_hopdongNCC_TT} " +
                            $" LEFT JOIN Tbl_HopDong_PhuLucHD PLHD ON {MyConstant.TBL_hopdongNCC_TT}.CodePhuLuc=PLHD.Code " +
                             $" LEFT JOIN Tbl_HopDong_ThongtinphulucHD TenPL ON PLHD.CodePl=TenPL.Code " +
                             $" LEFT JOIN Tbl_HopDong_TongHop ON Tbl_HopDong_TongHop.Code=TenPL.CodeHd WHERE Tbl_HopDong_TongHop.CodeHopDong='{CrowHD.Key}'";

                        DataTable dt_ChiTietDot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        KLKyNay = dt_ChiTietDot.AsEnumerable().Where(x => x["ThucHienKyNay"] != DBNull.Value).Sum(x => double.Parse(x["ThucHienKyNay"].ToString()));
                        double TyLe = KLHD==0?0: Math.Round(KLKyNay / KLHD);
                        source.Add(new BarChartViewModel()
                        {
                            TenCongViec = $"{CrowHD.FirstOrDefault()["TenHopDong"]}_{CrowHD.FirstOrDefault()["SoHopDong"]}",
                            Progress = TyLe * 100,
                            ParentUID = item.Code,
                            UID = Guid.NewGuid().ToString(),
                            NgayBatDauThiCong = DateTime.Parse(CrowHD.FirstOrDefault()["NgayBatDau"].ToString()).Date.AddHours(1),
                            NgayKetThucThiCong = DateTime.Parse(CrowHD.FirstOrDefault()["NgayKetThuc"].ToString()).Date.AddHours(23),
                            TaskType = 1,
                            Description = $"{TyLe * 100}%"
                        });

                    }
                }
                else
                {
                    DataRow[] NewRow = dt_ChiTiet.AsEnumerable().Where(x => x[item.ColCodeFK].ToString() == item.Code).ToArray();
                    if (!NewRow.Any())
                        continue;
                    source.Add(new BarChartViewModel()
                    {
                        TenCongViec = item.Ten.ToUpper(),
                        ParentUID = "0",
                        UID = item.Code,
                        TaskType = 0

                    });
                    var HD = NewRow.GroupBy(x => x["CodeHopDong"].ToString());
                    foreach(var CrowHD in HD)
                    {
                        KLHD = CrowHD.Sum(x => double.Parse(x["KhoiLuong"].ToString()));
                        dbString = $"SELECT {MyConstant.TBL_hopdongAB_HT}.* FROM {MyConstant.TBL_hopdongAB_HT} " +
                            $"LEFT JOIN {MyConstant.TBL_HopDong_DoBoc} ON {MyConstant.TBL_HopDong_DoBoc}.Code={MyConstant.TBL_hopdongAB_HT}.CodeDB" +
                            $" LEFT JOIN Tbl_HopDong_PhuLucHD PLHD ON  {MyConstant.TBL_HopDong_DoBoc}.CodePL=PLHD.Code " +
                             $" LEFT JOIN Tbl_HopDong_ThongtinphulucHD TenPL ON PLHD.CodePl=TenPL.Code " +
                             $" LEFT JOIN Tbl_HopDong_TongHop ON Tbl_HopDong_TongHop.Code=TenPL.CodeHd WHERE Tbl_HopDong_TongHop.CodeHopDong='{CrowHD.Key}'";

                        DataTable dt_ChiTietDot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        KLKyNay = dt_ChiTietDot.AsEnumerable().Where(x=>x["ThucHienKyNay"]!=DBNull.Value).Sum(x => double.Parse(x["ThucHienKyNay"].ToString()));
                        double TyLe = KLHD == 0 ? 0 : Math.Round(KLKyNay / KLHD,2);
                        source.Add(new BarChartViewModel()
                        {
                            TenCongViec = $"{CrowHD.FirstOrDefault()["TenHopDong"]}_{CrowHD.FirstOrDefault()["SoHopDong"]}",
                            Progress = TyLe * 100,
                            ParentUID = item.Code,
                            UID = Guid.NewGuid().ToString(),
                            NgayBatDauThiCong = DateTime.Parse(CrowHD.FirstOrDefault()["NgayBatDau"].ToString()).Date.AddHours(1),
                            NgayKetThucThiCong = DateTime.Parse(CrowHD.FirstOrDefault()["NgayKetThuc"].ToString()).Date.AddHours(23),
                            TaskType = 1,
                            Description = $"{TyLe * 100}%"
                        });

                    }


                }

            }
            for (int i = 0; i >= 0; i--)
            {
                source.Where(x => x.TaskType == i).ForEach(y =>
                {
                    y.NgayBatDauThiCong = source.Where(z => z.ParentUID == y.UID).Min(t => t.NgayBatDauThiCong);
                    y.NgayKetThucThiCong = source.Where(z => z.ParentUID == y.UID).Max(t => t.NgayKetThucThiCong);
                    y.Progress = Math.Round(source.Where(z => z.ParentUID == y.UID).Sum(t => t.Progress) / (source.Where(z => z.ParentUID == y.UID).Count()));
                });
            }
            gc_HopDong.DataSource = source;
            gc_HopDong.RefreshDataSource();
            gc_HopDong.Refresh();
            gc_HopDong.ExpandAll();

        }
        private void scc_Main23_SizeChanged(object sender, EventArgs e)
        {
            scc_Main23.SplitterPosition = scc_Main23.Height / 2;
        }

        private void sb_CapNhap_Click(object sender, EventArgs e)
        {

        }
        public event EventHandler sb_capNhap_Click
        {
            add
            {
                sb_CapNhap.Click += value;
            }
            remove
            {
                sb_CapNhap.Click -= value;
            }
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

        private void gc_HopDong_CustomDrawTask(object sender, DevExpress.XtraGantt.CustomDrawTaskEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.BackColor = Color.Green;
                e.Appearance.ProgressColor = Color.Green;

            }
            else
            {
                e.Appearance.BackColor = Color.Orange;
                e.Appearance.ProgressColor = Color.Orange;
            }
        }

        private void gc_HopDong_CustomDrawTimescaleColumn(object sender, DevExpress.XtraGantt.CustomDrawTimescaleColumnEventArgs e)
        {
            GanttTimescaleColumn column = e.Column;
            if (column.StartDate <= DateTime.Now && column.FinishDate >= DateTime.Now)
            {
                e.DrawBackground();
                float x = (float)e.GetPosition(DateTime.Now);
                float width = 4;
                RectangleF deadLineRect = new RectangleF(x, column.Bounds.Y, width, column.Bounds.Height);
                e.Cache.FillRectangle(DXSkinColors.FillColors.Danger, deadLineRect);
                e.DrawHeader();
                e.Handled = true;
            }
        }

        private void gc_HopDong_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
            }
        }
    }
}
