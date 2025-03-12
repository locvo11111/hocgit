using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class KhoanThu
    {
        public string NoiDungThu { get; set; }
        public string ID { get; set; }
        public string CodeDeXuat { get; set; }
        public double TrangThai { get; set; }
        public string ParentID { get; set; }
        public string GhiChu { get; set; }
        public string FileDinhKem { get; set; }
        public string CongTrinh { get; set; }
        public string NguoiGiao { get; set; }
        public string NguoiNhan { get; set; }
        public bool CheckDaThu { get; set; }
        public string CodeHopDong { get; set; }
        public DateTime NgayThangThucHien { get; set; }
        public double ThucTeThu { get; set; }
        public double TheoThucHien { get; set; }
        //public string CodeHd { get; set; }
        public string ToChucCaNhanNhanChiPhiTamUng { get; set; }
        public bool IsNguonThu { get; set; }
    }
}
