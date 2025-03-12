using DevExpress.CodeParser;
using DevExpress.Mvvm.Native;
using DevExpress.PivotGrid.OLAP.Mdx;
using DevExpress.PivotGrid.SliceQueryDataSource;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Menu;
using DevExpress.XtraSpreadsheet.Mouse;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Controls.PhuongAnTaiChinh;
using PhanMemQuanLyThiCong.Model.TDKH;
using PM360.Common.Helper;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_PhuongAnTaiChinh : DevExpress.XtraEditors.XtraUserControl
    {
        const string nameRRBaoHiem = "TBT_RuiRoBaoHiem";
        const string nameRRBoiThuong = "TBT_RuiRoBoiThuong";
        const string nameRRTiGia = "TBT_RuiRoTiGia";
        const string nameRRThanhToan = "TBT_RuiRoThanhToan";
        const string nameRRDauThau = "TBT_RuiRoDauThau";
        const string nameRRNguonVon = "TBT_RuiRoNguonVon";
        const string nameRRMua = "TBT_RuiRoMua";
        const string nameRRKhac = "TBT_RuiRoKhac";
        const string nameNhaThauVaNCC = "TBT_RuiRoNhaThauNCC";
        const string nameHinhThucDauThau = "TBT_HinhThucDauThau";

        const string nameFixGiaGianTiep = "TBT_Fixed_GiaGianTiep";
        const string nameFixGiaTrucTiep = "TBT_Fixed_GiaTrucTiep";
        const string nameChiPhiCongTruong = "TBT_ChiPhiCongTruong";
        const string nameFixChiPhiCongTruong = "TBT_Fixed_ChiPhiCongTruong";
        const string nameFixMayMocThiCong = "TBT_Fixed_MayMoc";
        const string nameDienTichSanNoi = "TBT_DienTichSanNoi";

        const string nameGiaTrucTiep_TuDong = "TBT_GiaTrucTiep_TuDong";
        const string nameGiaTrucTiep_ThuCong = "TBT_GiaTrucTiep_ThuCong";

        //const string nameTTDA = "TBT_ThongTinDuAn";
        const string nameXuatDauTu = "TBT_XuatDauTu";
        const string nameThongTinDauThau = "TBT_ThongTinDauThau";
        const string nameDieuKienBaoGiaVaThucHien = "TBT_DieuKienBaoGiaVaThucHien";
        const string nameKhoanMucBaoGiaVaThucHien = "TBT_KhoanMucBaoGiaVaThucHien";
        const string nameGiaTrucTiep = "TBT_GiaTrucTiep";



        const string nameTenGopCongTac = "TBT_GopCongTac";
        //const string nameTenGopPhanDoan = "TBT_GopPhanDoan";
        const string nameTenNhaThau = "TBT_TenNhaThau";
        const string nameTenDuAn = "TBT_TenDuAn";

        const string nameData = "TBT_Data";
        const string nameTongCongChiPhi = "TBT_TongCongChiPhi";

        const string wsTbl_NguonVon = "Tbl_NguonVon";
        const string wsTbl_RuiRoMua = "Tbl_RuiRoMua";
        const string wsTbl_HinhThucDauThau = "Tbl_HinhThucDauThau";
        const string wsTbl_MucDoRuiRo = "Tbl_MuaDoRuiRo";

        const string sheetName_PAKT = "Lập PA tài chính";
        const string sheetName_TongHopCPDA = "00 Tổng hợp CPDA";
        const string sheetName_Source = "Nguồn dữ liệu";
        const string sheetName_GiaTrucTiep = "10 Giá trực tiếp";
        const string sheetName_VanPhong = "02 Văn phòng";
        const string sheetName_ThietBiThiCong = "03 Thiết bị thi công";



        const string col_Ten = "Ten";
        const string col_RuiRo = "RuiRo";
        const string col_KhoiLuong = "KhoiLuong";
        const string col_ChiTiet = "ChiTiet";
        const string col_Json = "JSON";

        const string col_STT = "STT";
        const string col_NoiDung = "NoiDung";
        const string col_GiaTri = "GiaTri";

        const string col_DonVi = "DonVi";
        const string col_GVL = "GiaVatLieu";
        const string col_GNC = "GiaNhanCong";
        const string col_GMTC = "GiaMay";

        const string col_TTVL = "TTVatLieu";
        const string col_TTNC = "TTNhanCong";
        const string col_TTMTC = "TTMay";

        const string col_SumTT = "TongCongTT";
        const string col_DGNopThau = "DonGiaNopThau";
        const string col_TTNopThau = "ThanhTienNopThau";

        const string col_MieuTa = "MieuTa";
        const string col_KhoiLuongMua = "KhoiLuongMua";
        const string col_DonGiaMua = "DonGiaMua";
        const string col_TyLeKhauHao = "TyLeKhauHao";
        const string col_ThanhTienMua = "ThanhTienMua";
        const string col_KhoiLuongThue = "KhoiLuongThue";
        const string col_DonGiaThue = "DonGiaThue";
        const string col_ThoiGianThue = "ThoiGianThue";
        const string col_ThanhTienThue = "ThanhTienThue";
        const string col_HaoPhi = "HaoPhi";
        const string col_SoCa = "SoCa";
        const string col_HeSo = "HeSo";
        const string col_ThanhTienNhienLieu = "ThanhTienNhienLieu";
        const string col_CaMayDuToan = "CaMayDuToan";
        const string col_GiaMayDuToan = "GiaMayDuToan";
        const string col_ThanhTienDuToan = "ThanhTienDuToan";
        const string col_Code = "Code";


        uc_DanhGiaRuiRoChi m_ucRuiRoChi = new uc_DanhGiaRuiRoChi();
        uc_DoBocKhoiLuong m_ucDoBoc = new uc_DoBocKhoiLuong();
        //Worksheet _wsSour;

        public XtraForm_PhuongAnTaiChinh()
        {
            InitializeComponent();

            
            m_ucRuiRoChi.de_TransRuiRoChi = new uc_DanhGiaRuiRoChi.DE_TransRuiRoChi(fcn_DE_NhanDataRuiRo);
            m_ucRuiRoChi.Dock = DockStyle.None;
            this.Controls.Add(m_ucRuiRoChi);
            m_ucRuiRoChi.Hide();


            m_ucDoBoc.de_TransDoBocKL = new uc_DoBocKhoiLuong.DE_TransDoBocKL(fcn_DE_NhanDataDoBoc);
            m_ucDoBoc.Dock = DockStyle.None;
            this.Controls.Add(m_ucDoBoc);
            m_ucDoBoc.Hide();
        }

        public void SetRibbonMinimized(bool b)
        {
            ribbonControl1.Minimized = b;
        }
        public void Save()
        {
            if (spSheet_PATC.Document.Path.HasValue())
                spSheet_PATC.SaveDocument();
        }

        public void Export()
        {
            spSheet_PATC.SaveDocument();

            var saveFileDialog = SharedControls.saveFileDialog;
            saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = $"Báo cáo Phương án tài chính - {DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.xlsx";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Xuất phương án tài chính";


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                File.Copy(spSheet_PATC.Document.Path, fileName);
                DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Xuất thành công. Bạn có muốn mở file luôn hay không ???", "Thông báo");
                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
        }
        public void LoadData(bool forceResetFile = false)
        {
            try
            {
                string dirPATC = Path.Combine(BaseFrom.m_FullTempathDA, "PhuongAnTaiChinh",
                    SharedControls.slke_ThongTinDuAn.EditValue.ToString());



                if (!Directory.Exists(dirPATC))
                    Directory.CreateDirectory(dirPATC);

                string fileName = "18. PhuongAnTaiChinhTest.xlsx";
                string path = Path.Combine(dirPATC, fileName);
                string templateFile = Path.Combine(BaseFrom.m_templatePath, "FileExcel", fileName);

                if (forceResetFile && File.Exists(path))
                {
                    File.Delete(path);
                }


                if (!File.Exists(path))
                {
                    File.Copy(templateFile, path);
                }

                spSheet_PATC.LoadDocument(path);

                var wb = spSheet_PATC.Document;
                var ws = wb.Worksheets[sheetName_PAKT];

                ws.Range[nameTenDuAn].SetValue($"TÊN DỰ ÁN: {SharedControls.slke_ThongTinDuAn.Text}");
                string dbString = $"SELECT * FROM {Server.Tbl_ThongTinNhaThau} WHERE CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
                DataRow dr = DataProvider.InstanceTHDA.ExecuteQuery(dbString).AsEnumerable().First();
                ws.Range[nameTenNhaThau].SetValue($"TÊN CƠ QUAN: {dr["Ten"]}");
            }
            catch
            {

            }
        }


        private void LoadCongTac(bool isUpdate, bool isAdd)
        {
            string dbString = $"SELECT tg.Ten As MaGop, tg.DienGiai AS TenGop, dmct.DonVi, cttk.* FROM {TDKH.TBL_TenGopCongTac} tg\r\n" +
                $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                $"ON tg.Code = dmct.CodeGop\r\n" +
                $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                $"ON dmct.Code = cttk.CodeCongTac AND cttk.CodeNhaThau IS NOT NULL\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                $"ON dmct.CodeHangMuc = hm.Code\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                $"ON hm.CodeCongTrinh = ctr.Code\r\n" +
                $"WHERE ctr.CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
            var dt = DataProvider.InstanceTHDA.ExecuteQueryModel<CongTacTheoGiaiDoanExtensionModel>(dbString);

            if (dt.Count <= 0)
                return;

            var wb = spSheet_PATC.Document;
            var ws = wb.Worksheets[sheetName_PAKT];
            var wsSource = wb.Worksheets[sheetName_Source];

            var dfn = ws.DefinedNames.GetDefinedName(nameTenGopCongTac);
            var range = ws.Range[nameTenGopCongTac];
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            wb.BeginUpdate();


            var grs = dt.AsEnumerable().GroupBy(x => x.TenGop.ToString());
            ws.Rows.Insert(range.BottomRowIndex, grs.Count(), RowFormatMode.FormatAsNext);

            int indColRuiRo = ws.Range.GetColumnIndexByName(dic[col_RuiRo]);
            var rangeValidation = ws.Range.FromLTRB(indColRuiRo, dfn.Range.TopRowIndex + 1, indColRuiRo, dfn.Range.BottomRowIndex);
            /*ws.DataValidations.Add(rangeValidation, DevExpress.Spreadsheet.DataValidationType.List,
                ValueObject.FromRange(wsSource.Tables.Single(x => x.Name == wsTbl_MucDoRuiRo).DataRange.GetRangeWithAbsoluteReference()));*/

            int indColName = ws.Range.GetColumnIndexByName(dic[col_Ten]);
            var rangeSearch = ws.Range.FromLTRB(indColName, range.TopRowIndex, indColName, range.BottomRowIndex);
            int fstInd = range.TopRowIndex + 1;
            var i = 0;
            foreach (var gr in grs)
            {
                var lsCongTac = gr.ToList();
                var fstCt = gr.First();

                string name = fstCt.TenGop;
                var cells = rangeSearch.Search(name, MyConstant.MySearchOptions);

                Row crRow = null;
                if (cells.Any())
                {
                    if (isUpdate)
                    {
                        crRow = ws.Rows[cells.First().RowIndex];
                    }
                }
                else if (isAdd)
                {
                    ws.Rows.Insert(dfn.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                    crRow = ws.Rows[dfn.Range.BottomRowIndex - 1];
                }

                if (crRow != null)
                {
                    crRow[dic[col_Ten]].SetValue(gr.First().TenGop);
                    crRow[dic[col_RuiRo]].SetValue(gr.First().DonVi);
                    crRow[dic[col_KhoiLuong]].SetValue(gr.Sum(x => x.KinhPhiTheoTienDo));
                    crRow[dic[col_ChiTiet]].SetValue(gr.Average(x => x.DonGiaThiCong));
                }
            }
            wb.EndUpdate();
        }
        
        private void LoadPhanDoan(bool isUpdate, bool isAdd)
        {
            /*string dbString = $"SELECT tg.Ten As MaGop, tg.DienGiai AS TenGop, dmct.DonVi, cttk.* " +
                $"FROM {TDKH.TBL_TenGopPhanDoan} tg\r\n" +
                                 $"JOIN {TDKH.Tbl_PhanTuyen} pt\r\n" +
                                 $"ON tg.Code = pt.CodeGop\r\n" +
                                 $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                                 $"ON dmct.CodePhanTuyen = pt.Code\r\n" +
                                 $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                                 $"ON dmct.Code = cttk.CodeCongTac AND cttk.CodeNhaThau IS NOT NULL\r\n" +
                                 $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                                 $"ON dmct.CodeHangMuc = hm.Code\r\n" +
                                 $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                                 $"ON hm.CodeCongTrinh = ctr.Code\r\n" +
                                 $"WHERE ctr.CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
            var dt = DataProvider.InstanceTHDA.ExecuteQueryModel<CongTacTheoGiaiDoanExtensionModel>(dbString);

            if (dt.Count <= 0)
                return;

            var wb = spSheet_PATC.Document;
            var ws = wb.Worksheets[sheetName_PAKT];
            var wsSource = wb.Worksheets[sheetName_Source];

            var dfn = ws.DefinedNames.GetDefinedName(nameTenGopPhanDoan);
            var range = ws.Range[nameTenGopPhanDoan];
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            wb.BeginUpdate();


            var grs = dt.AsEnumerable().GroupBy(x => x.TenGop.ToString());
            ws.Rows.Insert(range.BottomRowIndex, grs.Count(), RowFormatMode.FormatAsNext);

            int indColRuiRo = ws.Range.GetColumnIndexByName(dic[col_RuiRo]);
            var rangeValidation = ws.Range.FromLTRB(indColRuiRo, dfn.Range.TopRowIndex + 1, indColRuiRo, dfn.Range.BottomRowIndex);
            ws.DataValidations.Add(rangeValidation, DevExpress.Spreadsheet.DataValidationType.List,
                ValueObject.FromRange(wsSource.Tables.Single(x => x.Name == wsTbl_MucDoRuiRo).DataRange.GetRangeWithAbsoluteReference()));

            int indColName = ws.Range.GetColumnIndexByName(dic[col_Ten]);
            var rangeSearch = ws.Range.FromLTRB(indColName, range.TopRowIndex, indColName, range.BottomRowIndex);
            int fstInd = range.TopRowIndex + 1;
            var i = 0;

            foreach (var gr in grs)
            {
                var lsCongTac = gr.ToList();
                var fstCt = gr.First();

                string name = fstCt.TenGop;
                var cells = rangeSearch.Search(name.Trim(), MyConstant.MySearchOptions);

                Row crRow = null;
                if (cells.Any())
                {
                    if (isUpdate)
                    {
                        crRow = ws.Rows[cells.First().RowIndex];
                    }
                }
                else if (isAdd)
                {
                    ws.Rows.Insert(dfn.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                    crRow = ws.Rows[dfn.Range.BottomRowIndex - 1];
                }

                if (crRow != null)
                {
                    crRow[dic[col_Ten]].SetValue(fstCt.TenGop);
                    crRow[dic[col_RuiRo]].SetValue(fstCt.DonVi);
                    crRow[dic[col_KhoiLuong]].SetValue(gr.Sum(x => x.KhoiLuongToanBo));
                    crRow[dic[col_ChiTiet]].SetValue(gr.Average(x => x.DonGiaThiCong));
                    i++;
                }
            }
            wb.EndUpdate();*/
        }

        private void LoadDefaultContractor(bool isUpdate, bool isAdd)
        {
            string dbString = $"SELECT * FROM {MyConstant.view_DonViThucHien} " +
                $"WHERE CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            if (dt.Rows.Count <= 0)
                return;

            var wb = spSheet_PATC.Document;
            var ws = wb.Worksheets[sheetName_PAKT];
            var wsSource = wb.Worksheets[sheetName_Source];

            var dfn = ws.DefinedNames.GetDefinedName(nameNhaThauVaNCC);
            var range = ws.Range[nameNhaThauVaNCC];
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            wb.BeginUpdate();
            //ws.Rows.Insert(range.BottomRowIndex, dt.Rows.Count, RowFormatMode.FormatAsNext);

            int indColRuiRo = ws.Range.GetColumnIndexByName(dic[col_RuiRo]);
            var rangeValidation = ws.Range.FromLTRB(indColRuiRo, dfn.Range.TopRowIndex + 1, indColRuiRo, dfn.Range.BottomRowIndex);
            ws.DataValidations.Add(rangeValidation, DevExpress.Spreadsheet.DataValidationType.List,
                ValueObject.FromRange(wsSource.Tables.Single(x => x.Name == wsTbl_MucDoRuiRo).DataRange.GetRangeWithAbsoluteReference()));


            int indColName = ws.Range.GetColumnIndexByName(dic[col_Ten]);
            var rangeSearch = ws.Range.FromLTRB(indColName, range.TopRowIndex, indColName, range.BottomRowIndex);
            int fstInd = range.TopRowIndex + 1;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                //var crRow = ws.Rows[fstInd + i];
                string name = dr["Ten"].ToString();

                var cells = rangeSearch.Search(name, MyConstant.MySearchOptions);

                Row crRow = null;
                if (cells.Any())
                {
                    if (isUpdate)
                    {
                        crRow = ws.Rows[cells.First().RowIndex];
                    }
                }
                else if (isAdd)
                {
                    ws.Rows.Insert(dfn.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                    crRow = ws.Rows[dfn.Range.BottomRowIndex - 1];
                }
                
                if (crRow != null)
                    crRow[dic[col_Ten]].SetValue(dr["Ten"]);

            }
            wb.EndUpdate();
        }

        private void XtraForm_PhuongAnTaiChinh_Load(object sender, EventArgs e)
        {
            
        }

        private void spSheet_PATC_CellBeginEdit(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellCancelEventArgs e)
        {
            var wb = spSheet_PATC.Document;
            var ws = spSheet_PATC.ActiveWorksheet;
            var wsSource = wb.Worksheets[sheetName_Source];
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            
            var spsheetLoc = spSheet_PATC.Location;
            if (e.Worksheet.Name == sheetName_PAKT)
            {
                if (e.ColumnIndex == ws.Range.GetColumnIndexByName(dic[col_RuiRo])
                    && 
                    (
                    ws.Range[nameRRBaoHiem].Contains(e.Cell)
                    || ws.Range[nameRRBoiThuong].Contains(e.Cell)
                    || ws.Range[nameRRTiGia].Contains(e.Cell)
                    || ws.Range[nameRRThanhToan].Contains(e.Cell)
                    )
                    )
                {
                    e.Cancel = true;

                    var crRow = ws.Rows[e.RowIndex];

                    string jsonString = crRow[dic[col_Json]].Value.ToString();
                    
                    var sources = wsSource.Tables.Single(x => x.Name == wsTbl_RuiRoMua)
                                    .DataRange.AsEnumerable()
                                    .Select(x => x.Value.ToString()).Where(x => x.HasValue()).Distinct().ToList();

                    m_ucRuiRoChi.Show();
                    Rectangle rec = spSheet_PATC.GetCellBounds(e.RowIndex, e.ColumnIndex);
                    m_ucRuiRoChi.Location = new Point(rec.Right + spsheetLoc.X, rec.Top + spsheetLoc.Y);
                    m_ucRuiRoChi.BringToFront();
                    m_ucRuiRoChi.LoadData(jsonString, sources);
                }
                else if (ws.Range[nameXuatDauTu].Contains(e.Cell) && e.Cell.ColumnIndex == ws.Range.GetColumnIndexByName(dic[col_KhoiLuong]))
                {
                    e.Cancel = true;

                    var crRow = ws.Rows[e.RowIndex];

                    string jsonString = crRow[dic[col_Json]].Value.ToString();

                    var sources = wsSource.Tables.Single(x => x.Name == wsTbl_RuiRoMua)
                                    .DataRange.AsEnumerable()
                                    .Select(x => x.Value.ToString()).Where(x => x.HasValue()).Distinct().ToList();

                    m_ucDoBoc.Show();
                    Rectangle rec = spSheet_PATC.GetCellBounds(e.RowIndex, e.ColumnIndex);
                    m_ucDoBoc.Location = new Point(rec.Right + spsheetLoc.X, rec.Top + spsheetLoc.Y);
                    m_ucDoBoc.BringToFront();
                    m_ucDoBoc.PushData(crRow[dic[col_Ten]].Value.ToString(), jsonString);
                }
            }
        }

        private void fcn_DE_NhanDataDoBoc(string encrypted, string DonVi, double SoBoPhanGiongNhau, double KhoiLuong)
        {
            var ws = spSheet_PATC.ActiveWorksheet;
            

            var crRow = ws.Rows[ws.SelectedCell.TopRowIndex];

            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());


            crRow[dic[col_RuiRo]].SetValue(SoBoPhanGiongNhau);
            crRow[dic[col_Json]].SetValue(encrypted);
            crRow[dic[col_KhoiLuong]].SetValue(KhoiLuong);
            crRow[dic[col_KhoiLuong]].NumberFormat = $"{MyConstant.EXCELFORMATNUM}\"{DonVi}\"";
            
        }
        
        private void fcn_DE_NhanDataRuiRo(string EncryptedText, string DispText)
        {
            var ws = spSheet_PATC.ActiveWorksheet;
            

            var crRow = ws.Rows[ws.SelectedCell.TopRowIndex];

            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            if (dic.ContainsKey(col_RuiRo))
            {
                crRow[dic[col_RuiRo]].SetValue(DispText);
            }
            
            if (dic.ContainsKey(col_Json))
            {
                crRow[dic[col_Json]].SetValue(EncryptedText);
            }
        }

        private void spSheet_PATC_SelectionChanged(object sender, EventArgs e)
        {
            m_ucRuiRoChi.Hide();
            m_ucDoBoc.Hide();
        }

        private void spSheet_PATC_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {
            var wb = spSheet_PATC.Document;
            var ws = spSheet_PATC.ActiveWorksheet;



            var dfns = ws.DefinedNames.Where(x => x.Name.StartsWith("TBT_"));

            foreach (var dfn in dfns)
            {
                var range = dfn.Range;
                if (range.Contains(e.Cell) && range.BottomRowIndex == e.RowIndex)
                {
                    ws.Rows.Insert(range.BottomRowIndex + 1, 1, RowFormatMode.FormatAsPrevious);
                    dfn.Range = ws.Range.FromLTRB(range.LeftColumnIndex, range.TopRowIndex, range.RightColumnIndex, range.BottomRowIndex + 1);
                }
            }

            if (ws.Name == sheetName_PAKT)
            {
/*                var dfns = ws.DefinedNames.Where(x => x.Name.StartsWith("TBT_"));

                foreach (var dfn in dfns)
                {
                    var range = dfn.Range;
                    if (range.Contains(e.Cell) && range.BottomRowIndex == e.RowIndex)
                    {
                        ws.Rows.Insert(range.BottomRowIndex + 1, 1, RowFormatMode.FormatAsPrevious);
                        dfn.Range = ws.Range.FromLTRB(range.LeftColumnIndex, range.TopRowIndex, range.RightColumnIndex, range.BottomRowIndex + 1);
                    }
                }*/
            }
            else if (ws.Name == sheetName_TongHopCPDA )
            {
                var dfn = ws.DefinedNames.Single(x => x.Name == nameData);
                var crRow = ws.Rows[e.RowIndex];
                var nextRow = ws.Rows[e.RowIndex + 1];
                var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

                var nextSTT = nextRow[dic[col_STT]].Value.ToString();

                bool isParse = int.TryParse(nextSTT, out int stt);




                if (e.RowIndex == dfn.Range.BottomRowIndex || isParse)
                {
                    ws.Rows.Insert(e.RowIndex + 1, 1, RowFormatMode.FormatAsPrevious);
                }

                    if (e.ColumnIndex != ws.Range.GetColumnIndexByName(dic[col_STT]))
                    {
                        string regexCon = @"(\d+).(\d+)";

                        int prevInd = e.RowIndex - 1;

                        var prevSTT = ws.Rows[prevInd][dic[col_STT]].Value.ToString();

                        bool isPrevParse = int.TryParse(prevSTT, out int prevStt);

                        Match m = Regex.Match(prevSTT, regexCon, RegexOptions.IgnoreCase);
                        if (isPrevParse)
                        {
                            crRow[dic[col_STT]].SetValue($"{prevSTT}.1");
                        }
                        else if (m != Match.Empty)
                        {
 /*                           foreach (var dfnLoop in ws.DefinedNames.Where(x => x.Name.StartsWith("TBT_") && x.Range.BottomRowIndex == e.RowIndex))
                            {
                                var range = dfnLoop.Range;
                                dfnLoop.Range = ws.Range.FromLTRB(range.LeftColumnIndex, range.TopRowIndex, range.RightColumnIndex, range.BottomRowIndex + 1);
                            }*/

                            int fst = int.Parse(m.Groups[1].Value);
                            int snd = int.Parse(m.Groups[2].Value);
                            crRow[dic[col_STT]].SetValue($"{fst}.{snd + 1}");

                            int indColSTT = ws.Range.GetColumnIndexByName(dic[col_STT]);
                            var cell = ws.Range.FromLTRB(indColSTT, dfn.Range.TopRowIndex, indColSTT, e.RowIndex - 1)
                                .Search(fst.ToString(), MyConstant.MySearchOptions).LastOrDefault();
                            if (cell != null)
                            {
                                ws.Rows[cell.RowIndex][dic[col_GiaTri]].Formula += $"+ {dic[col_GiaTri]}{e.RowIndex + 1}";
                            }

                        }
                        else
                        {
                            crRow[dic[col_STT]].SetValue($"1");
                        }
                    //}
                }
            }
            else if (ws.Name == sheetName_GiaTrucTiep  && ws.Range[nameGiaTrucTiep_ThuCong].Contains(e.Cell))
            {
                var dfn = ws.DefinedNames.Single(x => x.Name == nameGiaTrucTiep_ThuCong);
                var crRow = ws.Rows[e.RowIndex];
                var nextRow = ws.Rows[e.RowIndex + 1];
                var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

                var nextSTT = nextRow[dic[col_STT]].Value.ToString();

                bool isParse = int.TryParse(nextSTT, out int stt);




                if (e.RowIndex == dfn.Range.BottomRowIndex || isParse)
                {
                    ws.Rows.Insert(e.RowIndex + 1, 1, RowFormatMode.FormatAsPrevious);
                }

                if (e.ColumnIndex != ws.Range.GetColumnIndexByName(dic[col_STT]))
                {
                    string regexCon = @"(\d+).(\d+)";

                    int prevInd = e.RowIndex - 1;

                    var prevSTT = ws.Rows[prevInd][dic[col_STT]].Value.ToString();

                    bool isPrevParse = int.TryParse(prevSTT, out int prevStt);

                    Match m = Regex.Match(prevSTT, regexCon, RegexOptions.IgnoreCase);
                    if (isPrevParse)
                    {
                        crRow[dic[col_STT]].SetValue($"{prevSTT}.1");
                    }
                    else if (m != Match.Empty)
                    {
                        /*                           foreach (var dfnLoop in ws.DefinedNames.Where(x => x.Name.StartsWith("TBT_") && x.Range.BottomRowIndex == e.RowIndex))
                                                   {
                                                       var range = dfnLoop.Range;
                                                       dfnLoop.Range = ws.Range.FromLTRB(range.LeftColumnIndex, range.TopRowIndex, range.RightColumnIndex, range.BottomRowIndex + 1);
                                                   }*/

                        int fst = int.Parse(m.Groups[1].Value);
                        int snd = int.Parse(m.Groups[2].Value);
                        crRow[dic[col_STT]].SetValue($"{fst}.{snd + 1}");

                        int indColSTT = ws.Range.GetColumnIndexByName(dic[col_STT]);
                        var cell = ws.Range.FromLTRB(indColSTT, dfn.Range.TopRowIndex, indColSTT, e.RowIndex - 1)
                            .Search(fst.ToString(), MyConstant.MySearchOptions).LastOrDefault();
                        if (cell != null)
                        {
                            string[] colsSum = new string[]
{
                col_TTVL,
                col_TTNC,
                col_TTMTC,
                col_SumTT,
                col_TTNopThau
};

                            foreach (string col in colsSum)
                                ws.Rows[cell.RowIndex][dic[col]].Formula += $"+ {dic[col]}{e.RowIndex + 1}";
                        }

                    }
                    else
                    {
                        crRow[dic[col_STT]].SetValue($"1");
                    }
                    //}
                }
            }
        }
        
        private void spSheet_PATC_CellEndEdit(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellValidatingEventArgs e)
        {

        }

        private void spSheet_PATC_RowsRemoving(object sender, RowsChangingEventArgs e)
        {
            var wb = spSheet_PATC.Document;
            var ws = spSheet_PATC.ActiveWorksheet;

            if (ws.Name == sheetName_PAKT)
            {
                var dfns = ws.DefinedNames.Where(x => x.Name.StartsWith("TBT_"));

                foreach (var dfn in dfns)
                {
                    var range = dfn.Range;
                    if ((range.TopRowIndex >= e.StartIndex && range.TopRowIndex <= e.StartIndex + e.Count - 1)
                        || (range.BottomRowIndex >= e.StartIndex && range.BottomRowIndex <= e.StartIndex + e.Count - 1))
                    {
                        MessageShower.ShowError("Chỉ được xóa các dòng có dữ liệu và cùng nằm trong 1 bảng dữ liệu\r\n" +
                            "(Không chọn dòng tiêu đề đầu bảng hoặc dòng trống cuối bảng)");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void spSheet_PATC_RowsInserted(object sender, RowsChangedEventArgs e)
        {
            var wb = spSheet_PATC.Document;
            var ws = spSheet_PATC.ActiveWorksheet;

            if (ws.Name == sheetName_PAKT)
            {
                var dfns = ws.DefinedNames.Where(x => x.Name.StartsWith("TBT_"));

                foreach (var dfn in dfns)
                {
                    var range = dfn.Range;
                    if (range.BottomRowIndex == e.StartIndex + 1)
                    {
                        dfn.Range = ws.Range.FromLTRB(dfn.Range.LeftColumnIndex, dfn.Range.TopRowIndex,
                            dfn.Range.RightColumnIndex, e.StartIndex + e.Count - 1);
                    }
                }
            }
        }
        private void spSheet_PATC_PopupMenuShowing(object sender, DevExpress.XtraSpreadsheet.PopupMenuShowingEventArgs e)
        {
            IWorkbook wb = spSheet_PATC.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            //Dictionary<string, string> TDKH_DBC.dic_Dbc = MyFunction.fcn_getDicOfColumn(wb.Range[TDKH_DBC.RANGE_DoBocChuan]);
            Cell cell = ws.SelectedCell[0];


            SpreadsheetMenuItem itemCapNhatWhole = new SpreadsheetMenuItem("CẬP NHẬT LẠI TOÀN BỘ DỮ LIỆU", fcn_Handle_CapNhatWhole);
            itemCapNhatWhole.Appearance.ForeColor = Color.Blue;
            itemCapNhatWhole.Appearance.Font = new Font(itemCapNhatWhole.Appearance.Font, FontStyle.Bold);
            e.Menu.Items.Add(itemCapNhatWhole);

            SpreadsheetMenuItem itemCapNhatAll = new SpreadsheetMenuItem("Cập nhật số liệu đã lấy vào!", fcn_Handle_CapNhatAll);
            itemCapNhatAll.Appearance.ForeColor = Color.Blue;
            itemCapNhatAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
            e.Menu.Items.Add(itemCapNhatAll);

            SpreadsheetMenuItem itemCapNhatVaThemAll = new SpreadsheetMenuItem("Thêm và cập nhật mới số liệu", fcn_Handle_CapNhatVaThemAll);
            itemCapNhatVaThemAll.Appearance.ForeColor = Color.Blue;
            itemCapNhatVaThemAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
            e.Menu.Items.Add(itemCapNhatVaThemAll);

            SpreadsheetMenuItem itemChiThemAll = new SpreadsheetMenuItem("Thêm số liệu mặc định (Không cập nhật)", fcn_Handle_ChiThemAll);
            itemChiThemAll.Appearance.ForeColor = Color.Blue;
            itemChiThemAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
            e.Menu.Items.Add(itemChiThemAll);

            if (ws.Name == sheetName_PAKT)
            {
                if (ws.Range[nameNhaThauVaNCC].Contains(cell))
                {
                    SpreadsheetMenuItem itemCapNhat = new SpreadsheetMenuItem("Cập nhật \"Nhà Thầu/Nhà cung cấp\" đã lấy vào!", fcn_Handle_CapNhatNhaThau);
                    e.Menu.Items.Add(itemCapNhat);

                    SpreadsheetMenuItem itemCapNhatVaThem = new SpreadsheetMenuItem("Thêm và cập nhật mới các \"Nhà Thầu/Nhà cung cấp\"", fcn_Handle_CapNhatVaThemNhaThau);
                    e.Menu.Items.Add(itemCapNhatVaThem);
                    
                    SpreadsheetMenuItem itemChiThem = new SpreadsheetMenuItem("Thêm \"Nhà Thầu/Nhà cung cấp\" (Không cập nhật)", fcn_Handle_ChiThemNhaThau);
                    e.Menu.Items.Add(itemChiThem);
                }
                else if (ws.Range[nameTenGopCongTac].Contains(cell))
                {
                    SpreadsheetMenuItem itemCapNhat = new SpreadsheetMenuItem("Cập nhật \"Công tác\" đã lấy vào!", fcn_Handle_CapNhatCongTacGop);
                    e.Menu.Items.Add(itemCapNhat);

                    SpreadsheetMenuItem itemCapNhatVaThem = new SpreadsheetMenuItem("Thêm và cập nhật mới các \"Công tác\"", fcn_Handle_CapNhatVaThemCongTacGop);
                    e.Menu.Items.Add(itemCapNhatVaThem);

                    SpreadsheetMenuItem itemChiThem = new SpreadsheetMenuItem("Thêm \"Công tác\" (Không cập nhật)", fcn_Handle_ChiThemCongTacGop);
                    e.Menu.Items.Add(itemChiThem);
                }
                else if (ws.Range[nameFixGiaTrucTiep].Contains(cell))
                {
                    SpreadsheetMenuItem itemCapNhat = new SpreadsheetMenuItem("Cập nhật chi phí trực tiếp", Handle_CapNhatChiPhiTrucTiep);
                    e.Menu.Items.Add(itemCapNhat);
                }
                else if (ws.Range[nameFixGiaGianTiep].Contains(cell))
                {
                    SpreadsheetMenuItem itemGet = new SpreadsheetMenuItem("Cập nhật hạng mục chi phí gián tiếp", Handle_CapNhatChiPhiGianTiep);
                    e.Menu.Items.Add(itemGet);
                }
                else if (ws.Range[nameFixChiPhiCongTruong].Contains(cell))
                {
                    SpreadsheetMenuItem itemGet = new SpreadsheetMenuItem("Cập nhật chi phí công trường", Handle_CapNhatChiPhiCongTruong);
                    e.Menu.Items.Add(itemGet);
                }
                

            }
            else if (ws.Name == sheetName_TongHopCPDA)
            {
                //if (ws.Range[nameChiPhiCongTruong].Contains(cell))
                //{
                    //SpreadsheetMenuItem itemCapNhat = new SpreadsheetMenuItem("Cập nhật \"Nhà Thầu/Nhà cung cấp\" đã lấy vào!", fcn_Handle_CapNhatNhaThau);
                    //e.Menu.Items.Add(itemCapNhat);

                    //SpreadsheetMenuItem itemCapNhatVaThem = new SpreadsheetMenuItem("Thêm và cập nhật mới các \"Nhà Thầu/Nhà cung cấp\"", fcn_Handle_CapNhatVaThemNhaThau);
                    //e.Menu.Items.Add(itemCapNhatVaThem);

                    SpreadsheetMenuItem itemChiThem = new SpreadsheetMenuItem("Cập nhật tự động (Máy thi công)", Handle_CapNhatMTCGiaGianTiep);
                    e.Menu.Items.Add(itemChiThem);
                //}
            }    
            else if (ws.Name == sheetName_GiaTrucTiep)
            {
                //if (ws.Range[nameGiaTrucTiep_TuDong].Contains(cell))
                //{
                SpreadsheetMenuItem itemGet = new SpreadsheetMenuItem("Cập nhật tự động chi phí từ Kế Hoạch", Handle_CapNhatChiPhiTrucTiepTuDong);
                e.Menu.Items.Add(itemGet);
                
                
                SpreadsheetMenuItem itemGetThuCong = new SpreadsheetMenuItem("Lấy công tác thủ công", Handle_ThemCongTacThuCong);
                    e.Menu.Items.Add(itemGetThuCong);
                //}    
            }
            
            else if (ws.Name == sheetName_ThietBiThiCong)
            {
                //if (ws.Range[nameGiaTrucTiep_TuDong].Contains(cell))
                //{
                SpreadsheetMenuItem itemGet = new SpreadsheetMenuItem("Lấy máy thi công", Handle_LayMayThiCong);
                e.Menu.Items.Add(itemGet);
                //}    
            }
        }


        private void fcn_Handle_CapNhatAll(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Các số liệu lấy vào từ trước sẽ cập nhật theo số liệu mới nhất?");
            if (dr == DialogResult.Yes)
            {
                LoadDefaultContractor(true, false);
                LoadCongTac(true, false);
                LoadPhanDoan(true, false);
            }
        }
        
        private void fcn_Handle_CapNhatWhole(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Các số liệu lấy vào từ trước sẽ cập nhật theo số liệu mới nhất?");
            if (dr == DialogResult.Yes)
            {
                LoadDefaultContractor(true, false);
                LoadCongTac(true, false);
                LoadPhanDoan(true, false);
                Handle_CapNhatChiPhiGianTiep(null, null);
                Handle_CapNhatChiPhiCongTruong(null, null);
                Handle_CapNhatChiPhiTrucTiep(null, null);
            }
        }

        private void fcn_Handle_CapNhatVaThemAll(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Các số liệu lấy vào từ trước sẽ cập nhật theo số liệu mới nhất?\r\n" +
                "Các số liệu mới sẽ được thêm vào!");
            if (dr == DialogResult.Yes)
            {
                LoadDefaultContractor(true, true);
                LoadCongTac(true, true);
                LoadPhanDoan(true, true);
                Handle_CapNhatChiPhiGianTiep(null, null);
                Handle_CapNhatChiPhiCongTruong(null, null);
                Handle_CapNhatChiPhiTrucTiep(null, null);
            }
        }

        private void fcn_Handle_ChiThemAll(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Chỉ thêm các số liệu mới, KHÔNG cập nhật các số liệu đã thêm trước đó!");
            if (dr == DialogResult.Yes)
            {
                LoadDefaultContractor(false, true);
                LoadCongTac(false, true);
                LoadPhanDoan(false, true);
                Handle_CapNhatChiPhiGianTiep(null, null);
                Handle_CapNhatChiPhiCongTruong(null, null);
                Handle_CapNhatChiPhiTrucTiep(null, null);
            }
        }

        private void fcn_Handle_CapNhatNhaThau(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Các số liệu lấy vào từ trước sẽ cập nhật theo số liệu mới nhất?");
            if (dr == DialogResult.Yes)
            {
                LoadDefaultContractor(true, false);
            }
        }
        
        private void fcn_Handle_CapNhatVaThemNhaThau(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Các số liệu lấy vào từ trước sẽ cập nhật theo số liệu mới nhất?\r\n" +
                "Các số liệu mới sẽ được thêm vào!");
            if (dr == DialogResult.Yes)
            {
                LoadDefaultContractor(true, true);
            }
        }
        
        private void fcn_Handle_ChiThemNhaThau(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Chỉ thêm các số liệu mới, KHÔNG cập nhật các số liệu đã thêm trước đó!");
            if (dr == DialogResult.Yes)
            {
                LoadDefaultContractor(false, true);
            }
        }
        
        private void fcn_Handle_CapNhatCongTacGop(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Các số liệu lấy vào từ trước sẽ cập nhật theo số liệu mới nhất?");
            if (dr == DialogResult.Yes)
            {
                LoadCongTac(true, false);
            }
        }
        
        private void fcn_Handle_CapNhatVaThemCongTacGop(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Các số liệu lấy vào từ trước sẽ cập nhật theo số liệu mới nhất?\r\n" +
                "Các số liệu mới sẽ được thêm vào!");
            if (dr == DialogResult.Yes)
            {
                LoadCongTac(true, true);
            }
        }
        
        private void fcn_Handle_ChiThemCongTacGop(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Chỉ thêm các số liệu mới, KHÔNG cập nhật các số liệu đã thêm trước đó!");
            if (dr == DialogResult.Yes)
            {
                LoadCongTac(false, true);
            }
        }
        
        private void fcn_Handle_CapNhatPhanDoan(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Các số liệu lấy vào từ trước sẽ cập nhật theo số liệu mới nhất?");
            if (dr == DialogResult.OK)
            {
                LoadPhanDoan(true, false);
            }
        }
        
        private void fcn_Handle_CapNhatVaThemPhanDoan(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Các số liệu lấy vào từ trước sẽ cập nhật theo số liệu mới nhất?\r\n" +
                "Các số liệu mới sẽ được thêm vào!");
            if (dr == DialogResult.OK)
            {
                LoadPhanDoan(true, true);
            }
        }
        
        private void fcn_Handle_ChiThemPhanDoan(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Chỉ thêm các số liệu mới, KHÔNG cập nhật các số liệu đã thêm trước đó!");
            if (dr == DialogResult.OK)
            {
                LoadPhanDoan(false, true);
            }
        }

        private void Handle_CapNhatChiPhiGianTiep(object obj, EventArgs args)
        {
            var wb = spSheet_PATC.Document;
            var wsMain = wb.Worksheets[sheetName_PAKT];
            var wsCPDA = wb.Worksheets[sheetName_TongHopCPDA];

            var dicMain = MyFunction.fcn_getDicOfColumn(wsMain.GetUsedRange());
            var dicCPDA = MyFunction.fcn_getDicOfColumn(wsCPDA.GetUsedRange());

            var dfnGiaGianTiep = wsMain.DefinedNames.Single(x => x.Name == nameFixGiaGianTiep);

            var range = dfnGiaGianTiep.Range;
            if (dfnGiaGianTiep.Range.RowCount > 2)
            {
                wsMain.Rows.Remove(range.TopRowIndex + 1, dfnGiaGianTiep.Range.RowCount - 2);
            }

            var rangeDataCP = wsCPDA.Range[nameData];

            wb.BeginUpdate();

            for (int i = rangeDataCP.TopRowIndex; i <= rangeDataCP.BottomRowIndex; i++)
            {
                Row crRowCPDA = wsCPDA.Rows[i];
                string sttString = crRowCPDA[dicCPDA[col_STT]].Value.ToString();

                bool isParse = int.TryParse(sttString, out int stt);
                if (isParse && stt > 0)
                {
                    wsMain.Rows.Insert(dfnGiaGianTiep.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                    var crRowMain = wsMain.Rows[dfnGiaGianTiep.Range.BottomRowIndex - 1];
                    crRowMain.Font.Bold = false;
                    crRowMain[dicMain[col_Ten]].Formula = $"'{sheetName_TongHopCPDA}'!{dicCPDA[col_NoiDung]}{i + 1}";
                    crRowMain[dicMain[col_KhoiLuong]].Formula = $"'{sheetName_TongHopCPDA}'!{dicCPDA[col_GiaTri]}{i + 1}";

                    if (wsMain.DefinedNames.Contains(nameDienTichSanNoi))
                        crRowMain[dicMain[col_RuiRo]].Formula = $"IF(TBT_DienTichSanNoi>0;{dicMain[col_KhoiLuong]}{crRowMain.Index + 1}/TBT_DienTichSanNoi;\"\")";
                
                }
            }

            if (dfnGiaGianTiep.Range.Count() > 2)
            {
                wsMain.Rows[dfnGiaGianTiep.Range.BottomRowIndex][dicMain[col_KhoiLuong]].Formula 
                    = $"SUM({dicMain[col_KhoiLuong]}{dfnGiaGianTiep.Range.TopRowIndex + 2}:{dicMain[col_KhoiLuong]}{dfnGiaGianTiep.Range.BottomRowIndex})";
            }
            else
                wsMain.Rows[dfnGiaGianTiep.Range.BottomRowIndex][dicMain[col_KhoiLuong]].SetValue(0);


            wb.EndUpdate();
        }


        private void Handle_CapNhatMTCGiaGianTiep(object obj, EventArgs args)
        {
            var wb = spSheet_PATC.Document;
            var wsMain = wb.Worksheets[sheetName_TongHopCPDA];
            var wsCPDA = wb.Worksheets[sheetName_ThietBiThiCong];

            var dicMain = MyFunction.fcn_getDicOfColumn(wsMain.GetUsedRange());
            var dicCPDA = MyFunction.fcn_getDicOfColumn(wsCPDA.GetUsedRange());

            var dfnGiaGianTiep = wsMain.DefinedNames.Single(x => x.Name == nameFixMayMocThiCong);

            var range = dfnGiaGianTiep.Range;
            if (dfnGiaGianTiep.Range.RowCount > 2)
            {
                wsMain.Rows.Remove(range.TopRowIndex + 1, dfnGiaGianTiep.Range.RowCount - 2);
            }

            var rangeDataCP = wsCPDA.Range[nameData];

            wb.BeginUpdate();

            for (int i = rangeDataCP.TopRowIndex; i <= rangeDataCP.BottomRowIndex; i++)
            {
                Row crRowCPDA = wsCPDA.Rows[i];
                string sttString = crRowCPDA[dicCPDA[col_STT]].Value.ToString();

                bool isParse = int.TryParse(sttString, out int stt);
                if (isParse && stt > 0)
                {
                    wsMain.Rows.Insert(dfnGiaGianTiep.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                    var crRowMain = wsMain.Rows[dfnGiaGianTiep.Range.BottomRowIndex - 1];
                    crRowMain.Font.Bold = false;
                    crRowMain[dicMain[col_NoiDung]].Formula = $"'{sheetName_ThietBiThiCong}'!{dicCPDA[col_NoiDung]}{i + 1}";
                    crRowMain[dicMain[col_GiaTri]].Formula = $"'{sheetName_ThietBiThiCong}'!{dicCPDA[col_ThanhTienDuToan]}{i + 1}";

                    //if (wsMain.DefinedNames.Contains(nameDienTichSanNoi))
                    //    crRowMain[dicMain[col_RuiRo]].Formula = $"IF(TBT_DienTichSanNoi>0;{dicMain[col_KhoiLuong]}{crRowMain.Index + 1}/TBT_DienTichSanNoi;\"\")";

                }
            }

            if (dfnGiaGianTiep.Range.Count() > 2)
            {
                wsMain.Rows[dfnGiaGianTiep.Range.TopRowIndex][dicMain[col_GiaTri]].Formula
                    = $"SUM({dicMain[col_GiaTri]}{dfnGiaGianTiep.Range.TopRowIndex + 2}:{dicMain[col_GiaTri]}{dfnGiaGianTiep.Range.BottomRowIndex})";
            }
            else
                wsMain.Rows[dfnGiaGianTiep.Range.TopRowIndex][dicMain[col_KhoiLuong]].SetValue(0);


            wb.EndUpdate();
        }


        private void Handle_CapNhatChiPhiTrucTiep(object obj, EventArgs args)
        {
            var wb = spSheet_PATC.Document;
            var wsMain = wb.Worksheets[sheetName_PAKT];
            var wsCPDA = wb.Worksheets[sheetName_GiaTrucTiep];

            var dicMain = MyFunction.fcn_getDicOfColumn(wsMain.GetUsedRange());
            var dicCPDA = MyFunction.fcn_getDicOfColumn(wsCPDA.GetUsedRange());

            var dfnGiaGianTiep = wsMain.DefinedNames.Single(x => x.Name == nameFixGiaTrucTiep);

            var range = dfnGiaGianTiep.Range;
            if (dfnGiaGianTiep.Range.RowCount > 2)
            {
                wsMain.Rows.Remove(range.TopRowIndex + 1, dfnGiaGianTiep.Range.RowCount - 2);
            }

            var rangeDataCP = wsCPDA.Range[nameData];

            wb.BeginUpdate();

            for (int i = rangeDataCP.TopRowIndex; i <= rangeDataCP.BottomRowIndex; i++)
            {
                Row crRowCPDA = wsCPDA.Rows[i];
                string sttString = crRowCPDA[dicCPDA[col_STT]].Value.ToString();

                bool isParse = int.TryParse(sttString, out int stt);
                if (isParse && stt > 0)
                {
                    wsMain.Rows.Insert(dfnGiaGianTiep.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                    var crRowMain = wsMain.Rows[dfnGiaGianTiep.Range.BottomRowIndex - 1];
                    crRowMain.Font.Bold = false;
                    crRowMain[dicMain[col_Ten]].Formula = $"'{sheetName_GiaTrucTiep}'!{dicCPDA[col_NoiDung]}{i + 1}";
                    crRowMain[dicMain[col_KhoiLuong]].Formula = $"'{sheetName_GiaTrucTiep}'!{dicCPDA[col_TTNopThau]}{i + 1}";

                    if (wsMain.DefinedNames.Contains(nameDienTichSanNoi))
                        crRowMain[dicMain[col_RuiRo]].Formula = $"IF(TBT_DienTichSanNoi>0;{dicMain[col_KhoiLuong]}{crRowMain.Index + 1}/TBT_DienTichSanNoi;\"\")";
                
                }
            }

            if (dfnGiaGianTiep.Range.Count() > 2)
            {
                wsMain.Rows[dfnGiaGianTiep.Range.BottomRowIndex][dicMain[col_KhoiLuong]].Formula 
                    = $"SUM({dicMain[col_KhoiLuong]}{dfnGiaGianTiep.Range.TopRowIndex + 2}:{dicMain[col_KhoiLuong]}{dfnGiaGianTiep.Range.BottomRowIndex})";
            }
            else
                wsMain.Rows[dfnGiaGianTiep.Range.BottomRowIndex][dicMain[col_KhoiLuong]].SetValue(0);


            wb.EndUpdate();
        }
        
        private void Handle_CapNhatChiPhiCongTruong(object obj, EventArgs args)
        {
            var wb = spSheet_PATC.Document;
            var wsMain = wb.Worksheets[sheetName_PAKT];
            var wsCPDA = wb.Worksheets[sheetName_TongHopCPDA];

            var dicMain = MyFunction.fcn_getDicOfColumn(wsMain.GetUsedRange());
            var dicCPDA = MyFunction.fcn_getDicOfColumn(wsCPDA.GetUsedRange());

            var dfnChiPhiCongTruongMain = wsMain.DefinedNames.Single(x => x.Name == nameFixChiPhiCongTruong);

            var range = dfnChiPhiCongTruongMain.Range;
            if (dfnChiPhiCongTruongMain.Range.RowCount > 2)
            {
                wsMain.Rows.Remove(range.TopRowIndex + 1, dfnChiPhiCongTruongMain.Range.RowCount - 2);
            }

            //var rangeDataCP = wsCPDA.Range[nameData];

            var rangeChiPhiCongTruongSource = wsCPDA.Range[nameChiPhiCongTruong];
            wb.BeginUpdate();

            for (int i = rangeChiPhiCongTruongSource.TopRowIndex + 1; i <= rangeChiPhiCongTruongSource.BottomRowIndex; i++)
            {
                Row crRowCPDA = wsCPDA.Rows[i];
                string sttString = crRowCPDA[dicCPDA[col_STT]].Value.ToString();

                string regexCon = @"(\d+).(\d+)";
                Match m = Regex.Match(sttString, regexCon, RegexOptions.IgnoreCase);

                if (m != Match.Empty)
                {
                    wsMain.Rows.Insert(dfnChiPhiCongTruongMain.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                    var crRowMain = wsMain.Rows[dfnChiPhiCongTruongMain.Range.BottomRowIndex - 1];
                    crRowMain.Font.Bold = false;
                    crRowMain[dicMain[col_Ten]].Formula = $"'{sheetName_TongHopCPDA}'!{dicCPDA[col_NoiDung]}{i + 1}";
                    crRowMain[dicMain[col_KhoiLuong]].Formula = $"'{sheetName_TongHopCPDA}'!{dicCPDA[col_GiaTri]}{i + 1}";

                    //if (wsMain.DefinedNames.Contains(nameDienTichSanNoi))
                    crRowMain[dicMain[col_RuiRo]].SetValue("Từ bảng chi tiết");
                
                }
            }

            if (dfnChiPhiCongTruongMain.Range.Count() > 2)
            {
                wsMain.Rows[dfnChiPhiCongTruongMain.Range.BottomRowIndex][dicMain[col_KhoiLuong]].Formula
                    = $"SUM({dicMain[col_KhoiLuong]}{dfnChiPhiCongTruongMain.Range.TopRowIndex + 2}:{dicMain[col_KhoiLuong]}{dfnChiPhiCongTruongMain.Range.BottomRowIndex})";
            }
            else
                wsMain.Rows[dfnChiPhiCongTruongMain.Range.BottomRowIndex][dicMain[col_KhoiLuong]].SetValue(0);

            wb.EndUpdate();
        }
        
        private void Handle_CapNhatChiPhiTrucTiepTuDong(object obj, EventArgs args)
        {
            string codeDuAn = SharedControls.slke_ThongTinDuAn.EditValue.ToString();

            string dbString = $"SELECT gct.DienGiai AS TenGop, dmct.DonVi, dmct.TenCongTac, cttk.KhoiLuongToanBo AS KhoiLuongKeHoach,\r\n" +
                $"cttk.DonGia AS DonGiaCongTac, cttk.Code AS CodeCongTac,TOTAL(hp.DonGia*hp.HeSoNguoiDung*hp.DinhMucNguoiDung) AS DonGiaHaoPhi, hp.LoaiVatTu\r\n" +
                $"FROM {TDKH.TBL_TenGopCongTac} gct\r\n" +
                $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                $"ON gct.Code = dmct.CodeGop\r\n" +
                $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                $"ON dmct.Code = cttk.CodeCongTac\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                $"ON dmct.CodeHangMuc = hm.Code\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                $"ON hm.CodeCongTrinh = ctr.Code\r\n" +
                $"LEFT JOIN {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                $"ON cttk.Code = hp.CodeCongTac\r\n" +
                $"WHERE ctr.CodeDuAn = '{codeDuAn}' AND cttk.CodeNhaThau IS NOT NULL\r\n" +
                $"GROUP BY cttk.Code, hp.LoaiVatTu\r\n";

            var cts = DataProvider.InstanceTHDA
                .ExecuteQueryModel<CongTacPATC>(dbString);

            var grs = cts.GroupBy(x => x.TenGop);

            var wb = spSheet_PATC.Document;
            var ws = wb.Worksheets[sheetName_GiaTrucTiep];

            var dfn = ws.DefinedNames.Single(x => x.Name == nameGiaTrucTiep_TuDong);

            //var rangeTuDong = ws.Range[nameGiaTrucTiep_TuDong];

            if (dfn.Range.RowCount > 2) 
            {
                ws.Rows.Remove(dfn.Range.TopRowIndex + 1, dfn.Range.RowCount - 2);
            }

            if (!cts.Any())
                return;

            wb.BeginUpdate();
            //ws.Rows.Insert(dfn.Range.BottomRowIndex, grs.Count() + cts.Count, RowFormatMode.FormatAsNext);

            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            int crInd = dfn.Range.TopRowIndex;
            int STT = 0;

            string[] colsSum = new string[]
            {
                col_TTVL,
                col_TTNC,
                col_TTMTC,
                col_SumTT,
                col_TTNopThau
            };


            foreach (var col in colsSum)
            {

                ws.Rows[dfn.Range.TopRowIndex][dic[col]].ClearContents();// Formula += $"+ {dic[col]}{crRowCha.Index + 1}";
            }
            foreach (var gr in grs)
            {
                ws.Rows.Insert(dfn.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);

                var fst = gr.First();
                
                var crRowCha = ws.Rows[++crInd];
                crRowCha[dic[col_STT]].SetValue(++STT);
                crRowCha[dic[col_NoiDung]].SetValue(fst.TenGop);
                crRowCha.Font.Bold = true;
                int STTCon = 0;

                var grsCTac = gr.GroupBy(x => x.CodeCongTac);
                foreach (var grCTac in grsCTac)
                {
                    ws.Rows.Insert(dfn.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);

                    var ct = grCTac.First();
                    var crRowCon = ws.Rows[++crInd];
                    crRowCon.Font.Italic = true;
                    crRowCon[dic[col_STT]].SetValue($"{STT}.{++STTCon}");
                    crRowCon[dic[col_NoiDung]].SetValue(ct.TenCongTac);
                    crRowCon[dic[col_DonVi]].SetValue(ct.DonVi);
                    crRowCon[dic[col_KhoiLuong]].SetValue(ct.KhoiLuongKeHoach);
                    crRowCon[dic[col_DGNopThau]].SetValue(ct.DonGiaCongTac);

                    var DGVL = grCTac.Where(x => x.LoaiVatTu == "Vật liệu").Sum(x => x.DonGiaHaoPhi);
                    var DGNC = grCTac.Where(x => x.LoaiVatTu == "Nhân công").Sum(x => x.DonGiaHaoPhi);
                    var DGMTC = grCTac.Where(x => x.LoaiVatTu == "Máy thi công").Sum(x => x.DonGiaHaoPhi);

                    crRowCon[dic[col_GVL]].SetValue(DGVL);
                    crRowCon[dic[col_GNC]].SetValue(DGNC);
                    crRowCon[dic[col_GMTC]].SetValue(DGMTC);

                    crRowCon[dic[col_TTVL]].Formula =
                        $"{dic[col_KhoiLuong]}{crInd + 1} * {dic[col_GVL]}{crInd + 1}";           
                    
                    crRowCon[dic[col_TTNC]].Formula =
                        $"{dic[col_KhoiLuong]}{crInd + 1} * {dic[col_GNC]}{crInd + 1}";

                    crRowCon[dic[col_TTMTC]].Formula =
                        $"{dic[col_KhoiLuong]}{crInd + 1} * {dic[col_GMTC]}{crInd + 1}";

                    crRowCon[dic[col_SumTT]].Formula =
                        $"{dic[col_TTVL]}{crInd + 1} + {dic[col_TTNC]}{crInd + 1} + {dic[col_TTMTC]}{crInd + 1}";

                    crRowCon[dic[col_TTNopThau]].Formula =
                        $"{dic[col_KhoiLuong]}{crInd + 1} * {dic[col_DGNopThau]}{crInd + 1}";
                }


                foreach (var col in colsSum)
                {
                    crRowCha[dic[col]].Formula =
                        $"SUM({dic[col]}{crRowCha.Index + 2}:{dic[col]}{crInd + 1})";

                    ws.Rows[dfn.Range.TopRowIndex][dic[col]].Formula += $"+ {dic[col]}{crRowCha.Index + 1}";
                }    

            }
            wb.EndUpdate();
        }

        private void Handle_ThemCongTacThuCong(object obj, EventArgs args)
        {
            var form = new Form_LayDauViecTuCSDL(MyConstant.CONST_TYPE_LAYDAUVIEC_OnlyName, null);
            form.m_truyenData = new Form_LayDauViecTuCSDL.DE_TRUYENDATABANGCONGTAC(btn_NhanDataTuDialogForm);
            form.ShowDialog();
        }

        private void Handle_LayMayThiCong(object obj, EventArgs args)
        {
            var form = new XtraForm_LayMayThiCong();
            form.SendMTC = new XtraForm_LayMayThiCong.DE_SendMTC(UpdateMayThiCong);
            form.ShowDialog();
        }

        private void UpdateMayThiCong(List<MayThiCongWithMayQuyDoi> mtcs)
        {
            var wb = spSheet_PATC.Document;
            var ws = wb.Worksheets[sheetName_ThietBiThiCong];

            var dfn = ws.DefinedNames.Single(x => x.Name == nameData);

            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            var cell = ws.SelectedCell.First();
            int STT = 0;
            for (int i = 0; i >= dfn.Range.TopRowIndex + 1; i--)
            {
                var crRow = ws.Rows[i];
                var sttStr = crRow[dic[col_STT]].Value.ToString();

                if (int.TryParse(sttStr, out STT))
                {
                    break;
                }
            }

            int crInd = dfn.Range.BottomRowIndex - 1;
            string[] colsSumCon = new string[]
            {
                col_KhoiLuongMua,
                col_KhoiLuongThue,
                col_SoCa,
                col_ThoiGianThue,
                col_CaMayDuToan,
                col_ThanhTienDuToan
            };
            
            string[] colsSumCha = new string[]
            {
                col_KhoiLuongMua,
                col_KhoiLuongThue,
                col_SoCa,
                col_ThoiGianThue,
                col_CaMayDuToan,
                col_ThanhTienDuToan,
                col_ThanhTienMua, 
                col_ThanhTienThue
            };
            wb.BeginUpdate();
            foreach (var col in colsSumCha)
            {
                ws.Rows[dfn.Range.TopRowIndex][dic[col]].Clear();
            }
            foreach (var mqd in mtcs.Where(x => x.ParentCode is null))
            {

                ws.Rows.Insert(dfn.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                var crRowCha = ws.Rows[++crInd];
                crRowCha.Font.Bold = true;

                crRowCha[dic[col_STT]].SetValue(++STT);
                crRowCha[dic[col_NoiDung]].SetValue(mqd.VatTu);
                crRowCha[dic[col_DonVi]].SetValue(mqd.DonVi);

                crRowCha[dic[col_DonGiaMua]].SetValue(mqd.GiaMuaMay);
                crRowCha[dic[col_DonGiaThue]].SetValue(mqd.GiaCaMay);
                crRowCha[dic[col_HaoPhi]].SetValue(mqd.HaoPhi);
                crRowCha[dic[col_Code]].SetValue(mqd.Code);
                crRowCha[dic[col_HeSo]].SetValue(1);

                crRowCha[dic[col_ThanhTienMua]].Formula
                    += $"{dic[col_KhoiLuongMua]}{crInd + 1}*{dic[col_DonGiaMua]}{crInd + 1}*{dic[col_TyLeKhauHao]}{crInd + 1}";
                
                crRowCha[dic[col_ThanhTienThue]].Formula
                    += $"{dic[col_KhoiLuongThue]}{crInd + 1}*{dic[col_DonGiaThue]}{crInd + 1}*{dic[col_ThoiGianThue]}{crInd + 1}";

                crRowCha[dic[col_ThanhTienNhienLieu]].Formula
                    += $"{dic[col_HaoPhi]}{crInd + 1}*{dic[col_SoCa]}{crInd + 1}*{dic[col_HeSo]}{crInd + 1}";



                var mays = mtcs.Where(x => x.ParentCode == mqd.Code);
                int STTCon = 0;

                foreach (var may in mays)
                {

                    ws.Rows.Insert(dfn.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);

                    var crRowCon = ws.Rows[++crInd];
                    crRowCon.Font.Italic = true;
                    crRowCon[dic[col_STT]].SetValue($"{STT}.{++STTCon}");
                    crRowCon[dic[col_Code]].SetValue(may.Code);
                    crRowCon[dic[col_NoiDung]].SetValue(may.VatTu);
                    crRowCon[dic[col_DonVi]].SetValue(may.DonVi);
                    crRowCon[dic[col_KhoiLuongThue]].SetValue(may.KhoiLuongKeHoach);
                    crRowCon[dic[col_SoCa]].SetValue(may.KhoiLuongKeHoach);
                    crRowCon[dic[col_CaMayDuToan]].SetValue(may.KhoiLuongKeHoach);
                    crRowCon[dic[col_GiaMayDuToan]].SetValue(may.DonGia);

                    crRowCon[dic[col_ThanhTienDuToan]].Formula
                        += $"{dic[col_CaMayDuToan]}{crInd + 1}*{dic[col_GiaMayDuToan]}{crInd + 1}";
                }

                foreach (var col in colsSumCon)
                {
                    crRowCha[dic[col]].Formula =
                        $"SUM({dic[col]}{crRowCha.Index + 2}:{dic[col]}{crInd + 1})";

                }

                foreach (var col in colsSumCha)
                {
                    ws.Rows[dfn.Range.TopRowIndex][dic[col]].Formula += $"+ {dic[col]}{crRowCha.Index + 1}";
                }

                //ws.Rows[dfn.Range.TopRowIndex][dic[col_ThanhTienMua]].Formula += $"+ {dic[col]}{crRowCha.Index + 1}";

            }
            wb.EndUpdate();


        }

        private void btn_NhanDataTuDialogForm(LayCongTac[] dt, int type, bool isCopyNhom = true)
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu");
            

                string lsCodeCongTacTheoKy = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x.Code).ToArray());

            string dbString = $"SELECT hm.Ten AS TenGop, dmct.DonVi, dmct.TenCongTac, cttk.KhoiLuongToanBo AS KhoiLuongKeHoach,\r\n" +
                            $"cttk.DonGia AS DonGiaCongTac, cttk.Code AS CodeCongTac, TOTAL(hp.DonGia) AS DonGiaHaoPhi, hp.LoaiVatTu\r\n" +
                            $"FROM {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                            $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                            $"ON dmct.Code = cttk.CodeCongTac\r\n" +
                            $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                            $"ON dmct.CodeHangMuc = hm.Code\r\n" +
                            //$"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                            //$"ON hm.CodeCongTrinh = ctr.Code\r\n" +
                            $"LEFT JOIN {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                            $"ON cttk.Code = hp.CodeCongTac\r\n" +
                            $"WHERE cttk.Code IN ({lsCodeCongTacTheoKy})" +
                            $"GROUP BY cttk.Code, hp.LoaiVatTu\r\n";

            var cts = DataProvider.InstanceTHDA
                .ExecuteQueryModel<CongTacPATC>(dbString);

            var grs = cts.GroupBy(x => x.TenGop);

            var wb = spSheet_PATC.Document;
            var ws = wb.Worksheets[sheetName_GiaTrucTiep];

            var dfn = ws.DefinedNames.Single(x => x.Name == nameGiaTrucTiep_ThuCong);

            //var rangeTuDong = ws.Range[nameGiaTrucTiep_TuDong];

            //if (dfn.Range.RowCount > 2)
            //{
            //    ws.Rows.Remove(dfn.Range.TopRowIndex + 1, dfn.Range.RowCount - 2);
            //}

            if (!cts.Any())
                return;
            wb.BeginUpdate();
            //ws.Rows.Insert(dfn.Range.BottomRowIndex, grs.Count() + cts.Count, RowFormatMode.FormatAsNext);

            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            int crInd = dfn.Range.TopRowIndex;
            int STT = 0;

            string[] colsSum = new string[]
            {
                col_TTVL,
                col_TTNC,
                col_TTMTC,
                col_SumTT,
                col_TTNopThau
            };

            foreach (var gr in grs)
            {
                ws.Rows.Insert(dfn.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);

                var fst = gr.First();

                var crRowCha = ws.Rows[++crInd];
                crRowCha[dic[col_STT]].SetValue(++STT);
                crRowCha[dic[col_NoiDung]].SetValue(fst.TenGop);
                crRowCha.Font.Bold = true;
                int STTCon = 0;

                var grsCTac = gr.GroupBy(x => x.CodeCongTac);
                foreach (var grCTac in grsCTac)
                {
                    ws.Rows.Insert(dfn.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                    var ct = grCTac.First();
                    var crRowCon = ws.Rows[++crInd];
                    crRowCon.Font.Italic = true;
                    crRowCon[dic[col_STT]].SetValue($"{STT}.{++STTCon}");
                    crRowCon[dic[col_NoiDung]].SetValue(ct.TenCongTac);
                    crRowCon[dic[col_DonVi]].SetValue(ct.DonVi);
                    crRowCon[dic[col_KhoiLuong]].SetValue(ct.KhoiLuongKeHoach);
                    crRowCon[dic[col_DGNopThau]].SetValue(ct.DonGiaCongTac);

                    var DGVL = grCTac.Where(x => x.LoaiVatTu == "Vật liệu").Sum(x => x.DonGiaHaoPhi);
                    var DGNC = grCTac.Where(x => x.LoaiVatTu == "Nhân công").Sum(x => x.DonGiaHaoPhi);
                    var DGMTC = grCTac.Where(x => x.LoaiVatTu == "Máy thi công").Sum(x => x.DonGiaHaoPhi);

                    crRowCon[dic[col_GVL]].SetValue(DGVL);
                    crRowCon[dic[col_GNC]].SetValue(DGNC);
                    crRowCon[dic[col_GMTC]].SetValue(DGMTC);

                    crRowCon[dic[col_TTVL]].Formula =
                        $"{dic[col_KhoiLuong]}{crInd + 1} * {dic[col_GVL]}{crInd + 1}";

                    crRowCon[dic[col_TTNC]].Formula =
                        $"{dic[col_KhoiLuong]}{crInd + 1} * {dic[col_GNC]}{crInd + 1}";

                    crRowCon[dic[col_TTMTC]].Formula =
                        $"{dic[col_KhoiLuong]}{crInd + 1} * {dic[col_GMTC]}{crInd + 1}";

                    crRowCon[dic[col_SumTT]].Formula =
                        $"{dic[col_TTVL]}{crInd + 1} + {dic[col_TTNC]}{crInd + 1} + {dic[col_TTMTC]}{crInd + 1}";

                    crRowCon[dic[col_TTNopThau]].Formula =
                        $"{dic[col_KhoiLuong]}{crInd + 1} * {dic[col_DGNopThau]}{crInd + 1}";
                }


                foreach (var col in colsSum)
                {
                    crRowCha[dic[col]].Formula =
                        $"SUM({dic[col]}{crRowCha.Index + 2}:{dic[col]}{crInd + 1})";

                    ws.Rows[dfn.Range.TopRowIndex][dic[col]].Formula += $"+ {dic[col]}{crRowCha.Index + 1}";
                }

            }

            wb.EndUpdate();
            WaitFormHelper.CloseWaitForm();

        }

        private void bt_XuatBaoCao_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void bt_reset_Click(object sender, EventArgs e)
        {
            LoadData(true);
        }
    }
}