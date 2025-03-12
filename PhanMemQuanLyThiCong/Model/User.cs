//using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VChatCore.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public string Code { get; set; }
        public string CoQuan { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Gender { get; set; }
        public string NoiLamViec { get; set; }
        public string PhongLamViec { get; set; }
        public string ViTri { get; set; }
        public string NgaySinh { get; set; }
        public DateTime? LastLogin { get; set; }
        public string CurrentSession { get; set; }
        public byte[] AvatarArr { get; set; }

        //public virtual ICollection<GroupUser> GroupUsers { get; set; }

    }
}