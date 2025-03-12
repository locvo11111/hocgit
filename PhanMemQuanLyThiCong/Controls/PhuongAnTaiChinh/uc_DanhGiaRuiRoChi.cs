using DevExpress.Xpo;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.PhuongAnTaiChinh;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.PhuongAnTaiChinh
{
    public partial class uc_DanhGiaRuiRoChi : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_DanhGiaRuiRoChi()
        {
            InitializeComponent();
        }

        public delegate void DE_TransRuiRoChi(string encrypted, string display);
        public DE_TransRuiRoChi de_TransRuiRoChi;

        public void LoadData(string jsonString, List<string> arrRuiRoMua)
        {
            ((ListBox)clb_ruiro).DataSource = arrRuiRoMua;
            
            var ruiroChi = CryptoHelper.Base64DecodeToObject<RuiRoChiViewModel>(jsonString) ?? new RuiRoChiViewModel();
            //var checked = rio

            rg_dgrr.SelectedIndex = (int)ruiroChi.Type;
            foreach (string str in ruiroChi.RuiRos)
            {
                for (int i = 0; i < arrRuiRoMua.Count; i++)
                {
                        clb_ruiro.SetItemChecked(i, ruiroChi.RuiRos.Contains(arrRuiRoMua[i]));
                }                
            }
        }

        private void bt_huy_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void rg_dgrr_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_ruiro.Enabled = rg_dgrr.SelectedIndex == 0;
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            var vm = new RuiRoChiViewModel();
            vm.Type = (PhanTichRuiRoEnum)rg_dgrr.SelectedIndex;
            foreach (var item in clb_ruiro.CheckedItems)
            {
                vm.RuiRos.Add(item.ToString());
            }

            var encryptedStr = CryptoHelper.Base64EncodeObject(vm);
            var displayText = rg_dgrr.GetDescription();

            if (rg_dgrr.SelectedIndex == 0 && vm.RuiRos.Any())
            {
                displayText += $"({string.Join(", ", vm.RuiRos)})";
            }

            de_TransRuiRoChi(encryptedStr, displayText);

        }
    }
}
