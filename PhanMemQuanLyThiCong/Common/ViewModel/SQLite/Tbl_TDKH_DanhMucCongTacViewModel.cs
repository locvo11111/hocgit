using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_DanhMucCongTacViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeHangMuc { get; set; }
		public string CodePhanTuyen { get; set; }
		public string KyHieuBanVe { get; set; }
		public string MaHieuCongTac { get; set; } = "";
		public string TenCongTac { get; set; } = "";
		public string DonVi { get; set; } = "";
		public double KhoiLuongHopDongToanDuAn { get; set; } = 0;
		public bool? PhatSinh { get; set; }
		public int? SortId { get; set; }
		public bool HasHopDongAB { get; set; } = false;
		public bool PhanTichVatTu { get; set; } = false;
		public string CodeGop { get; set; }
		public double? KhoiLuongDocVao { get; set; }
		public string GhiChuBoSungJson { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public string CodeDienDai { get; set; }
		public double? ThanhTienDocVao { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
