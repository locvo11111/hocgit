using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_KinhPhiDuKienViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeGiaiDoan { get; set; }
		public string CodeNhaThau { get; set; }
		public string CodeToDoi { get; set; }
		public long KinhPhiDuKien { get; set; } = 0;
		public string CodeNhaThauPhu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
