using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant.Enum
{
    public enum VatTuStateEnum
    {
        [Display(Name = "Đang xét duyệt")]
        DangXetDuyet = 1,
        [Display(Name = "Đã duyệt")]
        DaDuyet = 2
    }
}
