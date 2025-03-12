using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum MemberRole
    {
        [Description("Người lập")]
        [Display(Name = "Người lập")]
        CREATOR,

        [Description("Người duyệt")]
        [Display(Name = "Người duyệt")]
        APPROVEDBY,

        [Description("Khách")]
        [Display(Name = "Khách")]
        CLINET,
    }
}
