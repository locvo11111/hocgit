using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum FileStateEnum
    {
        [Description("1. Chỉ xem")]
        VIEWONLY,
        [Description("2. Chưa duyệt")]
        WAITINGFORAPPROVAL, 
        [Description("3. Đã duyệt")]
        APPROVED
    }

    public enum FileLinkEnum
    {
        FileVatLy,
        FileLink
    }
}
