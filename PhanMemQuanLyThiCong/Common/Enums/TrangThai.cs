using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum TrangThai
    {
        
        [Display(Name = "Chưa thực hiện")]
        ChuaThucHien,       // = "Chưa thực hiện";
        
        [Display(Name = "Đang thực hiện")]
        DangThucHien,       // = "Đang thực hiện";
        
        [Display(Name = "Đang xét duyệt")]
        DangXetDuyet,       // = "Đang xét duyệt";
        
        [Display(Name = "Đề nghị kiểm tra")]
        DeNghiKiemTra,      // = "Đề nghị kiểm tra";
        
        [Display(Name = "Hoàn thành")]
        HoanThanh,      // = "Hoàn thành";
        
        [Display(Name = "Dừng hoạt động")]
        DungHoatDong,       // = "Dừng hoạt động";
    }
}
