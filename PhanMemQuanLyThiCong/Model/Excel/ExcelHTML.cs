using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.Excel
{
    public class ExcelHTML
    {
        public string ten { get; set; }
        public string Encode { get; set; }
        public MemoryStream stream { get; set; }
        public Encoding ecd { get; set; }
    }
}
