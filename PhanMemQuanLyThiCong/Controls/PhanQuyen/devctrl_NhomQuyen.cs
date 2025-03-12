using DevExpress.DevAV.Chat.Model;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraRichEdit.API.Native.Implementation;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraWaitForm;
using PermissionApp;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Dto;
using PhanMemQuanLyThiCong.Model;
using PM360.Common.Helper;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.Dto;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Ocsp;
using AutoMapper.Mappers;

namespace PhanMemQuanLyThiCong
{
    public partial class devctrl_NhomQuyen : DevExpress.XtraEditors.XtraUserControl
    {
        List<AppUserViewModel> _users;

        public devctrl_NhomQuyen()
        {
            InitializeComponent();
            fcn_LoadNhomQuyen();
        }

        private async void fcn_LoadNhomQuyen()
        {
            WaitFormHelper.ShowWaitForm("Đang tải nhóm quyền");

            var response = await CusHttpClient.InstanceCustomer.MGetAsync<List<AppRoleViewModel>>($@"{RouteAPI.APPROLE_CONTROLLER}\{RouteAPI.SUFFIX_GetAll}");
            WaitFormHelper.CloseWaitForm();
            if (response.MESSAGE_TYPECODE)
            {
                var allPermission = BaseFrom.allPermission;

                string notice = string.Empty;
                if (!BaseFrom.IsFullAccess)
                {
                    //lb_notice.Visible = true;
                    //bt_ThemNhomQuyen.Enabled = false;
                    tl_NhomQuyen.OptionsBehavior.ReadOnly = true;

                    if (allPermission.HaveInitProjectPermission)
                    {
                        notice = "Lưu ý: Bạn chỉ có quyền quản lý người dùng trong  nhóm quyền. Không có quyền thêm nhóm!";
                    }
                    else
                    {
                        response.Dto = response.Dto.Where(x => allPermission.AllRole.Contains(x.Id)).ToList();
                        notice = "Lưu ý: Bạn không sử dụng bản quyền. Chỉ có thể xem quyền, Không thể thêm và chỉnh sửa!";
                        gv_DanhSachNguoiDung.OptionsBehavior.ReadOnly = true;
                    }
                }

                lke_NhomQuyen.Properties.DataSource = new BindingList<AppRoleViewModel>(response.Dto);
                if (response.Dto.Any())
                {
                    lke_NhomQuyen.EditValue = response.Dto.First();
                    //lke_NhomQuyen_EditValueChanged(null, null);
                }
            }
            else
            {
                //lke_NhomQuyen.Properties.DataSource = new List<AppRoleViewModel>(); 
                MessageShower.ShowError("Không thể tải nhóm quyền, vui lòng kiểm tra kết nối");
                this.Enabled = false;
            }
        }

        private async void bt_ThemNhomQuyen_Click(object sender, EventArgs e)
        {
            string NhomQuyenMoi = XtraInputBox.Show("Nhóm quyền mới", "Nhập nhóm quyền mới", "");
            if (string.IsNullOrEmpty(NhomQuyenMoi))
            {
                return;
            }
            AppRoleViewModel appRoleViewModel = new AppRoleViewModel()
            {
                Id = Guid.NewGuid(),
                Name = NhomQuyenMoi,
                Description = NhomQuyenMoi,
            };

            DialogResult dr = MessageShower.ShowYesNoCancelQuestionWithCustomText("Chọn vị trí thêm tài khoản?", "Lựa chọn", "Nội bộ", "Khách");

            switch (dr)
            {
                case DialogResult.Yes://Nội bộ
                    appRoleViewModel.Type = AccountType.INTERNAL;
                    break;
                case DialogResult.No:
                    appRoleViewModel.Type = AccountType.EXTERNAL;
                    break;

                default: return;
            }


            WaitFormHelper.ShowWaitForm("Đang Thêm nhóm quyền");

            var response = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($@"{RouteAPI.APPROLE_CONTROLLER}\{RouteAPI.SUFFIX_Create}", appRoleViewModel);
            WaitFormHelper.CloseWaitForm();

            if (response.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Thêm nhóm quyền thành công");

                BindingList<AppRoleViewModel> lsRoles = lke_NhomQuyen.Properties.DataSource as BindingList<AppRoleViewModel>;
                lsRoles.Add(appRoleViewModel);
                lke_NhomQuyen.EditValue = appRoleViewModel;
            }
            else
            {
                MessageShower.ShowError("Thêm nhóm quyền thất bại");
            }
        }

        private async void lke_NhomQuyen_EditValueChanged(object sender, EventArgs e)
        {
            AppRoleViewModel arvm = lke_NhomQuyen.GetSelectedDataRow() as AppRoleViewModel;

            if (arvm == null)
            {
                tl_NhomQuyen.OptionsBehavior.ReadOnly = true;
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang tải chi tiết");
            var response = await CusHttpClient.InstanceCustomer
                .MGetAsync<PermissionsAndUsersInRole>($@"{RouteAPI.APPROLE_GetPermissionAndUserByRole}\{arvm.Id}");
            
            if (_users is null)
                _users = await UserHelper.GetAllUserInCusSever();

            if (!response.MESSAGE_TYPECODE || _users is null)
            {
                tl_NhomQuyen.OptionsBehavior.ReadOnly = true;
                MessageShower.ShowError("Lỗi tải thông tin!");
                WaitFormHelper.CloseWaitForm();
                return;
            }
            else
            {
                var allPermission = BaseFrom.allPermission;
                string notice = string.Empty;
                if (!BaseFrom.IsFullAccess
                    && !allPermission.HaveInitProjectPermission
                    && !allPermission.AllRoleThatUserIsAdmin.Contains(arvm.Id))
                {
                    notice = "Lưu ý: Bạn chỉ có quyền xem, không có quyền thêm bớt thành viên!";
                    bt_ThemThanhVien.Enabled = false;
                    ccbb_SelectUsers.Enabled = false;
                    gv_DanhSachNguoiDung.OptionsBehavior.ReadOnly = true;
                }    
                else if (allPermission.AllRoleThatUserIsAdmin.Contains(arvm.Id))
                {
                    notice = "Lưu ý: Bạn là admin của nhóm đã chọn. Có quyền thêm xóa thành viên!";

                }

/*                if (!string.IsNullOrEmpty(notice))
                {
                    lb_notice.Text = notice;
                    lb_notice.Visible = true;
                }

                lb_notice.Text = notice;*/
                _users = _users.OrderBy(x => x.DisplayInCombobox).ToList();

                tl_NhomQuyen.OptionsBehavior.ReadOnly = false;
                DataTable dt = TablePermission.Instance.CreatedTablePermissionTabMenu(response.Dto.Permissions, arvm.Type);
                tl_NhomQuyen.DataSource = dt;

                ce_CreateProject.EditValueChanging -= ce_CreateProject_EditValueChanging;
                ce_CreateProject.Checked = response.Dto.Permissions.Any(x => x.FunctionId == FunctionCode.EXTERNALCREATEPROJECT.GetEnumName() && x.CommandId == CommandCode.Add.GetEnumName());
                ce_CreateProject.EditValueChanging += ce_CreateProject_EditValueChanging;



                var parentsNode = tl_NhomQuyen.Nodes.Where(x => x.HasChildren).ToList();
                foreach (var parentNode in parentsNode)
                {
                    foreach (string cmd in Enum.GetNames(typeof(CommandCode)))
                    {
                        int trueNodesCount = parentNode.Nodes.Count(x => (bool)x.GetValue(cmd) == true);

                        if (trueNodesCount == parentNode.Nodes.Count())
                            parentNode.SetValue(cmd, true);
                        else if (trueNodesCount > 0)
                            parentNode.SetValue(cmd, null);
                        else
                            parentNode.SetValue(cmd, false);
                    }
                }

                gc_Users.DataSource = new BindingList<AppUserViewModel>(response.Dto.Users);

                var idUsersAdded = response.Dto.Users.Select(x => x.Id);

                ccbb_SelectUsers.Properties.DataSource = new BindingList<AppUserViewModel>(_users.Where(x => !idUsersAdded.Contains(x.Id)).ToList()); //UserForCheckBox

                tl_NhomQuyen.ExpandAll();
            }
            WaitFormHelper.CloseWaitForm();
        }

        private async void tl_NhomQuyen_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (!bool.TryParse(e.Value.ToString(), out bool newVal))
            {
                MessageShower.ShowError("Không thể thay đổi giá trị này");
                tl_NhomQuyen.CancelCurrentEdit();
                return;
            }    

            var node = tl_NhomQuyen.FocusedNode;
            var row = ((DataRowView)tl_NhomQuyen.GetDataRecordByNode(node)).Row;
            string CmdId = tl_NhomQuyen.FocusedColumn.FieldName;
            string FunctionId = row["Id"].ToString();
            Guid RoleId = (lke_NhomQuyen.GetSelectedDataRow() as AppRoleViewModel).Id;

            var crPermission = new PermissionViewModel()
            {
                CommandId = CmdId,
                FunctionId = FunctionId,
                RoleId = RoleId
            };
            
            string suffix = (newVal) ? RouteAPI.SUFFIX_Create : RouteAPI.SUFFIX_DeleteByVm;
            WaitFormHelper.ShowWaitForm("Đang cập nhật");
            var response = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($@"{RouteAPI.PERMISSION_CONTROLLER}\{suffix}", crPermission);
            WaitFormHelper.CloseWaitForm();

            if (!response.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Lỗi cập nhật quyền!");
                tl_NhomQuyen.CancelCurrentEdit();
                return;
            }
            else
            {
                tl_NhomQuyen.BeginUpdate();
                //tl_NhomQuyen.CloseEditor();
                node.SetValue(CmdId, newVal);

                if (node.HasChildren)
                {
                    foreach (TreeListNode childNode in node.Nodes)
                    {
                        childNode.SetValue(CmdId, newVal);
                    }
                }
                else if (node.ParentNode != null)
                {
                    if (!node.ParentNode.Nodes.Any(x => (bool)x.GetValue(CmdId) != newVal))
                    {
                        node.ParentNode.SetValue(CmdId, newVal);
                    }
                    else
                    {
                        node.ParentNode.SetValue(CmdId, null);
                    }
                }
                tl_NhomQuyen.EndUpdate();
            }
        }


        private void tl_NhomQuyen_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {

        }

        private void tl_NhomQuyen_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            
        }

        private void tl_NhomQuyen_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            TreeList treeList = sender as TreeList;

            if (e.Node.Level == 0)
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private async void bt_ThemThanhVien_Click(object sender, EventArgs e)
        {
            string[] selecteds = ccbb_SelectUsers.Properties.Items.Where(x => x.CheckState == CheckState.Checked).Select(x => x.Value.ToString()).ToArray();
            if (!selecteds.Any())
            {
                MessageShower.ShowInformation("Vui lòng chọn thành viên cần thêm");
                return;
            }

            var usersWillAdd = _users.Where(x => selecteds.Contains(x.DisplayInCombobox));

            var AppRoleVM = (lke_NhomQuyen.GetSelectedDataRow() as AppRoleViewModel);
            Guid RoleId = AppRoleVM.Id;

            var uirs = usersWillAdd.Select(x => new UserInRoleViewModel()
                        {
                            UserId = x.Id,
                            RoleId = RoleId,
                            IsAdmin = false,
                            RoleName = AppRoleVM.Name
                        });

            WaitFormHelper.ShowWaitForm("Đang thêm người dùng");

            var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.USERINROLE_CONTROLLER}/{RouteAPI.SUFFIX_CreateMulti}", uirs);

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

        private async void repoBt_Xoa_Click(object sender, EventArgs e)
        {

            var crUser = gv_DanhSachNguoiDung.FocusedRowObject as AppUserViewModel;

            var approle = (lke_NhomQuyen.GetSelectedDataRow() as AppRoleViewModel);
  
            
            Guid RoleId = approle.Id;

            var allPermission = BaseFrom.allPermission;
            
            if (!BaseFrom.IsFullAccess
                && !allPermission.HaveInitProjectPermission
                && allPermission.AllRoleThatUserIsAdmin.Contains(RoleId)
                && crUser.IsAdmin)
            {
                MessageShower.ShowError("Bạn không thể xóa admin của nhóm. Chỉ có thể xóa thành viên thông thường");
                return;
            }




            UserInRoleViewModel uip = new UserInRoleViewModel()
            {
                UserId = crUser.Id,
                RoleId = RoleId,
                RoleName = approle.Name
            };

            WaitFormHelper.ShowWaitForm("Đang cập nhật");

            var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.USERINROLE_CONTROLLER}/{RouteAPI.SUFFIX_DeleteByVm}", uip);

            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Xóa thất bại!");
                gv_DanhSachNguoiDung.ActiveEditor.EditValue = gv_DanhSachNguoiDung.ActiveEditor.OldEditValue;
                WaitFormHelper.CloseWaitForm();
                return;
            }
            MessageShower.ShowInformation("Đã xóa người dùng");

            var userInCombobox = ccbb_SelectUsers.Properties.DataSource as BindingList<AppUserViewModel>;
            var userInGc = gc_Users.DataSource as BindingList<AppUserViewModel>;

            userInCombobox.Add(crUser);
            ccbb_SelectUsers.Properties.DataSource = new BindingList<AppUserViewModel>(userInCombobox.OrderBy(x => x.DisplayInCombobox).ToList());
            userInGc.Remove(crUser);

            WaitFormHelper.CloseWaitForm();

        }

        private void repobt_IsAdmin_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private async void gv_DanhSachNguoiDung_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var crUser = gv_DanhSachNguoiDung.FocusedRowObject as AppUserViewModel;
            if (!bool.TryParse(e.Value.ToString(), out bool newVal) || e.Column.FieldName != nameof(AppUserViewModel.IsAdmin))
            {
                MessageShower.ShowError("Không thể thay đổi giá trị này");
                gv_DanhSachNguoiDung.ActiveEditor.EditValue = gv_DanhSachNguoiDung.ActiveEditor.OldEditValue;
                return;
            }

            var approle = (lke_NhomQuyen.GetSelectedDataRow() as AppRoleViewModel);
            Guid RoleId = approle.Id;
            UserInRoleViewModel ua = new UserInRoleViewModel()
            {
                UserId = crUser.Id,
                RoleId = RoleId,
                IsAdmin = newVal,
                RoleName = approle.Name,
                FieldUpdate = FieldUpdateEnum.IsAdmin
            };

            WaitFormHelper.ShowWaitForm("Đang cập nhật");

            var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.USERINROLE_CONTROLLER}/{RouteAPI.SUFFIX_Update}", ua);

            if (!res.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Không thể cập nhật trạng thái!");
                gv_DanhSachNguoiDung.ActiveEditor.EditValue = gv_DanhSachNguoiDung.ActiveEditor.OldEditValue;
                WaitFormHelper.CloseWaitForm();
                return;
            }
            WaitFormHelper.CloseWaitForm();
        }

        private async void gv_DanhSachNguoiDung_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            
        }

        private void cbb_LoaiNhom_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void lke_NhomQuyen_Popup(object sender, EventArgs e)
        {
            SearchLookUpEdit sle = sender as SearchLookUpEdit;
            PopupSearchLookUpEditForm popup = sle.GetPopupEditForm();
            SearchEditLookUpPopup slep = popup.Controls.OfType<SearchEditLookUpPopup>().FirstOrDefault();
            GridControl gc = slep.Controls.OfType<LayoutControl>().FirstOrDefault().Controls.OfType<GridControl>().FirstOrDefault();
            (gc.MainView as GridView).ExpandAllGroups();
        }

        private void gc_Users_DataSourceChanged(object sender, EventArgs e)
        {
            gv_DanhSachNguoiDung.ExpandAllGroups();
        }

        private async void ce_CreateProject_CheckedChanged(object sender, EventArgs e)
        {


            

        }

        private async void ce_CreateProject_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var newVal = e.NewValue as bool? == true;
            if (newVal)
            {
                DialogResult dr = MessageShower.ShowYesNoQuestion("Mọi người trong nhóm này sẽ đều có quyền tạo dự án. Bạn có muốn tiếp tục không?");
                if (dr != DialogResult.Yes)
                {
                    e.Cancel = true; ;
                }
            }

            var approle = (lke_NhomQuyen.GetSelectedDataRow() as AppRoleViewModel);
            Guid RoleId = approle.Id;
            var crPermission = new PermissionViewModel()
            {
                CommandId = Enum.GetName(typeof(CommandCode), CommandCode.Add),
                FunctionId = Enum.GetName(typeof(FunctionCode), FunctionCode.EXTERNALCREATEPROJECT),
                RoleId = RoleId
            };

            string suffix = (newVal) ? RouteAPI.SUFFIX_Create : RouteAPI.SUFFIX_DeleteByVm;
            WaitFormHelper.ShowWaitForm("Đang cập nhật");
            var response = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($@"{RouteAPI.PERMISSION_CONTROLLER}\{suffix}", crPermission);
            WaitFormHelper.CloseWaitForm();
            if (!response.MESSAGE_TYPECODE)
            {
                e.Cancel = true;
                MessageShower.ShowError("Cập nhật không thành công\r\n" + response.MESSAGE_CONTENT);
                return;
            }

        }
    }
}
