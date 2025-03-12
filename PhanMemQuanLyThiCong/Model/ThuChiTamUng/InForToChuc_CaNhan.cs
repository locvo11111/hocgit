using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class InForToChuc_CaNhan
    {
        public string Code { get; set; }
        public string Ten { get; set; }
        public string CodeDuAn { get; set; }
        public string Table { get; set; }
        public string Decription
        {
            get
            {
                switch (Table)
                {
                    case MyConstant.TBL_THONGTINNHATHAU:
                        return (Code == CodeDuAn) ? MyConstant.LoaiDVTH_TuThucHien : MyConstant.LoaiDVTH_NhaThau;
                    case MyConstant.TBL_THONGTINNHATHAUPHU:
                        return MyConstant.LoaiDVTH_NhaThauPhu;
                    case MyConstant.TBL_THONGTINTODOITHICONG:
                        return MyConstant.LoaiDVTH_ToDoiThiCong;         
                    case MyConstant.TBL_THONGTINNHACUNGCAP:
                        return MyConstant.LoaiDVTH_NhaCungCap;
                    default:
                        return "Nhân Viên";
                }
            }
        }
        public string Detail {
            get {
                switch (Decription)
                {
                    case "Nhân Viên":
                        return "Cá nhân";
                    default:
                        return "Tổ chức";                   
                }
            } }
        public string ColCodeFK
        {
            get
            {
                switch (Table)
                {
                    case MyConstant.TBL_THONGTINNHATHAU:
                        return "CodeNhaThau";
                    case MyConstant.TBL_THONGTINNHATHAUPHU:
                        return "CodeNhaThauPhu";
                    case MyConstant.TBL_THONGTINTODOITHICONG:
                        return "CodeToDoi";           
                    case MyConstant.TBL_THONGTINNHACUNGCAP:
                        return "CodeNhaCungCap";
                    default:
                        return "CodeNhanVien";
                }
            }
        }
        public string TenGhep
        {
            get
            {
                return $"{Ten} ({Decription},{Detail})";
            }
        }
    }
}
