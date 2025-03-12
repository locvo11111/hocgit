using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
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
    public partial class Ctrl_AnhTheNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        public string m_path, m_TempHinhAnh, m_TempLogo1, m_TempLogo2;

        private void gc_NhanVien_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            popupMenu1.ClearLinks();
            BaseView view = gc_NhanVien.GetViewAt(e.Location);
            DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo info = (DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo)view.CalcHitInfo(e.Location);
            if (info.InCard)
            {
                popupMenu1.AddItem(barButtonItem1).Caption ="Thay đổi Logo phía trên";
     
                popupMenu1.AddItem(barButtonItem2).Caption ="Thay đổi Logo phía dưới";

                popupMenu1.AddItem(barButtonItem3).Caption ="Thay đổi Logo phía trên toàn công ty";

                popupMenu1.AddItem(barButtonItem4).Caption ="Thay đổi Logo phía dưới toàn công ty";


            }
            popupMenu1.ShowPopup(gc_NhanVien.PointToScreen(e.Location));

        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DanhSachNhanVienModel NV = lv_NhanVien.GetFocusedRow() as DanhSachNhanVienModel;
            if (NV is null)
            {
                MessageShower.ShowError("Vui lòng chọn thẻ Nhân viên muốn thay đổi!!!!!!!!");
                return;
            }
            if (NV.Code != null)
            {
                openFileDialog.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
                //string tenFileCu = NV.LogoFirst;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName;
                    fileName = openFileDialog.FileName;

                    string tenhienthi = openFileDialog.SafeFileName;

                    var fi = new FileInfo(fileName);
                    string checksum = FileHelper.CalculateFileMD5(fileName);
                    if (fi.Length > MyConstant.MaxFileSize)
                    {
                        MessageShower.ShowError("Không thể thêm file vì vượt quá dung lượng cho phép (150MB)");
                        return;
                    }

                    DialogResult dialogResult = MessageShower.ShowYesNoQuestion($"Bạn có muốn thay thế File cũ bằng file {tenhienthi} không??", "Thay thế file");
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            if(NV.LogoFirst is null)
                            {
                                NV.LogoFirst = Guid.NewGuid().ToString();
                                File.Copy(fileName, $@"{m_path}\{NV.LogoFirst}");

                                string themfile = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} " +
                  $"(SortId,Code, FileDinhKem,CodeParent, Ngay, Checksum) VALUES " +
                  $"(1,'{NV.LogoFirst }', @FileDinhKem,'{NV.Code}', {DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)},'{checksum}')";

                                DataProvider.InstanceTHDA.ExecuteNonQuery(themfile, parameter: new object[] { tenhienthi });
                                gc_NhanVien.RefreshDataSource();
                                gc_NhanVien.Refresh();
                                return;
                            }
                            else
                            {
                                string OldCode = NV.LogoFirst;
                                File.Delete($@"{m_path}\{NV.LogoFirst}");
                                NV.LogoFirst = Guid.NewGuid().ToString();
                                File.Copy(fileName, $@"{m_path}\{NV.LogoFirst}");
                                gc_NhanVien.RefreshDataSource();
                                gc_NhanVien.Refresh();
                                string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} SET \"Code\"='{NV.LogoFirst}' WHERE \"Code\"='{OldCode}'";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                                return;
                            }

                        }
                        catch (Exception lỗi)
                        {
                            MessageShower.ShowError(lỗi.ToString());
                            return;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }


            }
        }      
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DanhSachNhanVienModel NV = lv_NhanVien.GetFocusedRow() as DanhSachNhanVienModel;
            if (NV is null)
            {
                MessageShower.ShowError("Vui lòng chọn thẻ Nhân viên muốn thay đổi!!!!!!!!");
                return;
            }
            if (NV.Code != null)
            {
                openFileDialog.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
                string tenFileCu = NV.LogoSecond;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName;
                    fileName = openFileDialog.FileName;

                    string tenhienthi = openFileDialog.SafeFileName;
                    string checksum = FileHelper.CalculateFileMD5(fileName);
                    var fi = new FileInfo(fileName);
                    if (fi.Length > MyConstant.MaxFileSize)
                    {
                        MessageShower.ShowError("Không thể thêm file vì vượt quá dung lượng cho phép (150MB)");
                        return;
                    }

                    DialogResult dialogResult = MessageShower.ShowYesNoQuestion($"Bạn có muốn thay thế {tenFileCu} bằng file {tenhienthi} không??", "Thay thế file");
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            if (NV.LogoSecond is null)
                            {
                                NV.LogoSecond = Guid.NewGuid().ToString();
                                File.Copy(fileName, $@"{m_path}\{NV.LogoSecond}");

                                string themfile = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} " +
                  $"(SortId,Code, FileDinhKem,CodeParent, Ngay, Checksum) VALUES " +
                  $"(2,'{NV.LogoSecond }', @FileDinhKem,'{NV.Code}', {DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)},'{checksum}')";

                                DataProvider.InstanceTHDA.ExecuteNonQuery(themfile, parameter: new object[] { tenhienthi });
                                gc_NhanVien.RefreshDataSource();
                                gc_NhanVien.Refresh();
                                return;
                            }
                            else
                            {
                                string OldCode = NV.LogoSecond;
                                File.Delete($@"{m_path}\{NV.LogoSecond}");
                                NV.LogoSecond = Guid.NewGuid().ToString();
                                File.Copy(fileName, $@"{m_path}\{NV.LogoSecond}");
                                gc_NhanVien.RefreshDataSource();
                                gc_NhanVien.Refresh();
                                string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} SET \"Code\"='{NV.LogoSecond}' WHERE \"Code\"='{OldCode}'";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                                return;
                            }

                        }
                        catch (Exception lỗi)
                        {
                            MessageShower.ShowError(lỗi.ToString());
                            return;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }


            }
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = openFileDialog.FileName;

                string tenhienthi = openFileDialog.SafeFileName;
                string checksum = FileHelper.CalculateFileMD5(fileName);
                var fi = new FileInfo(fileName);
                if (fi.Length > MyConstant.MaxFileSize)
                {
                    MessageShower.ShowError("Không thể thêm file vì vượt quá dung lượng cho phép (150MB)");
                    return;
                }

                DialogResult dialogResult = MessageShower.ShowYesNoQuestion($"Bạn có muốn thay thế Logo phía trên toàn công ty??", "Thay thế file");
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        List<DanhSachNhanVienModel> DSNV = gc_NhanVien.DataSource as List<DanhSachNhanVienModel>;

                        foreach(var NV in DSNV)
                        {
                            if (NV.LogoFirst is null)
                            {
                                NV.LogoFirst = Guid.NewGuid().ToString();
                                File.Copy(fileName, $@"{m_path}\{NV.LogoFirst}");

                                string themfile = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} " +
                  $"(SortId,Code, FileDinhKem,CodeParent, Ngay, Checksum) VALUES " +
                  $"(1,'{NV.LogoFirst }', @FileDinhKem,'{NV.Code}', {DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)},'{checksum}')";

                                DataProvider.InstanceTHDA.ExecuteNonQuery(themfile, parameter: new object[] { tenhienthi });
                                gc_NhanVien.RefreshDataSource();
                                gc_NhanVien.Refresh();
                                return;
                            }
                            else
                            {
                                string OldCode = NV.LogoFirst;
                                File.Delete($@"{m_path}\{NV.LogoFirst}");
                                NV.LogoFirst = Guid.NewGuid().ToString();
                                File.Copy(fileName, $@"{m_path}\{NV.LogoFirst}");
                                gc_NhanVien.RefreshDataSource();
                                gc_NhanVien.Refresh();
                                string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} SET \"Code\"='{NV.LogoFirst}' WHERE \"Code\"='{OldCode}'";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                                return;
                            }
                            //File.Delete($@"{m_path}\{NV.Code}\{NV.LogoFirst}");
                            //File.Copy(fileName, $@"{m_path}\{NV.Code}\{Path.GetFileName(fileName)}");
                            //NV.LogoFirst = tenhienthi;
                            //NV.FilePathLoGo1 = $@"{m_path}\{NV.Code}\{tenhienthi}";
                            ////NV.PhoToInit();
                            //string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} SET \"LogoFirst\"='{tenhienthi}' WHERE \"Code\"='{NV.Code}'";
                            //DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                        }
                        gc_NhanVien.RefreshDataSource();
                        gc_NhanVien.Refresh();
                        return;

                    }
                    catch (Exception lỗi)
                    {
                        MessageShower.ShowError(lỗi.ToString());
                        return;
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

        }
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = openFileDialog.FileName;

                string tenhienthi = openFileDialog.SafeFileName;
                string checksum = FileHelper.CalculateFileMD5(fileName);
                var fi = new FileInfo(fileName);
                if (fi.Length > MyConstant.MaxFileSize)
                {
                    MessageShower.ShowError("Không thể thêm file vì vượt quá dung lượng cho phép (150MB)");
                    return;
                }

                DialogResult dialogResult = MessageShower.ShowYesNoQuestion($"Bạn có muốn thay thế Logo phía dưới toàn công ty??", "Thay thế file");
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        List<DanhSachNhanVienModel> DSNV = gc_NhanVien.DataSource as List<DanhSachNhanVienModel>;

                        foreach(var NV in DSNV)
                        {
                            if (NV.LogoSecond is null)
                            {
                                NV.LogoSecond = Guid.NewGuid().ToString();
                                File.Copy(fileName, $@"{m_path}\{NV.LogoSecond}");

                                string themfile = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} " +
                  $"(SortId,Code, FileDinhKem,CodeParent, Ngay, Checksum) VALUES " +
                  $"(2,'{NV.LogoSecond }', @FileDinhKem,'{NV.Code}', {DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)},'{checksum}')";

                                DataProvider.InstanceTHDA.ExecuteNonQuery(themfile, parameter: new object[] { tenhienthi });
                                gc_NhanVien.RefreshDataSource();
                                gc_NhanVien.Refresh();
                                return;
                            }
                            else
                            {
                                string OldCode = NV.LogoSecond;
                                File.Delete($@"{m_path}\{NV.LogoSecond}");
                                NV.LogoSecond = Guid.NewGuid().ToString();
                                File.Copy(fileName, $@"{m_path}\{NV.LogoSecond}");
                                gc_NhanVien.RefreshDataSource();
                                gc_NhanVien.Refresh();
                                string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} SET \"Code\"='{NV.LogoSecond}' WHERE \"Code\"='{OldCode}'";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                                return;
                            }
                            //File.Delete($@"{m_path}\{NV.Code}\{NV.LogoFirst}");
                            //File.Copy(fileName, $@"{m_path}\{NV.Code}\{Path.GetFileName(fileName)}");
                            //NV.LogoSecond = tenhienthi;
                            //NV.FilePathLoGo2 = $@"{m_path}\{NV.Code}\{tenhienthi}";
                            ////NV.PhoToInit();
                            //string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} SET \"LogoSecond\"='{tenhienthi}' WHERE \"Code\"='{NV.Code}'";
                            //DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                        }
                        gc_NhanVien.RefreshDataSource();
                        gc_NhanVien.Refresh();
                        return;

                    }
                    catch (Exception lỗi)
                    {
                        MessageShower.ShowError(lỗi.ToString());
                        return;
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

        }

        private void sb_Export_Click(object sender, EventArgs e)
        {
            gc_NhanVien.ShowRibbonPrintPreview();
        }

        private void repositoryItemPictureEdit1_ImageLoading(object sender, SaveLoadImageEventArgs e)
        {
            int loc = 1;
        }

        private void lv_NhanVien_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column == bandedGridColumn5 && e.IsGetData)
            {
                lv_NhanVien.BeginUpdate();
                LayoutView view = sender as LayoutView;
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
                lv_NhanVien.EndUpdate();
            }
            if (e.Column == col_PhoTo1 && e.IsGetData)
            {
                lv_NhanVien.BeginUpdate();
                LayoutView view = sender as LayoutView;
                string fileCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_LogoFirst) as string ?? string.Empty;
                //object objCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_Code);
                if (!string.IsNullOrEmpty(fileCode))
                {
                    string filePath = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM, fileCode));
                    e.Value = FileHelper.fcn_ImageStreamDoc(filePath) ?? Properties.Resources.Logo1;
                }
                else
                    e.Value = Properties.Resources.Logo1;
                //if (fileCode != null && !string.IsNullOrEmpty(fileCode.ToString()))
                //{

         
                //}
                lv_NhanVien.EndUpdate();
            }    
            if (e.Column == col_PhoTo2 && e.IsGetData)
            {
                lv_NhanVien.BeginUpdate();
                LayoutView view = sender as LayoutView;
                string fileCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_LogoSecond) as string ?? string.Empty;
                //object objCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_Code);
                if (!string.IsNullOrEmpty(fileCode))
                {
                    string filePath = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM, fileCode));
                    e.Value = FileHelper.fcn_ImageStreamDoc(filePath) ?? Properties.Resources.Logo2;
                }
                else
                    e.Value = Properties.Resources.Logo2;
                //if (fileCode != null && !string.IsNullOrEmpty(fileCode.ToString()))
                //{


                //}
                lv_NhanVien.EndUpdate();
            }
        }

        public Ctrl_AnhTheNhanVien()
        {
            InitializeComponent();
            barButtonItem1.ItemClick += barButtonItem1_ItemClick;
            barButtonItem2.ItemClick += barButtonItem2_ItemClick;
            barButtonItem3.ItemClick += barButtonItem3_ItemClick;
            barButtonItem4.ItemClick += barButtonItem4_ItemClick;
        }
        public void Fcn_Update()
        {
            WaitFormHelper.ShowWaitForm("Đang tạo ẢNH THẺ cho toàn công ty!!!!!!!", "Vui Lòng chờ!");
            lv_NhanVien.CustomUnboundColumnData -= lv_NhanVien_CustomUnboundColumnData;
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}";
            List<ViTriNhanVien_PhongBan> ViTri = DataProvider.InstanceTHDA.ExecuteQueryModel<ViTriNhanVien_PhongBan>(dbString);
            rILUE_ViTri.DataSource = ViTri;
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_TENPHONGBAN}";
            List<ViTriNhanVien_PhongBan> PhongBan = DataProvider.InstanceTHDA.ExecuteQueryModel<ViTriNhanVien_PhongBan>(dbString);
            rILUE_PhongBan.DataSource = PhongBan;
            m_path = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM}");
            //m_TempHinhAnh = $@"{BaseFrom.m_templatePath}\FileHinhAnh\QLTC.jpg";
            if (!Directory.Exists(m_path))
                Directory.CreateDirectory(m_path);
            dbString = $"SELECT NC.*,DINHKEM.Code as UrlImage,DINHKEM1.Code as LogoFirst,DINHKEM2.Code as LogoSecond FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NC " +
                $" LEFT JOIN { DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} DINHKEM ON DINHKEM.CodeParent=NC.Code AND DINHKEM.State=1" +
                $" LEFT JOIN { DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} DINHKEM1 ON DINHKEM1.CodeParent=NC.Code AND DINHKEM1.SortId=1" +
                $" LEFT JOIN { DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} DINHKEM2 ON DINHKEM2.CodeParent=NC.Code AND DINHKEM2.SortId=2 ";
            List<DanhSachNhanVienModel> DanhSachNV = DataProvider.InstanceTHDA.ExecuteQueryModel<DanhSachNhanVienModel>(dbString);
            DanhSachNV.ForEach(x => {x.DuAn = $"DỰ ÁN: {SharedControls.slke_ThongTinDuAn.Text}";
                x.MaNhanVien = $"MNV: {x.MaNhanVien}";
            });        
            //DanhSachNV.ForEach(x => { x.FilePath = $@"{m_path}\{x.Code}"; x.FilePathLoGo1 = $@"{m_path}\{x.Code}\{x.LogoFirst}";
            //    x.FilePathLoGo2 = $@"{m_path}\{x.Code}\{x.LogoSecond}"; x.PhoToInit(); x.DuAn = $"DỰ ÁN: {SharedControls.slke_ThongTinDuAn.Text}";
            //    x.MaNhanVien = $"MNV: {x.MaNhanVien}";
            //});
            lv_NhanVien.CustomUnboundColumnData += lv_NhanVien_CustomUnboundColumnData;
            //DanhSachNV.ForEach(x => x.PhoToInit());
            if (DanhSachNV.Count() == 0)
            {
                DanhSachNV.Add(new DanhSachNhanVienModel
                {
                });
            }
            gc_NhanVien.DataSource = DanhSachNV;
            gc_NhanVien.Refresh();
            WaitFormHelper.CloseWaitForm();
            //CreatePictureEdit(gc_NhanVien, "Image");
        }
        static RepositoryItemPictureEdit CreatePictureEdit(GridControl grid, string name)
        {
            RepositoryItemPictureEdit ret = new RepositoryItemPictureEdit();
            ret.PictureInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            ret.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            ret.Name = name;
            grid.RepositoryItems.Add(ret);
            return ret;
        }
    }
}
