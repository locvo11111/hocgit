using Newtonsoft.Json;
//using PM360.Common.API;
//using PM360.DAO.Models;
//using PM360.DAO.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PM360.Common.Helper;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using Microsoft.AspNetCore.SignalR.Client;
//using PM360.Common.Message;
using DevExpress.DevAV.Chat.Model;
using System.Drawing;
using DevExpress.Office.Utils;
using PhanMemQuanLyThiCong.ChatBox.Model;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraRichEdit.Model;
using PhanMemQuanLyThiCong.ChatBox;
using System.Windows.Input;
using System.Security.Policy;
using PhanMemQuanLyThiCong.Common.Enums;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.ChatBox.ViewModels;
using System.Windows.Forms;
//using PM360.DAO.Enums;
using PM360.Common;
using PhanMemQuanLyThiCong.Common.Constant;
using DevExpress.XtraSpreadsheet.Commands;
using PhanMemQuanLyThiCong.Urcs;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.ViewModels;
using PhanMemQuanLyThiCong.Common.API;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;

namespace PhanMemQuanLyThiCong.Function
{
    public class ChatService : IChatService
    {
        //Duong dan file cua app//
        private string projectDirectory = $@"{BaseFrom.m_tempPath}\File\";
        /// <summary>
        /// Thêm công trình và nhóm chat và tạo người lập nhóm chat ban đầu
        /// </summary>
        /// <param name="model"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        public async Task<ResultMessage<ConstructionGroupViewModel>> AddConstructionGroup(ConstructionGroupViewModel model, string api)
        {
            var data = await UtilAPI<ConstructionGroupViewModel>.
                Post<ConstructionGroupViewModel>(model, $"{api}api/chatbot/addconstructionandgroupandmember");
            return data;
        }
        /// <summary>
        /// Lưu người mới thêm vào nhóm chat 
        /// </summary>
        /// <param name="memberToGroup"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        public async Task<ResultMessage<bool>> AddUserToGroup(MemberToGroupChatViewModel memberToGroup, string api)
        {
           
            var data = await UtilAPI<bool>.Post(memberToGroup, $@"{api}api/chatbot/addmembertogroup");
            return data;
        }

        /// <summary>
        /// Tạo file copy
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string CretedFileCopy(string filePath)
        {
            //Lấy các thông số của file đã gửi
            //File sixze
            decimal size = Math.Ceiling(FileSizeFormatter.FormatSizeMb(new FileInfo(filePath).Length));
            //Tên của  file
            string fileName = filePath;
            //Lấy định dạng file//
            string ext = Path.GetExtension(filePath);
            //Copy file 
            //Tao duong dan file sao luu//
            string fileCopy = projectDirectory + Path.GetFileName(fileName);
            if (System.IO.Directory.Exists(fileCopy))
            {
                File.Delete(fileCopy);
            }
            File.Copy(fileName, fileCopy);
            return fileCopy;
        }
        /// <summary>
        /// Lấy lịch sử chat
        /// </summary>
        /// <param name="groupchat"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        public async Task<ResultMessage<IEnumerable<ManageMessageViewModel>>> GetHistory(string groupchat, string api)
        {
            string url = $"{api}api/managemessage/gethitory?GroupChatId={groupchat}";
            var data = await UtilAPI<IEnumerable<ManageMessageViewModel>>.Get(url);
            return data;
        }
        #region lấy thông tin file từ serve
        //public async Task<string> LoadFileFromServe(string filePath, string api)
        //{
        //string file = $@"{api}{filePath}";

        //string fileName = Path.GetFileName(file);

        //HttpClient httpClient = new HttpClient();
        //var res = await httpClient.GetStreamAsync(file);

        //string filesave = projectDirectory + $@"\Shared\word\{fileName}";
        //if (!System.IO.Directory.Exists(filesave))
        //{
        //    File.Delete(filesave);
        //}
        ////Save file tạm vào trong thư mục
        //using (var fs = new FileStream(filesave, FileMode.CreateNew))
        //{
        //    await res.CopyToAsync(fs);
        //}
        //return filesave;
        //}
        #endregion
        /// <summary>
        /// Lưu file lên trên sever 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> SaveFileToSeverAsync(string fileName, ManageGroupMenberViewModel group, string api)
        {
            string fileold = fileName;
            MultipartFormDataContent multiForm;

            multiForm = new MultipartFormDataContent()
            {
                    { new StringContent(Path.Combine(group.ConstructionId.ToString(), group.GroupChatId.ToString()),
                     Encoding.UTF8, "application/json"),"Data"
                    }
            };
            //FileStream fs = File.OpenRead(fileName);
            //string Content = Path.GetFileName(fileName);
            //var content = new StreamContent(fs);
            var fileContent = await FileHelper.ToByteArrayContent(fileName);// new ByteArrayContent(await content.ReadAsByteArrayAsync());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            multiForm.Add(fileContent, Path.GetFileName(fileName), Path.GetFileName(fileName));

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            var res = await client.PostAsync($@"{api}api/managemessage/addfile", multiForm);
            if(res.IsSuccessStatusCode)
            {
                return $"FileChats/{group.ConstructionId}/{group.GroupChatId}/{fileContent}";
            }
            return string.Empty;
        }
        /// <summary>
        /// Lưu nội dung tin nhắn.
        /// </summary>
        /// <param name="manageMessage"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        public async Task<ResultMessage<bool>> SendMessage(ManageMessageViewModel manageMessage, string api)
        {
            var data = await UtilAPI<bool>.Post(manageMessage, $@"{api}api/managemessage/savemessage");
            return data;
        }

        public async Task<ResultMessage<MemberChatViewModel>> AddMember(MemberChatViewModel memberToGroup, string api)
        {
            var data = await UtilAPI<MemberChatViewModel>.Post(memberToGroup, $@"{api}api/chatbot/addmember");
            return data;
        }
        public async Task<List<ManageMessageViewModel>> GetMessageFollowImg(List<ManageMessageViewModel> menberViewModels, string api)
        {
            List<ManageMessageViewModel> manageGroups = new List<ManageMessageViewModel>();

            //foreach (var item in menberViewModels)
            //{
            //    if (item.IsImg)
            //    {
            //        string urlimg = $"{api}{item.FilePath}";
            //        System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            //        var response = await httpClient.GetAsync(urlimg);
            //        if(response.IsSuccessStatusCode)
            //        {
            //            item.ImgText = Image.FromStream(await response.Content.ReadAsStreamAsync());
            //        }    
                    
            //    } 
            //    else if (item.IsFile)
            //    {
            //        item.ImgText = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PhanMemQuanLyThiCong.Images.download_01.png"));
            //    }    
            //    if (!string.IsNullOrEmpty(item.Avatar))
            //    {
            //        string urlimg = $"{api}{item.Avatar}";
            //        System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            //        item.LogoTemp = Image.FromStream(await httpClient.GetStreamAsync(urlimg));
            //    }
            //    else
            //    {
            //        item.LogoTemp = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PhanMemQuanLyThiCong.Images.icon_user.jpg"));
            //    }
            //    item.IsOwner = ConnextService._user != item.MemberName ? false : true;
            //}
            //manageGroups.AddRange(menberViewModels);
            return manageGroups;
        }

        public async Task<ResultMessage<MemberChatViewModel>> GetInfoMember(string keyword, string api)
        {
            string url = $"{api}api/chatbot/getinfouser?keyword={keyword}";
            var data = await UtilAPI<MemberChatViewModel>.Get(url);
            return data;

        }

        public async Task<ResultMessage<bool>> SendFile(ManageFileViewModel manageFileView, string api)
        {
            var data = await UtilAPI<bool>.Post(manageFileView, $@"{api}api/managemessage/savefile");
            return data;
        }
        public async Task<string> SaveAvatar(string fileName, string api)
        {
            try
            {
                string fileold = fileName;
                MultipartFormDataContent multiForm;
                multiForm = new MultipartFormDataContent()
            {
                    { new StringContent(JsonConvert.SerializeObject("File"),
                     Encoding.UTF8, "application/json"),"Data"
                    }
            };
                //FileStream fs = File.OpenRead(fileName);
                //string Content = Path.GetFileName(fileName);
                //var content = new StreamContent(fs);
                var fileContent = await FileHelper.ToByteArrayContent(fileName);//new ByteArrayContent(await content.ReadAsByteArrayAsync());
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                multiForm.Add(fileContent, Path.GetFileName(fileName), Path.GetFileName(fileName));

                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                var res = await client.PostAsync($@"{api}api/managemessage/addavartar", multiForm);

                //fileName = $"FileChats/Avatar/{Content}";
                return fileName;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }
        public async Task<IEnumerable<ManageGroupMenberViewModel>> GetGroupFollowImg(string user, string api)
        {
            List<ManageGroupMenberViewModel> manageGroups = new List<ManageGroupMenberViewModel>();

            string url = $"{api}api/chatbot/groupchatbyuser?userid={user}";
            var data = await UtilAPI<List<ManageGroupMenberViewModel>>.Get(url);

            if (data.MESSAGE_TYPECODE)
            {
                foreach (var item in data.Dto)
                {
                    if (String.IsNullOrEmpty(item.Avatar))
                    {
                        try
                        {
                            string urlimg = $"{api}{item.Avatar}";
                            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                            item.Avartar = (Image)(new Bitmap(await httpClient.GetStreamAsync(urlimg)));
                        }
                        catch (Exception ex)
                        {
                            item.Avartar = (Image)Properties.Resources.QLTC;
                        }
                    }
                }
                manageGroups.AddRange(data.Dto);
                return manageGroups;
            }
            else
                return manageGroups;   
        }

        public async Task<ResultMessage<IEnumerable<MergeResult>>> CreatedManageWork(List<ManageWorkViewModel> workViewModels, string api)
        {
            var data = await UtilAPI<IEnumerable<MergeResult>>.Post(workViewModels, $@"{api}api/managemessage/addmanagework");
            return data;
        }

        public async Task<ResultMessage<IEnumerable<ManageWorkViewModel>>> GetAllFileFollowGroup(string member, string bussid, string api)
        {
            string url = $"{api}api/managemessage/getfilebygroupchat?memberId={member}&businessId={bussid}";
            var data = await UtilAPI<IEnumerable<ManageWorkViewModel>>.Get(url);
            return data;
        }

        public async Task<ResultMessage<MemberChatViewModel>> GetInfoMemberFollowLogo(string keyword, string api)
        {
            string url = $"{api}api/chatbot/getinfouser?keyword={keyword}";
            var data = await UtilAPI<MemberChatViewModel>.Get(url);

            //Tải Avartar người dùng //
            if (data.Dto != null)
            {
                if (!string.IsNullOrEmpty(data.Dto.Avatar))
                {
                    string urlimg = $"{api}{data.Dto.Avatar}";
                    System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                    data.Dto.Logo = (Image)(new Bitmap(await httpClient.GetStreamAsync(urlimg)));
                    data.Dto.AvatarOld = data.Dto.Avatar;
                }
            }
            return data;
        }

        public async Task<ResultMessage<ConstructionViewModel>> GetInfoConstruction(string contructionId, string memberId, string api)
        {
            string url = $"{api}api/chatbot/getinfoconstruction?constructionId={contructionId}&memberId={memberId}";
            var data = await UtilAPI<ConstructionViewModel>.Get(url);
            return data;
        }

        public async Task<ResultMessage<IEnumerable<ManageBusinessFileViewModel>>> GetFileByMember(string memberId, string api)
        {
            string url = $"{api}api/managemessage/getfilebymember?memberId={memberId}";
            var data = await UtilAPI<IEnumerable<ManageBusinessFileViewModel>>.Get(url);
            return data;
        }

        public async Task<ResultMessage<IEnumerable<ManageFileViewModel>>> GetfileByBusiness(string bussid, string api)
        {
            string url = $"{api}api/managemessage/getfilebybusiness?businessId={bussid}";
            var data = await UtilAPI<IEnumerable<ManageFileViewModel>>.Get(url);
            return data;
        }

        //public async Task GetViewBusinessFileWindows(string bussid, string api, ctrl_XemFileNhieuCuaSo ctrl_XemFileNhieuCua)
        //{
        //    try
        //    {
        //        string url = $"{api}api/managemessage/getfilebybusiness?businessId={bussid}";
        //        var data = await UtilAPI<IEnumerable<ManageFileViewModel>>.Get(url);
        //        List<string> listFileName = data.Dto.Select(x => x.FilePath).ToList();
        //        ctrl_XemFileNhieuCua.setFiles(listFileName.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        public async Task<ResultMessage<List<ManageGroupMenberViewModel>>> GetAllMemberbyGroup(string groupChat, string userId, string api)
        {
            string url = $"{api}api/chatbot/getallmemberbygroup?groupchat={groupChat}&member={userId}";
            var data = await UtilAPI<List<ManageGroupMenberViewModel>>.Get(url);
            return data;
        }

        public async Task<ResultMessage<bool>> ApproveWork(NoteApproveWorks noteApprove, string api)
        {
            var data = await UtilAPI<bool>.Post(noteApprove, $@"{api}api/managemessage/approvework");
            return data;
        }

        public async Task<ResultMessage<IEnumerable<ManageBusinessFileViewModel>>> GetBusiness(Guid memberId, Guid constructionId, TypeApprove typeApprove, string api)
        {
            string url = $"{api}api/managemessage/getbussiness?memberId={memberId}&constructionId={constructionId}&typeApprove={typeApprove}";
            var data = await UtilAPI<IEnumerable<ManageBusinessFileViewModel>>.Get(url);
            return data;
        }

        public void SetUpCheckList(CheckedListBoxControl checkedListBox, List<ManageBusinessFileViewModel> manageBusinessFile)
        {
            checkedListBox.Items.Clear();
            checkedListBox.DataSource = ChangNameManageBusinessFile(manageBusinessFile);
            checkedListBox.DisplayMember = "NameBusiness";
            checkedListBox.ValueMember = "BussinessId";
            checkedListBox.Refresh();
        }

        public async Task<ResultMessage<bool>> UpdateFile(ApproveFileBusiness approveFile, string api)
        {
            var data = await UtilAPI<bool>.Post(approveFile, $@"{api}api/managemessage/approvefilebusinesswork");
            return data;
        }
        public List<ManageWorkViewModel> ConvertCongTacToWork(List<CongTac> congTacList)
        {
            List<ManageWorkViewModel> manageWorkViewModels = new List<ManageWorkViewModel>();
            if (congTacList.Count > 0)
            {
                foreach (var item in congTacList)
                {
                    ManageWorkViewModel workViewModel = new ManageWorkViewModel()
                    {
                        Id = Guid.Parse(item.Code),
                        ApproveDate = null,
                        ConstructionId = ConnextService.ConstructionId,
                        ContentBrowsing = null,
                        ContentCreation = null,
                        CreateDate = DateTime.Now,
                        EndDay = item.NKTThiCong,
                        GroupChatId = ConnextService.groupIndex?.GroupChatId,
                        TaskId = ConnextService.groupIndex?.TaskId,
                        CongViecChaCode = ConnextService.groupIndex?.CodeCongViecCha,
                        CongViecConCode = ConnextService.groupIndex?.CodeCongViecCon,
                        Unit = item.DonVi,
                        UserId = ConnextService.UserId,
                        Name = item.TenCongTac,
                        NameBusiness = item.TenCongTac,
                        StartDay = item.NBDThiCong,
                        Status = false,
                        Stt = item.STT,
                        TypeApprove = TypeApprove.NOTYETAPPROVE,
                        WorkType = WorkType.DONOT,
                        KhoiLuongHD = item.KhoiLuongHD,
                        KhoiLuongTT = item.KhoiLuongTT,
                        TenHangMuc = item.TenHangMuc
                    };
                    if (workViewModel.KhoiLuongHD.HasValue && workViewModel.KhoiLuongHD.Value > 0 && workViewModel.KhoiLuongTT.HasValue && workViewModel.KhoiLuongTT.Value > 0)
                        workViewModel.TyLeHoanThanh = Math.Round((workViewModel.KhoiLuongTT.Value / workViewModel.KhoiLuongHD.Value) * 100, 2);
                    manageWorkViewModels.Add(workViewModel);
                }
            }
            return manageWorkViewModels;
        }

        public async Task<ResultMessage<IEnumerable<ManageWorkViewModel>>> GetBussinessByConstructionId(Guid constructionId, string api)
        {
            string url = $"{api}api/managemessage/getbussinessbyconstructionid?constructionId={constructionId}";
            var data = await UtilAPI<IEnumerable<ManageWorkViewModel>>.Get(url);
            return data;
        }

        public async Task<ResultMessage<bool>> UpdateNote(ManageWorkViewModel workViewModels, string api)
        {
            var data = await UtilAPI<bool>.Post(workViewModels, $@"{api}api/managemessage/updatenote");
            return data;
        }

        public async Task ShowBusinessInfo(Guid businessId, LabelControl lbNoiDungTraoDoi,
            RichTextBox lbTenCongTac, string api)
        {
            string url = $"{api}api/managemessage/getinfobybusinessid?businessId={businessId}";
            var data = await UtilAPI<ManageWorkViewModel>.Get(url);
            //lbNoiDungTraoDoi.Text = data.Dto.NameBusiness;
            lbTenCongTac.Text = data.Dto.Note;
        }

        public async Task<ResultMessage<bool>> SendFileApprove(ManageWorkFollowFile workFollowFile, string api)
        {
            var data = await UtilAPI<bool>.Post(workFollowFile, $@"{api}api/managemessage/sendfileapprove");
            return data;
        }

        public async Task<ResultMessage<WorkFileMember>> GetFileApproveByBussiness(Guid bussinessId, string api)
        {
            string url = $"{api}api/managemessage/getfileapprovebybussiness?businessId={bussinessId}";
            var data = await UtilAPI<WorkFileMember>.Get(url);
            return data;
        }

        public async Task<ResultMessage<AppUserViewModel>> GetUserCustomer(string keyword, string api)
        {
            string url = $"{api}api/users/accountchat?search={keyword}";
            var data = await UtilAPI<AppUserViewModel>.Get(url);
            return data;
        }

        public async Task<ResultMessage<bool>> Update(IEnumerable<ManageFileViewModel> viewModel, string api)
        {
            var data = await UtilAPI<bool>.Post(viewModel, $@"{api}api/managemessage/updatefileapprove");
            return data;
        }

        public async Task<ResultMessage<bool>> Update(ManageWorkViewModel viewModel, string api)
        {
            var data = await UtilAPI<bool>.Post(viewModel, $@"{api}api/managemessage/updatework");
            return data;
        }

        public async Task<List<string>> SaveMutiFileToSeverAsync(List<string> files, string api)
        {
            List<string> fileNames = new List<string>();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            MultipartFormDataContent multiForm;
            multiForm = new MultipartFormDataContent()
            {
                { new StringContent(Path.Combine(ConnextService.ConstructionId.ToString(), ConnextService.businessViewModel.Id.ToString()),
                    Encoding.UTF8, "application/json"),"Data"
                }
            };
            foreach (string path in files)
            {

                //FileStream fs = File.OpenRead(path);
                string Content = Path.GetFileName(path);
                //var streamContent = new StreamContent(fs);

                //string dd = MimeType(path);
                var fileContent = await FileHelper.ToByteArrayContent(path);// new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                multiForm.Add(fileContent, Content, Content);

                fileNames.Add($"FileChats/{ConnextService.ConstructionId}/{ConnextService.businessViewModel.Id}/{Content}");

                // Save file //
                //Tao duong dan file sao luu//
                if (!Directory.Exists(projectDirectory))
                {
                    Directory.CreateDirectory(projectDirectory);
                }
                string fileCopy = projectDirectory + Path.GetFileName(path);
                if (File.Exists(fileCopy))
                {
                    File.Delete(fileCopy);
                }
                File.Copy(path, fileCopy);

            }
            try
            {
                using (var response = await client.PostAsync($@"{api}api/managemessage/addfile", multiForm))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return fileNames;
                    }
                    else return new List<string>();
                }
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
            
        }
        private List<ManageBusinessFileViewModel> ChangNameManageBusinessFile(List<ManageBusinessFileViewModel> manageBusinessFileViews)
        {
            List<ManageBusinessFileViewModel> businessFileViewModels = new List<ManageBusinessFileViewModel>();
            foreach (var item in manageBusinessFileViews)
            {
                item.NameBusiness = item.NameBusiness + $" -- ({item.FileApprove}/{item.FileTotal})";
            }
            return manageBusinessFileViews;
        }

        public async Task<ResultMessage<IEnumerable<MergeResult>>> AddUpdateManagerFiles(List<ManageFileViewModel> viewModels, string api)
        {
            return await UtilAPI<IEnumerable<MergeResult>>.Post(viewModels, $@"{api}api/managerfile/add");
        }

        public async Task<ResultMessage<MergeResult>> AddManageWork(ManageWorkViewModel viewModels, string api)
        {
            return await UtilAPI<MergeResult>.Post(viewModels, $@"{api}api/managerwork/add");
        }

        /// <summary>
        /// SaveMutiFileToSeverAsync
        /// </summary>
        /// <param name="constructionId"></param>
        /// <param name="businessId"></param>
        /// <param name="files"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        public async Task<List<string>> SaveMutiFileToSeverAsync(string constructionId, string businessId, List<string> files, string api)
        {
            List<string> fileNames = new List<string>();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            MultipartFormDataContent multiForm;
            multiForm = new MultipartFormDataContent()
            {
                { new StringContent(Path.Combine(constructionId, businessId),
                    Encoding.UTF8, "application/json"),"Data"
                }
            };
            foreach (string path in files)
            {

                //FileStream fs = File.OpenRead(path);
                string fileName = Path.GetFileName(path);
                //var streamContent = new StreamContent(fs);

                //string dd = MimeType(path);
                var fileContent = await FileHelper.ToByteArrayContent(path);// new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                multiForm.Add(fileContent, fileName, fileName);

                fileNames.Add($"FileChats/{constructionId}/{businessId}/{fileName}");

                //// Save file //
                ////Tao duong dan file sao luu//
                //if (!Directory.Exists(projectDirectory))
                //{
                //    Directory.CreateDirectory(projectDirectory);
                //}
                //string fileCopy = projectDirectory + Path.GetFileName(path);
                //if (File.Exists(fileCopy))
                //{
                //    File.Delete(fileCopy);
                //}
                //File.Copy(path, fileCopy);

            }
            try
            {
                using (var response = await client.PostAsync($@"{api}api/managemessage/addfile", multiForm))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return fileNames;
                    }
                    else return new List<string>();
                }
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public async Task<ResultMessage<List<ManageBusinessFileViewModel>>> GetAllTaskCreateByUserId(string userId, string constructionId, string api)
        {
            string url = $"{api}api/managerwork/getAllTaskCreate?userId={userId}&constructionId={constructionId}";
            return await UtilAPI<List<ManageBusinessFileViewModel>>.Get(url);
        }

        public async Task<ResultMessage<List<ManageBusinessFileViewModel>>> GetAllTaskApproveByUserId(string userId, string constructionId, string api)
        {
            string url = $"{api}api/managerwork/getAllTaskApprove?userId={userId}&constructionId={constructionId}";
            return await UtilAPI<List<ManageBusinessFileViewModel>>.Get(url);
        }

        public async Task<ResultMessage<ManageWorkViewModel>> GetDetailManagerWork(string id, string api)
        {
            string url = $"{api}api/managerwork/getDetailManageWork?id={id}";
            return await UtilAPI<ManageWorkViewModel>.Get(url);
        }
    }
}
