using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum TypeUpdateDateEnum
    {
        [Description("Ngày bắt đầu")]
        Start,
        [Description("Ngày kết thúc")]
        End,
        [Description("Ngày bắt đầu và ngày kết thúc")]
        Both
    }
}
