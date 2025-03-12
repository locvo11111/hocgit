using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum AccountType
    {
        [Display(Name = "Chủ sở hữu")]
        OWNER = 0,

        [Display(Name = "Nội bộ")]
        INTERNAL = 1,

        [Display(Name = "Khách")]
        EXTERNAL,
        NON
    }
}
