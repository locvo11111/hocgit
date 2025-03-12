using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class NoteApproveWorks
    {
        public Guid BussId { get; set; }
        public DateTime? DateApprove { get; set; }
        public string Note { get; set; }
        public TypeApprove TypeApprove { get; set; }
    }
}
