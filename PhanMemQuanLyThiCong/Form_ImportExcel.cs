using DevExpress.Spreadsheet;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.Excel;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using PhanMemQuanLyThiCong.Model.TDKH;
using PM360.Common.Validate;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_ImportExcel : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<Control, string> dic_txtBox_CONGTRINH;
        Dictionary<int, string> dic_congtrinh = new Dictionary<int, string>();
        Dictionary<int, string> dic_hangmuc = new Dictionary<int, string>();
        static string codehangmuc, codecongtrinh, tenhangmuc, tencongtrinh = "", m_CodeDuAn, m_codegiadoan;
        static int RowDonGia = 0;
        private bool CheckAuto { get; set; } = false;
        Dictionary<Control, string> dic_Nhancong;
        Dictionary<Control, string> dic_Maythicong;
        Dictionary<string, Control> DonGiaVL_NC_MTC;
        Dictionary<string, Control> DinhMucChuan;
        Dictionary<Control, string> dic_txtHaophiVT;
        Dictionary<Control, string> dic_txtduthau;
        Dictionary<Control, string> dic_VT;
        Dictionary<string, string> TenSheetVLNCMTC;
        Dictionary<string, string> TenDonViVLNCMTC;
        public delegate void DE_TruyenDataDocFileExcel(DataTable dt, DataTable dt_congtactheogiadoan, int type);
        public DE_TruyenDataDocFileExcel _TruyenDataTuExcel;
        private List<string> lstSheetNames;
        List<string> LIST_loaivattu = new List<string>()
        {
            "Vật liệu",
            "Nhân công",
            "Máy thi công",
            "Ca máy",
            "c.) Máy thi công",
            "b.) Nhân công",
            "a.) Vật liệu",
            "c.) Máy thi công",
            "b.) Nhân công",
            "a) Vật liệu",
            "c) Máy thi công",
            "b) Nhân công",
            "a) Vật liệu",
            "c.)Máy thi công",
            "b.)Nhân công",
            "a.)Vật liệu",
            "c.)Máy thi công",
            "b.)Nhân công",
            "a)Vật liệu",
            "c)Máy thi công",
            "b)Nhân công",
            "a)Vật liệu",
            "Thuế tài nguyên"
        };
        public Form_ImportExcel()
        {
            InitializeComponent();
            #region Validate Control
            this.CausesValidation = false;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            dxValidationProvider.Validate();
            //dxValidationProvider.SetValidationRule(txt_congtrinh_stt, new CustomValidationComboboxRule { ErrorText = "Vui lòng chọn 1 sheet dữ liệu nguồn để đọc", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            //dxValidationProvider_DuThau.SetValidationRule(txt_BatDauDuThau, new CustomValidationTextEditRule { ErrorText = "Vui lòng nhập dòng bắt đầu để đọc dữ liệu Excel nguồn", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            //dxValidationProvider_DuThau.SetValidationRule(txt_KetThucDuThau, new CustomValidationTextEditRule { ErrorText = "Vui lòng nhập dòng bắt đầu để đọc dữ liệu Excel nguồn", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            //dxValidationProvider_DuThau.SetValidationRule(txt_DT_STT, new CustomValidationTextEditRule { ErrorText = "Không được để trống cột số thứ tự", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            //dxValidationProvider_DuThau.SetValidationRule(txt_DT_MaCT, new CustomValidationTextEditRule { ErrorText = "Không được để trống cột mã công tác", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            //dxValidationProvider_DuThau.SetValidationRule(txt_TenCongTac, new CustomValidationTextEditRule { ErrorText = "Không được để trống cột tên công tác", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            //dxValidationProvider_DuThau.SetValidationRule(KhoiLuongTextEdit, new CustomValidationTextEditRule { ErrorText = "Không được để trống cột khối lượng hợp đồng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            //dxValidationProvider_DuThau.SetValidationRule(DonGiaTextEdit, new CustomValidationTextEditRule { ErrorText = "Không được để trống cột đơn giá hợp đồng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            //dxValidationProvider_DuThau.SetValidationRule(ThanhTienTextEdit, new CustomValidationTextEditRule { ErrorText = "Không được để trống cột thành tiền", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            #endregion
        }
        private void Fcn_UpdateHaoPhiThuCong(DataTable dtHaoPhi, string CodeCongTacGD, string TenCongTac, string MaHieu, string DonVi, double DonGia, string LoaiVL)
        {
            if (DonGia == 0)
                return;
            DataRow dt_vattu = dtHaoPhi.NewRow();
            dt_vattu["Code"] = Guid.NewGuid().ToString();
            dt_vattu["CodeCongTac"] = CodeCongTacGD;
            dt_vattu["MaVatLieu"] = MaHieu;
            dt_vattu["VatTu"] = TenCongTac;
            dt_vattu["DonVi"] = DonVi;
            dt_vattu["LoaiVatTu"] = LoaiVL;
            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
            dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
            dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = DonGia;
            dtHaoPhi.Rows.Add(dt_vattu);
        }
        private void Fcn_TruyenDataExcelThuCongMyHouse(Dictionary<Control, string> dic, int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);

            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = 0;

            bool CotMaHieu = txtCTMaDuToan.Text == "" ? false : true;
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string CodeCtrinh = "", CodeHM = "";
            //bool NewCongTrinh = (rg_LayDuLieu.SelectedIndex == 1 && lUE_ToChucCaNhan.EditValue != null) ? false : true;
            int SortIDCT = dt_CTrinh.Rows.Count;
            int SortIDHM = dt_HM.Rows.Count;
            string DonVi = "", codetheogiaidoan = "", TenCongTac = "";
            int RowHaoPhi = 2;
            double DonGiaVLKhac = 0;
            Worksheet sheetThongTin = workbook.Worksheets[cboHPVTtenSheet.Text];
            int lastline_vl = sheetThongTin.GetUsedRange().RowCount;
            int Index = rg_LayDuLieu.SelectedIndex;
            if (Index > 0)
                CodeCtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            for(int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                DonVi = crRow[txtCTDonVi.Text].Value.TextValue;
                string STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{MaHieu}_{TenCongTac}");
                if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    if (CodeCtrinh == "" && Index == 0)
                    {
                        CodeCtrinh = Guid.NewGuid().ToString();
                        DataRow CrowCTrinh = dt_CTrinh.NewRow();
                        CrowCTrinh["Code"] = CodeCtrinh;
                        CrowCTrinh["SortID"] = SortIDCT++;
                        CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                        CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                        dt_CTrinh.Rows.Add(CrowCTrinh);
                    }
                    CodeHM = Guid.NewGuid().ToString();
                    DataRow CrowHM = dt_HM.NewRow();
                    CrowHM["Code"] = CodeHM;
                    CrowHM["SortID"] = SortIDHM++;
                    CrowHM["CodeCongTrinh"] = CodeCtrinh;
                    CrowHM["Ten"] = TenCongTac;
                    dt_HM.Rows.Add(CrowHM);
                }
                else if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (Index > 0)
                        continue;
                    CodeCtrinh = Guid.NewGuid().ToString();
                    DataRow CrowCTrinh = dt_CTrinh.NewRow();
                    CrowCTrinh["Code"] = CodeCtrinh;
                    CrowCTrinh["SortID"] = SortIDCT++;
                    CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                    CrowCTrinh["Ten"] = TenCongTac;
                    dt_CTrinh.Rows.Add(CrowCTrinh);
                }
                else if (CodeHM == "")
                {
                    if (Index == 0)
                    {
                        CodeCtrinh = Guid.NewGuid().ToString();
                        DataRow CrowCTrinh = dt_CTrinh.NewRow();
                        CrowCTrinh["Code"] = CodeCtrinh;
                        CrowCTrinh["SortID"] = SortIDCT++;
                        CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                        CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                        dt_CTrinh.Rows.Add(CrowCTrinh);
                    }
                    CodeHM = Guid.NewGuid().ToString();
                    DataRow CrowHM = dt_HM.NewRow();
                    CrowHM["Code"] = CodeHM;
                    CrowHM["SortID"] = SortIDHM++;
                    CrowHM["CodeCongTrinh"] = CodeCtrinh;
                    CrowHM["Ten"] = $"Hạng mục {dt_HM.Rows.Count}"; ;
                    dt_HM.Rows.Add(CrowHM);
                    DonGiaVLKhac = crRow[txtCTDGVatLieuKhac.Text].Value.NumericValue;
                    if (MaHieu != "" && STT != "")
                    {
                        string CodeCongTacGD = Guid.NewGuid().ToString();
                        Fcn_UpdateCongTacThuCongMyHouse(SortId, CodeCongTacGD, dt, dt_DLG, dt_congtactheogiaidoan, crRow, i, CodeHM);
                        if (MaHieu.ToUpper() == "TT")
                        {

                            if (txt_NC_Dongia.Text != "")
                            {
                                double DonGia = crRow[txt_NC_Dongia.Text].Value.IsNumeric ? crRow[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCongTacGD, TenCongTac, MaHieu, DonVi, DonGia, "Nhân công");
                            }
                            if (txt_VL_Dongia.Text != "")
                            {
                                double DonGia = crRow[txt_VL_Dongia.Text].Value.IsNumeric ? crRow[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCongTacGD, TenCongTac, MaHieu, DonVi, DonGia, "Vật liệu");
                            }
                            if (txt_MTC_Dongia.Text != "")
                            {
                                double DonGia = crRow[txt_MTC_Dongia.Text].Value.IsNumeric ? crRow[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCongTacGD, TenCongTac, MaHieu, DonVi, DonGia, "Máy thi công");
                            }

                        }
                        SortId++;
                        int End = fcn_updatenhomdiendaiThuCong(i + 1, lastLine, CodeCongTacGD, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (DonGiaVLKhac == 0)
                        {
                            RowHaoPhi = fcn_updateHaophiVTMyHouse(sheetThongTin, dtHaoPhi, TenCongTac, lastline_vl, CodeCongTacGD, RowHaoPhi);
                        }
                        else
                        {
                            DataRow dt_vattu = dtHaoPhi.NewRow();
                            dt_vattu["Code"] = Guid.NewGuid();
                            dt_vattu["CodeCongTac"] = CodeCongTacGD;
                            dt_vattu["MaVatLieu"] = MaHieu;
                            dt_vattu["VatTu"] = TenCongTac;
                            dt_vattu["DonVi"] = DonVi;
                            dt_vattu["DonGia"] = DonGiaVLKhac;
                            dt_vattu["LoaiVatTu"] = "Vật liệu";
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                            dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                            dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dtHaoPhi.Rows.Add(dt_vattu);
                        }
                        if (End != 0)
                        {
                            i = End - 1;
                        }
                    }
                    else if (MaHieu == te_Nhom.Text)
                    {
                        string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                        DataRow NewRow = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeNhomCT;
                        NewRow["CodeHangMuc"] = CodeHM;
                        NewRow["Ten"] = tennhom;
                        NewRow["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                        if (Te_DonGiaNhom.Text.HasValue())
                        {
                            double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                            if (DonGiaNhom > 0)
                                NewRow["DonGia"] = DonGiaNhom;
                        }
                        double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                        if (KLNhom > 0)
                            NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;
                        NewRow["SortId"] = SortIdNhom++;
                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                        if (txt_congtrinh_sttND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        NewRow["GhiChuBoSungJson"] = encryptedStr;
                        dt_NhomCT.Rows.Add(NewRow);
                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            Row CrowCTNhom = ws.Rows[j];
                            string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                            WaitFormHelper.ShowWaitForm($"{j + 1}.{Mahieu}_{TenCongTac}");
                            if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                Mahieu.ToUpper() == "THM" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                            {
                                i = j - 1;
                                break;

                            }
                            else
                            {
                                DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                DonGiaVLKhac = CrowCTNhom[txtCTDGVatLieuKhac.Text].Value.NumericValue;
                                Fcn_UpdateCongTacThuCongMyHouse(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeHM, CodeNhomCT);
                                if (Mahieu == "TT")
                                {
                                    if (txt_NC_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                    }
                                    if (txt_VL_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                    }
                                    if (txt_MTC_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                    }

                                }
                                SortId++;
                                int End = fcn_updatenhomdiendaiThuCong(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                TenCongTac = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                                if (DonGiaVLKhac == 0)
                                {
                                    RowHaoPhi = fcn_updateHaophiVTMyHouse(sheetThongTin, dtHaoPhi, TenCongTac, lastline_vl, codetheogiaidoan, RowHaoPhi);
                                }
                                else
                                {
                                    DataRow dt_vattu = dtHaoPhi.NewRow();
                                    dt_vattu["Code"] = Guid.NewGuid();
                                    dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                    dt_vattu["MaVatLieu"] = Mahieu;
                                    dt_vattu["VatTu"] = TenCongTac;
                                    dt_vattu["DonVi"] = DonVi;
                                    dt_vattu["DonGia"] = DonGiaVLKhac;
                                    dt_vattu["LoaiVatTu"] = "Vật liệu";
                                    dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                    dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                    dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dtHaoPhi.Rows.Add(dt_vattu);
                                }
                                if (End != 0)
                                {
                                    i = End;
                                    j = End - 1;
                                }
                            }
                        }
                    }
                    else if (MaHieu == te_PhanTuyen.Text)
                    {
                        string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                        DataRow NewRow = dt_Tuyen.NewRow();
                        string CodeTuyen = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeTuyen;
                        NewRow["CodeHangMuc"] = CodeHM;
                        NewRow["Ten"] = TenTuyen;
                        NewRow["SortId"] = SortIdTuyen++;
                        dt_Tuyen.Rows.Add(NewRow);

                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            Row CrowCTNhom = ws.Rows[j];
                            string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                            if (Mahieu == te_PhanTuyen.Text || Mahieu == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu == MyConstant.CONST_TYPE_HANGMUC)
                            {
                                i = j - 1;
                                break;

                            }
                            else if (Mahieu == te_Nhom.Text)
                            {
                                string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                                DataRow NewRowNhom = dt_NhomCT.NewRow();
                                string CodeNhomCT = Guid.NewGuid().ToString();
                                NewRowNhom["Code"] = CodeNhomCT;
                                NewRowNhom["CodeHangMuc"] = CodeHM;
                                NewRowNhom["Ten"] = tennhom;
                                NewRowNhom["DonVi"] = CrowCTNhom[txtCTDonVi.Text].Value.TextValue;
                                NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                                NewRowNhom["SortId"] = SortIdNhom++;
                                if (Te_DonGiaNhom.Text.HasValue())
                                {
                                    double.TryParse(CrowCTNhom[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                    if (DonGiaNhom > 0)
                                        NewRowNhom["DonGia"] = DonGiaNhom;
                                }
                                double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                                if (KLNhom > 0)
                                    NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                                TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                JsonGhiChu.STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString();
                                if (txt_congtrinh_sttND.Text.HasValue())
                                {
                                    JsonGhiChu.STTND = CrowCTNhom[txt_congtrinh_sttND.Text].Value.ToString();
                                }
                                JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                                var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                                dt_NhomCT.Rows.Add(NewRowNhom);
                                for (int k = j + 1; k <= lastLine; k++)
                                {
                                    Row CrowCTNhomNew = ws.Rows[k];
                                    Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                    DonGiaVLKhac = CrowCTNhomNew[txtCTDGVatLieuKhac.Text].Value.NumericValue;
                                    if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                        Mahieu.ToUpper() == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                                    {
                                        j = k - 1;
                                        i = j - 1;
                                        break;

                                    }
                                    else
                                    {
                                        DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                        codetheogiaidoan = Guid.NewGuid().ToString();
                                        Fcn_UpdateCongTacThuCongMyHouse(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, k, CodeHM, CodeNhomCT);
                                        if (Mahieu == "TT")
                                        {
                                            if (txt_NC_Dongia.Text != "")
                                            {
                                                double DonGia = CrowCTNhomNew[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                            }
                                            if (txt_VL_Dongia.Text != "")
                                            {
                                                double DonGia = CrowCTNhomNew[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                            }
                                            if (txt_MTC_Dongia.Text != "")
                                            {
                                                double DonGia = CrowCTNhomNew[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                            }

                                        }
                                        SortId++;
                                        int End = fcn_updatenhomdiendaiThuCong(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                        TenCongTac = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                        if (DonGiaVLKhac == 0)
                                        {
                                            RowHaoPhi = fcn_updateHaophiVTMyHouse(sheetThongTin, dtHaoPhi, TenCongTac, lastline_vl, codetheogiaidoan, RowHaoPhi);
                                        }
                                        else
                                        {
                                            DataRow dt_vattu = dtHaoPhi.NewRow();
                                            dt_vattu["Code"] = Guid.NewGuid();
                                            dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                            dt_vattu["MaVatLieu"] = Mahieu;
                                            dt_vattu["VatTu"] = TenCongTac;
                                            dt_vattu["DonVi"] = DonVi;
                                            dt_vattu["DonGia"] = DonGiaVLKhac;
                                            dt_vattu["LoaiVatTu"] = "Vật liệu";
                                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                            dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                            dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtHaoPhi.Rows.Add(dt_vattu);
                                        }
                                        if (End != 0)
                                        {
                                            j = End;
                                            k = End - 1;
                                            i = j - 1;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                                DonGiaVLKhac = CrowCTNhom[txtCTDGVatLieuKhac.Text].Value.NumericValue;
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                Fcn_UpdateCongTacThuCongMyHouse(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeHM, CodeTuyen: CodeTuyen);
                                if (Mahieu == "TT")
                                {
                                    if (txt_NC_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                    }
                                    if (txt_VL_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                    }
                                    if (txt_MTC_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                    }

                                }
                                SortId++;
                                int End = fcn_updatenhomdiendaiThuCong(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                TenCongTac = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                                if (DonGiaVLKhac == 0)
                                {
                                    RowHaoPhi = fcn_updateHaophiVTMyHouse(sheetThongTin, dtHaoPhi, TenCongTac, lastline_vl, codetheogiaidoan, RowHaoPhi);
                                }
                                else
                                {
                                    DataRow dt_vattu = dtHaoPhi.NewRow();
                                    dt_vattu["Code"] = Guid.NewGuid();
                                    dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                    dt_vattu["MaVatLieu"] = Mahieu;
                                    dt_vattu["VatTu"] = TenCongTac;
                                    dt_vattu["DonVi"] = DonVi;
                                    dt_vattu["DonGia"] = DonGiaVLKhac;
                                    dt_vattu["LoaiVatTu"] = "Vật liệu";
                                    dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                    dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                    dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dtHaoPhi.Rows.Add(dt_vattu);
                                }
                                if (End != 0)
                                {
                                    i = End;
                                    j = End - 1;
                                }
                            }
                        }


                    }
                }
                else if (MaHieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = CodeHM;
                    NewRow["Ten"] = tennhom;
                    NewRow["SortId"] = SortIdNhom++;
                    if (Te_DonGiaNhom.Text.HasValue())
                    {
                        double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                        if (DonGiaNhom > 0)
                            NewRow["DonGia"] = DonGiaNhom;
                    }
                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                    if (KLNhom > 0)
                        NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;

                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                    if (txt_congtrinh_sttND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRow["GhiChuBoSungJson"] = encryptedStr;
                    dt_NhomCT.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        WaitFormHelper.ShowWaitForm($"{j + 1}.{Mahieu}_{TenCongTac}");
                        if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                            Mahieu.ToUpper() == "THM" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                        {
                            i = j - 1;
                            break;

                        }
                        else
                        {
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            DonGiaVLKhac = CrowCTNhom[txtCTDGVatLieuKhac.Text].Value.NumericValue;
                            Fcn_UpdateCongTacThuCongMyHouse(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeHM, CodeNhomCT);
                            if (Mahieu == "TT")
                            {
                                if (txt_NC_Dongia.Text != "")
                                {
                                    double DonGia = CrowCTNhom[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                }
                                if (txt_VL_Dongia.Text != "")
                                {
                                    double DonGia = CrowCTNhom[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                }
                                if (txt_MTC_Dongia.Text != "")
                                {
                                    double DonGia = CrowCTNhom[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                }

                            }
                            SortId++;
                            int End = fcn_updatenhomdiendaiThuCong(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            TenCongTac = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            if (DonGiaVLKhac == 0)
                            {
                                RowHaoPhi = fcn_updateHaophiVTMyHouse(sheetThongTin, dtHaoPhi, TenCongTac, lastline_vl, codetheogiaidoan, RowHaoPhi);
                            }
                            else
                            {
                                DataRow dt_vattu = dtHaoPhi.NewRow();
                                dt_vattu["Code"] = Guid.NewGuid();
                                dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                dt_vattu["MaVatLieu"] = Mahieu;
                                dt_vattu["VatTu"] = TenCongTac;
                                dt_vattu["DonVi"] = DonVi;
                                dt_vattu["DonGia"] = DonGiaVLKhac;
                                dt_vattu["LoaiVatTu"] = "Vật liệu";
                                dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dtHaoPhi.Rows.Add(dt_vattu);
                            }
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }
                }
                else if (MaHieu == te_PhanTuyen.Text)
                {
                    string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = CodeHM;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);

                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_PhanTuyen.Text || Mahieu == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        else if (Mahieu == te_Nhom.Text)
                        {
                            string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRowNhom = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRowNhom["Code"] = CodeNhomCT;
                            NewRowNhom["CodeHangMuc"] = CodeHM;
                            NewRowNhom["Ten"] = tennhom;
                            NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                            NewRowNhom["SortId"] = SortIdNhom++;
                            if (Te_DonGiaNhom.Text.HasValue())
                            {
                                double.TryParse(CrowCTNhom[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                if (DonGiaNhom > 0)
                                    NewRowNhom["DonGia"] = DonGiaNhom;
                            }
                            double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                            if (KLNhom > 0)
                                NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;

                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRow["GhiChuBoSungJson"] = encryptedStr;
                            dt_NhomCT.Rows.Add(NewRowNhom);
                            for (int k = j + 1; k <= lastLine; k++)
                            {
                                Row CrowCTNhomNew = ws.Rows[k];
                                Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                DonGiaVLKhac = CrowCTNhomNew[txtCTDGVatLieuKhac.Text].Value.NumericValue;
                                if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                    Mahieu.ToUpper() == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                                {
                                    j = k - 1;
                                    i = j - 1;
                                    break;

                                }
                                else
                                {
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    Fcn_UpdateCongTacThuCongMyHouse(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, k, CodeHM, CodeNhomCT);
                                    if (Mahieu == "TT")
                                    {
                                        if (txt_NC_Dongia.Text != "")
                                        {
                                            double DonGia = CrowCTNhomNew[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                        }
                                        if (txt_VL_Dongia.Text != "")
                                        {
                                            double DonGia = CrowCTNhomNew[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                        }
                                        if (txt_MTC_Dongia.Text != "")
                                        {
                                            double DonGia = CrowCTNhomNew[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                        }

                                    }
                                    SortId++;
                                    int End = fcn_updatenhomdiendaiThuCong(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    TenCongTac = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    if (DonGiaVLKhac == 0)
                                    {
                                        RowHaoPhi = fcn_updateHaophiVTMyHouse(sheetThongTin, dtHaoPhi, TenCongTac, lastline_vl, codetheogiaidoan, RowHaoPhi);
                                    }
                                    else
                                    {
                                        DataRow dt_vattu = dtHaoPhi.NewRow();
                                        dt_vattu["Code"] = Guid.NewGuid();
                                        dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                        dt_vattu["MaVatLieu"] = Mahieu;
                                        dt_vattu["VatTu"] = TenCongTac;
                                        dt_vattu["DonVi"] = DonVi;
                                        dt_vattu["DonGia"] = DonGiaVLKhac;
                                        dt_vattu["LoaiVatTu"] = "Vật liệu";
                                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtHaoPhi.Rows.Add(dt_vattu);
                                    }
                                    if (End != 0)
                                    {
                                        j = End;
                                        k = End - 1;
                                        i = j - 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            DonGiaVLKhac = CrowCTNhom[txtCTDGVatLieuKhac.Text].Value.NumericValue;
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            Fcn_UpdateCongTacThuCongMyHouse(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeHM, CodeTuyen: CodeTuyen);
                            if (Mahieu == "TT")
                            {
                                if (txt_NC_Dongia.Text != "")
                                {
                                    double DonGia = CrowCTNhom[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                }
                                if (txt_VL_Dongia.Text != "")
                                {
                                    double DonGia = CrowCTNhom[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                }
                                if (txt_MTC_Dongia.Text != "")
                                {
                                    double DonGia = CrowCTNhom[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                }

                            }
                            SortId++;
                            int End = fcn_updatenhomdiendaiThuCong(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            TenCongTac = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            if (DonGiaVLKhac == 0)
                            {
                                RowHaoPhi = fcn_updateHaophiVTMyHouse(sheetThongTin, dtHaoPhi, TenCongTac, lastline_vl, codetheogiaidoan, RowHaoPhi);
                            }
                            else
                            {
                                DataRow dt_vattu = dtHaoPhi.NewRow();
                                dt_vattu["Code"] = Guid.NewGuid();
                                dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                dt_vattu["MaVatLieu"] = Mahieu;
                                dt_vattu["VatTu"] = TenCongTac;
                                dt_vattu["DonVi"] = DonVi;
                                dt_vattu["DonGia"] = DonGiaVLKhac;
                                dt_vattu["LoaiVatTu"] = "Vật liệu";
                                dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dtHaoPhi.Rows.Add(dt_vattu);
                            }
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }


                }
                else if (MaHieu != "" && STT != "")
                {
                    string CodeCongTacGD = Guid.NewGuid().ToString();
                    DonGiaVLKhac = crRow[txtCTDGVatLieuKhac.Text].Value.NumericValue;
                    Fcn_UpdateCongTacThuCongMyHouse(SortId, CodeCongTacGD, dt, dt_DLG, dt_congtactheogiaidoan, crRow, i, CodeHM);
                    if (MaHieu.ToUpper() == "TT")
                    {

                        if (txt_NC_Dongia.Text != "")
                        {
                            double DonGia = crRow[txt_NC_Dongia.Text].Value.IsNumeric ? crRow[txt_NC_Dongia.Text].Value.NumericValue : 0;
                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCongTacGD, TenCongTac, MaHieu, DonVi, DonGia, "Nhân công");
                        }
                        if (txt_VL_Dongia.Text != "")
                        {
                            double DonGia = crRow[txt_VL_Dongia.Text].Value.IsNumeric ? crRow[txt_VL_Dongia.Text].Value.NumericValue : 0;
                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCongTacGD, TenCongTac, MaHieu, DonVi, DonGia, "Vật liệu");
                        }
                        if (txt_MTC_Dongia.Text != "")
                        {
                            double DonGia = crRow[txt_MTC_Dongia.Text].Value.IsNumeric ? crRow[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCongTacGD, TenCongTac, MaHieu, DonVi, DonGia, "Máy thi công");
                        }

                    }
                    SortId++;
                    int End = fcn_updatenhomdiendaiThuCong(i + 1, lastLine, CodeCongTacGD, dt_NhomDienDai, dt_Chitietcongtaccon);

                    if (DonGiaVLKhac == 0)
                    {
                        RowHaoPhi = fcn_updateHaophiVTMyHouse(sheetThongTin, dtHaoPhi, TenCongTac, lastline_vl, CodeCongTacGD, RowHaoPhi);
                    }
                    else
                    {
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = Guid.NewGuid();
                        dt_vattu["CodeCongTac"] = CodeCongTacGD;
                        dt_vattu["MaVatLieu"] = MaHieu;
                        dt_vattu["VatTu"] = TenCongTac;
                        dt_vattu["DonVi"] = DonVi;
                        dt_vattu["DonGia"] = DonGiaVLKhac;
                        dt_vattu["LoaiVatTu"] = "Vật liệu";
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                    if (End != 0)
                    {
                        i = End - 1;
                    }
                }
                else
                {
                    continue;
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_CTrinh, MyConstant.TBL_THONGTINCONGTRINH);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_HM, MyConstant.TBL_THONGTINHANGMUC);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }
            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();

        }
        private void Fcn_TruyenDataExcelThuCongGT(int firstLine, int lastLine)
        {
            foreach (var item in dic_txtBox_CONGTRINH)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_TenDienDaiTuDo}";
            DataTable dt_TenDienDaiTuDo = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);

            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = 0;

            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            int SortIDCT = dt_CTrinh.Rows.Count;
            int SortIDHM = dt_HM.Rows.Count;
            string DonVi = string.Empty, TenCongTac = string.Empty, CodeCtrinh = string.Empty, CodeHM = string.Empty, CodeDDCT = string.Empty,
                CodeDDNhom = string.Empty, CodeDD = string.Empty,
                CodeDDTuyen = string.Empty;
            int Index = rg_LayDuLieu.SelectedIndex;
            bool TT = te_TTDocVao.Text.HasValue();
            if (Index > 0)
                CodeCtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            int STT = 0;
            dt_NhomDienDai.Clear();
            dt_Chitietcongtaccon.Clear();
            dt_congtactheogiaidoan.Clear();
            dt.Clear();
            dt_DLG.Clear();
            dt_NhomCT.Clear();
            dtHaoPhi.Clear();
            dt_TenDienDaiTuDo.Clear();
            double TTDocVao = 0;
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                DonVi = txtCTDonVi.Text == "" ? default : crRow[txtCTDonVi.Text].Value.TextValue;
                string _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                int.TryParse(_STT, out STT);
                WaitFormHelper.ShowWaitForm($"{_STT}_{TenCongTac}");
                if (STT > 0)
                {
                    CodeDDCT = string.Empty;
                    CodeDDNhom = string.Empty;
                    CodeDDTuyen = string.Empty;
                    CodeDD = string.Empty;
                    if (string.IsNullOrEmpty(CodeCtrinh) && Index == 0)
                    {
                        CodeCtrinh = Guid.NewGuid().ToString();
                        DataRow CrowCTrinh = dt_CTrinh.NewRow();
                        CrowCTrinh["Code"] = CodeCtrinh;
                        CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                        CrowCTrinh["SortID"] = SortIDCT++;
                        CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                        dt_CTrinh.Rows.Add(CrowCTrinh);
                    }
                    CodeHM = Guid.NewGuid().ToString();
                    DataRow CrowHM = dt_HM.NewRow();
                    CrowHM["Code"] = CodeHM;
                    CrowHM["SortID"] = SortIDHM++;
                    CrowHM["CodeCongTrinh"] = CodeCtrinh;
                    CrowHM["Ten"] = TenCongTac;
                    dt_HM.Rows.Add(CrowHM);
                }
                else if (STT == 0)
                {
                    if (_STT.ToUpper() == MyConstant.TYPEROW_CongTrinh)
                    {
                        CodeDDCT = string.Empty;
                        CodeDDNhom = string.Empty;
                        CodeDDTuyen = string.Empty;
                        CodeDD = string.Empty;
                        if (Index > 0)
                            continue;
                        CodeCtrinh = Guid.NewGuid().ToString();
                        DataRow CrowCTrinh = dt_CTrinh.NewRow();
                        CrowCTrinh["Code"] = CodeCtrinh;
                        CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                        CrowCTrinh["SortID"] = SortIDCT++;
                        CrowCTrinh["Ten"] = TenCongTac;
                        dt_CTrinh.Rows.Add(CrowCTrinh);
                        CodeHM = string.Empty;
                    }
                    else if (_STT.ToUpper() == MyConstant.TYPEROW_HangMuc)
                    {
                        CodeDDCT = string.Empty;
                        CodeDDNhom = string.Empty;
                        CodeDDTuyen = string.Empty;
                        if (string.IsNullOrEmpty(CodeCtrinh) && Index == 0)
                        {
                            CodeCtrinh = Guid.NewGuid().ToString();
                            DataRow CrowCTrinh = dt_CTrinh.NewRow();
                            CrowCTrinh["Code"] = CodeCtrinh;
                            CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                            CrowCTrinh["SortID"] = SortIDCT++;
                            CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                            dt_CTrinh.Rows.Add(CrowCTrinh);
                        }
                        CodeHM = Guid.NewGuid().ToString();
                        DataRow CrowHM = dt_HM.NewRow();
                        CrowHM["Code"] = CodeHM;
                        CrowHM["SortID"] = SortIDHM++;
                        CrowHM["CodeCongTrinh"] = CodeCtrinh;
                        CrowHM["Ten"] = TenCongTac;
                        dt_HM.Rows.Add(CrowHM);
                    }
                    else if (_STT == Te_DDCT.Text)
                    {
                        string _STTPre = ws.Rows[i - 1][txt_congtrinh_stt.Text].Value.ToString();
                        DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                        if (_STTPre == "DD")
                            CrowDD["CodeDienDaiCha"] = CodeDD;
                        CodeDDCT = Guid.NewGuid().ToString();
                        CrowDD["Code"] = CodeDDCT;
                        CrowDD["CodeDuAn"] = m_CodeDuAn;
                        CrowDD["Ten"] = TenCongTac;
                        dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                    }      
                    else if (_STT =="DD")
                    {
                        string _STTPre = ws.Rows[i - 1][txt_congtrinh_stt.Text].Value.ToString();
                        DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                        if (_STTPre == "DD")
                            CrowDD["CodeDienDaiCha"] = CodeDD;
                        CodeDD = Guid.NewGuid().ToString();
                        CrowDD["Code"] = CodeDD;
                        CrowDD["CodeDuAn"] = m_CodeDuAn;
                        CrowDD["Ten"] = TenCongTac;
                        dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                        //if(_STTPre == Te_DDCT.Text || _STTPre == "DD")
                        //{
                        //    DataRow CrowDDCon = dt_TenDienDaiTuDo.NewRow();
                        //    CrowDDCon["CodeDuAn"] = m_CodeDuAn;
                        //    CrowDDCon["Ten"] = ws.Rows[i - 1][txtCTTenCongTac.Text].Value.TextValue;
                        //    CrowDDCon["CodeDienDaiCha"] = CrowDD;
                        //    CodeDDCT = Guid.NewGuid().ToString();
                        //    CrowDDCon["Code"] = CrowDD;
                        //    dt_TenDienDaiTuDo.Rows.Add(CrowDDCon);
                        //}
                    }
                    else if (_STT == Te_DDNhom.Text)
                    {
                        string _STTPre = ws.Rows[i - 1][txt_congtrinh_stt.Text].Value.ToString();
                        DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                        if (_STTPre == "DD")
                            CrowDD["CodeDienDaiCha"] = CodeDD;
                        CodeDDNhom = Guid.NewGuid().ToString();
                        CrowDD["Code"] = CodeDDNhom;
                        CrowDD["CodeDuAn"] = m_CodeDuAn;
                        CrowDD["Ten"] = TenCongTac;
                        dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                    }
                    else if (_STT == Te_DDTuyen.Text)
                    {
                        string _STTPre = ws.Rows[i - 1][txt_congtrinh_stt.Text].Value.ToString();
                        DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                        if ( _STTPre == "DD") 
                            CrowDD["CodeDienDaiCha"] =CodeDD;
                        CodeDDTuyen = Guid.NewGuid().ToString();
                        CrowDD["Code"] = CodeDDTuyen;
                        CrowDD["CodeDuAn"] = m_CodeDuAn;
                        CrowDD["Ten"] = TenCongTac;
                        dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                        //if (_STTPre == Te_DDTuyen.Text || _STTPre == "DD")
                        //{
                        //    DataRow CrowDDCon = dt_TenDienDaiTuDo.NewRow();
                        //    CrowDDCon["CodeDuAn"] = m_CodeDuAn;
                        //    CrowDDCon["Ten"] = ws.Rows[i - 1][txtCTTenCongTac.Text].Value.TextValue;
                        //    CrowDDCon["CodeDienDaiCha"] = CodeDDTuyen;
                        //    CodeDDTuyen = Guid.NewGuid().ToString();
                        //    CrowDDCon["Code"] = CodeDDTuyen;
                        //    dt_TenDienDaiTuDo.Rows.Add(CrowDDCon);
                        //}
                    }
                    else
                    {
                        int count = _STT.Count(x => x == '.');
                        if (string.IsNullOrEmpty(CodeHM))
                        {
                            if (string.IsNullOrEmpty(CodeCtrinh) && Index == 0)
                            {
                                CodeCtrinh = Guid.NewGuid().ToString();
                                DataRow CrowCTrinh = dt_CTrinh.NewRow();
                                CrowCTrinh["Code"] = CodeCtrinh;
                                CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                                CrowCTrinh["SortID"] = SortIDCT++;
                                CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                                dt_CTrinh.Rows.Add(CrowCTrinh);
                            }
                            CodeHM = Guid.NewGuid().ToString();
                            DataRow CrowHM = dt_HM.NewRow();
                            CrowHM["Code"] = CodeHM;
                            CrowHM["SortID"] = SortIDHM++;
                            CrowHM["CodeCongTrinh"] = CodeCtrinh;
                            CrowHM["Ten"] = $"Hạng mục {dt_HM.Rows.Count}";
                            dt_HM.Rows.Add(CrowHM);
                        }
                        if (count == 1)
                        {
                            //string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRow = dt_Tuyen.NewRow();
                            string CodeTuyen = Guid.NewGuid().ToString();
                            NewRow["Code"] = CodeTuyen;
                            NewRow["CodeHangMuc"] = CodeHM;
                            NewRow["Ten"] = TenCongTac;
                            NewRow["SortId"] = SortIdTuyen++;
                            if (!string.IsNullOrEmpty(CodeDDTuyen))
                            {
                                NewRow["CodeDienDai"] = CodeDDTuyen;
                                CodeDDTuyen = string.Empty;
                            }
                            dt_Tuyen.Rows.Add(NewRow);
                            for (int j = i + 1; j <= lastLine; j++)
                            {
                                crRow = ws.Rows[j];
                                _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                int.TryParse(_STT, out STT);
                                count = _STT.Count(x => x == '.');
                                string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
                                if (STT > 0 || count == 1 ||
                                     _STT == Te_DDTuyen.Text || _STT.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH || _STT.ToUpper() == MyConstant.TYPEROW_HangMuc)
                                {
                                    i = j - 1;
                                    break;
                                }
                                else if (_STT == Te_DDNhom.Text)
                                {
                                    CodeDDNhom = Guid.NewGuid().ToString();
                                    DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                                    CrowDD["Code"] = CodeDDNhom;
                                    CrowDD["CodeDuAn"] = m_CodeDuAn;
                                    CrowDD["Ten"] = crRow[txtCTTenCongTac.Text].Value.ToString();
                                    dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                                }
                                else if (_STT == Te_DDCT.Text)
                                {
                                    CodeDDCT = Guid.NewGuid().ToString();
                                    DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                                    CrowDD["Code"] = CodeDDCT;
                                    CrowDD["CodeDuAn"] = m_CodeDuAn;
                                    CrowDD["Ten"] = crRow[txtCTTenCongTac.Text].Value.ToString();
                                    dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                                }
                                else if (count == 2)
                                {
                                    i = j;
                                    //string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                                    DataRow NewRowNhom = dt_NhomCT.NewRow();
                                    string CodeNhomCT = Guid.NewGuid().ToString();
                                    NewRowNhom["Code"] = CodeNhomCT;
                                    NewRowNhom["CodeHangMuc"] = CodeHM;
                                    NewRowNhom["Ten"] = tencongtac;
                                    NewRowNhom["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                                    NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                                    if (!string.IsNullOrEmpty(CodeDDNhom))
                                    {
                                        NewRowNhom["CodeDienDai"] = CodeDDNhom;
                                        CodeDDNhom = string.Empty;
                                    }
                                    if (Te_DonGiaNhom.Text.HasValue())
                                    {
                                        double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                        if (DonGiaNhom > 0)
                                            NewRowNhom["DonGia"] = DonGiaNhom;
                                    }
                                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                                    if (KLNhom > 0)
                                        NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;

                                    NewRowNhom["SortId"] = SortIdNhom++;
                                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                    if (txt_congtrinh_sttND.Text.HasValue())
                                    {
                                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                    }
                                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                    NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                                    dt_NhomCT.Rows.Add(NewRowNhom);
                                    for (int k = j + 1; k <= lastLine; k++)
                                    {
                                        crRow = ws.Rows[k];
                                        _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                        int.TryParse(_STT, out STT);
                                        count = _STT.Count(x => x == '.');
                                        tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
                                        if (STT > 0 || count <= 2 && count > 0 || _STT == Te_DDNhom.Text ||
                                            _STT.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH || _STT.ToUpper() == MyConstant.TYPEROW_HangMuc)
                                        {
                                            j = k - 1;
                                            i = j - 1;
                                            break;
                                        }
                                        else if (_STT == Te_DDCT.Text)
                                        {
                                            CodeDDCT = Guid.NewGuid().ToString();
                                            DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                                            CrowDD["Code"] = CodeDDCT;
                                            CrowDD["CodeDuAn"] = m_CodeDuAn;
                                            CrowDD["Ten"] = crRow[txtCTTenCongTac.Text].Value.ToString();
                                            dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                                        }
                                        else if (count > 2)
                                        {
                                            j = k;
                                            i = j;
                                            string MaHieu = txtCTMaDuToan.Text.HasValue() ? crRow[txtCTMaDuToan.Text].Value.ToString() : string.Empty;
                                            string code = Guid.NewGuid().ToString();
                                            DataRow dtRow = dt.NewRow();
                                            DataRow dtRowDLG = dt_DLG.NewRow();
                                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                            dtRow["Code"] = code;
                                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                                            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                            double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLCT);
                                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCT;
                                            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
                                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = string.IsNullOrEmpty(MaHieu) ? "TT" : MaHieu;
                                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
                                            dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                            dtRow["CodePhanTuyen"] = CodeTuyen;
                                            if (TT)
                                            {
                                                double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                                dtRow["ThanhTienDocVao"] = TTDocVao;
                                            }
                                            if (!string.IsNullOrEmpty(CodeDDCT))
                                            {
                                                dtRow["CodeDienDai"] = CodeDDCT;
                                                CodeDDCT = string.Empty;
                                            }
                                            if (txtCTDGVatLieu.Text.HasValue())
                                            {
                                                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                            }

                                            if (txtCTDGNhanCong.Text.HasValue())
                                            {
                                                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                            }

                                            if (txtCTDGMay.Text.HasValue())
                                            {
                                                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                            }
                                            if (txtCTKhoiLuongDocVao.Text.HasValue())
                                            {
                                                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                                if (KLDV > 0)
                                                    dtRow["KhoiLuongDocVao"] = KLDV;
                                                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                                    dtRow["KhoiLuongDocVao"] = 0;
                                            }
                                            TDKH_GhiChuBoSungJson JsonGhiChuCT = new TDKH_GhiChuBoSungJson();
                                            JsonGhiChuCT.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                            if (txt_congtrinh_sttND.Text.HasValue())
                                            {
                                                JsonGhiChuCT.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                            }
                                            JsonGhiChuCT.CodeDanhMucCongTac = code;
                                            var encryptedStrCT = JsonConvert.SerializeObject(JsonGhiChuCT);
                                            dtRow["GhiChuBoSungJson"] = encryptedStrCT;
                                            double DonGia = 0;
                                            if (DonGiaHD.Text.HasValue())
                                            {
                                                DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                            }
                                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                                            dtRow["Modified"] = true;
                                            dtRow["PhatSinh"] = false;
                                            dtRow_congtactheogiadoan["Modified"] = true;
                                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dt.Rows.Add(dtRow);
                                            dt_DLG.Rows.Add(dtRowDLG);
                                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                        }
                                        else if (TT && !string.IsNullOrEmpty(tencongtac))
                                        {
                                            bool TryTT = double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                            if (TTDocVao > 0)
                                            {
                                                j = k;
                                                i = j;
                                                string MaHieu = "TRỐNG";
                                                string code = Guid.NewGuid().ToString();
                                                DataRow dtRow = dt.NewRow();
                                                DataRow dtRowDLG = dt_DLG.NewRow();
                                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                                dtRow["Code"] = code;
                                                dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                                dtRow_congtactheogiadoan["SortId"] = SortId++;
                                                dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                                double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLCT);
                                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCT;
                                                dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                                dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
                                                dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
                                                dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
                                                dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                                dtRow["CodePhanTuyen"] = CodeTuyen;
                                                dtRow["ThanhTienDocVao"] = TTDocVao;
                                                if (!string.IsNullOrEmpty(CodeDDCT))
                                                {
                                                    dtRow["CodeDienDai"] = CodeDDCT;
                                                    CodeDDCT = string.Empty;
                                                }
                                                if (txtCTDGVatLieu.Text.HasValue())
                                                {
                                                    long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                                    dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                                }

                                                if (txtCTDGNhanCong.Text.HasValue())
                                                {
                                                    long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                                    dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                                }

                                                if (txtCTDGMay.Text.HasValue())
                                                {
                                                    long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                                    dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                                }
                                                if (txtCTKhoiLuongDocVao.Text.HasValue())
                                                {
                                                    double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                                    if (KLDV > 0)
                                                        dtRow["KhoiLuongDocVao"] = KLDV;
                                                    else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                                        dtRow["KhoiLuongDocVao"] = 0;
                                                }
                                                TDKH_GhiChuBoSungJson JsonGhiChuCT = new TDKH_GhiChuBoSungJson();
                                                JsonGhiChuCT.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                                if (txt_congtrinh_sttND.Text.HasValue())
                                                {
                                                    JsonGhiChuCT.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                                }
                                                JsonGhiChuCT.CodeDanhMucCongTac = code;
                                                var encryptedStrCT = JsonConvert.SerializeObject(JsonGhiChuCT);
                                                dtRow["GhiChuBoSungJson"] = encryptedStrCT;
                                                double DonGia = 0;
                                                if (DonGiaHD.Text.HasValue())
                                                {
                                                    DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                                }
                                                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                                                dtRow["Modified"] = true;
                                                dtRow["PhatSinh"] = false;
                                                dtRow_congtactheogiadoan["Modified"] = true;
                                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dt.Rows.Add(dtRow);
                                                dt_DLG.Rows.Add(dtRowDLG);
                                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                            }
                                        }
                                    }
                                }
                                else if (count > 2)
                                {
                                    i = j;
                                    string MaHieu = txtCTMaDuToan.Text.HasValue() ? crRow[txtCTMaDuToan.Text].Value.ToString() : string.Empty;
                                    string code = Guid.NewGuid().ToString();
                                    DataRow dtRow = dt.NewRow();
                                    DataRow dtRowDLG = dt_DLG.NewRow();
                                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                    dtRow["Code"] = code;
                                    dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                    dtRow_congtactheogiadoan["SortId"] = SortId++;
                                    dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLCT);
                                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCT;
                                    dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                    dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
                                    dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = string.IsNullOrEmpty(MaHieu) ? "TT" : MaHieu;
                                    dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
                                    dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                    dtRow["CodePhanTuyen"] = CodeTuyen;
                                    if (TT)
                                    {
                                        double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                        dtRow["ThanhTienDocVao"] = TTDocVao;
                                    }
                                    if (!string.IsNullOrEmpty(CodeDDCT))
                                    {
                                        dtRow["CodeDienDai"] = CodeDDCT;
                                        CodeDDCT = string.Empty;
                                    }
                                    if (txtCTDGVatLieu.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                    }

                                    if (txtCTDGNhanCong.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                    }

                                    if (txtCTDGMay.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                    }
                                    if (txtCTKhoiLuongDocVao.Text.HasValue())
                                    {
                                        double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                        if (KLDV > 0)
                                            dtRow["KhoiLuongDocVao"] = KLDV;
                                        else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                            dtRow["KhoiLuongDocVao"] = 0;
                                    }
                                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                    if (txt_congtrinh_sttND.Text.HasValue())
                                    {
                                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                    }
                                    JsonGhiChu.CodeDanhMucCongTac = code;
                                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                    dtRow["GhiChuBoSungJson"] = encryptedStr;
                                    double DonGia = 0;
                                    if (DonGiaHD.Text.HasValue())
                                    {
                                        DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                    }
                                    dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                           = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                                    dtRow["Modified"] = true;
                                    dtRow["PhatSinh"] = false;
                                    dtRow_congtactheogiadoan["Modified"] = true;
                                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dt.Rows.Add(dtRow);
                                    dt_DLG.Rows.Add(dtRowDLG);
                                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                }
                                else if (TT && !string.IsNullOrEmpty(tencongtac))
                                {
                                    bool TryTT = double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                    if (TTDocVao > 0)
                                    {
                                        i = j;
                                        string MaHieu = "TRỐNG";
                                        string code = Guid.NewGuid().ToString();
                                        DataRow dtRow = dt.NewRow();
                                        DataRow dtRowDLG = dt_DLG.NewRow();
                                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                        dtRow["Code"] = code;
                                        dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                                        dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                        double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLCT);
                                        dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCT;
                                        dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                        dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
                                        dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
                                        dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
                                        dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                        dtRow["CodePhanTuyen"] = CodeTuyen;
                                        dtRow["ThanhTienDocVao"] = TTDocVao;
                                        if (!string.IsNullOrEmpty(CodeDDCT))
                                        {
                                            dtRow["CodeDienDai"] = CodeDDCT;
                                            CodeDDCT = string.Empty;
                                        }
                                        if (txtCTDGVatLieu.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                        }

                                        if (txtCTDGNhanCong.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                        }

                                        if (txtCTDGMay.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                        }
                                        if (txtCTKhoiLuongDocVao.Text.HasValue())
                                        {
                                            double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                            if (KLDV > 0)
                                                dtRow["KhoiLuongDocVao"] = KLDV;
                                            else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                                dtRow["KhoiLuongDocVao"] = 0;
                                        }
                                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                        JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                        if (txt_congtrinh_sttND.Text.HasValue())
                                        {
                                            JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                        }
                                        JsonGhiChu.CodeDanhMucCongTac = code;
                                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                        dtRow["GhiChuBoSungJson"] = encryptedStr;
                                        double DonGia = 0;
                                        if (DonGiaHD.Text.HasValue())
                                        {
                                            DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                        }
                                        dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                               = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                                        dtRow["Modified"] = true;
                                        dtRow["PhatSinh"] = false;
                                        dtRow_congtactheogiadoan["Modified"] = true;
                                        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dt.Rows.Add(dtRow);
                                        dt_DLG.Rows.Add(dtRowDLG);
                                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                    }

                                }
                            }
                        }
                        else if (count == 2)
                        {
                            if (string.IsNullOrEmpty(CodeHM))
                            {
                                if (string.IsNullOrEmpty(CodeCtrinh) && Index == 0)
                                {
                                    CodeCtrinh = Guid.NewGuid().ToString();
                                    DataRow CrowCTrinh = dt_CTrinh.NewRow();
                                    CrowCTrinh["Code"] = CodeCtrinh;
                                    CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                                    CrowCTrinh["SortID"] = SortIDCT++;
                                    CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                                    dt_CTrinh.Rows.Add(CrowCTrinh);
                                }
                                CodeHM = Guid.NewGuid().ToString();
                                DataRow CrowHM = dt_HM.NewRow();
                                CrowHM["Code"] = CodeHM;
                                CrowHM["SortID"] = SortIDHM++;
                                CrowHM["CodeCongTrinh"] = CodeCtrinh;
                                CrowHM["Ten"] = $"Hạng mục {dt_HM.Rows.Count}";
                                dt_HM.Rows.Add(CrowHM);
                            }
                            //string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRow = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRow["Code"] = CodeNhomCT;
                            NewRow["CodeHangMuc"] = CodeHM;
                            NewRow["Ten"] = TenCongTac;
                            NewRow["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                            if (!string.IsNullOrEmpty(CodeDDNhom))
                            {
                                NewRow["CodeDienDai"] = CodeDDNhom;
                                CodeDDNhom = string.Empty;
                            }
                            if (Te_DonGiaNhom.Text.HasValue())
                            {
                                double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                if (DonGiaNhom > 0)
                                    NewRow["DonGia"] = DonGiaNhom;
                            }
                            double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                            if (KLNhom > 0)
                                NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;

                            NewRow["SortId"] = SortIdNhom++;
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRow["GhiChuBoSungJson"] = encryptedStr;
                            dt_NhomCT.Rows.Add(NewRow);
                            for (int j = i + 1; j <= lastLine; j++)
                            {
                                crRow = ws.Rows[j];
                                _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                int.TryParse(_STT, out STT);
                                count = _STT.Count(x => x == '.');
                                string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
                                if (STT > 0 || count <= 2 && count > 0 || _STT == Te_DDNhom.Text ||
                                    _STT.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH || _STT.ToUpper() == MyConstant.TYPEROW_HangMuc)
                                {
                                    i = j - 1;
                                    break;
                                }
                                else if (_STT == Te_DDCT.Text)
                                {
                                    CodeDDCT = Guid.NewGuid().ToString();
                                    DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                                    CrowDD["Code"] = CodeDDCT;
                                    CrowDD["CodeDuAn"] = m_CodeDuAn;
                                    CrowDD["Ten"] = crRow[txtCTTenCongTac.Text].Value.ToString();
                                    dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                                }
                                else if (count > 2)
                                {
                                    i = j;
                                    string MaHieu = txtCTMaDuToan.Text.HasValue() ? crRow[txtCTMaDuToan.Text].Value.ToString() : string.Empty;
                                    string code = Guid.NewGuid().ToString();
                                    DataRow dtRow = dt.NewRow();
                                    DataRow dtRowDLG = dt_DLG.NewRow();
                                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                    dtRow["Code"] = code;
                                    dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                    dtRow_congtactheogiadoan["SortId"] = SortId++;
                                    dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLCT);
                                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCT;
                                    dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                    dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
                                    dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = string.IsNullOrEmpty(MaHieu) ? "TT" : MaHieu;
                                    dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
                                    dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                    dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                    if (TT)
                                    {
                                        double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                        dtRow["ThanhTienDocVao"] = TTDocVao;
                                    }
                                    if (!string.IsNullOrEmpty(CodeDDCT))
                                    {
                                        dtRow["CodeDienDai"] = CodeDDCT;
                                        CodeDDCT = string.Empty;
                                    }
                                    if (txtCTDGVatLieu.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                    }

                                    if (txtCTDGNhanCong.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                    }

                                    if (txtCTDGMay.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                    }
                                    if (txtCTKhoiLuongDocVao.Text.HasValue())
                                    {
                                        double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                        if (KLDV > 0)
                                            dtRow["KhoiLuongDocVao"] = KLDV;
                                        else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                            dtRow["KhoiLuongDocVao"] = 0;
                                    }
                                    TDKH_GhiChuBoSungJson JsonGhiChuCT = new TDKH_GhiChuBoSungJson();
                                    JsonGhiChuCT.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                    if (txt_congtrinh_sttND.Text.HasValue())
                                    {
                                        JsonGhiChuCT.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                    }
                                    JsonGhiChuCT.CodeDanhMucCongTac = code;
                                    var encryptedStrCT = JsonConvert.SerializeObject(JsonGhiChuCT);
                                    dtRow["GhiChuBoSungJson"] = encryptedStrCT;
                                    double DonGia = 0;
                                    if (DonGiaHD.Text.HasValue())
                                    {
                                        DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                    }
                                    dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                           = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                                    dtRow["Modified"] = true;
                                    dtRow["PhatSinh"] = false;
                                    dtRow_congtactheogiadoan["Modified"] = true;
                                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dt.Rows.Add(dtRow);
                                    dt_DLG.Rows.Add(dtRowDLG);
                                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                }
                                else if (TT && !string.IsNullOrEmpty(tencongtac))
                                {
                                    bool TryTT = double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                    if (TTDocVao > 0)
                                    {
                                        i = j;
                                        string MaHieu = "TRỐNG";
                                        string code = Guid.NewGuid().ToString();
                                        DataRow dtRow = dt.NewRow();
                                        DataRow dtRowDLG = dt_DLG.NewRow();
                                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                        dtRow["Code"] = code;
                                        dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                                        dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                        double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLCT);
                                        dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCT;
                                        dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                        dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
                                        dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
                                        dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
                                        dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                        dtRow["ThanhTienDocVao"] = TTDocVao;
                                        if (!string.IsNullOrEmpty(CodeDDCT))
                                        {
                                            dtRow["CodeDienDai"] = CodeDDCT;
                                            CodeDDCT = string.Empty;
                                        }
                                        if (txtCTDGVatLieu.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                        }

                                        if (txtCTDGNhanCong.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                        }

                                        if (txtCTDGMay.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                        }
                                        if (txtCTKhoiLuongDocVao.Text.HasValue())
                                        {
                                            double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                            if (KLDV > 0)
                                                dtRow["KhoiLuongDocVao"] = KLDV;
                                            else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                                dtRow["KhoiLuongDocVao"] = 0;
                                        }
                                        TDKH_GhiChuBoSungJson JsonGhiChuCT = new TDKH_GhiChuBoSungJson();
                                        JsonGhiChuCT.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                        if (txt_congtrinh_sttND.Text.HasValue())
                                        {
                                            JsonGhiChuCT.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                        }
                                        JsonGhiChuCT.CodeDanhMucCongTac = code;
                                        var encryptedStrCT = JsonConvert.SerializeObject(JsonGhiChuCT);
                                        dtRow["GhiChuBoSungJson"] = encryptedStrCT;
                                        double DonGia = 0;
                                        if (DonGiaHD.Text.HasValue())
                                        {
                                            DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                        }
                                        dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                               = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                                        dtRow["Modified"] = true;
                                        dtRow["PhatSinh"] = false;
                                        dtRow_congtactheogiadoan["Modified"] = true;
                                        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dt.Rows.Add(dtRow);
                                        dt_DLG.Rows.Add(dtRowDLG);
                                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                    }

                                }
                            }
                        }
                        else if (count > 2)
                        {
                            //string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
                            string MaHieu = txtCTMaDuToan.Text.HasValue() ? crRow[txtCTMaDuToan.Text].Value.ToString() : string.Empty;
                            string code = Guid.NewGuid().ToString();
                            DataRow dtRow = dt.NewRow();
                            DataRow dtRowDLG = dt_DLG.NewRow();
                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                            dtRow["Code"] = code;
                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                            double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLCT);
                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCT;
                            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = string.IsNullOrEmpty(MaHieu) ? "TT" : MaHieu;
                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = TenCongTac;
                            if (TT)
                            {
                                double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                dtRow["ThanhTienDocVao"] = TTDocVao;
                            }
                            if (!string.IsNullOrEmpty(CodeDDCT))
                            {
                                dtRow["CodeDienDai"] = CodeDDCT;
                                CodeDDCT = string.Empty;
                            }
                            dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                            if (txtCTDGVatLieu.Text.HasValue())
                            {
                                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                            }

                            if (txtCTDGNhanCong.Text.HasValue())
                            {
                                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                            }

                            if (txtCTDGMay.Text.HasValue())
                            {
                                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                            }
                            if (txtCTKhoiLuongDocVao.Text.HasValue())
                            {
                                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                if (KLDV > 0)
                                    dtRow["KhoiLuongDocVao"] = KLDV;
                                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                    dtRow["KhoiLuongDocVao"] = 0;
                            }
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = code;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            dtRow["GhiChuBoSungJson"] = encryptedStr;
                            double DonGia = 0;
                            if (DonGiaHD.Text.HasValue())
                            {
                                DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                            }
                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                            dtRow["Modified"] = true;
                            dtRow["PhatSinh"] = false;
                            dtRow_congtactheogiadoan["Modified"] = true;
                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dt.Rows.Add(dtRow);
                            dt_DLG.Rows.Add(dtRowDLG);
                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                        }
                        else if (TT && !string.IsNullOrEmpty(TenCongTac))
                        {
                            bool TryTT = double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                            if (TTDocVao > 0)
                            {
                                string MaHieu = "TRỐNG";
                                string code = Guid.NewGuid().ToString();
                                DataRow dtRow = dt.NewRow();
                                DataRow dtRowDLG = dt_DLG.NewRow();
                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                dtRow["Code"] = code;
                                dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                dtRow_congtactheogiadoan["SortId"] = SortId++;
                                dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLCT);
                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCT;
                                dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
                                dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = string.IsNullOrEmpty(MaHieu) ? "TT" : MaHieu;
                                dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = TenCongTac;
                                dtRow["ThanhTienDocVao"] = TTDocVao;
                                if (!string.IsNullOrEmpty(CodeDDCT))
                                {
                                    dtRow["CodeDienDai"] = CodeDDCT;
                                    CodeDDCT = string.Empty;
                                }
                                dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                if (txtCTDGVatLieu.Text.HasValue())
                                {
                                    long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                }

                                if (txtCTDGNhanCong.Text.HasValue())
                                {
                                    long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                }

                                if (txtCTDGMay.Text.HasValue())
                                {
                                    long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                }
                                if (txtCTKhoiLuongDocVao.Text.HasValue())
                                {
                                    double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                    if (KLDV > 0)
                                        dtRow["KhoiLuongDocVao"] = KLDV;
                                    else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                        dtRow["KhoiLuongDocVao"] = 0;
                                }
                                TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                if (txt_congtrinh_sttND.Text.HasValue())
                                {
                                    JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                }
                                JsonGhiChu.CodeDanhMucCongTac = code;
                                var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                dtRow["GhiChuBoSungJson"] = encryptedStr;
                                double DonGia = 0;
                                if (DonGiaHD.Text.HasValue())
                                {
                                    DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                }
                                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                                dtRow["Modified"] = true;
                                dtRow["PhatSinh"] = false;
                                dtRow_congtactheogiadoan["Modified"] = true;
                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dt.Rows.Add(dtRow);
                                dt_DLG.Rows.Add(dtRowDLG);
                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                            }

                        }
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_CTrinh, MyConstant.TBL_THONGTINCONGTRINH);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_HM, MyConstant.TBL_THONGTINHANGMUC);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_TenDienDaiTuDo, TDKH.Tbl_TenDienDaiTuDo);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                dt_TenDienDaiTuDo.Clear();
            }

            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích lại khối lượng, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();

        }
        private void Fcn_TruyenDataExcelThuCong(Dictionary<Control, string> dic, int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);

            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = 0;

            bool CotMaHieu = txtCTMaDuToan.Text == "" ? false : true;
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string CodeCtrinh = "", CodeHM = "";
            //bool NewCongTrinh = (rg_LayDuLieu.SelectedIndex == 1 && lUE_ToChucCaNhan.EditValue != null) ? false : true;
            bool DonGiaDuThau = txt_dongiaduthau.Text == "" ? false : true;
            bool m_CheckSheet = cboCTtenSheet.Text == cbb_sheet.Text ? true : false;
            int SortIDCT = dt_CTrinh.Rows.Count;
            int SortIDHM = dt_HM.Rows.Count;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", TenCongTac = "";
            int Index = rg_LayDuLieu.SelectedIndex;
            if (Index > 0)
                CodeCtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            if (txtCTMaDuToan.Text == "")
            {
                for (int i = firstLine; i <= lastLine; i++)
                {
                    Row crRow = ws.Rows[i];
                    if (!crRow.Visible)
                        continue;
                    TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DonVi = txtCTDonVi.Text == "" ? default : crRow[txtCTDonVi.Text].Value.TextValue;
                    string _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                    int.TryParse(_STT, out int STT);
                    if (_STT.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                    {
                        if (CodeCtrinh == "" && Index == 0)
                        {
                            CodeCtrinh = Guid.NewGuid().ToString();
                            DataRow CrowCTrinh = dt_CTrinh.NewRow();
                            CrowCTrinh["Code"] = CodeCtrinh;
                            CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                            CrowCTrinh["SortID"] = SortIDCT++;
                            CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                            dt_CTrinh.Rows.Add(CrowCTrinh);
                        }
                        CodeHM = Guid.NewGuid().ToString();
                        DataRow CrowHM = dt_HM.NewRow();
                        CrowHM["Code"] = CodeHM;
                        CrowHM["SortID"] = SortIDHM++;
                        CrowHM["CodeCongTrinh"] = CodeCtrinh;
                        CrowHM["Ten"] = TenCongTac;
                        dt_HM.Rows.Add(CrowHM);
                    }
                    else if (_STT.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                    {
                        if (Index > 0)
                            continue;
                        CodeCtrinh = Guid.NewGuid().ToString();
                        codecongtrinh = CodeCtrinh;
                        DataRow CrowCTrinh = dt_CTrinh.NewRow();
                        CrowCTrinh["Code"] = CodeCtrinh;
                        CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                        CrowCTrinh["SortID"] = SortIDCT++;
                        CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                        dt_CTrinh.Rows.Add(CrowCTrinh);
                    }
                    if (Index == 0)
                    {
                        if (CodeHM == "")
                        {
                            CodeCtrinh = Guid.NewGuid().ToString();
                            DataRow CrowCTrinh = dt_CTrinh.NewRow();
                            CrowCTrinh["Code"] = CodeCtrinh;
                            CrowCTrinh["SortID"] = SortIDCT++;
                            CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                            CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                            dt_CTrinh.Rows.Add(CrowCTrinh);

                            CodeHM = Guid.NewGuid().ToString();
                            DataRow CrowHM = dt_HM.NewRow();
                            CrowHM["Code"] = CodeHM;
                            CrowHM["SortID"] = SortIDHM++;
                            CrowHM["CodeCongTrinh"] = CodeCtrinh;
                            CrowHM["Ten"] = $"Hạng mục {dt_HM.Rows.Count + 1}"; ;
                            dt_HM.Rows.Add(CrowHM);
                        }
                        if (_STT == "*" || _STT == "*T")
                        {
                            DataRow NewRow = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRow["Code"] = CodeNhomCT;
                            NewRow["CodeHangMuc"] = CodeHM;
                            NewRow["Ten"] = TenCongTac;
                            dt_NhomCT.Rows.Add(NewRow);
                            for (int j = i + 1; j <= lastLine; j++)
                            {
                                Row CrowCTNhom = ws.Rows[j];
                                string Mahieu = CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString();
                                if (Mahieu == "T*" || Mahieu == "*" || Mahieu == "THM")
                                {
                                    i = j - 1;
                                    break;

                                }
                                else
                                {
                                    string CodeCongTacGD = Guid.NewGuid().ToString();
                                    Fcn_UpdateCongTacThuCong(SortId, CodeCongTacGD, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeHM, DonGiaDuThau, m_CheckSheet, CodeNhomCT);
                                    int End = fcn_updatenhomdiendaiThuCongKhongCoMaCT(j + 1, lastLine, CodeCongTacGD, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (End != 0)
                                    {
                                        i = End;
                                        i = End - 1;
                                    }
                                    SortId++;
                                }
                            }
                        }
                        if (STT != 0)
                        {
                            string CodeCongTacGD = Guid.NewGuid().ToString();
                            Fcn_UpdateCongTacThuCong(SortId, CodeCongTacGD, dt, dt_DLG, dt_congtactheogiaidoan, crRow, i, CodeHM, DonGiaDuThau, m_CheckSheet);
                            int End = fcn_updatenhomdiendaiThuCongKhongCoMaCT(i + 1, lastLine, CodeCongTacGD, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (End != 0)
                            {
                                i = End;
                                i = End - 1;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = firstLine; i <= lastLine; i++)
                {
                    Row crRow = ws.Rows[i];
                    if (!crRow.Visible)
                        continue;
                    dt_NhomDienDai.Clear();
                    dt_Chitietcongtaccon.Clear();
                    dt_congtactheogiaidoan.Clear();
                    dt.Clear();
                    dt_DLG.Clear();
                    dt_NhomCT.Clear();
                    dtHaoPhi.Clear();
                    string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                    TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DonVi = crRow[txtCTDonVi.Text].Value.TextValue;
                    string STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                    WaitFormHelper.ShowWaitForm($"{i + 1}.{MaHieu}_{TenCongTac}");
                    if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                    {
                        if (CodeCtrinh == "" && Index == 0)
                        {
                            CodeCtrinh = Guid.NewGuid().ToString();
                            DataRow CrowCTrinh = dt_CTrinh.NewRow();
                            CrowCTrinh["Code"] = CodeCtrinh;
                            CrowCTrinh["SortID"] = SortIDCT++;
                            CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                            CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                            dt_CTrinh.Rows.Add(CrowCTrinh);
                        }
                        CodeHM = Guid.NewGuid().ToString();
                        DataRow CrowHM = dt_HM.NewRow();
                        CrowHM["Code"] = CodeHM;
                        CrowHM["SortID"] = SortIDHM++;
                        CrowHM["CodeCongTrinh"] = CodeCtrinh;
                        CrowHM["Ten"] = TenCongTac;
                        dt_HM.Rows.Add(CrowHM);
                    }
                    else if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                    {
                        if (Index > 0)
                            continue;
                        CodeCtrinh = Guid.NewGuid().ToString();
                        DataRow CrowCTrinh = dt_CTrinh.NewRow();
                        CrowCTrinh["Code"] = CodeCtrinh;
                        CrowCTrinh["SortID"] = SortIDCT++;
                        CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                        CrowCTrinh["Ten"] = TenCongTac;
                        dt_CTrinh.Rows.Add(CrowCTrinh);
                    }
                    else if (CodeHM == "")
                    {
                        if (Index == 0)
                        {
                            CodeCtrinh = Guid.NewGuid().ToString();
                            DataRow CrowCTrinh = dt_CTrinh.NewRow();
                            CrowCTrinh["Code"] = CodeCtrinh;
                            CrowCTrinh["SortID"] = SortIDCT++;
                            CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                            CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                            dt_CTrinh.Rows.Add(CrowCTrinh);
                        }

                        CodeHM = Guid.NewGuid().ToString();
                        DataRow CrowHM = dt_HM.NewRow();
                        CrowHM["Code"] = CodeHM;
                        CrowHM["SortID"] = SortIDHM++;
                        CrowHM["CodeCongTrinh"] = CodeCtrinh;
                        CrowHM["Ten"] = $"Hạng mục {dt_HM.Rows.Count}"; ;
                        dt_HM.Rows.Add(CrowHM);
                        if (MaHieu != "" && STT != "")
                        {
                            string CodeCongTacGD = Guid.NewGuid().ToString();
                            Fcn_UpdateCongTacThuCong(SortId, CodeCongTacGD, dt, dt_DLG, dt_congtactheogiaidoan, crRow, i, CodeHM, DonGiaDuThau, m_CheckSheet);
                            if (MaHieu.ToUpper() == "TT")
                            {

                                if (txt_NC_Dongia.Text != "")
                                {
                                    double DonGia = crRow[txt_NC_Dongia.Text].Value.IsNumeric ? crRow[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCongTacGD, TenCongTac, MaHieu, DonVi, DonGia, "Nhân công");
                                }
                                if (txt_VL_Dongia.Text != "")
                                {
                                    double DonGia = crRow[txt_VL_Dongia.Text].Value.IsNumeric ? crRow[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCongTacGD, TenCongTac, MaHieu, DonVi, DonGia, "Vật liệu");
                                }
                                if (txt_MTC_Dongia.Text != "")
                                {
                                    double DonGia = crRow[txt_MTC_Dongia.Text].Value.IsNumeric ? crRow[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCongTacGD, TenCongTac, MaHieu, DonVi, DonGia, "Máy thi công");
                                }

                            }
                            SortId++;
                            int End = fcn_updatenhomdiendaiThuCong(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (End != 0)
                            {
                                i = End - 1;
                            }
                        }
                        else if (MaHieu == te_Nhom.Text)
                        {
                            string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRow = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            double KLNhom = txtCTKhoiLuong.Text.HasValue() ? crRow[txtCTKhoiLuong.Text].Value.NumericValue : 0;
                            double DonGiaNhom = DonGiaHD.Text.HasValue() ? crRow[DonGiaHD.Text].Value.NumericValue : 0;
                            NewRow["Code"] = CodeNhomCT;
                            NewRow["CodeHangMuc"] = CodeHM;
                            NewRow["Ten"] = tennhom;
                            NewRow["DonGia"] = DonGiaNhom;
                            if (KLNhom > 0)
                                NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;
                            NewRow["SortId"] = SortIdNhom++;
                            dt_NhomCT.Rows.Add(NewRow);
                            for (int j = i + 1; j <= lastLine; j++)
                            {
                                Row CrowCTNhom = ws.Rows[j];
                                string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                                WaitFormHelper.ShowWaitForm($"{j + 1}.{Mahieu}_{TenCongTac}");
                                if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                    Mahieu == "THM" || Mahieu == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                                {
                                    i = j - 1;
                                    break;

                                }
                                else
                                {
                                    DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    Fcn_UpdateCongTacThuCong(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeHM, DonGiaDuThau, m_CheckSheet, CodeNhomCT);
                                    if (Mahieu == "TT")
                                    {
                                        if (txt_NC_Dongia.Text != "")
                                        {
                                            double DonGia = CrowCTNhom[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                        }
                                        if (txt_VL_Dongia.Text != "")
                                        {
                                            double DonGia = CrowCTNhom[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                        }
                                        if (txt_MTC_Dongia.Text != "")
                                        {
                                            double DonGia = CrowCTNhom[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                        }

                                    }
                                    SortId++;
                                    int End = fcn_updatenhomdiendaiThuCong(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (End != 0)
                                    {
                                        i = End;
                                        j = End - 1;
                                    }
                                }
                            }
                        }
                        else if (MaHieu == te_PhanTuyen.Text)
                        {
                            string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRow = dt_Tuyen.NewRow();
                            string CodeTuyen = Guid.NewGuid().ToString();
                            NewRow["Code"] = CodeTuyen;
                            NewRow["CodeHangMuc"] = CodeHM;
                            NewRow["Ten"] = TenTuyen;
                            NewRow["SortId"] = SortIdTuyen++;
                            dt_Tuyen.Rows.Add(NewRow);

                            for (int j = i + 1; j <= lastLine; j++)
                            {
                                Row CrowCTNhom = ws.Rows[j];
                                string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                                if (Mahieu == te_PhanTuyen.Text || Mahieu == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu == MyConstant.CONST_TYPE_HANGMUC)
                                {
                                    i = j - 1;
                                    break;

                                }
                                else if (Mahieu == te_Nhom.Text)
                                {
                                    string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                                    DataRow NewRowNhom = dt_NhomCT.NewRow();
                                    string CodeNhomCT = Guid.NewGuid().ToString();
                                    double KLNhom = txtCTKhoiLuong.Text.HasValue() ? CrowCTNhom[txtCTKhoiLuong.Text].Value.NumericValue : 0;
                                    double DonGiaNhom = DonGiaHD.Text.HasValue() ? CrowCTNhom[DonGiaHD.Text].Value.NumericValue : 0;
                                    NewRowNhom["Code"] = CodeNhomCT;
                                    NewRowNhom["CodeHangMuc"] = CodeHM;
                                    NewRowNhom["Ten"] = tennhom;
                                    NewRowNhom["DonGia"] = DonGiaNhom;
                                    if (KLNhom > 0)
                                        NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                                    NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                                    NewRowNhom["SortId"] = SortIdNhom++;
                                    dt_NhomCT.Rows.Add(NewRowNhom);
                                    for (int k = j + 1; k <= lastLine; k++)
                                    {
                                        Row CrowCTNhomNew = ws.Rows[k];
                                        Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                        if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                            Mahieu == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu == MyConstant.CONST_TYPE_HANGMUC)
                                        {
                                            j = k - 1;
                                            i = j - 1;
                                            break;

                                        }
                                        else
                                        {
                                            DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                            codetheogiaidoan = Guid.NewGuid().ToString();
                                            Fcn_UpdateCongTacThuCong(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, k, CodeHM, DonGiaDuThau, m_CheckSheet, CodeNhomCT);
                                            if (Mahieu == "TT")
                                            {
                                                if (txt_NC_Dongia.Text != "")
                                                {
                                                    double DonGia = CrowCTNhomNew[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                                }
                                                if (txt_VL_Dongia.Text != "")
                                                {
                                                    double DonGia = CrowCTNhomNew[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                                }
                                                if (txt_MTC_Dongia.Text != "")
                                                {
                                                    double DonGia = CrowCTNhomNew[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                                    Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                                }

                                            }
                                            SortId++;
                                            int End = fcn_updatenhomdiendaiThuCong(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                            if (End != 0)
                                            {
                                                j = End;
                                                k = End - 1;
                                                i = j - 1;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    Fcn_UpdateCongTacThuCong(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeHM, DonGiaDuThau, m_CheckSheet, CodeTuyen: CodeTuyen);
                                    if (Mahieu == "TT")
                                    {
                                        if (txt_NC_Dongia.Text != "")
                                        {
                                            double DonGia = CrowCTNhom[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                        }
                                        if (txt_VL_Dongia.Text != "")
                                        {
                                            double DonGia = CrowCTNhom[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                        }
                                        if (txt_MTC_Dongia.Text != "")
                                        {
                                            double DonGia = CrowCTNhom[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                            Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                        }

                                    }
                                    SortId++;
                                    int End = fcn_updatenhomdiendaiThuCong(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (End != 0)
                                    {
                                        i = End;
                                        j = End - 1;
                                    }
                                }
                            }


                        }
                    }
                    else if (MaHieu == te_Nhom.Text)
                    {
                        string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                        DataRow NewRow = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        double KLNhom = txtCTKhoiLuong.Text.HasValue() ? crRow[txtCTKhoiLuong.Text].Value.NumericValue : 0;
                        double DonGiaNhom = DonGiaHD.Text.HasValue() ? crRow[DonGiaHD.Text].Value.NumericValue : 0;
                        NewRow["Code"] = CodeNhomCT;
                        NewRow["CodeHangMuc"] = CodeHM;
                        NewRow["DonGia"] = DonGiaNhom;
                        if (KLNhom > 0)
                            NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;
                        NewRow["Ten"] = tennhom;
                        NewRow["SortId"] = SortIdNhom++;
                        dt_NhomCT.Rows.Add(NewRow);
                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            Row CrowCTNhom = ws.Rows[j];
                            string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                            WaitFormHelper.ShowWaitForm($"{j + 1}.{Mahieu}_{TenCongTac}");
                            if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                Mahieu == "THM" || Mahieu == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                            {
                                i = j - 1;
                                break;

                            }
                            else
                            {
                                DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                Fcn_UpdateCongTacThuCong(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeHM, DonGiaDuThau, m_CheckSheet, CodeNhomCT);
                                if (Mahieu == "TT")
                                {
                                    if (txt_NC_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                    }
                                    if (txt_VL_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                    }
                                    if (txt_MTC_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                    }

                                }
                                SortId++;
                                int End = fcn_updatenhomdiendaiThuCong(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                if (End != 0)
                                {
                                    i = End;
                                    j = End - 1;
                                }
                            }
                        }
                    }
                    else if (MaHieu == te_PhanTuyen.Text)
                    {
                        string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                        DataRow NewRow = dt_Tuyen.NewRow();
                        string CodeTuyen = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeTuyen;
                        NewRow["CodeHangMuc"] = CodeHM;
                        NewRow["Ten"] = TenTuyen;
                        NewRow["SortId"] = SortIdTuyen++;
                        dt_Tuyen.Rows.Add(NewRow);

                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            Row CrowCTNhom = ws.Rows[j];
                            string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                            WaitFormHelper.ShowWaitForm($"{j + 1}.{Mahieu}_{TenCongTac}");
                            if (Mahieu == te_PhanTuyen.Text || Mahieu == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu == MyConstant.CONST_TYPE_HANGMUC)
                            {
                                i = j - 1;
                                break;

                            }
                            else if (Mahieu == te_Nhom.Text)
                            {
                                string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                                double KLNhom = txtCTKhoiLuong.Text.HasValue() ? CrowCTNhom[txtCTKhoiLuong.Text].Value.NumericValue : 0;
                                double DonGiaNhom = DonGiaHD.Text.HasValue() ? CrowCTNhom[DonGiaHD.Text].Value.NumericValue : 0;
                                DataRow NewRowNhom = dt_NhomCT.NewRow();
                                string CodeNhomCT = Guid.NewGuid().ToString();
                                NewRowNhom["Code"] = CodeNhomCT;
                                NewRowNhom["CodeHangMuc"] = CodeHM;
                                NewRowNhom["Ten"] = tennhom;
                                NewRowNhom["DonGia"] = DonGiaNhom;
                                if (KLNhom > 0)
                                    NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                                NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                                NewRowNhom["SortId"] = SortIdNhom++;
                                dt_NhomCT.Rows.Add(NewRowNhom);
                                for (int k = j + 1; k <= lastLine; k++)
                                {
                                    Row CrowCTNhomNew = ws.Rows[k];
                                    Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                    WaitFormHelper.ShowWaitForm($"{k + 1}.{Mahieu}_{TenCongTac}");
                                    if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                        Mahieu == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu == MyConstant.CONST_TYPE_HANGMUC)
                                    {
                                        j = k - 1;
                                        i = j - 1;
                                        break;

                                    }
                                    else
                                    {
                                        DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                        codetheogiaidoan = Guid.NewGuid().ToString();
                                        Fcn_UpdateCongTacThuCong(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, k, CodeHM, DonGiaDuThau, m_CheckSheet, CodeNhomCT);
                                        if (Mahieu == "TT")
                                        {
                                            if (txt_NC_Dongia.Text != "")
                                            {
                                                double DonGia = CrowCTNhomNew[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                            }
                                            if (txt_VL_Dongia.Text != "")
                                            {
                                                double DonGia = CrowCTNhomNew[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                            }
                                            if (txt_MTC_Dongia.Text != "")
                                            {
                                                double DonGia = CrowCTNhomNew[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhomNew[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                            }

                                        }
                                        SortId++;
                                        int End = fcn_updatenhomdiendaiThuCong(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                        if (End != 0)
                                        {
                                            j = End;
                                            k = End - 1;
                                            i = j - 1;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                Fcn_UpdateCongTacThuCong(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeHM, DonGiaDuThau, m_CheckSheet, CodeTuyen: CodeTuyen);
                                if (Mahieu == "TT")
                                {
                                    if (txt_NC_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_NC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Nhân công");
                                    }
                                    if (txt_VL_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_VL_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Vật liệu");
                                    }
                                    if (txt_MTC_Dongia.Text != "")
                                    {
                                        double DonGia = CrowCTNhom[txt_MTC_Dongia.Text].Value.IsNumeric ? CrowCTNhom[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                        Fcn_UpdateHaoPhiThuCong(dtHaoPhi, codetheogiaidoan, TenCongTac, Mahieu, DonVi, DonGia, "Máy thi công");
                                    }

                                }
                                SortId++;
                                int End = fcn_updatenhomdiendaiThuCong(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                if (End != 0)
                                {
                                    i = End;
                                    j = End - 1;
                                }
                            }
                        }


                    }
                    else if (MaHieu != "" && STT != "")
                    {
                        string CodeCTTheoGD = Guid.NewGuid().ToString();
                        Fcn_UpdateCongTacThuCong(SortId, CodeCTTheoGD, dt, dt_DLG, dt_congtactheogiaidoan, crRow, i, CodeHM, DonGiaDuThau, m_CheckSheet);
                        if (MaHieu == "TT")
                        {
                            if (txt_NC_Dongia.Text != "")
                            {
                                double DonGia = crRow[txt_NC_Dongia.Text].Value.IsNumeric ? crRow[txt_NC_Dongia.Text].Value.NumericValue : 0;
                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCTTheoGD, TenCongTac, MaHieu, DonVi, DonGia, "Nhân công");
                            }
                            if (txt_VL_Dongia.Text != "")
                            {
                                double DonGia = crRow[txt_VL_Dongia.Text].Value.IsNumeric ? crRow[txt_VL_Dongia.Text].Value.NumericValue : 0;
                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCTTheoGD, TenCongTac, MaHieu, DonVi, DonGia, "Vật liệu");
                            }
                            if (txt_MTC_Dongia.Text != "")
                            {
                                double DonGia = crRow[txt_MTC_Dongia.Text].Value.IsNumeric ? crRow[txt_MTC_Dongia.Text].Value.NumericValue : 0;
                                Fcn_UpdateHaoPhiThuCong(dtHaoPhi, CodeCTTheoGD, TenCongTac, MaHieu, DonVi, DonGia, "Máy thi công");
                            }

                        }
                        SortId++;
                        int End = fcn_updatenhomdiendaiThuCong(i + 1, lastLine, CodeCTTheoGD, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;
                    }
                    else
                    {
                        continue;
                    }
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_CTrinh, MyConstant.TBL_THONGTINCONGTRINH);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_HM, MyConstant.TBL_THONGTINHANGMUC);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
                }
            }
            WaitFormHelper.CloseWaitForm();
            if (txtCTMaDuToan.Text != "")
            {
                WaitFormHelper.ShowWaitForm("Đang phân tích hao phí vật tư, Vui lòng chờ!");
                queryStr = $"SELECT COALESCE(cttk.CodeHangMuc, dmct.CodeHangMuc) AS CodeHangMuc,COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi," +
                    $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac,COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
                    $"cttk.* " +
                    $" FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                       $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
                       $"ON dmct.Code=cttk.CodeCongTac " +
                       $"WHERE cttk.CodeGiaiDoan='{m_codegiadoan}' AND cttk.CodeNhaThau='{m_codenhathau}'";
                dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);

                foreach (DataRow row in dt.Rows)
                {
                    if (row["MaHieuCongTac"].ToString() != "" && row["MaHieuCongTac"].ToString() != "TT")
                    {
                        WaitFormHelper.ShowWaitForm($"{row["MaHieuCongTac"]}_{row["TenCongTac"]}");
                        MyFunction.fcn_TDKH_ThemDinhMucMacDinhChoCongTac(TypeKLHN.CongTac, row["Code"].ToString(), false);
                    }
                }
                string[] lstCodeHM = dt_HM.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                TDKHHelper.CapNhatAllVatTuHaoPhi(CodesHangMuc: lstCodeHM);
            }
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();

        }
        private void fcn_truyendataExxcel_congtrinh(Dictionary<Control, string> dic, int firstLine, int lastLine, TextEdit stt)
        {
            foreach (var item in dic)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            CellRange usedRange = workbook.Worksheets[cboHPVTtenSheet.Text].GetUsedRange();
            int lastline_vl = usedRange.RowCount;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            fcn_kiemtracongtrinh_hangmuc(firstLine);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            int Index = rg_LayDuLieu.SelectedIndex;
            if (tencongtrinh == "" && Index == 0)
            {
                firstLine = dic_congtrinh.First().Key + 2;
                fcn_kiemtracongtrinh_hangmuc(firstLine);
            }
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";
            double VL = 0, NC = 0, MTC = 0, STT = 0;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            if (Index > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                mahieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{mahieu}_{TenCongTac}");
                if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (Index > 0)
                        continue;
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    NewRow["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                    if (Te_DonGiaNhom.Text.HasValue())
                    {
                        double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                        if (DonGiaNhom > 0)
                            NewRow["DonGia"] = DonGiaNhom;
                    }
                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                    if (KLNhom > 0)
                        NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;

                    NewRow["SortId"] = SortIdNhom++;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                    if (txt_congtrinh_sttND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRow["GhiChuBoSungJson"] = encryptedStr;
                    dt_NhomCT.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                            Mahieu.ToUpper() == "THM" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                        {
                            i = j - 1;
                            break;

                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }

                }
                else if (mahieu == te_PhanTuyen.Text)
                {
                    string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);

                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_PhanTuyen.Text || Mahieu.ToUpper() == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        else if (Mahieu == te_Nhom.Text)
                        {
                            string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRowNhom = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRowNhom["Code"] = CodeNhomCT;
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
                            NewRowNhom["DonVi"] = CrowCTNhom[txtCTDonVi.Text].Value.TextValue;
                            if (Te_DonGiaNhom.Text.HasValue())
                            {
                                double.TryParse(CrowCTNhom[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                if (DonGiaNhom > 0)
                                    NewRowNhom["DonGia"] = DonGiaNhom;
                            }
                            double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                            if (KLNhom > 0)
                                NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                            NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                            NewRowNhom["SortId"] = SortIdNhom++;
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = CrowCTNhom[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                            dt_NhomCT.Rows.Add(NewRowNhom);
                            for (int k = j + 1; k <= lastLine; k++)
                            {
                                Row CrowCTNhomNew = ws.Rows[k];
                                Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                    Mahieu.ToUpper() == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                                {
                                    j = k - 1;
                                    i = j - 1;
                                    break;

                                }
                                else
                                {
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    VL = CrowCTNhomNew[txtCTDGVatLieu.Text].Value.NumericValue;
                                    NC = CrowCTNhomNew[txtCTDGNhanCong.Text].Value.NumericValue;
                                    MTC = CrowCTNhomNew[txtCTDGMay.Text].Value.NumericValue;
                                    RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    int End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (double.TryParse(CrowCTNhomNew[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                        RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.ToString(), k, true);
                                    else
                                        RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.ToString(), k, true);
                                    if (End != 0)
                                    {
                                        j = End;
                                        k = End - 1;
                                        i = j - 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }


                }
                else if (!int.TryParse(crRow[stt.Text].Value.ToString(), out int so))
                {
                    if (mahieu.ToUpper() == "THM")
                    {
                        foreach (var item in dic_congtrinh)
                        {
                            if (i < item.Key)
                            {
                                i = item.Key + 2;
                                break;
                            }
                        }
                        fcn_kiemtracongtrinh_hangmuc(i);
                        continue;
                    }
                    else if (mahieu != "")
                    {
                        string _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                        int count = _STT.Count(x => x == '.');
                        if (count == 0)
                            continue;
                        DonVi = crRow[txtCTDonVi.Text].Value.ToString();
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        VL = crRow[txtCTDGVatLieu.Text].Value.NumericValue;
                        NC = crRow[txtCTDGNhanCong.Text].Value.NumericValue;
                        MTC = crRow[txtCTDGMay.Text].Value.NumericValue;
                        //double.TryParse(_STT, out STT);
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, _STT, i);
                        else
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, _STT, i);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                else
                {
                    if (mahieu != "")
                    {
                        DonVi = crRow[txtCTDonVi.Text].Value.ToString();
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        VL = crRow[txtCTDGVatLieu.Text].Value.NumericValue;
                        NC = crRow[txtCTDGNhanCong.Text].Value.NumericValue;
                        MTC = crRow[txtCTDGMay.Text].Value.NumericValue;
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), i);
                        else
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), i);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }
            spsheet_XemFile.EndUpdate();
            try
            {

            }
            catch (Exception) { }

            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();

            this.Close();
        }
        private void Fcn_ReadKienGiangCaMau(int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_TenDienDaiTuDo}";
            DataTable dt_TenDienDaiTuDo = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            string DonVi = string.Empty, codetheogiaidoan = string.Empty, _STT = string.Empty, TenCongTac = string.Empty, MaHieu = string.Empty;
            string VL = string.Empty, NC = string.Empty, MTC = string.Empty;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            string CodeCTKienGiang = Guid.NewGuid().ToString();
            string CodeCTCaMau = Guid.NewGuid().ToString();
            string[] ArrCodeCT = new string[]
            {
                CodeCTKienGiang,
                CodeCTCaMau
            };
            string KG = "Kiên Giang";
            string CM = "Cà Mau";
            string[] ArrTenCT = new string[]
            {
                KG,
                CM
            };
            
            Dictionary<int, string> Dic_CodeHM = new Dictionary<int, string>();
            Dictionary<string, string> Dic_ColKLNhom = new Dictionary<string, string>()
            {
                {KG,"K" },
                {CM,"L" }
            };          
            Dictionary<string, string> Dic_ColDonGiaNhom = new Dictionary<string, string>()
            {
                {KG,"M" },
                {CM,"N" }
            };          
            //Dictionary<string, string> Dic_ColDonGiaNhom = new Dictionary<string, string>()
            //{
            //    {KG,"Q" },
            //    {CM,"R" }
            //};         
            Dictionary<string, string> Dic_ColDonGiaCTac = new Dictionary<string, string>()
            {
                {KG,"J" },
                {CM,"K" }
            };       
            Dictionary<string, string> Dic_ColKLCTac = new Dictionary<string, string>()
            {
                {KG,"H" },
                {CM,"I" }
            };      
            Dictionary<string, string> Dic_ColDonGiaVT = new Dictionary<string, string>()
            {
                {KG,"P" },
                {CM,"R" }
            };    
            Dictionary<string, string> Dic_ColDonGiaVTNoLink = new Dictionary<string, string>()
            {
                {KG,"L" },
                {CM,"O" }
            };
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{CodeCTKienGiang}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { "Công trình Kiên Giang" });    
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{CodeCTCaMau}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { "Công trình Cà Mau" });
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            Worksheet ws = workbook.Worksheets[cbe_TongHop.Text];
            Worksheet wsCtac = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsVL = workbook.Worksheets[cboHPVTtenSheet.Text];
            int end = wsVL.GetUsedRange().BottomRowIndex;
            string CodeTuyen = string.Empty, CodeDDCT = string.Empty,
                CodeDDNhom = string.Empty,
                CodeDDTuyen = string.Empty; ;
            for (int i = firstLine; i <= lastLine; i++)
            {
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                dt_TenDienDaiTuDo.Clear();
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                CodeTuyen = string.Empty;
                _STT = crRow[te_TongHopSTT.Text].Value.ToString();
                TenCongTac = crRow[te_TongHopTenCT.Text].Value.ToString();
                MaHieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{MaHieu}_{TenCongTac}");
                if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    int IndexHM = 1;
                    Dic_CodeHM.Clear();
                    foreach(var itemHM in ArrCodeCT)
                    {
                        string CodeHM = Guid.NewGuid().ToString();
                        Dic_CodeHM.Add(IndexHM++, CodeHM);
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{CodeHM}','{itemHM}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { TenCongTac });
                    }
                    CodeDDCT = string.Empty;
                    CodeDDNhom = string.Empty;
                    CodeDDTuyen = string.Empty;
                }
                else if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                    continue;
                else if (MaHieu == Te_DDNhom.Text)
                {
                    CodeDDNhom = Guid.NewGuid().ToString();
                    DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                    CrowDD["Code"] = CodeDDNhom;
                    CrowDD["CodeDuAn"] = m_CodeDuAn;
                    CrowDD["Ten"] = TenCongTac;
                    dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                }
                else if (MaHieu == Te_DDTuyen.Text)
                {
                    CodeDDTuyen = Guid.NewGuid().ToString();
                    DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                    CrowDD["Code"] = CodeDDTuyen;
                    CrowDD["CodeDuAn"] = m_CodeDuAn;
                    CrowDD["Ten"] = TenCongTac;
                    dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                }
                else if (MaHieu == MyConstant.CONST_TYPE_PhanTuyen)
                {
                    if (!Dic_CodeHM.Any())
                    {
                        int IndexHM = 1;
                        foreach (var itemHM in ArrCodeCT)
                        {
                            string CodeHM = Guid.NewGuid().ToString();
                            Dic_CodeHM.Add(IndexHM++, CodeHM);
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{CodeHM}','{itemHM}',@Ten)";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { $"Hạng mục {dt_HM.Rows.Count + 1}" });
                        }
                    }
                    int Index = 1;
                    foreach (var itemTuyen in ArrTenCT)
                    {
                        string CodeHM = Dic_CodeHM[Index++];
                        DataRow NewRow = dt_Tuyen.NewRow();
                        CodeTuyen = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeTuyen;
                        NewRow["CodeHangMuc"] = CodeHM;
                        NewRow["Ten"] = TenCongTac;
                        NewRow["SortId"] = SortIdTuyen++;
                        if (!string.IsNullOrEmpty(CodeDDTuyen))
                        {
                            NewRow["CodeDienDai"] = CodeDDTuyen;
                            if (Index > 2)
                                CodeDDTuyen = string.Empty;
                        }
                        dt_Tuyen.Rows.Add(NewRow);

                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            crRow = ws.Rows[j];
                            MaHieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                            _STT = crRow[te_TongHopSTT.Text].Value.ToString();
                            if (_STT == te_PhanTuyen.Text || _STT.ToUpper() == "THM" || _STT == te_EndTuyen.Text ||
                                _STT == "" || _STT.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                            {
                                if (Index > 2)
                                    i = j - 1;
                                break;

                            }
                            else if (string.IsNullOrEmpty(MaHieu) && !string.IsNullOrEmpty(_STT))
                            {
                                //int IndexNhom = 1;
                                Regex Select = new Regex($@"\D{cboCTtenSheet.Text}\D+(?<ViTri>\d+)");
                                Regex SelectVT = new Regex($@"\D{cboHPVTtenSheet.Text}\D+(?<ViTri>\d+)");
                                string Fomula = crRow[te_TongHopSTT.Text].Formula;
                                MatchCollection Multimatch = Select.Matches(Fomula);
                                int RowIndexCTac = int.Parse(Multimatch[0].Groups["ViTri"].ToString());
                                string STTNhom = _STT;
                                DataRow NewRowNhom = dt_NhomCT.NewRow();
                                string CodeNhomCT = Guid.NewGuid().ToString();
                                double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTong);
                                double.TryParse(crRow[Dic_ColDonGiaNhom[itemTuyen]].Value.ToString(), out double DonGiaTinh);
                                double.TryParse(crRow[Dic_ColKLNhom[itemTuyen]].Value.ToString(), out double KLTinh);
                                //string CodeHM = Dic_CodeHM[Index++];
                                NewRowNhom["Code"] = CodeNhomCT;
                                NewRowNhom["CodeHangMuc"] = CodeHM;
                                NewRowNhom["Ten"] = TenCongTac;
                                if (!string.IsNullOrEmpty(CodeDDNhom))
                                {
                                    NewRowNhom["CodeDienDai"] = CodeDDNhom;
                                    if (Index > 2)
                                        CodeDDNhom = string.Empty;
                                }
                                if (DonGiaTinh > 0)
                                    NewRowNhom["DonGia"] = DonGiaTinh;
                                NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                                if (KLTinh > 0)
                                    NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTinh;
                                NewRowNhom["SortId"] = SortIdNhom++;
                                if (!string.IsNullOrEmpty(CodeTuyen))
                                    NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                                TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                JsonGhiChu.STT = STTNhom;
                                if (te_TongHopSTTND.Text.HasValue())
                                {
                                    JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                                }
                                JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                                var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                                dt_NhomCT.Rows.Add(NewRowNhom);

                                double TyLe = KLTong == 0 ? 0 : KLTinh / KLTong;
                                for (int k= RowIndexCTac - 2; k > 0; k--)
                                {
                                    Row CrowCTac = wsCtac.Rows[k];
                                    _STT = CrowCTac[txt_congtrinh_stt.Text].Value.ToString();
                                    MaHieu = CrowCTac[txtCTMaDuToan.Text].Value.ToString();
                                    if (_STT == "STT")
                                        break;
                                    else if (!string.IsNullOrEmpty(_STT) && !string.IsNullOrEmpty(MaHieu))
                                    {
                                        TenCongTac = CrowCTac[txtCTTenCongTac.Text].Value.ToString();
                                        DonVi = CrowCTac[txtCTDonVi.Text].Value.ToString();
                                        string code = Guid.NewGuid().ToString();
                                        DataRow dtRow = dt.NewRow();
                                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                        dtRow["Code"] = code;
                                        string codecongtactheogiaidoan = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                                        dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCTac[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                        double.TryParse(CrowCTac[Dic_ColKLCTac[itemTuyen]].Value.ToString(), out double KLCtac);
                                        dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                                            = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = Math.Round(TyLe * KLCtac, 4);
                                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                        dtRow["CodeHangMuc"] = CodeHM;
                                        dtRow["MaHieuCongTac"] = MaHieu;
                                        dtRow["TenCongTac"] = TenCongTac;
                                        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                        dtRow["DonVi"] = DonVi;
                                        if (!string.IsNullOrEmpty(CodeTuyen))
                                            dtRow["CodePhanTuyen"] = CodeTuyen;
                                        if (!string.IsNullOrEmpty(CodeDDCT))
                                        {
                                            dtRow["CodeDienDai"] = CodeDDCT;
                                            if (Index > 2)
                                                CodeDDCT = string.Empty;
                                        }
                                        if (txtCTDGVatLieu.Text.HasValue())
                                        {
                                            long.TryParse(CrowCTac[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                        }

                                        if (txtCTDGNhanCong.Text.HasValue())
                                        {
                                            long.TryParse(CrowCTac[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                        }

                                        if (txtCTDGMay.Text.HasValue())
                                        {
                                            long.TryParse(CrowCTac[txtCTDGMay.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                        }
                                        if (txtCTKhoiLuongDocVao.Text.HasValue())
                                        {
                                            double.TryParse(CrowCTac[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                            if (KLDV > 0)
                                                dtRow["KhoiLuongDocVao"] = KLDV;
                                            else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                                dtRow["KhoiLuongDocVao"] = 0;
                                        }
                                        TDKH_GhiChuBoSungJson JsonGhiChuCtac = new TDKH_GhiChuBoSungJson();
                                        JsonGhiChuCtac.STT = _STT;
                                        if (txt_congtrinh_sttND.Text.HasValue())
                                        {
                                            JsonGhiChuCtac.STTND = CrowCTac[txt_congtrinh_sttND.Text].Value.ToString();
                                        }
                                        JsonGhiChuCtac.CodeDanhMucCongTac = code;
                                        var encryptedStrCtac = JsonConvert.SerializeObject(JsonGhiChuCtac);
                                        dtRow["GhiChuBoSungJson"] = encryptedStrCtac;
                                        dtRow["Modified"] = true;
                                        dtRow["PhatSinh"] = false;
                                        dtRow_congtactheogiadoan["Modified"] = true;
                                        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCTac[te_KHBegin.Text].Value.IsDateTime ? CrowCTac[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCTac[te_KHEnd.Text].Value.IsDateTime ? CrowCTac[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        double.TryParse(CrowCTac[Dic_ColDonGiaCTac[itemTuyen]].Value.ToString(), out double DonGiaCtacTinh);
                                        dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                         = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCtacTinh;
                                        dt.Rows.Add(dtRow);
                                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);

                                        Fomula = CrowCTac[txtCTMaDuToan.Text].Formula;
                                        if (string.IsNullOrEmpty(Fomula))
                                        {
                                            string codevatu = Guid.NewGuid().ToString();
                                            DataRow dt_vattu = dtHaoPhi.NewRow();
                                            dt_vattu["Code"] = codevatu;
                                            dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                                            dt_vattu["MaVatLieu"] = "TT";
                                            dt_vattu["VatTu"] = TenCongTac;
                                            dt_vattu["DonVi"] = DonVi;
                                            double.TryParse(crRow[Dic_ColDonGiaVTNoLink[itemTuyen]].Value.ToString(), out double DonGiaVL);
                                            dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = DonGiaVL;
                                            dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dt_vattu["LoaiVatTu"] = "Vật liệu";
                                            dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                            dtHaoPhi.Rows.Add(dt_vattu);
                                        }
                                        else
                                        {
                                            MatchCollection MultimatchVT = SelectVT.Matches(Fomula);
                                            if (MultimatchVT.Count == 0)
                                            {
                                                string FormulaSheet = Fomula;
                                                do
                                                {
                                                    string FormulaCheck = Regex.Replace(FormulaSheet, $@"\D", string.Empty);
                                                    FormulaSheet = wsCtac.Rows[int.Parse(FormulaCheck) - 1][txtCTMaDuToan.Text].Formula;
                                                }
                                                while (!FormulaSheet.Contains(cboHPVTtenSheet.Text));

                                                MultimatchVT = SelectVT.Matches(FormulaSheet);
                                                int RowIndexVT = int.Parse(MultimatchVT[0].Groups["ViTri"].ToString());
                                                fcn_updateHaophiKienGiangCaMau(end, cboHPVTtenSheet.Text, Dic_ColDonGiaVT[itemTuyen], dtHaoPhi,
                                                    RowIndexVT, codecongtactheogiaidoan);
                                            }
                                            else
                                            {
                                                int RowIndexVT = int.Parse(MultimatchVT[0].Groups["ViTri"].ToString());
                                                fcn_updateHaophiKienGiangCaMau(end, cboHPVTtenSheet.Text, Dic_ColDonGiaVT[itemTuyen], dtHaoPhi,
                                                    RowIndexVT, codecongtactheogiaidoan);
                                            }
                                        }
                                    }
                                }



                            }



                        }
                    }
                                    
                }
                else if (string.IsNullOrEmpty(MaHieu) && !string.IsNullOrEmpty(_STT))
                {
                    if (!Dic_CodeHM.Any())
                    {
                        int IndexHM = 1;
                        foreach (var itemHM in ArrCodeCT)
                        {
                            string CodeHM = Guid.NewGuid().ToString();
                            Dic_CodeHM.Add(IndexHM++, CodeHM);
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{CodeHM}','{itemHM}',@Ten)";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { $"Hạng mục {dt_HM.Rows.Count + 1}" });
                        }
                    }
                    int Index = 1;
                    Regex Select = new Regex($@"\D{cboCTtenSheet.Text}\D+(?<ViTri>\d+)");
                    Regex SelectVT = new Regex($@"\D{cboHPVTtenSheet.Text}\D+(?<ViTri>\d+)");
                    string Fomula = crRow[te_TongHopSTT.Text].Formula;
                    MatchCollection Multimatch = Select.Matches(Fomula);
                    int RowIndexCTac=int.Parse(Multimatch[0].Groups["ViTri"].ToString());
                    string STTNhom = _STT;
                    foreach (var itemNhom in ArrTenCT)
                    {
                        DataRow NewRowNhom = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTong);
                        double.TryParse(crRow[Dic_ColDonGiaNhom[itemNhom]].Value.ToString(), out double DonGiaTinh);
                        double.TryParse(crRow[Dic_ColKLNhom[itemNhom]].Value.ToString(), out double KLTinh);
                        string CodeHM = Dic_CodeHM[Index++];
                        NewRowNhom["Code"] = CodeNhomCT;
                        NewRowNhom["CodeHangMuc"] = CodeHM;
                        NewRowNhom["Ten"] = crRow[te_TongHopTenCT.Text].Value.ToString(); ;
                        if (!string.IsNullOrEmpty(CodeDDNhom))
                        {
                            NewRowNhom["CodeDienDai"] = CodeDDNhom;
                            if(Index>2)
                                CodeDDNhom = string.Empty;
                        }
                        if (DonGiaTinh > 0)
                            NewRowNhom["DonGia"] = DonGiaTinh;
                        NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                        if (KLTinh > 0)
                            NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTinh;
                        NewRowNhom["SortId"] = SortIdNhom++;
                        if (!string.IsNullOrEmpty(CodeTuyen))
                            NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = STTNhom;
                        if (te_TongHopSTTND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                        dt_NhomCT.Rows.Add(NewRowNhom);

                        double TyLe = KLTong == 0 ? 0 : KLTinh / KLTong;
                        for (int j = RowIndexCTac-2; j > 0; j--)
                        {
                            Row CrowCTac = wsCtac.Rows[j];
                            _STT = CrowCTac[txt_congtrinh_stt.Text].Value.ToString();
                            MaHieu = CrowCTac[txtCTMaDuToan.Text].Value.ToString();
                            if (_STT == "STT")
                                break;
                            else if (!string.IsNullOrEmpty(_STT) && !string.IsNullOrEmpty(MaHieu))
                            {
                                TenCongTac = CrowCTac[txtCTTenCongTac.Text].Value.ToString();
                                DonVi = CrowCTac[txtCTDonVi.Text].Value.ToString();
                                string code = Guid.NewGuid().ToString();
                                DataRow dtRow = dt.NewRow();
                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                dtRow["Code"] = code;
                                string codecongtactheogiaidoan = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                dtRow_congtactheogiadoan["SortId"] = SortId++;
                                dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCTac[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                double.TryParse(CrowCTac[Dic_ColKLCTac[itemNhom]].Value.ToString(), out double KLCtac);
                                dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                                    = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = Math.Round(TyLe*KLCtac,4);
                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                dtRow["CodeHangMuc"] = CodeHM;
                                dtRow["MaHieuCongTac"] = MaHieu;
                                dtRow["TenCongTac"] = TenCongTac;
                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                dtRow["DonVi"] = DonVi;
                                if (!string.IsNullOrEmpty(CodeTuyen))
                                    dtRow["CodePhanTuyen"] = CodeTuyen;
                                if (!string.IsNullOrEmpty(CodeDDCT))
                                {
                                    dtRow["CodeDienDai"] = CodeDDCT;
                                    if (Index > 2)
                                        CodeDDCT = string.Empty;
                                }
                                if (txtCTDGVatLieu.Text.HasValue())
                                {
                                    long.TryParse(CrowCTac[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                }

                                if (txtCTDGNhanCong.Text.HasValue())
                                {
                                    long.TryParse(CrowCTac[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                }

                                if (txtCTDGMay.Text.HasValue())
                                {
                                    long.TryParse(CrowCTac[txtCTDGMay.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                }
                                if (txtCTKhoiLuongDocVao.Text.HasValue())
                                {
                                    double.TryParse(CrowCTac[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                    if (KLDV > 0)
                                        dtRow["KhoiLuongDocVao"] = KLDV;
                                    else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                        dtRow["KhoiLuongDocVao"] = 0;
                                }
                                TDKH_GhiChuBoSungJson JsonGhiChuCtac = new TDKH_GhiChuBoSungJson();
                                JsonGhiChuCtac.STT = _STT;
                                if (txt_congtrinh_sttND.Text.HasValue())
                                {
                                    JsonGhiChuCtac.STTND = CrowCTac[txt_congtrinh_sttND.Text].Value.ToString();
                                }
                                JsonGhiChuCtac.CodeDanhMucCongTac = code;
                                var encryptedStrCtac = JsonConvert.SerializeObject(JsonGhiChuCtac);
                                dtRow["GhiChuBoSungJson"] = encryptedStrCtac;
                                dtRow["Modified"] = true;
                                dtRow["PhatSinh"] = false;
                                dtRow_congtactheogiadoan["Modified"] = true;
                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCTac[te_KHBegin.Text].Value.IsDateTime ? CrowCTac[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCTac[te_KHEnd.Text].Value.IsDateTime ? CrowCTac[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                double.TryParse(CrowCTac[Dic_ColDonGiaCTac[itemNhom]].Value.ToString(), out double DonGiaCtacTinh);
                                dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                 = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCtacTinh;
                                dt.Rows.Add(dtRow);
                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);

                                Fomula = CrowCTac[txtCTMaDuToan.Text].Formula;
                                if (string.IsNullOrEmpty(Fomula))
                                {
                                    string codevatu = Guid.NewGuid().ToString();
                                    DataRow dt_vattu = dtHaoPhi.NewRow();
                                    dt_vattu["Code"] = codevatu;
                                    dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                                    dt_vattu["MaVatLieu"] = "TT";
                                    dt_vattu["VatTu"] = TenCongTac;
                                    dt_vattu["DonVi"] = DonVi;
                                    double.TryParse(crRow[Dic_ColDonGiaVTNoLink[itemNhom]].Value.ToString(), out double DonGiaVL);
                                    dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = DonGiaVL;
                                    dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dt_vattu["LoaiVatTu"] = "Vật liệu";
                                    dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                    dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                    dtHaoPhi.Rows.Add(dt_vattu);
                                }
                                else
                                {
                                    MatchCollection MultimatchVT = SelectVT.Matches(Fomula);
                                    if (MultimatchVT.Count == 0)
                                    {
                                        string FormulaSheet = Fomula;
                                        do
                                        {
                                            string FormulaCheck = Regex.Replace(FormulaSheet, $@"\D", string.Empty);
                                            FormulaSheet = wsCtac.Rows[int.Parse(FormulaCheck) - 1][txtCTMaDuToan.Text].Formula;
                                        }
                                        while (!FormulaSheet.Contains(cboHPVTtenSheet.Text));

                                        MultimatchVT = SelectVT.Matches(FormulaSheet);
                                        int RowIndexVT = int.Parse(MultimatchVT[0].Groups["ViTri"].ToString());
                                        fcn_updateHaophiKienGiangCaMau(end, cboHPVTtenSheet.Text, Dic_ColDonGiaVT[itemNhom], dtHaoPhi,
                                            RowIndexVT, codecongtactheogiaidoan);
                                    }
                                    else
                                    {
                                        int RowIndexVT = int.Parse(MultimatchVT[0].Groups["ViTri"].ToString());
                                        fcn_updateHaophiKienGiangCaMau(end, cboHPVTtenSheet.Text, Dic_ColDonGiaVT[itemNhom], dtHaoPhi,
                                            RowIndexVT, codecongtactheogiaidoan);
                                    }
                                }
                            }
                        }


                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_TenDienDaiTuDo, TDKH.Tbl_TenDienDaiTuDo);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
            }
            spsheet_XemFile.EndUpdate();
            try
            {

            }
            catch (Exception) { }
            WaitFormHelper.ShowWaitForm("Đang tính lại Khối lượng Kế hoạch, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            TDKHHelper.CalcOthersMaterials();
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();

            this.Close();

        }
        private void Fcn_DocMauKienGiangCanTho(int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            string DonVi = string.Empty, codetheogiaidoan = string.Empty, _STT = string.Empty, TenCongTac = string.Empty;
            string VL = string.Empty, NC = string.Empty, MTC = string.Empty;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            Worksheet ws = workbook.Worksheets[cbe_TongHop.Text];
            Worksheet wsLapGia = workbook.Worksheets[cbb_sheet.Text];
            Worksheet wsCongTrinh = workbook.Worksheets[cboCTtenSheet.Text];
            int LastLine = wsLapGia.GetUsedRange().BottomRowIndex;
            int IndexCT = rg_LayDuLieu.SelectedIndex;
            if (IndexCT > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            double KLDuThau = 0, KLHaiDang = 0, DonGiaDuToan = 0;
            for (int i = firstLine; i <= lastLine; i++)
            {
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                _STT = crRow[te_TongHopSTT.Text].Value.ToString();
                TenCongTac = crRow[te_TongHopTenCT.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{_STT}_{TenCongTac}");
                if (_STT.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (IndexCT > 0)
                        continue;
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                    continue;
                }
                else if (_STT.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    if (string.IsNullOrEmpty(codecongtrinh))
                    {
                        codecongtrinh = Guid.NewGuid().ToString();
                        tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                    }
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (_STT.ToUpper() == te_PhanTuyen.Text)
                {
                    if (string.IsNullOrEmpty(codehangmuc))
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    }
                    string TenTuyen = crRow[te_TongHopTenCT.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        crRow = ws.Rows[j];
                        _STT = crRow[te_TongHopMaHieu.Text].Value.ToString();
                        if (_STT == te_PhanTuyen.Text || _STT.ToUpper() == "THM" || _STT == te_EndTuyen.Text || _STT == "" || _STT.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        double.TryParse(crRow[te_TongHopKhoiLuongMoiThau.Text].Value.ToString(), out KLDuThau);
                        double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out KLHaiDang);
                        if (KLDuThau == 0 && KLHaiDang == 0)
                            continue;
                        DonVi = crRow[te_TongHopDonVi.Text].Value.TextValue;
                        double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out DonGiaDuToan);
                        KeyValuePair<long, long> ValueSortid = Fcn_CheckCongTacOrNhomOrHangMuc(crRow, wsLapGia, wsCongTrinh, _STT, KLDuThau, DonGiaDuToan, KLHaiDang, DonVi,
                            LastLine, dt_congtactheogiaidoan, dt, dt_NhomCT, SortId, SortIdNhom, dtHaoPhi: dtHaoPhi, CodeTuyen: CodeTuyen);
                        SortId = ValueSortid.Key;
                        SortIdNhom = ValueSortid.Value;
                    }
                }
                else
                {
                    double.TryParse(crRow[te_TongHopKhoiLuongMoiThau.Text].Value.ToString(), out KLDuThau);
                    double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out KLHaiDang);
                    if (KLDuThau == 0 && KLHaiDang == 0)
                        continue;
                    DonVi = crRow[te_TongHopDonVi.Text].Value.TextValue;
                    if (string.IsNullOrEmpty(codehangmuc))
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    }
                    double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out DonGiaDuToan);
                    KeyValuePair<long,long> ValueSortid = Fcn_CheckCongTacOrNhomOrHangMuc(crRow, wsLapGia, wsCongTrinh, _STT, KLDuThau, DonGiaDuToan, KLHaiDang, DonVi,
                        LastLine, dt_congtactheogiaidoan, dt, dt_NhomCT, SortId, SortIdNhom, dtHaoPhi: dtHaoPhi);
                    SortId = ValueSortid.Key;
                    SortIdNhom = ValueSortid.Value;
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }
            spsheet_XemFile.EndUpdate();
            try
            {

            }
            catch (Exception) { }
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            TDKHHelper.CalcOthersMaterials();
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();

            this.Close();



        }
        private KeyValuePair<long,long> Fcn_CheckCongTacOrNhomOrHangMuc(Row PhanBo, Worksheet LapGia, Worksheet CongTrinh, string STT, double KLMoiThau,
            double DonGiaDuThau, double KLHaiDang, string DonVi,
            int LastLine, DataTable dt_congtactheogiaidoan, DataTable dt, DataTable dt_NhomCT, long SortId, long SortIdNhom, DataTable dtHaoPhi, string CodeTuyen = default)
        {
            int RowIndex = 0;
            Row Crow;
            string codecongtactheogiaidoan = string.Empty;
            Regex Select = new Regex($@"\D+{cboCTtenSheet.Text}\D\D\D(?<TenCot>\D)\D(?<ViTri>\d+)");
            for (int i = 0; i < LastLine; i++)
            {
                Crow = LapGia.Rows[i];
                double.TryParse(Crow[txt_klduthau.Text].Value.ToString(), out double KLMoiThauCheck);
                if (STT == Crow[txt_STT.Text].Value.ToString() && DonVi == Crow[txt_tendonvi.Text].Value.ToString() && KLMoiThau == KLMoiThauCheck)
                    RowIndex = i;

            }
            if (RowIndex == 0)
                return new KeyValuePair<long, long>(SortId,SortIdNhom);
            Crow = LapGia.Rows[RowIndex];
            string Fomula = Crow[txt_dongiaduthau.Text].Formula;
            if (Fomula.Contains(cboCTtenSheet.Text))
            {
                //int index = Fomula.IndexOf(cboCTtenSheet.Text);
                int RowIndexCT = 0;
                string TenCotDonGia = string.Empty;
                MatchCollection Multimatch = Select.Matches(Fomula);
                //Fomula = Fomula.Remove(0, index);
                //Fomula = Regex.Replace(Fomula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                //Row CrowCongTrinh = CongTrinh.Rows[int.Parse(Fomula)];
                RowIndexCT = int.Parse(Multimatch[0].Groups["ViTri"].ToString());
                if (RowIndexCT == 75)
                {
                    string tennhom = Crow[txt_tencongtac_HD.Text].Value.ToString();
                    DataRow NewRowNhom = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRowNhom["Code"] = CodeNhomCT;
                    NewRowNhom["CodeHangMuc"] = codehangmuc;
                    NewRowNhom["Ten"] = tennhom;
                    NewRowNhom["CodePhanTuyen"] = CodeTuyen != default ? CodeTuyen : null;
                    if (DonGiaDuThau > 0)
                        NewRowNhom["DonGia"] = DonGiaDuThau;
                    NewRowNhom["DonVi"] = DonVi;
                    if (KLHaiDang > 0)
                        NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLHaiDang;
                    NewRowNhom["SortId"] = SortIdNhom++;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = STT;
                    if (te_TongHopSTTND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                    dt_NhomCT.Rows.Add(NewRowNhom);
                    Row CrowCTCon;
                    for (int i= RowIndexCT;i<= RowIndexCT + 1; i++)
                    {
                        CrowCTCon = CongTrinh.Rows[i];
                        string TTCon = CrowCTCon["N"].Formula;
                        double.TryParse(CrowCTCon["K"].Value.ToString(), out double KLCongTacCon);
                        string tencongtac = CrowCTCon[txtCTTenCongTac.Text].Value.ToString();
                        string MaHieu = CrowCTCon[txtCTMaDuToan.Text].Value.ToString();
                        string code = Guid.NewGuid().ToString();
                        DataRow dtRow = dt.NewRow();
                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                        dtRow["Code"] = code;
                        codecongtactheogiaidoan = Guid.NewGuid().ToString();
                        dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                        dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCTCon[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                        dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                            = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCongTacCon;
                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                        dtRow["CodeHangMuc"] = codehangmuc;
                        dtRow["MaHieuCongTac"] = MaHieu;
                        dtRow["TenCongTac"] = tencongtac;
                        dtRow["DonVi"] = CrowCTCon[txtCTDonVi.Text].Value.ToString();
                        if (CodeTuyen != default)
                            dtRow["CodePhanTuyen"] = CodeTuyen;
                        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                        if (txtCTDGVatLieu.Text.HasValue())
                        {
                            long.TryParse(CrowCTCon[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                            dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                        }

                        if (txtCTDGNhanCong.Text.HasValue())
                        {
                            long.TryParse(CrowCTCon[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                            dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                        }

                        if (txtCTDGMay.Text.HasValue())
                        {
                            long.TryParse(CrowCTCon[txtCTDGMay.Text].Value.ToString(), out long dg);
                            dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                        }
                        if (txtCTKhoiLuongDocVao.Text.HasValue())
                        {
                            double.TryParse(CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                            if (KLDV > 0)
                                dtRow["KhoiLuongDocVao"] = KLDV;
                            else if (!CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                dtRow["KhoiLuongDocVao"] = 0;
                        }
                        TDKH_GhiChuBoSungJson JsonGhiChuCon = new TDKH_GhiChuBoSungJson();
                        JsonGhiChuCon.STT = CrowCTCon[txt_congtrinh_stt.Text].Value.ToString(); ;
                        if (txt_congtrinh_sttND.Text.HasValue())
                        {
                            JsonGhiChuCon.STTND = CrowCTCon[txt_congtrinh_sttND.Text].Value.ToString();
                        }
                        JsonGhiChuCon.CodeDanhMucCongTac = code;
                        var encryptedStrCon = JsonConvert.SerializeObject(JsonGhiChuCon);
                        dtRow["GhiChuBoSungJson"] = encryptedStrCon;
                        string Formula = Regex.Replace(CrowCTCon[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);
                        double DonGiaCon = 0;
                        if (TTCon.Contains("+"))
                        {
                            double.TryParse(CrowCTCon["AF"].Value.ToString(), out double DonGia1);
                            double.TryParse(CrowCTCon["AG"].Value.ToString(), out double DonGia2);
                            DonGiaCon = (DonGia1 + DonGia2) / 2;
                            fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, string.Empty, true);
                        }
                        else
                        {
                            TTCon = TTCon.Replace("=", string.Empty);
                            TTCon = Regex.Replace(TTCon, $@"\d", string.Empty);
                            string ColDonGiaCTac = MyConstant.Dic_DonGia[TTCon];
                            string FomulaDonGia = CrowCTCon[ColDonGiaCTac].Formula;
                            string ColDonGiaVatLieu = string.Empty;
                            double.TryParse(CrowCTCon[ColDonGiaCTac].Value.ToString(), out  DonGiaCon);
                            foreach (var col in MyConstant.Dic_DonGiaVatTu)
                            {
                                string CongThuc = string.Format(col.Key, RowIndexCT, RowIndexCT);
                                if (FomulaDonGia.Contains(CongThuc))
                                    ColDonGiaVatLieu = col.Value;
                            }
                            if (!string.IsNullOrEmpty(ColDonGiaVatLieu))
                            {
                                fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, ColDonGiaVatLieu);

                            }
                        }
                        dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                               = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCon;
                        dtRow["Modified"] = true;
                        dtRow["PhatSinh"] = false;
                        dtRow_congtactheogiadoan["Modified"] = true;
                        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCTCon[te_KHBegin.Text].Value.IsDateTime ? CrowCTCon[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCTCon[te_KHEnd.Text].Value.IsDateTime ? CrowCTCon[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt.Rows.Add(dtRow);
                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                    }
                    return new KeyValuePair<long, long>(SortId, SortIdNhom);
                }
                TenCotDonGia = Multimatch[0].Groups["TenCot"].ToString();
                if (TenCotDonGia == "M")
                    TenCotDonGia = "L";
                if (TenCotDonGia == "I")
                    TenCotDonGia = "H";
                Row CrowCongTrinh = CongTrinh.Rows[RowIndexCT - 1];
                int IndexTenCotDG = CongTrinh.Columns[TenCotDonGia].Index;
                string TT = CrowCongTrinh[IndexTenCotDG + 2].Formula;
                string TTTHop = CrowCongTrinh[IndexTenCotDG + 1].Formula;
                double DonGia = 0;
                if (string.IsNullOrEmpty(TTTHop))
                {
                    string tencongtac = Crow[txt_tencongtac_HD.Text].Value.ToString();
                    string MaHieu = CrowCongTrinh[txtCTMaDuToan.Text].Value.ToString();
                    string code = Guid.NewGuid().ToString();
                    DataRow dtRow = dt.NewRow();
                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                    dtRow["Code"] = code;
                    codecongtactheogiaidoan = Guid.NewGuid().ToString();
                    dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                    dtRow_congtactheogiadoan["SortId"] = SortId++;
                    dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCongTrinh[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                    dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                        = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] =KLHaiDang;
                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                    dtRow["CodeHangMuc"] = codehangmuc;
                    dtRow["MaHieuCongTac"] = MaHieu;
                    dtRow["TenCongTac"] = tencongtac;
                    dtRow["DonVi"] = DonVi;
                    if (CodeTuyen != default)
                        dtRow["CodePhanTuyen"] = CodeTuyen;
                    if (txtCTDGVatLieu.Text.HasValue())
                    {
                        long.TryParse(CrowCongTrinh[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                        dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                    }

                    if (txtCTDGNhanCong.Text.HasValue())
                    {
                        long.TryParse(CrowCongTrinh[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                        dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                    }

                    if (txtCTDGMay.Text.HasValue())
                    {
                        long.TryParse(CrowCongTrinh[txtCTDGMay.Text].Value.ToString(), out long dg);
                        dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                    }
                    if (txtCTKhoiLuongDocVao.Text.HasValue())
                    {
                        double.TryParse(CrowCongTrinh[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                        if (KLDV > 0)
                            dtRow["KhoiLuongDocVao"] = KLDV;
                        else if (!CrowCongTrinh[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                            dtRow["KhoiLuongDocVao"] = 0;
                    }
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = STT;
                    if (te_TongHopSTTND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = code;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    dtRow["GhiChuBoSungJson"] = encryptedStr;
                    dtRow["Modified"] = true;
                    dtRow["PhatSinh"] = false;
                    dtRow_congtactheogiadoan["Modified"] = true;
                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCongTrinh[te_KHBegin.Text].Value.IsDateTime ? CrowCongTrinh[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCongTrinh[te_KHEnd.Text].Value.IsDateTime ? CrowCongTrinh[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    string Formula = Regex.Replace(CrowCongTrinh[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);
                    if (TT.Contains("+"))
                    {
                        //double.TryParse(CrowCongTrinh["AF"].Value.ToString(), out double DonGia1);
                        //double.TryParse(CrowCongTrinh["AG"].Value.ToString(), out double DonGia2);
                        //DonGia = (DonGia1 + DonGia2) / 2;
                        fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, string.Empty, true);
                    }
                    else
                    {
                        TT = TT.Replace("=", string.Empty);
                        TT = Regex.Replace(TT, $@"\d", string.Empty);
                        string ColDonGiaCTac = MyConstant.Dic_DonGia[TT];
                        //double.TryParse(CrowCongTrinh[ColDonGiaCTac].Value.ToString(), out DonGia);
                        string FomulaDonGia = CrowCongTrinh[ColDonGiaCTac].Formula;
                        string ColDonGiaVatLieu = string.Empty;
                        foreach (var item in MyConstant.Dic_DonGiaVatTu)
                        {
                            string CongThuc = string.Format(item.Key, RowIndexCT, RowIndexCT);
                            if (FomulaDonGia.Contains(CongThuc))
                                ColDonGiaVatLieu = item.Value;
                        }
                        if (string.IsNullOrEmpty(ColDonGiaVatLieu))
                            return new KeyValuePair<long, long>(SortId, SortIdNhom);
                        fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, ColDonGiaVatLieu);
                    }
                    dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaDuThau;
                    dt.Rows.Add(dtRow);
                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                    return new KeyValuePair<long, long>(SortId, SortIdNhom);
                }
                else
                {
                    //double.TryParse(CrowCongTrinh[IndexTenCotDG - 1].Value.ToString(), out double KLCongTac);
                    double TyLe = KLMoiThau == 0 ? 0 : KLHaiDang / KLMoiThau;
                    string tennhom = Crow[txt_tencongtac_HD.Text].Value.ToString();
                    DataRow NewRowNhom = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRowNhom["Code"] = CodeNhomCT;
                    NewRowNhom["CodeHangMuc"] = codehangmuc;
                    NewRowNhom["Ten"] = tennhom;
                    NewRowNhom["CodePhanTuyen"] = CodeTuyen != default ? CodeTuyen : null;
                    if (DonGiaDuThau > 0)
                        NewRowNhom["DonGia"] = DonGiaDuThau;
                    NewRowNhom["DonVi"] = DonVi;
                    if (KLHaiDang > 0)
                        NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLHaiDang;
                    NewRowNhom["SortId"] = SortIdNhom++;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = STT;
                    if (te_TongHopSTTND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                    dt_NhomCT.Rows.Add(NewRowNhom);
                    Row CrowCTCon;
                    if (TTTHop.Contains("SUM"))
                    {
                        string TenCotKL= CongTrinh.Columns[IndexTenCotDG-1].Heading;
                        Select = new Regex($@"\D+{TenCotKL}(?<ViTri1>\d+):{TenCotKL}(?<ViTri2>\d+)\D+");
                        Multimatch = Select.Matches(TTTHop);
                        int ViTri1 = int.Parse(Multimatch[0].Groups["ViTri1"].ToString());
                        int ViTri2 = int.Parse(Multimatch[0].Groups["ViTri2"].ToString());
                        for (int i = ViTri1 - 1; i <= ViTri2 - 1; i++)
                        {
                            CrowCTCon = CongTrinh.Rows[i];
                            TT = CrowCTCon[IndexTenCotDG + 2].Formula;
                            double.TryParse(CrowCTCon[IndexTenCotDG - 1].Value.ToString(), out double KLCongTacCon);
                            string tencongtac = CrowCTCon[txtCTTenCongTac.Text].Value.ToString();
                            string MaHieu = CrowCTCon[txtCTMaDuToan.Text].Value.ToString();
                            string code = Guid.NewGuid().ToString();
                            DataRow dtRow = dt.NewRow();
                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                            dtRow["Code"] = code;
                            codecongtactheogiaidoan = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCTCon[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                            dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                                = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = Math.Round(TyLe * KLCongTacCon, 4);
                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                            dtRow["CodeHangMuc"] = codehangmuc;
                            dtRow["MaHieuCongTac"] = MaHieu;
                            dtRow["TenCongTac"] = tencongtac;
                            dtRow["DonVi"] = CrowCTCon[txtCTDonVi.Text].Value.ToString();
                            if (CodeTuyen != default)
                                dtRow["CodePhanTuyen"] = CodeTuyen;
                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                            if (txtCTDGVatLieu.Text.HasValue())
                            {
                                long.TryParse(CrowCTCon[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                            }

                            if (txtCTDGNhanCong.Text.HasValue())
                            {
                                long.TryParse(CrowCTCon[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                            }

                            if (txtCTDGMay.Text.HasValue())
                            {
                                long.TryParse(CrowCTCon[txtCTDGMay.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                            }
                            if (txtCTKhoiLuongDocVao.Text.HasValue())
                            {
                                double.TryParse(CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                if (KLDV > 0)
                                    dtRow["KhoiLuongDocVao"] = KLDV;
                                else if (!CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                    dtRow["KhoiLuongDocVao"] = 0;
                            }
                            TDKH_GhiChuBoSungJson JsonGhiChuCon = new TDKH_GhiChuBoSungJson();
                            JsonGhiChuCon.STT = CrowCTCon[txt_congtrinh_stt.Text].Value.ToString(); ;
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChuCon.STTND = CrowCTCon[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChuCon.CodeDanhMucCongTac = code;
                            var encryptedStrCon = JsonConvert.SerializeObject(JsonGhiChuCon);
                            dtRow["GhiChuBoSungJson"] = encryptedStrCon;
                            string Formula = Regex.Replace(CrowCTCon[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);

                            if (TT.Contains("+"))
                            {
                                double.TryParse(CrowCTCon["AF"].Value.ToString(), out double DonGia1);
                                double.TryParse(CrowCTCon["AG"].Value.ToString(), out double DonGia2);
                                DonGia = (DonGia1 + DonGia2) / 2;
                                fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, string.Empty, true);
                            }
                            else
                            {
                                TT = TT.Replace("=", string.Empty);
                                TT = Regex.Replace(TT, $@"\d", string.Empty);
                                string ColDonGiaCTac = MyConstant.Dic_DonGia[TT];
                                string FomulaDonGia = CrowCTCon[ColDonGiaCTac].Formula;
                                string ColDonGiaVatLieu = string.Empty;
                                double.TryParse(CrowCTCon[ColDonGiaCTac].Value.ToString(), out DonGia);
                                foreach (var col in MyConstant.Dic_DonGiaVatTu)
                                {
                                    string CongThuc = string.Format(col.Key, RowIndexCT, RowIndexCT);
                                    if (FomulaDonGia.Contains(CongThuc))
                                        ColDonGiaVatLieu = col.Value;
                                }
                                if (!string.IsNullOrEmpty(ColDonGiaVatLieu))
                                {
                                    fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, ColDonGiaVatLieu);

                                }
                            }
                            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                            dtRow["Modified"] = true;
                            dtRow["PhatSinh"] = false;
                            dtRow_congtactheogiadoan["Modified"] = true;
                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCTCon[te_KHBegin.Text].Value.IsDateTime ? CrowCTCon[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCTCon[te_KHEnd.Text].Value.IsDateTime ? CrowCTCon[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dt.Rows.Add(dtRow);
                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                        }
                    }
                    else
                    {
                        string[] lstCol = TTTHop.Replace("=", string.Empty).Split('+');
                        foreach (var item in lstCol)
                        {
                            RowIndexCT = int.Parse(Regex.Replace(item, $@"\D", string.Empty));
                            CrowCTCon = CongTrinh.Rows[RowIndexCT - 1];
                            TT = CrowCTCon[IndexTenCotDG + 2].Formula;
                            double.TryParse(CrowCTCon[IndexTenCotDG - 1].Value.ToString(), out double KLCongTacCon);
                            string tencongtac = CrowCTCon[txtCTTenCongTac.Text].Value.ToString();
                            string MaHieu = CrowCTCon[txtCTMaDuToan.Text].Value.ToString();
                            string code = Guid.NewGuid().ToString();
                            DataRow dtRow = dt.NewRow();
                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                            dtRow["Code"] = code;
                            codecongtactheogiaidoan = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCTCon[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                            dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                                = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = Math.Round(TyLe * KLCongTacCon, 4);
                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                            dtRow["CodeHangMuc"] = codehangmuc;
                            dtRow["MaHieuCongTac"] = MaHieu;
                            dtRow["TenCongTac"] = tencongtac;
                            dtRow["DonVi"] = CrowCTCon[txtCTDonVi.Text].Value.ToString();
                            if (CodeTuyen != default)
                                dtRow["CodePhanTuyen"] = CodeTuyen;
                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                            if (txtCTDGVatLieu.Text.HasValue())
                            {
                                long.TryParse(CrowCTCon[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                            }

                            if (txtCTDGNhanCong.Text.HasValue())
                            {
                                long.TryParse(CrowCTCon[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                            }

                            if (txtCTDGMay.Text.HasValue())
                            {
                                long.TryParse(CrowCTCon[txtCTDGMay.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                            }
                            if (txtCTKhoiLuongDocVao.Text.HasValue())
                            {
                                double.TryParse(CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                if (KLDV > 0)
                                    dtRow["KhoiLuongDocVao"] = KLDV;
                                else if (!CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                    dtRow["KhoiLuongDocVao"] = 0;
                            }
                            TDKH_GhiChuBoSungJson JsonGhiChuCon = new TDKH_GhiChuBoSungJson();
                            JsonGhiChuCon.STT = CrowCTCon[txt_congtrinh_stt.Text].Value.ToString(); ;
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChuCon.STTND = CrowCTCon[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChuCon.CodeDanhMucCongTac = code;
                            var encryptedStrCon = JsonConvert.SerializeObject(JsonGhiChuCon);
                            dtRow["GhiChuBoSungJson"] = encryptedStrCon;
                            string Formula = Regex.Replace(CrowCTCon[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);

                            if (TT.Contains("+"))
                            {
                                double.TryParse(CrowCTCon["AF"].Value.ToString(), out double DonGia1);
                                double.TryParse(CrowCTCon["AG"].Value.ToString(), out double DonGia2);
                                DonGia = (DonGia1 + DonGia2) / 2;
                                fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, string.Empty, true);
                            }
                            else
                            {
                                TT = TT.Replace("=", string.Empty);
                                TT = Regex.Replace(TT, $@"\d", string.Empty);
                                string ColDonGiaCTac = MyConstant.Dic_DonGia[TT];
                                string FomulaDonGia = CrowCTCon[ColDonGiaCTac].Formula;
                                string ColDonGiaVatLieu = string.Empty;
                                double.TryParse(CrowCTCon[ColDonGiaCTac].Value.ToString(), out DonGia);
                                foreach (var col in MyConstant.Dic_DonGiaVatTu)
                                {
                                    string CongThuc = string.Format(col.Key, RowIndexCT, RowIndexCT);
                                    if (FomulaDonGia.Contains(CongThuc))
                                        ColDonGiaVatLieu = col.Value;
                                }
                                if (!string.IsNullOrEmpty(ColDonGiaVatLieu))
                                {
                                    fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, ColDonGiaVatLieu);

                                }
                            }
                            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                            dtRow["Modified"] = true;
                            dtRow["PhatSinh"] = false;
                            dtRow_congtactheogiadoan["Modified"] = true;
                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCTCon[te_KHBegin.Text].Value.IsDateTime ? CrowCTCon[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCTCon[te_KHEnd.Text].Value.IsDateTime ? CrowCTCon[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dt.Rows.Add(dtRow);
                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                        }
                    }
                }
            }
            else if (Fomula.Contains("4.CPXD-PA2"))
            {
                string tennhom = Crow[txt_tencongtac_HD.Text].Value.ToString();
                DataRow NewRowNhom = dt_NhomCT.NewRow();
                string CodeNhomCT = Guid.NewGuid().ToString();
                NewRowNhom["Code"] = CodeNhomCT;
                NewRowNhom["CodeHangMuc"] = codehangmuc;
                NewRowNhom["Ten"] = tennhom;
                NewRowNhom["CodePhanTuyen"] = CodeTuyen != default ? CodeTuyen : null;
                if (DonGiaDuThau > 0)
                    NewRowNhom["DonGia"] = DonGiaDuThau;
                NewRowNhom["DonVi"] = DonVi;
                if (KLHaiDang > 0)
                    NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLHaiDang;
                NewRowNhom["SortId"] = SortIdNhom++;
                TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                JsonGhiChu.STT = STT;
                if (te_TongHopSTTND.Text.HasValue())
                {
                    JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                }
                JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                dt_NhomCT.Rows.Add(NewRowNhom);

                Select = new Regex($@"\D+4.CPXD-PA2\D\D\D(?<TenCot>\D)\D(?<ViTri>\d+)");
                int RowIndexCT = 0;
                string TenCotDonGia = string.Empty;
                MatchCollection Multimatch = Select.Matches(Fomula);
                RowIndexCT = int.Parse(Multimatch[0].Groups["ViTri"].ToString());
                TenCotDonGia = Multimatch[0].Groups["TenCot"].ToString();
                Worksheet wsHangMuc = CongTrinh.Workbook.Worksheets["4.CPXD-PA2"];
                Row HangMuc = wsHangMuc.Rows[RowIndexCT - 1];
                Select = new Regex($@"\D+{TenCotDonGia}(?<ViTri1>\d+):{TenCotDonGia}(?<ViTri2>\d+)\D+");
                Fomula = HangMuc[TenCotDonGia].Formula;
                MatchCollection MultimatchHM = Select.Matches(Fomula);
                int ViTri1 = int.Parse(MultimatchHM[0].Groups["ViTri1"].ToString());
                int ViTri2 = int.Parse(MultimatchHM[0].Groups["ViTri2"].ToString());
                for (int i = ViTri1 - 1; i <= ViTri2 - 1; i++)
                {
                    HangMuc = wsHangMuc.Rows[i];
                    string tencongtac = HangMuc["C"].Value.ToString();
                    string MaHieu = "TT";
                    string code = Guid.NewGuid().ToString();
                    DataRow dtRow = dt.NewRow();
                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                    dtRow["Code"] = code;
                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                    dtRow_congtactheogiadoan["SortId"] = SortId++;
                    //dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                    double.TryParse(HangMuc["K"].Value.ToString(), out double KLTT);
                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] =
                        dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLTT;
                    dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                    dtRow["CodeHangMuc"] = codehangmuc;
                    dtRow["MaHieuCongTac"] = MaHieu;
                    dtRow["TenCongTac"] = tencongtac;
                    dtRow["DonVi"] = "trọn gói";
                    dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
           = dtRow_congtactheogiadoan["DonGiaDuThau"] = 1;
                    dtRow["Modified"] = true;
                    dtRow["PhatSinh"] = false;
                    dtRow_congtactheogiadoan["Modified"] = true;
                    dtRow_congtactheogiadoan["SortId"] = SortId++;
                    dtRow_congtactheogiadoan["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dtRow_congtactheogiadoan["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    TDKH_GhiChuBoSungJson JsonGhiChuCon = new TDKH_GhiChuBoSungJson();
                    JsonGhiChuCon.STT = HangMuc["A"].Value.ToString(); ;
                    JsonGhiChuCon.CodeDanhMucCongTac = code;
                    var encryptedStrCon = JsonConvert.SerializeObject(JsonGhiChuCon);
                    dtRow["GhiChuBoSungJson"] = encryptedStrCon;
                    dt.Rows.Add(dtRow);
                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                }  
                //Fomula = wsHangMuc.Rows[ViTri1 - 1][TenCotDonGia].Formula;
                //Select = new Regex($@"\D+{cboCTtenSheet.Text}\D\D\D(?<TenCot>\D)\D(?<ViTri>\d+)");
                //MatchCollection MultiHM = Select.Matches(Fomula);
                //RowIndexCT = int.Parse(MultiHM[0].Groups["ViTri"].ToString());
                //TenCotDonGia = MultiHM[0].Groups["TenCot"].ToString();
                //Row CrowCongTrinh = CongTrinh.Rows[RowIndexCT - 1];
                //Select = new Regex($@"\D+{TenCotDonGia}(?<ViTri1>\d+):{TenCotDonGia}(?<ViTri2>\d+)\D+");
                //Fomula = CrowCongTrinh[TenCotDonGia].Formula;
                //MultiHM = Select.Matches(Fomula);
                //int ViTriHM1 = int.Parse(MultiHM[0].Groups["ViTri1"].ToString());
                //int ViTriHM2 = int.Parse(MultiHM[0].Groups["ViTri2"].ToString());
                //string MaHM = "D";
                //int STTHM = 1;
                //string TenNhom = $"{MaHM}{STTHM}";
                //string CodeNhomCT = string.Empty;
                //for (int i = ViTriHM1 - 1; i <= ViTriHM2 - 1; i++)
                //{
                //    CrowCongTrinh = CongTrinh.Rows[i];
                //    if (CrowCongTrinh[TenCotDonGia].Value.IsEmpty)
                //        continue;
                //    string MaHieu = CrowCongTrinh[txtCTMaDuToan.Text].Value.ToString();
                //    if (MaHieu == TenNhom)
                //    {
                //        CodeNhomCT = Guid.NewGuid().ToString();
                //        DataRow NewRowNhom = dt_NhomCT.NewRow();
                //        NewRowNhom["Code"] = CodeNhomCT;
                //        NewRowNhom["CodeHangMuc"] = codehangmuc;
                //        NewRowNhom["Ten"] = CrowCongTrinh[txtCTTenCongTac.Text].Value.TextValue;
                //        NewRowNhom["CodePhanTuyen"] = CodeTuyen != default ? CodeTuyen : null;
                //        double.TryParse(CrowCongTrinh["L"].Value.ToString(), out double DGNhom);
                //        if (DGNhom > 0)
                //            NewRowNhom["DonGia"] = DGNhom;
                //        NewRowNhom["DonVi"] = CrowCongTrinh[txtCTDonVi.Text].Value.TextValue;
                //        double.TryParse(CrowCongTrinh["K"].Value.ToString(), out double KLNhom);
                //        if (KLNhom > 0)
                //            NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                //        NewRowNhom["SortId"] = SortIdNhom++;
                //        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                //        JsonGhiChu.STT = CrowCongTrinh[txt_congtrinh_stt.Text].Value.TextValue; ;
                //        if (txt_congtrinh_sttND.Text.HasValue())
                //        {
                //            JsonGhiChu.STTND = PhanBo[txt_congtrinh_sttND.Text].Value.ToString();
                //        }
                //        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                //        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                //        NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                //        dt_NhomCT.Rows.Add(NewRowNhom);
                //        STTHM++;
                //        TenNhom = $"{MaHM}{STTHM}";
                //    }
                //    else
                //    {
                //        string tencongtac = CrowCongTrinh[txtCTTenCongTac.Text].Value.ToString();
                //        string code = Guid.NewGuid().ToString();
                //        DataRow dtRow = dt.NewRow();
                //        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                //        dtRow["Code"] = code;
                //        codecongtactheogiaidoan = Guid.NewGuid().ToString();
                //        dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                //        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                //        dtRow_congtactheogiadoan["SortId"] = SortId++;
                //        dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCongTrinh[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                //        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                //        double.TryParse(CrowCongTrinh["K"].Value.ToString(), out double KLCTCon);
                //        dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                //            = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTCon;
                //        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                //        dtRow["CodeHangMuc"] = codehangmuc;
                //        dtRow["MaHieuCongTac"] = MaHieu;
                //        dtRow["TenCongTac"] = tencongtac;
                //        dtRow["DonVi"] = CrowCongTrinh[txtCTDonVi.Text].Value.ToString();
                //        if (CodeTuyen != default)
                //            dtRow["CodePhanTuyen"] = CodeTuyen;
                //        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                //        if (txtCTDGVatLieu.Text.HasValue())
                //        {
                //            long.TryParse(CrowCongTrinh[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                //            dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                //        }

                //        if (txtCTDGNhanCong.Text.HasValue())
                //        {
                //            long.TryParse(CrowCongTrinh[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                //            dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                //        }

                //        if (txtCTDGMay.Text.HasValue())
                //        {
                //            long.TryParse(CrowCongTrinh[txtCTDGMay.Text].Value.ToString(), out long dg);
                //            dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                //        }
                //        if (txtCTKhoiLuongDocVao.Text.HasValue())
                //        {
                //            double.TryParse(CrowCongTrinh[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                //            if (KLDV > 0)
                //                dtRow["KhoiLuongDocVao"] = KLDV;
                //        }
                //        TDKH_GhiChuBoSungJson JsonGhiChuCon = new TDKH_GhiChuBoSungJson();
                //        JsonGhiChuCon.STT = CrowCongTrinh[txt_congtrinh_stt.Text].Value.ToString(); ;
                //        if (txt_congtrinh_sttND.Text.HasValue())
                //        {
                //            JsonGhiChuCon.STTND = CrowCongTrinh[txt_congtrinh_sttND.Text].Value.ToString();
                //        }
                //        JsonGhiChuCon.CodeDanhMucCongTac = code;
                //        var encryptedStrCon = JsonConvert.SerializeObject(JsonGhiChuCon);
                //        dtRow["GhiChuBoSungJson"] = encryptedStrCon;

                //        double.TryParse(CrowCongTrinh["L"].Value.ToString(), out double DonGia);
                //        dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                //               = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                //        dtRow["Modified"] = true;
                //        dtRow["PhatSinh"] = false;
                //        dtRow_congtactheogiadoan["Modified"] = true;
                //        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCongTrinh[te_KHBegin.Text].Value.IsDateTime ? CrowCongTrinh[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                //        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCongTrinh[te_KHEnd.Text].Value.IsDateTime ? CrowCongTrinh[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                //        dt.Rows.Add(dtRow);
                //        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);

                //        string Formula = Regex.Replace(CrowCongTrinh[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);
                //        //string FomulaDonGia = CrowCongTrinh[ColDonGiaCTac].Formula;
                //        //string ColDonGiaVatLieu = string.Empty;
                //        //foreach (var col in MyConstant.Dic_DonGiaVatTu)
                //        //{
                //        //    string CongThuc = string.Format(col.Key, RowIndexCT, RowIndexCT);
                //        //    if (FomulaDonGia.Contains(CongThuc))
                //        //        ColDonGiaVatLieu = col.Value;
                //        //}
                //        //if (!string.IsNullOrEmpty(ColDonGiaVatLieu))
                //        //{
                //        //    fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, ColDonGiaVatLieu);

                //        //}
                //    }
                //}

            }
            else if (string.IsNullOrEmpty(Fomula) || Fomula.Contains(cboHPVTtenSheet.Text))
            {
                //if (string.IsNullOrEmpty(Fomula) || Fomula.Contains(cboHPVTtenSheet.Text))
                string tencongtac = Crow[txt_tencongtac_HD.Text].Value.ToString();
                string MaHieu = "TT";
                string code = Guid.NewGuid().ToString();
                DataRow dtRow = dt.NewRow();
                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                dtRow["Code"] = code;
                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                dtRow_congtactheogiadoan["SortId"] = SortId++;
                //dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                dtRow_congtactheogiadoan["KhoiLuongToanBo"] =
                    dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLHaiDang;
                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                dtRow_congtactheogiadoan["SortId"] = SortId++;
                dtRow["CodeHangMuc"] = codehangmuc;
                dtRow["MaHieuCongTac"] = MaHieu;
                dtRow["TenCongTac"] = tencongtac;
                dtRow["DonVi"] = DonVi;
                if (CodeTuyen != default)
                    dtRow["CodePhanTuyen"] = CodeTuyen;
                TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                JsonGhiChu.STT = STT;
                if (te_TongHopSTTND.Text.HasValue())
                {
                    JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                }
                JsonGhiChu.CodeDanhMucCongTac = code;
                var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                dtRow["GhiChuBoSungJson"] = encryptedStr;
                dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaDuThau;
                dtRow["Modified"] = true;
                dtRow["PhatSinh"] = false;
                dtRow_congtactheogiadoan["Modified"] = true;
                dtRow_congtactheogiadoan["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                dtRow_congtactheogiadoan["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                dt.Rows.Add(dtRow);
                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                return new KeyValuePair<long, long>(SortId, SortIdNhom);
            }
            else if (!string.IsNullOrEmpty(Fomula))
            {
                string FormulaSheet = Fomula;
                if (FormulaSheet.Contains('/'))
                {
                    string[] Arr = FormulaSheet.Split('/');
                    FormulaSheet = Arr[0];
                }
                do
                {
                    string FormulaCheck = Regex.Replace(FormulaSheet, $@"\D", string.Empty);
                    FormulaSheet = LapGia.Rows[int.Parse(FormulaCheck)-1][txt_dongiaduthau.Text].Formula;
                    if (FormulaSheet == "=F58")
                    {
                        FormulaSheet = LapGia.Rows[int.Parse(FormulaCheck) - 1]["F"].Formula;
                        goto Label;
                    }
                }
                while (!FormulaSheet.Contains(cboCTtenSheet.Text)&& !string.IsNullOrEmpty(FormulaSheet) && !FormulaSheet.Contains(cboHPVTtenSheet.Text));
                Label:
                Fomula = FormulaSheet;
                if (Fomula.Contains(cboCTtenSheet.Text))
                {
                    //int index = Fomula.IndexOf(cboCTtenSheet.Text);
                    int RowIndexCT = 0;
                    string TenCotDonGia = string.Empty;
                    MatchCollection Multimatch = Select.Matches(Fomula);
                    //Fomula = Fomula.Remove(0, index);
                    //Fomula = Regex.Replace(Fomula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                    //Row CrowCongTrinh = CongTrinh.Rows[int.Parse(Fomula)];
                    RowIndexCT = int.Parse(Multimatch[0].Groups["ViTri"].ToString());
                    if (RowIndexCT == 75)
                    {
                        string tennhom = Crow[txt_tencongtac_HD.Text].Value.ToString();
                        DataRow NewRowNhom = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        NewRowNhom["Code"] = CodeNhomCT;
                        NewRowNhom["CodeHangMuc"] = codehangmuc;
                        NewRowNhom["Ten"] = tennhom;
                        NewRowNhom["CodePhanTuyen"] = CodeTuyen != default ? CodeTuyen : null;
                        if (DonGiaDuThau > 0)
                            NewRowNhom["DonGia"] = DonGiaDuThau;
                        NewRowNhom["DonVi"] = DonVi;
                        if (KLHaiDang > 0)
                            NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLHaiDang;
                        NewRowNhom["SortId"] = SortIdNhom++;
                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = STT;
                        if (te_TongHopSTTND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                        dt_NhomCT.Rows.Add(NewRowNhom);
                        Row CrowCTCon;
                        for (int i = RowIndexCT; i <= RowIndexCT + 1; i++)
                        {
                            CrowCTCon = CongTrinh.Rows[i];
                            string TTCon = CrowCTCon["N"].Formula;
                            double.TryParse(CrowCTCon["K"].Value.ToString(), out double KLCongTacCon);
                            string tencongtac = CrowCTCon[txtCTTenCongTac.Text].Value.ToString();
                            string MaHieu = CrowCTCon[txtCTMaDuToan.Text].Value.ToString();
                            string code = Guid.NewGuid().ToString();
                            DataRow dtRow = dt.NewRow();
                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                            dtRow["Code"] = code;
                            codecongtactheogiaidoan = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCTCon[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                            dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                                = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCongTacCon;
                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                            dtRow["CodeHangMuc"] = codehangmuc;
                            dtRow["MaHieuCongTac"] = MaHieu;
                            dtRow["TenCongTac"] = tencongtac;
                            dtRow["DonVi"] = CrowCTCon[txtCTDonVi.Text].Value.ToString();
                            if (CodeTuyen != default)
                                dtRow["CodePhanTuyen"] = CodeTuyen;
                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                            if (txtCTDGVatLieu.Text.HasValue())
                            {
                                long.TryParse(CrowCTCon[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                            }

                            if (txtCTDGNhanCong.Text.HasValue())
                            {
                                long.TryParse(CrowCTCon[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                            }

                            if (txtCTDGMay.Text.HasValue())
                            {
                                long.TryParse(CrowCTCon[txtCTDGMay.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                            }
                            if (txtCTKhoiLuongDocVao.Text.HasValue())
                            {
                                double.TryParse(CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                if (KLDV > 0)
                                    dtRow["KhoiLuongDocVao"] = KLDV;
                                else if (!CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                    dtRow["KhoiLuongDocVao"] = 0;
                            }
                            TDKH_GhiChuBoSungJson JsonGhiChuCon = new TDKH_GhiChuBoSungJson();
                            JsonGhiChuCon.STT = CrowCTCon[txt_congtrinh_stt.Text].Value.ToString(); ;
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChuCon.STTND = CrowCTCon[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChuCon.CodeDanhMucCongTac = code;
                            var encryptedStrCon = JsonConvert.SerializeObject(JsonGhiChuCon);
                            dtRow["GhiChuBoSungJson"] = encryptedStrCon;
                            string Formula = Regex.Replace(CrowCTCon[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);
                            double DonGiaCon = 0;
                            if (TTCon.Contains("+"))
                            {
                                double.TryParse(CrowCTCon["AF"].Value.ToString(), out double DonGia1);
                                double.TryParse(CrowCTCon["AG"].Value.ToString(), out double DonGia2);
                                DonGiaCon = (DonGia1 + DonGia2) / 2;
                                fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, string.Empty, true);
                            }
                            else
                            {
                                TTCon = TTCon.Replace("=", string.Empty);
                                TTCon = Regex.Replace(TTCon, $@"\d", string.Empty);
                                string ColDonGiaCTac = MyConstant.Dic_DonGia[TTCon];
                                string FomulaDonGia = CrowCTCon[ColDonGiaCTac].Formula;
                                string ColDonGiaVatLieu = string.Empty;
                                double.TryParse(CrowCTCon[ColDonGiaCTac].Value.ToString(), out DonGiaCon);
                                foreach (var col in MyConstant.Dic_DonGiaVatTu)
                                {
                                    string CongThuc = string.Format(col.Key, RowIndexCT, RowIndexCT);
                                    if (FomulaDonGia.Contains(CongThuc))
                                        ColDonGiaVatLieu = col.Value;
                                }
                                if (!string.IsNullOrEmpty(ColDonGiaVatLieu))
                                {
                                    fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, ColDonGiaVatLieu);

                                }
                            }
                            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCon;
                            dtRow["Modified"] = true;
                            dtRow["PhatSinh"] = false;
                            dtRow_congtactheogiadoan["Modified"] = true;
                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCTCon[te_KHBegin.Text].Value.IsDateTime ? CrowCTCon[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCTCon[te_KHEnd.Text].Value.IsDateTime ? CrowCTCon[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dt.Rows.Add(dtRow);
                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                        }
                        return new KeyValuePair<long, long>(SortId, SortIdNhom);
                    }
                    TenCotDonGia = Multimatch[0].Groups["TenCot"].ToString();
                    if (TenCotDonGia == "M")
                        TenCotDonGia = "L";
                    if (TenCotDonGia == "I")
                        TenCotDonGia = "H";
                    Row CrowCongTrinh = CongTrinh.Rows[RowIndexCT - 1];
                    int IndexTenCotDG = CongTrinh.Columns[TenCotDonGia].Index;
                    string TT = CrowCongTrinh[IndexTenCotDG + 2].Formula;
                    string TTTHop = CrowCongTrinh[IndexTenCotDG + 1].Formula;
                    double DonGia = 0;
                    if (string.IsNullOrEmpty(TTTHop))
                    {
                        string tencongtac = Crow[txt_tencongtac_HD.Text].Value.ToString();
                        string MaHieu = CrowCongTrinh[txtCTMaDuToan.Text].Value.ToString();
                        string code = Guid.NewGuid().ToString();
                        DataRow dtRow = dt.NewRow();
                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                        dtRow["Code"] = code;
                        codecongtactheogiaidoan = Guid.NewGuid().ToString();
                        dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                        dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCongTrinh[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                        dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                            = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLHaiDang;
                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                        dtRow["CodeHangMuc"] = codehangmuc;
                        dtRow["MaHieuCongTac"] = MaHieu;
                        dtRow["TenCongTac"] = tencongtac;
                        dtRow["DonVi"] = DonVi;
                        if (CodeTuyen != default)
                            dtRow["CodePhanTuyen"] = CodeTuyen;
                        if (txtCTDGVatLieu.Text.HasValue())
                        {
                            long.TryParse(CrowCongTrinh[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                            dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                        }

                        if (txtCTDGNhanCong.Text.HasValue())
                        {
                            long.TryParse(CrowCongTrinh[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                            dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                        }

                        if (txtCTDGMay.Text.HasValue())
                        {
                            long.TryParse(CrowCongTrinh[txtCTDGMay.Text].Value.ToString(), out long dg);
                            dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                        }
                        if (txtCTKhoiLuongDocVao.Text.HasValue())
                        {
                            double.TryParse(CrowCongTrinh[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                            if (KLDV > 0)
                                dtRow["KhoiLuongDocVao"] = KLDV;
                            else if (!CrowCongTrinh[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                dtRow["KhoiLuongDocVao"] = 0;
                        }
                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = STT;
                        if (te_TongHopSTTND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = code;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        dtRow["GhiChuBoSungJson"] = encryptedStr;
                        dtRow["Modified"] = true;
                        dtRow["PhatSinh"] = false;
                        dtRow_congtactheogiadoan["Modified"] = true;
                        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCongTrinh[te_KHBegin.Text].Value.IsDateTime ? CrowCongTrinh[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCongTrinh[te_KHEnd.Text].Value.IsDateTime ? CrowCongTrinh[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        string Formula = Regex.Replace(CrowCongTrinh[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);
                        if (TT.Contains("+"))
                        {
                            //double.TryParse(CrowCongTrinh["AF"].Value.ToString(), out double DonGia1);
                            //double.TryParse(CrowCongTrinh["AG"].Value.ToString(), out double DonGia2);
                            //DonGia = (DonGia1 + DonGia2) / 2;
                            fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, string.Empty, true);
                        }
                        else
                        {
                            TT = TT.Replace("=", string.Empty);
                            TT = Regex.Replace(TT, $@"\d", string.Empty);
                            string ColDonGiaCTac = MyConstant.Dic_DonGia[TT];
                            //double.TryParse(CrowCongTrinh[ColDonGiaCTac].Value.ToString(), out DonGia);
                            string FomulaDonGia = CrowCongTrinh[ColDonGiaCTac].Formula;
                            string ColDonGiaVatLieu = string.Empty;
                            foreach (var item in MyConstant.Dic_DonGiaVatTu)
                            {
                                string CongThuc = string.Format(item.Key, RowIndexCT, RowIndexCT);
                                if (FomulaDonGia.Contains(CongThuc))
                                    ColDonGiaVatLieu = item.Value;
                            }
                            if (string.IsNullOrEmpty(ColDonGiaVatLieu))
                                return new KeyValuePair<long, long>(SortId, SortIdNhom);
                            fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, ColDonGiaVatLieu);
                        }
                        dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
           = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaDuThau;
                        dt.Rows.Add(dtRow);
                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                        return new KeyValuePair<long, long>(SortId, SortIdNhom);
                    }
                    else
                    {
                        //double.TryParse(CrowCongTrinh[IndexTenCotDG - 1].Value.ToString(), out double KLCongTac);
                        double TyLe = KLMoiThau == 0 ? 0 : KLHaiDang / KLMoiThau;
                        string tennhom = Crow[txt_tencongtac_HD.Text].Value.ToString();
                        DataRow NewRowNhom = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        NewRowNhom["Code"] = CodeNhomCT;
                        NewRowNhom["CodeHangMuc"] = codehangmuc;
                        NewRowNhom["Ten"] = tennhom;
                        NewRowNhom["CodePhanTuyen"] = CodeTuyen != default ? CodeTuyen : null;
                        if (DonGiaDuThau > 0)
                            NewRowNhom["DonGia"] = DonGiaDuThau;
                        NewRowNhom["DonVi"] = DonVi;
                        if (KLHaiDang > 0)
                            NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLHaiDang;
                        NewRowNhom["SortId"] = SortIdNhom++;
                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = STT;
                        if (te_TongHopSTTND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                        dt_NhomCT.Rows.Add(NewRowNhom);
                        Row CrowCTCon;
                        if (TTTHop.Contains("SUM"))
                        {
                            string TenCotKL = CongTrinh.Columns[IndexTenCotDG - 1].Heading;
                            Select = new Regex($@"\D+{TenCotKL}(?<ViTri1>\d+):{TenCotKL}(?<ViTri2>\d+)\D+");
                            Multimatch = Select.Matches(TTTHop);
                            int ViTri1 = int.Parse(Multimatch[0].Groups["ViTri1"].ToString());
                            int ViTri2 = int.Parse(Multimatch[0].Groups["ViTri2"].ToString());
                            for (int i = ViTri1 - 1; i <= ViTri2 - 1; i++)
                            {
                                CrowCTCon = CongTrinh.Rows[i];
                                TT = CrowCTCon[IndexTenCotDG + 2].Formula;
                                double.TryParse(CrowCTCon[IndexTenCotDG - 1].Value.ToString(), out double KLCongTacCon);
                                string tencongtac = CrowCTCon[txtCTTenCongTac.Text].Value.ToString();
                                string MaHieu = CrowCTCon[txtCTMaDuToan.Text].Value.ToString();
                                string code = Guid.NewGuid().ToString();
                                DataRow dtRow = dt.NewRow();
                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                dtRow["Code"] = code;
                                codecongtactheogiaidoan = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                dtRow_congtactheogiadoan["SortId"] = SortId++;
                                dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCTCon[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                                    = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = Math.Round(TyLe * KLCongTacCon, 4);
                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                dtRow["CodeHangMuc"] = codehangmuc;
                                dtRow["MaHieuCongTac"] = MaHieu;
                                dtRow["TenCongTac"] = tencongtac;
                                dtRow["DonVi"] = CrowCTCon[txtCTDonVi.Text].Value.ToString();
                                if (CodeTuyen != default)
                                    dtRow["CodePhanTuyen"] = CodeTuyen;
                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                dtRow_congtactheogiadoan["SortId"] = SortId++;
                                if (txtCTDGVatLieu.Text.HasValue())
                                {
                                    long.TryParse(CrowCTCon[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                }

                                if (txtCTDGNhanCong.Text.HasValue())
                                {
                                    long.TryParse(CrowCTCon[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                }

                                if (txtCTDGMay.Text.HasValue())
                                {
                                    long.TryParse(CrowCTCon[txtCTDGMay.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                }
                                if (txtCTKhoiLuongDocVao.Text.HasValue())
                                {
                                    double.TryParse(CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                    if (KLDV > 0)
                                        dtRow["KhoiLuongDocVao"] = KLDV;
                                    else if (!CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                        dtRow["KhoiLuongDocVao"] = 0;
                                }
                                TDKH_GhiChuBoSungJson JsonGhiChuCon = new TDKH_GhiChuBoSungJson();
                                JsonGhiChuCon.STT = CrowCTCon[txt_congtrinh_stt.Text].Value.ToString(); ;
                                if (txt_congtrinh_sttND.Text.HasValue())
                                {
                                    JsonGhiChuCon.STTND = CrowCTCon[txt_congtrinh_sttND.Text].Value.ToString();
                                }
                                JsonGhiChuCon.CodeDanhMucCongTac = code;
                                var encryptedStrCon = JsonConvert.SerializeObject(JsonGhiChuCon);
                                dtRow["GhiChuBoSungJson"] = encryptedStrCon;
                                string Formula = Regex.Replace(CrowCTCon[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);

                                if (TT.Contains("+"))
                                {
                                    double.TryParse(CrowCTCon["AF"].Value.ToString(), out double DonGia1);
                                    double.TryParse(CrowCTCon["AG"].Value.ToString(), out double DonGia2);
                                    DonGia = (DonGia1 + DonGia2) / 2;
                                    fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, string.Empty, true);
                                }
                                else
                                {
                                    TT = TT.Replace("=", string.Empty);
                                    TT = Regex.Replace(TT, $@"\d", string.Empty);
                                    string ColDonGiaCTac = MyConstant.Dic_DonGia[TT];
                                    string FomulaDonGia = CrowCTCon[ColDonGiaCTac].Formula;
                                    string ColDonGiaVatLieu = string.Empty;
                                    double.TryParse(CrowCTCon[ColDonGiaCTac].Value.ToString(), out DonGia);
                                    foreach (var col in MyConstant.Dic_DonGiaVatTu)
                                    {
                                        string CongThuc = string.Format(col.Key, RowIndexCT, RowIndexCT);
                                        if (FomulaDonGia.Contains(CongThuc))
                                            ColDonGiaVatLieu = col.Value;
                                    }
                                    if (!string.IsNullOrEmpty(ColDonGiaVatLieu))
                                    {
                                        fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, ColDonGiaVatLieu);

                                    }
                                }
                                dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                                dtRow["Modified"] = true;
                                dtRow["PhatSinh"] = false;
                                dtRow_congtactheogiadoan["Modified"] = true;
                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCTCon[te_KHBegin.Text].Value.IsDateTime ? CrowCTCon[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCTCon[te_KHEnd.Text].Value.IsDateTime ? CrowCTCon[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dt.Rows.Add(dtRow);
                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                            }
                        }
                        else
                        {
                            string[] lstCol = TTTHop.Replace("=", string.Empty).Split('+');
                            foreach (var item in lstCol)
                            {
                                RowIndexCT = int.Parse(Regex.Replace(item, $@"\D", string.Empty));
                                CrowCTCon = CongTrinh.Rows[RowIndexCT - 1];
                                TT = CrowCTCon[IndexTenCotDG + 2].Formula;
                                double.TryParse(CrowCTCon[IndexTenCotDG - 1].Value.ToString(), out double KLCongTacCon);
                                string tencongtac = CrowCTCon[txtCTTenCongTac.Text].Value.ToString();
                                string MaHieu = CrowCTCon[txtCTMaDuToan.Text].Value.ToString();
                                string code = Guid.NewGuid().ToString();
                                DataRow dtRow = dt.NewRow();
                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                dtRow["Code"] = code;
                                codecongtactheogiaidoan = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["Code"] = codecongtactheogiaidoan;
                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                dtRow_congtactheogiadoan["SortId"] = SortId++;
                                dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? CrowCTCon[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                dtRow_congtactheogiadoan["KhoiLuongToanBo"]
                                    = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = Math.Round(TyLe * KLCongTacCon, 4);
                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                dtRow["CodeHangMuc"] = codehangmuc;
                                dtRow["MaHieuCongTac"] = MaHieu;
                                dtRow["TenCongTac"] = tencongtac;
                                dtRow["DonVi"] = CrowCTCon[txtCTDonVi.Text].Value.ToString();
                                if (CodeTuyen != default)
                                    dtRow["CodePhanTuyen"] = CodeTuyen;
                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                dtRow_congtactheogiadoan["SortId"] = SortId++;
                                if (txtCTDGVatLieu.Text.HasValue())
                                {
                                    long.TryParse(CrowCTCon[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                }

                                if (txtCTDGNhanCong.Text.HasValue())
                                {
                                    long.TryParse(CrowCTCon[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                }

                                if (txtCTDGMay.Text.HasValue())
                                {
                                    long.TryParse(CrowCTCon[txtCTDGMay.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                }
                                if (txtCTKhoiLuongDocVao.Text.HasValue())
                                {
                                    double.TryParse(CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                    if (KLDV > 0)
                                        dtRow["KhoiLuongDocVao"] = KLDV;
                                    else if (!CrowCTCon[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                                        dtRow["KhoiLuongDocVao"] = 0;
                                }
                                TDKH_GhiChuBoSungJson JsonGhiChuCon = new TDKH_GhiChuBoSungJson();
                                JsonGhiChuCon.STT = CrowCTCon[txt_congtrinh_stt.Text].Value.ToString(); ;
                                if (txt_congtrinh_sttND.Text.HasValue())
                                {
                                    JsonGhiChuCon.STTND = CrowCTCon[txt_congtrinh_sttND.Text].Value.ToString();
                                }
                                JsonGhiChuCon.CodeDanhMucCongTac = code;
                                var encryptedStrCon = JsonConvert.SerializeObject(JsonGhiChuCon);
                                dtRow["GhiChuBoSungJson"] = encryptedStrCon;
                                string Formula = Regex.Replace(CrowCTCon[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);

                                if (TT.Contains("+"))
                                {
                                    double.TryParse(CrowCTCon["AF"].Value.ToString(), out double DonGia1);
                                    double.TryParse(CrowCTCon["AG"].Value.ToString(), out double DonGia2);
                                    DonGia = (DonGia1 + DonGia2) / 2;
                                    fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, string.Empty, true);
                                }
                                else
                                {
                                    TT = TT.Replace("=", string.Empty);
                                    TT = Regex.Replace(TT, $@"\d", string.Empty);
                                    string ColDonGiaCTac = MyConstant.Dic_DonGia[TT];
                                    string FomulaDonGia = CrowCTCon[ColDonGiaCTac].Formula;
                                    string ColDonGiaVatLieu = string.Empty;
                                    double.TryParse(CrowCTCon[ColDonGiaCTac].Value.ToString(), out DonGia);
                                    foreach (var col in MyConstant.Dic_DonGiaVatTu)
                                    {
                                        string CongThuc = string.Format(col.Key, RowIndexCT, RowIndexCT);
                                        if (FomulaDonGia.Contains(CongThuc))
                                            ColDonGiaVatLieu = col.Value;
                                    }
                                    if (!string.IsNullOrEmpty(ColDonGiaVatLieu))
                                    {
                                        fcn_updateHaophiKienGiangCanTho(cboHPVTtenSheet.Text, dtHaoPhi, int.Parse(Formula), codecongtactheogiaidoan, ColDonGiaVatLieu);

                                    }
                                }
                                dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
                                dtRow["Modified"] = true;
                                dtRow["PhatSinh"] = false;
                                dtRow_congtactheogiadoan["Modified"] = true;
                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (CrowCTCon[te_KHBegin.Text].Value.IsDateTime ? CrowCTCon[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (CrowCTCon[te_KHEnd.Text].Value.IsDateTime ? CrowCTCon[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dt.Rows.Add(dtRow);
                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                            }
                        }
                    }
                }
                else if (Fomula.Contains("4.CPXD-PA2"))
                {
                    string tennhom = Crow[txt_tencongtac_HD.Text].Value.ToString();
                    DataRow NewRowNhom = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRowNhom["Code"] = CodeNhomCT;
                    NewRowNhom["CodeHangMuc"] = codehangmuc;
                    NewRowNhom["Ten"] = tennhom;
                    NewRowNhom["CodePhanTuyen"] = CodeTuyen != default ? CodeTuyen : null;
                    if (DonGiaDuThau > 0)
                        NewRowNhom["DonGia"] = DonGiaDuThau;
                    NewRowNhom["DonVi"] = DonVi;
                    if (KLHaiDang > 0)
                        NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLHaiDang;
                    NewRowNhom["SortId"] = SortIdNhom++;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = STT;
                    if (te_TongHopSTTND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                    dt_NhomCT.Rows.Add(NewRowNhom);

                    Select = new Regex($@"\D+4.CPXD-PA2\D\D\D(?<TenCot>\D)\D(?<ViTri>\d+)");
                    int RowIndexCT = 0;
                    string TenCotDonGia = string.Empty;
                    MatchCollection Multimatch = Select.Matches(Fomula);
                    RowIndexCT = int.Parse(Multimatch[0].Groups["ViTri"].ToString());
                    TenCotDonGia = Multimatch[0].Groups["TenCot"].ToString();
                    Worksheet wsHangMuc = CongTrinh.Workbook.Worksheets["4.CPXD-PA2"];
                    Row HangMuc = wsHangMuc.Rows[RowIndexCT - 1];
                    Select = new Regex($@"\D+{TenCotDonGia}(?<ViTri1>\d+):{TenCotDonGia}(?<ViTri2>\d+)\D+");
                    Fomula = HangMuc[TenCotDonGia].Formula;
                    MatchCollection MultimatchHM = Select.Matches(Fomula);
                    int ViTri1 = int.Parse(MultimatchHM[0].Groups["ViTri1"].ToString());
                    int ViTri2 = int.Parse(MultimatchHM[0].Groups["ViTri2"].ToString());
                    for (int i = ViTri1 - 1; i <= ViTri2 - 1; i++)
                    {
                        HangMuc = wsHangMuc.Rows[i];
                        string tencongtac = HangMuc["C"].Value.ToString();
                        string MaHieu = "TT";
                        string code = Guid.NewGuid().ToString();
                        DataRow dtRow = dt.NewRow();
                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                        dtRow["Code"] = code;
                        dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                        //dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                        double.TryParse(HangMuc["K"].Value.ToString(), out double KLTT);
                        dtRow_congtactheogiadoan["KhoiLuongToanBo"] =
                            dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLTT;
                        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                        dtRow["CodeHangMuc"] = codehangmuc;
                        dtRow["MaHieuCongTac"] = MaHieu;
                        dtRow["TenCongTac"] = tencongtac;
                        dtRow["DonVi"] = "trọn gói";
                        dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
               = dtRow_congtactheogiadoan["DonGiaDuThau"] = 1;
                        dtRow["Modified"] = true;
                        dtRow["PhatSinh"] = false;
                        dtRow_congtactheogiadoan["Modified"] = true;
                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                        dtRow_congtactheogiadoan["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtRow_congtactheogiadoan["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        TDKH_GhiChuBoSungJson JsonGhiChuCon = new TDKH_GhiChuBoSungJson();
                        JsonGhiChuCon.STT = HangMuc["A"].Value.ToString(); ;
                        JsonGhiChuCon.CodeDanhMucCongTac = code;
                        var encryptedStrCon = JsonConvert.SerializeObject(JsonGhiChuCon);
                        dtRow["GhiChuBoSungJson"] = encryptedStrCon;
                        dt.Rows.Add(dtRow);
                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                    }
                }
                else if (string.IsNullOrEmpty(Fomula) || Fomula.Contains(cboHPVTtenSheet.Text))
                {
                    //if (string.IsNullOrEmpty(Fomula) || Fomula.Contains(cboHPVTtenSheet.Text))
                    string tencongtac = Crow[txt_tencongtac_HD.Text].Value.ToString();
                    string MaHieu = "TT";
                    string code = Guid.NewGuid().ToString();
                    DataRow dtRow = dt.NewRow();
                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                    dtRow["Code"] = code;
                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                    dtRow_congtactheogiadoan["SortId"] = SortId++;
                    //dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] =
                        dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLHaiDang;
                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                    dtRow_congtactheogiadoan["SortId"] = SortId++;
                    dtRow["CodeHangMuc"] = codehangmuc;
                    dtRow["MaHieuCongTac"] = MaHieu;
                    dtRow["TenCongTac"] = tencongtac;
                    dtRow["DonVi"] = DonVi;
                    if (CodeTuyen != default)
                        dtRow["CodePhanTuyen"] = CodeTuyen;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = STT;
                    if (te_TongHopSTTND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = PhanBo[te_TongHopSTTND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = code;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    dtRow["GhiChuBoSungJson"] = encryptedStr;
                    dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
           = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaDuThau;
                    dtRow["Modified"] = true;
                    dtRow["PhatSinh"] = false;
                    dtRow_congtactheogiadoan["Modified"] = true;
                    dtRow_congtactheogiadoan["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dtRow_congtactheogiadoan["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dt.Rows.Add(dtRow);
                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                    return new KeyValuePair<long, long>(SortId, SortIdNhom);
                }
            }
            return new KeyValuePair<long, long>(SortId, SortIdNhom);
        }
        private void fcn_truyendataExxcel_MauTruongSon2(int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            CellRange usedRange = workbook.Worksheets[cboHPVTtenSheet.Text].GetUsedRange();
            int lastline_vl = usedRange.RowCount;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            string DonVi = string.Empty, codetheogiaidoan = string.Empty, mahieu = string.Empty, TenCongTac = string.Empty;
            string VL = string.Empty, NC = string.Empty, MTC = string.Empty;
            int STT = 0;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            Worksheet ws = workbook.Worksheets[cbe_TongHop.Text];
            Worksheet wsTH = workbook.Worksheets[cbb_sheet.Text];
            int LastLine = wsTH.GetUsedRange().BottomRowIndex;
            string[] lstSheet = new string[]
            {
                "DTCT.duong","DTCT.DBGT","DTCT.d.ngang","DTCT.NGL.Thanh","DTCT.NGsbay","DTCT.gom","DTCT.DCV","DTCT.cauban","DTCT.caucacloai","DTCT","CauLD"
            };
            int IndexCT = rg_LayDuLieu.SelectedIndex;
            if (IndexCT > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            for (int i = firstLine; i <= lastLine; i++)
            {
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                TenCongTac = crRow[te_TongHopTenCT.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{mahieu}_{TenCongTac}");
                if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (IndexCT > 0)
                        continue;
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    if (string.IsNullOrEmpty(codecongtrinh))
                    {
                        codecongtrinh = Guid.NewGuid().ToString();
                        tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                    }
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == te_PhanTuyen.Text)
                {
                    if (string.IsNullOrEmpty(codehangmuc))
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    }
                    string TenTuyen = crRow[te_TongHopTenCT.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        crRow = ws.Rows[j];
                        mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                        if (mahieu == te_PhanTuyen.Text || mahieu.ToUpper() == "THM" || mahieu == te_EndTuyen.Text || mahieu == "" || mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        if (!int.TryParse(crRow[te_TongHopMaHieu.Text].Value.ToString(), out int STTNhom))
                            continue;
                        if (string.IsNullOrEmpty(codehangmuc))
                        {
                            if (string.IsNullOrEmpty(codecongtrinh))
                            {
                                codecongtrinh = Guid.NewGuid().ToString();
                                tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                                queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                            }
                            codehangmuc = Guid.NewGuid().ToString();
                            tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                        }
                        string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                        //double STTNhom = crRow[te_TongHopMaHieu.Text].Value.NumericValue;
                        DataRow NewRowNhom = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        //double KLTruongSon = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                        //double DonGia = crRow[te_TongHopDonGia.Text].Value.NumericValue;
                        //double KLHD = crRow["D"].Value.NumericValue;
                        double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTruongSon);
                        double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGia);
                        double.TryParse(crRow["D"].Value.ToString(), out double KLHD);
                        NewRowNhom["Code"] = CodeNhomCT;
                        NewRowNhom["CodeHangMuc"] = codehangmuc;
                        NewRowNhom["Ten"] = tennhom;
                        NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                        if (DonGia > 0)
                            NewRowNhom["DonGia"] = DonGia;
                        NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                        if (KLTruongSon > 0)
                            NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                        NewRowNhom["SortId"] = SortIdNhom++;
                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                        if (te_TongHopSTTND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                        dt_NhomCT.Rows.Add(NewRowNhom);
                        int RowNhom = 0;
                        double KLTong = 0;
                        for (long m = 9; m < LastLine; m++)
                        {
                            Row Crow = wsTH.Rows[(int)m];
                            if (!Crow.Visible)
                                continue;
                            string TenNhom = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                            if (TenNhom != tennhom)
                                continue;
                            double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaNhom);
                            double.TryParse(Crow[txt_klduthau.Text].Value.ToString(), out KLTong);
                            if (DonGiaNhom == DonGia || KLHD == KLTong)
                            {
                                RowNhom = (int)m;
                                break;
                            }

                        }
                        if (RowNhom == 0)
                        {
                            if (KLTruongSon > 0)
                            {
                                DataRow dtRow = dt.NewRow();
                                DataRow dtRowDLG = dt_DLG.NewRow();
                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                string code = Guid.NewGuid().ToString();
                                dtRow["Code"] = code;
                                dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                dtRow_congtactheogiadoan["SortId"] = SortId;
                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tennhom;
                                dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                                double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaCongTac);
                                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                dtRow["Modified"] = true;
                                dtRow["CodePhanTuyen"] = CodeTuyen;
                                dtRow["PhatSinh"] = false;
                                dtRow_congtactheogiadoan["Modified"] = true;
                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                dt.Rows.Add(dtRow);
                                dt_DLG.Rows.Add(dtRowDLG);
                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                SortId++;
                            }
                            else
                                continue;
                        }
                        else
                        {
                            for (int k = RowNhom + 1; k <= LastLine; k++)
                            {
                                Row Crow = wsTH.Rows[k];
                                if (!Crow[txt_STT.Text].Value.IsEmpty)
                                {
                                    if (Crow[txt_klduthau.Text].Value.NumericValue > 0)
                                        break;
                                    else
                                        continue;
                                }
                                else
                                {
                                    double.TryParse(Crow[txt_klduthau.Text].Value.ToString(), out double KLTongCheck);
                                    if (KLTongCheck > 0)
                                        break;
                                    double.TryParse(Crow[txt_klduthauCT.Text].Value.ToString(), out double KLChiTiet);
                                    if (Crow[txt_macongtac.Text].Value.IsEmpty)
                                    {
                                        if (KLChiTiet > 0 && !Crow["B"].Value.IsEmpty)
                                        {
                                            double KLCTacConNew = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                            DataRow dtRow = dt.NewRow();
                                            DataRow dtRowDLG = dt_DLG.NewRow();
                                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                            string code = Guid.NewGuid().ToString();
                                            dtRow["Code"] = code;
                                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                            dtRow_congtactheogiadoan["SortId"] = SortId;
                                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacConNew;
                                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue; ;
                                            dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                            double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                            dtRow_congtactheogiadoan["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow_congtactheogiadoan["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow["Modified"] = true;
                                            dtRow["PhatSinh"] = false;
                                            dtRow_congtactheogiadoan["Modified"] = true;
                                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                            dtRow["CodePhanTuyen"] = CodeTuyen;
                                            dt.Rows.Add(dtRow);
                                            dt_DLG.Rows.Add(dtRowDLG);
                                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                            SortId++;
                                            string codevatu = Guid.NewGuid().ToString();
                                            DataRow dt_vattu = dtHaoPhi.NewRow();
                                            dt_vattu["Code"] = codevatu;
                                            dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                            dt_vattu["MaVatLieu"] = "TT";
                                            dt_vattu["VatTu"] = Crow[txt_tencongtac_HD.Text].Value.TextValue; ;
                                            dt_vattu["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue; ;
                                            dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = 0;
                                            dt_vattu["LoaiVatTu"] = "Vật liệu";
                                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                            dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                            dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtHaoPhi.Rows.Add(dt_vattu);
                                        }
                                        continue;
                                    }
                                    string Fomular = Crow["B"].Formula;
                                    string NameSheetCon = string.Empty;
                                    if (string.IsNullOrEmpty(Fomular))
                                    {
                                        Fomular = Crow["C"].Formula;
                                        if (string.IsNullOrEmpty(Fomular))
                                        {
                                            double KLCTacConNew = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                            DataRow dtRow = dt.NewRow();
                                            DataRow dtRowDLG = dt_DLG.NewRow();
                                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                            string code = Guid.NewGuid().ToString();
                                            dtRow["Code"] = code;
                                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                            dtRow_congtactheogiadoan["SortId"] = SortId;
                                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacConNew;
                                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = Crow[txt_macongtac.Text].Value.TextValue;
                                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                            dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                            double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                            dtRow_congtactheogiadoan["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow_congtactheogiadoan["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow["Modified"] = true;
                                            dtRow["PhatSinh"] = false;
                                            dtRow_congtactheogiadoan["Modified"] = true;
                                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                            dtRow["CodePhanTuyen"] = CodeTuyen;
                                            dt.Rows.Add(dtRow);
                                            dt_DLG.Rows.Add(dtRowDLG);
                                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                            SortId++;
                                            continue;
                                        }
                                        else
                                        {
                                            int index = Fomular.IndexOf("Giá tổng hợp");
                                            if (index > 0)
                                            {
                                                Fomular = Fomular.Remove(0, index);
                                                Fomular = Regex.Replace(Fomular, $@"Giá tổng hợp|\D", string.Empty);
                                                NameSheetCon = "Giá tổng hợp";
                                            }
                                        }

                                    }
                                    else
                                    {
                                        foreach (string item in lstSheet)
                                        {
                                            int index = Fomular.IndexOf(item);
                                            if (index > 0)
                                            {
                                                Fomular = Fomular.Remove(0, index);
                                                Fomular = Regex.Replace(Fomular, $@"{item}|\D", string.Empty);
                                                NameSheetCon = item;
                                                break;
                                            }
                                        }
                                    }
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    Worksheet wsCon = workbook.Worksheets[NameSheetCon];
                                    double KLCTacCon = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                    Fcn_UpdateCongTacTruongSon2(KLCTacCon, SortId++, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, wsCon.Rows[int.Parse(Fomular) - 1], CodeNhomCT, CodeTuyen);
                                }
                            }
                        }
                    }

                }
                else
                {
                    if (!int.TryParse(crRow[te_TongHopMaHieu.Text].Value.ToString(), out int STTNhom))
                        continue;
                    if (string.IsNullOrEmpty(codehangmuc))
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    }
                    string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                    //double STTNhom = crRow[te_TongHopMaHieu.Text].Value.NumericValue;
                    DataRow NewRowNhom = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    //double KLTruongSon = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                    //double DonGia = crRow[te_TongHopDonGia.Text].Value.NumericValue;
                    //double KLHD = crRow["D"].Value.NumericValue;
                    double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTruongSon);
                    double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGia);
                    double.TryParse(crRow["D"].Value.ToString(), out double KLHD);
                    NewRowNhom["Code"] = CodeNhomCT;
                    NewRowNhom["CodeHangMuc"] = codehangmuc;
                    NewRowNhom["Ten"] = tennhom;
                    if (DonGia > 0)
                        NewRowNhom["DonGia"] = DonGia;
                    NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                    if (KLTruongSon > 0)
                        NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                    NewRowNhom["SortId"] = SortIdNhom++;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                    if (te_TongHopSTTND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                    dt_NhomCT.Rows.Add(NewRowNhom);
                    int RowNhom = 0;
                    double KLTong = 0;
                    for (long m = 9; m < LastLine; m++)
                    {
                        Row Crow = wsTH.Rows[(int)m];
                        if (!Crow.Visible)
                            continue;
                        string TenNhom = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                        if (TenNhom != tennhom)
                            continue;
                        //double DonGiaNhom = Crow[txt_dongiaduthau.Text].Value.NumericValue;
                        double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaNhom);
                        double.TryParse(Crow[txt_klduthau.Text].Value.ToString(), out KLTong);
                        //KLTong =Crow[txt_klduthau.Text].Value.NumericValue;
                        if (DonGiaNhom == DonGia || KLHD == KLTong)
                        {
                            RowNhom = (int)m;
                            break;
                        }

                    }
                    if (RowNhom == 0)
                    {
                        if (KLTruongSon > 0)
                        {
                            DataRow dtRow = dt.NewRow();
                            DataRow dtRowDLG = dt_DLG.NewRow();
                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                            string code = Guid.NewGuid().ToString();
                            dtRow["Code"] = code;
                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                            dtRow_congtactheogiadoan["SortId"] = SortId;
                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tennhom;
                            dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                            double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaCongTac);
                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                            dtRow["Modified"] = true;
                            dtRow["PhatSinh"] = false;
                            dtRow_congtactheogiadoan["Modified"] = true;
                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                            dt.Rows.Add(dtRow);
                            dt_DLG.Rows.Add(dtRowDLG);
                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                            SortId++;
                            goto Label;
                        }
                        else
                            continue;
                    }
                    else
                    {
                        for (int k = RowNhom + 1; k <= LastLine; k++)
                        {
                            Row Crow = wsTH.Rows[k];
                            if (!Crow[txt_STT.Text].Value.IsEmpty)
                            {
                                if (Crow[txt_klduthau.Text].Value.NumericValue > 0)
                                    break;
                                else
                                    continue;
                            }
                            else
                            {
                                double.TryParse(Crow[txt_klduthau.Text].Value.ToString(), out double KLTongCheck);
                                if (KLTongCheck > 0)
                                    break;
                                double.TryParse(Crow[txt_klduthauCT.Text].Value.ToString(), out double KLChiTiet);
                                if (Crow[txt_macongtac.Text].Value.IsEmpty)
                                {
                                    if (KLChiTiet > 0 && !Crow["B"].Value.IsEmpty)
                                    {
                                        double KLCTacConNew = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                        DataRow dtRow = dt.NewRow();
                                        DataRow dtRowDLG = dt_DLG.NewRow();
                                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                        string code = Guid.NewGuid().ToString();
                                        dtRow["Code"] = code;
                                        dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                        dtRow_congtactheogiadoan["SortId"] = SortId;
                                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                        dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacConNew;
                                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                        dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                        dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                        dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue; ;
                                        dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                        double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                        dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                               = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                        dtRow_congtactheogiadoan["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow_congtactheogiadoan["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow["Modified"] = true;
                                        dtRow["PhatSinh"] = false;
                                        dtRow_congtactheogiadoan["Modified"] = true;
                                        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                        dt.Rows.Add(dtRow);
                                        dt_DLG.Rows.Add(dtRowDLG);
                                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                        SortId++;
                                        string codevatu = Guid.NewGuid().ToString();
                                        DataRow dt_vattu = dtHaoPhi.NewRow();
                                        dt_vattu["Code"] = codevatu;
                                        dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                        dt_vattu["MaVatLieu"] = "TT";
                                        dt_vattu["VatTu"] = Crow[txt_tencongtac_HD.Text].Value.TextValue; ;
                                        dt_vattu["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue; ;
                                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = 0;
                                        dt_vattu["LoaiVatTu"] = "Vật liệu";
                                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtHaoPhi.Rows.Add(dt_vattu);
                                    }
                                    continue;
                                }
                                //double KLChiTiet = Crow[txt_klduthauCT.Text].Value.NumericValue;

                                string Fomular = Crow["B"].Formula;
                                string NameSheetCon = string.Empty;
                                if (string.IsNullOrEmpty(Fomular))
                                {
                                    Fomular = Crow["C"].Formula;
                                    if (string.IsNullOrEmpty(Fomular))
                                    {
                                        double KLCTacConNew = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                        DataRow dtRow = dt.NewRow();
                                        DataRow dtRowDLG = dt_DLG.NewRow();
                                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                        string code = Guid.NewGuid().ToString();
                                        dtRow["Code"] = code;
                                        dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                        dtRow_congtactheogiadoan["SortId"] = SortId;
                                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                        dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacConNew;
                                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                        dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                        dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = Crow[txt_macongtac.Text].Value.TextValue;
                                        dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                        dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                        double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                        dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                               = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                        dtRow_congtactheogiadoan["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow_congtactheogiadoan["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow["Modified"] = true;
                                        dtRow["PhatSinh"] = false;
                                        dtRow_congtactheogiadoan["Modified"] = true;
                                        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                        dt.Rows.Add(dtRow);
                                        dt_DLG.Rows.Add(dtRowDLG);
                                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                        SortId++;
                                        continue;
                                    }
                                    else
                                    {
                                        int index = Fomular.IndexOf("Giá tổng hợp");
                                        if (index > 0)
                                        {
                                            Fomular = Fomular.Remove(0, index);
                                            Fomular = Regex.Replace(Fomular, $@"Giá tổng hợp|\D", string.Empty);
                                            NameSheetCon = "Giá tổng hợp";
                                        }
                                    }

                                }
                                else
                                {
                                    foreach (string item in lstSheet)
                                    {
                                        int index = Fomular.IndexOf(item);
                                        if (index > 0)
                                        {
                                            Fomular = Fomular.Remove(0, index);
                                            Fomular = Regex.Replace(Fomular, $@"{item}|\D", string.Empty);
                                            NameSheetCon = item;
                                            break;
                                        }
                                    }
                                }
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                Worksheet wsCon = workbook.Worksheets[NameSheetCon];
                                double KLCTacCon = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                Fcn_UpdateCongTacTruongSon2(KLCTacCon, SortId++, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, wsCon.Rows[int.Parse(Fomular) - 1], CodeNhomCT);
                            }
                        }
                    }
                }
                Label:
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }
            spsheet_XemFile.EndUpdate();
            try
            {

            }
            catch (Exception) { }

            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();

            this.Close();
        }
        private void fcn_truyendataExxcel_congtrinhF1(Dictionary<Control, string> dic, int firstLine, int lastLine, TextEdit stt)
        {
            foreach (var item in dic)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            CellRange usedRange = workbook.Worksheets[cboHPVTtenSheet.Text].GetUsedRange();
            int lastline_vl = usedRange.RowCount;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            fcn_kiemtracongtrinh_hangmuc(firstLine);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            int Index = rg_LayDuLieu.SelectedIndex;
            if (tencongtrinh == "")
            {
                firstLine = dic_congtrinh.First().Key + 2;
                fcn_kiemtracongtrinh_hangmuc(firstLine);
            }
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";
            double VL = 0, NC = 0, MTC = 0, STT = 0;
            if (Index > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            //          spsheet_XemFile.Document.History.IsEnabled = false;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                mahieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{mahieu}_{TenCongTac}");
                if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (Index > 0)
                        continue;
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    NewRow["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                    if (Te_DonGiaNhom.Text.HasValue())
                    {
                        double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                        if (DonGiaNhom > 0)
                            NewRow["DonGia"] = DonGiaNhom;
                    }
                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                    if (KLNhom > 0)
                        NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;
                    NewRow["SortId"] = SortIdNhom++;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                    if (txt_congtrinh_sttND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRow["GhiChuBoSungJson"] = encryptedStr;
                    RowDuThau = Fcn_UpdateNhomCongTac(RowDuThau, wsDonGia, crRow, crRow[txtCTKhoiLuong.Text].Value.NumericValue, tennhom, NewRow, LastLineDonGia);
                    dt_NhomCT.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                            Mahieu.ToUpper() == "THM" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                        {
                            i = j - 1;
                            break;

                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }

                }
                else if (mahieu == te_PhanTuyen.Text)
                {
                    string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);

                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_PhanTuyen.Text || Mahieu.ToUpper() == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        else if (Mahieu == te_Nhom.Text)
                        {
                            string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRowNhom = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRowNhom["Code"] = CodeNhomCT;
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
                            NewRowNhom["DonVi"] = CrowCTNhom[txtCTDonVi.Text].Value.TextValue;
                            if (Te_DonGiaNhom.Text.HasValue())
                            {
                                double.TryParse(CrowCTNhom[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                if (DonGiaNhom > 0)
                                    NewRowNhom["DonGia"] = DonGiaNhom;
                            }
                            double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                            if (KLNhom > 0)
                                NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                            NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                            NewRowNhom["SortId"] = SortIdNhom++;
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = CrowCTNhom[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                            RowDuThau = Fcn_UpdateNhomCongTac(RowDuThau, wsDonGia, CrowCTNhom, CrowCTNhom[txtCTKhoiLuong.Text].Value.NumericValue, tennhom, NewRowNhom, LastLineDonGia);
                            dt_NhomCT.Rows.Add(NewRowNhom);
                            for (int k = j + 1; k <= lastLine; k++)
                            {
                                Row CrowCTNhomNew = ws.Rows[k];
                                Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                    Mahieu.ToUpper() == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                                {
                                    j = k - 1;
                                    i = j - 1;
                                    break;

                                }
                                else
                                {
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    VL = CrowCTNhomNew[txtCTDGVatLieu.Text].Value.NumericValue;
                                    NC = CrowCTNhomNew[txtCTDGNhanCong.Text].Value.NumericValue;
                                    MTC = CrowCTNhomNew[txtCTDGMay.Text].Value.NumericValue;
                                    RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    int End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (double.TryParse(CrowCTNhomNew[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                        RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.ToString(), k, true);
                                    else
                                        RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.ToString(), k, true);
                                    if (End != 0)
                                    {
                                        j = End;
                                        k = End - 1;
                                        i = j - 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }


                }
                else if (!int.TryParse(crRow[stt.Text].Value.ToString(), out int so))
                {
                    if (crRow[txtCTMaDuToan.Text].Value.ToString().ToUpper() == "THM")
                    {
                        foreach (var item in dic_congtrinh)
                        {
                            if (i < item.Key)
                            {
                                i = item.Key + 2;
                                break;
                            }
                        }
                        string mahieunext = ws.Rows[i + 1][txtCTMaDuToan.Text].Value.ToString();
                        if (mahieunext == MyConstant.CONST_TYPE_HANGMUC)
                            fcn_kiemtracongtrinh_hangmuc(i + 2, true);
                        else
                            fcn_kiemtracongtrinh_hangmuc(i);
                        continue;
                    }
                    else if (!string.IsNullOrEmpty(mahieu))
                    {
                        string _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                        int count = _STT.Count(x => x == '.');
                        if (count == 0)
                            continue;
                        DonVi = crRow[txtCTDonVi.Text].Value.ToString();
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        VL = crRow[txtCTDGVatLieu.Text].Value.NumericValue;
                        NC = crRow[txtCTDGNhanCong.Text].Value.NumericValue;
                        MTC = crRow[txtCTDGMay.Text].Value.NumericValue;
                        //double.TryParse(_STT, out STT);
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, _STT, i, true);
                        else
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, _STT, i, true);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(mahieu))
                    {
                        DonVi = crRow[txtCTDonVi.Text].Value.ToString();
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        VL = crRow[txtCTDGVatLieu.Text].Value.NumericValue;
                        NC = crRow[txtCTDGNhanCong.Text].Value.NumericValue;
                        MTC = crRow[txtCTDGMay.Text].Value.NumericValue;
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), i, true);
                        else
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), i, true);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }

            spsheet_XemFile.EndUpdate();
            try
            {
                ////          spsheet_XemFile.Document.History.IsEnabled = true;

            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();

            this.Close();
        }
        private void fcn_truyendataExxcel_congtrinhG8(Dictionary<Control, string> dic, int firstLine, int lastLine, TextEdit stt)
        {
            foreach (var item in dic)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            CellRange usedRange = workbook.Worksheets[cboHPVTtenSheet.Text].GetUsedRange();
            int lastline_vl = usedRange.RowCount;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            fcn_kiemtracongtrinh_hangmuc(firstLine);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            bool Mutiple = txtHPVTHS_NhanCong.Text == txtHPVTHS_MTC.Text ? false : true;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            if (tencongtrinh == "")
            {
                firstLine = dic_congtrinh.First().Key + 2;
                fcn_kiemtracongtrinh_hangmuc(firstLine);
            }
            int Index = rg_LayDuLieu.SelectedIndex;
            if (Index > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";
            double VL = 0, NC = 0, MTC = 0, STT = 0;
            int End = 0;
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                mahieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{mahieu}_{TenCongTac}");
                if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (Index > 0)
                        continue;
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    NewRow["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                    if (Te_DonGiaNhom.Text.HasValue())
                    {
                        double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                        if (DonGiaNhom > 0)
                            NewRow["DonGia"] = DonGiaNhom;
                    }
                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                    if (KLNhom > 0)
                        NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;
                    NewRow["SortId"] = SortIdNhom++;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                    if (txt_congtrinh_sttND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRow["GhiChuBoSungJson"] = encryptedStr;
                    dt_NhomCT.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                            Mahieu.ToUpper() == "THM" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                        {
                            i = j - 1;
                            break;

                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
                            SortId++;
                            End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, j);
                            else
                                RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, j);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }

                }
                else if (mahieu == te_PhanTuyen.Text)
                {
                    string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);

                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_PhanTuyen.Text || Mahieu.ToUpper() == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        else if (Mahieu == te_Nhom.Text)
                        {
                            string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRowNhom = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRowNhom["Code"] = CodeNhomCT;
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
                            NewRowNhom["DonVi"] = CrowCTNhom[txtCTDonVi.Text].Value.TextValue;
                            if (Te_DonGiaNhom.Text.HasValue())
                            {
                                double.TryParse(CrowCTNhom[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                if (DonGiaNhom > 0)
                                    NewRowNhom["DonGia"] = DonGiaNhom;
                            }
                            double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                            if (KLNhom > 0)
                                NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                            NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                            NewRowNhom["SortId"] = SortIdNhom++;
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = CrowCTNhom[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                            dt_NhomCT.Rows.Add(NewRowNhom);
                            for (int k = j + 1; k <= lastLine; k++)
                            {
                                Row CrowCTNhomNew = ws.Rows[k];
                                Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                    Mahieu.ToUpper() == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                                {
                                    j = k - 1;
                                    i = j - 1;
                                    break;

                                }
                                else
                                {
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    VL = CrowCTNhomNew[txtCTDGVatLieu.Text].Value.NumericValue;
                                    NC = CrowCTNhomNew[txtCTDGNhanCong.Text].Value.NumericValue;
                                    MTC = CrowCTNhomNew[txtCTDGMay.Text].Value.NumericValue;
                                    STT = CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue;
                                    RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (double.TryParse(CrowCTNhomNew[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                        RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhomNew[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhomNew[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhomNew[txtCTDGMay.Text].Value.ToString(), CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, k);
                                    else
                                        RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhomNew[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhomNew[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhomNew[txtCTDGMay.Text].Value.ToString(), CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, k);
                                    if (End != 0)
                                    {
                                        j = End;
                                        k = End - 1;
                                        i = j - 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
                            SortId++;
                            End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(),
                                    CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, j);
                            else
                                RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(),
                                    CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, j);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }


                }
                else if (!int.TryParse(crRow[stt.Text].Value.ToString(), out int so))
                {
                    if (crRow[txtCTMaDuToan.Text].Value.ToString().ToUpper() == "THM")
                    {
                        foreach (var item in dic_congtrinh)
                        {
                            if (i < item.Key)
                            {
                                i = item.Key + 2;
                                break;
                            }
                        }
                        string mahieunext = ws.Rows[i + 1][txtCTMaDuToan.Text].Value.ToString();
                        if (mahieunext == MyConstant.CONST_TYPE_HANGMUC)
                            fcn_kiemtracongtrinh_hangmuc(i + 2, true);
                        else
                            fcn_kiemtracongtrinh_hangmuc(i);
                        continue;
                    }
                    else if (mahieu != "")
                    {
                        string _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                        int count = _STT.Count(x => x == '.');
                        if (count == 0)
                            continue;
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;

                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, i);
                        else
                            RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, i);
                        End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                else
                {
                    if (mahieu != "")
                    {
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;

                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, i);
                        else
                            RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, i);
                        End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }
            spsheet_XemFile.EndUpdate();

            try
            {

            }
            catch (Exception) { }

            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private void fcn_truyendataExxcel_congtrinhBacNam(Dictionary<Control, string> dic, int firstLine, int lastLine, TextEdit stt)
        {
            foreach (var item in dic)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            CellRange usedRange = workbook.Worksheets[cboHPVTtenSheet.Text].GetUsedRange();
            int lastline_vl = usedRange.RowCount;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            fcn_kiemtracongtrinh_hangmuc(firstLine);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            bool Mutiple = txtHPVTHS_NhanCong.Text == txtHPVTHS_MTC.Text ? false : true;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            if (tencongtrinh == "")
            {
                firstLine = dic_congtrinh.First().Key + 2;
                fcn_kiemtracongtrinh_hangmuc(firstLine);
            }
            int Index = rg_LayDuLieu.SelectedIndex;
            if (Index > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";
            double VL = 0, NC = 0, MTC = 0, STT = 0;
            for (int i = firstLine; i <= lastLine; i++)
            {
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                mahieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{mahieu}_{TenCongTac}");
                if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (Index > 0)
                        continue;
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    NewRow["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                    if (Te_DonGiaNhom.Text.HasValue())
                    {
                        double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                        if (DonGiaNhom > 0)
                            NewRow["DonGia"] = DonGiaNhom;
                    }
                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                    if (KLNhom > 0)
                        NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;
                    NewRow["SortId"] = SortIdNhom++;

                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                    if (txt_congtrinh_sttND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRow["GhiChuBoSungJson"] = encryptedStr;
                    dt_NhomCT.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                            Mahieu.ToUpper() == "THM" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                        {
                            i = j - 1;
                            break;

                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                    Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, j);
                            else
                                RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                    Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, j);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }

                }
                else if (mahieu == te_PhanTuyen.Text)
                {
                    string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);

                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_PhanTuyen.Text || Mahieu.ToUpper() == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        else if (Mahieu == te_Nhom.Text)
                        {
                            string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRowNhom = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRowNhom["Code"] = CodeNhomCT;
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
                            NewRowNhom["DonVi"] = CrowCTNhom[txtCTDonVi.Text].Value.TextValue;
                            if (Te_DonGiaNhom.Text.HasValue())
                            {
                                double.TryParse(CrowCTNhom[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                if (DonGiaNhom > 0)
                                    NewRowNhom["DonGia"] = DonGiaNhom;
                            }
                            double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                            if (KLNhom > 0)
                                NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = CrowCTNhom[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                            NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                            NewRowNhom["SortId"] = SortIdNhom++;
                            dt_NhomCT.Rows.Add(NewRowNhom);
                            for (int k = j + 1; k <= lastLine; k++)
                            {
                                Row CrowCTNhomNew = ws.Rows[k];
                                Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                    Mahieu.ToUpper() == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                                {
                                    j = k - 1;
                                    i = j - 1;
                                    break;

                                }
                                else
                                {
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    VL = CrowCTNhomNew[txtCTDGVatLieu.Text].Value.NumericValue;
                                    NC = CrowCTNhomNew[txtCTDGNhanCong.Text].Value.NumericValue;
                                    MTC = CrowCTNhomNew[txtCTDGMay.Text].Value.NumericValue;
                                    STT = CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue;
                                    RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    int End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (double.TryParse(CrowCTNhomNew[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                        RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, CrowCTNhomNew[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhomNew[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhomNew[txtCTDGMay.Text].Value.ToString(), CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, k);
                                    else
                                        RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, CrowCTNhomNew[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhomNew[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhomNew[txtCTDGMay.Text].Value.ToString(), CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, k);
                                    if (End != 0)
                                    {
                                        j = End;
                                        k = End - 1;
                                        i = j - 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString(), j, true);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }


                }
                else if (!Int32.TryParse(crRow[stt.Text].Value.ToString(), out int so))
                {
                    if (crRow[txtCTMaDuToan.Text].Value.ToString().ToUpper() == "THM")
                    {
                        foreach (var item in dic_congtrinh)
                        {
                            if (i < item.Key)
                            {
                                i = item.Key + 2;
                                break;
                            }
                        }
                        string mahieunext = ws.Rows[i + 1][txtCTMaDuToan.Text].Value.ToString();
                        if (mahieunext == MyConstant.CONST_TYPE_HANGMUC)
                            fcn_kiemtracongtrinh_hangmuc(i + 2, true);
                        else
                            fcn_kiemtracongtrinh_hangmuc(i);
                        continue;
                    }
                    else if (mahieu != "")
                    {
                        string _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                        int count = _STT.Count(x => x == '.');
                        if (count == 0)
                            continue;
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;

                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, i);
                        else
                            RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, i);
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                else
                {
                    if (mahieu != "")
                    {
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;

                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, i);
                        else
                            RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.ToString(), Mutiple, i);
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }

            spsheet_XemFile.EndUpdate();

            try
            {
            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private void fcn_truyendataExxcel_GiaoThongTruongSon(int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            List<string> LstDG = new List<string>() { "3.DG Duong", "4.DG CS", "5.DG cau", "6.DG KS" };
            IWorkbook workbook = spsheet_XemFile.Document;
            CellRange usedRange = workbook.Worksheets[cboHPVTtenSheet.Text].GetUsedRange();
            int lastline_vl = usedRange.RowCount;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Worksheet ws = workbook.Worksheets[cbe_TongHop.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            bool Mutiple = txtHPVTHS_NhanCong.Text == txtHPVTHS_MTC.Text ? false : true;
            //fcn_kiemtracongtrinh_hangmuc(firstLine);
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            string mahieu = "", TenCongTac = "", codetheogiaidoan = "";
            int STT = 0;
            string NameSheetCon = "";
            int IndexCT = rg_LayDuLieu.SelectedIndex;
            if (IndexCT > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            for (int i = firstLine; i <= lastLine; i++)
            {
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                TenCongTac = crRow[te_TongHopTenCT.Text].Value.ToString();
                string STTCha = crRow[te_TongHopSTT.Text].Value.ToString();
                int.TryParse(STTCha, out STT);
                //STT = crRow[te_TongHopSTT.Text].Value.NumericValue;
                WaitFormHelper.ShowWaitForm($"{i + 1}.{mahieu}_{TenCongTac}");
                if (STT > 0)//Hạng mục hoặc công trình
                {
                    if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                    {
                        if (IndexCT > 0)
                            continue;
                        codecongtrinh = Guid.NewGuid().ToString();
                        tencongtrinh = TenCongTac;
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                    }
                    else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = TenCongTac;
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                        continue;
                    }
                    else if (mahieu.ToUpper() == te_PhanTuyen.Text)
                    {
                        string TenTuyen = crRow[te_TongHopTenCT.Text].Value.ToString();
                        DataRow NewRow = dt_Tuyen.NewRow();
                        string CodeTuyen = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeTuyen;
                        NewRow["CodeHangMuc"] = codehangmuc;
                        NewRow["Ten"] = TenTuyen;
                        NewRow["SortId"] = SortIdTuyen++;
                        dt_Tuyen.Rows.Add(NewRow);
                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            crRow = ws.Rows[j];
                            mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                            if (mahieu == te_PhanTuyen.Text || mahieu.ToUpper() == "THM" || mahieu == te_EndTuyen.Text || mahieu == "" || mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                            {
                                i = j - 1;
                                break;

                            }
                            if (!int.TryParse(crRow[te_TongHopMaHieu.Text].Value.ToString(), out int STTNhom))
                                continue;
                            STTCha = crRow[te_TongHopSTT.Text].Value.ToString();
                            string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                            //double STTNhom = crRow[te_TongHopMaHieu.Text].Value.NumericValue;
                            DataRow NewRowNhom = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRowNhom["Code"] = CodeNhomCT;
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                            NewRowNhom["Ten"] = tennhom;
                            double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaNhom);
                            if (DonGiaNhom > 0)
                                NewRowNhom["DonGia"] = DonGiaNhom;
                            NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                            double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTruongSon);
                            if (KLTruongSon > 0)
                                NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                            NewRowNhom["SortId"] = SortIdNhom++;
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                            if (te_TongHopSTTND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                            dt_NhomCT.Rows.Add(NewRowNhom);
                            int RowNhom = 0;
                            //double KLTruongSon = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                            double KLTong = 0;
                            foreach (string item in LstDG)
                            {
                                IEnumerable<Cell> searchResult = workbook.Worksheets[item].Search(tennhom);

                                foreach (Cell cell in searchResult)
                                {
                                    double.TryParse(cell.Worksheet.Rows[cell.RowIndex][txt_macongtac.Text].Value.ToString(), out double CheckSTT);
                                    string STTCon = cell.Worksheet.Rows[cell.RowIndex]["A"].Value.ToString();
                                    if (STTNhom == CheckSTT || STTCon == STTCha)
                                    {
                                        NameSheetCon = cell.Worksheet.Name;
                                        RowNhom = cell.RowIndex;
                                        double.TryParse(cell.Worksheet.Rows[cell.RowIndex][txt_klduthau.Text].Value.ToString(), out KLTong);
                                        goto NHOM1;
                                    }
                                }
                            }
                            NHOM1:
                            if (RowNhom == 0)
                                continue;
                            Worksheet WsCon = workbook.Worksheets[NameSheetCon];
                            int LastLine = WsCon.GetUsedRange().BottomRowIndex;
                            for (int k = RowNhom + 1; k <= LastLine; k++)
                            {
                                Row Crow = WsCon.Rows[k];
                                if (Crow[txt_macongtac.Text].Value.NumericValue > 0)
                                    break;
                                else
                                {
                                    double sttcon = Crow[txt_STT.Text].Value.NumericValue;
                                    double.TryParse(Crow[txt_klduthauCT.Text].Value.ToString(), out double KLChiTiet);
                                    if (sttcon <= 0)
                                    {
                                        if (KLChiTiet > 0)
                                        {
                                            double KLCTacCon = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                            DataRow dtRow = dt.NewRow();
                                            DataRow dtRowDLG = dt_DLG.NewRow();
                                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                            string code = Guid.NewGuid().ToString();
                                            dtRow["Code"] = code;
                                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                            dtRow_congtactheogiadoan["SortId"] = SortId;
                                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacCon;
                                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                            dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                            double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                            dtRow["Modified"] = true;
                                            dtRow["PhatSinh"] = false;
                                            dtRow["CodePhanTuyen"] = CodeTuyen;
                                            dtRow_congtactheogiadoan["Modified"] = true;
                                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                            dt.Rows.Add(dtRow);
                                            dt_DLG.Rows.Add(dtRowDLG);
                                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                            SortId++;
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        string formula = Crow[txt_STT.Text].Formula;
                                        string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                        codetheogiaidoan = Guid.NewGuid().ToString();
                                        double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCon);
                                        double KLCTacCon = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                        if (string.IsNullOrEmpty(Index))
                                        {
                                            if (Crow["B"].Value.TextValue.StartsWith("TT"))
                                            {
                                                DataRow dtRow = dt.NewRow();
                                                DataRow dtRowDLG = dt_DLG.NewRow();
                                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                                string code = Guid.NewGuid().ToString();
                                                dtRow["Code"] = code;
                                                dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
                                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                                dtRow_congtactheogiadoan["SortId"] = SortId;
                                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacCon;
                                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                                dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                                dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                                dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                                dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                                double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                                dtRow["Modified"] = true;
                                                dtRow["PhatSinh"] = false;
                                                dtRow["CodePhanTuyen"] = CodeTuyen;
                                                dtRow_congtactheogiadoan["Modified"] = true;
                                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                                dtRow_congtactheogiadoan["GhiChu"] = "Kiểm tra đơn giá và tên công tác";
                                                dt.Rows.Add(dtRow);
                                                dt_DLG.Rows.Add(dtRowDLG);
                                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                                SortId++;

                                                string codevatu = Guid.NewGuid().ToString();
                                                DataRow dt_vattu = dtHaoPhi.NewRow();
                                                dt_vattu["Code"] = codevatu;
                                                dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                                dt_vattu["MaVatLieu"] = "TT";
                                                dt_vattu["VatTu"] = Crow[txt_tencongtac_HD.Text].Value.TextValue; ;
                                                dt_vattu["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue; ;
                                                dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = 0;
                                                dt_vattu["LoaiVatTu"] = "Vật liệu";
                                                dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                                dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                                dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dtHaoPhi.Rows.Add(dt_vattu);
                                                continue;
                                            }
                                            else
                                            {
                                                double STT_CTNoLink = Crow[txt_STT.Text].Value.NumericValue;
                                                string TCT = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                                IEnumerable<Cell> searchResultCT = MyFunction.SearchRangeCell(workbook.Worksheets[cboCTtenSheet.Text], TCT);
                                                foreach (Cell cell in searchResultCT)
                                                {
                                                    double CheckSTT = cell.Worksheet.Rows[cell.RowIndex][txt_congtrinh_stt.Text].Value.NumericValue;
                                                    if (STT_CTNoLink == CheckSTT)
                                                    {
                                                        Fcn_UpdateCongTacGiaoThongTruongSon(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, cell.RowIndex + 1, KLCTacCon, DonGiaCon, CodeNhomCT, CodeTuyen);
                                                        SortId++;
                                                        break;
                                                    }
                                                }
                                                continue;
                                            }
                                        }
                                        Fcn_UpdateCongTacGiaoThongTruongSon(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLCTacCon, DonGiaCon, CodeNhomCT, CodeTuyen);
                                        SortId++;
                                    }
                                }
                            }
                        }

                    }
                    else
                        continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    if (string.IsNullOrEmpty(codecongtrinh))
                    {
                        codecongtrinh = Guid.NewGuid().ToString();
                        tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                    }
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (IndexCT > 0)
                        continue;
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                }
                else if (mahieu.ToUpper() == te_PhanTuyen.Text)
                {
                    if (string.IsNullOrEmpty(codehangmuc))
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    }
                    string TenTuyen = crRow[te_TongHopTenCT.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        crRow = ws.Rows[j];
                        mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                        if (mahieu == te_PhanTuyen.Text || mahieu.ToUpper() == "THM" || mahieu == te_EndTuyen.Text || mahieu == "" || mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        if (!int.TryParse(crRow[te_TongHopMaHieu.Text].Value.ToString(), out int STTNhom))
                            continue;
                        STTCha = crRow[te_TongHopSTT.Text].Value.ToString();
                        string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                        //double STTNhom = crRow[te_TongHopMaHieu.Text].Value.NumericValue;
                        DataRow NewRowNhom = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        NewRowNhom["Code"] = CodeNhomCT;
                        NewRowNhom["CodeHangMuc"] = codehangmuc;
                        NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                        NewRowNhom["Ten"] = tennhom;
                        double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaNhom);
                        if (DonGiaNhom > 0)
                            NewRowNhom["DonGia"] = DonGiaNhom;
                        NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                        double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTruongSon);
                        if (KLTruongSon > 0)
                            NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                        NewRowNhom["SortId"] = SortIdNhom++;
                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                        if (te_TongHopSTTND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                        dt_NhomCT.Rows.Add(NewRowNhom);
                        int RowNhom = 0;
                        //double KLTruongSon = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                        double KLTong = 0;
                        foreach (string item in LstDG)
                        {
                            IEnumerable<Cell> searchResult = workbook.Worksheets[item].Search(tennhom);

                            foreach (Cell cell in searchResult)
                            {
                                double.TryParse(cell.Worksheet.Rows[cell.RowIndex][txt_macongtac.Text].Value.ToString(), out double CheckSTT);
                                string STTCon = cell.Worksheet.Rows[cell.RowIndex]["A"].Value.ToString();
                                if (STTNhom == CheckSTT || STTCon == STTCha)
                                {
                                    NameSheetCon = cell.Worksheet.Name;
                                    RowNhom = cell.RowIndex;
                                    double.TryParse(cell.Worksheet.Rows[cell.RowIndex][txt_klduthau.Text].Value.ToString(), out KLTong);
                                    goto NHOM1;
                                }
                            }
                        }
                        NHOM1:
                        if (RowNhom == 0)
                            continue;
                        Worksheet WsCon = workbook.Worksheets[NameSheetCon];
                        int LastLine = WsCon.GetUsedRange().BottomRowIndex;
                        for (int k = RowNhom + 1; k <= LastLine; k++)
                        {
                            Row Crow = WsCon.Rows[k];
                            if (Crow[txt_macongtac.Text].Value.NumericValue > 0)
                                break;
                            else
                            {
                                double sttcon = Crow[txt_STT.Text].Value.NumericValue;
                                double.TryParse(Crow[txt_klduthauCT.Text].Value.ToString(), out double KLChiTiet);
                                if (sttcon <= 0)
                                {
                                    if (KLChiTiet > 0)
                                    {
                                        double KLCTacCon = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                        DataRow dtRow = dt.NewRow();
                                        DataRow dtRowDLG = dt_DLG.NewRow();
                                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                        string code = Guid.NewGuid().ToString();
                                        dtRow["Code"] = code;
                                        dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                        dtRow_congtactheogiadoan["SortId"] = SortId;
                                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                        dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacCon;
                                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                        dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                        dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                        dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                        dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                        double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                        dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                               = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                        dtRow["Modified"] = true;
                                        dtRow["PhatSinh"] = false;
                                        dtRow["CodePhanTuyen"] = CodeTuyen;
                                        dtRow_congtactheogiadoan["Modified"] = true;
                                        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                        dt.Rows.Add(dtRow);
                                        dt_DLG.Rows.Add(dtRowDLG);
                                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                        SortId++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    string formula = Crow[txt_STT.Text].Formula;
                                    string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCon);
                                    double KLCTacCon = KLTong == 0 ? 0 : Math.Round(KLTruongSon / KLTong * KLChiTiet, 4);
                                    if (string.IsNullOrEmpty(Index))
                                    {
                                        if (Crow["B"].Value.TextValue.StartsWith("TT"))
                                        {
                                            DataRow dtRow = dt.NewRow();
                                            DataRow dtRowDLG = dt_DLG.NewRow();
                                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                            string code = Guid.NewGuid().ToString();
                                            dtRow["Code"] = code;
                                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
                                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                            dtRow_congtactheogiadoan["SortId"] = SortId;
                                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacCon;
                                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                            dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                            double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                            dtRow["Modified"] = true;
                                            dtRow["PhatSinh"] = false;
                                            dtRow["CodePhanTuyen"] = CodeTuyen;
                                            dtRow_congtactheogiadoan["Modified"] = true;
                                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                            dtRow_congtactheogiadoan["GhiChu"] = "Kiểm tra đơn giá và tên công tác";
                                            dt.Rows.Add(dtRow);
                                            dt_DLG.Rows.Add(dtRowDLG);
                                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                            SortId++;

                                            string codevatu = Guid.NewGuid().ToString();
                                            DataRow dt_vattu = dtHaoPhi.NewRow();
                                            dt_vattu["Code"] = codevatu;
                                            dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                            dt_vattu["MaVatLieu"] = "TT";
                                            dt_vattu["VatTu"] = Crow[txt_tencongtac_HD.Text].Value.TextValue; ;
                                            dt_vattu["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue; ;
                                            dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = 0;
                                            dt_vattu["LoaiVatTu"] = "Vật liệu";
                                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                            dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                            dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtHaoPhi.Rows.Add(dt_vattu);
                                            continue;
                                        }
                                        else
                                        {
                                            double STT_CTNoLink = Crow[txt_STT.Text].Value.NumericValue;
                                            string TCT = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                            IEnumerable<Cell> searchResultCT = MyFunction.SearchRangeCell(workbook.Worksheets[cboCTtenSheet.Text], TCT);
                                            foreach (Cell cell in searchResultCT)
                                            {
                                                double CheckSTT = cell.Worksheet.Rows[cell.RowIndex][txt_congtrinh_stt.Text].Value.NumericValue;
                                                if (STT_CTNoLink == CheckSTT)
                                                {
                                                    Fcn_UpdateCongTacGiaoThongTruongSon(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, cell.RowIndex + 1, KLCTacCon, DonGiaCon, CodeNhomCT, CodeTuyen);
                                                    SortId++;
                                                    break;
                                                }
                                            }
                                            continue;
                                        }
                                    }
                                    Fcn_UpdateCongTacGiaoThongTruongSon(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLCTacCon, DonGiaCon, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                }
                            }
                        }
                    }

                }
                else///là nhóm công tác
                {
                    if (!int.TryParse(crRow[te_TongHopMaHieu.Text].Value.ToString(), out int STTNhom))
                        continue;
                    if (string.IsNullOrEmpty(codehangmuc))
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    }
                    string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                    //double STTNhom = crRow[te_TongHopMaHieu.Text].Value.NumericValue;
                    DataRow NewRowNhom = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRowNhom["Code"] = CodeNhomCT;
                    NewRowNhom["CodeHangMuc"] = codehangmuc;
                    NewRowNhom["Ten"] = tennhom;
                    double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaNhom);
                    if (DonGiaNhom > 0)
                        NewRowNhom["DonGia"] = DonGiaNhom;
                    NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                    double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTruongSon);
                    //double KLTruongSon = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                    if (KLTruongSon > 0)
                        NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                    if (te_TongHopSTTND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                    NewRowNhom["SortId"] = SortIdNhom++;
                    dt_NhomCT.Rows.Add(NewRowNhom);
                    int RowNhom = 0;
                    double KLTong = 0;
                    foreach (string item in LstDG)
                    {
                        IEnumerable<Cell> searchResult = workbook.Worksheets[item].Search(tennhom);

                        foreach (Cell cell in searchResult)
                        {
                            //double CheckSTT = cell.Worksheet.Rows[cell.RowIndex][txt_macongtac.Text].Value.NumericValue;
                            double.TryParse(cell.Worksheet.Rows[cell.RowIndex][txt_macongtac.Text].Value.ToString(), out double CheckSTT);
                            string STTCon = cell.Worksheet.Rows[cell.RowIndex]["A"].Value.ToString();
                            if (STTNhom == CheckSTT || STTCon == STTCha)
                            {
                                NameSheetCon = cell.Worksheet.Name;
                                RowNhom = cell.RowIndex;
                                double.TryParse(cell.Worksheet.Rows[cell.RowIndex][txt_klduthau.Text].Value.ToString(), out KLTong);
                                //KLTong = cell.Worksheet.Rows[cell.RowIndex][txt_klduthau.Text].Value.NumericValue;
                                goto NHOM;
                            }
                        }
                    }
                    NHOM:
                    if (RowNhom == 0)
                        continue;
                    Worksheet WsCon = workbook.Worksheets[NameSheetCon];
                    int LastLine = WsCon.GetUsedRange().BottomRowIndex;
                    for (int k = RowNhom + 1; k <= LastLine; k++)
                    {
                        Row Crow = WsCon.Rows[k];
                        //double KLChiTiet = Crow[txt_klduthauCT.Text].Value.NumericValue;
                        double.TryParse(Crow[txt_klduthauCT.Text].Value.ToString(), out double KLChiTiet);
                        if (Crow[txt_macongtac.Text].Value.NumericValue > 0)
                            break;
                        else
                        {
                            double sttcon = Crow[txt_STT.Text].Value.NumericValue;
                            if (sttcon <= 0)
                            {
                                if (KLChiTiet > 0)
                                {
                                    double KLCTacCon = KLTong <= 0 ? 0 : Math.Round(KLChiTiet / KLTong * KLTruongSon, 4);
                                    DataRow dtRow = dt.NewRow();
                                    DataRow dtRowDLG = dt_DLG.NewRow();
                                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                    string code = Guid.NewGuid().ToString();
                                    dtRow["Code"] = code;
                                    dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                    dtRow_congtactheogiadoan["SortId"] = SortId;
                                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacCon;
                                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                    dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                    dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                    dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                    dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                    double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                    dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                           = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                    dtRow["Modified"] = true;
                                    dtRow["PhatSinh"] = false;
                                    dtRow_congtactheogiadoan["Modified"] = true;
                                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ?
                                        (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                    dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                    dt.Rows.Add(dtRow);
                                    dt_DLG.Rows.Add(dtRowDLG);
                                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                    SortId++;
                                    continue;
                                }
                            }
                            else
                            {
                                string formula = Crow[txt_STT.Text].Formula;
                                string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCon);
                                //double DonGiaCon = Crow[txt_dongiaduthau.Text].Value.NumericValue;
                                double KLCTacCon = KLTong <= 0 ? 0 : Math.Round(KLChiTiet / KLTong * KLTruongSon, 4);
                                if (string.IsNullOrEmpty(Index))
                                {
                                    if (Crow["B"].Value.TextValue.StartsWith("TT"))
                                    {
                                        DataRow dtRow = dt.NewRow();
                                        DataRow dtRowDLG = dt_DLG.NewRow();
                                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                        string code = Guid.NewGuid().ToString();
                                        dtRow["Code"] = code;
                                        dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
                                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                        dtRow_congtactheogiadoan["SortId"] = SortId;
                                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                        dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCTacCon;
                                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                        dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                        dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                        dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                        dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue;
                                        double.TryParse(Crow[txt_dongiaduthau.Text].Value.ToString(), out double DonGiaCongTac);
                                        dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                               = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                        dtRow["Modified"] = true;
                                        dtRow["PhatSinh"] = false;
                                        dtRow_congtactheogiadoan["Modified"] = true;
                                        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ?
                                            Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ?
                                            Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                        dt.Rows.Add(dtRow);
                                        dt_DLG.Rows.Add(dtRowDLG);
                                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                        SortId++;

                                        string codevatu = Guid.NewGuid().ToString();
                                        DataRow dt_vattu = dtHaoPhi.NewRow();
                                        dt_vattu["Code"] = codevatu;
                                        dt_vattu["CodeCongTac"] = codetheogiaidoan;
                                        dt_vattu["MaVatLieu"] = "TT";
                                        dt_vattu["VatTu"] = Crow[txt_tencongtac_HD.Text].Value.TextValue; ;
                                        dt_vattu["DonVi"] = Crow[txt_tendonvi.Text].Value.TextValue; ;
                                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = 0;
                                        dt_vattu["LoaiVatTu"] = "Vật liệu";
                                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtHaoPhi.Rows.Add(dt_vattu);
                                        continue;
                                    }
                                    else
                                    {
                                        double STT_CTNoLink = Crow[txt_STT.Text].Value.NumericValue;
                                        string TCT = Crow[txt_tencongtac_HD.Text].Value.TextValue;
                                        IEnumerable<Cell> searchResultCT = MyFunction.SearchRangeCell(workbook.Worksheets[cboCTtenSheet.Text], TCT);
                                        foreach (Cell cell in searchResultCT)
                                        {
                                            double CheckSTT = cell.Worksheet.Rows[cell.RowIndex][txt_congtrinh_stt.Text].Value.NumericValue;
                                            if (STT_CTNoLink == CheckSTT)
                                            {
                                                Fcn_UpdateCongTacGiaoThongTruongSon(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, cell.RowIndex + 1, KLCTacCon, DonGiaCon, CodeNhomCT);
                                                SortId++;
                                                break;
                                            }
                                        }
                                        continue;
                                    }
                                }
                                Fcn_UpdateCongTacGiaoThongTruongSon(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLCTacCon, DonGiaCon, CodeNhomCT);
                                SortId++;
                            }
                        }
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }
            spsheet_XemFile.EndUpdate();
            try
            {
            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private void fcn_truyendataExxcel_GiaoThongTruongSon1(int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            List<string> LstDG = new List<string>() { "2.DG Đuong", "3.DG Cau", "4.DG CS", "1.DG Khao sat" };
            Dictionary<string, string> Dic_DGDuong = new Dictionary<string, string>()
            {
                { "STT","A" },
                { "MaCT","B" },
                { "TenCT","D" },
                { "DV","E" },
                { "KL","F" },
                { "KLCTiet","G" },
                { "DonGiaVL","H" },
                { "DonGia","K" },
            };
            Dictionary<string, string> Dic_DGCau_CS = new Dictionary<string, string>()
            {
                { "STT","A" },
                { "MaCT","B" },
                { "TenCT","C" },
                { "DV","D" },
                { "KL","E" },
                { "KLCTiet","F" },
                { "DonGiaVL","G" },
                { "DonGia","J" },
            };
            Dictionary<string, string> Dic_KS = new Dictionary<string, string>()
            {
                { "STT","A" },
                { "MaCT","B" },
                { "TenCT","D" },
                { "DV","E" },
                { "KL","F" },
                { "KLCTiet","G" },
                { "DonGia","H" },
            };
            IWorkbook workbook = spsheet_XemFile.Document;
            CellRange usedRange = workbook.Worksheets[cboHPVTtenSheet.Text].GetUsedRange();
            int lastline_vl = usedRange.RowCount;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Worksheet ws = workbook.Worksheets[cbe_TongHop.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            bool Mutiple = txtHPVTHS_NhanCong.Text == txtHPVTHS_MTC.Text ? false : true;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            string mahieu = "", TenCongTac = "", codetheogiaidoan = "";
            int STT = 0;
            string NameSheetCon = "";
            Dictionary<string, Dictionary<string, string>> DicSheet = new Dictionary<string, Dictionary<string, string>>()
            {
                {"2.DG Đuong",Dic_DGDuong },
                {"3.DG Cau",Dic_DGCau_CS },
                {"4.DG CS",Dic_DGCau_CS },
                {"1.DG Khao sat",Dic_KS },
            };
            Dictionary<string, string> Dic = new Dictionary<string, string>();
            Regex Multi = new Regex(@"\D(?<Start>\d+):\D(?<End>\d+)");
            Regex First = new Regex(@"\D(?<Start>\d+)");
            int IndexCT = rg_LayDuLieu.SelectedIndex;
            if (IndexCT > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            //Regex TenSheet = new Regex(@"\A=ROUND\D\D(?<Name>(\S|\s)+)\D!\D(?<Index>\d+);|\A=ROUND\D\D\D(?<Name1>(\S|\s)+)\D!\D(?<Index1>\d+)\S;");
            for (int i = firstLine; i <= lastLine; i++)
            {
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                TenCongTac = crRow[te_TongHopTenCT.Text].Value.ToString();
                string STTCha = crRow[te_TongHopSTT.Text].Value.ToString();
                int.TryParse(STTCha, out STT);
                WaitFormHelper.ShowWaitForm($"{i + 1}.{mahieu}_{TenCongTac}");
                if (STT > 0)//Hạng mục hoặc công trình
                {
                    if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                    {
                        if (IndexCT > 0)
                            continue;
                        codecongtrinh = Guid.NewGuid().ToString();
                        tencongtrinh = TenCongTac;
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        continue;
                    }
                    else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = TenCongTac;
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                        continue;
                    }
                    else if (mahieu.ToUpper() == te_PhanTuyen.Text)
                    {
                        string TenTuyen = crRow[te_TongHopTenCT.Text].Value.ToString();
                        DataRow NewRow = dt_Tuyen.NewRow();
                        string CodeTuyen = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeTuyen;
                        NewRow["CodeHangMuc"] = codehangmuc;
                        NewRow["Ten"] = TenTuyen;
                        NewRow["SortId"] = SortIdTuyen++;
                        dt_Tuyen.Rows.Add(NewRow);
                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            crRow = ws.Rows[j];
                            mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                            if (mahieu == te_PhanTuyen.Text || mahieu.ToUpper() == "THM" || mahieu == te_EndTuyen.Text || mahieu == "" || mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                            {
                                i = j - 1;
                                break;

                            }
                            //double KLTruongSon = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                            double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTruongSon);
                            if (!int.TryParse(crRow[te_TongHopMaHieu.Text].Value.ToString(), out int STTNhom))
                            {
                                if (KLTruongSon <= 0)
                                    continue;
                            }
                            if (string.IsNullOrEmpty(codehangmuc))
                            {
                                if (string.IsNullOrEmpty(codecongtrinh))
                                {
                                    codecongtrinh = Guid.NewGuid().ToString();
                                    tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                                }
                                codehangmuc = Guid.NewGuid().ToString();
                                tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                                queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                            }
                            string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                            //double STTNhom = crRow[te_TongHopMaHieu.Text].Value.NumericValue;
                            DataRow NewRowNhom = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRowNhom["Code"] = CodeNhomCT;
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                            NewRowNhom["Ten"] = tennhom;
                            double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaNhom);
                            if (DonGiaNhom > 0)
                                NewRowNhom["DonGia"] = DonGiaNhom;
                            //NewRowNhom["DonGia"] = crRow[te_TongHopDonGia.Text].Value.NumericValue;
                            NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                            if (KLTruongSon > 0)
                                NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                            NewRowNhom["SortId"] = SortIdNhom++;
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                            if (te_TongHopSTTND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                            dt_NhomCT.Rows.Add(NewRowNhom);
                            int RowNhom = 0;
                            double KLTong = 0;
                            STTCha = crRow[te_TongHopSTT.Text].Value.ToString();
                            foreach (string item in LstDG)
                            {
                                IEnumerable<Cell> searchResult = workbook.Worksheets[item].Search(tennhom);

                                foreach (Cell cell in searchResult)
                                {
                                    double CheckSTT = cell.Worksheet.Rows[cell.RowIndex][txt_macongtac.Text].Value.NumericValue;
                                    string STTCon = cell.Worksheet.Rows[cell.RowIndex]["A"].Value.TextValue;
                                    if (STTNhom == CheckSTT && STTNhom > 0 || STTCon == STTCha)
                                    {
                                        NameSheetCon = cell.Worksheet.Name;
                                        RowNhom = cell.RowIndex;
                                        goto Nhom;
                                    }
                                }
                            }
                            Nhom:
                            if (RowNhom == 0)
                            {
                                if (KLTruongSon > 0)
                                {
                                    NewRowNhom["GhiChu"] = "Không tìm thấy tên";
                                    DataRow dtRow = dt.NewRow();
                                    DataRow dtRowDLG = dt_DLG.NewRow();
                                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                    string code = Guid.NewGuid().ToString();
                                    dtRow["Code"] = code;
                                    dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                    dtRow_congtactheogiadoan["SortId"] = SortId;
                                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                    dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                    dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                    dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tennhom;
                                    dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                                    double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaCongTac);
                                    dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                           = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                    dtRow["Modified"] = true;
                                    dtRow["PhatSinh"] = false;
                                    dtRow["CodePhanTuyen"] = CodeTuyen;
                                    dtRow_congtactheogiadoan["Modified"] = true;
                                    dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                    dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                    dt.Rows.Add(dtRow);
                                    dt_DLG.Rows.Add(dtRowDLG);
                                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                    SortId++;
                                    goto Label;
                                }
                                else
                                    continue;
                            }
                            Worksheet WsCon = workbook.Worksheets[NameSheetCon];
                            Dic = DicSheet[NameSheetCon];
                            double.TryParse(WsCon.Rows[RowNhom][Dic["KL"]].Value.ToString(), out KLTong);
                            //KLTong = WsCon.Rows[RowNhom][Dic["KL"]].Value.NumericValue;
                            int LastLine = WsCon.GetUsedRange().BottomRowIndex;
                            string Colum = NameSheetCon == "1.DG Khao sat" ? "J" : (NameSheetCon == "2.DG Đuong" ? "M" : "L");
                            string FomularTT = WsCon.Rows[RowNhom][Colum].Formula;
                            int StartLine = RowNhom + 1;
                            if (!string.IsNullOrEmpty(FomularTT))
                            {
                                MatchCollection Multimatch = Multi.Matches(FomularTT.Replace("$", string.Empty));
                                if (Multimatch.Count == 0)
                                {
                                    Multimatch = First.Matches(FomularTT.Replace("$", string.Empty));
                                    StartLine = LastLine = int.Parse(Multimatch[0].Groups["Start"].ToString());
                                }
                                else
                                {
                                    StartLine = int.Parse(Multimatch[0].Groups["Start"].ToString());
                                    LastLine = int.Parse(Multimatch[0].Groups["End"].ToString());
                                }
                            }
                            else
                                continue;
                            for (int k = RowNhom + 1; k <= LastLine; k++)
                            {
                                Row Crow = WsCon.Rows[k];
                                //double KLChiTiet = Crow[Dic["KLCTiet"]].Value.NumericValue;
                                double.TryParse(Crow[Dic["KLCTiet"]].Value.ToString(), out double KLChiTiet);
                                int.TryParse(Crow[Dic["MaCT"]].Value.ToString(), out int STTConId);
                                int.TryParse(Crow[Dic["STT"]].Value.ToString(), out int STTCon);
                                if (NameSheetCon == "2.DG Đuong")
                                {
                                    if (STTCon <= 0 && STTConId > 0)
                                    {
                                        if (string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()))
                                        {
                                            string formula = Crow[Dic["DonGiaVL"]].Formula;
                                            string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                            codetheogiaidoan = Guid.NewGuid().ToString();
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                            if (string.IsNullOrEmpty(Index))
                                                continue;
                                            Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                            SortId++;
                                        }
                                        else
                                            break;
                                    }
                                    else
                                    {
                                        if (STTCon <= 0 && STTConId <= 0)
                                        {
                                            if (KLChiTiet > 0)
                                            {

                                                string formula = Crow[Dic["DonGiaVL"]].Formula;
                                                if (string.IsNullOrEmpty(formula))
                                                {
                                                    DataRow dtRow = dt.NewRow();
                                                    DataRow dtRowDLG = dt_DLG.NewRow();
                                                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                                    string code = Guid.NewGuid().ToString();
                                                    dtRow["Code"] = code;
                                                    dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                                    dtRow_congtactheogiadoan["SortId"] = SortId;
                                                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLChiTiet;
                                                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                                    dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                                    dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                                    dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[Dic["TenCT"]].Value.TextValue;
                                                    dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[Dic["DV"]].Value.TextValue;
                                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCongTac);
                                                    dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                           = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                                    dtRow["Modified"] = true;
                                                    dtRow["CodePhanTuyen"] = CodeTuyen;
                                                    dtRow["PhatSinh"] = false;
                                                    dtRow_congtactheogiadoan["Modified"] = true;
                                                    dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                                    dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                                    dt.Rows.Add(dtRow);
                                                    dt_DLG.Rows.Add(dtRowDLG);
                                                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                                    SortId++;
                                                }
                                                else
                                                {
                                                    string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                                    if (string.IsNullOrEmpty(Index))
                                                        continue;
                                                    Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                                    SortId++;
                                                }
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            string formula = Crow[Dic["DonGiaVL"]].Formula;
                                            string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                            codetheogiaidoan = Guid.NewGuid().ToString();
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                            if (string.IsNullOrEmpty(Index))
                                                continue;
                                            Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                            SortId++;
                                        }
                                    }
                                }
                                else if (NameSheetCon == "3.DG Cau")
                                {
                                    if (STTCon <= 0 && STTConId > 0)
                                        break;
                                    else
                                    {
                                        if (string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()) && string.IsNullOrEmpty(Crow[Dic["MaCT"]].Value.ToString()))
                                        {
                                            if (KLChiTiet > 0)
                                            {
                                                string formula = Crow[Dic["DonGiaVL"]].Formula;
                                                if (string.IsNullOrEmpty(formula))
                                                {

                                                    DataRow dtRow = dt.NewRow();
                                                    DataRow dtRowDLG = dt_DLG.NewRow();
                                                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                                    string code = Guid.NewGuid().ToString();
                                                    dtRow["Code"] = code;
                                                    dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                                    dtRow_congtactheogiadoan["SortId"] = SortId;
                                                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLChiTiet;
                                                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                                    dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                                    dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                                    dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[Dic["TenCT"]].Value.TextValue;
                                                    dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[Dic["DV"]].Value.TextValue;
                                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCongTac);
                                                    dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                           = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                                    dtRow["Modified"] = true;
                                                    dtRow["PhatSinh"] = false;
                                                    dtRow["CodePhanTuyen"] = CodeTuyen;
                                                    dtRow_congtactheogiadoan["Modified"] = true;
                                                    dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                                    dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                                    dt.Rows.Add(dtRow);
                                                    dt_DLG.Rows.Add(dtRowDLG);
                                                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                                    SortId++;
                                                }
                                                else
                                                {
                                                    string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                                    if (string.IsNullOrEmpty(Index))
                                                    {
                                                        if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                            break;
                                                        continue;
                                                    }
                                                    Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                                    SortId++;
                                                }
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            string formula = Crow[Dic["DonGiaVL"]].Formula;
                                            string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                            codetheogiaidoan = Guid.NewGuid().ToString();
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                            if (string.IsNullOrEmpty(Index))
                                            {
                                                if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                    break;
                                                continue;
                                            }
                                            Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                            SortId++;
                                        }
                                    }
                                }
                                else if (NameSheetCon == "1.DG Khao sat")
                                {
                                    if (STTCon <= 0 && STTConId > 0)
                                        break;
                                    else
                                    {
                                        string formula = Crow[Dic["DonGia"]].Formula;
                                        string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                        codetheogiaidoan = Guid.NewGuid().ToString();
                                        double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                        Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                        SortId++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()) && STTConId > 0)
                                        break;
                                    else
                                    {
                                        string formula = Crow[Dic["DonGiaVL"]].Formula;
                                        if ((string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()) && string.IsNullOrEmpty(Crow[Dic["MaCT"]].Value.ToString())) || string.IsNullOrEmpty(formula))
                                        {
                                            if (KLChiTiet > 0)
                                            {
                                                string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                                if (string.IsNullOrEmpty(formula))
                                                {
                                                    DataRow dtRow = dt.NewRow();
                                                    DataRow dtRowDLG = dt_DLG.NewRow();
                                                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                                    string code = Guid.NewGuid().ToString();
                                                    dtRow["Code"] = code;
                                                    dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                                    dtRow_congtactheogiadoan["SortId"] = SortId;
                                                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLChiTiet;
                                                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                                    dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                                    dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                                    dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[Dic["TenCT"]].Value.TextValue;
                                                    dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[Dic["DV"]].Value.TextValue;
                                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCongTac);
                                                    dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                           = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                                    dtRow["Modified"] = true;
                                                    dtRow["CodePhanTuyen"] = CodeTuyen;
                                                    dtRow["PhatSinh"] = false;
                                                    dtRow_congtactheogiadoan["Modified"] = true;
                                                    dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                                    dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                                    dt.Rows.Add(dtRow);
                                                    dt_DLG.Rows.Add(dtRowDLG);
                                                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                                    SortId++;
                                                }
                                                else
                                                {
                                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                                    if (string.IsNullOrEmpty(Index))
                                                    {
                                                        if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                            break;
                                                        continue;
                                                    }
                                                    Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                                    SortId++;
                                                }
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                            codetheogiaidoan = Guid.NewGuid().ToString();
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                            if (string.IsNullOrEmpty(Index))
                                            {
                                                if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                    break;
                                                continue;
                                            }
                                            Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                            SortId++;
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                        continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    if (string.IsNullOrEmpty(codecongtrinh))
                    {
                        codecongtrinh = Guid.NewGuid().ToString();
                        tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                    }
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (IndexCT > 0)
                        continue;
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                }
                else if (mahieu.ToUpper() == te_PhanTuyen.Text)
                {
                    if (string.IsNullOrEmpty(codehangmuc))
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    }
                    string TenTuyen = crRow[te_TongHopTenCT.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        crRow = ws.Rows[j];
                        mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                        if (mahieu == te_PhanTuyen.Text || mahieu.ToUpper() == "THM" || mahieu == te_EndTuyen.Text || mahieu == "" || mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        //double KLTruongSon = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                        double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTruongSon);
                        if (!int.TryParse(crRow[te_TongHopMaHieu.Text].Value.ToString(), out int STTNhom))
                        {
                            if (KLTruongSon <= 0)
                                continue;
                        }
                        if (string.IsNullOrEmpty(codehangmuc))
                        {
                            if (string.IsNullOrEmpty(codecongtrinh))
                            {
                                codecongtrinh = Guid.NewGuid().ToString();
                                tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                                queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                            }
                            codehangmuc = Guid.NewGuid().ToString();
                            tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                        }
                        string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                        //double STTNhom = crRow[te_TongHopMaHieu.Text].Value.NumericValue;
                        DataRow NewRowNhom = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        NewRowNhom["Code"] = CodeNhomCT;
                        NewRowNhom["CodeHangMuc"] = codehangmuc;
                        NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                        NewRowNhom["Ten"] = tennhom;
                        double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaNhom);
                        if (DonGiaNhom > 0)
                            NewRowNhom["DonGia"] = DonGiaNhom;
                        //NewRowNhom["DonGia"] = crRow[te_TongHopDonGia.Text].Value.NumericValue;
                        NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                        if (KLTruongSon > 0)
                            NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                        NewRowNhom["SortId"] = SortIdNhom++;
                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                        if (te_TongHopSTTND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                        dt_NhomCT.Rows.Add(NewRowNhom);
                        int RowNhom = 0;
                        double KLTong = 0;
                        STTCha = crRow[te_TongHopSTT.Text].Value.ToString();
                        foreach (string item in LstDG)
                        {
                            IEnumerable<Cell> searchResult = workbook.Worksheets[item].Search(tennhom);

                            foreach (Cell cell in searchResult)
                            {
                                double CheckSTT = cell.Worksheet.Rows[cell.RowIndex][txt_macongtac.Text].Value.NumericValue;
                                string STTCon = cell.Worksheet.Rows[cell.RowIndex]["A"].Value.TextValue;
                                if (STTNhom == CheckSTT && STTNhom > 0 || STTCon == STTCha)
                                {
                                    NameSheetCon = cell.Worksheet.Name;
                                    RowNhom = cell.RowIndex;
                                    goto Nhom;
                                }
                            }
                        }
                        Nhom:
                        if (RowNhom == 0)
                        {
                            if (KLTruongSon > 0)
                            {
                                NewRowNhom["GhiChu"] = "Không tìm thấy tên";
                                DataRow dtRow = dt.NewRow();
                                DataRow dtRowDLG = dt_DLG.NewRow();
                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                string code = Guid.NewGuid().ToString();
                                dtRow["Code"] = code;
                                dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                dtRow_congtactheogiadoan["SortId"] = SortId;
                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tennhom;
                                dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                                double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaCongTac);
                                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                dtRow["Modified"] = true;
                                dtRow["PhatSinh"] = false;
                                dtRow["CodePhanTuyen"] = CodeTuyen;
                                dtRow_congtactheogiadoan["Modified"] = true;
                                dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                dt.Rows.Add(dtRow);
                                dt_DLG.Rows.Add(dtRowDLG);
                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                SortId++;
                                goto Label;
                            }
                            else
                                continue;
                        }
                        Worksheet WsCon = workbook.Worksheets[NameSheetCon];
                        Dic = DicSheet[NameSheetCon];
                        double.TryParse(WsCon.Rows[RowNhom][Dic["KL"]].Value.ToString(), out KLTong);
                        //KLTong = WsCon.Rows[RowNhom][Dic["KL"]].Value.NumericValue;
                        int LastLine = WsCon.GetUsedRange().BottomRowIndex;
                        string Colum = NameSheetCon == "1.DG Khao sat" ? "J" : (NameSheetCon == "2.DG Đuong" ? "M" : "L");
                        string FomularTT = WsCon.Rows[RowNhom][Colum].Formula;
                        int StartLine = RowNhom + 1;
                        if (!string.IsNullOrEmpty(FomularTT))
                        {
                            MatchCollection Multimatch = Multi.Matches(FomularTT.Replace("$", string.Empty));
                            if (Multimatch.Count == 0)
                            {
                                Multimatch = First.Matches(FomularTT.Replace("$", string.Empty));
                                StartLine = LastLine = int.Parse(Multimatch[0].Groups["Start"].ToString());
                            }
                            else
                            {
                                StartLine = int.Parse(Multimatch[0].Groups["Start"].ToString());
                                LastLine = int.Parse(Multimatch[0].Groups["End"].ToString());
                            }
                        }
                        else
                            continue;
                        for (int k = RowNhom + 1; k <= LastLine; k++)
                        {
                            Row Crow = WsCon.Rows[k];
                            //double KLChiTiet = Crow[Dic["KLCTiet"]].Value.NumericValue;
                            double.TryParse(Crow[Dic["KLCTiet"]].Value.ToString(), out double KLChiTiet);
                            int.TryParse(Crow[Dic["MaCT"]].Value.ToString(), out int STTConId);
                            int.TryParse(Crow[Dic["STT"]].Value.ToString(), out int STTCon);
                            if (NameSheetCon == "2.DG Đuong")
                            {
                                if (STTCon <= 0 && STTConId > 0)
                                {
                                    if (string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()))
                                    {
                                        string formula = Crow[Dic["DonGiaVL"]].Formula;
                                        string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                        codetheogiaidoan = Guid.NewGuid().ToString();
                                        double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                        if (string.IsNullOrEmpty(Index))
                                            continue;
                                        Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                        SortId++;
                                    }
                                    else
                                        break;
                                }
                                else
                                {
                                    if (STTCon <= 0 && STTConId <= 0)
                                    {
                                        if (KLChiTiet > 0)
                                        {

                                            string formula = Crow[Dic["DonGiaVL"]].Formula;
                                            if (string.IsNullOrEmpty(formula))
                                            {
                                                DataRow dtRow = dt.NewRow();
                                                DataRow dtRowDLG = dt_DLG.NewRow();
                                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                                string code = Guid.NewGuid().ToString();
                                                dtRow["Code"] = code;
                                                dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                                dtRow_congtactheogiadoan["SortId"] = SortId;
                                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLChiTiet;
                                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                                dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                                dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                                dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[Dic["TenCT"]].Value.TextValue;
                                                dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[Dic["DV"]].Value.TextValue;
                                                double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCongTac);
                                                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                                dtRow["Modified"] = true;
                                                dtRow["PhatSinh"] = false;
                                                dtRow["CodePhanTuyen"] = CodeTuyen;
                                                dtRow_congtactheogiadoan["Modified"] = true;
                                                dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                                dt.Rows.Add(dtRow);
                                                dt_DLG.Rows.Add(dtRowDLG);
                                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                                SortId++;
                                            }
                                            else
                                            {
                                                string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                                codetheogiaidoan = Guid.NewGuid().ToString();
                                                double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                                if (string.IsNullOrEmpty(Index))
                                                    continue;
                                                Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen: CodeTuyen);
                                                SortId++;
                                            }
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        string formula = Crow[Dic["DonGiaVL"]].Formula;
                                        string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                        codetheogiaidoan = Guid.NewGuid().ToString();
                                        double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                        if (string.IsNullOrEmpty(Index))
                                            continue;
                                        Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                        SortId++;
                                    }
                                }
                            }
                            else if (NameSheetCon == "3.DG Cau")
                            {
                                if (STTCon <= 0 && STTConId > 0)
                                    break;
                                else
                                {
                                    if (string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()) && string.IsNullOrEmpty(Crow[Dic["MaCT"]].Value.ToString()))
                                    {
                                        if (KLChiTiet > 0)
                                        {
                                            string formula = Crow[Dic["DonGiaVL"]].Formula;
                                            if (string.IsNullOrEmpty(formula))
                                            {

                                                DataRow dtRow = dt.NewRow();
                                                DataRow dtRowDLG = dt_DLG.NewRow();
                                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                                string code = Guid.NewGuid().ToString();
                                                dtRow["Code"] = code;
                                                dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                                dtRow_congtactheogiadoan["SortId"] = SortId;
                                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLChiTiet;
                                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                                dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                                dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                                dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[Dic["TenCT"]].Value.TextValue;
                                                dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[Dic["DV"]].Value.TextValue;
                                                double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCongTac);
                                                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                                dtRow["Modified"] = true;
                                                dtRow["PhatSinh"] = false;
                                                dtRow["CodePhanTuyen"] = CodeTuyen;
                                                dtRow_congtactheogiadoan["Modified"] = true;
                                                dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                                dt.Rows.Add(dtRow);
                                                dt_DLG.Rows.Add(dtRowDLG);
                                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                                SortId++;
                                            }
                                            else
                                            {
                                                string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                                codetheogiaidoan = Guid.NewGuid().ToString();
                                                double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                                if (string.IsNullOrEmpty(Index))
                                                {
                                                    if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                        break;
                                                    continue;
                                                }
                                                Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen: CodeTuyen);
                                                SortId++;
                                            }
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        string formula = Crow[Dic["DonGiaVL"]].Formula;
                                        string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                        codetheogiaidoan = Guid.NewGuid().ToString();
                                        double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                        if (string.IsNullOrEmpty(Index))
                                        {
                                            if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                break;
                                            continue;
                                        }
                                        Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                        SortId++;
                                    }
                                }
                            }
                            else if (NameSheetCon == "1.DG Khao sat")
                            {
                                if (STTCon <= 0 && STTConId > 0)
                                    break;
                                else
                                {
                                    string formula = Crow[Dic["DonGia"]].Formula;
                                    string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                    Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    continue;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()) && STTConId > 0)
                                    break;
                                else
                                {
                                    string formula = Crow[Dic["DonGiaVL"]].Formula;
                                    if ((string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()) && string.IsNullOrEmpty(Crow[Dic["MaCT"]].Value.ToString())) || string.IsNullOrEmpty(formula))
                                    {
                                        if (KLChiTiet > 0)
                                        {
                                            string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                            if (string.IsNullOrEmpty(formula))
                                            {
                                                DataRow dtRow = dt.NewRow();
                                                DataRow dtRowDLG = dt_DLG.NewRow();
                                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                                string code = Guid.NewGuid().ToString();
                                                dtRow["Code"] = code;
                                                dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                                dtRow_congtactheogiadoan["SortId"] = SortId;
                                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLChiTiet;
                                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                                dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                                dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                                dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[Dic["TenCT"]].Value.TextValue;
                                                dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[Dic["DV"]].Value.TextValue;
                                                double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCongTac);
                                                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                                dtRow["Modified"] = true;
                                                dtRow["PhatSinh"] = false;
                                                dtRow["CodePhanTuyen"] = CodeTuyen;
                                                dtRow_congtactheogiadoan["Modified"] = true;
                                                dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                                dt.Rows.Add(dtRow);
                                                dt_DLG.Rows.Add(dtRowDLG);
                                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                                SortId++;
                                            }
                                            else
                                            {
                                                codetheogiaidoan = Guid.NewGuid().ToString();
                                                double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                                if (string.IsNullOrEmpty(Index))
                                                {
                                                    if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                        break;
                                                    continue;
                                                }
                                                Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen: CodeTuyen);
                                                SortId++;
                                            }
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                        codetheogiaidoan = Guid.NewGuid().ToString();
                                        double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                        if (string.IsNullOrEmpty(Index))
                                        {
                                            if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                break;
                                            continue;
                                        }
                                        Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT, CodeTuyen);
                                        SortId++;
                                    }
                                }
                            }
                        }
                    }

                }
                else///là nhóm công tác
                {
                    //double KLTruongSon = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                    double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLTruongSon);
                    if (!int.TryParse(crRow[te_TongHopMaHieu.Text].Value.ToString(), out int STTNhom))
                    {
                        if (KLTruongSon <= 0)
                            continue;
                    }
                    if (string.IsNullOrEmpty(codehangmuc))
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = $"Hạng mục {dt_HM.Rows.Count + 1}";
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    }
                    string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                    //double STTNhom = crRow[te_TongHopMaHieu.Text].Value.NumericValue;
                    DataRow NewRowNhom = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRowNhom["Code"] = CodeNhomCT;
                    NewRowNhom["CodeHangMuc"] = codehangmuc;
                    NewRowNhom["Ten"] = tennhom;
                    double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaNhom);
                    if (DonGiaNhom > 0)
                        NewRowNhom["DonGia"] = DonGiaNhom;
                    NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                    if (KLTruongSon > 0)
                        NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                    NewRowNhom["SortId"] = SortIdNhom++;
                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                    if (te_TongHopSTTND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                    dt_NhomCT.Rows.Add(NewRowNhom);
                    int RowNhom = 0;
                    double KLTong = 0;
                    //string FomularDG = crRow[te_TongHopDonGia.Text].Formula;
                    //if (string.IsNullOrEmpty(FomularDG))
                    //    goto Nhom1;
                    //MatchCollection RowDonGia = TenSheet.Matches(FomularDG);
                    //if (string.IsNullOrEmpty(RowDonGia[0].Groups["Name"].ToString()))
                    //{
                    //    NameSheetCon = RowDonGia[0].Groups["Name1"].ToString();
                    //    RowNhom = int.Parse(RowDonGia[0].Groups["Index1"].ToString()) - 1;
                    //}
                    //else
                    //{
                    //    NameSheetCon = RowDonGia[0].Groups["Name"].ToString();
                    //    RowNhom = int.Parse(RowDonGia[0].Groups["Index"].ToString()) - 1;
                    //}
                    foreach (string item in LstDG)
                    {
                        IEnumerable<Cell> searchResult = workbook.Worksheets[item].Search(tennhom);

                        foreach (Cell cell in searchResult)
                        {
                            double CheckSTT = cell.Worksheet.Rows[cell.RowIndex][txt_macongtac.Text].Value.NumericValue;
                            string STTCon = cell.Worksheet.Rows[cell.RowIndex]["A"].Value.TextValue;
                            if (STTNhom == CheckSTT && STTNhom > 0 || STTCon == STTCha)
                            {
                                NameSheetCon = cell.Worksheet.Name;
                                RowNhom = cell.RowIndex;
                                goto Nhom1;
                            }
                        }
                    }
                    Nhom1:
                    if (RowNhom == 0)
                    {
                        if (KLTruongSon > 0)
                        {
                            NewRowNhom["GhiChu"] = "Không tìm thấy tên";
                            DataRow dtRow = dt.NewRow();
                            DataRow dtRowDLG = dt_DLG.NewRow();
                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                            string code = Guid.NewGuid().ToString();
                            dtRow["Code"] = code;
                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                            dtRow_congtactheogiadoan["SortId"] = SortId;
                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLTruongSon;
                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tennhom;
                            dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                            double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaCongTac);
                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                            dtRow["Modified"] = true;
                            dtRow["PhatSinh"] = false;
                            dtRow_congtactheogiadoan["Modified"] = true;
                            dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue()
                                ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ?
                                (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                            dt.Rows.Add(dtRow);
                            dt_DLG.Rows.Add(dtRowDLG);
                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                            SortId++;
                            goto Label;
                        }
                        else
                            continue;
                    }
                    Worksheet WsCon = workbook.Worksheets[NameSheetCon];
                    string Colum = NameSheetCon == "1.DG Khao sat" ? "J" : (NameSheetCon == "2.DG Đuong" ? "M" : "L");
                    string FomularTT = WsCon.Rows[RowNhom][Colum].Formula;
                    int LastLine = WsCon.GetUsedRange().BottomRowIndex;
                    int StartLine = RowNhom + 1;
                    if (!string.IsNullOrEmpty(FomularTT))
                    {
                        MatchCollection Multimatch = Multi.Matches(FomularTT.Replace("$", string.Empty));
                        if (Multimatch.Count == 0)
                        {
                            Multimatch = First.Matches(FomularTT.Replace("$", string.Empty));
                            StartLine = LastLine = int.Parse(Multimatch[0].Groups["Start"].ToString());
                        }
                        else
                        {
                            StartLine = int.Parse(Multimatch[0].Groups["Start"].ToString());
                            LastLine = int.Parse(Multimatch[0].Groups["End"].ToString());
                        }

                        //string Range = FomularTT.Split(';')[1];
                        //string[] RowIndex = Range.Split(':');
                        //if (RowIndex.Count() > 1)
                        //{
                        //    StartLine = int.Parse(Regex.Replace(RowIndex[0], $@"\D", string.Empty));
                        //    LastLine = int.Parse(Regex.Replace(RowIndex[1], $@"\D", string.Empty));
                        //}
                        //else
                        //{
                        //    StartLine = LastLine = int.Parse(Regex.Replace(RowIndex[0], $@"\D", string.Empty));
                        //}
                    }
                    else
                        continue;
                    Dic = DicSheet[NameSheetCon];
                    double.TryParse(WsCon.Rows[RowNhom][Dic["KL"]].Value.ToString(), out KLTong);
                    //KLTong = WsCon.Rows[RowNhom][Dic["KL"]].Value.NumericValue;
                    for (int k = StartLine - 1; k <= LastLine - 1; k++)
                    {
                        Row Crow = WsCon.Rows[k];
                        //double KLChiTiet = Crow[Dic["KLCTiet"]].Value.NumericValue;
                        double.TryParse(Crow[Dic["KLCTiet"]].Value.ToString(), out double KLChiTiet);
                        int.TryParse(Crow[Dic["MaCT"]].Value.ToString(), out int STTConId);
                        int.TryParse(Crow[Dic["STT"]].Value.ToString(), out int STTCon);
                        if (NameSheetCon == "2.DG Đuong")
                        {
                            if (STTCon <= 0 && STTConId > 0)
                            {
                                if (string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()))
                                {
                                    string formula = Crow[Dic["DonGiaVL"]].Formula;
                                    string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    //double DonGiaCon = Crow[Dic["DonGia"]].Value.NumericValue;
                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                    if (string.IsNullOrEmpty(Index))
                                        continue;
                                    Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT);
                                    SortId++;
                                }
                                else
                                    break;
                            }
                            else
                            {
                                if (STTCon <= 0 && STTConId <= 0)
                                {
                                    if (KLChiTiet > 0)
                                    {
                                        string formula = Crow[Dic["DonGiaVL"]].Formula;
                                        if (string.IsNullOrEmpty(formula))
                                        {
                                            DataRow dtRow = dt.NewRow();
                                            DataRow dtRowDLG = dt_DLG.NewRow();
                                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                            string code = Guid.NewGuid().ToString();
                                            dtRow["Code"] = code;
                                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                            dtRow_congtactheogiadoan["SortId"] = SortId;
                                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLChiTiet;
                                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[Dic["TenCT"]].Value.TextValue;
                                            dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[Dic["DV"]].Value.TextValue;
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCongTac);
                                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                            dtRow["Modified"] = true;
                                            dtRow["PhatSinh"] = false;
                                            dtRow_congtactheogiadoan["Modified"] = true;
                                            dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                            dt.Rows.Add(dtRow);
                                            dt_DLG.Rows.Add(dtRowDLG);
                                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                            SortId++;
                                        }
                                        else
                                        {
                                            string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                            codetheogiaidoan = Guid.NewGuid().ToString();
                                            //double DonGiaCon = Crow[Dic["DonGia"]].Value.NumericValue;
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                            if (string.IsNullOrEmpty(Index))
                                                continue;
                                            Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT);
                                            SortId++;
                                        }
                                        continue;
                                    }
                                }
                                else
                                {
                                    string formula = Crow[Dic["DonGiaVL"]].Formula;
                                    string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                    //double DonGiaCon = Crow[Dic["DonGia"]].Value.NumericValue;
                                    if (string.IsNullOrEmpty(Index))
                                        continue;
                                    Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT);
                                    SortId++;
                                }
                            }
                        }
                        else if (NameSheetCon == "3.DG Cau")
                        {
                            if (STTCon <= 0 && STTConId > 0)
                                break;
                            else
                            {
                                if (string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()) && string.IsNullOrEmpty(Crow[Dic["MaCT"]].Value.ToString()))
                                {
                                    if (KLChiTiet > 0)
                                    {
                                        string formula = Crow[Dic["DonGiaVL"]].Formula;
                                        if (string.IsNullOrEmpty(formula))
                                        {

                                            DataRow dtRow = dt.NewRow();
                                            DataRow dtRowDLG = dt_DLG.NewRow();
                                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                            string code = Guid.NewGuid().ToString();
                                            dtRow["Code"] = code;
                                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                            dtRow_congtactheogiadoan["SortId"] = SortId;
                                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLChiTiet;
                                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[Dic["TenCT"]].Value.TextValue;
                                            dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[Dic["DV"]].Value.TextValue;
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCongTac);
                                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                            dtRow["Modified"] = true;
                                            dtRow["PhatSinh"] = false;
                                            dtRow_congtactheogiadoan["Modified"] = true;
                                            dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                            dt.Rows.Add(dtRow);
                                            dt_DLG.Rows.Add(dtRowDLG);
                                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                            SortId++;
                                        }
                                        else
                                        {
                                            string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                            codetheogiaidoan = Guid.NewGuid().ToString();
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                            //double DonGiaCon = Crow[Dic["DonGia"]].Value.NumericValue;
                                            if (string.IsNullOrEmpty(Index))
                                            {
                                                if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                    break;
                                                continue;
                                            }
                                            Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT);
                                            SortId++;
                                        }
                                        continue;
                                    }
                                }
                                else
                                {
                                    string formula = Crow[Dic["DonGiaVL"]].Formula;
                                    string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    //double DonGiaCon = Crow[Dic["DonGia"]].Value.NumericValue;
                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                    if (string.IsNullOrEmpty(Index))
                                    {
                                        if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                            break;
                                        continue;
                                    }
                                    Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT);
                                    SortId++;
                                    continue;
                                }
                            }
                        }
                        else if (NameSheetCon == "1.DG Khao sat")
                        {
                            if (STTCon <= 0 && STTConId > 0)
                                break;
                            else
                            {
                                string formula = Crow[Dic["DonGia"]].Formula;
                                string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                //double DonGiaCon = Crow[Dic["DonGia"]].Value.NumericValue;
                                double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT);
                                SortId++;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()) && STTConId > 0)
                                break;
                            else
                            {
                                string formula = Crow[Dic["DonGiaVL"]].Formula;
                                if ((string.IsNullOrEmpty(Crow[Dic["STT"]].Value.ToString()) && string.IsNullOrEmpty(Crow[Dic["MaCT"]].Value.ToString())) || string.IsNullOrEmpty(formula))
                                {
                                    if (KLChiTiet > 0)
                                    {
                                        string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                        if (string.IsNullOrEmpty(formula))
                                        {
                                            DataRow dtRow = dt.NewRow();
                                            DataRow dtRowDLG = dt_DLG.NewRow();
                                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                            string code = Guid.NewGuid().ToString();
                                            dtRow["Code"] = code;
                                            dtRowDLG["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                            dtRow_congtactheogiadoan["SortId"] = SortId;
                                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLChiTiet;
                                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
                                            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = "TT";
                                            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = Crow[Dic["TenCT"]].Value.TextValue;
                                            dtRow["DonVi"] = dtRowDLG["DonVi"] = Crow[Dic["DV"]].Value.TextValue;
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCongTac);
                                            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaCongTac;
                                            dtRow["Modified"] = true;
                                            dtRow["PhatSinh"] = false;
                                            dtRow_congtactheogiadoan["Modified"] = true;
                                            dtRow_congtactheogiadoan["GhiChu"] = "Không tìm thấy tên";
                                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (Crow[te_KHBegin.Text].Value.IsDateTime ? Crow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (Crow[te_KHEnd.Text].Value.IsDateTime ? Crow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                            dt.Rows.Add(dtRow);
                                            dt_DLG.Rows.Add(dtRowDLG);
                                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                            SortId++;
                                        }
                                        else
                                        {
                                            codetheogiaidoan = Guid.NewGuid().ToString();
                                            //double DonGiaCon = Crow[Dic["DonGia"]].Value.NumericValue;
                                            double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                            if (string.IsNullOrEmpty(Index))
                                            {
                                                if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                                    break;
                                                continue;
                                            }
                                            Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT);
                                            SortId++;
                                        }
                                        continue;
                                    }
                                }
                                else
                                {
                                    string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    //double DonGiaCon = Crow[Dic["DonGia"]].Value.NumericValue;
                                    double.TryParse(Crow[Dic["DonGia"]].Value.ToString(), out double DonGiaCon);
                                    if (string.IsNullOrEmpty(Index))
                                    {
                                        if (Crow[Dic["KL"]].Value.NumericValue > 0)
                                            break;
                                        continue;
                                    }
                                    Fcn_UpdateCongTacGiaoThongTruongSon1(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), KLChiTiet, DonGiaCon, CodeNhomCT);
                                    SortId++;
                                }
                            }
                        }
                    }
                }
                Label:
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }
            spsheet_XemFile.EndUpdate();
            try
            {
            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private void fcn_truyendataExxcel_GiaoThong(int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            CellRange usedRange = workbook.Worksheets[cboHPVTtenSheet.Text].GetUsedRange();
            int lastline_vl = usedRange.RowCount;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Worksheet ws = workbook.Worksheets[cbe_TongHop.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            bool Mutiple = txtHPVTHS_NhanCong.Text == txtHPVTHS_MTC.Text ? false : true;
            //fcn_kiemtracongtrinh_hangmuc(firstLine);
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", mahieu = "", TenCongTac = "", Fomular = "", codetheogiaidoan = "";
            double VL = 0, NC = 0, MTC = 0, STT = 0;
            string ParseSTT = "";
            int Index = rg_LayDuLieu.SelectedIndex;
            if (Index > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            for (int i = firstLine; i <= lastLine; i++)
            {
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                TenCongTac = crRow[te_TongHopTenCT.Text].Value.ToString();
                STT = crRow[te_TongHopSTT.Text].Value.NumericValue;
                WaitFormHelper.ShowWaitForm($"{i + 1}.{mahieu}_{TenCongTac}");
                if (STT <= 0)
                {
                    if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                    {
                        if (Index > 0)
                            continue;
                        codecongtrinh = Guid.NewGuid().ToString();
                        tencongtrinh = TenCongTac;
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                    }
                    else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                    {
                        if (string.IsNullOrEmpty(codecongtrinh))
                        {
                            codecongtrinh = Guid.NewGuid().ToString();
                            tencongtrinh = $"Công trình {dt.Rows.Count + 1}";
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                        }
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = TenCongTac;
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                        continue;
                    }
                    else if (mahieu.ToUpper() == te_PhanTuyen.Text)
                    {
                        string TenTuyen = crRow[te_TongHopTenCT.Text].Value.ToString();
                        DataRow NewRow = dt_Tuyen.NewRow();
                        string CodeTuyen = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeTuyen;
                        NewRow["CodeHangMuc"] = codehangmuc;
                        NewRow["Ten"] = TenTuyen;
                        NewRow["SortId"] = SortIdTuyen++;
                        dt_Tuyen.Rows.Add(NewRow);
                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            crRow = ws.Rows[j];
                            mahieu = crRow[te_TongHopMaHieu.Text].Value.ToString();
                            if (mahieu == te_PhanTuyen.Text || mahieu.ToUpper() == "THM" || mahieu == te_EndTuyen.Text || mahieu == "" || mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                            {
                                i = j - 1;
                                break;

                            }
                            Fomular = crRow[te_TongHopSTT.Text].Formula;
                            //DevExpress.Spreadsheet.Formulas.ParsedExpression pe = workbook.FormulaEngine.Parse(Fomular);
                            //pe.GetRanges().FirstOrDefault().GetReferenceA1();
                            if (Fomular.Contains(cboCTtenSheet.Text))
                            {
                                //ParseSTT = Fomular.Replace("=+", "").Replace($"'{cboCTtenSheet.Text}'!$B$", "");
                                ParseSTT = Regex.Replace(Fomular, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                Fcn_UpdateCongTacGiaoThong(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(ParseSTT), KLCongTac: crRow[te_TongHopKhoiLuong.Text].Value.NumericValue, DonGiaCT: crRow[te_TongHopDonGia.Text].Value.NumericValue);
                                SortId++;
                            }
                            else if (Fomular.Contains("2.DT_tuyen"))
                            {
                                //ParseSTT = Fomular.Replace($"=+'2.DT_tuyen'!$B$", "").Replace($"=+'2.DT_tuyen'!B", "").Replace($"$", "");
                                ParseSTT = Regex.Replace(Fomular, @"2.DT_tuyen|\D", string.Empty);
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                Fcn_UpdateCongTacGiaoThong("2.DT_tuyen", SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(ParseSTT), KLCongTac: crRow[te_TongHopKhoiLuong.Text].Value.NumericValue, DonGiaCT: crRow[te_TongHopDonGia.Text].Value.NumericValue);
                                SortId++;
                            }
                            else if (Fomular.Contains(cbe_DuToan.Text))
                            {
                                //ParseSTT = Fomular.Replace($"=+'{cbe_DuToan.Text}'!$B$", "").Replace($"=+'{cbe_DuToan.Text}'!B", "").Replace($"$", "");
                                ParseSTT = Regex.Replace(Fomular, $@"{cbe_DuToan.Text}|\D", string.Empty);
                                codetheogiaidoan = Guid.NewGuid().ToString();
                                Fcn_UpdateCongTacGiaoThong_DuToan(SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(ParseSTT), KLCongTac: crRow[te_TongHopKhoiLuong.Text].Value.NumericValue, DonGiaCT: crRow[te_TongHopDonGia.Text].Value.NumericValue);
                                SortId++;
                            }
                            else
                            {
                                string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                                DataRow NewRowNhom = dt_NhomCT.NewRow();
                                string CodeNhomCT = Guid.NewGuid().ToString();
                                NewRowNhom["Code"] = CodeNhomCT;
                                NewRowNhom["CodeHangMuc"] = codehangmuc;
                                NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                                NewRowNhom["Ten"] = tennhom;
                                //NewRowNhom["DonGia"] = crRow[te_TongHopDonGia.Text].Value.NumericValue;
                                NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                                //NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                                NewRowNhom["SortId"] = SortIdNhom++;
                                double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaNhom);
                                if (DonGiaNhom > 0)
                                    NewRowNhom["DonGia"] = DonGiaNhom;
                                double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLNhom);
                                if (KLNhom > 0)
                                    NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                                TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                                if (te_TongHopSTTND.Text.HasValue())
                                {
                                    JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                                }
                                JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                                var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                                dt_NhomCT.Rows.Add(NewRowNhom);
                                if (Fomular.Contains(cbb_sheet.Text))
                                {
                                    //ParseSTT = Fomular.Replace("=+", "").Replace($"'{cbb_sheet.Text}'!$A$", "");
                                    ParseSTT = Regex.Replace(Fomular, $@"{cbb_sheet.Text}|\D", string.Empty);
                                    SortId = Fcn_UpdateCongTacCha(cbb_sheet.Text, int.Parse(ParseSTT), SortId, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, CodeNhomCT);
                                }
                                else if (Fomular.Contains("DGTH duong"))
                                {
                                    //ParseSTT = Fomular.Replace($"=+'DGTH duong'!$A$", "").Replace($"='DGTH duong'!$A$", "");
                                    ParseSTT = Regex.Replace(Fomular, @"DGTH duong|\D", string.Empty);
                                    SortId = Fcn_UpdateCongTacCha("DGTH duong", int.Parse(ParseSTT), SortId, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, CodeNhomCT);
                                }
                            }
                        }

                    }
                    else
                        continue;
                }
                else
                {
                    Fomular = crRow[te_TongHopSTT.Text].Formula;
                    //DevExpress.Spreadsheet.Formulas.ParsedExpression pe = workbook.FormulaEngine.Parse(Fomular);
                    //pe.GetRanges().FirstOrDefault().GetReferenceA1();
                    if (Fomular.Contains(cboCTtenSheet.Text))
                    {
                        //ParseSTT = Fomular.Replace("=+", "").Replace($"'{cboCTtenSheet.Text}'!$B$", "");
                        ParseSTT = Regex.Replace(Fomular, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        Fcn_UpdateCongTacGiaoThong(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(ParseSTT), KLCongTac: crRow[te_TongHopKhoiLuong.Text].Value.NumericValue, DonGiaCT: crRow[te_TongHopDonGia.Text].Value.NumericValue);
                        SortId++;
                    }
                    else if (Fomular.Contains("2.DT_tuyen"))
                    {
                        //ParseSTT = Fomular.Replace($"=+'2.DT_tuyen'!$B$", "").Replace($"=+'2.DT_tuyen'!B", "").Replace($"$", "");
                        ParseSTT = Regex.Replace(Fomular, @"2.DT_tuyen|\D", string.Empty);
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        Fcn_UpdateCongTacGiaoThong("2.DT_tuyen", SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(ParseSTT), KLCongTac: crRow[te_TongHopKhoiLuong.Text].Value.NumericValue, DonGiaCT: crRow[te_TongHopDonGia.Text].Value.NumericValue);
                        SortId++;
                    }
                    else if (Fomular.Contains(cbe_DuToan.Text))
                    {
                        //ParseSTT = Fomular.Replace($"=+'{cbe_DuToan.Text}'!$B$", "").Replace($"=+'{cbe_DuToan.Text}'!B", "").Replace($"$", "");
                        ParseSTT = Regex.Replace(Fomular, $@"{cbe_DuToan.Text}|\D", string.Empty);
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        Fcn_UpdateCongTacGiaoThong_DuToan(SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(ParseSTT), KLCongTac: crRow[te_TongHopKhoiLuong.Text].Value.NumericValue, DonGiaCT: crRow[te_TongHopDonGia.Text].Value.NumericValue);
                        SortId++;
                    }
                    else
                    {
                        string tennhom = crRow[te_TongHopTenCT.Text].Value.ToString();
                        DataRow NewRowNhom = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        NewRowNhom["Code"] = CodeNhomCT;
                        NewRowNhom["CodeHangMuc"] = codehangmuc;
                        NewRowNhom["Ten"] = tennhom;
                        //NewRowNhom["DonGia"] = crRow[te_TongHopDonGia.Text].Value.NumericValue;
                        NewRowNhom["DonVi"] = crRow[te_TongHopDonVi.Text].Value.TextValue;
                        //NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = crRow[te_TongHopKhoiLuong.Text].Value.NumericValue;
                        double.TryParse(crRow[te_TongHopDonGia.Text].Value.ToString(), out double DonGiaNhom);
                        if (DonGiaNhom > 0)
                            NewRowNhom["DonGia"] = DonGiaNhom;
                        double.TryParse(crRow[te_TongHopKhoiLuong.Text].Value.ToString(), out double KLNhom);
                        if (KLNhom > 0)
                            NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                        NewRowNhom["SortId"] = SortIdNhom++;

                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = crRow[te_TongHopSTT.Text].Value.ToString();
                        if (te_TongHopSTTND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = crRow[te_TongHopSTTND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                        dt_NhomCT.Rows.Add(NewRowNhom);
                        if (Fomular.Contains(cbb_sheet.Text))
                        {
                            //ParseSTT = Fomular.Replace("=+", "").Replace($"'{cbb_sheet.Text}'!$A$", "");
                            ParseSTT = Regex.Replace(Fomular, $@"{cbb_sheet.Text}|\D", "");
                            SortId = Fcn_UpdateCongTacCha(cbb_sheet.Text, int.Parse(ParseSTT), SortId, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, CodeNhomCT);
                        }
                        else if (Fomular.Contains("DGTH duong"))
                        {
                            //ParseSTT = Fomular.Replace($"=+'DGTH duong'!$A$", "").Replace($"='DGTH duong'!$A$", "");
                            ParseSTT = Regex.Replace(Fomular, @"DGTH duong|\D", string.Empty);
                            SortId = Fcn_UpdateCongTacCha("DGTH duong", int.Parse(ParseSTT), SortId, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, CodeNhomCT);
                        }
                    }

                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }
            spsheet_XemFile.EndUpdate();
            try
            {
            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private long Fcn_UpdateCongTacCha(string TenSheet, int RowCTCha, long SortId, DataTable dtHaoPhi, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, string CodeNhomCongTac)
        {
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[TenSheet];
            int LastLine = sheetThongTin.GetUsedRange().BottomRowIndex;
            for (int i = RowCTCha; i <= LastLine; i++)
            {
                Row Crow = sheetThongTin.Rows[i];
                if (Crow[txt_STT.Text].Value.NumericValue > 0)
                    break;
                else
                {
                    double Mahieu = Crow[txt_macongtac.Text].Value.NumericValue;
                    if (Mahieu <= 0)
                        continue;
                    else
                    {
                        string formula = Crow[txt_macongtac.Text].Formula;
                        if (formula.Contains(cboCTtenSheet.Text))
                        {
                            //string Index = formula.Replace($"=+'{cboCTtenSheet.Text}'!$B$", "").Replace($"=+'{cboCTtenSheet.Text}'!B", "");
                            string Index = Regex.Replace(formula, $@"{cboCTtenSheet.Text}|\D", string.Empty);
                            string codetheogiaidoan = Guid.NewGuid().ToString();
                            Fcn_UpdateCongTacGiaoThong(cboCTtenSheet.Text, SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), CodeNhomCongTac);
                            SortId++;
                        }
                        else if (formula.Contains("2.DT_tuyen"))
                        {
                            //string Index = formula.Replace($"=+'2.DT_tuyen'!$B$", "").Replace($"=+'2.DT_tuyen'!B", "").Replace($"$", "");
                            string Index = Regex.Replace(formula, @"2.DT_tuyen|\D", string.Empty);
                            string codetheogiaidoan = Guid.NewGuid().ToString();
                            Fcn_UpdateCongTacGiaoThong("2.DT_tuyen", SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), CodeNhomCongTac);
                            SortId++;
                        }
                        else
                        {
                            //string Index = formula.Replace($"=+'{cbe_DuToan.Text}'!$B$", "").Replace($"=+'{cbe_DuToan.Text}'!B", "");
                            string Index = Regex.Replace(formula, $@"{cbe_DuToan.Text}|\D", string.Empty);
                            string codetheogiaidoan = Guid.NewGuid().ToString();
                            Fcn_UpdateCongTacGiaoThong_DuToan(SortId, codetheogiaidoan, dtHaoPhi, dt, dt_DLG, dt_congtactheogiaidoan, int.Parse(Index), CodeNhomCongTac);
                            SortId++;
                        }
                    }
                }
            }
            return SortId;
        }
        private void fcn_updateHaophiGiaoThongTruongSon(string TenSheet, DataTable dtHaoPhi, string vl, string nc, string mtc, string tenvattu, string donvi, int RowHaoPhi, string codecongtactheogiaidoan, string mahieu)
        {
            string codevatu = "";
            Dictionary<string, string> checkvt = new Dictionary<string, string>()
             {
                {"Vật liệu",vl},
                {"Nhân công",nc},
                {"Máy thi công",mtc}
             };
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[TenSheet];
            if (sheetThongTin.Rows[RowHaoPhi - 1][txtHPVTMaCongTac.Text].Value.IsEmpty)
            {
                for (int i = RowHaoPhi - 2; i >= 0; i--)
                {
                    Row crRow = sheetThongTin.Rows[i];
                    string MCT = crRow[txtHPVTMaCongTac.Text].Value.TextValue;
                    if (!string.IsNullOrEmpty(MCT))
                    {
                        RowHaoPhi = i + 1;
                        break;
                    }
                }
            }
            string loaivattu = sheetThongTin.Rows[RowHaoPhi][txtHPVTTenCongTac.Text].Value.ToString();
            if (loaivattu.Contains("Vật liệu VAT"))
            {
                loaivattu = "Vật liệu";
            }
            if (!LIST_loaivattu.Contains(loaivattu))
            {
                foreach (var item in checkvt)
                {
                    if (double.TryParse(item.Value, out double kl))
                    {
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = mahieu;
                        dt_vattu["VatTu"] = tenvattu;
                        dt_vattu["DonVi"] = donvi;
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = kl;
                        dt_vattu["LoaiVatTu"] = Fcn_CheckLoaiVL(item.Key);
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
                checkvt.Clear();
            }
            int end = sheetThongTin.GetUsedRange().BottomRowIndex;
            for (int i = RowHaoPhi + 1; i <= end; i++)
            {
                Row crRow = sheetThongTin.Rows[i];
                string checkloaivattu = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                if (string.IsNullOrEmpty(crRow[te_MaVatTuHaoPhi.Text].Value.TextValue))
                {
                    if (Fcn_CheckLoaiVL(checkloaivattu) == string.Empty)
                    {
                        break;
                    }
                    else
                    {
                        if (checkloaivattu.Contains("Vật liệu VAT") || checkloaivattu == "Cộng")
                        {
                            continue;
                        }
                        if (LIST_loaivattu.Contains(checkloaivattu) && checkloaivattu != loaivattu)
                            loaivattu = checkloaivattu;
                    }
                }
                else
                {
                    string LoaiVL = Fcn_CheckLoaiVL(loaivattu);
                    codevatu = Guid.NewGuid().ToString();
                    DataRow dt_vattu = dtHaoPhi.NewRow();
                    dt_vattu["Code"] = codevatu;
                    dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                    dt_vattu["MaVatLieu"] = crRow[te_MaVatTuHaoPhi.Text].Value.ToString();
                    dt_vattu["VatTu"] = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                    dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                    double.TryParse(crRow[txtHPVTDonGia.Text].Value.ToString(), out double DonGiaCon);
                    dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = DonGiaCon;
                    dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dt_vattu["LoaiVatTu"] = LoaiVL;
                    double.TryParse(crRow[txtHPVTHS.Text].Value.ToString(), out double HeSo);
                    dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = HeSo == 0 ? 1 : HeSo;
                    double.TryParse(crRow[txtHPVTDMCHUAN.Text].Value.ToString(), out double DinhMuc);
                    dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = DinhMuc == 0 ? 1 : DinhMuc;
                    dtHaoPhi.Rows.Add(dt_vattu);
                }
            }
        }
        private void fcn_updateHaophiKienGiangCaMau(int end,string TenSheet,string ColDonGia, DataTable dtHaoPhi, int RowHaoPhi, string codecongtactheogiaidoan)
        {
            string codevatu = string.Empty;
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[TenSheet];
            Row crRow = sheetThongTin.Rows[RowHaoPhi];
            string DonVi = string.Empty, MaVL = string.Empty, TenVatTu = string.Empty;
            MaVL = crRow[txtHPVTMaCongTac.Text].Value.TextValue;
            double DonGiaCon = 0;
            if (!string.IsNullOrEmpty(MaVL) &&
                    !string.IsNullOrEmpty(crRow[txtHPVTSTT.Text].Value.TextValue))
            {
                crRow = sheetThongTin.Rows[RowHaoPhi-1];
                MaVL = crRow[txtHPVTMaCongTac.Text].Value.TextValue;
                codevatu = Guid.NewGuid().ToString();
                DataRow dt_vattu = dtHaoPhi.NewRow();
                dt_vattu["Code"] = codevatu;
                dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                dt_vattu["MaVatLieu"] = MaVL;
                dt_vattu["VatTu"] = TenVatTu;
                dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                double.TryParse(crRow[ColDonGia].Value.ToString(), out DonGiaCon);
                dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = DonGiaCon;
                dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                dt_vattu["LoaiVatTu"] = "Vật liệu";
                double.TryParse(crRow[txtHPVTHS.Text].Value.ToString(), out double HS);
                dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = HS == 0 ? 1 : HS;
                double.TryParse(crRow[txtHPVTDMCHUAN.Text].Value.ToString(), out double DinhMuc);
                dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = DinhMuc == 0 ? 1 : DinhMuc;
                dtHaoPhi.Rows.Add(dt_vattu);
                return;
            }
            string loaivattu = sheetThongTin.Rows[RowHaoPhi][txtHPVTTenCongTac.Text].DisplayText.ToString();
            int STTVL = 1;
            if (loaivattu.Contains("A - Vật liệu"))
            {
                loaivattu = "Vật liệu";
            }
            else if (loaivattu.Contains("- Máy thi công"))
                loaivattu = "Máy thi công";
            
            for (int i = RowHaoPhi; i <= end; i++)
            {
                crRow = sheetThongTin.Rows[i];
                DonVi = crRow[txtHPVTDonVi.Text].Value.ToString();
                MaVL = crRow[txtHPVTMaCongTac.Text].Value.ToString();
                if (string.IsNullOrEmpty(MaVL) ||!string.IsNullOrEmpty(DonVi))
                {
                    TenVatTu = crRow[txtHPVTTenCongTac.Text].DisplayText.ToString();
                    if (string.IsNullOrEmpty(TenVatTu))
                    {
                        continue;
                    }
                    else if (!string.IsNullOrEmpty(MaVL) &&
    !string.IsNullOrEmpty(crRow[txtHPVTSTT.Text].Value.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(MaVL)&&DonVi.Contains("công"))
                        {
                            loaivattu = "Nhân công";
                            goto Label;
                        }
                        string checkloaivattu = Fcn_CheckLoaiVLCaMauKienGiang(TenVatTu);
                        if (LIST_loaivattu.Contains(checkloaivattu) && checkloaivattu != loaivattu)
                        {
                            loaivattu = checkloaivattu;
                            continue;
                        }
                        Label:
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        if (string.IsNullOrEmpty(MaVL))
                            dt_vattu["MaVatLieu"] = STTVL++;
                        else
                            dt_vattu["MaVatLieu"] = MaVL;
                        dt_vattu["VatTu"] = TenVatTu;
                        dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                        double.TryParse(crRow[ColDonGia].Value.ToString(), out DonGiaCon);
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = DonGiaCon;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["LoaiVatTu"] = loaivattu;
                        double.TryParse(crRow[txtHPVTHS.Text].Value.ToString(), out double HS);
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = HS==0?1:HS;
                        double.TryParse(crRow[txtHPVTDMCHUAN.Text].Value.ToString(), out double DinhMuc);
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = DinhMuc == 0 ? 1 : DinhMuc;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
                else if (!string.IsNullOrEmpty(MaVL) &&
    !string.IsNullOrEmpty(crRow[txtHPVTSTT.Text].Value.ToString()))
                {
                    break;
                }
            }
        }
        private void fcn_updateHaophiKienGiangCanTho(string TenSheet, DataTable dtHaoPhi, int RowHaoPhi, string codecongtactheogiaidoan, string ColDonGia, bool AVERAGE = false)
        {
            string codevatu = "";
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[TenSheet];
            string loaivattu = sheetThongTin.Rows[RowHaoPhi][txtHPVTTenCongTac.Text].Value.ToString();
            int STTVL = 1;
            if (loaivattu.Contains("Vật liệu VAT"))
            {
                loaivattu = "Vật liệu";
            }
            else if (loaivattu.Contains("Xe máy"))
                loaivattu = "Máy thi công";
            int end = sheetThongTin.GetUsedRange().BottomRowIndex;
            double DonGiaCon = 0;
            for (int i = RowHaoPhi + 1; i <= end; i++)
            {
                Row crRow = sheetThongTin.Rows[i];
                if (string.IsNullOrEmpty(crRow[txtHPVTMaCongTac.Text].Value.TextValue))
                {
                    string TenVatTu = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                    if (string.IsNullOrEmpty(TenVatTu))
                    {
                        continue;
                    }
                    else
                    {
                        string checkloaivattu = Fcn_CheckLoaiVL(TenVatTu);
                        if (LIST_loaivattu.Contains(checkloaivattu) && checkloaivattu != loaivattu)
                        {
                            loaivattu = checkloaivattu;
                            continue;
                        }
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = STTVL++;
                        dt_vattu["VatTu"] = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                        dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                        if (AVERAGE)
                        {
                            double.TryParse(crRow["L"].Value.ToString(), out double DonGiaCon1);
                            double.TryParse(crRow["J"].Value.ToString(), out double DonGiaCon2);
                            DonGiaCon = (DonGiaCon1 + DonGiaCon2) / 2;
                        }
                        else
                            double.TryParse(crRow[ColDonGia].Value.ToString(), out DonGiaCon);
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = DonGiaCon;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["LoaiVatTu"] = loaivattu;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                        double.TryParse(crRow[txtHPVTDMCHUAN.Text].Value.ToString(), out double DinhMuc);
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = DinhMuc == 0 ? 1 : DinhMuc;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void fcn_updateHaophiGiaoThongTruongSon2(string NameSheet, DataTable dtHaoPhi, string vl, string nc, string mtc, string tenvattu, string donvi, int RowHaoPhi, string codecongtactheogiaidoan, string mahieu)
        {
            string codevatu = "";
            Dictionary<string, string> checkvt = new Dictionary<string, string>()
             {
                {"Vật liệu",vl},
                {"Nhân công",nc},
                {"Máy thi công",mtc}
             };
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[NameSheet];
            if (sheetThongTin.Rows[RowHaoPhi - 1][txtHPVTMaCongTac.Text].Value.ToString() == "0" || sheetThongTin.Rows[RowHaoPhi - 1][txtHPVTMaCongTac.Text].Value.IsEmpty)
            {
                for (int i = RowHaoPhi - 2; i >= 0; i--)
                {
                    Row crRow = sheetThongTin.Rows[i];
                    string MCT = crRow[txtHPVTMaCongTac.Text].Value.TextValue;
                    if (!string.IsNullOrEmpty(MCT))
                    {
                        RowHaoPhi = i + 1;
                        break;
                    }
                }
            }
            string loaivattu = sheetThongTin.Rows[RowHaoPhi][txtHPVTTenCongTac.Text].Value.ToString();
            if (loaivattu.Contains("Vật liệu VAT"))
            {
                loaivattu = "Vật liệu";
            }
            if (!LIST_loaivattu.Contains(loaivattu))
            {
                foreach (var item in checkvt)
                {
                    if (double.TryParse(item.Value, out double kl))
                    {
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = mahieu;
                        dt_vattu["VatTu"] = tenvattu;
                        dt_vattu["DonVi"] = donvi;
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = kl;
                        dt_vattu["LoaiVatTu"] = Fcn_CheckLoaiVL(item.Key);
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
                checkvt.Clear();
            }
            int end = sheetThongTin.GetUsedRange().BottomRowIndex;
            for (int i = RowHaoPhi + 1; i <= end; i++)
            {
                Row crRow = sheetThongTin.Rows[i];
                string checkloaivattu = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                if (crRow[txtHPVTMaCongTac.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "0")
                {
                    break;
                }
                else
                {
                    if (checkloaivattu.Contains("Vật liệu VAT"))
                    {
                        continue;
                    }

                    if (LIST_loaivattu.Contains(checkloaivattu) && checkloaivattu != loaivattu)
                        loaivattu = checkloaivattu;
                    else
                    {
                        string LoaiVL = Fcn_CheckLoaiVL(loaivattu);
                        string MaVT = crRow[te_MaVatTuHaoPhi.Text].Value.ToString();
                        if (string.IsNullOrEmpty(MaVT))
                            continue;
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = MaVT;
                        dt_vattu["VatTu"] = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                        dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                        double.TryParse(crRow[txtHPVTDonGia.Text].Value.NumericValue.ToString(), out double DonGia);
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = DonGia;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["LoaiVatTu"] = LoaiVL;
                        double.TryParse(crRow[txtHPVTDMCHUAN.Text].Value.NumericValue.ToString(), out double DinhMuc);
                        double.TryParse(crRow[txtHPVTHS.Text].Value.NumericValue.ToString(), out double HeSo);
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = HeSo == 0 ? 1 : HeSo;
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = DinhMuc == 1 ? 1 : DinhMuc;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
        }
        private void fcn_updateHaophiGiaoThong(string TenSheet, DataTable dtHaoPhi, string vl, string nc, string mtc, string tenvattu, string donvi, int RowHaoPhi, string codecongtactheogiaidoan, string mahieu)
        {
            string codevatu = "";
            string formula = "";
            int rowindex = 0;
            double dongia = 0;
            Dictionary<string, string> checkvt = new Dictionary<string, string>()
             {
                {"Vật liệu",vl},
                {"Nhân công",nc},
                {"Máy thi công",mtc}
             };
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[TenSheet];
            string Fomular = "";
            bool Try = false;
            string DonVi = "";
            string loaivattu = sheetThongTin.Rows[RowHaoPhi][txtHPVTTenCongTac.Text].Value.ToString();
            if (loaivattu.Contains("Vật liệu VAT"))
            {
                loaivattu = "Vật liệu";
            }
            if (!LIST_loaivattu.Contains(loaivattu))
            {
                foreach (var item in checkvt)
                {
                    if (double.TryParse(item.Value, out double kl))
                    {
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = mahieu;
                        dt_vattu["VatTu"] = tenvattu;
                        dt_vattu["DonVi"] = donvi;
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = kl;
                        dt_vattu["LoaiVatTu"] = Fcn_CheckLoaiVL(item.Key);
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
                checkvt.Clear();
            }
            int end = sheetThongTin.GetUsedRange().BottomRowIndex;
            for (int i = RowHaoPhi + 1; i <= end; i++)
            {
                Row crRow = sheetThongTin.Rows[i];
                string checkloaivattu = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                if (crRow[txtHPVTSTT.Text].Value.NumericValue <= 0)
                {
                    break;
                }
                else
                {
                    if (checkloaivattu.Contains("Vật liệu VAT") || checkloaivattu == "Cộng")
                    {
                        continue;
                    }

                    if (LIST_loaivattu.Contains(checkloaivattu) && checkloaivattu != loaivattu)
                        loaivattu = checkloaivattu;
                    else
                    {
                        //if (crRow[te_MaVatTuHaoPhi.Text].Value.ToString() == "" && crRow[txtHPVTSTT.Text].Value.ToString() == "" || crRow[te_MaVatTuHaoPhi.Text].Value.IsNumeric)
                        //{
                        //    break;
                        //}
                        //if (txtHPVTDonGia.Text != "")
                        //{
                        //    dongia = crRow[txtHPVTDonGia.Text].Value.IsNumeric ? crRow[txtHPVTDonGia.Text].Value.NumericValue : 0;
                        //    goto Label;
                        //}
                        //formula = crRow[txtHPVTMaCongTac.Text].Formula.Replace($"='{cbe_GiaThang.Text}'!{txtHPVTMaCongTac.Text}", "");
                        //if (formula.StartsWith("$"))
                        //    formula = formula.Replace("$", "");
                        //else if (formula == "")
                        //{
                        //    dongia = 0;
                        //    goto Label;
                        //}
                        //rowindex = int.Parse(formula) - 1;
                        //dongia = workbook.Worksheets[cbe_GiaThang.Text].Rows[rowindex][te_GiaThangDonGia.Text].Value.IsNumeric ? workbook.Worksheets[cbe_GiaThang.Text].Rows[rowindex][te_GiaThangDonGia.Text].Value.NumericValue : 0;
                        //Label:
                        string LoaiVL = Fcn_CheckLoaiVL(loaivattu);
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = crRow[txtHPVTMaCongTac.Text].Value.ToString();
                        dt_vattu["VatTu"] = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                        dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = crRow[txtHPVTDonGia.Text].Value.NumericValue;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["LoaiVatTu"] = LoaiVL;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = crRow[txtHPVTHS.Text].Value.IsEmpty ? 1 : crRow[txtHPVTHS.Text].Value.NumericValue;
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.IsEmpty ? 1 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
        }
        private void Fcn_UpdateCongTacGiaoThong_DuToan(long SortId, string codetheogiaidoan, DataTable dtHaoPhi, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, int RowDuToan, string CodeNhomCongTac = default, string CodeTuyen = default, double KLCongTac = -1, double DonGiaCT = -1)
        {
            Row crRow = spsheet_XemFile.Document.Worksheets[cbe_DuToan.Text].Rows[RowDuToan - 1];
            string tencongtac = crRow[te_DuToan_TenCongTac.Text].Value.ToString();
            string MaHieu = crRow[te_DuToan_MaHieu.Text].Value.ToString();
            string DoViThau = crRow[te_DuToanDonVi.Text].Value.TextValue;
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = te_DuToanLyTrinh.Text.HasValue() ? crRow[te_DuToanLyTrinh.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            if (KLCongTac == -1)
                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = crRow[te_DuToanKL.Text].Value.ToString() == "" ? 0 : crRow[te_DuToanKL.Text].Value.NumericValue;
            else
                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCongTac;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = DoViThau;
            if (CodeTuyen != default)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            if (te_DuToanDonGiaVL.Text.HasValue())
            {
                long.TryParse(crRow[te_DuToanDonGiaVL.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (te_DuToanDonGiaNC.Text.HasValue())
            {
                long.TryParse(crRow[te_DuToanDonGiaNC.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (te_DuToanDonGiaMay.Text.HasValue())
            {
                long.TryParse(crRow[te_DuToanDonGiaMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                if (KLDV > 0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            double DonGia = 0;
            if (DonGiaCT != -1)
            {
                DonGia = DonGiaCT;
            }
            else if (te_DuToanDonGiaHD.Text.HasValue())
            {
                DonGia = crRow[te_DuToanDonGiaHD.Text].Value.NumericValue;
                goto Label;
            }

            Label:
            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_DuToanKHBegin.Text.HasValue() ? (crRow[te_DuToanKHBegin.Text].Value.IsDateTime ? crRow[te_DuToanKHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_DuToanBegin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_DuToanKHEnd.Text.HasValue() ? (crRow[te_DuToanKHEnd.Text].Value.IsDateTime ? crRow[te_DuToanKHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_DuToanEnd.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            //string Formula = crRow[te_DuToan_STT.Text].Formula.Replace($"='DGCT (CPK)'!$B$", "").Replace($"=+'DGCT (CPK)'!$B$", "");
            string Formula = Regex.Replace(crRow[te_DuToan_STT.Text].Formula, @"DGCT \(CPK\)|\D", string.Empty);
            fcn_updateHaophiGiaoThong("DGCT (CPK)", dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(),
                tencongtac, DoViThau, int.Parse(Formula), codetheogiaidoan, MaHieu);
        }
        private void Fcn_UpdateCongTacGiaoThongTruongSon1(string TenSheet, long SortId, string codetheogiaidoan, DataTable dtHaoPhi, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, int RowDuToan, double KLCongTac, double DonGia, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            Row crRow = spsheet_XemFile.Document.Worksheets[TenSheet].Rows[RowDuToan - 1];
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
            string DoViThau = crRow[txtCTDonVi.Text].Value.TextValue;
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCongTac;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = DoViThau;
            if (CodeTuyen != default)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            if (txtCTDGVatLieu.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (txtCTDGNhanCong.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (txtCTDGMay.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                if (KLDV > 0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            string Text = crRow[DonGiaHD.Text].Formula;
            if (Text.Contains("*"))
            {
                int pos = Text.IndexOf("*");
                Text = Text.Remove(pos);
            }
            string Formula = Regex.Replace(Text, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);
            fcn_updateHaophiGiaoThongTruongSon(cboHPVTtenSheet.Text, dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(),
                tencongtac, DoViThau, int.Parse(Formula), codetheogiaidoan, MaHieu);
        }
        private void Fcn_UpdateCongTacGiaoThongTruongSon(string TenSheet, long SortId, string codetheogiaidoan, DataTable dtHaoPhi, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, int RowDuToan, double KLCongTac, double DonGia, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            Row crRow = spsheet_XemFile.Document.Worksheets[TenSheet].Rows[RowDuToan - 1];
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
            string DoViThau = crRow[txtCTDonVi.Text].Value.TextValue;
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCongTac;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = DoViThau;
            if (CodeTuyen != default)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            if (txtCTDGVatLieu.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (txtCTDGNhanCong.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (txtCTDGMay.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                if (KLDV > 0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);

            string Formula = Regex.Replace(crRow[DonGiaHD.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);

            fcn_updateHaophiGiaoThongTruongSon(cboHPVTtenSheet.Text, dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(),
                tencongtac, DoViThau, int.Parse(Formula), codetheogiaidoan, MaHieu);
        }
        private void Fcn_UpdateCongTacGiaoThong(string TenSheet, long SortId, string codetheogiaidoan, DataTable dtHaoPhi, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, int RowDuToan, string CodeNhomCongTac = default, string CodeTuyen = default, double KLCongTac = -1, double DonGiaCT = -1)
        {
            Row crRow = spsheet_XemFile.Document.Worksheets[TenSheet].Rows[RowDuToan - 1];
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
            double KL = crRow[txtCTKhoiLuong.Text].Value.NumericValue;
            string DoViThau = crRow[txtCTDonVi.Text].Value.TextValue;
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            if (KLCongTac == -1)
                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = crRow[txtCTKhoiLuong.Text].Value.ToString() == "" ? 0 : crRow[txtCTKhoiLuong.Text].Value.NumericValue;
            else
                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCongTac;
            //dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = DoViThau;
            if (CodeTuyen != default)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            if (txtCTDGVatLieu.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (txtCTDGNhanCong.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (txtCTDGMay.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                if (KLDV > 0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            double DonGia = 0;
            if (DonGiaCT != -1)
            {
                DonGia = DonGiaCT;
            }
            else if (DonGiaHD.Text.HasValue())
            {
                DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                goto Label;
            }
            Label:
            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            //string Formula = crRow[txt_congtrinh_stt.Text].Formula.Replace($"='{cboHPVTtenSheet.Text}'!$B$","").Replace($"=+'{cboHPVTtenSheet.Text}'!$B$", "");
            string Formula = Regex.Replace(crRow[txt_congtrinh_stt.Text].Formula, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);
            fcn_updateHaophiGiaoThong(cboHPVTtenSheet.Text, dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(),
                tencongtac, DoViThau, int.Parse(Formula), codetheogiaidoan, MaHieu);
        }
        private void fcn_truyendataExxcel_DocThuCongHaoPhi(Dictionary<Control, string> dic, int firstLine, int lastLine, TextEdit stt)
        {
            foreach (var item in dic)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            CellRange usedRange = workbook.Worksheets[cboHPVTtenSheet.Text].GetUsedRange();
            int lastline_vl = usedRange.RowCount;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            fcn_kiemtracongtrinh_hangmuc(firstLine);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            if (tencongtrinh == "")
            {
                firstLine = dic_congtrinh.First().Key + 2;
                fcn_kiemtracongtrinh_hangmuc(firstLine);
            }
            int Index = rg_LayDuLieu.SelectedIndex;
            if (Index > 0)
                codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";
            for (int i = firstLine; i <= lastLine; i++)
            {
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                mahieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{mahieu}_{TenCongTac}");
                if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (Index > 0)
                        continue;
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    if (Te_DonGiaNhom.Text.HasValue())
                    {
                        double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                        if (DonGiaNhom > 0)
                            NewRow["DonGia"] = DonGiaNhom;
                    }
                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                    if (KLNhom > 0)
                        NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;

                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                    if (txt_congtrinh_sttND.Text.HasValue())
                    {
                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                    }
                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                    NewRow["GhiChuBoSungJson"] = encryptedStr;
                    NewRow["SortId"] = SortIdNhom++;
                    dt_NhomCT.Rows.Add(NewRow);
                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                            Mahieu.ToUpper() == "THM" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                        {
                            i = j - 1;
                            break;

                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            RowDuThau = Fcn_UpdateCongTacThuCongHaoPhi(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
                            SortId++;
                            int End = fcn_updatenhomdiendaiLayHaoPhi(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiThuCongLayHaoPhi(dtHaoPhi, "", "", "", CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                    Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, j, CrowCTNhom[txtCTKhoiLuong.Text].Value.NumericValue);
                            else
                                RowHaoPhi = fcn_updateHaophiThuCongLayHaoPhi(dtHaoPhi, "", "", "", CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                    Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, j, CrowCTNhom[txtCTKhoiLuong.Text].Value.NumericValue);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }

                }
                else if (mahieu == te_PhanTuyen.Text)
                {
                    string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_Tuyen.NewRow();
                    string CodeTuyen = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeTuyen;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = TenTuyen;
                    NewRow["SortId"] = SortIdTuyen++;
                    dt_Tuyen.Rows.Add(NewRow);

                    for (int j = i + 1; j <= lastLine; j++)
                    {
                        Row CrowCTNhom = ws.Rows[j];
                        string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                        if (Mahieu == te_PhanTuyen.Text || Mahieu.ToUpper() == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            i = j - 1;
                            break;

                        }
                        else if (Mahieu == te_Nhom.Text)
                        {
                            string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRowNhom = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRowNhom["Code"] = CodeNhomCT;
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
                            NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                            NewRowNhom["SortId"] = SortIdNhom++;
                            NewRowNhom["DonVi"] = CrowCTNhom[txtCTDonVi.Text].Value.TextValue;
                            if (Te_DonGiaNhom.Text.HasValue())
                            {
                                double.TryParse(CrowCTNhom[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                if (DonGiaNhom > 0)
                                    NewRowNhom["DonGia"] = DonGiaNhom;
                            }
                            double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                            if (KLNhom > 0)
                                NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = CrowCTNhom[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                            dt_NhomCT.Rows.Add(NewRowNhom);
                            for (int k = j + 1; k <= lastLine; k++)
                            {
                                Row CrowCTNhomNew = ws.Rows[k];
                                Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                    Mahieu.ToUpper() == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                                {
                                    j = k - 1;
                                    i = j - 1;
                                    break;

                                }
                                else
                                {
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    RowDuThau = Fcn_UpdateCongTacThuCongHaoPhi(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    int End = fcn_updatenhomdiendaiLayHaoPhi(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (double.TryParse(CrowCTNhomNew[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                        RowHaoPhi = fcn_updateHaophiThuCongLayHaoPhi(dtHaoPhi, "", "", "", CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(),
                                            CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue, k, CrowCTNhomNew[txtCTKhoiLuong.Text].Value.NumericValue);
                                    else
                                        RowHaoPhi = fcn_updateHaophiThuCongLayHaoPhi(dtHaoPhi, "", "", "", CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue, k, CrowCTNhomNew[txtCTKhoiLuong.Text].Value.NumericValue);
                                    if (End != 0)
                                    {
                                        j = End;
                                        k = End - 1;
                                        i = j - 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            RowDuThau = Fcn_UpdateCongTacThuCongHaoPhi(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
                            SortId++;
                            int End = fcn_updatenhomdiendaiLayHaoPhi(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiThuCongLayHaoPhi(dtHaoPhi, "", "", "", TenVL, DonVi, lastline_vl, codetheogiaidoan,
                                    Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, j, CrowCTNhom[txtCTKhoiLuong.Text].Value.NumericValue);
                            else
                                RowHaoPhi = fcn_updateHaophiThuCongLayHaoPhi(dtHaoPhi, "", "", "", TenVL, DonVi, lastline_vl, codetheogiaidoan,
                                    Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, j, CrowCTNhom[txtCTKhoiLuong.Text].Value.NumericValue);
                            if (End != 0)
                            {
                                i = End;
                                j = End - 1;
                            }
                        }
                    }


                }
                else if (!Int32.TryParse(crRow[stt.Text].Value.ToString(), out int so))
                {
                    if (crRow[txtCTMaDuToan.Text].Value.ToString().ToUpper() == "THM")
                    {
                        foreach (var item in dic_congtrinh)
                        {
                            if (i < item.Key)
                            {
                                i = item.Key + 2;
                                break;
                            }
                        }
                        string mahieunext = ws.Rows[i + 1][txtCTMaDuToan.Text].Value.ToString();
                        if (mahieunext.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                            fcn_kiemtracongtrinh_hangmuc(i + 2, true);
                        else
                            fcn_kiemtracongtrinh_hangmuc(i);
                        continue;
                    }
                }
                else
                {
                    if (mahieu != "")
                    {
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        RowDuThau = Fcn_UpdateCongTacThuCongHaoPhi(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;

                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiThuCongLayHaoPhi(dtHaoPhi, "", "", "", crRow[txtCTTenCongTac.Text].Value.ToString(),
                                crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi,
                                crRow[txt_congtrinh_stt.Text].Value.NumericValue, i, crRow[txtCTKhoiLuong.Text].Value.NumericValue);
                        else
                            RowHaoPhi = fcn_updateHaophiThuCongLayHaoPhi(dtHaoPhi, "", "", "", crRow[txtCTTenCongTac.Text].Value.ToString(),
                                crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi,
                                crRow[txt_congtrinh_stt.Text].Value.NumericValue, i, crRow[txtCTKhoiLuong.Text].Value.NumericValue);
                        int End = fcn_updatenhomdiendaiLayHaoPhi(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }

            spsheet_XemFile.EndUpdate();

            try
            {

            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private void fcn_truyendataExxcel_congtrinh360(Dictionary<Control, string> dic, int firstLine, int lastLine, TextEdit stt)
        {
            foreach (var item in dic)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_HopDongDuLieuGoc}";
            DataTable dt_DLG = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            fcn_kiemtracongtrinh_hangmuc(firstLine);
            bool _ChecHM = false;
            if (tencongtrinh == "")
            {
                firstLine = dic_congtrinh.First().Key + 2;
                fcn_kiemtracongtrinh_hangmuc(firstLine);
            }
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_DLG.Clear();
                dt_NhomCT.Clear();
                string mahieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                if (!Int32.TryParse(crRow[stt.Text].Value.ToString(), out int so))
                {
                    if (mahieu.ToUpper() == "HTGĐ")
                    {
                        foreach (var item in dic_congtrinh)
                        {
                            if (i < item.Key)
                            {
                                i = item.Key + 2;
                                break;
                            }
                        }
                        fcn_kiemtracongtrinh_hangmuc(i);
                    }
                    else if (mahieu.ToUpper() == "HTHM")
                    {
                        _ChecHM = true;
                        foreach (var item in dic_hangmuc)
                        {
                            if (i < item.Key)
                            {
                                i = item.Key + 2;
                                _ChecHM = false;
                                break;
                            }
                        }
                        if (_ChecHM)
                        {
                            i = i + 1;
                            continue;
                        }

                        fcn_kiemtracongtrinh_hangmuc(i);
                        i = i - 2;
                    }
                    if (mahieu == te_Nhom.Text)
                    {
                        string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                        DataRow NewRow = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeNhomCT;
                        NewRow["CodeHangMuc"] = codehangmuc;
                        NewRow["Ten"] = tennhom;
                        NewRow["SortId"] = SortIdNhom++;
                        if (Te_DonGiaNhom.Text.HasValue())
                        {
                            double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                            if (DonGiaNhom > 0)
                                NewRow["DonGia"] = DonGiaNhom;
                        }
                        double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                        if (KLNhom > 0)
                            NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = KLNhom;
                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                        JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                        if (txt_congtrinh_sttND.Text.HasValue())
                        {
                            JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                        }
                        JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                        NewRow["GhiChuBoSungJson"] = encryptedStr;
                        dt_NhomCT.Rows.Add(NewRow);
                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            Row CrowCTNhom = ws.Rows[j];
                            string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                            if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                Mahieu == "THM" || Mahieu == MyConstant.CONST_TYPE_HANGMUC || Mahieu == "" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text)
                            {
                                i = j - 1;
                                break;

                            }
                            else
                            {
                                string codetheogiaidoan = Guid.NewGuid().ToString();
                                Fcn_UpdateCongTac360(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeNhomCT);
                                SortId++;
                                int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            }
                        }

                    }
                    else if (mahieu == te_PhanTuyen.Text)
                    {
                        string TenTuyen = crRow[txtCTTenCongTac.Text].Value.ToString();
                        DataRow NewRow = dt_Tuyen.NewRow();
                        string CodeTuyen = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeTuyen;
                        NewRow["CodeHangMuc"] = codehangmuc;
                        NewRow["Ten"] = TenTuyen;
                        NewRow["SortId"] = SortIdTuyen++;
                        dt_Tuyen.Rows.Add(NewRow);

                        for (int j = i + 1; j <= lastLine; j++)
                        {
                            Row CrowCTNhom = ws.Rows[j];
                            string Mahieu = CrowCTNhom[txtCTMaDuToan.Text].Value.ToString();
                            if (Mahieu == te_PhanTuyen.Text || Mahieu.ToUpper() == "THM" || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                            {
                                i = j - 1;
                                break;

                            }
                            else if (Mahieu == te_Nhom.Text)
                            {
                                string tennhom = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                                DataRow NewRowNhom = dt_NhomCT.NewRow();
                                string CodeNhomCT = Guid.NewGuid().ToString();
                                NewRowNhom["Code"] = CodeNhomCT;
                                NewRowNhom["CodeHangMuc"] = codehangmuc;
                                NewRowNhom["Ten"] = tennhom;
                                NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                                NewRowNhom["SortId"] = SortIdNhom++;
                                if (Te_DonGiaNhom.Text.HasValue())
                                {
                                    double.TryParse(CrowCTNhom[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                    if (DonGiaNhom > 0)
                                        NewRowNhom["DonGia"] = DonGiaNhom;
                                }
                                double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double KLNhom);
                                if (KLNhom > 0)
                                    NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = KLNhom;
                                TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                JsonGhiChu.STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.ToString();
                                if (txt_congtrinh_sttND.Text.HasValue())
                                {
                                    JsonGhiChu.STTND = CrowCTNhom[txt_congtrinh_sttND.Text].Value.ToString();
                                }
                                JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                                var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                                dt_NhomCT.Rows.Add(NewRowNhom);
                                for (int k = j + 1; k <= lastLine; k++)
                                {
                                    Row CrowCTNhomNew = ws.Rows[k];
                                    Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                    if (Mahieu == te_Nhom.Text || Mahieu == te_PhanTuyen.Text ||
                                        Mahieu.ToUpper() == "THM" || Mahieu == te_EndNhom.Text || Mahieu == te_EndTuyen.Text || Mahieu == "" || Mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                                    {
                                        j = k - 1;
                                        i = j - 1;
                                        break;

                                    }
                                    else
                                    {
                                        string codetheogiaidoan = Guid.NewGuid().ToString();
                                        Fcn_UpdateCongTac360(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhomNew, k, CodeNhomCT, CodeTuyen: CodeTuyen);
                                        SortId++;
                                        int End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    }
                                }
                            }
                            else
                            {
                                string codetheogiaidoan = Guid.NewGuid().ToString();
                                Fcn_UpdateCongTac360(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, CrowCTNhom, j, CodeTuyen: CodeTuyen);
                                SortId++;
                                int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            }
                        }


                    }
                    if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                    {
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = crRow[txtCTTenCongTac.Text].Value.ToString(); ;
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                        continue;
                    }
                }
                else
                {
                    if (mahieu != "")
                    {
                        string codetheogiaidoan = Guid.NewGuid().ToString();
                        Fcn_UpdateCongTac360(SortId, codetheogiaidoan, dt, dt_DLG, dt_congtactheogiaidoan, crRow, i);
                        SortId++;
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;

                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            }
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            foreach (DataRow row in dt_congtactheogiaidoan.Rows)
            {
                MyFunction.fcn_TDKH_ThemDinhMucMacDinhChoCongTac(TypeKLHN.CongTac, row["Code"].ToString(), false);
            }
            TDKHHelper.CapNhatAllVatTuHaoPhi(CodesHangMuc: new string[] { codehangmuc });
            DuAnHelper.Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private void Fcn_UpdateCongTacThuCongMyHouse(long SortId, string codetheogiaidoan, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, Row crRow, int i, string CodeHM, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            WaitFormHelper.ShowWaitForm($"{i + 1}.{tencongtac}");
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            double KL = !txtCTKhoiLuong.Text.HasValue() ? 0 : (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl) ? crRow[txtCTKhoiLuong.Text].Value.NumericValue : 0);

            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KL;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = txtCTMaDuToan.Text != "" ? crRow[txtCTMaDuToan.Text].Value.ToString() : default;
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = txtCTDonVi.Text != "" ? crRow[txtCTDonVi.Text].Value.ToString() : default;
            if (CodeTuyen != default)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;

            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaDuThau"] = dtRow_congtactheogiadoan["DonGiaThiCong"] = crRow[DonGiaHD.Text].Value.NumericValue;
            if (txtCTDGVatLieu.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (txtCTDGNhanCong.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (txtCTDGMay.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                if (KLDV > 0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_UpdateCongTacThuCong(long SortId, string codetheogiaidoan, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, Row crRow, int i, string CodeHM, bool DonGiaDuThau, bool m_CheckSheet, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            WaitFormHelper.ShowWaitForm($"{i + 1}.{tencongtac}");
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            double KL = !txtCTKhoiLuong.Text.HasValue() ? 0 : (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl) ? crRow[txtCTKhoiLuong.Text].Value.NumericValue : 0);

            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KL;
            //dtRow_congtactheogiadoan["KhoiLuongToanBo_Iscongthucmacdinh"] = true;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = CodeHM;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = txtCTMaDuToan.Text != "" ? crRow[txtCTMaDuToan.Text].Value.ToString() : default;
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = txtCTDonVi.Text != "" ? crRow[txtCTDonVi.Text].Value.ToString() : default;
            if (CodeTuyen != default)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            //dtRow_congtactheogiadoan["NgayBatDauThiCong"] = te_KHBegin.Text != ""?(crRow[te_KHBegin.Text].Value.IsDateTime?DateTime.Parse(crRow[te_KHBegin.Text].Value.ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE):null):null;
            //dtRow_congtactheogiadoan["NgayKetThucThiCong"] = te_KHEnd.Text != "" ? (crRow[te_KHEnd.Text].Value.IsDateTime?DateTime.Parse(crRow[te_KHEnd.Text].Value.ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE):null):null;
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            if (DonGiaDuThau && m_CheckSheet)
            {
                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaDuThau"] = dtRow_congtactheogiadoan["DonGiaThiCong"] = crRow[txt_dongiaduthau.Text].Value.NumericValue;
            }
            if (ce_LayMotSheet.Checked && DonGiaHD.Text.HasValue())
            {
                dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaDuThau"] = dtRow_congtactheogiadoan["DonGiaThiCong"] = crRow[DonGiaHD.Text].Value.NumericValue;
            }
            if (txtCTDGVatLieu.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (txtCTDGNhanCong.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (txtCTDGMay.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                if (KLDV > 0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            WaitFormHelper.CloseWaitForm();
        }
        private int Fcn_UpdateNhomCongTac(int RowDuThau, Worksheet wsDonGia, Row crRow, double KL, string TenNhom, DataRow dtNhom, int i)
        {
            double STT = crRow[txt_congtrinh_stt.Text].Value.NumericValue;
            double DonGia = 0;
            for (int row = RowDuThau; row <= i; row++)
            {
                Row CrowDuThau = wsDonGia.Rows[row];
                string TryParseSTT = CrowDuThau[txt_STT.Text].Value.ToString();
                string TenNhomDuThau = CrowDuThau[txt_tencongtac_HD.Text].Value.ToString();
                bool CheckSTT = double.TryParse(TryParseSTT, out double STT_DuThau);
                double KLNhom = CrowDuThau[txt_klduthau.Text].Value.NumericValue;
                if (STT_DuThau == STT && KL == KLNhom && TenNhomDuThau == TenNhom)
                {
                    DonGia = CrowDuThau[txt_dongiaduthau.Text].Value.NumericValue;
                    RowDuThau = row + 1;
                    break;
                }
            }
            dtNhom["DonGia"] = DonGia;

            return RowDuThau;
        }
        private int Fcn_UpdateCongTac(long SortId, string codetheogiaidoan, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, Row crRow, Worksheet wsDonGia, int i, int RowDuThau, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
            double STT = crRow[txt_congtrinh_stt.Text].Value.NumericValue;
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double KLCT);
            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KLCT;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
            if (CodeTuyen != default)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            if (txtCTDGVatLieu.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (txtCTDGNhanCong.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (txtCTDGMay.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                if (KLDV > 0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            double DonGia = 0;
            for (int row = RowDuThau; row <= i; row++)
            {
                Row CrowDuThau = wsDonGia.Rows[row];
                string TryParseSTT = CrowDuThau[txt_STT.Text].Value.ToString();
                bool CheckSTT = double.TryParse(TryParseSTT, out double STT_DuThau);
                string MaHieuDuThau = CrowDuThau[txt_macongtac.Text].Value.ToString();
                double KLDT = CrowDuThau[txt_klduthau.Text].Value.NumericValue;
                if ((STT_DuThau == STT && MaHieu == MaHieuDuThau) || (KLDT == KLCT && MaHieu == MaHieuDuThau))
                {
                    double.TryParse(CrowDuThau[txt_dongiaduthau.Text].Value.ToString(), out DonGia);
                    //DonGia = CrowDuThau[txt_dongiaduthau.Text].Value.NumericValue;
                    RowDuThau = row + 1;
                    break;
                }
            }
            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            return RowDuThau;
        }
        private void Fcn_UpdateCongTacTruongSon2(double KLCT, long SortId, string codetheogiaidoan, DataTable dtHaoPhi, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, Row crRow, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
            //double STT = crRow[txt_congtrinh_stt.Text].Value.NumericValue;
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"]
                = KLCT;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            string DoViThau = crRow[txtCTDonVi.Text].Value.TextValue;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = DoViThau;
            if (CodeTuyen != default)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            if (txtCTDGVatLieu.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (txtCTDGNhanCong.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (txtCTDGMay.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                if (KLDV > 0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            //double DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
            double.TryParse(crRow[DonGiaHD.Text].Value.ToString(), out double DonGia);
            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            string Text = crRow["B"].Formula;
            string Formula = string.Empty;
            int pos = 0;
            if (string.IsNullOrEmpty(Text))
            {
                Text = crRow[txtCTDGVatLieu.Text].Formula;
                pos = Text.IndexOf("Đơn giá chi tiết");
                if (pos <= 0)
                {
                    Text = crRow[txtCTDGNhanCong.Text].Formula;
                    pos = Text.IndexOf("Đơn giá chi tiết");
                    if (pos <= 0)
                    {
                        Text = crRow[txtCTDGMay.Text].Formula;
                        pos = Text.IndexOf("Đơn giá chi tiết");
                    }
                }
                if (pos < 0)
                    return;
                Text = Text.Remove(0, pos);
                Formula = Regex.Replace(Text, $@"Đơn giá chi tiết|\D", string.Empty);
                fcn_updateHaophiGiaoThongTruongSon2("Đơn giá chi tiết", dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(),
    tencongtac, DoViThau, int.Parse(Formula), codetheogiaidoan, MaHieu);
                return;
            }
            pos = Text.IndexOf(cboHPVTtenSheet.Text);
            Text = Text.Remove(0, pos);
            Formula = Regex.Replace(Text, $@"{cboHPVTtenSheet.Text}|\D", string.Empty);
            if (crRow.Worksheet.Name == "DTCT")
                fcn_updateHaophiGiaoThongTruongSon2("Don gia XD phu", dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(),
          tencongtac, DoViThau, int.Parse(Formula), codetheogiaidoan, MaHieu);
            else
                fcn_updateHaophiGiaoThongTruongSon2(cboHPVTtenSheet.Text, dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(),
                tencongtac, DoViThau, int.Parse(Formula), codetheogiaidoan, MaHieu);
        }
        private int Fcn_UpdateCongTacThuCongHaoPhi(long SortId, string codetheogiaidoan, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, Row crRow, Worksheet wsDonGia, int i, int RowDuThau, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
            //double STT = crRow[txt_congtrinh_stt.Text].Value.NumericValue;
            double KL = crRow[txtCTKhoiLuong.Text].Value.NumericValue;
            string DoViThau = crRow[txtCTDonVi.Text].Value.TextValue;
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = crRow[txtCTKhoiLuong.Text].Value.ToString() == "" ? 0 : crRow[txtCTKhoiLuong.Text].Value.NumericValue;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = MaHieu;
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = DoViThau;
            if (CodeTuyen != default)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            if (txtCTDGVatLieu.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (txtCTDGNhanCong.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (txtCTDGMay.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out double KLDV);
                if(KL>0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            double DonGia = 0;
            if (ce_LayMotSheet.Checked && DonGiaHD.Text.HasValue())
            {
                DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                goto Label;
            }
            for (int row = RowDuThau; row <= i; row++)
            {
                Row CrowDuThau = wsDonGia.Rows[row];
                //string TryParseSTT = CrowDuThau[txt_STT.Text].Value.ToString();
                //bool CheckSTT = double.TryParse(TryParseSTT, out double STT_DuThau);
                string MaHieuDuThau = CrowDuThau[txt_macongtac.Text].Value.ToString();
                string TenCongTac = CrowDuThau[txt_tencongtac_HD.Text].Value.ToString();
                //string DonVi = CrowDuThau[txt_tendonvi.Text].Value.ToString();
                double KLDuThau = CrowDuThau[txt_klduthau.Text].Value.NumericValue;
                if (Math.Round(KL, 4) == Math.Round(KLDuThau, 4) && MaHieuDuThau == MaHieu)
                {
                    if (TenCongTac == tencongtac)
                    {
                        DonGia = CrowDuThau[txt_dongiaduthau.Text].Value.NumericValue;
                        RowDuThau = row + 1;
                        break;
                    }
                    else if (TenCongTac.Contains(tencongtac))
                    {
                        DialogResult rs = MessageShower.ShowYesNoQuestion($"Bạn có muốn lấy đơn giá Công tác: {tencongtac} ở dòng {row + 1}");
                        if (rs == DialogResult.No)
                            continue;
                        DonGia = CrowDuThau[txt_dongiaduthau.Text].Value.NumericValue;
                        RowDuThau = row + 1;
                        break;
                    }
                }
            }
            Label:
            dtRow_congtactheogiadoan["DonGia"] = dtRowDLG["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            return RowDuThau;
        }
        private void Fcn_UpdateCongTac360(long SortId, string codetheogiaidoan, DataTable dt, DataTable dt_DLG, DataTable dt_congtactheogiaidoan, Row crRow, int i, string CodeNhomCongTac = null, string CodeTuyen = null)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            WaitFormHelper.ShowWaitForm($"{i + 1}.{tencongtac}");
            string code = Guid.NewGuid().ToString();
            DataRow dtRow = dt.NewRow();
            DataRow dtRowDLG = dt_DLG.NewRow();
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow["Code"] = code;
            dtRowDLG["Code"] = Guid.NewGuid().ToString();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["CodeCongTac"] = code;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRowDLG["KhoiLuong"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = crRow[txtCTKhoiLuong.Text].Value.ToString() == "" ? 0 : crRow[txtCTKhoiLuong.Text].Value.NumericValue;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow["CodeHangMuc"] = dtRowDLG["CodeHangMuc"] = codehangmuc;
            dtRow["MaHieuCongTac"] = dtRowDLG["MaHieuCongTac"] = crRow[txtCTMaDuToan.Text].Value.ToString();
            dtRow["TenCongTac"] = dtRowDLG["TenCongTac"] = tencongtac;
            dtRow["DonVi"] = dtRowDLG["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"] = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaHD.Text.HasValue() ? crRow[DonGiaHD.Text].Value.NumericValue : 0;
            dtRow["Modified"] = true;
            dtRow["PhatSinh"] = false;
            if (CodeTuyen != null)
                dtRow["CodePhanTuyen"] = CodeTuyen;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (!crRow[te_KHBegin.Text].Value.IsEmpty && !crRow[te_KHBegin.Text].Value.IsEmpty)
                dtRow_congtactheogiadoan["NgayBatDau"] = crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (!crRow[te_KHEnd.Text].Value.IsEmpty && !crRow[te_KHEnd.Text].Value.IsEmpty)
                dtRow_congtactheogiadoan["NgayKetThuc"] = crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != null)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;

            if (txtCTDGVatLieu.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
            }

            if (txtCTDGNhanCong.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
            }

            if (txtCTDGMay.Text.HasValue())
            {
                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
            }
            if (txtCTKhoiLuongDocVao.Text.HasValue())
            {
                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                if (KLDV > 0)
                    dtRow["KhoiLuongDocVao"] = KLDV;
                else if (!crRow[txtCTKhoiLuongDocVao.Text].Value.IsEmpty)
                    dtRow["KhoiLuongDocVao"] = 0;
            }
            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
            if (txt_congtrinh_sttND.Text.HasValue())
            {
                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
            }
            JsonGhiChu.CodeDanhMucCongTac = code;
            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
            dtRow["GhiChuBoSungJson"] = encryptedStr;
            dt.Rows.Add(dtRow);
            dt_DLG.Rows.Add(dtRowDLG);
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            WaitFormHelper.CloseWaitForm();
            //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
            //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_DLG, MyConstant.TBL_HopDongDuLieuGoc);
            //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
        }
        private void Fcn_LoadCongTrinhHM(string worksheet)
        {
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[worksheet];
            List<string> tencongtrinh = new List<string>();
            List<string> hangmuc = new List<string>();
            #region SEARCH Công trình, Hạng mục, STT
            string searchString = "CÔNG TRÌNH";
            string searchhangmuc = "HẠNG MỤC";
            dic_congtrinh.Clear();
            dic_hangmuc.Clear();
            IEnumerable<Cell> searchResult = sheetThongTin.Search(searchString);
            foreach (Cell cell in searchResult)
            {
                if (!cell.Value.ToString().Contains(":") || !cell.Value.ToString().StartsWith(searchString))
                    continue;
                tencongtrinh.Add(cell.Value.ToString().Substring(11).Replace(":", ""));
                if (!dic_congtrinh.Keys.Contains(cell.RowIndex))
                    dic_congtrinh.Add(cell.RowIndex, cell.Value.ToString().Substring(11).Replace(":", ""));
            }
            IEnumerable<Cell> searchResult_hangmuc = sheetThongTin.Search(searchhangmuc);
            foreach (Cell cell in searchResult_hangmuc)
            {
                if (!cell.Value.ToString().Contains(":") || !cell.Value.ToString().StartsWith(searchhangmuc))
                    continue;
                hangmuc.Add(cell.Value.ToString().Substring(8).Replace(":", ""));
                if (!dic_hangmuc.Keys.Contains(cell.RowIndex))
                    dic_hangmuc.Add(cell.RowIndex, cell.Value.ToString().Substring(8).Replace(":", ""));
            }
            if (dic_congtrinh.Count == 0)
                dic_congtrinh.Add(2, "Công trình 1");
            if (dic_hangmuc.Count == 0)
                dic_hangmuc.Add(3, "Hạng mục 1");
            #endregion
        }
        private void Fcn_loadtencot360(string worksheet, Dictionary<Control, string> tendic, TextEdit stt)
        {
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[worksheet];
            #region SEARCH Công trình, Hạng mục, STT
            string searchString = "GĐ";
            string searchhangmuc = "HM";
            string searchstt = "STT";
            dic_congtrinh.Clear();
            dic_hangmuc.Clear();
            IEnumerable<Cell> searchResult_STT = MyFunction.SearchRangeCell(sheetThongTin, searchstt);
            foreach (Cell cell in searchResult_STT)
            {
                if (stt.Text == "")
                    stt.Text = sheetThongTin.Columns[cell.ColumnIndex].Heading;
                else
                    break;
            }
            foreach (var item in tendic)
            {
                Cell searchResult_duthau = MyFunction.SearchRangeCell(sheetThongTin, item.Value).Where(x => x.Value.ToString().CompareTo(item.Value) == 0).FirstOrDefault();
                if (searchResult_duthau == null)
                    continue;
                item.Key.Text = sheetThongTin.Columns[searchResult_duthau.ColumnIndex].Heading;
            }
            IEnumerable<Cell> searchResult = sheetThongTin.Columns[txtCTMaDuToan.Text].Search(searchString);
            foreach (Cell cell in searchResult)
            {
                string CT = sheetThongTin.Rows[cell.RowIndex][txtCTTenCongTac.Text].Value.TextValue;
                if (!dic_congtrinh.Keys.Contains(cell.RowIndex))
                    dic_congtrinh.Add(cell.RowIndex, CT);
            }
            IEnumerable<Cell> searchResult_hangmuc = MyFunction.SearchRangeCell(sheetThongTin, searchhangmuc);
            foreach (Cell cell in searchResult_hangmuc)
            {
                string HM = sheetThongTin.Rows[cell.RowIndex][txtCTTenCongTac.Text].Value.TextValue;
                if (!dic_hangmuc.Keys.Contains(cell.RowIndex))
                    dic_hangmuc.Add(cell.RowIndex, HM);
            }
        }
        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Fcn_LoadData()
        {
            lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_Nhancong.Visibility = lcg_DuThau.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            IWorkbook wb = spsheet_XemFile.Document;
            lstSheetNames = new List<string>();
            string[] ten = wb.Worksheets.ToList().Select(x => x.Name).ToArray();
            lstSheetNames.AddRange(ten);
            if (!lstSheetNames.Contains("Dự thầu"))
            {
                wb.Worksheets.Add().Name = "Dự thầu";
                wb.Worksheets["Dự thầu"].Visible = false;
                lstSheetNames.Add("Dự thầu");
            }
            Fcn_CheckMau();
            string NameMau = cbe_MauCongTrinh.Text;
            cbe_TongHop.Properties.Items.AddRange(ten);
            cbe_DuToan.Properties.Items.AddRange(ten);
            cbb_sheet.Properties.Items.AddRange(ten);
            cboCTtenSheet.Properties.Items.AddRange(ten);
            cboHPVTtenSheet.Properties.Items.AddRange(ten);
            cbb_haophivattu.Properties.Items.AddRange(ten);
            cbb_VL_tensheet.Properties.Items.AddRange(ten);
            cbb_NC_Tensheet.Properties.Items.AddRange(ten);
            cbb_MTC_Tensheet.Properties.Items.AddRange(ten);
            if (NameMau == MyConstant.DuToanQLTC)
            {
                cbb_sheet.Text = "Dự thầu- ND";
                cboHPVTtenSheet.Text = "Hao phí vật tư -ND";
                cbb_haophivattu.Text = "Hao phí vật tư -ND";
                cbb_VL_tensheet.Text = "Vật liệu";
                cbb_NC_Tensheet.Text = "Nhân công";
                cbb_MTC_Tensheet.Text = "Máy thi công";
                cboCTtenSheet.Text = "Công trình";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                Fcn_LoadCongTrinhHM(cboCTtenSheet.Text);
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.DuToanG8)
            {
                lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_Nhancong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_GiaThang.Text = "Giá tháng";
                cbe_GiaThang.Properties.Items.AddRange(ten);
                cbb_sheet.Text = "Dự thầu";
                cbe_GiaThang.Text = "Giá tháng";
                cboHPVTtenSheet.Text = cbb_haophivattu.Text = "Chiết tính";
                cboCTtenSheet.Text = "Công trình";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                Fcn_LoadCongTrinhHM(cboCTtenSheet.Text);
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.MauGiaoThong)
            {
                cbe_TongHop.Text = "Mẫu số 01D. Bảng kê HM ĐCG ";
                cboCTtenSheet.Text = "4.DT Cầu ";
                cbb_sheet.Text = "DGTH cầu";
                cboHPVTtenSheet.Text = "DGCT (cau)";
                cbe_DuToan.Text = "1.DT CPTV, CPK";
                lcg_TongHopCTCha.Visibility = lcg_ChiTietDuToan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabbedControlGroup2.SelectedTabPage = lcg_TongHopCTCha;
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbe_TongHop.Text];
                Fcn_LoadCongTrinhHM(cbe_TongHop.Text);
                CellRange usedRange = wb.Worksheets[cbe_TongHop.Text].GetUsedRange();
                nud_tonghop_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.MauGiaoThongTruongSon)
            {
                cbe_TongHop.Text = "Gia trung thau phan khai LD";
                cboCTtenSheet.Text = "Du toan XD";
                cbb_sheet.Text = "3.DG Duong";
                cboHPVTtenSheet.Text = "Don gia XD";
                lcg_TongHopCTCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabbedControlGroup2.SelectedTabPage = lcg_TongHopCTCha;
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbe_TongHop.Text];
                Fcn_LoadCongTrinhHM(cbe_TongHop.Text);
                CellRange usedRange = wb.Worksheets[cbe_TongHop.Text].GetUsedRange();
                nud_tonghop_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.MauCanThoKienGiang)
            {
                cbe_TongHop.Text = "Phân bổ";
                cboCTtenSheet.Text = "5. DTCT-PA2";
                cbb_sheet.Text = "Lập giá";
                cboHPVTtenSheet.Text = "6.PTDG";
                lcg_TongHopCTCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabbedControlGroup2.SelectedTabPage = lcg_TongHopCTCha;
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbe_TongHop.Text];
                Fcn_LoadCongTrinhHM(cbe_TongHop.Text);
                CellRange usedRange = wb.Worksheets[cbe_TongHop.Text].GetUsedRange();
                nud_tonghop_end.Value = usedRange.RowCount;
            }    
            else if (NameMau == MyConstant.MauKienGiangCaMau)
            {
                cbe_TongHop.Text = "Mau05-Tuyen";
                cboCTtenSheet.Text = "DGTH-R";
                //cbb_sheet.Text = "Lập giá";
                cboHPVTtenSheet.Text = "5.DGCT";
                lcg_TongHopCTCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_DuThau.Visibility=lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabbedControlGroup2.SelectedTabPage = lcg_TongHopCTCha;
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbe_TongHop.Text];
                Fcn_LoadCongTrinhHM(cbe_TongHop.Text);
                CellRange usedRange = wb.Worksheets[cbe_TongHop.Text].GetUsedRange();
                nud_tonghop_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.MauGiaoThongTruongSon1)
            {
                cbe_TongHop.Text = "Gia trung thau phan khai LD";
                cboCTtenSheet.Text = "Du toan XD";
                cbb_sheet.Text = "2.DG Đuong";
                cboHPVTtenSheet.Text = "Don gia XD";
                lcg_TongHopCTCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabbedControlGroup2.SelectedTabPage = lcg_TongHopCTCha;
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbe_TongHop.Text];
                Fcn_LoadCongTrinhHM(cbe_TongHop.Text);
                CellRange usedRange = wb.Worksheets[cbe_TongHop.Text].GetUsedRange();
                nud_tonghop_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.DuToanF1)
            {
                cbb_sheet.Text = "Dự thầu";
                cboHPVTtenSheet.Text = "Hao phí vật tư";
                cbb_haophivattu.Text = "Hao phí vật tư";
                cbb_VL_tensheet.Text = "Vật liệu";
                cbb_NC_Tensheet.Text = "Nhân công";
                cbb_MTC_Tensheet.Text = "Máy thi công";
                cboCTtenSheet.Text = "Công trình";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                Fcn_LoadCongTrinhHM(cboCTtenSheet.Text);
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.MauGiaoThongTruongSon2)
            {
                cbe_TongHop.Text = "Gia trung thau phan khai LD";
                cboHPVTtenSheet.Text = "Don gia XD";
                cbb_haophivattu.Text = "Don gia XD";
                cboCTtenSheet.Text = "DTCT.duong";
                cbb_sheet.Text = "DG TH";
                lcg_TongHopCTCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabbedControlGroup2.SelectedTabPage = lcg_TongHopCTCha;
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbe_TongHop.Text];
                Worksheet ws = wb.Worksheets[cbb_sheet.Text];
                if (ws.AutoFilter.Columns.Count > 0)
                    ws.AutoFilter.Columns[0].ApplyCustomFilter(0, FilterComparisonOperator.GreaterThan);
                CellRange usedRange = wb.Worksheets[cbe_TongHop.Text].GetUsedRange();
                nud_tonghop_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.DuToan360)
            {
                dic_txtBox_CONGTRINH[txtCTMaDuToan] = "Mã công tác";
                dic_txtBox_CONGTRINH[txtCTTenCongTac] = "Tên công tác";
                dic_txtBox_CONGTRINH[txtCTKhoiLuong] = "Khối lượng thanh toán";
                dic_txtBox_CONGTRINH.Add(te_KHEnd, "Ngày kết thúc thi công");
                dic_txtBox_CONGTRINH.Add(te_KHBegin, "Ngày bắt đầu thi công");
                cboCTtenSheet.Text = "Thiết lập";
                txtCTKhoiLuong.Text = "H";
                txtCTDGVatLieu.Text = txtCTDGNhanCong.Text = txtCTDGMay.Text = "";
                Fcn_loadtencot360(cboCTtenSheet.Text, dic_txtBox_CONGTRINH, txt_congtrinh_stt);
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.MauKhongCoSan || NameMau == MyConstant.MauThuCongCoHaoPhi)
            {
                cboCTtenSheet.SelectedIndex = 0;
                Fcn_LoadCongTrinhHM(cboCTtenSheet.Text);
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
                ce_LayMotSheet.Enabled = true;
            }
            else if (NameMau == MyConstant.DuToanBacnam)
            {
                lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_Nhancong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                cbe_GiaThang.Properties.Items.AddRange(ten);
                lcg_GiaThang.Text = "Gia tri vat tu";
                cbe_GiaThang.Text = "Gia tri vat tu";
                cbb_sheet.Text = "Du thau";
                cboCTtenSheet.Text = "Du toan";
                cboHPVTtenSheet.Text = cbb_haophivattu.Text = "Don gia chi tiet";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];

                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                Fcn_LoadCongTrinhHM(cboCTtenSheet.Text);
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.DuToanEta)
            {
                cbb_sheet.Text = "ĐGTH";
                cboHPVTtenSheet.Text = "PTVT";
                cbb_haophivattu.Text = "PTVT";
                cbb_VL_tensheet.Text = "Giá VL";
                cbb_NC_Tensheet.Text = "Giá NC";
                cbb_MTC_Tensheet.Text = "Giá Máy";
                cboCTtenSheet.Text = "Tiên lượng";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                Fcn_LoadCongTrinhHM(cboCTtenSheet.Text);
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.DuToanMyHouse)
            {
                cboCTtenSheet.Text = "DuToan";
                cboHPVTtenSheet.Text = "ChiTietVT";
                cbb_haophivattu.Text = "ChiTietVT";
                cbb_VL_tensheet.Text = "VatTu";
                lcg_GiaThang.Visibility = lcg_MTC.Visibility = lcg_Nhancong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
            TenSheetVLNCMTC = new Dictionary<string, string>()
               {
                {"Vật liệu",cbb_VL_tensheet.Text},
                {"Nhân công",cbb_NC_Tensheet.Text},
                {"Máy thi công",cbb_MTC_Tensheet.Text}
               };
            TenDonViVLNCMTC = new Dictionary<string, string>()
               {
                {"Vật liệu",txt_VL_Donvi.Text},
                {"Nhân công",txt_NC_Donvi.Text},
                {"Máy thi công",txt_MTC_Donvi.Text}
               };
            CheckAuto = true;
            spsheet_XemFile.Document.History.Clear();
        }
        private void Fcn_CheckMau()
        {
            string queryStr = $"SELECT * FROM {MyConstant.TBL_CategoryReadExcel}";
            DataTable dt = DataProvider.InstanceBaoCao.ExecuteQuery(queryStr);
            string NameMau = MyConstant.MauKhongCoSan;
            List<CategoryReadExcel> m_CategoryReadExcel = DuAnHelper.ConvertToList<CategoryReadExcel>(dt);
            Dictionary<string, List<CategoryReadExcel>> myDictionary = m_CategoryReadExcel.GroupBy(o => o.CategoryName).ToDictionary(g => g.Key, g => g.ToList());
            foreach (var item in myDictionary)
            {
                List<string> lst = new List<string>();
                item.Value.ForEach(x => lst.Add(x.SheetNameSoure));
                if (lstSheetNames.Intersect(lst).Count() == lst.Count())
                {
                    NameMau = item.Key;
                    break;
                }
            }
            cbe_MauCongTrinh.Text = NameMau;
            queryStr = $"SELECT * FROM {MyConstant.TBL_InfoReadExcel}";
            List<InfoReadExcel> Infor = DataProvider.InstanceBaoCao.ExecuteQueryModel<InfoReadExcel>(queryStr);
            if (NameMau == MyConstant.DuToanEta)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 10).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_VatLieu, Infor_Eta, "VatLieu");
                ControlsHelper.SetTextForControlByTagName(lcg_Nhancong, Infor_Eta, "NhanCong");
                ControlsHelper.SetTextForControlByTagName(lcg_MTC, Infor_Eta, "MayThiCong");
            }
            else if (NameMau == MyConstant.DuToanF1)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 1).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_VatLieu, Infor_Eta, "VatLieu");
                ControlsHelper.SetTextForControlByTagName(lcg_Nhancong, Infor_Eta, "NhanCong");
                ControlsHelper.SetTextForControlByTagName(lcg_MTC, Infor_Eta, "MayThiCong");
            }
            else if (NameMau == MyConstant.DuToanMyHouse)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 24).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_VatLieu, Infor_Eta, "VatLieu");

            }
            else if (NameMau == MyConstant.DuToanQLTC)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 18).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_VatLieu, Infor_Eta, "VatLieu");
                ControlsHelper.SetTextForControlByTagName(lcg_Nhancong, Infor_Eta, "NhanCong");
                ControlsHelper.SetTextForControlByTagName(lcg_MTC, Infor_Eta, "MayThiCong");
            }
            else if (NameMau == MyConstant.DuToanBacnam)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 16).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_GiaThang, Infor_Eta, "GiaThang");
            }
            else if (NameMau == MyConstant.DuToanG8)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 6).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                //ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                //ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_GiaThang, Infor_Eta, "GiaThang");
            }
            else if (NameMau == MyConstant.MauGiaoThong)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 25).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_ChiTietDuToan, Infor_Eta, "DuToan");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_TongHopCTCha, Infor_Eta, "TongHop");
            }
            else if (NameMau == MyConstant.MauGiaoThongTruongSon)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 26).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_TongHopCTCha, Infor_Eta, "TongHop");
            }
            else if (NameMau == MyConstant.MauCanThoKienGiang)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 30).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_TongHopCTCha, Infor_Eta, "TongHop");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
            }      
            else if (NameMau == MyConstant.MauKienGiangCaMau)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 31).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                //ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_TongHopCTCha, Infor_Eta, "TongHop");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
            }
            else if (NameMau == MyConstant.MauGiaoThongTruongSon1)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 27).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_TongHopCTCha, Infor_Eta, "TongHop");
            }
            else if (NameMau == MyConstant.MauGiaoThongTruongSon2)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 14).ToList();
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_TongHopCTCha, Infor_Eta, "TongHop");
            }
        }
        private void sb_ReadExcel_Click(object sender, EventArgs e)
        {
            string NameMau = cbe_MauCongTrinh.Text;
            if (NameMau == MyConstant.DuToanQLTC)
                fcn_truyendataExxcel_congtrinh(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);
            else if (NameMau == MyConstant.DuToanEta)
                fcn_truyendataExxcel_congtrinh(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);
            else if (NameMau == MyConstant.DuToanF1)
                fcn_truyendataExxcel_congtrinhF1(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);
            else if (NameMau == MyConstant.DuToan360)
                fcn_truyendataExxcel_congtrinh360(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);
            else if (NameMau == MyConstant.DuToanMyHouse)
                Fcn_TruyenDataExcelThuCongMyHouse(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1);
            else if (NameMau == MyConstant.MauKhongCoSan)
                Fcn_TruyenDataExcelThuCong(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1);
            else if (NameMau == MyConstant.MauThuCongGiaoThong)
                Fcn_TruyenDataExcelThuCongGT((int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1);
            else if (NameMau == MyConstant.MauGiaoThongChungTruongSon)
                Fcn_TruyenDataExcelThuCongGT((int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1);
            else if (NameMau == MyConstant.DuToanG8)
                fcn_truyendataExxcel_congtrinhG8(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);
            else if (NameMau == MyConstant.DuToanBacnam)
                fcn_truyendataExxcel_congtrinhBacNam(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);
            else if (NameMau == MyConstant.MauThuCongCoHaoPhi)
                fcn_truyendataExxcel_DocThuCongHaoPhi(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);
            else if (NameMau == MyConstant.MauGiaoThong)
                fcn_truyendataExxcel_GiaoThong((int)nud_tonghop_begin.Value - 1, (int)nud_tonghop_end.Value - 1);
            else if (NameMau == MyConstant.MauGiaoThongTruongSon)
                fcn_truyendataExxcel_GiaoThongTruongSon((int)nud_tonghop_begin.Value - 1, (int)nud_tonghop_end.Value - 1);
            else if (NameMau == MyConstant.MauGiaoThongTruongSon1)
                fcn_truyendataExxcel_GiaoThongTruongSon1((int)nud_tonghop_begin.Value - 1, (int)nud_tonghop_end.Value - 1);
            else if (NameMau == MyConstant.MauGiaoThongTruongSon2)
                fcn_truyendataExxcel_MauTruongSon2((int)nud_tonghop_begin.Value - 1, (int)nud_tonghop_end.Value - 1);
            else if (NameMau == MyConstant.MauCanThoKienGiang)
                Fcn_DocMauKienGiangCanTho((int)nud_tonghop_begin.Value - 1, (int)nud_tonghop_end.Value - 1);          
            else if (NameMau == MyConstant.MauKienGiangCaMau)
                Fcn_ReadKienGiangCaMau((int)nud_tonghop_begin.Value - 1, (int)nud_tonghop_end.Value - 1);
        }
        private void tabbedControlGroup2_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            IWorkbook workbook = spsheet_XemFile.Document;
            if (tabbedControlGroup2.SelectedTabPage.Text == "Dự thầu" && cbb_sheet.Text != "")
            {
                if (!workbook.Worksheets.Contains(cbb_sheet.Text))
                    return;
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cbb_sheet.Text];
            }
            else if (tabbedControlGroup2.SelectedTabPage.Text == "Công trình" && cboCTtenSheet.Text != "")
            {
                if (!workbook.Worksheets.Contains(cboCTtenSheet.Text))
                    return;
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cboCTtenSheet.Text];
            }
            else if (tabbedControlGroup2.SelectedTabPage.Text == "Hao phí vật tư" && cboHPVTtenSheet.Text != "")
            {
                if (!workbook.Worksheets.Contains(cboHPVTtenSheet.Text))
                    return;
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cboHPVTtenSheet.Text];
            }
            else if (tabbedControlGroup2.SelectedTabPage.Text == "Vật liệu" && cbb_VL_tensheet.Text != "")
            {
                if (!workbook.Worksheets.Contains(cbb_VL_tensheet.Text))
                    return;
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cbb_VL_tensheet.Text];
            }
            else if (tabbedControlGroup2.SelectedTabPage.Text == "Nhân công" && cbb_NC_Tensheet.Text != "")
            {
                if (!workbook.Worksheets.Contains(cbb_NC_Tensheet.Text))
                    return;
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cbb_NC_Tensheet.Text];
            }
            else if (tabbedControlGroup2.SelectedTabPage.Text == "Máy thi công" && cbb_MTC_Tensheet.Text != "")
            {
                if (!workbook.Worksheets.Contains(cbb_MTC_Tensheet.Text))
                    return;
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cbb_MTC_Tensheet.Text];
            }
            else if (tabbedControlGroup2.SelectedTabPage.Text == "Giá tháng" && cbe_GiaThang.Text != "")
            {
                if (!workbook.Worksheets.Contains(cbe_GiaThang.Text))
                    return;
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cbe_GiaThang.Text];
            }
            else if (tabbedControlGroup2.SelectedTabPage.Text == "Tổng hợp công tác cha" && cbe_TongHop.Text != "")
            {
                if (!workbook.Worksheets.Contains(cbe_TongHop.Text))
                    return;
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cbe_TongHop.Text];
            }
            else if (tabbedControlGroup2.SelectedTabPage.Text == "Chi tiết dự toán" && cbe_DuToan.Text != "")
            {
                if (!workbook.Worksheets.Contains(cbe_DuToan.Text))
                    return;
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cbe_DuToan.Text];
            }
        }
        private double Fcn_UpdateDonGia(string TenVatTu, string MaVatTu, string DonVi)
        {
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet ws = workbook.Worksheets[cbb_VL_tensheet.Text];
            int lastLine = ws.GetUsedRange().BottomRowIndex;
            int Row = 0;
            double DonGia = 0;
            for (int i = Row; i <= lastLine; i++)
            {
                Row Crow = ws.Rows[i];
                string TenVatTuNew = Crow[txt_VL_TenVL.Text].Value.TextValue;
                string MaVatTuNew = Crow[txt_VL_Mahieu.Text].Value.TextValue;
                string DonViNew = Crow[txt_VL_Donvi.Text].Value.TextValue;
                if (TenVatTu == TenVatTuNew && MaVatTuNew == MaVatTu && DonVi == DonViNew)
                {
                    DonGia = Crow[txt_VL_Dongia.Text].Value.NumericValue;
                    break;
                }
            }
            return DonGia;
        }
        private int fcn_updateHaophiVTMyHouse(Worksheet sheetThongTin, DataTable dtHaoPhi, string TenCT, int end, string codecongtactheogiaidoan, int HaoPhi)
        {
            int begin = 0;
            string codevatu = "";
            bool Try = false;
            string TenCongTacNew = "";
            for (int i = HaoPhi; i <= end; i++)
            {
                Row CrowHaoPhi = sheetThongTin.Rows[i];
                string TenCongTac = CrowHaoPhi[txtHPVTTenCongTac.Text].Value.ToString();
                if (TenCT == TenCongTac)
                {
                    begin = i;
                    Try = true;
                    break;
                }
            }
            if (!Try)
                return HaoPhi;
            for (int i = begin; i <= end; i++)
            {
                Row crRow = sheetThongTin.Rows[i];
                TenCongTacNew = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                string loaivattu = crRow[txtHPVTDonVi.Text].Value.ToString().ToUpper() == "CÔNG" ? "Nhân công"
    : sheetThongTin.Rows[begin][txtHPVTDonVi.Text].Value.ToString().ToUpper() == "CA" ? "Máy thi công" : "Vật liệu";
                if (TenCongTacNew != TenCT)
                {
                    HaoPhi = i;
                    return HaoPhi;
                }
                codevatu = Guid.NewGuid().ToString();
                DataRow dt_vattuNew = dtHaoPhi.NewRow();
                string TenVT = crRow[te_TenVatTuHaoPhi.Text].Value.ToString();
                string MaVT = crRow[te_MaVatTuHaoPhi.Text].Value.ToString();
                string DonVi = crRow[txtHPVTDonVi.Text].Value.ToString();
                dt_vattuNew["Code"] = codevatu;
                dt_vattuNew["CodeCongTac"] = codecongtactheogiaidoan;
                dt_vattuNew["MaVatLieu"] = MaVT;
                dt_vattuNew["VatTu"] = TenVT;
                dt_vattuNew["DonVi"] = DonVi;
                dt_vattuNew["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                dt_vattuNew["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                dt_vattuNew["LoaiVatTu"] = loaivattu;
                dt_vattuNew["HeSoNguoiDung"] = dt_vattuNew["HeSo"] = 1;
                dt_vattuNew["DinhMucNguoiDung"] = dt_vattuNew["DinhMuc"] = crRow[DinhMucChuan[loaivattu].Text].Value.NumericValue == 0 ? 1 : crRow[DinhMucChuan[loaivattu].Text].Value.NumericValue; ;

                dt_vattuNew["DonGia"] = Fcn_UpdateDonGia(TenVT, MaVT, DonVi);

                dtHaoPhi.Rows.Add(dt_vattuNew);
            }
            return HaoPhi;

        }
        private int fcn_updateHaophiVT(DataTable dtHaoPhi, double vl, double nc, double mtc, string tenvattu, string donvi, int end, string codecongtactheogiaidoan, string mahieu, int HaoPhi, string STT, int RowCongTrinh, bool F1 = false)
        {
            int begin = 0;
            string codevatu = "";
            string formula = "";
            int rowindex = 0;
            double dongia = 0;
            Dictionary<string, double> checkvt = new Dictionary<string, double>()
             {
                {"Vật liệu",vl},
                {"Nhân công",nc},
                {"Máy thi công",mtc}
             };
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[cboHPVTtenSheet.Text];
            string Fomular = "";
            bool Try = false;
            string DonVi = "";
            for (int i = HaoPhi; i <= end; i++)
            {
                Row CrowHaoPhi = sheetThongTin.Rows[i];
                string MaHieu = CrowHaoPhi[txtHPVTMaCongTac.Text].Value.ToString();
                string ParseSTT = CrowHaoPhi[txtHPVTSTT.Text].Value.ToString();
                bool TryParse = double.TryParse(ParseSTT, out double STTHaoPhi);
                DonVi = CrowHaoPhi[txtHPVTDonVi.Text].Value.ToString();
                if (MaHieu == mahieu && ParseSTT == STT && DonVi == donvi)
                {
                    begin = i;
                    break;
                }
                else if (STTHaoPhi > double.Parse(STT) && TryParse)
                {
                    Fomular = CrowHaoPhi[txtHPVTMaCongTac.Text].Formula.Replace($"='{cboCTtenSheet.Text}'!{txtCTMaDuToan.Text}", "");
                    Try = int.TryParse(Fomular, out int Vitri);
                    if (Try && Vitri < RowCongTrinh)
                        continue;
                    else if (Try && Vitri > RowCongTrinh)
                        continue;
                    HaoPhi = i;
                    return HaoPhi;
                }
            }
            string loaivattu = sheetThongTin.Rows[begin + 1][txtHPVTTenCongTac.Text].Value.ToString();
            if (loaivattu.Contains("Vật liệu VAT"))
            {
                loaivattu = "Vật liệu";
            }
            if (!LIST_loaivattu.Contains(loaivattu))
            {
                foreach (var item in checkvt.Where(x => x.Value > 0))
                {
                    codevatu = Guid.NewGuid().ToString();
                    DataRow dt_vattu = dtHaoPhi.NewRow();
                    dt_vattu["Code"] = codevatu;
                    dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                    dt_vattu["MaVatLieu"] = mahieu;
                    dt_vattu["VatTu"] = tenvattu;
                    dt_vattu["DonVi"] = donvi;
                    dt_vattu["DonGia"] = item.Value;
                    dt_vattu["LoaiVatTu"] = item.Key;
                    dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                    dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                    dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dtHaoPhi.Rows.Add(dt_vattu);
                }
                checkvt.Clear();
                return HaoPhi;
            }
            for (int i = begin + 2; i <= end; i++)
            {
                Row crRow = sheetThongTin.Rows[i];
                string checkloaivattu = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                if (crRow[txtHPVTMaCongTac.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "0")
                {
                    HaoPhi = i;
                    break;
                }
                else
                {
                    if (checkloaivattu.Contains("Vật liệu VAT"))
                    {
                        continue;
                    }

                    if (LIST_loaivattu.Contains(checkloaivattu) && checkloaivattu != loaivattu)
                        loaivattu = checkloaivattu;
                    else
                    {
                        if (crRow[txtHPVTMaCongTac.Text].Value.ToString() == "" && crRow[txtHPVTSTT.Text].Value.ToString() == "" || crRow[txtHPVTMaCongTac.Text].Value.IsNumeric)
                        {
                            HaoPhi = i;
                            break;
                        }
                        if (crRow[txtHPVTMaCongTac.Text].Formula.Contains(cboCTtenSheet.Text))
                        {
                            HaoPhi = i;
                            break;
                        }
                        formula = crRow[txtHPVTMaCongTac.Text].Formula.Replace($"='{TenSheetVLNCMTC[loaivattu]}'!{txtHPVTMaCongTac.Text}", "");
                        if (formula == "")
                            formula = crRow[txtHPVTDonVi.Text].Formula.Replace($"='{TenSheetVLNCMTC[loaivattu]}'!{TenDonViVLNCMTC[loaivattu]}", "");
                        if (formula == "")
                        {
                            codevatu = Guid.NewGuid().ToString();
                            DataRow dt_vattuNew = dtHaoPhi.NewRow();

                            dt_vattuNew["Code"] = codevatu;
                            dt_vattuNew["CodeCongTac"] = codecongtactheogiaidoan;
                            dt_vattuNew["MaVatLieu"] = crRow[txtHPVTMaCongTac.Text].Value.ToString();
                            dt_vattuNew["VatTu"] = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                            dt_vattuNew["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                            dt_vattuNew["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dt_vattuNew["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dt_vattuNew["LoaiVatTu"] = loaivattu;
                            dt_vattuNew["HeSoNguoiDung"] = dt_vattuNew["HeSo"] = crRow[txtHPVTHS.Text].Value.IsEmpty ? 1 : crRow[txtHPVTHS.Text].Value.NumericValue;
                            if (F1)
                                dt_vattuNew["DinhMucNguoiDung"] = dt_vattuNew["DinhMuc"] = crRow[DinhMucChuan[loaivattu].Text].Value.IsEmpty ? 1 : crRow[DinhMucChuan[loaivattu].Text].Value.NumericValue;
                            else
                                dt_vattuNew["DinhMucNguoiDung"] = dt_vattuNew["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.IsEmpty ? 1 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                            dtHaoPhi.Rows.Add(dt_vattuNew);
                            continue;
                        }
                        rowindex = int.Parse(formula) - 1;
                        double.TryParse(workbook.Worksheets[TenSheetVLNCMTC[loaivattu]].Rows[rowindex][DonGiaVL_NC_MTC[loaivattu].Text].Value.ToString(), out dongia);
                        //dongia = workbook.Worksheets[TenSheetVLNCMTC[loaivattu]].Rows[rowindex][DonGiaVL_NC_MTC[loaivattu].Text].Value.IsNumeric ? workbook.Worksheets[TenSheetVLNCMTC[loaivattu]].Rows[rowindex][DonGiaVL_NC_MTC[loaivattu].Text].Value.NumericValue : 0;
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();

                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = crRow[txtHPVTMaCongTac.Text].Value.ToString();
                        dt_vattu["VatTu"] = crRow[txtHPVTTenCongTac.Text].Value.ToString().Substring(3);
                        dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = dongia;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["LoaiVatTu"] = loaivattu;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = crRow[txtHPVTHS.Text].Value.IsEmpty ? 1 : crRow[txtHPVTHS.Text].Value.NumericValue;
                        if (F1)
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[DinhMucChuan[loaivattu].Text].Value.IsEmpty ? 1 : crRow[DinhMucChuan[loaivattu].Text].Value.NumericValue;
                        else
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.IsEmpty ? 1 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
            return HaoPhi;

        }
        private int fcn_updateHaophiVTG8(DataTable dtHaoPhi, string vl, string nc, string mtc, string tenvattu, string donvi, int end, string codecongtactheogiaidoan, string mahieu, int HaoPhi, string STT, bool Multiple, int RowCongTrinh)
        {
            int begin = 0;
            string codevatu = "";
            string formula = "";
            int rowindex = 0;
            double dongia = 0;
            Dictionary<string, string> checkvt = new Dictionary<string, string>()
             {
                {"Vật liệu",vl},
                {"Nhân công",nc},
                {"Máy thi công",mtc}
             };
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[cboHPVTtenSheet.Text];
            string Fomular = "";
            bool Try = false;
            string DonVi = "";
            for (int i = HaoPhi; i <= end; i++)
            {
                Row CrowHaoPhi = sheetThongTin.Rows[i];
                string MaHieu = CrowHaoPhi[txtHPVTMaCongTac.Text].Value.ToString();
                string ParseSTT = CrowHaoPhi[txtHPVTSTT.Text].Value.ToString();
                bool TryParse = double.TryParse(ParseSTT, out double STTHaoPhi);
                DonVi = CrowHaoPhi[txtHPVTDonVi.Text].Value.ToString();
                if (MaHieu == mahieu && ParseSTT == STT && DonVi == donvi)
                {
                    begin = i;
                    break;
                }
                else if (STTHaoPhi > double.Parse(STT) && TryParse)
                {
                    Fomular = CrowHaoPhi[txtHPVTMaCongTac.Text].Formula.Replace($"='{cboCTtenSheet.Text}'!{txtCTMaDuToan.Text}", "");
                    Try = int.TryParse(Fomular, out int Vitri);
                    if (Try && Vitri < RowCongTrinh)
                        continue;
                    else if (Try && Vitri > RowCongTrinh)
                        continue;
                    HaoPhi = i;
                    return HaoPhi;
                }
            }
            string loaivattu = sheetThongTin.Rows[begin + 1][txtHPVTTenCongTac.Text].Value.ToString();
            if (loaivattu.Contains("Vật liệu VAT"))
            {
                loaivattu = "Vật liệu";
            }
            if (!LIST_loaivattu.Contains(loaivattu))
            {
                foreach (var item in checkvt)
                {
                    if (double.TryParse(item.Value, out double kl))
                    {
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = mahieu;
                        dt_vattu["VatTu"] = tenvattu;
                        dt_vattu["DonVi"] = donvi;
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = kl;
                        dt_vattu["LoaiVatTu"] = Fcn_CheckLoaiVL(item.Key);
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
                checkvt.Clear();
                return HaoPhi;
            }
            for (int i = begin + 2; i <= end; i++)
            {
                Row crRow = sheetThongTin.Rows[i];
                string checkloaivattu = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                if (crRow[txtHPVTMaCongTac.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "0")
                {
                    HaoPhi = i;
                    break;
                }
                else
                {
                    if (checkloaivattu.Contains("Vật liệu VAT"))
                    {
                        continue;
                    }

                    if (LIST_loaivattu.Contains(checkloaivattu) && checkloaivattu != loaivattu)
                        loaivattu = checkloaivattu;
                    else
                    {
                        if (crRow[txtHPVTMaCongTac.Text].Value.ToString() == "")
                            continue;
                        if (crRow[txtHPVTMaCongTac.Text].Value.ToString() == "" && crRow[txtHPVTSTT.Text].Value.ToString() == "" || crRow[txtHPVTMaCongTac.Text].Value.IsNumeric)
                        {
                            HaoPhi = i;
                            break;
                        }
                        if (crRow[txtHPVTMaCongTac.Text].Formula.Contains(cboCTtenSheet.Text))
                        {
                            HaoPhi = i;
                            break;
                        }
                        if (txtHPVTDonGia.Text != "")
                        {
                            dongia = crRow[txtHPVTDonGia.Text].Value.IsNumeric ? crRow[txtHPVTDonGia.Text].Value.NumericValue : 0;
                            goto Label;
                        }
                        formula = crRow[txtHPVTMaCongTac.Text].Formula.Replace($"='{cbe_GiaThang.Text}'!{txtHPVTMaCongTac.Text}", "");
                        if (formula.StartsWith("$"))
                            formula = formula.Replace("$", "");
                        else if (formula == "")
                        {
                            dongia = 0;
                            goto Label;
                        }
                        rowindex = int.Parse(formula) - 1;
                        dongia = workbook.Worksheets[cbe_GiaThang.Text].Rows[rowindex][te_GiaThangDonGia.Text].Value.IsNumeric ? workbook.Worksheets[cbe_GiaThang.Text].Rows[rowindex][te_GiaThangDonGia.Text].Value.NumericValue : 0;

                        Label:
                        string LoaiVL = Fcn_CheckLoaiVL(loaivattu);
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = crRow[txtHPVTMaCongTac.Text].Value.ToString();
                        dt_vattu["VatTu"] = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                        dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = dongia;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["LoaiVatTu"] = LoaiVL;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = crRow[txtHPVTHS.Text].Value.IsEmpty ? 1 : crRow[txtHPVTHS.Text].Value.NumericValue;
                        if (Multiple)
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[DinhMucChuan[LoaiVL].Text].Value.IsEmpty ? 1 : crRow[DinhMucChuan[LoaiVL].Text].Value.NumericValue;
                        else
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.IsEmpty ? 1 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
            return HaoPhi;
        }
        private int fcn_updateHaophiThuCongLayHaoPhi(DataTable dtHaoPhi, string vl, string nc, string mtc, string tenvattu, string donvi, int end, string codecongtactheogiaidoan, string mahieu, int HaoPhi, double STT, int RowCongTrinh, double KLCT)
        {
            int begin = 0;
            string codevatu = "";
            string formula = "";
            int rowindex = 0;
            double dongia = 0;
            Dictionary<string, string> checkvt = new Dictionary<string, string>()
             {
                {"Vật liệu",vl},
                {"Nhân công",nc},
                {"Máy thi công",mtc}
             };
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[cboHPVTtenSheet.Text];
            string Fomular = "";
            bool Try = false;
            string DonVi = "";
            for (int i = HaoPhi; i <= end; i++)
            {
                Row CrowHaoPhi = sheetThongTin.Rows[i];
                string MaHieu = CrowHaoPhi[txtHPVTMaCongTac.Text].Value.ToString();
                string ParseSTT = CrowHaoPhi[txtHPVTSTT.Text].Value.ToString();
                bool TryParse = double.TryParse(ParseSTT, out double STTHaoPhi);
                if (!TryParse)
                    continue;
                double KL = CrowHaoPhi[txtHPVTKLVatTu.Text].Value.NumericValue;
                if (MaHieu == mahieu && STTHaoPhi == STT && KL == KLCT)
                {
                    begin = i;
                    break;
                }
                else if (STTHaoPhi > STT && TryParse)
                {
                    Fomular = CrowHaoPhi[txtHPVTMaCongTac.Text].Formula.Replace($"='{cboCTtenSheet.Text}'!{txtCTMaDuToan.Text}", "");
                    Try = int.TryParse(Fomular, out int Vitri);
                    if (Try && Vitri < RowCongTrinh)
                        continue;
                    else if (Try && Vitri > RowCongTrinh)
                        continue;
                    HaoPhi = i;
                    return HaoPhi;
                }
            }
            string loaivattu = sheetThongTin.Rows[begin + 1][txtHPVTTenCongTac.Text].Value.ToString();
            if (loaivattu.Contains("Vật liệu VAT"))
            {
                loaivattu = "Vật liệu";
            }
            if (!LIST_loaivattu.Contains(loaivattu))
            {
                foreach (var item in checkvt)
                {
                    if (double.TryParse(item.Value, out double kl))
                    {
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = mahieu;
                        dt_vattu["VatTu"] = tenvattu;
                        dt_vattu["DonVi"] = donvi;
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = kl;
                        dt_vattu["LoaiVatTu"] = Fcn_CheckLoaiVL(item.Key);
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
                checkvt.Clear();
                return HaoPhi;
            }
            for (int i = begin + 2; i <= end; i++)
            {
                Row crRow = sheetThongTin.Rows[i];
                string checkloaivattu = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                if (crRow[txtHPVTMaCongTac.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "0")
                {
                    HaoPhi = i;
                    break;
                }
                else
                {
                    if (checkloaivattu.Contains("Vật liệu VAT"))
                    {
                        continue;
                    }

                    if (LIST_loaivattu.Contains(checkloaivattu) && checkloaivattu != loaivattu)
                        loaivattu = checkloaivattu;
                    else
                    {
                        if (crRow[te_MaVatTuHaoPhi.Text].Value.ToString() == "")
                            continue;
                        if (crRow[te_MaVatTuHaoPhi.Text].Value.ToString() == "" && crRow[txtHPVTSTT.Text].Value.ToString() == "" || crRow[te_MaVatTuHaoPhi.Text].Value.IsNumeric)
                        {
                            HaoPhi = i;
                            break;
                        }
                        if (txtHPVTDonGia.Text != "")
                        {
                            dongia = crRow[txtHPVTDonGia.Text].Value.IsNumeric ? crRow[txtHPVTDonGia.Text].Value.NumericValue : 0;
                            goto Label;
                        }
                        formula = crRow[txtHPVTMaCongTac.Text].Formula.Replace($"='{cbe_GiaThang.Text}'!{txtHPVTMaCongTac.Text}", "");
                        if (formula.StartsWith("$"))
                            formula = formula.Replace("$", "");
                        else if (formula == "")
                        {
                            dongia = 0;
                            goto Label;
                        }
                        rowindex = int.Parse(formula) - 1;
                        dongia = workbook.Worksheets[cbe_GiaThang.Text].Rows[rowindex][te_GiaThangDonGia.Text].Value.IsNumeric ? workbook.Worksheets[cbe_GiaThang.Text].Rows[rowindex][te_GiaThangDonGia.Text].Value.NumericValue : 0;

                        Label:
                        string LoaiVL = Fcn_CheckLoaiVL(loaivattu);
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = crRow[te_MaVatTuHaoPhi.Text].Value.ToString();
                        dt_vattu["VatTu"] = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                        dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = dongia;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["LoaiVatTu"] = LoaiVL;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = crRow[txtHPVTHS.Text].Value.IsEmpty ? 1 : crRow[txtHPVTHS.Text].Value.NumericValue;
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.IsEmpty ? 1 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
            return HaoPhi;
        }
        private int fcn_updateHaophiVTBacNam(DataTable dtHaoPhi, string vl, string nc, string mtc, string tenvattu, string donvi, int end, string codecongtactheogiaidoan, string mahieu, int HaoPhi, string STT, bool Multiple, int RowCongTrinh)
        {
            int begin = 0;
            string codevatu = "";
            string formula = "";
            int rowindex = 0;
            double dongia = 0;
            Dictionary<string, string> checkvt = new Dictionary<string, string>()
             {
                {"Vật liệu",vl},
                {"Nhân công",nc},
                {"Máy thi công",mtc}
             };
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[cboHPVTtenSheet.Text];
            string Fomular = "";
            bool Try = false;
            string DonVi = "";
            for (int i = HaoPhi; i <= end; i++)
            {
                Row CrowHaoPhi = sheetThongTin.Rows[i];
                string MaHieu = CrowHaoPhi[txtHPVTMaCongTac.Text].Value.ToString();
                string ParseSTT = CrowHaoPhi[txtHPVTSTT.Text].Value.ToString();
                bool TryParse = double.TryParse(ParseSTT, out double STTHaoPhi);
                DonVi = CrowHaoPhi[txtHPVTDonVi.Text].Value.ToString();
                if (MaHieu == mahieu && ParseSTT == STT && DonVi == donvi)
                {
                    begin = i;
                    break;
                }
                else if (STTHaoPhi > double.Parse(STT) && TryParse)
                {
                    Fomular = CrowHaoPhi[txtHPVTMaCongTac.Text].Formula.Replace($"='{cboCTtenSheet.Text}'!{txtCTMaDuToan.Text}", "");
                    Try = int.TryParse(Fomular, out int Vitri);
                    if (Try && Vitri < RowCongTrinh)
                        continue;
                    else if (Try && Vitri > RowCongTrinh)
                        continue;
                    HaoPhi = i;
                    return HaoPhi;
                }
            }
            string loaivattu = sheetThongTin.Rows[begin + 1][txtHPVTTenCongTac.Text].Value.ToString();
            if (loaivattu.Contains("Vật liệu VAT"))
            {
                loaivattu = "Vật liệu";
            }
            if (!LIST_loaivattu.Contains(loaivattu))
            {
                foreach (var item in checkvt)
                {
                    if (double.TryParse(item.Value, out double kl))
                    {
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = mahieu;
                        dt_vattu["VatTu"] = tenvattu;
                        dt_vattu["DonVi"] = donvi;
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = kl;
                        dt_vattu["LoaiVatTu"] = Fcn_CheckLoaiVL(item.Key);
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = 1;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = 1;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
                checkvt.Clear();
                return HaoPhi;
            }
            for (int i = begin + 2; i <= end; i++)
            {
                Row crRow = sheetThongTin.Rows[i];
                string checkloaivattu = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                if (crRow[txtHPVTMaCongTac.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "" && crRow[txtHPVTSTT.Text].Value.ToString() != "0")
                {
                    HaoPhi = i;
                    break;
                }
                else
                {
                    if (checkloaivattu.Contains("Vật liệu VAT"))
                    {
                        continue;
                    }

                    if (LIST_loaivattu.Contains(checkloaivattu) && checkloaivattu != loaivattu)
                        loaivattu = checkloaivattu;
                    else
                    {
                        if (crRow[te_MaVatTuHaoPhi.Text].Value.ToString() == "")
                            continue;
                        if (crRow[te_MaVatTuHaoPhi.Text].Value.ToString() == "" && crRow[txtHPVTSTT.Text].Value.ToString() == "" || crRow[te_MaVatTuHaoPhi.Text].Value.IsNumeric)
                        {
                            HaoPhi = i;
                            break;
                        }
                        if (txtHPVTDonGia.Text != "")
                        {
                            dongia = crRow[txtHPVTDonGia.Text].Value.IsNumeric ? crRow[txtHPVTDonGia.Text].Value.NumericValue : 0;
                            goto Label;
                        }
                        formula = crRow[txtHPVTMaCongTac.Text].Formula.Replace($"='{cbe_GiaThang.Text}'!{txtHPVTMaCongTac.Text}", "");
                        if (formula.StartsWith("$"))
                            formula = formula.Replace("$", "");
                        else if (formula == "")
                        {
                            dongia = 0;
                            goto Label;
                        }
                        rowindex = int.Parse(formula) - 1;
                        dongia = workbook.Worksheets[cbe_GiaThang.Text].Rows[rowindex][te_GiaThangDonGia.Text].Value.IsNumeric ? workbook.Worksheets[cbe_GiaThang.Text].Rows[rowindex][te_GiaThangDonGia.Text].Value.NumericValue : 0;

                        Label:
                        string LoaiVL = Fcn_CheckLoaiVL(loaivattu);
                        codevatu = Guid.NewGuid().ToString();
                        DataRow dt_vattu = dtHaoPhi.NewRow();
                        dt_vattu["Code"] = codevatu;
                        dt_vattu["CodeCongTac"] = codecongtactheogiaidoan;
                        dt_vattu["MaVatLieu"] = crRow[te_MaVatTuHaoPhi.Text].Value.ToString();
                        dt_vattu["VatTu"] = crRow[txtHPVTTenCongTac.Text].Value.ToString();
                        dt_vattu["DonVi"] = crRow[txtHPVTDonVi.Text].Value.ToString();
                        dt_vattu["DonGia"] = dt_vattu["DonGiaThiCong"] = dongia;
                        dt_vattu["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dt_vattu["LoaiVatTu"] = LoaiVL;
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = crRow[txtHPVTHS.Text].Value.IsEmpty ? 1 : crRow[txtHPVTHS.Text].Value.NumericValue;

                        if (Multiple)
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[DinhMucChuan[LoaiVL].Text].Value.IsEmpty ? 1 : crRow[DinhMucChuan[LoaiVL].Text].Value.NumericValue;
                        else
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.IsEmpty ? 1 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
            return HaoPhi;
        }
        private string Fcn_CheckLoaiVL(string Vl)
        {
            if (Vl.Contains("Vật liệu") || Vl.Contains("Thuế tài nguyên"))
                return "Vật liệu";
            else if (Vl.Contains("Nhân công"))
                return "Nhân công";
            else if (Vl.Contains("Máy thi công") || Vl.Contains("Xe máy"))
                return "Máy thi công";
            else
                return string.Empty;
        }      
        private string Fcn_CheckLoaiVLCaMauKienGiang(string Vl)
        {
            if (Vl.Contains("Vật liệu") || Vl.Contains("A - Vật liệu"))
                return "Vật liệu";
            else if (Vl.Contains("Nhân công"))
                return "Nhân công";
            else if (Vl.Contains("Máy thi công") || Vl.Contains("C - Máy thi công"))
                return "Máy thi công";
            else
                return string.Empty;
        }
        private int fcn_updatenhomdiendaiThuCongKhongCoMaCT(int begin, int end, string codecongtac, DataTable dt_NhomDienDai, DataTable dt_Chitietcongtaccon)
        {
            string code_nhom = null, code_con = Guid.NewGuid().ToString();
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[cboCTtenSheet.Text];
            bool check = false;
            int End = 0;
            for (int i = begin; i <= end; i++)
            {
                Row Crow = sheetThongTin.Rows[i];
                if (Crow[txt_congtrinh_stt.Text].Value.TextValue != "")
                {
                    End = i;
                    break;
                }
                else
                {
                    if (Crow[txtCTTenCongTac.Text].Value.ToString().EndsWith(":"))
                    {
                        code_nhom = Guid.NewGuid().ToString();
                        code_con = code_nhom;
                        DataRow dtRow_nhom = dt_NhomDienDai.NewRow();
                        dtRow_nhom["Code"] = code_nhom;
                        dtRow_nhom["CodeCongTacTheoGiaiDoan"] = codecongtac;
                        dtRow_nhom["Ten"] = Crow[txtCTTenCongTac.Text].Value.TextValue.Trim(':');
                        check = true;
                        dt_NhomDienDai.Rows.Add(dtRow_nhom);
                    }
                    else
                    {
                        if (Crow[txtCTTenCongTac.Text].Value.ToString() == "")
                            continue;
                        DataRow dtRow_con = dt_Chitietcongtaccon.NewRow();
                        dtRow_con["Code"] = Guid.NewGuid().ToString();
                        dtRow_con["CodeCongTacCha"] = codecongtac;
                        dtRow_con["TenCongTac"] = Crow[txtCTTenCongTac.Text].Value.TextValue;
                        if (check)
                            dtRow_con["CodeNhom"] = code_con;
                        dt_Chitietcongtaccon.Rows.Add(dtRow_con);

                    }
                }

            }
            dt_Chitietcongtaccon.AsEnumerable().ForEach(x => x["Modified"] = true);
            return End;
        }
        private int fcn_updatenhomdiendai(int begin, int end, string codecongtac, DataTable dtNhomDienDai, DataTable dt_Chitietcongtaccon)
        {
            string code_nhom = null, code_con = Guid.NewGuid().ToString();
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[cboCTtenSheet.Text];
            bool check = false;
            int End = 0;
            for (int i = begin; i <= end; i++)
            {
                Row Crow = sheetThongTin.Rows[i];
                if (!string.IsNullOrEmpty(Crow[txtCTMaDuToan.Text].Value.TextValue))
                {
                    End = i;
                    break;
                }
                else
                {
                    if (Crow[txtCTTenCongTac.Text].Value.ToString().EndsWith(":"))
                    {
                        code_nhom = Guid.NewGuid().ToString();
                        code_con = code_nhom;
                        DataRow dtRow_nhom = dtNhomDienDai.NewRow();
                        dtRow_nhom["Code"] = code_nhom;
                        dtRow_nhom["CodeCongTacTheoGiaiDoan"] = codecongtac;
                        dtRow_nhom["Ten"] = Crow[txtCTTenCongTac.Text].Value.ToString().Trim(':');
                        check = true;
                        dtNhomDienDai.Rows.Add(dtRow_nhom);
                    }
                    else
                    {
                        if (Crow[txtCTTenCongTac.Text].Value.ToString() == "")
                            continue;
                        DataRow dtRow_con = dt_Chitietcongtaccon.NewRow();
                        dtRow_con["Code"] = Guid.NewGuid().ToString();
                        dtRow_con["CodeCongTacCha"] = codecongtac;
                        dtRow_con["TenCongTac"] = Crow[txtCTTenCongTac.Text].Value.ToString();
                        dtRow_con["Cao"] = te_Cao.Text.HasValue() ? Crow[te_Cao.Text].Value.NumericValue : 0;
                        dtRow_con["Dai"] = te_Dai.Text.HasValue() ? Crow[te_Dai.Text].Value.NumericValue : 0;
                        dtRow_con["Rong"] = te_Rong.Text.HasValue() ? Crow[te_Rong.Text].Value.NumericValue : 0;
                        dtRow_con["HeSoCauKien"] = te_HeSoCauKien.Text.HasValue() ? Crow[te_HeSoCauKien.Text].Value.NumericValue : 0;
                        dtRow_con["SoBoPhanGiongNhau"] = te_BoPhanGiongNhau.Text.HasValue() ? Crow[te_BoPhanGiongNhau.Text].Value.NumericValue : 0;
                        if (check)
                            dtRow_con["CodeNhom"] = code_con;
                        dt_Chitietcongtaccon.Rows.Add(dtRow_con);

                    }
                }

            }
            dt_Chitietcongtaccon.AsEnumerable().ForEach(x => x["Modified"] = true);
            return End;
        }
        private int fcn_updatenhomdiendaiLayHaoPhi(int begin, int end, string codecongtac, DataTable dtNhomDienDai, DataTable dt_Chitietcongtaccon)
        {
            string code_nhom = null, code_con = Guid.NewGuid().ToString();
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[cboCTtenSheet.Text];
            bool check = false;
            int End = 0;
            for (int i = begin; i <= end; i++)
            {
                Row Crow = sheetThongTin.Rows[i];
                if (Crow[txtCTMaDuToan.Text].Value.ToString() != "" || Crow[txtCTDonVi.Text].Value.ToString() != "")
                {
                    End = i;
                    break;
                }
                else
                {
                    if (Crow[txtCTTenCongTac.Text].Value.ToString().EndsWith(":"))
                    {
                        code_nhom = Guid.NewGuid().ToString();
                        code_con = code_nhom;
                        DataRow dtRow_nhom = dtNhomDienDai.NewRow();
                        dtRow_nhom["Code"] = code_nhom;
                        dtRow_nhom["CodeCongTacTheoGiaiDoan"] = codecongtac;
                        dtRow_nhom["Ten"] = Crow[txtCTTenCongTac.Text].Value.ToString().Trim(':');
                        check = true;
                        dtNhomDienDai.Rows.Add(dtRow_nhom);
                    }
                    else
                    {
                        if (Crow[txtCTTenCongTac.Text].Value.ToString() == "")
                            continue;
                        DataRow dtRow_con = dt_Chitietcongtaccon.NewRow();
                        dtRow_con["Code"] = Guid.NewGuid().ToString();
                        dtRow_con["CodeCongTacCha"] = codecongtac;
                        dtRow_con["TenCongTac"] = Crow[txtCTTenCongTac.Text].Value.ToString();
                        dtRow_con["Cao"] = te_Cao.Text.HasValue() ? Crow[te_Cao.Text].Value.NumericValue : 0;
                        dtRow_con["Dai"] = te_Dai.Text.HasValue() ? Crow[te_Dai.Text].Value.NumericValue : 0;
                        dtRow_con["Rong"] = te_Rong.Text.HasValue() ? Crow[te_Rong.Text].Value.NumericValue : 0;
                        dtRow_con["HeSoCauKien"] = te_HeSoCauKien.Text.HasValue() ? Crow[te_HeSoCauKien.Text].Value.NumericValue : 0;
                        dtRow_con["SoBoPhanGiongNhau"] = te_BoPhanGiongNhau.Text.HasValue() ? Crow[te_BoPhanGiongNhau.Text].Value.NumericValue : 0;
                        if (check)
                            dtRow_con["CodeNhom"] = code_con;
                        dt_Chitietcongtaccon.Rows.Add(dtRow_con);

                    }
                }

            }
            dt_Chitietcongtaccon.AsEnumerable().ForEach(x => x["Modified"] = true);
            return End;
        }
        private int fcn_updatenhomdiendaiThuCong(int begin, int end, string codecongtac, DataTable dtNhomDienDai, DataTable dt_Chitietcongtaccon)
        {
            string code_nhom = null, code_con = Guid.NewGuid().ToString();
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[cboCTtenSheet.Text];
            bool check = false;
            int End = 0;
            for (int i = begin; i <= end; i++)
            {
                Row Crow = sheetThongTin.Rows[i];
                if (Crow[txtCTMaDuToan.Text].Value.ToString() != "")
                {
                    End = i;
                    break;
                }
                else
                {
                    if (Crow[txtCTTenCongTac.Text].Value.ToString().EndsWith(":"))
                    {
                        code_nhom = Guid.NewGuid().ToString();
                        code_con = code_nhom;
                        DataRow dtRow_nhom = dtNhomDienDai.NewRow();
                        dtRow_nhom["Code"] = code_nhom;
                        dtRow_nhom["CodeCongTacTheoGiaiDoan"] = codecongtac;
                        dtRow_nhom["Ten"] = Crow[txtCTTenCongTac.Text].Value.ToString().Trim(':');
                        check = true;
                        dtNhomDienDai.Rows.Add(dtRow_nhom);
                    }
                    else
                    {
                        if (Crow[txtCTTenCongTac.Text].Value.ToString() == "")
                            continue;
                        DataRow dtRow_con = dt_Chitietcongtaccon.NewRow();
                        dtRow_con["Code"] = Guid.NewGuid().ToString();
                        dtRow_con["CodeCongTacCha"] = codecongtac;
                        dtRow_con["TenCongTac"] = Crow[txtCTTenCongTac.Text].Value.TextValue;
                        dtRow_con["Cao"] = te_Cao.Text.HasValue() ? Crow[te_Cao.Text].Value.NumericValue : 1;
                        dtRow_con["Dai"] = te_Dai.Text.HasValue() ? Crow[te_Dai.Text].Value.NumericValue : 1;
                        dtRow_con["Rong"] = te_Rong.Text.HasValue() ? Crow[te_Rong.Text].Value.NumericValue : 1;
                        dtRow_con["HeSoCauKien"] = te_HeSoCauKien.Text.HasValue() ? Crow[te_HeSoCauKien.Text].Value.NumericValue : 1;
                        dtRow_con["SoBoPhanGiongNhau"] = te_BoPhanGiongNhau.Text.HasValue() ? Crow[te_BoPhanGiongNhau.Text].Value.NumericValue : 1;
                        if (check)
                            dtRow_con["CodeNhom"] = code_con;
                        dt_Chitietcongtaccon.Rows.Add(dtRow_con);

                    }
                }

            }
            dt_Chitietcongtaccon.AsEnumerable().ForEach(x => x["Modified"] = true);
            return End;
        }
        private void fcn_kiemtracongtrinh_hangmuc(int batdau, bool NextCongTrinh = false)
        {
            if (rg_LayDuLieu.SelectedIndex > 0)
                return;
            int vitri = 0;
            codecongtrinh = Guid.NewGuid().ToString();
            if (NextCongTrinh)
                goto Label;
            foreach (var item in dic_congtrinh)
            {
                if (batdau > item.Key)
                {
                    vitri = item.Key;
                    tencongtrinh = item.Value;
                }
            }
            if (tencongtrinh == "")
                return;
            Label:
            string queryStr = $"SELECT \"Code\",\"Ten\" FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"Ten\"=@TenCongTrinh AND \"CodeDuAn\"='{m_CodeDuAn}'";
            string dbString = "";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr, parameter: new object[] { tencongtrinh });
            if (dt.Rows.Count == 0)
            {
                queryStr = $"SELECT \"Code\" FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE  \"CodeDuAn\"='{m_CodeDuAn}'";
                DataTable dtCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                dbString = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{dtCT.Rows.Count}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { tencongtrinh });
            }
            else
                codecongtrinh = dt.Rows[0]["Code"].ToString();
            foreach (var key in dic_hangmuc)
            {
                if (batdau > key.Key && key.Key > vitri)
                {
                    tenhangmuc = key.Value;
                }
            }
            if (tenhangmuc == "")
                tenhangmuc = dic_hangmuc[dic_hangmuc.Keys.Min()];
            string queryStr_hangmuc = $"SELECT \"Code\",\"Ten\" FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"Ten\"=@TenHangMuc AND \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_hangmuc = DataProvider.InstanceTHDA.ExecuteQuery(queryStr_hangmuc, parameter: new object[] { tenhangmuc });
            if (dt_hangmuc.Rows.Count == 0)
            {
                queryStr_hangmuc = $"SELECT \"Code\" FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE  \"CodeCongTrinh\"='{codecongtrinh}'";
                dt_hangmuc = DataProvider.InstanceTHDA.ExecuteQuery(queryStr_hangmuc);
                codehangmuc = Guid.NewGuid().ToString();
                dbString = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"Code\",\"CodeCongTrinh\",\"Ten\",\"SortID\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten,'{dt_hangmuc.Rows.Count}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { tenhangmuc });
            }
            else
                codehangmuc = dt_hangmuc.Rows[0]["Code"].ToString();
        }

        private void sb_ChonLaiFile_Click(object sender, EventArgs e)
        {
            m_openFileDialog.DefaultExt = "xls";
            m_openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            m_openFileDialog.Title = "Chọn file Excel";
            DialogResult rs = m_openFileDialog.ShowDialog();
            if (rs == DialogResult.OK)
            {
                filePath = m_openFileDialog.FileName;
                WaitFormHelper.ShowWaitForm("Đang phân tích File đọc vào, Vui lòng chờ!");
                hLE_File.EditValue = filePath;
                FileHelper.fcn_spSheetStreamDocument(spsheet_XemFile, filePath);
                Fcn_LoadData();
                WaitFormHelper.CloseWaitForm();
            }
        }

        private void cboCTtenSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cb = sender as ComboBoxEdit;
            IWorkbook wb = spsheet_XemFile.Document;
            if (wb.Worksheets.Contains(cb.Text))
            {
                if (cb.Text == cboCTtenSheet.Text)
                    Fcn_LoadCongTrinhHM(cb.Text);
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cb.Text];
                CellRange usedRange = wb.Worksheets[cb.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
        }

        private void rg_LayDuLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rg_LayDuLieu.SelectedIndex == 0)
            {
                lUE_ToChucCaNhan.Enabled = false;
            }
            else
            {
                lUE_ToChucCaNhan.Enabled = true;
                List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, false, true);
                lUE_ToChucCaNhan.Properties.DataSource = Infor;
                if (Infor.Count != 0)
                    lUE_ToChucCaNhan.EditValue = Infor.FirstOrDefault().ID;
            }
        }

        private void cboHPVTtenSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            IWorkbook wb = spsheet_XemFile.Document;
            if (cbe_MauCongTrinh.Text == MyConstant.DuToanG8)
            {
                string queryStr = $"SELECT * FROM {MyConstant.TBL_InfoReadExcel}";
                List<InfoReadExcel> Infor = DataProvider.InstanceBaoCao.ExecuteQueryModel<InfoReadExcel>(queryStr);
                if (cboHPVTtenSheet.Text == "HaoPhiVatTu")
                {
                    List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 7).ToList();
                    ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                    ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                }
                else
                {
                    List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 6).ToList();
                    ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                    ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                }
                if (!lstSheetNames.Contains(cboHPVTtenSheet.Text))
                    return;
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboHPVTtenSheet.Text];
            }
            else if (wb.Worksheets.Contains(cboHPVTtenSheet.Text))
            {
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboHPVTtenSheet.Text];
            }
        }

        private void txt_congtrinh_stt_EditValueChanged(object sender, EventArgs e)
        {
            dxValidationProvider.Validate();
        }

        private void Form_ImportExcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            spsheet_XemFile.Dispose();
            GC.Collect();
        }

        private void cbe_MauCongTrinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            if (CheckAuto)
                Fcn_AutoFillColum();
            IWorkbook wb = spsheet_XemFile.Document;
            lcg_TongHopCTCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (cbe_MauCongTrinh.SelectedIndex == 1)
            {
                ce_LayMotSheet.Enabled = true;
            }
            else if (cbe_MauCongTrinh.SelectedIndex == 12)
            {
                cbe_TongHop.Text = "Mẫu số 01D. Bảng kê HM ĐCG ";
                cboCTtenSheet.Text = "4.DT Cầu ";
                cbb_sheet.Text = "DGTH cầu";
                cboHPVTtenSheet.Text = "DGCT (cau)";
                cbe_DuToan.Text = "1.DT CPTV, CPK";
                lcg_TongHopCTCha.Visibility = lcg_ChiTietDuToan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabbedControlGroup2.SelectedTabPage = lcg_TongHopCTCha;
            }
            else if (cbe_MauCongTrinh.SelectedIndex == 13)
            {
                cbe_TongHop.Text = "Gia trung thau phan khai LD";
                cboCTtenSheet.Text = "Du toan XD";
                cbb_sheet.Text = "3.DG Duong";
                cboHPVTtenSheet.Text = "Don gia XD";
                lcg_TongHopCTCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabbedControlGroup2.SelectedTabPage = lcg_TongHopCTCha;
            }
            else if (cbe_MauCongTrinh.SelectedIndex == 14)
            {
                cbe_TongHop.Text = "Gia trung thau phan khai LD";
                cboCTtenSheet.Text = "Du toan XD";
                cbb_sheet.Text = "2.DG Đuong";
                cboHPVTtenSheet.Text = "Don gia XD";
                lcg_TongHopCTCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_Nhancong.Visibility = lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_GiaThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabbedControlGroup2.SelectedTabPage = lcg_TongHopCTCha;
            }
            else if (cbe_MauCongTrinh.SelectedIndex == 17)
            {
                string queryStr = $"SELECT * FROM {MyConstant.TBL_InfoReadExcel} CRE Where CRE.Id=29";
                List<InfoReadExcel> Infor = DataProvider.InstanceBaoCao.ExecuteQueryModel<InfoReadExcel>(queryStr);
                ControlsHelper.SetTextForControlByTagName(LayOut, Infor, "CongTrinh");
                cboCTtenSheet.Text = wb.Worksheets.ActiveWorksheet.Name;
            }
            if (wb.Worksheets.Contains(cboCTtenSheet.Text))
            {
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
        }

        private void txt_congtrinh_stt_Validating(object sender, CancelEventArgs e)
        {

        }

        private void hLE_File_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {

        }

        public string filePath { get; set; }

        private void cbb_sheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            IWorkbook wb = spsheet_XemFile.Document;
            if (wb.Worksheets.Contains(cbb_sheet.Text))
            {
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbb_sheet.Text];
            }
        }

        private void te_TTDocVao_EditValueChanged(object sender, EventArgs e)
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Nếu nhập vào ô này dữ liệu lúc đọc sẽ mất thời gian, Bạn có muốn tiếp tục không?????");
            if (rs == DialogResult.No)
            {
                te_TTDocVao.Text = string.Empty;
                return;
            }
        }

        private void cbe_TongHop_SelectedIndexChanged(object sender, EventArgs e)
        {
            IWorkbook wb = spsheet_XemFile.Document;
            if (wb.Worksheets.Contains(cbe_TongHop.Text))
            {
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbe_TongHop.Text];
            }
        }

        public string m_codenhathau { get; set; }
        private void Form_ImportExcel_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích File đọc vào, Vui lòng chờ!");
            De_Begin.DateTime = De_DuToanBegin.DateTime = DateTime.Now;
            De_End.DateTime = De_DuToanEnd.DateTime = DateTime.Now.AddDays(30);
            hLE_File.EditValue = filePath;
            FileHelper.fcn_spSheetStreamDocument(spsheet_XemFile, filePath);
            //spsheet_XemFile.LoadDocument(filePath);
            //spsheet_XemFile.LoadDocument(filePath);
            tencongtrinh = tenhangmuc = codehangmuc = codecongtrinh = string.Empty;
            dic_congtrinh.Clear();
            dic_hangmuc.Clear();
            dic_txtBox_CONGTRINH = new Dictionary<Control, string>()
            {
                {txtCTMaDuToan, "Mã hiệu công tác" },
                {txtCTTenCongTac, "Danh mục công tác" },
                {txtCTDonVi, "Đơn vị" },
                {txtCTKhoiLuong, "Khối lượng toàn bộ" }
            };
            dic_Nhancong = new Dictionary<Control, string>()
            {
                {txt_NC_Mahieu, "Mã hiệu" },
                {txt_NC_Tenvattu, "Tên vật tư" },
                {txt_NC_Donvi, "Đơn vị" },
                {txt_NC_Khoiluong, "Khối lượng" },
                {txt_NC_Dongia, "Giá hiện tại" }
            };
            dic_Maythicong = new Dictionary<Control, string>()
            {
                {txt_MTC_Mahieu, "Mã hiệu" },
                {txt_MTC_Tenvatlieu, "Tên vật tư" },
                {txt_MTC_Donvi, "Đơn vị" },
                {txt_MTC_Khoiluong, "Khối lượng" },
                {txt_MTC_Dongia, "Giá hiện tại" }
            };
            dic_txtduthau = new Dictionary<Control, string>()
            {
                {txt_macongtac, "Mã hiệu" },
                {txt_tencongtac_HD, "Tên công tác" },
                {txt_tendonvi, "Đơn vị" },
                {txt_klduthau, "Khối lượng dự thầu" },
                {txt_dongiaduthau, "Đơn giá dự thầu ban đầu" }
            };
            dic_VT = new Dictionary<Control, string>()
            {
                {txt_VL_Mahieu, "Mã hiệu" },
                {txt_VL_TenVL, "Tên vật tư" },
                {txt_VL_Donvi, "Đơn vị" },
                {txt_VL_Khoiluong, "Khối lượng" },
                {txt_VL_Dongia, "Giá hiện tại" }
            };
            DonGiaVL_NC_MTC = new Dictionary<string, Control>()
            {
                {"Vật liệu",txt_VL_Dongia},
                {"Nhân công",txt_NC_Dongia},
                {"Máy thi công",txt_MTC_Dongia}
            };
            DinhMucChuan = new Dictionary<string, Control>()
            {
                {"Vật liệu",txtHPVTDMCHUAN},
                {"Nhân công",txtHPVTHS_NhanCong},
                {"Máy thi công",txtHPVTHS_MTC}
            };
            dic_txtHaophiVT = new Dictionary<Control, string>()
            {
                {txtHPVTMaCongTac, "Mã hiệu" },
                {txtHPVTTenCongTac, "Tên công tác" },
                {txtHPVTDMCHUAN, "Định mức chuẩn" },
                {txtHPVTDonVi, "Đơn vị" },
                {txtHPVTKLVatTu, "Thi công" },
                {txtHPVTHS, "Hệ số vật tư" }
            };
            m_CodeDuAn = SharedControls.slke_ThongTinDuAn.EditValue.ToString();
            m_codegiadoan = SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString();
            Fcn_LoadData();
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_SetValueConTrol(LayoutControlGroup dataLayout)
        {
            var lstControls = dataLayout.Items.OfType<LayoutControlItem>().ToList();
            foreach (var item in lstControls)
            {
                if (item.Control is null)
                    continue;
                item.Control.Text = "A";
            }
        }
        private void Fcn_AutoFillColum()
        {
            IWorkbook wb = spsheet_XemFile.Document;
            string[] ten = wb.Worksheets.ToList().Select(x => x.Name).ToArray();
            if (!ten.Contains("Sheet Auto"))
            {
                lstSheetNames.Add("Sheet Auto");
                wb.Worksheets.Add().Name = "Sheet Auto";
            }
            Fcn_SetValueConTrol(lcg_DuThau);
            Fcn_SetValueConTrol(lcg_HaoPhi);
            Fcn_SetValueConTrol(lcg_Nhancong);
            Fcn_SetValueConTrol(lcg_MTC);
            Fcn_SetValueConTrol(lcg_VatLieu);
            Fcn_SetValueConTrol(lcg_GiaThang);
            cbb_haophivattu.Text = cbe_GiaThang.Text = cbb_VL_tensheet.Text =
                cbb_MTC_Tensheet.Text = cbb_NC_Tensheet.Text = cboHPVTtenSheet.Text = cbb_sheet.Text = "Sheet Auto";

        }
    }
    #endregion
}
