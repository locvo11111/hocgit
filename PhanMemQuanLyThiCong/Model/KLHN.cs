using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class KLHN
    {
        public string CodeCha { get; set; }
        public string CodeChaThiCong { get; set; }
        public string CodeHangNgay { get; set; }
        public string CodeCongTacTheoGiaiDoan { get; set; }
        public string Ngay { get; set; }
        public string NhaCungCap { get; set; }
        public double? KhoiLuongKeHoach { get; set; }
        public long? DonGiaKeHoach { get; set; }
        public double? KhoiLuongThiCong { get; set; }
        public long? DonGiaThiCong { get; set; }
        public long? DonGiaThiCongNhanThau { get; set; }

        public long? ThanhTienKeHoach { get; set; }
        public long? ThanhTienThiCong { get; set; }
        public long? ThanhTienThiCongNhanThau { get; set; }

    }
}
