using DevExpress.Spreadsheet;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraSpreadsheet.Menu;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Constant.Enum;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.HopDong;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls.QLVC
{
    public partial class Uc_DeXuatVatTu : DevExpress.XtraEditors.XtraUserControl
    {
        ctrlTimKiemVatLieu m_ctrlVatlieu;
        ctrl_TimKiemVatLieuThuCong m_ctrlVatlieuThuCong;
        public Uc_DeXuatVatTu()
        {
            InitializeComponent();
            SharedControls.rILUE_TenKhoTH = rILUE_TenKhoTH;
            SharedControls.tL_QLVC_TongHop = tL_QLVC_TongHop;
        }
        private void Fcn_Init()
        {
            m_ctrlVatlieu = new ctrlTimKiemVatLieu();
            m_ctrlVatlieu.m_DataChonVL = new ctrlTimKiemVatLieu.DE_TRUYENDATAVATLIEU(fcn_DE_NhanDatVL);
            m_ctrlVatlieu.Dock = DockStyle.None;
            this.Controls.Add(m_ctrlVatlieu);
            m_ctrlVatlieu.Hide();

            m_ctrlVatlieuThuCong = new ctrl_TimKiemVatLieuThuCong();
            m_ctrlVatlieuThuCong.m_DataChonVL = new ctrl_TimKiemVatLieuThuCong.DE_TRUYENDATAVATLIEU(fcn_DE_NhanDataKHVT);
            m_ctrlVatlieuThuCong.Dock = DockStyle.None;
            this.Controls.Add(m_ctrlVatlieuThuCong);
            m_ctrlVatlieuThuCong.Hide();
            //Fcn_LoadDataQLVC();
        }
        private void fcn_DE_NhanDataKHVT(DataTable dt)
        {
            if (dt == null)
            {
                return;
            }
            spsheet_KeHachVatTu.CloseCellEditor(CellEditorEnterValueMode.Cancel);
            Worksheet ws = spsheet_KeHachVatTu.Document.Worksheets.ActiveWorksheet;
            CellRange Select = ws.SelectedCell;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            double STT = 1;
            double RowCha = 0;
            Row copy = ws.Rows[2];
            for (int i = Select.TopRowIndex - 1; i >= 0; i--)
            {
                Row Crow = ws.Rows[i];
                string MaHieu = Crow[Name["MaVatTu"]].Value.ToString();
                string Code = Crow[Name["Code"]].Value.ToString();
                double Stt = Crow[Name["STT"]].Value.IsNumeric ? Crow[Name["STT"]].Value.NumericValue : 0;
                if (MaHieu == MyConstant.CONST_TYPE_HANGMUC)
                {
                    STT = 1;
                    RowCha = i + 1;
                    break;
                }
                else if (MaHieu == "")
                    continue;
                else if (Stt > 0)
                {
                    STT = Stt + 1;
                    RowCha = Crow[Name[MyConstant.COL_KHVT_RowCha]].Value.NumericValue;
                    break;
                }
            }
            spsheet_KeHachVatTu.BeginUpdate();
            ws.Rows.Insert(Select.TopRowIndex, dt.Rows.Count, RowFormatMode.FormatAsNext);
            int Index = Select.TopRowIndex;
            foreach (DataRow row in dt.Rows)
            {
                string Code = Guid.NewGuid().ToString();
                Row crRowWs = ws.Rows[Index++];
                crRowWs.CopyFrom(copy, PasteSpecial.All);
                crRowWs.Visible = true;
                if (row["MaVatLieu"].ToString() == "TT")
                    crRowWs.Font.Color = Color.Red;
                crRowWs[Name[MyConstant.COL_KHVT_CodeCT]].SetValueFromText(Code);
                crRowWs[Name[MyConstant.COL_KHVT_STT]].SetValue(STT++);
                crRowWs[Name[MyConstant.COL_KHVT_RowCha]].SetValue(RowCha);
                crRowWs[Name[MyConstant.COL_KHVT_MaVatTu]].SetValueFromText(row["MaVatLieu"].ToString());
                crRowWs[Name[MyConstant.COL_KHVT_TenVatTu]].SetValueFromText(row["VatTu"].ToString());
                crRowWs[Name[MyConstant.COL_KHVT_DonVi]].SetValueFromText(row["DonVi"].ToString());

                string db_string = $"INSERT INTO {QLVT.TBL_QLVT_KHVT} (\"Code\",\"CodeHangMuc\",\"VatTu\",\"DonVi\",\"MaVatLieu\") " +
                    $"VALUES ('{Code}','{m_ctrlVatlieuThuCong.m_codeHM}',@VatTu, @DonVi, @MaVatLieu)";
                DataProvider.InstanceTHDA.ExecuteNonQuery(db_string, parameter: new object[] { row["VatTu"], row["DonVi"], row["MaVatLieu"] });
            }
            spsheet_KeHachVatTu.EndUpdate();
        }
        private void fcn_UpdateVatLieu_ThuCong(DataTable dt)
        {
            tL_YeuCauVatTu.DeleteSelectedNodes();
            tL_NhapKho.RefreshDataSource();
            List<VatLieu> VL = tL_YeuCauVatTu.DataSource as List<VatLieu>;
            List<NhapVatLieu> NVL = tL_NhapKho.DataSource as List<NhapVatLieu>;
            foreach (DataRow item in dt.Rows)
            {
                string codedexuat = Guid.NewGuid().ToString();
                string dbString = $"INSERT INTO '{QLVT.TBL_QLVT_YEUCAUVT}' (\"TrangThai\",\"Code\",\"TenVatTu\",\"MaVatTu\",\"DonVi\",\"CodeGiaiDoan\",\"CodeHangMuc\") " +
                    $"VALUES ('{1}','{codedexuat}',@VatTu, @MaVatLieu, @DonVi,'{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','{m_ctrlVatlieu.m_codeHM}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { item["VatTu"], item["MaVatLieu"], item["DonVi"] });
                string codeNhapVT = Guid.NewGuid().ToString();
                dbString = $"INSERT INTO {QLVT.TBL_QLVT_NHAPVT} (\"TrangThai\",\"CodeDeXuat\",\"Code\",\"CodeGiaiDoan\",\"TenKhoNhap\") VALUES ('{1}','{codedexuat}','{codeNhapVT}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}',NULL)";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                VL.Add(new VatLieu()
                {
                    ParentID = m_ctrlVatlieu.m_codeHM,
                    ID = codedexuat,
                    TrangThai = 1,
                    MaVatTu = item["MaVatLieu"].ToString(),
                    TenVatTu = item["VatTu"].ToString(),
                    DonVi = item["Donvi"].ToString(),
                    FileDinhKem = "Xem File"
                    //CodeHd = "",
                    //CodeKHVT = "",
                    //CodeTDKH = ""
                });
                NVL.Add(new NhapVatLieu()
                {
                    ParentID = m_ctrlVatlieu.m_codeHM,
                    TenKhoNhap = m_ctrlVatlieu.m_codeHM,
                    ID = codeNhapVT,
                    CodeDeXuat = codedexuat,
                    MaVatTu = item["MaVatLieu"].ToString(),
                    TenVatTu = item["VatTu"].ToString(),
                    DonVi = item["Donvi"].ToString(),
                    TrangThai = 1,
                    FileDinhKem = "Xem File"
                    //CodeHd = null,
                    //CodeKHVT = null,
                    //CodeTDKH = null
                });

            }
            VL.Add(new VatLieu()
            {
                ParentID = m_ctrlVatlieu.m_codeHM,
                ID = Guid.NewGuid().ToString(),
            });


            tL_YeuCauVatTu.RefreshDataSource();
            tL_YeuCauVatTu.Refresh();
            tL_YeuCauVatTu.EndUpdate();
            //tL_YeuCauVatTu.DataSource = VL;
            tL_YeuCauVatTu.FocusedNode = tL_YeuCauVatTu.MoveLastVisible();

            tL_NhapKho.RefreshDataSource();
            //tL_NhapKho.DataSource = NVL;
            tL_NhapKho.FocusedNode = tL_NhapKho.MoveLastVisible();
            //Thread.CurrentThread.Abort();
        }
        private void fcn_DE_NhanDatVL(DataTable dt)
        {
            if (dt == null)
            {
                tL_YeuCauVatTu.CancelCurrentEdit();
                return;
            }
            switch (m_ctrlVatlieu._type)
            {
                //case 1:
                //    fcn_UpdateVatLieu(dt);
                //    break;
                case 2:
                    fcn_UpdateVatLieu_ThuCong(dt);
                    break;
                case 3:
                    //fcn_UpdateVatLieu_KHVT(dt);
                    break;
            }
        }
        private double Fcn_CalKLVatTu(string Code, string Tbl, DateTime? NBD = null, DateTime? NKT = null)
        {
            double KL = 0;
            string dbString = "";
            string Condition = NBD is null ? string.Empty : $" AND \"Ngay\">='{NBD.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND \"Ngay\"<='{NKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            if (Tbl == QLVT.TBL_QLVT_NHAPVTKLHN)
            {
                dbString = $"SELECT * FROM {QLVT.TBL_QLVT_NKVC} WHERE \"CodeCha\" = '{Code}' {Condition}";
                DataTable dtNc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dtNc.Rows.Count != 0)
                    KL += dtNc.AsEnumerable().Sum(x => double.Parse(x["ThucTeVanChuyen"].ToString()));

            }
            dbString = Tbl == QLVT.TBL_QLVT_NKVC ? $"SELECT {Tbl}.ThucTeVanChuyen AS KhoiLuong FROM {Tbl} WHERE \"CodeCha\" = '{Code}' {Condition}"
                : $"SELECT {Tbl}.KhoiLuong FROM {Tbl} WHERE \"CodeCha\" = '{Code}' AND \"TrangThai\"=2 {Condition}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count == 0)
                return KL;
            KL += dt.AsEnumerable().Sum(x => double.Parse(x["KhoiLuong"].ToString()));
            return KL;
        }
        private void fcn_updateDeXuatVatLieu()
        {
            string dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            DataTable dt_yeucau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<VatLieuHoanThanh> VLHT = new List<VatLieuHoanThanh>();
            int percent = 0, stt = 1;
            foreach (DataRow item in dt_yeucau.Rows)
            {
                if (item["TrangThai"].ToString() == "1" || item["IsDone"].ToString() == "True" || item["HopDongKl"] == DBNull.Value || item["LuyKeYeuCau"] == DBNull.Value)
                    continue;
                percent = (int)Math.Round(100 * ((double.Parse(item["HopDongKl"].ToString()) - double.Parse(item["LuyKeYeuCau"].ToString())) / double.Parse(item["HopDongKl"].ToString())));
                if (percent > 10)
                    continue;
                VLHT.Add(new VatLieuHoanThanh()
                {
                    STT = stt++,
                    HopDongKl = double.Parse(item["HopDongKl"].ToString()),
                    LuyKeYeuCau = double.Parse(item["LuyKeYeuCau"].ToString()),
                    MaVatTu = item["MaVatTu"].ToString(),
                    TenVatTu = item["TenVatTu"].ToString(),
                    DonVi = item["DonVi"].ToString(),
                    CodeTDKH = item["CodeTDKH"].ToString(),
                    CodeHd = item["CodeHd"].ToString(),
                    CodeKHVT = item["CodeKHVT"].ToString(),
                });
            }
            grvDeXuat_SHT.FormatConditions.Clear();
            grvDeXuat_SHT.FormatRules[0].Column = grvDeXuat_SHT.Columns["ConLai"];

            gcDeXuat_SHT.DataSource = VLHT;
            gcDeXuat_SHT.RefreshDataSource();
            gcDeXuat_SHT.Refresh();
        }
        private async void btn_GuiYeuCauVT_Click(object sender, EventArgs e)
        {
            var ngay = "";
            try
            {
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                args.Caption = "Cài đặt ngày yêu cầu vật tư";
                args.Prompt = "Ngày Yêu Cầu";
                args.DefaultButtonIndex = 0;
                //args.Showing += Args_Showing_Begin;
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
            bool isQuyTrinh = SelectDuyet.DuyetTheoQuyTrinh;
            string dbString = "";
            tL_NhapKho.RefreshDataSource();
            List<NhapVatLieu> NVL = tL_NhapKho.DataSource as List<NhapVatLieu>;
            List<VatLieu> VLAll = tL_YeuCauVatTu.DataSource as List<VatLieu>;
            List<VatLieu> VL = VLAll.Where(x => x.Chon && x.MaVatTu != "HM" && x.MaVatTu != "CTR" && x.MaVatTu != "" && x.YeuCauDotNay != 0).ToList();

            if (!VL.Any())
            {
                MessageShower.ShowWarning("Không có đề xuất thảo yêu cầu gửi duyệt;");
                return;
            }
            WaitFormHelper.ShowWaitForm("Quá trình gửi duyệt đang được tiến hành,Vui lòng chờ!");

            int countOK = 0;
            int countBad = 0;
            int count = 1;
            var date = DateTime.Parse(ngay);
            List<string> Noti = new List<string>();
            List<string> LstCode = new List<string>();
            bool IsGiayDuyet = SelectDuyet._CheckPhieuDuyet;
            foreach (VatLieu item in VL)
            {
                WaitFormHelper.ShowWaitForm($"Quá trình gửi duyệt đang được tiến hành,Vui lòng chờ: {count++}/{VL.Count}");

                if (!item.Chon || item.MaVatTu == MyConstant.CONST_TYPE_HANGMUC || item.MaVatTu == MyConstant.CONST_TYPE_CONGTRINH || item.MaVatTu == "" || item.YeuCauDotNay == 0)
                {
                    item.Chon = false;
                    continue;
                }
                item.Chon = false;
                if (IsGiayDuyet)
                    LstCode.Add(item.ID);

                if (isQuyTrinh)
                {
                    var newDXVTHN = new Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgayViewModel()
                    {
                        Code = Guid.NewGuid().ToString(),
                        CodeCha = item.ID,
                        Ngay = date,
                        KhoiLuong = item.YeuCauDotNay,
                        DonGia = item.DonGiaHienTruong,
                        TrangThai = (int)VatTuStateEnum.DangXetDuyet,
                        ACapB = item.ACapB
                    };

                    var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgayViewModel>(RouteAPI.ApprovalYeuCauVatTu_SendApprovalRequest, newDXVTHN);
                    if (result.MESSAGE_TYPECODE)
                    {
                        countOK++;

                    }
                    else
                    {
                        countBad++;
                        Noti.Add($"{item.MaVatTu}: {item.TenVatTu}: {result.MESSAGE_CONTENT}");
                    }
                    if (result.Dto != null)
                    {
                        var cvCha = (new List<Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgayViewModel>() { result.Dto }).fcn_ObjToDataTable();
                        DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(cvCha, Server.Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgay, isCompareTime: false);
                    }

                }
                else
                {
                    dbString = $"INSERT INTO {QLVT.TBL_QLVT_YEUCAUVTKLHN} (\"TrangThai\",\"ACapB\",\"Code\",\"CodeCha\",\"Ngay\",\"KhoiLuong\",\"DonGia\") " +
                        $"VALUES ('{2}','{item.ACapB}','{Guid.NewGuid()}','{item.ID}','{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.YeuCauDotNay}','{item.DonGiaHienTruong}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                    item.YeuCauDotNay = 0;
                    double KLLuyKe = Fcn_CalKLVatTu(item.ID, QLVT.TBL_QLVT_YEUCAUVTKLHN);
                    NhapVatLieu node = NVL.FindAll(x => x.CodeDeXuat == item.ID).FirstOrDefault();

                    if (item.TrangThai == 1)
                    {
                        node.DeXuatVatTu = item.HopDongKl;
                        //node.DeXuatVatTu = KLLuyKe;
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_NHAPVT} SET \"ACapB\"='{item.ACapB}',\"TrangThai\"='{2}',\"DeXuatVatTu\"='{item.HopDongKl}' WHERE \"CodeDeXuat\"='{item.ID}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        item.TrangThai = 2;
                        node.TrangThai = 2;
                        node.TenNhaCungCap = item.TenNhaCungCap;
                        node.DonViThucHien = item.DonViThucHien;
                        node.ACapB = item.ACapB;
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_YEUCAUVT} SET \"ACapB\"='{item.ACapB}',\"TrangThai\"='{2}',\"YeuCauDotNay\"='{0}' WHERE \"Code\"='{item.ID}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                    dbString = $"UPDATE  {QLVT.TBL_QLVT_NHAPVT} SET \"ACapB\"='{item.ACapB}', " +
                        $"\"DonGia\"='{item.DonGiaHienTruong}' WHERE \"CodeDeXuat\"='{item.ID}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    node.DaDuyetDeXuat = KLLuyKe;
                    node.DonGia = item.DonGiaHienTruong;
                }
            }
            tL_YeuCauVatTu.RefreshDataSource();
            if (!isQuyTrinh)
            {
                fcn_updateDeXuatVatLieu();
                MessageShower.ShowInformation("Gửi duyệt thành công!!!!!!!!!!");
            }
            else
            {
                DialogResult drs = DialogResult.None;
                if (countOK == VL.Count)
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
            WaitFormHelper.CloseWaitForm();
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
                string m_Path = Path.Combine(BaseFrom.m_path, "Template", "FileExcel", "19.1PhieuDeXuatVatTu.xlsx");
                SpreadsheetControl Spread = new SpreadsheetControl();
                Spread.LoadDocument(m_Path);
                Worksheet ws = Spread.Document.Worksheets[0];
                dbString = $"SELECT HM.Ten as TenHM,CTR.Ten as TenCTR,NCC.Ten as TenNhaCungCap,DVTH.Ten as TenDonViSuDung,DX.* FROM {QLVT.TBL_QLVT_YEUCAUVT} DX " +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHACUNGCAP} NCC ON NCC.Code=DX.TenNhaCungCap" +
                    $" LEFT JOIN view_DonViThucHien DVTH ON DVTH.Code=DX.DonViThucHien" +
                    $" LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} HM ON HM.Code=DX.CodeHangMuc" +
                    $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} CTR ON CTR.Code=HM.CodeCongTrinh" +
                    $" WHERE DX.Code IN ({MyFunction.fcn_Array2listQueryCondition(LstCode.ToArray())}) ";
                DataTable dt_DX = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} WHERE \"Code\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                List<Tbl_ThongTinDuAnViewModel> lst = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThongTinDuAnViewModel>(dbString);
                Spread.BeginUpdate();
                ws.Rows[2]["E"].SetValueFromText($"Dự án: {lst.SingleOrDefault().TenDuAn}");
                ws.Rows[3]["E"].SetValueFromText($"Địa điểm: {lst.SingleOrDefault().DiaChi}");
                ws.Rows[4]["E"].SetValueFromText($"Người gửi: {BaseFrom.BanQuyenKeyInfo.FullName}");
                ws.Rows[2]["Q"].SetValueFromText($"Ngày {date.Day} tháng {date.Month} năm {date.Year}");
                int i = 7, STT = 1;
                var grctr = dt_DX.AsEnumerable().GroupBy(x => x["TenCTR"].ToString());
                foreach (var CTR in grctr)
                {
                    Row Crow = ws.Rows[i++];
                    ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                    Crow.Font.Color = MyConstant.color_Row_CongTrinh;
                    Crow.Font.Bold = true;
                    Crow["I"].SetValueFromText(CTR.Key);
                    var grHM = CTR.GroupBy(x => x["TenHM"].ToString());
                    foreach (var HM in grHM)
                    {
                        Crow = ws.Rows[i++];
                        ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                        Crow.Font.Color = MyConstant.color_Row_HangMuc;
                        Crow.Font.Bold = true;
                        Crow["I"].SetValueFromText(HM.Key);
                        var grVL = CTR.GroupBy(x => x["Code"].ToString());
                        foreach (var item in grVL)
                        {
                            var row = item.FirstOrDefault();
                            dbString = $"SELECT CASE WHEN YCHN.TrangThai=1 THEN 'Đang xét duyệt' ELSE 'Đã duyệt' END AS TrangThai,YCHN.* " +
                                $"FROM {QLVT.TBL_QLVT_YEUCAUVTKLHN} YCHN WHERE YCHN.CodeCha='{row["Code"]}' AND YCHN.Ngay='{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
                            DataTable dthn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            if (dthn.Rows.Count == 0)
                                continue;
                            Crow = ws.Rows[i++];
                            ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                            Crow["D"].SetValue(STT++);
                            Crow["E"].SetValueFromText(row["TenNhaCungCap"].ToString());
                            Crow["F"].SetValueFromText(row["TenDonViSuDung"].ToString());
                            Crow["I"].SetValueFromText(row["TenVatTu"].ToString());
                            Crow["J"].SetValueFromText(row["DonVi"].ToString());
                            Crow["K"].SetValue(dthn.AsEnumerable().FirstOrDefault()["KhoiLuong"]);
                            Crow["H"].SetValue(dthn.AsEnumerable().FirstOrDefault()["TrangThai"]);
                            Crow["M"].SetValue(dthn.AsEnumerable().FirstOrDefault()["DonGia"]);
                            Crow["Q"].Formula = $"={Crow["K"].GetReferenceA1()}*{Crow["M"].GetReferenceA1()}";

                        }

                    }
                }
                CellRange NguoiDx = ws.Range["NguoiDeXuat"];
                ws.Rows[NguoiDx.BottomRowIndex][NguoiDx.RightColumnIndex].SetValueFromText($"{BaseFrom.BanQuyenKeyInfo.FullName}");
                Spread.EndUpdate();
                Spread.Document.History.IsEnabled = true;
                string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
                Spread.SaveDocument(Path.Combine(PathSave, $"Phiếu đề xuất Vật tư_{time}.xlsx"), DocumentFormat.Xlsx);
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowInformation("Xuất File thành công!");
                DialogResult dialogResult = XtraMessageBox.Show($"[Phiếu đề xuất Vật tư_{time}.xlsx] thành công. Bạn có muốn mở file không???", "QUẢN LÝ THI CÔNG - THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Process.Start(Path.Combine(PathSave, $"Phiếu đề xuất Vật tư_{time}.xlsx"));
                }
                WaitFormHelper.CloseWaitForm();

            }
        }
        private void fcn_loadYeuCauVT_Thucong_TDKH(bool Type, bool HD, bool LoadData, string m_codecheck, string n_codecheck)
        {
            if (SharedControls.slke_ThongTinDuAn is null)
                return;
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu đề xuất vật liệu", "Vui Lòng chờ!");
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
            //tL_YeuCauVatTu.Columns["CodeHd"].Visible = false;
            string dbString = "";
            if (Type)
                dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {m_codecheck} IS NULL AND \"CodeHd\"IS NULL AND {n_codecheck} IS NULL ";//AND \"IsDone\"='{0}'
            else
                dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {m_codecheck} IS NOT NULL AND \"CodeHd\" IS NULL ";//AND \"IsDone\"='{0}'

            if (HD)
            {
                dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND \"CodeHd\" IS NOT NULL ";//AND \"IsDone\"='{0}'
                //tL_YeuCauVatTu.Columns["CodeHd"].Visible = true;
            }
            DataTable dt_yeucau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //string lsCodeCT = MyFunction.fcn_Array2listQueryCondition(dt_yeucau.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            //dbString = $"SELECT * FROM {QLVT.TBL_QLVT_NHAPVT} WHERE \"CodeDeXuat\" IN ({lsCodeCT})";
            //DataTable dt_NhapVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //dbString = $"SELECT * FROM {QLVT.TBL_QLVT_XUATVT} WHERE \"CodeDeXuat\" IN ({lsCodeCT})";
            //DataTable dt_XuatVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //string lsCodeCVT = MyFunction.fcn_Array2listQueryCondition(dt_NhapVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            //dbString = $"SELECT * FROM {QLVT.TBL_QLVT_CHUYENKHOVT} WHERE \"CodeNhapVT\" IN ({lsCodeCVT})";
            //DataTable dt_CVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //dbString = $"SELECT * FROM {QLVT.TBL_QLVT_QLVC} WHERE \"CodeNhapVT\" IN ({lsCodeCVT})";
            //DataTable dt_QLVC = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<VatLieu> VatLieu = new List<VatLieu>();
            List<VatLieu> VatLieuAdd = new List<VatLieu>();

            foreach (DataRow CT in dtCT.Rows)
            {
                string crCodeCT = CT["Code"].ToString();
                VatLieu.Add(new VatLieu()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaVatTu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenVatTu = CT["Ten"].ToString(),
                });


                foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                {
                    string crCodeHM = HM["Code"].ToString();
                    VatLieu.Add(new VatLieu()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaVatTu = MyConstant.CONST_TYPE_HANGMUC,
                        TenVatTu = HM["Ten"].ToString(),
                    });
                    DataRow[] dt_vl = dt_yeucau.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM && x["IsDone"].ToString() == "False").ToArray();
                    if (dt_vl.Count() != 0)
                    {
                        DataTable dt_VatLieu = dt_vl.CopyToDataTable();
                        dt_VatLieu.Columns["Code"].ColumnName = "ID";
                        VatLieuAdd = DuAnHelper.ConvertToList<VatLieu>(dt_VatLieu);
                        foreach (VatLieu rowitem in VatLieuAdd)
                        {
                            rowitem.ParentID = crCodeHM;
                            double KLLuyKe = Fcn_CalKLVatTu(rowitem.ID, QLVT.TBL_QLVT_YEUCAUVTKLHN);
                            rowitem.LuyKeYeuCau = KLLuyKe;
                            if (rowitem.YeuCauDotNay != 0)
                            {
                                rowitem.LuyKeYeuCau = KLLuyKe + rowitem.YeuCauDotNay;
                            }
                        }
                        VatLieu.AddRange(VatLieuAdd);
                    }
                    //foreach (var item in dt_vl)
                    //{
                    //    if (item["IsDone"].ToString() == "True")
                    //        continue;
                    //    VatLieu.Add(new VatLieu()
                    //    {
                    //        ParentID = crCodeHM,
                    //        CodeHd=item["CodeHd"].ToString(),
                    //        ID = item["Code"].ToString(),
                    //        TrangThai = item["TrangThai"].ToString() != "1" ? 2 : 1,
                    //        TenNhaCungCap= item["TenNhaCungCap"].ToString(),
                    //        MaVatTu = item["MaVatTu"].ToString(),
                    //        TenVatTu = item["TenVatTu"].ToString(),
                    //        DonVi = item["DonVi"].ToString(),
                    //        YeuCauDotNay= item["YeuCauDotNay"] == DBNull.Value ? 0 : Math.Round(double.Parse(item["YeuCauDotNay"].ToString()), 3),
                    //        HopDongKl = item["HopDongKl"]==DBNull.Value ? 0 : Math.Round(double.Parse(item["HopDongKl"].ToString()),3),
                    //        LuyKeYeuCau = item["LuyKeYeuCau"]==DBNull.Value ? 0 : Math.Round(double.Parse(item["LuyKeYeuCau"].ToString()), 3),
                    //        LuyKeXuatKho = item["LuyKeXuatKho"]== DBNull.Value ? 0 : Math.Round(double.Parse(item["LuyKeXuatKho"].ToString()), 3),
                    //        Chon = false,                          
                    //        DonGiaHienTruong = item["DonGiaHienTruong"]== DBNull.Value ? 0 : Math.Round(double.Parse(item["DonGiaHienTruong"].ToString()), 3)
                    //    });
                    //}
                    if (Type)
                        VatLieu.Add(new VatLieu()
                        {
                            ParentID = crCodeHM,
                            ID = Guid.NewGuid().ToString(),
                        });

                }
            }
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, true);
            //dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"CodeNcc\"=\"CodeDonViThucHien\" AND \"CodeDuAn\"='{slke_ThongTinDuAn.EditValue}'";
            //DataTable dt_hd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //List<Infor_HopDong> LstHD = DuAnHelper.ConvertToList<Infor_HopDong>(dt_hd);
            //dbString = $"SELECT \"Code\",\"Ten\" FROM {MyConstant.TBL_THONGTINNHACUNGCAP} WHERE \"CodeDuAn\"='{slke_ThongTinDuAn.EditValue}'";
            //DataTable dt_ncc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //riLUE_NhaCungCap.DataSource= DuAnHelper.ConvertToList<Infor>(dt_ncc);
            //rILUE_TenHopDong.DataSource = LstHD;

            //rILUE_TenKhoChuyenKho.DataSource = Infor;
            //rILUE_TenKhoChuyenKho.DropDownRows = Infor.Count;
            tL_YeuCauVatTu.DataSource = VatLieu;
            tL_YeuCauVatTu.RefreshDataSource();
            tL_YeuCauVatTu.Refresh();
            tL_YeuCauVatTu.OptionsBehavior.Editable = true;

            tL_YeuCauVatTu.FormatConditions.Clear();
            tL_YeuCauVatTu.FormatRules[0].Column = tL_YeuCauVatTu.Columns["ConLai"];
            tL_YeuCauVatTu.ExpandAll();
            UpDateTenKho();
            if (!LoadData)
            {
                WaitFormHelper.CloseWaitForm();
                return;
            }
            Fcn_LoadDataNhapKho(dtCT, dtHM);
            Fcn_LoadDataXuatKho(dtCT, dtHM, Infor);
            Fcn_LoadDataTongHop(dtCT, dtHM, Infor);
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_LoadDataTongHop(DataTable dtCT, DataTable dtHM, List<InforCT_HM> Infor, DateTime? NBD = null, DateTime? NKT = null)
        {
            string dbString = $"SELECT YCVT.*,NVT.Code as CodeNVT,XVT.Code as CodeXVT,QLVC.Code as CodeQLVC," +
                $"CK.Code as CodeCK,CK.TenKhoChuyenDen,CK.TenKhoChuyenDi,XVT.KhoiLuongXuatThucTe,XVT.TonKhoThucTe,XVT.DonGiaKiemSoat FROM {QLVT.TBL_QLVT_YEUCAUVT} YCVT " +
        $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVT} NVT " +
        $"ON NVT.CodeDeXuat=YCVT.Code " +
        $"LEFT JOIN {QLVT.TBL_QLVT_XUATVT} XVT " +
        $"ON XVT.CodeDeXuat=YCVT.Code " +
        $"LEFT JOIN {QLVT.TBL_QLVT_CHUYENKHOVT} CK " +
        $"ON CK.CodeNhapVT=NVT.Code " +
        $"LEFT JOIN {QLVT.TBL_QLVT_QLVC} QLVC " +
        $"ON QLVC.CodeNhapVT=NVT.Code " +
        $" WHERE YCVT.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            DataTable dt_TH = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<TongHop> T_hop = new List<TongHop>();
            double DonGia = 0, LuyKeYeuCau = 0, LuyKeVanChuyenTheoDot = 0, LuyKeNhapTheoDot = 0, LuyKeXuatTheoDot = 0, TonKhoChuyenDi = 0;
            foreach (DataRow CT in dtCT.Rows)
            {
                string crCodeCT = CT["Code"].ToString();

                T_hop.Add(new TongHop()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaVatTu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenVatTu = CT["Ten"].ToString(),
                    //TenKhoNhap = ""
                });
                foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                {
                    string crCodeHM = HM["Code"].ToString();
                    T_hop.Add(new TongHop()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaVatTu = MyConstant.CONST_TYPE_HANGMUC,
                        TenVatTu = HM["Ten"].ToString(),
                        //TenKhoNhap = ""
                    });

                    DataRow[] dt_vl = dt_TH.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                    foreach (var item in dt_vl)
                    {
                        DonGia = item["DonGiaHienTruong"].ToString() == "" ? 0 : Math.Round(double.Parse(item["DonGiaHienTruong"].ToString()), 2);

                        LuyKeYeuCau = Fcn_CalKLVatTu(item["Code"].ToString(), QLVT.TBL_QLVT_YEUCAUVTKLHN, NBD, NKT);
                        //LuyKeVanChuyenTheoDot = item["LuyKeVanChuyenTheoDot"].ToString() == "" ? 0 : Math.Round(double.Parse(item["LuyKeVanChuyenTheoDot"].ToString()), 3);
                        LuyKeVanChuyenTheoDot = Fcn_CalKLVatTu(item["CodeQLVC"].ToString(), QLVT.TBL_QLVT_NKVC, NBD, NKT); ;
                        //TonKhoChuyenDi = item["TonKhoChuyenDi"].ToString() == "" ? 0 : Math.Round(double.Parse(item["TonKhoChuyenDi"].ToString()), 2);
                        //LuyKeXuatTheoDot = item["LuyKeXuatTheoDot"].ToString() == "" ? 0 : Math.Round(double.Parse(item["LuyKeXuatTheoDot"].ToString()), 2);
                        LuyKeXuatTheoDot = Fcn_CalKLVatTu(item["CodeXVT"].ToString(), QLVT.TBL_QLVT_NHAPVTKLHN, NBD, NKT);
                        //LuyKeNhapTheoDot = item["LuyKeNhapTheoDot"].ToString() == "" ? 0 : Math.Round(double.Parse(item["LuyKeNhapTheoDot"].ToString()), 2);
                        LuyKeNhapTheoDot = Fcn_CalKLVatTu(item["CodeNVT"].ToString(), QLVT.TBL_QLVT_NHAPVTKLHN, NBD, NKT);
                        double KLLuyKeChuyeKho = Fcn_CalKLVatTu(item["CodeCK"].ToString(), QLVT.TBL_QLVT_CHUYENKHOVTKLHN, NBD, NKT);
                        TonKhoChuyenDi = LuyKeNhapTheoDot - KLLuyKeChuyeKho - LuyKeXuatTheoDot;
                        T_hop.Add(new TongHop()
                        {
                            ParentID = crCodeHM,
                            ID = Guid.NewGuid().ToString(),
                            MaVatTu = item["MaVatTu"].ToString(),
                            TenVatTu = item["TenVatTu"].ToString(),
                            DonVi = item["DonVi"].ToString(),
                            TenKhoChuyenDen = item["TenKhoChuyenDen"].ToString(),
                            TenKhoChuyenDi = item["TenKhoChuyenDi"].ToString(),
                            HopDongKl = item["HopDongKl"].ToString() == "" ? 0 : Math.Round(double.Parse(item["HopDongKl"].ToString()), 3),
                            LuyKeYeuCau = LuyKeYeuCau,
                            LuyKeVanChuyenTheoDot = LuyKeVanChuyenTheoDot,
                            TonKhoChuyenDi = TonKhoChuyenDi,
                            //TonKhoChuyenDen = item["TonKhoChuyenDen"].ToString() == "" ? 0 : Math.Round(double.Parse(item["TonKhoChuyenDen"].ToString()), 3),
                            LuyKeXuatTheoDot = LuyKeXuatTheoDot,
                            LuyKeNhapTheoDot = LuyKeNhapTheoDot,
                            KhoiLuongXuatThucTe = item["KhoiLuongXuatThucTe"].ToString() == "" ? 0 : Math.Round(double.Parse(item["KhoiLuongXuatThucTe"].ToString()), 3),
                            TonKhoThucTe = item["TonKhoThucTe"].ToString() == "" ? 0 : Math.Round(double.Parse(item["TonKhoThucTe"].ToString()), 3),
                            Chon = false,
                            CodeDeXuat = item["Code"].ToString(),
                            CodeHd = item["CodeHd"].ToString(),
                            CodeKHVT = item["CodeKHVT"].ToString(),
                            CodeTDKH = item["CodeTDKH"].ToString(),
                            //DonGiaYC = item["DonGiaHienTruong"].ToString() == "" ? 0 : Math.Round(double.Parse(item["DonGiaHienTruong"].ToString()), 3),
                            //DonGiaNK = item["DonGia"].ToString() == "" ? 0 : Math.Round(double.Parse(item["DonGia"].ToString()), 3),
                            //DonGiaXK = item["DonGia"].ToString() == "" ? 0 : Math.Round(double.Parse(item["DonGia"].ToString()), 3),
                            //DonGiaVC = item["DonGia"].ToString() == "" ? 0 : Math.Round(double.Parse(item["DonGia"].ToString()), 3),
                            DonGiaKiemSoat = item["DonGiaKiemSoat"].ToString() == "" ? 0 : Math.Round(double.Parse(item["DonGiaKiemSoat"].ToString()), 3),
                            ThanhTienYC = Math.Round(LuyKeYeuCau * DonGia),
                            ThanhTienVC = Math.Round(DonGia * LuyKeVanChuyenTheoDot),
                            ThanhTienXK = Math.Round(DonGia * LuyKeXuatTheoDot),
                            ThanhTienNK = Math.Round(DonGia * LuyKeNhapTheoDot),
                            ThanhTienCK = DonGia * (DonGia * TonKhoChuyenDi),
                            DonGiaYC = item["DonGiaHienTruong"].ToString() == "" ? 0 : Math.Round(double.Parse(item["DonGiaHienTruong"].ToString()), 3)
                        });
                    }
                }
            }
            //rILUE_TenKhoTH.DataSource = Infor;
            //rILUE_TenKhoTH.DropDownRows = Infor.Count;
            tL_QLVC_TongHop.Refresh();
            tL_QLVC_TongHop.DataSource = T_hop;
            tL_QLVC_TongHop.ExpandAll();
            //tL_NhapKho.OptionsBehavior.Editable = true;
        }
        private void Fcn_LoadDataNhapKho(DataTable dtCT, DataTable dtHM)
        {
            string dbString = $"SELECT * FROM {QLVT.TBL_QLVT_NHAPVT} " +
                 $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
                 $"ON {QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat={QLVT.TBL_QLVT_YEUCAUVT}.Code " +
                $" WHERE {QLVT.TBL_QLVT_NHAPVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            DataTable dt_NhapVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lsCodeCVT = MyFunction.fcn_Array2listQueryCondition(dt_NhapVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            dbString = $"SELECT * FROM {QLVT.TBL_QLVT_QLVC} WHERE \"CodeNhapVT\" IN ({lsCodeCVT})";
            DataTable dt_QLVC = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<NhapVatLieu> NVL = new List<NhapVatLieu>();
            List<NhapVatLieu> NVLADD = new List<NhapVatLieu>();
            foreach (DataRow CT in dtCT.Rows)
            {
                string crCodeCT = CT["Code"].ToString();

                NVL.Add(new NhapVatLieu()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaVatTu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenVatTu = CT["Ten"].ToString(),
                    TenKhoNhap = "",

                });
                foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                {
                    string crCodeHM = HM["Code"].ToString();
                    NVL.Add(new NhapVatLieu()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaVatTu = MyConstant.CONST_TYPE_HANGMUC,
                        TenVatTu = HM["Ten"].ToString(),
                        TenKhoNhap = ""
                    });
                    DataRow[] dt_nvt = dt_NhapVT.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                    if (dt_nvt.Count() != 0)
                    {
                        DataTable dt_nvl = dt_nvt.CopyToDataTable();
                        dt_nvl.Columns["Code"].ColumnName = "ID";
                        NVLADD = DuAnHelper.ConvertToList<NhapVatLieu>(dt_nvl);
                        foreach (NhapVatLieu rowitem in NVLADD)
                        {
                            DataRow RowVC = dt_QLVC.AsEnumerable().Where(x => x["CodeNhapVT"].ToString() == rowitem.ID).FirstOrDefault();
                            double KLVC = RowVC is null ? 0 : Fcn_CalKLVatTu(RowVC["Code"].ToString(), QLVT.TBL_QLVT_NKVC);
                            rowitem.ParentID = crCodeHM;
                            double KLLuyKe = Fcn_CalKLVatTu(rowitem.ID, QLVT.TBL_QLVT_NHAPVTKLHN);
                            double DaDuyetDeXuat = Fcn_CalKLVatTu(rowitem.CodeDeXuat, QLVT.TBL_QLVT_YEUCAUVTKLHN);
                            rowitem.DaDuyetDeXuat = DaDuyetDeXuat;
                            if (rowitem.ThucNhap != 0)
                            {
                                rowitem.LuyKeNhapTheoDot = KLVC + KLLuyKe + rowitem.ThucNhap;
                            }
                        }
                        NVL.AddRange(NVLADD);
                    }
                }
            }
            tL_NhapKho.DataSource = NVL;
            tL_NhapKho.RefreshDataSource();
            tL_NhapKho.Refresh();
            tL_NhapKho.ExpandAll();
        }
        private void Fcn_LoadDataXuatKho(DataTable dtCT, DataTable dtHM, List<InforCT_HM> Infor)
        {
            string dbString = $"SELECT * FROM {QLVT.TBL_QLVT_XUATVT} " +
                 $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
                 $"ON {QLVT.TBL_QLVT_XUATVT}.CodeDeXuat={QLVT.TBL_QLVT_YEUCAUVT}.Code " +
                 $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVT} " +
                 $"ON {QLVT.TBL_QLVT_XUATVT}.CodeNhapVT={QLVT.TBL_QLVT_NHAPVT}.Code " +
                $" WHERE {QLVT.TBL_QLVT_XUATVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            DataTable dt_XuatVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lsCodeCVT = MyFunction.fcn_Array2listQueryCondition(dt_XuatVT.AsEnumerable().Select(x => x["CodeNhapVT"].ToString()).ToArray());
            dbString = $"SELECT * FROM {QLVT.TBL_QLVT_CHUYENKHOVT} WHERE \"CodeNhapVT\" IN ({lsCodeCVT})";
            DataTable dt_CVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {QLVT.TBL_QLVT_QLVC} WHERE \"CodeNhapVT\" IN ({lsCodeCVT})";
            DataTable dt_QLVC = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<XuatVatLieu> XVL = new List<XuatVatLieu>();
            List<ChuyenVatTu> CVL = new List<ChuyenVatTu>();
            List<PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC> QLVC = new List<PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC>();
            double KLVC = 0;
            foreach (DataRow CT in dtCT.Rows)
            {
                string crCodeCT = CT["Code"].ToString();

                XVL.Add(new XuatVatLieu()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaVatTu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenVatTu = CT["Ten"].ToString(),
                    TenKhoNhap = ""
                });

                CVL.Add(new ChuyenVatTu()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaVatTu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenVatTu = CT["Ten"].ToString(),
                    TenKhoChuyenDi = "",
                    TenKhoChuyenDen = "",
                });
                QLVC.Add(new PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaVatTu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenVatTu = CT["Ten"].ToString(),
                });
                foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                {
                    string crCodeHM = HM["Code"].ToString();
                    XVL.Add(new XuatVatLieu()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaVatTu = MyConstant.CONST_TYPE_HANGMUC,
                        TenVatTu = HM["Ten"].ToString(),
                        TenKhoNhap = ""
                    });
                    CVL.Add(new ChuyenVatTu()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaVatTu = MyConstant.CONST_TYPE_HANGMUC,
                        TenVatTu = HM["Ten"].ToString(),
                        TenKhoChuyenDi = "",
                        TenKhoChuyenDen = ""
                    });
                    QLVC.Add(new PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaVatTu = MyConstant.CONST_TYPE_HANGMUC,
                        TenVatTu = HM["Ten"].ToString(),
                    });
                    DataRow[] dt_xvt = dt_XuatVT.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                    foreach (var row in dt_xvt)
                    {
                        double KLLuyKeNhap = Fcn_CalKLVatTu(row["CodeNhapVT"].ToString(), QLVT.TBL_QLVT_NHAPVTKLHN);
                        double KLLuyKeXuat = Fcn_CalKLVatTu(row["Code"].ToString(), QLVT.TBL_QLVT_XUATVTKLHN);
                        double KLLuyKeDeXuat = Fcn_CalKLVatTu(row["CodeDeXuat"].ToString(), QLVT.TBL_QLVT_YEUCAUVTKLHN);
                        KLVC = 0;
                        XVL.Add(new XuatVatLieu()
                        {
                            ParentID = crCodeHM,
                            CodeDeXuat = row["CodeDeXuat"].ToString(),
                            CodeNhapVT = row["CodeNhapVT"].ToString(),
                            ID = row["Code"].ToString(),
                            DonGia = row["DonGia"].ToString() == "" ? 0 : int.Parse(row["DonGia"].ToString()),
                            CodeHd = row["CodeHd"].ToString(),
                            CodeKHVT = row["CodeKHVT"].ToString(),
                            CodeTDKH = row["CodeTDKH"].ToString(),
                            MaVatTu = row["MaVatTu"].ToString(),
                            TenVatTu = row["TenVatTu"].ToString(),
                            IsXuat = (bool)row["IsXuat"],
                            DonVi = row["DonVi"].ToString(),
                            TenKhoNhap = row["TenKhoNhap"].ToString(),
                            DaDuyetDeXuat = Math.Round(KLLuyKeDeXuat, 4),
                            LuyKeNhapTheoDot = Math.Round(KLLuyKeNhap, 4),
                            LuyKeXuatTheoDot = Math.Round(KLLuyKeXuat, 4),
                            ThucXuat = row["ThucXuat"].ToString() == "" ? 0 : Math.Round(double.Parse(row["ThucXuat"].ToString()), 3),
                            TonKhoThucTe = row["TonKhoThucTe"].ToString() == "" ? 0 : Math.Round(double.Parse(row["TonKhoThucTe"].ToString()), 3),
                            KhoiLuongXuatThucTe = row["KhoiLuongXuatThucTe"].ToString() == "" ? 0 : Math.Round(double.Parse(row["KhoiLuongXuatThucTe"].ToString()), 3),
                            DonGiaKiemSoat = row["DonGiaKiemSoat"].ToString() == "" ? 0 : Math.Round(double.Parse(row["DonGiaKiemSoat"].ToString()), 2),
                            TrangThai = (row["TrangThai"].ToString() == "1") ? 1 : 2,
                            ACapB = row["ACapB"] != DBNull.Value ? bool.Parse(row["ACapB"].ToString()) : false,
                            FileDinhKem = "Xem File"
                        });
                        DataRow[] _cvt = dt_CVT.AsEnumerable().Where(x => x["CodeNhapVT"].ToString() == row["CodeNhapVT"].ToString()).ToArray();
                        DataRow[] _qlvc = dt_QLVC.AsEnumerable().Where(x => x["CodeNhapVT"].ToString() == row["CodeNhapVT"].ToString()).ToArray();
                        foreach (var rowqlvt in _qlvc)
                        {
                            KLVC = Fcn_CalKLVatTu(rowqlvt["Code"].ToString(), QLVT.TBL_QLVT_NKVC);
                            QLVC.Add(new PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC
                            {
                                ID = rowqlvt["Code"].ToString(),
                                ParentID = crCodeHM,
                                NhatKy = "Xem Nhật Ký",
                                CodeDeXuat = row["CodeDeXuat"].ToString(),
                                CodeNhapVT = row["CodeNhapVT"].ToString(),
                                MaVatTu = row["MaVatTu"].ToString(),
                                TenVatTu = row["TenVatTu"].ToString(),
                                DonVi = row["DonVi"].ToString(),
                                CodeHd = row["CodeHd"].ToString(),
                                CodeKHVT = row["CodeKHVT"].ToString(),
                                CodeTDKH = row["CodeTDKH"].ToString(),
                                LuyKeVanChuyenTheoDot = KLVC,
                                DaDuyetDeXuat = KLLuyKeDeXuat,
                                KhoiLuongDaNhap = KLLuyKeNhap,
                                FileDinhKem = "Xem File"
                            });
                        }
                        foreach (var rowcvt in _cvt)
                        {
                            double KLLuyKeChuyeKho = Fcn_CalKLVatTu(rowcvt["Code"].ToString(), QLVT.TBL_QLVT_CHUYENKHOVTKLHN);
                            CVL.Add(new ChuyenVatTu
                            {
                                ID = rowcvt["Code"].ToString(),
                                TenKhoChuyenDen = rowcvt["TenKhoChuyenDen"].ToString(),
                                TenKhoChuyenDi = rowcvt["TenKhoChuyenDi"].ToString(),
                                ParentID = crCodeHM,
                                HoanThanh = false,
                                CodeNhapVT = row["CodeNhapVT"].ToString(),
                                CodeDeXuat = row["CodeDeXuat"].ToString(),
                                MaVatTu = row["MaVatTu"].ToString(),
                                TenVatTu = row["TenVatTu"].ToString(),
                                DonVi = row["DonVi"].ToString(),
                                CodeHd = row["CodeHd"].ToString(),
                                CodeKHVT = row["CodeKHVT"].ToString(),
                                CodeTDKH = row["CodeTDKH"].ToString(),
                                LuyKeThucNhapKhoDen = KLLuyKeChuyeKho,
                                TonKhoChuyenDi = KLLuyKeNhap - KLLuyKeChuyeKho - KLLuyKeXuat,
                            });
                        }

                    }
                }
            }

            tL_XuatKho.Refresh();
            tL_XuatKho.DataSource = XVL;
            tL_XuatKho.OptionsBehavior.Editable = true;

            tL_ChuyenKho.DataSource = CVL;
            tL_ChuyenKho.RefreshDataSource();
            tL_ChuyenKho.Refresh();
            tL_ChuyenKho.ExpandAll();

            tL_QLVC.DataSource = QLVC;
            tL_QLVC.OptionsBehavior.Editable = true;
            tL_QLVC.RefreshDataSource();
            tL_QLVC.Refresh();
            tL_QLVC.ExpandAll();
        }
        public void UpDateTenKho()
        {
            string dbString = $"SELECT QLVT.*,QLVT.Code as ID,COALESCE(QLVT.Ten, DA.TenDuAn,CT.Ten,HM.Ten,KC.Ten) AS TenKho," +
               $"COALESCE(DA.TenDuAn, DACT.TenDuAn,DAHM.TenDuAn) AS TenDuAn " +
               $" FROM {QLVT.Tbl_QLVT_TenKhoTheoDuAn} QLVT" +
               $" LEFT JOIN {MyConstant.TBL_THONGTINDUAN} DA ON DA.Code=QLVT.CodeDuAn " +
               $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} CT ON CT.Code=QLVT.CodeCongTrinh " +
               $" LEFT JOIN {MyConstant.TBL_THONGTINDUAN} DACT ON DACT.Code=CT.CodeDuAn " +
               $" LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} HM ON HM.Code=QLVT.CodeHangMuc " +
               $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} CTHM ON CTHM.Code=HM.CodeCongTrinh " +
               $" LEFT JOIN {MyConstant.TBL_THONGTINDUAN} DAHM ON DAHM.Code=CTHM.CodeDuAn " +
               $" LEFT JOIN {QLVT.Tbl_QLVT_TenKhoChung} KC ON KC.Code=QLVT.CodeKhoChung ";
            List<Infor_TenKho> Infor = DataProvider.InstanceTHDA.ExecuteQueryModel<Infor_TenKho>(dbString);

            //List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, true);
            dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"CodeNcc\"=\"CodeDonViThucHien\" AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_hd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<Infor_HopDong> LstHD = DuAnHelper.ConvertToList<Infor_HopDong>(dt_hd);
            dbString = $"SELECT \"Code\",\"Ten\" FROM {MyConstant.TBL_THONGTINNHACUNGCAP} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_ncc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            riLUE_NhaCungCap.DataSource = DuAnHelper.ConvertToList<Infor>(dt_ncc);
            rILUE_TenHopDong.DataSource = LstHD;

            rILUE_TenKhoChuyenKho.DataSource = Infor;
            rILUE_TenKhoChuyenKho.DropDownRows = Infor.Count;
            //NhapKho
            rILUE_TenNhaCungCap.DataSource = DuAnHelper.ConvertToList<Infor>(dt_ncc);
            rILETenKho.DataSource = Infor;
            rILETenKho.DropDownRows = Infor.Count;
            rILUE_NhapKho.DataSource = Infor;
            rILUE_NhapKho.DropDownRows = Infor.Count;
            rILUE_TenKhoTH.DataSource = Infor;
            rILUE_TenKhoTH.DropDownRows = Infor.Count;
            rILUE_KhoDen.DataSource = Infor;
            rILUE_KhoDen.DropDownRows = Infor.Count;
            rILUE_KhoDi.DataSource = Infor;
            rILUE_KhoDi.DropDownRows = Infor.Count;
            rlue_TenKhoTra.DataSource = Infor;
            rlue_TenKhoTra.DropDownRows = Infor.Count;
            List<InForToChuc_CaNhan> DVTH = DuAnHelper.GetCaNhanToChuc(false, false, true);
            rILUE_DVTH.DataSource = DVTH;
            rILUE_NhapKhoDVTH.DataSource = DVTH;
        }
        private void Fcn_UpdateTenKho()
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            string dbString = $"SELECT * FROM {QLVT.TBL_QLVT_CHUYENKHOVTKLHN} WHERE '{firstDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' <=\"Ngay\"<='{lastDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' ";
            DataTable dt_CVTHN = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lstCode = MyFunction.fcn_Array2listQueryCondition(dt_CVTHN.AsEnumerable().Select(x => x["CodeCha"].ToString()).Distinct().ToArray());
            string[] TenKho = dt_CVTHN.AsEnumerable().Select(x => x["TenKhoChuyenDen"].ToString()).Distinct().ToArray();
            dbString = $"SELECT * FROM {QLVT.TBL_QLVT_CHUYENKHOVT} WHERE \"Code\" IN ({lstCode})";
            DataTable dt_CVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            int stt = 1;
            List<VatLieuHoanThanh> TenKhoDen = new List<VatLieuHoanThanh>();
            List<VatLieuHoanThanh> TenKhoDi = new List<VatLieuHoanThanh>();
            foreach (string item in TenKho)
            {
                TenKhoDen.Add(new VatLieuHoanThanh
                {
                    STT = stt++,
                    KhoDen = item
                });
            }
            stt = 1;
            foreach (DataRow row in dt_CVT.Rows)
            {
                TenKhoDi.Add(new VatLieuHoanThanh
                {
                    STT = stt++,
                    KhoDi = row["TenKhoChuyenDi"].ToString()
                });
            }
            gc_TenKhoDen.DataSource = TenKhoDen;
            gc_TenKhoDi.DataSource = TenKhoDi;
        }
        private void fcn_UpdateNhapKho()
        {
            List<NhapVatLieu> VL = (tL_NhapKho.DataSource as List<NhapVatLieu>).Where(x => x.CodeDeXuat != null).ToList();
            string CodeDX = MyFunction.fcn_Array2listQueryCondition(VL.Where(x => x.CodeDeXuat != null).Select(x => x.CodeDeXuat).ToArray());
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            string dbString = $"SELECT {QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu,{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_YEUCAUVT}.MaVatTu,{QLVT.TBL_QLVT_NHAPVTKLHN}.KhoiLuong " +
                $" FROM {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"INNER JOIN {QLVT.TBL_QLVT_NHAPVT} " +
                $"ON {QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat = {QLVT.TBL_QLVT_YEUCAUVT}.Code " +
                $"INNER JOIN {QLVT.TBL_QLVT_NHAPVTKLHN} " +
                $"ON {QLVT.TBL_QLVT_NHAPVTKLHN}.CodeCha = {QLVT.TBL_QLVT_NHAPVT}.Code " +
                $" WHERE {QLVT.TBL_QLVT_YEUCAUVT}.Code IN ({CodeDX}) AND '{firstDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' <={QLVT.TBL_QLVT_NHAPVTKLHN}.Ngay<='{lastDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            DataTable dt_NVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] MaVatLieu = VL.Select(x => x.MaVatTu).ToArray();
            int stt = 1;

            List<VatLieuHoanThanh> VLHT = new List<VatLieuHoanThanh>();
            foreach (NhapVatLieu item in VL)
            {
                //DataRow dtrow = Dt.NewRow();
                //dtrow["STT"] = stt++;
                //dtrow["VatTu"] = item.TenVatTu;
                //dtrow["DonVi"] = item.DonVi;
                //dtrow["MaVatTu"] = item.MaVatTu;
                DataRow[] crRow = dt_NVT.AsEnumerable().Where(x => x["MaVatTu"].ToString() == item.MaVatTu).ToArray();
                //foreach (var row in crRow)
                //    KL += double.Parse(row["KhoiLuong"].ToString());
                double KL = crRow.AsEnumerable().Sum(x => double.Parse(x["KhoiLuong"].ToString()));
                if (KL == 0)
                    continue;
                VLHT.Add(new VatLieuHoanThanh
                {
                    STT = stt++,
                    MaVatTu = item.MaVatTu,
                    TenVatTu = item.TenVatTu,
                    DonVi = item.DonVi,
                    KhoiLuong = KL
                });
            }
            gc_VatLieuNhapTrongThang.DataSource = VLHT;
        }
        private void Fcn_UpdateQLVT_TDKH(List<TCTU_TDKH> ThuChiTDKH, DonViThucHien DVTH)
        {
            //tL_YeuCauVatTu.DeleteSelectedNodes();
            List<VatLieu> VL = tL_YeuCauVatTu.DataSource as List<VatLieu>;
            List<NhapVatLieu> NVL = tL_NhapKho.DataSource as List<NhapVatLieu>;
            string dbString = "";
            foreach (TCTU_TDKH item in ThuChiTDKH)
            {
                if (item.IsVatLieu)
                {
                    if (VL == null)
                        goto Label;
                    else
                    {
                        VatLieu Update = VL.FindAll(x => x.CodeKHVT == item.ID).SingleOrDefault();
                        if (Update == null)
                            goto Label;
                        else
                        {
                            Update.YeuCauDotNay = item.KhoiLuongKeHoach;
                            Update.LuyKeYeuCau = Update.LuyKeYeuCau + item.KhoiLuongKeHoach;
                            dbString = $"UPDATE  {QLVT.TBL_QLVT_YEUCAUVT} SET  \"YeuCauDotNay\"='{Update.YeuCauDotNay}' WHERE \"Code\"='{Update.ID}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            continue;
                        }

                    }
                    Label:
                    dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeKHVT\"='{item.ID}'";
                    DataTable DTyeucau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    DTyeucau.Columns["Code"].ColumnName = "ID";
                    VatLieu VatLieuAdd = DuAnHelper.ConvertToList<VatLieu>(DTyeucau).SingleOrDefault();
                    VatLieuAdd.YeuCauDotNay = item.KhoiLuongKeHoach;
                    VatLieuAdd.ParentID = VatLieuAdd.CodeHangMuc;
                    VatLieuAdd.LuyKeYeuCau = VatLieuAdd.LuyKeYeuCau + item.KhoiLuongKeHoach;
                    dbString = $"UPDATE  {QLVT.TBL_QLVT_YEUCAUVT} SET  \"YeuCauDotNay\"='{item.KhoiLuongKeHoach}',\"IsDone\"='{false}' WHERE \"Code\"='{VatLieuAdd.ID}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    VL.Add(VatLieuAdd);

                }
                else
                {
                    string codedexuat = Guid.NewGuid().ToString();
                    dbString = $"INSERT INTO '{QLVT.TBL_QLVT_YEUCAUVT}' (\"DonViThucHien\",\"DonGiaHienTruong\",\"TrangThai\",\"Code\",\"TenVatTu\",\"MaVatTu\",\"DonVi\",\"CodeGiaiDoan\",\"CodeHangMuc\",\"CodeKHVT\",\"HopDongKl\",\"YeuCauDotNay\")" +
    $" VALUES ('{DVTH.Code}','{item.DonGia}','{1}','{codedexuat}', @TenCongViec, @MaHieu, @DonVi,'{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','{item.ParentID}','{item.ID}','{item.KhoiLuongHopDong}','{item.KhoiLuongKeHoach}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { item.TenCongViec, item.MaHieu, item.DonVi });
                    string codeNhapVT = Guid.NewGuid().ToString();
                    dbString = $"INSERT INTO {QLVT.TBL_QLVT_NHAPVT} (\"DonGia\",\"TrangThai\",\"CodeDeXuat\",\"Code\",\"CodeGiaiDoan\",\"TenKhoNhap\") VALUES ('{item.DonGia}','{1}','{codedexuat}','{codeNhapVT}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}',NULL)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    VL.Add(new VatLieu()
                    {
                        ParentID = item.ParentID,
                        DonViThucHien = DVTH.Code,
                        ID = codedexuat,
                        TrangThai = 1,
                        MaVatTu = item.MaHieu,
                        TenVatTu = item.TenCongViec,
                        DonVi = item.DonVi,
                        HopDongKl = item.KhoiLuongHopDong,
                        YeuCauDotNay = item.KhoiLuongKeHoach,
                        LuyKeYeuCau = item.KhoiLuongKeHoach,
                        DonGiaHienTruong = item.DonGia,
                        FileDinhKem = "Xem File",
                        //CodeHd = "",
                        CodeKHVT = item.ID
                        //CodeTDKH = ""
                    });
                    NVL.Add(new NhapVatLieu()
                    {
                        ParentID = item.ParentID,
                        TenKhoNhap = item.ParentID,
                        DonViThucHien = DVTH.Code,
                        ID = codeNhapVT,
                        CodeDeXuat = codedexuat,
                        MaVatTu = item.MaHieu,
                        TenVatTu = item.TenCongViec,
                        DonVi = item.DonVi,
                        TrangThai = 1,
                        //CodeHd = null,
                        //CodeTDKH = null,
                        CodeKHVT = item.ID,
                        DonGia = item.DonGia,
                        FileDinhKem = "Xem File"
                    });

                }

            }

            tL_YeuCauVatTu.RefreshDataSource();
            tL_YeuCauVatTu.Refresh();
            tL_YeuCauVatTu.ExpandAll();
            //tL_YeuCauVatTu.DataSource = VL;
            tL_YeuCauVatTu.FocusedNode = tL_YeuCauVatTu.MoveLastVisible();

            tL_NhapKho.RefreshDataSource();
            tL_NhapKho.Refresh();
            tL_NhapKho.ExpandAll();
            //tL_NhapKho.DataSource = NVL;
            tL_NhapKho.FocusedNode = tL_NhapKho.MoveLastVisible();
        }

        private void Fcn_UpdateQLVT_HD(List<LayCongTacHopDong> CTHD, string CodeHD, string CodeNCC)
        {
            if (CTHD == null)
                return;
            //foreach(LayCongTacHopDong item in CTHD)
            //{
            //    string codedexuat = Guid.NewGuid().ToString();
            //    string dbString = $"INSERT INTO '{QLVT.TBL_QLVT_YEUCAUVT}' (\"TrangThai\",\"Code\",\"MaVatTu\",\"CodeHd\",\"HopDongKl\",\"CodeGiaiDoan\",\"TenVatTu\",\"DonVi\",\"CodeHangMuc\") VALUES ('{item.DonGiaHopDong}','{1}','{codedexuat}','{item.MaHieu}','{CodeHD}','{item.KhoiLuongHopDong}','{cbb_DBKH_ChonDot.SelectedValue}','{item.TenCongViec}','{item.DonVi}','{item.ParentID}')";
            //    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            //    dbString = $"INSERT INTO {QLVT.TBL_QLVT_NHAPVT} (\"DonGia\",\"TrangThai\",\"CodeDeXuat\",\"Code\",\"CodeGiaiDoan\",\"TenKhoNhap\") VALUES ('{item.DonGiaHopDong}','{1}','{codedexuat}','{Guid.NewGuid()}','{cbb_DBKH_ChonDot.SelectedValue}','{item.ParentID}')";
            //    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            //}
            List<VatLieu> VL = tL_YeuCauVatTu.DataSource as List<VatLieu>;
            List<NhapVatLieu> NVL = tL_NhapKho.DataSource as List<NhapVatLieu>;
            foreach (LayCongTacHopDong item in CTHD)
            {
                string codedexuat = Guid.NewGuid().ToString();
                string dbString = $"INSERT INTO '{QLVT.TBL_QLVT_YEUCAUVT}' (\"TenNhaCungCap\",\"DonGiaHienTruong\",\"TrangThai\",\"Code\",\"MaVatTu\",\"CodeHd\",\"HopDongKl\",\"CodeGiaiDoan\",\"TenVatTu\",\"DonVi\",\"CodeHangMuc\") VALUES" +
                    $" ('{CodeNCC}','{item.DonGiaHopDong}','{1}','{codedexuat}', @MaVatTu,'{CodeHD}','{item.KhoiLuongHopDong}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}',@TenCongViec,@DonVi,'{item.ParentID}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { item.MaHieu, item.TenCongViec, item.DonVi });
                string codeNhapVT = Guid.NewGuid().ToString();
                dbString = $"INSERT INTO {QLVT.TBL_QLVT_NHAPVT} (\"DonGia\",\"TrangThai\",\"CodeDeXuat\",\"Code\",\"CodeGiaiDoan\",\"TenKhoNhap\") VALUES ('{item.DonGiaHopDong}','{1}','{codedexuat}','{codeNhapVT}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}', NULL)";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                VL.Add(new VatLieu()
                {
                    ParentID = item.ParentID,
                    ID = codedexuat,
                    CodeHd = CodeHD,
                    TrangThai = 1,
                    MaVatTu = item.MaHieu,
                    TenVatTu = item.TenCongViec,
                    TenNhaCungCap = CodeNCC,
                    DonVi = item.DonVi,
                    HopDongKl = Math.Round(item.KhoiLuongHopDong, 3),
                    DonGiaHienTruong = (long)Math.Round(item.DonGiaHopDong),
                    FileDinhKem = "Xem File"
                }); ;
                NVL.Add(new NhapVatLieu()
                {
                    ParentID = item.ParentID,
                    TenKhoNhap = item.ParentID,
                    ID = codeNhapVT,
                    CodeDeXuat = codedexuat,
                    MaVatTu = item.MaHieu,
                    TenVatTu = item.TenCongViec,
                    DonVi = item.DonVi,
                    TrangThai = 1,
                    CodeHd = CodeHD,
                    //CodeTDKH=null,
                    DonGia = (long)Math.Round(item.DonGiaHopDong),
                    FileDinhKem = "Xem File"
                });

            }
            //VL.Add(new VatLieu()
            //{
            //    ParentID = m_ctrlVatlieu.m_codeHM,
            //    ID = Guid.NewGuid().ToString(),
            //});


            tL_YeuCauVatTu.RefreshDataSource();
            tL_YeuCauVatTu.Refresh();
            tL_YeuCauVatTu.ExpandAll();
            //tL_YeuCauVatTu.DataSource = VL;
            //tL_YeuCauVatTu.FocusedNode = tL_YeuCauVatTu.MoveLastVisible();

            tL_NhapKho.RefreshDataSource();
            tL_NhapKho.Refresh();
            tL_NhapKho.ExpandAll();
            //tL_NhapKho.DataSource = NVL;
            //tL_NhapKho.FocusedNode = tL_NhapKho.MoveLastVisible();
        }
        private void Fcn_UpdateQLVT_KHVT(List<KeHoachVatTu> KHVT)
        {
            List<VatLieu> VL = tL_YeuCauVatTu.DataSource as List<VatLieu>;
            List<NhapVatLieu> NVL = tL_NhapKho.DataSource as List<NhapVatLieu>;
            foreach (KeHoachVatTu item in KHVT)
            {
                string codedexuat = Guid.NewGuid().ToString();
                string dbString = $"INSERT INTO '{QLVT.TBL_QLVT_YEUCAUVT}' (\"DonGiaHienTruong\",\"TrangThai\",\"Code\",\"TenVatTu\",\"MaVatTu\",\"DonVi\",\"CodeGiaiDoan\",\"CodeHangMuc\",\"CodeTDKH\",\"HopDongKl\")" +
                    $" VALUES ('{item.DonGia}','{1}','{codedexuat}',@VatTu, @MaVatLieu, @DonVi,'{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','{item.CodeHangMuc}','{item.Code}','{item.KhoiLuong}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { item.VatTu, item.MaVatLieu, item.DonVi });
                string codeNhapVT = Guid.NewGuid().ToString();
                dbString = $"INSERT INTO {QLVT.TBL_QLVT_NHAPVT} (\"DonGia\",\"TrangThai\",\"CodeDeXuat\",\"Code\",\"CodeGiaiDoan\",\"TenKhoNhap\") VALUES ('{item.DonGia}','{1}','{codedexuat}','{codeNhapVT}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}',NULL)";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                VL.Add(new VatLieu()
                {
                    ParentID = item.CodeHangMuc,
                    ID = codedexuat,
                    TrangThai = 1,
                    MaVatTu = item.MaVatLieu,
                    TenVatTu = item.VatTu,
                    DonVi = item.DonVi,
                    HopDongKl = item.KhoiLuong,
                    DonGiaHienTruong = item.DonGia,
                    //CodeHd = "",
                    CodeTDKH = item.Code,
                    FileDinhKem = "Xem File"
                    //CodeTDKH = ""
                }); ;
                NVL.Add(new NhapVatLieu()
                {
                    ParentID = item.CodeHangMuc,
                    TenKhoNhap = item.CodeHangMuc,
                    ID = codeNhapVT,
                    CodeDeXuat = codedexuat,
                    MaVatTu = item.MaVatLieu,
                    TenVatTu = item.VatTu,
                    DonVi = item.DonVi,
                    TrangThai = 1,
                    //CodeHd = null,
                    //CodeTDKH = null,
                    CodeTDKH = item.Code,
                    DonGia = item.DonGia,
                    FileDinhKem = "Xem File"
                });

            }

            tL_YeuCauVatTu.RefreshDataSource();
            tL_YeuCauVatTu.Refresh();
            tL_YeuCauVatTu.ExpandAll();
            //tL_YeuCauVatTu.DataSource = VL;
            tL_YeuCauVatTu.FocusedNode = tL_YeuCauVatTu.MoveLastVisible();

            tL_NhapKho.RefreshDataSource();
            tL_NhapKho.Refresh();
            tL_NhapKho.ExpandAll();
            //tL_NhapKho.DataSource = NVL;
            tL_NhapKho.FocusedNode = tL_NhapKho.MoveLastVisible();
        }
        private void cbo_LuaChon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_LuaChon.SelectedIndex == 0 || cbo_LuaChon.SelectedIndex == 4)
            {
                fcn_loadYeuCauVT_Thucong_TDKH(true, false, false, "CodeKHVT", "CodeTDKH");
                fcn_updateDeXuatVatLieu();
            }
            else if (cbo_LuaChon.SelectedIndex == 1)
            {
                fcn_loadYeuCauVT_Thucong_TDKH(false, false, false, "CodeKHVT", "CodeTDKH");
                Form_QLVT_LayTuTDKH TDKH = new Form_QLVT_LayTuTDKH();
                TDKH.LoadData();
                TDKH.m__TRUYENDATA = new Form_QLVT_LayTuTDKH.DE__TRUYENDATA(Fcn_UpdateQLVT_TDKH);
                TDKH.ShowDialog();
            }
            else if (cbo_LuaChon.SelectedIndex == 2)
            {
                fcn_loadYeuCauVT_Thucong_TDKH(false, true, false, "CodeKHVT", "CodeTDKH");
                Form_QLVT_LayVatLieuHopDong HD = new Form_QLVT_LayVatLieuHopDong();
                HD.LoadDanhSachHopDong();
                HD.m__TRUYENDATA = new Form_QLVT_LayVatLieuHopDong.DE__TRUYENDATA(Fcn_UpdateQLVT_HD);
                HD.ShowDialog();
            }
            else
            {
                fcn_loadYeuCauVT_Thucong_TDKH(false, false, false, "CodeTDKH", "CodeKHVT");
                Form_QLVT_LayTuKHVT TDKH = new Form_QLVT_LayTuKHVT();
                TDKH.m__TRUYENDATA = new Form_QLVT_LayTuKHVT.DE__TRUYENDATA(Fcn_UpdateQLVT_KHVT);
                TDKH.ShowDialog();
            }
        }
        private void Fcn_LoadAllQLVC()
        {
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
            string dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
            DataTable dt_yeucau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<VatLieu> VatLieu = new List<VatLieu>();
            List<VatLieu> VatLieuAdd = new List<VatLieu>();
            foreach (DataRow CT in dtCT.Rows)
            {
                string crCodeCT = CT["Code"].ToString();
                VatLieu.Add(new VatLieu()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaVatTu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenVatTu = CT["Ten"].ToString(),
                });
                foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                {
                    string crCodeHM = HM["Code"].ToString();
                    VatLieu.Add(new VatLieu()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaVatTu = MyConstant.CONST_TYPE_HANGMUC,
                        TenVatTu = HM["Ten"].ToString(),
                    });
                    DataRow[] dt_vl = dt_yeucau.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM && x["IsDone"].ToString() == "False").ToArray();
                    if (dt_vl.Count() != 0)
                    {
                        DataTable dt_VatLieu = dt_vl.CopyToDataTable();
                        dt_VatLieu.Columns["Code"].ColumnName = "ID";
                        VatLieuAdd = DuAnHelper.ConvertToList<VatLieu>(dt_VatLieu);
                        foreach (VatLieu itemrow in VatLieuAdd)
                        {
                            itemrow.ParentID = crCodeHM;
                            double KLLuyKe = Fcn_CalKLVatTu(itemrow.ID, QLVT.TBL_QLVT_YEUCAUVTKLHN);
                            itemrow.LuyKeYeuCau = KLLuyKe;
                            if (itemrow.YeuCauDotNay != 0)
                                itemrow.LuyKeYeuCau = KLLuyKe + itemrow.YeuCauDotNay;
                        }
                        VatLieu.AddRange(VatLieuAdd);
                    }

                    VatLieu.Add(new VatLieu()
                    {
                        ParentID = crCodeHM,
                        ID = Guid.NewGuid().ToString(),
                    });
                }
            }

            tL_YeuCauVatTu.DataSource = VatLieu;
            tL_YeuCauVatTu.RefreshDataSource();
            tL_YeuCauVatTu.Refresh();
            tL_YeuCauVatTu.OptionsBehavior.Editable = true;

            tL_YeuCauVatTu.FormatConditions.Clear();
            tL_YeuCauVatTu.FormatRules[0].Column = tL_YeuCauVatTu.Columns["ConLai"];
            tL_YeuCauVatTu.ExpandAll();

        }
        private void ce_HienThiAll_CheckedChanged(object sender, EventArgs e)
        {
            cbo_LuaChon.Enabled = !ce_HienThiAll.Checked;
            if (ce_HienThiAll.Checked)
            {
                Fcn_LoadAllQLVC();
            }
            else
            {
                if (cbo_LuaChon.SelectedIndex == 0 || cbo_LuaChon.SelectedIndex == 4)
                {
                    fcn_loadYeuCauVT_Thucong_TDKH(true, false, false, "CodeKHVT", "CodeTDKH");
                    fcn_updateDeXuatVatLieu();
                }
                else if (cbo_LuaChon.SelectedIndex == 1)
                {
                    fcn_loadYeuCauVT_Thucong_TDKH(false, false, false, "CodeKHVT", "CodeTDKH");
                    Form_QLVT_LayTuTDKH TDKH = new Form_QLVT_LayTuTDKH();
                    TDKH.LoadData();
                    TDKH.m__TRUYENDATA = new Form_QLVT_LayTuTDKH.DE__TRUYENDATA(Fcn_UpdateQLVT_TDKH);
                    TDKH.ShowDialog();
                }
                else if (cbo_LuaChon.SelectedIndex == 2)
                {
                    fcn_loadYeuCauVT_Thucong_TDKH(false, true, false, "CodeKHVT", "CodeTDKH");
                    Form_QLVT_LayVatLieuHopDong HD = new Form_QLVT_LayVatLieuHopDong();
                    HD.LoadDanhSachHopDong();
                    HD.m__TRUYENDATA = new Form_QLVT_LayVatLieuHopDong.DE__TRUYENDATA(Fcn_UpdateQLVT_HD);
                    HD.ShowDialog();
                }
                else
                {
                    fcn_loadYeuCauVT_Thucong_TDKH(false, false, false, "CodeTDKH", "CodeKHVT");
                    Form_QLVT_LayTuKHVT TDKH = new Form_QLVT_LayTuKHVT();
                    TDKH.m__TRUYENDATA = new Form_QLVT_LayTuKHVT.DE__TRUYENDATA(Fcn_UpdateQLVT_KHVT);
                    TDKH.ShowDialog();
                }
            }
        }

        private void sb_AddVatLieu_Click(object sender, EventArgs e)
        {
            if (ce_HienThiAll.Checked)
            {
                MessageShower.ShowInformation("Vui lòng tắt Hiển thị tất cả vật liệu để lấy thêm vật liệu!!!!!!!!!");
                return;
            }
            if (cbo_LuaChon.SelectedIndex == 1)
            {
                //fcn_loadYeuCauVT_Thucong_TDKH(false, false, false, "CodeKHVT", "CodeTDKH");
                Form_QLVT_LayTuTDKH TDKH = new Form_QLVT_LayTuTDKH();
                TDKH.LoadData();
                TDKH.m__TRUYENDATA = new Form_QLVT_LayTuTDKH.DE__TRUYENDATA(Fcn_UpdateQLVT_TDKH);
                TDKH.ShowDialog();
            }
            else if (cbo_LuaChon.SelectedIndex == 2)
            {
                //fcn_loadYeuCauVT_Thucong_TDKH(false, true, false, "CodeKHVT", "CodeTDKH");
                Form_QLVT_LayVatLieuHopDong HD = new Form_QLVT_LayVatLieuHopDong();
                HD.LoadDanhSachHopDong();
                HD.m__TRUYENDATA = new Form_QLVT_LayVatLieuHopDong.DE__TRUYENDATA(Fcn_UpdateQLVT_HD);
                HD.ShowDialog();
            }
            else
            {
                //fcn_loadYeuCauVT_Thucong_TDKH(false, false, false, "CodeTDKH", "CodeKHVT");
                Form_QLVT_LayTuKHVT TDKH = new Form_QLVT_LayTuKHVT();
                TDKH.m__TRUYENDATA = new Form_QLVT_LayTuKHVT.DE__TRUYENDATA(Fcn_UpdateQLVT_KHVT);
                TDKH.ShowDialog();
            }
        }
        public void Fcn_LoadDataQLVC()
        {
            if (SharedControls.cbb_DBKH_ChonDot is null)
                return;
            RICBB_TrangThai.Items.Add("Chưa gửi duyệt", 1, 3);
            RICBB_TrangThai.Items.Add("Đã gửi duyệt", 2, 4);
            RICBB_TrangThai.Items.Add("Đang chờ duyệt", 3, 5);

            RICBB_TrangThai_XuatKho.Items.Add("Chưa xuất kho", 1, 3);
            RICBB_TrangThai_XuatKho.Items.Add("Đã xuất kho", 2, 4);
            RICBB_TrangThai_XuatKho.Items.Add("Đang chờ duyệt xuất kho", 3, 5);

            RICBB_TrangThai_NhapKho.Items.Add("Chờ xác nhận nhập kho đến", 5, 6);
            RICBB_TrangThai_NhapKho.Items.Add("Đang chờ duyệt", 4, 5);
            RICBB_TrangThai_NhapKho.Items.Add("Chưa gửi duyệt", 1, 3);
            RICBB_TrangThai_NhapKho.Items.Add("Đã nhập kho", 3, 4);
            RICBB_TrangThai_NhapKho.Items.Add("Chưa nhập kho", 2, 3);
            fcn_loadYeuCauVT_Thucong_TDKH(true, false, true, "CodeKHVT", "CodeTDKH");
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu phần Quản lý vận chuyển", "Vui Lòng chờ!");
            fcn_updateDeXuatVatLieu();
            fcn_UpdateXuatKho();
            fcn_UpdateNhapKho();
            fcn_UpdateChuyenKho();
            Fcn_UpdateTenKho();
            Fcn_UpdateVatLieuVC();
            fcn_UpdateXuatKhoTheoThang();
            WaitFormHelper.CloseWaitForm();
        }
        private void fcn_UpdateXuatKhoTheoThang()
        {
            List<XuatVatLieu> VL = (tL_XuatKho.DataSource as List<XuatVatLieu>).Where(x => x.CodeDeXuat != null).ToList();
            string CodeDX = MyFunction.fcn_Array2listQueryCondition(VL.Where(x => x.CodeDeXuat != null).Select(x => x.CodeDeXuat).ToArray());
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            string dbString = $"SELECT {QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu,{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_YEUCAUVT}.MaVatTu,{QLVT.TBL_QLVT_XUATVTKLHN}.KhoiLuong " +
                $" FROM {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"INNER JOIN {QLVT.TBL_QLVT_XUATVT} " +
                $"ON {QLVT.TBL_QLVT_XUATVT}.CodeDeXuat = {QLVT.TBL_QLVT_YEUCAUVT}.Code " +
                $"INNER JOIN {QLVT.TBL_QLVT_XUATVTKLHN} " +
                $"ON {QLVT.TBL_QLVT_XUATVTKLHN}.CodeCha = {QLVT.TBL_QLVT_XUATVT}.Code " +
                $" WHERE {QLVT.TBL_QLVT_YEUCAUVT}.Code IN ({CodeDX}) AND '{firstDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' <={QLVT.TBL_QLVT_XUATVTKLHN}.Ngay<='{lastDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            DataTable dt_XVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] MaVatLieu = VL.Select(x => x.MaVatTu).ToArray();
            int stt = 1;
            List<VatLieuHoanThanh> Source = new List<VatLieuHoanThanh>();
            foreach (XuatVatLieu item in VL)
            {

                double KL = 0;
                DataRow[] crRow = dt_XVT.AsEnumerable().Where(x => x["MaVatTu"].ToString() == item.MaVatTu).ToArray();
                foreach (var row in crRow)
                    KL += double.Parse(row["KhoiLuong"].ToString());
                Source.Add(new VatLieuHoanThanh
                {
                    STT = stt++,
                    MaVatTu = item.MaVatTu,
                    TenVatTu = item.TenVatTu,
                    DonVi = item.DonVi,
                    KhoiLuong = KL
                });
                //dtrow["KhoiLuong"] = KL;
                //Dt.Rows.Add(dtrow);
            }
            gc_VatLieuXuatNhieuTrongThang.DataSource = Source;
        }
        private void Fcn_UpdateVatLieuVC()
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            string dbString = $"SELECT * FROM {QLVT.TBL_QLVT_NKVC} WHERE '{firstDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' <=\"Ngay\"<='{lastDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' ";
            DataTable dt_CVTHN = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lstCode = MyFunction.fcn_Array2listQueryCondition(dt_CVTHN.AsEnumerable().Select(x => x["CodeCha"].ToString()).Distinct().ToArray());
            dbString = $"SELECT * FROM {QLVT.TBL_QLVT_QLVC} " +
                $"INNER JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code={QLVT.TBL_QLVT_QLVC}.CodeDeXuat " +
                $" WHERE {QLVT.TBL_QLVT_QLVC}.Code IN ({lstCode})";
            DataTable dt_CVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<VatLieuHoanThanh> VL = new List<VatLieuHoanThanh>();
            int stt = 1;
            foreach (DataRow row in dt_CVT.Rows)
            {
                VL.Add(new VatLieuHoanThanh
                {
                    STT = stt++,
                    TenVatTu = row["TenVatTu"].ToString(),
                    MaVatTu = row["MaVatTu"].ToString(),
                    DonVi = row["DonVi"].ToString(),
                });
            }
            gc_VatLieuVanChuyen.DataSource = VL;
        }
        private void fcn_UpdateChuyenKho()
        {
            List<ChuyenVatTu> VL = (tL_ChuyenKho.DataSource as List<ChuyenVatTu>).Where(x => x.CodeDeXuat != null).ToList();
            string CodeDX = MyFunction.fcn_Array2listQueryCondition(VL.Where(x => x.CodeDeXuat != null).Select(x => x.CodeDeXuat).ToArray());
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            string dbString = $"SELECT {QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu,{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_YEUCAUVT}.MaVatTu,{QLVT.TBL_QLVT_CHUYENKHOVTKLHN}.KhoiLuong " +
                $" FROM {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"INNER JOIN {QLVT.TBL_QLVT_CHUYENKHOVT} " +
                $"ON {QLVT.TBL_QLVT_CHUYENKHOVT}.CodeDeXuat = {QLVT.TBL_QLVT_YEUCAUVT}.Code " +
                $"INNER JOIN {QLVT.TBL_QLVT_CHUYENKHOVTKLHN} " +
                $"ON {QLVT.TBL_QLVT_CHUYENKHOVTKLHN}.CodeCha = {QLVT.TBL_QLVT_CHUYENKHOVT}.Code " +
                $" WHERE {QLVT.TBL_QLVT_YEUCAUVT}.Code IN ({CodeDX}) AND '{firstDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' <={QLVT.TBL_QLVT_CHUYENKHOVTKLHN}.Ngay<='{lastDayOfMonth.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            DataTable dt_CVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] MaVatLieu = VL.Select(x => x.MaVatTu).ToArray();
            int stt = 1;
            List<VatLieuHoanThanh> VLHT = new List<VatLieuHoanThanh>();
            foreach (ChuyenVatTu item in VL)
            {
                double KL = 0;
                DataRow[] crRow = dt_CVT.AsEnumerable().Where(x => x["MaVatTu"].ToString() == item.MaVatTu).ToArray();
                foreach (var row in crRow)
                    KL += double.Parse(row["KhoiLuong"].ToString());
                if (KL == 0)
                    continue;
                VLHT.Add(new VatLieuHoanThanh
                {
                    STT = stt++,
                    MaVatTu = item.MaVatTu,
                    TenVatTu = item.TenVatTu,
                    DonVi = item.DonVi,
                    KhoiLuong = KL
                });
            }
            gc_VatLieuChuyenTrongThang.DataSource = VLHT;
            gc_VatLieuChuyenKhoGanNhat.DataSource = VLHT;
        }
        private void fcn_UpdateXuatKho()
        {
            tL_XuatKho.RefreshDataSource();
            List<XuatVatLieu> VL = tL_XuatKho.DataSource as List<XuatVatLieu>;
            int percent = 0, stt = 1, stt_cycx = 1;
            List<VatLieuHoanThanh> VLXK = new List<VatLieuHoanThanh>();
            List<VatLieuHoanThanh> VLCX = new List<VatLieuHoanThanh>();
            DataTable Dt = new DataTable();

            foreach (XuatVatLieu item in VL)
            {
                if (item.TrangThai == 0)
                    continue;
                if (item.TrangThai == 1)
                {
                    VLCX.Add(new VatLieuHoanThanh
                    {
                        STT = stt_cycx++,
                        MaVatTu = item.MaVatTu,
                        TenVatTu = item.TenVatTu,
                        DonVi = item.DonVi
                    });

                }
                else
                {
                    DataRow dtrow = Dt.NewRow();
                    percent = (int)Math.Round(100 * ((double)(item.LuyKeNhapTheoDot - item.LuyKeXuatTheoDot) / item.LuyKeNhapTheoDot));
                    if (percent > 10)
                        continue;
                    VLXK.Add(new VatLieuHoanThanh
                    {
                        STT = stt++,
                        MaVatTu = item.MaVatTu,
                        TenVatTu = item.TenVatTu,
                        DonVi = item.DonVi,
                        HopDongKl = item.LuyKeNhapTheoDot,
                        LuyKeYeuCau = item.LuyKeXuatTheoDot
                    });

                }
            }
            gc_VatLieuXuatGanHet.DataSource = VLXK;
            gc_VatLieuXuatGanHet_Add.DataSource = VLXK;
            gc_VatLieuChoXuat.DataSource = VLCX;
        }
        private void sb_CapNhapDuLieuVC_Click(object sender, EventArgs e)
        {
            Fcn_LoadDataQLVC();
        }

        private void sb_VatLieuBaoCao_Click(object sender, EventArgs e)
        {
            XtraFolderBrowserDialog Xtra = new XtraFolderBrowserDialog();
            string PathSave = "";
            if (Xtra.ShowDialog() == DialogResult.OK)
            {
                PathSave = Xtra.SelectedPath;
            }
            else
                return;
            WaitFormHelper.ShowWaitForm("Đang xuất dữ liệu?Vui lòng chờ !!!!");
            tL_YeuCauVatTu.ForceInitialize();
            tL_NhapKho.ForceInitialize();
            tL_XuatKho.ForceInitialize();
            tL_ChuyenKho.ForceInitialize();
            tL_QLVC.ForceInitialize();
            tL_QLVC_TongHop.ForceInitialize();
            compositeLink.CreatePageForEachLink();
            XlsxExportOptions advOptions = new XlsxExportOptions();
            advOptions.ExportMode = XlsxExportMode.SingleFilePageByPage;
            string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
            string TenDuAn = FileHelper.RemoveInvalidChars(SharedControls.slke_ThongTinDuAn.Text);
            compositeLink.ExportToXlsx(Path.Combine(PathSave, $"Quản lý vận chuyển vật tư_{TenDuAn}_{time}.xlsx"), advOptions);
            SpreadsheetControl Spread = new SpreadsheetControl();
            Spread.LoadDocument(Path.Combine(PathSave, $"Quản lý vận chuyển vật tư_{TenDuAn}_{time}.xlsx"), DocumentFormat.Xlsx);
            IWorkbook wb = Spread.Document;
            wb.Worksheets[0].Name = "Đề xuất vật tư";
            wb.Worksheets[1].Name = "Nhập kho";
            wb.Worksheets[2].Name = "Xuât kho";
            wb.Worksheets[3].Name = "Chuyển kho";
            wb.Worksheets[4].Name = "Quản lý vận chuyển";
            wb.Worksheets[5].Name = "Thông tin kho";
            wb.Worksheets[0].ActiveView.ShowZeroValues = false;
            wb.Worksheets[1].ActiveView.ShowZeroValues = false;
            wb.Worksheets[2].ActiveView.ShowZeroValues = false;
            wb.Worksheets[3].ActiveView.ShowZeroValues = false;
            wb.Worksheets[4].ActiveView.ShowZeroValues = false;
            wb.Worksheets[5].ActiveView.ShowZeroValues = false;
            wb.SaveDocument(Path.Combine(PathSave, $"Quản lý vận chuyển vật tư_{TenDuAn}_{time}.xlsx"), DocumentFormat.Xlsx);
            WaitFormHelper.CloseWaitForm();
            DialogResult dialogResult = XtraMessageBox.Show($"Xuất file [Quản lý vận chuyển vật tư_{TenDuAn}_{time}.xlsx] thành công. Bạn có muốn mở file không???", "QUẢN LÝ THI CÔNG - THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(Path.Combine(PathSave, $"Quản lý vận chuyển vật tư_{TenDuAn}_{time}.xlsx"));
            }
        }
        private async void btn_NhapKho_Click(object sender, EventArgs e)
        {
            string dbString = "";
            XtraInputBoxArgs args = new XtraInputBoxArgs();
            args.Caption = "Cài đặt ngày Nhập kho";
            args.Prompt = "Ngày Nhập kho";
            args.DefaultButtonIndex = 0;
            //args.Showing += Args_Showing_Begin;
            // initialize a DateEdit editor with custom settings
            DateEdit editor = new DateEdit();
            editor.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            editor.Properties.Mask.EditMask = MyConstant.CONST_DATE_FORMAT_SPSHEET;
            args.Editor = editor;
            // a default DateEdit value
            args.DefaultResponse = DateTime.Now.Date;
            // display an Input Box with the custom editor
            var ngay = "";
            try
            {
                ngay = XtraInputBox.Show(args).ToString();
            }
            catch (Exception ex)
            {
                return;
            }
            XtraFormLuaChonDuyet SelectDuyet = new XtraFormLuaChonDuyet();
            SelectDuyet.ShowDialog();
            if (SelectDuyet.CancelSelect)
                return;
            bool isQuyTrinh = SelectDuyet.DuyetTheoQuyTrinh;
            WaitFormHelper.ShowWaitForm("Quá trình gửi duyệt đang được tiến hành,Vui lòng chờ!");
            tL_NhapKho.RefreshDataSource();
            tL_QLVC.RefreshDataSource();
            tL_XuatKho.RefreshDataSource();
            tL_ChuyenKho.RefreshDataSource();
            List<PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC> QLVC = tL_QLVC.DataSource as List<PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC>;
            List<VatLieu> YeuCau = tL_YeuCauVatTu.DataSource as List<VatLieu>;
            List<XuatVatLieu> xuatVatLieu = tL_XuatKho.DataSource as List<XuatVatLieu>;
            List<ChuyenVatTu> ChuyenVL = tL_ChuyenKho.DataSource as List<ChuyenVatTu>;
            List<NhapVatLieu> NVT = tL_NhapKho.DataSource as List<NhapVatLieu>;
            if (!NVT.Any())
            {
                WaitFormHelper.CloseWaitForm();
                return;
            }
            List<NhapVatLieu> NVTSelect = NVT.Where(x => x.Chon == true && x.MaVatTu != "HM" && x.MaVatTu != "CTR" && x.ThucNhap != 0).ToList();
            if (!NVTSelect.Any())
            {
                MessageShower.ShowWarning("Không có vật liệu nào được chọn để gửi duyệt hoặc khối lượng thực nhập bằng 0" +
                    "!!! Vui lòng chọn vật liệu để gửi duyệt");
                WaitFormHelper.CloseWaitForm();
                NVT.ForEach(x => x.Chon = false);
                tL_NhapKho.RefreshDataSource();
                return;
            }
            List<string> LstCode = new List<string>();

            int countOK = 0;
            int countBad = 0;
            int count = 1;
            var date = DateTime.Parse(ngay);
            List<string> Noti = new List<string>();
            bool IsGiayDuyet = SelectDuyet._CheckPhieuDuyet;
            foreach (NhapVatLieu item in NVTSelect)
            {
                if(item.TenKhoNhap is null)
                {
                    MessageShower.ShowError($"Bạn chưa nhập đầy đủ thông tin kho nhập của vật tư: {item.TenVatTu}!! Vui lòng điền đầy đủ thông tin tên kho nhập!!!");
                    continue;
                }
                item.Chon = false;
                if (IsGiayDuyet)
                    LstCode.Add(item.ID);
                if (isQuyTrinh)
                {
                    var newDXVTHN = new Tbl_QLVT_NhapVattu_KhoiLuongHangNgayViewModel()
                    {
                        Code = Guid.NewGuid().ToString(),
                        CodeCha = item.ID,
                        Ngay = date,
                        KhoiLuong = item.ThucNhap,
                        DonGia = item.DonGia,
                        TrangThai = (int)VatTuStateEnum.DangXetDuyet,
                        ACapB = item.ACapB
                    };

                    var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<Tbl_QLVT_NhapVattu_KhoiLuongHangNgayViewModel>(RouteAPI.ApprovalNhapVatTu_SendApprovalRequest, newDXVTHN);
                    if (result.MESSAGE_TYPECODE)
                    {
                        countOK++;

                    }
                    else
                    {
                        countBad++;
                        Noti.Add($"{item.MaVatTu}: {item.TenVatTu}: {result.MESSAGE_CONTENT}");
                    }
                    if (result.Dto != null)
                    {
                        var cvCha = (new List<Tbl_QLVT_NhapVattu_KhoiLuongHangNgayViewModel>() { result.Dto }).fcn_ObjToDataTable();
                        DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(cvCha, Server.Tbl_QLVT_NhapVattu_KhoiLuongHangNgay, isCompareTime: false);
                    }

                }
                else
                {
                    string code = Guid.NewGuid().ToString();
                    string codeCVT = Guid.NewGuid().ToString();
                    string codeCK = Guid.NewGuid().ToString();
                    dbString = $"INSERT INTO {QLVT.TBL_QLVT_NHAPVTKLHN} (\"TrangThai\",\"ACapB\",\"Code\",\"CodeCha\",\"Ngay\",\"KhoiLuong\",\"DonGia\",\"DaThanhToan\") " +
                        $"VALUES ('{2}','{item.ACapB}','{Guid.NewGuid()}','{item.ID}','{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.ThucNhap}','{item.DonGia}','{item.DaThanhToan}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    double KLLuyKe = Fcn_CalKLVatTu(item.ID, QLVT.TBL_QLVT_NHAPVTKLHN);
                    if (item.TrangThai == 2 || item.TrangThai == 4)
                    {
                        if (item.IsXuat)
                        {
                            dbString = $"INSERT INTO {QLVT.TBL_QLVT_XUATVT} (\"ACapB\",\"DonGiaKiemSoat\",\"LuyKeXuatTheoDot\",\"ThucXuat\",\"DonGia\",\"TenKhoNhap\",\"Code\",\"CodeDeXuat\",\"CodeGiaiDoan\",\"CodeNhapVT\",\"LuyKeNhapTheoDot\",\"TrangThai\",\"DaDuyetDeXuat\")" +
                                $" VALUES ('{item.ACapB}','{item.DonGia}','{KLLuyKe}','{0}','{item.DonGia}',@TenKhoNhap,'{code}','{item.CodeDeXuat}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString()}','{item.ID}','{item.LuyKeNhapTheoDot}','{2}','{item.DaDuyetDeXuat}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { item.TenKhoNhap });
                            dbString = $"INSERT INTO {QLVT.TBL_QLVT_XUATVTKLHN} (\"FullNameSend\",\"TrangThai\",\"ACapB\",\"Code\",\"CodeCha\",\"Ngay\",\"KhoiLuong\",\"DonGia\") " +
                                $"VALUES ('{BaseFrom.BanQuyenKeyInfo.UserId}','{2}','{item.ACapB}','{Guid.NewGuid()}','{code}','{DateTime.Parse(ngay).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.ThucNhap}','{item.DonGia}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            dbString = $"UPDATE  {QLVT.TBL_QLVT_YEUCAUVT} SET \"LuyKeXuatKho\"='{KLLuyKe}' WHERE \"Code\"='{item.CodeDeXuat}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            VatLieu VL = YeuCau.FindAll(x => x.ID == item.CodeDeXuat).FirstOrDefault();
                            dbString = $"UPDATE  {QLVT.TBL_QLVT_NHAPVT} SET  \"ACapB\"='{item.ACapB}',\"IsXuat\"='{true}' WHERE \"Code\"='{item.ID}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            if (VL != null)
                                VL.LuyKeXuatKho = KLLuyKe;
                            xuatVatLieu.Add(new XuatVatLieu
                            {
                                ID = code,
                                TrangThai = 2,
                                TenKhoNhap = item.TenKhoNhap,
                                ParentID = item.ParentID,
                                CodeDeXuat = item.CodeDeXuat,
                                CodeNhapVT = item.ID,
                                TenVatTu = item.TenVatTu,
                                MaVatTu = item.MaVatTu,
                                DonVi = item.DonVi,
                                DaDuyetDeXuat = item.DaDuyetDeXuat,
                                LuyKeNhapTheoDot = item.LuyKeNhapTheoDot,
                                CodeHd = item.CodeHd,
                                CodeKHVT = item.CodeKHVT,
                                CodeTDKH = item.CodeTDKH,
                                DonGia = item.DonGia,
                                LuyKeXuatTheoDot = item.ThucNhap,
                                DonGiaKiemSoat = item.DonGia,
                                IsXuat = true,
                                ACapB = item.ACapB,
                                FileDinhKem = "Xem File"
                            });

                            goto NexCode;

                        }
                        dbString = $"INSERT INTO {QLVT.TBL_QLVT_XUATVT} (\"ACapB\",\"DonGiaKiemSoat\",\"DonGia\",\"TenKhoNhap\",\"Code\",\"CodeDeXuat\",\"CodeGiaiDoan\",\"CodeNhapVT\",\"LuyKeNhapTheoDot\",\"TrangThai\",\"DaDuyetDeXuat\") " +
                            $"VALUES ('{item.ACapB}','{item.DonGia}','{item.DonGia}',@TenKhoNhap,'{code}','{item.CodeDeXuat}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString()}','{item.ID}','{item.LuyKeNhapTheoDot}','{1}','{item.DaDuyetDeXuat}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { item.TenKhoNhap });
                        xuatVatLieu.Add(new XuatVatLieu
                        {
                            ID = code,
                            TrangThai = 1,
                            TenKhoNhap = item.TenKhoNhap,
                            ParentID = item.ParentID,
                            CodeDeXuat = item.CodeDeXuat,
                            CodeNhapVT = item.ID,
                            TenVatTu = item.TenVatTu,
                            MaVatTu = item.MaVatTu,
                            DonVi = item.DonVi,
                            DaDuyetDeXuat = item.DaDuyetDeXuat,
                            LuyKeNhapTheoDot = item.LuyKeNhapTheoDot,
                            CodeHd = item.CodeHd,
                            CodeKHVT = item.CodeKHVT,
                            CodeTDKH = item.CodeTDKH,
                            DonGia = item.DonGia,
                            DonGiaKiemSoat = item.DonGia,
                            ACapB = item.ACapB,
                            FileDinhKem = "Xem File"
                        });
                        NexCode:
                        item.TrangThai = 3;
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_NHAPVT} SET \"TrangThai\"='{3}',\"ThucNhap\"='{0}',\"LuyKeNhapTheoDot\"=='{item.LuyKeNhapTheoDot}' WHERE \"Code\"='{item.ID}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                        dbString = $"INSERT INTO {QLVT.TBL_QLVT_QLVC} (\"CodeDeXuat\",\"DonGia\",\"Code\",\"CodeGiaiDoan\",\"CodeNhapVT\",\"HoanThoanh_Ok\",\"DaDuyetDeXuat\",\"LuyKeNhapKho\") VALUES ('{item.CodeDeXuat}','{0}','{codeCVT}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','{item.ID}','{false}','{item.DaDuyetDeXuat}','{item.LuyKeNhapTheoDot}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        dbString = $"INSERT INTO {QLVT.TBL_QLVT_CHUYENKHOVT} (\"TonKhoChuyenDi\",\"CodeDeXuat\",\"DonGia\",\"Code\",\"CodeGiaiDoan\",\"CodeNhapVT\",\"TenKhoChuyenDi\")" +
                            $" VALUES ('{item.LuyKeNhapTheoDot}','{item.CodeDeXuat}','{item.DonGia}','{codeCK}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','{item.ID}', @TenKhoNhap)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { item.TenKhoNhap });
                        QLVC.Add(new PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC
                        {
                            ID = codeCVT,
                            ParentID = item.ParentID,
                            CodeDeXuat = item.CodeDeXuat,
                            CodeNhapVT = item.ID,
                            DonGia = 0,
                            TenVatTu = item.TenVatTu,
                            MaVatTu = item.MaVatTu,
                            DonVi = item.DonVi,
                            CodeHd = item.CodeHd,
                            CodeKHVT = item.CodeKHVT,
                            CodeTDKH = item.CodeTDKH,
                            DaDuyetDeXuat = item.DaDuyetDeXuat,
                            KhoiLuongDaNhap = item.LuyKeNhapTheoDot,
                            NhatKy = "Xem Nhật Ký",
                        });
                        ChuyenVL.Add(new ChuyenVatTu
                        {
                            ID = codeCK,
                            TenKhoChuyenDi = item.TenKhoNhap,
                            ParentID = item.ParentID,
                            CodeNhapVT = item.ID,
                            TenVatTu = item.TenVatTu,
                            MaVatTu = item.MaVatTu,
                            DonVi = item.DonVi,
                            HoanThanh = false,
                            CodeHd = item.CodeHd,
                            CodeKHVT = item.CodeKHVT,
                            CodeTDKH = item.CodeTDKH,
                            TonKhoChuyenDi = item.LuyKeNhapTheoDot
                        });

                    }
                    else
                    {
                        XuatVatLieu XVL = xuatVatLieu.FindAll(x => x.CodeNhapVT == item.ID).FirstOrDefault();
                        if (!item.IsXuat)
                        {
                            XVL.LuyKeNhapTheoDot = item.LuyKeNhapTheoDot;
                            XVL.DonGia = item.DonGia;
                            dbString = $"UPDATE  {QLVT.TBL_QLVT_XUATVT} SET  \"ACapB\"='{item.ACapB}',\"LuyKeNhapTheoDot\"='{item.LuyKeNhapTheoDot}',\"DonGia\"='{item.DonGia}' WHERE \"CodeNhapVT\"='{item.ID}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        }
                        else
                        {
                            XVL.LuyKeNhapTheoDot = item.LuyKeNhapTheoDot;
                            XVL.LuyKeXuatTheoDot = item.LuyKeNhapTheoDot;
                            XVL.DonGia = item.DonGia;
                            dbString = $"INSERT INTO {QLVT.TBL_QLVT_XUATVTKLHN} (\"FullNameSend\",\"TrangThai\",\"ACapB\",\"Code\",\"CodeCha\",\"Ngay\",\"KhoiLuong\",\"DonGia\")" +
                                $" VALUES ('{BaseFrom.BanQuyenKeyInfo.UserId}','{2}','{item.ACapB}','{Guid.NewGuid()}','{XVL.ID}','{DateTime.Parse(ngay).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.ThucNhap}','{item.DonGia}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            dbString = $"UPDATE  {QLVT.TBL_QLVT_XUATVT} SET \"ACapB\"='{item.ACapB}',\"TrangThai\"='{2}',\"ThucXuat\"='{0}',\"LuyKeXuatTheoDot\"='{item.LuyKeNhapTheoDot}' WHERE \"Code\"='{XVL.ID}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            dbString = $"UPDATE  {QLVT.TBL_QLVT_YEUCAUVT} SET \"LuyKeXuatKho\"='{item.LuyKeNhapTheoDot}' WHERE \"Code\"='{item.CodeDeXuat}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            VatLieu VL = YeuCau.FindAll(x => x.ID == item.CodeDeXuat).FirstOrDefault();
                            if (VL != null)
                                VL.LuyKeXuatKho = item.LuyKeNhapTheoDot;
                        }

                        PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC qLVC = QLVC.FindAll(x => x.CodeNhapVT == item.ID).FirstOrDefault();
                        qLVC.KhoiLuongDaNhap = item.LuyKeNhapTheoDot;
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_QLVC} SET  \"LuyKeNhapKho\"='{item.LuyKeNhapTheoDot}',\"DonGia\"='{item.DonGia}' WHERE \"CodeNhapVT\"='{item.ID}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_NHAPVT} SET  \"ACapB\"='{item.ACapB}',\"ThucNhap\"='{0}',\"LuyKeNhapTheoDot\"='{item.LuyKeNhapTheoDot}' WHERE \"Code\"='{item.ID}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                    item.DaThanhToan = false;
                    item.ThucNhap = 0;
                }
            }
            if (!isQuyTrinh)
            {
                TreeListNode[] node = tL_NhapKho.FindNodes(x => double.Parse(x["LuyKeNhapTheoDot"].ToString()) >= double.Parse(x["DaDuyetDeXuat"].ToString())).Where(x => x["CodeDeXuat"] != null && double.Parse(x["TrangThai"].ToString()) > 1).ToArray();
                foreach (TreeListNode item in node)
                {
                    TreeListNode NodeYC = tL_YeuCauVatTu.FindNodeByFieldValue("ID", item["CodeDeXuat"].ToString());
                    if (NodeYC == null)
                        continue;
                    dbString = $"UPDATE  {QLVT.TBL_QLVT_YEUCAUVT} SET  \"IsDone\"='{true}' WHERE \"Code\"='{NodeYC["ID"]}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    tL_YeuCauVatTu.DeleteNode(NodeYC);
                }
                tL_NhapKho.RefreshDataSource();
                fcn_UpdateNhapKho();
                fcn_UpdateXuatKho();
                MessageShower.ShowInformation("Gửi duyệt thành công!!!!!!!!!!");
            }
            else
            {
                DialogResult drs = DialogResult.None;
                if (countOK == NVTSelect.Count)
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

            WaitFormHelper.CloseWaitForm();
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
                string m_Path = Path.Combine(BaseFrom.m_path, "Template", "FileExcel", "19.2PhieuNhapKhoVatTu.xlsx");
                SpreadsheetControl Spread = new SpreadsheetControl();
                Spread.LoadDocument(m_Path);
                Worksheet ws = Spread.Document.Worksheets[0];
                dbString = $"SELECT HM.Ten as TenHM,CTR.Ten as TenCTR,NCC.Ten as TenNhaCungCap,DVTH.Ten as TenDonViSuDung,NK.*,DX.TenVatTu,DX.DonVi " +
                    $"FROM {QLVT.TBL_QLVT_NHAPVT} NK " +
                    $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} DX ON DX.Code=NK.CodeDeXuat " +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHACUNGCAP} NCC ON NCC.Code=DX.TenNhaCungCap" +
                    $" LEFT JOIN view_DonViThucHien DVTH ON DVTH.Code=DX.DonViThucHien" +
                    $" LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} HM ON HM.Code=DX.CodeHangMuc" +
                    $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} CTR ON CTR.Code=HM.CodeCongTrinh" +
                    $" WHERE NK.Code IN ({MyFunction.fcn_Array2listQueryCondition(LstCode.ToArray())}) ";
                DataTable dt_DX = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} WHERE \"Code\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                List<Tbl_ThongTinDuAnViewModel> lst = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThongTinDuAnViewModel>(dbString);
                Spread.BeginUpdate();
                ws.Rows[3]["I"].SetValueFromText($"Dự án: {lst.SingleOrDefault().TenDuAn}");
                ws.Rows[4]["I"].SetValueFromText($"Địa điểm: {lst.SingleOrDefault().DiaChi}");
                ws.Rows[5]["I"].SetValueFromText($"Người gửi: {BaseFrom.BanQuyenKeyInfo.FullName}");
                ws.Rows[3]["M"].SetValueFromText($"Ngày {date.Day} tháng {date.Month} năm {date.Year}");
                int i = 8, STT = 1;
                var grctr = dt_DX.AsEnumerable().GroupBy(x => x["TenCTR"].ToString());
                foreach (var CTR in grctr)
                {
                    Row Crow = ws.Rows[i++];
                    ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                    Crow.Font.Color = MyConstant.color_Row_CongTrinh;
                    Crow.Font.Bold = true;
                    Crow["I"].SetValueFromText(CTR.Key);
                    var grHM = CTR.GroupBy(x => x["TenHM"].ToString());
                    foreach (var HM in grHM)
                    {
                        Crow = ws.Rows[i++];
                        ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                        Crow.Font.Color = MyConstant.color_Row_HangMuc;
                        Crow.Font.Bold = true;
                        Crow["I"].SetValueFromText(HM.Key);
                        var grVL = CTR.GroupBy(x => x["Code"].ToString());
                        foreach (var item in grVL)
                        {
                            var row = item.FirstOrDefault();
                            dbString = $"SELECT CASE WHEN YCHN.TrangThai=1 THEN 'Đang xét duyệt' ELSE 'Đã duyệt' END AS TrangThai,YCHN.* " +
                                $"FROM {QLVT.TBL_QLVT_NHAPVTKLHN} YCHN WHERE YCHN.CodeCha='{row["Code"]}' AND YCHN.Ngay='{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
                            DataTable dthn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            if (dthn.Rows.Count == 0)
                                continue;
                            double KLLuyKe = Fcn_CalKLVatTu(row["CodeDeXuat"].ToString(), QLVT.TBL_QLVT_YEUCAUVTKLHN);
                            Crow = ws.Rows[i++];
                            ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                            Crow["H"].SetValue(STT++);
                            Crow["Q"].SetValueFromText(row["TenNhaCungCap"].ToString());
                            Crow["T"].SetValueFromText(row["TenDonViSuDung"].ToString());
                            Crow["I"].SetValueFromText(row["TenVatTu"].ToString());
                            Crow["J"].SetValueFromText(row["DonVi"].ToString());
                            Crow["L"].SetValue(KLLuyKe);
                            Crow["K"].SetValue(dthn.AsEnumerable().FirstOrDefault()["KhoiLuong"]);
                            Crow["U"].SetValue(dthn.AsEnumerable().FirstOrDefault()["TrangThai"]);
                            Crow["M"].SetValue(dthn.AsEnumerable().FirstOrDefault()["DonGia"]);
                            Crow["P"].Formula = $"={Crow["K"].GetReferenceA1()}*{Crow["M"].GetReferenceA1()}";
                        }
                    }
                }
                CellRange NguoiDx = ws.Range["NguoiDeXuat"];
                ws.Rows[NguoiDx.BottomRowIndex][NguoiDx.RightColumnIndex].SetValueFromText($"{BaseFrom.BanQuyenKeyInfo.FullName}");
                Spread.EndUpdate();
                Spread.Document.History.IsEnabled = true;
                string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
                Spread.SaveDocument(Path.Combine(PathSave, $"Phiếu Nhập Vật tư_{time}.xlsx"), DocumentFormat.Xlsx);
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowInformation("Xuất File thành công!");
                DialogResult dialogResult = XtraMessageBox.Show($"[Phiếu Nhập Vật tư_{time}.xlsx] thành công. Bạn có muốn mở file không???", "QUẢN LÝ THI CÔNG - THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Process.Start(Path.Combine(PathSave, $"Phiếu Nhập Vật tư_{time}.xlsx"));
                }
                WaitFormHelper.CloseWaitForm();

            }
        }
        private async void btn_xuatkho_Click(object sender, EventArgs e)
        {
            XtraInputBoxArgs args = new XtraInputBoxArgs();
            args.Caption = "Cài đặt ngày Xuất kho";
            args.Prompt = "Ngày Xuất kho";
            args.DefaultButtonIndex = 0;
            //args.Showing += Args_Showing_Begin;
            // initialize a DateEdit editor with custom settings
            DateEdit editor = new DateEdit();
            editor.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            editor.Properties.Mask.EditMask = MyConstant.CONST_DATE_FORMAT_SPSHEET;
            args.Editor = editor;
            // a default DateEdit value
            args.DefaultResponse = DateTime.Now.Date;
            // display an Input Box with the custom editor
            var ngay = "";
            try
            {
                ngay = XtraInputBox.Show(args).ToString();
            }
            catch (Exception ex)
            {
                return;
            }
            XtraFormLuaChonDuyet SelectDuyet = new XtraFormLuaChonDuyet();
            SelectDuyet.ShowDialog();
            if (SelectDuyet.CancelSelect)
                return;
            string dbString = "";
            List<ChuyenVatTu> CVT = tL_ChuyenKho.DataSource as List<ChuyenVatTu>;
            List<VatLieu> YeuCau = tL_YeuCauVatTu.DataSource as List<VatLieu>;
            tL_ChuyenKho.RefreshDataSource();
            tL_YeuCauVatTu.RefreshDataSource();
            tL_XuatKho.RefreshDataSource();

            List<XuatVatLieu> XVT = tL_XuatKho.DataSource as List<XuatVatLieu>;
            if (!XVT.Any())
                return;
            List<XuatVatLieu> XVTSelect = XVT.Where(x => x.Chon == true && x.MaVatTu != "HM" && x.MaVatTu != "CTR" && x.ThucXuat != 0).ToList();
            if (!XVTSelect.Any())
            {
                MessageShower.ShowWarning("Không có vật liệu nào được chọn để gửi duyệt hoặc khối lượng xuất bằng 0!!! Vui lòng chọn vật liệu để gửi duyệt");
                XVT.ForEach(x => x.Chon = false);
                tL_XuatKho.RefreshDataSource();
                return;
            }
            bool isQuyTrinh = SelectDuyet.DuyetTheoQuyTrinh;
            WaitFormHelper.ShowWaitForm("Quá trình gửi duyệt đang được tiến hành,Vui lòng chờ!");

            int countOK = 0;
            int countBad = 0;
            int count = 1;
            var date = DateTime.Parse(ngay);
            List<string> Noti = new List<string>();
            List<string> LstCode = new List<string>();
            bool IsGiayDuyet = SelectDuyet._CheckPhieuDuyet;
            foreach (XuatVatLieu item in XVTSelect)
            {
                item.Chon = false;
                if (item.TenKhoNhap is null)
                {
                    MessageShower.ShowError($"Bạn chưa nhập đầy đủ thông tin kho xuất của vật tư: {item.TenVatTu}!! Vui lòng điền đầy đủ thông tin tên kho xuất!!!");
                    continue;
                }
                if (IsGiayDuyet)
                    LstCode.Add(item.ID);
                if (isQuyTrinh)
                {
                    var newDXVTHN = new Tbl_QLVT_XuatVatTu_KhoiLuongHangNgayViewModel()
                    {
                        Code = Guid.NewGuid().ToString(),
                        CodeCha = item.ID,
                        Ngay = date,
                        KhoiLuong = item.ThucXuat,
                        DonGia = item.DonGia,
                        TrangThai = (int)VatTuStateEnum.DangXetDuyet,
                        ACapB = item.ACapB
                    };

                    var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<Tbl_QLVT_XuatVatTu_KhoiLuongHangNgayViewModel>(RouteAPI.ApprovalXuatVatTu_SendApprovalRequest, newDXVTHN);
                    if (result.MESSAGE_TYPECODE)
                    {
                        countOK++;

                    }
                    else
                    {
                        countBad++;
                        Noti.Add($"{item.MaVatTu}: {item.TenVatTu}: {result.MESSAGE_CONTENT}");
                    }
                    if (result.Dto != null)
                    {
                        var cvCha = (new List<Tbl_QLVT_XuatVatTu_KhoiLuongHangNgayViewModel>() { result.Dto }).fcn_ObjToDataTable();
                        DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(cvCha, Server.Tbl_QLVT_XuatVatTu_KhoiLuongHangNgay, isCompareTime: false);
                    }

                }
                else
                {
                    dbString = $"INSERT INTO {QLVT.TBL_QLVT_XUATVTKLHN} (\"TrangThai\",\"ACapB\",\"Code\",\"CodeCha\",\"Ngay\",\"KhoiLuong\",\"DonGia\")" +
               $" VALUES ('{2}','{item.ACapB}','{Guid.NewGuid()}','{item.ID}','{DateTime.Parse(ngay).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.ThucXuat}','{item.DonGia}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    double KLLuyKe = Fcn_CalKLVatTu(item.ID, QLVT.TBL_QLVT_XUATVTKLHN);
                    ChuyenVatTu m_CVT = CVT.FindAll(x => x.CodeNhapVT == item.CodeNhapVT).FirstOrDefault();
                    item.TrangThai = 2;
                    if (item.LuyKeNhapTheoDot > item.LuyKeXuatTheoDot)
                    {
                        double KLLuyKeNhap = Fcn_CalKLVatTu(item.CodeNhapVT, QLVT.TBL_QLVT_NHAPVTKLHN);
                        m_CVT.TonKhoChuyenDi = KLLuyKeNhap - KLLuyKe;
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_CHUYENKHOVT} SET \"TonKhoChuyenDi\"='{m_CVT.TonKhoChuyenDi}' WHERE \"CodeNhapVT\"='{item.CodeNhapVT}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                    else
                    {
                        m_CVT.TonKhoChuyenDi = 0;
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_CHUYENKHOVT} SET \"TonKhoChuyenDi\"='{m_CVT.TonKhoChuyenDi}' WHERE \"CodeNhapVT\"='{item.CodeNhapVT}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                    VatLieu VL = YeuCau.FindAll(x => x.ID == item.CodeDeXuat).FirstOrDefault();
                    if (VL != null)
                        VL.LuyKeXuatKho = KLLuyKe;
                    dbString = $"UPDATE  {QLVT.TBL_QLVT_XUATVT} SET \"ACapB\"='{item.ACapB}',\"TrangThai\"='{2}',\"ThucXuat\"='{0}',\"LuyKeXuatTheoDot\"='{KLLuyKe}' WHERE \"Code\"='{item.ID}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    dbString = $"UPDATE  {QLVT.TBL_QLVT_YEUCAUVT} SET \"LuyKeXuatKho\"='{KLLuyKe}' WHERE \"Code\"='{item.CodeDeXuat}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    item.ThucXuat = 0;
                }
            }
            if (!isQuyTrinh)
            {
                tL_XuatKho.RefreshDataSource();
                fcn_UpdateXuatKho();
                fcn_UpdateXuatKhoTheoThang();
                MessageShower.ShowInformation("Gửi duyệt thành công!!!!!!!!!!");
            }
            else
            {
                DialogResult drs = DialogResult.None;
                if (countOK == XVTSelect.Count)
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
                string m_Path = Path.Combine(BaseFrom.m_path, "Template", "FileExcel", "19.3PhieuXuatKhoVatTu.xlsx");
                SpreadsheetControl Spread = new SpreadsheetControl();
                Spread.LoadDocument(m_Path);
                Worksheet ws = Spread.Document.Worksheets[0];
                dbString = $"SELECT HM.Ten as TenHM,CTR.Ten as TenCTR,NCC.Ten as TenNhaCungCap,DVTH.Ten as TenDonViSuDung,XK.*,DX.TenVatTu,DX.DonVi " +
                    $"FROM {QLVT.TBL_QLVT_XUATVT} XK " +
                    $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} DX ON DX.Code=XK.CodeDeXuat " +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHACUNGCAP} NCC ON NCC.Code=DX.TenNhaCungCap" +
                    $" LEFT JOIN view_DonViThucHien DVTH ON DVTH.Code=DX.DonViThucHien" +
                    $" LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} HM ON HM.Code=DX.CodeHangMuc" +
                    $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} CTR ON CTR.Code=HM.CodeCongTrinh" +
                    $" WHERE XK.Code IN ({MyFunction.fcn_Array2listQueryCondition(LstCode.ToArray())}) ";
                DataTable dt_DX = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} WHERE \"Code\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                List<Tbl_ThongTinDuAnViewModel> lst = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThongTinDuAnViewModel>(dbString);
                Spread.BeginUpdate();
                ws.Rows[2]["E"].SetValueFromText($"Dự án: {lst.SingleOrDefault().TenDuAn}");
                ws.Rows[3]["E"].SetValueFromText($"Địa điểm: {lst.SingleOrDefault().DiaChi}");
                ws.Rows[4]["E"].SetValueFromText($"Người gửi: {BaseFrom.BanQuyenKeyInfo.FullName}");
                ws.Rows[2]["H"].SetValueFromText($"Ngày {date.Day} tháng {date.Month} năm {date.Year}");
                int i = 7, STT = 1;
                var grctr = dt_DX.AsEnumerable().GroupBy(x => x["TenCTR"].ToString());
                foreach (var CTR in grctr)
                {
                    Row Crow = ws.Rows[i++];
                    ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                    Crow.Font.Color = MyConstant.color_Row_CongTrinh;
                    Crow.Font.Bold = true;
                    Crow["E"].SetValueFromText(CTR.Key);
                    var grHM = CTR.GroupBy(x => x["TenHM"].ToString());
                    foreach (var HM in grHM)
                    {
                        Crow = ws.Rows[i++];
                        ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                        Crow.Font.Color = MyConstant.color_Row_HangMuc;
                        Crow.Font.Bold = true;
                        Crow["E"].SetValueFromText(HM.Key);
                        var grVL = CTR.GroupBy(x => x["Code"].ToString());
                        foreach (var item in grVL)
                        {
                            var row = item.FirstOrDefault();
                            dbString = $"SELECT CASE WHEN YCHN.TrangThai=1 THEN 'Đang xét duyệt' ELSE 'Đã duyệt' END AS TrangThai,YCHN.* " +
                                $"FROM {QLVT.TBL_QLVT_XUATVTKLHN} YCHN WHERE YCHN.CodeCha='{row["Code"]}' AND YCHN.Ngay='{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
                            DataTable dthn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            if (dthn.Rows.Count == 0)
                                continue;
                            double KLLuyKe = Fcn_CalKLVatTu(row["CodeDeXuat"].ToString(), QLVT.TBL_QLVT_YEUCAUVTKLHN);
                            Crow = ws.Rows[i++];
                            ws.Rows.Insert(i, 1, RowFormatMode.FormatAsNext);
                            Crow["D"].SetValue(STT++);
                            Crow["S"].SetValueFromText(row["TenNhaCungCap"].ToString());
                            Crow["T"].SetValueFromText(row["TenDonViSuDung"].ToString());
                            Crow["E"].SetValueFromText(row["TenVatTu"].ToString());
                            Crow["F"].SetValueFromText(row["DonVi"].ToString());
                            Crow["H"].SetValue(KLLuyKe);
                            Crow["G"].SetValue(dthn.AsEnumerable().FirstOrDefault()["KhoiLuong"]);
                            Crow["U"].SetValue(dthn.AsEnumerable().FirstOrDefault()["TrangThai"]);
                            Crow["K"].SetValue(dthn.AsEnumerable().FirstOrDefault()["DonGia"]);
                            Crow["L"].Formula = $"={Crow["K"].GetReferenceA1()}*{Crow["G"].GetReferenceA1()}";
                        }
                    }
                }
                CellRange NguoiDx = ws.Range["NguoiDeXuat"];
                ws.Rows[NguoiDx.BottomRowIndex][NguoiDx.RightColumnIndex].SetValueFromText($"{BaseFrom.BanQuyenKeyInfo.FullName}");
                Spread.EndUpdate();
                Spread.Document.History.IsEnabled = true;
                string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
                Spread.SaveDocument(Path.Combine(PathSave, $"Phiếu Xuất Vật tư_{time}.xlsx"), DocumentFormat.Xlsx);
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowInformation("Xuất File thành công!");
                DialogResult dialogResult = XtraMessageBox.Show($"[Phiếu Xuất Vật tư_{time}.xlsx] thành công. Bạn có muốn mở file không???", "QUẢN LÝ THI CÔNG - THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Process.Start(Path.Combine(PathSave, $"Phiếu Xuất Vật tư_{time}.xlsx"));
                }
                WaitFormHelper.CloseWaitForm();

            }
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_LoadDataNhapKho_Tong()
        {
            List<NhapVatLieu> NVL = new List<NhapVatLieu>();
            List<NhapVatLieu> NVL_HT = tL_NhapKho.DataSource as List<NhapVatLieu>;
            tL_NhapKho.Columns["Nguon"].Visible = false;
            tL_NhapKho.Columns["TrangThai"].Visible = false;
            tL_NhapKho.Columns["ThucNhap"].Visible = false;
            var List_VL = NVL_HT.Where(x => x.CodeDeXuat != null).Select(x => new { x.MaVatTu, x.TenVatTu, x.DonVi, x.DonGia, x.TenKhoNhap }).Distinct().OrderByDescending(x => x.TenVatTu).ToList();
            foreach (var item in List_VL)
            {
                double KLDX = 0, KLDD = 0, LKTN = 0;
                List<NhapVatLieu> SearchNK = NVL_HT.FindAll(x => x.MaVatTu == item.MaVatTu && x.TenVatTu == item.TenVatTu && x.DonVi == item.DonVi && x.DonGia == item.DonGia);
                foreach (NhapVatLieu row in SearchNK)
                {
                    KLDX += row.DeXuatVatTu;
                    KLDD += row.DaDuyetDeXuat;
                    LKTN += row.LuyKeNhapTheoDot;
                }
                NVL.Add(new NhapVatLieu()
                {
                    MaVatTu = item.MaVatTu,
                    TenVatTu = item.TenVatTu,
                    DonVi = item.DonVi,
                    DonGia = item.DonGia,
                    TenKhoNhap = item.TenKhoNhap,
                    DeXuatVatTu = KLDX,
                    DaDuyetDeXuat = KLDD,
                    LuyKeNhapTheoDot = LKTN,
                });
            }

        }
        private void CE_DetailNhapKho_CheckedChanged(object sender, EventArgs e)
        {
            if (!CE_DetailNhapKho.Checked)
            {
                Fcn_LoadDataNhapKho_Tong();
            }
            else
            {
                tL_NhapKho.Columns["Nguon"].Visible = true;
                tL_NhapKho.Columns["TrangThai"].Visible = true;
                tL_NhapKho.Columns["ThucNhap"].Visible = true;
                DataTable dtCT, dtHM;
                DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
                Fcn_LoadDataNhapKho(dtCT, dtHM);
            }
        }
        private void sB_UpdateQLVC_Click(object sender, EventArgs e)
        {
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, true);
            Fcn_LoadDataTongHop(dtCT, dtHM, Infor);
            MessageShower.ShowInformation("Cập nhập hoàn tất!", "");
        }
        private void ce_CheckThongTinKho_CheckedChanged(object sender, EventArgs e)
        {
            de_NBDVL.Enabled = de_NKTVL.Enabled = ce_CheckThongTinKho.Checked;
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, true);
            if (ce_CheckThongTinKho.Checked)
            {
                string dbString = $"SELECT MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
                $"FROM {QLVT.TBL_QLVT_YEUCAUVTKLHN} WHERE \"TrangThai\"=2 ";
                DataTable dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dttc.Rows.Count == 0)
                {
                    de_NBDVL.DateTime = DateTime.Now;
                    de_NKTVL.DateTime = DateTime.Now.AddDays(30);
                }
                else
                {
                    de_NKTVL.DateTime = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : DateTime.Now.AddDays(30);
                    de_NBDVL.DateTime = dttc.Rows[0]["MinNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : DateTime.Now;
                }
                Fcn_LoadDataTongHop(dtCT, dtHM, Infor, de_NBDVL.DateTime, de_NKTVL.DateTime);
            }
            else
                Fcn_LoadDataTongHop(dtCT, dtHM, Infor);
        }
        private void fcn_UpdateQLVT_KHVT(DataTable dt)
        {
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, QLVT.TBL_QLVT_KHVT);
            fcn_QLVT_KHVT();
        }
        private void fcn_QLVT_KHVT()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu phần Quản lý vật tư", "Vui Lòng chờ!");
            FileHelper.fcn_spSheetStreamDocument(spsheet_KeHachVatTu, $@"{BaseFrom.m_templatePath}\FileExcel\10.eKeHoachVatTu.xls"); // Kế hoạch vật tư                                                                                                                        //Files.fcn_spSheetStreamDocument(spsheet_GV_KH_ChiTietCacHMCongViec, $@"C:\Users\buiva\OneDrive\Desktop\Book1.xlsx"); // Hiển thị bảng chi tiết giao việc trong kế hoạch
            Worksheet ws = spsheet_KeHachVatTu.Document.Worksheets[0];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[QLVT.TBL_QLVT_KHVT]);
            string dbString = $"SELECT \"Code\", \"Ten\" FROM {QLVT.TBL_QLVT_KHVT_CongTrinh} WHERE \"CodeDuAn\" = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dtCT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string lsCodeCT = MyFunction.fcn_Array2listQueryCondition(dtCT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());

            dbString = $"SELECT \"Code\", \"Ten\", \"CodeCongTrinh\" FROM {QLVT.TBL_QLVT_KHVT_HangMuc} WHERE \"CodeCongTrinh\" IN ({lsCodeCT})";
            DataTable dtHM = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lsCodeHM = MyFunction.fcn_Array2listQueryCondition(dtHM.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            dbString = $"SELECT * FROM {QLVT.TBL_QLVT_KHVT} WHERE \"CodeHangMuc\" IN ({lsCodeHM})";
            DataTable dt_VT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            spsheet_KeHachVatTu.BeginUpdate();
            ws.Range[QLVT.TBL_QLVT_KHVT].ClearContents();
            ws.Range[QLVT.TBL_QLVT_KHVT].Font.Color = Color.Black;
            if (ws.Range[QLVT.TBL_QLVT_KHVT].RowCount < dtCT.Rows.Count * 2 + dtHM.Rows.Count * 2 + dt_VT.Rows.Count)
                ws.Rows.Insert(ws.Range[QLVT.TBL_QLVT_KHVT].BottomRowIndex - 1,
                dtCT.Rows.Count * 2 + dtHM.Rows.Count * 2 + dt_VT.Rows.Count, RowFormatMode.FormatAsPrevious);
            int crRowInd = ws.Range[QLVT.TBL_QLVT_KHVT].TopRowIndex;
            Row copy = ws.Rows[2];
            int RowHM = 0, STT = 1;
            foreach (DataRow CT in dtCT.Rows)
            {
                string crCodeCT = CT["Code"].ToString();
                //ws.rows.insert(crRowInd, 1);
                Row crRowWs = ws.Rows[crRowInd++];

                crRowWs.Font.Bold = true;
                crRowWs.Font.Color = Color.DarkTurquoise;
                crRowWs[Name[TDKH.COL_Code]].SetValue(crCodeCT);
                crRowWs[Name["MaVatTu"]].SetValue(MyConstant.CONST_TYPE_CONGTRINH);
                crRowWs[Name["TenVatTu"]].SetValue(CT["Ten"].ToString());
                foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                {
                    string crCodeHM = HM["Code"].ToString();
                    crRowWs = ws.Rows[crRowInd++];
                    crRowWs.Font.Bold = true;
                    crRowWs.Font.Color = Color.DarkGreen;
                    crRowWs[Name[TDKH.COL_Code]].SetValue(crCodeHM);
                    crRowWs[Name[MyConstant.COL_KHVT_RowCha]].SetValue(crRowInd - 1);
                    crRowWs[Name["MaVatTu"]].SetValue(MyConstant.CONST_TYPE_HANGMUC);
                    crRowWs[Name["TenVatTu"]].SetValue(HM["Ten"].ToString().ToUpper());
                    DataRow[] dtvl = dt_VT.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                    RowHM = crRowInd;
                    foreach (var row in dtvl)
                    {
                        crRowWs = ws.Rows[crRowInd++];
                        crRowWs.CopyFrom(copy, PasteSpecial.All);
                        crRowWs.Visible = true;
                        crRowWs.Font.Color = Color.Black;
                        if (row["MaVatLieu"].ToString() == "TT")
                            crRowWs.Font.Color = MyConstant.color_DinhMucTamTinh;
                        crRowWs[Name["TenVatTu"]].SetValueFromText(row["VatTu"].ToString());
                        crRowWs[Name["MaVatTu"]].SetValueFromText(row["MaVatLieu"].ToString());
                        crRowWs[Name[MyConstant.COL_KHVT_RowCha]].SetValue(RowHM);
                        crRowWs[Name[MyConstant.COL_KHVT_STT]].SetValue(STT++);
                        foreach (DataColumn colum in dt_VT.Columns)
                        {
                            if (Name.ContainsKey(colum.ColumnName))
                            {
                                if (colum.ColumnName == "ThoiGianTu" || colum.ColumnName == "ThoiGianDen")
                                {
                                    if (row[colum.ColumnName] == DBNull.Value)
                                        continue;
                                    crRowWs[Name[colum.ColumnName]].SetValueFromText(DateTime.Parse(row[colum.ColumnName].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
                                }
                                else
                                    crRowWs[Name[colum.ColumnName]].SetValueFromText(row[colum.ColumnName].ToString());
                            }
                        }
                    }
                    crRowInd++;
                }
                crRowInd++;
            }
            spsheet_KeHachVatTu.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }
        private void sb_DocQLVC_Click(object sender, EventArgs e)
        {
            //FormKeHoachVatTu KHVT = new FormKeHoachVatTu(slke_ThongTinDuAn.EditValue.ToString(), cbb_DBKH_ChonDot.SelectedValue.ToString());
            //KHVT._TruyenDataTuExcel = new FormKeHoachVatTu.DE_TruyenDataDocFileExcel(fcn_UpdateQLVT_KHVT);
            //KHVT.ShowDialog();
            var DA = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;
            if (DA is null)
            {
                MessageShower.ShowWarning("Vui lòng chọn dự án hoặc tạo dự án mới để đọc excel vào!");
                return;
            }
            openFileDialog.DefaultExt = "xls";
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Chọn file Excel";
            DialogResult rs = openFileDialog.ShowDialog();
            if (rs == DialogResult.OK)
            {
                Form_ImportExcel_KHVT Import = new Form_ImportExcel_KHVT();
                Import.filePath = openFileDialog.FileName;
                Import._TruyenDataTuExcel = new Form_ImportExcel_KHVT.DE_TruyenDataDocFileExcel(fcn_UpdateQLVT_KHVT);
                Import.ShowDialog();
            }
        }
        private void tL_YeuCauVatTu_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Level <= 1)
                e.CanFocus = false;
        }
        private void tL_YeuCauVatTu_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node is null || e.Node.GetValue("ID") is null)
                return;
            if (e.Node.Level == 2)
            {
                string Id = e.Node.GetValue("ID").ToString();
                uc_LichSuDuyetHangNgayYeuCau.Fcn_LoadData(QLVT.TBL_QLVT_YEUCAUVTKLHN, Id);
            }
        }
        private void tL_YeuCauVatTu_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
                return;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.LightSeaGreen;
                return;
            }
            VatLieu task = tL_YeuCauVatTu.GetRow(e.Node.Id) as VatLieu;
            if (task == null)
                return;
            if (task.LuyKeYeuCau < task.HopDongKl && task.TrangThai > 1)
            {
                e.Appearance.ForeColor = Color.Blue;
            }
            if (task.YeuCauDotNay > 0)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            if (task == null || !task.IsCompleted)
                return;
            if (e.Column.FieldName == "HopDongKl")
            {
                e.Appearance.FontStyleDelta = (FontStyle.Italic | FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.Red;
            }

        }
        private void tL_YeuCauVatTu_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string dbString = "";
            string TenCotTrongCSDL = e.Column.FieldName;
            string queryStr = $"SELECT *  FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            string code = (string)tL_YeuCauVatTu.GetRowCellValue(e.Node, "ID");
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            if (TenCotTrongCSDL == "YeuCauDotNay")
            {
                if (string.IsNullOrEmpty(e.Value.ToString()))
                    return;
                if (string.IsNullOrEmpty(code))
                    return;
                if (e.Value.ToString().Contains("."))
                {
                    return;
                }
                var KLHD = tL_YeuCauVatTu.GetRowCellValue(e.Node, "HopDongKl");
                try
                {
                    if ((double)KLHD == 0)
                        tL_YeuCauVatTu.SetRowCellValue(e.Node, tL_YeuCauVatTu.Columns["HopDongKl"], e.Value);
                    var n_yeucau = dt.AsEnumerable().Where(y => y["Code"].ToString() == code).Select(x => x["TrangThai"].ToString()).ToArray();
                    var n_Luyke = dt.AsEnumerable().Where(y => y["Code"].ToString() == code).Select(x => x["LuyKeYeuCau"].ToString()).ToArray();
                    double luyke = 0;
                    if (n_yeucau[0].ToString() == "1")
                    {
                        tL_YeuCauVatTu.SetRowCellValue(e.Node, tL_YeuCauVatTu.Columns["LuyKeYeuCau"], e.Value);
                    }
                    else
                    {
                        luyke = double.Parse(e.Value.ToString()) + double.Parse(n_Luyke[0].ToString());
                        tL_YeuCauVatTu.SetRowCellValue(e.Node, tL_YeuCauVatTu.Columns["LuyKeYeuCau"], luyke);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            if (tL_YeuCauVatTu.GetRowCellValue(e.Node, "MaVatTu") != null && e.Column.FieldName != "Chon")
            {
                dbString = $"UPDATE  {QLVT.TBL_QLVT_YEUCAUVT} SET \"{TenCotTrongCSDL}\"=@NewValue WHERE \"Code\"='{code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                if (e.Column.FieldName == "DonVi" && cbo_LuaChon.SelectedIndex == 4)
                    fcn_loadYeuCauVT_Thucong_TDKH(true, false, true, "CodeKHVT", "CodeTDKH");
            }
            else
            {
                if (cbo_LuaChon.SelectedIndex == 4)
                {
                    if (e.Column.FieldName == "TenVatTu")
                    {
                        TreeListNode Parent = e.Node.ParentNode;
                        string CodeHM = Parent.GetValue("ID").ToString();
                        string codedexuat = Guid.NewGuid().ToString();
                        dbString = $"INSERT INTO '{QLVT.TBL_QLVT_YEUCAUVT}' (\"TrangThai\",\"Code\",{TenCotTrongCSDL},\"MaVatTu\",\"CodeGiaiDoan\",\"CodeHangMuc\") " +
                            $"VALUES ('{1}','{codedexuat}',@{TenCotTrongCSDL},'TT','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','{CodeHM}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                        string codeNhapVT = Guid.NewGuid().ToString();
                        dbString = $"INSERT INTO {QLVT.TBL_QLVT_NHAPVT} (\"TrangThai\",\"CodeDeXuat\",\"Code\",\"CodeGiaiDoan\",\"TenKhoNhap\") VALUES ('{1}','{codedexuat}','{codeNhapVT}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}',NULL)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        fcn_loadYeuCauVT_Thucong_TDKH(true, false, true, "CodeKHVT", "CodeTDKH");
                    }
                }
            }
        }
        private void fcn_handle_txtBoxTimKiemVatLieuThuCong_TextChange(TextBox tb)
        {
            //TextBox tb = sender as TextBox;
            //tb.TextChanged -= textBox_TextChanged;
            if (tb.Text.Length < 2)
                return;
            string strFull = MyFunction.fcn_RemoveAccents(tb.Text);
            //Worksheet ws_NhapVT = spsheet_BangDeXuatVatTuThiCong.Document.Worksheets[0];
            //Dictionary<string, string> NAME_NVT = MyFunction.fcn_getDicOfColumn(ws_NhapVT.Range[QLVT.TBL_QLVT_YEUCAUVT]);
            List<string> strLs = strFull.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            DataTable dt = null;
            string queryStr = "";
            string str;
            string NameCot;
            if (m_ctrlVatlieu.m_NameCot == "TenVatTu")
            {
                string condition = "";
                NameCot = "\"VatTu_KhongDau\"";
                queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_VATTU} WHERE ";
                //dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                //dt.Columns.Add("TenVatlieu", typeof(string));
                //foreach (DataRow row in dt.Rows)
                //    row["TenVatlieu"] = MyFunction.fcn_RemoveAccents(row["VatTu"].ToString());

                if (strLs.Count == 1)
                {
                    str = strLs[0];
                    //dt.DefaultView.RowFilter = $"[TenVatlieu] LIKE '%{str}%'";
                    queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_VATTU} WHERE ({NameCot} LIKE '%{str}%' AND \"LoaiVatTu\"='{"Vật liệu"}')";
                }
                else
                {
                    //queryStr = $"[TenVatlieu] LIKE";
                    //for (int i = 1; i < m_lsDauDM.Count; i++)
                    //    queryStr += $",'{m_lsDauDM[i]}'";
                    //queryStr += ")) ";
                    /*string strSearch = "";*/
                    foreach (string strSearch in strLs)
                    {
                        condition += $" AND {NameCot} LIKE '%{strSearch}%'";
                    }
                    queryStr += condition.Remove(0, 4);
                    queryStr += $"AND \"LoaiVatTu\"='{"Vật liệu"}'";
                    //dt.DefaultView.RowFilter += condition.Remove(0, 4);//Xóa bỏ chữ AND sau WHERE
                }
                //dt.DefaultView.RowFilter = queryStr;
                //dt = DataProvider.InstanceTBT.ExecuteQuery(queryStr);
            }
            else if ((m_ctrlVatlieu.m_NameCot == "MaVatTu"))
            {
                NameCot = "\"MaVatLieu\"";
                if (strLs.Count == 1)
                {
                    str = strLs[0];
                    queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_VATTU} WHERE ({NameCot} LIKE '%{str}%' AND \"LoaiVatTu\"='{"Vật liệu"}')";
                }
                else
                {
                    queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_VATTU} WHERE ";
                    //for (int i = 1; i < m_lsDauDM.Count; i++)
                    //    queryStr += $",'{m_lsDauDM[i]}'";
                    //queryStr += ")) ";
                    /*string strSearch = "";*/
                    string condition = "";
                    foreach (string strSearch in strLs)
                    {
                        condition += $" AND '{NameCot}' LIKE '%{strSearch}%'";
                    }
                    queryStr += condition.Remove(0, 4);//Xóa bỏ chữ AND sau WHERE
                    queryStr += $"AND \"LoaiVatTu\"='{"Vật liệu"}'";
                }
            }
            else
                return;
            dt = DataProvider.InstanceTBT.ExecuteQuery(queryStr);

            //queryStr = $"SELECT * FROM {MyConstant.TBL_TDKH_KHVT_VatTu}";
            m_ctrlVatlieu.Visible = true;
            m_ctrlVatlieu.Show();
            m_ctrlVatlieu.Parent = tb.Parent.Parent.Parent;
            //m_ctrlTKDM.PointToScreen(tb.PointToScreen(Point.Empty));

            m_ctrlVatlieu.Location = new Point(tb.Parent.Location.X + tb.Width, tb.Parent.Parent.Location.Y + 25);
            m_ctrlVatlieu.Size = new Size(tb.Parent.Parent.Width - (tb.Parent.Location.X + tb.Width), tb.Parent.Parent.Height - 50);
            //sz.Height = tb.Parent.Height;
            //sz.Width = tb.Parent.Width - (10 + tb.Parent.mopenFileDialogation.X + tb.Width)
            //m_ctrlTKDM.
            m_ctrlVatlieu.BringToFront();
            m_ctrlVatlieu.fcn_loadVatLieu(dt);
            //m_ctrlVatlieu.Hide();
        }
        private TextBox FindTextBox(Control.ControlCollection controls)
        {
            TextBox tb = null;
            foreach (Control c in controls)
            {
                if (c.GetType() == typeof(ctrlTimKiemDinhMuc) || c.GetType() == typeof(ctrlTimKiemVatLieu))
                    continue;

                tb = c as TextBox;
                if (tb != null)
                    break;

                if (c.Controls.Count != 0)
                {
                    tb = FindTextBox(c.Controls);
                    if (tb != null)
                        break;
                }
            }

            return tb;
        }
        private void fcn_Handle_Popup_QLVT_LuuDinhMucNguoiDung_Test(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM {MyConstant.TBL_TBT_VATTU} ";
            DataTable dt = DataProvider.InstanceTBT.ExecuteQuery(query);
            foreach (VatLieu item in (tL_YeuCauVatTu.DataSource as List<VatLieu>))
            {
                if (item.Chon)
                {
                    item.Chon = false;
                    string crMaDM = $"TT";
                    string crTenDinhMuc = item.MaVatTu;
                    string crDonVi = item.DonVi;
                    //CrRow["A"].SetValue(false);
                    string dbString = $"INSERT INTO {MyConstant.TBL_TBT_VATTU} (\"Id\",\"MaVatLieu\", \"VatTu\", \"VatTu_KhongDau\", \"DonVi\", \"LoaiVatTu\") VALUES " +
              $"('{Guid.NewGuid()}', @MaDinhMuc, @TenDinhMuc, @VatTuKhongDau, @DonVi, '{"Vật liệu"}')";
                    DataProvider.InstanceTBT.ExecuteNonQuery(dbString, parameter: new object[] { crMaDM, crTenDinhMuc, MyFunction.fcn_RemoveAccents(crTenDinhMuc), crDonVi });
                    item.MaVatTu = crMaDM;
                    dbString = $"UPDATE  {QLVT.TBL_QLVT_YEUCAUVT} SET \"MaVatTu\"=@MaDinhMuc WHERE \"Code\"='{item.ID}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { crMaDM });
                }
            }
            tL_YeuCauVatTu.Refresh();
        }
        private void tL_YeuCauVatTu_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList tL = sender as DevExpress.XtraTreeList.TreeList;
            DevExpress.XtraTreeList.TreeListHitInfo hitInfo = tL.CalcHitInfo(e.Point);
            if (hitInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell && (cbo_LuaChon.SelectedIndex == 0 || cbo_LuaChon.SelectedIndex == 4))
            {
                DXMenuItem menuItem = new DXMenuItem("Lưu thành vật liệu người dùng", this.fcn_Handle_Popup_QLVT_LuuDinhMucNguoiDung_Test);
                menuItem.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem);
            }
            DXMenuItem menuChon = new DXMenuItem("Chọn Vật Liệu", this.fcn_Handle_Popup_QLVT_ChonVatLieu);
            menuChon.Tag = hitInfo.Column;
            e.Menu.Items.Add(menuChon);
            DXMenuItem menuXoa = new DXMenuItem("Xóa Vật Liệu", this.fcn_Handle_Popup_QLVT_XoaVatLieu);
            menuXoa.Tag = hitInfo.Column;
            e.Menu.Items.Add(menuXoa);
            DXMenuItem menuThuChi = new DXMenuItem("Lấy khối lượng hợp đồng qua yêu cầu đợt này", this.fcn_Handle_Popup_QLVT_ChuyenKLHD);
            menuThuChi.Tag = hitInfo.Column;
            e.Menu.Items.Add(menuThuChi);

        }
        private void fcn_Handle_Popup_QLVT_ChonVatLieu(object sender, EventArgs e)
        {
            var node = tL_YeuCauVatTu.Selection;
            foreach (TreeListNode row in node)
            {
                row.CheckState = CheckState.Checked;
            }
            tL_YeuCauVatTu.Refresh();
        }
        private void fcn_Handle_Popup_QLVT_XoaVatLieu(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang Xóa dữ liệu Quản lý Vận chuyển", "Vui Lòng chờ!!!!!");
            var node = tL_YeuCauVatTu.GetNodeList().Where(x => x.Checked == true);
            foreach (TreeListNode row in node)
            {
                object Value = row.GetValue("MaVatTu");
                if (Value is null)
                    continue;
                string MaVL = Value.ToString();
                if (MaVL == MyConstant.CONST_TYPE_CONGTRINH || MaVL == MyConstant.CONST_TYPE_HANGMUC || MaVL == "")
                    continue;
                string ID = row.GetValue("ID").ToString();
                string dbString = $"DELETE FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"Code\" ='{ID}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            WaitFormHelper.CloseWaitForm();
            Fcn_LoadDataQLVC();
            MessageShower.ShowInformation("Xóa dữ liệu thành công !!!!!");
        }
        private void fcn_Handle_Popup_QLVT_ChuyenKLHD(object sender, EventArgs e)
        {
            List<VatLieu> VL = tL_YeuCauVatTu.DataSource as List<VatLieu>;
            if (VL == null)
                return;
            List<VatLieu> VLConDitions = VL.Where(x => x.Chon && x.MaVatTu != "HM" && x.MaVatTu != "CTR").ToList();
            foreach (VatLieu item in VLConDitions)
            {
                if (item.HopDongKl == 0 || item.HopDongKl <= item.LuyKeYeuCau)
                    continue;
                item.YeuCauDotNay = item.HopDongKl - item.LuyKeYeuCau;
                item.LuyKeYeuCau = item.HopDongKl;
            }
            tL_YeuCauVatTu.RefreshDataSource();
            tL_YeuCauVatTu.Refresh();



        }
        private void tL_YeuCauVatTu_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            var HM = tL_YeuCauVatTu.GetRowCellValue(e.Node.ParentNode, "TenVatTu");
            var codeHM = tL_YeuCauVatTu.GetRowCellValue(e.Node.ParentNode, "ID");

            //e.Node.ParentNode.Id;
            //var value = tL_YeuCauVatTu.EditingValue;
            //if (e.Node.Level == 0 || e.Node.Level == 1)
            //{
            //    tL_YeuCauVatTu.CancelCurrentEdit();
            //    return;
            //}

            //if (check_DXVL_LayTuTienDoVatTu.Checked)
            //{
            //    m_ctrlVatlieu.fcn_LoadCot(e.Column.FieldName, 1, (string)codeHM, 0, (string)HM, this.Controls);
            //    if (e.Column.FieldName != "TenVatTu" && e.Column.FieldName != "MaVatTu")
            //        m_ctrlVatlieu.Hide();
            //    BeginInvoke(new Action<DevExpress.XtraTreeList.TreeList>((ss) =>
            //    {
            //        TextBox tb = FindTextBox(ss.Controls);
            //        if (tb != null)
            //            tb.TextChanged += fcn_handle_txtBoxTimKiemVatLieu_TextChange;
            //    }), sender as DevExpress.XtraTreeList.TreeList);
            //    Debug.WriteLine("Finished");
            //    return;
            //}
            if (cbo_LuaChon.SelectedIndex == 0)
            {
                m_ctrlVatlieu.fcn_LoadCot(e.Column.FieldName, 2, (string)codeHM, 0, (string)HM, this.Controls);

                if (e.Column.FieldName == "TenVatTu")
                {
                    Thread thread = new Thread(() =>
                    {
                        tL_YeuCauVatTu.BeginInvoke(new Action<DevExpress.XtraTreeList.TreeList>((ss) =>
                        {
                            TextBox tb = FindTextBox(ss.Controls);
                            if (tb != null)
                                fcn_handle_txtBoxTimKiemVatLieuThuCong_TextChange(tb);
                            //tb.TextChanged += fcn_handle_txtBoxTimKiemVatLieuThuCong_TextChange;
                        }), sender as DevExpress.XtraTreeList.TreeList);
                    });
                    thread.Start();
                }
                else
                    m_ctrlVatlieu.Hide();
                //else if (e.Column.FieldName == "MaVatTu")
                //{
                //    BeginInvoke(new Action<DevExpress.XtraTreeList.TreeList>((ss) =>
                //    {
                //        TextBox tb = FindTextBox(ss.Controls);
                //        if (tb != null)
                //            tb.TextChanged += fcn_handle_txtBoxTimKiemVatLieuThuCong_TextChange;
                //    }), sender as DevExpress.XtraTreeList.TreeList);
                //    tL_YeuCauVatTu.EndUpdate();
                //    return;
                //}
                //else
                //    m_ctrlVatlieu.Hide();
            }
            else
                m_ctrlVatlieu.Hide();
            //else if (check_DXVL_LayTuKHVT.Checked)
            //{
            //    m_ctrlVatlieu.fcn_LoadCot(e.Column.FieldName, 2, (string)codeHM, 0, (string)HM, this.Controls);
            //    BeginInvoke(new Action<SpreadsheetControl>((ss) =>
            //    {
            //        TextBox tb = FindTextBox(ss.Controls);
            //        if (tb != null)
            //        {
            //            tb.TextChanged += fcn_handle_txtBoxTimKiemVatLieuKHVT_TextChange;
            //        }
            //    }), sender as SpreadsheetControl);
            //    Debug.WriteLine("Finished");

            //    return;
            //}

        }
        private void tL_YeuCauVatTu_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
            else if ((e.Column.FieldName == "Nguon" || e.Column.FieldName == "FileDinhKem") && e.Node.Level <= 1)
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
            //else if((int)treeList.GetRowCellValue(e.Node, "TrangThai") < 2)
            //{
            //    e.Appearance.ForeColor = Color.Red;
            //}

            //else if(object.Equals(e.CellValue, (double)0)&& treeList.GetRowCellValue(e.Node, "ID") == null)
            //{
            //    e.Appearance.FillRectangle(e.Cache, e.Bounds);
            //    e.Handled = true;
            //}
        }
        private void tL_NhapKho_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            TreeList tL = sender as TreeList;
            if (e.Node.Level <= 1 || (int)tL.GetRowCellValue(e.Node, "TrangThai") < 2)
                e.CanFocus = false;
        }

        private void tL_NhapKho_ShowingEditor(object sender, CancelEventArgs e)
        {
            NhapVatLieu vl = tL_NhapKho.GetFocusedRow() as NhapVatLieu;
            if (tL_NhapKho.FocusedColumn.FieldName == "Chon")
            {
                if (vl.ThucNhap == 0)
                {
                    MessageShower.ShowWarning("Vui lòng điền đầy đủ khối lượng thực nhập!");
                    e.Cancel = true;

                }
            }
            else if (tL_NhapKho.FocusedColumn.FieldName == "IsXuat")
            {
                if (vl.IsXuat == true && vl.TrangThai == 3)
                {
                    MessageShower.ShowWarning("Không được thay đổi lựa chọn!");
                    e.Cancel = true;

                }
            }
        }

        private void tL_XuatKho_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value.ToString()))
                return;
            string TenCotTrongCSDL = e.Column.FieldName;
            string dbString = "";
            string code = (string)tL_XuatKho.GetRowCellValue(e.Node, "ID");
            string queryStr = $"SELECT *  FROM {QLVT.TBL_QLVT_XUATVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            if (TenCotTrongCSDL != "Chon")
            {
                if (TenCotTrongCSDL == "TenKhoNhap")
                {
                    XuatVatLieu XVL = tL_XuatKho.GetFocusedRow() as XuatVatLieu;
                    List<ChuyenVatTu> CVT = tL_ChuyenKho.DataSource as List<ChuyenVatTu>;
                    ChuyenVatTu m_CVT = CVT.FindAll(x => x.CodeNhapVT == XVL.CodeNhapVT).FirstOrDefault();
                    m_CVT.TenKhoChuyenDi = e.Value.ToString();
                    dbString = $"UPDATE  {QLVT.TBL_QLVT_CHUYENKHOVT} SET \"TenKhoChuyenDi\"='{m_CVT.TenKhoChuyenDi}' WHERE \"CodeNhapVT\"='{XVL.CodeNhapVT}'";
                    tL_ChuyenKho.RefreshDataSource();
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    dbString = $"UPDATE  {QLVT.TBL_QLVT_XUATVT} SET \"{TenCotTrongCSDL}\"= @NewValue WHERE \"Code\"='{code}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                }
                else if (e.Column.FieldName != "Chon" && e.Column.FieldName != "KhoiLuongTra")
                {
                    dbString = $"UPDATE  {QLVT.TBL_QLVT_XUATVT} SET \"{TenCotTrongCSDL}\"= @NewValue WHERE \"Code\"='{code}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                }
            }
            if (TenCotTrongCSDL == "ThucXuat")
            {
                XuatVatLieu XVL = tL_XuatKho.GetFocusedRow() as XuatVatLieu;
                if (e.Value.ToString().Contains("."))
                {
                    return;
                }
                var n_yeucau = dt.AsEnumerable().Where(y => y["Code"].ToString() == code).Select(x => x["TrangThai"].ToString()).ToArray().FirstOrDefault();
                var n_Luyke = dt.AsEnumerable().Where(y => y["Code"].ToString() == code).Select(x => x["LuyKeXuatTheoDot"].ToString()).ToArray().FirstOrDefault();
                if (n_yeucau == "1")
                {
                    if (double.Parse(e.Value.ToString()) > XVL.DaDuyetDeXuat)
                    {
                        MessageShower.ShowWarning("Bạn nhập quá khối lượng đã nhập kho, Vui lòng nhập lại khối lượng!");
                        e.Value = XVL.LuyKeNhapTheoDot;
                    }
                    tL_XuatKho.SetRowCellValue(e.Node, tL_XuatKho.Columns["LuyKeXuatTheoDot"], e.Value);
                }
                else
                {
                    double luyke = double.Parse(e.Value.ToString()) + double.Parse(n_Luyke);
                    if (luyke > XVL.LuyKeNhapTheoDot)
                    {
                        MessageShower.ShowWarning("Bạn nhập quá khối lượng đã nhập kho, Vui lòng nhập lại khối lượng!");
                        e.Value = XVL.LuyKeNhapTheoDot - double.Parse(n_Luyke);
                        luyke = XVL.LuyKeNhapTheoDot;

                    }
                    tL_XuatKho.SetRowCellValue(e.Node, tL_XuatKho.Columns["LuyKeXuatTheoDot"], luyke);
                }
            }
        }
        private void tL_NhapKho_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList tL = sender as DevExpress.XtraTreeList.TreeList;
            DevExpress.XtraTreeList.TreeListHitInfo hitInfo = tL.CalcHitInfo(e.Point);
            //if (hitInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell && radio_DXVL_NhapThuCong.Checked)
            //{
            //    DXMenuItem menuItem = new DXMenuItem("Lưu thành vật liệu người dùng", this.fcn_Handle_Popup_QLVT_LuuDinhMucNguoiDung_Test);
            //    menuItem.Tag = hitInfo.Column;
            //    e.Menu.Items.Add(menuItem);
            //}
            if (hitInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell)
            {
                DXMenuItem menuChon = new DXMenuItem("Chọn Vật Liệu", this.fcn_Handle_Popup_QLVT_ChonVatLieuNK);
                menuChon.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuChon);
                DXMenuItem menuThuChi = new DXMenuItem("Lấy khối lượng đề xuất sang nhập kho", this.fcn_Handle_Popup_QLVT_ChuyenKLNhap);
                menuThuChi.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuThuChi);
            }
        }
        private void fcn_Handle_Popup_QLVT_ChuyenKLNhap(object sender, EventArgs e)
        {
            List<NhapVatLieu> NK = tL_NhapKho.DataSource as List<NhapVatLieu>;
            if (NK == null)
                return;
            List<NhapVatLieu> NKCondition = NK.Where(x => x.Chon && x.MaVatTu != "HM" && x.MaVatTu != "CTR").ToList();
            foreach (NhapVatLieu item in NKCondition)
            {
                if (item.DaDuyetDeXuat <= item.LuyKeNhapTheoDot)
                    continue;
                item.ThucNhap = item.DaDuyetDeXuat - item.LuyKeNhapTheoDot;
                item.LuyKeNhapTheoDot = item.DaDuyetDeXuat;
            }
            tL_NhapKho.RefreshDataSource();
            tL_NhapKho.Refresh();

        }
        private void fcn_Handle_Popup_QLVT_ChonVatLieuNK(object sender, EventArgs e)
        {
            var node = tL_NhapKho.Selection;
            foreach (TreeListNode row in node)
                row.CheckState = CheckState.Checked;
            tL_NhapKho.Refresh();
        }
        private void tL_NhapKho_KeyUp(object sender, KeyEventArgs e)
        {
            var treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (e.KeyCode == Keys.Enter)
            {
                treeList.CloseEditor();
                e.SuppressKeyPress = true;
                treeList.FocusedNode = treeList.FocusedNode.NextNode;
            }
        }
        private void tL_XuatKho_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Node.Level == 0 || e.Node.Level == 1)
                tL_XuatKho.CancelCurrentEdit();
        }
        private void tL_NhapKho_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node is null)
                return;
            if (e.Node.Level == 2)
            {
                string Id = e.Node.GetValue("ID").ToString();
                uc_LichSuDuyetHangNgayNhapKho.Fcn_LoadData(QLVT.TBL_QLVT_NHAPVTKLHN, Id);
            }
        }
        private void tL_NhapKho_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.LightSeaGreen;
            }
            else if (e.Node.Level == 2)
            {
                if (treeList.GetRowCellValue(e.Node, "TrangThai") is null)
                    return;
                if ((int)treeList.GetRowCellValue(e.Node, "TrangThai") == 1)
                    e.Appearance.ForeColor = Color.Red;
                else if ((bool)treeList.GetRowCellValue(e.Node, "IsXuat") == true)
                    e.Appearance.ForeColor = Color.Purple;
                else
                {
                    NhapVatLieu task = tL_NhapKho.GetRow(e.Node.Id) as NhapVatLieu;
                    if (task == null)
                        return;
                    if (task.TrangThai > 1 && task.LuyKeNhapTheoDot >= task.DaDuyetDeXuat && e.Column.FieldName == "DaDuyetDeXuat")
                    {
                        e.Appearance.FontStyleDelta = (FontStyle.Italic | FontStyle.Strikeout);
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else if (task.TrangThai > 1 && task.LuyKeNhapTheoDot < task.DaDuyetDeXuat)
                        e.Appearance.ForeColor = Color.Blue;
                }
            }
        }

        private void tL_NhapKho_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string TenCotTrongCSDL = e.Column.FieldName;
            string code = (string)tL_NhapKho.GetRowCellValue(e.Node, "ID");
            string queryStr = $"SELECT *  FROM {QLVT.TBL_QLVT_NHAPVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            if (e.Column.FieldName == "TenNhaCungCap" || e.Column.FieldName == "DonViThucHien")
                return;
            if (TenCotTrongCSDL == "ThucNhap")
            {
                if (e.Value.ToString().Contains("."))
                {
                    return;
                }
                var n_yeucau = dt.AsEnumerable().Where(y => y["Code"].ToString() == code).Select(x => x["TrangThai"].ToString()).ToArray().FirstOrDefault();
                var n_Luyke = dt.AsEnumerable().Where(y => y["Code"].ToString() == code).Select(x => x["LuyKeNhapTheoDot"].ToString()).ToArray().FirstOrDefault();
                //string[] n_yeucau = worksheet.Rows[e.RowIndex][NAME["Trangthai"]].Value.TextValue.Split(' ');
                if (n_yeucau == "2")
                {
                    tL_NhapKho.SetRowCellValue(e.Node, tL_NhapKho.Columns["LuyKeNhapTheoDot"], e.Value);
                    //ws_NhapVT.Rows[e.RowIndex][NAME["LuyKeNhapTheoDot"]].Formula = $"={NAME["ThucNhap"]}{e.RowIndex + 1}";
                    //worksheet.Rows[e.RowIndex][NAME["LuyKeYeuCau"]].Formula = $"={NAME["YeuCauDotNay"]}{e.RowIndex+ 1}+{NAME["LuyKeYeuCau"]}{e.RowIndex}";
                }
                else
                {
                    double luyke = double.Parse(e.Value.ToString()) + double.Parse(n_Luyke);
                    tL_NhapKho.SetRowCellValue(e.Node, tL_NhapKho.Columns["LuyKeNhapTheoDot"], luyke);
                }
            }
            if (e.Column.FieldName != "Chon" && e.Column.FieldName != "DaThanhToan" && e.Column.FieldName != "KhoiLuongTra")
            {
                string dbString = $"UPDATE  {QLVT.TBL_QLVT_NHAPVT} SET \"{TenCotTrongCSDL}\" = @NewValue WHERE \"Code\"='{code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
            }
        }
        private void tL_NhapKho_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0) || ((object.Equals(e.CellValue, false)
                || e.Column.FieldName == "FileDinhKem" || e.Column.FieldName == "TraVatTu") && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }
        private void tL_XuatKho_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList tL = sender as DevExpress.XtraTreeList.TreeList;
            DevExpress.XtraTreeList.TreeListHitInfo hitInfo = tL.CalcHitInfo(e.Point);
            if (hitInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell)
            {
                DXMenuItem menuChon = new DXMenuItem("Chọn Vật Liệu", this.fcn_Handle_Popup_QLVT_ChonVatLieuXK);
                menuChon.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuChon);
                DXMenuItem menuThuChi = new DXMenuItem("Chuyển khối lượng nhập sang xuất", this.fcn_Handle_Popup_QLVT_ChuyenKLNhapSangXuat);
                menuThuChi.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuThuChi);
            }
        }
        private void fcn_Handle_Popup_QLVT_ChuyenKLNhapSangXuat(object sender, EventArgs e)
        {
            List<XuatVatLieu> XVL = tL_XuatKho.DataSource as List<XuatVatLieu>;
            if (XVL == null)
                return;
            List<XuatVatLieu> XVLCondition = XVL.Where(x => x.Chon && x.MaVatTu != "HM" && x.MaVatTu != "CTR").ToList();
            foreach (XuatVatLieu item in XVLCondition)
            {
                if (item.LuyKeNhapTheoDot <= item.LuyKeXuatTheoDot)
                    continue;
                item.ThucXuat = item.LuyKeNhapTheoDot - item.LuyKeXuatTheoDot;
                item.LuyKeXuatTheoDot = item.LuyKeNhapTheoDot;
            }
            tL_XuatKho.RefreshDataSource();
            tL_XuatKho.Refresh();
        }
        private void fcn_Handle_Popup_QLVT_ChonVatLieuXK(object sender, EventArgs e)
        {
            var node = tL_XuatKho.Selection;
            foreach (TreeListNode row in node)
                row.CheckState = CheckState.Checked;
            tL_XuatKho.Refresh();
        }
        private void tL_XuatKho_ShowingEditor(object sender, CancelEventArgs e)
        {
            XuatVatLieu XVL = tL_XuatKho.GetFocusedRow() as XuatVatLieu;
            if (XVL is null)
                return;
            if (XVL.IsXuat)
            {
                MessageShower.ShowWarning("Vật liệu được xuất tự động,Không được thay đổi dữ liệu!");
                e.Cancel = true;
            }
            if (tL_XuatKho.FocusedColumn.FieldName == "TenKhoNhap" && XVL.TrangThai == 2)
            {
                MessageShower.ShowInformation("Vật liệu đã xuất kho, Bạn không thể đổi lại Tên kho xuất");
                e.Cancel = true;
            }
        }
        private void tL_XuatKho_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node is null)
                return;
            if (e.Node.Level == 2)
            {
                string Id = e.Node.GetValue("ID").ToString();
                uc_LichSuDuyetHangNgayXuatKho.Fcn_LoadData(QLVT.TBL_QLVT_XUATVTKLHN, Id);
            }
        }
        private void tL_XuatKho_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.LightSeaGreen;
            }
            else if (e.Node.Level == 2)
            {
                XuatVatLieu XVL = tL_XuatKho.GetRow(e.Node.Id) as XuatVatLieu;
                if (XVL == null)
                    return;
                if (XVL.IsXuat)
                    e.Appearance.ForeColor = Color.Purple;
                else if (XVL.TrangThai > 1 && XVL.LuyKeXuatTheoDot < XVL.LuyKeNhapTheoDot)
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
                else if (XVL.TrangThai >= 1 && XVL.LuyKeXuatTheoDot >= XVL.LuyKeNhapTheoDot)
                {
                    if (XVL.IsXuat)
                        return;
                    if (e.Column.FieldName == "LuyKeNhapTheoDot")
                    {
                        e.Appearance.FontStyleDelta = (FontStyle.Italic | FontStyle.Strikeout);
                        e.Appearance.ForeColor = Color.Red;
                    }
                }

            }
        }
        private void tL_XuatKho_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            //if (object.Equals(e.CellValue, (double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level < 2)))
            //{
            //    e.Appearance.FillRectangle(e.Cache, e.Bounds);
            //    e.Handled = true;
            //}
            if (object.Equals(e.CellValue, (double)0) || ((object.Equals(e.CellValue, false)
    || e.Column.FieldName == "FileDinhKem" || e.Column.FieldName == "TraVatTu") && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }
        private void tL_ChuyenKho_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string TenCotTrongCSDL = e.Column.FieldName;
            string dbString = "";
            string code = (string)tL_ChuyenKho.GetRowCellValue(e.Node, "ID");
            string queryStr = $"SELECT *  FROM {QLVT.TBL_QLVT_CHUYENKHOVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            if (TenCotTrongCSDL != "Chon" && TenCotTrongCSDL != "ThucNhapKhoDen" && TenCotTrongCSDL != "TenKhoChuyenDen" && TenCotTrongCSDL != "ChuyenKho")
            {
                dbString = $"UPDATE  {QLVT.TBL_QLVT_CHUYENKHOVT} SET \"{TenCotTrongCSDL}\"= @NewValue WHERE \"Code\"='{code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
            }
        }
        private void tL_ChuyenKho_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (tL_ChuyenKho.GetRowCellValue(e.Node, "TonKhoChuyenDi") is null)
                return;
            if (e.Node.Level == 0 || e.Node.Level == 1 || (double)tL_ChuyenKho.GetRowCellValue(e.Node, "TonKhoChuyenDi") == 0)
                tL_ChuyenKho.CancelCurrentEdit();
            //else if(e.Column.FieldName=="ChuyenKho")
            //    tL_ChuyenKho.CancelCurrentEdit();
            //else if(e.Column.FieldName== "ThucNhapKhoDen"||e.Column.FieldName== "TenKhoChuyenDen")
            //{
            //    if((int)tL_ChuyenKho.GetRowCellValue(e.Node, "ChuyenKho") == 1)
            //        tL_ChuyenKho.CancelCurrentEdit();
            //}
        }
        private void tL_ChuyenKho_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (tL_ChuyenKho.FocusedNode == null)
                return;
            if (e.Node.Level < 2)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnActivate = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                btnActivate.Buttons.Clear();
                e.RepositoryItem = btnActivate;
            }
            else if (e.Node.Focused == tL_ChuyenKho.FocusedNode.Focused && e.Column.FieldName == "ChuyenKho")
            {
                e.RepositoryItem = rIBE_ChuyeKho;
            }
        }

        private void tL_ChuyenKho_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;

            }
        }
        private void tL_ChuyenKho_CustomNodeCellEditForEditing(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Node.Focused == tL_ChuyenKho.FocusedNode.Focused && e.Column.FieldName == "ChuyenKho")
            {
                e.RepositoryItem = rIBE_ChuyeKho;
            }
        }
        private void tL_ChuyenKho_EditFormPrepared(object sender, DevExpress.XtraTreeList.EditFormPreparedEventArgs e)
        {
            //e.Panel.Location = new Point(tL_ChuyenKho.Parent.Location.X + tL_ChuyenKho.Width, tL_ChuyenKho.Parent.Parent.Location.Y + 25);
            Control ctrl = MyExtenstions.FindControl(e.Panel, "Update");
            if (ctrl != null)
            {
                ctrl.Text = "Ok";
                (ctrl as SimpleButton).ImageOptions.Image = imageCollection.Images[0];
                (ctrl as SimpleButton).Click += (s, ee) =>
                {
                    ChuyenVatTu CVL = tL_ChuyenKho.GetFocusedRow() as ChuyenVatTu;
                    CVL.HoanThanh = true;
                    MessageShower.ShowInformation("Kiểm tra lại số liệu và nhấn chọn Ok để hoàn thành!", "Lưu ý");
                    tL_ChuyenKho.RefreshDataSource();
                };
            }

            ctrl = MyExtenstions.FindControl(e.Panel, "Cancel");
            if (ctrl != null)
            {
                (ctrl as SimpleButton).ImageOptions.Image = imageCollection.Images[1];
                ctrl.Text = "Đóng";
            }
        }
        private void tL_QLVC_TongHop_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void tL_QLVC_TongHop_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
                if (e.Node.Level == 2)
                    e.Appearance.DrawString(e.Cache, "-", e.Bounds);
            }
        }

        private void tL_QLVC_TongHop_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.LightSeaGreen;
            }
        }
        private void spsheet_KeHachVatTu_CellBeginEdit(object sender, SpreadsheetCellCancelEventArgs e)
        {
            Worksheet ws = spsheet_KeHachVatTu.Document.Worksheets.ActiveWorksheet;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange range = ws.Range[QLVT.TBL_QLVT_KHVT];
            string colHeading = ws.Columns[e.Cell.ColumnIndex].Heading;
            if (!range.Contains(e.Cell))
                return;
            if (colHeading == Name["MaVatTu"])
            {
                string Code = ws.Rows[e.RowIndex][Name["Code"]].Value.ToString();
                if (e.Value.ToString().ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    if (Code != "")
                        e.Cancel = true;
                }
                else if (e.Value.ToString().ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    if (Code != "")
                        e.Cancel = true;
                }
                else
                {
                    if (!ce_goiythucong.Checked)
                    {
                        return;
                    }
                    string CodeHM = "";
                    for (int ind = e.RowIndex - 1; ind >= range.TopRowIndex; ind--)
                    {
                        Row crRowLoop = ws.Rows[ind];
                        string code = crRowLoop[Name[MyConstant.COL_KHVT_CodeCT]].Value.ToString();
                        string MaVatLieu = crRowLoop[e.ColumnIndex].Value.ToString();
                        if (code == "")
                            continue;
                        else if (code != "")
                        {
                            if (MaVatLieu == MyConstant.CONST_TYPE_HANGMUC)
                            {
                                CodeHM = code;
                                break;
                            }
                            else
                                continue;
                        }
                    }
                    m_ctrlVatlieuThuCong.fcn_LoadCot("MaVatTu", CodeHM);
                    BeginInvoke(new Action<SpreadsheetControl>((ss) =>
                    {
                        TextBox tb = FindTextBox(ss.Controls);
                        if (tb != null)
                        {
                            tb.TextChanged += fcn_handle_txtBoxTimKiemVatLieuThuCongKHVT_TextChange;
                        }
                    }), sender as SpreadsheetControl);
                }


            }
            else if (colHeading == Name["TenVatTu"])
            {
                string MaVatLieu = ws.Rows[e.RowIndex][Name["MaVatTu"]].Value.ToString();
                if (MaVatLieu == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    return;
                }
                else if (MaVatLieu == MyConstant.CONST_TYPE_HANGMUC)
                {
                    return;
                }
                else
                {
                    if (!ce_goiythucong.Checked)
                    {
                        return;
                    }
                    string CodeHM = "";
                    for (int ind = e.RowIndex - 1; ind > range.TopRowIndex; ind--)
                    {
                        Row crRowLoop = ws.Rows[ind];
                        string code = crRowLoop[Name[MyConstant.COL_KHVT_CodeCT]].Value.ToString();
                        MaVatLieu = crRowLoop[Name["MaVatTu"]].Value.ToString();
                        if (code == "")
                            continue;
                        else if (code != "")
                        {
                            if (MaVatLieu == MyConstant.CONST_TYPE_HANGMUC)
                            {
                                CodeHM = code;
                                break;
                            }
                            else
                                continue;
                        }
                    }
                    m_ctrlVatlieuThuCong.fcn_LoadCot("TenVatTu", CodeHM);
                    BeginInvoke(new Action<SpreadsheetControl>((ss) =>
                    {
                        TextBox tb = FindTextBox(ss.Controls);
                        if (tb != null)
                        {
                            tb.TextChanged += fcn_handle_txtBoxTimKiemVatLieuThuCongKHVT_TextChange;
                        }
                    }), sender as SpreadsheetControl);
                }
            }
            else
            {
                string NewVal = ws.Rows[e.RowIndex][Name["MaVatTu"]].Value.TextValue;
                if (NewVal == MyConstant.CONST_TYPE_CONGTRINH || NewVal == MyConstant.CONST_TYPE_HANGMUC)
                    e.Cancel = true;
            }
        }
        private void fcn_handle_txtBoxTimKiemVatLieuThuCongKHVT_TextChange(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Length < 2)
                return;
            string strFull = MyFunction.fcn_RemoveAccents(tb.Text);
            List<string> strLs = strFull.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            DataTable dt = null;
            string queryStr = "";
            string str;
            string NameCot;
            if (m_ctrlVatlieuThuCong.m_NameCot == "TenVatTu")
            {
                string condition = "";
                NameCot = "\"VatTu_KhongDau\"";
                queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_VATTU} WHERE ";

                if (strLs.Count == 1)
                {
                    str = strLs[0];
                    //dt.DefaultView.RowFilter = $"[TenVatlieu] LIKE '%{str}%'";
                    queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_VATTU} WHERE ({NameCot} LIKE '%{str}%' AND \"LoaiVatTu\"='{"Vật liệu"}')";
                }
                else
                {
                    //queryStr = $"[TenVatlieu] LIKE";
                    //for (int i = 1; i < m_lsDauDM.Count; i++)
                    //    queryStr += $",'{m_lsDauDM[i]}'";
                    //queryStr += ")) ";
                    /*string strSearch = "";*/
                    foreach (string strSearch in strLs)
                    {
                        condition += $" AND {NameCot} LIKE '%{strSearch}%'";
                    }
                    queryStr += condition.Remove(0, 4);
                    queryStr += $"AND \"LoaiVatTu\"='{"Vật liệu"}'";
                    //dt.DefaultView.RowFilter += condition.Remove(0, 4);//Xóa bỏ chữ AND sau WHERE
                }
                //dt.DefaultView.RowFilter = queryStr;
                //dt = DataProvider.InstanceTBT.ExecuteQuery(queryStr);
            }
            else if ((m_ctrlVatlieuThuCong.m_NameCot == "MaVatTu"))
            {
                NameCot = "\"MaVatLieu\"";
                if (strLs.Count == 1)
                {
                    str = strLs[0];
                    queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_VATTU} WHERE ({NameCot} LIKE '%{str}%' AND \"LoaiVatTu\"='{"Vật liệu"}')";
                }
                else
                {
                    queryStr = $"SELECT * FROM {MyConstant.TBL_TBT_VATTU} WHERE ";
                    //for (int i = 1; i < m_lsDauDM.Count; i++)
                    //    queryStr += $",'{m_lsDauDM[i]}'";
                    //queryStr += ")) ";
                    /*string strSearch = "";*/
                    string condition = "";
                    foreach (string strSearch in strLs)
                    {
                        condition += $" AND '{NameCot}' LIKE '%{strSearch}%'";
                    }
                    queryStr += condition.Remove(0, 4);//Xóa bỏ chữ AND sau WHERE
                    queryStr += $"AND \"LoaiVatTu\"='{"Vật liệu"}'";
                }
            }
            else
                return;
            dt = DataProvider.InstanceTBT.ExecuteQuery(queryStr);

            //queryStr = $"SELECT * FROM {MyConstant.TBL_TDKH_KHVT_VatTu}";
            m_ctrlVatlieuThuCong.Visible = true;
            m_ctrlVatlieuThuCong.Show();
            m_ctrlVatlieuThuCong.Parent = tb.Parent.Parent.Parent;
            //m_ctrlTKDM.PointToScreen(tb.PointToScreen(Point.Empty));

            m_ctrlVatlieuThuCong.Location = new Point(tb.Parent.Location.X + tb.Width, tb.Parent.Parent.Location.Y + 25);
            m_ctrlVatlieuThuCong.Size = new Size(tb.Parent.Parent.Width - (tb.Parent.Location.X + tb.Width), tb.Parent.Parent.Height - 50);
            //sz.Height = tb.Parent.Height;
            //sz.Width = tb.Parent.Width - (10 + tb.Parent.mopenFileDialogation.X + tb.Width)
            //m_ctrlTKDM.
            m_ctrlVatlieuThuCong.BringToFront();
            m_ctrlVatlieuThuCong.fcn_loadVatLieu(dt);
            //m_ctrlVatlieu.Hide();
        }
        private void spsheet_KeHachVatTu_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            Worksheet ws = spsheet_KeHachVatTu.Document.Worksheets.ActiveWorksheet;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            string Code = ws.Rows[e.RowIndex][Name["Code"]].Value.ToString();
            string colHeading = ws.Rows[0][e.ColumnIndex].Value.TextValue;
            string Header = ws.Columns[e.ColumnIndex].Heading;
            CellRange Range = ws.Range[QLVT.TBL_QLVT_KHVT];
            string dbString = "";
            if (e.RowIndex == Range.BottomRowIndex - 1)
            {
                ws.Rows.Insert(e.RowIndex + 1, 1, RowFormatMode.FormatAsPrevious);
                ws.Rows[e.RowIndex + 1].Visible = true;
            }
            if (Header == Name["MaVatTu"])
            {
                if (e.Value.ToString().ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    string CodeCT = "";
                    double RowCha = 0;
                    for (int ind = e.RowIndex - 1; ind >= 0; ind--)
                    {
                        Row crRowLoop = ws.Rows[ind];
                        string code = crRowLoop[Name[MyConstant.COL_KHVT_CodeCT]].Value.ToString();
                        string MaVatLieu = crRowLoop[e.ColumnIndex].Value.ToString();
                        if (code == "")
                            continue;
                        else if (code != "")
                        {
                            if (MaVatLieu == MyConstant.CONST_TYPE_CONGTRINH)
                            {
                                CodeCT = code;
                                RowCha = ind + 1;
                                break;
                            }
                            else
                                continue;
                        }
                    }
                    string CodeHM = Guid.NewGuid().ToString();
                    ws.Rows[e.RowIndex].Font.Color = MyConstant.color_Row_HangMuc;
                    ws.Rows[e.RowIndex].Font.Bold = true;
                    ws.Rows[e.RowIndex][e.ColumnIndex].SetValueFromText(e.Value.ToString().ToUpper());
                    ws.Rows[e.RowIndex][Name["TenVatTu"]].SetValueFromText("Hạng mục mới");
                    ws.Rows[e.RowIndex][Name[MyConstant.COL_KHVT_RowCha]].SetValue(RowCha);
                    ws.Rows[e.RowIndex][Name[MyConstant.COL_KHVT_CodeCT]].SetValueFromText(CodeHM);
                    dbString = $"INSERT INTO {QLVT.TBL_QLVT_KHVT_HangMuc} (\"Code\",\"Ten\",\"CodeCongTrinh\") VALUES ('{CodeHM}','{"Hạng mục mới"}','{CodeCT}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    m_ctrlVatlieuThuCong.Hide();
                }
                else if (e.Value.ToString().ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    ws.Rows.Insert(e.RowIndex + 1, 1, RowFormatMode.FormatAsPrevious);
                    ws.Rows[e.RowIndex + 1].Visible = true;

                    string CodeCongTrinh = Guid.NewGuid().ToString();
                    ws.Rows[e.RowIndex].Font.Color = MyConstant.color_Row_CongTrinh;
                    ws.Rows[e.RowIndex].Font.Bold = true;
                    ws.Rows[e.RowIndex][e.ColumnIndex].SetValueFromText(e.Value.ToString().ToUpper());
                    ws.Rows[e.RowIndex][Name["TenVatTu"]].SetValueFromText("Công trình mới");
                    ws.Rows[e.RowIndex][Name[MyConstant.COL_KHVT_CodeCT]].SetValueFromText(CodeCongTrinh);
                    dbString = $"INSERT INTO {QLVT.TBL_QLVT_KHVT_CongTrinh} (\"Code\",\"Ten\",\"CodeDuAn\") VALUES ('{CodeCongTrinh}','{"Công trình mới"}','{SharedControls.slke_ThongTinDuAn.EditValue}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                    string CodeHM = Guid.NewGuid().ToString();
                    ws.Rows[e.RowIndex + 1].Font.Color = MyConstant.color_Row_HangMuc;
                    ws.Rows[e.RowIndex + 1].Font.Bold = true;
                    ws.Rows[e.RowIndex + 1][e.ColumnIndex].SetValueFromText("HM");
                    ws.Rows[e.RowIndex + 1][Name["TenVatTu"]].SetValueFromText("Hạng mục mới");
                    ws.Rows[e.RowIndex + 1][Name[MyConstant.COL_KHVT_RowCha]].SetValue(e.RowIndex + 1);
                    ws.Rows[e.RowIndex + 1][Name[MyConstant.COL_KHVT_CodeCT]].SetValueFromText(CodeHM);
                    dbString = $"INSERT INTO {QLVT.TBL_QLVT_KHVT_HangMuc} (\"Code\",\"Ten\",\"CodeCongTrinh\") VALUES ('{CodeHM}','{"Hạng mục mới"}','{CodeCongTrinh}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    m_ctrlVatlieuThuCong.Hide();
                }
                else
                {
                    colHeading = "MaVatLieu";
                    if (Code == "")
                    {
                        double STT = 1, RowCha = 0;
                        string CodeHM = "";
                        Row Rowcopy = ws.Rows[2];
                        for (int ind = e.RowIndex - 1; ind >= 0; ind--)
                        {
                            Row crRowLoop = ws.Rows[ind];
                            string code = crRowLoop[Name[MyConstant.COL_KHVT_CodeCT]].Value.ToString();
                            string MaVatLieu = crRowLoop[e.ColumnIndex].Value.ToString();
                            if (code == "")
                                continue;
                            else if (code != "")
                            {
                                if (MaVatLieu == MyConstant.CONST_TYPE_HANGMUC)
                                {
                                    RowCha = ind + 1;
                                    CodeHM = code;
                                    break;
                                }
                                else
                                {
                                    STT = crRowLoop[Name[MyConstant.COL_KHVT_STT]].Value.NumericValue + 1;
                                }
                            }
                        }
                        string CodeVL = Guid.NewGuid().ToString();
                        ws.Rows[e.RowIndex].CopyFrom(Rowcopy, PasteSpecial.All);
                        ws.Rows[e.RowIndex].Visible = true;
                        ws.Rows[e.RowIndex][e.ColumnIndex].SetValue(e.Value);
                        ws.Rows[e.RowIndex][Name[MyConstant.COL_KHVT_RowCha]].SetValue(RowCha);
                        ws.Rows[e.RowIndex][Name[MyConstant.COL_KHVT_STT]].SetValue(STT);
                        ws.Rows[e.RowIndex][Name[MyConstant.COL_KHVT_CodeCT]].SetValue(CodeVL);
                        dbString = $"INSERT INTO {QLVT.TBL_QLVT_KHVT} (\"CodeHangMuc\",\"Code\",'{colHeading}') VALUES ('{CodeHM}','{CodeVL}',@NewValue)";


                    }
                    else
                        dbString = $"UPDATE {QLVT.TBL_QLVT_KHVT} SET {colHeading}= @NewValue WHERE \"Code\" = '{Code}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });



                }
            }
            else
            {
                string MaHieu = ws.Rows[e.RowIndex][Name["MaVatTu"]].Value.ToString();
                if (MaHieu == MyConstant.CONST_TYPE_HANGMUC)
                {
                    dbString = $"UPDATE {QLVT.TBL_QLVT_KHVT_HangMuc} SET \"Ten\"= @NewValue WHERE \"Code\" = '{Code}'";
                }
                else if (MaHieu == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    dbString = $"UPDATE {QLVT.TBL_QLVT_KHVT_CongTrinh} SET \"Ten\"= @NewValue WHERE \"Code\" = '{Code}'";
                }
                else
                {
                    colHeading = colHeading == "TenVatTu" ? "VatTu" : colHeading;
                    if (Code == "")
                    {
                        double STT = 1, RowCha = 0;
                        string CodeHM = "";
                        Row Rowcopy = ws.Rows[2];
                        for (int ind = e.RowIndex - 1; ind >= 0; ind--)
                        {
                            Row crRowLoop = ws.Rows[ind];
                            string code = crRowLoop[Name[MyConstant.COL_KHVT_CodeCT]].Value.ToString();
                            string MaVatLieu = crRowLoop[Name["MaVatTu"]].Value.ToString();
                            if (code == "")
                                continue;
                            else if (code != "")
                            {
                                if (MaVatLieu == MyConstant.CONST_TYPE_HANGMUC)
                                {
                                    RowCha = ind + 1;
                                    CodeHM = code;
                                    break;
                                }
                                else
                                {
                                    STT = crRowLoop[Name[MyConstant.COL_KHVT_STT]].Value.NumericValue + 1;
                                }
                            }
                        }
                        string CodeVL = Guid.NewGuid().ToString();
                        string Value = e.Value.ToString();
                        ws.Rows[e.RowIndex].CopyFrom(Rowcopy, PasteSpecial.All);
                        ws.Rows[e.RowIndex].Visible = true;
                        ws.Rows[e.RowIndex][e.ColumnIndex].SetValueFromText(Value);
                        ws.Rows[e.RowIndex][Name[MyConstant.COL_KHVT_RowCha]].SetValue(RowCha);
                        ws.Rows[e.RowIndex][Name[MyConstant.COL_KHVT_STT]].SetValue(STT);
                        ws.Rows[e.RowIndex][Name[MyConstant.COL_KHVT_CodeCT]].SetValue(CodeVL);
                        dbString = $"INSERT INTO {QLVT.TBL_QLVT_KHVT} (\"CodeHangMuc\",\"Code\",'{colHeading}') VALUES ('{CodeHM}','{CodeVL}',@NewValue)";
                    }
                    else
                        dbString = $"UPDATE {QLVT.TBL_QLVT_KHVT} SET {colHeading}= @NewValue WHERE \"Code\" = '{Code}'";
                }
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
            }
        }
        private void spsheet_KeHachVatTu_PopupMenuShowing(object sender, DevExpress.XtraSpreadsheet.PopupMenuShowingEventArgs e)
        {
            Worksheet ws = spsheet_KeHachVatTu.Document.Worksheets.ActiveWorksheet;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            SpreadsheetMenuItem myItem2 = new SpreadsheetMenuItem("Lưu công tác người dùng", new EventHandler(fcn_Handle_Popup_QLVT_LuuDinhMucNguoiDung_KHVT));
            e.Menu.Items.Add(myItem2);
        }
        private void fcn_Handle_Popup_QLVT_LuuDinhMucNguoiDung_KHVT(object sender, EventArgs e)
        {
            Worksheet ws = spsheet_KeHachVatTu.Document.Worksheets.ActiveWorksheet;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange Range = ws.SelectedCell;
            string MaHieu = ws.Rows[Range.TopRowIndex][Name[MyConstant.COL_KHVT_MaVatTu]].Value.ToString();
            string Code = ws.Rows[Range.TopRowIndex][Name[MyConstant.COL_KHVT_CodeCT]].Value.ToString();
            if (MaHieu == MyConstant.CONST_TYPE_HANGMUC || MaHieu == MyConstant.CONST_TYPE_CONGTRINH || Code == "")
                return;
            string Ten = ws.Rows[Range.TopRowIndex][Name[MyConstant.COL_KHVT_TenVatTu]].Value.ToString();
            string Donvi = ws.Rows[Range.TopRowIndex][Name[MyConstant.COL_KHVT_DonVi]].Value.ToString();
            MaHieu = MaHieu == "" ? "TT" : $"{MaHieu}_TT";
            ws.Rows[Range.TopRowIndex][Name[MyConstant.COL_KHVT_MaVatTu]].SetValueFromText(MaHieu);
            ws.Rows[Range.TopRowIndex].Font.Color = Color.Red;
            string dbString = $"INSERT INTO {MyConstant.TBL_TBT_VATTU} (\"Id\",\"MaVatLieu\", \"VatTu\", \"VatTu_KhongDau\", \"DonVi\", \"LoaiVatTu\") VALUES " +
                                $"('{Guid.NewGuid()}', @MaVatLieu, @VatTu, @TenKhongDau, @DonVi, 'Vật liệu')";
            DataProvider.InstanceTBT.ExecuteNonQuery(dbString, parameter: new object[] { MaHieu, Ten, MyFunction.fcn_RemoveAccents(Ten), Donvi });

            dbString = $"UPDATE  {QLVT.TBL_QLVT_KHVT} SET \"MaVatLieu\" = @MaVatLieu WHERE \"Code\"='{Code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { MaHieu });

        }
        public void xtraTabControl8_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var tabControl = (XtraTabControl)sender;
            if (BaseFrom.IsSetNullOnChangedTab)
            {
                tabControl.SelectedTabPage = null;

                return;
            }

            //xtraTabControl_TabMain_EnabledChanged(null, null);
            if (e.Page?.Text == "Nhập kho")
            {
                tL_NhapKho.RefreshDataSource();
                tL_NhapKho.ExpandAll();
            }
            else if (e.Page?.Text == "Xuất kho")
            {
                tL_XuatKho.RefreshDataSource();
                tL_XuatKho.ExpandAll();
            }
            else if (e.Page?.Text == "Quản lý vận chuyển")
            {
                tL_QLVC.RefreshDataSource();
                tL_QLVC.ExpandAll();
            }
            else if (e.Page?.Text == "Chuyển kho")
            {
                tL_ChuyenKho.RefreshDataSource();
                tL_ChuyenKho.ExpandAll();
            }
            else if (e.Page?.Text == "Đề xuất khối lượng")
            {
                tL_YeuCauVatTu.RefreshDataSource();

                tL_YeuCauVatTu.ExpandAll();
            }
            else if (e.Page?.Text == "Thông tin kho vật liệu")
            {
                if (SharedControls.slke_ThongTinDuAn is null)
                    return;
                DataTable dtCT, dtHM;
                DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
                List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, true);
                Fcn_LoadDataTongHop(dtCT, dtHM, Infor);
            }
            else if (e.Page?.Text == "Kế hoạch vật tư")
                fcn_QLVT_KHVT();
            else if (e.Page == xtraTabVatLieuTraVe)
            {
                Fcn_UpdateTraVatTu();
            }
        }
        private void tL_QLVC_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Node.Level == 0 || e.Node.Level == 1)
                tL_QLVC.CancelCurrentEdit();
        }
        private void tL_QLVC_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
            if (e.Column.FieldName == "FileDinhKem" && e.Node.Level <= 1)
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }
        private void tL_QLVC_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.LightSeaGreen;
            }
        }
        private void tL_QLVC_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string dbString = "";
            string TenCotTrongCSDL = e.Column.FieldName;
            PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC qLVC = tL_QLVC.GetFocusedRow() as PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC;
            if (TenCotTrongCSDL != "Chon")
            {
                dbString = $"UPDATE  {QLVT.TBL_QLVT_QLVC} SET \"{TenCotTrongCSDL}\"='{e.Value}' WHERE \"Code\"='{qLVC.ID}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            if (TenCotTrongCSDL == "ThucTeVanChuyen" || TenCotTrongCSDL == "TongSoLuongChuyen" || TenCotTrongCSDL == "KhoiLuong_1Chuyen")
            {
                if (qLVC.ThucTeVanChuyen != 0)
                {
                    dbString = $"SELECT *  FROM {QLVT.TBL_QLVT_NKVC} WHERE \"CodeCha\"='{qLVC.ID}'";
                    DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (dt.Rows.Count == 0)
                    {
                        //dbString = $"SELECT *  FROM {QLVT.TBL_QLVT_QLVC} WHERE \"Code\"='{code}'";
                        //dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        qLVC.LuyKeVanChuyenTheoDot = qLVC.ThucTeVanChuyen;
                        //ws_QLVC.Rows[e.RowIndex][NAMEQLVC["LuyKeVanChuyenTheoDot"]].Formula = $"{ws_QLVC.Rows[e.RowIndex][NAMEQLVC["ThucTeVanChuyen"]].GetReferenceA1()}";
                        //ws_QLVC.Calculate();
                        //luyke = double.Parse(dt.Rows[0]["LuyKeNhapKho"].ToString()) + ws_QLVC.Rows[e.RowIndex][NAMEQLVC["LuyKeVanChuyenTheoDot"]].Value.NumericValue;
                        //ws_QLVC.Rows[e.RowIndex][NAMEQLVC["LuyKeNhapKho"]].SetValue(luyke);
                    }
                    else
                    {
                        double luyke = 0;
                        foreach (DataRow row in dt.Rows)
                            luyke += int.Parse(row["ThucTeVanChuyen"].ToString());
                        luyke = luyke + qLVC.ThucTeVanChuyen;
                        qLVC.LuyKeVanChuyenTheoDot = luyke;

                    }
                }
            }
            tL_QLVC.RefreshDataSource();

        }
        private void tL_QLVC_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (tL_QLVC.FocusedNode == null)
                return;
            if (e.Node.Level < 2)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnActivate = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                btnActivate.Buttons.Clear();
                e.RepositoryItem = btnActivate;
            }
            else if (e.Node.Focused == tL_QLVC.FocusedNode.Focused && e.Column.FieldName == "HoanThoanh_Ok")
            {
                e.RepositoryItem = rIBE_HoanThanh;
            }
        }
        private void rICE_NhapKhiYeuCau_CheckedChanged(object sender, EventArgs e)
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn nhập kho ko qua yêu cầu không?");
            if (rs == DialogResult.Yes)
            {
                VatLieu Vl = tL_YeuCauVatTu.GetFocusedRow() as VatLieu;
                Vl.TrangThai = 2;

            }
        }
        private void rIHP_FileDinhKemYeuCau_Click(object sender, EventArgs e)
        {
            VatLieu VL = tL_YeuCauVatTu.GetFocusedRow() as VatLieu;
            if (VL.ParentID == "0" || VL == null)
                return;
            FormLuaChon luachon = new FormLuaChon(VL.ID, FileManageTypeEnum.QLVC_YeuCauVatTu, $"{VL.TenVatTu}_Đề xuất");
            luachon.ShowDialog();
        }

        private void Uc_DeXuatVatTu_Load(object sender, EventArgs e)
        {
            Fcn_Init();
        }
        private void rIHP_FileDinhKem_NhapVL_Click(object sender, EventArgs e)
        {
            NhapVatLieu VL = tL_NhapKho.GetFocusedRow() as NhapVatLieu;
            if (VL.ParentID == "0" || VL == null)
                return;
            FormLuaChon luachon = new FormLuaChon(VL.ID, FileManageTypeEnum.QLVC_NhapKho, $"{VL.TenVatTu}_Nhập kho");
            luachon.ShowDialog();
        }
        private void rIHP_FileDinhKem_XuatKho_Click(object sender, EventArgs e)
        {
            XuatVatLieu VL = tL_XuatKho.GetFocusedRow() as XuatVatLieu;
            if (VL.ParentID == "0" || VL == null)
                return;
            FormLuaChon luachon = new FormLuaChon(VL.ID, FileManageTypeEnum.QLVC_XuatKho, $"{VL.TenVatTu}_Xuất kho");
            luachon.ShowDialog();
        }
        private async void rIBE_ChuyeKho_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Caption)
            {
                case "Chuyển Kho":
                    ChuyenVatTu CVL = tL_ChuyenKho.GetFocusedRow() as ChuyenVatTu;
                    if (CVL.HoanThanh || CVL.CodeNhapVT == null || CVL.TonKhoChuyenDi == 0)
                        return;
                    if (MessageShower.ShowYesNoQuestion("Bạn có muốn Chuyển Kho?", "Chuyển Kho") != DialogResult.No)
                    {
                        CVL.ThucNhapKhoDen = CVL.TonKhoChuyenDi;
                        tL_ChuyenKho.OptionsBehavior.EditingMode = DevExpress.XtraTreeList.TreeListEditingMode.EditForm;
                        tL_ChuyenKho.CloseEditor();
                        tL_ChuyenKho.OptionsEditForm.PopupEditFormWidth = tL_ChuyenKho.Width;
                        tL_ChuyenKho.ShowEditForm();
                        tL_ChuyenKho.OptionsBehavior.EditingMode = DevExpress.XtraTreeList.TreeListEditingMode.Default;
                    }
                    break;
                case "Đồng ý":
                    ChuyenVatTu CVT = tL_ChuyenKho.GetFocusedRow() as ChuyenVatTu;
                    if (!CVT.HoanThanh)
                        return;
                    if (CVT.TenKhoChuyenDen == CVT.TenKhoChuyenDi || CVT.TenKhoChuyenDen == "")
                    {
                        MessageShower.ShowYesNoQuestion("Tên kho đến và kho đi phải khác nhau!", "Cảnh báo tên kho trùng nhau");
                        return;
                    }
                    CVT.HoanThanh = false;
                    XtraInputBoxArgs args = new XtraInputBoxArgs();
                    args.Caption = "Cài đặt ngày Chuyển Kho";
                    args.Prompt = "Ngày Chuyển Kho";
                    args.DefaultButtonIndex = 0;
                    //args.Showing += Args_Showing_Begin;
                    DateEdit editor = new DateEdit();
                    editor.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
                    editor.Properties.Mask.EditMask = MyConstant.CONST_DATE_FORMAT_SPSHEET;
                    args.Editor = editor;
                    args.DefaultResponse = DateTime.Now.Date;
                    var ngay = "";
                    try
                    {
                        ngay = XtraInputBox.Show(args).ToString();
                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                    var dr = MessageShower.ShowYesNoCancelQuestionWithCustomText("Chọn phương pháp duyệt!", "Lựa chọn", yesString: "Duyệt 1 bước", Nostring: "Duyệt theo quy trình");
                    if (dr == DialogResult.Cancel)
                        return;
                    bool isQuyTrinh = dr == DialogResult.No;
                    WaitFormHelper.ShowWaitForm("Quá trình gửi duyệt đang được tiến hành,Vui lòng chờ!");

                    int countOK = 0;
                    int countBad = 0;
                    int count = 1;
                    var date = DateTime.Parse(ngay);
                    List<string> Noti = new List<string>();
                    if (isQuyTrinh)
                    {
                        var newDXVTHN = new Tbl_QLVT_ChuyenKho_KhoiLuongHangNgayViewModel()
                        {
                            Code = Guid.NewGuid().ToString(),
                            CodeCha = CVT.ID,
                            Ngay = date,
                            KhoiLuong = CVT.ThucNhapKhoDen,
                            DonGia = CVT.DonGia,
                            TrangThai = (int)VatTuStateEnum.DangXetDuyet,
                            //ACapB = item.ACapB
                        };

                        var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<Tbl_QLVT_ChuyenKho_KhoiLuongHangNgayViewModel>(RouteAPI.ApprovalChuyenKho_SendApprovalRequest, newDXVTHN);
                        if (result.MESSAGE_TYPECODE)
                        {
                            countOK++;

                        }
                        else
                        {
                            countBad++;
                            Noti.Add($"{CVT.MaVatTu}: {CVT.TenVatTu}: {result.MESSAGE_CONTENT}");
                        }
                        if (result.Dto != null)
                        {
                            var cvCha = (new List<Tbl_QLVT_ChuyenKho_KhoiLuongHangNgayViewModel>() { result.Dto }).fcn_ObjToDataTable();
                            DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(cvCha, Server.Tbl_QLVT_ChuyenKho_KhoiLuongHangNgay, isCompareTime: false);
                        }
                        DialogResult drs = DialogResult.None;
                        if (countOK == 1)
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
                    else
                    {
                        string dbString = $"INSERT INTO {QLVT.TBL_QLVT_CHUYENKHOVTKLHN} (\"Code\",\"CodeCha\",\"Ngay\",\"KhoiLuong\",\"TenKhoChuyenDen\") " +
    $"VALUES ('{Guid.NewGuid()}','{CVT.ID}','{DateTime.Parse(ngay).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{CVT.ThucNhapKhoDen}', @TenKhoChuyenDen)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { CVT.TenKhoChuyenDen });

                        List<NhapVatLieu> NVL = tL_NhapKho.DataSource as List<NhapVatLieu>;
                        NhapVatLieu NhapVL = NVL.FindAll(x => x.ID == CVT.CodeNhapVT).FirstOrDefault();
                        NhapVL.LuyKeNhapTheoDot = NhapVL.LuyKeNhapTheoDot - CVT.TonKhoChuyenDen;
                        PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC VC = (tL_QLVC.DataSource as List<PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC>).FindAll(x => x.CodeNhapVT == CVT.CodeNhapVT).FirstOrDefault();
                        VC.KhoiLuongDaNhap = NhapVL.LuyKeNhapTheoDot;
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_NHAPVT} SET \"LuyKeNhapTheoDot\"='{ NhapVL.LuyKeNhapTheoDot}' WHERE \"Code\"='{NhapVL.ID}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_QLVC} SET \"LuyKeNhapKho\"='{NhapVL.LuyKeNhapTheoDot}' WHERE \"CodeNhapVT\"='{NhapVL.ID}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                        string codeNhapVT = Guid.NewGuid().ToString();
                        string codedexuat = Guid.NewGuid().ToString();
                        dbString = $"INSERT INTO '{QLVT.TBL_QLVT_YEUCAUVT}' (\"TrangThai\",\"Code\",\"TenVatTu\",\"MaVatTu\",\"DonVi\",\"CodeGiaiDoan\",\"CodeHangMuc\",\"IsDone\") " +
    $"VALUES ('{2}','{codedexuat}',@TenVatTu, @MaVatTu, @DonVi,'{SharedControls.cbb_DBKH_ChonDot.SelectedValue}',@TenKhoChuyenDen,'{true}')";

                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { CVT.TenVatTu, CVT.MaVatTu, CVT.DonVi, CVT.TenKhoChuyenDen });
                        dbString = $"INSERT INTO {QLVT.TBL_QLVT_NHAPVT} (\"IsChuyenKho\",\"TrangThai\",\"CodeDeXuat\",\"Code\",\"CodeGiaiDoan\",\"TenKhoNhap\",\"DeXuatVatTu\",\"DaDuyetDeXuat\") VALUES " +
                            $"('{true}','{4}','{codedexuat}','{codeNhapVT}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}',@TenKhoNhap,'{CVT.ThucNhapKhoDen}','{CVT.ThucNhapKhoDen}')";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { CVT.TenKhoChuyenDen });
                        DataTable dtCT, dtHM;
                        DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
                        Fcn_LoadDataNhapKho(dtCT, dtHM);
                        CVT.LuyKeThucNhapKhoDen = CVT.TonKhoChuyenDen;
                        CVT.ThucNhapKhoDen = 0;
                        dbString = $"UPDATE  {QLVT.TBL_QLVT_CHUYENKHOVT} SET \"TonKhoChuyenDen\"='{ CVT.LuyKeThucNhapKhoDen}',\"TonKhoChuyenDi\"='{ CVT.TonKhoChuyenDi - CVT.TonKhoChuyenDen}' WHERE \"Code\"='{CVT.ID}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        CVT.TenKhoChuyenDen = null;
                        CVT.TonKhoChuyenDi = CVT.TonKhoChuyenDi - CVT.TonKhoChuyenDen;

                        tL_ChuyenKho.RefreshDataSource();
                        tL_NhapKho.RefreshDataSource();
                        tL_XuatKho.RefreshDataSource();
                        tL_QLVC.RefreshDataSource();
                        fcn_UpdateChuyenKho();
                        Fcn_UpdateTenKho();
                        MessageShower.ShowInformation("Chuyển kho thành công!!!!!!!!!!");
                        WaitFormHelper.CloseWaitForm();
                    }
                    break;
            }
        }
        private void rIHP_FileDinhKem_QLVC_Click(object sender, EventArgs e)
        {
            PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC VL = tL_QLVC.GetFocusedRow() as PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC;
            if (VL.ParentID == "0" || VL == null)
                return;
            FormLuaChon luachon = new FormLuaChon(VL.ID, FileManageTypeEnum.QLVC_QuanLyVanChuyen, $"{VL.TenVatTu}_Quản lý vận chuyển");
            luachon.ShowDialog();
        }
        private void rIHPL_NhatKy_Click(object sender, EventArgs e)
        {
            PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC qLVC = tL_QLVC.GetFocusedRow() as PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC;
            Form_QLVT_NhatKy QLNK = new Form_QLVT_NhatKy(qLVC.ID, qLVC.TenVatTu);
            QLNK.ShowDialog();
        }
        private void rIBE_HoanThanh_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Bạn muốn hoàn thành nhập dữ liệu! Vui lòng kiểm tra kỹ trước khi nhấn nút đồng ý ?", "Cảnh Báo!");
            if (dialogResult == DialogResult.Yes)
            {
                var ngay = "";
                try
                {
                    XtraInputBoxArgs args = new XtraInputBoxArgs();
                    args.Caption = "Cài đặt ngày vận chuyển vật tư";
                    args.Prompt = "Ngày Vận Chuyển";
                    args.DefaultButtonIndex = 0;
                    //args.Showing += Args_Showing_Begin;
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
                PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC qLVC = tL_QLVC.GetFocusedRow() as PhanMemQuanLyThiCong.Model.QuanLyVanChuyen.QLVC;
                tL_NhapKho.RefreshDataSource();
                tL_XuatKho.RefreshDataSource();
                NhapVatLieu NVL = (tL_NhapKho.DataSource as List<NhapVatLieu>).FindAll(x => x.ID == qLVC.CodeNhapVT).FirstOrDefault();
                XuatVatLieu XVL = (tL_XuatKho.DataSource as List<XuatVatLieu>).FindAll(x => x.CodeNhapVT == qLVC.CodeNhapVT).FirstOrDefault();
                string dbString = $"INSERT INTO {QLVT.TBL_QLVT_NKVC} (\"Ngay\",\"DonGia\",\"Code\",\"CodeCha\",\"TaiXe\",\"BienSoXe\",\"KichThuocThungXe\",\"KhoiLuong_1Chuyen\",\"TongSoLuongChuyen\",\"ThucTeVanChuyen\")" +
                    $" VALUES ('{DateTime.Parse(ngay).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{qLVC.DonGia}','{Guid.NewGuid()}','{qLVC.ID}','{qLVC.TaiXe}','{qLVC.BienSoXe}','{qLVC.KichThuocThungXe}','{qLVC.KhoiLuong_1Chuyen}','{qLVC.TongSoLuongChuyen}','{qLVC.ThucTeVanChuyen}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                //string[] DB = { "HoanThoanh_Ok", "TaiXe", "BienSoXe", "KichThuocThungXe", "KhoiLuong_1Chuyen", "TongSoLuongChuyen", "ThucTeVanChuyen" };
                //foreach (string item in DB)
                //    ws_QLVC.Rows[e.RowIndex][NAMEQLVC[item]].SetValueFromText("");
                dbString = $"UPDATE  {QLVT.TBL_QLVT_QLVC} SET \"LuyKeNhapKho\"='{qLVC.LuyKeNhapKho}',\"LuyKeVanChuyenTheoDot\"='{qLVC.LuyKeVanChuyenTheoDot}' WHERE \"Code\"='{qLVC.ID}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                dbString = $"UPDATE  {QLVT.TBL_QLVT_NHAPVT} SET \"LuyKeNhapTheoDot\"='{qLVC.LuyKeNhapKho}' WHERE \"Code\"='{qLVC.CodeNhapVT}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                dbString = $"UPDATE  {QLVT.TBL_QLVT_XUATVT} SET \"LuyKeNhapTheoDot\"='{qLVC.LuyKeNhapKho}' WHERE \"CodeNhapVT\"='{qLVC.CodeNhapVT}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                NVL.LuyKeNhapTheoDot = qLVC.LuyKeNhapKho;
                XVL.LuyKeNhapTheoDot = qLVC.LuyKeNhapKho;
                qLVC.KhoiLuongDaNhap = qLVC.LuyKeNhapKho;
                qLVC.GenerateSource(qLVC);
                tL_QLVC.RefreshDataSource();
            }
            else
                return;
            Fcn_UpdateVatLieuVC();
        }

        private void btn_TraVatTu_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Bạn muốn trả vật tư không???? Vui lòng kiểm tra kỹ trước khi nhấn nút đồng ý ?", "Cảnh Báo!");
            if (dialogResult == DialogResult.Yes)
            {
                var ngay = "";
                try
                {
                    XtraInputBoxArgs args = new XtraInputBoxArgs();
                    args.Caption = "Cài đặt ngày trả vật tư";
                    args.Prompt = "Ngày Trả vật tư";
                    args.DefaultButtonIndex = 0;
                    //args.Showing += Args_Showing_Begin;
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
                NhapVatLieu NVL = tL_NhapKho.GetFocusedRow() as NhapVatLieu;
                var date = DateTime.Parse(ngay);
                if (string.IsNullOrEmpty(NVL.ID) || NVL.TrangThai == 2 || NVL.LuyKeNhapTheoDot == 0)
                {
                    MessageShower.ShowError("Bạn chưa nhập kho hoặc khối lượng nhập kho đang không tồn tại!!!!!");
                    return;
                }
                if (NVL.KhoiLuongTra > NVL.LuyKeNhapTheoDot||NVL.KhoiLuongTra==0)
                {
                    MessageShower.ShowError("Khối lượng trả đang bằng không hoặc vượt quá lũy kế nhập theo đợt!!!Vui lòng nhập lại khối lượng trả!!!!!");
                    return;
                }
                if(NVL.TenKhoNhap is null)
                {
                    MessageShower.ShowError("Tên kho nhập không tồn tại!!!Vui lòng nhập lại tên kho nhập!!!!!");
                    return;
                }
                NVL.LuyKeNhapTheoDot = NVL.LuyKeNhapTheoDot - NVL.KhoiLuongTra;
                string dbString = $"UPDATE  {QLVT.TBL_QLVT_NHAPVT} SET \"LuyKeNhapTheoDot\"='{NVL.LuyKeNhapTheoDot}' WHERE \"Code\"='{NVL.ID}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                dbString = $"INSERT INTO '{QLVT.Tbl_QLVT_TraVatTu}' (\"Code\",\"TenKhoTra\",\"KhoiLuong\",\"Ngay\",\"DonGia\",\"CodeNhapVatTu\") " +
$"VALUES ('{Guid.NewGuid()}','{NVL.TenKhoNhap}','{NVL.KhoiLuongTra}','{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{NVL.DonGia}','{NVL.ID}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                NVL.KhoiLuongTra = 0;
                tL_NhapKho.SetRowCellValue(tL_NhapKho.FocusedNode, "LuyKeNhapTheoDot", NVL.LuyKeNhapTheoDot);
                tL_NhapKho.SetRowCellValue(tL_NhapKho.FocusedNode, "KhoiLuongTra", 0);
                MessageShower.ShowInformation("Trả vật tư thành công!!!!!!!!!!");
            }
            else
                return;
        }

        private void tL_NhapKho_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {

        }

        private void btn_TraVTXuatKho_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Bạn muốn trả vật tư không???? Vui lòng kiểm tra kỹ trước khi nhấn nút đồng ý ?", "Cảnh Báo!");
            if (dialogResult == DialogResult.Yes)
            {
                var ngay = "";
                try
                {
                    XtraInputBoxArgs args = new XtraInputBoxArgs();
                    args.Caption = "Cài đặt ngày trả vật tư";
                    args.Prompt = "Ngày Trả vật tư";
                    args.DefaultButtonIndex = 0;
                    //args.Showing += Args_Showing_Begin;
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
                XuatVatLieu XVL = tL_XuatKho.GetFocusedRow() as XuatVatLieu;
                var date = DateTime.Parse(ngay);
                if (string.IsNullOrEmpty(XVL.ID) || XVL.TrangThai == 1|| XVL.LuyKeXuatTheoDot==0)
                {
                    MessageShower.ShowError("Bạn chưa xuất kho hoặc khối lượng xuất kho đang không tồn tại!!!!!");
                    return;
                }
                if (XVL.KhoiLuongTra > XVL.LuyKeXuatTheoDot || XVL.KhoiLuongTra == 0)
                {
                    MessageShower.ShowError("Khối lượng trả đang bằng không hoặc vượt quá lũy kế xuất theo đợt!!!Vui lòng nhập lại khối lượng trả!!!!!");
                    return;
                }
                XVL.LuyKeXuatTheoDot = XVL.LuyKeXuatTheoDot - XVL.KhoiLuongTra;
                string dbString = $"UPDATE  {QLVT.TBL_QLVT_XUATVT} SET \"LuyKeXuatTheoDot\"='{XVL.LuyKeXuatTheoDot}' WHERE \"Code\"='{XVL.ID}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                dbString = $"INSERT INTO '{QLVT.Tbl_QLVT_TraVatTu}' (\"Code\",\"TenKhoTra\",\"KhoiLuong\",\"Ngay\",\"DonGia\",\"CodeXuatVatTu\") " +
$"VALUES ('{Guid.NewGuid()}','{XVL.TenKhoNhap}','{XVL.KhoiLuongTra}','{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{XVL.DonGia}','{XVL.ID}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                XVL.KhoiLuongTra = 0;
                tL_XuatKho.SetRowCellValue(tL_XuatKho.FocusedNode, "LuyKeXuatTheoDot", XVL.LuyKeXuatTheoDot);
                tL_XuatKho.SetRowCellValue(tL_XuatKho.FocusedNode, "KhoiLuongTra", 0);
                MessageShower.ShowInformation("Trả vật tư thành công!!!!!!!!!!");
            }
            else
                return;
        }
        private void Fcn_UpdateTraVatTu()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu phần Quản lý vận chuyển", "Vui Lòng chờ!");
            string dbString = $"SELECT TVT.*,TVT.Code as ID,COALESCE(YCNVT.TenVatTu, YCXVT.TenVatTu) AS TenVatTu," +
                $"COALESCE(YCNVT.DonVi, YCXVT.DonVi) AS DonVi," +
                $"COALESCE(YCNVT.CodeHangMuc, YCXVT.CodeHangMuc) AS CodeHangMuc," +
                $"HM.Ten as TenHangMuc,CTHM.Ten as TenCongTrinh,CTHM.Code as CodeCongTrinh " +
   $" FROM {QLVT.Tbl_QLVT_TraVatTu} TVT" +
   $" LEFT JOIN {QLVT.TBL_QLVT_NHAPVT} NVT ON NVT.Code=TVT.CodeNhapVatTu " +
   $" LEFT JOIN {QLVT.TBL_QLVT_XUATVT} XVT ON XVT.Code=TVT.CodeXuatVatTu " +
   $" LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} YCNVT ON YCNVT.Code=NVT.CodeDeXuat " +
   $" LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} YCXVT ON YCXVT.Code=XVT.CodeDeXuat" +
   $" LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} HM ON HM.Code=COALESCE(YCNVT.CodeHangMuc, YCXVT.CodeHangMuc) " +
   $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} CTHM ON CTHM.Code=HM.CodeCongTrinh " +
   $" WHERE (YCNVT.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}') OR (YCXVT.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}')";
            List<QLVC_TraVatTu> Infor = DataProvider.InstanceTHDA.ExecuteQueryModel<QLVC_TraVatTu>(dbString);
            List<QLVC_TraVatTu> VatLieu = new List<QLVC_TraVatTu>();
            var grCtrinh = Infor.GroupBy(x => x.CodeCongTrinh);
            foreach(var ctrinh in grCtrinh)
            {
                VatLieu.Add(new QLVC_TraVatTu()
                {
                    ParentID = "0",
                    ID = ctrinh.Key,
                    //MaVatTu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenVatTu = ctrinh.FirstOrDefault().TenCongTrinh,
                });
                var grHM = ctrinh.GroupBy(x => x.CodeHangMuc);
                foreach(var HM in grHM)
                {
                    VatLieu.Add(new QLVC_TraVatTu()
                    {
                        ParentID = ctrinh.Key,
                        ID = HM.Key,
                        TenVatTu = HM.FirstOrDefault().TenHangMuc,
                    });
                    var grTenVatTu = HM.GroupBy(x => x.ID);
                    foreach(var VT in grTenVatTu)
                    {
                        foreach(var itemVT in VT)
                        {
                            itemVT.ParentID = HM.Key;
                            VatLieu.Add(itemVT);
                        }
                    }
                }
            }
            tl_TraVatTu.DataSource = VatLieu;
            tl_TraVatTu.ExpandAll();
            tl_TraVatTu.RefreshDataSource();
            tl_TraVatTu.Refresh();
            WaitFormHelper.CloseWaitForm();
        }

        private void tl_TraVatTu_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
            else if ((e.Column.FieldName == "Ngay" || e.Column.FieldName == "ThanhTien") && e.Node.Level <= 1)
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tl_TraVatTu_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
                return;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.LightSeaGreen;
                return;
            }
        }
    }
}
