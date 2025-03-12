using DevExpress.Data.Mask.Internal;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Model
{
    public class FileSQLiteViewModel : Tbl_GiaoViec_FileDinhKemViewModel
    {
        public string ParentCode
        {
            get
            {
                return ParentCodeCustom ?? (((Link is null) ? nameof(FileLinkEnum.FileVatLy) : nameof(FileLinkEnum.FileLink)) + $"_{StateString}");
            }
        }
        public bool Checked { get; set; } = false;
        
        public string StateString
        {
            get
            {
                return ((FileStateEnum)State).GetEnumName();
            }
        }

        public string ParentCodeCustom { get; set; }

    }
}
