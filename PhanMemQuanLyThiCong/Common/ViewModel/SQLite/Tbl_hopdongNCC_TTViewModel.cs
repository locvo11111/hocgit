using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_hopdongNCC_TTViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDot { get; set; }
		public double? TheoHopDong { get; set; }
		public double? LuyKeDenHetKyTruoc { get; set; }
		public double? ThucHienKyNay { get; set; }
		public double? LuyKeDenHetKyNay { get; set; }
		public double? KhoiLuongThanhToan { get; set; }
		public double? DonGiaBoSung { get; set; }
		public string GhiChu { get; set; }
		public string CodePhuLuc { get; set; }
		public double? KhoiLuongThuCong { get; set; }
		public int? SortId { get; set; }
		public string Code_Goc { get; set; }
		public double? KhoiLuongVanChuyen { get; set; }
		public double? KhoiLuongHangNgay { get; set; }
		public bool IsEdit { get; set; } = false;
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
