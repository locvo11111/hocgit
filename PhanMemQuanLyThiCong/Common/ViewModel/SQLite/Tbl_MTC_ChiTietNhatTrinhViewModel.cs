using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_MTC_ChiTietNhatTrinhViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeCongTacTheoGiaiDoan { get; set; }
		public string CodeCongTacThuCong { get; set; }
		public string GioBatDauSang { get; set; }
		public string GioKetThucSang { get; set; }
		public string GioBatDauChieu { get; set; }
		public string GioKetThucChieu { get; set; }
		public string GioBatDauToi { get; set; }
		public string GioKetThucToi { get; set; }
		public double? TongGio { get; set; }
		public DateTime NgayThang { get; set; }
		public double? NhienLieuChinh { get; set; }
		public string NhienLieuPhu { get; set; }
		public string GhiChu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public double? KmDau { get; set; }
		public double? KmCuoi { get; set; }
		public string CodeDinhMuc { get; set; }
		public double? TongGioSang { get; set; }
		public double? TongGioChieu { get; set; }
		public double? TongGioToi { get; set; }
		public string CodeMayDuAn { get; set; }
		public double? KhoiLuong { get; set; }
		public long? TienTaiXe { get; set; }
		public string LyLichSuaChua { get; set; }
        public bool IsTongGio { get; set; }
        public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
