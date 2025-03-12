using VChatCore.ViewModels.SyncSqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit.Fields;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class ThongTinDuAnExtensionViewModel : Tbl_ThongTinDuAnViewModel
    {
        public bool Chon { get; set; } = false;
        
        public DateTime? LastSyncInSQLite { get; set; } = null;

        public bool IsValidDownload
        {
            get
            {
                if (!IsExistInSQL)
                    return true; 

                if (!BaseTime.HasValue)
                    return true;
                else if (!LastSyncInSQLite.HasValue || BaseTime > LastSyncInSQLite) 
                    return false;

                return true;
            }
        }

        public bool IsExistInSQL = false;
        public Guid OwnerCode { get; set; }
        public DateTime? LastTimeSyncToServer { get; set; } = null;
        public string LastSyncToServerBy { get; set; } = null;



    }
}
