using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_CongVanDiDen_QuanLyCongVanViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDuAn { get; set; }
		public string MaHoSo { get; set; }
		public string TenHoSo { get; set; }
		public string NguoiKy { get; set; }
		public string CodeDonViPhatHanh { get; set; }
		public DateTime? NgayNhan { get; set; }
		public string LoaiCongVan { get; set; }
		public DateTime? NgayMuon { get; set; }
		public string LyDoMuon { get; set; }
		public string DaMuon { get; set; }
		public string TrangThai { get; set; }
		public string SoanThao { get; set; }
		public string GhiChu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
