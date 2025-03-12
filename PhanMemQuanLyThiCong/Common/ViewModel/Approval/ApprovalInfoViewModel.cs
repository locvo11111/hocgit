using System;
using System.IO.Packaging;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace VChatCore.ViewModels.Approval
{
    public class ApprovalInfoViewModel
    {
        public string Id { get; set; }
        public string MaCongTac { get; set; }
        public string TenCongTac { get; set; }
        public double KhoiLuongKeHoach { get; set; }
        public int Level { get; set; }
        public Guid ProcessId { get; set; }
        public DateTime? Time { get; set; }
        public string CodeDuAn { get; set; }
        public string TenDuAn { get; set; }

        [JsonIgnore]
        public string Note { get; set; }
        
        public int MaxApprovedLevel { get; set; } = 0;
        [JsonIgnore]
        public int NumPeopleHaveNotPreApproved
        {
            get { return Level - MaxApprovedLevel - 1; }
        }

        [JsonIgnore]
        public bool IsRootNode { get; set; } = false;

        [JsonIgnore]
        public string ParentId
        {
            get
            {
                return IsRootNode ? null : (NumPeopleHaveNotPreApproved == 0) ? "0" : "1";
            }
        }
    }
}
