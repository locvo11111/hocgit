using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Controls.Approval;
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
    public partial class XtraForm_ApprovalProcesscs : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_ApprovalProcesscs()
        {
            InitializeComponent();
        }

        private void XtraForm_ApprovalProcesscs_Load(object sender, EventArgs e)
        {
            LoadProcess();
            //xtraTabPage_Setting.Controls.Add(new uc_ApprovalSetting());
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadProcess();
        }

        private async void LoadProcess()
        {
            
            if (xtraTabControl1.SelectedTabPage == xtraTabPage_Setting)
            {
                xtraTabPage_Setting.Controls.Clear();
                var uc = new uc_ApprovalSetting();
                uc.Parent = xtraTabPage_Setting;
                uc.Dock = DockStyle.Fill;
            }
            else
            {
                xtraTabPage_Task.Controls.Clear();
                var uc = new uc_ApprovalDetails();
                uc.Parent = xtraTabPage_Task;
                uc.Dock = DockStyle.Fill;
            }
        }
    }
}