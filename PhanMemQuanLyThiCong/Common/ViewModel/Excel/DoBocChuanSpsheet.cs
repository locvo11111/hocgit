using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel.Excel
{
    public class DoBocChuanSpsheet
    {
        public bool Choose { get; set; }
        public string STT { get; set; }
        public int? ThuTuCongTac { get; set; }
        public string MaHieuCongTac { get; set; }
        public string DanhMucCongTac { get; set; }
        public string DonVi { get; set; }
        public int? SoBoPhanGiongNhau { get; set; }
        public double? Dai { get; set; }
        public double? Rong { get; set; }
        public double? Cao { get; set; }
        public double? HeSoCauKien { get; set; }
        public double? KhoiLuong1BP { get; set; }
        public double? KhoiLuongToanBo { get; set; }
        public double? LuyKeDaThucHien { get; set; }

    }
}
