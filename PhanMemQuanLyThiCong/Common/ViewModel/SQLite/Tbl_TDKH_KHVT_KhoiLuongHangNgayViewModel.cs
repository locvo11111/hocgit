using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_KHVT_KhoiLuongHangNgayViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeVatTu { get; set; }
		public string NhaCungCap { get; set; }
		public DateTime Ngay { get; set; }
		public double? KhoiLuongKeHoach { get; set; }
		public double? KhoiLuongBoSung { get; set; }
		public long? ThanhTienKeHoach { get; set; }
		public double? KhoiLuongKeHoachGiaoViec { get; set; }
		public long? ThanhTienKeHoachGiaoViec { get; set; }
		public string GhiChu { get; set; }
		public double? KhoiLuongThiCong { get; set; }
		public long? ThanhTienThiCong { get; set; }
		public bool IsSumKeHoach { get; set; } = false;
		public bool IsSumThiCong { get; set; } = false;
		public bool? IsEdited { get; set; } = false;
		public string DoBoc { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
