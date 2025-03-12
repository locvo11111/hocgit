using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VChatCore.Enums.Approval;
//using VChatCore.Model.SyncSqlite;

namespace VChatCore.ViewModels.Approval
{
    public class ApprovalDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid ApprovalRequestId { get; set; }
        public Guid ApprovalProcessId { get; set; }

        public string CodeCongViecCha { get; set; }
        public string CodeDeXuatVatTu { get; set; }
        public string CodeNhapVatTu { get; set; }
        public string CodeXuatVatTu { get; set; }
        public string CodeChuyenKho { get; set; }


        public string FullNameApprover { get; set; }
        public DateTime ApprovedOn { get; set; }
        public ApprovalStateEnum StateId { get; set; }
        public string ApprovalNote { get; set; }

    }
}
