using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_DependencyViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string Predecessorcode_KeHoach { get; set; }
		public string Successorcode_KeHoach { get; set; }
		public string LoaiCongTac { get; set; }
		public int? Dependencytype { get; set; }
		public int? SortId { get; set; }
		public string Successorcode_HM { get; set; }
		public string Predecessorcode_HM { get; set; }
		public string Predecessorcode_CTR { get; set; }
		public string Successorcode_CTR { get; set; }
		public string Successorcode_CodeNhom { get; set; }
		public string Predecessorcode_CodeNhom { get; set; }
		public string Predecessorcode_CodeTuyen { get; set; }
		public string Successorcode_CodeTuyen { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
