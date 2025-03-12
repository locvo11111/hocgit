using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class Uc_TenNhanVien : DevExpress.XtraEditors.XtraUserControl
    {

        public delegate void DE_TransDanhSachNV(List<DanhSachNhanVienModel> DanhSachNV);
        public DE_TransDanhSachNV de_TranDanhSachNV;
        public string _Code;
        public string _ColCode;
        public Uc_TenNhanVien()
        {
            InitializeComponent();
        }
        public void Fcn_UpDateDanhSachNhanVien(List<DanhSachNhanVienModel> DanhSachNV,string ColCode,string Code)
        {
            glue_NhanVien.Properties.DataSource = DanhSachNV;
            _Code = Code;
            _ColCode = ColCode;
        }
        private void sb_Ok_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridLookUpEdit1View.GetSelectedRows();
            List<DanhSachNhanVienModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as DanhSachNhanVienModel).ToList();
            de_TranDanhSachNV(DanhSachNV);
            this.Hide();    
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void glue_NhanVien_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            int[] selectedRows = (((DevExpress.XtraEditors.GridLookUpEditBase)(sender)).Properties).View.GetSelectedRows();
            List<DanhSachNhanVienModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as DanhSachNhanVienModel).ToList();
            var displayText = string.Join(", ", DanhSachNV.Where(x => x.TenGhep != null).Select(x => x.TenGhep));
            glue_NhanVien.Properties.NullText = displayText;
        }

        private void glue_NhanVien_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            int[] selectedRows = (((DevExpress.XtraEditors.GridLookUpEditBase)(sender)).Properties).View.GetSelectedRows();
            List<DanhSachNhanVienModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as DanhSachNhanVienModel).ToList();
            var displayText = string.Join(", ", DanhSachNV.Where(x => x.TenGhep != null).Select(x => x.TenGhep));
            e.DisplayText = displayText;
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
