using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_HopDong_DoBocViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodePL { get; set; }
		public string CodeDot { get; set; }
		public double KhoiLuongToanBo { get; set; } = 0;
		public string KhoiLuongToanBo_CongThuc { get; set; } = "";
		public string CodeNhom { get; set; }
		public string GhiChu { get; set; } = "";
		public int RowDoBoc { get; set; } = - 1;
		public int RowKeHoach { get; set; } = - 1;
		public int RowKhoiLuongHangNgay { get; set; } = - 1;
		public double? LoaiCT { get; set; }
		public double? TheoHopDong { get; set; }
		public bool KhoiLuongToanBo_Iscongthucmacdinh { get; set; }
		public string Code_Goc { get; set; }
		public bool? IsEdit { get; set; } = false;
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
