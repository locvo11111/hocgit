using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public static class DanhSachNhanVienConstant
    {
        public const string TBL_CHAMCONG_BANGNHANVIEN = "Tbl_ChamCong_BangNhanVien";
        public const string TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET = "Tbl_ChamCong_CongNhatCongKhoan_ChiTiet";
        public const string TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN = "Tbl_ChamCong_CongNhatCongKhoan";
        public const string TBL_CHAMCONG_BANGNHANVIEN_CHITIETPHUPHI = "Tbl_ChamCong_ChiTietPhuPhiNhanVien";
        public const string TBL_CHAMCONG_GIOCHAMCONG = "Tbl_ChamCong_GioChamCong";
        public const string TBL_CHAMCONG_TAMUNG = "Tbl_ChamCong_TamUng";
        public const string TBL_CHAMCONG_TAMUNG_FILEDINHKEM = "Tbl_ChamCong_TamUng_FileDinhKem";
        public const string TBL_CHAMCONG_KYHIEU = "Tbl_ChamCong_KyHieu";
        public const string TBL_CHAMCONG_CAIDATHESO = "Tbl_ChamCong_CaiDatHeSo";
        public const string TBL_CHAMCONG_CAIDATGIOLAM = "Tbl_ChamCong_CaiDatGioLam";
        public const string TBL_CHAMCONG_TENPHONGBAN = "Tbl_ChamCong_BangPhongBan";
        public const string TBL_CHAMCONG_VITRINHANVIEN = "Tbl_ChamCong_ViTriNhanVien";
        public const string TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM = "Tbl_BangNhanVien_FileDinhKem";
        public const string COL_STT = "STT";
        public const string COL_MaNhanVien = "MaNhanVien";
        public const string COL_TenNhanVien = "TenNhanVien";
        public const string COL_ChucVu = "ChucVu";
        public const string COL_SoTien = "SoTien";
        public const string COL_NoiDungUng = "NoiDungTamUng";
        public const string COL_NgayThangUng = "NgayThangUng";
        public const string COL_GiayTo = "GiayTo";
        public const string COL_LoaiNhanVien = "LoaiNhanVien";
        public const string COL_GhiChu = "GhiChu";
        public const string COL_Code = "Code";
        public const string COL_CodePhuPhi = "CodePhuPhi";
        public const string COL_CodeNhanVien = "CodeNhanVien";
        public static  Dictionary<string, string> NN = new Dictionary<string, string>()
            {
                {"Nghỉ phép","NghiPhep" },
                {"Nghỉ ốm","NghiOm" },
                {"Học tập","HocTap" },
                {"Nghỉ đẻ","NghiDe" },
                {"Nghỉ công tác","CongTac" },
                {"Nghỉ theo chế độ","NghiTheoCheDo" },
                {"Nghỉ thứ 7, Chủ nhật","NghiThu7ChuNhat" },
                {"Nghỉ không lý do","NghiKhongLyDo" }
            };        
        public static  Dictionary<string, string> PhanTramNN = new Dictionary<string, string>()
            {
                {"NghiPhep","PhanTramNghiPhep" },
                {"NghiOm","" },
                {"HocTap","PhanTramHocTap" },
                {"NghiDe","PhanTramNghiDe" },
                {"CongTac","PhanTramCongTac"},
                {"NghiTheoCheDo","PhanTramNghiTheoCheDo"},
                {"NghiThu7ChuNhat","PhanTramNghiThu7ChuNhat" },
                {"NghiKhongLyDo","PhanTramNghiKhongLyDo" }
            };   
        public static  Dictionary<string, string> TangCa = new Dictionary<string, string>()
            {
                {"Tăng ca thứ 7","TangCaThu7" },
                {"Tăng ca chủ nhật","TangCaChuNhat" },
                {"Tăng ca ngày lễ","TangCaNgayLe" },
                {"Tăng ca cả tuần","TangCaNgayTuan" }
            };     
        public static  Dictionary<string, int> LoaiNhanVien = new Dictionary<string, int>()
            {
                {"Văn phòng",1 },
                {"Công trường",2 },
                {"Công nhật",3 },
                {"Công khoán",4 },
                {"Không kiểm soát giờ",4 }
            };     
        public static  Dictionary<DayOfWeek, string> NgayTrongTuan = new Dictionary<DayOfWeek, string>()
            {
                {DayOfWeek.Monday,"Thứ hai" },
                {DayOfWeek.Tuesday,"Thứ ba" },
                {DayOfWeek.Wednesday,"Thứ tư" },
                {DayOfWeek.Thursday,"Thứ năm" },
                {DayOfWeek.Friday,"Thứ sáu" },
                {DayOfWeek.Saturday,"Thứ bảy" },
                {DayOfWeek.Sunday,"Chủ nhật" }
            };
    }
}
