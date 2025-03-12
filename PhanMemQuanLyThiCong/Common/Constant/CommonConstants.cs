using System.Collections.Generic;
using System.Linq;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public static class CommonConstants
    {
      

        public const string LOG_SQL = "Run SQL: {0} - Width Param: {1} ";

        public const string FOLDER_TEMP = @"\Temp\";
        public const string FILE_BAOCAOHANGNGAY = "Xuất báo cáo hàng ngày.xlsx";
        public const string bm_ListCongTac = "ListCongTac";


        public const string bm_Type_Col = "Cột";
        public const string bm_Type_Range = "Vùng";
        public const string bm_Type_Detail = "Chi tiết";
        public const string bm_Type_HangMuc = "Hạng mục";
        public const string bm_Type_BatDau = "Bắt đầu";
        public const string bm_Type_KetThuc = "Kết thúc";
        public const string bm_Field_ChiTietDinhMuc = "ChiTietDinhMucs";
        public const string bm_ChiTietNghiemThu = "ChiTietNghiemThu";
        //public const string bm_ListCongTac = "ListCongTac";
        public const string bm_TenCongTrinh = "TenCongTrinh";
        public const string bm_DiaDiemXayDung = "DiaDiemXayDung";
        public const string bm_ChuDauTu = "ChuDauTu";
        public const string bm_DonViGiamSat = "DonViGiamSat";
        public const string bm_DonViThiCong = "DonViThiCong";
        public const string bm_NgayBaoCao = "NgayBaoCao";

        public static string NAME_PHANMEM = "Quản lý thi công";
        public const string COMMAND_ADD = "Add";
        public const string COMMAND_APPROVE = "Approve";
        public const string COMMAND_DELTE = "Delete";
        public const string COMMAND_EDIT = "Edit";
        public const string COMMAND_VIEW = "View";

        //public const


        /// <summary>
        /// Phương thức đổi chuỗi về dạng list
        /// </summary>
        /// <param name="tkey">Chuỗi truyền vào</param>
        /// <returns>list nhận được</returns>
        public static List<string> GetTkeys(string tkey)
        {
             return tkey.Split(new char[] { ',' }).ToList();
        }
        public static List<string> GetFileImgExt()
        {
            return new List<string>() { ".jpg",".JPG",".jpeg",".JPEG",".png",".PNG", ".gif",".GIF",".bmp",".BMP", ".jfif", ".JFIF" };
        }
        
        public static List<string> GetFileDocExt()
        {
            return new List<string>() { ".doc",".DOC",".docx",".DOCX" };
        }
        
        public static List<string> GetFileExcelExt()
        {
            return new List<string>() { ".xls",".XLS",".xlsx",".XLSX" };
        }
        
        public static List<string> GetFileCsvExt()
        {
            return new List<string>() { ".csv",".CSV" };
        }

        public static List<string> GetFilePdfExt()
        {
            return new List<string>() { ".pdf",".PDF" };
        }


        public static List<string> GetFileName()
        {
            return new List<string>() { ".doc", ".DOC", ".docx", ".DOCX", ".xls", ".XLS", ".xlsx", ".XLSX", ".pdf", ".PDF" };
        }

    }
}
