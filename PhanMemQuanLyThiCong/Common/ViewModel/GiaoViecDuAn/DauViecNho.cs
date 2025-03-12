using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Common.ViewModel.GiaoViecDuAn
{
    public class DauViecNho: Tbl_GiaoViec_DauViecNhoViewModel
    {
        public List<Tbl_GiaoViec_CongViecChaViewModel> CongViecChas { get; set; }
    }
}
