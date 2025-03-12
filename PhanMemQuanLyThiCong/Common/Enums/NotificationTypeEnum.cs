using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum NotificationTypeEnum
    {
        [Description("Phân quyền")]
        PERMISSION,

        [Description("Tiến độ")]
        PROGRESS,

        [Description("Duyệt công tác")]
        TASKAPPROVE,

        [Description("Dự án")]
        PROJECT,

        [Description("Giao nhiệm vụ")]
        TASK,

        [Description("Khác")]
        OTHER
    }
}
