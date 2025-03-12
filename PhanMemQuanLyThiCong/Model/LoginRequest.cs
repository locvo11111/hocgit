using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhanMemQuanLyThiCong.Common.Enums;

namespace PhanMemQuanLyThiCong.Model
{
    public class LoginRequest
    { 
        [Required(ErrorMessage = "Không được để trống tên tài khoản")]
        [DisplayName("Tên người dùng/Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Không được để trống mật khẩu")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [DisplayName("Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; } = false;

        public string LoginProvider { get; set; } = nameof(LoginProviderEnum.WINFORM);

        public string Name { get; set; } = "TBT360";
    }
}
