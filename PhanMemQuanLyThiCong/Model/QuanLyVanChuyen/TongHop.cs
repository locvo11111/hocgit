using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class TongHop
    {
        public string ID { get; set; }
        public string CodeCT { get; set; }
        public bool Chon { get; set; }
        public string ParentID { get; set; }
        public int TrangThai { get; set; }
        public string MaVatTu { get; set; }
        public string TenVatTu { get; set; }
        public string HangMuc { get; set; }
        public string DonVi { get; set; }
        public double HopDongKl { get; set; }
        public double TonKhoChuyenDi { get; set; }
        public double TonKhoChuyenDen { get; set; }
        public double LuyKeVanChuyenTheoDot { get; set; }
        public double LuyKeYeuCau { get; set; }
        public double LuyKeNhapTheoDot { get; set; }
        public double LuyKeXuatTheoDot { get; set; }
        public string TenKhoChuyenDi { get; set; }
        public string TenKhoChuyenDen { get; set; }
        public double DonGiaYC { get; set; }
        public double DonGiaNK { get; set; }
        public double DonGiaXK { get; set; }
        public double DonGiaCK { get; set; }
        public double DonGiaVC { get; set; }
        public double KhoiLuongXuatThucTe { get; set; }
        public double TonKhoThucTe { get; set; }
        public double DonGiaKiemSoat { get; set; }
        //public int ThanhTienYC { get { return DonGiaYC * LuyKeYeuCau; } }
        //public int ThanhTienNK { get { return DonGiaNK * LuyKeNhapTheoDot; } }
        //public int ThanhTienXK { get { return DonGiaXK * LuyKeXuatTheoDot; } }
        //public int ThanhTienCK { get { return DonGiaCK * TonKhoChuyenDen; } }
        //public int ThanhTienVC { get { return DonGiaVC * LuyKeVanChuyenTheoDot; } }    
        public double ThanhTienYC { get; set; }
        public double ThanhTienNK { get; set; }
        public double ThanhTienXK { get; set; }
        public double ThanhTienCK { get; set; }
        public double ThanhTienVC { get; set; }
        public string CodeTDKH { get; set; }
        public string CodeHd { get; set; }
        public string CodeKHVT { get; set; }
        public string CodeDeXuat { get; set; }
        public string CodeNhapVT { get; set; }
        public string Nguon
        {
            get
            {
                if (CodeDeXuat != null)
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
                else
                    return "";

            }
        }



    }
}
