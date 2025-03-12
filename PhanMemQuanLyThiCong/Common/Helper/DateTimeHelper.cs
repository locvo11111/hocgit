using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.UI;
using System;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class DateTimeHelper
    {
        public static bool IsTimeSpan(string text, out TimeSpan timeSpan)
        {
            timeSpan = TimeSpan.Zero;
            try
            {
                if (string.IsNullOrEmpty(text)) return true;
                if (text.IndexOf("h") < 0|| !TimeSpan.TryParse(text.Replace("h", ":"), out timeSpan))
                {
                    XtraMessageBox.Show($"Vui lòng nhập đúng 1 trong định dạng thời gian sau {Environment.NewLine} (VD: [08h00; 8h00])", "Quản lý thi công - Thông báo");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CompareTimeSpanNhoHon(string gioIndex, TimeSpan timeKetThuc)
        {
            if (string.IsNullOrEmpty(gioIndex)) return true;
            TimeSpan timeBatDau = TimeSpan.Zero;
            bool res1 = IsTimeSpan(gioIndex, out timeBatDau);
            if (res1)
            {
                if (timeBatDau.CompareTo(timeKetThuc) >= 0)
                {
                    XtraMessageBox.Show($"Vui lòng nhập giờ thi công nhỏ hơn [{timeKetThuc.ToString("hh'h'mm")}]");
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool CompareTimeSpanLonHon(string gioIndex, TimeSpan timeBatDau)
        {
            if (string.IsNullOrEmpty(gioIndex)) return true;
            TimeSpan timeKetThuc = TimeSpan.Zero;
            bool res1 = IsTimeSpan(gioIndex, out timeKetThuc);
            if (res1)
            {
                if (timeBatDau.CompareTo(timeKetThuc) >= 0)
                {
                    XtraMessageBox.Show($"Vui lòng nhập giờ thi công lớn hơn [{timeBatDau.ToString("hh'h'mm")}]");
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool CompareTimeSpanBetwwen(string gioIndex, TimeSpan timeBatDau, TimeSpan timeKetThuc)
        {
            if (string.IsNullOrEmpty(gioIndex)) return true;
            TimeSpan timeIndex = TimeSpan.Zero;
            bool res1 = IsTimeSpan(gioIndex, out timeIndex);
            if (res1)
            {
                if (timeBatDau.CompareTo(timeIndex) >= 0 || timeIndex.CompareTo(timeKetThuc)>=0)
                {
                    XtraMessageBox.Show($"Vui lòng nhập giờ thi công nằm trong khoảng từ [{timeBatDau.ToString("hh'h'mm")} đến {timeKetThuc.ToString("hh'h'mm")}]");
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool CompareTimeSpanBetwwenBatDauAndKetThuc(string gioBatDau, string gioBetwen, string gioKetThuc)
        {
            bool isEmtryBatDau = string.IsNullOrEmpty(gioBatDau);
            bool isEmtryKetThuc = string.IsNullOrEmpty(gioKetThuc);
            bool isEmtryBetwwen = string.IsNullOrEmpty(gioBetwen);
            if (isEmtryBatDau && isEmtryKetThuc) return true;
            if(isEmtryBetwwen) return true;
            TimeSpan timeBatDau = TimeSpan.Zero;
            TimeSpan timeKetThuc = TimeSpan.Zero;
            TimeSpan timeBetwwen = TimeSpan.Zero;
            bool res1 = false;
            bool res2 = false;
            bool res3 = false;
            if (!isEmtryBatDau)
                res1 = IsTimeSpan(gioBatDau, out timeBatDau);
            if (!isEmtryKetThuc)
                res2 = IsTimeSpan(gioKetThuc, out timeKetThuc);
            if (!isEmtryBetwwen)
                res3 = IsTimeSpan(gioBetwen, out timeBetwwen);           
            if (res1 && res2 && res3)
            {
                if (timeBatDau.CompareTo(timeBetwwen) >= 0 || timeBetwwen.CompareTo(timeKetThuc) >= 0)
                {
                    XtraMessageBox.Show($"Vui lòng nhập giờ thi công nằm trong khoảng từ [{gioBatDau}] đến [{gioKetThuc}]");
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Find the Nth DayOfWeek in a specified month and year
        /// </summary>
        /// <param name="n">1 = first or last, 2 = second/next to last, etc.</param>
        /// <param name="fromBeginning">True for first, second, etc. 
        /// or False for last, next to last.</param>
        public static DateTime nthDay(int year, int month, int n, DayOfWeek dayOfWeek, bool fromBeginning = true)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime startInNweek = startDate.AddDays(7 * (n-1));

            var crDOW = startDate.DayOfWeek;
            var offset = ((int)dayOfWeek + 7 - (int)crDOW) % 7;

            //answer (step 4)
            return startInNweek.AddDays(offset);

        }

        public static void CalcNthWeekStartEndDate(int year, int month, int nthWeek, DayOfWeek startWeek,
            out DateTime dateBDWeek, out DateTime dateKTWeek)
        {
            DateTime startDateInMonth = new DateTime(year, month, 1);
            DayOfWeek lastDOW = (DayOfWeek)(((int)startWeek + 6) % 7);

            dateKTWeek = DateTimeHelper.nthDay(year, month, nthWeek, lastDOW, false);
            dateBDWeek = dateKTWeek.AddDays(-6);

            if (dateBDWeek < startDateInMonth)
            {
                dateBDWeek = dateBDWeek.AddDays(7);
                dateKTWeek = dateKTWeek.AddDays(7);
            }
        }

        public static DateTime Min(DateTime date1, DateTime date2)
        {
            return (date1 > date2) ? date2 : date1;
        }  
        
        public static DateTime Max(DateTime date1, DateTime date2)
        {
            return (date1 > date2) ? date1 : date2;
        }
    }
}