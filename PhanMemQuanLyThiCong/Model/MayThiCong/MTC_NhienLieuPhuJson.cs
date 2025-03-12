using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.MayThiCong
{
    public class MTC_NhienLieuPhuJson
    {
        //public string Code { get; set; }
        //[JsonIgnore]
        public string DonVi { get; set; }
        //[JsonIgnore]
        public string Ten { get; set; }
        public double? MucTieuThu { get; set; }
        //public string CodeChiTiet { get; set; }
        public string CodeNhienLieu { get; set; }
        //[JsonIgnore]
        public int? STT { get; set; }
    }
}
