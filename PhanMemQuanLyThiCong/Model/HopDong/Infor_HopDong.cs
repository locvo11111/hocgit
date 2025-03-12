using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.HopDong
{
    public class Infor_HopDong
    {
        public string Code { get; set; }
        public string TenHopDong { get; set; }
        public string SoHopDong { get; set; }
        public double GiaTriHopDong { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public DateTime NgayHoanThanh { get; set; }
        public string CodeDonViThucHien { get; set; }
        public string TenGhep
        {
            get
            {
                return $"{TenHopDong} ({SoHopDong})";
            }
        }    
        public string TenGhepSoHopDong
        {
            get
            {
                return $"{SoHopDong}-({Math.Round(GiaTriHopDong/1000000000,2)}Tỷ)";
            }
        }

    }
}
