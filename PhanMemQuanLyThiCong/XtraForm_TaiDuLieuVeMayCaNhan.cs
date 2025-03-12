using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraTreeList;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Controls.DrainageControls.CumDuAn;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_TaiDuLieuVeMayCaNhan : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_TaiDuLieuVeMayCaNhan()
        {
            InitializeComponent();
        }

        List<AppRoleViewModel> _appRoles = new List<AppRoleViewModel>();

        public delegate void DE_BeginDownloadProjects(TypeDownloadProjectEnum type, ThongTinDuAnExtensionViewModel[] prjIds);
        public DE_BeginDownloadProjects BeginDownload;
        private async void XtraForm_TaiDuLieuVeMayCaNhan_Load(object sender, EventArgs e)
        {
            await LoadCumDuAn();
            WaitFormHelper.ShowWaitForm("Đang tải danh sách dự án");

            string api = RouteAPI.TongDuAn_GetAllDAByCurrentUser;

            var result = await CusHttpClient.InstanceCustomer.MGetAsync<List<ThongTinDuAnExtensionViewModel>>(api);

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Lỗi tải danh sách dự án");
                WaitFormHelper.CloseWaitForm();
                this.Close();
                return;
            }

            var DasLocal = DataProvider.InstanceTHDA
                .ExecuteQueryModel<ThongTinDuAnExtensionViewModel>($"SELECT * FROM {MyConstant.TBL_THONGTINDUAN}");

            foreach (var DaServer in result.Dto)
            {
                var daLocal = DasLocal.SingleOrDefault(x => x.Code == DaServer.Code);
                if (daLocal != null)
                {
                    DaServer.IsExistInSQL = true;
                    DaServer.LastSyncInSQLite = daLocal.LastSync;
                }
            }

            tl_DuAns.DataSource = new BindingList<ThongTinDuAnExtensionViewModel>(result.Dto);

            if (BaseFrom.allPermission.HaveInitProjectPermission)
            {
                var response = await CusHttpClient.InstanceCustomer.MGetAsync<List<AppRoleViewModel>>($@"{RouteAPI.APPROLE_CONTROLLER}\{RouteAPI.SUFFIX_GetAll}");

                if (!response.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError("Không thể tải nhóm quyền");
                    //repoSlke_PrjOwner_EditValueChanging -=
                    //return;
                }
                else
                {
                    repoSlke_PrjOwner.DataSource = _appRoles = response.Dto;
                }
            }
            else
                col_owner.Visible = false;
            

            WaitFormHelper.CloseWaitForm();

        }

        private async Task LoadCumDuAn()
        {
            WaitFormHelper.ShowWaitForm("Đang tải danh sách cụm dự án");

            string api = RouteAPI.CumDuAn_GetAll;

            var result = await CusHttpClient.InstanceCustomer.MGetAsync<List<CumDuAnDto>>(api);

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Lỗi tải danh sách cụm dự án");
                WaitFormHelper.CloseWaitForm();
                this.Close();
                return;
            }

            repoSlke_CumDuAn.DataSource = result.Dto;
            WaitFormHelper.CloseWaitForm();

        }

        private void bt_Download_Click(object sender, EventArgs e)
        {
            var DAsSelected = (tl_DuAns.DataSource as BindingList<ThongTinDuAnExtensionViewModel>).Where(x => x.Chon);

            //var daIds = DAsSelected.Where(x => x.Chon).Select(x => x.Code).ToArray();
            if (!DAsSelected.Any())
            {
                MessageShower.ShowError("Chưa chọn dự án nào!");
                return;
            }
            BeginDownload((TypeDownloadProjectEnum)rg_typeDownload.SelectedIndex, DAsSelected.ToArray());
            this.Close();
        }


        private async void repobt_Xóa_Click(object sender, EventArgs e)
        {
            if (!BaseFrom.IsFullAccess)
            {
                MessageShower.ShowError("Chỉ người sở hữu bản quyền mới có thể xóa dự án. Vui lòng liên hệ chủ sở hữu hoặc chuyển sang chế độ bản quyền!");
                return;
            }
            var crDuAn = tl_DuAns.GetFocusedRow() as ThongTinDuAnExtensionViewModel;
            if (crDuAn is null)
            {
                MessageShower.ShowError("Không tìm thấy dự án");
                return;
            }

            var dr = MessageShower.ShowYesNoQuestion($"Bạn có chắc chắn muốn xóa dự án \"{crDuAn.TenDuAn}\" không?");

            if (dr != DialogResult.Yes)
                return;

            dr = MessageShower.ShowYesNoQuestion($"Xóa dự án \"{crDuAn.TenDuAn}\" sẽ tốn thời gian 3-5 phút!\r\n" +
                $"Sau khi XÁC NHẬN sẽ KHÔNG THỂ HOÀN TÁC?");

            if (dr != DialogResult.Yes)
                return;

            WaitFormHelper.ShowWaitForm("Đang xóa dự án");
            var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{RouteAPI.RawSQL_CONTROLLER}/DeleteProject", crDuAn.Code);

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError($"Lỗi xóa dự án: {result.MESSAGE_CONTENT}");
                WaitFormHelper.CloseWaitForm();

                return;
            }
            else
            {
                MessageShower.ShowInformation("Đã xóa dự án!");
                XtraForm_TaiDuLieuVeMayCaNhan_Load(null, null);
            }
            WaitFormHelper.CloseWaitForm();
        }
        private async void bt_rebase_Click(object sender, EventArgs e)
        {
            var DAsSelected = tl_DuAns.DataSource as BindingList<ThongTinDuAnExtensionViewModel>;

            var daIds = DAsSelected.Where(x => x.Chon).Select(x => x.Code).ToArray();
            if (!daIds.Any())
            {
                MessageShower.ShowError("Chưa chọn dự án nào!");
                return;
            }
            this.Enabled = false;
            WaitFormHelper.ShowWaitForm("Đang chuẩn hóa thời gian các dự án");
            var result = await CusHttpClient.InstanceCustomer
                        .MPostAsJsonAsync<bool>(RouteAPI.TongDuAn_SetBaseTimeProjects, daIds);

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Cập nhật không thành công!");
                this.Enabled = true;
                WaitFormHelper.CloseWaitForm();
                return;
            }

            MessageShower.ShowInformation("Đã cập nhật!");

            this.Enabled = true;
            WaitFormHelper.CloseWaitForm();

            XtraForm_TaiDuLieuVeMayCaNhan_Load(null, null);



        }

        private void tl_DuAns_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {

        }

        private void tl_DuAns_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void tl_DuAns_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            var da = tl_DuAns.GetFocusedRow() as ThongTinDuAnExtensionViewModel;

            if (da is null)
                return;

            if (!da.IsValidDownload && e.Column == col_Check && e.Value.ToString() == true.ToString())
            {
                MessageShower.ShowWarning("Dữ liệu dự án lưu ở offline đã cũ hơn so với thời gian chuẩn hóa!.\r\n" +
                    "Vui lòng chọn chế độ tải về file mới");
                tl_DuAns.CancelCurrentEdit();
            }
        }

        private void tl_DuAns_CustomDrawRow(object sender, CustomDrawRowEventArgs e)
        {

        }

        private void tl_DuAns_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (rg_typeDownload.SelectedIndex == (int)TypeDownloadProjectEnum.NEW)
                return;
            var da = tl_DuAns.GetRow(e.Node.Id) as ThongTinDuAnExtensionViewModel;
            if (!da.IsValidDownload)
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void rg_typeDownload_SelectedIndexChanged(object sender, EventArgs e)
        {
            tl_DuAns.RefreshDataSource();
        }

        private async void repoSlke_PrjOwner_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var crDa = tl_DuAns.GetFocusedRow() as ThongTinDuAnExtensionViewModel;

            //var role = _appRoles.SingleOrDefault(x => x.Id.ToString() == e.NewValue?.ToString()) ?? default;
            

            string api = $"{RouteAPI.TongDuAn_UpdateOwnerDA}/{crDa.Code}/{e.NewValue??Guid.Empty}";

            var ret = await CusHttpClient.InstanceCustomer.MGetAsync<bool>(api);
            

            if (!ret.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Lỗi server");
                e.Cancel = true;
                return;
            }
        }

        private async void repoSlke_CumDuAn_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var crDa = tl_DuAns.GetFocusedRow() as ThongTinDuAnExtensionViewModel;

            //var role = _appRoles.SingleOrDefault(x => x.Id.ToString() == e.NewValue?.ToString()) ?? default;


            string api = $"{RouteAPI.TongDuAn_UpdateCumDuAn}/{crDa.Code}/{e.NewValue ?? Guid.Empty}";

            var ret = await CusHttpClient.InstanceCustomer.MGetAsync<bool>(api);


            if (!ret.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Lỗi server");
                e.Cancel = true;
                return;
            }
        }

        private void bt_CumDuAn_Click(object sender, EventArgs e)
        {
            var uc = new uc_CumDuAn();
            var form = uc.CreateFormToShowControl("Quản lý cụm dự án", state: FormWindowState.Maximized);
            
            form.ShowDialog();

            if (uc.Changed)
            {
                LoadCumDuAn();
            }
        }

        private async void repoCe_AutoDivision_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var da = (tl_DuAns.GetFocusedRow() as ThongTinDuAnExtensionViewModel).Clone() as ThongTinDuAnExtensionViewModel;
            da.AutoDivision = (bool)e.NewValue;

            if (da is null)
                return;

            var allPermission = BaseFrom.allPermission;
            if (!allPermission.HaveInitProjectPermission && !allPermission.AllProjectThatUserIsAdmin.Contains(da.Code))
            {
                MessageShower.ShowWarning("Bạn không có quyền thay đổi trường thông tin này! Vui lòng liên hệ Admin");
                return;
            };

            var ret = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{Server.Tbl_ThongTinDuAn}/Update", da);

            if (!ret.MESSAGE_TYPECODE)
            {
                MessageShower.ShowWarning("Lỗi cập nhật");
                e.Cancel = true;
                return;
            }

            MessageShower.ShowInformation("Đã cập nhật");


        }

        private async void repoCe_AutoSynthetic_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var da = (tl_DuAns.GetFocusedRow() as ThongTinDuAnExtensionViewModel).Clone() as ThongTinDuAnExtensionViewModel;
            da.IsAutoSynthetic = (bool)e.NewValue;
            if (da is null)
                return;

            var allPermission = BaseFrom.allPermission;
            if (!allPermission.HaveInitProjectPermission && !allPermission.AllProjectThatUserIsAdmin.Contains(da.Code))
            {
                MessageShower.ShowWarning("Bạn không có quyền thay đổi trường thông tin này! Vui lòng liên hệ Admin");
                return;
            };

            var ret = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{Server.Tbl_ThongTinDuAn}Controller/Update", da);

            if (!ret.MESSAGE_TYPECODE)
            {
                MessageShower.ShowWarning("Lỗi cập nhật");
                e.Cancel = true;
                return;
            }

            MessageShower.ShowInformation("Đã cập nhật");
        }
    }
}