using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum OrderStatus
    {
        [Description("Xóa")]
        [Display(Name = "Xóa")]
        Delete = -1,
        [Description("Tạo nháp (Dùng thử)")]
        [Display(Name = "Tạo nháp (Dùng thử)")]
        Created,
        [Description("Chờ xác nhận")]
        [Display(Name = "Chờ xác nhận",Order =1)]
        Confirmed,
        [Description("Cập nhật lại thông tin")]
        [Display(Name = "Cập nhật lại thông tin")]
        FeedBack,
        [Description("Xác nhận đơn hàng")]
        [Display(Name = "Xác nhận đơn hàng")]
        InProgress,
        [Description("Đang giao hàng")]
        [Display(Name = "Đang giao hàng")]
        InShipCode,
        [Description("Hoàn thành")]
        [Display(Name = "Hoàn thành")]
        Success,
        [Description("Hủy đơn")]
        [Display(Name = "Hủy đơn")]
        Canceled,
        [Description("Khóa mềm có phí")]
        [Display(Name = "Khóa mềm có phí")]
        PaidSoftKey,
        [Description("Tất cả")]
        [Display(Name = "Tất cả")]
        All,
    }
}