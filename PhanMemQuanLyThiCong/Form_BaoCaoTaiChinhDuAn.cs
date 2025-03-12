using DevExpress.Utils;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
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
using static PhanMemQuanLyThiCong.Common.Helper.DuAnHelper;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_BaoCaoTaiChinhDuAn : DevExpress.XtraEditors.XtraForm
    {
        public Form_BaoCaoTaiChinhDuAn()
        {
            InitializeComponent();
            DuAnHelper.GetDonViThucHiens(ctrl_DonViThucHienDuAn);
            dv_BaoCao.Hide();
        }

        private void rg_CaiDat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = rg_CaiDat.SelectedIndex;
            if (index != 0)
            {
                se_songay.Enabled = false;
                if (index == 1)
                    se_songay.Value = 7;
            }

            else
                se_songay.Enabled = true;
        }
        public int SoNgay { get { if (Index == 2) return 0; else return (int)se_songay.Value; } set { se_songay.Value = value; } }
        public bool TrangThai { get { return se_songay.Enabled; } set { se_songay.Enabled = value; } }
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
        public int Index { get { return rg_CaiDat.SelectedIndex; } set { rg_CaiDat.SelectedIndex = value; } }

        private void sb_xuatbaocao_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu, Vui lòng chờ!");
            string dbString = $"DELETE FROM Tbl_ChartTaiChinh";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            dbString = $"DELETE FROM Tbl_BaoCaoTaiChinhDuAn";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            dbString = $"SELECT * FROM Tbl_BaoCaoTaiChinhDuAn";
            DataTable dt_NhaThau = DataProvider.InstanceBaoCao.ExecuteQuery(dbString);
            ChartTaiChinhDuAn Baocao = new ChartTaiChinhDuAn();
            if (!ce_All.Checked)
            {
                DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;

                string Code = Guid.NewGuid().ToString();
                var dtTheoNgay = Fcn_Update(DVTH.Code, DVTH.ColCodeFK);
                if (dtTheoNgay.Count == 0)
                {
                    MessageShower.ShowInformation("Nhà thầu chọn hiện chưa có công tác, Vui lòng chọn nhà thầu khác có công tác để xuất báo cáo!!!!!!!!");
                    WaitFormHelper.CloseWaitForm();
                    return;
                }
                double TyLe = (double)double.Parse(Ce_LoiNhuan.Text) / 100;
                List<Chart_KhoiLuongThanhTien> Source = PushData(dtTheoNgay, TyLe, DVTH);
                List<Chart_KhoiLuongThanhTien> SourceCondition = Fcn_UpdateTheoSetting(Source, SoNgay);
                SourceCondition.ForEach(x => x.ID = Code);
                SourceCondition.ForEach(x => x.Code = Guid.NewGuid().ToString());

                DataRow newRow = dt_NhaThau.NewRow();
                newRow["Code"] = Code;
                newRow["Ten"] = DVTH.Ten;
                dt_NhaThau.Rows.Add(newRow);
                DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dt_NhaThau, "Tbl_BaoCaoTaiChinhDuAn");
                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                DataTable dtTaiChinh = converter.ToDataTableList<Chart_KhoiLuongThanhTien>(SourceCondition);
                foreach (DataRow row in dtTaiChinh.Rows)
                    row["Date"] = DateTime.Parse(row["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dtTaiChinh, "Tbl_ChartTaiChinh");
                DateTime Min = Source.Min(x => x.Date);
                DateTime Max = Source.Max(x => x.Date);
                Baocao.Fcn_Setting(SoNgay/*, Min, Max*/, (DefaultBoolean)Check);
                dv_BaoCao.UseAsyncDocumentCreation = DefaultBoolean.True;
                dv_BaoCao.RequestDocumentCreation = true;
                dv_BaoCao.DocumentSource = Baocao;
                dv_BaoCao.InitiateDocumentCreation();
                dv_BaoCao.Visible = true;
            }
            else
            {
                List<DonViThucHien> DonViTH = ctrl_DonViThucHienDuAn.DataSource as List<DonViThucHien>;
                List<Chart_KhoiLuongThanhTien> SourceOut = new List<Chart_KhoiLuongThanhTien>();
                foreach (DonViThucHien item in DonViTH)
                {
                    string Code = Guid.NewGuid().ToString();
                    var dtTheoNgay = Fcn_Update(item.Code, item.ColCodeFK);
                    double TyLe = (double)double.Parse(Ce_LoiNhuan.Text) / 100;
                    List<Chart_KhoiLuongThanhTien> Source = PushData(dtTheoNgay, TyLe, item);
                    if (Source == null)
                        continue;
                    List<Chart_KhoiLuongThanhTien> SourceCondition = Fcn_UpdateTheoSetting(Source, SoNgay);
                    SourceCondition.ForEach(x => x.ID = Code);
                    SourceCondition.ForEach(x => x.Code = Guid.NewGuid().ToString());

                    DataRow newRow = dt_NhaThau.NewRow();
                    newRow["Code"] = Code;
                    newRow["Ten"] = item.Ten;
                    dt_NhaThau.Rows.Add(newRow);
                    SourceOut.AddRange(SourceCondition);
                }
                DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dt_NhaThau, "Tbl_BaoCaoTaiChinhDuAn");
                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                DataTable dtTaiChinh = converter.ToDataTableList<Chart_KhoiLuongThanhTien>(SourceOut);
                foreach (DataRow row in dtTaiChinh.Rows)
                    row["Date"] = DateTime.Parse(row["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dtTaiChinh, "Tbl_ChartTaiChinh");
                Baocao.Fcn_Setting(SoNgay, (DefaultBoolean)Check);
                dv_BaoCao.UseAsyncDocumentCreation = DefaultBoolean.True;
                dv_BaoCao.RequestDocumentCreation = true;
                dv_BaoCao.DocumentSource = Baocao;
                dv_BaoCao.InitiateDocumentCreation();
                dv_BaoCao.Visible = true;
            }
            WaitFormHelper.CloseWaitForm();
            MessageShower.ShowInformation("Xuất báo cáo thành công!", "Thông tin");

        }
        private List<Chart_KhoiLuongThanhTien> Fcn_UpdateTheoSetting(List<Chart_KhoiLuongThanhTien> Source, int Ngay)
        {
            if (Source == null)
                return null;

            List<Chart_KhoiLuongThanhTien> lsOutCondition = new List<Chart_KhoiLuongThanhTien>();
            DateTime Min = Source.Min(x => x.Date);
            DateTime Max = Source.Max(x => x.Date);
            long LuyKeThu = Source.FindAll(x => x.Date == Min).FirstOrDefault().LuyKeThu;
            long LuyKeChi = Source.FindAll(x => x.Date == Min).FirstOrDefault().LuyKeChi;
            long Thu = Source.FindAll(x => x.Date == Min).FirstOrDefault().Thu;
            long Chi = Source.FindAll(x => x.Date == Min).FirstOrDefault().Chi;
            long LuyKeThanhTienKeHoach = Source.FindAll(x => x.Date == Min).FirstOrDefault().LuyKeThanhTienKeHoach;
            long LuyKeThanhTienThiCong = Source.FindAll(x => x.Date == Min).FirstOrDefault().LuyKeThanhTienThiCong;
            long ThanhTienKeHoach = Source.FindAll(x => x.Date == Min).FirstOrDefault().ThanhTienKeHoach;
            long ThanhTienThiCong = Source.FindAll(x => x.Date == Min).FirstOrDefault().ThanhTienThiCong;
            double Tyle = Source.FirstOrDefault().TyLe;
            if (Ngay == 1)
                lsOutCondition = Source;
            else if (Ngay > 1)
            {
                lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = Min,
                    LuyKeThu = LuyKeThu,
                    LuyKeChi = LuyKeChi,
                    LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                    ThanhTienKeHoach = ThanhTienKeHoach,
                    ThanhTienThiCong = ThanhTienThiCong,
                    TyLe = Tyle,
                    Thu = Thu,
                    Chi = Chi,
                    LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach

                });
                for (DateTime i = Min.AddDays(Ngay); i <= Max; i = i.AddDays(Ngay))
                {
                    LuyKeThanhTienKeHoach = Source.FindAll(x => x.Date == i).FirstOrDefault()==null? LuyKeThanhTienKeHoach: Source.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThanhTienKeHoach;
                    LuyKeThanhTienThiCong = Source.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeThanhTienThiCong:Source.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThanhTienThiCong;
                    LuyKeThu = Source.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeThu: Source.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThu;
                    LuyKeChi = Source.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeChi: Source.FindAll(x => x.Date == i).FirstOrDefault().LuyKeChi;

                    DateTime Pre = i.AddDays(-Ngay);

                    ThanhTienKeHoach = Source.FindAll(x => x.Date > Pre && x.Date <= i).Sum(x => x.ThanhTienKeHoach);
                    ThanhTienThiCong = Source.FindAll(x => x.Date > Pre && x.Date <= i).Sum(x => x.ThanhTienThiCong);
                    Thu = Source.FindAll(x => x.Date > Pre && x.Date <= i).Sum(x => x.Thu);
                    Chi = Source.FindAll(x => x.Date > Pre && x.Date <= i).Sum(x => x.Chi);

                    lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = i,
                        LuyKeThu = LuyKeThu,
                        LuyKeChi = LuyKeChi,
                        LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                        ThanhTienKeHoach = ThanhTienKeHoach,
                        ThanhTienThiCong = ThanhTienThiCong,
                        TyLe = Tyle,
                        Thu = Thu,
                        Chi = Chi,
                        LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach

                    });

                    if (i.AddDays(Ngay) >= Max)
                    {
                        LuyKeThanhTienKeHoach = Source.FindAll(x => x.Date == Max).FirstOrDefault()==null? LuyKeThanhTienKeHoach: Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienKeHoach;
                        LuyKeThanhTienThiCong = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienThiCong: Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienThiCong;
                        LuyKeThu = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThu: Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThu;
                        LuyKeChi = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeChi: Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeChi;

                        ThanhTienKeHoach = Source.FindAll(x => x.Date > i && x.Date <= Max).Sum(x => x.ThanhTienKeHoach);
                        ThanhTienThiCong = Source.FindAll(x => x.Date > i && x.Date <= Max).Sum(x => x.ThanhTienThiCong);
                        Thu = Source.FindAll(x => x.Date > i && x.Date <= Max).Sum(x => x.Thu);
                        Chi = Source.FindAll(x => x.Date > i && x.Date <= Max).Sum(x => x.Chi);

                        lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = Max,
                            LuyKeThu = LuyKeThu,
                            LuyKeChi = LuyKeChi,
                            LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                            ThanhTienKeHoach = ThanhTienKeHoach,
                            ThanhTienThiCong = ThanhTienThiCong,
                            TyLe = Tyle,
                            Thu = Thu,
                            Chi = Chi,
                            LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach
                        });
                        return lsOutCondition;
                    }

                }

            }
            else
            {
                List<DateTime> Month = new List<DateTime>();
                int tongThang = (Max.Year - Min.Year) * 12 + Max.Month - Min.Month;
                for (int i = 0; i <= tongThang; i++)
                {
                    DateTime Add = Min.AddMonths(i);
                    DateTime Begin = new DateTime(Add.Year, Add.Month, 1);
                    DateTime MaxDay = new DateTime(Add.Year, Add.Month, DateTime.DaysInMonth(Add.Year, Add.Month));
                    if (Add.Month == Max.Month && Add.Year == Max.Year)
                    {
                        LuyKeThanhTienKeHoach = Source.FindAll(x => x.Date == Max).FirstOrDefault()==null? LuyKeThanhTienKeHoach: Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienKeHoach;
                        LuyKeThanhTienThiCong = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienThiCong: Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienThiCong;
                        LuyKeChi = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeChi: Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeChi;
                        LuyKeThu = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThu: Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThu;
                        ThanhTienKeHoach = Source.FindAll(x => x.Date >= Begin && x.Date <= Max).Sum(x => x.ThanhTienKeHoach);
                        ThanhTienThiCong = Source.FindAll(x => x.Date >= Begin && x.Date <= Max).Sum(x => x.ThanhTienThiCong);
                        Thu = Source.FindAll(x => x.Date >= Begin && x.Date <= Max).Sum(x => x.Thu);
                        Chi = Source.FindAll(x => x.Date >= Begin && x.Date <= Max).Sum(x => x.Chi);
                        lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = Begin,
                            LuyKeThu = LuyKeThu,
                            LuyKeChi = LuyKeChi,
                            LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                            ThanhTienKeHoach = ThanhTienKeHoach,
                            ThanhTienThiCong = ThanhTienThiCong,
                            TyLe = Tyle,
                            Thu = Thu,
                            Chi = Chi,
                            LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach
                        });
                        return lsOutCondition;
                    }
                    LuyKeThanhTienKeHoach = Source.FindAll(x => x.Date == MaxDay).FirstOrDefault()==null? LuyKeThanhTienKeHoach: Source.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThanhTienKeHoach;
                    LuyKeThanhTienThiCong = Source.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeThanhTienThiCong: Source.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThanhTienThiCong;
                    LuyKeChi = Source.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeChi: Source.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeChi;
                    LuyKeThu = Source.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeThu: Source.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThu;
                    ThanhTienKeHoach = Source.FindAll(x => x.Date >= Begin && x.Date <= MaxDay).Sum(x => x.ThanhTienKeHoach);
                    ThanhTienThiCong = Source.FindAll(x => x.Date >= Begin && x.Date <= MaxDay).Sum(x => x.ThanhTienThiCong);
                    Thu = Source.FindAll(x => x.Date >= Begin && x.Date <= MaxDay).Sum(x => x.Thu);
                    Chi = Source.FindAll(x => x.Date >= Begin && x.Date <= MaxDay).Sum(x => x.Chi);
                    lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = Begin,
                        LuyKeThu = LuyKeThu,
                        LuyKeChi = LuyKeChi,
                        LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                        ThanhTienKeHoach = ThanhTienKeHoach,
                        ThanhTienThiCong = ThanhTienThiCong,
                        TyLe = Tyle,
                        Thu = Thu,
                        Chi = Chi,
                        LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach
                    });
                }
            }
            return lsOutCondition;
        }
        private List<Chart_KhoiLuongThanhTien> PushData(List<KLHN> lsSourcete, double TyLe, DonViThucHien DVTH)
        {
            string dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GiaTriGiaiChi as Chi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.DateXacNhanDaChi as Date FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
                $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} " +
                $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CodeDeXuat " +
                $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.ToChucCaNhanNhanChiPhiTamUng='{DVTH.Code}'";
            string dbStringThu = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NgayThangThucHien as Date,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.ThucTeThu as Thu  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
    $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} " +
    $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CodeDeXuat " +
    $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.ToChucCaNhanNhanChiPhiTamUng='{DVTH.Code}'";

            DataTable dt_kc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


            DataTable dt_kt = DataProvider.InstanceTHDA.ExecuteQuery(dbStringThu);

            List<Chart_KhoiLuongThanhTien> SourceThu = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kt);
            List<Chart_KhoiLuongThanhTien> SourceChi = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kc);
            List<Chart_KhoiLuongThanhTien> SumThuChi = new List<Chart_KhoiLuongThanhTien>(); ;

            List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();
            var grsDate = lsSourcete.GroupBy(x => x.Ngay).OrderBy(x => x.Key);
            long luyKeTTKeHoach = 0;
            long luyKeTTThiCong = 0;
            long luyKeThu = 0;
            long luyKeChi = 0;
            long ThanhTienThu = 0;
            long ThanhTienChi = 0;
            if (lsSourcete.Count() == 0)
            {
                if (SourceThu.Count() == 0 && SourceChi.Count() == 0)
                {
                    return null;
                }
                else
                {
                    if (SourceThu.Count() != 0)
                        SumThuChi.AddRange(SourceThu);
                    if (SourceChi.Count() != 0)
                        SumThuChi.AddRange(SourceChi);
                    var crDateChi = SumThuChi.GroupBy(x => x.Date).OrderBy(x => x.Key);
                    foreach (var item in crDateChi)
                    {
                        ThanhTienThu = item.Sum(x => x.Thu);
                        ThanhTienChi = item.Sum(x => x.Chi);
                        luyKeThu += ThanhTienThu;
                        luyKeChi += ThanhTienChi;
                        lsOut.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = DateTime.Parse(item.Key.ToString()),
                            LuyKeThu = luyKeThu,
                            LuyKeChi = luyKeChi,
                            Thu = ThanhTienThu,
                            Chi = ThanhTienChi
                        });

                    }
                    if (crDateChi.Count() == 1)
                    {
                        lsOut.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = crDateChi.First().Key.AddDays(5),
                            LuyKeThu = luyKeThu,
                            LuyKeChi = luyKeChi
                        });
                        lsOut.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = crDateChi.First().Key.AddDays(-5),
                            LuyKeThu = 0,
                            LuyKeChi = 0
                        });
                    }
                }
                return lsOut;

            }
            Chart_KhoiLuongThanhTien[] crThu;
            Chart_KhoiLuongThanhTien[] crChi;
            DateTime MaxNgay = grsDate.Max(x => x.Key);
            DateTime MinNgay = grsDate.Min(x => x.Key);

            crThu = SourceThu.Where(x => x.Date < MinNgay).ToArray();
            crChi = SourceChi.Where(x => x.Date < MinNgay).ToArray();
            if (crChi.Count() != 0)
                SumThuChi.AddRange(crChi);
            if (crThu.Count() != 0)
                SumThuChi.AddRange(crThu);
            if (SumThuChi.Count() != 0)
            {
                var crDateChi = SumThuChi.GroupBy(x => x.Date).OrderBy(x => x.Key);
                foreach (var item in crDateChi)
                {
                    ThanhTienThu = item.Sum(x => x.Thu);
                    ThanhTienChi = item.Sum(x => x.Chi);
                    luyKeThu += ThanhTienThu;
                    luyKeChi += ThanhTienChi;
                    lsOut.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = DateTime.Parse(item.Key.ToString()),
                        LuyKeThanhTienKeHoach = luyKeTTKeHoach,
                        LuyKeThanhTienThiCong = luyKeTTThiCong,
                        TyLe = TyLe,
                        LuyKeThu = luyKeThu,
                        LuyKeChi = luyKeChi,
                        Thu = ThanhTienThu,
                        Chi = ThanhTienChi
                    });

                }
            }

            foreach (var item in grsDate)
            {
                //crThu = dt_kt.AsEnumerable().Where(x => DateTime.Parse(x["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) == DateTime.Parse(item.Key.ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)).ToArray();
                //crChi = dt_kc.AsEnumerable().Where(x => DateTime.Parse(x["Date"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) == DateTime.Parse(item.Key.ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)).ToArray();
                crThu = SourceThu.Where(x => x.Date == item.Key).ToArray();
                crChi = SourceChi.Where(x => x.Date == item.Key).ToArray();
                ThanhTienChi = ThanhTienThu = 0;
                if (crThu.Count() != 0)
                {
                    ThanhTienThu = crThu.AsEnumerable().Sum(x => x.Thu);
                    luyKeThu += ThanhTienThu;
                }
                if (crChi.Count() != 0)
                {
                    ThanhTienChi = crChi.AsEnumerable().Sum(x => x.Chi);
                    luyKeChi += ThanhTienChi;
                }


                long ThanhTienKH =(long) item.Sum(x => x.ThanhTienKeHoach);
                long ThanhTienTC =(long) item.Sum(x => x.ThanhTienThiCong);
                //long ThanhTienLNKH = item.Sum(x => x.ThanhTienKeHoach);
                //long ThanhTienTC = item.Sum(x => x.ThanhTienThiCong);
                luyKeTTKeHoach += ThanhTienKH;
                luyKeTTThiCong += ThanhTienTC;


                lsOut.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    LuyKeThanhTienKeHoach = luyKeTTKeHoach,
                    LuyKeThanhTienThiCong = luyKeTTThiCong,
                    TyLe = TyLe,
                    LuyKeChi = luyKeChi,
                    LuyKeThu = luyKeThu,
                    ThanhTienKeHoach = ThanhTienKH,
                    ThanhTienThiCong = ThanhTienTC,
                    Thu = ThanhTienThu,
                    Chi = ThanhTienChi
                    //lU = ThanhTienKH,
                    //ThanhTienThiCong = luyKeTTThiCong

                });
            }
            SumThuChi.Clear();
            crThu = SourceThu.Where(x => x.Date > MaxNgay).ToArray();
            crChi = SourceChi.Where(x => x.Date > MaxNgay).ToArray();
            if (crChi.Count() != 0)
                SumThuChi.AddRange(crChi);
            if (crThu.Count() != 0)
                SumThuChi.AddRange(crThu);
            if (SumThuChi.Count() != 0)
            {
                var crDateChi = SumThuChi.GroupBy(x => x.Date).OrderBy(x => x.Key);
                foreach (var item in crDateChi)
                {
                    ThanhTienThu = item.Sum(x => x.Thu);
                    ThanhTienChi = item.Sum(x => x.Chi);
                    luyKeThu += ThanhTienThu;
                    luyKeChi += ThanhTienChi;
                    lsOut.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = DateTime.Parse(item.Key.ToString()),
                        LuyKeThanhTienKeHoach = luyKeTTKeHoach,
                        LuyKeThanhTienThiCong = luyKeTTThiCong,
                        TyLe = TyLe,
                        LuyKeThu = luyKeThu,
                        LuyKeChi = luyKeChi,
                        Thu = ThanhTienThu,
                        Chi = ThanhTienChi
                    });

                }
            }

            return lsOut;
        }
        private List<KLHN> Fcn_Update(string CodeDVTH, string colCode)
        {
            string[] lsCodeCongTac;
            string dbString = "";
            string dbStringHangNgay;
            dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {colCode}='{CodeDVTH}'";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            var dtTheoNgay = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac);
            return dtTheoNgay;
        }

        private void ce_All_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_All.Checked)
                ctrl_DonViThucHienDuAn.Enabled = false;
            else
                ctrl_DonViThucHienDuAn.Enabled = true;
        }
    }
}