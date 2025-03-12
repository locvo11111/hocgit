using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class TCTU_TDKH
    {
        public bool Chon { get; set; }
        public bool IsVatLieu { get; set; }
        public double STT { get; set; }
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string CodeCT { get; set; }
        public string MaHieu { get; set; }
        public string TenCongViec { get; set; }
        public string DonVi { get; set; }
        public string LoaiCT { get; set; }
        public double KhoiLuongThiCong { get; set; }
        public double KhoiLuongKeHoach { get; set; }
        public double KhoiLuongHopDong { get; set; }
        public double LuyKeYeuCau { get; set; }
        public double LuyKeXuatKho { get; set; }
        public double ConLai
        {
            get { return KhoiLuongHopDong == 0 ? 0 : Math.Round((double)(100 * LuyKeYeuCau / KhoiLuongHopDong), 2); }
        }
        public double DonGia { get; set; }
        public long ThanhTienKeHoach { get; set; }
        public long ThanhTienThiCong { get; set; }
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }


    }
}
