using DevExpress.Utils.Extensions;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class ctrlTimKiemVatLieu : UserControl
    {
        string m_codeGD;
        public string m_NameCot,m_codeHM,m_TenHM;
        public int _type,_rowindex;
        public delegate void DE_TRUYENDATAVATLIEU(DataTable dtTbl);
        public DE_TRUYENDATAVATLIEU m_DataChonVL;
        public ctrl_TimKiemYeuCauVatLieu m_TimkiemYC;
        public ControlCollection m_Name;
        public ctrlTimKiemVatLieu()
        {
            InitializeComponent();
            if (m_NameCot != "TenVatTu"&&m_NameCot == "MaVatTu")
                this.Hide();
        }
        public void fcn_loadVatLieu(DataTable dt)
        {
            dt.Columns.Add("Chon", typeof(bool));
            dt.AsEnumerable().ForEach(x => x["Chon"] = false);
            gc_LoadVatLieu.DataSource = dt;         
        }
        private void fcn_DE_NhanDatVLYC(DataTable dt)
        {
            if (dt == null)
                return;
            dt.Columns["TenVatTu"].ColumnName = "VatTu";
            dt.Columns["MaVatTu"].ColumnName = "MaVatLieu";
            m_DataChonVL(dt);
            this.Hide();
        }
        public void fcn_LoadCot(string NameCot,int type,string codeHM,int rowindex,string tenHM,ControlCollection Name)
        {
            m_NameCot = NameCot;
            _type = type;
            m_codeHM = codeHM;
            _rowindex = rowindex;
            m_TenHM = tenHM;
            m_Name = Name;
        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {

            DataRow[] rows = (gc_LoadVatLieu.DataSource as DataTable).AsEnumerable().Where(x => x["Chon"].ToString() == true.ToString()).ToArray();// (DataTable)dgv_TimKiemDinhMuc.DataSource;
            if (rows.Any())
                m_DataChonVL(rows.CopyToDataTable());
            this.Hide();
        }

        private void btn_BoChonAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in (gc_LoadVatLieu.DataSource as DataTable).Rows)
                row["Chon"] = false;
            gc_LoadVatLieu.RefreshDataSource();

        }

        private void btn_ChonYeuCau_Click(object sender, EventArgs e)
        {
            this.Hide();
            m_TimkiemYC = new ctrl_TimKiemYeuCauVatLieu();
            m_TimkiemYC.m_DataChonVLYC = new ctrl_TimKiemYeuCauVatLieu.DE_TRUYENDATAVATLIEUYC(fcn_DE_NhanDatVLYC);
            m_TimkiemYC.Dock = DockStyle.None;
            m_Name.Add(m_TimkiemYC);
            m_TimkiemYC.Show();
            string dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND \"IsDone\"='{true}'";//AND \"IsDone\"='{0}'
            DataTable dt_yeucau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            m_TimkiemYC.BringToFront();
            m_TimkiemYC.fcn_loadVatLieu(dt_yeucau);
            //m_TimkiemYC.Hide();

        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string MaVatLieu = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MaVatLieu"]);
                if (MaVatLieu == "TT")
                {
                    e.Appearance.BackColor = Color.Salmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;
                    e.HighPriority = true;
                }
            }
        }

        private void btn_ChonAll_Click(object sender, EventArgs e)
        {
            foreach(DataRow row in (gc_LoadVatLieu.DataSource as DataTable).Rows)
                row["Chon"] = true;
            gc_LoadVatLieu.RefreshDataSource();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            m_DataChonVL(null);
            this.Hide();
        }
    }
}
