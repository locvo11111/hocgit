using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class ExcelHelper
    {
        public static Dictionary<string, int> GetListColumnByRange(CellRange range)
        {
            Dictionary<string, int> lstTitles = new Dictionary<string, int>();
            try
            {
                if (range.LeftColumnIndex > 0)
                {
                    for (int i = 0; i < range.ColumnCount; i++)
                    {
                        CellRange index = range[i];
                        if (!index.Value.IsEmpty)
                            lstTitles.Add(index.Value.TextValue, i + range.LeftColumnIndex);
                    }
                }
                else
                {
                    for (int i = range.LeftColumnIndex; i <= range.RightColumnIndex; i++)
                    {
                        CellRange index = range[i];
                        if (!index.Value.IsEmpty)
                            lstTitles.Add(index.Value.TextValue, i);
                    }
                }
            }
            catch (Exception)
            {
                return lstTitles;
            }
            return lstTitles;
        }
    }
}