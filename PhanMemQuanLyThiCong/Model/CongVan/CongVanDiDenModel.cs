using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.CongVan
{
    public class CongVanDiDenModel
    {
        public int STT{ get; set; }
        public string Code{ get; set; }
        public string CodeDVPH { get; set; }
        public string CodeDuAn { get; set; }
        public string MaHoSo{ get; set; }
        public string TenHoSo{ get; set; }
        public string NguoiKy{ get; set; }
        public string DonViPhatHanh{ get; set; }
        public string Kieu{ get; set; }
        public string LoaiCongVan{ get; set; }
        public string LyDoMuon{ get; set; }
        public string DaMuon{ get; set; }
        public string DuAn{ get; set; }
        public string TrangThai{ get; set; }
        public string SoanThao{ get; set; }
        public string GhiChu{ get; set; }
        public string FileDinhKem { get; set; } = "File đính kèm";
        public DateTime? NgayNhan{ get; set; }
        public DateTime? NgayMuon{ get; set; }
    }
}
