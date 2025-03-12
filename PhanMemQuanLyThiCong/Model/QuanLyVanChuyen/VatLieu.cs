using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class VatLieu
    {
        public string ID { get; set; }
        public string FileDinhKem { get; set; } = "Xem File";
        public bool Chon { get; set; } = false;
        public bool ACapB { get; set; }
        public string ParentID { get; set; }
        public string TenNhaCungCap { get; set; }
        public string CodeHd { get; set; }
        public int TrangThai { get; set; }
        public string MaVatTu { get; set; }
        public string TenVatTu { get; set; }
        public string CodeHangMuc { get; set; }
        public string TenHangMuc { get; set; }
        public string CodeCongTrinh { get; set; }
        public string TenCongTrinh { get; set; }
        public string TenDuAn { get; set; }
        public string CodeDuAn { get; set; }
        public string TenTuyen { get; set; }
        public string CodePhanTuyen { get; set; }
        public string DonVi { get; set; }
        public double YeuCauDotNay { get; set; }
        public double HopDongKl { get; set; }
        public double LuyKeYeuCau { get; set; }
        public double ThanhTien { get { return LuyKeYeuCau * DonGiaHienTruong; } }
        public double LuyKeXuatKho { get; set; }
        public string CodeTDKH { get; set; }
        public string DonViThucHien { get; set; }
        public string CodeKHVT { get; set; }
        public double ConLai
        {
            get { return HopDongKl == 0 ? 0 : Math.Round((double)(100 * LuyKeYeuCau / HopDongKl),2); }
        }
        public double DonGiaHienTruong { get; set; }
        public double SubValue
        {
            get { return HopDongKl - LuyKeYeuCau; }
        }
        public bool IsCompleted
        {
            get
            {
                if (SubValue <=0 && ConLai>=100)
                    return true;
                else
                    return false;
            }
        }
        public string Nguon
        {
            get
            {
                if (ID != null)
                {
                    if (MaVatTu == null)
                        return "Thủ công";
                    if (CodeTDKH != null)
                        return "Kế hoạch vật tư";
                    else if (CodeHd != null)
                        return "Hợp đồng";
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
