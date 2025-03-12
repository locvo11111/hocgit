using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class ContactViewsModel
    {
        public Status StatusCore { get; set; }
        public long ID { get; set; }
        public string UserName { get; set; }
        public Image Avatar { get; set; }
        public DateTime LastActivity { get; set; }

        public DateTime LastOnline { get; set; }
        public string LastOnlineText { get; set; }
        public int UnreadCount { get; set; }

        public bool HasUnreadMessages { get; set; }

        public bool IsInactive { get; set; }
        public enum Status
        {
            Inactive = 0,
            Active = 1
        }
    }
}
