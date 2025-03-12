using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class AppUserUpdateViewModel
    {
        public Guid Id { set; get; }
        public string FullName { get; set; }


        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }


        public string Email { get; set; }

        public string Address { get; set; }


        public string Company { get; set; }


        public string Department { get; set; }

  
        public string Position { get; set; }

        public string TaxCode { get; set; }

        public string PhoneNumber { get; set; }
        public bool HaveInitProjectPermission { get; set; }
    }
}
