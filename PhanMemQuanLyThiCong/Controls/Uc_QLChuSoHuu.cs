using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Uc_QLChuSoHuu : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dt;
        public Uc_QLChuSoHuu()
        {
            InitializeComponent();
        }

        private void bt_Delete_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void Uc_QLChuSoHuu_Load(object sender, EventArgs e)
        {
            GetDataTable();
        }

        private void GetDataTable()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_MTC_DANHSACHCHUSOHUU}";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            gridControl1.DataSource = dt;
            gridControl1.ForceInitialize();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.OptionsNavigation.AutoFocusNewRow = true;
            gridView1.SetFocusedRowCellValue(col_Code, Guid.NewGuid().ToString());
            gridView1.SetFocusedRowCellValue(col_ChuSoHuu, "Chủ sở hữu");
        }

        private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                view.DeleteSelectedRows();
                e.Handled = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, MyConstant.TBL_MTC_DANHSACHCHUSOHUU);
            Form tmp = this.FindForm();
            tmp.Close();
            tmp.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form tmp = this.FindForm();
            tmp.Close();
            tmp.Dispose();
        }
    }

}
