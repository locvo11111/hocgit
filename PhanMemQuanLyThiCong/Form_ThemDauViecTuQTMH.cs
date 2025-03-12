using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
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
    public partial class Form_ThemDauViecTuQTMH : Form
    {
        //DataProvider m_db = new DataProvider("");
        string m_codeDM;//Code đầu mục
        public Form_ThemDauViecTuQTMH(string codeDM)
        {
            InitializeComponent();
            //DataProvider.InstanceTHDA.changePath(dbPath);
            m_codeDM = codeDM;
        }

        private void Form_ThemDauViecTuQTMH_Load(object sender, EventArgs e)
        {
            string dbString = $"SELECT \"Code\", \"TenQuyTrinh\", \"ThoiDiemGuiDeXuat\" FROM {MyConstant.TBL_GV_QTMH_ThongTin}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //dt.Columns.Add("Bắt đầu", typeof(string));
            
            dgv_ListQTMH.DataSource = dt;
            dgv_ListQTMH.Rows[0].Selected = true;
        }

        private void dgv_ListQTMH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_ListQTMH.Rows[e.RowIndex].Selected = true;
            string codeQT = dgv_ListQTMH.Rows[e.RowIndex].Cells["Code"].Value.ToString();
            string dbString = $"SELECT * FROM {MyConstant.TBL_GV_QTMH_DeXuat_VatTu} WHERE \"CodeQuyTrinh\"='{codeQT}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count ==0)
            {
                return;
            }    
            txt_TenCongTac.Text = dgv_ListQTMH.Rows[e.RowIndex].Cells["TenQuyTrinh"].Value.ToString(); ;
            dgv_listVatTuDeXuat.DataSource = dt;
            foreach (DataGridViewRow r in dgv_listVatTuDeXuat.Rows)
            {
                r.DefaultCellStyle.BackColor = Color.Yellow;
                r.Cells["Chon"].Value = true;
            }    
        }

        private void dgv_listVatTuDeXuat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dgv_listVatTuDeXuat.Columns[e.ColumnIndex].HeaderText == "Chon")
            {
                dgv_listVatTuDeXuat.Rows[e.RowIndex].DefaultCellStyle.BackColor = ((bool)dgv_listVatTuDeXuat.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue) ? Color.Yellow : Color.LightGray;
            }
        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bt_Thêm_Click(object sender, EventArgs e)
        {
            if (dgv_listVatTuDeXuat.RowCount == 0)
            {
                MessageShower.ShowInformation("Không có vật tư để thêm vào công tác! Vui lòng thêm vật tư tại Quy trình mua hàng");
                return;
            }    
            if (txt_TenCongTac.Text == "")
            {
                MessageShower.ShowInformation("Vui lòng nhập tên cho công tác");
                return;
            }

            string codeCVCha = Guid.NewGuid().ToString();
            string dbString = $"INSERT INTO {GiaoViec.TBL_CONGVIECCHA} (\"CodeCongViecCha\", \"CodeDauMuc\", \"TenCongViec\") VALUES ('{codeCVCha}', '{m_codeDM}', '{txt_TenCongTac.Text}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            string tgGuiDX = dgv_ListQTMH.SelectedRows[0].Cells["ThoiDiemGuiDeXuat"].Value.ToString();
            //string tenCV = 
            foreach (DataGridViewRow r in dgv_listVatTuDeXuat.Rows)
            {
                if ((bool)r.Cells["Chon"].EditedFormattedValue)
                {
                    dbString = $"INSERT INTO {GiaoViec.TBL_CONGVIECCON} (\"CodeCongViecCon\", \"CodeCongViecCha\", \"TenCongViec\",\"Bắt đầu\", \"Kết thúc\") VALUES ('{Guid.NewGuid()}', '{codeCVCha}', '{r.Cells["VatTu"].Value}','{tgGuiDX}','{r.Cells["ThoiGianCanVatTu"].Value}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
            this.Close();
        }
    }
}
