using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class VatLieuHoanThanh
    {
        public int STT { get; set; }
        public double HopDongKl { get; set; }
        public double KhoiLuong { get; set; }
        public double LuyKeYeuCau { get; set; }
        public double KLCL { get { return HopDongKl - LuyKeYeuCau; } }
        public string MaVatTu { get; set; }
        public string KhoDen { get; set; }
        public string KhoDi { get; set; }
        public string TenVatTu { get; set; }
        public string DonVi { get; set; }
        public double ConLai
        {
            get { return (int)Math.Round(100 * ((double)(HopDongKl - LuyKeYeuCau) / HopDongKl),2); }
        }
        public string CodeTDKH { get; set; }
        public string CodeHd { get; set; }
        public string CodeKHVT { get; set; }
        public string Nguon
        {
            get
            {
                if (CodeTDKH != "")
                    return "Kế hoạch vật tư";
                else if (CodeHd != "")
                    return "Hợp đồng";
                else if (CodeKHVT != "")
                    return "Tiến độ kế hoạch";
                else
                    return "Thủ công";
            }
        }
    }
}
