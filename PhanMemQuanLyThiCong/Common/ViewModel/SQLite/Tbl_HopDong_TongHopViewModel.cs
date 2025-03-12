using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_HopDong_TongHopViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string CodeHopDong { get; set; }
		public string CodeDuAn { get; set; }
		public string Code { get; set; }
		public string NguoiThucHien { get; set; }
		public string DienThoaiLienHe { get; set; }
		public string GhiChu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
