using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Controls;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PhanMemQuanLyThiCong.Common.Helper.DuAnHelper;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model.HopDong;
using DevExpress.Utils.Extensions;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Model;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using System.IO;
using System.Diagnostics;
using PhanMemQuanLyThiCong.Common.Constant.Enum;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_ThuChiTamUng_DeXuat : DevExpress.XtraEditors.XtraUserControl
    {
        int offset = 5;
        int markWidth = 16;
        Color QLVC = Color.Green;
        Color ClTDKH = Color.Orange;
        Color TC = Color.Blue;
        Color HD = Color.Yellow;
        EditFormDeXuat editform=new EditFormDeXuat();
        static bool CheckNewRow = false;
        public List<KhoanChi> NewKC = new List<KhoanChi>();
        public List<KhoanThu> NewKT = new List<KhoanThu>();
        public bool _IsQuyTrinh { get; set; } = false;
        public  Icon Icon { get; set; }
        public Form_ThuChiTamUng_DeXuat()
        {
            InitializeComponent();
        }
        public void Fcn_UpdateTCTU(Icon _Icon)
        {
            Icon = _Icon;
            string dbString = $"SELECT (CASE WHEN COALESCE(KC.TrangThai,KT.TrangThai) IS NULL THEN {(int)TrangThaiDeXuatEnum.ChuaGuiDuyet} WHEN COALESCE(KC.TrangThai,KT.TrangThai)=5 THEN 5 ELSE {(int)TrangThaiDeXuatEnum.DaDuyet} END) AS TrangThai" +
                $",DX.* FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} DX" +
                $" LEFT JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} KC ON KC.CodeDeXuat=DX.Code " +
                $"LEFT JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} KT ON KT.CodeDeXuat=DX.Code  " +
                $" WHERE DX.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count == 0)
            {
                gc_ThuChiTamUng_DeXuat.DataSource = null;
                return;
            }
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, false);
            rILUE_CongTrinhHM.DataSource = Infor;
            rILUE_CongTrinhHM.DropDownRows = Infor.Count;

            dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_LOAIKINHPHI}";
            List<Infor> LoaiKP = new List<Infor>();

            foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                LoaiKP.Add(new Infor
                {
                    Code = item["Code"].ToString(),
                    Ten = item["Ten"].ToString()
                });
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}";
            List<Infor> TenNV = new List<Infor>();
            foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                TenNV.Add(new Infor
                {
                    Code = item["Code"].ToString(),
                    Ten = item["TenNhanVien"].ToString(),
                    Decription = item["MaNhanVien"].ToString()
                });
            LoaiKP.Add(new Infor
            {
                Code = "Add",
                Ten = "Thêm"
            });
            dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_hd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<Infor_HopDong> HD = DuAnHelper.ConvertToList<Infor_HopDong>(dt_hd);
            rILUE_TenHopDong.DataSource = HD;
            rILUE_LoaiKinhPhi.DataSource = LoaiKP;
            //lUE_LoaiKinhPhi.Properties.DataSource = LoaiKP;
            rILUE_TenNguoiLap.DataSource = TenNV;
            //lUE_TenNguoiLap.Properties.DataSource = TenNV;
            rILUE_ToChucCaNhan.DataSource = DuAnHelper.GetCaNhanToChuc();
            //lUE_ToChucCaNhan.Properties.DataSource = DuAnHelper.GetCaNhanToChuc();
            //gc_ThuChiTamUng_DeXuat.DataSource = DexuatVL;
            BindingList<DeXuat> Dexuatchinh = new BindingList<DeXuat>();
            Dexuatchinh = new BindingList<DeXuat>(DuAnHelper.ConvertToList<DeXuat>(dt));
            //foreach (DeXuat item in Dexuatchinh)
            //{
            //    if (item.CodeNhaCungCap != null)
            //        item.ToChucCaNhanNhanChiPhiTamUng = item.CodeNhaCungCap;
            //    else if (item.CodeNhaThau != null)
            //        item.ToChucCaNhanNhanChiPhiTamUng = item.CodeNhaThau;
            //    else if (item.CodeToDoi != null)
            //        item.ToChucCaNhanNhanChiPhiTamUng = item.CodeToDoi;
            //    else
            //        item.ToChucCaNhanNhanChiPhiTamUng = item.CodeNhaThauPhu;
            //}
            gc_ThuChiTamUng_DeXuat.DataSource = Dexuatchinh;
            //foreach (DataRow row in dt.Rows)
            //{
            //    DeXuat DX = new DeXuat();


            //}
        }
        public void Fcn_AddData(BindingList<DeXuat> DexuatVL)
        {
            BindingList<DeXuat> DX = gc_ThuChiTamUng_DeXuat.DataSource as BindingList<DeXuat>;
            if (DX == null)
            {
                List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, false);
                rILUE_CongTrinhHM.DataSource = Infor;
                rILUE_CongTrinhHM.DropDownRows = Infor.Count;
                string dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                DataTable dt_hd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                List<Infor_HopDong> HD = DuAnHelper.ConvertToList<Infor_HopDong>(dt_hd);
                rILUE_TenHopDong.DataSource = HD;
                dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_LOAIKINHPHI}";
                List<Infor> LoaiKP = new List<Infor>();
                foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                    LoaiKP.Add(new Infor
                    {
                        Code = item["Code"].ToString(),
                        Ten = item["Ten"].ToString()
                    });
                dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}";
                List<Infor> TenNV = new List<Infor>();
                foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                    TenNV.Add(new Infor
                    {
                        Code = item["Code"].ToString(),
                        Ten = item["TenNhanVien"].ToString(),
                        Decription = item["MaNhanVien"].ToString()
                    });
                LoaiKP.Add(new Infor
                {
                    Code = "Add",
                    Ten = "Thêm"
                });

                rILUE_LoaiKinhPhi.DataSource = LoaiKP;
                //lUE_LoaiKinhPhi.Properties.DataSource = LoaiKP;
                rILUE_TenNguoiLap.DataSource = TenNV;
                //lUE_TenNguoiLap.Properties.DataSource = TenNV;
                rILUE_ToChucCaNhan.DataSource = DuAnHelper.GetCaNhanToChuc();
                gc_ThuChiTamUng_DeXuat.DataSource = DexuatVL;
                return;
            }
            foreach (DeXuat item in DexuatVL)
                DX.Add(item);
            gc_ThuChiTamUng_DeXuat.RefreshDataSource();
        }
        private void sB_LocInfor_Click(object sender, EventArgs e)
        {
            int stt = 0;
            BindingList<DeXuat> DX = (gc_ThuChiTamUng_DeXuat.DataSource as BindingList<DeXuat>);
            if (DX == null)
                return;
            List<int> Delerow = new List<int>();
            foreach (DeXuat item in DX)
            {
                stt++;
                if (!item.Chon)
                    continue;
                //bool check = (bool)gv_DeXuatThuChi.GetRowCellValue(item, "Chon");
                //if (!check)
                //    continue;
                string dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET \"IsEdit\"='{false}' WHERE \"Code\"='{item.Code}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
                Delerow.Add(stt - 1);
                //gv_DeXuatThuChi.
                //gv_DeXuatThuChi.DeleteRow(stt - 1);
                //gv_DeXuatThuChi.SetRowCellValue(item, "Chon", false);
            }
            gv_DeXuatThuChi.BeginUpdate();
            int check = 0;
            foreach (int item in Delerow)
            {
                gv_DeXuatThuChi.DeleteRow(item - check);
                check++;
            }
            gv_DeXuatThuChi.EndUpdate();
        }
        public void Fcn_UpdateDB()
        {
            string query = $"DELETE FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(query);
            string dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}";
            DataTable Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            BindingList<DeXuat> Dexuat = gc_ThuChiTamUng_DeXuat.DataSource as BindingList<DeXuat>;
            List<InForToChuc_CaNhan> Infor = rILUE_ToChucCaNhan.DataSource as List<InForToChuc_CaNhan>;
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            if (Dexuat == null)
                return;
            DataTable dt = converter.ToDataTable(Dexuat);
            foreach(DataColumn colum in Dt.Columns)
            {
                if (!dt.Columns.Contains(colum.ColumnName))
                    dt.Columns.Add(colum.ColumnName, colum.DataType);
            }
            foreach (DataRow row in dt.Rows)
            {
                row["CodeDuAn"] = SharedControls.slke_ThongTinDuAn.EditValue;
                InForToChuc_CaNhan detail = Infor.Find(x => x.Code == row["ToChucCaNhanNhanChiPhiTamUng"].ToString());
                if (detail == null)
                    continue;
                row[detail.ColCodeFK] = detail.Code;
            }
            dt.Columns.Remove("ToChucCaNhanNhanChiPhiTamUng");
            dt.Columns.Remove("Chon");
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT);
        }
        private void sB_TaoThuCong_Click(object sender, EventArgs e)
        {
            editform.Fcn_LoadComponent();
            gv_DeXuatThuChi.OptionsEditForm.CustomEditFormLayout = editform;
            BindingList<DeXuat> DeXuatVL = gv_DeXuatThuChi.DataSource as BindingList<DeXuat>;
            //BindingList<DeXuat> NewVL = new BindingList<DeXuat>();
            //NewVL.Add(new DeXuat
            //{
            //    Code = null,
            //    CodeQLVCYCVT = null,
            //});
            //Fcn_AddData(NewVL);
            if (DeXuatVL == null)
            {
                BindingList<DeXuat> NewVL = new BindingList<DeXuat>();
                NewVL.Add(new DeXuat
                {
                    Code = null,
                    CodeQLVCYCVT = null,
                });
                Fcn_AddData(NewVL);
            }
            else
            {
                gv_DeXuatThuChi.AddNewRow();
                gc_ThuChiTamUng_DeXuat.RefreshDataSource();
            }
            gv_DeXuatThuChi.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
            gv_DeXuatThuChi.OptionsEditForm.PopupEditFormWidth = gc_ThuChiTamUng_DeXuat.Width;
            //gv_DeXuatThuChi.ShowingPopupEditForm();
            ////gv_DeXuatThuChi.CloseEditor();
            gv_DeXuatThuChi.ShowEditForm();
        }

        private void rILUE_LoaiKinhPhi_EditValueChanged(object sender, EventArgs e)
        {
            if (gv_DeXuatThuChi.EditingValue == "Add")
            {
                string LoaiKP = XtraInputBox.Show("Tên kinh phí", "Nhập tên loại kinh phí", "");
                if (LoaiKP !=null)
                {
                    string Code = Guid.NewGuid().ToString();
                    string dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_LOAIKINHPHI} (\"Code\",\"Ten\") VALUES ('{Code}','@LoaiKP)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { LoaiKP });
                    List<Infor> Infor = rILUE_LoaiKinhPhi.DataSource as List<Infor>;
                    //dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_LOAIKINHPHI}";
                    List<Infor> KP = rILUE_LoaiKinhPhi.DataSource as List<Infor>;
                    KP.Add(new Infor
                    {
                        Code = Code,
                        Ten = LoaiKP
                    });
                    KP.RemoveAll(x => x.Code == "Add");
                    KP.Add(new Infor
                    {
                        Code = "Add",
                        Ten = "Thêm"
                    });
                    rILUE_LoaiKinhPhi.DataSource = KP;
                    //rILUE_LoaiKinhPhi. =0;
                    gv_DeXuatThuChi.SetFocusedValue(Code);
                }
                else
                {
                    string oldvalue = (string)gv_DeXuatThuChi.ActiveEditor.OldEditValue;
                    gv_DeXuatThuChi.SetFocusedValue(oldvalue);       
                }
            }
        }

        private void gv_DeXuatThuChi_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (e.MenuType == GridMenuType.Row)
            {
                DXMenuItem menuItem_Chon = new DXMenuItem("Chọn ", this.fcn_Handle_Popup_TCTU_ChonCT);
                menuItem_Chon.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem_Chon);

                DXMenuItem menuItem_KT = new DXMenuItem("Chuyển sang thu ", this.fcn_Handle_Popup_TCTU_KT);
                menuItem_KT.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem_KT);
                DXMenuItem menuItem_KC = new DXMenuItem("Chuyển sang chi ", this.fcn_Handle_Popup_TCTU_KC);
                menuItem_KC.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem_KC);
            }

        }
        private void fcn_Handle_Popup_TCTU_KT(object sender, EventArgs e)
        {
            BindingList<DeXuat> DX = gc_ThuChiTamUng_DeXuat.DataSource as BindingList<DeXuat>;
            List<KhoanThu> KT = new List<KhoanThu>();
            foreach (DeXuat item in DX)
            {
                if (!item.Chon)
                    continue;
                item.NguonThuChi = 2;
                string codeThu = Guid.NewGuid().ToString();
                item.Chon = false;      
                string dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET \"NguonThuChi\"='{2}' WHERE \"Code\"='{item.Code}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
            }
            gc_ThuChiTamUng_DeXuat.Refresh();
            gc_ThuChiTamUng_DeXuat.RefreshDataSource();
        }
        private void fcn_Handle_Popup_TCTU_KC(object sender, EventArgs e)
        {
            BindingList<DeXuat> DX = gc_ThuChiTamUng_DeXuat.DataSource as BindingList<DeXuat>;
            List<KhoanThu> KT = new List<KhoanThu>();
            foreach (DeXuat item in DX)
            {
                if (!item.Chon)
                    continue;
                item.NguonThuChi = 1;
                string codeThu = Guid.NewGuid().ToString();
                item.Chon = false;
                string dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET \"NguonThuChi\"='{1}' WHERE \"Code\"='{item.Code}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
            }
            gc_ThuChiTamUng_DeXuat.Refresh();
            gc_ThuChiTamUng_DeXuat.RefreshDataSource();
        }
        private void fcn_Handle_Popup_TCTU_ChonCT(object sender, EventArgs e)
        {
            var node = gv_DeXuatThuChi.GetSelectedRows();
            gv_DeXuatThuChi.BeginUpdate();
            foreach (int item in node)
                gv_DeXuatThuChi.SetRowCellValue(item, "Chon", true);
            gv_DeXuatThuChi.EndUpdate();
        }
        private void fcn_Handle_Popup_QLVT_LuuND(object sender, EventArgs e)
        {
            //var node = gv_DeXuatThuChi.GetSelectedRows();
            int stt = 0;
            BindingList<DeXuat> DX = (gc_ThuChiTamUng_DeXuat.DataSource  as BindingList<DeXuat>);
            List<int> Delerow = new List<int>();
            foreach (DeXuat item in DX)
            {
                stt++;
                if (!item.Chon)
                    continue;
                //bool check = (bool)gv_DeXuatThuChi.GetRowCellValue(item, "Chon");
                //if (!check)
                //    continue;
                string dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET \"IsEdit\"='{false}' WHERE \"Code\"='{item.Code}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
                Delerow.Add(stt - 1);
                //gv_DeXuatThuChi.
                //gv_DeXuatThuChi.DeleteRow(stt - 1);
                //gv_DeXuatThuChi.SetRowCellValue(item, "Chon", false);
            }
            gv_DeXuatThuChi.BeginUpdate();
            int check = 0;
            foreach (int item in Delerow)
            {
                gv_DeXuatThuChi.DeleteRow(item-check);
                check++;
            }
            gv_DeXuatThuChi.EndUpdate();
        }

        private void gv_DeXuatThuChi_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
        {
            //BaseEdit edit = e.BindableControls["LoaiKinhPhi"] as BaseEdit;
            //edit.EditValueChanged += edit_EditValueChanged;
            //e.Panel.Location = new Point(tL_ChuyenKho.Parent.Location.X + tL_ChuyenKho.Width, tL_ChuyenKho.Parent.Parent.Location.Y + 25);
            Control ctrl = MyExtenstions.FindControl(e.Panel, "Update");
            if (ctrl != null)
            {
                (ctrl as SimpleButton).ImageOptions.Image = imageCollection.Images[0];
                ctrl.Text = "Ok";
                (ctrl as SimpleButton).Click += (s, ee) =>
                {
                    //DeXuat VL = gv_DeXuatThuChi.GetFocusedRow() as DeXuat;
                    //gv_DeXuatThuChi.Columns[0].Visible = true;
                    //gv_DeXuatThuChi.Columns["ChonChiTiet"].Visible = true;
                    //gv_DeXuatThuChi.OptionsBehavior.EditingMode = GridEditingMode.Default;
                    gv_DeXuatThuChi.SetRowCellValue(gv_DeXuatThuChi.FocusedRowHandle, "ChonChiTiet", "Xem chi tiết");

                    //ChuyenVatTu CVL = tL_ChuyenKho.GetFocusedRow() as ChuyenVatTu;
                    //CVL.HoanThanh = true;
                    //MessageShower.ShowInformation("Kiểm tra lại số liệu và nhấn chọn Ok để hoàn thành!", "Lưu ý", MessageBoxButtons.OK);
                    //tL_ChuyenKho.RefreshDataSource();
                };
            }

            ctrl = MyExtenstions.FindControl(e.Panel, "Cancel");
            if (ctrl != null)
            {
                (ctrl as SimpleButton).ImageOptions.Image = imageCollection.Images[1];
                ctrl.Text = "Đóng";
                //(ctrl as SimpleButton).Click += (s, ee) =>
                //{
                //    //DeXuat VL = gv_DeXuatThuChi.GetFocusedRow() as DeXuat;
                //    //gv_DeXuatThuChi.Columns[0].Visible = true;
                //    //gv_DeXuatThuChi.Columns["ChonChiTiet"].Visible = true;
                //    gv_DeXuatThuChi.OptionsBehavior.EditingMode = GridEditingMode.Default;
                //    //ChuyenVatTu CVL = tL_ChuyenKho.GetFocusedRow() as ChuyenVatTu;
                //    //CVL.HoanThanh = true;
                //    //MessageShower.ShowInformation("Kiểm tra lại số liệu và nhấn chọn Ok để hoàn thành!", "Lưu ý", MessageBoxButtons.OK);
                //    //tL_ChuyenKho.RefreshDataSource();
                //};
            }
        }

        private void gv_DeXuatThuChi_RowUpdated(object sender, RowObjectEventArgs e)
        {

        }

        private void gv_DeXuatThuChi_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            //if((string)gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "CodeQLVCYCVT")!=""&&e.Column.FieldName=="CongTrinh")
            //{
            //    MessageShower.ShowInformation("Không được thay đổi ô này!", "Cảnh báo", MessageBoxButtons.OK);
            //    gv_DeXuatThuChi.CancelUpdateCurrentRow();      
            //}            
            if((string)gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "CodeQLVCYCVT")!=""&&e.Column.FieldName=="CodeHd")
            {
                MessageShower.ShowWarning("Không được thay đổi ô này!", "Cảnh báo");
                gv_DeXuatThuChi.CancelUpdateCurrentRow();      
            }    
        }

        private void gv_DeXuatThuChi_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            e.DefaultDraw();
            Color color = QLVC;
            Rectangle markRectangle;
            string priorityText = " -Nguồn từ quản lý vận chuyển ";

            Image img = imageCollection.Images[2];
            for (int i = 0; i < 4; i++)
            {
                if (i == 1)
                {
                    color = ClTDKH;
                    priorityText = " -Nguồn từ tiến độ KH";
                }
                else if(i==2)
                {
                    color = TC;
                    priorityText = " -Nhập thủ công";
                }      
                else if(i==3)
                {
                    color = HD;
                    priorityText = " -Nhập từ hợp đồng";
                }
                markRectangle = new Rectangle(e.Bounds.X + offset + (markWidth + offset+200) * i, e.Bounds.Y + offset , markWidth, markWidth);
                e.Cache.FillEllipse(markRectangle.X, markRectangle.Y, markRectangle.Width, markRectangle.Height, color);
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                e.Appearance.Options.UseTextOptions = true;
                e.Appearance.DrawString(e.Cache, priorityText, new Rectangle(markRectangle.Right + offset, markRectangle.Y, e.Bounds.Width, markRectangle.Height));
            }
            Rectangle Rectangle = new Rectangle(e.Bounds.X + offset+(markWidth + offset + 200) * 4, e.Bounds.Y + offset, markWidth, markWidth);
            e.Cache.DrawImage(img, e.Bounds.X + offset + (markWidth + offset+200) * 4, e.Bounds.Y + offset);
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            e.Appearance.FontStyleDelta = FontStyle.Bold;
            e.Appearance.ForeColor = Color.Red;
            e.Appearance.Options.UseTextOptions = true;
            e.Appearance.DrawString(e.Cache, "Lưu ý: Chỉ gộp những nội dung ứng cùng nguồn", new Rectangle(Rectangle.Right + offset, Rectangle.Y, e.Bounds.Width, Rectangle.Height));
        }

        private void gv_DeXuatThuChi_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Appearance.Options.UseTextOptions = true;
            e.DefaultDraw();

            if (e.Column.FieldName == "Chon")
            {
                Color color;
                bool IsVanChuyen =(bool)gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "IsVanChuyen");
                bool IsTDKH =(bool)gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "IsTDKH");
                object CodeHD =gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "CodeHd");
                if (IsVanChuyen)
                    color = QLVC;
                else if (IsTDKH)
                    color = ClTDKH;
                else if (CodeHD != null)
                    color = HD;
                else
                    color = TC;
                e.Cache.FillEllipse(e.Bounds.X + 1, e.Bounds.Y + 1, markWidth, markWidth, color);
            }
        }

        private void gv_DeXuatThuChi_EditFormHidden(object sender, EditFormHiddenEventArgs e)
        {
            CheckNewRow = false;
            gv_DeXuatThuChi.OptionsBehavior.EditingMode = GridEditingMode.Default;
            gv_DeXuatThuChi.RefreshData();
            gc_ThuChiTamUng_DeXuat.RefreshDataSource();
            BindingList<PhuLucThuCong> PL = editform.Fcn_DataThuCong();
            List<Infor> Infor = editform.Fcn_DataLoaiKP();
            string[] CodeLKP = (rILUE_LoaiKinhPhi.DataSource as List<Infor>).AsEnumerable().Select(x => x.Code).ToArray();
            List<Infor> List = Infor.FindAll(x => !CodeLKP.Contains(x.Code));
            string dbString = "";
            if (List.Count != 0)
            {
                rILUE_LoaiKinhPhi.DataSource = Infor;
                foreach (Infor item in List)
                {
                    dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_LOAIKINHPHI} (\"Code\",\"Ten\") VALUES ('{item.Code}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { item.Ten });
                }
            }
            string Code = Guid.NewGuid().ToString();
            //string CodeKC = Guid.NewGuid().ToString();
            DeXuat DX =(object)gv_DeXuatThuChi.GetRow(gv_DeXuatThuChi.RowCount-1) as DeXuat;
            if (DX == null)
                return;
            if (DX.NoiDungUng == null)
            {
                gv_DeXuatThuChi.DeleteRow(gv_DeXuatThuChi.RowCount - 1);
                return;
            }
            dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} WHERE \"Code\"='{DX.Code}'";
            DataTable TCTU_DeXuat = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (TCTU_DeXuat.Rows.Count == 1)
                return;
            DX.Code = Code;
            DX.TrangThai = 1;
            DX.NguonThuChi = 1;
            DX.IsEdit = true;
            CheckNewRow = true;
            gc_ThuChiTamUng_DeXuat.RefreshDataSource();
            InForToChuc_CaNhan detail = (rILUE_ToChucCaNhan.DataSource as List<InForToChuc_CaNhan>).Find(x => x.Code == DX.ToChucCaNhanNhanChiPhiTamUng);



            dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} (\"GiaTriDotNay\",\"TrangThai\",\"Code\",\"NoiDungUng\",\"CodeDuAn\",\"CongTrinh\",\"IsEdit\",\"LoaiKinhPhi\",\"NguoiLapTamUng\",\"ToChucCaNhanNhanChiPhiTamUng\") " +
    $"VALUES ('{DX.GiaTriDotNay}','{1}','{Code}',@NoiDungUng,'{SharedControls.slke_ThongTinDuAn.EditValue}','{DX.CongTrinh}','{true}','{DX.LoaiKinhPhi}','{DX.NguoiLapTamUng}','{detail.Code}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { DX.NoiDungUng});        
            if (PL == null)
                return;
            foreach (PhuLucThuCong item in PL)
                item.CodeCha = Code;     
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(PL);
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL);
        }

        private void rEHL_XemChiTiet_Click(object sender, EventArgs e)
        {
            DeXuat VL = gv_DeXuatThuChi.GetFocusedRow() as DeXuat;
            if (VL.ChonChiTiet == null)
                return;
            string Code = VL.Code;
            string dbString = ""; DataTable Dt = null;
            string tbl = "";
            string ColCode = "";
            if (VL.IsVanChuyen)
            {
                dbString = $"SELECT *" +
                    $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCT} " +
                    $" WHERE \"CodeCha\"='{Code}'";
                Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                tbl = ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCT;
            }
            else if (VL.IsTDKH)
            {
                tbl = ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH;
                if (VL.IsVatLieu)
                    dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.Code,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayBD,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayKT,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.ThanhTien,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.KhoiLuong,{TDKH.TBL_KHVT_VatTu}.VatTu as TenCongViec,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaHieu,{TDKH.TBL_KHVT_VatTu}.DonVi " +
    $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH} " +
    $"LEFT JOIN {TDKH.TBL_KHVT_VatTu} " +
    $"ON {TDKH.TBL_KHVT_VatTu}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeVatTu " +
    $" WHERE \"CodeCha\"='{Code}'";
                else
                    dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.Code,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.ThanhTien," +
                        $"{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayBD,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayKT," +
                        $"{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.KhoiLuong,COALESCE(cttk.TenCongTac, dmct.TenCongTac) as TenCongViec," +
                        $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) as MaHieu,COALESCE(cttk.DonVi, dmct.DonVi) as DonVi " +
    $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH} " +
    $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
    $"ON cttk.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeCongTac " +  
    $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
    $"ON dmct.Code=cttk.CodeCongTac " +
    $" WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeCha='{Code}'";
                Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            }
            else if(VL.CodeHd != null)
            {
                dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.*," +
                    $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) as TenCongViec,COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) as MaHieu," +
                    $"COALESCE(cttk.DonVi, dmct.DonVi) as DonVi " +
                $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD} " +
                $"LEFT JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                $"ON {MyConstant.TBL_HopDong_PhuLuc}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.CodePl " +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"ON cttk.Code={MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan " +          
                $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
                $"ON dmct.Code=cttk.CodeCongTac " +
                $" WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.CodeCha='{Code}'";
                Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            }
            else
            {
                dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL} WHERE \"CodeCha\"='{Code}'";
                Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                Dt.Columns.Add("ThanhTien", typeof(double));
                Dt.Columns["ThanhTien"].Expression = $"[DonGia]*[KhoiLuong]";
                tbl = ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL;

            }
            List<PhuLucThuCong_ChiTiet> PL = DuAnHelper.ConvertToList<PhuLucThuCong_ChiTiet>(Dt);
            Form_ThuChiTamUngPhuLuc frmPL = new Form_ThuChiTamUngPhuLuc();
            int stt = 1;
            PL.ForEach(x => x.TenNoiDungUng = VL.NoiDungUng);
            PL.ForEach(x => x.STT = stt++);
            frmPL.Fcn_Update(tbl, PL);
            if (VL.IsTDKH|| VL.IsVanChuyen|| VL.CodeHd != null)
                frmPL.Fcn_VisibleColum();
            else
                frmPL.m__TRUYENDATA = new Form_ThuChiTamUngPhuLuc.DE__TRUYENDATA(Fcn_UpdatrThanhTienTC);
            frmPL.ShowDialog();
        }
        private void Fcn_UpdatrThanhTienTC(long TT)
        {
            string CodeCha =(string)gv_DeXuatThuChi.GetRowCellValue(gv_DeXuatThuChi.FocusedRowHandle, "Code");
            string dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET \"GiaTriDotNay\"='{TT}' WHERE \"Code\"='{CodeCha}' ";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
            gv_DeXuatThuChi.SetRowCellValue(gv_DeXuatThuChi.FocusedRowHandle, "GiaTriDotNay", TT);
        }
        private void sB_TaoKeHoach_Click(object sender, EventArgs e)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.Caption = "Lựa chọn giai đoạn";
            args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Yes, DialogResult.Cancel};
            args.Showing += Args_Showing_TDKH;
            DevExpress.XtraEditors.XtraMessageBox.Show(args);
        }
        private void Args_Showing_TDKH(object sender, XtraMessageShowingArgs e)
        {
            e.Form.Appearance.FontStyleDelta = FontStyle.Bold;
            e.Form.Appearance.FontSizeDelta = 2;
            foreach (var control in e.Form.Controls)
            {
                SimpleButton button = control as SimpleButton;
                if (button != null)
                {
                    button.ImageOptions.SvgImageSize = new Size(16, 16);
                    // button.Height = 25;
                    switch (button.DialogResult.ToString())
                    {
                        case ("OK"):
                            button.ImageOptions.SvgImage = svgImageCollection1[0];
                            button.Text = "Thi Công";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Form_ThuChiTamUngTDKH TDKH = new Form_ThuChiTamUngTDKH();
                                TDKH.Fcn_UpdateData(button.Text);
                                TDKH.m__TRUYENDATA = new Form_ThuChiTamUngTDKH.DE__TRUYENDATA(Fcn_UpdateDataTDKH);
                                TDKH.ShowDialog();
                            };
                            break;
                        case ("Yes"):
                            button.ImageOptions.SvgImage = svgImageCollection1[3];
                            button.Text = "Kế Hoạch";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Form_ThuChiTamUngTDKH TDKH = new Form_ThuChiTamUngTDKH();
                                TDKH.Fcn_UpdateData(button.Text);
                                TDKH.m__TRUYENDATA = new Form_ThuChiTamUngTDKH.DE__TRUYENDATA(Fcn_UpdateDataTDKH);
                                TDKH.ShowDialog();
                            };
                            break;
                        default:
                            button.ImageOptions.SvgImage = svgImageCollection1[5];
                            button.Text = "Thoát";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) => { e.Form.Close(); };
                            break;
                    }
                }
            }
        }
        private void gv_DeXuatThuChi_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            string Code = (string)gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "Code");
            if (Code == null)
                return;
            string CodeDb = e.Column.FieldName;
            if (CodeDb == "ToChucCaNhanNhanChiPhiTamUng")
            {
                //List<InForToChuc_CaNhan> Infor = rILUE_ToChucCaNhan.DataSource as List<InForToChuc_CaNhan>;

                //InForToChuc_CaNhan detail = Infor.Find(x => x.Code == e.Value.ToString());
                //string dbstring = "";
                //if (gv_DeXuatThuChi.ActiveEditor.OldEditValue != null)
                //{
                //    string oldvalue = (string)gv_DeXuatThuChi.ActiveEditor.OldEditValue;
                //    InForToChuc_CaNhan old = Infor.Find(x => x.Code == oldvalue);
                //    dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET '{detail.ColCodeFK}'='{e.Value.ToString()}','{old.ColCodeFK}'=NULL WHERE \"Code\"='{Code}' ";
                //    if (detail.ColCodeFK == old.ColCodeFK)
                //        dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET '{detail.ColCodeFK}'='{e.Value.ToString()}' WHERE \"Code\"='{Code}' ";
                //    DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
                //    return;
                //}
                //if (detail == null)
                //    return;
                string dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET \"ToChucCaNhanNhanChiPhiTamUng\"='{e.Value}' WHERE \"Code\"='{Code}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);

                //row[detail.ColCodeFK] = detail.Code;
            }
            else if (CodeDb != "Chon" && CodeDb != ""&& CodeDb != "ChonChiTiet"&&CodeDb != "GiaTriDotNay")
            {
                string dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET '{CodeDb}'=@Value WHERE \"Code\"='{Code}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring, parameter: new object[] { e.Value });
            }
            else if (CodeDb == "GiaTriDotNay")
            {
                bool IsVanChuyen = (bool)gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "IsVanChuyen");
                bool IsTDKH = (bool)gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "IsTDKH");
                if (!IsVanChuyen && !IsTDKH)
                {
                    string dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET '{CodeDb}'=@Value WHERE \"Code\"='{Code}' ";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring, parameter: new object[] { e.Value });
                }
                else
                {
                    DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn muốn đổi giá trị đợt này khác với phụ lục?", "Cảnh báo!");
                    if (rs == DialogResult.Yes)
                    {
                        string dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET \"IsEditGiaTri\"='{true}',\"GiaTriDotNay\"='{e.Value}' WHERE \"Code\"='{Code}' ";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
                        gv_DeXuatThuChi.SetRowCellValue(e.RowHandle, "IsEditGiaTri", true);
                    }
                    else
                        gv_DeXuatThuChi.CancelUpdateCurrentRow();
                }

            }

        }
        private void gv_DeXuatThuChi_ShowingEditor(object sender, CancelEventArgs e)
        {
            DeXuat DX = gv_DeXuatThuChi.GetFocusedRow() as DeXuat;
            if (DX.CodeQLVCYCVT != "" && gv_DeXuatThuChi.FocusedColumn.FieldName == "CongTrinh")
            {
                //MessageShower.ShowInformation("Không được thay đổi ô này!", "Cảnh báo", MessageBoxButtons.OK);
                //e.Cancel = true;
            }
            else if (gv_DeXuatThuChi.FocusedColumn.FieldName == "CodeHd")
            {
                MessageShower.ShowWarning("Không được thay đổi ô này!", "Cảnh báo");
                e.Cancel = true;
            }
            if ((gv_DeXuatThuChi.FocusedColumn.FieldName == "Chon"))
            {
                DeXuat dx = gv_DeXuatThuChi.GetFocusedRow() as DeXuat;
                string dbString = $"SELECT \"IsEdit\" FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} WHERE \"Code\"='{dx.Code}'";
                DataTable Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dx.TrangThai > 1)
                {
                    MessageShower.ShowWarning("Nội dung đã gửi duyệt hoặc đang chờ duyệt nên không được chỉnh sửa!", "Thông báo!");
                    e.Cancel = true;
                }
                if (string.IsNullOrEmpty(dx.CongTrinh))
                {
                    DialogResult rs = MessageShower.ShowYesNoQuestion("Đầu việc chưa có công trình, bạn có muốn lấy công trình mặc định không??????");
                    if (rs == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                    dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                    Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    dx.CongTrinh = Dt.Rows[0]["Code"].ToString();
                    dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET \"CongTrinh\"='{dx.CongTrinh}' WHERE \"Code\"='{dx.Code}' ";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
        }
        private void sB_Qlvc_Click(object sender, EventArgs e)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.Caption = "Lựa chọn giai đoạn";
            args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel, DialogResult.Yes,DialogResult.No,DialogResult.Abort,DialogResult.Retry };
            args.Showing += Args_Showing;
            DevExpress.XtraEditors.XtraMessageBox.Show(args);
        }
        private void Fcn_UpdateDataTDKH(List<TCTU_TDKH> ThuChiTDKH, string ColCode, string TenNoiDung, string LoaiCP, string ToChucCaNhan)
        {
            CheckNewRow = false;
            if (ThuChiTDKH == null)
                return;
            string Code = Guid.NewGuid().ToString();
            string CodeKC = Guid.NewGuid().ToString();
            long GiaTri = 0;
            ThuChiTDKH.ForEach(x => GiaTri += x.ThanhTienKeHoach);
            string dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} (\"TrangThai\",\"ToChucCaNhanNhanChiPhiTamUng\",\"LoaiKinhPhi\",\"GiaTriDotNay\",\"IsEdit\",\"IsVatLieu\",\"Code\",\"NoiDungUng\",\"CodeDuAn\",\"CongTrinh\",\"IsTDKH\") " +
                $"VALUES ('{1}','{ToChucCaNhan}','{LoaiCP}','{GiaTri}','{true}','{ThuChiTDKH.FirstOrDefault().IsVatLieu}','{Code}',@TenNoiDung,'{SharedControls.slke_ThongTinDuAn.EditValue}','{ThuChiTDKH.FirstOrDefault().CodeCT}','{true}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { TenNoiDung });
            BindingList<DeXuat> DeXuatVL = new BindingList<DeXuat>();
            DeXuatVL.Add(new DeXuat
            {
                Code = Code,
                GiaTriDotNay = GiaTri,
                CodeKC=CodeKC,
                NguonThuChi=1,
                TrangThai=1,
                NoiDungUng = TenNoiDung,
                CongTrinh = ThuChiTDKH.FirstOrDefault().CodeCT,
                IsTDKH = true,
                IsVatLieu= ThuChiTDKH.FirstOrDefault().IsVatLieu,
                ToChucCaNhanNhanChiPhiTamUng=ToChucCaNhan,
                LoaiKinhPhi=LoaiCP
            });
            foreach (TCTU_TDKH item in ThuChiTDKH)
            {
                string code = Guid.NewGuid().ToString();
                dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH} (\"NgayBD\",\"NgayKT\",\"LoaiCT\",\"Code\",\"CodeCha\",\"ThanhTien\",\"KhoiLuong\",'{ColCode}') " +
                    $"VALUES ('{item.NgayBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.NgayKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.LoaiCT}','{code}','{Code}'," +
                    $"'{item.ThanhTienKeHoach}','{item.KhoiLuongKeHoach}','{item.ID}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            Fcn_AddData(DeXuatVL);
            CheckNewRow = true;
        }
        private void Fcn_UpdateDauViecQLVC(string CodeCT,DataTable TH,string parentID,string NoiDungUng,string ColTT,long GiaTri,string ColKL,string NBD,string NKT,string colCode)
        {
            CheckNewRow = false;
            if (TH == null)
                return;
            string Code = Guid.NewGuid().ToString();
            string CodeKC = Guid.NewGuid().ToString();

            string dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} (\"TrangThai\",\"GiaTriDotNay\",\"IsEdit\",\"Code\"," +
                $"\"NoiDungUng\",\"CodeDuAn\",\"CongTrinh\"," +
                $"\"IsVanChuyen\") VALUES ('{1}','{GiaTri}','{true}','{Code}',@NoiDungUng,'{SharedControls.slke_ThongTinDuAn.EditValue}','{CodeCT}','{true}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { NoiDungUng });
            BindingList<DeXuat> DeXuatVL = new BindingList<DeXuat>();
            DeXuatVL.Add(new DeXuat
            {
                Code = Code,
                CodeKC=CodeKC,
                GiaTriDotNay = GiaTri,
                NoiDungUng = NoiDungUng,
                CongTrinh = parentID,
                IsVanChuyen=true,
                TrangThai=1,
                NguonThuChi = 1
            });
            foreach (DataRow item in TH.Rows)
            {
                string code = Guid.NewGuid().ToString();
                dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCT} " +
                    $"(\"DonVi\",\"TenCongViec\",\"MaHieu\",\"Code\",\"CodeCha\",\"ThanhTien\",\"KhoiLuong\",\"NgayBD\",\"NgayKT\") " +
                    $"VALUES ('{item["DonVi"]}',@TenVatTu,'{item["MaVatTu"]}','{code}','{Code}','{item[ColTT]}','{item[ColKL]}','{NBD}','{NKT}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { item["TenVatTu"] });
            }
            Fcn_AddData(DeXuatVL);
            CheckNewRow = true;
        }

        private void Args_Showing(object sender, XtraMessageShowingArgs e)
        {
            e.Form.Appearance.FontStyleDelta = FontStyle.Bold;
            e.Form.Appearance.FontSizeDelta = 2;
            foreach (var control in e.Form.Controls)
            {
                SimpleButton button = control as SimpleButton;
                if (button != null)
                {
                    button.ImageOptions.SvgImageSize = new Size(16, 16);
                    // button.Height = 25;
                    switch (button.DialogResult.ToString())
                    {
                        case ("OK"):
                            button.ImageOptions.SvgImage = svgImageCollection1[0];
                            button.Text = "Đề xuất";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Form_TCTU_LayDauViec VL = new Form_TCTU_LayDauViec();
                                VL.Fcn_Update(button.Text);
                                VL.m_TruyenData = new Form_TCTU_LayDauViec.DE__TRUYENDATAVL(Fcn_UpdateDauViecQLVC);
                                VL.ShowDialog();
                            }; 
                            break;
                        case ("Cancel"):
                            button.ImageOptions.SvgImage = svgImageCollection1[1];
                            button.Text = "Nhập kho";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Form_TCTU_LayDauViec VL = new Form_TCTU_LayDauViec();
                                VL.Fcn_Update(button.Text);
                                VL.m_TruyenData = new Form_TCTU_LayDauViec.DE__TRUYENDATAVL(Fcn_UpdateDauViecQLVC);
                                VL.ShowDialog();
                            };
                            break;
                        case ("Yes"):
                            button.ImageOptions.SvgImage = svgImageCollection1[2];
                            button.Text = "Xuất kho";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Form_TCTU_LayDauViec VL = new Form_TCTU_LayDauViec();
                                VL.Fcn_Update(button.Text);
                                VL.m_TruyenData = new Form_TCTU_LayDauViec.DE__TRUYENDATAVL(Fcn_UpdateDauViecQLVC);
                                VL.ShowDialog();
                            };
                            break;          
                        case ("No"):
                            button.ImageOptions.SvgImage = svgImageCollection1[3];
                            button.Text = "Chuyển kho";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Form_TCTU_LayDauViec VL = new Form_TCTU_LayDauViec();
                                VL.Fcn_Update(button.Text);
                                VL.m_TruyenData = new Form_TCTU_LayDauViec.DE__TRUYENDATAVL(Fcn_UpdateDauViecQLVC);
                                VL.ShowDialog();
                            };
                            break;                  
                        case ("Abort"):
                            button.ImageOptions.SvgImage = svgImageCollection1[4];
                            button.Text = "Vận chuyển";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Form_TCTU_LayDauViec VL = new Form_TCTU_LayDauViec();
                                VL.Fcn_Update(button.Text);
                                VL.m_TruyenData = new Form_TCTU_LayDauViec.DE__TRUYENDATAVL(Fcn_UpdateDauViecQLVC);
                                VL.ShowDialog();
                            };
                            break;
                        default:
                            button.ImageOptions.SvgImage = svgImageCollection1[5];
                            button.Text = "Thoát";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) => { e.Form.Close(); };
                            break;
                    }
                }
            }
        }
        public DeXuat Fcn_UpdateNewList()
        {
            if (CheckNewRow)
            {
                DeXuat DX = new DeXuat();
                return DX = gv_DeXuatThuChi.GetRow(gv_DeXuatThuChi.RowCount - 1) as DeXuat;
            }
            else
                return null;

        }
        public event EditFormHiddenEventHandler Gv_DeXuatThuChi_EditFormHidden
        {
            add
            {
                gv_DeXuatThuChi.EditFormHidden += value;
            }
            remove
            {
                gv_DeXuatThuChi.EditFormHidden -= value;
            }
        }      
        public event System.EventHandler sb_Qlvc_Click
        {
            add
            {
                sB_Qlvc.Click += value;
            }
            remove
            {
                sB_Qlvc.Click -= value;
            }
        }  
        public event System.EventHandler sb_TaoKeHoach_Click
        {
            add
            {
                sB_TaoKeHoach.Click += value;
            }
            remove
            {
                sB_TaoKeHoach.Click -= value;
            }
        }     
        public event System.EventHandler sb_LayTuHopDong_Click
        {
            add
            {
                sB_LayTuHopDong.Click += value;
            }
            remove
            {
                sB_LayTuHopDong.Click -= value;
            }
        }      

        private void gv_DeXuatThuChi_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.CellValue == null)
                return;
            if (e.Column.FieldName == "GiaTriDotNay")
            {
                if (gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "IsEditGiaTri") == null)
                    return;
                bool Edit = (bool)gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "IsEditGiaTri");
                if (Edit)
                    e.Appearance.ForeColor = Color.Salmon;
            }
        }

        private void sB_LayTuHopDong_Click(object sender, EventArgs e)
        {
            Form_TCTU_LayDauViecHopDong HopDong = new Form_TCTU_LayDauViecHopDong();
            HopDong.LoadDanhSachHopDong();
            HopDong.m__TRUYENDATA = new Form_TCTU_LayDauViecHopDong.DE__TRUYENDATA(Fcn_UpdateDataHopDong);
            HopDong.ShowDialog();
        }
        private void Fcn_UpdateDataHopDong(List<LayCongTacHopDong> CTHD, string TenNoiDung,string CodeHD,bool chi,bool HD,string ToChucCaNhan)
        {
            string dbString = "";
            int nguonthuchi = chi == true ? 1 : 2;
            CheckNewRow = false;
            BindingList<DeXuat> DeXuatVL = new BindingList<DeXuat>();
            string Code = Guid.NewGuid().ToString();
            if (!HD)
            {
                if (CTHD == null)
                {
                    return;
                }

                string CodeKC = Guid.NewGuid().ToString();
                double GiaTri = 0;
                CTHD.ForEach(x => GiaTri += x.ThanhTienThiCong);
               
                dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} (\"TrangThai\",\"ToChucCaNhanNhanChiPhiTamUng\"," +
                    $"\"NguonThuChi\",\"GiaTriDotNay\",\"IsEdit\",\"Code\",\"NoiDungUng\",\"CodeDuAn\",\"CongTrinh\",\"CodeHd\")" +
                    $" VALUES ('{1}','{ToChucCaNhan}','{nguonthuchi}','{GiaTri}','{true}','{Code}',@TenNoiDung,'{SharedControls.slke_ThongTinDuAn.EditValue}','{CTHD.FirstOrDefault().CodeCT}','{CodeHD}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { TenNoiDung });
                
                DeXuatVL.Add(new DeXuat
                {
                    Code = Code,
                    GiaTriDotNay = (long)Math.Round(GiaTri),
                    NoiDungUng = TenNoiDung,
                    NguonThuChi = nguonthuchi,
                    CongTrinh = CTHD.FirstOrDefault().CodeCT,
                    CodeHd = CodeHD,
                    TrangThai=1,
                    ToChucCaNhanNhanChiPhiTamUng=ToChucCaNhan
                });
                foreach (LayCongTacHopDong item in CTHD)
                {
                    string code = Guid.NewGuid().ToString();
                    dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD} (\"NgayBD\",\"NgayKT\",\"Code\",\"CodeCha\",\"ThanhTien\",\"KhoiLuong\",\"CodePl\") VALUES ('{item.NgayBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.NgayKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{code}','{Code}','{item.ThanhTienThiCong}','{item.KhoiLuongThiCong}','{item.ID}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
                Fcn_AddData(DeXuatVL);
            }
            else
            {
                dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"Code\"='{CodeHD}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} (\"TrangThai\",\"ToChucCaNhanNhanChiPhiTamUng\",\"NguonThuChi\",\"GiaTriDotNay\"," +
                    $"\"IsEdit\",\"Code\",\"NoiDungUng\",\"CodeDuAn\",\"CodeHd\") " +
                    $"VALUES ('{1}','{ToChucCaNhan}','{nguonthuchi}','{dt.AsEnumerable().FirstOrDefault()["GiaTriHopDong"]}','{true}','{Code}',@TenNoiDung," +
                    $"'{SharedControls.slke_ThongTinDuAn.EditValue}','{CodeHD}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { TenNoiDung });
                DeXuatVL.Add(new DeXuat
                {
                    Code = Code,
                    GiaTriDotNay =(long)Math.Round(double.Parse(dt.AsEnumerable().FirstOrDefault()["GiaTriHopDong"].ToString())),
                    NoiDungUng = TenNoiDung,
                    NguonThuChi = nguonthuchi,
                    CodeHd = CodeHD,
                    TrangThai=1,
                    ToChucCaNhanNhanChiPhiTamUng=ToChucCaNhan
                });
                foreach (LayCongTacHopDong item in CTHD)
                {
                    string code = Guid.NewGuid().ToString();
                    dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD} (\"NgayBD\",\"NgayKT\",\"Code\",\"CodeCha\",\"ThanhTien\",\"KhoiLuong\",\"CodePl\") VALUES ('{item.NgayBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.NgayKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{code}','{Code}','{item.ThanhTienThiCong}','{item.KhoiLuongThiCong}','{item.ID}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
                Fcn_AddData(DeXuatVL);
            }
            CheckNewRow = true;
        }
        public event System.EventHandler SB_GuiDuyet_Click
        {
            add
            {
                sB_GuiDuyet.Click += value;
            }
            remove
            {
                sB_GuiDuyet.Click -= value;
            }
        }     
        public event System.EventHandler Sb_CapNhapTCTU_Click
        {
            add
            {
                sb_CapNhapTCTU.Click += value;
            }
            remove
            {
                sb_CapNhapTCTU.Click -= value;
            }
        }     
        public event System.EventHandler Sb_DeXuatExport_Click
        {
            add
            {
                sb_DeXuatExport.Click += value;
            }
            remove
            {
                sb_DeXuatExport.Click -= value;
            }
        }
        private void gv_DeXuatThuChi_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "TrangThai") == null)
                return;
            if ((int)gv_DeXuatThuChi.GetRowCellValue(e.RowHandle, "TrangThai") == 2)
                e.Appearance.ForeColor = Color.Red;
        }
        private void Args_Showing_Begin(object sender, XtraMessageShowingArgs e)
        {
            e.Form.Icon = Icon;
        }
        private async void sB_GuiDuyet_Click(object sender, EventArgs e)
        {
            _IsQuyTrinh = false;
            if (gv_DeXuatThuChi.DataSource == null)
                return;
            var ngay = "";
            try
            {
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                args.Caption = "Cài đặt ngày yêu cầu vật tư";
                args.Prompt = "Ngày Yêu Cầu";
                args.DefaultButtonIndex = 0;
                args.Showing += Args_Showing_Begin;
                DateEdit editor = new DateEdit();
                editor.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
                editor.Properties.Mask.EditMask = MyConstant.CONST_DATE_FORMAT_SPSHEET;
                args.Editor = editor;
                args.DefaultResponse = DateTime.Now.Date;
                ngay = XtraInputBox.Show(args).ToString();
            }
            catch
            {
                return;
            }
            XtraFormLuaChonDuyet SelectDuyet = new XtraFormLuaChonDuyet();
            SelectDuyet.ShowDialog();
            if (SelectDuyet.CancelSelect)
                return;
            
            //var dr = MessageShower.ShowYesNoCancelQuestionWithCustomText("Chọn phương pháp duyệt!", "Lựa chọn", yesString: "Duyệt 1 bước", Nostring: "Duyệt theo quy trình");

            //if (dr == DialogResult.Cancel)
            //    return;

            bool isQuyTrinh = _IsQuyTrinh= SelectDuyet.DuyetTheoQuyTrinh;
            int countOK = 0;
            int countBad = 0;
            int count = 1;
            bool IsGiayDuyet = SelectDuyet._CheckPhieuDuyet;
            List<string> Noti = new List<string>();
            List<string> LstCode = new List<string>();
            string dbString = "";
            List<DeXuat> DX = new List<DeXuat>(gv_DeXuatThuChi.DataSource as BindingList<DeXuat>).FindAll(x => x.Chon == true);
            if (DX.Count == 0)
            {
                MessageShower.ShowInformation("Vui lòng chọn ít nhất 1 đầu việc để gửi duyệt!!!!!!");
                return;
            }
            List<KhoanChi> KC = new List<KhoanChi>();
            List<KhoanThu> KT = new List<KhoanThu>();
            List<Tbl_TCTU_DeXuatViewModel> DxModel = new List<Tbl_TCTU_DeXuatViewModel>();
            List<Tbl_TCTU_KhoanChiViewModel> KCModel = new List<Tbl_TCTU_KhoanChiViewModel>();
            List<Tbl_TCTU_KhoanThuViewModel> KTModel = new List<Tbl_TCTU_KhoanThuViewModel>();
            var date = DateTime.Parse(ngay);
            bool chitietThuCong = false;
            foreach (DeXuat item in DX)
            {
                if (item.CongTrinh == null)
                {
                    MessageShower.ShowError($"Vui lòng chọn lại Tên công trình cho Nội dung ứng:{item.NoiDungUng}");
                    continue;
                }
                WaitFormHelper.ShowWaitForm($"Quá trình gửi duyệt đang được tiến hành,Vui lòng chờ: {count++}/{DX.Count}");
                if (IsGiayDuyet)
                    LstCode.Add(item.Code);
                item.Chon =chitietThuCong= false;
                DataTable Dt = new DataTable();
                string tbl = "";
                if (item.NguonThuChi == 1)
                {
                    if (item.IsVanChuyen)
                    {
                        dbString = $"SELECT *" +
                            $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCT} " +
                            $" WHERE \"CodeCha\"='{item.Code}'";
                        Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        tbl = ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCT;
                    }
                    else if (item.IsTDKH)
                    {
                        tbl = ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH;
                        if (item.IsVatLieu)
                            dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.Code,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayBD,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayKT,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.ThanhTien,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.KhoiLuong,{TDKH.TBL_KHVT_VatTu}.VatTu as TenCongViec,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaHieu,{TDKH.TBL_KHVT_VatTu}.DonVi " +
            $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH} " +
            $"LEFT JOIN {TDKH.TBL_KHVT_VatTu} " +
            $"ON {TDKH.TBL_KHVT_VatTu}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeVatTu " +
            $" WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeCha='{item.Code}'";
                        else
                            dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.Code,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.ThanhTien,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayBD,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayKT,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.KhoiLuong,{TDKH.TBL_DanhMucCongTac}.TenCongTac as TenCongViec,{TDKH.TBL_DanhMucCongTac}.MaHieuCongTac as MaHieu,{TDKH.TBL_DanhMucCongTac}.DonVi " +
            $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH} " +
            $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
            $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeCongTac " +
            $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} " +
            $"ON {TDKH.TBL_DanhMucCongTac}.Code={TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
            $" WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeCha='{item.Code}'";
                        Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    }
                    else if (item.CodeHd != null)
                    {
                        dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.Code,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.NgayBD,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.NgayKT,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.ThanhTien,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.KhoiLuong,{TDKH.TBL_DanhMucCongTac}.TenCongTac as TenCongViec,{TDKH.TBL_DanhMucCongTac}.MaHieuCongTac as MaHieu,{TDKH.TBL_DanhMucCongTac}.DonVi " +
                        $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD} " +
                        $"LEFT JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                        $"ON {MyConstant.TBL_HopDong_PhuLuc}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.CodePl " +
                        $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                        $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code={MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan " +
                        $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} " +
                        $"ON {TDKH.TBL_DanhMucCongTac}.Code={TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                        $" WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.CodeCha='{item.Code}'";
                        Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    }
                    else
                    {
                        chitietThuCong = true;
                        dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL} WHERE \"CodeCha\"='{item.Code}'";
                        Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        Dt.Columns.Add("ThanhTien", typeof(double));
                        Dt.Columns["ThanhTien"].Expression = $"[DonGia]*[KhoiLuong]";
                        tbl = ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL;
                        goto Label1;
                    }
                }
                Label1:
                if (isQuyTrinh)
                {
                    dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} WHERE Code = '{item.Code}'";
                    Tbl_TCTU_DeXuatViewModel newDXVTHN = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_TCTU_DeXuatViewModel>(dbString).SingleOrDefault();
                    newDXVTHN.NgayThangYeuCau = date;
                    newDXVTHN.TrangThai = (int)TrangThaiDeXuatEnum.ChoDuyet;
                    ThuChiTamUngTongHopViewModel DataIn = new ThuChiTamUngTongHopViewModel();
                    DataIn.DeXuat = newDXVTHN;
                    ResultMessage<ThuChiTamUngTongHopViewModel> result;
                    if (item.NguonThuChi == 1)
                    {
                        List<Tbl_TCTU_KhoanChiChiTietViewModel> KhoanChiChiTiets = new List<Tbl_TCTU_KhoanChiChiTietViewModel>();
                        if (chitietThuCong)
                        {
                            List<PhuLucThuCong_ChiTiet> PLThuCong = DuAnHelper.ConvertToList<PhuLucThuCong_ChiTiet>(Dt);
                            foreach (PhuLucThuCong_ChiTiet itemChiTiet in PLThuCong)
                            {
                                KhoanChiChiTiets.Add(new Tbl_TCTU_KhoanChiChiTietViewModel
                                {
                                    Code = Guid.NewGuid().ToString(),
                                    Ten = itemChiTiet.TenCongViec,
                                    DonVi = itemChiTiet.DonVi,
                                    DonGia = itemChiTiet.DonGia,
                                    KhoiLuong = itemChiTiet.KhoiLuong,
                                });
                            }
                        }
                        else
                        {
                            List<PhuLucThuCong_ChiTiet> PL = DuAnHelper.ConvertToList<PhuLucThuCong_ChiTiet>(Dt);
                            foreach (PhuLucThuCong_ChiTiet itemChiTiet in PL)
                            {
                                KhoanChiChiTiets.Add(new Tbl_TCTU_KhoanChiChiTietViewModel
                                {
                                    Code = Guid.NewGuid().ToString(),
                                    Ten = itemChiTiet.TenCongViec,
                                    DonVi = itemChiTiet.DonVi,
                                    ThanhTienChiTiet = itemChiTiet.ThanhTien,
                                    KhoiLuong = itemChiTiet.KhoiLuong,
                                    ChiTietCoDonGia = false
                                });
                            }
                        }
                        result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<ThuChiTamUngTongHopViewModel>(RouteAPI.ApprovalKhoanChi_SendApprovalRequest, DataIn);
                    }
                    else
                        result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<ThuChiTamUngTongHopViewModel>(RouteAPI.ApprovalKhoanThu_SendApprovalRequest, DataIn);
                    
                    if (result.MESSAGE_TYPECODE)
                    {
                        countOK++;
                        if (result.Dto != null)
                        {
                            if (result.Dto.DeXuat != null)
                                DxModel.Add(result.Dto.DeXuat);
                            if (result.Dto.KhoanChi != null)
                                KCModel.Add(result.Dto.KhoanChi);
                            if (result.Dto.KhoanThu != null)
                                KTModel.Add(result.Dto.KhoanThu);
                        }
                    }
                    else
                    {
                        countBad++;
                        if (result.STATUS_CODE ==1)//Chưa có dự án
                            MessageShower.ShowInformation(result.MESSAGE_CONTENT);
                        Noti.Add($"{item.NoiDungUng}: {item.GiaTriDotNay}: {result.MESSAGE_CONTENT}");
                    }

                }
                else
                {
                    item.TrangThai = 2;
                    if (item.NguonThuChi == 1)
                    {
                        long GiaTriGiaiChi = 0;
                        string CodeKC = Guid.NewGuid().ToString();
                        string Code = item.Code;


                        Dictionary<string, object> dicVals = new Dictionary<string, object>()
                    {
                        { "GiaTriTamUngDaDuyet", item.GiaTriDotNay},
                        { "DateThucNhanUng", DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)},
                        { "DateXacNhanDaUng", DateTime.Now.AddDays(30).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)},
                        { "DateXacNhanDaChi", DateTime.Now.AddDays(30).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)},
                        { "CodeDeXuat", item.Code},
                        { "TrangThai", 2},
                        { "Code", CodeKC},
                        { "NoiDungUng", item.NoiDungUng},
                        { "CongTrinh", item.CongTrinh},
                        { "LoaiKinhPhi", item.LoaiKinhPhi},
                        { "NguoiLapTamUng", item.NguoiLapTamUng},
                    };
                        dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} ({string.Join(", ", dicVals.Keys)}) " +
                            $"VALUES ({string.Join(", ", dicVals.Keys.Select(x => $"@{x}"))})";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: dicVals.Values.ToArray());


                        KC.Add(new KhoanChi
                        {
                            ID = CodeKC,
                            ParentID = "0",
                            CodeDeXuat = item.Code,
                            NoiDungUng = item.NoiDungUng,
                            GiaTriTamUngDaDuyet = item.GiaTriDotNay,
                            TrangThai = item.TrangThai,
                            CongTrinh = item.CongTrinh,
                            ToChucCaNhanNhanChiPhiTamUng = item.ToChucCaNhanNhanChiPhiTamUng,
                            LoaiKinhPhi = item.LoaiKinhPhi,
                            NguoiLapTamUng = item.NguoiLapTamUng,
                            AddCP = "",
                            CheckDaChi = false,
                            CheckDaUng = false,
                            DateThucNhanUng = DateTime.Now.Date,
                            DateXacNhanDaChi = DateTime.Now.Date,
                            DateXacNhanDaUng = DateTime.Now.Date,
                            File = "Xem File"
                        });
                        if (chitietThuCong)
                        {
                            List<PhuLucThuCong_ChiTiet> PLThuCong = DuAnHelper.ConvertToList<PhuLucThuCong_ChiTiet>(Dt);
                            GiaTriGiaiChi = PLThuCong.Sum(x => x.ThanhTien);
                            foreach (PhuLucThuCong_ChiTiet itemChiTiet in PLThuCong)
                            {
                                dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} (\"CodeKC\",\"Code\",\"Ten\",\"DonVi\",\"KhoiLuong\",\"DonGia\") " +
                                    $"VALUES ('{CodeKC}','{Guid.NewGuid()}', @Ten, @DonVi,'{itemChiTiet.KhoiLuong}','{itemChiTiet.DonGia}')";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { itemChiTiet.TenCongViec, itemChiTiet.DonVi });
                            }
                            goto Label;
                        }
                        else
                        {
                            List<PhuLucThuCong_ChiTiet> PL = DuAnHelper.ConvertToList<PhuLucThuCong_ChiTiet>(Dt);
                            GiaTriGiaiChi = PL.Sum(x => x.ThanhTien);
                            foreach (PhuLucThuCong_ChiTiet itemChiTiet in PL)
                            {
                                dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} (\"CodeKC\",\"Code\",\"Ten\",\"DonVi\",\"KhoiLuong\",\"ThanhTienChiTiet\",\"ChiTietCoDonGia\") " +
                                    $"VALUES ('{CodeKC}','{Guid.NewGuid()}', @Ten, @DonVi,'{itemChiTiet.KhoiLuong}','{itemChiTiet.ThanhTien}','{false}')";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { itemChiTiet.TenCongViec, itemChiTiet.DonVi });
                            }
                            KC.Where(x => x.ID == CodeKC).SingleOrDefault().GiaTriGiaiChi = GiaTriGiaiChi;
                            dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"GiaTriGiaiChi\"='{GiaTriGiaiChi}' WHERE \"Code\"='{CodeKC}' ";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        }
                //        if (item.IsVanChuyen)
                //        {
                //            dbString = $"SELECT *" +
                //                $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCT} " +
                //                $" WHERE \"CodeCha\"='{Code}'";
                //            Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                //            tbl = ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCT;
                //        }
                //        else if (item.IsTDKH)
                //        {
                //            tbl = ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH;
                //            if (item.IsVatLieu)
                //                dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.Code,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayBD,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayKT,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.ThanhTien,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.KhoiLuong,{TDKH.TBL_KHVT_VatTu}.VatTu as TenCongViec,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaHieu,{TDKH.TBL_KHVT_VatTu}.DonVi " +
                //$"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH} " +
                //$"LEFT JOIN {TDKH.TBL_KHVT_VatTu} " +
                //$"ON {TDKH.TBL_KHVT_VatTu}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeVatTu " +
                //$" WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeCha='{Code}'";
                //            Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                //            else
                //                dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.Code,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.ThanhTien,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayBD,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.NgayKT,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.KhoiLuong,{TDKH.TBL_DanhMucCongTac}.TenCongTac as TenCongViec,{TDKH.TBL_DanhMucCongTac}.MaHieuCongTac as MaHieu,{TDKH.TBL_DanhMucCongTac}.DonVi " +
                //$"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH} " +
                //$"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                //$"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeCongTac " +
                //$"LEFT JOIN {TDKH.TBL_DanhMucCongTac} " +
                //$"ON {TDKH.TBL_DanhMucCongTac}.Code={TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                //$" WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTTDKH}.CodeCha='{Code}'";
                //            Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                //        }
                //        else if (item.CodeHd != null)
                //        {
                //            dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.Code,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.NgayBD,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.NgayKT,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.ThanhTien,{ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.KhoiLuong,{TDKH.TBL_DanhMucCongTac}.TenCongTac as TenCongViec,{TDKH.TBL_DanhMucCongTac}.MaHieuCongTac as MaHieu,{TDKH.TBL_DanhMucCongTac}.DonVi " +
                //            $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD} " +
                //            $"LEFT JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                //            $"ON {MyConstant.TBL_HopDong_PhuLuc}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.CodePl " +
                //            $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                //            $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code={MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan " +
                //            $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} " +
                //            $"ON {TDKH.TBL_DanhMucCongTac}.Code={TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                //            $" WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATCTHD}.CodeCha='{Code}'";
                //            Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                //        }
                //        else
                //        {
                //            dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL} WHERE \"CodeCha\"='{Code}'";
                //            Dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                //            Dt.Columns.Add("ThanhTien", typeof(double));
                //            Dt.Columns["ThanhTien"].Expression = $"[DonGia]*[KhoiLuong]";
                //            tbl = ThuChiTamUng.TBL_THUCHITAMUNG_DEXUATPL;
                //            List<PhuLucThuCong_ChiTiet> PLThuCong = DuAnHelper.ConvertToList<PhuLucThuCong_ChiTiet>(Dt);
                //            GiaTriGiaiChi = PLThuCong.Sum(x => x.ThanhTien);
                //            foreach (PhuLucThuCong_ChiTiet itemChiTiet in PLThuCong)
                //            {
                //                dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} (\"CodeKC\",\"Code\",\"Ten\",\"DonVi\",\"KhoiLuong\",\"DonGia\") " +
                //                    $"VALUES ('{CodeKC}','{Guid.NewGuid()}', @Ten, @DonVi,'{itemChiTiet.KhoiLuong}','{itemChiTiet.DonGia}')";
                //                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { itemChiTiet.TenCongViec, itemChiTiet.DonVi });
                //            }
                //            goto Label;
                //        }
                //        List<PhuLucThuCong_ChiTiet> PL = DuAnHelper.ConvertToList<PhuLucThuCong_ChiTiet>(Dt);
                //        GiaTriGiaiChi = PL.Sum(x => x.ThanhTien);
                //        foreach (PhuLucThuCong_ChiTiet itemChiTiet in PL)
                //        {
                //            dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} (\"CodeKC\",\"Code\",\"Ten\",\"DonVi\",\"KhoiLuong\",\"ThanhTienChiTiet\",\"ChiTietCoDonGia\") " +
                //                $"VALUES ('{CodeKC}','{Guid.NewGuid()}', @Ten, @DonVi,'{itemChiTiet.KhoiLuong}','{itemChiTiet.ThanhTien}','{false}')";
                //            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { itemChiTiet.TenCongViec, itemChiTiet.DonVi });
                //        }
                //        KC.Where(x => x.ID == CodeKC).SingleOrDefault().GiaTriGiaiChi = GiaTriGiaiChi;
                //        dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"GiaTriGiaiChi\"='{GiaTriGiaiChi}' WHERE \"Code\"='{CodeKC}' ";
                //        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                    else
                    {
                        string codeThu = Guid.NewGuid().ToString();

                        Dictionary<string, object> dicVals = new Dictionary<string, object>()
                    {
                        { "NgayThangThucHien", DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) },
                        { "TrangThai", 2},
                        { "IsNguonThu", true},
                        { "ToChucCaNhanNhanChiPhiTamUng", item.ToChucCaNhanNhanChiPhiTamUng},
                        { "Code", codeThu},
                        { "CodeDeXuat", item.Code},
                        { "TheoThucHien", item.GiaTriDotNay},
                        { "CongTrinh", item.CongTrinh},
                        { "NoiDungThu", item.NoiDungUng }
                    };
                        dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} ({string.Join(", ", dicVals.Keys)}) " +
                              $"VALUES ({string.Join(", ", dicVals.Keys.Select(x => $"@{x}"))})";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: dicVals.Values.ToArray());
                        KT.Add(new KhoanThu
                        {
                            ID = codeThu,
                            ParentID = item.CongTrinh,
                            CodeDeXuat = item.Code,
                            TrangThai = 2,
                            NoiDungThu = item.NoiDungUng,
                            TheoThucHien = item.GiaTriDotNay,
                            CongTrinh = item.CongTrinh,
                            CheckDaThu = false,
                            ToChucCaNhanNhanChiPhiTamUng = item.ToChucCaNhanNhanChiPhiTamUng,
                            NgayThangThucHien = DateTime.Now.Date
                        });
                    }
                    Label:
                    dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} SET \"TrangThai\" = '{2}',\"NgayThangYeuCau\"='{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' WHERE \"Code\" = '{item.Code}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
            WaitFormHelper.CloseWaitForm();
            if (!isQuyTrinh)
            {
                NewKC = KC;
                NewKT = KT;
                gc_ThuChiTamUng_DeXuat.RefreshDataSource();
                gc_ThuChiTamUng_DeXuat.Refresh();
                MessageShower.ShowInformation("Gửi duyệt thành công", "");
            }
            else
            {
                DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(DxModel.fcn_ObjToDataTable(), ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT, isCompareTime: false);
                DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(KCModel.fcn_ObjToDataTable(), ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI, isCompareTime: false);
                DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(KTModel.fcn_ObjToDataTable(), ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU, isCompareTime: false);
                DialogResult drs = DialogResult.None;
                if (countOK == DX.Count)
                    MessageShower.ShowInformation("Gửi duyệt thành công", "");
                else if (countOK == 0)
                    drs = MessageShower.ShowYesNoCancelQuestionWithCustomText("Gửi duyệt không thành công", "", yesString: "Xem chi tiết lỗi", Nostring: "Không xem chi tiết");
                else
                {
                    string mess = $@"Gửi duyệt thành công 1 phần: {countOK} thành công, {countBad} thất bại";
                    drs = MessageShower.ShowYesNoCancelQuestionWithCustomText("Gửi duyệt không thành công một phần", "", yesString: "Xem chi tiết lỗi", Nostring: "Không xem chi tiết");
                }
                if (drs == DialogResult.Yes)
                {
                    XtraFormThongBaoMutilError FrmThongBao = new XtraFormThongBaoMutilError();
                    FrmThongBao.Description = string.Join("\r\n\t", Noti.ToArray());
                    FrmThongBao.ShowDialog();
                }

            }
            if (SelectDuyet._CheckPhieuDuyet)
            {
                string PathSave = "";
                XtraFolderBrowserDialog Xtra = new XtraFolderBrowserDialog();
                if (Xtra.ShowDialog() == DialogResult.OK)
                {
                    PathSave = Xtra.SelectedPath;
                }
                else
                    return;
                WaitFormHelper.ShowWaitForm("Đang xuất phiếu duyệt");
                string m_Path = Path.Combine(BaseFrom.m_path, "Template", "FileExcel", "Phiếu đề xuất và chi phí kinh phí.xlsx");
                SpreadsheetControl Spread = new SpreadsheetControl();
                Spread.LoadDocument(m_Path);
                Worksheet ws = Spread.Document.Worksheets[0];
                dbString = $"SELECT DX.*,LKP.Ten AS TenKinhPhi FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} DX " +
                    $"LEFT JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_LOAIKINHPHI} LKP ON LKP.Code=DX.LoaiKinhPhi" +
                    $" WHERE DX.Code IN ({MyFunction.fcn_Array2listQueryCondition(LstCode.ToArray())})";
                DataTable dt_DX = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} WHERE \"Code\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                List<Tbl_ThongTinDuAnViewModel> lst = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThongTinDuAnViewModel>(dbString);
                Spread.BeginUpdate();
                ws.Rows[2]["D"].SetValueFromText($"Dự án: {lst.SingleOrDefault().TenDuAn}");
                ws.Rows[3]["D"].SetValueFromText($"Địa điểm: {lst.SingleOrDefault().DiaChi}");
                ws.Rows[4]["D"].SetValueFromText($"Người gửi: {BaseFrom.BanQuyenKeyInfo.FullName}");
                ws.Rows[5]["E"].SetValueFromText($"Ngày {date.Day} tháng {date.Month} năm {date.Year}");
                int i = 7,STT=1;
                foreach (DataRow row in dt_DX.Rows)
                {
                    Row Crow = ws.Rows[i++];
                    ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                    Crow["A"].SetValue(STT++);
                    Crow["D"].SetValueFromText(row["NoiDungUng"].ToString());
                    Crow["H"].SetValueFromText(row["TenKinhPhi"].ToString());
                    Crow["K"].SetValueFromText(row["GhiChu"].ToString());
                    Crow["E"].SetValue(row["GiaTriDotNay"]);

                }
                CellRange NguoiDx = ws.Range["NguoiDeXuat"];
                ws.Rows[NguoiDx.BottomRowIndex][NguoiDx.RightColumnIndex].SetValueFromText($"{BaseFrom.BanQuyenKeyInfo.FullName}");
                Spread.EndUpdate();
                Spread.Document.History.IsEnabled = true;
                string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
                Spread.SaveDocument(Path.Combine(PathSave, $"Phiếu đề xuất và chi phí kinh phí_{time}.xlsx"), DocumentFormat.Xlsx);
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowInformation("Xuất File thành công!");
                DialogResult dialogResult = XtraMessageBox.Show($"[Phiếu đề xuất và chi phí kinh phí_{time}.xlsx] thành công. Bạn có muốn mở file không???", "QUẢN LÝ THI CÔNG - THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Process.Start(Path.Combine(PathSave, $"Phiếu đề xuất và chi phí kinh phí_{time}.xlsx"));
                }
                WaitFormHelper.CloseWaitForm();

            }

        }

        private void sb_CapNhapTCTU_Click(object sender, EventArgs e)
        {

        }

        private void sb_DeXuatExport_Click(object sender, EventArgs e)
        {
          
        }

        private void RiBe_Xoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string Code = "", dbstring = "";
            DeXuat DX = gv_DeXuatThuChi.GetFocusedRow() as DeXuat;
            if (DX.TrangThai == 2)
            {
                MessageShower.ShowWarning("Nội dung đã gửi duyệt, Bạn không có quyền chỉnh sửa hay xóa, Nội dung sẽ được ẩn đi!");
                gv_DeXuatThuChi.DeleteSelectedRows();
                return;
            }
            if (e.Button.Caption == "Xóa")
            {
                DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa nội dung này không?");
                if (rs == DialogResult.Yes)
                {
                    Code = (string)gv_DeXuatThuChi.GetRowCellValue(gv_DeXuatThuChi.FocusedRowHandle, "Code");
                    dbstring = $"DELETE FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} WHERE \"Code\"='{Code}' ";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
                    gv_DeXuatThuChi.DeleteSelectedRows();
                }
            }
        }

        private void rEHL_FileDinhKem_Click(object sender, EventArgs e)
        {
            DeXuat KC = gv_DeXuatThuChi.GetFocusedRow() as DeXuat;
            FormLuaChon luachon = new FormLuaChon(KC.Code, FileManageTypeEnum.THUCHITAMUNG_DeXuat, KC.NoiDungUng);
            luachon.ShowDialog();
        }
    }
}
