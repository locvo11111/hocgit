using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
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
    public partial class ctrl_TimKiemVatLieuThuCong : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void DE_TRUYENDATAVATLIEU(DataTable dtTbl);
        public DE_TRUYENDATAVATLIEU m_DataChonVL;
        public string m_NameCot, m_codeHM, m_TenHM;
        public ctrl_TimKiemVatLieuThuCong()
        {
            InitializeComponent();
        }
        public void fcn_loadVatLieu(DataTable dt)
        {
            dt.Columns.Add("Chon", typeof(bool));
            dt.AsEnumerable().ForEach(x => x["Chon"] = false);
            gc_LoadVatLieu.DataSource = dt;
        }
        public void fcn_LoadCot(string NameCot, string codeHM)
        {
            m_NameCot = NameCot;
            m_codeHM = codeHM;
        }

        private void sb_DongY_Click(object sender, EventArgs e)
        {

            DataRow[] rows = (gc_LoadVatLieu.DataSource as DataTable).AsEnumerable().Where(x => x["Chon"].ToString() == true.ToString()).ToArray();// (DataTable)dgv_TimKiemDinhMuc.DataSource;
            if (rows.Any())
                m_DataChonVL(rows.CopyToDataTable());
            this.Hide();
        }

        private void sb_ChonAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in (gc_LoadVatLieu.DataSource as DataTable).Rows)
                row["Chon"] = true;
            gc_LoadVatLieu.RefreshDataSource();
        }

        private void sb_HuyChon_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
