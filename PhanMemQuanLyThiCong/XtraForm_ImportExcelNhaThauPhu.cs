using DevExpress.Spreadsheet;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.Excel;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
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
    public partial class XtraForm_ImportExcelNhaThauPhu : DevExpress.XtraEditors.XtraForm
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
        public string filePath { get; set; }
        private string CodeNhaThauPhu { get; set; }
        private string ColCode { get; set; }
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
            "a)Vật liệu"
        };
        public XtraForm_ImportExcelNhaThauPhu()
        {
            InitializeComponent();
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
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
            IWorkbook wb = spsheet_XemFile.Document;
            if (wb.Worksheets.Contains(cboCTtenSheet.Text))
            {
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
            }
        }

        private void tabbedControlGroup2_SelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
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
        }

        private void cbb_sheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            IWorkbook wb = spsheet_XemFile.Document;
            if (wb.Worksheets.Contains(cbb_sheet.Text))
            {
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbb_sheet.Text];
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
            else if (NameMau == MyConstant.DuToanG8)
                fcn_truyendataExxcel_congtrinhG8(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);
            else if (NameMau == MyConstant.DuToanBacnam)
                fcn_truyendataExxcel_congtrinhBacNam(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);
            else if (NameMau == MyConstant.MauThuCongCoHaoPhi)
                fcn_truyendataExxcel_DocThuCongHaoPhi(dic_txtBox_CONGTRINH, (int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1, txt_congtrinh_stt);



            DuAnHelper.GetDonViThucHiens(SharedControls.ctrl_DonViThucHienDuAnTDKH);
            this.Close();
        }
        private int Fcn_UpdateCongTacThuCongHaoPhi(long SortId, string codetheogiaidoan, DataTable dt_congtactheogiaidoan, Row crRow, Worksheet wsDonGia, int i, int RowDuThau, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
            double KL = crRow[txtCTKhoiLuong.Text].Value.NumericValue;
            string DoViThau = crRow[txtCTDonVi.Text].Value.TextValue;
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["KhoiLuongToanBo"]  = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = crRow[txtCTKhoiLuong.Text].Value.ToString() == "" ? 0 : crRow[txtCTKhoiLuong.Text].Value.NumericValue;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow_congtactheogiadoan["CodeHangMuc"] = codehangmuc;
            dtRow_congtactheogiadoan["MaHieuCongTac"]  = MaHieu;
            dtRow_congtactheogiadoan["TenCongTac"]  = tencongtac;
            dtRow_congtactheogiadoan[ColCode] = CodeNhaThauPhu;
            dtRow_congtactheogiadoan["DonVi"]  = DoViThau;
            if (CodeTuyen != default)
                dtRow_congtactheogiadoan["CodePhanTuyen"] = CodeTuyen;
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
            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
            dtRow_congtactheogiadoan["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            return RowDuThau;
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
                if (Crow[txtCTMaDuToan.Text].Value.TextValue != "" || Crow[txtCTDonVi.Text].Value.TextValue != "")
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
                //DonVi = CrowHaoPhi[txtHPVTDonVi.Text].Value.ToString();
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
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = crRow[txtHPVTHS.Text].Value.TextValue == "" ? 1 : crRow[txtHPVTHS.Text].Value.NumericValue;
                        dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.TextValue == "" ? 1 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
            return HaoPhi;
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
            string queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
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

            codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            codehangmuc = Guid.NewGuid().ToString();
            tenhangmuc = $"Hạng mục tạo mới {dt_HM.Rows.Count + 1}";
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";
            CodeNhaThauPhu = ctrl_DonViThucHienDuAn.SelectedDVTH.Code;
            ColCode = ctrl_DonViThucHienDuAn.SelectedDVTH.ColCodeFK;
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
            for (int i = firstLine; i <= lastLine; i++)
            {
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
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
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    continue;
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    NewRow["SortId"] = SortIdNhom++;
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
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            RowDuThau = Fcn_UpdateCongTacThuCongHaoPhi(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
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
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
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
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    //VL = CrowCTNhomNew[txtCTDGVatLieu.Text].Value.NumericValue;
                                    //NC = CrowCTNhomNew[txtCTDGNhanCong.Text].Value.NumericValue;
                                    //MTC = CrowCTNhomNew[txtCTDGMay.Text].Value.NumericValue;
                                    //STT = CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue;
                                    RowDuThau = Fcn_UpdateCongTacThuCongHaoPhi(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
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
                            //VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            //NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            //MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            //STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTacThuCongHaoPhi(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
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
                        continue;
                    }
                }
                else
                {
                    if (mahieu != "")
                    {
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        RowDuThau = Fcn_UpdateCongTacThuCongHaoPhi(SortId, codetheogiaidoan, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
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
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
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
            Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private int fcn_updateHaophiVTBacNam(DataTable dtHaoPhi, string vl, string nc, string mtc, string tenvattu, string donvi, int end, string codecongtactheogiaidoan, string mahieu, int HaoPhi, double STT, bool Multiple, int RowCongTrinh)
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
                if (MaHieu == mahieu && STTHaoPhi == STT && DonVi == donvi)
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
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = crRow[txtHPVTHS.Text].Value.ToString() == "" ? 0 : crRow[txtHPVTHS.Text].Value.NumericValue;

                        if (Multiple)
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[DinhMucChuan[LoaiVL].Text].Value.ToString() == "" ? 0 : crRow[DinhMucChuan[LoaiVL].Text].Value.NumericValue;
                        else
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.ToString() == "" ? 0 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
            return HaoPhi;
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
            string queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
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
            codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            codehangmuc = Guid.NewGuid().ToString();
            tenhangmuc = $"Hạng mục tạo mới {dt_HM.Rows.Count + 1}";

            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count;
            bool Mutiple = txtHPVTHS_NhanCong.Text == txtHPVTHS_MTC.Text ? false : true;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            CodeNhaThauPhu = ctrl_DonViThucHienDuAn.SelectedDVTH.Code;
            ColCode = ctrl_DonViThucHienDuAn.SelectedDVTH.ColCodeFK;
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
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
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    continue;
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    NewRow["SortId"] = SortIdNhom++;
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
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                    Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, j);
                            else
                                RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                    Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, j);
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
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
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
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    VL = CrowCTNhomNew[txtCTDGVatLieu.Text].Value.NumericValue;
                                    NC = CrowCTNhomNew[txtCTDGNhanCong.Text].Value.NumericValue;
                                    MTC = CrowCTNhomNew[txtCTDGMay.Text].Value.NumericValue;
                                    STT = CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue;
                                    RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    int End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (double.TryParse(CrowCTNhomNew[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                        RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, CrowCTNhomNew[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhomNew[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhomNew[txtCTDGMay.Text].Value.ToString(), CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, k);
                                    else
                                        RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, CrowCTNhomNew[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhomNew[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhomNew[txtCTDGMay.Text].Value.ToString(), CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, k);
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
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
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
                        continue;
                    }
                }
                else
                {
                    if (mahieu != "")
                    {
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;

                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, i);
                        else
                            RowHaoPhi = fcn_updateHaophiVTBacNam(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, i);
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
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
            Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private string Fcn_CheckLoaiVL(string Vl)
        {
            if (Vl.Contains("Vật liệu"))
                return "Vật liệu";
            else if (Vl.Contains("Nhân công"))
                return "Nhân công";
            else
                return "Máy thi công";
        }
        private int fcn_updateHaophiVTG8(DataTable dtHaoPhi, string vl, string nc, string mtc, string tenvattu, string donvi, int end, string codecongtactheogiaidoan, string mahieu, int HaoPhi, double STT, bool Multiple, int RowCongTrinh)
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
                if (MaHieu == mahieu && STTHaoPhi == STT && DonVi == donvi)
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
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = crRow[txtHPVTHS.Text].Value.ToString() == "" ? 0 : crRow[txtHPVTHS.Text].Value.NumericValue;
                        if (Multiple)
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[DinhMucChuan[LoaiVL].Text].Value.ToString() == "" ? 0 : crRow[DinhMucChuan[LoaiVL].Text].Value.NumericValue;
                        else
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.ToString() == "" ? 0 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
            return HaoPhi;
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
            string queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
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
            codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            codehangmuc = Guid.NewGuid().ToString();
            tenhangmuc = $"Hạng mục tạo mới {dt_HM.Rows.Count + 1}";

            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count;
            bool Mutiple = txtHPVTHS_NhanCong.Text == txtHPVTHS_MTC.Text ? false : true;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";
            double VL = 0, NC = 0, MTC = 0, STT = 0;
            int End = 0;
            CodeNhaThauPhu = ctrl_DonViThucHienDuAn.SelectedDVTH.Code;
            ColCode = ctrl_DonViThucHienDuAn.SelectedDVTH.ColCodeFK;
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
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
                    continue;
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    NewRow["SortId"] = SortIdNhom++;
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
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
                            SortId++;
                            End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, j);
                            else
                                RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, j);
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
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
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
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    VL = CrowCTNhomNew[txtCTDGVatLieu.Text].Value.NumericValue;
                                    NC = CrowCTNhomNew[txtCTDGNhanCong.Text].Value.NumericValue;
                                    MTC = CrowCTNhomNew[txtCTDGMay.Text].Value.NumericValue;
                                    STT = CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue;
                                    RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (double.TryParse(CrowCTNhomNew[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                        RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhomNew[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhomNew[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhomNew[txtCTDGMay.Text].Value.ToString(), CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, k);
                                    else
                                        RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhomNew[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhomNew[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhomNew[txtCTDGMay.Text].Value.ToString(), CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhomNew[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan,
                                            Mahieu, RowHaoPhi, CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, k);
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
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
                            SortId++;
                            End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(),
                                    CrowCTNhom[txtCTDGMay.Text].Value.ToString(), CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, j);
                            else
                                RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, CrowCTNhom[txtCTDGVatLieu.Text].Value.ToString(), CrowCTNhom[txtCTDGNhanCong.Text].Value.ToString(), CrowCTNhom[txtCTDGMay.Text].Value.ToString(),
                                    CrowCTNhom[txtCTTenCongTac.Text].Value.ToString(), CrowCTNhom[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, j);
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
                        continue;
                    }
                }
                else
                {
                    if (mahieu != "")
                    {
                        codetheogiaidoan = Guid.NewGuid().ToString();
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;

                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, i);
                        else
                            RowHaoPhi = fcn_updateHaophiVTG8(dtHaoPhi, crRow[txtCTDGVatLieu.Text].Value.ToString(), crRow[txtCTDGNhanCong.Text].Value.ToString(), crRow[txtCTDGMay.Text].Value.ToString(), crRow[txtCTTenCongTac.Text].Value.ToString(), crRow[txtCTDonVi.Text].Value.ToString(), lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, crRow[txt_congtrinh_stt.Text].Value.NumericValue, Mutiple, i);
                        End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
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
            Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private void Fcn_UpdateCongTacThuCong(long SortId, string codetheogiaidoan, DataTable dt_congtactheogiaidoan, Row crRow, int i, string CodeHM, bool DonGiaDuThau, bool m_CheckSheet, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            WaitFormHelper.ShowWaitForm($"{i + 1}.{tencongtac}");
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            double KL = !txtCTKhoiLuong.Text.HasValue() ? 0 : (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl) ? crRow[txtCTKhoiLuong.Text].Value.NumericValue : 0);

            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KL;
            dtRow_congtactheogiadoan[ColCode] = CodeNhaThauPhu;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow_congtactheogiadoan["CodeHangMuc"] = CodeHM;
            dtRow_congtactheogiadoan["MaHieuCongTac"]  = txtCTMaDuToan.Text != "" ? crRow[txtCTMaDuToan.Text].Value.ToString() : default;
            dtRow_congtactheogiadoan["TenCongTac"] = tencongtac;
            dtRow_congtactheogiadoan["DonVi"] = txtCTDonVi.Text != "" ? crRow[txtCTDonVi.Text].Value.ToString() : default;
            if (CodeTuyen != default)
                dtRow_congtactheogiadoan["CodePhanTuyen"] = CodeTuyen;
            dtRow_congtactheogiadoan["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            if (DonGiaDuThau && m_CheckSheet)
            {
                dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaDuThau"] = dtRow_congtactheogiadoan["DonGiaThiCong"] = crRow[txt_dongiaduthau.Text].Value.NumericValue;
            }
            if (ce_LayMotSheet.Checked && DonGiaHD.Text.HasValue())
            {
                dtRow_congtactheogiadoan["DonGia"]= dtRow_congtactheogiadoan["DonGiaDuThau"] = dtRow_congtactheogiadoan["DonGiaThiCong"] = crRow[DonGiaHD.Text].Value.NumericValue;
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
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            WaitFormHelper.CloseWaitForm();
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
                if (Crow[txt_congtrinh_stt.Text].Value.ToString() != "")
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
                        dtRow_nhom["Ten"] = Crow[txtCTTenCongTac.Text].Value.ToString().Trim(':');
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
                        dtRow_con["TenCongTac"] = Crow[txtCTTenCongTac.Text].Value.ToString();
                        if (check)
                            dtRow_con["CodeNhom"] = code_con;
                        dt_Chitietcongtaccon.Rows.Add(dtRow_con);

                    }
                }

            }
            dt_Chitietcongtaccon.AsEnumerable().ForEach(x => x["Modified"] = true);
            return End;
        }
        private void Fcn_TruyenDataExcelThuCong(Dictionary<Control, string> dic, int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
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
            codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            codehangmuc = Guid.NewGuid().ToString();
            tenhangmuc = $"Hạng mục tạo mới {dt_HM.Rows.Count + 1}";

            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = 0;

            bool CotMaHieu = txtCTMaDuToan.Text == "" ? false : true;

            bool NewCongTrinh = (rg_LayDuLieu.SelectedIndex == 1 && lUE_ToChucCaNhan.EditValue != null) ? false : true;
            bool DonGiaDuThau = txt_dongiaduthau.Text == "" ? false : true;
            bool m_CheckSheet = cboCTtenSheet.Text == cbb_sheet.Text ? true : false;
            int SortIDHM = dt_HM.Rows.Count;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", TenCongTac = "";
            CodeNhaThauPhu = ctrl_DonViThucHienDuAn.SelectedDVTH.Code;
            ColCode = ctrl_DonViThucHienDuAn.SelectedDVTH.ColCodeFK;
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
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
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = TenCongTac;
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                        continue;
                    }
                    else if (_STT.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                    {
                        continue;
                    }
                    if (!NewCongTrinh)
                    {
                        if (_STT == "*" || _STT == "*T")
                        {
                            DataRow NewRow = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRow["Code"] = CodeNhomCT;
                            NewRow["CodeHangMuc"] = codehangmuc;
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
                                    Fcn_UpdateCongTacThuCong(SortId, CodeCongTacGD, dt_congtactheogiaidoan, CrowCTNhom, j, codehangmuc, DonGiaDuThau, m_CheckSheet, CodeNhomCT);
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
                            Fcn_UpdateCongTacThuCong(SortId, CodeCongTacGD, dt_congtactheogiaidoan, crRow, i, codehangmuc, DonGiaDuThau, m_CheckSheet);
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
                    dt_NhomCT.Clear();
                    dtHaoPhi.Clear();
                    string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                    TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DonVi = crRow[txtCTDonVi.Text].Value.TextValue;
                    string STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                    WaitFormHelper.ShowWaitForm($"{i + 1}.{MaHieu}_{TenCongTac}");
                    if (NewCongTrinh)
                    {
                        if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                        {
                            codehangmuc = Guid.NewGuid().ToString();
                            tenhangmuc = TenCongTac;
                            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                            continue;
                        }
                        else if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                        {
                            continue;
                        }
                        else if (MaHieu == te_Nhom.Text)
                        {
                            string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRow = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            double KLNhom = txtCTKhoiLuong.Text.HasValue() ? crRow[txtCTKhoiLuong.Text].Value.NumericValue : 0;
                            double DonGiaNhom = DonGiaHD.Text.HasValue() ? crRow[DonGiaHD.Text].Value.NumericValue : 0;
                            NewRow["Code"] = CodeNhomCT;
                            NewRow["CodeHangMuc"] = codehangmuc;
                            NewRow["DonGia"] = DonGiaNhom;
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
                                    Fcn_UpdateCongTacThuCong(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, j, codehangmuc, DonGiaDuThau, m_CheckSheet, CodeNhomCT);
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
                            NewRow["CodeHangMuc"] = codehangmuc;
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
                                    NewRowNhom["CodeHangMuc"] = codehangmuc;
                                    NewRowNhom["Ten"] = tennhom;
                                    NewRowNhom["DonGia"] = DonGiaNhom;
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
                                            Fcn_UpdateCongTacThuCong(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhomNew, k, codehangmuc, DonGiaDuThau, m_CheckSheet, CodeNhomCT);
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
                                    Fcn_UpdateCongTacThuCong(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, j, codehangmuc, DonGiaDuThau, m_CheckSheet, CodeTuyen: CodeTuyen);
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
                            Fcn_UpdateCongTacThuCong(SortId, CodeCTTheoGD, dt_congtactheogiaidoan, crRow, i, codehangmuc, DonGiaDuThau, m_CheckSheet);
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
                    }
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                }
            }
            WaitFormHelper.CloseWaitForm();
            if (txtCTMaDuToan.Text != "")
            {
                //checkLai
                //WaitFormHelper.ShowWaitForm("Đang phân tích hao phí vật tư, Vui lòng chờ!");
                //queryStr = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,{TDKH.TBL_DanhMucCongTac}.TenCongTac,{TDKH.TBL_DanhMucCongTac}.MaHieuCongTac FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                //       $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} " +
                //       $"ON {TDKH.TBL_DanhMucCongTac}.Code={TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                //       $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan='{m_codegiadoan}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThau='{m_codenhathau}'";
                //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);

                //foreach (DataRow row in dt.Rows)
                //{
                //    if (row["MaHieuCongTac"].ToString() != "" && row["MaHieuCongTac"].ToString() != "TT")
                //    {
                //        WaitFormHelper.ShowWaitForm($"{row["MaHieuCongTac"]}_{row["TenCongTac"]}");
                //        MyFunction.fcn_TDKH_ThemDinhMucMacDinhChoCongTac(TypeKLHN.CongTac, row["Code"].ToString(), false);
                //    }
                //}
                //string[] lstCodeHM = dt_HM.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                //TDKHHelper.CapNhatAllVatTuHaoPhi(CodesHangMuc: lstCodeHM);
            }
            Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();

        }
        private void Fcn_UpdateCongTacThuCongMyHouse(long SortId, string codetheogiaidoan, DataTable dt_congtactheogiaidoan, Row crRow, int i, string CodeHM, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            WaitFormHelper.ShowWaitForm($"{i + 1}.{tencongtac}");
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            double KL = !txtCTKhoiLuong.Text.HasValue() ? 0 : (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl) ? crRow[txtCTKhoiLuong.Text].Value.NumericValue : 0);
            dtRow_congtactheogiadoan[ColCode] = CodeNhaThauPhu;
            dtRow_congtactheogiadoan["KhoiLuongToanBo"]  = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = KL;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow_congtactheogiadoan["CodeHangMuc"] = CodeHM;
            dtRow_congtactheogiadoan["MaHieuCongTac"] = txtCTMaDuToan.Text != "" ? crRow[txtCTMaDuToan.Text].Value.ToString() : default;
            dtRow_congtactheogiadoan["TenCongTac"] = tencongtac;
            dtRow_congtactheogiadoan["DonVi"] = txtCTDonVi.Text != "" ? crRow[txtCTDonVi.Text].Value.ToString() : default;
            if (CodeTuyen != default)
                dtRow_congtactheogiadoan["CodePhanTuyen"] = CodeTuyen;
            dtRow_congtactheogiadoan["PhatSinh"] = false;
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;

            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaDuThau"] = dtRow_congtactheogiadoan["DonGiaThiCong"] = crRow[DonGiaHD.Text].Value.NumericValue;
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
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            WaitFormHelper.CloseWaitForm();
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
                //double KL = crRow[txtHPVTKLVatTu.Text].Value.NumericValue;
                dt_vattuNew["Code"] = codevatu;
                dt_vattuNew["CodeCongTac"] = codecongtactheogiaidoan;
                dt_vattuNew["MaVatLieu"] = MaVT;
                dt_vattuNew["VatTu"] = TenVT;
                dt_vattuNew["DonVi"] = DonVi;
                dt_vattuNew["NgayBatDau"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                dt_vattuNew["NgayKetThuc"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                dt_vattuNew["LoaiVatTu"] = loaivattu;
                //dt_vattuNew["HeSoNguoiDung"] = dt_vattuNew["HeSo"] = crRow[txtHPVTHS.Text].Value.NumericValue==0?1: crRow[txtHPVTHS.Text].Value.NumericValue;
                dt_vattuNew["HeSoNguoiDung"] = dt_vattuNew["HeSo"] = 1;
                dt_vattuNew["DinhMucNguoiDung"] = dt_vattuNew["DinhMuc"] = crRow[DinhMucChuan[loaivattu].Text].Value.NumericValue == 0 ? 1 : crRow[DinhMucChuan[loaivattu].Text].Value.NumericValue; ;

                dt_vattuNew["DonGia"] = Fcn_UpdateDonGia(TenVT, MaVT, DonVi);

                //if (txtHPVTDonGia.Text.HasValue())
                //    dt_vattuNew["DonGia"] = crRow[txtHPVTDonGia.Text].Value.IsNumeric ? crRow[txtHPVTDonGia.Text].Value.NumericValue : 0;
                //else if()

                dtHaoPhi.Rows.Add(dt_vattuNew);
            }
            return HaoPhi;

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
        private void Fcn_TruyenDataExcelThuCongMyHouse(Dictionary<Control, string> dic, int firstLine, int lastLine)
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
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

            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = 0;

            bool CotMaHieu = txtCTMaDuToan.Text == "" ? false : true;
            codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            codehangmuc = Guid.NewGuid().ToString();
            tenhangmuc = $"Hạng mục tạo mới {dt_HM.Rows.Count + 1}";
            bool NewCongTrinh = (rg_LayDuLieu.SelectedIndex == 1 && lUE_ToChucCaNhan.EditValue != null) ? false : true;
            int SortIDHM = dt_HM.Rows.Count;
            string DonVi = "", codetheogiaidoan = "", TenCongTac = "";
            int RowHaoPhi = 2;
            double DonGiaVLKhac = 0;
            Worksheet sheetThongTin = workbook.Worksheets[cboHPVTtenSheet.Text];
            int lastline_vl = sheetThongTin.GetUsedRange().RowCount;
            CodeNhaThauPhu = ctrl_DonViThucHienDuAn.SelectedDVTH.Code;
            ColCode = ctrl_DonViThucHienDuAn.SelectedDVTH.ColCodeFK;
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
                TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                DonVi = crRow[txtCTDonVi.Text].Value.TextValue;
                string STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                WaitFormHelper.ShowWaitForm($"{i + 1}.{MaHieu}_{TenCongTac}");
                if (!NewCongTrinh)
                {
                    if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                    {
                        codehangmuc = Guid.NewGuid().ToString();
                        tenhangmuc = TenCongTac;
                        queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                        continue;
                    }
                    else if (MaHieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                    {
                        continue;
                    }
                    else if (MaHieu == te_Nhom.Text)
                    {
                        string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                        DataRow NewRow = dt_NhomCT.NewRow();
                        string CodeNhomCT = Guid.NewGuid().ToString();
                        NewRow["Code"] = CodeNhomCT;
                        NewRow["CodeHangMuc"] = codehangmuc;
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
                                DonGiaVLKhac = CrowCTNhom[txtCTDGVatLieuKhac.Text].Value.NumericValue;
                                Fcn_UpdateCongTacThuCongMyHouse(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, j, codehangmuc, CodeNhomCT);
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
                        NewRow["CodeHangMuc"] = codehangmuc;
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
                                NewRowNhom["CodeHangMuc"] = codehangmuc;
                                NewRowNhom["Ten"] = tennhom;
                                NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                                NewRowNhom["SortId"] = SortIdNhom++;
                                dt_NhomCT.Rows.Add(NewRowNhom);
                                for (int k = j + 1; k <= lastLine; k++)
                                {
                                    Row CrowCTNhomNew = ws.Rows[k];
                                    Mahieu = CrowCTNhomNew[txtCTMaDuToan.Text].Value.ToString();
                                    DonGiaVLKhac = CrowCTNhomNew[txtCTDGVatLieuKhac.Text].Value.NumericValue;
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
                                        Fcn_UpdateCongTacThuCongMyHouse(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhomNew, k, codehangmuc, CodeNhomCT);
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
                                Fcn_UpdateCongTacThuCongMyHouse(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, j, codehangmuc, CodeTuyen: CodeTuyen);
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
                        Fcn_UpdateCongTacThuCongMyHouse(SortId, CodeCongTacGD, dt_congtactheogiaidoan, crRow, i, codehangmuc);
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
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
            }
            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            Fcn_DeleteCongTrinh();
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
            string queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            codehangmuc = Guid.NewGuid().ToString();
            tenhangmuc = $"Hạng mục tạo mới {dt_HM.Rows.Count + 1}";
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count;
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            bool _ChecHM = false;
            CodeNhaThauPhu = ctrl_DonViThucHienDuAn.SelectedDVTH.Code;
            ColCode = ctrl_DonViThucHienDuAn.SelectedDVTH.ColCodeFK;
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
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
                                Fcn_UpdateCongTac360(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, j, CodeNhomCT);
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
                                NewRowNhom["CodeHangMuc"] = codehangmuc;
                                NewRowNhom["Ten"] = tennhom;
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
                                        string codetheogiaidoan = Guid.NewGuid().ToString();
                                        Fcn_UpdateCongTac360(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, j, CodeNhomCT);
                                        SortId++;
                                        int End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    }
                                }
                            }
                            else
                            {
                                string codetheogiaidoan = Guid.NewGuid().ToString();
                                Fcn_UpdateCongTac360(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, j, CodeTuyen: CodeTuyen);
                                SortId++;
                                int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            }
                        }


                    }
                }
                else
                {
                    if (mahieu != "")
                    {
                        string codetheogiaidoan = Guid.NewGuid().ToString();
                        Fcn_UpdateCongTac360(SortId, codetheogiaidoan, dt_congtactheogiaidoan, crRow, i);
                        SortId++;
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (End != 0)
                            i = End - 1;

                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
            }
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            foreach (DataRow row in dt_congtactheogiaidoan.Rows)
            {
                MyFunction.fcn_TDKH_ThemDinhMucMacDinhChoCongTac(TypeKLHN.CongTac, row["Code"].ToString(), false);
            }
            TDKHHelper.CapNhatAllVatTuHaoPhi(CodesHangMuc: new string[] { codehangmuc });
            Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private void Fcn_UpdateCongTac360(long SortId, string codetheogiaidoan, DataTable dt_congtactheogiaidoan, Row crRow, int i, string CodeNhomCongTac = null, string CodeTuyen = null)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            WaitFormHelper.ShowWaitForm($"{i + 1}.{tencongtac}");
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["KhoiLuongToanBo"] =  dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = crRow[txtCTKhoiLuong.Text].Value.ToString() == "" ? 0 : crRow[txtCTKhoiLuong.Text].Value.NumericValue;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow_congtactheogiadoan["CodeHangMuc"] = codehangmuc;
            dtRow_congtactheogiadoan["MaHieuCongTac"] =  crRow[txtCTMaDuToan.Text].Value.ToString();
            dtRow_congtactheogiadoan["TenCongTac"] =  tencongtac;
            dtRow_congtactheogiadoan["DonVi"] =  crRow[txtCTDonVi.Text].Value.ToString();
            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"] = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGiaHD.Text.HasValue() ? crRow[DonGiaHD.Text].Value.NumericValue : 0;
            dtRow_congtactheogiadoan["PhatSinh"] = false;
            dtRow_congtactheogiadoan[ColCode] = CodeNhaThauPhu;
            if (CodeTuyen != null)
                dtRow_congtactheogiadoan["CodePhanTuyen"] = CodeTuyen;
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
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            WaitFormHelper.CloseWaitForm();
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
            string queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
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
            codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            codehangmuc = Guid.NewGuid().ToString();
            tenhangmuc = $"Hạng mục tạo mới {dt_HM.Rows.Count + 1}";
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count;

            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";
            double VL = 0, NC = 0, MTC = 0, STT = 0;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            CodeNhaThauPhu = ctrl_DonViThucHienDuAn.SelectedDVTH.Code;
            ColCode = ctrl_DonViThucHienDuAn.SelectedDVTH.ColCodeFK;
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
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
                    continue;
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    NewRow["SortId"] = SortIdNhom++;
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
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
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
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
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
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    VL = CrowCTNhomNew[txtCTDGVatLieu.Text].Value.NumericValue;
                                    NC = CrowCTNhomNew[txtCTDGNhanCong.Text].Value.NumericValue;
                                    MTC = CrowCTNhomNew[txtCTDGMay.Text].Value.NumericValue;
                                    STT = CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue;
                                    RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    int End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (double.TryParse(CrowCTNhomNew[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                        RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, k, true);
                                    else
                                        RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, k, true);
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
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
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
                        continue;
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
                        STT = crRow[txt_congtrinh_stt.Text].Value.NumericValue;
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, STT, i);
                        else
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, STT, i);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
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

            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích vật liệu, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            Fcn_DeleteCongTrinh();
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
            string queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
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
            codecongtrinh = lUE_ToChucCaNhan.EditValue.ToString();
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            codehangmuc = Guid.NewGuid().ToString();
            tenhangmuc = $"Hạng mục tạo mới {dt_HM.Rows.Count+1}";

            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            Worksheet wsDonGia = workbook.Worksheets[cbb_sheet.Text];

            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count;
            int LastLineDonGia = wsDonGia.GetUsedRange().BottomRowIndex;
            int RowDuThau = 1, RowHaoPhi = 2;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";
            double VL = 0, NC = 0, MTC = 0, STT = 0;
            spsheet_XemFile.BeginUpdate();
            spsheet_XemFile.Document.History.Clear();
            CodeNhaThauPhu = ctrl_DonViThucHienDuAn.SelectedDVTH.Code;
            ColCode = ctrl_DonViThucHienDuAn.SelectedDVTH.ColCodeFK;
            queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINHANGMUC} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
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
                    continue;
                }
                else if (mahieu == te_Nhom.Text)
                {
                    string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                    DataRow NewRow = dt_NhomCT.NewRow();
                    string CodeNhomCT = Guid.NewGuid().ToString();
                    NewRow["Code"] = CodeNhomCT;
                    NewRow["CodeHangMuc"] = codehangmuc;
                    NewRow["Ten"] = tennhom;
                    NewRow["SortId"] = SortIdNhom++;
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
                            TenVL = CrowCTNhom[txtCTTenCongTac.Text].Value.ToString();
                            DonVi = CrowCTNhom[txtCTDonVi.Text].Value.ToString();
                            codetheogiaidoan = Guid.NewGuid().ToString();
                            VL = CrowCTNhom[txtCTDGVatLieu.Text].Value.NumericValue;
                            NC = CrowCTNhom[txtCTDGNhanCong.Text].Value.NumericValue;
                            MTC = CrowCTNhom[txtCTDGMay.Text].Value.NumericValue;
                            STT = CrowCTNhom[txt_congtrinh_stt.Text].Value.NumericValue;
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
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
                            NewRowNhom["CodeHangMuc"] = codehangmuc;
                            NewRowNhom["Ten"] = tennhom;
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
                                    TenVL = CrowCTNhomNew[txtCTTenCongTac.Text].Value.ToString();
                                    DonVi = CrowCTNhomNew[txtCTDonVi.Text].Value.ToString();
                                    codetheogiaidoan = Guid.NewGuid().ToString();
                                    VL = CrowCTNhomNew[txtCTDGVatLieu.Text].Value.NumericValue;
                                    NC = CrowCTNhomNew[txtCTDGNhanCong.Text].Value.NumericValue;
                                    MTC = CrowCTNhomNew[txtCTDGMay.Text].Value.NumericValue;
                                    STT = CrowCTNhomNew[txt_congtrinh_stt.Text].Value.NumericValue;
                                    RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhomNew, wsDonGia, LastLineDonGia, RowDuThau, CodeNhomCT, CodeTuyen);
                                    SortId++;
                                    int End = fcn_updatenhomdiendai(k + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                                    if (double.TryParse(CrowCTNhomNew[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                        RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, k, true);
                                    else
                                        RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, k, true);
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
                            RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, CrowCTNhom, wsDonGia, LastLineDonGia, RowDuThau, CodeTuyen: CodeTuyen);
                            SortId++;
                            int End = fcn_updatenhomdiendai(j + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                            if (double.TryParse(CrowCTNhom[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
                            else
                                RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenVL, DonVi, lastline_vl, codetheogiaidoan, Mahieu, RowHaoPhi, STT, j, true);
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
                        continue;
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
                        STT = crRow[txt_congtrinh_stt.Text].Value.NumericValue;
                        RowDuThau = Fcn_UpdateCongTac(SortId, codetheogiaidoan, dt_congtactheogiaidoan, crRow, wsDonGia, LastLineDonGia, RowDuThau);
                        SortId++;
                        int End = fcn_updatenhomdiendai(i + 1, lastLine, codetheogiaidoan, dt_NhomDienDai, dt_Chitietcongtaccon);
                        if (double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out double kl))
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, STT, i, true);
                        else
                            RowHaoPhi = fcn_updateHaophiVT(dtHaoPhi, VL, NC, MTC, TenCongTac, DonVi, lastline_vl, codetheogiaidoan, mahieu, RowHaoPhi, STT, i, true);
                        if (End != 0)
                            i = End - 1;
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
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
            Fcn_DeleteCongTrinh();
            WaitFormHelper.CloseWaitForm();
        }
        private int fcn_updateHaophiVT(DataTable dtHaoPhi, double vl, double nc, double mtc, string tenvattu, string donvi, int end, string codecongtactheogiaidoan, string mahieu, int HaoPhi, double STT, int RowCongTrinh, bool F1 = false)
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
                if (MaHieu == mahieu && STTHaoPhi == STT && DonVi == donvi)
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
            //for (int i = HaoPhi; i <= end; i++)
            //{
            //    Row CrowHaoPhi = sheetThongTin.Rows[i];
            //    string MaHieu = CrowHaoPhi[txt_macongtac.Text].Value.ToString();
            //    string ParseSTT = CrowHaoPhi[txt_STT.Text].Value.ToString();
            //    bool TryParse = double.TryParse(ParseSTT, out double STTHaoPhi);
            //    if (MaHieu == mahieu || STTHaoPhi == STT)
            //    {
            //        Fomular = CrowHaoPhi[txt_macongtac.Text].Formula.Replace($"='{cboCTtenSheet.Text}'!{txtCTMaDuToan.Text}", "");
            //        Try = int.TryParse(Fomular, out int Vitri);
            //        if (Try && Vitri - 1 == RowCongTrinh)
            //        {
            //            begin = i;
            //            break;
            //        }
            //    }
            //    else if (STTHaoPhi > STT && TryParse)
            //    {
            //        Fomular = CrowHaoPhi[txt_macongtac.Text].Formula.Replace($"='{cboCTtenSheet.Text}'!{txtCTMaDuToan.Text}", "");
            //        Try = int.TryParse(Fomular, out int Vitri);
            //        if (Try && Vitri < RowCongTrinh)
            //            continue;
            //        HaoPhi = i;
            //        return HaoPhi;
            //    }
            //}
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
                            dt_vattuNew["HeSoNguoiDung"] = dt_vattuNew["HeSo"] = crRow[txtHPVTHS.Text].Value.ToString() == "" ? 0 : crRow[txtHPVTHS.Text].Value.NumericValue;
                            if (F1)
                                dt_vattuNew["DinhMucNguoiDung"] = dt_vattuNew["DinhMuc"] = crRow[DinhMucChuan[loaivattu].Text].Value.ToString() == "" ? 0 : crRow[DinhMucChuan[loaivattu].Text].Value.NumericValue;
                            else
                                dt_vattuNew["DinhMucNguoiDung"] = dt_vattuNew["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.ToString() == "" ? 0 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                            dtHaoPhi.Rows.Add(dt_vattuNew);
                            continue;
                        }
                        rowindex = int.Parse(formula) - 1;
                        dongia = workbook.Worksheets[TenSheetVLNCMTC[loaivattu]].Rows[rowindex][DonGiaVL_NC_MTC[loaivattu].Text].Value.IsNumeric ? workbook.Worksheets[TenSheetVLNCMTC[loaivattu]].Rows[rowindex][DonGiaVL_NC_MTC[loaivattu].Text].Value.NumericValue : 0;
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
                        dt_vattu["HeSoNguoiDung"] = dt_vattu["HeSo"] = crRow[txtHPVTHS.Text].Value.ToString() == "" ? 0 : crRow[txtHPVTHS.Text].Value.NumericValue;
                        if (F1)
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[DinhMucChuan[loaivattu].Text].Value.ToString() == "" ? 0 : crRow[DinhMucChuan[loaivattu].Text].Value.NumericValue;
                        else
                            dt_vattu["DinhMucNguoiDung"] = dt_vattu["DinhMuc"] = crRow[txtHPVTDMCHUAN.Text].Value.ToString() == "" ? 0 : crRow[txtHPVTDMCHUAN.Text].Value.NumericValue;
                        dtHaoPhi.Rows.Add(dt_vattu);
                    }
                }
            }
            return HaoPhi;

        }
        private int fcn_updatenhomdiendai(int begin, int end, string codecongtac, DataTable dtNhomDienDai, DataTable dt_Chitietcongtaccon)
        {
            string code_nhom = null, code_con = Guid.NewGuid().ToString();
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[cboCTtenSheet.Text];
            //string queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            //DataTable dt_Nhomcongtac = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            //queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            //DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            //dt_Nhomcongtac.Clear();
            //dt_Chitietcongtaccon.Clear();
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
            //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Nhomcongtac, TDKH.TBL_NhomDienGiai);
            //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
            return End;
        }
        private int Fcn_UpdateCongTac(long SortId, string codetheogiaidoan, DataTable dt_congtactheogiaidoan, Row crRow, Worksheet wsDonGia, int i, int RowDuThau, string CodeNhomCongTac = default, string CodeTuyen = default)
        {
            string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
            string MaHieu = crRow[txtCTMaDuToan.Text].Value.ToString();
            double STT = crRow[txt_congtrinh_stt.Text].Value.NumericValue;
            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
            dtRow_congtactheogiadoan["Code"] = codetheogiaidoan;
            dtRow_congtactheogiadoan["SortId"] = SortId;
            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = crRow[txtCTKhoiLuong.Text].Value.ToString() == "" ? 0 : crRow[txtCTKhoiLuong.Text].Value.NumericValue;
            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
            dtRow_congtactheogiadoan["CodeHangMuc"] = codehangmuc;
            dtRow_congtactheogiadoan["MaHieuCongTac"] = MaHieu;
            dtRow_congtactheogiadoan["TenCongTac"] =  tencongtac;
            dtRow_congtactheogiadoan[ColCode] =  CodeNhaThauPhu;
            dtRow_congtactheogiadoan["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
            if (CodeTuyen != default)
                dtRow_congtactheogiadoan["CodePhanTuyen"] = CodeTuyen;
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
            double DonGia = 0;
            for (int row = RowDuThau; row <= i; row++)
            {
                Row CrowDuThau = wsDonGia.Rows[row];
                string TryParseSTT = CrowDuThau[txt_STT.Text].Value.ToString();
                bool CheckSTT = double.TryParse(TryParseSTT, out double STT_DuThau);
                string MaHieuDuThau = CrowDuThau[txt_macongtac.Text].Value.ToString();
                if (STT_DuThau == STT && MaHieu == MaHieuDuThau)
                {
                    DonGia = CrowDuThau[txt_dongiaduthau.Text].Value.NumericValue;
                    RowDuThau = row + 1;
                    break;
                }
            }
            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = DonGia;
            //dtRow["Modified"] = true;
            dtRow_congtactheogiadoan["PhatSinh"] = false;
            //if (te_KHBegin.Text.HasValue())
            //{
            //    if (crRow[te_KHBegin.Text].Value.IsDateTime)
            //        dtRow_congtactheogiadoan["NgayBatDauThiCong"] = crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            //}
            //if (te_KHEnd.Text.HasValue())
            //{
            //    if (crRow[te_KHEnd.Text].Value.IsDateTime)
            //        dtRow_congtactheogiadoan["NgayKetThucThiCong"] =crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            //}
            dtRow_congtactheogiadoan["Modified"] = true;
            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (CodeNhomCongTac != default)
                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCongTac;
            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
            return RowDuThau;
        }
        private void Fcn_DeleteCongTrinh()
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa Công trình, Hạng mục không có công tác không!!!!!!");
            if (rs == DialogResult.No)
                return;
            WaitFormHelper.ShowWaitForm("Đang xóa công trình hạng mục không có công tác, Vui lòng chờ!");
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out DataTable dtCongTrinh, out DataTable dtHangMuc, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
            foreach (DataRow row in dtCongTrinh.Rows)
            {
                string CodeHM = MyFunction.fcn_Array2listQueryCondition(dtHangMuc.AsEnumerable().Where(x => x["CodeCongTrinh"].ToString() == row["Code"].ToString()).Select(x => x["Code"].ToString()).ToArray());
                string queryStr = $"SELECT {TDKH.TBL_DanhMucCongTac}.Code FROM {TDKH.TBL_DanhMucCongTac} WHERE \"CodeHangMuc\" IN ({CodeHM})" +
                    $" UNION ALL SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.Code FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeHangMuc\" IN ({CodeHM})";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                if (dt.Rows.Count == 0)
                {
                    DuAnHelper.DeleteDataRows(MyConstant.TBL_THONGTINCONGTRINH, new string[] { row["Code"].ToString() });
                }
            }
            foreach (DataRow row in dtHangMuc.Rows)
            {
                string queryStr = $"SELECT {TDKH.TBL_DanhMucCongTac}.Code FROM {TDKH.TBL_DanhMucCongTac} WHERE \"CodeHangMuc\"='{row["Code"]}'" +
                    $" UNION ALL SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.Code FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeHangMuc\"='{row["Code"]}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);

                if (dt.Rows.Count == 0)
                {
                    DuAnHelper.DeleteDataRows(MyConstant.TBL_THONGTINHANGMUC, new string[] { row["Code"].ToString() });
                }
            }
            WaitFormHelper.CloseWaitForm();


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
            cbe_GiaThang.Text = cbb_VL_tensheet.Text =
                cbb_MTC_Tensheet.Text = cbb_NC_Tensheet.Text = cboHPVTtenSheet.Text = cbb_sheet.Text = "Sheet Auto";

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
        private void cbe_MauCongTrinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckAuto)
                Fcn_AutoFillColum();
            if (cbe_MauCongTrinh.SelectedIndex == 1)
            {
                ce_LayMotSheet.Enabled = true;
            }
            IWorkbook wb = spsheet_XemFile.Document;
            if (wb.Worksheets.Contains(cboCTtenSheet.Text))
            {
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
        }

        private void rg_LayDuLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rg_LayDuLieu.SelectedIndex == 1)
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
                ControlsHelper.SetTextForControlByTagName(lcg_CongTrinh, Infor_Eta, "CongTrinh");
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
                ControlsHelper.SetTextForControlByTagName(lcg_CongTrinh, Infor_Eta, "CongTrinh");
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
                ControlsHelper.SetTextForControlByTagName(lcg_CongTrinh, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_VatLieu, Infor_Eta, "VatLieu");

            }
            else if (NameMau == MyConstant.DuToanQLTC)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 18).ToList();
                ControlsHelper.SetTextForControlByTagName(lcg_CongTrinh, Infor_Eta, "CongTrinh");
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
                ControlsHelper.SetTextForControlByTagName(lcg_CongTrinh, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_GiaThang, Infor_Eta, "GiaThang");
            }
            else if (NameMau == MyConstant.DuToanG8)
            {
                List<InfoReadExcel> Infor_Eta = Infor.Where(x => x.Id == 6).ToList();
                ControlsHelper.SetTextForControlByTagName(lcg_CongTrinh, Infor_Eta, "CongTrinh");
                ControlsHelper.SetTextForControlByTagName(lcg_DuThau, Infor_Eta, "DuThau");
                //ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi, Infor_Eta, "HaoPhi");
                //ControlsHelper.SetTextForControlByTagName(lcg_HaoPhi1, Infor_Eta, "HaoPhi");
                ControlsHelper.SetTextForControlByTagName(lcg_GiaThang, Infor_Eta, "GiaThang");
            }
        }
        private void Fcn_LoadData()
        {
            lcg_VatLieu.Visibility = lcg_MTC.Visibility = lcg_Nhancong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
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
            cbb_sheet.Properties.Items.AddRange(ten);
            cboCTtenSheet.Properties.Items.AddRange(ten);
            cboHPVTtenSheet.Properties.Items.AddRange(ten);
            //cbb_haophivattu.Properties.Items.AddRange(ten);
            cbb_VL_tensheet.Properties.Items.AddRange(ten);
            cbb_NC_Tensheet.Properties.Items.AddRange(ten);
            cbb_MTC_Tensheet.Properties.Items.AddRange(ten);
            if (NameMau == MyConstant.DuToanQLTC)
            {
                cbb_sheet.Text = "Dự thầu- ND";
                cboHPVTtenSheet.Text = "Hao phí vật tư -ND";
                cbb_VL_tensheet.Text = "Vật liệu";
                cbb_NC_Tensheet.Text = "Nhân công";
                cbb_MTC_Tensheet.Text = "Máy thi công";
                cboCTtenSheet.Text = "Công trình";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
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
             
                cboCTtenSheet.Text = "Công trình";

                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;

            }
            else if (NameMau == MyConstant.DuToanF1)
            {
                cbb_sheet.Text = "Dự thầu";
                cboHPVTtenSheet.Text = "Hao phí vật tư";
                //cbb_haophivattu.Text = "Hao phí vật tư";
                cbb_VL_tensheet.Text = "Vật liệu";
                cbb_NC_Tensheet.Text = "Nhân công";
                cbb_MTC_Tensheet.Text = "Máy thi công";
                cboCTtenSheet.Text = "Công trình";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
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
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];

                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.DuToanEta)
            {
                cbb_sheet.Text = "ĐGTH";
                cboHPVTtenSheet.Text = "PTVT";
                cbb_VL_tensheet.Text = "Giá VL";
                cbb_NC_Tensheet.Text = "Giá NC";
                cbb_MTC_Tensheet.Text = "Giá Máy";
                cboCTtenSheet.Text = "Tiên lượng";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cboCTtenSheet.Text];
                CellRange usedRange = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.DuToanMyHouse)
            {
                cboCTtenSheet.Text = "DuToan";
                cboHPVTtenSheet.Text = "ChiTietVT";
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
        private void XtraForm_ImportExcelNhaThauPhu_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích File đọc vào, Vui lòng chờ!");
            List<DonViThucHien> DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.DataSource as List<DonViThucHien>;
            if(!DVTH.Any())
            {
                return;
                this.Close();
            }
            DonViThucHien ThauChinh = DVTH.Where(x => x.IsGiaoThau).FirstOrDefault();
            DVTH.Remove(ThauChinh);
            ctrl_DonViThucHienDuAn.DataSource = DVTH;
            De_Begin.DateTime = DateTime.Now;
            De_End.DateTime = DateTime.Now.AddDays(30);
            hLE_File.EditValue = filePath;
            //spsheet_XemFile.LoadDocument(filePath);
            FileHelper.fcn_spSheetStreamDocument(spsheet_XemFile, filePath);
            tencongtrinh = tenhangmuc = codehangmuc = "";
            dic_congtrinh.Clear();
            dic_hangmuc.Clear();
            lUE_ToChucCaNhan.Enabled = true;
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, false, true);
            lUE_ToChucCaNhan.Properties.DataSource = Infor;
            if (Infor.Count != 0)
                lUE_ToChucCaNhan.EditValue = Infor.FirstOrDefault().ID;
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
    }
    #endregion
}