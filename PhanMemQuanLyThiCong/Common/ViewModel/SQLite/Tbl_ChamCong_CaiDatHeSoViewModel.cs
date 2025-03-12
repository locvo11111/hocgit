using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ChamCong_CaiDatHeSoViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public int? LoaiCaiDat { get; set; }
		public string CodeDuAn { get; set; }
		public double? Thu7 { get; set; }
		public double? ChuNhat { get; set; }
		public double? NgayLe { get; set; }
		public double? TangCaThu7 { get; set; }
		public double? TangCaChuNhat { get; set; }
		public double? TangCaNgayLe { get; set; }
		public double? TangCaNgayTuan { get; set; }
		public double? BuoiSang { get; set; }
		public double? BuoiChieu { get; set; }
		public double? BuoiToi { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
