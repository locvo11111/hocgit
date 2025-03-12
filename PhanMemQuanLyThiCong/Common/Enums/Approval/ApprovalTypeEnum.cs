using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VChatCore.Enums.Approval
{
    public enum ApprovalTypeEnum
    {
        [Display(Name = "Công tác giao việc")]
        GiaoViec,
        [Display(Name = "Đề xuất vật tư")]
        YeuCauVatTu,
        [Display(Name = "Nhập kho vật tư")]
        NhapVatTu,
        [Display(Name = "Xuất kho vật tư")]
        XuatVatTu,
        [Display(Name = "Chuyển kho vật tư")]
        ChuyenKho,
        [Display(Name = "Đề xuất tạm ứng thu")]
        KhoanThu,
        [Display(Name = "Đề xuất tạm ứng chi")]
        KhoanChi
    }
}
