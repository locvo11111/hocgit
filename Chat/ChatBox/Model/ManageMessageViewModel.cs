using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class ManageMessageViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? GroupChatId { get; set; }
        public Guid? TaskId { get; set; }
        public Guid UserId { get; set; }
        public string CongViecChaCode { get; set; }
        public string CongViecConCode { get; set; }
        public string GroupName { get; set; }

        [JsonIgnore]
        public string AvatarMember { set; get; }
        public string MemberName { get; set; }
        public string Avatar { get; set; }
        [JsonIgnore]
        public Image LogoTemp { get; set; }
        [JsonIgnore]
        public Image Logo { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }
        public byte[] FileContent { get; set; }

        [MaxLength(500)]
        public string FileName { get; set; }

        [MaxLength(1000)]
        public string FilePath { get; set; }

        [MaxLength(10)]
        public string FileType { get; set; }

        public FileTypeEnum IsType { get; set; } = FileTypeEnum.MESSAGE;
        public float Size { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public bool IsOwner { get; set; } = true;
        [JsonIgnore]
        public Image ImgText { get; set; }
        [JsonIgnore]
        public virtual List<IFormFile> ListFiles { get; set; }

        [JsonIgnore]
        public string IdStr
        {
            get
            {
                return GroupChatId != null ? GroupChatId.ToString() :
                    TaskId != null ? TaskId.ToString() : CongViecChaCode ?? CongViecConCode;
            }
        }

        [JsonIgnore]
        public string Time
        {
            get
            {
                if (Created == DateTime.MinValue)
                {
                    return string.Empty;
                }
                return GetTimeOrDate();
            }
        }


        private string GetTimeOrDate()
        {
            TimeSpan timeSpan = Created.Subtract(CreateDate);
            if (timeSpan.TotalMinutes < 1.0)
            {
                return "Vừa xong";
            }

            if (timeSpan.TotalMinutes < 60.0)
            {
                return (int)(0.5 + timeSpan.TotalMinutes) + " phút trước";
            }

            if (timeSpan.TotalMinutes < 90.0)
            {
                return "1 giờ trước";
            }

            if (timeSpan.TotalMinutes < 360.0)
            {
                return (int)(timeSpan.TotalMinutes / 60.0 + 0.5) + " giờ trước";
            }

            if (timeSpan.TotalMinutes < 1440.0)
            {
                return CreateDate.ToString("hh:mm");
            }

            if (timeSpan.TotalMinutes < 2880.0)
            {
                return "Ngày hôm qua " + CreateDate.ToShortTimeString();
            }

            return CreateDate.ToString("dd/MM/yyyy hh:mm");
        }


    }
}