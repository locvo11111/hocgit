using DevExpress.Spreadsheet;
using DevExpress.Utils.CommonDialogs;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Mouse;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PM360.Common.Validate;
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
    public partial class XtraForm_ImportDonGiaVatTu : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_ImportDonGiaVatTu()
        {
            InitializeComponent();


            dxValidationProvider.SetValidationRule(txt_TenVatTu, 
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Nhập tên cột Excel phù hợp", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(txt_DonGia, 
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Nhập tên cột Excel phù hợp", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(txt_DonVi, 
                new CustomValidationTextEditColumnNameExcelRule { ErrorText = "Nhập tên cột Excel phù hợp", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });

            rg_LoaiLayDonGia.SelectedIndex = 1;

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
                return;


            string nameType = rg_LoaiLayDonGia.GetAccessibleName();
            string typeDonGia;
            string formatDonGia;

            if (nameType == "All")
            {
                typeDonGia = "ĐƠN GIÁ THI CÔNG và ĐƠN GIÁ KẾ HOẠCH";
                formatDonGia = "DonGia = '{0}', DonGiaThiCong = '{0}'";
            }
            else if (nameType == "DonGia")
            {
                typeDonGia = "ĐƠN GIÁ KẾ HOẠCH";
                formatDonGia = "DonGia = '{0}'";
            }
            else
            {
                typeDonGia = "ĐƠN GIÁ THI CÔNG";
                formatDonGia = "DonGiaThiCong = '{0}'";
            }    

            var ws = spsheet_XemFile.ActiveWorksheet;
            string mess = $"{typeDonGia} của các vật tư được tìm thấy trong sheet {ws.Name.ToUpper()} sẽ được thay thế bằng đơn giá bạn đọc vào!" +
                $"Bạn sẽ không thể lấy lại đơn giá cũ!";

            if (MessageShower.ShowYesNoQuestion(mess) != DialogResult.Yes)
            {
                return;
            }

            var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            if (crDVTH == null)
            {
                MessageShower.ShowWarning("Không tìm thấy đơn vị thực hiện");
                return;
            }

            string dbString = $"SELECT hp.Code " +
                $"FROM {TDKH.Tbl_HaoPhiVatTu} hp " +
                $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"ON hp.CodeCongTac = cttk.Code " +
                $"WHERE cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}'";
            
            DataTable dtCodeHp = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            if (dtCodeHp.Rows.Count == 0)
            {
                MessageShower.ShowWarning("Không có vật tư trong dự án được cập nhật");
                Close();
                return;
            }

            string codesHPValid = MyFunction.fcn_Array2listQueryCondition(dtCodeHp.AsEnumerable().Select(x => x[0].ToString()));

            List<string> queries = new List<string>();
            WaitFormHelper.ShowWaitForm("Đang cập nhật đơn giá");

            List<object> objs = new List<object>();
            for (int i = (int)nud_TuDong.Value - 1; i <= (int)nud_DenDong.Value - 1; i++)
            {
                Row crRow = ws.Rows[i];
                string colTen = txt_TenVatTu.Text;
                string colDonVi = txt_DonVi.Text;
                string colDonGia = txt_DonGia.Text;

                if (!long.TryParse(crRow[colDonGia].Value.ToString(), out long donGia))
                {
                    continue;
                }

                string Ten = crRow[colTen].Value.ToString();
                string DonVi = crRow[colDonVi].Value.ToString();

                objs.Add(Ten);
                objs.Add(DonVi);
                objs.Add(Ten);
                objs.Add(DonVi);

                string query = $"UPDATE {TDKH.TBL_KHVT_VatTu} " +
                    $"SET {string.Format(formatDonGia, donGia.ToString())} " +
                    $"WHERE VatTu = @Ten AND DonVi = @DonVi " +
                    $"AND {crDVTH.ColCodeFK} = '{crDVTH.Code}'" +
                    $";\r\n" +
                    $"UPDATE {TDKH.Tbl_HaoPhiVatTu} " +
                    $"SET {string.Format(formatDonGia, donGia.ToString())} " +
                    $"WHERE VatTu = @Ten AND DonVi = @DonVi " +
                    $"AND Code IN ({codesHPValid})";
                queries.Add(query);
            }

            if (!queries.Any())
            {
                MessageShower.ShowWarning("Không có vật tư trong dự án được cập nhật");
                WaitFormHelper.CloseWaitForm();
                Close();
                return;
            }

            var num = DataProvider.InstanceTHDA.ExecuteNonQueryFromList(queries, parameter: objs.ToArray());

            WaitFormHelper.CloseWaitForm();
            mess = $"Đã cập nhật đơn giá cho {queries.Count()} Vật tư và {num} hao phí công tác";
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

        private void XtraForm_ImportDonGiaVatTu_FormClosed(object sender, FormClosedEventArgs e)
        {
            spsheet_XemFile.Dispose();
            GC.Collect();
        }
    }
}