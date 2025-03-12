using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
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
    public partial class Form_QLVT_NhatKy : DevExpress.XtraEditors.XtraForm
    {
        private string m_code,m_TVT;
        public Form_QLVT_NhatKy(string code,string tenvattu)
        {
            InitializeComponent();
            m_code = code;
            m_TVT = tenvattu;
        }

        private void Form_QLVT_NhatKy_Load(object sender, EventArgs e)
        {
            string dbString = $"SELECT *  FROM {QLVT.TBL_QLVT_NKVC} WHERE \"CodeCha\"='{m_code}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dt.Columns.Add("TenVatTu", typeof(string));
            dt.Columns["Code"].ColumnName = "ID";
            dt.Columns["CodeCha"].ColumnName = "ParentID";
            double luyke = 0;
            foreach (DataRow row in dt.Rows)
                luyke += double.Parse(row["ThucTeVanChuyen"].ToString());
            DataRow newdt = dt.NewRow();
            newdt["TenVatTu"] = m_TVT;
            newdt["ID"] = m_code;
            newdt["ThucTeVanChuyen"] = luyke;
            newdt["ParentID"] = "0";
            dt.Rows.Add(newdt);
            Tree_NhatKy.DataSource = dt;
        }
    }
}