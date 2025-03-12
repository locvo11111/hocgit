using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_MTC_ChiTietDinhMucViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeMay { get; set; }
		public string CodeDinhMuc { get; set; }
		public string DonVi { get; set; }
		public double? MucTieuThu { get; set; } = 0;
		public string GhiChu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
