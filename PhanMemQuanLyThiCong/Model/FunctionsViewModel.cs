using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class FunctionsViewModel
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public int? FuntionTypeId { get; set; }

        public string Url { set; get; }

        public int? ParentId { set; get; }

        public string Icon { get; set; }

        public int DisplayOrder { set; get; }
    }
}
