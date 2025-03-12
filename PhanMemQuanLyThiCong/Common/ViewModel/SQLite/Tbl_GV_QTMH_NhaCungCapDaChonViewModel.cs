using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GV_QTMH_NhaCungCapDaChonViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeQuyTrinh { get; set; }
		public string CodeNhaCungCap { get; set; }
		public string CodeVatTu { get; set; }
		public string DonGia { get; set; }
		public string KhoiLuong { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
