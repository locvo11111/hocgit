using DevExpress.Spreadsheet;
using DevExpress.Utils.Menu;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraSpreadsheet.Commands;
using DevExpress.XtraSpreadsheet.Menu;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Controls.MTC;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.ChamCong;
using PhanMemQuanLyThiCong.Model.HopDong;
using PhanMemQuanLyThiCong.Model.MayThiCong;
using PM360.Common.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Uc_MTCHangNgay : DevExpress.XtraEditors.XtraUserControl
    {
        private List<MTC_DanhSachMay> lstMayInDuAns;
        Uc_TenQuyDoiMay m_ucTenQuyDoi = new Uc_TenQuyDoiMay();
        private bool _TheoTenMay { get { return ce_LayTheoMay.Checked; } set { _TheoTenMay = value; } } 
        private DateTime _Date { get { return date_NhapLieu.DateTime; }set { _Date = value; } }
        public Uc_MTCHangNgay()
        {
            InitializeComponent();
            //m_ucTenMay.de_TranDanhSachMay = new Uc_TenMayThiCongNhatTrinh.DE_TransDanhSachMay(fcn_DE_DanhSachNV);
            //m_ucTenMay.Dock = DockStyle.None;
            //this.Controls.Add(m_ucTenMay);
            //m_ucTenMay.Hide();

            m_ucTenQuyDoi.de_TranDanhSachDVPH = new Uc_TenQuyDoiMay.DE_TransDonViPH(fcn_DE_DanhSachQuyDoi);
            m_ucTenQuyDoi.Dock = DockStyle.None;
            this.Controls.Add(m_ucTenQuyDoi);
            m_ucTenQuyDoi.Hide();
            InitDataControl();
        }
        private void fcn_DE_DanhSachQuyDoi(List<MTC_ChiTietDinhMuc> DanhSachQuyDoi, string CodeCV)
        {
            if (DanhSachQuyDoi is null|| !DanhSachQuyDoi.Any())
                return;
            MTC_ChiTietDinhMuc item = DanhSachQuyDoi.SingleOrDefault();
            if (item is null)
                return;
            IWorkbook workbook = spSheet_NhatTrinhThiCong.Document;
            Worksheet sheet = workbook.Worksheets.ActiveWorksheet;
            CellRange RangeData = sheet.Range["Data"];
            CellRange cell = sheet.SelectedCell;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            string db_string = $"UPDATE  {MyConstant.TBL_MTC_CHITIETNHATTRINH} SET" +
            $" \"CodeDinhMuc\"='{item.Code}' WHERE \"Code\"='{CodeCV}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            sheet.Rows[cell.TopRowIndex][Name["DinhMucMacDinh"]].SetValueFromText(item.DinhMucCongViec);
            sheet.Rows[cell.TopRowIndex][Name["CodeDinhMuc"]].SetValueFromText(item.Code);
            sheet.Rows[cell.TopRowIndex][Name["MucTieuThu"]].SetValue(item.MucTieuThu);
        }    
        private void fcn_DE_DanhSachNV(List<Tbl_MTC_DanhSachMayViewModel> DanhSachMay)
        {
//            WaitFormHelper.ShowWaitForm("Đang thêm Máy vào công tác");
//            foreach (var item in DanhSachMay)
//            {
//                string dbString = $"INSERT INTO {MyConstant.TBL_MTC_CHITIETNHATTRINH} " +
//$"(Code,CodeMay,NgayThang,{m_ucTenMay._ColCode}) VALUES " +
//$"('{Guid.NewGuid()}','{item.Code}','{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'" +
//$",'{m_ucTenMay._Code}')";
//                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
//            }
//            WaitFormHelper.CloseWaitForm();
//            Fcn_LoadDataNhatTrinh();
        }
        private void InitDataControl()
        {
            InitDataSoure();
            date_NhapLieu.EditValueChanged -= date_NhapLieu_EditValueChanged;
            date_NhapLieu.Properties.MaxValue = DateTime.Now;
            date_NhapLieu.EditValue = DateTime.Now;
            date_NhapLieu.EditValueChanged += date_NhapLieu_EditValueChanged;
        }

        private void InitDataSoure()
        {
            string dbString = $"SELECT * FROM {MyConstant.VIEW_DANHSACHMAYDUAN} WHERE CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue?.ToString()}'";
            lstMayInDuAns = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_DanhSachMay>(dbString);
        }

        public void LoadData()
        {
            Fcn_LoadDataNhatTrinh();
            //TDKHHelper.LoadCongTacNhatTrinhThiCong(spSheet_NhatTrinhThiCong, CusSlke_DVTH, date_NhapLieu, cbb_LayCongTacNgoaiKeHoach.Checked, cb_ShowCongTacExitMay.Checked, lstMayInDuAns);
        }
        private void fcn_updateluyke(int luykedau, int luykenhot, int luykexang)
        {
            fcn_Tinhtoanluyke(luykedau, luykenhot, luykexang, false);
        }

        private void fcn_Tinhtoanluyke(int luykedau, int luykenhot, int luykexang, bool method)
        {
        }

        private void date_NhapLieu_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void spSheet_NhatTrinhThiCong_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {
            Worksheet sheet = e.Worksheet;
            CellRange titleHeader = sheet.Range[MyConstant.TITLE_HEADER];
            //CellRange body = sheet.Range["Data"];
            Dictionary<string, int> lstTitles = ExcelHelper.GetListColumnByRange(titleHeader);
            string columnName = lstTitles.Where(x => x.Value == e.ColumnIndex)?.First().Key;
            int typeRow = sheet.Rows[e.RowIndex][lstTitles[MyConstant.TYPEROW]].Value.IsEmpty ? 2 : (int)sheet.Rows[e.RowIndex][lstTitles[MyConstant.TYPEROW]].Value.NumericValue;
            string code = sheet.Rows[e.RowIndex][lstTitles[MyConstant.CODE]].Value.TextValue;
            string codeCongTacThuCong = sheet.Rows[e.RowIndex][lstTitles[MyConstant.CODECONGTACTHUCONG]].Value.TextValue;
            string[] LstMerge = {TDKH.COL_MaHieuCongTac, TDKH.COL_DanhMucCongTac, TDKH.COL_DonVi, "TenHangMuc" };
            if (_TheoTenMay)
            {
                if (string.IsNullOrEmpty(codeCongTacThuCong))
                {
                    if (columnName != "TenCongTac")
                    {
                        MessageShower.ShowError("Vui lòng nhập tên công tác trước khi nhập thông tin các cột khác!!!!!");
                        spSheet_NhatTrinhThiCong.CloseCellEditor(CellEditorEnterValueMode.Cancel);
                        sheet.Rows[e.RowIndex][e.ColumnIndex].SetValue(string.Empty);
                        return;
                    }
                    DonViThucHien DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;
                    codeCongTacThuCong = Guid.NewGuid().ToString();
                    string dbString = $"INSERT INTO {MyConstant.TBL_MTC_NHATTRINHCONGTAC} " +
$"(Code,TenCongTac,CodeGiaiDoan,{DVTH.ColCodeFK},TypeRow) VALUES " +
$"('{codeCongTacThuCong}','{e.Value}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','{DVTH.Code}','{2}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                    dbString = $"INSERT INTO {MyConstant.TBL_MTC_CHITIETNHATTRINH} " +
$"(Code,NgayThang,CodeCongTacThuCong,CodeMayDuAn) VALUES " +
$"('{Guid.NewGuid()}','{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{codeCongTacThuCong}'," +
$"'{sheet.Rows[e.RowIndex][lstTitles["CodeMay"]].Value.TextValue}'" +
$")";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    LoadData();
                }
                else
                {
                    string updatestt = $"UPDATE  {MyConstant.TBL_MTC_NHATTRINHCONGTAC} SET {columnName}='{e.Value}' WHERE \"Code\"='{codeCongTacThuCong}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                }

            }
            else
            {
                if (typeRow == 2 && LstMerge.Contains(columnName))
                {
                    if (!cbb_LayCongTacNgoaiKeHoach.Checked)
                    {
                        MessageShower.ShowWarning("Nhập thủ công chỉ áp dụng cho công tác ngoài kế hoạch, Vui lòng chọn Công tác ngoài kế hoạch!!!");
                        spSheet_NhatTrinhThiCong.CloseCellEditor(CellEditorEnterValueMode.Cancel);
                        sheet.Rows[e.RowIndex][e.ColumnIndex].SetValue("");
                        return;
                    }
                    if (string.IsNullOrEmpty(codeCongTacThuCong))
                    {
                        if (columnName != "TenCongTac")
                        {
                            MessageShower.ShowError("Vui lòng nhập tên công tác trước khi nhập thông tin các cột khác!!!!!");
                            spSheet_NhatTrinhThiCong.CloseCellEditor(CellEditorEnterValueMode.Cancel);
                            sheet.Rows[e.RowIndex][e.ColumnIndex].SetValue("");
                            return;
                        }
                        DonViThucHien DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;
                        codeCongTacThuCong = Guid.NewGuid().ToString();
                        string dbString = $"INSERT INTO {MyConstant.TBL_MTC_NHATTRINHCONGTAC} " +
    $"(Code,TenCongTac,CodeGiaiDoan,{DVTH.ColCodeFK},TypeRow) VALUES " +
    $"('{codeCongTacThuCong}','{e.Value}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','{DVTH.Code}','{2}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        LoadData();
                    }
                    else
                    {
                        string updatestt = $"UPDATE  {MyConstant.TBL_MTC_NHATTRINHCONGTAC} SET {columnName}='{e.Value}' WHERE \"Code\"='{codeCongTacThuCong}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                    }
                }
            }

        }

        private MTC_ChiTietHangNgay MapRow(Worksheet sheet, Dictionary<string, int> lstTitles, int rowIndex)
        {
            var itemIndex = new MTC_ChiTietHangNgay();
            foreach (var item in lstTitles)
            {
                try
                {
                    Cell cell = sheet.Rows[rowIndex][item.Value];
                    PropertyInfo propertyInfo = typeof(MTC_ChiTietHangNgay).GetProperty(item.Key);
                    if (propertyInfo != null)
                    {
                        switch (Type.GetTypeCode(propertyInfo.PropertyType))
                        {
                            case TypeCode.Boolean:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsBoolean ? cell.Value.BooleanValue : false);
                                break;

                            case TypeCode.Decimal:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (decimal?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.Double:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (double?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.Int16:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (short?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.Int32:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (int?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.Int64:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (long?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.String:
                                propertyInfo.SetValue(itemIndex, cell.DisplayText);
                                break;

                            case TypeCode.DateTime:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsDateTime ? (DateTime?)cell.Value.DateTimeValue : null);
                                break;
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            if (string.IsNullOrEmpty(itemIndex.CodeCongTacTheoGiaiDoan)) itemIndex.CodeCongTacTheoGiaiDoan = null;
            if (string.IsNullOrEmpty(itemIndex.CodeCongTacThuCong)) itemIndex.CodeCongTacThuCong = null;
            if(string.IsNullOrEmpty(itemIndex.NgayThang)) itemIndex.NgayThang = DateTime.Parse(date_NhapLieu.EditValue.ToString()).ToString("yyyy-MM-dd");
            return itemIndex;
        }

        private void ActivateEditor()
        {
            Worksheet sheet = spSheet_NhatTrinhThiCong.ActiveWorksheet;
            if (sheet.Name == "Nhập hàng ngày MTC")
            {
                IList<CustomCellInplaceEditor> editors = sheet.CustomCellInplaceEditors.GetCustomCellInplaceEditors(sheet.Selection);
                if (editors.Count == 1)
                    spSheet_NhatTrinhThiCong.OpenCellEditor(DevExpress.XtraSpreadsheet.CellEditorMode.Edit);
            }
        }

        private void spSheet_NhatTrinhThiCong_SelectionChanged(object sender, EventArgs e)
        {
            ActivateEditor();
        }
        private void fcn_Handle_XoaMay(object sender, EventArgs args)
        {
            IWorkbook wb = spSheet_NhatTrinhThiCong.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Cell = ws.SelectedCell;
            string CodeMay = ws.Rows[Cell.TopRowIndex][Name["CodeMay"]].Value.TextValue;
            if (CodeMay is null)
            {
                MessageShower.ShowWarning("Vui lòng Chọn đúng dòng có máy để xóa!!!!!!");
                return;
            }
            string Code = ws.Rows[Cell.TopRowIndex][Name["Code"]].Value.TextValue;
            string db_string = $"DELETE FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} WHERE \"Code\"='{Code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            Fcn_LoadDataNhatTrinh();
        }    
        private void fcn_Handle_ThemNV(object sender, EventArgs args)
        {
            IWorkbook wb = spSheet_NhatTrinhThiCong.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Cell = ws.SelectedCell;
            double TypeRow = ws.Rows[Cell.TopRowIndex][Name[TDKH.COL_TypeRow]].Value.NumericValue;
            if (TypeRow == 0)
            {
                MessageShower.ShowWarning("Vui lòng chọn ô có tên Công tác");
                return;
            }
            string ColCode = ws.Rows[Cell.TopRowIndex][Name[TDKH.COL_TypeRow]].Value.NumericValue == 1 ? "CodeCongTacTheoGiaiDoan" : "CodeCongTacThuCong";
            string Code = ws.Rows[Cell.TopRowIndex][Name[ColCode]].Value.TextValue;
            string CodeChiTiet = ws.Rows[Cell.TopRowIndex][Name["Code"]].Value.TextValue;
            string CodeDinhMuc = ws.Rows[Cell.TopRowIndex][Name["CodeDinhMuc"]].Value.TextValue;
            XtraForm_ThemDinhMucMay MTC = new XtraForm_ThemDinhMucMay(Code,ColCode, date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
            MTC.de_TranDanhSachDM =new XtraForm_ThemDinhMucMay.DE_TransDanhSachDinhMuc(Fcn_UpdateDataMTC);
            MTC.ShowDialog();
        }    
        private void Fcn_UpdateDataMTC(List<MTC_ChiTietDinhMuc> CTDM, string Code, string ColCode)
        {
            WaitFormHelper.ShowWaitForm("Đang thêm Máy vào công tác");
            List<MTC_ChiTietDinhMuc> CTDMParent = CTDM.Where(x => x.CodeMay == "0").ToList();
            string dbString = "";
            foreach (var item in CTDMParent)
            {
                List<MTC_ChiTietDinhMuc> CTDMChild = CTDM.Where(x => x.CodeMay == item.Code).ToList();
                if (CTDMChild.Count() != 0)
                {
                    foreach(var crow in CTDMChild)
                    {
                        dbString = $"INSERT INTO {MyConstant.TBL_MTC_CHITIETNHATTRINH} " +
$"(Code,NgayThang,{ColCode},CodeMayDuAn,CodeDinhMuc) VALUES " +
$"('{Guid.NewGuid()}','{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{Code}','{item.Code}','{crow.Code}'" +
$")";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                }
                else
                {
                    dbString = $"INSERT INTO {MyConstant.TBL_MTC_CHITIETNHATTRINH} " +
$"(Code,NgayThang,{ColCode},CodeMayDuAn) VALUES " +
$"('{Guid.NewGuid()}','{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{Code}','{item.Code}'" +
$")";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
            WaitFormHelper.CloseWaitForm();
            Fcn_LoadDataNhatTrinh();
        }
        private void fcn_Handle_ThemCongTacChoMay(object sender, EventArgs args)
        {
            IWorkbook wb = spSheet_NhatTrinhThiCong.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Cell = ws.SelectedCell;
            string CodeMay = ws.Rows[Cell.TopRowIndex][Name["CodeMay"]].Value.TextValue;
            if (CodeMay is null)
            {
                MessageShower.ShowWarning("Vui lòng Thêm tên máy trước khi thêm nhiên liệu phụ!!!!!");
                return;
            }
            MessageShower.ShowWarning("Công tác được lấy dựa trên tên NHÀ THẦU được lựa chọn mặc định ở trang chính và Công tác được tạo thủ công!!!!!");
            XtraForm_LayCongTacMTC MTC = new XtraForm_LayCongTacMTC();
            MTC.Fcn_UpdateData(CodeMay, date_NhapLieu.DateTime);
            MTC.de_TranDanhSachCT = new XtraForm_LayCongTacMTC.DE_TransDanhSachCongTac(Fcn_UpdataCongTacMay);
            MTC.ShowDialog();
        }
        private void Fcn_UpdataCongTacMay(List<LayCongTacHopDong> DanhSachCT, string CodeMayDuAn)
        {
            if (DanhSachCT.Count == 0 || DanhSachCT is null)
                return;
            WaitFormHelper.ShowWaitForm("Đang cập nhập dữ liệu công tác máy thi công", "Vui Lòng chờ!");
            string dbString = $"SELECT * FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH}";
            DataTable ChiTietMay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            ChiTietMay.Clear();
            foreach (var item in DanhSachCT)
            {
                if (item.MaHieu == MyConstant.CONST_TYPE_HANGMUC || item.MaHieu == MyConstant.CONST_TYPE_CONGTRINH || item.ParentID == "0")
                    continue;
                DataRow Row = ChiTietMay.NewRow();
                Row["Code"] = Guid.NewGuid().ToString();
                Row["CodeMayDuAn"] = CodeMayDuAn;
                Row["NgayThang"] = date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                if (item.ParentID == "2")//thủ công
                {
                    Row["CodeCongTacThuCong"] = item.ID;
                }
                else
                {
                    Row["CodeCongTacTheoGiaiDoan"] = item.ID;
                }
                ChiTietMay.Rows.Add(Row);
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(ChiTietMay, MyConstant.TBL_MTC_CHITIETNHATTRINH);
            WaitFormHelper.CloseWaitForm();
            Fcn_LoadDataNhatTrinh();
        }
        private void fcn_Handle_ThemNhienLieuPhu(object sender, EventArgs args)
        {
            IWorkbook wb = spSheet_NhatTrinhThiCong.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Cell = ws.SelectedCell;
            string CodeMay = ws.Rows[Cell.TopRowIndex][Name["CodeMay"]].Value.TextValue;
            if (CodeMay is null)
            {
                MessageShower.ShowWarning("Vui lòng Thêm tên máy trước khi thêm nhiên liệu phụ!!!!!");
                return;
            }
            string Json= ws.Rows[Cell.TopRowIndex][Name["JSON"]].Value.TextValue;
            string CodeNT= ws.Rows[Cell.TopRowIndex][Name["Code"]].Value.TextValue;
            string TenMay= ws.Rows[Cell.TopRowIndex][Name["TenMayThiCong"]].Value.TextValue;
            XtraForm_ThemNhienLieuPhu NLP = new XtraForm_ThemNhienLieuPhu();
            NLP.de_TransNLP = new XtraForm_ThemNhienLieuPhu.DE_TransNLP(Fcn_UpdataNLP);
            NLP.Fcn_LoadData(Json, CodeMay, TenMay,CodeNT);
            NLP.ShowDialog();
        }     
        private void Fcn_UpdataNLP(string encrypted, string display, string CodeNhatTrinh)
        {
            IWorkbook wb = spSheet_NhatTrinhThiCong.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Cell = ws.SelectedCell;
            ws.Rows[Cell.TopRowIndex][Name["JSON"]].SetValueFromText(encrypted);
            ws.Rows[Cell.TopRowIndex][Name["NhienLieuPhu"]].SetValueFromText(display);
            string db_string = $"UPDATE  {MyConstant.TBL_MTC_CHITIETNHATTRINH} SET" +
            $" \"NhienLieuPhu\"='{encrypted}' WHERE \"Code\"='{CodeNhatTrinh}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            //Fcn_UpdateDaTaTongHopNhienLieuPhu();
            Fcn_LoadThongBao();
        }
        private void fcn_Handle_ThemDong(object sender, EventArgs args)
        {
            Fcn_LoadDataNhatTrinh();
        }
        private void fcn_Handle_ThemTenRutGon(object sender, EventArgs args)
        {
            IWorkbook wb = spSheet_NhatTrinhThiCong.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Cell = ws.SelectedCell;
            string CodeMay = ws.Rows[Cell.TopRowIndex][Name["CodeMay"]].Value.TextValue;
            if (CodeMay is null)
            {
                MessageShower.ShowWarning("Vui lòng Thêm tên máy trước khi chọn tên quy đổi!!!!!");
                return;
            }
            string CodeMayToanDuAn = ws.Rows[Cell.TopRowIndex][Name["CodeMayToanDuAn"]].Value.TextValue;
            string Code = ws.Rows[Cell.TopRowIndex][Name["Code"]].Value.TextValue;
            string CodeDM = ws.Rows[Cell.TopRowIndex][Name["CodeDinhMuc"]].Value.TextValue;
            string dbString = $"SELECT CT.*,DM.DinhMucCongViec FROM Tbl_MTC_ChiTietDinhMuc CT " +
                $"LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUCTHAMKHAO} DM" +
                $" ON DM.Code=CT.CodeDinhMuc " +
                $"LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} May ON May.Code=CT.CodeMay " +
                $"LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} Mayda ON May.Code=Mayda.CodeMay " +
                $"WHERE May.Code='{CodeMayToanDuAn}' AND Mayda.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            List<MTC_ChiTietDinhMuc> lstDinhMucMD = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietDinhMuc>(dbString);
            m_ucTenQuyDoi.Show();
            var spsheetLoc = spSheet_NhatTrinhThiCong.Location;
            Rectangle rec = spSheet_NhatTrinhThiCong.GetCellBounds(Cell.TopRowIndex, Cell.LeftColumnIndex);
            m_ucTenQuyDoi.Location = new Point(rec.Left- m_ucTenQuyDoi.Width + spsheetLoc.X, rec.Top + spsheetLoc.Y);
            m_ucTenQuyDoi.BringToFront();
            m_ucTenQuyDoi.Fcn_UpDateDanhSachDVPH(lstDinhMucMD, Code, CodeDM);
        }
        private void spSheet_NhatTrinhThiCong_PopupMenuShowing(object sender, DevExpress.XtraSpreadsheet.PopupMenuShowingEventArgs e)
        {
            e.Menu.RemoveMenuItem(SpreadsheetCommandId.RemoveSheetRowsContextMenuItem);
            e.Menu.RemoveMenuItem(SpreadsheetCommandId.RemoveSheetRows);
            IWorkbook wb = spSheet_NhatTrinhThiCong.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            CellRange Cell = ws.SelectedCell;
            if (!RangeData.Contains(Cell) || Cell.RowCount > 1)
                return;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            if (_TheoTenMay)
            {
                if (Name["DinhMucMacDinh"] == ws.Columns[Cell.LeftColumnIndex].Heading)
                {
                    SpreadsheetMenuItem itemCapNhatAll = new SpreadsheetMenuItem("Thêm tên rút gọn", fcn_Handle_ThemTenRutGon);
                    itemCapNhatAll.Appearance.ForeColor = Color.Blue;
                    itemCapNhatAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
                    e.Menu.Items.Add(itemCapNhatAll);
                }
                else if (Name["NhienLieuPhu"] == ws.Columns[Cell.LeftColumnIndex].Heading)
                {
                    SpreadsheetMenuItem itemCapNhatAll = new SpreadsheetMenuItem("Thêm nhiên liệu phụ", fcn_Handle_ThemNhienLieuPhu);
                    itemCapNhatAll.Appearance.ForeColor = Color.Blue;
                    itemCapNhatAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
                    e.Menu.Items.Add(itemCapNhatAll);
                }      
                else if (Name["TenMayThiCong"] == ws.Columns[Cell.LeftColumnIndex].Heading)
                {
                    SpreadsheetMenuItem itemCapNhatAll = new SpreadsheetMenuItem("Thêm công tác cho máy", fcn_Handle_ThemCongTacChoMay);
                    itemCapNhatAll.Appearance.ForeColor = Color.Blue;
                    itemCapNhatAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
                    e.Menu.Items.Add(itemCapNhatAll);
                }
            }
            else
            {
                SpreadsheetMenuItem itemXoa = new SpreadsheetMenuItem("Xóa máy thi công", fcn_Handle_XoaMay);
                itemXoa.Appearance.ForeColor = Color.Blue;
                itemXoa.Appearance.Font = new Font(itemXoa.Appearance.Font, FontStyle.Bold);
                e.Menu.Items.Add(itemXoa);
                if (Name[TDKH.COL_DanhMucCongTac] == ws.Columns[Cell.LeftColumnIndex].Heading)
                {
                    SpreadsheetMenuItem itemCapNhatAll = new SpreadsheetMenuItem("Thêm máy thi công", fcn_Handle_ThemNV);
                    itemCapNhatAll.Appearance.ForeColor = Color.Blue;
                    itemCapNhatAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
                    e.Menu.Items.Add(itemCapNhatAll);
                }
                else if (Name["DinhMucMacDinh"] == ws.Columns[Cell.LeftColumnIndex].Heading)
                {
                    SpreadsheetMenuItem itemCapNhatAll = new SpreadsheetMenuItem("Thêm tên rút gọn", fcn_Handle_ThemTenRutGon);
                    itemCapNhatAll.Appearance.ForeColor = Color.Blue;
                    itemCapNhatAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
                    e.Menu.Items.Add(itemCapNhatAll);
                }
                else if (Name["NhienLieuPhu"] == ws.Columns[Cell.LeftColumnIndex].Heading)
                {
                    SpreadsheetMenuItem itemCapNhatAll = new SpreadsheetMenuItem("Thêm nhiên liệu phụ", fcn_Handle_ThemNhienLieuPhu);
                    itemCapNhatAll.Appearance.ForeColor = Color.Blue;
                    itemCapNhatAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
                    e.Menu.Items.Add(itemCapNhatAll);
                }
                SpreadsheetMenuItem itemThemDong = new SpreadsheetMenuItem("Thêm dòng công tác thủ công", fcn_Handle_ThemDong);
                itemThemDong.Appearance.ForeColor = Color.Blue;
                itemThemDong.Appearance.Font = new Font(itemThemDong.Appearance.Font, FontStyle.Bold);
                e.Menu.Items.Add(itemThemDong);
            }
        }

        private void deleteMay_Click(object sender, EventArgs e)
        {
            var selections = spSheet_NhatTrinhThiCong.Selection;
            Worksheet worksheet = spSheet_NhatTrinhThiCong.ActiveWorksheet;
            CellRange titleHeader = worksheet.Range[MyConstant.TITLE_HEADER];
            Dictionary<string, int> lstTitles = ExcelHelper.GetListColumnByRange(titleHeader);
            List<MTC_ChiTietHangNgay> lstRowDeletes = new List<MTC_ChiTietHangNgay>();
            foreach (var cell in selections)
            {
                lstRowDeletes.Add(MapRow(worksheet, lstTitles, cell.RowIndex));
            }
            if(lstRowDeletes.Any(x=>!string.IsNullOrEmpty(x.Code)))
            {
                int res = DataProvider.InstanceTHDA.Execute("DELETEMAYINCONGTAC", new { Code = lstRowDeletes.FindAll(x=>!string.IsNullOrEmpty(x.Code)).Select(x=>x.Code).ToList()}, true, true);
                if(res>0)
                {
                    LoadData();
                }    
            }    
        }

        private void insertCongTac_Click(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            item.Click -= insertCongTac_Click;
            Worksheet worksheet = spSheet_NhatTrinhThiCong.ActiveWorksheet;
            CellRange titleHeader = worksheet.Range[MyConstant.TITLE_HEADER];
            Dictionary<string, int> lstTitles = ExcelHelper.GetListColumnByRange(titleHeader);
            spSheet_NhatTrinhThiCong.BeginUpdate();
            try
            {
                int rowStart = spSheet_NhatTrinhThiCong.ActiveCell.RowIndex;
                Cell start = worksheet.Rows[rowStart][lstTitles[MyConstant.TENCONGTAC]];
                if (start.IsMerged)
                {
                    var group = start.GetMergedRanges().First();
                    rowStart = group.BottomRowIndex;
                }
                worksheet.Rows.Insert(rowStart + 1, 1, RowFormatMode.FormatAsNext);
            }
            finally
            {
                spSheet_NhatTrinhThiCong.EndUpdate();
            }
        }

        private void insertMay_Click(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            item.Click -= insertMay_Click;
            Worksheet worksheet = spSheet_NhatTrinhThiCong.ActiveWorksheet;
            CellRange titleHeader = worksheet.Range[MyConstant.TITLE_HEADER];
            Dictionary<string, int> lstTitles = ExcelHelper.GetListColumnByRange(titleHeader);
            spSheet_NhatTrinhThiCong.BeginUpdate();
            try
            {
                int rowStart = spSheet_NhatTrinhThiCong.ActiveCell.RowIndex;
                int rowGroup = rowStart;
                Cell start = worksheet.Rows[rowStart][lstTitles[MyConstant.TENCONGTAC]];
                if (start.IsMerged)
                {
                    var group = start.GetMergedRanges().First();
                    rowGroup = group.TopRowIndex;
                    start = worksheet.Rows[rowGroup][1];
                }
                start.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                start.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                worksheet.Rows.Insert(rowStart + 1, 1, RowFormatMode.FormatAsNext);
                Cell end = worksheet.Rows[rowStart + 1][lstTitles[MyConstant.TENCONGTAC]];
                worksheet.MergeCells(worksheet.Range[$"{start.GetReferenceA1()}:{end.GetReferenceA1()}"]);
                start = worksheet.Rows[rowGroup][lstTitles[MyConstant.STT]];
                start.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                start.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                end = worksheet.Rows[rowStart + 1][lstTitles[MyConstant.STT]];
                worksheet.MergeCells(worksheet.Range[$"{start.GetReferenceA1()}:{end.GetReferenceA1()}"]);
                worksheet.Rows[rowStart + 1][lstTitles[MyConstant.CODECONGTACTheoGiaiDoan]].Value = worksheet.Rows[rowStart][lstTitles[MyConstant.CODECONGTACTheoGiaiDoan]].Value;
                worksheet.Rows[rowStart + 1][lstTitles[MyConstant.CODECONGTACTHUCONG]].Value = worksheet.Rows[rowStart][lstTitles[MyConstant.CODECONGTACTHUCONG]].Value;
                worksheet.Rows[rowStart + 1][lstTitles[MyConstant.TYPEROW]].Value = worksheet.Rows[rowStart][lstTitles[MyConstant.TYPEROW]].Value;
                worksheet.Rows[rowStart + 1][lstTitles[MyConstant.TENHANGMUC]].Value = worksheet.Rows[rowStart][lstTitles[MyConstant.TENHANGMUC]].Value;
                worksheet.Rows[rowStart + 1][lstTitles[MyConstant.NGAYTHANG]].Value = worksheet.Rows[rowStart][lstTitles[MyConstant.NGAYTHANG]].Value;
                worksheet.Rows[rowStart + 1][lstTitles[MyConstant.DONVI]].Value = worksheet.Rows[rowStart][lstTitles[MyConstant.DONVI]].Value;
                worksheet.Rows[rowStart + 1][lstTitles[MyConstant.KHOILUONG]].Value = worksheet.Rows[rowStart][lstTitles[MyConstant.KHOILUONG]].Value;
            }
            finally
            {
                spSheet_NhatTrinhThiCong.EndUpdate();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            InitDataSoure();
            LoadData();
        }
        private void Fcn_LoadDataTheoMay()
        {
            FileHelper.fcn_spSheetStreamDocument(spSheet_NhatTrinhThiCong, $@"{BaseFrom.m_templatePath}\FileExcel\18.MayThiCong.xlsx");
            IWorkbook workbook = spSheet_NhatTrinhThiCong.Document;
            Worksheet sheet = workbook.Worksheets["Nhập hàng ngày MTC theo máy"];
            workbook.Worksheets.Where(x => x.Name != sheet.Name).ToList().ForEach(x => x.Visible = false);
            CellRange RangeData = sheet.Range["Data"];
            CellRange Mau = workbook.Worksheets["Data"].Range["Data_Mau"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            sheet.Visible = true;
            spSheet_NhatTrinhThiCong.Document.History.IsEnabled = false;
            DonViThucHien DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;
            if (DVTH is null)
            {
                return;
            }
            spSheet_NhatTrinhThiCong.BeginUpdate();
            WaitFormHelper.ShowWaitForm("Đang tải danh sách công tác");
            string dbString = $"SELECT DM.MucTieuThu,may.Code as CodeMayToanDuAn,mayDA.Code as CodeMay,may.Ten as TenMayThiCong,may.CaMayKm," +
             $"DM.Code as CodeDinhMuc,DMTK.DinhMucCongViec AS DinhMucMacDinh," +
             $"cttk.Code AS CodeCongTacTheoGiaiDoan,( CASE WHEN nt.CodeCongTacThuCong IS NOT NULL  THEN 2 WHEN nt.CodeCongTacTheoGiaiDoan IS NOT NULL THEN 1 ELSE 3 END ) AS TypeRow," +
              $" hm.Code as CodeHangMuc,COALESCE(hm.Ten, ntct.TenHangMuc) AS TenHangMuc,ctrinh.Code as CodeCongTrinh,ctrinh.Ten as TenCongTrinh," +
              $" COALESCE(dmct.TenCongTac,cttk.TenCongTac,ntct.TenCongTac) as TenCongTac" +
              $", COALESCE(dmct.MaHieuCongTac,cttk.MaHieuCongTac,ntct.MaHieuCongTac) as MaHieuCongTac," +
              $"COALESCE(dmct.DonVi,cttk.DonVi,ntct.DonVi) as DonVi, \r\n" +
              $"ctrinh.CodeDuAn, \r\n" +
             $"nt.* FROM {MyConstant.TBL_MTC_DUANINMAY} mayDA " +
         $" LEFT JOIN {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt ON mayDA.Code=nt.CodeMayDuAn  AND nt.NgayThang='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'  " +
         $"LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} may ON may.Code=mayDA.CodeMay " +
         $"LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUC} DM ON DM.Code=nt.CodeDinhMuc " +
         $"LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUCTHAMKHAO} DMTK ON DMTK.Code=DM.CodeDinhMuc " +
         $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk ON cttk.Code=nt.CodeCongTacTheoGiaiDoan AND cttk.{DVTH.ColCodeFK}='{DVTH.Code}' AND (cttk.IsUseMTC=1 OR cttk.IsUseMTC='True') " +
         $"LEFT JOIN {MyConstant.TBL_MTC_NHATTRINHCONGTAC} ntct ON ntct.Code=nt.CodeCongTacThuCong AND ntct.{DVTH.ColCodeFK}='{DVTH.Code}' AND ntct.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'" +
          $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
          $"ON cttk.CodeCongTac = dmct.Code \r\n" +
          $"LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
          $"ON dmct.CodeHangMuc = hm.Code \r\n" +
          $"LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
          $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
          $"LEFT JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
          $"ON ctrinh.CodeDuAn = da.Code AND da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' \r\n" +
         $"  ORDER BY may.SortId ASC";
            List<MTC_ChiTietHangNgay> ChiTietMay = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
            string currentDate = DateTime.Parse(date_NhapLieu.EditValue.ToString()).ToString("yyyy-MM-dd");
            int RowIndex = RangeData.TopRowIndex;
            var GrTenMay = ChiTietMay.GroupBy(x => x.CodeMay);
            int STT = 1;
            bool _ThuCong = false;
            int RowCha = 0;
            
            foreach (var TenMay in GrTenMay)
            {
                var FirstTenMay = TenMay.FirstOrDefault();
                Row crRowWs = sheet.Rows[RowIndex++];
                sheet.Rows.Insert(RowIndex, 7, RowFormatMode.FormatAsNext);
                crRowWs.CopyFrom(Mau, PasteSpecial.All);
                crRowWs[Name["TenMayThiCong"]].SetValue(FirstTenMay.TenMayThiCong);
                crRowWs[Name[TDKH.COL_STT]].SetValue(STT++);
                crRowWs[Name["CodeMay"]].Value = TenMay.Key;
                sheet.Rows[RowIndex][Name["CodeMay"]].Value = TenMay.Key;
                sheet.Rows[RowIndex+1][Name["CodeMay"]].Value = TenMay.Key;
                var grTenCongTac = TenMay.GroupBy(x => x.TypeRow);
                _ThuCong = false;
                foreach (var Type in grTenCongTac)
                {
                    if (Type.Key==3)
                    {
                        sheet.Rows[RowIndex].Visible = false;
                        sheet.Rows[RowIndex+2][Name[TDKH.COL_DanhMucCongTac]].SetValueFromText("(Nhập công tác thủ công tại đây)");
                    }
                    else if (Type.Key == 1)
                    {
                        RowCha = RowIndex;
                        //sheet.Rows[RowIndex - 3][Name["CodeMay"]].Value = TenMay.Key;
                        var GrCongTrinh = Type.GroupBy(x => new { x.CodeCongTrinh, x.TenCongTrinh });
                        foreach (var Ctrinh in GrCongTrinh)
                        {
                            if (Ctrinh.Key.CodeCongTrinh is null)
                                continue;
                            var grHangMuc = Ctrinh.GroupBy(x => new { x.CodeHangMuc, x.TenHangMuc });
                            foreach (var HM in grHangMuc)
                            {                             
                                var grCongTac = HM.GroupBy(x => x.CodeCongTacTheoGiaiDoan);
                                foreach(var CtacTong in grCongTac)
                                {
                                    if (CtacTong.Key is null)
                                        continue;
                                    foreach(var CongTac in CtacTong)
                                    {
                                        //var CongTac = Ctac.FirstOrDefault();
                                        crRowWs = sheet.Rows[RowIndex++];

                                        crRowWs[Name["TenCongTrinh"]].Font.Color = Color.DarkTurquoise;
                                        crRowWs[Name[TDKH.COL_Code]].SetValue(Ctrinh.Key.CodeCongTrinh);
                                        crRowWs[Name["TenCongTrinh"]].SetValue(Ctrinh.Key.TenCongTrinh);

                                        crRowWs[Name["TenHangMuc"]].Font.Color = Color.DarkGreen;
                                        crRowWs[Name[TDKH.COL_Code]].SetValue(HM.Key.CodeHangMuc);
                                        crRowWs[Name["TenHangMuc"]].SetValue(HM.Key.TenHangMuc);
                                        sheet.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsPrevious);

                                        crRowWs[Name["CodeCongTacTheoGiaiDoan"]].SetValue(CtacTong.Key);
                                        crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(CongTac.MaHieuCongTac);
                                        crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(CongTac.TenCongTac);
                                        crRowWs[Name[TDKH.COL_DonVi]].SetValue(CongTac.DonVi);
                                        crRowWs[Name[TDKH.COL_TypeRow]].SetValue(Type.Key);
                                        //crRowWs[Name[TDKH.COL_RowCha]].SetValue(RowCha);
                                        if (CongTac.IsTongGio)
                                        {
                                            crRowWs[Name[MyConstant.TONGGIO]].SetValue(CongTac.TongGio);
                                        }
                                        else if (CongTac.CaMayKm == "Ca")
                                        {
                                            crRowWs[Name[MyConstant.GIOBATDAUSANG]].Value = CongTac.GioBatDauSang;
                                            crRowWs[Name[MyConstant.GIOKETTHUCSANG]].Value = CongTac.GioKetThucSang;
                                            crRowWs[Name[MyConstant.GIOBATDAUCHIEU]].Value = CongTac.GioBatDauChieu;
                                            crRowWs[Name[MyConstant.GIOKETTHUCCHIEU]].Value = CongTac.GioKetThucChieu;
                                            crRowWs[Name[MyConstant.GIOBATDAUTOI]].Value = CongTac.GioBatDauToi;
                                            crRowWs[Name[MyConstant.GIOKETTHUCTOI]].Value = CongTac.GioKetThucToi;
                                            crRowWs[Name[MyConstant.TONGGIOCHIEU]].Value = CongTac.TongGioChieu;
                                            crRowWs[Name[MyConstant.TONGGIOSANG]].Value = CongTac.TongGioSang;
                                            crRowWs[Name[MyConstant.TONGGIOTOI]].Value = CongTac.TongGioToi;
                                            crRowWs[Name[MyConstant.TONGGIO]].Formula = $"{crRowWs[Name[MyConstant.TONGGIOCHIEU]].GetReferenceA1()}" +
                                                $"+{crRowWs[Name[MyConstant.TONGGIOSANG]].GetReferenceA1()}+{crRowWs[Name[MyConstant.TONGGIOTOI]].GetReferenceA1()}";
                                        }
                                        else
                                        {

                                            crRowWs[Name[MyConstant.KmDau]].Value = CongTac.KmDau;
                                            crRowWs[Name[MyConstant.KmCuoi]].Value = CongTac.KmCuoi;
                                            crRowWs[Name[MyConstant.TONGGIO]].Formula = $"{crRowWs[Name[MyConstant.KmCuoi]].GetReferenceA1()}-{crRowWs[Name[MyConstant.KmDau]].GetReferenceA1()}";
                                        }
                                        crRowWs[Name[MyConstant.NGAYTHANG]].SetValueFromText(currentDate);
                                        crRowWs[Name[MyConstant.GHICHU]].Value = CongTac.GhiChu;
                                        crRowWs[Name[MyConstant.NHIENLIEUCHINH]].Value = CongTac.NhienLieuChinh;
                                        crRowWs[Name[MyConstant.CODE]].Value = CongTac.Code;
                                        crRowWs[Name["CodeMay"]].Value = TenMay.Key;
                                        crRowWs[Name["CodeMayToanDuAn"]].Value = CongTac.CodeMayToanDuAn;
                                        crRowWs[Name["CodeDinhMuc"]].Value = CongTac.CodeDinhMuc;
                                        crRowWs[Name["DinhMucMacDinh"]].Value = CongTac.DinhMucMacDinh;
                                        crRowWs[Name["CaMayKm"]].Value = CongTac.CaMayKm;
                                        crRowWs[Name["MucTieuThu"]].Value = CongTac.MucTieuThu;
                                        crRowWs[Name[TDKH.COL_KhoiLuong]].SetValue(CongTac.KhoiLuong);
                                        crRowWs[Name["TienTaiXe"]].SetValue(CongTac.TienTaiXe);
                                        crRowWs[Name["LyLichSuaChua"]].SetValue(CongTac.LyLichSuaChua);
                                        crRowWs[Name["IsTongGio"]].SetValue(CongTac.IsTongGio);
                                        sheet.Hyperlinks.Add(crRowWs[Name[GiaoViec.COL_FileDinhKem]], $"{Name[GiaoViec.COL_FileDinhKem]}{RowIndex}", false, "Xem chi tiết");
                                    }
                                }
                            }
                        }
                        sheet.Rows[RowIndex].Visible = false;
                    }
                    else
                    {
                        _ThuCong = true;
                        RowIndex = RowIndex + 2;
                        RowCha = RowIndex;
                        var grCongTac = Type.GroupBy(x => x.CodeCongTacThuCong);
                        sheet.Rows[RowIndex-2].Visible = false;
                        foreach (var CtacTong in grCongTac)
                        {
                            if (CtacTong.Key is null)
                                continue;
                            foreach (var CongTac in CtacTong)
                            {
                                crRowWs = sheet.Rows[RowIndex++];

                                crRowWs[Name["TenHangMuc"]].Font.Color = Color.DarkGreen;
                                crRowWs[Name["TenHangMuc"]].SetValue(CongTac.TenHangMuc);
                                sheet.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsPrevious);

                                crRowWs[Name["CodeCongTacThuCong"]].SetValue(CtacTong.Key);
                                crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(CongTac.MaHieuCongTac);
                                crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(CongTac.TenCongTac);
                                crRowWs[Name[TDKH.COL_DonVi]].SetValue(CongTac.DonVi);
                                crRowWs[Name[TDKH.COL_TypeRow]].SetValue(Type.Key);
                                crRowWs[Name[TDKH.COL_RowCha]].SetValue(RowCha);
                                if (CongTac.IsTongGio)
                                {
                                    crRowWs[Name[MyConstant.TONGGIO]].SetValue(CongTac.TongGio);
                                }
                                else if (CongTac.CaMayKm == "Ca")
                                {
                                    crRowWs[Name[MyConstant.GIOBATDAUSANG]].Value = CongTac.GioBatDauSang;
                                    crRowWs[Name[MyConstant.GIOKETTHUCSANG]].Value = CongTac.GioKetThucSang;
                                    crRowWs[Name[MyConstant.GIOBATDAUCHIEU]].Value = CongTac.GioBatDauChieu;
                                    crRowWs[Name[MyConstant.GIOKETTHUCCHIEU]].Value = CongTac.GioKetThucChieu;
                                    crRowWs[Name[MyConstant.GIOBATDAUTOI]].Value = CongTac.GioBatDauToi;
                                    crRowWs[Name[MyConstant.GIOKETTHUCTOI]].Value = CongTac.GioKetThucToi;
                                    crRowWs[Name[MyConstant.TONGGIOCHIEU]].Value = CongTac.TongGioChieu;
                                    crRowWs[Name[MyConstant.TONGGIOSANG]].Value = CongTac.TongGioSang;
                                    crRowWs[Name[MyConstant.TONGGIOTOI]].Value = CongTac.TongGioToi;
                                    crRowWs[Name[MyConstant.TONGGIO]].Formula = $"{crRowWs[Name[MyConstant.TONGGIOCHIEU]].GetReferenceA1()}" +
                                        $"+{crRowWs[Name[MyConstant.TONGGIOSANG]].GetReferenceA1()}+{crRowWs[Name[MyConstant.TONGGIOTOI]].GetReferenceA1()}";
                                }
                                else
                                {
                                    crRowWs[Name[MyConstant.KmDau]].Value = CongTac.KmDau;
                                    crRowWs[Name[MyConstant.KmCuoi]].Value = CongTac.KmCuoi;
                                    crRowWs[Name[MyConstant.TONGGIO]].Formula = $"{crRowWs[Name[MyConstant.KmCuoi]].GetReferenceA1()}-{crRowWs[Name[MyConstant.KmDau]].GetReferenceA1()}";
                                }
                                crRowWs[Name[MyConstant.NGAYTHANG]].SetValueFromText(currentDate);
                                crRowWs[Name[MyConstant.GHICHU]].Value = CongTac.GhiChu;
                                crRowWs[Name[MyConstant.NHIENLIEUCHINH]].Value = CongTac.NhienLieuChinh;
                                crRowWs[Name[MyConstant.CODE]].Value = CongTac.Code;
                                crRowWs[Name["CodeMay"]].Value = TenMay.Key;
                                crRowWs[Name["CodeMayToanDuAn"]].Value = CongTac.CodeMayToanDuAn;
                                crRowWs[Name["CodeDinhMuc"]].Value = CongTac.CodeDinhMuc;
                                crRowWs[Name["DinhMucMacDinh"]].Value = CongTac.DinhMucMacDinh;
                                crRowWs[Name["CaMayKm"]].Value = CongTac.CaMayKm;
                                crRowWs[Name["MucTieuThu"]].Value = CongTac.MucTieuThu;
                                crRowWs[Name[TDKH.COL_KhoiLuong]].SetValue(CongTac.KhoiLuong);
                                crRowWs[Name["TienTaiXe"]].SetValue(CongTac.TienTaiXe);
                                crRowWs[Name["LyLichSuaChua"]].SetValue(CongTac.LyLichSuaChua);
                                crRowWs[Name["IsTongGio"]].SetValue(CongTac.IsTongGio);
                                sheet.Hyperlinks.Add(crRowWs[Name[GiaoViec.COL_FileDinhKem]], $"{Name[GiaoViec.COL_FileDinhKem]}{RowIndex}", false, "Xem chi tiết");
                            }
                        }
                        sheet.Rows[RowIndex][Name[TDKH.COL_DanhMucCongTac]].SetValueFromText("(Nhập công tác thủ công tại đây)");
                    }
                }
                if (!_ThuCong)
                {
                    RowIndex = RowIndex + 5;
                }
                else
                {
                    RowIndex = RowIndex + 3;
                }
                sheet.Rows[RowIndex-2][Name["CodeMay"]].Value = TenMay.Key;
                sheet.Rows[RowIndex-3][Name["CodeMay"]].Value = TenMay.Key;
                sheet.Rows[RowIndex - 1].Visible = false;
            }
            spSheet_NhatTrinhThiCong.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_LoadDataNhatTrinh()
        {
            if(_TheoTenMay)
            {
                Fcn_LoadDataTheoMay();
                return;
            }    
            FileHelper.fcn_spSheetStreamDocument(spSheet_NhatTrinhThiCong, $@"{BaseFrom.m_templatePath}\FileExcel\18.MayThiCong.xlsx");
            IWorkbook workbook = spSheet_NhatTrinhThiCong.Document;
            Worksheet sheet = workbook.Worksheets[0];
            workbook.Worksheets.Where(x => x.Name != sheet.Name).ToList().ForEach(x => x.Visible = false);
            sheet.Visible = true;
            CellRange RangeData = sheet.Range["Data"];
            CellRange titleHeader = sheet.Range[MyConstant.TITLE_HEADER];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(titleHeader);
            spSheet_NhatTrinhThiCong.Document.History.IsEnabled = false;
            spSheet_NhatTrinhThiCong.BeginUpdate();
            DonViThucHien DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;
            if (DVTH is null)
            {
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang tải danh sách công tác");
            string condition = cbb_LayCongTacNgoaiKeHoach.Checked ? "" : $"AND cttk.NgayBatDau<='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND " +
                $"cttk.NgayKetThuc>='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            string dbString = $"SELECT cttk.Code AS CodeCongTacTheoGiaiDoan,1 AS TypeRow," +
                 $" hm.Code as CodeHangMuc,hm.Ten as TenHangMuc,ctrinh.Code as CodeCongTrinh,ctrinh.Ten as TenCongTrinh," +
                 $" dmct.TenCongTac, dmct.MaHieuCongTac,dmct.DonVi, \r\n" +
                 $"ctrinh.CodeDuAn \r\n" +
                 $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                 $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                 $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                 $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                 $"ON dmct.CodeHangMuc = hm.Code \r\n" +
                 $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                 $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
                 $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                 $"ON ctrinh.CodeDuAn = da.Code \r\n" +
                 $"WHERE da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' AND cttk.{DVTH.ColCodeFK}='{DVTH.Code}' AND (cttk.IsUseMTC=1 OR cttk.IsUseMTC='True') \r\n" +
                 $"{condition} " +
                 $"ORDER BY ctrinh.SortId ASC, hm.SortId ASC, cttk.SortId ASC\r\n";
            List<MTC_ChiTietHangNgay> dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
            string lstCode = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.Select(x => x.CodeCongTacTheoGiaiDoan).ToArray());
            dbString = $"SELECT DM.MucTieuThu,may.Code as CodeMayToanDuAn,mayDA.Code as CodeMay,may.Ten as TenMayThiCong,may.CaMayKm," +
                $"DM.Code as CodeDinhMuc,DMTK.DinhMucCongViec AS DinhMucMacDinh," +
                $"nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt " +
            $" LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} mayDA ON mayDA.Code=nt.CodeMayDuAn " +
            $"LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} may ON may.Code=mayDA.CodeMay " +
            //$"LEFT JOIN {MyConstant.TBL_MTC_NHIENLIEUINMAY} NL ON may.Code=NL.CodeMay " +
            $"LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUC} DM ON DM.Code=nt.CodeDinhMuc " +
            $"LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUCTHAMKHAO} DMTK ON DMTK.Code=DM.CodeDinhMuc " +
            $"WHERE nt.CodeCongTacTheoGiaiDoan IN ({lstCode})  AND nt.NgayThang='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'  ORDER BY may.SortId ASC";

            List<MTC_ChiTietHangNgay> ChiTietMay = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);

            string currentDate = DateTime.Parse(date_NhapLieu.EditValue.ToString()).ToString("yyyy-MM-dd");
            if (RangeData.RowCount - 1 <= dtCongTacTheoKy.Count())
                sheet.Rows.Insert(RangeData.TopRowIndex + 1, dtCongTacTheoKy.Count(), RowFormatMode.FormatAsNext);
            RangeData = sheet.Range["Data"];
            var GrCongTrinh = dtCongTacTheoKy.GroupBy(x => new { x.CodeCongTrinh, x.TenCongTrinh });
            int RowIndex = RangeData.TopRowIndex;
            int STT = 1;
            List<MTC_ChiTietHangNgay> ChiTiet = new List<MTC_ChiTietHangNgay>();
            int RowNV = 0;
            CellRange Merge;
            string[] LstMerge = { MyConstant.STT, TDKH.COL_MaHieuCongTac, TDKH.COL_DanhMucCongTac, TDKH.COL_DonVi, "TenHangMuc" };
            KeyValuePair<int, int> IndexMerge;            
            foreach (var Ctrinh in GrCongTrinh)
            {
                Row crRowWs = sheet.Rows[RowIndex++];
                crRowWs.Font.Bold = true;
                crRowWs.Font.Color = Color.DarkTurquoise;
                crRowWs[Name[TDKH.COL_Code]].SetValue(Ctrinh.Key.CodeCongTrinh);
                crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(MyConstant.CONST_TYPE_CONGTRINH);
                crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(Ctrinh.Key.TenCongTrinh);
                sheet.Rows.Insert(RowIndex + 1, 1, RowFormatMode.FormatAsNext);
                var grHangMuc = Ctrinh.GroupBy(x => new { x.CodeHangMuc, x.TenHangMuc});
                foreach (var HM in grHangMuc)
                {
                    crRowWs = sheet.Rows[RowIndex++];
                    sheet.Rows.Insert(RowIndex + 1, 1, RowFormatMode.FormatAsNext);
                    crRowWs.Font.Bold = true;
                    crRowWs.Font.Color = Color.DarkGreen;
                    crRowWs[Name[TDKH.COL_Code]].SetValue(HM.Key.CodeHangMuc);
                    crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(MyConstant.CONST_TYPE_HANGMUC);
                    crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(HM.Key.TenHangMuc);

                    var grCongTac = HM.GroupBy(x => x.CodeCongTacTheoGiaiDoan);
                    foreach (var CongTac in grCongTac)
                    {
                        IndexMerge = new KeyValuePair<int, int>();
                        var FirstCT = CongTac.FirstOrDefault();

                        crRowWs = sheet.Rows[RowIndex++];
                        sheet.Rows.Insert(RowIndex + 1, 1, RowFormatMode.FormatAsNext);
                        crRowWs.Visible = true;
                        crRowWs.Font.Bold = false;
                        crRowWs.Font.Color = Color.Black;
                        crRowWs[Name["CodeCongTacTheoGiaiDoan"]].SetValue(CongTac.Key);
                        crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(FirstCT.MaHieuCongTac);
                        crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(FirstCT.TenCongTac);
                        crRowWs[Name[TDKH.COL_DonVi]].SetValue(FirstCT.DonVi);
                        crRowWs[Name["TenHangMuc"]].SetValue(HM.Key.TenHangMuc);
                        crRowWs[Name[TDKH.COL_TypeRow]].SetValue(FirstCT.TypeRow);
                        crRowWs[Name[TDKH.COL_STT]].SetValue(STT++);
                        if (ChiTietMay.Any())
                        {
                            ChiTiet = ChiTietMay.Where(x => x.CodeCongTacTheoGiaiDoan == CongTac.Key).ToList();
                            if (ChiTiet.Any())
                            {
                                RangeData = sheet.Range["Data"];
                                RowNV = RowIndex - 1;
                                IndexMerge = new KeyValuePair<int, int>(RowNV + 1, RowNV + ChiTiet.Count());
                                sheet.Rows.Insert(RangeData.BottomRowIndex, ChiTiet.Count(), RowFormatMode.FormatAsPrevious);
                                foreach (var itemNV in ChiTiet)
                                {
                                    Row crRowWsnv = sheet.Rows[RowNV++];
                                    if (itemNV.CaMayKm == "Ca")
                                    {
                                        crRowWsnv[Name[MyConstant.GIOBATDAUSANG]].Value = itemNV.GioBatDauSang;
                                        crRowWsnv[Name[MyConstant.GIOKETTHUCSANG]].Value = itemNV.GioKetThucSang;
                                        crRowWsnv[Name[MyConstant.GIOBATDAUCHIEU]].Value = itemNV.GioBatDauChieu;
                                        crRowWsnv[Name[MyConstant.GIOKETTHUCCHIEU]].Value = itemNV.GioKetThucChieu;
                                        crRowWsnv[Name[MyConstant.GIOBATDAUTOI]].Value = itemNV.GioBatDauToi;
                                        crRowWsnv[Name[MyConstant.GIOKETTHUCTOI]].Value = itemNV.GioKetThucToi;
                                        crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].Value = itemNV.TongGioChieu;
                                        crRowWsnv[Name[MyConstant.TONGGIOSANG]].Value = itemNV.TongGioSang;
                                        crRowWsnv[Name[MyConstant.TONGGIOTOI]].Value = itemNV.TongGioToi;
                                        crRowWsnv[Name[MyConstant.TONGGIO]].Formula = $"{crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].GetReferenceA1()}" +
                                            $"+{crRowWsnv[Name[MyConstant.TONGGIOSANG]].GetReferenceA1()}+{crRowWsnv[Name[MyConstant.TONGGIOTOI]].GetReferenceA1()}";
                                    }
                                    else
                                    {

                                        crRowWsnv[Name[MyConstant.KmDau]].Value = itemNV.KmDau;
                                        crRowWsnv[Name[MyConstant.KmCuoi]].Value = itemNV.KmCuoi;
                                        crRowWsnv[Name[MyConstant.TONGGIO]].Formula = $"{crRowWsnv[Name[MyConstant.KmCuoi]].GetReferenceA1()}-{crRowWsnv[Name[MyConstant.KmDau]].GetReferenceA1()}";
                                    }
                                    crRowWsnv.Font.Color = Color.Black;
                                    crRowWsnv[Name["TenMayThiCong"]].SetValue(itemNV.TenMayThiCong);
                                    crRowWsnv[Name[MyConstant.NGAYTHANG]].SetValueFromText(currentDate);
                                    crRowWsnv[Name[MyConstant.GHICHU]].Value = itemNV.GhiChu;
                                    crRowWsnv[Name[MyConstant.NHIENLIEUCHINH]].Value = itemNV.NhienLieuChinh;
                                    //crRowWsnv[Name[MyConstant.NHIENLIEUPHU]].Value = itemNV.NhienLieuPhu;
                                    crRowWsnv[Name[MyConstant.CODE]].Value = itemNV.Code;
                                    crRowWsnv[Name[MyConstant.CODECONGTACTheoGiaiDoan]].Value = itemNV.CodeCongTacTheoGiaiDoan;
                                    crRowWsnv[Name[MyConstant.CODECONGTACTHUCONG]].Value = itemNV.CodeCongTacThuCong;
                                    crRowWsnv[Name["CodeMay"]].Value = itemNV.CodeMay;
                                    crRowWsnv[Name["CodeMayToanDuAn"]].Value = itemNV.CodeMayToanDuAn;
                                    crRowWsnv[Name["CodeDinhMuc"]].Value = itemNV.CodeDinhMuc;
                                    crRowWsnv[Name["DinhMucMacDinh"]].Value = itemNV.DinhMucMacDinh;
                                    crRowWsnv[Name["CaMayKm"]].Value = itemNV.CaMayKm;
                                    crRowWsnv[Name["MucTieuThu"]].Value = itemNV.MucTieuThu;
                                    crRowWsnv[Name[TDKH.COL_TypeRow]].SetValue(1);
                                    crRowWsnv[Name[TDKH.COL_KhoiLuong]].SetValue(itemNV.KhoiLuong);
                                    crRowWsnv[Name["TienTaiXe"]].SetValue(itemNV.TienTaiXe);
                                    crRowWsnv[Name["LyLichSuaChua"]].SetValue(itemNV.LyLichSuaChua);
                                    sheet.Hyperlinks.Add(crRowWsnv[Name[GiaoViec.COL_FileDinhKem]], $"{Name[GiaoViec.COL_FileDinhKem]}{RowNV}", false, "Xem chi tiết");
                                    if (!string.IsNullOrEmpty(itemNV.NhienLieuPhu)&&itemNV.NhienLieuPhu!="0")
                                    {
                                        dbString = $"SELECT NL.Ten,NL.DonVi,NL.Code " +
                                            $"FROM {MyConstant.TBL_MTC_NHIENLIEUINMAY} NLDA" +
                                            $" LEFT JOIN {MyConstant.TBL_MTC_NHIENLIEU} NL ON NL.Code=NLDA.CodeNhienLieu " +
                                            $" LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} May ON May.Code=NLDA.CodeMay " +
                                            $"LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} MAYDA ON MAYDA.CodeMay=MAY.Code " +
                                            $"LEFT JOIN {MyConstant.TBL_MTC_CHITIETNHATTRINH} CT ON CT.CodeMayDuAn=MAYDA.Code " +
                                            $" WHERE MAYDA.Code='{itemNV.CodeMay}' AND NLDA.LoaiNhienLieu=2 AND CT.Code='{itemNV.Code}'";
                                        List<MTC_NhienLieu> NLCP = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_NhienLieu>(dbString);
                                        var ChiTietNLPhu =JsonConvert.DeserializeObject<List<MTC_NhienLieuPhuJson>>(itemNV.NhienLieuPhu) ?? new List<MTC_NhienLieuPhuJson> { };
                                        var ChiTietNLPhuNew = new List<MTC_NhienLieuPhuJson>();

                                        foreach (var itemnl in ChiTietNLPhu)
                                        {
                                            List<MTC_NhienLieu> NLCPNew = NLCP.Where(x => x.Code == itemnl.CodeNhienLieu).ToList();
                                            if (!NLCPNew.Any())
                                                continue;
                                            itemnl.Ten = NLCPNew.SingleOrDefault().Ten;
                                            itemnl.DonVi = NLCPNew.SingleOrDefault().DonVi;
                                            ChiTietNLPhuNew.Add(itemnl);
                                        }
                                        string[] TenKL = ChiTietNLPhuNew.Select(x => $"{x.Ten}: {x.MucTieuThu}").ToArray();
                                        string Display = TenKL.Any() ? string.Join(",", TenKL) : string.Empty;
                                        crRowWsnv[Name[MyConstant.NHIENLIEUPHU]].SetValueFromText(Display);
                                        crRowWsnv[Name["JSON"]].SetValueFromText(itemNV.NhienLieuPhu);
                                    }
                                }
                                RowIndex = RowIndex + ChiTiet.Count() - 1;
                            }
                        }
                        if (IndexMerge.Key != 0)
                        {
                            foreach (var itemmerge in LstMerge)
                            {
                                string ColHeading = Name[itemmerge];
                                CellRange RangeMerge = sheet.Range[$"{ColHeading}{IndexMerge.Key}:{ColHeading}{IndexMerge.Value}"];
                                sheet.MergeCells(RangeMerge);
                            }
                        }
                    }
                }

            }

            if (cbb_LayCongTacNgoaiKeHoach.Checked)
            {
             
                dbString = $"SELECT cttk.* FROM {MyConstant.TBL_MTC_NHATTRINHCONGTAC} cttk " +
                    $"WHERE {DVTH.ColCodeFK}='{DVTH.Code}' AND \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
                List<MTC_NhatTrinhCongTac> ThuCong = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_NhatTrinhCongTac>(dbString);
                lstCode = MyFunction.fcn_Array2listQueryCondition(ThuCong.Select(x => x.Code).ToArray());
                if (ThuCong.Any())
                {
                    Row crRowWs = sheet.Rows[RowIndex++];
                    sheet.Rows.Insert(RowIndex + 1, 1, RowFormatMode.FormatAsNext);
                    crRowWs.Visible = true;
                    crRowWs.Font.Bold = true;
                    crRowWs.Font.Color = Color.DarkGreen;
                    crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValueFromText("CÔNG TÁC THỦ CÔNG");
                }
                dbString = $"SELECT DM.MucTieuThu,may.Code as CodeMayToanDuAn,mayDA.Code as CodeMay,may.Ten as TenMayThiCong,may.CaMayKm,DM.Code as CodeDinhMuc,DMTK.DinhMucCongViec AS DinhMucMacDinh," +
                    $"nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt " +
            $" LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} mayDA ON mayDA.Code=nt.CodeMayDuAn " +
            $"LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} may ON may.Code=mayDA.CodeMay " +
            $"LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUC} DM ON DM.Code=nt.CodeDinhMuc " +
            $"LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUCTHAMKHAO} DMTK ON DMTK.Code=DM.CodeDinhMuc " +
                $"WHERE nt.CodeCongTacThuCong IN ({lstCode}) AND nt.NgayThang='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' ORDER BY may.SortId ASC ";
                ChiTietMay = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
                foreach (var item in ThuCong)
                {
                    IndexMerge = new KeyValuePair<int, int>();
                    Row crRowWs = sheet.Rows[RowIndex++];
                    sheet.Rows.Insert(RowIndex + 1, 1, RowFormatMode.FormatAsNext);
                    crRowWs.Visible = true;
                    crRowWs.Font.Bold = false;
                    crRowWs.Font.Color = Color.Black;
                    crRowWs[Name["CodeCongTacThuCong"]].SetValue(item.Code);
                    crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(item.MaHieuCongTac);
                    crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(item.TenCongTac);
                    crRowWs[Name[TDKH.COL_DonVi]].SetValue(item.DonVi);
                    crRowWs[Name[TDKH.COL_TypeRow]].SetValue(item.TypeRow);
                    crRowWs[Name[TDKH.COL_STT]].SetValue(STT++);
                    crRowWs[Name["TenHangMuc"]].SetValue(item.TenHangMuc);
                    if (ChiTietMay.Any())
                    {
                        ChiTiet = ChiTietMay.Where(x => x.CodeCongTacThuCong == item.Code).ToList();
                        if (ChiTiet.Any())
                        {
                            RangeData = sheet.Range["Data"];
                            RowNV = RowIndex - 1;
                            IndexMerge = new KeyValuePair<int, int>(RowNV + 1, RowNV + ChiTiet.Count());
                            sheet.Rows.Insert(RangeData.BottomRowIndex, ChiTiet.Count(), RowFormatMode.FormatAsPrevious);
                            foreach (var itemNV in ChiTiet)
                            {
                                Row crRowWsnv = sheet.Rows[RowNV++];
                                if (itemNV.CaMayKm == "Ca")
                                {
                                    crRowWsnv[Name[MyConstant.GIOBATDAUSANG]].Value = itemNV.GioBatDauSang;
                                    crRowWsnv[Name[MyConstant.GIOKETTHUCSANG]].Value = itemNV.GioKetThucSang;
                                    crRowWsnv[Name[MyConstant.GIOBATDAUCHIEU]].Value = itemNV.GioBatDauChieu;
                                    crRowWsnv[Name[MyConstant.GIOKETTHUCCHIEU]].Value = itemNV.GioKetThucChieu;
                                    crRowWsnv[Name[MyConstant.GIOBATDAUTOI]].Value = itemNV.GioBatDauToi;
                                    crRowWsnv[Name[MyConstant.GIOKETTHUCTOI]].Value = itemNV.GioKetThucToi;
                                    crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].Value = itemNV.TongGioChieu;
                                    crRowWsnv[Name[MyConstant.TONGGIOSANG]].Value = itemNV.TongGioSang;
                                    crRowWsnv[Name[MyConstant.TONGGIOTOI]].Value = itemNV.TongGioToi;
                                    crRowWsnv[Name[MyConstant.TONGGIO]].Formula = $"{crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].GetReferenceA1()}" +
                                        $"+{crRowWsnv[Name[MyConstant.TONGGIOSANG]].GetReferenceA1()}+{crRowWsnv[Name[MyConstant.TONGGIOTOI]].GetReferenceA1()}";
                                }
                                else
                                {

                                    crRowWsnv[Name[MyConstant.KmDau]].Value = itemNV.KmDau;
                                    crRowWsnv[Name[MyConstant.KmCuoi]].Value = itemNV.KmCuoi;
                                    crRowWsnv[Name[MyConstant.TONGGIO]].Formula = $"{crRowWsnv[Name[MyConstant.KmCuoi]].GetReferenceA1()}-{crRowWsnv[Name[MyConstant.KmDau]].GetReferenceA1()}";
                                }
                                crRowWsnv.Font.Color = Color.Black;
                                crRowWsnv[Name["TenMayThiCong"]].SetValue(itemNV.TenMayThiCong);
                                crRowWsnv[Name[MyConstant.NGAYTHANG]].SetValueFromText(currentDate);
                                crRowWsnv[Name[MyConstant.GHICHU]].Value = itemNV.GhiChu;
                                crRowWsnv[Name[MyConstant.NHIENLIEUCHINH]].Value = itemNV.NhienLieuChinh;
                                //crRowWsnv[Name[MyConstant.NHIENLIEUPHU]].Value = itemNV.NhienLieuPhu;
                                crRowWsnv[Name[MyConstant.CODE]].Value = itemNV.Code;
                                crRowWsnv[Name[MyConstant.CODECONGTACTheoGiaiDoan]].Value = itemNV.CodeCongTacTheoGiaiDoan;
                                crRowWsnv[Name[MyConstant.CODECONGTACTHUCONG]].Value = itemNV.CodeCongTacThuCong;
                                crRowWsnv[Name["CodeMay"]].Value = itemNV.CodeMay;
                                crRowWsnv[Name["CodeMayToanDuAn"]].Value = itemNV.CodeMayToanDuAn;
                                crRowWsnv[Name["CodeDinhMuc"]].Value = itemNV.CodeDinhMuc;
                                crRowWsnv[Name["DinhMucMacDinh"]].Value = itemNV.DinhMucMacDinh;
                                crRowWsnv[Name["CaMayKm"]].Value = itemNV.CaMayKm;
                                crRowWsnv[Name["MucTieuThu"]].Value = itemNV.MucTieuThu;
                                crRowWsnv[Name[TDKH.COL_TypeRow]].SetValue(2);
                                crRowWsnv[Name[TDKH.COL_KhoiLuong]].SetValue(itemNV.KhoiLuong);
                                crRowWsnv[Name["TienTaiXe"]].SetValue(itemNV.TienTaiXe);
                                crRowWsnv[Name["LyLichSuaChua"]].SetValue(itemNV.LyLichSuaChua);
                                sheet.Hyperlinks.Add(crRowWsnv[Name[GiaoViec.COL_FileDinhKem]], $"{Name[GiaoViec.COL_FileDinhKem]}{RowNV}", false, "Xem chi tiết");
                                if (!string.IsNullOrEmpty(itemNV.NhienLieuPhu) && itemNV.NhienLieuPhu != "0")
                                {
                                    var ChiTietNLPhu = JsonConvert.DeserializeObject<List<MTC_NhienLieuPhuJson>>(itemNV.NhienLieuPhu);
                                    string[] TenKL = ChiTietNLPhu.Select(x => $"{x.Ten}: {x.MucTieuThu}").ToArray();
                                    string Display = TenKL.Any() ? string.Join(",", TenKL) : string.Empty;
                                    crRowWsnv[Name[MyConstant.NHIENLIEUPHU]].SetValueFromText(Display);
                                    crRowWsnv[Name["JSON"]].SetValueFromText(itemNV.NhienLieuPhu);
                                }
                            }
                            RowIndex = RowIndex + ChiTiet.Count() - 1;

                        }
                    }
                    if (IndexMerge.Key != 0)
                    {
                        foreach (var itemmerge in LstMerge)
                        {
                            string ColHeading = Name[itemmerge];
                            CellRange RangeMerge = sheet.Range[$"{ColHeading}{IndexMerge.Key}:{ColHeading}{IndexMerge.Value}"];
                            sheet.MergeCells(RangeMerge);
                        }
                    }
                }

            }
            spSheet_NhatTrinhThiCong.EndUpdate();
            Fcn_LoadThongBao();
            WaitFormHelper.CloseWaitForm();

        }
        private void spSheet_NhatTrinhThiCong_CellEndEdit(object sender, SpreadsheetCellValidatingEventArgs e)
        {
            Worksheet worksheet = spSheet_NhatTrinhThiCong.ActiveWorksheet;
            CellRange titleHeader = worksheet.Range[MyConstant.TITLE_HEADER];
            Dictionary<string, int> lstTitles = ExcelHelper.GetListColumnByRange(titleHeader);
            string columnName = lstTitles.Where(x => x.Value == e.ColumnIndex)?.First().Key;
            var itemIndex = MapRow(worksheet, lstTitles, e.RowIndex);
            List<string> lstValueLefts = new List<string>();
            List<string> lstValueRights = new List<string>();
            double Time = 0;
            string ColCode =string.Empty;
            string dbString = string.Empty;
            bool Cal = true;
            string FieldName = worksheet.Rows[0][e.ColumnIndex].Value.TextValue;
            string Code = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.CODE]].Value.TextValue;
            switch (columnName)
            {
                case MyConstant.GIOBATDAUSANG:
                    //lstValueRights.Add(itemIndex.GioKetThucSang);
                    //lstValueRights.Add(itemIndex.GioBatDauChieu);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    //lstValueRights.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    //ValidateTime(e, lstValueLefts, lstValueRights, 1);
                    //break;
                    lstValueRights.Add(itemIndex.GioKetThucSang);
                    //lstValueRights.Add(itemIndex.GioBatDauChieu);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    //lstValueRights.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 1);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOSANG]].SetValue(Time);
                    ColCode = "TongGioSang";
                    break;
                case MyConstant.GIOKETTHUCSANG:
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueRights.Add(itemIndex.GioBatDauChieu);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    //lstValueRights.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    //ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    //break;
                    lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueRights.Add(itemIndex.GioBatDauChieu);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    //lstValueRights.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOSANG]].SetValue(Time);
                    ColCode = "TongGioSang";
                    break;
                case MyConstant.GIOBATDAUCHIEU:
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    //lstValueRights.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    //ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    //break;
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    lstValueRights.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 1);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOCHIEU]].SetValue(Time);
                    ColCode = "TongGioChieu";
                    break;
                case MyConstant.GIOKETTHUCCHIEU:
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueLefts.Add(itemIndex.GioBatDauChieu);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);                    
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    //ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    //break;
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    lstValueLefts.Add(itemIndex.GioBatDauChieu);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOCHIEU]].SetValue(Time);
                    ColCode = "TongGioChieu";
                    break;
                case MyConstant.GIOBATDAUTOI:
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueLefts.Add(itemIndex.GioBatDauChieu);
                    //lstValueLefts.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    //ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    //break;
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueLefts.Add(itemIndex.GioBatDauChieu);
                    //lstValueLefts.Add(itemIndex.GioKetThucChieu);
                    lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 1);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOTOI]].SetValue(Time);
                    ColCode = "TongGioToi";
                    break;
                case MyConstant.GIOKETTHUCTOI:
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueLefts.Add(itemIndex.GioBatDauChieu);
                    //lstValueLefts.Add(itemIndex.GioKetThucChieu);
                    //lstValueLefts.Add(itemIndex.GioBatDauToi);
                    //ValidateTime(e, lstValueLefts, lstValueRights, 3);
                    //break;
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueLefts.Add(itemIndex.GioBatDauChieu);
                    //lstValueLefts.Add(itemIndex.GioKetThucChieu);
                    lstValueLefts.Add(itemIndex.GioBatDauToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOTOI]].SetValue(Time);
                    ColCode = "TongGioToi";
                    break;
                case MyConstant.KmDau:
                    ColCode = "KmDau";
                    Cal = false;
                    break;             
                case MyConstant.KmCuoi:
                    ColCode = "KmCuoi";
                    Cal = false;
                    break;
                case MyConstant.NHIENLIEUCHINH:
                    ColCode = "NhienLieuChinh";
                    Cal = false;
                    break;
                case MyConstant.TONGGIO:
                    dbString =$"UPDATE {MyConstant.TBL_MTC_CHITIETNHATTRINH} SET " +
               $"\"TongGio\" = '{e.EditorText}', " +
               $"\"IsTongGio\" = '{true}'" +
               $" WHERE \"Code\" = '{Code}'" ;
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    break;
                case MyConstant.GHICHU:
                    ColCode = "GhiChu";
                    Cal = false;
                    break;             
                case MyConstant.KHOILUONG:
                    ColCode = "KhoiLuong";
                    Cal = false;
                    break;
                case MyConstant.LyLichSuaChua:
                    ColCode = "LyLichSuaChua";
                    Cal = false;
                    break;              
                case MyConstant.TienTaiXe:
                    if(!long.TryParse(e.EditorText,out long TienTaiXe))
                    {
                        MessageShower.ShowError("Vui lòng nhập định dạng số ở đây!!!!!");
                        //spSheet_NhatTrinhThiCong.CloseCellEditor(CellEditorEnterValueMode.Cancel);
                        e.EditorText = "";
                        break;
                    }
                    ColCode = "TienTaiXe";
                    Cal = false;
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(ColCode))
            {
                string CodeMay = worksheet.Rows[e.RowIndex][lstTitles["CodeMay"]].Value.TextValue;
                if (string.IsNullOrEmpty(CodeMay))
                {
                    MessageShower.ShowWarning("Vui lòng Thêm máy thi công trước khi nhập giá trị ô này!!!!!");
                    spSheet_NhatTrinhThiCong.CloseCellEditor(CellEditorEnterValueMode.Cancel);
                    return;
                }
                worksheet.Calculate();
                double TongGio = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIO]].Value.NumericValue;
                dbString = Cal?$"UPDATE {MyConstant.TBL_MTC_CHITIETNHATTRINH} SET " +
               $"\"TongGio\" = '{TongGio}', " +
               $"{ColCode} = '{Time}'," +
               $"{FieldName} = '{e.EditorText}'" +
               $" WHERE \"Code\" = '{Code}'":
               $"UPDATE {MyConstant.TBL_MTC_CHITIETNHATTRINH} SET " +
               $"\"TongGio\" = '{TongGio}', " +
               $"{ColCode} = '{e.EditorText}'" +
               $" WHERE \"Code\" = '{Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            if(columnName== MyConstant.NHIENLIEUCHINH)
            {
                double OldValue = e.Value.IsEmpty ? 0 : double.Parse(e.Value.ToString());
                double NewValue = string.IsNullOrEmpty(e.EditorText) ? 0 : double.Parse(e.EditorText.ToString());
                //Fcn_UpdateDaTaTongHopNhienLieuPhu();
                Fcn_LoadThongBao();
            }
        }

        private void IsValidateDatetime(SpreadsheetCellValidatingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.EditorText))
            {
                TimeSpan timeSpan;
                if (!DateTimeHelper.IsTimeSpan(e.EditorText, out timeSpan))
                {
                    e.EditorText = string.Empty;
                    e.Cancel = true;
                }
            }
        }
        private double ValidateTime(SpreadsheetCellValidatingEventArgs e, List<string> lstValueColumnLefts, List<string> lstValueColumnRights, int type)
        {
            TimeSpan timeSpanNew = TimeSpan.Zero;
            TimeSpan timeSpan = TimeSpan.Zero;
            double Time = 0;
            if (!string.IsNullOrEmpty(e.EditorText))
            {
                if (!DateTimeHelper.IsTimeSpan(e.EditorText, out timeSpan))
                {
                    e.EditorText = string.Empty;
                    e.Cancel = true;
                }
            }
            TimeSpan timeMin = TimeSpan.Zero;
            TimeSpan timeMax = TimeSpan.Zero;
            timeSpanNew = timeSpan;
            List<TimeSpan> lstTimes;
            switch (type)
            {
                case 1:
                    lstTimes = new List<TimeSpan>();
                    foreach (var item in lstValueColumnRights)
                    {
                        if (string.IsNullOrEmpty(item)) continue;
                        if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
                        {
                            lstTimes.Add(timeSpan);
                        }
                    }
                    if (!lstTimes.Any()) return 0;
                    timeMax = lstTimes.Max();
                    if (!DateTimeHelper.CompareTimeSpanNhoHon(e.EditorText, timeMax))
                    {
                        e.EditorText = string.Empty;
                        e.Cancel = true;
                        break;
                    }
                    timeMin = timeSpanNew;
                    break;
                case 2:
                    lstTimes = new List<TimeSpan>();
                    foreach (var item in lstValueColumnLefts)
                    {
                        if (string.IsNullOrEmpty(item)) continue;
                        if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
                        {
                            lstTimes.Add(timeSpan);
                        }
                    }
                    if (lstTimes.Any())
                    {
                        timeMin = lstTimes.Min();
                    }
                    timeMax = timeSpanNew;
                    if (timeMin != TimeSpan.Zero && timeMax != TimeSpan.Zero || timeMin != TimeSpan.Zero && timeMax == TimeSpan.Zero)
                    {
                        if (!DateTimeHelper.CompareTimeSpanLonHon(e.EditorText, timeMin))
                        {
                            e.EditorText = string.Empty;
                            e.Cancel = true;
                            break;
                        }
                    }
                    //lstTimes = new List<TimeSpan>();
                    //foreach (var item in lstValueColumnRights)
                    //{
                    //    if (string.IsNullOrEmpty(item)) continue;
                    //    if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
                    //    {
                    //        lstTimes.Add(timeSpan);
                    //    }
                    //}
                    //if (lstTimes.Any())
                    //{
                    //    timeMin = lstTimes.Min();
                    //}
                    //if (timeMin != TimeSpan.Zero && timeMax != TimeSpan.Zero)
                    //{
                    //    if (!DateTimeHelper.CompareTimeSpanBetwwen(e.EditorText, timeMax, timeMin))
                    //    {
                    //        e.EditorText = string.Empty;
                    //        e.Cancel = true;
                    //        break;
                    //    }
                    //}
                    //else if (timeMin != TimeSpan.Zero && timeMax == TimeSpan.Zero)
                    //{
                    //    if (!DateTimeHelper.CompareTimeSpanNhoHon(e.EditorText, timeMin))
                    //    {
                    //        e.EditorText = string.Empty;
                    //        e.Cancel = true;
                    //        break;
                    //    }
                    //}
                    //else if (timeMin == TimeSpan.Zero && timeMax != TimeSpan.Zero)
                    //{
                    //    if (!DateTimeHelper.CompareTimeSpanLonHon(e.EditorText, timeMax))
                    //    {
                    //        e.EditorText = string.Empty;
                    //        e.Cancel = true;
                    //        break;
                    //    }
                    //}
                    break;
                case 3:
                    lstTimes = new List<TimeSpan>();
                    foreach (var item in lstValueColumnLefts)
                    {
                        if (string.IsNullOrEmpty(item)) continue;
                        if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
                        {
                            lstTimes.Add(timeSpan);
                        }
                    }
                    if (!lstTimes.Any()) return 0;
                    timeMax = lstTimes.Max();
                    if (!DateTimeHelper.CompareTimeSpanLonHon(e.EditorText, timeMax))
                    {
                        e.EditorText = string.Empty;
                        e.Cancel = true;
                        break;
                    }
                    break;
            }
            if (!e.Cancel)
                return Fcn_CalCulateMinMax(timeMin, timeMax);
            else
                return 0;
        }
        private double Fcn_CalCulateMinMax(TimeSpan timeMin, TimeSpan timeMax)
        {
            double Time = 0;
            if (timeMin == TimeSpan.Zero || timeMax == TimeSpan.Zero)
                return 0;
            Time = Math.Round((double)((double)(timeMax.Hours * 60 + timeMax.Minutes - timeMin.Hours * 60 - timeMin.Minutes) / 60.00), 2);
            return Time;

        }

        //private void ValidateTime(SpreadsheetCellValidatingEventArgs e, List<string> lstValueColumnLefts, List<string> lstValueColumnRights, int type)
        //{
        //    TimeSpan timeSpan;
        //    if (!string.IsNullOrEmpty(e.EditorText))
        //    {
        //        if (!DateTimeHelper.IsTimeSpan(e.EditorText, out timeSpan))
        //        {
        //            e.EditorText = string.Empty;
        //            e.Cancel = true;
        //        }
        //    }
        //    TimeSpan timeMin = TimeSpan.Zero;
        //    TimeSpan timeMax = TimeSpan.Zero;
        //    List<TimeSpan> lstTimes;
        //    switch (type)
        //    {
        //        case 1:
        //            lstTimes = new List<TimeSpan>();
        //            foreach (var item in lstValueColumnRights)
        //            {
        //                if (string.IsNullOrEmpty(item)) continue;
        //                if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan !=TimeSpan.Zero)
        //                {
        //                    lstTimes.Add(timeSpan);
        //                }                
        //            }
        //            if (!lstTimes.Any()) return;
        //            timeMin = lstTimes.Min();                    
        //            if (!DateTimeHelper.CompareTimeSpanNhoHon(e.EditorText, timeMin))
        //            {
        //                e.EditorText = string.Empty;
        //                e.Cancel = true;
        //                break;
        //            }
        //            break;
        //        case 2:
        //            lstTimes = new List<TimeSpan>();
        //            foreach (var item in lstValueColumnLefts)
        //            {
        //                if (string.IsNullOrEmpty(item)) continue;
        //                if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
        //                {
        //                    lstTimes.Add(timeSpan);
        //                }
        //            }
        //            if (lstTimes.Any())
        //            {
        //                timeMax = lstTimes.Max();
        //            }
        //            lstTimes = new List<TimeSpan>();
        //            foreach (var item in lstValueColumnRights)
        //            {
        //                if (string.IsNullOrEmpty(item)) continue;
        //                if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
        //                {
        //                    lstTimes.Add(timeSpan);
        //                }
        //            }
        //            if (lstTimes.Any())
        //            {
        //                timeMin = lstTimes.Min();
        //            }    
        //            if( timeMin!=TimeSpan.Zero && timeMax != TimeSpan.Zero)
        //            {
        //                if (!DateTimeHelper.CompareTimeSpanBetwwen(e.EditorText, timeMax, timeMin))
        //                {
        //                    e.EditorText = string.Empty;
        //                    e.Cancel = true;
        //                    break;
        //                }
        //            }
        //            else if(timeMin != TimeSpan.Zero && timeMax == TimeSpan.Zero)
        //            {
        //                if (!DateTimeHelper.CompareTimeSpanNhoHon(e.EditorText, timeMin))
        //                {
        //                    e.EditorText = string.Empty;
        //                    e.Cancel = true;
        //                    break;
        //                }
        //            }
        //            else if (timeMin == TimeSpan.Zero && timeMax != TimeSpan.Zero)
        //            {
        //                if (!DateTimeHelper.CompareTimeSpanLonHon(e.EditorText, timeMax))
        //                {
        //                    e.EditorText = string.Empty;
        //                    e.Cancel = true;
        //                    break;
        //                }
        //            }
        //            break;
        //        case 3:
        //            lstTimes = new List<TimeSpan>();
        //            foreach (var item in lstValueColumnLefts)
        //            {
        //                if (string.IsNullOrEmpty(item)) continue;
        //                if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
        //                {
        //                    lstTimes.Add(timeSpan);
        //                }
        //            }
        //            if (!lstTimes.Any()) return;
        //            timeMax = lstTimes.Max();
        //            if (!DateTimeHelper.CompareTimeSpanLonHon(e.EditorText, timeMax))
        //            {
        //                e.EditorText = string.Empty;
        //                e.Cancel = true;
        //                break;
        //            }
        //            break;
        //    }
        //}

        private void spSheet_NhatTrinhThiCong_CellBeginEdit(object sender, SpreadsheetCellCancelEventArgs e)
        {
            Worksheet worksheet = spSheet_NhatTrinhThiCong.ActiveWorksheet;
            CellRange titleHeader = worksheet.Range[MyConstant.TITLE_HEADER];
            Dictionary<string, int> lstTitles = ExcelHelper.GetListColumnByRange(titleHeader);
            string columnName = lstTitles.Where(x => x.Value == e.ColumnIndex)?.First().Key;
            string Code = worksheet.Rows[e.RowIndex][lstTitles["Code"]].Value.TextValue;
            string CaMay = "";
            string MaCongTac = worksheet.Rows[e.RowIndex][lstTitles["MaHieuCongTac"]].Value.TextValue;
            if (_TheoTenMay)
            {
                if (MaCongTac == "TC" || MaCongTac == "KH")
                    e.Cancel = true;
                switch (columnName)
                {
                    case MyConstant.STT:
                    case "DinhMucMacDinh":
                    case "NgayThang":
                    case "TenMayThiCong":
                    case "CaMayKm":
                    case "MucTieuThu":
                    case "NhienLieuPhu":
                        e.Cancel = true;
                        break;
                    case "TongGio":
                        if (string.IsNullOrEmpty(Code))
                            e.Cancel = true;
                        bool IsTongGio = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.IsTongGio]].Value.BooleanValue;
                        if (!IsTongGio)
                        {
                            DialogResult rs = MessageShower.ShowYesNoQuestion("Tổng công đang được tính từ chi tiết giờ theo ca hoặc Km đầu cuối, Nếu bạn nhập sẽ mất hoàn toàn chi tiết!!Bạn có muốn tiếp tục nhập không?????");
                            if (rs == DialogResult.No)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "GioBatDauSang":
                    case "GioKetThucSang":
                    case "GioBatDauChieu":
                    case "GioKetThucChieu":
                    case "GioBatDauToi":
                    case "GioKetThucToi":
                        if (string.IsNullOrEmpty(Code) ||MaCongTac == "TC" || MaCongTac == "KH")
                            e.Cancel = true;
                        CaMay = worksheet.Rows[e.RowIndex][lstTitles["CaMayKm"]].Value.TextValue;
                        if (CaMay == "Km")
                        {
                            MessageShower.ShowError("Máy đang thuộc loại đường Km, Vui lòng nhập vào Km đầu và cuối hoặc sang Bảng Danh Sách Máy thi công chọn lại đơn vị là Ca!!!!!");
                            e.Cancel = true;
                        }
                        break;
                    case "KmDau":
                    case "KmCuoi":
                        if (string.IsNullOrEmpty(Code))
                            e.Cancel = true;
                        CaMay = worksheet.Rows[e.RowIndex][lstTitles["CaMayKm"]].Value.TextValue;
                        if (CaMay == "Ca")
                        {
                            MessageShower.ShowError("Máy đang thuộc loại Ca, Vui lòng nhập vào giờ bắt đầu sáng chiều tối hoặc sang Bảng Danh Sách Máy thi công chọn lại đơn vị là Km!!!!!");
                            e.Cancel = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (columnName)
                {
                    case MyConstant.STT:
                    case "DinhMucMacDinh":
                    case "NgayThang":
                    case "TenMayThiCong":
                    case "CaMayKm":
                    case "MucTieuThu":
                    case "NhienLieuPhu":
                        e.Cancel = true;
                        break;
                    case "TongGio":
                        if (string.IsNullOrEmpty(Code))
                            e.Cancel = true;
                        bool IsTongGio = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.IsTongGio]].Value.BooleanValue;
                        if (!IsTongGio)
                        {
                            DialogResult rs = MessageShower.ShowYesNoQuestion("Tổng công đang được tính từ chi tiết giờ theo ca hoặc Km đầu cuối, Nếu bạn nhập sẽ mất hoàn toàn chi tiết!!Bạn có muốn tiếp tục nhập không?????");
                            if (rs == DialogResult.No)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "GioBatDauSang":
                    case "GioKetThucSang":
                    case "GioBatDauChieu":
                    case "GioKetThucChieu":
                    case "GioBatDauToi":
                    case "GioKetThucToi":
                        if (string.IsNullOrEmpty(Code) || MaCongTac == MyConstant.CONST_TYPE_HANGMUC || MaCongTac == MyConstant.CONST_TYPE_CONGTRINH || MaCongTac == "TC" || MaCongTac == "KH")
                            e.Cancel = true;
                        CaMay = worksheet.Rows[e.RowIndex][lstTitles["CaMayKm"]].Value.TextValue;
                        if (CaMay == "Km")
                        {
                            MessageShower.ShowError("Máy đang thuộc loại đường Km, Vui lòng nhập vào Km đầu và cuối hoặc sang Bảng Danh Sách Máy thi công chọn lại đơn vị là Ca!!!!!");
                            e.Cancel = true;
                        }
                        break;
                    case "KmDau":
                    case "KmCuoi":
                        if (string.IsNullOrEmpty(Code) || MaCongTac == MyConstant.CONST_TYPE_HANGMUC || MaCongTac == MyConstant.CONST_TYPE_CONGTRINH)
                            e.Cancel = true;
                        CaMay = worksheet.Rows[e.RowIndex][lstTitles["CaMayKm"]].Value.TextValue;
                        if (CaMay == "Ca")
                        {
                            MessageShower.ShowError("Máy đang thuộc loại Ca, Vui lòng nhập vào giờ bắt đầu sáng chiều tối hoặc sang Bảng Danh Sách Máy thi công chọn lại đơn vị là Km!!!!!");
                            e.Cancel = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void cb_ShowCongTacExitMay_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cbb_LayCongTacNgoaiKeHoach_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void spSheet_NhatTrinhThiCong_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Insert)
                Fcn_LoadDataNhatTrinh();
        }

        private void sb_NhapThemNhienLieu_Click(object sender, EventArgs e)
        {
            XtraForm_NhapNhienLieuHangNgay HN = new XtraForm_NhapNhienLieuHangNgay();
            HN.ShowDialog();
            Fcn_LoadThongBao();
            //Fcn_CalCulateAll();
        }
//        private void Fcn_UpdateDaTaTongHopNhienLieuChinh()
//        {
//            WaitFormHelper.ShowWaitForm("Đang Tính toán lại nhiên liệu đã nhập!!!!!!!!!Vui lòng chờ!!!!!!!!!");
//            string dbString = $"SELECT NL.CodeNhienLieu,may.Code as CodeMayToanDuAn,mayDA.Code as CodeMay,may.Ten as TenMayThiCong,may.CaMayKm, " +
//    $"nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt " +
//$" LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} mayDA ON mayDA.Code=nt.CodeMayDuAn " +
//$"LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} may ON may.Code=mayDA.CodeMay "
//+$"LEFT JOIN {MyConstant.TBL_MTC_NHIENLIEUINMAY} NL ON may.Code=NL.CodeMay " +
//$"WHERE NL.LoaiNhienLieu=1";
//            List<MTC_ChiTietHangNgay> ChiTietMay = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
//            if (!ChiTietMay.Any())
//                return;
//            dbString = $"SELECT TH.* " +
//$"FROM {MyConstant.TBL_MTC_NHIENLIEU} TH ";
//            List<MTC_TongHopNhienLieuHangNgay> Tong = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_TongHopNhienLieuHangNgay>(dbString);

//            dbString = $"SELECT nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt ";

//            List<MTC_ChiTietHangNgay> ChiTietMayPhu = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
//            List<MTC_NhienLieuInMay> NLP = new List<MTC_NhienLieuInMay>();
//            foreach (var item in ChiTietMayPhu)
//            {
//                if (!string.IsNullOrEmpty(item.NhienLieuPhu) && item.NhienLieuPhu != "0")
//                {
//                    var ChiTietNLPhu = JsonConvert.DeserializeObject<List<MTC_NhienLieuInMay>>(item.NhienLieuPhu);
//                    NLP.AddRange(ChiTietNLPhu);
//                }
//            }
//            foreach (var item in Tong)
//            {
//                List<MTC_ChiTietHangNgay> NL = ChiTietMay.Where(x => x.CodeNhienLieu == item.Code).ToList();
//                if (!NL.Any())
//                    continue;
//                item.KhoiLuongDaDung = NL.Sum(x=>x.NhienLieuChinh);
//                List<MTC_NhienLieuInMay> NLPTH = NLP.Where(x => x.CodeNhienLieu == item.Code).ToList();
//                item.KhoiLuongDaDung += NLPTH.Sum(x => x.MucTieuThu);

//            }
//            int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_TongHopNhienLieuHangNgay>("UPDATE_MTC_TongHopNhienLieu",
//Tong, false, true, true);
//            WaitFormHelper.CloseWaitForm();
//        }
/// <summary>
/// KLDA DUNG
/// </summary>
/// <param name="lsCodeNhienLieu"></param>
/// <returns></returns>
//        private double Fcn_UpdateDaTaTongHopNhienLieuPhu(string lsCodeNhienLieu)
//        {
//            //WaitFormHelper.ShowWaitForm("Đang Tính toán lại nhiên liệu đã nhập!!!!!!!!!Vui lòng chờ!!!!!!!!!");
//            string dbString = $"SELECT TH.* " +
// $"FROM {MyConstant.TBL_MTC_NHIENLIEU} TH WHERE TH.Code='{lsCodeNhienLieu}'";
//            List<MTC_TongHopNhienLieuHangNgay> Tong = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_TongHopNhienLieuHangNgay>(dbString);

//            dbString = $"SELECT nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt ";

//            List<MTC_ChiTietHangNgay> ChiTietMay = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
//            List<MTC_NhienLieuInMay> NLP = new List<MTC_NhienLieuInMay>();
//            foreach (var item in ChiTietMay)
//            {
//                if (!string.IsNullOrEmpty(item.NhienLieuPhu) && item.NhienLieuPhu != "0")
//                {
//                    var ChiTietNLPhu = JsonConvert.DeserializeObject<List<MTC_NhienLieuInMay>>(item.NhienLieuPhu);
//                    if (ChiTietNLPhu != null)
//                        NLP.AddRange(ChiTietNLPhu);
//                }
//            }
//            dbString = $"SELECT NL.CodeNhienLieu,may.Code as CodeMayToanDuAn,mayDA.Code as CodeMay,may.Ten as TenMayThiCong,may.CaMayKm, " +
//$"nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt " +
//$" LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} mayDA ON mayDA.Code=nt.CodeMayDuAn " +
//$"LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} may ON may.Code=mayDA.CodeMay "
//+ $"LEFT JOIN {MyConstant.TBL_MTC_NHIENLIEUINMAY} NL ON may.Code=NL.CodeMay " +
//$"WHERE NL.LoaiNhienLieu=1 AND NL.CodeNhienLieu='{lsCodeNhienLieu}'";
//            List<MTC_ChiTietHangNgay> ChiTietMayChinh = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
//            double KLDD = 0;
//            foreach (var item in Tong)
//            {
//                List<MTC_NhienLieuInMay> NLPTH = NLP.Where(x => x.CodeNhienLieu == item.Code).ToList();
//                KLDD += (double)NLPTH.Sum(x => x.MucTieuThu);
//                //List<MTC_ChiTietHangNgay> NL = ChiTietMayChinh.Where(x => x.CodeNhienLieu == item.Code).ToList();
//                if (!ChiTietMayChinh.Any())
//                    continue;
//                KLDD +=(double) ChiTietMayChinh.Sum(x => x.NhienLieuChinh);
//            }
//            //            int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_TongHopNhienLieuHangNgay>("UPDATE_MTC_TongHopNhienLieu",
//            //Tong, false, true, true);
//            //            WaitFormHelper.CloseWaitForm();
//            return KLDD;
//        }
        private void Fcn_LoadThongBao()
        {
            string Dbstring = $"SELECT ROW_NUMBER() OVER(ORDER BY TH.Code) AS STT,TH.* " +
             $"FROM {MyConstant.TBL_MTC_NHIENLIEU} TH  WHERE TH.IsHienThi=1 OR TH.IsHienThi='true'";
            List<MTC_TongHopNhienLieuHangNgay> Tong = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_TongHopNhienLieuHangNgay>(Dbstring);
            Control [] lst = { te_First, te_Second, te_thirst };
            if (Tong.Any())
            {
                int STT = 0;
                foreach(var item in Tong)
                {
                    item.KhoiLuongDaDung =MyFunction.Fcn_UpdateDaTaTongHopNhienLieuPhu(item.Code);
                    item.KhoiLuongDaNhap =MyFunction.Fcn_CalCulateAll(item.Code);
                    lst[STT++].Text = $"{item.Ten}: {item.ConLai}({item.DonVi})";
                }
            }

        }

        private void sb_XuatBaoCao_Click(object sender, EventArgs e)
        {
            XtraFormXuatNhatTrinhMay May = new XtraFormXuatNhatTrinhMay();
            May.ShowDialog();
        }

        private void spSheet_NhatTrinhThiCong_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
        {
            IWorkbook workbook = spSheet_NhatTrinhThiCong.Document;
            Worksheet sheet = workbook.Worksheets[0];
            CellRange RangeData = sheet.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Cell = sheet.SelectedCell;
            string Code = sheet.Rows[Cell.TopRowIndex][Name["Code"]].Value.TextValue;
            string TenMay = sheet.Rows[Cell.TopRowIndex][Name["TenMayThiCong"]].Value.TextValue;
            FormLuaChon luachon = new FormLuaChon(Code, FileManageTypeEnum.NhatTrinhThiCong,TenMay);
            luachon.ShowDialog();
        }
        /// <summary>
        /// KhoiLuongDaNhap
        /// </summary>
        /// <param name="lsCodeNhienLieu"></param>
        /// <returns></returns>
//        private double Fcn_CalCulateAll(string lsCodeNhienLieu)
//        {
//            //WaitFormHelper.ShowWaitForm("Đang Tính toán lại nhiên liệu hằng ngày!!!!!!!!!Vui lòng chờ!!!!!!!!!");
////            string dbString = $"SELECT TH.* " +
////$"FROM {MyConstant.TBL_MTC_NHIENLIEU} TH ";
////            List<MTC_TongHopNhienLieuHangNgay> Tong = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_TongHopNhienLieuHangNgay>(dbString);
//            //string lstCode = string.Join(",", lsCodeNhienLieu);
//            string Dbstring = $"SELECT ROW_NUMBER() OVER(ORDER BY HN.Code) AS STT,HN.* " +
//$"FROM {MyConstant.TBL_MTC_NHIENLIEUHANGNGAY} HN WHERE HN.CodeNhienLieu='{lsCodeNhienLieu}' ORDER BY HN.Ngay ASC";
//            List<MTC_NhienLieuHangNgay> HN = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_NhienLieuHangNgay>(Dbstring);
//            double KLHN =(double) HN.Sum(x => x.KhoiLuong);
////            foreach (var item in Tong)
////            {
////                List<MTC_NhienLieuHangNgay> NL = HN.Where(x => x.Code == item.Code).ToList();
////                if (!NL.Any())
////                    continue;
////                item.KhoiLuongDaNhap = NL.Sum(x => x.KhoiLuong);
////            }
////            int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_TongHopNhienLieuHangNgay>("UPDATE_MTC_TongHopNhienLieu",
////Tong, false, true, true);
////            WaitFormHelper.CloseWaitForm();
//            //Fcn_UpdateDaTaTongHopNhienLieuPhu();
//            //Fcn_LoadThongBao();
//            return KLHN;
//        }
        private void sb_Calculate_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn tính toán lại toàn bộ dữ liệu máy không ?????????");
            if (rs == DialogResult.No)
                return;
            Fcn_LoadThongBao();
        }

        private void ce_LayTheoMay_CheckedChanged(object sender, EventArgs e)
        {
            cbb_LayCongTacNgoaiKeHoach.Enabled = ce_LayTheoMay.Checked;
            LoadData();
        }
    }
}