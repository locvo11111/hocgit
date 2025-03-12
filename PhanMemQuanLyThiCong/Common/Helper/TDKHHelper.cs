using DevExpress.Spreadsheet;
using DevExpress.Utils.Filtering;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Constant.Enum;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using PhanMemQuanLyThiCong.Model.TDKH;
using PhanMemQuanLyThiCong.Model.PermissionControl;
using DevExpress.Data;
using PhanMemQuanLyThiCong.Model;
using Dapper;
using System.Security.Policy;
using MoreLinq;
using DevExpress.Internal.WinApi;
using DevExpress.Mvvm.POCO;
using PhanMemQuanLyThiCong.Controls.KiemSoat;
using DevExpress.XtraSpreadsheet.Commands.Internal;
using StackExchange.Profiling.Internal;
using DevExpress.CodeParser;
using DevExpress.XtraReports.Templates;
using DevExpress.PivotGrid.SliceQueryDataSource;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Pkcs;
using PM360.Common.Helper;
using ZXing.OneD;
using DevExpress.XtraSpreadsheet.Utils.Trees;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using DevExpress.DevAV.Chat.Model;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Model.MayThiCong;
using PhanMemQuanLyThiCong.Controls;
using Microsoft.Owin.Security;
using DevExpress.XtraSpreadsheet.Mouse;
using Newtonsoft.Json;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using DevExpress.XtraRichEdit.Layout.Engine;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class TDKHHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="setSortId">Cập nhật thứ tự sortId vào cơ sở dữ liệu</param>
        /// <param name="setCustomOrder"></param>
        public static void LoadCongTacDoBoc(bool setSortId = false, bool setCustomOrder = false)
        {

            var wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            var ws = wb.Worksheets[TDKH.SheetName_DoBocChuan];


            //wb.History.Clear();
            //wb.History.IsEnabled = false;
            try
            {
                
                var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
                if (crDVTH is null)
                {
                    return;
                }

                WaitFormHelper.ShowWaitForm("Đang tải Đo bóc công tác");
                TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);

                DataTable CongTacs = TDKHHelper.GetCongTacsDataTable(SharedControls.slke_ThongTinDuAn.EditValue?.ToString(), codeCT, codeHM, SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString(), $"cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}'");


                string strCodeCTTheoKy = MyFunction.fcn_Array2listQueryCondition(CongTacs.AsEnumerable().Select(x => x["Code"].ToString()));
                string strCodeNhom = MyFunction.fcn_Array2listQueryCondition(CongTacs.AsEnumerable().Where(x => x["CodeNhom"] != DBNull.Value)
                    .Select(x => x["CodeNhom"].ToString()).Distinct());

                string dbString = $"SELECT con.*, nhom.Ten AS TenNhom\r\n" +
                    $"FROM {TDKH.TBL_ChiTietCongTacCon} con\r\n" +
                    $"LEFT JOIN {TDKH.TBL_NhomDienGiai} nhom\r\n" +
                    $"ON con.CodeNhom = nhom.Code\r\n" +
                    $"WHERE \"CodeCongTacCha\" IN ({strCodeCTTheoKy}) ORDER BY \"Row\" ASC";

                DataTable ctCons = DataProvider.InstanceTHDA
                    .ExecuteQuery(dbString);

                dbString = $"SELECT COALESCE(con.DonVi, dmct.DonVi) AS DonVi,COALESCE(con.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
                        $"COALESCE(con.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                        $"COALESCE(con.KyHieuBanVe, dmct.KyHieuBanVe) AS KyHieuBanVe,con.*,Mui.Code as CodeMuiThiCong,Mui.Ten as MuiThiCong " +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} con\r\n" +
                    $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct ON dmct.Code=con.CodeCongTac \r\n" +
                    $"LEFT JOIN {TDKH.Tbl_TDKH_MuiThiCong} Mui\r\n" +
                    $"ON con.CodeMuiThiCong = Mui.Code\r\n" +
                    $"WHERE \"CodeCha\" IN ({strCodeCTTheoKy}) ORDER BY \"Row\" ASC";

                DataTable ctConChia = DataProvider.InstanceTHDA
                    .ExecuteQuery(dbString);

                dbString = $"SELECT * FROM {TDKH.TBL_NhomCongTac} WHERE Code IN ({strCodeNhom})";
                DataTable dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                DataTable dtData = DataTableCreateHelper.DoBocChuanTDKH();

                var grsCTrinh = CongTacs.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);

                int STTCTrinh = 0;

                //FileHelper.fcn_spSheetStreamDocument(SharedControls.spsheet_TD_KH_LapKeHoach, $@"{BaseFrom.m_templatePath}\FileExcel\6.aBangNhapKeHachDanhGiaLoiNhuan.xlsx");

                var definedName = wb.DefinedNames.GetDefinedName(TDKH.RANGE_DoBocChuan);
                var dic = MyFunction.fcn_getDicOfColumn(definedName.Range);
                BaseFrom.IndsCheckBoxDoBocChuan = new List<int>()
                {
                    0,
                    ws.Range.GetColumnIndexByName(dic[TDKH.COL_PhanTichVatTu]),
                    ws.Range.GetColumnIndexByName(dic[TDKH.COL_IsUseMTC]),
                    ws.Range.GetColumnIndexByName(dic[TDKH.COL_HasHopDongAB]),
                };

                BaseFrom.IndFstCellDataDoBocChuan = definedName.Range.TopRowIndex;
                ws.Columns[dic[TDKH.COL_PhanTichVatTu]].Visible
                    = ws.Columns[dic[TDKH.COL_HasHopDongAB]].Visible
                    = ws.Columns[dic[TDKH.COL_TenGop]].Visible = crDVTH.IsGiaoThau;

                if (definedName.Range.RowCount > 2)
                {
                    ws.Rows.Remove(definedName.Range.TopRowIndex + 1, definedName.Range.RowCount - 2);
                }

                //Ẩn kế hoạch
                if (!BaseFrom.allPermission.HaveInitProjectPermission)
                {
                    ws.Columns[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Visible = false;
                    ws.Columns[dic[TDKH.COL_KhoiLuongHopDongDuAn]].Visible = false;
                    ws.Columns[dic[TDKH.COL_KhoiLuongHopDongChiTiet]].Visible = false;
                    //ws.Columns[dic[TDKH.COL_DonGia]].Visible = false;
                }

                var crRowInd = definedName.Range.TopRowIndex;
                int rowTongInd = crRowInd;
                var fstInd = definedName.Range.TopRowIndex;
                //var range = definedName.Range;
                //if (definedName.Range.RowCount > 2)
                //{
                //    ws.Rows.Remove(definedName.Range.TopRowIndex, definedName.Range.RowCount - 2);
                //}
                string prefixFormula = MyConstant.PrefixFormula;
                //           //wb.History.IsEnabled = false;
                wb.BeginUpdate();
                foreach (var grCTrinh in grsCTrinh)
                {
                    DataRow newRowCtrinh = dtData.NewRow();
                    dtData.Rows.Add(newRowCtrinh);

                    var crRowCtrInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;

                    DataRow fstCtr = grCTrinh.First();
                    newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                    newRowCtrinh[TDKH.COL_Code] = grCTrinh.Key;
                    newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                    newRowCtrinh[TDKH.COL_DanhMucCongTac] = fstCtr["TenCongTrinh"];
                    newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                    newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{rowTongInd + 1})";

                    var grsHM = grCTrinh.GroupBy(x => x["CodeHangMuc"]);
                    int STTHM = 0;

                    int sorid = 0;
                    int STTCtChia = 1;
                    foreach (var grHM in grsHM)
                    {
                        //int sttWholeHM = 0;
                        DataRow newRowHM = dtData.NewRow();
                        dtData.Rows.Add(newRowHM);

                        var crRowHMInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;


                        DataRow fstHM = grHM.First();
                        newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                        newRowHM[TDKH.COL_Code] = grHM.Key;
                        newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                        newRowHM[TDKH.COL_DanhMucCongTac] = fstHM["TenHangMuc"];
                        newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                        newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd + 1})";

                        var grsPhanTuyen = grHM.GroupBy(x => (int)x["IndPT"])
                            .OrderBy(x => x.Key);

                        foreach (var grPhanTuyen in grsPhanTuyen)
                        {
                            DataRow fstPT = grPhanTuyen.First();

                            string codePT = fstPT["CodePhanTuyen"].ToString();
                            bool isPhanTuyen = codePT.HasValue();
                            int? crRowPTInd = null;
                            if (isPhanTuyen)
                            {
                                DataRow newRowPT = dtData.NewRow();
                                dtData.Rows.Add(newRowPT);

                                crRowPTInd = crRowInd = fstInd + dtData.Rows.Count;
                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                newRowPT[TDKH.COL_Code] = codePT;
                                newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                                newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
                                newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                                newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";
                            }


                            var grsNhom = grPhanTuyen.GroupBy(x => (int)x["IndNhom"])
                                                .OrderBy(x => x.Key);

                            foreach (var grNhom in grsNhom)
                            {
                                var fstNhom = grNhom.First();
                                string codeNhom = fstNhom["CodeNhom"].ToString();
                                bool isNhom = codeNhom.HasValue();
                                //bool isNhomHasValue = false;
                                int? crRowNhomInd = null;

                                if (isNhom)
                                {
                                    var drNhom = dtNhom.AsEnumerable().Single(x => x["Code"].ToString() == codeNhom);
                                    DataRow newRowNhom = dtData.NewRow();
                                    dtData.Rows.Add(newRowNhom);
                                    crRowNhomInd = crRowInd = fstInd + dtData.Rows.Count;

                                    //if (fstNhom["KhoiLuongKeHoach"] != DBNull.Value)
                                    //{
                                    //    isNhomHasValue = true;
                                    //}
                                    newRowNhom[TDKH.COL_TenGop] = fstNhom["TenGopNhom"];
                                    newRowNhom[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_NHOM;
                                    newRowNhom[TDKH.COL_DanhMucCongTac] = drNhom["Ten"];
                                    newRowNhom[TDKH.COL_DonVi] = drNhom["DonVi"];
                                    newRowNhom[TDKH.COL_DBC_LuyKeDaThucHien] = $"{prefixFormula}{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}";
                                    newRowNhom[TDKH.COL_DBC_KhoiLuongToanBo] = drNhom["KhoiLuongKeHoach"];
                                    newRowNhom[TDKH.COL_KhoiLuongHopDongChiTiet] = drNhom["KhoiLuongHopDongChiTiet"];
                                    newRowNhom[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd) + 1})";
                                    newRowNhom[TDKH.COL_Code] = drNhom["Code"];
                                    newRowNhom[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                                    if (drNhom["GhiChuBoSungJson"] != DBNull.Value)
                                    {
                                        var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(drNhom["GhiChuBoSungJson"].ToString());
                                        newRowNhom[TDKH.COL_STTDocVao] = GhiChuBoSungJson.STT;
                                        newRowNhom[TDKH.COL_STTND] = GhiChuBoSungJson.STTND;
                                    }
                                }

                                int STTCTac = 0;
                                var ctacs = grNhom.ToArray();
                                if (setCustomOrder)
                                    ctacs = grNhom.OrderBy(x => x[TDKH.COL_CustomOrder]).ToArray();
                                if (!isNhom || MSETTING.Default.CongTacInNhomVisible)
                                {
                                    foreach (var ctac in ctacs)
                                    {
                                        sorid++;

                                        DataRow newRowCTac = dtData.NewRow();
                                        dtData.Rows.Add(newRowCTac);


                                        int crRowCtInd = crRowInd = fstInd + dtData.Rows.Count;
                                        if (ctac["Code"] == DBNull.Value)
                                            continue;

                                        //if (BaseFrom.allPermission.HaveInitProjectPermission && sorid.ToString() != ctac["SortId"].ToString())
                                        //{
                                        //    ctac["SortId"] = sorid;
                                        //}

                                        if (setSortId)
                                            ctac["SortId"] = sorid;
                                        if (ctac["GhiChuBoSungJson"] != DBNull.Value)
                                        {
                                            var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(ctac["GhiChuBoSungJson"].ToString());
                                            newRowCTac[TDKH.COL_STTDocVao] = GhiChuBoSungJson.STT;
                                            newRowCTac[TDKH.COL_STTND] = GhiChuBoSungJson.STTND;
                                        }
                                        newRowCTac[TDKH.COL_STT] = ++STTCTac;
                                        newRowCTac[TDKH.COL_CustomOrderWholeHM] = sorid;
                                        newRowCTac[TDKH.COL_CustomOrder] = ctac["CustomOrder"];
                                        newRowCTac[TDKH.COL_MaHieuCongTac] = ctac["MaHieuCongTac"];
                                        newRowCTac[TDKH.COL_TenGop] = ctac["TenGopCongTac"];

                                        newRowCTac[TDKH.COL_DanhMucCongTac] = ctac["TenCongTac"];
                                        newRowCTac[TDKH.COL_DonVi] = ctac["DonVi"];
                                        newRowCTac[TDKH.COL_DBC_KhoiLuongToanBo] = ctac["KhoiLuongToanBo"];
                                        newRowCTac[TDKH.COL_DBC_LuyKeDaThucHien] = $"{prefixFormula}{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}";
                                        newRowCTac[TDKH.COL_PhanTichVatTu] = ctac["PhanTichVatTu"];
                                        newRowCTac[TDKH.COL_HasHopDongAB] = ctac["HasHopDongAB"];
                                        newRowCTac[TDKH.COL_IsUseMTC] = ctac[TDKH.COL_IsUseMTC];
                                        newRowCTac[TDKH.COL_KhoiLuongHopDongChiTiet] = ctac["KhoiLuongHopDongChiTiet"];
                                        newRowCTac[TDKH.COL_KhoiLuongHopDongDuAn] = ctac["KhoiLuongHopDongDuAn"];
                                        newRowCTac[TDKH.COL_GhiChu] = ctac["GhiChu"];
                                        newRowCTac[TDKH.COL_MuiThiCong] = ctac["MuiThiCong"];
                                        newRowCTac[TDKH.COL_CodeMuiThiCong] = ctac["CodeMuiThiCong"];
                                        newRowCTac[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowNhomInd ?? crRowPTInd ?? crRowHMInd) + 1})";

                                        newRowCTac[TDKH.COL_Code] = ctac["Code"];
                                        newRowCTac[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                        DataRow[] drs_ChiaCT = ctConChia.Select($"[CodeCha] = '{ctac["Code"]}'");
                                        if (drs_ChiaCT.Any())
                                        {
                                            STTCtChia = 1;
                                            foreach (var RowChia in drs_ChiaCT)
                                            {
                                                int? crRowCTChia = crRowInd = fstInd + dtData.Rows.Count;
                                                DataRow newRowCTChia = dtData.NewRow();
                                                dtData.Rows.Add(newRowCTChia);

                                                newRowCTChia[TDKH.COL_STT] = $"{STTCTac}.{STTCtChia++}";
                                                newRowCTChia[TDKH.COL_MaHieuCongTac] = RowChia["MaHieuCongTac"];
                                                newRowCTChia[TDKH.COL_DanhMucCongTac] = RowChia["TenCongTac"];
                                                newRowCTChia[TDKH.COL_DonVi] = RowChia["DonVi"];
                                                newRowCTChia[TDKH.COL_DBC_KhoiLuongToanBo] = RowChia["KhoiLuongToanBo"];
                                                newRowCTChia[TDKH.COL_MuiThiCong] = RowChia["MuiThiCong"];
                                                newRowCTChia[TDKH.COL_CodeMuiThiCong] = RowChia["CodeMuiThiCong"];
                                                newRowCTChia[TDKH.COL_DBC_LuyKeDaThucHien] = $"{prefixFormula}{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}";
                                                newRowCTChia[TDKH.COL_PhanTichVatTu] = ctac["PhanTichVatTu"];
                                                newRowCTChia[TDKH.COL_HasHopDongAB] = ctac["HasHopDongAB"];
                                                newRowCTChia[TDKH.COL_IsUseMTC] = RowChia[TDKH.COL_IsUseMTC];
                                                newRowCTChia[TDKH.COL_KhoiLuongHopDongChiTiet] = RowChia["KhoiLuongHopDongChiTiet"];
                                                newRowCTChia[TDKH.COL_KhoiLuongHopDongDuAn] = ctac["KhoiLuongHopDongDuAn"];
                                                newRowCTChia[TDKH.COL_GhiChu] = RowChia["GhiChu"];
                                                newRowCTChia[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowCtInd) + 1})";

                                                newRowCTChia[TDKH.COL_Code] = RowChia["Code"];
                                                newRowCTChia[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCHIA;
                                            }
                                        }
                                        DataRow[] drs_CTCon = ctCons.Select($"[CodeCongTacCha] = '{ctac["Code"]}'");
                                        bool? isCTMD = null;
                                        if (bool.TryParse(ctac["KhoiLuongToanBo_Iscongthucmacdinh"].ToString(), out bool check))
                                        {
                                            isCTMD = check;
                                        }

                                        double? sumCon = null;

                                        Dictionary<string, int> codesNhomDG = new Dictionary<string, int>();
                                        foreach (DataRow con in drs_CTCon)
                                        {
                                            string codeNhomDG = con["CodeNhom"].ToString();
                                            int? crRowNhomDGInd = null;
                                            if (codeNhomDG.HasValue())
                                            {
                                                if (!codesNhomDG.ContainsKey(codeNhomDG))
                                                {
                                                    DataRow newRowNhomDG = dtData.NewRow();
                                                    dtData.Rows.Add(newRowNhomDG);
                                                    crRowNhomDGInd = crRowInd = fstInd + dtData.Rows.Count;

                                                    newRowNhomDG[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_NHOMDIENGIAI;
                                                    newRowNhomDG[TDKH.COL_DanhMucCongTac] = con["TenNhom"];
                                                    newRowNhomDG[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowCtInd) + 1})";
                                                    newRowNhomDG[TDKH.COL_Code] = con["CodeNhom"];
                                                    newRowNhomDG[TDKH.COL_TypeRow] = MyConstant.TYPEROW_NhomDienGiai;

                                                    codesNhomDG.Add(codeNhomDG, crRowInd);
                                                }
                                                crRowNhomDGInd = codesNhomDG[codeNhomDG];
                                            }

                                            DataRow newRowCon = dtData.NewRow();
                                            dtData.Rows.Add(newRowCon);
                                            ++crRowInd;

                                            string value = (string)con["TenCongTac"];

                                            newRowCon[TDKH.COL_DanhMucCongTac] = value;
                                            //newRowCTac[TDKH.COL_DonVi] = con["DonVi"];

                                            newRowCon[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowNhomDGInd ?? crRowCtInd) + 1})";
                                            newRowCon[TDKH.COL_Code] = con["Code"];
                                            newRowCon[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCON;

                                            if (!CheckCongThucDienGiai(value, out string newValue, out double KL1BP))
                                            {
                                                newRowCon[TDKH.COL_DBC_SoBoPhanGiongNhau] = con[TDKH.COL_DBC_SoBoPhanGiongNhau];
                                                newRowCon[TDKH.COL_DBC_Dai] = con[TDKH.COL_DBC_Dai];
                                                newRowCon[TDKH.COL_DBC_Rong] = con[TDKH.COL_DBC_Rong];
                                                newRowCon[TDKH.COL_DBC_Cao] = con[TDKH.COL_DBC_Cao];
                                                newRowCon[TDKH.COL_DBC_HeSoCauKien] = con[TDKH.COL_DBC_HeSoCauKien];

                                                newRowCon[TDKH.COL_DBC_KL1BoPhan] = $"{prefixFormula}PRODUCT({dic[TDKH.COL_DBC_SoBoPhanGiongNhau]}{crRowInd + 1};" +
                                                                $"{dic[TDKH.COL_DBC_Dai]}{crRowInd + 1};" +
                                                                $"{dic[TDKH.COL_DBC_Rong]}{crRowInd + 1};" +
                                                                $"{dic[TDKH.COL_DBC_Cao]}{crRowInd + 1};" +
                                                                $"{dic[TDKH.COL_DBC_HeSoCauKien]}{crRowInd + 1})";


                                                int.TryParse(con[TDKH.COL_DBC_SoBoPhanGiongNhau].ToString(), out int SBPGN);
                                                int.TryParse(con[TDKH.COL_DBC_Dai].ToString(), out int Dai);
                                                int.TryParse(con[TDKH.COL_DBC_Rong].ToString(), out int Rong);
                                                int.TryParse(con[TDKH.COL_DBC_Cao].ToString(), out int Cao);
                                                int.TryParse(con[TDKH.COL_DBC_HeSoCauKien].ToString(), out int HeSo);

                                                KL1BP = (double)Math.Round((double)SBPGN * Dai * Rong * Cao * HeSo, 3);
                                            }
                                            else
                                            {
                                                newRowCon[TDKH.COL_DBC_KL1BoPhan] = KL1BP;
                                            }
                                            int.TryParse(con[TDKH.COL_DBC_KL1BoPhan].ToString(), out int KL1BPOld);

                                            if (Math.Abs(KL1BP - KL1BPOld) > 0.01)
                                                con[TDKH.COL_DBC_KL1BoPhan] = KL1BP;

                                            sumCon = (sumCon ?? 0) + KL1BP;
                                            newRowCon[TDKH.COL_DanhMucCongTac] = value;

                                        }
                                        double.TryParse(ctac["KhoiLuongToanBo"].ToString(), out double kltbOld);

                                        if (sumCon.HasValue)
                                        {
                                            if (!isCTMD.HasValue)
                                            {
                                                isCTMD = (Math.Abs(sumCon.Value - kltbOld) <= 0.01);
                                                ctac["KhoiLuongToanBo_Iscongthucmacdinh"] = isCTMD;
                                            }
                                            else if (isCTMD == true && Math.Abs(sumCon.Value - kltbOld) > 0.01)
                                            {
                                                ctac[TDKH.COL_DBC_KhoiLuongToanBo] = sumCon;
                                            }
                                        }
                                        else if (isCTMD != true)
                                        {
                                            isCTMD = true;
                                            if (ctac["KhoiLuongToanBo_Iscongthucmacdinh"].ToString() != true.ToString())
                                                ctac["KhoiLuongToanBo_Iscongthucmacdinh"] = true;
                                        }

                                        newRowCTac[TDKH.COL_KhoiLuongToanBo_Iscongthucmacdinh] = isCTMD;
                                        if (isCTMD == true && sumCon.HasValue)
                                        {
                                            newRowCTac[TDKH.COL_DBC_KhoiLuongToanBo] = $"{prefixFormula}SUM({dic[TDKH.COL_DBC_KL1BoPhan]}{crRowCtInd + 2}: {dic[TDKH.COL_DBC_KL1BoPhan]}{crRowInd + 1})";
                                        }

                                    }
                                }
                            }



                            if (isPhanTuyen)
                            {
                                DataRow newRowPT = dtData.NewRow();
                                dtData.Rows.Add(newRowPT);

                                ++crRowInd;

                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                newRowPT[TDKH.COL_Code] = codePT;
                                newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;

                                newRowPT[TDKH.COL_DanhMucCongTac] = $"{prefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                                newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";
                            }
                        }

                        DataRow newRow = dtData.NewRow();
                        dtData.Rows.Add(newRow);

                        ++crRowInd;
                    }
                }

                int numRow = dtData.Rows.Count;
                ws.Rows.Insert(definedName.Range.BottomRowIndex, numRow + 5, RowFormatMode.FormatAsNext);
                ws.Import(dtData, false, rowTongInd + 1, 0);
                ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;
                SpreadsheetHelper.ReplaceAllFormulaAfterImport(definedName.Range);

                SpreadsheetHelper.FormatRowsInRange(definedName.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha],
                    dic[TDKH.COL_Code], colMaCongTac: dic[TDKH.COL_MaHieuCongTac], codesCT: CongTacs.AsEnumerable().Select(x => x["MaHieuCongTac"].ToString()));
                CheckAnHienDienGiai();
                CheckAnHienPhanDoan();

                wb.EndUpdate();

                try
                {
                    //////          //wb.History.IsEnabled = true;

                }
                catch (Exception) { }
                WaitFormHelper.ShowWaitForm("Đang cập đồng bộ cơ sở dữ liệu");

                //if (setSortId)
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(CongTacs.AsEnumerable().Where(x => x["Code"] != DBNull.Value).ToArray(), TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(ctCons, TDKH.TBL_ChiTietCongTacCon);
            }
            finally
            {
                WaitFormHelper.CloseWaitForm();
                //SharedControls.spsheet_TD_KH_LapKeHoach.CustomDrawCell += FormMainHelper.spsheet_TD_KH_LapKeHoach_CustomDrawCell;
                //SharedControls.spsheet_TD_KH_LapKeHoach.Refresh();
                //SharedControls.spsheet_TD_KH_LapKeHoach.CustomDrawCell -= FormMainHelper.spsheet_TD_KH_LapKeHoach_CustomDrawCell;
                //wb.History.IsEnabled = true;

            }

        }

        public static void GetFormulaMimMaxDate(int minIndCon, int maxIndCon, string colNBD, string colNKT, out string forNBD, out string forNKT)
        {
            string lsDateBD = $"{colNBD}{minIndCon + 1}:{colNBD}{maxIndCon + 1}";
            string lsDateKT = $"{colNKT}{minIndCon + 1}:{colNKT}{maxIndCon + 1}";

            forNBD = $"IF(MIN({lsDateBD})>0; MIN({lsDateBD}); \"\")";
            forNKT = $"IF(MAX({lsDateKT})>0; MAX({lsDateKT}); \"\")";
        }

        public static string GetFormulaSumChild(int minIndCon, int maxIndCon, string col, string colRowCha)
        {
            return $"SUMIF({colRowCha}{minIndCon + 1}:{colRowCha}{maxIndCon + 1};ROW();{col}{minIndCon + 1}:{col}{maxIndCon + 1})";
        }

        public static void LoadCongKinhPhiTienDo()
        {
            var wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            var ws = wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi];

            //wb.History.Clear();
            //wb.History.IsEnabled = false;
            try
            {
                //SharedControls.spsheet_TD_KH_LapKeHoach.CustomDrawCell += FormMainHelper.spsheet_TD_KH_LapKeHoach_CustomDrawCell;

                var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
                if (crDVTH is null)
                {
                    return;
                }

                WaitFormHelper.ShowWaitForm("Đang tải kinh phí");
                TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);


                DataTable CongTacs = TDKHHelper.GetCongTacsDataTable(SharedControls.slke_ThongTinDuAn.EditValue?.ToString(), codeCT, codeHM,
                    SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString(), $"cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}'", GetAllHM: false);

                //CongTacs = CongTacs.AsEnumerable().Where(x => x[""]);

                //string strCodeCTTheoKy = MyFunction.fcn_Array2listQueryCondition(CongTacs.AsEnumerable().Select(x => x["Code"].ToString()));
                var lsCode = CongTacs.AsEnumerable().Select(x => x["Code"].ToString());
                var codesNhom = CongTacs.AsEnumerable().Where(x => x["CodeNhom"] != DBNull.Value)
                                    .Select(x => x["CodeNhom"].ToString()).Distinct();

                //var KLHNNhoms = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, codesNhom, ignoreKLKH: true);
                //List<KLHN> KLHNs = new List<KLHN>();
                //List<KLHN> KLHNNhoms = new List<KLHN>();
                string strCodeNhom = MyFunction.fcn_Array2listQueryCondition(codesNhom);

                string dbString = $"SELECT nhom.*, " +
                     $"TOTAL(klhn.KhoiLuongThiCong) AS KhoiLuongDaThiCong,\r\n" +
                    $"TOTAL(klhn.KhoiLuongThiCong*nhom.DonGiaThiCong) AS ThanhTienDaThiCong,\r\n" +
                    $"MAX(klhn.Ngay) AS NgayKetThucThiCong,\r\n" +
                    $"MIN(klhn.Ngay) AS NgayBatDauThiCong\r\n" +
                    $"FROM {TDKH.TBL_NhomCongTac} nhom\r\n" +
                    $"JOIN {TDKH.TBL_NhomCongTac} nhomtc\r\n" +
                    $"ON nhom.CodeNhomGiaoThau IS NULL AND nhom.Code = nhomtc.Code\r\n" +
                    $"LEFT JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn\r\n " +
                    $"ON klhn.KhoiLuongThiCong > 0 AND nhomtc.Code = klhn.CodeNhom\r\n" +
                    $"WHERE nhom.Code IN ({strCodeNhom})\r\n" +
                    $"GROUP BY nhom.Code\r\n" +
                    $"UNION ALL\r\n" +
                    $"SELECT nhom.*, " +
                     $"TOTAL(klhn.KhoiLuongThiCong) AS KhoiLuongDaThiCong,\r\n" +
                    $"TOTAL(klhn.KhoiLuongThiCong*nhom.DonGiaThiCong) AS ThanhTienDaThiCong,\r\n" +
                    $"MAX(klhn.Ngay) AS NgayKetThucThiCong,\r\n" +
                    $"MIN(klhn.Ngay) AS NgayBatDauThiCong\r\n" +
                    $"FROM {TDKH.TBL_NhomCongTac} nhom\r\n" +

                    $"LEFT JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn\r\n " +
                    $"ON klhn.KhoiLuongThiCong > 0 AND nhom.Code = klhn.CodeNhom\r\n" +
                    $"WHERE nhom.CodeNhomGiaoThau IS NOT NULL AND nhom.Code IN ({strCodeNhom})\r\n" +
                    $"GROUP BY nhom.Code\r\n" +
                    $"";
                DataTable dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                var codesNhomNotHasHasKls = dtNhom.AsEnumerable().Where(x => x["KhoiLuongKeHoach"] == DBNull.Value).Select(x => x["Code"].ToString());

                var lsCodeNotHasNhom = CongTacs.AsEnumerable().Where(x => x["CodeNhom"] == DBNull.Value || codesNhomNotHasHasKls.Contains(x["CodeNhom"].ToString())).Select(x => x["Code"].ToString());
                //var KLHNs = (MSETTING.Default.CongTacInNhomVisible)
                //    ? MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lsCode, ignoreKLKH: true)
                //    : MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lsCodeNotHasNhom, ignoreKLKH: true);
                TDKHHelper.ReCalcNhomPeriod();

                dbString = $"SELECT COALESCE(con.DonVi, dmct.DonVi) AS DonVi,COALESCE(con.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
                    $"COALESCE(con.KyHieuBanVe, dmct.KyHieuBanVe) AS KyHieuBanVe,COALESCE(con.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                    $"con.*,Mui.Code as CodeMuiThiCong,Mui.Ten as MuiThiCong,TOTAL(hp.HeSoNguoiDung*hp.DinhMucNguoiDung)*con.KhoiLuongToanBo AS NhanCongReal " +
    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} con\r\n" +
    $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct ON dmct.Code=con.CodeCongTac \r\n" +
    $"LEFT JOIN {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
    $"ON hp.CodeCongTac = con.Code AND hp.LoaiVatTu = 'Nhân công'\r\n" +
    $"LEFT JOIN {TDKH.Tbl_TDKH_MuiThiCong} Mui\r\n" +
    $"ON con.CodeMuiThiCong = Mui.Code\r\n" +
    $"WHERE con.CodeCha IN ({MyFunction.fcn_Array2listQueryCondition(lsCode)}) GROUP BY con.Code ORDER BY \"Row\" ASC";
                DataTable ctConChia = DataProvider.InstanceTHDA
                    .ExecuteQuery(dbString);
                DataTable dtData = DataTableCreateHelper.TienDoKinhPhi();

                var grsCTrinh = CongTacs.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);

                int STTCTrinh = 0;
                int STTCtChia = 1;
                //FileHelper.fcn_spSheetStreamDocument(SharedControls.spsheet_TD_KH_LapKeHoach, $@"{BaseFrom.m_templatePath}\FileExcel\6.aBangNhapKeHachDanhGiaLoiNhuan.xlsx");


                var definedName = wb.DefinedNames.GetDefinedName(TDKH.RANGE_KeHoach);

                Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
                BaseFrom.IndsCheckBoxKeHoach = new List<int>()
                {
                    0
                };
                BaseFrom.IndFstCellDataKeHoach = definedName.Range.TopRowIndex;


                if (!BaseFrom.allPermission.HaveInitProjectPermission)
                {
                    ws.Columns[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Visible = false;
                    ws.Columns[dic[TDKH.COL_DonGia]].Visible = false;
                }
                Dictionary<int, int> dicFomular = new Dictionary<int, int>();

                int fstInd = definedName.Range.TopRowIndex;
                wb.BeginUpdate();

                if (definedName.Range.RowCount > 2)
                {
                    ws.Rows.Remove(definedName.Range.TopRowIndex + 1, definedName.Range.RowCount - 2);
                }

                ws.Range["DateRange"].Clear();
                for (int i = definedName.Range.TopRowIndex; i <= definedName.Range.BottomRowIndex; i++)
                {
                    ws.Rows[i].Visible = true;
                }

ws.Columns[dic[TDKH.COL_KhoiLuongNhapVao]].Visible =
        ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Visible =
        ws.Columns[dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]].Visible =
        ws.Columns[dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]].Visible =
        ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]].Visible =
        ws.Columns[dic[TDKH.COL_KhoiLuongConLaiGiaiDoan]].Visible =
        ws.Columns[dic[TDKH.COL_GiaTriKeHoachGiaiDoan]].Visible =
        ws.Columns[dic[TDKH.COL_GiaTriThiCongGiaiDoan]].Visible =
        ws.Columns[dic[TDKH.COL_GiaTriBoSungGiaiDoan]].Visible =
        ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan]].Visible =
        ws.Columns[dic[TDKH.COL_GiaTriConLaiGiaiDoan]].Visible =
                    ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc]].Visible =
                    ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc]].Visible =
                    ws.Columns[dic[TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong]].Visible =
                    ws.Columns[dic[TDKH.COL_GiaTriConLaiSoVoiHopDong]].Visible =
                    ws.Columns[dic[TDKH.COL_PhanTramGiaTriKyNay]].Visible =
                    ws.Columns[dic[TDKH.COL_PhanTramGiaTriLuyKeKyNay]].Visible = false;


                definedName.Range.ForEach<Cell>(x => x.SetValue(""));

                string prefixFormula = MyConstant.PrefixFormula;
                ////          //wb.History.IsEnabled = false;
                string forGiaTri, forNBD, forNKT, forNBDTC, forNKTTC;
                var crRowInd = definedName.Range.TopRowIndex;

                var colsSum = dic.Keys.Where(x => x.StartsWith("GiaTri")).ToList();
                colsSum.Add(TDKH.COL_GiaTri);
                colsSum.Add(TDKH.COL_GiaTriThiCong);
                int countCTac = 0;
                int countAll = CongTacs.Rows.Count;
                foreach (var grCTrinh in grsCTrinh)
                {
                    DataRow newRowCtrinh = dtData.NewRow();
                    dtData.Rows.Add(newRowCtrinh);

                    var crRowCtrInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                    //var crBottomInd = definedName.Range.BottomRowIndex + 1 + dtData.Rows.Count;
                    DataRow fstCtr = grCTrinh.First();
                    newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                    newRowCtrinh[TDKH.COL_Code] = grCTrinh.Key;
                    newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                    newRowCtrinh[TDKH.COL_DanhMucCongTac] = fstCtr["TenCongTrinh"];
                    newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                    newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{fstInd + 1})";
                    newRowCtrinh[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";



                    var grsHM = grCTrinh.GroupBy(x => x["CodeHangMuc"]);
                    int STTHM = 0;
                    foreach (var grHM in grsHM)
                    {
                        DataRow newRowHM = dtData.NewRow();
                        dtData.Rows.Add(newRowHM);

                        var crRowHMInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        //crBottomInd = definedName.Range.BottomRowIndex + 1 + dtData.Rows.Count;

                        DataRow fstHM = grHM.First();
                        newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                        newRowHM[TDKH.COL_Code] = grHM.Key;
                        newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                        newRowHM[TDKH.COL_DanhMucCongTac] = fstHM["TenHangMuc"];
                        newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                        newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd + 1})";
                        newRowHM[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";

                        var grsPhanTuyen = grHM.GroupBy(x => (int)x["IndPT"])
                            .OrderBy(x => x.Key);

                        foreach (var grPhanTuyen in grsPhanTuyen)
                        {
                            //int? crRowPTInd = null;
                            DataRow fstPT = grPhanTuyen.First();
                            string codePT = fstPT["CodePhanTuyen"].ToString();
                            bool isPhanTuyen = codePT.HasValue();
                            int? crRowPTInd = null;
                            DataRow newRowPT = null;
                            if (isPhanTuyen)
                            {
                                if (fstPT["TenDDTuyen"] != DBNull.Value)
                                {
                                    DataRow newRowDD = dtData.NewRow();
                                    dtData.Rows.Add(newRowDD);
                                    //crRowPTInd = crRowInd = RangeData.TopRowIndex + dtData.Rows.Count;
                                    newRowDD[TDKH.COL_DanhMucCongTac] = $"{fstPT["TenDDTuyen"]}";
                                    newRowDD[TDKH.COL_TypeRow] = MyConstant.TYPEROW_GOP;
                                    //newRowDD[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd})";
                                }
                                newRowPT = dtData.NewRow();
                                dtData.Rows.Add(newRowPT);

                                crRowPTInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                //crBottomInd = definedName.Range.BottomRowIndex + 1 + dtData.Rows.Count;
                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                newRowPT[TDKH.COL_Code] = codePT;
                                newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                                newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
                                newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                                newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";
                                newRowPT[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";

                            }

                            var grsNhom = grPhanTuyen.GroupBy(x => (int)x["IndNhom"])
                                        .OrderBy(x => x.Key);

                            foreach (var grNhom in grsNhom)
                            {
                                var fstNhom = grNhom.First();
                                string codeNhom = fstNhom["CodeNhom"].ToString();
                                bool isNhom = codeNhom.HasValue();
                                bool isNhomHasValue = false;
                                int? crRowNhomInd = null;
                                DataRow newRowNhom = null;
                                DataRow drNhom = null;
                                if (isNhom)
                                {
                                    if (fstNhom["TenDDNhom"] != DBNull.Value)
                                    {
                                        DataRow newRowDD = dtData.NewRow();
                                        dtData.Rows.Add(newRowDD);
                                        newRowDD[TDKH.COL_DanhMucCongTac] = $"{fstNhom["TenDDNhom"]}";
                                        newRowDD[TDKH.COL_TypeRow] = MyConstant.TYPEROW_GOP;
                                    }
                                    drNhom = dtNhom.AsEnumerable().Single(x => x["Code"].ToString() == codeNhom);
                                    newRowNhom = dtData.NewRow();
                                    if (drNhom["KhoiLuongKeHoach"] != DBNull.Value)
                                    {
                                        isNhomHasValue = true;
                                    }
                                    dtData.Rows.Add(newRowNhom);
                                    if (drNhom["GhiChuBoSungJson"] != DBNull.Value)
                                    {
                                        var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(drNhom["GhiChuBoSungJson"].ToString());
                                        newRowNhom[TDKH.COL_STTDocVao] = GhiChuBoSungJson.STT;
                                        newRowNhom[TDKH.COL_STTND] = GhiChuBoSungJson.STTND;
                                    }
                                    crRowNhomInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                    //crBottomInd = definedName.Range.BottomRowIndex + 1 + dtData.Rows.Count;

                                    newRowNhom[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_NHOM;
                                    newRowNhom[TDKH.COL_DanhMucCongTac] = drNhom["Ten"];
                                    newRowNhom[TDKH.COL_DonVi] = drNhom["DonVi"];
                                    newRowNhom[TDKH.COL_DBC_KhoiLuongToanBo] = drNhom["KhoiLuongKeHoach"];
                                    newRowNhom[TDKH.COL_KhoiLuongHopDongChiTiet] = drNhom["KhoiLuongHopDongChiTiet"];
                                    newRowNhom[TDKH.COL_DonGia] = drNhom["DonGia"];
                                    newRowNhom[TDKH.COL_DonGiaThiCong] = drNhom["DonGiaThiCong"];
                                    newRowNhom[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd) + 1})";
                                    newRowNhom[TDKH.COL_Code] = drNhom["Code"];
                                    newRowNhom[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                                    newRowNhom[TDKH.COL_TrangThai] = drNhom["TrangThai"];

                                    newRowNhom[TDKH.COL_PhanTramThucHien] = $"IF({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} = 0;\"\";{prefixFormula}{dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}/{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1})";
                                    newRowNhom[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                    
                                    newRowNhom[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                    newRowNhom[TDKH.COL_KinhPhiDuKien] = $"{prefixFormula}IF({dic[TDKH.COL_GiaTri]}{fstInd + 1}=0;\"\";{dic[TDKH.COL_GiaTri]}{fstInd + 1}*{TDKH.RANGE_KinhPhiPhanBoToanDuAn}{dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{dic[TDKH.COL_GiaTri]}{fstInd + 1}*{TDKH.RANGE_KinhPhiPhanBoToanDuAn})";
                                    newRowNhom[TDKH.COL_GiaTriConLaiSoVoiKeHoach] = $"{prefixFormula}ROUND({dic[TDKH.COL_GiaTri]}{crRowInd + 1}-{dic[TDKH.COL_GiaTriThiCong]}{crRowInd + 1}; 0)";
                                    newRowNhom[TDKH.COL_GiaTriConLaiSoVoiHopDong] = $"{prefixFormula}ROUND({dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}-{dic[TDKH.COL_GiaTriThiCong]}{crRowInd + 1}; 0)";
                                    newRowNhom[TDKH.COL_KhoiLuongConLaiGiaiDoan] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]}{crRowInd + 1}-{dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}";
                                    newRowNhom[TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1}-{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]}{crRowInd + 1}";
                                    newRowNhom[TDKH.COL_PhanTramGiaTriKyNay] = $"{prefixFormula}IF({dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]}{crRowInd + 1} = 0;\"\";{dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}/{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]}{crRowInd + 1})";
                                    newRowNhom[TDKH.COL_PhanTramGiaTriLuyKeKyNay] = $"{prefixFormula}IF({dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1} = 0;\"\";{dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}/{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1})";


                                    newRowNhom[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixDate}{drNhom["NgayBatDauThiCong"]}";
                                    newRowNhom[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixDate}{drNhom["NgayKetThucThiCong"]}";
                                    newRowNhom[TDKH.COL_KhoiLuongDaThiCong] = drNhom["KhoiLuongDaThiCong"];
                                    newRowNhom[TDKH.COL_GiaTriThiCong] = drNhom["ThanhTienDaThiCong"];

                                    //if (crDVTH.IsGiaoThau)
                                    //{

                                    //    newRowNhom[TDKH.COL_NgayBatDauThiCong] = kltchnsInNhom.Min(x => x.Ngay);
                                    //    newRowNhom[TDKH.COL_NgayKetThucThiCong] = kltchnsInNhom.Max(x => x.Ngay);
                                    //    newRowNhom[TDKH.COL_KhoiLuongDaThiCong] = kltchnsInNhom.Sum(x => x.KhoiLuongThiCong);
                                    //    newRowNhom[TDKH.COL_GiaTriThiCong] = kltchnsInNhom.Sum(x => x.ThanhTienThiCong) ?? 0;

                                    //}
                                    //else
                                    //{

                                    //    //var kltchnsInNhom = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, new string[] { drNhom["Code"].ToString() }, ignoreKLKH: true);
                                    //    //if (kltchnsInNhom.Any())
                                    //    //{
                                    //    //    newRowNhom[TDKH.COL_NgayBatDauThiCong] = kltchnsInNhom.Min(x => x.Ngay);
                                    //    //    newRowNhom[TDKH.COL_NgayKetThucThiCong] = kltchnsInNhom.Max(x => x.Ngay);
                                    //    //    newRowNhom[TDKH.COL_KhoiLuongDaThiCong] = kltchnsInNhom.Sum(x => x.KhoiLuongThiCong);
                                    //    //    newRowNhom[TDKH.COL_GiaTriThiCong] = kltchnsInNhom.Sum(x => x.ThanhTienThiCong) ?? 0;
                                    //    //}
                                    //}
                                    newRowNhom[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;

                                }
                                int STTCTac = 0;
                                if (!isNhomHasValue || MSETTING.Default.CongTacInNhomVisible)
                                {
                                    foreach (var ctac in grNhom)
                                    {
                                        if (ctac["TenDDCT"] != DBNull.Value)
                                        {
                                            DataRow newRowDD = dtData.NewRow();
                                            dtData.Rows.Add(newRowDD);
                                            newRowDD[TDKH.COL_DanhMucCongTac] = $"{ctac["TenDDCT"]}";
                                            newRowDD[TDKH.COL_TypeRow] = MyConstant.TYPEROW_GOP;
                                        }
                                        WaitFormHelper.ShowWaitForm("Đang tải kinh phí\r\n" +
                                            $"{++countCTac}/{countAll}: {ctac["TenCongTac"]}");
                                        DataRow newRowCTac = dtData.NewRow();
                                        dtData.Rows.Add(newRowCTac);


                                        var crrowIndCt = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                        //crBottomInd = definedName.Range.BottomRowIndex + 1 + dtData.Rows.Count;
                                        if (ctac["Code"] == DBNull.Value)
                                            continue;
                                        if (ctac["GhiChuBoSungJson"] != DBNull.Value)
                                        {
                                            var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(ctac["GhiChuBoSungJson"].ToString());
                                            newRowCTac[TDKH.COL_STTDocVao] = GhiChuBoSungJson.STT;
                                            newRowCTac[TDKH.COL_STTND] = GhiChuBoSungJson.STTND;
                                        }

                                        newRowCTac[TDKH.COL_STT] = ++STTCTac;
                                        newRowCTac[TDKH.COL_MaHieuCongTac] = ctac["MaHieuCongTac"];
                                        newRowCTac[TDKH.COL_DanhMucCongTac] = ctac["TenCongTac"];
                                        newRowCTac[TDKH.COL_DonVi] = ctac["DonVi"];
                                        newRowCTac[TDKH.COL_DBC_KhoiLuongToanBo] = ctac["KhoiLuongToanBo"];
                                        newRowCTac[TDKH.COL_KhoiLuongHopDongChiTiet] = ctac["KhoiLuongHopDongChiTiet"];

                                        newRowCTac[TDKH.COL_PhanTramThucHien] = $"{prefixFormula}IF({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} = 0;\"\";{dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}/{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1})";
                                        newRowCTac[TDKH.COL_DonGia] = ctac["DonGia"];
                                        newRowCTac[TDKH.COL_DonGiaThiCong] = ctac["DonGiaThiCong"];
                                        newRowCTac[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{ctac["NgayBatDau"]}"; //ctac["NgayBatDau"];
                                        newRowCTac[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{ctac["NgayKetThuc"]}"; //ctac["NgayKetThuc"];
                                        newRowCTac[TDKH.COL_NgayBatDauGT] = $"{MyConstant.PrefixDate}{ctac["NBDGT"]}"; //ctac["NgayBatDau"];
                                        newRowCTac[TDKH.COL_NgayKetThucGT] = $"{MyConstant.PrefixDate}{ctac["NKTGT"]}"; //ctac["NgayKetThuc"];
                                        newRowCTac[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                        newRowCTac[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                        newRowCTac[TDKH.COL_TrangThai] = ctac["TrangThai"];
                                        newRowCTac[TDKH.COL_NhanCong] = ctac["NhanCongReal"];
                                        newRowCTac[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                        newRowCTac[TDKH.COL_GiaTriThiCong] = $"{prefixFormula}ROUND({dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}; 0)";
                                        newRowCTac[TDKH.COL_GiaTriNhanThau] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}; 0)";
                                        newRowCTac[TDKH.COL_GiaTriConLaiSoVoiKeHoach] = $"{prefixFormula}ROUND({dic[TDKH.COL_GiaTri]}{crRowInd + 1}-{dic[TDKH.COL_GiaTriThiCong]}{crRowInd + 1}; 0)";
                                        newRowCTac[TDKH.COL_GiaTriConLaiSoVoiHopDong] = $"{prefixFormula}ROUND({dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}-{dic[TDKH.COL_GiaTriThiCong]}{crRowInd + 1}; 0)";
                                        newRowCTac[TDKH.COL_KhoiLuongConLaiGiaiDoan] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]}{crRowInd + 1}-{dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}";
                                        newRowCTac[TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1}-{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]}{crRowInd + 1}";
                                        newRowCTac[TDKH.COL_PhanTramGiaTriKyNay] = $"{prefixFormula}ROUND({dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}/{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]}{crRowInd + 1}; 2)";
                                        newRowCTac[TDKH.COL_PhanTramGiaTriLuyKeKyNay] = $"{prefixFormula}ROUND({dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}/{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1}; 2)";

                                        newRowCTac[TDKH.COL_KinhPhiDuKien] = $"{prefixFormula}ROUND({dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{dic[TDKH.COL_GiaTri]}{fstInd + 1}*{TDKH.RANGE_KinhPhiPhanBoToanDuAn}; 0)";

                                        newRowCTac[TDKH.COL_GhiChu] = ctac["GhiChu"];


                                        newRowCTac[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixDate}{ctac["NgayBatDauThiCong"]}";
                                        newRowCTac[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixDate}{ctac["NgayKetThucThiCong"]}";
                                        newRowCTac[TDKH.COL_KhoiLuongDaThiCong] = ctac["KhoiLuongDaThiCong"];
                                        newRowCTac[TDKH.COL_GiaTriThiCong] = ctac["ThanhTienDaThiCong"];


                                        newRowCTac[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowNhomInd ?? crRowPTInd ?? crRowHMInd) + 1})";
                                        newRowCTac[TDKH.COL_Code] = ctac["Code"];
                                        newRowCTac[TDKH.COL_CodeDMCT] = ctac["CodeDanhMucCongTac"];
                                        newRowCTac[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;



                                        DataRow[] drs_ChiaCT = ctConChia.Select($"[CodeCha] = '{ctac["Code"]}'");
                                        if (drs_ChiaCT.Any())
                                        {
                                            dicFomular.Add(crRowInd, drs_ChiaCT.Count());
                                            STTCtChia = 1;
                                            //newRowCTac[TDKH.COL_NgayBatDau] = $"{prefixFormula}MIN({dic[TDKH.COL_NgayBatDau]}{crRowInd + 1}:{dic[TDKH.COL_NgayBatDau]}{crRowInd + drs_ChiaCT.Count()})";
                                            //newRowCTac[TDKH.COL_NgayKetThuc] = $"{prefixFormula}MAX({dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1}:{dic[TDKH.COL_NgayKetThuc]}{crRowInd + drs_ChiaCT.Count()})";
                                            foreach (var RowChia in drs_ChiaCT)
                                            {
                                                int? crRowCTChia = crRowInd = fstInd + dtData.Rows.Count;
                                                DataRow newRowCTChia = dtData.NewRow();
                                                dtData.Rows.Add(newRowCTChia);

                                                newRowCTChia[TDKH.COL_STT] = $"{STTCTac}.{STTCtChia++}";
                                                newRowCTChia[TDKH.COL_MaHieuCongTac] = ctac["MaHieuCongTac"];
                                                newRowCTChia[TDKH.COL_DanhMucCongTac] = ctac["TenCongTac"];
                                                newRowCTChia[TDKH.COL_DonVi] = ctac["DonVi"];
                                                newRowCTChia[TDKH.COL_DBC_KhoiLuongToanBo] = RowChia["KhoiLuongToanBo"];
                                                newRowCTChia[TDKH.COL_KhoiLuongHopDongChiTiet] = RowChia["KhoiLuongHopDongChiTiet"];
                                                newRowCTChia[TDKH.COL_PhanTramThucHien] = $"{prefixFormula}IF({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} = 0;\"\";{dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}/{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1})";
                                                newRowCTChia[TDKH.COL_DonGia] = RowChia["DonGia"];
                                                newRowCTChia[TDKH.COL_DonGiaThiCong] = RowChia["DonGiaThiCong"];
                                                newRowCTChia[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{RowChia["NgayBatDau"]}"; //RowChia["NgayBatDau"];
                                                newRowCTChia[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{RowChia["NgayKetThuc"]}"; //RowChia["NgayKetThuc"];
                                                newRowCTChia[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                                newRowCTChia[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                                newRowCTChia[TDKH.COL_TrangThai] = RowChia["TrangThai"];
                                                newRowCTChia[TDKH.COL_NhanCong] = RowChia["NhanCongReal"];
                                                newRowCTChia[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
                                                newRowCTChia[TDKH.COL_GiaTriNhanThau] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}; 0)";
                                                newRowCTChia[TDKH.COL_KinhPhiDuKien] = $"{prefixFormula}IF({dic[TDKH.COL_GiaTri]}{fstInd + 1}*{TDKH.RANGE_KinhPhiPhanBoToanDuAn} = 0;\"\";{dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{dic[TDKH.COL_GiaTri]}{fstInd + 1}*{TDKH.RANGE_KinhPhiPhanBoToanDuAn})";
                                                newRowCTChia[TDKH.COL_GhiChu] = RowChia["GhiChu"];


                                                //var klhnsInCTacChia = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, new string[] { RowChia["Code"].ToString() }, ignoreKLKH: true);
                                                //if (klhnsInCTacChia.Any())
                                                //{
                                                //    newRowCTChia[TDKH.COL_NgayBatDauThiCong] = klhnsInCTacChia.Min(x => x.Ngay);
                                                //    newRowCTChia[TDKH.COL_NgayKetThucThiCong] = klhnsInCTacChia.Max(x => x.Ngay);
                                                //    newRowCTChia[TDKH.COL_KhoiLuongDaThiCong] = klhnsInCTacChia.Sum(x => x.KhoiLuongThiCong);
                                                //    newRowCTChia[TDKH.COL_GiaTriThiCong] = klhnsInCTacChia.Sum(x => x.ThanhTienThiCong) ?? 0;
                                                //}


                                                newRowCTChia[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCTChia + 1})";
                                                newRowCTChia[TDKH.COL_Code] = RowChia["Code"];
                                                newRowCTChia[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCHIA;
                                            }
                                        }

                                    }
                                }
                                if (isNhom)
                                {
                                    crRowInd = fstInd + dtData.Rows.Count;

                                    //var forKPDK = GetFormulaSumChild(crRowInd + 1, dic[TDKH.COL_KinhPhiDuKien], dic[TDKH.COL_RowCha]);

                                    if (drNhom["KhoiLuongKeHoach"] == DBNull.Value)
                                    {
                                        GetFormulaMimMaxDate((crRowNhomInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                                        GetFormulaMimMaxDate((crRowNhomInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                                        newRowNhom[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                                        newRowNhom[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                                        newRowNhom[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                                        newRowNhom[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                                        foreach (string col in colsSum)
                                        {
                                            forGiaTri = GetFormulaSumChild((crRowNhomInd ?? 0), crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                            newRowNhom[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                        }
                                    }
                                    else
                                    {
                                        newRowNhom[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowNhomInd + 1}*{dic[TDKH.COL_DonGia]}{crRowNhomInd + 1}; 0)";
                                        newRowNhom[TDKH.COL_GiaTriNhanThau] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowNhomInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowNhomInd + 1}; 0)";
                                        newRowNhom[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{drNhom["NgayBatDau"]}";
                                        newRowNhom[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{drNhom["NgayKetThuc"]}";



                                    }

                                }

                            }
                            if (isPhanTuyen)
                            {
                                DataRow newRowHTPT = dtData.NewRow();
                                dtData.Rows.Add(newRowHTPT);


                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                newRowHTPT[TDKH.COL_Code] = codePT;
                                newRowHTPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;

                                newRowHTPT[TDKH.COL_DanhMucCongTac] = $"{prefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                                newRowHTPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                newRowHTPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";

                                crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                                GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                                newRowPT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                                newRowPT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                                newRowPT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                                newRowPT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                                foreach (string col in colsSum)
                                {
                                    forGiaTri = GetFormulaSumChild((crRowPTInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                    newRowPT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                }
                            }
                        }

                        DataRow newRowEmpty = dtData.NewRow();
                        dtData.Rows.Add(newRowEmpty);

                        crRowInd = fstInd + dtData.Rows.Count;
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowHM[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowHM[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowHM[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowHM[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = GetFormulaSumChild(crRowHMInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }
                    }
                    crRowInd = fstInd + dtData.Rows.Count;
                    GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                    GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                    newRowCtrinh[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                    newRowCtrinh[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                    newRowCtrinh[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                    newRowCtrinh[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";


                    foreach (string col in colsSum)
                    {
                        forGiaTri = GetFormulaSumChild(crRowCtrInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                        newRowCtrinh[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                    }
                }

                var rowTong = dtData.NewRow();
                dtData.Rows.InsertAt(rowTong, 0);


                rowTong[TDKH.COL_DanhMucCongTac] = "TỔNG";
                crRowInd = fstInd - 1 + dtData.Rows.Count;
                GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                rowTong[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                rowTong[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                rowTong[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                rowTong[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";
                rowTong[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDau]}{fstInd + 1} + 1";
                rowTong[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{fstInd + 1} + 1";

                foreach (string col in colsSum)
                {
                    forGiaTri = GetFormulaSumChild(fstInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                    rowTong[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                }


                int numRow = dtData.Rows.Count;
                ws.Rows.Insert(definedName.Range.BottomRowIndex, numRow + 1, RowFormatMode.FormatAsNext);
                ws.Import(dtData, false, fstInd, 0);
                ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;
                SpreadsheetHelper.ReplaceAllFormulaAfterImport(definedName.Range);
                SpreadsheetHelper.FormatRowsInRange(definedName.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha],
                dic[TDKH.COL_Code], colMaCongTac: dic[TDKH.COL_MaHieuCongTac], codesCT: CongTacs.AsEnumerable().Select(x => x["MaHieuCongTac"].ToString()), 
                ColFileDinhKem: dic[TDKH.COL_FileDinhKem],dic:dic);
                foreach (var item in dicFomular)
                {
                    Row Crow = ws.Rows[item.Key];
                    Crow[dic[TDKH.COL_NgayBatDau]].Formula = $"MIN({dic[TDKH.COL_NgayBatDau]}{item.Key + 2}:{dic[TDKH.COL_NgayBatDau]}{item.Key + 1 + item.Value})";
                    Crow[dic[TDKH.COL_NgayKetThuc]].Formula = $"MAX({dic[TDKH.COL_NgayKetThuc]}{item.Key + 2}:{dic[TDKH.COL_NgayKetThuc]}{item.Key + 1 + item.Value})";
                }
                //int indKPPB = ws.Range.GetColumnIndexByName(dic[TDKH.COL_KinhPhiDuKien]);
                //int indGT = ws.Range.GetColumnIndexByName(dic[TDKH.COL_GiaTri]);
                //DinhMucHelper.fcn_TDKH_CapNhatRowChaConTienDoKeHoach(ws.Rows[fstInd]/*, indsNeedSum: new int[] {indKPPB, indGT}*/);
                //WaitFormHelper.ShowWaitForm("Đang cập đồng bộ cơ sở dữ liệu");

                wb.EndUpdate();
                //wb.History.Clear();
                //wb.History.IsEnabled = false;
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtNhom, TDKH.TBL_NhomCongTac);
                WaitFormHelper.CloseWaitForm();
                //if (SharedControls.ce_LocTheoNgay.Checked)
                LoadGiaiDoanKinhPhiTienDo();
                CheckAnHienPhanDoan();
                //ws.Calculate();
                //ws.Range["TBT_Value"].AutoFitColumns();
            }
            finally
            {
                WaitFormHelper.CloseWaitForm();
                //SharedControls.spsheet_TD_KH_LapKeHoach.CustomDrawCell -= FormMainHelper.spsheet_TD_KH_LapKeHoach_CustomDrawCell;
                //wb.History.IsEnabled = true;
            }
        }

        public static void LoadGiaiDoanKinhPhiTienDo()
        {

            var ws = SharedControls.spsheet_TD_KH_LapKeHoach.Document.Worksheets[TDKH.SheetName_KeHoachKinhPhi];
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            bool isLoc = SharedControls.ce_LocTheoNgay.Checked && SharedControls.de_Loc_TuNgay.EditValue != null && SharedControls.de_Loc_DenNgay.EditValue != null;
            var start = SharedControls.de_Loc_TuNgay.DateTime;
            var end = SharedControls.de_Loc_DenNgay.DateTime;
            var wb = ws.Workbook;
            //wb.History.Clear();
            //wb.History.IsEnabled = false;
            //bool isValid = isLoc && start
            ws.Workbook.BeginUpdate();


            ws.Columns[dic[TDKH.COL_KhoiLuongNhapVao]].Visible =
                        ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_KhoiLuongConLaiGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_GiaTriKeHoachGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_GiaTriThiCongGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_GiaTriBoSungGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_GiaTriConLaiGiaiDoan]].Visible =
                        ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc]].Visible =
                        ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc]].Visible =

                        ws.Columns[dic[TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong]].Visible =
                        ws.Columns[dic[TDKH.COL_GiaTriConLaiSoVoiHopDong]].Visible =
                        ws.Columns[dic[TDKH.COL_PhanTramGiaTriKyNay]].Visible =
                        ws.Columns[dic[TDKH.COL_PhanTramGiaTriLuyKeKyNay]].Visible = isLoc;
            //var dic = TDKH.dic

            if (isLoc)
            {
                WaitFormHelper.ShowWaitForm("Đang tính khối lượng kế hoạch giai đoạn");
                ws.Range["DateRange"].SetValue($"Tổng hợp từ {start.ToShortDateString()} đên ngày {end.ToShortDateString()}");
                var colTypeRow = ws.Columns[dic[TDKH.COL_TypeRow]];
                //CTac
                var cells = colTypeRow.Search(MyConstant.TYPEROW_CVCha, MyConstant.MySearchOptions);

                var dicCVCha = new Dictionary<string, int>();
                foreach (var cell in cells)
                {
                    string code = ws.Rows[cell.RowIndex][dic[TDKH.COL_Code]].Value.ToString();
                    dicCVCha.Add(code, cell.RowIndex);
                }

                if (dicCVCha.Any())
                {
                    var KLHNs = MyFunction.CalcKLHNBrief(TypeKLHN.CongTac, dicCVCha.Keys, start, end);
                    foreach (var item in dicCVCha)
                    {
                        var klhnsInCha = KLHNs.Where(x => x.ParentCode == item.Key).SingleOrDefault();
                        var klntsInCha = KLHNs.Where(x => x.ParentCode == item.Key).SingleOrDefault();
                        var klhnInCtac = klhnsInCha?.KLKHInRange;
                        var kltcInCtac = klhnsInCha?.KLTCInRange;
                        var klbsInCtac = klhnsInCha?.KLBSInRange;
                        var kllkInCtac = klhnsInCha?.LuyKeKLKHLastDate??0;
                        var kllkTCInCtac = klhnsInCha?.LuyKeKLTCLastDate ?? 0;
                        //var klclInCtac = klhnInCtac - kltcInCtac;

                        var gthnInCtac = klhnsInCha?.TTKHInRange;
                        var gttcInCtac = klhnsInCha?.TTTCInRange;
                        var gtbsInCtac = klhnsInCha?.TTBSInRange;
                        var gtlkInCtac = klhnsInCha?.LuyKeTTKHLastDate??0;
                        var gtlktcInCtac = klhnsInCha?.LuyKeTTTCLastDate ?? 0;
                        var gtclInCtac = gthnInCtac - gttcInCtac;


                        ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].SetValue(klhnInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]].SetValue(kllkTCInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]].SetValue(kltcInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc]].SetValue(klhnsInCha?.LuyKeKLTCKyTruoc);
                        //ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongConLaiGiaiDoan]].SetValue(klclInCtac);
                        
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriKeHoachGiaiDoan]].SetValue(gthnInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc]].SetValue(klhnsInCha?.LuyKeTTTCKyTruoc);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan]].SetValue(gtlktcInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriThiCongGiaiDoan]].SetValue(gttcInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriBoSungGiaiDoan]].SetValue(gtbsInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriConLaiGiaiDoan]].SetValue(gtclInCtac);
                    }
                }

                //Nhom
                //CTac
                var cellNhoms = colTypeRow.Search(MyConstant.TYPEROW_Nhom, MyConstant.MySearchOptions);

                var dicNhom = new Dictionary<string, int>();
                foreach (var cell in cellNhoms)
                {
                    string KLKH = ws.Rows[cell.RowIndex][dic[TDKH.COL_DBC_KhoiLuongToanBo]].Value.ToString();
                    if (!KLKH.HasValue())
                        continue;
                    string code = ws.Rows[cell.RowIndex][dic[TDKH.COL_Code]].Value.ToString();
                    dicNhom.Add(code, cell.RowIndex);
                }

                if (dicNhom.Any())
                {
                    var KLHNs = MyFunction.CalcKLHNBrief(TypeKLHN.Nhom, dicNhom.Keys, start, end);
                    foreach (var item in dicNhom)
                    {
                        var klhnsInCha = KLHNs.Where(x => x.ParentCode == item.Key).SingleOrDefault();
                        var klntsInCha = KLHNs.Where(x => x.ParentCode == item.Key).SingleOrDefault();
                        var klhnInCtac = klhnsInCha?.KLKHInRange;
                        var kltcInCtac = klhnsInCha?.KLTCInRange;
                        var klbsInCtac = klhnsInCha?.KLBSInRange;
                        var kllkInCtac = klhnsInCha?.LuyKeKLKHLastDate ?? 0;
                        var kllkTCInCtac = klhnsInCha?.LuyKeKLTCLastDate ?? 0;
                        //var klclInCtac = klhnInCtac - kltcInCtac;

                        var gthnInCtac = klhnsInCha?.TTKHInRange;
                        var gttcInCtac = klhnsInCha?.TTTCInRange;
                        var gtbsInCtac = klhnsInCha?.TTBSInRange;
                        var gtlkInCtac = klhnsInCha?.LuyKeTTKHLastDate ?? 0;
                        var gtlktcInCtac = klhnsInCha?.LuyKeTTTCLastDate ?? 0;
                        var gtclInCtac = gthnInCtac - gttcInCtac;


                        ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].SetValue(klhnInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]].SetValue(kllkTCInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]].SetValue(kltcInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc]].SetValue(klhnsInCha?.LuyKeKLTCKyTruoc);
                        //ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongConLaiGiaiDoan]].SetValue(klclInCtac);

                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriKeHoachGiaiDoan]].SetValue(gthnInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan]].SetValue(gtlktcInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc]].SetValue(klhnsInCha?.LuyKeTTTCKyTruoc);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriThiCongGiaiDoan]].SetValue(gttcInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriBoSungGiaiDoan]].SetValue(gtbsInCtac);
                        ws.Rows[item.Value][dic[TDKH.COL_GiaTriConLaiGiaiDoan]].SetValue(gtclInCtac);
                    }
                }
                WaitFormHelper.CloseWaitForm();

            }
                ws.Workbook.EndUpdate();
            FormMainHelper.LoadInputRePlan();
            //ws.Calculate();
            //ws.Range["TBT_Value"].AutoFitColumns();


        }

        private static void CapNhatMacDinhKhoiLuongToanBo(CellRange range, Dictionary<string, string> dic)
        {
            var ws = range.Worksheet;
            string headingKLTB = dic[TDKH.COL_DBC_KhoiLuongToanBo];
            string headingKLTBDefault = dic[TDKH.COL_KhoiLuongToanBo_Iscongthucmacdinh];
            string headingKL1BP = dic[TDKH.COL_DBC_KL1BoPhan];
            string headingTypeRow = dic[TDKH.COL_TypeRow];

            var indsCha = ws.Columns[headingTypeRow].Search(MyConstant.TYPEROW_CVCha, MyConstant.MySearchOptions).Select(x => x.RowIndex);
            int indRowCha = ws.Range.GetColumnIndexByName(dic[TDKH.COL_RowCha]);

            List<string> dbUpdates = new List<string>();
            foreach (int indCha in indsCha)
            {
                Row crRowCha = ws.Rows[indCha];
                var crKLTB = crRowCha[headingKLTB].Value.NumericValue;

                if (!bool.TryParse(crRowCha[headingKLTBDefault].Value.ToString(), out bool isDefault) || isDefault)
                {
                    int LastChildInd = SpreadsheetHelper.FindNextGreaterTypeInd(range, indCha, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha]) - 1;
                    crRowCha[headingKLTBDefault].SetValue(true);
                    crRowCha[headingKLTB].Formula = $"SUM({headingKL1BP}{indCha + 1}:{headingKL1BP}{LastChildInd + 1})";

                    //dbUpdates.
                }
            }
        }


        public static string GetDbStringFullInfoVatTuWithHaoPhi(DoBocVatTu type, string codeCT, string codeHM, DonViThucHien crDVTH,
            DateTime? start = null, DateTime? end = null, bool ignoreKLKH = false, bool isGetNhapKho = false, bool isGetKLTCCongTac = false)    
        {

            string condNhaThau = "";
            if (crDVTH.ColCodeFK == "CodeNhaThau")
                condNhaThau = $"OR COALESCE(hp.CodeNhaThau, hp.CodeNhaThauPhu, hp.CodeToDoi) IS NULL";
            
            //TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);

            string dbString =
                $"WITH TempTC AS\r\n" +
                $"(" +
                $"SELECT ct.Code,\r\n" +
                $"TOTAL(klhn.KhoiLuongThiCong) AS KhoiLuongDaThiCong,\r\n" +
                    $"MAX(klhn.Ngay) AS NgayKetThucThiCong,\r\n" +
                    $"MIN(klhn.Ngay) AS NgayBatDauThiCong\r\n" +
                    $"FROM {Server.Tbl_TDKH_KHVT_VatTu} ct\r\n" +
                    $"JOIN {MyConstant.view_VatTuKeHoachThiCong} cttc\r\n" +
                    $"ON ct.CodeNhaThau IS NOT NULL AND ct.Code = cttc.CodeGiaoThau\r\n" +
                    $"JOIN {Server.Tbl_TDKH_KHVT_KhoiLuongHangNgay} klhn\r\n" +
                    $"ON cttc.Code = klhn.CodeVatTu AND KhoiLuongThiCong IS NOT NULL\r\n" +
                    $"GROUP BY ct.Code\r\n" +
                    $"UNION ALL\r\n" +
                    $"SELECT ct.Code,\r\n" +
                $"TOTAL(klhn.KhoiLuongThiCong) AS KhoiLuongDaThiCong,\r\n" +
                    $"MAX(klhn.Ngay) AS NgayKetThucThiCong,\r\n" +
                    $"MIN(klhn.Ngay) AS NgayBatDauThiCong\r\n" +
                    $"FROM {Server.Tbl_TDKH_KHVT_VatTu} ct\r\n" +
                    $"JOIN {Server.Tbl_TDKH_KHVT_KhoiLuongHangNgay} klhn\r\n" +
                    $"ON ct.CodeNhaThau IS NULL AND ct.Code = klhn.CodeVatTu AND KhoiLuongThiCong IS NOT NULL\r\n" +
                    $"GROUP BY ct.Code\r\n" +
                    $"" +
                $")\r\n" +




                    $"SELECT hp.*, hm.Code AS CodeHangMuc, hm.Ten AS TenHangMuc, mtc.Ten AS MayQuyDoi, \r\n" +
                     $" klhn.KhoiLuongDaThiCong, klhn.KhoiLuongDaThiCong*hp.DonGiaThiCong AS ThanhTienDaThiCong, klhn.NgayBatDauThiCong, klhn.NgayKetThucThiCong,\r\n" +
                $"ctrinh.Code AS CodeCongTrinh, ctrinh.Ten AS TenCongTrinh, pt.Ten AS TenPhanTuyen, mui.Ten AS TenMuiThiCong,\r\n" +
                $"hp.MaVatLieu || ';' || hp.MaTXHienTruong || ';' || hp.VatTu || ';' || hp.DonVi || ';' || hp.DonGia || ';' || hp.LoaiVatTu || ';' || hp.CodeHangMuc || ';' || mui.Code || ';' || pt.Code AS SearchString\r\n" +
                ((isGetKLTCCongTac) ? $", TOTAL(hnct.KhoiLuongThiCong) AS KLTCTheoCTac\r\n" : "") +


                    $"FROM {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                    $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                    $"ON hm.CodeCongTrinh = ctrinh.Code\r\n" +
                    ((codeCT.HasValue()) ? $"AND ctrinh.Code = '{codeCT}'\r\n" : "") +
                    ((codeHM.HasValue()) ? $"AND hm.Code = '{codeHM}'\r\n" : "") +
                    $"LEFT JOIN {MyConstant.view_HaoPhiWithVatTu} hp\r\n" +
                    $"ON hp.CodeHangMuc = hm.Code\r\n" +
                    $"AND (hp.{crDVTH.ColCodeFK} = '{crDVTH.Code}' {condNhaThau})\r\n" +
                    $"LEFT JOIN {TDKH.Tbl_PhanTuyen} pt\r\n" +
                    $"ON hp.CodePhanTuyen = pt.Code\r\n" +
                    $"LEFT JOIN {Server.Tbl_MTC_DanhSachMay} mtc\r\n" +
                    $"ON hp.CodeMay = mtc.Code\r\n" +

                    $"LEFT JOIN {Server.Tbl_TDKH_MuiThiCong} mui\r\n" +
                    $"ON hp.CodeMuiThiCong = mui.Code\r\n" +

                    

                    ((isGetKLTCCongTac)
                    ?$"LEFT JOIN {MyConstant.view_CongTacKeHoachThiCong} cttk\r\n" +
                    $"ON cttk.CodeGiaoThau = hp.CodeCongTac\r\n" +
                    $"LEFT JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} hnct\r\n" +
                    $"ON hnct.CodeCongTacTheoGiaiDoan = cttk.Code AND hnct.KhoiLuongThiCong IS NOT NULL\r\n":"") +
                    
                    $"LEFT JOIN TempTC klhn ON klhn.Code = hp.CodeVatTu\r\n" +

                    $"WHERE hp.PhanTichKeHoach = 1 AND IsCha = 0\r\n" +
                    ((type != DoBocVatTu.AllVatTu)
                    ? $"AND hp.LoaiVatTu = '{type.GetEnumDisplayName()}' "
                    : "") +
                    $"GROUP BY COALESCE(hp.Code, '')\r\n" +
                    $"ORDER BY ctrinh.SortId, hm.SortId, hp.LoaiVatTu DESC,pt.SortId, pt.CreatedOn, hp.VatTu ASC";

            return dbString;
        }

        public static string GetDbStringFullInfoVatTuBrief(DoBocVatTu type, string codeCT, string codeHM, DonViThucHien crDVTH,
            DateTime? start = null, DateTime? end = null, bool ignoreKLKH = false, bool isGetNhapKho = false, string IsPhanTich = null)
        {


            string condNhaThau = "";
            if (crDVTH.ColCodeFK == "CodeNhaThau")
                condNhaThau = $"OR COALESCE(hp.CodeNhaThau, hp.CodeNhaThauPhu, hp.CodeToDoi) IS NULL";


            string dbString =
                $"WITH TempTC AS\r\n" +
                $"(" +
                $"SELECT ct.Code,\r\n" +
                $"TOTAL(klhn.KhoiLuongThiCong) AS KhoiLuongDaThiCong,\r\n" +
                    $"MAX(klhn.Ngay) AS NgayKetThucThiCong,\r\n" +
                    $"MIN(klhn.Ngay) AS NgayBatDauThiCong\r\n" +
                    $"FROM {Server.Tbl_TDKH_KHVT_VatTu} ct\r\n" +
                    $"JOIN {MyConstant.view_VatTuKeHoachThiCong} cttc\r\n" +
                    $"ON ct.CodeNhaThau IS NOT NULL AND ct.Code = cttc.CodeGiaoThau\r\n" +
                    $"JOIN {Server.Tbl_TDKH_KHVT_KhoiLuongHangNgay} klhn\r\n" +
                    $"ON cttc.Code = klhn.CodeVatTu AND KhoiLuongThiCong IS NOT NULL\r\n" +
                    $"GROUP BY ct.Code\r\n" +
                    $"UNION ALL\r\n" +
                    $"SELECT ct.Code,\r\n" +
                $"TOTAL(klhn.KhoiLuongThiCong) AS KhoiLuongDaThiCong,\r\n" +
                    $"MAX(klhn.Ngay) AS NgayKetThucThiCong,\r\n" +
                    $"MIN(klhn.Ngay) AS NgayBatDauThiCong\r\n" +
                    $"FROM {Server.Tbl_TDKH_KHVT_VatTu} ct\r\n" +
                    $"JOIN {Server.Tbl_TDKH_KHVT_KhoiLuongHangNgay} klhn\r\n" +
                    $"ON ct.CodeNhaThau IS NULL AND ct.Code = klhn.CodeVatTu AND KhoiLuongThiCong IS NOT NULL\r\n" +
                    $"GROUP BY ct.Code\r\n" +
                    $"" +
                $")\r\n" +
                    $"SELECT hp.*, hm.Code AS CodeHangMuc, hm.Ten AS TenHangMuc, mtc.Ten AS MayQuyDoi, \r\n" +
                    $" klhn.KhoiLuongDaThiCong, klhn.KhoiLuongDaThiCong*hp.DonGiaThiCong AS ThanhTienDaThiCong, klhn.NgayBatDauThiCong, klhn.NgayKetThucThiCong,\r\n" +
                $"ctrinh.Code AS CodeCongTrinh, ctrinh.Ten AS TenCongTrinh, pt.Ten AS TenPhanTuyen, mui.Ten AS TenMuiThiCong,\r\n" +
                $"hp.MaVatLieu || ';' || hp.MaTXHienTruong || ';' || hp.VatTu || ';' || hp.DonVi || ';' || hp.DonGia || ';' || hp.LoaiVatTu || ';' || hp.CodeHangMuc  || ';' || mui.Code || ';' || pt.Code AS SearchString\r\n" +


                    $"FROM {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                    $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                    $"ON hm.CodeCongTrinh = ctrinh.Code\r\n" +
                    ((codeCT.HasValue()) ? $"AND ctrinh.Code = '{codeCT}'\r\n" : "") +
                    ((codeHM.HasValue()) ? $"AND hm.Code = '{codeHM}'\r\n" : "") +
                    $"LEFT JOIN {MyConstant.view_VatTuKeHoachThiCong} hp\r\n" +
                    $"ON hp.CodeHangMuc = hm.Code\r\n" +
                    $"AND (hp.{crDVTH.ColCodeFK} = '{crDVTH.Code}' {condNhaThau})\r\n" +
                    $"LEFT JOIN {TDKH.Tbl_PhanTuyen} pt\r\n" +
                    $"ON hp.CodePhanTuyen = pt.Code\r\n" +
                    $"LEFT JOIN {Server.Tbl_MTC_DanhSachMay} mtc\r\n" +
                    $"ON hp.CodeMay = mtc.Code\r\n" +
                    $"LEFT JOIN {Server.Tbl_TDKH_MuiThiCong} mui\r\n" +
                    $"ON hp.CodeMuiThiCong = mui.Code\r\n" +
                    $"LEFT JOIN TempTC klhn ON klhn.Code = hp.Code\r\n" +
                   
                    ((type != DoBocVatTu.AllVatTu)
                    ? $"WHERE hp.LoaiVatTu = '{type.GetEnumDisplayName()}' AND ctrinh.CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}' {IsPhanTich} "
                    : "") +
                    $"GROUP BY COALESCE(hp.Code, '')\r\n" +
                    $"ORDER BY ctrinh.SortId, hm.SortId, hp.LoaiVatTu DESC,pt.SortId, pt.CreatedOn, hp.VatTu ASC";

            return dbString;
        }

        public static DataTable GetFullInfoVatTuWithHaoPhiDataTable(DoBocVatTu type, string codeCT, string codeHM, DonViThucHien crDVTH, DateTime? start = null, DateTime? end = null, bool isGetNhapKho = false, bool isGetKLTCCongTac = false)
        {
            string dbString = GetDbStringFullInfoVatTuWithHaoPhi(type, codeCT, codeHM, crDVTH, start, end, isGetNhapKho, isGetKLTCCongTac);

            DataTable vatTus = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


            return vatTus;
        }
        public static DataTable GetFullInfoVatTuBriefDataTable(DoBocVatTu type, string codeCT, string codeHM, DonViThucHien crDVTH,
                        DateTime? start = null, DateTime? end = null, bool ignoreKLKH = false, bool isGetNhapKho = false,string IsPhanTich=null)
        {
            //klhnHPhis = new List<KLHN>();
            string dbString = GetDbStringFullInfoVatTuBrief(type, codeCT, codeHM,crDVTH, start, end, ignoreKLKH, isGetNhapKho,IsPhanTich: IsPhanTich);

            DataTable vatTus = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var _codeVTus = vatTus.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Select(x => (string)x["Code"]).Distinct().ToList();
            //var _codeHPhis = vatTus.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Select(x => (string)x["Code"]).Distinct().ToList();

            //klhnVTus = MyFunction.Fcn_CalKLKHNew(TypeKLHN.VatLieu, _codeVTus);
            //klhnHPhis = MyFunction.Fcn_CalKLKHNew(TypeKLHN.HaoPhiVatTu, _codeHPhis, ignoreKLKH: ignoreKLKH);

            return vatTus;
        }



        public static void LoadVatTuBrief(DoBocVatTu type)
        {
            int indTypeSheet = (int)type;
            if (SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH is null)
            {
                SharedControls.xtraTabControl_TienDoKeHoach.Enabled = false;
                goto RETURN;
            }

            SharedControls.xtraTabControl_TienDoKeHoach.Enabled = true;

            string description = "Đang tải vật tư";

            WaitFormHelper.ShowWaitForm(description, "Đang tải công tác");

            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
  

            wb.BeginUpdate();

            Worksheet ws = wb.Worksheets[TDKH.sheetsName[indTypeSheet]];
            ws.ScrollTo(0, 0);

            ws.Range["DateRange"].SetValue(null);
            DefinedName definedName = wb.DefinedNames.GetDefinedName(TDKH.rangesNameData[indTypeSheet]);
            //ws.Comments.Clear();
            CellRange range = definedName.Range;

            if (range.RowCount > 2)
            {
                ws.Rows.Remove(range.TopRowIndex + 1, range.RowCount - 2);
            }
            definedName.Range.ForEach<Cell>(x => x.SetValue(""));


            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            if (!BaseFrom.allPermission.HaveInitProjectPermission)
            {
                ws.Columns[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Visible = false;
                ws.Columns[dic[TDKH.COL_KhoiLuongHopDongChiTiet]].Visible = false;
                ws.Columns[dic[TDKH.COL_DonGia]].Visible = false;
            }

            string prefixFormula = MyConstant.PrefixFormula;
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);

            var lsHaoPhi = GetFullInfoVatTuBriefDataTable(type, codeCT, codeHM,SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH, ignoreKLKH: true, isGetNhapKho: false,IsPhanTich: "AND hp.PhanTichKeHoach=true");

            var codesHp = lsHaoPhi.AsEnumerable().Select(x => x["Code"].ToString()).Distinct();
            //string[] codesVT = lsHaoPhi.AsEnumerable().Select(x => x["Code"].ToString()).Distinct();

            string dbString = $@"
SELECT vvt.Code AS ParentCode, 
TOTAL(hp.HeSoNguoiDung*hp.DinhMucNguoiDung*kl.KhoiLuongThiCong) AS KhoiLuongThiCong
FROM {Server.Tbl_TDKH_KHVT_VatTu} vvt
JOIN {MyConstant.view_VatTuKeHoachThiCong} vttc
ON vvt.CodeNhaThau IS NOT NULL AND vvt.Code = vttc.CodeGiaoThau
JOIN {Server.Tbl_TDKH_HaoPhiVatTu} hp
ON vttc.Code = hp.CodeVatTu
JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} kl
ON hp.CodeCongTac = kl.CodeCongTacTheoGiaiDoan AND kl.KhoiLuongThiCong > 0
WHERE vvt.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHp)})
GROUP BY vvt.Code 

UNION ALL
SELECT vvt.Code AS ParentCode, 
TOTAL(hp.HeSoNguoiDung*hp.DinhMucNguoiDung*kl.KhoiLuongThiCong) AS KhoiLuongThiCong
FROM {Server.Tbl_TDKH_KHVT_VatTu} vvt
JOIN {Server.Tbl_TDKH_HaoPhiVatTu} hp
ON vvt.CodeNhaThau IS NULL AND vvt.Code = hp.CodeVatTu
JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} kl
ON hp.CodeCongTac = kl.CodeCongTacTheoGiaiDoan AND kl.KhoiLuongThiCong > 0
WHERE vvt.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHp)})
GROUP BY vvt.Code 
";

            var klsHpByCTac = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(dbString);

            var grsCTrinh = lsHaoPhi.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);

            DataTable dtData = DataTableCreateHelper.TienDoVatTu();

            int crRowInd = definedName.Range.TopRowIndex;
            int rowTongInd = crRowInd;

            int count = 0;
            int STTCTrinh = 0;
            string forGiaTri, forNBD, forNKT, forNBDTC, forNKTTC;
            int fstInd = definedName.Range.TopRowIndex;
            string[] colsSum = new string[]
{
                TDKH.COL_GiaTri,
                TDKH.COL_GiaTriThiCong,
                TDKH.COL_KinhPhiDuKien
};
//                        string dbString = $@"SELECT COALESCE(vt.CodeGiaoThau, vt.Code) AS Code, TOTAL(vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongThiCong) AS KhoiLuongThiCong, klhn.Ngay
//FROM {MyConstant.view_VatTuKeHoachThiCong} vt
//JOIN {MyConstant.view_HaoPhiVatTu} vvt
//ON vt.Code = vvt.CodeVatTu
//JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk
//ON vvt.CodeCongTac = cttk.Code
//LEFT JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn
//ON cttk.Code = klhn.CodeCongTacTheoGiaiDoan AND klhn.KhoiLuongThiCong IS NOT NULL
//WHERE COALESCE(vt.CodeGiaoThau, vt.Code) IN ({MyFunction.fcn_Array2listQueryCondition(codesHp)})
//GROUP BY COALESCE(vt.CodeGiaoThau, vt.Code)";

//            var klsHpByCTac = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(dbString);


            foreach (var grCTrinh in grsCTrinh)
            {

                DataRow newRowCtrinh = dtData.NewRow();
                dtData.Rows.Add(newRowCtrinh);

                var crRowCtrInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;


                DataRow fstCtr = grCTrinh.First();
                newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                newRowCtrinh[TDKH.COL_Code] = grCTrinh.Key;
                newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                newRowCtrinh[TDKH.COL_DanhMucCongTac] = fstCtr["TenCongTrinh"];
                newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{rowTongInd + 1})";

                var grsMuiTC = grCTrinh.GroupBy(x => x["CodeMuiThiCong"].ToString())
                                    .OrderBy(x => x.Key.HasValue());

                foreach (var grMuiTC in grsMuiTC)
                {
                    DataRow fstMuiTC = grMuiTC.First();
                    int? crRowMuiTCInd = null;
                    bool isMTC = grMuiTC.Key.HasValue();
                    DataRow newRowMuiTC = null;
                    if (isMTC)
                    {
                        newRowMuiTC = dtData.NewRow();
                        dtData.Rows.Add(newRowMuiTC);

                        crRowMuiTCInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                        newRowMuiTC[TDKH.COL_Code] = grMuiTC.Key;
                        newRowMuiTC[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_MuiThiCong;
                        newRowMuiTC[TDKH.COL_DanhMucCongTac] = fstMuiTC["TenMuiThiCong"];
                        newRowMuiTC[TDKH.COL_TypeRow] = MyConstant.TYPEROW_MuiThiCong;
                        newRowMuiTC[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd + 1})";
                    }


                    var grsHM = grMuiTC.GroupBy(x => x["CodeHangMuc"]);
                    int STTHM = 0;
                    foreach (var grHM in grsHM)
                    {
                        DataRow newRowHM = dtData.NewRow();
                        dtData.Rows.Add(newRowHM);

                        var crRowHMInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;


                        DataRow fstHM = grHM.First();
                        newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                        newRowHM[TDKH.COL_Code] = grHM.Key;
                        newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                        newRowHM[TDKH.COL_DanhMucCongTac] = fstHM["TenHangMuc"];
                        newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                        newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowMuiTCInd ?? crRowCtrInd) + 1})";

                        var grsPhanTuyen = grHM.GroupBy(x => x["CodePhanTuyen"].ToString())
                                .OrderByDescending(x => x.Key.HasValue());

                        foreach (var grPhanTuyen in grsPhanTuyen)
                        {
                            DataRow fstPT = grPhanTuyen.First();
                            int? crRowPTInd = null;
                            DataRow newRowPT = null;

                            if (grPhanTuyen.Key.HasValue())
                            {
                                newRowPT = dtData.NewRow();
                                dtData.Rows.Add(newRowPT);

                                crRowPTInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                newRowPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                                newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
                                newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                                newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";
                            }

                            //var grsVatTu = grPhanTuyen.GroupBy(x => x["CodeVatTu"]);

                            int STTVatTu = 0;
                            foreach (var grVatTu in grPhanTuyen)
                            {
                                DataRow newRowVT = dtData.NewRow();
                                dtData.Rows.Add(newRowVT);

                                int crRowVtInd = crRowInd = fstInd + dtData.Rows.Count;
                                var fstVT = grVatTu;

                                if (fstVT["Code"] == DBNull.Value && fstVT["CodeNhanThau"] == DBNull.Value)
                                    continue;

                                //var klhnsInVatTu = klhnsVtu.Where(x => x.ParentCode == fstVT["Code"].ToString());

                                newRowVT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{++STTVatTu}";
                                newRowVT[TDKH.COL_Code] = fstVT["Code"];
                                newRowVT[TDKH.COL_MaHieuCongTac] = fstVT["MaVatLieu"];
                                newRowVT[TDKH.COL_DanhMucCongTac] = fstVT["VatTu"];
                                newRowVT[TDKH.COL_DonVi] = fstVT["DonVi"];
                                newRowVT[TDKH.COL_DonGia] = fstVT["DonGia"];
                                newRowVT[TDKH.COL_DonGiaThiCong] = fstVT["DonGiaThiCong"];
                                newRowVT[TDKH.COL_DBC_KhoiLuongToanBo] = fstVT["KhoiLuongKeHoach"];
                                newRowVT[TDKH.COL_KhoiLuongHopDongChiTiet] = fstVT["KhoiLuongHopDong"];
                                newRowVT[TDKH.COL_DonGiaThiCong] = fstVT["DonGiaThiCong"];
                                newRowVT[TDKH.COL_MayQuyDoi] = fstVT["MayQuyDoi"];
                                //newRowVTDics[TDKH.COL_DonGiaThiCong]].SetValue(fstVT.DonGiaThiCong);
                                newRowVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                newRowVT[TDKH.COL_KHVT_Search] = fstVT["SearchString"];
                                newRowVT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{fstVT["NgayBatDau"]}"; // fstVT["NgayBatDau"];
                                newRowVT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{fstVT["NgayKetThuc"]}"; //fstVT["NgayKetThuc"];
                                newRowVT[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                newRowVT[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                
                                newRowVT[TDKH.COL_GiaTriKeHoachGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}";
                                newRowVT[TDKH.COL_GiaTriThiCongGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                newRowVT[TDKH.COL_GiaTriThiCongLuyKeDenKyTruocTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                newRowVT[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                
                                //newRowVT[TDKH.COL_GiaTriKeHoachGiaiDoanCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}";
                                //newRowVT[TDKH.COL_GiaTriThiCongGiaiDoanCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongGiaiDoanCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                //newRowVT[TDKH.COL_GiaTriThiCongLuyKeDenKyTruocCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                //newRowVT[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoanCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                
                                
                                
                                
                                
                                newRowVT[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";

                                //var datesThiCong = klhnsInVatTu.Where(x => x.KhoiLuongThiCong > 0).Select(x => x.Ngay);

                                newRowVT[TDKH.COL_NgayBatDauThiCong] = fstVT["NgayBatDauThiCong"];
                                newRowVT[TDKH.COL_NgayKetThucThiCong] = fstVT["NgayKetThucThiCong"];
                                newRowVT[TDKH.COL_GiaTriThiCong] = fstVT["ThanhTienDaThiCong"];
                                newRowVT[TDKH.COL_KhoiLuongDaThiCong] = fstVT["KhoiLuongDaThiCong"];

               
                                newRowVT[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = klsHpByCTac.SingleOrDefault(x => x.ParentCode == fstVT["Code"].ToString())?.KhoiLuongThiCong ?? 0;


                                newRowVT[TDKH.COL_KHVT_MaTXHientruong] = fstVT["MaTXHienTruong"];

                                newRowVT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd) + 1})";

                                newRowVT[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1};0)";
                                newRowVT[TDKH.COL_KinhPhiDuKien] =
                                $"{prefixFormula}ROUND({dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{TDKH.rangesTongKinhPhiVatTu[indTypeSheet]}*{TDKH.rangesKinhPhiDuKienVatTu[indTypeSheet]};0)";

                                //foreach (var haoPhi in grVatTu)
                                //{
                                //    WaitFormHelper.ShowWaitForm($"{count++}: {haoPhi["VatTu"]}", "Đang tải vật liệu");

                                //    DataRow newRowHp = dtData.NewRow();
                                //    dtData.Rows.Add(newRowHp);
                                //    ++crRowInd;

                                //    var klhnsInHaoPhi = klhnsHPhi.Where(x => x.CodeCha == haoPhi["Code"].ToString());

                                //    newRowHp[TDKH.COL_MaHieuCongTac] = haoPhi["MaHieuCongTac"];
                                //    newRowHp[TDKH.COL_DanhMucCongTac] = haoPhi["TenCongTac"];
                                //    newRowHp[TDKH.COL_Code] = haoPhi["Code"];
                                //    newRowHp[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowVtInd + 1})";
                                //    newRowHp[TDKH.COL_DonGiaThiCong] = haoPhi["DonGiaThiCong"];
                                //    newRowHp[TDKH.COL_DonGia] = haoPhi["DonGia"];
                                //    newRowHp[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCON;
                                //    newRowHp[TDKH.COL_NgayBatDau] = haoPhi["NgayBatDau"];
                                //    newRowHp[TDKH.COL_NgayKetThuc] = haoPhi["NgayKetThuc"];
                                //    newRowHp[TDKH.COL_DBC_KhoiLuongToanBo] = haoPhi["KhoiLuongKeHoach"];
                                //    newRowHp[TDKH.COL_KhoiLuongHopDongChiTiet] = haoPhi["KhoiLuongHopDong"];
                                //    newRowHp[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = klsHpByCTac.Where(x => x.Code == newRowHp["Code"].ToString()).SingleOrDefault()?.KhoiLuongThiCong ?? 0;

                                //    var thicongs = klhnsInHaoPhi.Where(x => x.KhoiLuongThiCong > 0).Select(x => x.Ngay);

                                //    if (thicongs.Any())
                                //    {
                                //        newRowHp[TDKH.COL_NgayBatDauThiCong] = thicongs.Min();
                                //        newRowHp[TDKH.COL_NgayKetThucThiCong] = thicongs.Max();
                                //    }
                                //    newRowHp[TDKH.COL_KhoiLuongDaThiCong] = (klhnsInHaoPhi.Sum(x => x.KhoiLuongThiCong));
                                //    newRowHp[TDKH.COL_GiaTriThiCong] = (klhnsInHaoPhi.Sum(x => x.ThanhTienThiCong) ?? 0);


                                //    newRowHp[TDKH.COL_KHVT_Search] = haoPhi["Code"];

                                //    newRowHp[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1};0)";
                                //    newRowHp[TDKH.COL_KinhPhiDuKien] =
                                //    $"{prefixFormula}ROUND({dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{TDKH.rangesTongKinhPhiVatTu[indTypeSheet]}*{TDKH.rangesKinhPhiDuKienVatTu[indTypeSheet]};0)";

                                //    newRowHp[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                //    newRowHp[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                //    //crRowWs[Dics[TDKH.COL_DBC_KhoiLuongToanBo]].Formula = $"{Dics[TDKH.COL_NgayKetThucThiCong]}{crRowsInd + 1} - {Dics[TDKH.COL_NgayBatDauThiCong]}{crRowsInd + 1} + 1";
                                //    //crRowWs[Dics[TDKH.COL_SoNgayThiCong]].Formula = $"{Dics[TDKH.COL_NgayKetThucThiCong]}{crRowsInd + 1} - {Dics[TDKH.COL_NgayBatDauThiCong]}{crRowsInd + 1} + 1";

                                //    //DinhMucHelper.fcn_TDKH_CapNhatRangeNgayVatLieu(TypeKLHN.HaoPhiVatTu, haoPhi.Code, crRowWs);
                                //}
                            }

                            if (grPhanTuyen.Key.HasValue())
                            {
                                DataRow newRowHTPT = dtData.NewRow();
                                dtData.Rows.Add(newRowHTPT);

                                ++crRowInd;

                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                newRowHTPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                newRowHTPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;
                                newRowHTPT[TDKH.COL_DanhMucCongTac] = $"{prefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                                newRowHTPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                newRowHTPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";

                                crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                                GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                                newRowPT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                                newRowPT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                                newRowPT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                                newRowPT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                                foreach (string col in colsSum)
                                {
                                    forGiaTri = GetFormulaSumChild((crRowPTInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                    newRowPT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                }
                            }
                        }

                        crRowInd = fstInd + dtData.Rows.Count;
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowHM[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowHM[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowHM[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowHM[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = GetFormulaSumChild(crRowHMInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }
                    }

                    if (isMTC)
                    {
                        crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowMuiTC[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowMuiTC[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowMuiTC[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowMuiTC[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = GetFormulaSumChild((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowMuiTC[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }

                    }


                }

                crRowInd = fstInd + dtData.Rows.Count;
                GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                newRowCtrinh[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                newRowCtrinh[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                newRowCtrinh[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                newRowCtrinh[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";


                foreach (string col in colsSum)
                {
                    forGiaTri = GetFormulaSumChild(crRowCtrInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                    newRowCtrinh[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                }

            }

            var rowTong = dtData.NewRow();
            dtData.Rows.InsertAt(rowTong, 0);


            rowTong[TDKH.COL_DanhMucCongTac] = "TỔNG";
            crRowInd = fstInd - 1 + dtData.Rows.Count;
            GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
            GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

            rowTong[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
            rowTong[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
            rowTong[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
            rowTong[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";
            rowTong[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDau]}{fstInd + 1} + 1";
            rowTong[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{fstInd + 1} + 1";

            foreach (string col in colsSum)
            {
                forGiaTri = GetFormulaSumChild(fstInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                rowTong[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
            }
            //var forKPDK = GetFormulaSumChild(crRowInd + 1, dic[TDKH.COL_KinhPhiDuKien], dic[TDKH.COL_RowCha]);

            int numRow = dtData.Rows.Count;
            ws.Rows.Insert(definedName.Range.BottomRowIndex, numRow + 5, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, rowTongInd, 0);
            ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;


            SpreadsheetHelper.ReplaceAllFormulaAfterImport(definedName.Range);
            SpreadsheetHelper.FormatRowsInRange(definedName.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha],
                dic[TDKH.COL_Code], colMaVatTu: dic[TDKH.COL_MaHieuCongTac]);

            //DinhMucHelper.fcn_TDKH_CapNhatRowChaConTienDoKeHoach(ws.Rows[rowTongInd]);
            //CheckAnHienCongTacVatTu(ws, SharedControls.cb_TDKH_HienCongTac.Checked);
            CheckAnHienPhanDoan();
            wb.EndUpdate();
            LoadGiaiDoanVatTu(ws);
            RETURN:
            WaitFormHelper.CloseWaitForm();

        }

        public static void LoadVatTuFull(DoBocVatTu type)
        {
            int indTypeSheet = (int)type;
            if (SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH is null)
            {
                SharedControls.xtraTabControl_TienDoKeHoach.Enabled = false;
                goto RETURN;
            }

            SharedControls.xtraTabControl_TienDoKeHoach.Enabled = true;

            string description = "Đang tải vật tư";

            WaitFormHelper.ShowWaitForm(description, "Đang tải công tác");
            //FileHelper.fcn_spSheetStreamDocument(SharedControls.spsheet_TD_KH_LapKeHoach, $@"{BaseFrom.m_templatePath}\FileExcel\6.aBangNhapKeHachDanhGiaLoiNhuan.xlsx");
            ////          //SharedControls.spsheet_TD_KH_LapKeHoach.Document.History.IsEnabled = false;

            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            wb.BeginUpdate();
            string[] sheetsName =
            {
                TDKH.SheetName_DoBocChuan,
                TDKH.SheetName_KeHoachKinhPhi,
                TDKH.SheetName_VatLieu,
                TDKH.SheetName_NhanCong,
                TDKH.SheetName_MayThiCong
            };

            string[] rangesName =
            {
                TDKH.RANGE_DoBocChuan,
                TDKH.RANGE_KeHoach,
                TDKH.RANGE_KeHoachVatTu_VL_TuDong,
                TDKH.RANGE_KeHoachVatTu_NC_TuDong,
                TDKH.RANGE_KeHoachVatTu_MTC_TuDong
            };


            Worksheet ws = wb.Worksheets[sheetsName[indTypeSheet]];
            ws.ScrollTo(0, 0);

            ws.Range["DateRange"].SetValue(null);
            DefinedName definedName = wb.DefinedNames.GetDefinedName(rangesName[indTypeSheet]);
            //ws.Comments.Clear();
            CellRange range = definedName.Range;

            if (range.RowCount > 2)
            {
                ws.Rows.Remove(range.TopRowIndex + 1, range.RowCount - 2);
            }
            definedName.Range.ForEach<Cell>(x => x.SetValue(""));



            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            if (!BaseFrom.allPermission.HaveInitProjectPermission)
            {
                ws.Columns[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Visible = false;
                ws.Columns[dic[TDKH.COL_KhoiLuongHopDongChiTiet]].Visible = false;
                ws.Columns[dic[TDKH.COL_DonGia]].Visible = false;
            }

            string prefixFormula = MyConstant.PrefixFormula;
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);

            var lsHaoPhi = GetFullInfoVatTuWithHaoPhiDataTable(type, codeCT, codeHM,SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH, isGetNhapKho: false);

            var codesHp = lsHaoPhi.AsEnumerable().Select(x => x["Code"].ToString()).Distinct();
            //string[] codesVT = lsHaoPhi.AsEnumerable().Select(x => x["Code"].ToString()).Distinct();

            string dbString = $@"SELECT vvt.Code AS ParentCode, TOTAL(vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongThiCong) AS KhoiLuongThiCong, klhn.Ngay
FROM {MyConstant.view_HaoPhiVatTu} vvt
JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} ct
ON vvt.CodeCongTac = ct.Code
JOIN {MyConstant.view_CongTacKeHoachThiCong} cttc
ON ct.CodeNhaThau IS NOT NULL AND ct.Code = cttc.CodeGiaoThau
JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn
ON cttc.Code = klhn.CodeCongTacTheoGiaiDoan AND klhn.KhoiLuongThiCong IS NOT NULL
WHERE vvt.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHp)})
GROUP BY vvt.Code
UNION ALL
SELECT vvt.Code AS ParentCode, TOTAL(vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongThiCong) AS KhoiLuongThiCong, klhn.Ngay
FROM {MyConstant.view_HaoPhiVatTu} vvt
JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} ct
ON vvt.CodeCongTac = ct.Code
JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn
ON ct.CodeNhaThau IS NULL AND ct.Code = klhn.CodeCongTacTheoGiaiDoan AND klhn.KhoiLuongThiCong IS NOT NULL
WHERE vvt.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHp)})
GROUP BY vvt.Code
";

            var klsHpALLByCTac = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(dbString);

            var grsCTrinh = lsHaoPhi.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);

            DataTable dtData = DataTableCreateHelper.TienDoVatTu();

            int crRowInd = definedName.Range.TopRowIndex;
            int rowTongInd = crRowInd;

            int count = 0;
            int STTCTrinh = 0;

            string forGiaTri, forNBD, forNKT, forNBDTC, forNKTTC;
            int fstInd = definedName.Range.TopRowIndex;

            var colsSum = dic.Keys.Where(x => x.StartsWith("GiaTri") || x.StartsWith("KinhPhi")).ToList();
            foreach (var grCTrinh in grsCTrinh)
            {

                DataRow newRowCtrinh = dtData.NewRow();
                dtData.Rows.Add(newRowCtrinh);

                var crRowCtrInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;

                DataRow fstCtr = grCTrinh.First();
                newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                newRowCtrinh[TDKH.COL_Code] = grCTrinh.Key;
                newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                newRowCtrinh[TDKH.COL_DanhMucCongTac] = fstCtr["TenCongTrinh"];
                newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{rowTongInd + 1})";

                var grsMuiTC = grCTrinh.GroupBy(x => x["CodeMuiThiCong"].ToString())
                    .OrderBy(x => x.Key.HasValue());

                foreach (var grMuiTC in grsMuiTC)
                {
                    DataRow fstMuiTC = grMuiTC.First();
                    int? crRowMuiTCInd = null;
                    bool isMTC = grMuiTC.Key.HasValue();
                    DataRow newRowMuiTC = null;
                    if (isMTC)
                    {
                        newRowMuiTC = dtData.NewRow();
                        dtData.Rows.Add(newRowMuiTC);

                        crRowMuiTCInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;

                        newRowMuiTC[TDKH.COL_Code] = grMuiTC.Key;
                        newRowMuiTC[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_MuiThiCong;
                        newRowMuiTC[TDKH.COL_DanhMucCongTac] = fstMuiTC["TenMuiThiCong"];
                        newRowMuiTC[TDKH.COL_TypeRow] = MyConstant.TYPEROW_MuiThiCong;
                        newRowMuiTC[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd + 1})";
                    }

                    var grsHM = grMuiTC.GroupBy(x => x["CodeHangMuc"]);
                    int STTHM = 0;
                    foreach (var grHM in grsHM)
                    {
                        DataRow newRowHM = dtData.NewRow();
                        dtData.Rows.Add(newRowHM);
                       
                        var crRowHMInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;


                        DataRow fstHM = grHM.First();
                        newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                        newRowHM[TDKH.COL_Code] = grHM.Key;
                        newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                        newRowHM[TDKH.COL_DanhMucCongTac] = fstHM["TenHangMuc"];
                        newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                        newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowMuiTCInd ?? crRowCtrInd) + 1})";

                        var grsPhanTuyen = grHM.GroupBy(x => x["CodePhanTuyen"].ToString())
                                .OrderByDescending(x => x.Key.HasValue());

                        foreach (var grPhanTuyen in grsPhanTuyen)
                        {
                            DataRow fstPT = grPhanTuyen.First();
                            int? crRowPTInd = null;
                            DataRow newRowPT = null;

                            if (grPhanTuyen.Key.HasValue())
                            {
                                newRowPT = dtData.NewRow();
                                dtData.Rows.Add(newRowPT);

                                crRowPTInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                newRowPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                                newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
                                newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                                newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";
                            }

                            var grsVatTu = grPhanTuyen.GroupBy(x => x["CodeVatTu"]);

                            int STTVatTu = 0;
                            foreach (var grVatTu in grsVatTu)
                            {
                                DataRow newRowVT = dtData.NewRow();
                                dtData.Rows.Add(newRowVT);

                                int crRowVtInd = crRowInd = fstInd + dtData.Rows.Count;
                                var fstVT = grVatTu.First();

                                if (fstVT["CodeVatTu"] == DBNull.Value && fstVT["CodeGiaoThau"] == DBNull.Value)
                                    continue;

                                //var klhnsInVatTu = klhnsVtu.Where(x => x.ParentCode == fstVT["CodeVatTu"].ToString());

                                newRowVT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{++STTVatTu}";
                                newRowVT[TDKH.COL_Code] = fstVT["CodeVatTu"];
                                newRowVT[TDKH.COL_MaHieuCongTac] = fstVT["MaVatLieu"];
                                newRowVT[TDKH.COL_DanhMucCongTac] = fstVT["VatTu"];
                                newRowVT[TDKH.COL_DonVi] = fstVT["DonVi"];
                                newRowVT[TDKH.COL_DonGia] = fstVT["DonGia"];
                                newRowVT[TDKH.COL_DonGiaThiCong] = fstVT["DonGiaThiCongVatTuNhanThau"];
                                newRowVT[TDKH.COL_DBC_KhoiLuongToanBo] = fstVT["KhoiLuongKeHoachVatTu"];
                                newRowVT[TDKH.COL_KhoiLuongHopDongChiTiet] = fstVT["KhoiLuongHopDongVatTu"];
                                newRowVT[TDKH.COL_DonGiaThiCong] = fstVT["DonGiaThiCongVatTuNhanThau"];
                                newRowVT[TDKH.COL_MayQuyDoi] = fstVT["MayQuyDoi"];
                                //newRowVTDics[TDKH.COL_DonGiaThiCong]].SetValue(fstVT.DonGiaThiCong);
                                newRowVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                newRowVT[TDKH.COL_KHVT_Search] = fstVT["SearchString"];
                                newRowVT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{fstVT["NgayBatDauVatTu"]}";
                                newRowVT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{fstVT["NgayKetThucVatTu"]}";
                                newRowVT[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                newRowVT[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";

                                newRowVT[TDKH.COL_GiaTriKeHoachGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}";
                                newRowVT[TDKH.COL_GiaTriThiCongGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                newRowVT[TDKH.COL_GiaTriThiCongLuyKeDenKyTruocTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                newRowVT[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";


                                newRowVT[TDKH.COL_NgayBatDauThiCong] = fstVT["NgayBatDauThiCong"];
                                newRowVT[TDKH.COL_NgayKetThucThiCong] = fstVT["NgayKetThucThiCong"];
                                newRowVT[TDKH.COL_GiaTriThiCong] = fstVT["ThanhTienDaThiCong"];
                                newRowVT[TDKH.COL_KhoiLuongDaThiCong] = fstVT["KhoiLuongDaThiCong"];

                                newRowVT[TDKH.COL_KHVT_MaTXHientruong] = fstVT["MaTXHienTruong"];

                                newRowVT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd) + 1})";

                                newRowVT[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1};0)";
                                newRowVT[TDKH.COL_KinhPhiDuKien] =
                                $"{prefixFormula}ROUND({dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{TDKH.rangesTongKinhPhiVatTu[indTypeSheet]}*{TDKH.rangesKinhPhiDuKienVatTu[indTypeSheet]};0)";

                                foreach (var haoPhi in grVatTu)
                                {
                                    WaitFormHelper.ShowWaitForm($"{count++}: {haoPhi["VatTu"]}", "Đang tải vật liệu");

                                    DataRow newRowHp = dtData.NewRow();
                                    dtData.Rows.Add(newRowHp);
                                    ++crRowInd;

                                    //var klhnsInHaoPhi = klhnsHPhi.Where(x => x.ParentCode == haoPhi["Code"].ToString());

                                    newRowHp[TDKH.COL_MaHieuCongTac] = haoPhi["MaHieuCongTac"];
                                    newRowHp[TDKH.COL_DanhMucCongTac] = haoPhi["TenCongTac"];
                                    newRowHp[TDKH.COL_Code] = haoPhi["Code"];
                                    newRowHp[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowVtInd + 1})";
                                    newRowHp[TDKH.COL_DonGiaThiCong] = haoPhi["DonGiaThiCong"];
                                    newRowHp[TDKH.COL_DonGia] = haoPhi["DonGia"];
                                    newRowHp[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCON;
                                    newRowHp[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{haoPhi["NgayBatDau"]}";// haoPhi["NgayBatDau"];
                                    newRowHp[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{haoPhi["NgayKetThuc"]}"; //haoPhi["NgayKetThuc"];
                                    newRowHp[TDKH.COL_DBC_KhoiLuongToanBo] = haoPhi["KhoiLuongKeHoach"];
                                    newRowHp[TDKH.COL_KhoiLuongHopDongChiTiet] = haoPhi["KhoiLuongHopDong"];
                                    newRowHp[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = klsHpALLByCTac.Where(x => x.ParentCode == newRowHp["Code"].ToString()).SingleOrDefault()?.KhoiLuongThiCong ?? 0;

                                    //var thicongs = klhnsInHaoPhi.Where(x => x.KhoiLuongThiCong > 0).Select(x => x.Ngay);

                                    //newRowVT[TDKH.COL_NgayBatDauThiCong] = fstVT["NgayBatDauThiCong"];
                                    //newRowVT[TDKH.COL_NgayKetThucThiCong] = fstVT["NgayKetThucThiCong"];
                                    //newRowVT[TDKH.COL_GiaTriThiCong] = fstVT["ThanhTienDaThiCong"];
                                    //newRowVT[TDKH.COL_KhoiLuongDaThiCong] = fstVT["KhoiLuongDaThiCong"];


                                    newRowHp[TDKH.COL_GiaTriKeHoachGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}";
                                    newRowHp[TDKH.COL_GiaTriThiCongGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                    newRowHp[TDKH.COL_GiaTriThiCongLuyKeDenKyTruocTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                    newRowHp[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";


                                    newRowHp[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanCongTac]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}";
                                    newRowHp[TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongGiaiDoanCongTac]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}";
                                    newRowHp[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac]}{crRowInd +1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}";
                                    newRowHp[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}";



                                    newRowHp[TDKH.COL_KHVT_Search] = haoPhi["Code"];

                                    newRowHp[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1};0)";
                                    newRowHp[TDKH.COL_KinhPhiDuKien] =
                                    $"{prefixFormula}ROUND({dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{TDKH.rangesTongKinhPhiVatTu[indTypeSheet]}*{TDKH.rangesKinhPhiDuKienVatTu[indTypeSheet]};0)";

                                    newRowHp[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                    newRowHp[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";


                                    //crRowWs[Dics[TDKH.COL_DBC_KhoiLuongToanBo]].Formula = $"{Dics[TDKH.COL_NgayKetThucThiCong]}{crRowsInd + 1} - {Dics[TDKH.COL_NgayBatDauThiCong]}{crRowsInd + 1} + 1";
                                    //crRowWs[Dics[TDKH.COL_SoNgayThiCong]].Formula = $"{Dics[TDKH.COL_NgayKetThucThiCong]}{crRowsInd + 1} - {Dics[TDKH.COL_NgayBatDauThiCong]}{crRowsInd + 1} + 1";

                                    //DinhMucHelper.fcn_TDKH_CapNhatRangeNgayVatLieu(TypeKLHN.HaoPhiVatTu, haoPhi.Code, crRowWs);
                                }

                                newRowVT[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = $"{MyConstant.PrefixFormula}SUM({dic[TDKH.COL_KhoiLuongDaThiCongTheoCongTac]}{crRowVtInd + 2}:{dic[TDKH.COL_KhoiLuongDaThiCongTheoCongTac]}{crRowInd + 1})";
                            }

                            if (grPhanTuyen.Key.HasValue())
                            {
                                DataRow newRowHTPT = dtData.NewRow();
                                dtData.Rows.Add(newRowHTPT);

                                ++crRowInd;

                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                newRowHTPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                newRowHTPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;
                                newRowHTPT[TDKH.COL_DanhMucCongTac] = $"{prefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                                newRowHTPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                newRowHTPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";

                                crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                                GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                                newRowPT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                                newRowPT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                                newRowPT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                                newRowPT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                                foreach (string col in colsSum)
                                {
                                    forGiaTri = GetFormulaSumChild((crRowPTInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                    newRowPT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                }
                            }
                        }

                        crRowInd = fstInd + dtData.Rows.Count;
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowHM[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowHM[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowHM[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowHM[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = GetFormulaSumChild(crRowHMInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }

                    }

                    if (isMTC)
                    {
                        crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowMuiTC[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowMuiTC[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowMuiTC[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowMuiTC[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = GetFormulaSumChild((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowMuiTC[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }

                    }
                }
            }


            var rowTong = dtData.NewRow();
            dtData.Rows.InsertAt(rowTong, 0);


            rowTong[TDKH.COL_DanhMucCongTac] = "TỔNG";
            crRowInd = fstInd - 1 + dtData.Rows.Count;
            GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
            GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

            rowTong[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
            rowTong[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
            rowTong[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
            rowTong[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";
            rowTong[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDau]}{fstInd + 1} + 1";
            rowTong[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{fstInd + 1} + 1";

            foreach (string col in colsSum)
            {
                forGiaTri = GetFormulaSumChild(fstInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                rowTong[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
            }

            int numRow = dtData.Rows.Count;
            ws.Rows.Insert(definedName.Range.BottomRowIndex, numRow + 5, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, rowTongInd, 0);
            ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;


            SpreadsheetHelper.ReplaceAllFormulaAfterImport(definedName.Range);
            SpreadsheetHelper.FormatRowsInRange(definedName.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha],
                dic[TDKH.COL_Code], colMaVatTu: dic[TDKH.COL_MaHieuCongTac]);


            //CheckAnHienCongTacVatTu(ws, SharedControls.cb_TDKH_HienCongTac.Checked);
            CheckAnHienPhanDoan();
            wb.EndUpdate();
            LoadGiaiDoanVatTu(ws);
            RETURN:
            WaitFormHelper.CloseWaitForm();

        }

        public static void LoadFullTongHopVatTuBrief(string codeCT, string codeHM, DateTime? start = null, DateTime? end = null)
        {

            if (SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH is null)
            {
                SharedControls.xtraTabControl_TienDoKeHoach.Enabled = false;
                return;
            }

            SharedControls.xtraTabControl_TienDoKeHoach.Enabled = true;

            string description = "Đang tải hao phí";

            WaitFormHelper.ShowWaitForm(description, "Đang tải vật tư");


            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;

            Worksheet ws = wb.Worksheets[TDKH.SheetName_TongHopHaoPhi];
            wb.BeginUpdate();
            ws.ScrollTo(0, 0);

            ws.Range["DateRange"].SetValue(null);
            //Worksheet wssTemp = wb.Worksheets[sheetsName[indTypeSheet] + "Temp"];
            DefinedName definedName = wb.DefinedNames.GetDefinedName(TDKH.RANGE_KeHoachVatTuFull);

            CellRange range = definedName.Range;

            if (range.RowCount > 2)
            {

                ws.Rows.Remove(range.TopRowIndex + 1, range.RowCount - 2);

            }
            definedName.Range.ForEach<Cell>(x => x.SetValue(""));




            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());


            if (!BaseFrom.allPermission.HaveInitProjectPermission)
            {
                ws.Columns[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Visible = false;
                ws.Columns[dic[TDKH.COL_KhoiLuongHopDongChiTiet]].Visible = false;
                ws.Columns[dic[TDKH.COL_DonGia]].Visible = false;
            }
            ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachTheoCongTac]].Visible =
                            ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongGiaiDoanCongTac]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongHopDongTheoCongTac]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongConLaiGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriKeHoachGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriThiCongGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriBoSungGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriConLaiGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc]].Visible =
ws.Columns[dic[TDKH.COL_PhanTramGiaTriKyNay]].Visible =
ws.Columns[dic[TDKH.COL_PhanTramGiaTriLuyKeKyNay]].Visible = false;
            int count = 0;
            int STTCtrinh = 0;

            //ws.Comments.Clear();

            string prefixFormula = MyConstant.PrefixFormula;

            var lsHaoPhi = GetFullInfoVatTuBriefDataTable(DoBocVatTu.AllVatTu, codeCT, codeHM, SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH, ignoreKLKH: true, isGetNhapKho: false);

            var codesHp = lsHaoPhi.AsEnumerable().Select(x => x["Code"].ToString()).Distinct();
            //string[] codesVT = lsHaoPhi.AsEnumerable().Select(x => x["Code"].ToString()).Distinct();
            string dbString = $@"
SELECT vvt.Code AS ParentCode, 
TOTAL(hp.HeSoNguoiDung*hp.DinhMucNguoiDung*kl.KhoiLuongThiCong) AS KhoiLuongThiCong
FROM {Server.Tbl_TDKH_KHVT_VatTu} vvt
JOIN {MyConstant.view_VatTuKeHoachThiCong} vttc
ON vvt.CodeNhaThau IS NOT NULL AND vvt.Code = vttc.CodeGiaoThau
JOIN {Server.Tbl_TDKH_HaoPhiVatTu} hp
ON vttc.Code = hp.CodeVatTu
JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} kl
ON hp.CodeCongTac = kl.CodeCongTacTheoGiaiDoan AND kl.KhoiLuongThiCong > 0
WHERE vvt.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHp)})
GROUP BY vvt.Code 
UNION ALL
SELECT vvt.Code AS ParentCode, 
TOTAL(hp.HeSoNguoiDung*hp.DinhMucNguoiDung*kl.KhoiLuongThiCong) AS KhoiLuongThiCong
FROM {Server.Tbl_TDKH_KHVT_VatTu} vvt
JOIN {Server.Tbl_TDKH_HaoPhiVatTu} hp
ON vvt.CodeNhaThau IS NULL AND vvt.Code = hp.CodeVatTu
JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} kl
ON hp.CodeCongTac = kl.CodeCongTacTheoGiaiDoan AND kl.KhoiLuongThiCong > 0
WHERE vvt.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHp)})
";

            var klsHpByCTac = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(dbString);

            var grsCTrinh = lsHaoPhi.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);

            //var codesCongTac = lsHaoPhi.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).Distinct();
            //var hncts = MyFunction.Fcn_CalKLKHModel(TypeKLHN.CongTac, codesCongTac, ignoreKLKH: true);


            DataTable dtData = DataTableCreateHelper.TienDoVatTuFullTongHop();

            int crRowInd = definedName.Range.TopRowIndex;
            int rowTongInd = crRowInd;

            string forGiaTri, forNBD, forNKT, forNBDTC, forNKTTC;
            int fstInd = definedName.Range.TopRowIndex;
            string[] colsSum = new string[]
{
                TDKH.COL_GiaTri,
                TDKH.COL_GiaTriThiCong,
                TDKH.COL_KinhPhiDuKien,
};

            int STTCTrinh = 0;
            ////          //wb.History.IsEnabled = false;
            //wb.BeginUpdate();

            ws.ScrollTo(0, 0);
            foreach (var grCTrinh in grsCTrinh)
            {

                DataRow newRowCtrinh = dtData.NewRow();
                dtData.Rows.Add(newRowCtrinh);

                var crRowCtrInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;


                DataRow fstCtr = grCTrinh.First();
                newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                newRowCtrinh[TDKH.COL_Code] = grCTrinh.Key;
                newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                newRowCtrinh[TDKH.COL_DanhMucCongTac] = fstCtr["TenCongTrinh"];
                newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{rowTongInd + 1})";

                var grsMuiTC = grCTrinh.GroupBy(x => x["CodeMuiThiCong"].ToString())
                    .OrderBy(x => x.Key.HasValue());
                foreach (var grMuiTC in grsMuiTC)
                {
                    DataRow fstMuiTC = grMuiTC.First();
                    int? crRowMuiTCInd = null;
                    bool isMTC = grMuiTC.Key.HasValue();
                    DataRow newRowMuiTC = null;
                    if (isMTC)
                    {
                        newRowMuiTC = dtData.NewRow();
                        dtData.Rows.Add(newRowMuiTC);

                        crRowMuiTCInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                        newRowMuiTC[TDKH.COL_Code] = grMuiTC.Key;
                        newRowMuiTC[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_MuiThiCong;
                        newRowMuiTC[TDKH.COL_DanhMucCongTac] = fstMuiTC["TenMuiThiCong"];
                        newRowMuiTC[TDKH.COL_TypeRow] = MyConstant.TYPEROW_MuiThiCong;
                        newRowMuiTC[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd + 1})";
                    }

                    var grsHM = grMuiTC.GroupBy(x => x["CodeHangMuc"]);
                    int STTHM = 0;
                    foreach (var grHM in grsHM)
                    {
                        DataRow newRowHM = dtData.NewRow();
                        dtData.Rows.Add(newRowHM);

                        var crRowHMInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;


                        DataRow fstHM = grHM.First();
                        newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                        newRowHM[TDKH.COL_Code] = grHM.Key;
                        newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                        newRowHM[TDKH.COL_DanhMucCongTac] = fstHM["TenHangMuc"];
                        newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                        newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowMuiTCInd ?? crRowCtrInd) + 1})";

                        var grsLoaiVT = grHM.GroupBy(x => x["LoaiVatTu"].ToString());

                        foreach (var grLoaiVT in grsLoaiVT)
                        {

                            DataRow newRowLVT = dtData.NewRow();
                            dtData.Rows.Add(newRowLVT);

                            int crRowLVTInd = fstInd + dtData.Rows.Count;
                            var fstLVT = grLoaiVT.First();

                            newRowLVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                            newRowLVT[TDKH.COL_DanhMucCongTac] = grLoaiVT.Key;

                            var grsPhanTuyen = grLoaiVT.GroupBy(x => x["CodePhanTuyen"].ToString())
                                        .OrderByDescending(x => x.Key.HasValue());

                            foreach (var grPhanTuyen in grsPhanTuyen)
                            {
                                DataRow fstPT = grPhanTuyen.First();
                                int? crRowPTInd = null;
                                DataRow newRowPT = null;

                                if (grPhanTuyen.Key.HasValue())
                                {
                                    newRowPT = dtData.NewRow();
                                    dtData.Rows.Add(newRowPT);

                                    crRowPTInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                    //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                    newRowPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                    newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                                    newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
                                    newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                                    newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";
                                }

                                var grsVatTu = grPhanTuyen.GroupBy(x => x["Code"]);
                                int STTVatTu = 0;
                                foreach (var grVatTu in grsVatTu)
                                {
                                    DataRow newRowVT = dtData.NewRow();
                                    dtData.Rows.Add(newRowVT);

                                    int crRowVtInd = crRowInd = fstInd + dtData.Rows.Count;
                                    var fstVT = grVatTu.First();

                                    if (fstVT["Code"] == DBNull.Value && fstVT["CodeGiaoThau"] == DBNull.Value)
                                        continue;

                                    //var klhnsInVatTu = klhnsVtu.Where(x => x.ParentCode == fstVT["Code"].ToString());

                                    newRowVT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{++STTVatTu}";
                                    newRowVT[TDKH.COL_Code] = fstVT["Code"];
                                    newRowVT[TDKH.COL_MaHieuCongTac] = fstVT["MaVatLieu"];
                                    newRowVT[TDKH.COL_DanhMucCongTac] = fstVT["VatTu"];
                                    newRowVT[TDKH.COL_DonVi] = fstVT["DonVi"];
                                    newRowVT[TDKH.COL_DonGia] = fstVT["DonGia"];
                                    newRowVT[TDKH.COL_DonGiaThiCong] = fstVT["DonGiaThiCong"];
                                    newRowVT[TDKH.COL_DBC_KhoiLuongToanBo] = fstVT["KhoiLuongKeHoach"];
                                    newRowVT[TDKH.COL_KhoiLuongHopDongChiTiet] = fstVT["KhoiLuongHopDong"];
                                    //newRowVTDics[TDKH.COL_DonGiaThiCong]].SetValue(fstVT.DonGiaThiCong);
                                    newRowVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                    newRowVT[TDKH.COL_KHVT_Search] = fstVT["SearchString"];
                                    newRowVT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{fstVT["NgayBatDau"]}"; //fstVT["NgayBatDau"];
                                    newRowVT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{fstVT["NgayKetThuc"]}"; //fstVT["NgayKetThuc"];


                                    newRowVT[TDKH.COL_KhoiLuongConLaiGiaiDoan] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]}{crRowInd + 1}-{dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}";
                                    newRowVT[TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1}-{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]}{crRowInd + 1}";
                                    newRowVT[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                    newRowVT[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                    newRowVT[TDKH.COL_PhanTramGiaTriKyNay] = $"{prefixFormula}ROUND({dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}/{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]}{crRowInd + 1}; 2)";
                                    newRowVT[TDKH.COL_PhanTramGiaTriLuyKeKyNay] = $"{prefixFormula}ROUND({dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}/{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1}; 2)";
                                    //var datesThiCong = klhnsInVatTu.Where(x => x.KhoiLuongThiCong > 0).Select(x => x.Ngay);

                                    //if (datesThiCong.Any())
                                    //{
                                    //}

                                    newRowVT[TDKH.COL_KhoiLuongDaNhapKho] = 0;// klhnsInVatTu.Sum(x => x.KhoiLuongNhapKho);
                                    newRowVT[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = klsHpByCTac.SingleOrDefault(x => x.Code == fstVT["Code"].ToString())?.KhoiLuongThiCong;
                                    newRowVT[TDKH.COL_NgayBatDauThiCong] = fstVT["NgayBatDauThiCong"];
                                        newRowVT[TDKH.COL_NgayKetThucThiCong] = fstVT["NgayKetThucThiCong"];
                                    newRowVT[TDKH.COL_GiaTriThiCong] = fstVT["ThanhTienDaThiCong"];
                                    newRowVT[TDKH.COL_KhoiLuongDaThiCong] = fstVT["KhoiLuongDaThiCong"];

                                    newRowVT[TDKH.COL_KHVT_MaTXHientruong] = fstVT["MaTXHienTruong"];

                                    newRowVT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";

                                    newRowVT[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = klsHpByCTac.SingleOrDefault(x => x.ParentCode == fstVT["Code"].ToString())?.KhoiLuongThiCong ??0;
                                    newRowVT[TDKH.COL_GiaTriKeHoachGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}";
                                    newRowVT[TDKH.COL_GiaTriThiCongGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                    newRowVT[TDKH.COL_GiaTriThiCongLuyKeDenKyTruocTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                    newRowVT[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";




                                    /*foreach (var haoPhi in grVatTu)
                                    {
                                        WaitFormHelper.ShowWaitForm($"{count++}: {haoPhi["VatTu"]}", "Đang tải vật liệu");

                                        DataRow newRowHp = dtData.NewRow();
                                        dtData.Rows.Add(newRowHp);
                                        ++crRowInd;

                                        var klhnctsInHaoPhi = hncts.Where(x => x.CodeCha == haoPhi["CodeCongTac"].ToString());
                                        var klhnsInHaoPhi = klhnsHPhi.Where(x => x.CodeCha == haoPhi["Code"].ToString());

                                        newRowHp[TDKH.COL_MaHieuCongTac] = haoPhi["MaHieuCongTac"];
                                        newRowHp[TDKH.COL_DanhMucCongTac] = haoPhi["TenCongTac"];
                                        newRowHp[TDKH.COL_Code] = haoPhi["Code"];
                                        newRowHp[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowVtInd + 1})";
                                        newRowHp[TDKH.COL_DonGiaThiCong] = haoPhi["DonGiaThiCong"];
                                        newRowHp[TDKH.COL_DonGia] = haoPhi["DonGia"];
                                        newRowHp[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCON;
                                        newRowHp[TDKH.COL_NgayBatDau] = haoPhi["NgayBatDau"];
                                        newRowHp[TDKH.COL_NgayKetThuc] = haoPhi["NgayKetThuc"];
                                        newRowHp[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = klsHpByCTac.Where(x => x.Code == newRowHp["Code"].ToString()).SingleOrDefault()?.KhoiLuongThiCong ?? 0;



                                        if (start.HasValue)
                                            newRowHp[TDKH.COL_TuNgay] = start;
                                        else newRowHp[TDKH.COL_TuNgay] = DBNull.Value;

                                        if (end.HasValue)
                                            newRowHp[TDKH.COL_DenNgay] = end;
                                        else newRowHp[TDKH.COL_DenNgay] = DBNull.Value;

                                        newRowHp[TDKH.COL_DBC_KhoiLuongToanBo] = haoPhi["KhoiLuongKeHoach"];
                                        newRowHp[TDKH.COL_KhoiLuongHopDongChiTiet] = haoPhi["KhoiLuongHopDong"];

                                        var thicongs = klhnsInHaoPhi.Where(x => x.KhoiLuongThiCong > 0).Select(x => x.Ngay);

                                        if (thicongs.Any())
                                        {
                                            newRowHp[TDKH.COL_NgayBatDauThiCong] = thicongs.Min();
                                            newRowHp[TDKH.COL_NgayKetThucThiCong] = thicongs.Max();
                                        }
                                        newRowHp[TDKH.COL_KhoiLuongDaThiCong] = (klhnsInHaoPhi.Sum(x => x.KhoiLuongThiCong));
                                        newRowHp[TDKH.COL_GiaTriThiCong] = (klhnsInHaoPhi.Sum(x => x.ThanhTienThiCong) ?? 0);


                                        newRowHp[TDKH.COL_KHVT_Search] = haoPhi["Code"];

                                        newRowHp[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1};0)";
                                        //newRowHp[TDKH.COL_KinhPhiDuKien] =
                                        //$"{prefixFormula}ROUND({Dics[TDKH.COL_GiaTri]}{crRowInd + 1}/{TDKH.rangesTongKinhPhiVatTu[indTypeSheet]}*{TDKH.rangesKinhPhiDuKienVatTu[indTypeSheet]};0)";

                                        newRowHp[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                        newRowHp[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                        newRowHp[TDKH.COL_SoNgayTongHop] = $"{prefixFormula}{dic[TDKH.COL_DenNgay]}{crRowInd + 1} - {dic[TDKH.COL_TuNgay]}{crRowInd + 1} + 1";
                                        //crRowWs[Dics[TDKH.COL_DBC_KhoiLuongToanBo]].Formula = $"{Dics[TDKH.COL_NgayKetThucThiCong]}{crRowsInd + 1} - {Dics[TDKH.COL_NgayBatDauThiCong]}{crRowsInd + 1} + 1";
                                        //crRowWs[Dics[TDKH.COL_SoNgayThiCong]].Formula = $"{Dics[TDKH.COL_NgayKetThucThiCong]}{crRowsInd + 1} - {Dics[TDKH.COL_NgayBatDauThiCong]}{crRowsInd + 1} + 1";

                                        //DinhMucHelper.fcn_TDKH_CapNhatRangeNgayVatLieu(TypeKLHN.HaoPhiVatTu, haoPhi.Code, crRowWs);
                                    }*/
                                }
                                if (grPhanTuyen.Key.HasValue())
                                {
                                    DataRow newRowHTPT = dtData.NewRow();
                                    dtData.Rows.Add(newRowHTPT);

                                    ++crRowInd;

                                    //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                    newRowHTPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                    newRowHTPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;
                                    newRowHTPT[TDKH.COL_DanhMucCongTac] = $"{prefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                                    newRowHTPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                    newRowHTPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";

                                    crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                    GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                                    GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                                    newRowPT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                                    newRowPT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                                    newRowPT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                                    newRowPT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                                    foreach (string col in colsSum)
                                    {
                                        forGiaTri = GetFormulaSumChild((crRowPTInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                        newRowPT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                    }
                                }
                            }

                            crRowInd = fstInd + dtData.Rows.Count;
                            GetFormulaMimMaxDate(crRowLVTInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                            GetFormulaMimMaxDate(crRowLVTInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                            newRowLVT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                            newRowLVT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                            newRowLVT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                            newRowLVT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                            foreach (string col in colsSum)
                            {
                                forGiaTri = GetFormulaSumChild(crRowLVTInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                newRowLVT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                            }


                        }

                        crRowInd = fstInd + dtData.Rows.Count;
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowHM[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowHM[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowHM[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowHM[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = GetFormulaSumChild(crRowHMInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }
                    }

                    if (isMTC)
                    {
                        crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowMuiTC[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowMuiTC[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowMuiTC[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowMuiTC[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = GetFormulaSumChild((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowMuiTC[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }

                    }

                }

                crRowInd = fstInd + dtData.Rows.Count;
                GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                newRowCtrinh[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                newRowCtrinh[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                newRowCtrinh[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                newRowCtrinh[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                foreach (string col in colsSum)
                {
                    forGiaTri = GetFormulaSumChild(crRowCtrInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                    newRowCtrinh[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                }
            }

            var rowTong = dtData.NewRow();
            dtData.Rows.InsertAt(rowTong, 0);


            rowTong[TDKH.COL_DanhMucCongTac] = "TỔNG";
            crRowInd = fstInd - 1 + dtData.Rows.Count;
            GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
            GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

            rowTong[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
            rowTong[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
            rowTong[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
            rowTong[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";
            rowTong[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDau]}{fstInd + 1} + 1";
            rowTong[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{fstInd + 1} + 1";

            foreach (string col in colsSum)
            {
                forGiaTri = GetFormulaSumChild(fstInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                rowTong[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
            }

            int numRow = dtData.Rows.Count;
            ws.Rows.Insert(definedName.Range.BottomRowIndex, numRow + 5, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, rowTongInd, 0);
            ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;


            SpreadsheetHelper.ReplaceAllFormulaAfterImport(definedName.Range);
            SpreadsheetHelper.FormatRowsInRange(definedName.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha],
                        dic[TDKH.COL_Code], colMaVatTu: dic[TDKH.COL_MaHieuCongTac]);

            //CheckAnHienCongTacVatTu(ws, SharedControls.cb_TDKH_HienCongTac.Checked);
            LoadGiaiDoanVatTu(ws);

            CheckAnHienPhanDoan();
            wb.EndUpdate();
            try
            {
                ////          //wb.History.IsEnabled = true;

            }
            catch (Exception) { }
            RETURN:
            WaitFormHelper.CloseWaitForm();
        }
        public static void LoadGiaiDoanVatTu(Worksheet ws = null)
        {
            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;

            ws = ws ?? SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            bool isLoc = SharedControls.ce_LocTheoNgay.Checked && SharedControls.de_Loc_TuNgay.EditValue != null && SharedControls.de_Loc_DenNgay.EditValue != null;
            wb.BeginUpdate();


         ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachTheoCongTac]].Visible =
         ws.Columns[dic[TDKH.COL_KhoiLuongHopDongTheoCongTac]].Visible =
         ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriKeHoachGiaiDoanTheoCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriThiCongGiaiDoanTheoCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruocTheoCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriConLaiGiaiDoan]].Visible =
            ws.Columns[dic[TDKH.COL_PhanTramGiaTriKyNay]].Visible =
            ws.Columns[dic[TDKH.COL_PhanTramGiaTriLuyKeKyNay]].Visible = isLoc;


            ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongGiaiDoanCongTac]].Visible =
         ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongConLaiGiaiDoan]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriKeHoachGiaiDoan]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriThiCongGiaiDoan]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriBoSungGiaiDoan]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc]].Visible =
            ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan]].Visible = isLoc;

            var start = SharedControls.de_Loc_TuNgay.DateTime;
            var end = SharedControls.de_Loc_DenNgay.DateTime;
            if (isLoc)
            {     

                //var klsHpALLByCTac = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(dbString);
                WaitFormHelper.ShowWaitForm("Đang tính khối lượng kế hoạch giai đoạn");
                ws.Range["DateRange"].SetValue($"Tổng hợp từ {start.ToShortDateString()} đên ngày {end.ToShortDateString()}");
                var colTypeRow = ws.Columns[dic[TDKH.COL_TypeRow]];
                //CTac
                var cells = colTypeRow.Search(MyConstant.TYPEROW_CVCha, MyConstant.MySearchOptions);

                var dicCVCha = new Dictionary<string, int>();
                foreach (var cell in cells)
                {
                    string code = ws.Rows[cell.RowIndex][dic[TDKH.COL_Code]].Value.ToString();
                    dicCVCha.Add(code, cell.RowIndex);
                }
                
                var cellCons = colTypeRow.Search(MyConstant.TYPEROW_CVCON, MyConstant.MySearchOptions);

                var dicCVCon = new Dictionary<string, int>();
                foreach (var cellCon in cellCons)
                {
                    string code = ws.Rows[cellCon.RowIndex][dic[TDKH.COL_Code]].Value.ToString();
                    dicCVCon.Add(code, cellCon.RowIndex);
                }
                var startDateString = start.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                var endDateString = end.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                if (dicCVCha.Any())
                {

                    string dbString = $@"SELECT vt.Code AS ParentCode,
                                        TOTAL(CASE WHEN klhn.Ngay < '{startDateString}' THEN  vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongThiCong ELSE 0 END) AS KLTCUntilPrevPeriod,
                                        TOTAL(CASE WHEN klhn.Ngay >= '{startDateString}' THEN  vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongThiCong ELSE 0 END) AS KLTCInRange, 
                                        TOTAL(CASE WHEN klhn.Ngay >= '{startDateString}' THEN  vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongKeHoach ELSE 0 END) AS KLKHInRange, klhn.Ngay
                                        FROM {Server.Tbl_TDKH_KHVT_VatTu} vt
                                        JOIN {MyConstant.view_VatTuKeHoachThiCong} vttc
                                        ON (vt.CodeNhaThau IS NOT NULL AND vt.Code = vttc.CodeGiaoThau)

                                        JOIN {MyConstant.view_HaoPhiVatTu} vvt
                                        ON vttc.Code = vvt.CodeVatTu
                                        LEFT JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn
                                        ON vvt.CodeCongTac = klhn.CodeCongTacTheoGiaiDoan
                                        AND klhn.Ngay <= '{endDateString}'
                                        WHERE vt.Code IN ({MyFunction.fcn_Array2listQueryCondition(dicCVCha.Keys)})
                                        GROUP BY vt.Code
UNION ALL
SELECT vt.Code AS ParentCode,
                                        TOTAL(CASE WHEN klhn.Ngay < '{startDateString}' THEN  vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongThiCong ELSE 0 END) AS KLTCUntilPrevPeriod,
                                        TOTAL(CASE WHEN klhn.Ngay >= '{startDateString}' THEN  vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongThiCong ELSE 0 END) AS KLTCInRange, 
                                        TOTAL(CASE WHEN klhn.Ngay >= '{startDateString}' THEN  vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongKeHoach ELSE 0 END) AS KLKHInRange, klhn.Ngay
                                        FROM {Server.Tbl_TDKH_KHVT_VatTu} vt

                                        JOIN {MyConstant.view_HaoPhiVatTu} vvt
                                        ON vt.CodeNhaThau IS NULL AND vt.Code = vvt.CodeVatTu
                                        LEFT JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn
                                        ON vvt.CodeCongTac = klhn.CodeCongTacTheoGiaiDoan
                                        AND klhn.Ngay <= '{endDateString}'
                                        WHERE vt.Code IN ({MyFunction.fcn_Array2listQueryCondition(dicCVCha.Keys)})
                                        GROUP BY vt.Code
";


                    var KLTCHNs = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHNBriefViewModel>(dbString);
                    var KLHNs = MyFunction.CalcKLHNBrief(TypeKLHN.VatLieu, dicCVCha.Keys, start, end);
                    foreach (var item in dicCVCha)
                    {
                        var klhnsInCha = KLHNs.SingleOrDefault(x => x.ParentCode == item.Key);
                        var klhnInCtac = klhnsInCha?.KLKHInRange;
                        var kltcInCtac = klhnsInCha?.KLTCInRange;
                        var klbsInCtac = klhnsInCha?.KLBSInRange;
                        var kllkInCtac = klhnsInCha?.LuyKeKLKHLastDate ?? 0;
                        var kllkTCInCtac = klhnsInCha?.LuyKeKLTCLastDate ?? 0;
                        //var klclInCtac = klhnInCtac - kltcInCtac;

                        var gthnInCtac = klhnsInCha?.TTKHInRange;
                        var gttcInCtac = klhnsInCha?.TTTCInRange;
                        var gtbsInCtac = klhnsInCha?.TTBSInRange;
                        var gtlkInCtac = klhnsInCha?.LuyKeTTKHLastDate ?? 0;
                        var gtlktcInCtac = klhnsInCha?.LuyKeTTTCLastDate ?? 0;
                        var gtclInCtac = gthnInCtac - gttcInCtac;

                        var kltc = KLTCHNs.SingleOrDefault(x => x.ParentCode == item.Key);
                        var kllkktByCtac = kltc?.KLTCUntilPrevPeriod ?? 0;
                        var klknByCtac = kltc?.KLTCInRange;
                        var klkhByCtac = kltc?.KLKHInRange ?? 0;

                        var kllkknnByCtac = kllkktByCtac + klknByCtac;

                        var crRow = ws.Rows[item.Value];

                        crRow[dic[TDKH.COL_KhoiLuongHopDongTheoCongTac]].SetValue(kltc?.KhoiLuongHopDong);

                        crRow[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].SetValue(klhnInCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]].SetValue(kllkTCInCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]].SetValue(kltcInCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc]].SetValue(klhnsInCha?.LuyKeKLTCKyTruoc);
                        //ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongConLaiGiaiDoan]].SetValue(klclInCtac);

                        crRow[dic[TDKH.COL_GiaTriKeHoachGiaiDoan]].SetValue(gthnInCtac);
                        crRow[dic[TDKH.COL_GiaTriKeHoachGiaiDoanTheoCongTac]].SetValue(klkhByCtac); 
                        crRow[dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc]].SetValue(klhnsInCha?.TTTCFromBeginOfPrj);
                        crRow[dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan]].SetValue(gtlktcInCtac);
                        crRow[dic[TDKH.COL_GiaTriThiCongGiaiDoan]].SetValue(gttcInCtac);
                        crRow[dic[TDKH.COL_GiaTriBoSungGiaiDoan]].SetValue(gtbsInCtac);
                        crRow[dic[TDKH.COL_GiaTriConLaiGiaiDoan]].SetValue(gtclInCtac);


                        crRow[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac]].SetValue(kllkknnByCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac]].SetValue(klknByCtac);
                        crRow[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac]].SetValue(klkhByCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac]].SetValue(kllkktByCtac);

                    }
                }
                
                if (dicCVCon.Any())
                {

                    string dbString = $@"SELECT vvt.Code AS Code, cttk.KhoiLuongToanBo AS KLKHSum, cttk.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong,
                                        TOTAL(CASE WHEN klhn.Ngay < '{startDateString}' THEN  klhn.KhoiLuongThiCong ELSE 0 END) AS KLTCUntilPrevPeriod,
                                        TOTAL(CASE WHEN klhn.Ngay >= '{startDateString}' THEN  klhn.KhoiLuongThiCong ELSE 0 END) AS KLTCFromBeginOfPrj, 
                                        TOTAL(CASE WHEN klhn.Ngay >= '{startDateString}' THEN  klhn.KhoiLuongKeHoach ELSE 0 END) AS KLKHInRange,klhn.Ngay
                                        FROM {MyConstant.view_HaoPhiVatTu} vvt
                                        JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk
                                        ON vvt.CodeCongTac = cttk.Code
                                        LEFT JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn
                                        ON cttk.Code = klhn.CodeCongTacTheoGiaiDoan
                                        AND klhn.Ngay <= '{endDateString}'
                                        WHERE vvt.Code IN ({MyFunction.fcn_Array2listQueryCondition(dicCVCon.Keys)})
                                        GROUP BY vvt.Code";

                    //var klByCTacInRange = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHNBriefViewModel>
                    //    (string.Format(dbString, $" AND Ngay >= '{start.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND Ngay <= '{end.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'"));

                    //var klByCTacPrevPeriod = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>
                    //    (string.Format(dbString, $" AND Ngay < '{start.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'"));

                    var KLTCHNs = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHNBriefViewModel>(dbString);
                    var KLHNs = MyFunction.CalcKLHNBrief(TypeKLHN.VatLieu, dicCVCon.Keys, start, end);
                    foreach (var item in dicCVCon)
                    {
                        var klhnsInCon = KLHNs.SingleOrDefault(x => x.ParentCode == item.Key);
                        var klhnInCtac = klhnsInCon?.KLKHInRange;
                        var kltcInCtac = klhnsInCon?.KLTCInRange;
                        var klbsInCtac = klhnsInCon?.KLBSInRange;
                        var kllkInCtac = klhnsInCon?.LuyKeKLBSLastDate ?? 0;
                        var kllkTCInCtac = klhnsInCon?.LuyKeKLTCLastDate ?? 0;
                        //var klclInCtac = klhnInCtac - kltcInCtac;

                        var gthnInCtac = klhnsInCon?.TTKHInRange;
                        var gttcInCtac = klhnsInCon?.TTTCInRange;
                        var gtbsInCtac = klhnsInCon?.TTBSInRange;
                        var gtlkInCtac = klhnsInCon?.LuyKeTTKHLastDate??0;
                        var gtlktcInCtac = klhnsInCon?.LuyKeTTTCLastDate??0;
                        var gtclInCtac = gthnInCtac - gttcInCtac;

                        var kltc = KLTCHNs.SingleOrDefault(x => x.ParentCode == item.Key);
                        var kllkktByCtac = kltc?.KLTCUntilPrevPeriod ?? 0;
                        var klknByCtac = kltc?.LuyKeKLTCLastDate;
                        var klkhByCtac = kltc?.KLKHInRange ?? 0;
                        var kllkknnByCtac = kllkktByCtac + klknByCtac;

                        var crRow = ws.Rows[item.Value];

                        crRow[dic[TDKH.COL_KhoiLuongKeHoachTheoCongTac]].SetValue(kltc?.KLKHSum);
                        crRow[dic[TDKH.COL_KhoiLuongHopDongTheoCongTac]].SetValue(kltc?.KhoiLuongHopDong);


                        crRow[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].SetValue(klhnInCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]].SetValue(kllkTCInCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]].SetValue(kltcInCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc]].SetValue(klhnsInCon?.LuyKeKLTCKyTruoc);
                        //ws.Rows[item.Value][dic[TDKH.COL_KhoiLuongConLaiGiaiDoan]].SetValue(klclInCtac);

                        crRow[dic[TDKH.COL_GiaTriKeHoachGiaiDoan]].SetValue(gthnInCtac);
                        crRow[dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc]].SetValue(klhnsInCon?.TTTCFromBeginOfPrj);
                        crRow[dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan]].SetValue(gtlktcInCtac);
                        crRow[dic[TDKH.COL_GiaTriThiCongGiaiDoan]].SetValue(gttcInCtac);
                        crRow[dic[TDKH.COL_GiaTriBoSungGiaiDoan]].SetValue(gtbsInCtac);
                        crRow[dic[TDKH.COL_GiaTriConLaiGiaiDoan]].SetValue(gtclInCtac);


                        crRow[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac]].SetValue(kllkknnByCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongGiaiDoanCongTac]].SetValue(klknByCtac);
                        crRow[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanCongTac]].SetValue(klkhByCtac);
                        crRow[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac]].SetValue(kllkktByCtac);

                        

                    }
                }
                WaitFormHelper.CloseWaitForm();

            }
            wb.EndUpdate();
        }
        public static void LoadFullTongHopVatTu(string codeCT, string codeHM, DateTime? start = null, DateTime? end = null)
        {

            if (SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH is null)
            {
                SharedControls.xtraTabControl_TienDoKeHoach.Enabled = false;
                return;
            }

            SharedControls.xtraTabControl_TienDoKeHoach.Enabled = true;

            string description = "Đang tải hao phí";

            WaitFormHelper.ShowWaitForm(description, "Đang tải vật tư");


            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;

            Worksheet ws = wb.Worksheets[TDKH.SheetName_TongHopHaoPhi];

            wb.BeginUpdate();
            ws.ScrollTo(0, 0);

            ws.Range["DateRange"].SetValue(null);
            //Worksheet wssTemp = wb.Worksheets[sheetsName[indTypeSheet] + "Temp"];
            DefinedName definedName = wb.DefinedNames.GetDefinedName(TDKH.RANGE_KeHoachVatTuFull);

            CellRange range = definedName.Range;

            if (range.RowCount > 2)
            {

                ws.Rows.Remove(range.TopRowIndex + 1, range.RowCount - 2);

            }
            definedName.Range.ForEach<Cell>(x => x.SetValue(""));
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());         
            if (!BaseFrom.allPermission.HaveInitProjectPermission)
            {
                ws.Columns[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Visible = false;
                ws.Columns[dic[TDKH.COL_KhoiLuongHopDongChiTiet]].Visible = false;
                ws.Columns[dic[TDKH.COL_DonGia]].Visible = false;
            }
            ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachTheoCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongHopDongTheoCongTac]].Visible =
                        ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac]].Visible =
            ws.Columns[dic[TDKH.COL_KhoiLuongThiCongGiaiDoanCongTac]].Visible =
   ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]].Visible =
   ws.Columns[dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongBoSungGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongConLaiGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriKeHoachGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriThiCongGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriBoSungGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriConLaiGiaiDoan]].Visible =
ws.Columns[dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc]].Visible =
ws.Columns[dic[TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc]].Visible =
ws.Columns[dic[TDKH.COL_PhanTramGiaTriKyNay]].Visible =
ws.Columns[dic[TDKH.COL_PhanTramGiaTriLuyKeKyNay]].Visible = false;

            ws.Range["DateRange"].SetValue(null);
            //int indCha = crRowsInd;
            int count = 0;
            int STTCtrinh = 0;

            ws.Comments.Clear();

            string prefixFormula = MyConstant.PrefixFormula;

            var lsHaoPhi = GetFullInfoVatTuWithHaoPhiDataTable(DoBocVatTu.AllVatTu, codeCT, codeHM, SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH, start, end);

            var codesHp = lsHaoPhi.AsEnumerable().Select(x => x["Code"].ToString()).Distinct();
            //string[] codesVT = lsHaoPhi.AsEnumerable().Select(x => x["Code"].ToString()).Distinct();

            string dbString = $@"SELECT vvt.Code AS ParentCode, TOTAL(vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongThiCong) AS KhoiLuongThiCong, klhn.Ngay
FROM {MyConstant.view_HaoPhiVatTu} vvt
JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} ct
ON vvt.CodeCongTac = ct.Code
JOIN {MyConstant.view_CongTacKeHoachThiCong} cttc
ON ct.CodeNhaThau IS NOT NULL AND ct.Code = cttc.CodeGiaoThau
JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn
ON cttc.Code = klhn.CodeCongTacTheoGiaiDoan AND klhn.KhoiLuongThiCong IS NOT NULL
WHERE vvt.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHp)})
GROUP BY vvt.Code
UNION ALL
SELECT vvt.Code AS ParentCode, TOTAL(vvt.HeSoNguoiDung*vvt.DinhMucNguoiDung*klhn.KhoiLuongThiCong) AS KhoiLuongThiCong, klhn.Ngay
FROM {MyConstant.view_HaoPhiVatTu} vvt
JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} ct
ON vvt.CodeCongTac = ct.Code
JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn
ON ct.CodeNhaThau IS NULL AND ct.Code = klhn.CodeCongTacTheoGiaiDoan AND klhn.KhoiLuongThiCong IS NOT NULL
WHERE vvt.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHp)})
GROUP BY vvt.Code

";

            var klsHpALLByCTac = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(dbString);

            var grsCTrinh = lsHaoPhi.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);

            var codesCongTac = lsHaoPhi.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).Distinct();
            //var hncts = MyFunction.Fcn_CalKLKHModel(TypeKLHN.CongTac, codesCongTac);


            DataTable dtData = DataTableCreateHelper.TienDoVatTuFullTongHop();

            int crRowInd = definedName.Range.TopRowIndex;
            int rowTongInd = crRowInd;

            int STTCTrinh = 0;
            ////          //wb.History.IsEnabled = false;
            string forGiaTri, forNBD, forNKT, forNBDTC, forNKTTC;
            int fstInd = definedName.Range.TopRowIndex;
            string[] colsSum = new string[]
{
                TDKH.COL_GiaTri,
                TDKH.COL_GiaTriThiCong,
                TDKH.COL_KinhPhiDuKien,
};


            foreach (var grCTrinh in grsCTrinh)
            {

                DataRow newRowCtrinh = dtData.NewRow();
                dtData.Rows.Add(newRowCtrinh);

                var crRowCtrInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;


                DataRow fstCtr = grCTrinh.First();
                newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                newRowCtrinh[TDKH.COL_Code] = grCTrinh.Key;
                newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                newRowCtrinh[TDKH.COL_DanhMucCongTac] = fstCtr["TenCongTrinh"];
                newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{rowTongInd + 1})";

                var grsMuiTC = grCTrinh.GroupBy(x => x["CodeMuiThiCong"].ToString())
                    .OrderBy(x => x.Key.HasValue());

                foreach (var grMuiTC in grsMuiTC)
                {
                    DataRow fstMuiTC = grMuiTC.First();
                    int? crRowMuiTCInd = null;
                    bool isMTC = grMuiTC.Key.HasValue();
                    DataRow newRowMuiTC = null;
                    if (isMTC)
                    {
                        newRowMuiTC = dtData.NewRow();
                        dtData.Rows.Add(newRowMuiTC);

                        crRowMuiTCInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                        newRowMuiTC[TDKH.COL_Code] = grMuiTC.Key;
                        newRowMuiTC[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_MuiThiCong;
                        newRowMuiTC[TDKH.COL_DanhMucCongTac] = fstMuiTC["TenMuiThiCong"];
                        newRowMuiTC[TDKH.COL_TypeRow] = MyConstant.TYPEROW_MuiThiCong;
                        newRowMuiTC[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd + 1})";
                    }

                    var grsHM = grMuiTC.GroupBy(x => x["CodeHangMuc"]);
                    int STTHM = 0;
                    foreach (var grHM in grsHM)
                    {
                        DataRow newRowHM = dtData.NewRow();
                        dtData.Rows.Add(newRowHM);

                        var crRowHMInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;


                        DataRow fstHM = grHM.First();
                        newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                        newRowHM[TDKH.COL_Code] = grHM.Key;
                        newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                        newRowHM[TDKH.COL_DanhMucCongTac] = fstHM["TenHangMuc"];
                        newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                        newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowMuiTCInd ?? crRowCtrInd) + 1})";

                        var grsLoaiVT = grHM.GroupBy(x => x["LoaiVatTu"].ToString());

                        foreach (var grLoaiVT in grsLoaiVT)
                        {

                            DataRow newRowLVT = dtData.NewRow();
                            dtData.Rows.Add(newRowLVT);

                            int crRowLVTInd = fstInd + dtData.Rows.Count;

                            var fstLVT = grLoaiVT.First();

                            newRowLVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                            newRowLVT[TDKH.COL_DanhMucCongTac] = grLoaiVT.Key;

                            var grsPhanTuyen = grLoaiVT.GroupBy(x => x["CodePhanTuyen"].ToString())
                                        .OrderByDescending(x => x.Key.HasValue());

                            foreach (var grPhanTuyen in grsPhanTuyen)
                            {
                                DataRow fstPT = grPhanTuyen.First();
                                int? crRowPTInd = null;
                                DataRow newRowPT = null;

                                if (grPhanTuyen.Key.HasValue())
                                {
                                    newRowPT = dtData.NewRow();
                                    dtData.Rows.Add(newRowPT);

                                    crRowPTInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                    //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                    newRowPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                    newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                                    newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
                                    newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                                    newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";
                                }

                                var grsVatTu = grPhanTuyen.GroupBy(x => x["CodeVatTu"]);
                                int STTVatTu = 0;
                                foreach (var grVatTu in grsVatTu)
                                {
                                    DataRow newRowVT = dtData.NewRow();
                                    dtData.Rows.Add(newRowVT);

                                    int crRowVtInd = crRowInd = fstInd + dtData.Rows.Count;
                                    var fstVT = grVatTu.First();

                                    if (fstVT["CodeVatTu"] == DBNull.Value && fstVT["CodeGiaoThau"] == DBNull.Value)
                                        continue;

                                    newRowVT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{++STTVatTu}";
                                    newRowVT[TDKH.COL_Code] = fstVT["CodeVatTu"];
                                    newRowVT[TDKH.COL_MaHieuCongTac] = fstVT["MaVatLieu"];
                                    newRowVT[TDKH.COL_DanhMucCongTac] = fstVT["VatTu"];
                                    newRowVT[TDKH.COL_DonVi] = fstVT["DonVi"];
                                    newRowVT[TDKH.COL_DonGia] = fstVT["DonGia"];
                                    newRowVT[TDKH.COL_DonGiaThiCong] = fstVT["DonGiaThiCongVatTu"];
                                    newRowVT[TDKH.COL_DBC_KhoiLuongToanBo] = fstVT["KhoiLuongKeHoachVatTu"];
                                    newRowVT[TDKH.COL_KhoiLuongHopDongChiTiet] = fstVT["KhoiLuongHopDongVatTu"];
                                    //newRowVTDics[TDKH.COL_DonGiaThiCong]].SetValue(fstVT.DonGiaThiCong);
                                    newRowVT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                    newRowVT[TDKH.COL_KHVT_Search] = fstVT["SearchString"];
                                    newRowVT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{fstVT["NgayBatDauVatTu"]}";
                                    newRowVT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{fstVT["NgayKetThucVatTu"]}";


                                    newRowVT[TDKH.COL_KhoiLuongConLaiGiaiDoan] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]}{crRowInd + 1}-{dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}";
                                    newRowVT[TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1}-{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan]}{crRowInd + 1}";
                                    
                                    newRowVT[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                    newRowVT[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
                                    newRowVT[TDKH.COL_PhanTramGiaTriKyNay] = $"{prefixFormula}ROUND({dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}/{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoan]}{crRowInd + 1}; 2)";
                                    newRowVT[TDKH.COL_PhanTramGiaTriLuyKeKyNay] = $"{prefixFormula}ROUND({dic[TDKH.COL_KhoiLuongThiCongGiaiDoan]}{crRowInd + 1}/{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1}; 2)";
                                    //var datesThiCong = klhnsInVatTu.Where(x => x.KhoiLuongThiCong > 0).Select(x => x.Ngay);


                                    //newRowVT[TDKH.COL_KhoiLuongDaNhapKho] = klhnsInVatTu.Sum(x => x.KhoiLuongNhapKho);
                                    newRowVT[TDKH.COL_NgayBatDauThiCong] = fstVT["NgayBatDauThiCong"];
                                    newRowVT[TDKH.COL_NgayKetThucThiCong] = fstVT["NgayKetThucThiCong"];
                                    newRowVT[TDKH.COL_GiaTriThiCong] = fstVT["ThanhTienDaThiCong"];
                                    newRowVT[TDKH.COL_KhoiLuongDaThiCong] = fstVT["KhoiLuongDaThiCong"];

                                    newRowVT[TDKH.COL_KHVT_MaTXHientruong] = fstVT["MaTXHienTruong"];

                                    newRowVT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd) + 1})";


                                    foreach (var haoPhi in grVatTu)
                                    {
                                        WaitFormHelper.ShowWaitForm($"{count++}: {haoPhi["VatTu"]}", "Đang tải vật liệu");

                                        DataRow newRowHp = dtData.NewRow();
                                        dtData.Rows.Add(newRowHp);
                                        ++crRowInd;


                                        newRowHp[TDKH.COL_MaHieuCongTac] = haoPhi["MaHieuCongTac"];
                                        newRowHp[TDKH.COL_DanhMucCongTac] = haoPhi["TenCongTac"];
                                        newRowHp[TDKH.COL_Code] = haoPhi["Code"];
                                        newRowHp[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowVtInd + 1})";
                                        newRowHp[TDKH.COL_DonGiaThiCong] = haoPhi["DonGiaThiCong"];
                                        newRowHp[TDKH.COL_DonGia] = haoPhi["DonGia"];
                                        newRowHp[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCON;
                                        newRowHp[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{haoPhi["NgayBatDau"]}"; //haoPhi["NgayBatDau"];
                                        newRowHp[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{haoPhi["NgayKetThuc"]}"; //haoPhi["NgayKetThuc"];
                                        newRowHp[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = klsHpALLByCTac.Where(x => x.ParentCode == newRowHp["Code"].ToString()).SingleOrDefault()?.KhoiLuongThiCong ?? 0;
                                        newRowHp[TDKH.COL_KHVT_HeSoNguoiDung] = fstVT["HeSoNguoiDung"];
                                        newRowHp[TDKH.COL_KHVT_DinhMucNguoiDung] = fstVT["DinhMucNguoiDung"];

                                        newRowHp[TDKH.COL_DBC_KhoiLuongToanBo] = haoPhi["KhoiLuongKeHoach"];
                                        newRowHp[TDKH.COL_KhoiLuongHopDongChiTiet] = haoPhi["KhoiLuongHopDong"];

                                        //newRowVT[TDKH.COL_NgayBatDauThiCong] = fstVT["NgayBatDauThiCong"];
                                        //newRowVT[TDKH.COL_NgayKetThucThiCong] = fstVT["NgayKetThucThiCong"];
                                        //newRowVT[TDKH.COL_GiaTriThiCong] = fstVT["ThanhTienDaThiCong"];
                                        //newRowVT[TDKH.COL_KhoiLuongDaThiCong] = fstVT["KhoiLuongDaThiCong"];
                                        newRowHp[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = klsHpALLByCTac.Where(x => x.ParentCode == newRowHp["Code"].ToString()).SingleOrDefault()?.KhoiLuongThiCong ?? 0;


                                        newRowHp[TDKH.COL_KHVT_Search] = haoPhi["Code"];

                                        newRowHp[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1};0)";
                                        //newRowHp[TDKH.COL_KinhPhiDuKien] =
                                        //$"{prefixFormula}ROUND({Dics[TDKH.COL_GiaTri]}{crRowInd + 1}/{TDKH.rangesTongKinhPhiVatTu[indTypeSheet]}*{TDKH.rangesKinhPhiDuKienVatTu[indTypeSheet]};0)";

                                        newRowHp[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
                                        newRowHp[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";

                                        newRowHp[TDKH.COL_GiaTriKeHoachGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}";
                                        newRowHp[TDKH.COL_GiaTriThiCongGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                        newRowHp[TDKH.COL_GiaTriThiCongLuyKeDenKyTruocTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";
                                        newRowHp[TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac]}{crRowInd + 1}*{dic[TDKH.COL_DonGiaThiCong]}{crRowInd + 1}";


                                        newRowHp[TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongKeHoachGiaiDoanCongTac]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}";
                                        newRowHp[TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongGiaiDoanCongTac]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}";
                                        newRowHp[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}";
                                        newRowHp[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_HeSoNguoiDung]}{crRowInd + 1}*{dic[TDKH.COL_KHVT_DinhMucNguoiDung]}{crRowInd + 1}";


                                        //crRowWs[Dics[TDKH.COL_DBC_KhoiLuongToanBo]].Formula = $"{Dics[TDKH.COL_NgayKetThucThiCong]}{crRowsInd + 1} - {Dics[TDKH.COL_NgayBatDauThiCong]}{crRowsInd + 1} + 1";
                                        //crRowWs[Dics[TDKH.COL_SoNgayThiCong]].Formula = $"{Dics[TDKH.COL_NgayKetThucThiCong]}{crRowsInd + 1} - {Dics[TDKH.COL_NgayBatDauThiCong]}{crRowsInd + 1} + 1";

                                        //DinhMucHelper.fcn_TDKH_CapNhatRangeNgayVatLieu(TypeKLHN.HaoPhiVatTu, haoPhi.Code, crRowWs);
                                    }
                                    newRowVT[TDKH.COL_KhoiLuongDaThiCongTheoCongTac] = $"{MyConstant.PrefixFormula}SUM({dic[TDKH.COL_KhoiLuongDaThiCongTheoCongTac]}{crRowVtInd + 2}:{dic[TDKH.COL_KhoiLuongDaThiCongTheoCongTac]}{crRowInd + 1})";

                                }
                                if (grPhanTuyen.Key.HasValue())
                                {
                                    DataRow newRowHTPT = dtData.NewRow();
                                    dtData.Rows.Add(newRowHTPT);

                                    ++crRowInd;

                                    //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                    newRowHTPT[TDKH.COL_Code] = grPhanTuyen.Key;
                                    newRowHTPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;
                                    newRowHTPT[TDKH.COL_DanhMucCongTac] = $"{prefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                                    newRowHTPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                    newRowHTPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";


                                    crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                                    GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                                    GetFormulaMimMaxDate((crRowPTInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                                    newRowPT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                                    newRowPT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                                    newRowPT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                                    newRowPT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                                    foreach (string col in colsSum)
                                    {
                                        forGiaTri = GetFormulaSumChild((crRowPTInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                        newRowPT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                    }
                                }
                            }


                            crRowInd = fstInd + dtData.Rows.Count;
                            GetFormulaMimMaxDate(crRowLVTInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                            GetFormulaMimMaxDate(crRowLVTInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                            newRowLVT[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                            newRowLVT[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                            newRowLVT[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                            newRowLVT[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                            foreach (string col in colsSum)
                            {
                                forGiaTri = GetFormulaSumChild(crRowLVTInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                newRowLVT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                            }
                        }

                        crRowInd = fstInd + dtData.Rows.Count;
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        GetFormulaMimMaxDate(crRowHMInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowHM[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowHM[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowHM[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowHM[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = GetFormulaSumChild(crRowHMInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }
                    }

                    if (isMTC)
                    {
                        crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
                        GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                        GetFormulaMimMaxDate((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                        newRowMuiTC[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                        newRowMuiTC[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                        newRowMuiTC[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                        newRowMuiTC[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                        foreach (string col in colsSum)
                        {
                            forGiaTri = GetFormulaSumChild((crRowMuiTCInd ?? 0) + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                            newRowMuiTC[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }

                    }
                }

                crRowInd = fstInd + dtData.Rows.Count;
                GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
                GetFormulaMimMaxDate(crRowCtrInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

                newRowCtrinh[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
                newRowCtrinh[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
                newRowCtrinh[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
                newRowCtrinh[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";

                foreach (string col in colsSum)
                {
                    forGiaTri = GetFormulaSumChild(crRowCtrInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                    newRowCtrinh[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                }
            }


            var rowTong = dtData.NewRow();
            dtData.Rows.InsertAt(rowTong, 0);


            rowTong[TDKH.COL_DanhMucCongTac] = "TỔNG";
            crRowInd = fstInd - 1 + dtData.Rows.Count;
            GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDau], dic[TDKH.COL_NgayKetThuc], out forNBD, out forNKT);
            GetFormulaMimMaxDate(fstInd + 1, crRowInd, dic[TDKH.COL_NgayBatDauThiCong], dic[TDKH.COL_NgayKetThucThiCong], out forNBDTC, out forNKTTC);

            rowTong[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixFormula}{forNBD}";
            rowTong[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixFormula}{forNKT}";
            rowTong[TDKH.COL_NgayBatDauThiCong] = $"{MyConstant.PrefixFormula}{forNBDTC}";
            rowTong[TDKH.COL_NgayKetThucThiCong] = $"{MyConstant.PrefixFormula}{forNKTTC}";
            rowTong[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDau]}{fstInd + 1} + 1";
            rowTong[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{fstInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{fstInd + 1} + 1";

            foreach (string col in colsSum)
            {
                forGiaTri = GetFormulaSumChild(fstInd + 1, crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                rowTong[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
            }


            int numRow = dtData.Rows.Count;
            ws.Rows.Insert(definedName.Range.BottomRowIndex, numRow + 5, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, rowTongInd, 0);
            ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;


            SpreadsheetHelper.ReplaceAllFormulaAfterImport(definedName.Range);
            SpreadsheetHelper.FormatRowsInRange(definedName.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha],
                        dic[TDKH.COL_Code], colMaVatTu: dic[TDKH.COL_MaHieuCongTac]);


            //CheckAnHienCongTacVatTu(ws, SharedControls.cb_TDKH_HienCongTac.Checked);
            LoadGiaiDoanVatTu(ws);
            CheckAnHienPhanDoan();
            wb.EndUpdate();
            try
            {
                ////          //wb.History.IsEnabled = true;

            }
            catch (Exception) { }
            RETURN:
            WaitFormHelper.CloseWaitForm();
        }

        public static void fcn_loadCongThucKeHoach(Row crRowWsKH, int firstInd)
        {
            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            Worksheet wsKH = crRowWsKH.Worksheet;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(wsKH.GetUsedRange());
            //wsKH.Calculate();
            //Row crRowWsKH = wss[KeHoach].Rows[crRowsInd[KeHoach]];
            //switch (wsKH.Name)
            //{
            //    case TDKH.SheetName_KeHoachKinhPhi:
            Dictionary<string, string> dicData = dic;
            int rowInd = crRowWsKH.Index;
            //int firstInd = wb.Range[TDKH.RANGE_KeHoach].TopRowIndex;
            crRowWsKH[dicData[TDKH.COL_PhanTramThucHien]].Formula = $"IF({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{rowInd + 1} = 0;\"\";{dic[TDKH.COL_KhoiLuongDaThiCong]}{rowInd + 1}/{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{rowInd + 1})";
            //crRowWsKH[dicData[TDKH.COL_GiaTri]].Formula = $"{dicData[TDKH.COL_DonGia]}{rowInd + 1}*{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{rowInd + 1}";
            crRowWsKH[dicData[TDKH.COL_GiaTri]].Formula = $"ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{rowInd + 1}*{dicData[TDKH.COL_DonGia]}{rowInd + 1}; 0)";
            //ws.Rows[lastInd][dicData[TDKH.COL_KinhPhiTheoTienDo]].Formula = $"SUM({dicData[TDKH.COL_KinhPhiTheoTienDo]}{firstInd + 2}:{dicData[TDKH.COL_KinhPhiTheoTienDo]}{lastInd})";
            crRowWsKH[dicData[TDKH.COL_KinhPhiDuKien]].Formula = $"ROUND({dicData[TDKH.COL_GiaTri]}{rowInd + 1}/{dicData[TDKH.COL_GiaTri]}{firstInd + 1}*{TDKH.RANGE_KinhPhiPhanBoToanDuAn}; 0)";
            crRowWsKH[dicData[TDKH.COL_SoNgayThucHien]].Formula = $"{dicData[TDKH.COL_NgayKetThuc]}{rowInd + 1}-{dicData[TDKH.COL_NgayBatDau]}{rowInd + 1} + 1";
            crRowWsKH[dicData[TDKH.COL_SoNgayThiCong]].Formula = $"{dicData[TDKH.COL_NgayKetThucThiCong]}{rowInd + 1}-{dicData[TDKH.COL_NgayBatDauThiCong]}{rowInd + 1} + 1";
            //DataTable dtHaoPhi = DinhMucHelper.fcn_GetTblHaoPhiVatTuHienTai(crRowWsKH[dic[TDKH.COL_Code]].Value.ToString());
            double HPNhanCong = DinhMucHelper.GetHPNhanCong(crRowWsKH[dic[TDKH.COL_Code]].Value.ToString());
            crRowWsKH[dicData[TDKH.COL_NhanCong]].Formula = $"ROUNDUP({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{rowInd + 1}*({HPNhanCong}); 0)";

            //        break;
            //    default:
            //        //dicData = TDKH.dic_KLHangNgay_All;
            //        //rowInd = crRowWsKH.Index;
            //        ////lastInd = wb.Range[TDKH.RANGE_KLHangNgay].BottomRowIndex;
            //        //crRowWsKH[dicData[TDKH.COL_SoNgayThucHien]].Formula = $"{dicData[TDKH.COL_NgayKetThucThiCong]}{rowInd + 1}-{dicData[TDKH.COL_NgayBatDauThiCong]}{rowInd + 1} + 1";
            //        //crRowWsKH[dicData[TDKH.COL_GiaTriThiCong]].Formula = $"{dicData[TDKH.COL_DBC_LuyKeDaThucHien]}{rowInd + 1}*{dicData[TDKH.COL_DonGiaThiCong]}{rowInd + 1} + 1";
            //        break;
            //}

        }

        public static void fcn_TDKH_DBC_ChenCotPhatSinh(string tenPhatSinh, string guid)
        {
            ////          SharedControls.spsheet_TD_KH_LapKeHoach.Document.History.IsEnabled = false;
            SharedControls.spsheet_TD_KH_LapKeHoach.BeginUpdate();

            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            Worksheet wsDoBoc = wb.Worksheets[TDKH.SheetName_DoBocChuan];
            Dictionary<string, string> dicDoBoc = MyFunction.fcn_getDicOfColumn(wb.Range[TDKH.RANGE_DoBocChuan]);
            string colChen = dicDoBoc[TDKH.COL_DBC_LuyKeDaThucHien];
            wsDoBoc.Columns.Insert(colChen, 1, ColumnFormatMode.FormatAsPrevious);
            wsDoBoc.Rows[0][colChen].SetValue($"{tenPhatSinh}_{guid}");
            wsDoBoc.Rows[1][colChen].SetValue(tenPhatSinh);

            int KeHoach = 1;

            dicDoBoc = MyFunction.fcn_getDicOfColumn(wb.Range[TDKH.RANGE_DoBocChuan]);
            Worksheet[] wss =
            {
                wsDoBoc,
                wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi],
            };

            SharedControls.spsheet_TD_KH_LapKeHoach.EndUpdate();
            try
            {
                ////          SharedControls.spsheet_TD_KH_LapKeHoach.Document.History.IsEnabled = true;

            }
            catch (Exception) { }

        }


        public static DataTable GetCongTacTheoKys(IEnumerable<string> condition, bool isLimit0 = false)
        {
            string dbString = $"SELECT cttk.*, dmct.CodePhanTuyen, pt.Ten AS TenPhanTuyen, " +
                                $"dmct.Code AS CodeDanhMucCongTac,\r\n" +
                                $"dmct.CodeHangMuc, dmct.MaHieuCongTac,\r\n" +
                                $"dmct.TenCongTac, dmct.DonVi,\r\n" +
                                $"cttk3.KhoiLuongHopDongChiTiet AS KhoiLuongHopDongToanDuAn,\r\n" +
                                $"dmct.PhatSinh,\r\n" +
                                $"TOTAL(KhoiLuongThiCong) AS {TDKH.COL_KhoiLuongDaThiCong},\r\n" +
                                $"MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) AS MaxNgayThiCong\r\n" +
                                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                                $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                                $"ON cttk.CodeCongTac = dmct.Code\r\n" +
                                $"LEFT JOIN {TDKH.Tbl_PhanTuyen} pt\r\n" +
                                $"ON dmct.CodePhanTuyen = pt.Code\r\n" +
                                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk2\r\n" +
                                $"ON (cttk.CodeNhaThau IS NOT NULL AND cttk.CodeCongTac = cttk2.CodeCongTac AND cttk.CodeGiaiDoan = cttk2.CodeGiaiDoan and cttk2.CodeNhaThau IS NULL)\r\n" +
                                $"OR (cttk.CodeNhaThau IS NULL AND cttk.Code = cttk2.Code)\r\n" +
                                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk3\r\n" +
                                $"ON (cttk.CodeNhaThau IS NOT NULL AND cttk.Code = cttk3.Code)\r\n" +
                                $"OR (cttk.CodeNhaThau IS NULL AND cttk.CodeCongTac = cttk3.CodeCongTac AND cttk.CodeGiaiDoan = cttk3.CodeGiaiDoan and cttk3.CodeNhaThau IS NOT NULL)\r\n" +
                                $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} klhn\r\n" +
                                $"ON cttk2.Code = klhn.CodeCongTacTheoGiaiDoan\r\n" +
                                $"AND klhn.KhoiLuongThiCong IS NOT NULL\r\n" +
                                $"{{0}}\r\n" +
                                $"GROUP BY cttk.Code\r\n" +
                                $"ORDER BY pt.SortId, cttk.SortId ASC {{1}}";

            string whereConditions = "";
            string Limit = "";
            if (isLimit0)
                Limit = "LIMIT 0";
            else if (condition?.Any() == true)
                whereConditions = $"WHERE {string.Join(" AND ", condition)}";

            dbString = string.Format(dbString, whereConditions, Limit);
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            return dt;
        }

        public static void fcn_TDKH_CTacTheoKyWithReorder()
        {

        }

        public static void fcn_TDKH_CTacTheoKy(out DataTable dtCongTacTheoKy, DataTable dtCT = null, DataTable dtHM = null, bool isCheckMHCT = false)
        {
            if (dtCT == null || dtHM == null)
            {
                DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
            }

            string condition = TongHopHelper.GetConditionNhaThauToDoiDoBocByGiaiDoan();

            string tasksVisible = "";
            var allPermission = BaseFrom.allPermission;

            DonViThucHien dvth = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;

            var crDuAn = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;

            if (dvth is null || crDuAn is null)
            {
                dtCongTacTheoKy = GetCongTacTheoKys(null, true);
            }
            else
            {

                if (allPermission.HaveInitProjectPermission
                    || allPermission.AllProjectThatUserIsAdmin.Contains(crDuAn.Code)
                    || allPermission.AllContractorThatUserIsAdmin.Contains(dvth.Code))
                {
                    dtCongTacTheoKy = GetCongTacTheoKys(new string[] { condition });

                }
                else if (allPermission.AllTask.Any())
                {
                    string[] lsCondition = new string[]
                    {
                        condition,
                        $"cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(allPermission.AllTask)})"
                    };
                    //dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {condition}" +
                    //    $"AND Code IN ({MyFunction.fcn_Array2listQueryCondition(allPermission.AllTask)})" +
                    //    $"ORDER BY \"SortId\" ASC";
                    dtCongTacTheoKy = GetCongTacTheoKys(lsCondition);
                }
                else
                {
                    dtCongTacTheoKy = GetCongTacTheoKys(null, true);

                    //dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} LIMIT 0";
                }
            }
            //dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


            //CUSTOMORDER
            var grs = dtCongTacTheoKy.AsEnumerable().GroupBy(x => x["CodeHangMuc"].ToString());
            foreach (var gr in grs)
            {
                int customSortId = 1;
                var ls = gr.ToList();
                ls.Where(x => x["CustomOrder"] != DBNull.Value)
                    .OrderBy(x => x["CustomOrder"]).ForEach(x => x["CustomOrder"] = customSortId++);

                ls.Where(x => x["CustomOrder"] == DBNull.Value).ForEach(x => x["CustomOrder"] = customSortId++);
                ls.Where(x => x["OriginalOrder"] == DBNull.Value).ForEach(x => x["OriginalOrder"] = x["SortId"]);
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtCongTacTheoKy, TDKH.TBL_ChiTietCongTacTheoKy);

            string strCodeCT = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray());

            if (isCheckMHCT)
            {
                string[] lsMaCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["MaHieuCongTac"].ToString()).ToArray();
                string dbString = $"SELECT MaDinhMuc FROM tbl_DinhMucAll WHERE MaDinhMuc IN ({MyFunction.fcn_Array2listQueryCondition(lsMaCongTac)})";

                DataTable dtDm = DataProvider.InstanceTBT.ExecuteQuery(dbString);

                dtCongTacTheoKy.Columns.Add("IsValidMHCT", typeof(bool));
                foreach (DataRow dr in dtCongTacTheoKy.Rows)
                {
                    var drDM = dtDm.AsEnumerable().Where(x => (string)x["MaDinhMuc"] == (string)dr["MaHieuCongTac"]).SingleOrDefault();
                    dr["IsValidMHCT"] = (drDM != null);
                }
            }
        }

        public static double fcn_TDKH_TinhLuyKe(string codeCT, bool isLoaiKy = false)
        {
            string dbString;
            DataTable dtTinhLuyKe;


            string condition = TongHopHelper.GetConditionNhaThauToDoiDoBoc();

            if (isLoaiKy)
                condition += $" AND \"CodeGiaiDoan\" != '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";

            dbString = $"SELECT \"KhoiLuongToanBo\" FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk WHERE \"CodeCongTac\" = '{codeCT}' AND {condition}";

            dtTinhLuyKe = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            double lk = 0;
            foreach (DataRow r in dtTinhLuyKe.Rows)
            {
                lk += double.Parse(r[0].ToString());
            }
            return lk;
        }

        public static bool CheckCongThucDienGiai(string value, out string val, out double KL1BP)
        {
            KL1BP = 0;
            val = value;
            try
            {
                DataTable dt = new DataTable();
                value = value.Split('=').First();

                value = value.Trim(' ');
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException();
                string[] lsName = value.Split(':');// new Char[] { ':', '=' });
                string CT = lsName.Last();

                CT = CT.Replace(".", "___");
                CT = CT.Replace(",", ".");
                CT = CT.Replace("___", "");
                CT = CT.Replace("x", "*");
                CT = CT.Replace("X", "*");
                CT = CT.Replace("%", "/100");
                var compu = dt.Compute(CT, "");
                //double kq;
                if (double.TryParse(compu.ToString(), out KL1BP))
                {
                    val = $"{value} = {KL1BP}";
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static string fcn_TDKH_UpdateRowConTheoTenCongTac_Suacongtac(Worksheet ws, int rowInd, out double KL1BP)
        {
            IWorkbook wb = ws.Workbook;
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.Range[TDKH.RANGE_DoBocChuan]);
            string sualaicongthuc = "";
            string value = ws.Rows[rowInd][dic[TDKH.COL_DanhMucCongTac]].Value.ToString();
            //bool isCTMacDinh = true;
            //if (!bool.TryParse(ws.Rows[rowInd][dic[TDKH.COL_DBC_KhoiLuongToanBoIsCongThucMacDinh]].Value.ToString(), out isCTMacDinh))
            //{
            //    ws.Rows[rowInd][dic[TDKH.COL_DBC_KhoiLuongToanBoIsCongThucMacDinh]].SetValue(true);
            //    isCTMacDinh = true;
            //}
            string code = ws.Rows[rowInd][dic[TDKH.COL_Code]].Value.ToString();

            string dbString = "";

            //if (isCTMacDinh)
            //{
            try
            {
                DataTable dt = new DataTable();

                //if (value.Contains('='))
                //{
                value = value.Split('=').First();
                //}

                value = value.Trim(' ');
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException();
                string[] lsName = value.Split(':');// new Char[] { ':', '=' });
                string CT = lsName.Last();

                CT = CT.Replace(".", "___");
                CT = CT.Replace(",", ".");
                CT = CT.Replace("___", "");
                CT = CT.Replace("x", "*");
                CT = CT.Replace("X", "*");
                CT = CT.Replace("%", "/100");
                var compu = dt.Compute(CT, "");
                //double kq;
                if (double.TryParse(compu.ToString(), out KL1BP))
                {
                    sualaicongthuc = $"{value} = {KL1BP}";
                    //if (kq != double.Parse(lsName[lsName.Length - 1]))
                    //{
                    //    ////if (lsName.Count() == 2)
                    //    //    sualaicongthuc = $"{lsName[0]}={kq}";
                    //    //else if (lsName.Count() == 3)
                    //    //    sualaicongthuc = $"{lsName[0]}:{lsName[1]}={kq}";
                    //}
                    //ws.Rows[rowInd][dic[TDKH.TENCONGTAC]].SetValueFromText(sualaicongthuc);
                    ws.Rows[rowInd][dic[TDKH.COL_DanhMucCongTac]].SetValue(sualaicongthuc);
                    ws.Rows[rowInd][dic[TDKH.COL_DBC_KL1BoPhan]].SetValue(KL1BP);
                    ws.Rows[rowInd][dic[TDKH.COL_DBC_SoBoPhanGiongNhau]].SetValue("");
                    ws.Rows[rowInd][dic[TDKH.COL_DBC_Dai]].SetValue("");
                    ws.Rows[rowInd][dic[TDKH.COL_DBC_Rong]].SetValue("");
                    ws.Rows[rowInd][dic[TDKH.COL_DBC_Cao]].SetValue("");
                    ws.Rows[rowInd][dic[TDKH.COL_DBC_HeSoCauKien]].SetValue("");
                    //ws.Rows[rowInd][dic[TDKH.COL_DBC_KL1BoPhan]].SetValue("");
                }
                dbString = $"UPDATE \"{TDKH.TBL_ChiTietCongTacCon}\" SET \"KhoiLuongMotBoPhan\" = '{ws.Rows[rowInd][dic[TDKH.COL_DBC_KL1BoPhan]].Value.NumericValue}' WHERE \"Code\" = '{code}'";
                //return sualaicongthuc;
            }
            catch (Exception ex)
            {
                dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon} WHERE \"Code\" = '{code}'";
                DataTable dtChiTiet = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                DataRow dr = dtChiTiet.Rows[0];

                int.TryParse(dr[TDKH.COL_DBC_SoBoPhanGiongNhau].ToString(), out int SBPGN);
                int.TryParse(dr[TDKH.COL_DBC_Dai].ToString(), out int Dai);
                int.TryParse(dr[TDKH.COL_DBC_Rong].ToString(), out int Rong);
                int.TryParse(dr[TDKH.COL_DBC_Cao].ToString(), out int Cao);
                int.TryParse(dr[TDKH.COL_DBC_HeSoCauKien].ToString(), out int HeSo);

                ws.Rows[rowInd][dic[TDKH.COL_DBC_SoBoPhanGiongNhau]].SetValue(SBPGN);
                ws.Rows[rowInd][dic[TDKH.COL_DBC_Dai]].SetValue(Dai);
                ws.Rows[rowInd][dic[TDKH.COL_DBC_Rong]].SetValue(Rong);
                ws.Rows[rowInd][dic[TDKH.COL_DBC_Cao]].SetValue(Cao);
                ws.Rows[rowInd][dic[TDKH.COL_DBC_HeSoCauKien]].SetValue(dr[TDKH.COL_DBC_HeSoCauKien].ToString());

                ws.Rows[rowInd][dic[TDKH.COL_DBC_KL1BoPhan]].Formula = $"PRODUCT({dic[TDKH.COL_DBC_SoBoPhanGiongNhau]}{rowInd + 1};" +
                    $"{dic[TDKH.COL_DBC_Dai]}{rowInd + 1};" +
                    $"{dic[TDKH.COL_DBC_Rong]}{rowInd + 1};" +
                    $"{dic[TDKH.COL_DBC_Cao]}{rowInd + 1};" +
                    $"{dic[TDKH.COL_DBC_HeSoCauKien]}{rowInd + 1})";
                //ws.Calculate();


                KL1BP = (double)Math.Round((double)SBPGN * Dai * Rong * Cao * HeSo, 2);
                dbString = $"UPDATE \"{TDKH.TBL_ChiTietCongTacCon}\" SET \"KhoiLuongMotBoPhan\" = '{KL1BP}' WHERE \"Code\" = '{code}'";
                sualaicongthuc = value;
                //return value;
            }
            //}
            if (DataProvider.InstanceTHDA.ExecuteNonQuery(dbString) != 1)
            {
                MessageShower.ShowInformation("Lỗi cập nhật công tác con");
                return sualaicongthuc;
            }
            return sualaicongthuc;
        }

        //public static void CapNhatThiCongHaoPhiVatTuTheoCongTac()

        /*     public static void CapNhatAllVatTuByCodesHaoPhi(IEnumerable<string> codesHaoPhi = null)
             {
                 string dbString1 = $"SELECT hp.*" +
                     $"FROM {TDKH.Tbl_HaoPhiVatTu} hp" +
                     $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk" +
                     $"ON hp.CodeCongTac = cttk.Code" +
                     $"JOIN {TDKH.TBL_DanhMucCongTac}" +
                     $"ON .CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code WHERE PhanTichKeHoach = true";

                 if (codesHaoPhi != null && codesHaoPhi.Any())
                     dbString1 += $" AND {MyConstant.view_HaoPhiVatTu}.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHaoPhi)})";

                 DataTable dthps = DataProvider.InstanceTHDA.ExecuteQuery(dbString1);

                 var codeHM = dthps.AsEnumerable().Select(x => x["CodeHangMuc"].ToString()).Distinct().ToArray();

                 dbString1 = $"DELETE" +
                     $"FROM {TDKH.TBL_KHVT_VatTu}" +
                     $"WHERE rowid IN" +
                     $"(SELECT vt2.rowid FROM {TDKH.TBL_KHVT_VatTu} vt1" +
                     $"JOIN {TDKH.TBL_KHVT_VatTu} vt2" +
                     $"ON vt1.rowid < vt2.rowid" +
                     $"AND {string.Format(TDKH.formatStringVatTu, "vt1")} = {string.Format(TDKH.formatStringVatTu, "vt2")}" +
                      $"AND {string.Format(TDKH.formatStringDonViThucHien, "vt1")} = {string.Format(TDKH.formatStringDonViThucHien, "vt2")}" +
                      //$"AND vt1.CodePhanTuyen = vt2.CodePhanTuyen" +
                      //$"AND vt1.CodeHangMuc = vt2.CodeHangMuc" +
                      $"AND vt1.CodeGiaiDoan = vt2.CodeGiaiDoan)" +

                 DataProvider.InstanceTHDA.ExecuteNonQuery(dbString1);

                 DataTable dtVTuNotValid = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT Code FROM view_VatTuNotPhanTich");
                 var codes = dtVTuNotValid.AsEnumerable().Select(x => x["Code"].ToString());
                 if (codes.Any())
                 {
                     DuAnHelper.DeleteDataRows(TDKH.TBL_KHVT_VatTu, codes);
                 }


                 //List<string>
                 foreach (string codeHangMuc in codeHM)
                 {

                     string[] searchStringFull = dthps.AsEnumerable().Select(x => $"{x["MaVatLieu"]};{x["VatTu"]};{x["DonVi"]};{x["DonGia"]};{x["LoaiVatTu"]};{x["CodeHangMuc"]};{x["CodePhanTuyen"]}").Distinct().ToArray();

                     string searchStr = MyFunction.fcn_Array2listQueryCondition(searchStringFull);

                     string dbString = $"SELECT hp.*," +
                         $"cttk.CodeNhaThau, cttk.CodeNhaThauPhu, cttk.CodeToDoi," +
                         $"vt.Code AS CodeVatTu, cttk.CodeGiaiDoan," +
                          $"{string.Format(TDKH.formatStringDonViThucHien, "cttk")} AS CodeDonViThucHien," +
                                     $"dmct.CodeHangMuc" +
                                     $"FROM {MyConstant.view_HaoPhiVatTu} hp" +
                                     $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk" +
                                     $"ON hp.CodeCongTac = cttk.Code" +
                                     $"JOIN {TDKH.TBL_DanhMucCongTac} dmct" +
                                     $"ON cttk.CodeCongTac = dmct.Code" +
                                     $"LEFT JOIN {TDKH.TBL_KHVT_VatTu} vt" +
                                     $"ON vt.CodeHangMuc = '{codeHangMuc}'" +
                                     $"AND {string.Format(TDKH.formatStringVatTu, "hp")} = {string.Format(TDKH.formatStringVatTu, "vt")}" +
                                     $"AND {string.Format(TDKH.formatStringDonViThucHien, "cttk")} = {string.Format(TDKH.formatStringDonViThucHien, "vt")}" +
                                     $"WHERE hp.PhanTichKeHoach = 1" +
                                     $"AND {string.Format(TDKH.formatStringVatTu, "hp")} IN ({searchStr}) AND dmct.CodeHangMuc = '{codeHangMuc}' ";

                     var KLHNHaoPhis = DataProvider.InstanceTHDA.ExecuteQueryModel<HPVTExtensionViewModel>(dbString);

                     var lsCodeVatTu = KLHNHaoPhis.Where(x => x.CodeVatTu != null).Select(x => x.CodeVatTu).Distinct().ToList();

                     string codesVT = MyFunction.fcn_Array2listQueryCondition(lsCodeVatTu);

                     dbString = $"SELECT {TDKH.TBL_KHVT_VatTu}.* FROM {TDKH.TBL_KHVT_VatTu}" +
                                 $"WHERE Code IN ({codesVT})";

                     DataTable dtVatTu = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


                     var grVTs = KLHNHaoPhis.GroupBy(x => new { x.SearchString, x.CodeHangMuc, x.CodeDonViThucHien });
                     foreach (var grVT in grVTs)
                     {
                         HPVTExtensionViewModel fstRecord = grVT.First();
                         string CodeVT = fstRecord.CodeVatTu;
                         DataRow drVT = dtVatTu.AsEnumerable().SingleOrDefault(x => x["Code"].ToString() == CodeVT);

                         if (drVT is null)
                         {
                             CodeVT = Guid.NewGuid().ToString();
                             lsCodeVatTu.Add(CodeVT);
                             drVT = dtVatTu.NewRow();
                             drVT["Code"] = CodeVT;
                             drVT["CodeHangMuc"] = codeHangMuc;
                             drVT["MaVatLieu"] = fstRecord.MaVatLieu;
                             drVT["MaTXHienTruong"] = fstRecord.MaTXHienTruong;
                             drVT["VatTu"] = fstRecord.VatTu;
                             drVT["DonVi"] = fstRecord.DonVi;
                             drVT["DonGia"] = fstRecord.DonGia;
                             drVT["DonGiaThiCong"] = fstRecord.DonGia;
                             drVT["LoaiVatTu"] = fstRecord.LoaiVatTu;
                             drVT["CodeNhaThau"] = fstRecord.CodeNhaThau;
                             drVT["CodeNhaThauPhu"] = fstRecord.CodeNhaThauPhu;
                             drVT["CodeToDoi"] = fstRecord.CodeToDoi;
                             drVT["CodeGiaiDoan"] = fstRecord.CodeGiaiDoan;
                             drVT["CodePhanTuyen"] = fstRecord.CodePhanTuyen;

                             dtVatTu.Rows.Add(drVT);
                         }
                         drVT["CodeHangMuc"] = grVT.Key.CodeHangMuc;
                         //DataRow[] crDrs = KLHNsVatTuAll.AsEnumerable().Where(x => x["CodeVatTu"].ToString() == CodeVT).ToArray();

                         //if (!grVT.Any(x => x.Ngay != null))
                         //    continue;

                         drVT["NgayBatDau"] = grVT.Min(x => x.NgayBatDau);
                         drVT["NgayKetThuc"] = grVT.Max(x => x.NgayKetThuc);
                         drVT["KhoiLuongKeHoach"] = grVT.Sum(x => x.KhoiLuongKeHoach);
                         drVT["KhoiLuongHopDong"] = grVT.Sum(x => x.KhoiLuongHopDong);
     *//*                    //drVT["KhoiLuongKeHoach"] = KLHNHaoPhis.Sum
                         DateTime NBD = DateTime.Parse(drVT["NgayBatDau"].ToString());
                         DateTime NKT = DateTime.Parse(drVT["NgayKetThuc"].ToString());*/

        /*//for (DateTime date = NBD; date <= NKT; date = date.AddDays(1))
        //{
        //    string dateStr = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        //    DataRow rowKLHN = crDrs.AsEnumerable().FirstOrDefault(x => x["CodeVatTu"].ToString() == fstRecord.CodeVatTu && x["Ngay"].ToString() == dateStr);
        //    if (rowKLHN is null)
        //    {
        //        rowKLHN = KLHNsVatTuAll.NewRow();
        //        KLHNsVatTuAll.Rows.Add(rowKLHN);

        //        rowKLHN["Code"] = Guid.NewGuid().ToString();
        //        rowKLHN["CodeVatTu"] = CodeVT;
        //        rowKLHN["Ngay"] = dateStr;
        //    }

        //    double KLKH = grVT.Where(x => x.Ngay == dateStr).Sum(x => x.KhoiLuongKeHoach) ?? 0;

        //    rowKLHN["KhoiLuongKeHoach"] = KLKH;
        //    rowKLHN["ThanhTienKeHoach"] = Math.Round(fstRecord.DonGia * KLKH);

        //    if (rowKLHN["IsSumThiCong"].ToString() == true.ToString())
        //    {
        //        double? KLTC = grVT.Where(x => x.Ngay == dateStr).Sum(x => x.KhoiLuongThiCong);
        //        //double TTTC = grVT.Where(x => x.Ngay == dateStr).Sum(x => x.KhoiLuongThiCong) ;
        //        if (KLTC.HasValue)
        //        {
        //            if (KLTC.HasValue)
        //            {
        //                rowKLHN["KhoiLuongThiCong"] = KLTC.Value;
        //                rowKLHN["ThanhTienThiCong"] = grVT.Where(x => x.Ngay == dateStr).Sum(x => x.ThanhTienThiCong) ?? 0;
        //            }
        //            else
        //                rowKLHN["KhoiLuongThiCong"] = rowKLHN["ThanhTienThiCong"] = DBNull.Value;
        //        }
        //    }
        //}

        //crDrs.AsEnumerable().Where(x => DateTime.Parse(x["Ngay"].ToString()) < NBD
        //|| DateTime.Parse(x["Ngay"].ToString()) > NKT).ForEach(x =>
        //{
        //    x["KhoiLuongKeHoach"] = x["ThanhTienKeHoach"] = DBNull.Value;
        //});

        //if (crDrs.AsEnumerable().Any(x => x["KhoiLuongThiCong"] != DBNull.Value))
        //{
        //    drVT["NgayBatDauThiCong"] = crDrs.AsEnumerable().Min(x => x["Ngay"]);
        //    drVT["NgayKetThucThiCong"] = crDrs.AsEnumerable().Max(x => x["Ngay"]);
        //}*//*
    }

    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtVatTu, TDKH.TBL_KHVT_VatTu);
    //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(KLHNsVatTuAll, TDKH.TBL_KHVT_KhoiLuongHangNgay);
    //MyFunction.Fcn_CalKLKHNew(TypeKLHN.VatLieu, lsCodeVatTu.ToArray());
}


}*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codesHaoPhi"></param>
        /// <param name="MaVatTu">Trường hợp đổi tên</param>
        /// <param name="tenVatTu">Trường hợp đổi mã</param>
        public static void CapNhatAllVatTuHaoPhi(IEnumerable<string> codesHaoPhi = null, IEnumerable<string> MaVatTu = null, IEnumerable<string> tenVatTu = null, IEnumerable<string> CodesHangMuc = null, IEnumerable<string> CodesPhanTuyen = null, IEnumerable<string> CodesCongTrinh = null)
        {
            if ((MaVatTu?.Any() == true || tenVatTu?.Any() == true) && CodesHangMuc is null && CodesPhanTuyen is null)
            {
                AlertShower.ShowInfo("Vui lòng truyền Code Hạng mục và phân tuyến kèm theo MaVatTu, TenVatTu");
                MaVatTu = null;
                tenVatTu = null;
            }
            WaitFormHelper.ShowWaitForm("Đang cập nhật lại vật tư");
            if (codesHaoPhi is null && MaVatTu is null && CodesHangMuc is null && CodesPhanTuyen is null && CodesCongTrinh is null)
                DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.UpdateVatTu", isSQLFile: true, AddToDeletedDataTable: false);
            else
            {
                List<string> listCondition = new List<string>();
                if (codesHaoPhi != null)
                {
                    listCondition.Add($"vt1.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesHaoPhi)})");
                }

                if (MaVatTu != null)
                {
                    listCondition.Add($"vt1.MaVatLieu IN ({MyFunction.fcn_Array2listQueryCondition(MaVatTu)})");
                }

                if (CodesHangMuc != null)
                    listCondition.Add($"ct.CodeHangMuc IN ({MyFunction.fcn_Array2listQueryCondition(CodesHangMuc)})");

                if (CodesPhanTuyen != null)
                    listCondition.Add($"ct.CodePhanTuyen IN ({MyFunction.fcn_Array2listQueryCondition(CodesPhanTuyen)})");

                if (CodesCongTrinh != null)
                    listCondition.Add($"ct.CodeCongTrinh IN ({MyFunction.fcn_Array2listQueryCondition(CodesCongTrinh)})");
                var conds = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("--Condition1", $"AND {string.Join(" AND ", listCondition)}")
                };
                DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.UpdateVatTuWithCondition", isSQLFile: true,
                    AddToDeletedDataTable: false, Conditions: conds);

            }

            WaitFormHelper.CloseWaitForm();

        }

        public static void TinhKLKHVatTuOnly(IEnumerable<string> codesCtac)
        {
            
        }
        public static void TinhLaiToanBoKhoiLuongKeHoach(IEnumerable<string> idsCongTacTheoKy, bool isPushData = true)
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhật kế hoạch công tác");

            //string dbString = $"SELECT hp.* " +
            //    $"FROM {MyConstant.view_HaoPhiVatTu} hp" +
            //    $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk" +
            //    $"ON hp.CodeCongTac = cttk.Code ";


            //if (idsCongTacTheoKy != null)
            //{
            //    dbString += $" WHERE hp.CodeCongTac IN ({MyFunction.fcn_Array2listQueryCondition(idsCongTacTheoKy)})";
            //}

            //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            //var codesHp = dt.AsEnumerable().Select(x => x["Code"].ToString());

            ////WaitFormHelper.ShowWaitForm("Đang cập nhật kế hoạch vật liệu");
            //CapNhatAllVatTuHaoPhi(codesHp);


            string dbString = $"SELECT COALESCE(dmct.CodeHangMuc,cttk.CodeHangMuc) AS CodeHangMuc \r\n" +
                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                $"ON cttk.CodeCongTac = dmct.Code\r\n" +
                $"WHERE cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(idsCongTacTheoKy)})";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            CapNhatAllVatTuHaoPhi(CodesHangMuc: dt.AsEnumerable().Select(x => x.Field<string>("CodeHangMuc")));
            WaitFormHelper.CloseWaitForm();
        }
        public static void TinhLaiToanBoKhoiLuongHopDong(IEnumerable<string> idsCongTacTheoKy = null)
        {
            //WaitFormHelper.ShowWaitForm("Đang cập nhật kế hoạch công tác");
            //string dbString = $"SELECT Code FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            //if (idsCongTacTheoKy != null)
            //{
            //    dbString += $" WHERE Code IN ({MyFunction.fcn_Array2listQueryCondition(idsCongTacTheoKy)})";
            //}


            //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, dt.AsEnumerable().Select(x => x[0].ToString()).ToArray());

            //WaitFormHelper.ShowWaitForm("Đang cập nhật kế hoạch hao phí");

            //dbString = $"SELECT Code FROM {TDKH.Tbl_HaoPhiVatTu}";
            //if (idsCongTacTheoKy != null)
            //{
            //    dbString += $" WHERE CodeCongTac IN ({MyFunction.fcn_Array2listQueryCondition(idsCongTacTheoKy)})";
            //}

            //dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


            //var codesHaoPhi = dt.AsEnumerable().Select(x => x[0].ToString()).ToArray();
            //MyFunction.Fcn_CalKLKHNew(TypeKLHN.HaoPhiVatTu, codesHaoPhi);

            //WaitFormHelper.ShowWaitForm("Đang cập nhật kế hoạch vật liệu");
            //CapNhatAllVatTuByCodesHaoPhi(codesHaoPhi);
            //WaitFormHelper.CloseWaitForm();
        }

        public static void TinhLaiToanBoKhoiLuongKeHoachDuAn(IEnumerable<string> idsDuAn)
        {
            if (!idsDuAn.Any())
            {
                MessageShower.ShowError("Chưa cung cấp id dự án!");
                return;
            }

            string idQuery = MyFunction.fcn_Array2listQueryCondition(idsDuAn);

            string dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.Code\r\n" +
                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy}\r\n" +
                $"JOIN {TDKH.TBL_DanhMucCongTac}\r\n" +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC}\r\n" +
                $"ON {TDKH.TBL_DanhMucCongTac}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH}\r\n" +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code\r\n" +
                $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn IN ({idQuery})";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var idsCongTac = dt.AsEnumerable().Select(x => x[0].ToString());
            TinhLaiToanBoKhoiLuongKeHoach(idsCongTac);
        }

        public static DateTime GetNextDate(List<DateTime> NgayNghis, DateTime date)
        {
            while (NgayNghis.Contains(date))
            {
                date = date.AddDays(1);
            }
            return date;
        }

        public static DateTime GetPrevDate(List<DateTime> NgayNghis, DateTime date)
        {
            while (NgayNghis.Contains(date))
            {
                date = date.AddDays(-1);
            }
            return date;
        }
        public static List<DateTime> GetNgayNghiHangMuc(string codeHangMuc, DateTime minDate, DateTime maxDate)
        {
            string NBDString = minDate.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string NKTString = maxDate.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

            var result = new List<DateTime>();
            string dbString = $"SELECT nn.*\r\n" +
                $"FROM {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ct\r\n" +
                $"ON hm.CodeCongTrinh = ct.Code\r\n" +
                $"JOIN {TDKH.Tbl_Ngaynghi} nn\r\n" +
                $"ON nn.CodeHangMuc = hm.Code\r\n" +
                $"OR nn.CodeCongTrinh = ct.Code\r\n" +
                $"OR nn.CodeDuAn = ct.CodeDuAn\r\n" +
                $"WHERE hm.Code == '{codeHangMuc}'\r\n" +
                $"AND ((NgayTrongTuan IS NOT NULL) OR\r\n" +
                $"Ngay >= '{NBDString}' AND Ngay <= '{NKTString}')";

            var NgayNghis = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_TDKH_NgayNghiViewModel>(dbString);

            return GetNgayNghisFromModel(NgayNghis, minDate, maxDate);

        }

        public static List<DateTime> GetNgayNghisFromModel(List<Tbl_TDKH_NgayNghiViewModel> NgayNghis, DateTime minDate, DateTime maxDate)
        {
            var result = new List<DateTime>();

            var NgayTrongTuan = new List<DayOfWeek>();
            foreach (var nn in NgayNghis)
            {
                if (nn.NgayTrongTuan != null)
                {
                    string[] lsDayofWeek = nn.NgayTrongTuan.ToString().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in lsDayofWeek)
                    {
                        if (!NgayTrongTuan.Contains(MyConstant.dicDayOfWeek[str]))
                        {
                            NgayTrongTuan.Add(MyConstant.dicDayOfWeek[str]);
                        }
                    }
                }

                if (nn.Ngay != null)
                {
                    result.Add(nn.Ngay.Value);
                }
            }

            for (DateTime date = minDate; date <= maxDate; date = date.AddDays(1))
            {
                if (NgayTrongTuan.Contains(date.DayOfWeek))
                    result.Add(date);
            }

            return result;
        }

        public static List<DateTime> GetNgayNghiOfCongTac(TypeKLHN type, string code)
        {
            var result = new List<DateTime>();
            string dbString = $"SELECT {TDKH.Tbl_Ngaynghi}.*\r\n" +
                $"{{0}}" +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC}\r\n" +
                $"ON {{1}}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH}\r\n" +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code\r\n" +
                $"JOIN {TDKH.Tbl_Ngaynghi}\r\n" +
                $"ON {TDKH.Tbl_Ngaynghi}.CodeCongTac = {{2}}.Code\r\n" +
                $"OR {TDKH.Tbl_Ngaynghi}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code\r\n" +
                $"OR {TDKH.Tbl_Ngaynghi}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code\r\n" +
                $"OR {TDKH.Tbl_Ngaynghi}.CodeDuAn = {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn\r\n" +
                $"WHERE {{2}}.Code = '{code}'\r\n" +
                $"AND (NgayTrongTuan IS NOT NULL OR\r\n" +
                $"(Ngay >= {{2}}.NgayBatDau AND Ngay <= {{2}}.NgayKetThuc)";

            string format0;
            string tblMain;
            switch (type)
            {
                case TypeKLHN.Nhom:
                    format0 = $"FROM {TDKH.TBL_NhomCongTac}\r\n";
                    tblMain = TDKH.TBL_NhomCongTac;
                    dbString = string.Format(dbString, format0, TDKH.TBL_NhomCongTac, tblMain);
                    break;
                case TypeKLHN.CongTac:
                    format0 = $"FROM {TDKH.TBL_ChiTietCongTacTheoKy}\r\n" +
                                $"JOIN {TDKH.TBL_DanhMucCongTac}\r\n" +
                                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code ";
                    tblMain = TDKH.TBL_ChiTietCongTacTheoKy;
                    dbString = string.Format(dbString, format0, TDKH.TBL_DanhMucCongTac, tblMain);
                    break;
                case TypeKLHN.HaoPhiVatTu:
                    format0 = $"FROM {TDKH.Tbl_HaoPhiVatTu}\r\n" +
                        $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy}\r\n" +
                        $"ON {TDKH.Tbl_HaoPhiVatTu}.CodeCongTac = {TDKH.TBL_ChiTietCongTacTheoKy}.Code\r\n" +
                        $"JOIN {TDKH.TBL_DanhMucCongTac}\r\n" +
                        $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code ";
                    tblMain = TDKH.Tbl_HaoPhiVatTu;

                    dbString = string.Format(dbString, format0, TDKH.TBL_DanhMucCongTac, tblMain);

                    break;
                case TypeKLHN.VatLieu:
                    format0 = $"FROM {TDKH.TBL_KHVT_VatTu} ";
                    tblMain = TDKH.TBL_KHVT_VatTu;

                    dbString = string.Format(dbString, format0, TDKH.TBL_KHVT_VatTu, tblMain);
                    break;

                default:
                    return result;
            }            //List<TDKH_NgayNghiViewModel>


            List<Tbl_TDKH_NgayNghiViewModel> NgayNghis = new List<Tbl_TDKH_NgayNghiViewModel>();// DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_TDKH_NgayNghiViewModel>(dbString);


            dbString = $"SELECT NgayBatDau AS MinDate, NgayKetThuc AS MaxDate\r\n" +
                $"FROM {tblMain} WHERE Code = '{code}'";

            DataRow drMinMaxDate = DataProvider.InstanceTHDA.ExecuteQuery(dbString).AsEnumerable().SingleOrDefault();

            if (drMinMaxDate is null || !DateTime.TryParse(drMinMaxDate["MinDate"].ToString(), out DateTime minDate) || !DateTime.TryParse(drMinMaxDate["MaxDate"].ToString(), out DateTime maxDate))
                return result;


            return GetNgayNghisFromModel(NgayNghis, minDate, maxDate);
        }

        public static List<DateTime> CalcNgayNghiBetweenDates(TypeKLHN type, string code, DateTime NBD, DateTime NKT)
        {
            List<DateTime> result = GetNgayNghiOfCongTac(type, code);
            return result.Where(x => x >= NBD && x <= NKT).ToList();
        }

        public static void CheckAnHienDienGiai()
        {
            WaitFormHelper.ShowWaitForm("Đang tải");
            bool isChecked = SharedControls.cb_TDKH_HienDienGiai.Checked;
            Worksheet ws = SharedControls.spsheet_TD_KH_LapKeHoach.Document.Worksheets[TDKH.SheetName_DoBocChuan];
            ////          ws.Workbook.History.IsEnabled = false;
            ws.Workbook.BeginUpdate();
            var dicDoBoc = MyFunction.fcn_getDicOfColumn(ws.Range[TDKH.RANGE_DoBocChuan]);

            ws.Columns[dicDoBoc[TDKH.COL_TypeRow]].Search(MyConstant.TYPEROW_CVCON, MyConstant.MySearchOptions)
                .Select(x => ws.Rows[x.RowIndex])
                .ForEach(x => x.Visible = isChecked);

            ws.Columns[dicDoBoc[TDKH.COL_TypeRow]].Search(MyConstant.TYPEROW_NhomDienGiai, MyConstant.MySearchOptions)
                .Select(x => ws.Rows[x.RowIndex])
                .ForEach(x => x.Visible = isChecked);
            WaitFormHelper.CloseWaitForm();


            ws.Workbook.EndUpdate();
            try
            {
                ////          ws.Workbook.History.IsEnabled = true;

            }
            catch (Exception) { }


        }

        public static void CheckAnHienPhanDoan()
        {
            WaitFormHelper.ShowWaitForm("Đang tải");
            bool isChecked = SharedControls.ce_TDKH_HienHTPD.Checked;
            Worksheet ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
            ////          ws.Workbook.History.IsEnabled = false;

            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            switch (ws.Name)
            {
                case TDKH.SheetName_DoBocChuan:
                    //dic = MyFunction.fcn_getDicOfColumn(ws.Range[TDKH.RANGE_DoBocChuan]);
                    break;
                case TDKH.SheetName_KeHoachKinhPhi:
                    //dic = dic;
                    break;
                case TDKH.SheetName_VatLieu:
                case TDKH.SheetName_NhanCong:
                case TDKH.SheetName_MayThiCong:
                    //dic = dicKPVL_All;
                    break;
                case TDKH.SheetName_TongHopHaoPhi:
                    //dic = TDKH.dic_FullVatTuAll;
                    break;
                default:

                    //ws.Workbook.EndUpdate();
                    return;
            }    

            ws.Workbook.BeginUpdate();
            ws.Columns[dic[TDKH.COL_TypeRow]].Search(MyConstant.TYPEROW_HTPhanTuyen, MyConstant.MySearchOptions)
                .Select(x => ws.Rows[x.RowIndex])
                .ForEach(x => x.Visible = isChecked);

            WaitFormHelper.CloseWaitForm();


            ws.Workbook.EndUpdate();
            try
            {
                ////          ws.Workbook.History.IsEnabled = true;

            }
            catch (Exception) { }
        }

        public static void CheckAnHienCongTacVatTu(Worksheet ws, bool isChecked, Dictionary<string, string> dicDoBoc = null)
        {
            WaitFormHelper.ShowWaitForm("Đang tải");
            //bool isChecked = SharedControls.cb_TDKH_HienCongTac.Checked;
            //IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            //if (ws is null)
            //ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;

            ////          ws.Workbook.History.IsEnabled = false;
            ws.Workbook.BeginUpdate();

            if (dicDoBoc is null)
            {
                int type = Array.IndexOf(TDKH.sheetsName, ws.Name);
                dicDoBoc = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            }

            var cells = ws.Columns[dicDoBoc[TDKH.COL_TypeRow]].Search(MyConstant.TYPEROW_CVCON, MyConstant.MySearchOptions);

            if (!cells.Any() && isChecked)
            {
                string name = ws.Name;
                switch (name)
                {

                    case TDKH.SheetName_VatLieu:
                    case TDKH.SheetName_NhanCong:
                    case TDKH.SheetName_MayThiCong:
                        TDKHHelper.LoadVatTuFull((DoBocVatTu)Array.IndexOf(TDKH.sheetsName, name));
   
                        break;
                    case TDKH.SheetName_TongHopHaoPhi:
                        TDKHHelper.LoadTongHopVatTuKeHoach();
                        break;
                    default:
                        break;
                }
            }

            cells.Select(x => ws.Rows[x.RowIndex])
                .ForEach(x => x.Visible = isChecked);

            //ws.Columns[dicDoBoc[TDKH.COL_TypeRow]].Search(MyConstant.TYPEROW_NhomDienGiai, MyConstant.MySearchOptions)
            //    .Select(x => ws.Rows[x.RowIndex])
            //    .ForEach(x => x.Visible = isChecked);
            WaitFormHelper.CloseWaitForm();

            ws.Workbook.EndUpdate();
            try
            {
                ////          ws.Workbook.History.IsEnabled = true;

            }
            catch (Exception) { }
        }

        public static void DongBoDuLieuTDKH()
        {
            string dbString = $"SELECT ct1.*\r\n" +
                                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} ct1\r\n" +
                                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} ct2\r\n" +
                                $"ON ct2.CodeCongTac = ct1.CodeCongTac AND ct2.CodeGiaiDoan = ct1.CodeGiaiDoan AND ct1.Code != ct2.Code\r\n" +
                                $"WHERE ct2.Code IS NULL AND ct1.CodeNhaThau IS NULL";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] codes = dt.AsEnumerable().Select(x => (string)x["Code"]).ToArray();

            if (codes.Any())
            {
                DuAnHelper.DeleteDataRows(TDKH.TBL_ChiTietCongTacTheoKy, codes);
            }

            dbString = $"SELECT dmct.*\r\n" +
                $"FROM {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} ct\r\n" +
                $"ON dmct.Code = ct.CodeCongTac\r\n" +
                $"WHERE ct.Code IS NULL";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            codes = dt.AsEnumerable().Select(x => (string)x["Code"]).ToArray();

            if (codes.Any())
            {
                DuAnHelper.DeleteDataRows(TDKH.TBL_DanhMucCongTac, codes);
            }
        }

        public static DataTable GetCongTacWithTheSameHMAndContractorWith(string codeCongTac)
        {
            string dbString = $"SELECT cttkRef.*\r\n" +
                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttkOri\r\n" +
                $"JOIN {TDKH.TBL_DanhMucCongTac} dmctOri\r\n" +
                $"ON cttkOri.CodeCongTac = dmctOri.Code\r\n" +
                $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkRef\r\n" +
                $"ON COALESCE(cttkOri.CodeNhaThau, cttkOri.CodeNhaThauPhu, cttkOri.CodeToDoi) = COALESCE(cttkRef.CodeNhaThau, cttkRef.CodeNhaThauPhu, cttkRef.CodeToDoi)\r\n" +
                $"JOIN {TDKH.TBL_DanhMucCongTac} dmctRef\r\n" +
                $"ON cttkRef.CodeCongTac = dmctRef.Code\r\n" +
                $"AND dmctOri.CodeHangMuc = dmctRef.CodeHangMuc\r\n" +
                $"WHERE cttkOri.Code = '{codeCongTac}' ";
            return DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        }

        public static int? FindIndHangMuc(CellRange range, Dictionary<string, string> dic)
        {
            Worksheet ws = range.Worksheet;
            var indColTypeRow = ws.Columns[dic[TDKH.COL_TypeRow]].Index;
            var cell = ws.Range.FromLTRB(indColTypeRow, range.TopRowIndex, indColTypeRow, ws.SelectedCell.TopRowIndex)
            .Search(MyConstant.TYPEROW_HangMuc, MyConstant.MySearchOptions).LastOrDefault();


            return cell?.RowIndex;
        }

        public static void GetCodeCongTrinhHangMuc(out string CodeHM, out string CodeCT)
        {
            var cthm = SharedControls.slke_ChonCTHM.EditValue as string;
            CodeHM = null;
            CodeCT = null;
            if (cthm is null || cthm == "ALL")
            {
                return;
            }

            if (cthm.StartsWith("ALL_"))
            {
                CodeCT = cthm.Replace("ALL_", "");
                return;
            }
            CodeHM = cthm;


        }
        public static string GetDbStringCongTacWithHangMucPhanTuyen(string codeDA, string CodeCongTrinh = null, string CodeHangMuc = null, string codeGiaiDoan = null, string conditionDVTH = null, string conditionCTTK = null, 
            string conditionDMCT = null, string whereCondition = null, bool GetAllHM = true, DateTime? NBD = null, DateTime? NKT = null,string ConditonNKH=null)  
        {
            string dbString = $"" +
                $"WITH TempTC AS\r\n" +
                $"(" +
                $"SELECT ct.Code,\r\n" +
                $"TOTAL(klhn.KhoiLuongThiCong) AS KhoiLuongDaThiCong,\r\n" +
                    $"MAX(klhn.Ngay) AS NgayKetThucThiCong,\r\n" +
                    $"MIN(klhn.Ngay) AS NgayBatDauThiCong\r\n" +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} ct\r\n" +
                    $"JOIN {MyConstant.view_CongTacKeHoachThiCong} cttc\r\n" +
                    $"ON ct.CodeNhaThau IS NOT NULL AND ct.Code = cttc.CodeGiaoThau\r\n" +
                    $"JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn\r\n" +
                    $"ON cttc.Code = klhn.CodeCongTacTheoGiaiDoan AND KhoiLuongThiCong IS NOT NULL\r\n" +
                    $"GROUP BY ct.Code\r\n" +
                    $"UNION ALL\r\n" +
                    $"SELECT ct.Code,\r\n" +
                $"TOTAL(klhn.KhoiLuongThiCong) AS KhoiLuongDaThiCong,\r\n" +
                    $"MAX(klhn.Ngay) AS NgayKetThucThiCong,\r\n" +
                    $"MIN(klhn.Ngay) AS NgayBatDauThiCong\r\n" +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} ct\r\n" +
                    $"JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} klhn\r\n" +
                    $"ON ct.CodeNhaThau IS NULL AND ct.Code = klhn.CodeCongTacTheoGiaiDoan AND KhoiLuongThiCong IS NOT NULL\r\n" +
                    $"GROUP BY ct.Code\r\n" +
                    $"" +
                $")" +
                $"\r\n" +
                $"SELECT COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi, klhn.KhoiLuongDaThiCong, klhn.KhoiLuongDaThiCong*cttk.DonGiaThiCong AS ThanhTienDaThiCong, klhn.NgayBatDauThiCong, klhn.NgayKetThucThiCong," +
                    
                    $"COALESCE(hmpt.Code,cttk.CodeHangMuc) AS CodeHangMuc," +
                    $" nhom.Ten AS TenNhom, " +
                    $"dmct.Code AS CodeDanhMucCongTac,dmct.GhiChuBoSungJson,\r\n" +
                    $" dmct.CodeGop, \r\n" +
                    $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
                    $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                    $"COALESCE(cttk.KyHieuBanVe, dmct.KyHieuBanVe) AS KyHieuBanVe, " +
                    $"COALESCE(cttk.KhoiLuongHopDongToanDuAn, dmct.KhoiLuongHopDongToanDuAn) AS KhoiLuongHopDongToanDuAn, " +
                    $"COALESCE(cttk.PhatSinh, dmct.PhatSinh) AS PhatSinh, " +
                    $"COALESCE(dmct.HasHopDongAB,cttk.HasHopDongAB) AS HasHopDongAB, " +
                    $"COALESCE(cttk.PhanTichVatTu, dmct.PhanTichVatTu) AS PhanTichVatTu, " +
                    $"COALESCE(cttk.CodeGop, dmct.CodeGop) AS CodeGop, " +
                    //$"COALESCE(cttk.CodePhanTuyen, dmct.CodePhanTuyen) AS CodePhanTuyen, " +
                    $"hmpt.CodePhanTuyen, hmpt.TenPhanTuyen,cttk.*,cttkgt.NgayBatDau as NBDGT,cttkgt.NgayKetThuc as NKTGT,\r\n" +

                    $"ctrinh.Code AS CodeCongTrinh, ctrinh.Ten AS TenCongTrinh,\r\n" +
                    $"hmpt.Ten AS TenHangMuc, NULL AS KhoiLuongHopDongDuAn,\r\n" +
                    $"TOTAL(hp.HeSoNguoiDung*hp.DinhMucNguoiDung)*cttk.KhoiLuongToanBo AS NhanCongReal, dmct.HasHopDongAB, " +
                    $"dmct.PhanTichVatTu, tgct.Ten AS TenGopCongTac, tgnhom.Ten AS TenGopNhom, tgpd.Ten AS TenGopPhanDoan\r\n, " +
                    $"dvth.Code AS CodeDVTH, dvth.Ten AS TenDVTH, dvth.DonViTrucThuoc, dvth.CodeTongThau, " +
                    $"Mui.Code as CodeMuiThiCong,Mui.Ten as MuiThiCong,\r\n" +
                 
                    $"ddtdCT.Ten as TenDDCT,ddtdNhom.Ten as TenDDNhom,ddtdTuyen.Ten as TenDDTuyen\r\n" +

                    $"FROM {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                    $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                    $"ON da.Code = ctrinh.CodeDuAn\r\n" +
                    ((codeDA.HasValue()) ? $"AND da.Code = '{codeDA}'\r\n" : "") +
                    $"" +
                    $"JOIN {MyConstant.view_HangMucWithPhanTuyen} hmpt\r\n" +
                    $"ON hmpt.CodeCongTrinh = ctrinh.Code\r\n" +
                    ((CodeCongTrinh.HasValue()) ? $"AND ctrinh.Code = '{CodeCongTrinh}'\r\n" : "") +
                    ((CodeHangMuc.HasValue()) ? $"AND hmpt.Code = '{CodeHangMuc}'\r\n" : "") +

                    ((GetAllHM) ? "LEFT ": "") + $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    $"ON (hmpt.Code = dmct.CodeHangMuc AND ((dmct.CodePhanTuyen IS NOT NULL AND hmpt.CodePhanTuyen = dmct.CodePhanTuyen) OR (dmct.CodePhanTuyen IS NULL AND hmpt.CodePhanTuyen IS NULL)))\r\n" +
                    //$"OR (dmct.CodePhanTuyen IS NOT NULL AND dmct.CodePhanTuyen = hmpt.CodePhanTuyen)) \r\n" +
                    ((conditionDMCT.HasValue()) ? $"AND ({conditionDMCT})\r\n" : "") +

                    ((GetAllHM) ? "LEFT " : "") + $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"ON (dmct.Code = cttk.CodeCongTac OR (cttk.CodeCongTac IS NULL AND cttk.CodeHangMuc = hmpt.Code\r\n" +
                    $"AND ((cttk.CodePhanTuyen IS NOT NULL AND cttk.CodePhanTuyen = hmpt.CodePhanTuyen)\r\n" +
                    $" OR (cttk.CodePhanTuyen IS NULL AND hmpt.CodePhanTuyen IS NULL)))) AND cttk.CodeCha IS NULL\r\n" +

                    ((conditionCTTK.HasValue()) ? $"AND ({conditionCTTK})\r\n" : "") +
                    ((NBD.HasValue) ? ConditonNKH.HasValue()? $"AND ((cttk.NgayBatDau<'{NKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                    $"AND cttk.NgayKetThuc>='{NKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}') OR (cttk.Code IN ({ConditonNKH}))) \r\n":
                    $"AND cttk.NgayBatDau<='{NKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND cttk.NgayKetThuc>='{NKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'\r\n" : "") +

                    ((conditionDVTH.HasValue()) ? $"AND ({conditionDVTH})\r\n" : "") +
                    ((codeGiaiDoan.HasValue()) ? $"AND cttk.CodeGiaiDoan = '{codeGiaiDoan}'\r\n" : "") +


                    $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkgt\r\n" +
                    $"ON cttkgt.CodeCongTac = cttk.CodeCongTac AND cttkgt.CodeNhaThau IS NOT NULL\r\n" + 

                    $"LEFT JOIN {TDKH.Tbl_TDKH_MuiThiCong} Mui\r\n" +
                    $"ON cttk.CodeMuiThiCong = Mui.Code\r\n" +

                    $"LEFT JOIN {TDKH.TBL_NhomCongTac} nhom\r\n" +
                    $"ON cttk.CodeNhom = nhom.Code\r\n" +
                    $"LEFT JOIN {TDKH.Tbl_TenDienDaiTuDo} ddtdNhom\r\n" +
                    $"ON nhom.CodeDienDai = ddtdNhom.Code \r\n" +
                    $"LEFT JOIN {TDKH.Tbl_TenDienDaiTuDo} ddtdTuyen\r\n" +
                    $"ON hmpt.CodeDienDai = ddtdTuyen.Code \r\n" +
                    $"LEFT JOIN {TDKH.Tbl_TenDienDaiTuDo} ddtdCT\r\n" +
                    $"ON dmct.CodeDienDai = ddtdCT.Code \r\n" +
                    $"LEFT JOIN {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                    $"ON hp.CodeCongTac = cttk.Code AND hp.LoaiVatTu = 'Nhân công'\r\n" +
                    $"LEFT JOIN {TDKH.TBL_TenGopCongTac} tgct\r\n" +
                    $"ON (dmct.CodeGop = tgct.Code OR cttk.CodeGop = tgct.Code )\r\n" +
                    $"LEFT JOIN {TDKH.TBL_TenGopNhom} tgnhom\r\n" +
                    $"ON nhom.CodeGop = tgNhom.Code\r\n" +
                    $"LEFT JOIN {TDKH.TBL_TenGopPhanDoan} tgpd\r\n" +
                    $"ON hmpt.CodeGop = tgpd.Code\r\n" +
                    $"LEFT JOIN {MyConstant.view_DonViThucHien} dvth\r\n" +
                    $"ON COALESCE(cttk.CodeNhaThau, cttk.CodeNhaThauPhu, cttk.CodeToDoi, '') = dvth.Code\r\n" +
                    $"LEFT JOIN tempTC klhn\r\n" +
                    $"ON cttk.Code = klhn.Code\r\n" +

                    //$"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk2\r\n" +
                    //$"ON cttk2.CodeNhaThau IS NULL AND cttk.CodeNhaThau IS NOT NULL AND cttk2.CodeCongTac = cttk.CodeCongTac\r\n" +
                    $"{{0}}\r\n" +
                    $"GROUP BY hmpt.Code, hmpt.CodePhanTuyen, cttk.Code \r\n" +
                    $"ORDER BY ctrinh.SortId, hmpt.SortId, cttk.SortId ";

            var allPermission = BaseFrom.allPermission;

            List<string> whereConditions = new List<string>();
            if (whereCondition.HasValue())
                whereConditions.Add(whereCondition);


            //if (!BaseFrom.allPermission.HaveInitProjectPermission)
            //    whereConditions.Add($"cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(BaseFrom.allPermission.AllTask)})");

            string whereCond = "";
            if (allPermission.HaveInitProjectPermission
                    || allPermission.AllProjectThatUserIsAdmin.Contains(codeDA)
                   )
            {
                //dtCongTacTheoKy = GetCongTacTheoKys(new string[] { condition });

            }
            else if (allPermission.AllContractor.Any())
            {
                
                    whereConditions.Add($"(dvth.Code IN ({MyFunction.fcn_Array2listQueryCondition(allPermission.AllContractorThatUserIsAdmin)}) OR cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(allPermission.AllTask)}))");
                
            }    
            else if (allPermission.AllTask.Any())
            {
                //string[] lsCondition = new string[]
                //{
                //condition,
                whereConditions.Add($"cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(allPermission.AllTask)})");
                //};

                //dtCongTacTheoKy = GetCongTacTheoKys(lsCondition);
            }
            //else
            //{
            //    //dtCongTacTheoKy = GetCongTacTheoKys(null, true);

            //    //dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} LIMIT 0";
            //}

            string fm = (whereConditions.Any()) ? $"WHERE {string.Join(" AND ", whereConditions)}" : "";
            dbString = string.Format(dbString, fm);


            return dbString;
        }

        public static List<CongTacTheoGiaiDoanExtension> GetCongTacsModel(string codeDA, string CodeCT, string CodeHM, string codeGiaiDoan = null, string conditionDVTH = null, 
            string conditionCTTK = null, string conditionDMCT = null, string whereCondition = null)
        {
            string dbString = GetDbStringCongTacWithHangMucPhanTuyen(codeDA, CodeCT, CodeHM, codeGiaiDoan, conditionDVTH, conditionCTTK, conditionDMCT, whereCondition);
            try
            {
                List<CongTacTheoGiaiDoanExtension> CongTacs = DataProvider.InstanceTHDA.ExecuteQueryModel<CongTacTheoGiaiDoanExtension>(dbString);

                return CongTacs;
            }
            catch (Exception ex)
            {
                return new List<CongTacTheoGiaiDoanExtension>();
            }
        }

        public static DataTable GetCongTacsDataTable(string codeDA, string CodeCT, string CodeHM, string codeGiaiDoan = null, string conditionDVTH = null, string conditionCTTK = null, string conditionDMCT = null, bool GetAllHM = true,DateTime?NBD=null,DateTime?NKT=null, string ConditonNKH = null)
        {
            string dbString = GetDbStringCongTacWithHangMucPhanTuyen(codeDA, CodeCT, CodeHM, codeGiaiDoan, conditionDVTH, conditionCTTK, conditionDMCT, GetAllHM: GetAllHM,NBD: NBD,NKT:NKT, ConditonNKH: ConditonNKH);
            DataTable CongTacs = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var codes = CongTacs.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            dbString = $"UPDATE {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} SET KhoiLuongKeHoach = NULL\r\n" +
                $"WHERE Code IN \r\n" +
                $"(SELECT hn.Code FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} hn\r\n" +
                $"JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk\r\n" +
                $"ON hn.CodeCongTacTheoGiaiDoan = cttk.Code\r\n" +
                $"AND (hn.Ngay < cttk.NgayBatDau OR hn.Ngay > cttk.NgayKetThuc));\r\n";

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            dbString =     $"DELETE FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} WHERE KhoiLuongKeHoach IS NULL AND KhoiLuongThiCong IS NULL AND KhoiLuongKeHoachGoc IS NULL";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);


            CongTacs.AddIndPhanTuyenNhom();
            //CUSTOMORDER

            /*CongTacs.Columns.Add("IndPT", typeof(int));
            var grsHM = CongTacs.AsEnumerable().GroupBy(x => x["CodeHangMuc"].ToString());
            
            foreach (var grHM in grsHM)
            {
                //Xắp nhóm phân tuyến

                int indPhanTuyen = 0;
                foreach (DataRow dr in grHM)
                {
                    
                    if (dr["CodePhanTuyen"] == DBNull.Value && dr["IndPT"] == DBNull.Value)
                    {
                        dr["IndPT"] = indPhanTuyen;
                    }
                    else
                    {
                        dr["IndPT"] = ++indPhanTuyen;
                        var drs = grHM.Where(x => x["CodePhanTuyen"].ToString() == dr["CodePhanTuyen"].ToString());
                        drs.ForEach(x => x["IndPT"] = indPhanTuyen);
                        indPhanTuyen++;
                    }
                }
                var grsPhanTuyen = grHM.GroupBy(x => (int)x["IndPT"]);

                foreach (var grPhanTuyen in grsPhanTuyen)
                {
                    int customSortId = 1;
                    var ls = grPhanTuyen.ToList();
                    ls.Where(x => x["CustomOrder"] != DBNull.Value)
                        .OrderBy(x => x["CustomOrder"]).ForEach(x => x["CustomOrder"] = customSortId++);

                    ls.Where(x => x["CustomOrder"] == DBNull.Value).ForEach(x => x["CustomOrder"] = customSortId++);
                    ls.Where(x => x["OriginalOrder"] == DBNull.Value).ForEach(x => x["OriginalOrder"] = x["SortId"]);
                }
            }*/


            //if (!BaseFrom.allPermission.HaveInitProjectPermission)
                CongTacs.AcceptChanges();
            return CongTacs;
        }


        public static void AddIndPhanTuyenNhom(this DataTable dtCttk)
        {
            dtCttk.Columns.Add("IndPT", typeof(int));
            dtCttk.Columns.Add("IndNhom", typeof(int));
            var grsHM = dtCttk.AsEnumerable().GroupBy(x => x["CodeHangMuc"].ToString());

            foreach (var grHM in grsHM)
            {
                //Xắp nhóm phân tuyến

                int indPhanTuyen = 0;
                foreach (DataRow dr in grHM.Where(x => x["Code"] != DBNull.Value))
                {
                    if (dr["CodePhanTuyen"] == DBNull.Value && dr["IndPT"] == DBNull.Value)
                    {
                        dr["IndPT"] = indPhanTuyen;
                    }
                    else if (dr["IndPT"] == DBNull.Value)
                    {
                        dr["IndPT"] = ++indPhanTuyen;
                        var drs = grHM.Where(x => x["CodePhanTuyen"].ToString() == dr["CodePhanTuyen"].ToString());
                        drs.ForEach(x => x["IndPT"] = indPhanTuyen);
                        indPhanTuyen++;
                    }
                    else
                        continue;

                    grHM.Where(x => x["CodePhanTuyen"].ToString() == dr["CodePhanTuyen"].ToString() && x["CodeNhom"] != DBNull.Value && x["CodeNhom"].ToString() == dr["CodeNhom"].ToString())
                        .ForEach(x => x["IndPT"] = dr["IndPT"]);
                }

                foreach (var item in grHM.AsEnumerable().Where(x => x["Code"] == DBNull.Value))
                {
                    item["IndPT"] = indPhanTuyen++;
                }
                var grsNhom = grHM.GroupBy(x => (int)x["IndPT"]);

                foreach (var grNhom in grsNhom)
                {

                    int indNhom = 0;

                    foreach (DataRow drNhom in grNhom)
                    {
                        if (drNhom["CodeNhom"] == DBNull.Value && drNhom["IndNhom"] == DBNull.Value)
                        {
                            drNhom["IndNhom"] = indNhom;
                        }
                        else if (drNhom["IndNhom"] == DBNull.Value)
                        {
                            drNhom["IndNhom"] = ++indNhom;
                            var drs = grNhom.Where(x => x["CodeNhom"].ToString() == drNhom["CodeNhom"].ToString());
                            drs.ForEach(x => x["IndNhom"] = indNhom);
                            indNhom++;
                        }
                    }
                }

                if (dtCttk.Columns.Contains("CustomOrder") && dtCttk.Columns.Contains("OriginalOrder"))
                {
                    var grsPhanTuyen = grHM.GroupBy(x => (int)x["IndPT"]);
                    Dictionary<string, int> dicPTNhomMaxInd = new Dictionary<string, int>();

                    foreach (var grPhanTuyen in grsPhanTuyen)
                    {
                        string codePT = grPhanTuyen.First()["CodePhanTuyen"].ToString();
                        var grsNhom1 = grPhanTuyen.GroupBy(x => (int)x["IndNhom"]);
                        foreach (var grNhom in grsNhom1)
                        {
                            string codeNhom = grNhom.First()["CodeNhom"].ToString();
                            string codeGop = $"{codePT}_{codeNhom}";

                            int customSortId = 0;

                            if (dicPTNhomMaxInd.ContainsKey(codeGop))
                                customSortId = dicPTNhomMaxInd[codeGop];

                            //var ls = grPhanTuyen.ToList();
                            grNhom.Where(x => x["CustomOrder"] != DBNull.Value)
                            .OrderBy(x => x["CustomOrder"]).ForEach(x => x["CustomOrder"] = ++customSortId);

                            grNhom.Where(x => x["CustomOrder"] == DBNull.Value).ForEach(x => x["CustomOrder"] = ++customSortId);
                            grNhom.Where(x => x["OriginalOrder"] == DBNull.Value).ForEach(x => x["OriginalOrder"] = x["SortId"]);

                            dicPTNhomMaxInd[codeGop] = customSortId;
                        }


                    }
                }
            }


        }

        public static void LoadTongHopVatTuKeHoachBrief()
        {
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);

            if (!SharedControls.ce_LocTheoNgay.Checked)
            {
                TDKHHelper.LoadFullTongHopVatTuBrief(codeCT, codeHM);
                SharedControls.lb_HienTai.Text = "Không lọc";
            }
            else
            {
                if (SharedControls.de_Loc_TuNgay.EditValue is null || SharedControls.de_Loc_DenNgay.EditValue is null)
                {
                    MessageShower.ShowWarning("Vui lòng nhập đầy đủ ngày bắt đầu và kết thúc");
                    return;
                }
                TDKHHelper.LoadFullTongHopVatTuBrief(codeCT, codeHM, SharedControls.de_Loc_TuNgay.EditValue as DateTime?, SharedControls.de_Loc_DenNgay.DateTime as DateTime?);

                SharedControls.lb_HienTai.Text = $"Từ ngày {SharedControls.de_Loc_TuNgay.DateTime.ToShortDateString()} đến ngày {SharedControls.de_Loc_DenNgay.DateTime.ToShortDateString()}";
            }
        }

        public static void LoadTongHopVatTuKeHoach()
        {
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);

            if (!SharedControls.ce_LocTheoNgay.Checked)
            {
                TDKHHelper.LoadFullTongHopVatTu(codeCT, codeHM);
                SharedControls.lb_HienTai.Text = "Không lọc";
            }
            else
            {
                if (SharedControls.de_Loc_TuNgay.EditValue is null || SharedControls.de_Loc_DenNgay.EditValue is null)
                {
                    MessageShower.ShowWarning("Vui lòng nhập đầy đủ ngày bắt đầu và kết thúc");
                    return;
                }
                TDKHHelper.LoadFullTongHopVatTu(codeCT, codeHM, SharedControls.de_Loc_TuNgay.EditValue as DateTime?, SharedControls.de_Loc_DenNgay.DateTime as DateTime?);

                SharedControls.lb_HienTai.Text = $"Từ ngày {SharedControls.de_Loc_TuNgay.DateTime.ToShortDateString()} đến ngày {SharedControls.de_Loc_DenNgay.DateTime.ToShortDateString()}";
            }
        }

        public static bool UpdateDesiredVolumn()
        {
            DateTime NgayBatDau = SharedControls.de_Loc_TuNgay.DateTime;
            DateTime NgayKetThuc = SharedControls.de_Loc_DenNgay.DateTime;

            if (NgayBatDau == default || NgayKetThuc == default)
            {
                MessageShower.ShowWarning("Vui lòng chọn ngày hợp lệ");
                return false;
            }

            var GianNgay = false;


            if (SharedControls.ce_GianNgay.Checked)
            {
                var mess = "Bạn đang ở chế độ Giãn NGÀY kế hoạch," +
                    "\r\nCác công tác đã chọn và có khối lượng mong muốn kỳ này sẽ được được cập nhật lại ngày và KHÔNG THỂ HOÀN TÁC";
                var ret = MessageShower.ShowYesNoQuestion(mess);
                if (ret != DialogResult.Yes)
                {
                    return false;

                }
                else
                {
                    GianNgay = true;
                }
            }

            var dr = MessageShower.ShowYesNoQuestion("Khối lượng mong muốn của các công tác đã chọn sẽ được cập nhật và không thể hoàn tác!");
            if (dr != DialogResult.Yes)
            {
                return false;
            }
            int soNgay = (NgayKetThuc - NgayBatDau).Days + 1;
            if (soNgay <= 0)
            {
                MessageShower.ShowWarning("Vui lòng chọn ngày bắt đầu nhỏ hơn ngày kết thúc");
                return false;
            }

            var wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            var ws = wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi];

            var range = ws.Range[TDKH.RANGE_KeHoach];
            var dic = MyFunction.fcn_getDicOfColumn(range);

            var types = new string[]
            {
                MyConstant.TYPEROW_CVCha,
                MyConstant.TYPEROW_Nhom
            };

            var codesNhomGianNgay = new List<string>();
            var codesCTacGianNgay = new List<string>();
            string NBDString = NgayBatDau.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string NKTString = NgayKetThuc.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

            int total = 0;

            for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
            {
                var crRow = ws.Rows[i];
                var typeRow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();
                var ischecked = crRow[TDKH.COL_Chon].Value.ToString() == true.ToString();

                if (crRow.Visible && types.Contains(typeRow) && ischecked)
                    total ++;
            }
            if (total <= 0)
                return false;

            int count = 0;

            for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
            {
                var crRow = ws.Rows[i];
                var typeRow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();
                var ischecked = crRow[TDKH.COL_Chon].Value.ToString() == true.ToString();

                if (!crRow.Visible || !types.Contains(typeRow) || !ischecked)
                    continue;

                var code = crRow[dic[TDKH.COL_Code]].Value.ToString();
                var KLTB = crRow[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Value.ToString();
                var KLMMStr = crRow[dic[TDKH.COL_KhoiLuongNhapVao]].Value.ToString();
                bool ParseKLMM = double.TryParse(KLMMStr, out double KLMM);
                DateTime NgayBatDauInSheet = crRow[dic[TDKH.COL_NgayBatDau]].Value.DateTimeValue;
                DateTime NgayKetThucInSheet = crRow[dic[TDKH.COL_NgayKetThuc]].Value.DateTimeValue;

                if (!ParseKLMM)
                    continue;


                count++;
                WaitFormHelper.ShowWaitForm($"Đang cập nhật {count / (double)total * 100}%");

                if (!GianNgay)
                {
                    var mindate = DateTimeHelper.Max(NgayBatDau, NgayBatDauInSheet);
                    var maxdate = DateTimeHelper.Min(NgayKetThuc, NgayKetThucInSheet);
                    soNgay = (NgayKetThuc - NgayBatDau).Days + 1;
                    if (soNgay <= 0)
                    {
                        continue;
                    }
                }
                if (typeRow == MyConstant.TYPEROW_Nhom)
                {
                    if (KLTB.HasValue())
                    {
                        if (ParseKLMM)
                        {
                            var klNhomHN = KLMM / soNgay;
                            codesNhomGianNgay.Add(code);
                            string dbStringNhom = $"SELECT * FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} " +
                                $"WHERE CodeNhom = '{code}' AND Ngay >= '{NBDString}' AND Ngay <= '{NKTString}'";
                            DataTable dthnNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbStringNhom);

                            for (DateTime date = NgayBatDau; date <= NgayKetThuc; date = date.AddDays(1))
                            {
                                if (KLMM == 0 && (date > NgayKetThucInSheet || date < NgayBatDauInSheet))
                                {
                                    continue;
                                }
                                string dateStr = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                var drow = dthnNhom.AsEnumerable().SingleOrDefault(x => x["Ngay"].ToString() == dateStr);

                                if (drow is null)
                                {
                                    drow = dthnNhom.NewRow();
                                    dthnNhom.Rows.Add(drow);

                                    drow["CodeNhom"] = code;
                                    drow["Code"] = Guid.NewGuid();
                                    drow["Ngay"] = dateStr;
                                }
                                drow["KhoiLuongKeHoach"] = klNhomHN;
                            }
                            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnNhom, Server.Tbl_TDKH_KhoiLuongCongViecTungNgay);
                        }
                        var inds = SpreadsheetHelper.FindAllChildRow(range, i, dic[TDKH.COL_RowCha], false);

                        if (inds.Any())
                            i = inds.Max();
                    }
                    else
                        continue;
                }
                else
                {
                    if (ParseKLMM)
                    {
                        var klNhomHN = KLMM / soNgay;
                        codesCTacGianNgay.Add(code);
                        string dbStringNhom = $"SELECT * FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} " +
                            $"WHERE CodeCongTacTheoGiaiDoan = '{code}' AND Ngay >= '{NBDString}' AND Ngay <= '{NKTString}'";
                        DataTable dthnNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbStringNhom);

                        for (DateTime date = NgayBatDau; date <= NgayKetThuc; date = date.AddDays(1))
                        {
                            if (KLMM == 0 && (date > NgayKetThucInSheet || date < NgayBatDauInSheet))
                            {
                                continue;
                            }
                            string dateStr = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            var drow = dthnNhom.AsEnumerable().SingleOrDefault(x => x["Ngay"].ToString() == dateStr);

                            if (drow is null)
                            {
                                drow = dthnNhom.NewRow();
                                dthnNhom.Rows.Add(drow);

                                drow["CodeCongTacTheoGiaiDoan"] = code;
                                drow["Code"] = Guid.NewGuid();
                                drow["Ngay"] = dateStr;
                            }
                            drow["KhoiLuongKeHoach"] = klNhomHN;
                        }
                        DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnNhom, Server.Tbl_TDKH_KhoiLuongCongViecTungNgay);
                    }
                }


            }

                if (codesNhomGianNgay.Any())
                {                    
                    if (GianNgay)
                        FormMainHelper.GianNgayNhom(codesNhomGianNgay);

                }

            if (codesCTacGianNgay.Any())
            {
                if (GianNgay)
                    FormMainHelper.GianNgayCongTac(codesCTacGianNgay);

            }

            WaitFormHelper.CloseWaitForm();
            return true;
        }

        public static bool RePlan()
        {

            if (SharedControls.de_Loc_TuNgay.DateTime == default)
            {
                MessageShower.ShowWarning("Vui lòng chọn ngày hợp lệ");
                return false;
            }

            var mess = "Bạn đang ở chế độ LẬP LẠI KHỐI LƯỢNG kế hoạch," +
                "\r\nCác công tác đã chọn và có khối lượng mong muốn kỳ này sẽ được được cập nhật lại ngày và KHÔNG THỂ HOÀN TÁC";
            var ret = MessageShower.ShowYesNoQuestion(mess);
            if (ret != DialogResult.Yes)
            {
                return false;

            }


            mess = $"Bạn có muốn lưu lại khối lượng kế hoạch hiện tại làm khối lượng Gốc để so sánh\r\n" +
                $"CÁC KHỐI LƯỢNG GỐC ĐÃ LƯU TRƯỚC ĐÓ SẼ BỊ LƯU ĐÈ VÀ KHÔNG THỂ HOÀN TÁC!";
            ret = MessageShower.ShowYesNoQuestion(mess);
            var saveOriginalVolumn = ret == DialogResult.Yes;

            var dr = MessageShower.ShowYesNoQuestion("Khối lượng kế hoạch của các công tác đã chọn sẽ được cập nhật và không thể hoàn tác!");
            if (dr != DialogResult.Yes)
            {
                return false;
            }


            var wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            var ws = wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi];

            var range = ws.Range[TDKH.RANGE_KeHoach];
            var dic = MyFunction.fcn_getDicOfColumn(range);

            var types = new string[]
            {
                MyConstant.TYPEROW_CVCha,
                MyConstant.TYPEROW_Nhom
            };

            var codesNhomGianNgay = new List<string>();
            var codesCTacGianNgay = new List<string>();


            int total = 0;

            for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
            {
                var crRow = ws.Rows[i];
                var typeRow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();
                var ischecked = crRow[TDKH.COL_Chon].Value.ToString() == true.ToString();

                if (crRow.Visible && types.Contains(typeRow) && ischecked)
                    total++;
            }
            if (total <= 0)
                return false;

            int count = 0;

            for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
            {
                var crRow = ws.Rows[i];
                var typeRow = crRow[dic[TDKH.COL_TypeRow]].Value.ToString();
                var ischecked = crRow[TDKH.COL_Chon].Value.ToString() == true.ToString();

                if (!crRow.Visible || !types.Contains(typeRow) || !ischecked)
                    continue;

                var code = crRow[dic[TDKH.COL_Code]].Value.ToString();
                var KLTB = crRow[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Value.ToString();
                var KLMMStr = crRow[dic[TDKH.COL_KhoiLuongNhapVao]].Value.ToString();
                bool ParseKLMM = double.TryParse(KLMMStr, out double KLMM);
                DateTime NgayBatDauInSheet = crRow[dic[TDKH.COL_NgayBatDau]].Value.DateTimeValue;
                DateTime NgayKetThucInSheet = SharedControls.de_Loc_TuNgay.DateTime.AddDays(-1);
                count++;
                WaitFormHelper.ShowWaitForm($"Đang cập nhật {count / (double)total * 100}%");


                var mindate = NgayBatDauInSheet;
                var maxdate = NgayKetThucInSheet;

                var NBDString = mindate.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                var NKTString = maxdate.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);




                if (typeRow == MyConstant.TYPEROW_Nhom)
                {
                    if (KLTB.HasValue())
                    {
                        if (ParseKLMM)
                        {
                            var lsCon = new List<string>()
                                {
                                    "KhoiLuongKeHoach = NULL"
                                };

                            if (saveOriginalVolumn)
                                lsCon.Insert(0, "KhoiLuongKeHoachGoc = KhoiLuongKeHoach");

                            string dbString = $"UPDATE {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} SET {string.Join(", ", lsCon)} WHERE CodeNhom = '{code}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        }

                        codesNhomGianNgay.Add(code);
                        string dbStringNhom = $"SELECT * FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} " +
                            $"WHERE CodeNhom = '{code}' AND Ngay >= '{NBDString}' AND Ngay <= '{NKTString}'";
                        DataTable dthnNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbStringNhom);

                        for (DateTime date = mindate; date <= maxdate; date = date.AddDays(1))
                        {
                            string dateStr = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            var drow = dthnNhom.AsEnumerable().SingleOrDefault(x => x["Ngay"].ToString() == dateStr);

                            if (drow is null)
                            {
                                drow = dthnNhom.NewRow();
                                dthnNhom.Rows.Add(drow);

                                drow["CodeNhom"] = code;
                                drow["Code"] = Guid.NewGuid();
                                drow["Ngay"] = dateStr;
                                drow["KhoiLuongKeHoach"] = 0;
                            }
                            else
                            {
                                if (drow["KhoiLuongThiCong"] == DBNull.Value)
                                    drow["KhoiLuongKeHoach"] = 0;
                                else
                                drow["KhoiLuongKeHoach"] = drow["KhoiLuongThiCong"];


                            }
                        }
                        DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnNhom, Server.Tbl_TDKH_KhoiLuongCongViecTungNgay);

                        var inds = SpreadsheetHelper.FindAllChildRow(range, i, dic[TDKH.COL_RowCha], false);

                        if (inds.Any())
                            i = inds.Max();
                    }
                    else
                        continue;
                }
                else
                {
                    if (ParseKLMM)
                    {


                        var lsCon = new List<string>()
                                {
                                    "KhoiLuongKeHoach = NULL"
                                };

                        if (saveOriginalVolumn)
                            lsCon.Insert(0, "KhoiLuongKeHoachGoc = KhoiLuongKeHoach");

                        string dbString = $"UPDATE {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} SET {string.Join(", ", lsCon)} WHERE CodeCongTacTheoGiaiDoan = '{code}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);


                        string dbStringNhom = $"SELECT * FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} " +
                            $"WHERE CodeCongTacTheoGiaiDoan = '{code}' AND Ngay >= '{NBDString}' AND Ngay <= '{NKTString}'";
                        DataTable dthnNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbStringNhom);

                        for (DateTime date = mindate; date <= maxdate; date = date.AddDays(1))
                        {
                            string dateStr = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            var drow = dthnNhom.AsEnumerable().SingleOrDefault(x => x["Ngay"].ToString() == dateStr);

                            if (drow is null)
                            {
                                drow = dthnNhom.NewRow();
                                dthnNhom.Rows.Add(drow);

                                drow["CodeCongTacTheoGiaiDoan"] = code;
                                drow["Code"] = Guid.NewGuid();
                                drow["Ngay"] = dateStr;
                                drow["KhoiLuongKeHoach"] = 0;
                            }
                            else
                            {
                                if (drow["KhoiLuongThiCong"] == DBNull.Value)
                                    drow["KhoiLuongKeHoach"] = 0;
                                else
                                drow["KhoiLuongKeHoach"] = drow["KhoiLuongThiCong"];
                            }
                            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnNhom, Server.Tbl_TDKH_KhoiLuongCongViecTungNgay);
                        }
                    }


                }

            }
            var dbString1 = $"DELETE FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} WHERE KhoiLuongKeHoach IS NULL AND KhoiLuongThiCong IS NULL AND KhoiLuongKeHoachGoc IS NULL";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString1);
            WaitFormHelper.CloseWaitForm();


            return true;
        }

        public static void ReCalcNhomPeriod()
        {
            string dbString = $"UPDATE Tbl_TDKH_NhomCongTac\r\n" +
                $"SET (NgayBatDau, NgayKetThuc) = " +
                $"(SELECT MIN(NgayBatDau) AS NgayBatDau, MAX(NgayKetThuc) AS NgayKetThuc FROM {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} WHERE CodeNhom = Tbl_TDKH_NhomCongTac.Code GROUP BY CodeNhom)\r\n" +
                $"WHERE KhoiLuongKeHoach IS NOT NULL AND (NgayBatDau IS NULL OR NgayKetThuc IS NULL)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            dbString = $"UPDATE {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} SET KhoiLuongKeHoach = NULL\r\n" +
    $"WHERE Code IN \r\n" +
    $"(SELECT hn.Code FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} hn\r\n" +
    $"JOIN {Server.Tbl_TDKH_NhomCongTac} cttk\r\n" +
    $"ON hn.CodeNhom = cttk.Code\r\n" +
    $"AND (hn.Ngay < cttk.NgayBatDau OR hn.Ngay > cttk.NgayKetThuc));\r\n";

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            dbString = $"DELETE FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} WHERE KhoiLuongKeHoach IS NULL AND KhoiLuongThiCong IS NULL AND KhoiLuongKeHoachGoc IS NULL";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
        }

        public static void CalcOthersMaterials()
        {
            string dbString = @"UPDATE Tbl_TDKH_HaoPhiVatTu
SET DonGia =
COALESCE(
(
SELECT SUM(vt.DonGia*vt.DinhMucNguoiDung*vt.HeSoNguoiDung/100) 
FROM Tbl_TDKH_HaoPhiVatTu vt
WHERE vt.CodeCongTac  = Tbl_TDKH_HaoPhiVatTu.CodeCongTac AND vt.Code != Tbl_TDKH_HaoPhiVatTu.Code
), 0)
WHERE UPPER(VatTu) LIKE '%M_Y KH_C%' OR UPPER(VatTu) LIKE '%V_T LI_U KH_C%'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
        }
    }
}
