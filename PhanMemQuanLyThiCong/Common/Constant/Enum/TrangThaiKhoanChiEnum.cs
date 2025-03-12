using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant.Enum
{
    public enum TrangThaiKhoanChiEnum
    {
        [Display(Name = "Chưa gửi duyệt")]
        ChuaGuiDuyet = 1,
        [Display(Name = "Tạm ứng")]
        TamUng = 2,
        [Display(Name = "Giải chi")]
        GiaiChi = 3,
        [Display(Name = "Hoàn thành")]
        HoanThanh = 4,
        [Display(Name = "Đang chờ duyệt")]
        DangChoDuyet = 5


    }
}
