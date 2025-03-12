using DevExpress.Spreadsheet;
using PM360.Common.Helper;
using System.Linq;

namespace PM360.Common.Validate
{
    public class ValidateCell
    {
        ///// <summary>
        ///// ValidateCell
        ///// </summary>
        ///// <param name="worksheet"></param>
        ///// <param name="rowIndex"></param>
        ///// <param name="columnName"></param>
        ///// <returns></returns>
        //public static bool ValidateCellNotExitsOrNull(Worksheet sheet, CellRange range, int rowIndex, string columnName)
        //{
        //    int index = ExcelHelper.GetColumnIndexByName(range, columnName, sheet.Name);
        //    if (index == -1) return true;
        //    if (sheet.Rows[rowIndex][index].Value.IsEmpty) return true;
        //    return false;
        //}
        /// <summary>
        /// ValidateCellIsEmtry
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="range"></param>
        /// <param name="isComment"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static bool ValidateCellIsEmtry(Worksheet sheet, CellRange range, bool isComment = false, bool isRequired = false, bool isDisplayText = false)
        {
            bool isEmpty = false;
            isEmpty = range.Value.IsEmpty;
            if (isDisplayText && isEmpty)
            {
                if (!string.IsNullOrEmpty(((Cell)range).DisplayText)) isEmpty = false;
            }
            string author = "PM360:";
            if (isEmpty && isRequired)
            {
                if (!sheet.Comments.GetComments(range).Any()) sheet.Comments.Add(range, author, "Không được để trống dữ liệu");
            }
            return isEmpty;
        }

        /// <summary>
        /// ValidateCellIsNumber
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="range"></param>
        /// <param name="isComment"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static bool ValidateCellIsNumber(Worksheet sheet, CellRange range, bool isComment = false)
        {
            bool isEmpty = false;
            isEmpty = range.Value.IsNumeric;
            string author = "PM360:";
            if (!isEmpty)
            {
                if (!sheet.Comments.GetComments(range).Any()) sheet.Comments.Add(range, author, "Vui lòng nhập đúng kiểu dữ liệu số (number)");
            }
            return isEmpty;
        }
        /// <summary>
        /// ValidateCellIsDatetime
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="range"></param>
        /// <param name="isComment"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static bool ValidateCellIsDatetime(Worksheet sheet, CellRange range, bool isComment = false)
        {
            bool isEmpty = false;
            isEmpty = range.Value.IsDateTime;
            string author = "PM360:";
            if (!isEmpty)
            {
                if (!sheet.Comments.GetComments(range).Any()) sheet.Comments.Add(range, author, "Vui lòng nhập đúng kiểu dữ liệu ngày tháng");
            }
            return isEmpty;
        }
        /// <summary>
        /// ValidateCellIsBolean
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="range"></param>
        /// <param name="isComment"></param>
        /// <returns></returns>
        public static bool ValidateCellIsBolean(Worksheet sheet, CellRange range, bool isComment = false)
        {
            bool isEmpty = false;
            isEmpty = range.Value.IsBoolean;
            string author = "PM360:";
            if (!isEmpty)
            {
                if (!sheet.Comments.GetComments(range).Any()) sheet.Comments.Add(range, author, "Vui lòng nhập đúng kiểu boolean");
            }
            return isEmpty;
        }
        /// <summary>
        /// ValidateCellIsEmtry
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="range"></param>
        /// <param name="isComment"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static bool ValidateCellIsEmtry(Worksheet sheet, Cell cell, bool isComment = false, bool isRequired = false, bool isDisplayText = false)
        {
            bool isEmpty = false;
            isEmpty = cell.Value.IsEmpty;
            if (isDisplayText && isEmpty)
            {
                if (!string.IsNullOrEmpty(cell.DisplayText)) isEmpty = false;
            }
            string author = "PM360:";
            if (isEmpty && isRequired)
            {
                if (!sheet.Comments.GetComments(cell).Any()) sheet.Comments.Add(cell, author, "Không được để trống dữ liệu");
            }
            return isEmpty;
        }
        /// <summary>
        /// ValidateCellIsNumber
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="range"></param>
        /// <param name="isComment"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static bool ValidateCellIsNumber(Worksheet sheet, Cell cell, bool isComment = false)
        {
            bool isEmpty = false;
            isEmpty = cell.Value.IsNumeric;
            string author = "PM360:";
            if (!isEmpty)
            {
                if (!sheet.Comments.GetComments(cell).Any()) sheet.Comments.Add(cell, author, "Vui lòng nhập đúng kiểu dữ liệu số (number)");
            }
            return isEmpty;
        }
    }
}