using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_KhoiLuongCongViecTungNgayViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeCongTacTheoGiaiDoan { get; set; }
		public string CodeNhom { get; set; }
		public string CodeCongViecCha { get; set; }
		public string CodeCongViecCon { get; set; }
		public string NhaCungCap { get; set; }
		public DateTime Ngay { get; set; }
		public double? KhoiLuongKeHoach { get; set; }
		public double? KhoiLuongKeHoachGoc { get; set; }
		public double? KhoiLuongBoSung { get; set; }
		public double? KhoiLuongKeHoachGiaoViec { get; set; }
		public long? ThanhTienKeHoachGiaoViec { get; set; }
		public long? ThanhTienKeHoach { get; set; }
		public bool IsEdited { get; set; } = false;
		public bool IsEditedThanhTien { get; set; } = false;
		public long? ThanhTienThiCong { get; set; }
		public double? KhoiLuongThiCong { get; set; }
		public bool IsSumThiCong { get; set; } = false;
		public string GhiChu { get; set; }
		public string LyTrinhCaoDo { get; set; }
		public string DoBoc { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
