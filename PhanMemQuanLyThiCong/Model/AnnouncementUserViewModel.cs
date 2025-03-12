using PhanMemQuanLyThiCong.Model;
using System;

namespace PhanMemQuanLyThiCong.Model
{
    public class AnnouncementUserViewModel
    {
        public int Id { get; set; }

        public Guid AnnouncementId { get; set; }

        public string UserId { get; set; }

        public bool? HasRead { get; set; }

        public virtual AnnouncementViewModel Announcement { get; set; }
    }
}
