using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VChatCore.Enums.Approval;
//using VChatCore.Model.SyncSqlite;

namespace VChatCore.ViewModels.Approval
{
    public class ApprovalProcessViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public ApprovalTypeEnum ApprovalTypeId { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string CodeDuAn { get; set; }
        public string Description { get; set; }

    }
}
