using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using DevExpress.Utils.Extensions;
using PhanMemQuanLyThiCong;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Constant;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_LuachonChiPhi_GiaChi : Form
    {
        string m_codeduan;
        public Form_LuachonChiPhi_GiaChi(string code)
        {
            InitializeComponent();
            m_codeduan = code;
        }
        private void fcn_initFile()
        {
            // Nhập thông tin công trình //
            spread_Thuchitamung_guiduyet.LoadDocument($@"{BaseFrom.m_templatePath}\FileExcel\15.2Thuchitamungkhoanchiguiduyet.xlsx");
            fcn_Importdatabasetoexecl(spread_Thuchitamung_guiduyet, "Tbl_ThuChiTamUng_KhoanChi", m_codeduan);
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {

        }

        private void Form_LuachonChiPhi_GiaChi_Load(object sender, EventArgs e)
        {
            fcn_initFile();
        }

        private void btn_capnhap_Click(object sender, EventArgs e)
        {
            Worksheet ws = spread_Thuchitamung_guiduyet.Document.Worksheets[0];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange rangeXD = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            for (int i = rangeXD.TopRowIndex; i <= rangeXD.BottomRowIndex; i++)
            {
                if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CAPNHAP]].Value.ToString() == "CẬP NHẬP")
                    MyFunction.fcn_update(i, spread_Thuchitamung_guiduyet,m_codeduan);
            }
        }
        private void fcn_Importdatabasetoexecl(DevExpress.XtraSpreadsheet.SpreadsheetControl name, string database, string code)
        {
            IWorkbook workbook = name.Document;
            Worksheet ws = workbook.Worksheets[0];
            Worksheet ws1 = workbook.Worksheets[1];
            CellRange cp = ws1.Range["B1"];
            CellRange gc = ws1.Range["B2"];    
            CellRange capnhap = ws1.Range["A4:B4"];
            CellRange thuchien = ws1.Range["A3:B3"];
            string queryStr = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi} WHERE \"CodeDuAn\"='{code}' ORDER BY \"Sortid\"";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string queryStr1 = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_cp} WHERE \"CodeDuAn\"='{code}'";
            DataTable dt1 = DataProvider.InstanceTHDA.ExecuteQuery(queryStr1);
            string queryStr2 = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_gc} WHERE \"CodeDuAn\"='{code}'";
            DataTable dt2 = DataProvider.InstanceTHDA.ExecuteQuery(queryStr2);
            CellRange rangeXD = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            Dictionary<string, string> DIC_KHOANCHI = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);
            if (dt.Rows.Count + dt1.Rows.Count + dt2.Rows.Count >= rangeXD.RowCount - 1)
                ws.Rows.Insert(rangeXD.BottomRowIndex, dt.Rows.Count + dt1.Rows.Count + dt2.Rows.Count - rangeXD.RowCount + 2, RowFormatMode.FormatAsPrevious);
            int stt_cha = 1;
            int j = 0;
            int k = 1;
            int m = 0;
            name.BeginUpdate();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string queryStr_cp = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_cp} WHERE \"CodeDuAn\"='{code}' AND \"CodeKhoanChi\"='{dt.Rows[i]["Code"].ToString()}'ORDER BY \"Sortid\"";
                DataTable dt_cp = DataProvider.InstanceTHDA.ExecuteQuery(queryStr_cp);
                string queryStr_gc = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_gc} WHERE \"CodeDuAn\"='{code}' AND \"CodeKhoanChi\"='{dt.Rows[i]["Code"].ToString()}'ORDER BY \"Sortid\"";
                DataTable dtgc = DataProvider.InstanceTHDA.ExecuteQuery(queryStr_gc);
                DataRow rowCon = dt.Rows[i];
                m = 0;
                foreach (var item in DIC_KHOANCHI)
                {
                    if (dt.Columns.Contains(item.Key))
                    {
                        ws.Rows[rangeXD.TopRowIndex + j][item.Value].SetValueFromText(rowCon[item.Key].ToString(), true);
                        ws.Rows[rangeXD.TopRowIndex + j][DIC_KHOANCHI["Stt"]].SetValueFromText($"{stt_cha}", true);
                    }
                }
                if (dt_cp.Rows.Count == 0 && dtgc.Rows.Count == 0)
                    ws.Range[DIC_KHOANCHI[ThuChiTamUng.COL_THUCHI_CAPNHAP]+(rangeXD.TopRowIndex + j+1).ToString()+":"+ DIC_KHOANCHI[ThuChiTamUng.COL_THUCHI_THUCHIEN] + (rangeXD.TopRowIndex + 1+j).ToString()].CopyFrom(thuchien, PasteSpecial.All);
                else
                    ws.Range[DIC_KHOANCHI[ThuChiTamUng.COL_THUCHI_CAPNHAP] + (rangeXD.TopRowIndex + j+1).ToString() + ":" + DIC_KHOANCHI[ThuChiTamUng.COL_THUCHI_THUCHIEN] + (rangeXD.TopRowIndex +1+ j).ToString()].CopyFrom(capnhap, PasteSpecial.All);
                foreach (DataRow dataRow in dt_cp.Rows)
                {
                    foreach (var item in DIC_KHOANCHI)
                    {
                        if (dt_cp.Columns.Contains(item.Key))
                        {
                            ws.Rows[rangeXD.TopRowIndex + j + 1 + m][item.Value].SetValueFromText(dataRow[item.Key].ToString(), true);
                            ws.Rows[rangeXD.TopRowIndex + j + 1 + m][DIC_KHOANCHI["Stt"]].SetValueFromText("+", true);
                            ws.Rows[rangeXD.TopRowIndex + j + 1 + m].Font.Color = Color.Red;
                        }
                    }
                    if(m==dt_cp.Rows.Count-1)
                        ws.Range[DIC_KHOANCHI[ThuChiTamUng.COL_THUCHI_THUCHIEN] + (rangeXD.TopRowIndex + j + 2 + m).ToString()].CopyFrom(cp, PasteSpecial.All);   
                    m++;
                }
                m = 0;
                foreach (DataRow dataRow in dtgc.Rows)
                {
                    foreach (var item in DIC_KHOANCHI)
                    {
                        if (dtgc.Columns.Contains(item.Key))
                        {
                            ws.Rows[rangeXD.TopRowIndex + j + 1 + m + dt_cp.Rows.Count][item.Value].SetValueFromText(dataRow[item.Key].ToString(), true);
                            ws.Rows[rangeXD.TopRowIndex + j + 1 + m + dt_cp.Rows.Count].Alignment.Vertical = SpreadsheetVerticalAlignment.Bottom;
                            ws.Rows[rangeXD.TopRowIndex + j + 1 + m + dt_cp.Rows.Count].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                            if (item.Key == "NoiDungChiPhi")
                                ws.Rows[rangeXD.TopRowIndex + j + 1 + m + dt_cp.Rows.Count][item.Value].Alignment.Indent = 2;
                            ws.Rows[rangeXD.TopRowIndex + j + 1 + m + dt_cp.Rows.Count][DIC_KHOANCHI["Stt"]].SetValueFromText($"{stt_cha}.{k}", true);
                            ws.Rows[rangeXD.TopRowIndex + j + 1 + m + dt_cp.Rows.Count].Font.Color = Color.Blue;
                        }
                    }
                    if (m == dtgc.Rows.Count - 1)
                    {
                        ws.Range[DIC_KHOANCHI[ThuChiTamUng.COL_THUCHI_THUCHIEN] + (rangeXD.TopRowIndex + j + 2 + dt_cp.Rows.Count + m).ToString()].CopyFrom(gc, PasteSpecial.All);
                        MyFunction.fcn_calsum(name, rangeXD.TopRowIndex + j + 2 + dt_cp.Rows.Count + m);
                    }                        
                    m++;
                    k++;
                }
                k = 1;
                stt_cha++;
                j = dt_cp.Rows.Count + dtgc.Rows.Count + j + 1;
            }
            name.EndUpdate();
        }

        private void spread_Thuchitamung_guiduyet_Click(object sender, EventArgs e)
        {
            Worksheet ws = spread_Thuchitamung_guiduyet.Document.Worksheets[0];
            Worksheet ws1 = spread_Thuchitamung_guiduyet.Document.Worksheets[1];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange rangeXD = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            if (ws.SelectedCell.BottomRowIndex < 2|| ws.SelectedCell.BottomRowIndex>rangeXD.BottomRowIndex-2)
                return;
            if (ws.Columns[ws.SelectedCell.RightColumnIndex].Heading == Name[ThuChiTamUng.COL_THUCHI_CAPNHAP]|| ws.Columns[ws.SelectedCell.RightColumnIndex].Heading == Name[ThuChiTamUng.COL_THUCHI_THUCHIEN])
            {
                CellRange select = ws1.Range["A1:O2"];
                CellRange target = ws.Range[Name[ThuChiTamUng.COL_THUCHI_THUCHIEN] + (ws.SelectedCell.TopRowIndex + 2).ToString()];
                CellRange themgiaichi = ws1.Range["A2:O2"];
                CellRange themchiphi = ws1.Range["A1:O1"];
                Dictionary<string, CellRange> addgiaichichiphi = new Dictionary<string, CellRange>() { { "THÊM GIẢI CHI", themgiaichi },{ "THÊM CHI PHÍ", themchiphi}, };
                string noiung = ws.SelectedCell.Value.ToString();
                if (ws.SelectedCell.Value.ToString() == "THÊM GIẢI CHI" || ws.SelectedCell.Value.ToString() == "THÊM CHI PHÍ")
                {
                    string noidung = ws.SelectedCell.Value.ToString();
                    spread_Thuchitamung_guiduyet.BeginUpdate();
                    ws.Rows.Insert(ws.SelectedCell.BottomRowIndex + 1, 1, RowFormatMode.None);
                    spread_Thuchitamung_guiduyet.EndUpdate();
                    ws.Range[Name[ThuChiTamUng.COL_THUCHI_THUCHIEN] + (ws.SelectedCell.TopRowIndex + 1).ToString()].MoveTo(target);
                    ws.Range[Name[ThuChiTamUng.COL_THUCHI_CAPNHAP] + (ws.SelectedCell.TopRowIndex + 2).ToString() + ":" + Name[ThuChiTamUng.COL_THUCHI_NOIDUNGCHIPHI] + (ws.SelectedCell.TopRowIndex + 2).ToString()].CopyFrom(addgiaichichiphi[noidung], PasteSpecial.All);
                    //if (noidung == "THÊM GIẢI CHI")
                    //    fcn_calsum();
                }
                switch (noiung)
                {
                    case "THỰC HIỆN":
                        spread_Thuchitamung_guiduyet.BeginUpdate();
                        ws.Rows.Insert(ws.SelectedCell.BottomRowIndex + 1, 2, RowFormatMode.None);
                        spread_Thuchitamung_guiduyet.EndUpdate();
                        ws.Range[Name[ThuChiTamUng.COL_THUCHI_THUCHIEN] + (ws.SelectedCell.TopRowIndex + 1).ToString()].MoveTo(target);
                        ws.Range[Name[ThuChiTamUng.COL_THUCHI_CAPNHAP] + (ws.SelectedCell.TopRowIndex + 2).ToString() + ":" + Name[ThuChiTamUng.COL_THUCHI_NOIDUNGCHIPHI] + (ws.SelectedCell.TopRowIndex + 3).ToString()].CopyFrom(select, PasteSpecial.All);
                        break;
                    case "CẬP NHẬP":
                        if (ws.Rows[ws.SelectedCell.BottomRowIndex][Name[ThuChiTamUng.COL_THUCHI_THUCHIEN]].Value.ToString() == "")
                        {
                            DialogResult rs = MessageShower.ShowYesNoQuestion( "Bạn có muốn cập nhập dữ liệu không,hãy kiểm trả kỹ trước khi đồng ý?", "Cập nhập");
                            if (rs == DialogResult.Yes)
                            {
                                //string codekhoanchi = ws.Rows[ws.SelectedCell.BottomRowIndex][Name[ThuChiTamUng.COL_THUCHI_CODE]].Value.ToString();
                                //string codetamung = ws.Rows[ws.SelectedCell.BottomRowIndex][Name[ThuChiTamUng.COL_THUCHI_CODE]].Value.ToString();
                                //string dbString = $"UPDATE  {MyConstant.Tbl_ThuchiTamUng_Khoanchi} SET \"Duyet\"='{true}' WHERE \"Code\"='{codekhoanchi}'";
                                //DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                                //string queryStr = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_cp} WHERE \"CodeDuAn\"='{m_codeduan}' AND \"CodeKhoanChi\"='{codekhoanchi}'";
                                //DataTable DT_cp = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                                //string queryStr1 = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_gc} WHERE \"CodeDuAn\"='{m_codeduan}' AND \"CodeKhoanChi\"='{codekhoanchi}'";
                                //DataTable DT_gc = DataProvider.InstanceTHDA.ExecuteQuery(queryStr1);
                                //int sortID_cp = DT_cp.Rows.Count;
                                //int sortID_gc = DT_gc.Rows.Count;
                                //DataRow rowdt;
                                MyFunction.fcn_update(ws.SelectedCell.TopRowIndex, spread_Thuchitamung_guiduyet, m_codeduan);
                                //fcn_update(ws.SelectedCell.TopRowIndex);
                                //for (int i = ws.SelectedCell.BottomRowIndex + 1; i <= rangeXD.BottomRowIndex; i++)
                                //{
                                //    Row crRow = ws.Rows[i];
                                //    string code = ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CODE]].Value.ToString();
                                //    if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CAPNHAP]].Value.ToString() == "CẬP NHẬP")
                                //        break;
                                //    if (code == "")
                                //    {
                                //        if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() == "Chi phí")
                                //        {
                                //            crRow[Name[MyConstant.COL_TDKH_DBC_CodeCT]].Value = Guid.NewGuid().ToString();
                                //            crRow[Name[MyConstant.COL_HD_CodeDuAn]].Value = m_codeduan;
                                //            crRow[Name[ThuChiTamUng.COL_THUCHI_CODEKHOANCHI]].Value = codekhoanchi;
                                //            rowdt = DT_cp.NewRow();
                                //            rowdt["Sortid"] = sortID_cp;
                                //            //rowdt["Đã thêm"] = true;
                                //            foreach (var str in Name)
                                //            {
                                //                if (DT_cp.Columns.Contains(str.Key))
                                //                {
                                //                    if (str.Key == "SoTien")
                                //                        rowdt[str.Key] = Double.Parse(crRow[str.Value].Value.ToString());
                                //                    else
                                //                        rowdt[str.Key] = crRow[str.Value].Value.ToString();
                                //                }
                                //            }
                                //            DT_cp.Rows.Add(rowdt);
                                //            sortID_cp++;
                                //        }
                                //        if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() == "Giải chi")
                                //        {
                                //            crRow[Name[MyConstant.COL_TDKH_DBC_CodeCT]].Value = Guid.NewGuid().ToString();
                                //            crRow[Name[MyConstant.COL_HD_CodeDuAn]].Value = m_codeduan;
                                //            crRow[Name[ThuChiTamUng.COL_THUCHI_CODEKHOANCHI]].Value = codekhoanchi;
                                //            rowdt = DT_gc.NewRow();
                                //            rowdt["Sortid"] = sortID_gc;
                                //            //rowdt["Đã thêm"] = true;
                                //            foreach (var str in Name)
                                //            {
                                //                if ((DT_gc.Columns.Contains(str.Key)))
                                //                {
                                //                    if (str.Key == "GiaiChiKinhPhi")
                                //                        rowdt[str.Key] = Double.Parse(crRow[str.Value].Value.ToString());
                                //                    else
                                //                    {
                                //                        if (str.Key == "SoTien")
                                //                            break;
                                //                        else
                                //                            rowdt[str.Key] = crRow[str.Value].Value.ToString();
                                //                    }
                                //                }
                                //            }
                                //            DT_gc.Rows.Add(rowdt);
                                //            sortID_gc++;
                                //        }
                                //    }
                                //}
                                //DataProvider.InstanceTHDA.UpdateDataTable(DT_cp, MyConstant.Tbl_ThuchiTamUng_Khoanchi_cp);
                                //DataProvider.InstanceTHDA.UpdateDataTable(DT_gc, MyConstant.Tbl_ThuchiTamUng_Khoanchi_gc);
                            }
                            else
                                return;
                        }
                        break;
                }
            }
        }
        //private void fcn_update(int vitri)
        //{
        //    Worksheet ws = spread_Thuchitamung_guiduyet.Document.Worksheets[0];
        //    Worksheet ws1 = spread_Thuchitamung_guiduyet.Document.Worksheets[1];
        //    Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
        //    CellRange rangeXD = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
        //    string codekhoanchi = ws.Rows[vitri][Name[ThuChiTamUng.COL_THUCHI_CODE]].Value.ToString();
        //    string codetamung = ws.Rows[vitri][Name[ThuChiTamUng.COL_THUCHI_CODE]].Value.ToString();
        //    string dbString = $"UPDATE  {MyConstant.Tbl_ThuchiTamUng_Khoanchi} SET \"Duyet\"='{true}' WHERE \"Code\"='{codekhoanchi}'";
        //    DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        //    string queryStr = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_cp} WHERE \"CodeDuAn\"='{m_codeduan}' AND \"CodeKhoanChi\"='{codekhoanchi}'";
        //    DataTable DT_cp = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
        //    string queryStr1 = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_gc} WHERE \"CodeDuAn\"='{m_codeduan}' AND \"CodeKhoanChi\"='{codekhoanchi}'";
        //    DataTable DT_gc = DataProvider.InstanceTHDA.ExecuteQuery(queryStr1);
        //    int sortID_cp = DT_cp.Rows.Count;
        //    int sortID_gc = DT_gc.Rows.Count;
        //    DataRow rowdt;
        //    int j = 0;
        //    for (int i = vitri + 1; i <= rangeXD.BottomRowIndex; i++)
        //    {
        //        Row crRow = ws.Rows[i];
        //        string code = ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CODE]].Value.ToString();
        //        if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CAPNHAP]].Value.ToString() == "CẬP NHẬP")
        //            break;
        //        if (code == "")
        //        {
        //            if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() == "Chi phí")
        //            {
        //                crRow[Name[MyConstant.COL_TDKH_DBC_CodeCT]].Value = Guid.NewGuid().ToString();
        //                crRow[Name[MyConstant.COL_HD_CodeDuAn]].Value = m_codeduan;
        //                crRow[Name[ThuChiTamUng.COL_THUCHI_CODEKHOANCHI]].Value = codekhoanchi;
        //                rowdt = DT_cp.NewRow();
        //                rowdt["Sortid"] = sortID_cp;
        //                //rowdt["Đã thêm"] = true;
        //                foreach (var str in Name)
        //                {
        //                    if (DT_cp.Columns.Contains(str.Key))
        //                    {
        //                        if (str.Key == "SoTien")
        //                            rowdt[str.Key] = Double.Parse(crRow[str.Value].Value.ToString());
        //                        else
        //                            rowdt[str.Key] = crRow[str.Value].Value.ToString();
        //                    }
        //                }
        //                DT_cp.Rows.Add(rowdt);
        //                sortID_cp++;
        //            }
        //            if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() == "Giải chi")
        //            {
        //                crRow[Name[MyConstant.COL_TDKH_DBC_CodeCT]].Value = Guid.NewGuid().ToString();
        //                crRow[Name[MyConstant.COL_HD_CodeDuAn]].Value = m_codeduan;
        //                crRow[Name[ThuChiTamUng.COL_THUCHI_CODEKHOANCHI]].Value = codekhoanchi;
        //                rowdt = DT_gc.NewRow();
        //                rowdt["Sortid"] = sortID_gc;
        //                //rowdt["Đã thêm"] = true;
        //                foreach (var str in Name)
        //                {
        //                    if ((DT_gc.Columns.Contains(str.Key)))
        //                    {
        //                        if (str.Key == "GiaiChiKinhPhi")
        //                            rowdt[str.Key] = Double.Parse(crRow[str.Value].Value.ToString());
        //                        else
        //                        {
        //                            if (str.Key == "SoTien")
        //                                break;
        //                            else
        //                                rowdt[str.Key] = crRow[str.Value].Value.ToString();
        //                        }
        //                    }
        //                }
        //                DT_gc.Rows.Add(rowdt);
        //                sortID_gc++;
        //            }
        //        }
        //    }
        //    DataProvider.InstanceTHDA.UpdateDataTable(DT_cp, MyConstant.Tbl_ThuchiTamUng_Khoanchi_cp);
        //    DataProvider.InstanceTHDA.UpdateDataTable(DT_gc, MyConstant.Tbl_ThuchiTamUng_Khoanchi_gc);
        //}
        private void spread_Thuchitamung_guiduyet_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {
            Worksheet ws = spread_Thuchitamung_guiduyet.Document.Worksheets[0];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange rangeXD = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            string heading = ws.Columns[e.ColumnIndex].Heading;
            if (heading == Name[ThuChiTamUng.COL_THUCHI_SOTIEN])
            {   
                if (ws.Rows[e.RowIndex][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() != "Chi phí")
                {
                    MessageShower.ShowInformation("Công tác đang nhập thuộc loại giải chi, vui lòng nhập vào cột Số tiền giải chi!");
                    e.Cell.Value = e.OldValue;
                }
            }
            if (heading == Name[ThuChiTamUng.COL_THUCHI_SOTIENGIAICHI])
            {
                if (ws.Rows[e.RowIndex][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() != "Giải chi")
                {
                    MessageShower.ShowInformation("Công tác đang nhập thuộc loại chi phí, vui lòng nhập vào cột Số tiền tạm ứng/chi phí!");
                    e.Cell.Value = e.OldValue;
                }
                MyFunction.fcn_calsum(spread_Thuchitamung_guiduyet,e.RowIndex);
            }
            if(heading == Name[ThuChiTamUng.COL_THUCHI_NOIDUNGCHIPHI])
            {
                 
            }
        }

        private void Form_LuachonChiPhi_GiaChi_FormClosed(object sender, FormClosedEventArgs e)
        {
            spread_Thuchitamung_guiduyet.Dispose();
            GC.Collect();
        }
        //private void fcn_calsum()
        //{
        //    Worksheet ws = spread_Thuchitamung_guiduyet.Document.Worksheets[0];
        //    Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
        //    CellRange rangeXD = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
        //    int beginindex = 0;
        //    int endindex = 0;
        //    int vitri = 0;
        //    for (int i = ws.SelectedCell.BottomRowIndex + 1; i >= rangeXD.TopRowIndex; i--)
        //    {
        //        Row crRow = ws.Rows[i];
        //        string kyhieu = crRow[Name[ThuChiTamUng.COL_THUCHI_THUCHIEN]].Value.ToString();
        //        if (kyhieu == "THÊM GIẢI CHI")
        //            endindex = i;
        //        if (kyhieu == "THÊM CHI PHÍ")
        //            beginindex = i;
        //        if(kyhieu==""&&ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CAPNHAP]].Value.ToString()=="CẬP NHẬP")
        //        {
        //            vitri = i;
        //            break;
        //        }
        //    }
        //    ws.Rows[vitri][Name[ThuChiTamUng.COL_THUCHI_SOTIENGIAICHI]].Formula = $"SUM({Name[ThuChiTamUng.COL_THUCHI_SOTIENGIAICHI]}{beginindex + 2}:{Name[ThuChiTamUng.COL_THUCHI_SOTIENGIAICHI]}{endindex + 1})";
        //}

    }
}
