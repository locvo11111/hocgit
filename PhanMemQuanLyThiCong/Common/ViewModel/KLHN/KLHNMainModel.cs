using DevExpress.Pdf.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel.KLHN
{
    public class KLHNMainModel
    {
        public string Code { get; set; }
        public string CodeCongTacTheoGiaiDoan { get; set; }
        public string CodeThiCong { get; set; }
        public double? KhoiLuongKeHoach { get; set; } = 0;
        public long? DonGiaKeHoach { get; set; } = 0;
        public long? DonGiaThiCong { get; set; } = 0;
        public DateTime? NgayNghi { get; set; }
        public string NgayNghiTrongTuan { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string CodeHangMuc { get; set; }
        public string CodeCongTrinh { get; set; }
        public string CodeDuAn { get; set; }
        public string TenCongTac { get; set; }
        public string MaCongTac { get; set; }

        //public double KLKHThuCongLK { get; set; }
        public double KLTCFromBeginOfPrj { get; set; }
        public double TTTCFromBeginOfPrj { get; set; }
        public double KLBSFromBeginOfPrj { get; set; }
        public double TTBSFromBeginOfPrj { get; set; }

        public double KLKHThuCong { get; set; } = 0;
        public int CountNgayThuCong { get; set; } = 0;
        
        public double KLKHThuCongLK { get; set; } = 0;
        public int CountNgayThuCongLK { get; set; } = 0;
        public DateTime? DateCalcLuyKe { get; set; }
    }
}
