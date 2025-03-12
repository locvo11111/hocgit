using System.ComponentModel.DataAnnotations;

namespace VChatCore.Enums.Approval
{
    public enum ApprovalStateEnum
    {
        [Display(Name = "Đang chờ")]
        Pending,
        [Display(Name = "Đang duyệt")]
        InReview,
        [Display(Name = "Đã duyệt")]
        Approved,
        [Display(Name = "Không được duyệt")]
        Rejected,
        [Display(Name = "Đã hoàn thành")]
        Completed
    }
}
