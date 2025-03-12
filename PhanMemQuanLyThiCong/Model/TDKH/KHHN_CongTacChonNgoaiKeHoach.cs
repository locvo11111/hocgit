using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    [Serializable,XmlRoot("CongTacChonNgoaiKeHoach")]
    public class KHHN_CongTacChonNgoaiKeHoach
    {
        [XmlElement("CodeCongTacTheoGiaiDoan")]
        public List<string> CodeCongTacTheoGiaiDoan { get; set; }
        [XmlElement("PhanLoaiND")]
        public string IdNguoiDung { get; set; }
        [XmlElement("PhanLoaiDuAn")]
        public string CodeDuAn { get; set; }      
        [XmlElement("CodeDonViThucHien")]
        public string CodeDVTH { get; set; }
    }
}
