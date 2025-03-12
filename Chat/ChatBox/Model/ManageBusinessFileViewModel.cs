using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class ManageBusinessFileViewModel
    {
        public Guid Id { get; set; }
        public Guid ConstructionId { get; set; }
        public string GroupName { get; set; }
        public Guid UserId { get; set; }
        public string NameBusiness { get; set; }
        public string Stt { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FileTotal { get; set; }
        public int FileApprove { get; set; }
        public TypeApprove TypeApprove { get; set; }
    }
}