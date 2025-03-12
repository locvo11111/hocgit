using DevExpress.XtraEditors;
using Microsoft.AspNetCore.SignalR.Client;
using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Function;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace PhanMemQuanLyThiCong.ChatBox
{
    //[Obfuscation(ApplyToMembers = true, Exclude = false, Feature = "-rename")]

    public static class ConnextService
    {
        public static bool CheckConnext = true;
        public static string UriChat = "";
        private static Guid _userId;
        public static string _user { get; set; }
        public static string Avatarmember { get; set; }
        public static BusinessViewModel businessViewModel { get; set; }
        public static LabelControl lb_NoiDungCongTac { get; set; }
        public static RichTextBox lb_TenCongTac { get; set; }
        public static ctrl_XemFileNhieuCuaSo ctrl_XemFileNhieuCuaSo { get; set; }
        public static List<GiaoViecExtensionViewModel> ManageGiaoViecs { get; set; } = new List<GiaoViecExtensionViewModel>();
        public static GeneralGroupChatViewModel groupIndex { get; set; }

        public static List<GeneralGroupChatViewModel> ManageGroups { get; set; } = new List<GeneralGroupChatViewModel>();

        public static Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }



        public static string GroupId { get; set; }
        public static string Avatar { get; set; }
        public static int amountBusiness { get; set; }
        public static Guid ConstructionId { get; set; }
        public static List<string> files { get; set; } = new List<string>();
        public static List<string> memberList { get; set; } = new List<string>();
        public static HubConnection _Connection { get; set; }
        public static bool IsConnected
        {
            get
            {
                if (_Connection == null) return false;
                return _Connection.State == HubConnectionState.Connected;
            }
        } 

        private static IChatService _chatService = ConfigUnity.Container.Resolve<IChatService>();

        public static async Task<HubConnection> GetConnection()
        {
            string url = $@"{UriChat}notificationHub?userId={UserId.ToString()}";
            if (_Connection == null)
            {
                HubConnection connection = new HubConnectionBuilder().WithUrl(url).Build();
                connection.ServerTimeout = TimeSpan.MaxValue;
                try
                {
                    await connection.StartAsync();
                    _Connection = connection;
                }
                catch (Exception)
                {
                    await connection.StopAsync();
                    _Connection = null;
                }
                _Connection = connection;
            }
            return _Connection;
        }

        public static async Task AddUserToGroups(List<GeneralGroupChatViewModel> groupChats)
        {
            if (_Connection != null && ConnextService._Connection.State == HubConnectionState.Connected)
            {
                foreach (var gr in groupChats) 
                    await _Connection.InvokeAsync("AddUserToGroup", gr, false);
            }
        }
    }
}