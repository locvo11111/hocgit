using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class ConstructionViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        public decimal? FaceValue { get; set; }
        [MaxLength(256)]
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
