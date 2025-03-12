using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model.ChamCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.ChamCong
{
    public partial class CaiDatKyHieuChamCong : DevExpress.XtraEditors.XtraUserControl
    {
        public CaiDatKyHieuChamCong()
        {
            InitializeComponent();
        }
        private bool EditValue { get; set; } = false;
        public void Fcn_Update()
        {
            EditValue = false;
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_KYHIEU} ";
            List<KyHieu> KyHieu = DataProvider.InstanceTHDA.ExecuteQueryModel<KyHieu>(dbString);
            kyHieuBindingSource.DataSource = KyHieu;
            EditValue = true;
        }

        private void te_NghiOm_EditValueChanged(object sender, EventArgs e)
        {
            if (!EditValue)
                return;
            Control sp = sender as Control;
            string FieldName = sp.Name;
            string dbString = $"UPDATE {DanhSachNhanVienConstant.TBL_CHAMCONG_KYHIEU} SET" +
                $" {FieldName}=@Value ";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString,parameter:new object[] { sp.Text });
        }
    }
}
