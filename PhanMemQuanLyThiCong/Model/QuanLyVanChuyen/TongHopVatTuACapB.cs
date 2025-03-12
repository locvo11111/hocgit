using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class TongHopVatTuACapB: VatLieu
    {
        public string  Code{ get; set; }
        public string  CodeDot{ get; set; }
        public string  CodeYeuCauVatTu{ get; set; }
        public double KhoiLuongThucHienTrongKy { get; set; }
        public long GiaTriThucNhan { get; set; }
        public string GhiChu { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public double KhoiLuongHopDong { get; set; }
        public long GiaTriHD { get; set; }
    }
}
