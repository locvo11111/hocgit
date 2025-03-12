using System.Collections.Generic;

namespace VChatCore.ViewModels.Approval
{
    public class UpdateProcessArgs
    {
        public ApprovalArgs ApprovalArgs { get; set; }
        public List<ApprovalProcessViewModel> Processes { get; set; }
    }
}
