using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class LoaiCongViecViewModel
    {
        public TypeTask Type { get; set; }
        public string Name
        {
            get
            {
                return Type.GetEnumDisplayName();
            }
        }
        public string ColFK
        {
            get
            {
                return Type.GetEnumDescription();
            }
        }
        
        public string Description { get; set; }
    }
}
