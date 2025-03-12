using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TCTU_KhoanThu_FiledinhkemViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeParent { get; set; }
		public string FileDinhKem { get; set; }
		public string Checksum { get; set; }
		public DateTime? Ngay { get; set; }
		public string Link { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
