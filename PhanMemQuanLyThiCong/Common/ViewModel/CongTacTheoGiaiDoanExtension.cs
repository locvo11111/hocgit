using DevExpress.Accessibility;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class CongTacTheoGiaiDoanExtension : Tbl_TDKH_ChiTietCongTacTheoGiaiDoanViewModel
    {
        public string CodeDanhMucCongTac { get; set; }
        public double? KhoiLuongToanBoNhanThau { get; set; }

        public string TenPhanTuyen { get; set; }

        public string CodeCongTrinh { get; set; }
        public string TenCongTrinh { get; set; }
        public string TenHangMuc { get; set; }
        public string TenDVTH { get; set; }

        public double? KhoiLuongKeHoachHangNgay { get; set; }
        public double? KhoiLuongThiCongHangNgay { get; set; }
        public double? DonGiaThiCongHangNgay { get; set; }
        public double? KhoiLuongDaThiCong { get; set; }
        public double? ThanhTienDaThiCong { get; set; }

        public string NgayKeHoach { get; set; }
        public string NgayThiCong { get; set; }
        public string Ngay { get; set; }

        public int indPT { get; set; }
        public int IndNhom { get; set; }
        public string TenGopCongTac { get; set; }
        public bool DonViTrucThuoc { get; set; }
        public string CodeTongThau { get; set; }

        public double? KhoiLuongDaGiao { get; set; }
        public double KhoiLuongConLai
        {
            get
            {
                return KhoiLuongToanBo - KhoiLuongDaGiao??0;
            }
        }

        public long ThanhTienHopDong
        {
            get
            {
                return (long)Math.Round(KhoiLuongHopDongChiTiet * DonGia);
            }
        }

        public string CodeDVTH
        {
            get
            {
                return CodeNhaThau ?? CodeNhaThauPhu ?? CodeToDoi;
            }
        }

        public DateTime? dtNgay
        {
            get
            {
                if (Ngay.HasValue())
                    return DateTime.Parse(Ngay);
                else
                    return null;
            }
        }

        //public DateTime? dtNgayBatDauThiCong
        //{
        //    get
        //    {
        //        if (MinNgayThiCong.HasValue())
        //            return DateTime.Parse(MinNgayThiCong);
        //        else
        //            return null;
        //    }
        //}

        //public DateTime? dtNgayKetThucThiCong
        //{
        //    get
        //    {
        //        if (MaxNgayThiCong.HasValue())
        //            return DateTime.Parse(MaxNgayThiCong);
        //        else
        //            return null;
        //    }
        //}

        public string CodeDuAn { get; set; }
        public string TenDuAn { get; set; }
        public string Owner { get; set; }
        public string DaiDienDVTH { get; set; }
    }
}
