using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class ChuyenVatTu
    {
        public string ID { get; set; }
        public string CodeNhapVT { get; set; }
        public string CodeDeXuat { get; set; }
        public string TenKhoChuyenDi { get; set; }
        public string TenKhoChuyenDen { get; set; }
        public string ChuyenKho { get; set; }
        public bool HoanThanh { get; set; }
        public string ParentID { get; set; }
        public string MaVatTu { get; set; }
        public string TenVatTu { get; set; }
        public long DonGia { get; set; }
        public string DonVi { get; set; }
        public double TonKhoChuyenDi { get; set; }
        //public int ThucXuatChuyenKho { get; set; }
        //public int LuyKeKhoChuyenDiTheoDot { get { return ThucXuatChuyenKho; } set { LuyKeKhoChuyenDiTheoDot=value; } }
        public double ThucNhapKhoDen { get; set; }
        public double LuyKeThucNhapKhoDen { get; set; }
        //public int LuyKeKhoChuyenDenTheoDot { get { return ThucNhapKhoDen; } set { LuyKeKhoChuyenDenTheoDot = value; } }
        public double TonKhoChuyenDen { get { return ThucNhapKhoDen+ LuyKeThucNhapKhoDen; } }
        public string CodeTDKH { get; set; }
        public string CodeHd { get; set; }
        public string CodeKHVT { get; set; }
        public string Nguon
        {
            get
            {
                if (CodeNhapVT != null)
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
