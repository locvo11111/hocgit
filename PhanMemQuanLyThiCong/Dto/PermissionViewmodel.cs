using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Dto
{
    public class PermissionViewModel
    {
        public string FunctionId { get; set; }

        public Guid RoleId { get; set; }

        public string CommandId { get; set; }

        public virtual FunctionViewModel Fcn { get; set; }

        public virtual AppRoleViewModel AppRole { get; set; }

        public virtual CommandViewModel Cmd { get; set; }
    }
}
