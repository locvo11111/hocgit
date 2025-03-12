using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class InforCT_HM
    {
        public string ID { get; set; }
        public string HangMuc { get; set; }
        public string CongTrinh { get; set; }
        public string DuAn { get; set; }

        public string TenGhep
        {
            get
            {
                return $"{HangMuc} ({CongTrinh})";
            }
        }   
        public string TenGhepDuAn
        {
            get
            {
                return $"{DuAn}({CongTrinh})";
            }
        }
    }
}
