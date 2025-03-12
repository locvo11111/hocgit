using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    public class CongTacTheoGiaiDoanExtensionModel : Tbl_TDKH_ChiTietCongTacTheoGiaiDoanViewModel
    {
        public string MaGop { get; set; }
        public string TenGop { get; set; }
        public string TenCongTac { get; set; }
        public string DonVi { get; set; }

        //public string ThanhTienKeHoach { get; set; }
    }
}
