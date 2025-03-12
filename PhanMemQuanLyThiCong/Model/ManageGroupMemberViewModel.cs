using System;

namespace PhanMemQuanLyThiCong.Model
{
    public class ManageGroupMemberViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? GroupChatId { get; set; }
        public Guid? TaskId { get; set; }
        public string CongViecChaCode { get; set; }
        public string CongViecConCode { get; set; }
        public string Name { get; set; }

        public Guid UserId { get; set; }
        //public virtual AppUserViewModel User { get; set; }

        //public virtual GroupChatViewModel GroupChat { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}