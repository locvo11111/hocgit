using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_HopDong_ThongtinphulucHDViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeLoaiHd { get; set; }
		public string CodeHd { get; set; }
		public string TenPl { get; set; }
		public double? PhatSinh { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
