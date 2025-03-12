using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TCTU_DeXuat_ChiTiet_HopDongViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeCha { get; set; }
		public double? ThanhTien { get; set; }
		public double? KhoiLuong { get; set; }
		public double? DonGia { get; set; }
		public DateTime NgayBD { get; set; }
		public DateTime NgayKT { get; set; }
		public string CodePl { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
