using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ChamCong_CaiDatGioLamViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public long? GioVao { get; set; }
		public long? PhutVao { get; set; }
		public long? GioRa { get; set; }
		public long? PhutRa { get; set; }
		public long? VaoTre { get; set; }
		public long? RaSom { get; set; }
		public string BuoiSang_Chieu_Toi { get; set; } = "Chiều";
		public DateTime? NgayBatDau { get; set; }
		public DateTime? NgayKetThuc { get; set; }
		public string CongTruong_VanPhong { get; set; } = "Công trường";
		public string CodeDuAn { get; set; }
		public string MuaHe_Dong { get; set; } = "Mùa hè";
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
