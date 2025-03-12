using DevExpress.XtraSpreadsheet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public class Server
    {
        //DefinedTableDb
        #region SQLITE
        public const string Tbl_BangNhanVien_FileDinhKem = "Tbl_BangNhanVien_FileDinhKem";
        public const string Tbl_ChamCong_BangNhanVien = "Tbl_ChamCong_BangNhanVien";
        public const string Tbl_ChamCong_BangPhongBan = "Tbl_ChamCong_BangPhongBan";
        public const string Tbl_ChamCong_CaiDatGioLam = "Tbl_ChamCong_CaiDatGioLam";
        public const string Tbl_ChamCong_CaiDatHeSo = "Tbl_ChamCong_CaiDatHeSo";
        public const string Tbl_ChamCong_ChiTietPhuPhiNhanVien = "Tbl_ChamCong_ChiTietPhuPhiNhanVien";
        public const string Tbl_ChamCong_CongNhatCongKhoan = "Tbl_ChamCong_CongNhatCongKhoan";
        public const string Tbl_ChamCong_CongNhatCongKhoan_ChiTiet = "Tbl_ChamCong_CongNhatCongKhoan_ChiTiet";
        public const string Tbl_ChamCong_GioChamCong = "Tbl_ChamCong_GioChamCong";
        public const string Tbl_ChamCong_KyHieu = "Tbl_ChamCong_KyHieu";
        public const string Tbl_ChamCong_TamUng = "Tbl_ChamCong_TamUng";
        public const string Tbl_ChamCong_TamUng_FileDinhKem = "Tbl_ChamCong_TamUng_FileDinhKem";
        public const string Tbl_ChamCong_ViTriNhanVien = "Tbl_ChamCong_ViTriNhanVien";
        public const string Tbl_CongVanDiDen_DonViPhatHanh = "Tbl_CongVanDiDen_DonViPhatHanh";
        public const string Tbl_CongVanDiDen_FileDinhKem = "Tbl_CongVanDiDen_FileDinhKem";
        public const string Tbl_CongVanDiDen_QuanLyCongVan = "Tbl_CongVanDiDen_QuanLyCongVan";
        public const string Tbl_DanhmuCongTacTongmucdautu = "Tbl_DanhmuCongTacTongmucdautu";
        public const string Tbl_DanhmuCongTacduthau = "Tbl_DanhmuCongTacduthau";
        public const string Tbl_DanhmucCongTacVatlieu = "Tbl_DanhmucCongTacVatlieu";
        public const string Tbl_DanhmucCongTacVatlieu_Diendai = "Tbl_DanhmucCongTacVatlieu_Diendai";
        public const string Tbl_DeletedRecored = "Tbl_DeletedRecored";
        public const string Tbl_GV_QTMH_ChonNCC_NguoiThamGia = "Tbl_GV_QTMH_ChonNCC_NguoiThamGia";
        public const string Tbl_GV_QTMH_DeXuat_VatTu = "Tbl_GV_QTMH_DeXuat_VatTu";
        public const string Tbl_GV_QTMH_DuyetPA_NguoiThamGia = "Tbl_GV_QTMH_DuyetPA_NguoiThamGia";
        public const string Tbl_GV_QTMH_DuyetPhuongAn = "Tbl_GV_QTMH_DuyetPhuongAn";
        public const string Tbl_GV_QTMH_NhaCungCap = "Tbl_GV_QTMH_NhaCungCap";
        public const string Tbl_GV_QTMH_NhaCungCapDaChon = "Tbl_GV_QTMH_NhaCungCapDaChon";
        public const string Tbl_GV_QTMH_NhaCungCapDaDuyet = "Tbl_GV_QTMH_NhaCungCapDaDuyet";
        public const string Tbl_GV_QTMH_ThongTin = "Tbl_GV_QTMH_ThongTin";
        public const string Tbl_GV_QTMH_TimNCC_NguoiThamGia = "Tbl_GV_QTMH_TimNCC_NguoiThamGia";
        public const string Tbl_GiaoViec_ChiaThiCong = "Tbl_GiaoViec_ChiaThiCong";
        public const string Tbl_GiaoViec_CongViecCha = "Tbl_GiaoViec_CongViecCha";
        public const string Tbl_GiaoViec_CongViecCon = "Tbl_GiaoViec_CongViecCon";
        public const string Tbl_GiaoViec_CongViecCon_FileDinhKem = "Tbl_GiaoViec_CongViecCon_FileDinhKem";
        public const string Tbl_GiaoViec_DauViecLon = "Tbl_GiaoViec_DauViecLon";
        public const string Tbl_GiaoViec_DauViecNho = "Tbl_GiaoViec_DauViecNho";
        public const string Tbl_GiaoViec_FileDinhKem = "Tbl_GiaoViec_FileDinhKem";
        public const string Tbl_GiaoViec_KeHoach_NguoiThamGia = "Tbl_GiaoViec_KeHoach_NguoiThamGia";
        public const string Tbl_GiaoViec_NhomCongTacCha = "Tbl_GiaoViec_NhomCongTacCha";
        public const string Tbl_GiaoViec_NhomCongTacCon = "Tbl_GiaoViec_NhomCongTacCon";
        public const string Tbl_GiaoViec_QuyTrinhThucHien = "Tbl_GiaoViec_QuyTrinhThucHien";
        public const string Tbl_GiaoViec_ThongTinHopDong = "Tbl_GiaoViec_ThongTinHopDong";
        public const string Tbl_HopDongChiTiet = "Tbl_HopDongChiTiet";
        public const string Tbl_HopDong_CaiDatDot = "Tbl_HopDong_CaiDatDot";
        public const string Tbl_HopDong_DeNghiThanhToan = "Tbl_HopDong_DeNghiThanhToan";
        public const string Tbl_HopDong_DoBoc = "Tbl_HopDong_DoBoc";
        public const string Tbl_HopDong_DotHopDong = "Tbl_HopDong_DotHopDong";
        public const string Tbl_HopDong_DuLieuGoc = "Tbl_HopDong_DuLieuGoc";
        public const string Tbl_HopDong_FileDinhKem = "Tbl_HopDong_FileDinhKem";
        public const string Tbl_HopDong_LoaiHD = "Tbl_HopDong_LoaiHD";
        public const string Tbl_HopDong_LoaiHopDong = "Tbl_HopDong_LoaiHopDong";
        public const string Tbl_HopDong_PhuLucHD = "Tbl_HopDong_PhuLucHD";
        public const string Tbl_HopDong_PhuLucHD_ChiTietVL = "Tbl_HopDong_PhuLucHD_ChiTietVL";
        public const string Tbl_HopDong_ThongtinphulucHD = "Tbl_HopDong_ThongtinphulucHD";
        public const string Tbl_HopDong_TongHop = "Tbl_HopDong_TongHop";
        public const string Tbl_HopDong_TongHopACap = "Tbl_HopDong_TongHopACap";
        public const string Tbl_Hopdong_ChiTietCongTacCon = "Tbl_Hopdong_ChiTietCongTacCon";
        public const string Tbl_MTC_ChiTietDinhMuc = "Tbl_MTC_ChiTietDinhMuc";
        public const string Tbl_MTC_ChiTietNhatTrinh = "Tbl_MTC_ChiTietNhatTrinh";
        public const string Tbl_MTC_ChiTietNhatTrinh_FileDinhKem = "Tbl_MTC_ChiTietNhatTrinh_FileDinhKem";
        public const string Tbl_MTC_ChuSoHuuInMay = "Tbl_MTC_ChuSoHuuInMay";
        public const string Tbl_MTC_DanhSachChuSoHuu = "Tbl_MTC_DanhSachChuSoHuu";
        public const string Tbl_MTC_DanhSachMay = "Tbl_MTC_DanhSachMay";
        public const string Tbl_MTC_DinhMucThamKhao = "Tbl_MTC_DinhMucThamKhao";
        public const string Tbl_MTC_DuAnInMay = "Tbl_MTC_DuAnInMay";
        public const string Tbl_MTC_FileDinhKem = "Tbl_MTC_FileDinhKem";
        public const string Tbl_MTC_LoaiMay = "Tbl_MTC_LoaiMay";
        public const string Tbl_MTC_LoaiNhienLieu = "Tbl_MTC_LoaiNhienLieu";
        public const string Tbl_MTC_NguoiVanHanhInMay = "Tbl_MTC_NguoiVanHanhInMay";
        public const string Tbl_MTC_NhatTrinhCongTac = "Tbl_MTC_NhatTrinhCongTac";
        public const string Tbl_MTC_NhienLieu = "Tbl_MTC_NhienLieu";
        public const string Tbl_MTC_NhienLieuHangNgay = "Tbl_MTC_NhienLieuHangNgay";
        public const string Tbl_MTC_NhienLieuInMay = "Tbl_MTC_NhienLieuInMay";
        public const string Tbl_MTC_TrangThai = "Tbl_MTC_TrangThai";
        public const string Tbl_QLVT_ChuyenKhoVatTu = "Tbl_QLVT_ChuyenKhoVatTu";
        public const string Tbl_QLVT_ChuyenKho_KhoiLuongHangNgay = "Tbl_QLVT_ChuyenKho_KhoiLuongHangNgay";
        public const string Tbl_QLVT_KeHoachVatTu = "Tbl_QLVT_KeHoachVatTu";
        public const string Tbl_QLVT_KeHoachVatTu_CongTrinh = "Tbl_QLVT_KeHoachVatTu_CongTrinh";
        public const string Tbl_QLVT_KeHoachVatTu_HangMuc = "Tbl_QLVT_KeHoachVatTu_HangMuc";
        public const string Tbl_QLVT_NhapKho_FileDinhKem = "Tbl_QLVT_NhapKho_FileDinhKem";
        public const string Tbl_QLVT_NhapVattu_KhoiLuongHangNgay = "Tbl_QLVT_NhapVattu_KhoiLuongHangNgay";
        public const string Tbl_QLVT_Nhapvattu = "Tbl_QLVT_Nhapvattu";
        public const string Tbl_QLVT_NhatKyVC = "Tbl_QLVT_NhatKyVC";
        public const string Tbl_QLVT_QLVanChuyen = "Tbl_QLVT_QLVanChuyen";
        public const string Tbl_QLVT_QLVanChuyen_FileDinhKem = "Tbl_QLVT_QLVanChuyen_FileDinhKem";
        public const string Tbl_QLVT_TenKhoChung = "Tbl_QLVT_TenKhoChung";
        public const string Tbl_QLVT_TenKhoTheoDuAn = "Tbl_QLVT_TenKhoTheoDuAn";
        public const string Tbl_QLVT_TraVatTu = "Tbl_QLVT_TraVatTu";
        public const string Tbl_QLVT_XuatKho_FileDinhKem = "Tbl_QLVT_XuatKho_FileDinhKem";
        public const string Tbl_QLVT_XuatVatTu = "Tbl_QLVT_XuatVatTu";
        public const string Tbl_QLVT_XuatVatTu_KhoiLuongHangNgay = "Tbl_QLVT_XuatVatTu_KhoiLuongHangNgay";
        public const string Tbl_QLVT_YeuCauVatTu = "Tbl_QLVT_YeuCauVatTu";
        public const string Tbl_QLVT_YeuCauVatTu_FileDinhKem = "Tbl_QLVT_YeuCauVatTu_FileDinhKem";
        public const string Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgay = "Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgay";
        public const string Tbl_TCTU_DeXuat = "Tbl_TCTU_DeXuat";
        public const string Tbl_TCTU_DeXuat_ChiTiet = "Tbl_TCTU_DeXuat_ChiTiet";
        public const string Tbl_TCTU_DeXuat_ChiTietTDKH = "Tbl_TCTU_DeXuat_ChiTietTDKH";
        public const string Tbl_TCTU_DeXuat_ChiTiet_HopDong = "Tbl_TCTU_DeXuat_ChiTiet_HopDong";
        public const string Tbl_TCTU_DeXuat_Filedinhkem = "Tbl_TCTU_DeXuat_Filedinhkem";
        public const string Tbl_TCTU_KhoanChi = "Tbl_TCTU_KhoanChi";
        public const string Tbl_TCTU_KhoanChiChiTiet = "Tbl_TCTU_KhoanChiChiTiet";
        public const string Tbl_TCTU_KhoanChiChiTiet_Filedinhkem = "Tbl_TCTU_KhoanChiChiTiet_Filedinhkem";
        public const string Tbl_TCTU_KhoanChi_Filedinhkem = "Tbl_TCTU_KhoanChi_Filedinhkem";
        public const string Tbl_TCTU_KhoanThu = "Tbl_TCTU_KhoanThu";
        public const string Tbl_TCTU_KhoanThu_Filedinhkem = "Tbl_TCTU_KhoanThu_Filedinhkem";
        public const string Tbl_TCTU_LoaiKinhPhi = "Tbl_TCTU_LoaiKinhPhi";
        public const string Tbl_TCTU_NewKhoanChi = "Tbl_TCTU_NewKhoanChi";
        public const string Tbl_TCTU_PhuLucThuCong = "Tbl_TCTU_PhuLucThuCong";
        public const string Tbl_TDKH_CTTK_FileDinhKem = "Tbl_TDKH_CTTK_FileDinhKem";
        public const string Tbl_TDKH_ChiTietCongTacCon = "Tbl_TDKH_ChiTietCongTacCon";
        public const string Tbl_TDKH_ChiTietCongTacTheoGiaiDoan = "Tbl_TDKH_ChiTietCongTacTheoGiaiDoan";
        public const string Tbl_TDKH_DanhMucCongTac = "Tbl_TDKH_DanhMucCongTac";
        public const string Tbl_TDKH_Dependency = "Tbl_TDKH_Dependency";
        public const string Tbl_TDKH_DonGiaThiCongHangNgay = "Tbl_TDKH_DonGiaThiCongHangNgay";
        public const string Tbl_TDKH_GiaiDoanThucHien = "Tbl_TDKH_GiaiDoanThucHien";
        public const string Tbl_TDKH_HaoPhiVatTu = "Tbl_TDKH_HaoPhiVatTu";
        public const string Tbl_TDKH_HaoPhiVatTu_DonGia = "Tbl_TDKH_HaoPhiVatTu_DonGia";
        public const string Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay = "Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay";
        public const string Tbl_TDKH_KHVT_DonGia = "Tbl_TDKH_KHVT_DonGia";
        public const string Tbl_TDKH_KHVT_KhoiLuongHangNgay = "Tbl_TDKH_KHVT_KhoiLuongHangNgay";
        public const string Tbl_TDKH_KHVT_VatTu = "Tbl_TDKH_KHVT_VatTu";
        public const string Tbl_TDKH_KhoiLuongCongViecTungNgay = "Tbl_TDKH_KhoiLuongCongViecTungNgay";
        public const string Tbl_TDKH_KhoiLuongPhatSinh = "Tbl_TDKH_KhoiLuongPhatSinh";
        public const string Tbl_TDKH_KinhPhiDuKien = "Tbl_TDKH_KinhPhiDuKien";
        public const string Tbl_TDKH_KinhPhiPhanBo = "Tbl_TDKH_KinhPhiPhanBo";
        public const string Tbl_TDKH_MuiThiCong = "Tbl_TDKH_MuiThiCong";
        public const string Tbl_TDKH_NgayNghi = "Tbl_TDKH_NgayNghi";
        public const string Tbl_TDKH_NhomCongTac = "Tbl_TDKH_NhomCongTac";
        public const string Tbl_TDKH_NhomDienGiai = "Tbl_TDKH_NhomDienGiai";
        public const string Tbl_TDKH_Nhom_FileDinhKem = "Tbl_TDKH_Nhom_FileDinhKem";
        public const string Tbl_TDKH_PhanTuyen = "Tbl_TDKH_PhanTuyen";
        public const string Tbl_TDKH_SoLanPhatSinh = "Tbl_TDKH_SoLanPhatSinh";
        public const string Tbl_TDKH_TenDienDaiTuDo = "Tbl_TDKH_TenDienDaiTuDo";
        public const string Tbl_TDKH_TenGopCongTac = "Tbl_TDKH_TenGopCongTac";
        public const string Tbl_TDKH_TenGopNhom = "Tbl_TDKH_TenGopNhom";
        public const string Tbl_TDKH_TenGopPhanDoan = "Tbl_TDKH_TenGopPhanDoan";
        public const string Tbl_TaoHopDongMoi = "Tbl_TaoHopDongMoi";
        public const string Tbl_TaoHopDongMoi_Hopdongmecon = "Tbl_TaoHopDongMoi_Hopdongmecon";
        public const string Tbl_ThanhtoanTMDT = "Tbl_ThanhtoanTMDT";
        public const string Tbl_ThoiTiet = "Tbl_ThoiTiet";
        public const string Tbl_ThongTinCongTrinh = "Tbl_ThongTinCongTrinh";
        public const string Tbl_ThongTinDuAn = "Tbl_ThongTinDuAn";
        public const string Tbl_ThongTinDuAn_FileDinhKem = "Tbl_ThongTinDuAn_FileDinhKem";
        public const string Tbl_ThongTinDuAn_ThuyetMinh = "Tbl_ThongTinDuAn_ThuyetMinh";
        public const string Tbl_ThongTinHangMuc = "Tbl_ThongTinHangMuc";
        public const string Tbl_ThongTinNganSach = "Tbl_ThongTinNganSach";
        public const string Tbl_ThongTinNhaCungCap = "Tbl_ThongTinNhaCungCap";
        public const string Tbl_ThongTinNhaThau = "Tbl_ThongTinNhaThau";
        public const string Tbl_ThongTinNhaThauPhu = "Tbl_ThongTinNhaThauPhu";
        public const string Tbl_ThongTinToDoiThiCong = "Tbl_ThongTinToDoiThiCong";
        public const string Tbl_ThongTin_NhomThamGia = "Tbl_ThongTin_NhomThamGia";
        public const string Tbl_ThongTin_ThanhPhanThamGia = "Tbl_ThongTin_ThanhPhanThamGia";
        public const string Tbl_hopdongAB_HT = "Tbl_hopdongAB_HT";
        public const string Tbl_hopdongAB_PS = "Tbl_hopdongAB_PS";
        public const string Tbl_hopdongAB_TT = "Tbl_hopdongAB_TT";
        public const string Tbl_hopdongAB_TToan = "Tbl_hopdongAB_TToan";
        public const string Tbl_hopdongNCC_TT = "Tbl_hopdongNCC_TT";

        #endregion


        public static Dictionary<string, string> tblsFileDinhKem = new Dictionary<string, string>()
        {
            { Tbl_CongVanDiDen_FileDinhKem, Tbl_CongVanDiDen_QuanLyCongVan },
            { Tbl_GiaoViec_FileDinhKem, Tbl_GiaoViec_CongViecCha },
            { Tbl_GiaoViec_CongViecCon_FileDinhKem, Tbl_GiaoViec_CongViecCon},
            { Tbl_HopDong_FileDinhKem, Tbl_HopDong_TongHop},
            { Tbl_TCTU_DeXuat_Filedinhkem, Tbl_TCTU_DeXuat},
            { Tbl_TCTU_KhoanThu_Filedinhkem, Tbl_TCTU_KhoanThu},
            { Tbl_TCTU_KhoanChi_Filedinhkem, Tbl_TCTU_KhoanChi},
            { Tbl_TCTU_KhoanChiChiTiet_Filedinhkem, Tbl_TCTU_KhoanChiChiTiet},
            { Tbl_QLVT_NhapKho_FileDinhKem, Tbl_QLVT_Nhapvattu },
            { Tbl_QLVT_QLVanChuyen_FileDinhKem, Tbl_QLVT_QLVanChuyen },
            { Tbl_QLVT_XuatKho_FileDinhKem, Tbl_QLVT_XuatVatTu },
            { Tbl_QLVT_YeuCauVatTu_FileDinhKem, Tbl_QLVT_YeuCauVatTu },
            { Tbl_ChamCong_TamUng_FileDinhKem, Tbl_ChamCong_TamUng},
            { Tbl_BangNhanVien_FileDinhKem, Tbl_ChamCong_BangNhanVien},
            { Tbl_MTC_FileDinhKem, Tbl_MTC_DanhSachMay},
            { Tbl_MTC_ChiTietNhatTrinh_FileDinhKem, Tbl_MTC_ChiTietNhatTrinh},
            { Tbl_TDKH_Nhom_FileDinhKem, Tbl_TDKH_NhomCongTac},
            { Tbl_TDKH_CTTK_FileDinhKem, Tbl_TDKH_ChiTietCongTacTheoGiaiDoan}
        };

        public static string[] tbls_Contractor =
        {
            Tbl_ThongTinNhaThau,
            Tbl_ThongTinNhaThauPhu,
            Tbl_ThongTinToDoiThiCong,
        };



        public static string[] LS_CONST_TYPE_SERVER_TBL =
        {
            Tbl_ThongTinDuAn,//
            Tbl_QLVT_TenKhoChung,
            Tbl_TDKH_TenDienDaiTuDo,//
            Tbl_TDKH_GiaiDoanThucHien,//
            Tbl_MTC_DanhSachMay,
            Tbl_MTC_DinhMucThamKhao,
            Tbl_MTC_LoaiMay,
            Tbl_CongVanDiDen_DonViPhatHanh,
            Tbl_CongVanDiDen_QuanLyCongVan,//
            Tbl_ThongTinNhaThau,//
            Tbl_ThongTinToDoiThiCong,//
            Tbl_ThongTinNhaCungCap,//
            Tbl_ThongTinNhaThauPhu,//
            Tbl_ThongTinCongTrinh,//
            Tbl_ThongTinHangMuc,//
            Tbl_QLVT_TenKhoTheoDuAn,
            Tbl_ChamCong_BangPhongBan,
            Tbl_ChamCong_ViTriNhanVien,
            Tbl_ChamCong_BangNhanVien,
            Tbl_ChamCong_TamUng,
            Tbl_ChamCong_CongNhatCongKhoan,
            Tbl_TDKH_TenGopCongTac,
            Tbl_TDKH_TenGopNhom,
            Tbl_TDKH_TenGopPhanDoan,
            Tbl_TDKH_PhanTuyen,//
            Tbl_DanhmucCongTacVatlieu,//
            Tbl_HopDong_LoaiHopDong,//
            Tbl_TaoHopDongMoi,//
            Tbl_HopDong_TongHop,//
            Tbl_GiaoViec_DauViecLon,//
            Tbl_TDKH_NhomCongTac,//
            Tbl_TDKH_SoLanPhatSinh,//
            Tbl_TDKH_MuiThiCong,
            Tbl_TDKH_DanhMucCongTac,//
            Tbl_TDKH_ChiTietCongTacTheoGiaiDoan,//
            Tbl_GiaoViec_DauViecNho,//
            Tbl_GiaoViec_NhomCongTacCha,//
            Tbl_GiaoViec_CongViecCha,//
            Tbl_TDKH_KHVT_VatTu,//
            Tbl_TDKH_HaoPhiVatTu,//
            Tbl_TDKH_NhomDienGiai,//
            Tbl_GiaoViec_NhomCongTacCon,//
            Tbl_GiaoViec_CongViecCon,//
            Tbl_GV_QTMH_ThongTin,//
            Tbl_GV_QTMH_DeXuat_VatTu,//
            Tbl_GV_QTMH_NhaCungCap,//
            Tbl_GV_QTMH_NhaCungCapDaChon,//
            Tbl_HopDong_DotHopDong,//
            Tbl_HopDong_LoaiHD,//
            Tbl_HopDong_ThongtinphulucHD,//
            Tbl_HopDong_PhuLucHD,//
            Tbl_HopDong_DoBoc,//
            Tbl_QLVT_KeHoachVatTu_CongTrinh, //
            Tbl_QLVT_KeHoachVatTu_HangMuc, //
            Tbl_QLVT_KeHoachVatTu,//
            Tbl_QLVT_YeuCauVatTu,//
            Tbl_QLVT_Nhapvattu,//
            Tbl_QLVT_QLVanChuyen,//
            Tbl_QLVT_XuatVatTu,//                      
            Tbl_QLVT_ChuyenKhoVatTu,//
            Tbl_TCTU_DeXuat,//
            Tbl_TCTU_KhoanChi,//
            Tbl_TCTU_NewKhoanChi,//
            Tbl_TCTU_KhoanChiChiTiet,
            Tbl_TCTU_KhoanThu,
            Tbl_MTC_DanhSachChuSoHuu,
            Tbl_MTC_LoaiNhienLieu,
            Tbl_MTC_NhatTrinhCongTac,

//ListLastTable - Table mà không có bảng nào foreign đến
		    Tbl_ThongTinDuAn_FileDinhKem,
            Tbl_ThongTinDuAn_ThuyetMinh,
            Tbl_ChamCong_CongNhatCongKhoan_ChiTiet,
            Tbl_BangNhanVien_FileDinhKem,
            Tbl_ChamCong_CaiDatGioLam,
            Tbl_ChamCong_CaiDatHeSo,
            Tbl_ChamCong_ChiTietPhuPhiNhanVien,
            Tbl_ChamCong_GioChamCong,
            Tbl_ChamCong_KyHieu,
            Tbl_ChamCong_TamUng_FileDinhKem,
            Tbl_CongVanDiDen_FileDinhKem,
            Tbl_DanhmuCongTacTongmucdautu,
            Tbl_DanhmuCongTacduthau,
            Tbl_DanhmucCongTacVatlieu_Diendai,
            Tbl_DeletedRecored,
            Tbl_GV_QTMH_ChonNCC_NguoiThamGia,
            Tbl_GV_QTMH_DuyetPA_NguoiThamGia,
            Tbl_GV_QTMH_DuyetPhuongAn,
            Tbl_GV_QTMH_NhaCungCapDaDuyet,
            Tbl_GV_QTMH_TimNCC_NguoiThamGia,
            Tbl_GiaoViec_ChiaThiCong,
            Tbl_GiaoViec_CongViecCon_FileDinhKem,
            Tbl_GiaoViec_FileDinhKem,
            Tbl_GiaoViec_KeHoach_NguoiThamGia,
            Tbl_GiaoViec_QuyTrinhThucHien,
            Tbl_GiaoViec_ThongTinHopDong,
            Tbl_HopDongChiTiet,
            Tbl_HopDong_CaiDatDot,
            Tbl_HopDong_DuLieuGoc,
            Tbl_HopDong_FileDinhKem,
            Tbl_HopDong_PhuLucHD_ChiTietVL,
            Tbl_Hopdong_ChiTietCongTacCon,
            Tbl_MTC_ChiTietDinhMuc,
            Tbl_MTC_DuAnInMay,
            Tbl_MTC_ChiTietNhatTrinh,
            Tbl_MTC_ChuSoHuuInMay,
            Tbl_MTC_FileDinhKem,
            Tbl_MTC_NguoiVanHanhInMay,
            Tbl_MTC_NhienLieu,
            Tbl_MTC_NhienLieuInMay,
            Tbl_MTC_TrangThai,
            Tbl_MTC_NhienLieuHangNgay,
            Tbl_MTC_ChiTietNhatTrinh_FileDinhKem,
            Tbl_QLVT_TraVatTu,
            Tbl_QLVT_ChuyenKho_KhoiLuongHangNgay,
            Tbl_QLVT_NhapKho_FileDinhKem,
            Tbl_QLVT_NhapVattu_KhoiLuongHangNgay,
            Tbl_QLVT_NhatKyVC,
            Tbl_QLVT_QLVanChuyen_FileDinhKem,
            Tbl_QLVT_XuatKho_FileDinhKem,
            Tbl_QLVT_XuatVatTu_KhoiLuongHangNgay,
            Tbl_QLVT_YeuCauVatTu_FileDinhKem,
            Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgay,
            Tbl_TCTU_DeXuat_ChiTiet,
            Tbl_TCTU_DeXuat_ChiTietTDKH,
            Tbl_TCTU_DeXuat_ChiTiet_HopDong,
            Tbl_TCTU_DeXuat_Filedinhkem,
            Tbl_TCTU_KhoanChiChiTiet_Filedinhkem,
            Tbl_TCTU_KhoanChi_Filedinhkem,
            Tbl_TCTU_KhoanThu_Filedinhkem,
            Tbl_TCTU_LoaiKinhPhi,
            Tbl_TCTU_PhuLucThuCong,
            Tbl_TDKH_CTTK_FileDinhKem,
            Tbl_TDKH_ChiTietCongTacCon,
            Tbl_TDKH_Dependency,
            Tbl_TDKH_DonGiaThiCongHangNgay,
            Tbl_TDKH_HaoPhiVatTu_DonGia,
            Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay,
            Tbl_TDKH_KHVT_DonGia,
            Tbl_TDKH_KHVT_KhoiLuongHangNgay,
            Tbl_TDKH_KhoiLuongCongViecTungNgay,
            Tbl_TDKH_KhoiLuongPhatSinh,
            Tbl_TDKH_KinhPhiDuKien,
            Tbl_TDKH_KinhPhiPhanBo,
            Tbl_TDKH_NgayNghi,
            Tbl_TDKH_Nhom_FileDinhKem,
            Tbl_TaoHopDongMoi_Hopdongmecon,
            Tbl_ThanhtoanTMDT,
            Tbl_ThoiTiet,
            Tbl_ThongTinNganSach,
            Tbl_ThongTin_NhomThamGia,
            Tbl_ThongTin_ThanhPhanThamGia,
            Tbl_hopdongAB_HT,
            Tbl_hopdongAB_PS,
            Tbl_hopdongAB_TT,
            Tbl_hopdongAB_TToan,
            Tbl_hopdongNCC_TT,
            Tbl_HopDong_TongHopACap,
            Tbl_HopDong_DeNghiThanhToan,
        };

        public static Dictionary<string, Dictionary<string, string>> dicFks = new Dictionary<string, Dictionary<string, string>>()
        {
//AllDicForeign
#region SQLITE
			{Tbl_BangNhanVien_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_ChamCong_BangNhanVien"}
            } },

            {Tbl_ChamCong_BangNhanVien, new Dictionary<string, string>()
            {
                {"PhongBan","Tbl_ChamCong_BangPhongBan"},
                {"ChucVu","Tbl_ChamCong_ViTriNhanVien"}
            } },

            {Tbl_ChamCong_BangPhongBan, new Dictionary<string, string>()
            {

            } },

            {Tbl_ChamCong_CaiDatGioLam, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ChamCong_CaiDatHeSo, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ChamCong_ChiTietPhuPhiNhanVien, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"},
                {"CodeNhanVien","Tbl_ChamCong_BangNhanVien"}
            } },

            {Tbl_ChamCong_CongNhatCongKhoan, new Dictionary<string, string>()
            {
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_ChamCong_CongNhatCongKhoan_ChiTiet, new Dictionary<string, string>()
            {
                {"CodeCongTacThuCong","Tbl_ChamCong_CongNhatCongKhoan"},
                {"CodeCongTacTheoGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeNhanVien","Tbl_ChamCong_BangNhanVien"}
            } },

            {Tbl_ChamCong_GioChamCong, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"},
                {"CodeNhanVien","Tbl_ChamCong_BangNhanVien"}
            } },

            {Tbl_ChamCong_KyHieu, new Dictionary<string, string>()
            {

            } },

            {Tbl_ChamCong_TamUng, new Dictionary<string, string>()
            {
                {"CodeNhanVien","Tbl_ChamCong_BangNhanVien"},
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ChamCong_TamUng_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_ChamCong_TamUng"}
            } },

            {Tbl_ChamCong_ViTriNhanVien, new Dictionary<string, string>()
            {

            } },

            {Tbl_CongVanDiDen_DonViPhatHanh, new Dictionary<string, string>()
            {

            } },

            {Tbl_CongVanDiDen_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_CongVanDiDen_QuanLyCongVan"}
            } },

            {Tbl_CongVanDiDen_QuanLyCongVan, new Dictionary<string, string>()
            {
                {"CodeDonViPhatHanh","Tbl_CongVanDiDen_DonViPhatHanh"},
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_DanhmuCongTacTongmucdautu, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_DanhmuCongTacduthau, new Dictionary<string, string>()
            {
                {"CodeHangMuc","Tbl_ThongTinHangMuc"}
            } },

            {Tbl_DanhmucCongTacVatlieu, new Dictionary<string, string>()
            {
                {"CodeHangMuc","Tbl_ThongTinHangMuc"}
            } },

            {Tbl_DanhmucCongTacVatlieu_Diendai, new Dictionary<string, string>()
            {
                {"CodeVatLieu","Tbl_DanhmucCongTacVatlieu"}
            } },

            {Tbl_DeletedRecored, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_GV_QTMH_ChonNCC_NguoiThamGia, new Dictionary<string, string>()
            {
                {"CodeQuyTrinh","Tbl_GV_QTMH_ThongTin"}
            } },

            {Tbl_GV_QTMH_DeXuat_VatTu, new Dictionary<string, string>()
            {
                {"CodeQuyTrinh","Tbl_GV_QTMH_ThongTin"}
            } },

            {Tbl_GV_QTMH_DuyetPA_NguoiThamGia, new Dictionary<string, string>()
            {
                {"CodeQuyTrinh","Tbl_GV_QTMH_ThongTin"}
            } },

            {Tbl_GV_QTMH_DuyetPhuongAn, new Dictionary<string, string>()
            {
                {"CodeQuyTrinh","Tbl_GV_QTMH_ThongTin"}
            } },

            {Tbl_GV_QTMH_NhaCungCap, new Dictionary<string, string>()
            {
                {"CodeQuyTrinh","Tbl_GV_QTMH_ThongTin"}
            } },

            {Tbl_GV_QTMH_NhaCungCapDaChon, new Dictionary<string, string>()
            {
                {"CodeVatTu","Tbl_GV_QTMH_DeXuat_VatTu"},
                {"CodeNhaCungCap","Tbl_GV_QTMH_NhaCungCap"},
                {"CodeQuyTrinh","Tbl_GV_QTMH_ThongTin"}
            } },

            {Tbl_GV_QTMH_NhaCungCapDaDuyet, new Dictionary<string, string>()
            {
                {"CodeNhaCungCapDaChon","Tbl_GV_QTMH_NhaCungCapDaChon"}
            } },

            {Tbl_GV_QTMH_ThongTin, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_GV_QTMH_TimNCC_NguoiThamGia, new Dictionary<string, string>()
            {
                {"CodeQuyTrinh","Tbl_GV_QTMH_ThongTin"}
            } },

            {Tbl_GiaoViec_ChiaThiCong, new Dictionary<string, string>()
            {
                {"CodeCongViecCon","Tbl_GiaoViec_CongViecCon"}
            } },

            {Tbl_GiaoViec_CongViecCha, new Dictionary<string, string>()
            {
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeNhaCungCap","Tbl_ThongTinNhaCungCap"},
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodeNhom","Tbl_GiaoViec_NhomCongTacCha"},
                {"CodeCongTacTheoGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"},
                {"CodeDauMuc","Tbl_GiaoViec_DauViecNho"}
            } },

            {Tbl_GiaoViec_CongViecCon, new Dictionary<string, string>()
            {
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeNhaCungCap","Tbl_ThongTinNhaCungCap"},
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodeNhom","Tbl_GiaoViec_NhomCongTacCon"},
                {"CodeCongViecCha","Tbl_GiaoViec_CongViecCha"}
            } },

            {Tbl_GiaoViec_CongViecCon_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_GiaoViec_CongViecCon"}
            } },

            {Tbl_GiaoViec_DauViecLon, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_GiaoViec_DauViecNho, new Dictionary<string, string>()
            {
                {"CodeDauViecLon","Tbl_GiaoViec_DauViecLon"}
            } },

            {Tbl_GiaoViec_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_GiaoViec_CongViecCha"}
            } },

            {Tbl_GiaoViec_KeHoach_NguoiThamGia, new Dictionary<string, string>()
            {
                {"CodeCongViecCha","Tbl_GiaoViec_CongViecCha"}
            } },

            {Tbl_GiaoViec_NhomCongTacCha, new Dictionary<string, string>()
            {
                {"CodeHangMuc","Tbl_ThongTinHangMuc"}
            } },

            {Tbl_GiaoViec_NhomCongTacCon, new Dictionary<string, string>()
            {
                {"CodeCongViecCha","Tbl_GiaoViec_CongViecCha"}
            } },

            {Tbl_GiaoViec_QuyTrinhThucHien, new Dictionary<string, string>()
            {
                {"CodeCongViecChaTiepTheo","Tbl_GiaoViec_CongViecCha"},
                {"CodeCongViecConTiepTheo","Tbl_GiaoViec_CongViecCon"},
                {"CodeCongViecConHienTai","Tbl_GiaoViec_CongViecCon"},
                {"CodeCongViecChaHienTai","Tbl_GiaoViec_CongViecCha"}
            } },

            {Tbl_GiaoViec_ThongTinHopDong, new Dictionary<string, string>()
            {
                {"CodeCongViecCon","Tbl_GiaoViec_CongViecCon"}
            } },

            {Tbl_HopDongChiTiet, new Dictionary<string, string>()
            {
                {"CodeHopDong","Tbl_TaoHopDongMoi"}
            } },

            {Tbl_HopDong_CaiDatDot, new Dictionary<string, string>()
            {
                {"CodeHopDongCon","Tbl_TaoHopDongMoi"},
                {"CodeDotMe","Tbl_HopDong_DotHopDong"}
            } },

            {Tbl_HopDong_DeNghiThanhToan, new Dictionary<string, string>()
            {
                {"CodeDot","Tbl_HopDong_DotHopDong"}
            } },

            {Tbl_HopDong_DoBoc, new Dictionary<string, string>()
            {
                {"CodeNhom","Tbl_TDKH_NhomCongTac"},
                {"CodeDot","Tbl_HopDong_DotHopDong"},
                {"CodePL","Tbl_HopDong_PhuLucHD"}
            } },

            {Tbl_HopDong_DotHopDong, new Dictionary<string, string>()
            {
                {"CodeHd","Tbl_TaoHopDongMoi"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_HopDong_DuLieuGoc, new Dictionary<string, string>()
            {
                {"CodeHangMuc","Tbl_ThongTinHangMuc"}
            } },

            {Tbl_HopDong_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_HopDong_TongHop"}
            } },

            {Tbl_HopDong_LoaiHD, new Dictionary<string, string>()
            {

            } },

            {Tbl_HopDong_LoaiHopDong, new Dictionary<string, string>()
            {

            } },

            {Tbl_HopDong_PhuLucHD, new Dictionary<string, string>()
            {
                {"CodeNhom","Tbl_TDKH_NhomCongTac"},
                {"CodeHM","Tbl_ThongTinHangMuc"},
                {"CodeCongTacTheoGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeKHVT","Tbl_TDKH_KHVT_VatTu"},
                {"CodePl","Tbl_HopDong_ThongtinphulucHD"}
            } },

            {Tbl_HopDong_PhuLucHD_ChiTietVL, new Dictionary<string, string>()
            {
                {"CodeCongTacGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeCha","Tbl_HopDong_PhuLucHD"}
            } },

            {Tbl_HopDong_ThongtinphulucHD, new Dictionary<string, string>()
            {
                {"CodeHd","Tbl_HopDong_TongHop"},
                {"CodeLoaiHd","Tbl_HopDong_LoaiHD"}
            } },

            {Tbl_HopDong_TongHop, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"},
                {"CodeHopDong","Tbl_TaoHopDongMoi"}
            } },

            {Tbl_HopDong_TongHopACap, new Dictionary<string, string>()
            {
                {"CodeYeuCauVatTu","Tbl_QLVT_YeuCauVatTu"},
                {"CodeDot","Tbl_HopDong_DotHopDong"}
            } },

            {Tbl_Hopdong_ChiTietCongTacCon, new Dictionary<string, string>()
            {
                {"CodeNhom","Tbl_TDKH_NhomDienGiai"},
                {"CodeCongTacCha","Tbl_HopDong_DoBoc"}
            } },

            {Tbl_MTC_ChiTietDinhMuc, new Dictionary<string, string>()
            {
                {"CodeDinhMuc","Tbl_MTC_DinhMucThamKhao"},
                {"CodeMay","Tbl_MTC_DanhSachMay"}
            } },

            {Tbl_MTC_ChiTietNhatTrinh, new Dictionary<string, string>()
            {
                {"CodeCongTacThuCong","Tbl_MTC_NhatTrinhCongTac"},
                {"CodeCongTacTheoGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeMayDuAn","Tbl_MTC_DuAnInMay"},
                {"CodeDinhMuc","Tbl_MTC_ChiTietDinhMuc"}
            } },

            {Tbl_MTC_ChiTietNhatTrinh_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_MTC_ChiTietNhatTrinh"}
            } },

            {Tbl_MTC_ChuSoHuuInMay, new Dictionary<string, string>()
            {
                {"CodeChuSoHuu","Tbl_MTC_DanhSachChuSoHuu"},
                {"CodeMay","Tbl_MTC_DanhSachMay"}
            } },

            {Tbl_MTC_DanhSachChuSoHuu, new Dictionary<string, string>()
            {

            } },

            {Tbl_MTC_DanhSachMay, new Dictionary<string, string>()
            {

            } },

            {Tbl_MTC_DinhMucThamKhao, new Dictionary<string, string>()
            {

            } },

            {Tbl_MTC_DuAnInMay, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"},
                {"CodeMay","Tbl_MTC_DanhSachMay"}
            } },

            {Tbl_MTC_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_MTC_DanhSachMay"}
            } },

            {Tbl_MTC_LoaiMay, new Dictionary<string, string>()
            {

            } },

            {Tbl_MTC_LoaiNhienLieu, new Dictionary<string, string>()
            {

            } },

            {Tbl_MTC_NguoiVanHanhInMay, new Dictionary<string, string>()
            {
                {"CodeNguoiVanHanh","Tbl_ChamCong_BangNhanVien"},
                {"CodeMay","Tbl_MTC_DanhSachMay"}
            } },

            {Tbl_MTC_NhatTrinhCongTac, new Dictionary<string, string>()
            {
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_MTC_NhienLieu, new Dictionary<string, string>()
            {

            } },

            {Tbl_MTC_NhienLieuHangNgay, new Dictionary<string, string>()
            {
                {"CodeNhienLieu","Tbl_MTC_NhienLieu"}
            } },

            {Tbl_MTC_NhienLieuInMay, new Dictionary<string, string>()
            {
                {"CodeNhienLieu","Tbl_MTC_NhienLieu"},
                {"CodeMay","Tbl_MTC_DanhSachMay"}
            } },

            {Tbl_MTC_TrangThai, new Dictionary<string, string>()
            {

            } },

            {Tbl_QLVT_ChuyenKhoVatTu, new Dictionary<string, string>()
            {
                {"CodeDeXuat","Tbl_QLVT_YeuCauVatTu"},
                {"CodeNhapVT","Tbl_QLVT_Nhapvattu"},
                {"TenKhoChuyenDen","Tbl_QLVT_TenKhoTheoDuAn"},
                {"TenKhoChuyenDi","Tbl_QLVT_TenKhoTheoDuAn"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_QLVT_ChuyenKho_KhoiLuongHangNgay, new Dictionary<string, string>()
            {
                {"CodeCha","Tbl_QLVT_ChuyenKhoVatTu"}
            } },

            {Tbl_QLVT_KeHoachVatTu, new Dictionary<string, string>()
            {
                {"CodeHangMuc","Tbl_QLVT_KeHoachVatTu_HangMuc"}
            } },

            {Tbl_QLVT_KeHoachVatTu_CongTrinh, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_QLVT_KeHoachVatTu_HangMuc, new Dictionary<string, string>()
            {
                {"CodeCongTrinh","Tbl_QLVT_KeHoachVatTu_CongTrinh"}
            } },

            {Tbl_QLVT_NhapKho_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_QLVT_Nhapvattu"}
            } },

            {Tbl_QLVT_NhapVattu_KhoiLuongHangNgay, new Dictionary<string, string>()
            {
                {"CodeCha","Tbl_QLVT_Nhapvattu"}
            } },

            {Tbl_QLVT_Nhapvattu, new Dictionary<string, string>()
            {
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"},
                {"CodeDeXuat","Tbl_QLVT_YeuCauVatTu"},
                {"TenKhoNhap","Tbl_QLVT_TenKhoTheoDuAn"}
            } },

            {Tbl_QLVT_NhatKyVC, new Dictionary<string, string>()
            {
                {"CodeCha","Tbl_QLVT_QLVanChuyen"}
            } },

            {Tbl_QLVT_QLVanChuyen, new Dictionary<string, string>()
            {
                {"CodeDeXuat","Tbl_QLVT_YeuCauVatTu"},
                {"CodeNhapVT","Tbl_QLVT_Nhapvattu"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_QLVT_QLVanChuyen_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_QLVT_QLVanChuyen"}
            } },

            {Tbl_QLVT_TenKhoChung, new Dictionary<string, string>()
            {

            } },

            {Tbl_QLVT_TenKhoTheoDuAn, new Dictionary<string, string>()
            {
                {"CodeKhoChung","Tbl_QLVT_TenKhoChung"},
                {"CodeCongTrinh","Tbl_ThongTinCongTrinh"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"},
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_QLVT_TraVatTu, new Dictionary<string, string>()
            {
                {"CodeXuatVatTu","Tbl_QLVT_XuatVatTu"},
                {"CodeNhapVatTu","Tbl_QLVT_Nhapvattu"},
                {"TenKhoTra","Tbl_QLVT_TenKhoTheoDuAn"}
            } },


            {Tbl_QLVT_XuatKho_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_QLVT_XuatVatTu"}
            } },

            {Tbl_QLVT_XuatVatTu, new Dictionary<string, string>()
            {
                {"CodeDeXuat","Tbl_QLVT_YeuCauVatTu"},
                {"TenKhoNhap","Tbl_QLVT_TenKhoTheoDuAn"},
                {"CodeNhapVT","Tbl_QLVT_Nhapvattu"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_QLVT_XuatVatTu_KhoiLuongHangNgay, new Dictionary<string, string>()
            {
                {"CodeCha","Tbl_QLVT_XuatVatTu"}
            } },

            {Tbl_QLVT_YeuCauVatTu, new Dictionary<string, string>()
            {
                {"TenNhaCungCap","Tbl_ThongTinNhaCungCap"},
                {"CodeHd","Tbl_TaoHopDongMoi"},
                {"CodeTDKH","Tbl_QLVT_KeHoachVatTu"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"},
                {"CodeKHVT","Tbl_TDKH_KHVT_VatTu"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_QLVT_YeuCauVatTu_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_QLVT_YeuCauVatTu"}
            } },

            {Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgay, new Dictionary<string, string>()
            {
                {"CodeCha","Tbl_QLVT_YeuCauVatTu"}
            } },

            {Tbl_TCTU_DeXuat, new Dictionary<string, string>()
            {
                {"CodeCongTacTheoGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeKHVT","Tbl_TDKH_KHVT_VatTu"},
                {"CodeDuAn","Tbl_ThongTinDuAn"},
                {"CodeHd","Tbl_TaoHopDongMoi"}
            } },

            {Tbl_TCTU_DeXuat_ChiTiet, new Dictionary<string, string>()
            {
                {"CodeCha","Tbl_TCTU_DeXuat"}
            } },

            {Tbl_TCTU_DeXuat_ChiTietTDKH, new Dictionary<string, string>()
            {
                {"CodeVatTu","Tbl_TDKH_KHVT_VatTu"},
                {"CodeCongTac","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeCha","Tbl_TCTU_DeXuat"}
            } },

            {Tbl_TCTU_DeXuat_ChiTiet_HopDong, new Dictionary<string, string>()
            {
                {"CodePl","Tbl_HopDong_PhuLucHD"},
                {"CodeCha","Tbl_TCTU_DeXuat"}
            } },

            {Tbl_TCTU_DeXuat_Filedinhkem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_TCTU_DeXuat"}
            } },

            {Tbl_TCTU_KhoanChi, new Dictionary<string, string>()
            {
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodeNhaCungCap","Tbl_ThongTinNhaCungCap"},
                {"CodeDeXuat","Tbl_TCTU_DeXuat"}
            } },

            {Tbl_TCTU_KhoanChiChiTiet, new Dictionary<string, string>()
            {
                {"CodeKCNew","Tbl_TCTU_NewKhoanChi"},
                {"CodeKC","Tbl_TCTU_KhoanChi"}
            } },

            {Tbl_TCTU_KhoanChiChiTiet_Filedinhkem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_TCTU_KhoanChiChiTiet"}
            } },

            {Tbl_TCTU_KhoanChi_Filedinhkem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_TCTU_KhoanChi"}
            } },

            {Tbl_TCTU_KhoanThu, new Dictionary<string, string>()
            {
                {"CodeDeXuat","Tbl_TCTU_DeXuat"}
            } },

            {Tbl_TCTU_KhoanThu_Filedinhkem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_TCTU_KhoanThu"}
            } },

            {Tbl_TCTU_LoaiKinhPhi, new Dictionary<string, string>()
            {

            } },

            {Tbl_TCTU_NewKhoanChi, new Dictionary<string, string>()
            {
                {"CodeKC","Tbl_TCTU_KhoanChi"}
            } },

            {Tbl_TCTU_PhuLucThuCong, new Dictionary<string, string>()
            {
                {"CodeCha","Tbl_TCTU_DeXuat"}
            } },

            {Tbl_TDKH_CTTK_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"}
            } },

            {Tbl_TDKH_ChiTietCongTacCon, new Dictionary<string, string>()
            {
                {"CodeDoBocHD","Tbl_HopDong_DoBoc"},
                {"CodeNhom","Tbl_TDKH_NhomDienGiai"},
                {"CodeCongTacCha","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"}
            } },

            {Tbl_TDKH_ChiTietCongTacTheoGiaiDoan, new Dictionary<string, string>()
            {
                {"CodePhanTuyen","Tbl_TDKH_PhanTuyen"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"},
                {"CodeCongTacGiaoThau","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeCha","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeMuiThiCong","Tbl_TDKH_MuiThiCong"},
                {"CodeGop","Tbl_TDKH_TenGopCongTac"},
                {"CodeNhom","Tbl_TDKH_NhomCongTac"},
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodePhatSinh","Tbl_TDKH_SoLanPhatSinh"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"},
                {"CodeCongTac","Tbl_TDKH_DanhMucCongTac"}
            } },

            {Tbl_TDKH_DanhMucCongTac, new Dictionary<string, string>()
            {
                {"CodeDienDai","Tbl_TDKH_TenDienDaiTuDo"},
                {"CodeGop","Tbl_TDKH_TenGopCongTac"},
                {"CodePhanTuyen","Tbl_TDKH_PhanTuyen"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"}
            } },

            {Tbl_TDKH_Dependency, new Dictionary<string, string>()
            {
                {"Successorcode_CodeTuyen","Tbl_TDKH_PhanTuyen"},
                {"Predecessorcode_CodeTuyen","Tbl_TDKH_PhanTuyen"},
                {"Predecessorcode_CodeNhom","Tbl_TDKH_NhomCongTac"},
                {"Successorcode_CodeNhom","Tbl_TDKH_NhomCongTac"},
                {"Successorcode_CTR","Tbl_ThongTinCongTrinh"},
                {"Predecessorcode_CTR","Tbl_ThongTinCongTrinh"},
                {"Predecessorcode_HM","Tbl_ThongTinHangMuc"},
                {"Successorcode_HM","Tbl_ThongTinHangMuc"},
                {"Successorcode_KeHoach","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"Predecessorcode_KeHoach","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"}
            } },

            {Tbl_TDKH_DonGiaThiCongHangNgay, new Dictionary<string, string>()
            {
                {"CodeCongViecCon","Tbl_GiaoViec_CongViecCon"},
                {"CodeCongViecCha","Tbl_GiaoViec_CongViecCha"},
                {"CodeNhom","Tbl_TDKH_NhomCongTac"},
                {"CodeCongTacTheoGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"}
            } },

            {Tbl_TDKH_GiaiDoanThucHien, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_TDKH_HaoPhiVatTu, new Dictionary<string, string>()
            {
                {"CodeVatTu","Tbl_TDKH_KHVT_VatTu"},
                {"CodeCongViecCha","Tbl_GiaoViec_CongViecCha"},
                {"CodeCongTac","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"}
            } },

            {Tbl_TDKH_HaoPhiVatTu_DonGia, new Dictionary<string, string>()
            {
                {"CodeHaoPhiVatTu","Tbl_TDKH_HaoPhiVatTu"}
            } },

            {Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay, new Dictionary<string, string>()
            {
                {"CodeCongViecCon","Tbl_GiaoViec_CongViecCon"},
                {"CodeHaoPhiVatTu","Tbl_TDKH_HaoPhiVatTu"}
            } },

            {Tbl_TDKH_KHVT_DonGia, new Dictionary<string, string>()
            {
                {"CodeVatTu","Tbl_TDKH_KHVT_VatTu"}
            } },

            {Tbl_TDKH_KHVT_KhoiLuongHangNgay, new Dictionary<string, string>()
            {
                {"CodeVatTu","Tbl_TDKH_KHVT_VatTu"}
            } },

            {Tbl_TDKH_KHVT_VatTu, new Dictionary<string, string>()
            {
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"},
                {"CodeMuiThiCong","Tbl_TDKH_MuiThiCong"},
                {"CodeMay","Tbl_MTC_DanhSachMay"},
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodePhanTuyen","Tbl_TDKH_PhanTuyen"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"}
            } },

            {Tbl_TDKH_KhoiLuongCongViecTungNgay, new Dictionary<string, string>()
            {
                {"CodeCongViecCon","Tbl_GiaoViec_CongViecCon"},
                {"CodeCongViecCha","Tbl_GiaoViec_CongViecCha"},
                {"CodeNhom","Tbl_TDKH_NhomCongTac"},
                {"CodeCongTacTheoGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"}
            } },

            {Tbl_TDKH_KhoiLuongPhatSinh, new Dictionary<string, string>()
            {
                {"CodeCongTac","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodePhatSinh","Tbl_TDKH_SoLanPhatSinh"}
            } },

            {Tbl_TDKH_KinhPhiDuKien, new Dictionary<string, string>()
            {
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_TDKH_KinhPhiPhanBo, new Dictionary<string, string>()
            {
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_TDKH_MuiThiCong, new Dictionary<string, string>()
            {
                {"CodeNhanVien","Tbl_ChamCong_BangNhanVien"},
                {"CodeGiaiDoan","Tbl_TDKH_GiaiDoanThucHien"}
            } },

            {Tbl_TDKH_NgayNghi, new Dictionary<string, string>()
            {
                {"CodeCongTac","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"},
                {"CodeCongTrinh","Tbl_ThongTinCongTrinh"},
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_TDKH_NhomCongTac, new Dictionary<string, string>()
            {
                {"CodeDienDai","Tbl_TDKH_TenDienDaiTuDo"},
                {"CodeNhomGiaoThau","Tbl_TDKH_NhomCongTac"},
                {"CodeGop","Tbl_TDKH_TenGopNhom"},
                {"CodePhanTuyen","Tbl_TDKH_PhanTuyen"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"}
            } },

            {Tbl_TDKH_NhomDienGiai, new Dictionary<string, string>()
            {
                {"CodeCongTacTheoGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"}
            } },

            {Tbl_TDKH_Nhom_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_TDKH_NhomCongTac"}
            } },

            {Tbl_TDKH_PhanTuyen, new Dictionary<string, string>()
            {
                {"CodeDienDai","Tbl_TDKH_TenDienDaiTuDo"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"},
                {"CodeGop","Tbl_TDKH_TenGopPhanDoan"}
            } },

            {Tbl_TDKH_SoLanPhatSinh, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_TDKH_TenDienDaiTuDo, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_TDKH_TenGopCongTac, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_TDKH_TenGopNhom, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_TDKH_TenGopPhanDoan, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_TaoHopDongMoi, new Dictionary<string, string>()
            {
                {"CodeLoaiHopDong","Tbl_HopDong_LoaiHopDong"},
                {"CodeToDoi","Tbl_ThongTinToDoiThiCong"},
                {"CodeNcc","Tbl_ThongTinNhaCungCap"},
                {"CodeNhaThauPhu","Tbl_ThongTinNhaThauPhu"},
                {"CodeNhaThau","Tbl_ThongTinNhaThau"},
                {"CodeHangMuc","Tbl_ThongTinHangMuc"},
                {"CodeCongTrinh","Tbl_ThongTinCongTrinh"},
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_TaoHopDongMoi_Hopdongmecon, new Dictionary<string, string>()
            {
                {"CodeCon","Tbl_TaoHopDongMoi"},
                {"CodeMe","Tbl_TaoHopDongMoi"}
            } },

            {Tbl_ThanhtoanTMDT, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ThoiTiet, new Dictionary<string, string>()
            {
                {"CodeCongTrinh","Tbl_ThongTinCongTrinh"},
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ThongTinCongTrinh, new Dictionary<string, string>()
            {
                {"ThauChinh","Tbl_ThongTinNhaThau"},
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ThongTinDuAn, new Dictionary<string, string>()
            {

            } },
                        {Tbl_ThongTinDuAn_FileDinhKem, new Dictionary<string, string>()
            {
                {"CodeParent","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ThongTinDuAn_ThuyetMinh, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"},
                {"CodeParent","Tbl_ThongTinDuAn_ThuyetMinh"}
            } },

            {Tbl_ThongTinHangMuc, new Dictionary<string, string>()
            {
                {"CodeCongTrinh","Tbl_ThongTinCongTrinh"}
            } },

            {Tbl_ThongTinNganSach, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ThongTinNhaCungCap, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ThongTinNhaThau, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ThongTinNhaThauPhu, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ThongTinToDoiThiCong, new Dictionary<string, string>()
            {
                {"CodeDuAn","Tbl_ThongTinDuAn"}
            } },

            {Tbl_ThongTin_NhomThamGia, new Dictionary<string, string>()
            {
                {"CodeCongTrinh","Tbl_ThongTinCongTrinh"}
            } },

            {Tbl_ThongTin_ThanhPhanThamGia, new Dictionary<string, string>()
            {
                {"CodeCongTrinh","Tbl_ThongTinCongTrinh"}
            } },

            {Tbl_hopdongAB_HT, new Dictionary<string, string>()
            {
                {"CodeDB","Tbl_HopDong_DoBoc"},
                {"CodeDot","Tbl_HopDong_DotHopDong"}
            } },

            {Tbl_hopdongAB_PS, new Dictionary<string, string>()
            {
                {"CodeDB","Tbl_HopDong_DoBoc"},
                {"CodeDot","Tbl_HopDong_DotHopDong"}
            } },

            {Tbl_hopdongAB_TT, new Dictionary<string, string>()
            {
                {"CodeDB","Tbl_HopDong_DoBoc"},
                {"CodeDot","Tbl_HopDong_DotHopDong"}
            } },

            {Tbl_hopdongAB_TToan, new Dictionary<string, string>()
            {
                {"CodeNhom","Tbl_TDKH_NhomCongTac"},
                {"CodeCongTacTheoGiaiDoan","Tbl_TDKH_ChiTietCongTacTheoGiaiDoan"},
                {"CodeHopDong","Tbl_TaoHopDongMoi"}
            } },

            {Tbl_hopdongNCC_TT, new Dictionary<string, string>()
            {
                {"CodePhuLuc","Tbl_HopDong_PhuLucHD"},
                {"CodeDot","Tbl_HopDong_DotHopDong"}
            } }


#endregion


        };


        public static List<string> FkSetNull = new List<string>()
        {
            $"{Tbl_TDKH_DanhMucCongTac}_{Tbl_TDKH_PhanTuyen}",
            $"{Tbl_TDKH_DanhMucCongTac}_{Tbl_TDKH_TenGopCongTac}",
            $"{Tbl_TDKH_NhomCongTac}_{Tbl_TDKH_PhanTuyen}",
            $"{Tbl_CongVanDiDen_QuanLyCongVan}_{Tbl_CongVanDiDen_DonViPhatHanh}",
            $"{Tbl_TDKH_PhanTuyen}_{Tbl_TDKH_TenGopPhanDoan}",
            $"{Tbl_TDKH_NhomCongTac}_{Tbl_TDKH_TenGopNhom}",
            $"{Tbl_GiaoViec_CongViecCha}_{Tbl_GiaoViec_NhomCongTacCha}",
            $"{Tbl_GiaoViec_CongViecCon}_{Tbl_GiaoViec_NhomCongTacCon}",
            $"{Tbl_TDKH_ChiTietCongTacTheoGiaiDoan}_{Tbl_TDKH_NhomCongTac}",
            $"{Tbl_TDKH_ChiTietCongTacCon}_{Tbl_TDKH_NhomDienGiai}",
            $"{Tbl_MTC_ChiTietNhatTrinh}_{Tbl_MTC_ChiTietDinhMuc}",
            $"{Tbl_TDKH_HaoPhiVatTu}_{Tbl_TDKH_KHVT_VatTu}",
            $"{Tbl_TDKH_KHVT_VatTu}_{Tbl_TDKH_MuiThiCong}",
            $"{Tbl_TDKH_KHVT_VatTu}_{Tbl_MTC_DanhSachMay}",
        };

        public static Dictionary<string, Dictionary<string, string>> dicSetNull = new Dictionary<string, Dictionary<string, string>>()
        {
            {Tbl_TDKH_PhanTuyen, new Dictionary<string, string>(){
                {Tbl_TDKH_DanhMucCongTac , "CodePhanTuyen" } ,
                {Tbl_TDKH_NhomCongTac , "CodePhanTuyen" } ,
            } },

            {Tbl_CongVanDiDen_DonViPhatHanh, new Dictionary<string, string>(){ { Tbl_CongVanDiDen_QuanLyCongVan, "CodeDonViPhatHanh" } } },

            {Tbl_TDKH_TenGopPhanDoan, new Dictionary<string, string>(){ {Tbl_TDKH_PhanTuyen, "CodeGop" } } },
            {Tbl_TDKH_TenGopNhom, new Dictionary<string, string>(){ {Tbl_TDKH_NhomCongTac, "CodeGop" } } },
            {Tbl_TDKH_TenGopCongTac, new Dictionary<string, string>(){ {Tbl_TDKH_DanhMucCongTac, "CodeGop" } } },

            {Tbl_GiaoViec_NhomCongTacCha, new Dictionary<string, string>(){ { Tbl_GiaoViec_CongViecCha, "CodeNhom" } } },
            {Tbl_GiaoViec_NhomCongTacCon, new Dictionary<string, string>(){ { Tbl_GiaoViec_CongViecCon, "CodeNhom" } } },
            {Tbl_TDKH_NhomCongTac, new Dictionary<string, string>(){ { Tbl_TDKH_ChiTietCongTacTheoGiaiDoan, "CodeNhom" } } },
            {Tbl_TDKH_NhomDienGiai, new Dictionary<string, string>(){ { Tbl_TDKH_ChiTietCongTacCon, "CodeNhom" } } },
            {Tbl_MTC_ChiTietDinhMuc, new Dictionary<string, string>(){ { Tbl_MTC_ChiTietNhatTrinh, "CodeDinhMuc" } } },
            {Tbl_TDKH_KHVT_VatTu, new Dictionary<string, string>(){ { Tbl_TDKH_HaoPhiVatTu, "CodeVatTu" } } },
            {Tbl_TDKH_MuiThiCong, new Dictionary<string, string>(){ { Tbl_TDKH_KHVT_VatTu, "CodeMuiThiCong" } } },
            {Tbl_MTC_DanhSachMay, new Dictionary<string, string>(){ { Tbl_TDKH_KHVT_VatTu, "CodeMay" } } },
                        {Tbl_TDKH_TenDienDaiTuDo, new Dictionary<string, string>(){
                { Tbl_TDKH_DanhMucCongTac, "CodeDienDai" },
                { Tbl_TDKH_NhomCongTac, "CodeDienDai" },
                { Tbl_TDKH_PhanTuyen, "CodeDienDai" } ,
            } },
            
            //{Tbl_MTC_DanhSachMay, new Dictionary<string, string>(){ { Tbl_TDKH_ChiTietCongTacTheoGiaiDoan, "CodeMay" } } },
        };


        public static Dictionary<string, string[]> dicTblIndex = new Dictionary<string, string[]>()
        {
            {Tbl_TDKH_ChiTietCongTacTheoGiaiDoan, new string[] {"CodeCongTac", "CodeNhaThau", "CodeNhaThauPhu", "CodeToDoi" } },
            {Tbl_TDKH_KHVT_VatTu, new string[] { "MaVatLieu", "MaTXHienTruong", "VatTu", "DonGia", "DonVi", "CodeHangMuc", "CodePhanTuyen", "CodeGiaiDoan", "CodeNhaThau", "CodeNhaThauPhu", "CodeToDoi" } },
            {Tbl_TDKH_KHVT_KhoiLuongHangNgay, new string[] { "Ngay", "CodeVatTu" } },
            {Tbl_TDKH_KhoiLuongCongViecTungNgay, new string[] { "Ngay", "CodeCongTacTheoGiaiDoan", "CodeNhom" } },
            {Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay, new string[] { "Ngay", "CodeHaoPhiVatTu" } },
            {Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgay, new string[] { "Ngay", "CodeCha" } },
            {Tbl_QLVT_XuatVatTu_KhoiLuongHangNgay, new string[] { "Ngay", "CodeCha" } },
            {Tbl_QLVT_NhapVattu_KhoiLuongHangNgay, new string[] { "Ngay", "CodeCha" } },
            {Tbl_QLVT_ChuyenKho_KhoiLuongHangNgay, new string[] { "Ngay", "CodeCha" } },
            {Tbl_QLVT_NhatKyVC, new string[] { "Ngay", "CodeCha" } },
            {Tbl_ThoiTiet, new string[] { "Ngay", "CodeDuAn", "CodeCongTrinh" } },
        };


    }
}