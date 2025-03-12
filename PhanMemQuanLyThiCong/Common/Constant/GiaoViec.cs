using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public enum LevelCongViec
    {
        Nhom,
        CongViecCha,
        CongViecCon
    };

    public enum TypeLoadKLHangNgay
    {
        Only1Day,
        RangeDate,
        BetweenNgayBatDauKetThuc,
        NgayBatDauBetween,
        DenHienTai,
        All
    }

    public static class GiaoViec
    {

        public const string Range_TieuDe = "TieuDe";
        public const string Range_Mau = "Mau";

        //public const string COL_RowCha = "1";
        public const string COL_NguoiDuyet = "2";
        public const string COL_HopDong = "3";

        public const string COL_Chon = "Chon";
        public const string COL_STT = "STT";

        public const string COL_MaDinhMuc = "MaDinhMuc";
        public const string COL_TenCongViec = "TenCongViec";
        public const string COL_DonVi = "DonVi";
        public const string COL_KLHopDong = "KhoiLuongHopDong";
        public const string COL_KLKeHoach = "KhoiLuongKeHoach";
        public const string COL_NguoiThucHien = "NguoiThucHien";

        public const string COL_NgayBatDau = "NgayBatDau";
        public const string COL_NgayKetThuc = "NgayKetThuc";
        public const string COL_SoNgay = "SoNgayThucHien";
        
        public const string COL_NgayBatDauThiCong = "NgayBatDauThiCong";
        public const string COL_NgayKetThucThiCong = "NgayKetThucThiCong";
        public const string COL_SoNgayThiCong = "SoNgayThiCong";
        
        public const string COL_NoiDungThucHien = "NoiDungThucHien";
        public const string COL_DonViThucHien = "DonViThucHien";

        public const string COL_DonGiaKeHoach = "DonGia";
        public const string COL_DonGiaThiCong = "DonGiaThiCong";
        public const string COL_LyTrinhCaoDo = "LyTrinhCaoDo";
        public const string COL_ThanhTienKeHoach = "ThanhTienKeHoach";
        public const string COL_FileDinhKem = "FileDinhKem";
        public const string COL_LoaiChiPhi = "LoaiChiPhi";
        public const string COL_CapDoQuanTrong = "CapDoQuanTrong";
        public const string COL_GhiChu = "GhiChu";
        public const string COL_TrangThai = "TrangThai";
        public const string COL_QuyTrinhThucHien = "QuyTrinhThucHien";
        public const string COL_GhiNhatKy = "GhiNhatKy";
        public const string COL_NhomKy = "NhomKy";
        public const string COL_LinkHop = "LinkHop";
        public const string COL_TypeRow = "TypeRow";//TypeRow

        public const string COL_RowCha = "RowCha";
        public const string COL_CodeCT = "CodeCT";

        public const string COL_ThucHien = "ThucHien";
        public const string COL_PhuTrach  = "PhuTrach";
        public const string COL_HoTro = "HoTro";
        public const string COL_CoQuanLienHe = "CoQuanLienHe";
        public const string COL_NguoiLienHe = "NguoiLienHe";
        //public const string COL_TenDuAn = "TenDuAn";
        public const string COL_CodeCTTheoGiaiDoan = "CodeCongTacTheoGiaiDoan";

        public const string view_ThongTinGiaoViecDuAnAll = "view_ThongTinGiaoViecDuAnAll";

        public const string TYPEROW_KHGV_THICONG = "ThiCong"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới

        public static string[] colHaveDb =
        {
            COL_MaDinhMuc,
            COL_TenCongViec,
            COL_DonVi,
            COL_NgayBatDau,
            COL_NgayKetThuc,
            COL_KLHopDong,
            COL_KLKeHoach,
            COL_NoiDungThucHien,
            COL_GhiChu,
            COL_TrangThai,
            COL_LinkHop,
            COL_DonGiaKeHoach,
            COL_DonGiaThiCong,
            COL_NgayBatDauThiCong,
            COL_NgayKetThucThiCong,
            COL_ThucHien,
            COL_PhuTrach,
            COL_HoTro,
            COL_CoQuanLienHe,
            COL_NguoiLienHe,
            COL_NguoiThucHien,
            //COL_NguoiLienHe,
        };

        public const string NhomGiaoViec_DuAn = "1. Dự án";
        public const string NhomGiaoViec_CongTacThuCong = "2. Công tác nhập thủ công";
        public const string NhomGiaoViec_CongTacTuKeHoach = "3. Công tác từ kế hoạch";

        /// <summary>
        /// Phần bảng giao việc
        /// </summary>
        /// 
        public const string TBL_DauViecLon = "Tbl_GiaoViec_DauViecLon"; //Giao việc đầu việc lớn
        public const string TBL_DauViecNho = "Tbl_GiaoViec_DauViecNho"; //Giao việc đầu việc nhỏ
        public const string TBL_NhomCongTacCha = "Tbl_GiaoViec_NhomCongTacCha";// Giao viec nhom cong tac cha                                                            
        public const string TBL_NhomCongTacCon = "Tbl_GiaoViec_NhomCongTacCon";// Giao viec nhom cong tac con
        public const string TBL_CONGVIECCHA = "Tbl_GiaoViec_CongViecCha"; //Công việc cha
        public const string TBL_CONGVIECCON = "Tbl_GiaoViec_CongViecCon"; //Công việc con
        public const string TBL_CHIATHICONG = "Tbl_GiaoViec_ChiaThiCong"; //Công việc con
        public const string TBL_FileDinhKem = "Tbl_GiaoViec_FileDinhKem"; //Công việc con
        public const string TBL_CongViecCon_FileDinhKem = Server.Tbl_GiaoViec_CongViecCon_FileDinhKem; //Công việc con
        public const string TBL_NguoiDuyet = "Tbl_GiaoViec_NguoiDuyet"; //Công việc con
        public const string TBL_KEHOACH_NGUOITHAMGIA = "Tbl_GiaoViec_KeHoach_NguoiThamGia"; //Công việc con
        public const string TBL_KEHOACH_NHACUNGCAP = "Tbl_GiaoViec_KeHoach_NhaCungCap"; //Công việc con
        public const string TBL_KEHOACH_NHATHAU = "Tbl_GiaoViec_KeHoach_NhaThau"; //Công việc con
        public const string TBL_KEHOACH_TODOI = "Tbl_GiaoViec_KeHoach_ToDoi"; //Công việc con
        public const string TBL_QUYTRINHTHUCHIEN = "Tbl_GiaoViec_QuyTrinhThucHien"; //Công việc con
        //public const string TBL_HAOPHIVATTU = "Tbl_GiaoViec_HaoPhiVatTu"; //Công việc con
        public const string TBL_THONGTINHOPDONG = "Tbl_GiaoViec_ThongTinHopDong"; //Công việc con
        public const string TBL_GV_QTMH_TimNCC_NguoiThamGia = "Tbl_GV_QTMH_TimNCC_NguoiThamGia"; //Công việc con
        public const string TBL_GV_QTMH_ChonNCC_NguoiThamGia = "Tbl_GV_QTMH_ChonNCC_NguoiThamGia"; //Công việc con
        public const string TBL_GV_QTMH_DuyetPA_NguoiThamGia = "Tbl_GV_QTMH_DuyetPA_NguoiThamGia"; //Công việc con
        //public const string TBL_BaoCaoCongViecHangNgay = "Tbl_GiaoViec_BaoCaoCongViecHangNgay"; //Công việc con

        public const string TBL_QuyTrinhThucHien = "Tbl_GiaoViec_QuyTrinhThucHien";// Giao viec quy trình thực hiện   
        //public const string TBL_FileDinhKem = "Tbl_GiaoViec_FileDinhKem";// Giao viec quy trình thực hiện   
    }
}
