using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.TDKH;
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
    public partial class XtraForm_DocDuLieuKeHoachNam : DevExpress.XtraEditors.XtraForm
    {
        public string filePath { get; set; }

        public XtraForm_DocDuLieuKeHoachNam()
        {
            InitializeComponent();
            this.CausesValidation = false;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            dxValidationProvider.Validate();
        }

        private void de_Year_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Fcn_LoadData(string FilePath)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích File đọc vào, Vui lòng chờ!");
            hLE_File.EditValue = FilePath;
            FileHelper.fcn_spSheetStreamDocument(spsheet_XemFile, FilePath);
            IWorkbook wb = spsheet_XemFile.Document;
            string[] ten = wb.Worksheets.ToList().Select(x => x.Name).ToArray();
            cbe_TenSheet.Properties.Items.AddRange(ten);
            cbe_TenSheet.Text = wb.Worksheets.ActiveWorksheet.Name;
            //wb.Worksheets.ActiveWorksheet = wb.Worksheets[cbe_TenSheet.Text];
            nud_tonghop_end.Value = wb.Worksheets[cbe_TenSheet.Text].GetUsedRange().BottomRowIndex;
            WaitFormHelper.CloseWaitForm();
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

        private void XtraForm_DocDuLieuKeHoachNam_Load(object sender, EventArgs e)
        {
            Fcn_LoadData(filePath);
        }
        private List<InitialVolumnDto> Fcn_ReadExcel()
        {
            WaitFormHelper.ShowWaitForm("Đang đọc dữ liệu, Vui lòng chờ!");
            Regex ColumCheck = new Regex($"[{te_TuCot.Text}-{te_DenCot.Text}]");
            Regex FromTo = new Regex(@"(?<Start>(\d|\/){3,10})(\s|-|[a-z]|[A-Z])+(?<End>(\d|\/){3,10})");
            List<string> NameColum = new List<string>();
            IWorkbook wb = spsheet_XemFile.Document;
            Worksheet ws = wb.Worksheets[cbe_TenSheet.Text];
            CellRange UseRange = ws.GetUsedRange();
            wb.BeginUpdate();
            for (int i = UseRange.LeftColumnIndex; i <= UseRange.RightColumnIndex; i++)
            {
                string ColumName = ws.Columns[i].Heading;
                MatchCollection Multimatch = ColumCheck.Matches(ColumName);
                if (Multimatch.Count > 0)
                {
                    if (ColumName.Length == Multimatch[0].Length)
                        NameColum.Add(ColumName);
                }
            }
            int STT = 0;
            int State = 1;
            string TenCT = string.Empty;
            string Begin = string.Empty, End = string.Empty;
            bool DayFrom = false, DayTo = false;
            List<InitialVolumnDto> Lst = new List<InitialVolumnDto>();
            double TT = 0;
            for (int i = (int)nud_tonghop_begin.Value - 1; i <= (int)nud_tonghop_end.Value - 1; i++)
            {
                Row Crow = ws.Rows[i];
                var isSTT = int.TryParse(Crow[te_STT.Text].Value.ToString(), out STT);
                if (!isSTT)
                {
                    continue;
                }
                TenCT = Crow[te_TenCT.Text].Value.TextValue;
                if (string.IsNullOrEmpty(TenCT))
                    continue;
                WaitFormHelper.ShowWaitForm($"{STT}_{TenCT}");

                    foreach (var Name in NameColum)
                    {
                        string Header = MyFunction.fcn_RemoveAccents(ws.Rows[(int)spe_Row.Value - 1][Name].Value.ToString());

                        MatchCollection Multimatch = FromTo.Matches(Header);
                        if (Multimatch.Count > 0)
                        {
                            Begin = Multimatch[0].Groups["Start"].ToString();
                            End = Multimatch[0].Groups["End"].ToString();
                            DayFrom = DateTime.TryParse($"{Begin}/{de_Year.Text}", out DateTime From);
                            DayTo = DateTime.TryParse($"{End}/{de_Year.Text}", out DateTime To);
                            if (!DayFrom || !DayTo)
                                continue;
                            double.TryParse(Crow[Name].Value.ToString(), out TT);
                            InitialVolumnDto NewItem = new InitialVolumnDto();
                            NewItem.ProjectName = TenCT;
                            NewItem.Volume = TT;
                            NewItem.From = From;
                            NewItem.To = To;
                            NewItem.State = (StateEnum)State;
                            Lst.Add(NewItem);
                        }

                    }
            }
            wb.EndUpdate();
            WaitFormHelper.CloseWaitForm();
            return Lst;
        }
        private async void sb_ReadExcel_Click(object sender, EventArgs e)
        {
            List<InitialVolumnDto> Lst= Fcn_ReadExcel();
            WaitFormHelper.ShowWaitForm("Đang gửi dữ liệu lên server!");
            var ret = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>(RouteAPI.Initialize_UpdateVolumes, Lst);
            if (!ret.MESSAGE_TYPECODE)
            {
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowError($"Lỗi cập nhật dữ liệu\r\n{ret.MESSAGE_CONTENT}");
                return;
            }

            WaitFormHelper.CloseWaitForm();
            MessageShower.ShowInformation("Đã cập nhật");
        }

        private void te_TongHopSTT_EditValueChanged(object sender, EventArgs e)
        {
            dxValidationProvider.Validate();
        }

        private void cbe_TenSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cb = sender as ComboBoxEdit;
            IWorkbook wb = spsheet_XemFile.Document;
            if (wb.Worksheets.Contains(cb.Text))
            {
                wb.Worksheets.ActiveWorksheet = wb.Worksheets[cb.Text];
                nud_tonghop_end.Value = wb.Worksheets[cb.Text].GetUsedRange().BottomRowIndex;
            }
        }
    }
}