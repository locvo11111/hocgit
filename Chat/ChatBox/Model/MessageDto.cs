using DevExpress.DevAV.Chat.Model;
using DevExpress.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class MessageDto
    {
        public Contact Contact { get; set; }

        public long Id { get; set; }
        public long Code { get; set; }
        public string Type { get; set; }
        public string GroupCode { get; set; }
        public string Content { get; set; }
        public string Path { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string SendTo { get; set; }
       // public UserDto UserCreatedBy { get; set; }
        public bool isOwner { get; set; }
        public string UserName { get; set; }
        public Image ImgText { get; set; }
        public bool IsImg { get; set; } = false;
        public bool IsFile { get; set; } = false;
        public Image Avatar { get; set; }
    }
}
