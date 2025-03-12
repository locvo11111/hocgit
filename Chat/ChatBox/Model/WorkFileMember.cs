using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class WorkFileMember
    {
        public string Creator { get; set; }
        public string Approver { get; set; }
        public DateTime? DateCreted { get; set; }
        public DateTime? DateApprover { get; set; }
        public string Note { get; set; }
        public List<ManageFileViewModel> manageFileViewModels { get; set; }
    }
}
