using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant.Enum
{
    public enum TrangThaiKhoanThuEnum
    {
        [Display(Name = "Chưa gửi duyệt")]
        ChuaGuiDuyet = 1,
        [Display(Name = "Đề xuất")]
        DeXuat = 2,
        [Display(Name = "NguoiDung")]
        NguoiDung = 3,
        [Display(Name = "Đang chờ duyệt")]
        DangChoDuyet = 5

    }
}
