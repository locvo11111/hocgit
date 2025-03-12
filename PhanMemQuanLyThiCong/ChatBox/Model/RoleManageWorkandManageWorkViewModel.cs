using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class RoleManageWorkandManageWorkViewModel
    {
        public ManageWorkViewModel manageWorkViewModel { get; set; }
        public RoleManageWorkViewModel roleManageWorkView { get; set; }
        //
        public IEnumerable<ManageFileViewModel> fileViewModels { get; set; }
        public IEnumerable<ManageTasksByMembersViewModel> manageTasksByMembers { get; set; }
    }
}
