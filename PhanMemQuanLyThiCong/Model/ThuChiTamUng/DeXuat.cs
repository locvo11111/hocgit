using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class DeXuat
    {

        public string NoiDungUng { get; set; }
        public string Code { get; set; }
        public string CodeKC { get; set; }
        public string CodeQLVCYCVT { get; set; }
        public string CodeQLVCNVT { get; set; }
        public string CodeNhaThauPhu { get; set; }
        public string XoaSua { get; set; } = "Xóa";
        public string CodeToDoi { get; set; }
        public string CodeNhaThau { get; set; }
        public string CodeNhaCungCap { get; set; }
        public string CodeNhanVien { get; set; }
        public long GiaTriDotNay { get; set; }
        public double SoTienDaSuDung { get; set; }
        public double SoTienDaGiaiChi { get; set; }
        public int TrangThai { get; set; }
        public int NguonThuChi { get; set; }
        public string GhiChu { get; set; }
        public string ChonChiTiet { get; set; } = "Chọn chi tiết";
        public string File { get; set; } = "Xem chi tiết";
        public string LoaiKinhPhi { get; set; }
        public string CongTrinh { get; set; }
        public string NguoiLapTamUng { get; set; }
        public string FileDinhKem { get; set; }
        public string CodeHd { get; set; }
        public string CodeCongTacTheoGiaiDoan { get; set; }   
        public string ToChucCaNhanNhanChiPhiTamUng { get; set; }
        //public string ToChucCaNhanNhanChiPhiTamUng { get {
        //        if (CodeNhaCungCap != null)
        //            return CodeNhaCungCap;
        //        else if (CodeNhaThau != null)
        //            return CodeNhaThau;
        //        else if (CodeToDoi != null)
        //            return CodeToDoi;
        //        else
        //            return CodeNhaThauPhu;


        //    } set { value; } }
        public bool Chon { get; set; }
        public bool IsEdit { get; set; }
        public bool IsVanChuyen { get; set; }
        public bool IsTDKH { get; set; }
        //public bool IsEdit { get; set; }
        public bool GuiDuyet { get; set; }
        public bool IsVatLieu { get; set; }
    }
}
