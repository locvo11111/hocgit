using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum PaymentMethod
    {
        [Description("Thanh toán khi giao hàng (COD)")]
        [Display(Name = "Thanh toán khi giao hàng (COD)")]
        CashOnDelivery,

        [Description("Chuyển khoản")]
        [Display(Name = "Chuyển khoản")]
        OnlineBanking,

        [Description("Cổng thanh toán")]
        [Display(Name = "Cổng thanh toán")]
        PaymentGateway,

        [Description("Visa")]
        [Display(Name = "Visa")]
        Visa,

        [Description("Master Card")]
        [Display(Name = "Master Card")]
        MasterCard,

        [Description("PayPal")]
        [Display(Name = "PayPal")]
        PayPal,

        [Description("Atm")]
        [Display(Name = "Atm")]
        Atm,

        [Description("Tất cả")]
        [Display(Name = "Tất cả")]
        All
    }
}