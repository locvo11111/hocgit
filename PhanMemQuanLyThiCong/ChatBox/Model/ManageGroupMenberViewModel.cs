using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class ManageGroupMenberViewModel : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        /// <summary>
        /// public Guid Id { get; set; } = Guid.NewGuid();
        /// </summary>

        public Guid UserId { get; set; }
        public Guid ConstructionId { get; set; }
        public string MemberName { get; set; }
        public string Avatar { get; set; }
        public string AvararMember { get; set; }
        public Image Avartar { get; set; }
        public string MemberEmail { get; set; }
        public Guid GroupChatId { get; set; }
        public string ConstructionName { get; set; }
        public string ConstructionAddress { get; set; }
        public string GroupChatName { get; set; }
        public MemberRole MemberRole { get; set; }
        public int? UnseenMessages { get; set; }
        public string RoleGroupText {
            get
            {                                           
                if (MemberRole == MemberRole.CREATOR)
                    return "Người tạo";
                else if (MemberRole == MemberRole.APPROVEDBY)
                    return "Người duyệt";
                else return "Thành viên";
            }
        }

        [Browsable(false)]
        public DateTime LastOnline { get; internal set; }

        public string LastOnlineText
        {
            get
            {
                return "Online";

                if (LastOnline == DateTime.MinValue)
                {
                    return string.Empty;
                }

                TimeSpan timeSpan = DateTime.Now.Subtract(LastOnline);
                if (timeSpan.TotalMinutes < 1.0)
                {
                    return "Just now";
                }

                if (timeSpan.TotalMinutes < 60.0)
                {
                    return (int)(0.5 + timeSpan.TotalMinutes) + " minutes ago";
                }

                if (timeSpan.TotalMinutes < 90.0)
                {
                    return "1 hour ago";
                }

                if (timeSpan.TotalMinutes < 360.0)
                {
                    return (int)(timeSpan.TotalMinutes / 60.0 + 0.5) + " hours ago";
                }

                if (timeSpan.TotalMinutes < 1440.0)
                {
                    return LastOnline.ToShortTimeString();
                }

                if (timeSpan.TotalMinutes < 2880.0)
                {
                    return "Yesterday, " + LastOnline.ToShortTimeString();
                }

                return LastOnline.ToShortDateString();
            }
        }

        public int UnreadCount {
            get { return Messages.Count; }  
        }

        [Browsable(false)]
        public bool HasUnreadMessages => UnreadCount > 0;

        public List<string> Messages { get; internal set; } = new List<string>();
    }
}
