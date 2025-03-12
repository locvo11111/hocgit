using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum TypeContractor
    {
        [Display(Name = "Tự thực hiện")]
        [Description("CodeNhaThauPhu")]
        SELF,

        [Display(Name = "Nhà thầu")]
        [Description("CodeNhaThau")]
        NHATHAU,

        [Display(Name = "Nhà thầu phụ")]
        [Description("CodeNhaThauPhu")]
        NHATHAUPHU,

        [Display(Name = "Tổ đội thi công")]
        [Description("CodeToDoi")]
        TODOITHICONG,
    }
}
