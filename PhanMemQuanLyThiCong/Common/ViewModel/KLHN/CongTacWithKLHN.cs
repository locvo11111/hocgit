using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel.KLHN
{
    public class CongTacWithKLHN
    {
        public List<CongTacTheoGiaiDoanExtension> Cttks { get; set; }
        public List<KLHN> KLHNs { get; set; }
        public List<KLHNBriefViewModel> KLHNBriefs { get; set; }
        public List<MultiKLHNBriefViewModel> KLHNMultiBriefs { get; set; }
    }
}
