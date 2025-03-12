using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Enums
{
    public class LoginResponse
    {
        public Guid Id { set; get; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }

        public string Position { get; set; }

        public string WorkAddress { get; set; }

        public string TaxCode { get; set; }

        public string PhoneNumber { get; set; }

        public string Token { get; set; }
    }
}
