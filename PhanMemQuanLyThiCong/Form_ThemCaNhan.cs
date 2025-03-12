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
    public partial class Form_ThemCaNhan : Form
    {
        //private readonly Entities_QLTC m_EF = new Entities_QLTC();
        //DataProvider m_db = new DataProvider("");
        //DataProvider m_dbSyncFromServer = new DataProvider("");

        string m_codeDanhMucLon;//mục mà người dùng trực thuộc
        string m_tblDb;
        string m_colCodeLoaiNguoiDung; //Cột thành phần cần chọn: Vd: Người tham gia, người phụ trách
        string m_colCodeDanhMucLon; //Mã để nhận biết người dùng. Ví dụ cần thêm user cho công trình hay cho công việc .....
        public Form_ThemCaNhan(string codeDanhMucLon, string tblDb, string col2getUser, string colCodeLoaiNguoiDung)
        {
            InitializeComponent();
            m_codeDanhMucLon = codeDanhMucLon;
            m_tblDb = tblDb;
            m_colCodeDanhMucLon = col2getUser;
            m_colCodeLoaiNguoiDung = colCodeLoaiNguoiDung;
        }

        private void Form_TTCT_ThanhPhanThamGia_Load(object sender, EventArgs e)
        {

            string dbString = $"SELECT \"Code\", \"FullName\", \"Email\" FROM \"{MyConstant.TBL_FROMSERVER_USER}\"";
            DataTable dtUser1 = DataProvider.InstanceServer.ExecuteQuery(dbString);

            DataTable dtUser2 = dtUser1.Copy();
            dgv_thanhPhanChuaThamGia.DataSource = dtUser1;
            dgv_thanhPhanThamGia.DataSource = dtUser2;



            dgv_thanhPhanThamGia.Columns["Xoa"].DisplayIndex = dgv_thanhPhanThamGia.Columns.Count - 1;
            dgv_thanhPhanChuaThamGia.Columns["Them"].DisplayIndex = dgv_thanhPhanChuaThamGia.Columns.Count - 1;


            dbString = $"SELECT \"{m_colCodeLoaiNguoiDung}\" FROM {m_tblDb} WHERE \"{m_colCodeDanhMucLon}\"='{m_codeDanhMucLon}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            if (dt.Rows.Count == 0)
            {
                MessageShower.ShowInformation("Lỗi tải người dùng!");
                return;
            }

            //Chuyển thành array
            string[] arrayCode = dt.AsEnumerable().Select(x => x.Field<string>(m_colCodeLoaiNguoiDung)).ToArray();
            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dgv_thanhPhanChuaThamGia.DataSource];
            CurrencyManager currencyManager2 = (CurrencyManager)BindingContext[dgv_thanhPhanThamGia.DataSource];

            foreach (DataGridViewRow row in dgv_thanhPhanThamGia.Rows)
            {
                currencyManager1.SuspendBinding();
                currencyManager2.SuspendBinding();

                if (arrayCode.Contains(row.Cells["Code"].Value.ToString()))
                {
                    row.Visible = true;
                    dgv_thanhPhanChuaThamGia.Rows[row.Index].Visible = false;
                }
                else
                {
                    row.Visible = false;
                    dgv_thanhPhanChuaThamGia.Rows[row.Index].Visible = true;
                }

                currencyManager1.ResumeBinding();
                currencyManager2.ResumeBinding();
            }
        }

        

        private void dgv_thanhPhanChuaThamGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_thanhPhanChuaThamGia.Columns[e.ColumnIndex].HeaderText != "Thêm")
                return;

            string dbString = $"UPDATE {m_tblDb} SET \"{m_colCodeLoaiNguoiDung}\"='{dgv_thanhPhanChuaThamGia.Rows[e.RowIndex].Cells["Code"].Value}' " +
                $"WHERE \"{m_colCodeDanhMucLon}\"='{m_codeDanhMucLon}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            Form_TTCT_ThanhPhanThamGia_Load(null, null);

            //CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dgv_thanhPhanChuaThamGia.DataSource];
            //CurrencyManager currencyManager2 = (CurrencyManager)BindingContext[dgv_thanhPhanThamGia.DataSource];

            //currencyManager1.SuspendBinding();
            //currencyManager2.SuspendBinding();
            //dgv_thanhPhanChuaThamGia.Rows[e.RowIndex].Visible = false;
            //dgv_thanhPhanThamGia.Rows[e.RowIndex].Visible = true;
            //currencyManager1.ResumeBinding();
            //currencyManager2.ResumeBinding();

        }

        private void dgv_thanhPhanThamGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_thanhPhanThamGia.Columns[e.ColumnIndex].HeaderText != "Xoa")
                return;

            string dbString = $"UPDATE {m_tblDb} SET \"{m_colCodeLoaiNguoiDung}\"='\"\"' " +
                $"WHERE \"{m_colCodeDanhMucLon}\"='{m_codeDanhMucLon}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            Form_TTCT_ThanhPhanThamGia_Load(null, null);
        }

        private void bt_LuuThayDoi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn hủy bỏ các thay đổi?", "Cảnh báo");
            if (rs == DialogResult.OK)
                this.Close();
        }
    }
}
