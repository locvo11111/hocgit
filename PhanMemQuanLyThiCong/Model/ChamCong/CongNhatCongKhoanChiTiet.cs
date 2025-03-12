using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ChamCong
{
    public class CongNhatCongKhoanChiTiet
    {
        public string Code { get; set; }
        public string CodeNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string MaNhanVien { get; set; }
        public string CodeCongTacTheoGiaiDoan { get; set; }
        public string CodeCongTacThuCong { get; set; }
        public string GioBatDauSang { get; set; }
        public string GioKetThucSang { get; set; }
        public string GioBatDauChieu { get; set; }
        public string GioKetThucChieu { get; set; }
        public string GioBatDauToi { get; set; }
        public string GioKetThucToi { get; set; }
        public double? TongGio { get; set; }
        public double? HeSoSang { get; set; }
        public double? HeSoChieu { get; set; }
        public double? HeSoToi { get; set; }
        public double? TongCong { get; set; }        
        public double? TongGioToi { get; set; }        
        public double? TongGioChieu { get; set; }        
        public double? TongGioSang { get; set; }        
        public double? KhoiLuongToanBo { get; set; }        
        public DateTime? NgayThang { get; set; }
        public string GhiChu { get; set; }
        public string TenCongTac { get; set; }
        public string MaHieuCongTac { get; set; }
        public string DonVi { get; set; }
        public double? KhoiLuong { get; set; }
        public string TenHangMuc { get; set; }
        public string CodeHangMuc { get; set; }
        public string TenCongTrinh { get; set; }
        public string CodeCongTrinh { get; set; }
        public string CodeGiaiDoan { get; set; }
        public string CodeNhaThau { get; set; }
        public string CodeToDoi { get; set; }
        public string CodeNhaThauPhu { get; set; }
        public int TypeRow { get; set; }


    }
}
