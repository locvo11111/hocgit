using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    //Tiến độ kế hoạch - Đo bóc chuẩn
    public static class TDKH
    {

        public const string formatStringVatTu = "{0}.MaVatLieu || ';' || {0}.VatTu || ';' || {0}.DonVi || ';' || {0}.DonGia || ';' || {0}.LoaiVatTu|| ';' || {0}.CodeHangMuc|| ';' || COALESCE({0}.CodePhanTuyen, \"\") || ';' || COALESCE({0}.CodeMuiThiCong, \"\")";
        public const string formatStringDonViThucHien = "COALESCE({0}.CodeNhaThau, {0}.CodeNhaThauPhu, {0}.CodeToDoi, '')";

        public const string VatTu_VatLieuKhac = "Vật liệu khác"; 

        public const string Suffix_DinhMucTamTinh = "  ";
        public const string TBL_GiaiDoanThucHien = "Tbl_TDKH_GiaiDoanThucHien";
        public const string TBL_KinhPhiPhanBo = "Tbl_TDKH_KinhPhiPhanBo";
        public const string TBL_SoLanPhatSinh = "Tbl_TDKH_SoLanPhatSinh";
        //public const string TBL_KhoiLuongPhatSinh = "Tbl_TDKH_KhoiLuongPhatSinh";

        public const string RANGE_DoBocChuan = "DoBocChuan";
        public const string SheetName_DoBocChuan = "Đo bóc chuẩn";    
        public const string SheetName_DoBoc= "Đo bóc";

        public const int TYPEDoBoc_AllDA = 0;
        public const int TYPEDoBoc_NhaThau = 1;
        public const int TYPEDoBoc_ToDoi = 2;
        public const int TYPEDoBoc_NhaThauPhu = 3;
        
        public const int TYPELOAD_DoBoc = 0;
        public const int TYPELOAD_KeHoach = 1;
        public const int TYPELOAD_TienDo = 2;
        //public const int TYPELOAD_KeHoach_KLHangNgay = 2;
        

        public const int TYPE_TaskTienDo_DuAn = 0;
        public const int TYPE_TaskTienDo_CongTrinh = 1;
        public const int TYPE_TaskTienDo_HangMuc = 2;
        public const int TYPE_TaskTienDo_CongViecTong = 3;
        public const int TYPE_TaskTienDo_CongViec = 4;
        public const int TYPE_TaskTienDo_KeHoach = 5;
        public const int TYPE_TaskTienDo_ThiCong = 6;   
        
        public const int TYPE_TaskTienDo_DuAn_New = 0;
        public const int TYPE_TaskTienDo_CongTrinh_New = 1;
        public const int TYPE_TaskTienDo_HangMuc_New = 2;
        public const int TYPE_TaskTienDo_MTC_New = 2;
        public const int TYPE_TaskTienDo_NhaThau = 3;
        public const int TYPE_TaskTienDo_Tuyen_New = 4; 
        public const int TYPE_TaskTienDo_CongViecKhongThuocTuyenNhom= 4;
        public const int TYPE_TaskTienDo_CongViecThuocTuyen =5;
        public const int TYPE_TaskTienDo_CongViecThuocNhomTrongTuyen =6;
        public const int TYPE_TaskTienDo_NhomThuocTuyen = 5;
        public const int TYPE_TaskTienDo_NhomKhongThuocTuyen = 4;
        public const int TYPE_TaskTienDo_CongViecThuocNhom = 5;
        public const int TYPE_TaskTienDo_KeHoach_New = 7;
        public const int TYPE_TaskTienDo_ThiCong_New = 7;



        public const string TBL_NhomCongTac = "Tbl_TDKH_NhomCongTac";
        public const string TBL_NhomDienGiai = "Tbl_TDKH_NhomDienGiai";
        
        public const string TBL_DanhMucCongTac = "Tbl_TDKH_DanhMucCongTac";
        //public const string TBL_KHVT_VATTU = "Tbl_TDKH_KHVT_VatTu";
        public const string TBL_ChiTietCongTacTheoKy = "Tbl_TDKH_ChiTietCongTacTheoGiaiDoan";
        public const string TBL_ChiTietCongTacTheoKyFileDinhKem = "Tbl_TDKH_CTTK_FileDinhKem";
        public const string TBL_NhomFileDinhKem = "Tbl_TDKH_Nhom_FileDinhKem";
        public const string TBL_ChiTietCongTacCon = "Tbl_TDKH_ChiTietCongTacCon";
        public const string TBL_TenGopCongTac = "Tbl_TDKH_TenGopCongTac";
        public const string TBL_TenGopNhom = "Tbl_TDKH_TenGopNhom";
        public const string TBL_TenGopPhanDoan = "Tbl_TDKH_TenGopPhanDoan";
        public const string TBL_KinhPhiDuKien = "Tbl_TDKH_KinhPhiDuKien";
        public const string Tbl_HaoPhiVatTu = "Tbl_TDKH_HaoPhiVatTu";
        public const string Tbl_HaoPhiVatTu_HangNgay = "Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay";
        public const string Tbl_HaoPhiVatTu_DonGia = "Tbl_TDKH_HaoPhiVatTu_DonGia";
        public const string Tbl_PhanTuyen = "Tbl_TDKH_PhanTuyen";
        public const string Tbl_TenDienDaiTuDo = "Tbl_TDKH_TenDienDaiTuDo";
        
        public const string Tbl_DonGiaThiCongHangNgay = "Tbl_TDKH_DonGiaThiCongHangNgay";


        public const string Tbl_Dependency = "Tbl_TDKH_Dependency";
        public const string Tbl_Ngaynghi = "Tbl_TDKH_NgayNghi";


        //Các bảng cho kế hoạch vật tư
        public const string TBL_KHVT_VatTu = "Tbl_TDKH_KHVT_VatTu";
        public const string TBL_KHVT_DonGia = "Tbl_TDKH_KHVT_DonGia";
        public const string TBL_KHVT_KhoiLuongHangNgay = "Tbl_TDKH_KHVT_KhoiLuongHangNgay";

        public const string TBL_KyThucHien = "Tbl_TDKH_KyThucHien";



        public const string COL_Chon = "A";
        //public const string COL_KhoiLuongHopDongToanDuAn = "Q";

        public const string COL_CustomOrder = "CustomOrder";
        public const string COL_CustomOrderWholeHM = "CustomOrderWholeHM";
        public const string COL_MaHieuCongTac = "MaHieuCongTac";
        public const string COL_MaVatLieu = "MaVatLieu";
        public const string COL_DanhMucCongTac = "TenCongTac";
        public const string COL_VatTu = "VatTu";
        public const string COL_DonVi = "DonVi";
        public const string COL_DBC_SoBoPhanGiongNhau = "SoBoPhanGiongNhau";
        public const string COL_DBC_Dai = "Dai";
        public const string COL_DBC_Rong = "Rong";
        public const string COL_DBC_Cao = "Cao";
        public const string COL_DBC_HeSoCauKien = "HeSoCauKien";
        public const string COL_DBC_KL1BoPhan = "KhoiLuongMotBoPhan";
        public const string COL_DBC_LoaiCT = "LoaiCT";
        public const string COL_DBC_KhoiLuongToanBo = "KhoiLuongToanBo";
        public const string COL_KhoiLuongNhapVao = "KhoiLuongNhapVao";
        public const string COL_KhoiLuongGiaoKhoan = "KhoiLuongGiaoKhoan";
        public const string COL_DBC_TheoHD = "TheoHopDong";
        //public const string COL_DBC_KhoiLuongToanBoIsCongThucMacDinh = "KhoiLuongToanBo_Iscongthucmacdinh";
        public const string COL_DBC_LuyKeDaThucHien = "LuyKe";
        //public const string COL_KhoiLuongHopDongChiTiet = "KhoiLuongHopDongChiTiet"; //Sang nhà thầu là khối lượng hợp đồng nhà thầu, tương tự cho tổ đội

        public const string COL_IsCongTacPhatSinh = "PhatSinh"; //Sang nhà thầu là khối lượng hợp đồng nhà thầu, tương tự cho tổ đội

        public const string COL_KhoiLuongHopDongDuAn = "KhoiLuongHopDongToanDuAn";
        public const string COL_MuiThiCong = "MuiThiCong";
        public const string COL_CodeMuiThiCong = "CodeMuiThiCong";
        public const string COL_KhoiLuongDaThiCong = "KhoiLuongDaThiCong";
        public const string COL_DangDo = "DangDo";
        public const string COL_KhoiLuongKeHoachTheoCongTac = "KhoiLuongKeHoachTheoCongTac";
        public const string COL_KhoiLuongHopDongTheoCongTac = "KhoiLuongHopDongTheoCongTac";

        
        public const string COL_NhapKhoiLuongKeHoachGiaiDoan = "NhapKhoiLuongKeHoachGiaiDoan";
        public const string COL_NhapKhoiLuongBoSungGiaiDoan = "NhapKhoiLuongKeHoachGiaiDoan";

        public const string COL_KhoiLuongBoSungGiaiDoan = "KhoiLuongBoSungGiaiDoan";
        public const string COL_GiaTriBoSungGiaiDoan = "GiaTriBoSungGiaiDoan";
        public const string COL_KhoiLuongConLaiGiaiDoan = "KhoiLuongConLaiGiaiDoan";
        public const string COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong = "KhoiLuongConLaiGiaiDoanSoVoiHopDong";
        public const string COL_GiaTriConLaiGiaiDoan = "GiaTriConLaiGiaiDoan";

        
        public const string COL_KhoiLuongKeHoachGiaiDoan = "KhoiLuongKeHoachGiaiDoan";
        public const string COL_KhoiLuongThiCongGiaiDoan = "KhoiLuongThiCongGiaiDoan";
        public const string COL_KhoiLuongThiCongLuyKeDenKyTruoc = "KhoiLuongThiCongLuyKeDenKyTruoc";
        public const string COL_KhoiLuongThiCongLuyKeDenGiaiDoan = "KhoiLuongThiCongLuyKeDenGiaiDoan";
        public const string COL_GiaTriKeHoachGiaiDoan = "GiaTriKeHoachGiaiDoan";
        public const string COL_GiaTriThiCongGiaiDoan = "GiaTriThiCongGiaiDoan";
        public const string COL_GiaTriThiCongLuyKeDenKyTruoc = "GiaTriThiCongLuyKeDenKyTruoc";
        public const string COL_GiaTriThiCongLuyKeDenGiaiDoan = "GiaTriThiCongLuyKeDenGiaiDoan";
        
        public const string COL_KhoiLuongKeHoachGiaiDoanTheoCongTac = "KhoiLuongKeHoachGiaiDoanTheoCongTac";
        public const string COL_KhoiLuongThiCongGiaiDoanTheoCongTac = "KhoiLuongThiCongGiaiDoanTheoCongTac";
        public const string COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac = "KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac";
        public const string COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac = "KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac";
        public const string COL_GiaTriKeHoachGiaiDoanTheoCongTac = "GiaTriKeHoachGiaiDoanTheoCongTac";
        public const string COL_GiaTriThiCongGiaiDoanTheoCongTac = "GiaTriThiCongGiaiDoanTheoCongTac";
        public const string COL_GiaTriThiCongLuyKeDenKyTruocTheoCongTac = "GiaTriThiCongLuyKeDenKyTruocTheoCongTac";
        public const string COL_GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac = "GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac";
        
        public const string COL_KhoiLuongKeHoachGiaiDoanCongTac = "KhoiLuongKeHoachGiaiDoanCongTac";
        public const string COL_KhoiLuongThiCongGiaiDoanCongTac = "KhoiLuongThiCongGiaiDoanCongTac";
        public const string COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac = "KhoiLuongThiCongLuyKeDenKyTruocCongTac";
        public const string COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac = "KhoiLuongThiCongLuyKeDenGiaiDoanCongTac";
        //public const string COL_GiaTriKeHoachGiaiDoanCongTac = "GiaTriKeHoachGiaiDoanCongTac";
        //public const string COL_GiaTriThiCongGiaiDoanCongTac = "GiaTriThiCongGiaiDoanCongTac";
        //public const string COL_GiaTriThiCongLuyKeDenKyTruocCongTac = "GiaTriThiCongLuyKeDenKyTruocCongTac";
        //public const string COL_GiaTriThiCongLuyKeDenGiaiDoanCongTac = "GiaTriThiCongLuyKeDenGiaiDoanCongTac";


        public const string COL_STTDocVao = "STTDocVao";
        public const string COL_STTND = "STTND";

        public const string COL_PhanTramGiaTriKyNay = "PhanTramGiaTriKyNay";
        public const string COL_PhanTramGiaTriLuyKeKyNay = "PhanTramGiaTriLuyKeKyNay";
        
        public const string COL_KhoiLuongDaThiCongTheoCongTac = "KhoiLuongDaThiCongTheoCongTac";
        public const string COL_KhoiLuongDaNhapKho = "KhoiLuongDaNhapKho";
        public const string COL_GhiChu = "Ghichu";
        public const string COL_FileDinhKem = "FileDinhKem";
        //public const string COL_CongTrinh = "CodeCongTrinh";
        //public const string COL_HangMuc = "CodeHangMuc";
        public const string COL_Code = "Code";
        public const string COL_CodeDMCT = "CodeDMCT";
        public const string COL_CodeGop = "CodeGop";
        public const string COL_IsEdit = "IsEdit";
        public const string COL_Code_Goc = "Code_Goc";
        public const string COL_DonGiaTheoHopDong = "DonGiaTheoHopDong";
        public const string COL_DonGiaPS = "DonGiaPhatSinh";
        public const string COL_CodeDot = "CodeDot";
        public const string COL_TypeRow = "TypeRow";
        public const string COL_MayQuyDoi = "MayQuyDoi";

        //public const string COL_MaHieuCongTac = "MaVatLieu";
        //public const string COL_KHVT_TenVatTu = "VatTu";
        public const string COL_KHVT_DonGia = "DonGia";
        public const string COL_KHVT_DinhMuc = "DinhMuc";
        public const string COL_KHVT_DinhMucNguoiDung = "DinhMucNguoiDung";
        public const string COL_KHVT_HeSo = "HeSo";
        public const string COL_KHVT_HeSoNguoiDung = "HeSoNguoiDung";

        #region Phần kế hoạch

        //Phần đo bóc chuẩn
        public static Color color_DaNhapKhoiLuongNgayThuCong = Color.Blue;

        public const string SheetName_KeHoachKinhPhi = "Tiến độ - Kế hoạch kinh phí";
        //public const string SheetName_KLHangNgay = "Nhập KL hàng ngày";
        public const string SheetName_VatLieu = "Vật liệu";
        public const string SheetName_NhanCong = "Nhân công";
        public const string SheetName_MayThiCong = "Máy thi công";
        public const string SheetName_TongHopHaoPhi = "Tổng hợp hao phí thi công";
        //public const string SheetName_KeHoachVatTu = "Máy thi công";
        
        public const string RANGE_KeHoach = "KeHoach";
        //public const string RANGE_KLHangNgay= "KLThiCongHangNgay";


        public const string RANGE_KinhPhiPhanBoToanDuAn = "KinhPhiPhanBoToanDuAn";

        public const string RANGE_KinhPhiPhanBoVatLieu = "KinhPhiPhanBoVatLieu";
        public const string RANGE_KinhPhiPhanBoNhanCong = "KinhPhiPhanBoNhanCong";
        public const string RANGE_KinhPhiPhanBoMay = "KinhPhiPhanBoMay";


        public const string RANGE_TongKinhPhiVatLieu = "TongKinhPhiVatLieu";
        public const string RANGE_TongKinhPhiNhanCong = "TongKinhPhiNhanCong";
        public const string RANGE_TongKinhPhiMayThiCong = "TongKinhPhiMayThiCong";
        
        
        public const string RANGE_TongKinhPhiTheoTienDoVatTu_VatLieu = "TongKinhPhiTheoTienDoVatTu_VatLieu";
        public const string RANGE_TongKinhPhiTheoTienDoVatTu_NhanCong = "TongKinhPhiTheoTienDoVatTu_NhanCong";
        public const string RANGE_TongKinhPhiTheoTienDoVatTu_MayThiCong = "TongKinhPhiTheoTienDoVatTu_May"; 


        public const string RANGE_TongKinhPhiDuKienVatTu = "TongKinhPhiDuKienVatTu";

        //public const string RANGE_KeHoachKP_Ngay = "KeHoachKinhPhi_Ngay";
        //public const string RANGE_KLHangNgay_Ngay = "KLHangNgay_Ngay";
        //public const string RANGE_VL_Ngay = "Vatlieu_Ngay";
        //public const string RANGE_NC_Ngay = "Nhancong_Ngay";
        //public const string RANGE_MTC_Ngay = "Maythicong_Ngay";

        public const string RANGE_KeHoachVatTu_VL_TuDong = "KeHoachVatTu_VatLieu_TuDong";
        public const string RANGE_KeHoachVatTu_VL_ThuCong = "KeHoachVatTu_VatLieu_ThuCong";
        public const string RANGE_KeHoachVatTu_MTC_TuDong = "KeHoachVatTu_MayThiCong_TuDong";
        public const string RANGE_KeHoachVatTu_MTC_ThuCong = "KeHoachVatTu_MayThiCong_ThuCong";

        public const string RANGE_KeHoachVatTuFull = "TongHopFullVatTu";
        
        
        //public const string RANGE_KeHoachVatTu_TongKinhPhiTienDo = "TongKinhPhiTienDoVatTu";

        //public const string R

        
        public const string RANGE_KeHoachVatTu_NC_TuDong = "KeHoachVatTu_NhanCong_TuDong";
        public const string RANGE_KeHoachVatTu_NC_ThuCong = "KeHoachVatTu_NhanCong_ThuCong";

        //public const string TDKH_FormulaType_MacDinh = "Mặc định";
        //public const string TDKH_FormulaType_NguoiDung = "Người dùng";

        //Phần kế hoạch
        public const string TBL_KhoiLuongCongViecHangNgay = "Tbl_TDKH_KhoiLuongCongViecTungNgay";
        //public const string TBL_KhoiLuongThiCongTheoNgay = "TBL_TDKH_KhoiLuongThiCongHangNgay";
        public const string TBL_KhoiLuongHaoPhiTheoNgay = "Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay";

        //public const string COL_Code = "Code";
        public const string COL_STT = "Stt";
        public const string COL_TenGop = "TenGop";
        //public const string COL_ThuTuCongTac = "ThuTuCongTac";
        public const string COL_PhanTramThucHien = "PhanTramThucHien";

        public const string COL_GiaoKhoanBanDau = "GiaoKhoanBanDau";
        public const string COL_GiaoKhoanBoSung = "GiaoKhoanBoSung";
        public const string COL_GiaoKhoanCatGiam = "GiaoKhoanCatGiam";
        
        public const string COL_DonGia = "DonGia";
        public const string COL_DonGiaThiCong = "DonGiaThiCong";

        public const string COL_KhoiLuong = "KhoiLuong";
        public const string COL_KhoiLuongThiCong = "KhoiLuongThiCong";
        public const string COL_PhanTichVatTu = "PhanTichVatTu";
        public const string COL_HasHopDongAB = "HasHopDongAB";
        public const string COL_IsUseMTC = "IsUseMTC";
        public const string COL_KhoiLuongHopDongChiTiet = "KhoiLuongHopDongChiTiet";
        
        //public const string COL_GiaTri = "GiaTri";
        public const string COL_GiaTriThiCong = "GiaTriThiCong";

        public const string COL_SoNgayThucHien = "SoNgayThucHien";
        public const string COL_SoNgayThiCong = "SoNgayThiCong";
        //public const string COL_SoNgayTongHop = "SoNgayTongHop";
        //public const string COL_TuNgay = "TuNgay";
        //public const string COL_DenNgay = "DenNgay";
        public const string COL_NgayBatDau = "NgayBatDau";
        public const string COL_NgayBatDauGT = "NgayBatDauGT";
        public const string COL_NgayKetThuc = "NgayKetThuc";
        public const string COL_NgayKetThucGT = "NgayKetThucGT";
        public const string COL_NgayBatDauThiCong = "NgayBatDauThiCong";
        public const string COL_NgayKetThucThiCong = "NgayKetThucThiCong";
        public const string COL_NhanCong = "NhanCong";
        public const string COL_CustomNote = "CustomNote";

        public const string COL_LuyKeKyTruoc = "LuyKeKyTruoc";
        public const string COL_LuyKeKyNay = "LuyKeKyNay";
        public const string COL_ConLai = "ConLai";
        public const string COL_GiaTriHD = "GiaTriHD";
        public const string COL_GiaTriLKKT = "GiaTriLKKT";
        public const string COL_GiaTriThucHien = "GiaTriThucHien";
        public const string COL_GiaTriLKKN = "GiaTriLKKN";
        public const string COL_GiaTriConLai = "GiaTriConLai";
        public const string COL_TyLe = "TyLe";
        //public const string COL_ = "LuyKeKyNay";
        //public const string COL_LuyKeKhoiCong = "LuyKeKhoiCong";
        public const string COL_KeHoachKyNay = "KeHoachKyNay";
        public const string COL_KeHoachKySau = "KeHoachKySau";
        public const string COL_ThucHienKyNay = "ThucHienKyNay";
        public const string COL_KhoiLuongConLai = "KhoiLuongConLai";
        public const string COL_KhoiLuongConLaiTong = "KhoiLuongConLaiTong";
        public const string COL_LyDo = "LyDo";
        public const string COL_Dat = "Dat";
        public const string COL_KhongDat = "KhongDat";
        public const string COL_NguoiDaiDien = "NguoiDaiDien";

        public const string COL_NhanCongIsCongThucMacDinh = "NhanCong_Iscongthucmacdinh";
        public const string COL_KinhPhiDuKien = "KinhPhiDuKien";
        public const string COL_GiaTriNhanThau = "GiaTriNhanThau";
        //public const string COL_KinhPhiThiCongThucTe = "KinhPhiThiCongThucTe";
        //public const string COL_KinhPhiDuKienThiCong = "KinhPhiDuKienThiCong";
        public const string COL_KinhPhiDuKienIsCongThucMacDinh = "KinhPhiDuKien_Iscongthucmacdinh";
        public const string COL_KhoiLuongToanBo_Iscongthucmacdinh = "KhoiLuongToanBo_Iscongthucmacdinh";

        public const string COL_GiaTri = "KinhPhiTheoTienDo";
        public const string COL_KinhPhiTheoTienDoThiCong = "KinhPhiTheoTienDoThiCong";
        public const string COL_KinhPhiPhanBoDuKien = "KinhPhiPhanBoDuKien";
        public const string COL_RowCha = "RowCha";
        public const string COL_TrangThai = "TrangThai";
        public const string COL_LyTrinhCaoDo = "LyTrinhCaoDo";

        //public const string COL_ThanhTienThiCong = "ThanhTienThiCong";


        public const string COL_KHVT_KhoiLuongKeHoach = "KhoiLuongKeHoach";
        public const string COL_KHVT_KhoiLuongHopDong = "KhoiLuongHopDong";
        public const string COL_KHVT_Search = "Search";
        public const string COL_KHVT_MaTXHientruong = "MaTXHienTruong";

        public const string COL_GiaTriHopDong = "GiaTriHopDong";
        
        public const string COL_GiaTriConLaiSoVoiHopDong = "GiaTriConLaiSoVoiHopDong";
        public const string COL_GiaTriConLaiSoVoiKeHoach = "GiaTriConLaiSoVoiKeHoach";
        public const string COL_GiaTriHopDongWithCPDP = "GiaTriHopDongWithCPDP";
        public const string COL_GiaTriHopDongWithoutCPDP = "GiaTriHopDongWithoutCPDP";
        public const string COL_LuyKeHetNamTruoc = "LuyKeNamTruoc";
        //public const string COL_LuyKeTuDauNamNay = "LuyKeTuDauNamNay";
        public const string COL_KLThanhToan = "KLThanhToan";
        public const string COL_LuyKeTuDauNam = "LuyKeTuDauNam";
        public const string COL_KeHoachNamNay = "KeHoachNamNay";

        public const string COL_GTSXTrongKy = "GTSXKy";
        public const string COL_GTSXTrongThang = "GTSXThang";
        public const string COL_GTSXTrongNam = "GTSXNam";
        public const string COL_GTSXDaThiCong = "GTSXDaThiCong";
        public const string COL_GTSXFromBeginOfPrj = "GTSXFromBeginOfPrj";
        public const string COL_GTSXTyLe = "GTSXTyLe";
        
        public const string COL_GTNTTrongKy = "GTNTKy";
        public const string COL_GTNTTrongThang = "GTNTThang";
        public const string COL_GTNTTrongNam = "GTNTNam";
        public const string COL_GTNTFromBeginOfPrj = "GTNTFromBeginOfPrj";
        public const string COL_GTNTTyLe = "GTNTTyLe";


        public const string COL_GTDangDo = "GiaTriDangDo";
        //public const string COL_NhapKLHN = "NhapKLHangNgay";


        //public static Dictionary<string, string> dic_Kh = new Dictionary<string, string>()
        //{
        //    {COL_STT, "B" },
        //    {COL_MaHieuCongTac, "C" },
        //    {COL_DanhMucCongTac, "D" },
        //    {COL_DonVi, "E" },
        //    {COL_DBC_KhoiLuongToanBo, "F" },
        //    {COL_KhoiLuongHopDongChiTiet,  "H"},
        //    {COL_Code,  "Z"},

        //};

        /*        public static Dictionary<string, string> dic_Kh = new Dictionary<string, string>()
                {
                    {COL_STT, "B" },
                    {COL_MaHieuCongTac, "C" },
                    {COL_DanhMucCongTac, "D" },
                    {COL_DonVi, "E" },
                    {COL_DBC_KhoiLuongToanBo, "F" },
                    {COL_KhoiLuongKeHoachGiaiDoan, "G" },
                    {COL_KhoiLuongDaThiCong, "H" },
                    {COL_KhoiLuongHopDongChiTiet,  "I"},
                    {COL_PhanTramThucHien,  "J"},
                    {COL_DonGia,  "K"},
                    {COL_DonGiaThiCong,  "L"},
                    {COL_TuNgay,  "M"},
                    {COL_DenNgay,  "N"},
                    {COL_SoNgayTongHop,  "O"},
                    {COL_NgayBatDau,  "P"},
                    {COL_NgayKetThuc,  "Q"},
                    {COL_SoNgayThucHien,  "R"},
                    {COL_NgayBatDauThiCong,  "S"},
                    {COL_NgayKetThucThiCong,  "T"},
                    {COL_SoNgayThiCong,  "U"},
                    {COL_TrangThai,  "V"},
                    {COL_NhanCong,  "W"},
                    {COL_KinhPhiDuKien,  "X"},
                    {COL_GiaTri,  "Z"},
                    {COL_GiaTriNhanThau,  "Y"},
                    {COL_GiaTriThiCong,  "AA"},
                    {COL_LyTrinhCaoDo,  "AB"},
                    {COL_GhiChu,  "AC"},
                    {COL_RowCha,  "AD"},
                    {COL_Code,  "AE"},
                    //{COL_Code,  "Q"},
                    {COL_NhanCongIsCongThucMacDinh,  "AF"},
                    {COL_KinhPhiDuKienIsCongThucMacDinh,  "AG"},
                    {COL_TypeRow,  "AH"},

                };


                //public static Dictionary<string, string> dic_Kh = dic_Kh;



                public static Dictionary<string, string> dic_TongHopKinhPhiTDKH = new Dictionary<string, string>()
                {
                    {COL_STT, "B" },
                    {COL_MaHieuCongTac, "C" },
                    {COL_DanhMucCongTac, "D" },
                    {COL_DonVi, "E" },
                    {COL_DBC_KhoiLuongToanBo, "F" },
                    {COL_KhoiLuongDaThiCong, "G" },
                    {COL_KhoiLuongKeHoachGiaiDoan, "H" },
                    {COL_KhoiLuongHopDongChiTiet,  "I"},
                    {COL_PhanTramThucHien,  "J"},
                    {COL_DonGia,  "K"},
                    {COL_DonGiaThiCong,  "L"},
                    {COL_NgayBatDau,  "M"},
                    {COL_NgayKetThuc,  "N"},
                    {COL_SoNgayThucHien,  "O"},
                    {COL_NgayBatDauThiCong,  "P"},
                    {COL_NgayKetThucThiCong,  "Q"},
                    {COL_SoNgayThiCong,  "R"},
                    {COL_TrangThai,  "S"},
                    {COL_NhanCong,  "T"},
                    {COL_KinhPhiDuKien,  "U"},
                    {COL_GiaTri,  "V"},
                    {COL_GiaTriThiCong,  "W"},
                    {COL_LyTrinhCaoDo,  "X"},
                    {COL_GhiChu,  "Y"},
                    {COL_RowCha,  "Z"},
                    {COL_Code,  "AA"},
                    //{COL_Code,  "Q"},
                    {COL_NhanCongIsCongThucMacDinh,  "AB"},
                    {COL_KinhPhiDuKienIsCongThucMacDinh,  "AC"},
                    {COL_TypeRow,  "AD"},

                };


                public static Dictionary<string, string> dic_KhKPVL_All = new Dictionary<string, string>()
                {
                    {COL_STT,  "A"},
                    {COL_MaHieuCongTac,  "B"},
                    {COL_DanhMucCongTac,  "C"},
                    {COL_DonVi,  "D"},
                    {COL_DonGia,  "E"},
                    {COL_DonGiaThiCong,  "F"},
                    {COL_DBC_KhoiLuongToanBo,  "G"},
                    {COL_KhoiLuongHopDongChiTiet,  "I"},
                    {COL_NgayBatDau,  "J"},
                    {COL_NgayKetThuc,  "K"},
                    {COL_SoNgayThucHien,  "L"},
                    {COL_NgayBatDauThiCong,  "M"},
                    {COL_NgayKetThucThiCong,  "N"},
                    {COL_SoNgayThiCong,  "O"},
                    {COL_KhoiLuongDaThiCong,  "P"},
                    {COL_GiaTriThiCong,  "Q"},
                    {COL_KinhPhiDuKien,  "R"},
                    {COL_GiaTri,  "S"},
                    {COL_KHVT_MaTXHientruong, "T" },
                    {COL_KHVT_Search,  "U" },
                    {COL_Code,  "V"},
                    {COL_TypeRow,  "W"},
                    {COL_RowCha,  "X"},
                    {COL_KHVT_DinhMucNguoiDung,  "Y"},
                    {COL_KHVT_HeSoNguoiDung,  "Z"},

                };


                public static Dictionary<string, string> dic_FullVatTuAll = new Dictionary<string, string>()
                {
                    {COL_STT,  "A"},
                    {COL_MaHieuCongTac,  "B"},
                    {COL_DanhMucCongTac,  "C"},
                    {COL_DonVi,  "D"},
                    {COL_DonGia,  "E"},
                    {COL_DonGiaThiCong,  "F"},
                    {COL_DBC_KhoiLuongToanBo,  "G"},
                    {COL_KhoiLuongHopDongChiTiet,  "H"},
                    {COL_NgayBatDau,  "I"},
                    {COL_NgayKetThuc,  "J"},
                    {COL_SoNgayThucHien,  "K"},
                    {COL_NgayBatDauThiCong,  "L"},
                    {COL_NgayKetThucThiCong,  "M"},
                    {COL_SoNgayThiCong,  "N"},
                    {COL_TuNgay,  "O"},
                    {COL_DenNgay,  "P"},
                    {COL_SoNgayTongHop,  "Q"},
                    {COL_KhoiLuongDaThiCong,  "R"},
                    {COL_KhoiLuongDaThiCongTheoCongTac,  "S"},
                    {COL_KhoiLuongDaNhapKho,  "T"},
                    {COL_GiaTriThiCong,  "U"},
                    {COL_KinhPhiDuKien,  "V"},
                    {COL_GiaTri,  "W"},
                    {COL_KHVT_MaTXHientruong, "X" },
                    {COL_KHVT_Search,  "Y" },
                    {COL_Code,  "Z"},
                    {COL_TypeRow,  "AA"},
                    {COL_RowCha,  "AB"},
                    {COL_KHVT_DinhMucNguoiDung,  "AC"},
                    {COL_KHVT_HeSoNguoiDung,  "AD"},

                };*/

        /*        public static Dictionary<string, string> dic_KhKPVL_ref_DtHaoPhi = new Dictionary<string, string>()
                {

                };


                public static Dictionary<string, string> dic_KhKPVL_ref_DBC = new Dictionary<string, string>()
                { 
                };


                public static Dictionary<string, string> dic_KhKPVL_ref_KeHoach_mul_HaoPhi = new Dictionary<string, string>()
                {

                };


                public static Dictionary<string, string> dic_KhKPVL_ref_All = dic_KhKPVL_ref_DBC
                    .Concat(dic_KhKPVL_ref_KeHoach_mul_HaoPhi).ToDictionary(x => x.Key, x => x.Value);

                public static Dictionary<string, string> dic_KhKPVL_All = dic_KhKPVL
                    .Concat(dic_KhKPVL_ref_All).Concat(dic_KhKPVL_ref_DtHaoPhi).ToDictionary(x => x.Key, x => x.Value);*/

        #endregion

        #region Phần nhập KL hàng ngày

        //public static Dictionary<string, string> dic_KLHangNgay_Ref = new Dictionary<string, string>()
        //{

        //    {COL_MaHieuCongTac, "B" },
        //    {COL_DanhMucCongTac, "C" },
        //    {COL_DonVi, "D" },
        //    {COL_DBC_KhoiLuongToanBo, "E" },
        //    {COL_DBC_HopDong_BoPhan,  "K"},
        //    {COL_Code,  "P"},

        //};

        //public static Dictionary<string, string> dic_KLHangNgay = new Dictionary<string, string>()
        //{
        //    {COL_STT,  "A"},
        //    //{COL_MaHieuCongTac,  "B"},
        //    //{COL_DanhMucCongTac,  "C"},
        //    //{COL_DonVi,  "D"},
        //    //{COL_DBC_KhoiLuongToanBo,  "E"},
        //    {COL_NhapKLHN,  "F"},
        //    {COL_DonGiaThiCong,  "G"},
        //    {COL_NgayBatDauThiCong,  "H"},
        //    {COL_NgayKetThucThiCong,  "I"},
        //    {COL_SoNgayThucHien,  "J"},
        //    {COL_DBC_LuyKeDaThucHien,  "L"},
        //    {COL_TrangThai,  "M"},
        //    {COL_GiaTriThiCong,  "N"},
        //    {COL_RowCha,  "O"},
        //    {COL_TypeRow,  "Q"},
        //};

        public static string[] lsColRowTongKeHoachCapNhat =
        {
            COL_MaHieuCongTac,
            COL_DanhMucCongTac,
            COL_DonVi,
            COL_Code
        };

        //public static Dictionary<string, string> dic_KLHangNgay_All = dic_KLHangNgay.Concat(dic_KLHangNgay_Ref).ToDictionary(x => x.Key, x => x.Value);

        #endregion

        public const int RowIndKLTT_VatTu = 3;// Dòng khối lượng thành tiền
        public static string[] sheetsName =
        {
                SheetName_DoBocChuan,
                SheetName_KeHoachKinhPhi, 
                SheetName_VatLieu,    
                SheetName_NhanCong,    
                SheetName_MayThiCong,    
                SheetName_TongHopHaoPhi,
                
        };

        public static int[] numColsPerDays =
        {
            0,
            2,
            3,
            5,
            5,
            5
        };

        public static string[] rangesNameData =
        {
                RANGE_DoBocChuan,
                RANGE_KeHoach,
                RANGE_KeHoachVatTu_VL_TuDong,
                RANGE_KeHoachVatTu_NC_TuDong,
                RANGE_KeHoachVatTu_MTC_TuDong,
                RANGE_KeHoachVatTuFull
        };
        
        
        public static string[] rangesTongKinhPhiVatTu =
        {
                "",
                "",
                RANGE_TongKinhPhiTheoTienDoVatTu_VatLieu,
                RANGE_TongKinhPhiTheoTienDoVatTu_NhanCong,
                RANGE_TongKinhPhiTheoTienDoVatTu_MayThiCong
        };
        
        public static string[] rangesKinhPhiDuKienVatTu =
        {
                "",
                "",
                RANGE_KinhPhiPhanBoVatLieu,
                RANGE_KinhPhiPhanBoNhanCong,
                RANGE_KinhPhiPhanBoMay,
        };
        
        public static string[] rangesNgayName =
        {
            "",
            "",
            "VatLieu_Ngay",
            "NhanCong_Ngay",
            "MayThiCong_Ngay",
        };
        
        public static string[] wss_Name =
        {
            SheetName_DoBocChuan,
            SheetName_KeHoachKinhPhi,
        };

        public static string[] typeColsNgayBatDau =
        {
            COL_NgayBatDau,
            COL_NgayBatDauThiCong
        };
        
        public static string[] typeColsNgayKetThuc =
        {
            COL_NgayKetThuc,
            COL_NgayKetThucThiCong
        };
            
                
        public static string[] ColsNgayBatDau =
        {
            "",
            COL_NgayBatDau,
            COL_NgayBatDauThiCong,
            "",
            "",
            ""
        };
        
        public static string[] ColsNgayKetThuc =
        {
            "",
            COL_NgayKetThuc,
            COL_NgayKetThucThiCong,
            "",
            "",
            ""
        };

        public static string[] arrColSum =
        {
            COL_GiaTri,
            COL_GiaTriThiCong,
            COL_KinhPhiDuKien,
            COL_GiaTriNhanThau,
            COL_KinhPhiTheoTienDoThiCong,
            //COL_GiaTriThiCong,
            COL_DBC_KhoiLuongToanBo,
            COL_KhoiLuongHopDongChiTiet,
            //COL_KhoiLuongDaThiCong,
            COL_KhoiLuongDaThiCongTheoCongTac,
            COL_DonGia,
            COL_DonGiaThiCong
            
            //COL_KhoiLuongDaNhapKho
        };
        public static string[] KeyTruongSon =
        {
            "-1677660441",
            "0C5YX143VJPJPZ",
            "-2146811743",

        };
        public static XtraTabPage[] TabForceHide =
        {
            SharedControls.xtraTabPage_CongVanDiDen,
            SharedControls.xtraTabPage_DSDuAn_CT,
            SharedControls.xtraTabPage_NhanCong,
            SharedControls.xtraTabPage_QLMay_TB,
            SharedControls.xtraTabPage_GiaoViec,
            SharedControls.xtraTabPage_ThuChi_TamUng,
            SharedControls.xtraTab_QLVatLieu_VanChuyen,
        };
        public static XtraTabPage[] SubTabForceHide =
        {
            SharedControls.xtraTab_VLMTCNC,
            SharedControls.xtraTab_ThamDinhDauVao
        };
        #region Phần tiến đọ
        //public const int TYPE_TIENDO_DUAN = 0;
        //public const int TYPE_TIENDO_CONGTRINH = 1;
        //public const int TYPE_TIENDO_HANGMUC = 2;
        //public const int TYPE_TIENDO_CONGTAC = 3;
        #endregion
        #region Phần MuiThiCong
        public const string Tbl_TDKH_MuiThiCong = "Tbl_TDKH_MuiThiCong";

        #endregion
    }
}
