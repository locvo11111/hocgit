using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel.KLHN
{
    public class KLHNBriefViewModel : ICloneable
    {
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public double? ThanhTienHopDong { get; set; }
        public double? KhoiLuongHopDong { get; set; }


        public double? KLKHInRange { get; set; } = 0;
        public double? KLTCInRange { get; set; } = 0;
        public double? KLBSInRange { get; set; } = 0;
        public double? TTKHInRange { get; set; } = 0;
        public double? TTTCInRange { get; set; } = 0;
        public double? TTBSInRange { get; set; } = 0;

        public double? KLKHInDate { get; set; } = 0;
        public double? KLTCInDate { get; set; } = 0;
        public double? KLBSInDate { get; set; } = 0;
        public double? TTKHInDate { get; set; } = 0;
        public double? TTTCInDate { get; set; } = 0;
        public double? TTBSInDate { get; set; } = 0;


        public double? KLKHFromBeginOfPeriod { get; set; } = 0;
        public double? KLTCFromBeginOfPeriod { get; set; } = 0;
        public double? KLBSFromBeginOfPeriod { get; set; } = 0;
        public double? TTKHFromBeginOfPeriod { get; set; } = 0;
        public double? TTTCFromBeginOfPeriod { get; set; } = 0;
        public double? TTBSFromBeginOfPeriod { get; set; } = 0;

        public double? KLKHFromBeginOfYear { get; set; } = 0;
        public double? KLTCFromBeginOfYear { get; set; } = 0;
        public double? KLBSFromBeginOfYear { get; set; } = 0;
        public double? TTKHFromBeginOfYear { get; set; } = 0;
        public double? TTTCFromBeginOfYear { get; set; } = 0;
        public double? TTBSFromBeginOfYear { get; set; } = 0;


        public double? KLKHFromBeginOfPrj { get; set; } = 0;
        public double? KLTCFromBeginOfPrj { get; set; } = 0;
        public double? KLBSFromBeginOfPrj { get; set; } = 0;
        public double? TTKHFromBeginOfPrj { get; set; } = 0;
        public double? TTTCFromBeginOfPrj { get; set; } = 0;
        public double? TTBSFromBeginOfPrj { get; set; } = 0;

        public double? KLKHSum { get; set; } = 0;
        public double? KLTCSum { get; set; } = 0;
        public double? KLBSSum { get; set; } = 0;
        public double? TTKHSum { get; set; } = 0;
        public double? TTTCSum { get; set; } = 0;
        public double? TTBSSum { get; set; } = 0;

        public double? KLKHOriginal { get; set; } = 0;
        public double? TTKHOriginal { get; set; } = 0;

        public double? InitialInYear { get; set; } = 0;
        public double? InitialInRange { get; set; } = 0;
        public double? InitialFromBeginOfYear { get; set; } = 0;

        public double? DonGiaNgayTinhLuyKe { get; set; }

        public double? GTNTInDate { get; set; }
        public double? GTNTFromBeginOfPrj { get; set; }

        public double? KLTCUntilPrevPeriod { get; set; }

        public DateTime? Ngay { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string CodeDuAn { get; set; }
        public string TenDuAn { get; set; }

        public string TenCongTac { get; set; }
        public string MaCongTac { get; set; }
        public string CodeDonViThucHien { get; set; }
        public string CodeCongTrinh { get; set; }
        public string CodeHangMuc { get; set; }
        public string TenCumDuAn { get; set; }
        public string CodeDVTH { get; set; }


        public double? KLKHFirstDate { get; set; } = 0;
        public double? KLTCFirstDate { get; set; } = 0;
        public double? KLBSFirstDate { get; set; } = 0;
        public double? TTKHFirstDate { get; set; } = 0;
        public double? TTTCFirstDate { get; set; } = 0;
        public double? TTBSFirstDate { get; set; } = 0;
        public double? KLKHLastDate { get; set; } = 0;
        public double? KLTCLastDate { get; set; } = 0;
        public double? KLBSLastDate { get; set; } = 0;
        public double? TTKHLastDate { get; set; } = 0;
        public double? TTTCLastDate { get; set; } = 0;
        public double? TTBSLastDate { get; set; } = 0;
        
        public double? LuyKeKLKHFirstDate { get; set; } = 0;
        public double? LuyKeKLTCFirstDate { get; set; } = 0;
        public double? LuyKeKLBSFirstDate { get; set; } = 0;
        public double? LuyKeTTKHFirstDate { get; set; } = 0;
        public double? LuyKeTTTCFirstDate { get; set; } = 0;
        public double? LuyKeTTBSFirstDate { get; set; } = 0;
        public double? LuyKeKLKHLastDate { get; set; } = 0;
        public double? LuyKeKLTCLastDate { get; set; } = 0;
        public double? LuyKeKLBSLastDate { get; set; } = 0;
        public double? LuyKeTTKHLastDate { get; set; } = 0;
        public double? LuyKeTTTCLastDate { get; set; } = 0;
        public double? LuyKeTTBSLastDate { get; set; } = 0;


        public double? LuyKeKLKHKyTruoc
        {
            get
            {
                return LuyKeKLKHFirstDate - (KLKHFirstDate??0);

            }
        }
        public double? LuyKeKLTCKyTruoc{ get
                {
                return LuyKeKLTCFirstDate - (KLTCFirstDate??0);

            }
        }
        public double? LuyKeKLBSKyTruoc{ get
                {
                return LuyKeKLBSFirstDate - (KLBSFirstDate??0);

            }
        }
        public double? LuyKeTTKHKyTruoc{ get
                {
                return LuyKeTTKHFirstDate - (TTKHFirstDate ?? 0);

            }
        }
        public double? LuyKeTTTCKyTruoc{ get
                {
                return LuyKeTTTCFirstDate - (TTTCFirstDate ?? 0);

            }
        }
        public double? LuyKeTTBSKyTruoc{ get
            {
                return LuyKeTTBSFirstDate - (TTBSFirstDate ?? 0);

            }
        }


        public ProgressStateEnum? CusProgress { get; set; }

        public ReportPeriodTypeEnum periodType { get; set; } = ReportPeriodTypeEnum.FROMSTARTOFPROJECT;


        [JsonIgnore]
        public ProgressStateEnum state
        {
            get
            {
                var LuyKeTTKH = (periodType == ReportPeriodTypeEnum.FROMSTARTOFPROJECT) ? TTKHFromBeginOfPrj : TTKHFromBeginOfPeriod;
                var LuyKeTTTC = (periodType == ReportPeriodTypeEnum.FROMSTARTOFPROJECT) ? TTTCFromBeginOfPrj : TTTCFromBeginOfPeriod;
                if (Ngay < NgayBatDau)
                {
                    if (TTTCFromBeginOfPrj > 0)
                    {
                        return ProgressStateEnum.Nhanh;
                    }
                    else
                    {
                        return ProgressStateEnum.ChuaThucHien;
                    }
                }
                var tl = (TTTCFromBeginOfPrj - TTKHFromBeginOfPrj) / TTKHFromBeginOfPrj;
                double standard = 0.1 / 100.0;
                return (tl > standard) ? ProgressStateEnum.Nhanh :
                    ((tl < -standard) ? ProgressStateEnum.Cham : ProgressStateEnum.Dat);
            }
        }

        public ProgressStateEnum Progress
        {
            get
            {
                if (CusProgress != null)
                {
                    return CusProgress.Value;

                }
                var LuyKeTTKH = (periodType == ReportPeriodTypeEnum.FROMSTARTOFPROJECT) ? TTKHFromBeginOfPrj : TTKHFromBeginOfPeriod;
                var LuyKeTTTC = (periodType == ReportPeriodTypeEnum.FROMSTARTOFPROJECT) ? TTTCFromBeginOfPrj : TTTCFromBeginOfPeriod;
                if (Ngay < NgayBatDau)
                {
                    if (LuyKeTTTC > 0)
                    {
                        return ProgressStateEnum.Nhanh;
                    }
                    else
                    {
                        return ProgressStateEnum.ChuaThucHien;
                    }
                }
                var tl = (LuyKeTTTC - LuyKeTTKH) / LuyKeTTKH;
                double standard = 0.1 / 100.0;
                return (tl > standard) ? ProgressStateEnum.Nhanh :
                    ((tl < -standard) ? ProgressStateEnum.Cham : ProgressStateEnum.Dat);
            }
        }


        public bool NeedCount { get; set; } = false;
        public int CtChamCount { get; set; } = 0;
        public int CtNhanhCount { get; set; } = 0;
        public int CtDatCount { get; set; } = 0;
        public int CtChuaThucHienCount { get; set; } = 0;

        public TypeKLHN Type { get; set; }



        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
