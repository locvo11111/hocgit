using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class ManageWorkFollowFile
    {
        public Guid BusinessId { get; set; }
        public string ContentCreation { get; set; }

        public List<ManageTasksByMembersViewModel> manageTasksByMembers { get; set; }
        public List<ManageFileViewModel> manageFiles { get; set; }
    }
}
