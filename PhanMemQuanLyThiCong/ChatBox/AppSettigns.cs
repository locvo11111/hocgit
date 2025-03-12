namespace PhanMemQuanLyThiCong.ChatBox
{
    sealed class AppSettigns : ISettingsService {
        public static void Register() {
            DevExpress.Mvvm.ServiceContainer.Default.RegisterService(new AppSettigns());
        }
        public string CurrentUser {
            get { return "Người dùng 01"; }
        }
        public string Theme {
            get { return "Theme người dùng 01"; }
        }
    }
}
