using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class NhapVatLieu
    {
        public string ID { get; set; }
        public bool ACapB { get; set; }
        public string FileDinhKem { get; set; } = "Xem File";
        public string CodeDeXuat { get; set; }
        public string TenKhoNhap { get; set; }
        public string TenNhaCungCap { get; set; }
        public string DonViThucHien { get; set; }
        public bool Chon { get; set; }
        public bool IsXuat { get; set; }
        public string ParentID { get; set; }
        public int TrangThai { get; set; }
        public string MaVatTu { get; set; }
        public string TenVatTu { get; set; }
        public string DonVi { get; set; }
        public bool DaThanhToan { get; set; }
        public double DeXuatVatTu { get; set; }
        public double DaDuyetDeXuat { get; set; }
        public double LuyKeNhapTheoDot { get; set; }
        public double ThucNhap { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien { get { return DonGia * LuyKeNhapTheoDot; } }
        public string CodeTDKH { get; set; }
        public string CodeHd { get; set; }
        public string CodeKHVT { get; set; }
        public string TraVatTu { get; set; }
        public double KhoiLuongTra { get; set; }
        public string Nguon{
            get
            {
                if (CodeDeXuat != null)
                {
                    if (CodeTDKH != null)
                        return "Kế hoạch vật tư";
                    else if (CodeHd != null)
                        return "Hợp đồng";      
                    else if (TrangThai==4)
                        return "Chuyển từ kho khác";
                    else if (CodeKHVT !=null)
                        return "Tiến độ kế hoạch";
                    else
                        return "Thủ công";
                }
                else
                    return "";

            }
        }

    }
}
