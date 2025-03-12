using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class KhoiLuongXuatVatTu : TongHopVatTuACapB
    {
        public DateTime Ngay { get; set; }
        public double KhoiLuong { get; set; }
        public double DonGia { get; set; }
        public double GiaTri
        {
            get
            {
                return KhoiLuong * DonGia;
            }
        }
    }
}
