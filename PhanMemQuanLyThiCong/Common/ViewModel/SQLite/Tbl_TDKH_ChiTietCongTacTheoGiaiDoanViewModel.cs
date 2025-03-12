using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_TDKH_ChiTietCongTacTheoGiaiDoanViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string CodeCongTac { get; set; }
		public string CodeGiaiDoan { get; set; }
		public string CodePhatSinh { get; set; }
		public string CodeNhaThau { get; set; }
		public string CodeToDoi { get; set; }
		public string CodeNhaThauPhu { get; set; }
		public double KhoiLuongToanBo { get; set; } = 0;
		public string KhoiLuongToanBo_CongThuc { get; set; } = "";
		public bool? KhoiLuongToanBo_Iscongthucmacdinh { get; set; }
		public double KhoiLuongHopDongChiTiet { get; set; } = 0;
		public string CodeNhom { get; set; }
		public double DonGia { get; set; } = 0;
        public DateTime NgayBatDau { get; set; } = new DateTime(2023, 01, 01);
        public DateTime NgayKetThuc { get; set; } = new DateTime(2023, 01, 10);
        public double NhanCong { get; set; } = 0;
		public string NhanCong_CongThuc { get; set; } = "";
		public bool? NhanCong_Iscongthucmacdinh { get; set; }
		public long KinhPhiDuKien { get; set; } = 0;
		public string KinhPhiDuKien_CongThuc { get; set; } = "";
		public bool KinhPhiDuKien_Iscongthucmacdinh { get; set; } = false;
		public long KinhPhiTheoTienDo { get; set; } = 0;
		public string GhiChu { get; set; } = "";
		public DateTime? NgayBatDauThiCong { get; set; }
		public DateTime? NgayKetThucThiCong { get; set; }
		public int RowDoBoc { get; set; } = - 1;
		public int RowKeHoach { get; set; } = - 1;
		public int RowKhoiLuongHangNgay { get; set; } = - 1;
		public double DonGiaThiCong { get; set; } = 0;
		public long KinhPhiDuKienThiCong { get; set; } = 0;
		public double? HeSoQuyDoiDonVi { get; set; }
		public long? KinhPhiTheoTienDoThiCong { get; set; }
		public double? LoaiCT { get; set; }
		public string TrangThai { get; set; } = "Chưa thực hiện";
		public int SortId { get; set; } = - 1;
		public long? SortIdGoc { get; set; }
		public int? OriginalOrder { get; set; }
		public int? CustomOrder { get; set; }
		public string LyTrinhCaoDo { get; set; }
		public long? DonGiaDuThau { get; set; } = 0;
		public long DonGiaVatLieuDocVao { get; set; } = 0;
		public long DonGiaNhanCongDocVao { get; set; } = 0;
		public long DonGiaMayDocVao { get; set; } = 0;
		public bool IsUseMTC { get; set; } = false;
		public string CodeGop { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public string CodeMuiThiCong { get; set; }
		public string CodeCha { get; set; }
		public string KyHieuBanVe { get; set; }
		public string MaHieuCongTac { get; set; }
		public string TenCongTac { get; set; }
		public string DonVi { get; set; }
		public double? KhoiLuongHopDongToanDuAn { get; set; }
		public bool? PhatSinh { get; set; } = false;
		public bool? HasHopDongAB { get; set; } = false;
		public bool? PhanTichVatTu { get; set; }
		public string CodeCongTacGiaoThau { get; set; }
		public string CodeHangMuc { get; set; }
		public string CodePhanTuyen { get; set; }
		public long? STT { get; set; }
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
