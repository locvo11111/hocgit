using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_QLVanChuyenViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string CodeGiaiDoan { get; set; }
		public string CodeNhapVT { get; set; }
		public string Code { get; set; }
		public string NhatKy { get; set; }
		public bool? HoanThoanh_Ok { get; set; } = false;
		public double? DaDuyetDeXuat { get; set; }
		public double? LuyKeNhapKho { get; set; }
		public double? LuyKeVanChuyenTheoDot { get; set; }
		public double? ThucTeVanChuyen { get; set; }
		public long? TongSoLuongChuyen { get; set; }
		public double? KhoiLuong_1Chuyen { get; set; }
		public string KichThuocThungXe { get; set; }
		public string BienSoXe { get; set; }
		public string TaiXe { get; set; }
		public string CuLyVanChuyen { get; set; }
		public string SoHopDong { get; set; }
		public double? DonGiaHienTruong { get; set; }
		public DateTime? ThoiGianXuat { get; set; }
		public string GhiChu { get; set; }
		public string CodeDeXuat { get; set; }
		public double? DonGia { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
