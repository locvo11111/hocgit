using AutoMapper.Internal;
using DevExpress.Office.PInvoke;
using DevExpress.PivotGrid.SliceQueryDataSource;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpellChecker.Parser;
using DevExpress.XtraSpreadsheet.Mouse;
using PhanMemQuanLyThiCong.Common.Constant;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class SpreadsheetHelper
    {
        public static void ReplaceAllFormulaAfterImport(CellRange range)
        {
////          range.Worksheet.Workbook.History.IsEnabled = false;
            range.Worksheet.Workbook.BeginUpdate();

            SearchOptions MySearchOptions = new SearchOptions()
            {
                SearchIn = SearchIn.Values,
                MatchEntireCellContents = false,
            };

            var res = range.Search(MyConstant.PrefixFormula, MySearchOptions);
            foreach (var cell in res)
            {
                cell.Formula = cell.Value.ToString().Replace(MyConstant.PrefixFormula, "");
            }
            range.Worksheet.Calculate();

            res = range.Search(MyConstant.PrefixMerge, MySearchOptions);

            List<CellRange> ranges = new List<CellRange>();
            var ws = range.Worksheet;
            foreach ( var cell in res)
            {
                string val = cell.Value.ToString().Replace(MyConstant.PrefixMerge, "");
                bool isParse = int.TryParse(val, out int ind);
                if (isParse)
                {
                    ws.Range.FromLTRB(cell.ColumnIndex, ind, cell.ColumnIndex, cell.RowIndex).Merge();
                }

            }    
            
            res = range.Search(MyConstant.PrefixDate, MySearchOptions);

            foreach (var cell in res)
            {
                cell.SetValueFromText(cell.Value.ToString().Replace(MyConstant.PrefixDate, ""));
            }

            range.Worksheet.Workbook.EndUpdate();

            
            foreach (var cell in res)
            {
                cell.Formula = cell.Value.ToString().Replace(MyConstant.PrefixFormula, "");
            }
            try
            {
                ////          range.Worksheet.Workbook.History.IsEnabled = true;

            }
            catch (Exception) { }

        }

        public static void FormatRowsInRange(CellRange range, string typeRowHeading, string rowChaHeading = null, string codeHeading = null, 
            string colMaVatTu = null, string colMaCongTac = null,IEnumerable<string> codesCT = null,bool Visible=true, string ColFileDinhKem = null, Dictionary<string, string> dic = null)
        {
            List<string> ValidMaCT = new List<string>();
            if (codesCT != null)
            {
                string dbString = $"SELECT MaDinhMuc FROM tbl_DinhMucAll WHERE MaDinhMuc IN ({MyFunction.fcn_Array2listQueryCondition(codesCT)})";

                DataTable dtDm = DataProvider.InstanceTBT.ExecuteQuery(dbString);
                ValidMaCT = dtDm.AsEnumerable().Select(x => x["MaDinhMuc"].ToString()).ToList();
            }

            var ws = range.Worksheet;
            ws.OutlineOptions.SummaryRowsBelow = false;
////          ws.Workbook.History.IsEnabled = false;
            ws.Workbook.BeginUpdate();
            List<int> indsFormated = new List<int>();
            string[] typesRowNeedGroup =
            {
                //MyConstant.TYPEROW_HangMuc,
                MyConstant.TYPEROW_PhanTuyen,
                MyConstant.TYPEROW_Nhom,


            };
            for (int i = range.TopRowIndex; i < range.BottomRowIndex + 1; i++)
            {
                if (indsFormated.Contains(i))
                    continue;

                indsFormated.Add(i);
                Row crRow = ws.Rows[i];
                string typeRow = crRow[typeRowHeading].Value.ToString();

                //if (typesRowNeedGroup.Contains(typeRow))
                //{
                //    var nextInd = SpreadsheetHelper.FindNextGreaterTypeInd(range, i, rowChaHeading, typeRowHeading, typeRow);
                //    if (nextInd > i + 1)
                //        ws.Rows.Group(i, nextInd - 1, false);                
                //}
                switch (typeRow)
                {
                    case MyConstant.TYPEROW_CVTong:
                        //crRow.Font.Color = MyConstant.color_Row_CongTrinh;
                        crRow.Font.Bold = true;
                        crRow.FillColor= Color.Yellow;
                        break;    
                    case MyConstant.TYPEROW_SUMTT:
                        //crRow.Font.Color = MyConstant.color_Row_CongTrinh;
                        crRow.Font.Bold = true;
                        break;
                    case MyConstant.TYPEROW_CongTrinh:
                        crRow.Font.Color = MyConstant.color_Row_CongTrinh;
                        crRow.Font.Bold = true;
                        crRow.Visible = Visible;
                        break;
                    
                    case MyConstant.TYPEROW_MuiThiCong:
                        crRow.Font.Color = MyConstant.color_Row_MuiTC;
                        crRow.Font.Bold = true;
                        break;      
                    case MyConstant.TYPEROW_GOP:
                        crRow.Font.Color = MyConstant.color_Row_GOP;
                        crRow.Font.Bold = true;
                        break;
                    case MyConstant.TYPEROW_HangMuc:
                        crRow.Font.Color = MyConstant.color_Row_HangMuc;
                        crRow.Font.Bold = true;
                        break;
                    case MyConstant.TYPEROW_PhanTuyen:
                    case MyConstant.TYPEROW_HTPhanTuyen:
                        crRow.Font.Color = MyConstant.color_Row_PhanTuyen;
                        crRow.Font.Bold = true;
                        if(typeRow== MyConstant.TYPEROW_HTPhanTuyen)
                            crRow.Visible = Visible;
                        if (rowChaHeading.HasValue())
                        {
                            int indRowCha = ws.Range.GetColumnIndexByName(rowChaHeading);
                            var indsCon = ws.Range.FromLTRB(indRowCha, i, indRowCha, range.BottomRowIndex)
                                            .Search((i + 1).ToString(), MyConstant.MySearchOptions).Select(x => x.RowIndex);
                            
                        }

                        if (typeRow == MyConstant.TYPEROW_PhanTuyen)
                        {
                            if (rowChaHeading.HasValue() && colMaCongTac.HasValue())
                            {
                                int indRowCha = ws.Range.GetColumnIndexByName(rowChaHeading);
                                var indsCon = ws.Range.FromLTRB(indRowCha, i, indRowCha, range.BottomRowIndex)
                                                .Search((i + 1).ToString(), MyConstant.MySearchOptions).Select(x => x.RowIndex);
                                foreach (var ind in indsCon)
                                {
                                    ws.Rows[ind].Font.Color = crRow.Font.Color;
                                    ws.Rows[ind][colMaCongTac].Font.Color = crRow[colMaCongTac].Font.Color;
                                    //indsFormated.Add(ind);
                                }
                            }
                        }    
                        break;
                    case MyConstant.TYPEROW_Nhom:
                        crRow.Font.Color = MyConstant.color_Row_NhomCongTac;
                        crRow.Font.Bold = true;
                        if(!string.IsNullOrEmpty(ColFileDinhKem))
                            ws.Hyperlinks.Add(crRow[ColFileDinhKem], $"{ColFileDinhKem}{i+1}", false, "Xem chi tiết");
                        goto ChangeChildColor;
                    case MyConstant.TYPEROW_CVCha:
                        if (codeHeading.HasValue())
                        {
                            string code = crRow[codeHeading].Value.ToString();
                            if (colMaVatTu.HasValue() && code.Contains(";"))
                            {
                                crRow[colMaVatTu].Font.Color = Color.Red;
                                ws.Comments.Add(crRow[colMaVatTu], "TBT", "Vật tư mới ở đơn vị nhận thầu. Không có tại giao thầu!");
                            }


                            if (colMaCongTac.HasValue() && codesCT != null && !ValidMaCT.Contains(crRow[colMaCongTac].Value.ToString()))
                            {
                                crRow[colMaCongTac].FillColor = MyConstant.colorChuaCodb;
                            }
                        }
                        if (!string.IsNullOrEmpty(ColFileDinhKem))
                            ws.Hyperlinks.Add(crRow[ColFileDinhKem], $"{ColFileDinhKem}{i + 1}", false, "Xem chi tiết");
                        if (dic!=null)
                        {
                            bool isBD = DateTime.TryParse(crRow[dic[TDKH.COL_NgayBatDau]].Value.ToString(), out DateTime dayBD);
                            bool isKT = DateTime.TryParse(crRow[dic[TDKH.COL_NgayKetThuc]].Value.ToString(), out DateTime dayKT);     
                            bool isBDGT = DateTime.TryParse(crRow[dic[TDKH.COL_NgayBatDauGT]].Value.ToString(), out DateTime dayBDGT);
                            bool isKTGT = DateTime.TryParse(crRow[dic[TDKH.COL_NgayKetThucGT]].Value.ToString(), out DateTime dayKTGT);
                            if(isBD&&isBDGT&&dayBD<dayBDGT)
                                crRow[dic[TDKH.COL_NgayBatDau]].Font.Color = MyConstant.colorNhanhCham;      
                            if(isKT&& isKTGT && dayKT>dayKTGT)
                                crRow[dic[TDKH.COL_NgayKetThuc]].Font.Color = MyConstant.colorNhanhCham;
                        }
                        break;
                    case MyConstant.TYPEROW_NhomDienGiai:
                        crRow.Font.Color = MyConstant.color_Row_NhomDienGiai;
                        crRow.Font.Bold = false;

                        ChangeChildColor:
                        if (rowChaHeading.HasValue() && colMaCongTac.HasValue())
                        {
                            int indRowCha = ws.Range.GetColumnIndexByName(rowChaHeading);
                            var indsCon = ws.Range.FromLTRB(indRowCha, i, indRowCha, range.BottomRowIndex)
                                            .Search((i + 1).ToString(), MyConstant.MySearchOptions).Select(x => x.RowIndex);
                            foreach (var ind in indsCon)
                            {
                                ws.Rows[ind].Font.Color = crRow.Font.Color;
                                ws.Rows[ind][colMaCongTac].Font.Color = crRow[colMaCongTac].Font.Color;
                                indsFormated.Add(ind);
                                if (!string.IsNullOrEmpty(ColFileDinhKem))
                                    ws.Hyperlinks.Add(ws.Rows[ind][ColFileDinhKem], $"{ColFileDinhKem}{ind + 1}", false, "Xem chi tiết");
                                if (typeRow == MyConstant.TYPEROW_Nhom)
                                {
                                    ws.Rows[ind].Visible = MSETTING.Default.CongTacInNhomVisible;
                                }
                                if (dic != null)
                                {
                                    bool isBD = DateTime.TryParse(ws.Rows[ind][dic[TDKH.COL_NgayBatDau]].Value.ToString(), out DateTime dayBD);
                                    bool isKT = DateTime.TryParse(ws.Rows[ind][dic[TDKH.COL_NgayKetThuc]].Value.ToString(), out DateTime dayKT);
                                    bool isBDGT = DateTime.TryParse(ws.Rows[ind][dic[TDKH.COL_NgayBatDauGT]].Value.ToString(), out DateTime dayBDGT);
                                    bool isKTGT = DateTime.TryParse(ws.Rows[ind][dic[TDKH.COL_NgayKetThucGT]].Value.ToString(), out DateTime dayKTGT);
                                    if (isBD && isBDGT && dayBD < dayBDGT)
                                        ws.Rows[ind][dic[TDKH.COL_NgayBatDau]].Font.Color = MyConstant.colorNhanhCham;
                                    if (isKT && isKTGT && dayKT > dayKTGT)
                                        ws.Rows[ind][dic[TDKH.COL_NgayKetThuc]].Font.Color = MyConstant.colorNhanhCham;
                                }
                            }
                        }
                        break;
                    case MyConstant.TYPEROW_CVCON:
                    case MyConstant.TYPEROW_CVCHIA:
                        crRow.Font.Color = MyConstant.color_Row_DienGiai;
                        crRow.Font.Bold = false;

                        break;

                    default:
                        break;
                }
            };

            ws.Workbook.EndUpdate();

            try
            {
////          ws.Workbook.History.IsEnabled = true;

            }
            catch (Exception) { }
        }

        public static void FindAllChildrenOfCongTac(CellRange range, Row crRow, string rowChaHeading)
        {

        }

        public static int FindNextGreaterTypeInd(CellRange range, int crRowInd, string rowChaHeading, string typeRowHeading, string crTypeRow = null)
        {
            Worksheet ws = range.Worksheet;
            int indColCha = ws.Range.GetColumnIndexByName(rowChaHeading);

            if (crTypeRow is null)
                crTypeRow = ws.Rows[crRowInd][typeRowHeading].Value.ToString();

            int indTypeRow = Array.IndexOf(MyConstant.TypeRows, crTypeRow);
            string[] parentTypeRow = MyConstant.TypeRows.Take(indTypeRow + 1).ToArray();
            
            for (int i = crRowInd + 1; i <= range.BottomRowIndex; i++)
            {
                string sTypeRow = ws.Rows[i][typeRowHeading].Value.ToString();
                if (parentTypeRow.Contains(sTypeRow))
                    return i;
            }

            return range.BottomRowIndex;
        }
        
        public static int FindPrevGreaterTypeInd(CellRange range, int crRowInd, string rowChaHeading, string typeRowHeading, string crTypeRow = null)
        {
            Worksheet ws = range.Worksheet;
            int indColCha = ws.Range.GetColumnIndexByName(rowChaHeading);

            if (crTypeRow is null) 
                crTypeRow = ws.Rows[crRowInd][typeRowHeading].Value.ToString();

            int indTypeRow = Array.IndexOf(MyConstant.TypeRows, crTypeRow);
            string[] parentTypeRow = MyConstant.TypeRows.Take(indTypeRow + 1).ToArray();
            
            for (int i = crRowInd - 1; i > range.TopRowIndex; i--)
            {
                string sTypeRow = ws.Rows[i][typeRowHeading].Value.ToString();
                if (parentTypeRow.Contains(sTypeRow))
                    return i;
            }

            return range.TopRowIndex + 1;
        }
        
        public static int FindPrevSameTypeInd(CellRange range, int crRowInd, string rowChaHeading, string typeRowHeading, string crTypeRow = null)
        {
            Worksheet ws = range.Worksheet;
            int indColCha = ws.Range.GetColumnIndexByName(rowChaHeading);

            if (crTypeRow is null) 
                crTypeRow = ws.Rows[crRowInd][typeRowHeading].Value.ToString();
            
            for (int i = crRowInd - 1; i > range.TopRowIndex; i--)
            {
                string sTypeRow = ws.Rows[i][typeRowHeading].Value.ToString();
                if (crTypeRow == sTypeRow)
                    return i;
            }

            return -1;
        }
        
        public static int FindNextSameTypeInd(CellRange range, int crRowInd, string rowChaHeading, string typeRowHeading, string crTypeRow = null)
        {
            Worksheet ws = range.Worksheet;
            int indColCha = ws.Range.GetColumnIndexByName(rowChaHeading);

            if (crTypeRow is null) 
                crTypeRow = ws.Rows[crRowInd][typeRowHeading].Value.ToString();

            
            for (int i = crRowInd + 1; i <= range.BottomRowIndex; i++)
            {
                string sTypeRow = ws.Rows[i][typeRowHeading].Value.ToString();
                if (crTypeRow == sTypeRow)
                    return i;
            }

            return range.BottomRowIndex + 1;
        }

        public static int[] FindAllChildRow(CellRange range, int crRowInd, string rowChaHeading, bool isRecursive)
        {
            Worksheet ws = range.Worksheet;
            int indColCha = ws.Range.GetColumnIndexByName(rowChaHeading);
            List<int> inds = new List<int>();
            var ret = ws.Range.FromLTRB(indColCha, crRowInd + 1, indColCha, range.BottomRowIndex)
                        .Search((crRowInd + 1).ToString(), MyConstant.MySearchOptions).Select(x => x.RowIndex);
            inds.AddRange(ret);

            if (isRecursive)
            {

                foreach (int ind in inds)
                {
                    var indcons = FindAllChildRow(range, ind, rowChaHeading, true);
                    inds.AddRange(indcons);
                }
            }

            return inds.Distinct().ToArray();
        }
        
    }
}
