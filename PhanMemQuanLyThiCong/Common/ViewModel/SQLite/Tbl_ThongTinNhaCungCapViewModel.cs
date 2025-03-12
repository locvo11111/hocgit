using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ThongTinNhaCungCapViewModel : ICloneable
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
		public string MaSoThue { get; set; } = "";
		public string MaDoanhNghiep { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public bool DonViTrucThuoc { get; set; } = false;
		public long? GiaTri { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
