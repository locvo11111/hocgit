using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class CongViecChaChartViewModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string CodeCongViecCha { get; set; }
        public string TenCongViec { get; set; }
        public string MoTa { get; set; }
        public string DVN { get; set; }
        public string DonVi { get; set; }
        public string DVL { get; set; }
        public string CodeDVL { get; set; }
        public string CodeDVN { get; set; }
        public int? SortIdDVL { get; set; }
        public int? SortIdDVN { get; set; }
        public string CodeDuAn { get; set; }
        public double TyLe { get; set; }
        public DateTime? NgayBatDauThiCong { get; set; }
        public DateTime? NgayKetThucThiCong { get; set; }

        public int Total { get; set; } = 0;
        public string ThoiGianThiCong
        {
            get
            {
                if (NgayKetThucThiCong == null)
                    return "";
                else
                    return (1 + (NgayKetThucThiCong - NgayBatDauThiCong).Value.Days).ToString();
            }
        }
        public string NguoiThucHien { get; set; }
    }
}
