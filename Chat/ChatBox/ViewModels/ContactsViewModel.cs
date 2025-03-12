namespace PhanMemQuanLyThiCong.ViewModels {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DevExpress.DevAV;
    using DevExpress.DevAV.Chat;
    using DevExpress.DevAV.Chat.Commands;
    using DevExpress.DevAV.Chat.Events;
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Filtering;
    using DevExpress.XtraReports.Design;
    using Microsoft.AspNetCore.SignalR.Client;
    
    using PhanMemQuanLyThiCong.ChatBox;
    using PhanMemQuanLyThiCong.ChatBox.Model;
    using PhanMemQuanLyThiCong.ChatBox.Views;
    using PhanMemQuanLyThiCong.Common.Enums;
    using PhanMemQuanLyThiCong.Model;
    using PhanMemQuanLyThiCong.Function;
    using PhanMemQuanLyThiCong.Model;
    using Unity;


    public class ContactsViewModel : ChannelViewModel
    {
        private readonly IChatService _chatService = ConfigUnity.Container.Resolve<IChatService>();

        public List<ManageGroupMenberViewModel> GroupDtos { get; set; }

        private List<ManageBusinessFileViewModel> listDataChuaDuyet = new List<ManageBusinessFileViewModel>();
        private List<ManageBusinessFileViewModel> listDataChoDuyet = new List<ManageBusinessFileViewModel>();
        private List<ManageBusinessFileViewModel> listDataDaDuyet = new List<ManageBusinessFileViewModel>();

        private AnnouncementViewModel announcementViewModel;

        public ContactsViewModel()
            : base()
        {
            
            Contacts = new GeneralGroupChatViewModel[0];
            ConnextService.CheckConnext = false;
            Messenger.Default.Register<GeneralGroupChatViewModel>(this, OnContact);
        }
        private void ConnextHub()
        {
            MethodFromServe();
        }
        protected override void OnConnected(IChannel channel)
        {
            base.OnConnected(channel);
            channel.Subscribe(OnContactEvents);
        }
        protected override async void OnChannelReady()
        {
            //var channelContacts = await GetManageGroupMenber();
            //var allContacts = channelContacts.ToList();
            await DispatcherService?.BeginInvoke(() => Contacts = ConnextService.ManageGroups);
        }

        public async void UpdateContacts(List<GeneralGroupChatViewModel> models)
        {
            await DispatcherService?.BeginInvoke(() => Contacts = models);
        }
        void OnContact(GeneralGroupChatViewModel contact)
        {
            UpdateSelectedContact(contact);
        }
        async void OnContactEvents(Dictionary<long, ContactEvent> events)
        {
            ContactEvent @event = null;
            if (events.Count > 0)
                await DispatcherService?.BeginInvoke(RaiseContactsChanged);
        }
        void RaiseContactsChanged()
        {
            this.RaisePropertyChanged(x => x.Contacts);
        }
        public virtual IReadOnlyCollection<GeneralGroupChatViewModel> Contacts
        {
            get;
            protected set;
        }
        protected void OnContactsChanged()
        {
            if (SelectedContact == null)
                SelectedContact = Contacts.FirstOrDefault();
            else UpdateSelectedContact(SelectedContact);
        }
        public virtual GeneralGroupChatViewModel SelectedContact
        {
            get;
            set;
        }

        protected void OnSelectedContactChanged()
        {
            NotifyContactSelected(SelectedContact);
            this.RaiseCanExecuteChanged(x => x.ClearConversation());
            this.RaiseCanExecuteChanged(x => x.CopyContact());
        }
        int lockContact = 0;
        async void UpdateSelectedContact(GeneralGroupChatViewModel contact)
        {
            if (lockContact > 0 || contact == null)
            {
                //// Kết nối user vào nhóm chat //
                ConnextService.GroupId = contact.IdStr;
                return;
            }
            lockContact++;
            try
            {
                if(Contacts.Any())
                {
                    string id = ConnextService.GroupId = contact.IdStr;
                    if (ConnextService.groupIndex !=null)
                        SelectedContact = ConnextService.groupIndex;
                    else SelectedContact = Contacts.First();
                }
                
            }
            finally { lockContact--; }
        }
        async void NotifyContactSelected(GeneralGroupChatViewModel contact)
        {
            if (lockContact > 0 || contact == null)
                return;
            lockContact++;
            try
            {
                if (contact != null)
                {
                    ConnextService.GroupId = contact.IdStr;
                    UpdateDataByContact();
                    Messenger.Default.Send(contact);
                }
            }
            finally { lockContact--; }
        }

        public async void UpdateDataByContact()
        {
            //var result = await _chatService.GetAllTaskCreateByUserId(ConnextService.UserId.ToString(), ConnextService.groupIndex.ConstructionId.ToString(), ConnextService.UriChat);
            //if (result.MESSAGE_TYPECODE)
            //{
            //    listDataChuaDuyet = result.Dto.FindAll(x => x.TypeApprove == TypeApprove.NOTYETAPPROVE);
            //    listDataDaDuyet = result.Dto.FindAll(x => x.TypeApprove == TypeApprove.APPROVE);
            //    ConnextService.tab_chuaduyet.Caption = listDataChuaDuyet.Count() > 0 ? $"Gửi Duyệt({listDataChuaDuyet.Count()})" : "Gửi Duyệt";
            //    ConnextService.tab_daduyet.Caption = listDataDaDuyet.Count() > 0 ? $"Đã Duyệt({listDataDaDuyet.Count()})" : "Đã Duyệt";
            //    _chatService.SetUpCheckList(ConnextService.check_listctchuaduyet, listDataChuaDuyet);
            //    _chatService.SetUpCheckList(ConnextService.check_listctdaduyet, listDataDaDuyet);
            //}
            //result = await _chatService.GetAllTaskApproveByUserId(ConnextService.UserId.ToString(), ConnextService.groupIndex.ConstructionId.ToString(), ConnextService.UriChat);
            //if (result.MESSAGE_TYPECODE)
            //{
            //    listDataChoDuyet = result.Dto;
            //    ConnextService.tabduyet.Caption = listDataChoDuyet.Count() > 0 ? $"Duyệt({listDataChoDuyet.Count()})" : "Duyệt";
            //    _chatService.SetUpCheckList(ConnextService.check_dscongtacduyet, listDataChoDuyet);
            //}
        }

        public async void ShowContact(Contact contact)
        {
            var contactInfo = await Channel.GetUserInfo(contact.ID);
            MessengerViewModel.ShowContactInfo(this, contactInfo);
        }
        [Command(false)]
        public bool HasContact()
        {
            return SelectedContact != null;
        }
        [Command(CanExecuteMethodName = nameof(HasContact))]
        public void ClearConversation()
        {
            var data = SelectedContact;
            //if (Channel != null) { };
            // Channel.Send(new ClearConversation(SelectedContact));
            

        }
        [Command(CanExecuteMethodName = nameof(HasContact))]
        public async void CopyContact()
        {
            //try
            //{
            //    var info = await Channel.GetUserInfo(SelectedContact.ID);
            //    string contact =
            //        info.Name + System.Environment.NewLine +
            //        $"Email: {info.Email}" + System.Environment.NewLine +
            //        $"Phone: {info.MobilePhone}";
            //    System.Windows.Forms.Clipboard.SetText(contact);
            //}
            //catch { }
        }
        public async void LoadGorup()
        {          
            if (ConnextService.groupIndex != null) SelectedContact = ConnextService.groupIndex;
            await DispatcherService?.BeginInvoke(() => Contacts = ConnextService.ManageGroups);
            await DispatcherService?.BeginInvoke(RaiseContactsChanged);
            OnSelectedContactChanged();
        }     

        ContactViewModel contactTooltipViewModel;
        public async Task<ContactViewModel> EnsureTooltipViewModel(Contact contact)
        {
            Employee employee = new Employee();
            var data = GetContactInfo(contact.ID);
            employee.Id = data.ID; employee.Photo = data.Avatar; employee.FullName = data.UserName;
            UserInfo contactInfo = new UserInfo(employee);

            if (contactTooltipViewModel == null)
                contactTooltipViewModel = ContactViewModel.Create(contactInfo);
            else
                ((ISupportParameter)contactTooltipViewModel).Parameter = contactInfo;
            return contactTooltipViewModel;
        }

        protected override void MethodFromServe()
        {
            LoadGorup();
        }

        //private void AddedUserToGroup(string a, string b)
        //{
        //    throw new NotImplementedException();
        //}
        private Contact GetContactInfo(long id)
        {
            return MesgersData.GetContactsAsync().Where(x => x.ID == id).FirstOrDefault();
        }
        private async Task<IEnumerable<GeneralGroupChatViewModel>> GetManageGroupMenber()
        {
            //var data = await _chatService.GetGroupFollowImg(ConnextService.UserId.ToString(),ConnextService.UriChat);
            //return data;
            return new List<GeneralGroupChatViewModel>();
        }
        public void LoadDataCheckListBox(string ContrationId)
        {

        }
        private List<ManageFileViewModel> ConvertName(List<ManageFileViewModel> fileViewModels)
        {
            /// Tổng số file trong công tác này //
            int totalFile = fileViewModels.Count;
            /// Tổng số file đã duyệt //
            int totalApprove = fileViewModels.Where(x => x.Status).Count();
            foreach (var item in fileViewModels)
            {
                item.Name = item.Name.Trim() + " - " + $@"{totalApprove}\{totalFile}";
            };
            return fileViewModels;
        }
    }
}

