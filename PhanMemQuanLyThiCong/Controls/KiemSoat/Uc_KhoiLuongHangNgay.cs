using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Menu;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
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
using System.Xml.Serialization;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Uc_KhoiLuongHangNgay : DevExpress.XtraEditors.XtraUserControl
    {
        private string _CodeNgoaiKH { get; set; }
        public Uc_KhoiLuongHangNgay()
        {
            InitializeComponent();
        }

        private void Uc_KhoiLuongHangNgay_Load(object sender, EventArgs e)
        {
            de_Begin.DateTime = DateTime.Now.AddDays(-2);
            de_End.DateTime = DateTime.Now;
        }
        public void Fcn_LoadDaTa()
        {
            var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            FileHelper.fcn_spSheetStreamDocument(spsheet_TongHop, $@"{BaseFrom.m_templatePath}\FileExcel\16a.BaoCaoHangNgayTDKH.xlsx");
            if (crDVTH.IsGiaoThau)
            {
                MessageShower.ShowWarning("Vui lòng chọn ĐƠN VỊ NHẬN THẦU để thực hiện tính năng nhập khối lượng hằng ngày!!!!!!");
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu");
            XmlSerializer deserializer = new XmlSerializer(typeof(List<KHHN_CongTacChonNgoaiKeHoach>), new XmlRootAttribute("CongTacChonNgoaiKeHoach"));
            List<KHHN_CongTacChonNgoaiKeHoach> LstTong = new List<KHHN_CongTacChonNgoaiKeHoach>();
            var xml = Path.Combine(BaseFrom.m_FullTempathDA, "DuLieuTam.xml");
            if (!File.Exists(xml))
            {
                File.Copy(Path.Combine(BaseFrom.m_templatePath, "DuAnMau", "DuLieuTam.xml"),
                    Path.Combine(BaseFrom.m_FullTempathDA, "DuLieuTam.xml"));
            }
            List<KHHN_CongTacChonNgoaiKeHoach> Test = new List<KHHN_CongTacChonNgoaiKeHoach>();
            var Reader = new StreamReader(xml);
            if (Reader.BaseStream.Length > 0)
            {
                Test = deserializer.Deserialize(Reader) as List<KHHN_CongTacChonNgoaiKeHoach>;
            }
            Reader.Close();
            Reader.Dispose();
            string Condition = string.Empty;
            if (Test.Any())
            {
                KHHN_CongTacChonNgoaiKeHoach Idnd = Test.Where(x => x.IdNguoiDung == BaseFrom.BanQuyenKeyInfo.SerialNo
                && x.CodeDVTH == crDVTH.Code && x.CodeDuAn == SharedControls.slke_ThongTinDuAn.EditValue.ToString()).Any() ?
                    Test.Where(x => x.IdNguoiDung == BaseFrom.BanQuyenKeyInfo.SerialNo).FirstOrDefault() : null;
                _CodeNgoaiKH = Condition = Idnd is null ? string.Empty : MyFunction.fcn_Array2listQueryCondition(Idnd.CodeCongTacTheoGiaiDoan);
            }
            BaseFrom.IsLoadKLHN = true;
            IWorkbook wb = spsheet_TongHop.Document;
            Worksheet ws = wb.Worksheets[0];
            CellRange Data = ws.Range["Data"];
            CellRange DataMau = spsheet_TongHop.Document.Worksheets[1].Range["DataMau"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(Data);
            DateTime Min = de_Begin.DateTime;
            DateTime Max = de_End.DateTime;
            spsheet_TongHop.BeginUpdate();
            ws.Rows[1]["F"].SetValueFromText($"{SharedControls.slke_ThongTinDuAn.Text}");
            ws.Rows[3]["F"].SetValueFromText($"{crDVTH.Ten}");
            ws.Rows[3][Name["Code"]].SetValueFromText($"{crDVTH.Code}");
            ws.Rows[4]["F"].SetValueFromText(Min.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
            ws.Rows[4]["I"].SetValueFromText(Max.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
            for (DateTime i = Min.Date; i <= Max.Date; i = i.AddDays(1))
            {
                ws.Columns.Insert(ws.Range["RangeNgay"].RightColumnIndex, 2, ColumnFormatMode.FormatAsPrevious);
                ws.Rows[9][ws.Range["RangeNgay"].RightColumnIndex - 2].CopyFrom(DataMau, PasteSpecial.All);
                ws.Rows[9][ws.Range["RangeNgay"].RightColumnIndex - 2].SetValueFromText(i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
                ws.Rows[0][ws.Range["RangeNgay"].RightColumnIndex - 2].SetValueFromText($"KL_{i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}");
                ws.Rows[0][ws.Range["RangeNgay"].RightColumnIndex - 1].SetValueFromText($"DG_{i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}");
                //ws.Columns[ws.Range["RangeNgay"].RightColumnIndex - 1].Visible=false;
            }
            Data = ws.Range["Data"];
            Name = MyFunction.fcn_getDicOfColumn(Data);
            foreach (var item in Name)
            {
                if (item.Key.Contains("DG_"))
                    ws.Columns[item.Value].Visible = false;
            }
            DataTable dtData = new DataTable();
            foreach (var item in Name)
            {
                dtData.Columns.Add(item.Key, typeof(object));
            }
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);
            DataTable CongTacs = TDKHHelper.GetCongTacsDataTable(SharedControls.slke_ThongTinDuAn.EditValue?.ToString(),
                codeCT, codeHM, SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString(), $"cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}'", GetAllHM: false, NBD: de_Begin.DateTime, NKT: de_End.DateTime, ConditonNKH: Condition);
            var codeNhoms = CongTacs.AsEnumerable().Select(x => x["CodeNhom"].ToString()).Distinct().ToArray();
            var CodeCtac = CongTacs.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            string strCodeNhom = MyFunction.fcn_Array2listQueryCondition(codeNhoms);

            string dbString = $"SELECT * FROM {TDKH.TBL_NhomCongTac} WHERE Code IN ({strCodeNhom})";
            DataTable dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string mindateStr = CongTacs.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Min(x => StringHelper.Min((string)x["NgayBatDau"], (x["NgayBatDauThiCong"] == DBNull.Value) ? (string)x["NgayBatDau"] : (string)x["NgayBatDauThiCong"]));
            string maxdateStr = CongTacs.AsEnumerable().Where(x => x["Code"] != DBNull.Value).Max(x => StringHelper.Max((string)x["NgayKetThuc"], (x["NgayKetThucThiCong"] == DBNull.Value) ? (string)x["NgayKetThuc"] : (string)x["NgayKetThucThiCong"]));
            if (!DateTime.TryParse(mindateStr, out DateTime MinNew) || !DateTime.TryParse(maxdateStr, out DateTime MaxNew))
            {
                MessageShower.ShowWarning("Nhà thầu không có công tác trong ngày kế hoạch để tổng hợp");
                spsheet_TongHop.EndUpdate();
                return;
            }
            string prefixFormula = MyConstant.PrefixFormula;
            var grsCTrinh = CongTacs.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);
            var crRowInd = Data.TopRowIndex + 1;
            int STTCTrinh = 0;
            var klhnsInCTac = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, CodeCtac, dateBD: Min, dateKT: Max);
            var klhnsInNhom = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, codeNhoms, dateBD: Min, dateKT: Max);
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
                bool Nhom = false;
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
                            Nhom = false;
                            var firstCol = Name[$"KL_{Min.Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"];
                            var lastCol = Name[$"KL_{Max.Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"];
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
                                    Nhom = true;
                                    newRowNhom[TDKH.COL_DBC_KhoiLuongToanBo] = drNhom["KhoiLuongKeHoach"];
                                    newRowNhom[TDKH.COL_TrangThai] = drNhom["TrangThai"];
                                    newRowNhom["KhoiLuongDaThiCong"] = $"{prefixFormula}SUMIF({firstCol}{IndSumIf + 1}:{lastCol}{IndSumIf + 1};\"Khối lượng\";{firstCol}{crRowNhomInd}:{lastCol}{crRowNhomInd})";
                                    if (klhnsInNhom.Any())
                                    {
                                        var klhns = klhnsInNhom.Where(x => x.ParentCode == crCodeNhom && x.KhoiLuongThiCong.HasValue);
                                        foreach (var item in klhns)
                                        {
                                            //if (!item.KhoiLuongThiCong.HasValue)
                                            //    continue;
                                            newRowNhom[$"KL_{item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"] = item.KhoiLuongThiCong;
                                        }
                                    }
                                    //for (DateTime j = Min.Date; j <= Max.Date; j = j.AddDays(1))
                                    //{
                                    //    //Fomular += $"+{ crRowWs[Name[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].GetReferenceA1()}";
                                    //    var hn = klhnsInNhom.SingleOrDefault(x => x.Ngay.Date == j);
                                    //    if (hn != null)
                                    //    {
                                    //        newRowNhom[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"] = hn.KhoiLuongThiCong;
                                    //        //crRowWs[Name[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(hn.KhoiLuongThiCong);
                                    //        //crRowWs[Name[$"DG_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(hn.DonGiaThiCong);
                                    //    }
                                    //}
                                }
                            }
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
                                //newRowCTac[TDKH.COL_KhoiLuongHopDongChiTiet] = ctac["KhoiLuongHopDongChiTiet"];
                                newRowCTac[TDKH.COL_TrangThai] = ctac["TrangThai"];
                                if (ctac["GhiChuBoSungJson"] != DBNull.Value)
                                {
                                    var GhiChuBoSungJson = JsonConvert.DeserializeObject<TDKH_GhiChuBoSungJson>(ctac["GhiChuBoSungJson"].ToString());
                                    newRowCTac[TDKH.COL_STTDocVao] = GhiChuBoSungJson.STT;
                                    newRowCTac[TDKH.COL_STTND] = GhiChuBoSungJson.STTND;
                                }
                                newRowCTac["KhoiLuongDaThiCong"] = $"{prefixFormula}SUMIF({firstCol}{IndSumIf + 1}:{lastCol}{IndSumIf + 1};\"Khối lượng\";{firstCol}{crrowIndCt}:{lastCol}{crrowIndCt})";
                                if (klhnsInCTac.Any())
                                {
                                    var klhns = klhnsInCTac.Where(x => x.ParentCode == ctac["Code"].ToString() && x.KhoiLuongThiCong.HasValue);
                                    foreach (var item in klhns)
                                    {
                                        //if (!item.KhoiLuongThiCong.HasValue)
                                        //    continue;
                                        newRowCTac[$"KL_{item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"] = item.KhoiLuongThiCong;
                                    }
                                }
                                //for (DateTime j = Min.Date; j <= Max.Date; j = j.AddDays(1))
                                //{
                                //    var hn = klhnsInCTac.SingleOrDefault(x => x.Ngay.Date == j);
                                //    //Fomular += $"+{ crRowWs[Name[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].GetReferenceA1()}";
                                //    if (hn != null)
                                //    {
                                //        newRowCTac[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"] = hn.KhoiLuongThiCong;
                                //        //crRowWs[Name[$"KL_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(hn.KhoiLuongThiCong);
                                //        //crRowWs[Name[$"DG_{j.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}"]].SetValue(hn.DonGiaThiCong);
                                //    }
                                //}
                                //newRowCTac[TDKH.COL_SoNgayThiCong] = $"{Name[TDKH.COL_NgayKetThucThiCong]}{crRowInd}-{Name[TDKH.COL_NgayBatDauThiCong]}{crRowInd}+1";
                                //crRowWs[Name[TDKH.COL_SoNgayThiCong]].Formula = $"{crRowWs[Name[TDKH.COL_NgayKetThucThiCong]].GetReferenceA1()}-{crRowWs[Name[TDKH.COL_NgayBatDauThiCong]].GetReferenceA1()}+1";
                                //crRowWs[Name[TDKH.COL_SoNgayThucHien]].Formula = $"{crRowWs[Name[TDKH.COL_NgayKetThuc]].GetReferenceA1()}-{crRowWs[Name[TDKH.COL_NgayBatDau]].GetReferenceA1()}+1";
                                //crRowWs[Name[TDKH.COL_PhanTramThucHien]].Formula =
                                //    $"IF({crRowWs[Name[TDKH.COL_DBC_KhoiLuongToanBo]].GetReferenceA1()}= 0;0;{crRowWs[Name[TDKH.COL_KhoiLuongDaThiCong]].GetReferenceA1()}/{crRowWs[Name[TDKH.COL_DBC_KhoiLuongToanBo]].GetReferenceA1()})";

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
            //spsheet_TongHop.Document.History.Clear();
            WaitFormHelper.CloseWaitForm();
        }

        private void sb_ReadExcel_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang Lưu dữ liệu vào!!!!!", "Vui Lòng chờ!");
            IWorkbook wb = spsheet_TongHop.Document;
            Worksheet ws = wb.Worksheets[0];
            CellRange Data = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(Data);
            Dictionary<KeyValuePair<string, int>, double> dic_In = new Dictionary<KeyValuePair<string, int>, double>();
            spsheet_TongHop.BeginUpdate();
            Dictionary<string, string> NameNgay = Name.Where(x => x.Key.Contains("KL_")).ToDictionary(x => x.Key, y => y.Value);

            string DbString = string.Empty;
            List<string> lstCodeCTac = new List<string>();
            List<string> lstCodeNhom = new List<string>();
            bool isCapNhap = false;
            string Code = string.Empty;
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
                    //RowNhom = i;
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
                    WaitFormHelper.ShowWaitForm("Đang cập nhật lại khối lượng thi công");
                    //TempWorkLoadHelper.CalcTempGroupWorkLoad(Code);
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
                    if (TypeRow == MyConstant.TYPEROW_CVCha)
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
                        WaitFormHelper.ShowWaitForm("Đang cập nhật lại khối lượng thi công");
                        //TempWorkLoadHelper.CalcTempJobWorkLoad(Code);
                        if (isCapNhap)
                            lstCodeCTac.Add(Code);
                    }
                }
            }
            DbString = $"DELETE FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE KhoiLuongKeHoach IS NULL AND KhoiLuongThiCong IS NULL AND KhoiLuongBoSung IS NULL ";
            DataProvider.InstanceTHDA.ExecuteNonQuery(DbString);
            spsheet_TongHop.EndUpdate();
            WaitFormHelper.CloseWaitForm();

            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn cập nhật lại trạng thái công tác theo khối lượng thi công không????");
            if (rs == DialogResult.Yes)
            {
                WaitFormHelper.ShowWaitForm("Đang cập nhật lại trạng thái theo khối lượng thi công");
                DuAnHelper.UpdateStateTDKHByKhoiLuongThiCong(lstCodeCTac.ToArray());
            }
            MessageShower.ShowInformation("Lưu dữ liệu hoàn tất!!!!!!!!!!!!");
            WaitFormHelper.CloseWaitForm();
        }

        private void spsheet_TongHop_CellBeginEdit(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellCancelEventArgs e)
        {
            IWorkbook wb = e.Worksheet.Workbook;
            Worksheet ws = e.Worksheet;
            if (!ws.Range["Data"].Contains(e.Cell)/* || string.IsNullOrEmpty(colInDb)*/)
            {
                e.Cancel = true;
                return;
            }
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range["Data"]);
            string TypeRow = ws.Rows[e.RowIndex][Name[TDKH.COL_TypeRow]].Value.ToString();
            if (TypeRow == MyConstant.TYPEROW_Nhom || TypeRow == MyConstant.TYPEROW_CVCha)
            {
                string TrangThai = ws.Rows[e.RowIndex][Name[TDKH.COL_TrangThai]].Value.ToString();
                if (TrangThai != "Đang thực hiện")
                {
                    MessageShower.ShowError("Vui lòng thay đổi trạng thái thành Đang thực hiện!!!");
                    e.Cancel = true;
                    return;
                }

            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        private void spsheet_TongHop_PopupMenuShowing(object sender, DevExpress.XtraSpreadsheet.PopupMenuShowingEventArgs e)
        {
            e.Menu.Items.Clear();
            var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            if (crDVTH.IsGiaoThau)
            {
                MessageShower.ShowWarning("Vui lòng chọn ĐƠN VỊ NHẬN THẦU để thực hiện tính năng nhập khối lượng hằng ngày!!!!!!");
                return;
            }
            SpreadsheetMenuItem Item_LayDuLieuTuTDKH = new SpreadsheetMenuItem("Thêm công tác ngoài kế hoạch", new EventHandler(fcn_handle_GV_GetDuLieuGVTuTDKH));
            e.Menu.Items.Add(Item_LayDuLieuTuTDKH);

        }
        private void fcn_handle_GV_GetDuLieuGVTuTDKH(object sender, EventArgs e)
        {
            XtraForm_LayCongTacKhoiLuongHangNgay form = new XtraForm_LayCongTacKhoiLuongHangNgay();
            form.Fcn_LoadData(_CodeNgoaiKH, de_Begin.DateTime, de_End.DateTime);
            form.ShowDialog();
            Fcn_LoadDaTa();
        }

        private void sb_LamMoiDuLieu_Click(object sender, EventArgs e)
        {
            Fcn_LoadDaTa();
        }

        private void bt_Export_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Vui lòng bấm vào nút XEM TRƯỚC trước khi xuất File!!!!! Bạn có muốn tiếp tục không??????????");
            if (dialogResult == DialogResult.No)
                return;
            XtraFolderBrowserDialog Xtra = new XtraFolderBrowserDialog();
            string PathSave = "";
            if (Xtra.ShowDialog() == DialogResult.OK)
            {
                PathSave = Xtra.SelectedPath;
            }
            else
                return;
            var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            string TenDuAn = FileHelper.RemoveInvalidChars($"Báo cáo thi công hằng ngày _{SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH.Ten}_{DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.xlsx");
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
    }
}
