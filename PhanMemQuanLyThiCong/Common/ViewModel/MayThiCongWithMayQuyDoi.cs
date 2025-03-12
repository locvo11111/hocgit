using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class MayThiCongWithMayQuyDoi : HaoPhiVatTuExtensionViewModel
    {
        public string CodeMayQuyDoi { get; set; } 
        public string MayQuyDoi { get; set; }
        public string No { get; set; }
        public string TrangThai { get; set; }
        public string CodeChuSoHuu { get; set; }
        public string CodeNhaThau { get; set; }
        public string CodeNhaThauPhu { get; set; }
        public string CodeToDoi { get; set; }
        public string GhiChu { get; set; }
        public double? GiaMuaMay { get; set; }
        public DateTime? NgayMuaMay { get; set; }
        public string UrlImage { get; set; }
        public string DonViMayQuyDoi { get; set; }
        public double? GiaCaMay { get; set; }
        public double? HaoPhi { get; set; }
        public string TrangThaiMayQuyDoi { get; set; }


    }
}
