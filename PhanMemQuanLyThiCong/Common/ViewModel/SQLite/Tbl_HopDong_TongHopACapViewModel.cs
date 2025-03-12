using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_HopDong_TongHopACapViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDot { get; set; }
		public string CodeYeuCauVatTu { get; set; }
		public double? KhoiLuongThucHienTrongKy { get; set; }
		public long? GiaTriThucNhan { get; set; }
		public string GhiChu { get; set; }
		public DateTime? NgayBatDau { get; set; }
		public DateTime? NgayKetThuc { get; set; }
		public double? KhoiLuongHopDong { get; set; }
		public long? GiaTriHD { get; set; }
		public bool? Modified { get; set; } = false;
		public bool? ModifiedFromServer { get; set; } = false;
		public int? SortId { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
