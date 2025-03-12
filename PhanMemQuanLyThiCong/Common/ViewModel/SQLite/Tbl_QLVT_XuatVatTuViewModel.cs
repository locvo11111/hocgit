using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_XuatVatTuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeGiaiDoan { get; set; }
		public string CodeNhapVT { get; set; }
		public string TenKhoNhap { get; set; }
		public double? DaDuyetDeXuat { get; set; }
		public double? LuyKeNhapTheoDot { get; set; }
		public double? LuyKeXuatTheoDot { get; set; }
		public double? ThucXuat { get; set; }
		public double? DonGiaHienTruong { get; set; }
		public DateTime? ThoiGianXuat { get; set; }
		public string GhiChu { get; set; }
		public string CodeDeXuat { get; set; }
		public double TrangThai { get; set; } = 0;
		public int? DonGia { get; set; }
		public int? SortId { get; set; }
		public double? DonGiaKiemSoat { get; set; }
		public double? KhoiLuongXuatThucTe { get; set; }
		public double? TonKhoThucTe { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public bool? ACapB { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
