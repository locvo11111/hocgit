using System;
using System.Collections.Generic;
using System.Linq;

namespace PhanMemQuanLyThiCong.Common
{
    public class ConvertToList
    {
        public static List<string> ConvertList(string convert)
        {
            return convert.Split(',').Select(h => h).ToList();
        }
        public static string ConvertString<T>(List<string> list)
        {
            return string.Join(", ", list);
        }
    }
}