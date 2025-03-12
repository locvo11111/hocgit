using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
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
    public partial class Uc_DanhSachMuiThiCong : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dt;
        public Uc_DanhSachMuiThiCong()
        {
            InitializeComponent();
        }
        private void GetDataTable()
        {
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}";
            List<Infor> TenNV = new List<Infor>();
            foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                TenNV.Add(new Infor
                {
                    Code = item["Code"].ToString(),
                    Ten = item["TenNhanVien"].ToString(),
                    Decription = item["MaNhanVien"].ToString()
                });
            rILUE_TenNhanVien.DataSource = TenNV;
            dbString = $"SELECT * FROM {TDKH.Tbl_TDKH_MuiThiCong} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            gridControl1.DataSource = dt;
            gridControl1.ForceInitialize();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form tmp = this.FindForm();
            tmp.Close();
            tmp.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.Tbl_TDKH_MuiThiCong);
            Form tmp = this.FindForm();
            tmp.Close();
            tmp.Dispose();
        }

        private void Uc_DanhSachMuiThiCong_Load(object sender, EventArgs e)
        {
            GetDataTable();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.OptionsNavigation.AutoFocusNewRow = true;
            gridView1.SetFocusedRowCellValue(col_Code, Guid.NewGuid().ToString());
            gridView1.SetFocusedRowCellValue(col_SortId, gridView1.RowCount);
            gridView1.SetFocusedRowCellValue(Col_CodeGiaiDoan,SharedControls.cbb_DBKH_ChonDot.SelectedValue);
            //gridView1.SetFocusedRowCellValue(col_ChuSoHuu, $"Mũi thi công {gridView1.RowCount+1}");
        }

        private void bt_Delete_Click(object sender, EventArgs e)
        {
            string Code = gridView1.GetFocusedDataRow()["Code"].ToString();
            DuAnHelper.DeleteDataRows(TDKH.Tbl_TDKH_MuiThiCong, new string[] { Code });
            gridView1.DeleteSelectedRows();
        }
    }
}
