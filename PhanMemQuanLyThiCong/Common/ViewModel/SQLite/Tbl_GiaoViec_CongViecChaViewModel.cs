using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GiaoViec_CongViecChaViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string CodeCongViecCha { get; set; }
		public string CodeDauMuc { get; set; }
		public string CodeHangMuc { get; set; }
		public string CodeCongTacTheoGiaiDoan { get; set; }
		public string MaDinhMuc { get; set; } = "";
		public string TenCongViec { get; set; } = "";
		public string DonVi { get; set; } = "";
		public string NguoiGiao { get; set; } = "";
		public string NguoiPhuTrach { get; set; } = "";
		public string NguoiTheoDoi { get; set; } = "";
        public DateTime NgayBatDau { get; set; } = new DateTime(2023, 01, 01);
        public DateTime NgayKetThuc { get; set; } = new DateTime(2023, 01, 10);
        public string PhuTrachChinh { get; set; } = "";
		public double KhoiLuongHopDong { get; set; } = 0;
		public double KhoiLuongKeHoach { get; set; } = 0;
		public string NoiDungThucHien { get; set; } = "";
		public string SoHopDong { get; set; } = "";
		public string LoaiChiPhi { get; set; } = "";
		public string CapDoQuanTrong { get; set; } = "";
		public string GhiChu { get; set; } = "";
		public int TienDo { get; set; } = 0;
		public string TrangThai { get; set; } = "Chưa thực hiện";
		public int SortId { get; set; } = - 1;
		public string NguoiDuyetBuoc1 { get; set; } = "";
		public string NguoiDuyetBuoc2 { get; set; } = "";
		public string NguoiDuyetBuoc3 { get; set; } = "";
		public string NguoiDuyetBuoc4 { get; set; } = "";
		public string CodeNhom { get; set; }
		public double KhoiLuongThanhToan { get; set; } = 0;
		public double? DonGia { get; set; } = 0;
		public double? DonGiaThiCong { get; set; } = 0;
		public string CodeNhaThau { get; set; }
		public string CodeToDoi { get; set; }
		public string CodeNhaCungCap { get; set; }
		public string CodeNhaThauPhu { get; set; }
		public bool? GhiNhatKy { get; set; }
		public string LinkHop { get; set; }
		public int? RowIndex { get; set; }
		public string UserSendId { get; set; }
		public DateTime? NgayGuiDuyet { get; set; }
		public string FullNameSend { get; set; }
		public string UserApproveId { get; set; }
		public string FullNameApprove { get; set; }
		public DateTime? NgayDuyet { get; set; }
		public string GhiChuDuyet { get; set; }
		public string LyTrinhCaoDo { get; set; }
		public string ThucHien { get; set; }
		public string PhuTrach { get; set; }
		public string HoTro { get; set; }
		public string CoQuanLienHe { get; set; }
		public string NguoiLienHe { get; set; }
		public string NguoiThucHien { get; set; }
		public DateTime? NgayBatDauThiCong { get; set; }
		public DateTime? NgayKetThucThiCong { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public double? DonGiaVatLieuDocVao { get; set; }
		public double? DonGiaNhanCongDocVao { get; set; }
		public double? DonGiaMayDocVao { get; set; }
		public string MoTa { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
