using DevExpress.LookAndFeel.Design;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_CaiDatKhoaCung : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_CaiDatKhoaCung()
        {
            InitializeComponent();
        }

        private async void XtraForm_CaiDatKhoaCung_Load(object sender, EventArgs e)
        {
            if (BaseFrom.IsFullAccess) 
            {
                //txtFullName.Text = BaseFrom.BanQuyenKeyInfo.FullName;
                //txtEmail.Text = BaseFrom.BanQuyenKeyInfo.Email;
                //txtKeyCode.Text = BaseFrom.BanQuyenKeyInfo.KeyCode;
                //txtSerialNo.Text = BaseFrom.BanQuyenKeyInfo.SerialNo;
                banQuyenKeyInfoBindingSource.DataSource = BaseFrom.BanQuyenKeyInfo;

                await GetUserInKey();
            }
            else
                this.Close();
        }

        private async Task GetUserInKey()
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhật thông tin khóa...");

            var usersInKey = await CusHttpClient.InstanceTBT
                .MGetAsync<List<AppUserViewModel>>($"{RouteAPI.SYSINFO_USERS}/{CryptoHelper.Base64Encode(BaseFrom.BanQuyenKeyInfo.SerialNo)}");

            if (!usersInKey.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Không thể tải thông tin khóa");
                this.Close();
            }

            gc_DanhSachNoiBo.DataSource = new BindingList<AppUserViewModel>(usersInKey.Dto.Where(x => x.Email != BaseFrom.BanQuyenKeyInfo.Email).ToList());
            WaitFormHelper.CloseWaitForm();
        }

        private async void bt_TiemKiem_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang tìm kiếm ....");

            var user = await CusHttpClient.InstanceTBT.MGetAsync<AppUserViewModel>($"{RouteAPI.USER_GETBYEMAILORPHONE}?search={txtTimKiem.Text}");
            
            if (!user.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Không tìm thấy thông tin người dùng!");
                WaitFormHelper.CloseWaitForm();
                return;
            }

            appUserViewModelBindingSource.DataSource = user.Dto;
            WaitFormHelper.CloseWaitForm();
        }

        private async void bt_Them_Click(object sender, EventArgs e)
        {
            var crUser = appUserViewModelBindingSource.DataSource as AppUserViewModel;
            
            if (crUser is null)
            {
                MessageShower.ShowError("Vui lòng nhập thông tin tài khoản trước");
                return;
            }

            var listExistingUser = gc_DanhSachNoiBo.DataSource as BindingList<AppUserViewModel>;

            if (crUser.Email == BaseFrom.BanQuyenKeyInfo.Email)
            {
                MessageShower.ShowInformation("Đây là tài khoản Admin gắn liền với khóa cứng");
                return;
            }



            if (listExistingUser.Any(x => x.Id == crUser.Id))
            {
                MessageShower.ShowInformation("Người dùng đã tồn tại trong danh sách!");
                return;
            }

            UserInKeyViewModel ua = new UserInKeyViewModel()
            {
                UserId = crUser.Id,
                SerialNo = CryptoHelper.Base64Encode(BaseFrom.BanQuyenKeyInfo.SerialNo)
            };

            DialogResult dr = MessageShower.ShowYesNoCancelQuestionWithCustomText("Chọn vị trí thêm tài khoản?", "Lựa chọn", "Nội bộ", "Khách");

            switch (dr)
            {
                case DialogResult.Yes://Nội bộ
                    ua.AccountTypeId = AccountType.INTERNAL;
                    if (listExistingUser.Count(x => x.AccountTypeId == AccountType.INTERNAL) >= BaseFrom.BanQuyenKeyInfo.LimitUser)
                    {
                        MessageShower.ShowInformation("Đã đạt số tài khoản nội bộ tối đa, không thể thêm mới");
                        return;
                    }
                    break; 
                case DialogResult.No:
                    ua.AccountTypeId = AccountType.EXTERNAL;
                    if (listExistingUser.Count(x => x.AccountTypeId == AccountType.EXTERNAL) >= BaseFrom.BanQuyenKeyInfo.LimitUserExternal)
                    {
                        MessageShower.ShowInformation("Đã đạt số tài khoản khách tối đa, không thể thêm mới");
                        return;
                    }
                    break;

                default: return;
            }

            WaitFormHelper.ShowWaitForm("Đang thêm người dùng");

            var res = await CusHttpClient.InstanceTBT.MPostAsJsonAsync<bool>(RouteAPI.SYSINFO_ADDUSERS, ua);
           
            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Không thể thêm người dùng, Kiểm tra kết nối internet!");
                WaitFormHelper.CloseWaitForm();
                return;
            }
            else
            {
                WaitFormHelper.ShowWaitForm("Đang đồng bộ người dùng");
                var response = await UserHelper.SyncUserFromTBTBySerialNo();

                if (!response.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowInformation("Không thể đồng bộ người dùng với sever chủ");
                }
                WaitFormHelper.CloseWaitForm();

            }

            await GetUserInKey();
        }

        private void gc_DanhSachNoiBo_DataSourceChanged(object sender, EventArgs e)
        {
            gv_DanhSachNguoiDung.ExpandAllGroups();
        }

        private async void repoBt_Xoa_Click(object sender, EventArgs e)
        {
            var crUser = gv_DanhSachNguoiDung.FocusedRowObject as AppUserViewModel;

            UserInKeyViewModel ua = new UserInKeyViewModel()
            {
                UserId = crUser.Id,
                SerialNo = CryptoHelper.Base64Encode(BaseFrom.BanQuyenKeyInfo.SerialNo)
            };

            if (MessageShower.ShowYesNoQuestion($"Bạn có muốn xóa {crUser.Email} khỏi khóa này không?") == DialogResult.No)
                return;

            WaitFormHelper.ShowWaitForm("Đang xóa người dùng");

            var res = await CusHttpClient.InstanceTBT.MPostAsJsonAsync<bool>(RouteAPI.SYSINFO_DeleteUSERS, ua);

            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Không thể xóa người dùng, Kiểm tra kết nối internet!");
                WaitFormHelper.CloseWaitForm();
                return;
            }
            else
            {
                WaitFormHelper.ShowWaitForm("Đang đồng bộ người dùng");
                var response = await UserHelper.SyncUserFromTBTBySerialNo();

                if (!response.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowInformation("Không thể đồng bộ người dùng với sever chủ");
                }
                WaitFormHelper.CloseWaitForm();

            }

            await GetUserInKey();
        }

        private async void gv_DanhSachNguoiDung_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var crUser = gv_DanhSachNguoiDung.FocusedRowObject as AppUserViewModel;

            if (crUser.AccountTypeId != AccountType.INTERNAL)
            {
                MessageShower.ShowError("Không thể cấp quyền dự án cho tài khoản khách!");
                gv_DanhSachNguoiDung.HideEditor();

                return;
            }

            if (crUser is null || !bool.TryParse(e.Value.ToString(), out bool newval))
            {
                MessageShower.ShowError("Lỗi");
                gv_DanhSachNguoiDung.HideEditor();

                return;
            }

            UserInKeyViewModel ua = new UserInKeyViewModel()
            {
                UserId = crUser.Id,
                AccountTypeId = crUser.AccountTypeId,
                SerialNo = CryptoHelper.Base64Encode(BaseFrom.BanQuyenKeyInfo.SerialNo),
                HaveInitProjectPermission = newval
            };


            var res = await CusHttpClient.InstanceTBT
                .MPostAsJsonAsync<bool>($"{RouteAPI.SYSINFO_UpdateUSERS}", ua);

            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Không thể cập nhật trạng thái");
                gv_DanhSachNguoiDung.HideEditor();
                return;
            }

            var user = new AppUserViewModel { Id = crUser.Id, HaveInitProjectPermission = newval };
            res = await CusHttpClient.InstanceCustomer
                    .MPostAsJsonAsync<bool>($"{RouteAPI.USER_SetInitPrjPermission}", ua);

            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Lỗi đồng bộ server");
                gv_DanhSachNguoiDung.HideEditor();
                return;
            }
        }

        private async void gv_DanhSachNguoiDung_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void repoce_KhoiTaoDA_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void repoce_KhoiTaoDA_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void gv_DanhSachNguoiDung_DataSourceChanged(object sender, EventArgs e)
        {
            gv_DanhSachNguoiDung.ExpandAllGroups();
        }
    }
}