using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class ChangedCodeViewModel
    {
        public string TableName { get; set; }

        public Dictionary<string, string> DicOldNewCode { get; set; } = new Dictionary<string, string>();

        public List<string> IdsNeedDeleted { get; set; } = new List<string>();
    }
}
