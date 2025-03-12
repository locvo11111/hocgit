using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_KHVT_VatTuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeHangMuc { get; set; }
		public string CodePhanTuyen { get; set; }
		public string MaVatLieu { get; set; }
		public string MaTXHienTruong { get; set; }
		public string VatTu { get; set; }
		public string DonVi { get; set; } = "";
		public long DonGia { get; set; } = 0;
		public double KhoiLuongKeHoach { get; set; } = 0;
		public double KinhPhiDuKien { get; set; } = 0;
		public double KinhPhiTheoTienDo { get; set; } = 0;
		public string LoaiVatTu { get; set; }
		public string CachTinh { get; set; }
		public string CodeNhaThau { get; set; }
		public string CodeToDoi { get; set; }
		public string CodeNhaThauPhu { get; set; }
		public string CodeMay { get; set; }
		public string CodeMuiThiCong { get; set; }
		public double KhoiLuongHopDong { get; set; } = 0;
		public DateTime NgayBatDau { get; set; }
		public DateTime NgayKetThuc { get; set; }
		public DateTime? NgayBatDauThiCong { get; set; }
		public DateTime? NgayKetThucThiCong { get; set; }
		public string CodeGiaiDoan { get; set; }
		public long DonGiaThiCong { get; set; } = 0;
		public bool PhanTichKeHoach { get; set; } = true;
        public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
