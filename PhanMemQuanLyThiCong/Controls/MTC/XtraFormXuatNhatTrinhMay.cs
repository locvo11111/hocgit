using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.MayThiCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.MTC
{
    public partial class XtraFormXuatNhatTrinhMay : DevExpress.XtraEditors.XtraForm
    {
        public XtraFormXuatNhatTrinhMay()
        {
            InitializeComponent();
        }
        private void Fcn_LoadTenMay()
        {
            string dbString = $"SELECT may.* FROM {MyConstant.TBL_MTC_DANHSACHMAY} may ";

            List<MTC_DanhSachMay> ChiTietMay = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_DanhSachMay>(dbString);
            slue_TenMay.Properties.DataSource = ChiTietMay;
            if (ChiTietMay.Any())
                slue_TenMay.EditValue = ChiTietMay.FirstOrDefault().Code;
        }

        private void ce_XuatTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            de_NgayBatDau.Enabled = de_NgayKetThuc.Enabled = ce_XuatTheoNgay.Checked;
        }

        private void XtraFormXuatNhatTrinhMay_Load(object sender, EventArgs e)
        {
            de_NgayBatDau.DateTime = de_NgayKetThuc.DateTime = DateTime.Now;
            Fcn_LoadTenMay();
        }

        private void sb_XuatBaoCao_Click(object sender, EventArgs e)
        {
            if (slue_TenMay.EditValue is null)
                return;

            WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu máy");
            FileHelper.fcn_spSheetStreamDocument(SheetNhatTrinh, $@"{BaseFrom.m_templatePath}\FileExcel\18.MayThiCong.xlsx");
            IWorkbook workbook = SheetNhatTrinh.Document;
            workbook.Worksheets[1].Visible = true;
            workbook.Worksheets[0].Visible = false;
            Worksheet sheet = workbook.Worksheets[1];
            CellRange RangeData = sheet.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            SheetNhatTrinh.Document.History.IsEnabled = false;
            string Codition = ce_XuatTheoNgay.Checked ? $"AND nt.NgayThang>='{de_NgayBatDau.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND" +
                $" nt.NgayThang<='{de_NgayKetThuc.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'" : "";
            SheetNhatTrinh.BeginUpdate();
            string dbString = $"SELECT da.TenDuAn,dvth.Code AS CodeDVTH, dvth.Ten as TenDonViThuchien,may.Code as CodeMayToanDuAn,may.CaMayKm," +
                                 $" dmct.TenCongTac, dmct.MaHieuCongTac,dmct.DonVi, \r\n" +
    $"nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk ON cttk.Code=nt.CodeCongTacTheoGiaiDoan " +
                 $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct \r\n" +
                              $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                    $"LEFT JOIN {MyConstant.view_DonViThucHien} dvth " +
                                    $"ON {string.Format(FormatString.CodeDonViThucHien, "cttk")} = dvth.Code " +
                                                        $"LEFT JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                    $"ON dvth.CodeDuAn = da.Code \r\n" +
$" LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} mayDA ON mayDA.Code=nt.CodeMayDuAn " +
$"LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} may ON may.Code=mayDA.CodeMay " +
$"WHERE may.Code='{slue_TenMay.EditValue}' {Codition} AND nt.CodeCongTacTheoGiaiDoan IS NOT NULL UNION ALL " +

$"SELECT da.TenDuAn,dvth.Code AS CodeDVTH, dvth.Ten as TenDonViThuchien,may.Code as CodeMayToanDuAn,may.CaMayKm," +
                                 $" cttk.TenCongTac, cttk.MaHieuCongTac,cttk.DonVi, \r\n" +
    $"nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt LEFT JOIN {MyConstant.TBL_MTC_NHATTRINHCONGTAC} cttk ON cttk.Code=nt.CodeCongTacThuCong " +
                    $"LEFT JOIN {MyConstant.view_DonViThucHienThuCong} dvth " +
                                    $"ON {string.Format(FormatString.CodeDonViThucHien, "cttk")} = dvth.Code " +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                    $"ON dvth.CodeDuAn = da.Code \r\n" +
$" LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} mayDA ON mayDA.Code=nt.CodeMayDuAn " +
$"LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} may ON may.Code=mayDA.CodeMay WHERE may.Code='{slue_TenMay.EditValue}' {Codition} AND nt.CodeCongTacThuCong IS NOT NULL";

            List<MTC_ChiTietHangNgay> ChiTietMay = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
            var GroupNgay = ChiTietMay.GroupBy(x => x.NgayThang).OrderBy(x=>x.Key);
            Row Copy = sheet.Rows[7];
            int RowIndex = 8;
            int STT = 1;
            sheet.Rows[2]["H"].SetValueFromText($"NHẬT KÝ THIẾT BỊ:{slue_TenMay.Text}");
            if (ce_XuatTheoNgay.Checked)
            {
                sheet.Rows[3]["H"].SetValueFromText($"TỪ NGÀY {de_NgayBatDau.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} ĐẾN NGÀY {de_NgayKetThuc.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}");
            }
            foreach(var item in GroupNgay)
            {
                foreach(var crow in item)
                {
                    Row Crow = sheet.Rows[RowIndex++];
                    sheet.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
                    Crow.CopyFrom(Copy, PasteSpecial.All);
                    Crow.Visible = true;
                    Crow[Name["Stt"]].SetValue(STT++);
                    Crow[Name["NgayThang"]].SetValue(crow.NgayThang);
                    Crow[Name["DonViThucHien"]].SetValueFromText(crow.TenDonViThuchien);
                    Crow[Name["MaHieuCongTac"]].SetValueFromText(crow.MaHieuCongTac);
                    Crow[Name["TenCongTac"]].SetValueFromText(crow.TenCongTac);
                    Crow[Name["DonVi"]].SetValueFromText(crow.DonVi);
                    Crow[Name["TenDuAn"]].SetValueFromText(crow.TenDuAn);

                    if (crow.CaMayKm == "Ca")
                    {
                        Crow[Name[MyConstant.GIOBATDAUSANG]].Value = crow.GioBatDauSang;
                        Crow[Name[MyConstant.GIOKETTHUCSANG]].Value = crow.GioKetThucSang;
                        Crow[Name[MyConstant.GIOBATDAUCHIEU]].Value = crow.GioBatDauChieu;
                        Crow[Name[MyConstant.GIOKETTHUCCHIEU]].Value = crow.GioKetThucChieu;
                        Crow[Name[MyConstant.GIOBATDAUTOI]].Value = crow.GioBatDauToi;
                        Crow[Name[MyConstant.GIOKETTHUCTOI]].Value = crow.GioKetThucToi;
                        Crow[Name[MyConstant.TONGGIOCHIEU]].Value = crow.TongGioChieu;
                        Crow[Name[MyConstant.TONGGIOSANG]].Value = crow.TongGioSang;
                        Crow[Name[MyConstant.TONGGIOTOI]].Value = crow.TongGioToi;
                        Crow[Name[MyConstant.TONGGIO]].Formula = $"{Crow[Name[MyConstant.TONGGIOCHIEU]].GetReferenceA1()}" +
                            $"+{Crow[Name[MyConstant.TONGGIOSANG]].GetReferenceA1()}+{Crow[Name[MyConstant.TONGGIOTOI]].GetReferenceA1()}";
                    }
                    else
                    {

                        Crow[Name[MyConstant.KmDau]].Value = crow.KmDau;
                        Crow[Name[MyConstant.KmCuoi]].Value = crow.KmCuoi;
                        Crow[Name[MyConstant.TONGGIO]].Formula = $"{Crow[Name[MyConstant.KmCuoi]].GetReferenceA1()}-{Crow[Name[MyConstant.KmDau]].GetReferenceA1()}";
                    }
                    Crow[Name[MyConstant.GHICHU]].Value = crow.GhiChu;
                    Crow[Name[MyConstant.NHIENLIEUCHINH]].Value = crow.NhienLieuChinh;
                    Crow[Name["TienTaiXe"]].SetValue(crow.TienTaiXe);
                    Crow[Name["LyLichSuaChua"]].SetValue(crow.LyLichSuaChua);
                    if (!string.IsNullOrEmpty(crow.NhienLieuPhu))
                    {
                        var ChiTietNLPhu = JsonConvert.DeserializeObject<List<MTC_NhienLieuInMay>>(crow.NhienLieuPhu);
                        string[] TenKL = ChiTietNLPhu.Select(x => $"{x.Ten}: {x.MucTieuThu}").ToArray();
                        string Display = TenKL.Any() ? string.Join(",", TenKL) : string.Empty;
                        Crow[Name[MyConstant.NHIENLIEUPHU]].SetValueFromText(Display);
                    }

                }
            }
            SheetNhatTrinh.EndUpdate();
            WaitFormHelper.CloseWaitForm();
            XtraFolderBrowserDialog Xtra = new XtraFolderBrowserDialog();
            string PathSave = "";
            if (Xtra.ShowDialog() == DialogResult.OK)
            {
                PathSave = Xtra.SelectedPath;
            }
            else
                return;
            string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
            workbook.SaveDocument(Path.Combine(PathSave, $"Báo cáo nhật trình công tác thiết bị {slue_TenMay.Text}_{time}.xlsx"), DocumentFormat.Xlsx);
            DialogResult dialogResult = XtraMessageBox.Show($"Báo cáo nhật trình công tác thiết bị {slue_TenMay.Text}_{time}.xlsx thành công. Bạn có muốn mở file không???", "QUẢN LÝ THI CÔNG - THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(Path.Combine(PathSave, $"Báo cáo nhật trình công tác thiết bị {slue_TenMay.Text}_{time}.xlsx"));
            }
        }
    }
}