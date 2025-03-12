//using DevExpress.Pdf.Native.BouncyCastle.Asn1.Pkcs;
//using DevExpress.PivotGrid.SliceQueryDataSource;
using DevExpress.DevAV.Chat.Model;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraSpreadsheet.Import.OpenXml;
using DevExpress.XtraSpreadsheet.Menu;

//using DevExpress.XtraSpreadsheet.Model;
using MoreLinq;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using PhanMemQuanLyThiCong.Constant.Enum;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using PM360.Common.Helper;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong
{

    //enum VatTuFilter
    //{
    //    DuAn,
    //    CongTrinh,
    //    HangMuc
    //};

    public partial class XtraForm_BaoCaoDongTienTDKH : DevExpress.XtraEditors.XtraForm
    {

        private Dictionary<DateTime, int> _dicDate = new Dictionary<DateTime, int>();
        long _KinhPhiPhanBo = 0;
        long _KinhPhiPhanBoVatLieu = 0;
        long _KinhPhiPhanBoNhanCong = 0;
        long _KinhPhiPhanBoMay = 0;

        string prefixFormula = MyConstant.PrefixFormula;

        DateTime _minDate, _maxDate;

        List<string> _codeNhomsHasKL = new List<string>();
        List<string> _codeCTacs = new List<string>();
        List<string> _codeVTus = new List<string>();
        List<string> _codeHPhis = new List<string>();


        //List<KLHN> _KLHNCTacs = new List<KLHN>();
        //List<KLHN> _KLHNVTus = new List<KLHN>();
        //List<KLHN> _KLHNHPhis = new List<KLHN>();
        //List<KLHN> _KLHNHNhoms = new List<KLHN>();
        List<string> _initCodeCongTac = new List<string>();
        List<string> _initNhom = new List<string>();

        public XtraForm_BaoCaoDongTienTDKH(List<string> codes, List<string> nhoms)

        {

            _initCodeCongTac = codes;
            _initNhom = nhoms;
            InitializeComponent();
            rg_type.SelectedIndexChanged -= rg_type_SelectedIndexChanged;
            rg_type.SelectedIndex = 0;
            rg_type.SelectedIndexChanged += rg_type_SelectedIndexChanged;

            var drKPPB = TongHopHelper.GetKinhPhiPhanBo();
            _KinhPhiPhanBo = (long)drKPPB[TDKH.RANGE_KinhPhiPhanBoToanDuAn];
            _KinhPhiPhanBoVatLieu = (long)drKPPB[TDKH.RANGE_KinhPhiPhanBoVatLieu];
            _KinhPhiPhanBoNhanCong = (long)drKPPB[TDKH.RANGE_KinhPhiPhanBoNhanCong];
            _KinhPhiPhanBoMay = (long)drKPPB[TDKH.RANGE_KinhPhiPhanBoMay];

            ctrl_DonViThucHien.DVTHChanged -= ctrl_DonViThucHien_DVTHChanged;
            DuAnHelper.GetDonViThucHiens(ctrl_DonViThucHien);
            ctrl_DonViThucHien.EditValue = SharedControls.ctrl_DonViThucHienDuAnTDKH.EditValue;
            LoadData();
            ctrl_DonViThucHien.DVTHChanged += ctrl_DonViThucHien_DVTHChanged;
        }

        private void XtraForm_BaoCaoDongTienTDKH_Load(object sender, EventArgs e)
        {
            bool isKeHoach = BaseFrom.allPermission.HaveInitProjectPermission;
            ce_HienKeHoach.CheckedChanged -= ce_HienKeHoach_CheckedChanged;
            ce_HienKeHoach.Checked = isKeHoach;
            ce_HienKeHoach.Enabled = isKeHoach;
            ce_HienKeHoach.CheckedChanged += ce_HienKeHoach_CheckedChanged;

        }

        private void LoadData()
        {
            var crDVTH = ctrl_DonViThucHien.SelectedDVTH;
            if (crDVTH is null)
            {
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu");

            if (rg_type.GetAccessibleName() == "CongTac")
            {
                lci_hienCongTac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gr_LapLaiKeHoach.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                LoadAllKhoiLuongCongTacToSheet();
            }
            else
            {
                lci_hienCongTac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                gr_LapLaiKeHoach.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                LoadVatTu();
            }
            WaitFormHelper.CloseWaitForm();
        }

        private void LoadVatTu()
        {

            string type = rg_Filter.GetAccessibleName();
            switch (type)
            {
                case "PhanDoan":
                    LoadAllKhoiLuongVatTuHaoPhiToSheet();
                    break;
                default:
                    LoadAllKhoiLuongVatTuHaoPhiToSheetWithFilter(type);
                    break;

            }
        }
        private void LoadAllKhoiLuongCongTacToSheet()
        {
            var crDVTH = ctrl_DonViThucHien.SelectedDVTH;
            if (crDVTH is null)
            {
                return;
            }

            string conditionCTTK = null;
            if (_initCodeCongTac != null)
            {

                conditionCTTK = $"cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(_initCodeCongTac)})";
            }

            if (_initNhom?.Any() == true)
            {

                if (conditionCTTK != null)
                    conditionCTTK += " OR ";
                conditionCTTK = $"cttk.CodeNhom IN ({MyFunction.fcn_Array2listQueryCondition(_initNhom)})";

            }



            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);


            DataTable CongTacs = TDKHHelper.GetCongTacsDataTable(SharedControls.slke_ThongTinDuAn.EditValue?.ToString(),
                codeCT, codeHM, SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString(), $"cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}'", conditionCTTK);

            //_codeCTacs = CongTacs.AsEnumerable().Select(x => x["Code"].ToString()).ToList();
            //_KLHNCTacs = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, _codeCTacs, isLapLaiKeHoach: ce_LapLaiKeHoach.Checked);

            var codeNhoms = CongTacs.AsEnumerable().Where(x => x["CodeNhom"] != DBNull.Value)
                    .Select(x => x["CodeNhom"].ToString()).Distinct().ToList();

            string strCodeNhom = MyFunction.fcn_Array2listQueryCondition(codeNhoms);

            string dbString = $"SELECT * FROM {TDKH.TBL_NhomCongTac} WHERE Code IN ({strCodeNhom})";
            DataTable dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            _codeNhomsHasKL = dtNhom.AsEnumerable().Where(x => x["KhoiLuongKeHoach"] != DBNull.Value).Select(x => x["Code"].ToString()).ToList();
            //_KLHNHNhoms = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, _codeNhomsHasKL);
            _codeCTacs = CongTacs.AsEnumerable().Where(x => !_codeNhomsHasKL.Contains(x["CodeNhom"].ToString())).Select(x => x["Code"].ToString()).ToList();

            if (CongTacs.Rows.Count == 0)
            {
                MessageShower.ShowWarning("Không có công tác để tổng hợp!");
                return;
            }

            //DateTime? minDate = null, maxDate = null;

            string mindateStr = CongTacs.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Min(x => StringHelper.Min((string)x["NgayBatDau"], (x["NgayBatDauThiCong"] == DBNull.Value) ? (string)x["NgayBatDau"] : (string)x["NgayBatDauThiCong"]));
            string maxdateStr = CongTacs.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Max(x => StringHelper.Max((string)x["NgayKetThuc"], (x["NgayKetThucThiCong"] == DBNull.Value) ? (string)x["NgayKetThuc"] : (string)x["NgayKetThucThiCong"]));

            if (!DateTime.TryParse(mindateStr, out _minDate) || !DateTime.TryParse(maxdateStr, out _maxDate))
            {
                MessageShower.ShowWarning("Không có ngày thực hiện để tổng hợp");
                return;
            }

            //var lsNgay = _KLHNCTacs.Select(x => x.Ngay.Date).Distinct().OrderBy(x => x);


            FileHelper.fcn_spSheetStreamDocument(spsheet_TongHop, $@"{BaseFrom.m_templatePath}\FileExcel\16.BaoCaoDongTienTDKH.xlsx");

            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu");

            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];
            ////          wb.History.IsEnabled = false;
            wb.BeginUpdate();

            var definedName = wb.DefinedNames.GetDefinedName("Data");
            var rangeDate = wb.Range["Ngay"];

            int firstDateInd = definedName.Range.RightColumnIndex + 1;
            int rowIndexNgay = definedName.Range.TopRowIndex - 3;

            var crIndDate = firstDateInd;
            De_Begin.DateTime = De_Begin.Properties.MinValue = De_End.Properties.MinValue = _minDate;
            De_End.DateTime = De_Begin.Properties.MaxValue = De_End.Properties.MaxValue = _maxDate;

            _dicDate.Clear();

            DataTable dtData = DataTableCreateHelper.TongHopKinhPhiTDKH();
            int offsetDt = (dtData.Columns.Count) - crIndDate;

            List<DateTime> lsNgay = new List<DateTime>();


            for (DateTime crDate = _minDate; crDate <= _maxDate; crDate = crDate.AddDays(1))
            {
                lsNgay.Add(crDate);
                ws.Rows[rowIndexNgay][crIndDate].CopyFrom(rangeDate);
                ws.Rows[rowIndexNgay][crIndDate].SetValue(crDate);
                _dicDate.Add(crDate, crIndDate);
                crIndDate += rangeDate.ColumnCount;

                string prefixDate = $"d{crDate.ToString("ddMMyyyy")}";
                dtData.Columns.Add(new DataColumn($"{prefixDate}KLKH", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}TTKH", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}KLBS", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}TTBS", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}KLTC", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}DGTC", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}TTTC", typeof(object)));
            };


            string prefixFormula = MyConstant.PrefixFormula;
            wb.Range["DuAn"].SetValue(SharedControls.slke_ThongTinDuAn.Text);
            wb.Range["DonViThucHien"].SetValue(crDVTH.TenGhep);
            wb.Range["KinhPhiPhanBoToanDuAn"].SetValue(_KinhPhiPhanBo);
            //wsData.Rows.Insert(definedNameData.Range.BottomRowIndex, numInsert, RowFormatMode.FormatAsNext);

            //Row crRowsTong = ws.Rows[definedName.Range.TopRowIndex];

            //int indCha = crRowsInd;
            var crRowInd = definedName.Range.TopRowIndex;
            int fstInd = crRowInd;
            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            //ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Visible = ce_LapLaiKeHoach.Checked;


            ws.Range["LoaiKeHoach"].Value = (ce_LapLaiKeHoach.Checked)
                    ? "Kế hoạch được thiết lập lại để phù hợp với khối lượng đã thi công"
                    : "Kê hoạch gốc";

            ws.Range["TuNgay"].SetValue(De_Begin.DateTime);
            ws.Range["DenNgay"].SetValue(De_End.DateTime);

            if (!BaseFrom.allPermission.HaveInitProjectPermission)
            {
                ws.Columns[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Visible = false;
                ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Visible = false;
                ws.Columns[dic[TDKH.COL_DonGia]].Visible = false;
            }

            //int count = 0;
            int STTCTrinh = 0;

            string forGiaTri, forNBD, forNKT, forNBDTC, forNKTTC;

            string[] colsSum = new string[]
            {
                TDKH.COL_GiaTri,
                TDKH.COL_GiaTriThiCong,
                //TDKH.COL_GiaTriNhanThau,
                //TDKH.COL_KinhPhiDuKien,
            };

            var grsCTrinh = CongTacs.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);
            //int indRowTong = definedName.Range.TopRowIndex;
            foreach (var grCTrinh in grsCTrinh)
            {
                DataRow newRowCtrinh = dtData.NewRow();
                dtData.Rows.Add(newRowCtrinh);

                var crRowCtrInd = crRowInd = fstInd + dtData.Rows.Count;

                DataRow fstCtr = grCTrinh.First();
                newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                newRowCtrinh[TDKH.COL_Code] = grCTrinh.Key;
                newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                newRowCtrinh[TDKH.COL_DanhMucCongTac] = fstCtr["TenCongTrinh"];
                newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{fstInd + 1})";

                var grsHM = grCTrinh.GroupBy(x => x["CodeHangMuc"]);
                int STTHM = 0;
                foreach (var grHM in grsHM)
                {
                    DataRow newRowHM = dtData.NewRow();
                    dtData.Rows.Add(newRowHM);

                    var crRowHMInd = crRowInd = fstInd + dtData.Rows.Count;

                    DataRow fstHM = grHM.First();
                    newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                    newRowHM[TDKH.COL_Code] = grHM.Key;
                    newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                    newRowHM[TDKH.COL_DanhMucCongTac] = fstHM["TenHangMuc"];
                    newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                    newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd + 1})";

                    var grsPhanTuyen = grHM.GroupBy(x => (int)x["IndPT"])
                        .OrderBy(x => x.Key);

                    foreach (var grPhanTuyen in grsPhanTuyen)
                    {
                        //int? crRowPTInd = null;
                        DataRow fstPT = grPhanTuyen.First();
                        string codePT = fstPT["CodePhanTuyen"].ToString();
                        bool isPhanTuyen = codePT.HasValue();
                        int? crRowPTInd = null;
                        DataRow newRowPT = null;
                        if (isPhanTuyen)
                        {
                            newRowPT = dtData.NewRow();
                            dtData.Rows.Add(newRowPT);

                            crRowPTInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                            //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                            newRowPT[TDKH.COL_Code] = codePT;
                            newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                            newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
                            newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                            newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";
                        }

                        var grsNhom = grPhanTuyen.GroupBy(x => (int)x["IndNhom"])
                                    .OrderBy(x => x.Key);

                        foreach (var grNhom in grsNhom)
                        {
                            var fstNhom = grNhom.First();
                            string codeNhom = fstNhom["CodeNhom"].ToString();
                            bool isNhom = codeNhom.HasValue();
                            int? crRowNhomInd = null;
                            DataRow newRowNhom = null;
                            DataRow drNhom = null;
                            if (isNhom)
                            {
                                drNhom = dtNhom.AsEnumerable().Single(x => x["Code"].ToString() == codeNhom);
                                newRowNhom = dtData.NewRow();
                                dtData.Rows.Add(newRowNhom);

                                crRowNhomInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                if (drNhom["GhiChuBoSungJson"] != DBNull.Value)
                                {
                                    var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(drNhom["GhiChuBoSungJson"].ToString());
                                    newRowNhom[TDKH.COL_STTDocVao] = GhiChuBoSungJson.STT;
                                    newRowNhom[TDKH.COL_STTND] = GhiChuBoSungJson.STTND;
                                }
                                newRowNhom[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_NHOM;
                                newRowNhom[TDKH.COL_DanhMucCongTac] = drNhom["Ten"];
                                newRowNhom[TDKH.COL_DonVi] = drNhom["DonVi"];
                                newRowNhom[TDKH.COL_DBC_KhoiLuongToanBo] = drNhom["KhoiLuongKeHoach"];
                                newRowNhom[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{drNhom["NgayBatDau"]}";
                                newRowNhom[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{drNhom["NgayKetThuc"]}"; //drNhom["NgayKetThuc"];
                                newRowNhom[TDKH.COL_DonGia] = drNhom["DonGia"];
                                newRowNhom[TDKH.COL_DonGiaThiCong] = drNhom["DonGiaThiCong"];
                                newRowNhom[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd) + 1})";
                                newRowNhom[TDKH.COL_Code] = drNhom["Code"];
                                newRowNhom[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                                newRowNhom[TDKH.COL_KhoiLuongKeHoachGiaiDoan] = $"{prefixFormula}{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} - {dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}";



                                newRowNhom[TDKH.COL_PhanTramThucHien] = $"{prefixFormula}IF({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} = 0;\"\";{dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}/{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1})";
                                newRowNhom[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                newRowNhom[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                newRowNhom[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                newRowNhom[TDKH.COL_KinhPhiDuKien] = $"{prefixFormula}IF({dic[TDKH.COL_GiaTri]}{fstInd + 1} = 0;\"\";{dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{dic[TDKH.COL_GiaTri]}{fstInd + 1}*{TDKH.RANGE_KinhPhiPhanBoToanDuAn})";

                                var klhnsInNhom = MyFunction.Fcn_CalKLKHNewWithoutLuyKe(TypeKLHN.Nhom, new string[] { (string)drNhom["Code"] });
                                if (klhnsInNhom.Any())
                                {
                                    newRowNhom[TDKH.COL_NgayBatDauThiCong] = klhnsInNhom.Min(x => x.Ngay);
                                    newRowNhom[TDKH.COL_NgayKetThucThiCong] = klhnsInNhom.Max(x => x.Ngay);
                                    newRowNhom[TDKH.COL_KhoiLuongDaThiCong] = klhnsInNhom.Sum(x => x.KhoiLuongThiCong);
                                    newRowNhom[TDKH.COL_GiaTriThiCong] = klhnsInNhom.Sum(x => x.ThanhTienThiCong) ?? 0;
                                }
                                newRowNhom[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;


                                foreach (DateTime crDate in lsNgay)
                                {
                                    var hn = klhnsInNhom.SingleOrDefault(x => x.Ngay == crDate);
                                    int offset = 1;
                                    var indKLKeHoach = _dicDate[crDate] + offsetDt;
                                    var indTTKeHoach = indKLKeHoach + offset++;

                                    var indKLBoSung = indKLKeHoach + offset++;
                                    var indTTBoSung = indKLKeHoach + offset++;

                                    var indKLThiCong = indKLKeHoach + offset++;
                                    var indDGThiCong = indKLKeHoach + 3;
                                    var indTTThiCong = indKLKeHoach + 4;

                                    if (hn != null)
                                    {
                                        if (hn.KhoiLuongKeHoach.HasValue)
                                        {
                                            newRowNhom[indKLKeHoach] = hn.KhoiLuongKeHoach;
                                            newRowNhom[indTTKeHoach] = $"{prefixFormula}ROUND({ws.Range.GetColumnNameByIndex(indKLKeHoach)}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                        }

                                        if (hn.KhoiLuongBoSung.HasValue)
                                        {
                                            newRowNhom[indKLBoSung] = hn.KhoiLuongBoSung;
                                            newRowNhom[indTTBoSung] = $"{prefixFormula}ROUND({ws.Range.GetColumnNameByIndex(indKLBoSung)}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                        }

                                        if (hn.KhoiLuongThiCong.HasValue)
                                        {
                                            newRowNhom[indKLThiCong] = hn.KhoiLuongThiCong;
                                            newRowNhom[indDGThiCong] = hn.DonGiaThiCong;
                                            newRowNhom[indTTThiCong] = hn.ThanhTienThiCong;
                                        }
                                    }
                                }

                            }


                            int STTCTac = 0;
                            foreach (var ctac in grNhom)
                            {

                                string tt = $"{STTCTrinh}.{STTHM}.{++STTCTac}";

                                WaitFormHelper.ShowWaitForm($"{tt}: {ctac["TenCongTac"]}", "Đang tổng hợp dữ liệu");

                                //var fstRow = grCongTac.First();
                                if (ctac["Code"] == DBNull.Value)
                                    continue;
                                DataRow newRowCTac = dtData.NewRow();
                                dtData.Rows.Add(newRowCTac);


                                int crRowCtInd = crRowInd = fstInd + dtData.Rows.Count;
                                if (ctac["Code"] == DBNull.Value)
                                    continue;
                                if (ctac["GhiChuBoSungJson"] != DBNull.Value)
                                {
                                    var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(ctac["GhiChuBoSungJson"].ToString());
                                    newRowCTac[TDKH.COL_STTDocVao] = GhiChuBoSungJson.STT;
                                    newRowCTac[TDKH.COL_STTND] = GhiChuBoSungJson.STTND;
                                }
                                newRowCTac[TDKH.COL_STT] = STTCTac;
                                newRowCTac[TDKH.COL_MaHieuCongTac] = ctac["MaHieuCongTac"];
                                newRowCTac[TDKH.COL_DanhMucCongTac] = ctac["TenCongTac"];
                                newRowCTac[TDKH.COL_DonVi] = ctac["DonVi"];
                                newRowCTac[TDKH.COL_DBC_KhoiLuongToanBo] = ctac["KhoiLuongToanBo"];
                                newRowCTac[TDKH.COL_KhoiLuongKeHoachGiaiDoan] = $"{prefixFormula}{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} - {dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}";
                                newRowCTac[TDKH.COL_KhoiLuongHopDongChiTiet] = ctac["KhoiLuongHopDongChiTiet"];

                                newRowCTac[TDKH.COL_PhanTramThucHien] = $"IF({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} = 0;0;{prefixFormula}{dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}/{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1})";
                                newRowCTac[TDKH.COL_DonGia] = ctac["DonGia"];
                                newRowCTac[TDKH.COL_DonGiaThiCong] = ctac["DonGiaThiCong"];
                                newRowCTac[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{ctac["NgayBatDau"]}"; //ctac["NgayBatDau"];
                                newRowCTac[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{ctac["NgayKetThuc"]}"; //ctac["NgayKetThuc"];
                                newRowCTac[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                newRowCTac[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                newRowCTac[TDKH.COL_TrangThai] = ctac["TrangThai"];
                                newRowCTac[TDKH.COL_NhanCong] = ctac["NhanCongReal"];
                                newRowCTac[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                newRowCTac[TDKH.COL_KinhPhiDuKien] = $"{prefixFormula}ROUND({dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{dic[TDKH.COL_GiaTri]}{fstInd + 1}*{TDKH.RANGE_KinhPhiPhanBoToanDuAn}; 0)";

                                newRowCTac[TDKH.COL_GhiChu] = ctac["GhiChu"];

                                var klhnsInCTac = MyFunction.Fcn_CalKLKHNewWithoutLuyKe(TypeKLHN.CongTac, new string[] { ctac["Code"].ToString() }); ;

                                if (klhnsInCTac.Any())
                                {
                                    newRowCTac[TDKH.COL_NgayBatDauThiCong] = klhnsInCTac.Min(x => x.Ngay);
                                    newRowCTac[TDKH.COL_NgayKetThucThiCong] = klhnsInCTac.Max(x => x.Ngay);
                                    newRowCTac[TDKH.COL_KhoiLuongDaThiCong] = klhnsInCTac.Sum(x => x.KhoiLuongThiCong);
                                    newRowCTac[TDKH.COL_GiaTriThiCong] = klhnsInCTac.Sum(x => x.ThanhTienThiCong);
                                }

                                newRowCTac[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowNhomInd ?? crRowHMInd) + 1})";
                                newRowCTac[TDKH.COL_Code] = ctac["Code"];
                                newRowCTac[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;

                                foreach (DateTime crDate in lsNgay)
                                {
                                    var hn = klhnsInCTac.SingleOrDefault(x => x.Ngay == crDate);

                                    int offset = 1;
                                    var indKLKeHoach = _dicDate[crDate] + offsetDt;
                                    var indTTKeHoach = indKLKeHoach + offset++;
                                    var indKLBoSung = indKLKeHoach + offset++;
                                    var indTTBoSung = indKLKeHoach + offset++;
                                    var indKLThiCong = indKLKeHoach + offset++;
                                    var indDGThiCong = indKLKeHoach + offset++;
                                    var indTTThiCong = indKLKeHoach + offset++;

                                    if (hn != null)
                                    {
                                        if (hn.KhoiLuongKeHoach.HasValue)
                                        {
                                            newRowCTac[indKLKeHoach] = hn.KhoiLuongKeHoach;
                                            newRowCTac[indTTKeHoach] = $"{prefixFormula}ROUND({ws.Range.GetColumnNameByIndex(indKLKeHoach)}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                        }

                                        if (hn.KhoiLuongBoSung.HasValue)
                                        {
                                            newRowCTac[indKLBoSung] = hn.KhoiLuongBoSung;
                                            newRowCTac[indTTBoSung] = $"{prefixFormula}ROUND({ws.Range.GetColumnNameByIndex(indKLKeHoach)}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                        }

                                        if (hn.KhoiLuongThiCong.HasValue)
                                        {
                                            newRowCTac[indKLThiCong] = hn.KhoiLuongThiCong;
                                            newRowCTac[indDGThiCong] = hn.DonGiaThiCong;
                                            newRowCTac[indTTThiCong] = hn.ThanhTienThiCong;
                                        }
                                    }
                                }
                            }

                            if (isNhom)
                            {
                                crRowInd = fstInd + dtData.Rows.Count;
                                TDKHHelper.GetFormulaMimMaxDate((crRowNhomInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                                TDKHHelper.GetFormulaMimMaxDate((crRowNhomInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                                //var forKPDK = GetFormulaSumChild(crRowInd + 1, dic[TDKH.COL_KinhPhiDuKien], dic[TDKH.COL_RowCha]);

                                if (drNhom["NgayBatDau"] == DBNull.Value)
                                {

                                    newRowNhom[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                                    newRowNhom[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                                    newRowNhom[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                                    newRowNhom[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";
                                }
                                if (drNhom["KhoiLuongKeHoach"] == DBNull.Value)
                                {

                                    foreach (string col in colsSum)
                                    {
                                        forGiaTri = TDKHHelper.GetFormulaSumChild((crRowNhomInd ?? 0), crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                        newRowNhom[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                    }
                                }
                            }


                        }

                        if (isPhanTuyen)
                        {
                            DataRow newRowHTPT = dtData.NewRow();
                            dtData.Rows.Add(newRowHTPT);


                            //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                            newRowHTPT[TDKH.COL_Code] = codePT;
                            newRowHTPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;

                            newRowHTPT[TDKH.COL_DanhMucCongTac] = $"{prefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                            newRowHTPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                            newRowHTPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";

                            crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                            TDKHHelper.GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                            TDKHHelper.GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                            newRowPT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                            newRowPT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                            newRowPT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                            newRowPT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                            foreach (string col in colsSum)
                            {
                                forGiaTri = TDKHHelper.GetFormulaSumChild((crRowPTInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                newRowPT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                            }
                        }
                    }

                    crRowInd = fstInd + dtData.Rows.Count;
                    TDKHHelper.GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                    TDKHHelper.GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                    newRowHM[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                    newRowHM[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                    newRowHM[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                    newRowHM[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                    foreach (string col in colsSum)
                    {
                        forGiaTri = TDKHHelper.GetFormulaSumChild(crRowHMInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                        newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                    }
                }

                crRowInd = fstInd + dtData.Rows.Count;
                TDKHHelper.GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                TDKHHelper.GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                newRowCtrinh[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                newRowCtrinh[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                newRowCtrinh[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                newRowCtrinh[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                foreach (string col in colsSum)
                {
                    forGiaTri = TDKHHelper.GetFormulaSumChild(crRowCtrInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                    newRowCtrinh[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                }
            }

            var rowTong = dtData.NewRow();
            dtData.Rows.InsertAt(rowTong, 0);


            rowTong[TDKH.COL_DanhMucCongTac] = "TỔNG";
            crRowInd = fstInd - 1 + dtData.Rows.Count;
            TDKHHelper.GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
            TDKHHelper.GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

            rowTong[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
            rowTong[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
            rowTong[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
            rowTong[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";
            rowTong[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDau]}{fstInd + 1} + 1";
            rowTong[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{fstInd + 1} + 1";

            foreach (string col in colsSum)
            {
                forGiaTri = TDKHHelper.GetFormulaSumChild(fstInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                rowTong[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
            }


            int numRow = dtData.Rows.Count;
            ws.Rows.Insert(definedName.Range.BottomRowIndex, numRow, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, fstInd, 0);
            ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;
            SpreadsheetHelper.ReplaceAllFormulaAfterImport(ws.GetUsedRange());
            wb.EndUpdate();

            try
            {
                ////          wb.History.IsEnabled = true;

            }
            catch (Exception) { }

            SpreadsheetHelper.FormatRowsInRange(definedName.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha],
    dic[TDKH.COL_Code], colMaCongTac: dic[TDKH.COL_MaHieuCongTac], codesCT: CongTacs.AsEnumerable().Select(x => x["MaHieuCongTac"].ToString()));

            //if (!ce_HienThiCong.Checked || !ce_HienKeHoach.Checked)
            checkAnHien();
            checkAnHienRow();
            dateChanged();
            wb.Worksheets.RemoveAt(1);
            WaitFormHelper.CloseWaitForm();
        }


        private void LoadAllKhoiLuongVatTuHaoPhiToSheet()
        {
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);
            if (!InitVatTu(codeCT, codeHM, out DataTable dtData, out DataTable vatTus, out List<DateTime> lsNgay, out int offsetDt))
                return;



            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];

            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            ////          wb.History.IsEnabled = false;
            var definedName = wb.DefinedNames.GetDefinedName("Data");

            wb.BeginUpdate();
            dtData.BeginLoadData();


            int crRowInd = definedName.Range.TopRowIndex;
            int rowTongInd = crRowInd;

            int STTCTrinh = 0;

            var grsCTrinh = vatTus.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);

            string forGiaTri, forNBD, forNKT, forNBDTC, forNKTTC;
            int fstInd = definedName.Range.TopRowIndex;
            string[] colsSum = new string[]
{
                TDKH.COL_GiaTri,
                TDKH.COL_GiaTriThiCong,
                //TDKH.COL_KinhPhiDuKien,
};

            foreach (var grCTrinh in grsCTrinh)
            {

                DataRow newRowCtrinh = dtData.NewRow();
                dtData.Rows.Add(newRowCtrinh);

                var crRowCtrInd = crRowInd = fstInd + dtData.Rows.Count;

                DataRow fstCtr = grCTrinh.First();
                newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                newRowCtrinh[TDKH.COL_Code] = grCTrinh.Key;
                newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                newRowCtrinh[TDKH.COL_DanhMucCongTac] = fstCtr["TenCongTrinh"];
                newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{rowTongInd + 1})";

                var grsMuiTC = grCTrinh.GroupBy(x => x["CodeMuiThiCong"].ToString())
                    .OrderBy(x => x.Key.HasValue());
                foreach (var grMuiTC in grsMuiTC)
                {
                    DataRow fstMuiTC = grMuiTC.First();
                    int? crRowMuiTCInd = null;
                    bool isMTC = grMuiTC.Key.HasValue();
                    DataRow newRowMuiTC = null;
                    if (isMTC)
                    {
                        newRowMuiTC = dtData.NewRow();
                        dtData.Rows.Add(newRowMuiTC);

                        crRowMuiTCInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                        newRowMuiTC[TDKH.COL_Code] = grMuiTC.Key;
                        newRowMuiTC[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_MuiThiCong;
                        newRowMuiTC[TDKH.COL_DanhMucCongTac] = fstMuiTC["TenMuiThiCong"];
                        newRowMuiTC[TDKH.COL_TypeRow] = MyConstant.TYPEROW_MuiThiCong;
                        newRowMuiTC[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd + 1})";
                    }

                    var grsHM = grMuiTC.GroupBy(x => x["CodeHangMuc"]);
                    int STTHM = 0;
                    int count = 0;
                    foreach (var grHM in grsHM)
                    {
                        DataRow newRowHM = dtData.NewRow();
                        dtData.Rows.Add(newRowHM);

                        var crRowHMInd = crRowInd = fstInd + dtData.Rows.Count;

                        DataRow fstHM = grHM.First();
                        newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                        newRowHM[TDKH.COL_Code] = grHM.Key;
                        newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                        newRowHM[TDKH.COL_DanhMucCongTac] = fstHM["TenHangMuc"];
                        newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                        newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowMuiTCInd ?? crRowCtrInd) + 1})";

                        var grsLoaiVT = grHM.GroupBy(x => x["LoaiVatTu"].ToString());
                        foreach (var grLoaiVT in grsLoaiVT)
                        {

                            DataRow newRowLVT = dtData.NewRow();
                            dtData.Rows.Add(newRowLVT);

                            int crRowLVTInd = fstInd + dtData.Rows.Count;
                            var fstLVT = grLoaiVT.First();

                            newRowLVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                            newRowLVT[TDKH.COL_DanhMucCongTac] = grLoaiVT.Key;
                            newRowLVT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";

                            var grsPhanTuyen = grLoaiVT.GroupBy(x => x["CodePhanTuyen"].ToString())
                                        .OrderByDescending(x => x.Key.HasValue());

                            foreach (var grPhanTuyen in grsPhanTuyen)
                            {
                                DataRow fstPT = grPhanTuyen.First();
                                int? crRowPTInd = null;
                                DataRow newRowPT = null;

                                if (grPhanTuyen.Key.HasValue())
                                {
                                    newRowPT = dtData.NewRow();
                                    dtData.Rows.Add(newRowPT);

                                    crRowPTInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                    //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                    newRowPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                    newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                                    newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
                                    newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                                    newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowLVTInd + 1})";
                                }

                                //var grsVatTu = grPhanTuyen.GroupBy(x => x["Code"]);
                                int STTVatTu = 0;
                                foreach (var fstVT in grPhanTuyen)
                                {
                                    WaitFormHelper.ShowWaitForm($"{count++}: {fstVT["VatTu"]}", "Đang tải vật liệu");

                                    DataRow newRowVT = dtData.NewRow();
                                    dtData.Rows.Add(newRowVT);

                                    int crRowVtInd = crRowInd = fstInd + dtData.Rows.Count;
                                    //var fstVT = fstVT.First();

                                    if (fstVT["Code"] == DBNull.Value && fstVT["CodeGiaoThau"] == DBNull.Value)
                                        continue;
                                        
                                    var klhnsInVatTu = MyFunction.Fcn_CalKLKHNewWithoutLuyKe(TypeKLHN.VatLieu, new string[] { fstVT["Code"].ToString() });

                                    newRowVT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{++STTVatTu}";
                                    newRowVT[TDKH.COL_Code] = fstVT["Code"];
                                    newRowVT[TDKH.COL_MaHieuCongTac] = fstVT["MaVatLieu"];
                                    newRowVT[TDKH.COL_DanhMucCongTac] = fstVT["VatTu"];
                                    newRowVT[TDKH.COL_DonVi] = fstVT["DonVi"];
                                    newRowVT[TDKH.COL_DonGia] = fstVT["DonGia"];
                                    newRowVT[TDKH.COL_DonGiaThiCong] = fstVT["DonGiaThiCong"];
                                    //newRowVTDics[TDKH.COL_DonGiaThiCong]].SetValue(fstVT.DonGiaThiCong);
                                    newRowVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                    //newRowVT[TDKH.COL_KHVT_Search] = fstVT["SearchString"];
                                    newRowVT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{fstVT["NgayBatDau"]}";// fstVT["NgayBatDau"];
                                    newRowVT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{fstVT["NgayKetThuc"]}"; //fstVT["NgayKetThuc"];
                                    newRowVT[TDKH.COL_DBC_KhoiLuongToanBo] = fstVT["KhoiLuongKeHoach"];
                                    newRowVT[TDKH.COL_KhoiLuongHopDongChiTiet] = fstVT["KhoiLuongHopDong"];
                                    newRowVT[TDKH.COL_PhanTramThucHien] = $"IF({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} = 0;0;{prefixFormula}{dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}/{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1})";
                                    newRowVT[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                    newRowVT[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";

                                    var datesThiCong = klhnsInVatTu.Where(x => x.KhoiLuongThiCong > 0).Select(x => x.Ngay);

                                    if (datesThiCong.Any())
                                    {
                                        newRowVT[TDKH.COL_NgayBatDauThiCong] = datesThiCong.Min();
                                        newRowVT[TDKH.COL_NgayKetThucThiCong] = datesThiCong.Max();
                                    }

                                    //newRowVT[TDKH.COL_KhoiLuongDaNhapKho] = klhnsInVatTu.Sum(x => x.KhoiLuongNhapKho);
                                    newRowVT[TDKH.COL_GiaTriThiCong] = klhnsInVatTu.Sum(x => x.ThanhTienThiCong);
                                    newRowVT[TDKH.COL_KhoiLuongDaThiCong] = klhnsInVatTu.Sum(x => x.KhoiLuongThiCong);

                                    //newRowVT[TDKH.COL_KHVT_MaTXHientruong] = fstVT["MaTXHienTruong"];

                                    newRowVT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd ?? crRowHMInd + 1})";


                                    foreach (DateTime crDate in lsNgay)
                                    {
                                        var hn = klhnsInVatTu.SingleOrDefault(x => x.Ngay == crDate);

                                        int offset = 1;
                                        var indKLKeHoach = _dicDate[crDate] + offsetDt;
                                        var indTTKeHoach = indKLKeHoach + offset++;
                                        var indKLBoSung = indKLKeHoach + offset++;
                                        var indTTBoSung = indKLKeHoach + offset++;
                                        var indKLThiCong = indKLKeHoach + offset++;
                                        var indDGThiCong = indKLKeHoach + offset++;
                                        var indTTThiCong = indKLKeHoach + offset++;


                                        if (hn != null)
                                        {
                                            if (hn.KhoiLuongKeHoach.HasValue)
                                            {
                                                newRowVT[indKLKeHoach] = hn.KhoiLuongKeHoach;
                                                newRowVT[indTTKeHoach] = $"{prefixFormula}ROUND({ws.Range.GetColumnNameByIndex(indKLKeHoach)}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                            }

                                            if (hn.KhoiLuongBoSung.HasValue)
                                            {
                                                newRowVT[indKLBoSung] = hn.KhoiLuongBoSung;
                                                newRowVT[indTTBoSung] = $"{prefixFormula}ROUND({ws.Range.GetColumnNameByIndex(indKLBoSung)}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                            }

                                            if (hn.KhoiLuongThiCong.HasValue)
                                            {
                                                newRowVT[indKLThiCong] = hn.KhoiLuongThiCong;
                                                newRowVT[indDGThiCong] = hn.DonGiaThiCong;
                                                newRowVT[indTTThiCong] = hn.ThanhTienThiCong;
                                            }
                                        }

                                    }


                                    /*foreach (var haoPhi in grVatTu)
                                    {
                                        WaitFormHelper.ShowWaitForm($"{count++}: {haoPhi["VatTu"]}", "Đang tải vật liệu");

                                        DataRow newRowHp = dtData.NewRow();
                                        dtData.Rows.Add(newRowHp);
                                        ++crRowInd;

                                        var klhnsInHaoPhi = _KLHNHPhis.Where(x => x.CodeCha == haoPhi["Code"].ToString());

                                        newRowHp[TDKH.COL_MaHieuCongTac] = haoPhi["MaHieuCongTac"];
                                        newRowHp[TDKH.COL_DanhMucCongTac] = haoPhi["TenCongTac"];
                                        newRowHp[TDKH.COL_Code] = haoPhi["Code"];
                                        newRowHp[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowVtInd + 1})";
                                        newRowHp[TDKH.COL_DonGiaThiCong] = haoPhi["DonGiaThiCong"];
                                        newRowHp[TDKH.COL_DonGia] = haoPhi["DonGia"];
                                        newRowHp[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCON;
                                        newRowHp[TDKH.COL_NgayBatDau] = haoPhi["NgayBatDau"];
                                        newRowHp[TDKH.COL_NgayKetThuc] = haoPhi["NgayKetThuc"];


                                        newRowHp[TDKH.COL_DBC_KhoiLuongToanBo] = haoPhi["KhoiLuongKeHoach"];
                                        newRowHp[TDKH.COL_KhoiLuongHopDongChiTiet] = haoPhi["KhoiLuongHopDong"];

                                        var thicongs = klhnsInHaoPhi.Where(x => x.KhoiLuongThiCong > 0).Select(x => x.Ngay);

                                        if (thicongs.Any())
                                        {
                                            newRowHp[TDKH.COL_NgayBatDauThiCong] = thicongs.Min();
                                            newRowHp[TDKH.COL_NgayKetThucThiCong] = thicongs.Max();
                                        }
                                        newRowHp[TDKH.COL_KhoiLuongDaThiCong] = (klhnsInHaoPhi.Sum(x => x.KhoiLuongThiCong));
                                        newRowHp[TDKH.COL_GiaTriThiCong] = (klhnsInHaoPhi.Sum(x => x.ThanhTienThiCong));


                                        //newRowHp[TDKH.COL_KHVT_Search] = haoPhi["Code"];

                                        newRowHp[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1};0)";
                                        //newRowHp[TDKH.COL_KinhPhiDuKien] =
                                        //$"{prefixFormula}ROUND({Dics[TDKH.COL_GiaTri]}{crRowInd + 1}/{TDKH.rangesTongKinhPhiVatTu[indTypeSheet]}*{TDKH.rangesKinhPhiDuKienVatTu[indTypeSheet]};0)";

                                        newRowHp[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                        newRowHp[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";

                                        for (DateTime crDate = minDate; crDate <= maxDate; crDate = crDate.AddDays(1))
                                        {
                                            var hn = klhnsInHaoPhi.SingleOrDefault(x => x.Ngay == crDate);

                                            var indKLKeHoach = _dicDate[crDate] + offsetDt;
                                            var indTTKeHoach = indKLKeHoach + 1;
                                            var indKLThiCong = indKLKeHoach + 2;
                                            var indDGThiCong = indKLKeHoach + 3;
                                            var indTTThiCong = indKLKeHoach + 4;

                                            if (hn != null)
                                            {
                                                if (hn.KhoiLuongKeHoach.HasValue)
                                                {
                                                    newRowHp[indKLKeHoach] = hn.KhoiLuongKeHoach;
                                                    newRowHp[indTTKeHoach] = hn.ThanhTienKeHoach;
                                                }

                                                if (hn.KhoiLuongThiCong.HasValue)
                                                {
                                                    newRowHp[indKLThiCong] = hn.KhoiLuongThiCong;
                                                    newRowHp[indDGThiCong] = hn.DonGiaThiCong;
                                                    newRowHp[indTTThiCong] = hn.ThanhTienThiCong;
                                                }
                                            }
                                        }
                                    }*/

                                }
                                if (grPhanTuyen.Key.HasValue())
                                {
                                    DataRow newRowHTPT = dtData.NewRow();
                                    dtData.Rows.Add(newRowHTPT);

                                    ++crRowInd;

                                    //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                    newRowHTPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                    newRowHTPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;
                                    newRowHTPT[TDKH.COL_DanhMucCongTac] = $"{prefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                                    newRowHTPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                    newRowHTPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";

                                    crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                    TDKHHelper.GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                                    TDKHHelper.GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                                    newRowPT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                                    newRowPT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                                    newRowPT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                                    newRowPT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                                    foreach (string col in colsSum)
                                    {
                                        forGiaTri = TDKHHelper.GetFormulaSumChild((crRowPTInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                        newRowPT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                    }
                                }
                            }


                            crRowInd = fstInd + dtData.Rows.Count;
                            TDKHHelper.GetFormulaMimMaxDate(crRowLVTInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                            TDKHHelper.GetFormulaMimMaxDate(crRowLVTInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                            newRowLVT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                            newRowLVT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                            newRowLVT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                            newRowLVT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                            foreach (string col in colsSum)
                            {
                                forGiaTri = TDKHHelper.GetFormulaSumChild(crRowLVTInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                newRowLVT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                            }

                        }

                        crRowInd = fstInd + dtData.Rows.Count;
                        TDKHHelper.GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        TDKHHelper.GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowHM[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowHM[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowHM[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowHM[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = TDKHHelper.GetFormulaSumChild(crRowHMInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }
                    }

                    if (isMTC)
                    {
                        crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        TDKHHelper.GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        TDKHHelper.GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowMuiTC[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowMuiTC[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowMuiTC[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowMuiTC[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = TDKHHelper.GetFormulaSumChild((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowMuiTC[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }

                    }
                }


                crRowInd = fstInd + dtData.Rows.Count;
                TDKHHelper.GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                TDKHHelper.GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                newRowCtrinh[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                newRowCtrinh[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                newRowCtrinh[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                newRowCtrinh[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                foreach (string col in colsSum)
                {
                    forGiaTri = TDKHHelper.GetFormulaSumChild(crRowCtrInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                    newRowCtrinh[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                }
            }


            var rowTong = dtData.NewRow();
            dtData.Rows.InsertAt(rowTong, 0);


            rowTong[TDKH.COL_DanhMucCongTac] = "TỔNG";
            crRowInd = fstInd - 1 + dtData.Rows.Count;
            TDKHHelper.GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
            TDKHHelper.GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

            rowTong[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
            rowTong[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
            rowTong[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
            rowTong[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";
            rowTong[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDau]}{fstInd + 1} + 1";
            rowTong[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{fstInd + 1} + 1";

            foreach (string col in colsSum)
            {
                forGiaTri = TDKHHelper.GetFormulaSumChild(fstInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                rowTong[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
            }



            dtData.EndLoadData();
            int numRow = dtData.Rows.Count;
            ws.Rows.Insert(definedName.Range.BottomRowIndex, numRow + 5, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, rowTongInd, 0);
            ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;

            SpreadsheetHelper.ReplaceAllFormulaAfterImport(ws.GetUsedRange());
            ws.Calculate();
            SpreadsheetHelper.FormatRowsInRange(definedName.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha],
                dic[TDKH.COL_Code], colMaVatTu: dic[TDKH.COL_MaHieuCongTac]);

            List<int> indSum = new List<int>();
            foreach (int ind in _dicDate.Values)
            {
                indSum.Add(ind);
                indSum.Add(ind + 1);
            }
            //DinhMucHelper.fcn_TDKH_CapNhatRowChaConTienDoKeHoach(ws.Rows[rowTongInd], 6, indSum.ToArray());

            //if (!ce_HienThiCong.Checked || !ce_HienKeHoach.Checked)
            checkAnHien();
            checkAnHienRow();

            wb.Worksheets.RemoveAt(1);
            wb.EndUpdate();
            cb_TDKH_HienCongTac_CheckedChanged(null, null);


            try
            {
                ////          wb.History.IsEnabled = true;

            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
        }


        private bool InitVatTu(string CodeCT, string CodeHM, out DataTable dtData, out DataTable vatTus, out List<DateTime> lsNgay, out int offsetDt)
        {
            dtData = DataTableCreateHelper.TongHopKinhPhiTDKH();
            vatTus = null;
            lsNgay = new List<DateTime>();
            offsetDt = 0;
            var crDVTH = ctrl_DonViThucHien.SelectedDVTH;
            if (crDVTH is null)
            {
                return false;
            }

            string condNhaThau = "";
            if (crDVTH.ColCodeFK == "CodeNhaThau")
                condNhaThau = $"OR COALESCE(vt.CodeNhaThau, vt.CodeNhaThauPhu, vt.CodeToDoi) IS NULL";


            string dbString = $"SELECT vt.*, hm.Code AS CodeHangMuc, hm.Ten AS TenHangMuc,\r\n" +
                    $"ctrinh.Code AS CodeCongTrinh, ctrinh.Ten AS TenCongTrinh, pt.Ten AS TenPhanTuyen, mui.Ten AS TenMuiThiCong,\r\n" +
                    $"vt.MaVatLieu || ';' || vt.MaTXHienTruong || ';' || vt.VatTu || ';' || vt.DonVi || ';' || vt.DonGia || ';' || vt.LoaiVatTu || ';' || vt.CodeHangMuc || ';' || vt.CodeMuiThiCong || ';' || pt.Code AS SearchString\r\n" +

                        $"FROM {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                        $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                        $"ON hm.CodeCongTrinh = ctrinh.Code\r\n" +
                        ((CodeCT.HasValue()) ? $"AND ctrinh.Code = '{CodeCT}'\r\n" : "") +
                        ((CodeHM.HasValue()) ? $"AND hm.Code = '{CodeHM}'\r\n" : "") +
                        $"LEFT JOIN {MyConstant.view_VatTuKeHoachThiCong} vt\r\n" +
                        $"ON vt.CodeHangMuc = hm.Code\r\n" +
                        $"AND (vt.{crDVTH.ColCodeFK} = '{crDVTH.Code}' {condNhaThau})\r\n" +
                        $"LEFT JOIN {TDKH.Tbl_PhanTuyen} pt\r\n" +
                        $"ON vt.CodePhanTuyen = pt.Code\r\n" +
                                        $"LEFT JOIN {TDKH.Tbl_TDKH_MuiThiCong} Mui\r\n" +
                $"ON vt.CodeMuiThiCong = Mui.Code\r\n" +

                        $"GROUP BY COALESCE(vt.Code, '')\r\n" +
                        $"ORDER BY vt.LoaiVatTu DESC, vt.VatTu ASC";


            vatTus = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var _codeVTus = vatTus.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Select(x => (string)x["Code"]).Distinct().ToList();
            //var _codeHPhis = vatTus.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Select(x => (string)x["Code"]).Distinct().ToList();

            //_KLHNVTus = MyFunction.Fcn_CalKLKHNew(TypeKLHN.VatLieu, _codeVTus);
            //_KLHNHPhis = MyFunction.Fcn_CalKLKHNew(TypeKLHN.HaoPhiVatTu, _codeHPhis, isLapLaiKeHoach: ce_LapLaiKeHoach.Checked);

            if (!_codeVTus.Any() && !_codeHPhis.Any())
            {
                MessageShower.ShowWarning("Không có công tác để tổng hợp!");
                return false;
            }

            string mindateStr = vatTus.AsEnumerable().Where(x => x["Code"] != DBNull.Value && x["NgayBatDau"] != DBNull.Value).Min(x => StringHelper.Min((string)x["NgayBatDau"], (x["NgayBatDauThiCong"] == DBNull.Value) ? (string)x["NgayBatDau"] : (string)x["NgayBatDauThiCong"]));
            string maxdateStr = vatTus.AsEnumerable().Where(x => x["Code"] != DBNull.Value && x["NgayKetThuc"] != DBNull.Value).Max(x => StringHelper.Max((string)x["NgayKetThuc"], (x["NgayKetThucThiCong"] == DBNull.Value) ? (string)x["NgayKetThuc"] : (string)x["NgayKetThucThiCong"]));

            if (!DateTime.TryParse(mindateStr, out _minDate) || !DateTime.TryParse(maxdateStr, out _maxDate))
            {
                MessageShower.ShowWarning("Không có ngày thực hiện để tổng hợp");
                return false;
            }


            De_Begin.DateTime = De_Begin.Properties.MinValue = De_End.Properties.MinValue = _minDate;
            De_End.DateTime = De_Begin.Properties.MaxValue = De_End.Properties.MaxValue = _maxDate;

            //lsNgay = _KLHNCTacs.Select(x => x.Ngay).Distinct().OrderBy(x => x).ToList();


            FileHelper.fcn_spSheetStreamDocument(spsheet_TongHop, $@"{BaseFrom.m_templatePath}\FileExcel\16.BaoCaoDongTienTDKH.xlsx");
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu");

            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];
            ////          wb.History.IsEnabled = false;
            wb.BeginUpdate();

            ws.Comments.Clear();

            var definedName = wb.DefinedNames.GetDefinedName("Data");
            var rangeDate = wb.Range["Ngay"];

            int firstDateInd = definedName.Range.RightColumnIndex + 1;
            int rowIndexNgay = definedName.Range.TopRowIndex - 3;

            var crIndDate = firstDateInd;

            offsetDt = (dtData.Columns.Count) - crIndDate;
            _dicDate.Clear();

            dtData.BeginLoadData();

            for (DateTime crDate = _minDate; crDate <= _maxDate; crDate = crDate.AddDays(1))
            {
                lsNgay.Add(crDate);
                ws.Rows[rowIndexNgay][crIndDate].CopyFrom(rangeDate);
                ws.Rows[rowIndexNgay][crIndDate].SetValue(crDate);
                _dicDate.Add(crDate, crIndDate);
                crIndDate += rangeDate.ColumnCount;

                string prefixDate = $"d{crDate.ToString("ddMMyyyy")}";
                dtData.Columns.Add(new DataColumn($"{prefixDate}KLKH", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}TTKH", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}KLBS", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}TTBS", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}KLTC", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}DGTC", typeof(object)));
                dtData.Columns.Add(new DataColumn($"{prefixDate}TTTC", typeof(object)));
            }

            dtData.EndLoadData();



            wb.Range["DuAn"].SetValue(SharedControls.slke_ThongTinDuAn.Text);
            wb.Range["LoaiKeHoach"].SetValue(rg_Filter.GetDescription());
            wb.Range["DonViThucHien"].SetValue(crDVTH.TenGhep);
            wb.Range["KinhPhiPhanBoToanDuAn"].SetValue(_KinhPhiPhanBo);
            //wb.Range["KinhPhiPhanBoVatLieu"].SetValue(_KinhPhiPhanBoVatLieu);
            //wb.Range["KinhPhiPhanBoNhanCong"].SetValue(_KinhPhiPhanBoNhanCong);
            //wb.Range["KinhPhiPhanBoMayThiCong"].SetValue(_KinhPhiPhanBoMay);
            //wsData.Rows.Insert(definedNameData.Range.BottomRowIndex, numInsert, RowFormatMode.FormatAsNext);

            //int indCha = crRowsInd;


            //var dic = TDKH.dic_TongHopKinhPhiTDKH;

            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            ws.Range["LoaiKeHoach"].Value = "Kế hoạch gốc";
            ws.Range["TuNgay"].SetValue(De_Begin.DateTime);
            ws.Range["DenNgay"].SetValue(De_End.DateTime);

            ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Visible = false;

            ws.Comments.Clear();
            wb.EndUpdate();
            WaitFormHelper.CloseWaitForm();
            return true;
        }

        private void LoadAllKhoiLuongVatTuHaoPhiToSheetWithFilter(string type)
        {
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);

            if (!InitVatTu(codeCT, codeHM, out DataTable dtData, out DataTable vatTus, out List<DateTime> lsNgay, out int offsetDt))
                return;



            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];

            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            ////          wb.History.IsEnabled = false;
            var definedName = wb.DefinedNames.GetDefinedName("Data");

            wb.BeginUpdate();
            dtData.BeginLoadData();


            int crRowInd = definedName.Range.TopRowIndex;
            int rowTongInd = crRowInd;

            int STTCTrinh = 0;

            IEnumerable<IGrouping<string, DataRow>> grsLoaiVT;
            if (type == "CongTrinh")
            {
                vatTus.AsEnumerable().ForEach(x => x["CodeHangMuc"] = DBNull.Value);
            }
            else if (type == "DuAn")
            {
                vatTus.AsEnumerable().ForEach(x => x["CodeHangMuc"] = x["CodeCongTrinh"] = DBNull.Value);
            }
            var grsCTrinh = vatTus.AsEnumerable().GroupBy(x => x["CodeCongTrinh"].ToString());

            string forGiaTri, forNBD, forNKT, forNBDTC, forNKTTC;
            int fstInd = definedName.Range.TopRowIndex;
            string[] colsSum = new string[]
{
                TDKH.COL_GiaTri,
                TDKH.COL_GiaTriThiCong,
                //TDKH.COL_KinhPhiDuKien,
};

            foreach (var grCTrinh in grsCTrinh)
            {
                int? crRowCtrInd = null;
                DataRow newRowCtrinh = null;
                if (grCTrinh.Key.HasValue())
                {
                    newRowCtrinh = dtData.NewRow();
                    dtData.Rows.Add(newRowCtrinh);

                    crRowCtrInd = crRowInd = fstInd + dtData.Rows.Count;

                    DataRow fstCtr = grCTrinh.First();
                    newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                    newRowCtrinh[TDKH.COL_Code] = grCTrinh.Key;
                    newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                    newRowCtrinh[TDKH.COL_DanhMucCongTac] = fstCtr["TenCongTrinh"];
                    newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                    newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{rowTongInd + 1})";
                }
                var grsMuiTC = grCTrinh.GroupBy(x => x["CodeMuiThiCong"].ToString())
                    .OrderBy(x => x.Key.HasValue());

                foreach (var grMuiTC in grsMuiTC)
                {
                    DataRow fstMuiTC = grMuiTC.First();
                    int? crRowMuiTCInd = null;
                    bool isMTC = grMuiTC.Key.HasValue();
                    DataRow newRowMuiTC = null;
                    if (isMTC)
                    {
                        newRowMuiTC = dtData.NewRow();
                        dtData.Rows.Add(newRowMuiTC);

                        crRowMuiTCInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                        newRowMuiTC[TDKH.COL_Code] = grMuiTC.Key;
                        newRowMuiTC[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_MuiThiCong;
                        newRowMuiTC[TDKH.COL_DanhMucCongTac] = fstMuiTC["TenMuiThiCong"];
                        newRowMuiTC[TDKH.COL_TypeRow] = MyConstant.TYPEROW_MuiThiCong;
                        newRowMuiTC[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd + 1})";
                    }
                    var grsHM = grCTrinh.GroupBy(x => x["CodeHangMuc"].ToString());
                    int STTHM = 0;
                    int count = 0;
                    foreach (var grHM in grsHM)
                    {

                        int? crRowHMInd = null;
                        DataRow newRowHM = null;
                        if (grHM.Key.HasValue())
                        {
                            newRowHM = dtData.NewRow();
                            dtData.Rows.Add(newRowHM);

                            crRowHMInd = crRowInd = fstInd + dtData.Rows.Count;

                            DataRow fstHM = grHM.First();
                            newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                            newRowHM[TDKH.COL_Code] = grHM.Key;
                            newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                            newRowHM[TDKH.COL_DanhMucCongTac] = fstHM["TenHangMuc"];
                            newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                            newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowMuiTCInd ?? crRowCtrInd ?? rowTongInd) + 1})";
                        }

                        grsLoaiVT = grHM.GroupBy(x => x["LoaiVatTu"].ToString());


                        foreach (var grLoaiVT in grsLoaiVT)
                        {

                            DataRow newRowLVT = dtData.NewRow();
                            dtData.Rows.Add(newRowLVT);

                            int crRowLVTInd = fstInd + dtData.Rows.Count;
                            var fstLVT = grLoaiVT.First();

                            newRowLVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                            newRowLVT[TDKH.COL_DanhMucCongTac] = grLoaiVT.Key;
                            newRowLVT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd ?? crRowCtrInd ?? rowTongInd + 1})";

                            var grsVatTu = grLoaiVT.Where(x => x["Code"] != DBNull.Value || x["CodeGiaoThau"] != DBNull.Value).GroupBy(x => new
                            {
                                MaVatLieu = x.Field<string>("MaVatLieu"),
                                VatTu = x.Field<string>("VatTu"),
                                DonVi = x.Field<string>("DonVi"),
                                DonGia = x.Field<long?>("DonGia"),

                            });

                            int STTVatTu = 0;
                            foreach (var grVT in grsVatTu)
                            {
                                WaitFormHelper.ShowWaitForm($"{count++}: {grVT.Key.VatTu}", "Đang tải vật liệu");

                                DataRow newRowVT = dtData.NewRow();
                                dtData.Rows.Add(newRowVT);

                                int crRowVtInd = crRowInd = fstInd + dtData.Rows.Count;
                                //var fstVT = fstVT.First();

                                var lsCodesVT = grVT.Select(x => x.Field<string>("Code"));
                                var klhnsInVatTu = MyFunction.Fcn_CalKLKHNewWithoutLuyKe(TypeKLHN.VatLieu, lsCodesVT);

                                newRowVT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{++STTVatTu}";
                                //newRowVT[TDKH.COL_Code] = fstVT["Code"];
                                newRowVT[TDKH.COL_MaHieuCongTac] = grVT.Key.MaVatLieu;
                                newRowVT[TDKH.COL_DanhMucCongTac] = grVT.Key.VatTu;
                                newRowVT[TDKH.COL_DonVi] = grVT.Key.DonVi;
                                newRowVT[TDKH.COL_DonGia] = grVT.Key.DonGia;
                                //newRowVT[TDKH.COL_DonGiaThiCong] = ;
                                //newRowVTDics[TDKH.COL_DonGiaThiCong]].SetValue(fstVT.DonGiaThiCong);
                                newRowVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                //newRowVT[TDKH.COL_KHVT_Search] = fstVT["SearchString"];

                                if (grVT.Where(x => x["NgayBatDau"] != DBNull.Value).Any())
                                {
                                    newRowVT[TDKH.COL_NgayBatDau] = grVT.Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => x["NgayBatDau"].ToString());
                                    newRowVT[TDKH.COL_NgayKetThuc] = grVT.Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => x["NgayKetThuc"].ToString());
                                }
                                newRowVT[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                newRowVT[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";

                                var datesThiCong = klhnsInVatTu.Where(x => x.KhoiLuongThiCong > 0).Select(x => x.Ngay);

                                if (datesThiCong.Any())
                                {
                                    newRowVT[TDKH.COL_NgayBatDauThiCong] = datesThiCong.Min();
                                    newRowVT[TDKH.COL_NgayKetThucThiCong] = datesThiCong.Max();
                                }

                                //newRowVT[TDKH.COL_KhoiLuongDaNhapKho] = klhnsInVatTu.Sum(x => x.KhoiLuongNhapKho);
                                newRowVT[TDKH.COL_GiaTriThiCong] = klhnsInVatTu.Sum(x => x.ThanhTienThiCong);
                                newRowVT[TDKH.COL_KhoiLuongDaThiCong] = klhnsInVatTu.Sum(x => x.KhoiLuongThiCong);

                                //newRowVT[TDKH.COL_KHVT_MaTXHientruong] = fstVT["MaTXHienTruong"];

                                newRowVT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowLVTInd + 1})";


                                foreach (DateTime crDate in lsNgay)
                                {
                                    var hn = klhnsInVatTu.Where(x => x.Ngay == crDate);

                                    int offset = 1;
                                    var indKLKeHoach = _dicDate[crDate] + offsetDt;
                                    var indTTKeHoach = indKLKeHoach + offset++;
                                    var indKLBoSung = indKLKeHoach + offset++;
                                    var indTTBoSung = indKLKeHoach + offset++;
                                    var indKLThiCong = indKLKeHoach + offset++;
                                    var indDGThiCong = indKLKeHoach + offset++;
                                    var indTTThiCong = indKLKeHoach + offset++;


                                    if (hn != null)
                                    {
                                        //if (hn.KhoiLuongKeHoach.HasValue)
                                        //{
                                        newRowVT[indKLKeHoach] = hn.Sum(x => x.KhoiLuongKeHoach);
                                        newRowVT[indTTKeHoach] = $"{prefixFormula}ROUND({ws.Range.GetColumnNameByIndex(indKLKeHoach)}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";

                                        newRowVT[indKLBoSung] = hn.Sum(x => x.KhoiLuongKeHoach);
                                        newRowVT[indTTBoSung] = $"{prefixFormula}ROUND({ws.Range.GetColumnNameByIndex(indKLBoSung)}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                        //}

                                        //if (hn.KhoiLuongThiCong.HasValue)
                                        //{
                                        newRowVT[indKLThiCong] = hn.Sum(x => x.KhoiLuongThiCong);
                                        newRowVT[indDGThiCong] = hn.Sum(x => x.DonGiaThiCong);
                                        newRowVT[indTTThiCong] = hn.Sum(x => x.ThanhTienThiCong);
                                        //}
                                    }

                                }
                            }

                            crRowInd = fstInd + dtData.Rows.Count;
                            TDKHHelper.GetFormulaMimMaxDate(crRowLVTInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                            TDKHHelper.GetFormulaMimMaxDate(crRowLVTInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                            newRowLVT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                            newRowLVT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                            newRowLVT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                            newRowLVT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                            foreach (string col in colsSum)
                            {
                                forGiaTri = TDKHHelper.GetFormulaSumChild(crRowLVTInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                newRowLVT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                            }

                        }

                        if (grHM.Key.HasValue())
                        {
                            crRowInd = fstInd + dtData.Rows.Count;
                            TDKHHelper.GetFormulaMimMaxDate((crRowHMInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                            TDKHHelper.GetFormulaMimMaxDate((crRowHMInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                            newRowHM[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                            newRowHM[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                            newRowHM[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                            newRowHM[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                            foreach (string col in colsSum)
                            {
                                forGiaTri = TDKHHelper.GetFormulaSumChild((crRowHMInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                            }
                        }
                    }

                    if (isMTC)
                    {
                        crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        TDKHHelper.GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        TDKHHelper.GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowMuiTC[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowMuiTC[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowMuiTC[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowMuiTC[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = TDKHHelper.GetFormulaSumChild((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowMuiTC[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }

                    }
                }


                if (grCTrinh.Key.HasValue())
                {
                    crRowInd = fstInd + dtData.Rows.Count;
                    TDKHHelper.GetFormulaMimMaxDate((crRowCtrInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                    TDKHHelper.GetFormulaMimMaxDate((crRowCtrInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                    newRowCtrinh[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                    newRowCtrinh[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                    newRowCtrinh[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                    newRowCtrinh[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                    foreach (string col in colsSum)
                    {
                        forGiaTri = TDKHHelper.GetFormulaSumChild((crRowCtrInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                        newRowCtrinh[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                    }
                }
            }

            var rowTong = dtData.NewRow();
            dtData.Rows.InsertAt(rowTong, 0);


            rowTong[TDKH.COL_DanhMucCongTac] = "TỔNG";
            crRowInd = fstInd - 1 + dtData.Rows.Count;
            TDKHHelper.GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
            TDKHHelper.GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

            rowTong[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
            rowTong[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
            rowTong[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
            rowTong[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";
            rowTong[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDau]}{fstInd + 1} + 1";
            rowTong[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{fstInd + 1} + 1";

            foreach (string col in colsSum)
            {
                forGiaTri = TDKHHelper.GetFormulaSumChild(fstInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                rowTong[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
            }


            dtData.EndLoadData();
            int numRow = dtData.Rows.Count;
            ws.Rows.Insert(definedName.Range.BottomRowIndex, numRow + 5, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, rowTongInd, 0);
            ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;
            wb.EndUpdate();

            SpreadsheetHelper.ReplaceAllFormulaAfterImport(ws.GetUsedRange());
            ws.Calculate();
            SpreadsheetHelper.FormatRowsInRange(definedName.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha],
                dic[TDKH.COL_Code], colMaVatTu: dic[TDKH.COL_MaHieuCongTac]);

            List<int> indSum = new List<int>();
            foreach (int ind in _dicDate.Values)
            {
                indSum.Add(ind);
                indSum.Add(ind + 1);
            }
            //DinhMucHelper.fcn_TDKH_CapNhatRowChaConTienDoKeHoach(ws.Rows[rowTongInd], 6, indSum.ToArray());

            //if (!ce_HienThiCong.Checked || !ce_HienKeHoach.Checked)
            checkAnHien();
            checkAnHienRow();

            wb.Worksheets.RemoveAt(1);
            wb.EndUpdate();
            cb_TDKH_HienCongTac_CheckedChanged(null, null);


            try
            {
                ////          wb.History.IsEnabled = true;

            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
        }


        private void ctrl_DonViThucHien_DVTHChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ce_HienThiCong_CheckedChanged(object sender, EventArgs e)
        {
            checkAnHien();
        }

        private void ce_HienKeHoach_CheckedChanged(object sender, EventArgs e)
        {
            checkAnHien();
        }

        private void checkAnHien()
        {
            int[] colsKehoach = _dicDate.Select(x => x.Value).ToArray();
            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];
            var definedNameData = wb.DefinedNames.GetDefinedName("Data");
            var range = definedNameData.Range;
            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            wb.BeginUpdate();

            bool isVisibleKLKH = ce_HienKeHoach.Checked && ce_HienKhoiLuong.Checked;
            bool isVisibleTTKH = ce_HienKeHoach.Checked && ce_HienThanhTien.Checked;
            bool isVisibleKLBS = ce_HienBoSung.Checked && ce_HienKhoiLuong.Checked;
            bool isVisibleTTBS = ce_HienBoSung.Checked && ce_HienThanhTien.Checked;
            bool isVisibleKLTC = ce_HienThiCong.Checked && ce_HienKhoiLuong.Checked;
            bool isVisibleTTTC = ce_HienThiCong.Checked && ce_HienThanhTien.Checked;
            bool isVisibleDGTC = isVisibleKLTC && isVisibleTTTC;

            foreach (var item in _dicDate)
            {
                int offset = 1;
                var indKLKeHoach = item.Value;
                var indTTKeHoach = indKLKeHoach + offset++;
                var indKLBoSung = indKLKeHoach + offset++;
                var indTTBoSung = indKLKeHoach + offset++;
                var indKLThiCong = indKLKeHoach + offset++;
                var indDGThiCong = indKLKeHoach + offset++;
                var indTTThiCong = indKLKeHoach + offset++;

                DateTime crDate = item.Key;

                if (crDate < De_Begin.DateTime.Date || crDate > De_End.DateTime.Date)
                {
                    ws.Columns[indKLKeHoach].Visible =
                    ws.Columns[indTTKeHoach].Visible =
                    ws.Columns[indKLBoSung].Visible =
                    ws.Columns[indTTBoSung].Visible =
                    ws.Columns[indKLThiCong].Visible =
                    ws.Columns[indDGThiCong].Visible =
                    ws.Columns[indTTThiCong].Visible = false;
                }
                else
                {
                    ws.Columns[indKLKeHoach].Visible = isVisibleKLKH;
                    ws.Columns[indTTKeHoach].Visible = isVisibleTTKH;

                    ws.Columns[indKLBoSung].Visible = isVisibleKLBS;
                    ws.Columns[indTTBoSung].Visible = isVisibleTTBS;

                    ws.Columns[indKLThiCong].Visible = isVisibleKLTC;
                    ws.Columns[indDGThiCong].Visible = isVisibleDGTC;
                    ws.Columns[indTTThiCong].Visible = isVisibleTTTC;
                }
            }


            if (rg_type.GetAccessibleName() == "CongTac")
            {
                ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Visible = true;
            }


            wb.EndUpdate();

            try
            {
                ////          wb.History.IsEnabled = true;

            }
            catch (Exception) { }
        }

        private void checkAnHienRow()
        {
            int[] colsKehoach = _dicDate.Select(x => x.Value).ToArray();
            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];
            var definedNameData = wb.DefinedNames.GetDefinedName("Data");
            var range = definedNameData.Range;
            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            wb.BeginUpdate();


            for (int i = definedNameData.Range.TopRowIndex + 1; i <= definedNameData.Range.BottomRowIndex; i++)
            {
                var crRow = ws.Rows[i];
                var dateBD = crRow[dic[TDKH.COL_NgayBatDau]].Value.DateTimeValue;
                var dateKT = crRow[dic[TDKH.COL_NgayKetThuc]].Value.DateTimeValue;

                if ((dateKT < De_Begin.DateTime || dateBD > De_End.DateTime))
                {
                    crRow.Visible = false;
                }
                else
                {
                    crRow.Visible = true;
                }
            }

            wb.EndUpdate();

            try
            {
                ////          wb.History.IsEnabled = true;

            }
            catch (Exception) { }
        }

        private void ReCalSumKLKH()
        {
            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];
            var definedNameData = wb.DefinedNames.GetDefinedName("Data");
            var range = definedNameData.Range;
            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            ////          wb.History.IsEnabled = false;
            wb.BeginUpdate();


            DateTime minDate, maxDate;
            minDate = De_Begin.DateTime;
            maxDate = De_End.DateTime;

            if (rg_type.GetAccessibleName() == "CongTac")
            {
                //if (ce_LapLaiKeHoach.Checked)
                //{
                //    minDate = _minDate;
                //    maxDate = _maxDate;
                //}
                //var klhnCtacsInDates = _KLHNCTacs.Where(x => x.Ngay >= minDate && x.Ngay <= maxDate);
                //var klhnNhomsInDates = _KLHNHNhoms.Where(x => x.Ngay >= minDate && x.Ngay <= maxDate);
                for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
                {
                    Row crRow = ws.Rows[i];
                    string typeRow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();

                    string code = ws.Rows[i][dic[TDKH.COL_Code]].Value.ToString();

                    double klkh = 0, kltc = 0;
                    double? klbs = 0;

                    if (typeRow != MyConstant.TYPEROW_CVCha && typeRow != MyConstant.TYPEROW_CVCON)
                        continue;

                    foreach (DateTime date in _dicDate.Keys.Where(x => x >= minDate && x <= maxDate))
                    {
                        klkh += crRow[_dicDate[date]].Value.NumericValue;
                        klbs += crRow[_dicDate[date] + 2].Value.NumericValue;
                        kltc += crRow[_dicDate[date] + 4].Value.NumericValue;
                    }
                    crRow[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].SetValue(klkh);
                    crRow[dic[TDKH.COL_KhoiLuongDaThiCong]].SetValue(kltc);

                    if (klbs == 0)
                        klbs = null;
                    crRow[dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]].SetValue(klbs);

                    //if (typeRow == MyConstant.TYPEROW_CVCha)
                    //{

                    //    var kls = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, new string[] { code }, isLapLaiKeHoach: ce_LapLaiKeHoach.Checked);
                    //    klkhs = kls.Sum(x => x.KhoiLuongKeHoach);
                    //    kltcs = kls.Sum(x => x.KhoiLuongThiCong);
                    //    crRow[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].SetValue(klkhs ?? 0);
                    //    crRow[dic[TDKH.COL_KhoiLuongDaThiCong]].SetValue(kltcs ?? 0);
                    //}
                    //else if (typeRow == MyConstant.TYPEROW_Nhom)
                    //{
                    //    var kls = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, new string[] { code }, isLapLaiKeHoach: ce_LapLaiKeHoach.Checked);

                    //    klkhs = kls.Sum(x => x.KhoiLuongKeHoach);
                    //    kltcs = kls.Sum(x => x.KhoiLuongThiCong);
                    //    crRow[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].SetValue(klkhs ?? 0);
                    //    crRow[dic[TDKH.COL_KhoiLuongDaThiCong]].SetValue(kltcs ?? 0);
                    //}
                }
            }
            else
            {
                //var klhnVTInDates = _KLHNVTus.Where(x => x.Ngay >= minDate && x.Ngay <= maxDate);
                //var klhnHPInDates = _KLHNHPhis.Where(x => x.Ngay >= minDate && x.Ngay <= maxDate);

                for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
                {
                    Row crRow = ws.Rows[i];
                    string typeRow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();
                    string code = ws.Rows[i][dic[TDKH.COL_Code]].Value.ToString();
                    double klkh = 0, kltc = 0;
                    double? klbs = 0;

                    if (typeRow != MyConstant.TYPEROW_CVCha && typeRow != MyConstant.TYPEROW_CVCON)
                        continue;

                    foreach (DateTime date in _dicDate.Keys.Where(x => x >= minDate && x <= maxDate))
                    {
                        klkh += crRow[_dicDate[date]].Value.NumericValue;
                        klbs += crRow[_dicDate[date] + 2].Value.NumericValue;
                        kltc += crRow[_dicDate[date] + 4].Value.NumericValue;
                    }
                    crRow[dic[TDKH.COL_DBC_KhoiLuongToanBo]].SetValue(klkh);
                    crRow[dic[TDKH.COL_KhoiLuongDaThiCong]].SetValue(kltc);

                    if (klbs == 0)
                        klbs = null;
                    crRow[dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]].SetValue(klbs);

                }
            }

            wb.EndUpdate();

            try
            {
                ////          wb.History.IsEnabled = true;

            }
            catch (Exception) { }
        }

        private void bt_Export_Click(object sender, EventArgs e)
        {
            var saveFileDialog = SharedControls.saveFileDialog;

            saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = $"Báo cáo tổng hợp kinh phí - {DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.xlsx";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Xuất tổng hợp kinh phí";
            var crDVTH = ctrl_DonViThucHien.SelectedDVTH;
            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Workbook wb = new Workbook();
            var ws = wb.Worksheets[0];
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ws.Name = $"Tổng hợp kinh phí";

                ws.CopyFrom(spsheet_TongHop.ActiveWorksheet);

                var range = ws.GetUsedRange();
                //for (int i = range.RightColumn
                //Index; i >= 0; i--)
                //{
                //    if (ws.Columns[i].)
                //    if (!ws.Columns[i].Visible)
                //        ws.Columns.Remove(i);
                //}
                DialogResult dialogResult = MessageShower.ShowYesNoCancelQuestionWithCustomText(
                    "Bạn có muốn xuất Full File để xem không???"
                    , "Thông báo",
                    "Xuất đầy đủ dữ liệu",
                    "Xuất bản rút gọn");


                Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
                if (dialogResult == DialogResult.No)
                {
                    ws.Columns.Remove(ws.Columns[dic[TDKH.COL_Code]].Index + 1, ws.GetUsedRange().RightColumnIndex);
                    ws.Columns.Remove(dic[TDKH.COL_TypeRow]);
                    ws.Columns.Remove(dic[TDKH.COL_KinhPhiDuKienIsCongThucMacDinh]);
                    ws.Columns.Remove(dic[TDKH.COL_NhanCongIsCongThucMacDinh]);
                    ws.Columns.Remove(dic[TDKH.COL_LyTrinhCaoDo]);
                    ws.Columns.Remove(dic[TDKH.COL_GiaTriThiCong]);
                    ws.Columns.Remove(dic[TDKH.COL_GiaTri]);
                    ws.Columns.Remove(dic[TDKH.COL_KinhPhiPhanBoDuKien]);
                    ws.Columns.Remove(dic[TDKH.COL_NhanCong]);
                    ws.Columns.Remove(dic[TDKH.COL_TrangThai]);
                    ws.Columns.Remove(dic[TDKH.COL_SoNgayThiCong]);
                    ws.Columns.Remove(dic[TDKH.COL_NgayKetThucThiCong]);
                    ws.Columns.Remove(dic[TDKH.COL_NgayBatDauThiCong]);
                    ws.Columns.Remove(dic[TDKH.COL_SoNgayThucHien]);
                    ws.Columns.Remove(dic[TDKH.COL_NgayKetThuc]);
                    ws.Columns.Remove(dic[TDKH.COL_NgayBatDau]);
                    ws.Columns.Remove(dic[TDKH.COL_DonGiaThiCong]);
                    ws.Columns.Remove(dic[TDKH.COL_DonGia]);
                    ws.Columns.Remove(dic[TDKH.COL_PhanTramThucHien]);
                    ws.Columns.Remove(dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]);

                }
                else
                {
                    ws.Columns.Remove(dic[TDKH.COL_TypeRow]);
                    ws.Columns.Remove(dic[TDKH.COL_KinhPhiDuKienIsCongThucMacDinh]);
                    ws.Columns.Remove(dic[TDKH.COL_NhanCongIsCongThucMacDinh]);
                }
                wb.SaveDocument(saveFileDialog.FileName, DocumentFormat.Xlsx);
                wb.Dispose();
                dialogResult = MessageShower.ShowYesNoQuestion("File lưu thành công. Bạn có muốn mở file luôn hay không ???", "Thông báo");
                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
        }

        private void rg_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void spsheet_TongHop_ActiveSheetChanged(object sender, ActiveSheetChangedEventArgs e)
        {

        }

        private void cb_TDKH_HienCongTac_CheckedChanged(object sender, EventArgs e)
        {
            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            TDKHHelper.CheckAnHienCongTacVatTu(spsheet_TongHop.ActiveWorksheet, cb_TDKH_HienCongTac.Checked, dic);
        }

        private void De_Begin_EditValueChanged(object sender, EventArgs e)
        {
            dateChanged();
        }

        private void dateChanged()
        {
            var ws = spsheet_TongHop.ActiveWorksheet;
            ws.Range["TuNgay"].SetValue(De_Begin.DateTime);
            ws.Range["DenNgay"].SetValue(De_End.DateTime);
            checkAnHien();
            checkAnHienRow();
            ReCalSumKLKH();

        }

        //private void CapNhatRangeNgay()
        //{
        //    var dateBD = De_Begin.DateTime.Date;
        //    var dateKT = De_End.DateTime.Date;
        //    var wb = spsheet_TongHop.Document;
        //    var ws = spsheet_TongHop.ActiveWorksheet;

        //    wb.BeginUpdate();
        //    WaitFormHelper.ShowWaitForm("Đang cập nhật");
        //    foreach (var item in dicDate)
        //    {
        //        var isVisible = (item.Key >= dateBD && item.Key <= dateKT);
        //        ws.Columns[item.Value].Visible =
        //        ws.Columns[item.Value + 1].Visible = isVisible;
        //    }
        //    WaitFormHelper.CloseWaitForm();
        //    wb.EndUpdate();
        //}

        private void De_End_EditValueChanged(object sender, EventArgs e)
        {
            dateChanged();
        }

        private void ce_HienKhoiLuong_CheckedChanged(object sender, EventArgs e)
        {
            checkAnHien();
        }

        private void ce_HienThanhTien_CheckedChanged(object sender, EventArgs e)
        {
            checkAnHien();

        }

        private void spsheet_TongHop_DocumentLoaded(object sender, EventArgs e)
        {
            spsheet_TongHop.Document.History.IsEnabled = false;
        }

        private void ce_LapLaiKeHoach_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_LapLaiKeHoach.Checked)
            {
                gr_LapLaiKeHoach.Enabled = true;
                if (De_Begin.DateTime < DateTime.Now.Date)
                    De_Begin.DateTime = DateTime.Now.Date;

                if (!ce_TinhKeHoach.Checked && !ce_TinhBoSung.Checked)
                    bt_Tinh_Click(null, null);



            }
            else
            {
                gr_LapLaiKeHoach.Enabled = false;

                LoadData();
            }


            /*var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];

////          wb.History.IsEnabled = false;
            wb.BeginUpdate();

            ws.Range["LoaiKeHoach"].Value = (ce_LapLaiKeHoach.Checked) 
                ? "Kế hoạch được thiết lập lại để phù hợp với khối lượng đã thi công"
                : "Kê hoạch gốc";

            Color clText = (ce_LapLaiKeHoach.Checked) ? Color.Red : Color.Black;

            var dic = TDKH.dic_TongHopKinhPhiTDKH;
            var colCode = ws.Columns[dic[TDKH.COL_Code]];
            var range = ws.Range["Data"];
            Dictionary<string, int> dicCode = new Dictionary<string, int>();

            for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
            {
                string code = ws.Rows[i][dic[TDKH.COL_Code]].Value.ToString();
                string typeRow = ws.Rows[i][dic[TDKH.COL_TypeRow]].Value.ToString();
                if (code.HasValue() && (typeRow == MyConstant.TYPEROW_CVCha || typeRow == MyConstant.TYPEROW_CVCON))
                {
                    dicCode.Add(code, i);
                }
            }

            List<List<KLHN>> lslsKLHN = new List<List<KLHN>>();
            if (rg_type.GetAccessibleName() == "CongTac")
            {
                _KLHNCTacs = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, _codeCTacs, isLapLaiKeHoach: ce_LapLaiKeHoach.Checked);
                lslsKLHN = new List<List<KLHN>>() { _KLHNCTacs.Where(x => x.Ngay > DateTime.Now.Date).ToList() };
            }
            else
            {
                _KLHNVTus = MyFunction.Fcn_CalKLKHNew(TypeKLHN.VatLieu, _codeVTus, isLapLaiKeHoach: ce_LapLaiKeHoach.Checked);
                _KLHNHPhis = MyFunction.Fcn_CalKLKHNew(TypeKLHN.HaoPhiVatTu, _codeHPhis, isLapLaiKeHoach: ce_LapLaiKeHoach.Checked);
                lslsKLHN = new List<List<KLHN>>() 
                { 
                    _KLHNVTus.Where(x => x.Ngay > DateTime.Now.Date).ToList(), 
                    _KLHNHPhis.Where(x => x.Ngay > DateTime.Now.Date).ToList() 
                };
            }

            foreach (List<KLHN> klhns in lslsKLHN )
            {
                //var klhns = klhnsFull.Where(x => x.Ngay > DateTime.Now.Date).ToList();
                var grsCongTac = klhns.GroupBy(x => x.CodeCha);
                
                foreach (var grCTac in grsCongTac)
                {
                    string code = grCTac.Key;
                    if (!dicCode.ContainsKey(code))
                    {
                        AlertShower.ShowInfo("Lỗi group: " + code);
                        continue;
                    }
                    int ind = dicCode[code];

                    var crRow = ws.Rows[ind];

                    foreach (var hn in grCTac)
                    {
                        crRow[_dicDate[hn.Ngay]].SetValue(hn.KhoiLuongKeHoach);
                        crRow[_dicDate[hn.Ngay] + 1].SetValue(hn.ThanhTienKeHoach);

                        crRow[_dicDate[hn.Ngay]].Font.Color =
                        crRow[_dicDate[hn.Ngay]].Font.Color = clText;
                    }
                }
            }
            ReCalSumKLKH();


            wb.EndUpdate();
////          wb.History.IsEnabled = true;*/
        }


        private List<int> ValidInds = new List<int>();

        private void bt_Tinh_Click(object sender, EventArgs e)
        {
            ValidInds.Clear();
            if (!ce_TinhKeHoach.Checked && !ce_TinhBoSung.Checked)
            {
                MessageBox.Show("Vui lòng chọn tính \"TÍNH KẾ HOẠCH\", \"TÍNH BỔ SUNG\" Hoặc chọn cả 2 trước khi tính");
                return;
            }
            var ws = spsheet_TongHop.ActiveWorksheet;
            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            ws.Workbook.BeginUpdate();
            var range = ws.Range["Data"];


            for (int ind = range.TopRowIndex; ind <= range.BottomRowIndex; ind++)
            {
                var crRow = ws.Rows[ind];
                if (!crRow.Visible)
                    continue;

                int indCha = (int)crRow[dic[TDKH.COL_RowCha]].Value.NumericValue - 1;
                string crTyperow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();
                string crCode = crRow[dic[TDKH.COL_Code]].Value.ToString();


                if (crTyperow != MyConstant.TYPEROW_CVCha && crTyperow != MyConstant.TYPEROW_Nhom)
                    continue;

                if (crTyperow == MyConstant.TYPEROW_CVCha)
                {
                    string parentTyperow = ws.Rows[indCha][dic[TDKH.COL_TypeRow]].Value.ToString();
                    string parentCode = ws.Rows[indCha][dic[TDKH.COL_Code]].Value.ToString();

                    if ((_codeNhomsHasKL.Contains(parentCode) && parentTyperow == MyConstant.TYPEROW_Nhom))
                    {
                        continue;
                    }

                }

                if ((!_codeNhomsHasKL.Contains(crCode) && crTyperow == MyConstant.TYPEROW_Nhom))
                {
                    continue;
                }


                var isKLKH = double.TryParse(crRow[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Value.ToString(), out double KLKH);
                var isKLBS = double.TryParse(crRow[dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]].Value.ToString(), out double KLBS);

                if (!isKLKH && ce_TinhKeHoach.Checked)
                    continue;
                else if (!isKLBS && ce_TinhBoSung.Checked)
                    continue;

                DateTime minDate = De_Begin.DateTime.Date;
                DateTime maxDate = De_End.DateTime.Date;

                if (DateTime.TryParse(crRow[dic[TDKH.COL_NgayBatDau]].Value.ToString(), out DateTime dateBDInSheet) && dateBDInSheet > minDate)
                {
                    minDate = dateBDInSheet;
                }

                if (DateTime.TryParse(crRow[dic[TDKH.COL_NgayKetThuc]].Value.ToString(), out DateTime dateKTInSheet) && dateKTInSheet < maxDate)
                {
                    maxDate = dateKTInSheet;
                }
                int soNgay = (maxDate - minDate).Days + 1;
                if (soNgay < 0)
                {
                    continue;
                }

                ValidInds.Add(ind);

                double? KlPerDate = KLKH / soNgay;
                double? klbsNgay = KLBS / soNgay;

                var KLCL = KLKH - KlPerDate * (soNgay - 1);

                if (klbsNgay <= 0)
                    klbsNgay = null;

                var KLCLBS = KLBS - klbsNgay * (soNgay - 1);
                var lastDate = _dicDate.Keys.LastOrDefault();
                foreach (DateTime date in _dicDate.Keys)
                {
                    if (date == lastDate)
                    {
                        if (ce_TinhKeHoach.Checked)
                            crRow[_dicDate[date]].SetValue(KLCL);

                        if (ce_TinhBoSung.Checked)
                            crRow[_dicDate[date] + 2].SetValue(klbsNgay);
                        continue;
                    }
                    if (date >= minDate && date <= maxDate)
                    {
                        if (ce_TinhKeHoach.Checked)
                            crRow[_dicDate[date]].SetValue(KlPerDate);

                        if (ce_TinhBoSung.Checked)
                            crRow[_dicDate[date] + 2].SetValue(klbsNgay);

                    }
                    //else
                    //    crRow[_dicDate[date]].Clear();
                }

            }
            ws.Workbook.EndUpdate();
            //checkAnHien();
            MessageShower.ShowInformation("Đã tính xong");
        }

        private void bt_LuuLaiKhoiLuong_Click(object sender, EventArgs e)
        {
            var ws = spsheet_TongHop.ActiveWorksheet;
            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            WaitFormHelper.ShowWaitForm("Đang cập nhật");
            if (rg_type.GetAccessibleName() == "CongTac")
            {

                string dbString = $"SELECT * " +
                    $"FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
                    $"WHERE CodeCongTacTheoGiaiDoan IN ({MyFunction.fcn_Array2listQueryCondition(_codeCTacs)})";
                DataTable dthnCttk = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


                dbString = $"SELECT * " +
                    $"FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
                    $"WHERE CodeNhom IN ({MyFunction.fcn_Array2listQueryCondition(_codeNhomsHasKL)})";
                DataTable dthnNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                var inds = ws.Columns[dic[TDKH.COL_TypeRow]]
                    .Search(MyConstant.TYPEROW_CVCha, MyConstant.MySearchOptions).Select(x => x.RowIndex);

                foreach (int ind in inds)
                {
                    if (!ValidInds.Contains(ind))
                    {
                        continue;
                    }

                    var crRow = ws.Rows[ind];

                    //int indCha = (int)crRow[dic[TDKH.COL_RowCha]].Value.NumericValue - 1;
                    //string parentTyperow = ws.Rows[indCha][dic[TDKH.COL_TypeRow]].Value.ToString();
                    //string parentCode = ws.Rows[indCha][dic[TDKH.COL_Code]].Value.ToString();


                    string code = crRow[dic[TDKH.COL_Code]].Value.ToString();
                    if (!_codeCTacs.Contains(code))
                    {
                        //crRow.Visible = false;
                        continue;
                    }


                    DateTime minDate = De_Begin.DateTime.Date;
                    DateTime maxDate = De_End.DateTime.Date;

                    if (DateTime.TryParse(crRow[dic[TDKH.COL_NgayBatDau]].Value.ToString(), out DateTime dateBDInSheet) && dateBDInSheet > minDate)
                    {
                        minDate = dateBDInSheet;
                    }

                    if (DateTime.TryParse(crRow[dic[TDKH.COL_NgayKetThuc]].Value.ToString(), out DateTime dateKTInSheet) && dateKTInSheet < maxDate)
                    {
                        maxDate = dateKTInSheet;
                    }
                    int soNgay = (maxDate - minDate).Days + 1;


                    var drs = dthnCttk.AsEnumerable().Where(x => x["CodeCongTacTheoGiaiDoan"].ToString() == code);

                    foreach (DateTime crDate in _dicDate.Keys.Where(x => x >= minDate && x <= maxDate))
                    {
                        string dateStr = crDate.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        var kh = crRow[_dicDate[crDate]].Value;
                        var bs = crRow[_dicDate[crDate] + 2].Value;

                        double? KLKH = null, KLBS = null, KLTC = null;

                        if (kh.ToString().HasValue())
                        {
                            KLKH = kh.NumericValue;
                        }

                        if (bs.ToString().HasValue())
                        {
                            KLBS = bs.NumericValue;
                        }



                        var drKL = drs.SingleOrDefault(x => x["Ngay"].ToString() == dateStr);

                        if (drKL is null)
                        {
                            if (ce_TinhBoSung.Checked && KLBS is null)
                                continue;

                            else if (ce_TinhKeHoach.Checked && KLKH is null)
                                continue;

                            drKL = dthnCttk.NewRow();
                            dthnCttk.Rows.Add(drKL);
                            drKL["Code"] = Guid.NewGuid().ToString();
                            drKL["CodeCongTacTheoGiaiDoan"] = code;
                            drKL["Ngay"] = dateStr;
                        }

                        if (KLKH is null)
                            drKL["KhoiLuongKeHoach"] = DBNull.Value;
                        drKL["KhoiLuongKeHoach"] = KLKH;

                        if (KLBS is null)
                            drKL["KhoiLuongBoSung"] = DBNull.Value;
                        else
                            drKL["KhoiLuongBoSung"] = KLBS;
                    }

                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnCttk, TDKH.TBL_KhoiLuongCongViecHangNgay);
                //DoBocChuanHelper.ReCalcKlhnVatTuByDonViThucHien(ctrl_DonViThucHien.SelectedDVTH);


                var indNhoms = ws.Columns[dic[TDKH.COL_TypeRow]]
                            .Search(MyConstant.TYPEROW_Nhom, MyConstant.MySearchOptions).Select(x => x.RowIndex);
                var validCodesNhom = new List<string>();
                foreach (int indNhom in indNhoms)
                {
                    if (!ValidInds.Contains(indNhom))
                    {
                        continue;
                    }
                    var crRow = ws.Rows[indNhom];

                    int indCha = (int)crRow[dic[TDKH.COL_RowCha]].Value.NumericValue - 1;

                    string code = crRow[dic[TDKH.COL_Code]].Value.ToString();

                    if (!_codeNhomsHasKL.Contains(code))
                        continue;

                    validCodesNhom.Add(code);
                    DateTime minDate = De_Begin.DateTime.Date;
                    DateTime maxDate = De_End.DateTime.Date;

                    if (DateTime.TryParse(crRow[dic[TDKH.COL_NgayBatDau]].Value.ToString(), out DateTime dateBDInSheet) && dateBDInSheet > minDate)
                    {
                        minDate = dateBDInSheet;
                    }

                    if (DateTime.TryParse(crRow[dic[TDKH.COL_NgayKetThuc]].Value.ToString(), out DateTime dateKTInSheet) && dateKTInSheet < maxDate)
                    {
                        maxDate = dateKTInSheet;
                    }
                    int soNgay = (maxDate - minDate).Days + 1;

                    var drs = dthnNhom.AsEnumerable().Where(x => x["CodeNhom"].ToString() == code);

                    foreach (DateTime crDate in _dicDate.Keys.Where(x => x >= minDate && x <= maxDate))
                    {
                        string dateStr = crDate.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        var KLKH = crRow[_dicDate[crDate]].Value.NumericValue;
                        double? KLBS = crRow[_dicDate[crDate]].Value.NumericValue;

                        if (KLBS <= 0)
                            KLBS = null;

                        var drKL = drs.SingleOrDefault(x => x["Ngay"].ToString() == dateStr);

                        if (drKL is null)
                        {
                            if (!ce_TinhKeHoach.Checked && KLBS is null)
                                continue;
                            drKL = dthnNhom.NewRow();
                            dthnNhom.Rows.Add(drKL);
                            drKL["Code"] = Guid.NewGuid().ToString();
                            drKL["CodeNhom"] = code;
                            drKL["Ngay"] = dateStr;
                        }

                        drKL["KhoiLuongKeHoach"] = KLKH;
                        if (KLBS is null)
                            drKL["KhoiLuongBoSung"] = DBNull.Value;
                        else
                            drKL["KhoiLuongBoSung"] = KLBS;
                    }

                }

                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnNhom, TDKH.TBL_KhoiLuongCongViecHangNgay);



            }
            WaitFormHelper.CloseWaitForm();
        }

        private void spsheet_TongHop_CellBeginEdit(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellCancelEventArgs e)
        {
            isPasting = false;

            if (rg_type.GetAccessibleName() == "CongTac" && !ce_LapLaiKeHoach.Checked)
            {
                e.Cancel = true;
                MessageShower.ShowWarning("Vui lòng chọn lập lại kế hoạch để thao tác");
                return;
            }

            e.Cancel = !checkEnableChange(e.Cell);
        }

        private bool checkEnableChange(Cell e, bool ShowMessageBox = true)
        {
            Worksheet ws = spsheet_TongHop.ActiveWorksheet;
            //var dic = TDKH.dic_TongHopKinhPhiTDKH;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            if (!ce_LapLaiKeHoach.Checked)
            {
                return false;

            }
            var colName = ws.Range.GetColumnNameByIndex(e.ColumnIndex);
            if (colName != dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan] && colName != dic[TDKH.COL_KhoiLuongBoSungGiaiDoan])
            {
                return false;

            }

            if ((!ce_TinhKeHoach.Checked && colName == dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan])
                || (!ce_TinhBoSung.Checked && colName == dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]))
            {
                return false;

            }

            string typerow = ws.Rows[e.RowIndex][dic[TDKH.COL_TypeRow]].Value.ToString();

            if (typerow != MyConstant.TYPEROW_CVCha && typerow != MyConstant.TYPEROW_Nhom)
            {
                return false;

            }

            string code = ws.Rows[e.RowIndex][dic[TDKH.COL_Code]].Value.ToString();
            if (typerow == MyConstant.TYPEROW_Nhom && !_codeNhomsHasKL.Contains(code))
            {

                if (ShowMessageBox)
                    MessageShower.ShowWarning("Nhóm này không được phân khối lượng để tính toán");
                else
                    AlertShower.ShowInfo("Nhóm này không được phân khối lượng để tính toán");
                return false;
            }

            if (typerow == MyConstant.TYPEROW_CVCha)
            {


                var crRow = ws.Rows[e.RowIndex];

                int indCha = (int)crRow[dic[TDKH.COL_RowCha]].Value.NumericValue - 1;
                string parentTyperow = ws.Rows[indCha][dic[TDKH.COL_TypeRow]].Value.ToString();
                string parentCode = ws.Rows[indCha][dic[TDKH.COL_Code]].Value.ToString();


                if (_codeNhomsHasKL.Contains(parentCode) && parentTyperow == MyConstant.TYPEROW_Nhom)
                {
                    if (ShowMessageBox)
                        MessageShower.ShowWarning("Công tác này được chia theo nhóm");
                    else
                        AlertShower.ShowInfo("Công tác này được chia theo nhóm");

                    return false;

                }
            }

            return true;
        }

        private void gr_LapLaiKeHoach_Hidden(object sender, EventArgs e)
        {
            lci_filter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        private void gr_LapLaiKeHoach_Shown(object sender, EventArgs e)
        {
            lci_filter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void rg_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVatTu();
        }

        private void bt_UpdateCongThuc_Click(object sender, EventArgs e)
        {

        }

        private void ce_TinhBoSung_CheckedChanged(object sender, EventArgs e)
        {
            ReCalcConditionChanged();
        }
        private void ReCalcConditionChanged()
        {
            if (!ce_TinhBoSung.Checked && !ce_TinhKeHoach.Checked)
            {
                bt_Tinh.Enabled = bt_LuuLaiKhoiLuong.Enabled = false;
            }
            else
                bt_Tinh.Enabled = bt_LuuLaiKhoiLuong.Enabled = true;

        }
        private void ce_TinhKeHoach_CheckedChanged(object sender, EventArgs e)
        {
            ReCalcConditionChanged();
        }

        private void ce_HienBoSung_CheckedChanged(object sender, EventArgs e)
        {
            checkAnHien();
        }

        private void sb_ReadExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = SharedControls.openFileDialog;
            openFileDialog.DefaultExt = "xls";
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Chọn file Excel";
            DialogResult rs = openFileDialog.ShowDialog();
            if (rs == DialogResult.OK)
            {
                SpreadsheetControl Spread = new SpreadsheetControl();
                Spread.LoadDocument(openFileDialog.FileName);
                if (!Spread.Document.Worksheets.Select(x => x.Name).ToList().Contains("Tổng hợp kinh phí"))
                {
                    MessageShower.ShowError("Mẫu đọc vào không phải mẫu Tổng hợp kinh phí của hệ thống, Vui lòng chọn lại Mẫu khác!!!!");
                    return;
                }
                Worksheet ws = Spread.Document.Worksheets["Tổng hợp kinh phí"];
                if (!ws.DefinedNames.Contains("Data"))
                {
                    MessageShower.ShowError("Mẫu đọc vào đã được chỉnh sửa, Vui lòng chọn lại Mẫu khác!!!!");
                    return;
                }
                WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu vào!!!!!", "Vui Lòng chờ!");
                Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range["Data"]);
                Dictionary<KeyValuePair<string, int>, double> dic_In = new Dictionary<KeyValuePair<string, int>, double>();
                CellRange Data = ws.Range["Data"];
                for (int i = Data.TopRowIndex; i <= Data.BottomRowIndex; i++)
                {
                    Row Crow = ws.Rows[i];
                    string MCT = Crow[Name["MaHieuCongTac"]].Value.TextValue;
                    if (MCT == MyConstant.TYPEROW_HangMuc || MCT == MyConstant.TYPEROW_CongTrinh
                        || MCT == MyConstant.CONST_TYPE_NHOM || MCT == MyConstant.CONST_TYPE_PhanTuyen || MCT == MyConstant.CONST_TYPE_HoanThanhPhanTuyen)
                        continue;
                    string Code = Crow[Name["Code"]].Value.TextValue;
                    if (string.IsNullOrEmpty(Code))
                        continue;
                    dic_In.Add(new KeyValuePair<string, int>(Code, i), Crow[Name["KhoiLuongKeHoachGiaiDoan"]].Value.NumericValue);
                }
                Worksheet wsIn = spsheet_TongHop.Document.Worksheets[0];
                Data = wsIn.Range["Data"];
                Name = MyFunction.fcn_getDicOfColumn(wsIn.Range["Data"]);
                spsheet_TongHop.BeginUpdate();
                for (int i = Data.TopRowIndex; i <= Data.BottomRowIndex; i++)
                {
                    Row Crow = wsIn.Rows[i];
                    string MCT = Crow[Name["MaHieuCongTac"]].Value.TextValue;
                    if (MCT == MyConstant.TYPEROW_HangMuc || MCT == MyConstant.TYPEROW_CongTrinh
                        || MCT == MyConstant.CONST_TYPE_NHOM || MCT == MyConstant.CONST_TYPE_PhanTuyen || MCT == MyConstant.CONST_TYPE_HoanThanhPhanTuyen)
                        continue;
                    string Code = Crow[Name["Code"]].Value.TextValue;
                    if (string.IsNullOrEmpty(Code))
                        continue;
                    KeyValuePair<KeyValuePair<string, int>, double> dicOut = dic_In.Where(x => x.Key.Key == Code).SingleOrDefault();
                    Crow[Name["KhoiLuongKeHoachGiaiDoan"]].SetValue(dicOut.Value);
                }
                spsheet_TongHop.EndUpdate();
            }
            WaitFormHelper.CloseWaitForm();
            MessageShower.ShowInformation("Đọc dữ liệu hoàn tất!!!!!!!!!!!!");

        }

        private void spsheet_TongHop_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            SpreadsheetMenuItem MuiTC = new SpreadsheetMenuItem("XÓA TOÀN BỘ CỘT KHỐI LƯỢNG!", fcn_Handle_XoaToanBoKhoiLuong);
            MuiTC.Appearance.ForeColor = Color.Blue;
            MuiTC.Appearance.Font = new Font(MuiTC.Appearance.Font, FontStyle.Bold);
            e.Menu.Items.Add(MuiTC);

            SpreadsheetMenuItem MuiTC1 = new SpreadsheetMenuItem("XÓA TOÀN BỘ CỘT KHỐI LƯỢNG NGOÀI VÙNG CHỌN!", fcn_Handle_XoaToanBoKhoiLuongNgaoi);
            MuiTC1.Appearance.ForeColor = Color.Blue;
            MuiTC1.Appearance.Font = new Font(MuiTC1.Appearance.Font, FontStyle.Bold);
            e.Menu.Items.Add(MuiTC1);
        }

        private void fcn_Handle_XoaToanBoKhoiLuong(object sender, EventArgs e)
        {
            var dr = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa toàn bộ cột khối lượng giai đoạn không?");
            if (dr != DialogResult.Yes)
                return;

            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];
            var range = ws.Range["Data"];
            var dic = MyFunction.fcn_getDicOfColumn(range);
            var indKL = ws.Range.GetColumnIndexByName(dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]);

            var KLRange = ws.Range.FromLTRB(indKL, range.TopRowIndex + 1, indKL, range.BottomRowIndex);
            KLRange.Clear();
        }

        private void fcn_Handle_XoaToanBoKhoiLuongNgaoi(object sender, EventArgs e)
        {
            var dr = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa toàn bộ cột khối lượng giai đoạn NGOÀI VÙNG CHỌN không?");
            if (dr != DialogResult.Yes)
                return;

            var wb = spsheet_TongHop.Document;
            var ws = wb.Worksheets[0];
            var range = ws.Range["Data"];
            var dic = MyFunction.fcn_getDicOfColumn(range);
            var indKL = ws.Range.GetColumnIndexByName(dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]);

            var selectedRange = ws.GetSelectedRanges().First();
            var SelectedTopInd = selectedRange.TopRowIndex;
            var SelectedBotInd = selectedRange.BottomRowIndex;

            if (range.TopRowIndex + 1 < SelectedTopInd)
            {
                var KLRange = ws.Range.FromLTRB(indKL, range.TopRowIndex + 1, indKL, SelectedTopInd - 1);
                KLRange.Clear();

            }

            if (range.BottomRowIndex > SelectedBotInd)
            {
                var KLRange = ws.Range.FromLTRB(indKL, SelectedBotInd + 1, indKL, range.BottomRowIndex);
                KLRange.Clear();
            }

        }

        bool isPasting = false;
        private void spsheet_TongHop_CopiedRangePasting(object sender, CopiedRangePastingEventArgs e)
        {
            if (rg_type.GetAccessibleName() == "CongTac" && !ce_LapLaiKeHoach.Checked)
            {
                e.Cancel = true;
                MessageShower.ShowWarning("Vui lòng chọn lập lại kế hoạch để thao tác");
                return;
            }
            isPasting = true;
        }

        private void spsheet_TongHop_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            if (!checkEnableChange(e.Cell, false))
                MyFunction.fcn_ReverseCell(e);
        }

        private void spsheet_TongHop_ClipboardDataPasting(object sender, EventArgs e)
        {


        }

        private void spsheet_TongHop_Click(object sender, EventArgs e)
        {

        }

        private void spsheet_TongHop_ClipboardDataObtained(object sender, ClipboardDataObtainedEventArgs e)
        {
            if (rg_type.GetAccessibleName() == "CongTac" && !ce_LapLaiKeHoach.Checked)
            {
                e.Cancel = true;
                MessageShower.ShowWarning("Vui lòng chọn lập lại kế hoạch để thao tác");
                return;
            }
            isPasting = true;
        }

        private void XtraForm_BaoCaoDongTienTDKH_FormClosed(object sender, FormClosedEventArgs e)
        {
            spsheet_TongHop.Dispose();
            GC.Collect();
        }
    }
}
