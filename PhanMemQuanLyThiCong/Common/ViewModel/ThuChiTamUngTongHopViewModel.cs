using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class ThuChiTamUngTongHopViewModel
    {
        public Tbl_TCTU_DeXuatViewModel DeXuat { get; set; }
        public Tbl_TCTU_KhoanThuViewModel KhoanThu { get; set; }
        public Tbl_TCTU_KhoanChiViewModel KhoanChi { get; set; }
        public List<Tbl_TCTU_KhoanChiChiTietViewModel> KhoanChiChiTiets { get; set; }
    }
}
