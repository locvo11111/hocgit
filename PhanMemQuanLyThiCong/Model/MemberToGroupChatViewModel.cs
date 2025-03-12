using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class MemberToGroupChatViewModel
    {
        public Guid? UserId { get; set; }
        public MemberRole MemberRole { get; set; }
        public Guid? GroupChatId { get; set; }
        public string GroupName { get; set; }
    }
}
