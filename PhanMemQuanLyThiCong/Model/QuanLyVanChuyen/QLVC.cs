using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class QLVC
    {
        public string NhatKy { get; set; }
        public string FileDinhKem { get; set; }
        public string ID { get; set; }
        public bool Chon { get; set; }
        public string ParentID { get; set; }
        public string HoanThoanh_Ok { get; set; }
        public string CodeDeXuat { get; set; }
        public string CodeNhapVT { get; set; }
        public string MaVatTu { get; set; }
        public string TenVatTu { get; set; }
        public double DonGia { get; set; }
        public string DonVi { get; set; }
        public double DaDuyetDeXuat { get; set; }
        public double LuyKeVanChuyenTheoDot { get; set; }
        public double TongSoLuongChuyen { get; set; }
        public double KhoiLuong_1Chuyen { get; set; }
        public double KhoiLuongDaNhap { get; set; }
        public double ThucTeVanChuyen
        {
            get { return TongSoLuongChuyen * KhoiLuong_1Chuyen; } 
        }
        public double LuyKeNhapKho { get {return ThucTeVanChuyen + KhoiLuongDaNhap;}}
        public string KichThuocThungXe { get; set; }
        public string BienSoXe { get; set; }
        public string TaiXe { get; set; }
        public string CodeTDKH { get; set; }
        public string CodeHd { get; set; }
        public string CodeKHVT { get; set; }
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
        public void GenerateSource(QLVC Soure)
        {
            Soure.TaiXe = "";
            Soure.TongSoLuongChuyen = 0;
            Soure.BienSoXe = "";
            Soure.KichThuocThungXe = "";
            Soure.KhoiLuong_1Chuyen = 0;
        }

    }
}
