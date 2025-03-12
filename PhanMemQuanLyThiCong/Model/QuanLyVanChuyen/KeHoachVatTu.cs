using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class KeHoachVatTu
    {
        public bool Chon { get; set; }
        public string MaVatLieu { get; set; }
        public string Code{ get; set; }
        public string CodeHangMuc { get; set; }
        public string DonVi { get; set; }
        public string VatTu { get; set; }
        public double KhoiLuongDinhMuc { get; set; }
        public double KhoiLuongKeHoach { get; set; }
        public double DonGiaGoc { get; set; }
        public double DonGiaKeHoach { get; set; }
        public double Index { get; set; }
        public double KhoiLuong
        {
            get
            {
                if (Index == 0)
                    return KhoiLuongDinhMuc;
                else
                    return KhoiLuongKeHoach;
            }
        }      
        public double DonGia
        {
            get
            {
                if (Index == 0)
                    return DonGiaGoc;
                else
                    return DonGiaKeHoach;
            }
        }



    }
}
