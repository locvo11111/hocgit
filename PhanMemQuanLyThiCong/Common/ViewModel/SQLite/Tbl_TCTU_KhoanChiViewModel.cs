using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TCTU_KhoanChiViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public int? TrangThai { get; set; }
		public string LoaiKinhPhi { get; set; }
		public string NoiDungUng { get; set; }
		public long? GiaTriTamUngDaDuyet { get; set; }
		public long? GiaTriTamUngThucTe { get; set; }
		public long? GiaTriGiaiChi { get; set; }
		public DateTime? DateThucNhanUng { get; set; }
		public DateTime? DateXacNhanDaUng { get; set; }
		public string CongTrinh { get; set; }
		public string GhiChu { get; set; }
		public string NguoiLapTamUng { get; set; }
		public string CodeDeXuat { get; set; }
		public string CodeNhanVien { get; set; }
		public string CodeNhaCungCap { get; set; }
		public string CodeNhaThau { get; set; }
		public string CodeNhaThauPhu { get; set; }
		public string CodeToDoi { get; set; }
		public DateTime? DateXacNhanDaChi { get; set; }
		public bool? CheckDaChi { get; set; } = false;
		public bool? CheckDaUng { get; set; } = false;
		public bool IsLayChiTiet { get; set; } = false;
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public string FullNameSend { get; set; }
		public DateTime? NgayGuiDuyet { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
