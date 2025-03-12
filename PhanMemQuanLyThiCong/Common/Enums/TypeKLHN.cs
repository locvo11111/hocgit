using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum TypeKLHN
    {
        
        GiaoViecCha,
        GiaoViecCon,
        HaoPhiGiaoViecCon,
        HaoPhiGiaoViecCha,
        [Description("TempWorkload.Group")]
        [Display(Name = "Tbl_TDKH_NhomCongTac")]
        Nhom,
        [Description("TempWorkload.Job")]
        [Display(Name = "Tbl_TDKH_ChiTietCongTacTheoGiaiDoan")]
        CongTac,
        [Description("TempWorkload.Material")]
        [Display(Name = "Tbl_TDKH_KHVT_VatTu")]
        VatLieu,
        HaoPhiVatTu,
        All
    }
}
