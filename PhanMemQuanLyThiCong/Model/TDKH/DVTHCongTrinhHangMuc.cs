using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.TDKH
{

    public enum TypeBaoCao
    {
        DongTien,
        KhoiLuong
    }    
    public enum TypeChiTiet
    {
        CongTac,
        HaoPhiVatTu
    }
    public enum TypeDVTH
    {
        NhaThau,
        NhaThauPhu,
        ToDoiThiCong,
        NhaCungCap,
        TuThucHien,
        DuAn
    }

    public class DVTHCongTrinhHangMuc
    {
        //create Properties Code, ParentCode, CodeCongTrinh, CodeHangMuc, TenCongTrinh, TenHangMuc, CodeDVTH, TenDVTH, double GiaTri
        public int SortId { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string CodeCongTrinh { get; set; }
        public string CodeHangMuc { get; set; }
        public string TenCongTrinh { get; set; }
        public string TenHangMuc { get; set; }
        public string CodeDVTH { get; set; }
        public string TenDVTH { get; set; }
        public double? GiaTri { get; set; }
        public int? CountCT { get; set; }

        public string Display
        {
            get => $"{TenHangMuc} ({CountCT} công tác)";
        }
    }
}
