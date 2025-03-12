using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using VChatCore.ViewModels.SyncSqlite;
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
using VChatCore.Dto;
using PhanMemQuanLyThiCong.Constant.Enum;
using VChatCore.Model;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;

namespace PhanMemQuanLyThiCong
{
    public partial class DevControl_ThongTinDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        Tbl_ThongTinDuAnViewModel _DA;
        List<AppUserViewModel> _users;
        public DevControl_ThongTinDuAn(Tbl_ThongTinDuAnViewModel TTDA)
        {
            _DA = TTDA;
            //WaitFormHelper.ShowWaitForm("Đang tải thông tin");
            InitializeComponent();
        }
        private async void DevControl_ThongTinChiTietTongHopDuAn_Load(object sender, EventArgs e)
        {
            lb_notice.Visible = false;
            //WaitFormHelper.ShowWaitForm("Đang tải thông tin");

            Tbl_ThongTinDuAnViewModelBindingSource.DataSource = _DA;

            string notice = string.Empty;
            var allPermission = BaseFrom.allPermission;
            if (!BaseFrom.IsFullAccess
                && !allPermission.HaveInitProjectPermission
                && !allPermission.AllProjectThatUserIsAdmin.Contains(_DA.Code))
            {
                notice = "Lưu ý: Bạn không có quyền admin trong dự án này nên chỉ có thể xem thông tin!";
                lb_notice.Text = notice;
                lb_notice.Visible = true;
                ccbb_SelectUsers.ReadOnly = true;
                bt_ThemThanhVien.Enabled = false;
                gv_DanhSachNguoiDung.OptionsBehavior.ReadOnly = true;
                col_Xoa.Visible = false;
            }
            else if (allPermission.AllProjectThatUserIsAdmin.Contains(_DA.Code))
            {
                notice = "Lưu ý: Bạn chỉ có quyền thêm/xóa thành viên, không có quyền cài đặt admin!";
                lb_notice.Text = notice;
                lb_notice.Visible = true;
                col_admin.OptionsColumn.ReadOnly = true;
            }

            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu");
            _users = await UserHelper.GetAllUserInCusSever();

            var result = await CusHttpClient.InstanceCustomer
                    .MGetAsync<List<AppUserViewModel>>($"{RouteAPI.USERINPROJECT_getUserIdsInProject}/{_DA.Code}");

            WaitFormHelper.CloseWaitForm();
            if (!result.MESSAGE_TYPECODE || _users is null)
            {
                MessageShower.ShowError("Không thể tải dữ liệu");
                gc_Users.Enabled = false;
                //WaitFormHelper.CloseWaitForm();

                return;
            }
            else
            {
                gc_Users.Enabled = true;

                _users = _users.OrderBy(x => x.DisplayInCombobox).ToList();

                gc_Users.DataSource = new BindingList<AppUserViewModel>(result.Dto);

                var idUsersAdded = result.Dto.Select(x => x.Id);

                ccbb_SelectUsers.Properties.DataSource = new BindingList<AppUserViewModel>(_users.Where(x => !idUsersAdded.Contains(x.Id)).ToList()); //UserForCheckBox
            }
            //WaitFormHelper.CloseWaitForm();

        }

        /*        private void LoadPhanQuyen()
                {

                    foreach (var user in _users)
                    {
                        TreeListBand mainBand = tl_PhanQuyen.Bands.AddBand(caption: $"{user.FullName} ({user.Email})");

                        TreeListColumn colPermission = tl_PhanQuyen.Columns.AddVisible(fieldName: "");
                        colPermission.Caption = "Quyền";

                        TreeListColumn colUser = tl_PhanQuyen.Columns.AddVisible(fieldName: "");
                        colUser.Caption = "Vai trò";

                        mainBand.Columns.Add(colPermission);
                        mainBand.Columns.Add(colUser);
                        mainBand.SeparatorWidth = 2;
                    }
                }*/


        private async void bt_ThemThanhVien_Click(object sender, EventArgs e)
        {
            bt_ThemThanhVien.Enabled = false;

            string[] selecteds = ccbb_SelectUsers.Properties.Items.Where(x => x.CheckState == CheckState.Checked).Select(x => x.Value.ToString()).ToArray();
            if (!selecteds.Any()) 
            {
                MessageShower.ShowInformation("Vui lòng chọn thành viên cần thêm");
                bt_ThemThanhVien.Enabled = true;

                return;
            }

            var usersWillAdd = _users.Where(x => selecteds.Contains(x.DisplayInCombobox));
            var uips = usersWillAdd
                .Select(x => new UserInProjectViewModel
                {
                    UserId = x.Id,
                    ProjectId = _DA.Code,
                    IsAdmin = false,
                    ProjectName = _DA.TenDuAn,
                }).ToList();

            WaitFormHelper.ShowWaitForm("Đang cập nhật");
            var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.USERINPROJECT_CONTROLLER}/{RouteAPI.SUFFIX_CreateMulti}", uips);
            WaitFormHelper.CloseWaitForm();

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Thêm người dùng không thành công! Kiểm tra kết nối internet");
                
            }
            else
            {
                MessageShower.ShowInformation("Thêm người dùng thành công");
                //_users = _users.Where(x => !selecteds.Contains(x.DisplayInCombobox)).ToList();
                var userInCombobox = ccbb_SelectUsers.Properties.DataSource as BindingList<AppUserViewModel>;
                var userInGc = gc_Users.DataSource as BindingList<AppUserViewModel>;

                ccbb_SelectUsers.SetEditValue(null);
                //ccbb_SelectUsers.Text = "";

                foreach (var u in usersWillAdd)
                {
                    userInCombobox.Remove(u);
                    userInGc.Add(u);
                }

                ccbb_SelectUsers.EditValue = "";

                
            }
            bt_ThemThanhVien.Enabled = true;


        }

        private async void repobt_IsAdmin_Click(object sender, EventArgs e)
        {
            
        }

        private async void gv_DanhSachNguoiDung_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var crUser = gv_DanhSachNguoiDung.FocusedRowObject as AppUserViewModel;
            if (!bool.TryParse(e.Value.ToString(), out bool newVal) || e.Column.FieldName != nameof(AppUserViewModel.IsAdmin))
            {
                MessageShower.ShowError("Không thể thay đổi giá trị này");
                gv_DanhSachNguoiDung.HideEditor();
                return;
            }

            UserInProjectViewModel uip = new UserInProjectViewModel()
            {
                UserId = crUser.Id,
                ProjectId = _DA.Code,
                IsAdmin = newVal,
                ProjectName = _DA.TenDuAn,
                FieldUpdate = FieldUpdateEnum.IsAdmin

            };

            WaitFormHelper.ShowWaitForm("Đang cập nhật");

            var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.USERINPROJECT_CONTROLLER}/{RouteAPI.SUFFIX_Update}", uip);

            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Không thể cập nhật trạng thái!");
                gv_DanhSachNguoiDung.HideEditor();
                WaitFormHelper.CloseWaitForm();
                return;
            }
            WaitFormHelper.CloseWaitForm();
        }

        private async void repoBt_Xoa_Click(object sender, EventArgs e)
        {
            var crUser = gv_DanhSachNguoiDung.FocusedRowObject as AppUserViewModel;
            
            var allPermission = BaseFrom.allPermission;
            if (!BaseFrom.IsFullAccess
                && !allPermission.HaveInitProjectPermission
                && allPermission.AllProjectThatUserIsAdmin.Contains(_DA.Code)
                && crUser.IsAdmin)
            {
                MessageShower.ShowError("Bạn không thể xóa admin của dự án. Chỉ có thể xóa thành viên thông thường");
                return;
            }

            UserInProjectViewModel uip = new UserInProjectViewModel()
            {
                UserId = crUser.Id,
                ProjectId = _DA.Code,
                ProjectName = _DA.TenDuAn,
            };


            WaitFormHelper.ShowWaitForm("Đang cập nhật");

            var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.USERINPROJECT_CONTROLLER}/{RouteAPI.SUFFIX_DeleteByVm}", uip);

            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Xóa thất bại!");
                gv_DanhSachNguoiDung.HideEditor();
                WaitFormHelper.CloseWaitForm();
                return;
            }
            MessageShower.ShowInformation("Đã xóa người dùng");

            var userInCombobox = ccbb_SelectUsers.Properties.DataSource as BindingList<AppUserViewModel>;
            gv_DanhSachNguoiDung.DeleteSelectedRows();

            userInCombobox.Add(crUser);
            ccbb_SelectUsers.Properties.DataSource = new BindingList<AppUserViewModel>(userInCombobox.OrderBy(x => x.DisplayInCombobox).ToList());
            
            WaitFormHelper.CloseWaitForm();
        }

        private void gc_Users_DataSourceChanged(object sender, EventArgs e)
        {
            gv_DanhSachNguoiDung.ExpandAllGroups();
        }

        private void ccbb_SelectUsers_Popup(object sender, EventArgs e)
        {

        }
    }
}
