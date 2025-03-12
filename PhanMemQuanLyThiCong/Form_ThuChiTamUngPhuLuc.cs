using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
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

namespace PhanMemQuanLyThiCong
{
    public partial class Form_ThuChiTamUngPhuLuc : DevExpress.XtraEditors.XtraForm
    {
        List<PhuLucThuCong_ChiTiet> m_PhuLuc;
        string m_TBL;
        public delegate void DE__TRUYENDATA(long ThanhTien);
        public DE__TRUYENDATA m__TRUYENDATA;
        public Form_ThuChiTamUngPhuLuc()
        {
            InitializeComponent();

        }
        public void Fcn_Update(string TBL, List<PhuLucThuCong_ChiTiet> PhuLuc)
        {
            m_TBL = TBL;
            m_PhuLuc = PhuLuc;
            if(TBL == ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL)
            {
                gv_PhuLucThuChi.Columns["ThanhTien"].Visible = false;
                gv_PhuLucThuChi.Columns["ThanhTienThuCong"].Visible = true;
            }
        }
        public void Fcn_VisibleColum()
        {
            gv_PhuLucThuChi.Columns["NgayBD"].Visible = true;
            gv_PhuLucThuChi.Columns["NgayKT"].Visible = true;
            gv_PhuLucThuChi.Columns["DonGia"].Visible = false;
        }
        private void Form_ThuChiTamUngPhuLuc_Load(object sender, EventArgs e)
        {
            gc_PhuLucThuChi.DataSource = m_PhuLuc;
            gv_PhuLucThuChi.Columns["TenNoiDungUng"].Group();
            gv_PhuLucThuChi.ExpandAllGroups();
        }

        private void gv_PhuLucThuChi_ShowingEditor(object sender, CancelEventArgs e)
        {
            //e.Cancel = true;
        }

        private void gv_PhuLucThuChi_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (m_TBL != ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL)
            {
                gv_PhuLucThuChi.CancelUpdateCurrentRow();
                return;
            }
            gv_PhuLucThuChi.UpdateGroupSummary();
            string Code =(string)gv_PhuLucThuChi.GetRowCellValue(e.RowHandle, "Code");
            string dbstring = $"UPDATE {m_TBL} SET {e.Column.FieldName}='{e.Value}' WHERE \"Code\"='{Code}' ";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
        }

        private void Form_ThuChiTamUngPhuLuc_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_TBL == ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL)
            {
                gv_PhuLucThuChi.UpdateGroupSummary();
                m__TRUYENDATA((long)(Math.Round(double.Parse(gv_PhuLucThuChi.Columns["ThanhTienThuCong"].SummaryItem.SummaryValue.ToString()))));
            }
        }

        private void gv_PhuLucThuChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gv_PhuLucThuChi.CloseEditor();
                e.SuppressKeyPress = true;
                gv_PhuLucThuChi.FocusedRowHandle = gv_PhuLucThuChi.FocusedRowHandle + 1;
            }
            gv_PhuLucThuChi.UpdateGroupSummary();
        }
    }
}