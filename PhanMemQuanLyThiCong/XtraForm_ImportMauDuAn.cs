using DevExpress.PivotGrid.SliceQueryDataSource;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraRichEdit.Import.Html;
using MoreLinq;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PM360.Common.Validate;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_ImportMauDuAn : DevExpress.XtraEditors.XtraForm
    {
        string _crCodeDVN = null;
        string _crCodeDVL = null;

        DataTable _dtDv = null;
        //DataTable _dtDvn = null;

        string CodeDA = SharedControls.slke_ThongTinDuAn.EditValue?.ToString();

        const string KHDVL = "*";
        const string KHDVN = "+";
        public XtraForm_ImportMauDuAn(string crCodeDVN = null, string crCodeDVL = null)
        {


            _crCodeDVL = crCodeDVL;
            _crCodeDVN = crCodeDVN;

            _dtDv = DataProvider.InstanceTHDA
                .ExecuteQuery($"SELECT dvn.Code AS CodeDvn, dvn.DauViec AS TenDvn,\r\n" +
                $" dvl.Code AS CodeDvl, dvl.DauViec AS TenDvl, dvl.CodeDuAn, dvl.SortId AS SortIdDVL, dvn.SortId AS SortIdDVN \r\n" +
                $"FROM {GiaoViec.TBL_DauViecLon} dvl\r\n" +
                $"LEFT JOIN {GiaoViec.TBL_DauViecNho} dvn\r\n" +
                $"ON dvn.CodeDauViecLon = dvl.Code\r\n" +
                $"WHERE CodeDuAn = '{CodeDA}'");

            InitializeComponent();
        }

        private void link_file_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var m_openFileDialog = SharedControls.openFileDialog;
            m_openFileDialog.DefaultExt = "xls";
            m_openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            m_openFileDialog.Title = "Chọn file Excel";
            DialogResult rs = m_openFileDialog.ShowDialog();
            if (rs == DialogResult.OK)
            {
                string filePath = m_openFileDialog.FileName;

                link_file.Text = filePath;
                spsheet_XemFile.LoadDocument(filePath);

                var wb = spsheet_XemFile.Document;
                string[] wss = wb.Worksheets.Select(x => x.Name).ToArray();
            }

        }

        public void ForceRead(string codeDA, string filePath, 
            string col_STT = "A",
            string col_NoiDung = "B",
            string col_ThucHien = "C",
            string col_PhuTrach = "D",
            string col_HoTro = "E",
            string col_CQLienHe = "F",
            string col_NguoiLienHe = "G",
            string col_TrangThai = "H",
            string col_TyLeHoanThanh = "I",
            string col_NguoiThucHien = "J",
            string col_NgayBatDau = "K",
            string col_SoNgay = "M"
            )
        {
            CodeDA = codeDA;
            txt_STT.Text = col_STT;
            txt_NoiDung.Text = col_NoiDung;
            txt_ThucHien.Text = col_ThucHien;
            txt_NguoiThucHien.Text = col_NguoiThucHien;
            txt_PhuTrach.Text = col_PhuTrach;
            txt_Hotro.Text = col_HoTro;
            txt_CQLienHe.Text = col_CQLienHe;
            txt_NguoiLienHe.Text = col_NguoiLienHe;
            txt_TrangThai.Text = col_TrangThai;
            txt_TyLe.Text = col_TyLeHoanThanh;
            txt_ngayBatDau.Text = col_NgayBatDau;
            txt_SoNgay.Text = col_SoNgay;

            spsheet_XemFile.LoadDocument(filePath);
            
            var wb = spsheet_XemFile.Document;
            var ws = spsheet_XemFile.ActiveWorksheet;

            var range = ws.Range["Data"];
            nud_TuDong.Maximum = nud_DenDong.Maximum = range.BottomRowIndex + 1;
            nud_TuDong.Value = range.TopRowIndex + 1;

            nud_DenDong.Value = range.BottomRowIndex + 1;

            bt_Ok_Click(null, null);

        }

        private void bt_Ok_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider.Validate(txt_STT) || !dxValidationProvider.Validate(txt_NoiDung))
                return;

            var dvlnews = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {GiaoViec.TBL_DauViecLon} LIMIT 0");
            var dvnnews = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {GiaoViec.TBL_DauViecNho} LIMIT 0");
            var cts = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {GiaoViec.TBL_CONGVIECCHA} LIMIT 0");

            var wb = spsheet_XemFile.Document;
            var ws = spsheet_XemFile.ActiveWorksheet;

            string col_STT = txt_STT.Text;
            string col_NoiDung = txt_NoiDung.Text;
            string col_ThucHien = txt_ThucHien.Text;
            string col_NguoiThucHien = txt_NguoiThucHien.Text;
            string col_PhuTrach = txt_PhuTrach.Text;
            string col_HoTro = txt_Hotro.Text;
            string col_CQLienHe = txt_CQLienHe.Text;
            string col_NguoiLienHe = txt_NguoiLienHe.Text;
            string col_TrangThai = txt_TrangThai.Text;
            string col_TyLeHoanThanh = txt_TyLe.Text;
            string col_NgayBatDau = txt_ngayBatDau.Text;
            string col_SoNgay = txt_SoNgay.Text;

            bool hasThucHien = dxValidationProvider.Validate(txt_ThucHien);
            bool hasNguoiThucHien = dxValidationProvider.Validate(txt_NguoiThucHien);
            bool hasPhuTrach = dxValidationProvider.Validate(txt_PhuTrach);
            bool hasHoTro = dxValidationProvider.Validate(txt_Hotro);
            bool hasCQLienHe = dxValidationProvider.Validate(txt_CQLienHe);
            bool hasNguoiLienHe = dxValidationProvider.Validate(txt_NguoiLienHe);
            bool hasTrangThai = dxValidationProvider.Validate(txt_TrangThai);
            bool hasTyLeHoanThanh = dxValidationProvider.Validate(txt_TyLe);
            bool hasNgayBatDau = dxValidationProvider.Validate(txt_ThucHien);
            bool hasSoNgay = dxValidationProvider.Validate(txt_SoNgay);

            Dictionary<string, string> dicQDTT = new Dictionary<string, string>();

            //var dbString = $"SELECT Max(SortId) FROM {GiaoViec.TBL_DauViecLon} WHERE CodeDuAn =";

            //string dbString = $"SELECT  dvn.SortId AS SortIdDVN, dvl.SortId AS SortIdDVL\r\n" +
            //    $"FROM {GiaoViec.TBL_DauViecLon} dvl\r\n" +
            //    $"LEFT JOIN {GiaoViec.TBL_DauViecNho} dvn\r\n" +
            //    $"ON dvn.CodeDauViecLon = dvl.Code " +
            //    $"WHERE dvl.CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'\r\n" +
            //    $"ORDER BY dvl.SortId, dvn.SortId";
            //DataTable dtDV = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var lastRowDVL = _dtDv.AsEnumerable().LastOrDefault(x => x["SortIdDVL"] != DBNull.Value);
            //var lastRowDVN = dtDV.AsEnumerable().LastOrDefault(x => x["SortIdDVN"] != DBNull.Value);


            int sortIdDVL = (lastRowDVL is null) ? 0: int.Parse(lastRowDVL["SortIdDVL"].ToString());
            //int sortIdDVN = (lastRowDVN is null) ? (int)lastRowDVN["SortIdDVN"] : 0;


            //int countDVL = lastRowDVL int.Parse(DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0][0].ToString());
            //int countDVN = int.Parse(DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0][0].ToString());

            //string dbString = $"SELECT COUNT(Code) FROM {GiaoViec.TBL_CONGVIECCHA} WHERE CodeHangMuc IS NULL";
            //int countCVC = int.Parse(DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0][0].ToString());

            int countCVC = 0;
            int sortIdDVN = 0;
            for (int i = (int)nud_TuDong.Value - 1; i <= (int)nud_DenDong.Value - 1; i++)
            {

                Row crRow = ws.Rows[i];
                string STT = crRow[col_STT].Value.ToString();
                string STTNext = ws.Rows[i+1][col_STT].Value.ToString();
                string Ten = crRow[col_NoiDung].Value.ToString();

                if (!Ten.HasValue())
                {
                    continue;
                }    

                if (STT == KHDVL)
                {
                    var dvlInDb = _dtDv.AsEnumerable().FirstOrDefault(x => x["CodeDuAn"].ToString() == CodeDA && x["TenDvl"].ToString() == Ten );
                    
                    if (dvlInDb is null)
                    {
                        _crCodeDVL = Guid.NewGuid().ToString();
                        var newdvl = dvlnews.NewRow();
                        dvlnews.Rows.Add(newdvl);
                        newdvl["Code"] = _crCodeDVL;
                        newdvl["DauViec"] = Ten;
                        newdvl["LoaiMau"] = "NguoiDung";

                        newdvl["CodeDuAn"] = CodeDA;
                        newdvl["SortId"] = ++sortIdDVL;
                        sortIdDVN = 0;
                    }
                    else
                    {
                        _crCodeDVL = (string)dvlInDb["CodeDvl"];
                    }

                    continue;
                }
                else if (STT == KHDVN)
                {
                    if (_crCodeDVL is null)
                    {
                        _crCodeDVL = Guid.NewGuid().ToString();
                        var newdvl = dvlnews.NewRow();
                        dvlnews.Rows.Add(newdvl);
                        newdvl["Code"] = _crCodeDVL;
                        newdvl["LoaiMau"] = "NguoiDung";

                        newdvl["DauViec"] = "Tiêu đề mới";
                        newdvl["CodeDuAn"] = CodeDA;
                        newdvl["SortId"] = ++sortIdDVL;
                        sortIdDVN = 0;
                    }
                    else
                    {
                        var lastDvnInDvl = _dtDv.AsEnumerable()
                            .LastOrDefault(x => x["CodeDvl"].ToString() == _crCodeDVL && x["SortIdDVN"] != DBNull.Value);

                        if (lastDvnInDvl != null)
                        {
                            sortIdDVN = int.Parse(lastDvnInDvl["SortIdDVN"].ToString());
                        }

                    }

                    var dvnInDb = _dtDv.AsEnumerable()
                        .FirstOrDefault(x => x["CodeDvl"].ToString() == _crCodeDVL &&  x["TenDvn"].ToString() == Ten);

                    if (dvnInDb is null)
                    {



                        _crCodeDVN = Guid.NewGuid().ToString();
                        countCVC = 0;
                        var newdvn = dvnnews.NewRow();
                        dvnnews.Rows.Add(newdvn);

                        newdvn["Code"] = _crCodeDVN;
                        newdvn["DauViec"] = Ten;
                        newdvn["CodeDauViecLon"] = _crCodeDVL;
                        newdvn["SortId"] = ++sortIdDVN;
                    }

                    if (STTNext == KHDVN)
                    {
                        goto CongTacCal;
                    }
                    continue;
                }
                else
                {
                    goto CongTacCal;

                }
                CongTacCal:
                string ThucHien = (hasThucHien) ? crRow[col_ThucHien].Value.ToString() : "";
                string NguoiThucHien = (hasNguoiThucHien) ? crRow[col_NguoiThucHien].Value.ToString() : "";
                string PhuTrach = (hasPhuTrach) ? crRow[col_PhuTrach].Value.ToString() : "";
                string HoTro = (hasHoTro) ? crRow[col_HoTro].Value.ToString() : "";
                string CQLienHe = (hasCQLienHe) ? crRow[col_CQLienHe].Value.ToString() : "";
                string NguoiLienHe = (hasNguoiLienHe) ? crRow[col_NguoiLienHe].Value.ToString() : "";
                string TrangThai = (hasTrangThai) ? crRow[col_TrangThai].Value.ToString() : "";
                string TyLe = (hasTyLeHoanThanh) ? crRow[col_TyLeHoanThanh].Value.ToString() : "";
                string NgayBatDau = (hasNgayBatDau) ? crRow[col_NgayBatDau].Value.ToString() : "";
                string SoNgay = (hasSoNgay) ? crRow[col_SoNgay].Value.ToString() : "";
                if (_crCodeDVN is null)
                {
                    if (_crCodeDVL is null)
                    {
                        _crCodeDVL = Guid.NewGuid().ToString();
                        var newdvl = dvlnews.NewRow();
                        dvlnews.Rows.Add(newdvl);
                        newdvl["Code"] = _crCodeDVL;
                        newdvl["DauViec"] = "Tiêu đề mới";
                        newdvl["LoaiMau"] = "NguoiDung";

                        newdvl["CodeDuAn"] = CodeDA;
                        //newdvl["SortId"] = ++countDVL;

                    }

                    _crCodeDVN = Guid.NewGuid().ToString();
                    countCVC = 0;
                    var newdvn = dvnnews.NewRow();
                    dvnnews.Rows.Add(newdvn);

                    newdvn["Code"] = _crCodeDVN;
                    newdvn["DauViec"] = "Mẫu mới";
                    newdvn["CodeDauViecLon"] = _crCodeDVL;
                    newdvn["SortId"] = ++sortIdDVN;

                    //newdvn["SortId"] = ++countDVN;

                }
                //else
                //{
                //    string dbString = $"SELECT COUNT(CodeCongViecCha) FROM {GiaoViec.TBL_CONGVIECCHA} WHERE CodeDauMuc = '{_crCodeDVN}'";
                //    countCVC = int.Parse(DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0][0].ToString());
                //}




                var newCt = cts.NewRow();
                cts.Rows.Add(newCt);

                newCt["CodeCongViecCha"] = Guid.NewGuid().ToString();
                newCt["CodeDauMuc"] = _crCodeDVN;
                newCt["TenCongViec"] = Ten;
                newCt["ThucHien"] = ThucHien;
                newCt["NguoiThucHien"] = NguoiThucHien;
                newCt["PhuTrach"] = PhuTrach;
                newCt["HoTro"] = HoTro;
                newCt["CoQuanLienHe"] = CQLienHe;
                newCt["NguoiLienHe"] = NguoiLienHe;
                newCt["TrangThai"] = TrangThai.HasValue() ? TrangThai :EnumTrangThai.CHUATHUCHIEN.GetEnumDisplayName();
                newCt["SortId"] = ++countCVC;

                double.TryParse(TyLe, out double KLKH);
                double.TryParse(SoNgay, out double numDate);
                    
                newCt["KhoiLuongKeHoach"] = KLKH;
                    
                if (!DateTime.TryParse(NgayBatDau, out DateTime dateBD))
                {
                    dateBD = DateTime.Now.Date;
                }
                else
                {
                    newCt["NgayBatDauThiCong"] = dateBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    newCt["NgayKetThucThiCong"] = dateBD.AddDays(numDate).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                }

                newCt["NgayBatDau"] = dateBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                newCt["NgayKetThuc"] = dateBD.AddDays(numDate).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            }

            var ttHeThongs = EnumHelper.GetDisplayNames<EnumTrangThai>();
            var tthaisNotMet = cts.AsEnumerable().Select(x => (string)x["TrangThai"]).Where(x => !ttHeThongs.Contains(x)).Distinct().ToList();

            if (tthaisNotMet.Any())
            {
                MessageShower.ShowWarning($"Một số trạng thái bạn đọc vào không có trong hệ thống, Vui lòng quy đổi để hoàn tất việc đọc vào!");
                var dic = tthaisNotMet.Select(x => new QuyDoiTrangThaiModel()
                {
                    TenGoc = x,
                    TenHeThong = "Chưa thực hiện"
                }).ToList();
                XtraForm_QuyDoiTrangThai form = new XtraForm_QuyDoiTrangThai(dic);
                form.ShowDialog();

                foreach (var item in dic)
                {
                    cts.AsEnumerable().Where(x => (string)x["TrangThai"] == item.TenGoc)
                        .ForEach(x => x["TrangThai"] = item.TenHeThong);
                }
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dvlnews, GiaoViec.TBL_DauViecLon);
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dvnnews, GiaoViec.TBL_DauViecNho);
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(cts, GiaoViec.TBL_CONGVIECCHA);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void XtraForm_ImportMauDuAn_Load(object sender, EventArgs e)
        {
            nud_TuDong.Maximum = int.MaxValue;
            nud_DenDong.Maximum = int.MaxValue;

            List<TextEdit> txts = new List<TextEdit>()
            {
                txt_STT,
                txt_NoiDung,
                txt_ThucHien,
                txt_NguoiThucHien,
                txt_PhuTrach,
                txt_Hotro,
                txt_CQLienHe,
                txt_NguoiLienHe,
                txt_TrangThai,
                txt_TyLe,
                txt_ngayBatDau,
                txt_SoNgay,
            };
            foreach (TextEdit txt in txts)
            {
                dxValidationProvider.SetValidationRule(txt,
                    new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Nhập tên cột Excel phù hợp", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });

            }

            link_file_LinkClicked(null, null);
        }

        private void spsheet_XemFile_ActiveSheetChanged(object sender, ActiveSheetChangedEventArgs e)
        {
            var ws = spsheet_XemFile.ActiveWorksheet;
            var range = ws.GetUsedRange();

            nud_TuDong.Value = range.TopRowIndex;
            nud_DenDong.Value = range.BottomRowIndex;
        }

        private void spsheet_XemFile_DocumentLoaded(object sender, EventArgs e)
        {
            var ws = spsheet_XemFile.ActiveWorksheet;
            var range = ws.GetUsedRange();

            nud_TuDong.Value = range.TopRowIndex + 1;
            nud_DenDong.Value = range.BottomRowIndex + 1;
        }

        private void bt_HuongDan_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(BaseFrom.m_templatePath, "Picture", "1.HuongDanDocGiaoViecDuAn.jpg");
            var fileVM = new FileViewModel(fileName);
            MyFunction.xemTruocHINHANH(new FileViewModel()
            {
                FilePath = fileName,
            });
        }
    }
}