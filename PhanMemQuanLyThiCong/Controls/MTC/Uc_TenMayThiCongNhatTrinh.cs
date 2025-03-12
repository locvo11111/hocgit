using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Model.MayThiCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls.MTC
{
    public partial class Uc_TenMayThiCongNhatTrinh : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void DE_TransDanhSachMay(List<Tbl_MTC_DanhSachMayViewModel> DanhSachMay);
        public DE_TransDanhSachMay de_TranDanhSachMay;
        public string _Code;
        public string _ColCode;
        public Uc_TenMayThiCongNhatTrinh()
        {
            InitializeComponent();
        }
        public void Fcn_UpDateDanhSachNhanVien(List<Tbl_MTC_DanhSachMayViewModel> DanhSachMay, string ColCode, string Code)
        {
            glue_NhanVien.Properties.DataSource = DanhSachMay;
            glue_NhanVien.Refresh();
            _Code = Code;
            _ColCode = ColCode;
        }
        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Hide();
            glue_NhanVien.Properties.DataSource = null;
        }

        private void sb_Ok_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridLookUpEdit1View.GetSelectedRows();
            List<Tbl_MTC_DanhSachMayViewModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as Tbl_MTC_DanhSachMayViewModel).ToList();
            de_TranDanhSachMay(DanhSachNV);
            this.Hide();
            glue_NhanVien.Properties.DataSource = null;
        }

        private void glue_NhanVien_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            //int[] selectedRows = (((DevExpress.XtraEditors.GridLookUpEditBase)(sender)).Properties).View.GetSelectedRows();
            //List<Tbl_MTC_DanhSachMayViewModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as Tbl_MTC_DanhSachMayViewModel).ToList();
            //var displayText = string.Join(", ", DanhSachNV.Select(x => x.Ten));
            //glue_NhanVien.Properties.NullText = displayText;
        }

        private void glue_NhanVien_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            int[] selectedRows = (((DevExpress.XtraEditors.GridLookUpEditBase)(sender)).Properties).View.GetSelectedRows();
            List<Tbl_MTC_DanhSachMayViewModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as Tbl_MTC_DanhSachMayViewModel).ToList();
            var displayText = string.Join(", ", DanhSachNV.Select(x => x.Ten));
            glue_NhanVien.Properties.NullText = displayText;
        }

        private void gridLookUpEdit1View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView view = sender as GridView;
            int i = view.FocusedRowHandle;
            if (view.Columns.Contains(view.FocusedColumn))
            {
                if (view.IsRowSelected(i))
                {
                    view.UnselectRow(i);
                }
                else
                {
                    view.SelectRow(i);
                }
            }
            
        }
    }
}
