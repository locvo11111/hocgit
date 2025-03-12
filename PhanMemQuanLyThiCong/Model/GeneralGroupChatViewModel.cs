using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Common.Constant.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Model
{
    public class GeneralGroupChatViewModel : ICloneable
    {

        public Guid? GroupChatId { get; set; }
        public Guid? TaskId { get; set; }
        public string CongViecChaCode { get; set; }
        public string CongViecConCode { get; set; }
        public string CodeDuAn { get; set; }
        public string CodeNhom { get; set; }
        public string CodeCongTacTheoGiaiDoan { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }

        public List<ManageGroupMemberViewModel> MemberChats { get; set; } = new List<ManageGroupMemberViewModel>();
        public string TenCongTrinh { get; set; }
        public string TenDuAn { get; set; }
        public List<RoleDetailViewModel> RoleDetails { get; set; } = new List<RoleDetailViewModel>();

        public string ParentId { get; set; }
        public ChatTypeEnum ChatType { get; set; }

        [JsonIgnore]
        public string IdStr
        {
            get
            {
                return ParentId ?? (GroupChatId != null ? GroupChatId.ToString() :
                    TaskId != null ? TaskId.ToString() : CongViecChaCode ?? CongViecConCode);
            }
        }

        public List<ManageMessageViewModel> messages { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}