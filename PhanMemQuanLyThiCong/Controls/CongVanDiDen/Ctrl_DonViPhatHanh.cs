using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Model.CongVan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.CongVanDiDen
{
    public partial class Ctrl_DonViPhatHanh : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void DE_TransDonViPH(List<CongVanDiDenModel> DanhSachNV,string CodeCV);
        public DE_TransDonViPH de_TranDanhSachDVPH;
        public string _Code;
        public string _ColCode;
        public Ctrl_DonViPhatHanh()
        {
            InitializeComponent();
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void Fcn_UpDateDanhSachDVPH(List<CongVanDiDenModel> DanhSachNV, string Code,string CodeDVPH)
        {
            glue_NhanVien.Properties.DataSource = DanhSachNV;
            _Code = Code;
            if(CodeDVPH!=null)
                gridLookUpEdit1View.SetFocusedRowCellValue("Code", CodeDVPH);
        }
        private void sb_Ok_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridLookUpEdit1View.GetSelectedRows();
            List<CongVanDiDenModel> DanhSachDV= selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as CongVanDiDenModel).ToList();
            de_TranDanhSachDVPH(DanhSachDV,_Code);
            this.Hide();
        }

        private void gridLookUpEdit1View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
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
