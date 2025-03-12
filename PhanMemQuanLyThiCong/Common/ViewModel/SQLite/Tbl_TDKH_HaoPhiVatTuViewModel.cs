using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_HaoPhiVatTuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeCongTac { get; set; }
		public string CodeCongViecCha { get; set; }
		public string MaVatLieu { get; set; } = "";
		public string MaTXHienTruong { get; set; }
		public bool PhanTichKeHoach { get; set; } = false;
		public string VatTu { get; set; }
		public string DonVi { get; set; } = "";
		public double DinhMuc { get; set; } = 0;
		public double DinhMucNguoiDung { get; set; } = 0;
		public double HeSo { get; set; } = 0;
		public double HeSoNguoiDung { get; set; } = 0;
		public string LoaiVatTu { get; set; }
		public long DonGia { get; set; } = 0;
		public long DonGiaThiCong { get; set; } = 0;
		public DateTime? NgayBatDau { get; set; }
		public DateTime? NgayKetThuc { get; set; }
		public DateTime? NgayBatDauThiCong { get; set; }
		public DateTime? NgayKetThucThiCong { get; set; }
		public string CodeGiaoThau { get; set; }
		public string CodeHaoPhiCha { get; set; }
		public string CodeVatTu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
