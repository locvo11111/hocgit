using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_MTC_NhienLieuHangNgayViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeNhienLieu { get; set; }
		public double? KhoiLuong { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? Ngay { get; set; }
		public int? SortId { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
