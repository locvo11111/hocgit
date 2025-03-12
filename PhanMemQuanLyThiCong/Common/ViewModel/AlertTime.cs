using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class AlertTime
    {
        public int STT { get; set; }
        public int Id { get; set; }
        public string Time { get; set; }
        public DateTime? TimeParse
        {
            get
            {
                return null; 
            }
        }
    }
}
