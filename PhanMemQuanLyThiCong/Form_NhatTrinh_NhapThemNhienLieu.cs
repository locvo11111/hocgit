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
    public partial class Form_NhatTrinh_NhapThemNhienLieu : Form
    {
        public delegate void DE__TRUYENDATALK(int luykedau,int luykenhot,int luykexang);
        public DE__TRUYENDATALK m_TruyenDataLK;
        int m_luykedau=0, m_luykenhot=0, m_luykexang=0;
        public Form_NhatTrinh_NhapThemNhienLieu()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string noidung = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string tencot = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            string code = Guid.NewGuid().ToString();
            string queryStr = $"SELECT *  FROM Tbl_Nhapnhienlieu";
            DataTable dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
            int result = dt.Rows.Count;
            if (result >= (e.RowIndex + 1))
            {
                string dbString = $"UPDATE  Tbl_Nhapnhienlieu SET '{tencot}'='{noidung}' WHERE \"Code\"='{dataGridView1.Rows[e.RowIndex].Cells["NTNL_Code"].Value.ToString()}'";
                DataProvider.InstanceTHDA. ExecuteQuery(dbString);
            }
            else
            {
                string dbString = $"INSERT INTO Tbl_Nhapnhienlieu (\"Code\",'{tencot}') VALUES ('{code}','{noidung}')";
                DataProvider.InstanceTHDA. ExecuteQuery(dbString);
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = (result + 1).ToString();
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = code;
            }
        }

        private void btn_Nhap_Click(object sender, EventArgs e)
        {
            string code = Guid.NewGuid().ToString();
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (row.Index < dataGridView1.Rows.Count-1)
                {
                    m_luykedau = m_luykedau + Int32.Parse(row.Cells["Dau"].Value.ToString());
                    m_luykenhot = m_luykenhot + Int32.Parse(row.Cells["Nhot"].Value.ToString());
                    m_luykexang = m_luykexang + Int32.Parse(row.Cells["Xang"].Value.ToString());
                }
            }
            string queryStr = $"SELECT *  FROM Tbl_Tonghopluykenhienlieu";
            DataTable dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
            m_TruyenDataLK(m_luykedau, m_luykenhot, m_luykexang);
            if (dt.Rows.Count == 1)
            {
                string dbString = $"UPDATE  Tbl_Tonghopluykenhienlieu SET \"LuyKeDau\"='{m_luykedau}',\"LuyKeNhot\"='{m_luykenhot}',\"LuyKeXang\"='{m_luykexang}'";
                DataProvider.InstanceTHDA. ExecuteQuery(dbString);
            }
            else
            {
                string dbString = $"INSERT INTO Tbl_Tonghopluykenhienlieu (\"Code\",\"LuyKeDau\",\"LuyKeNhot\",\"LuyKeXang\") VALUES ('{code}','{m_luykedau}','{m_luykenhot}','{m_luykexang}')";
                DataProvider.InstanceTHDA. ExecuteQuery(dbString);
            }
            this.Close();
        }
        DateTimePicker oDateTimePicker = new DateTimePicker();
        private void oDateTimePicker_CloseUp1(object sender, EventArgs e)
        {
            // Hiding the control after use   
            oDateTimePicker.Visible = false;
        }
        private void taodatepicker1(int hang, int cot)
        {
            //Initialized a new DateTimePicker Control  
            oDateTimePicker = new DateTimePicker();

            //Adding DateTimePicker control into DataGridView   
            dataGridView1.Controls.Add(oDateTimePicker);

            // Setting the format (i.e. 2014-10-10)  
            oDateTimePicker.Format = DateTimePickerFormat.Short;
            // It returns the retangular area that represents the Display area for a cell  
            Rectangle oRectangle = dataGridView1.GetCellDisplayRectangle(cot, hang, true);

            //Setting area for DateTimePicker Control  
            oDateTimePicker.Size = new Size(oRectangle.Width, oRectangle.Height);

            // Setting Location  
            oDateTimePicker.Location = new Point(oRectangle.X, oRectangle.Y);

            // An event attached to dateTimePicker Control which is fired when DateTimeControl is closed  
            oDateTimePicker.CloseUp += new EventHandler(oDateTimePicker_CloseUp1);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            int cot = e.ColumnIndex;
            int hang = e.RowIndex;
            if (e.ColumnIndex == 2)
            {
                taodatepicker1(hang, cot);
                // An event attached to dateTimePicker Control which is fired when any date is selected  
                oDateTimePicker.TextChanged += new EventHandler(dateTimePicker_OnTextChange);

                // Now make it visible  
                oDateTimePicker.Visible = true;
            }
        }
        private void dateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            // Saving the 'Selected Date on Calendar' into DataGridView current cell  
            dataGridView1.CurrentCell.Value = oDateTimePicker.Text.ToString();
            int hang = dataGridView1.CurrentRow.Index;
            updatevaodatabase("NgayThang", oDateTimePicker.Text.ToString(), hang);
        }

        private void Form_NhatTrinh_NhapThemNhienLieu_Load(object sender, EventArgs e)
        {
            fcn_loadatadexuatphuongtien();
        }

        private void updatevaodatabase(string vitri, string noidung, int hang)
        {
            string queryStr = $"SELECT * FROM Tbl_Nhapnhienlieu";
            DataTable dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
            int result = dt.Rows.Count;
            string code = Guid.NewGuid().ToString();
            if (result < hang + 1)
            {
                string dbString = $"INSERT INTO Tbl_Nhapnhienlieu (\"Code\",'{vitri}') VALUES ('{code}','{noidung}')";
                DataProvider.InstanceTHDA. ExecuteQuery(dbString);
                dataGridView1.Rows[hang].Cells[0].Value = code;
                dataGridView1.Rows[hang].Cells[1].Value = (result + 1).ToString();
            }
            else
            {
                string dbString = $"UPDATE  Tbl_Nhapnhienlieu SET '{vitri}'='{noidung}' WHERE \"Code\"='{dt.Rows[hang]["Code"].ToString()}'";
                DataProvider.InstanceTHDA. ExecuteQuery(dbString);
            }
        }
        private void fcn_loadatadexuatphuongtien()
        {
            string queryStr = $"SELECT * FROM Tbl_Nhapnhienlieu";
            DataTable dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
            DataTable dt2 = dt.Copy();
            dt2.Columns.RemoveAt(dt2.Columns["Code"].Ordinal);
            dt2.AcceptChanges();
            dataGridView1.DataSource = dt2;
            int i = 0;
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (dt.Rows.Count > i)
                {
                    row.Cells["NTNL_STT"].Value = row.Index + 1;
                    row.Cells["NTNL_Code"].Value = dt.Rows[i]["Code"];
                    i++;
                }
            }
        }
    }
}
