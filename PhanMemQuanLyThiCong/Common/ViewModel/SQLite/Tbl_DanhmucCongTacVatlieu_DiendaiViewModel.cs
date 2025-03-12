using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_DanhmucCongTacVatlieu_DiendaiViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeVatLieu { get; set; }
		public string MaHieu { get; set; }
		public string TenVatTu { get; set; }
		public string DonVi { get; set; }
		public string KhoiLuongDuThau { get; set; }
		public string GiaDuToan { get; set; }
		public string ThanhTienDuThau { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
