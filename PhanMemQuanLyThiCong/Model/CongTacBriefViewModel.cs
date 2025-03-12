using PhanMemQuanLyThiCong.Constant.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{   
    public class CongTacBriefViewModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string MaDinhMuc { get; set; }
        public int? TrinhTuThiCong { get; set; }
        public string Name { get; set; }
        public string CodeHM_DVN { get; set; }//Code Hạng mục, Đầu việc nhỏ
        public string TenHM_DVN { get; set; }//Tên Hạng mục, Đầu việc nhỏ
        public string CodeCT_DVL { get; set; }//Code Công trình, Đầu việc lớn
        public string TenCT_DVL { get; set; }//Tên Công trình, Đầu việc lớn
    }
}
