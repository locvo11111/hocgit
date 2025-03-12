using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ThongTinDuAnViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string TenDuAn { get; set; } = "";
		public string MaDuAn { get; set; } = "";
        public DateTime NgayBatDau { get; set; } = new DateTime(2023, 01, 01);
        public DateTime NgayKetThuc { get; set; } = new DateTime(2023, 01, 10);
        public long TongVon { get; set; } = 0;
		public string TenChuDauTu { get; set; } = "";
		public string MaChuDauTu { get; set; } = "";
		public string DiaChi { get; set; } = "";
		public string MaHieu { get; set; } = "";
		public string DaiDien { get; set; } = "";
		public string DienThoai { get; set; } = "";
		public string MaSoThue { get; set; } = "";
		public string GhiChu { get; set; } = "";
		public string ChiTiet { get; set; } = "";
		public int SoNgayLayCongTacTuDong { get; set; } = 10;
		public bool? IsLayCongTacTuDong { get; set; } = false;
		public string CreatedBy { get; set; }
		public DateTime? LastSync { get; set; }
		public string LastSyncSerialNo { get; set; }
		public DateTime? BaseTime { get; set; }
		public string TrangThai { get; set; }
		public string CreatedBySerialno { get; set; }
		public bool IsShowKhoiLuongKeHoach { get; set; } = false;
		public bool IsShowDonGiaKeHoach { get; set; } = false;
		public bool IsShareToOtherKey { get; set; } = false;
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public bool AutoDivision { get; set; } = false;
		public Guid? CumDuAnId { get; set; }
		public bool IsAutoSynthetic { get; set; } = false;
        public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }




    }
}
