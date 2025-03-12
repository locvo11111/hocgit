using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class FileSizeFormatter
    {
        // Load all suffixes in an array  
        static readonly string[] suffixes =
        { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        public static string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n1}{1}", number, suffixes[counter]);
        }
        public static decimal FormatSizeMb(long bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
                if (counter == 2) break;
            }
            return number;
        }

        public static double ConvertByteToMb(FileInfo fileInfo)
        {
            if (fileInfo.Length > 0)
            {
                return fileInfo.Length / (1024 * 1024);
            }
            return 0;
        }
    }
}
