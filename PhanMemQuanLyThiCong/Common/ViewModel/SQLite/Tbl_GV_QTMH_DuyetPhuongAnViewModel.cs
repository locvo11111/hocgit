using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GV_QTMH_DuyetPhuongAnViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeQuyTrinh { get; set; }
		public string PhongDuyet { get; set; } = "";
		public string NguoiDuyet { get; set; } = "";
		public string ThoiGianDuyet { get; set; } = "";
		public string PhongMua { get; set; } = "";
		public string NguoiMua { get; set; } = "";
		public string ThoiGianMua { get; set; } = "";
		public string PhongNhanSanPham { get; set; } = "";
		public string NguoiNhanSanPham { get; set; } = "";
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
