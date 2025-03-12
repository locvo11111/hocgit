using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Spreadsheet;
using DevExpress.Utils.CommonDialogs;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
//using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Function;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Unity;
using Row = DevExpress.Spreadsheet.Row;

namespace PhanMemQuanLyThiCong.ChatBox.Views
{
    public partial class BusinessTableView : DevExpress.XtraEditors.XtraForm
    {

        public BusinessTableView()
        {
            InitializeComponent();
        }

        private async void BusinessTableView_Load(object sender, EventArgs e)
        {
            if (BaseFrom.IsValidAccount && BaseFrom.BanQuyenKeyInfo != null && ConnextService.UserId !=null)
            {
                var request = new GiaoViecRequest()
                {
                    UserId = ConnextService.UserId
                };
                var lstDatas = await ChatHelper.GetAllGiaoViecByUser(request); 
                if (lstDatas.MESSAGE_TYPECODE)
                {
                    ConnextService.ManageGiaoViecs = lstDatas.Dto;
                    gridControl.DataSource = ConnextService.ManageGiaoViecs;
                }
            }
        }
 
        private async void dowload_file_btn_Click(object sender, EventArgs e)
        {
            gridControl.CloseEditor();
            //ManageWorkViewModel item = gridView.FocusedRowObject as ManageWorkViewModel;

            //var datafile = await _chatService.GetfileByBusiness(item.Id.ToString(), ConnextService.UriChat);

            ////Save file
            //System.Drawing.Image image = null;
            //Stream res = null;
            //FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            //folderBrowserDialog.ShowNewFolderButton = true;
            //DialogResult result = folderBrowserDialog.ShowDialog(this);
            //if (result == DialogResult.OK)
            //{
            //    string fileRoot = folderBrowserDialog.SelectedPath;

            //    List<string> files = datafile.Dto.Select(x => x.FilePath).ToList();
            //    try
            //    {
            //        foreach (string file in files)
            //        {
            //            //Đọc file từ sever về 
            //            string filedow = $@"{ConnextService.UriChat}{file}";
            //            string fileName = Path.GetFileName(filedow);
            //            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            //            res = await httpClient.GetStreamAsync(filedow);
            //            string filesave = $@"{fileRoot}\{fileName}";
            //            if(File.Exists(filesave))
            //                File.Delete(filesave);
            //            //Save file tạm vào trong thư mục
            //            using (var fs = new FileStream(filesave, FileMode.CreateNew))
            //            {
            //                await res.CopyToAsync(fs);
            //            }
            //        };
            //        DialogResult dialogResult = MessageShower.ShowYesNoQuestion("File đã được tải xuống. Bạn có muốn mở thư mục chứa file hay không ???", "Thông báo");
            //        if(dialogResult == DialogResult.Yes)
            //        {
            //            System.Diagnostics.Process.Start("explorer.exe", fileRoot);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageShower.ShowError($"Đã lỗi khi tải file: {ex.Message}", "Thông báo");
            //        return;
            //    }
            //}
        }

        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //try
            //{
            //    view.BeginUpdate();
            //    if (e.RowHandle > -1)
            //    {
            //        double klhd = double.Parse(GetRowCellValue(view, e.RowHandle, "KhoiLuongHD") == null ? "0" : GetRowCellValue(view, e.RowHandle, "KhoiLuongHD"));
            //        double kltt = double.Parse(GetRowCellValue(view, e.RowHandle, "KhoiLuongTT") == null ? "0" : GetRowCellValue(view, e.RowHandle, "KhoiLuongTT"));
            //        switch (e.Column.FieldName)
            //        {
            //            case "KhoiLuongHD":
            //            case "KhoiLuongTT":
            //                if(klhd > 0 && kltt > 0)
            //                {
            //                    double tyle = Math.Round((kltt / klhd) * 100, 2);
            //                    view.SetRowCellValue(e.RowHandle, "TyLeHoanThanh", tyle);
            //                }
            //                break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            //    view.EndUpdate();
            //}
        }

        /// <summary>
        /// GetRowCellValue
        /// </summary>
        /// <param name="view"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetRowCellValue(DevExpress.XtraGrid.Views.Grid.GridView view, int row, string column)
        {
            try
            {
                return view.GetRowCellValue(row, column) == null ? null : Convert.ToString(view.GetRowCellValue(row, column));
            }
            catch
            {
                return null;
            }
        }
      
        private void btn_Export_Click(object sender, EventArgs e)
        {
            var lstWorks = (List<GiaoViecExtensionViewModel>)gridControl.DataSource;
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Execl files (*.xlsx)|*.xlsx";
            f.FileName = $"Báo cáo hàng ngày - {DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.xlsx";
            f.FilterIndex = 0;
            f.RestoreDirectory = true;
            f.CreatePrompt = true;
            f.Title = "Xuất báo cáo hàng ngày";
            if (f.ShowDialog() == DialogResult.OK)
            {
                DevExpress.Spreadsheet.Workbook wb = new DevExpress.Spreadsheet.Workbook();
                if(File.Exists(Path.Combine(BaseFrom.m_path, "Template", CommonConstants.FILE_BAOCAOHANGNGAY)))
                {
                    wb.LoadDocument(Path.Combine(BaseFrom.m_path, "Template", CommonConstants.FILE_BAOCAOHANGNGAY));
                    DefinedName name = wb.DefinedNames.GetDefinedName(CommonConstants.bm_ListCongTac);
                    DevExpress.Spreadsheet.Worksheet ws = name.Range.Worksheet;                  
                    Row crRow;
                    List<string> lst = new List<string>();
                    List<string> lstIds = new List<string>();
                    int stt = 0;
                    foreach (var item in lstWorks)
                    {
                        if (lstIds.Contains(item.CodeCongViecCha)) continue;
                        lstIds.Add(item.CodeCongViecCha);
                        string tenHangMuc = string.IsNullOrEmpty(item.TenHangMuc) ? item.TenDauViecNho: item.TenHangMuc;
                        if (!lst.Exists(x=>x.Contains(tenHangMuc)))
                        {
                            lst.Add(tenHangMuc);
                            ws.Rows.Insert(name.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                            crRow = ws.Rows[name.Range.BottomRowIndex - 1];
                            crRow.Font.Bold = true;
                            crRow.Font.Color = Color.Blue;
                            crRow["C"].SetValue(tenHangMuc);
                        }
                        ws.Rows.Insert(name.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                        crRow = ws.Rows[name.Range.BottomRowIndex - 1];
                        crRow["B"].SetValue(++stt);
                        crRow["C"].SetValue(item.TenCongViec);
                        crRow["D"].SetValue(item.DonVi);
                        crRow["E"].SetValue(item.KhoiLuongHopDong);
                        crRow["F"].SetValue(item.KhoiLuongKeHoach);
                        crRow["G"].SetValue(item.KhoiLuongThanhToan);
                        crRow["H"].Formula = $"=IFERROR({crRow["G"].GetReferenceA1()}/{crRow["E"].GetReferenceA1()};0%)";
                        crRow["I"].SetValue(item.NgayBatDau);
                        crRow["K"].SetValue(item.NgayKetThuc);
                        crRow["M"].SetValue(item.NgayDuyet);
                        crRow["N"].SetValue(item.FullNameSend);
                        crRow["O"].SetValue(item.FullNameApprove);
                        crRow["P"].SetValue(item.GhiChuDuyet);
                    }
                    ws.Rows[name.Range.BottomRowIndex].Delete();
                    name = wb.DefinedNames.GetDefinedName(CommonConstants.bm_NgayBaoCao);
                    if(name!=null)
                    {
                        crRow = ws.Rows[name.Range.BottomRowIndex];
                        crRow[name.Range.RightColumnIndex].SetValue($"Ngày {DateTime.Now.Date.Day} tháng {DateTime.Now.Date.Month} năm {DateTime.Now.Date.Year}");
                    }
                    name = wb.DefinedNames.GetDefinedName(CommonConstants.bm_TenCongTrinh);
                    if (name != null)
                    {
                        crRow = ws.Rows[name.Range.BottomRowIndex];
                        //crRow[name.Range.RightColumnIndex].SetValue($"Công trình: {ConnextService.groupIndex.ConstructionName}");
                    }
                    name = wb.DefinedNames.GetDefinedName(CommonConstants.bm_DiaDiemXayDung);
                    if (name != null)
                    {
                        crRow = ws.Rows[name.Range.BottomRowIndex];
                        //crRow[name.Range.RightColumnIndex].SetValue($"Địa điểm xây dựng: {ConnextService.groupIndex.ConstructionAddress}");
                    }
                }
                wb.Calculate();
                wb.SaveDocument(f.FileName, DocumentFormat.Xlsx);
                DialogResult dialogResult = MessageShower.ShowYesNoQuestion("File lưu thành công. Bạn có muốn mở file luôn hay không ???", "Thông báo");
                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(f.FileName);
                }
            }
        }

        private void btn_DongBo_Click(object sender, EventArgs e)
        {

        }
    }
}