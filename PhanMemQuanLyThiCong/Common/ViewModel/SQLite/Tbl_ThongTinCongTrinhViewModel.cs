using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ThongTinCongTrinhViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeDuAn { get; set; }
		public string Ten { get; set; } = "";
		public string DiaChi { get; set; } = "";
		public string DaiDien { get; set; } = "";
		public string DienThoai { get; set; } = "";
		public long? GiaTri { get; set; } = 0;
		public string ThauChinh { get; set; }
		public string TrangThai { get; set; } = "";
		public int SortId { get; set; } = - 1;
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public long? STT { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
