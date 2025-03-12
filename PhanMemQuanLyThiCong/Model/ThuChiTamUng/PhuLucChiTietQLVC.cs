using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class PhuLucChiTietQLVC
    {
        public string Code { get; set; }
        public int STT { get; set; }
        public string TenCongViec { get; set; }
        public int DonGia { get; set; }
        public int KhoiLuong { get; set; }
        public int ThanhTien { get { return DonGia * KhoiLuong; } }
        public string TenNoiDungUng { get; set; }
        public string CodeCha { get; set; }
        public string CodeDeXuat { get; set; }


    }
}
