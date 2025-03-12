using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Notify;
using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Services.AspUsersServices;
using PhanMemQuanLyThiCong.Validate;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using VChatCore.Dto;

namespace PhanMemQuanLyThiCong
{
    public partial class fromnhomquyen : DevExpress.XtraEditors.XtraForm
    {
        //private IAppGroupServices _iAppGroupServices;
        //private IAppRolesServices _iAppRolesServices;
        private IAspUsersServices _aspUsersServices;


        public fromnhomquyen()
        {
            InitializeComponent();
            //_iAppGroupServices = ConfigUnity.Container.Resolve<IAppGroupServices>();
            //_iAppRolesServices = ConfigUnity.Container.Resolve<IAppRolesServices>();
            _aspUsersServices = ConfigUnity.Container.Resolve<IAspUsersServices>();
        }

        private async void btnaddnhomquyen_ClickAsync(object sender, EventArgs e)
        {
            //bool check = CheckInput.CheckTextbox(lbthongbaonomquyen, txttennhomquyen);
            //if (check)
            //{
            //    List<string> ListDuAn = ConvertToList.ConvertList(checkedComboduan.EditValue.ToString());
            //    List<string> ListUser = ConvertToList.ConvertList(checkedComboUser.EditValue.ToString());
            //    List<string> ListRole = ConvertToList.ConvertList(checkedCombonhomqquyen.EditValue.ToString());

            //    AppGroupViewModel appGroupViewModel = new AppGroupViewModel();
            //    appGroupViewModel.Name = txttennhomquyen.Text;
            //    appGroupViewModel.Description = txtmota.Text;
            //    appGroupViewModel.Id =int.Parse(lbidappgroup.Text);
            //    if (ListUser.Count > 0)
            //    { appGroupViewModel.ListUsers = _iAppGroupServices.AddAppUserGroups(ListUser); }
            //    if (ListDuAn.Count > 0)
            //    { appGroupViewModel.ListDuAns = _iAppGroupServices.AddAppDuAnGoups(ListDuAn); }
            //    if (ListRole.Count > 0)
            //    {
            //        appGroupViewModel.ListRoles = _iAppGroupServices.AddAppRoleGoups(ListRole);
            //    }
            //    // Thêm mới vào sql lite
            //    //bool resuslt =  _iAppGroupServices.Add(appGroupViewModel);
            //    bool resuslt = false;
            //    if (appGroupViewModel.Id < 1)
            //    {
            //         resuslt = await _iAppGroupServices.AddAsync(appGroupViewModel);
            //        XtraMessageShower.ShowInformation(Notification.successfulInsert, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    }
            //    else
            //    {
            //        resuslt = await _iAppGroupServices.UpdateAsync(appGroupViewModel, appGroupViewModel.Id);
            //        XtraMessageShower.ShowInformation(Notification.successfulUpdate, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    }
            //    if (resuslt)
            //    {
                   
            //    }
            //    else
            //    {
            //        XtraMessageShower.ShowInformation(Notification.error, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //    }
            //    LoadGridView();
            //    Resetnhomquyen();

            //}
        }

        private async void LoadDuanAn()
        {
            var dataSource = await _thongTinTongHopDuAnServices.AllAsync();
            checkedComboduan.Properties.DataSource = dataSource;
            checkedComboduan.Properties.ValueMember = "Code";
            checkedComboduan.Properties.DisplayMember = "TenTongHopDuAn";
        }

        private async void LoadUser()
        {
            var dataSource = await _aspUsersServices.AllAsync();
            checkedComboUser.Properties.DataSource = dataSource;
            checkedComboUser.Properties.ValueMember = "Id";
            checkedComboUser.Properties.DisplayMember = "UserName";
        }

        private async void LoadRoles()
        {
            //var dataSource = await _iAppRolesServices.AllAsync();
            //checkedCombonhomqquyen.Properties.DataSource = dataSource;
            //checkedCombonhomqquyen.Properties.ValueMember = "Id";
            //checkedCombonhomqquyen.Properties.DisplayMember = "Name";
        }
        private void txttennhomquyen_KeyDown(object sender, KeyEventArgs e)
        {
            lbthongbaonomquyen.Visible = false;
        }

        private void Resetnhomquyen()
        {
            txttennhomquyen.ResetText();
            txtmota.ResetText();

            checkedComboduan.SetEditValue(-1);
            checkedComboUser.SetEditValue(-1);
            checkedCombonhomqquyen.SetEditValue(-1);

            
            btnaddnhomquyen.Text = "Lưu lại";
        }

        private void fromnhomquyen_Load(object sender, EventArgs e)
        {
            LoadGridView();
            Resetnhomquyen();
            LoadDuanAn();
            LoadUser();
            LoadRoles();
        }
        private async void LoadGridView()
        {
            // lấy dữ liệu từ sqlite//
            // var dataSource = _iAppGroupServices.All();
            //var dataSource = await _iAppGroupServices.AllAsync();
            //gridControlNhomQuyen.DataSource = dataSource;
            //gridViewNhomQuyen.RefreshData();
            //gridControlNhomQuyen.RefreshDataSource();
        }

        private void GridView_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
        {
            Control ctrl = MyExtenstions.FindControl(e.Panel, "Update");
            if (ctrl != null)
            {
                ctrl.Text = "Cập nhật";
                ctrl.Name = "btnupdatenhomquyencon";
                ctrl.Click += new EventHandler(btnupdatenhomquyencon_Click);
            }
            ctrl = MyExtenstions.FindControl(e.Panel, "Cancel");
            if (ctrl != null)
            {
                ctrl.Text = "Đóng";
            }
        }

        private void gridControlNhomQuyen_Click(object sender, EventArgs e)
        {
        }

        private async void btnupdatenhomquyencon_Click(object sender, EventArgs e)
        {
            /////Lấy dữ liệu data ///
            //var appGroupViewModel = gridViewNhomQuyen.GetFocusedRow() as AppGroupViewModel;
            //bool check = CheckInput.Obligatory(appGroupViewModel.Name);
            //if (!check)
            //{
            //    //Cập nhật trong sqllite//
            //    // bool resuslt =  _iAppGroupServices.Update(appGroupViewModel, appGroupViewModel.Id);
            //    bool resuslt = await _iAppGroupServices.UpdateAsync(appGroupViewModel, appGroupViewModel.Id);
            //    if (resuslt)
            //    {
            //        XtraMessageShower.ShowInformation(Notification.successfulUpdate, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    }
            //    else
            //    {
            //        XtraMessageShower.ShowInformation(Notification.error, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    XtraMessageShower.ShowInformation(Notification.obligatory + " Tên nhóm quyền", Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //}
            //LoadGridView();
            //Resetnhomquyen();
        }

        private async void btnxoanhomquyen_Click(object sender, EventArgs e)
        {
            //var appGroupViewModel = gridViewNhomQuyen.GetFocusedRow() as AppGroupViewModel;
            //if (XtraMessageShower.ShowInformation(Notification.deleteNotice + appGroupViewModel.Name, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    //Xóa bỏ quyền trong sqlite///
            //    // _iAppGroupServices.Remove(appGroupViewModel);
            //    bool res = await _iAppGroupServices.RemoveAsync(appGroupViewModel.Id);
            //    if (res)
            //    {
            //        XtraMessageShower.ShowInformation(Notification.successfulDelete, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    }
            //    else
            //    {
            //        XtraMessageShower.ShowInformation(Notification.error, Notification.captionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //}
            //LoadGridView();
            //Resetnhomquyen();
        }

        private async void btnupdatenhomquyen_Click(object sender, EventArgs e)
        {
            //string listUser; string listDuan; string listRoles;
            //listUser = listDuan = listRoles = null;
            //var appGroupViewModel = gridViewNhomQuyen.GetFocusedRow() as AppGroupViewModel;
            //var data = await _iAppGroupServices.FindAsync(appGroupViewModel.Id);
            //if (data.ListUsers!=null)
            //{ data.ListUsers.ForEach(x => listUser += x.UserId + ","); }
            //if (data.ListDuAns != null)
            //{ data.ListDuAns.ForEach(x => listDuan += x.TongHopDuAnId + ","); }
            //if (data.ListRoles!=null)
            //{ data.ListRoles.ForEach(x => listRoles += x.RoleId + ","); }
            //checkedComboUser.SetEditValue(listUser);
            //checkedComboduan.SetEditValue(listDuan);
            //checkedCombonhomqquyen.SetEditValue(listRoles);
            //txttennhomquyen.Text = data.Name;
            //txtmota.Text = data.Description;
            //lbidappgroup.Text = data.Id.ToString();

            //btnaddnhomquyen.Text = "Cập nhật";
        }

        private void checkedComboUser_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void checkedComboduan_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnthemquyen_Click(object sender, EventArgs e)
        {
            LoadGridView();
            Resetnhomquyen();
            LoadDuanAn();
            LoadUser();
            LoadRoles();
        }
    }
}