using DevExpress.XtraSpreadsheet.Model;
using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class NotificationViewModel
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public NotificationStateEnum? State { get; set; } //Chưa đọc, Đã đọc từ popup, đã click
        public NotificationTypeEnum? Type { get; set; } //Type
        public string RelativeTime
        {
            get 
            {
                var timeSpan = (DateTime.Now - CreatedOn);
                var seconds = timeSpan.Seconds;
                var minutes = Math.Round(timeSpan.TotalMinutes);
                var hours = Math.Round(timeSpan.TotalHours);
                var days = (DateTime.Now.Date - CreatedOn.Date).Days;
                
                if (days > 0)
                {
                    if (hours > 10)
                        return $"{days} ngày trước";
                    else 
                        return $"{hours} giờ trước";
                }
                else if (hours > 0)
                    return $"{hours} giờ trước";
                else if (minutes > 0)
                    return $"{minutes} phút trước";
                else
                    return $"{seconds} giây trước";

            } 
        }
    }
}
