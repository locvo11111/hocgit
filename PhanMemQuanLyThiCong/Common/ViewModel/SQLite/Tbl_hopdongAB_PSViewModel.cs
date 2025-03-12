using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_hopdongAB_PSViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDot { get; set; }
		public double? LuyKeDenHetKyTruoc { get; set; }
		public double? ThucHienKyNay { get; set; }
		public double? LuyKeDenHetKyNay { get; set; }
		public double? DonGiaPhatSinh { get; set; }
		public double? TheoHopDong_Thanhtien { get; set; }
		public double? LuyKeDenHetKyTruoc_Thanhtien { get; set; }
		public double? ThucHienKyNay_Thanhtien { get; set; }
		public double? LuyKeDenHetKyNay_Thanhtien { get; set; }
		public string GhiChu { get; set; }
		public string CodeDB { get; set; }
		public string Code_Goc { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
