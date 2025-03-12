using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class UpDownTableSqlite
    {
        public string tbl { get; set; }
        public object Data { get; set; }
        public List<string> CodesDAs { get; set; }
        public bool CheckedChangeOnly { get; set; }
        public List<Tbl_DeletedRecoredViewModel> DeletedRecored { get; set; }
    }
}
