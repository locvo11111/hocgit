using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
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
    public partial class Form_Luachonhopdong : Form
    {
        string m_codeduan;
        int m_hang = 0;
        public delegate void DE__TRUYENDATAVL(DataTable dt,int hang);
        public DE__TRUYENDATAVL m_TruyenDatavl;
        public Form_Luachonhopdong(string codeduan,int hang)
        {
            InitializeComponent();
            m_codeduan = codeduan;
            m_hang = hang;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            DataTable dt = dtgv_danhsachhopdong.DataSource as DataTable;
            int result = dtgv_danhsachhopdong.Rows.Count;
            for (int i = 0; i < result; i++)
            {
                if (Convert.ToBoolean(dtgv_danhsachhopdong.Rows[i].Cells["check"].Value) == false)
                {
                    dt.Rows.RemoveAt(i);
                    i = i - 1;
                    result = result - 1;
                }
            }
            dt.AcceptChanges();
            m_TruyenDatavl(dt,m_hang);
            this.Close();
        }
        private void loaddanhsachhopdong()
        {
            string queryDbStr = $"SELECT \"Code\",\"TenHopDong\",\"SoHopDong\" FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"CodeDuAn\"='{m_codeduan}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryDbStr);
            dtgv_danhsachhopdong.DataSource = dt;
        }

        private void Form_Luachonhopdong_Load(object sender, EventArgs e)
        {
            loaddanhsachhopdong();
        }

        private void dtgv_danhsachhopdong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            for (int i = 0; i < dtgv_danhsachhopdong.Rows.Count; i++)
            {
                if (e.RowIndex != i)
                {
                    dtgv_danhsachhopdong.Rows[i].Cells["check"].Value = false;
                }
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
