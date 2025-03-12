using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum NotificationStateEnum
    {
        [Description("Thông báo mới")]
        NEW,

        [Description("Đã xem từ Popup")]
        READINPOPUP,

        [Description("Đã xem chi tiết")]
        READDETAIL
    }
}
