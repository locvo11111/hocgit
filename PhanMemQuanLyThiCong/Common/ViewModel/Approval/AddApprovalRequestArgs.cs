using System.Collections.Generic;
//using VChatCore.Model.SyncSqlite;
using VChatCore.ViewModels.SyncSqlite;

namespace VChatCore.ViewModels.Approval
{
    public class AddApprovalRequestArgs
    {
        public List<Tbl_GiaoViec_CongViecChaViewModel> CongViecChas { get; set; }
        public List<Tbl_QLVT_YeuCauVatTuViewModel> YeuCauVatTus { get; set; }
        public List<Tbl_QLVT_NhapvattuViewModel> NhapVatTus { get; set; }
        public List<Tbl_QLVT_ChuyenKhoVatTuViewModel> ChuyenKhos { get; set; }
    }
}
