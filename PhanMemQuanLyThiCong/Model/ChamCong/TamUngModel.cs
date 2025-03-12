using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ChamCong
{
    public class TamUngModel: DanhSachNhanVienModel
    {
        public bool Chon { get; set; } = false;
        public string CodeNhanVien { get; set; }
        public string NoiDungTamUng { get; set; }
        public string GhiChu { get; set; }
        public string LoaiNhanVien { get
            {
                if (IsNhanVienCty)
                    return "Nhân viên";
                else
                    return "Không thuộc Cty";

            } }
        public long SoTien { get; set; }
        public int STT { get; set; }
        public DateTime? NgayThangUng { get; set; }
    }
}
