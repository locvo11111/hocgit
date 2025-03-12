namespace PhanMemQuanLyThiCong.ViewModels {
    using System.Threading.Tasks;
    using DevExpress.DevAV.Chat;
    using DevExpress.DevAV.Chat.Commands;
    using DevExpress.DevAV.Chat.Events;
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using PhanMemQuanLyThiCong.ChatBox;
    using Unity;

    public class MessengerViewModel {
        public virtual string Title {
            get;
            protected set;
        }
        IDispatcherService dispatcher;
        IChannel channel;
        public async Task OnLoad() {
            dispatcher = this.GetRequiredService<IDispatcherService>();
            
            var settingsService = this.GetRequiredService<ISettingsService>();
            var theme = settingsService.Theme ?? "Light";
            var currentUser = settingsService.CurrentUser ?? "John Heart";
            
            var messageServer = this.GetRequiredService<IMessageServer>();
            channel = await messageServer.Create(currentUser);
            channel.Subscribe(OnChannelEvent);
            
            Messenger.Default.Send(channel);
            await dispatcher.BeginInvoke(() => Title = $"DevExpress Chat Client (CS) - [{currentUser.ToUpper()}]");
        }
        int authCounter = 0;
        void OnChannelEvent(ChannelEvent @event) {
            var credentialsRequired = @event as CredentialsRequiredEvent;
            if(credentialsRequired != null) {
                if(0 == authCounter++) {
                    
                    var cacheQuery = QueryAccessTokenFromLocalAuthCache(@event.UserName, credentialsRequired.Salt);
                    credentialsRequired.SetAccessTokenQuery(cacheQuery);
                }
                else {
                    
                    var userQuery = QueryAccessTokenFromUser(@event.UserName, credentialsRequired.Salt);
                    credentialsRequired.SetAccessTokenQuery(userQuery);
                }
            }
        }
        Task<string> QueryAccessTokenFromLocalAuthCache(string userName, string salt) {
            
            return Task.FromResult(DevAVEmpployeesInMemoryServer.GetPasswordHash(string.Empty, salt));
        }
        Task<string> QueryAccessTokenFromUser(string userName, string salt) {
            var accessTokenQueryCompletionSource = new TaskCompletionSource<string>();
            //dispatcher.BeginInvoke(() => {
            //    var signInViewModel = SignInViewModel.Create(userName, salt);
            //    signInViewModel.ShowDialog();
            //    if(!string.IsNullOrEmpty(signInViewModel.AccessToken))
            //        accessTokenQueryCompletionSource.SetResult(signInViewModel.AccessToken);
            //    else
            //        accessTokenQueryCompletionSource.SetCanceled();
            //});
            return accessTokenQueryCompletionSource.Task;
        }
        public void OnClosed() {
            if(channel != null)
                channel.Dispose();
            channel = null;
        }
        public void LogOff() {
            if(channel != null)
                channel.Send(new LogOff(channel));
        }
        UserViewModel userViewModel;
        [Command(false)]
        public void ShowUserInfo(UserInfo userInfo) {
            ShowPopup(userViewModel ?? (userViewModel = UserViewModel.Create(userInfo)), userInfo);
        }
        ContactViewModel contactViewModel;
        [Command(false)]
        public void ShowContactInfo(UserInfo contactInfo) {
            ShowPopup(contactViewModel ?? (contactViewModel = ContactViewModel.Create(contactInfo)), contactInfo);
        }
        void ShowPopup(UserInfoViewModel viewModel, UserInfo info) {
            viewModel.SetParentViewModel(this);
            var popup = this.GetService<IWindowService>(viewModel.ServiceKey);
            popup.Show(null, viewModel, info, this);
        }
        public static void ShowUserInfo(object viewModel, UserInfo userInfo) {
            var messenger = POCOViewModelExtensions.GetParentViewModel<MessengerViewModel>(viewModel);
            if(messenger != null)
                messenger.ShowUserInfo(userInfo);
        }
        public static void ShowContactInfo(object viewModel, UserInfo contactInfo) {
            var messenger = POCOViewModelExtensions.GetParentViewModel<MessengerViewModel>(viewModel);
            if(messenger != null)
                messenger.ShowContactInfo(contactInfo);
        }
        
    }
}
