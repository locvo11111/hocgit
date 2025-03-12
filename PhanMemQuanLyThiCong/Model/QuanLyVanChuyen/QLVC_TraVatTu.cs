using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class QLVC_TraVatTu
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string TenKhoTra { get; set; }
        public double KhoiLuong { get; set; }
        public DateTime Ngay { get; set; }
        public double DonGia { get; set; }
        public long ThanhTien { get
            {
                return (long)Math.Round(KhoiLuong * DonGia,0);
            } }
        public string CodeNhapVatTu { get; set; }
        public string CodeXuatVatTu { get; set; }
        public string TenVatTu { get; set; }
        public string DonVi { get; set; }
        public string CodeHangMuc { get; set; }
        public string CodeCongTrinh { get; set; }
        public string TenHangMuc { get; set; }
        public string TenCongTrinh { get; set; }
        public string NguonPhatSinh { 
            get {
                if (!string.IsNullOrEmpty(CodeXuatVatTu))
                    return "Xuất Kho";
                else
                    return "Nhập Kho";
            } }



    }
}
