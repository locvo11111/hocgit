using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class ValidateHelper
    {
        private static Regex regexDate = new Regex(@"^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$");
        static readonly List<string> regexCate = new List<string>() { "+", "-", "=" };

        /// <summary>
        /// IsNumber
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNumber(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            return Regex.IsMatch(text, "[0-9]");
        }

        public static bool IsDateCate(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text)) return false;
                foreach (string cate in regexCate)
                {
                    if (text.Contains(cate)) return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// IsDateTime
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsDateTime(object obj)
        {

            string text = obj.ToString();
            if (!DateTime.TryParse(text, out DateTime date) || date.Year < 2000)
                return false;

            return true;
        }

        public static DateTime ConvertNumberToDatetime(double number)
        {
            try
            {
                return DateTime.FromOADate(number);
            }
            catch (Exception)
            {
                return default(DateTime);
            }
        }

        public static bool ConvertDateTime(string text, out DateTime? date)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    date = null;
                    return false;
                }
                if (int.TryParse(text, out int so))
                {
                    date = DateTime.FromOADate(double.Parse(so.ToString()));
                    if (date.HasValue && date.Value.CompareTo(DateTime.Parse("01/01/2015")) < 0)
                    {
                        return false;
                    }
                }
                date = DateTime.Parse(text);
                return true;
            }
            catch (Exception)
            {
                date = null;
                return false;
            }
        }

        /// <summary>
        /// IsBoolean
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsBoolean(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            return bool.TryParse(text, out bool date);
        }

        /// <summary>
        /// IsTimeSpan
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsTimeSpan(string text)
        {
            if (!text.Contains("đến")) return false;
            text = text.Replace("đến", string.Empty);
            string[] chuoi = text.Split(' ');
            int coutTime = 0;
            foreach (var item in chuoi)
            {
                if (string.IsNullOrEmpty(item)) continue;
                if (!item.Contains("h")) return false;
                coutTime++;
                if (!TimeSpan.TryParse(item.Replace("h", ":"), out TimeSpan timeSpan)) return false;
            }
            if (coutTime > 2) return false;
            return true;
        }

        /// <summary>
        /// IsTimeSpan
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsCompareTimeSpan(string text)
        {
            if (!text.Contains("đến")) return false;
            text = text.Replace("đến", string.Empty);
            TimeSpan startTime = new TimeSpan();
            TimeSpan endTime = new TimeSpan();
            bool isStart = false;
            bool isEnd = false;
            string[] chuoi = text.Split(' ');
            int i = 0;
            foreach (var item in chuoi)
            {
                if (string.IsNullOrEmpty(item)) continue;
                if (i == 0) isStart = TimeSpan.TryParse(item.Replace("h", ":"), out startTime);
                else isEnd = TimeSpan.TryParse(item.Replace("h", ":"), out endTime);
                i++;
            }
            if (isStart && isEnd)
            {
                return endTime.CompareTo(startTime) > 0;
            }
            return true;
        }
    }
}