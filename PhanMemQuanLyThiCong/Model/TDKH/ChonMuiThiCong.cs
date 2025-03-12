using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    public class ChonMuiThiCong
    {
        public string Code { get; set; }
        public string Ten { get; set; }
        public string GhiChu { get; set; }
        public string CodeCongTac { get; set; }
        public int SortId { get; set; }
        public bool Chon { get; set; } = false;
    }
}
