using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum TypeApprove
    {
        [Description("Duyệt")]
        [Display(Name = "Duyệt")]
        APPROVE,

        [Description("Chưa duyệt")]
        [Display(Name = "Chưa duyệt")]
        NOTYETAPPROVE,

        [Description("Hủy bỏ")]
        [Display(Name = "Hủy bỏ")]
        CANCEL,
    }
}
