using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Notify;
using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Validate;
using System;
using System.Windows.Forms;
using Unity;

namespace PhanMemQuanLyThiCong
{
    public partial class fromquyen : DevExpress.XtraEditors.XtraForm
    {
        //private IAppRolesServices _iAppRolesServices;

        public fromquyen()
        {
            InitializeComponent();
            //_iAppRolesServices = ConfigUnity.Container.Resolve<IAppRolesServices>();
        }

        private async void rebtnxoaroles_Click(object sender, EventArgs e)
        {
            //var appRoleViewModel = gridViewRole.GetFocusedRow() as AppRolesViewModel;
            //if (MessageShower.ShowInformation(Notification.deleteNotice + appRoleViewModel.Name, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    // _iAppRolesServices.Remove(appRoleViewModel);
            //    bool res = await _iAppRolesServices.RemoveAsync(appRoleViewModel.Id);
            //    if (res)
            //    {
            //        MessageShower.ShowInformation(Notification.successfulDelete, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    }
            //    else
            //    {
            //        MessageShower.ShowInformation(Notification.error, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    // MessageShower.ShowInformation(Notification.error, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //}
            //LoadRoles();
            //Resetquyen();
        }

        private void repbtnupdateroles_Click(object sender, EventArgs e)
        {
            gridViewRole.EditFormPrepared += GridView_EditFormPrepared;
            gridViewRole.OptionsEditForm.ShowOnDoubleClick = DevExpress.Utils.DefaultBoolean.False;
            gridViewRole.OptionsEditForm.ShowOnEnterKey = DevExpress.Utils.DefaultBoolean.False;
            gridViewRole.OptionsEditForm.ShowOnF2Key = DevExpress.Utils.DefaultBoolean.False;
            gridViewRole.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;

            gridViewRole.CloseEditor();
            gridViewRole.ShowEditForm();
        }

        private void Resetquyen()
        {
            txtenquyen.Text = null;
            txtmota.Text = null;
            txtcondau.Text = null;
            txttenchuanhoa.Text = null;
        }

        private async void btnaddnhomquyen_Click(object sender, EventArgs e)
        {
            //bool check = CheckInput.CheckTextbox(lbthongbaoquyen, txtenquyen);
            //if (check)
            //{
            //    AppRolesViewModel appRoleViewModel = new AppRolesViewModel();
            //    appRoleViewModel.Name = txtenquyen.Text;
            //    appRoleViewModel.Description = txtmota.Text;
            //    appRoleViewModel.ConcurrencyStamp = txtcondau.Text;
            //    appRoleViewModel.NormalizedName = txttenchuanhoa.Text;

            //    // bool resuslt = _iAppRolesServices.Add(appRoleViewModel);
            //    bool resuslt = await _iAppRolesServices.AddAsync(appRoleViewModel);
            //    if (resuslt)
            //    {
            //        MessageShower.ShowInformation(Notification.successfulInsert, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    }
            //    else
            //    {
            //        MessageShower.ShowInformation(Notification.error, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //    }
            //    LoadRoles();
            //    Resetquyen();
            //}
        }

        private async void LoadRoles()
        {
            ////var dataSource = _iAppRolesServices.All();
            //var dataSource = await _iAppRolesServices.AllAsync();
            //gridControlRole.DataSource = dataSource;
            //gridViewRole.RefreshData();
            //gridControlRole.RefreshDataSource();
        }

        private void fromquyen_Load(object sender, EventArgs e)
        {
            LoadRoles();
            Resetquyen();
        }

        private void GridView_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
        {
            Control ctrl = MyExtenstions.FindControl(e.Panel, "Update");
            if (ctrl != null)
            {
                ctrl.Text = "Cập nhật";
                ctrl.Name = "btnupdatenhomquyencon";
                ctrl.Click += new EventHandler(btnupdatenquyencon_Click);
            }
            ctrl = MyExtenstions.FindControl(e.Panel, "Cancel");
            if (ctrl != null)
            {
                ctrl.Text = "Đóng";
            }
        }

        private async void btnupdatenquyencon_Click(object sender, EventArgs e)
        {
            //var appRoleViewModel = gridViewRole.GetFocusedRow() as AppRolesViewModel;
            //bool check = CheckInput.Obligatory(appRoleViewModel.Name);
            //if (!check)
            //{
            //    // bool resuslt = _iAppRolesServices.Update(appRoleViewModel, appRoleViewModel.Id);
            //    bool resuslt = await _iAppRolesServices.UpdateAsync(appRoleViewModel, appRoleViewModel.Id);
            //    if (resuslt)
            //    {
            //        MessageShower.ShowInformation(Notification.successfulUpdate, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    }
            //    else
            //    {
            //        MessageShower.ShowInformation(Notification.error, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    MessageShower.ShowInformation(Notification.obligatory + " Tên nhóm quyền", Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //}
            //LoadRoles();
            //Resetquyen();
        }
    }
}