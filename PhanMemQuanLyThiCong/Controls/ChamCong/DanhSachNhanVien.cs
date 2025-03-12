using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.ChamCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.ChamCong
{
    public partial class DanhSachNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        public string m_path, m_TempHinhAnh,m_TempLogo1, m_TempLogo2;
        public DanhSachNhanVien()
        {
            InitializeComponent();
        }
        public bool IsNhanVien { get; set; }
        public void Fcn_Update(bool IsNhanVienCty=true)
        {
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}";
            string Condition = IsNhanVienCty ? "OR NC.IsNhanVienCty=1" : "OR NC.IsNhanVienCty=0";
            List <ViTriNhanVien_PhongBan> ViTri = DataProvider.InstanceTHDA.ExecuteQueryModel<ViTriNhanVien_PhongBan>(dbString);
            ViTri.Add(new ViTriNhanVien_PhongBan
            {
                Code = "Add",
                Ten = "Thêm vị trí mới"
            });

            rILUE_ViTri.DataSource = ViTri;
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_TENPHONGBAN}";
            List<ViTriNhanVien_PhongBan> PhongBan = DataProvider.InstanceTHDA.ExecuteQueryModel<ViTriNhanVien_PhongBan>(dbString);
            PhongBan.Add(new ViTriNhanVien_PhongBan
            {
                Code = "Add",
                Ten = "Thêm phòng ban"
            });
            rILUE_PhongBan.DataSource = PhongBan;
            IsNhanVien = IsNhanVienCty;
            m_path = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}");
            //m_TempHinhAnh= $@"{BaseFrom.m_templatePath}\FileHinhAnh\QLTC.jpg";
            //m_TempLogo1= $@"{BaseFrom.m_templatePath}\FileHinhAnh\Logo1.jpg";
            //m_TempLogo2= $@"{BaseFrom.m_templatePath}\FileHinhAnh\Logo2.jpg";   
            //if (!Directory.Exists(m_path))
            //    Directory.CreateDirectory(m_path);
            dbString = $"SELECT NC.*,DINHKEM.Code as UrlImage FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NC " +
                $"LEFT JOIN { DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} DINHKEM ON DINHKEM.CodeParent=NC.Code AND DINHKEM.State=1 " +
                $"WHERE NC.IsNhanVienCty='{IsNhanVienCty}' {Condition} ";
            List<DanhSachNhanVienModel> DanhSachNV = DataProvider.InstanceTHDA.ExecuteQueryModel<DanhSachNhanVienModel>(dbString);
            //DanhSachNV.ForEach(x => { x.FilePath = $@"{m_path}\{x.Code}\{x.HinhAnh}"; x.PhoToInit(); });
            if (DanhSachNV.Count()==0)
            {
                DanhSachNV.Add(new DanhSachNhanVienModel
                {
                });
            }
            gc_NhanVien.DataSource = DanhSachNV;
            gc_NhanVien.Refresh();
            gc_NhanVien.RefreshDataSource();
            gv_NhanVien.RefreshData();
            //CreatePictureEdit(gc_NhanVien, "Image");
        }
        static RepositoryItemPictureEdit CreatePictureEdit(GridControl grid, string name)
        {
            RepositoryItemPictureEdit ret = new RepositoryItemPictureEdit();
            grid.RepositoryItems.Add(ret);
            ret.PictureInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            ret.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            ret.Name = name;
            return ret;
        }
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            DanhSachNhanVienModel NV = gv_NhanVien.GetFocusedRow() as DanhSachNhanVienModel;
            if (NV.Code is null)
                return;
            FormLuaChon luachon = new FormLuaChon(NV.Code, FileManageTypeEnum.ChamCong_NhanVien, NV.TenNhanVien);
            luachon.ShowDialog();
            Fcn_Update(IsNhanVien);
            //    goto Label;
            //else
            //{
            //    if (NV.PhoTo!=null)
            //    {   
            //        openFileDialog.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
            //        string tenFileCu = NV.HinhAnh;
            //        if (openFileDialog.ShowDialog() == DialogResult.OK)
            //        {
            //            string fileName;
            //            fileName = openFileDialog.FileName;

            //            string tenhienthi = openFileDialog.SafeFileName;

            //            var fi = new FileInfo(fileName);
            //            if (fi.Length > MyConstant.MaxFileSize)
            //            {
            //                MessageShower.ShowError("Không thể thêm file vì vượt quá dung lượng cho phép (150MB)");
            //                return;
            //            }

            //            DialogResult dialogResult = MessageShower.ShowYesNoQuestion($"Bạn có muốn thay thế {tenFileCu} bằng file {tenhienthi} không??", "Thay thế file");
            //            if (dialogResult == DialogResult.Yes)
            //            {
            //                try
            //                {

            //                    File.Delete($@"{m_path}\{NV.Code}\{tenFileCu}");
            //                    File.Copy(fileName, $@"{m_path}\{NV.Code}\{Path.GetFileName(fileName)}",true);
            //                    gc_NhanVien.RefreshDataSource();
            //                    gc_NhanVien.Refresh();
            //                    NV.HinhAnh = tenhienthi;
            //                    NV.FilePath = $@"{m_path}\{NV.Code}\{tenhienthi}";
            //                    NV.PhoToInit();
            //                    gc_NhanVien.RefreshDataSource();
            //                    gc_NhanVien.Refresh();
            //                    string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} SET \"HinhAnh\"='{tenhienthi}' WHERE \"Code\"='{NV.Code}'";
            //                    DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
            //                    return;

            //                }
            //                catch (Exception lỗi)
            //                {
            //                    MessageShower.ShowError(lỗi.ToString());
            //                    return;
            //                }
            //            }
            //            else if (dialogResult == DialogResult.No)
            //            {
            //                return;
            //            }
            //        }
            //        else return;

            //    }
            //    else
            //        goto Label;

            //}
            //Label:
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string hienthitenfile = openFileDialog.SafeFileName;
            //    string fileName = openFileDialog.FileName;
            //    if (Path.GetDirectoryName(fileName).Length > 248)
            //    {
            //        MessageShower.ShowInformation($"Thư mục \"{Path.GetDirectoryName(fileName)}\" quá dài, vui lòng đổi tên file ngắn hơn! Các file hợp lệ sẽ vẫn được thêm vào dự án");
            //    }

            //    if (fileName.Length > 260)
            //    {
            //        MessageShower.ShowInformation($"Đường dẫn đến file {fileName} quá dài! Vui lòng copy ra thư mục bên ngoài! Các file hợp lệ vẫn được thêm vào");

            //    }
            //    FileInfo fi = new FileInfo(fileName);
            //    if (fi.Length > MyConstant.MaxFileSize)
            //    {
            //        MessageShower.ShowError("Không thể thêm file vì vượt quá dung lượng cho phép (150MB)");
            //        return;
            //    }
            //    try
            //    {
            //        string newGUID = Guid.NewGuid().ToString();
            //        if (!Directory.Exists($@"{m_path}\{newGUID}"))
            //            Directory.CreateDirectory($@"{m_path}\{newGUID}");
            //        File.Copy(fileName, $@"{m_path}\{newGUID}\{Path.GetFileName(fileName)}");

            //        string themfile = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} " +
            //        $"(Code, HinhAnh,IsNhanVienCty,ChucVu,PhongBan,LogoFirst,LogoSecond) VALUES " +
            //        $"('{newGUID}','{hienthitenfile}','{IsNhanVien}','{NV.ChucVu}','{NV.PhongBan}','Logo1.jpg','Logo2.jpg')";

            //        DataProvider.InstanceTHDA.ExecuteNonQuery(themfile);
            //        NV.Code = newGUID;
            //        NV.HinhAnh = hienthitenfile;
            //        NV.FilePath = $@"{m_path}\{newGUID}\{hienthitenfile}";
            //        NV.PhoToInit();
            //        gc_NhanVien.RefreshDataSource();
            //        gc_NhanVien.Refresh();
            //    //    string db_string = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN_CHITIETPHUPHI} " +
            //    //$"(Code,CodeNhanVien,CodeDuAn,NgayTinhLuong,LuongCoBan) VALUES " +
            //    //$"('{Guid.NewGuid()}','{newGUID}','{SharedControls.slke_ThongTinDuAn.EditValue}','{22}','{12000000}')";
            //    //    DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageShower.ShowInformation($"{ex.Message}__Inner: {ex.InnerException?.Message}");
            //    }
            //}
        }

        private void gv_NhanVien_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (e.MenuType == GridMenuType.Row)
            {
                DXMenuItem menuItem = new DXMenuItem("Thêm nhân viên", this.fcn_Handle_Popup_ChenDong);
                menuItem.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem);

                DXMenuItem Xoa = new DXMenuItem("Xóa nhân viên", this.fcn_Handle_Popup_Xoa);
                Xoa.Tag = hitInfo.Column;
                e.Menu.Items.Add(Xoa);

            }
        }
        private void fcn_Handle_Popup_Xoa(object sender, EventArgs e)
        {
            DanhSachNhanVienModel NV = gv_NhanVien.GetFocusedRow() as DanhSachNhanVienModel;
            if (NV.Code != null)
            {
                DuAnHelper.DeleteDataRows(DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN, new string[] { NV.Code });
                //string db_string = $"DELETE FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} WHERE \"Code\"='{NV.Code}'";
                //DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            }
            gv_NhanVien.DeleteSelectedRows();
            if (gv_NhanVien.RowCount == 0)
            {
                List<DanhSachNhanVienModel> DanhSachNV = new List<DanhSachNhanVienModel>();
                DanhSachNV.Add(new DanhSachNhanVienModel
                {

                });
                gc_NhanVien.DataSource = DanhSachNV;
                gc_NhanVien.RefreshDataSource();
            }

        }
        private void fcn_Handle_Popup_ChenDong(object sender, EventArgs e)
        {
            gv_NhanVien.CloseEditor();
            (gc_NhanVien.DataSource as List<DanhSachNhanVienModel>).Add(new DanhSachNhanVienModel
            {
            });
            gc_NhanVien.RefreshDataSource();
        }

        private void gc_NhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                gv_NhanVien.CloseEditor();
                e.SuppressKeyPress = true;
                (gc_NhanVien.DataSource as List<DanhSachNhanVienModel>).Add(new DanhSachNhanVienModel
                {
                });
                gc_NhanVien.RefreshDataSource();
            }
        }

        private void gv_NhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                gv_NhanVien.CloseEditor();
                e.SuppressKeyPress = true;
                (gc_NhanVien.DataSource as List<DanhSachNhanVienModel>).Add(new DanhSachNhanVienModel
                {
                });
                gc_NhanVien.RefreshDataSource();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                gv_NhanVien.CloseEditor();
                e.SuppressKeyPress = true;
                gv_NhanVien.FocusedRowHandle = gv_NhanVien.FocusedRowHandle + 1;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DanhSachNhanVienModel NV = gv_NhanVien.GetFocusedRow() as DanhSachNhanVienModel;
                if (NV.Code != null)
                {
                    string db_string = $"DELETE FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} WHERE \"Code\"='{NV.Code}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
                }
                gv_NhanVien.DeleteSelectedRows();
                if (gv_NhanVien.RowCount == 0)
                {
                    List<DanhSachNhanVienModel> DanhSachNV=new List<DanhSachNhanVienModel>();
                    DanhSachNV.Add(new DanhSachNhanVienModel
                    {

                    });
                    gc_NhanVien.DataSource = DanhSachNV;
                    gc_NhanVien.RefreshDataSource();
                }
            }
        }

        private void rILUE_PhongBan_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit editor = (sender as LookUpEdit);
            string Code = editor.EditValue.ToString();
            if (Code == "Add")
            {
                string PhongBan = XtraInputBox.Show("Tên phòng ban mới", "Nhập tên phòng ban mới", "");
                if (PhongBan != null)
                {
                    Code = Guid.NewGuid().ToString();

                    List<ViTriNhanVien_PhongBan> KP = editor.Properties.DataSource as List<ViTriNhanVien_PhongBan>;
                    KP.Add(new ViTriNhanVien_PhongBan
                    {
                        Code = Code,
                        Ten = PhongBan
                    });
                    string db_string = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_TENPHONGBAN} " +
$"(Code,Ten) VALUES " +
$"('{Code}','{PhongBan}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
                    KP.RemoveAll(x => x.Code == "Add");
                    KP.Add(new ViTriNhanVien_PhongBan
                    {
                        Code = "Add",
                        Ten = "Thêm phòng ban"
                    });

                    editor.Properties.DataSource = KP;
                    editor.EditValue = Code;

                }


            }
        }

        private async void sb_downLoad_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu Nhân viên thi công từ Server");
            var result = await CusHttpClient.InstanceCustomer.MGetAsync<SQLiteAllDataViewModel1>(RouteAPI.TongDuAn_GetNhanCongs);

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

            Fcn_Update(IsNhanVien);

        }


        private void gv_NhanVien_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column== bandedGridColumn5 && e.IsGetData)
            {
                GridView view = sender as GridView;
                string fileCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_UrlImage) as string ?? string.Empty;
                object objCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_Code);
                if (objCode != null && !string.IsNullOrEmpty(objCode.ToString()))
                {

                    if (!string.IsNullOrEmpty(fileCode))
                    {
                        string filePath = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM, objCode.ToString()), fileCode);
                        e.Value = FileHelper.fcn_ImageStreamDoc(filePath) ?? Properties.Resources.QLTC;
                    }
                    else
                        e.Value = Properties.Resources.QLTC;
                }

            }
        }

        private void sb_AddNew_Click(object sender, EventArgs e)
        {
            gv_NhanVien.CloseEditor();
            (gc_NhanVien.DataSource as List<DanhSachNhanVienModel>).Add(new DanhSachNhanVienModel
            {
            });
            gc_NhanVien.RefreshDataSource();
        }

        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            DanhSachNhanVienModel NV = gv_NhanVien.GetFocusedRow() as DanhSachNhanVienModel;
            if (NV.Code is null)
                return;
            FormLuaChon luachon = new FormLuaChon(NV.Code, FileManageTypeEnum.ChamCong_NhanVien, NV.TenNhanVien);
            luachon.ShowDialog();
        }

        private void rILUE_ViTri_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit editor = (sender as LookUpEdit);
            string Code = editor.EditValue.ToString();
            if (Code == "Add")
            {
                string Vitri = XtraInputBox.Show("Tên vị trí nhân viên", "Nhập tên vị trí nhân viên", "");
                if (Vitri != null)
                {
                   Code = Guid.NewGuid().ToString();

                    List<ViTriNhanVien_PhongBan> KP = editor.Properties.DataSource as List<ViTriNhanVien_PhongBan>;
                    KP.Add(new ViTriNhanVien_PhongBan
                    {
                        Code = Code,
                        Ten = Vitri
                    });
                    string db_string=$"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} " +
               $"(Code,Ten) VALUES " +
               $"('{Code}','{Vitri}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
                    KP.RemoveAll(x => x.Code == "Add");
                    KP.Add(new ViTriNhanVien_PhongBan
                    {
                        Code = "Add",
                        Ten = "Thêm vị trí mới"
                    });

                    editor.Properties.DataSource = KP;
                    editor.EditValue = Code;

                }


            }
        }

        private void gv_NhanVien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DanhSachNhanVienModel NV = gv_NhanVien.GetFocusedRow() as DanhSachNhanVienModel;
            string db_string = "";
            string colum = e.Column.FieldName;
            string Code = Guid.NewGuid().ToString();
            string Value = colum == "NgayKyHopDong" || colum == "NgaySinh" ? ((DateTime)e.Value).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : e.Value.ToString();
            if (NV.Code is null)
            {
                db_string =colum=="TenNhanVien"? $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} " +
                $"(Code, {colum},IsNhanVienCty,TenNhanVienKhongDau,HinhAnh,ChucVu,PhongBan) VALUES " +
                $"('{Code}','{Value}','{IsNhanVien}','{MyFunction.fcn_RemoveAccents(Value)}','QLTC.jpg','{NV.ChucVu}','{NV.PhongBan}')" :
                colum=="ChucVu"||colum=="PhongBan" ? $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} " +
                $"(Code, {colum},IsNhanVienCty,HinhAnh) VALUES " +
                $"('{Code}','{Value}','{IsNhanVien}','QLTC.jpg')" :
                    $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} " +
                $"(Code, {colum},IsNhanVienCty,HinhAnh,ChucVu,PhongBan) VALUES " +
                $"('{Code}','{Value}','{IsNhanVien}','QLTC.jpg','{NV.ChucVu}','{NV.PhongBan}')";
                NV.Code = Code;
                //if (!Directory.Exists($@"{m_path}\{NV.Code}"))
                //    Directory.CreateDirectory($@"{m_path}\{NV.Code}");
                //File.Copy(m_TempHinhAnh, $@"{m_path}\{NV.Code}\{Path.GetFileName(m_TempHinhAnh)}");
                //File.Copy(m_TempLogo1, $@"{m_path}\{NV.Code}\{Path.GetFileName(m_TempLogo1)}");
                //File.Copy(m_TempLogo2, $@"{m_path}\{NV.Code}\{Path.GetFileName(m_TempLogo2)}");
                //NV.HinhAnh = "QLTC.jpg";
                //NV.FilePath = $@"{m_path}\{NV.Code}\{Path.GetFileName(m_TempHinhAnh)}";
                //NV.PhoToInit();
                gc_NhanVien.RefreshDataSource();
                gc_NhanVien.Refresh();
                DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
                //db_string = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN_CHITIETPHUPHI} " +
                //$"(Code,CodeNhanVien,CodeDuAn,NgayTinhLuong,LuongCoBan) VALUES " +
                //$"('{Guid.NewGuid()}','{Code}','{SharedControls.slke_ThongTinDuAn.EditValue}','{22}','{12000000}')";
                //DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            }
            else
            {
                db_string = colum == "TenNhanVien" ? $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} SET" +
                    $" '{colum}'='{Value}',\"TenNhanVienKhongDau\"='{MyFunction.fcn_RemoveAccents(Value)}' WHERE \"Code\"='{NV.Code}'":
                    $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} SET '{colum}'='{Value}' WHERE \"Code\"='{NV.Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            }
        }
    }
}
