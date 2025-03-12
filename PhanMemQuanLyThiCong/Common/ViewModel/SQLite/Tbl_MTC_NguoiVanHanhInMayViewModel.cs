using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_MTC_NguoiVanHanhInMayViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeMay { get; set; }
		public string CodeNguoiVanHanh { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
