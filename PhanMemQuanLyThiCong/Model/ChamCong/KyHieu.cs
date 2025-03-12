using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ChamCong
{
    public class KyHieu
    {
        public string Code { get; set; }
        public string NghiOm { get; set; }
        public string HocTap { get; set; }
        public string NghiDe { get; set; }
        public string ChamCong { get; set; }
        public string NghiPhep { get; set; }
        public string CongTac { get; set; }
        public string NghiTheoCheDo { get; set; }
        public string NghiThu7ChuNhat { get; set; }
        public string NghiKhongLyDo { get; set; }
        public int PhanTramNghiOm { get; set; }
        public int PhanTramHocTap { get; set; }
        public int PhanTramNghiDe { get; set; }
        public int PhanTramNghiPhep { get; set; }
        public int PhanTramChamCong { get; set; }
        public int PhanTramCongTac { get; set; }
        public int PhanTramNghiKhongLyDo { get; set; }
        public int PhanTramNghiTheoCheDo { get; set; }
        public int PhanTramNghiThu7ChuNhat { get; set; }
        public double BaoHiem { get; set; }
        public double Thue { get; set; }


    }
}
