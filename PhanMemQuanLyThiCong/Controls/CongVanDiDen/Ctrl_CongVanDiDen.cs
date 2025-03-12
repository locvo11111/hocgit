using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Commands;
using DevExpress.XtraSpreadsheet.Menu;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.CongVan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls.CongVanDiDen
{
    public partial class Ctrl_CongVanDiDen : DevExpress.XtraEditors.XtraUserControl
    {
        Ctrl_DonViPhatHanh DVPH = new Ctrl_DonViPhatHanh();
        Ctrl_DuAnCongVan DUANCV = new Ctrl_DuAnCongVan();
        public string m_path,m_PathHoSo;
        public Ctrl_CongVanDiDen()
        {
            InitializeComponent();
            DVPH.de_TranDanhSachDVPH= new Ctrl_DonViPhatHanh.DE_TransDonViPH(fcn_DE_DanhSachNV);
            DVPH.Dock = DockStyle.None;
            this.Controls.Add(DVPH);
            DVPH.Hide();

            DUANCV.de_TranDanhSachDVPH= new Ctrl_DuAnCongVan.DE_TransDonViPH(fcn_DE_DanhSachNVDA);
            DUANCV.Dock = DockStyle.None;
            this.Controls.Add(DUANCV);
            DUANCV.Hide();
        }
        private void fcn_DE_DanhSachNV(List<CongVanDiDenModel> DanhSachNV, string CodeCV)
        {
            CongVanDiDenModel item = DanhSachNV.SingleOrDefault();
            if (item is null)
                goto Label;
            IWorkbook workbook = spsheet_CongVanDiDen.Document;
            Worksheet sheet = workbook.Worksheets[0];
            CellRange RangeData = sheet.Range["RangeData"];
            CellRange cell = sheet.SelectedCell;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            string db_string = $"UPDATE  {MyConstant.QL_CVDD} SET" +
            $" \"CodeDonViPhatHanh\"='{item.Code}' WHERE \"Code\"='{CodeCV}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            sheet.Rows[cell.TopRowIndex][Name[MyConstant.COL_CONGVAN_DonViPhatHanh]].SetValueFromText(item.DonViPhatHanh);
            sheet.Rows[cell.TopRowIndex][Name[MyConstant.COL_CONGVAN_Kieu]].SetValueFromText(item.Kieu);
            sheet.Rows[cell.TopRowIndex][Name[MyConstant.COL_CONGVAN_CodeDVPH]].SetValueFromText(item.Code);
            Label:
            spsheet_CongVanDiDen.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Cancel);
        }      
        private void fcn_DE_DanhSachNVDA(List<Tbl_ThongTinDuAnViewModel> DanhSachNV, string CodeCV)
        {
            if (!DanhSachNV.Any())
                return;
            Tbl_ThongTinDuAnViewModel item = DanhSachNV.SingleOrDefault();
            if (item is null)
                return;
            IWorkbook workbook = spsheet_CongVanDiDen.Document;
            Worksheet sheet = workbook.Worksheets[0];
            CellRange RangeData = sheet.Range["RangeData"];
            CellRange cell = sheet.SelectedCell;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            string db_string = $"UPDATE  {MyConstant.QL_CVDD} SET" +
            $" \"CodeDuAn\"='{item.Code}' WHERE \"Code\"='{CodeCV}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            sheet.Rows[cell.TopRowIndex][Name[MyConstant.COL_CONGVAN_DuAn]].SetValueFromText(item.TenDuAn);
            sheet.Rows[cell.TopRowIndex][Name[MyConstant.COL_CONGVAN_CodeDA]].SetValueFromText(item.Code);
            spsheet_CongVanDiDen.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Cancel);
        }
        public void InitData()
        {
            FileHelper.fcn_spSheetStreamDocument(spsheet_CongVanDiDen, $@"{BaseFrom.m_templatePath}\FileExcel\11.QLCongVanDiDen.xls"); // Công văn tài liệu đi đến
            string dbString = $"SELECT DVPH.*,DVPH.Code as CodeDVPH,CV.*,DA.TenDuAn as DuAn FROM {MyConstant.QL_CVDD} CV" +
                $" LEFT JOIN {MyConstant.TBL_THONGTINDUAN} DA ON DA.Code=CV.CodeDuAn" +
                $" LEFT JOIN {MyConstant.QL_CVDD_DVPH} DVPH ON DVPH.Code=CV.CodeDonViPhatHanh";
            List<CongVanDiDenModel> CVDD= DataProvider.InstanceTHDA.ExecuteQueryModel<CongVanDiDenModel>(dbString);
            m_path = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{MyConstant.QL_CVDD}");
            if (!Directory.Exists(m_path))
                Directory.CreateDirectory(m_path);
            IWorkbook workbook = spsheet_CongVanDiDen.Document;
            Worksheet sheet = workbook.Worksheets[0];
            CellRange RangeData = sheet.Range["RangeData"];
            spsheet_CongVanDiDen.BeginUpdate();
            RangeData.ClearContents();
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            if (CVDD.Count() >= RangeData.RowCount - 1)
                sheet.Rows.Insert(RangeData.BottomRowIndex - 1, RangeData.RowCount - CVDD.Count(), RowFormatMode.FormatAsPrevious);
            long STT = 1;
            int RowIndex = RangeData.TopRowIndex;
            foreach(var item in CVDD)
            {
                Row Crow = sheet.Rows[RowIndex++];
                Crow[Name[MyConstant.STT]].SetValue(STT++);
                Crow[Name[MyConstant.COL_CONGVAN_MaHoSo]].SetValueFromText(item.MaHoSo);
                Crow[Name[MyConstant.COL_CONGVAN_CodeDA]].SetValueFromText(item.CodeDuAn);
                Crow[Name[MyConstant.COL_CONGVAN_Code]].SetValueFromText(item.Code);
                Crow[Name[MyConstant.COL_CONGVAN_CodeDVPH]].SetValueFromText(item.CodeDVPH);
                Crow[Name[MyConstant.COL_CONGVAN_TenHoSo]].SetValueFromText(item.TenHoSo);
                Crow[Name[MyConstant.COL_CONGVAN_NguoiKy]].SetValueFromText(item.NguoiKy);
                Crow[Name[MyConstant.COL_CONGVAN_DonViPhatHanh]].SetValueFromText(item.DonViPhatHanh);
                Crow[Name[MyConstant.COL_CONGVAN_Kieu]].SetValueFromText(item.Kieu);
                Crow[Name[MyConstant.COL_CONGVAN_NgayNhan]].SetValueFromText(item.NgayNhan.HasValue?item.NgayNhan.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE):"");
                Crow[Name[MyConstant.COL_CONGVAN_NgayMuon]].SetValueFromText(item.NgayMuon.HasValue?item.NgayMuon.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE):"");
                Crow[Name[MyConstant.COL_CONGVAN_LoaiCongVan]].SetValueFromText(item.LoaiCongVan);
                Crow[Name[MyConstant.COL_CONGVAN_LyDoMuon]].SetValueFromText(item.LyDoMuon);
                Crow[Name[MyConstant.COL_CONGVAN_DaMuon]].SetValueFromText(item.DaMuon);
                Crow[Name[MyConstant.COL_CONGVAN_DuAn]].SetValueFromText(item.DuAn);
                Crow[Name[MyConstant.COL_CONGVAN_TrangThai]].SetValueFromText(item.TrangThai);
                Crow[Name[MyConstant.COL_CONGVAN_SoanThao]].SetValueFromText(item.SoanThao);
                Crow[Name[MyConstant.COL_CONGVAN_GhiChu]].SetValueFromText(item.GhiChu);
                sheet.Hyperlinks.Add(Crow[Name[MyConstant.COL_CONGVAN_FILEDINHKEM]],Crow[Name[MyConstant.COL_CONGVAN_FILEDINHKEM]].GetReferenceA1(), false, item.FileDinhKem);
                sheet.Hyperlinks.Add(Crow[Name[MyConstant.COL_CONGVAN_DUONGDAN]],
                    Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{MyConstant.QL_CVDD}/{STT-1}.{item.MaHoSo}_{item.TenHoSo}"), true,"Đường dẫn");
            }

            dbString = $"SELECT * FROM {MyConstant.QL_CVDD_DVPH}";
            List<CongVanDiDenModel> DVPH = DataProvider.InstanceTHDA.ExecuteQueryModel<CongVanDiDenModel>(dbString);
            Worksheet sheetDHPH = workbook.Worksheets[1];
            CellRange RangeDataDVPH = sheetDHPH.Range["RangeData"];
            Dictionary<string, string> NameDVPH = MyFunction.fcn_getDicOfColumn(RangeDataDVPH);
            if (DVPH.Count() >= RangeDataDVPH.RowCount - 1)
                sheetDHPH.Rows.Insert(RangeDataDVPH.BottomRowIndex - 1, RangeDataDVPH.RowCount - DVPH.Count(), RowFormatMode.FormatAsPrevious);
            RangeDataDVPH.ClearContents();
            STT = 1;
            RowIndex = RangeDataDVPH.TopRowIndex;
            foreach (var item in DVPH)
            {
                Row Crow = sheetDHPH.Rows[RowIndex++];
                Crow[NameDVPH[MyConstant.COL_CONGVAN_Code]].SetValueFromText(item.Code);
                Crow[NameDVPH[MyConstant.STT]].SetValue(STT++);
                Crow[NameDVPH[MyConstant.COL_CONGVAN_DonViPhatHanh]].SetValueFromText(item.DonViPhatHanh);
                Crow[NameDVPH[MyConstant.COL_CONGVAN_Kieu]].SetValueFromText(item.Kieu);
                Crow[NameDVPH[MyConstant.COL_CONGVAN_GhiChu]].SetValueFromText(item.GhiChu);
            }
            spsheet_CongVanDiDen.EndUpdate();
        }
        private void spsheet_CongVanDiDen_CellBeginEdit(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellCancelEventArgs e)
        {
            Worksheet sheet = e.Worksheet;
            CellRange RangeData = sheet.Range["RangeData"];
            if (!RangeData.Contains(e.Cell))
            {
                e.Cancel = true;
                return;
            }
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            if (Name[MyConstant.STT] == sheet.Columns[e.ColumnIndex].Heading)
                e.Cancel = true;
            string Code = sheet.Rows[e.RowIndex][Name[MyConstant.CODE]].Value.TextValue;
            switch (sheet.Name)
            {
                case MyConstant.SHEETBAOCAO:
                    if (Name[MyConstant.COL_CONGVAN_MaHoSo] != sheet.Columns[e.ColumnIndex].Heading && Name[MyConstant.COL_CONGVAN_TenHoSo] != sheet.Columns[e.ColumnIndex].Heading)
                    {
                        if (Code is null)
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (Name[MyConstant.COL_CONGVAN_DonViPhatHanh] == sheet.Columns[e.ColumnIndex].Heading || Name[MyConstant.COL_CONGVAN_Kieu] == sheet.Columns[e.ColumnIndex].Heading)
                        {
                            string CodeDVPH = sheet.Rows[e.RowIndex][Name["CodeDVPH"]].Value.TextValue;
                            string dbString = $"SELECT * FROM {MyConstant.QL_CVDD_DVPH}";
                            List<CongVanDiDenModel> DVPHModel = DataProvider.InstanceTHDA.ExecuteQueryModel<CongVanDiDenModel>(dbString);
                            DVPH.Show();
                            var spsheetLoc = spsheet_CongVanDiDen.Location;
                            Rectangle rec = spsheet_CongVanDiDen.GetCellBounds(e.RowIndex, e.ColumnIndex);
                            DVPH.Location = new Point(rec.Right + spsheetLoc.X, rec.Top + spsheetLoc.Y);
                            DVPH.BringToFront();
                            DVPH.Fcn_UpDateDanhSachDVPH(DVPHModel, Code,CodeDVPH);
                        }     
                        else if (Name[MyConstant.COL_CONGVAN_DuAn] == sheet.Columns[e.ColumnIndex].Heading)
                        {
                            string CodeDVPH = sheet.Rows[e.RowIndex][Name["CodeDuAn"]].Value.TextValue;
                            DUANCV.Show();
                            var spsheetLoc = spsheet_CongVanDiDen.Location;
                            Rectangle rec = spsheet_CongVanDiDen.GetCellBounds(e.RowIndex, e.ColumnIndex);
                            DUANCV.Location = new Point(rec.Right + spsheetLoc.X, rec.Top + spsheetLoc.Y);
                            DUANCV.BringToFront();
                            DUANCV.Fcn_UpDateDanhSachDVPH(Code,CodeDVPH);
                        }
                    }
                    break;
                default:
                    //string dbString = $"INSERT OR REPLACE INTO '{MyConstant.QL_CVDD_DVPH}' (\"Code\",{FieldName}) VALUES " +
                    //$"('{Code}','{e.Value}')";
                    //DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    break;

            }
        }

        private void spsheet_CongVanDiDen_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {
            Worksheet sheet = e.Worksheet;
            CellRange RangeData = sheet.Range["RangeData"];
            if (!RangeData.Contains(e.Cell))
            {
                spsheet_CongVanDiDen.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Cancel);
                return;
            }
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            string Code = sheet.Rows[e.RowIndex][Name[MyConstant.CODE]].Value.TextValue;
            bool UpDate = Code is null ? false : true;
            Code = Code ?? Guid.NewGuid().ToString();
            double STT= sheet.Rows[e.RowIndex][Name[MyConstant. STT]].Value.NumericValue;
            string FieldName= sheet.Rows[0][e.ColumnIndex].Value.TextValue;
            if (e.RowIndex == RangeData.BottomRowIndex - 1)
                sheet.Rows.Insert(e.RowIndex+1, 1, RowFormatMode.FormatAsPrevious);
            sheet.Rows[e.RowIndex + 1].Visible = true;
            string MHS = "", THS = "", dbString = "", TenThuMuc = "", TenThuMucCu = "", PathCu = "";
            switch (sheet.Name)
            {
                case MyConstant.SHEETBAOCAO:
                    STT = STT.ToString() == "0" ? (double)e.RowIndex - 2 : STT;
                    if (!UpDate)
                    {
                        sheet.Rows[e.RowIndex][Name[MyConstant.STT]].SetValue(STT);
                        sheet.Rows[e.RowIndex][Name[MyConstant.CODE]].SetValue(Code);
                        sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_DuAn]].SetValue(SharedControls.slke_ThongTinDuAn.Text);
                    }
                    if (Name[MyConstant.COL_CONGVAN_MaHoSo] == sheet.Columns[e.ColumnIndex].Heading)
                    {
                        THS = sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_TenHoSo]].Value.TextValue;
                        THS = THS ?? $"Hồ sơ {STT}";
                        sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_TenHoSo]].SetValue(THS);
                        dbString = !UpDate? $"INSERT  INTO {MyConstant.QL_CVDD} (\"Code\",{FieldName},\"TenHoSo\",\"CodeDuAn\") VALUES " +
                        $"('{Code}','{e.Value}','{THS}','{SharedControls.slke_ThongTinDuAn.EditValue}')" :$"UPDATE {MyConstant.QL_CVDD} SET {FieldName}='{e.Value}',\"TenHoSo\"='{THS}' " +
                        $" WHERE \"Code\" = '{Code}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                        TenThuMuc = $"{STT}.{e.Value}_{THS}";
                        m_PathHoSo = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{MyConstant.QL_CVDD}/{TenThuMuc}");

                        TenThuMucCu = $"{STT}.{e.OldValue}_{THS}";
                        PathCu = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{MyConstant.QL_CVDD}/{TenThuMucCu}");

                        if (Directory.Exists(PathCu))
                        {
                            Directory.Move(PathCu, m_PathHoSo);
                        }
                        else if(!Directory.Exists(m_PathHoSo))
                            Directory.CreateDirectory(m_PathHoSo);
                        sheet.Hyperlinks.Add(sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_FILEDINHKEM]], sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_FILEDINHKEM]].GetReferenceA1(), false,"File đính kèm");
                        sheet.Hyperlinks.Add(sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_DUONGDAN]],m_PathHoSo, true, "Đường dẫn");
                    }
                    else if( Name[MyConstant.COL_CONGVAN_TenHoSo] == sheet.Columns[e.ColumnIndex].Heading )
                    {
                        MHS = sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_MaHoSo]].Value.TextValue;
                        MHS = MHS ?? $"MHS{STT}";
                        sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_MaHoSo]].SetValue(MHS);
                        dbString = !UpDate ? $"INSERT INTO {MyConstant.QL_CVDD} (\"Code\",{FieldName},\"MaHoSo\",\"CodeDuAn\") VALUES " +
                        $"('{Code}','{e.Value}','{MHS}','{SharedControls.slke_ThongTinDuAn.EditValue}')" : $"UPDATE {MyConstant.QL_CVDD} SET {FieldName}='{e.Value}',\"MaHoSo\"='{MHS}' " +
                        $" WHERE \"Code\" = '{Code}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                        TenThuMuc = $"{STT}.{MHS}_{e.Value}";
                        m_PathHoSo = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{MyConstant.QL_CVDD}/{TenThuMuc}");

                        TenThuMucCu = $"{STT}.{MHS}_{e.OldValue}";
                        PathCu = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{MyConstant.QL_CVDD}/{TenThuMucCu}");

                        if (Directory.Exists(PathCu))
                        {
                            Directory.Move(PathCu, m_PathHoSo);
                        }
                        else if (!Directory.Exists(m_PathHoSo))
                            Directory.CreateDirectory(m_PathHoSo);
                        sheet.Hyperlinks.Add(sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_FILEDINHKEM]], sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_FILEDINHKEM]].GetReferenceA1(), false, "File đính kèm");
                        sheet.Hyperlinks.Add(sheet.Rows[e.RowIndex][Name[MyConstant.COL_CONGVAN_DUONGDAN]], m_PathHoSo, true, "Đường dẫn");
                    }
                    else if(Name[MyConstant.COL_CONGVAN_NgayMuon] == sheet.Columns[e.ColumnIndex].Heading|| Name[MyConstant.COL_CONGVAN_NgayNhan] == sheet.Columns[e.ColumnIndex].Heading)
                    {
                        bool TryParse = DateTime.TryParse(e.Value.ToString(),out DateTime Out);
                        if (!TryParse)
                        {
                            MessageShower.ShowError("Vui lòng nhập đúng định dạng dd//mm/yyyy!!!!!!");
                            spsheet_CongVanDiDen.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Cancel);
                            return;
                        }
                        dbString = $"UPDATE {MyConstant.QL_CVDD} SET " +
                        $"{FieldName} = '{Out.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                        $" WHERE \"Code\" = '{Code}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                    }
                    else if (Name[MyConstant.COL_CONGVAN_DonViPhatHanh] == sheet.Columns[e.ColumnIndex].Heading
                        || Name[MyConstant.COL_CONGVAN_Kieu] == sheet.Columns[e.ColumnIndex].Heading ||
                        Name[MyConstant.COL_CONGVAN_DUONGDAN] == sheet.Columns[e.ColumnIndex].Heading||
                        Name[MyConstant.COL_CONGVAN_FILEDINHKEM] == sheet.Columns[e.ColumnIndex].Heading)
                    {
                        spsheet_CongVanDiDen.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Cancel);
                        return;
                    }
                    else
                    {
                        try
                        {
                            dbString = $"UPDATE {MyConstant.QL_CVDD} SET " +
                            $"{FieldName} = '{e.Value}' " +
                            $" WHERE \"Code\" = '{Code}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        }
                        catch(Exception ex)
                        {

                        }
                    }

                    break;
                default:
                    STT = STT.ToString() == "0" ? (double)e.RowIndex - 3 : STT;
                    sheet.Rows[e.RowIndex][Name[MyConstant.STT]].SetValue(STT);
                    sheet.Rows[e.RowIndex][Name[MyConstant.CODE]].SetValue(Code);
                    dbString =!UpDate? $"INSERT INTO {MyConstant.QL_CVDD_DVPH} (\"Code\",{FieldName}) VALUES " +
                    $"('{Code}','{e.Value}')": $"UPDATE {MyConstant.QL_CVDD_DVPH} SET {FieldName}='{e.Value}' " +
                        $" WHERE \"Code\" = '{Code}'"; ;
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    break;

            }
        }

        private void spsheet_CongVanDiDen_PopupMenuShowing(object sender, DevExpress.XtraSpreadsheet.PopupMenuShowingEventArgs e)
        {
            e.Menu.RemoveMenuItem(SpreadsheetCommandId.RemoveSheetRowsContextMenuItem);
            e.Menu.RemoveMenuItem(SpreadsheetCommandId.RemoveSheetRows);
            IWorkbook wb = spsheet_CongVanDiDen.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["RangeData"];
            CellRange Cell = ws.SelectedCell;
            if (!RangeData.Contains(Cell) || Cell.RowCount > 1)
                return;
            SpreadsheetMenuItem itemCapNhatAll = new SpreadsheetMenuItem("Xóa công văn", fcn_Handle_XoaCV);
            itemCapNhatAll.Appearance.ForeColor = Color.Blue;
            itemCapNhatAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
            e.Menu.Items.Add(itemCapNhatAll);
        }
        private void fcn_Handle_XoaCV(object sender, EventArgs args)
        {
            IWorkbook wb = spsheet_CongVanDiDen.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["RangeData"];
            CellRange Cell = ws.SelectedCell;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            string Code = ws.Rows[Cell.TopRowIndex][Name[MyConstant.CODE]].Value.TextValue;
            if (string.IsNullOrEmpty(Code))
            {
                MessageShower.ShowError("Vui lòng chọn dòng có công văn để xóa!!!");
                return;
            }
            DialogResult rs=MessageShower.ShowYesNoQuestion("Bạn có muốn xóa công văn này?????");
            if (rs == DialogResult.No)
                return;
            string dbString = $"DELETE  FROM {MyConstant.QL_CVDD} WHERE \"Code\" = '{Code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            string MHS = ws.Rows[Cell.TopRowIndex][Name[MyConstant.COL_CONGVAN_MaHoSo]].Value.TextValue;
            string THS = ws.Rows[Cell.TopRowIndex][Name[MyConstant.COL_CONGVAN_TenHoSo]].Value.TextValue;
            double STT = ws.Rows[Cell.TopRowIndex][Name[MyConstant.STT]].Value.NumericValue;
            string TenThuMuc = $"{STT}.{MHS}_{THS}";
            string PathCu = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{MyConstant.QL_CVDD}/{TenThuMuc}");
            if (Directory.Exists(PathCu))
            {
                Directory.Delete(PathCu, true);
            }
            ws.Rows[Cell.TopRowIndex].Delete();
        }
        private void spsheet_CongVanDiDen_HyperlinkClick(object sender, DevExpress.XtraSpreadsheet.HyperlinkClickEventArgs e)
        {
            Worksheet ws = spsheet_CongVanDiDen.Document.Worksheets[0];
            CellRange range = e.TargetRange;
            if (range is null)
                return;
            CellRange RangeData = ws.Range["RangeData"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            string codeCV = ws.Rows[range.TopRowIndex][Name[MyConstant.COL_CONGVAN_Code]].Value.TextValue;
            if(Name[MyConstant.COL_CONGVAN_FILEDINHKEM] == ws.Columns[range.RightColumnIndex].Heading)
            {
                FormLuaChon form = new FormLuaChon(codeCV, FileManageTypeEnum.CONGVANDIDEN);
                form.ShowDialog();
            }
        }
    }
}
