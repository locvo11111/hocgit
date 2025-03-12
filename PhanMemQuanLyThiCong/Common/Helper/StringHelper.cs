using DevExpress.DirectX.Common.Direct2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class StringHelper
    {
        public static string GetHangMucRangeName(string codeHM)
        {
            return $"HM_{codeHM.Replace("-", "")}";
        }

        public static string ToN2String(this double db)
        {
            return (db == 0) ? "-": db.ToString("N2");
        }

        public static string ToN0String(this double db)
        {
            return (db == 0) ? "-" : db.ToString("N0");
        }
        
        public static string ToN0String(this long db)
        {
            return (db == 0) ? "-" : db.ToString("N0");
        }

        public static string Min(string str1, string str2)
        {
            return (string.Compare(str1, str2) < 0) ? str1 : str2;
        }
        
        public static string Max(string str1, string str2)
        {
            return (string.Compare(str1, str2) < 0) ? str2 : str1;
        }

    }
}
