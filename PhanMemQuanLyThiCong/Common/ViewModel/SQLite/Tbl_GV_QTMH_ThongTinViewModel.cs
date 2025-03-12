using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GV_QTMH_ThongTinViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeDuAn { get; set; } = "";
		public string TenQuyTrinh { get; set; }
		public string ThoiDiemGuiDeXuat { get; set; } = "01/01/2022";
		public string ThoiGianCanVatTu { get; set; } = "01/01/2022";
		public int Chon_1_DeXuat { get; set; } = 1;
		public string Chon_1_TimNcc { get; set; } = "1";
		public string Chon_1_ChonNcc { get; set; } = "1";
		public int Chon_1_DuyetPhuongAn { get; set; } = 1;
		public string Chon_2_DeXuat_DuyetDeXuat { get; set; } = "1";
		public string Chon_2_DeXuat_DuyetMua { get; set; } = "1";
		public string Chon_2_DeXuat_DuyetThanhToan { get; set; } = "1";
		public string Chon_2_TimNcc_ThucHien { get; set; } = "1";
		public string Chon_2_TimNcc_GiamSat { get; set; } = "1";
		public string Chon_2_ChonNcc_DanhGia { get; set; } = "1";
		public string Chon_2_ChonNcc_Duyet { get; set; } = "1";
		public string Chon_2_DuyetPhuongAn_Duyet { get; set; } = "1";
		public string Chon_2_DuyetPhuongAn_Mua { get; set; } = "1";
		public string DeXuat_NguoiDeXuat { get; set; }
		public string DeXuat_NguoiDuyetDeXuat { get; set; }
		public string DeXuat_ThoiGianDuyetDeXuat { get; set; } = "0";
		public string DeXuat_NguoiDuyetMua { get; set; }
		public string DeXuat_ThoiGianDuyetMua { get; set; } = "0";
		public string DeXuat_DuyetMua_CongTacTruoc { get; set; }
		public string DeXuat_DuyetMua_LoaiTuongQuan { get; set; }
		public string DeXuat_DuyetMua_SoNgay { get; set; } = "0";
		public string DeXuat_NguoiDuyetThanhToan { get; set; }
		public string DeXuat_ThoiGianDuyetThanhToan { get; set; } = "0";
		public string DeXuat_DuyetThanhToan_CongTacTruoc { get; set; }
		public string DeXuat_DuyetThanhToan_LoaiTuongQuan { get; set; }
		public string DeXuat_DuyetThanhToan_SoNgay { get; set; } = "0";
		public string TimNcc_PhongThucHien { get; set; }
		public string TimNcc_NguoiThucHien { get; set; }
		public string TimNcc_ThoiGianThucHien { get; set; } = "0";
		public string TimNcc_ThucHien_CongTacTruoc { get; set; }
		public string TimNcc_ThucHien_LoaiTuongQuan { get; set; }
		public string TimNcc_ThucHien_SoNgay { get; set; } = "0";
		public string TimNcc_PhongGiamSat { get; set; }
		public string TimNcc_NguoiGiamSat { get; set; }
		public string TimNcc_ThoiGianGiamSat { get; set; } = "0";
		public string TimNcc_GiamSat_CongTacTruoc { get; set; }
		public string TimNcc_GiamSat_LoaiTuongQuan { get; set; }
		public string TimNcc_GiamSat_SoNgay { get; set; } = "0";
		public string ChonNcc_PhongDanhGiaNcc { get; set; }
		public string ChonNcc_NguoiDanhGiaNcc { get; set; }
		public string ChonNcc_ThoiGianDanhGiaNcc { get; set; } = "0";
		public string ChonNcc_DanhGiaNcc_CongTacTruoc { get; set; }
		public string ChonNcc_DanhGiaNcc_LoaiTuongQuan { get; set; }
		public string ChonNcc_DanhGiaNcc_SoNgay { get; set; } = "0";
		public string ChonNcc_PhongDuyetNcc { get; set; }
		public string ChonNcc_NguoiDuyetNcc { get; set; }
		public string ChonNcc_ThoiGianDuyetNcc { get; set; } = "0";
		public string ChonNcc_DuyetNcc_CongTacTruoc { get; set; }
		public string ChonNcc_DuyetNcc_LoaiTuongQuan { get; set; }
		public string ChonNcc_DuyetNcc_SoNgay { get; set; } = "0";
		public string DuyetPhuongAn_PhongDuyet { get; set; }
		public string DuyetPhuongAn_NguoiDuyet { get; set; }
		public string DuyetPhuongAn_ThoiGianDuyet { get; set; } = "0";
		public string DuyetPhuongAn_Duyet_CongTacTruoc { get; set; }
		public string DuyetPhuongAn_Duyet_LoaiTuongQuan { get; set; }
		public string DuyetPhuongAn_Duyet_SoNgay { get; set; } = "0";
		public string DuyetPhuongAn_PhongMua { get; set; }
		public string DuyetPhuongAn_NguoiMua { get; set; }
		public string DuyetPhuongAn_ThoiGianMua { get; set; } = "0";
		public string DuyetPhuongAn_Mua_CongTacTruoc { get; set; }
		public string DuyetPhuongAn_Mua_LoaiTuongQuan { get; set; }
		public string DuyetPhuongAn_Mua_SoNgay { get; set; } = "0";
		public string DuyetPhuongAn_PhongNhanSanPham { get; set; }
		public string DuyetPhuongAn_NguoiNhanSanPham { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
