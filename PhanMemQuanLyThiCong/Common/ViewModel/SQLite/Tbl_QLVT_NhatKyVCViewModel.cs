using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_NhatKyVCViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeCha { get; set; }
		public string TaiXe { get; set; }
		public string BienSoXe { get; set; }
		public string KichThuocThungXe { get; set; } = "0";
		public double? TongSoLuongChuyen { get; set; } = 0;
		public double? KhoiLuong_1Chuyen { get; set; } = 0;
		public double? ThucTeVanChuyen { get; set; } = 0;
		public DateTime? Ngay { get; set; }
		public double? DonGia { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
