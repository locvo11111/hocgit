using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_CongVanDiDen_DonViPhatHanhViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string DonViPhatHanh { get; set; }
		public string Kieu { get; set; }
		public string GhiChu { get; set; }
		public bool? Modified { get; set; } = false;
		public bool? ModifiedFromServer { get; set; } = false;
		public int? SortId { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
