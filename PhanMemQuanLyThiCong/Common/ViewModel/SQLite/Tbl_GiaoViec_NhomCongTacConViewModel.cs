using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GiaoViec_NhomCongTacConViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeCongViecCha { get; set; }
		public string Ten { get; set; } = "Nhóm con mới";
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
