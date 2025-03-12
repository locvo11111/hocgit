using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum TypeStatus
    {
        [Description("Tất cả")]
        [Display(Name = "Tất cả")]
        ALL = 1,
        [Description("Khóa mềm")]
        [Display(Name = "Khóa mềm")]
        KHOAMEM = 2,

        [Description("Khóa cứng")]
        [Display(Name = "Khóa cứng")]
        KHOACUNG = 3
    }
}