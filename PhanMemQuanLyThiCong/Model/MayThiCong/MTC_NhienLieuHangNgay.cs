using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.MayThiCong
{
    public class MTC_NhienLieuHangNgay
    {
        public string Code { get; set; }
        public string CodeNhienLieu { get; set; }
        public DateTime? Ngay { get; set; }
        public double? KhoiLuong { get; set; }
        public int? STT { get; set; }
        public bool Modified { get; set; }
        public string LastChange { get; set; }
        public string CreatedOn { get; set; }
        public bool ModifiedFromServer { get; set; }
    }
}
