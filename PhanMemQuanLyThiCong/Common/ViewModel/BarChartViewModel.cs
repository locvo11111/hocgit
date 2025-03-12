using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class BarChartViewModel: CongViecChaChartViewModel
    {
        public string UID { get; set; }
        public string ParentUID { get; set; }
        public string Name { get; set; }
        public double Progress { get; set; }
        public Color Color { get; set; }
        public string Description { get; set; }
        public int TaskType { get; set; }
        public int SortId { get; set; } = 0;
    }
}
