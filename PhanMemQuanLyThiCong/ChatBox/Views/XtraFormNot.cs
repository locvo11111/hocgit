using AutoMapper;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Mvvm.Native;
using DevExpress.XtraEditors;
//using Microsoft.Office.Interop.Word;
using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.ChatBox.ViewModels;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class XtraFormNot : DevExpress.XtraEditors.XtraForm
    {
        public string tenCongTac { get; set; }
        public string congTacId { get; set; }
        private ManageWorkViewModel managerWork = new ManageWorkViewModel();
        private List<ManageFileViewModel> listManageGroups = new List<ManageFileViewModel>();
        private IChatService _chatService = ConfigUnity.Container.Resolve<IChatService>();
        private List<ManageFileViewModel> lstFileOlds = new List<ManageFileViewModel>();
        List<string> lstFileUploads = new List<string>();
        public XtraFormNot()
        {
            InitializeComponent();
        }

        private async void btn_dieuchinh_Click(object sender, EventArgs e)
        {
            managerWork.ContentBrowsing = txt_noidungduyet.Text;
            if (lstFileUploads.Any())
            {
                await _chatService.SaveMutiFileToSeverAsync(managerWork.ConstructionId.ToString(), managerWork.Id.ToString(), lstFileUploads, ConnextService.UriChat);
                SaveData();
            }               
             else
                SaveData();
        }
        private async void SaveData()
        {
            var lstDeletes = lstFileOlds.Except(listManageGroups).ToList();
            foreach (var item in lstDeletes)
            {
                item.IsDeleted = true;
            }
            listManageGroups.AddRange(lstDeletes);
            managerWork.ManageFiles.Clear();
            managerWork.ManageFiles.AddRange(listManageGroups);
            // Save file to database ManageWork and ManageFiles sever
            var resultMessage = await _chatService.AddManageWork(managerWork, ConnextService.UriChat);
            if (resultMessage.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Cập nhật công tác thành công");
                this.Close();
            }
            else
            {
                MessageShower.ShowInformation("Cập nhật công tác không thành công");
            }
        }
        private async void XtraFormNot_Load(object sender, EventArgs e)
        {
            lb_tencongtacduyet.Text = tenCongTac;
            var resultMessage = await _chatService.GetDetailManagerWork(congTacId, ConnextService.UriChat);
            if (resultMessage.MESSAGE_TYPECODE)
            {
                managerWork = resultMessage.Dto;
                lstFileOlds.AddRange(managerWork.ManageFiles);
                listManageGroups.AddRange(managerWork.ManageFiles);
                LoadCheckBoxFile(listManageGroups);
                txt_noidungduyet.Text = managerWork.ContentBrowsing;
            }                         
        }

        private void LoadCheckBoxFile(List<ManageFileViewModel> manageFileViewModels)
        {
            check_listfileduyet.Items.Clear();
            check_listfileduyet.DataSource = manageFileViewModels;
            check_listfileduyet.DisplayMember = "Name";
            check_listfileduyet.ValueMember = "Id";
            check_listfileduyet.CheckMember = "Status";
        }

        private async void btn_themfileduyet_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!lstFileUploads.Exists(x => x == dlg.FileName))
                    {
                        lstFileUploads.Add(dlg.FileName);
                        string fileName = Path.GetFileName(dlg.FileName);
                        string path = $"FileChats/{managerWork.ConstructionId}/{managerWork.Id}/{fileName}";
                        if (!string.IsNullOrEmpty(path))
                        {
                            FileInfo fs = new FileInfo(dlg.FileName);
                            listManageGroups.Add(new ManageFileViewModel()
                            {
                                Id = Guid.NewGuid(),
                                BusinessId = managerWork.Id,
                                GroupChatId = ConnextService.groupIndex?.GroupChatId,
                                TaskId = ConnextService.groupIndex?.TaskId,
                                CongViecChaCode = ConnextService.groupIndex?.CongViecChaCode,
                                CongViecConCode = ConnextService.groupIndex?.CongViecConCode,
                                UserId = ConnextService.UserId,
                                FilePath = path,
                                Name = fileName,
                                FileSize = FileSizeFormatter.ConvertByteToMb(fs),
                                FileType = Path.GetFileName(dlg.FileName),
                            });
                        }
                        LoadCheckBoxFile(listManageGroups);
                    }
                    else
                        MessageShower.ShowInformation("File đã tồn tại trong danh sách");
                }
            }
        }

        private void btn_xoaFileDuyet_Click(object sender, EventArgs e)
        {
            var fileIndex = check_listfileduyet.SelectedItem as ManageFileViewModel;
            DialogResult dr = MessageShower.ShowYesNoQuestion($"Bạn có chắc chắn muốn xóa file ({fileIndex.Name}) này ?", "Quản lý thi công");
            if (dr == DialogResult.Yes)
            {
                listManageGroups.Remove(fileIndex);
                string file = lstFileUploads.Find(x => x.EndsWith(fileIndex.Name));
                if (!string.IsNullOrEmpty(file))
                {
                    lstFileUploads.Remove(file);
                }
                LoadCheckBoxFile(listManageGroups);
            }
        }

    }
}
