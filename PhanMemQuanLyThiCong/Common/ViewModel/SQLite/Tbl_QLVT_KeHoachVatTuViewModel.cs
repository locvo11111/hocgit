using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_QLVT_KeHoachVatTuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeHangMuc { get; set; }
		public string VatTu { get; set; }
		public string DonVi { get; set; }
		public double? KhoiLuongDinhMuc { get; set; }
		public double? KhoiLuongKeHoach { get; set; }
		public double? DonGiaGoc { get; set; }
		public double? DonGiaKeHoach { get; set; }
		public DateTime? ThoiGianTu { get; set; }
		public DateTime? ThoiGianDen { get; set; }
		public string GhiChu { get; set; }
		public string MaVatLieu { get; set; }
		public string TenVatTu_KhongDau { get; set; }
		public string NhanHieu { get; set; }
		public string QuyCach { get; set; }
		public string XuatXu { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
