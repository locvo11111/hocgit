using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ChamCong_ChiTietPhuPhiNhanVienViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public long? LuongCoBan { get; set; } = 12000000;
		public long? NgayTinhLuong { get; set; } = 22;
		public double? NgayCong { get; set; }
		public long? PhuCapTrachNhiem { get; set; }
		public long? TienCom { get; set; }
		public long? XangXe { get; set; }
		public long? NhaOConNho { get; set; }
		public long? SoTienGiamTru { get; set; }
		public long? KhoanKhac { get; set; }
		public long? Phat { get; set; }
		public string GhiChu { get; set; }
		public string CodeNhanVien { get; set; }
		public DateTime? Month { get; set; }
		public double? SoNgayTangCa { get; set; }
		public string CodeDuAn { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
