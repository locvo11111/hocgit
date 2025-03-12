using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.MayThiCong
{
    public class MTC_LoaiMayMoc
    {
        public string Code { get; set; }
        public string GhiChu { get; set; }
        public string Ten { get; set; }
        public bool Modified { get; set; }
        public int SortId { get; set; }
        public string LastChange { get; set; }
        public string CreatedOn { get; set; }
    }
}
