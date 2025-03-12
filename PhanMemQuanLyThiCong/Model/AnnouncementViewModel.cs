using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Model
{
    public class AnnouncementViewModel
    {
        public Guid AnnouncementId { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { set; get; }

        public string AvatarMember { set; get; }
        public string MemberName { set; get; }

        [StringLength(250)]
        public string Content { set; get; }

        public string FileType { get; set; }
        public string Namefile { get; set; }
        public string Sizefile { get; set; }
        public string Time { get; set; }
        public string GroupName { get; set; }
        public FileTypeEnum IsType { get; set; } = FileTypeEnum.MESSAGE;
    }
}