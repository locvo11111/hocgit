using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class Infor
    {
        public string Code { get; set; }
        public string Ten { get; set; }
        public string Decription { get; set; }
        public string ColCode { get; set; }
        public string CodeHD { get; set; }
        public string TenGhep
        {
            get
            {
                return $"{Ten} ({Decription})";
            }
        }

    }

}
