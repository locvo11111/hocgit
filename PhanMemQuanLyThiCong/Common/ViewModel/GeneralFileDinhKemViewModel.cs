using System;
using VChatCore.ViewModels.SyncSqlite;

namespace VChatCore.ViewModels
{
    public class GeneralFileDinhKemViewModel : Tbl_GiaoViec_FileDinhKemViewModel, ICloneable
    {
		public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
