using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Common.Helper;
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
using DevExpress.XtraSpreadsheet.Internal;
using PermissionApp;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Dto;
using PM360.Common.Helper;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Mvvm.Native;
using DevExpress.XtraPrinting.Native;
using System.Diagnostics.Contracts;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class uc_UserInTask : DevExpress.XtraEditors.XtraUserControl
    {
        List<AppUserViewModel> _users = new List<AppUserViewModel>();
        List<RoleDetailViewModel> _roleDetails = new List<RoleDetailViewModel>();
        string _DAId;
        public uc_UserInTask(string DAId)
        {
            _DAId = DAId;
            InitializeComponent();
        }

        private async void uc_UserInTask_Load(object sender, EventArgs e)
        {
            lb_notice.Visible = false;
            await InitCongViec();
        }

        private async Task<List<LoaiCongViecViewModel>> InitCongViec()
        {
            List<LoaiCongViecViewModel> cvs = new List<LoaiCongViecViewModel>
            {

                new LoaiCongViecViewModel()
                {
                    Type = Common.Enums.TypeTask.GIAOVIECDUAN,
                    Description = "Công việc phần giao việc dự án"
                },                
                
                new LoaiCongViecViewModel()
                {
                    Type = Common.Enums.TypeTask.GIAOVIECTHICONG,
                    Description = "Công việc từ phần giao việc thi công"
                },                
                
                new LoaiCongViecViewModel()
                {
                    Type = Common.Enums.TypeTask.TIENDOKEHOACH,
                    Description = "Công việc từ phần tiến độ kế hoạch"
                },
            };
            lke_LoaiCongTac.Properties.DataSource = cvs;
            //lke_LoaiCongTac.EditValue = cvs.First();
            var response = await CusHttpClient.InstanceCustomer
                        .MGetAsync<List<ContractorViewModel>>($"{RouteAPI.TongDuAn_CONTROLLER}/GetAllConstrutorByPrjAndUser/{_DAId}/false");

            //var users = await UserHelper.GetAllUserInCusSever();
            if (!response.MESSAGE_TYPECODE)
            {   
                MessageShower.ShowError(MessageText.SERVER_FAIL);
            }
            else
            {
                this.Enabled = true;
                //sle_Users.Properties.DataSource = users;
                sle_DVTH.Properties.DataSource = response.Dto;
                sle_DVTH.EditValue = response.Dto.FirstOrDefault();

            }
            return cvs;
        }

        private void cbb_LoaiCongTac_SelectedIndexChanged(object sender, EventArgs e)
        {
            tl_NhomQuyen.DataSource = null;
        }

        private async void lke_LoaiCongTac_EditValueChanged(object sender, EventArgs e)
        {
            ContractorViewModel c = sle_DVTH.EditValue as ContractorViewModel;
            if (c == null)
                return;
            LoaiCongViecViewModel crCV = lke_LoaiCongTac.EditValue as LoaiCongViecViewModel;

            switch (crCV.Type)
            {
                /*                //case TypeTask.TASK:
                                //    lci_DonViThucHien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                                //    break;*/
                case TypeTask.GIAOVIECDUAN:
                    lci_DonViThucHien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //api = $"{RouteAPI.TongDuAn_CONTROLLER}/GetCongTacGiaoViecDuAnWithRolesDeTail/{_DAId}/{crUser.Id}";
                    break;
                case TypeTask.GIAOVIECTHICONG:
                    lci_DonViThucHien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //api = $"{RouteAPI.TongDuAn_CONTROLLER}/GetCongTacGiaoViecThiCongWithRolesDeTail/{_DAId}/{crUser.Id}/{contractor.colFK}/{contractor.Code}";
                    break;
                case TypeTask.TIENDOKEHOACH:
                    lci_DonViThucHien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //api = $"{RouteAPI.TongDuAn_CONTROLLER}/GetCongTacTienDoKeHoachWithRolesDeTail/{_DAId}/{crUser.Id}/{contractor.colFK}/{contractor.Code}";
                    break;
                default:
                    return;
            }

            if (lci_DonViThucHien.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                sle_DVTH_EditValueChanged(null, null);
            }

            else
            {
                var users = await UserHelper.GetAllUserInCusSever();
                
                if (users is null)
                {
                    MessageShower.ShowError(MessageText.SERVER_FAIL);
                    tl_NhomQuyen.Enabled = false;
                }
                else
                {
                    tl_NhomQuyen.Enabled = true;
                    sle_Users.Properties.DataSource = users;
                }
            }

            //loadPermission(); 
        }

        private async void loadPermission()
        {
            LoaiCongViecViewModel crCV = lke_LoaiCongTac.EditValue as LoaiCongViecViewModel;
            ContractorViewModel contractor = sle_DVTH.EditValue as ContractorViewModel;
            AppUserViewModel crUser = sle_Users.EditValue as AppUserViewModel;

            if (crCV is null || contractor is null || crUser is null)
            {
                tl_NhomQuyen.DataSource = null;
                tl_NhomQuyen.Enabled = false;
                return;
            }    

            string api;

            switch (crCV.Type)
            {
                /*                //case TypeTask.TASK:
                                //    lci_DonViThucHien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                                //    break;*/
                case TypeTask.GIAOVIECDUAN:
                    //lci_DonViThucHien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    api = $"{RouteAPI.TongDuAn_CONTROLLER}/GetCongTacGiaoViecDuAnWithRolesDeTail/{_DAId}/{crUser.Id}";
                    break;
                case TypeTask.GIAOVIECTHICONG:
                    //lci_DonViThucHien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    api = $"{RouteAPI.TongDuAn_CONTROLLER}/GetCongTacGiaoViecThiCongWithRolesDeTail/{_DAId}/{crUser.Id}/{contractor.colFK}/{contractor.Code}";
                    break;
                case TypeTask.TIENDOKEHOACH:
                    //lci_DonViThucHien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    api = $"{RouteAPI.TongDuAn_CONTROLLER}/GetCongTacTienDoKeHoachWithRolesDeTail/{_DAId}/{crUser.Id}/{contractor.colFK}/{contractor.Code}";
                    break;
                default:
                    return;
            }

            WaitFormHelper.ShowWaitForm("Đang tải công tác!");
            var response = await CusHttpClient.InstanceCustomer
                                .MGetAsync<CongTacBriefWithRoleDetailViewModel>(api);

            if (!response.MESSAGE_TYPECODE)
            {
                tl_NhomQuyen.DataSource = null;
                MessageShower.ShowError(MessageText.SERVER_FAIL);
                WaitFormHelper.CloseWaitForm();
                return;
            }
            _roleDetails = response.Dto.RoleDetails; 

            var tbl = TablePermission.Instance.CreatedTablePermissionCongTac(response.Dto, crCV.ColFK);
            tl_NhomQuyen.DataSource = tbl;

            tl_NhomQuyen.Enabled = true;
            TreeListHelper.SetParentNotTrueFalseStateByCommand(tl_NhomQuyen);

            WaitFormHelper.CloseWaitForm();
        }

        private void repoCheckEdit_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void tl_NhomQuyen_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (!bool.TryParse(e.Value.ToString(), out bool newVal))
            {
                MessageShower.ShowError("Không thể thay đổi giá trị này");
                tl_NhomQuyen.CancelCurrentEdit();
                return;
            }
            //tl_NhomQuyen.CloseEditor();
            //return;
            //this. = false;
            var crDVTH = sle_DVTH.EditValue as ContractorViewModel;
            var crLoaiCT = lke_LoaiCongTac.EditValue as LoaiCongViecViewModel;
            var node = tl_NhomQuyen.FocusedNode;
            
            //string FunctionId = row["Id"].ToString();
            
            var crUser = sle_Users.EditValue as AppUserViewModel;

            string CmdId = tl_NhomQuyen.FocusedColumn.FieldName;

            if (!node.HasChildren)
            {
                var row = ((DataRowView)tl_NhomQuyen.GetDataRecordByNode(node)).Row;
                var crRoleDetails = _roleDetails
                    .Where(x => x.GetValueByPropName(crLoaiCT.ColFK).ToString() == row["Id"].ToString()
                    && x.UserId == crUser.Id && x.CommandId == CmdId).SingleOrDefault();


                if (newVal)
                {
                    if (crRoleDetails is null)
                    {
                        crRoleDetails = new RoleDetailViewModel()
                        {
                            Id = Guid.NewGuid(),
                            CommandId = CmdId,
                            UserId = crUser.Id,
                        };
                        crRoleDetails.SetValueByPropName(crLoaiCT.ColFK, row["Id"]);
                    }

                    var res = await CusHttpClient.InstanceCustomer
                        .MPostAsJsonAsync<bool>($@"{RouteAPI.ROLEDETAIL_CONTROLLER}\{RouteAPI.SUFFIX_Create}", crRoleDetails);

                    if (!res.MESSAGE_TYPECODE)
                    {
                        MessageShower.ShowError("Lỗi cập nhật quyền!");
                        tl_NhomQuyen.CancelCurrentEdit();
                        //this.Enabled = true;
                        return;
                    }

                    if (!_roleDetails.Contains(crRoleDetails))
                        _roleDetails.Add(crRoleDetails);

                    //this.Enabled = true;
                }
                else
                {
                    if (crRoleDetails is null)
                    {
                        MessageShower.ShowError("Lỗi đồng bộ dữ liệu!");
                        tl_NhomQuyen.CancelCurrentEdit();
                        return;
                    }

                    var res = await CusHttpClient.InstanceCustomer
                                .MPostAsJsonAsync<bool>($@"{RouteAPI.ROLEDETAIL_CONTROLLER}\{RouteAPI.SUFFIX_Delete}", crRoleDetails.Id);

                    _roleDetails.Remove(crRoleDetails);

                    if (!res.MESSAGE_TYPECODE)
                    {
                        MessageShower.ShowError("Lỗi cập nhật quyền!");
                        tl_NhomQuyen.CancelCurrentEdit();
                        //this.Enabled = true;
                        return;
                    }
                }
                node.SetValue(CmdId, newVal);

            }
            else
            {
                var children = TreeListHelper.GetAllChildNodeAllLevel(node);
                var nodeDeepLevel = children.Where(x => !x.HasChildren).ToList();

                List<RoleDetailViewModel> RDs = new List<RoleDetailViewModel>();
                foreach (var crNode in nodeDeepLevel)
                {
                    var row = ((DataRowView)tl_NhomQuyen.GetDataRecordByNode(crNode)).Row;

                    if ((bool)crNode.GetValue(CmdId) == newVal)
                        continue;

                    
                    var crRoleDetails = _roleDetails
                                        .Where(x => x.GetValueByPropName(crLoaiCT.ColFK).ToString() == row["Id"].ToString()
                                        && x.UserId == crUser.Id && x.CommandId == CmdId).SingleOrDefault();

                    if (crRoleDetails is null)
                    {
                        crRoleDetails = new RoleDetailViewModel()
                        {
                            Id = Guid.NewGuid(),
                            CommandId = CmdId,
                            UserId = crUser.Id,
                        };
                        crRoleDetails.SetValueByPropName(crLoaiCT.ColFK, row["Id"]);
                        
                    }
                    RDs.Add(crRoleDetails);
                }

                var ExistingIds = _roleDetails.Select(x => x.Id).ToArray();
                if (newVal)
                {
                    var res = await CusHttpClient.InstanceCustomer
                        .MPostAsJsonAsync<bool>($@"{RouteAPI.ROLEDETAIL_CONTROLLER}\{RouteAPI.SUFFIX_AddOrUpdateMulti}", RDs);


                    if (!res.MESSAGE_TYPECODE)
                    {
                        MessageShower.ShowError("Lỗi cập nhật quyền!");
                        tl_NhomQuyen.CancelCurrentEdit();
                        return;
                    }
                    _roleDetails.AddRange(RDs.Where(x => !ExistingIds.Contains(x.Id)));
                }
                else
                {
                    var res = await CusHttpClient.InstanceCustomer
                        .MPostAsJsonAsync<bool>($@"{RouteAPI.ROLEDETAIL_CONTROLLER}\{RouteAPI.SUFFIX_DeleteMulti}", RDs.Select(x => x.Id));

                    if (!res.MESSAGE_TYPECODE)
                    {
                        MessageShower.ShowError("Lỗi cập nhật quyền!");
                        tl_NhomQuyen.CancelCurrentEdit();
                        return;
                    }
                    
                    foreach (var roleDetail in RDs)
                    {
                        if (_roleDetails.Contains(roleDetail))
                            _roleDetails.Remove(roleDetail);
                    }
                }

                node.SetValue(CmdId, newVal);
                foreach (var child in children)
                {
                    child.SetValue(CmdId, newVal);
                }
                //tl_NhomQuyen.EndUpdate();
            }

            TreeListHelper.SetParentNotTrueFalseStateByCommand(tl_NhomQuyen);
        }

        private void sle_Users_EditValueChanged(object sender, EventArgs e)
        {
            loadPermission();
        }

        private async void sle_DVTH_EditValueChanged(object sender, EventArgs e)
        {
            ContractorViewModel c = sle_DVTH.EditValue as ContractorViewModel;
            if (c is null)
                return;

            var response = await CusHttpClient.InstanceCustomer
                            .MGetAsync<List<AppUserViewModel>>
                            ($"{RouteAPI.USERINCONTRACTOR_CONTROLLER}/GetUsersByContractorId/{c.type.GetEnumDescription()}/{c.Code}");

            if (!response.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError(MessageText.SERVER_FAIL);
                tl_NhomQuyen.Enabled = false;
                return;
            }
            var allPermission = BaseFrom.allPermission;
            string notice = string.Empty;
            if (!BaseFrom.IsFullAccess
                && !allPermission.HaveInitProjectPermission
                && !allPermission.AllProjectThatUserIsAdmin.Contains(_DAId)
                && !allPermission.AllContractorThatUserIsAdmin.Contains(c.Code))
            {
                notice = "Lưu ý: Bạn chỉ có quyền của tài khoản! không có quyền chỉnh sửa";
                tl_NhomQuyen.OptionsBehavior.ReadOnly = true;
                response.Dto = response.Dto.Where(x => x.Id == BaseFrom.BanQuyenKeyInfo.UserId).ToList();
                //ccbb_SelectUsers.ReadOnly = true;
                //bt_ThemThanhVien.Enabled = false;
                //gv_DanhSachNguoiDung.OptionsBehavior.ReadOnly = true;
            }

            if (!string.IsNullOrEmpty(notice))
            {
                lb_notice.Text = notice;
                lb_notice.Visible = true;
            }

            
            if (response.MESSAGE_TYPECODE)
            {
                tl_NhomQuyen.Enabled = true;
                sle_Users.Properties.DataSource = response.Dto;
            }

        }

        private void tl_NhomQuyen_DataSourceChanged(object sender, EventArgs e)
        {
            tl_NhomQuyen.ExpandAll();
        }

        private void sle_DVTH_Popup(object sender, EventArgs e)
        {
            SearchLookUpEdit sle = sender as SearchLookUpEdit;
            PopupSearchLookUpEditForm popup = sle.GetPopupEditForm();
            SearchEditLookUpPopup slep = popup.Controls.OfType<SearchEditLookUpPopup>().FirstOrDefault();
            GridControl gc = slep.Controls.OfType<LayoutControl>().FirstOrDefault().Controls.OfType<GridControl>().FirstOrDefault();
            (gc.MainView as GridView).ExpandAllGroups();
        }
    }
}
