using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum SubscriptionTypeEnum
    {
        [Display(Name = "Cơ bản")]
        INDIVIDUAL = 1,

        [Display(Name = "Doanh nghiệp")]
        BUSINESS,

        [Display(Name = "Chuyên nghiệp")]
        PROFESSIONAL,

        [Display(Name = "Nâng cao")]
        SPECIAL,
    }
}
