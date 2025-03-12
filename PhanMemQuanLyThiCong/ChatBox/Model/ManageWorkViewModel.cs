using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class ManageWorkViewModel
    {
        public Guid Id { get; set; }
        public Guid ConstructionId { get; set; }
        public Guid UserId { get; set; }
        public Guid? GroupChatId { get; set; }
        public Guid? TaskId { get; set; }
        public string CongViecChaCode { get; set; }
        public string CongViecConCode { get; set; }
        public string GroupName { get; set; }
        public DateTime? CreateDate { get; set; }

        [MaxLength(20)]
        public string Stt { get; set; }

        [MaxLength(1000)]
        public string NameBusiness { get; set; }

        [MaxLength(500)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string ContentCreation { get; set; }

        [MaxLength(1000)]
        public string ContentBrowsing { get; set; }
        public string TenHangMuc { get; set; }

        public DateTime? ApproveDate { get; set; }
        public TypeApprove TypeApprove { get; set; }
        public string Unit { get; set; }
        public double? KhoiLuongHD { get; set; }
        public double? KhoiLuongTT { get; set; }
        public double? TyLeHoanThanh { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public bool Status { get; set; }
        public string Note { get; set; }
        public DateTime? StartDayReality { get; set; }
        public DateTime? EndDayReality { get; set; }
        public WorkType WorkType { get; set; }
        public int? TotalFile { get; set; }

        public int workTypeInt
        {
            get { return (int)WorkType; }
            set { WorkType = (WorkType)value; }
        }

        public virtual List<ManageFileViewModel> ManageFiles { get; set; }
        public virtual List<ManageTasksByMembersViewModel> ManageTasksByMembers { get; set; }
    }
}
