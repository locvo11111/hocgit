using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_KhoiLuongPhatSinhViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodePhatSinh { get; set; }
		public string CodeCongTac { get; set; }
		public double KhoiLuong { get; set; } = 0;
		public double DonGia { get; set; } = 0;
        public DateTime NgayBatDau { get; set; } = new DateTime(2023, 01, 01);
        public DateTime NgayKetThuc { get; set; } = new DateTime(2023, 01, 10);
        public DateTime NgayBatDauThiCong { get; set; } = new DateTime(2023, 01, 01);
        public DateTime NgayKetThucThiCong { get; set; } = new DateTime(2023, 01, 10);
        public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
