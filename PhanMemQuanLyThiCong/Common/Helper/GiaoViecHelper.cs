using DevExpress.Spreadsheet;
using DevExpress.Utils.Extensions;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Controls;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.PermissionControl;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using PhanMemQuanLyThiCong.Model.TDKH;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class GiaoViecHelper
    {

        //public static List<string> GetConditionCongTacGiaoViec(string CVChaBrief = null)
        //{
        //    List<string> cons = new List<string>();
        //    CVChaBrief = CVChaBrief ?? GiaoViec.TBL_CONGVIECCHA;

        //    DonViThucHien dvth = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH as DonViThucHien;
        //    var crDuAn = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;

        //    if (dvth is null || crDuAn is null)
        //    {
        //        SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.Enabled = false;
        //        return;
        //    }

        //    string conString = "";


        //    var allPermission = BaseFrom.allPermission;

        //    if (allPermission.HaveInitProjectPermission
        //        || allPermission.AllProjectThatUserIsAdmin.Contains(crDuAn.Code)
        //        || allPermission.AllContractorThatUserIsAdmin.Contains(dvth.Code))
        //    {

        //    }
        //    else if (allPermission.AllTask.Any())
        //    {
        //        conString = $" AND CodeCongViecCha IN ({MyFunction.fcn_Array2listQueryCondition(allPermission.AllTask)}) ";

        //    }
        //    else
        //    {
        //        conString = " LIMIT 0 ";
        //    }
        //    return null;
        //}
        public static void LoadAllKeHoachGiaoViecThiCong()
        {
            string colNgayBatDau = GiaoViec.COL_NgayBatDau;
            string colNgayKetThuc = GiaoViec.COL_NgayKetThuc;
            string colSoNgay = GiaoViec.COL_SoNgay;


            DonViThucHien dvth = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH as DonViThucHien;
            var crDuAn = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;
            if (dvth is null || crDuAn is null)
            {
                SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.Enabled = false;
                return;
            }

            string conString = "";
            string Limit = "";


            var allPermission = BaseFrom.allPermission;

            if (allPermission.HaveInitProjectPermission
                || allPermission.AllProjectThatUserIsAdmin.Contains(crDuAn.Code)
                || allPermission.AllContractorThatUserIsAdmin.Contains(dvth.Code))
            {
                
            }
            else if (allPermission.AllTask.Any())
            {
                conString = $" AND CodeCongViecCha IN ({MyFunction.fcn_Array2listQueryCondition(allPermission.AllTask)}) ";
            }
            else
            {
                Limit = " LIMIT 0 ";
            }


            string colFk = dvth.ColCodeFK;
            string code = dvth.Code;
            FileHelper.fcn_spSheetStreamDocument(SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec, $@"{BaseFrom.m_templatePath}\FileExcel\13.aKeHoachGiaoViec.xlsx");
            SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.Enabled = true;
            //SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.CloseCellEditor(CellEditorEnterValueMode.Cancel);

            IWorkbook wb = SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.Document;
////          wb.History.IsEnabled = false;
            Worksheet ws = wb.Worksheets[MyConstant.CONST_SheetName_KHGV_KeHoach];
            SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.BeginUpdate();
            CellRange range = wb.Range[MyConstant.Range_KeHoach];

            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(range);

            string condTask = "";
            if (!BaseFrom.allPermission.HaveInitProjectPermission)
            {
                ws.Columns[dic[GiaoViec.COL_KLKeHoach]].Visible = false;
                ws.Columns[dic[GiaoViec.COL_KLHopDong]].Visible = false;
                ws.Columns[dic[GiaoViec.COL_DonGiaKeHoach]].Visible = false;
                //condTask = $"{GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha IN ({MyFunction.fcn_Array2listQueryCondition(BaseFrom.allPermission)})";
            }

            //DataTable dtCT, dtHM;
            //MyFunction.fcn_GetTblCongTrinhVaHangMucOfDuAn(SharedControls.slke_ThongTinDuAn.EditValue.ToString(), out dtCT, out dtHM);
            string dbString = $"SELECT {MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc," +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh," +
                $"TenDuAn, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc " +
                $"FROM {MyConstant.TBL_THONGTINHANGMUC} " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code " +
                $"JOIN {MyConstant.TBL_THONGTINDUAN} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code " +
                $"WHERE {MyConstant.TBL_THONGTINDUAN}.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' " +
                $"ORDER BY " +
                $"{MyConstant.TBL_THONGTINDUAN}.CreatedOn DESC, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.CreatedOn DESC, " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.CreatedOn DESC ";

            DataTable dtHangMuc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lsHMCondition = MyFunction.fcn_Array2listQueryCondition(dtHangMuc.AsEnumerable().Select(x => x["CodeHangMuc"].ToString()).ToArray());

            //Lấy datatable của các công việc cha và công việc con hiện tại
            dbString = $"SELECT COALESCE({TDKH.TBL_ChiTietCongTacTheoKy}.TrangThai, {GiaoViec.TBL_CONGVIECCHA}.TrangThai) AS TrangThai, " +
                $"{GiaoViec.TBL_CONGVIECCHA}.*, COUNT({GiaoViec.TBL_FileDinhKem}.Code) AS FileCount " +
                $"FROM {GiaoViec.TBL_CONGVIECCHA} " +
                $"LEFT JOIN {GiaoViec.TBL_FileDinhKem} " +
                $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha = {GiaoViec.TBL_FileDinhKem}.CodeParent " +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeCongTacTheoGiaiDoan = {TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                $"WHERE {GiaoViec.TBL_CONGVIECCHA}.CodeHangMuc IN ({lsHMCondition}) AND (CodeCongTacTheoGiaiDoan IS NULL OR CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}') " +
                $"AND {GiaoViec.TBL_CONGVIECCHA}.{colFk} = '{code}' {conString} " +
                $"GROUP BY {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha " +
                $"ORDER BY \"SortId\" ASC {Limit}";
            DataTable dtCVCha = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            int num = dtCVCha.Rows.Count;
            dtCVCha.AsEnumerable().Where(x => x["CodeCongTacTheoGiaiDoan"] != DBNull.Value)
                .ForEach(x => x["SortId"] = (int)x["SortId"] + num);

            dtCVCha.DefaultView.Sort = "SortId ASC";
            dtCVCha = dtCVCha.DefaultView.ToTable();

            string lsCtChaCondition = MyFunction.fcn_Array2listQueryCondition(dtCVCha.AsEnumerable().Select(x => x["CodeCongViecCha"].ToString()).ToArray());

            dbString = $"SELECT {GiaoViec.TBL_CONGVIECCON}.*, COUNT({GiaoViec.TBL_CongViecCon_FileDinhKem}.Code) AS FileCount FROM {GiaoViec.TBL_CONGVIECCON} " +
                $"LEFT JOIN {GiaoViec.TBL_CongViecCon_FileDinhKem} " +
                $"ON {GiaoViec.TBL_CongViecCon_FileDinhKem}.CodeParent = {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCon " +
                $"WHERE " +
                $"\"CodeCongViecCha\" IN ({lsCtChaCondition}) " +
                $"GROUP BY {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCon " +
                $"ORDER BY \"CreatedOn\" ASC";
            DataTable dtCVCon = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string lsCtConCondition = MyFunction.fcn_Array2listQueryCondition(dtCVCon.AsEnumerable().Select(x => x["CodeCongViecCon"].ToString()).ToArray());

            dbString = $"SELECT * FROM {GiaoViec.TBL_CHIATHICONG} WHERE " +
                $"\"CodeCongViecCon\" IN ({lsCtConCondition})";
            DataTable dtChiaThiCong = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            int numGroup = dtCVCha.AsEnumerable().Where(x => x["CodeNhom"].ToString() != "").Select(x => x["CodeNhom"].ToString()).Distinct().Count() +
                dtCVCon.AsEnumerable().Where(x => x["CodeNhom"].ToString() != "").Select(x => x["CodeNhom"].ToString()).Distinct().Count();

            var grsCongTrinh = dtHangMuc.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);

            ws.Rows.Insert(wb.Range[MyConstant.Range_KeHoach].BottomRowIndex,
                grsCongTrinh.Count() + dtHangMuc.Rows.Count*2 /** 5*/ + dtCVCha.Rows.Count /** 2*/ + dtCVCon.Rows.Count + numGroup + dtChiaThiCong.Rows.Count + 10, RowFormatMode.FormatAsPrevious);

            int crRowInd = wb.Range[MyConstant.Range_KeHoach].TopRowIndex + 1;
            string crCodeCT = "", crCodeHM = "", crCodeCVCha = "", crCodeCVCon = "", crCodeNhomCha = "", crCodeNhomCon = "";
            Row crRowCT, crRowHM, crRowCTCha, crRowCTCon;
            //Cập nhật hàng công trình
            foreach (var grCTrinh in grsCongTrinh)
            {
                
                DataRow drCT = grCTrinh.First();
                Row crRow = crRowCT = ws.Rows[crRowInd++];
                string tenDA = drCT["TenDuAn"].ToString();
                crCodeCT = drCT["CodeCongTrinh"].ToString();
                crRow[dic[GiaoViec.COL_CodeCT]].SetValueFromText(crCodeCT);
                //if (crCodeCVCha != "")
                //{
                //    crRow = ws.Rows[crRowInd++];
                //}
                crCodeCVCha = "";

                crRow.Font.Color = MyConstant.color_Row_CongTrinh;
                crRow.Font.Bold = true;

                crRow[dic[GiaoViec.COL_TypeRow]].SetValueFromText(MyConstant.TYPEROW_CongTrinh);

                crRow[dic[GiaoViec.COL_MaDinhMuc]].SetValueFromText(MyConstant.CONST_TYPE_CONGTRINH);
                crRow[dic[GiaoViec.COL_TenCongViec]].SetValueFromText(drCT["TenCongTrinh"].ToString());
                //crRow[dic[GiaoViec.COL_TenDuAn]].SetValueFromText(drCT["TenDuAn"].ToString());

                //Lấy hạng mục trong công trình
                DataRow[] HMs = grCTrinh.ToArray();
                //Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.Range_KeHoach]);
                foreach (DataRow drHM in HMs)
                {
                    crRow = crRowHM = ws.Rows[crRowInd++];
                    crRow[dic[GiaoViec.COL_CodeCT]].SetValue(drHM["CodeHangMuc"]);
                    crRow[dic[GiaoViec.COL_RowCha]].Formula = $"ROW(A{crRowCT.Index + 1})";
                    //if (crCodeCVCha != "")
                    //{
                    //    crRow = ws.Rows[crRowInd++];
                    //}
                    crCodeCVCha = "";

                    crRow.Font.Color = MyConstant.color_Row_HangMuc;
                    crRow.Font.Bold = true;

                    crRow[dic[GiaoViec.COL_TypeRow]].SetValueFromText(MyConstant.TYPEROW_HangMuc);

                    crRow[dic[GiaoViec.COL_MaDinhMuc]].SetValueFromText(MyConstant.CONST_TYPE_HANGMUC);
                    crRow[dic[GiaoViec.COL_TenCongViec]].SetValueFromText(drHM["TenHangMuc"].ToString());
                    //crRow[dic[GiaoViec.COL_TenDuAn]].SetValueFromText(drCT["TenDuAn"].ToString());

                    crCodeHM = drHM["CodeHangMuc"].ToString();
                    //crRow[dic[GiaoViec.COL_CongTrinh].SetValueFromText(crCodeCT);
                    //crRow[dic[GiaoViec.COL_HangMuc].SetValueFromText(crCodeHM);

                    //Lấy công việc con trong hạng mục
                    DataRow[] CVChas = dtCVCha.Select($"[CodeHangMuc] = '{crCodeHM}'");

                    int STTCha = 0;

                    foreach (DataRow drCVCha in CVChas)
                    {

                        //Kiểm tra nhóm và thêm nhóm
                        string codeNhom = (drCVCha["CodeNhom"] == DBNull.Value) ? "" : drCVCha["CodeNhom"].ToString();
                        //if (crCodeCVCha != "")
                        //{
                        //    crRow = ws.Rows[crRowInd++];
                        //}


                        int crIndRowCha = crRowHM.Index;
                        if (codeNhom != "" && codeNhom != crCodeNhomCha)
                        {
                            dbString = $"SELECT * FROM \"{GiaoViec.TBL_NhomCongTacCha}\" WHERE \"Code\" = '{codeNhom}'";
                            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            if (dt.Rows.Count == 0)
                            {
                                MessageShower.ShowInformation("Lỗi tải danh sách nhóm");
                            }
                            else
                            {
                                crCodeNhomCha = codeNhom;
                                //crRowWs.Insert();
                                crRow.Font.Color = MyConstant.color_Row_NhomCongTac;
                                crRow.Font.Bold = true;

                                crRow[dic[GiaoViec.COL_MaDinhMuc]].SetValueFromText(MyConstant.CONST_TYPE_NHOM);
                                crRow[dic[GiaoViec.COL_TenCongViec]].SetValueFromText(dt.Rows[0]["Ten"].ToString());
                                crRow[dic[GiaoViec.COL_CodeCT]].SetValueFromText(codeNhom);
                                //crRow[dic[GiaoViec.COL_TenDuAn]].SetValueFromText(drCT["TenDuAn"].ToString());
                                crRow[dic[GiaoViec.COL_RowCha]].Formula = $"ROW(A{crRowHM.Index + 1})";
                                crIndRowCha = crRowInd - 1;

                                crRow = ws.Rows[crRowInd++];
                            }
                        }

                        crRow = crRowCTCha = ws.Rows[crRowInd++];
                        crRow[dic[GiaoViec.COL_CodeCT]].SetValue(drCVCha["CodeCongViecCha"]);
                        //crRow[dic[GiaoViec.COL_STT]].SetValue(drCVCha["CodeCongViecCha"]);
                        crRow[dic[GiaoViec.COL_RowCha]].Formula = $"ROW(A{crIndRowCha + 1})";
                        //crRow[dic[GiaoViec.COL_TenDuAn]].SetValueFromText(drCT["TenDuAn"].ToString());

                        crRow[dic[GiaoViec.COL_ThanhTienKeHoach]].Formula = $"{dic[GiaoViec.COL_DonGiaKeHoach]}{crRow.Index + 1}*{dic[GiaoViec.COL_KLKeHoach]}{crRow.Index + 1}";
                        crRow[dic[GiaoViec.COL_STT]].SetValue(++STTCha);

                        crRow.Font.Color = (crCodeNhomCha != "" && codeNhom == crCodeNhomCha) ? MyConstant.color_Row_NhomCongTac : MyConstant.color_Nomal;
                        crRow.Font.Bold = true;
                        crRow[dic[GiaoViec.COL_TenCongViec]].Alignment.Indent = 1;
                        crRow[dic[GiaoViec.COL_TypeRow]].SetValueFromText(MyConstant.TYPEROW_CVCha);
                        crRow[dic[colSoNgay]].Formula = $"{dic[colNgayKetThuc]}{crRow.Index + 1} - {dic[colNgayBatDau]}{crRow.Index + 1} + 1";
                        //crRow[dic[GiaoViec.COL_TenDuAn]].SetValueFromText(drCT["TenDuAn"].ToString());

                        if (drCVCha["CodeCongTacTheoGiaiDoan"] != DBNull.Value)
                            crRow[dic[GiaoViec.COL_STT]].Font.Color = MyColor.GiaoViecFromTDKH;

                        crCodeCVCha = drCVCha["CodeCongViecCha"].ToString();
                        foreach (var item in dic)
                        {
                            //if (item.Key == GiaoViec.COL_CodeCT)
                            //    continue;
                            if (item.Key == GiaoViec.COL_NguoiThucHien)
                            {
                                ws.Hyperlinks.Add(crRow[item.Value], $"{item.Value}{crRow.Index + 1}", false, "Xem chi tiết");
                            }
                            else if (item.Key == GiaoViec.COL_FileDinhKem)
                            {
                                ws.Hyperlinks.Add(crRow[item.Value], $"{item.Value}{crRow.Index + 1}", false, $"{drCVCha["FileCount"]} Files");
                            }
                            if (dtCVCha.Columns.Contains(item.Key))
                                MyFunction.SetValueToCellWorksheet(crRow[item.Value], drCVCha[item.Key]);
                        }

                        DataRow[] CVCons = dtCVCon.Select($"[CodeCongViecCha] = '{crCodeCVCha}'");

                        int STTCon = 0;
                            int crIndRowNhomCon = crRowCTCha.Index;
                        foreach (DataRow drCVCon in CVCons)
                        {
                            crCodeCVCon = drCVCon["CodeCongViecCon"].ToString();
                            crRow = crRowCTCon = ws.Rows[crRowInd++];
                            crRow[dic[GiaoViec.COL_CodeCT]].SetValueFromText(crCodeCVCon);
                            //crRow[dic[GiaoViec.COL_TenDuAn]].SetValueFromText(drCT["TenDuAn"].ToString());

                            crRowCTCon = crRow;
                            //Kiểm tra nhóm và thêm nhóm
                            codeNhom = (drCVCon["CodeNhom"] == null) ? "" : drCVCon["CodeNhom"].ToString();

                            if (codeNhom != "" && codeNhom != crCodeNhomCon)
                            {
                                dbString = $"SELECT * FROM \"{GiaoViec.TBL_NhomCongTacCon}\" WHERE \"Code\" = '{codeNhom}'";
                                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                                if (dt.Rows.Count == 0)
                                {
                                    MessageShower.ShowInformation("Lỗi tải danh sách nhóm công tác con");
                                }
                                else
                                {
                                    crCodeNhomCon = codeNhom;
                                    //crRowWs.Insert();
                                    crRow.Font.Color = MyConstant.color_Row_NhomDienGiai;
                                    crRow.Font.Bold = true;
                                    crRow[dic[GiaoViec.COL_TenCongViec]].Alignment.Indent = 2;
                                    crRow[dic[GiaoViec.COL_MaDinhMuc]].SetValueFromText(MyConstant.CONST_TYPE_NHOMDIENGIAI);
                                    crRow[dic[GiaoViec.COL_TenCongViec]].SetValueFromText(dt.Rows[0]["Ten"].ToString());
                                    crRow[dic[GiaoViec.COL_CodeCT]].SetValueFromText(codeNhom);
                                    crRow[dic[GiaoViec.COL_RowCha]].Formula = $"ROW(A{crRowCTCha.Index + 1})";
                                    crIndRowNhomCon = crRowInd - 1;

                                    //crRow[dic[GiaoViec.COL_TenDuAn]].SetValueFromText(drCT["TenDuAn"].ToString());

                                    crRow = ws.Rows[crRowInd++];
                                }
                                crRow[dic[GiaoViec.COL_RowCha]].Formula = $"ROW(A{crIndRowNhomCon + 1})";

                            }
                            else
                                crRow[dic[GiaoViec.COL_RowCha]].Formula = $"ROW(A{crRowCTCha.Index + 1})";

                            crRow.Font.Color = (crCodeNhomCon == codeNhom) ? MyConstant.color_Row_NhomDienGiai : MyConstant.color_Row_DienGiai;
                            crRow.Font.Bold = false;

                            crRow[dic[GiaoViec.COL_TypeRow]].SetValueFromText(MyConstant.TYPEROW_CVCON);
                            crRow[dic[GiaoViec.COL_STT]].SetValueFromText($"{STTCha}.{++STTCon}");
                            crRow[dic[GiaoViec.COL_ThanhTienKeHoach]].Formula = $"{dic[GiaoViec.COL_DonGiaKeHoach]}{crRow.Index + 1}*{dic[GiaoViec.COL_KLKeHoach]}{crRow.Index + 1}";

                            crRow[dic[GiaoViec.COL_TenCongViec]].Alignment.Indent = 3;

                            crRow[dic[GiaoViec.COL_CodeCT]].SetValueFromText(crCodeCVCon);
                            crRow[dic[colSoNgay]].Formula = $"{dic[colNgayKetThuc]}{crRow.Index + 1} - {dic[colNgayBatDau]}{crRow.Index + 1} + 1";
                            //crRow[dic[GiaoViec.COL_TenDuAn]].SetValueFromText(drCT["TenDuAn"].ToString());

                            foreach (var item in dic)
                            {
   
                                if (dtCVCon.Columns.Contains(item.Key))
                                    MyFunction.SetValueToCellWorksheet(crRow[item.Value], drCVCon[item.Key]);
                                else if (item.Key == GiaoViec.COL_FileDinhKem)
                                {
                                    ws.Hyperlinks.Add(crRow[item.Value], $"{item.Value}{crRow.Index + 1}", false, $"{drCVCon["FileCount"]} Files");
                                }
                            }

                            //Lấy công tác đã chia thi công
                            DataRow[] ThiCongs = dtChiaThiCong.Select($"[CodeCongViecCon] = '{crCodeCVCon}'");

                            foreach (DataRow drThiCong in ThiCongs)
                            {
                                crRow = ws.Rows[crRowInd++];
                                crRow.Font.Color = Color.Blue;
                                foreach (var item in dic)
                                {
                                    if (dtChiaThiCong.Columns.Contains(item.Key))
                                        MyFunction.SetValueToCellWorksheet(crRow[item.Value], drThiCong[item.Key]);
                                }
                                crRow[dic[GiaoViec.COL_TenCongViec]].Alignment.Indent = 4;
                                crRow[dic[GiaoViec.COL_TypeRow]].SetValueFromText(GiaoViec.TYPEROW_KHGV_THICONG);
                                crRow[dic[GiaoViec.COL_CodeCT]].SetValueFromText(drThiCong["Code"].ToString());
                                //crRow[dic[GiaoViec.COL_TenDuAn]].SetValueFromText(drCT["TenDuAn"].ToString());

                                //File đính kèm
                                ws.Hyperlinks.Add(crRow[dic[GiaoViec.COL_FileDinhKem]], $"{dic[GiaoViec.COL_FileDinhKem]}{crRow.Index + 1}", false, "Xem chi tiết");
                                ws.Hyperlinks.Add(crRow[dic[GiaoViec.COL_NguoiThucHien]], $"{dic[GiaoViec.COL_NguoiThucHien]}{crRow.Index + 1}", false, "Xem chi tiết");


                                if (ThiCongs.Last() == drThiCong)
                                {
                                    crRow[dic[GiaoViec.COL_KLKeHoach]].Formula = $"{dic[GiaoViec.COL_KLKeHoach]}{crRowCTCon.Index + 1} - SUM({dic[GiaoViec.COL_KLKeHoach]}{crRowCTCon.Index + 2}:{dic[GiaoViec.COL_KLKeHoach]}{crRowInd - 1})";
                                    crRow[dic[GiaoViec.COL_KLHopDong]].Formula = $"{dic[GiaoViec.COL_KLHopDong]}{crRowCTCon.Index + 1} - SUM({dic[GiaoViec.COL_KLHopDong]}{crRowCTCon.Index + 2}:{dic[GiaoViec.COL_KLHopDong]}{crRowInd - 1})";
                                }
                                else
                                {
                                    crRow[dic[GiaoViec.COL_KLKeHoach]].SetValueFromText(drThiCong["KhoiLuongKeHoach"].ToString());
                                }
                            }
                        }
                        //if (crCodeCVCha != "")
                        //{
                        //    //crRow = ws.Rows[crRowInd++];
                        //    //crRow[dic[GiaoViec.COL_RowCha]].Formula = $"ROW(A{crRowHM.Index + 1})";
                        //}
                        crCodeCVCha = "";
                    }
                    crRow = ws.Rows[crRowInd++];
                }
            }
            CapNhatRowChaCon();

            SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.EndUpdate();
            try
            {
////          wb.History.IsEnabled = true;

            }
            catch (Exception) { }
            wb.History.Clear();
        }

        

        public static void CapNhatCongThucChoRowCha(Row rowCha)
        {
            Worksheet ws = rowCha.Worksheet;
            CellRange range = ws.Range[MyConstant.Range_KeHoach];

            var dic = MyFunction.fcn_getDicOfColumn(range);
            string typeRow = rowCha[dic[GiaoViec.COL_TypeRow]].Value.ToString();

            if (string.IsNullOrEmpty(typeRow))
                return;


            int[] indsCon = ws.Columns[dic[GiaoViec.COL_RowCha]].Search((rowCha.Index + 1).ToString(), MyConstant.MySearchOptions).Select(x => x.RowIndex + 1).ToArray();

            if (!indsCon.Any())
                return;

////          ws.Workbook.History.IsEnabled = false;
            ws.Workbook.BeginUpdate();

            int minIndCon = indsCon.Min();
            int maxIndCon = indsCon.Max();

            string lsDateBD = $"{dic[TDKH.COL_NgayBatDau]}{minIndCon}:{dic[TDKH.COL_NgayBatDau]}{minIndCon}";
            string lsDateKT = $"{dic[TDKH.COL_NgayKetThuc]}{maxIndCon}:{dic[TDKH.COL_NgayKetThuc]}{maxIndCon}";

            rowCha[dic[TDKH.COL_NgayBatDau]].Formula = $"IF(MIN({lsDateBD})>0; MIN({lsDateBD}); \"\")";
            rowCha[dic[TDKH.COL_NgayKetThuc]].Formula = $"IF(MAX({lsDateKT})>0; MAX({lsDateKT}); \"\")";


            string headerColRowCha = dic[TDKH.COL_RowCha];
            string crHeader = dic[GiaoViec.COL_ThanhTienKeHoach];

            rowCha[dic[GiaoViec.COL_ThanhTienKeHoach]].Formula = $"SUMIF({headerColRowCha}{minIndCon}:{headerColRowCha}{maxIndCon};ROW();{crHeader}{minIndCon}:{crHeader}{maxIndCon})";

            if (typeRow == MyConstant.TYPEROW_CVCha)
            {

                crHeader = dic[GiaoViec.COL_KLHopDong];

                rowCha[dic[GiaoViec.COL_KLHopDong]].Formula = $"SUMIF({headerColRowCha}{minIndCon}:{headerColRowCha}{maxIndCon};ROW();{crHeader}{minIndCon}:{crHeader}{maxIndCon})";

                crHeader = dic[GiaoViec.COL_KLKeHoach];

                rowCha[dic[GiaoViec.COL_KLKeHoach]].Formula = $"SUMIF({headerColRowCha}{minIndCon}:{headerColRowCha}{maxIndCon};ROW();{crHeader}{minIndCon}:{crHeader}{maxIndCon})";
            }

            ws.Workbook.EndUpdate();
            try
            {
////              ws.Workbook.History.IsEnabled = true;
            }
            catch (Exception) { }
        }

        public static void CapNhatRowChaCon()
        {
            IWorkbook wb = SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.Document;
            Worksheet ws = SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.ActiveWorksheet;

            CellRange rangeData = wb.Range[MyConstant.Range_KeHoach];
            ws.Calculate();

            for (int i = rangeData.TopRowIndex; i <= rangeData.BottomRowIndex; i++)
            {
                Row crRow = ws.Rows[i];
                CapNhatCongThucChoRowCha(crRow);
            }
        }


        public static void GetMinMaxDateFromKhoiLuongHangNgay(out DateTime? minDate, out DateTime? maxDate)
        {
            minDate = maxDate = null;
            var crDVTH = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH;
            if (crDVTH is null)
            {
                return;
            }
            string dbString = $"SELECT {TDKH.TBL_KhoiLuongCongViecHangNgay}.Ngay FROM {GiaoViec.TBL_CONGVIECCHA} " +
                $"JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
                $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeCongTacTheoGiaiDoan IS NOT NULL " +
                $"AND {GiaoViec.TBL_CONGVIECCHA}.CodeCongTacTheoGiaiDoan = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
                $"WHERE {GiaoViec.TBL_CONGVIECCHA}.{crDVTH.ColCodeFK} = '{crDVTH.Code}' " +
                $"ORDER BY {TDKH.TBL_KhoiLuongCongViecHangNgay}.Ngay";
            DataTable dtFromKHTD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            if (dtFromKHTD.Rows.Count > 0)
            {
                minDate = DateTime.Parse(dtFromKHTD.AsEnumerable().First()["Ngay"].ToString());
                maxDate = DateTime.Parse(dtFromKHTD.AsEnumerable().Last()["Ngay"].ToString());
            }    

            dbString = $"SELECT {TDKH.TBL_KhoiLuongCongViecHangNgay}.Ngay FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
                $"JOIN {GiaoViec.TBL_CONGVIECCHA} " +
                $"ON {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongViecCha = {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha " +
                $"WHERE {GiaoViec.TBL_CONGVIECCHA}.{crDVTH.ColCodeFK} = '{crDVTH.Code}' " +
                $"ORDER BY {TDKH.TBL_KhoiLuongCongViecHangNgay}.Ngay";

            DataTable dtFromgv = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


            if (dtFromgv.Rows.Count > 0)
            {
                DateTime minDate1 = DateTime.Parse(dtFromgv.AsEnumerable().First()["Ngay"].ToString());
                DateTime maxDate1 = DateTime.Parse(dtFromgv.AsEnumerable().Last()["Ngay"].ToString());

                if (minDate.HasValue && maxDate.HasValue)
                {
                    minDate = minDate1 < minDate ? minDate1 : minDate;
                    maxDate = maxDate1 > maxDate ? maxDate1 : maxDate;
                }
                else
                {
                    minDate = minDate1;
                    maxDate = maxDate1;
                }
            }
        }

        /// <summary>
        /// Cập nhật bảng báo cáo công việc hàng ngày
        /// </summary>
        /// <param name="isRangeDate">True: Load từ ngày đến ngày, FALSE: Load Chỉ 1 ngày</param>
        /// <param name="dateBDorSelectedString"></param>
        /// <param name="dateKTString"></param>
        public static List<KLTTHangNgay> LoadCongTacHangNgay(TypeLoadKLHangNgay typeKLHN, DateTime? dateBDorSelected = null, DateTime? dateKT = null, bool isShowDateDetail = true, bool isGetDangThucHien = false, string codeDuAn = null)
        {
            //List<KLTTHangNgay> lsHangNgay = new List<KLTTHangNgay>();

            //if (!isRangeDate)
            //{
            string dateBDorSelectedString = dateBDorSelected.HasValue ? dateBDorSelected.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null;
            string dateKTString = dateKT.HasValue ? dateKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null;
            DateTime? dateBDKLHN = null, dateKTKLHN = null;
            List<KLTTHangNgay> lsHangNgay = new List<KLTTHangNgay>()
            {
                new KLTTHangNgay { Code = GiaoViec.NhomGiaoViec_DuAn, TenCongTac = GiaoViec.NhomGiaoViec_DuAn },
                new KLTTHangNgay { Code = GiaoViec.NhomGiaoViec_CongTacThuCong, TenCongTac = GiaoViec.NhomGiaoViec_CongTacThuCong },
                new KLTTHangNgay { Code = GiaoViec.NhomGiaoViec_CongTacTuKeHoach, TenCongTac = GiaoViec.NhomGiaoViec_CongTacTuKeHoach },
            };
            //}

            List<string> /*dkJoin = new List<string>(),*/ dkWhere = new List<string>();
            switch (typeKLHN)
            {
                case TypeLoadKLHangNgay.Only1Day:
                    //dkJoin.Add($"{{0}}.Ngay = '{dateBDorSelected}'");

                    if (!SharedControls.ce_CongTacNgoaiKeHoach.Checked)
                    {
                        dkWhere.Add($"{GiaoViec.TBL_CONGVIECCHA}.NgayBatDau <= '{dateBDorSelectedString}' AND {GiaoViec.TBL_CONGVIECCHA}.NgayKetThuc >= '{dateBDorSelectedString}'");
                    }
                    dateBDKLHN = dateKTKLHN = dateBDorSelected;
                    break;
                case TypeLoadKLHangNgay.RangeDate:
                    //dkWhere.Add((string.IsNullOrEmpty(dateBDorSelectedString) ? "" : $" {{0}}.Ngay >= '{dateBDorSelectedString}' ") +
                    //            (string.IsNullOrEmpty(dateKTString) ? "" : $"AND {{0}}.Ngay <= '{dateKTString}'"));
                    dateBDKLHN = dateBDorSelected;
                    dateKTKLHN = dateKT??DateTime.Now.Date;
                    break;
                case TypeLoadKLHangNgay.BetweenNgayBatDauKetThuc:
                    dkWhere.Add($"(({GiaoViec.TBL_CONGVIECCHA}.CodeHangMuc IS NOT NULL AND {GiaoViec.TBL_CONGVIECCHA}.NgayBatDau <= '{dateBDorSelectedString}' AND {GiaoViec.TBL_CONGVIECCHA}.NgayKetThuc >= '{dateBDorSelectedString}')\r\n" +
                        $"OR ({GiaoViec.TBL_CONGVIECCHA}.CodeHangMuc IS NULL AND {GiaoViec.TBL_CONGVIECCHA}.NgayBatDauThiCong <= '{dateBDorSelectedString}' AND {GiaoViec.TBL_CONGVIECCHA}.NgayKetThucThiCong >= '{dateBDorSelectedString}'))");
                    //dkWhere.Add($"{{0}}.NgayBatDau <= '{dateBDorSelected}' AND {{0}}.NgayKetThuc >= '{dateBDorSelected}'");
                    dateBDKLHN = null;
                    dateKTKLHN = DateTime.Now.Date;
                    break;
                case TypeLoadKLHangNgay.NgayBatDauBetween:
                    dkWhere.Add($"{GiaoViec.TBL_CONGVIECCHA}.NgayBatDau >= '{dateBDorSelectedString}' AND {GiaoViec.TBL_CONGVIECCHA}.NgayBatDau <= '{dateKTString}' ");
                    dateBDKLHN = null;
                    dateKTKLHN = DateTime.Now.Date;
                    break;
                case TypeLoadKLHangNgay.DenHienTai:
                    dkWhere.Add($"{GiaoViec.TBL_CONGVIECCHA}.NgayBatDau <= '{DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'");
                    dateBDKLHN = null;
                    dateKTKLHN = DateTime.Now.Date;
                    //dkJoin.Add($"{{0}}.Ngay <= '{DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'");
                    break;
                case TypeLoadKLHangNgay.All:
                    break;
                default:
                    return lsHangNgay;
            }

            var crDVTH = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH as DonViThucHien;
            if (crDVTH is null)
                return lsHangNgay;


            var whereCond = (dkWhere.Any()) ? string.Format(string.Join(" AND ", dkWhere), TDKH.TBL_KhoiLuongCongViecHangNgay) : "";

            if (isGetDangThucHien)
            {

                whereCond += ((whereCond.HasValue()) ? " OR " : "  ") + $"{GiaoViec.TBL_CONGVIECCHA}.TrangThai = 'Đang thực hiện'";
            }
            if (codeDuAn.HasValue())
            {
                //dkWhere.Add($"{MyConstant.TBL_THONGTINDUAN}.Code = '{codeDuAn}'");
                whereCond = ((whereCond.HasValue()) ? $"({whereCond}) AND " : "  ") + $"{MyConstant.TBL_THONGTINDUAN}.Code = '{codeDuAn}'";

            }

            if (whereCond.HasValue())
            {
                whereCond = " WHERE " + whereCond;
            }

            //List các công tác cha
            string dbString = $"SELECT {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha AS Code, {GiaoViec.TBL_CONGVIECCHA}.NoiDungThucHien, {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha AS CodeCha, COUNT({GiaoViec.TBL_FileDinhKem}.Code) AS FileCount, " +
                $"\"MaDinhMuc\" AS MaHieuCongTac, \"TenCongViec\" AS TenCongTac, " +
                $"{GiaoViec.TBL_CONGVIECCHA}.DonVi, {GiaoViec.TBL_CONGVIECCHA}.KhoiLuongKeHoach, {GiaoViec.TBL_CONGVIECCHA}.CodeCongTacTheoGiaiDoan, " +
                $"{GiaoViec.TBL_CONGVIECCHA}.NgayBatDau, {GiaoViec.TBL_CONGVIECCHA}.NgayKetThuc, {GiaoViec.TBL_CONGVIECCHA}.KhoiLuongHopDong, {GiaoViec.TBL_CONGVIECCHA}.KhoiLuongKeHoach, " +
                //$"{GiaoViec.TBL_BaoCaoCongViecHangNgay}.Code AS CodeHangNgay, {GiaoViec.TBL_BaoCaoCongViecHangNgay}.KhoiLuongThiCong, {GiaoViec.TBL_BaoCaoCongViecHangNgay}.GhiChu, " +
                //$"{GiaoViec.TBL_BaoCaoCongViecHangNgay}.IsSumThiCong, {GiaoViec.TBL_BaoCaoCongViecHangNgay}.Ngay, " +
                $"{MyConstant.view_DonViThucHien}.Ten AS TenDonViThucHien, {MyConstant.TBL_THONGTINDUAN}.TenDuAn, {GiaoViec.TBL_CONGVIECCHA}.CodeHangMuc,  {GiaoViec.TBL_CONGVIECCHA}.CodeDauMuc, {GiaoViec.TBL_CONGVIECCHA}.TrangThai " +
                $"FROM {GiaoViec.TBL_CONGVIECCHA} " +
                $"LEFT JOIN {GiaoViec.TBL_FileDinhKem} " +
                $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha = {GiaoViec.TBL_FileDinhKem}.CodeParent " +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeCongTacTheoGiaiDoan = {TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                $"LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code " +
                $"LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code " +
                $"LEFT JOIN {GiaoViec.TBL_DauViecNho} " +
                $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeDauMuc = {GiaoViec.TBL_DauViecNho}.Code " +
                $"LEFT JOIN {GiaoViec.TBL_DauViecLon} " +
                $"ON {GiaoViec.TBL_DauViecNho}.CodeDauViecLon = {GiaoViec.TBL_DauViecLon}.Code " +
                $"JOIN {MyConstant.TBL_THONGTINDUAN} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code OR {GiaoViec.TBL_DauViecLon}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code " +
                $"LEFT JOIN {MyConstant.view_DonViThucHien} " +
                $"ON ((COALESCE({GiaoViec.TBL_CONGVIECCHA}.CodeNhaThau, {GiaoViec.TBL_CONGVIECCHA}.CodeNhaThauPhu, {GiaoViec.TBL_CONGVIECCHA}.CodeToDoi)) = {MyConstant.view_DonViThucHien}.Code) " +
                //$"OR (COALESCE({GiaoViec.TBL_CONGVIECCHA}.CodeNhaThau, {GiaoViec.TBL_CONGVIECCHA}.CodeNhaThauPhu, {GiaoViec.TBL_CONGVIECCHA}.CodeToDoi)) IS NULL " +
                //$"LEFT JOIN {GiaoViec.TBL_BaoCaoCongViecHangNgay} " +
                //$"ON {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha = {GiaoViec.TBL_BaoCaoCongViecHangNgay}.CodeCongViecCha " +

                //((dkJoin.Any()) ? "AND " + string.Format(string.Join(" AND ", dkJoin), GiaoViec.TBL_BaoCaoCongViecHangNgay):"") +

                //$" WHERE " +

                whereCond + "\r\n" +
                $" GROUP BY {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha " +
                $" ORDER BY {GiaoViec.TBL_CONGVIECCHA}.SortId ASC, " +
                $"{MyConstant.TBL_THONGTINDUAN}.CreatedOn DESC";

            var lsCha = DataProvider.InstanceTHDA.ExecuteQueryModel<KLTTHangNgay>(dbString);

            lsCha.Where(x => x.CodeHangMuc != null && x.CodeCongTacTheoGiaiDoan is null).ForEach(x => x.ParentCode = GiaoViec.NhomGiaoViec_CongTacThuCong);
            lsCha.Where(x => x.CodeCongTacTheoGiaiDoan != null).ForEach(x => x.ParentCode = GiaoViec.NhomGiaoViec_CongTacTuKeHoach);
            lsCha.Where(x => x.CodeDauMuc != null).ForEach(x => x.ParentCode = GiaoViec.NhomGiaoViec_DuAn);
            lsHangNgay.AddRange(lsCha);

            var codeCongTacs = lsCha.Select(x => x.Code.ToString()).Distinct();
            var codeCongTacTheoGiaiDoan = lsCha.Where(x => x.CodeCongTacTheoGiaiDoan != null).ToDictionary(x => x.CodeCongTacTheoGiaiDoan, x => x.Code);


            List<KLTTHangNgay> KLHangNgay;// = MyFunction.Fcn_CalKLKHNew(Enums.TypeKLHN.GiaoViecCha, codeCongTacs, dateBDorSelected, dateKT).fcn_DataTable2List<KLTTHangNgay>();
            List<KLTTHangNgay> KLTDKHs;

            //if (typeKLHN == TypeLoadKLHangNgay.RangeDate)
            //{
            KLHangNgay = MyFunction.Fcn_CalKLKHModel(Enums.TypeKLHN.GiaoViecCha, codeCongTacs, dateBDKLHN, dateKTKLHN);

            if (KLHangNgay is null)
                return lsHangNgay;


            KLHangNgay.ForEach(x => x.ParentCode = x.CodeCha);

            foreach (var gr in lsHangNgay)
            {
                if (gr.CodeCha is null)
                    continue;

                var allKLTC = KLHangNgay.Where(x => x.CodeCha == gr.CodeCha && x.KhoiLuongThiCong.HasValue);
                var allKLKH = KLHangNgay.Where(x => x.CodeCha == gr.CodeCha && x.KhoiLuongKeHoach.HasValue);

                //if (allKLTC.Any())
                    gr.KhoiLuongThiCong = allKLTC.Sum(x => x.KhoiLuongThiCong.Value);
                
                //if (allKLKH.Any())
                    gr.KhoiLuongKeHoach = allKLKH.Sum(x => x.KhoiLuongKeHoach.Value);
                
                gr.ThanhTienThiCongCustom = KLHangNgay.Where(x => x.CodeCha == gr.CodeCha && x.ThanhTienThiCong.HasValue).Sum(x => x.ThanhTienThiCong.Value);
                gr.ThanhTienKeHoachCustom = KLHangNgay.Where(x => x.CodeCha == gr.CodeCha && x.ThanhTienKeHoach.HasValue).Sum(x => x.ThanhTienKeHoach.Value);

            }


            if (typeKLHN != TypeLoadKLHangNgay.Only1Day)
            {

                if (isShowDateDetail)
                {
                    KLHangNgay.ForEach(x => x.TenCongTac = x.NgayString);
                    lsHangNgay.AddRange(KLHangNgay);
                }
                lsCha.Where(x => !x.KhoiLuongThiCong.HasValue).ForEach(x => x.KhoiLuongThiCong = 0);
                
            }
            else //ONLY 1 DATE
            {
                //List các công tác con

                
                foreach (var gr in lsCha)
                {
                    //gr.CodeHangNgay = KLHangNgay.FirstOrDefault(x => x.CodeCongTac == gr.CodeCongViecCha)?.CodeHangNgay;
                    //gr.CodeHangNgayTDKH = KLHangNgay.FirstOrDefault(x => x.CodeCongTac == gr.CodeCongViecCha)?.CodeHangNgayTDKH;
                    var hn = KLHangNgay.FirstOrDefault(x => x.CodeCha == gr.CodeCha);
                    gr.GhiChu = hn?.GhiChu;
                    gr.FileCount = hn?.FileCount ?? 0;
                    gr.LyTrinhCaoDo = hn?.LyTrinhCaoDo;
                    //gr.KhoiLuongKeHoach = hn?.KhoiLuongKeHoach;
                    gr.DonGiaKeHoach = hn?.DonGiaKeHoach;
                    gr.DonGiaThiCong = hn?.DonGiaThiCong;
                    gr.CodeHangNgay = hn?.Code ?? Guid.NewGuid().ToString();
                    gr.Ngay = dateBDorSelected;
                }



                string[] lsCodeCVCha = lsHangNgay.Select(x => x.CodeCha).ToArray();
                dbString = $"SELECT {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCon,  {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCha as ParentCode,  COUNT({GiaoViec.TBL_CongViecCon_FileDinhKem}.Code) AS FileCount," +
                    $"\"MaDinhMuc\" AS MaHieuCongTac, \"TenCongViec\" AS TenCongTac, " +
                        $"{GiaoViec.TBL_CONGVIECCON}.CodeCongViecCon AS Code, \"DonVi\", \"TrangThai\", {GiaoViec.TBL_CONGVIECCON}.KhoiLuongKeHoach,{GiaoViec.TBL_CONGVIECCON}.DonGia AS DonGiaKeHoach, " +
                        $"\"NgayBatDau\", \"NgayKetThuc\" " +
                        //$"{GiaoViec.TBL_BaoCaoCongViecHangNgay}.Code AS CodeHangNgay, {GiaoViec.TBL_BaoCaoCongViecHangNgay}.KhoiLuongThiCong,{GiaoViec.TBL_BaoCaoCongViecHangNgay}.GhiChu, " +
                        //$"{GiaoViec.TBL_BaoCaoCongViecHangNgay}.IsSumThiCong " +
                        $"FROM {GiaoViec.TBL_CONGVIECCON} " +

                         $"LEFT JOIN {GiaoViec.TBL_CongViecCon_FileDinhKem} " +
                        $"ON {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCon = {GiaoViec.TBL_CongViecCon_FileDinhKem}.CodeParent " +

                        $"WHERE {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCha IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeCVCha)}) " +
                        $"GROUP BY {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCon ";

                //$" ORDER BY Ngay ASC";


                var lsCons = DataProvider.InstanceTHDA.ExecuteQueryModel<KLTTHangNgay>(dbString);

                foreach (var row in lsCons)
                {
                    var rowCha = lsHangNgay.Single(x => x.CodeCha == row.ParentCode);
                    row.TenDuAn = rowCha.TenDuAn;
                    row.TenDonViThucHien = rowCha.TenDonViThucHien;
                    
                }

                var codeCons = lsCons.Select(x => x.Code).ToArray();
                KLHangNgay = MyFunction.Fcn_CalKLKHModel(Enums.TypeKLHN.GiaoViecCon, codeCons, dateBDKLHN, dateKTKLHN);

                foreach (var gr in lsCons)
                {
                    if (gr.Code is null)
                        continue;
                    var hn = KLHangNgay.SingleOrDefault(x => x.CodeCha == gr.Code && x.Ngay == dateBDKLHN);
                   
                    gr.Ngay = dateBDKLHN;
                    gr.CodeHangNgay = hn?.Code ?? Guid.NewGuid().ToString();

                    gr.CodeCha = gr.Code;
                    //gr.DonGiaKeHoach = hn.DonGiaKeHoach;
                    gr.DonGiaThiCong = hn?.DonGiaThiCong ?? gr.DonGiaThiCong;
                    gr.KhoiLuongThiCong = KLHangNgay.Where(x => x.CodeCha == gr.Code && x.KhoiLuongThiCong.HasValue).Sum(x => x.KhoiLuongThiCong.Value);
                    gr.KhoiLuongKeHoach = KLHangNgay.Where(x => x.CodeCha == gr.Code && x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach.Value);
                }

                lsHangNgay.AddRange(lsCons);
                
                //string[] lsCodeCVCha = lsHangNgay.Select(x => x.Code).ToArray();
                //dbString = $"SELECT {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCha as ParentCode,\"MaDinhMuc\" AS MaHieuCongTac, \"TenCongViec\" AS TenCongTac, " +
                //        $"{GiaoViec.TBL_CONGVIECCON}.CodeCongViecCon AS Code, \"DonVi\", \"TrangThai\", " +
                //        $"\"NgayBatDau\", \"NgayKetThuc\", " +
                //        $"{GiaoViec.TBL_BaoCaoCongViecHangNgay}.Code AS CodeHangNgay, {GiaoViec.TBL_BaoCaoCongViecHangNgay}.KhoiLuongThiCong,{GiaoViec.TBL_BaoCaoCongViecHangNgay}.GhiChu, " +
                //        $"{GiaoViec.TBL_BaoCaoCongViecHangNgay}.IsSumThiCong " +
                //        $"FROM {GiaoViec.TBL_CONGVIECCON} " +

                //        $"LEFT JOIN {GiaoViec.TBL_BaoCaoCongViecHangNgay} " +
                //        $"ON {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCon = {GiaoViec.TBL_BaoCaoCongViecHangNgay}.CodeCongViecCon " +

                //        ((dkJoin.Any()) ? " AND "+string.Format(string.Join(" AND ", dkJoin), GiaoViec.TBL_BaoCaoCongViecHangNgay):"") +

                //        $"WHERE {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCha IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeCVCha)}) " +

                //        $" ORDER BY Ngay ASC";


                //var lsCons = DataProvider.InstanceTHDA.ExecuteQueryModel<KLTTHangNgay>(dbString);

                //foreach (var row in lsCons)
                //{
                //    var rowCha = lsHangNgay.Single(x => x.Code  == row.ParentCode);
                //    row.TenDuAn = rowCha.TenDuAn;
                //    row.TenDonViThucHien = rowCha.TenDonViThucHien;
                //}    
                //lsHangNgay.AddRange(lsCons);
            }
            return lsHangNgay;
        }

        public static void SetSortIdCongTacCha()
        {
            //string dbString = $"SELECT * FROM {GiaoViec.TBL_CONGVIECCHA} WHERE CodeHangMuc IS NULL";
            //DataTable dt = DataProvider.in
        }

        //public static DataTable GetCongTacWithTheSameHMAndContractorWith(string codeCongViecCha)
        //{
        //    //string dbString = $"SELECT cvcRef.* " +
        //    //    $"FROM {GiaoViec.TBL_CONGVIECCHA} cvcOri " +
        //    //    $"JOIN {TDKH.TBL_DanhMucCongTac} dmctOri " +
        //    //    $"ON cvcOri.CodeCongTac = dmctOri.Code " +
        //    //    $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkRef " +
        //    //    $"ON COALESCE(cvcOri.CodeNhaThau, cvcOri.CodeNhaThauPhu, cvcOri.CodeToDoi) = COALESCE(cttkRef.CodeNhaThau, cttkRef.CodeNhaThauPhu, cttkRef.CodeToDoi) " +
        //    //    $"JOIN {TDKH.TBL_DanhMucCongTac} dmctRef " +
        //    //    $"ON cttkRef.CodeCongTac = dmctRef.Code " +
        //    //    $"AND dmctOri.CodeHangMuc = dmctRef.CodeHangMuc " +
        //    //    $"WHERE cvcOri.Code = '{codeCongViecCha}' ";
        //    //return DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        //}
    }
}
