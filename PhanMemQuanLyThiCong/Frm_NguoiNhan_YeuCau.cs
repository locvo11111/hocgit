using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit;
using DevExpress.XtraPdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using MWORD = DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong;


namespace PhanMemQuanLyThiCong
{
    public partial class Frm_NguoiNhan_YeuCau : Form
    {
        //DataProvider m_db1 = new DataProvider(@"..\..\..\Database\db_QuanLyThiCong.sqlite");
        public delegate void DE__TRUYENDATAPERSON(DataTable dt, string code, string cot, int hang, DevExpress.XtraSpreadsheet.SpreadsheetControl name, string database);
        string m_code;
        string m_cot;
        int m_hang;
        string m_database;
        DevExpress.XtraSpreadsheet.SpreadsheetControl m_name;
        public DE__TRUYENDATAPERSON m_TruyenDataperson;
        public Frm_NguoiNhan_YeuCau(string code,string cot,int hang, DevExpress.XtraSpreadsheet.SpreadsheetControl name,string database)
        {
            InitializeComponent();
            m_code = code;
            m_cot = cot;
            m_name = name;
            m_hang = hang;
            m_database = database;
        }
        private void Frm_NguoiNhan_YeuCau_Load(object sender, EventArgs e)
        {
            update();
        }
        private void btn_luachon_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView2.DataSource as DataTable;
            int result = dataGridView2.Rows.Count;
            for (int i = 0; i < result; i++)
            {
                if (Convert.ToBoolean(dataGridView2.Rows[i].Cells["check"].Value) == false)
                {
                    dt.Rows.RemoveAt(i);
                    i = i - 1;
                    result = result - 1;
                }
            }
            dt.AcceptChanges();
            m_TruyenDataperson(dt,m_code,m_cot,m_hang,m_name,m_database);
            this.Close();
        }
        private void update()
        {
            //dữ liệu phần chọn hợp đồng
            string[] row = new string[] { "Danh Sách Nhân Viên" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "Danh Sách Quản lý" };
            dataGridView1.Rows.Add(row);
            capnhapsolieu("Tbl_ChamCong_BangNhanVien",true);
        }
        private void capnhapsolieu(string database,bool luachon)
        {
            if(luachon==true)
            {
                string queryStr = $"SELECT \"HoVaTen\",\"ChucVu\",\"PhongBan\" FROM {database}";
                DataTable dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
                int result = dt.Rows.Count;
                dataGridView2.DataSource = dt;
            }
            else
            {
                string queryStr = $"SELECT \"HoVaTen\", \"PhongBan\",\"ChucDanh\" FROM {database}";
                DataTable dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
                int result = dt.Rows.Count;
                dataGridView2.DataSource = dt;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            int hang = e.RowIndex;
            int cot = e.ColumnIndex;
            string task = dataGridView1.Rows[hang].Cells[cot].Value.ToString();
            if (task == "Danh Sách Nhân Viên")
            {
                capnhapsolieu("Tbl_ChamCong_BangNhanVien", true);
            }
            if (task == "Danh Sách Quản lý")
            {
                capnhapsolieu("Tbl_ChamCong_NVQL", false);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (e.RowIndex != i)
                {
                    dataGridView2.Rows[i].Cells["check"].Value = false;
                }
            }
        }
    }
}
