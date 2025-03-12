using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_MTC_NhienLieuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string Ten { get; set; }
		public string DonVi { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public bool? IsHienThi { get; set; }
		public double? KhoiLuongDaNhap { get; set; }
		public double? KhoiLuongDaDung { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
