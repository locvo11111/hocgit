using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_TenGopCongTacViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string Ten { get; set; }
		public string DienGiai { get; set; }
		public string CodeDuAn { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
