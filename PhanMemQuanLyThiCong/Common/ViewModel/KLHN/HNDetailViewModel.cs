using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel.KLHN
{
    public class HNDetailViewModel
    {
        public List<KLHNMainModel> KLHNMain { get; set; } = new List<KLHNMainModel>();
        public List<KLHN> KLHNs { get; set; } = new List<KLHN> { };
        public List<KLHN> KLHNNhapKhos { get; set; } = new List<KLHN> { };
        public List<KLHN> KLHNNhanThaus { get; set; } = new List<KLHN> { };
        public List<DonGiaHN> DonGia { get; set; } = new List<DonGiaHN> { };
    }
}
