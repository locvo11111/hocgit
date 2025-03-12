using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ChamCong_CongNhatCongKhoanViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string TenCongTac { get; set; }
		public string CodeGiaiDoan { get; set; }
		public string CodeNhaThau { get; set; }
		public string CodeToDoi { get; set; }
		public string CodeNhaThauPhu { get; set; }
		public int TypeRow { get; set; } = 2;
		public string DonVi { get; set; }
		public string TenHangMuc { get; set; }
		public int? SortId { get; set; }
		public string MaHieuCongTac { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
