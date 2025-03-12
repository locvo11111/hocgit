using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public enum WorkType
    {
        [Display(Name ="Chưa thi công")]
        
        DONOT,
        [Display(Name = "Đang thi công")]
        DO,
        [Display(Name = "Hoàn thành")]
        COMPLETE
    }
}
