using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace PhanMemQuanLyThiCong.Controls.CongVanDiDen
{
    public partial class Ctrl_DuAnCongVan : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void DE_TransDonViPH(List<Tbl_ThongTinDuAnViewModel> DanhSachNV, string CodeCV);
        public DE_TransDonViPH de_TranDanhSachDVPH;
        public string _Code;
        public string _ColCode;
        public Ctrl_DuAnCongVan()
        {
            InitializeComponent();
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void Fcn_UpDateDanhSachDVPH(string Code, string CodeDA)
        {
            List<Tbl_ThongTinDuAnViewModel> lst = SharedControls.slke_ThongTinDuAn.Properties.DataSource as List<Tbl_ThongTinDuAnViewModel>;
            List<Tbl_ThongTinDuAnViewModel> Manage = lst.FindAll(x => x.Code == "Manage").ToList();
            if (Manage.Count() != 0)
                lst.Remove(Manage.SingleOrDefault());
            glue_NhanVien.Properties.DataSource = lst;
            _Code = Code;
            if (CodeDA != null)
                gridLookUpEdit1View.SetFocusedRowCellValue("Code", CodeDA);
        }
        private void sb_Ok_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridLookUpEdit1View.GetSelectedRows();
            List<Tbl_ThongTinDuAnViewModel> DanhSachDV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as Tbl_ThongTinDuAnViewModel).ToList();
            de_TranDanhSachDVPH(DanhSachDV, _Code);
            this.Hide();
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
