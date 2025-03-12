using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum EnumTrangThai
    {
        [Display(Name = "Chưa thực hiện")]
        CHUATHUCHIEN,

        [Display(Name = "Đang thực hiện")]
        DANGTHUCHIEN,

        [Display(Name = "Đang xét duyệt")]
        DANGXETDUYET,

        [Display(Name = "Đang chỉnh sửa")]
        DANGCHINHSUA,
        
        [Display(Name = "Đề nghị kiểm tra")]
        DENGHIKIEMTRA,

        [Display(Name = "Hoàn thành")]
        HOANTHANH,

        [Display(Name = "Dừng hoạt động")]
        DUNGHOATDONG
    }
}