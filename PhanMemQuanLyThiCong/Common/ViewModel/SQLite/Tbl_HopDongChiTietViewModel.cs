using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_HopDongChiTietViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public double? GiaTri { get; set; }
		public string CodeHopDong { get; set; }
		public string Ngay { get; set; }
		public int? Loai { get; set; } = 0;
		public bool? TheoThang { get; set; } = false;
		public bool? IsPhanTram { get; set; } = false;
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
