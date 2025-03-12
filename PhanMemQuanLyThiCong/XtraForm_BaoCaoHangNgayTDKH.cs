using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_BaoCaoHangNgayTDKH : DevExpress.XtraEditors.XtraForm
    {
        private List<string> lstCodeNhom { get; set; }
        private List<string> lstCodeCtac { get; set; }
        public XtraForm_BaoCaoHangNgayTDKH()
        {
            InitializeComponent();
        }

        private void sb_ReadExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = SharedControls.openFileDialog;
            openFileDialog.DefaultExt = "xls";
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Chọn file Excel";
            DialogResult rs = openFileDialog.ShowDialog();
            if (rs == DialogResult.OK)
            {
                SpreadsheetControl Spread = new SpreadsheetControl();
                Spread.LoadDocument(openFileDialog.FileName);
                if (!Spread.Document.Worksheets.Select(x => x.Name).ToList().Contains("Báo cáo hằng ngày"))
                {
                    MessageShower.ShowError("Mẫu đọc vào không phải mẫu Tổng hợp kinh phí của hệ thống, Vui lòng chọn lại Mẫu khác!!!!");
                    return;
                }
                Worksheet ws = Spread.Document.Worksheets["Báo cáo hằng ngày"];
                if (!ws.DefinedNames.Contains("RangeNgay"))
                {
                    MessageShower.ShowError("Mẫu đọc vào đã được chỉnh sửa, Vui lòng chọn lại Mẫu khác!!!!");
                    return;
                }
                WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu vào!!!!!", "Vui Lòng chờ!");
                Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range["Data"]);
                var crDVTH = ctrl_DonViThucHien.SelectedDVTH;
                string Code = ws.Rows[3][Name["Code"]].Value.ToString();
                if (Code != crDVTH.Code)
                {
                    MessageShower.ShowError($"Mẫu đọc vào không phải mẫu của nhà thầu {crDVTH.Ten}, Vui lòng chọn lại Mẫu khác!!!!");
                    return;
                }
                Dictionary<KeyValuePair<string, int>, double> dic_In = new Dictionary<KeyValuePair<string, int>, double>();
                Spread.BeginUpdate();
                CellRange Data = ws.Range["Data"];
                Dictionary<string, string> NameNgay = Name.Where(x => x.Key.Contains("KL_")).ToDictionary(x => x.Key, y => y.Value);

                string DbString = string.Empty;
                List<string> lstCodeCTac = new List<string>();
                List<string> lstCodeNhom = new List<string>();
                bool isCapNhap = false;
                for (int i = Data.TopRowIndex; i <= Data.BottomRowIndex; i++)
                {
                    Row Crow = ws.Rows[i];
                    if (!Crow.Visible)
                        continue;
                    string MCT = Crow[Name["MaHieuCongTac"]].Value.TextValue;
                    if (MCT == MyConstant.TYPEROW_HangMuc || MCT == MyConstant.TYPEROW_CongTrinh
                        || MCT == MyConstant.CONST_TYPE_PhanTuyen || MCT == MyConstant.CONST_TYPE_HoanThanhPhanTuyen)
                        continue;
                    Code = Crow[Name["Code"]].Value.TextValue;
                    if (string.IsNullOrEmpty(Code))
                        continue;
                    WaitFormHelper.ShowWaitForm($"{MCT}: {Crow[Name[TDKH.COL_DanhMucCongTac]].Value.TextValue}", "Đang tổng hợp dữ liệu");
                    string TypeRow = Crow[Name[TDKH.COL_TypeRow]].Value.TextValue;
                    if (TypeRow == MyConstant.TYPEROW_Nhom)
                    {
                        if (string.IsNullOrEmpty(Crow[Name["KhoiLuongToanBo"]].Value.TextValue))
                            continue;
                        DbString = $"SELECT * FROM { TDKH.TBL_NhomCongTac} WHERE Code='{Code}'";
                        DataTable dtnhom = DataProvider.InstanceTHDA.ExecuteQuery(DbString);
                        if (dtnhom.Rows.Count == 0)
                            continue;
                        //if (dtnhom.Rows[0]["KhoiLuongKeHoach"] == DBNull.Value)
                        //    continue;
                        lstCodeNhom.Add(Code);
                        DbString = $"SELECT * FROM { TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE CodeNhom='{Code}'";
                        DataTable dthn = DataProvider.InstanceTHDA.ExecuteQuery(DbString);
                        DataTable dthnNew = dthn.Copy();
                        foreach (var item in NameNgay)
                        {
                            if (Crow[item.Value].Value.IsEmpty)
                                continue;
                            string Date = item.Key.Replace("KL_", string.Empty);
                            DateTime.TryParse(Date, out DateTime DateParse);
                            if (DateParse.Date > DateTime.Now)
                                continue;
                            string TextKLTC = Crow[item.Value].Value.ToString();
                            if (string.IsNullOrEmpty(TextKLTC))
                                continue;
                            double.TryParse(TextKLTC, out double KLTC);
                            //double KLTC = Crow[item.Value].Value.NumericValue;
                            //if (KLTC == 0)
                            //    continue;
                            if (dthn.Rows.Count > 0)
                            {
                                DataRow RowUpdate = dthn.AsEnumerable().Where(x => DateTime.Parse(x["Ngay"].ToString()).Date == DateParse.Date).SingleOrDefault();
                                if (RowUpdate is null)
                                {
                                    if (KLTC == 0)
                                        continue;
                                    DataRow NewRow = dthnNew.NewRow();
                                    NewRow["Code"] = Guid.NewGuid();
                                    NewRow["CodeNhom"] = Code;
                                    NewRow["KhoiLuongThiCong"] = KLTC;
                                    NewRow["IsEdited"] = true;
                                    NewRow["Ngay"] = DateParse.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dthnNew.Rows.Add(NewRow);
                                }
                                else
                                {
                                    RowUpdate = dthnNew.AsEnumerable().Where(x => DateTime.Parse(x["Ngay"].ToString()).Date == DateParse.Date).SingleOrDefault();
                                    if (KLTC == 0)
                                        RowUpdate["KhoiLuongThiCong"] = DBNull.Value;
                                    else
                                        RowUpdate["KhoiLuongThiCong"] = KLTC;
                                }
                            }
                            else
                            {
                                if (KLTC == 0)
                                    continue;
                                DataRow NewRow = dthnNew.NewRow();
                                NewRow["Code"] = Guid.NewGuid();
                                NewRow["CodeNhom"] = Code;
                                NewRow["KhoiLuongThiCong"] = KLTC;
                                NewRow["IsEdited"] = true;
                                NewRow["Ngay"] = DateParse.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dthnNew.Rows.Add(NewRow);
                            }
                        }
                        DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnNew, TDKH.TBL_KhoiLuongCongViecHangNgay);
                        //for (int j = RowNhom + 1; j <= Data.BottomRowIndex; j++)
                        //{
                        //    i = j - 1;
                        //    Crow = ws.Rows[j];
                        //    MCT = Crow[Name["MaHieuCongTac"]].Value.TextValue;
                        //    if (MCT == MyConstant.TYPEROW_HangMuc || MCT == MyConstant.CONST_TYPE_NHOM || MCT == MyConstant.TYPEROW_CongTrinh
                        //        || MCT == MyConstant.CONST_TYPE_PhanTuyen || MCT == MyConstant.CONST_TYPE_HoanThanhPhanTuyen)
                        //        break;

                        //}
                    }
                    else
                    {
                        if (TypeRow == MyConstant.TYPEROW_CVCha|| TypeRow == "CONGTAC")
                        {
                            isCapNhap = false;
                            DbString = $"SELECT * FROM { TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE CodeCongTacTheoGiaiDoan='{Code}'";
                            DataTable dthn = DataProvider.InstanceTHDA.ExecuteQuery(DbString);
                            DataTable dthnNew = dthn.Copy();
                            foreach (var item in NameNgay)
                            {
                                if (Crow[item.Value].Value.IsEmpty)
                                    continue;
                                string Date = item.Key.Replace("KL_", string.Empty);
                                DateTime.TryParse(Date, out DateTime DateParse);
                                if (DateParse.Date > DateTime.Now)
                                    continue;
                                string TextKLTC = Crow[item.Value].Value.ToString();
                                if (string.IsNullOrEmpty(TextKLTC))
                                    continue;
                                double.TryParse(TextKLTC, out double KLTC);
                                //double KLTC = Crow[item.Value].Value.NumericValue;
                                isCapNhap = true;
                                if (dthn.Rows.Count > 0)
                                {
                                    DataRow RowUpdate = dthn.AsEnumerable().Where(x => DateTime.Parse(x["Ngay"].ToString()).Date == DateParse.Date).SingleOrDefault();
                                    if (RowUpdate is null)
                                    {
                                        if (KLTC == 0)
                                            continue;
                                        DataRow NewRow = dthnNew.NewRow();
                                        NewRow["Code"] = Guid.NewGuid();
                                        NewRow["CodeCongTacTheoGiaiDoan"] = Code;
                                        NewRow["KhoiLuongThiCong"] = KLTC;
                                        NewRow["IsEdited"] = true;
                                        NewRow["Ngay"] = DateParse.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dthnNew.Rows.Add(NewRow);
                                    }
                                    else
                                    {
                                        RowUpdate = dthnNew.AsEnumerable().Where(x => DateTime.Parse(x["Ngay"].ToString()).Date == DateParse.Date).SingleOrDefault();
                                        if (KLTC == 0)
                                            RowUpdate["KhoiLuongThiCong"] = DBNull.Value;
                                        else
                                            RowUpdate["KhoiLuongThiCong"] = KLTC;
                                    }
                                }
                                else
                                {
                                    if (KLTC == 0)
                                        continue;
                                    DataRow NewRow = dthnNew.NewRow();
                                    NewRow["Code"] = Guid.NewGuid();
                                    NewRow["CodeCongTacTheoGiaiDoan"] = Code;
                                    NewRow["KhoiLuongThiCong"] = KLTC;
                                    NewRow["IsEdited"] = true;
                                    NewRow["Ngay"] = DateParse.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dthnNew.Rows.Add(NewRow);
                                }
                            }
                            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnNew, TDKH.TBL_KhoiLuongCongViecHangNgay);
                            if (isCapNhap)
                                lstCodeCTac.Add(Code);
                        }
                    }
                }
                DbString = $"DELETE FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE KhoiLuongKeHoach IS NULL AND KhoiLuongThiCong IS NULL AND KhoiLuongBoSung IS NULL ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(DbString);
                Spread.EndUpdate();
                WaitFormHelper.CloseWaitForm();
                if (lstCodeNhom.Any())
                {
                    WaitFormHelper.ShowWaitForm("Đang cập nhật lại trạng thái theo khối lượng thi công cho Nhóm");
                    DuAnHelper.UpdateStateTDKHNhomByKhoiLuongThiCong(lstCodeNhom.ToArray());
                }
                rs = MessageShower.ShowYesNoQuestion("Bạn có muốn cập nhật lại trạng thái công tác theo khối lượng thi công không????");
                if (rs == DialogResult.Yes)
                {
                    WaitFormHelper.ShowWaitForm("Đang cập nhật lại trạng thái theo khối lượng thi công");
                    DuAnHelper.UpdateStateTDKHByKhoiLuongThiCong(lstCodeCTac.ToArray());
                }
                WaitFormHelper.ShowWaitForm("Đang cập nhật lại khối lượng thi công");

                //lstCodeCTac.ForEach(x => DuAnHelper.UpdateStateTDKHByKhoiLuongThiCong(new string[] { x }));
                //lstCodeNhom.ForEach(x => DoBocChuanHelper.ReCalcCongTacHaoPhiInNhom(x));
            }
            MessageShower.ShowInformation("Đọc dữ liệu hoàn tất!!!!!!!!!!!!");
            rs = MessageShower.ShowYesNoQuestion("Bạn có muốn tải lại dữ liệu hiện tại không????");
            if (rs == DialogResult.Yes)
            {
                Fcn_UpdateCongTac(false);
                Fcn_UpDateData();
            }
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_UpdateCongTac(bool IsCapNhap = false)
        {
            WaitFormHelper.ShowWaitForm($"Đang phân tích dữ liệu", "Đang tổng hợp dữ liệu");
            FileHelper.fcn_spSheetStreamDocument(spsheet_TongHop, $@"{BaseFrom.m_templatePath}\FileExcel\16.BaoCaoHangNgayTDKH.xlsx");
            IWorkbook wb = spsheet_TongHop.Document;
            Worksheet ws = wb.Worksheets[0];
            CellRange Data = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(Data);
            var crDVTH = ctrl_DonViThucHien.SelectedDVTH;
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);
            DataTable CongTacs = TDKHHelper.GetCongTacsDataTable(SharedControls.slke_ThongTinDuAn.EditValue?.ToString(),
                codeCT, codeHM, SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString(), $"cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}'", GetAllHM: false);
            var codeNhoms = lstCodeNhom= CongTacs.AsEnumerable()
        .Select(x => x["CodeNhom"].ToString()).Distinct().ToList();
            lstCodeCtac= CongTacs.AsEnumerable()
        .Select(x => x["Code"].ToString()).Distinct().ToList();
            //List<string> CotAn = new List<string>();
            List<object> CotAn = ccbe_CotAn.Properties.Items.GetCheckedValues();
            CotAn.Remove("DonGiaHangNgay");
            string strCodeNhom = MyFunction.fcn_Array2listQueryCondition(codeNhoms);

            string dbString = $"SELECT * FROM {TDKH.TBL_NhomCongTac} WHERE Code IN ({strCodeNhom})";
            DataTable dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DateTime Min = DateTime.Now;
            DateTime Max = DateTime.Now.AddDays(30);
            string mindateStr = CongTacs.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Min(x => StringHelper.Min((string)x["NgayBatDau"], (x["NgayBatDauThiCong"] == DBNull.Value) ? (string)x["NgayBatDau"] : (string)x["NgayBatDauThiCong"]));
            string maxdateStr = CongTacs.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Max(x => StringHelper.Max((string)x["NgayKetThuc"], (x["NgayKetThucThiCong"] == DBNull.Value) ? (string)x["NgayKetThuc"] : (string)x["NgayKetThucThiCong"]));
            if (!DateTime.TryParse(mindateStr, out Min) || !DateTime.TryParse(maxdateStr, out Max))
            {
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowWarning("Nhà thầu chưa có công tác để tổng hợp");
                return;
            }
            if (IsCapNhap)
            {
                de_Begin.DateTime = Min;
                de_End.DateTime = Max;
            }
            spsheet_TongHop.BeginUpdate();
            CotAn.ForEach(x =>ws.Columns[Name[x.ToString()]].Visible = true);
            ws.Rows[1]["F"].SetValueFromText($"{SharedControls.slke_ThongTinDuAn.Text}");
            ws.Rows[3]["F"].SetValueFromText($"{crDVTH.Ten}");
            ws.Rows[3][Name["Code"]].SetValueFromText($"{crDVTH.Code}");
            DataTable dtData = new DataTable();
            foreach (var item in Name)
            {
                dtData.Columns.Add(item.Key, typeof(object));
            }
            string prefixFormula = MyConstant.PrefixFormula;
            var crRowInd = Data.TopRowIndex + 1;
            int RowIndex = Data.TopRowIndex;
            //int RowHM = 0, RowTuyen = 0, RowNhom = 0, RowCongTrinh = 0;
            int STTCTrinh = 0;
            //bool Nhom = false;
            var grsCTrinh = CongTacs.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);
            foreach (var Ctrinh in grsCTrinh)
            {
                DataRow newRowCtrinh = dtData.NewRow();
                dtData.Rows.Add(newRowCtrinh);
                var fstCtr = Ctrinh.FirstOrDefault();
                newRowCtrinh[TDKH.COL_DanhMucCongTac] = $"{fstCtr["TenCongTrinh"]}".ToUpper();
                newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                newRowCtrinh[TDKH.COL_Code] = Ctrinh.Key;
                newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                var crRowCtrInd = crRowInd = Data.TopRowIndex + dtData.Rows.Count;
                var grsHM = Ctrinh.GroupBy(x => x["CodeHangMuc"]);
                int STTHM = 0;
                int IndSumIf = Data.TopRowIndex - 1;
                foreach (var HM in grsHM)
                {
                    DataRow newRowHM = dtData.NewRow();
                    dtData.Rows.Add(newRowHM);
                    var fstHM = HM.First();
                    newRowHM[TDKH.COL_DanhMucCongTac] = $"{fstHM["TenHangMuc"]}".ToUpper();
                    newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                    newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                    newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd})";
                    newRowHM[TDKH.COL_Code] = HM.Key;
                    newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                    var crRowHMInd = crRowInd = Data.TopRowIndex + dtData.Rows.Count;
                    var grsPhanTuyen = HM.GroupBy(x => (int)x["IndPT"]).OrderBy(x => x.Key);
                    foreach (var grPhanTuyen in grsPhanTuyen)
                    {
                        var fstTuyen = grPhanTuyen.First();
                        string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : $"{fstTuyen["CodePhanTuyen"]}";
                        DataRow fstPT = grPhanTuyen.First();
                        string codePT = fstPT["CodePhanTuyen"].ToString();
                        bool isPhanTuyen = codePT.HasValue();
                        int? crRowPTInd = null;
                        DataRow newRowPT = null;
                        if (isPhanTuyen)
                        {
                            newRowPT = dtData.NewRow();
                            dtData.Rows.Add(newRowPT);

                            crRowPTInd = crRowInd = Data.TopRowIndex + dtData.Rows.Count;
                            //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                            newRowPT[TDKH.COL_Code] = codePT;
                            newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                            newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
                            newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                            newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";
                        }
                        var grsNhom = grPhanTuyen.GroupBy(x => (int)x["IndNhom"]).OrderBy(x => x.Key);
                        foreach (var grNhom in grsNhom)
                        {
                            var fstNhom = grNhom.First();
                            int? crRowNhomInd = null;
                            string crCodeNhom = (fstNhom["CodeNhom"] == DBNull.Value) ? null : $"{fstNhom["CodeNhom"]}";
                            DataRow drNhom = null;
                            //Nhom = false;
                            //var firstCol = Name[$"KL_{Min.Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"];
                            //var lastCol = Name[$"KL_{Max.Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"];
                            if (!string.IsNullOrEmpty(crCodeNhom))
                            {
                                drNhom = dtNhom.AsEnumerable().Single(x => x["Code"].ToString() == crCodeNhom);
                                DataRow newRowNhom = dtData.NewRow();
                                dtData.Rows.Add(newRowNhom);
                                crRowNhomInd = crRowInd = Data.TopRowIndex + dtData.Rows.Count;

                                newRowNhom[TDKH.COL_DanhMucCongTac] = drNhom["Ten"].ToString().ToUpper();
                                newRowNhom[TDKH.COL_DonVi] = drNhom["DonVi"];
                                newRowNhom[TDKH.COL_Code] = crCodeNhom;
                                newRowNhom[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_NHOM;
                                newRowNhom[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd)})";
                                newRowNhom[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                                if (drNhom["GhiChuBoSungJson"] != DBNull.Value)
                                {
                                    var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(drNhom["GhiChuBoSungJson"].ToString());
                                    newRowNhom[TDKH.COL_STTDocVao] = GhiChuBoSungJson.STT;
                                    newRowNhom[TDKH.COL_STTND] = GhiChuBoSungJson.STTND;
                                }
                                if (drNhom["KhoiLuongKeHoach"] != DBNull.Value)
                                {
                                    //Nhom = true;
                                    newRowNhom[TDKH.COL_DBC_KhoiLuongToanBo] = drNhom["KhoiLuongKeHoach"];
                                    newRowNhom[TDKH.COL_TrangThai] = drNhom["TrangThai"];
                                    //newRowNhom["KhoiLuongDaThiCong"] = $"{prefixFormula}SUMIF({firstCol}{IndSumIf + 1}:{lastCol}{IndSumIf + 1};\"Khối lượng\";{firstCol}{crRowNhomInd}:{lastCol}{crRowNhomInd})";
                                }
                            }
                            if (!ce_ChiDocNhom.Checked)
                            {
                                int STTCTac = 0;
                                foreach (var ctac in grNhom)
                                {
                                    string tt = $"{STTCTrinh}.{STTHM}.{++STTCTac}";

                                    WaitFormHelper.ShowWaitForm($"{tt}: {ctac["TenCongTac"]}", "Đang tổng hợp dữ liệu");
                                    if (ctac["Code"] == DBNull.Value)
                                        continue;
                                    DataRow newRowCTac = dtData.NewRow();
                                    dtData.Rows.Add(newRowCTac);
                                    var crrowIndCt = crRowInd = Data.TopRowIndex + dtData.Rows.Count;
                                    newRowCTac[TDKH.COL_STT] = tt;
                                    newRowCTac[TDKH.COL_Code] = ctac["Code"];
                                    newRowCTac[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowNhomInd ?? crRowPTInd ?? crRowHMInd)})";
                                    newRowCTac[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                    newRowCTac[TDKH.COL_MaHieuCongTac] = ctac["MaHieuCongTac"];
                                    newRowCTac[TDKH.COL_DanhMucCongTac] = ctac["TenCongTac"];
                                    newRowCTac[TDKH.COL_DonVi] = ctac["DonVi"];
                                    newRowCTac["CodeDanhMucCongTac"] = ctac["CodeDanhMucCongTac"];
                                    newRowCTac[TDKH.COL_GhiChu] = ctac["GhiChu"];
                                    newRowCTac[TDKH.COL_DBC_KhoiLuongToanBo] = ctac["KhoiLuongToanBo"];
                                    newRowCTac[TDKH.COL_TrangThai] = ctac["TrangThai"];
                                    if (ctac["GhiChuBoSungJson"] != DBNull.Value)
                                    {
                                        var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(ctac["GhiChuBoSungJson"].ToString());
                                        newRowCTac[TDKH.COL_STTDocVao] = GhiChuBoSungJson.STT;
                                        newRowCTac[TDKH.COL_STTND] = GhiChuBoSungJson.STTND;
                                    }
                                    //newRowCTac["KhoiLuongDaThiCong"] = $"{prefixFormula}SUMIF({firstCol}{IndSumIf + 1}:{lastCol}{IndSumIf + 1};\"Khối lượng\";{firstCol}{crrowIndCt}:{lastCol}{crrowIndCt})";
                                    newRowCTac[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{Name[TDKH.COL_NgayKetThucThiCong]}{crrowIndCt}-{Name[TDKH.COL_NgayBatDauThiCong]}{crrowIndCt}+1";
                                    newRowCTac[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{Name[TDKH.COL_NgayKetThuc]}{crrowIndCt}-{Name[TDKH.COL_NgayBatDau]}{crrowIndCt}+1";
                                    newRowCTac[TDKH.COL_PhanTramThucHien] = $"{prefixFormula}IF({Name[TDKH.COL_DBC_KhoiLuongToanBo]}{crrowIndCt}= 0;0;{Name[TDKH.COL_KhoiLuongDaThiCong]}{crrowIndCt}/{Name[TDKH.COL_DBC_KhoiLuongToanBo]}{crrowIndCt})";
                                }
                            }
                        }
                        if (isPhanTuyen)
                        {
                            DataRow newRowHTPT = dtData.NewRow();
                            dtData.Rows.Add(newRowHTPT);
                            newRowHTPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                            newRowHTPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd})";
                            newRowHTPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;
                            newRowHTPT[TDKH.COL_DanhMucCongTac] = $"Hoàn thành {fstPT["TenPhanTuyen"]}".ToUpper();
                        }

                    }
                }
            }
            int numRow = dtData.Rows.Count;
            ws.Rows.Insert(Data.TopRowIndex + 1, numRow + 1, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, Data.TopRowIndex, 0);
            ws.Columns[Name[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;
            Data = ws.Range["Data"];
            SpreadsheetHelper.ReplaceAllFormulaAfterImport(Data);
            SpreadsheetHelper.FormatRowsInRange(Data, Name[TDKH.COL_TypeRow], Name[TDKH.COL_RowCha],
            Name[TDKH.COL_Code], colMaCongTac: Name[TDKH.COL_MaHieuCongTac]);
            spsheet_TongHop.EndUpdate();
            WaitFormHelper.CloseWaitForm();

    //        foreach (var grCTrinh in grsCTrinh)
    //        {
    //            Row crRowWs = ws.Rows[RowIndex++];
    //            ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
    //            var First = grCTrinh.FirstOrDefault();
    //            crRowWs.Font.Bold = true;
    //            RowCongTrinh = RowIndex;
    //            crRowWs[Name[TDKH.COL_STT]].SetValue(++STTCTrinh);
    //            crRowWs.Font.Color = MyConstant.color_Row_CongTrinh;
    //            crRowWs[Name[TDKH.COL_Code]].SetValueFromText(grCTrinh.Key.ToString());
    //            crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(MyConstant.CONST_TYPE_CONGTRINH);
    //            crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(First["TenCongTrinh"].ToString().ToUpper());
    //            //crRowWs[Name[TDKH.COL_TypeRow]].SetValueFromText(MyConstant.TYPEROW_CongTrinh);
    //            var grsHM = grCTrinh.GroupBy(x => x["CodeHangMuc"]);
    //            int STTHM = 0;
    //            foreach (var grHM in grsHM)
    //            {
    //                crRowWs = ws.Rows[RowIndex++];
    //                ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
    //                crRowWs.Font.Bold = true;
    //                RowHM = RowIndex;
    //                crRowWs.Font.Color = MyConstant.color_Row_HangMuc;
    //                crRowWs[Name[TDKH.COL_STT]].SetValue($"{STTCTrinh}.{++STTHM}");
    //                crRowWs[Name[TDKH.COL_Code]].SetValueFromText(grHM.Key.ToString());
    //                crRowWs[Name[TDKH.COL_RowCha]].SetValue(RowCongTrinh);
    //                crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(MyConstant.CONST_TYPE_HANGMUC);
    //                crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(grHM.FirstOrDefault()["TenHangMuc"].ToString().ToUpper());
    //                var grsPhanTuyen = grHM.GroupBy(x => (int)x["IndPT"])
    //.OrderBy(x => x.Key);
    //                foreach (var grPhanTuyen in grsPhanTuyen)
    //                {
    //                    var fstTuyen = grPhanTuyen.First();
    //                    string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : $"{fstTuyen["CodePhanTuyen"]}";
    //                    //DataRow fstPT = grPhanTuyen.First();
    //                    //string codePT = fstPT["CodePhanTuyen"].ToString();
    //                    //bool isPhanTuyen = codePT.HasValue();
    //                    //int? crRowPTInd = null;
    //                    //DataRow newRowPT = null;
    //                    //if (isPhanTuyen)
    //                    //{
    //                    //    newRowPT = dtData.NewRow();
    //                    //    dtData.Rows.Add(newRowPT);

    //                    //    crRowPTInd = crRowInd = definedName.Range.TopRowIndex + dtData.Rows.Count;
    //                    //    //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
    //                    //    newRowPT[TDKH.COL_Code] = codePT;
    //                    //    newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
    //                    //    newRowPT[TDKH.COL_DanhMucCongTac] = fstPT["TenPhanTuyen"];
    //                    //    newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
    //                    //    newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd + 1})";
    //                    //}                   var fstTuyen = grPhanTuyen.First();
    //                    DataRow fstPT = grPhanTuyen.First();
    //                    string codePT = fstPT["CodePhanTuyen"].ToString();
    //                    bool isPhanTuyen = codePT.HasValue();
    //                    if (isPhanTuyen)
    //                    {
    //                        crRowWs = ws.Rows[RowIndex++];
    //                        ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
    //                        crRowWs.Font.Bold = true;
    //                        crRowWs.Font.Color = MyConstant.color_Row_PhanTuyen;
    //                        crRowWs[Name[TDKH.COL_Code]].SetValueFromText(codePT);
    //                        crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(MyConstant.CONST_TYPE_PhanTuyen);
    //                        crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(fstPT["TenPhanTuyen"]);
    //                        crRowWs[Name[TDKH.COL_TypeRow]].SetValueFromText(MyConstant.TYPEROW_PhanTuyen);
    //                    }
    //                    var grsNhom = grPhanTuyen.GroupBy(x => (int)x["IndNhom"])
    //    .OrderBy(x => x.Key);
    //                    foreach (var grNhom in grsNhom)
    //                    {
    //                        var fstNhom = grNhom.First();
    //                        int? crRowNhomInd = null;
    //                        string crCodeNhom = (fstNhom["CodeNhom"] == DBNull.Value) ? null : $"{fstNhom["CodeNhom"]}";
    //                        DataRow drNhom = null;
    //                        Nhom = false;
    //                        if (!string.IsNullOrEmpty(crCodeNhom))
    //                        {
    //                            drNhom = dtNhom.AsEnumerable().Single(x => x["Code"].ToString() == crCodeNhom);
    //                            crRowWs = ws.Rows[RowIndex++];
    //                            ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
    //                            crRowWs.Font.Bold = true;
    //                            RowNhom = RowIndex;
    //                            crRowWs.Font.Color = MyConstant.color_Row_NhomCongTac;
    //                            crRowWs[Name[TDKH.COL_Code]].SetValueFromText(crCodeNhom);
    //                            crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(MyConstant.CONST_TYPE_NHOM);
    //                            crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(drNhom["Ten"]);
    //                            crRowWs[Name[TDKH.COL_DonVi]].SetValue(drNhom["DonVi"]);
    //                            crRowWs[Name[TDKH.COL_TypeRow]].SetValueFromText(MyConstant.TYPEROW_Nhom);
    //                            if (drNhom["GhiChuBoSungJson"] != DBNull.Value)
    //                            {
    //                                var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(drNhom["GhiChuBoSungJson"].ToString());
    //                                crRowWs[Name[TDKH.COL_STTDocVao]].SetValueFromText(GhiChuBoSungJson.STT);
    //                                crRowWs[Name[TDKH.COL_STTND]].SetValueFromText(GhiChuBoSungJson.STTND);
    //                            }
    //                            if (drNhom["KhoiLuongKeHoach"] != DBNull.Value)
    //                            {
    //                                Nhom = true;
    //                                crRowWs[Name[TDKH.COL_DBC_KhoiLuongToanBo]].SetValue(drNhom["KhoiLuongKeHoach"]);
    //                                //crRowWs[Name[TDKH.COL_TypeRow]].SetValueFromText("NHOM");
    //                            }
    //                            //crRowWs[Name[TDKH.COL_TypeRow]].SetValueFromText(MyConstant.TYPEROW_Nhom);

    //                            //newRowNhom[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{drNhom["NgayBatDau"]}";
    //                            //newRowNhom[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{drNhom["NgayKetThuc"]}"; //drNhom["NgayKetThuc"];
    //                            //newRowNhom[TDKH.COL_DonGia] = drNhom["DonGia"];
    //                            //newRowNhom[TDKH.COL_DonGiaThiCong] = drNhom["DonGiaThiCong"];
    //                            //newRowNhom[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd) + 1})";
    //                            //newRowNhom[TDKH.COL_Code] = drNhom["Code"];
    //                            //newRowNhom[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
    //                        }

    //                        int STTCTac = 0;
    //                        foreach (var ctac in grNhom)
    //                        {

    //                            string tt = $"{STTCTrinh}.{STTHM}.{++STTCTac}";

    //                            WaitFormHelper.ShowWaitForm($"{tt}: {ctac["TenCongTac"]}", "Đang tổng hợp dữ liệu");

    //                            //var fstRow = grCongTac.First();
    //                            if (ctac["Code"] == DBNull.Value)
    //                                continue;
    //                            crRowWs = ws.Rows[RowIndex++];
    //                            ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
    //                            crRowWs.Font.Color = crCodeNhom is null ? Color.Black : MyConstant.color_Row_NhomCongTac;
    //                            crRowWs.Font.Bold = false;
    //                            int RowCha = crCodeTuyen != null ? RowTuyen : Nhom ? RowNhom : RowHM;
    //                            crRowWs[Name[TDKH.COL_RowCha]].SetValue(RowCha);
    //                            crRowWs[Name[TDKH.COL_STT]].SetValueFromText(tt);
    //                            crRowWs[Name[TDKH.COL_Code]].SetValueFromText(ctac["Code"].ToString());

    //                            crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValueFromText(ctac["MaHieuCongTac"].ToString());
    //                            crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValueFromText(ctac["TenCongTac"].ToString());
    //                            crRowWs[Name[TDKH.COL_DonVi]].SetValueFromText(ctac["DonVi"].ToString());
    //                            //crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValueFromText(ctac["MaHieuCongTac"].ToString());
    //                            crRowWs[Name["CodeDanhMucCongTac"]].SetValueFromText(ctac["CodeDanhMucCongTac"].ToString());
    //                            //crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValueFromText(ctac["MaHieuCongTac"].ToString());
    //                            crRowWs[Name[TDKH.COL_GhiChu]].SetValueFromText(ctac["GhiChu"].ToString());
    //                            crRowWs[Name[TDKH.COL_DBC_KhoiLuongToanBo]].SetValue(ctac["KhoiLuongToanBo"]);
    //                            crRowWs[Name[TDKH.COL_KhoiLuongHopDongChiTiet]].SetValue(ctac["KhoiLuongHopDongChiTiet"]);
    //                            crRowWs[Name[TDKH.COL_TypeRow]].SetValueFromText("CONGTAC");
    //                            if (ce_ChiDocNhom.Checked)
    //                                crRowWs.Visible = false;
    //                            foreach (var item in CotAn)
    //                            {
    //                                if (ctac.Table.Columns.Contains(item.ToString()))
    //                                    crRowWs[Name[item.ToString()]].SetValueFromText(ctac[item.ToString()].ToString());
    //                            }
    //                            if (ctac["GhiChuBoSungJson"] != DBNull.Value)
    //                            {
    //                                var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(ctac["GhiChuBoSungJson"].ToString());
    //                                crRowWs[Name[TDKH.COL_STTDocVao]].SetValueFromText(GhiChuBoSungJson.STT);
    //                                crRowWs[Name[TDKH.COL_STTND]].SetValueFromText(GhiChuBoSungJson.STTND);
    //                            }
    //                            crRowWs[Name[TDKH.COL_SoNgayThiCong]].Formula = $"{crRowWs[Name[TDKH.COL_NgayKetThucThiCong]].GetReferenceA1()}-{crRowWs[Name[TDKH.COL_NgayBatDauThiCong]].GetReferenceA1()}+1";
    //                            crRowWs[Name[TDKH.COL_SoNgayThucHien]].Formula = $"{crRowWs[Name[TDKH.COL_NgayKetThuc]].GetReferenceA1()}-{crRowWs[Name[TDKH.COL_NgayBatDau]].GetReferenceA1()}+1";
    //                            crRowWs[Name[TDKH.COL_PhanTramThucHien]].Formula =
    //                                $"IF({crRowWs[Name[TDKH.COL_DBC_KhoiLuongToanBo]].GetReferenceA1()}= 0;0;{crRowWs[Name[TDKH.COL_KhoiLuongDaThiCong]].GetReferenceA1()}/{crRowWs[Name[TDKH.COL_DBC_KhoiLuongToanBo]].GetReferenceA1()})";
    //                            //newRowCTac[TDKH.COL_PhanTramThucHien] = $"IF({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} = 0;0;{prefixFormula}{dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}/{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1})";
    //                            //newRowCTac[TDKH.COL_DonGia] = ctac["DonGia"];
    //                            //newRowCTac[TDKH.COL_DonGiaThiCong] = ctac["DonGiaThiCong"];
    //                            //newRowCTac[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{ctac["NgayBatDau"]}"; //ctac["NgayBatDau"];
    //                            //newRowCTac[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{ctac["NgayKetThuc"]}"; //ctac["NgayKetThuc"];
    //                            //newRowCTac[TDKH.COL_SoNgayThucHien] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThuc]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDau]}{crRowInd + 1} + 1";
    //                            //newRowCTac[TDKH.COL_SoNgayThiCong] = $"{prefixFormula}{dic[TDKH.COL_NgayKetThucThiCong]}{crRowInd + 1} - {dic[TDKH.COL_NgayBatDauThiCong]}{crRowInd + 1} + 1";
    //                            //newRowCTac[TDKH.COL_TrangThai] = ctac["TrangThai"];
    //                            //newRowCTac[TDKH.COL_NhanCong] = ctac["NhanCongReal"];
    //                            //newRowCTac[TDKH.COL_GiaTri] = $"{prefixFormula}ROUND({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1}*{dic[TDKH.COL_DonGia]}{crRowInd + 1}; 0)";
    //                            //newRowCTac[TDKH.COL_KinhPhiDuKien] = $"{prefixFormula}ROUND({dic[TDKH.COL_GiaTri]}{crRowInd + 1}/{dic[TDKH.COL_GiaTri]}{fstInd + 1}*{TDKH.RANGE_KinhPhiPhanBoToanDuAn}; 0)";

    //                            //newRowCTac[TDKH.COL_GhiChu] = ctac["GhiChu"];
    //                        }

    //                    }
    //                    if (isPhanTuyen)
    //                    {
    //                        crRowWs = ws.Rows[RowIndex++];
    //                        ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
    //                        crRowWs.Font.Bold = true;
    //                        crRowWs.Font.Color = MyConstant.color_Row_PhanTuyen;
    //                        crRowWs[Name[TDKH.COL_Code]].SetValueFromText(codePT);
    //                        crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(MyConstant.CONST_TYPE_HoanThanhPhanTuyen);
    //                        crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue($"Hoàn thành {fstPT["TenPhanTuyen"]}".ToUpper());
    //                        crRowWs[Name[TDKH.COL_TypeRow]].SetValueFromText(MyConstant.TYPEROW_HTPhanTuyen);
    //                    }
    //                }

    //            }

    //        }
    //        spsheet_TongHop.EndUpdate();
            //WaitFormHelper.CloseWaitForm();
        }
        private void bt_Export_Click(object sender, EventArgs e)
        {
            //List<DonViThucHien> DVTH =SharedControls.ctrl_DonViThucHienDuAnTDKH.DataSource as List<DonViThucHien>;
            //List<DonViThucHien> ThauPhu = DVTH.Where(x => !x.IsGiaoThau).ToList();
            //List<string> lst = new List<string>();
            //foreach(var item in ThauPhu)
            //{
            //    lst.Clear();
            //    string dbString = $"SELECT cttk.*,NHOM.Ten as TenNhom,NHOM.GhiChuBoSungJson FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
            //        $"LEFT JOIN {TDKH.TBL_NhomCongTac} NHOM ON cttk.CodeNhom=NHOM.Code  WHERE cttk.{item.ColCodeFK}='{item.Code}'";
            //    DataTable dttheoky = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //    var grNhom = dttheoky.AsEnumerable().GroupBy(x => x["CodeNhom"]);
            //    foreach(var crow in grNhom)
            //    {
            //        if (lst.Contains(crow.Key.ToString()))
            //            continue;
            //        dbString = $"SELECT NHOM.*,cttk.Code as CodeCT FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
            //            $" LEFT JOIN {TDKH.TBL_NhomCongTac} NHOM ON cttk.CodeNhom=NHOM.Code AND NHOM.Ten='{crow.FirstOrDefault()["TenNhom"]}' " +
            //            $"AND NHOM.GhiChuBoSungJson='{crow.FirstOrDefault()["GhiChuBoSungJson"]}' " +
            //            $"WHERE cttk.{item.ColCodeFK}='{item.Code}' AND NHOM.Code<>'{crow.Key}'";
            //        DataTable Nhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //        foreach(DataRow row in Nhom.Rows)
            //        {
            //            dbString = $"UPDATE {TDKH.TBL_ChiTietCongTacTheoKy} SET \"CodeNhom\"='{crow.Key}' " +
            //      $" WHERE \"Code\"='{row["CodeCT"]}'";
            //            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            //            if (!lst.Contains(row["Code"].ToString()))
            //                lst.Add(row["Code"].ToString());
            //        }
            //    }
            //    DuAnHelper.DeleteDataRows(TDKH.TBL_NhomCongTac, lst.ToArray());
            //} 

            DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Vui lòng bấm vào nút XEM TRƯỚC trước khi xuất File!!!!! Bạn có muốn tiếp tục không??????????");
            if (dialogResult == DialogResult.No)
                return;
            //var saveFileDialog = SharedControls.saveFileDialog;

            //saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
            //saveFileDialog.FileName = $"Báo cáo thi công hằng ngày -{ctrl_DonViThucHien.SelectedDVTH.Ten}_{DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.xlsx";
            //saveFileDialog.FilterIndex = 0;
            //saveFileDialog.RestoreDirectory = true;
            //saveFileDialog.CreatePrompt = true;
            //saveFileDialog.Title = "Xuất báo cáo hằng ngày";
            XtraFolderBrowserDialog Xtra = new XtraFolderBrowserDialog();
            string PathSave = "";
            if (Xtra.ShowDialog() == DialogResult.OK)
            {
                PathSave = Xtra.SelectedPath;
            }
            else
                return;
            var crDVTH = ctrl_DonViThucHien.SelectedDVTH;
            string TenDuAn = FileHelper.RemoveInvalidChars($"Báo cáo thi công hằng ngày _{ctrl_DonViThucHien.SelectedDVTH.Ten}_{DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.xlsx");
            Workbook wb = new Workbook();
            var ws = wb.Worksheets[0];
            ws.Name = $"Báo cáo hằng ngày";
            ws.CopyFrom(spsheet_TongHop.ActiveWorksheet);
            wb.SaveDocument(Path.Combine(PathSave, TenDuAn), DocumentFormat.Xlsx);
            wb.Dispose();
            dialogResult = MessageShower.ShowYesNoQuestion("File lưu thành công. Bạn có muốn mở file luôn hay không ???", "Thông báo");
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(Path.Combine(PathSave, TenDuAn));
            }
        }
        public void Fcn_UpDateData()
        {
            WaitFormHelper.ShowWaitForm($"Đang phân tích dữ liệu", "Đang tổng hợp dữ liệu");
            spsheet_TongHop.BeginUpdate();
            IWorkbook wb = spsheet_TongHop.Document;
            Worksheet ws = wb.Worksheets[0];
            CellRange Data = ws.Range["Data"];
            CellRange DataMau = spsheet_TongHop.Document.Worksheets[1].Range["DataMau"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(Data);
            //foreach (var str in Name)
            //{
            //    if (str.Key.Remove(2) == "KL" || str.Key.Remove(2) == "TT" || str.Key.Remove(2) == "DG")
            //    {
            //        Name = MyFunction.fcn_getDicOfColumn(ws.Range["Data"]);
            //        ws.Columns.Remove(Name[str.Key]);
            //    }

            //}
            DateTime Min = de_Begin.DateTime;
            DateTime Max = de_End.DateTime;
            List<object> CotAn = ccbe_CotAn.Properties.Items.GetCheckedValues();
            bool AnDonGia = CotAn.Where(x => x.ToString() == "DonGiaHangNgay").Any() ? true : false;
            for (DateTime i = Min.Date; i <= Max.Date; i = i.AddDays(1))
            {
                ws.Columns.Insert(ws.Range["RangeNgay"].RightColumnIndex, 2, ColumnFormatMode.FormatAsPrevious);
                ws.Rows[9][ws.Range["RangeNgay"].RightColumnIndex - 2].CopyFrom(DataMau, PasteSpecial.All);
                ws.Rows[9][ws.Range["RangeNgay"].RightColumnIndex - 2].SetValueFromText(i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
                ws.Rows[0][ws.Range["RangeNgay"].RightColumnIndex - 2].SetValueFromText($"KL_{i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}");
                ws.Rows[0][ws.Range["RangeNgay"].RightColumnIndex - 1].SetValueFromText($"DG_{i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}");
            }
            Data = ws.Range["Data"];
            Name = MyFunction.fcn_getDicOfColumn(Data);
            if (!AnDonGia)
            {
                foreach(var item in Name)
                {
                    if (item.Key.Contains("DG_"))
                        ws.Columns[item.Value].Visible = false;
                }
            }

            int IndSumIf = Data.TopRowIndex - 1;
            var klhnsInCTac = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lstCodeCtac, dateBD: Min, dateKT: Max);
            var klhnsInNhom = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, lstCodeNhom, dateBD: Min, dateKT: Max);
            for (int i = Data.TopRowIndex; i <= Data.BottomRowIndex; i++)
            {
                Row crRowWs = ws.Rows[i];
                if (!crRowWs.Visible)
                    continue;
                string TypeRow = crRowWs[Name[TDKH.COL_TypeRow]].Value.TextValue;
                string Code = crRowWs[Name[TDKH.COL_Code]].Value.TextValue;
                if (string.IsNullOrEmpty(Code))
                {
                    continue;
                }
                WaitFormHelper.ShowWaitForm($"{crRowWs[Name[TDKH.COL_DanhMucCongTac]].Value.TextValue}", "Đang tổng hợp dữ liệu");
                //string Fomular = string.Empty;
                    var firstCol = Name[$"KL_{Min.Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"];
                    var lastCol = Name[$"KL_{Max.Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"];
                    crRowWs[Name["KhoiLuongDaThiCong"]].Formula = $"SUMIF({firstCol}{IndSumIf + 1}:{lastCol}{IndSumIf + 1};\"Khối lượng\";{firstCol}{i + 1}:{lastCol}{i + 1})";
                if (TypeRow == MyConstant.TYPEROW_Nhom)
                {
                    if (crRowWs[Name["KhoiLuongToanBo"]].Value.NumericValue == 0)
                        continue;
                    //var klhnsInNhom = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, new string[] { Code }, dateBD: Min, dateKT: Max);
                    //for (DateTime j = Min.Date; j <= Max.Date; j = j.AddDays(1))
                    //{
                    //    //Fomular += $"+{ crRowWs[Name[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].GetReferenceA1()}";
                    //    var hn = klhnsInNhom.SingleOrDefault(x => x.Ngay.Date == j);
                    //    if (hn != null)
                    //    {
                    //        crRowWs[Name[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(hn.KhoiLuongThiCong);
                    //        //crRowWs[Name[$"DG_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(hn.DonGiaThiCong);
                    //    }
                    //}
                    if (klhnsInNhom.Any())
                    {
                        var klhns = klhnsInNhom.Where(x => x.ParentCode == Code && x.KhoiLuongThiCong.HasValue);
                        foreach (var item in klhns)
                        {
                            //if (!item.KhoiLuongThiCong.HasValue)
                            //    continue;
                            crRowWs[Name[$"KL_{item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(item.KhoiLuongThiCong);
                        }
                    }

                }
                else
                {
                    if (crRowWs[Name[TDKH.COL_NgayBatDauThiCong]].Value.IsEmpty)
                    {
                        crRowWs[Name[TDKH.COL_NgayBatDauThiCong]].SetValueFromText(de_Begin.DateTime.ToString());
                        crRowWs[Name[TDKH.COL_NgayKetThucThiCong]].SetValueFromText(de_End.DateTime.ToString());
                    }
                    if (TypeRow ==MyConstant.TYPEROW_CVCha)
                    {
                        if (klhnsInCTac.Any())
                        {
                            var klhns = klhnsInCTac.Where(x => x.ParentCode == Code && x.KhoiLuongThiCong.HasValue);
                            foreach (var item in klhns)
                            {
                                crRowWs[Name[$"KL_{item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(item.KhoiLuongThiCong);
                            }
                        }
                        //var klhnsInCTac = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, new string[] { Code }, dateBD: Min, dateKT: Max);

                        //for (DateTime j = Min.Date; j <= Max.Date; j = j.AddDays(1))
                        //{
                        //    var hn = klhnsInCTac.SingleOrDefault(x => x.Ngay.Date == j);
                        //    //Fomular += $"+{ crRowWs[Name[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].GetReferenceA1()}";
                        //    if (hn != null)
                        //    {
                        //        crRowWs[Name[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(hn.KhoiLuongThiCong);
                        //        //crRowWs[Name[$"DG_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(hn.DonGiaThiCong);
                        //    }
                        //}
                        //crRowWs[Name["KhoiLuongDaThiCong"]].Formula = Fomular;
                    }
                }
            }
            ws.Rows[4]["F"].SetValueFromText(Min.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
            ws.Rows[4]["Z"].SetValueFromText(Max.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
            spsheet_TongHop.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }

        private void XtraForm_BaoCaoHangNgayTDKH_Load(object sender, EventArgs e)
        {
            List<DonViThucHien> DVTH = DuAnHelper.GetDonViThucHiens();
            if (DVTH is null)
            {
                this.Close();
                return;
            }
            DVTH.Remove(DVTH.Where(x => x.IsGiaoThau == true).FirstOrDefault());
            ctrl_DonViThucHien.DVTHChanged -= ctrl_DonViThucHien_DVTHChanged;
            ctrl_DonViThucHien.DataSource = DVTH;
            if (!SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH.IsGiaoThau)
                ctrl_DonViThucHien.EditValue = SharedControls.ctrl_DonViThucHienDuAnTDKH.EditValue;
            de_Begin.DateTime = DateTime.Now;
            DateTime.Now.AddDays(30);
            Fcn_UpdateCongTac(true);
            ctrl_DonViThucHien.DVTHChanged += ctrl_DonViThucHien_DVTHChanged;
        }

        private void ctrl_DonViThucHien_DVTHChanged(object sender, EventArgs e)
        {
            Fcn_UpdateCongTac(true);
        }

        private void sb_LamMoi_Click(object sender, EventArgs e)
        {
            Fcn_UpdateCongTac(false);
            Fcn_UpDateData();
        }

        private void ce_ChiDocNhom_CheckedChanged(object sender, EventArgs e)
        {
            spsheet_TongHop.BeginUpdate();
            IWorkbook wb = spsheet_TongHop.Document;
            Worksheet ws = wb.Worksheets[0];
            CellRange Data = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(Data);
            Column TypeRow = ws.Columns[Name[TDKH.COL_TypeRow]];
            IEnumerable<Cell> searchResult = TypeRow.Search("CONGTAC");
            foreach (Cell cell in searchResult)
            {
                ws.Rows[cell.RowIndex].Visible = !ce_ChiDocNhom.Checked;
            }
            spsheet_TongHop.EndUpdate();
        }

        private void ccbe_CotAn_EditValueChanged(object sender, EventArgs e)
        {
            Fcn_UpdateCongTac(false);

        }
    }
}
