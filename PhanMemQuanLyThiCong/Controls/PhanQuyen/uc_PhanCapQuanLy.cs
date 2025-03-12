using DevExpress.DataAccess.Native.Web;
using DevExpress.Utils.Menu;
using DevExpress.Xpo;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraTreeList;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.PermissionControl;
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

namespace PhanMemQuanLyThiCong.Controls.PhanQuyen
{
    public partial class uc_PhanCapQuanLy : DevExpress.XtraEditors.XtraUserControl
    {
        const string Route_GetRoleLevelWithRole = "AppRoleLevel/GetRoleLevelsWithRoles";
        List<AppRoleLevelViewModel> _rolesLevel = new List<AppRoleLevelViewModel>();
        public uc_PhanCapQuanLy()
        {
            InitializeComponent();
        }

        private async void uc_PhanCapQuanLy_Load(object sender, EventArgs e)
        {
            var result = await CusHttpClient.InstanceCustomer.MGetAsync<RoleLevelsAndRolesViewModel>(Route_GetRoleLevelWithRole);
            
            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Lỗi tải cấp quản trị:\r\n" + result.MESSAGE_CONTENT);
                Enabled = false;
                return;
            }

            var RoleLevelWithRole = result.Dto;
            var RoleLevel = RoleLevelWithRole.RoleLevels.OrderBy(x => x.Level).ToList();
            var Role = RoleLevelWithRole.Roles;


            var allPermission = BaseFrom.allPermission;

            string notice = string.Empty;


            if (!BaseFrom.IsFullAccess)
            {
                if (!BaseFrom.allPermission.HaveInitProjectPermission)
                {
                    Role = Role.Where(x => allPermission.AllRole.Contains(x.Id)).ToList();
                    RoleLevel = RoleLevel.Where(x => Role.Select(y => y.RoleLevelId).Contains(x.Id)).ToList();
                    lb_notice.Text = "Lưu ý: Bạn không sử dụng bản quyền. Chỉ có thể xem quyền, Không thể thêm và chỉnh sửa!";
                    tl_Role.OptionsBehavior.ReadOnly = true;
                    bt_AddCapQuanTri.Enabled = bt_ThemNhomVaoCapQuanTri.Enabled = bt_XapXep.Enabled = false;
                }
                else
                {
                    lb_notice.Text = "Lưu ý: Bạn chỉ có quyền quản lý người dùng trong  nhóm quyền. Không có quyền thêm nhóm!";
                    bt_AddCapQuanTri.Enabled = bt_ThemNhomVaoCapQuanTri.Enabled = bt_XapXep.Enabled = false;

                }
            }
            
            

            var source = RoleLevel.OrderBy(x => x.Level).Select(x => new PhanCapQuanLy()
            {
                Id = x.Id.ToString(),
                Level = x.Level,
                Ten = x.Name,
                
            }).ToList();

            source.Add(new PhanCapQuanLy()
            {
                Id = "NON",
                LevelCusString = "Nhóm tự do",
                Ten = "Nhóm tự do",
            });

            source.AddRange(Role.Select(x => new PhanCapQuanLy()
            {
                Id = x.Id.ToString(),
                ParentId = (x.RoleLevelId is null) ? "NON" : x.RoleLevelId.ToString(),
                Ten = x.Name
            }));

            _rolesLevel = RoleLevel;
            tl_Role.DataSource = source;
        }

        private void tl_Role_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            DevExpress.XtraTreeList.TreeListHitInfo hitInfo = tl_Role.CalcHitInfo(e.Point);


            var crNode = tl_Role.FocusedNode;
            var crRow = tl_Role.GetFocusedRow() as PhanCapQuanLy;

            if (crNode.Level == 1)
            {
                var item = new DXSubMenuItem("Chuyển đến cấp quản trị khác", Handle_ChuyenDenCapQuanTriKhac);

                foreach (var rl in _rolesLevel.Where(x => x.Id.ToString() != crRow.ParentId))
                {
                    var subItem = new DXMenuItem(rl.Name, async (s, args) =>
                    {
                        var newvm = new AppRoleViewModel()
                        {
                            Id = Guid.Parse(crRow.Id),
                            RoleLevelId = rl.Id,
                        };
                        var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.APPROLE_CONTROLLER}/SetLevel", newvm);


                        if (result.MESSAGE_TYPECODE)
                        {
                            uc_PhanCapQuanLy_Load(null, null);
                        }
                        else
                        {
                            MessageShower.ShowWarning("Lỗi thay đổi cấp quản trị");
                        }
                    });
                    item.Items.Add(subItem);
                };

                if (crRow.ParentId != "NON")
                {
                    var subItem1 = new DXMenuItem($"Không phân cấp", async (s, args) =>
                    {
                        var newvm = new AppRoleViewModel()
                        {
                            Id = Guid.Parse(crRow.Id),
                            RoleLevelId = null,
                        };
                        var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.APPROLE_CONTROLLER}/SetLevel", newvm);


                        if (result.MESSAGE_TYPECODE)
                        {
                            uc_PhanCapQuanLy_Load(null, null);
                        }
                        else
                        {
                            MessageShower.ShowWarning("Lỗi thay đổi cấp quản trị");
                        }
                    });
                    item.Items.Add(subItem1);
                }
                item.Tag = hitInfo.Column;
                e.Menu.Items.Add(item);
            }
        }


        private void Handle_ChuyenDenCapQuanTriKhac(object sender, EventArgs args)
        {
            var crNode = tl_Role.FocusedNode;
            var crRow = tl_Role.GetFocusedRow() as PhanCapQuanLy;
        }

        private void tl_Role_DataSourceChanged(object sender, EventArgs e)
        {
            tl_Role.ExpandAll();
        }

        private void tl_Role_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 2)
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
        }

        private void tl_Role_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            var crNode = e.Node;
            var crRow = tl_Role.GetDataRecordByNode(crNode);
            string fieldName = e.Column.FieldName;

            if (fieldName != colName.FieldName)
            {
                tl_Role.CancelCurrentEdit();
                return;
            }    

            if (crNode.Level == 0)
            {
                
            }

        }

        private void bt_XapXep_Click(object sender, EventArgs e)
        {
            var form = new XtraForm_XapXepCapQuanTri(_rolesLevel);
            var dr = form.ShowDialog();

            if (dr == DialogResult.OK)
            {
                uc_PhanCapQuanLy_Load(null, null);
            }
        }

        private async void bt_AddCapQuanTri_Click(object sender, EventArgs e)
        {
            string NhomQuyenMoi = XtraInputBox.Show("Cấp quản trị mới", "Nhập tên", "");
            if (string.IsNullOrEmpty(NhomQuyenMoi))
            {
                return;
            }
            AppRoleLevelViewModel appRoleViewModel = new AppRoleLevelViewModel()
            {
                Id = _rolesLevel.Max(x => x.Id) + 1,
                Level = _rolesLevel.Max(x => x.Level) + 1,
                Name = NhomQuyenMoi,
                Description = NhomQuyenMoi,
            };



            WaitFormHelper.ShowWaitForm("Đang Thêm cấp quản trị");

            var response = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($@"{RouteAPI.APPROLELEVEL_CONTROLLER}\{RouteAPI.SUFFIX_Create}", appRoleViewModel);
            WaitFormHelper.CloseWaitForm();

            if (!response.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Thêm nhóm quyền không thành công");
               
            }
            else
            {
                uc_PhanCapQuanLy_Load(null, null);
            }
        }

        private async void bt_ThemNhomVaoCapQuanTri_Click(object sender, EventArgs e)
        {
            var crNode = tl_Role.FocusedNode;
            if (crNode?.Level != 0)
            {
                MessageShower.ShowWarning("Vui lòng chọn 1 Cấp quản trị từ danh sách phía trên để thêm nhóm quyền");
                return;
            }
            string NhomQuyenMoi = XtraInputBox.Show("Nhóm quyền mới", "Nhập nhóm quyền mới", "");
            if (string.IsNullOrEmpty(NhomQuyenMoi))
            {
                return;
            }

            var crRow = tl_Role.GetFocusedRow() as PhanCapQuanLy;

            
            bool isParse = int.TryParse(crRow.Id, out int id);
            int? parentId = null;

            if (isParse)
            {
                parentId = id;
            }

            AppRoleViewModel appRoleViewModel = new AppRoleViewModel()
            {
                Id = Guid.NewGuid(),
                RoleLevelId = parentId,
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
                var source = tl_Role.DataSource as List<PhanCapQuanLy>;
                source.Add(new PhanCapQuanLy()
                {
                    Id = appRoleViewModel.Id.ToString(),
                    ParentId = crRow.Id,
                    Ten = NhomQuyenMoi
                });

                tl_Role.RefreshDataSource();
            }
            else
            {
                MessageShower.ShowError("Thêm nhóm quyền thất bại");
            }
        }

        private async void tl_Role_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

            var crNode = tl_Role.FocusedNode;
            var crRow = tl_Role.GetDataRecordByNode(crNode) as PhanCapQuanLy;
            string fieldName = tl_Role.FocusedColumn.FieldName;

            if (fieldName != colName.FieldName || crRow.Id == "NON")
            {
               e.Valid = false;
                if (crRow.Id == "NON")
                {
                    e.ErrorText = "Không được thay đổi nhóm này";
                }    
               return;
            }


            var oldVal = tl_Role.ActiveEditor.OldEditValue.ToString().Trim();
            var newVal = tl_Role.ActiveEditor.EditValue.ToString().Trim();

            if (oldVal != newVal)
            {
                var dr = MessageShower.ShowYesNoQuestion($"Bạn có muốn đổi tên nhóm từ \"{oldVal}\" thành \"{newVal}\"");
                
                if (dr == DialogResult.Yes)
                {
                    if (crNode.Level == 0)
                    {
                        var vm = new AppRoleLevelViewModel()
                        {
                            Id = int.Parse(crRow.Id),
                            Name = newVal
                        };

                        var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.APPROLELEVEL_CONTROLLER}/Rename", vm);
                        
                        if (result.MESSAGE_TYPECODE)
                        {
                            MessageShower.ShowInformation("Đã đổi tên");
                            _rolesLevel.Single(x => x.Id == vm.Id).Name = newVal;
                        }
                        else
                        {
                            MessageShower.ShowInformation("Không thể đổi tên");
                        }    
                    }
                    else if (crNode.Level == 1)
                    {
                        var vm = new AppRoleViewModel()
                        {
                            Id = Guid.Parse(crRow.Id),
                            Name = newVal
                        };

                        var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.APPROLE_CONTROLLER}/Rename", vm);

                        if (result.MESSAGE_TYPECODE)
                        {
                            MessageShower.ShowInformation("Đã đổi tên");
                        }
                        else
                        {
                            MessageShower.ShowInformation("Không thể đổi tên");
                        }
                    }
                }
            }
        }

        private async void repobt_Xoa_Click(object sender, EventArgs e)
        {
            var crNode = tl_Role.FocusedNode;
            var crRow = tl_Role.GetDataRecordByNode(crNode) as PhanCapQuanLy;

            if (crRow.Id == "NON")
            {
                MessageShower.ShowWarning("Đây là nhóm tự do được tạo tự động nên không thể xóa");
                return;
            }

            MessageShower.ShowYesNoQuestion($"Bạn có chắc chắn muốn xóa nhóm \"{crRow.Ten}\"");

            if (crNode.Level == 0)
            {
                var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.APPROLELEVEL_CONTROLLER}/Delete", int.Parse(crRow.Id));

                if (result.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowInformation("Đã xóa");
                    tl_Role.DeleteNode(crNode);
                }
                else
                {
                    MessageShower.ShowInformation("Không thể xóa");
                }
            }
            else if (crNode.Level == 1)
            {
                var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.APPROLE_CONTROLLER}/Delete", Guid.Parse(crRow.Id));

                if (result.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowInformation("Đã xóa");
                    tl_Role.DeleteNode(crNode);
                }
                else
                {
                    MessageShower.ShowInformation("Không thể xóa");
                }
            }
        }

        private void tl_Role_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeList treeList = sender as TreeList;
                TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
                if (info.Node != null)
                {
                    treeList.FocusedNode = info.Node;
                }
            }
        }
    }
}
