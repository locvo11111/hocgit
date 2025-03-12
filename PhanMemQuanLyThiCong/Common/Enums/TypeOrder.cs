using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum TypeOrder
    {
        [Display(Name = "Chưa dùng")]
        Unworn,

        [Display(Name = "Đang dùng")]
        Use,

        [Display(Name = "Cấp lại")]
        Renew,

        [Display(Name = "Hỏng")]
        Fail,

        [Display(Name = "Mất")]
        Lose,

        [Display(Name = "Hoàn đơn")]
        Return
    }
}
