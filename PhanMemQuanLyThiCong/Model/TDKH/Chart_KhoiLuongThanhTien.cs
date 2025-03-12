using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class Chart_KhoiLuongThanhTien
    {
        public double TyLe { get; set; }
        public DateTime Date { get; set; }
        public double KhoiLuongThiCong { get; set; }
        public double DonGia { get; set; }
        public string CodeHopDong { get; set; }
        public string ID { get; set; }
        public string Code { get; set; }
        public string CodeCongTac { get; set; }
        public string CodeGoc { get; set; }
        public double KhoiLuongKeHoach { get; set; }
        public double LuyKeKhoiLuongKeHoach { get; set; }
        public double LuyKeKhoiLuongThiCong { get; set; }
        public long ThanhTienThiCong { get; set; }
        public long ThanhTienKeHoach { get; set; }
        public long XuHuongKeHoach { get { return ThanhTienKeHoach; } }
        public long XuHuongThiCong { get { return ThanhTienThiCong; } }
        public long ThanhTienHopDongThu { get; set; }
        public long ThanhTienHopDongChi { get; set; }
        public long LuyKeThanhTienHopDongThu { get; set; }
        public long Thu { get; set; }
        public long Chi { get; set; }
        public long ThuThiCong { get; set; }
        public long ChiThiCong { get; set; }
        public long TamUngThu { get; set; }
        public long TamUngChi { get; set; }
        public long LuyKeThanhTienKeHoach { get; set; }
        public long LuyKeThu { get; set; }
        public long LuyKeChi { get; set; }
        public long LuyKeThanhTienThiCong { get; set; }
        public long LuyKeKeHoachLoiNhuan { get { return (long)Math.Round(LuyKeThanhTienKeHoach * TyLe); } }
        public long LuyKeLoiNhuanTamTinh { get { return (long)Math.Round(LuyKeThanhTienThiCong * TyLe); } }
        public long ThanhTienLoiNhuan { get { return (long)Math.Round(ThanhTienKeHoach * TyLe); } }
        public long ThanhTienTamTinh { get { return (long)Math.Round(ThanhTienThiCong * TyLe); } }
    }
}
