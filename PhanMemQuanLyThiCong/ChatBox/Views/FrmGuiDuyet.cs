using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Common.ViewModel;

namespace PhanMemQuanLyThiCong.ChatBox.Views
{
    public partial class FrmGuiDuyet : Form
    {
        public GiaoViecExtensionViewModel _giaoViec;
        private List<GiaoViec_FileDinhKemExtensionViewModel> lstFiles = new List<GiaoViec_FileDinhKemExtensionViewModel>();

        public FrmGuiDuyet(GiaoViecExtensionViewModel giaoViec)
        {
            InitializeComponent();
            _giaoViec = giaoViec;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmGuiDuyet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private async void FrmGuiDuyet_Load(object sender, EventArgs e)
        {
            lstFiles = await ChatHelper.GetAllFileByCode(_giaoViec.CodeCongViecCon??_giaoViec.CodeCongViecCha, _giaoViec.typeCongTac);
            lblCongTac.Text = string.IsNullOrEmpty(_giaoViec.TenCongViec) ? _giaoViec.TenDauViecNho : _giaoViec.TenCongViec; ;
            lblCongTrinh.Text = string.IsNullOrEmpty(_giaoViec.TenCongTrinh) ? _giaoViec.TenDauViecLon : _giaoViec.TenCongTrinh;
            var listManageGroups = new List<GiaoViec_FileDinhKemExtensionViewModel>();
            foreach (var item in lstFiles)
            {
                if (item.State == (int)FileStateEnum.APPROVED)
                    item.Chon = true;
                listManageGroups.Add(item);
            }
            LoadCheckBoxFile(listManageGroups);
        }

        private void LoadCheckBoxFile(List<GiaoViec_FileDinhKemExtensionViewModel> manageGroups)
        {
            check_listfileduyet.Items.Clear();
            check_listfileduyet.DataSource = manageGroups;
            check_listfileduyet.DisplayMember = "FileDinhKem";
            check_listfileduyet.ValueMember = "Code";
            check_listfileduyet.CheckMember = "Chon";
        }
    }
}