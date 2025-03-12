using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.TDKH;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_DocPhanKhaiNhaThau : DevExpress.XtraEditors.XtraForm
    {
        public string filePath { get; set; }
        public string m_codenhathau { get; set; }
        public XtraForm_DocPhanKhaiNhaThau()
        {
            InitializeComponent();
            this.CausesValidation = false;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            dxValidationProvider.Validate();
        }
        private void Fcn_TruyenDataExcelThuCongGT(int firstLine, int lastLine)
        {
            Dictionary<Control, string> dic = new Dictionary<Control, string>()
            {
                {txt_congtrinh_stt, "STT" },
                {txtCTMaDuToan, "Mã hiệu công tác" },
                {txtCTTenCongTac, "Danh mục công tác" },
                {txtCTDonVi, "Đơn vị" },
                {txtCTKhoiLuong, "Khối lượng toàn bộ" },
                {te_TenThauChinh, "Cột thầu chính" },
            };
            foreach (var item in dic)
            {
                if (item.Key.Text.Trim(' ') == "")
                {
                    MessageShower.ShowInformation($"Vui lòng nhập tên cột cho \"{item.Value}\"");
                    return;
                }
            }
            bool IsThauPhu = ce_Tp.Checked;
            bool IsThauPhuDG = ce_DonGiaTP.Checked;
            bool IsToDoi = ce_TD.Checked;
            bool IsToDoiDG = ce_DonGiaTD.Checked;
            if (IsThauPhu && (!te_beginTP.Text.HasValue() || !te_beginTP.Text.HasValue()))
            {
                MessageShower.ShowInformation($"Vui lòng nhập tên cột bắt đầu và kết thức cho THẦU PHỤ");
                return;
            }      
            if (IsThauPhuDG && (!te_DonGiaTPBegin.Text.HasValue() || !te_DonGiaTPEnd.Text.HasValue()))
            {
                MessageShower.ShowInformation($"Vui lòng nhập tên cột bắt đầu và kết thức cho ĐƠN GIÁ THẦU PHỤ");
                return;
            }
            if (IsToDoi && (!te_beginTD.Text.HasValue() || !te_beginTD.Text.HasValue()))
            {
                MessageShower.ShowInformation($"Vui lòng nhập tên cột bắt đầu và kết thức cho TỔ ĐỘI");
                return;
            }
            if (IsToDoiDG && (!te_DonGiaTDBegin.Text.HasValue() || !te_DonGiaTDEnd.Text.HasValue()))
            {
                MessageShower.ShowInformation($"Vui lòng nhập tên cột bắt đầu và kết thức cho ĐƠN GIÁ TỔ ĐỘI");
                return;
            }
            if (ce_TuThucHien.Checked && !te_TuThucHien.Text.HasValue())
            {
                MessageShower.ShowInformation($"Vui lòng nhập tên cột chứa ĐƠN VỊ TỰ THỰC HIỆN");
                return;
            }
            int RowTieuDe = (int)sp_TenNhaThau.Value - 1;
            if (RowTieuDe < 0)
            {
                MessageShower.ShowInformation($"Vui lòng nhập đúng dòng tiêu đề, Dòng tiêu đề phải lớn hơn 0");
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu excel, Vui lòng chờ!");
            IWorkbook workbook = spsheet_XemFile.Document;
            string queryStr = $"SELECT * FROM {TDKH.TBL_DanhMucCongTac}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu}";
            DataTable dtHaoPhi = DataProvider.InstanceTHDA.ExecuteQuery(queryStr).Clone();
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            DataTable dt_congtactheogiaidoan = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomCongTac}";
            DataTable dt_NhomCT = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_NhomDienGiai}";
            DataTable dt_NhomDienDai = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_PhanTuyen}";
            DataTable dt_Tuyen = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacCon}";
            DataTable dt_Chitietcongtaccon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            queryStr = $"SELECT * FROM {TDKH.Tbl_TenDienDaiTuDo}";
            DataTable dt_TenDienDaiTuDo = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            Dictionary<string, string> NameTP = new Dictionary<string, string>();
            Dictionary<string, string> NameTD = new Dictionary<string, string>();
            Worksheet ws = workbook.Worksheets[cboCTtenSheet.Text];
            long SortId = 0, SortIdNhom = 0, SortIdTuyen = 0;
            //Regex ColumTP = new Regex($"[{te_beginTP.Text}-{te_EndTP.Text}]");
            //Regex ColumTD = new Regex($"[{te_beginTD.Text}-{te_EndTD.Text}]");
            CellRange UseRange = ws.GetUsedRange();
            Row Crow = ws.Rows[RowTieuDe];
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINNHATHAUPHU}";
            DataTable dttp = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);    
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINTODOITHICONG}";
            DataTable dttd = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            dttd.Clear();
            dttp.Clear();
            string m_CodeDuAn = SharedControls.slke_ThongTinDuAn.EditValue.ToString();
            string m_codegiadoan = SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString();
            bool _CheckTP = false, _CheckTPDG = false,_CheckTD = false,_CheckTDDG = false;
            List<string> lstColDGTP = new List<string>();
            List<string> lstColDGTD = new List<string>();
            for (int i = UseRange.LeftColumnIndex; i <= UseRange.RightColumnIndex; i++)
            {
                if (!ws.Columns[i].Visible)
                    continue;
                string ColumName = ws.Columns[i].Heading;               
                if (IsThauPhu)
                {
                    if (ColumName == te_beginTP.Text)
                    {
                        _CheckTP = te_beginTP.Text==te_EndTP.Text?false: true;
                        DataRow RowTP = dttp.NewRow();
                        string CodeTP = Guid.NewGuid().ToString();
                        RowTP["Ten"] = Crow[ColumName].Value.TextValue;
                        RowTP["Code"] = CodeTP;
                        RowTP["CodeDuAn"] = m_CodeDuAn;
                        dttp.Rows.Add(RowTP);
                        NameTP.Add(ColumName, CodeTP);
                        continue;
                    }
                    else if (ColumName == te_EndTP.Text&&_CheckTP)
                    {
                        _CheckTP = false;
                        DataRow RowTP = dttp.NewRow();
                        string CodeTP = Guid.NewGuid().ToString();
                        RowTP["Ten"] = Crow[ColumName].Value.TextValue;
                        RowTP["Code"] = CodeTP;
                        RowTP["CodeDuAn"] = m_CodeDuAn;
                        dttp.Rows.Add(RowTP);
                        NameTP.Add(ColumName, CodeTP);
                        continue;
                    }
                    else if (_CheckTP)
                    {
                        DataRow RowTP = dttp.NewRow();
                        string CodeTP = Guid.NewGuid().ToString();
                        RowTP["Ten"] = Crow[ColumName].Value.TextValue;
                        RowTP["Code"] = CodeTP;
                        RowTP["CodeDuAn"] = m_CodeDuAn;
                        dttp.Rows.Add(RowTP);
                        NameTP.Add(ColumName, CodeTP);
                        continue;
                    }
                    else if (IsThauPhuDG)
                    {
                        if (ColumName == te_DonGiaTPBegin.Text)
                        {
                            _CheckTPDG = te_DonGiaTPBegin.Text == te_DonGiaTPEnd.Text ? false : true;
                            lstColDGTP.Add(ColumName);
                            continue;
                        }
                        else if (ColumName == te_DonGiaTPEnd.Text && _CheckTPDG)
                        {
                            _CheckTPDG = false;
                            lstColDGTP.Add(ColumName);
                            continue;
                        }
                        else if (_CheckTPDG)
                        {
                            lstColDGTP.Add(ColumName);
                            continue;
                        }
                    }
                    //MatchCollection Multimatch = ColumTP.Matches(ColumName);
                    //if (Multimatch.Count > 0)
                    //{
                    //    if (ColumName.Length == Multimatch[0].Length && !string.IsNullOrEmpty(Crow[ColumName].Value.TextValue))
                    //    {
                    //        DataRow RowTP = dttp.NewRow();
                    //        string CodeTP = Guid.NewGuid().ToString();
                    //        RowTP["Ten"] = Crow[ColumName].Value.TextValue;
                    //        RowTP["Code"] = CodeTP;
                    //        RowTP["CodeDuAn"] = m_CodeDuAn;
                    //        dttp.Rows.Add(RowTP);
                    //        NameTP.Add(ColumName, CodeTP);
                    //        continue;
                    //    }
                    //}
                }
                if (IsToDoi)
                {
                    if (ColumName == te_beginTD.Text)
                    {
                        _CheckTD = te_beginTD.Text == te_EndTD.Text ? false : true;
                        DataRow RowTD = dttd.NewRow();
                        string CodeTD = Guid.NewGuid().ToString();
                        RowTD["Ten"] = Crow[ColumName].Value.TextValue;
                        RowTD["Code"] = CodeTD;
                        RowTD["CodeDuAn"] = m_CodeDuAn;
                        dttd.Rows.Add(RowTD);
                        NameTD.Add(ColumName, CodeTD);
                        continue;
                    }
                    else if (ColumName == te_EndTD.Text)
                    {
                        _CheckTD = false;
                        DataRow RowTD = dttd.NewRow();
                        string CodeTD = Guid.NewGuid().ToString();
                        RowTD["Ten"] = Crow[ColumName].Value.TextValue;
                        RowTD["Code"] = CodeTD;
                        RowTD["CodeDuAn"] = m_CodeDuAn;
                        dttd.Rows.Add(RowTD);
                        NameTD.Add(ColumName, CodeTD);
                        continue;
                    }
                    else if (_CheckTD)
                    {
                        DataRow RowTD = dttd.NewRow();
                        string CodeTD = Guid.NewGuid().ToString();
                        RowTD["Ten"] = Crow[ColumName].Value.TextValue;
                        RowTD["Code"] = CodeTD;
                        RowTD["CodeDuAn"] = m_CodeDuAn;
                        dttd.Rows.Add(RowTD);
                        NameTD.Add(ColumName, CodeTD);
                        continue;
                    }
                    //MatchCollection Multimatch = ColumTD.Matches(ColumName);
                    //if (Multimatch.Count > 0)
                    //{
                    //    if (ColumName.Length == Multimatch[0].Length && !string.IsNullOrEmpty(Crow[ColumName].Value.TextValue))
                    //    {
                    //        DataRow RowTD = dttd.NewRow();
                    //        string CodeTD = Guid.NewGuid().ToString();
                    //        RowTD["Ten"] = Crow[ColumName].Value.TextValue;
                    //        RowTD["Code"] = CodeTD;
                    //        RowTD["CodeDuAn"] = m_CodeDuAn;
                    //        dttd.Rows.Add(RowTD);
                    //        NameTD.Add(ColumName, CodeTD);
                    //    }
                    //}
                }
            }    
            if (IsToDoiDG || IsThauPhuDG)
            {
                for (int i = UseRange.LeftColumnIndex; i <= UseRange.RightColumnIndex; i++)
                {
                    if (!ws.Columns[i].Visible)
                        continue;
                    string ColumName = ws.Columns[i].Heading;
                   
                    if (IsToDoiDG)
                    {
                        if (ColumName == te_DonGiaTDBegin.Text)
                        {
                            _CheckTDDG = te_DonGiaTDBegin.Text == te_DonGiaTDEnd.Text ? false : true;
                            lstColDGTD.Add(ColumName);
                            continue;
                        }
                        else if (ColumName == te_DonGiaTDEnd.Text && _CheckTDDG)
                        {
                            _CheckTDDG = false;
                            lstColDGTD.Add(ColumName);
                            continue;
                        }
                        else if (_CheckTDDG)
                        {
                            lstColDGTD.Add(ColumName);
                            continue;
                        }
                    }
                }
            }
            if (IsThauPhu && IsThauPhuDG)
            {
                if (NameTP.Count() != lstColDGTP.Count())
                {
                    MessageShower.ShowInformation($"Số cột Đơn giá và Khối lượng Thầu phụ không phù hợp, Vui lòng kiểm tra lại!!!!!");
                    return;
                }
            }       
            if (IsToDoi && IsToDoiDG)
            {
                if (NameTD.Count() != lstColDGTD.Count())
                {
                    MessageShower.ShowInformation($"Số cột Đơn giá và Khối lượng Tổ đội không phù hợp, Vui lòng kiểm tra lại!!!!!");
                    return;
                }
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dttp, MyConstant.TBL_THONGTINNHATHAUPHU);
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dttd, MyConstant.TBL_THONGTINTODOITHICONG);
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn cập nhập lại Tên Thầu Chính không???");
            if (rs == DialogResult.Yes)
            {
                queryStr = $"UPDATE {MyConstant.TBL_THONGTINNHATHAU} SET \"Ten\"='{ws.Rows[RowTieuDe][te_TenThauChinh.Text].Value}' WHERE " +
                    $"\"Code\"='{m_codenhathau}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(queryStr);
            }
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{m_CodeDuAn}'";
            DataTable dt_CTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string lstCodeCtrinh = MyFunction.fcn_Array2listQueryCondition(dt_CTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lstCodeCtrinh})";
            DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            int SortIDCT = dt_CTrinh.Rows.Count;
            int SortIDHM = dt_HM.Rows.Count;
            string DonVi = string.Empty, TenCongTac = string.Empty, CodeCtrinh = string.Empty, CodeHM = string.Empty,CodeDVTTH=string.Empty,
                CodeDDCT = string.Empty,
                CodeDDNhom = string.Empty,
                CodeDDTuyen = string.Empty; ;
            int Index = rg_LayDuLieu.SelectedIndex;
            int STT = 0;
            dt_NhomDienDai.Clear();
            dt_Chitietcongtaccon.Clear();
            dt_congtactheogiaidoan.Clear();
            dt.Clear();
            dt_NhomCT.Clear();
            dtHaoPhi.Clear();
            dt_TenDienDaiTuDo.Clear();
            double KLCT = 0,KLNhom=0;
            int RowNhom = 0;
            if (ce_TuThucHien.Checked)
            {
                queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINNHATHAUPHU} WHERE \"CodeTongThau\" IS NOT NULL AND \"CodeDuAn\"='{m_CodeDuAn}'";
                DataTable dttth = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                CodeDVTTH = dttth.Rows[0][0].ToString();
            }
            bool TT = te_TTDocVao.Text.HasValue();
            double TTDocVao = 0;
            rs = MessageShower.ShowYesNoQuestion("Bạn có muốn làm tròn số liệu lấy vào????");
            bool LamTron = rs == DialogResult.Yes ? true : false;
            int SaiSo = 0;
            if (rs == DialogResult.Yes)
            {
                var rslt = DevExpress.XtraEditors.XtraInputBox.Show("Nhập sai số làm tròn", "Nhập sai số làm tròn", "3");
                if (string.IsNullOrEmpty(rslt))
                    return;
                if (!int.TryParse(rslt, out SaiSo))
                {
                    MessageShower.ShowError("Vui lòng nhập đúng định dạng sai số");
                    return;
                }
            }
            for (int i = firstLine; i <= lastLine; i++)
            {
                Row crRow = ws.Rows[i];
                if (!crRow.Visible)
                    continue;
                TenCongTac = crRow[txtCTTenCongTac.Text].Value.ToString();
                DonVi = txtCTDonVi.Text == "" ? default : crRow[txtCTDonVi.Text].Value.TextValue;
                string _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                int.TryParse(_STT, out STT);
                WaitFormHelper.ShowWaitForm($"{_STT}_{TenCongTac}");
                if (STT > 0)
                {
                    CodeDDCT = string.Empty;
                    CodeDDNhom = string.Empty;
                    CodeDDTuyen = string.Empty;
                    if (string.IsNullOrEmpty(CodeCtrinh) && Index == 0)
                    {
                        CodeCtrinh = Guid.NewGuid().ToString();
                        DataRow CrowCTrinh = dt_CTrinh.NewRow();
                        CrowCTrinh["Code"] = CodeCtrinh;
                        CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                        CrowCTrinh["SortID"] = SortIDCT++;
                        CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                        dt_CTrinh.Rows.Add(CrowCTrinh);
                    }
                    CodeHM = Guid.NewGuid().ToString();
                    DataRow CrowHM = dt_HM.NewRow();
                    CrowHM["Code"] = CodeHM;
                    CrowHM["SortID"] = SortIDHM++;
                    CrowHM["CodeCongTrinh"] = CodeCtrinh;
                    CrowHM["Ten"] = TenCongTac;
                    dt_HM.Rows.Add(CrowHM);                    
                }
                else if (STT == 0)
                {
                    if (_STT.ToUpper() == MyConstant.TYPEROW_CongTrinh)
                    {
                        CodeDDCT = string.Empty;
                        CodeDDNhom = string.Empty;
                        CodeDDTuyen = string.Empty;
                        if (Index > 0)
                            continue;
                        CodeCtrinh = Guid.NewGuid().ToString();
                        DataRow CrowCTrinh = dt_CTrinh.NewRow();
                        CrowCTrinh["Code"] = CodeCtrinh;
                        CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                        CrowCTrinh["SortID"] = SortIDCT++;
                        CrowCTrinh["Ten"] = TenCongTac;
                        dt_CTrinh.Rows.Add(CrowCTrinh);
                        CodeHM = string.Empty;
                    }
                    else if(_STT.ToUpper() == MyConstant.TYPEROW_HangMuc)
                    {
                        CodeDDCT = string.Empty;
                        CodeDDNhom = string.Empty;
                        CodeDDTuyen = string.Empty;
                        if (string.IsNullOrEmpty(CodeCtrinh) && Index == 0)
                        {
                            CodeCtrinh = Guid.NewGuid().ToString();
                            DataRow CrowCTrinh = dt_CTrinh.NewRow();
                            CrowCTrinh["Code"] = CodeCtrinh;
                            CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                            CrowCTrinh["SortID"] = SortIDCT++;
                            CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                            dt_CTrinh.Rows.Add(CrowCTrinh);
                        }
                        CodeHM = Guid.NewGuid().ToString();
                        DataRow CrowHM = dt_HM.NewRow();
                        CrowHM["Code"] = CodeHM;
                        CrowHM["SortID"] = SortIDHM++;
                        CrowHM["CodeCongTrinh"] = CodeCtrinh;
                        CrowHM["Ten"] = TenCongTac;
                        dt_HM.Rows.Add(CrowHM);
                    }
                    else if (_STT == Te_DDCT.Text)
                    {
                        CodeDDCT = Guid.NewGuid().ToString();
                        DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                        CrowDD["Code"] = CodeDDCT;
                        CrowDD["CodeDuAn"] = m_CodeDuAn;
                        CrowDD["Ten"] = TenCongTac;
                        dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                    }
                    else if (_STT == Te_DDNhom.Text)
                    {
                        CodeDDNhom = Guid.NewGuid().ToString();
                        DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                        CrowDD["Code"] = CodeDDNhom;
                        CrowDD["CodeDuAn"] = m_CodeDuAn;
                        CrowDD["Ten"] = TenCongTac;
                        dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                    }
                    else if (_STT == Te_DDTuyen.Text)
                    {
                        CodeDDTuyen = Guid.NewGuid().ToString();
                        DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                        CrowDD["Code"] = CodeDDTuyen;
                        CrowDD["CodeDuAn"] = m_CodeDuAn;
                        CrowDD["Ten"] = TenCongTac;
                        dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                    }
                    else
                    {
                        int count = _STT.Count(x => x == '.');
                        if (string.IsNullOrEmpty(CodeHM))
                        {
                            if (string.IsNullOrEmpty(CodeCtrinh) && Index == 0)
                            {
                                CodeCtrinh = Guid.NewGuid().ToString();
                                DataRow CrowCTrinh = dt_CTrinh.NewRow();
                                CrowCTrinh["Code"] = CodeCtrinh;
                                CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                                CrowCTrinh["SortID"] = SortIDCT++;
                                CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                                dt_CTrinh.Rows.Add(CrowCTrinh);
                            }
                            CodeHM = Guid.NewGuid().ToString();
                            DataRow CrowHM = dt_HM.NewRow();
                            CrowHM["Code"] = CodeHM;
                            CrowHM["SortID"] = SortIDHM++;
                            CrowHM["CodeCongTrinh"] = CodeCtrinh;
                            CrowHM["Ten"] = $"Hạng mục {dt_HM.Rows.Count}";
                            dt_HM.Rows.Add(CrowHM);
                        }
                        if (count == 1)
                        {
                            DataRow NewRow = dt_Tuyen.NewRow();
                            string CodeTuyen = Guid.NewGuid().ToString();
                            NewRow["Code"] = CodeTuyen;
                            NewRow["CodeHangMuc"] = CodeHM;
                            NewRow["Ten"] = TenCongTac;
                            NewRow["SortId"] = SortIdTuyen++;
                            if (!string.IsNullOrEmpty(CodeDDTuyen))
                            {
                                NewRow["CodeDienDai"] = CodeDDTuyen;
                                CodeDDTuyen = string.Empty;
                            }
                            dt_Tuyen.Rows.Add(NewRow);
                            for (int j = i + 1; j <= lastLine; j++)
                            {
                                crRow = ws.Rows[j];
                                _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                int.TryParse(_STT, out STT);
                                count = _STT.Count(x => x == '.');
                                string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
                                if (STT > 0 || count == 1 || _STT == Te_DDTuyen.Text ||
                                    _STT.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH|| _STT.ToUpper() == MyConstant.TYPEROW_HangMuc)
                                {
                                    i = j - 1;
                                    break;
                                }
                                else if (_STT == Te_DDNhom.Text)
                                {
                                    CodeDDNhom = Guid.NewGuid().ToString();
                                    DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                                    CrowDD["Code"] = CodeDDNhom;
                                    CrowDD["CodeDuAn"] = m_CodeDuAn;
                                    CrowDD["Ten"] = crRow[txtCTTenCongTac.Text].Value.ToString();
                                    dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                                }
                                else if (_STT == Te_DDCT.Text)
                                {
                                    CodeDDCT = Guid.NewGuid().ToString();
                                    DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                                    CrowDD["Code"] = CodeDDCT;
                                    CrowDD["CodeDuAn"] = m_CodeDuAn;
                                    CrowDD["Ten"] = crRow[txtCTTenCongTac.Text].Value.ToString();
                                    dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                                }
                                else if (count == 2)
                                {
                                    i = j;
                                    //string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                                    DataRow NewRowNhom = dt_NhomCT.NewRow();
                                    string CodeNhomCT = Guid.NewGuid().ToString();
                                    NewRowNhom["Code"] = CodeNhomCT;
                                    NewRowNhom["CodeHangMuc"] = CodeHM;
                                    NewRowNhom["Ten"] = tencongtac;
                                    NewRowNhom["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                                    NewRowNhom["CodePhanTuyen"] = CodeTuyen;
                                    if (!string.IsNullOrEmpty(CodeDDNhom))
                                    {
                                        NewRowNhom["CodeDienDai"] = CodeDDNhom;
                                        CodeDDNhom = string.Empty;
                                    }
                                    RowNhom = j;
                                    if (Te_DonGiaNhom.Text.HasValue())
                                    {
                                        double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                        if (DonGiaNhom > 0)
                                            NewRowNhom["DonGia"] = NewRowNhom["DonGiaThiCong"] =LamTron?Math.Round(DonGiaNhom,SaiSo): DonGiaNhom;
                                    }
                                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out  KLNhom);
                                    if (KLNhom > 0)
                                        NewRowNhom["KhoiLuongKeHoach"] = NewRowNhom["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                    NewRowNhom["SortId"] = SortIdNhom++;
                                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                    if (txt_congtrinh_sttND.Text.HasValue())
                                    {
                                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                    }
                                    JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                    NewRowNhom["GhiChuBoSungJson"] = encryptedStr;
                                    dt_NhomCT.Rows.Add(NewRowNhom);
                                    bool NewNhomTTH = true;
                                    Dictionary<string, string> CodeNhomTP = new Dictionary<string, string>();
                                    Dictionary<string, string> CodeNhomTD = new Dictionary<string, string>();
                                    string CodeNhomTTH = Guid.NewGuid().ToString();
                                    for (int k = j + 1; k <= lastLine; k++)
                                    {
                                        crRow = ws.Rows[k];
                                        _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                        int.TryParse(_STT, out STT);
                                        count = _STT.Count(x => x == '.');
                                        tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
                                        if (STT > 0 || count <= 2 && count > 0 || _STT == Te_DDNhom.Text ||
                                            _STT.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH || _STT.ToUpper() == MyConstant.TYPEROW_HangMuc)
                                        {
                                            j = k - 1;
                                            i = j - 1;
                                            break;
                                        }
                                        else if (_STT == Te_DDCT.Text)
                                        {
                                            CodeDDCT = Guid.NewGuid().ToString();
                                            DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                                            CrowDD["Code"] = CodeDDCT;
                                            CrowDD["CodeDuAn"] = m_CodeDuAn;
                                            CrowDD["Ten"] = crRow[txtCTTenCongTac.Text].Value.ToString();
                                            dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                                        }
                                        else if (count > 2)
                                        {
                                            j = k;
                                            i = j;
                                            string MaHieu = txtCTMaDuToan.Text.HasValue() ? crRow[txtCTMaDuToan.Text].Value.ToString() : string.Empty;
                                            string code = Guid.NewGuid().ToString();
                                            DataRow dtRow = dt.NewRow();
                                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                            dtRow["Code"] = code;
                                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                                            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                            double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out  KLCT);
                                            dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                            dtRow["CodeHangMuc"]  = CodeHM;
                                            dtRow["MaHieuCongTac"] = string.IsNullOrEmpty(MaHieu) ? "TT" : MaHieu;
                                            dtRow["TenCongTac"] = tencongtac;
                                            dtRow["DonVi"]  = crRow[txtCTDonVi.Text].Value.ToString();
                                            dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                            dtRow["CodePhanTuyen"] = CodeTuyen;
                                            if (TT)
                                            {
                                                double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                                dtRow["ThanhTienDocVao"] = TTDocVao;
                                            }
                                            if (!string.IsNullOrEmpty(CodeDDCT))
                                            {
                                                dtRow["CodeDienDai"] = CodeDDCT;
                                                CodeDDCT = string.Empty;
                                            }
                                            if (txtCTDGVatLieu.Text.HasValue())
                                            {
                                                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                            }

                                            if (txtCTDGNhanCong.Text.HasValue())
                                            {
                                                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                            }

                                            if (txtCTDGMay.Text.HasValue())
                                            {
                                                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                            }
                                            if (txtCTKhoiLuongDocVao.Text.HasValue())
                                            {
                                                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                                if (KLDV > 0)
                                                    dtRow["KhoiLuongDocVao"] = KLDV;
                                            }
                                            TDKH_GhiChuBoSungJson JsonGhiChuCT = new TDKH_GhiChuBoSungJson();
                                            JsonGhiChuCT.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                            if (txt_congtrinh_sttND.Text.HasValue())
                                            {
                                                JsonGhiChuCT.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                            }
                                            JsonGhiChuCT.CodeDanhMucCongTac = code;
                                            var encryptedStrCT = JsonConvert.SerializeObject(JsonGhiChuCT);
                                            dtRow["GhiChuBoSungJson"] = encryptedStrCT;
                                            double DonGia = 0;
                                            if (DonGiaHD.Text.HasValue())
                                            {
                                                DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                            }
                                            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = LamTron ? Math.Round(DonGia) : DonGia;
                                            dtRow["Modified"] = true;
                                            dtRow["PhatSinh"] = false;
                                            dtRow_congtactheogiadoan["Modified"] = true;
                                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                            dt.Rows.Add(dtRow);
                                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                            int countdg = 0;
                                            double DonGiaNT = 0;
                                            foreach (var item in NameTP)
                                            {
                                                double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                                if (KLCT == 0)
                                                    continue;
                                                if (!CodeNhomTP.Keys.Contains(item.Key))
                                                {
                                                    string CodeTP = Guid.NewGuid().ToString();
                                                    CodeNhomTP.Add(item.Key, CodeTP);
                                                    DataRow NewRowNhomTP = dt_NhomCT.NewRow();
                                                    NewRowNhomTP.ItemArray = NewRowNhom.ItemArray;
                                                    NewRowNhomTP["Code"] = CodeTP;
                                                    double.TryParse(ws.Rows[RowNhom][item.Key].Value.ToString(), out KLNhom);
                                                    if (KLNhom > 0)
                                                        NewRowNhomTP["KhoiLuongKeHoach"] = NewRowNhomTP["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                                    NewRowNhomTP["SortId"] = SortIdNhom++;
                                                    dt_NhomCT.Rows.Add(NewRowNhomTP);
                                                }
                                                DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                                dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                                dtRowNew["Code"] = Guid.NewGuid().ToString();
                                                dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                                dtRowNew["CodeNhaThauPhu"] = item.Value;
                                                dtRowNew["CodeNhom"] = CodeNhomTP[item.Key];
                                                dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                                dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                                if (IsThauPhuDG)
                                                {
                                                    double.TryParse(crRow[lstColDGTP[countdg++]].Value.ToString(), out DonGiaNT);
                                                    dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                                 = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                                }
                                                dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                            }
                                            countdg = 0;
                                            DonGiaNT = 0;
                                            foreach (var item in NameTD)
                                            {
                                                double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                                if (KLCT == 0)
                                                    continue;
                                                if (!CodeNhomTD.Keys.Contains(item.Key))
                                                {
                                                    string CodeTD = Guid.NewGuid().ToString();
                                                    CodeNhomTD.Add(item.Key, CodeTD);
                                                    DataRow NewRowNhomTD = dt_NhomCT.NewRow();
                                                    NewRowNhomTD.ItemArray = NewRowNhom.ItemArray;
                                                    NewRowNhomTD["Code"] = CodeTD;
                                                    double.TryParse(ws.Rows[RowNhom][item.Key].Value.ToString(), out KLNhom);
                                                    if (KLNhom > 0)
                                                        NewRowNhomTD["KhoiLuongKeHoach"] = NewRowNhomTD["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                                    NewRowNhomTD["SortId"] = SortIdNhom++;
                                                    dt_NhomCT.Rows.Add(NewRowNhomTD);
                                                }
                                                DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                                dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                                dtRowNew["Code"] = Guid.NewGuid().ToString();
                                                dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                                dtRowNew["CodeToDoi"] = item.Value;
                                                dtRowNew["CodeNhom"] = CodeNhomTD[item.Key];
                                                dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                                dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                                if (IsToDoiDG)
                                                {
                                                    double.TryParse(crRow[lstColDGTD[countdg++]].Value.ToString(), out DonGiaNT);
                                                    dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                                 = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                                }
                                                dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                            }
                                            if (ce_TuThucHien.Checked)
                                            {
                                                double.TryParse(crRow[te_TuThucHien.Text].Value.ToString(), out KLCT);
                                                if (KLCT == 0)
                                                    continue;
                                                if (NewNhomTTH)
                                                {
                                                    DataRow NewRowNhomTTH = dt_NhomCT.NewRow();
                                                    NewRowNhomTTH.ItemArray = NewRowNhom.ItemArray;
                                                    NewRowNhomTTH["Code"] = CodeNhomTTH;
                                                    double.TryParse(ws.Rows[RowNhom][te_TuThucHien.Text].Value.ToString(), out KLNhom);
                                                    if (KLNhom > 0)
                                                        NewRowNhomTTH["KhoiLuongKeHoach"] = NewRowNhomTTH["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                                    NewRowNhomTTH["SortId"] = SortIdNhom++;
                                                    dt_NhomCT.Rows.Add(NewRowNhomTTH);
                                                }
                                                NewNhomTTH = false;
                                                DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                                dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                                dtRowNew["Code"] = Guid.NewGuid().ToString();
                                                dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                                dtRowNew["CodeNhaThauPhu"] = CodeDVTTH;
                                                dtRowNew["CodeNhom"] = CodeNhomTTH;
                                                dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                                dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                                double DonGiaTTH = 0;
                                                if (te_DonGiaTTH.Text.HasValue())
                                                {
                                                    double.TryParse(crRow[te_DonGiaTTH.Text].Value.ToString(), out DonGiaTTH);
                                                    dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                                 = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaTTH, SaiSo) : DonGiaTTH;
                                                }
                                                dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                            }
                                        }
                                        else if (TT && !string.IsNullOrEmpty(tencongtac))
                                        {
                                            bool TryTT = double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                            if (TTDocVao > 0)
                                            {
                                                j = k;
                                                i = j;
                                                string MaHieu = "TRỐNG";
                                                string code = Guid.NewGuid().ToString();
                                                DataRow dtRow = dt.NewRow();
                                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                                dtRow["Code"] = code;
                                                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                                dtRow_congtactheogiadoan["SortId"] = SortId++;
                                                dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                                double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out KLCT);
                                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                                dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                                dtRow["CodeHangMuc"] = CodeHM;
                                                dtRow["MaHieuCongTac"] = MaHieu;
                                                dtRow["TenCongTac"] = tencongtac;
                                                dtRow["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                                dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                                dtRow["CodePhanTuyen"] = CodeTuyen;
                                                dtRow["ThanhTienDocVao"] = TTDocVao;
                                                if (!string.IsNullOrEmpty(CodeDDCT))
                                                {
                                                    dtRow["CodeDienDai"] = CodeDDCT;
                                                    CodeDDCT = string.Empty;
                                                }
                                                if (txtCTDGVatLieu.Text.HasValue())
                                                {
                                                    long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                                    dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                                }

                                                if (txtCTDGNhanCong.Text.HasValue())
                                                {
                                                    long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                                    dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                                }

                                                if (txtCTDGMay.Text.HasValue())
                                                {
                                                    long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                                    dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                                }
                                                if (txtCTKhoiLuongDocVao.Text.HasValue())
                                                {
                                                    double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                                    if (KLDV > 0)
                                                        dtRow["KhoiLuongDocVao"] = KLDV;
                                                }
                                                TDKH_GhiChuBoSungJson JsonGhiChuCT = new TDKH_GhiChuBoSungJson();
                                                JsonGhiChuCT.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                                if (txt_congtrinh_sttND.Text.HasValue())
                                                {
                                                    JsonGhiChuCT.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                                }
                                                JsonGhiChuCT.CodeDanhMucCongTac = code;
                                                var encryptedStrCT = JsonConvert.SerializeObject(JsonGhiChuCT);
                                                dtRow["GhiChuBoSungJson"] = encryptedStrCT;
                                                double DonGia = 0;
                                                if (DonGiaHD.Text.HasValue())
                                                {
                                                    DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                                }
                                                dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] = LamTron ? Math.Round(DonGia) : DonGia;
                                                dtRow["Modified"] = true;
                                                dtRow["PhatSinh"] = false;
                                                dtRow_congtactheogiadoan["Modified"] = true;
                                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                                dt.Rows.Add(dtRow);
                                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                                int countdg = 0;
                                                double DonGiaNT = 0;
                                                foreach (var item in NameTP)
                                                {
                                                    double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                                    if (KLCT == 0)
                                                        continue;
                                                    if (!CodeNhomTP.Keys.Contains(item.Key))
                                                    {
                                                        string CodeTP = Guid.NewGuid().ToString();
                                                        CodeNhomTP.Add(item.Key, CodeTP);
                                                        DataRow NewRowNhomTP = dt_NhomCT.NewRow();
                                                        NewRowNhomTP.ItemArray = NewRowNhom.ItemArray;
                                                        NewRowNhomTP["Code"] = CodeTP;
                                                        double.TryParse(ws.Rows[RowNhom][item.Key].Value.ToString(), out KLNhom);
                                                        if (KLNhom > 0)
                                                            NewRowNhomTP["KhoiLuongKeHoach"] = NewRowNhomTP["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                                        NewRowNhomTP["SortId"] = SortIdNhom++;
                                                        dt_NhomCT.Rows.Add(NewRowNhomTP);
                                                    }
                                                    DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                                    dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                                    dtRowNew["Code"] = Guid.NewGuid().ToString();
                                                    dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                                    dtRowNew["CodeNhaThauPhu"] = item.Value;
                                                    dtRowNew["CodeNhom"] = CodeNhomTP[item.Key];
                                                    dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                                    dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                                    if (IsThauPhuDG)
                                                    {
                                                        double.TryParse(crRow[lstColDGTP[countdg++]].Value.ToString(), out DonGiaNT);
                                                        dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                                     = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                                    }
                                                    dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                                }
                                                countdg = 0;
                                                DonGiaNT = 0;
                                                foreach (var item in NameTD)
                                                {
                                                    double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                                    if (KLCT == 0)
                                                        continue;
                                                    if (!CodeNhomTD.Keys.Contains(item.Key))
                                                    {
                                                        string CodeTD = Guid.NewGuid().ToString();
                                                        CodeNhomTD.Add(item.Key, CodeTD);
                                                        DataRow NewRowNhomTD = dt_NhomCT.NewRow();
                                                        NewRowNhomTD.ItemArray = NewRowNhom.ItemArray;
                                                        NewRowNhomTD["Code"] = CodeTD;
                                                        double.TryParse(ws.Rows[RowNhom][item.Key].Value.ToString(), out KLNhom);
                                                        if (KLNhom > 0)
                                                            NewRowNhomTD["KhoiLuongKeHoach"] = NewRowNhomTD["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                                        NewRowNhomTD["SortId"] = SortIdNhom++;
                                                        dt_NhomCT.Rows.Add(NewRowNhomTD);
                                                    }
                                                    DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                                    dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                                    dtRowNew["Code"] = Guid.NewGuid().ToString();
                                                    dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                                    dtRowNew["CodeToDoi"] = item.Value;
                                                    dtRowNew["CodeNhom"] = CodeNhomTD[item.Key];
                                                    dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                                    dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                                    if (IsToDoiDG)
                                                    {
                                                        double.TryParse(crRow[lstColDGTD[countdg++]].Value.ToString(), out DonGiaNT);
                                                        dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                                     = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                                    }
                                                    dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                                }
                                                if (ce_TuThucHien.Checked)
                                                {
                                                    double.TryParse(crRow[te_TuThucHien.Text].Value.ToString(), out KLCT);
                                                    if (KLCT == 0)
                                                        continue;
                                                    if (NewNhomTTH)
                                                    {
                                                        DataRow NewRowNhomTTH = dt_NhomCT.NewRow();
                                                        NewRowNhomTTH.ItemArray = NewRowNhom.ItemArray;
                                                        NewRowNhomTTH["Code"] = CodeNhomTTH;
                                                        double.TryParse(ws.Rows[RowNhom][te_TuThucHien.Text].Value.ToString(), out KLNhom);
                                                        if (KLNhom > 0)
                                                            NewRowNhomTTH["KhoiLuongKeHoach"] = NewRowNhomTTH["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                                        NewRowNhomTTH["SortId"] = SortIdNhom++;
                                                        dt_NhomCT.Rows.Add(NewRowNhomTTH);
                                                    }
                                                    NewNhomTTH = false;
                                                    DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                                    dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                                    dtRowNew["Code"] = Guid.NewGuid().ToString();
                                                    dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                                    dtRowNew["CodeNhaThauPhu"] = CodeDVTTH;
                                                    dtRowNew["CodeNhom"] = CodeNhomTTH;
                                                    dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                                    dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                                    double DonGiaTTH = 0;
                                                    if (te_DonGiaTTH.Text.HasValue())
                                                    {
                                                        double.TryParse(crRow[te_DonGiaTTH.Text].Value.ToString(), out DonGiaTTH);
                                                        dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                                     = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaTTH, SaiSo) : DonGiaTTH;
                                                    }
                                                    dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (count > 2)
                                {
                                    i = j;
                                    string MaHieu = txtCTMaDuToan.Text.HasValue() ? crRow[txtCTMaDuToan.Text].Value.ToString() : string.Empty;
                                    string code = Guid.NewGuid().ToString();
                                    DataRow dtRow = dt.NewRow();
                                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                    dtRow["Code"] = code;
                                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                    dtRow_congtactheogiadoan["SortId"] = SortId++;
                                    dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out  KLCT);
                                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                    dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                    dtRow["CodeHangMuc"] = CodeHM;
                                    dtRow["MaHieuCongTac"] = string.IsNullOrEmpty(MaHieu) ? "TT" : MaHieu;
                                    dtRow["TenCongTac"] = tencongtac;
                                    dtRow["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                    dtRow["CodePhanTuyen"] = CodeTuyen;
                                    if (TT)
                                    {
                                        double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                        dtRow["ThanhTienDocVao"] = TTDocVao;
                                    }
                                    if (!string.IsNullOrEmpty(CodeDDCT))
                                    {
                                        dtRow["CodeDienDai"] = CodeDDCT;
                                        CodeDDCT = string.Empty;
                                    }
                                    if (txtCTDGVatLieu.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                    }

                                    if (txtCTDGNhanCong.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                    }

                                    if (txtCTDGMay.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                    }
                                    if (txtCTKhoiLuongDocVao.Text.HasValue())
                                    {
                                        double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                        if (KLDV > 0)
                                            dtRow["KhoiLuongDocVao"] = KLDV;
                                    }
                                    TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                    JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                    if (txt_congtrinh_sttND.Text.HasValue())
                                    {
                                        JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                    }
                                    JsonGhiChu.CodeDanhMucCongTac = code;
                                    var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                    dtRow["GhiChuBoSungJson"] = encryptedStr;
                                    double DonGia = 0;
                                    if (DonGiaHD.Text.HasValue())
                                    {
                                        DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                    }
                                    dtRow_congtactheogiadoan["DonGia"]  = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                           = dtRow_congtactheogiadoan["DonGiaDuThau"] = LamTron ? Math.Round(DonGia) : DonGia;
                                    dtRow["Modified"] = true;
                                    dtRow["PhatSinh"] = false;
                                    dtRow_congtactheogiadoan["Modified"] = true;
                                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dt.Rows.Add(dtRow);
                                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                    int countdg = 0;
                                    double DonGiaNT = 0;
                                    foreach (var item in NameTP)
                                    {
                                        double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                        if (KLCT == 0)
                                            continue;
                                        DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                        dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                        dtRowNew["Code"] = Guid.NewGuid().ToString();
                                        dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                        dtRowNew["CodeNhaThauPhu"] = item.Value;
                                        dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                        dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                        if (IsThauPhuDG)
                                        {
                                            double.TryParse(crRow[lstColDGTP[countdg++]].Value.ToString(), out DonGiaNT);
                                            dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                         = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                        }
                                        dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                    }
                                    countdg = 0;
                                    DonGiaNT = 0;
                                    foreach (var item in NameTD)
                                    {
                                        double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                        if (KLCT == 0)
                                            continue;
                                        DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                        dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                        dtRowNew["Code"] = Guid.NewGuid().ToString();
                                        dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                        dtRowNew["CodeToDoi"] = item.Value;
                                        dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                        dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                        if (IsToDoiDG)
                                        {
                                            double.TryParse(crRow[lstColDGTD[countdg++]].Value.ToString(), out DonGiaNT);
                                            dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                         = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                        }
                                        dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                    }
                                    if (ce_TuThucHien.Checked)
                                    {
                                        double.TryParse(crRow[te_TuThucHien.Text].Value.ToString(), out KLCT);
                                        if (KLCT == 0)
                                            continue;
                                        DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                        dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                        dtRowNew["Code"] = Guid.NewGuid().ToString();
                                        dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                        dtRowNew["CodeNhaThauPhu"] = CodeDVTTH;
                                        dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                        dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                        double DonGiaTTH = 0;
                                        if (te_DonGiaTTH.Text.HasValue())
                                        {
                                            double.TryParse(crRow[te_DonGiaTTH.Text].Value.ToString(), out DonGiaTTH);
                                            dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                         = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaTTH, SaiSo) : DonGiaTTH;
                                        }
                                        dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                    }
                                }
                                else if (TT && !string.IsNullOrEmpty(tencongtac))
                                {
                                    bool TryTT = double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                    if (TTDocVao > 0)
                                    {
                                        i = j;
                                        string MaHieu = "TRỐNG";
                                        string code = Guid.NewGuid().ToString();
                                        DataRow dtRow = dt.NewRow();
                                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                        dtRow["Code"] = code;
                                        dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                                        dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                        double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out KLCT);
                                        dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                        dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                        dtRow["CodeHangMuc"] = CodeHM;
                                        dtRow["MaHieuCongTac"] = MaHieu;
                                        dtRow["TenCongTac"] = tencongtac;
                                        dtRow["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                        dtRow["CodePhanTuyen"] = CodeTuyen;
                                        dtRow["ThanhTienDocVao"] = TTDocVao;
                                        if (!string.IsNullOrEmpty(CodeDDCT))
                                        {
                                            dtRow["CodeDienDai"] = CodeDDCT;
                                            CodeDDCT = string.Empty;
                                        }
                                        if (txtCTDGVatLieu.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                        }

                                        if (txtCTDGNhanCong.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                        }

                                        if (txtCTDGMay.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                        }
                                        if (txtCTKhoiLuongDocVao.Text.HasValue())
                                        {
                                            double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                            if (KLDV > 0)
                                                dtRow["KhoiLuongDocVao"] = KLDV;
                                        }
                                        TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                        JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                        if (txt_congtrinh_sttND.Text.HasValue())
                                        {
                                            JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                        }
                                        JsonGhiChu.CodeDanhMucCongTac = code;
                                        var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                        dtRow["GhiChuBoSungJson"] = encryptedStr;
                                        double DonGia = 0;
                                        if (DonGiaHD.Text.HasValue())
                                        {
                                            DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                        }
                                        dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                               = dtRow_congtactheogiadoan["DonGiaDuThau"] = LamTron ? Math.Round(DonGia) : DonGia;
                                        dtRow["Modified"] = true;
                                        dtRow["PhatSinh"] = false;
                                        dtRow_congtactheogiadoan["Modified"] = true;
                                        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dt.Rows.Add(dtRow);
                                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                        int countdg = 0;
                                        double DonGiaNT = 0;
                                        foreach (var item in NameTP)
                                        {
                                            double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                            if (KLCT == 0)
                                                continue;
                                            DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                            dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                            dtRowNew["Code"] = Guid.NewGuid().ToString();
                                            dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                            dtRowNew["CodeNhaThauPhu"] = item.Value;
                                            dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                            dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                            if (IsThauPhuDG)
                                            {
                                                double.TryParse(crRow[lstColDGTP[countdg++]].Value.ToString(), out DonGiaNT);
                                                dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                             = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                            }
                                            dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                        }
                                        countdg = 0;
                                        DonGiaNT = 0;
                                        foreach (var item in NameTD)
                                        {
                                            double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                            if (KLCT == 0)
                                                continue;
                                            DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                            dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                            dtRowNew["Code"] = Guid.NewGuid().ToString();
                                            dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                            dtRowNew["CodeToDoi"] = item.Value;
                                            dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                            dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                            if (IsToDoiDG)
                                            {
                                                double.TryParse(crRow[lstColDGTD[countdg++]].Value.ToString(), out DonGiaNT);
                                                dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                             = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                            }
                                            dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                        }
                                        if (ce_TuThucHien.Checked)
                                        {
                                            double.TryParse(crRow[te_TuThucHien.Text].Value.ToString(), out KLCT);
                                            if (KLCT == 0)
                                                continue;
                                            DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                            dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                            dtRowNew["Code"] = Guid.NewGuid().ToString();
                                            dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                            dtRowNew["CodeNhaThauPhu"] = CodeDVTTH;
                                            dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                            dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                            double DonGiaTTH = 0;
                                            if (te_DonGiaTTH.Text.HasValue())
                                            {
                                                double.TryParse(crRow[te_DonGiaTTH.Text].Value.ToString(), out DonGiaTTH);
                                                dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                             = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaTTH, SaiSo) : DonGiaTTH;
                                            }
                                            dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                        }
                                    }
                                }
                            }
                        }
                        else if (count == 2)
                        {
                            if (string.IsNullOrEmpty(CodeHM))
                            {
                                if (string.IsNullOrEmpty(CodeCtrinh) && Index == 0)
                                {
                                    CodeCtrinh = Guid.NewGuid().ToString();
                                    DataRow CrowCTrinh = dt_CTrinh.NewRow();
                                    CrowCTrinh["Code"] = CodeCtrinh;
                                    CrowCTrinh["CodeDuAn"] = m_CodeDuAn;
                                    CrowCTrinh["SortID"] = SortIDCT++;
                                    CrowCTrinh["Ten"] = $"Công trình {dt_CTrinh.Rows.Count + 1}";
                                    dt_CTrinh.Rows.Add(CrowCTrinh);
                                }
                                CodeHM = Guid.NewGuid().ToString();
                                DataRow CrowHM = dt_HM.NewRow();
                                CrowHM["Code"] = CodeHM;
                                CrowHM["SortID"] = SortIDHM++;
                                CrowHM["CodeCongTrinh"] = CodeCtrinh;
                                CrowHM["Ten"] = $"Hạng mục {dt_HM.Rows.Count}";
                                dt_HM.Rows.Add(CrowHM);
                            }
                            //string tennhom = crRow[txtCTTenCongTac.Text].Value.ToString();
                            DataRow NewRow = dt_NhomCT.NewRow();
                            string CodeNhomCT = Guid.NewGuid().ToString();
                            NewRow["Code"] = CodeNhomCT;
                            NewRow["CodeHangMuc"] = CodeHM;
                            NewRow["Ten"] = TenCongTac;
                            NewRow["DonVi"] = crRow[txtCTDonVi.Text].Value.TextValue;
                            if (!string.IsNullOrEmpty(CodeDDNhom))
                            {
                                NewRow["CodeDienDai"] = CodeDDNhom;
                                CodeDDNhom = string.Empty;
                            }
                            RowNhom = i;
                            if (Te_DonGiaNhom.Text.HasValue())
                            {
                                double.TryParse(crRow[Te_DonGiaNhom.Text].Value.ToString(), out double DonGiaNhom);
                                if (DonGiaNhom > 0)
                                    NewRow["DonGia"] =NewRow["DonGiaThiCong"] = LamTron ? Math.Round(DonGiaNhom, SaiSo) : DonGiaNhom;
                            }
                            double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out  KLNhom);
                            if (KLNhom > 0)
                                NewRow["KhoiLuongKeHoach"] = NewRow["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;

                            NewRow["SortId"] = SortIdNhom++;
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = CodeNhomCT;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            NewRow["GhiChuBoSungJson"] = encryptedStr;
                            dt_NhomCT.Rows.Add(NewRow);
                            bool /*NewNhomTP = true, NewNhomTD = true,*/NewNhomTTH = true;
                            Dictionary<string, string> CodeNhomTP = new Dictionary<string, string>();
                            Dictionary<string, string> CodeNhomTD = new Dictionary<string, string>();
                            //string CodeNhomTP = Guid.NewGuid().ToString();
                            //string CodeNhomTD = Guid.NewGuid().ToString();
                            string CodeNhomTTH = Guid.NewGuid().ToString();
                            for (int j = i + 1; j <= lastLine; j++)
                            {
                                crRow = ws.Rows[j];
                                _STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                int.TryParse(_STT, out STT);
                                count = _STT.Count(x => x == '.');
                                string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
                                if (STT > 0 || count <= 2 && count > 0 || _STT == Te_DDNhom.Text||
                                    _STT.ToUpper() == MyConstant.CONST_TYPE_CONGTRINH || _STT.ToUpper() == MyConstant.TYPEROW_HangMuc)
                                {
                                    i = j - 1;
                                    break;
                                }
                                else if (_STT == Te_DDCT.Text)
                                {
                                    CodeDDCT = Guid.NewGuid().ToString();
                                    DataRow CrowDD = dt_TenDienDaiTuDo.NewRow();
                                    CrowDD["Code"] = CodeDDCT;
                                    CrowDD["CodeDuAn"] = m_CodeDuAn;
                                    CrowDD["Ten"] = crRow[txtCTTenCongTac.Text].Value.ToString();
                                    dt_TenDienDaiTuDo.Rows.Add(CrowDD);
                                }
                                else if (count > 2)
                                {
                                    i = j;
                                    string MaHieu = txtCTMaDuToan.Text.HasValue() ? crRow[txtCTMaDuToan.Text].Value.ToString() : string.Empty;
                                    string code = Guid.NewGuid().ToString();
                                    DataRow dtRow = dt.NewRow();
                                    DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                    dtRow["Code"] = code;                              
                                    dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                    dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                    dtRow_congtactheogiadoan["SortId"] = SortId++;
                                    dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                    dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                    double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out  KLCT);
                                    dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                    dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                    dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                    dtRow["CodeHangMuc"]  = CodeHM;
                                    dtRow["MaHieuCongTac"]  = string.IsNullOrEmpty(MaHieu) ? "TT" : MaHieu;
                                    dtRow["TenCongTac"]  = tencongtac;
                                    dtRow["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                    if (TT)
                                    {
                                        double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                        dtRow["ThanhTienDocVao"] = TTDocVao;
                                    }
                                    if (!string.IsNullOrEmpty(CodeDDCT))
                                    {
                                        dtRow["CodeDienDai"] = CodeDDCT;
                                        CodeDDCT = string.Empty;
                                    }
                                    dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                    if (txtCTDGVatLieu.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                    }

                                    if (txtCTDGNhanCong.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                    }

                                    if (txtCTDGMay.Text.HasValue())
                                    {
                                        long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                        dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                    }
                                    if (txtCTKhoiLuongDocVao.Text.HasValue())
                                    {
                                        double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                        if (KLDV > 0)
                                            dtRow["KhoiLuongDocVao"] = KLDV;
                                    }
                                    TDKH_GhiChuBoSungJson JsonGhiChuCT = new TDKH_GhiChuBoSungJson();
                                    JsonGhiChuCT.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                    if (txt_congtrinh_sttND.Text.HasValue())
                                    {
                                        JsonGhiChuCT.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                    }
                                    JsonGhiChuCT.CodeDanhMucCongTac = code;
                                    var encryptedStrCT = JsonConvert.SerializeObject(JsonGhiChuCT);
                                    dtRow["GhiChuBoSungJson"] = encryptedStrCT;
                                    double DonGia = 0;
                                    if (DonGiaHD.Text.HasValue())
                                    {
                                        DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                    }
                                    dtRow_congtactheogiadoan["DonGia"]  = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                           = dtRow_congtactheogiadoan["DonGiaDuThau"] = LamTron ? Math.Round(DonGia) : DonGia;
                                    dtRow["Modified"] = true;
                                    dtRow["PhatSinh"] = false;
                                    dtRow_congtactheogiadoan["Modified"] = true;
                                    dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                    dt.Rows.Add(dtRow);
                                    dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                    int countdg = 0;
                                    double DonGiaNT = 0;
                                    foreach (var item in NameTP)
                                    {
                                        double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                        if (KLCT == 0)
                                            continue;
                                        if (!CodeNhomTP.Keys.Contains(item.Key))
                                        {
                                            string CodeTP = Guid.NewGuid().ToString();
                                            CodeNhomTP.Add(item.Key, CodeTP);
                                            DataRow NewRowNhomTP = dt_NhomCT.NewRow();
                                            NewRowNhomTP.ItemArray = NewRow.ItemArray;
                                            NewRowNhomTP["Code"] = CodeTP;
                                            double.TryParse(ws.Rows[RowNhom][item.Key].Value.ToString(), out KLNhom);
                                            if (KLNhom > 0)
                                                NewRowNhomTP["KhoiLuongKeHoach"] = NewRowNhomTP["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                            NewRowNhomTP["SortId"] = SortIdNhom++;
                                            dt_NhomCT.Rows.Add(NewRowNhomTP);
                                        }
                                        DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                        dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                        dtRowNew["Code"] = Guid.NewGuid().ToString();
                                        dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                        dtRowNew["CodeNhaThauPhu"] = item.Value;
                                        dtRowNew["CodeNhom"] = CodeNhomTP[item.Key];
                                        dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                        dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                        if (IsThauPhuDG)
                                        {
                                            double.TryParse(crRow[lstColDGTP[countdg++]].Value.ToString(), out DonGiaNT);
                                            dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                         = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                        }
                                        dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                    }
                                    countdg = 0;
                                    DonGiaNT = 0;
                                    foreach (var item in NameTD)
                                    {
                                        double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                        if (KLCT == 0)
                                            continue;
                                        if (!CodeNhomTD.Keys.Contains(item.Key))
                                        {
                                            string CodeTD = Guid.NewGuid().ToString();
                                            CodeNhomTD.Add(item.Key, CodeTD);
                                            DataRow NewRowNhomTD = dt_NhomCT.NewRow();
                                            NewRowNhomTD.ItemArray = NewRow.ItemArray;
                                            NewRowNhomTD["Code"] = CodeTD;
                                            double.TryParse(ws.Rows[RowNhom][item.Key].Value.ToString(), out KLNhom);
                                            if (KLNhom > 0)
                                                NewRowNhomTD["KhoiLuongKeHoach"] = NewRowNhomTD["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                            NewRowNhomTD["SortId"] = SortIdNhom++;
                                            dt_NhomCT.Rows.Add(NewRowNhomTD);
                                        }
                                        DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                        dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                        dtRowNew["Code"] = Guid.NewGuid().ToString();
                                        dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                        dtRowNew["CodeToDoi"] = item.Value;
                                        dtRowNew["CodeNhom"] = CodeNhomTD[item.Key];
                                        dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                        dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                        if (IsToDoiDG)
                                        {
                                            double.TryParse(crRow[lstColDGTD[countdg++]].Value.ToString(), out DonGiaNT);
                                            dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                         = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                        }
                                        dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                    }
                                    if (ce_TuThucHien.Checked)
                                    {
                                        double.TryParse(crRow[te_TuThucHien.Text].Value.ToString(), out KLCT);
                                        if (KLCT == 0)
                                            continue;
                                        if (NewNhomTTH)
                                        {
                                            DataRow NewRowNhomTTH = dt_NhomCT.NewRow();
                                            NewRowNhomTTH.ItemArray = NewRow.ItemArray;
                                            NewRowNhomTTH["Code"] = CodeNhomTTH;
                                            double.TryParse(ws.Rows[RowNhom][te_TuThucHien.Text].Value.ToString(), out KLNhom);
                                            if (KLNhom > 0)
                                                NewRowNhomTTH["KhoiLuongKeHoach"] = NewRowNhomTTH["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                            NewRowNhomTTH["SortId"] = SortIdNhom++;
                                            dt_NhomCT.Rows.Add(NewRowNhomTTH);
                                        }
                                        NewNhomTTH = false;
                                        DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                        dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                        dtRowNew["Code"] = Guid.NewGuid().ToString();
                                        dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                        dtRowNew["CodeNhaThauPhu"] = CodeDVTTH;
                                        dtRowNew["CodeNhom"] = CodeNhomTTH;
                                        dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                        dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                        double DonGiaTTH = 0;
                                        if (te_DonGiaTTH.Text.HasValue())
                                        {
                                            double.TryParse(crRow[te_DonGiaTTH.Text].Value.ToString(), out DonGiaTTH);
                                            dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                         = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaTTH, SaiSo) : DonGiaTTH;
                                        }
                                        dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                    }
                                }
                                else if (TT && !string.IsNullOrEmpty(tencongtac))
                                {
                                    bool TryTT = double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                    if (TTDocVao > 0)
                                    {
                                        i = j;
                                        string MaHieu = "TRỐNG";
                                        string code = Guid.NewGuid().ToString();
                                        DataRow dtRow = dt.NewRow();
                                        DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                        dtRow["Code"] = code;
                                        dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                        dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                        dtRow_congtactheogiadoan["SortId"] = SortId++;
                                        dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                        dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                        double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out KLCT);
                                        dtRow_congtactheogiadoan["KhoiLuongToanBo"] = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                        dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                        dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                        dtRow["CodeHangMuc"] = CodeHM;
                                        dtRow["MaHieuCongTac"] = MaHieu;
                                        dtRow["TenCongTac"] = tencongtac;
                                        dtRow["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                        dtRow["ThanhTienDocVao"] = TTDocVao;
                                        if (!string.IsNullOrEmpty(CodeDDCT))
                                        {
                                            dtRow["CodeDienDai"] = CodeDDCT;
                                            CodeDDCT = string.Empty;
                                        }
                                        dtRow_congtactheogiadoan["CodeNhom"] = CodeNhomCT;
                                        if (txtCTDGVatLieu.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                        }

                                        if (txtCTDGNhanCong.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                        }

                                        if (txtCTDGMay.Text.HasValue())
                                        {
                                            long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                            dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                        }
                                        if (txtCTKhoiLuongDocVao.Text.HasValue())
                                        {
                                            double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                            if (KLDV > 0)
                                                dtRow["KhoiLuongDocVao"] = KLDV;
                                        }
                                        TDKH_GhiChuBoSungJson JsonGhiChuCT = new TDKH_GhiChuBoSungJson();
                                        JsonGhiChuCT.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                        if (txt_congtrinh_sttND.Text.HasValue())
                                        {
                                            JsonGhiChuCT.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                        }
                                        JsonGhiChuCT.CodeDanhMucCongTac = code;
                                        var encryptedStrCT = JsonConvert.SerializeObject(JsonGhiChuCT);
                                        dtRow["GhiChuBoSungJson"] = encryptedStrCT;
                                        double DonGia = 0;
                                        if (DonGiaHD.Text.HasValue())
                                        {
                                            DonGia = crRow[DonGiaHD.Text].Value.NumericValue;
                                        }
                                        dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                               = dtRow_congtactheogiadoan["DonGiaDuThau"] = LamTron ? Math.Round(DonGia) : DonGia;
                                        dtRow["Modified"] = true;
                                        dtRow["PhatSinh"] = false;
                                        dtRow_congtactheogiadoan["Modified"] = true;
                                        dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                        dt.Rows.Add(dtRow);
                                        dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                        int countdg = 0;
                                        double DonGiaNT = 0;
                                        foreach (var item in NameTP)
                                        {
                                            double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                            if (KLCT == 0)
                                                continue;
                                            if (!CodeNhomTP.Keys.Contains(item.Key))
                                            {
                                                string CodeTP = Guid.NewGuid().ToString();
                                                CodeNhomTP.Add(item.Key, CodeTP);
                                                DataRow NewRowNhomTP = dt_NhomCT.NewRow();
                                                NewRowNhomTP.ItemArray = NewRow.ItemArray;
                                                NewRowNhomTP["Code"] = CodeTP;
                                                double.TryParse(ws.Rows[RowNhom][item.Key].Value.ToString(), out KLNhom);
                                                if (KLNhom > 0)
                                                    NewRowNhomTP["KhoiLuongKeHoach"] = NewRowNhomTP["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                                NewRowNhomTP["SortId"] = SortIdNhom++;
                                                dt_NhomCT.Rows.Add(NewRowNhomTP);
                                            }
                                            DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                            dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                            dtRowNew["Code"] = Guid.NewGuid().ToString();
                                            dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                            dtRowNew["CodeNhaThauPhu"] = item.Value;
                                            dtRowNew["CodeNhom"] = CodeNhomTP[item.Key];
                                            dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                            dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                            if (IsThauPhuDG)
                                            {
                                                double.TryParse(crRow[lstColDGTP[countdg++]].Value.ToString(), out DonGiaNT);
                                                dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                             = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                            }
                                            dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                        }
                                        countdg = 0;
                                        DonGiaNT = 0;
                                        foreach (var item in NameTD)
                                        {
                                            double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                            if (KLCT == 0)
                                                continue;
                                            if (!CodeNhomTD.Keys.Contains(item.Key))
                                            {
                                                string CodeTD = Guid.NewGuid().ToString();
                                                CodeNhomTD.Add(item.Key, CodeTD);
                                                DataRow NewRowNhomTD = dt_NhomCT.NewRow();
                                                NewRowNhomTD.ItemArray = NewRow.ItemArray;
                                                NewRowNhomTD["Code"] = CodeTD;
                                                double.TryParse(ws.Rows[RowNhom][item.Key].Value.ToString(), out KLNhom);
                                                if (KLNhom > 0)
                                                    NewRowNhomTD["KhoiLuongKeHoach"] = NewRowNhomTD["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                                NewRowNhomTD["SortId"] = SortIdNhom++;
                                                dt_NhomCT.Rows.Add(NewRowNhomTD);
                                            }
                                            DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                            dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                            dtRowNew["Code"] = Guid.NewGuid().ToString();
                                            dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                            dtRowNew["CodeToDoi"] = item.Value;
                                            dtRowNew["CodeNhom"] = CodeNhomTD[item.Key];
                                            dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                            dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                            if (IsToDoiDG)
                                            {
                                                double.TryParse(crRow[lstColDGTD[countdg++]].Value.ToString(), out DonGiaNT);
                                                dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                             = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                            }
                                            dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                        }
                                        if (ce_TuThucHien.Checked)
                                        {
                                            double.TryParse(crRow[te_TuThucHien.Text].Value.ToString(), out KLCT);
                                            if (KLCT == 0)
                                                continue;
                                            if (NewNhomTTH)
                                            {
                                                DataRow NewRowNhomTTH = dt_NhomCT.NewRow();
                                                NewRowNhomTTH.ItemArray = NewRow.ItemArray;
                                                NewRowNhomTTH["Code"] = CodeNhomTTH;
                                                double.TryParse(ws.Rows[RowNhom][te_TuThucHien.Text].Value.ToString(), out KLNhom);
                                                if (KLNhom > 0)
                                                    NewRowNhomTTH["KhoiLuongKeHoach"] = NewRowNhomTTH["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLNhom, SaiSo) : KLNhom;
                                                NewRowNhomTTH["SortId"] = SortIdNhom++;
                                                dt_NhomCT.Rows.Add(NewRowNhomTTH);
                                            }
                                            NewNhomTTH = false;
                                            DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                            dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                            dtRowNew["Code"] = Guid.NewGuid().ToString();
                                            dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                            dtRowNew["CodeNhaThauPhu"] = CodeDVTTH;
                                            dtRowNew["CodeNhom"] = CodeNhomTTH;
                                            dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                            dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                            double DonGiaTTH = 0;
                                            if (te_DonGiaTTH.Text.HasValue())
                                            {
                                                double.TryParse(crRow[te_DonGiaTTH.Text].Value.ToString(), out DonGiaTTH);
                                                dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                             = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaTTH, SaiSo) : DonGiaTTH;
                                            }
                                            dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                        }
                                    }
                                }
                            }
                        }
                        else if (count > 2)
                        {
                            //string tencongtac = crRow[txtCTTenCongTac.Text].Value.ToString();
                            string MaHieu = txtCTMaDuToan.Text.HasValue() ? crRow[txtCTMaDuToan.Text].Value.ToString() : string.Empty;
                            string code = Guid.NewGuid().ToString();

                            DataRow dtRow = dt.NewRow();
                            DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                            dtRow["Code"] = code;
                            dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                            dtRow_congtactheogiadoan["CodeCongTac"] = code;
                            dtRow_congtactheogiadoan["SortId"] = SortId++;
                            dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                            dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                            double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out  KLCT);
                            dtRow_congtactheogiadoan["KhoiLuongToanBo"]  = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                            dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                            dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                            dtRow["CodeHangMuc"]  = CodeHM;
                            dtRow["MaHieuCongTac"] = string.IsNullOrEmpty(MaHieu) ? "TT" : MaHieu;
                            dtRow["TenCongTac"]  = TenCongTac;
                            dtRow["DonVi"]  = crRow[txtCTDonVi.Text].Value.ToString();
                            if (TT)
                            {
                                double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                                dtRow["ThanhTienDocVao"] = TTDocVao;
                            }
                            if (!string.IsNullOrEmpty(CodeDDCT))
                            {
                                dtRow["CodeDienDai"] = CodeDDCT;
                                CodeDDCT = string.Empty;
                            }
                            if (txtCTDGVatLieu.Text.HasValue())
                            {
                                long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                            }

                            if (txtCTDGNhanCong.Text.HasValue())
                            {
                                long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                            }

                            if (txtCTDGMay.Text.HasValue())
                            {
                                long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                            }
                            if (txtCTKhoiLuongDocVao.Text.HasValue())
                            {
                                double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                if (KLDV > 0)
                                    dtRow["KhoiLuongDocVao"] = KLDV;
                            }
                            TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                            JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                            if (txt_congtrinh_sttND.Text.HasValue())
                            {
                                JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                            }
                            JsonGhiChu.CodeDanhMucCongTac = code;
                            var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                            dtRow["GhiChuBoSungJson"] = encryptedStr;
                            double DonGia = 0;
                            if (DonGiaHD.Text.HasValue())
                            {
                                double.TryParse(crRow[DonGiaHD.Text].Value.ToString(), out DonGia);
                            }
                            dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                   = dtRow_congtactheogiadoan["DonGiaDuThau"] = LamTron ? Math.Round(DonGia) : DonGia;
                            dtRow["Modified"] = true;
                            dtRow["PhatSinh"] = false;
                            dtRow_congtactheogiadoan["Modified"] = true;
                            dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            dt.Rows.Add(dtRow);
                            dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                            int countdg = 0;
                            double DonGiaNT = 0;
                            foreach (var item in NameTP)
                            {
                                double.TryParse(crRow[item.Key].Value.ToString(), out  KLCT);
                                if (KLCT == 0)
                                    continue;
                                DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                dtRowNew["Code"] = Guid.NewGuid().ToString();
                                dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                dtRowNew["CodeNhaThauPhu"] = item.Value;
                                dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                if (IsThauPhuDG)
                                {
                                    double.TryParse(crRow[lstColDGTP[countdg++]].Value.ToString(), out DonGiaNT);
                                    dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                 = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                }
                                dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                            }
                            countdg = 0;
                            DonGiaNT = 0;
                            foreach (var item in NameTD)
                            {
                                double.TryParse(crRow[item.Key].Value.ToString(), out  KLCT);
                                if (KLCT == 0)
                                    continue;
                                DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                dtRowNew["Code"] = Guid.NewGuid().ToString();
                                dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                dtRowNew["CodeToDoi"] = item.Value;
                                dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                if (IsToDoiDG)
                                {
                                    double.TryParse(crRow[lstColDGTD[countdg++]].Value.ToString(), out DonGiaNT);
                                    dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                 = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                }
                                dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                            }
                            if (ce_TuThucHien.Checked)
                            {
                                double.TryParse(crRow[te_TuThucHien.Text].Value.ToString(), out KLCT);
                                if (KLCT == 0)
                                    continue;
                                DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                dtRowNew["Code"] = Guid.NewGuid().ToString();
                                dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                dtRowNew["CodeNhaThauPhu"] = CodeDVTTH;
                                dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                double DonGiaTTH = 0;
                                if (te_DonGiaTTH.Text.HasValue())
                                {
                                    double.TryParse(crRow[te_DonGiaTTH.Text].Value.ToString(), out DonGiaTTH);
                                    dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                 = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaTTH, SaiSo) : DonGiaTTH;
                                }
                                dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                            }
                        }
                        else if (TT && !string.IsNullOrEmpty(TenCongTac))
                        {
                            bool TryTT = double.TryParse(crRow[te_TTDocVao.Text].Value.ToString(), out TTDocVao);
                            if (TTDocVao > 0)
                            {
                                string MaHieu = "TRỐNG";
                                string code = Guid.NewGuid().ToString();

                                DataRow dtRow = dt.NewRow();
                                DataRow dtRow_congtactheogiadoan = dt_congtactheogiaidoan.NewRow();
                                dtRow["Code"] = code;
                                dtRow_congtactheogiadoan["Code"] = Guid.NewGuid().ToString();
                                dtRow_congtactheogiadoan["CodeCongTac"] = code;
                                dtRow_congtactheogiadoan["SortId"] = SortId++;
                                dtRow_congtactheogiadoan["LyTrinhCaoDo"] = Te_LyTrinhCaoDo.Text.HasValue() ? crRow[Te_LyTrinhCaoDo.Text].Value.ToString() : null;
                                dtRow_congtactheogiadoan["CodeNhaThau"] = m_codenhathau;
                                double.TryParse(crRow[txtCTKhoiLuong.Text].Value.ToString(), out KLCT);
                                dtRow_congtactheogiadoan["KhoiLuongToanBo"] 
                                    = dtRow_congtactheogiadoan["KhoiLuongHopDongChiTiet"] =LamTron?Math.Round(KLCT,SaiSo): KLCT;
                                dtRow_congtactheogiadoan["RowDoBoc"] = i + 1;
                                dtRow_congtactheogiadoan["CodeGiaiDoan"] = m_codegiadoan;
                                dtRow["CodeHangMuc"] = CodeHM;
                                dtRow["MaHieuCongTac"] = MaHieu;
                                dtRow["TenCongTac"] = TenCongTac;
                                dtRow["DonVi"] = crRow[txtCTDonVi.Text].Value.ToString();
                                dtRow["ThanhTienDocVao"] = TTDocVao;
                                if (!string.IsNullOrEmpty(CodeDDCT))
                                {
                                    dtRow["CodeDienDai"] = CodeDDCT;
                                    CodeDDCT = string.Empty;
                                }
                                if (txtCTDGVatLieu.Text.HasValue())
                                {
                                    long.TryParse(crRow[txtCTDGVatLieu.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaVatLieuDocVao"] = dg;
                                }

                                if (txtCTDGNhanCong.Text.HasValue())
                                {
                                    long.TryParse(crRow[txtCTDGNhanCong.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaNhanCongDocVao"] = dg;
                                }

                                if (txtCTDGMay.Text.HasValue())
                                {
                                    long.TryParse(crRow[txtCTDGMay.Text].Value.ToString(), out long dg);
                                    dtRow_congtactheogiadoan["DonGiaMayDocVao"] = dg;
                                }
                                if (txtCTKhoiLuongDocVao.Text.HasValue())
                                {
                                    double.TryParse(crRow[txtCTKhoiLuongDocVao.Text].Value.ToString(), out double KLDV);
                                    if (KLDV > 0)
                                        dtRow["KhoiLuongDocVao"] = KLDV;
                                }
                                TDKH_GhiChuBoSungJson JsonGhiChu = new TDKH_GhiChuBoSungJson();
                                JsonGhiChu.STT = crRow[txt_congtrinh_stt.Text].Value.ToString();
                                if (txt_congtrinh_sttND.Text.HasValue())
                                {
                                    JsonGhiChu.STTND = crRow[txt_congtrinh_sttND.Text].Value.ToString();
                                }
                                JsonGhiChu.CodeDanhMucCongTac = code;
                                var encryptedStr = JsonConvert.SerializeObject(JsonGhiChu);
                                dtRow["GhiChuBoSungJson"] = encryptedStr;
                                double DonGia = 0;
                                if (DonGiaHD.Text.HasValue())
                                {
                                    double.TryParse(crRow[DonGiaHD.Text].Value.ToString(), out DonGia);
                                }
                                dtRow_congtactheogiadoan["DonGia"] = dtRow_congtactheogiadoan["DonGiaThiCong"]
                                       = dtRow_congtactheogiadoan["DonGiaDuThau"] =LamTron?Math.Round(DonGia): DonGia;
                                dtRow["Modified"] = true;
                                dtRow["PhatSinh"] = false;
                                dtRow_congtactheogiadoan["Modified"] = true;
                                dtRow_congtactheogiadoan["NgayBatDau"] = te_KHBegin.Text.HasValue() ? (crRow[te_KHBegin.Text].Value.IsDateTime ? crRow[te_KHBegin.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dtRow_congtactheogiadoan["NgayKetThuc"] = te_KHEnd.Text.HasValue() ? (crRow[te_KHEnd.Text].Value.IsDateTime ? crRow[te_KHEnd.Text].Value.DateTimeValue.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) : null) : De_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                                dt.Rows.Add(dtRow);
                                dt_congtactheogiaidoan.Rows.Add(dtRow_congtactheogiadoan);
                                int countdg = 0;
                                double DonGiaNT = 0;
                                foreach (var item in NameTP)
                                {
                                    double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                    if (KLCT == 0)
                                        continue;
                                    DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                    dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                    dtRowNew["Code"] = Guid.NewGuid().ToString();
                                    dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                    dtRowNew["CodeNhaThauPhu"] = item.Value;
                                    dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                    dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                    if (IsThauPhuDG)
                                    {
                                        double.TryParse(crRow[lstColDGTP[countdg++]].Value.ToString(), out DonGiaNT);
                                        dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                     = dtRowNew["DonGiaDuThau"] =LamTron?Math.Round(DonGiaNT,SaiSo): DonGiaNT;
                                    }
                                    dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                }
                                countdg = 0;
                                DonGiaNT = 0;
                                foreach (var item in NameTD)
                                {
                                    double.TryParse(crRow[item.Key].Value.ToString(), out KLCT);
                                    if (KLCT == 0)
                                        continue;
                                    DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                    dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                    dtRowNew["Code"] = Guid.NewGuid().ToString();
                                    dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                    dtRowNew["CodeToDoi"] = item.Value;
                                    dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT; 
                                    dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                    if (IsToDoiDG)
                                    {
                                        double.TryParse(crRow[lstColDGTD[countdg++]].Value.ToString(), out DonGiaNT);
                                        dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                     = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaNT, SaiSo) : DonGiaNT;
                                    }
                                    dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                }
                                if (ce_TuThucHien.Checked)
                                {
                                    double.TryParse(crRow[te_TuThucHien.Text].Value.ToString(), out KLCT);
                                    if (KLCT == 0)
                                        continue;
                                    DataRow dtRowNew = dt_congtactheogiaidoan.NewRow();
                                    dtRowNew.ItemArray = dtRow_congtactheogiadoan.ItemArray;
                                    dtRowNew["Code"] = Guid.NewGuid().ToString();
                                    dtRowNew["CodeNhaThau"] = dtRowNew["CodeNhaThauPhu"] = dtRowNew["CodeToDoi"] = null;
                                    dtRowNew["CodeNhaThauPhu"] = CodeDVTTH;
                                    dtRowNew["KhoiLuongToanBo"] = dtRowNew["KhoiLuongHopDongChiTiet"] = LamTron ? Math.Round(KLCT, SaiSo) : KLCT;
                                    dtRowNew["KhoiLuongToanBo_Iscongthucmacdinh"] = DBNull.Value;
                                    double DonGiaTTH = 0;
                                    if (te_DonGiaTTH.Text.HasValue())
                                    {
                                        double.TryParse(crRow[te_DonGiaTTH.Text].Value.ToString(), out DonGiaTTH);
                                        dtRowNew["DonGia"] = dtRowNew["DonGiaThiCong"]
                                     = dtRowNew["DonGiaDuThau"] = LamTron ? Math.Round(DonGiaTTH, SaiSo) : DonGiaTTH;
                                    }
                                    dt_congtactheogiaidoan.Rows.Add(dtRowNew);
                                }
                            }
                        }
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_CTrinh, MyConstant.TBL_THONGTINCONGTRINH);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_HM, MyConstant.TBL_THONGTINHANGMUC);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_TenDienDaiTuDo, TDKH.Tbl_TenDienDaiTuDo);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Tuyen, TDKH.Tbl_PhanTuyen);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_DanhMucCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomCT, TDKH.TBL_NhomCongTac);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_congtactheogiaidoan, TDKH.TBL_ChiTietCongTacTheoKy);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHaoPhi, TDKH.Tbl_HaoPhiVatTu);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_NhomDienDai, TDKH.TBL_NhomDienGiai);
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt_Chitietcongtaccon, TDKH.TBL_ChiTietCongTacCon);
                dt_NhomDienDai.Clear();
                dt_Chitietcongtaccon.Clear();
                dt_congtactheogiaidoan.Clear();
                dt.Clear();
                dt_NhomCT.Clear();
                dtHaoPhi.Clear();
                dt_TenDienDaiTuDo.Clear();
            }

            WaitFormHelper.CloseWaitForm();
            WaitFormHelper.ShowWaitForm("Đang phân tích lại khối lượng, Vui lòng chờ!");
            string[] lstcode = dt_congtactheogiaidoan.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(lstcode);
            DuAnHelper.Fcn_DeleteCongTrinh();
            Fcn_DeleteNhaThau();
            WaitFormHelper.CloseWaitForm();
            this.Close();

        }
        private void Fcn_DeleteNhaThau()
        {
            WaitFormHelper.ShowWaitForm("Đang xóa Nhà thầu, Tổ đội mặc định, Vui lòng chờ!");
            string queryStr = string.Empty;
            DataTable dttp = null;
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa Nhà thầu phụ mặc định không!!!!!!");
            if (rs == DialogResult.Yes)
            {
                queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINNHATHAUPHU} WHERE CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'" +
                    $" AND CodeTongThau IS NULL";
                dttp = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                if (dttp.Rows.Count > 1)
                {
                    DuAnHelper.DeleteDataRows(MyConstant.TBL_THONGTINNHATHAUPHU, new string[] { dttp.Rows[0]["Code"].ToString() });
                }
                else
                    MessageShower.ShowError("Đang chỉ có 1 nhà thầu phụ nên không thể xóa Nhà thầu phụ mặc định được!!!!!!!!!!");

                rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa Tổ đội mặc định không!!!!!!");
            }
            if (rs == DialogResult.No)
                goto Label;
            queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINTODOITHICONG} WHERE CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            dttp = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            if (dttp.Rows.Count > 1)
            {
                DuAnHelper.DeleteDataRows(MyConstant.TBL_THONGTINTODOITHICONG, new string[] { dttp.Rows[0]["Code"].ToString() });
            }
            else
                MessageShower.ShowError("Đang chỉ có 1 Tổ đội nên không thể xóa Tổ đội phụ mặc định được!!!!!!!!!!");
            Label:
            WaitFormHelper.CloseWaitForm();
        }
        private void sb_ReadExcel_Click(object sender, EventArgs e)
        {
            Fcn_TruyenDataExcelThuCongGT((int)nud_congtrinh_begin.Value - 1, (int)nud_congtrinh_end.Value - 1);

        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ChonLaiFile_Click(object sender, EventArgs e)
        {
            m_openFileDialog.DefaultExt = "xls";
            m_openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            m_openFileDialog.Title = "Chọn file Excel";
            DialogResult rs = m_openFileDialog.ShowDialog();
            if (rs == DialogResult.OK)
            {
                filePath = m_openFileDialog.FileName;
                Fcn_LoadData(m_openFileDialog.FileName);
            }
        }

        private void ce_Tp_CheckedChanged(object sender, EventArgs e)
        {
            te_beginTP.Enabled = te_EndTP.Enabled = ce_Tp.Checked;
        }

        private void ce_TD_CheckedChanged(object sender, EventArgs e)
        {
            te_beginTD.Enabled = te_EndTD.Enabled = ce_TD.Checked;
        }
        private void Fcn_LoadData(string FilePath)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích File đọc vào, Vui lòng chờ!");
            hLE_File.EditValue = FilePath;
            FileHelper.fcn_spSheetStreamDocument(spsheet_XemFile, FilePath);
            IWorkbook wb = spsheet_XemFile.Document;
            string[] ten = wb.Worksheets.ToList().Select(x => x.Name).ToArray();
            cboCTtenSheet.Properties.Items.AddRange(ten);
            cboCTtenSheet.Text = wb.Worksheets.ActiveWorksheet.Name;
            //wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbe_TenSheet.Text];
            nud_congtrinh_end.Value = wb.Worksheets[cboCTtenSheet.Text].GetUsedRange().BottomRowIndex;
            WaitFormHelper.CloseWaitForm();
        }

        private void txt_congtrinh_stt_EditValueChanged(object sender, EventArgs e)
        {
            dxValidationProvider.Validate();
        }

        private void XtraForm_DocPhanKhaiNhaThau_Load(object sender, EventArgs e)
        {
            Fcn_LoadData(filePath);
        }

        private void ce_TuThucHien_CheckedChanged(object sender, EventArgs e)
        {
            te_TuThucHien.Enabled = ce_TuThucHien.Checked;
        }

        private void te_TTDocVao_EditValueChanged(object sender, EventArgs e)
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Nếu nhập vào ô này dữ liệu lúc đọc sẽ mất thời gian, Bạn có muốn tiếp tục không?????");
            if (rs == DialogResult.No)
            {
                te_TTDocVao.Text = string.Empty;
                return;
            }
        }

        private void cboCTtenSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cb = sender as ComboBoxEdit;
            IWorkbook wb = spsheet_XemFile.Document;
            if (wb.Worksheets.Contains(cb.Text))
            {
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cb.Text];
                CellRange usedRange = wb.Worksheets[cb.Text].GetUsedRange();
                nud_congtrinh_end.Value = usedRange.RowCount;
            }
        }

        private void ce_DonGiaTP_CheckedChanged(object sender, EventArgs e)
        {
            te_DonGiaTPBegin.Enabled = te_DonGiaTPEnd.Enabled = ce_DonGiaTP.Checked;
        }

        private void ce_DonGiaTD_CheckedChanged(object sender, EventArgs e)
        {
            te_DonGiaTDBegin.Enabled = te_DonGiaTDEnd.Enabled = ce_DonGiaTD.Checked;
        }
    }
}