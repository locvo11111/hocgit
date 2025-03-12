using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_DanhmuCongTacTongmucdautuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDuAn { get; set; }
		public string NoiDungChiPhi { get; set; }
		public double? TyLe { get; set; }
		public double? HeSo { get; set; }
		public string CachTinh { get; set; }
		public long? GiaTriTruocThueMacDinh { get; set; }
		public double? ThueGtgtMacDinh { get; set; }
		public long? GiaTriSauThueMacDinh { get; set; }
		public string KyHieu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
