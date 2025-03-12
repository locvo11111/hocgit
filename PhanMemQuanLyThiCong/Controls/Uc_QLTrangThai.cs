using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Data;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Uc_QLTrangThai : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dt;

        public Uc_QLTrangThai()
        {
            InitializeComponent();
        }

        private void bt_Delete_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void GetDataTable()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_MTC_TRANGTHAI}";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            gridControl1.DataSource = dt;
            gridControl1.ForceInitialize();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.OptionsNavigation.AutoFocusNewRow = true;
            gridView1.SetFocusedRowCellValue(col_Code, Guid.NewGuid().ToString());
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
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, MyConstant.TBL_MTC_TRANGTHAI);
            Form someForm = (Form)this.Parent;
            someForm.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form someForm = (Form)this.Parent;
            someForm.Close();
        }

        private void Uc_QLTrangThai_Load(object sender, EventArgs e)
        {
            GetDataTable();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}