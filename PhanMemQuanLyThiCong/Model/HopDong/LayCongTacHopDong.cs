using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.HopDong
{
    public class LayCongTacHopDong
    {
        public bool Chon { get; set; }
        public double STT { get; set; }
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string CodeCT { get; set; }
        public string MaHieu { get; set; }
        public string TenCongViec { get; set; }
        public string DonVi { get; set; }
        public double KhoiLuongThiCong { get; set; }
        public double KhoiLuongHopDong { get; set; }
        public double KhoiLuongKeHoach { get; set; }
        public double KhoiLuongPS { get; set; }
        public double ThanhTienThiCong { get; set; }
        public double DonGiaHopDong { get; set; }
        public double DonGiaThiCong { get; set; }
        public double DonGiaKeHoach { get; set; }
        public double DonGiaPhatSinh { get; set; }
        public double ThanhTienHopDong { get { return KhoiLuongHopDong * DonGiaHopDong; } }
        //public long ThanhTienHopDong { get; set; }
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }



    }
}
