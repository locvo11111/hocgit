using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_ChangePassword : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_ChangePassword()
        {
            InitializeComponent();
        }

        private async void bt_Ok_Click(object sender, EventArgs e)
        {
            
            string oldPass = txt_OldPass.Text;
            string newPass = txt_NewPass.Text;
            string confirm = txt_Confirm.Text;

            if (!oldPass.HasValue() || !newPass.HasValue() || !confirm.HasValue())
            {
                MessageShower.ShowWarning("Vui lòng điền đầy đủ các trường thông tin");
                return;
            }

            if (newPass != confirm)
            {
                MessageShower.ShowWarning("Xác nhận mật khẩu mới không trùng khớp");
                return;
            }

            var vm = new ChangePasswordRequest()
            {
                OldPassword = oldPass,
                NewPassword = newPass
            };
            WaitFormHelper.ShowWaitForm("Đang đổi mật khẩu");
            var result = await CusHttpClient.InstanceTBT
                .MPostAsJsonAsync<bool>($"{RouteAPI.USER_ChangePassword}", vm);

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError(result.MESSAGE_CONTENT, "Đổi mật khẩu không thành công!");
                WaitFormHelper.CloseWaitForm();
                return;
            }
            WaitFormHelper.CloseWaitForm();

            Close();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}