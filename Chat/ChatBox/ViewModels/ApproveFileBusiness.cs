using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.ViewModels
{
    public class ApproveFileBusiness
    {
        public Guid BusinessId { get; set; }
        public IEnumerable<ManageFileViewModel> ManageFiles { get; set; }
        public NoteApproveWorks NoteApproveWorks { get; set; }
        public int TypeAction { get; set; } /// 1 là duyệt file vs 0 là gửi lại file để duyệt//
    }
}
