using DevExpress.XtraBars.ViewInfo;
using DevExpress.XtraSpreadsheet.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PM360.Common.Helper;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_ChonCongTacDinhMuc : Form
    {
        //dt

        IEnumerable<string>  _codeCongTac = null;
        string _codeCTTK = "";
        string _codeHaoPhiCha = "";
        string _colFk = "";
        string _loaiVatTu = null;
        double _heSo = 1;
        double _heSoNguoiDung = 1;
        double _dinhMuc = 1;
        double _dinhMucNguoiDung = 1;
        double _KLKH = 0;
        string _suffix = "";
        HaoPhiVatTuExtensionViewModel _hp;
        public Form_ChonCongTacDinhMuc(string colFk, IEnumerable<string> codeCongTac, string codeCTTK = null, HaoPhiVatTuExtensionViewModel hp = null, string suffix = "")
        {
            DialogResult = DialogResult.No;

            _colFk = colFk;
            _codeCongTac = codeCongTac;
            _codeCTTK = codeCTTK;
            _codeHaoPhiCha = hp?.Code;
            _loaiVatTu = hp?.LoaiVatTu;
            _heSo = hp?.HeSo ?? 1;
            _heSoNguoiDung = hp?.HeSoNguoiDung ?? 1;
            _dinhMuc = hp?.DinhMuc ?? 1;
            _dinhMucNguoiDung = hp?.DinhMucNguoiDung ?? 1;
            _hp = hp;

            _suffix = suffix;

            InitializeComponent();
        }

        public delegate void DE_TRUYENDATAVATTU(DataRow[] dtrs);
        public DE_TRUYENDATAVATTU m_DataChonVL;

        private void Form_ChonCongTacDinhMuc_Load(object sender, EventArgs e)
        {
            cbb_LoaiVatTu.SelectedIndex = 0;
            if (_loaiVatTu != null)
            {
                cbb_LoaiVatTu.Text = _loaiVatTu;
                cbb_LoaiVatTu.Enabled = false;
            }
            uc_ChonTinhThanh1.init();
            LoadData();
        }
        
        private void LoadData()
        {

            string DonGia = (BaseFrom.ProvincesHaveDonGia.Contains(MSETTING.Default.Province))
                ? $", gvl.{MSETTING.Default.Province} AS DonGia "
                : ", 0 AS DonGia";

            string queryStr = $"SELECT vt.* {DonGia} " +
                $"FROM tbl_VatTu vt " +
                $"LEFT JOIN tbl_GiaVatLieu gvl " +
                $"ON vt.MaVatLieu = gvl.MaHieu360";

            var dt = DataProvider.InstanceTBT.ExecuteQuery(queryStr);

            dt.Columns.Add("Chon", typeof(bool));
            dt.Columns["Chon"].DefaultValue = false;

            foreach (DataRow dr in dt.Rows)
            {
                dr["Chon"] = false;
                string LoaiVatLieu = dr["LoaiVatTu"].ToString();
                if (LoaiVatLieu != "Vật liệu" && LoaiVatLieu != "Nhân công")
                {
                    dr["LoaiVatTu"] = "Máy thi công";
                }
            }

            //dgv_TimKiemDinhMuc.DataSource = dt;
            //cbb_LoaiVatTu.SelectedIndex = 0;
            gc_LoadVatLieu.DataSource = dt;

        }

        private void txt_MaVatTu_TextChanged(object sender, EventArgs e)
        {
            fcn_locCongTac();
        }

        private void txt_TenVatTu_TextChanged(object sender, EventArgs e)
        {
            fcn_locCongTac();
        }

        private void cbb_LoaiVatTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_LoaiVatTu.SelectedIndex < 0)
                return;

            fcn_locCongTac();
        }

        private void fcn_locCongTac()
        {
            gc_LoadVatLieu.BeginUpdate();
            if (cbb_LoaiVatTu.Text == "Tất cả")
            {
                col_LoaiVatLieu.ClearFilter();
            }
            else
            {
                col_LoaiVatLieu.FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo($"[LoaiVatTu] = '{cbb_LoaiVatTu.Text}'");
            }


            if (txt_MaVatTu.Text.HasValue())
            {
                string MaVatLieuSearch = MyFunction.fcn_RemoveAccents(txt_MaVatTu.Text).ToUpper();
                ColMaVatLieu.FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo($"[MaVatLieu] LIKE '%{MaVatLieuSearch}%'");
            }
            else
                ColMaVatLieu.ClearFilter();

            if (txt_TenVatTu.Text.HasValue())
            {
                string VatTuSearch = MyFunction.fcn_RemoveAccents(txt_TenVatTu.Text).ToUpper();
                col_VatTuKhongDau.FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo($"[VatTu_KhongDau] LIKE '%{VatTuSearch}%'");
            }
            else
                col_VatTuKhongDau.ClearFilter();
            gc_LoadVatLieu.EndUpdate();

            //DataTable dtSource = (rows.Length > 0) ? rows.CopyToDataTable() : dt.Clone();



            //dtSource.Columns.Add("Chon", typeof(bool));
            //dtSource.Columns["Chon"].SetOrdinal(0);
            //dtSource.AcceptChanges();
            //dgv_TimKiemDinhMuc.DataSource = dtSource;
            //dgv_TimKiemDinhMuc.Columns["Chon"].DisplayIndex = 0;
            //CurrencyManager currencyManager = (CurrencyManager)BindingContext[dgv_TimKiemDinhMuc.DataSource];
            //currencyManager.SuspendBinding();
            //foreach (DataGridViewRow dgvr in dgv_TimKiemDinhMuc.Rows)
            //{
            //    if (dgvr.Cells["MaVatLieu"].Value.ToString().ToUpper().Contains(txt_MaVatTu.Text.ToUpper()) &&
            //        MyFunction.fcn_RemoveAccents(dgvr.Cells["VatTu"].Value.ToString().ToUpper()).Contains(MyFunction.fcn_RemoveAccents(txt_TenVatTu.Text.ToUpper())))
            //    {
            //        if (cbb_LoaiVatTu.Text == "Tất cả")
            //            dgvr.Visible = true;
            //        else
            //            dgvr.Visible = cbb_LoaiVatTu.Text == dgvr.Cells["LoaiVatTu"].Value.ToString();
            //    }
            //    else
            //    {
            //        dgvr.Visible = true;
            //    }
            //}
            //currencyManager.ResumeBinding();
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            DataTable selectedDt = (gc_LoadVatLieu.DataSource as DataTable);
            DataRow[] dataRows = selectedDt.AsEnumerable().Where(x => x["Chon"].ToString() == true.ToString()).ToArray();


            List<Tbl_TDKH_HaoPhiVatTuViewModel> newsHp = new List<Tbl_TDKH_HaoPhiVatTuViewModel>();
            List<string> newCodesHp = new List<string>();

            if (_hp != null)
            {
                string dbString = $"SELECT COUNT(Code) FROM {TDKH.Tbl_HaoPhiVatTu} WHERE CodeHaoPhiCha = '{_hp.Code}'";
                int count = int.Parse(DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0][0].ToString());

                if (count > 0)
                    _dinhMuc = _dinhMucNguoiDung = 0;
                else
                    _dinhMuc = _dinhMucNguoiDung = Math.Round(_dinhMuc/(double)dataRows.Count(), 4);
            }

            foreach (DataRow dr in dataRows)
            {

                foreach (var codeCt in _codeCongTac)
                {
                    long.TryParse(dr["DonGia"].ToString(), out long DonGia);
                    string newGuid = Guid.NewGuid().ToString();
                    newCodesHp.Add(newGuid);

                    var newVT = new Tbl_TDKH_HaoPhiVatTuViewModel()
                    {
                        Code = newGuid,
                        CodeCongTac = codeCt,
                        MaVatLieu = dr["MaVatLieu"].ToString(),
                        VatTu = dr["VatTu"].ToString() + _suffix,
                        LoaiVatTu = dr["LoaiVatTu"].ToString(),
                        DonVi = dr["DonVi"].ToString(),
                        MaTXHienTruong = dr["MaTXHienTruong"].ToString(),
                        DonGia = DonGia,
                        HeSo = _heSo,
                        HeSoNguoiDung = _heSoNguoiDung,
                        DinhMuc = _dinhMuc,
                        DinhMucNguoiDung = _dinhMucNguoiDung,
                        CodeHaoPhiCha = _codeHaoPhiCha
                    };

                    if (_codeCTTK != null)
                        newVT.CodeCongTac = _codeCTTK;
                    else
                        newVT.SetValueByPropName(_colFk, codeCt);

                    if (_codeHaoPhiCha != null)
                    {

                    }

                    newsHp.Add(newVT);
                }
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(newsHp.fcn_ObjToDataTable(), TDKH.Tbl_HaoPhiVatTu);

            TDKHHelper.CapNhatAllVatTuHaoPhi(newCodesHp);

            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void bt_boLoc_Click(object sender, EventArgs e)
        {
            txt_MaVatTu.Text = txt_TenVatTu.Text = "";
            cbb_LoaiVatTu.SelectedIndex = 0;
        }

        private void bt_checkAll_Click(object sender, EventArgs e)
        {

        }

        private void uc_ChonTinhThanh1_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ce_VatLieuMoi_CheckedChanged(object sender, EventArgs e)
        {
            bool isCheck = ce_VatLieuMoi.Checked;
            spn_LocVatTu.Enabled = !isCheck;
            if (isCheck)
            {
                col_LoaiVatLieu.ClearFilter();
                col_VatTuKhongDau.ClearFilter();
                ColMaVatLieu.FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo($"[MaVatLieu] LIKE '%.NEW'");
            }
            else
                fcn_locCongTac();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            
        }
 
    }
}
