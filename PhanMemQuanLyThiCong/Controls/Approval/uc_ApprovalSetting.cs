using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.Enums.Approval;
using VChatCore.ViewModels.Approval;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls.Approval
{
    public partial class uc_ApprovalSetting : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_ApprovalSetting()
        {
            InitializeComponent();
        }

        private async void uc_ApprovalSetting_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu");
            var users = await UserHelper.GetAllUserInCusSever();
            repoSlke_Users.DataSource = users;

            //Load combobox ApprovalType
            var types = EnumHelper.GetDisplayNames<ApprovalTypeEnum>();
            cbb_ChonQuyTrinh.Properties.Items.AddRange(types.ToArray());

            //Load Dự Án
            var res = await CusHttpClient.InstanceCustomer.MGetAsync<List<Tbl_ThongTinDuAnViewModel>>($"{Server.Tbl_ThongTinDuAn}/{RouteAPI.SUFFIX_GetAll}");

            if (res.MESSAGE_TYPECODE)
            {
                var das = res.Dto;
                slke_DuAn.Properties.DataSource = res.Dto;
            }
            else
            {
                MessageShower.ShowError("Không thể tải thông tin dự án. Kiểm tra kết nối Internet!");
                AlertShower.ShowInfo(res.MESSAGE_CONTENT);
                
            }

            cbb_ChonQuyTrinh.SelectedIndexChanged -= cbb_ChonQuyTrinh_SelectedIndexChanged;
            cbb_ChonQuyTrinh.SelectedIndex = 0;
            cbb_ChonQuyTrinh.SelectedIndexChanged += cbb_ChonQuyTrinh_SelectedIndexChanged;

            //cbb_Setting.SelectedIndexChanged -= cbb_Set_SelectedIndexChanged;
            cbb_Setting.SelectedIndex = 0;



            LoadApprovalProcess();
            WaitFormHelper.CloseWaitForm();


        }

        private void cbb_Setting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_Setting.SelectedIndex == 1)
            {
                var ls = (slke_DuAn.Properties.DataSource as List<Tbl_ThongTinDuAnViewModel>);
                if (ls is null || ls.Count == 0)
                {
                    MessageShower.ShowWarning("Không có dự án nào trên server");
                    cbb_Setting.SelectedIndex = 0;
                    return;
                }
                if (slke_DuAn.EditValue is null)
                {
                    slke_DuAn.EditValueChanged -= slke_DuAn_EditValueChanged;
                    slke_DuAn.EditValue = ls.First().Code;
                    slke_DuAn.EditValueChanged += slke_DuAn_EditValueChanged;

                }
                lci_ChonDuAn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {

                lci_ChonDuAn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            LoadApprovalProcess();
        }

        private async void LoadApprovalProcess(bool forceNullCodeDuAn = false)
        {
            ApprovalArgs args = new ApprovalArgs();

            if (lci_ChonDuAn.Visible && !forceNullCodeDuAn)
                args.CodeDuAn = slke_DuAn.EditValue as string;

            args.Type = (ApprovalTypeEnum)cbb_ChonQuyTrinh.SelectedIndex;

            var ret = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<List<ApprovalProcessViewModel>>(RouteAPI.Approval_LoadProcess, args);
            if (ret.MESSAGE_TYPECODE)
            {
                ret.Dto.ForEach(x => x.Id = Guid.NewGuid());
                tl_QuyTrinh.DataSource = ret.Dto;
                gr_actionButton.Enabled = true;

            }
            else
            {
                tl_QuyTrinh.DataSource = null;
                gr_actionButton.Enabled = false;
                MessageShower.ShowError($"Lỗi tải quy trình: {ret.MESSAGE_CONTENT}");
            }
        }

        private async void bt_Save_Click(object sender, EventArgs e)
        {
            var ret = MessageShower.ShowYesNoQuestion("Vui lòng xác nhận cập nhật quy trình duyệt!");
            if (ret == DialogResult.Yes)
            {
                var datas = new List<ApprovalProcessViewModel>();

                foreach (var node in tl_QuyTrinh.GetNodeList())
                {
                    datas.Add(tl_QuyTrinh.GetDataRecordByNode(node) as  ApprovalProcessViewModel);
                }

                if (datas is null)
                {
                    MessageShower.ShowWarning("Tải quy trình không thành công!");
                    return;
                }

                if (datas.Any(x => x.UserId == default))
                {
                    MessageShower.ShowWarning("Vui lòng chọn người duyệt cho tất cả các bước");
                    return;
                }



                ApprovalArgs args = new ApprovalArgs();

                if (lci_ChonDuAn.Visible)
                    args.CodeDuAn = slke_DuAn.EditValue as string;

                args.Type = (ApprovalTypeEnum)cbb_ChonQuyTrinh.SelectedIndex;

                var argsUpdate = new UpdateProcessArgs()
                {
                    ApprovalArgs = args,
                    Processes = datas
                    //CodeD
                };

                var res = await CusHttpClient.InstanceCustomer
                    .MPostAsJsonAsync<bool>(RouteAPI.Approval_UpdateProcess, argsUpdate);

                if (res.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowInformation("Đã cập nhật quy trình");
                    int ind = 1;
                    foreach (var node in tl_QuyTrinh.GetNodeList())
                    {
                        node.SetValue("Level", ind++);
                    }
                    return;
                }
                else
                {
                    MessageShower.ShowError("Cập nhật không thành công");
                    
                }

            }
        }

        private void bt_Add_Click(object sender, EventArgs e)
        {
            var datas = tl_QuyTrinh.DataSource as List<ApprovalProcessViewModel>;

            if (datas is null)
            {
                MessageShower.ShowWarning("Tải quy trình không thành công!");
                return;
            }

            datas.Add(new ApprovalProcessViewModel()
            {
                Id = Guid.NewGuid(),
            });
            tl_QuyTrinh.RefreshDataSource();
        }

        private void bt_Copy_Click(object sender, EventArgs e)
        {
            LoadApprovalProcess(true);
        }

        private void repobt_Del_Click(object sender, EventArgs e)
        {
            var rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa bước duyệt này không?");
            
            if (rs == DialogResult.Yes)
            {
                tl_QuyTrinh.FocusedNode.Remove();
            } 
                
        }

        private void cbb_ChonQuyTrinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApprovalProcess();

        }

        private void slke_DuAn_EditValueChanged(object sender, EventArgs e)
        {
            LoadApprovalProcess();
        }

        private void repobt_moveUp_Click(object sender, EventArgs e)
        {
            var crNode = tl_QuyTrinh.FocusedNode;
            var crInd = tl_QuyTrinh.GetNodeIndex(crNode);

            if (crInd == 0)
            {
                MessageShower.ShowWarning("Nhóm này đã ở trên cùng rồi");
                return;
            }

            tl_QuyTrinh.SetNodeIndex(crNode, crInd - 1);
        }

        private void repobt_moveDown_Click(object sender, EventArgs e)
        {
            var crNode = tl_QuyTrinh.FocusedNode;
            var crInd = tl_QuyTrinh.GetNodeIndex(crNode);

            if (crInd == tl_QuyTrinh.AllNodesCount - 1)
            {
                MessageShower.ShowWarning("Nhóm này đã ở dưới cùng rồi");
                return;
            }

            tl_QuyTrinh.SetNodeIndex(crNode, crInd + 1);
        }

        private void tl_QuyTrinh_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }

        private void slke_DuAn_VisibleChanged(object sender, EventArgs e)
        {
            if (slke_DuAn.Visible)
            {
                lci_Copy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
                lci_Copy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            LoadApprovalProcess();
        }
    }
}
