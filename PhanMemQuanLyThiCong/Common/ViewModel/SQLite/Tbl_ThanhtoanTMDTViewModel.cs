using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ThanhtoanTMDTViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string CodeDuAn { get; set; }
		public string Code { get; set; }
		public string TenCongViecThucHien { get; set; }
		public string HeSo { get; set; }
		public string KhoiLuongTheoHopDong { get; set; }
		public string KhoiLuongThanhToan { get; set; }
		public string KhoiLuongThamTra { get; set; }
		public string KhoiLuongQuyetToan { get; set; }
		public string GiaTriBanDau { get; set; }
		public string GiaTriHopDong { get; set; }
		public string GiaTriThanhToan { get; set; }
		public string GiaTriThamTra { get; set; }
		public string GiaTriQuyetToan { get; set; }
		public string GiaTriTamUng { get; set; }
		public string SoTienConLai { get; set; }
		public string KyHieu { get; set; }
		public string Xoa { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
