using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Model;

namespace PhanMemQuanLyThiCong.Model
{
    public class OrderViewModel
    {
        public int Id { set; get; }
        [Required(ErrorMessage ="Bắt buộc nhập trường này!")]
        [MaxLength(256,ErrorMessage ="Quá 256 ký tự!")]
        public string CustumerName { set; get; }

        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        [MaxLength(256, ErrorMessage = "Quá 256 ký tự!")]
        public string CustumerAddress { set; get; }

        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        [MaxLength(256, ErrorMessage = "Quá 256 ký tự!")]
        public string CustumerEmail { set; get; }

        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        [MaxLength(256, ErrorMessage = "Quá 50 ký tự!")]
        public string CustumerPhoneNumber { set; get; }

        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        [MaxLength(256, ErrorMessage = "Quá 256 ký tự!")]
        public string CustumerProvince { set; get; } // Tỉnh thành

        [MaxLength(256, ErrorMessage = "Quá 256 ký tự!")]
        public string CustumerMessage { set; get; } // Note khách hàng

        [MaxLength(256, ErrorMessage = "Quá 256 ký tự!")]
        public string BillCode { get; set; } // Mã hóa đơn

        [MaxLength(256, ErrorMessage = "Quá 256 ký tự!")]
        public string Company { get; set; } // Công ty

        public string Department { get; set; } // Phòng ban

        [MaxLength(256, ErrorMessage = "Quá 256 ký tự!")]
        public string Position { get; set; } // Chức vụ

        [MaxLength(256, ErrorMessage = "Quá 256 ký tự!")]
        public string TaxCode { get; set; } // Mã số thuế
        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        public int ProductCategoryId { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        public PaymentMethod PaymentMethod { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        public OrderStatus PaymentStatus { set; get; }
        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        public DateTime OrderDate { set; get; } =DateTime.Now;
        public Status Status { set; get; }
        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        public Guid UserId { set; get; }
        public Guid? CustumerId { set; get; }
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? AmountPaid { get; set; } = 0;
        [Column(TypeName = "decimal(18,0)")]
        public decimal? Discount { get; set; } = 0;
        [Column(TypeName = "decimal(18,0)")]
        public decimal? TotalMoney { get; set; } = 0;
        public string Note { get; set; }
        //Khi người dùng check vào dùng thử trên from//
        public bool? CheckTrial { get; set; }
        public bool? Updatekey { get; set; }
        public bool IsSignature { get; set; } = false;
        [Required(ErrorMessage = "Bắt buộc nhập trường này!")]
        [Column(TypeName = "decimal(18,0)")]
        public decimal SoftwareMoney { get; set; } = 0;
        [Column(TypeName = "decimal(18,0)")]
        public decimal OtherMoney { get; set; } = 0;
        public virtual AppUserViewModel User { get; set; }
        public virtual AppUserViewModel Custumer { get; set; }
        public virtual ProductCategoryViewModel ProductCategory { get; set; }
        public List<OrderDetailViewModel> OrderDetails { set; get; } = new List<OrderDetailViewModel>();        
    }
}