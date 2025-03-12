using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class WordHelper
    {
        public static void SetValue(this TableCell cell, Document doc, string value)
        {
            doc.Replace(cell.ContentRange, value);
        }
    }
}
