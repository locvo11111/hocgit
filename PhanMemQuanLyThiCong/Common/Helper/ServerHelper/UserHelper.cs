using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Namequyery;
using PhanMemQuanLyThiCong.Dto;
using RestSharp;
using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using VChatCore.Dto;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using DevExpress.XtraSpreadsheet.API.Native.Implementation;
using System.Web;

namespace PhanMemQuanLyThiCong.Common.Helper.SeverHelper
{
    public class UserHelper
    {
        

       

        public static async Task<ResultMessage<bool>> SyncUserFromTBTBySerialNo()
        {
            var res = await CusHttpClient.InstanceCustomer
                            .MPostAsJsonAsync<bool>($"{RouteAPI.USER_SyncUserBySerialNo}", CryptoHelper.Base64Encode(BaseFrom.BanQuyenKeyInfo.SerialNo));

            return res; 
        }

        public static async Task<List<AppUserViewModel>> GetAllUserInCusSever()
        {
            var res = await CusHttpClient.InstanceCustomer
                            .MGetAsync<List<AppUserViewModel>>($"{RouteAPI.USER_All}");

            if (!res.MESSAGE_TYPECODE)
                return null;

            return res.Dto;
        }

        public static async Task<List<AppUserViewModel>> GetAllUserInProject(string projectId)
        {
            var res = await CusHttpClient.InstanceCustomer
                    .MGetAsync<List<AppUserViewModel>>($"{RouteAPI.USERINPROJECT_getUserIdsInProject}/{projectId}");

            if (!res.MESSAGE_TYPECODE)
                return null;

            return res.Dto;
        }

        public static async Task<AppUserViewModel> GetCurrentUser()
        {
            WaitFormHelper.ShowWaitForm("Đang tải thông tin tài khoản");
            var res = await CusHttpClient.InstanceTBT
                            .MGetAsync<AppUserViewModel>($"{RouteAPI.USER_VALIDATETOKEN}");
            
            WaitFormHelper.CloseWaitForm();
            if (!res.MESSAGE_TYPECODE)
                return null;
            return res.Dto;
        }
    }
}
