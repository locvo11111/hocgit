using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class PhuLucThuCong_ChiTiet
    {
        public string Code { get; set; }
        public int STT { get; set; }
        public string TenCongViec { get; set; }
        public string MaHieu { get; set; }
        public string DonVi { get; set; }
        public double DonGia { get; set; }
        public double KhoiLuong { get; set; }
        public long ThanhTien { get; set; }
        public long ThanhTienThuCong { get {
                return (long)Math.Round(DonGia * KhoiLuong);
            } }
        public string TenNoiDungUng { get; set; }
        public string CodeCha { get; set; }
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }


    }
}
