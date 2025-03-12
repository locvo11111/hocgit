using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_XuatVatTu_KhoiLuongHangNgayViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeCha { get; set; }
		public DateTime? Ngay { get; set; }
		public double? KhoiLuong { get; set; }
		public double? DonGia { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public bool? ACapB { get; set; }
		public int? TrangThai { get; set; } = 1;
		public string FullNameSend { get; set; }
		public DateTime? NgayGuiDuyet { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
