using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class MemberChatViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(256)]
        [Required]
        public string FullName { get; set; }
        [MaxLength(550)]
        public string Avatar { get; set; }
        public string AvatarOld { get; set; }
        [MaxLength(256)]
        [Required]
        public string Email { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        public Image Logo { get; set; }
    }
}
