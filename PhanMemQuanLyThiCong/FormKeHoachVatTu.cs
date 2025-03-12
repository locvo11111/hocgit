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
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;

namespace PhanMemQuanLyThiCong
{
    public partial class FormKeHoachVatTu : Form
    {
        string m_duongdan;
        string m_codeduan, m_codegiadoan;
        Dictionary<Control, string> dic_VT;
        Dictionary<int, string> dic_congtrinh = new Dictionary<int, string>();
        Dictionary<int, string> dic_hangmuc = new Dictionary<int, string>();
        public FormKeHoachVatTu(string codeduan, string codegiadoan)
        {
            InitializeComponent();
            m_codeduan = codeduan;
            m_codegiadoan = codegiadoan;
        }
        public delegate void DE_TruyenDataDocFileExcel(DataTable dt);
        public DE_TruyenDataDocFileExcel _TruyenDataTuExcel;
        private void btn_dongy_Click(object sender, EventArgs e)
        {
            //IWorkbook workbook = spreadsheetControl1.Document;
            //Worksheet worksheet = workbook.Worksheets[0];
            //CellRange rangeXD = worksheet.Range["Tbl_thongsovattu"];
            //string madv = tx_mahieu.Text;
            //string tenvattu = tx_Tenvt.Text;
            //string donvi = txt_donvi.Text;
            //string giagoc = txt_giagoc.Text;
            //string giakehoach = txt_giakehoach.Text;
            //DataTable dt = new DataTable();
            //dt.Clear();
            //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Tên vật tư"), new DataColumn("Đơn vị"), new DataColumn("Đơn giá gốc"), new DataColumn("Đơn giá Kế hoạch") });
            //dt.AcceptChanges();
            //DataRow dr = null;
            ////dt.Columns.Add("Tên vật tư");
            ////dt.Columns.Add("Đơn vị");
            ////dt.Columns.Add("Đơn giá gốc");
            ////dt.Columns.Add("Đơn giá Kế hoạch");
            //for (int i = rangeXD.TopRowIndex; i < rangeXD.BottomRowIndex; i++)
            //{
            //    if(worksheet.Rows[i].Visible==true && worksheet.Rows[i][madv].Value.ToString()!="")
            //        dt.Rows.Add(worksheet.Rows[i][tenvattu].Value.ToString(), worksheet.Rows[i][donvi].Value.ToString(), worksheet.Rows[i][giagoc].Value.ToString(), worksheet.Rows[i][giakehoach].Value.ToString());
            //    //dt.Rows[i - 5]["Tên vật tư"] = worksheet.Rows[i][tenvattu].Value.ToString();
            //    //dt.Rows[i - 5]["Đơn vị"] = worksheet.Rows[i][donvi].Value.ToString();
            //    //dt.Rows[i - 5]["Đơn giá gốc"] = worksheet.Rows[i][giagoc].Value.ToString();
            //    //dt.Rows[i - 5]["Đơn giá Kế hoạch"] = worksheet.Rows[i][giakehoach].Value.ToString();
            //}
            //dt.AcceptChanges();
            //m_TruyenData(dt);
            //this.Close();
        }

        private void linkduongdan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkduongdan.Text);
            //if (Directory.Exists(linkduongdan.Text))

        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_VL_Hoantat_Click(object sender, EventArgs e)
        {
            fcn_truyendataExxcel_VL(dic_VT, txt_VL_Mahieu, txt_VL_TenVL, txt_VL_Khoiluong, txt_VL_Dongiagoc,tb_DonGiaKeHoach, txt_VL_Donvi, (int)nud_VL_Begin.Value - 1, (int)nud_VL_End.Value - 1, txt_VT_STT);
        }

        private void cbb_VL_tensheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormKeHoachVatTu_Load(object sender, EventArgs e)
        {
            DialogResult res = m_openFileDialog.ShowDialog();

            if (res != DialogResult.OK)
            {
                this.Close();
                return;
            }

            linkduongdan.Text = m_openFileDialog.FileName;
            spsheet_XemFile.LoadDocument(m_openFileDialog.FileName);
            dic_VT = new Dictionary<Control, string>()
            {
                {txt_VL_Mahieu, "Mã hiệu" },
                {txt_VL_TenVL, "Tên vật tư" },
                {txt_VL_Donvi, "Đơn vị" },
                {txt_VL_Khoiluong, "Khối lượng" },
                {txt_VL_Dongiagoc, "Giá gốc" },
                {tb_DonGiaKeHoach, "Giá thông báo" }
            };
            fcn_loadhopdong();
        }
        private void fcn_loadhopdong()
        {
            IWorkbook wb = spsheet_XemFile.Document;
            //List<string> ten = new List<string>();
            string[] ten = wb.Worksheets.ToList().Select(x => x.Name).ToArray();
            cbb_VL_tensheet.DataSource = ten.Clone();
            cbb_VL_tensheet.Text = "Vật liệu";
        }
        private void fcn_loadtencot(string worksheet, Dictionary<Control, string> tendic, TextBox stt)
        {
            IWorkbook workbook = spsheet_XemFile.Document;
            Worksheet sheetThongTin = workbook.Worksheets[worksheet];
            List<string> tencongtrinh = new List<string>();
            List<string> hangmuc = new List<string>();
            //CellRange usedRange = sheetThongTin.GetUsedRange();
            //nud_end.Value= usedRange.RowCount;
            //nud_tonghop_kt.Value= usedRange.RowCount;

            #region SEARCH Công trình, Hạng mục, STT
            string searchString = "CÔNG TRÌNH:";
            string searchhangmuc = "HẠNG MỤC:";
            string searchstt = "Stt";
            dic_congtrinh.Clear();
            dic_hangmuc.Clear();
            IEnumerable<Cell> searchResult = sheetThongTin.Search(searchString);//.ToList().ForEach(x => x.Font.Color = Color.re
            foreach (Cell cell in searchResult)
            {
                tencongtrinh.Add(cell.Value.ToString().Substring(11));
                if (!dic_congtrinh.Keys.Contains(cell.RowIndex))
                    dic_congtrinh.Add(cell.RowIndex, cell.Value.ToString().Substring(11));
            }
            IEnumerable<Cell> searchResult_hangmuc = sheetThongTin.Search(searchhangmuc);//.ToList().ForEach(x => x.Font.Color = Color.re
            foreach (Cell cell in searchResult_hangmuc)
            {
                hangmuc.Add(cell.Value.ToString().Substring(9));
                if (!dic_hangmuc.Keys.Contains(cell.RowIndex))
                    dic_hangmuc.Add(cell.RowIndex, cell.Value.ToString().Substring(9));
            }
            if (dic_congtrinh.Count() == 0)
            {
                IEnumerable<Cell> searchResultNext = sheetThongTin.Search("CÔNG TRÌNH :");
                foreach (Cell cell in searchResult)
                {
                    tencongtrinh.Add(cell.Value.ToString().Substring(11));
                    if (!dic_congtrinh.Keys.Contains(cell.RowIndex))
                        dic_congtrinh.Add(cell.RowIndex, cell.Value.ToString().Substring(11));
                }

            }
            if (dic_hangmuc.Count() == 0)
            {
                IEnumerable<Cell> searchResult_hangmucnext = sheetThongTin.Search("HẠNG MỤC :");
                foreach (Cell cell in searchResult_hangmuc)
                {
                    hangmuc.Add(cell.Value.ToString().Substring(9));
                    if (!dic_hangmuc.Keys.Contains(cell.RowIndex))
                        dic_hangmuc.Add(cell.RowIndex, cell.Value.ToString().Substring(9));
                }
            }
            IEnumerable<Cell> searchResult_STT = sheetThongTin.Search(searchstt);//.ToList().ForEach(x => x.Font.Color = Color.re
            foreach (Cell cell in searchResult_STT)
            {
                if (stt.Text == "")
                    stt.Text = sheetThongTin.Columns[cell.ColumnIndex].Heading;
                else
                    break;
            }
            //cbb_congtrinh.DataSource = tencongtrinh;
            //cbb_hangmuc.DataSource = hangmuc;
            //cbb_tonghop_congtrinh.DataSource = tencongtrinh;
            //cbb_tonghop_hangmuc.DataSource = hangmuc;
            foreach (var item in tendic)
            {
                IEnumerable<Cell> searchResult_duthau = sheetThongTin.Search(item.Value.ToString());
                string cot = "";
                foreach (Cell cell in searchResult_duthau)
                {
                    if (!cell.Value.ToString().Contains(":")&& cell.Value.ToString().StartsWith(item.Value))
                    {
                        if (cot == "")
                            cot = sheetThongTin.Columns[cell.ColumnIndex].Heading;
                        else
                            break;
                    }
                }
                item.Key.Text = cot;
            }
            #endregion

            #region SEARCH cột đơn giá
            //sheetThongTin
            #endregion
        }

        private void cbb_VL_tensheet_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbb_VL_tensheet.SelectedIndex < 0)
                return;
            IWorkbook workbook = spsheet_XemFile.Document;
            workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[cbb_VL_tensheet.Text];
            CellRange usedRange = workbook.Worksheets[cbb_VL_tensheet.Text].GetUsedRange();
            nud_VL_End.Value = usedRange.RowCount;
            fcn_loadtencot(cbb_VL_tensheet.Text, dic_VT, txt_VT_STT);
        }
        private void fcn_truyendataExxcel_VL(Dictionary<Control, string> dic, TextBox mahieu, TextBox tenvattu, TextBox khoiluong, TextBox dongiagoc,TextBox dongiakehoach, TextBox donvi, int firstLine, int lastLine, TextBox stt)
        {
            string queryStr = $"SELECT * FROM {QLVT.TBL_QLVT_KHVT}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            dt.Clear();
            Worksheet ws = spsheet_XemFile.Document.Worksheets.ActiveWorksheet;
            fcn_kiemtracongtrinh_hangmuc(firstLine);
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if(Int32.TryParse(crRow[stt.Text].Value.ToString(), out int sott)&& mahieu.Text == "")
                {
                    string code = Guid.NewGuid().ToString();
                    DataRow dtRow = dt.NewRow();
                    dtRow["Code"] = code;
                    //dtRow["DonVi"] = crRow[donvi.Text].Value.ToString();
                    //dtRow["MaVatLieu"] = crRow[mahieu.Text].Value.ToString();
                    //dtRow["DonGiaGoc"] = !crRow[dongiagoc.Text].Value.IsNumeric ? 0 : crRow[dongiagoc.Text].Value.NumericValue;
                    //dtRow["DonGiaKeHoach"] = !crRow[dongiakehoach.Text].Value.IsNumeric ? 0 : crRow[dongiakehoach.Text].Value.NumericValue;
                    dtRow["VatTu"] = crRow[tenvattu.Text].Value.ToString();
                    dtRow["TenVatTu_KhongDau"] = MyFunction.fcn_RemoveAccents(crRow[tenvattu.Text].Value.ToString());
                    //dtRow["KhoiLuongDinhMuc"] = !crRow[khoiluong.Text].Value.IsNumeric ? 0 : crRow[khoiluong.Text].Value.NumericValue;
                    dtRow["CodeHangMuc"] = codehangmuc;
                    dtRow["ThoiGianTu"] = dtp_begin.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dtRow["ThoiGianDen"] = dtp_end.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    dt.Rows.Add(dtRow);
                }
                else if (!Int32.TryParse(crRow[stt.Text].Value.ToString(), out int so) && crRow[mahieu.Text].Value.ToString() == "")
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
                    if (crRow[mahieu.Text].Value.ToString() != "" && Int32.TryParse(crRow[stt.Text].Value.ToString(), out int sostt))
                    {
                        string code = Guid.NewGuid().ToString();
                        DataRow dtRow = dt.NewRow();
                        dtRow["Code"] = code;
                        dtRow["DonVi"] = crRow[donvi.Text].Value.ToString();
                        dtRow["MaVatLieu"] = crRow[mahieu.Text].Value.ToString();
                        dtRow["DonGiaGoc"] =!crRow[dongiagoc.Text].Value.IsNumeric?0 :crRow[dongiagoc.Text].Value.NumericValue;            
                        dtRow["DonGiaKeHoach"] =!crRow[dongiakehoach.Text].Value.IsNumeric?0 :crRow[dongiakehoach.Text].Value.NumericValue;            
                        dtRow["VatTu"] = crRow[tenvattu.Text].Value.ToString();
                        dtRow["TenVatTu_KhongDau"] =MyFunction.fcn_RemoveAccents(crRow[tenvattu.Text].Value.ToString());
                        dtRow["KhoiLuongDinhMuc"] =!crRow[khoiluong.Text].Value.IsNumeric?0:crRow[khoiluong.Text].Value.NumericValue;
                        dtRow["CodeHangMuc"] = codehangmuc;
                        dtRow["ThoiGianTu"] = dtp_begin.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        dtRow["ThoiGianDen"] = dtp_end.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                        foreach (var item in dic)
                        {
                            if (item.Key.Text.Trim(' ') == "")
                            {
                                MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                                dt.Rows.Clear();
                                return;
                            }
                        }
                        dt.Rows.Add(dtRow);
                    }
                }
            }
            _TruyenDataTuExcel(dt);

            this.Close();
        }
        static string codehangmuc;
        static string tenhangmuc;
        static string tencongtrinh = "";

        private void FormKeHoachVatTu_FormClosed(object sender, FormClosedEventArgs e)
        {
            spsheet_XemFile.Dispose();
            GC.Collect();
        }

        private void fcn_kiemtracongtrinh_hangmuc(int batdau)
        {
            int vitri = 0;
            //string tencongtrinh = "";
            string codecongtrinh = Guid.NewGuid().ToString();
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
            string queryStr = $"SELECT \"Code\",\"Ten\" FROM {QLVT.TBL_QLVT_KHVT_CongTrinh} WHERE \"Ten\"=@TenCongTrinh";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr, parameter: new object[] {tencongtrinh});
            if (dt.Rows.Count == 0)
            {
                string dbString = $"INSERT INTO {QLVT.TBL_QLVT_KHVT_CongTrinh} (\"Code\",\"CodeDuAn\",\"Ten\") VALUES ('{codecongtrinh}','{m_codeduan}',@Ten)";
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
            string queryStr_hangmuc = $"SELECT \"Code\",\"Ten\" FROM {QLVT.TBL_QLVT_KHVT_HangMuc} WHERE \"Ten\"=@TenHangMuc AND \"CodeCongTrinh\"='{codecongtrinh}'";
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
    }
}
