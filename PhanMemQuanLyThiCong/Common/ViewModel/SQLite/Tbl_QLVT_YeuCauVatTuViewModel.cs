using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_YeuCauVatTuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string CodeGiaiDoan { get; set; }
		public string Code { get; set; }
		public string TenVatTu { get; set; }
		public string DonVi { get; set; }
		public double? YeuCauDotNay { get; set; }
		public double? HopDongKl { get; set; }
		public double? LuyKeYeuCau { get; set; }
		public double? LuyKeXuatKho { get; set; }
		public double? DonGiaHienTruong { get; set; }
		public string NguoiYeuCau { get; set; }
		public string NguoiNhan { get; set; }
		public DateTime? NgayGuiYeuCau { get; set; }
		public DateTime? NgayCanCoVatTu { get; set; }
		public string NhaCungCap { get; set; }
		public string NguoiPhuTrach { get; set; }
		public string NguoiThamGia { get; set; }
		public string NguoiTheoDoi { get; set; }
		public string HopDong { get; set; }
		public string DonViThucHien { get; set; }
		public double? TrangThai { get; set; } = 0;
		public string CodeKHVT { get; set; }
		public string MaVatTu { get; set; }
		public string CodeHangMuc { get; set; }
		public bool? IsDone { get; set; } = false;
		public string CodeTDKH { get; set; }
		public string CodeHd { get; set; }
		public int? DonGia { get; set; }
		public string TenNhaCungCap { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public bool? ACapB { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
