using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_TenKhoChungViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string Ten { get; set; }
		public string GhiChu { get; set; }
		public int? SortId { get; set; }
		public bool? Modified { get; set; } = false;
		public bool? ModifiedFromServer { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
