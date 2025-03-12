using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class SearchVatTuViewModel : Tbl_TDKH_KHVT_VatTuViewModel
    {
        public bool Chon { get; set; }
        public string VatTu_KhongDau { get;set; }
    }
}
