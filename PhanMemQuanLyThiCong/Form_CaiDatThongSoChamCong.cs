using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
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

namespace PhanMemQuanLyThiCong
{
    public partial class Form_CaiDatThongSoChamCong : DevExpress.XtraEditors.XtraForm
    {
        private DanhSachNhanVienModel _NV { get; set; }
        public Form_CaiDatThongSoChamCong()
        {
            InitializeComponent();
        }
        public void Fcn_Update(DanhSachNhanVienModel NV)
        {
            if (NV != null)
                this.Text = $"Cài đặt thông số chấm công cho nhân viên:{NV.TenGhep}";
            _NV = NV;
            te_GioChamCong.EditValue=de_NgayChamCong.EditValue = DateTime.Now;
            te_Vao.EditValue = te_Ra.EditValue = DateTime.Now;
            string dbString = $"SELECT {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.*,{DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}.Ten as ChucVu " +
                $"FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}" +
                $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} ON {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.ChucVu={DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}.Code ";
            List<DanhSachNhanVienModel> DanhSachNV = DataProvider.InstanceTHDA.ExecuteQueryModel<DanhSachNhanVienModel>(dbString);
            glue_NhanVien.Properties.DataSource = DanhSachNV;
        }

        private void sb_Ok_Click(object sender, EventArgs e)
        {
            DateTime NgayChamCong = de_NgayChamCong.DateTime;
            DateTime GioVao = default, GioRa = default;
            if (ce_ChamMuti.Checked)
            {
                int [] selectedRows = gridLookUpEdit1View.GetSelectedRows();
                List<DanhSachNhanVienModel> DanhSachNV = selectedRows.Select(x => gridLookUpEdit1View.GetRow(x) as DanhSachNhanVienModel).ToList();
                foreach(var item in DanhSachNV)
                {
                    string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} WHERE" +
$" \"CodeNhanVien\"='{item.Code}' AND \"DateTime\"='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'" +
$" AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"Buoi\"='{cbe_SangChieuToi.Text}'";
                    DataTable ChamCongCheck = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (ce_VaoRa.Checked)
                    {
                        GioVao = te_Vao.Time;
                        GioRa = te_Ra.Time;
                    }
                    else
                    {
                        dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM} WHERE" +
        $"\"NgayBatDau\"<='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND" +
        $" \"NgayKetThuc\">='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND \"CongTruong_VanPhong\"='{item.NhanVienVanPhongCongTruong}' " +
        $"AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"BuoiSang_Chieu_Toi\"='{cbe_SangChieuToi.Text}' ";
                        List<CaiDaGioLamChamCong> GioLamMacDinh = DataProvider.InstanceTHDA.ExecuteQueryModel<CaiDaGioLamChamCong>(dbString);

                        if (cbe_VaoRa.Text == "Vào")
                        {
                            GioVao = te_GioChamCong.Time;
                            GioRa = new DateTime(NgayChamCong.Year, NgayChamCong.Month, NgayChamCong.Day, GioLamMacDinh.FirstOrDefault().GioRa, GioLamMacDinh.FirstOrDefault().PhutRa, 0);
                        }
                        else
                        {
                            GioRa = te_GioChamCong.Time;
                            GioVao = new DateTime(NgayChamCong.Year, NgayChamCong.Month, NgayChamCong.Day, GioLamMacDinh.FirstOrDefault().GioVao, GioLamMacDinh.FirstOrDefault().PhutVao, 0);
                        }
                    }
                    if (TimeSpan.Compare(GioVao.TimeOfDay, GioRa.TimeOfDay) == 1)
                    {
                        MessageShower.ShowWarning("Vui lòng nhập giờ ra lớn hơn giờ vào!!!!!!!!!!!!!!");
                        return;

                    }
                    if (ChamCongCheck.Rows.Count == 0)
                    {
                        dbString = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} " +
    $"(Code,CodeNhanVien,Buoi,DateTime,GioChamCongVao,GioChamCongRa,CodeDuAn) VALUES " +
    $"('{Guid.NewGuid()}','{item.Code}','{cbe_SangChieuToi.Text}','{de_NgayChamCong.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'," +
    $"'{GioVao.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}','{GioRa.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}','{SharedControls.slke_ThongTinDuAn.EditValue}')";
                    }
                    else
                    {
                        dbString = $"UPDATE {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} SET " +
                    $"\"GioChamCongVao\" = '{GioVao.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}', " +
                    $"\"GioChamCongRa\" = '{GioRa.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'" +
                    $" WHERE \"Code\" = '{ChamCongCheck.Rows[0][0]}'";
                    }
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                }
            }
            else
            {
                if (_NV is null)
                    return;
                string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} WHERE" +
$" \"CodeNhanVien\"='{_NV.Code}' AND \"DateTime\"='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
$"AND \"Buoi\"='{cbe_SangChieuToi.Text}' AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                DataTable ChamCongCheck = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (ce_VaoRa.Checked)
                {
                    GioVao = te_Vao.Time;
                    GioRa = te_Ra.Time;
                }
                else
                {
                    dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM} WHERE" +
    $"\"NgayBatDau\"<='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND" +
    $" \"NgayKetThuc\">='{NgayChamCong.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND \"CongTruong_VanPhong\"='{_NV.NhanVienVanPhongCongTruong}' " +
    $"AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"BuoiSang_Chieu_Toi\"='{cbe_SangChieuToi.Text}' ";
                    List<CaiDaGioLamChamCong> GioLamMacDinh = DataProvider.InstanceTHDA.ExecuteQueryModel<CaiDaGioLamChamCong>(dbString);
                                                
                    if (cbe_VaoRa.Text == "Vào")
                    {
                        GioVao = te_GioChamCong.Time;
                        GioRa =new DateTime(NgayChamCong.Year,NgayChamCong.Month,NgayChamCong.Day, GioLamMacDinh.FirstOrDefault().GioRa, GioLamMacDinh.FirstOrDefault().PhutRa,0);
                    }
                    else
                    {
                        GioRa = te_GioChamCong.Time;
                        GioVao = new DateTime(NgayChamCong.Year, NgayChamCong.Month, NgayChamCong.Day, GioLamMacDinh.FirstOrDefault().GioVao, GioLamMacDinh.FirstOrDefault().PhutVao, 0);
                    }
                }
                if(TimeSpan.Compare(GioVao.TimeOfDay,GioRa.TimeOfDay)==1)
                {
                    MessageShower.ShowWarning("Vui lòng nhập giờ ra lớn hơn giờ vào!!!!!!!!!!!!!!");
                    return;
                        
                }    
                if (ChamCongCheck.Rows.Count == 0)
                {
                    dbString = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} " +
$"(Code,CodeNhanVien,Buoi,DateTime,GioChamCongVao,GioChamCongRa,CodeDuAn) VALUES " +
$"('{Guid.NewGuid()}','{_NV.Code}','{cbe_SangChieuToi.Text}','{de_NgayChamCong.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'," +
$"'{GioVao.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}','{GioRa.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}','{SharedControls.slke_ThongTinDuAn.EditValue}')";
                }
                else
                {
                    dbString = $"UPDATE {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} SET " +
                $"\"GioChamCongVao\" = '{GioVao.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}', " +
                $"\"GioChamCongRa\" = '{GioRa.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'" +
                $" WHERE \"Code\" = '{ChamCongCheck.Rows[0][0]}'";
                }
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            this.Close();
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ce_VaoRa_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_VaoRa.Checked)
            {
                te_Ra.Enabled = te_Vao.Enabled = true;
            }
            else
            {
                te_Ra.Enabled = te_Vao.Enabled = false;
            }
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
            //glue_NhanVien.ClosePopup();
        }

        private void ce_ChamMuti_CheckedChanged(object sender, EventArgs e)
        {
            glue_NhanVien.Enabled = ce_ChamMuti.Checked;
        }

        private void glue_NhanVien_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridLookUpEdit1View_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view != null)
                view.GetSelectedRows();
        }

        private void de_NgayChamCong_EditValueChanged(object sender, EventArgs e)
        {
            te_GioChamCong.Time = te_Vao.Time = te_Ra.Time= de_NgayChamCong.DateTime;
        }
    }
}