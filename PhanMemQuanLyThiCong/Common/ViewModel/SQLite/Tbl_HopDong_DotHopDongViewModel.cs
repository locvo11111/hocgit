using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_HopDong_DotHopDongViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeGiaiDoan { get; set; }
		public string Ten { get; set; }
		public DateTime NgayBatDau { get; set; }
		public DateTime NgayKetThuc { get; set; }
		public string CodeHd { get; set; }
		public long? GiaTriKyNay { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public long? SoTienDaTamUng { get; set; }
		public long? SanLuongThiCong { get; set; }
		public long? SoTienThucNhan { get; set; }
		public string TrangThai { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
