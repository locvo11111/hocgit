using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GiaoViec_ChiaThiCongViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeCongViecCon { get; set; }
		public double KhoiLuongKeHoach { get; set; } = 0;
		public double KhoiLuongHopDong { get; set; } = 0;
		public string TenCongViec { get; set; } = "";
		public DateTime? NgayBatDau { get; set; }
		public DateTime? NgayKetThuc { get; set; }
		public string TrangThai { get; set; } = "Chưa thực hiện";
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
