using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_NhomCongTacViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeHangMuc { get; set; }
		public string Ten { get; set; } = "Nhóm mới";
		public string DonVi { get; set; }
		public double? DonGia { get; set; }
		public string TrangThai { get; set; } = "Chưa thực hiện";
		public int? SortId { get; set; }
		public string CodePhanTuyen { get; set; }
		public double? KhoiLuongKeHoach { get; set; }
		public double? KhoiLuongDaThiCong { get; set; }
		public DateTime? NgayBatDau { get; set; }
		public DateTime? NgayKetThuc { get; set; }
		public double? DonGiaThiCong { get; set; }
		public double? KhoiLuongHopDongChiTiet { get; set; }
		public string CodeGop { get; set; }
		public string LyTrinhCaoDo { get; set; }
		public string GhiChuBoSungJson { get; set; }
		public string CodeNhomGiaoThau { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public string GhiChu { get; set; }
		public string CodeDienDai { get; set; }
		public long? STT { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
