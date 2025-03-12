using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ChamCong_CongNhatCongKhoan_ChiTietViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeNhanVien { get; set; }
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
		public string GhiChu { get; set; }
		public int? SortId { get; set; }
		public double? KhoiLuong { get; set; }
		public double? HeSoSang { get; set; }
		public double? HeSoChieu { get; set; }
		public double? HeSoToi { get; set; }
		public double? TongCong { get; set; }
		public double? TongGioSang { get; set; }
		public double? TongGioChieu { get; set; }
		public double? TongGioToi { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
