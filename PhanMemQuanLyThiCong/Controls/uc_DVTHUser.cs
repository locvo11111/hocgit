using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Model;
using PM360.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Model.PermissionControl;
using DevExpress.Mvvm.Native;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class uc_DVTHUser : DevExpress.XtraEditors.XtraUserControl
    {
        string _DAId;
        List<AppUserViewModel> _users;

        public uc_DVTHUser(string DAId)
        {
            _DAId = DAId;
            InitializeComponent();
        }

        private async void uc_DVTHUser_Load(object sender, EventArgs e)
        {
            lb_notice.Visible = false;

            //WaitFormHelper.ShowWaitForm("Đang tải thông tin");
            this.Enabled = false;

            var response = await CusHttpClient.InstanceCustomer
                .MGetAsync<List<ContractorViewModel>>($"{RouteAPI.TongDuAn_CONTROLLER}/GetAllConstrutorByPrjAndUser/{_DAId}/false");

            if (_users is null)
                _users = await UserHelper.GetAllUserInProject(_DAId);

            if (!response.MESSAGE_TYPECODE || _users is null)
            {
                MessageShower.ShowError(MessageText.SERVER_FAIL);
            }
            else
            {
                this.Enabled = true;
                var DVTHs = response.Dto;
                var allPermission = BaseFrom.allPermission;
                if (!BaseFrom.IsFullAccess
                    && !allPermission.HaveInitProjectPermission
                    && !allPermission.AllProject.Contains(_DAId))
                {
                    DVTHs = DVTHs.Where(x => allPermission.AllContractor.Contains(x.Code)).ToList();
                }

                sle_DVTH.Properties.DataSource = DVTHs;
                sle_DVTH.EditValue = DVTHs.FirstOrDefault();
            }
            //WaitFormHelper.CloseWaitForm();
        }

        private async void sle_DVTH_EditValueChanged(object sender, EventArgs e)
        {
            ContractorViewModel c = sle_DVTH.EditValue as ContractorViewModel;

            if (c is null)
            return;

            var allPermission = BaseFrom.allPermission;
            string notice = string.Empty;
            if (!BaseFrom.IsFullAccess
                && !allPermission.HaveInitProjectPermission
                && !allPermission.AllProjectThatUserIsAdmin.Contains(_DAId)
                && !allPermission.AllContractorThatUserIsAdmin.Contains(c.Code))
            {
                notice = "Lưu ý: Bạn chỉ có quyền xem danh sách! không có quyền chỉnh sửa";
                ccbb_SelectUsers.ReadOnly = true;
                bt_ThemThanhVien.Enabled = false;
                gv_DanhSachNguoiDung.OptionsBehavior.ReadOnly = true;
                col_Xoa.Visible = false;
            }    
            else if (allPermission.AllContractorThatUserIsAdmin.Contains(c.Code))
            {
                notice = "Lưu ý: Bạn chỉ có quyền thêm/xóa thành viên, không có quyền cài đặt admin!";
                col_admin.OptionsColumn.ReadOnly = true;
            }

            if (!string.IsNullOrEmpty(notice))
            {
                lb_notice.Text = notice;
                lb_notice.Visible = true;
            }
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu");
            var response  = await CusHttpClient.InstanceCustomer
                .MGetAsync<List<AppUserViewModel>>
                ($"{RouteAPI.USERINCONTRACTOR_CONTROLLER}/GetUsersByContractorId/{c.type.GetEnumDescription()}/{c.Code}");
            WaitFormHelper.CloseWaitForm();

            if (!response.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError(MessageText.SERVER_FAIL);
                bt_ThemThanhVien.Enabled = gc_Users.Enabled = false;
            }
            else
            {
                _users.ForEach(x => x.UserInContractorId = Guid.NewGuid());
                bt_ThemThanhVien.Enabled = gc_Users.Enabled = true;
                gc_Users.Enabled = true;

                _users = _users.OrderBy(x => x.DisplayInCombobox).ToList();

                gc_Users.DataSource = new BindingList<AppUserViewModel>(response.Dto);

                var idUsersAdded = response.Dto.Select(x => x.Id);

                ccbb_SelectUsers.Properties.DataSource = new BindingList<AppUserViewModel>(_users.Where(x => !idUsersAdded.Contains(x.Id)).ToList());
            }    
        }

        private void repobt_IsAdmin_Click(object sender, EventArgs e)
        {

        }

        private async void repoBt_Xoa_Click(object sender, EventArgs e)
        {
            ContractorViewModel c = sle_DVTH.EditValue as ContractorViewModel;

            var crUser = gv_DanhSachNguoiDung.GetFocusedRow() as AppUserViewModel;

            var allPermission = BaseFrom.allPermission;
            if (!BaseFrom.IsFullAccess
                && !allPermission.HaveInitProjectPermission
                && allPermission.AllContractorThatUserIsAdmin.Contains(c.Code)
                && crUser.IsAdmin)
            {
                MessageShower.ShowError("Bạn không thể xóa admin của nhóm. Chỉ có thể xóa thành viên thông thường");
                return;
            }

            if (MessageShower.ShowYesNoQuestion($"Người dùng \"{crUser.FullName} ({crUser.Email})\" sẽ bị xóa khỏi {c.DisplayType}")
                == DialogResult.No)
            {
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang xóa người dùng");

            var Contractor = (sle_DVTH.GetSelectedDataRow() as ContractorViewModel);

            UserInContractorViewModel uic = new UserInContractorViewModel()
            {
                Id = crUser.UserInContractorId,
                UserId = crUser.Id,
                ContractorName = Contractor.Ten
            };

            var res = await CusHttpClient.InstanceCustomer
                .MPostAsJsonAsync<bool>($"{RouteAPI.USERINCONTRACTOR_CONTROLLER}/{RouteAPI.SUFFIX_DeleteByVm}", uic);

            if (res.MESSAGE_TYPECODE)
            {
                gv_DanhSachNguoiDung.DeleteSelectedRows();
                MessageShower.ShowInformation("Xóa thành công");

                var userInCombobox = ccbb_SelectUsers.Properties.DataSource as BindingList<AppUserViewModel>;
                userInCombobox.Add(crUser);
            }
            else
            {
                MessageShower.ShowError("Xóa thất bại");
            }
            WaitFormHelper.CloseWaitForm();

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


            ContractorViewModel contractor = (sle_DVTH.EditValue as ContractorViewModel);
            UserInContractorViewModel ua = new UserInContractorViewModel()
            {
                Id = crUser.UserInContractorId,
                UserId = crUser.Id,
                IsAdmin = newVal,
                ContractorName = contractor.Ten,
                FieldUpdate = Common.Enums.FieldUpdateEnum.IsAdmin
            };

            ua.SetValueByPropName(contractor.type.GetEnumDescription(), contractor.Code);

            WaitFormHelper.ShowWaitForm("Đang cập nhật");

            var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.USERINCONTRACTOR_CONTROLLER}/{RouteAPI.SUFFIX_Update}", ua);

            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Không thể cập nhật trạng thái!");
                gv_DanhSachNguoiDung.HideEditor();
                WaitFormHelper.CloseWaitForm();
                return;
            }
            WaitFormHelper.CloseWaitForm();
        }
        
        private void sle_DVTH_Popup(object sender, EventArgs e)
        {
            SearchLookUpEdit sle = sender as SearchLookUpEdit;
            PopupSearchLookUpEditForm popup = sle.GetPopupEditForm();
            SearchEditLookUpPopup slep = popup.Controls.OfType<SearchEditLookUpPopup>().FirstOrDefault();
            GridControl gc = slep.Controls.OfType<LayoutControl>().FirstOrDefault().Controls.OfType<GridControl>().FirstOrDefault();
            (gc.MainView as GridView).ExpandAllGroups();
        }

        private async void bt_ThemThanhVien_Click(object sender, EventArgs e)
        {
            string[] selecteds = ccbb_SelectUsers.Properties.Items.Where(x => x.CheckState == CheckState.Checked).Select(x => x.Value.ToString()).ToArray();
            if (!selecteds.Any())
            {
                MessageShower.ShowInformation("Vui lòng chọn thành viên cần thêm");
                bt_ThemThanhVien.Enabled = true;

                return;
            }

            var usersWillAdd = _users.Where(x => selecteds.Contains(x.DisplayInCombobox));


            ContractorViewModel contractor = (sle_DVTH.EditValue as ContractorViewModel);
            var uics = usersWillAdd
                .Select(x => new UserInContractorViewModel()
            {
                Id = x.UserInContractorId,
                UserId = x.Id,
                IsAdmin = false,
                ContractorName = contractor.Ten,
            }).ToList();

            foreach (var uic in uics)
            {
                uic.SetValueByPropName(contractor.type.GetEnumDescription(), contractor.Code);
            }

            WaitFormHelper.ShowWaitForm("Đang thêm người dùng");

            var res = await CusHttpClient.InstanceCustomer
                .MPostAsJsonAsync<bool>($"{RouteAPI.USERINCONTRACTOR_CONTROLLER}/{RouteAPI.SUFFIX_CreateMulti}", uics);

            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Không thể thêm người dùng, Kiểm tra kết nối internet!");
                WaitFormHelper.CloseWaitForm();
                return;
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

                WaitFormHelper.CloseWaitForm();

            }
        }

        private void gc_Users_DataSourceChanged(object sender, EventArgs e)
        {
            gv_DanhSachNguoiDung.ExpandAllGroups();
        }
    }
}
