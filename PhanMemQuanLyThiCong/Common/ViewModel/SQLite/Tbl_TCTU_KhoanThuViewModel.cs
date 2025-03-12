using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TCTU_KhoanThuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDeXuat { get; set; }
		public long? TheoThucHien { get; set; }
		public long? ThucTeThu { get; set; }
		public string NguoiGiao { get; set; }
		public string NguoiNhan { get; set; }
		public DateTime? NgayThangThucHien { get; set; }
		public string XuatPhieu { get; set; }
		public double? SoHoaDon { get; set; }
		public string CongTrinh { get; set; }
		public bool? IsNguonThu { get; set; } = false;
		public string NoiDungThu { get; set; }
		public string ToChucCaNhanNhanChiPhiTamUng { get; set; }
		public int? TrangThai { get; set; } = 1;
		public bool? CheckDaThu { get; set; } = false;
		public string GhiChu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public string FullNameSend { get; set; }
		public DateTime? NgayGuiDuyet { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
