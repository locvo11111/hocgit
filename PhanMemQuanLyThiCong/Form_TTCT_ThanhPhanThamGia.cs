using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
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
    public partial class Form_TTCT_ThanhPhanThamGia : Form
    {
        //private readonly Entities_QLTC m_EF = new Entities_QLTC();
        //DataProvider m_db = new DataProvider("");
        //DataProvider m_dbSyncFromServer = new DataProvider("");
        string m_codeCT;
        string m_tblDb;
        string m_codeThanhPhan;// Người dùng hay nhóm hay NCC
        GiaoViecTypeEnum m_type;
        string m_colCodeDanhMucLon;
        string m_tbl2GetData;
        public Form_TTCT_ThanhPhanThamGia(string codeCT, GiaoViecTypeEnum type)
        {
            InitializeComponent();
            //this.Text = title;
            m_codeCT = codeCT;
            m_type = type;
            m_codeThanhPhan = "CodeNguoiDung";
            m_colCodeDanhMucLon = "CodeCongTrinh";

            switch (type)
            {
                case GiaoViecTypeEnum.TYPE_THANHPHANTHAMGIA:
                    m_tblDb = MyConstant.TBL_THONGTIN_THANHPHANTHAMGIA;
                    break;
                case GiaoViecTypeEnum.TYPE_NHOMTHAMGIA:
                    m_tblDb = MyConstant.TBL_THONGTIN_NHOMTHAMGIA;
                    m_codeThanhPhan = "CodeNhom";
                    break;
                case GiaoViecTypeEnum.TYPE_GV_KEHOACH_NGUOITHAMGIA:
                    m_tblDb = GiaoViec.TBL_KEHOACH_NGUOITHAMGIA;
                    m_colCodeDanhMucLon = "CodeCongViecCon";
                    break;
                case GiaoViecTypeEnum.TYPE_GV_KEHOACH_NHACUNGCAP:
                    m_tblDb = GiaoViec.TBL_KEHOACH_NHACUNGCAP;
                    m_tbl2GetData = MyConstant.TBL_THONGTINNHACUNGCAP;
                    m_colCodeDanhMucLon = "CodeCongViecCon";
                    m_codeThanhPhan = "CodeNhaCungCap";
                    break;                
                case GiaoViecTypeEnum.TYPE_GV_KEHOACH_NHATHAU:
                    m_tblDb = GiaoViec.TBL_KEHOACH_NHATHAU;
                    m_tbl2GetData = MyConstant.TBL_THONGTINNHATHAU;

                    m_colCodeDanhMucLon = "CodeCongViecCon";
                    m_codeThanhPhan = "CodeNhaThau";
                    break;                
                case GiaoViecTypeEnum.TYPE_GV_KEHOACH_TODOI:
                    m_tblDb = GiaoViec.TBL_KEHOACH_TODOI;
                    m_tbl2GetData = MyConstant.TBL_THONGTINTODOITHICONG;

                    m_colCodeDanhMucLon = "CodeCongViecCon";
                    m_codeThanhPhan = "CodeToDoi";
                    break;
                case GiaoViecTypeEnum.TYPE_GV_QTMH_TIMNCC_NguoiThamGia:
                    m_tblDb = GiaoViec.TBL_GV_QTMH_TimNCC_NguoiThamGia;
                    m_colCodeDanhMucLon = "CodeQuyTrinh";
                    break;                
                case GiaoViecTypeEnum.TYPE_GV_QTMH_CHONNCC_NguoiThamGia:
                    m_tblDb = GiaoViec.TBL_GV_QTMH_ChonNCC_NguoiThamGia;
                    m_colCodeDanhMucLon = "CodeQuyTrinh";
                    break;                
                case GiaoViecTypeEnum.TYPE_GV_QTMH_DUYETPA_NguoiThamGia:
                    m_tblDb = GiaoViec.TBL_GV_QTMH_DuyetPA_NguoiThamGia;
                    m_colCodeDanhMucLon = "CodeQuyTrinh";
                    break;
                default:
                    break;
            }
        }

        private void Form_TTCT_ThanhPhanThamGia_Load(object sender, EventArgs e)
        {
            string dbString = $"SELECT \"{m_codeThanhPhan}\" FROM {m_tblDb} WHERE \"{m_colCodeDanhMucLon}\"='{m_codeCT}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //Chuyển đổi datatable
            string[] arrayCode = dt.AsEnumerable().Select(x => x.Field<string>(m_codeThanhPhan)).ToArray();


            switch (m_type)
            {
                case GiaoViecTypeEnum.TYPE_THANHPHANTHAMGIA:
                case GiaoViecTypeEnum.TYPE_GV_KEHOACH_NGUOITHAMGIA:
                case GiaoViecTypeEnum.TYPE_GV_QTMH_TIMNCC_NguoiThamGia:
                    dbString = $"SELECT \"Code\", \"FullName\", \"Email\" FROM \"{MyConstant.TBL_FROMSERVER_USER}\"";
                    DataTable dtUser1 = DataProvider.InstanceServer.ExecuteQuery(dbString);

                    DataTable dtUser2 = dtUser1.Copy();
                    dgv_thanhPhanChuaThamGia.DataSource = dtUser1;
                    dgv_thanhPhanThamGia.DataSource = dtUser2;
                    break;
                case GiaoViecTypeEnum.TYPE_GV_KEHOACH_NHACUNGCAP:
                case GiaoViecTypeEnum.TYPE_GV_KEHOACH_NHATHAU:
                case GiaoViecTypeEnum.TYPE_GV_KEHOACH_TODOI:
                    dbString = $"SELECT * FROM {m_tbl2GetData}";
                    DataTable dt1 = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    DataTable dt2 = dt1.Copy();
                    if (dt1.Rows.Count <= 0)
                    {
                        MessageShower.ShowInformation("Chưa có thông tin này cho dự án!");
                        this.Close();
                        return;
                    }
                    dgv_thanhPhanChuaThamGia.DataSource = dt1;
                    dgv_thanhPhanThamGia.DataSource = dt2;
                    break;
                default://Nhom tham gia
                    dbString = $"SELECT \"Code\", \"Name\" FROM \"{MyConstant.TBL_FROMSERVER_USER}\"";
                    DataTable dtGroup1 = DataProvider.InstanceServer.ExecuteQuery(dbString);
                    DataTable dtGroup2 = dtGroup1.Copy();
                    dgv_thanhPhanChuaThamGia.DataSource = dtGroup1;
                    dgv_thanhPhanThamGia.DataSource = dtGroup2;
                    break;
            }

            //if (m_type == MyConstant.TYPE_THANHPHANTHAMGIA || m_type ==MyConstant.TYPE_GV_KEHOACH_NGUOITHAMGIA)
            //{
            //    var luser = m_EF.User
            //        .Select(x => new { x.Code, x.FullName, x.Email }).ToList();
            //    var luser1 = m_EF.User
            //        .Select(x => new { x.Code, x.FullName, x.Email }).ToList();
            //    dgv_thanhPhanChuaThamGia.DataSource = luser;
            //    dgv_thanhPhanThamGia.DataSource = luser1;
            //}
            //else if (m_type == MyConstant.TYPE_GV_KEHOACH_NHACUNGCAP)
            //{
            //    dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINNHACUNGCAP}";
            //    DataTable dt1 = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //    DataTable dt2 = dt1.Copy();
            //    if (dt1.Rows.Count <= 0)
            //    {
            //        MessageShower.ShowInformation("Chưa có nhà cung cấp nào cho dự án!");
            //        this.Close();
            //        return;
            //    }    
            //    dgv_thanhPhanChuaThamGia.DataSource = dt1;
            //    dgv_thanhPhanThamGia.DataSource = dt2;
            //}
            //else
            //{
            //    var luser = m_EF.Group
            //        .Select(x => new { x.Code, x.Name, x.LastActive }).ToList();
            //    var luser1 = m_EF.Group
            //        .Select(x => new { x.Code, x.Name, x.LastActive }).ToList();
            //    dgv_thanhPhanChuaThamGia.DataSource = luser;
            //    dgv_thanhPhanThamGia.DataSource = luser1;
            //}



            //dgv_thanhPhanThamGia.Columns["Xoa"].DisplayIndex = dgv_thanhPhanThamGia.Columns.Count - 1;
            //dgv_thanhPhanChuaThamGia.Columns["Them"].DisplayIndex = dgv_thanhPhanChuaThamGia.Columns.Count - 1;

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

            string dbString = $"INSERT INTO {m_tblDb} (\"Code\", \"{m_colCodeDanhMucLon}\", \"{m_codeThanhPhan}\") VALUES ('{Guid.NewGuid()}', '{m_codeCT}', '{dgv_thanhPhanChuaThamGia.Rows[e.RowIndex].Cells["Code"].Value}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dgv_thanhPhanChuaThamGia.DataSource];
            CurrencyManager currencyManager2 = (CurrencyManager)BindingContext[dgv_thanhPhanThamGia.DataSource];
            //Giúp cập nhât bounded-data
            currencyManager1.SuspendBinding();
            currencyManager2.SuspendBinding();
            dgv_thanhPhanChuaThamGia.Rows[e.RowIndex].Visible = false;
            dgv_thanhPhanThamGia.Rows[e.RowIndex].Visible = true;
            currencyManager1.ResumeBinding();
            currencyManager2.ResumeBinding();

        }

        private void dgv_thanhPhanThamGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_thanhPhanThamGia.Columns[e.ColumnIndex].HeaderText != "Xoa")
                return;

            string dbString = $"DELETE FROM {m_tblDb} WHERE (\"{m_colCodeDanhMucLon}\"='{m_codeCT}' AND \"{m_codeThanhPhan}\"='{dgv_thanhPhanChuaThamGia.Rows[e.RowIndex].Cells["Code"].Value}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dgv_thanhPhanChuaThamGia.DataSource];
            CurrencyManager currencyManager2 = (CurrencyManager)BindingContext[dgv_thanhPhanThamGia.DataSource];

            currencyManager1.SuspendBinding();
            currencyManager2.SuspendBinding();
            dgv_thanhPhanChuaThamGia.Rows[e.RowIndex].Visible = true;
            dgv_thanhPhanThamGia.Rows[e.RowIndex].Visible = false;
            currencyManager1.ResumeBinding();
            currencyManager2.ResumeBinding();
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
