using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class MergeResult
    {
        public Guid Id { get; set; }

        [MaxLength(10)]
        public string TypeSql { get; set; }
    }
}
