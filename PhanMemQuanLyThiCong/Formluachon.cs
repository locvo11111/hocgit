using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit;
using DevExpress.XtraPdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using MWORD = DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong;
using DevExpress.XtraSpreadsheet;
using System.Threading.Tasks;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.ChatBox;
using Dapper;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.Xpo.DB.Helpers;
using StackExchange.Profiling.Internal;

namespace PhanMemQuanLyThiCong
{
    public partial class FormLuaChon : Form
    {
        OpenFileDialog dlg = new OpenFileDialog();
        //string m_path;
        string m_path;
        //string m_tenHoSo;
        string m_TenThuMucLonChuaFile;
        string m_tblDbSaveFile;
        string m_CodeDanhMucCha;
        string m_colCodeDanhMucCha;
        //DataProvider m_db = new DataProvider("");
        Color ColorDaChon = Color.Green;
        Color ColorChuaChon = Color.Black;
        private FileManageTypeEnum _type;
        DateTime? _date;
        //OpenFileDialog dlg = new OpenFileDialog();


        const string ChiXem = "ChiXem";
        const string CanDuyet = "CanDuyet";

        //static string FileVatLy_ViewOnly = $"{nameof(FileLinkEnum.FileVatLy)}_{ChiXem}";
        //static string FileVatLy_NeedApprove = $"{nameof(FileLinkEnum.FileVatLy)}_{CanDuyet}";

        //static string FileLink_ViewOnly = $"{nameof(FileLinkEnum.FileLink)}_{ChiXem}";
        //static string FileLink_NeedApprove = $"{nameof(FileLinkEnum.FileLink)}_{CanDuyet}";

        private void fcn_init()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory; // Nhận đường dẫn
        }
        public FormLuaChon(string codeDanhMucCha, FileManageTypeEnum type, string TenCT = null, DateTime? date = null)
        {
            InitializeComponent();
            fcn_init();
            lb_Ten.Text = TenCT;
            dtp.Visible = date.HasValue;

            
            m_CodeDanhMucCha = codeDanhMucCha;
            _type = type;
            _date = date;
            m_colCodeDanhMucCha = "CodeParent";
            col_CanDuyet.Visible = false;
            switch (type)
            {
                case FileManageTypeEnum.CONGVANDIDEN:
                    m_tblDbSaveFile = MyConstant.QL_CVDD_Filedinhkem;
                    //m_TenThuMucLonChuaFile = "CongVanDiDen";
                    //m_colCodeDanhMucCha = "CodeHoSo";
                    break;
                case FileManageTypeEnum.ThongTinDuAn:
                    m_tblDbSaveFile = MyConstant.TBL_THONGTINDUAN_FileDinhKem;
                    //col_GhiChu.Caption = "Ảnh đại điện";
                    col_GhiChu.Visible = true;
                    col_GhiChu.VisibleIndex = 2;
                    //m_TenThuMucLonChuaFile = "CongVanDiDen";
                    //m_colCodeDanhMucCha = "CodeHoSo";
                    break;
                case FileManageTypeEnum.CONGVIECCHA:
                    m_tblDbSaveFile = GiaoViec.TBL_FileDinhKem;
                    //m_TenThuMucLonChuaFile = "KeHoachGiaoViec";
                    //m_colCodeDanhMucCha = "CodeCongViecCha";
                    col_CanDuyet.Visible = false;
                    col_CanDuyet.VisibleIndex = 2;
                    var allPermission = BaseFrom.allPermission;

                    if (!allPermission.HaveInitProjectPermission
            && !allPermission.AllContractorThatUserIsAdmin.Contains(SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH?.Code)
            && !allPermission.AllContractorThatUserIsAdmin.Contains(SharedControls.slke_ThongTinDuAn.EditValue as string)
            && !allPermission.TasksInEdit.Contains(codeDanhMucCha))
                    {
                        bt_ThemFile.Enabled = bt_ThemLink.Enabled = false;
                        col_ThayThes.OptionsColumn.ReadOnly = col_Xoa.OptionsColumn.ReadOnly = true;

                        return;
                    }
                    break;

                case FileManageTypeEnum.CongViecCon:
                    m_tblDbSaveFile = GiaoViec.TBL_CongViecCon_FileDinhKem;
                    string dbString = $"SELECT CodeCongViecCha FROM {GiaoViec.TBL_CONGVIECCON} WHERE " +
                        $"CodeCongViecCon = '{m_CodeDanhMucCha}'";
                    DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (dt.Rows.Count == 0)
                    {
                        MessageShower.ShowError("Lỗi tải File công việc con!");
                        this.Close();
                        return;
                    }
                    string codeCVCha = dt.Rows[0][0].ToString();
                    //m_TenThuMucLonChuaFile = "KeHoachGiaoViec";
                    //m_colCodeDanhMucCha = "CodeCongViecCha";
                    col_CanDuyet.Visible = true;
                    col_CanDuyet.VisibleIndex = 2;
                    allPermission = BaseFrom.allPermission;

                    if (!allPermission.HaveInitProjectPermission
            && !allPermission.AllContractorThatUserIsAdmin.Contains(SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH?.Code)
            && !allPermission.AllContractorThatUserIsAdmin.Contains(SharedControls.slke_ThongTinDuAn.EditValue as string)
            && !allPermission.TasksInEdit.Contains(codeCVCha))
                    {
                        bt_ThemFile.Enabled = bt_ThemLink.Enabled = false;
                        col_ThayThes.OptionsColumn.ReadOnly = col_Xoa.OptionsColumn.ReadOnly = true;

                        return;
                    }
                    break;
                //case MyConstant.TYPE_QLFILE_KeHoachGiaoViec_ChiaThiCong:
                //    m_tblDbSaveFile = MyConstant.TBL_QLFILE_GIAOVIEC;
                //    //m_TenThuMucLonChuaFile = @"KeHoachGiaoViec\ChiaThiCong";
                //    //m_colCodeDanhMucCha = "CodeCongViecThiCong";
                //    break;
                //case FileManageTypeEnum.CongViecHangNgay:
                //    m_tblDbSaveFile = MyConstant.TBL_QLFILE_CongViecHangNgay;
                //    //m_TenThuMucLonChuaFile = "QuanLyCongViecHangNgay";
                //    //m_colCodeDanhMucCha = "CodeCongViec";
                //    break;
                case FileManageTypeEnum.TongHopDanhSachHopDong:
                    m_tblDbSaveFile = MyConstant.TBL_QLFILE_HD;
                    //m_TenThuMucLonChuaFile = "HopDong";
                    //m_colCodeDanhMucCha = "CodeHopDong";
                    break;
                case FileManageTypeEnum.THUCHITAMUNG_KC:
                    m_tblDbSaveFile = MyConstant.TBL_QLFILE_KC;
                    //m_TenThuMucLonChuaFile = "ThuChiTamUng_Dexuat";
                    //m_colCodeDanhMucCha = "CodeThuChiTamUng";
                    break;
                case FileManageTypeEnum.THUCHITAMUNG_KT:
                    m_tblDbSaveFile = MyConstant.TBL_QLFILE_KT;
                    //m_TenThuMucLonChuaFile = "ThuChiTamUng_Khoanthu";
                    //m_colCodeDanhMucCha = "CodeKhoanThu";
                    break;
                case FileManageTypeEnum.THUCHITAMUNG_KCCT:
                    m_tblDbSaveFile = MyConstant.TBL_QLFILE_KCCT;
                    break;
                case FileManageTypeEnum.THUCHITAMUNG_DeXuat:
                    m_tblDbSaveFile = MyConstant.TBL_QLFILE_DeXuat;
                    break;          
                case FileManageTypeEnum.QLVC_YeuCauVatTu:
                    m_tblDbSaveFile = QLVT.TBL_QLVT_YEUCAUVT_FileDinhKem;
                    break;        
                case FileManageTypeEnum.QLVC_NhapKho:
                    m_tblDbSaveFile = QLVT.TBL_QLVT_NHAPVT_FileDinhKem;
                    break;             
                case FileManageTypeEnum.QLVC_XuatKho:
                    m_tblDbSaveFile = QLVT.TBL_QLVT_XUATVT_FileDinhKem;
                    break;            
                case FileManageTypeEnum.QLVC_QuanLyVanChuyen:
                    m_tblDbSaveFile = QLVT.TBL_QLVT_QLVC_FileDinhKem;
                    break;
                case FileManageTypeEnum.MayThiCong:
                    m_tblDbSaveFile = MyConstant.TBL_MTC_FILEDINHKEM;
                    col_CanDuyet.Caption = "Ảnh đại điện";
                    col_CanDuyet.Visible = true;
                    col_CanDuyet.VisibleIndex = 2;
                    break;
                case FileManageTypeEnum.ChamCong_TamUng:
                    m_tblDbSaveFile = DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG_FILEDINHKEM;
                    break;
                case FileManageTypeEnum.ChamCong_NhanVien:
                    m_tblDbSaveFile = DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM;
                    col_CanDuyet.Caption = "Ảnh đại điện";
                    col_CanDuyet.Visible = true;
                    col_CanDuyet.VisibleIndex = 2;
                    break;         
                case FileManageTypeEnum.NhatTrinhThiCong:
                    m_tblDbSaveFile = MyConstant.TBL_MTC_NHATTRINHFILEDINHKEM;
                    break;         
                case FileManageTypeEnum.ChiTietCongTacTheoGiaiDoan:
                    m_tblDbSaveFile = TDKH.TBL_ChiTietCongTacTheoKyFileDinhKem;
                    break;      
                case FileManageTypeEnum.NhomCongTac:
                    m_tblDbSaveFile = TDKH.TBL_NhomFileDinhKem;
                    break;
                default:
                    MessageShower.ShowInformation("Sai loại File, không thể mở form!");
                    this.Close();
                    return;
            }

            m_TenThuMucLonChuaFile = m_tblDbSaveFile;
            m_path = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, m_TenThuMucLonChuaFile, codeDanhMucCha));

            //$@"{BaseFrom.m_tempPath}\{BaseFrom.m_crTempDATH}\{m_TenThuMucLonChuaFile}\{codeDanhMucCha}";
            if (!Directory.Exists(m_path))
                Directory.CreateDirectory(m_path);

            cbb_loaiFile.SelectedIndex = 0;
        }


        DataTable dt = new DataTable();
        private void fr_luachon_Load(object sender, EventArgs e)
        {
            //fcn_LoadFile();
        }

        private void fcn_LoadFile(FileFilter loaiFile = FileFilter.LOAIFILE_ALL)
        {
            string queryStr =$"SELECT * FROM {m_tblDbSaveFile}";

            List<string> condition = new List<string>()
            {
                $"{m_colCodeDanhMucCha} = '{m_CodeDanhMucCha}'"
            };

            if (_date.HasValue)
                condition.Add($"Ngay = '{_date.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'");

            switch (loaiFile)
            {
                case FileFilter.LOAIFILE_HinhAnh:
                    condition.Add($"(FileDinhKem LIKE '%.jpg' OR FileDinhKem LIKE '%.jpeg' OR FileDinhKem LIKE '%.png')");
                    break;
                case FileFilter.LOAIFILE_Excel:
                    condition.Add($"(FileDinhKem LIKE '%.xls' OR FileDinhKem LIKE '%.xlsx' OR FileDinhKem LIKE '%.csv')");
                    break;
                case FileFilter.LOAIFILE_Word:
                    condition.Add($"(FileDinhKem LIKE '%.doc' OR FileDinhKem LIKE '%.docx')");
                    break;
                case FileFilter.LOAIFILE_Pdf:
                    condition.Add($"FileDinhKem LIKE '%.pdf')");
                    break;
                default:
                    break;
            }
            queryStr += $" WHERE {string.Join(" AND ", condition)}";
            if (_type == FileManageTypeEnum.ChamCong_NhanVien)
                queryStr += $" AND (SortId=0 OR SortId IS NULL)";
            var ls = DataProvider.InstanceTHDA.ExecuteQueryModel<FileSQLiteViewModel>(queryStr);


            ls.Add(new FileSQLiteViewModel()
            {
                ParentCodeCustom = "",
                Code = nameof(FileLinkEnum.FileVatLy),
                FileDinhKem = "1. File vật lý"
            });


            ls.Add(new FileSQLiteViewModel()
            {
                ParentCodeCustom = "",
                Code = nameof(FileLinkEnum.FileLink),
                FileDinhKem = "2. Đường dẫn File"
            });

            foreach (FileStateEnum state in Enum.GetValues(typeof(FileStateEnum)))
            {
                ls.Add(new FileSQLiteViewModel()
                {
                    ParentCodeCustom = nameof(FileLinkEnum.FileVatLy),
                    Code = $"{nameof(FileLinkEnum.FileVatLy)}_{state.GetEnumName()}",
                    FileDinhKem = state.GetEnumDescription()
                });

                ls.Add(new FileSQLiteViewModel()
                {
                    ParentCodeCustom = nameof(FileLinkEnum.FileLink),
                    Code = $"{nameof(FileLinkEnum.FileLink)}_{state.GetEnumName()}",
                    FileDinhKem = state.GetEnumDescription()
                });
            }

            gc_files.DataSource = new BindingList<FileSQLiteViewModel>(ls);

        }

        //static int stt = 1;


        /*private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int hang = e.RowIndex;
            int cot = e.ColumnIndex;
            int sohang = dtg_luachon.Rows.Count;
            string tenfile = dtg_luachon.Rows[hang].Cells["FileDinhKem"].Value.ToString();
            string duongdantoifile = $@"{m_path}\{tenfile}";
            //string extension = Path.GetExtension(duongdantoifile);
            try
            {
                string Task = dtg_luachon.Columns[cot].HeaderText;
                string Code = dtg_luachon.Rows[e.RowIndex].Cells["Code"].Value.ToString();

                switch (Task)
                {
                    case "Xoa":
                        if (MessageShower.ShowYesNoQuestion("Bạn có chắc muốn xóa không?", "Đang xóa...") == DialogResult.Yes)
                        {
                            File.Delete($@"{m_path}\{tenfile}");
                            string xoafile = $"DELETE FROM {m_tblDbSaveFile} WHERE \"Code\"='{Code}'";
                            DataProvider.InstanceTHDA.ExecuteQuery(xoafile);
                            fr_luachon_Load(null, null);
                        }
                        break;
                    case "Thay thế":
                        m_openDialog.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
                        string tenFileCu = dtg_luachon.Rows[e.RowIndex].Cells["FileDinhKem"].Value.ToString();
                        if (m_openDialog.ShowDialog() == DialogResult.OK)
                        {
                            string fileName;
                            fileName = m_openDialog.FileName;
                            string tenhienthi = m_openDialog.SafeFileName;
                            DialogResult dialogResult = MessageShower.ShowYesNoQuestion($"Bạn có muốn thay thế {tenFileCu} bằng file {tenhienthi} không??", "Thay thế file");
                            if (dialogResult == DialogResult.Yes)
                            {
                                try
                                {
                                    File.Delete($@"{m_path}\{tenfile}");
                                    File.Copy(fileName, $@"{m_path}\{tenhienthi}");

                                    dtg_luachon.Rows[hang].Cells["FileDinhKem"].Value = tenhienthi;
                                    string updatestt = $"UPDATE  {m_tblDbSaveFile} SET \"FileDinhKem\"='{tenhienthi}' WHERE \"Code\"='{Code}'";
                                    DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                                    fr_luachon_Load(null, null);

                                }
                                catch (Exception lỗi)
                                {
                                    MessageShower.ShowInformation(lỗi.ToString());
                                    return;
                                }
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //do something else
                                return;
                            }

                            // MessageShower.ShowInformation(fileName);
                        }
                        break;
                    case "Xem trước":
                        int rowIndex = e.RowIndex;//bo sung sau khi da du thong tin

                        MyFunction.xemTruocFileCoBan(duongdantoifile);
                        break;

                    default:
                        break;
                }                
            }
            catch (Exception ex)
            {
                MessageShower.ShowInformation("Lỗi: " + ex.Message);
            }
             
        }*/

        private void dtg_luachon_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fr_luachon_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void fr_luachon_FormClosed(object sender, FormClosedEventArgs e)
        {
            //int loc = 1;
            //MessageShower.ShowInformation("loc");
        }

        private void bt_chonTatCa_Click(object sender, EventArgs e)
        {
            gc_files.CheckAll();
        }

        enum FileFilter
        {
            LOAIFILE_ALL,
            LOAIFILE_HinhAnh,
            LOAIFILE_Excel,
            LOAIFILE_Word,
            LOAIFILE_Pdf
        }

        private void cbb_loaiFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            fcn_LoadFile((FileFilter)cbb_loaiFile.SelectedIndex);
        }

        private void bt_taiFile_Click(object sender, EventArgs e)
        {
            var lsFile = (gc_files.DataSource as BindingList<FileSQLiteViewModel>)
                .Where(x => x.CreatedOn != null && x.Link is null && x.Checked).ToList();

            if (lsFile.Count == 0)
            {
                MessageShower.ShowInformation("Chưa chọn file Vật lý nào!");
                return;
            }

            try
            {
                if (m_folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in lsFile)
                    {
                        string fileName = file.FileDinhKem;
                        string fileCode = file.Code;
                        if (File.Exists($@"{m_folderBrowserDialog.SelectedPath}\{fileName}"))
                        {
                            if (MessageShower.ShowYesNoQuestion($"File {Path.GetFileName(fileName)} đã tồn tại trong thư mục, Bạn có muốn ghi đè không?", "Cảnh báo") == DialogResult.Yes)
                            {
                                File.Copy($@"{m_path}\{fileCode}", $@"{m_folderBrowserDialog.SelectedPath}\{fileName}", true);
                            }
                            continue;
                        }
                        File.Copy($@"{m_path}\{fileCode}", $@"{m_folderBrowserDialog.SelectedPath}\{fileName}");
                    }
                }
                MessageShower.ShowInformation($"Tải các file thành công về thư mục: {m_folderBrowserDialog.SelectedPath}");
            }
            catch (Exception ex)
            {
                MessageShower.ShowInformation("Lỗi tải file: " + ex.Message);

            }

        }

        private void bt_boChonTatCa_Click(object sender, EventArgs e)
        {
            gc_files.UncheckAll();

        }

        private void repobt_Xoa_Click(object sender, EventArgs e)
        {
            var crNode = gc_files.FocusedNode;
            if (crNode.Level <= 1)
                return;

            if (MessageShower.ShowYesNoQuestion("Bạn có chắc muốn xóa không?", "Đang xóa...") == DialogResult.Yes)
            {
                var crFile = gc_files.GetFocusedRow() as FileSQLiteViewModel;
                File.Delete($@"{m_path}\{crFile.Code}");
                string xoafile = $"DELETE FROM {m_tblDbSaveFile} WHERE \"Code\"='{crFile.Code}'";
                int i = DataProvider.InstanceTHDA.ExecuteNonQuery(xoafile);

                if (i <= 0)
                {
                    MessageShower.ShowError("Không thể xóa file!");
                    return;
                }
                gc_files.DeleteSelectedNodes();
                //if(crFile.State > 0 && _type == MyConstant.TYPE_QLFILE_KEHOACHGIAOVIEC)
                //{
                //    SetFileData();
                //}    
            }
        }

        private async void SetFileData()
        {
            if (BaseFrom.BanQuyenKeyInfo != null && BaseFrom.IsValidAccount)
            {
                var request = new GiaoViecRequest()
                {
                    UserId = ConnextService.UserId
                };
                var lstDatas = await ChatHelper.GetAllGiaoViecByUser(request);
                if (lstDatas.MESSAGE_TYPECODE)
                {
                    ConnextService.ManageGiaoViecs = lstDatas.Dto;
                }
            }
        }

        private void repobt_Replace_Click(object sender, EventArgs e)
        {
            var crNode = gc_files.FocusedNode;
            if (crNode.Level <= 1)
                return;
            var crFile = gc_files.GetFocusedRow() as FileSQLiteViewModel;

            m_openDialog.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
            string tenFileCu = crFile.FileDinhKem;

            if (crFile.Link is null)
            {
                if (m_openDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName;
                    fileName = m_openDialog.FileName;

                    string tenhienthi = m_openDialog.SafeFileName;

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
                            string checksum = FileHelper.CalculateFileMD5(fileName);

                            File.Delete($@"{m_path}\{crFile.FileDinhKem}");
                            File.Copy(fileName, $@"{m_path}\{tenhienthi}");

                            crFile.FileDinhKem = tenhienthi;
                            string updatestt = $"UPDATE  {m_tblDbSaveFile} SET \"FileDinhKem\"=@TenHienThi, \"CheckSum\" = '{checksum}' WHERE \"Code\"='{crFile.Code}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt, parameter: new object[] {tenhienthi});
                            fr_luachon_Load(null, null);

                        }
                        catch (Exception lỗi)
                        {
                            MessageShower.ShowError(lỗi.ToString());
                            return;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                        return;
                    }

                    // MessageShower.ShowInformation(fileName);
                }
            }
            else
            {
                string uri = XtraInputBox.Show("Đường dẫn file", "Nhập đường dẫn file đính kèm", "");
                if (!uri.HasValue())
                {
                    MessageShower.ShowWarning("Vui lòng nhập đường dẫn");
                }
                else
                {
                    if (!System.Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
                    {
                        MessageShower.ShowWarning("Đường dẫn không hợp lệ!");
                    }
                    else
                    {
                        string updatestt = $"UPDATE  {m_tblDbSaveFile} SET \"Link\"= @Link WHERE \"Code\"='{crFile.Code}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt, parameter: new object[] { uri });
                    }
                }
            }
        }

        private void repobt_Preview_Click(object sender, EventArgs e)
        {
                var crNode = gc_files.FocusedNode;
            if (crNode.Level <= 1)
                return;

            var crFile = gc_files.GetFocusedRow() as FileSQLiteViewModel;

            if (crFile.Link is null)
            {
                FileViewModel file = new FileViewModel()
                {
                    FilePath = $@"{m_path}\{crFile.Code}",
                    Table = m_tblDbSaveFile,
                    Checksum =  crFile.Checksum,
                    Code = crFile.Code,
                    FileNameInDb = crFile.FileDinhKem

                };
                MyFunction.xemTruocFileCoBan(file, extension: Path.GetExtension(crFile.FileDinhKem));
            }
            else
            {
                try
                {
                    Process.Start(crFile.Link);
                }
                catch 
                {
                    MessageShower.ShowError("Không thể mở đường dẫn");
                }

            }    
        }

        private void bt_ThemFile_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();

            m_openDialog.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
            m_openDialog.Multiselect = true;

            if (m_openDialog.ShowDialog() == DialogResult.OK)
            {
                //string fileName;
                //fileName = m_openDialog.FileName;
                List<string> InvalidFile = new List<string>();
                foreach (string fileName in m_openDialog.FileNames)
                {
                    string hienthitenfile = Path.GetFileName(fileName);
                    string checksum = FileHelper.CalculateFileMD5(fileName);
                    if (Path.GetDirectoryName(fileName).Length > 248)
                    {
                        MessageShower.ShowInformation($"Thư mục \"{Path.GetDirectoryName(fileName)}\" quá dài, vui lòng đổi tên file ngắn hơn! Các file hợp lệ sẽ vẫn được thêm vào dự án");
                        continue;
                    }

                    if (fileName.Length > 260)
                    {
                        MessageShower.ShowInformation($"Đường dẫn đến file {fileName} quá dài! Vui lòng copy ra thư mục bên ngoài! Các file hợp lệ vẫn được thêm vào");
                        continue;
                    }

                    //DialogResult dialogResult = MessageShower.ShowInformation("Bạn muốn chọn file này ?", "Chọn File", MessageBoxButtons.YesNo);
                    //if (dialogResult == DialogResult.Yes)
                    //{
                    FileInfo fi = new FileInfo(fileName);
                    if (fi.Length > MyConstant.MaxFileSize)
                    {
                        InvalidFile.Add(Path.GetFileName(fileName));
                        continue;
                    }


                    try
                    {
                        //textlink.Text = fileName;
                        string newGUID = Guid.NewGuid().ToString();
                        File.Copy(fileName, $@"{m_path}\{newGUID}");

                        string strNgay = (_date.HasValue) ? $"'{_date.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'" : "NULL";

                        Dictionary<string, object> dicVal = new Dictionary<string, object>()
                        {

                        };
                        string themfile = $"INSERT INTO {m_tblDbSaveFile} " +
                        $"(Code, FileDinhKem, {m_colCodeDanhMucCha}, Ngay, Checksum) VALUES " +
                        $"('{newGUID}', @FileDinhKem,'{m_CodeDanhMucCha}', {strNgay},'{checksum}')";

                        DataProvider.InstanceTHDA.ExecuteNonQuery(themfile, parameter: new object[] {hienthitenfile});

                        var lsFile = gc_files.DataSource as BindingList<FileSQLiteViewModel>;
                        lsFile.Add(new FileSQLiteViewModel()
                        {
                            Code = newGUID,
                            FileDinhKem = hienthitenfile,
                            Ngay = _date,
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageShower.ShowInformation($"{ex.Message}__Inner: {ex.InnerException?.Message}");
                    }

                }
                if (InvalidFile.Any())
                {
                    string noti = $"Các file sau không được thêm vì vượt quá dung lượng cho phép (150MB):\r\n" +
                        $"{string.Join("\r\n", InvalidFile)}";
                    MessageShower.ShowWarning(noti);
                }
            }

            gc_files.ExpandAll();
        }


        private void gc_files_DataSourceChanged(object sender, EventArgs e)
        {
            gc_files.ExpandAll();
        }

        private void gc_files_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
                return;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                return;
            }
        }

        private void gc_files_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == nameof(FileSQLiteViewModel.State))
            {
                gc_files.CloseEditor();
                var crFile = gc_files.GetFocusedRow() as FileSQLiteViewModel;

                if (crFile.State == (int)FileStateEnum.APPROVED)
                {
                    MessageShower.ShowError("Không thể thay đổi trạng thái file đã duyệt");

                    return;
                }
                string dbString = $"UPDATE {m_tblDbSaveFile} SET State = '{e.Value}' WHERE \"Code\"='{crFile.Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                crFile.State = (int)e.Value;
                if (col_CanDuyet.Caption == "Ảnh đại điện")
                {
                    if (crFile.State == 1)
                    {
                        dbString = $"SELECT * " +
$"FROM {m_tblDbSaveFile}  WHERE \"CodeParent\" ='{m_CodeDanhMucCha}'";
                        DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Code"].ToString() == crFile.Code)
                                continue;
                            dbString = $"UPDATE {m_tblDbSaveFile} SET State = '{0}' WHERE \"Code\"='{row["Code"]}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        }
                        fcn_LoadFile((FileFilter)cbb_loaiFile.SelectedIndex);
                        return;
                    }
                }
                gc_files.RefreshDataSource();
                gc_files.ExpandAll();
            }
        }

        private void gc_files_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            var crFile = gc_files.GetFocusedRow() as FileSQLiteViewModel;
            if (e.Column.FieldName == nameof(FileSQLiteViewModel.FileDinhKem)||e.Column.FieldName == nameof(FileSQLiteViewModel.GhiChu))
            {
                string dbString = $"UPDATE {m_tblDbSaveFile} SET {e.Column.FieldName} = @FileDinhKem WHERE \"Code\"='{crFile.Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] {e.Value});
            }      
        }

        private void bt_ThemLink_Click(object sender, EventArgs e)
        {
            string uri = XtraInputBox.Show("Đường dẫn file", "Nhập đường dẫn file đính kèm", "");
            if (!uri.HasValue())
            {
                MessageShower.ShowWarning("Vui lòng nhập đường dẫn");
            }
            else
            {
                if (!System.Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
                {
                    MessageShower.ShowWarning("Đường dẫn không hợp lệ!");
                }
                else
                {
                    string newGUID = Guid.NewGuid().ToString();
                    //File.Copy("Đường dẫn File mới", $@"{m_path}\{newGUID}");

                    string strNgay = (_date.HasValue) ? $"'{_date.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'" : "NULL";

                    string themfile = $"INSERT INTO {m_tblDbSaveFile} " +
                    $"(Code, FileDinhKem, {m_colCodeDanhMucCha}, Ngay, Link) VALUES " +
                    $"('{newGUID}','Đường dẫn File mới','{m_CodeDanhMucCha}', {strNgay}, @Link)";

                    DataProvider.InstanceTHDA.ExecuteNonQuery(themfile, parameter: new object[] { uri });

                    var lsFile = gc_files.DataSource as BindingList<FileSQLiteViewModel>;
                    lsFile.Add(new FileSQLiteViewModel()
                    {
                        Code = newGUID,
                        FileDinhKem = "Đường dẫn File mới",
                        Ngay = _date,
                        Link = uri
                    });
                }
            }
            gc_files.ExpandAll();
        }

        private void gc_files_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            var fieldName = e.Column.FieldName;
            string[] strs =
            {
                nameof(col_Xoa),
                nameof(col_XemTruoc),
                nameof(col_ThayThes),
            };

            if (strs.Contains(e.Column.Name) && (e.Node.Level <= 1))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }
    }
}
