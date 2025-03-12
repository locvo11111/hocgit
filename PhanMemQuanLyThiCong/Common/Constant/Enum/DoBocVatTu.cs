using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Constant.Enum
{
    public enum DoBocVatTu
    {
        DoBoc,
        KeHoach,

        [Display(Name = "Vật liệu")]
        VatLieu,

        [Display(Name = "Nhân công")]
        NhanCong,

        [Display(Name = "Máy thi công")]
        MayThiCong,

        AllVatTu
    }
}
