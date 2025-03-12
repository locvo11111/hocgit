using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VChatCore.Enums.Approval;
//using VChatCore.Model.SyncSqlite;

namespace VChatCore.ViewModels.Approval
{
    public class ApprovalRequestViewModel
    {
        public Guid Id { get; set; }

        public string RequestNote { get; set; }
        public DateTime RequestOn { get; set; }

        public DateTime? CompletedOn { get; set; }
        public ApprovalStateEnum StateId { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }

    }
}
