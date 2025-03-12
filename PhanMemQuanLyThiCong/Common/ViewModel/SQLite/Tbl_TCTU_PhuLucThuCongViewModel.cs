using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TCTU_PhuLucThuCongViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string TenCongViec { get; set; }
		public double? KhoiLuong { get; set; }
		public double? DonGia { get; set; }
		public string CodeCha { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
