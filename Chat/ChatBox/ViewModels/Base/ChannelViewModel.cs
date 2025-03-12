namespace PhanMemQuanLyThiCong.ViewModels
{
    using DevExpress.DevAV.Chat;
    using DevExpress.DevAV.Chat.Events;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.POCO;
    using Microsoft.AspNetCore.SignalR.Client;
    using PhanMemQuanLyThiCong.ChatBox;
    using PhanMemQuanLyThiCong.Common.Helper;
    using PhanMemQuanLyThiCong.Function;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using Unity;

    public abstract class ChannelViewModel
    {
        private IChatService _chatService = ConfigUnity.Container.Resolve<IChatService>();
        /// <summary>
        public static HubConnection _connection { get; set; }
        /// </summary>
        protected ChannelViewModel()
        {
            //ConnextHub();
            Messenger.Default.Register<IChannel>(this, OnConnected);
        }
        protected virtual void OnConnected(IChannel channel)
        {
            channel.Subscribe(OnChannelEvent);
        }
        public virtual void OnCreate()
        {
            EnsureDispatcherService();
            OnChannelReady();
            MethodFromServe();
        }

        public virtual void OnDestroy()
        {
            Messenger.Default.Unregister<IChannel>(this, OnConnected);
        }
        protected IChannel Channel
        {
            get;
            private set;
        }
        void OnChannelEvent(ChannelEvent @event)
        {
            var channelReady = @event as ChannelReadyEvent;
            if (channelReady != null)
            {
                EnsureDispatcherService();
                Channel = channelReady.Channel;
                OnChannelReady();
            }
        }
        protected virtual void OnChannelReady() { }
        protected virtual void MethodFromServe() { }
        protected IDispatcherService DispatcherService
        {
            get;
            private set;
        }
        protected IDispatcherService EnsureDispatcherService()
        {
            return DispatcherService ?? (DispatcherService = this.GetRequiredService<IDispatcherService>());
        }
        #region ChatBox
        /// <summary>
        /// ConnextHub
        /// </summary>
        /// <param name="api"></param>
        /// <param name="user"></param>
        public async void ConnextHub()
        {
            //await PermissionHelper.CheckKeyInfoAsync();
            //if (ConnextService._Connection is null && BaseFrom.IsFullAccess)
            //{
            //    ConnextService.UserId = BaseFrom.BanQuyenKeyInfo.UserId;
            //    ConnextService._Connection = await ConnextService.GetConnection();
            //    _connection = ConnextService._Connection;
            //    var channelContacts = await _chatService.GetGroupFollowImg(ConnextService.UserId.ToString(), ConnextService.UriChat);                
            //    if (channelContacts.Any())
            //    {
            //        ConnextService.ManageGroups = channelContacts.ToList();
            //        await ConnextService._Connection.InvokeAsync("AddUserToGroups", ConnextService.UserId.ToString(), channelContacts, false);
            //    }
            //}
            //_connection = ConnextService._Connection;
            if (_connection!=null)
                MethodFromServe();
        }
        #endregion
    }
}