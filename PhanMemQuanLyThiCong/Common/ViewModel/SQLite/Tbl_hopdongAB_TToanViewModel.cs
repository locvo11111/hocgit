using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_hopdongAB_TToanViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeHopDong { get; set; }
		public string GhiChu { get; set; }
		public string Code_Goc { get; set; }
		public double? DonGiaBoSung { get; set; }
		public string CodeCongTacTheoGiaiDoan { get; set; }
		public int? SortId { get; set; }
		public string CodeNhom { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
