using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraEditors;
using MWORD = DevExpress.XtraRichEdit.API.Native;
namespace PhanMemQuanLyThiCong
{
    public partial class Form_QLMay_LuaChonCongViec : Form
    {
        //DataProvider m_db1 = new DataProvider(@"..\..\..\Database\db_QuanLyThiCong.sqlite");
        List<string> m_code = new List<string>();
        Dictionary<string, string> m_dicc = new Dictionary<string, string>();
        public delegate void DE__TRUYENDATALuachon(DataTable dt, Dictionary<string, string> dicc);
        public DE__TRUYENDATALuachon m_TruyenData;
        public Form_QLMay_LuaChonCongViec(Dictionary<string,string> dicc)
        {
            InitializeComponent();
            m_dicc = dicc;
        }

        private void fcn_loaddulieu()
        {
            string queryStr = $"SELECT ";
            string condition = "";
            foreach (string el in m_dicc.Keys)
            {
                condition += $",\"{el}\"";
            }
            queryStr += condition.Remove(0,1);
            queryStr += $" FROM Tbl_NhapTrinhThiCong";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            dataGridView1.DataSource = dt;
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            currencyManager.SuspendBinding();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index < dataGridView1.Rows.Count)
                {

                    if (MyFunction.fcn_RemoveAccents(row.Cells["NgayThang"].Value.ToString().ToLower()).Contains(MyFunction.fcn_RemoveAccents(txt_Ngaythuchien.Text.ToLower()))
                        && MyFunction.fcn_RemoveAccents(row.Cells["TenCongViecThucHien"].Value.ToString().ToLower()).Contains(MyFunction.fcn_RemoveAccents(txt_CongViecThucHien.Text.ToLower())))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
            currencyManager.ResumeBinding();
        }

        private void Form_QLMay_LuaChonCongViec_Load(object sender, EventArgs e)
        {
            fcn_loaddulieu();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (Convert.ToBoolean(row.Cells["check"].Value) ==false)
            //    {
            //        dataGridView1.Rows.RemoveAt(row.Index);
            //    }

            //}
            int result = dataGridView1.Rows.Count;
            for (int i = 0; i < result; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["check"].Value) == false)
                {
                    dt.Rows.RemoveAt(i);
                    i = i - 1;
                    result = result - 1;
                }
            }
            dt.AcceptChanges();
            m_TruyenData(dt, m_dicc);
            this.Close();
        }

        private void btn_ChonAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells["check"]).Value = true;
            }
        }

        private void btn_HuyChon_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells["check"]).Value = false;
            }
        }
    }
}
