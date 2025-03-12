using DevExpress.Spreadsheet;
using DevExpress.Utils.CommonDialogs;
using DevExpress.XtraEditors;
using DevExpress.XtraSpellChecker;
using DevExpress.XtraSpreadsheet.Mouse;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PM360.Common.Validate;
using StackExchange.Profiling.Internal;
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
    public partial class XtraForm_ImportGiaoViec : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_ImportGiaoViec()
        {
            InitializeComponent();


            dxValidationProvider.SetValidationRule(txt_STT, 
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Nhập tên cột Excel phù hợp", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(txt_TenCT, 
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Nhập tên cột Excel phù hợp", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });

            dxValidationOptional.SetValidationRule(txt_MaCT,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            dxValidationOptional.SetValidationRule(txt_Desc,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            dxValidationOptional.SetValidationRule(txt_DonVi,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            dxValidationOptional.SetValidationRule(txt_NgayBD,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            dxValidationOptional.SetValidationRule(txt_NgayKT,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            dxValidationOptional.SetValidationRule(txt_KhoiLuong,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            dxValidationOptional.SetValidationRule(txt_DonGia,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            //dxValidationOptional.SetValidationRule(txt_VatLieu,
            //    new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            //dxValidationOptional.SetValidationRule(txt_NhanCong,
            //    new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            //dxValidationOptional.SetValidationRule(txt_May,
            //    new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            dxValidationOptional.SetValidationRule(txt_DGVL,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            dxValidationOptional.SetValidationRule(txt_DGNC,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });
            dxValidationOptional.SetValidationRule(txt_DGMay,
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Tên cột không đúng", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning });

            link_file_LinkClicked(null, null);
            spsheet_XemFile_ActiveSheetChanged(null, null);
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

        private void bt_Ok_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider.Validate())
            {
                MessageShower.ShowWarning("Vui lòng nhập đúng tên cột cho cột STT và TÊN CÔNG TÁC");
                return;
            }
            if (!dxValidationOptional.Validate())
            {
                var dr = MessageShower.ShowYesNoQuestion($"Các cột chưa nhập sẽ được bỏ qua và không lấy được số liệu!", "Tiếp tục đọc", "Kiểm tra lại");
                if (dr != DialogResult.Yes)
                    return;
            }

            var ws = spsheet_XemFile.ActiveWorksheet;


            List<string> queries = new List<string>();
            List<string> queriesDesc = new List<string>();
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu");

            List<object> objs = new List<object>();
            List<object> objsDesc = new List<object>();

            string col_STT = txt_STT.Text;
            string col_MaCT = txt_MaCT.Text;
            string col_TenCT = txt_TenCT.Text;
            string col_NgayBD = txt_NgayBD.Text;
            string col_NgayKT = txt_NgayKT.Text;
            string col_Desc = txt_Desc.Text;
            string col_DonVi = txt_DonVi.Text;
            string col_KhoiLuong = txt_KhoiLuong.Text;
            string col_DonGia = txt_DonGia.Text;
            //string col_VatLieu = txt_VatLieu.Text;
            //string col_NhanCong = txt_NhanCong.Text;
            //string col_May = txt_May.Text;
            string col_DGVL = txt_DGVL.Text;
            string col_DGNC = txt_DGNC.Text;
            string col_DGMay = txt_DGMay.Text;

            int STTAll = -1;
            int order = 1;
            int numRow = ((int)nud_DenDong.Value - (int)nud_TuDong.Value) + 1;
            string DescAll = "";
            string crGuid = default;
            for (int i = (int)nud_TuDong.Value - 1; i <= (int)nud_DenDong.Value - 1; i++)
            {
                WaitFormHelper.ShowWaitForm($"Đang đọc {order++}/{numRow}");
                Row crRow = ws.Rows[i];

                string STT =  crRow[col_STT].Value.ToString();
                string MaCT =  (dxValidationOptional.Validate(txt_MaCT)) ? crRow[col_MaCT].Value.ToString() : "";
                string TenCT =  crRow[col_TenCT].Value.ToString();
                string Desc = (dxValidationOptional.Validate(txt_Desc)) ? crRow[col_Desc].Value.ToString() : "";

                if (!int.TryParse(STT, out int parseSTTAll))
                {
                    
                    if (STTAll > 0 && STT.StartsWith($"{STTAll}."))
                    {
                        if (MaCT.HasValue())
                        {
                            DescAll += $"\r\n{MaCT}";
                        }

                        if (TenCT.HasValue())
                        {
                            DescAll += $"\r\n{TenCT}";
                        }

                        if (Desc.HasValue())
                        {
                            DescAll += $"\r\n{Desc}";
                        }

                    }

                    if (i == (int)nud_DenDong.Value && DescAll.HasValue() && crGuid != default)
                    {
                        objsDesc.Add(DescAll);
                        queriesDesc.Add($"UPDATE {Server.Tbl_GiaoViec_CongViecCha} SET MoTa = @MoTa WHERE CodeCongViecCha = '{crGuid}'");
                    }
                    continue;

                }
                STTAll = parseSTTAll;
                if (DescAll.HasValue() && crGuid != default)
                {
                    objsDesc.Add(DescAll);

                    queriesDesc.Add($"UPDATE {Server.Tbl_GiaoViec_CongViecCha} SET MoTa = @MoTa WHERE CodeCongViecCha = '{crGuid}'");
                }
                

                DescAll = "";
                crGuid = Guid.NewGuid().ToString();
                string NgayBD = (dxValidationOptional.Validate(txt_NgayBD)) ? crRow[col_NgayBD].Value.ToString() : "";
                string NgayKT = (dxValidationOptional.Validate(txt_NgayKT)) ? crRow[col_NgayKT].Value.ToString() : "";
                string DonVi = (dxValidationOptional.Validate(txt_DonVi)) ? crRow[col_DonVi].Value.ToString() : "";
                //string MoTa = (dxValidationOptional.Validate(txt_)) ? crRow[col_DonVi].Value.ToString() : "";
                string KhoiLuong =  (dxValidationOptional.Validate(txt_KhoiLuong)) ? crRow[col_KhoiLuong].Value.ToString() : "";
                string DonGia =  (dxValidationOptional.Validate(txt_DonGia)) ? crRow[col_DonGia].Value.ToString() : "";
                string DGVL =  (dxValidationOptional.Validate(txt_DGVL)) ? crRow[col_DGVL].Value.ToString() : "";
                string DGNC =  (dxValidationOptional.Validate(txt_DGNC)) ? crRow[col_DGNC].Value.ToString() : "";
                string DGMay = (dxValidationOptional.Validate(txt_DGMay)) ?  crRow[col_DGMay].Value.ToString() : "";


                //DateTime objNgayBD = DateTime.Now.Date;
                //DateTime objNgayKT = DateTime.Now.AddDays(30).Date;
                //double objKhoiLuong = 0;
                //double objDonGia = 0;
                double? objDGVL = null;
                double? objDGNC = null;
                double? objDGMay = null;

                DateTime objNgayBD = DateTime.TryParse(NgayBD, out DateTime parseObjNgayBD) ? parseObjNgayBD : DateTime.Now.Date;
                DateTime objNgayKT = DateTime.TryParse(NgayKT, out DateTime parseObjNgayKT)? parseObjNgayKT : DateTime.Now.AddDays(30).Date;
                double objKhoiLuong = Double.TryParse(KhoiLuong, out double parseObjKhoiLuong) ? parseObjKhoiLuong : 0;
                double objDonGia = Double.TryParse(DonGia, out double parseObjDonGia) ? parseObjDonGia : 0;
                objDGVL = Double.TryParse(DGVL, out double parseObjDGVL) ? parseObjDGVL : objDGVL;
                objDGNC = Double.TryParse(DGNC, out double parseObjDGNC) ? parseObjDGNC : objDGNC;
                objDGMay = Double.TryParse(DGMay, out double parseObjDGMay) ? parseObjDGMay : objDGMay;

                objs.Add(crGuid);
                objs.Add(MaCT);
                objs.Add(TenCT);
                objs.Add(DonVi);
                objs.Add(Desc);

                objs.Add(objNgayBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                objs.Add(objNgayKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                objs.Add(objKhoiLuong);
                objs.Add(objKhoiLuong);
                objs.Add(objDonGia);
                objs.Add(objDonGia);
                objs.Add(objDGVL);
                objs.Add(objDGNC);
                objs.Add(objDGMay);

                string query = $@"INSERT INTO {Server.Tbl_GiaoViec_CongViecCha} 
(CodeCongViecCha, MaDinhMuc, TenCongViec, DonVi, MoTa,NgayBatDau, NgayKetThuc, KhoiLuongKeHoach, KhoiLuongHopDong, DonGia, DonGiaThiCong, DonGiaVatLieuDocVao, DonGiaNhanCongDocVao, DonGiaMayDocVao) VALUES
(@CodeCongViecCha, @MaDinhMuc, @TenCongViec, @DonVi, @MoTa ,@NgayBatDau, @NgayKetThuc, @KhoiLuongKeHoach, @KhoiLuongHopDong, @DonGia, @DonGiaThiCong, @DonGiaVatLieuDocVao, @DonGiaNhanCongDocVao, @DonGiaMayDocVao)";
                //DataProvider.InstanceTHDA.ExecuteNonQuery(query, parameter: objs.ToArray());
                queries.Add(query);
            }

            if (!queries.Any())
            {
                MessageShower.ShowWarning("Không có công tác để đọc vào");
                WaitFormHelper.CloseWaitForm();
                Close();
                return;
            }

            var num = DataProvider.InstanceTHDA.ExecuteNonQueryFromList(queries, parameter: objs.ToArray());

            if (queriesDesc.Any())
            {
                DataProvider.InstanceTHDA.ExecuteNonQueryFromList(queriesDesc, parameter: objsDesc.ToArray());

            }

            WaitFormHelper.CloseWaitForm();
            var mess = $"Đã đọc vào {numRow} công tác";
            MessageShower.ShowInformation(mess);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void spsheet_XemFile_ActiveSheetChanged(object sender, ActiveSheetChangedEventArgs e)
        {
            var ws = spsheet_XemFile.ActiveWorksheet;
            var range = ws.GetUsedRange();

            nud_TuDong.Minimum = nud_DenDong.Minimum = range.TopRowIndex + 1;
            nud_TuDong.Maximum = nud_DenDong.Maximum = range.BottomRowIndex + 1;

            nud_TuDong.Value = nud_TuDong.Minimum;
            nud_DenDong.Value = nud_DenDong.Maximum;
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void XtraForm_ImportGiaoViec_FormClosed(object sender, FormClosedEventArgs e)
        {
            spsheet_XemFile.Dispose();
            GC.Collect();
        }
    }
}