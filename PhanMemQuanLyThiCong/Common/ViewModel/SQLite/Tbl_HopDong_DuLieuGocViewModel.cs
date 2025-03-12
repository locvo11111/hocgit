using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_HopDong_DuLieuGocViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeHangMuc { get; set; }
		public string MaHieuCongTac { get; set; }
		public string TenCongTac { get; set; }
		public string DonVi { get; set; }
		public double KhoiLuong { get; set; } = 0;
		public double? DonGia { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
