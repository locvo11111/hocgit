using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Common.ViewModel.GiaoViecDuAn
{
    public class DauViecLon : Tbl_GiaoViec_DauViecLonViewModel
    {
        public List<DauViecNho> DauViecNhos { get; set; }
    }
}
