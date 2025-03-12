using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TCTU_DeXuatViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public int? TrangThai { get; set; }
		public string LoaiKinhPhi { get; set; }
		public string NoiDungUng { get; set; }
		public long? GiaTriDotNay { get; set; }
		public long? SoTienDaSuDung { get; set; }
		public long? SoTienDaGiaiChi { get; set; }
		public string NguoiYeuCau { get; set; }
		public string NguoiDuyet { get; set; }
		public DateTime? NgayThangYeuCau { get; set; }
		public DateTime? NgayThangNhan { get; set; }
		public string CongTrinh { get; set; }
		public string GhiChu { get; set; }
		public string NguoiLapTamUng { get; set; }
		public string CodeHd { get; set; }
		public string CodeDuAn { get; set; }
		public string CodeKHVT { get; set; }
		public string CodeCongTacTheoGiaiDoan { get; set; }
		public bool? IsEdit { get; set; } = false;
		public bool? IsVanChuyen { get; set; } = false;
		public bool? IsTDKH { get; set; } = false;
		public bool? IsVatLieu { get; set; } = false;
		public bool? IsEditGiaTri { get; set; } = false;
		public int NguonThuChi { get; set; } = 1;
		public string ToChucCaNhanNhanChiPhiTamUng { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
