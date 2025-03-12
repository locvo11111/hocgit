using DevExpress.Office.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using Microsoft.AspNetCore.SignalR.Client;

using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace PhanMemQuanLyThiCong.ChatBox.Views
{
    public partial class BusinessView : DevExpress.XtraEditors.XtraForm
    {
        private IChatService _chatService = ConfigUnity.Container.Resolve<IChatService>();
        public List<FileCheckItem> ListFile = new List<FileCheckItem>();
        public List<string> fileNew = new List<string>();
        public List<string> memberNew = new List<string>();
        private FileCheckItem fileCheckItem = new FileCheckItem();
        public List<ManageGroupMenberViewModel> MemberList = new List<ManageGroupMenberViewModel>();
        public List<ManageTasksByMembersViewModel> ManageTasksByMembers;
        public ManageWorkFollowFile manageWorkFollow;
        public BusinessView()
        {
            InitializeComponent();
        }

        private async void BusinessView_Load(object sender, EventArgs e)
        {
            if (ConnextService.businessViewModel != null)
            {
                lb_TenCongTac.Text = ConnextService.businessViewModel.Name;
                lb_NguoiLapCongTac.Text = ConnextService._user;
                lb_NgayLapCongTac.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //lblTenCongTrinh.Text = ConnextService.groupIndex.GroupChatName;
                LoadCheckList(ListFile);

                check_listmember.Items.Clear();
                var data = await _chatService.GetAllMemberbyGroup(ConnextService.GroupId.ToString(), ConnextService.UserId.ToString(), ConnextService.UriChat);
                check_listmember.DataSource = data.Dto;
                check_listmember.DisplayMember= "MemberName";
                check_listmember.ValueMember = "MemberId";
            }
        }
        private void check_listfile_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            var data = check_listfile.SelectedItem as FileCheckItem;
            if (data != null)
            {
                fileCheckItem = data;

                if (check_listfile.GetItemChecked(check_listfile.SelectedIndex))
                {
                    ListFile.Where(x => x.Name == data.Name).FirstOrDefault().IsChecked = true;
                }
            }
        }

        private void check_listmember_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            var data  = check_listmember.SelectedItem as ManageGroupMenberViewModel;
            if (check_listmember.GetItemChecked(check_listmember.SelectedIndex))
            {
                MemberList.Add(data);
            }
            else
            {
                MemberList.Remove(data);
            }

        }
        private IEnumerable<ManageFileViewModel>  ConvertStringToFile(List<FileCheckItem> files)
        {
            List<ManageFileViewModel> manageFiles = new List<ManageFileViewModel>();
            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    //Lấy thông tin file dữ liệu 
                    //File sixze
                    decimal size = Math.Ceiling(FileSizeFormatter.FormatSizeMb(new FileInfo(file.Name).Length));
                    //Tên của  file
                    string fileName = Path.GetFileName(file.Name); ;
                    //Lấy định dạng file//
                    string ext = Path.GetExtension(file.Name);
                    ManageFileViewModel manageFileView = new ManageFileViewModel()
                    {
                        FilePath = $"FileMessage/{fileName}",
                        CreateDate = DateTime.Now,
                        FileSize = FileSizeFormatter.ConvertByteToMb(new FileInfo(file.Name)),
                        FileType = ext,
                        Name = fileName,
                        GroupChatId = ConnextService.groupIndex?.GroupChatId,
                        TaskId = ConnextService.groupIndex?.TaskId,
                        CongViecChaCode = ConnextService.groupIndex?.CodeCongViecCha,
                        CongViecConCode = ConnextService.groupIndex?.CodeCongViecCon,
                        UserId = ConnextService.UserId,
                        BusinessId = ConnextService.businessViewModel.Id,
                        IsDeleted = file.IsChecked,
                    };
                    manageFiles.Add(manageFileView);
                }
            }
            return manageFiles;
        }
        private ManageWorkFollowFile GetManageWorkViewModel()
        {
            ManageWorkFollowFile manageWork = new ManageWorkFollowFile();
            ///ManageWork
            manageWork.BusinessId = ConnextService.businessViewModel.Id;
            manageWork.ContentCreation = txt_ghichuduyet.Text;

            //manageWorkViewModel.roleManageWorkView = new RoleManageWorkViewModel()
            //{
            //    Id = Guid.NewGuid(),
            //    MemberChatId = ConnextService.UserId,
            //    Status = false
            //};
            manageWork.manageFiles = ConvertStringToFile(ListFile).ToList();
            manageWork.manageTasksByMembers = ConvertChatToManageTasks(MemberList).ToList();
            return manageWork;

        }
        private IEnumerable<ManageTasksByMembersViewModel> ConvertChatToManageTasks(List<ManageGroupMenberViewModel> memberChatViews)
        {
            List<ManageTasksByMembersViewModel> manageTasksByMembers = new List<ManageTasksByMembersViewModel>();
            if(memberChatViews.Count>0)
            {
                foreach (var item in memberChatViews)
                {
                    ManageTasksByMembersViewModel manageTasksBy = new ManageTasksByMembersViewModel()
                    {
                        BusinessId = ConnextService.businessViewModel.Id,
                        ConstructionId = ConnextService.ConstructionId,
                        UserId = item.UserId,
                        MemberRole = MemberRole.APPROVEDBY
                    };
                    manageTasksByMembers.Add(manageTasksBy);
                }
            }  
            return manageTasksByMembers;
        }

        private async void btn_giucongtacduyet_Click_1(object sender, EventArgs e)
        {
            manageWorkFollow = GetManageWorkViewModel();
            ManageTasksByMembers = new List<ManageTasksByMembersViewModel>();
            ManageTasksByMembers.AddRange(manageWorkFollow.manageTasksByMembers);
            ConnextService.files = ListFile.Select(x => x.Name).ToList();
            ConnextService.memberList = MemberList.Select(x => x.UserId.ToString()).ToList();
            //var data = await _chatService.SendFileApprove(manageWorkFollow, ConnextService.UriChat);
            //if (data.MESSAGE_TYPECODE)
            //{
            //    this.Close();
            //}
            //else
            //{
            //    MessageShower.ShowWarning("Có lỗi sảy ra khi gửi file!", "Thông báo!");
            //    return;
            //}
        }
        private void LoadCheckList(List<FileCheckItem> list)
        {
            check_listfile.Items.Clear();
            check_listfile.DataSource = list;
            check_listfile.DisplayMember = "Name";
        }
    }
}
