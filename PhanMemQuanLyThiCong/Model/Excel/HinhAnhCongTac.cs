using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.Excel
{
    public class HinhAnhCongTac
    {
        public string TenCongTac { get; set; }
        public string imgDirectory { get; set; }
        public string[] imgNames { get; set; }
        public bool is2Col { get; set; }
    }
}
