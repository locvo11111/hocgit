using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_ChiTietCongTacConViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeCongTacCha { get; set; }
		public string KyHieuBanVe { get; set; }
		public string MaHieuCongTac { get; set; }
		public string TenCongTac { get; set; }
		public string TenPhanTang { get; set; } = "";
		public int SoBoPhanGiongNhau { get; set; } = 0;
		public double Dai { get; set; } = 0;
		public double Rong { get; set; } = 0;
		public double Cao { get; set; } = 0;
		public double HeSoCauKien { get; set; } = 0;
		public bool IsCongThucMacDinh { get; set; } = false;
		public string CodeNhom { get; set; }
		public double? KhoiLuongMotBoPhan { get; set; }
		public int Row { get; set; } = - 1;
		public string CodeDoBocHD { get; set; }
		public double? LoaiCT { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
