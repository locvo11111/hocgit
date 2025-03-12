
using DevExpress.XtraEditors.Filtering;
using PhanMemQuanLyThiCong.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public enum FunctionCode
    {
        #region ExtenalFunction
        [Display(Name = "Tạo dự án")]
        EXTERNALCREATEPROJECT,
        #endregion

        #region KIỂM SOÁT
        // Thêm __EX để chia sẻ ra tài khoản khách
        [Display(Name = "Kiểm soát")]
        [Description("xtraTabPage_KiemSoat")]
        KIEMSOAT,

        [Display(Name = "Kiếm soát chung")]
        [Description("xtraTab_KiemSoatChung")]
        KIEMSOAT_KIEMSOATCHUNG,

        [Display(Name = "Báo cáo dòng tiền")]
        [Description("xtraTab_BaoCaoDongTien")]
        KIEMSOAT_BAOCAODONGTIEN,

        [Display(Name = "Báo cáo lợi nhuận")]
        [Description("xtraTab_BaoCaoLoiNhuan")]
        KIEMSOAT_BAOCAOLOINHUAN,

        [Display(Name = "Báo cáo hợp đồng")]
        [Description("xtraTab_BaoCaoHopDong")]
        KIEMSOAT_BAOCAOHOPDONG,

        [Display(Name = "Kiểm soát tiến độ")]
        [Description("xtraTab_KiemSoatTienDo")]
        KIEMSOAT_KIEMSOATTIENDO,

        [Display(Name = "Khối lượng - Tiến độ")]
        [Description("xtraTab_KhoiLuong_TienDo")]
        KIEMSOAT_KHOILUONGTIENDO,

        [Display(Name = "Vật tư")]
        [Description("xtraTab_VLMTCNC")]
        KIEMSOAT_VLMTCNC,

        #endregion

        #region Danh Sách dự án công trình
        [Display(Name = "Danh sách Dự án - Công trình")]
        [Description("xtraTabPage_DSDuAn_CT")]
        DSDUANCT,
        #endregion

        #region Thông Tin Dự án
        [Display(Name = "Thông tin dự án")]
        [Description("xtraTabPage_ThongTinDuAn")]
        THONGTINDUAN,

        [Display(Name = "Thông tin")]
        [Description("xtraTab_ThongTin")]
        THONGTINDUAN_THONGTIN,

        [Display(Name = "Thành phần tham gia")]
        [Description("xtraTab_ThanhPhanThamGia")]
        THONGTINDUAN_THANHPHANTHAMGIA,

        #endregion

        #region giao việc
        [Display(Name = "Giao việc")]
        [Description("xtraTabPage_GiaoViec")]
        GIAOVIEC,

        [Display(Name = "Kế hoạch giao việc")]
        [Description("xtraTabKeHoachGiaoViec")]
        GIAOVIEC_KEHOACHGIAOVIEC,

        [Display(Name = "Thực hiện dự án")]
        [Description("xtraTabThucHienDuAn")]
        GIAOVIEC_THUCHIENDUAN,


        [Display(Name = "Báo cáo công việc hàng ngày")]
        [Description("xtraTabBaoCaoCongViecHangNgay")]
        GIAOVIEC_BAOCAOCONGVIECHANGNGAY__EX,

        [Display(Name = "*Tiến độ giao việc")]
        [Description("xtraTabTienDoGiaoViec")]
        GIAOVIEC_TIENDOGIAOVIEC,

        [Display(Name = "*Đánh giá hiệu quả")]
        [Description("xtraTabDanhGiaHieuQua")]
        GIAOVIEC_DANHGIAHIEUQUA,

        [Display(Name = "*Quy trình thực hiện")]
        [Description("xtraTabQuyTrinhThucHien")]
        GIAOVIEC_QUYTRINHTHUCHIEN,

        [Display(Name = "*Quy trình mua hàng")]
        [Description("xtraTabQuyTrinhMuaHang")]
        GIAOVIEC_QUYTRINHMUAHANG,

        [Display(Name = "*Theo dõi mua hàng")]
        [Description("xtraTabTheoDoiMuaHang")]
        GIAOVIEC_THEODOIMUAHANG,

        [Display(Name = "*Kiểm soát giao việc")]
        [Description("xtraTabKiemSoatGiaoViec")]
        GIAOVIEC_KIEMSOATGIAOVIEC,
        #endregion

        #region QL Hợp đồng - TT
        [Display(Name = "QL hợp đồng - Thanh toán")]
        [Description("xtraTabPage_QLHDong_TT")]
        QLHDONGTT,

        [Display(Name = "Chi tiết phụ lục giá")]
        [Description("xtraTab_ChiTietPhuLucGia")]
        QLHDONGTT_CHITIETPHULUCGIA,

        [Display(Name = "Tổng hợp danh sách các hợp đồng")]
        [Description("xtraTab_TongHopHD")]
        QLHDONGTT_TONGHOPHD,

        [Display(Name = "*Giá trị dự án")]
        [Description("xtraTab_GiaTriDuAn")]
        QLHDONGTT_GIATRIDUAN,

        [Display(Name = "Tự Thực Hiện")]
        [Description("xtraTab_TuThucHien")]
        QLHDONGTT_TUTHUCHIEN,

        [Display(Name = "Thanh toán A-B")]
        [Description("xtraTab_ThanhToanAB")]
        QLHDONGTT_THANHTOANAB__EX,

        [Display(Name = "Thanh toán B-B'")]
        [Description("xtraTab_ThanhToanBB")]
        QLHDONGTT_THANHTOANBB__EX,

        [Display(Name = "Thanh toán tổ đội")]
        [Description("xtraTab_ThanhToanToDoi")]
        QLHDONGTT_THANHTOANTODOI__EX,

        [Display(Name = "Thanh toán nhà CC")]
        [Description("xtraTab_ThanhToanNhaCC")]
        QLHDONGTT_THANHTOANNHACC__EX,

        [Display(Name = "*Tạo hợp đồng mới")]
        [Description("xtraTab_TaoHDMoi")]
        QLHDONGTT_TAOHDMOI,

        #endregion

        #region Tiến độ kế hoạch
        [Display(Name = "QL tiến độ - Kế hoạch")]
        [Description("xtraTab_QLTienDo_KeHoach")]
        QLTIENDOKEHOACH,

        [Display(Name = "Tiến độ")]
        [Description("xtraTab_TienDo")]
        QLTIENDOKEHOACH_TIENDO__EX,

        [Display(Name = "Kế hoạch")]
        [Description("xtraTab_KeHoach")]
        QLTIENDOKEHOACH_KEHOACH__EX,

        [Display(Name = "Khối lượng hàng ngày")]
        [Description("xtraTab_KhoiLuongHangNgay")]
        QLTIENDOKEHOACH_KLHN__EX,

        [Display(Name = "Thẩm định đầu vào")]
        [Description("xtraTab_ThamDinhDauVao")]
        QLTIENDOKEHOACH_ThamDinh__EX,

        [Display(Name = "*Tạo báo cáo tiến độ - Kế hoạch")]
        [Description("xtraTab_TaoBaoCaoTienDoKeHoach")]
        QLTIENDOKEHOACH_TAOBAOCAOTIENDOKEHOACH__EX,

        [Display(Name = "*Phụ lục khối lượng báo cáo")]
        [Description("xtraTab_PhuLucKLBaoCao")]
        QLTIENDOKEHOACH_PHULUCKLBAOCAO__EX,

        [Display(Name = "*Khối lượng thực hiện")]
        [Description("xtraTab_KhoiLuongThucHien")]
        QLTIENDOKEHOACH_KHOILUONGTHUCHIEN__EX,
        #endregion

        #region QL Vật liệu -Vận chuyển
        [Display(Name = "QL vật liệu - Vận chuyển")]
        [Description("xtraTab_QLVatLieu_VanChuyen")]
        QLVATLIEUVANCHUYEN,

        [Display(Name = "Đề xuất khối lượng")]
        [Description("xtraTab_DeXuatKhoiLuong")]
        QLVATLIEUVANCHUYEN_DEXUATKHOILUONG__EX,

        [Display(Name = "Nhập kho")]
        [Description("xtraTab_NhapKho")]
        QLVATLIEUVANCHUYEN_NHAPKHO,

        [Display(Name = "Xuất kho")]
        [Description("xtraTab_XuatKho")]
        QLVATLIEUVANCHUYEN_XUATKHO,

        [Display(Name = "Chuyển kho")]
        [Description("xtraTab_ChuyenKho")]
        QLVATLIEUVANCHUYEN_CHUYENKHO,

        [Display(Name = "Thông tin kho vật liệu")]
        [Description("xtraTab_ThongTinKhoVatLieu")]
        QLVATLIEUVANCHUYEN_THONGTINKHOVATLIEU,

        [Display(Name = "Kế hoạch vật tư")]
        [Description("xtraTab_KHVT")]
        QLVATLIEUVANCHUYEN_KHVT,

        [Display(Name = "Quản lý vận chuyển")]
        [Description("xtraTab_QLVanChuyen")]
        QLVATLIEUVANCHUYEN_QLVANCHUYEN__EX,
        #endregion

        #region Quản lý Máy Thiết Bị
        [Display(Name = "QL máy - Thiết bị")]
        [Description("xtraTabPage_QLMay_TB")]
        QLMAYTB,

        [Display(Name = "Nhật trình thi công")]
        [Description("xtraTab_NhatTrinhThiCong")]
        QLMAYTB_NHATTRINHTHICONG__EX,

        [Display(Name = "Danh sách phương tiện")]
        [Description("xtraTab_DanhSachPhuongTien")]
        QLMAYTB_DANHSACHPHUONGTIEN,

        [Display(Name = "Sửa chữa - Bảo dưỡng")]
        [Description("xtraTab_SuaChuaBaoDuong")]
        QLMAYTB_SUACHUABAODUONG,

        [Display(Name = "Dữ liệu máy tham khảo")]
        [Description("xtraTab_DuLieuMayThamKhao")]
        QLMAYTB_DULIEUMAYTHAMKHAO,

        [Display(Name = "Đề xuất máy thi công")]
        [Description("xtraTab_DeXuatMayThiCong")]
        QLMAYTB_DEXUATMAYTHICONG,

        [Display(Name = "Chi tiết lịch trình thiết bị")]
        [Description("xtraTab_ChiTietLichTrinhThietBi")]
        QLMAYTB_CHITIETLICHTRINHTHIETBI,

        [Display(Name = "Cài đặt số liệu bảo dưỡng")]
        [Description("xtraTab_CaiDatSoLieuBaoDuong")]
        QLMAYTB_CAIDATSOLIEUBAODUONG,

        [Display(Name = "Đề xuất phương tiện công tác")]
        [Description("xtraTab_DeXuatPhuongTienCongTac")]
        QLMAYTB_DEXUATPHUONGTIENCONGTAC,
        #endregion

        #region Thu Chi - Tạm ứng
        [Display(Name = "Thu Chi - Tạm ứng")]
        [Description("xtraTabPage_ThuChi_TamUng")]
        THUCHITAMUNG,

        [Display(Name = "Đề xuất")]
        [Description("xtraTab_ThuChi_DeXuat")]
        THUCHITAMUNG_THUCHIDEXUAT__EX,

        [Display(Name = "Khoản chi")]
        [Description("xtraTab_KhoanChi")]
        THUCHITAMUNG_KHOANCHI,

        [Display(Name = "Khoản thu")]
        [Description("xtraTab_TU_KhoanThu")]
        THUCHITAMUNG_TUKHOANTHU,

        #endregion


        #region Chấm công
        [Display(Name = "Chấm công")]
        [Description("xtraTabPage_ChamCong")]
        CHAMCONG,

        [Display(Name = "Bảng chấm công")]
        [Description("xtraTab_BangChamCong")]
        CHAMCONG_BANGCHAMCONG__EX,

        [Display(Name = "Bảng thanh toán lương")]
        [Description("xtraTab_BangThanhToanLuong")]
        CHAMCONG_BANGTHANHTOANLUONG,

        [Display(Name = "Bảng tính lương")]
        [Description("xtraTab_BangTinhLuong")]
        CHAMCONG_BANGTINHLUONG,

        [Display(Name = "Đơn từ - Thanh toán")]
        [Description("xtraTab_DonTu_ThanhToan")]
        CHAMCONG_DONTUTHANHTOAN,

        [Display(Name = "Tạm ứng")]
        [Description("xtraTab_TamUng")]
        CHAMCONG_TAMUNG,

        [Display(Name = "Danh sách nhân viên")]
        [Description("xtraTab_DanhSachNhanVien")]
        CHAMCONG_DANHSACHNHANVIEN,

        [Display(Name = "Cài đặt bảng chấm công")]
        [Description("xtraTab_CaiDatBangChamCong")]
        CHAMCONG_CAIDATBANGCHAMCONG,

        [Display(Name = "Bảng quy đổi lương ngoài giờ")]
        [Description("xtraTab_BangQuyDoiLuongNgoaiGio")]
        CHAMCONG_BANGQUYDOILUONGNGOAIGIO,

        [Display(Name = "Vị trí chấm công")]
        [Description("xtraTab_ViTriChamCong")]
        CHAMCONG_VITRICHAMCONG,

        [Display(Name = "Lịch công tác")]
        [Description("xtraTab_LichCongTac")]
        CHAMCONG_LICHCONGTAC,
        #endregion

        #region Công văn đi đên
        [Display(Name = "Công văn đi đến")]
        [Description("xtraTabPage_CongVanDiDen")]
        CONGVANDIDEN__EX,
        #endregion
    }
}
