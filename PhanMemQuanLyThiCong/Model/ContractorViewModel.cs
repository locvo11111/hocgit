using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Model
{
    public class ContractorViewModel : Tbl_ThongTinNhaThauPhuViewModel
    {

        public TypeContractor type { get; set; }
        public string DisplayType
        {
            get
            {
                return type.GetEnumDisplayName();
            }
        }

        public string colFK
        {
            get
            {
                return type.GetEnumDescription();
            }
        }

        public string LoaiGiaoNhanThau
        {
            get
            {
                switch (type)
                {
                    case TypeContractor.NHATHAU:
                        return "Giao thầu";
                    case TypeContractor.NHATHAUPHU:
                    case TypeContractor.SELF:
                    case TypeContractor.TODOITHICONG:
                        return "Nhận thầu";
                    default:
                        return "";
                }
            }
        }

        public string LoaiDVTH
        {
            get
            {
                switch (type)
                {
                    case TypeContractor.NHATHAU:
                        return null;
                    case TypeContractor.SELF:
                        return MyConstant.LoaiDVTH_TuThucHien;
                    case TypeContractor.NHATHAUPHU:
                        return MyConstant.LoaiDVTH_NhaThauPhu;
                    case TypeContractor.TODOITHICONG:
                        return MyConstant.LoaiDVTH_ToDoiThiCong;
                    default:
                        return "";
                }
            }
        }
    }
}
