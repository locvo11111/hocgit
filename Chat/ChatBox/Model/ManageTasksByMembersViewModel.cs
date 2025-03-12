using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class ManageTasksByMembersViewModel
    {
        public Guid ConstructionId { get; set; }
        public Guid UserId { get; set; }
        public Guid BusinessId { get; set; }

        public MemberRole MemberRole { get; set; }
    }
}