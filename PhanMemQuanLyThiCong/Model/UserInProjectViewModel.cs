using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class UserInProjectViewModel
    {
        public Guid UserId { get; set; }
        public string ProjectId { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string ProjectName { get; set; } = "";
        public FieldUpdateEnum FieldUpdate { get; set; } = FieldUpdateEnum.None;
    }
}
