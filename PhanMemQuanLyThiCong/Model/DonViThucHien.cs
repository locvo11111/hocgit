using DevExpress.Spreadsheet.Charts;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class DonViThucHien
    {
        public string Code { get; set; }
        public string CodeDuAn { get; set; }
        public string Ten { get; set; }
        public string Table { get; set; }
        public bool IsSoSanh { get; set; } = true;

        public string CodeFk
        {
            get
            {
                return $"{Code}_{Table}";
            }
        }

        public string CodeTongThau { get; set; }

        public string LoaiDVTH {
            get
            {
                switch (Table)
                {
                    case MyConstant.TBL_THONGTINNHATHAU:
                        return null;
                    case MyConstant.TBL_THONGTINNHATHAUPHU:
                        if (!string.IsNullOrEmpty(CodeTongThau))
                            return MyConstant.LoaiDVTH_TuThucHien;
                        else
                            return MyConstant.LoaiDVTH_NhaThauPhu;
                    case MyConstant.TBL_THONGTINTODOITHICONG:
                        return MyConstant.LoaiDVTH_ToDoiThiCong;
                    case MyConstant.TBL_THONGTINNHACUNGCAP:
                        return MyConstant.LoaiDVTH_NhaCungCap;
                    default:
                        return "";
                }
            }
        }

        public bool IsGiaoThau
        {
            get
            {

                switch (Table)
                {
                    case MyConstant.TBL_THONGTINNHATHAU:
                        return true;
                    default:
                        return false;
                }
            }
        }
        public string LoaiGiaoNhanThau
        {
            get
            {
                switch (Table)
                {
                    case MyConstant.TBL_THONGTINNHATHAU:
                        return "Giao thầu";
                    case MyConstant.TBL_THONGTINNHATHAUPHU:
                    case MyConstant.TBL_THONGTINTODOITHICONG:
                    case MyConstant.TBL_THONGTINNHACUNGCAP:
                        return "Nhận thầu";
                    default:
                        return "";
                }
            }
        }


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
                        return "CodeNcc";
                    default:
                        return "";
                }
            }
        }

        public string TenGhep 
        { 
            get
            {
                return $"{Ten} ({LoaiGiaoNhanThau})";
            } 
        }
    }
}
