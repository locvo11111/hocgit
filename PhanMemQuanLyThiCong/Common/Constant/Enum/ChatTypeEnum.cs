using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant.Enum
{
    public enum ChatTypeEnum
    {
        [Display(Name = "CodeDuAn")]
        DuAn,
        [Display(Name = "TaskId")]
        GiaoNhiemVu,
        [Display(Name = "CongViecChaCode")]
        GiaoViecCha,
        [Display(Name = "CongViecConCode")]
        GiaoViecCon,
        [Display(Name = "CodeCongTacTheoGiaiDoan")]
        CongTacTheoKy,
        [Display(Name = "CodeNhom")]
        Nhom
    }
}
