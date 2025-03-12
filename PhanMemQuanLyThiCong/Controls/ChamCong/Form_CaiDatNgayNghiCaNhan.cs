using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model.ChamCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.ChamCong
{
    public partial class Form_CaiDatNgayNghiCaNhan : DevExpress.XtraEditors.XtraForm
    {
        private DanhSachNhanVienModel _NV { get; set; }
        public Form_CaiDatNgayNghiCaNhan()
        {
            InitializeComponent();
        }

        private void sb_Ok_Click(object sender, EventArgs e)
        {
            DateTime NgayChamCong = de_NgayChamCong.DateTime;
            DateTime GioVao = default, GioRa = default;
            if (ce_ChamMuti.Checked)
            {
                int[] selectedRows = gridLookUpEdit1View.GetSelectedRows();
                List<DanhSachNhanVienModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as DanhSachNhanVienModel).ToList();
                foreach (var itemNV in DanhSachNV)
                {
                    string NghiLam = DanhSachNhanVienConstant.NN[cbe_LyDoNghi.Text];
                    var ids = (cbe_SangChieuToi.Properties.Items.Where(m => m.CheckState == CheckState.Checked)).Select(m => m.Description).ToArray();
                    foreach (var item in ids)
                    {
                        string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} WHERE" +
$" \"CodeNhanVien\"='{itemNV.Code}' AND \"DateTime\"='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND \"Buoi\"='{item}' AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                        DataTable ChamCongCheck = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM} WHERE" +
    $"\"NgayBatDau\"<='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND" +
    $" \"NgayKetThuc\">='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND \"CongTruong_VanPhong\"='{_NV.NhanVienVanPhongCongTruong}' " +
    $"AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"BuoiSang_Chieu_Toi\"='{item}' ";
                        List<CaiDaGioLamChamCong> GioLamMacDinh = DataProvider.InstanceTHDA.ExecuteQueryModel<CaiDaGioLamChamCong>(dbString);
                        GioVao = new DateTime(NgayChamCong.Year, NgayChamCong.Month, NgayChamCong.Day, GioLamMacDinh.FirstOrDefault().GioVao, GioLamMacDinh.FirstOrDefault().PhutVao, 0);
                        GioRa = new DateTime(NgayChamCong.Year, NgayChamCong.Month, NgayChamCong.Day, GioLamMacDinh.FirstOrDefault().GioRa, GioLamMacDinh.FirstOrDefault().PhutRa, 0);
                        if (ChamCongCheck.Rows.Count == 0)
                        {
                            dbString = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} " +
        $"(Code,CodeNhanVien,Buoi,DateTime,GioChamCongVao,GioChamCongRa,CodeDuAn,NghiLam,GhiChu,ChuThich) VALUES " +
        $"('{Guid.NewGuid()}','{_NV.Code}','{item}','{de_NgayChamCong.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'," +
        $"'{GioVao.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}','{GioRa.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'," +
        $"'{SharedControls.slke_ThongTinDuAn.EditValue}','{NghiLam}','{me_GhiChu.Text}','{cbe_LyDoNghi.Text}')";
                        }
                        else
                        {
                            dbString = $"UPDATE {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} SET " +
                        $"\"GioChamCongVao\" = '{GioVao.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}', " +
                        $"\"GioChamCongRa\" = '{GioRa.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'" +
                        $"\"NghiLam\" = '{NghiLam}'" +
                        $"\"GhiChu\" = '{me_GhiChu.Text}'" +
                        $"\"ChuThich\" = '{cbe_LyDoNghi.Text}'" +
                        $" WHERE \"Code\" = '{ChamCongCheck.Rows[0][0]}'";
                        }
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                }
            }
            else
            {
                if (_NV is null)
                    return;
                string NghiLam = DanhSachNhanVienConstant.NN[cbe_LyDoNghi.Text];
                var ids = (cbe_SangChieuToi.Properties.Items.Where(m => m.CheckState == CheckState.Checked)).Select(m => m.Description).ToArray();
                foreach(var item in ids)
                {
                    string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} WHERE" +
$" \"CodeNhanVien\"='{_NV.Code}' AND \"DateTime\"='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND \"Buoi\"='{item}' AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                    DataTable ChamCongCheck = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                    dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM} WHERE" +
$"\"NgayBatDau\"<='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND" +
$" \"NgayKetThuc\">='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND \"CongTruong_VanPhong\"='{_NV.NhanVienVanPhongCongTruong}' " +
$"AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"BuoiSang_Chieu_Toi\"='{item}' ";
                    List<CaiDaGioLamChamCong> GioLamMacDinh = DataProvider.InstanceTHDA.ExecuteQueryModel<CaiDaGioLamChamCong>(dbString);
                    GioVao = new DateTime(NgayChamCong.Year, NgayChamCong.Month, NgayChamCong.Day, GioLamMacDinh.FirstOrDefault().GioVao, GioLamMacDinh.FirstOrDefault().PhutVao, 0);
                    GioRa = new DateTime(NgayChamCong.Year, NgayChamCong.Month, NgayChamCong.Day, GioLamMacDinh.FirstOrDefault().GioRa, GioLamMacDinh.FirstOrDefault().PhutRa, 0);

                    if (ChamCongCheck.Rows.Count == 0)
                    {
                        dbString = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} " +
    $"(Code,CodeNhanVien,Buoi,DateTime,GioChamCongVao,GioChamCongRa,CodeDuAn,NghiLam,GhiChu,ChuThich) VALUES " +
    $"('{Guid.NewGuid()}','{_NV.Code}','{item}','{de_NgayChamCong.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'," +
    $"'{GioVao.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}','{GioRa.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'," +
    $"'{SharedControls.slke_ThongTinDuAn.EditValue}','{NghiLam}','{me_GhiChu.Text}','{cbe_LyDoNghi.Text}')";
                    }
                    else
                    {
                        dbString = $"UPDATE {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} SET " +
                    $"\"GioChamCongVao\" = '{GioVao.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}', " +
                    $"\"GioChamCongRa\" = '{GioRa.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'" +
                    $"\"NghiLam\" = '{NghiLam}'" +
                    $"\"GhiChu\" = '{me_GhiChu.Text}'" +
                    $"\"ChuThich\" = '{cbe_LyDoNghi.Text}'" +
                    $" WHERE \"Code\" = '{ChamCongCheck.Rows[0][0]}'";
                    }
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
            this.Close();
        }
        public void Fcn_LoadData(DanhSachNhanVienModel NV,DateTime Date)
        {
            _NV = NV;
            if (NV != null)
                this.Text = $"Cài đặt ngày nghỉ: {NV.TenGhep}";
            de_NgayChamCong.DateTime = Date;
            string dbString = $"SELECT {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.*,{DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}.Ten as ChucVu " +
                $"FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}" +
                $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} ON {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.ChucVu={DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}.Code ";
            List<DanhSachNhanVienModel> DanhSachNV = DataProvider.InstanceTHDA.ExecuteQueryModel<DanhSachNhanVienModel>(dbString);
            glue_NhanVien.Properties.DataSource = DanhSachNV;
        }
        private void Form_CaiDatNgayNghiCaNhan_Load(object sender, EventArgs e)
        {

        }

        private void glue_NhanVien_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            int[] selectedRows = (((DevExpress.XtraEditors.GridLookUpEditBase)(sender)).Properties).View.GetSelectedRows();
            List<DanhSachNhanVienModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as DanhSachNhanVienModel).ToList();
            var displayText = string.Join(", ", DanhSachNV.Where(x => x.TenGhep != null).Select(x => x.TenGhep));
            glue_NhanVien.Properties.NullText = displayText;
        }

        private void glue_NhanVien_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            int[] selectedRows = (((DevExpress.XtraEditors.GridLookUpEditBase)(sender)).Properties).View.GetSelectedRows();
            List<DanhSachNhanVienModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as DanhSachNhanVienModel).ToList();
            var displayText = string.Join(", ", DanhSachNV.Where(x => x.TenGhep != null).Select(x => x.TenGhep));
            e.DisplayText = displayText;
        }

        private void gridLookUpEdit1View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView view = sender as GridView;
            int i = view.FocusedRowHandle;
            if (view.Columns.Contains(view.FocusedColumn))
            {
                if (view.IsRowSelected(i))
                {
                    view.UnselectRow(i);
                }
                else
                {
                    view.SelectRow(i);
                }
            }
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ce_ChamMuti_CheckedChanged(object sender, EventArgs e)
        {
            glue_NhanVien.Enabled = ce_ChamMuti.Checked;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }




}