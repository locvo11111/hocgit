using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_ThemNCCChoQTMH : Form
    {
        //DataProvider m_db = new DataProvider("");
        string m_codeQT;//Code quy trình
        public Form_ThemNCCChoQTMH(string codeQT)
        {
            InitializeComponent();
            m_codeQT = codeQT;
            fcn_loadVT();
            fcn_RefreshNCC();

            //fcn_RefreshNCCDaChon();
            ///Cập nhật vật tư

        }

        private void bt_LuuThayDoiNCC_Click(object sender, EventArgs e)
        {
            DataTable dt = dgv_listNCC.DataSource as DataTable;
            foreach (DataRow r in dt.Rows)
            {
                if (r["Code"].ToString() == "")
                    r["Code"] = Guid.NewGuid().ToString();
                if (r["CodeQuyTrinh"].ToString() == "")
                    r["CodeQuyTrinh"] = m_codeQT;
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, MyConstant.TBL_GV_QTMH_ListNCC);
            fcn_RefreshNCC();
        }

        private void fcn_RefreshNCC()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_GV_QTMH_ListNCC} WHERE \"CodeQuyTrinh\"='{m_codeQT}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dgv_listNCC.DataSource = dt;
           
            Dictionary<string, string> DIC_NCC = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                DIC_NCC.Add(r["Code"].ToString(), r["NhaCungCap"].ToString());
            }
            DataGridViewComboBoxColumn col = (dgv_NCCDaChon.Columns["NCC"] as DataGridViewComboBoxColumn);

            col.DataSource = DIC_NCC.ToList();
            col.DisplayMember = "Value";
            col.ValueMember = "Key";

            fcn_RefreshNCCDaChon();


        }

        private void fcn_RefreshNCCDaChon()
        {
            dgv_NCCDaChon.CellValueChanged -= dgv_NCCDaChon_CellValueChanged;
            string dbString = $"SELECT * FROM {MyConstant.TBL_GV_QTMH_NCCDaChon} WHERE \"CodeQuyTrinh\"='{m_codeQT}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            foreach (DataRow r in dt.Rows)
            {
                dgv_NCCDaChon.Rows.Add();
                
                dgv_NCCDaChon.Rows[dgv_NCCDaChon.Rows.Count - 2].Cells["Code"].Value = r["Code"].ToString();
                dgv_NCCDaChon.Rows[dgv_NCCDaChon.Rows.Count - 2].Cells["NCC"].Value = r["CodeNhaCungCap"].ToString();
                dgv_NCCDaChon.Rows[dgv_NCCDaChon.Rows.Count - 2].Cells["VT"].Value = r["CodeVatTu"].ToString();
                dgv_NCCDaChon.Rows[dgv_NCCDaChon.Rows.Count - 2].Cells["KL"].Value = r["KhoiLuong"].ToString();
                dbString = $"SELECT \"DonViTinh\" FROM {MyConstant.TBL_GV_QTMH_DeXuat_VatTu} WHERE \"Code\"='{r["CodeVatTu"]}'";
                DataTable dt1 = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dt1.Rows.Count > 0)
                    dgv_NCCDaChon.Rows[dgv_NCCDaChon.Rows.Count - 1].Cells["DV"].Value = dt1.Rows[0][0].ToString();
            }
            dgv_NCCDaChon.CellValueChanged += dgv_NCCDaChon_CellValueChanged;
        }

        private void fcn_loadVT()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_GV_QTMH_DeXuat_VatTu} WHERE \"CodeQuyTrinh\"='{m_codeQT}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            Dictionary<string, string> DIC_VT = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                DIC_VT.Add(r["Code"].ToString(), r["VatTu"].ToString());
            }
            DataGridViewComboBoxColumn col1 = (dgv_NCCDaChon.Columns["VT"] as DataGridViewComboBoxColumn);
            col1.DataSource = DIC_VT.ToList();
            col1.DisplayMember = "Value";
            col1.ValueMember = "Key";
        }

        private void bt_LuuNCCDaChon_Click(object sender, EventArgs e)
        {
            
            string dbString = $"SELECT * FROM {MyConstant.TBL_GV_QTMH_NCCDaChon}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataTable dtNew = dt.Clone();
            foreach (DataGridViewRow r in dgv_NCCDaChon.Rows)
            {
                if (r.Index == dgv_NCCDaChon.RowCount - 1)
                    continue;
                string code = (string.IsNullOrEmpty(r.Cells["Code"].Value as string)) ? Guid.NewGuid().ToString() : r.Cells["Code"].Value.ToString();
                
                string NCC = (r.Cells["NCC"] as DataGridViewComboBoxCell).Value as string;
                string VT = (r.Cells["VT"] as DataGridViewComboBoxCell).Value as string;
                string KL = r.Cells["KL"].Value as string;
                dtNew.Rows.Add(code, m_codeQT, NCC, VT, KL);
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtNew, MyConstant.TBL_GV_QTMH_NCCDaChon);
        }

        private void dgv_NCCDaChon_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dgv_NCCDaChon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            int crRow = e.RowIndex;
            string heading = dgv_NCCDaChon.Columns[e.ColumnIndex].HeaderText;
            string value = dgv_NCCDaChon.Rows[crRow].Cells[e.ColumnIndex].Value.ToString();
            
            switch (heading)
            {
                case "NhaCungCap":
                    heading = "CodeNhaCungCap";
                    //value = dgv_NCCDaChon.Rows[crRow].Cells[e.ColumnIndex].Value.ToString();
                    break;
                case "VatTu":
                    heading = "CodeVatTu";
                    //value = dgv_NCCDaChon.Rows[crRow].Cells[e.ColumnIndex].Value.ToString();
                    break;
                default:
                    break;
            }    
            string newCode = Guid.NewGuid().ToString();
            string dbString;
            if (string.IsNullOrEmpty(dgv_NCCDaChon.Rows[crRow].Cells["Code"].Value as string))
            {
                dgv_NCCDaChon.Rows[crRow].Cells["Code"].Value = newCode;
                dbString = $"INSERT INTO {MyConstant.TBL_GV_QTMH_NCCDaChon} (\"Code\", \"CodeQuyTrinh\",\"{heading}\") VALUES ('{newCode}', '{m_codeQT}','{value}')";
            }
            else
            {
                dbString = $"UPDATE {MyConstant.TBL_GV_QTMH_NCCDaChon} SET \"{heading}\"='{value}' WHERE \"Code\"='{dgv_NCCDaChon.Rows[crRow].Cells["Code"].Value}'";
            }
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
        }

        private void Form_ThemNCCChoQTMH_FormClosing(object sender, FormClosingEventArgs e)
        {
            dgv_NCCDaChon.EndEdit();
        }
    }
}
