using VChatCore.ViewModels.SyncSqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Profiling.Internal;

namespace PhanMemQuanLyThiCong.Common.ViewModel.SQLite
{
    public class VatTuExtensionViewModel : HPVTExtensionViewModel
    {
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
        public double? KhoiLuongDaThiCongHaoPhi { get; set; } = 0;
        public string NgayBatDauThiCongHaoPhi { get; set; }
        public string NgayKetThucThiCongHaoPhi { get; set; }
        
        public double? KhoiLuongDaNhapKho { get; set; } = 0; //Theo công tác

        public double? KhoiLuongDaThiCongHaoPhiGoc { get; set; } = 0; //Theo công tác
        public string NgayBatDauThiCongHaoPhiGoc { get; set; }
        public string NgayKetThucThiCongHaoPhiGoc { get; set; }
        
        public long? ThanhTienThiCong { get; set; }
        public long? ThanhTienThiCongHaoPhi { get; set; }

        public string Ngay { get; set; }
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

        public DateTime? dtNgayBatDau
        {
            get
            {
                if (NgayBatDau.HasValue())
                    return DateTime.Parse(NgayBatDau);
                else
                    return null;
            }
        }

        public DateTime? dtNgayKetThuc
        {
            get
            {
                if (NgayKetThuc.HasValue())
                    return DateTime.Parse(NgayKetThuc);
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

    }
}
