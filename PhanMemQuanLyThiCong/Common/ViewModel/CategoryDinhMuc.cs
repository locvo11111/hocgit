using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class CategoryDinhMuc
    {
        public bool Checked { get; set; }
        [Key]
        public int ID { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

    }
}
