using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_PhanTuyenViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string Ten { get; set; }
		public string CodeGop { get; set; }
		public int SortId { get; set; } = 0;
		public string CodeHangMuc { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public string CodeDienDai { get; set; }
		public long? STT { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
