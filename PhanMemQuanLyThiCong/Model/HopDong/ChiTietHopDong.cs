using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.HopDong
{
    public enum TypeThuChi
    {
        HopDongThu,
        HopDongChi,
        ThuThucTe,
        ChiThucTe,
        TamUngThu,
        TamUngChi
    }
    public class ChiTietHopDong
    {
        public string Code { get; set; }
        public string GiaTri { get; set; }
        public double GiaTriHopDong { get; set; }
        public string CodeHopDong { get; set; }
        public string Ngay { get; set; }
        public int Loai { get; set; }
        public int STT { get; set; }
        public bool TheoThang { get; set; }
        public bool IsPhanTram { get; set; }
        public string GiaTriCal
        {
            get
            {
                if (IsPhanTram)
                    return $"{Math.Round(GiaTriHopDong * double.Parse(GiaTri) / 100, 0)} (Chiếm {GiaTri}%)";
                else
                    return GiaTri;
            }
        }
        public string SoTien
        {
            get
            {
                if (GiaTriCal == null)
                    return "Lấy theo kế hoạch";
                else
                    return GiaTriCal;
            }
        }
        public long SoTienCal
        {
            get
            {
                if (IsPhanTram)
                    return (long)Math.Round(GiaTriHopDong * double.Parse(GiaTri) / 100);
                else if (!IsPhanTram && (Loai < 2||Loai>=3))
                    return long.Parse(GiaTri);
                else
                    return 0;
            }
        }
        public string NgayThucHien
        {
            get
            {
                if (Loai == 2&&TheoThang)
                    return $"Ngày {Ngay} hằng tháng";
                else
                    return Ngay;
            }
        }

    }
}
