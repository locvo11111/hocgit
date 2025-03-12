using System;

namespace PhanMemQuanLyThiCong.Model
{
    public class GiaoViecRequest
    {
        public string CodeDuAn { get; set; }
        public string CodeConstructor { get; set; }
        public Guid UserId { get; set; }
        public string CommandId { get; set; }
        public string State { get; set; }
        public DateTime? Date { get; set; }
        bool GetNgoaiKeHoach { get; set; }
        public int? TypeCV { get; set; }
    }
}