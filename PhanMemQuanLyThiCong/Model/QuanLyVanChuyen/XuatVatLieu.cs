using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class XuatVatLieu
    {
        public string ID { get; set; }
        public bool ACapB { get; set; }
        public string FileDinhKem { get; set; }
        public string CodeDeXuat { get; set; }
        public string CodeNhapVT { get; set; }
        public string TenKhoNhap { get; set; }
        public bool Chon { get; set; }
        public bool IsXuat { get; set; }
        public string ParentID { get; set; }
        public int TrangThai { get; set; }
        public string MaVatTu { get; set; }
        public string TenVatTu { get; set; }
        public string DonVi { get; set; }
        public double DaDuyetDeXuat { get; set; }
        public double LuyKeNhapTheoDot { get; set; }
        public double LuyKeXuatTheoDot { get; set; }
        public double ThucXuat { get; set; }
        public double DonGiaKiemSoat { get; set; }
        public double KhoiLuongXuatThucTe { get; set; }
        public double TonKhoThucTe { get; set; }
        public string CodeTDKH { get; set; }
        public string CodeHd { get; set; }
        public string CodeKHVT { get; set; }
        public string TraVatTu { get; set; }
        public double KhoiLuongTra { get; set; }
        public string Nguon
        {
            get
            {
                if (CodeDeXuat != null)
                {
                    if (CodeTDKH != "")
                        return  "Kế hoạch vật tư";
                    else if (CodeHd != "")
                        return  "Hợp đồng";
                    else if (CodeKHVT != "")
                        return  "Tiến độ kế hoạch";
                    else
                        return "Thủ công";
                }
                else
                    return "";

            }
        }
        public double DonGia { get; set; }
        public double ThanhTien { get { return DonGia * LuyKeXuatTheoDot; } }
    }
}
