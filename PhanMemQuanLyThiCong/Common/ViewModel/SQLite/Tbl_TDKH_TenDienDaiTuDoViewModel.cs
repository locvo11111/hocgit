using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_TenDienDaiTuDoViewModel : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
        public string Code { get; set; }
        public string Ten { get; set; }
        public string GhiChu { get; set; }
        public string CodeDuAn { get; set; }
        public int? SortId { get; set; }
        public bool ModifiedFromServer { get; set; } = false;
        public bool? Modified { get; set; } = false;
        public string CodeDienDaiCha { get; set; }
        public DateTime? LastChange { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
