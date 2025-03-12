using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Common.SQLite;
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
using VChatCore.Dto;
using PhanMemQuanLyThiCong.Constant.Enum;

namespace PhanMemQuanLyThiCong
{
    public partial class DevControl_ThongTinChiTietTongHopDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        ThongTinTHDAViewModel _THDA;
        List<AppUserViewModel> _users;
        public DevControl_ThongTinChiTietTongHopDuAn(ThongTinTHDAViewModel THDA)
        {
            InitializeComponent();
            _THDA = THDA;
            thongTinTHDAViewModelBindingSource.DataSource = THDA;
            
        }
        private async void DevControl_ThongTinChiTietTongHopDuAn_Load(object sender, EventArgs e)
        {
            List<CongTacBriefViewModel> congTacs = new List<CongTacBriefViewModel>()
            {
                new CongTacBriefViewModel()
                {
                    Id = "0",
                    ParentId = null,
                    Name = "Tổng dự án",
                },
                new CongTacBriefViewModel()
                {
                    Id = "1",
                    ParentId = "0",
                    Name = "Công trình 1",
                },
            }; 

            tl_PhanQuyen.DataSource = congTacs;

            _users = await UserHelper.GetAllUserInCusSever();
            if (_users is null)
            {
                XtraMessageBox.Show("Không thể tải dữ liệu! Vui lòng kiểm tra kết nối internet của bạn!");
                this.Enabled = false;
                return;
            }
            LoadPhanQuyen();
        }

        private void LoadPhanQuyen()
        {
            
            foreach (var user in _users)
            {
                TreeListBand mainBand = tl_PhanQuyen.Bands.AddBand(caption: $"{user.FullName} ({user.Email})");

                TreeListColumn colPermission = tl_PhanQuyen.Columns.AddVisible(fieldName: "");
                colPermission.Caption = "Quyền";

                TreeListColumn colUser = tl_PhanQuyen.Columns.AddVisible(fieldName: "");
                colUser.Caption = "Vai trò";

                mainBand.Columns.Add(colPermission);
                mainBand.Columns.Add(colUser);
                mainBand.SeparatorWidth = 2;
            }
        }


        private void bt_ThemThanhVien_Click(object sender, EventArgs e)
        {
            DevForm_ThemCaNhanVaoTHDA form = new DevForm_ThemCaNhanVaoTHDA();
            form.ShowDialog();
            
        }

    }
}
