using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class HPVTExtensionViewModel : HaoPhiVatTuExtensionViewModel
    {
        public string CodeDot { get; set; }
        public string CodeVatTuNew { get; set; }
        public string CodeGiaoThau { get; set; }
        public string CodeThiCong { get; set; }
        public string CodeNhaThau { get; set; }
        public string CodeNhaThauPhu { get; set; }
        public string CodeToDoi { get; set; }
        public string CodeGiaiDoan { get; set; }
        public string TenPhanTuyen { get; set; }
        public string CodeDonViThucHien { get; set; }
        public double KhoiLuongThiCong { get; set; }
        public double KhoiLuongHopDong { get; set; }
        public double KhoiLuongHopDongVatTu { get; set; }

        public string MaHieuCongTac { get; set; }
        //public string CodeHangMuc { get; set; }
        //public string CodeVatTu { get; set; }
        public string TenHangMuc { get; set; }
        public string CodeCongTrinh { get; set; }
        public string TenCongTrinh { get; set; }
        public string TenCongTac { get; set; }
        public string CodeCongTacTheoKy { get; set; }
        public double KhoiLuongToanBo { get; set; }
        public long? DonGiaThiCongVatTu { get; set; }
        public double? KhoiLuongDaThiCong { get; set; } = 0;
        public double? KhoiLuongDaThiCongVatTu { get; set; } = 0;
        public DateTime? NgayBatDauThiCongVatTu { get; set; }
        public DateTime? NgayKetThucThiCongVatTu { get; set; }
        public DateTime? NgayBatDauVatTu { get; set; }
        public DateTime? NgayKetThucVatTu { get; set; }

        public double? KhoiLuongDaNhapKho { get; set; } = 0; //Theo công tác

        public double? KhoiLuongDaThiCongHaoPhiGoc { get; set; } = 0; //Theo công tác
        public string NgayBatDauThiCongHaoPhiGoc { get; set; }
        public string NgayKetThucThiCongHaoPhiGoc { get; set; }

        public long? ThanhTienThiCong { get; set; }
        public long? ThanhTienThiCongVatTu { get; set; }

        public double? KLTCHPHN { get; set; }
        public double? KLTCVTHN { get; set; }
        public long? DGTCVTHN { get; set; }
        public long? DGTCHPHN { get; set; }
        public double? KLKHHPHN { get; set; }
        public double? KLKHVTHN { get; set; }




        public DateTime? Ngay { get; set; }
/*        public DateTime? dtNgay
        {
            get
            {
                if (Ngay.HasValue())
                    return DateTime.Parse(Ngay);
                else
                    return null;
            }
        }*/

/*        public DateTime? dtNgayBatDau
        {
            get
            {
                if (NgayBatDau.HasValue())
                    return DateTime.Parse(NgayBatDau);
                else
                    return null;
            }
        }*/

/*        public DateTime? dtNgayKetThuc
        {
            get
            {
                if (NgayKetThuc.HasValue())
                    return DateTime.Parse(NgayKetThuc);
                else
                    return null;
            }
        }*/
/*        public DateTime? dtNgayBatDauVatTu
        {
            get
            {
                if (NgayBatDauVatTu.HasValue())
                    return DateTime.Parse(NgayBatDauVatTu);
                else
                    return null;
            }
        }

        public DateTime? dtNgayKetThucVatTu
        {
            get
            {
                if (NgayKetThucVatTu.HasValue())
                    return DateTime.Parse(NgayKetThucVatTu);
                else
                    return null;
            }
        }*/

        public string SearchString
        {
            get { return $"{MaVatLieu};{MaTXHienTruong};{VatTu};{DonVi};{DonGia};{LoaiVatTu};{CodeGiaiDoan};{CodeHangMuc};{CodePhanTuyen}"; }
        }
    }
}
