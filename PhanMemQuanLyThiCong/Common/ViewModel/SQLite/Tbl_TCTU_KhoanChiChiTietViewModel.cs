using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TCTU_KhoanChiChiTietViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string Ten { get; set; }
		public string DonVi { get; set; }
		public double? DonGia { get; set; }
		public double? KhoiLuong { get; set; }
		public string GhiChu { get; set; }
		public string CodeKC { get; set; }
		public string CodeKCNew { get; set; }
		public long? ThanhTienChiTiet { get; set; }
		public bool? ChiTietCoDonGia { get; set; } = false;
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
