using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet.Menu;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
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
using PhanMemQuanLyThiCong.Model.HopDong;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_PhuLucHopDong : Form
    {
        string m_TenHD,m_codeHD,m_codetheodot;
        DataTable m_dtDanhMucCongTac, m_dtCongTacTheoKy;
        //Dictionary<string, string> m_NameLyKhai;
        public Form_PhuLucHopDong()
        {
            InitializeComponent();

        }
        public void Fcn_UpdatePropoties(string codeHD, string codetheodot,string TenHopDong)
        {
            m_codeHD = codeHD;
            m_codetheodot = codetheodot;
            m_TenHD = TenHopDong;
            this.Text = $"Chi tiết phụ lục hợp đồng: {m_TenHD}";
        }
        private void spread_Phuluchopdong_PopupMenuShowing(object sender, DevExpress.XtraSpreadsheet.PopupMenuShowingEventArgs e)
        {
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);
            //e.Menu.Items.Clear();
            if (worksheet.Rows[worksheet.SelectedCell.BottomRowIndex][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "CT" && worksheet.Rows[worksheet.SelectedCell.BottomRowIndex][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].Value.ToString() == "")
            {
                SpreadsheetMenuItem myItem2 = new SpreadsheetMenuItem("Thêm công tác", new EventHandler(fcn_handleThemcongtac));
                e.Menu.Items.Add(myItem2);

                SpreadsheetMenuItem myItem4 = new SpreadsheetMenuItem("Thêm vật liệu", new EventHandler(fcn_handleThemcongtac_VL));
                e.Menu.Items.Add(myItem4);
                SpreadsheetMenuItem myItem5 = new SpreadsheetMenuItem("Thêm nhân công", new EventHandler(fcn_handleThemcongtac_NC));
                e.Menu.Items.Add(myItem5);
                SpreadsheetMenuItem myItem6 = new SpreadsheetMenuItem("Thêm máy thi công", new EventHandler(fcn_handleThemcongtac_MTC));
                e.Menu.Items.Add(myItem6);
                SpreadsheetMenuItem myItem7 = new SpreadsheetMenuItem("Thêm vật liệu từ kế hoạch vật tư", new EventHandler(fcn_handleThemcongtac_VLKHVT));
                e.Menu.Items.Add(myItem7);
            }
        }
        private void fcn_handleThemcongtac_VLKHVT(object sender, EventArgs e)
        {
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];

            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];

            Dictionary<string, string> DIC_HD = MyFunction.fcn_getDicOfColumn(worksheet.Range[MyConstant.TBL_QUYETTHONGTIN]);
            int ps = -1;
            for (int i = worksheet.SelectedCell.BottomRowIndex; i > rangeXD.TopRowIndex; i--)
            {
                string kyhieu = worksheet.Rows[i][DIC_HD[MyConstant.COL_HD_Kyhieu]].Value.ToString();
                if (kyhieu == "HD")
                {
                    string tenhopdong = worksheet.Rows[i + 1][DIC_HD[MyConstant.COL_HD_Tencongviec]].Value.ToString();
                    Dictionary<string, string> DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, false, 0,m_codeHD);
                    if (DVTH == null)
                    {
                        MessageShower.ShowError("Vui lòng chọn Tên hợp đồng!", "Lỗi hợp đồng..");
                        return;
                    }
                    if (DVTH.FirstOrDefault().Key != MyConstant.TBL_THONGTINNHACUNGCAP)
                    {
                        MessageShower.ShowError("Vui lòng chọn lại Thêm công tác!", "Lỗi loại hợp đồng..");
                        return;
                    }
                    Form_LayVatLieuHopDongKHVT HD = new Form_LayVatLieuHopDongKHVT();
                    HD.m_TruyenData = new Form_LayVatLieuHopDongKHVT.DE__TRUYENDATA(fcn_truyencongtac_VLKHVT);
                    HD.ShowDialog();

                    return;
                }
                else if (kyhieu == "PL")
                    ps++;
            }
        }
        private void fcn_truyencongtac_VLKHVT(List<KeHoachVatTu> KHVT)
        {
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];

            string codepl = "";
            WaitFormHelper.ShowWaitForm("Đang thêm dữ liệu vào hệ thống", "Vui Lòng chờ!");
            for (int j = worksheet.SelectedCell.BottomRowIndex + 1; j > rangeXD.TopRowIndex; j--)
            {
                Row crRow = worksheet.Rows[j];
                if (crRow[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "PL" && crRow[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString() != "")
                {
                    codepl = crRow[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString();

                    break;
                }
                //if (crRow[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "PL" && crRow[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString() == "")
                //{
                //    string codeloaiHD = worksheet.Rows[j - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString();
                //    if (worksheet.Rows[j - 1][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() != "HD")
                //    {
                //        for (int i = j; i > rangeXD.TopRowIndex; i--)
                //        {
                //            if (worksheet.Rows[i][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "HD")
                //            {
                //                codeloaiHD = worksheet.Rows[i][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString();
                //            }
                //        }
                //    }
                //    break;
                //}
            }
            string dbString = "";
            List<string> lstPhuLuc = new List<string>();
            List<string> lstTT = new List<string>();
            List<object> lst = new List<object>();
            foreach (KeHoachVatTu item in KHVT)
            {
                string CodePL = Guid.NewGuid().ToString();
                dbString = $"INSERT INTO '{MyConstant.TBL_ThongTinHopDong}' (\"CodeHM\",\"Code\",\"MaHieu\",\"TenCongViec\",\"CodePl\",\"DonGia\",\"KhoiLuong\",\"DonVi\")" +
          $" VALUES ('{item.CodeHangMuc}','{CodePL}',@MaVatLieu,@VatTu,'{codepl}','{item.DonGia}','{item.KhoiLuong}',@DonVi)";
                lstPhuLuc.Add(dbString);
                lst.Add(item.MaVatLieu);
                lst.Add(item.VatTu);
                lst.Add(item.DonVi);

                dbString = $"INSERT INTO '{MyConstant.TBL_hopdongNCC_TT}' (\"CodePhuLuc\",\"Code\",\"Code_Goc\",\"CodeDot\") VALUES ('{CodePL}','{Guid.NewGuid()}','{Guid.NewGuid()}','{m_codetheodot}')";
                lstTT.Add(dbString);
            }
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstPhuLuc, parameter: lst.ToArray());
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstTT);
            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            Fcn_UpdatePhuLucHDNCC();

            WaitFormHelper.CloseWaitForm();
        }
        private void fcn_truyencongtac_VL(List<LayCongTacHopDong> HD, bool KH,bool IsDonGiaKeHoach)
        {
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            string queryStr = $"SELECT *  FROM {MyConstant.TBL_ThongTinHopDong} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_congtac = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string codepl = "";
            WaitFormHelper.ShowWaitForm("Đang thêm dữ liệu vào hệ thống", "Vui Lòng chờ!");
            for (int j = worksheet.SelectedCell.BottomRowIndex + 1; j > rangeXD.TopRowIndex; j--)
            {
                Row crRow = worksheet.Rows[j];
                if (crRow[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "PL" && crRow[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString() != "")
                {
                    codepl = crRow[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString();

                    break;
                }
                if (crRow[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "PL" && crRow[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString() == "")
                {
                    string codeloaiHD = worksheet.Rows[j - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString();
                    if (worksheet.Rows[j - 1][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() != "HD")
                    {
                        for (int i = j; i > rangeXD.TopRowIndex; i--)
                        {
                            if (worksheet.Rows[i][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "HD")
                            {
                                codeloaiHD = worksheet.Rows[i][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString();
                            }
                        }
                    }
                    break;
                }
            }
            string dbString = "";
            List<string> lstPhuLuc = new List<string>();
            List<string> lstChiTiet = new List<string>();
            List<string> lstTT = new List<string>();
            List<LayCongTacHopDong> TenVatLieu = HD.FindAll(x => x.CodeCT == null);
            List<LayCongTacHopDong> TenCT = HD.FindAll(x => x.CodeCT != null);
            foreach (LayCongTacHopDong item in TenVatLieu)
            {
                string CodePL = Guid.NewGuid().ToString();
                double DonGia = IsDonGiaKeHoach ? item.DonGiaKeHoach : item.DonGiaThiCong;
                List<LayCongTacHopDong> ChiTiet = TenCT.FindAll(x => x.ParentID == item.ID);
                if (KH)
                    dbString = $"INSERT INTO '{MyConstant.TBL_ThongTinHopDong}' (\"IsDonGiaKeHoach\",\"CodeKHVT\",\"NgayBatDau\",\"NgayKetThuc\",\"Code\",\"CodePl\",\"DonGia\",\"KhoiLuong\")" +
                        $" VALUES ('{IsDonGiaKeHoach}','{item.ID}','{item.NgayBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{item.NgayKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{CodePL}','{codepl}','{DonGia}','{item.KhoiLuongKeHoach}')";
                else
                    dbString = $"INSERT INTO '{MyConstant.TBL_ThongTinHopDong}' (\"IsDonGiaKeHoach\",\"CodeKHVT\",\"Code\",\"CodePl\",\"DonGia\",\"KhoiLuong\")" +
                        $" VALUES ('{IsDonGiaKeHoach}','{item.ID}','{CodePL}','{codepl}','{DonGia}','{item.KhoiLuongHopDong}')";
                lstPhuLuc.Add(dbString);
                foreach (LayCongTacHopDong row in ChiTiet)
                {
                    DonGia = IsDonGiaKeHoach ? row.DonGiaKeHoach : row.DonGiaThiCong;
                    if (!KH)
                        dbString = $"INSERT INTO '{MyConstant.TBL_ThongTinHopDongChiTietHD}' (\"DonGia\",\"KhoiLuong\",\"Code\",\"CodeCha\",\"CodeCongTacGiaiDoan\")" +
             $" VALUES ('{DonGia}','{row.KhoiLuongHopDong}','{Guid.NewGuid()}','{CodePL}','{row.CodeCT}')";
                    else
                        dbString = $"INSERT INTO '{MyConstant.TBL_ThongTinHopDongChiTietHD}' (\"DonGia\",\"KhoiLuong\",\"Code\",\"CodeCha\",\"CodeCongTacGiaiDoan\")" +
$" VALUES ('{DonGia}','{row.KhoiLuongKeHoach}','{Guid.NewGuid()}','{CodePL}','{row.CodeCT}')";
                    lstChiTiet.Add(dbString);
                }
                dbString = $"INSERT INTO '{MyConstant.TBL_hopdongNCC_TT}' (\"CodePhuLuc\",\"Code\",\"Code_Goc\",\"CodeDot\") VALUES ('{CodePL}','{Guid.NewGuid()}','{Guid.NewGuid()}','{m_codetheodot}')";
                lstTT.Add(dbString);
            }
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstPhuLuc);
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstChiTiet);
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstTT);
            WaitFormHelper.CloseWaitForm();
            Fcn_UpdatePhuLucHDNCC();
        }
        private void Fcn_UpdatePhuLucHDNCC()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Worksheet worksheet_congtrinh = workbook.Worksheets["Thông tin công trình_Hạng mục"];
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);

            CellRange copy_pl = worksheet_congtrinh.Rows[3];
            CellRange copy_hd = worksheet_congtrinh.Rows[2];
            CellRange copy_ct = worksheet_congtrinh.Rows[4];
            string dbString = $"SELECT * FROM view_PhuLuHopDongVatTu" +
                $" WHERE CodeHDChinh='{m_codeHD}' ORDER BY SortIdCtrinh ASC, SortIdHM ASC,SortIdCtac ASC ";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dtCongTacTheoKy.AddIndPhanTuyenNhom();
            if (rangeXD.RowCount != 6)
                worksheet.Rows.Remove(6, rangeXD.RowCount - 6);
            worksheet.Rows[6].CopyFrom(copy_ct, PasteSpecial.All);
            worksheet.Rows[5][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText("");
            string lsCodeHD = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["CodeHd"].ToString()).ToArray());
            dbString = $"SELECT * FROM {MyConstant.TBL_ThongtinphulucHD} WHERE \"CodeHd\" IN ({lsCodeHD})";
            DataTable dt_PL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt_PL.Rows.Count == 0)
            {
                dbString = $"SELECT *  FROM {MyConstant.TBL_Tonghopdanhsachhopdong} INNER JOIN {MyConstant.Tbl_TAOMOIHOPDONG} ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code = {MyConstant.TBL_Tonghopdanhsachhopdong}.CodeHopDong " +
                    $"WHERE \"CodeHopDong\"='{m_codeHD}'";
                DataTable dtHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                lsCodeHD = MyFunction.fcn_Array2listQueryCondition(dtHD.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
                dbString = $"SELECT * FROM {MyConstant.TBL_ThongtinphulucHD} WHERE \"CodeHd\" IN ({lsCodeHD})";
                dt_PL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            }
            dbString = $"SELECT * FROM {MyConstant.TBL_LoaiHD}";
            DataTable dt_LoaiHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT {MyConstant.Tbl_TAOMOIHOPDONG}.*,{MyConstant.TBL_Tonghopdanhsachhopdong}.Code as CodeHd  FROM  {MyConstant.Tbl_TAOMOIHOPDONG} INNER JOIN {MyConstant.TBL_Tonghopdanhsachhopdong} " +
                $"ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code = {MyConstant.TBL_Tonghopdanhsachhopdong}.CodeHopDong " +
                $" WHERE {MyConstant.Tbl_TAOMOIHOPDONG}.Code='{m_codeHD}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lsCodeChitiet = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["CodePLHD"].ToString()).ToArray());
            dbString = $"SELECT COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
         $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi," +
        $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
        $"ctvl.*,cttk.* FROM {MyConstant.TBL_ThongTinHopDongChiTietHD} ctvl " +
   $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
   $"ON cttk.Code=ctvl.CodeCongTacGiaiDoan " +
   $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
   $"ON cttk.CodeCongTac=dmct.Code " +
   $" WHERE ctvl.CodeCha IN ({lsCodeChitiet})";
            DataTable dt_ChiTiet = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt_PL.Rows.Count != 0 && dtCongTacTheoKy.Rows.Count != 0)
                worksheet.Rows.Insert(worksheet.Range[MyConstant.TBL_QUYETTHONGTIN].BottomRowIndex, (dtCongTacTheoKy.Rows.Count * 2 + dt_ChiTiet.Rows.Count) * dt_PL.Rows.Count, RowFormatMode.FormatAsPrevious);
            int crRowInd = 6;
            bool check = true;
            Dictionary<string, string> DVTH = null;
            int stt = 1;
            bool IsDonGiaKeHoach = true;
            int sttcon = 1;
            spread_Phuluchopdong.BeginUpdate();
            foreach (DataRow item in dt.Rows)
            {
                worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, true, crRowInd - 1, m_codeHD);

                if (dtCongTacTheoKy.Rows.Count == 0)
                {
                    DataRow[] drs_PL = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["CodeHd"].ToString()).ToArray();
                    if (drs_PL.Count() == 0)
                        break;
                    var tenpl = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["CodeHd"].ToString()).Select(x => x["TenPl"].ToString()).ToList();
                    var codepl = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["CodeHd"].ToString()).Select(x => x["Code"].ToString()).ToList();
                    var codeloaiHD = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["CodeHd"].ToString()).Select(x => x["CodeLoaiHd"].ToString()).ToList();
                    var tenloaiHD = dt_LoaiHD.AsEnumerable().Where(x => x["Code"].ToString() == codeloaiHD[0].ToString()).Select(x => x["LoaiHopDong"].ToString()).ToList();
                    if (check)
                    {
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                        //DVTH = MyFunction.fcn_getdonvithuchien(spread_PhuLucHopDong_Full, $"{item["TenHopDong"].ToString()}({item["SoHopDong"]})", true, crRowInd - 1, slke_ThongTinDuAn.EditValue.ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenpl[0]);
                        worksheet.Rows[crRowInd - 2][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codepl[0]);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeHD_HD]].SetValueFromText(item["Code"].ToString());
                        check = false;

                    }
                    else
                    {
                        Row crRowWs = worksheet.Rows[crRowInd++];
                        crRowWs.CopyFrom(copy_hd, PasteSpecial.All);
                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                        worksheet.Rows[crRowInd++].CopyFrom(copy_pl, PasteSpecial.All);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                        //DVTH = MyFunction.fcn_getdonvithuchien(spread_PhuLucHopDong_Full, $"{item["TenHopDong"].ToString()}({item["SoHopDong"]})", true, crRowInd - 1, slke_ThongTinDuAn.EditValue.ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenpl[0]);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codepl[0]);

                    }
                }
                var CrowTenPL = dtCongTacTheoKy.AsEnumerable().GroupBy(x => x["CodePl"].ToString());
                foreach (var rowPL in CrowTenPL)
                {
                    var rowPLFirst = rowPL.FirstOrDefault();
                    if (double.Parse(rowPLFirst[MyConstant.COL_HD_Phatsinh].ToString()) != 0)
                    {
                        Row crRowWs = worksheet.Rows[crRowInd++];
                        crRowWs.CopyFrom(copy_pl, PasteSpecial.All);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(rowPLFirst["TenPl"].ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(rowPLFirst["CodePl"].ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Phatsinh]].SetValueFromText(rowPLFirst["PhatSinh"].ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Phuluc]].SetValueFromText($"{"Phụ lục "}{(int.Parse(rowPLFirst["PhatSinh"].ToString()) + 1)}");
                    }
                    else
                    {
                        var tenloaiHD = dt_LoaiHD.AsEnumerable().Where(x => x["Code"].ToString() == rowPLFirst["CodeLoaiHd"].ToString()).Select(x => x["LoaiHopDong"].ToString()).ToList();
                        if (check)
                        {
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(rowPLFirst["TenPl"].ToString());
                            worksheet.Rows[crRowInd - 2][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(rowPLFirst["CodePl"].ToString());
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeHD_HD]].SetValueFromText(item["Code"].ToString());
                            check = false;

                        }
                        else
                        {
                            Row crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs.CopyFrom(copy_hd, PasteSpecial.All);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                            worksheet.Rows[crRowInd++].CopyFrom(copy_pl, PasteSpecial.All);
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(rowPLFirst["TenPl"].ToString());
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(rowPLFirst["CodePl"].ToString());

                        }
                        var grCongTrinh = rowPL.GroupBy(x => x["CodeCongTrinh"].ToString());
                        foreach (var Ctrinh in grCongTrinh)
                        {
                            string crCodeCT = Ctrinh.Key;
                            Row crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs.Font.Bold = true;
                            crRowWs.Font.Color = Color.DarkTurquoise;
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_CT]].SetValue(crCodeCT);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_CONGTRINH);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(Ctrinh.FirstOrDefault()["TenCongTrinh"].ToString());
                            var grHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"].ToString());
                            foreach (var HM in grHangMuc)
                            {
                                string crCodeHM = HM.Key;
                                crRowWs = worksheet.Rows[crRowInd++];
                                crRowWs.Font.Bold = true;
                                crRowWs.Font.Color = Color.DarkGreen;
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_HM]].SetValue(crCodeHM);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_HANGMUC);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(HM.FirstOrDefault()["TenHangMuc"].ToString().ToUpper());
                                var grPhanTuyen = HM.GroupBy(x => (int)x["IndPT"]).OrderBy(x => x.Key);
                                foreach (var Tuyen in grPhanTuyen)
                                {
                                    var fstTuyen = Tuyen.First();
                                    string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : fstTuyen["CodePhanTuyen"].ToString();
                                    if (fstTuyen["CodePhanTuyen"] != DBNull.Value)
                                    {
                                        crRowWs = worksheet.Rows[crRowInd++];
                                        crRowWs.Font.Bold = true;
                                        crRowWs.Font.Color = Color.Red;
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValue(crCodeTuyen);
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_PhanTuyen);
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(fstTuyen["TenTuyen"].ToString().ToUpper());
                                    }
                                    var grTuyenNhom = Tuyen.GroupBy(x => (int)x["IndNhom"])
                                          .OrderBy(x => x.Key);
                                    foreach (var NhomTuyen in grTuyenNhom)
                                    {
                                        var fstNhom = NhomTuyen.First();
                                        var grCongTacTuyen = NhomTuyen.GroupBy(x => x["CodePLHD"]);
                                        foreach (var CongTac in grCongTacTuyen)
                                        {

                                            var fstCongTac = CongTac.FirstOrDefault();
                                            crRowWs = worksheet.Rows[crRowInd++];
                                            string Mahieu = fstCongTac["MaHieuCongTac"].ToString();
                                            string TenCongTac = fstCongTac["TenCongTac"].ToString();
                                            WaitFormHelper.ShowWaitForm($"{stt}.{Mahieu}_{TenCongTac}");
                                            crRowWs.Font.Bold = false;
                                            crRowWs.Font.Color = Color.Black;
                                            crRowWs.Visible = true;
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValue(fstCongTac["CodePLHD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_Congtac]].SetValue(fstCongTac["Code"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValueFromText(Mahieu);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(TenCongTac);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_DonVi]].SetValue(fstCongTac["DonVi"]);

                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValue(stt++);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]].SetValue(fstCongTac["KLPLHD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Dongia]].SetValue(fstCongTac["DonGiaPLHD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NBD]].SetValue(fstCongTac["NBD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NKT]].SetValue(fstCongTac["NKT"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]].Formula = $"{DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]}{crRowInd}*{DIC_HOPDONG[MyConstant.COL_HD_Dongia]}{crRowInd}";
                                            DataRow[] crow_Chitiet = dt_ChiTiet.AsEnumerable().Where(x => x["CodeCha"].ToString() == fstCongTac["CodePLHD"].ToString()).ToArray();
                                            foreach (var rowCT in crow_Chitiet)
                                            {
                                                crRowWs = worksheet.Rows[crRowInd++];
                                                //crRowWs.Font.Bold = true;
                                                crRowWs.Font.Color = Color.Red;
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(rowCT["MaHieuCongTac"]);
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(rowCT["TenCongTac"]);
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_DonVi]].SetValue(rowCT["DonVi"]);
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]].SetValue(rowCT[MyConstant.COL_HD_Khoiluong]);
                                                crRowWs.Visible = false;
                                            }
                                        }
                                    }
                                    if (crCodeTuyen != null)
                                    {
                                        crRowWs = worksheet.Rows[crRowInd++];
                                        crRowWs.Font.Bold = true;
                                        crRowWs.Font.Color = Color.Red;
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_HoanThanhPhanTuyen);
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue($"HOÀN THÀNH {fstTuyen["TenTuyen"]}".ToUpper());
                                    }

                                }

                                crRowWs = worksheet.Rows[crRowInd++];
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue("CT");
                                crRowWs = worksheet.Rows[crRowInd++];
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue("");

                            }

                        }

                    }

                }

            }
            spread_Phuluchopdong.EndUpdate();
            WaitFormHelper.CloseWaitForm();

        }
        private void fcn_updatephuluchopdongNCC()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            string dbString = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"CodeDuAn\" = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count == 0)
                return;
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Worksheet worksheet_congtrinh = workbook.Worksheets["Thông tin công trình_Hạng mục"];
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            string queryStr = $"SELECT *  FROM {MyConstant.TBL_Tonghopdanhsachhopdong} INNER JOIN {MyConstant.Tbl_TAOMOIHOPDONG} " +
                $"ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code = {MyConstant.TBL_Tonghopdanhsachhopdong}.CodeHopDong WHERE \"CodeHopDong\"='{m_codeHD}'";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange copy_pl = worksheet_congtrinh.Rows[3];
            CellRange copy_hd = worksheet_congtrinh.Rows[2];
            CellRange copy_ct = worksheet_congtrinh.Rows[4];
            DataTable dtCongTacTheoKy, dtDanhMucCongTac, dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC,false);
            queryStr = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"Code\"='{m_codeHD}'";
            DataTable dt_TaoMoiHD = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lsCodeHD = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());

            dbString = $"SELECT * FROM {MyConstant.TBL_ThongtinphulucHD} WHERE \"CodeHd\" IN ({lsCodeHD})";
            DataTable dt_PL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lsCodePL = MyFunction.fcn_Array2listQueryCondition(dt_PL.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());

            dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_PhuLuc} " +
                $"INNER JOIN {TDKH.TBL_KHVT_VatTu} " +
$"ON {TDKH.TBL_KHVT_VatTu}.Code={MyConstant.TBL_HopDong_PhuLuc}.CodeKHVT " +
                $"WHERE {MyConstant.TBL_HopDong_PhuLuc}.CodePl IN ({lsCodePL})";
            DataTable dt_CongtacHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string lsCodeChitiet = MyFunction.fcn_Array2listQueryCondition(dt_CongtacHD.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            dbString = $"SELECT * FROM {MyConstant.TBL_ThongTinHopDongChiTietHD} " +
                       $"INNER JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                       $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code={MyConstant.TBL_ThongTinHopDongChiTietHD}.CodeCongTacGiaiDoan " +
                       $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
                       $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac={TDKH.TBL_DanhMucCongTac}.Code " +
                $" WHERE {MyConstant.TBL_ThongTinHopDongChiTietHD}.CodeCha IN ({lsCodeChitiet})";
            DataTable dt_ChiTiet = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {MyConstant.TBL_LoaiHD}";
            DataTable dt_LoaiHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (rangeXD.RowCount != 6)
                worksheet.Rows.Remove(6, rangeXD.RowCount - 6);
            worksheet.Rows[6].CopyFrom(copy_ct, PasteSpecial.All);
            worksheet.Rows[5][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText("");
            if (dt_PL.Rows.Count != 0)
                worksheet.Rows.Insert(worksheet.Range[MyConstant.TBL_QUYETTHONGTIN].BottomRowIndex - 1, (dtCT.Rows.Count + dtHM.Rows.Count + 10) * dt_PL.Rows.Count + dt_PL.Rows.Count * 2 + dt_ChiTiet.Rows.Count + dt_CongtacHD.Rows.Count, RowFormatMode.FormatAsPrevious);
            int crRowInd = 6;
            bool check = true;
            Dictionary<string, string> DVTH = null;
////          spread_Phuluchopdong.Document.History.IsEnabled = false;
            spread_Phuluchopdong.BeginUpdate();
            foreach (DataRow item in dt.Rows)
            {
                worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, true, crRowInd - 1, m_codeHD);
                DataRow[] drs_PL = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["Code"].ToString()).ToArray();
                if (drs_PL.Count() == 0)
                    break;
                var tenpl = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["Code"].ToString()).Select(x => x["TenPl"].ToString()).ToList();
                var codepl = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["Code"].ToString()).Select(x => x["Code"].ToString()).ToList();
                var codeloaiHD = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["Code"].ToString()).Select(x => x["CodeLoaiHd"].ToString()).ToList();
                var tenloaiHD = dt_LoaiHD.AsEnumerable().Where(x => x["Code"].ToString() == codeloaiHD[0].ToString()).Select(x => x["LoaiHopDong"].ToString()).ToList();
                if (check)
                {
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                    DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, true, crRowInd - 1, m_codeHD);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenpl[0]);
                    worksheet.Rows[crRowInd - 2][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codepl[0]);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeHD_HD]].SetValueFromText(item["Code"].ToString());
                    check = false;
                }
                else
                {
                    Row crRowWs = worksheet.Rows[crRowInd++];
                    crRowWs.CopyFrom(copy_hd, PasteSpecial.All);
                    crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                    worksheet.Rows[crRowInd++].CopyFrom(copy_pl, PasteSpecial.All);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                    DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, true, crRowInd - 1, m_codeHD);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenpl[0]);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codepl[0]);
                }
                foreach (var rowpl in drs_PL)
                {
                    foreach (DataRow rowct in dtCT.Rows)
                    {
                        Row crRowWs = worksheet.Rows[crRowInd++];
                        string crCodeCT = rowct["Code"].ToString();
                        crRowWs.Font.Bold = true;
                        crRowWs.Font.Color = Color.DarkTurquoise;
                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_CT]].SetValue(crCodeCT);
                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_CONGTRINH);
                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(rowct["Ten"].ToString());
                        foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                        {
                            string crCodeHM = HM["Code"].ToString();
                            crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs.Font.Bold = true;
                            crRowWs.Font.Color = Color.DarkGreen;
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_HM]].SetValue(crCodeHM);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_HANGMUC);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(HM["Ten"].ToString().ToUpper());
                            DataRow[] dt_CTCon = dt_CongtacHD.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                            foreach (var rowctc in dt_CTCon)
                            {
                                crRowWs = worksheet.Rows[crRowInd++];
                                crRowWs.Font.Bold = true;
                                crRowWs.Font.Color = Color.Black;
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValue(rowctc["Code"]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_Congtac]].SetValue(rowctc["CodeKHVT"]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(rowctc["MaVatLieu"]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(rowctc["VatTu"]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_DonVi]].SetValue(rowctc["DonVi"]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]].SetValue(rowctc[MyConstant.COL_HD_Khoiluong]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Dongia]].SetValue(rowctc[MyConstant.COL_HD_Dongia]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NBD]].SetValue(rowctc[MyConstant.COL_HD_NBD]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NKT]].SetValue(rowctc[MyConstant.COL_HD_NKT]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]].Formula = $"{DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]}{crRowInd}*{DIC_HOPDONG[MyConstant.COL_HD_Dongia]}{crRowInd}";
                                DataRow[] crow_Chitiet = dt_ChiTiet.AsEnumerable().Where(x => x["CodeCha"].ToString() == rowctc["Code"].ToString()).ToArray();
                                foreach (var rowCT in crow_Chitiet)
                                {
                                    crRowWs = worksheet.Rows[crRowInd++];
                                    crRowWs.Font.Bold = true;
                                    crRowWs.Font.Color = Color.Red;
                                    crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(rowCT["MaHieuCongTac"]);
                                    crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(rowCT["TenCongTac"]);
                                    crRowWs[DIC_HOPDONG[MyConstant.COL_HD_DonVi]].SetValue(rowCT["DonVi"]);
                                    crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]].SetValue(rowCT[MyConstant.COL_HD_Khoiluong]);
                                    crRowWs.Visible = false;
                                }
                            }
                            crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue("CT");
                            crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue("");
                        }
                    }

                }
            }

            spread_Phuluchopdong.EndUpdate();
            try
            {
////          spread_Phuluchopdong.Document.History.IsEnabled = true;

            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
        }
        private void fcn_handleThemcongtac_VL(object sender, EventArgs e)
        {
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            Dictionary<string, string> DIC_HD = MyFunction.fcn_getDicOfColumn(worksheet.Range[MyConstant.TBL_QUYETTHONGTIN]);
            int ps = -1;
            for (int i = worksheet.SelectedCell.BottomRowIndex; i > rangeXD.TopRowIndex; i--)
            {
                string kyhieu = worksheet.Rows[i][DIC_HD[MyConstant.COL_HD_Kyhieu]].Value.ToString();
                if (kyhieu == "HD")
                {
                    string tenhopdong = worksheet.Rows[i + 1][DIC_HD[MyConstant.COL_HD_Tencongviec]].Value.ToString();
                    Dictionary<string, string> DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, false, 0, m_codeHD);
                    if (DVTH == null)
                    {
                        MessageShower.ShowError("Vui lòng chọn Tên hợp đồng!", "Lỗi hợp đồng..");
                        return;
                    }
                    if (DVTH.FirstOrDefault().Key != MyConstant.TBL_THONGTINNHACUNGCAP)
                    {
                        MessageShower.ShowError("Vui lòng chọn lại Thêm công tác!", "Lỗi loại hợp đồng..");
                        return;
                    }
                    string dbString = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG}";
                    DataTable dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    dbString = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG_MECON} WHERE \"CodeCon\"='{m_codeHD}'";
                    DataTable dt_me = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (dt_me.Rows.Count == 0)
                    {
                        MessageShower.ShowError("Bạn chưa tạo hợp đồng chính nên chưa có bảng số liệu để thực hiện cho hợp đồng này. Vui lòng quay lại nhà thầu chính (bên giao thầu) để tạo ít nhất 1 hợp đồng trước khi tạo bảng phụ lục này!", "Lỗi loại hợp đồng..");
                        return;
                    }
                    string codenhathau = dt_HD.AsEnumerable().Where(x => x["Code"].ToString() == dt_me.Rows[0]["CodeMe"].ToString())
                        .Select(x => x["CodeNhaThau"].ToString()).ToList().FirstOrDefault().ToString();
                    Dictionary<string, string> DV = new Dictionary<string, string>();
                    DV.Add(MyConstant.TBL_THONGTINNHATHAU, codenhathau);
                    DV.Add(MyConstant.TBL_THONGTINNHACUNGCAP, m_codeHD);
                    //.Add(MyConstant.TBL_THONGTINNHATHAU, codenhathau);
                    FormLayVatLieuHopDong HD = new FormLayVatLieuHopDong();
                    HD.Fcn_LoadData(ps, DV);
                    HD.LoaiVatLieu = "Vật liệu";
                    HD.m_TruyenData = new FormLayVatLieuHopDong.DE__TRUYENDATA(fcn_truyencongtac_VL);
                    HD.ShowDialog();
                    return;
                }
                else if (kyhieu == "PL")
                    ps++;
            }
        }
        private void fcn_handleThemcongtac_NC(object sender, EventArgs e)
        {
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            Dictionary<string, string> DIC_HD = MyFunction.fcn_getDicOfColumn(worksheet.Range[MyConstant.TBL_QUYETTHONGTIN]);
            int ps = -1;
            for (int i = worksheet.SelectedCell.BottomRowIndex; i > rangeXD.TopRowIndex; i--)
            {
                string kyhieu = worksheet.Rows[i][DIC_HD[MyConstant.COL_HD_Kyhieu]].Value.ToString();
                if (kyhieu == "HD")
                {
                    string tenhopdong = worksheet.Rows[i + 1][DIC_HD[MyConstant.COL_HD_Tencongviec]].Value.ToString();
                    Dictionary<string, string> DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, false, 0, m_codeHD);
                    if (DVTH == null)
                    {
                        MessageShower.ShowError("Vui lòng chọn Tên hợp đồng!", "Lỗi hợp đồng..");
                        return;
                    }
                    if (DVTH.FirstOrDefault().Key != MyConstant.TBL_THONGTINNHACUNGCAP)
                    {
                        MessageShower.ShowError("Vui lòng chọn lại Thêm công tác!", "Lỗi loại hợp đồng..");
                        return;
                    }
                    string dbString = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG}";
                    DataTable dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    dbString = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG_MECON} WHERE \"CodeCon\"='{m_codeHD}'";
                    DataTable dt_me = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (dt_me.Rows.Count == 0)
                    {
                        MessageShower.ShowError("Bạn chưa tạo hợp đồng chính nên chưa có bảng số liệu để thực hiện cho hợp đồng này. Vui lòng quay lại nhà thầu chính (bên giao thầu) để tạo ít nhất 1 hợp đồng trước khi tạo bảng phụ lục này!", "Lỗi loại hợp đồng..");
                        return;
                    }
                    string codenhathau = dt_HD.AsEnumerable().Where(x => x["Code"].ToString() == dt_me.Rows[0]["CodeMe"].ToString())
                        .Select(x => x["CodeNhaThau"].ToString()).ToList().FirstOrDefault().ToString();
                    Dictionary<string, string> DV = new Dictionary<string, string>();
                    DV.Add(MyConstant.TBL_THONGTINNHATHAU, codenhathau);
                    DV.Add(MyConstant.TBL_THONGTINNHACUNGCAP, m_codeHD);
                    //.Add(MyConstant.TBL_THONGTINNHATHAU, codenhathau);
                    FormLayVatLieuHopDong HD = new FormLayVatLieuHopDong();
                    HD.Fcn_LoadData(ps, DV);
                    HD.LoaiVatLieu = "Nhân công";
                    HD.m_TruyenData = new FormLayVatLieuHopDong.DE__TRUYENDATA(fcn_truyencongtac_VL);
                    HD.ShowDialog();
                    return;
                }
                else if (kyhieu == "PL")
                    ps++;
            }
        }
        private void fcn_handleThemcongtac_MTC(object sender, EventArgs e)
        {
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            Dictionary<string, string> DIC_HD = MyFunction.fcn_getDicOfColumn(worksheet.Range[MyConstant.TBL_QUYETTHONGTIN]);
            int ps = -1;
            for (int i = worksheet.SelectedCell.BottomRowIndex; i > rangeXD.TopRowIndex; i--)
            {
                string kyhieu = worksheet.Rows[i][DIC_HD[MyConstant.COL_HD_Kyhieu]].Value.ToString();
                if (kyhieu == "HD")
                {
                    string tenhopdong = worksheet.Rows[i + 1][DIC_HD[MyConstant.COL_HD_Tencongviec]].Value.ToString();
                    Dictionary<string, string> DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, false, 0, m_codeHD);
                    if (DVTH == null)
                    {
                        MessageShower.ShowError("Vui lòng chọn Tên hợp đồng!", "Lỗi hợp đồng..");
                        return;
                    }
                    if (DVTH.FirstOrDefault().Key != MyConstant.TBL_THONGTINNHACUNGCAP)
                    {
                        MessageShower.ShowError("Vui lòng chọn lại Thêm công tác!", "Lỗi loại hợp đồng..");
                        return;
                    }
                    string dbString = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG}";
                    DataTable dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    dbString = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG_MECON} WHERE \"CodeCon\"='{m_codeHD}'";
                    DataTable dt_me = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (dt_me.Rows.Count == 0)
                    {
                        MessageShower.ShowError("Bạn chưa tạo hợp đồng chính nên chưa có bảng số liệu để thực hiện cho hợp đồng này. Vui lòng quay lại nhà thầu chính (bên giao thầu) để tạo ít nhất 1 hợp đồng trước khi tạo bảng phụ lục này!", "Lỗi loại hợp đồng..");
                        return;
                    }
                    string codenhathau = dt_HD.AsEnumerable().Where(x => x["Code"].ToString() == dt_me.Rows[0]["CodeMe"].ToString())
                        .Select(x => x["CodeNhaThau"].ToString()).ToList().FirstOrDefault().ToString();
                    Dictionary<string, string> DV = new Dictionary<string, string>();
                    DV.Add(MyConstant.TBL_THONGTINNHATHAU, codenhathau);
                    DV.Add(MyConstant.TBL_THONGTINNHACUNGCAP, m_codeHD);
                    //.Add(MyConstant.TBL_THONGTINNHATHAU, codenhathau);
                    FormLayVatLieuHopDong HD = new FormLayVatLieuHopDong();
                    HD.Fcn_LoadData(ps, DV);
                    HD.LoaiVatLieu = "Máy thi công";
                    HD.m_TruyenData = new FormLayVatLieuHopDong.DE__TRUYENDATA(fcn_truyencongtac_VL);
                    HD.ShowDialog();
                    return;
                }
                else if (kyhieu == "PL")
                    ps++;
            }
        }
        private void fcn_handleThemcongtac(object sender, EventArgs e)
        {
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            Dictionary<string, string> DIC_HD = MyFunction.fcn_getDicOfColumn(worksheet.Range[MyConstant.TBL_QUYETTHONGTIN]);
            int ps = -1;
            for (int i = worksheet.SelectedCell.BottomRowIndex; i > rangeXD.TopRowIndex; i--)
            {
                string kyhieu = worksheet.Rows[i][DIC_HD[MyConstant.COL_HD_Kyhieu]].Value.ToString();
                if (kyhieu == "HD")
                {
                    string tenhopdong = worksheet.Rows[i + 1][DIC_HD[MyConstant.COL_HD_Tencongviec]].Value.ToString();
                    Dictionary<string, string> DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, false, 0, m_codeHD);
                    if (DVTH == null)
                    {
                        MessageShower.ShowError("Vui lòng chọn Tên hợp đồng!", "Lỗi hợp đồng..");
                        return;
                    }
                    if (DVTH.Keys.Contains(MyConstant.TBL_THONGTINNHACUNGCAP))
                    {
                        MessageShower.ShowError("Vui lòng chọn lại Thêm công tác vật tư.", "Lỗi loại hợp đồng..");
                        return;
                    }
                    if (DVTH.FirstOrDefault().Key != MyConstant.TBL_THONGTINNHATHAU)
                    {
                        string queryStr = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi_Hopdongmecon} WHERE \"CodeCon\"='{m_codeHD}'";
                        DataTable dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                        if (dt_HD.Rows.Count == 0)
                        {
                            MessageShower.ShowError("Vui lòng Tạo Hợp đồng cha cho Hợp đồng hiện tại!!!!!!");
                            return;
                        }
                    }
                    Form_LayCongTacHopDong HD = new Form_LayCongTacHopDong();
                    HD.Fcn_LoadData(ps, DVTH);
                    HD.m_TruyenData = new Form_LayCongTacHopDong.DE__TRUYENDATA(fcn_truyencongtac);
                    HD.ShowDialog();
                    break;
                }
                else if (kyhieu == "PL")
                    ps++;
            }
        }
        private void fcn_truyencongtac(List<LayCongTacHopDong> lst, int ps, bool KH, bool IsDonGiaKeHoach)
        {
            WaitFormHelper.ShowWaitForm("Đang lưu dữ liệu đã chọn vào hợp đồng", "Vui Lòng chờ!");
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];

            string codepl = "", dbString = "";
            for (int j = worksheet.SelectedCell.BottomRowIndex + 1; j > rangeXD.TopRowIndex; j--)
            {
                Row crRow = worksheet.Rows[j];
                if (crRow[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "PL" && crRow[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString() != "")
                {
                    codepl = crRow[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString();

                    break;
                }
            }
            List<string> lstPhuLuc = new List<string>();
            List<string> lstDoBoc = new List<string>();
            List<string> lstHT = new List<string>();
            List<string> lstPS = new List<string>();
            List<string> lstTT = new List<string>();
            List<string> lstToan = new List<string>();
            foreach (var row in lst)
            {
                string CodePL = Guid.NewGuid().ToString();
                string codeDB = Guid.NewGuid().ToString();
                string code_Goc = Guid.NewGuid().ToString();
                string Col = row.MaHieu == "*" ? "CodeNhom" : "CodeCongTacTheoGiaiDoan";
                double DonGia = IsDonGiaKeHoach ? row.DonGiaKeHoach : row.DonGiaThiCong;
                double KL = KH ? row.KhoiLuongKeHoach : row.KhoiLuongHopDong;
                if (KH)
                    dbString = $"INSERT INTO '{MyConstant.TBL_ThongTinHopDong}' (\"SortId\",\"IsDonGiaKeHoach\",\"NgayBatDau\",\"NgayKetThuc\",\"Code\",\"CodePl\"," +
                        $"\"DonGia\",\"KhoiLuong\",{Col})" +
                        $" VALUES ('{row.STT}','{IsDonGiaKeHoach}','{row.NgayBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{row.NgayKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'" +
                        $",'{CodePL}','{codepl}','{DonGia}','{KL}','{row.ID}')";
                else
                    dbString = $"INSERT INTO '{MyConstant.TBL_ThongTinHopDong}' (\"SortId\",\"IsDonGiaKeHoach\",\"Code\",\"CodePl\",\"DonGia\",\"KhoiLuong\",{Col}) " +
                        $"VALUES ('{row.STT}','{IsDonGiaKeHoach}','{CodePL}','{codepl}','{DonGia}','{KL}','{row.ID}')";
                lstPhuLuc.Add(dbString);
                int loaiCT = ps == 0 ? 3 : 4;
                dbString = $"INSERT INTO '{MyConstant.TBL_HopDong_DoBoc}' (\"KhoiLuongToanBo\",\"TheoHopDong\",\"CodePL\",\"Code\",\"CodeDot\",\"LoaiCT\",\"KhoiLuongToanBo_Iscongthucmacdinh\",\"Code_Goc\")" +
                    $" VALUES ('{0}','{KL}','{CodePL}','{codeDB}','{m_codetheodot}','{loaiCT}','{true}','{code_Goc}')";
                lstDoBoc.Add(dbString);
                if (ps == 0)
                {
                    dbString = $"INSERT INTO '{MyConstant.TBL_hopdongAB_HT}' (\"CodeDB\",\"Code\",\"CodeDot\",\"Code_Goc\") VALUES ('{codeDB}','{Guid.NewGuid()}','{m_codetheodot}','{code_Goc}')";
                    lstHT.Add(dbString);
                }
                else
                {
                    dbString = $"INSERT INTO '{MyConstant.TBL_hopdongAB_PS}' (\"CodeDB\",\"Code\",\"CodeDot\",\"Code_Goc\") VALUES ('{codeDB}','{Guid.NewGuid()}','{m_codetheodot}','{code_Goc}')";
                    lstPS.Add(dbString);
                }
                dbString = $"INSERT INTO '{MyConstant.TBL_hopdongAB_TT}' (\"CodeDB\",\"Code\",\"CodeDot\") VALUES ('{codeDB}','{Guid.NewGuid()}','{m_codetheodot}')";
                lstTT.Add(dbString);
                dbString = $"INSERT INTO '{MyConstant.TBL_hopdongAB_TToan}' (\"Code\",\"CodeHopDong\",\"Code_Goc\",{Col}) VALUES ('{Guid.NewGuid()}'," +
                    $"'{m_codeHD}','{code_Goc}','{row.ID}')";
                lstToan.Add(dbString);
            }

            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstPhuLuc);
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstDoBoc);
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstHT);
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstPS);
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstTT);
            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(lstToan);
            WaitFormHelper.CloseWaitForm();
            Fcn_UpDatePLHD();
            long ThanhTien = MyFunction.Fcn_UpdateGiaTriHopDong(m_codeHD);
            dbString = $"UPDATE '{MyConstant.TBL_TaoHopDongMoi}' SET \"GiaTriHopDong\"='{ThanhTien}' WHERE \"Code\"='{m_codeHD}'";
            DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        }
        private void Fcn_UpDatePLHD()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Worksheet worksheet_congtrinh = workbook.Worksheets["Thông tin công trình_Hạng mục"];
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);

            CellRange copy_pl = worksheet_congtrinh.Rows[3];
            CellRange copy_hd = worksheet_congtrinh.Rows[2];
            CellRange copy_ct = worksheet_congtrinh.Rows[4];
            string dbString = $"SELECT * FROM view_PhuLucHopDong_NhomCongTac WHERE CodeHDChinh='{m_codeHD}' ";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dtCongTacTheoKy.AddIndPhanTuyenNhom();
            if (rangeXD.RowCount != 6)
                worksheet.Rows.Remove(6, rangeXD.RowCount - 6);
            worksheet.Rows[6].CopyFrom(copy_ct, PasteSpecial.All);
            worksheet.Rows[5][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText("");
            string lsCodeHD = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["CodeHd"].ToString()).ToArray());
            dbString = $"SELECT * FROM {MyConstant.TBL_ThongtinphulucHD} WHERE \"CodeHd\" IN ({lsCodeHD})";
            DataTable dt_PL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt_PL.Rows.Count == 0)
            {
                dbString = $"SELECT *  FROM {MyConstant.TBL_Tonghopdanhsachhopdong} INNER JOIN {MyConstant.Tbl_TAOMOIHOPDONG} ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code = {MyConstant.TBL_Tonghopdanhsachhopdong}.CodeHopDong WHERE \"CodeHopDong\"='{m_codeHD}'";
                DataTable dtHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                lsCodeHD = MyFunction.fcn_Array2listQueryCondition(dtHD.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
                dbString = $"SELECT * FROM {MyConstant.TBL_ThongtinphulucHD} WHERE \"CodeHd\" IN ({lsCodeHD})";
                dt_PL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            }
            dbString = $"SELECT * FROM {MyConstant.TBL_LoaiHD}";
            DataTable dt_LoaiHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT {MyConstant.Tbl_TAOMOIHOPDONG}.*,{MyConstant.TBL_Tonghopdanhsachhopdong}.Code as CodeHd  FROM  {MyConstant.Tbl_TAOMOIHOPDONG} INNER JOIN {MyConstant.TBL_Tonghopdanhsachhopdong} " +
                $"ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code = {MyConstant.TBL_Tonghopdanhsachhopdong}.CodeHopDong " +
                $" WHERE {MyConstant.Tbl_TAOMOIHOPDONG}.Code='{m_codeHD}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            if (dt_PL.Rows.Count != 0 && dtCongTacTheoKy.Rows.Count != 0)
                worksheet.Rows.Insert(worksheet.Range[MyConstant.TBL_QUYETTHONGTIN].BottomRowIndex, (dtCongTacTheoKy.Rows.Count * 2) * dt_PL.Rows.Count, RowFormatMode.FormatAsPrevious);
            int crRowInd = 6;
            bool check = true;
            Dictionary<string, string> DVTH = null;
            int stt = 1;
            bool IsDonGiaKeHoach = true;
            int sttcon = 1;
            spread_Phuluchopdong.BeginUpdate();
            foreach (DataRow item in dt.Rows)
            {
                worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, true, crRowInd - 1, m_codeHD);

                if (dtCongTacTheoKy.Rows.Count == 0)
                {
                    DataRow[] drs_PL = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["CodeHd"].ToString()).ToArray();
                    if (drs_PL.Count() == 0)
                        break;
                    var tenpl = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["CodeHd"].ToString()).Select(x => x["TenPl"].ToString()).ToList();
                    var codepl = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["CodeHd"].ToString()).Select(x => x["Code"].ToString()).ToList();
                    var codeloaiHD = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["CodeHd"].ToString()).Select(x => x["CodeLoaiHd"].ToString()).ToList();
                    var tenloaiHD = dt_LoaiHD.AsEnumerable().Where(x => x["Code"].ToString() == codeloaiHD[0].ToString()).Select(x => x["LoaiHopDong"].ToString()).ToList();
                    if (check)
                    {
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                        //DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, $"{item["TenHopDong"].ToString()}({item["SoHopDong"]})", true, crRowInd - 1, slke_ThongTinDuAn.EditValue.ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenpl[0]);
                        worksheet.Rows[crRowInd - 2][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codepl[0]);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeHD_HD]].SetValueFromText(item["Code"].ToString());
                        check = false;

                    }
                    else
                    {
                        Row crRowWs = worksheet.Rows[crRowInd++];
                        crRowWs.CopyFrom(copy_hd, PasteSpecial.All);
                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                        worksheet.Rows[crRowInd++].CopyFrom(copy_pl, PasteSpecial.All);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                        //DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, $"{item["TenHopDong"].ToString()}({item["SoHopDong"]})", true, crRowInd - 1, slke_ThongTinDuAn.EditValue.ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenpl[0]);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codepl[0]);

                    }
                }
                var CrowTenPL = dtCongTacTheoKy.AsEnumerable().GroupBy(x => x["CodePl"].ToString());
                foreach (var rowPL in CrowTenPL)
                {
                    var rowPLFirst = rowPL.FirstOrDefault();
                    if (double.Parse(rowPLFirst[MyConstant.COL_HD_Phatsinh].ToString()) != 0)
                    {
                        Row crRowWs = worksheet.Rows[crRowInd++];
                        crRowWs.CopyFrom(copy_pl, PasteSpecial.All);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(rowPLFirst["TenPl"].ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(rowPLFirst["CodePl"].ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Phatsinh]].SetValueFromText(rowPLFirst["PhatSinh"].ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Phuluc]].SetValueFromText($"{"Phụ lục "}{(int.Parse(rowPLFirst["PhatSinh"].ToString()) + 1)}");
                    }
                    else
                    {
                        var tenloaiHD = dt_LoaiHD.AsEnumerable().Where(x => x["Code"].ToString() == rowPLFirst["CodeLoaiHd"].ToString()).Select(x => x["LoaiHopDong"].ToString()).ToList();
                        if (check)
                        {
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(rowPLFirst["TenPl"].ToString());
                            worksheet.Rows[crRowInd - 2][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(rowPLFirst["CodePl"].ToString());
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeHD_HD]].SetValueFromText(item["Code"].ToString());
                            check = false;

                        }
                        else
                        {
                            Row crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs.CopyFrom(copy_hd, PasteSpecial.All);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                            worksheet.Rows[crRowInd++].CopyFrom(copy_pl, PasteSpecial.All);
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(rowPLFirst["TenPl"].ToString());
                            worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(rowPLFirst["CodePl"].ToString());

                        }
                        var grCongTrinh = rowPL.GroupBy(x => x["CodeCongTrinh"].ToString());
                        foreach (var Ctrinh in grCongTrinh)
                        {
                            string crCodeCT = Ctrinh.Key;
                            Row crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs.Font.Bold = true;
                            crRowWs.Font.Color = Color.DarkTurquoise;
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_CT]].SetValue(crCodeCT);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_CONGTRINH);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(Ctrinh.FirstOrDefault()["TenCongTrinh"].ToString());
                            var grHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"].ToString());
                            foreach (var HM in grHangMuc)
                            {
                                string crCodeHM = HM.Key;
                                crRowWs = worksheet.Rows[crRowInd++];
                                crRowWs.Font.Bold = true;
                                crRowWs.Font.Color = Color.DarkGreen;
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_HM]].SetValue(crCodeHM);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_HANGMUC);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(HM.FirstOrDefault()["TenHangMuc"].ToString().ToUpper());
                                var grPhanTuyen = HM.GroupBy(x => (int)x["IndPT"]).OrderBy(x => x.Key);
                                foreach (var Tuyen in grPhanTuyen)
                                {
                                    var fstTuyen = Tuyen.First();
                                    string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : fstTuyen["CodePhanTuyen"].ToString();
                                    if (fstTuyen["CodePhanTuyen"] != DBNull.Value)
                                    {
                                        crRowWs = worksheet.Rows[crRowInd++];
                                        crRowWs.Font.Bold = true;
                                        crRowWs.Font.Color = Color.Red;
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValue(crCodeTuyen);
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_PhanTuyen);
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(fstTuyen["TenTuyen"].ToString().ToUpper());
                                    }
                                    var grTuyenNhom = Tuyen.GroupBy(x => (int)x["IndNhom"])
                                          .OrderBy(x => x.Key);
                                    foreach (var NhomTuyen in grTuyenNhom)
                                    {
                                        var fstNhom = NhomTuyen.First();

                                        string crCodeNhom = (fstNhom["CodeNhom"] == DBNull.Value) ? null : fstNhom["CodeNhom"].ToString();
                                        if (fstNhom["CodeNhom"] != DBNull.Value)
                                        {
                                            sttcon = 1;
                                            IsDonGiaKeHoach = bool.Parse(fstNhom["IsDonGiaKeHoach"].ToString());
                                            crRowWs = worksheet.Rows[crRowInd++];
                                            crRowWs.Font.Bold = false;
                                            crRowWs.Font.Color = MyConstant.color_Row_NhomCongTac;
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValue(stt++);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValue(fstNhom["CodePLHD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_NHOM);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(fstNhom["TenNhom"].ToString().ToUpper());
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_DonVi]].SetValue(fstNhom["DonViNhom"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]].SetValue(fstNhom["KLPLHD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Dongia]].SetValue(fstNhom["DonGiaPLHD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NBD]].SetValue(fstNhom["NBD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NKT]].SetValue(fstNhom["NKT"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]].Formula = $"{DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]}{crRowInd}*{DIC_HOPDONG[MyConstant.COL_HD_Dongia]}{crRowInd}";
                                        }
                                        var grCongTacTuyen = NhomTuyen.GroupBy(x => x["Code"]);
                                        foreach (var CongTac in grCongTacTuyen)
                                        {
                                            var fstCongTac = CongTac.FirstOrDefault();
                                            crRowWs = worksheet.Rows[crRowInd++];
                                            string Mahieu = fstCongTac["MaHieuCongTac"].ToString();
                                            string TenCongTac = fstCongTac["TenCongTac"].ToString();
                                            WaitFormHelper.ShowWaitForm($"{stt}.{Mahieu}_{TenCongTac}");
                                            crRowWs.Font.Bold = false;
                                            crRowWs.Font.Color = crCodeNhom is null ? Color.Black : MyConstant.color_Row_NhomCongTac;
                                            crRowWs.Visible = crCodeNhom is null ? true : false;
                                            //crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValue(stt++);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValue(fstCongTac["CodePLHD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_Congtac]].SetValue(fstCongTac["Code"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValueFromText(Mahieu);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(TenCongTac);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_DonVi]].SetValue(fstCongTac["DonVi"]);
                                            if (crCodeNhom is null)
                                            {
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValue(stt++);
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]].SetValue(fstCongTac["KLPLHD"]);
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Dongia]].SetValue(fstCongTac["DonGiaPLHD"]);
                                            }
                                            else
                                            {
                                                //if (fstCongTac["NBD"] != DBNull.Value)
                                                //{

                                                //}
                                                //else
                                                //{

                                                //}
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValue($"{stt - 1}.{sttcon++}");
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]].SetValue(fstCongTac["KhoiLuongHopDongChiTiet"]);
                                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Dongia]].SetValue(IsDonGiaKeHoach ? fstCongTac["DonGia"] : fstCongTac["DonGiaThiCong"]);
                                                //if (IsDonGiaKeHoach)
                                                //{

                                                //    crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Dongia]].SetValue(fstCongTac["DonGia"]);
                                                //}
                                                //else
                                                //{
                                                //    crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Dongia]].SetValue(fstCongTac["DonGiaThiCong"]);
                                                //}
                                            }
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NBD]].SetValue(fstCongTac["NBD"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NKT]].SetValue(fstCongTac["NKT"]);
                                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]].Formula = $"{DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]}{crRowInd}*{DIC_HOPDONG[MyConstant.COL_HD_Dongia]}{crRowInd}";

                                        }
                                    }
                                    if (crCodeTuyen != null)
                                    {
                                        crRowWs = worksheet.Rows[crRowInd++];
                                        crRowWs.Font.Bold = true;
                                        crRowWs.Font.Color = Color.Red;
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_HoanThanhPhanTuyen);
                                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue($"HOÀN THÀNH {fstTuyen["TenTuyen"]}".ToUpper());
                                    }

                                }

                                crRowWs = worksheet.Rows[crRowInd++];
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue("CT");
                                crRowWs = worksheet.Rows[crRowInd++];
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue("");

                            }

                        }

                    }

                }

            }
            spread_Phuluchopdong.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }
        private void fcn_updatephuluchopdong()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");

            string dbString = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"CodeDuAn\" = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count == 0)
                return;

            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Worksheet worksheet_congtrinh = workbook.Worksheets["Thông tin công trình_Hạng mục"];
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            string queryStr = $"SELECT *  FROM {MyConstant.TBL_Tonghopdanhsachhopdong} INNER JOIN {MyConstant.Tbl_TAOMOIHOPDONG} ON {MyConstant.Tbl_TAOMOIHOPDONG}.Code = {MyConstant.TBL_Tonghopdanhsachhopdong}.CodeHopDong WHERE \"CodeHopDong\"='{m_codeHD}'";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange copy_pl = worksheet_congtrinh.Rows[3];
            CellRange copy_hd = worksheet_congtrinh.Rows[2];
            CellRange copy_ct = worksheet_congtrinh.Rows[4];
            DataTable dtCongTacTheoKy, dtDanhMucCongTac, dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC,false);
            worksheet.Rows[1]["E"].SetValueFromText($"CHI TIẾT PHỤ LỤC GIÁ TRỊ HỢP ĐỒNG: {m_TenHD}");
            queryStr = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"Code\"='{m_codeHD}'";
            DataTable dt_TaoMoiHD = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string condition = "";
            foreach (var item in MyConstant.DIC_NhaThau_ToDoi_NTP)
            {
                if (dt_TaoMoiHD.Rows[0][item.Value].ToString() == dt_TaoMoiHD.Rows[0]["CodeDonViThucHien"].ToString())
                {
                    condition = $"\"{item.Value}\"='{dt_TaoMoiHD.Rows[0][item.Value]}'";
                    break;
                }
            }

            dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeGiaiDoan\" = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {condition} ORDER BY \"RowDoBoc\" ASC";
            if (condition == "")
                dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeGiaiDoan\" = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ORDER BY \"RowDoBoc\" ASC";
            dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string strCodeCT = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray());

            dbString = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac} WHERE \"Code\" IN ({strCodeCT})";
            dtDanhMucCongTac = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lsCodeHD = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());

            dbString = $"SELECT * FROM {MyConstant.TBL_ThongtinphulucHD} WHERE \"CodeHd\" IN ({lsCodeHD})";
            DataTable dt_PL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string lsCodePL = MyFunction.fcn_Array2listQueryCondition(dt_PL.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());

            dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_PhuLuc} WHERE \"CodePl\" IN ({lsCodePL})";
            DataTable dt_CongtacHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {MyConstant.TBL_LoaiHD}";
            DataTable dt_LoaiHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (rangeXD.RowCount != 6)
                worksheet.Rows.Remove(6, rangeXD.RowCount - 6);
            worksheet.Rows[6].CopyFrom(copy_ct, PasteSpecial.All);
            worksheet.Rows[5][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText("");
            if (dt_PL.Rows.Count != 0)
                worksheet.Rows.Insert(worksheet.Range[MyConstant.TBL_QUYETTHONGTIN].BottomRowIndex - 1, (dtCT.Rows.Count + dtHM.Rows.Count + 10) * dt_PL.Rows.Count + dt_PL.Rows.Count * 2 + dt_CongtacHD.Rows.Count, RowFormatMode.FormatAsPrevious);
            int crRowInd = 6;
            bool check = true;
            Dictionary<string, string> DVTH = null;
            spread_Phuluchopdong.BeginUpdate();
            foreach (DataRow item in dt.Rows)
            {
                worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                DVTH = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, true, crRowInd - 1, m_codeHD);
                DataRow[] drs_PL = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["Code"].ToString()).ToArray();
                if (drs_PL.Count() == 0)
                    break;
                var tenpl = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["Code"].ToString()).Select(x => x["TenPl"].ToString()).ToList();
                var codepl = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["Code"].ToString()).Select(x => x["Code"].ToString()).ToList();
                var codeloaiHD = dt_PL.AsEnumerable().Where(x => x["CodeHd"].ToString() == item["Code"].ToString()).Select(x => x["CodeLoaiHd"].ToString()).ToList();
                var tenloaiHD = dt_LoaiHD.AsEnumerable().Where(x => x["Code"].ToString() == codeloaiHD[0].ToString()).Select(x => x["LoaiHopDong"].ToString()).ToList();
                if (check)
                {
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenpl[0]);
                    worksheet.Rows[crRowInd - 2][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codepl[0]);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeHD_HD]].SetValueFromText(item["Code"].ToString());
                    check = false;

                }
                else
                {
                    Row crRowWs = worksheet.Rows[crRowInd++];
                    crRowWs.CopyFrom(copy_hd, PasteSpecial.All);
                    crRowWs[DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenloaiHD[0]);
                    worksheet.Rows[crRowInd++].CopyFrom(copy_pl, PasteSpecial.All);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenpl[0]);
                    worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codepl[0]);

                }
                foreach (var rowpl in drs_PL)
                {
                    DataRow[] drs_CT_1 = dt_CongtacHD.AsEnumerable().Where(x => x["CodePl"].ToString() == rowpl["Code"].ToString()).ToArray();
                    if (Double.Parse(rowpl[MyConstant.COL_HD_Phatsinh].ToString()) != 0)
                    {
                        Row crRowWs = worksheet.Rows[crRowInd++];
                        crRowWs.CopyFrom(copy_pl, PasteSpecial.All);
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValueFromText($"{item["TenHopDong"]}({item["SoHopDong"]})");
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(rowpl["TenPl"].ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(rowpl["Code"].ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Phatsinh]].SetValueFromText(rowpl["PhatSinh"].ToString());
                        worksheet.Rows[crRowInd - 1][DIC_HOPDONG[MyConstant.COL_HD_Phuluc]].SetValueFromText($"{"Phụ lục "}{(Int32.Parse(rowpl["PhatSinh"].ToString()) + 1)}");
                    }
                    foreach (DataRow rowct in dtCT.Rows)
                    {
                        Row crRowWs = worksheet.Rows[crRowInd++];
                        string crCodeCT = rowct["Code"].ToString();
                        crRowWs.Font.Bold = true;
                        crRowWs.Font.Color = Color.DarkTurquoise;
                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_CT]].SetValue(crCodeCT);
                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_CONGTRINH);
                        crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(rowct["Ten"].ToString());
                        foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                        {
                            string crCodeHM = HM["Code"].ToString();
                            crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs.Font.Bold = true;
                            crRowWs.Font.Color = Color.DarkGreen;
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_HM]].SetValue(crCodeHM);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(MyConstant.CONST_TYPE_HANGMUC);
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(HM["Ten"].ToString().ToUpper());
                            var lsDanhMucCongTac = dtDanhMucCongTac.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToDictionary(x => x["Code"].ToString(), y => y["TenCongTac"].ToString());
                            DataRow[] drs_CTTheoKy = dtCongTacTheoKy.AsEnumerable().Where(x => lsDanhMucCongTac.Keys.Contains(x["CodeCongTac"].ToString())).ToArray();
                            Dictionary<string, string> strCodeCTTheoKy = drs_CTTheoKy.AsEnumerable().ToDictionary(x => x["Code"].ToString(), y => y["CodeCongTac"].ToString());
                            DataRow[] dt_CTCon = drs_CT_1.AsEnumerable().Where(x => strCodeCTTheoKy.Keys.Contains(x["CodeCongTacTheoGiaiDoan"].ToString())).ToArray();
                            foreach (var rowctc in dt_CTCon)
                            {
                                var mahieucongtac = dtDanhMucCongTac.AsEnumerable()
                                                    .Where(x => x["Code"].ToString() == strCodeCTTheoKy[rowctc["CodeCongTacTheoGiaiDoan"].ToString()].ToString()).Select(x => x["MaHieuCongTac"].ToString()).ToList();
                                var donvi = dtDanhMucCongTac.AsEnumerable()
                                                   .Where(x => x["Code"].ToString() == strCodeCTTheoKy[rowctc["CodeCongTacTheoGiaiDoan"].ToString()].ToString()).Select(x => x["DonVi"].ToString()).ToList();
                                crRowWs = worksheet.Rows[crRowInd++];
                                crRowWs.Font.Bold = true;
                                crRowWs.Font.Color = Color.Black;
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValue(rowctc["Code"]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Code_Congtac]].SetValue(rowctc["CodeCongTacTheoGiaiDoan"]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue(mahieucongtac[0]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(lsDanhMucCongTac[strCodeCTTheoKy[rowctc["CodeCongTacTheoGiaiDoan"].ToString()].ToString()]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_DonVi]].SetValue(donvi[0]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]].SetValue(rowctc[MyConstant.COL_HD_Khoiluong]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Dongia]].SetValue(rowctc[MyConstant.COL_HD_Dongia]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NBD]].SetValue(rowctc[MyConstant.COL_HD_NBD]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_NKT]].SetValue(rowctc[MyConstant.COL_HD_NKT]);
                                crRowWs[DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]].Formula = $"{DIC_HOPDONG[MyConstant.COL_HD_Khoiluong]}{crRowInd}*{DIC_HOPDONG[MyConstant.COL_HD_Dongia]}{crRowInd}";
                            }
                            crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue("CT");
                            crRowWs = worksheet.Rows[crRowInd++];
                            crRowWs[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].SetValue("");
                        }
                    }

                }
            }
            spread_Phuluchopdong.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }

        private void Form_PhuLucHopDong_FormClosed(object sender, FormClosedEventArgs e)
        {
            spread_Phuluchopdong.Dispose();
            GC.Collect();
            this.Close();
        }

        private void spread_Phuluchopdong_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {
            IWorkbook workbook = spread_Phuluchopdong.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Worksheet worksheet_congtrinh = workbook.Worksheets["Thông tin công trình_Hạng mục"];
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
            string colHeading = worksheet.Columns[e.Cell.ColumnIndex].Heading;
            string colDB = worksheet.Rows[0][e.ColumnIndex].Value.TextValue;
            CellRange copy_pl = worksheet_congtrinh.Rows[3];
            int beginindex = 0;
            int endindex = 0;
            string queryStr = "", dbString = "";
            int checkso;
            CellRange ct_hm = worksheet.Range["D7:F8"];
            if (colHeading == DIC_HOPDONG[MyConstant.COL_HD_Dongia] || colHeading == DIC_HOPDONG[MyConstant.COL_HD_Khoiluong])
            {
                bool check = Int32.TryParse(e.Value.ToString(), out checkso);
                if (check == false)
                    e.Cell.Value = e.OldValue;
                dbString = $"UPDATE '{MyConstant.TBL_HopDong_PhuLuc}' SET '{colDB}'=@Value WHERE \"Code\"='{worksheet.Rows[e.RowIndex][DIC_HOPDONG["Code"]].Value.TextValue}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                return;
            }
            else if (colHeading == DIC_HOPDONG[MyConstant.COL_HD_Kyhieu])
            {
                int pl = 1;
                if (e.Value.ToString() == "PL" && e.OldValue.ToString() != "")
                {
                    worksheet.Rows.Insert(e.RowIndex, 2, RowFormatMode.FormatAsPrevious);
                    worksheet.Rows[e.RowIndex + 1][e.ColumnIndex].Value = "CT";
                    worksheet.Rows[e.RowIndex + 2][e.ColumnIndex].Value = "";
                    worksheet.Rows[e.RowIndex].CopyFrom(copy_pl, PasteSpecial.All);
                    for (int i = e.RowIndex - 1; i > rangeXD.TopRowIndex; i--)
                    {
                        Row crRow = worksheet.Rows[i];
                        string kyhieu = crRow[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString();
                        if (kyhieu == "PL" && worksheet.Rows[i - 1][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "HD")
                        {
                            beginindex = i;
                            worksheet.Rows[e.RowIndex][DIC_HOPDONG[MyConstant.COL_HD_Phatsinh]].Value = pl.ToString();
                            worksheet.Rows[e.RowIndex][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].Value = crRow[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].Value.ToString();
                            worksheet.Rows[beginindex][DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]].Formula = $"SUM({DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]}{beginindex + 2}:{DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]}{e.RowIndex})";
                            worksheet.Rows[beginindex][DIC_HOPDONG[MyConstant.COL_HD_ThanhTienthicong]].Formula = $"SUM({DIC_HOPDONG[MyConstant.COL_HD_ThanhTienthicong]}{beginindex + 2}:{DIC_HOPDONG[MyConstant.COL_HD_ThanhTienthicong]}{e.RowIndex})";
                            break;
                        }
                        else if (kyhieu == "PL" && worksheet.Rows[i - 1][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() != "HD")
                            pl++;

                    }
                }
                if (e.OldValue.ToString() == "")
                {
                    worksheet.Rows.Insert(e.RowIndex + 1, 3, RowFormatMode.FormatAsPrevious);
                    worksheet.Rows[e.RowIndex + 2][e.ColumnIndex].Value = "CT";
                    worksheet.Rows[e.RowIndex][e.ColumnIndex].Value = "";
                    worksheet.Rows[e.RowIndex + 1].CopyFrom(copy_pl, PasteSpecial.All);
                    for (int i = e.RowIndex - 1; i > rangeXD.TopRowIndex; i--)
                    {
                        Row crRow = worksheet.Rows[i];
                        string kyhieu = crRow[DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString();
                        if (kyhieu == "PL" && worksheet.Rows[i - 1][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "HD")
                        {
                            beginindex = i;
                            string codepl = Guid.NewGuid().ToString();
                            string codeloaiHD = worksheet.Rows[i - 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.ToString();
                            string codehd = crRow[DIC_HOPDONG[MyConstant.COL_HD_CodeHD_HD]].Value.TextValue;
                            string tenpl = $"Phụ lục {pl + 1}";
                            worksheet.Rows[e.RowIndex + 1][DIC_HOPDONG[MyConstant.COL_HD_Phuluc]].SetValueFromText(tenpl);
                            worksheet.Rows[e.RowIndex + 1][DIC_HOPDONG[MyConstant.COL_HD_TenPL]].SetValueFromText(tenpl);
                            worksheet.Rows[e.RowIndex + 1][DIC_HOPDONG[MyConstant.COL_HD_Phatsinh]].Value = pl.ToString();
                            worksheet.Rows[e.RowIndex + 1][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].Value = crRow[DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].Value.ToString();
                            worksheet.Rows[e.RowIndex + 1][DIC_HOPDONG[MyConstant.COL_HD_DVTH]].Value = crRow[DIC_HOPDONG[MyConstant.COL_HD_DVTH]].Value.ToString();
                            dbString = $"INSERT INTO '{MyConstant.TBL_ThongtinphulucHD}' (\"PhatSinh\",\"Code\",\"CodeLoaiHd\",\"CodeHd\",\"TenPl\") VALUES ('{pl}','{codepl}','{codeloaiHD}','{codehd}','{tenpl}')";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            worksheet.Rows[e.RowIndex + 1][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codepl);
                            worksheet.Rows[e.RowIndex + 1][DIC_HOPDONG[MyConstant.COL_HD_CodeHD_HD]].SetValueFromText(codehd);
                            worksheet.Rows[e.RowIndex][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].SetValueFromText(codeloaiHD);
                            //worksheet.Rows[beginindex][DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]].Formula = $"SUM({DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]}{beginindex + 2}:{DIC_HOPDONG[MyConstant.COL_HD_ThanhTien]}{e.RowIndex})";
                            //worksheet.Rows[beginindex][DIC_HOPDONG[MyConstant.COL_HD_ThanhTienthicong]].Formula = $"SUM({DIC_HOPDONG[MyConstant.COL_HD_ThanhTienthicong]}{beginindex + 2}:{DIC_HOPDONG[MyConstant.COL_HD_ThanhTienthicong]}{e.RowIndex})";
                            break;
                        }
                        else if (kyhieu == "PL" && worksheet.Rows[i - 1][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() != "HD")
                            pl++;
                    }
                }
                if (e.Value.ToString() != "PL")
                    worksheet.Rows[e.RowIndex][e.ColumnIndex].Value = e.OldValue;
            }
            else if (colHeading == DIC_HOPDONG[MyConstant.COL_HD_TenPL] && worksheet.Rows[e.RowIndex][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "HD")
                worksheet.Rows[e.RowIndex][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value = MyConstant.DIC_LOAIHOPDONG[e.Value.ToString()];
            else if (colHeading == DIC_HOPDONG[MyConstant.COL_HD_TenPL] && worksheet.Rows[e.RowIndex][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "PL")
            {
                dbString = $"UPDATE  '{MyConstant.TBL_ThongtinphulucHD}' SET \"TenPl\"=@Ten WHERE \"Code\"='{worksheet.Rows[e.RowIndex][DIC_HOPDONG[MyConstant.COL_HD_CodeCT]].Value.TextValue}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
            }
            else if (colHeading == DIC_HOPDONG[MyConstant.COL_HD_Tencongviec])
            {
                if (worksheet.Rows[e.RowIndex][DIC_HOPDONG[MyConstant.COL_HD_Kyhieu]].Value.ToString() == "PL")
                {
                    Dictionary<string, string> check = MyFunction.fcn_getdonvithuchien(spread_Phuluchopdong, true, e.RowIndex, m_codeHD);
                }
            }
        }

        private void sb_HoanThanh_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_PhuLucHopDong_Load(object sender, EventArgs e)
        {
            FileHelper.fcn_spSheetStreamDocument(spread_Phuluchopdong, $@"{BaseFrom.m_templatePath}\FileExcel\4.aPhuLucHopDong_Full.xlsx");
            //fcn_updatephuluchopdong();
            Fcn_UpDatePLHD();
        }
    }
}
