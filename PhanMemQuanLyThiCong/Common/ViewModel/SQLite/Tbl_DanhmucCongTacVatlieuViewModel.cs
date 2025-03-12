using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_DanhmucCongTacVatlieuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeHangMuc { get; set; }
		public string MaHieu { get; set; }
		public string TenVatTu { get; set; }
		public string DonVi { get; set; }
		public double? KhoiLuongDuThau { get; set; }
		public double? GiaDuToan { get; set; }
		public double? ThanhTienDuThau { get; set; }
		public long DonGia { get; set; } = 0;
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
