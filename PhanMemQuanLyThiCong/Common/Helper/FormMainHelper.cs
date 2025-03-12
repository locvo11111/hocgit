using DevExpress.LookAndFeel;
using DevExpress.PivotGrid.SliceQueryDataSource;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class FormMainHelper
    {
        public static void XuatBaoCaoChiPhi_Click()
        {
            var dr = MessageShower.ShowYesNoCancelQuestionWithCustomText("Tùy chọn", "Tùy chọn", "Công tác đã chọn", "Toàn bộ công tác đang hiển thị");
            if (dr == DialogResult.Cancel)
                return;
            bool isOnlyChecked = dr == DialogResult.Yes;
            var wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            var ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
            var range = ws.Range[TDKH.RANGE_KeHoach];
            var dic = MyFunction.fcn_getDicOfColumn(range);
            Dictionary<string, int> ctacs = new Dictionary<string, int>();
            Dictionary<string, int> nhoms = new Dictionary<string, int>();
            for (int i = range.TopRowIndex; i < range.BottomRowIndex; i++)
            {
                var crRow = ws.Rows[i];
                var typeRow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();
                var Code = crRow[dic[TDKH.COL_Code]].Value.ToString();
                var Checked = crRow[TDKH.COL_Chon].Value.ToString() == true.ToString();
                

                if (crRow.Visible && (!isOnlyChecked || Checked))
                {
                    if (typeRow ==  MyConstant.TYPEROW_CVCha)
                    ctacs.Add(Code, i);
                    else if (typeRow == MyConstant.TYPEROW_Nhom) nhoms.Add(Code, i);
                }
            }

            if (!ctacs.Any())
            {
                MessageShower.ShowWarning("Không có công tác nào được chọn!");
                return;
            }

            //if (SharedControls.ce_NgoaiKeHoach.Checked)
            //{
            //    string mess = "Bạn đang ở chế độ lập lại ngày kế hoạch, Bạn có muốn kế hoạch lại toàn bộ công tác đã chọn. KHÔNG THỂ HOÀN TÁC!";
            //    var dire = MessageShower.ShowYesNoQuestion(mess);
            //    if (dire == DialogResult.Yes)
            //    {

            //    }
            //}

            XtraForm_BaoCaoDongTienTDKH form = new XtraForm_BaoCaoDongTienTDKH(ctacs.Keys.ToList(), nhoms.Keys.ToList());
            form.ShowDialog();
        }

        public static void GianNgayCongTac(IEnumerable<string> codes)
        {
            DateTime NgayBatDau = SharedControls.de_Loc_TuNgay.DateTime.Date;
            DateTime NgayKetThuc = SharedControls.de_Loc_DenNgay.DateTime.Date;

            if (NgayBatDau == default || NgayKetThuc == default || NgayBatDau>NgayKetThuc)
            {
                MessageShower.ShowWarning("Vui lòng chọn ngày hợp lệ");
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang lập lại ngày kế hoạch");
            string dbString = $"SELECT cttk.*, MIN(hn.Ngay) AS MinDate, MAX(hn.Ngay) AS MaxDate\r\n" +
                $"FROM {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk\r\n" +
                $"LEFT JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} hn\r\n" +
                $"ON cttk.Code = hn.CodeCongTacTheoGiaiDoan AND hn.KhoiLuongKeHoach IS NOT NULL\r\n" +
                $"WHERE cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(codes)})\r\n" +
                $"GROUP BY cttk.Code";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var wb = SharedControls.spsheet_TD_KH_LapKeHoach;
            wb.BeginUpdate();
            foreach (DataRow dtr in dt.Rows)
            {
                string code = dtr["Code"].ToString();
                //var crRow = ws.Rows[ctacs[code]];
                DateTime NBD = DateTime.Parse(dtr["NgayBatDau"].ToString());
                DateTime NKT = DateTime.Parse(dtr["NgayKetThuc"].ToString());

                var isHasmindate = DateTime.TryParse(dtr["MinDate"].ToString(), out DateTime MinDatePlanned);
                var isHasmaxdate = DateTime.TryParse(dtr["MaxDate"].ToString(), out DateTime MaxDatePlanned);
                if (isHasmindate && NBD != MinDatePlanned)
                {

                    dtr["NgayBatDau"] = MinDatePlanned.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    //crRow[dic[TDKH.COL_NgayBatDau]].SetValue(minDateSelected);

                }

                if (isHasmindate && NKT != MaxDatePlanned)
                {

                    dtr["NgayKetThuc"] = MaxDatePlanned.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    //crRow[dic[TDKH.COL_NgayKetThuc]].SetValue(maxDateSelected);


                }
            }
            wb.EndUpdate();

            dt.Columns.Remove("MinDate");
            dt.Columns.Remove("MaxDate");

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan);
            WaitFormHelper.CloseWaitForm();
        }
        
        public static void GianNgayNhom(IEnumerable<string> codes)
        {
            WaitFormHelper.ShowWaitForm("Đang lập lại ngày kế hoạch");
            string dbString = $"SELECT cttk.*, MIN(hn.Ngay) AS MinDate, MAX(hn.Ngay) AS MaxDate\r\n" +
                $"FROM {Server.Tbl_TDKH_NhomCongTac} cttk\r\n" +
                $"LEFT JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} hn\r\n" +
                $"ON cttk.Code = hn.CodeNhom AND hn.KhoiLuongKeHoach IS NOT NULL\r\n" +
                $"WHERE cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(codes)})\r\n" +
                $"GROUP BY cttk.Code";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var wb = SharedControls.spsheet_TD_KH_LapKeHoach;
            wb.BeginUpdate();
            foreach (DataRow dtr in dt.Rows)
            {
                string code = dtr["Code"].ToString();
                //var crRow = ws.Rows[ctacs[code]];
                DateTime NBD = DateTime.Parse(dtr["NgayBatDau"].ToString());
                DateTime NKT = DateTime.Parse(dtr["NgayKetThuc"].ToString());

                var isHasmindate = DateTime.TryParse(dtr["MinDate"].ToString(), out DateTime MinDatePlanned);
                var isHasmaxdate = DateTime.TryParse(dtr["MaxDate"].ToString(), out DateTime MaxDatePlanned);

                if (isHasmindate && NBD != MinDatePlanned)
                {

                        dtr["NgayBatDau"] = MinDatePlanned.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        //crRow[dic[TDKH.COL_NgayBatDau]].SetValue(minDateSelected);
                    
                }

                if (isHasmindate && NKT != MaxDatePlanned)
                {

                        dtr["NgayKetThuc"] = MaxDatePlanned.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        //crRow[dic[TDKH.COL_NgayKetThuc]].SetValue(maxDateSelected);

                    
                }
            }
            wb.EndUpdate();

            dt.Columns.Remove("MinDate");
            dt.Columns.Remove("MaxDate");

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, Server.Tbl_TDKH_NhomCongTac);
            WaitFormHelper.CloseWaitForm();
        }
        
        public static void TinhNgayNhom(bool recalc = false)
        {
            WaitFormHelper.ShowWaitForm("Đang tính lại ngày nhóm công tác");

            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            Worksheet ws = wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi];
            CellRange range = ws.Range[TDKH.RANGE_KeHoach];

            List<string> codesNhom = new List<string>();
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
            {
                var crRow = ws.Rows[i];
                var typeRow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();
                var ischecked = crRow[TDKH.COL_Chon].Value.ToString() == true.ToString();

                if (!crRow.Visible || typeRow != MyConstant.TYPEROW_Nhom || !ischecked)
                    continue;

                var code = crRow[dic[TDKH.COL_Code]].Value.ToString();

                codesNhom.Add(code);
            }

            if (!codesNhom.Any())
            {
                MessageShower.ShowError("Không có nhóm công tác nào được chọn");
                WaitFormHelper.CloseWaitForm();

                return;
            }

            string dbString = $"UPDATE Tbl_TDKH_NhomCongTac\r\n" +
                           $"SET (NgayBatDau, NgayKetThuc) = " +
                           $"(SELECT MIN(NgayBatDau) AS NgayBatDau, MAX(NgayKetThuc) AS NgayKetThuc FROM {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} WHERE CodeNhom= Tbl_TDKH_NhomCongTac.Code GROUP BY CodeNhom)\r\n" +
                           $"WHERE {Server.Tbl_TDKH_NhomCongTac}.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesNhom)})";

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            //DoBocChuanHelper.ReCalcCongTacHaoPhiInNhoms(codesNhom);
            WaitFormHelper.CloseWaitForm();
            TDKHHelper.LoadCongKinhPhiTienDo();
        }

        public static void fcn_Handle_Popup_TDKH_LayNgayNhom(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Ngày kế hoạch của nhóm sẽ được cập nhật theo công tác và không thể hoàn tác");
            if (dr != DialogResult.Yes)
            {
                return;
            }
            TinhNgayNhom();


        }
        
        public static void fcn_Handle_Popup_TDKH_LayNgayNhomWithCalc(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Nhóm và công tác sẽ được tính lại theo nguyên tác nội suy và KHÔNG THỂ HOÀN TÁC");
            if (dr != DialogResult.Yes)
            {
                return;
            }
            TinhNgayNhom(true);


        }


/*        public static void spsheet_TD_KH_LapKeHoach_CustomDrawCell(object sender, CustomDrawCellEventArgs e)
        {

            string wsName = e.Cell.Worksheet.Name;
            //CellRange range = e.Cell.Worksheet.Workbook.Range[TDKH_DBC.RANGE_DoBocChuan];
            IWorkbook wb = e.Cell.Worksheet.Workbook;
            Worksheet ws = e.Cell.Worksheet;
            if (!wb.DefinedNames.Contains(TDKH.RANGE_DoBocChuan))
                return;
            wb.BeginUpdate();
            //string loaiHang = e.Cell.Worksheet.Rows[e.Cell.RowIndex][dic[GiaoViec.COL_LOAIHANG].Value.ToString();
            Dictionary<string, string> dicDB = MyFunction.fcn_getDicOfColumn(wb.Range[TDKH.RANGE_DoBocChuan]);


            if (e.Cell.Value.ErrorValue?.Type == ErrorType.Value
                || e.Cell.Value.ErrorValue?.Type == ErrorType.DivisionByZero)
            {
                e.Text = "";
                e.DrawDefault();
                e.Handled = true;
                goto Return;
            }

            string typeRow = ws.Rows[e.Cell.RowIndex][dicDB[TDKH.COL_TypeRow]].Value.ToString();
            if (((e.Cell.ColumnIndex == 0
                || ((e.Cell.ColumnIndex == ws.Range.GetColumnIndexByName(dicDB[TDKH.COL_PhanTichVatTu]) || e.Cell.ColumnIndex == ws.Range.GetColumnIndexByName(dicDB[TDKH.COL_IsUseMTC]) || e.Cell.ColumnIndex == ws.Range.GetColumnIndexByName(dicDB[TDKH.COL_HasHopDongAB])) && (typeRow == MyConstant.TYPEROW_CVCha || typeRow == MyConstant.TYPEROW_CVCHIA) && wb.Range[TDKH.RANGE_DoBocChuan].Contains(e.Cell))
                || (ws.Name == TDKH.SheetName_KeHoachKinhPhi && e.Cell.ColumnIndex == 0 && wb.Range[TDKH.RANGE_KeHoach].Contains(e.Cell))
                )))
            {
                if (e.Cell.RowIndex < 4)
                    goto Return;
                e.Text = "";
                int x_center = (e.Bounds.Left + e.Bounds.Right) / 2;
                int y_center = (e.Bounds.Bottom + e.Bounds.Top) / 2;
                Rectangle rec = new Rectangle(x_center - 25, y_center - 10, 50, 20);
                e.Cache.Paint.DrawCheckBox(e.Cache.Graphics, rec, (e.Cell.Value == true) ? ButtonState.Checked : ButtonState.Normal);

            }

            int type = Array.IndexOf(TDKH.sheetsName, ws.Name);
            if (type < 1)
                goto Return;
            //string typeRow = ws.Rows[e.Cell.RowIndex][dic[TDKH.COL_TypeRow]].Value.ToString();


            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            CellRange range = ws.Range[TDKH.rangesNameData[type]];
            if (!range.Contains(e.Cell))
            {
                goto Return;
            }

            Return:
            wb.EndUpdate();

        }

*/

        public static void fcn_Handle_Popup_TDKH_LayNgayCongTacTheoNhom(object sender, EventArgs args)
        {
            var dr = MessageShower.ShowYesNoQuestion("Ngày kế hoạch của công tác sẽ được cập nhật theo nhóm và không thể hoàn tác");
            if (dr != DialogResult.Yes)
            {
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang tính lại ngày nhóm công tác");

            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            Worksheet ws = wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi];
            CellRange range = ws.Range[TDKH.RANGE_KeHoach];

            List<string> codesNhom = new List<string>();
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
            {
                var crRow = ws.Rows[i];
                var typeRow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();
                var ischecked = crRow[TDKH.COL_Chon].Value.ToString() == true.ToString();

                if (!crRow.Visible || typeRow != MyConstant.TYPEROW_Nhom || !ischecked)
                    continue;

                var code = crRow[dic[TDKH.COL_Code]].Value.ToString();

                codesNhom.Add(code);
            }

            if (!codesNhom.Any())
            {
                MessageShower.ShowError("Không có nhóm công tác nào được chọn");
                WaitFormHelper.CloseWaitForm();
                return;
            }

            string dbString = $"UPDATE {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan}\r\n" +
                $"SET (NgayBatDau, NgayKetThuc) = \r\n" +
                $"(SELECT NgayBatDau, NgayKetThuc FROM {Server.Tbl_TDKH_NhomCongTac} nhom WHERE nhom.Code = {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan}.CodeNhom)\r\n" +
                $"WHERE {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan}.CodeNhom IN ({MyFunction.fcn_Array2listQueryCondition(codesNhom)})";

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            WaitFormHelper.CloseWaitForm();
            TDKHHelper.LoadCongKinhPhiTienDo();
        }

        public static void LoadInputRePlan()
        {

            if (SharedControls.ce_RePlan.Checked)
            {
                SharedControls.ce_GianNgay.Checked = false;
                SharedControls.de_Loc_TuNgay.EditValue = DateTime.Now.Date;
                IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
                wb.BeginUpdate();
                Worksheet ws = wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi];

                var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
                var range = ws.Range[TDKH.RANGE_KeHoach];

                for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
                {
                    var typerow = ws.Rows[i][dic[TDKH.COL_TypeRow]].Value.ToString();
                    if (typerow == MyConstant.TYPEROW_CVCha || typerow == MyConstant.TYPEROW_Nhom)
                    {
                        ws.Rows[i][dic[TDKH.COL_KhoiLuongNhapVao]].Formula =
                            $"{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{i + 1}-{dic[TDKH.COL_KhoiLuongDaThiCong]}{i + 1}";
                    }
                }
                wb.EndUpdate();
            }
            else
            {
                SharedControls.ce_GianNgay.Enabled = true;

            }
            
        }
    }
}
