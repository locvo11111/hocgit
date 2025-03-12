using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel.KLHN
{
    public class KLHN
    {
        public int MyProperty { get; set; }
        public string CodeChaThiCong { get; set; }
        public string Code { get; set; } = Guid.NewGuid().ToString();
        public string ParentCode { get; set; } = Guid.NewGuid().ToString();
        public string CodeCongTacTheoGiaiDoan { get; set; }
        public DateTime Ngay { get; set; }
        public string NhaCungCap { get; set; }
        public string LyTrinhCaoDo { get; set; }
        public int FileCount { get; set; } = 0;
        public double? DonGiaThiCong { get; set; }
        public double? DonGiaKeHoach { get; set; }

        public double? KhoiLuongKeHoach { get; set; }
        public double? KhoiLuongKeHoachGoc { get; set; }
        public double? KhoiLuongThiCong { get; set; }
        public double? KhoiLuongBoSung { get; set; }
        public double? ThanhTienKeHoach { get; set; }
        public double? ThanhTienKeHoachGoc { get; set; }
        public double? ThanhTienThiCong { get; set; }
        public double? ThanhTienBoSung { get; set; }
    
        public double? LuyKeKhoiLuongKeHoach { get; set; }
        public double? LuyKeKhoiLuongKeHoachGoc { get; set; }
        public double? LuyKeKhoiLuongThiCong { get; set; }
        public double? LuyKeKhoiLuongBoSung { get; set; }
        public double? LuyKeThanhTienKeHoach { get; set; }
        public double? LuyKeThanhTienKeHoachGoc { get; set; }
        public double? LuyKeThanhTienThiCong { get; set; }
        public double? LuyKeThanhTienBoSung { get; set; }
        
        public double? LuyKeKhoiLuongKeHoachKyTruoc { 
            get
                {
                return LuyKeKhoiLuongKeHoach - (KhoiLuongKeHoach ?? 0);
            }}
        public double? LuyKeKhoiLuongKeHoachGocKyTruoc { 
            get
                {
                return LuyKeKhoiLuongKeHoachGoc - (KhoiLuongKeHoachGoc ?? 0);
            }}
        public double? LuyKeKhoiLuongThiCongKyTruoc { 
            get
                {
                return LuyKeKhoiLuongThiCong - (KhoiLuongThiCong ?? 0);
            }}
        public double? LuyKeKhoiLuongBoSungKyTruoc { 
            get
                {
                return LuyKeKhoiLuongBoSung - (KhoiLuongBoSung ?? 0);
            }}
        public double? LuyKeThanhTienKeHoachKyTruoc { 
            get
                {
                return LuyKeThanhTienKeHoach - (ThanhTienKeHoach??0);
            }}
        public double? LuyKeThanhTienKeHoachGocKyTruoc { 
            get
                {
                return LuyKeThanhTienKeHoachGoc - (ThanhTienKeHoachGoc??0);
            }}
        public double? LuyKeThanhTienThiCongKyTruoc { 
            get
                {
                return LuyKeThanhTienThiCong - (ThanhTienThiCong ?? 0);
            }
        }
        public double? LuyKeThanhTienBoSungKyTruoc { 
            get
                {
                return LuyKeThanhTienBoSung - (ThanhTienBoSung ?? 0);
            }
        }
        

        public double? KhoiLuongNhapKho { get; set; }

        public bool IsThuCong { get; set; } = false;
        public bool IsEdited { get; set; } = false;
        public string CodeDuAn { get; set; }
        public string TenDuAn { get; set; }

        public string TenCongTac { get; set; }
        public string MaCongTac { get; set; }
        public string CodeDonViThucHien { get; set; }
        public string CodeCongTrinh { get; set; }
        public string CodeHangMuc { get; set; }
        public string TenCumDuAn { get; set; }
        public string CodeDVTH { get; set; }



        public ProgressStateEnum? CusProgress { get; set; }

        public ReportPeriodTypeEnum periodType { get; set; } = ReportPeriodTypeEnum.FROMSTARTOFPROJECT;

        public ProgressStateEnum Progress
        {
            get
            {
                var LuyKeTTTC = LuyKeThanhTienThiCong;
                var LuyKeTTKH = LuyKeThanhTienKeHoach;
                var tl = (LuyKeTTTC - LuyKeTTKH) / LuyKeTTKH;
                double standard = 0.1 / 100.0;
                return (tl > standard) ? ProgressStateEnum.Nhanh :
                    ((tl < -standard) ? ProgressStateEnum.Cham : ProgressStateEnum.Dat);
            }
        }
    }
}
