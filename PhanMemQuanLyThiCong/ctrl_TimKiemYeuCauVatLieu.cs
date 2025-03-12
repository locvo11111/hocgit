using DevExpress.Utils.Extensions;
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

namespace PhanMemQuanLyThiCong
{
    public partial class ctrl_TimKiemYeuCauVatLieu : UserControl
    {
        public delegate void DE_TRUYENDATAVATLIEUYC(DataTable dtTbl);
        public DE_TRUYENDATAVATLIEUYC m_DataChonVLYC;
        public ctrl_TimKiemYeuCauVatLieu()
        {
            InitializeComponent();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            m_DataChonVLYC(null);
            this.Hide();
        }
        public void fcn_loadVatLieu(DataTable dt)
        {
            dt.Columns.Add("Chon", typeof(bool));
            dt.AsEnumerable().ForEach(x => x["Chon"] = false);
            gc_TimKiemVatLieu.DataSource = dt;
        }
        private void btn_ChonAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in (gc_TimKiemVatLieu.DataSource as DataTable).Rows)
                row["Chon"] = true;
            gc_TimKiemVatLieu.RefreshDataSource();
        }

        private void btn_BoChonAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in (gc_TimKiemVatLieu.DataSource as DataTable).Rows)
                row["Chon"] = false;
            gc_TimKiemVatLieu.RefreshDataSource();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            DataRow[] rows = (gc_TimKiemVatLieu.DataSource as DataTable).AsEnumerable().Where(x => x["Chon"].ToString() == true.ToString()).ToArray();// (DataTable)dgv_TimKiemDinhMuc.DataSource;
            if (rows.Any())
                m_DataChonVLYC(rows.CopyToDataTable());
            this.Hide();
        }
    }
}
