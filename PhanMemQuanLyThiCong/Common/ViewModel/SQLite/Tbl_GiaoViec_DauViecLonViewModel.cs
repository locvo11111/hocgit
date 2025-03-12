using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GiaoViec_DauViecLonViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeDuAn { get; set; }
		public string DauViec { get; set; } = "";
		public string QuyTrinh { get; set; } = "Khác";
		public string LoaiMau { get; set; } = "HeThong";
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
