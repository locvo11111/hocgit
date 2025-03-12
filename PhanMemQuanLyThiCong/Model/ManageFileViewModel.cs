using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class ManageFileViewModel
    {
        [Required]
        public Guid Id { get; set; }
        public Guid? GroupChatId { get; set; }
        public Guid? TaskId { get; set; }
        public string CongViecChaCode { get; set; }
        public string CongViecConCode { get; set; }
        public Guid UserId { get; set; }
        public Guid BusinessId { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        [Required]
        public string FilePath { get; set; }

        public double FileSize { get; set; }

        [MaxLength(50)]
        public string FileType { get; set; }

        public DateTime? CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        public string Content { get; set; }
    }
}
