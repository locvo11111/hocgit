using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ChamCong_TamUngViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public int? SortId { get; set; }
		public string CodeDuAn { get; set; }
		public string CodeNhanVien { get; set; }
		public DateTime? NgayThangUng { get; set; }
		public long? SoTien { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public string GhiChu { get; set; }
		public string NoiDungTamUng { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
