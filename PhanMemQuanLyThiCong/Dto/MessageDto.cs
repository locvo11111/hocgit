//using Microsoft.AspNetCore.Http;
using DevExpress.DevAV.Chat.Model;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace VChatCore.Dto
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
        public UserDto UserCreatedBy { get; set; }
        public bool isOwner { get; set; }
        public string UserName { get; set; }
        public Image ImgText { get; set; }

        public Image Avatar { get; set; }
        //public List<IFormFile> Attachments { get; set; }
    }
}
