using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    public class Chart_Gantt
    {
        public string VatTu { get; set; }//Tên
        public string Name { get; set; }//Tên
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public DateTime? NgayBatDauThiCong { get; set; }
        public DateTime? NgayKetThucThiCong { get; set; }
    }
}
