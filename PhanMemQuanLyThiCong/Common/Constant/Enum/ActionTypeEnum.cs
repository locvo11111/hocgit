using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant.Enum
{
    public enum ActionTypeEnum
    {
        [Display(Name = "Tạo mới")]
        ADD,

        [Display(Name = "Chỉnh sửa thông tin")]
        UPDATE,

        [Display(Name = "Thông tin")]
        VIEW
    }
}
