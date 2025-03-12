using Dapper;
using DevExpress.XtraEditors;

using DevExpress.XtraSpreadsheet.Model.CopyOperation;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
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
using PhanMemQuanLyThiCong.Common.Constant;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Pkcs;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Spreadsheet;
using DevExpress.Utils.Menu;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Model;
using Newtonsoft.Json;
using StackExchange.Profiling.Internal;
using DevExpress.XtraRichEdit.API.Native;
using log4net;
using PM360.Common.Helper;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars.Navigation;
using DevExpress.Data.Linq.Helpers;
using DevExpress.CodeParser;
using DevExpress.Internal.WinApi;
//using DevExpress.ClipboardSource.SpreadsheetML;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_NhapKhoiLuongHangNgay : DevExpress.XtraEditors.XtraUserControl
    {
        string _tbl, _tblHangNgay, _tblHangNgayFileDinhKem, _tblDonGia, _colFK, _code, _colMaHieu, _codeNhom = null;
        public static Dictionary<string, KeyValuePair<string,string>> _DicMaHieu;
        public static KeyValuePair<string, string> _TenCT_MH;
        DateTime _ngayBD, _ngayKT;
        Worksheet _ws = null;
        double _donGiaKeHoach, _donGiaThiCong, _KLKHTong= 0;
        double? _KLKHNhom = null;
        TypeKLHN _type;
        string _arrCodeHaoPhiQuery;
        string[] _arrCodeHaoPhiCongTac;
        List<DateTime> _NgayNghi;
        //public static Dictionary<string, KeyValuePair<DateTime, DateTime>> _dicNgay = new Dictionary<string, KeyValuePair<DateTime, DateTime>>();
        //public static KeyValuePair<DateTime, DateTime> _dicNBD_KT = new KeyValuePair<DateTime, DateTime>();
        public static Dictionary<string, string> _dicSheet = new Dictionary<string, string>();
        //public static Worksheet _ws = null;
        List<Tbl_ThongTinNhaCungCapViewModel> _nccs = new List<Tbl_ThongTinNhaCungCapViewModel>();
        Dictionary<string, string> _DicNccs;// = new List<Tbl_ThongTinNhaCungCapViewModel>();
        string _crState = null;
        public  int RowIndex { get; set; }
        public XtraForm_NhapKhoiLuongHangNgay()
        {
            InitializeComponent();
            dtp_Ngay.Properties.MaxValue = DateTime.Now.Date;
            dtp_Ngay.EditValue = DateTime.Now.Date;
        }
        public void pushData(NavigationPaneState StateNP, DonViThucHien DVTH,Dictionary<string,string> dic,bool IsLoad,TypeKLHN type, string code, KeyValuePair<string, string> TenCT_MH = default, string[] arrCodeHaoPhiCongTac = null,Dictionary<string,KeyValuePair<string,string>> DicMaHieu=null, int Index = 0, bool onlyLoadData = false)
        {

            //if (StateNP != NavigationPaneState.Collapsed && _code != "")
            //    goto LabelLoad;

            var da = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;

            if (da.IsAutoSynthetic && DVTH.IsGiaoThau)
            {
                tl_CongTacVatLieu.OptionsBehavior.ReadOnly = true;
                lci_warningReadOnly.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                tl_CongTacVatLieu.OptionsBehavior.ReadOnly = false;
                lci_warningReadOnly.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            }


            _crState = null;
            _ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
            band_KeHoach.Visible = (BaseFrom.allPermission.HaveInitProjectPermission);

            col_LyTrinhCaoDo.Visible = (type == TypeKLHN.CongTac);
            if (onlyLoadData)
                goto LabelLoad;

            else if (code == "")
                return;
            lci_warning.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.Enabled = true;
            col_NCC.Visible = true;
            band_FileDinhKem.Visible = true;
            BandThiCong.Visible = !SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH.IsGiaoThau;

            _nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
            rpCbe_NCC.DataSource = _nccs;
            _DicNccs = _nccs.ToDictionary(x => x.Code, x=> x.Ten);
            _dicSheet = dic;
                if (!IsLoad || StateNP == NavigationPaneState.Collapsed)
                {
                    return;
                }
            if (_code == code)
            {
                if (type == TypeKLHN.CongTac)
                    goto Label;
                else if (type == TypeKLHN.VatLieu)
                    goto LabelVatLieu;
                else if (type == TypeKLHN.HaoPhiVatTu)
                    goto LabelHaoPhiVatTu;
                else return;
            }
            
            _DicMaHieu = DicMaHieu ?? _DicMaHieu;
            _code = code;
            _type = type;
            _TenCT_MH = TenCT_MH;
            RowIndex = Index;
            string dbString = string.Empty;
            _arrCodeHaoPhiCongTac = arrCodeHaoPhiCongTac;
            if (arrCodeHaoPhiCongTac != null)
            {
                _arrCodeHaoPhiQuery = string.Join(", ", arrCodeHaoPhiCongTac.Select(x => $"'{x}'").ToArray());
            }
            nud_KLTC.Properties.MaxValue = nud_DonGiaKeHoach.Maximum = nud_KhoiLuongToanBo.Maximum = nud_DonGiaThiCong.Maximum = decimal.MaxValue;
            if(_NgayNghi!=null)
                _NgayNghi.Clear();

            LabelLoad:
            _codeNhom = null;
            _KLKHNhom = null;

            if (!_code.HasValue())
                return;
            switch (_type)
            {
                case TypeKLHN.Nhom:
                    
                    col_NCC.Visible = false;
                    _tbl = TDKH.TBL_NhomCongTac;
                    _tblHangNgay = TDKH.TBL_KhoiLuongCongViecHangNgay;
                    _tblHangNgayFileDinhKem = TDKH.TBL_NhomFileDinhKem;
                    _tblDonGia = TDKH.Tbl_DonGiaThiCongHangNgay;
                    //_colMaHieu = "MaHieuCongTac";
                    _colFK = "CodeNhom";
                    scc_CongViec.PanelVisibility = SplitPanelVisibility.Panel1;
                    //tl_ColMaHieu.Visible = false;
                    //tl_colName.Visible = false;
                    //tl_ColXoa.Visible = false;
                    _NgayNghi = new List<DateTime>();// TDKHHelper.GetNgayNghiOfCongTac(TypeKLHN.Nhom, _code);
                    lcgr_ThemNgayVatTu.Text = "Thêm ngày thực hiện";
                    scc_CongViec.PanelVisibility = SplitPanelVisibility.Panel1;
                    lcgr_ThemNgayVatTu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    _NgayNghi = TDKHHelper.GetNgayNghiOfCongTac(TypeKLHN.Nhom, _code);

                    dbString = $"SELECT * FROM {_tbl} WHERE Code = '{_code}'";
                    var dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    lb_TenCongTac.Text = $"NHÓM: {dtNhom.Rows[0]["Ten"]}";
                    double.TryParse(dtNhom.Rows[0]["DonGia"].ToString(), out _donGiaKeHoach);
                    double.TryParse(dtNhom.Rows[0]["DonGiaThiCong"].ToString(), out _donGiaThiCong);

                    if (!double.TryParse(dtNhom.Rows[0]["KhoiLuongKeHoach"].ToString(), out _KLKHTong))
                    {
                        lb_TenCongTac.Text += "(Chưa có số liệu)";

                        this.Enabled = false;
                    }

                    DateTime.TryParse(dtNhom.Rows[0]["NgayBatDau"].ToString(), out _ngayBD);
                    DateTime.TryParse(dtNhom.Rows[0]["NgayKetThuc"].ToString(), out _ngayKT);
                    break;

                case TypeKLHN.CongTac:
                    col_NCC.Visible = false;
                    _tbl = TDKH.TBL_ChiTietCongTacTheoKy;
                    _tblHangNgay = TDKH.TBL_KhoiLuongCongViecHangNgay;
                    _tblHangNgayFileDinhKem = TDKH.TBL_ChiTietCongTacTheoKyFileDinhKem;
                    _tblDonGia = TDKH.Tbl_DonGiaThiCongHangNgay;
                    _colMaHieu = "MaHieuCongTac";
                    _colFK = "CodeCongTacTheoGiaiDoan";
                    scc_CongViec.PanelVisibility = SplitPanelVisibility.Panel1;
                    //tl_ColMaHieu.Visible = false;
                    //tl_colName.Visible = false;
                    //tl_ColXoa.Visible = false;
                    _NgayNghi = TDKHHelper.GetNgayNghiOfCongTac(TypeKLHN.CongTac, _code);
                    lcgr_ThemNgayVatTu.Text = "Thêm ngày thực hiện";
                    scc_CongViec.PanelVisibility = SplitPanelVisibility.Panel1;
                    lcgr_ThemNgayVatTu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    dbString = $"SELECT * " +
                        $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                        $"JOIN {GiaoViec.TBL_CONGVIECCHA} " +
                        $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {GiaoViec.TBL_CONGVIECCHA}.CodeCongTacTheoGiaiDoan " +
                        $"JOIN {GiaoViec.TBL_CONGVIECCON} " +
                        $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha = {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCha " +
                        $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.Code  = '{_code}'";

                    DataTable dtCon = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                    if (dtCon.Rows.Count > 0)
                        lci_warning.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    goto Label;         
                case TypeKLHN.VatLieu:
                    _tbl = TDKH.TBL_KHVT_VatTu;
                    _tblHangNgay = TDKH.TBL_KHVT_KhoiLuongHangNgay;
                    _tblDonGia = TDKH.TBL_KHVT_DonGia;
                    _colFK = "CodeVatTu";
                    _colMaHieu = "MaVatLieu";
                    //tl_ColMaHieu.Visible = true;
                    //tl_ColXoa.Visible = true;
                    //tl_colName.Visible = true;
                    //tl_ColMaHieu.VisibleIndex = 1;
                    //tl_colName.VisibleIndex = 2;
                    tl_ColXoa.VisibleIndex = tl_CongTacVatLieu.Columns.Count;
                    lcgr_ThemNgayVatTu.Text = "Thêm ngày vật tư";

                    scc_CongViec.PanelVisibility = SplitPanelVisibility.Panel1;
                    lcgr_ThemNgayVatTu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    band_FileDinhKem.Visible = false;
                    goto LabelVatLieu;
                
                case TypeKLHN.HaoPhiVatTu:
                    _tbl = MyConstant.view_HaoPhiVatTu;
                    _tblHangNgay = TDKH.Tbl_HaoPhiVatTu_HangNgay;
                    _tblDonGia = TDKH.Tbl_HaoPhiVatTu_DonGia;
                    _colFK = "CodeHaoPhiVatTu";
                    //tl_ColMaHieu.Visible = false;
                    //tl_colName.Visible = false;
                    //tl_ColXoa.Visible = false;
                    lcgr_ThemNgayVatTu.Text = "Thêm ngày hao phí vật tư";
                    scc_CongViec.PanelVisibility = SplitPanelVisibility.Panel1;
                    lcgr_ThemNgayVatTu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    band_FileDinhKem.Visible = false;
                    goto LabelHaoPhiVatTu;

                default:
                    this.Enabled = false;
                    return;

                
            }
            if (StateNP == NavigationPaneState.Collapsed)
                return;
            WaitFormHelper.ShowWaitForm("Đang tải khối lượng hàng ngày");
            goto NexLabel;

            Label:
            dbString = $"SELECT {TDKH.TBL_DanhMucCongTac}.*, {_tbl}.TrangThai, {_tbl}.KhoiLuongToanBo, {_tbl}.CodeNhom, {_tbl}.DonGia,{_tbl}.NgayBatDau,{_tbl}.NgayKetThuc,{_tbl}.NgayKetThucThiCong," +
                $"{_tbl}.NgayBatDauThiCong, {_tbl}.DonGiaThiCong, {TDKH.TBL_NhomCongTac}.KhoiLuongKeHoach AS KLKHNhom\r\n " +
                $"FROM {_tbl} " +
     $"JOIN {TDKH.TBL_DanhMucCongTac} " +
     $"ON {_tbl}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code " +
     $"\r\n LEFT JOIN {TDKH.TBL_NhomCongTac} \r\n" +
     $"ON {_tbl}.CodeNhom IS NOT NULL AND {TDKH.TBL_NhomCongTac}.Code = {_tbl}.CodeNhom\r\n" +
     $"WHERE {_tbl}.Code = '{_code}'";
            DataTable dt= DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count > 0)
            {
                _codeNhom = dt.Rows[0]["CodeNhom"].ToString();
                lb_TenCongTac.Text = $"Công tác: ({dt.Rows[0]["MaHieuCongTac"]}) {dt.Rows[0]["TenCongTac"]}";
                _donGiaKeHoach = double.Parse(dt.Rows[0]["DonGia"].ToString());
                _donGiaThiCong = double.Parse(dt.Rows[0]["DonGiaThiCong"].ToString());
                _KLKHTong = double.Parse(dt.Rows[0]["KhoiLuongToanBo"].ToString());
                _crState = dt.Rows[0]["TrangThai"].ToString();
                if (dt.Rows[0]["KLKHNHom"] != DBNull.Value)
                    _KLKHNhom = double.Parse(dt.Rows[0]["KLKHNhom"].ToString());




                if (dt.Rows[0]["NgayKetThuc"] != DBNull.Value && dt.Rows[0]["NgayBatDau"] != DBNull.Value)
                {
                    _ngayBD = DateTime.Parse(dt.Rows[0]["NgayBatDau"].ToString());
                    _ngayKT = DateTime.Parse(dt.Rows[0]["NgayKetThuc"].ToString());
                }
            }
            goto NexLabel;

            
            
            LabelVatLieu:
            dbString = $"SELECT * FROM {_tbl} " +
                        $"WHERE Code = '{_code}'";
            DataTable dtVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtVT.Rows.Count > 0)
            {

                if (dtVT.Rows[0]["NgayKetThuc"] != DBNull.Value && dtVT.Rows[0]["NgayBatDau"] != DBNull.Value)
                {
                    _ngayBD = DateTime.Parse(dtVT.Rows[0]["NgayBatDau"].ToString());
                    _ngayKT = DateTime.Parse(dtVT.Rows[0]["NgayKetThuc"].ToString());
                }

                lb_TenCongTac.Text = $"Vật tư: ({dtVT.Rows[0]["MaVatLieu"]}) {dtVT.Rows[0]["VatTu"]}";
                _donGiaKeHoach = long.Parse(dtVT.Rows[0]["DonGia"].ToString());
                _donGiaThiCong = long.Parse(dtVT.Rows[0]["DonGiaThiCong"].ToString());
                _KLKHTong = double.Parse(dtVT.Rows[0]["KhoiLuongKeHoach"].ToString());

            }
            goto NexLabel;

            LabelHaoPhiVatTu:
            dbString = $"SELECT {_tbl}.*, {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhom, {TDKH.TBL_DanhMucCongTac}.MaHieuCongTac FROM {_tbl} " +
                        $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                        $"ON {_tbl}.CodeCongTac = {TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                        $"JOIN {TDKH.TBL_DanhMucCongTac} " +
                        $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code " +
                        $"WHERE {_tbl}.Code = '{_code}'";
            DataTable dtHP = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtHP.Rows.Count > 0)
            {

                _codeNhom = dtHP.Rows[0]["CodeNhom"].ToString();

                if (dtHP.Rows[0]["NgayKetThuc"] != DBNull.Value && dtHP.Rows[0]["NgayBatDau"] != DBNull.Value)
                {
                    _ngayBD = DateTime.Parse(dtHP.Rows[0]["NgayBatDau"].ToString());
                    _ngayKT = DateTime.Parse(dtHP.Rows[0]["NgayKetThuc"].ToString());
                }
                lb_TenCongTac.Text = $"Hao phí: ({dtHP.Rows[0]["MaVatLieu"]}) {dtHP.Rows[0]["VatTu"]} __ Công tác: {dtHP.Rows[0]["MaHieuCongTac"]}";
                _donGiaKeHoach = long.Parse(dtHP.Rows[0]["DonGia"].ToString());
                _donGiaThiCong = long.Parse(dtHP.Rows[0]["DonGia"].ToString());
                _KLKHTong = double.Parse(dtHP.Rows[0]["KhoiLuongKeHoach"].ToString());

            }
            goto NexLabel;



            NexLabel:
            this.Enabled = true;
            if(DVTH.IsGiaoThau)
                lcgr_ThemNgayVatTu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            loadData();
            WaitFormHelper.CloseWaitForm();

        }
        private void Fcn_Update_KTTCDelete()
        {
            if (!BandThiCong.Visible)
                return;
            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            Worksheet ws = _ws;
            int Row = MyFunction.SearchRangeCell(ws, _code).FirstOrDefault().RowIndex;
            List<KLTTHangNgay> lsCongTac = (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>);
            double ThiCong = (double)lsCongTac.Sum(x => x.KhoiLuongThiCong);
            List<KLTTHangNgay> MinMax = lsCongTac.Where(x => x.KhoiLuongThiCong > 0).ToList();
            Dictionary<string, string> _dicACtive = _dicSheet;
            //if ()
            wb.BeginUpdate();
            try
            {
                if (!MinMax.Any())
                {
                    ws.Rows[Row][_dicACtive[TDKH.COL_NgayBatDauThiCong]].SetValue(null);
                    ws.Rows[Row][_dicACtive[TDKH.COL_NgayKetThucThiCong]].SetValue(null);
                }
                else
                {
                    ws.Rows[Row][_dicACtive[TDKH.COL_NgayBatDauThiCong]].SetValue(MinMax.Min(x => x.Ngay.Value));
                    ws.Rows[Row][_dicACtive[TDKH.COL_NgayKetThucThiCong]].SetValue(MinMax.Max(x => x.Ngay.Value));
                }

                ws.Rows[Row][_dicACtive[TDKH.COL_KhoiLuongDaThiCong]].SetValue(MinMax.Sum(x => x.KhoiLuongThiCong));
                ws.Rows[Row][_dicACtive[TDKH.COL_GiaTriThiCong]].SetValue(MinMax.Sum(x => x.ThanhTienThiCong));
                bt_Update_Click(null, null);
            }
            catch
            {

            }
            wb.EndUpdate();
        }
        /*private void Fcn_UpdateNBD_KTTC(DateTime Date,KeyValuePair<DateTime,DateTime> dicNgay,string SheetName)
        {
            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            Worksheet ws = wb.Worksheets[SheetName];
            int Row = MyFunction.SearchRangeCell(ws, _code).FirstOrDefault().RowIndex;
            string dbString = "",SheetNameActive=SharedControls.ws_VatLieu.Name;
            Dictionary<string, string> _dicACtive = _dicSheet;
            List<KLTTHangNgay> lsCongTac = (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>);
            double KeHoach = (double)lsCongTac.Sum(x => x.KhoiLuongThiCong);
            //if(_type!=TypeKLHN.VatLieu)

            if (ws.Name != TDKH.SheetName_DoBocChuan)
            {
                ws.Rows[Row][_dicSheet[TDKH.COL_KhoiLuongDaThiCong]].SetValue(KeHoach);
            }
            
            //else if (SheetNameActive == TDKH.SheetName_KeHoachKinhPhi)
            //{
            //    Worksheet ws_KLHN = wb.Worksheets[TDKH.SheetName_KLHangNgay];
            //    int Row_index = MyFunction.SearchRangeCell(ws_KLHN, _code).FirstOrDefault().RowIndex;
            //    ws_KLHN.Rows[Row_index][TDKH.dic_KLHangNgay_All[TDKH.COL_DBC_LuyKeDaThucHien]].SetValue(KeHoach);
            //    _dicACtive = TDKH.dic_KLHangNgay_All;
            //}

            if (dicNgay.Key==default)
            {
                if (ws.Name != TDKH.SheetName_DoBocChuan)
                {
                    ws.Rows[Row][_dicACtive[TDKH.COL_NgayBatDauThiCong]].SetValueFromText(Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
                    ws.Rows[Row][_dicACtive[TDKH.COL_NgayKetThucThiCong]].SetValueFromText(Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
                }
                //                dbString = $"UPDATE {_tbl} " +
                //$"SET NgayBatDauThiCong = '{Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}', " +
                //$"NgayKetThucThiCong = '{Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                //$"WHERE Code = '{_code}'";
                //                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            else
            {
                if (Date > dicNgay.Value)
                {
                    if (ws.Name != TDKH.SheetName_DoBocChuan)
                    {
                        ws.Rows[Row][_dicACtive[TDKH.COL_NgayKetThucThiCong]].SetValueFromText(Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
                    }
                }
                else if (Date < dicNgay.Key)
                {
                    if (ws.Name != TDKH.SheetName_DoBocChuan)
                    {
                        ws.Rows[Row][_dicACtive[TDKH.COL_NgayBatDauThiCong]].SetValueFromText(Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
                    }
                }
                //DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
        }*/
        private void loadData()
        {
            de_NBDKH.EditValueChanging -= de_NBDKH_EditValueChanging;
            de_NKTKH.EditValueChanging -= de_NKTKH_EditValueChanging;
            de_NBDTC.EditValueChanging -= de_NBDTC_EditValueChanging;
            de_NKTTC.EditValueChanging -= de_NKTTC_EditValueChanging;
            nud_DonGiaKeHoach.ValueChanged -= nud_DonGiaKeHoach_ValueChanged;
            nud_DonGiaThiCong.ValueChanged -= nud_DonGiaThiCong_ValueChanged;

            
            //var dicDonGia = MyFunction.fcn_GetDicNgay_DonGia(_code, _type);

            var datas = MyFunction.Fcn_CalKLKHModel(_type, new string[] { _code });

            

            foreach (var vt in datas.Where(x => x.NhaCungCap.HasValue()))
            {
                //var AllCrNCC = MyFunction.TryGetObjFromJson<NhaCungCapHangNgayViewModel>(vt.NhaCungCap);
                //var AllCrCodeNCC = AllCrNCC.Select(x => x.Code);

                var lsCon = DuAnHelper.GetNCCDetail(vt, _DicNccs);
                datas.AddRange(lsCon);
            }

            //foreach (var data in datas.ToArray())
            //{

            //}
            datas.ForEach(x => { x.KLKHTong = _KLKHTong; x.KhoiLuongKeHoach = x.KhoiLuongKeHoach; x.KhoiLuongThiCong = x.KhoiLuongThiCong;  });
            //datas.ForEach(x => { x.KLKHTong = _KLKHTong; x.KhoiLuongKeHoach = x.KhoiLuongKeHoach; });
            tl_CongTacVatLieu.DataSource =datas;
            tl_CongTacVatLieu.RefreshDataSource();
            tl_CongTacVatLieu.Refresh();
            //rpCbe_NCC.DataSource = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();

            de_NBDKH.EditValueChanging += de_NBDKH_EditValueChanging;
            de_NKTKH.EditValueChanging += de_NKTKH_EditValueChanging;
            de_NBDTC.EditValueChanging += de_NBDTC_EditValueChanging;
            de_NKTTC.EditValueChanging += de_NKTTC_EditValueChanging;
            nud_DonGiaKeHoach.ValueChanged += nud_DonGiaKeHoach_ValueChanged;
            nud_DonGiaThiCong.ValueChanged += nud_DonGiaThiCong_ValueChanged;
            //Fcn_Update_KTTCDelete();
        }
        public void Clear()
        {
            lb_TenCongTac.Text = "CHƯA CHỌN CÔNG TÁC";
            tl_CongTacVatLieu.DataSource = null;
            _code = "";
            lci_warning.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.Enabled = false;
        }
        private void de_NBDKH_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue is null)
            {
                e.Cancel = true;
                return;
            }   
            
            if ((DateTime)e.NewValue > (DateTime)de_NKTKH.EditValue)
            {
                MessageShower.ShowInformation("Vui lòng chọn ngày bắt đầu không lớn hơn ngày kết thúc");
                e.Cancel= true;
                return;
            }

            string dbString = $"UPDATE {_tbl} " +
                $"SET NgayBatDau = '{((DateTime)e.NewValue).Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                $"WHERE Code = '{_code}'";

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            loadData();
        }

        private void de_NKTKH_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue is null)
            {
                e.Cancel = true;
                return;
            }

            if ((DateTime)e.NewValue < (DateTime)de_NBDKH.EditValue)
            {
                MessageShower.ShowInformation("Vui lòng chọn ngày bắt đầu không lớn hơn ngày kết thúc");
                e.Cancel = true;
                return;
            }

            string dbString = $"UPDATE {_tbl} " +
                $"SET NgayKetThuc = '{((DateTime)e.NewValue).Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                $"WHERE Code = '{_code}'";

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            loadData();
        }

        private void de_NBDTC_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (de_NKTTC.EditValue is null )
                return;

            string dbString = $"UPDATE {_tbl} " +
                $"SET NgayBatDauThiCong = '{((DateTime)e.NewValue).Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}', " +
                $"NgayKetThucThiCong = '{((DateTime)de_NKTTC.EditValue).Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                $"WHERE Code = '{_code}'"; 

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            loadData();
            
        }

        private void de_NKTTC_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (de_NBDTC.EditValue is null)
                return;

            string dbString = $"UPDATE {_tbl} " +
                $"SET NgayBatDauThiCong = '{((DateTime)de_NBDTC.EditValue).Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}', " +
                $"NgayKetThucThiCong = '{((DateTime)e.NewValue).Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                $"WHERE Code = '{_code}'";

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            loadData();
        }

        private void nud_DonGiaKeHoach_ValueChanged(object sender, EventArgs e)
        {
            string dbString = $"UPDATE {_tbl} SET DonGia = '{nud_DonGiaKeHoach.Value}' WHERE Code = '{_code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);


            loadData();
        }

        private void sbt_ThayDoiDonGiaThiCongHangNgay_Click(object sender, EventArgs e)
        {
            Form_ThayDoiDonGiaThiCong form = new Form_ThayDoiDonGiaThiCong(_code, _type);
            DialogResult ret = form.ShowDialog();

            if (ret == DialogResult.OK)
            {
                loadData();
                
            }
        }
        private void repobt_Xoa_Click(object sender, EventArgs e)
        {
            //KLTTHangNgay row = gv_CTVL.FocusedRowObject as KLTTHangNgay;
            //string dbString = $"DELETE FROM {_tblHangNgay} WHERE Code = '{row.Code}'";
            //DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            

            //dbString = $"DELETE FROM {TDKH.Tbl_HaoPhiVatTu_HangNgay} " +
            //    $"WHERE CodeHaoPhiVatTu IN ({_arrCodeHaoPhiQuery}) " +
            //    $"AND Ngay = '{row.Ngay.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            //DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            
            //gv_CTVL.DeleteSelectedRows();
            //loadChiTietCongTac();
        }

        private void tl_CongTacVatLieu_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (e.Node.Level == 0)
                return;

            //if (object.Equals(e.CellValue, (double)0) || e.Column.FieldName == "Xoa" || e.Column.FieldName == "Ngay")
            //{
            //    e.Appearance.FillRectangle(e.Cache, e.Bounds);
            //    e.Handled = true;
            //}

        }

        private void tl_CongTacVatLieu_ShowingEditor(object sender, CancelEventArgs e)
        {
            //DateTime _NewNgayNghi =(DateTime)KLHN.Ngay.Value.Date;
            string fieldName = tl_CongTacVatLieu.FocusedColumn.FieldName;
            if (fieldName == TDKH.COL_FileDinhKem)
                return;
            //if (_codeNhom.HasValue() && _KLKHNhom != null)
            //{
            //    AlertShower.ShowInfo("Khối lượng được tính tự động từ Nhóm!");
            //    e.Cancel = true;
            //    return;
            //}

            KLTTHangNgay KLHN = tl_CongTacVatLieu.GetFocusedRow() as KLTTHangNgay;

            var node = tl_CongTacVatLieu.FocusedNode;

            string[] fieldNamesAfterKLTC =
            {
                nameof(KLTTHangNgay.CodesNCCString),
                TDKH.COL_GhiChu,
                TDKH.COL_LyTrinhCaoDo,
            };

            var rootNode = node.RootNode;
            //KLTTHangNgay KLHNRoot = tl_CongTacVatLieu.GetDataRecordByNode(rootNode) as KLTTHangNgay;


            if (KLHN.Ngay > DateTime.Now.Date && fieldName.Contains("ThiCong"))
            {
                MessageShower.ShowInformation("Không thể thay đổi ô này vì quá ngày hiện tại! Vui lòng chỉ thay đổi các ô phù hợp");
                e.Cancel = true;
                return;
            }

            if (KLHN.Ngay < DateTime.Now.AddDays(-2) && _crState.HasValue() && _crState != EnumTrangThai.DANGCHINHSUA.GetEnumDisplayName())
            {
                MessageShower.ShowInformation("Không thể sửa khối lượng thi công. Vui lòng liên hệ Admin chuyển TRẠNG THÁI về \"Đang chỉnh sửa\" để tiếp tục nhập khối lượng");
                e.Cancel = true;
                return;
            }

            if (KLHN.Ngay < _ngayBD || KLHN.Ngay > _ngayKT)
            {
                if (fieldName.Contains("KeHoach"))
                {
                    MessageShower.ShowInformation("Không thể thay đổi ô này vì quá ngày Kế Hoạch! Vui lòng chỉ thay đổi các ô phù hợp");
                    e.Cancel = true;
                    return;
                }
            }

            if (fieldNamesAfterKLTC.Contains(fieldName) && KLHN.KhoiLuongThiCong is null)
            {
                MessageShower.ShowWarning("Vui lòng nhập khối lượng thi công trước khi chọn nhà cung cấp");
                e.Cancel = true;
                return;
            }    

            if (_NgayNghi != null)
            {
                if (_NgayNghi.Contains(KLHN.Ngay.Value))
                {
                    DialogResult rs = MessageShower.ShowYesNoQuestion("Đây là ngày nghỉ, Bạn có muốn cập nhập khối lượng không?");
                    if (rs == DialogResult.No)
                        e.Cancel = true;
                }
            }
            if (tl_CongTacVatLieu.GetFocusedDisplayText() == null && tl_CongTacVatLieu.FocusedColumn.FieldName == "KhoiLuongKeHoach")
            {
                MessageShower.ShowInformation("Không thể thay đổi ô này! Vui lòng chỉ thay đổi các ô đã có giá trị");
                e.Cancel = true;
                return;
            }
            
            if (node.Level == 1)
            {
                if (KLHN.IsNCCConLai)
                {
                    MessageShower.ShowWarning("Đây là dòng tính tự động, không được chỉnh sửa");
                    e.Cancel = true;
                    return;
                }
                if (fieldName != "KhoiLuongThiCong" && fieldName != "DonGiaThiCong")
                {
                    MessageShower.ShowError("Chỉ có thể thay đổi khối lượng và đơn giá thi công của nhà cung cấp");
                    e.Cancel = true;
                }
            }    
        }

        private void tl_CongTacVatLieu_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList tL = sender as DevExpress.XtraTreeList.TreeList;
            DevExpress.XtraTreeList.TreeListHitInfo hitInfo = tL.CalcHitInfo(e.Point);

                DXMenuItem chonToanBo = new DXMenuItem("Chọn toàn bộ", this.fcn_Handle_Popup_ChonTatCa);
                chonToanBo.Tag = hitInfo.Column;
                e.Menu.Items.Add(chonToanBo);
                
                DXMenuItem boChonToanBo = new DXMenuItem("Bỏ chọn toàn bộ", this.fcn_Handle_Popup_BoChonTatCa);
                boChonToanBo.Tag = hitInfo.Column;
                e.Menu.Items.Add(boChonToanBo);
            
            DXMenuItem xoaKhoiLuong = new DXMenuItem("Xóa khối lượng", this.fcn_Handle_Popup_XoaKhoiLuongThiCong);
            xoaKhoiLuong.Tag = hitInfo.Column;
                e.Menu.Items.Add(xoaKhoiLuong );
            if (BandThiCong.Visible)
            {


                DXMenuItem menuItem = new DXMenuItem("Lấy khối lượng kế hoạch sang thi công", this.fcn_Handle_Popup_QLVT_LayKhoiLuong);
                menuItem.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem);
                DXMenuItem menuItem1 = new DXMenuItem("Lấy khối lượng kế hoạch sang thi công (Lấy toàn bộ những ngày chưa có thi công)", this.fcn_Handle_Popup_QLVT_LayKhoiLuong_Remove);
                menuItem1.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem1);
            }
        }

        private void GetKLTCFromKLKH(bool isToanBo)
        {
            List<KLTTHangNgay> lsCongTac = (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>).Where(x => x.Chon).ToList();
            var drs = lsCongTac.Where(x => x.Ngay <= DateTime.Now).ToArray();

            if (!isToanBo)
                drs = drs.Where(x => !x.KhoiLuongThiCong.HasValue).ToArray();

            if (!drs.Any())
            {
                MessageShower.ShowError("Không có ngày để lấy khối lượng");
                return;
            }

            if (drs.Length == 0)
                return;
            DateTime Min = drs.Min(x => x.Ngay.Value.Date);
            DateTime Max = drs.Max(x => x.Ngay.Value.Date);

            if (Max.Date > DateTime.Now.Date)
                Max = DateTime.Now;

/*            string dbString = $"UPDATE {_tbl} " +
                                $"SET NgayBatDauThiCong = '{Min.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}', " +
                                $"NgayKetThucThiCong = '{Max.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                                $"WHERE Code = '{_code}'";

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);*/
            List<string> queries = new List<string>();

            foreach (var dr in drs)
            {
                dr.KhoiLuongThiCong = dr.KhoiLuongKeHoach;
                string query = $"INSERT OR IGNORE INTO {_tblHangNgay} " +
                    $"(Code, {_colFK},  Ngay) " +
                    $"VALUES ('{dr.Code}', '{_code}', '{dr.NgayString}');\r\n" +
                    $"UPDATE {_tblHangNgay} SET KhoiLuongThiCong = '{dr.KhoiLuongKeHoach}' WHERE code = '{dr.Code}'";

                queries.Add(query);
            }
            string dbString = string.Join($";\r\n", queries);
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            //DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dtnew, _tblHangNgay, "Code", isCompareTime: false);
            if (_type == TypeKLHN.CongTac)
                DuAnHelper.UpdateStateTDKHByKhoiLuongThiCong(new string[] { _code });       
            else if (_type == TypeKLHN.Nhom)
                DuAnHelper.UpdateStateTDKHNhomByKhoiLuongThiCong(new string[] { _code });
            //loadData();
            //string sheetName = _type == TypeKLHN.CongTac ? TDKH.SheetName_KeHoachKinhPhi : SharedControls.ws_VatLieu.Name;
            CapNhatKhoiLuongThiCong();


            tl_CongTacVatLieu.RefreshDataSource();
        }


        private void CapNhatKhoiLuongThiCong()
        {
            List<KLTTHangNgay> lsCongTac = (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>).Where(x => x.KhoiLuongThiCong.HasValue && x.KhoiLuongThiCong.Value != 0).ToList();


            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            Worksheet ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;

            if (ws.Name != TDKH.SheetName_DoBocChuan)
            {
                int Row = MyFunction.SearchRangeCell(ws, _code).FirstOrDefault().RowIndex;
                double KeHoach = (double)lsCongTac.Sum(x => x.KhoiLuongThiCong);
                //if (_type == TypeKLHN.CongTac)
                //{
                //    List<KLTTHangNgay> lsCongTac = (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>);
                //    ws.Rows[Row][_dicSheet[TDKH.COL_KhoiLuongDaThiCong]].SetValue(KeHoach);
                //}
                if (!lsCongTac.Any())
                {
                    ws.Rows[Row][_dicSheet[TDKH.COL_NgayBatDauThiCong]].SetValue(null);
                    ws.Rows[Row][_dicSheet[TDKH.COL_NgayKetThucThiCong]].SetValue(null);
                    ws.Rows[Row][_dicSheet[TDKH.COL_KhoiLuongDaThiCong]].SetValue(0);
                    return;
                }

                DateTime Min = lsCongTac.Min(x => x.Ngay.Value);
                DateTime Max = lsCongTac.Max(x => x.Ngay.Value);

                ws.Rows[Row][_dicSheet[TDKH.COL_NgayBatDauThiCong]].SetValueFromText(Min.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                ws.Rows[Row][_dicSheet[TDKH.COL_NgayKetThucThiCong]].SetValueFromText(Max.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                ws.Rows[Row][_dicSheet[TDKH.COL_KhoiLuongDaThiCong]].SetValue(KeHoach);
            }
        }


        private void fcn_Handle_Popup_ChonTatCa(object sender, EventArgs e)
        {
            tl_CongTacVatLieu.CheckAll();
            //foreach (TreeListNode node in tl_CongTacVatLieu.Nodes)
            //{
            //    node.check
            //}    
        }

        private void fcn_Handle_Popup_BoChonTatCa(object sender, EventArgs e)
        {
            tl_CongTacVatLieu.UncheckAll();
        }

        private void fcn_Handle_Popup_QLVT_LayKhoiLuong(object sender, EventArgs e)
        {
            if (MessageShower.ShowOkCancelInformation("Khối lượng thi công các ngày đã chọn sẽ bị ghi đề và KHÔNG THỂ HOÀN TÁC!") != DialogResult.OK)
                return;
            GetKLTCFromKLKH(true);
        }   
        
        private void fcn_Handle_Popup_XoaKhoiLuongThiCong(object sender, EventArgs e)
        {
            if (MessageShower.ShowOkCancelInformation("Khối lượng các ngày đã chọn sẽ bị Xóa!") != DialogResult.OK)
                return;
            XoaCongTacDaChon();
        }   
        private void fcn_Handle_Popup_QLVT_LayKhoiLuong_Remove(object sender, EventArgs e)
        {
            if (MessageShower.ShowOkCancelInformation("TOÀN BỘ các ngày chưa nhập thi công sẽ được lấy khối lượng tự động từ kế hoạch") != DialogResult.OK)
                return;
            GetKLTCFromKLKH(false);
        }

        private void rpCbe_NCC_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //string oldVal = (e.OldValue as string);
            //string newVal = (e.NewValue as string);

            //if (!oldVal.HasValue())
            //    return;

            //string[] oldNccs = oldVal.Split(',').Select(x => x.Trim()).ToArray();
            //string[] newNccs = newVal.Split(',').Where(x => x.HasValue()).Select(x => x.Trim()).ToArray();

            //var NccsWillBeDeleted = oldNccs.Where(x => !newNccs.Contains(x));

            //if (!NccsWillBeDeleted.Any())
            //    return;

            //string[] namesnccsWillDeleted = _nccs.Where(x => NccsWillBeDeleted.Contains(x.Code)).Select(x => x.Ten).ToArray();

            //var info = $"Các nhà cung cấp sau sẽ bị xóa:\r\n " +
            //            $"{string.Join("\r\n", namesnccsWillDeleted)}";

            //if (MessageShower.ShowOkCancelInformation(info) != DialogResult.OK)
            //    e.Cancel = true;
        }

        private void rpCbe_NCC_EditValueChanged(object sender, EventArgs e)
        {

            
            tl_CongTacVatLieu.CloseEditor();
        }

        private void tl_CongTacVatLieu_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column == null)
                return;
            if (e.Node.Level == 0)
            {
                if (e.Node.GetValue("Ngay")!=null)
                {
                    DateTime _Date = DateTime.Parse(e.Node.GetValue("Ngay").ToString());
                    if (_Date > DateTime.Now&&e.Column.FieldName.Contains("ThiCong"))
                    {
                        e.Appearance.BackColor = MyConstant.color_Disable;
                        return;
                    }
                    bool IsEdit =(bool)e.Node.GetValue("IsEdited");

                    if (_Date < _ngayBD || _Date > _ngayKT)
                    {
                        if (e.Column.FieldName.Contains("KeHoach"))
                        {
                            e.Appearance.BackColor = MyConstant.color_Disable;
                        }
                    }
                    
                    if (IsEdit)
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    if (_NgayNghi != null)
                    {
                        if(_NgayNghi.Contains(_Date))
                            e.Appearance.ForeColor = MyConstant.color_Disable;
                    }
                }
            }
            else if (e.Node.Level == 1)
            {

            }
        }

        private void bt_ChonNgay_Click(object sender, EventArgs e)
        {
            foreach (var item in (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>).Where(x => x.Ngay.HasValue && x.Ngay >= De_Begin.DateTime.Date && x.Ngay <= De_End.DateTime.Date))
            {
                item.Chon = true;
            };
            tl_CongTacVatLieu.RefreshDataSource();

        }
        private void bt_BoChonNgay_Click(object sender, EventArgs e)
        {
            foreach (var item in (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>).Where(x => x.Ngay.HasValue && x.Ngay >= De_Begin.DateTime.Date && x.Ngay <= De_End.DateTime.Date))
            {
                item.Chon = false;
            };
            tl_CongTacVatLieu.RefreshDataSource();
        }

        private void tl_CongTacVatLieu_DataSourceChanged(object sender, EventArgs e)
        {
            if (tl_CongTacVatLieu.DataSource is null)
                return;
            var datas = (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>).Where(x => x.Ngay.HasValue);
            
            if (datas.Any())
            {
                gr_ChonNgay.Enabled = true;

                De_Begin.DateTime = De_Begin.Properties.MinValue = De_End.Properties.MinValue = datas.Min(x => x.Ngay.Value);
                De_End.DateTime = De_Begin.Properties.MaxValue = De_End.Properties.MaxValue = datas.Max(x => x.Ngay.Value);
            }
            else
            {
                gr_ChonNgay.Enabled = false;
            }

        }


        private void lb_TenCongTac_Click(object sender, EventArgs e)
        {
            //var result = TDKHHelper.SumKhoiLuongVatTuFromHaoPhi("420c585b-25b4-4b00-bfb2-8f9b25e40d2b");
        }

        private void XoaCongTacDaChon()
        {
            List<KLTTHangNgay> lsCongTac = (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>).Where(x => x.Chon).ToList();

            xoa1NgayThiCong(lsCongTac);
            
        }

        private void tl_CongTacVatLieu_Validating(object sender, CancelEventArgs e)
        {

        }

        private void tl_CongTacVatLieu_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (!double.TryParse(e.Value.ToString(), out double parseVal))
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập số thực";
                return;
            }
        }

        private void rihl_FileDinhKem_Click(object sender, EventArgs e)
        {
            KLTTHangNgay KLHN = tl_CongTacVatLieu.GetFocusedRow() as KLTTHangNgay;
            if (_tblHangNgayFileDinhKem == TDKH.TBL_ChiTietCongTacTheoKyFileDinhKem)
            {
                FormLuaChon form = new FormLuaChon(KLHN.CodeCha, FileManageTypeEnum.ChiTietCongTacTheoGiaiDoan, date: KLHN.Ngay);
                form.ShowDialog();
            }
            else
            {
                FormLuaChon form = new FormLuaChon(KLHN.CodeCha, FileManageTypeEnum.NhomCongTac, date: KLHN.Ngay);
                form.ShowDialog();
            }
        }

        private void bt_Update_Click(object sender, EventArgs e)
        {
            
        }

        private void rihl_FileDinhKem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void xoa1NgayThiCong(List<KLTTHangNgay> lsCongTac)
        {
            string dbString = "";

            List<string> queries = new List<string>();
            //string sheetName = _type == TypeKLHN.CongTac ? TDKH.SheetName_KeHoachKinhPhi : SharedControls.ws_VatLieu.Name;
            foreach (var row in lsCongTac)
            {
                if (row.KhoiLuongKeHoach == null && row.KhoiLuongThiCong == null && row.KhoiLuongKeHoachGiaoViec == null)
                {
                    dbString = $"DELETE FROM {_tblHangNgay} WHERE Code = '{row.Code}'";
                    //DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    tl_CongTacVatLieu.DeleteSelectedNodes();
                }
                else
                {
                    dbString = $"UPDATE {_tblHangNgay} SET KhoiLuongThiCong = NULL WHERE Code = '{row.Code}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    row.KhoiLuongThiCong = null;
                }
                queries.Add(dbString);
                if (_type == TypeKLHN.VatLieu)
                {
                    if (row.KhoiLuongKeHoach == null && row.KhoiLuongThiCong == null && row.KhoiLuongKeHoachGiaoViec == null)
                    {
                        dbString = $"DELETE FROM {TDKH.Tbl_HaoPhiVatTu_HangNgay} " +
                            $"WHERE CodeHaoPhiVatTu IN ({_arrCodeHaoPhiQuery}) " +
                            $"AND Ngay = '{row.Ngay.Value.Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        tl_CongTacVatLieu.DeleteSelectedNodes();

                    }

                }
                //DateTime dateSelected = (DateTime)row.Ngay;
                //if (_dicNgay.ContainsKey("ThiCong"))
            }

            if (queries.Any())
            {
                DataProvider.InstanceTHDA.ExecuteNonQuery(string.Join(";\r\n", queries));
            }
            if (_type == TypeKLHN.CongTac)
                DuAnHelper.UpdateStateTDKHByKhoiLuongThiCong(lsCongTac.Select(x=> x.Code));
            else if (_type == TypeKLHN.Nhom)
                DuAnHelper.UpdateStateTDKHNhomByKhoiLuongThiCong(lsCongTac.Select(x => x.Code));


            loadData();

        }
        private void rIBE_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa dữ liệu này?");
            if (rs == DialogResult.No)
                return;
            KLTTHangNgay row = tl_CongTacVatLieu.GetFocusedRow() as KLTTHangNgay;
                xoa1NgayThiCong(new List<KLTTHangNgay>() { row });

            string dbString = $"DELETE FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE " +
    $"KhoiLuongThiCong IS NULL AND KhoiLuongKeHoach IS NULL AND KhoiLuongKeHoachGiaoViec IS NULL";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

        }

        private void tl_CongTacVatLieu_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {

       
            //var node = e.Node;
            //var KLHN = tl_CongTacVatLieu.GetFocusedRow() as KLTTHangNgay;
            //var all = tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>;
            //if (node.Level == 1 && e.Column.FieldName == "KhoiLuongThiCong")
            //{
            //    var allCon = all.Where(x => x.ParentCode == KLHN.ParentCode);
            //    var crCha = all.Single(x => x.Code == KLHN.ParentCode);


            //}
        }

        private void tl_CongTacVatLieu_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            
            string fieldName = e.Column.FieldName;

            if (e.Column == col_TyLeKeHoach)
            {
                var val = double.Parse(e.Value.ToString());
                if (_KLKHTong != 0)
                {
                    double KLKH = Math.Round(_KLKHTong*val/100, 4);
                    e.Node.SetValue("KhoiLuongKeHoach", KLKH);
                    gv_CTVL_updateCol("KhoiLuongKeHoach", e.Node, KLKH);
                    return;
                }
            }
            else if (e.Column == col_TyLeThiCong)
            {
                var val = double.Parse(e.Value.ToString());

                if (_KLKHTong != 0)
                {
                    double KLTC = Math.Round(_KLKHTong * val / 100, 4);
                    e.Node.SetValue("KhoiLuongThiCong", KLTC);
                    gv_CTVL_updateCol("KhoiLuongThiCong", e.Node, KLTC);
                    return;
                }
            }    

            gv_CTVL_updateCol(fieldName,e.Node, e.Value);


        }

        private void nud_DonGiaThiCong_ValueChanged(object sender, EventArgs e)
        {
            string dbString = $"UPDATE {_tbl} SET DonGiaThiCong = '{nud_DonGiaThiCong.Value}' WHERE Code = '{_code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            loadData();
        }
                
        private void getDateKLTC(out DateTime? ngayBDTC, out DateTime? ngayKTTC, out double KLTC)
        {
            ngayBDTC = ngayKTTC = null;
            KLTC = 0;
            List<KLTTHangNgay> lsCongTac = (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>);
            KLTC = (double)lsCongTac.Sum(x => x.KhoiLuongThiCong);
            List<KLTTHangNgay> MinMax = lsCongTac.Where(x => x.KhoiLuongThiCong > 0).ToList();

            if (MinMax.Any())
            {
                ngayBDTC = MinMax.Min(x => x.Ngay);
                ngayKTTC = MinMax.Max(x => x.Ngay);
            }
        }
        private void gv_CTVL_updateCol(string fieldName,TreeListNode node, object val)
        {
            KLTTHangNgay row = tl_CongTacVatLieu.GetDataRecordByNode(node) as KLTTHangNgay;
            string dbString,sheetName=_type==TypeKLHN.CongTac?TDKH.SheetName_KeHoachKinhPhi:SharedControls.ws_VatLieu.Name;
            dbString = $"SELECT Code FROM {_tblHangNgay} WHERE {_colFK} = '{_code}' AND Ngay = '{row.Ngay.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            var dtInDb = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtInDb.Rows.Count > 0)
            {
                row.Code = dtInDb.Rows[0][0].ToString();
            }

            var all = tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>;

            if (node.Level == 0)
            {
                DateTime Ngay = row.Ngay.Value;

                if (fieldName == "KhoiLuongThiCong")
                {
                    double KLnew = double.Parse(val.ToString());
                    double klCons = all.Where(x => x.ParentCode == row.Code && !x.IsNCCConLai).Sum(x => x.KhoiLuongThiCong) ?? 0;
                    if (KLnew < klCons)
                    {
                        MessageShower.ShowWarning("Khối lượng bạn nhập quá nhỏ so với khối lượng các nhà cung cấp!");
                        KLnew += (klCons - KLnew);
                    }
                    //row.KhoiLuongThiCong = KLnew;
                    var cl = all.SingleOrDefault(x => x.ParentCode == row.Code && x.IsNCCConLai);
                    if (cl != null)
                        cl.KhoiLuongThiCong = 0;

                    tl_CongTacVatLieu.RefreshDataSource();

                }

                if (fieldName == col_NCC.FieldName)
                {
                    DuAnHelper.updateNCC(rpCbe_NCC, row);
                }

                if (fieldName == "LyTrinhCaoDo")
                {
                    dbString = $"INSERT OR REPLACE INTO '{_tblHangNgay}' (Code,{_colFK},KhoiLuongKeHoach,KhoiLuongThiCong,Ngay,IsEdited, LyTrinhCaoDo, GhiChu, NhaCungCap) " +
                                $"VALUES (@Code,@{_colFK},@KhoiLuongKeHoach,@KhoiLuongThiCong,@Ngay,@IsEdited, @LyTrinhCaoDo, @GhiChu, @NhaCungCap)";

                    object[] mparams =
                    {
                        row.Code,
                        _code,
                        row.KhoiLuongKeHoach,
                        row.KhoiLuongThiCong,
                        Ngay.Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE),
                        true,
                        row.LyTrinhCaoDo,
                        row.GhiChu,
                        row.NhaCungCap
                    };
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: mparams);
                }

 

                if (fieldName == "KhoiLuongKeHoach")
                {
                    dbString = $"INSERT OR REPLACE INTO '{_tblHangNgay}' (Code,{_colFK},KhoiLuongKeHoach,KhoiLuongThiCong,Ngay,IsEdited, GhiChu, NhaCungCap) " +
                                    $"VALUES (@Code,@{_colFK},@KhoiLuongKeHoach,@KhoiLuongThiCong,@Ngay,@IsEdited, @GhiChu, @NhaCungCap)";

                    object[] mparams =
                    {
                        row.Code,
                        _code,
                        row.KhoiLuongKeHoach,
                        row.KhoiLuongThiCong,
                        Ngay.Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE),
                        true,
                        row.GhiChu,
                        row.NhaCungCap
                    };
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: mparams);

                }
                else
                {
                    dbString = $"INSERT OR REPLACE INTO '{_tblHangNgay}' (Code,{_colFK},KhoiLuongKeHoach,KhoiLuongThiCong,Ngay,IsEdited, GhiChu, NhaCungCap) " +
                                $"VALUES (@Code,@{_colFK},@KhoiLuongKeHoach,@KhoiLuongThiCong,@Ngay,@IsEdited, @GhiChu, @NhaCungCap)";

                    object[] mparams =
                    {
                        row.Code,
                        _code,
                        row.KhoiLuongKeHoach,
                        row.KhoiLuongThiCong,
                        Ngay.Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE),
                        true,
                        row.GhiChu,
                        row.NhaCungCap
                        };
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: mparams);
                    if (_type == TypeKLHN.CongTac)
                        DuAnHelper.UpdateStateTDKHByKhoiLuongThiCong(new string[] { _code });
                    else if (_type == TypeKLHN.Nhom)
                        DuAnHelper.UpdateStateTDKHNhomByKhoiLuongThiCong(new string[] { _code });

                }


                switch (fieldName)
                {
                    case "KhoiLuongKeHoach":

                        if (row.IsNewThiCong)
                        {

                            row.IsNewThiCong = false;
                            //Fcn_UpdateNBD_KTTC(Ngay, _dicNgay["KeHoach"],TDKH.SheetName_KeHoachKinhPhi,"KeHoach",false);
                        }
                        loadData();
                        goto case "GhiChu";
                    case "KhoiLuongThiCong":
                        CapNhatKhoiLuongThiCong();
                        goto case "GhiChu";
                    case "GhiChu":
                        if (row.IsNewThiCong)
                        {

                            row.IsNewThiCong = false;
                        }
    

                        break;
                    case "CodesNCCString":
                        
                        all.RemoveAll(x => x.ParentCode == row.Code);
                        var lsCon = DuAnHelper.GetNCCDetail(row, _DicNccs);
                        all.AddRange(lsCon);
                        tl_CongTacVatLieu.RefreshDataSource();
                        tl_CongTacVatLieu.FocusedNode.Expand();
                        break;
                    default:
                        return;
                }


                Fcn_Update_KTTCDelete();
            }
            else if (node.Level == 1)
            {
                string col;
                switch (fieldName)
                {
                    case "KhoiLuongThiCong":
                        col = "KhoiLuong";
                        break;
                    case "DonGiaThiCong":
                        col = "DonGia";
                        break;
                    default:
                        return;
                }


                var crCha = tl_CongTacVatLieu.GetDataRecordByNode(node.ParentNode) as KLTTHangNgay;
                var allCrNCCs = MyFunction.TryGetObjFromJson<NhaCungCapHangNgayViewModel>(crCha.NhaCungCap);

                if (fieldName == "KhoiLuongThiCong")
                {
                    double KLnew = double.Parse(val.ToString());
                    var sumKLCon = all.Where(x => x.ParentCode == row.ParentCode && !x.IsNCCConLai).Sum(x => x.KhoiLuongThiCong) ?? 0;
                    var KLCha = all.Single(x => x.Code == row.ParentCode).KhoiLuongThiCong ?? 0;
                    if (sumKLCon > KLCha)
                    {
                        MessageShower.ShowWarning("Khối lượng bạn nhập quá lớn so với Khối lượng tổng nên sẽ bị giảm về tối đa có thể!");
                        KLnew -= (sumKLCon - KLCha);
                    }
                    allCrNCCs.Single(x => x.Code == row.CodeNhaCungCap).KhoiLuong = KLnew;
                }
                else
                    allCrNCCs.Single(x => x.Code == row.CodeNhaCungCap).DonGia = long.Parse(val.ToString());

                string jsonNCC = JsonConvert.SerializeObject(allCrNCCs);

                crCha.NhaCungCap = jsonNCC;

                //dbString = $"UPDATE {_tblHangNgay} SET NhaCungCap = JSON_SET(NhaCungCap, '$.{col}', '{KLnew}') WHERE Code = '{row.ParentCode}'";
                dbString = $"UPDATE {_tblHangNgay} SET NhaCungCap = @NhaCungCap WHERE Code = '{row.ParentCode}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { jsonNCC });

                all.RemoveAll(x => x.ParentCode == row.ParentCode);
                var lsCon = DuAnHelper.GetNCCDetail(crCha, _DicNccs);
                all.AddRange(lsCon);
                tl_CongTacVatLieu.RefreshDataSource();
                tl_CongTacVatLieu.FocusedNode.Expand();

                //bt_Update_Click(null, null);

            }    
        }

        
        private void bt_Them_Click(object sender, EventArgs e)
        {
            //if (_type != TypeKLHN.VatLieu)
            //    return;


            DateTime dateSelected = dtp_Ngay.DateTime.Date;
            
            
            string dateString = dateSelected.Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string sheetName = _type == TypeKLHN.CongTac ? TDKH.SheetName_KeHoachKinhPhi : SharedControls.ws_VatLieu.Name;
            List<KLTTHangNgay> lsCongTac = (tl_CongTacVatLieu.DataSource as List<KLTTHangNgay>);


            KLTTHangNgay CongTac = lsCongTac.Where(x => x.Ngay.Value.Date == dateSelected.Date).SingleOrDefault();

            string dbString = $"SELECT * FROM {_tblDonGia} " +
                        $"WHERE {_colFK} = '{_code}' " +
                        $"AND TuNgay <= '{dateString}' AND DenNgay >= '{dateString}'";

            DataRow drDonGia = DataProvider.InstanceTHDA.ExecuteQuery(dbString).AsEnumerable().SingleOrDefault();
            double donGiaThiCongNgay = (drDonGia is null) ? _donGiaThiCong : double.Parse(drDonGia[TDKH.COL_DonGiaThiCong].ToString());
            if (CongTac is null)
            {
                CongTac = new KLTTHangNgay()
                {
                    Code = Guid.NewGuid().ToString(),
                    Ngay = dateSelected,
                    KhoiLuongThiCong = (double)nud_KLTC.Value,
                };
            }
            dbString = $"SELECT Code FROM {_tblHangNgay} WHERE {_colFK} = '{_code}' AND Ngay = '{dateSelected.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";

            var dtInDb = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtInDb.Rows.Count > 0)
            {
                CongTac.Code = dtInDb.Rows[0][0].ToString();
            }


            CongTac.DonGiaKeHoach =(long)Math.Round(_donGiaKeHoach);
            CongTac.DonGiaThiCong = (long)Math.Round(donGiaThiCongNgay);

                lsCongTac.Add(CongTac);

                dbString = $"INSERT OR REPLACE INTO {_tblHangNgay} (Code, {_colFK}, Ngay, " +
                    $"KhoiLuongThiCong, ThanhTienThiCong) VALUES " +
                    $"('{CongTac.Code}', '{_code}', '{dateString}', " +
                    $"'{CongTac.KhoiLuongThiCong}', '{CongTac.ThanhTienKeHoach}') ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                //DateTime NBD = dateSelected.Date < _dicNgay["ThiCong"].Key ? dateSelected : _dicNgay["ThiCong"].Key;
                //DateTime NKT = dateSelected > _dicNgay["ThiCong"].Value ? dateSelected : _dicNgay["ThiCong"].Value;
                //dbString = $"UPDATE {_tbl} " +
                //$"SET NgayBatDauThiCong = '{NBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}',NgayKetThucThiCong = '{NKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                //$"WHERE Code = '{_code}'";
                //SharedControls.ws_VatLieu.Rows[RowIndex][dicKPVL_All[TDKH.COL_NgayBatDauThiCong]].SetValueFromText(NBD.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
                //SharedControls.ws_VatLieu.Rows[RowIndex][dicKPVL_All[TDKH.COL_NgayKetThucThiCong]].SetValueFromText(NKT.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));

                CongTac.KhoiLuongThiCong = (double)nud_KLTC.Value;

                dbString = $"INSERT OR IGNORE INTO {_tblHangNgay} " +
                    $"(Code, {_colFK}, Ngay) " +
                    $"VALUES ('{CongTac.Code}', '{_code}','{dateString}');\r\n" +
                    $"UPDATE {_tblHangNgay} " +
                    $"SET KhoiLuongThiCong = '{nud_KLTC.Value}' " +
                    $"WHERE Code = '{CongTac.Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);   
            
            Fcn_Update_KTTCDelete();

            tl_CongTacVatLieu.RefreshDataSource();
            //tl_CongTacVatLieu.Refresh();
            MessageShower.ShowInformation("Thêm ngày cho thi công hoàn thành!");
        }
        //public event System.EventHandler Bt_Them_Click
        //{
        //    add
        //    {
        //        bt_Them.Click += value;
        //    }
        //    remove
        //    {
        //        bt_Them.Click -= value;
        //    }
        //}
    }
}
