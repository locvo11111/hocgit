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
    public partial class FormChiTietThucHienGiaoViec : Form
    {
        //DataProvider m_db = new DataProvider("");

        string m_codeCVCon;
        public FormChiTietThucHienGiaoViec(string codeCVcon)
        {
            InitializeComponent();
            //DataProvider.InstanceTBT.changePath(m_dbPath);
            m_codeCVCon = codeCVcon;
        }

        private void FormChiTietThucHienGiaoViec_Load(object sender, EventArgs e)
        {
            string dbString = $"SELECT * FROM {GiaoViec.TBL_CONGVIECCON} WHERE \"CodeCongViecCon\"='{m_codeCVCon}'";
            DataTable dtCVcon = DataProvider.InstanceTBT.ExecuteQuery(dbString);
            //DataSet dtCVcon1 = DataProvider.InstanceTBT.ExecuteQuery_Dataset(dbString);
            if (dtCVcon.Rows.Count == 0)
            {
                MessageShower.ShowInformation("Không thể tải chi tiết công việc");
                this.Close();
                return;
            }

            //Datagrid
            dbString = $"SELECT \"CodeDauMuc\" FROM {GiaoViec.TBL_CONGVIECCHA} WHERE \"CodeCongViecCha\"='{dtCVcon.Rows[0]["CodeCongViecCha"]}'";
            DataTable dtTenCVCha = DataProvider.InstanceTBT.ExecuteQuery(dbString);
            if (dtTenCVCha.Rows.Count == 0)
            {
                MessageShower.ShowInformation("Không thể tải chi tiết công việc");
                this.Close();
                return;
            }

            dbString = $"SELECT \"CodeDuAn\", \"DauViec\" FROM {GiaoViec.TBL_DauViecLon} WHERE \"Code\"='{dtTenCVCha.Rows[0]["CodeDauMuc"]}'";
            DataTable dtDauViec = DataProvider.InstanceTBT.ExecuteQuery(dbString);
            if (dtDauViec.Rows.Count == 0)
            {
                MessageShower.ShowInformation("Không thể tải chi tiết công việc");
                this.Close();
                return;
            }

            dbString = $"SELECT \"TenDuAn\" FROM {MyConstant.TBL_THONGTINDUAN} WHERE \"Code\"='{dtDauViec.Rows[0]["CodeDuAn"]}'";
            DataTable dtTenDA = DataProvider.InstanceTBT.ExecuteQuery(dbString);
            if (dtTenDA.Rows.Count == 0)
            {
                MessageShower.ShowInformation("Không thể tải chi tiết công việc");
                this.Close();
                return;
            }

            txt_TenDuAn.Text = dtTenDA.Rows[0]["TenDuAn"].ToString();
            txt_CTGV_TenHangMuc.Text = dtDauViec.Rows[0]["DauViec"].ToString();

        }
    }
}
