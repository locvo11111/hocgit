using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ThongTinNganSachViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDuAn { get; set; }
		public string NguonVon { get; set; }
		public long? SoTien { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
