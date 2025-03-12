using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant.Enum
{
    public enum TrangThaiDeXuatEnum
    {
        [Display(Name = "Chưa gửi duyệt")]
        ChuaGuiDuyet = 1,
        [Display(Name = "Đã duyệt")]
        DaDuyet = 2,   
        [Display(Name = "Đang chờ duyệt")]
        ChoDuyet = 5
    }
}
    