using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GV_QTMH_DeXuat_VatTuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeQuyTrinh { get; set; } = "";
		public string VatTu { get; set; } = "";
		public string DonViTinh { get; set; } = "";
		public string KhoiLuong { get; set; } = "0";
		public string ThoiGianCanVatTu { get; set; } = "01/01/2022";
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
