using DevExpress.DevAV.Chat;
using DevExpress.DevAV.Chat.Events;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.UnitConversion;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using Microsoft.AspNetCore.SignalR.Client;
using PhanMemQuanLyThiCong.ChatBox;
using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Function;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Unity;
using VChatCore.Dto;
using static DevExpress.Skins.SolidColorHelper;

namespace PhanMemQuanLyThiCong.ViewModels
{
    public class MessagesViewModel : ChannelViewModel
    {
        private HashSet<int> updatedMessagesIdices = new HashSet<int>();
        private List<ManageMessageViewModel> MessageDtos { get; set; }
        private int Index = 40025;
        public bool isTyping;
        public string client;
        private IChatService _chatService = ConfigUnity.Container.Resolve<IChatService>();
        private readonly string _uriChat = BaseFrom.BanQuyenKeyInfo.BaseSeverUrl;

        IDispatcherService dispatcher;
        public MessagesViewModel()
            : base()
        {
            dispatcher = this.GetRequiredService<IDispatcherService>();

            MessageDtos = new List<ManageMessageViewModel>();
            ConnextService.CheckConnext = false;
            Messenger.Default.Register<GeneralGroupChatViewModel>(this, OnContact);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Messenger.Default.Unregister<GeneralGroupChatViewModel>(this, OnContact);
        }

        protected override void OnConnected(IChannel channel)
        {
            base.OnConnected(channel);
            channel.Subscribe(OnContactEvents);
        }

        //public async void LoadContact()
        //{

        //}

        private async void OnContactEvents(Dictionary<long, ContactEvent> events)
        {
            if (Contact != null)
            {
                ContactEvent @event;
                if (events.Count > 0)
                    await dispatcher?.BeginInvoke(RaiseMessagesChanged);
            }
        }

        private async void OnMessageEvents(Dictionary<long, ManageMessageViewModel> events)
        {
            updatedMessagesIdices.Clear();
            ManageMessageViewModel @event = null; int index = 0;
            if (events.Count > 0)
                await dispatcher?.BeginInvoke(RaiseMessagesUpdated);
        }

        protected override async void OnChannelReady()
        {
            //await LoadMessages(Contact);
            await dispatcher?.BeginInvoke(UpdateUIOnChannelReady);
        }

        private void UpdateUIOnChannelReady()
        {
            this.RaisePropertyChanged(x => x.Messages);
            UpdateActions();
        }

        protected void UpdateActions()
        {
            this.RaiseCanExecuteChanged(x => x.SendMessage());
        }

        private void RaiseMessagesChanged()
        {
            this.RaisePropertyChanged(x => x.Messages);
        }

        private void RaiseContactChanged()
        {
            this.RaisePropertyChanged(x => x.Contact);
        }

        private void RaiseMessagesUpdated()
        {
            this.RaisePropertyChanged(x => x.UpdatedMessageIndices);
            updatedMessagesIdices.Clear();
        }

        private async void OnContact(GeneralGroupChatViewModel contact)
        {
            dispatcher?.Invoke(() => this.Contact = contact);
            MessageDtos = new List<ManageMessageViewModel>();
            await LoadMessages(contact);
        }

        protected void OnSelectedMessageChanged()
        {
            this.RaiseCanExecuteChanged(x => x.SaveFile());
        }

        private async Task LoadMessages(GeneralGroupChatViewModel contact)
        {
            if (contact != null)
            {
                if (MessageDtos.Count == 0)
                {
                    var data = await GetMessageDtosAsync(contact);
                    MessageDtos = data;
                }
                await dispatcher?.BeginInvoke(() => Messages = MessageDtos);
            }
        }

        public virtual GeneralGroupChatViewModel Contact
        {
            get;
            protected set;
        }

        public virtual List<ManageMessageViewModel> Messages
        {
            get;
            protected set;
        }

        public IReadOnlyCollection<int> UpdatedMessageIndices
        {
            get { return updatedMessagesIdices.ToArray(); }
        }

        public virtual ManageMessageViewModel SelectedMessage
        {
            get;
            set;
        }

        public virtual string MessageText
        {
            get;
            set;
        }

        public void Update()
        {
            this.RaiseCanExecuteChanged(x => x.SendMessage());
            this.RaiseCanExecuteChanged(x => x.ImgCall());
            this.RaiseCanExecuteChanged(x => x.FileCall());
            this.RaiseCanExecuteChanged(x => x.SendFile());
        }

        public void SendFile()
        {
        }

        public void SendMessage()
        {
            if(ConnextService.groupIndex ==null)
            {
                SharedControls.alertControl.Show("Vui lòng chọn 1 công tác tab Công tác phía tay trái.", SharedControls.Form);
                return;
            }    
            AddMessage(MessageText);           
        }

        public bool CanExecuteActions()
        {
            return Contact != null;
        }

        [Command(CanExecuteMethodName = nameof(CanExecuteActions))]
        public async void ShowGroup()
        {
        }

        public async void SaveFile()
        {
            var data = SelectedMessage;
            if (data.IsType == FileTypeEnum.MESSAGE) return;
            Image image = null;
            Stream res = null;
            string fileName = Path.GetFileName(data.FilePath);
            string urlFile = $"{ConnextService.UriChat}{data.FilePath}";
            HttpClient httpClient = new HttpClient();
            res = await httpClient.GetStreamAsync(urlFile);

            if (data.IsType == FileTypeEnum.IMAGE)
            {
                Bitmap bmp = new Bitmap(res);
                image = (Image)bmp;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (data.IsType == FileTypeEnum.IMAGE)
            {
                saveFileDialog.Title = "Lưu Ảnh";
                saveFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            }
            else
            {
                saveFileDialog.Title = "Lưu File";
                saveFileDialog.Filter = "Word Documents|*.doc;*.docx" +
                                        "|Excel Worksheets|*.xls;*.xlsx" +
                                        "|PowerPoint Presentations|*.ppt;*.pptx" +
                                        "|Office Files|*.doc;*.docx;*.xls;*.xlsx;*.ppt ;*.pptx" +
                                        "|Image Files|*.jpg;*.png" +
                                        "|Text Files|*.txt;" +
                                        "|Archives Files|*.zip;*.rar" +
                                        "|Pdf Files|*.pdf" +
                                        "|All Files|*.*";
            }
            saveFileDialog.FileName = data.Content;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string appPath = Path.GetDirectoryName(saveFileDialog.FileName);

                    if (Directory.Exists(appPath) == false)
                    {
                        Directory.CreateDirectory(appPath);
                    }
                    if (data.IsType == FileTypeEnum.IMAGE)
                    {
                        image.Save(saveFileDialog.FileName);
                    }
                    else
                    {
                        var fileStream = new FileStream(saveFileDialog.FileName, FileMode.CreateNew);
                        await res.CopyToAsync(fileStream);
                    }
                    //Thông báo đã tải xong//
                    MessageShower.ShowInformation("File đã được tải xuống", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageShower.ShowError($"Đã lỗi khi tải file: {ex.Message}", "Thông báo");
                    return;
                }
            }
        }

        private async Task<List<ManageMessageViewModel>> GetMessageDtosAsync(GeneralGroupChatViewModel contact)
        {
            return await ChatHelper.GetHistoryChatByGroup(contact);
        }

        /// <summary>
        /// Phương thức gửi ảnh
        /// </summary>
        public void ImgCall()
        {
            if (ConnextService.groupIndex == null)
            {
                MessageShower.ShowInformation("Vui lòng chọn 1 công tác bên phía tay trái tab Công tác trước khi upload tài liệu lên sever.");
                return;
            }
            string filePath = string.Empty;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var dlr = MessageShower.ShowYesNoQuestion("Bạn chắc chắn muốn gửi file này!", "Thông báo");
                    if (dlr == DialogResult.Yes)
                    {
                        SaveFileAndPushFile(dlg);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public void FileCall()
        {
            if (ConnextService.groupIndex == null)
            {
                MessageShower.ShowInformation("Vui lòng chọn 1 công tác bên phía tay trái tab Công tác trước khi upload tài liệu lên sever.");
                return;
            }
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open File";
                dlg.Filter = "Word Documents|*.doc;*.docx" +
                             "|Excel Worksheets|*.xls;*.xlsx" +
                             "|PowerPoint Presentations|*.ppt; *.pptx" +
                             "|Office Files|*.doc;*.docx;*.xls;*.xlsx;*.ppt ;*.pptx" +
                             "|Text Files|*.txt;" +
                            "|Pdf Files| *.pdf;" +
                             "|Archives Files|*.zip;*.rar";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var dlr = MessageShower.ShowYesNoQuestion("Bạn chắc chắn muốn gửi file này!", "Thông báo");
                    if (dlr == DialogResult.Yes)
                    {
                        SaveFileAndPushFile(dlg);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public async void AddMessage(string message)
        {
            ManageMessageViewModel messageDto = new ManageMessageViewModel()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Content = message,
                FilePath = null,
                FileType = "text",
                GroupChatId = ConnextService.groupIndex.GroupChatId,
                CongViecChaCode = ConnextService.groupIndex.CodeCongViecCha,
                CongViecConCode = ConnextService.groupIndex.CodeCongViecCon,
                TaskId = ConnextService.groupIndex.TaskId,
                UserId = ConnextService.UserId,
                Status = false,
                IsType = FileTypeEnum.MESSAGE,
                Size = 0,
                IsOwner = true,
                LogoTemp = Properties.Resources._007,
                Logo = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PhanMemQuanLyThiCong.Images.icon_user.png")),
                MemberName = BaseFrom.BanQuyenKeyInfo.FullName
            };
            
                
            var res = await ChatHelper.SaveMessage(messageDto);
            if(res)
            {
                MessageDtos.Add(messageDto);
                MessageText = null;
                MessageChange();
                //PushToGroup(messageDto.GroupChatId.ToString(), messageDto);
            }
        }

        private void MessageChange()
        {
            OnChannelReady();
        }

        #region Chat box

        //private async void PushToGroup(string groupchat, ManageMessageViewModel message)
        //{
        //    if (ConnextService._Connection != null && ConnextService._Connection.State == HubConnectionState.Connected)
        //        await ConnextService._Connection.InvokeAsync("PushToGroup", groupchat, message); 
        //}

        protected override void MethodFromServe()
        {
            if (ConnextService._Connection != null && ConnextService._Connection.State == HubConnectionState.Connected)
                ConnextService._Connection.On<ManageMessageViewModel>("BroadcastGroupMessage", (c) => BroadcastGroupMessage(c));
        }

        public void RegisterMessageEvent()
        {
            MethodFromServe();
        }

        private async void BroadcastGroupMessage(ManageMessageViewModel message)
        {
            //if (message.ConnectionId == ConnextService._Connection?.ConnectionId)
            //{
            //    return;
            //}

            if (message.IdStr == ConnextService.groupIndex?.IdStr)
            {
                if (!MessageDtos.Exists(x => x.Id == message.Id))
                {
                    switch (message.IsType)
                    {
                        case FileTypeEnum.MESSAGE:
                            break;
                        case FileTypeEnum.FILE:
                            message.ImgText = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PhanMemQuanLyThiCong.Images.download_01.png"));
                            break;
                        case FileTypeEnum.IMAGE:
                            string file = $@"{_uriChat}{message.FilePath}";
                            HttpClient httpClient = new HttpClient();
                            var res = await httpClient.GetAsync(file);
                            if(res.IsSuccessStatusCode)
                            {
                                message.ImgText = Image.FromStream(await res.Content.ReadAsStreamAsync());
                            }                         
                            break;
                        default:
                            break;
                    }
                    if (!string.IsNullOrEmpty(message.AvatarMember))
                    {
                        string file = $@"{_uriChat}{message.AvatarMember}";
                        string fileName = Path.GetFileName(file);
                        HttpClient httpClient = new HttpClient();
                        var res = await httpClient.GetAsync(file);
                        if (res.IsSuccessStatusCode)
                        {
                            message.Logo = Image.FromStream(await res.Content.ReadAsStreamAsync());
                        }
                    }
                    else
                    {
                        message.Logo = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PhanMemQuanLyThiCong.Images.icon_user.png"));
                    }
                    message.IsOwner = message.UserId.ToString() != ConnextService.UserId.ToString() ? false : true;
                    MessageDtos.Add(message);
                    MessageChange();
                }                                  
            }
            else
            {
                SharedControls.alertControl.Show($"{message.MemberName} vừa gửi tin nhắn trong nhóm chát {message.GroupName}", SharedControls.Form);
            }
        }

        private async void SaveFileAndPushFile(OpenFileDialog dlg)
        {
            Image image = null;
            try
            {
                using (var bmpTemp = new Bitmap(dlg.FileName))
                {
                    image = new Bitmap(bmpTemp);
                }
            }
            catch (Exception ex)
            {
                image = null;
            }

            string fileName = Path.GetFileName(dlg.FileName);
            //Lấy định dạng file//
            string ext = Path.GetExtension(dlg.FileName);

            Guid newId = Guid.NewGuid();
            ManageMessageViewModel messageDto = new ManageMessageViewModel()
            {
                Id = newId,
                GroupChatId = ConnextService.groupIndex.GroupChatId,
                TaskId = ConnextService.groupIndex.TaskId,
                CongViecChaCode = ConnextService.groupIndex.CodeCongViecCha,
                CongViecConCode = ConnextService.groupIndex.CodeCongViecCon,
                MemberName = BaseFrom.BanQuyenKeyInfo.FullName,
                Content = fileName,
                FilePath = $"FileChats/{ConnextService.groupIndex.IdStr}/{fileName}",
                FileType = ext,
                AvatarMember = ConnextService.Avatar,
                UserId = ConnextService.UserId,
                Status = true,
                IsType = image != null ? FileTypeEnum.IMAGE : FileTypeEnum.FILE,
                ImgText = image,
                FileName = fileName,
                FileContent = FileHelper.GetBytes(dlg.FileName)
            };

            if (messageDto.IsType == FileTypeEnum.FILE)
            {
                messageDto.ImgText = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PhanMemQuanLyThiCong.Images.download_01.png"));
            }
            messageDto.IsOwner = true;
            bool isUpload = await ChatHelper.SaveMessage(messageDto);
            if (isUpload)
            {
                MessageDtos.Add(messageDto);
                MessageChange();
                //PushToGroup(messageDto.GroupChatId.ToString(), messageDto);
            }
        }

        #endregion Chat box
    }
}