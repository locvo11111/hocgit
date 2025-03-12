using DevExpress.Spreadsheet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class TDKHPastingViewModel
    {
        public bool IsPasting { get; set; } = false;
        public List<string> Codes { get; set; } = new List<string>();
        public List<string> CodesNhom { get; set; } = new List<string>();
        public Cell LastCell { get; set; } = null;
    }
}
