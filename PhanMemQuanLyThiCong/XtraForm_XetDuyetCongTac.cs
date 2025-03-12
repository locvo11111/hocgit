using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.ChatBox;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using MoreLinq;
using PhanMemQuanLyThiCong.ChatBox.Views;
using Microsoft.AspNetCore.SignalR.Client;
using DevExpress.Spreadsheet;
using System.IO;
using PhanMemQuanLyThiCong.Common.ViewModel;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_XetDuyetCongTac : DevExpress.XtraEditors.XtraForm
    {
        List<GiaoViecExtensionViewModel> giaoViecs = new List<GiaoViecExtensionViewModel>();
        public XtraForm_XetDuyetCongTac()
        {
            InitializeComponent();
        }

        private async void XtraForm_XetDuyetCongTac_Load(object sender, EventArgs e)
        {
            if (BaseFrom.IsValidAccount && BaseFrom.BanQuyenKeyInfo != null && ConnextService.UserId != null)
            {
                var request = new GiaoViecRequest()
                {
                    UserId = ConnextService.UserId,
                    
                };
                WaitFormHelper.ShowWaitForm("Đang tải thông tin công tác!");

               

                var lstDatas = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<List<GiaoViecExtensionViewModel>>($"{RouteAPI.GiaoViec_GetByRequest}", request);
                WaitFormHelper.CloseWaitForm();
                if (!lstDatas.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError("Lỗi tải công tác xét duyệt, Kiểm tra kết nối internet hoặc liên hệ hỗ trợ!");
                    this.Close();
                    return;
                }

                giaoViecs = lstDatas.Dto;
                tl_CongTac.DataSource = giaoViecs;
                
                //var roles = lstDatas.Dto.roles;

                var CtacSelfApprove = giaoViecs.Where(x => x.ListUserDuyets.Contains(BaseFrom.BanQuyenKeyInfo.UserId))
                    .Select(x => x.CodeCongViecCha);

                List<GiaoViecExtensionViewModel> lsDoiDuyet = new List<GiaoViecExtensionViewModel>()
                        {
                            new GiaoViecExtensionViewModel(){Id = nameof(CongTacDoiDuyetTypeEnum.SELF), TenCongViec =  "Đợi bạn duyệt"},
                            new GiaoViecExtensionViewModel(){Id = nameof(CongTacDoiDuyetTypeEnum.OTHERS), TenCongViec = "Đợi người khác duyệt"}
                        };

                var lsChoDuyet = giaoViecs.Where(x => x.TrangThai == EnumTrangThai.DANGXETDUYET.GetEnumDisplayName());
                var lsSelf = lsChoDuyet.Where(x => CtacSelfApprove.Contains(x.CodeCongViecCha));
                var lsOthers = lsChoDuyet.Where(x => !CtacSelfApprove.Contains(x.CodeCongViecCha));
                lsSelf.Where(x => x.CodeCongViecCon is null).ForEach(x => x.ParentId = nameof(CongTacDoiDuyetTypeEnum.SELF));
                lsOthers.Where(x => x.CodeCongViecCon is null).ForEach(x => x.ParentId = nameof(CongTacDoiDuyetTypeEnum.OTHERS));

                lsDoiDuyet.AddRange(lsSelf);
                lsDoiDuyet.AddRange(lsOthers);
                tl_DoiDuyet.DataSource = lsDoiDuyet;

                tl_DaDuyet.DataSource = giaoViecs.Where(x => x.TrangThai == EnumTrangThai.HOANTHANH.GetEnumDisplayName()).ToList();



            }
            else
            {
                MessageShower.ShowError("Lỗi tải công tác xét duyệt");
                this.Close();
                return;
            }    
        }

        private void gridControl_DataSourceChanged(object sender, EventArgs e)
        {
            tl_CongTac.ExpandAll();
        }

        private void tl_DoiDuyet_DataSourceChanged(object sender, EventArgs e)
        {
            tl_DoiDuyet.ExpandAll();
        }

        private void tl_DaDuyet_DataSourceChanged(object sender, EventArgs e)
        {
            tl_DaDuyet.ExpandAll();
        }

        private void tl_DoiDuyet_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 0:
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                    break;
                case 1:
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    break;
                default:
                    break;
            }
        }

        private void tl_DoiDuyet_DoubleClick(object sender, EventArgs e)
        {
            var congViecIndex = tl_DoiDuyet.GetFocusedRow() as GiaoViecExtensionViewModel;
            if (congViecIndex == null) return;
            ApproveWork frm = new ApproveWork(congViecIndex);
            //if (!CheckOpened(frm.Name))
            //{
            frm.sendState = new ApproveWork.SendStateDuyet(MapStateDuyet);
            frm.ShowDialog();
            //}
        }

        private async void MapStateDuyet(int state, GiaoViecExtensionViewModel giaoViec)
        {
            //LoadDataGiaoViec();
            //switch (state)
            //{
            //    //Duyệt 1 phần
            //    case 1:
            //        if (ConnextService.IsConnected)
            //        {
            //            await ConnextService._Connection.InvokeAsync("NotiDuyetCongTacInComplete", BaseFrom.BanQuyenKeyInfo, giaoViec);
            //        }
            //        break;
            //    ////Duyệt toàn bộ
            //    case 2:
            //        if (ConnextService.IsConnected)
            //        {
            //            await ConnextService._Connection.InvokeAsync("NotiDuyetCongTacComplete", BaseFrom.BanQuyenKeyInfo, giaoViec);
            //        }
            //        break;
            //    default:
            //        break;
            //}
            XtraForm_XetDuyetCongTac_Load(null, null);
        }

        private void tl_DoiDuyet_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            var lstWorks = (List<GiaoViecExtensionViewModel>)tl_CongTac.DataSource;
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Execl files (*.xlsx)|*.xlsx";
            f.FileName = $"Báo cáo hàng ngày - {DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.xlsx";
            f.FilterIndex = 0;
            f.RestoreDirectory = true;
            f.CreatePrompt = true;
            f.Title = "Xuất báo cáo hàng ngày";
            if (f.ShowDialog() == DialogResult.OK)
            {
                Workbook wb = new Workbook();
                if (File.Exists(Path.Combine(BaseFrom.m_path, "Template", CommonConstants.FILE_BAOCAOHANGNGAY)))
                {
                    wb.LoadDocument(Path.Combine(BaseFrom.m_path, "Template", CommonConstants.FILE_BAOCAOHANGNGAY));
                    DefinedName name = wb.DefinedNames.GetDefinedName(CommonConstants.bm_ListCongTac);
                    Worksheet ws = name.Range.Worksheet;
////                  wb.History.IsEnabled = false;
                    wb.BeginUpdate();

                    Row crRow;
                    List<string> lst = new List<string>();
                    List<string> lstIds = new List<string>();
                    int stt = 0;

                    var grs = lstWorks.GroupBy(x => x.CodeCongViecCha);

                    foreach (var gr in grs)
                    {
                        var cha = gr.SingleOrDefault(x => x.CodeCongViecCon is null);
                        if (cha is null)
                        {
                            MessageShower.ShowError("Lỗi xuất files! Không có công tác cha!");
                            wb.Dispose();
                            return;
                        }   
                        
                        if (lstIds.Contains(cha.CodeCongViecCha)) continue;
                        lstIds.Add(cha.CodeCongViecCha);
                        string tenHangMuc = string.IsNullOrEmpty(cha.TenHangMuc) ? cha.TenDauViecNho : cha.TenHangMuc;
                        if (!lst.Exists(x => x.Contains(tenHangMuc)))
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
                        crRow["C"].SetValue(cha.TenCongViec);
                        crRow["C"].Font.Bold = true;
                        crRow["D"].SetValue(cha.DonVi);
                        crRow["E"].SetValue(cha.KhoiLuongHopDong);
                        crRow["F"].SetValue(cha.KhoiLuongKeHoach);
                        crRow["G"].SetValue(cha.KhoiLuongThanhToan);
                        crRow["H"].Formula = $"=IFERROR({crRow["G"].GetReferenceA1()}/{crRow["E"].GetReferenceA1()};0%)";
                        crRow["I"].SetValue(cha.NgayBatDau);
                        crRow["K"].SetValue(cha.NgayKetThuc);
                        crRow["M"].SetValue(cha.NgayDuyet);
                        crRow["N"].SetValue(cha.FullNameSend);
                        crRow["O"].SetValue(cha.FullNameApprove);
                        crRow["P"].SetValue(cha.GhiChuDuyet);

                        var cons = gr.Where(x => x.CodeCongViecCon != null);
                        int sttCon = 1;
                        if (cons.Any())
                        {
                            foreach (var con in cons)
                            {
                                ws.Rows.Insert(name.Range.BottomRowIndex, 1, RowFormatMode.FormatAsNext);
                                crRow = ws.Rows[name.Range.BottomRowIndex - 1];
                                crRow.Font.Italic = true;
                                crRow["B"].SetValue($"{stt}.{sttCon++}");
                                crRow["C"].SetValue(con.TenCongViec);
                                crRow["D"].SetValue(con.DonVi);
                                crRow["E"].SetValue(con.KhoiLuongHopDong);
                                crRow["F"].SetValue(con.KhoiLuongKeHoach);
                                crRow["G"].SetValue(con.KhoiLuongThanhToan);
                                crRow["H"].Formula = $"=IFERROR({crRow["G"].GetReferenceA1()}/{crRow["E"].GetReferenceA1()};0%)";
                                crRow["I"].SetValue(con.NgayBatDau);
                                crRow["K"].SetValue(con.NgayKetThuc);
                                crRow["M"].SetValue(con.NgayDuyet);
                                crRow["N"].SetValue(con.FullNameSend);
                                crRow["O"].SetValue(con.FullNameApprove);
                                crRow["P"].SetValue(con.GhiChuDuyet);
                            }
                        }
                    }
                    ws.Rows[name.Range.BottomRowIndex].Delete();
                    name = wb.DefinedNames.GetDefinedName(CommonConstants.bm_NgayBaoCao);
                    if (name != null)
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

                wb.EndUpdate();

                try
                {
////              wb.History.IsEnabled  = true;

                }
                catch (Exception) { }

                wb.SaveDocument(f.FileName, DocumentFormat.Xlsx);
                wb.Dispose();
                DialogResult dialogResult = MessageShower.ShowYesNoQuestion("File lưu thành công. Bạn có muốn mở file luôn hay không ???", "Thông báo");
                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(f.FileName);
                }
            }
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            var congViecIndex = tl_CongTac.GetFocusedRow() as GiaoViecExtensionViewModel;
            if (congViecIndex == null) return;


            //congViecIndex.ListUserViews = BaseFrom.usersInView;// RoleDetails.FindAll(x => x.CongViecChaCode == congViecIndex.CodeCongViecCha && x.CommandId == CommonConstants.COMMAND_VIEW).Select(x => x.UserId).ToList();
            //congViecIndex.ListUserDuyets = BaseFrom.usersInApprove;// RoleDetails.FindAll(x => x.CongViecChaCode == congViecIndex.CodeCongViecCha && x.CommandId == CommonConstants.COMMAND_APPROVE).Select(x => x.UserId).ToList();
            //congViecIndex.ListUserThiCongs = BaseFrom.usersInAdmin.Concat(BaseFrom.usersInEdit).ToList();// RoleDetails.FindAll(x => x.CongViecChaCode == congViecIndex.CodeCongViecCha && x.CommandId == CommonConstants.COMMAND_EDIT).Select(x => x.UserId).ToList();
            FrmGuiDuyet frm = new FrmGuiDuyet(congViecIndex);
            //if (!CheckOpened(frm.Name))
            //{
                frm.ShowDialog();
            //}
        }

        private void btn_CongTac_Click(object sender, EventArgs e)
        {
            BusinessTableView frm = new BusinessTableView();
            frm.ShowDialog();
        }
    }
}