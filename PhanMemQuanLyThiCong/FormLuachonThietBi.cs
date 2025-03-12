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
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Constant;

namespace PhanMemQuanLyThiCong
{
    public partial class FormLuachonThietBi : Form
    {
        List<string> m_code = new List<string>();
        public delegate void DE__TRUYENDATA(List<string> code);
        public DE__TRUYENDATA m_TruyenData;
        public FormLuachonThietBi()
        {
            InitializeComponent();
        }

        private void FormLuachonThietBi_Load(object sender, EventArgs e)
        {
            fcn_loaddulieu();
        }
        private void fcn_loaddulieu()
        {
            string queryStr = $"SELECT \"Code\",\"BienSo\",\"TenThietBi_BienSo\" FROM {MyConstant.QLMayTB_Danhsachphuongtien}";
            DataTable dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
            DataTable dt2 = dt.Copy();
            dt2.Columns.RemoveAt(dt2.Columns["Code"].Ordinal);
            dataGridView1.DataSource = dt2;
            int i = 0;
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells["luachon_code"].Value = dt.Rows[i]["Code"];
                i++;
            }
            }

        private void btn_chon_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            int result = dataGridView1.Rows.Count;
            for (int i = 0; i < result; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["check"].Value) == true)
                {
                    m_code.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                }
            }
            m_TruyenData(m_code);
            this.Close();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
