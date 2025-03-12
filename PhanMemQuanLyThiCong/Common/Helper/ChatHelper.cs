using DevExpress.DataAccess.Wizard.Presenters;
using DevExpress.Office.Utils;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.ChatBox;
using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Common.ViewModel;
using Microsoft.AspNetCore.SignalR.Client;
using PhanMemQuanLyThiCong.Common.Enums;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class ChatHelper
    {
        public static async Task<bool> AddOrUpdateGroupChat(GeneralGroupChatViewModel model)
        {
            var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.CHAT_GROUPCHAT_ADDUPDATE}", model);
            return res.MESSAGE_TYPECODE;
        }

        public static async Task<ResultMessage<List<GeneralGroupChatViewModel>>> GetAllGroupChatByUser(GiaoViecRequest model)
        {
            return await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<List<GeneralGroupChatViewModel>>($"{RouteAPI.CHAT_GROUPCHAT_ALL}", model);
        }

        public static async Task<ResultMessage<List<GiaoViecExtensionViewModel>>> GetAllGiaoViecByUser(GiaoViecRequest model)
        {
            return await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<List<GiaoViecExtensionViewModel>> ($"{RouteAPI.CHAT_GIAOVIEC_ALL}", model);
        }

        //public static async Task<ResultMessage<List<RoleDetailViewModel>>> GetAllGiaoViecWithRole()
        //{
        //    return await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<List<RoleDetailViewModel>>($"{RouteAPI.TongDuAn_GetAllGiaoViecWithRoles}");
        //}

        public static async Task<List<ManageMessageViewModel>> GetHistoryChatByGroup(GeneralGroupChatViewModel gr)
        {
            List<ManageMessageViewModel> response = new List<ManageMessageViewModel>();
            var data = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<GeneralGroupChatViewModel>($"{RouteAPI.CHAT_HISTORY}", gr);
            if (data.MESSAGE_TYPECODE)
            {
                ConnextService.groupIndex = data.Dto.Clone() as GeneralGroupChatViewModel;
                ConnextService.groupIndex.messages = null;

                foreach (var item in data.Dto.messages)
                {
                    switch (item.IsType)
                    {
                        case Enums.FileTypeEnum.FILE:
                            item.ImgText = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PhanMemQuanLyThiCong.Images.download_01.png"));
                            break;
                        case Enums.FileTypeEnum.IMAGE:
                            string urlimg = $"{BaseFrom.BanQuyenKeyInfo.BaseSeverUrl}{item.FilePath}";
                            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                            var content = await httpClient.GetAsync(urlimg);
                            if (content.IsSuccessStatusCode)
                            {
                                item.ImgText = Image.FromStream(await content.Content.ReadAsStreamAsync());
                            }
                            break;
                        default:
                            break;
                    }
                    if (!string.IsNullOrEmpty(item.AvatarMember))
                    {
                        string file = $@"{BaseFrom.BanQuyenKeyInfo.BaseSeverUrl}{item.AvatarMember}";
                        string fileName = Path.GetFileName(file);
                        HttpClient httpClient = new HttpClient();
                        var res = await httpClient.GetAsync(file);
                        if (res.IsSuccessStatusCode)
                        {
                            item.Logo = Image.FromStream(await res.Content.ReadAsStreamAsync());
                        }
                    }
                    else
                    {
                        item.Logo = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PhanMemQuanLyThiCong.Images.icon_user.png"));
                    }
                    if (!string.IsNullOrEmpty(item.Avatar))
                    {
                        string urlimg = $"{BaseFrom.BanQuyenKeyInfo.BaseSeverUrl}{item.Avatar}";
                        System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                        item.LogoTemp = Image.FromStream(await httpClient.GetStreamAsync(urlimg));
                    }
                    else
                    {
                        item.LogoTemp = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PhanMemQuanLyThiCong.Icon.icon_user.png"));
                    }
                    item.IsOwner = ConnextService.UserId != item.UserId ? false : true;
                    response.Add(item.Clone() as ManageMessageViewModel);
                }
            }
            return response.OrderBy(c => c.CreateDate).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileNames">List file upload</param>
        /// <param name="group">Group chat</param>
        /// <returns></returns>
        //public static async Task<bool> SaveMutiFileToSeverAsync(string[] fileNames, ManageMessageViewModel group)
        //{
        //    bool isUpload = false;
        //    try
        //    {
        //        foreach (var file in fileNames)
        //        {
        //            isUpload = await SaveFileToSeverAsync(file, group);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        isUpload = false;
        //    }
        //    return isUpload;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFilename">"c:\newProduct.jpg"</param>
        /// <returns></returns>
        //public static async Task<bool> SaveFileToSeverAsync(string localFilename, ManageMessageViewModel mess)
        //{
        //    bool isUpload = false;
        //    try
        //    {
        //        using (var fileStream = new FileStream(localFilename, FileMode.Open))
        //        {
        //            var fileInfo = new System.IO.FileInfo(localFilename);

        //            MultipartFormDataContent multiForm = new MultipartFormDataContent();
        //            string val = JsonConvert.SerializeObject(mess);
        //            multiForm.Add(new StringContent(val, Encoding.UTF8, "application/json"), "\"group\"");
        //            var content = new StreamContent(fileStream);
        //            var fileContent = new ByteArrayContent(await content.ReadAsByteArrayAsync());
        //            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
        //            multiForm.Add(fileContent, fileInfo.Name, fileInfo.Name);

        //            var uploadResult = await CusHttpClient.InstanceCustomer.PostAsync<bool>($@"{BaseFrom.BanQuyenKeyInfo.UrlAPI}{RouteAPI.CHAT_UOLOADFILE}", multiForm);
        //            fileStream.Dispose();
        //            return uploadResult.MESSAGE_TYPECODE;
        //        };
        //        //var fileStream = File.Open(localFilename, FileMode.Open);              
        //    }
        //    catch (Exception ex)
        //    {
        //        isUpload = false;
        //    }
        //    return isUpload;
        //}

        /// <summary>
        /// Update info giao việc
        /// </summary>
        /// <param name="viewModel">object</param>
        /// <returns></returns>
        public static async Task<ResultMessage<bool>> AddUpdateGiaoViec(GiaoViecExtensionViewModel viewModel)
        {
            return await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.CHAT_GIAOVIEC_UPDATEDATA}", viewModel);
        }

        public static async Task<List<GiaoViec_FileDinhKemExtensionViewModel>> GetAllFileByCode(string code, TypeCongTacEnum type) 
        {
            List<GiaoViec_FileDinhKemExtensionViewModel> response = new List<GiaoViec_FileDinhKemExtensionViewModel>();
            var data = await CusHttpClient.InstanceCustomer.MGetAsync<List<GiaoViec_FileDinhKemExtensionViewModel>>($"{RouteAPI.CHAT_GIAOVIEC_GETALLFILEBYCODE}/{(int)type}/{code}");
            if (data.MESSAGE_TYPECODE)
            {
                response = data.Dto;
            }
            return response;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public static async Task<bool> SaveMessage(ManageMessageViewModel mess)
        {
            var response = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($@"{BaseFrom.BanQuyenKeyInfo.UrlAPI}{RouteAPI.CHAT_SAVEMESSAGE}", mess);
            return response.MESSAGE_TYPECODE;
        }
    }
}