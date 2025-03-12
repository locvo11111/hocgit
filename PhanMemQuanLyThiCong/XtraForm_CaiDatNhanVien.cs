using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraSpreadsheet.Commands;
using DevExpress.XtraSpreadsheet.Menu;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.ChamCong;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.ChamCong
{
    public partial class XtraForm_CaiDatNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        Uc_TenNhanVien m_ucTenNV = new Uc_TenNhanVien();
        public XtraForm_CaiDatNhanVien()
        {
            InitializeComponent();
            m_ucTenNV.de_TranDanhSachNV = new Uc_TenNhanVien.DE_TransDanhSachNV(fcn_DE_DanhSachNV);
            m_ucTenNV.Dock = DockStyle.None;
            this.Controls.Add(m_ucTenNV);
            m_ucTenNV.Hide();
        }
        ctrl_DanhSachNhanVien m_ctrlDanhSachNV;


        //public void reload 

        private void fcn_DE_DanhSachNV(List<DanhSachNhanVienModel> DanhSachNV)
        {
            WaitFormHelper.ShowWaitForm("Đang thêm nhân viên vào công tác");
            foreach (var item in DanhSachNV)
            {
                string dbString = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} " +
$"(Code,CodeNhanVien,HeSoSang,HeSoChieu,HeSoToi,NgayThang,{m_ucTenNV._ColCode}) VALUES " +
$"('{Guid.NewGuid()}','{item.Code}','{0.125}','{0.125}','{0.125}','{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'," +
$"'{m_ucTenNV._Code}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            WaitFormHelper.CloseWaitForm();
            Fcn_LoadDaTaCongNhatCongKhoan(spsheet_BangCongNhatCongKhoan, cbb_LayCongTacNgoaiKeHoach.Checked);
        }
        private void fcn_handle_txtBoxTimKiemNhanVien_TextChange(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Length < 2)
                return;
            string strFull = MyFunction.fcn_RemoveAccents(tb.Text);
            List<string> strLs = strFull.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<TamUngModel> Lst;
            string queryStr = "";
            string str;
            string NameCot;
            if (m_ctrlDanhSachNV.m_NameCot == "MaNhanVien")
            {
                string condition = "";
                NameCot = m_ctrlDanhSachNV.m_NameCot;
                if (strLs.Count == 1)
                {
                    str = strLs[0];
                    queryStr = $"SELECT NV.*,VT.Ten as ChucVu FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NV " +
                        $"LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} VT ON VT.Code=NV.ChucVu " +
                        $"WHERE ({NameCot} LIKE '%{str}%')";
                }
                else
                {
                    foreach (string strSearch in strLs)
                    {
                        condition += $" AND {NameCot} LIKE '%{strSearch}%'";
                    }
                    queryStr += condition.Remove(0, 4);
                }
            }
            else if ((m_ctrlDanhSachNV.m_NameCot == "TenNhanVien"))
            {
                NameCot = "\"TenNhanVienKhongDau\"";
                if (strLs.Count == 1)
                {
                    str = strLs[0];
                    queryStr = $"SELECT NV.*,VT.Ten as ChucVu FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NV " +
                        $"LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} VT ON VT.Code=NV.ChucVu " +
                        $"WHERE ({NameCot} LIKE '%{str}%')";
                }
                else
                {
                    queryStr = $"SELECT NV.*,VT.Ten as ChucVu FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NV" +
                        $"LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} VT ON VT.Code=NV.ChucVu " +
                        $" WHERE ";
                    string condition = "";
                    foreach (string strSearch in strLs)
                    {
                        condition += $" AND {NameCot} LIKE '%{strSearch}%'";
                    }
                    queryStr += condition.Remove(0, 4);//Xóa bỏ chữ AND sau WHERE
                    queryStr += $"AND \"LoaiVatTu\"='{"Vật liệu"}'";
                }
            }
            Lst = DataProvider.InstanceTHDA.ExecuteQueryModel<TamUngModel>(queryStr);

            m_ctrlDanhSachNV.Show();
            m_ctrlDanhSachNV.Parent = tb.Parent.Parent;

            m_ctrlDanhSachNV.Location = new Point(tb.Parent.Location.X + tb.Width, tb.Parent.Parent.Location.Y + 40);
            m_ctrlDanhSachNV.Size = new Size(tb.Parent.Parent.Width - (tb.Parent.Location.X + tb.Width), tb.Parent.Parent.Height - 150);
            m_ctrlDanhSachNV.BringToFront();
            m_ctrlDanhSachNV.Fcn_Update(Lst);
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
        private void fcn_DE_NhanDataDanhSachNV(List<TamUngModel> Lst)
        {
            string Db_String = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG}";
            DataTable Dt_TamUng = DataProvider.InstanceTHDA.ExecuteQuery(Db_String);
            int SortId = Dt_TamUng.Rows.Count;
            Dt_TamUng.Clear();
            foreach (var item in Lst)
            {
                DataRow NewRow = Dt_TamUng.NewRow();
                NewRow["Code"] = Guid.NewGuid();
                NewRow["CodeDuAn"] = SharedControls.slke_ThongTinDuAn.EditValue;
                NewRow["SortId"] = SortId++;
                NewRow["CodeNhanVien"] = item.Code;
                NewRow["NgayThangUng"] = de_ThangTamUng.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                Dt_TamUng.Rows.Add(NewRow);
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(Dt_TamUng, DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG);
            Fcn_LoadTamUng();
            spsheet_TamUng.CloseCellEditor(CellEditorEnterValueMode.Cancel);
        }
        private void Fcn_LoadTamUng()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            Worksheet ws = spsheet_TamUng.Document.Worksheets[0];
            CellRange RangeData = ws.Range["Data_TamUng"];
            RangeData.ClearContents();
            Dictionary<string, string> Dic = MyFunction.fcn_getDicOfColumn(RangeData);
            DateTime date = de_ThangTamUng.DateTime;
            DateTime Min = new DateTime(date.Year, date.Month, 1);
            DateTime Max = Min.AddMonths(1).AddDays(-1);
            string MinDay = Min.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string MaxDay = Max.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string Db_String = $"SELECT {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG}.*,{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.MaNhanVien," +
                $"{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.Code as CodeNhanVien," +
                $"{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.TenNhanVien," +
                $"{DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}.Ten as ChucVu" +
                $",{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.IsNhanVienCty  " +
                $"FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG}" +
                $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} ON {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG}.CodeNhanVien={DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.Code " +
                $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} ON {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}.Code={DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.ChucVu " +
                $" WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG}.NgayThangUng<='{MaxDay}' AND {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG}.NgayThangUng>='{MinDay}'";
            List<TamUngModel> Dt_TamUng = DataProvider.InstanceTHDA.ExecuteQueryModel<TamUngModel>(Db_String);
            spsheet_TamUng.BeginUpdate();
            int RowIndex = RangeData.TopRowIndex;
            if (RangeData.RowCount <= Dt_TamUng.Count())
                ws.Rows.Insert(RangeData.TopRowIndex, Dt_TamUng.Count(), RowFormatMode.FormatAsNext);
            Row Crow = ws.Rows[RowIndex];
            int STT = 1;
            foreach (TamUngModel item in Dt_TamUng)
            {
                Crow = ws.Rows[RowIndex++];
                Crow[Dic["STT"]].SetValue(STT++);
                Crow[Dic[DanhSachNhanVienConstant.COL_TenNhanVien]].SetValueFromText(item.TenNhanVien);
                Crow[Dic[DanhSachNhanVienConstant.COL_MaNhanVien]].SetValueFromText(item.MaNhanVien);
                Crow[Dic[DanhSachNhanVienConstant.COL_ChucVu]].SetValueFromText(item.ChucVu);
                Crow[Dic[DanhSachNhanVienConstant.COL_NoiDungUng]].SetValueFromText(item.NoiDungTamUng);
                Crow[Dic[DanhSachNhanVienConstant.COL_SoTien]].SetValue(item.SoTien);
                Crow[Dic[DanhSachNhanVienConstant.COL_LoaiNhanVien]].SetValueFromText(item.LoaiNhanVien);
                Crow[Dic[DanhSachNhanVienConstant.COL_GhiChu]].SetValueFromText(item.GhiChu);
                Crow[Dic[DanhSachNhanVienConstant.COL_Code]].SetValueFromText(item.Code);
                Crow[Dic[DanhSachNhanVienConstant.COL_CodeNhanVien]].SetValueFromText(item.CodeNhanVien);
                Crow[Dic[DanhSachNhanVienConstant.COL_NgayThangUng]].SetValueFromText(item.NgayThangUng.HasValue ? item.NgayThangUng.Value.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET) : "");
                ws.Hyperlinks.Add(Crow[Dic[DanhSachNhanVienConstant.COL_GiayTo]], $"{Dic[DanhSachNhanVienConstant.COL_GiayTo]}{RowIndex}", false, "Xem chi tiết");
            }
            spsheet_TamUng.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }
        private void spsheet_TamUng_CellBeginEdit(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellCancelEventArgs e)
        {
            Worksheet ws = spsheet_TamUng.Document.Worksheets[0];
            CellRange RangeData = ws.Range["Data_TamUng"];
            Dictionary<string, string> Dic = MyFunction.fcn_getDicOfColumn(RangeData);
            string Code = ws.Rows[e.RowIndex][Dic[DanhSachNhanVienConstant.COL_Code]].Value.TextValue;
            if (!RangeData.Contains(e.Cell))
                e.Cancel = true;
            string Heading = ws.Columns[e.ColumnIndex].Heading;
            if (Heading == Dic["STT"])
            {
                e.Cancel = true;
                return;
            }
            if (Heading == Dic[DanhSachNhanVienConstant.COL_MaNhanVien])
            {
                if (Code.HasValue())
                    return;
                m_ctrlDanhSachNV.Fcn_LoadTenCot("MaNhanVien");
                BeginInvoke(new Action<SpreadsheetControl>((ss) =>
                {

                    TextBox tb = FindTextBox(ss.Controls);
                    if (tb != null)
                    {
                        tb.TextChanged += fcn_handle_txtBoxTimKiemNhanVien_TextChange;
                    }
                }), sender as SpreadsheetControl);
            }
            else if (Heading == Dic[DanhSachNhanVienConstant.COL_TenNhanVien])
            {
                if (Code.HasValue())
                    return;
                m_ctrlDanhSachNV.Fcn_LoadTenCot("TenNhanVien");
                BeginInvoke(new Action<SpreadsheetControl>((ss) =>
                {

                    TextBox tb = FindTextBox(ss.Controls);
                    if (tb != null)
                    {
                        tb.TextChanged += fcn_handle_txtBoxTimKiemNhanVien_TextChange;
                    }
                }), sender as SpreadsheetControl);
            }
        }

        private void spsheet_TamUng_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            Worksheet ws = spsheet_TamUng.Document.Worksheets[0];
            CellRange RangeData = ws.Range["Data_TamUng"];
            Dictionary<string, string> Dic = MyFunction.fcn_getDicOfColumn(RangeData);
            string Code = ws.Rows[e.RowIndex][Dic[DanhSachNhanVienConstant.COL_Code]].Value.TextValue;
            string colum = ws.Rows[0][e.ColumnIndex].Value.TextValue;
            if (colum == "TenNhanVien" || colum == "MaNhanVien" || colum == "ChucVu" || colum == "LoaiNhanVien" || colum == "CodeDVPH" || colum == "DuAn")
                return;
            string Value = colum == "NgayThangUng" ? (DateTime.Parse(e.Value.ToString())).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : e.Value.ToString();
            string db_string = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG} SET" +
            $" '{colum}'='{Value}' WHERE \"Code\"='{Code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
        }

        private void spsheet_TamUng_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
        {
            Worksheet ws = spsheet_TamUng.Document.Worksheets[0];
            CellRange RangeData = ws.Range["Data_TamUng"];
            Dictionary<string, string> Dic = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Range = ws.SelectedCell;
            string Code = ws.Rows[Range.TopRowIndex][Dic[DanhSachNhanVienConstant.COL_Code]].Value.TextValue;
            string NoiDung = ws.Rows[Range.TopRowIndex][Dic[DanhSachNhanVienConstant.COL_NoiDungUng]].Value.TextValue;
            FormLuaChon luachon = new FormLuaChon(Code, FileManageTypeEnum.ChamCong_TamUng, NoiDung);
            luachon.ShowDialog();
        }
        public void Fcn_UpDate()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            date_NhapLieu.EditValueChanged -= date_NhapLieu_EditValueChanged;
            InitDataControl();
            date_NhapLieu.EditValueChanged += date_NhapLieu_EditValueChanged;
            de_ThangTinhLuong.DateTime = de_ThangTamUng.DateTime = de_ThangTinLuongNCQL.DateTime = DateTime.Now;
            m_ctrlDanhSachNV = new ctrl_DanhSachNhanVien();
            m_ctrlDanhSachNV.m_DataChonNV = new ctrl_DanhSachNhanVien.DE_TRUYENDATANHANVIEN(fcn_DE_NhanDataDanhSachNV);
            m_ctrlDanhSachNV.Dock = DockStyle.None;
            this.Controls.Add(m_ctrlDanhSachNV);
            m_ctrlDanhSachNV.Hide();
            FileHelper.fcn_spSheetStreamDocument(spsheet_TamUng, $@"{BaseFrom.m_templatePath}\FileExcel\12.dTamUngLuong.xls"); //Tam ứng lương
            FileHelper.fcn_spSheetStreamDocument(spsheet_BangTinhLuong, $@"{BaseFrom.m_templatePath}\FileExcel\12.bBangTinhLuong.xls");  //Banngr tính lương trước khi thanh toan
            FileHelper.fcn_spSheetStreamDocument(spsheet_BangChamCong, $@"{BaseFrom.m_templatePath}\FileExcel\12.aChamCong.xls"); // Chấm công cá nhân và chấm hộ đội thi công
            FileHelper.fcn_spSheetStreamDocument(spsheet_TongLuongCaNhan, $@"{BaseFrom.m_templatePath}\FileExcel\12.hTongLuongCaNhan.xls"); // Hiển thi tổng lương cá nhân thực lĩnh hàng tháng
            WaitFormHelper.CloseWaitForm();
        }

        public void xtraTabControl_BangNhanCong_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

            if (xtraTabControl_BangNhanCong.SelectedTabPage == xtraTabPage_BangNV)
                danhSachNhanVien.Fcn_Update();
            else if (xtraTabControl_BangNhanCong.SelectedTabPage == xtraTabPage_DanhSachNgoaiCty)
                danhSachNhanVienNgoaiCty.Fcn_Update(false);
            else if (xtraTabControl_BangNhanCong.SelectedTabPage == xtraTabPage_CaiDatChamCong)
            {
                xtraTabControl1.SelectedTabPage = null;
            }
            else if (xtraTabControl_BangNhanCong.SelectedTabPage == xtraTabPage_BangChamCong)
            {
                xtraTabControl2.SelectedTabPage = null;
                caiDatGioLamChamCong.Fcn_Update(true);
            }
            else if (xtraTabControl_BangNhanCong.SelectedTabPage == xtraTabPage_TTLuong)
            {
                Fcn_UpDateBangThanhToanLuong();
            }
            else if (xtraTabControl_BangNhanCong.SelectedTabPage == xtraTabPage_TinhLuong)
            {
                Fcn_UpdateBangTinhLuong();
            }
            else if (xtraTabControl_BangNhanCong.SelectedTabPage == xtraTabPage_TamUng)
            {
                Fcn_LoadTamUng();
            }
            else if (xtraTabControl_BangNhanCong.SelectedTabPage == xtraTabPage_CongNhat)
            {
                Fcn_LoadDaTaCongNhatCongKhoan(spsheet_BangCongNhatCongKhoan, cbb_LayCongTacNgoaiKeHoach.Checked);
            }
            else if (xtraTabControl_BangNhanCong.SelectedTabPage == xtraTabPage_AnhThe)
            {
                ctrl_AnhTheNhanVien.Fcn_Update();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page is null)
                return;
            if (e.Page == xtraTabPage_HeSo)
                caiDatChamCongDuAn.Fcn_Update();
            else
                caiDatGioLamChamCong.Fcn_Update();
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == xtraTabPage_ChamCong)
            {
                chamCongCa_Nhan.Fcn_LoadData();
            }
            else if (e.Page == xtraTabPage_LuongCaNhan)
            {
                string m_path = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}");
                string m_TemPHinhAnh = $@"{BaseFrom.m_templatePath}\FileHinhAnh\QLTC.jpg";
                if (!Directory.Exists(m_path))
                    Directory.CreateDirectory(m_path);
                string dbString = $"SELECT NV.*,VT.Ten as ChucVu,PB.Ten as PhongBan FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NV " +
                    $"LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} VT " +
                    $" ON VT.Code=NV.ChucVu LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_TENPHONGBAN}" +
                    $" PB ON PB.Code=NV.PhongBan" +
                     $"LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} DINHKEM ON DINHKEM.CodeParent=NV.Code AND DINHKEM.State=1 " +
                $" WHERE NV.IsNhanVienCty='{true}' OR NV.IsNhanVienCty=1";
                List<DanhSachNhanVienModel> DanhSachNV = DataProvider.InstanceTHDA.ExecuteQueryModel<DanhSachNhanVienModel>(dbString);
                //DanhSachNV.ForEach(x => { x.FilePath = $@"{m_path}\{x.Code}\{x.HinhAnh}"; x.PhoToInit(); });
                slue_TenNhanVien.Properties.DataSource = DanhSachNV;
                //TongHopHelper.CreatePictureEdit(slue_TenNhanVien, "PhoTo");
            }
        }

        private void TabChamCongAll_Load(object sender, EventArgs e)
        {
            xtraTabControl_BangNhanCong.SelectedTabPage = null;
            de_ThangTinhLuongCaNhan.DateTime = DateTime.Now;

        }
        private void Fcn_UpDateChamCongCaNhan()
        {
            IWorkbook workbook = spsheet_BangChamCong.Document;
            Worksheet ws = workbook.Worksheets[0];
            CellRange range = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            CellRange rangeKyHieu = ws.Range["Range_KyHieu"];
            range.ClearContents();
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(range);
            string dbString = $"SELECT NV.*,PB.Ten as PhongBan,VITRI.Ten as ChucVu " +
                    $"FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NV" +
                     $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_TENPHONGBAN} PB ON PB.Code=NV.PhongBan " +
                  $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} VITRI ON VITRI.Code=NV.ChucVu " +
                  $"WHERE NV.IsNhanVienCty='{false}'";
            List<DanhSachNhanVienModel> DanhSachNV = DataProvider.InstanceTHDA.ExecuteQueryModel<DanhSachNhanVienModel>(dbString);
            spsheet_BangChamCong.BeginUpdate();

            if (range.RowCount - 2 <= DanhSachNV.Count())
                ws.Rows.Insert(range.BottomRowIndex, DanhSachNV.Count(), RowFormatMode.FormatAsPrevious);
            DateTime date = de_ThangTinLuongNCQL.DateTime;
            DateTime Min = new DateTime(date.Year, date.Month, 1);
            DateTime Max = Min.AddMonths(1).AddDays(-1);
            for (DateTime i = Min; i <= Max; i = i.AddDays(1))
            {
                ws.Rows[2][Name[i.Day.ToString()]].SetValueFromText(DanhSachNhanVienConstant.NgayTrongTuan[i.DayOfWeek]);
            }
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_KYHIEU} ";
            DataTable KyHieu = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataRow RowKyHieu = KyHieu.AsEnumerable().SingleOrDefault();
            double HeSoChamCong = double.Parse(RowKyHieu["PhanTramChamCong"].ToString()) / 100;
            for (int i = rangeKyHieu.LeftColumnIndex; i <= rangeKyHieu.RightColumnIndex; i++)
            {
                string FieldName = ws.Rows[0][i].Value.TextValue;
                ws.Rows[3].SetValue(RowKyHieu[FieldName]);
            }
            int STT = 1;
            int RowIndex = range.TopRowIndex;
            double ChamCong = 0, NghiPhep = 0, NghiOm = 0, Hoctap = 0, NghiDe = 0, NghiKoLD = 0, CongTac = 0, NghiTheoCheDo = 0, NghiThu7cn = 0;

            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATHESO} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable HeSo = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            Dictionary<string, string> DayDetail = new Dictionary<string, string>();
            Dictionary<string, double> NN = new Dictionary<string, double>();
            double HeSoSangChieuToi = 0;

            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM} WHERE" +
                $"  \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            List<CaiDaGioLamChamCong> GioLamMacDinh = DataProvider.InstanceTHDA.ExecuteQueryModel<CaiDaGioLamChamCong>(dbString);

            foreach (var itemNV in DanhSachNV)
            {
                DayDetail.Clear();
                NN.Clear();
                Row Crow = ws.Rows[RowIndex++];
                Crow.Visible = true;
                Crow[Name[DanhSachNhanVienConstant.COL_Code]].SetValue(itemNV.Code);
                Crow[Name[DanhSachNhanVienConstant.COL_STT]].SetValue(STT++);
                Crow[Name[DanhSachNhanVienConstant.COL_MaNhanVien]].SetValue(itemNV.MaNhanVien);
                Crow[Name[DanhSachNhanVienConstant.COL_TenNhanVien]].SetValue(itemNV.TenNhanVien);
                Crow[Name["PhongBan"]].SetValue(itemNV.PhongBan);
                Crow[Name["ChucVu"]].SetValue(itemNV.ChucVu);

                dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} WHERE " +
                $"\"CodeNhanVien\"='{itemNV.Code}' AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'" +
                $" AND \"DateTime\"<='{Max}' AND \"DateTime\">='{Min}'";
                List<ChiTietChamCong_Gio_Ngay> GioThucTe = DataProvider.InstanceTHDA.ExecuteQueryModel<ChiTietChamCong_Gio_Ngay>(dbString);
                DataRow HeSoNhanVien = HeSo.AsEnumerable().Where(x => x["LoaiCaiDat"].ToString() == DanhSachNhanVienConstant.LoaiNhanVien[itemNV.NhanVienVanPhongCongTruong].ToString()).SingleOrDefault();
                List<CaiDaGioLamChamCong> GioLamMacDinhNV = GioLamMacDinh.Where(x => x.CongTruong_VanPhong == itemNV.NhanVienVanPhongCongTruong).ToList();
                foreach (var item in GioThucTe)
                {
                    item.ChiTietChamCong();
                    if (item.Buoi != "Theo giờ")
                    {
                        CaiDaGioLamChamCong _GioLam = GioLamMacDinhNV.Where(x => x.BuoiSang_Chieu_Toi ==
item.Buoi && x.NgayBatDau <= item.DateTime.Value && x.NgayKetThuc >= item.DateTime.Value).FirstOrDefault();
                        item.GioRaMacDinh = _GioLam.GioRa;
                        item.PhutRaMacDinh = _GioLam.PhutRa;
                        item.GioVaoMacDinh = _GioLam.GioVao;
                        item.PhutVaoMacDinh = _GioLam.PhutVao;
                        item.Fcn_CheckVaoTreRaSom();
                        item.ChiTietChamCong();
                    }
                    if (item.Label == 3)///NGHỈ
                    {
                        double HeSoNghi = double.Parse(RowKyHieu[DanhSachNhanVienConstant.PhanTramNN[item.NghiLam]].ToString()) / 100;
                        if (NN.Keys.Contains(item.NghiLam))
                        {

                        }
                        else
                            NN.Add(item.NghiLam, HeSoNghi * HeSoSangChieuToi);

                    }
                    else if (item.Label == 4)//TĂNG CA
                    {
                        double HeSoTangCa = double.Parse(HeSoNhanVien[item.TangCa].ToString());
                        ChamCong += HeSoChamCong * HeSoSangChieuToi * HeSoTangCa;
                    }
                    else//CHẤM CÔNG
                    {
                        ChamCong += HeSoChamCong * HeSoSangChieuToi;
                    }

                }

            }
            spsheet_BangChamCong.EndUpdate();

        }
        private void Fcn_UpDateBangThanhToanLuong()
        {
            FileHelper.fcn_spSheetStreamDocument(spsheet_BangThanhToanLuong, $@"{BaseFrom.m_templatePath}\FileExcel\12.eBangThanhToanLuong.xls"); // Bảng thanh toán lương cuối tháng
            IWorkbook workbook = spsheet_BangThanhToanLuong.Document;
            Worksheet ws = workbook.Worksheets[0];
            CellRange range = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            range.ClearContents();
            IWorkbook wb_TinhLuong = spsheet_BangTinhLuong.Document;
            Worksheet ws_TinhLuong = wb_TinhLuong.Worksheets[0];
            CellRange rangeTinhLuong = ws_TinhLuong.Range[MyConstant.TBL_QUYETDICWS];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(range);
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_KYHIEU} ";
            DataTable KyHieu = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            ws.Rows[1]["A"].SetValueFromText($"BẢNG THANH TOÁN TIỀN LƯƠNG THÁNG {de_ThangTinhLuong.Text}");
            ws.Rows[3][Name["BaoHiem"]].SetValue(double.Parse(KyHieu.AsEnumerable().FirstOrDefault()["BaoHiem"].ToString()) / 100);
            ws.Rows[3][Name["Thue"]].SetValue(double.Parse(KyHieu.AsEnumerable().FirstOrDefault()["Thue"].ToString()) / 100);
            if (range.RowCount <= rangeTinhLuong.RowCount)
                ws.Rows.Insert(range.TopRowIndex + 1, rangeTinhLuong.RowCount, RowFormatMode.FormatAsNext);
            ws.Rows[4]["A"].CopyFrom(rangeTinhLuong, PasteSpecial.All);
        }
        private void Fcn_UpdateBangTinhLuong()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            IWorkbook workbook = spsheet_BangTinhLuong.Document;
            Worksheet ws = workbook.Worksheets[0];
            CellRange range = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            range.ClearContents();
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(range);
            string dbString = $"SELECT PB.Ten as PhongBan,VITRI.Ten as ChucVu,NV.* " +
                    $"FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NV" +
                     $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_TENPHONGBAN} PB ON PB.Code=NV.PhongBan " +
                  $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} VITRI ON VITRI.Code=NV.ChucVu ";
            DataTable dtNhanVien = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string CodeNV = MyFunction.fcn_Array2listQueryCondition(dtNhanVien.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            DateTime date = de_ThangTinhLuong.DateTime;
            DateTime Min = new DateTime(date.Year, date.Month, 1);
            DateTime Max = Min.AddMonths(1).AddDays(-1);
            dbString = $"SELECT PP.*,PP.Code as CodePhuPhi FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN_CHITIETPHUPHI} PP " +
                $"WHERE PP.Month='{Min.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND PP.CodeNhanVien IN ({CodeNV}) AND PP.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'  ";
            DataTable dtPhuPhi = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_KYHIEU} ";
            DataTable KyHieu = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATHESO} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable HeSo = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string MinDay = Min.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string MaxDay = Max.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG} TU " +
            $"WHERE TU.NgayThangUng<='{MaxDay}' AND TU.NgayThangUng>='{MinDay}' AND TU.CodeNhanVien IN ({CodeNV}) " +
            $"AND TU.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            List<TamUngModel> TamUng = DataProvider.InstanceTHDA.ExecuteQueryModel<TamUngModel>(dbString);
            List<TamUngModel> TamUngNhanVien = new List<TamUngModel>();
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn lấy Ngày công nhân viên tự động không hay lấy giá trị nhập thủ công???????");
            spsheet_BangTinhLuong.BeginUpdate();
            range.ClearContents();
            if (range.RowCount - 1 <= dtNhanVien.Rows.Count)
                ws.Rows.Insert(range.BottomRowIndex - 1, dtNhanVien.Rows.Count, RowFormatMode.FormatAsPrevious);
            int stt = 1;
            double SoNgayCong = 0, SoNgayTangCa = 0;
            int RowIndex = range.TopRowIndex;
            ws.Rows[1]["A"].SetValueFromText($"BẢNG TÍNH LƯƠNG THÁNG {de_ThangTinhLuong.Text}");
            ws.Rows[3][Name["BaoHiem"]].SetValue(double.Parse(KyHieu.AsEnumerable().FirstOrDefault()["BaoHiem"].ToString()) / 100);
            ws.Rows[3][Name["Thue"]].SetValue(double.Parse(KyHieu.AsEnumerable().FirstOrDefault()["Thue"].ToString()) / 100);
            Row Copy = ws.Rows[4];
            foreach (DataRow row in dtNhanVien.Rows)
            {
                Row Crow = ws.Rows[RowIndex++];
                Crow.CopyFrom(Copy, PasteSpecial.All);
                Crow.Visible = true;
                CodeNV = row["Code"].ToString();
                Crow[Name[DanhSachNhanVienConstant.COL_Code]].SetValue(CodeNV);
                Crow[Name[DanhSachNhanVienConstant.COL_STT]].SetValue(stt++);
                Crow[Name[DanhSachNhanVienConstant.COL_MaNhanVien]].SetValue(row[DanhSachNhanVienConstant.COL_MaNhanVien]);
                Crow[Name[DanhSachNhanVienConstant.COL_TenNhanVien]].SetValue(row[DanhSachNhanVienConstant.COL_TenNhanVien]);
                Crow[Name["PhongBan"]].SetValue(row["PhongBan"]);
                Crow[Name["ChucVu"]].SetValue(row["ChucVu"]);
                Crow[Name["SoNguoiPhuThuoc"]].SetValue(row["SoNguoiPhuThuoc"]);
                DataRow[] Row = dtPhuPhi.AsEnumerable().Where(x => x["CodeNhanVien"].ToString() == CodeNV).ToArray();
                if (Row.Any())
                {
                    DataRow RowFirst = Row.FirstOrDefault();
                    foreach (DataColumn item in dtPhuPhi.Columns)
                    {
                        if (item.ColumnName == "NgayCong" || item.ColumnName == "SoNgayTangCa")
                            continue;
                        if (Name.Keys.Contains(item.ColumnName) && item.ColumnName != "Code")
                            Crow[Name[item.ColumnName]].SetValue(RowFirst[item.ColumnName]);
                    }
                }
                if (rs == DialogResult.Yes)
                {
                    //List<CaiDaGioLamChamCong> GioLamNV = GioLamMacDinh.Where(x => x.CongTruong_VanPhong == row["CongTruong_VanPhong"].ToString()).ToList();
                    KeyValuePair<double, double> NC = Fcn_TinhLuongCaNhan(row, KyHieu.AsEnumerable().SingleOrDefault(), HeSo, MinDay, MaxDay);
                    SoNgayCong = NC.Key;
                    SoNgayTangCa = NC.Value;
                }
                else
                {
                    SoNgayCong = Row.Any() ? double.Parse(Row.FirstOrDefault()["NgayCong"].ToString()) : 0;
                    SoNgayTangCa = Row.Any() ? double.Parse(Row.FirstOrDefault()["SoNgayTangCa"].ToString()) : 0;
                }

                Crow[Name["NgayCong"]].SetValue(SoNgayCong);
                Crow[Name["SoNgayTangCa"]].SetValue(SoNgayTangCa);

                if (TamUng.Any())
                {
                    TamUngNhanVien = TamUng.Where(x => x.CodeNhanVien == CodeNV).ToList();
                    if (TamUngNhanVien.Any())
                    {
                        long TT = TamUngNhanVien.Sum(x => x.SoTien);
                        Crow[Name["TamUng"]].SetValue(TT);
                    }
                }
            }
            spsheet_BangTinhLuong.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }

        private void sb_CapNhapBangTinhLuong_Click(object sender, EventArgs e)
        {
            Fcn_UpdateBangTinhLuong();
        }
        private KeyValuePair<double, double> Fcn_TinhLuongCaNhan(DataRow NV, DataRow RowKyHieu, DataTable HeSo, string Min, string Max)
        {
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} WHERE " +
                $"\"CodeNhanVien\"='{NV["Code"]}' AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'" +
                $" AND \"DateTime\"<='{Max}' AND \"DateTime\">='{Min}'";
            List<ChiTietChamCong_Gio_Ngay> GioThucTe = DataProvider.InstanceTHDA.ExecuteQueryModel<ChiTietChamCong_Gio_Ngay>(dbString);
            //if (GioThucTe.Count() == 0)
            //    return new KeyValuePair<double, double>(0, 0);
            double TongLuong = 0;
            double TongTangCa = 0;
            double HeSoSangChieuToi = 0;
            double HeSoChamCong = double.Parse(RowKyHieu["PhanTramChamCong"].ToString()) / 100;
            double HeSoTangCa = 0;
            double HeSoNghi = 0;
            DataRow HeSoNhanVien = HeSo.AsEnumerable().Where(x => x["LoaiCaiDat"].ToString() == DanhSachNhanVienConstant.LoaiNhanVien[NV["NhanVienVanPhongCongTruong"].ToString()].ToString()).SingleOrDefault();
            foreach (var item in GioThucTe)
            {
                item.ChiTietChamCong();
                if (item.Buoi != "Theo giờ")
                {
                    HeSoSangChieuToi = item.Buoi == "Sáng" ? double.Parse(HeSoNhanVien["BuoiSang"].ToString())
                        : item.Buoi == "Chiều" ? double.Parse(HeSoNhanVien["BuoiChieu"].ToString()) : double.Parse(HeSoNhanVien["BuoiToi"].ToString());
                }
                if (item.Label == 3)
                {
                    HeSoNghi = double.Parse(RowKyHieu[DanhSachNhanVienConstant.PhanTramNN[item.NghiLam]].ToString()) / 100;
                    TongLuong += HeSoNghi * HeSoSangChieuToi;
                }
                else if (item.Label == 4)
                {
                    if (item.Buoi == "Theo giờ")
                    {
                        TongTangCa += item.TongCong();
                    }
                    else
                    {
                        HeSoTangCa = double.Parse(HeSoNhanVien[item.TangCa].ToString());
                        TongTangCa += HeSoChamCong * HeSoSangChieuToi * HeSoTangCa;
                    }
                }
                else
                {
                    TongLuong += HeSoChamCong * HeSoSangChieuToi;
                }

            }
            dbString = $"SELECT NC.* FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} NC" +
            $" WHERE NC.CodeNhanVien='{NV["Code"]}' AND NC.NgayThang >='{Min}' AND NC.NgayThang <='{Max}'";
            List<CongNhatCongKhoanChiTiet> ChiTietNC = DataProvider.InstanceTHDA.ExecuteQueryModel<CongNhatCongKhoanChiTiet>(dbString);
            if (ChiTietNC.Any())
                TongLuong += (double)ChiTietNC.Sum(x => x.TongCong);
            return new KeyValuePair<double, double>(TongLuong, TongTangCa);
        }
        private void spsheet_BangTinhLuong_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            IWorkbook workbook = spsheet_BangTinhLuong.Document;
            Worksheet ws = workbook.Worksheets[0];
            CellRange range = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(range);
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN_CHITIETPHUPHI}";
            DataTable PhuPhi = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string CodePhuPhi = ws.Rows[e.RowIndex][Name[DanhSachNhanVienConstant.COL_CodePhuPhi]].Value.TextValue;
            string CodeNV = ws.Rows[e.RowIndex][Name[DanhSachNhanVienConstant.COL_Code]].Value.TextValue;
            string Col = ws.Rows[0][e.ColumnIndex].Value.TextValue;
            if (Col == "BaoHiem" || Col == "Thue")
            {
                if (e.RowIndex == 3)
                {
                    string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_KYHIEU} SET {Col}='{e.Value.NumericValue * 100}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                }
                return;
            }
            if (PhuPhi.Columns.Contains(Col))
            {
                if (CodePhuPhi is null)
                {
                    CodePhuPhi = Guid.NewGuid().ToString();
                    DateTime date = de_ThangTinhLuong.DateTime;
                    DateTime Min = new DateTime(date.Year, date.Month, 1);
                    ws.Rows[e.RowIndex][Name[DanhSachNhanVienConstant.COL_CodePhuPhi]].SetValueFromText(CodePhuPhi);
                    dbString = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN_CHITIETPHUPHI} " +
      $"(Code,{Col},CodeNhanVien,Month,CodeDuAn) VALUES " +
      $"('{CodePhuPhi}','{e.Value}','{CodeNV}','{Min.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{SharedControls.slke_ThongTinDuAn.EditValue}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
                else
                {
                    string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN_CHITIETPHUPHI} SET {Col}='{e.Value}' WHERE \"Code\"='{CodePhuPhi}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                }
            }
        }

        private void spsheet_BangTinhLuong_CellBeginEdit(object sender, SpreadsheetCellCancelEventArgs e)
        {
            IWorkbook workbook = spsheet_BangTinhLuong.Document;
            Worksheet ws = workbook.Worksheets[0];
            //string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN_CHITIETPHUPHI}";
            //DataTable PhuPhi = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //string Col = ws.Rows[0][e.ColumnIndex].Value.TextValue;
            //if (!PhuPhi.Columns.Contains(Col))
            //{
            //    if (Col == "BaoHiem" || Col == "Thue")
            //    {

            //    }
            //    else
            //        e.Cancel = true;
            //}
        }

        private void sb_TamUng_Click(object sender, EventArgs e)
        {
            Fcn_LoadTamUng();
        }
        private void Fcn_BangLuongCaNhan(DanhSachNhanVienModel NV)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            IWorkbook workbook = spsheet_TongLuongCaNhan.Document;
            Worksheet ws = workbook.Worksheets[0];
            Worksheet ws_TinhToan = workbook.Worksheets[1];
            CellRange range = ws_TinhToan.Range[MyConstant.TBL_QUYETTHONGTIN];
            CellRange Ten = ws.Range["HoTenNguoiKyNhan"];
            CellRange DiMuon = ws.Range["Range_DiMuon"];
            DiMuon.ClearContents();
            if (DiMuon.RowCount > 2)
                ws.Rows.Remove(DiMuon.BottomRowIndex, DiMuon.RowCount - 2);

            CellRange VeSom = ws.Range["Rage_VeSom"];
            VeSom.ClearContents();
            if (VeSom.RowCount > 2)
                ws.Rows.Remove(VeSom.BottomRowIndex, VeSom.RowCount - 2);

            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(range);
            DateTime date = de_ThangTinhLuongCaNhan.DateTime;
            DateTime Min = new DateTime(date.Year, date.Month, 1);
            DateTime Max = Min.AddMonths(1).AddDays(-1);
            string MinDay = Min.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string MaxDay = Max.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            spsheet_TongLuongCaNhan.BeginUpdate();
            string dbString = $"SELECT PP.*,PP.Code as CodePhuPhi FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN_CHITIETPHUPHI} PP " +
                $"WHERE PP.Month='{de_ThangTinhLuongCaNhan.Text}' AND PP.CodeNhanVien='{NV.Code}' AND PP.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable dtPhuPhi = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_KYHIEU} ";
            DataTable KyHieu = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataRow RowKyHieu = KyHieu.AsEnumerable().FirstOrDefault();
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATHESO} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable HeSo = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG} TU " +
$"WHERE TU.NgayThangUng<='{MaxDay}' AND TU.NgayThangUng>='{MinDay}' AND TU.CodeNhanVien='{NV.Code}' AND TU.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            List<TamUngModel> TamUng = DataProvider.InstanceTHDA.ExecuteQueryModel<TamUngModel>(dbString);


            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn lấy Ngày công nhân viên tự động không hay lấy giá trị nhập thủ công???????");
            bool TuDong = rs == DialogResult.Yes ? true : false;
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} WHERE " +
    $"\"CodeNhanVien\"='{NV.Code}' AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'" +
    $" AND \"DateTime\"<='{Max}' AND \"DateTime\">='{Min}'";
            List<ChiTietChamCong_Gio_Ngay> GioThucTe = DataProvider.InstanceTHDA.ExecuteQueryModel<ChiTietChamCong_Gio_Ngay>(dbString);
            if (GioThucTe.Count() == 0)
            {
                FileHelper.fcn_spSheetStreamDocument(spsheet_TongLuongCaNhan, $@"{BaseFrom.m_templatePath}\FileExcel\12.hTongLuongCaNhan.xls"); // Hiển thi tổng lương cá nhân thực lĩnh hàng tháng
                workbook = spsheet_TongLuongCaNhan.Document;
                ws = workbook.Worksheets[0];
                ws_TinhToan = workbook.Worksheets[1];
                range = ws_TinhToan.Range[MyConstant.TBL_QUYETTHONGTIN];
                Ten = ws.Range["HoTenNguoiKyNhan"];
                Name = MyFunction.fcn_getDicOfColumn(range);
            }

            ws_TinhToan.Rows[5].ClearContents();
            ws_TinhToan.Rows[5].CopyFrom(ws_TinhToan.Rows[4], PasteSpecial.All);
            ws.Rows[0]["A"].SetValueFromText($"TỔNG LƯƠNG {de_ThangTinhLuongCaNhan.Text}: {NV.TenGhep}".ToUpper());
            ws.Rows[1]["D"].SetValueFromText(NV.TenNhanVien);
            ws.Rows[Ten.TopRowIndex]["E"].SetValueFromText(NV.TenNhanVien);
            ws.Rows[2]["D"].SetValueFromText(NV.NgaySinh.HasValue ? NV.NgaySinh.Value.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET) : "");
            ws.Rows[3]["D"].SetValueFromText(NV.PhongBan);
            ws.Rows[4]["D"].SetValueFromText(NV.ChucVu);
            Row Crow = ws_TinhToan.Rows[5];
            double TongLuong = 0;
            double TongTangCa = 0;
            double HeSoSangChieuToi = 0;
            double HeSoChamCong = double.Parse(RowKyHieu["PhanTramChamCong"].ToString()) / 100;
            double HeSoTangCa = 0;
            double HeSoNghi = 0;
            double SoNgayCong = 0, SoNgayTangCa = 0;
            DataRow HeSoNhanVien = HeSo.AsEnumerable().Where(x => x["LoaiCaiDat"].ToString() == DanhSachNhanVienConstant.LoaiNhanVien[NV.NhanVienVanPhongCongTruong].ToString()).SingleOrDefault();
            ///Check ngày chấm công thực tế
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM} WHERE" +
    $" \"CongTruong_VanPhong\"='{NV.NhanVienVanPhongCongTruong}' AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            List<CaiDaGioLamChamCong> GioLamMacDinh = DataProvider.InstanceTHDA.ExecuteQueryModel<CaiDaGioLamChamCong>(dbString);
            List<string> VaoTre = new List<string>();
            List<string> RaSom = new List<string>();
            Dictionary<int, double> DayDetail = new Dictionary<int, double>();
            Dictionary<int, string> DayDetail_New = new Dictionary<int, string>();
            dbString = $"SELECT NC.* FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} NC" +
$" WHERE NC.CodeNhanVien='{NV.Code}' AND \"NgayThang\">='{Min}' AND \"MAX\"<='{Max}'";
            List<CongNhatCongKhoanChiTiet> ChiTietNC = DataProvider.InstanceTHDA.ExecuteQueryModel<CongNhatCongKhoanChiTiet>(dbString);

            foreach (var item in GioThucTe.OrderBy(x => x.DateTime))
            {
                if (item.Buoi != "Theo giờ")
                {
                    CaiDaGioLamChamCong _GioLam = GioLamMacDinh.Where(x => x.BuoiSang_Chieu_Toi ==
item.Buoi && x.NgayBatDau <= item.DateTime.Value && x.NgayKetThuc >= item.DateTime.Value).FirstOrDefault();
                    item.GioRaMacDinh = _GioLam.GioRa;
                    item.PhutRaMacDinh = _GioLam.PhutRa;
                    item.GioVaoMacDinh = _GioLam.GioVao;
                    item.PhutVaoMacDinh = _GioLam.PhutVao;
                }
                item.Fcn_CheckVaoTreRaSom();
                item.ChiTietChamCong();

                if (item.VaoTre != "")
                    VaoTre.Add(item.VaoTre);
                else if (item.RaSom != "")
                    RaSom.Add(item.RaSom);
                if (TuDong)
                {
                    if (item.Buoi != "Theo giờ")
                        HeSoSangChieuToi = item.Buoi == "Sáng" ? double.Parse(HeSoNhanVien["BuoiSang"].ToString())
                     : item.Buoi == "Chiều" ? double.Parse(HeSoNhanVien["BuoiChieu"].ToString()) : double.Parse(HeSoNhanVien["BuoiToi"].ToString());
                    if (item.Label == 3)
                    {
                        HeSoNghi = double.Parse(RowKyHieu[DanhSachNhanVienConstant.PhanTramNN[item.NghiLam]].ToString()) / 100;
                        TongLuong += HeSoNghi * HeSoSangChieuToi;
                        if (DayDetail.Keys.Contains(item.DateTime.Value.Day))
                        {
                            DayDetail[item.DateTime.Value.Day] += HeSoNghi * HeSoSangChieuToi;
                        }
                        else
                            DayDetail.Add(item.DateTime.Value.Day, HeSoNghi * HeSoSangChieuToi);

                        if (DayDetail_New.Keys.Contains(item.DateTime.Value.Day))
                        {
                            DayDetail_New[item.DateTime.Value.Day] += $"+{HeSoNghi * HeSoSangChieuToi}{RowKyHieu[item.NghiLam]}";
                        }
                        else
                            DayDetail_New.Add(item.DateTime.Value.Day, $"{HeSoNghi * HeSoSangChieuToi}{RowKyHieu[item.NghiLam]}");
                    }
                    else if (item.Label == 4)
                    {
                        if (item.Buoi != "Theo giờ")
                        {
                            double HsTc = item.TongCong();
                            TongTangCa += HsTc;
                            if (DayDetail.Keys.Contains(item.DateTime.Value.Day))
                            {
                                DayDetail[item.DateTime.Value.Day] += HsTc;
                            }
                            else
                                DayDetail.Add(item.DateTime.Value.Day, HsTc);

                            if (DayDetail_New.Keys.Contains(item.DateTime.Value.Day))
                            {
                                DayDetail_New[item.DateTime.Value.Day] = $"{DayDetail[item.DateTime.Value.Day]}{RowKyHieu["ChamCong"]}";
                            }
                            else
                                DayDetail_New.Add(item.DateTime.Value.Day, $"{HsTc}{RowKyHieu["ChamCong"]}");
                        }
                        else
                        {
                            HeSoTangCa = double.Parse(HeSoNhanVien[item.TangCa].ToString());
                            TongTangCa += HeSoChamCong * HeSoSangChieuToi * HeSoTangCa;
                            if (DayDetail.Keys.Contains(item.DateTime.Value.Day))
                            {
                                DayDetail[item.DateTime.Value.Day] += HeSoChamCong * HeSoSangChieuToi * HeSoTangCa;
                            }
                            else
                                DayDetail.Add(item.DateTime.Value.Day, HeSoChamCong * HeSoSangChieuToi * HeSoTangCa);

                            if (DayDetail_New.Keys.Contains(item.DateTime.Value.Day))
                            {
                                DayDetail_New[item.DateTime.Value.Day] = $"{DayDetail[item.DateTime.Value.Day]}{RowKyHieu["ChamCong"]}";
                            }
                            else
                                DayDetail_New.Add(item.DateTime.Value.Day, $"{HeSoChamCong * HeSoSangChieuToi * HeSoTangCa}{RowKyHieu["ChamCong"]}");
                        }
                    }
                    else
                    {
                        if (DayDetail.Keys.Contains(item.DateTime.Value.Day))
                        {
                            DayDetail[item.DateTime.Value.Day] += HeSoChamCong * HeSoSangChieuToi;
                        }
                        else
                            DayDetail.Add(item.DateTime.Value.Day, HeSoChamCong * HeSoSangChieuToi);

                        if (DayDetail_New.Keys.Contains(item.DateTime.Value.Day))
                        {
                            DayDetail_New[item.DateTime.Value.Day] = $"{DayDetail[item.DateTime.Value.Day]}{RowKyHieu["ChamCong"]}";
                        }
                        else
                            DayDetail_New.Add(item.DateTime.Value.Day, $"{HeSoChamCong * HeSoSangChieuToi}{RowKyHieu["ChamCong"]}");

                        TongLuong += HeSoChamCong * HeSoSangChieuToi;
                    }
                }
            }
            var grChiTietNC = ChiTietNC.GroupBy(x => x.NgayThang);
            foreach (var CrItem in grChiTietNC)
            {
                double TongCong = (double)CrItem.Sum(x => x.TongCong);
                if (DayDetail.Keys.Contains(CrItem.Key.Value.Day))
                    DayDetail[CrItem.Key.Value.Day] += TongCong;
                else
                    DayDetail[CrItem.Key.Value.Day] = TongCong;

                if (DayDetail_New.Keys.Contains(CrItem.Key.Value.Day))
                    DayDetail_New[CrItem.Key.Value.Day] += $"+={TongCong}CN-CK";
                else
                    DayDetail_New[CrItem.Key.Value.Day] = $"={TongCong}CN-CK";

            }
            if (DayDetail.Any())
            {
                CellRange ChamCong = ws.Range["Range_ChamCongCaNhan"];
                for (int i = ChamCong.TopRowIndex; i <= ChamCong.BottomRowIndex; i++)
                {
                    Row CrowChamCong = ws.Rows[i];
                    if (i % 2 == 0)
                    {
                        for (int j = ChamCong.LeftColumnIndex; j <= ChamCong.RightColumnIndex; j++)
                        {
                            int Ngay = (int)CrowChamCong[j].Value.NumericValue;
                            if (Ngay == 0)
                                continue;
                            if (DayDetail.Keys.Contains(Ngay))
                                ws.Rows[i + 1][j].SetValue(DayDetail[Ngay]);
                            else
                                ws.Rows[i + 1][j].SetValue(0);
                        }
                    }
                    else
                        continue;
                }

            }
            if (DayDetail_New.Any())
            {
                CellRange ChamCong = ws.Range["Range_CachChamCong"];
                for (int i = ChamCong.TopRowIndex; i <= ChamCong.BottomRowIndex; i++)
                {
                    Row CrowChamCong = ws.Rows[i];
                    if (i % 2 == 0)
                    {
                        for (int j = ChamCong.LeftColumnIndex; j <= ChamCong.RightColumnIndex; j++)
                        {
                            int Ngay = (int)CrowChamCong[j].Value.NumericValue;
                            if (Ngay == 0)
                                continue;
                            if (DayDetail_New.Keys.Contains(Ngay))
                                ws.Rows[i + 1][j].SetValue(DayDetail_New[Ngay]);
                            else
                                ws.Rows[i + 1][j].SetValue(0);
                        }
                    }
                    else
                        continue;
                }

            }
            Row RowCopy = ws.Rows[11];
            if (VaoTre.Any())
            {
                int RowVaoTre = DiMuon.TopRowIndex + 1;
                if (VaoTre.Count() > 1)
                    ws.Rows.Insert(DiMuon.BottomRowIndex, VaoTre.Count() - 1, RowFormatMode.FormatAsNext);

                foreach (var itemVaoTre in VaoTre)
                {
                    Row CrowVaoTre = ws.Rows[RowVaoTre++];
                    CrowVaoTre.CopyFrom(RowCopy, PasteSpecial.All);
                    CrowVaoTre.Visible = true;
                    CrowVaoTre["B"].SetValueFromText(itemVaoTre);
                }

            }
            if (RaSom.Any())
            {
                int RowRaSom = VeSom.TopRowIndex + 1;
                if (RaSom.Count() > 1)
                    ws.Rows.Insert(VeSom.BottomRowIndex, RaSom.Count() - 1, RowFormatMode.FormatAsNext);

                foreach (var itemVeSom in RaSom)
                {
                    Row CrowVeSom = ws.Rows[RowRaSom++];
                    CrowVeSom.CopyFrom(RowCopy, PasteSpecial.All);
                    CrowVeSom.Visible = true;
                    CrowVeSom["B"].SetValueFromText(itemVeSom);
                }

            }
            if (TuDong)
            {
                Crow[Name["NgayCong"]].SetValue(TongLuong);
                Crow[Name["SoNgayTangCa"]].SetValue(TongTangCa);
            }

            /////tính toán hệ số mặc định
            ws_TinhToan.Rows[3][Name["BaoHiem"]].SetValue(double.Parse(RowKyHieu["BaoHiem"].ToString()) / 100);
            ws_TinhToan.Rows[3][Name["Thue"]].SetValue(double.Parse(RowKyHieu["Thue"].ToString()) / 100);
            long TT = TamUng.Any() ? TamUng.Sum(x => x.SoTien) : 0;
            Crow[Name["TamUng"]].SetValue(TT);
            if (dtPhuPhi.Rows.Count != 0)
            {
                DataRow RowFirst = dtPhuPhi.AsEnumerable().FirstOrDefault();
                if (!TuDong)
                {
                    SoNgayCong = double.Parse(RowFirst["NgayCong"].ToString());
                    SoNgayTangCa = double.Parse(RowFirst["SoNgayTangCa"].ToString());
                    Crow[Name["NgayCong"]].SetValue(SoNgayCong);
                    Crow[Name["SoNgayTangCa"]].SetValue(SoNgayTangCa);
                }
                foreach (DataColumn item in dtPhuPhi.Columns)
                {
                    if (item.ColumnName == "NgayCong" || item.ColumnName == "SoNgayTangCa")
                        continue;
                    if (Name.Keys.Contains(item.ColumnName) && item.ColumnName != "Code")
                        Crow[Name[item.ColumnName]].SetValue(RowFirst[item.ColumnName]);
                }
            }
            //////Tính toán bảng cụ thể 31 ngày
            ///

            spsheet_TongLuongCaNhan.EndUpdate();
            WaitFormHelper.CloseWaitForm();

        }
        private void sb_CapNhapTinhLuong_Click(object sender, EventArgs e)
        {
            DanhSachNhanVienModel NV = slue_TenNhanVien.GetSelectedDataRow() as DanhSachNhanVienModel;
            if (NV is null)
                return;
            Fcn_BangLuongCaNhan(NV);
        }
        private void InitDataControl()
        {
            date_NhapLieu.Properties.MaxValue = DateTime.Now;
            date_NhapLieu.EditValue = DateTime.Now;
        }
        private void Fcn_LoadDaTaCongNhatCongKhoan(SpreadsheetControl spreadsheet, bool isNgoaiKeHoach)
        {
            FileHelper.fcn_spSheetStreamDocument(spsheet_BangCongNhatCongKhoan, $@"{BaseFrom.m_templatePath}\FileExcel\12.m CongNhatCongKhoan.xlsx"); // Hiển thi tổng lương cá nhân thực lĩnh hàng tháng
            IWorkbook workbook = spreadsheet.Document;
            Worksheet sheet = workbook.Worksheets[0];
            CellRange RangeData = sheet.Range["Data"];
            CellRange titleHeader = workbook.Range[MyConstant.TITLE_HEADER];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(titleHeader);
            spreadsheet.Document.History.IsEnabled = false;
            DonViThucHien DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;
            spreadsheet.BeginUpdate();
            WaitFormHelper.ShowWaitForm("Đang tải danh sách công tác");
            string condition = isNgoaiKeHoach ? "" : $"AND cttk.NgayBatDau<='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND " +
                $"cttk.NgayKetThuc>='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            string dbString = $"SELECT cttk.Code AS CodeCongTacTheoGiaiDoan,cttk.KhoiLuongToanBo,1 AS TypeRow," +
                 $" hm.Code as CodeHangMuc,hm.Ten as TenHangMuc,ctrinh.Code as CodeCongTrinh,ctrinh.Ten as TenCongTrinh," +
                 $" dmct.TenCongTac, dmct.MaHieuCongTac,dmct.DonVi, \r\n" +
                 $"ctrinh.CodeDuAn \r\n" +
                 $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                 $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                 $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                 $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                 $"ON dmct.CodeHangMuc = hm.Code \r\n" +
                 $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                 $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
                 $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                 $"ON ctrinh.CodeDuAn = da.Code \r\n" +
                 $"WHERE da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' AND cttk.{DVTH.ColCodeFK}='{DVTH.Code}' \r\n" +
                 $"{condition} " +
                 $"ORDER BY ctrinh.SortId ASC, hm.SortId ASC, cttk.SortId ASC\r\n";
            List<CongNhatCongKhoanChiTiet> dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQueryModel<CongNhatCongKhoanChiTiet>(dbString);
            string lstCode = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.Select(x => x.CodeCongTacTheoGiaiDoan).ToArray());
            dbString = $"SELECT NV.TenNhanVien,NV.MaNhanVien,NC.* FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} NC" +
                $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NV ON NV.Code=NC.CodeNhanVien WHERE NC.CodeCongTacTheoGiaiDoan IN ({lstCode})  AND NC.NgayThang='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            List<CongNhatCongKhoanChiTiet> ChiTietNC = DataProvider.InstanceTHDA.ExecuteQueryModel<CongNhatCongKhoanChiTiet>(dbString);
            string currentDate = DateTime.Parse(date_NhapLieu.EditValue.ToString()).ToString("yyyy-MM-dd");
            if (RangeData.RowCount - 1 <= dtCongTacTheoKy.Count())
                sheet.Rows.Insert(RangeData.TopRowIndex + 1, dtCongTacTheoKy.Count(), RowFormatMode.FormatAsNext);
            RangeData = sheet.Range["Data"];
            var GrCongTrinh = dtCongTacTheoKy.GroupBy(x => new { x.CodeCongTrinh, x.TenCongTrinh });
            int RowIndex = RangeData.TopRowIndex;
            int STT = 1;
            List<CongNhatCongKhoanChiTiet> ChiTiet = new List<CongNhatCongKhoanChiTiet>();
            int RowNV = 0;
            CellRange Merge;
            string[] LstMerge = { MyConstant.STT, TDKH.COL_MaHieuCongTac, TDKH.COL_DanhMucCongTac, TDKH.COL_DonVi, "TenHangMuc" };
            KeyValuePair<int, int> IndexMerge;
            foreach (var Ctrinh in GrCongTrinh)
            {
                Row crRowWs = sheet.Rows[RowIndex++];
                crRowWs.Font.Bold = true;
                crRowWs.Font.Color = Color.DarkTurquoise;
                crRowWs[Name[TDKH.COL_Code]].SetValue(Ctrinh.Key.CodeCongTrinh);
                crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(MyConstant.CONST_TYPE_CONGTRINH);
                crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(Ctrinh.Key.TenCongTrinh);
                sheet.Rows.Insert(RowIndex + 1, 1, RowFormatMode.FormatAsNext);
                var grHangMuc = Ctrinh.GroupBy(x => new { x.CodeHangMuc, x.TenHangMuc });
                foreach (var HM in grHangMuc)
                {
                    crRowWs = sheet.Rows[RowIndex++];
                    sheet.Rows.Insert(RowIndex + 1, 1, RowFormatMode.FormatAsNext);
                    crRowWs.Font.Bold = true;
                    crRowWs.Font.Color = Color.DarkGreen;
                    crRowWs[Name[TDKH.COL_Code]].SetValue(HM.Key.CodeHangMuc);
                    crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(MyConstant.CONST_TYPE_HANGMUC);
                    crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(HM.Key.TenHangMuc);

                    var grCongTac = HM.GroupBy(x => x.CodeCongTacTheoGiaiDoan);
                    foreach (var CongTac in grCongTac)
                    {
                        IndexMerge = new KeyValuePair<int, int>();
                        var FirstCT = CongTac.FirstOrDefault();

                        crRowWs = sheet.Rows[RowIndex++];
                        sheet.Rows.Insert(RowIndex + 1, 1, RowFormatMode.FormatAsNext);
                        crRowWs.Visible = true;
                        crRowWs.Font.Bold = false;
                        crRowWs.Font.Color = Color.Black;
                        crRowWs[Name["CodeCongTacTheoGiaiDoan"]].SetValue(CongTac.Key);
                        crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(FirstCT.MaHieuCongTac);
                        crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(FirstCT.TenCongTac);
                        crRowWs[Name[TDKH.COL_TypeRow]].SetValue(FirstCT.TypeRow);
                        crRowWs[Name[TDKH.COL_STT]].SetValue(STT++);
                        crRowWs[Name["TenHangMuc"]].SetValue(HM.Key.TenHangMuc);
                        crRowWs[Name[TDKH.COL_DonVi]].SetValue(FirstCT.DonVi);
                        if (ChiTietNC.Any())
                        {
                            ChiTiet = ChiTietNC.Where(x => x.CodeCongTacTheoGiaiDoan == CongTac.Key).ToList();
                            if (ChiTiet.Any())
                            {
                                RangeData = sheet.Range["Data"];
                                RowNV = RowIndex - 1;
                                IndexMerge = new KeyValuePair<int, int>(RowNV + 1, RowNV + ChiTiet.Count());
                                sheet.Rows.Insert(RangeData.BottomRowIndex, ChiTiet.Count(), RowFormatMode.FormatAsPrevious);
                                foreach (var itemNV in ChiTiet)
                                {
                                    Row crRowWsnv = sheet.Rows[RowNV++];
                                    //if(RowNV-1> RowIndex - 1)
                                    //{
                                    //    crRowWsnv.CopyFrom(Copy, PasteSpecial.All);
                                    //    crRowWsnv.Visible = true;
                                    //}
                                    crRowWsnv[Name[MyConstant.TONGGIO]].Formula = $"{crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].GetReferenceA1()}" +
    $"+{crRowWsnv[Name[MyConstant.TONGGIOSANG]].GetReferenceA1()}+{crRowWsnv[Name[MyConstant.TONGGIOTOI]].GetReferenceA1()}";
                                    crRowWsnv.Font.Color = Color.Black;
                                    crRowWsnv[Name[MyConstant.TONGCONG]].Formula = $"{crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].GetReferenceA1()}*{crRowWsnv[Name[MyConstant.HESOCHIEU]].GetReferenceA1()}" +
    $"+{crRowWsnv[Name[MyConstant.TONGGIOSANG]].GetReferenceA1()}*{crRowWsnv[Name[MyConstant.HESOSANG]].GetReferenceA1()}+{crRowWsnv[Name[MyConstant.TONGGIOTOI]].GetReferenceA1()}*{crRowWsnv[Name[MyConstant.HESOTOI]].GetReferenceA1()}";
                                    crRowWsnv.Font.Color = Color.Black;
                                    crRowWsnv[Name[DanhSachNhanVienConstant.COL_TenNhanVien]].SetValue(itemNV.TenNhanVien);
                                    crRowWsnv[Name[DanhSachNhanVienConstant.COL_MaNhanVien]].SetValue(itemNV.MaNhanVien);
                                    crRowWsnv[Name[MyConstant.GIOBATDAUSANG]].Value = itemNV.GioBatDauSang;
                                    crRowWsnv[Name[MyConstant.GIOKETTHUCSANG]].Value = itemNV.GioKetThucSang;
                                    crRowWsnv[Name[MyConstant.GIOBATDAUCHIEU]].Value = itemNV.GioBatDauChieu;
                                    crRowWsnv[Name[MyConstant.GIOKETTHUCCHIEU]].Value = itemNV.GioKetThucChieu;
                                    crRowWsnv[Name[MyConstant.GIOBATDAUTOI]].Value = itemNV.GioBatDauToi;
                                    crRowWsnv[Name[MyConstant.GIOKETTHUCTOI]].Value = itemNV.GioKetThucToi;
                                    crRowWsnv[Name[MyConstant.HESOSANG]].Value = itemNV.HeSoSang;
                                    crRowWsnv[Name[MyConstant.HESOCHIEU]].Value = itemNV.HeSoChieu;
                                    crRowWsnv[Name[MyConstant.HESOTOI]].Value = itemNV.HeSoToi;
                                    crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].Value = itemNV.TongGioChieu;
                                    crRowWsnv[Name[MyConstant.TONGGIOSANG]].Value = itemNV.TongGioSang;
                                    crRowWsnv[Name[MyConstant.TONGGIOTOI]].Value = itemNV.TongGioToi;
                                    crRowWsnv[Name[MyConstant.NGAYTHANG]].SetValueFromText(currentDate);
                                    crRowWsnv[Name[MyConstant.GHICHU]].Value = itemNV.GhiChu;
                                    crRowWsnv[Name[MyConstant.CODE]].Value = itemNV.Code;
                                    crRowWsnv[Name[MyConstant.CODECONGTACTheoGiaiDoan]].Value = itemNV.CodeCongTacTheoGiaiDoan;
                                    crRowWsnv[Name[MyConstant.CODECONGTACTHUCONG]].Value = itemNV.CodeCongTacThuCong;
                                    crRowWsnv[Name[TDKH.COL_TypeRow]].SetValue(1);
                                }
                                RowIndex = RowIndex + ChiTiet.Count() - 1;

                            }
                        }
                        if (IndexMerge.Key != 0)
                        {
                            foreach (var itemmerge in LstMerge)
                            {
                                string ColHeading = Name[itemmerge];
                                CellRange RangeMerge = sheet.Range[$"{ColHeading}{IndexMerge.Key}:{ColHeading}{IndexMerge.Value}"];
                                sheet.MergeCells(RangeMerge);
                            }
                        }
                    }
                }

            }
            if (isNgoaiKeHoach)
            {
                dbString = $"SELECT cttk.* FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN} cttk " +
                    $"WHERE {DVTH.ColCodeFK}='{DVTH.Code}' AND \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
                List<CongNhatCongKhoanChiTiet> ThuCong = DataProvider.InstanceTHDA.ExecuteQueryModel<CongNhatCongKhoanChiTiet>(dbString);
                lstCode = MyFunction.fcn_Array2listQueryCondition(ThuCong.Select(x => x.Code).ToArray());
                dbString = $"SELECT NV.TenNhanVien,NV.MaNhanVien,NC.* FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} NC" +
               $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NV ON NV.Code=NC.CodeNhanVien WHERE NC.CodeCongTacThuCong IN ({lstCode}) AND NC.NgayThang='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
                ChiTietNC = DataProvider.InstanceTHDA.ExecuteQueryModel<CongNhatCongKhoanChiTiet>(dbString);
                foreach (var item in ThuCong)
                {
                    IndexMerge = new KeyValuePair<int, int>();
                    Row crRowWs = sheet.Rows[RowIndex++];
                    sheet.Rows.Insert(RowIndex + 1, 1, RowFormatMode.FormatAsNext);
                    crRowWs.Visible = true;
                    crRowWs.Font.Bold = false;
                    crRowWs.Font.Color = Color.Black;
                    crRowWs[Name["CodeCongTacThuCong"]].SetValue(item.Code);
                    crRowWs[Name[TDKH.COL_MaHieuCongTac]].SetValue(item.MaHieuCongTac);
                    crRowWs[Name[TDKH.COL_DanhMucCongTac]].SetValue(item.TenCongTac);
                    crRowWs[Name[TDKH.COL_DonVi]].SetValue(item.DonVi);
                    crRowWs[Name[TDKH.COL_TypeRow]].SetValue(item.TypeRow);
                    crRowWs[Name[TDKH.COL_STT]].SetValue(STT++);
                    crRowWs[Name["TenHangMuc"]].SetValue(item.TenHangMuc);
                    if (ChiTietNC.Any())
                    {
                        ChiTiet = ChiTietNC.Where(x => x.CodeCongTacThuCong == item.Code).ToList();
                        if (ChiTiet.Any())
                        {
                            RangeData = sheet.Range["Data"];
                            RowNV = RowIndex - 1;
                            IndexMerge = new KeyValuePair<int, int>(RowNV + 1, RowNV + ChiTiet.Count());
                            sheet.Rows.Insert(RangeData.BottomRowIndex, ChiTiet.Count(), RowFormatMode.FormatAsPrevious);
                            foreach (var itemNV in ChiTiet)
                            {
                                Row crRowWsnv = sheet.Rows[RowNV++];
                                //if(RowNV-1> RowIndex - 1)
                                //{
                                //    crRowWsnv.CopyFrom(Copy, PasteSpecial.All);
                                //    crRowWsnv.Visible = true;
                                //}
                                crRowWsnv[Name[MyConstant.TONGGIO]].Formula = $"{crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].GetReferenceA1()}" +
$"+{crRowWsnv[Name[MyConstant.TONGGIOSANG]].GetReferenceA1()}+{crRowWsnv[Name[MyConstant.TONGGIOTOI]].GetReferenceA1()}";
                                crRowWsnv.Font.Color = Color.Black;
                                crRowWsnv[Name[MyConstant.TONGCONG]].Formula = $"{crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].GetReferenceA1()}*{crRowWsnv[Name[MyConstant.HESOCHIEU]].GetReferenceA1()}" +
$"+{crRowWsnv[Name[MyConstant.TONGGIOSANG]].GetReferenceA1()}*{crRowWsnv[Name[MyConstant.HESOSANG]].GetReferenceA1()}+{crRowWsnv[Name[MyConstant.TONGGIOTOI]].GetReferenceA1()}*{crRowWsnv[Name[MyConstant.HESOTOI]].GetReferenceA1()}";
                                crRowWsnv.Font.Color = Color.Black;
                                crRowWsnv[Name[DanhSachNhanVienConstant.COL_TenNhanVien]].SetValue(itemNV.TenNhanVien);
                                crRowWsnv[Name[DanhSachNhanVienConstant.COL_MaNhanVien]].SetValue(itemNV.MaNhanVien);
                                crRowWsnv[Name[MyConstant.GIOBATDAUSANG]].Value = itemNV.GioBatDauSang;
                                crRowWsnv[Name[MyConstant.GIOKETTHUCSANG]].Value = itemNV.GioKetThucSang;
                                crRowWsnv[Name[MyConstant.GIOBATDAUCHIEU]].Value = itemNV.GioBatDauChieu;
                                crRowWsnv[Name[MyConstant.GIOKETTHUCCHIEU]].Value = itemNV.GioKetThucChieu;
                                crRowWsnv[Name[MyConstant.GIOBATDAUTOI]].Value = itemNV.GioBatDauToi;
                                crRowWsnv[Name[MyConstant.GIOKETTHUCTOI]].Value = itemNV.GioKetThucToi;
                                crRowWsnv[Name[MyConstant.HESOSANG]].Value = itemNV.HeSoSang;
                                crRowWsnv[Name[MyConstant.HESOCHIEU]].Value = itemNV.HeSoChieu;
                                crRowWsnv[Name[MyConstant.HESOTOI]].Value = itemNV.HeSoToi;
                                crRowWsnv[Name[MyConstant.TONGGIOCHIEU]].Value = itemNV.TongGioChieu;
                                crRowWsnv[Name[MyConstant.TONGGIOSANG]].Value = itemNV.TongGioSang;
                                crRowWsnv[Name[MyConstant.TONGGIOTOI]].Value = itemNV.TongGioToi;
                                crRowWsnv[Name[MyConstant.NGAYTHANG]].SetValueFromText(currentDate);
                                crRowWsnv[Name[MyConstant.GHICHU]].Value = itemNV.GhiChu;
                                crRowWsnv[Name[MyConstant.CODE]].Value = itemNV.Code;
                                crRowWsnv[Name[TDKH.COL_TypeRow]].SetValue(1);
                            }
                            RowIndex = RowIndex + ChiTiet.Count() - 1;

                        }
                    }
                    if (IndexMerge.Key != 0)
                    {
                        foreach (var itemmerge in LstMerge)
                        {
                            string ColHeading = Name[itemmerge];
                            CellRange RangeMerge = sheet.Range[$"{ColHeading}{IndexMerge.Key}:{ColHeading}{IndexMerge.Value}"];
                            sheet.MergeCells(RangeMerge);
                        }
                    }
                }

            }
            spreadsheet.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }

        private void spsheet_BangCongNhatCongKhoan_CellBeginEdit(object sender, SpreadsheetCellCancelEventArgs e)
        {
            Worksheet worksheet = spsheet_BangCongNhatCongKhoan.ActiveWorksheet;
            CellRange titleHeader = worksheet.Range[MyConstant.TITLE_HEADER];
            Dictionary<string, int> lstTitles = ExcelHelper.GetListColumnByRange(titleHeader);
            string columnName = lstTitles.Where(x => x.Value == e.ColumnIndex)?.First().Key;
            string Code = worksheet.Rows[0][lstTitles["Code"]].Value.TextValue;
            switch (columnName)
            {
                case MyConstant.STT:
                case "NgayThang":
                case "TenNhanVien":
                case "MaNhanVien":
                case "MaHieuCongTac":
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
            //if (string.IsNullOrEmpty(Code))
            //{
            //    MessageShower.ShowError("Vui lòng chọn tên nhân viên trước khi nhập dữ liệu!!!!!!");
            //    e.Cancel = true;
            //}

        }

        private void spsheet_BangCongNhatCongKhoan_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            Worksheet sheet = e.Worksheet;
            CellRange titleHeader = sheet.Range[MyConstant.TITLE_HEADER];
            CellRange body = sheet.Range["Data"];
            Dictionary<string, int> lstTitles = ExcelHelper.GetListColumnByRange(titleHeader);
            string columnName = lstTitles.Where(x => x.Value == e.ColumnIndex)?.First().Key;
            int typeRow = sheet.Rows[e.RowIndex][lstTitles[MyConstant.TYPEROW]].Value.IsEmpty ? 2 : (int)sheet.Rows[e.RowIndex][lstTitles[MyConstant.TYPEROW]].Value.NumericValue;
            string code = sheet.Rows[e.RowIndex][lstTitles[MyConstant.CODE]].Value.TextValue;
            string codeCongTacThuCong = sheet.Rows[e.RowIndex][lstTitles[MyConstant.CODECONGTACTHUCONG]].Value.TextValue;
            string[] LstMerge = { TDKH.COL_MaHieuCongTac, TDKH.COL_DanhMucCongTac, TDKH.COL_DonVi, "TenHangMuc" };
            if (typeRow == 2 && LstMerge.Contains(columnName))
            {
                if (!cbb_LayCongTacNgoaiKeHoach.Checked)
                {
                    MessageShower.ShowWarning("Nhập thủ công chỉ áp dụng cho công tác ngoài kế hoạch, Vui lòng chọn Công tác ngoài kế hoạch!!!");
                    spsheet_BangCongNhatCongKhoan.CloseCellEditor(CellEditorEnterValueMode.Cancel);
                    sheet.Rows[e.RowIndex][e.ColumnIndex].SetValue("");
                    return;
                }
                if (string.IsNullOrEmpty(codeCongTacThuCong))
                {
                    if (columnName != "TenCongTac")
                    {
                        MessageShower.ShowError("Vui lòng nhập tên công tác trước khi nhập thông tin các cột khác!!!!!");
                        spsheet_BangCongNhatCongKhoan.CloseCellEditor(CellEditorEnterValueMode.Cancel);
                        sheet.Rows[e.RowIndex][e.ColumnIndex].SetValue("");
                        return;
                    }
                    DonViThucHien DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;
                    codeCongTacThuCong = Guid.NewGuid().ToString();
                    string dbString = $"INSERT INTO {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN} " +
$"(Code,TenCongTac,CodeGiaiDoan,{DVTH.ColCodeFK},TypeRow) VALUES " +
$"('{codeCongTacThuCong}','{e.Value}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','{DVTH.Code}','{2}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    sheet.Rows[e.RowIndex][lstTitles[MyConstant.CODECONGTACTHUCONG]].Value = codeCongTacThuCong;
                    sheet.Rows[e.RowIndex][lstTitles[MyConstant.TYPEROW]].Value = 2;
                    //sheet.Rows[e.RowIndex][lstTitles[MyConstant.STT]].Value = e.RowIndex-2;
                    sheet.Rows[e.RowIndex][lstTitles[MyConstant.NGAYTHANG]].SetValueFromText(date_NhapLieu.DateTime.ToString("yyyy-MM-dd"));
                }
                else
                {
                    string updatestt = $"UPDATE  {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN} SET {columnName}='{e.Value}' WHERE \"Code\"='{codeCongTacThuCong}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
                }
            }
        }

        private void cbb_LayCongTacNgoaiKeHoach_CheckedChanged(object sender, EventArgs e)
        {
            Fcn_LoadDaTaCongNhatCongKhoan(spsheet_BangCongNhatCongKhoan, cbb_LayCongTacNgoaiKeHoach.Checked);
        }

        private void date_NhapLieu_EditValueChanged(object sender, EventArgs e)
        {
            Fcn_LoadDaTaCongNhatCongKhoan(spsheet_BangCongNhatCongKhoan, cbb_LayCongTacNgoaiKeHoach.Checked);
        }

        private void spsheet_BangCongNhatCongKhoan_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            e.Menu.RemoveMenuItem(SpreadsheetCommandId.RemoveSheetRowsContextMenuItem);
            e.Menu.RemoveMenuItem(SpreadsheetCommandId.RemoveSheetRows);
            IWorkbook wb = spsheet_BangCongNhatCongKhoan.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            CellRange Cell = ws.SelectedCell;
            if (!RangeData.Contains(Cell) || Cell.RowCount > 1)
                return;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            SpreadsheetMenuItem itemXoa = new SpreadsheetMenuItem("Xóa nhân công!", fcn_Handle_XoaMay);
            itemXoa.Appearance.ForeColor = Color.Blue;
            itemXoa.Appearance.Font = new Font(itemXoa.Appearance.Font, FontStyle.Bold);
            e.Menu.Items.Add(itemXoa);
            if (Name[TDKH.COL_DanhMucCongTac] == ws.Columns[Cell.LeftColumnIndex].Heading)
            {
                SpreadsheetMenuItem itemCapNhatAll = new SpreadsheetMenuItem("Thêm nhân viên!", fcn_Handle_ThemNV);
                itemCapNhatAll.Appearance.ForeColor = Color.Blue;
                itemCapNhatAll.Appearance.Font = new Font(itemCapNhatAll.Appearance.Font, FontStyle.Bold);
                e.Menu.Items.Add(itemCapNhatAll);
            }
            SpreadsheetMenuItem itemThemDong = new SpreadsheetMenuItem("Thêm dòng công tác thủ công!", fcn_Handle_ThemDong);
            itemThemDong.Appearance.ForeColor = Color.Blue;
            itemThemDong.Appearance.Font = new Font(itemThemDong.Appearance.Font, FontStyle.Bold);
            e.Menu.Items.Add(itemThemDong);
        }
        private void fcn_Handle_XoaMay(object sender, EventArgs args)
        {
            IWorkbook wb = spsheet_BangCongNhatCongKhoan.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Cell = ws.SelectedCell;
            string CodeMay = ws.Rows[Cell.TopRowIndex][Name["Code"]].Value.TextValue;
            if (CodeMay is null)
            {
                MessageShower.ShowWarning("Vui lòng Chọn đúng dòng có nhân viên để xóa!!!!!!");
                return;
            }
            string Code = ws.Rows[Cell.TopRowIndex][Name["Code"]].Value.TextValue;
            string db_string = $"DELETE FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} WHERE \"Code\"='{Code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            Fcn_LoadDaTaCongNhatCongKhoan(spsheet_BangCongNhatCongKhoan, cbb_LayCongTacNgoaiKeHoach.Checked);
        }
        private void fcn_Handle_ThemDong(object sender, EventArgs args)
        {
            Fcn_LoadDaTaCongNhatCongKhoan(spsheet_BangCongNhatCongKhoan, cbb_LayCongTacNgoaiKeHoach.Checked);
        }
        private void fcn_Handle_ThemNV(object sender, EventArgs args)
        {
            IWorkbook wb = spsheet_BangCongNhatCongKhoan.Document;
            Worksheet ws = wb.Worksheets.ActiveWorksheet;
            CellRange RangeData = ws.Range["Data"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeData);
            CellRange Cell = ws.SelectedCell;
            double TypeRow = ws.Rows[Cell.TopRowIndex][Name[TDKH.COL_TypeRow]].Value.NumericValue;
            if (TypeRow == 0)
            {
                MessageShower.ShowWarning("Vui lòng chọn ô có tên Công tác");
                return;
            }
            string ColCode = ws.Rows[Cell.TopRowIndex][Name[TDKH.COL_TypeRow]].Value.NumericValue == 1 ? "CodeCongTacTheoGiaiDoan" : "CodeCongTacThuCong";
            string Code = ws.Rows[Cell.TopRowIndex][Name[ColCode]].Value.TextValue;
            //string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} " +
            //    $"WHERE {ColCode}='{Code}' AND \"NgayThang\"='{date_NhapLieu.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            //List<CongNhatCongKhoanChiTiet> ChiTiet = DataProvider.InstanceTHDA.ExecuteQueryModel<CongNhatCongKhoanChiTiet>(dbString);
            //string LstCode = MyFunction.fcn_Array2listQueryCondition(ChiTiet.Select(x => x.CodeNhanVien).ToArray());
            string dbString = $"SELECT {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.*,{DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}.Ten as ChucVu " +
                            $"FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}" +
                            $" LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} " +
                            $"ON {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.ChucVu={DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN}.Code ";
            //$"WHERE {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.Code NOT IN ({LstCode}) ";
            List<DanhSachNhanVienModel> DanhSachNV = DataProvider.InstanceTHDA.ExecuteQueryModel<DanhSachNhanVienModel>(dbString);
            m_ucTenNV.Show();
            var spsheetLoc = spsheet_BangCongNhatCongKhoan.Location;
            Rectangle rec = spsheet_BangCongNhatCongKhoan.GetCellBounds(Cell.TopRowIndex, Cell.RightColumnIndex);
            m_ucTenNV.Location = new Point(rec.Right + spsheetLoc.X, rec.Top + spsheetLoc.Y);
            m_ucTenNV.BringToFront();
            m_ucTenNV.Fcn_UpDateDanhSachNhanVien(DanhSachNV, ColCode, Code);
        }
        private CongNhatCongKhoanChiTiet MapRow(Worksheet sheet, Dictionary<string, int> lstTitles, int rowIndex)
        {
            var itemIndex = new CongNhatCongKhoanChiTiet();
            foreach (var item in lstTitles)
            {
                try
                {
                    Cell cell = sheet.Rows[rowIndex][item.Value];
                    PropertyInfo propertyInfo = typeof(CongNhatCongKhoanChiTiet).GetProperty(item.Key);
                    if (propertyInfo != null)
                    {
                        switch (Type.GetTypeCode(propertyInfo.PropertyType))
                        {
                            case TypeCode.Boolean:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsBoolean ? cell.Value.BooleanValue : false);
                                break;

                            case TypeCode.Decimal:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (decimal?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.Double:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (double?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.Int16:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (short?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.Int32:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (int?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.Int64:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsNumeric ? (long?)Math.Round(cell.Value.NumericValue, 4) : null);
                                break;

                            case TypeCode.String:
                                propertyInfo.SetValue(itemIndex, cell.DisplayText);
                                break;

                            case TypeCode.DateTime:
                                propertyInfo.SetValue(itemIndex, cell.Value.IsDateTime ? (DateTime?)cell.Value.DateTimeValue : null);
                                break;
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            if (string.IsNullOrEmpty(itemIndex.CodeCongTacTheoGiaiDoan)) itemIndex.CodeCongTacTheoGiaiDoan = null;
            if (string.IsNullOrEmpty(itemIndex.CodeCongTacThuCong)) itemIndex.CodeCongTacThuCong = null;
            return itemIndex;
        }
        private double Fcn_CalCulateMinMax(TimeSpan timeMin, TimeSpan timeMax)
        {
            double Time = 0;
            if (timeMin == TimeSpan.Zero || timeMax == TimeSpan.Zero)
                return 0;
            Time = Math.Round((double)((double)(timeMax.Hours * 60 + timeMax.Minutes - timeMin.Hours * 60 - timeMin.Minutes) / 60.00), 2);
            return Time;

        }
        private double ValidateTime(SpreadsheetCellValidatingEventArgs e, List<string> lstValueColumnLefts, List<string> lstValueColumnRights, int type)
        {
            TimeSpan timeSpanNew = TimeSpan.Zero;
            TimeSpan timeSpan = TimeSpan.Zero;
            double Time = 0;
            if (!string.IsNullOrEmpty(e.EditorText))
            {
                if (!DateTimeHelper.IsTimeSpan(e.EditorText, out timeSpan))
                {
                    e.EditorText = string.Empty;
                    e.Cancel = true;
                }
            }
            TimeSpan timeMin = TimeSpan.Zero;
            TimeSpan timeMax = TimeSpan.Zero;
            timeSpanNew = timeSpan;
            List<TimeSpan> lstTimes;
            switch (type)
            {
                case 1:
                    lstTimes = new List<TimeSpan>();
                    foreach (var item in lstValueColumnRights)
                    {
                        if (string.IsNullOrEmpty(item)) continue;
                        if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
                        {
                            lstTimes.Add(timeSpan);
                        }
                    }
                    if (!lstTimes.Any()) return 0;
                    timeMax = lstTimes.Max();
                    if (!DateTimeHelper.CompareTimeSpanNhoHon(e.EditorText, timeMax))
                    {
                        e.EditorText = string.Empty;
                        e.Cancel = true;
                        break;
                    }
                    timeMin = timeSpanNew;
                    break;
                case 2:
                    lstTimes = new List<TimeSpan>();
                    foreach (var item in lstValueColumnLefts)
                    {
                        if (string.IsNullOrEmpty(item)) continue;
                        if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
                        {
                            lstTimes.Add(timeSpan);
                        }
                    }
                    if (lstTimes.Any())
                    {
                        timeMin = lstTimes.Min();
                    }
                    timeMax = timeSpanNew;
                    if (timeMin != TimeSpan.Zero && timeMax != TimeSpan.Zero || timeMin != TimeSpan.Zero && timeMax == TimeSpan.Zero)
                    {
                        if (!DateTimeHelper.CompareTimeSpanLonHon(e.EditorText, timeMin))
                        {
                            e.EditorText = string.Empty;
                            e.Cancel = true;
                            break;
                        }
                    }
                    //lstTimes = new List<TimeSpan>();
                    //foreach (var item in lstValueColumnRights)
                    //{
                    //    if (string.IsNullOrEmpty(item)) continue;
                    //    if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
                    //    {
                    //        lstTimes.Add(timeSpan);
                    //    }
                    //}
                    //if (lstTimes.Any())
                    //{
                    //    timeMin = lstTimes.Min();
                    //}
                    //if (timeMin != TimeSpan.Zero && timeMax != TimeSpan.Zero)
                    //{
                    //    if (!DateTimeHelper.CompareTimeSpanBetwwen(e.EditorText, timeMax, timeMin))
                    //    {
                    //        e.EditorText = string.Empty;
                    //        e.Cancel = true;
                    //        break;
                    //    }
                    //}
                    //else if (timeMin != TimeSpan.Zero && timeMax == TimeSpan.Zero)
                    //{
                    //    if (!DateTimeHelper.CompareTimeSpanNhoHon(e.EditorText, timeMin))
                    //    {
                    //        e.EditorText = string.Empty;
                    //        e.Cancel = true;
                    //        break;
                    //    }
                    //}
                    //else if (timeMin == TimeSpan.Zero && timeMax != TimeSpan.Zero)
                    //{
                    //    if (!DateTimeHelper.CompareTimeSpanLonHon(e.EditorText, timeMax))
                    //    {
                    //        e.EditorText = string.Empty;
                    //        e.Cancel = true;
                    //        break;
                    //    }
                    //}
                    break;
                case 3:
                    lstTimes = new List<TimeSpan>();
                    foreach (var item in lstValueColumnLefts)
                    {
                        if (string.IsNullOrEmpty(item)) continue;
                        if (DateTimeHelper.IsTimeSpan(item, out timeSpan) && timeSpan != TimeSpan.Zero)
                        {
                            lstTimes.Add(timeSpan);
                        }
                    }
                    if (!lstTimes.Any()) return 0;
                    timeMax = lstTimes.Max();
                    if (!DateTimeHelper.CompareTimeSpanLonHon(e.EditorText, timeMax))
                    {
                        e.EditorText = string.Empty;
                        e.Cancel = true;
                        break;
                    }
                    break;
            }
            if (!e.Cancel)
                return Fcn_CalCulateMinMax(timeMin, timeMax);
            else
                return 0;
        }

        private void spsheet_BangCongNhatCongKhoan_CellEndEdit(object sender, SpreadsheetCellValidatingEventArgs e)
        {
            Worksheet worksheet = spsheet_BangCongNhatCongKhoan.ActiveWorksheet;
            CellRange titleHeader = worksheet.Range[MyConstant.TITLE_HEADER];
            Dictionary<string, int> lstTitles = ExcelHelper.GetListColumnByRange(titleHeader);
            string columnName = lstTitles.Where(x => x.Value == e.ColumnIndex)?.First().Key;
            var itemIndex = MapRow(worksheet, lstTitles, e.RowIndex);
            List<string> lstValueLefts = new List<string>();
            List<string> lstValueRights = new List<string>();
            double Time = 0;
            string ColCode = "", conditon = "";
            bool Cal = true;
            string FieldName = worksheet.Rows[0][e.ColumnIndex].Value.TextValue;
            string Code = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.CODE]].Value.TextValue;
            switch (columnName)
            {
                case MyConstant.GIOBATDAUSANG:
                    lstValueRights.Add(itemIndex.GioKetThucSang);
                    //lstValueRights.Add(itemIndex.GioBatDauChieu);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    //lstValueRights.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 1);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOSANG]].SetValue(Time);
                    ColCode = "TongGioSang";
                    break;
                case MyConstant.GIOKETTHUCSANG:
                    lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueRights.Add(itemIndex.GioBatDauChieu);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    //lstValueRights.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOSANG]].SetValue(Time);
                    ColCode = "TongGioSang";
                    break;
                case MyConstant.GIOBATDAUCHIEU:
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    lstValueRights.Add(itemIndex.GioKetThucChieu);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 1);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOCHIEU]].SetValue(Time);
                    ColCode = "TongGioChieu";
                    break;
                case MyConstant.GIOKETTHUCCHIEU:
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    lstValueLefts.Add(itemIndex.GioBatDauChieu);
                    //lstValueRights.Add(itemIndex.GioBatDauToi);
                    //lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOCHIEU]].SetValue(Time);
                    ColCode = "TongGioChieu";
                    break;
                case MyConstant.GIOBATDAUTOI:
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueLefts.Add(itemIndex.GioBatDauChieu);
                    //lstValueLefts.Add(itemIndex.GioKetThucChieu);
                    lstValueRights.Add(itemIndex.GioKetThucToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 1);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOTOI]].SetValue(Time);
                    ColCode = "TongGioToi";
                    break;
                case MyConstant.GIOKETTHUCTOI:
                    //lstValueLefts.Add(itemIndex.GioBatDauSang);
                    //lstValueLefts.Add(itemIndex.GioKetThucSang);
                    //lstValueLefts.Add(itemIndex.GioBatDauChieu);
                    //lstValueLefts.Add(itemIndex.GioKetThucChieu);
                    lstValueLefts.Add(itemIndex.GioBatDauToi);
                    Time = ValidateTime(e, lstValueLefts, lstValueRights, 2);
                    worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIOTOI]].SetValue(Time);
                    ColCode = "TongGioToi";
                    break;
                case MyConstant.GHICHU:
                    ColCode = "GhiChu";
                    Cal = false;
                    break;
                case MyConstant.KHOILUONG:
                    ColCode = "KhoiLuong";
                    Cal = false;
                    break;
                default:
                    break;
            }
            worksheet.Calculate();
            if (ColCode != "")
            {
                string CodeNV = worksheet.Rows[e.RowIndex][lstTitles["Code"]].Value.TextValue;
                if (string.IsNullOrEmpty(CodeNV))
                {
                    MessageShower.ShowWarning("Vui lòng Thêm nhân viên trước khi nhập giá trị ô này!!!!!");
                    spsheet_BangCongNhatCongKhoan.CloseCellEditor(CellEditorEnterValueMode.Cancel);
                    return;
                }
                worksheet.Calculate();
                double TongGio = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIO]].Value.NumericValue;
                double TongCong = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGCONG]].Value.NumericValue;
                string dbString = Cal ? $"UPDATE {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} SET " +
               $"\"TongGio\" = '{TongGio}', " +
                    $"\"TongCong\" = '{TongCong}'," +
               $"{ColCode} = '{Time}'," +
               $"{FieldName} = '{e.EditorText}'" +
               $" WHERE \"Code\" = '{Code}'" :
               $"UPDATE {MyConstant.TBL_MTC_CHITIETNHATTRINH} SET " +
               $"\"TongGio\" = '{TongGio}', " +
               $"{ColCode} = '{e.EditorText}'" +
               $" WHERE \"Code\" = '{Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            // double TongGio = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGGIO]].Value.NumericValue;
            // double TongCong = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.TONGCONG]].Value.NumericValue;
            // Code = worksheet.Rows[e.RowIndex][lstTitles[MyConstant.CODE]].Value.TextValue;
            // string dbString = IsEdit? $"UPDATE {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} SET " +
            //$"\"TongGio\" = '{TongGio}', " +
            //$"\"TongCong\" = '{TongCong}'," +
            //$"{columnName} = '{e.EditorText}'," +
            //$"{ColCode} = '{Time}'" +
            //$" WHERE \"Code\" = '{Code}'":
            //$"UPDATE {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNCONGNHATCONGKHOAN_CHITIET} SET " +
            //$"{columnName} = '{e.EditorText}'" +
            //$" WHERE \"Code\" = '{Code}'";
            // DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
        }

        private void slue_TenNhanVien_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void sb_UpDateLuongNCQL_Click(object sender, EventArgs e)
        {

        }

        private void spsheet_TamUng_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Worksheet ws = spsheet_TamUng.Document.Worksheets[0];
                CellRange RangeData = ws.Range["Data_TamUng"];
                Dictionary<string, string> Dic = MyFunction.fcn_getDicOfColumn(RangeData);
                string Code = ws.Rows[ws.SelectedCell.TopRowIndex][Dic[DanhSachNhanVienConstant.COL_Code]].Value.TextValue;
                string dbString = $"DELETE  FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_TAMUNG} WHERE \"Code\" = '{Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
        }

        private void spsheet_BangCongNhatCongKhoan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
                Fcn_LoadDaTaCongNhatCongKhoan(spsheet_BangCongNhatCongKhoan, cbb_LayCongTacNgoaiKeHoach.Checked);
        }

        private void searchLookUpEdit1View_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column == bandedGridColumn5 && e.IsGetData)
            {
                DevExpress.XtraGrid.Views.Tile.TileView view = sender as DevExpress.XtraGrid.Views.Tile.TileView;
                string fileCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_UrlImage) as string ?? string.Empty;
                object objCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_Code);
                if (objCode != null && !string.IsNullOrEmpty(objCode.ToString()))
                {

                    if (!string.IsNullOrEmpty(fileCode))
                    {
                        string filePath = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM, objCode.ToString()), fileCode);
                        e.Value = FileHelper.fcn_ImageStreamDoc(filePath) ?? Properties.Resources.QLTC;
                    }
                    else
                        e.Value = Properties.Resources.QLTC;
                }
            }
        }
    }
}
