using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Model.NumberFormatting;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.ChatBox;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_DangNhap_ThongTin : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SenData(LoginResponse response);
        public SenData senData;
        public XtraForm_DangNhap_ThongTin()
        {
            InitializeComponent();
        }

        private async void XtraForm_DangKy_DangNhap_ThongTin_Load(object sender, EventArgs e)
        {
            this.Controls.Clear();

            try
            {
                WaitFormHelper.ShowWaitForm("Đang tải thông tin cá nhân!");
                var res = await CusHttpClient.InstanceTBT.MGetAsync<AppUserViewModel>(RouteAPI.USER_VALIDATETOKEN);
                WaitFormHelper.CloseWaitForm();
                if (res.MESSAGE_TYPECODE)
                {
                    
                    //var res = MyDataHelper.ReadDataFromJson<ResultMessage<AppUserViewModel>>(response);
                    AppUserViewModel user = res.Dto;

                    var login = new LoginResponse();
                    login.UserName = user.UserName;
                    login.Id = user.Id;
                    login.UserName = user.UserName;
                    login.FullName = user.FullName;
                    login.Email = user.Email;
                    login.PhoneNumber = user.PhoneNumber;
                    login.Avatar = user.Avatar;
                    login.DateOfBirth = user.DateOfBirth;
                    BaseFrom.UserInfo = login;
                    uc_ThongTinCaNhan uc_ThongTin = new uc_ThongTinCaNhan();
                    uc_ThongTin.Dock = DockStyle.Fill;
                    uc_ThongTin.SetInfoUser(user);
                    uc_ThongTin.SignedOut += XtraForm_DangNhap_ThongTin_SignOut;
                    uc_ThongTin.KeyCodeChanged += XtraForm_DangNhap_ThongTin_KeyCodeChange;
                    this.Controls.Add(uc_ThongTin);
                }
                else
                {
                    if (SharedControls.ce_Mode.Checked)
                    {
                        var mess = $"Bạn đang ở chế độ \"BẢN QUYỀN\" nhưng không có bản quyền.\r\n" +
                            $"Vui lóng đăng ký bản quyền hoặc chuyển sang chế độ \"TÀI KHOẢN\" để \"ĐĂNG NHẬP\"!";
                        MessageShower.ShowWarning(mess);
                        Close();
                    }

                    CusHttpClient.InstanceCustomer.BaseAddress = string.Empty;
                    uc_DangNhap uc_DangNhap = new uc_DangNhap();
                    uc_DangNhap.Dock = DockStyle.Fill;
                    uc_DangNhap.LoginSucceed += XtraForm_DangNhap_ThongTin_LoginSucceed;
                    this.Controls.Add(uc_DangNhap);
                }    
            }
            catch (Exception ex)
            {
                MessageShower.ShowInformation("Lỗi tải thông tin người dùng! Vui lòng kiểm tra kết nối internet của bạn!");
                return;
            }
        }

        private void XtraForm_DangNhap_ThongTin_LoginSucceed(object sender, EventArgs e)
        {

            XtraForm_DangKy_DangNhap_ThongTin_Load(null, null);
        }
        
        private void XtraForm_DangNhap_ThongTin_SignOut(object sender, EventArgs e)
        {

            XtraForm_DangKy_DangNhap_ThongTin_Load(null, null);
            senData(null);

        }

        private void XtraForm_DangNhap_ThongTin_KeyCodeChange(object sender, EventArgs e)
        {
            XtraForm_DangKy_DangNhap_ThongTin_Load(null, null);
            senData(null);
            this.Close();
        }

        private void XtraForm_DangNhap_ThongTin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //senData(BaseFrom.UserInfo);
        }
    }
}