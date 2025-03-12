using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public enum CategoryEnum
    {
        [Display(Name = "Hệ thống")]
        SYSTEM = 2,
        [Display(Name = "Khách hàng")]
        CUSTOMER = 3,
        [Display(Name = "Tất cả")]
        ALL = 1,
    }
}
