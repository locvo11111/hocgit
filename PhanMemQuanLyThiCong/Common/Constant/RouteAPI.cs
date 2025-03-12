using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public class RouteAPI
    {
        
        public const string SUFFIX_AddOrUpdate = "CreateOrUpdate";
        public const string SUFFIX_AddOrUpdateMulti = "CreateOrUpdateMulti";
        public const string SUFFIX_CreateMulti = "CreateMulti";
        public const string SUFFIX_DeleteByVm = "deletebyVM";
        public const string SUFFIX_Delete = "delete";
        public const string SUFFIX_DeleteMulti = "DeleteMulti";
        public const string SUFFIX_UpdateMulti = "UpdateMulti";

        public const string SUFFIX_GetAll = "GetAll";
        public const string SUFFIX_Create = "create";
        public const string SUFFIX_Update = "update";
        public const string SUFFIX_GetById = "GetById";

        public const string USER_LOGIN = "users/login";
        public const string USER_All = "users/getall";
        public const string USER_ChangePassword = "users/ChangePassword";
        public const string USER_ChangeSerialNo = "users/ChangeSerialNo";
        public const string USER_Current = "users/CurrentUser";

        public const string USER_REGISTER = "users/add";

        public const string USER_LOGOUT = "users/logout";

        public const string USER_VALIDATETOKEN = "users/ValidateToken";
        
        public const string USER_GETBYEMAILORPHONE = "users/accountchat";

        public const string USER_SyncUserBySerialNo = "users/SyncUserBySerialNo";
        public const string USER_SetInitPrjPermission = "users/SetInitPrjPermission";

        public const string KEYSTORE_INFO = "storekeycode/finById";
        public const string KEYSTORE_KeyContainingUser = "storekeycode/GetKeyContainingUser";
        public const string KEYSTORE_KeyContainingUserBySerialNo = "storekeycode/GetKeyContainingUserBySerialNo";

        public const string SYSINFO_FUNCTION = "sysinfo/functions";

        public const string SYSINFO_COMMAND = "sysinfo/commands";

        public const string SYSINFO_FUNCTIONTYPE = "sysinfo/functiontypes";

        public const string SYSINFO_KEYSTORE = "sysinfo/keystore";

        public const string SYSINFO_PERMISSION = "sysinfo/permissions";

        public const string SYSINFO_USERAPPROVER = "sysinfo/userapprovers";
        
        public const string SYSINFO_USERS = "sysinfo/users";
        
        public const string SYSINFO_ADDUSERS = "sysinfo/adduser";
        public const string SYSINFO_DeleteUSERS = "sysinfo/DeleteUser";
        public const string SYSINFO_UpdateUSERS = "sysinfo/UpdateUser";

        public const string APPGROUP_GETALL = "appgroup/getall";

        public const string APPGROUP_DELETE = "appgroup/delete";

        public const string APPGROUP_UPDATE = "appgroup/update";

        public const string APPGROUP_GETDETAIL = "appgroup/getdetail";

        public const string THOI_TIET = "GetListWeather?code_province=";

        public const string CHECK_KEY_OLD = "customer-checkdate?key_code=";

        public const string REGISTER_KEY_OLD = "register-customer";

        public const string ACTIVE_KEY_OLD = "customer-active";

        public const string ADD_ORDER = "orderstore/registeronline";

        public const string ACTIVE_KEY = "orderstore/activatecodeonline";

        public const string LOGOUT_KEY = "storekeycode/removeonline";

        public const string LOGOUT_PLUG = "storekeycode/removeplugonline";

        public const string SYSINFO_TYPEACCOUNT = "sysinfo/typeaccounts";

        public const string SYSINFO_APPUSER = "sysinfo/users";

        public const string APPGROUP_ADD = "appgroup/add";

        /// <summary>
        /// Api APPROLE
        /// </summary>
        public const string RawSQL_CONTROLLER = "RawSQL";

        public const string APPROLE_CONTROLLER = "approle";
        public const string APPROLELEVEL_CONTROLLER = "approlelevel";
        public const string APPROLE_GetPermissionAndUserByRole = "approle/GetPermissionsAndUsersByRoleId";

        public const string PERMISSION_CONTROLLER = "permission";
        public const string ROLEDETAIL_CONTROLLER = "roledetail";

        public const string USERINROLE_CONTROLLER = "userinrole";
        //public const string USERINKey_CONTROLLER = "userinkey";


        public const string USERINCONTRACTOR_CONTROLLER = "userincontractor";
        public const string USERINPROJECT_CONTROLLER = "userinproject";
        public const string USERINPROJECT_getUserIdsInProject = "userinproject/getUserIdsInProject";

        public const string TongDuAn_CONTROLLER = "TongDuAn";
        public const string TongDuAn_GetAllPermissionByUserId = "TongDuAn/GetAllPermissionByUserId";
        public const string TongDuAn_GetAllDAByCurrentUser = "TongDuAn/GetAllDAByCurrentUser";
        public const string TongDuAn_SetBaseTimeProjects = "TongDuAn/SetBaseTimeProjects";
        public const string TongDuAn_GetUserWithRoleInGiaoViec = "TongDuAn/GetUserWithRoleInGiaoViec";
        public const string TongDuAn_GetAllGiaoViecWithRoles = "TongDuAn/GetAllGiaoViecWithRoles";
        public const string TongDuAn_UpdateDivision = "TongDuAn/UpdateDivision";
        public const string TongDuAn_GetCongTacTDKHWithKLHN = "TongDuAn/GetCongTacTDKHWithKLHN";
        public const string TongDuAn_GetBaoCaoNgay = "TongDuAn/GetKLNgay";
        public const string TongDuAn_UpdateOwnerDA = "TongDuAn/UpdateOwnerDA";
        public const string TongDuAn_UpdateCumDuAn = "TongDuAn/UpdateCumDuAn";
        public const string TongDuAn_GetMTCs = "TongDuAn/GetMTCs";
        public const string TongDuAn_GetNhanCongs = "TongDuAn/GetNhanCongs";
        public const string TongDuAn_GetAlertTimes = "TongDuAn/GetAlertTimes";
        public const string TongDuAn_SetAllAlertTimes = "TongDuAn/SetAllAlertTimes";


        public const string Approval_LoadProcess = "Approval/LoadProcess";
        public const string Approval_UpdateProcess = "Approval/UpdateProcess";

        public const string ApprovalGiaoViec_SendApprovalRequest = "ApprovalGiaoViec/SendApprovalRequest";
        public const string ApprovalGiaoViec_GetCongTacDoiDuyet = "ApprovalGiaoViec/GetCongTacDoiDuyet";
        public const string ApprovalGiaoViec_DuyetCongTac = "ApprovalGiaoViec/DuyetCongTac";
        public const string ApprovalGiaoViec_GetCongTacDaDuyet = "ApprovalGiaoViec/GetCongTacDaDuyet";
        
        public const string ApprovalYeuCauVatTu_SendApprovalRequest = "ApprovalYeuCauVatTu/SendApprovalRequest";
        public const string ApprovalYeuCauVatTu_GetCongTacDoiDuyet = "ApprovalYeuCauVatTu/GetCongTacDoiDuyet";
        public const string ApprovalYeuCauVatTu_DuyetCongTac = "ApprovalYeuCauVatTu/DuyetCongTac";
        public const string ApprovalYeuCauVatTu_GetCongTacDaDuyet = "ApprovalYeuCauVatTu/GetCongTacDaDuyet";
                
        public const string ApprovalNhapVatTu_SendApprovalRequest = "ApprovalNhapVatTu/SendApprovalRequest";
        public const string ApprovalNhapVatTu_GetCongTacDoiDuyet = "ApprovalNhapVatTu/GetCongTacDoiDuyet";
        public const string ApprovalNhapVatTu_DuyetCongTac = "ApprovalNhapVatTu/DuyetCongTac";
        public const string ApprovalNhapVatTu_GetCongTacDaDuyet = "ApprovalNhapVatTu/GetCongTacDaDuyet";
                
        public const string ApprovalXuatVatTu_SendApprovalRequest = "ApprovalXuatVatTu/SendApprovalRequest";
        public const string ApprovalXuatVatTu_GetCongTacDoiDuyet = "ApprovalXuatVatTu/GetCongTacDoiDuyet";
        public const string ApprovalXuatVatTu_DuyetCongTac = "ApprovalXuatVatTu/DuyetCongTac";
        public const string ApprovalXuatVatTu_GetCongTacDaDuyet = "ApprovalXuatVatTu/GetCongTacDaDuyet";
                        
        public const string ApprovalChuyenKho_SendApprovalRequest = "ApprovalChuyenKho/SendApprovalRequest";
        public const string ApprovalChuyenKho_GetCongTacDoiDuyet = "ApprovalChuyenKho/GetCongTacDoiDuyet";
        public const string ApprovalChuyenKho_DuyetCongTac = "ApprovalChuyenKho/DuyetCongTac";
        public const string ApprovalChuyenKho_GetCongTacDaDuyet = "ApprovalChuyenKho/GetCongTacDaDuyet";
        
        public const string ApprovalKhoanThu_SendApprovalRequest = "ApprovalKhoanThu/SendApprovalRequest";
        public const string ApprovalKhoanThu_GetCongTacDoiDuyet = "ApprovalKhoanThu/GetCongTacDoiDuyet";
        public const string ApprovalKhoanThu_DuyetCongTac = "ApprovalKhoanThu/DuyetCongTac";
        public const string ApprovalKhoanThu_GetCongTacDaDuyet = "ApprovalKhoanThu/GetCongTacDaDuyet";
                
        public const string ApprovalKhoanChi_SendApprovalRequest = "ApprovalKhoanChi/SendApprovalRequest";
        public const string ApprovalKhoanChi_GetCongTacDoiDuyet = "ApprovalKhoanChi/GetCongTacDoiDuyet";
        public const string ApprovalKhoanChi_DuyetCongTac = "ApprovalKhoanChi/DuyetCongTac";
        public const string ApprovalKhoanChi_GetCongTacDaDuyet = "ApprovalKhoanChi/GetCongTacDaDuyet";

        public const string CumDuAn_Create = "CumDuAn/Create";
        public const string CumDuAn_Update = "CumDuAn/Update";
        public const string CumDuAn_GetAll = "CumDuAn/GetAll";
        public const string CumDuAn_Delete = "CumDuAn/Delete";

        public const string KLHN_GetBrief = "KLHN/CalcKLHNBrief";


        public const string GiaoViec_GetByRequest = "GiaoViecExtension/GetByRequest";

        public const string Notification_CONTROLLER = "Notification";

        /// <summary>
        /// Api appuser
        /// </summary>
        public const string APPUER_GETALL = "users/allusers";
        /// <summary>
        /// Thong tin du an get all
        /// </summary>
        public const string THONGTINTONGHOPDUAN_GETALL = "ThongTinTongHopDuAn/AllTongHopDuAn";

        /// <summary>
        /// ChatBoards
        /// </summary>
        public const string GETALLGROUPS = "chatboards/allgroups";
        public const string GETHISTORY = "chatboards/get-history";

        /// <summary>
        /// Task
        /// </summary>
        public const string TASK_GetAll = "Task/getall";
        public const string TASK_GetFile = "Task/getfile";

        public const string CHAT_GROUPCHAT_ADDUPDATE = "chathub/addorupdate";
        public const string CHAT_GROUPCHAT_ALL = "chathub/groupchatbyuser";
        public const string CHAT_GIAOVIEC_ALL = "chathub/getallgiaoviec";
        //public const string CHAT_ROLEDETAIL_BYCODEGIAOVIEC = "chathub/gellallrolebycode";
        public const string CHAT_HISTORY = "chathub/history";
        //public const string CHAT_UOLOADFILE = "chathub/uploadfile"; 
        public const string CHAT_GIAOVIEC_UPDATEDATA = "chathub/updategiaoviec";
        public const string CHAT_GIAOVIEC_GETALLFILEBYCODE = "chathub/getallfilebycode";
        public const string CHAT_SAVEMESSAGE = "chathub/savemessage";
        /// <summary>
        /// Task
        /// </summary>
        public const string FILEMANAGE_GetFromRootFolder = "FileManage/GetFileFromRootContenFolder";
        public const string FILEMANAGE_GetFilesFromRootContenFolder = "FileManage/GetFilesFromRootContenFolder";
        public const string FILEMANAGE_PostFilesToServer = "FileManage/PostFilesToServer";


        public const string Initialize_UpdateVolumes = "Initialize/UpdateVolumes";
    }
}
