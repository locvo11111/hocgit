using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VChatCore.Enums.Approval;

namespace VChatCore.ViewModels.Approval
{
    public class ApprovalTypeViewModel
    {
        public ApprovalTypeEnum Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<ApprovalProcessViewModel> Approvals { get; set; }
    }
}
