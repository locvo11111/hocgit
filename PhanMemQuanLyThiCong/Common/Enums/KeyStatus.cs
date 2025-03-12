using DevExpress.DevAV;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum KeyStatus
    {
        [Display(Name = "Xóa", Order = 5)]
        Delete = -1,

        [Display(Name = "Chưa kích hoạt", Order = 2)]
        InActive,

        [Display(Name = "Kích hoạt", Order = 3)]
        Active,

        [Display(Name = "Khóa", Order = 4)]
        Lock,

        [Display(Name = "Tất cả", Order = 1)]
        All,
    }

}
