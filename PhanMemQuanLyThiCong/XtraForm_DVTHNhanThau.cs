using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Helper;
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
    public partial class XtraForm_DVTHNhanThau : DevExpress.XtraEditors.XtraForm
    {
        public DonViThucHien dvth { get; set; }
        public XtraForm_DVTHNhanThau()
        {
            InitializeComponent();
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Fcn_UpdateData()
        {
            List<DonViThucHien> DVTH=DuAnHelper.GetDonViThucHiens();
            if(DVTH is null)
            {
                this.Close();
                return;
            }
            DVTH.Remove(DVTH.Where(x => x.IsGiaoThau == true).FirstOrDefault());
            ctrl_DonViThucHienDuAn.DataSource = DVTH;
            //ctrl_DonViThucHienDuAn.EditValue = DVTH.FirstOrDefault().Code;
        }
        private void sb_Save_Click(object sender, EventArgs e)
        {
            dvth = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            this.Close();
        }

        private void XtraForm_DVTHNhanThau_Load(object sender, EventArgs e)
        {
            Fcn_UpdateData();
        }
    }
}