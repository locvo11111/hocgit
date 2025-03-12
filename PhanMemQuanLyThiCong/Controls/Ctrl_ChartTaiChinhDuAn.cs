using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.CustomEditor;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
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
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Ctrl_ChartTaiChinhDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        static Dictionary<string, List<Chart_KhoiLuongThanhTien>> dicBieuDo = new Dictionary<string, List<Chart_KhoiLuongThanhTien>>();
        DataTable _dt;
        static string newCodeDuAn = "";
        static bool checkDuAn = false;
        static Dictionary<string, KeyValuePair<long, long>> ThuChi = new Dictionary<string, KeyValuePair<long, long>>();
        public Ctrl_ChartTaiChinhDuAn()
        {
            InitializeComponent();
        }
        public void FcnLoadDataGrid(TypeDVTH typeDVTH,double TyLe,int SoNgay)
        {
            string colFk = MyConstant.lsColFkDVTH[(int)typeDVTH];
            string tbl = MyConstant.lsTblDVTH[(int)typeDVTH];
            string dbString = $"SELECT \"Code\",\"Ten\" FROM {tbl} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"CodeDuAn\"<>\"Code\"";
            if(typeDVTH == TypeDVTH.TuThucHien)
                dbString = $"SELECT \"Code\",\"Ten\" FROM {tbl} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"CodeTongThau\" IS NOT NULL";  
            else if(typeDVTH == TypeDVTH.NhaThauPhu)
                dbString = $"SELECT \"Code\",\"Ten\" FROM {tbl} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"CodeTongThau\" IS NULL";
            else if (colFk == "DuAn")
                dbString = $"SELECT \"Code\",\"TenDuAn\" as Ten FROM {tbl} WHERE \"Code\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_NhaThau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dt_NhaThau.Columns.Add("Thu", typeof(long));
            dt_NhaThau.Columns.Add("Chi", typeof(long));
            ThuChi.Clear();
            newCodeDuAn = Guid.NewGuid().ToString();
            foreach (DataRow item in dt_NhaThau.Rows)
            {
                List<KLHN> dtTheoNgay = Fcn_Update(item["Code"].ToString(), colFk);
                //List<KLHN> check = dtTheoNgay.Where(x=>x.ThanhTienThiCong<0).ToList();
                List<Chart_KhoiLuongThanhTien> Source = new List<Chart_KhoiLuongThanhTien>();
                List<Chart_KhoiLuongThanhTien> SourceCondition = new List<Chart_KhoiLuongThanhTien>();
                if (dtTheoNgay.Count() == 0 && colFk != "TenNhaCungCap")
                    Source = PushDataNullCongTac(TyLe, item["Code"].ToString(), colFk);
                else
                    Source = PushData(dtTheoNgay, TyLe, item["Code"].ToString(), colFk);
                if (Source.Count == 0)
                    continue;
                SourceCondition = Fcn_UpdateTheoSetting(Source, SoNgay);
                if (typeDVTH == TypeDVTH.DuAn)
                    item["Code"] = newCodeDuAn;
                if (dicBieuDo.Keys.Contains(item["Code"].ToString()))
                {
                    dicBieuDo.Remove(item["Code"].ToString());
                    dicBieuDo.Add(item["Code"].ToString(), SourceCondition);
                }
                else
                    dicBieuDo.Add(item["Code"].ToString(), SourceCondition);

                if (ThuChi.Keys.Contains(item["Code"].ToString()))
                {
                    item["Thu"] = ThuChi[item["Code"].ToString()].Key;
                    item["Chi"] = ThuChi[item["Code"].ToString()].Value;
                }
            }
            gc_BieuDoTaiChinh.DataSource = dt_NhaThau;
            gv_DanhSachDACT.ExpandAllGroups();
            Fcn_LoadData();
            //Fcn_LoadData();
        }
        public void Fcn_SetRowHeight(int Height)
        {
            gv_DanhSachDACT.RowHeight = Height;
        }
        public void Fcn_LoadData()
        {
            RepositoryItemAnyControl item = new RepositoryItemAnyControl();
            item.Control = cc_VatTu;
            item.AllowFocused = false;
            //ColChart.View.GridControl.RepositoryItems.Add(item);
            ColChart.ColumnEdit = item;
            item.AutoHeight = false;
            gv_DanhSachDACT.RowHeight = 100;
        }
        private List<KLHN> Fcn_Update(string CodeDVTH, string colCode)
        {
            string[] lsCodeCongTac;
            string dbString = "";
            DataTable dtTheoNgay = new DataTable();
            List<KLHN> lst = new List<KLHN>();
            string dbStringHangNgay;
            if (colCode == "TenNhaCungCap")
            {
                return lst;
            }
            dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {colCode}='{CodeDVTH}'";
            if (colCode == "DuAn")
                dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE (\"CodeNhaThau\" IS NULL OR \"CodeToDoi\" IS NOT NULL OR \"CodeNhaThauPhu\" IS NOT NULL) AND \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            lst = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(TypeKLHN.CongTac, lsCodeCongTac);
            return lst;
        }
        private List<Chart_KhoiLuongThanhTien> Fcn_UpdateTheoSetting(List<Chart_KhoiLuongThanhTien> Source,int Ngay)
        {
            if (Source == null)
                return null;

            List<Chart_KhoiLuongThanhTien> lsOutCondition = new List<Chart_KhoiLuongThanhTien>();
            DateTime Min = Source.Min(x => x.Date);
            DateTime Max = Source.Max(x => x.Date);
            XYDiagram diagram = (XYDiagram)cc_VatTu.Diagram;

            if (Ngay >= 1)
            {
                diagram.AxisX.WholeRange.SideMarginsValue = 1;
                diagram.AxisX.WholeRange.SetMinMaxValues(Min, Max);
                diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
                diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
                diagram.AxisX.DateTimeScaleOptions.GridSpacing = Ngay;
            }
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
                    TyLe=Tyle,
                    Thu=Thu,
                    Chi=Chi,
                    LuyKeThanhTienKeHoach= LuyKeThanhTienKeHoach

                });
                for (DateTime i = Min.AddDays(Ngay); i <= Max; i = i.AddDays(Ngay))
                {
                    LuyKeThanhTienKeHoach = Source.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : Source.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThanhTienKeHoach;
                    LuyKeThanhTienThiCong = Source.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeThanhTienThiCong : Source.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThanhTienThiCong;
                    LuyKeThu = Source.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeThu : Source.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThu;
                    LuyKeChi = Source.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeChi : Source.FindAll(x => x.Date == i).FirstOrDefault().LuyKeChi;

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
                        LuyKeThanhTienKeHoach = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienKeHoach;
                        LuyKeThanhTienThiCong = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienThiCong : Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienThiCong;
                        LuyKeThu = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThu : Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThu;
                        LuyKeChi = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeChi : Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeChi;

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
                diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                diagram.AxisX.DateTimeScaleOptions.GridSpacing = 1;
                diagram.AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Manual;
                diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month;
                List<DateTime> Month = new List<DateTime>();
                int tongThang = (Max.Year - Min.Year) * 12 + Max.Month - Min.Month;
                for (int i = 0; i <= tongThang; i++)
                {
                    DateTime Add = Min.AddMonths(i);
                    DateTime Begin = new DateTime(Add.Year, Add.Month, 1);
                    DateTime MaxDay = new DateTime(Add.Year, Add.Month, DateTime.DaysInMonth(Add.Year, Add.Month));
                    if (Add.Month == Max.Month && Add.Year == Max.Year)
                    {
                        LuyKeThanhTienKeHoach = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienKeHoach;
                        LuyKeThanhTienThiCong = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienThiCong : Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienThiCong;
                        LuyKeChi = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeChi : Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeChi;
                        LuyKeThu = Source.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThu : Source.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThu;
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
                    LuyKeThanhTienKeHoach = Source.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : Source.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThanhTienKeHoach;
                    LuyKeThanhTienThiCong = Source.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeThanhTienThiCong : Source.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThanhTienThiCong;
                    LuyKeChi = Source.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeChi : Source.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeChi;
                    LuyKeThu = Source.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeThu : Source.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThu;
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
        private List<KLHN> PushDataVatLieu(string colCode, string CodeNhacc)
        {
            List<KLHN> lsOut = new List<KLHN>();

            string dbString = $"SELECT {TDKH.TBL_KHVT_KhoiLuongHangNgay}.*,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCongTac,{TDKH.TBL_KHVT_KhoiLuongHangNgay}.Code AS CodeHangNgay,{TDKH.TBL_KHVT_VatTu}.DonGia AS DonGiaKeHoach," +
       $" {TDKH.TBL_KHVT_VatTu}.LoaiVatTu,{TDKH.TBL_KHVT_VatTu}.DonGiaThiCong, {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,{TDKH.TBL_KHVT_VatTu}.DonVi,  " +
       $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
       $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
       $"FROM {TDKH.TBL_KHVT_KhoiLuongHangNgay} " +
       $"JOIN {TDKH.TBL_KHVT_VatTu} ON {TDKH.TBL_KHVT_VatTu}.Code={TDKH.TBL_KHVT_KhoiLuongHangNgay}.CodeVatTu " +
       $"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
       $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
       $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
       $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KLTTHangNgay> lsSource = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            var lsCon = new List<KLTTHangNgay>();
            foreach (KLTTHangNgay item in lsSource)
            {
                List<Tbl_ThongTinNhaCungCapViewModel> _nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                var NccsVM = MyFunction.TryGetObjFromJson<NhaCungCapHangNgayViewModel>(item.NhaCungCap);
                if (!NccsVM.Any())
                    continue;
                Dictionary<string, string> _DicNccs = _nccs.ToDictionary(x => x.Code, x => x.Ten);
                if (_DicNccs is null)
                {
                    var nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                    _DicNccs = nccs.ToDictionary(x => x.Code, x => x.Ten);
                }
                foreach (var ncc in NccsVM.Where(x => x.Code == CodeNhacc))
                {
                    lsCon.Add(new KLTTHangNgay()
                    {
                        ParentCode = item.CodeCha,
                        TenCongTac = item.TenCongTac,
                        DonVi = item.DonVi,
                        MaCongTac = item.MaCongTac,
                        Code = Guid.NewGuid().ToString(),
                        CodeNhaCungCap = ncc.Code,
                        KhoiLuongThiCong = ncc.KhoiLuong,
                        DonGiaThiCong = ncc.DonGia,
                        CodeHangMuc = item.CodeHangMuc,
                        Ngay = item.Ngay

                    });
                }
            }
            dbString = $"SELECT {MyConstant.TBL_THONGTINNHACUNGCAP}.Code AS CodeDVTH, {MyConstant.TBL_THONGTINNHACUNGCAP}.Ten AS TenDVTH, " +
                $"{QLVT.TBL_QLVT_NHAPVT}.Code AS CodeCongTac,{QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu as TenCongTac,{QLVT.TBL_QLVT_YEUCAUVT}.MaVatTu as MaCongTac," +
                $"{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl as KhoiLuongHDVatLieu,{QLVT.TBL_QLVT_YEUCAUVT}.DonGiaHienTruong as DonGiaHDVatLieu , " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, " +
                $"{QLVT.TBL_QLVT_NHAPVTKLHN}.KhoiLuong AS KhoiLuongThiCong,{QLVT.TBL_QLVT_NHAPVTKLHN}.DonGia AS DonGiaThiCong,{QLVT.TBL_QLVT_NHAPVTKLHN}.Ngay " +
                $"FROM {MyConstant.TBL_THONGTINNHACUNGCAP} " +
                $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"ON {QLVT.TBL_QLVT_YEUCAUVT}.TenNhaCungCap = {MyConstant.TBL_THONGTINNHACUNGCAP}.Code " +
                $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVT} " +
                $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code = {QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
                $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVTKLHN} ON " +
                $"{QLVT.TBL_QLVT_NHAPVTKLHN}.CodeCha = {QLVT.TBL_QLVT_NHAPVT}.Code " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.Code = {QLVT.TBL_QLVT_YEUCAUVT}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {MyConstant.TBL_THONGTINNHACUNGCAP}.Code='{CodeNhacc}'";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<KLTTHangNgay> lsSourceQLVC = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            dbString = $"SELECT {TDKH.TBL_KHVT_VatTu}.*,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCha,{TDKH.TBL_KHVT_VatTu}.DonGiaThiCong AS DonGiaKeHoach," +
    $" {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,  " +
    $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
    $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
    $"FROM {TDKH.TBL_KHVT_VatTu} " +
    $"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
    $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
    $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
    $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_KHVT_VatTu}.CodeNhaThau IS NOT NULL";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KLTTHangNgay> lsSourceKHVTGoc = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            foreach (KLTTHangNgay item in lsSourceKHVTGoc)
            {
                List<KLTTHangNgay> CrQLVC = lsSourceQLVC.Where(x => x.CodeHangMuc == item.CodeHangMuc && x.MaCongTac == item.MaCongTac && x.DonVi == item.DonVi && x.TenCongTac == item.TenCongTac).ToList();
                foreach (KLTTHangNgay crow in CrQLVC)
                {
                    if (crow.KhoiLuongThiCong != null)
                        crow.ThanhTienThiCongSaved = (long)Math.Round((double)crow.KhoiLuongThiCong * (double)item.DonGiaThiCong);
                }
                List<KLTTHangNgay> CrCon = lsCon.Where(x => x.CodeHangMuc == item.CodeHangMuc && x.MaCongTac == item.MaCongTac && x.DonVi == item.DonVi && x.TenCongTac == item.TenCongTac).ToList();
                foreach (KLTTHangNgay crow in CrCon)
                {
                    if (crow.KhoiLuongThiCong != null)
                        crow.ThanhTienThiCongSaved = (long)Math.Round((double)crow.KhoiLuongThiCong * (double)item.DonGiaThiCong);
                }
            }

            List<KLTTHangNgay> lsSourceKHVT;
            if (lsSourceQLVC.Count == 0 && lsCon.Count() != 0)
                lsSourceKHVT = lsCon;
            else if (lsCon.Count() == 0 && lsSourceQLVC.Count() != 0)
                lsSourceKHVT = lsSourceQLVC;
            else
                lsSourceKHVT = lsSourceQLVC.Concat(lsCon).ToList();

            var CrDate = lsSourceKHVT.GroupBy(x => x.Ngay).OrderBy(x => x.Key);
            foreach (var item in CrDate)
            {
                if (item.Key == null)
                    continue;
                long TTTC = (long)item.Sum(x => x.ThanhTienThiCong);
                long TTTCGoc = (long)item.Sum(x => x.ThanhTienThiCongSaved);


                lsOut.Add(new KLHN
                {
                    Ngay = (DateTime)item.Key,
                    ThanhTienThiCong = TTTC,

                });

            }
            return lsOut;
        }
        private List<Chart_KhoiLuongThanhTien> PushDataNullCongTac(double TyLe, string CodeToChucCaNhan, string colCode)
        {
            string dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GiaTriGiaiChi as Chi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.DateXacNhanDaChi as Date FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
                $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} " +
                $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CodeDeXuat " +
                $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.ToChucCaNhanNhanChiPhiTamUng='{CodeToChucCaNhan}'";
            string dbStringThu = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NgayThangThucHien as Date,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.ThucTeThu as Thu  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
    $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} " +
    $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CodeDeXuat " +
    $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.ToChucCaNhanNhanChiPhiTamUng='{CodeToChucCaNhan}'";
            if (colCode == "DuAn")
            {
                dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GiaTriGiaiChi as Chi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.DateXacNhanDaChi as Date FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
$"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} " +
$"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CodeDeXuat " +
$"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";

                dbStringThu = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NgayThangThucHien as Date,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.ThucTeThu as Thu  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} " +
                    $"LEFT JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
                    $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CodeDeXuat " +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                    $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CongTrinh " +
                    $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            }

            DataTable dt_kc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataTable dt_kt = DataProvider.InstanceTHDA.ExecuteQuery(dbStringThu);
            List<Chart_KhoiLuongThanhTien> SourceThu = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kt);
            List<Chart_KhoiLuongThanhTien> SourceChi = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kc);

            List<Chart_KhoiLuongThanhTien> SumThuChi = new List<Chart_KhoiLuongThanhTien>(); ;

            List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();
            long luyKeTTThiCongThu = 0;
            long luyKeThu = 0;
            long luyKeChi = 0;
            long ThanhTienThu = 0;
            long ThanhTienChi = 0;

            if (SourceThu.Count() != 0)
                SumThuChi.AddRange(SourceThu);
            if (SourceChi.Count() != 0)
                SumThuChi.AddRange(SourceChi);           
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
                        ThuThiCong = luyKeTTThiCongThu,
                        TyLe = TyLe,
                        LuyKeThu = luyKeThu,
                        LuyKeChi = luyKeChi,
                        Thu = ThanhTienThu,
                        Chi = ThanhTienChi
                    });

                }
            }
            if (colCode == "DuAn")
            {
                ThuChi.Add(newCodeDuAn, new KeyValuePair<long, long>(luyKeThu, luyKeChi));
            }

            else
                ThuChi.Add(CodeToChucCaNhan, new KeyValuePair<long, long>(luyKeThu, luyKeChi));

            return lsOut;
        }
        private List<Chart_KhoiLuongThanhTien> PushData(List<KLHN> lsSourcete, double TyLe, string CodeToChucCaNhan, string colCode)
        {
            string dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GiaTriGiaiChi as Chi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.DateXacNhanDaChi as Date FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
                $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} " +
                $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CodeDeXuat " +
                $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.ToChucCaNhanNhanChiPhiTamUng='{CodeToChucCaNhan}'" +
                $" AND ({ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CheckDaChi='{true}' OR {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CheckDaChi='1') ";
            string dbStringThu = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NgayThangThucHien as Date,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.ThucTeThu as Thu  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
    $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} " +
    $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CodeDeXuat " +
    $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.ToChucCaNhanNhanChiPhiTamUng='{CodeToChucCaNhan}' " +
    $"AND ({ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CheckDaThu='{true}' OR {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CheckDaThu='1')";
            if (colCode == "DuAn")
            {
                dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GiaTriGiaiChi as Chi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.DateXacNhanDaChi as Date FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
$"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} " +
$"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CodeDeXuat " +
$"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' " +
$"AND ({ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CheckDaChi='{true}' OR {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CheckDaChi='1')";

                dbStringThu = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NgayThangThucHien as Date,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.ThucTeThu as Thu  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} " +
                    $"LEFT JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
                    $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CodeDeXuat " +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                    $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CongTrinh " +
                    $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' " +
                    $"AND ({ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CheckDaThu='{true}' OR {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CheckDaThu='1')";
            }

            DataTable dt_kc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataTable dt_kt = DataProvider.InstanceTHDA.ExecuteQuery(dbStringThu);
            List<Chart_KhoiLuongThanhTien> SourceThu = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kt);
            List<Chart_KhoiLuongThanhTien> SourceChi = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kc);

            if (colCode == "TenNhaCungCap")
            {
                lsSourcete = PushDataVatLieu(colCode, CodeToChucCaNhan);
            }
            List<Chart_KhoiLuongThanhTien> SumThuChi = new List<Chart_KhoiLuongThanhTien>(); ;

            List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();
            var grsDate = lsSourcete.GroupBy(x => x.Ngay).OrderBy(x => x.Key);
            long luyKeTTKeHoach = 0;
            long luyKeTTThiCong = 0;
            long luyKeTTThiCongThu = 0;
            long luyKeThu = 0;
            long luyKeChi = 0;
            long ThanhTienThu = 0;
            long ThanhTienChi = 0;
            if (lsSourcete.Count() == 0)
            {
                if (SourceThu.Count() == 0 && SourceChi.Count() == 0)
                {
                    cc_VatTu.DataSource = null;
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
                if (colCode == "DuAn")
                {
                    ThuChi.Add(newCodeDuAn, new KeyValuePair<long, long>(luyKeThu, luyKeChi));
                }

                else
                    ThuChi.Add(CodeToChucCaNhan, new KeyValuePair<long, long>(luyKeThu, luyKeChi));
                return lsOut;

            }
            DateTime MaxNgay = grsDate.Max(x => x.Key);
            DateTime MinNgay = grsDate.Min(x => x.Key);
            Chart_KhoiLuongThanhTien[] crThu;
            Chart_KhoiLuongThanhTien[] crChi;

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
                        ThuThiCong = luyKeTTThiCongThu,
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


                long ThanhTienKH =(long)item.Sum(x => x.ThanhTienKeHoach);
                long ThanhTienTC = colCode == "CodeNhaThau"|| colCode == "TenNhaCungCap" ? (long)item.Sum(x => x.ThanhTienThiCong) : (long)item.Sum(x => x.ThanhTienThiCong);
                long ThanhTienTCThu = (long)item.Sum(x => x.ThanhTienThiCong);
                luyKeTTKeHoach += ThanhTienKH;
                luyKeTTThiCong += ThanhTienTC;
                luyKeTTThiCongThu += ThanhTienTCThu;

                lsOut.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    LuyKeThanhTienKeHoach = luyKeTTKeHoach,
                    LuyKeThanhTienThiCong = luyKeTTThiCong,
                    ThuThiCong = luyKeTTThiCongThu,
                    TyLe = TyLe,
                    LuyKeChi = luyKeChi,
                    LuyKeThu = luyKeThu,
                    ThanhTienKeHoach = ThanhTienKH,
                    ThanhTienThiCong = ThanhTienTC,
                    Thu = ThanhTienThu,
                    Chi = ThanhTienChi

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
                        ThuThiCong = luyKeTTThiCongThu,
                        TyLe = TyLe,
                        LuyKeThu = luyKeThu,
                        LuyKeChi = luyKeChi,
                        Thu = ThanhTienThu,
                        Chi = ThanhTienChi
                    });

                }
            }
            if (colCode == "DuAn")
            {
                ThuChi.Add(newCodeDuAn, new KeyValuePair<long, long>(luyKeThu, luyKeChi));
            }

            else
                ThuChi.Add(CodeToChucCaNhan, new KeyValuePair<long, long>(luyKeThu, luyKeChi));

            return lsOut;
        }

        private void gv_DanhSachDACT_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column == ColChart)
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string code = dr["Code"].ToString();
                if(dicBieuDo.ContainsKey(code))
                    e.Value = dicBieuDo[code];
            }
        }
        [DisplayName("Visible")]
        [Category("SeriLuyKeThu")]
        public bool SeriLuyKeThu
        {
            get
            {
                return cc_VatTu.Series[4].Visible;
            }

            set
            {
                cc_VatTu.Series[4].Visible = value;
            }
        }   
        [DisplayName("Visible")]
        [Category("SeriLuyKeChi")]
        public bool SeriLuyKeChi
        {
            get
            {
                return cc_VatTu.Series[5].Visible;
            }

            set
            {
                cc_VatTu.Series[5].Visible = value;
            }
        }     
        [DisplayName("Visible")]
        [Category("SeriThanhTienKeHoach")]
        public bool SeriThanhTienKeHoach
        {
            get
            {
                return cc_VatTu.Series[5].Visible;
            }

            set
            {
                cc_VatTu.Series[0].Visible = value;
                cc_VatTu.Series[2].Visible = value;
                //cc_VatTu.Series[5].Visible = value;
            }
        }  
        [DisplayName("Visible")]
        [Category("SeriThanhTienHangNgay")]
        public bool SeriThanhTienHangNgay
        {
            get
            {
                return cc_VatTu.Series[5].Visible;
            }

            set
            {
                cc_VatTu.Series[6].Visible = value;
                cc_VatTu.Series[7].Visible = value;
                cc_VatTu.Series[8].Visible = value;
                cc_VatTu.Series[9].Visible = value;
                cc_VatTu.Series[10].Visible = value;
                cc_VatTu.Series[11].Visible = value;
                checkDuAn = value;
            }
        }   
        [DisplayName("Visible")]
        [Category("SeriThanhTienHangNgay")]
        public bool SeriThanhTienThiCong
        {
            get
            {
                return cc_VatTu.Series[12].Visible;
                //return cc_VatTu.Series[13].Visible;
            }

            set
            {
                cc_VatTu.Series[12].Visible = value;
                //cc_VatTu.Series[13].Visible = value;
                checkDuAn = value;
            }
        }
        [DisplayName("Visible")]
        [Category("Legend")]
        public DefaultBoolean Legend
        {
            get
            {
                return cc_VatTu.Legend.Visibility;
            }

            set
            {
                cc_VatTu.Legend.Visibility = value;
            }
        }
    }
}
