namespace PhanMemQuanLyThiCong.Services {
    sealed class AppSettigns : ISettingsService {
        public static void Register() {
            DevExpress.Mvvm.ServiceContainer.Default.RegisterService(new AppSettigns());
        }
        public string CurrentUser {
            get { return PhanMemQuanLyThiCong.Properties.Settings.Default.TokenTBT; }
        }
        public string Theme {
            get { return PhanMemQuanLyThiCong.Properties.Settings.Default.Theme; }
        }
    }
}
