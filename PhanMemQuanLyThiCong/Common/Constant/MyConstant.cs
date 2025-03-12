using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public static class MyConstant
    {
        //public const float width_A4 = 8.27f;//Inchs
        //public const float height_A4 = 11.69f;//Inchs

        //public const float width_pdf_A4 = 8.5f;//Inchs
        //public const float height_pdf_A4 = 11f;//Inchs

        //public const string TempKhoiLuongCongTac = "TempKhoiLuongCongTac";
        //public const string TempKhoiLuongNhom = "TempKhoiLuongNhom";
        //public const string TempKhoiLuongVatTu = "TempKhoiLuongVatTu";

        public const string DefautWaitFormErrorText = "Vui lòng chờ....";
        public const int MaxFileSize = 157286400; //150MB
        public const int MaxRequestSize = 209715200; //200MB
        public const int MaxSoNgayThucHien = 3650;
        public static string[] ColWithTime =
        {
            "LastChange",
            "CreatedOn",
            "LastSync"
        };

        public const string EXCELFORMATNUM = "#,##0.00";

        public static string[] TypeRows =
        {
            TYPEROW_CongTrinh,
            TYPEROW_HangMuc,
            TYPEROW_PhanTuyen,
            TYPEROW_HTPhanTuyen,
            TYPEROW_Nhom,
            TYPEROW_CVCha,
            TYPEROW_NhomDienGiai,
            TYPEROW_CVCON,

        };

        public static string[] InternalEmail =
        {
            "BINHBUIBK@GMAIL.COM",
            "DUONGTHANGF1@GMAIL.COM",
            "THUYCONGBINH@GMAIL.COM",
            "THUYCONGBINH2@GMAIL.COM",
        };

        public const string PrefixFormula = "TBTFormula";
        public const string PrefixDate = "TBTDate";
        public const string PrefixMerge = "TBTMerge";
        public const string DefaultProvince = "HoChiMinh";

        public const string stringRegexTVCD = "ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\\s";

        public static Dictionary<string, DayOfWeek> dicDayOfWeek = new Dictionary<string, DayOfWeek>()
        {
            {"Thứ 2", DayOfWeek.Monday },
            {"Thứ 3", DayOfWeek.Tuesday },
            {"Thứ 4", DayOfWeek.Wednesday },
            {"Thứ 5", DayOfWeek.Thursday },
            {"Thứ 6", DayOfWeek.Friday },
            {"Thứ 7", DayOfWeek.Saturday },
            {"Chủ nhật", DayOfWeek.Sunday },
        };
        public static Dictionary<string, string> VLNCMTC = new Dictionary<string, string>()
        {
            {"VL", "Vật liệu" },
            {"NC", "Nhân công" },
            {"MTC", "Máy thi công" },
        };
        public static SearchOptions MySearchOptions = new SearchOptions()
        {
            SearchIn = SearchIn.Values,
            MatchEntireCellContents = true,
        };

        public static Dictionary<string, string> dicCompare = new Dictionary<string, string>()
        {
            {"fi", "đường kính ống|đường kính|fi|đk|đ|d|thép|ø"},
            {"cao", "chiều cao|độ cao|cao"},
            {"rong", "chiều rộng|độ rộng|rộng"},
            {"k", "k|độ chặt" },
            {"damnen", "đầm nền" },
            {"sau", "chiều sâu|độ sâu|sâu" },
            {"day", "chiều dày|độ dày|dày" },
        };

        //options.SearchIn = SearchIn.Values;
        //                    options.MatchEntireCellContents = true;
        #region Liên quan Web site
        public const int CONST_TYPE_LAYDAUVIEC_KeHoachTDsangGiaoViec = 0;
        public const int CONST_TYPE_LAYDAUVIEC_DoBocChuan = 1;
        public const int CONST_TYPE_LAYDAUVIEC_OnlyName = 2;
        public const int CONST_TYPE_LAYDAUVIEC_FromGiaoViecChung = 3;
        public static CultureInfo culture = (CultureInfo)CultureInfo.GetCultureInfo("vi-VN").Clone();

        public const string separator_checkedEdit = ";";
        public const string SERVER_TYPE_MODEL_Users = "Users";



        public const string CONST_TYPE_CONGTRINH = "CTR";
        public const string CONST_TYPE_HANGMUC = "HM";
        public const string CONST_TYPE_PhanTuyen = "*T";
        public const string CONST_TYPE_MuiThiCong = "*MTC";
        public const string CONST_TYPE_HoanThanhPhanTuyen = "HT*T";
        public const string CONST_TYPE_NHOM = "*";
        public const string CONST_TYPE_CTGOP = "*T3";
        public const string CONST_TYPE_NHOMGOP = "*T2";
        public const string CONST_TYPE_TUYENGOP = "*T1";
        public const string CONST_TYPE_NHOMDIENGIAI = "+";

        public const string CONSTSTR_KHGV_LoaiCT_LayTuTienDo = "Lấy từ tiến độ";

        public static Color color_Error = Color.Red;
        public static Color color_NguoiDung = Color.Blue;
        public static Color color_Nomal = Color.Black;
        public static Color color_PhatSinh = Color.Green;
        public static Color color_DinhMucTamTinh = Color.OrangeRed;

        public const string TYPEROW_NoType = ""; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_CongTrinh = "CTR"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_HangMuc = "HM"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới

        public const string TYPEROW_MuiThiCong = "MuiThiCong"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_PhanTuyen = "PhanTuyen"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_HTPhanTuyen = "HTPhanTuyen"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_Nhom = "Nhom"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_NhomDienGiai = "NhomCon"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_CVCha = "CVCha"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_CVCON = "CVCon"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_CVCHIA = "CVCHIA"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        //public const string TYPEROW_THEMMOI = "Add"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        public const string TYPEROW_CVTong = "Tong"; //Hàng tổng chứa con là kế hoạch và phát sinh
        public const string TYPEROW_SUMTT = "SUMTT"; //Hàng tổng BÁO CÁO TRƯỜNG SƠN
        public const string TYPEROW_GOP = "GOP"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới
        //public static string[] TypeRows =
        //{
        //    TYPEROW_CongTrinh,
        //    TYPEROW_HangMuc,
        //    TYPEROW_Nhom,
        //    TYPEROW_CVCha,
        //    TYPEROW_NhomDienGiai,
        //    TYPEROW_CVCON,
        //    TYPEROW_NoType
        //};

        public const string HAUTO_Tien = "_Tien";


        //public static string FORMAT_TIEN = $"#,##0.00[${culture.NumberFormat.CurrencySymbol}-{culture.LCID.ToString("x")}]";//, culture.NumberFormat.CurrencySymbol, culture.LCID.ToString("x");
        public static string FORMAT_KhoiLuong = $"#.##0,00";//, culture.NumberFormat.CurrencySymbol, culture.LCID.ToString("x");
        public static string FORMAT_TIEN = $"_-* #,##0 ₫_-;-* #,##0 ₫_-;_-* \" - \"?? {culture.NumberFormat.CurrencySymbol}_-;_-@_-";//, culture.NumberFormat.CurrencySymbol, culture.LCID.ToString("x");
                                                                                                                                     //public const string TYPEROW_ = "Add"; //Hàng chưa CV Con, CV Cha hay hàng nhập mới


        //public const string BASE_ADRESS = "http://localhost:5001";
        //public const string BASE_ADRESS = "http://45.118.146.38:5005";
        //public const string CONST_WEBSITE = BASE_ADRESS + "/api/";


        //public const string BASE_ADRESSChatTest = "http://localhost:5001/";
        //public const string CONST_WEBSITEChatTest = BASE_ADRESSChatTest + "/api/";
        //public const string CONST_WEBSITE = "https://binh2022.quanlythicong.com/api/";
        //public const string CONST_WEBSITE = "https://binh2022.quanlythicong.com/api/";

        //public const string CONST_SERVER_IP = "103.28.36.96";

        public const int CONST_TYPE_FORMTHONGTINCANHAN_DANGKY = 0;
        public const int CONST_TYPE_FORMTHONGTINCANHAN_THONGTIN = 1;
        public const string TBL_FROMSERVER_USER = "dbo.User";
        public const string TBL_FROMSERVER_GROUP = "dbo.Group";
        //public const string TBL_USER = "dbo.User";


        #endregion




        public const string nullSuf = "_nullable"; //Hậu tố cho các cột có thể null

        public static string[] Trangthai =
        {
            "Đang thực hiện",
            "Đang duyệt",
            "Đã duyệt",
            "Đang thi công",
            "Đang quyết toán",
            "Đang kiểm toán",
            "Đang bàn giao",
            "Đã bàn giao hoàn thành"
        };
        public static Color colorDaCodb = Color.White;
        public static Color colorChuaCodb = Color.GreenYellow;
        public static Color colorNhanhCham = Color.Purple;

        public static Color colorKhongNhapKLHN = Color.White;
        public static Color colorNhapKLHangNgay = Color.LightYellow;


        public static Color color_Row_CongTrinh = Color.DarkTurquoise;
        public static Color color_Row_MuiTC = Color.Blue;
        public static Color color_Row_GOP = Color.Purple;
        public static Color color_Row_HangMuc = Color.DarkGreen;
        public static Color color_Row_NhaThau = Color.DarkOrange;
        public static Color color_Row_PhanTuyen = Color.OrangeRed;
        public static Color color_Row_NhomCongTac = Color.DarkGoldenrod;
        public static Color color_Row_NhomDienGiai = Color.DarkViolet;
        public static Color color_Row_DienGiai = Color.Blue;

        public static Color color_Cell_DonGiaCaiDatRieng = Color.LightGreen;

        public static Color color_Disable = Color.Red;

        public static List<Color> color_COLNHATHAU_TODOI = new List<Color>
        {
            Color.DarkTurquoise,
            Color.DarkGreen,
            Color.DarkGoldenrod,
            Color.DarkViolet,
            Color.DarkBlue,
            Color.DarkGray
        };
        public const int CONST_TYPE_DinhMuc_KHGV = 0;
        public const int CONST_TYPE_DinhMuc_DoBocChuan = 1;
        public const int CONST_TYPE_DinhMuc_TDKH_KeHoach = 2;
        public const int CONST_TYPE_DinhMuc_TDKH_KH_VL = 3;
        public const int CONST_TYPE_DinhMuc_TDKH_KH_VL_MTC = 4;
        public const int CONST_TYPE_DANHMUCCONGTAC_DUTHAU = 5;
        public const int CONST_TYPE_DANHMUCCONGTAC_VL = 6;
        public const int CONST_TYPE_DANHMUCCONGTAC_TONGMUCDAUTU = 7;
        public const int CONST_TYPE_DANHMUCCONGTAC_CONGTRINH = 8;
        public const int CONST_TYPE_DANHMUCCONGTAC_VATLIEU = 9;
        #region Cơ sở dữ liệu
        public const string CONST_DbFromPathDA = "db_QuanLyThiCong.sqlite3";
        #endregion
        public const string CONST_DATE_FORMAT_SQLITE = "yyyy-MM-dd";
        public const string CONST_DATE_FORMAT_SQLITE_WithTime = "yyyy-MM-dd HH:mm:ss.fff";
        public const string CONST_DATE_FORMAT_FileSUffix = "dd-MM-yyyy_hh-mm-ss";
        public const string CONST_DATE_FORMAT_SPSHEET = "dd/MM/yyyy";

        public const string CONST_SheetName_TDKH_DoBocChuan = "Đo bóc chuẩn";
        public const string CONST_SheetName_HOPDONGDB = "Đo bóc";
        public const string CONST_SheetName_TTNghiemThu = "Khối lượng nghiệm thu";
        public const string CONST_SheetName_TTKLHT = "Thanh toán KL hoàn thành";
        public const string CONST_SheetName_TTKLPS = "Thanh toán KL phát sinh";
        public const string CONST_SheetName_TTKLTT = "Thanh toán thông thường";
        public const string CONST_SheetName_THTT = "Tổng hợp các kỳ thanh toán";
        public const string CONST_SheetName_TDKH_KeHoach_KinhPhi = "Tiến độ - Kế hoạch kinh phí";
        public const string CONST_SheetName_TDKH_KeHoach_KeHoachVatTu = "Kế hoạch vật tư";

        public const string CONST_SheetName_KHGV_KeHoach = "Kế hoạch";
        public const string view_HaoPhiVatTu = "view_HaoPhiVatTu";
        public const string view_HaoPhiWithVatTu = "view_HaoPhiWithVatTu";
        public const string view_GiaoViecCha = "view_GiaoViecCha";

        public const string view_HaoPhiKeHoachThiCong = "view_HaoPhiKeHoachThiCong";
        public const string view_VatTuKeHoachThiCong = "view_VatTuKeHoachThiCong";
        public const string view_CongTacKeHoachThiCong = "view_CongTacKeHoachThiCong";
        public const string view_CongTacWithHangMucCongTrinh = "view_CongTacWithHangMucCongTrinh";
        public const string view_HangMucWithPhanTuyen = "view_HangMucWithPhanTuyen";


        public const string view_ViewVatTuHaoPhiNhapKhoHangNgay = "view_VatTuHaoPhiNhapKhoHangNgay";
        public const string view_KhoiLuongKeHoachAndThiCongCongTacTheoKy = "view_KhoiLuongKeHoachAndThiCongCongTacTheoKy";
        public const string view_KhoiLuongKeHoachAndThiCongVatTu = "view_KhoiLuongKeHoachAndThiCongVatTu";

        public const string view_HaoPhiVatTuGiaoViecCon = "view_HaoPhiVatTuCongTacCon";

        public const string view_KhoiLuongXuatKhoACap = "view_KhoiLuongXuatKhoACap";


        /// <summary>
        /// Phần bảng thông tin
        /// </summary>
        //public const string TBL_THONGTINTHDA = "Tbl_ThongTinTHDA"; //Thông tin tổng hợp dự án
        public const string TBL_DeletedRecored = "Tbl_DeletedRecored"; //Thông tin tổng hợp dự án

        public const string TBL_THONGTINDUAN = "Tbl_ThongTinDuAn";
        public const string TBL_THONGTINDUAN_FileDinhKem = "Tbl_ThongTinDuAn_FileDinhKem";
        public const string TBL_THONGTINDUAN_ThuyetMinh = "Tbl_ThongTinDuAn_ThuyetMinh";
        public const string TBL_THONGTINCONGTRINH = "Tbl_ThongTinCongTrinh";
        public const string TBL_THONGTINDAIDIENCHUDAUTU = "Tbl_ThongTinDaiDienChuDauTu";
        public const string TBL_THONGTINNHACUNGCAP = "Tbl_ThongTinNhaCungCap";
        public const string TBL_THONGTINNHATHAU = "Tbl_ThongTinNhaThau";
        public const string TBL_THONGTINNHATHAUPHU = "Tbl_ThongTinNhaThauPhu";
        public const string TBL_THONGTINTODOITHICONG = "Tbl_ThongTinToDoiThiCong";
        public const string TBL_THONGTINNGANSACH = "Tbl_ThongTinNganSach";
        public const string TBL_THONGTIN_THANHPHANTHAMGIA = "Tbl_ThongTin_ThanhPhanThamGia";
        public const string TBL_THONGTIN_NHOMTHAMGIA = "Tbl_ThongTin_NhomThamGia";
        public const string TBL_THONGTINHANGMUC = "Tbl_ThongTinHangMuc";
        public const string view_DonViThucHien = "view_DonViThucHien";
        public const string view_DonViThucHienThuCong = "view_DonViThuHienCongTacThuCong";
        //public const string Tbl_Temp_TDKH_KhoiLuongCongViecHangNgay = "Tbl_Temp_TDKH_KhoiLuongCongViecHangNgay";

        /// <summary>
        /// Phần bảng Máy thi công
        /// </summary>
        public const string TBL_MTC_CHITIETDINHMUCTHAMKHAO = "Tbl_MTC_DinhMucThamKhao";
        public const string TBL_MTC_CHITIETDINHMUC = "Tbl_MTC_ChiTietDinhMuc";
        public const string TBL_MTC_CHUSOHUUINMAY = "Tbl_MTC_ChuSoHuuInMay";
        public const string TBL_MTC_DANHSACHCHUSOHUU = "Tbl_MTC_DanhSachChuSoHuu";
        public const string TBL_MTC_DANHSACHMAY = "Tbl_MTC_DanhSachMay";
        public const string TBL_MTC_DUANINMAY = "Tbl_MTC_DuAnInMay";
        public const string TBL_MTC_LOAINHIENLIEU = "Tbl_MTC_LoaiNhienLieu";
        public const string TBL_MTC_NGUOIVANHANHINMAY = "Tbl_MTC_NguoiVanHanhInMay";
        public const string TBL_MTC_NHIENLIEU = "Tbl_MTC_NhienLieu";
        public const string TBL_MTC_NHIENLIEUINMAY = "Tbl_MTC_NhienLieuInMay";
        public const string TBL_MTC_CHITIETNHATTRINH = "Tbl_MTC_ChiTietNhatTrinh";
        public const string TBL_MTC_NHATTRINHCONGTAC = "Tbl_MTC_NhatTrinhCongTac";
        public const string VIEW_DANHSACHCHUSOHUU = "view_DanhSachChuSoHuu";
        public const string VIEW_DANHSACHMAYDUAN = "view_DanhSachMayDuAn";
        public const string TBL_MTC_TRANGTHAI = "Tbl_MTC_TrangThai";
        public const string TBL_MTC_FILEDINHKEM = "Tbl_MTC_FileDinhKem";
        public const string TBL_MTC_LOAIMAY = "Tbl_MTC_LoaiMay";
        public const string TBL_MTC_NHIENLIEUHANGNGAY = "Tbl_MTC_NhienLieuHangNgay";
        public const string TBL_MTC_NHATTRINHFILEDINHKEM = "Tbl_MTC_ChiTietNhatTrinh_FileDinhKem";

        public const string TBL_Provinces = "Provinces";
        public const string TBL_Department = "Department";
        public const string TBL_DinhMuc = "CategoryDinhMuc";

        public const string KeyWin = "systemkey";

        public const string LoaiDVTH_NhaThau = "Đơn vị giao thầu";
        public const string LoaiDVTH_TuThucHien = "Tự thực hiện";
        public const string LoaiDVTH_NhaThauPhu = "Nhà thầu";
        public const string LoaiDVTH_ToDoiThiCong = "Tổ đội thi công";
        public const string LoaiDVTH_NhaCungCap = "Nhà Cung Cấp";

        public static string[] lsTblDVTH =
        {
            TBL_THONGTINNHATHAU,
            TBL_THONGTINNHATHAUPHU,
            TBL_THONGTINTODOITHICONG,
            TBL_THONGTINNHACUNGCAP,
            TBL_THONGTINNHATHAUPHU,
            TBL_THONGTINDUAN,
        };
        public const string FS = "FS";
        public const string SS = "SS";
        public const string FF = "FF";
        public const string SF = "SF";
        public static string[] lstTaoLienKet =
        {
            FS,
            SS,
            FF,
            SF,
        };

        public static string[] lsColFkDVTH =
        {
            "CodeNhaThau",
            "CodeNhaThauPhu",
            "CodeToDoi",
            "TenNhaCungCap",
            "CodeNhaThauPhu",
            "DuAn",
        };
        public const string ConditionAllDVTH = "CodeNhaThau IS NULL AND CodeNhaThauPhu IS NULL AND CodeToDoi IS NULL";

        /// <summary>
        /// bảng trong cơ sở dữ liệu chung
        /// </summary>
        public const string TBL_TBT_DINHMUCALL = "Tbl_DinhMucAll";
        public const string TBL_TBT_HaoPhiNguoiDung = "Tbl_HaoPhiNguoiDung";
        public const string TBL_HaoPhiVatTu = "Tbl_HaoPhiVatTu";
        //public const string view_HaoPhiVatTu = "view_HaoPhiVatTu";
        public const string TBL_TBT_VATTU = "Tbl_VatTu";
        public const string TBL_TBT_GiaVatLieu = "Tbl_GiaVatLieu";

        //public static string[] ls_tblVatLieu =
        //{
        //    "Tbl_DinhMucCongIch2020",
        //    "Tbl_DinhMucDienLuc2019",
        //    "Tbl_DinhMucDienLuc2020",
        //    "Tbl_DinhMucDienLuc2020",
        //    "Tbl_DinhMucLapDat",
        //    "Tbl_DinhMucSuaChua",
        //    "Tbl_DinhMucVienThong2009",
        //    "Tbl_DinhMucVienThong2020",
        //    "Tbl_DinhMucXayDung"
        //};

        public static string[] COL_YEUCAUVT =
        {
            "Code",
            "TenVatTu",
            "DonVi",
            "YeuCauDotNay",
            "HopDongKl",
            "LuyKeYeuCau",
            "LuyKeXuatKho",
            "DonGiaHienTruong",
            "NguoiYeuCau",
            "NguoiNhan",
            "NgayGuiYeuCau",
            "Code",
            "Code",
        };

        public const string TBL_CongVanDiDen_FileDinhKem = "Tbl_CongVanDiDen_FileDinhKem";// Công văn đi đến file đính kèm
        public const string TBL_QuanLyCongVanDiDen = "Tbl_QuanLyCongVanDiDen";// Quản lý công văn đi đến
        public const string TBL_ThuChiTamUngDeXuat = "Tbl_ThuChiTamUngDeXuat";//Thu chi tạm ứng đề xuất
        public const string TBL_TaoHopDongMoi = "Tbl_TaoHopDongMoi";//Tạo hợp đồng mới
        public const string TBL_ThuChiTamUng_KhoanChi = "Tbl_ThuChiTamUng_KhoanChi";//Thu chi tạm ứng khoản chi
        public const string TBL_ThuChiTamUng_KhoanChi_ChiPhi = "Tbl_ThuChiTamUng_KhoanChi_ChiPhi";//Thu chi tạm ứng khoản chi chi phí
        public const string TBL_ThuChiTamUng_KhoanChi_GiaiChi = "Tbl_ThuChiTamUng_KhoanChi_GiaiChi";//Thu chi tạm ứng khoản chi giải chi
        public const string TBL_tonghopdanhsachhopdong = "Tbl_HopDong_TongHop";//Tổng hợp danh sách hợp đồng
        public const string TBL_ThuChiTamUng_Khoanthu = "Tbl_ThuChiTamUng_Khoanthu";//Thu chi tạm ứng khoản thu
        public const string TBL_ThuChiTamUng_Filedinhkem = "Tbl_ThuChiTamUng_Filedinhkem";//thu chi tạm ưng file đính kèm
        public const string TBL_ChiaThiCong = "TBL_ChiaThiCong";//Giao việc chia thi công
        public const string TBL_ChamCong_BangNhanVien = "Tbl_ChamCong_BangNhanVien";//chấm công bảng nhân viên
        public const string TBL_ChamCong_BangThanhToanLuong = "Tbl_ChamCong_BangThanhToanLuong";//chấm công bảng thanh toán lương
        public const string TBL_ChamCong_LichCT = "Tbl_ChamCong_LichCT";//chấm công lịch công tác
        public const string TBL_ChamCong_TamUng = "Tbl_ChamCong_TamUng";//chấm công  tạm ứng
        public const string TBL_ChamCong_NVQL = "Tbl_ChamCong_NVQL";//chấm công nhân viên quản lý
        public const string TBL_DanhmucCongTacVatlieu = "Tbl_DanhmucCongTacVatlieu";//danh mục công tác vật liệu 
        public const string TBL_DanhmucCongTacVatlieu_Diendai = "Tbl_DanhmucCongTacVatlieu_Diendai";//danh mục công tác vật liệu diễn dải
        public const string TBL_DanhmucCongTacduthau = "Tbl_DanhmuCongTacduthau";//danh mục công tác dự thầu
        public const string TBL_DanhmucCongTacTongmucdautu = "Tbl_DanhmuCongTacTongmucdautu";//danh mục công tác tổng mục đầu tư
        public const string TBL_Danhsachphuongtien = "Tbl_Danhsachphuongtien";//danh mục phương tiện
        public const string TBL_Dexuatphuongtien = "Tbl_Dexuatphuongtien";//Đề xuất phương tiện
        public const string TBL_Dobocchuan_AB = "Tbl_Dobocchuan_AB";//Độ bóc chuẩn AB
        public const string TBL_FileHopdongdinhkem = "Tbl_HopDong_FileDinhKem";//file hợp đồng đính kèm                                                     
        public const string TBL_hopdongNCC_TT_NTC = "Tbl_hopdongNCC_TT_NTC";// hợp đồng AB_HT
        public const string TBL_HopDongCaiDatDot = "Tbl_HopDong_CaiDatDot";
        public const string TBL_hopdongNCC_TT = "Tbl_hopdongNCC_TT";// hợp đồng AB_HT
        public const string TBL_hopdongAB_HT = "Tbl_hopdongAB_HT";// hợp đồng AB_HT
        public const string TBL_hopdongAB_PS = "Tbl_hopdongAB_PS";// hợp đồng AB_PS
        public const string TBL_hopdongAB_TT = "Tbl_hopdongAB_TT";// hợp đồng AB_TT
        public const string TBL_LoaiHD = "Tbl_HopDong_LoaiHD";// loại hợp đồng
        public const string TBL_hopdongAB_TToan = "Tbl_hopdongAB_TToan";// loại hợp đồng AB_TToan
        public const string TBL_hopdongcungcap = "Tbl_hopdongcungcap";// loại hợp đồng cung cấp
        public const string TBL_hopdongkhac = "Tbl_hopdongkhac";// loại hợp đồng khác
        public const string TBL_hopdongthauphuBB = "Tbl_hopdongthauphuBB";// loại hợp đồng thầu phụ
        public const string TBL_hopdongthinghiem = "Tbl_hopdongthinghiem";// Hợp đồng thí nghiệm
        public const string TBL_ThongtinphulucHD = "Tbl_HopDong_ThongtinphulucHD";// thông tin phụ lục hướng dẫn
        public const string TBL_HopDong_DoBoc = "Tbl_HopDong_DoBoc";// thông tin phụ lục hướng dẫn
        public const string TBL_HopDong_DeNghiThanhToan = "Tbl_HopDong_DeNghiThanhToan";// thông tin phụ lục hướng dẫn
        public const string TBL_HopDong_PhuLuc = "Tbl_HopDong_PhuLucHD";// thông tin hợp đồng chính AB
        public const string TBL_hopdongtodoi = "Tbl_hopdongtodoi";// hợp đồng  tổ đội
        public const string TBL_KeHoachVatTu = "Tbl_KeHoachVatTu";// Kế hoạch vật tư
        public const string TBL_Nhapnhienlieu = "Tbl_Nhapnhienlieu";
        public const string TBL_TaoHopDongMoi_Hopdongmecon = "Tbl_TaoHopDongMoi_Hopdongmecon";
        public const string TBL_NhapTrinhThiCong = "Tbl_NhapTrinhThiCong";
        public const string TBL_Nhapvattu = "Tbl_Nhapvattu";
        public const string TBL_NhatKyQLVanChuyen = "Tbl_NhatKyQLVanChuyen";
        public const string TBL_QLVanChuyen = "Tbl_QLVanChuyen";
        public const string TBL_suachuaBaoduong = "Tbl_SuachuaBaoduong";

        //public const string TBL_ThongTinTHDA = "Tbl_ThongTinTHDA";
        public const string TBL_ThanhtoanTMDT = "Tbl_ThanhtoanTMDT";
        //public const string TBL_ThongTin_NhomThamGia = "Tbl_ThongTin_NhomThamGia";
        //public const string TBL_ThongTin_ThanhPhanThamGia = "Tbl_ThongTin_ThanhPhanThamGia";
        public const string TBL_Tonghopluykenhienlieu = "Tbl_Tonghopluykenhienlieu";
        public const string TBL_tonghopthanhtoan = "Tbl_tonghopthanhtoan";
        public const string TBL_XuatVatTu = "Tbl_XuatVatTu";
        public const string NhienLieuChinh = "Nhiên liệu chính";
        public const string NhienLieuPhu = "Nhiên liệu phụ";

        // public const string TBL_TDKH_KyThucHien = "Tbl_TDKH_KyThucHien";
        /// <summary>
        /// Phần bảng quản lý file
        /// </summary>
        /// 

        public const string TBL_QLFILE_CONGVANDIDEN = "Tbl_QuanLyCongVanDiDen";
        public const string TBL_QLFILE_CongViecHangNgay = "Tbl_GV_CVHN_FileDinhKem";

        public static string[] TBL_ALL = new string[]
        {
            TBL_THONGTINDUAN,
            TBL_THONGTINCONGTRINH,
            TBL_QLFILE_CONGVANDIDEN,
            TBL_THONGTINCONGTRINH,
            TBL_THONGTINNHACUNGCAP,
            TBL_THONGTINNHATHAU,
            TBL_THONGTINNHATHAUPHU,
            TBL_THONGTINTODOITHICONG
        };

        public static string[] RANGES_THONGTINDUAN = new string[]
        {
            "Code",
            "TenDuAn",
            "MaDuAn",
            "NgayBatDau",
            "NgayKetThuc",
            "TenChuDauTu",
            "DiaChi",
            "MaHieu",
            "DaiDien",
            "DienThoai",
            "MaSoThue",
            "TongVon"
        };

        /// <summary>
        /// Các mục thuộc dự án
        /// </summary>
        public static string[] RANGES_MUCTHUOCDUAN = new string[]
        {
            TBL_THONGTINCONGTRINH,
            TBL_THONGTINNHATHAU,
            TBL_THONGTINNHATHAUPHU,
            TBL_THONGTINNHACUNGCAP,
            TBL_THONGTINTODOITHICONG
        };

        public static string[] RANGES_DonViThucHienNCC = new string[]
        {
            TBL_THONGTINNHATHAU,
            TBL_THONGTINNHATHAUPHU,
            TBL_THONGTINNHACUNGCAP,
            TBL_THONGTINTODOITHICONG
        };
        public static Dictionary<string, string> DIC_TENLOAIHD = new Dictionary<string, string>()
        {
            { TBL_THONGTINNHATHAU, "(HĐ -A-B)" },
            { TBL_THONGTINNHATHAUPHU, "(HĐ -B-B')" },
            { TBL_THONGTINNHACUNGCAP, "(HĐ -B-B')" },
            { TBL_THONGTINTODOITHICONG, "(HĐ - B-B')" }
        };
        public const string COL_TEN = "E";
        public const string COL_DIACHI = "I";
        public const string COL_DAIDIEN = "L";
        public const string COL_DIENTHOAI = "N";
        public const string COL_MASOTHUE = "P";
        public const string COL_CHITIET = "Q";

        public const string COL_CODE = "C";
        public const string COL_CODEHM = "B";
        public const string COL_CODECT = "T";
        public const string COL_MADANHNGHIEP = "R";
        public const string COL_DONVITRUCTHUOC = "S";

        public const string COL_THONGTINVON_STT = "C";
        public const string COL_THONGTIN_NGUONVON = "D";
        public const string COL_THONGTIN_SOTIEN = "E";

        public static Dictionary<string, string> DIC_TenCotUngVoiDb = new Dictionary<string, string>()
        {
            { COL_TEN, "Ten" },
            { COL_DIACHI, "DiaChi" },
            { COL_DAIDIEN, "DaiDien" },
            { COL_DIENTHOAI, "DienThoai" },
            { COL_MADANHNGHIEP, "MaDoanhNghiep" },
            { COL_MASOTHUE, "MaSoThue" },
            { COL_DONVITRUCTHUOC, "DonViTrucThuoc" }
        };
        public static Dictionary<string, string> DIC_TenCotUngVoiDb_CT = new Dictionary<string, string>()
        {
            { COL_TEN, "Ten" },
            { COL_DIACHI, "DiaChi" },
            { COL_DIENTHOAI, "DienThoai" },
            { COL_MASOTHUE, "GiaTri" }
        };
        public static Dictionary<string, string> DIC_TenNhathau = new Dictionary<string, string>()
        {
            { TBL_THONGTINNHATHAU, "Nhà thầu chính" },
            { TBL_THONGTINNHATHAUPHU, "Nhà thầu phụ" },
            { TBL_THONGTINNHACUNGCAP, "Nhà cung cấp" },
            { TBL_THONGTINTODOITHICONG, "Tổ đội" }
        };
        public static Dictionary<string, string> DIC_vitrithuchien = new Dictionary<string, string>()
        {
            { "Nhà thầu chính",TBL_THONGTINNHATHAU },
            { "Nhà thầu phụ", TBL_THONGTINNHATHAUPHU},
            {"NCC",TBL_THONGTINNHACUNGCAP},
            {"Tổ đội" , TBL_THONGTINTODOITHICONG},
            { "Công trình" , TBL_THONGTINCONGTRINH},
            { "Hạng mục" , TBL_THONGTINHANGMUC}
        };
        public static Dictionary<string, string> DIC_TaomoiHD = new Dictionary<string, string>()
        {
            { TBL_THONGTINDUAN , "CodeDuAn"},
            { TBL_THONGTINCONGTRINH , "CodeCongTrinh"},
            {TBL_THONGTINHANGMUC, "CodeHangMuc"},
            { TBL_THONGTINNHATHAU,"CodeNhaThau" },
            { TBL_THONGTINNHATHAUPHU, "CodeNhaThauPhu"},
            {TBL_THONGTINNHACUNGCAP,"CodeNcc"},
            {TBL_THONGTINTODOITHICONG , "CodeToDoi"}
        };
        public static Dictionary<string, string> DIC_SHEETHD = new Dictionary<string, string>()
        {
            { CONST_SheetName_TTKLHT , TBL_hopdongAB_HT},
            { CONST_SheetName_TTKLPS , TBL_hopdongAB_PS},
            {CONST_SheetName_TTKLTT, TBL_hopdongAB_TT}
        };
        public static Dictionary<string, string> DIC_NhaThau_ToDoi_NTP = new Dictionary<string, string>()
        {
            { TBL_THONGTINNHATHAU,"CodeNhaThau" },
            { TBL_THONGTINNHATHAUPHU, "CodeNhaThauPhu"},
            {TBL_THONGTINTODOITHICONG , "CodeToDoi"}
        };
        public static Dictionary<string, string> DIC_TaomoiSHD = new Dictionary<string, string>()
        {
            { TBL_THONGTINNHATHAU,"- HĐTC" },
            { TBL_THONGTINNHATHAUPHU, "- HĐTCTP"},
            {TBL_THONGTINNHACUNGCAP,"- HĐNCC"},
            {TBL_THONGTINTODOITHICONG , "- HĐTD"},
            { TBL_THONGTINCONGTRINH , "- HĐDA"},
            {TBL_THONGTINHANGMUC, "- HĐDA"}
        };
        public static List<string> LOAIDOITUONG = new List<string>()
        {
            "DuAn",
            "CongTrinh",
            "Đại diện Chủ đầu tư",
            "Hạng mục",
            "Nhà thầu chính",
            "Nhà thầu phụ",
            "NCC",
            "Tổ đội"
        };
        #region Tabcontrol
        public const string TAB_THONGTINDUAN = "Thông tin dự án";
        public const string TAB_DanhSachDuAnCongTrinh = "Danh sách Dự án - Công trình";
        public const string TAB_TienDoKeHoach = "QL tiến độ - Kế hoạch";
        #endregion

        //#region Thành Phần và nhóm tham gia công trình
        //public const int TYPE_THANHPHANTHAMGIA = 0;
        //public const int TYPE_NHOMTHAMGIA = 1;
        //public const int TYPE_GV_KEHOACH_NGUOITHAMGIA = 2;
        //public const int TYPE_GV_KEHOACH_NHACUNGCAP = 3;
        //public const int TYPE_GV_KEHOACH_NHATHAU = 4;
        //public const int TYPE_GV_KEHOACH_TODOI = 5;
        //public const int TYPE_QLFILE_CONGVANDIDEN = 6; //Quản lý file
        //public const int TYPE_QLFILE_KEHOACHGIAOVIEC = 7;
        //public const int TYPE_QLFILE_CongViecHangNgay = 8;
        //public const int TYPE_QLFILE_KeHoachGiaoViec_ChiaThiCong = 9;


        //public const int TYPE_GV_QTMH_TIMNCC_NguoiThamGia = 8;
        //public const int TYPE_GV_QTMH_CHONNCC_NguoiThamGia = 9;
        //public const int TYPE_GV_QTMH_DUYETPA_NguoiThamGia = 10;

        //#endregion

        #region Phần giao việc
        public static string[] HangMucCVMuaHang = new string[]
        {
                "MUA VẬT TƯ",
                "MUA THIẾT BỊ",
                "MUA SẢN PHẨM KHÁC",
                "ĐỀ XUẤT VẬT TƯ"
        };

        public static string[] HangMucCVKhac = new string[]
        {
                "CHUẨN BỊ PHÁP LÝ ĐẦU TƯ",
                "KHẢO SÁT",
                "THIẾT KẾ KIẾN TRÚC",
                "THIẾT KẾ KẾT CẤU",
                "LẬP DỰ TOÁN",
                "LẬP TIẾN ĐỘ",
                "LẬP HỒ SƠ PHÁP LÝ",
                "LẬP GIÁ THẦU",
                "PHẦN MÓNG",
                "PHẦN THÂN",
                "PHẦN MÁI",
                "PHẦN HOÀN THIỆN",
                "PHẦN KẾT CẤU",
                "PHẦN NỘI THẤT",
                "LÀM HỒ SƠ NGHIỆM THU",
                "LÀM HỒ SƠ THANH TOÁN",
                "LÀM HỒ SƠ QUYẾT TOÁN",
                "LÀM THỦ TỤC BÀN GIAO - BẢO HÀNH",
                "CÁC CÔNG VIỆC KHÁC"
        };


        /// <summary>
        /// Nháp
        /// </summary>


        public const int CONST_GV_QTTH_THEMMOI = 0;
        public const int CONST_GV_QTTH_CHINHSUA = 1;

        public const string Range_KeHoach = "DanhSachCongViec";

        //public static Dictionary<string, string> DIC_GiaoViec_TenCotUngVoiDb = new Dictionary<string, string>();

        public const string TRANGTHAI_QTMH_DeXuat = "Đề xuất";
        public const string TRANGTHAI_QTMH_TimNCC = "Tìm nhà cung cấp";
        public const string TRANGTHAI_QTMH_ChonNCC = "Chọn nhà cung cấp";
        public const string TRANGTHAI_QTMH_DuyetPA = "Duyệt phương án";

        public const string CONST_LoaiQT_QTMH = "Mua hàng";
        public const string CONST_LoaiQT_Khac = "DuAn";

        public const string TRANGTHAI_ChuaThucHien = "Chưa thực hiện";

        public const string TRANGTHAI_QTTH_DangThucHien = "Đang thực hiện";
        public const string TRANGTHAI_QTTH_DangXetDuyet = "Đang xét duyệt";
        public const string TRANGTHAI_QTTH_DeNghiKiemTra = "Đề nghị kiểm tra";
        public const string TRANGTHAI_QTTH_HoanThanh = "Hoàn thành";
        public const string TRANGTHAI_QTTH_DungHoatDong = "Dừng hoạt động";
        #endregion

        public static string[] lsQTMH = { TRANGTHAI_QTMH_DeXuat, TRANGTHAI_QTMH_TimNCC, TRANGTHAI_QTMH_ChonNCC, TRANGTHAI_QTMH_DuyetPA };
        public static string[] lsQTTH = { TRANGTHAI_QTTH_DangThucHien, TRANGTHAI_QTTH_DangXetDuyet, TRANGTHAI_QTTH_DeNghiKiemTra, TRANGTHAI_QTTH_HoanThanh };

        ///// <summary>
        ///// Dùng để chạy phần đường găng quy trình
        ///// </summary>
        //public const int CONST_ChiCoCongViecOTruoc = 0; //Chỉ có công việc trước
        //public const int CONST_ChiCoCongViecOSau = 1; //Chỉ có công việc trước
        //public const int CONST_CongViecCaTruocVaSau = 2; //Chỉ có công việc trước
        //public const int CONST_KhongCoCongViecCaTruocVaSau = 3; //Chỉ có công việc trước

        #region QUY TRÌNH MUA HÀNG
        public const string TBL_GV_QTMH_ThongTin = "Tbl_GV_QTMH_ThongTin";
        public const string TBL_GV_QTMH_DeXuat = "Tbl_GV_QTMH_DeXuat";
        public const string TBL_GV_QTMH_DeXuat_VatTu = "Tbl_GV_QTMH_DeXuat_VatTu";
        public const string TBL_GV_QTMH_LuaChonNhaCungCap = "Tbl_GV_QTMH_LuaChonNhaCungCap";
        public const string TBL_GV_QTMH_DuyetPhuongAn = "Tbl_GV_QTMH_DuyetPhuongAn";
        public const string TBL_GV_QTMH_NhaCungCap = "Tbl_GV_QTMH_NhaCungCap";
        public const string TBL_GV_QTMH_TenQTMH = "Tbl_GV_QTMH_TenQuyTrinhMuaHang";
        public const string TBL_GV_QTMH_TimNhaCungCapBaoGia = "Tbl_GV_QTMH_TimNhaCungCapBaoGia";
        public const string TBL_GV_QTMH_TimNhaCungCapBaoGia_NguoiThamGia = "Tbl_GV_QTMH_TimNhaCungCapBaoGia_NguoiThamGia";
        public const string TBL_GV_QTMH_ListNCC = "Tbl_GV_QTMH_NhaCungCap";
        public const string TBL_GV_QTMH_NCCDaChon = "Tbl_GV_QTMH_NhaCungCapDaChon";
        public const string TBL_GV_QTMH_NCCDaDuyet = "Tbl_GV_QTMH_NhaCungCapDaDuyet";



        //public static Dictionary<string, string> DIC_QTMH_MapCongTacTruoc = new Dictionary<string, string>()
        //{
        //    { "Duyệt đề xuất", "DeXuat_ThoiGianDuyetDeXuat" },//Key: Hiển thị trên combobox, Value: Giá trị - Là tên cột để cộng ngày trong db


        //}
        //public static List<string> LIST_TBL_QTMH = new List<string>()
        //{
        //    TBL_GV_QTMH_DeXuat,
        //    TBL_GV_QTMH_LuaChonNhaCungCap,
        //    TBL_GV_QTMH_TimNhaCungCapBaoGia,
        //    TBL_GV_QTMH_DuyetPhuongAn
        //};

        //public static Dictionary<string, string> DIC_QTMH_Map2Db = new Dictionary<string, string>()
        //{
        //    { PhanMemQuanLyThiCong., "Ten" },
        //    { COL_DIACHI, "DiaChi" },
        //    { COL_DAIDIEN, "DaiDien" },
        //    { COL_DIENTHOAI, "DienThoai" },
        //    { COL_MASOTHUE, "MaSoThue" }
        //};
        #endregion

        #region Thực hiện dự án
        public const string COL_CVHN_Date = "B";
        public const string COL_CVHN_TenCV = "C";
        public const string COL_CVHN_NguoiThamGia = "D";
        public const string COL_CVHN_DonVi = "E";
        public const string COL_CVHN_KLKeHoachToanBo = "F";
        public const string COL_CVHN_KLKeHoachtungngay = "G";
        public const string COL_CVHN_KLThucHien = "H";
        public const string COL_CVHN_ChiTiet = "I";
        public const string COL_CVHN_FileDinhKem = "J";
        public const string COL_CVHN_GhiChu = "K";

        public static Dictionary<string, string> DIC_CVHN_Sheet = new Dictionary<string, string>()
        {
            {COL_CVHN_TenCV, "TenCongViec" },
            {COL_CVHN_DonVi, "DonVi" },
            {COL_CVHN_KLThucHien, "KhoiLuong" },
            {COL_CVHN_KLKeHoachToanBo, "KhoiLuongKeHoach" },
            {COL_CVHN_KLKeHoachtungngay, "Khối lượng kế hoạch hàng ngày" },
            {COL_CVHN_ChiTiet, "ChiTietCongViecHangNgay" },
            {COL_CVHN_GhiChu, "Ghichu" }
        };
        #endregion




        //public const string HAUTO_CONGTHUC = "_CongThuc";//Hậu tố cho các col chứa công thức trong DB
        //public const string HAUTO_IsMacDinh = "_Iscongthucmacdinh)";//Hậu tố cho các col chứa công thức trong DB

        #region Lộc
        /// <summary>
        /// Kế hoạch vật tư
        /// </summary>
        public const string COL_KHVT_CodeCT = "Code";
        public const string COL_KHVT_RowCha = "RowCha";
        public const string COL_KHVT_STT = "STT";
        public const string COL_KHVT_MaVatTu = "MaVatTu";
        public const string COL_KHVT_TenVatTu = "TenVatTu";
        public const string COL_KHVT_DonVi = "DonVi";
        public const string COL_KHVT_KhoiLuongDinhMuc = "KhoiLuongDinhMuc";
        public const string COL_KHVT_KhoiLuongKeHoach = "KhoiLuongKeHoach";
        public const string COL_KHVT_DonGiaGoc = "DonGiaGoc";
        //public const string COL_KHVT_ThanhTienGiaGoc = "ThanhTienGiaGoc";
        public const string COL_KHVT_DonGiaKeHoach = "DonGiaKeHoach";
        //public const string COL_KHVT_ThanhTienKeHoach = "ThanhTienKeHoach";
        public const string COL_KHVT_ThoiGianTu = "ThoiGianTu";
        public const string COL_KHVT_ThoiGianDen = "ThoiGianDen";
        public const string COL_KHVT_QuyCach = "QuyCach";
        public const string COL_KHVT_NhanHieu = "NhanHieu";
        public const string COL_KHVT_XuatXu = "XuatXu";
        public const string COL_KHVT_GhiChu = "GhiChu";
        ///<summary>
        ///Hợp đồng
        /// </summary>
        public const string TBL_ThongTinHopDong = "Tbl_HopDong_PhuLucHD";
        public const string TBL_HopDongDuLieuGoc = "Tbl_HopDong_DuLieuGoc";
        public const string TBL_ThongTinHopDongChiTietHD = "Tbl_HopDong_PhuLucHD_ChiTietVL";
        public const string TBL_ThongTinPL = "Tbl_ThongtinphulucHD";
        public const string TBL_Tonghopdanhsachhopdong = "Tbl_HopDong_TongHop";
        public const string TBL_HopDong_DotHopDong = "Tbl_HopDong_DotHopDong";
        public const string TBL_QUYETTHONGTIN = "Tbl_RangeSeclect";
        public const string TBL_QUYETDICWS = "Tbl_RangeWS";
        public const string TBL_QUYETCLEAR = "Tbl_Rangclear";
        //Vật liệu//
        public const string TBL_QUYETDICWS_VL = "Tbl_RangeWS_VL";
        public const string TBL_KHOILUONG_VL = "Khoiluong_VL";
        public const string TBL_DONGIA_VL = "DonGia_VL";
        public const string TBL_THANHTIEN_VL = "ThanhTIen_VL";
        //Nhân Công
        public const string TBL_QUYETDICWS_NC = "Tbl_RangeWS_NC";
        public const string TBL_KHOILUONG_NC = "Khoiluong_NC";
        public const string TBL_DONGIA_NC = "DonGia_NC";
        public const string TBL_THANHTIEN_NC = "ThanhTIen_NC";
        //Máy thi công
        public const string TBL_QUYETDICWS_MTC = "Tbl_RangeWS_MTC";
        public const string TBL_KHOILUONG_MTC = "Khoiluong_MTC";
        public const string TBL_DONGIA_MTC = "DonGia_MTC";
        public const string TBL_THANHTIEN_MTC = "ThanhTIen_MTC";
        public static Dictionary<string, string> TBL_TAOCOT_VL = new Dictionary<string, string>()
        {
            {TBL_KHOILUONG_VL,"KL_Khối lượng " },
            { TBL_DONGIA_VL,"DG_Đơn giá " },
            {TBL_THANHTIEN_VL,"TT_Thành tiền " }
        };
        public static Dictionary<string, string> TBL_TAOCOT_NC = new Dictionary<string, string>()
        {
            {TBL_KHOILUONG_NC,"KL_Khối lượng " },
            { TBL_DONGIA_NC,"DG_Đơn giá " },
            {TBL_THANHTIEN_NC,"TT_Thành tiền " }
        };
        public static Dictionary<string, string> TBL_TAOCOT_MTC = new Dictionary<string, string>()
        {
            {TBL_KHOILUONG_MTC,"KL_Khối lượng " },
            { TBL_DONGIA_MTC,"DG_Đơn giá " },
            {TBL_THANHTIEN_MTC,"TT_Thành tiền " }
        };
        public const string COL_HD_CodeCT = "Code";
        public const string COL_HD_CodePL = "Code";
        public const string COL_HD_CodeHDCHINH = "CodeHopDong";
        public const string COL_HD_CodeHD_VL = "CodeVatLieu";
        public const string COL_HD_CodeHD_HD = "CodeTHHD";
        public const string COL_HD_CodeHD_DTND = "Code dự thầu ND";
        public const string COL_HD_CodeHD_CHUDAUTU = "CodeTongMucDauTu";
        public const string COL_HD_CodeHD_NGUOIDUNG = "CodeHangMuc_NguoiDungTao";
        public const string COL_HD_CodeHDCHinhPL = "CodePl";
        public const string COL_HD_CodeDuAn = "CodeDuAn";
        public const string COL_HD_CodeLoaiHD = "CodeLoaiHd";
        public const string COL_HD_CodeHD = "CodeHd";
        public const string COL_HD_Tencongviec = "TenCongViec";
        public const string COL_HD_DonVi = "DonVi";
        public const string COL_HD_Khoiluong = "KhoiLuong";
        public const string COL_HD_Dongia = "DonGia";
        public const string COL_HD_ThanhTien = "ThanhTien";
        public const string COL_HD_NBD = "NgayBatDau";
        public const string COL_HD_NKT = "NgayKetThuc";
        public const string COL_HD_Khoiluongthicong = "KhoiLuongThiCong";
        public const string COL_HD_ThanhTienthicong = "ThanhTienThiCong";
        public const string COL_HD_TenPL = "TenPl";
        public const string COL_HD_Kyhieu = "KyHieu";
        public const string COL_HD_Code_CT = "CodeCongTrinh";
        public const string COL_HD_Code_HM = "CodeHangMuc";
        public const string COL_HD_Code_Congtac = "CodeCongTac";
        public const string COL_HD_Phatsinh = "PhatSinh";
        public const string COL_HD_Phuluc = "Phụ lục";
        public const string COL_HD_DVTH = "DonViThucHien";
        public const string COL_HD_TT = "3";
        public const string COL_HD_PS = "4";
        public const string COL_HD_PS_TT = "5";
        public const string NAMEDOT1 = "Đợt 1";
        public static string[] TBL_HOPDONG = new string[]
        {
            "Tbl_hopdongchinhAB",
            "Tbl_hopdongcungcap",
            "Tbl_hopdongkhac",
            "Tbl_hopdongthauphuBB",
            "Tbl_hopdongthinghiem",
            "Tbl_hopdongtodoi",
        };
        public static string[] TBL_HOPDONGAB = new string[]
{
            "Tbl_hopdongAB_HT",
            "Tbl_hopdongAB_PS",
            "Tbl_hopdongAB_TT",
            "Tbl_hopdongAB_TToan",
};
        public const string COL_DXVL_Chọn = "A";
        public const string COL_DXVL_STT = "B";
        public const string COL_DXVL_Code = "C";
        public const string COL_DXVL_Codedựán = "D";
        public const string COL_DXVL_Gửiyêucầu = "E";
        public const string COL_DXVL_Tênvậttư = "F";
        public const string COL_DXVL_Đơnvị = "G";
        public const string COL_DXVL_Yêucầuđợtnày = "H";
        public const string COL_DXVL_HợpđồngKL = "I";
        public const string COL_DXVL_LũykếYêucầu = "J";
        public const string COL_DXVL_Lũykếxuấtkho = "K";
        public const string COL_DXVL_Đơngiáhiệntrường = "L";
        public const string COL_DXVL_Ngườiyêucầu = "M";
        public const string COL_DXVL_Ngườinhận = "N";
        public const string COL_DXVL_Ngàygửiyêucầu = "O";
        public const string COL_DXVL_Ngàycầncóvậttư = "P";
        public const string COL_DXVL_Ưutiên = "Q";
        public const string COL_DXVL_Tạophiếu = "R";
        public const string COL_DXVL_Sốphiếuđềxuấttựđộng = "S";
        public const string COL_DXVL_Nhàcungcấp = "T";
        public const string COL_DXVL_Ngườiphụtrách = "U";
        public const string COL_DXVL_Ngườithamgia = "V";
        public const string COL_DXVL_Ngườitheodõi = "W";
        public const string COL_DXVL_Hợpđồng = "X";
        public const string COL_DXVL_Đơnvịthựchiện = "Y";
        public const string COL_DXVL_Nhắcduyệt = "Z";
        public const string COL_DXVL_Bảnglấyvậttư = "AA";


        public static Dictionary<string, string> DIC_DXVL_TenCotUngVoiDb = new Dictionary<string, string>()
        {
            {COL_DXVL_Chọn, "Chon"},
            {COL_DXVL_STT, "Stt"},
            {COL_DXVL_Code, "Code"},
            {COL_DXVL_Codedựán, "CodeDuAn"},
            {COL_DXVL_Gửiyêucầu, "GuiYeuCau"},
            {COL_DXVL_Tênvậttư, "TenVatTu"},
            {COL_DXVL_Đơnvị, "DonVi"},
            {COL_DXVL_Yêucầuđợtnày, "YeuCauDotNay"},
            {COL_DXVL_HợpđồngKL, "HopDongKl"},
            {COL_DXVL_LũykếYêucầu, "LuyKeYeuCau"},
            {COL_DXVL_Lũykếxuấtkho, "LuyKeXuatKho"},
            {COL_DXVL_Đơngiáhiệntrường, "DonGiaHienTruong"},
            {COL_DXVL_Ngườiyêucầu, "NguoiYeuCau"},
            {COL_DXVL_Ngườinhận, "NguoiNhan"},
            {COL_DXVL_Ngàygửiyêucầu, "NgayGuiYeuCau"},
            {COL_DXVL_Ngàycầncóvậttư, "NgayCanCoVatTu"},
            {COL_DXVL_Ưutiên, "UuTien"},
            {COL_DXVL_Tạophiếu, "TaoPhieu"},
            {COL_DXVL_Sốphiếuđềxuấttựđộng, "SoPhieuDeXuatTuDong"},
            {COL_DXVL_Nhàcungcấp, "NhaCungCap"},
            {COL_DXVL_Ngườiphụtrách, "NguoiPhuTrach"},
            {COL_DXVL_Ngườithamgia, "NguoiThamGia"},
            {COL_DXVL_Ngườitheodõi, "NguoiTheoDoi"},
            {COL_DXVL_Hợpđồng, "HopDong"},
            {COL_DXVL_Đơnvịthựchiện, "DonViThucHien"},
            {COL_DXVL_Nhắcduyệt, "NhacDuyet"},
            {COL_DXVL_Bảnglấyvậttư, "BangLayVatTu"},

        };
        //public const int TYPE_HD_TonghopDanhsachHD = 11;
        public const string TBL_QLFILE_HD = "Tbl_HopDong_FileDinhKem";
        public const string TBL_QLFILE_THUCHITAMUNG = "Tbl_ThuChiTamUng_Filedinhkem";
        public const string TBL_QLFILE_KC = "Tbl_TCTU_KhoanChi_Filedinhkem";
        public const string TBL_QLFILE_KT = "Tbl_TCTU_KhoanThu_Filedinhkem";
        public const string TBL_QLFILE_KCCT = "Tbl_TCTU_KhoanChiChiTiet_Filedinhkem";
        public const string TBL_QLFILE_DeXuat = "Tbl_TCTU_DeXuat_Filedinhkem";
        /// <summary>
        /// CÔNG VĂN ĐI ĐẾN
        /// </summary>
        public const string QL_CVDD = "Tbl_CongVanDiDen_QuanLyCongVan";
        public const string QL_CVDD_Filedinhkem = "Tbl_CongVanDiDen_FileDinhKem";
        public const string QL_CVDD_DVPH = "Tbl_CongVanDiDen_DonViPhatHanh";
        public const string COL_CONGVAN_FILEDINHKEM = "FileDinhKem";
        public const string COL_CONGVAN_DUONGDAN = "DuongDan";
        public const string COL_CONGVAN_Code = "Code";
        public const string COL_CONGVAN_CodeDVPH = "CodeDVPH";
        public const string COL_CONGVAN_MaHoSo = "MaHoSo";
        public const string COL_CONGVAN_NguoiKy = "NguoiKy";
        public const string COL_CONGVAN_TenHoSo = "TenHoSo";
        public const string COL_CONGVAN_DonViPhatHanh = "DonViPhatHanh";
        public const string COL_CONGVAN_Kieu = "Kieu";
        public const string COL_CONGVAN_NgayNhan = "NgayNhan";
        public const string COL_CONGVAN_LoaiCongVan = "LoaiCongVan";
        public const string COL_CONGVAN_NgayMuon = "NgayMuon";
        public const string COL_CONGVAN_LyDoMuon = "LyDoMuon";
        public const string COL_CONGVAN_DaMuon = "DaMuon";
        public const string COL_CONGVAN_DuAn = "DuAn";
        public const string COL_CONGVAN_TrangThai = "TrangThai";
        public const string COL_CONGVAN_SoanThao = "SoanThao";
        public const string COL_CONGVAN_GhiChu = "GhiChu";
        public const string COL_CONGVAN_Stt = "Stt";
        public const string COL_CONGVAN_CodeDA = "CodeDuAn";
        public const string SHEETBAOCAO = "Báo cáo danh sách hồ sơ";
        public const string SHEETDVPH = "Kiểu-Đơn vị phát hành";

        /// <summary>
        /// QLM_THIETBI
        /// </summary>
        ///
        public const string QLMayTB_Danhsachphuongtien = "Tbl_Danhsachphuongtien";
        public const string QLMayTB_Dexuatphuongtien = "Tbl_Dexuatphuongtien";
        public const string QLMayTB_Nhattrinhthicong = "Tbl_NhapTrinhThiCong";
        public const string QLMayTB_SuachuaBaoduong = "Tbl_SuachuaBaoduong";
        ///<summary>
        ///Chấm công
        ///
        public const string Tbl_ChamCong_TaiAnh = "Tbl_ChamCong_TaiAnh";
        public const string Tbl_ChamCong_LichHen = "Tbl_ChamCong_LichHen";
        public const string Tbl_BangChamCongHangThang = "Tbl_ChamCong_BangChamCongHangThang";
        public const string Tbl_LichCongtac = "Tbl_ChamCong_LichCT";
        public const string Tbl_ChamCong_BangNhanVien = "Tbl_ChamCong_BangNhanVien";
        public const string Tbl_ChamCong_BangThanhToanLuong = "Tbl_ChamCong_BangThanhToanLuong";
        public const string Tbl_ChamCong_TamUng = "Tbl_ChamCong_TamUng";
        //<sumary>
        ///Thu chi tạm ứng
        ///
        public const string Tbl_ThuchiTamUng_Dexuat = "Tbl_ThuChiTamUngDeXuat";
        public const string Tbl_ThuchiTamUng_Khoanchi = "Tbl_ThuChiTamUng_KhoanChi";
        public const string Tbl_ThuchiTamUng_Khoanchi_cp = "Tbl_ThuChiTamUng_KhoanChi_ChiPhi";
        public const string Tbl_ThuchiTamUng_Khoanchi_gc = "Tbl_ThuChiTamUng_KhoanChi_GiaiChi";
        //<sumary>
        //danhmuccongtac
        public const string Tbl_DANHMUCCONGTAC_DUTHAU = "Tbl_DanhmuCongTacduthau";
        public const string Tbl_DANHMUCCONGTAC_VL = "Tbl_DanhmucCongTacVatlieu";
        public const string Tbl_DANHMUCCONGTAC_VLDIEDAI = "Tbl_DanhmucCongTacVatlieu_Diendai";
        public const string Tbl_DANHMUCCONGTAC_TONGDAUTU = "Tbl_DanhmuCongTacTongmucdautu";
        //Tạo mới hợp đồng
        public const string Tbl_HopDong_LoaiHopDong = "Tbl_HopDong_LoaiHopDong";
        public const string Tbl_TAOMOIHOPDONG = "Tbl_TaoHopDongMoi";
        public const string Tbl_CHITIETHOPDONG = "Tbl_HopDongChiTiet";
        public const string Tbl_TAOMOIHOPDONG_MECON = "Tbl_TaoHopDongMoi_Hopdongmecon";
        public static Dictionary<string, string> DIC_LOAIHOPDONG = new Dictionary<string, string>()
        {
            { "HỢP ĐỒNG CHÍNH A_B", "11" },
            { "HỢP ĐỒNG THẦU PHỤ B_B", "22" },
            { "HỢP ĐỒNG CUNG CẤP", "33" },
            { "HỢP ĐỒNG TỔ ĐỘI", "44" },
            { "HỢP ĐỒNG KHÁC", "55" },
            { "HỢP ĐỒNG THÍ NGHIỆM", "66" },
            { "KHÔNG THUỘC HỢP ĐỒNG", "77" },
            { "HỢP ĐỒNG THEO DỰ ÁN", "88" },
        };
        public static List<string> CONGTAC = new List<string>()
        {
            "Khối lượng thực tế",
            "Chênh lệch ban đầu và thực tế thi công",
            "Đơn giá thực hiện",
        };
        public static string[] VATLIEU = new string[]
        {
            "Code",
            "MaVatLieu",
            "VatTu",
            "DonVi",
            "KhoiLuongHopDong",
            "DonGiaHopDong"
        };
        /// <summary>
        /// Mẫu dự toán
        /// </summary>
        public static string MauGiaoThongTruongSon = "Mẫu giao thông Trường sơn";
        public static string MauGiaoThongTruongSon1 = "Mẫu giao thông 1 Trường sơn";
        public static string MauGiaoThongTruongSon2 = "Mẫu giao thông 2 Trường sơn";
        public static string MauGiaoThongChungTruongSon = "Mẫu giao thông chung Trường Sơn";
        public static string DuToanF1 = "Dự toán F1";
        public static string DuToanMyHouse = "Mẫu dtPro MyHouse";
        public static string DuToanG8 = "Dự toán G8";
        public static string DuToanDelTa = "Dự toán Delta";
        public static string DuToanEta = "Dự toán Eta";
        public static string DuToanHitosoft = "Dự toán Hitosoft";
        public static string DuToanGXD = "Mẫu dự toán-Dự thầu GXD";
        public static string DuToan360 = "Nghiệm thu 360";
        public static string DuToanQLTC = "Mẫu dự toán Quản lý thi công";
        public static string MauGiaoThong = "Mẫu giao thông";
        public static string MauCanThoKienGiang = "Mẫu Cần Thơ Kiên Giang";
        public static string MauKienGiangCaMau = "Mẫu Kiên Giang Cà Mau ";
        public static string MauKhongCoSan = "Không có mẫu sẵn";
        public static string MauThuCongGiaoThong = "Mẫu thủ công Bộ giao thông";
        public static string MauThuCongCoHaoPhi = "Đọc thủ công lấy hao phí";
        public static string DuToanBacnam = "Dự toán Bắc-Nam";
        public static string TBL_CategoryReadExcel = "Tbl_CategoryReadExcel";
        public static string TBL_InfoReadExcel = "Tbl_InfoReadExcel";

        /// <summary>
        /// BookMarks Tổng Hợp
        /// </summary>
        /// 

        public const string CONST_SheetName_KLPK = "Khối lượng phân khai";
        public const string CONST_SheetName_TenBieuDo = "TenBieuDo";
        public const string CONST_SheetName_TienDuAn = "TienDuAn";
        public const string CONST_SheetName_ThanhTienTCDA = "ThanhTienTCDA";
        public const string CONST_SheetName_TyLeHT = "TyLeHT";
        public const string CONST_SheetName_SoNgay = "SoNgay";
        public const string CONST_SheetName_CTDA = "CTDA";
        public const string CONST_SheetName_CTDTH = "CTDTH";
        public const string CONST_SheetName_CTCTH = "CTCTH";
        public const string CONST_SheetName_CTXD = "CTXD";
        public const string CONST_SheetName_CVKT = "CVKT";
        public const string CONST_SheetName_CTHT = "CTHT";
        public const string CONST_SheetName_CTDungHĐ = "CTDungHĐ";
        public const string CONST_SheetName_ChiPhi = "ChiPhi";
        public const string CONST_SheetName_ChiPhiDuKien = "ChiPhiDuKien";
        public const string CONST_SheetName_HinhAnh = "HinhAnh";
        public const string CONST_SheetName_BieuDo = "BieuDo";
        public const string CONST_SheetName_TenNhaThauChinh = "TENNHATHAU";
        public const string CONST_SheetName_TenDuAn = "DuAn";
        public const string CONST_SheetName_DateHienTai = "DateHienTai";
        public const string CONST_SheetName_DateBaoCao = "DateBaoCao";
        public const string CONST_SheetName_BaoCaoDuAn = "ListTongHop";
        public const string CONST_SheetName_ThuChiTamUng = "ThuChiTamUng";
        public const string CONST_SheetName_ThongTinNhaThau = "ThongTinNhaThau";
        public const string CONST_SheetName_TenNhaThau = "TenNhaThau";
        public const string CONST_SheetName_GiaoNhanThau = "ThongTinGiaoNhanThau";
        public const string CONST_SheetName_VatLieu = "VatLieu";
        public const string CONST_SheetName_NhanCong = "NhanCong";
        public const string CONST_SheetName_MayThiCong = "MTC";
        public const string CONST_ListCongTac = "ListCongTac";
        public const string CONST_ListCongTacKyToi = "ListCongTacKyToi";
        public const float width_A4 = 8.27f;//Inchs
        public const float height_A4 = 11.69f;//Inchs

        public const float width_pdf_A4 = 8.5f;//Inchs
        public const float height_pdf_A4 = 11f;//Inchs

        ///<summary>
        ///Báo cáo hằng ngày
        /// </summary>
        public const string CONST_SheetName_TenNguoiXuatBaoCao = "TenNguoiXuatBaoCao";
        public const string CONST_SheetName_DienThoai = "DienThoai";
        public const string CONST_SheetName_ThoiTiet = "ThoiTiet";
        public const string CONST_SheetName_NhanLuc = "NhanLuc";
        public const string CONST_SheetName_GiaoThau = "GiaoThau";
        public const string CONST_SheetName_MayTC = "MayTC";
        public const string CONST_SheetName_DuAn = "TenDuAn";
        public const string CONST_SheetName_CongTacTrongNgay = "CongTacTrongNgay";
        public const string CONST_SheetName_CongTac30Ngay = "CongTac30Ngay";
        public const string CONST_SheetName_ThongTinMayNC = "ThongTinMayNC";
        public const string CONST_SheetName_DeXuatVatLieu = "DeXuatVatLieu";
        public const string CONST_SheetName_VLDeXuat = "VLDeXuat";
        public const string CONST_SheetName_VLTH = "VLTH";
        public const string CONST_SheetName_KhoanChi = "KhoanChi";
        public const string CONST_SheetName_KhoanChiDuyet = "KhoanChiDuyet";
        public const string CONST_SheetName_KhoanChiDeXuat = "KhoanChiDeXuat";
        public const string CONST_SheetName_KhoanThu = "KhoanThu";
        public const string CONST_SheetName_KhoanThuDuyet = "KhoanThuDuyet";
        public const string CONST_SheetName_KhoanThuDeXuat = "KhoanThuDeXuat";
        public const string CONST_SheetName_CongTacHoanThanh = "CongTacHoanThanh";
        public const string CONST_BM_HinhAnhCongTac = "HinhAnhCongTac";




        public const string CONST_BCHN_BM_ChiTietKhoanThu = "ChiTietKhoanThu";
        public const string CONST_BCHN_BM_KhoanThuDeXuat = "KhoanThuDeXuat";

        public const string CONST_BCHN_BM_ChiTietKhoanChi = "ChiTietKhoanChi";
        public const string CONST_BCHN_BM_KhoanChiDeXuat = "KhoanChiDeXuat";

        public const string CONST_BCHN_BM_VatTuThucHien = "VatTuThucHien";
        public const string CONST_BCHN_BM_VatTuDeXuat = "VatTuDeXuat";

        public static string[] BCHN_BMS_NotLoop =
        {
            CONST_BCHN_BM_ChiTietKhoanThu,
            CONST_BCHN_BM_KhoanThuDeXuat,
            CONST_BCHN_BM_ChiTietKhoanChi,
            CONST_BCHN_BM_KhoanChiDeXuat,
            CONST_BCHN_BM_VatTuThucHien,
            CONST_BCHN_BM_VatTuDeXuat,
            CONST_ListCongTacKyToi
        };

        ///<summary>
        ///Formular HopDong
        /// </summary>
        public const string CONST_SOTIENTAMUNG = "SoTienTamUng";
        public const string CONST_SOTIENTHANHTOAN = "SoTienThanhToan";
        public const string CONST_SANLUONGTHICONG = "SanLuongThiCong";
        public const string CONST_TamUngHT = "TamUngHT";
        public const string CONST_ThuHoiTamUngHT = "ThuHoiTamUngHT";
        public const string CONST_TLTTTHT = "TLTTTHT";
        public const string CONST_TamUngPS = "TamUngPS";
        public const string CONST_ThuHoiTamUngPS = "ThuHoiTamUngPS";
        public const string CONST_TLTTTPS = "TLTTTPS";
        ///<summary>
        ///DicVatLieu
        /// </summary>
        public static Dictionary<int, string[]> DIC_LOAIVL = new Dictionary<int, string[]>()
        {
            { 1, new string [] {"Vật liệu","Máy thi công","Nhân công" } },
            { 2, new string [] {"Máy thi công","Nhân công" } },
            { 3, new string [] {"Vật liệu","Nhân công" } },
            { 4, new string [] {"Vật liệu","Máy thi công" } },
            { 5, new string [] {"Nhân công" } },
            { 6, new string [] {"Máy thi công"} },
            { 7, new string [] {"Vật liệu"} },
            { 8, new string [] {"Ngày"} }
        };
        public static Dictionary<string, Color> DIC_Color = new Dictionary<string, Color>()
        {
            {"Vật liệu", Color.Orange },
            {"Nhân công", Color.Green },
            {"Máy thi công", Color.Purple },
        };
        public enum LsTongHopEnum
        {
            STT,
            Ten,
            NBD,
            SoNgay,
            NKT,
            DonVi,
            KLHD,
            GhiChu,
        }
        public enum LstBaoCaoMay
        {
            STT,
            TenNguoiVanHanh,
            Ten,
            BienSo,
            TenVietTat,
            No,
            DonVi,
            TenChuSoHuu,
            XuatXu,
            NamSanXuat,
            SoKhung,
            GiaMuaMay,
            GiaCaMay,
            NhienLieuChinh,
            HaoPhi,
            ChiPhiSuaChua,
            ChiPhiKhac,
            TienTaiXe,
            CaMayKm,
            NgayMuaMay,
            ViTriMay,
            TenTrangThai,
            TenDuAn,
            GhiChu,
        }
        public static string[] TrangThai =
            {
            "Đang thực hiện",
            "Đang xét duyệt",
            "Đề nghị kiểm tra",
            "Đang chỉnh sửa"
        };
        public static Dictionary<string, string> Dic_DonGia = new Dictionary<string, string>()
            {
                {"AJ","AE"},
                {"AK","AF"},
                {"AL","AG"},
            };
        public static string DonGiaVinhThanh = $"V{{0}}:X{{0}}";
        public static string DonGiaTanHiep = $"Y{{0}}:AA{{0}}";
        public static string DonGiaChauThanh = $"AB{{0}}:AD{{0}}";
        public static Dictionary<string, string> Dic_DonGiaVatTu = new Dictionary<string, string>()
            {
            {DonGiaVinhThanh,"H"},
            {DonGiaTanHiep,"J"},
            {DonGiaChauThanh,"L"},
            };
        #endregion

        #region Tuan
        public const string TITLE_HEADER = "TitleHeader";
        public const string BODYDATA = "BodyData";
        public const string STT = "Stt";
        public const string CODE = "Code";
        public const string TENMAYTHICONG = "TenMayThiCong";
        public const string CODECONGTACTheoGiaiDoan = "CodeCongTacTheoGiaiDoan";
        public const string GIOBATDAUSANG = "GioBatDauSang";
        public const string GIOKETTHUCSANG = "GioKetThucSang";
        public const string GIOBATDAUCHIEU = "GioBatDauChieu";
        public const string GIOKETTHUCCHIEU = "GioKetThucChieu";
        public const string GIOBATDAUTOI = "GioBatDauToi";
        public const string GIOKETTHUCTOI = "GioKetThucToi";
        public const string TONGGIO = "TongGio";
        public const string HINHANH = "HinhAnh";
        public const string NGAYTHANG = "NgayThang";
        public const string NHIENLIEUCHINH = "NhienLieuChinh";
        public const string NHIENLIEUPHU = "NhienLieuPhu";
        public const string GHICHU = "GhiChu";
        public const string TENCONGTAC = "TenCongTac";
        public const string DONVI = "DonVi";
        public const string KHOILUONG = "KhoiLuong";
        public const string TENHANGMUC = "TenHangMuc";
        public const string CODEGIAIDOAN = "CodeGiaiDoan";
        public const string CODENHATHAU = "CodeNhaThau";
        public const string CODETODOI = "CodeToDoi";
        public const string CODENHATHAUPHU = "CodeNhaThauPhu";
        public const string TYPEROW = "TypeRow";
        public const string CODECONGTACTHUCONG = "CodeCongTacThuCong";
        public const string HESOSANG = "HeSoSang";
        public const string HESOCHIEU = "HeSoChieu";
        public const string HESOTOI = "HeSoToi";
        public const string TONGCONG = "TongCong";
        public const string TONGGIOSANG = "TongGioSang";
        public const string TONGGIOCHIEU = "TongGioChieu";
        public const string TONGGIOTOI = "TongGioToi";
        public const string KmDau = "KmDau";
        public const string KmCuoi = "KmCuoi";
        public const string TienTaiXe = "TienTaiXe";
        public const string LyLichSuaChua = "LyLichSuaChua";
        public const string IsTongGio = "IsTongGio";

        #endregion
    }
}
