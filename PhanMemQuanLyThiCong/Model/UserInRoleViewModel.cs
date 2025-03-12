using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class UserInRoleViewModel
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string RoleName { get; set; } = "";
        public FieldUpdateEnum FieldUpdate { get; set; } = FieldUpdateEnum.None;
    }
}
