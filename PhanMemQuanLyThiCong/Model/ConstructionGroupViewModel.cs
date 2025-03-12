using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class ConstructionGroupViewModel
    {
        /// <summary>
        /// Phần của công trình
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public decimal? FaceValue { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        public string Phone { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Phần tên nhóm chát
        /// </summary>
        public Guid GroupId { get; set; }

        [Required]
        [MaxLength(256)]
        public string GroupChatName { get; set; }

        public string AvatarGroup { get; set; }

        /// <summary>
        /// Thành viên nhóm chat người lập ra nhóm đó ///
        /// </summary>
        public Guid UserId { get; set; }

        public MemberRole MemberRole { get; set; } = MemberRole.CLINET;
        public string MemberAvatar { get; set; }
        public string MemberConnectionId { get; set; }
    }
}
