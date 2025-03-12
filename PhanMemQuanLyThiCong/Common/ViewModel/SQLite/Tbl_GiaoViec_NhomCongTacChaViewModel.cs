using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GiaoViec_NhomCongTacChaViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeHangMuc { get; set; }
		public string Ten { get; set; } = "Nhóm mới";
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
