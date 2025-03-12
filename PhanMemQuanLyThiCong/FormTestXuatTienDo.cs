using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using PhanMemQuanLyThiCong.Model.TDKH;
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
    public partial class FormTestXuatTienDo : DevExpress.XtraEditors.XtraForm
    {
        public List<TaskDataItem> tasks { get; set; }
        public FormTestXuatTienDo()
        {
            InitializeComponent();
        }
        public enum LsTongHopEnum
        {
            STT,
            Ten,
            NBD,
            SoNgay,
            NKT,
            DonVi,
            KLHD,
            GhiChu,
        }
        private void Xuatexcel()
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu");
            string m_Path = Path.Combine(BaseFrom.m_path, "Template", "FileExcel", "TienDo.xlsx");
            spreadsheetControl1.LoadDocument(m_Path);
            Worksheet ws = spreadsheetControl1.Document.Worksheets[0];
            CellRange CongTac = ws.Range["Data_CongTac"];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(CongTac);
            int InDexCT = 6;
            int stt = 1;
            spreadsheetControl1.BeginUpdate();
            CellRange Copy = ws.Range["4:6"];
            DateTime Max = tasks.Where(x=>x.FinishDate.HasValue).Max(x => x.FinishDate.Value);
            DateTime Min = tasks.Where(x => x.FinishDate.HasValue).Min(x => x.StartDate.Value);
            double SoCot = (Max.Date - Min.Date).TotalDays;
            ws.Columns.Insert(CongTac.RightColumnIndex + 2, (int)SoCot, ColumnFormatMode.FormatAsPrevious);
            int colum = 9;
            for(DateTime i= Min;i <= Max; i = i.AddDays(1))
            {
                ws.Rows[1][colum++].SetValueFromText(i.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
            }
            ws.MergeCells(ws.Range[$"J1:{ws.Columns[CongTac.RightColumnIndex + (int)SoCot+1].Heading}1"]);
            int SubMin = 0;
            CellRange RangeFil,RangeDecrip;
            int Type = 0;
            string CodeNhaThau = "";
            foreach (var item in tasks)
            {
                Type = item.TaskType;
                Row CrowCT = ws.Rows[InDexCT];
                switch (Type)
                {
                    case TDKH.TYPE_TaskTienDo_DuAn:
                        CrowCT = ws.Rows[InDexCT++];
                        CrowCT[(int)LsTongHopEnum.STT].SetValueFromText("DA");
                        CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                        CrowCT.Font.Bold = true;
                        break;    
                    case TDKH.TYPE_TaskTienDo_CongTrinh:
                        CrowCT = ws.Rows[InDexCT++];
                        ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                        CrowCT[(int)LsTongHopEnum.STT].SetValueFromText("CTR");
                        CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                        CrowCT.Font.Bold = true;
                        CrowCT.Font.Color = MyConstant.color_Row_CongTrinh;
                        break;              
                    
                    
                    case TDKH.TYPE_TaskTienDo_HangMuc:
                        CrowCT = ws.Rows[InDexCT++];
                        ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                        CrowCT[(int)LsTongHopEnum.STT].SetValueFromText("HM");
                        CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                        CrowCT.Font.Bold = true;
                        CrowCT.Font.Color = MyConstant.color_Row_HangMuc;
                        break;

                    case TDKH.TYPE_TaskTienDo_NhaThau:
                        CodeNhaThau = item.UID;
                        CrowCT = ws.Rows[InDexCT++];
                        ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                        CrowCT[(int)LsTongHopEnum.STT].SetValueFromText("DVTH");
                        CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                        CrowCT.Font.Bold = true;
                        break;
                    case TDKH.TYPE_TaskTienDo_Tuyen_New:
                        if (item.UID.Contains("_CodeTuyen"))
                        {
                            CrowCT = ws.Rows[InDexCT++];
                            ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                            CrowCT[(int)LsTongHopEnum.STT].SetValueFromText("*T");
                            CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                            CrowCT.Font.Bold = true;
                            CrowCT.Font.Color = Color.Red;

                        }
                        else if (item.UID.Contains("_CodeNhom"))
                        {
                            CrowCT = ws.Rows[InDexCT++];
                            ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                            CrowCT[(int)LsTongHopEnum.STT].SetValueFromText("*");
                            CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                            CrowCT.Font.Bold = true;
                            CrowCT.Font.Color = MyConstant.color_Row_NhomCongTac;
                        }
                        else if (item.UID.Contains("_KeHoach"))
                        {
                            CrowCT = ws.Rows[InDexCT++];
                            ws.Rows.Insert(InDexCT, 3, RowFormatMode.FormatAsNext);
                            CrowCT.CopyFrom(Copy, PasteSpecial.All);
                            CrowCT[(int)LsTongHopEnum.STT].SetValue(stt++);
                            CrowCT[(int)LsTongHopEnum.NBD].SetValueFromText(item.StartDate.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                            CrowCT[(int)LsTongHopEnum.NKT].SetValueFromText(item.FinishDate.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                            CrowCT[(int)LsTongHopEnum.DonVi].SetValue(item.DonVi);
                            CrowCT[(int)LsTongHopEnum.KLHD].SetValue(item.KhoiLuongHD);
                            CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                            CrowCT[(int)LsTongHopEnum.GhiChu].SetValueFromText(item.GhiChu);
                            SubMin = (int)(item.StartDate.Value.Date - Min.Date).TotalDays;
                            RangeFil = ws.Range[$"{ws.Columns[9 + SubMin].Heading}{InDexCT + 1}:{ws.Columns[9 + SubMin + item.SoNgay].Heading}{InDexCT + 1}"];
                            RangeFil.FillColor = Color.Blue;
                            ws.Rows[InDexCT][10 + SubMin + item.SoNgay].SetValueFromText(item.Description);
                            //RangeDecrip = ws.Range[$"{ws.Columns[10 + SubMin + item.SoNgay].Heading}{InDexCT}:{ws.Columns[colum].Heading}{InDexCT + 2}"];
                            //ws.MergeCells(RangeDecrip);
                            InDexCT = InDexCT + 2;
                        }


                            break;
                    case TDKH.TYPE_TaskTienDo_NhomThuocTuyen:
                        if (item.UID.Contains("_CodeNhom"))
                        {
                            CrowCT = ws.Rows[InDexCT++];
                            ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                            CrowCT[(int)LsTongHopEnum.STT].SetValueFromText("*");
                            CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                            CrowCT.Font.Bold = true;
                            CrowCT.Font.Color = MyConstant.color_Row_NhomCongTac;
                        }
                        else
                        {
                            CrowCT = ws.Rows[InDexCT++];
                            ws.Rows.Insert(InDexCT, 3, RowFormatMode.FormatAsNext);
                            CrowCT.CopyFrom(Copy, PasteSpecial.All);
                            CrowCT[(int)LsTongHopEnum.STT].SetValue(stt++);
                            CrowCT[(int)LsTongHopEnum.NBD].SetValueFromText(item.StartDate.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                            CrowCT[(int)LsTongHopEnum.NKT].SetValueFromText(item.FinishDate.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                            CrowCT[(int)LsTongHopEnum.DonVi].SetValue(item.DonVi);
                            CrowCT[(int)LsTongHopEnum.KLHD].SetValue(item.KhoiLuongHD);
                            CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                            CrowCT[(int)LsTongHopEnum.GhiChu].SetValueFromText(item.GhiChu);
                            SubMin = (int)(item.StartDate.Value.Date - Min.Date).TotalDays;
                            RangeFil = ws.Range[$"{ws.Columns[9 + SubMin].Heading}{InDexCT + 1}:{ws.Columns[9 + SubMin + item.SoNgay].Heading}{InDexCT + 1}"];
                            RangeFil.FillColor = Color.Blue;
                            ws.Rows[InDexCT][10 + SubMin + item.SoNgay].SetValueFromText(item.Description);
                            //RangeDecrip = ws.Range[$"{ws.Columns[10 + SubMin + item.SoNgay].Heading}{InDexCT}:{ws.Columns[colum].Heading}{InDexCT + 2}"];
                            //ws.MergeCells(RangeDecrip);
                            InDexCT = InDexCT + 2;
                        }

                        break;
                    case TDKH.TYPE_TaskTienDo_CongViecThuocNhomTrongTuyen:
                        if (item.UID.Contains("_KeHoach"))
                        {
                            CrowCT = ws.Rows[InDexCT++];
                            ws.Rows.Insert(InDexCT, 3, RowFormatMode.FormatAsNext);
                            CrowCT.CopyFrom(Copy, PasteSpecial.All);
                            CrowCT[(int)LsTongHopEnum.STT].SetValue(stt++);
                            CrowCT[(int)LsTongHopEnum.NBD].SetValueFromText(item.StartDate.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                            CrowCT[(int)LsTongHopEnum.NKT].SetValueFromText(item.FinishDate.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                            CrowCT[(int)LsTongHopEnum.DonVi].SetValue(item.DonVi);
                            CrowCT[(int)LsTongHopEnum.KLHD].SetValue(item.KhoiLuongHD);
                            CrowCT[(int)LsTongHopEnum.Ten].SetValueFromText(item.Name.ToUpper());
                            CrowCT[(int)LsTongHopEnum.GhiChu].SetValueFromText(item.GhiChu);
                            SubMin = (int)(item.StartDate.Value.Date - Min.Date).TotalDays;
                            RangeFil = ws.Range[$"{ws.Columns[9 + SubMin].Heading}{InDexCT + 1}:{ws.Columns[9 + SubMin + item.SoNgay].Heading}{InDexCT + 1}"];
                            RangeFil.FillColor = Color.Blue;
                            ws.Rows[InDexCT][10 + SubMin + item.SoNgay].SetValueFromText(item.Description);
                            //RangeDecrip = ws.Range[$"{ws.Columns[10 + SubMin + item.SoNgay].Heading}{InDexCT}:{ws.Columns[colum].Heading}{InDexCT + 2}"];
                            //ws.MergeCells(RangeDecrip);
                            InDexCT = InDexCT + 2;
                        }

                        break;
                    default:
                        break;
                }



            }
            Fcn_UpdateVatLieu("Máy thi công", "Data_MayThiCong", ws, Copy, Min, Max);
            Fcn_UpdateVatLieu("Vật liệu", "Data_VatLieu", ws, Copy, Min, Max);
            Fcn_UpdateVatLieu("Nhân công", "Data_NhanCong",ws, Copy, Min, Max);
            ws.Rows.Remove(2,4);
            ws.Rows.Remove(ws.Range["Data_CongTac"].BottomRowIndex-3, 4);
            spreadsheetControl1.EndUpdate();


            spreadsheetControl1.SaveDocumentAs();
            WaitFormHelper.CloseWaitForm();
        }
        private Dictionary<DateTime, int> LoadBieuDoNhanCong()
        {
            Dictionary<DateTime, int> dicNC = new Dictionary<DateTime, int>();
            var DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            //if (DVTH is null)
            //    return;
            string dbString = $"SELECT hp.Code FROM {TDKH.Tbl_HaoPhiVatTu} hp " +
                $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} ct " +
                $"ON hp.CodeCongTac = ct.Code " +
                $"WHERE {DVTH.ColCodeFK} = '{DVTH.Code}' " +
                $"AND CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                $"AND LoaiVatTu = 'Nhân công'";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            var codesVt = dt.AsEnumerable().Select(x => (string)x["Code"]).ToArray();

            var hangNgays = MyFunction.Fcn_CalKLKHModel(TypeKLHN.HaoPhiVatTu, codesVt);
            if (hangNgays == null)
                return dicNC;

            var grs = hangNgays.GroupBy(x => x.Ngay.Value);
            foreach (var gr in grs)
            {
                int nc = gr.Where(x => x.KhoiLuongKeHoach.HasValue).Any() ? (int)Math.Ceiling(gr.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach.Value)) : 0;
                dicNC.Add(gr.Key.Date, nc);
            }

            //chartControl_NhanCong.DataSource = dicNC;
            return dicNC;
        }
        private void Fcn_UpdateVatLieu(string LoaiVL, string Range, Worksheet ws, CellRange RangCopy, DateTime Min, DateTime Max)
        {
            CellRange VatLieu = ws.Range[Range];
            if (LoaiVL == "Nhân công")
            {
                Dictionary<DateTime, int> dic = LoadBieuDoNhanCong();

                int maxNC = dic.Values.Max();
                int duNC = maxNC % 40;
                maxNC += (40 - duNC);
                int buocNhanCong = maxNC / 40;
                int m = 0;
                for (int k = 40; k >= 1; k--)
                {
                    ws.Rows[VatLieu.TopRowIndex + 1 + (m * 5)][7].Value = buocNhanCong * k;
                    m++;
                }
                int maxNhom = (Max.Date - Min.Date).Days;
                for (int i = 0; i < maxNhom; i++)
                {
                    DateTime Value =DateTime.Parse(ws.Rows[1][9 + i].Value.ToString()).Date;
                    int sttEnd = dic[Value];
                    if (sttEnd < 1) continue;
                    int duIndexColumnNC = sttEnd % buocNhanCong;
                    int nguyenIndexColumnNC = sttEnd / buocNhanCong;
                    if (duIndexColumnNC > 0) nguyenIndexColumnNC++;
                    Cell start = ws.Rows[VatLieu.BottomRowIndex - (nguyenIndexColumnNC * 5)][9 + i];
                    Cell end = ws.Rows[VatLieu.BottomRowIndex - 1][9 + i];
                    CellRange range = ws.Range[$"{start.GetReferenceA1()}:{end.GetReferenceA1()}"];
                    range.FillColor = Color.Red;
                }
                return;
            }
            DataTable dtCongTacTheoKy = null, dtDependency = null;
            List<KLHN> dtTheoNgay;
            int SubMin = 0;
            CellRange RangeFil;
            DinhMucHelper.fcn_CTac(LoaiVL, out dtCongTacTheoKy, out dtDependency, out dtTheoNgay, false, false);
            int stt = 1;
            int InDexCT = VatLieu.TopRowIndex;
            //DonViThucHien DVTH = ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;

            Row CrowCT = ws.Rows[InDexCT++];
            CrowCT[(int)MyConstant.LsTongHopEnum.STT].SetValueFromText("DA");
            //CrowCT[(int)MyConstant.LsTongHopEnum.Ten].SetValueFromText(slke_ThongTinDuAn.Text.ToUpper());
            CrowCT.Font.Bold = true;
            var grCongTrinh = dtCongTacTheoKy.AsEnumerable().GroupBy(x => x["CodeCongTrinh"].ToString());
            DateTime? dateBDKH, dateKTKH;
            int SoNgay = 0;
            foreach (var Ctrinh in grCongTrinh)
            {
                CrowCT = ws.Rows[InDexCT++];
                ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                CrowCT[(int)MyConstant.LsTongHopEnum.STT].SetValueFromText("CTR");
                CrowCT[(int)MyConstant.LsTongHopEnum.Ten].SetValueFromText(Ctrinh.First()["TenCongTrinh"].ToString().ToUpper());
                CrowCT.Font.Bold = true;
                CrowCT.Font.Color = MyConstant.color_Row_CongTrinh;
                var grHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"].ToString());
                foreach (var HM in grHangMuc)
                {
                    CrowCT = ws.Rows[InDexCT++];
                    ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                    CrowCT[(int)MyConstant.LsTongHopEnum.STT].SetValueFromText("HM");
                    CrowCT[(int)MyConstant.LsTongHopEnum.Ten].SetValueFromText(HM.First()["TenHangMuc"].ToString().ToUpper());
                    CrowCT.Font.Bold = true;
                    CrowCT.Font.Color = MyConstant.color_Row_HangMuc;

                    CrowCT = ws.Rows[InDexCT++];
                    ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                    CrowCT[(int)MyConstant.LsTongHopEnum.STT].SetValueFromText("DVTH");
                    //CrowCT[(int)MyConstant.LsTongHopEnum.Ten].SetValueFromText(DVTH.Ten.ToUpper());
                    CrowCT.Font.Bold = true;

                    var grPhanTuyen = HM.Where(x => x["CodePhanTuyen"] != DBNull.Value).GroupBy(x => x["CodePhanTuyen"].ToString());
                    foreach (var Tuyen in grPhanTuyen)
                    {
                        CrowCT = ws.Rows[InDexCT++];
                        ws.Rows.Insert(InDexCT, 1, RowFormatMode.FormatAsNext);
                        CrowCT[(int)MyConstant.LsTongHopEnum.STT].SetValueFromText("*T");
                        CrowCT[(int)MyConstant.LsTongHopEnum.Ten].SetValueFromText(Tuyen.First()["TenTuyen"].ToString().ToUpper());
                        CrowCT.Font.Bold = true;
                        CrowCT.Font.Color = Color.Red;

                        var grCongTacThuocTuyen = Tuyen.GroupBy(x => x["Code"]);
                        foreach (var CongTac in grCongTacThuocTuyen)
                        {
                            dateBDKH = (DateTime.TryParse(CongTac.First()["NgayBatDau"].ToString(), out DateTime dateBD) ? (DateTime?)dateBD : null);
                            dateKTKH = (DateTime.TryParse(CongTac.First()["NgayKetThuc"].ToString(), out DateTime dateKT) ? (DateTime?)(dateKT.Date.AddHours(23)) : null);
                            if (dateBDKH is null || dateKTKH is null)
                                continue;
                            SoNgay = (dateKTKH.Value.Date - dateBDKH.Value.Date).Days;
                            CrowCT = ws.Rows[InDexCT++];
                            ws.Rows.Insert(InDexCT, 3, RowFormatMode.FormatAsNext);
                            CrowCT.CopyFrom(RangCopy, PasteSpecial.All);
                            CrowCT[(int)MyConstant.LsTongHopEnum.STT].SetValue(stt++);
                            CrowCT[(int)MyConstant.LsTongHopEnum.NBD].SetValueFromText(dateBDKH.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                            CrowCT[(int)MyConstant.LsTongHopEnum.NKT].SetValueFromText(dateKTKH.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                            CrowCT[(int)MyConstant.LsTongHopEnum.DonVi].SetValue(CongTac.First()["DonVi"].ToString());
                            CrowCT[(int)MyConstant.LsTongHopEnum.KLHD].SetValue(CongTac.First()["KhoiLuongToanBo"].ToString());
                            CrowCT[(int)MyConstant.LsTongHopEnum.Ten].SetValueFromText(CongTac.First()["TenCongTac"].ToString());

                            SubMin = (int)(dateBDKH.Value.Date - Min.Date).TotalDays;
                            RangeFil = ws.Range[$"{ws.Columns[9 + SubMin].Heading}{InDexCT + 1}:{ws.Columns[9 + SubMin + SoNgay].Heading}{InDexCT + 1}"];
                            RangeFil.FillColor = MyConstant.DIC_Color[LoaiVL];
                            InDexCT = InDexCT + 2;

                        }
                    }

                    var grCongTac = HM.Where(x => x["CodePhanTuyen"] == DBNull.Value).GroupBy(x => x["Code"]);
                    foreach (var CongTac in grCongTac)
                    {
                        dateBDKH = (DateTime.TryParse(CongTac.First()["NgayBatDau"].ToString(), out DateTime dateBD) ? (DateTime?)dateBD : null);
                        dateKTKH = (DateTime.TryParse(CongTac.First()["NgayKetThuc"].ToString(), out DateTime dateKT) ? (DateTime?)(dateKT.Date.AddHours(23)) : null);
                        if (dateBDKH is null || dateKTKH is null)
                            continue;
                        SoNgay = (dateKTKH.Value.Date - dateBDKH.Value.Date).Days;
                        CrowCT = ws.Rows[InDexCT++];
                        ws.Rows.Insert(InDexCT, 3, RowFormatMode.FormatAsNext);
                        CrowCT.CopyFrom(RangCopy, PasteSpecial.All);
                        CrowCT[(int)MyConstant.LsTongHopEnum.STT].SetValue(stt++);
                        CrowCT[(int)MyConstant.LsTongHopEnum.NBD].SetValueFromText(dateBDKH.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                        CrowCT[(int)MyConstant.LsTongHopEnum.NKT].SetValueFromText(dateKTKH.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE));
                        CrowCT[(int)MyConstant.LsTongHopEnum.DonVi].SetValue(CongTac.First()["DonVi"].ToString());
                        CrowCT[(int)MyConstant.LsTongHopEnum.KLHD].SetValue(CongTac.First()["KhoiLuongToanBo"].ToString());
                        CrowCT[(int)MyConstant.LsTongHopEnum.Ten].SetValueFromText(CongTac.First()["TenCongTac"].ToString());

                        SubMin = (int)(dateBDKH.Value.Date - Min.Date).TotalDays;
                        RangeFil = ws.Range[$"{ws.Columns[9 + SubMin].Heading}{InDexCT + 1}:{ws.Columns[9 + SubMin + SoNgay].Heading}{InDexCT + 1}"];
                        RangeFil.FillColor = MyConstant.DIC_Color[LoaiVL];
                        InDexCT = InDexCT + 2;
                    }
                }
            }
            CellRange Visible = ws.Range[$"A{ws.Range[Range].BottomRowIndex - 2}:A{ws.Range[Range].BottomRowIndex + 1}"];
            //Visible.Select(x => x.RowIndex).ForEach(x => ws.Rows[x].Visible = false);
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Xuatexcel();
        }
    }
}