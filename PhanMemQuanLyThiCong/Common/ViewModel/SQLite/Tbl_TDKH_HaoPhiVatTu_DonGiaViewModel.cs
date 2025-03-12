using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_HaoPhiVatTu_DonGiaViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeHaoPhiVatTu { get; set; }
		public DateTime TuNgay { get; set; }
		public DateTime DenNgay { get; set; }
		public long? DonGia { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
