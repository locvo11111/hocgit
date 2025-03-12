using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_MTC_DanhSachMayViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string Ten { get; set; } = "Chưa nhập tên";
		public string No { get; set; }
		public string TrangThai { get; set; }
		public string CodeChuSoHuu { get; set; }
		public string CodeNhaThau { get; set; }
		public string CodeNhaThauPhu { get; set; }
		public string CodeToDoi { get; set; }
		public int? SortId { get; set; }
		public string GhiChu { get; set; }
		public DateTime? NgayMuaMay { get; set; }
		public string UrlImage { get; set; }
		public string DonVi { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public string CaMayKm { get; set; }
		public double? HaoPhi { get; set; }
		public string LoaiMay { get; set; }
		public string BienSo { get; set; }
		public string TenVietTat { get; set; }
		public string XuatXu { get; set; }
		public string NamSanXuat { get; set; }
		public long? GiaMuaMay { get; set; }
		public string SoKhung { get; set; }
		public long? GiaCaMay { get; set; }
		public long? ChiPhiSuaChua { get; set; }
		public long? ChiPhiKhac { get; set; }
		public long? TienTaiXe { get; set; }
		public string ViTriMay { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
