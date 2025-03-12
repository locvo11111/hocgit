using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.MayThiCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.MTC
{
    public partial class XtraForm_NhapNhienLieuHangNgay : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_NhapNhienLieuHangNgay()
        {
            InitializeComponent();
            GridLocalizer.Active = new CustomGridLocalizer();
        }
        private class CustomGridLocalizer : GridLocalizer
        {
            public override string GetLocalizedString(GridStringId id)
            {
                if (id == GridStringId.CheckboxSelectorColumnCaption)
                {
                    return "Lựa chọn hiển thị thông báo";
                }
                return base.GetLocalizedString(id);
            }
        }
        private void Fcn_LoadDaTaTongHop()
        {
            string Dbstring = $"SELECT ROW_NUMBER() OVER(ORDER BY TH.Code) AS STT,TH.* " +
               $"FROM {MyConstant.TBL_MTC_NHIENLIEU} TH ";
            List<MTC_TongHopNhienLieuHangNgay> Tong = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_TongHopNhienLieuHangNgay>(Dbstring);
            foreach (var item in Tong)
            {
                item.KhoiLuongDaDung =MyFunction.Fcn_UpdateDaTaTongHopNhienLieuPhu(item.Code);
                item.KhoiLuongDaNhap =MyFunction.Fcn_CalCulateAll(item.Code);
            }
            gc_DanhSachNhienLieu.DataSource = Tong;
        }
        private void cb_DanhSachNhienLieuChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int indOfTableLayout = 0;
            bool isMucLon = int.TryParse(cb.Name.Substring(cb.Name.Length - 1), out indOfTableLayout); //Có phải mục lớn không: Ví dụ ĐỀ XUẤT
            string TienTo = "Hiện";/*(isMucLon) ? "Thêm " : "Thêm quy trình ";*/
            string nameSearch = cb.Name.Replace("cb", "");
            bool isHasControl = false;
            foreach (Control ctrl in cb.Parent.Controls)
            {
                if (ctrl.Name.EndsWith(nameSearch) && ctrl != cb)
                {
                    isHasControl = true;
                    ctrl.Visible = cb.Checked;


                    if (!cb.Checked)
                    {
                        cb.Text = $"Hiện {cb.Text.Replace("Ẩn ", "")}";
                        cb.ForeColor = Color.Red;


                    }
                    else
                    {
                        cb.Text = $"Ẩn {cb.Text.Replace("Hiện ", "")}";
                        cb.ForeColor = Color.Black;

                    }
                }
            }
        }
        private void Fcn_UpdateHangNgay(string CodeNhienLieu)
        {
            string Dbstring = $"SELECT ROW_NUMBER() OVER(ORDER BY HN.Code) AS STT,HN.* " +
          $"FROM {MyConstant.TBL_MTC_NHIENLIEUHANGNGAY} HN WHERE HN.CodeNhienLieu='{CodeNhienLieu}' ORDER BY HN.Ngay ASC";
            List<MTC_NhienLieuHangNgay> HN = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_NhienLieuHangNgay>(Dbstring);
            gc_DanhSachNhienLieuChiTiet.DataSource = HN;
        }
        private void gv_DanhSachNhienLieu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            MTC_TongHopNhienLieuHangNgay TH = gv_DanhSachNhienLieu.GetRow(e.FocusedRowHandle) as MTC_TongHopNhienLieuHangNgay;
            if (TH is null)
                return;
            Fcn_UpdateHangNgay(TH.Code);
        }

        private void XtraForm_NhapNhienLieuHangNgay_Load(object sender, EventArgs e)
        {
            Fcn_LoadDaTaTongHop();
        }

        private void bt_Delete_Click(object sender, EventArgs e)
        {

            MTC_NhienLieuHangNgay HN = gv_DanhSachNhienLieuChiTiet.GetFocusedRow() as MTC_NhienLieuHangNgay;
            if (HN is null)
                return;
            DialogResult RS=MessageShower.ShowYesNoQuestion($"Bạn có muốn xóa khối lượng nhiên liệu Ngày {HN.Ngay.Value.ToShortDateString()} ");
            if (RS == DialogResult.No)
                return;
            List<MTC_TongHopNhienLieuHangNgay> Tong = gc_DanhSachNhienLieu.DataSource as List<MTC_TongHopNhienLieuHangNgay>;
            MTC_TongHopNhienLieuHangNgay ChiTiet = Tong.Where(x => x.Code == HN.CodeNhienLieu).SingleOrDefault();
            ChiTiet.KhoiLuongDaNhap = ChiTiet.KhoiLuongDaNhap - HN.KhoiLuong;
            string db_string = $"DELETE FROM {MyConstant.TBL_MTC_NHIENLIEUHANGNGAY} WHERE \"Code\"='{HN.Code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            gv_DanhSachNhienLieuChiTiet.DeleteSelectedRows();
            gc_DanhSachNhienLieu.RefreshDataSource();
            gc_DanhSachNhienLieu.Refresh();
//            int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_TongHopNhienLieuHangNgay>("UPDATE_MTC_TongHopNhienLieu",
//Tong, false, true, true);
        }

        private void sb_LuuDuLieu_Click(object sender, EventArgs e)
        {
            //List<MTC_TongHopNhienLieuHangNgay> Tong = gc_DanhSachNhienLieu.DataSource as List<MTC_TongHopNhienLieuHangNgay>;
            //int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_TongHopNhienLieuHangNgay>("UPDATE_MTC_TongHopNhienLieu",
            //    Tong, false, true, true);
            this.Close();
        }

        private void sb_ThemNhienLieu_Click(object sender, EventArgs e)
        {
            List<MTC_TongHopNhienLieuHangNgay> Tong = gc_DanhSachNhienLieu.DataSource as List<MTC_TongHopNhienLieuHangNgay>;
            foreach(var item in Tong)
            {
                if (item.KhoiLuong == 0)
                    continue;
                item.KhoiLuongDaNhap = item.KhoiLuongDaNhap + item.KhoiLuong;
                string dbString = $"INSERT INTO {MyConstant.TBL_MTC_NHIENLIEUHANGNGAY} " +
$"(Code,CodeNhienLieu,Ngay,KhoiLuong) VALUES " +
$"('{Guid.NewGuid()}','{item.Code}','{de_NhapNhienLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.KhoiLuong}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                item.KhoiLuong = 0;
            }
            gc_DanhSachNhienLieu.RefreshDataSource();
            gc_DanhSachNhienLieu.Refresh();
    //        int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_TongHopNhienLieuHangNgay>("UPDATE_MTC_TongHopNhienLieu",
    //Tong, false, true, true);
            MTC_TongHopNhienLieuHangNgay TH = gv_DanhSachNhienLieu.GetFocusedRow() as MTC_TongHopNhienLieuHangNgay;
            if (TH is null)
                return;
            Fcn_UpdateHangNgay(TH.Code);
        }

        private void gv_DanhSachNhienLieu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gv_DanhSachNhienLieu_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.Yellow;
                e.HighPriority = true;
            }
        }

        private void gv_DanhSachNhienLieu_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            MTC_TongHopNhienLieuHangNgay Check = gv_DanhSachNhienLieu.GetFocusedRow() as MTC_TongHopNhienLieuHangNgay;
            if (Check.IsHienThi != true)
                return;
            List<MTC_TongHopNhienLieuHangNgay> Tong = gc_DanhSachNhienLieu.DataSource as List<MTC_TongHopNhienLieuHangNgay>;
            if (Tong.Where(x => x.IsHienThi == true).ToList().Count() >= 4)
            {
                MessageShower.ShowError("Chỉ hiện thị tối đa 3 nhiên liệu, Vui lòng bỏ chọn 1 loại nhiên liệu trước khi chọn nhiên liệu mới");
                gv_DanhSachNhienLieu.SetFocusedValue(false);
                return;
            }
        }

        private void gv_DanhSachNhienLieu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (object.Equals(e.CellValue, (double)0))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }
    }
}