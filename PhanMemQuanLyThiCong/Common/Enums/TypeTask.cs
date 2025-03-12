using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum TypeTask
    {
        //[Display(Name = "Giao việc nhanh")]
        //[Description("TaskId")]
        //TASK,

        [Display(Name = "Giao việc dự án")]
        [Description("CongViecChaCode")]
        GIAOVIECDUAN,

        [Display(Name = "Giao việc thi công")]
        [Description("CongViecChaCode")]
        GIAOVIECTHICONG,

        [Display(Name = "Giao việc tiến độ kế hoạch")]
        [Description("CongViecTheoGiaiDoanCode")]
        TIENDOKEHOACH
    }
}