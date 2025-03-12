using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GV_QTMH_NhaCungCapViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeQuyTrinh { get; set; }
		public string NhaCungCap { get; set; }
		public string DiaChi { get; set; } = "";
		public string LienHe { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
