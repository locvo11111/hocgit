using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.HopDong
{
    public class TongHopHopDong
    {
        public string Code { get; set; }
        public string CodeNhaThau { get; set; }
        public string CodeDonViThucHien { get; set; }
        public string CodeBenGiao { get; set; }
        public string ParentCode { get; set; }
        public string TrangThai { get; set; }
        public string LoaiHopDong { get; set; }
        public string CodeHopDong { get; set; }
        public string CodeDuAn { get; set; }
        public string TenDuAn { get; set; }
        public string ThenFile { get; set; }
        public string TenHopDong { get; set; }
        public string SoHopDong { get; set; }
        public string HopDongMe { get; set; }
        public string HopDongCon { get; set; }
        public string Edit { get; set; }
        public string XemTruoc { get; set; }
        public string Xoa { get; set; }
        public long GiaTriHopDong { get; set; }
        public long SoTienDaTamUng { get; set; }
        public long SoTienThanhToan { get; set; }
        public long SanLuongThiCong { get; set; }
        public long SoTienThucNhan { get ; set; }
        public long SoTienConLai { get { return SanLuongThiCong - SoTienDaTamUng;}  }
        public long TienBaoHanh { get; set; }
        public long TienBaoLanh { get; set; }
        public DateTime ThoiGianBaoLanh { get; set; }
        public DateTime ThoiGianBaoHanh { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public long STT { get; set; }
        public bool HDChinh { get { return CodeBenGiao == CodeNhaThau ? false : true; } }
    }
}
