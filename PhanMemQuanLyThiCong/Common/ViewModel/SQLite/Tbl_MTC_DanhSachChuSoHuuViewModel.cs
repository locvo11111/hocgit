using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_MTC_DanhSachChuSoHuuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string Ten { get; set; } = "Chưa nhập tên";
		public string GhiChu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public string LoaiSoHuu { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
