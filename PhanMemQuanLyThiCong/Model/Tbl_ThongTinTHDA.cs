using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class Tbl_ThongTinTHDA
    {
        public int Code { get; set; }
        public string SaveBy { get; set; }
        public string Version { get; set; }
        public string CreatedOn { get; set; }
        public string LastChange { get; set; }
        public string OriginalVersion { get; set; }
        public string Modified { get; set; }
        public string CreateBySerialNo { get; set; }
        public string LastMigrationVersion { get; set; }
    }
}
