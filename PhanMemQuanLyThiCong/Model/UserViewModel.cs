using DevExpress.XtraEditors.Filtering.Templates;

namespace PhanMemQuanLyThiCong.Model
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FullName { set; get; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Avatar { get; set; }

        public int Status { get; set; } = 1;

        public string Gender { get; set; }

        public string DateOfBirth { get; set; }

        public string Company { get; set; } // Công ty

        public string Department { get; set; } // Phòng ban

        public string Position { get; set; } // Chức vụ

        public string TaxCode { get; set; } // Mã số thuế

        public string WorkAddress { get; set; }

        public string DateCreated { get; set; }
      
        //public byte[] AvatarArr { get; set; }
    }
}