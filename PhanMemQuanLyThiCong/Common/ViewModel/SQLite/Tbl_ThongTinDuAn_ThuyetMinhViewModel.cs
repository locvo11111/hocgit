using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ThongTinDuAn_ThuyetMinhViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeParent { get; set; }
		public string FileDinhKem { get; set; }
		public DateTime? Ngay { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public int? State { get; set; }
		public string CodeDuAn { get; set; }
		public string NoiDung { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
