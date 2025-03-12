using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TaoHopDongMoiViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDuAn { get; set; }
		public string TenHopDong { get; set; }
		public string SoHopDong { get; set; }
		public string CodeCongTrinh { get; set; }
		public string CodeHangMuc { get; set; }
		public string CodeNhaThau { get; set; }
		public string CodeNhaThauPhu { get; set; }
		public string CodeNcc { get; set; }
		public string CodeToDoi { get; set; }
		public DateTime? NgayBatDau { get; set; }
		public DateTime? NgayKetThuc { get; set; }
		public string CodeDonViThucHien { get; set; }
		public string CodeBenGiao { get; set; }
		public string TrangThai { get; set; }
		public double? GiaTriHopDong { get; set; }
		public string CodeDonViChuQuan { get; set; }
		public string LoaiHD { get; set; }
		public string CodeLoaiHopDong { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
