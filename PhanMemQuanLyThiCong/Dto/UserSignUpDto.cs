//using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VChatCore.Dto
{
    public class UserSignUpDto
    {
        public string CoQuan { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string NoiLamViec { get; set; }
        public string PhongLamViec { get; set; }
        public string ViTri { get; set; }
        public string NgaySinh { get; set; }
        //public List<UserRoleDto> UserRoles { get; set; }
    }
}