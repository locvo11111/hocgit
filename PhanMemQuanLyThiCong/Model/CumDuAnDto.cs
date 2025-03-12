using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class CumDuAnDto
    {
        public Guid Id { get; set; }
        public string STT { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
