using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TCTU_NewKhoanChiViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string NoiDungUng { get; set; }
		public double? GiaTriTamUngDaDuyet { get; set; }
		public double? GiaTriTamUngThucTe { get; set; }
		public double? GiaTriGiaiChi { get; set; }
		public string CodeKC { get; set; }
		public bool IsLayChiTiet { get; set; } = false;
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
