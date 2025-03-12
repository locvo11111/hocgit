using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class uc_DangNhap : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_DangNhap()
        {
            InitializeComponent();
        }

        public event EventHandler LoginSucceed;

        protected void OnLoginSucceed()
        {
            LoginSucceed?.Invoke(this, EventArgs.Empty);

        }
        private async void bt_DangNhap_Click(object sender, EventArgs e)
        {
            if (!dataLayoutControl1.Validate())
            {
                MessageShower.ShowInformation("Vui lòng nhập đủ các trường yêu cầu!");
                return;
            }
            LoginRequest loginReq = new LoginRequest()
            {
                UserName = UserNameTextEdit.Text,
                Password = PasswordTextEdit.Text,
                RememberMe = RememberMeCheckEdit.Checked,
            };

            var res = await CusHttpClient.InstanceTBT.MPostAsJsonAsync<LoginResponse>(RouteAPI.USER_LOGIN, loginReq);


            if (res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Đăng nhập thành công!");
                //var loginResponse = MyDataHelper.ReadDataFromJson<ResultMessage<LoginResponse>>(response);

                MSETTING.Default.TokenTBT = res.Dto.Token;
                MSETTING.Default.Save();

                BaseFrom.BanQuyenKeyInfo = new BanQuyenKeyInfo(res.Dto);
                MSETTING.Default.SerialNo = string.Empty;
                MSETTING.Default.Save();

                if (BaseFrom.IsFullAccess)
                {
                    var ret = await UserHelper.SyncUserFromTBTBySerialNo();
                    if (!ret.MESSAGE_TYPECODE)
                    {
                        MessageShower.ShowInformation("Không thể đồng bộ sever");
                    }    
                }

                OnLoginSucceed();
                //btDnOK.PerformClick();
            }
            else
            {
                MessageShower.ShowInformation("Đăng ký không thành công!");
            }    


        }
    }
}
