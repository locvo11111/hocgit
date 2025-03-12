using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
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

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_XapXepCapQuanTri : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_XapXepCapQuanTri(List<AppRoleLevelViewModel> ls)
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;
            tl_CapQuanTri.DataSource = ls;
        }

        private void repobt_moveDown_Click(object sender, EventArgs e)
        {
            var crNode = tl_CapQuanTri.FocusedNode;
            var crInd = tl_CapQuanTri.GetNodeIndex(crNode);

            if (crInd == tl_CapQuanTri.AllNodesCount - 1)
            {
                MessageShower.ShowWarning("Nhóm này đã ở dưới cùng rồi");
                return;
            }    

            tl_CapQuanTri.SetNodeIndex(crNode, crInd + 1);
        }

        private void repobt_moveup_Click(object sender, EventArgs e)
        {
            var crNode = tl_CapQuanTri.FocusedNode;
            var crInd = tl_CapQuanTri.GetNodeIndex(crNode);

            if (crInd == 0)
            {
                MessageShower.ShowWarning("Nhóm này đã ở trên cùng rồi");
                return;
            }

            tl_CapQuanTri.SetNodeIndex(crNode, crInd - 1);
        }

        private async void bt_OK_Click(object sender, EventArgs e)
        {
            foreach (TreeListNode node in tl_CapQuanTri.GetNodeList())
            {
                node.SetValue("Level", tl_CapQuanTri.GetNodeIndex(node) + 1);
            }

            var source = tl_CapQuanTri.DataSource as List<AppRoleLevelViewModel>;

            string url = "AppRoleLevel/CreateOrUpdateMulti";

            var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>(url, source);

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Không thể cập nhật, vui lòng kiểm tra lại server!");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();

        }
    }
}