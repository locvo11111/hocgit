using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ThoiTietViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDuAn { get; set; }
		public string CodeCongTrinh { get; set; }
		public DateTime? Ngay { get; set; }
		public string Sang { get; set; }
		public string Chieu { get; set; }
		public string Toi { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
