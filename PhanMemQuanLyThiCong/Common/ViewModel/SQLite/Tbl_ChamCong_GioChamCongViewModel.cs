using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ChamCong_GioChamCongViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeNhanVien { get; set; }
		public string Buoi { get; set; }
		public DateTime? DateTime { get; set; }
		public string GioChamCongVao { get; set; }
		public string GioChamCongRa { get; set; }
		public string CodeDuAn { get; set; }
		public string GhiChu { get; set; }
		public string ChuThich { get; set; }
		public string NghiLam { get; set; }
		public string TangCa { get; set; }
		public int? SortId { get; set; }
		public double? HeSoTangCa { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
