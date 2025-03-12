using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ChamCong
{
    public class HeSoChamCong
    {
        public string  Code { get; set; }
        public string  CodeDuAn { get; set; }
        /// <summary>
        /// 1:Kỷ thuật hiện trường
        /// 2:Nhân viên văn phòng 
        /// 3:Công nhật lao động tự do
        /// 4:Công khoán
        /// </summary>
        public int  LoaiCaiDat { get; set; }
        public double  Thu7 { get; set; }
        public double  ChuNhat { get; set; }
        public double NgayLe { get; set; }
        public double TangCaThu7 { get; set; }
        public double TangCaChuNhat { get; set; }
        public double TangCaNgayLe { get; set; }
        public double TangCaNgayTuan { get; set; }
        public double BuoiSang { get; set; }
        public double BuoiChieu { get; set; }
        public double BuoiToi { get; set; }
        public bool? Modified { get; set; }
        public string CreatedOn { get; set; }
        public string LastChange { get; set; }
    }
}
