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

namespace PhanMemQuanLyThiCong.Controls.MTC
{
    public partial class Uc_TenQuyDoiMay : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void DE_TransDonViPH(List<MTC_ChiTietDinhMuc> DanhSachNV, string CodeCV);
        public DE_TransDonViPH de_TranDanhSachDVPH;
        public string _Code;
        public string _ColCode;
        public Uc_TenQuyDoiMay()
        {
            InitializeComponent();
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void sb_Ok_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridLookUpEdit1View.GetSelectedRows();
            List<MTC_ChiTietDinhMuc> DanhSachDV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as MTC_ChiTietDinhMuc).ToList();
            de_TranDanhSachDVPH(DanhSachDV, _Code);
            this.Hide();
            this.Hide();
        }
        public void Fcn_UpDateDanhSachDVPH(List<MTC_ChiTietDinhMuc> ChiTiet,string Code, string Current)
        {
            glue_NhanVien.Properties.DataSource = ChiTiet;
            _Code = Code;
            //if (Current != null)
            //    gridLookUpEdit1View.SetFocusedRowCellValue("Code", Current);
        }

        private void gridLookUpEdit1View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //GridView view = sender as GridView;
            //int i = view.FocusedRowHandle;
            //if (view.Columns.Contains(view.FocusedColumn))
            //{
            //    if (view.IsRowSelected(i))
            //    {
            //        view.UnselectRow(i);
            //    }
            //    else
            //    {
            //        view.SelectRow(i);
            //    }
            //}
            //glue_NhanVien.ClosePopup();
        }
    }
}
