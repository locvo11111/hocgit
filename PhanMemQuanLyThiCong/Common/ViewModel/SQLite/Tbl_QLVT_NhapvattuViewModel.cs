using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_NhapvattuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public double? TrangThai { get; set; } = 0;
		public bool? XuatKhiNhap { get; set; } = false;
		public string TenKhoNhap { get; set; }
		public double? DeXuatVatTu { get; set; }
		public double? DaDuyetDeXuat { get; set; }
		public double? LuyKeNhapTheoDot { get; set; }
		public double? ThucNhap { get; set; }
		public string NguoiGiaoVatTu { get; set; }
		public string NguoiNhanVatTu { get; set; }
		public string DonViCungCap { get; set; }
		public bool? HoanThoanh_Ok { get; set; } = false;
		public string ThoiGianNhapKho { get; set; }
		public double? DonGiaHienTruong { get; set; }
		public string HopDong { get; set; }
		public string GhiChu { get; set; }
		public string CodeDeXuat { get; set; }
		public string CodeGiaiDoan { get; set; }
		public int? DonGia { get; set; }
		public bool? IsXuat { get; set; } = false;
		public bool? IsChuyenKho { get; set; } = false;
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public bool? ACapB { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
