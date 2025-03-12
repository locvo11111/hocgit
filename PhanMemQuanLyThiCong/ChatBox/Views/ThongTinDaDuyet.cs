using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Function;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using Unity;
using PhanMemQuanLyThiCong.Common.ViewModel;

namespace PhanMemQuanLyThiCong.ChatBox.Views
{
    public partial class ThongTinDaDuyet : DevExpress.XtraEditors.XtraForm
    {
        public GiaoViecExtensionViewModel _giaoViec;
        private List<GiaoViec_FileDinhKemExtensionViewModel> lstFiles = new List<GiaoViec_FileDinhKemExtensionViewModel>();
        public ThongTinDaDuyet(GiaoViecExtensionViewModel giaoViec)
        {
            InitializeComponent();
            _giaoViec = giaoViec;
        }

        private void LoadCheckBoxFile(List<GiaoViec_FileDinhKemExtensionViewModel> manageGroups)
        {
            check_listfileduyet.Items.Clear();
            check_listfileduyet.DataSource = manageGroups;
            check_listfileduyet.DisplayMember = "FileDinhKem";
            check_listfileduyet.ValueMember = "Code";
            check_listfileduyet.CheckMember = "Chon";
        }

        private async void ThongTinDaDuyet_Load(object sender, EventArgs e)
        {
            lstFiles = await ChatHelper.GetAllFileByCode(_giaoViec.CodeCongViecCon ?? _giaoViec.CodeCongViecCha, _giaoViec.typeCongTac);
            lbl_TenCongTac.Text = string.IsNullOrEmpty(_giaoViec.TenCongViec) ? _giaoViec.TenDauViecNho : _giaoViec.TenCongViec; ;
            lbl_TenCongTrinh.Text = string.IsNullOrEmpty(_giaoViec.TenCongTrinh) ? _giaoViec.TenDauViecLon : _giaoViec.TenCongTrinh;
            txt_noidungduyet.Text = _giaoViec.GhiChuDuyet;
            lbl_NgayDuyet.Text = _giaoViec.NgayDuyet.ToString();
            lbl_NgayGuiDuyet.Text = _giaoViec.NgayGuiDuyet.ToString();
            lbl_NguoiDuyet.Text = string.IsNullOrEmpty(_giaoViec.FullNameApprove) ? " " : _giaoViec.FullNameApprove;
            lbl_NguoiGui.Text = string.IsNullOrEmpty(_giaoViec.FullNameSend) ? " " : _giaoViec.FullNameSend;
            var listManageGroups = new List<GiaoViec_FileDinhKemExtensionViewModel>();
            foreach (var item in lstFiles)
            {
                if (item.State == (int)FileStateEnum.APPROVED)
                    item.Chon = true;
                listManageGroups.Add(item);
            }
            LoadCheckBoxFile(listManageGroups);
        }
    }
}