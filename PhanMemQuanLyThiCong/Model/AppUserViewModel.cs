using DevExpress.XtraRichEdit.Fields;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PhanMemQuanLyThiCong.Model
{
    public class AppUserViewModel
    {
        public AppUserViewModel()
        {
            //Roles = new List<string>();
        }

        public Guid Id { set; get; }
        [Required(ErrorMessage = "Không được để trống họ và tên")]
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        //[Required(ErrorMessage = "Không được để trống giới tính")]
        [DisplayName("Giới tính")]
        public string Gender { get; set; }

        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Không được để trống Email")]
        [EmailAddress(ErrorMessage = "Không phải định dạng Email")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string Avatar { get; set; }

        [DisplayName("Tỉnh thành")]
        public string Province { get; set; }

        [DisplayName("Công ty")]
        public string Company { get; set; }

        [DisplayName("Phòng ban")]
        public string Department { get; set; }

        [DisplayName("Chức vụ")]
        public string Position { get; set; }

        [DisplayName("Địa chỉ làm việc")]
        public string WorkAddress { get; set; }

        [DisplayName("Mã số thuế")]
        public string TaxCode { get; set; }
        [Required(ErrorMessage = "Không được để trống số điện thoại")]

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Không được để trống tên tài khoản")]
        [DisplayName("Tài khoản")]
        public string UserName { get; set; }

        public Status Status { get; set; } = Status.Active;

        [Required(ErrorMessage = "Không được để trống mật khẩu")]
        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp nhau.")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Ngày tạo")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public CategoryEnum Category { get; set; }
        //public List<KeyStoreViewModel> KeyStores { get; set; }
        //public List<OrderViewModel> Orders { get; set; }
        //public List<string> Roles { get; set; }
        public string SerialNo { get; set; }
        public AccountType AccountTypeId { get; set; } = AccountType.NON;

        public string AccountTypeName
        {
            get 
            {
                return EnumHelper.GetEnumDisplayName(AccountTypeId); 
            }
        }

        public string DisplayInCombobox
        {
            get
            {
                return $"{AccountTypeName}:  \t\t\t{FullName}  \t\t\t({Email})";
            }
        }

        public bool IsAdmin { get; set; } = false;

        public Guid UserInContractorId { get; set; }
        public bool HaveInitProjectPermission { get; set; }

    }
}