using Antlr.Runtime;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using MoreLinq.Experimental;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    enum PickDateType
    {
        TuDo,
        TheoTuan
    }



    public partial class XtraForm_BaoCaoDuAnOnline : DevExpress.XtraEditors.XtraForm
    {
        string file = $@"{BaseFrom.m_templatePath}\FileReport\Online\Báo cáo tổng hợp tất cả dự án.xlsx";
    
        
        const string col_STT = "Stt";
        const string col_TenDuAn = "TenDuAn";
        const string col_GiaTriHopDong = "GiaTriHopDong";
        const string col_GTSXTrongKy = "col_GTSXTrongKy";
        const string col_GTSXTrongThang = "col_GTSXTrongThang";
        const string col_GTSXTrongNam = "col_GTSXTrongNam";
        const string col_GTSXDaThiCong = "col_GTSXDaThiCong";

        const string sheetName = "Báo cáo tổng hợp";

        const string nameData = "TBT_Data";
        const string nameTime = "TBT_Time";
        const string nameTitle = "TBT_Title";

        string[] colsSum =
        {
            TDKH.COL_KhoiLuongHopDongChiTiet,
            TDKH.COL_GTSXTrongKy,
            TDKH.COL_GTSXTrongThang,
            TDKH.COL_GTSXTrongNam,
            TDKH.COL_GTSXDaThiCong,
        };
        public XtraForm_BaoCaoDuAnOnline()
        {
            InitializeComponent();

            uc_BaoCao1.LoadSetting(BaoCaoFileType.EXCEL);

            uc_BaoCao1.SpreadSheet.LoadDocument(file);
        }

        private void uc_BaoCao1_CustomPreviewIndexChanged(object sender, EventArgs e)
        {
            LoadBaoCao();
        }

        private async void LoadBaoCao()
        {
            if (uc_BaoCao1.PreviewAccessibleName != "HoanChinh")
            {
                uc_BaoCao1.LoadSetting(BaoCaoFileType.EXCEL);
                uc_BaoCao1.SpreadSheet.LoadDocument(file);

                return; 
            }
            
            uc_BaoCao1.SpreadSheet.LoadDocument(file);

            var nbd = de_TuNgay.DateTime.Date;
            var nkt = de_DenNgay.DateTime.Date;

            var wb = uc_BaoCao1.SpreadSheet.Document;
            var ws = wb.Worksheets[sheetName];

            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            var dfnData = ws.DefinedNames.Single(x => x.Name == nameData);

            string time = "";


            if (cbe_CachLocKyTH.SelectedIndex == (int)PickDateType.TuDo)
                time = $"Báo cáo từ {nbd.ToShortDateString()} đến ngày {nkt.ToShortDateString()}";
            else
            {
                int year1 = (int)nud_year.Value;
                int month1 = monthEdit1.SelectedIndex + 1;
                int week1 = cbbe_Week.SelectedIndex + 1;
                time = $"Báo cáo Tuần {week1} Tháng {month1} Năm {year1} (Từ {nbd.ToShortDateString()} đến ngày {nkt.ToShortDateString()})";
            }


            int year = nbd.Year;
            int month = nbd.Month;

            ws.Range[nameTime].SetValue(time);
            ws.Range[nameTitle].SetValue($"KẾT QUẢ THỰC HIỆN CÁC DỰ ÁN NĂM {year}");

            //if (dfnData.Range.RowCount > 3)

            var nbdInMonth = new DateTime(year, month, 1);
            var nktInMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            var nbdInYear = new DateTime(year, 1, 1);
            var nKTInYear = nbdInYear.AddYears(1).AddDays(-1);

            var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<CongTacWithKLHN>(RouteAPI.TongDuAn_GetCongTacTDKHWithKLHN,
                new KLHNRequest()
                {
                });

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Không thể lấy thông tin dự án từ server!");
                uc_BaoCao1.PreviewSelectedIndex = 0;
                return;
            }

            var cttks = result.Dto.Cttks;
            var klhns = result.Dto.KLHNs;

            var grs = cttks.GroupBy(x => x.Owner);
            DataTable dtData = DataTableCreateHelper.BaoCaoToanDuAnOnline();

            int STTOwner = 0;
            foreach (var gr in grs)
            {
                var fstR = gr.First();
                ++STTOwner;

                DataRow newRowOwner = dtData.NewRow();
                dtData.Rows.Add(newRowOwner);

                newRowOwner[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                newRowOwner[TDKH.COL_DanhMucCongTac] = fstR.Owner;
                newRowOwner[TDKH.COL_STT] = STTOwner.ToRoman();
                int crIndRowOwner = dfnData.Range.TopRowIndex + 1 + dtData.Rows.Count;

                int sttDA = 0;
                var grsDA = gr.GroupBy(x => x.CodeDuAn);
                
                foreach (var grDA in grsDA)
                {
                    ++sttDA;

                    var fstDA = grDA.First();

                    DataRow newRowDA = dtData.NewRow();
                    dtData.Rows.Add(newRowDA);

                    newRowDA[TDKH.COL_STT] = sttDA;
                    newRowDA[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                    newRowDA[TDKH.COL_DanhMucCongTac] = fstDA.TenDuAn;
                    
                    var codesCTac = grDA.Select(x => x.Code);
                    var KLHNsInDA = klhns.Where(x => codesCTac.Contains(x.ParentCode));

                    var gthds = grDA.Sum(x => x.ThanhTienHopDong);
                    var GTSXKy = KLHNsInDA.Where(x => x.Ngay >= nbd && x.Ngay <= nkt).Sum(x => x.ThanhTienThiCong);
                    var GTSXThang = KLHNsInDA.Where(x => x.Ngay >= nbdInMonth && x.Ngay <= nktInMonth).Sum(x => x.ThanhTienThiCong);
                    var GTSXNam = KLHNsInDA.Where(x => x.Ngay >= nbdInYear && x.Ngay <= nKTInYear).Sum(x => x.ThanhTienThiCong);
                    var GTSXDaThiCong = KLHNsInDA.Sum(x => x.ThanhTienThiCong);

                    newRowDA[TDKH.COL_KhoiLuongHopDongChiTiet] = gthds;
                    newRowDA[TDKH.COL_GTSXTrongKy] = GTSXKy;
                    newRowDA[TDKH.COL_GTSXTrongThang] = GTSXThang;
                    newRowDA[TDKH.COL_GTSXTrongNam] = GTSXNam;
                    newRowDA[TDKH.COL_GTSXDaThiCong] = GTSXDaThiCong;
                }
                int crIndRow = dfnData.Range.TopRowIndex + 1 + dtData.Rows.Count;

                foreach (var col in colsSum)
                {
                    newRowOwner[col] = $"{MyConstant.PrefixFormula}SUM({dic[col]}{crIndRowOwner + 2}:{dic[col]}{crIndRow + 1})";
                    
                }

            }
            ws.Rows.Insert(dfnData.Range.BottomRowIndex, dtData.Rows.Count, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, dfnData.Range.TopRowIndex + 2, 0);
            SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfnData.Range);
            SpreadsheetHelper.FormatRowsInRange(dfnData.Range, dic[TDKH.COL_TypeRow]);
        }

        private void calcDate()
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
            int year = (int)nud_year.Value;
            int month = monthEdit1.SelectedIndex + 1;
            int week = cbbe_Week.SelectedIndex + 1;

            DayOfWeek startDOW = (DayOfWeek)(cbbe_NgayBatDauMoiTuan.SelectedIndex);
            DateTimeHelper.CalcNthWeekStartEndDate(year, month, week, startDOW, out DateTime dateBD, out DateTime dateKT);

            de_TuNgay.DateTime = dateBD;
            de_DenNgay.DateTime = dateKT;
        }

        private void nud_year_ValueChanged(object sender, EventArgs e)
        {
            calcDate();
        }

        private void monthEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcDate();
        }

        private void cbbe_Week_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcDate();
        }

        private void cbbe_NgayBatDauMoiTuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcDate();
        }

        private void cbe_CachLocKyTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbe_CachLocKyTH.SelectedIndex == (int)PickDateType.TuDo)
            {
                lcg_TheoTuan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcg_TuNgayDenNgay.Enabled = true;
                uc_BaoCao1.PreviewSelectedIndex = 0;
            }
            else
            {
                lcg_TheoTuan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_TuNgayDenNgay.Enabled = false;
                calcDate();
            }   
        }

        private void uc_BaoCao1_CustomExportMau(object sender, EventArgs e)
        {
            if (uc_BaoCao1.PreviewAccessibleName == "HoanChinh")
            {
                string fileName = $"Báo cáo hằng ngày_{DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}";
                if (uc_BaoCao1._type == BaoCaoFileType.WORD)
                {
                    var rec = uc_BaoCao1.RichEditControl;
                    rec.Options.DocumentSaveOptions.CurrentFileName = $"{fileName}.docx";
                    rec.SaveDocumentAs();
                }
                else
                {
                    var spsheet = uc_BaoCao1.SpreadSheet;
                    spsheet.Options.Save.CurrentFileName = $"{fileName}.xlsx";
                    spsheet.SaveDocumentAs();
                }
            }
        }
    }
}