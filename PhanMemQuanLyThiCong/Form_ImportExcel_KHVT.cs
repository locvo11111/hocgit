using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_ImportExcel_KHVT : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<Control, string> dic_VT;
        Dictionary<int, string> dic_congtrinh = new Dictionary<int, string>();
        Dictionary<int, string> dic_hangmuc = new Dictionary<int, string>();
        private List<string> lstSheetNames;
        static string codehangmuc, tenhangmuc, tencongtrinh = "", codecongtrinh, m_CodeDuAn, m_codegiadoan;
        public Form_ImportExcel_KHVT()
        {
            InitializeComponent();
        }
        public delegate void DE_TruyenDataDocFileExcel(DataTable dt);
        public DE_TruyenDataDocFileExcel _TruyenDataTuExcel;
        private void Fcn_CheckMau()
        {
            string queryStr = $"SELECT * FROM {MyConstant.TBL_CategoryReadExcel}";
            DataTable dt = DataProvider.InstanceBaoCao.ExecuteQuery(queryStr);
            string NameMau = MyConstant.MauKhongCoSan;
            List<CategoryReadExcel> m_CategoryReadExcel = DuAnHelper.ConvertToList<CategoryReadExcel>(dt);
            Dictionary<string, List<CategoryReadExcel>> myDictionary = m_CategoryReadExcel.GroupBy(o => o.CategoryName).ToDictionary(g => g.Key, g => g.ToList());
            foreach (var item in myDictionary)
            {
                List<string> lst = new List<string>();
                item.Value.ForEach(x => lst.Add(x.SheetNameSoure));
                if (lstSheetNames.Intersect(lst).Count() == lst.Count())
                {
                    NameMau = item.Key;
                    break;
                }
            }
            cbe_MauCongTrinh.Text = NameMau;
        }
        private void Fcn_LoadData()
        {
            IWorkbook wb = spsheet_XemFile.Document;
            lstSheetNames = new List<string>();
            string[] ten = wb.Worksheets.ToList().Select(x => x.Name).ToArray();
            lstSheetNames.AddRange(ten);
            Fcn_CheckMau();
            string NameMau = cbe_MauCongTrinh.Text;
            cbb_VL_tensheet.Properties.Items.AddRange(ten);
            foreach (Control item in dic_VT.Keys)
                item.Text = "";
            if (NameMau == MyConstant.DuToanQLTC)
            {
                cbb_VL_tensheet.Text = "Vật liệu";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbb_VL_tensheet.Text];
                CellRange usedRange = wb.Worksheets[cbb_VL_tensheet.Text].GetUsedRange();
                sp_End.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.DuToanF1)
            {
                cbb_VL_tensheet.Text = "Vật liệu";
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbb_VL_tensheet.Text];
                CellRange usedRange = wb.Worksheets[cbb_VL_tensheet.Text].GetUsedRange();
                sp_End.Value = usedRange.RowCount;
            }
            else if (NameMau == MyConstant.MauKhongCoSan)
            {
                cbb_VL_tensheet.SelectedIndex = 0;
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbb_VL_tensheet.Text];
                CellRange usedRange = wb.Worksheets[cbb_VL_tensheet.Text].GetUsedRange();
                sp_End.Value = usedRange.RowCount;
            }
            Fcn_loadtencot(cbb_VL_tensheet.Text,dic_VT,txt_VT_STT);
        }
        private void fcn_kiemtracongtrinh_hangmuc(int batdau)
        {
            int vitri = 0;
            //string tencongtrinh = "";
            codecongtrinh = Guid.NewGuid().ToString();
            //string tenhangmuc = "";
            foreach (var item in dic_congtrinh)
            {
                if (batdau > item.Key)
                {
                    vitri = item.Key;
                    tencongtrinh = item.Value;
                }
            }
            if (tencongtrinh == "")
                return;
            string queryStr = $"SELECT \"Code\",\"Ten\" FROM {QLVT.TBL_QLVT_KHVT_CongTrinh} WHERE \"Ten\"=@tencongtrinh";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr, parameter: new object[] { tencongtrinh });
            if (dt.Rows.Count == 0)
            {
                string dbString = $"INSERT INTO {QLVT.TBL_QLVT_KHVT_CongTrinh} (\"Code\",\"CodeDuAn\",\"Ten\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten)";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { tencongtrinh });
            }
            else
            {
                codecongtrinh = dt.Rows[0]["Code"].ToString();
            }

            foreach (var key in dic_hangmuc)
            {
                if (batdau > key.Key && key.Key > vitri)
                {
                    tenhangmuc = key.Value;
                }
            }
            string queryStr_hangmuc = $"SELECT \"Code\",\"Ten\" FROM {QLVT.TBL_QLVT_KHVT_HangMuc} WHERE \"Ten\"=@tenhangmuc AND \"CodeCongTrinh\"='{codecongtrinh}'";
            DataTable dt_hangmuc = DataProvider.InstanceTHDA.ExecuteQuery(queryStr_hangmuc, parameter: new object[] { tenhangmuc });
            if (dt_hangmuc.Rows.Count == 0)
            {
                codehangmuc = Guid.NewGuid().ToString();
                string dbString1 = $"INSERT INTO {QLVT.TBL_QLVT_KHVT_HangMuc} (\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{codehangmuc}','{codecongtrinh}',@Ten)";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString1, parameter: new object[] { tenhangmuc });
            }
            else
                codehangmuc = dt_hangmuc.Rows[0]["Code"].ToString();
        }
        private void fcn_truyendataExxcelThuCong_VL(int firstLine, int lastLine)
        {
            foreach (var item in dic_VT)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {QLVT.TBL_QLVT_KHVT}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            dt.Clear();
            Worksheet ws = workbook.Worksheets[cbb_VL_tensheet.Text];


        }
        private void fcn_truyendataExxcel_VL(int firstLine, int lastLine)
        {
            foreach (var item in dic_VT)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu", "Vui Lòng chờ!");
            string queryStr = $"SELECT * FROM {QLVT.TBL_QLVT_KHVT}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            dt.Clear();
            Worksheet ws = spsheet_XemFile.Document.Worksheets.ActiveWorksheet;
            fcn_kiemtracongtrinh_hangmuc(firstLine);
            queryStr = $"SELECT * FROM {QLVT.TBL_QLVT_KHVT_CongTrinh} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {QLVT.TBL_QLVT_KHVT_HangMuc} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0, SortIdHM = dt_HM.Rows.Count, SortIdctr = dt_CTrinh.Rows.Count;
            string TenVL = "", DonVi = "", codetheogiaidoan = "", mahieu = "", TenCongTac = "";

            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                mahieu = crRow[txt_VL_Mahieu.Text].Value.ToString();
                if (mahieu.ToUpper() == MyConstant.CONST_TYPE_HANGMUC)
                {
                    codehangmuc = Guid.NewGuid().ToString();
                    tenhangmuc = TenCongTac;
                    queryStr = $"INSERT INTO {QLVT.TBL_QLVT_KHVT_HangMuc} (\"SortId\",\"Code\",\"CodeCongTrinh\",\"Ten\") VALUES ('{SortIdHM++}','{codehangmuc}','{codecongtrinh}',@Ten)";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tenhangmuc });
                    continue;
                }
                else if (mahieu.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH)
                {
                    codecongtrinh = Guid.NewGuid().ToString();
                    tencongtrinh = TenCongTac;
                    queryStr = $"INSERT INTO {MyConstant.TBL_THONGTINCONGTRINH} (\"Code\",\"CodeDuAn\",\"Ten\",\"SortID\") VALUES ('{codecongtrinh}','{m_CodeDuAn}',@Ten,'{SortIdctr++}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr, parameter: new object[] { tencongtrinh });
                }
                if (Int32.TryParse(crRow[txt_VT_STT.Text].Value.ToString(), out int sott) && txt_VL_Mahieu.Text == "")
                {
                    string code = Guid.NewGuid().ToString();
                    DataRow dtRow = dt.NewRow();
                    dtRow["Code"] = code;
                    dtRow["VatTu"] = crRow[txt_VL_TenVL.Text].Value.ToString();
                    dtRow["TenVatTu_KhongDau"] = MyFunction.fcn_RemoveAccents(crRow[txt_VL_TenVL.Text].Value.ToString());
                    dtRow["CodeHangMuc"] = codehangmuc;
                    dtRow["ThoiGianTu"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dtRow["ThoiGianDen"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dtRow["NhanHieu"] = te_NhanHieu.Text == "" ? default : crRow[te_NhanHieu.Text].Value.ToString();
                    dtRow["QuyCach"] = te_QuyCach.Text == "" ? default : crRow[te_QuyCach.Text].Value.ToString();
                    dtRow["XuatXu"] = te_XuatXu.Text == "" ? default : crRow[te_XuatXu.Text].Value.ToString();
                    dt.Rows.Add(dtRow);
                }
                else if (!Int32.TryParse(crRow[txt_VT_STT.Text].Value.ToString(), out int so) && crRow[txt_VL_Mahieu.Text].Value.ToString() == "")
                {
                    foreach (var item in dic_congtrinh)
                    {
                        if (i < item.Key)
                        {
                            i = item.Key + 4;
                            break;
                        }
                    }
                    fcn_kiemtracongtrinh_hangmuc(i);

                }
                else
                {
                    if (crRow[txt_VL_Mahieu.Text].Value.ToString() != "" && Int32.TryParse(crRow[txt_VT_STT.Text].Value.ToString(), out int sostt))
                    {
                        string code = Guid.NewGuid().ToString();
                        DataRow dtRow = dt.NewRow();
                        dtRow["Code"] = code;
                        dtRow["DonVi"] = crRow[txt_VL_Donvi.Text].Value.ToString();
                        dtRow["MaVatLieu"] = crRow[txt_VL_Mahieu.Text].Value.ToString();
                        dtRow["DonGiaGoc"] = !crRow[txt_VL_Dongiagoc.Text].Value.IsNumeric ? 0 : crRow[txt_VL_Dongiagoc.Text].Value.NumericValue;
                        dtRow["DonGiaKeHoach"] = !crRow[txt_VL_Dongiagoc.Text].Value.IsNumeric ? 0 : crRow[txt_VL_Dongiagoc.Text].Value.NumericValue;
                        dtRow["VatTu"] = crRow[txt_VL_TenVL.Text].Value.ToString();
                        dtRow["TenVatTu_KhongDau"] = MyFunction.fcn_RemoveAccents(crRow[txt_VL_TenVL.Text].Value.ToString());
                        dtRow["KhoiLuongDinhMuc"] = dtRow["KhoiLuongKeHoach"] = !crRow[txt_VL_Khoiluong.Text].Value.IsNumeric ? 0 : crRow[txt_VL_Khoiluong.Text].Value.NumericValue;
                        dtRow["CodeHangMuc"] = codehangmuc;
                        dtRow["ThoiGianTu"] = De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtRow["ThoiGianDen"] = De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtRow["NhanHieu"] = te_NhanHieu.Text == "" ? default : crRow[te_NhanHieu.Text].Value.ToString();
                        dtRow["QuyCach"] = te_QuyCach.Text == "" ? default : crRow[te_QuyCach.Text].Value.ToString();
                        dtRow["XuatXu"] = te_XuatXu.Text == "" ? default : crRow[te_XuatXu.Text].Value.ToString();
                        dt.Rows.Add(dtRow);
                    }
                }
            }
            _TruyenDataTuExcel(dt);
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }
        private void sb_ReadExcel_Click(object sender, EventArgs e)
        {
            fcn_truyendataExxcel_VL((int)sp_Begin.Value - 1, (int)sp_End.Value - 1);

        }

        private void Form_ImportExcel_KHVT_FormClosed(object sender, FormClosedEventArgs e)
        {
            spsheet_XemFile.Dispose();
            GC.Collect();
        }

        public string filePath { get; set; }
        private void sb_ChonLaiFile_Click(object sender, EventArgs e)
        {
            m_openFileDialog.DefaultExt = "xls";
            m_openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            m_openFileDialog.Title = "Chọn file Excel";
            DialogResult rs = m_openFileDialog.ShowDialog();
            if (rs == DialogResult.OK)
            {
                filePath = m_openFileDialog.FileName;
                WaitFormHelper.ShowWaitForm("Đang phân tích File đọc vào, Vui lòng chờ!");
                hLE_File.EditValue = filePath;
                spsheet_XemFile.LoadDocument(filePath);
                Fcn_LoadData();
                WaitFormHelper.CloseWaitForm();
            }
        }

        private void Form_ImportExcel_KHVT_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích File đọc vào, Vui lòng chờ!");
            De_Begin.DateTime = DateTime.Now;
            De_End.DateTime = DateTime.Now.AddDays(30);
            hLE_File.EditValue = filePath;
            spsheet_XemFile.LoadDocument(filePath);
            tencongtrinh = tenhangmuc = codehangmuc = "";
            dic_congtrinh.Clear();
            dic_hangmuc.Clear();
            dic_VT = new Dictionary<Control, string>()
            {
                {txt_VL_Mahieu, "Mã hiệu" },
                {txt_VL_TenVL, "Tên vật tư" },
                {txt_VL_Donvi, "Đơn vị" },
                {txt_VL_Khoiluong, "Khối lượng" },
                {txt_VL_Dongiagoc, "Giá gốc" },
                //{te_QuyCach, "Quy cách" },
                //{te_NhanHieu, "Nhãn hiệu" },
                //{te_Begin, "Ngày bắt đầu" },
                //{te_End, "Ngày kết thúc" },
                //{te_XuatXu, "Xuât xứ" },
                {te_DonGiaKeHoach, "Giá thông báo" }
            };
            m_CodeDuAn = SharedControls.slke_ThongTinDuAn.EditValue.ToString();
            m_codegiadoan = SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString();
            Fcn_LoadData();
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_loadtencot(string worksheet, Dictionary<Control, string> tendic, TextEdit stt)
        {
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[worksheet];
            List<string> tencongtrinh = new List<string>();
            List<string> hangmuc = new List<string>();
            #region SEARCH Công trình, Hạng mục, STT
            string searchstt = "Stt";
            string searchString = "CÔNG TRÌNH";
            string searchhangmuc = "HẠNG MỤC";
            dic_congtrinh.Clear();
            dic_hangmuc.Clear();
            IEnumerable<Cell> searchResult = sheetThongTin.Search(searchString);
            foreach (Cell cell in searchResult)
            {
                if (!cell.Value.ToString().Contains(":")||!cell.Value.ToString().Contains("CÔNG TRÌNH"))
                    continue;
                tencongtrinh.Add(cell.Value.ToString().Substring(11).Replace(":", ""));
                if (!dic_congtrinh.Keys.Contains(cell.RowIndex))
                    dic_congtrinh.Add(cell.RowIndex, cell.Value.ToString().Substring(11).Replace(":", ""));
            }
            IEnumerable<Cell> searchResult_hangmuc = sheetThongTin.Search(searchhangmuc);
            foreach (Cell cell in searchResult_hangmuc)
            {
                if (!cell.Value.ToString().Contains(":") || !cell.Value.ToString().Contains("HẠNG MỤC"))
                    continue;
                hangmuc.Add(cell.Value.ToString().Substring(9).Replace(":", ""));
                if (!dic_hangmuc.Keys.Contains(cell.RowIndex))
                    dic_hangmuc.Add(cell.RowIndex, cell.Value.ToString().Substring(9).Replace(":", ""));
            }
            IEnumerable<Cell> searchResult_STT = MyFunction.SearchRangeCell(sheetThongTin, searchstt);
            foreach (Cell cell in searchResult_STT)
            {
                if (stt.Text == "")
                    stt.Text = sheetThongTin.Columns[cell.ColumnIndex].Heading;
                else
                    break;
            }
            foreach (var item in tendic)
            {
                Cell searchResult_duthau = MyFunction.SearchRangeCell(sheetThongTin, item.Value).Where(x => x.Value.ToString().CompareTo(item.Value) == 0).FirstOrDefault();
                if (searchResult_duthau == null)
                    continue;
                item.Key.Text = sheetThongTin.Columns[searchResult_duthau.ColumnIndex].Heading;
            }
            #endregion
        }
    }
}