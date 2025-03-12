using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Dto
{
    public class DanhSachDACTDto
    {
        public string Code { get; set; }
        public string TenDuAn { get; set; }
        public string TenCongTrinh { get; set; }
        public string DiaChi { get; set; }
        public string DaiDien { get; set; }
        public string DienThoai { get; set; }
        public string NhaThauChinh { get; set; }
        public List<BieuDoDanhSachDACT> BieuDoPoints { get; set; } = new List<BieuDoDanhSachDACT>();
    }

    public class BieuDoDanhSachDACT
    {
        public string Ten { get; set; }
        public double SoLuong { get; set; }
    }
}
