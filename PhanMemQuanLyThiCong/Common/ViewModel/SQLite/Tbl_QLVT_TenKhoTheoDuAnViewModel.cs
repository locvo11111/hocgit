using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_TenKhoTheoDuAnViewModel : ICloneable
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
		public string CodeDuAn { get; set; }
		public string CodeHangMuc { get; set; }
		public string CodeCongTrinh { get; set; }
		public string CodeKhoChung { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
