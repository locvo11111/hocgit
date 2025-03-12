using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model;
using DevExpress.XtraEditors.Mask;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using StackExchange.Profiling.Internal;

namespace PhanMemQuanLyThiCong
{
    public partial class ctrlTimKiemDinhMuc : UserControl
    {
        const string m_checkColHeader = "Chon";
        public int _type = -1;
        //DataTable m_dataTable;
        string _codeCVCha, _codeHangMuc;
        string _prevCode, _nextCode, _codeNhom, _codePhanTuyen;//, _nextHM, _nextCTac, _nextCTrinh;
        int _indKeHoach, _indKLHangNgay;
        public string _search_Text = "";
        public ctrlTimKiemDinhMuc()
        {
            InitializeComponent();
            //pushProvinces();
        }

        public delegate void DE_TRUYENDATADAUDINHMUC(int type, DataTable dtTbl, string codeHM="", int indKeHoach = -1, int indKLHangNgay = -1, string prevCode = "", string nextCode = "", string codeNhom = "", string codePhanTuyen = null);
        public DE_TRUYENDATADAUDINHMUC m_DataChonDM;

        public void fcn_loadDinhMuc(string searchText)
        {           
            _search_Text = searchText;
            fcn_LoadGrid();
        }

        public void fcn_LoadGrid()
        {
            string queryStr = "";
            DataTable dt = null;

            List<string> strLs = _search_Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> lsCon = new List<string>();
            if (strLs.Count == 1)
            {
                string str = strLs[0];

                lsCon.Add($"(MaDinhMuc LIKE '%{str}%' OR TenKhongDau LIKE '%{str}%')");
                //queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_DINHMUCALL} WHERE (\"MaDinhMuc\" LIKE '%{str}%' OR \"TenKhongDau\" LIKE '%{str}%')";
            }
            else
            {
                string condition = "";
                foreach (string strSearch in strLs)
                {
                    lsCon.Add($"TenKhongDau LIKE '%{strSearch}%'");
                }
                //queryStr += condition.Remove(0, 4);//Xóa bỏ chữ AND sau WHERE
            }

            lsCon.Add($"CTDinhMuc IN ({MyFunction.fcn_Array2listQueryCondition(BaseFrom.lsCTDinhMuc)})");
            queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_DINHMUCALL} WHERE {string.Join(" AND ", lsCon)}";
            Debug.WriteLine($"Query String {queryStr}");
            dt = DataProvider.InstanceTBT.ExecuteQuery(queryStr);
            dt.Columns.Add("Chon", typeof(bool));
            dt.Columns["Chon"].DefaultValue = false;
            
            foreach (DataRow dr in dt.Rows)
            {
                dr["Chon"] = false;
            };

            gc_DinhMuc.DataSource = dt;
        }

        public void fcn_LoadPositionDoBoc(string prevCode, string nextCode, string codeNhom,string codePhanTuyen)
        {

            _prevCode = prevCode;
            _nextCode = nextCode;
            _codeNhom = codeNhom;
            _codePhanTuyen = codePhanTuyen;
        }

        //public void pushProvinces()
        //{
        //    var source = BaseFrom.Provinces.Where(x => x.HaveDonGia).ToList();
        //    slke_TinhThanh.Properties.DataSource = source;
        //    slke_TinhThanh.EditValue = source.Select(x => x.TenKhongDau)
        //        .FirstOrDefault(x => x == MSETTING.Default.Province) ?? MyConstant.DefaultProvince;
        //}

       

        public void fcn_LoadCodeAndType(int type, string CodeHM = "")
        {
            _type = type;
            _codeHangMuc = CodeHM;
        }

        private void bt_cancle_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bt_checkAll_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmChonDinhMuc form = new frmChonDinhMuc();
            if (form.ShowDialog() == DialogResult.OK)
            {
                fcn_LoadGrid();
            }
        }

        private void ctrlTimKiemDinhMuc_Load(object sender, EventArgs e)
        {
            uc_ChonTinhThanh1.init();
        }

        private void ctrlTimKiemDinhMuc_VisibleChanged_1(object sender, EventArgs e)
        {
            if (this.Visible)
                uc_ChonTinhThanh1.init();
        }

        //private void ce_DonGiaTheoTinh_CheckedChanged(object sender, EventArgs e)
        //{
        //    slke_TinhThanh.Enabled = ce_DonGiaTheoTinh.Checked;
        //}

        //private void slke_TinhThanh_EditValueChanged(object sender, EventArgs e)
        //{
        //    var tt = slke_TinhThanh.GetSelectedDataRow() as Provinces;
        //    if (tt is null)
        //    {
        //        if (!MSETTING.Default.Province.Contains("*"))
        //        {
        //            MSETTING.Default.Province = $"*{MSETTING.Default.Province}";
        //            MSETTING.Default.Save();
        //        }
        //    }
        //    else
        //    {
        //        MSETTING.Default.Province = tt.TenKhongDau;
        //        MSETTING.Default.Save();
        //    }    
        //}

        private void bt_uncheckAll_Click(object sender, EventArgs e)
        {

        }

        //private void dgv_TimKiemDinhMuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0 || e.ColumnIndex < 0)
        //    {
        //        return;
        //    }

        //    DataGridViewCell cell = dgv_TimKiemDinhMuc.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //    Debug.WriteLine($"Cell value change {e.ColumnIndex} {cell.Value}");
        //    if (dgv_TimKiemDinhMuc.Columns[e.ColumnIndex].HeaderText == m_checkColHeader)
        //    {
        //        Color cl = ((bool)cell.EditedFormattedValue) ? Color.Yellow : Color.White;
        //        dgv_TimKiemDinhMuc.Rows[e.RowIndex].DefaultCellStyle.BackColor = cl;
        //    }
        //}

        private void bt_OK_Click(object sender, EventArgs e)
        {

            DataRow[] rows = (gc_DinhMuc.DataSource as DataTable).AsEnumerable().Where(x => x["Chon"].ToString() == true.ToString()).ToArray();// (DataTable)dgv_TimKiemDinhMuc.DataSource;
            
            if (rows.Any())
                m_DataChonDM(_type, rows.CopyToDataTable(), _codeHangMuc, _indKeHoach, _indKLHangNgay, _prevCode, _nextCode, _codeNhom, _codePhanTuyen);

            //foreach (DataGridViewRow row in dgv_TimKiemDinhMuc.Rows)
            //{
            //    if ((bool)row.Cells[m_checkColHeader].FormattedValue)
            //        dtTable.Rows.Add(((row.DataBoundItem as DataRowView).Row).ItemArray);
            //}    

            this.Hide();
        }

        private void ctrlTimKiemDinhMuc_Leave(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ctrlTimKiemDinhMuc_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
