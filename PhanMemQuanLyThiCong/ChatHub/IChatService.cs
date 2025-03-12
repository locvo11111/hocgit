using DevExpress.DevAV.Chat.Model;
using DevExpress.Mvvm.Native;
using DevExpress.XtraEditors;
using Microsoft.AspNetCore.SignalR.Client;
using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.ChatBox.ViewModels;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Urcs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Function
{
    public interface IChatService
    {
        Task<ResultMessage<AppUserViewModel>> GetUserCustomer(string keyword, string api);
        Task<ResultMessage<ConstructionGroupViewModel>> AddConstructionGroup(ConstructionGroupViewModel model, string api);
        string CretedFileCopy(string filePath);
        //Task<string> LoadFileFromServe(string filePath, string api);
        Task<string> SaveFileToSeverAsync(string fileName, ManageGroupMenberViewModel group, string api);
        Task<List<string>> SaveMutiFileToSeverAsync(string constructionId, string businessId, List<string> files, string api);
        Task<List<string>> SaveMutiFileToSeverAsync(List<string> files, string api);
        Task<ResultMessage<bool>> AddUserToGroup(MemberToGroupChatViewModel memberToGroup, string api);
        Task<ResultMessage<MemberChatViewModel>> AddMember(MemberChatViewModel memberToGroup, string api);
        Task<ResultMessage<MemberChatViewModel>> GetInfoMember(string keyword, string api);
        Task<ResultMessage<MemberChatViewModel>> GetInfoMemberFollowLogo(string keyword, string api);
        Task<ResultMessage<bool>> SendMessage(ManageMessageViewModel manageMessage, string api);
        Task<ResultMessage<IEnumerable<ManageMessageViewModel>>> GetHistory(string groupchat, string api);
        Task<ResultMessage<bool>> SendFile(ManageFileViewModel manageFileView, string api);

        Task<ResultMessage<IEnumerable<MergeResult>>> CreatedManageWork(List<ManageWorkViewModel> workViewModels, string api);
        Task<string> SaveAvatar(string fileName, string api);
        Task<IEnumerable<ManageGroupMenberViewModel>> GetGroupFollowImg(string user, string api);
        // Phần nhóm cửa sổ chát //
        Task<List<ManageMessageViewModel>> GetMessageFollowImg(List<ManageMessageViewModel> menberViewModels, string api);
        Task<ResultMessage<IEnumerable<ManageWorkViewModel>>> GetAllFileFollowGroup(string member, string bussid, string api);

        Task<ResultMessage<ConstructionViewModel>> GetInfoConstruction(string contructionId, string memberId, string api);
        Task<ResultMessage<IEnumerable<ManageBusinessFileViewModel>>> GetFileByMember(string memberId, string api);
        Task<ResultMessage<IEnumerable<ManageFileViewModel>>> GetfileByBusiness(string bussid, string api);
        Task<ResultMessage<List<ManageGroupMenberViewModel>>> GetAllMemberbyGroup(string groupChat, string userId, string api);
        Task<ResultMessage<bool>> ApproveWork(NoteApproveWorks noteApprove, string api);
        Task<ResultMessage<IEnumerable<ManageBusinessFileViewModel>>> GetBusiness(Guid memberId, Guid constructionId, TypeApprove typeApprove, string api);
        //Task GetViewBusinessFileWindows(string bussid, string api, ctrl_XemFileNhieuCuaSo ctrl_XemFileNhieuCua);
        Task<ResultMessage<bool>> UpdateFile(ApproveFileBusiness approveFile, string api);
        void SetUpCheckList(CheckedListBoxControl checkedListBox, List<ManageBusinessFileViewModel> manageBusinessFile);
        List<ManageWorkViewModel> ConvertCongTacToWork(List<CongTac> congTacList);

        Task<ResultMessage<IEnumerable<ManageWorkViewModel>>> GetBussinessByConstructionId(Guid constructionId, string api);
        Task<ResultMessage<bool>> UpdateNote(ManageWorkViewModel workViewModels, string api);
        Task ShowBusinessInfo(Guid businessId, LabelControl lbtencongtac, RichTextBox lbnoidungtraodoi, string api);

        Task<ResultMessage<bool>> SendFileApprove(ManageWorkFollowFile workFollowFile, string api);
        Task<ResultMessage<WorkFileMember>> GetFileApproveByBussiness(Guid bussinessId, string api);

        Task<ResultMessage<bool>> Update(IEnumerable<ManageFileViewModel> viewModel, string api);
        Task<ResultMessage<bool>> Update(ManageWorkViewModel viewModel, string api);

        Task<ResultMessage<IEnumerable<MergeResult>>> AddUpdateManagerFiles(List<ManageFileViewModel> viewModels, string api);

        Task<ResultMessage<MergeResult>> AddManageWork(ManageWorkViewModel viewModels, string api);

        Task<ResultMessage<List<ManageBusinessFileViewModel>>> GetAllTaskCreateByUserId(string userId, string constructionId, string api);
        Task<ResultMessage<List<ManageBusinessFileViewModel>>> GetAllTaskApproveByUserId(string userId, string constructionId, string api);
        Task<ResultMessage<ManageWorkViewModel>> GetDetailManagerWork(string id, string api);
    }
}
