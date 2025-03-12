using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace PhanMemQuanLyThiCong.ChatBox.Views
{
    public partial class ApproveWork : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SendStateDuyet(int state, GiaoViecExtensionViewModel giaoViec);

        public SendStateDuyet sendState;
        public GiaoViecExtensionViewModel _giaoViec;
        private IDataRepository _dataService = ConfigUnity.Container.Resolve<IDataRepository>();
        private List<GiaoViec_FileDinhKemExtensionViewModel> lstFiles = new List<GiaoViec_FileDinhKemExtensionViewModel>();
        private List<GiaoViec_FileDinhKemExtensionViewModel> lstFileChanges = new List<GiaoViec_FileDinhKemExtensionViewModel>();

        public ApproveWork(GiaoViecExtensionViewModel giaoViec)
        {
            InitializeComponent();
            _giaoViec = giaoViec;
        }

        private async void btn_duyet_Click(object sender, EventArgs e)
        {
            _giaoViec.TrangThai = EnumTrangThai.HOANTHANH.GetEnumDisplayName();
            var time = DateTime.Now;
            var lstChecks = check_listfileduyet.DataSource as List<GiaoViec_FileDinhKemExtensionViewModel>;
            foreach (var item in lstChecks)
            {
                item.LastChange = time;
                item.State = (int)FileStateEnum.APPROVED;
            }
            _giaoViec.GhiChuDuyet = txt_noidungduyet.Text;
            _giaoViec.ListFiles.Clear();
            _giaoViec.ListFiles.AddRange(lstChecks);
            _giaoViec.LastChange = time;
            _giaoViec.NgayDuyet = time;
            _giaoViec.TotalApprove = _giaoViec.TotalFile;
            _giaoViec.UserApproveId = BaseFrom.BanQuyenKeyInfo.UserId.ToString();
            _giaoViec.FullNameApprove = BaseFrom.BanQuyenKeyInfo.FullName;
            //Save
            var result = await ChatHelper.AddUpdateGiaoViec(_giaoViec);
            if (result.MESSAGE_TYPECODE)
            {
                var res1 = _dataService.UpdateGiaoViec(new List<GiaoViecExtensionViewModel> { _giaoViec });
                var res = _dataService.UpdateStateGiaoViecFileDinhKem(lstChecks);
                MessageShower.ShowInformation("Duyệt công tác thành công");
                sendState(2, _giaoViec);
                this.Close();
            }
            else
            {
                MessageShower.ShowInformation("Duyệt công tác không thành công. Vui lòng liên hệ hỗ trợ");
            }
        }

        private async void btn_dieuchinh_Click(object sender, EventArgs e)
        {
            var time = DateTime.Now;
            var lstChecks = check_listfileduyet.DataSource as List<GiaoViec_FileDinhKemExtensionViewModel>;
            var lstChanges = new List<GiaoViec_FileDinhKemExtensionViewModel>();
            foreach (var item in lstChecks)
            {
                var index = lstFileChanges.Find(x => x.Code == item.Code);
                if (index.State != item.State)
                {
                    index.LastChange = time;
                    index.State = item.State;
                    lstChanges.Add(index);
                }
            }
            _giaoViec.GhiChuDuyet = txt_noidungduyet.Text;
            if (lstChecks.FindAll(x => x.State == (int)FileStateEnum.APPROVED).Count == lstChecks.Count)
            {
                _giaoViec.TrangThai = EnumTrangThai.HOANTHANH.GetEnumDisplayName();
                _giaoViec.LastChange = time;
                _giaoViec.NgayDuyet = time;
                _giaoViec.UserApproveId = BaseFrom.BanQuyenKeyInfo.UserId.ToString();
                _giaoViec.FullNameApprove = BaseFrom.BanQuyenKeyInfo.FullName;
            }

            _giaoViec.ListFiles.Clear();
            _giaoViec.ListFiles.AddRange(lstChanges);
            _giaoViec.TotalApprove = lstChecks.FindAll(x => x.State == (int)FileStateEnum.APPROVED).Count;
            var result = await ChatHelper.AddUpdateGiaoViec(_giaoViec);
            if (result.MESSAGE_TYPECODE)
            {
                var res = _dataService.UpdateStateGiaoViecFileDinhKem(lstChanges);
                MessageShower.ShowInformation("Duyệt công tác thành công");
                if (_giaoViec.TrangThai == EnumTrangThai.HOANTHANH.GetEnumDisplayName())
                {
                    var res1 = _dataService.UpdateGiaoViec(new List<GiaoViecExtensionViewModel> { _giaoViec });
                    sendState(2, _giaoViec);
                }
                else
                    sendState(1, _giaoViec);
                this.Close();
            }
            else
            {
                MessageShower.ShowInformation("Duyệt công tác không thành công. Vui lòng liên hệ hỗ trợ");
            }
        }

        private void LoadCheckBoxFile(List<GiaoViec_FileDinhKemExtensionViewModel> manageGroups)
        {
            check_listfileduyet.Items.Clear();
            check_listfileduyet.DataSource = manageGroups;
            check_listfileduyet.DisplayMember = "FileDinhKem";
            check_listfileduyet.ValueMember = "Code";
            check_listfileduyet.CheckMember = "Chon";
        }

        private async void ApproveWork_Load(object sender, EventArgs e)
        {
            lstFiles = await ChatHelper.GetAllFileByCode(_giaoViec.CodeCongViecCon??_giaoViec.CodeCongViecCha, _giaoViec.typeCongTac);
            lstFileChanges.AddRange(lstFiles.Select(x => (GiaoViec_FileDinhKemExtensionViewModel)x.Clone()));
            lblCongTac.Text = string.IsNullOrEmpty(_giaoViec.TenCongViec) ? _giaoViec.TenDauViecNho : _giaoViec.TenCongViec; ;
            lblCongTrinh.Text = string.IsNullOrEmpty(_giaoViec.TenCongTrinh) ? _giaoViec.TenDauViecLon : _giaoViec.TenCongTrinh;
            txt_noidungduyet.Text = _giaoViec.GhiChuDuyet;
            var listManageGroups = new List<GiaoViec_FileDinhKemExtensionViewModel>();
            foreach (var item in lstFiles)
            {
                if (item.State == (int)FileStateEnum.APPROVED)
                    item.Chon = true;
                listManageGroups.Add(item);
            }
            LoadCheckBoxFile(listManageGroups);
        }

        private void btn_themfileduyet_Click(object sender, EventArgs e)
        {
            FormLuaChon form3 = new FormLuaChon(_giaoViec.CodeCongViecCha, FileManageTypeEnum.CONGVIECCHA);
            form3.ShowDialog();
        }

        private async void btn_xoaFileDuyet_Click(object sender, EventArgs e)
        {
            //var fileIndex = check_listfileduyet.SelectedItem as ManageFileViewModel;
            //DialogResult dr = MessageShower.ShowYesNoQuestion($"Bạn có chắc chắn muốn xóa file ({fileIndex.Name}) này ?", "NGHIỆM THU XÂY DỰNG 360");
            //if (dr == DialogResult.Yes)
            //{
            //    listManageGroups.Remove(fileIndex);
            //    string file = lstFileUploads.Find(x => x.EndsWith(fileIndex.Name));
            //    if (!string.IsNullOrEmpty(file))
            //    {
            //        lstFileUploads.Remove(file);
            //    }
            //    LoadCheckBoxFile(listManageGroups);
            //}
        }

        private void ApproveWork_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void check_listfileduyet_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            CheckedListBoxControl checkedListBox = (CheckedListBoxControl)sender;
            GiaoViec_FileDinhKemExtensionViewModel item = (GiaoViec_FileDinhKemExtensionViewModel)checkedListBox.GetItem(e.Index);
            if (item.Chon) item.State = (int)FileStateEnum.APPROVED;
            else item.State = (int)FileStateEnum.WAITINGFORAPPROVAL;
        }
    }
}