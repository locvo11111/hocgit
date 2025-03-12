using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_ChuyenKhoVatTuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string CodeGiaiDoan { get; set; }
		public string Code { get; set; }
		public double? TonKhoChuyenDi { get; set; }
		public string TenKhoChuyenDi { get; set; }
		public double? LuyKeKhoChuyenDiTheoDot { get; set; }
		public double? ThucXuatChuyenKho { get; set; }
		public double? TonKhoChuyenDen { get; set; }
		public string TenKhoChuyenDen { get; set; }
		public double? LuyKeKhoChuyenDenTheoDot { get; set; }
		public double? ThucNhapKhoDen { get; set; }
		public bool? Ok { get; set; }
		public double? DonGia { get; set; }
		public string GhiChu { get; set; }
		public string CodeNhapVT { get; set; }
		public double? TrangThai { get; set; } = 0;
		public string CodeDeXuat { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
