using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Model.MayThiCong
{
    public class MTC_TongHopNhienLieuHangNgay : MTC_NhienLieu
    {
        //public string Code { get; set; }
        //public string CodeNhienLieu { get; set; }
        public double? KhoiLuong { get; set; } = 0;//kl nhập hôm nay
        //public double? KhoiLuongDaDung { get; set; } = 0;
        //public double? KhoiLuongDaNhap { get; set; } = 0;
        public double? ConLai { get { return KhoiLuongDaNhap - KhoiLuongDaDung; } }
        //public bool IsHienThi { get; set; }
        //public bool Modified { get; set; }
        //public bool ModifiedFromServer { get; set; }
        //public string LastChange { get; set; }
        //public string CreatedOn { get; set; }
    }
}
