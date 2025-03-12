
﻿using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.API;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model;

namespace PhanMemQuanLyThiCong
{
    public partial class FormDangNhap_HeThong_ChatRoom : Form
    {
        private string _apiUrl;

        private string _token;

        private Guid _userId;

        private bool _isConnectedToInternet;

        private string _tempifo = string.Empty;

        public FormDangNhap_HeThong_ChatRoom()
        {
            //Check connec internet
            _isConnectedToInternet = InternetConnection.IsConnectedToInternet();

            //Check setting url API call info login
            _apiUrl = AppSettings.UrlAPI;

            InitializeComponent();
        }

        private async void btn_DN_DangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                ResultMessage<AppUserViewModel> apiResponse = await ActLoginAsync();
                if (apiResponse.MESSAGE_TYPECODE)
                {
                    //MSETTING.Default.Token = apiResponse.Dto.Token;
                    MSETTING.Default.UserId = apiResponse.Dto.Id.ToString();
                    //_token = MSETTING.Default.Token = apiResponse.Dto.Token;
                    MSETTING.Default.Save();
                   
                    _tempifo = CryptoHelper.CompressString(JsonConvert.SerializeObject(apiResponse.Dto));
                    SaveSetting();
                    this.Hide();

                    FormDangNhap_ThongTinCaNhan frm = new FormDangNhap_ThongTinCaNhan(MyConstant.CONST_TYPE_FORMTHONGTINCANHAN_THONGTIN, apiResponse.Dto);
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageShower.ShowInformation("Đăng nhập không thành công");
                }
                //// Đăng nhập thông tin cá nhân cho hệ thống chung
                //var user = new User() { Email = txt_Email.Text, Password = txt_Pass.Text };
                //var response = await GeneralProp.Instance.client.PostAsJsonAsync("auths/login", user);
                ////var content =

                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    MessageShower.ShowInformation("Đăng nhập thành công");

                //    var content = response.Content.ReadAsStringAsync().Result;

                //    var token = JObject.Parse(JObject.Parse(content.ToString())["data"].ToString())["Token"].ToString();         // Đăng nhập fom đăng nhập
                //    MSETTING.Default.Token = token as string;
                //    MSETTING.Default.Save();
                //    //response = await GeneralProp.Instance.client.GetAsync("users/profile");
                //    this.Hide();

                //    FormDangNhap_ThongTinCaNhan frm = new FormDangNhap_ThongTinCaNhan(MyConstant.CONST_TYPE_FORMTHONGTINCANHAN_THONGTIN);
                //    frm.ShowDialog();
                //    this.Close();
                //}
                //else
                //{
                //    MessageShower.ShowInformation("Đăng nhập không thành công");

                //}
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                MessageShower.ShowInformation("Không thể đồng bộ dữ liệu với server! Vui lòng kiểm tra kết nối mạng hoặc website của bạn");
            }
        }

        private void btn_DN_Thoat_Click(object sender, EventArgs e)
        {
            // Đóng cửa sổ trong đăng nhập hệ thống
            this.Close();
        }

        private void bt_TaoTaiKhoan_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDangNhap_ThongTinCaNhan frm = new FormDangNhap_ThongTinCaNhan(MyConstant.CONST_TYPE_FORMTHONGTINCANHAN_DANGKY);
            frm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Login app API
        /// </summary>
        /// <returns></returns>
        private async Task<ResultMessage<AppUserViewModel>> ActLoginAsync()
        {
            AppUserViewModel sysUser = new AppUserViewModel();
            sysUser.Email = CryptoHelper.Base64Encode(txt_Email.Text);
            sysUser.Password = CryptoHelper.Base64Encode(txt_Pass.Text);

            ResultMessage<AppUserViewModel> apiResponse = await UtilAPI<AppUserViewModel>.Post(sysUser, _apiUrl + RouteAPI.USER_LOGIN);
            return apiResponse;
        }

        /// <summary>
        /// The SaveSetting
        /// </summary>
        private void SaveSetting()
        {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.AppSettings.Settings["TempUserName"].Value = CryptoHelper.Base64Encode(txt_Email.Text);
            //config.AppSettings.Settings["TempPassword"].Value = CryptoHelper.Base64Encode(txt_Pass.Text);
            //config.AppSettings.Settings["TempUserId"].Value = _userId.ToString();
            MSETTING.Default.TokenTBT = _token.ToString();
            MSETTING.Default.TempInfo = _tempifo ?? string.Empty;
            MSETTING.Default.Save();
        }
    }
}
