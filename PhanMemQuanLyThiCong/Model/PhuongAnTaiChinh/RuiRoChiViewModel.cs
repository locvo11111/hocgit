using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.PhuongAnTaiChinh
{
    public enum PhanTichRuiRoEnum
    {
        CoThe,
        KhongThe,
        KhongCan
    }
    public class RuiRoChiViewModel
    {
        public PhanTichRuiRoEnum Type { get; set; } = PhanTichRuiRoEnum.KhongThe;//0 Có thể
        public List<string> RuiRos { get; set; } = new List<string>();
    }

}
