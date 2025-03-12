using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_TraVatTuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string TenKhoTra { get; set; }
		public double? KhoiLuong { get; set; }
		public DateTime? Ngay { get; set; }
		public double? DonGia { get; set; }
		public string GhiChu { get; set; }
		public string CodeNhapVatTu { get; set; }
		public string CodeXuatVatTu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
