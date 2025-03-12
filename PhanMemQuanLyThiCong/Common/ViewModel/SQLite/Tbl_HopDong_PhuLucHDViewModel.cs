using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_HopDong_PhuLucHDViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string CodePl { get; set; }
		public string Code { get; set; }
		public string TenCongViec { get; set; }
		public string DonVi { get; set; }
		public double? KhoiLuong { get; set; }
		public double? DonGia { get; set; }
		public double? ThanhTien { get; set; }
		public string GhiChu { get; set; }
		public double? KhoiLuongThiCong { get; set; }
		public double? ThanhTienThiCong { get; set; }
		public string CodeKHVT { get; set; }
		public string CodeCongTacTheoGiaiDoan { get; set; }
		public DateTime? NgayBatDau { get; set; }
		public DateTime? NgayKetThuc { get; set; }
		public string MaHieu { get; set; }
		public bool? IsDonGiaKeHoach { get; set; }
		public string CodeHM { get; set; }
		public int? SortId { get; set; }
		public string CodeNhom { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
