using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class QuanLyDanhSachKhoDuAn
    {
        public string ID { get; set; }
        public string FileDinhKem { get; set; } = "Xem File";
        public bool Chon { get; set; } = false;
        public string ParentID { get; set; }
        public string TenKho { get; set; }
        public string GhiChu { get; set; }
        public int SortId { get; set; }
        public string CodeDuAn { get; set; }
        public string CodeHangMuc { get; set; }
        public string CodeCongTrinh { get; set; }
        public string CodeKhoChung { get; set; }
        public string ColCode
        {
            get
            {
                if (!string.IsNullOrEmpty(CodeDuAn))
                    return "CodeDuAn";  
                else if (!string.IsNullOrEmpty(CodeHangMuc))
                    return "CodeHangMuc";  
                else if (!string.IsNullOrEmpty(CodeCongTrinh))
                    return "CodeCongTrinh";
                else
                    return "CodeKhoChung";
            }
        }
    }
}
