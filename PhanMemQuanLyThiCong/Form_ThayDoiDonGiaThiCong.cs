using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhanMemQuanLyThiCong.Common.Constant;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_ThayDoiDonGiaThiCong : Form
    {
        string _codeCT;
        //string _ten;
        //string _ngayBD, _ngayKT;
        string _tblDonGia, _tblMain , _colFK, _colName;
        TypeKLHN _type;
        public Form_ThayDoiDonGiaThiCong(string codePK, TypeKLHN type)
        {
            InitializeComponent();
            _codeCT = codePK;
            //_ten = ten;
            //_ngayBD = dateBD;
            //_ngayKT = dateKT;
            _type= type;
            nud_DonGia.Maximum = decimal.MaxValue;
            switch (type)
            {
                case TypeKLHN.CongTac:
                    _tblMain = TDKH.TBL_ChiTietCongTacTheoKy;
                    _tblDonGia = TDKH.Tbl_DonGiaThiCongHangNgay;
                    _colFK = "CodeCongTacTheoGiaiDoan";
                    _colName = "TenCongTac";
                    break;
                case TypeKLHN.VatLieu:
                    _tblMain = TDKH.TBL_KHVT_VatTu;
                    _tblDonGia = TDKH.TBL_KHVT_DonGia;
                    _colFK = "CodeVatTu";
                    _colName = "VatTu";

                    break;
                case TypeKLHN.HaoPhiVatTu:
                    _tblMain = TDKH.Tbl_HaoPhiVatTu;
                    _tblDonGia = TDKH.Tbl_HaoPhiVatTu_DonGia;
                    _colFK = "CodeHaoPhiVatTu";
                    _colName = "VatTu";

                    break;
                default:
                    MessageShower.ShowInformation("Không hỗ trợ cài đặt đơn giá");
                    this.Close();
                    return;
            }
        }

        private void Form_ThayDoiDonGiaThiCong_Load(object sender, EventArgs e)
        {

            string dbString;// = $"SELECT * FROM {_tblMain} WHERE Code = '{_codeCT}'";
            DataRow drMain;// = DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0];

            if (_type == TypeKLHN.CongTac)
            {

                dbString = $"SELECT COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac, cttk.NgayBatDau, cttk.NgayKetThuc " +
                    //$"" +
                    $"FROM {_tblMain} cttk\r\n" +
                    $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    $"ON cttk.CodeCongTac = dmct.Code " +
                    $"WHERE cttk.Code = '{_codeCT}'";

                drMain = DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0];

                //if (!ValidateHelper.IsDateTime(drMain[TDKH.COL_NgayBatDauThiCong]) || !ValidateHelper.IsDateTime(drMain[TDKH.COL_NgayKetThucThiCong]))
                //{
                //    MessageShower.ShowInformation("Vui lòng nhập NGÀY BẮT ĐẦU và KẾT THÚC THI CÔNG!");
                //    return;
                //}

                DateTime dateBD = DateTime.Parse(drMain[TDKH.COL_NgayBatDau].ToString()); 
                DateTime dateKT = DateTime.Parse(drMain[TDKH.COL_NgayKetThuc].ToString());

                dtp_NBDTC.Value = dateBD;
                dtp_NKTTC.Value = dateKT;
                //dtp_TuNgay.MinDate = dtp_DenNgay.MinDate = dateBD;
                //dtp_TuNgay.MaxDate = dtp_DenNgay.MaxDate = dateKT;
            }
            else
            {
                dbString = $"SELECT * FROM {_tblMain} WHERE Code = '{_codeCT}'";
                drMain = DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0];
                lcgr_Ngay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            txt_Ten.Text = (string)drMain[_colName];
            dbString = $"SELECT * FROM {_tblDonGia} WHERE \"{_colFK}\" = '{_codeCT}' ORDER BY TuNgay ASC";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dt.Columns.Add("TuNgayDate", typeof(DateTime));
            dt.Columns.Add("DenNgayDate", typeof(DateTime));

            foreach (DataRow dr in dt.Rows)
            {
                dr["TuNgayDate"] = DateTime.Parse(dr["TuNgay"].ToString());
                dr["DenNgayDate"] = DateTime.Parse(dr["DenNgay"].ToString());
            }

            gc_DonGia.DataSource = dt;
        }

        private void bt_Luu_Click(object sender, EventArgs e)
        {
            this.FormClosing -= Form_ThayDoiDonGiaThiCong_FormClosing;
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource((gc_DonGia.DataSource as DataTable), _tblDonGia);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            var dr = MessageShower.ShowYesNoQuestion("Bạn có chắc chắn muốn xóa đơn giá này?");

            if (dr == DialogResult.Yes)
            {
                gridView1.DeleteSelectedRows();
            }
        }

        private void dgv_DonGiaDaCaiDat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.Columns[e.ColumnIndex].HeaderText == "Xóa")
            {
                dgv.Rows.RemoveAt(e.RowIndex);
            }
        }

        //private void repoDe_DateBD_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
            
        //}

        private void Form_ThayDoiDonGiaThiCong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageShower.ShowOkCancelInformation("Các thay đổi sẽ không được lưu khi thoát form! Bạn có muốn thoát?", "CẢNH BÁO") != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void bt_ThemDonGia_Click(object sender, EventArgs e)
        {
            DataTable dt = gc_DonGia.DataSource as DataTable;

            DateTime TuNgay = dtp_TuNgay.Value.Date;
            DateTime DenNgay = dtp_DenNgay.Value.Date;

            if (TuNgay > DenNgay)
            {
                MessageShower.ShowInformation("Lỗi: Ngày bắt đầu lớn hơn ngày kết thúc");
                return;
            }

            if (dt.AsEnumerable().Where(x => (DateTime.Parse(x["TuNgay"].ToString()).Date <= TuNgay && DateTime.Parse(x["DenNgay"].ToString()).Date >= TuNgay) ||
            (DateTime.Parse(x["TuNgay"].ToString()).Date <= DenNgay && DateTime.Parse(x["DenNgay"].ToString()).Date >= DenNgay)).Any())
            {
                MessageShower.ShowInformation("Bị trùng ngày. Chỉ được cài đặt đơn giá cho các ngày chưa có đơn giá thủ công");
                return;
            }

            DataRow newRow = dt.NewRow();
            newRow["Code"] = Guid.NewGuid().ToString();
            newRow[_colFK] = _codeCT;
            newRow["TuNgay"] = TuNgay.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            newRow["DenNgay"] = DenNgay.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            newRow["TuNgayDate"] = TuNgay;
            newRow["DenNgayDate"] = DenNgay;
            newRow["DonGia"] = (UInt64)nud_DonGia.Value;
            dt.Rows.Add(newRow);
        }
    }
}
