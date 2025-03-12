using DevExpress.DevAV;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.PermissionControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class uc_ThongTinCaNhan : DevExpress.XtraEditors.XtraUserControl
    {
        TypeThongTinCaNhan _typeTTCN = TypeThongTinCaNhan.ThongTinCaNhan;
        AppUserViewModel _user = null;
        public uc_ThongTinCaNhan()
        {
            InitializeComponent();
        }

        public event EventHandler SignedOut;
        public event EventHandler KeyCodeChanged;

        protected void OnSignedOut()
        {
            SignedOut?.Invoke(this, EventArgs.Empty);
        }

        protected void OnKeyCodeChange()
        {
            KeyCodeChanged?.Invoke(this, EventArgs.Empty);
        }

        public async void SetInfoUser(AppUserViewModel user)
        {
            _user = user;
            appUserViewModelBindingSource.DataSource = user;
            
            if (!MSETTING.Default.IsLicenseMode)
            {
                lc_KeySelection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                string route = $"{RouteAPI.KEYSTORE_KeyContainingUser}/{user.Id}/{AppSettings.CategoryCode}";
                var result = await CusHttpClient.InstanceTBT.MGetAsync<List<KeyInfoViewModel>>(route);
                
                if (!result.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError("Không thể tải thông tin các khóa cứng bạn đang tham gia");
                    gc_keys.DataSource = null;
                    return;
                }

                //gv_Keys.FormatRules.BeginUpdate();

                gc_keys.DataSource = new BindingList<KeyInfoViewModel>(result.Dto);
                //gv_Keys.FormatRules.EndUpdate();
            }
        }

        private void bt_ChinhSua_Click(object sender, EventArgs e)
        {
            switch (_typeTTCN)
            {
                case TypeThongTinCaNhan.ThongTinCaNhan:
                    break;
                case TypeThongTinCaNhan.ChinhSua:
                    break;
                default:
                    break;
            }    
        }

        private async void bt_Logout_Click(object sender, EventArgs e)
        {
            if (BaseFrom.IsFullAccess)
            {
                MessageShower.ShowInformation("Không thể đăng xuất khi đang dùng bản quyền, Chuyển sang chế độ dùng tài khoản!");
                return;
            }

            switch (_typeTTCN)
            {
                case TypeThongTinCaNhan.ThongTinCaNhan:
                    var response = await CusHttpClient.InstanceTBT.PostAsJsonAsync(RouteAPI.USER_LOGOUT, new { });
                    if (response.IsSuccessStatusCode)
                    {
                        MessageShower.ShowInformation("Đã đăng xuất");
                        MSETTING.Default.TokenTBT = string.Empty;
                        MSETTING.Default.Save();// = string.Empty;
                        OnSignedOut();

                    }
                    break;
                case TypeThongTinCaNhan.ChinhSua:
                    break;
                default:
                    break;
            }
        }

        private void repoCe_ChonDangDung_CheckedChanged(object sender, EventArgs e)
        {
            gv_Keys.CloseEditor();
        }

        private void gv_Keys_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //var current
        }

        private async void repoBt_Click(object sender, EventArgs e)
        {
            KeyInfoViewModel crKey = gv_Keys.FocusedRowObject as KeyInfoViewModel;
            if (crKey == null)
                return;

            if (crKey.IsUsing)
            {
                MessageShower.ShowInformation("Khóa đã được chọn!");
                return;
            }

            if (crKey.IsSeverExpired)
            {
                MessageShower.ShowInformation("Khóa đã hết hạn sử dụng!");
                return;
            }
            
            WaitFormHelper.ShowWaitForm("Đang tải thông tin khóa");

            var resultChangeKey = await CusHttpClient.InstanceTBT.MGetAsync<string>($"{RouteAPI.USER_ChangeSerialNo}/{CryptoHelper.Base64Encode(crKey.SerialNo)}");


            if (!resultChangeKey.MESSAGE_TYPECODE)
            {
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowError($"{resultChangeKey.MESSAGE_CONTENT}", "Lỗi đăng nhập khóa mới");
                return;
            }
            MSETTING.Default.TokenTBT = resultChangeKey.Dto;
            MSETTING.Default.Save();


            CusHttpClient.InstanceCustomer.BaseAddress = crKey.UrlAPI;

            
            await SharedProjectHelper.SyncRoleFromServer();
            TongHopHelper.SetControlsByPermission();

            BaseFrom.BanQuyenKeyInfo.UrlAPI = crKey.UrlAPI;
            BaseFrom.BanQuyenKeyInfo.KeyCode = crKey.KeyCode;
            BaseFrom.BanQuyenKeyInfo.SerialNo = crKey.SerialNo;
            BaseFrom.BanQuyenKeyInfo.TypeCode = crKey.TypeCode;
            BaseFrom.BanQuyenKeyInfo.StartSeverDate = crKey.StartSeverDate;
            BaseFrom.BanQuyenKeyInfo.EndSeverDate = crKey.EndSeverDate;
            BaseFrom.BanQuyenKeyInfo.StartDate = crKey.StartDate;
            BaseFrom.BanQuyenKeyInfo.EndDate = crKey.EndDate;

            MSETTING.Default.SerialNo = crKey.SerialNo;
            MSETTING.Default.Save();
            OnKeyCodeChange();


            gc_keys.RefreshDataSource();
            WaitFormHelper.CloseWaitForm();
            //gv_Keys.FormatRules.EndUpdate();


        }

        private void gc_keys_DataSourceChanged(object sender, EventArgs e)
        {
            gv_Keys.ExpandAllGroups();
        }

        private void lb_ChangePassword_Click(object sender, EventArgs e)
        {
            XtraForm_ChangePassword form = new XtraForm_ChangePassword();
            form.ShowDialog();
            OnSignedOut();
        }
    }
}
