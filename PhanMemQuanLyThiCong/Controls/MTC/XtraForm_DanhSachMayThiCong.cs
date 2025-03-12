using Dapper;
using DevExpress.Internal.WinApi;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Win;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraCharts.Design;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.ChamCong;
using PhanMemQuanLyThiCong.Model.MayThiCong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls.MTC
{
    public partial class XtraForm_DanhSachMayThiCong : DevExpress.XtraEditors.XtraForm
    {
        private string DonViChinh;
        private int focusedRowMay;
        private int focusedRowNhienLieu;
        private int focusedRowDinhMuc;
        private List<MTC_DanhSachChuSoHuu> lstChuSoHuus;
        private List<DanhSachNhanVienModel> lstNguoiVanHanhs;
        private List<MTC_NhienLieu> lstNhienLieus;
        private List<MTC_LoaiNhienLieu> lstLoaiNhienLieus;
        private List<Tbl_ThongTinDuAnViewModel> lstDuAns;
        private List<MTC_TrangThai> lstTrangThais;
        private List<MTC_LoaiMayMoc> lstMayMoc;
        private List<MTC_ChiTietDinhMuc> lstDinhMucMD;
        private List<string> lstCodeMay;
        private DataSet dtSet;
        private DataTable dtMay;
        private DataTable dtNhienLieu;
        private DataTable dtDinhMuc;
        private DataTable dtDuAn;
        private ColumnHeaderExtender columnHeaderExtender;
        Dictionary<string, Image> imageCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        public XtraForm_DanhSachMayThiCong()
        {
            InitializeComponent();
        }

        private void Uc_DanhSachMayThiCong_Load(object sender, EventArgs e)
        {
            LoadData();
            InitDataSoureControl();
            InitEventControl();
        }
        private void InitEventControl()
        {
            this.gc_ListMay.ViewRegistered += gc_ListMay_ViewRegistered;
        }
        private void gc_ListMay_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            columnHeaderExtender = new ColumnHeaderExtender(e.View.ParentView as AdvBandedGridView, e.View as GridView);
            columnHeaderExtender.AddCustomButton();
        }
        private void InitDataSoureControl()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_MTC_NHIENLIEU}";
            lstNhienLieus = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_NhienLieu>(dbString);
            mTCChiTietDinhMucBindingSource.DataSource = lstNhienLieus;
            dbString = $"SELECT * FROM {MyConstant.TBL_MTC_LOAINHIENLIEU}";
            lstLoaiNhienLieus = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_LoaiNhienLieu>(dbString);
            cbb_LoaiNhienLieu.DataSource = lstLoaiNhienLieus;
            dbString = $"SELECT * FROM {MyConstant.TBL_ChamCong_BangNhanVien}";
            lstNguoiVanHanhs = DataProvider.InstanceTHDA.ExecuteQueryModel<DanhSachNhanVienModel>(dbString);
            cbb_ListNguoiVanHanh.DataSource = lstNguoiVanHanhs.ToDictionary(x => x.Code, x => x.TenNhanVien);
            dbString = $"SELECT * FROM {MyConstant.TBL_MTC_DANHSACHCHUSOHUU}";
            lstChuSoHuus = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_DanhSachChuSoHuu>(dbString);
            cbb_ListChuSoHuu.DataSource = lstChuSoHuus.ToDictionary(x => x.Code, x => x.Ten);
            lstDuAns = SharedControls.slke_ThongTinDuAn.Properties.DataSource as List<Tbl_ThongTinDuAnViewModel>;
            cbb_ListDuAn.DataSource = lstDuAns.ToDictionary(x => x.Code, x => x.TenDuAn);
            dbString = $"SELECT * FROM {MyConstant.TBL_MTC_TRANGTHAI}";
            lstTrangThais = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_TrangThai>(dbString);
            cbb_TrangThai.DataSource = lstTrangThais;
            dbString = $"SELECT * FROM {MyConstant.TBL_MTC_LOAIMAY}";
            lstMayMoc = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_LoaiMayMoc>(dbString);
            cbb_LoaiMay.DataSource = lstMayMoc;
            dbString = $"SELECT * FROM Tbl_MTC_DinhMucThamKhao ";
            lstDinhMucMD = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietDinhMuc>(dbString);
            bindingSource_DinhMucMacDinh.DataSource = lstDinhMucMD;
        }
        public void LoadData()
        {
            //var dtSet = DataTableCreateHelper.DanhSachMayInView(out DataTable dtMay, out DataTable dtNhienLieu, out DataTable dtDinhMuc);
            WaitFormHelper.ShowWaitForm("Đang tải danh sách Máy!!!!!", "Vui Lòng chờ!");
            string dbString = $"SELECT\r\n\tROW_NUMBER( ) OVER (ORDER BY SortID ) STT" +
                $",\r\n\tCode,\r\n\tTen,\r\n\tNo,\r\n\tTrangThai,\r\n\tGhiChu,\r\n\t NgayMuaMay,\r\n\t LoaiMay," +
                $"\r\n\tUrlImage,\r\n\tDonVi,\r\n\tHaoPhi,\r\n\tCaMayKm,\r\n\tGROUP_CONCAT( DISTINCT CodeDuAn )" +
                $" AS DuAn,\r\n\tGROUP_CONCAT( DISTINCT CodeChuSoHuu ) AS CodeChuSoHuu,\r\n\tGROUP_CONCAT( DISTINCT CodeNguoiVanHanh )" +
                $" AS CodeNguoiVanHanh,\r\n\tGROUP_CONCAT( DISTINCT ( CASE WHEN LoaiNhienLieu = 1 THEN TenNhienLieu END ) )" +
                $" AS NhienLieuChinh,\r\n\tGROUP_CONCAT( DISTINCT ( CASE WHEN CaMayKm ='Ca' THEN '1 ca 7 tiếng' WHEN CaMayKm ='Km' THEN '100 Km' END ) ) AS DonViHaoPhi ," +
                $"\r\n\tGROUP_CONCAT( DISTINCT ( CASE WHEN LoaiNhienLieu = 2 THEN TenNhienLieu END ) )" +
                $" AS NhienLieuPhu,\r\n\t'Tên:' AS TenCaption,\r\n\t'Dự án:' AS DuAnCaption,\r\n\t'Người vận hành:'" +
                $" AS NguoiVanHanhCaption,\r\n\t'Định mức công việc:' AS DinhMucCongViecCaption,\r\n\t'Ghi chú:'" +
                $" AS GhiChuCaption,\r\n\t'Đơn vị:' AS DonViCaption,\r\n\t'Chủ sở hữu:' AS ChuSoHuuCaption,\r\n\t'Nhiên liệu chính:' " +
                $"AS NhienLieuChinhCaption,\r\n\t'Nhiên liệu phụ:' AS NhienLieuPhuCaption,\r\n\t'Trạng thái:' " +
                $"AS TrangThaiCaption,\r\n\t'Ngày mua máy:' AS NgayMuaMayCaption " +
                $"FROM\r\n\tview_DanhSachMay \r\nGROUP BY\r\n\tCode ORDER BY \"SortId\" ASC";
            dtMay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dtSet = new DataSet();
            dtSet.Tables.Add(dtMay);
            lstCodeMay = dtMay.AsEnumerable().Select(x => x["Code"].ToString()).Distinct().ToList();
            dbString = $"SELECT * FROM view_DanhSachNhienLieuInMay WHERE CodeMay IN ('{string.Join("','", lstCodeMay.ToArray())}');";
            dtNhienLieu = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dtSet.Tables.Add(dtNhienLieu);
            dtSet.Relations.Add("Nhiên liệu", dtMay.Columns["Code"], dtNhienLieu.Columns["CodeMay"]);
            dbString = $"SELECT CT.*,DM.DinhMucCongViec FROM Tbl_MTC_ChiTietDinhMuc CT LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUCTHAMKHAO} DM" +
                $" ON DM.Code=CT.CodeDinhMuc WHERE CodeMay IN ('{string.Join("','", lstCodeMay.ToArray())}')";
            dtDinhMuc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dtSet.Tables.Add(dtDinhMuc);
            dtSet.Relations.Add("Định mức", dtMay.Columns["Code"], dtDinhMuc.Columns["CodeMay"]);

            dbString = $"SELECT DAINMAY.*,DA.TenDuAn as DuAn FROM Tbl_MTC_DuAnInMay DAINMAY LEFT JOIN {MyConstant.TBL_THONGTINDUAN} DA ON DA.Code=DAINMAY.CodeDuAn " +
            $"  WHERE CodeMay IN ('{string.Join("','", lstCodeMay.ToArray())}');";
            dtDuAn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtDuAn.Rows.Count != 0)
            {
                dtSet.Tables.Add(dtDuAn);
                dtSet.Relations.Add("Dự án", dtMay.Columns["Code"], dtDuAn.Columns["CodeMay"]);
            }
            gc_ListMay.DataSource = dtSet.Tables[0];
            WaitFormHelper.CloseWaitForm();
        }
        private void AddUpdateRowDetailViewNhienLieu(GridView view, bool isNew = false)
        {
            MTC_NhienLieuInMay item = new MTC_NhienLieuInMay();
            if (isNew)
                item.Code = Guid.NewGuid().ToString();
            else
                item.Code = view.GetRowCellValue(focusedRowNhienLieu, col_NLCode).ToString();
            object obj;
            item.CodeMay = bgv_ListMay.GetFocusedRowCellValue(bgv_Code).ToString();
            obj = view.GetRowCellValue(focusedRowNhienLieu, col_NameNhienLieu);
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                item.CodeNhienLieu = obj.ToString();
                item.LoaiNhienLieu = int.Parse(view.GetRowCellValue(focusedRowNhienLieu, col_LoaiNhienLieu).ToString());
                obj = view.GetRowCellValue(focusedRowNhienLieu, col_MucTieuThu);
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    item.MucTieuThu = double.Parse(obj.ToString());
                }
                int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_NhienLieuInMay>("INSERT_OR_REPLACE_NHIENLIEUINMAY", new List<MTC_NhienLieuInMay> { item }, false, true, true);
                if (res > -1 && isNew)
                    view.SetRowCellValue(focusedRowNhienLieu, col_NLCode, item.Code);
            }
        }
        private void cbb_NhienLieu_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit editor = (SearchLookUpEdit)sender;
            if (editor.EditValue != null && focusedRowMay > -1 && focusedRowNhienLieu > -1)
            {
                var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 0);
                if (view != null)
                {
                    var lstNhienLieus = mTCChiTietDinhMucBindingSource.DataSource as List<MTC_NhienLieu>;
                    var obj = lstNhienLieus.Find(x => x.Code == editor.EditValue.ToString());
                    if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
                    {
                        view.SetRowCellValue(view.FocusedRowHandle, col_NameNhienLieu, obj.Code);
                        view.SetRowCellValue(view.FocusedRowHandle, col_DonViNhienLieu, obj.DonVi);
                        view.PostEditor();
                        view.UpdateCurrentRow();
                        AddUpdateRowDetailViewNhienLieu(view, true);
                    }
                    else
                    {
                        view.SetRowCellValue(focusedRowNhienLieu, col_NameNhienLieu, obj.Code);
                        view.SetRowCellValue(focusedRowNhienLieu, col_DonViNhienLieu, obj.DonVi);
                    }
                    MTC_NhienLieu dsMayChinh;
                    string dsMayPhu;
                    GetMayChinhPhu(view, out dsMayChinh, out dsMayPhu);
                    if (dsMayChinh != null)
                        bgv_ListMay.SetRowCellValue(bgv_ListMay.FocusedRowHandle, col_NhienLieuChinh, dsMayChinh.Ten);
                    bgv_ListMay.SetRowCellValue(bgv_ListMay.FocusedRowHandle, col_NhienLieuPhu, dsMayPhu);
                    bgv_ListMay.RefreshData();
                }
            }
        }
        private void GetMayChinhPhu(GridView gridView, out MTC_NhienLieu dataMayChinh, out string dataMayPhu)
        {
            var lstNhienLieus = mTCChiTietDinhMucBindingSource.DataSource as List<MTC_NhienLieu>;
            dataMayChinh = null;
            dataMayPhu = string.Empty;
            if (gridView != null)
            {
                //MTC_NhienLieu lstMayChinh = new MTC_NhienLieu();
                List<string> lstMayPhu = new List<string>();
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    object objIdVatLieu = gridView.GetRowCellValue(i, col_NameNhienLieu);
                    if (objIdVatLieu == null) continue;
                    object objValue = gridView.GetRowCellValue(i, col_LoaiNhienLieu);
                    if (objValue != null)
                    {
                        var findNhienLieu = lstNhienLieus.Find(x => x.Code == objIdVatLieu.ToString());
                        if (findNhienLieu == null) continue;
                        int loaiNhienLieu = int.Parse(objValue.ToString());
                        switch (loaiNhienLieu)
                        {
                            case 1:
                                dataMayChinh = new MTC_NhienLieu() { Ten = findNhienLieu.Ten, DonVi = findNhienLieu.DonVi };
                                //dataMayChinh.Ten=findNhienLieu.Ten;
                                //dataMayChinh.DonVi=findNhienLieu.DonVi;
                                break;

                            case 2:
                                lstMayPhu.Add(findNhienLieu.Ten);
                                break;

                            default:
                                break;
                        }
                    }
                }
                //if (lstMayChinh.Any())
                //    dataMayChinh = string.Join(", ", lstMayChinh.ToArray());
                if (lstMayPhu.Any())
                    dataMayPhu = string.Join(", ", lstMayPhu.ToArray());
            }
        }
        private void cbb_NhienLieu_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 0);
            if (view != null && e.NewValue != null)
            {
                var lstNhienLieus = mTCChiTietDinhMucBindingSource.DataSource as List<MTC_NhienLieu>;
                var obj = lstNhienLieus.Find(x => x.Code == e.NewValue.ToString());
                if (obj != null)
                {
                    int index = view.LocateByDisplayText(0, col_NameNhienLieu, obj.Ten);
                    if (index > -1)
                    {
                        XtraMessageBox.Show("Nhiên liệu đã tồn tại trong máy. Vui lòng chọn nhiên liệu khác");
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void cbb_NhienLieu_Popup(object sender, EventArgs e)
        {
            PopupSearchLookUpEditForm f = (sender as DevExpress.Utils.Win.IPopupControl).PopupWindow as PopupSearchLookUpEditForm;
            if (f != null)
            {
                SearchEditLookUpPopup popup = f.ActiveControl as SearchEditLookUpPopup;
                ((LayoutControlItem)popup.lcgAction.Items[1]).Control.Text = "Thêm nhiên liệu";
            }
        }

        private void gv_NhienLieu_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, col_LoaiNhienLieu, 2);
            view.SetRowCellValue(e.RowHandle, col_STTNhienLieu, view.RowCount);
            view.PostEditor();
            view.UpdateCurrentRow();
        }

        private void gv_DinhMuc_MouseDown(object sender, MouseEventArgs e)
        {
            var view = sender as GridView;
            var hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.InRow)
            {
                focusedRowMay = hitInfo.RowHandle;
            }
        }

        private void gv_NhienLieu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedRowHandle < 0)
                focusedRowNhienLieu = view.RowCount - 1;
            else
                focusedRowNhienLieu = view.FocusedRowHandle;
        }
        private int DeleteRowDetailByCode(string tableName, string code)
        {
            DuAnHelper.DeleteDataRows(tableName, new string[] { code });
            return 1;
            //string querry = $"DELETE FROM {tableName} WHERE Code = '{code}'";
            //return DataProvider.InstanceTHDA.Execute(querry, new { Code = code }, true, false);
        }
        private void bt_Delete_Click(object sender, EventArgs e)
        {
            ButtonEdit button = (ButtonEdit)sender;
            if (button != null && focusedRowMay > -1 && focusedRowNhienLieu > -1)
            {
                var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 0);
                if (view != null)
                {
                    if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
                    {
                        view.DeleteRow(GridControl.NewItemRowHandle);
                        view.RefreshData();
                    }
                    else
                    {
                        DialogResult dialogResult = XtraMessageBox.Show($"Bạn có muốn xóa nhiên liệu \" {view.GetRowCellDisplayText(focusedRowNhienLieu, col_NameNhienLieu)} \" khỏi máy \" {bgv_ListMay.GetRowCellValue(focusedRowMay, bgv_TenMay)} \"", "Quản lý thi công - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string code = view.GetRowCellValue(focusedRowNhienLieu, col_NLCode).ToString();
                            int result = DeleteRowDetailByCode(MyConstant.TBL_MTC_NHIENLIEUINMAY, code);
                            if (result > 0)
                            {
                                view.DeleteRow(view.FocusedRowHandle);
                                view.RefreshData();
                                MTC_NhienLieu dsMayChinh;
                                string dsMayPhu;
                                GetMayChinhPhu(view, out dsMayChinh, out dsMayPhu);
                                if (dsMayChinh != null)
                                    bgv_ListMay.SetRowCellValue(bgv_ListMay.FocusedRowHandle, col_NhienLieuChinh, dsMayChinh.Ten);
                                bgv_ListMay.SetRowCellValue(bgv_ListMay.FocusedRowHandle, col_NhienLieuPhu, dsMayPhu);
                                bgv_ListMay.RefreshData();
                            }
                            else
                            {
                                XtraMessageBox.Show("Lỗi không xóa được nhiên liệu", "Quản lý thi công - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                }
            }
        }
        private int DeleteDataByCodeMay(string tableName, string code)
        {
            DuAnHelper.DeleteDataRows(tableName, new string[] { code });
            return 1;
            //string querry = $"DELETE FROM {tableName} WHERE CodeMay = '{code}'";
            //return DataProvider.InstanceTHDA.Execute(querry, new { Code = code }, true, false);
        }

        private void gv_NhienLieu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            object obj = view.GetFocusedRowCellValue(col_NLCode);
            bool isNew = (obj == null || string.IsNullOrEmpty(obj.ToString()));
            AddUpdateRowDetailViewNhienLieu(view, isNew);
        }

        private void bt_DinhMucDelete_Click(object sender, EventArgs e)
        {
            ButtonEdit button = (ButtonEdit)sender;
            if (button != null && focusedRowMay > -1 && focusedRowDinhMuc > -1)
            {
                var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 1);
                if (view != null)
                {
                    if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
                    {
                        view.DeleteRow(GridControl.NewItemRowHandle);
                    }
                    else
                    {
                        DialogResult dialogResult = XtraMessageBox.Show($"Bạn có muốn xóa định mức \" {view.GetRowCellDisplayText(focusedRowDinhMuc, col_DinhMucCongViec)} \" khỏi máy \" {bgv_ListMay.GetRowCellValue(focusedRowMay, bgv_TenMay)} \"", "Quản lý thi công - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string code = view.GetRowCellValue(focusedRowDinhMuc, col_DinhMucCode).ToString();
                            int result = DeleteRowDetailByCode(MyConstant.TBL_MTC_CHITIETDINHMUC, code);
                            if (result > 0)
                            {
                                view.DeleteRow(view.FocusedRowHandle);
                            }
                            else
                            {
                                XtraMessageBox.Show("Lỗi không xóa được định mức", "Quản lý thi công - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    view.RefreshData();
                }
            }
        }

        private void gv_DinhMuc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedRowHandle < 0)
                focusedRowDinhMuc = view.RowCount - 1;
            else
                focusedRowDinhMuc = view.FocusedRowHandle;
        }

        private void gv_DinhMuc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName != col_DinhMucCode.FieldName)
            {
                object obj = view.GetFocusedRowCellValue(col_DinhMucCode);
                bool isNew = (obj == null || string.IsNullOrEmpty(obj.ToString()));
                AddUpdateRowDetailViewDinhMuc(view, isNew);
            }
        }

        private void AddUpdateRowDetailViewDinhMuc(GridView view, bool isNew = false)
        {
            MTC_ChiTietDinhMuc item = new MTC_ChiTietDinhMuc();
            if (isNew)
                item.Code = Guid.NewGuid().ToString();
            else
                item.Code = view.GetFocusedRowCellValue(col_DinhMucCode).ToString();
            object obj;
            item.CodeMay = bgv_ListMay.GetFocusedRowCellValue(bgv_Code).ToString();
            obj = view.GetRowCellValue(focusedRowDinhMuc, col_DinhMucCongViec);
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                item.CodeDinhMuc = obj.ToString();
                obj = view.GetRowCellValue(focusedRowDinhMuc, col_DinhMucDonVi);
                if (obj != null) item.DonVi = obj.ToString();
                obj = view.GetRowCellValue(focusedRowDinhMuc, col_GhiChuDinhMuc);
                if (obj != null) item.GhiChu = obj.ToString();
                obj = view.GetRowCellValue(focusedRowDinhMuc, col_DinhMucTieuThu);
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    item.MucTieuThu = double.Parse(obj.ToString());
                }
                int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_ChiTietDinhMuc>("INSERT_OR_REPLACE_DINHMUCINMAY", new List<MTC_ChiTietDinhMuc> { item }, false, true, true);
                if (res > -1 && isNew)
                {
                    view.SetRowCellValue(focusedRowDinhMuc, col_DinhMucCode, item.Code);
                }
                view.RefreshData();
            }
        }

        private void repositoryItemPictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            object obj = bgv_ListMay.GetFocusedRowCellValue(bgv_Code);
            if (string.IsNullOrEmpty(obj.ToString()) || int.TryParse(obj.ToString(), out int result))
            {
                XtraMessageBox.Show("Vui lòng nhập tên máy để lưu trước khi chọn ảnh đại diện cho máy");
                return;
            }
            FormLuaChon frm = new FormLuaChon(obj.ToString(), FileManageTypeEnum.MayThiCong);
            frm.ShowDialog();
            LoadData();
        }

        private void bgv_ListMay_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            AdvBandedGridView view = sender as AdvBandedGridView;
            object obj = view.GetFocusedRowCellValue(bgv_Code);
            string code = obj == null ? string.Empty : obj.ToString();
            bool IsNew = int.TryParse(code, out int result);
            switch (e.Column.FieldName)
            {
                case "Ten":
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                        AddUpdateRowMay(view, IsNew);
                    LoadData();
                    break;
                case "DuAn":
                    if (!string.IsNullOrEmpty(code))
                    {
                        List<MTC_DuAnInMay> lstDuAns = new List<MTC_DuAnInMay>();
                        obj = view.GetFocusedRowCellValue(bgv_DuAn);
                        if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                        {
                            foreach (var it in obj.ToString().Split(','))
                            {
                                lstDuAns.Add(new MTC_DuAnInMay()
                                {
                                    Code = Guid.NewGuid().ToString(),
                                    CodeMay = code,
                                    CodeDuAn = it.Trim()
                                });
                            }
                        }
                        //if (!lstDuAns.Any())
                        //{
                        //    DeleteDataByCodeMay(MyConstant.TBL_MTC_DUANINMAY, code);
                        //    return;
                        //}
                        if (dtDuAn.Rows.Count == 0)
                        {
                            DataProvider.InstanceTHDA.AddOrUpdate<MTC_DuAnInMay>("INSERT_OR_REPLACE_DUANINMAY", lstDuAns, false, true, true);
                        }
                        else
                        {
                            DataRow[] dt_DuAnMay = dtDuAn.AsEnumerable().Where(x => x["CodeMay"].ToString() == code).ToArray();
                            if (dt_DuAnMay.Any())
                            {
                                string[] CodeDA = dt_DuAnMay.Select(x => x["CodeDuAn"].ToString()).ToArray();
                                List<MTC_DuAnInMay> lstnotcontainDA = lstDuAns.Where(x => !CodeDA.Contains(x.CodeDuAn)).ToList();
                                if (lstnotcontainDA.Any())
                                    DataProvider.InstanceTHDA.AddOrUpdate<MTC_DuAnInMay>("INSERT_OR_REPLACE_DUANINMAY", lstnotcontainDA, false, true, true);
                                string[] CodeDANew = lstDuAns.Select(x => x.CodeDuAn).ToArray();
                                string[] CodeDADelete = dt_DuAnMay.Where(x => !CodeDANew.Contains(x["CodeDuAn"].ToString())).Select(x => x["CodeDuAn"].ToString()).ToArray();
                                string db_string = $"DELETE FROM {MyConstant.TBL_MTC_DUANINMAY} WHERE \"CodeMay\"='{code}' AND CodeDuAn IN ('{string.Join("','", CodeDADelete)}')";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
                            }
                            else
                            {
                                DataProvider.InstanceTHDA.AddOrUpdate<MTC_DuAnInMay>("INSERT_OR_REPLACE_DUANINMAY", lstDuAns, false, true, true);
                            }
                        }
                        if (!lstCodeMay.Contains(code))
                            lstCodeMay.Add(code);
                        LoadData();
                        //Fcn_UpdateDuAnInMay();
                    }
                    else
                    {
                        bgv_ListMay.CancelUpdateCurrentRow();
                    }
                    break;
                default:
                    AddUpdateRowMay(view, IsNew);
                    break;
            }
        }

        private void btn_AddMay_Click(object sender, EventArgs e)
        {
            bgv_ListMay.CellValueChanged -= bgv_ListMay_CellValueChanged;
            bgv_ListMay.AddNewRow();
            bgv_ListMay.CellValueChanged += bgv_ListMay_CellValueChanged;
        }

        private void bgv_ListMay_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            AdvBandedGridView view = sender as AdvBandedGridView;
            view.SetRowCellValue(e.RowHandle, bgv_Code, bgv_ListMay.RowCount);
            view.SetRowCellValue(e.RowHandle, bgv_STT, bgv_ListMay.RowCount);
            view.SetRowCellValue(e.RowHandle, col_MaMay, bgv_ListMay.RowCount);
            view.SetRowCellValue(e.RowHandle, bgc_TenMayCaption, "Tên máy:");
            view.SetRowCellValue(e.RowHandle, bgc_DuAnCaption, "Dự án:");
            view.SetRowCellValue(e.RowHandle, bgc_NguoiVanHanhCaption, "Người vận hành:");
            view.SetRowCellValue(e.RowHandle, bgc_GhiChuCaption, "Ghi Chú:");
            view.SetRowCellValue(e.RowHandle, bgc_DonViCaption, "Đơn Vị:");
            view.SetRowCellValue(e.RowHandle, bgc_ChuSoHuuCaption, "Chủ sở hữu:");
            view.SetRowCellValue(e.RowHandle, bgc_TrangThaiCaption, "Trạng thái:");
            view.SetRowCellValue(e.RowHandle, bgc_NhienLieuChinhCaption, "Nhiên liệu chính:");
            view.SetRowCellValue(e.RowHandle, bgc_NhienLieuPhuCaption, "Nhiên liệu phụ:");
            view.SetRowCellValue(e.RowHandle, bgc_NgayMuaMayCaption, "Ngày mua máy:");
            view.SetRowCellValue(e.RowHandle, bgc_GiaMuaMayCaption, "Giá mua máy:");
            view.SetRowCellValue(e.RowHandle, bgc_GiaCaMayCaption, "Giá ca máy:");
            view.SetRowCellValue(e.RowHandle, bgv_IsMayCaKm, "Ca");
            view.PostEditor();
            view.UpdateCurrentRow();
        }
        private void AddUpdateRowMay(AdvBandedGridView view, bool isNew = false)
        {
            List<MTC_ChuSoHuuInMay> lstChuSoHuus = new List<MTC_ChuSoHuuInMay>();
            List<MTC_NguoiVanHanhInMay> lstNguoiVanHanhs = new List<MTC_NguoiVanHanhInMay>();
            MTC_DanhSachMay item = new MTC_DanhSachMay();
            if (isNew)
            {
                item.Code = Guid.NewGuid().ToString();
            }
            else
                item.Code = view.GetFocusedRowCellValue(bgv_Code).ToString();
            object obj;
            item.Ten = view.GetFocusedRowCellValue(bgv_TenMay).ToString();
            obj = view.GetFocusedRowCellValue(bgv_TrangThai);
            item.TrangThai = obj == null ? string.Empty : obj.ToString();
            obj = view.GetFocusedRowCellValue(col_MaMay);
            item.No = obj == null ? string.Empty : obj.ToString();
            obj = view.GetFocusedRowCellValue(bgv_GhiChu);
            item.GhiChu = obj == null ? string.Empty : obj.ToString();
            obj = view.GetFocusedRowCellValue(bgv_LoaiMay);
            item.LoaiMay = obj == null ? string.Empty : obj.ToString();
            obj = view.GetFocusedRowCellValue(bgv_DonVi);
            item.DonVi = obj == null ? string.Empty : obj.ToString();
            obj = view.GetFocusedRowCellValue(bgv_IsMayCaKm);
            item.CaMayKm = obj == null ? string.Empty : obj.ToString();
            double HaoPhi;
            obj = view.GetFocusedRowCellValue(bgv_MucHaoPhiMacDinh);
            if (obj != null && double.TryParse(obj.ToString(), out HaoPhi))
                item.HaoPhi = HaoPhi;
            double giacamay;
            obj = view.GetFocusedRowCellValue(bgv_GiaCaMay);
            //if (obj != null && double.TryParse(obj.ToString(), out giacamay))
            //    item.GiaCaMay = giacamay;
            obj = view.GetFocusedRowCellValue(bgv_GiaMuaMay);
            double giamua;
            //if (obj != null && double.TryParse(obj.ToString(), out giamua))
            //    item.GiaMuaMay = giamua;
            obj = view.GetFocusedRowCellValue(bgv_NgayMuaMay);
            DateTime ngayMua;
            if (obj != null && DateTime.TryParse(obj.ToString(), out ngayMua))
                item.NgayMuaMay = ngayMua.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

            obj = view.GetFocusedRowCellValue(bgv_STT);
            item.SortId = int.Parse(obj.ToString());
            obj = view.GetFocusedRowCellValue(bgv_ChuSoHuu);
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                foreach (var it in obj.ToString().Split(','))
                {
                    lstChuSoHuus.Add(new MTC_ChuSoHuuInMay()
                    {
                        Code = Guid.NewGuid().ToString(),
                        CodeMay = item.Code,
                        CodeChuSoHuu = it.Trim()
                    });
                }
            }
            obj = view.GetFocusedRowCellValue(bgv_VanHanh);
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                foreach (var it in obj.ToString().Split(','))
                {
                    lstNguoiVanHanhs.Add(new MTC_NguoiVanHanhInMay()
                    {
                        Code = Guid.NewGuid().ToString(),
                        CodeMay = item.Code,
                        CodeNguoiVanHanh = it.Trim()
                    });
                }
            }
            bool isSuccess = false;
            if (isNew)
            {
                if (string.IsNullOrEmpty(item.Ten)) return;
                int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_DanhSachMay>("INSERT_DANHSACHMAY", new List<MTC_DanhSachMay> { item }, false, true, true);
                if (res > 0)
                {
                    view.SetFocusedRowCellValue(bgv_Code, item.Code);
                    isSuccess = true;
                }
            }
            else
            {
                //DeleteDataByCodeMay(MyConstant.TBL_MTC_DUANINMAY, item.Code);
                DeleteDataByCodeMay(MyConstant.TBL_MTC_NGUOIVANHANHINMAY, item.Code);
                DeleteDataByCodeMay(MyConstant.TBL_MTC_CHUSOHUUINMAY, item.Code);
                int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_DanhSachMay>("UPDATE_DANHSACHMAY", new List<MTC_DanhSachMay> { item }, false, true, true);
                if (res > 0)
                {
                    isSuccess = true;
                }
            }
            if (isSuccess)
            {
                //if (lstDuAns.Any())
                //{
                //    DataProvider.InstanceTHDA.AddOrUpdate<MTC_DuAnInMay>("INSERT_OR_REPLACE_DUANINMAY", lstDuAns, false, true, true);
                //    Fcn_UpdateDuAnInMay();
                //}
                if (lstChuSoHuus.Any())
                {
                    DataProvider.InstanceTHDA.AddOrUpdate<MTC_ChuSoHuuInMay>("INSERT_OR_REPLACE_CHUSOHUUINMAY", lstChuSoHuus, false, true, true);
                }
                if (lstNguoiVanHanhs.Any())
                {
                    DataProvider.InstanceTHDA.AddOrUpdate<MTC_NguoiVanHanhInMay>("INSERT_OR_REPLACE_NGUOIVANHANHINMAY", lstNguoiVanHanhs, false, true, true);
                }
            }
        }

        private void cbb_NhienLieu_AddNewValue(object sender, DevExpress.XtraEditors.Controls.AddNewValueEventArgs e)
        {
            XtraForm2 frm = new XtraForm2("Tên nhiên liệu");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(frm.textEdit1.Text.Trim()))
                {
                    XtraMessageBox.Show("Tên không được để trống");
                    e.Cancel = true;
                }
                else
                {
                    if (CheckExitName(MyConstant.TBL_MTC_NHIENLIEU, frm.textEdit1.Text.Trim(), "Ten"))
                    {
                        MessageShower.ShowWarning("Tên đã tồn tại trong database. Vui lòng đặt tên khác");
                        e.Cancel = true;
                    }
                }
                if (string.IsNullOrEmpty(frm.textEdit2.Text.Trim()))
                {
                    MessageShower.ShowWarning("Đơn vị không được để trống");
                    e.Cancel = true;
                }
                var newNhienLieu = new MTC_NhienLieu();
                newNhienLieu.Code = Guid.NewGuid().ToString();
                newNhienLieu.Ten = frm.textEdit1.Text.Trim();
                newNhienLieu.DonVi = frm.textEdit2.Text.Trim();
                int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_NhienLieu>("INSERT_NHIENLIEU", new List<MTC_NhienLieu> { newNhienLieu }, false, true, true);
                if (res > 0)
                {
                    lstNhienLieus.Add(newNhienLieu);
                    var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 0);
                    if (view != null)
                    {
                        if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
                        {
                            view.SetRowCellValue(view.FocusedRowHandle, col_NameNhienLieu, newNhienLieu.Code);
                            view.SetRowCellValue(view.FocusedRowHandle, col_DonViNhienLieu, newNhienLieu.DonVi);
                            view.PostEditor();
                            view.UpdateCurrentRow();
                        }
                        else
                        {
                            view.SetRowCellValue(focusedRowNhienLieu, col_NameNhienLieu, newNhienLieu.Code);
                            view.SetRowCellValue(focusedRowNhienLieu, col_DonViNhienLieu, newNhienLieu.DonVi);
                        }
                        view.RefreshData();
                        MTC_NhienLieu dsMayChinh;
                        string dsMayPhu;
                        GetMayChinhPhu(view, out dsMayChinh, out dsMayPhu);
                        if (dsMayChinh != null)
                            bgv_ListMay.SetRowCellValue(bgv_ListMay.FocusedRowHandle, col_NhienLieuChinh, dsMayChinh.Ten);
                        bgv_ListMay.SetRowCellValue(bgv_ListMay.FocusedRowHandle, col_NhienLieuPhu, dsMayPhu);
                        bgv_ListMay.RefreshData();
                    }
                }
            }
            else
                e.Cancel = true;
        }

        private bool CheckExitName(string tableName, string name, string FieldName)
        {
            string querry = $"SELECT * FROM {tableName} WHERE UPPER({FieldName})=UPPER('{name}')";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(querry);
            return dt.Rows.Count > 0;
        }

        private void btn_QLChuSoHuu_Click(object sender, EventArgs e)
        {
            XtraForm3 frm = new XtraForm3(UcType.MTC_CHUSOHUU, "Quản lý danh sách chủ sở hữu");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string dbString = $"SELECT * FROM {MyConstant.TBL_MTC_DANHSACHCHUSOHUU}";
                lstChuSoHuus = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_DanhSachChuSoHuu>(dbString);
                cbb_ListChuSoHuu.DataSource = lstChuSoHuus.ToDictionary(x => x.Code, x => x.Ten);
            }
        }

        private void bgv_ListMay_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Picture" && e.IsGetData)
            {
                GridView view = sender as GridView;
                string fileCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "UrlImage") as string ?? string.Empty;
                object objCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), bgv_Code);
                if (objCode != null && !string.IsNullOrEmpty(objCode.ToString()))
                {

                    if (!string.IsNullOrEmpty(fileCode))
                    {
                        string filePath = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, MyConstant.TBL_MTC_FILEDINHKEM, objCode.ToString()), fileCode);
                        e.Value = FileHelper.fcn_ImageStreamDoc(filePath) ?? Properties.Resources._2_MTC;
                    }
                    else
                        e.Value = Properties.Resources._2_MTC;
                }

            }
        }

        private void btn_QLTrangThai_Click(object sender, EventArgs e)
        {
            XtraForm3 frm = new XtraForm3(UcType.MTC_TRANGTHAI, "Quản lý danh sách trạng thái");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string dbString = $"SELECT * FROM {MyConstant.TBL_MTC_TRANGTHAI}";
                lstTrangThais = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_TrangThai>(dbString);
                cbb_TrangThai.DataSource = lstTrangThais;
            }
        }

        private void cbb_DinhMuc_AddNewValue(object sender, DevExpress.XtraEditors.Controls.AddNewValueEventArgs e)
        {
            XtraForm2 frm = new XtraForm2("Tên định mức", DonViChinh);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(frm.textEdit1.Text.Trim()))
                {
                    XtraMessageBox.Show("Tên không được để trống");
                    e.Cancel = true;
                }
                else
                {
                    if (CheckExitName(MyConstant.TBL_MTC_CHITIETDINHMUCTHAMKHAO, frm.textEdit1.Text.Trim(), "DinhMucCongViec"))
                    {
                        XtraMessageBox.Show("Tên đã tồn tại trong database. Vui lòng đặt tên khác");
                        e.Cancel = true;
                    }
                }
                if (string.IsNullOrEmpty(frm.textEdit2.Text.Trim()))
                {
                    XtraMessageBox.Show("Đơn vị không được để trống");
                    e.Cancel = true;
                }
                var NewDinhMuc = new MTC_ChiTietDinhMuc();
                NewDinhMuc.Code = Guid.NewGuid().ToString();
                NewDinhMuc.DinhMucCongViec = frm.textEdit1.Text.Trim();
                NewDinhMuc.DonVi = frm.textEdit2.Text.Trim();
                int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_ChiTietDinhMuc>("INSERT_DINHMUCTHAMKHAO",
                    new List<MTC_ChiTietDinhMuc> { NewDinhMuc }, false, true, true);
                if (res > 0)
                {
                    lstDinhMucMD.Add(NewDinhMuc);
                    var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 1);
                    if (view != null)
                    {
                        if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
                        {
                            view.SetRowCellValue(view.FocusedRowHandle, col_DinhMucCongViec, NewDinhMuc.Code);
                            view.SetRowCellValue(view.FocusedRowHandle, col_DinhMucDonVi, NewDinhMuc.DonVi);
                            view.PostEditor();
                            view.UpdateCurrentRow();
                        }
                        else
                        {
                            view.SetRowCellValue(focusedRowDinhMuc, col_DinhMucCongViec, NewDinhMuc.Code);
                            view.SetRowCellValue(focusedRowDinhMuc, col_DinhMucDonVi, NewDinhMuc.DonVi);
                        }
                        view.RefreshData();
                    }
                }
            }
            else
                e.Cancel = true;
        }

        private void cbb_DinhMuc_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit editor = (SearchLookUpEdit)sender;
            if (editor.EditValue != null && focusedRowMay > -1 && focusedRowDinhMuc > -1)
            {
                var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 1);
                if (view != null)
                {
                    var lstDinhMuc = bindingSource_DinhMucMacDinh.DataSource as List<MTC_ChiTietDinhMuc>;
                    var obj = lstDinhMuc.Find(x => x.Code == editor.EditValue.ToString());
                    if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
                    {
                        view.SetRowCellValue(view.FocusedRowHandle, col_DinhMucCongViec, obj.Code);
                        view.SetRowCellValue(view.FocusedRowHandle, col_DinhMucDonVi, obj.DonVi);
                        view.PostEditor();
                        view.UpdateCurrentRow();
                        AddUpdateRowDetailViewDinhMuc(view, true);
                    }
                    else
                    {
                        view.SetRowCellValue(focusedRowDinhMuc, col_DinhMucCongViec, obj.Code);
                        view.SetRowCellValue(focusedRowDinhMuc, col_DinhMucDonVi, DonViChinh);
                    }
                }
            }
        }

        private void cbb_DinhMuc_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 1);
            if (view != null && e.NewValue != null)
            {
                var DinhMuc = bindingSource_DinhMucMacDinh.DataSource as List<MTC_ChiTietDinhMuc>;
                var obj = DinhMuc.Find(x => x.Code == e.NewValue.ToString());
                if (obj != null)
                {
                    int index = view.LocateByDisplayText(0, col_DinhMucCongViec, obj.DinhMucCongViec);
                    if (index > -1)
                    {
                        XtraMessageBox.Show("Định mức đã tồn tại trong máy. Vui lòng chọn định mức khác");
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void cbb_DinhMuc_Popup(object sender, EventArgs e)
        {
            PopupSearchLookUpEditForm f = (sender as DevExpress.Utils.Win.IPopupControl).PopupWindow as PopupSearchLookUpEditForm;
            if (f != null)
            {
                SearchEditLookUpPopup popup = f.ActiveControl as SearchEditLookUpPopup;
                ((LayoutControlItem)popup.lcgAction.Items[1]).Control.Text = "Thêm định mức";
            }
        }

        private void gv_DinhMuc_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 0);
            string dsMayPhu;
            GetMayChinhPhu(view, out MTC_NhienLieu dsMayChinh, out dsMayPhu);
            DonViChinh = dsMayChinh.DonVi;
            view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, col_DinhMucDonVi, dsMayChinh.DonVi);
            ////view.SetRowCellValue(e.RowHandle, col_STTNhienLieu, view.RowCount);
            view.PostEditor();
            view.UpdateCurrentRow();
        }

        private void bgv_ListMay_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "NgayMuaMay")
                e.DisplayText = e.Value.ToString() == "" ? "" : DateTime.Parse(e.Value.ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
        }

        private void gv_DuAn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string FieldName = e.Column.FieldName;
            var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 2);
            string Code = view.GetRowCellValue(e.RowHandle, "Code").ToString();
            string db_string = $"UPDATE  {MyConstant.TBL_MTC_DUANINMAY} SET" +
            $" '{FieldName}'='{e.Value}' WHERE \"Code\"='{Code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
        }

        private void bgv_ListMay_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (e.MenuType == GridMenuType.Row)
            {
                DXMenuItem menuItem = new DXMenuItem("Tham khảo hao phí", this.fcn_Handle_Popup_QLVT_LuuND);
                menuItem.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem);     
                DXMenuItem menuItemXoa = new DXMenuItem("Xóa máy thi công", this.fcn_Handle_Popup_XoaMay);
                menuItem.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem);

            }
        }
        private void fcn_Handle_Popup_QLVT_LuuND(object sender, EventArgs e)
        {

        }
               
        private void fcn_Handle_Popup_XoaMay(object sender, EventArgs e)
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa máy thi công này không????");
            if (rs == DialogResult.No)
                return;
            string Code=bgv_ListMay.GetFocusedRowCellValue(bgv_Code).ToString();
            DeleteDataByCodeMay(MyConstant.TBL_MTC_DANHSACHMAY, Code);
            bgv_ListMay.DeleteSelectedRows();
        }

        private void sb_DanhSachThietBi_Click(object sender, EventArgs e)
        {
            XtraForm3 frm = new XtraForm3(UcType.MTC_LOAIMAY, "Quản lý danh sách loại thiết bị");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string dbString = $"SELECT * FROM {MyConstant.TBL_MTC_LOAIMAY}";
                lstMayMoc = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_LoaiMayMoc>(dbString);
                cbb_LoaiMay.DataSource = lstMayMoc;
            }
        }

        private async void sb_downLoad_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu Máy thi công từ Server");
            var result = await CusHttpClient.InstanceCustomer.MGetAsync<SQLiteAllDataViewModel1>(RouteAPI.TongDuAn_GetMTCs);

            if (!result.MESSAGE_TYPECODE)
            {
                AlertShower.ShowInfo($"Không thể tải dữ liệu từ server: {result.MESSAGE_CONTENT}");
                WaitFormHelper.CloseWaitForm();
                return;
            }
            else
            {
                var allSqlite = result.Dto;

                await SharedProjectHelper.PushAllToSqlite(allSqlite);
            }

            WaitFormHelper.CloseWaitForm();

            LoadData();
        }

        private void bgv_ListMay_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa máy thi công này không????");
                if (rs == DialogResult.No)
                    return;
                object Code = bgv_ListMay.GetFocusedRowCellValue(bgv_Code);
                if (Code is null)
                    return;
                DeleteDataByCodeMay(MyConstant.TBL_MTC_DANHSACHMAY, Code.ToString());
                bgv_ListMay.DeleteSelectedRows();
            }
        }

        private void repositoryItemButtonEditXoaNhienLieu_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemGridLookUpEdit_NhienLieu_Popup(object sender, EventArgs e)
        {
            PopupGridLookUpEditForm f = (sender as DevExpress.Utils.Win.IPopupControl).PopupWindow as PopupGridLookUpEditForm;
        }

        private void repositoryItemGridLookUpEdit_NhienLieu_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {

        }

        private void repositoryItemGridLookUpEdit_NhienLieu_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue != null && focusedRowMay > -1 && focusedRowNhienLieu > -1)
            {
                var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 0);
                if (view != null)
                {
                    var lstNhienLieus = mTCChiTietDinhMucBindingSource.DataSource as List<MTC_NhienLieu>;
                    var obj = lstNhienLieus.Find(x => x.Code == editor.EditValue.ToString());
                    if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
                    {
                        view.SetRowCellValue(view.FocusedRowHandle, col_NameNhienLieu, obj.Code);
                        view.SetRowCellValue(view.FocusedRowHandle, col_DonViNhienLieu, obj.DonVi);
                        view.PostEditor();
                        view.UpdateCurrentRow();
                        AddUpdateRowDetailViewNhienLieu(view, true);
                    }
                    else
                    {
                        view.SetRowCellValue(focusedRowNhienLieu, col_NameNhienLieu, obj.Code);
                        view.SetRowCellValue(focusedRowNhienLieu, col_DonViNhienLieu, obj.DonVi);
                    }
                    MTC_NhienLieu dsMayChinh;
                    string dsMayPhu;
                    GetMayChinhPhu(view, out dsMayChinh, out dsMayPhu);
                    if (dsMayChinh != null)
                        bgv_ListMay.SetRowCellValue(bgv_ListMay.FocusedRowHandle, col_NhienLieuChinh, dsMayChinh.Ten);
                    bgv_ListMay.SetRowCellValue(bgv_ListMay.FocusedRowHandle, col_NhienLieuPhu, dsMayPhu);
                    bgv_ListMay.RefreshData();
                }
            }
        }

        private void repositoryItemGridLookUpEdit_NhienLieu_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var view = (GridView)bgv_ListMay.GetDetailView(focusedRowMay, 0);
            if (view != null && e.NewValue != null)
            {
                var lstNhienLieus = mTCChiTietDinhMucBindingSource.DataSource as List<MTC_NhienLieu>;
                var obj = lstNhienLieus.Find(x => x.Code == e.NewValue.ToString());
                if (obj != null)
                {
                    int index = view.LocateByDisplayText(0, col_NameNhienLieu, obj.Ten);
                    if (index > -1)
                    {
                        XtraMessageBox.Show("Nhiên liệu đã tồn tại trong máy. Vui lòng chọn nhiên liệu khác");
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
    }
}
