using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class RoleManageWorkViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ManageWorkId { get; set; }
        public Guid MemberChatId { get; set; }
        public bool Status { get; set; }
    }
}
