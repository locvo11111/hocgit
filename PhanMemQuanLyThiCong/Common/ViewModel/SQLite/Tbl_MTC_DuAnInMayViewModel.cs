using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_MTC_DuAnInMayViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeMay { get; set; }
		public string CodeDuAn { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public double? GiaCaMay { get; set; }
		public double? GiaMuaMay { get; set; }
		public string GhiChu { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
