using DevExpress.Xpo;
using VChatCore.ViewModels.SyncSqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class ThongTinCongTacChaExtensionViewModel : Tbl_GiaoViec_CongViecChaViewModel
    {
        public string Chon { get; set; }
        public string ParentCode { get; set; }
        public string TenDonViThucHien { get; set; }
        public string TenDuAn { get;set; }
        public string TenCongTrinh { get; set; }
        public string TenHangMuc { get; set; }
        public string CodeHM { get; set; }
        
        public string CodeDuAn { get;set; }
        public string CodeCongTrinh { get; set; }

        //public string GetCodeDonViThucHien { get; set; }
    }
}
