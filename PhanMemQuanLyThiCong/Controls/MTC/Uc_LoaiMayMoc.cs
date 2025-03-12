using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.MTC
{
    public partial class Uc_LoaiMayMoc : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dt;
        public Uc_LoaiMayMoc()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.OptionsNavigation.AutoFocusNewRow = true;
            gridView1.SetFocusedRowCellValue(col_Code, Guid.NewGuid().ToString());
        }
        private void GetDataTable()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_MTC_LOAIMAY}";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            gridControl1.DataSource = dt;
            gridControl1.ForceInitialize();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, MyConstant.TBL_MTC_LOAIMAY);
            Form someForm = (Form)this.Parent;
            someForm.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form someForm = (Form)this.Parent;
            someForm.Close();
        }

        private void Uc_LoaiMayMoc_Load(object sender, EventArgs e)
        {
            GetDataTable();
        }

        private void bt_Delete_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }
    }
}
