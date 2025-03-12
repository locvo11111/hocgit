using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_HopDong_DeNghiThanhToanViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDot { get; set; }
		public string GhiChu { get; set; }
		public string Type_Row { get; set; }
		public string TenCongTac { get; set; }
		public string DonVi { get; set; }
		public string LuyKeKyTruoc { get; set; }
		public string LuyKeKyNay { get; set; }
		public string KyNay { get; set; }
		public long? LoaiCT { get; set; }
		public long? IndexCT { get; set; }
		public long? IndexCha { get; set; }
		public bool? IsCoDinh { get; set; }
		public bool? Modified { get; set; } = false;
		public bool? ModifiedFromServer { get; set; } = false;
		public int? SortId { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
