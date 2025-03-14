﻿using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
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
//using DevExpress.Spreadsheet;
using PhanMemQuanLyThiCong.Model.TDKH;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Model.Excel;
//using DocumentFormat = DevExpress.XtraRichEdit.DocumentFormat;
using DevExpress.Office.Utils;
using WordTable = DevExpress.XtraRichEdit.API.Native.Table;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.Office;
using PhanMemQuanLyThiCong.Common.MLogging;
using System.Windows.Forms.VisualStyles;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PM360.Common.Helper;
using MoreLinq;
using PhanMemQuanLyThiCong.Controls.BaoCao;
using PhanMemQuanLyThiCong.Common.Enums;
using DevExpress.PivotGrid.SliceQueryDataSource;
using StackExchange.Profiling.Internal;
using DevExpress.Spreadsheet;
using DevExpress.SpreadsheetSource.Implementation;
using System.Runtime.InteropServices;
using DevExpress.CodeParser;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using DevExpress.XtraRichEdit.Forms;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Import.Html;
using Row = DevExpress.Spreadsheet.Row;
using System.Drawing.Printing;
using PhanMemQuanLyThiCong.Model.DuAn;

namespace PhanMemQuanLyThiCong
{

    public partial class Form_BaoCaoHangNgay : DevExpress.XtraEditors.XtraForm
    {

        const string sheetNameBieu2A = sheetName2A;
        const string name2ATongCong = "TBT_TongCong";
        const string name2APhanTheoDonVi = "TBT_PhanTheoDonVi";

        const string nameBieu1TrucThuoc = "TBT_DonViTrucThuoc";
        const string nameBieu1DonViTuThucHien = "TBT_DonViTuThucHien";
        const string nameBieu1ThauPhuKhongTrucThuoc = "TBT_ThauPhuKhongTrucThuoc";
        const string nameBieu1BillChung = "TBT_BillChung";
        const string nameBieu1ChuaPhanKhai = "TBT_ChuaPhanKhai";
        const string nameBieu1VatTuACap = "TBT_VatTuACap";

        const string nameDuAn = "TBT_DuAn";
        const string nameTieuDe = "TBT_TieuDe";

        const string nameData = "TBT_Data";
        const string nameTime = "TBT_Time";
        const string nameTitle = "TBT_Title";



        private enum PeriodFilterType
        {
            CUSTOM,
            WEEKLY,
            MONTHLY,
            YEARLY
        }

        private class dfnDt
        {
            public DevExpress.Spreadsheet.DefinedName dfn { get; set; }
            public DataTable dt { get; set; }
            public int FirstRowInd { get; set; }
            public int Index { get; set; } = 0;
        }

        private enum Bieu2AType
        {
            TongCong,
            TheoDonViThucHien
        }

        private enum CT30Ngay
        {
            STT,
            Ten,
            DonVi,
            LyTrinh,
            KeHoach,
            ThucHien,
            DonGia,
            ThanhTienKeHoach,
            TyLe
        }

        private enum CTTrongNgay
        {
            STT,
            Ten,
            DonVi,
            KLHopDong,
            KLThiCong,
            KLLuyKeThiCong,
            KLConLai,
            DonGiaHopDong,
            DonGiaThiCong,
            TTHopDong,
            TTThiCong,
            TTLuyKeThiCong,
            TTConLai,
            TyLeHoanThanh
        }


        //public enum CTTrongNgayBrief
        //{
        //    STT,
        //    Ten,

        //}

        private enum CTHTEnum
        {
            STT,
            Ten,
            DonVi,
            LyTrinh,
            KLThiCong,
            NguoiYeuCau,
            NguoiDuyet,
            ThoiGianDuyet
        }

        private enum ThuChiEnum
        {
            STT,
            NoiDung,
            SoTien,
            NguoiNhan,
            GhiChu
        }

        private enum NhanLucEnum
        {
            STT,
            TenDonViThucHien,
            KeHoach,
            ThucTe,
            GhiChu
        };

        private enum VatTuEnum
        {
            STT,
            Ten,
            DonVi,
            KhoiLuongHopDong,
            KLDeXuat,
            KLNhap,
            KLDaXuat,
            NhaCungCap
        }

        private enum GiaiDoanTypeEnum
        {
            TUDO,
            THEOTUAN,
            THEOTHANG,
            THEONAM
        }

        private enum CongTacBaoCaoTypeEnum
        {
            FULL,
            CHILAYMAGOP
        }

        string[] FilesBaoCaoHangNgay =
        {
            "_OFFLINE_[Full] Báo cáo hằng ngày.docx",
            "_OFFLINE_[Rút gọn] Báo cáo hằng ngày.docx"
        };

        public Form_BaoCaoHangNgay()
        {
            InitializeComponent();


            uc_BaoCao1.LoadSetting(BaoCaoFileType.WORD);
            //uc_BaoCao1.
            uc_BaoCao1.CustomPreviewIndexChanged -= rg_XemTruoc_SelectedIndexChanged;
            uc_BaoCao1.PreviewSelectedIndex = 0;
            uc_BaoCao1.CustomPreviewIndexChanged += rg_XemTruoc_SelectedIndexChanged;
            nud_year.Value = DateTime.Now.Year;
            monthEdit1.Month = DateTime.Now.Month;
            LoadDate();
            //LoadBaoCao();

        }

        private async void Form_BaoCaoHangNgay_Load(object sender, EventArgs e)
        {

            //this.Controls.OfType<DateEdit>().ForEach(x => x.DateTime = DateTime.Now.Date);
            LoadMauBaoCao();
            await Fcn_UpdateDA();
        }
        private void LoadMauBaoCao()
        {
            //Load các mẫu báo cáo
            string TemplateFolder = $@"{BaseFrom.m_templatePath}\FileReport";
            //var filenames = Directory.GetFiles(TemplateFolder);
            var files = Directory.GetFiles(TemplateFolder).Select(x => new FileViewModel()
            {
                FilePath = x
            }).ToList();

            if (rb_onl.Checked)
            {
                files = files.Where(x => !x.FileName.StartsWith("_OFFLINE_")).OrderBy(x => x.FileName).ToList();
            }
            else
                files = files.Where(x => !x.FileName.StartsWith("_ONLINE_")).OrderByDescending(x => x.FileName == "_OFFLINE_Báo cáo hằng ngày.xlsx").ToList();



            if (!files.Any())
            {
                MessageShower.ShowInformation("Không có mẫu báo cáo nào!");
                Close();
                return;
            }

            slke_ChonMauBaoCao.Properties.DataSource = files;
            slke_ChonMauBaoCao.EditValue = files.First();
        }

        public async Task Fcn_UpdateDA()
        {
            if (!rb_onl.Checked)
            {
                List<Tbl_ThongTinDuAnViewModel> lst = SharedControls.slke_ThongTinDuAn.Properties.DataSource as List<Tbl_ThongTinDuAnViewModel>;
                List<Tbl_ThongTinDuAnViewModel> Manage = lst.FindAll(x => x.Code == "Manage").ToList();
                if (Manage.Count() != 0)
                    lst.Remove(Manage.SingleOrDefault());
                slke_ThongTinDuAn.Properties.DataSource = lst;
                //FileHelper.fcn_spSheetStreamDocument(spreadsheetControl1, Path.Combine(BaseFrom.m_path, "Template", "FileExcel", "BaoCaoTongHop.xlsx"));
                de_begin.DateTime = DateTime.Now.AddDays(-1);
            }
            else
            {
                //Load Dự Án
                var res = await CusHttpClient.InstanceCustomer.MGetAsync<List<Tbl_ThongTinDuAnViewModel>>($"{Server.Tbl_ThongTinDuAn}/{RouteAPI.SUFFIX_GetAll}");

                if (res.MESSAGE_TYPECODE)
                {
                    var das = res.Dto;
                    slke_ThongTinDuAn.Properties.DataSource = res.Dto;
                }
                else
                {
                    MessageShower.ShowError("Không thể tải thông tin dự án. Kiểm tra kết nối Internet!");
                    AlertShower.ShowInfo(res.MESSAGE_CONTENT);
                    rb_off.Checked = true;

                }
            }
        }
        private List<DonViThucHien> GetDonViThucHiens()
        {
            string codeDA = slke_ThongTinDuAn.EditValue as string;

            string[] tbls =
            {
                MyConstant.TBL_THONGTINNHATHAU,
                MyConstant.TBL_THONGTINNHATHAUPHU,
                MyConstant.TBL_THONGTINTODOITHICONG,
            };
            //if (NhaCungCap)
            //{
            //    var lst = tbls.ToList();
            //    lst.Add(MyConstant.TBL_THONGTINNHACUNGCAP);
            //    tbls = lst.ToArray();
            //}
            List<DonViThucHien> DVTHS = new List<DonViThucHien>();

            foreach (string tbl in tbls)
            {
                string dbString = $"SELECT * FROM \"{tbl}\" WHERE \"CodeDuAn\" = '{codeDA}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                foreach (DataRow dr in dt.Rows)
                {
                    var newDVTH = new DonViThucHien()
                    {
                        Code = dr["Code"].ToString(),
                        CodeDuAn = codeDA,
                        Ten = dr["Ten"].ToString(),
                        Table = tbl,
                    };

                    if (dt.Columns.Contains("CodeTongThau"))
                    {
                        newDVTH.CodeTongThau = dr["CodeTongThau"].ToString();
                    }

                    DVTHS.Add(newDVTH);
                }
            }
            return DVTHS;
        }
        private double Fcn_TinhToan(double KL)
        {
            double TT = KL;
            double TTUP = Math.Ceiling(TT);
            double TTDown = Math.Floor(TT);
            if (TT + 0.5 == TTUP)
                return TT;
            else if (TTDown == TTUP)
                return TTDown;
            else if (KL == 0)
                return 0;
            else if (TT - 0.5 < TTDown)
                return TTDown + 0.5;
            else if (TT == TTUP || TT - 0.5 > TTDown)
                return TTUP;

            return TT;
        }
        private double Fcn_TinhToanMTC(double KL)
        {
            double TT = KL;



            return TT;
        }

        const string sheetName2A = "Bieu 2A- BĐH";
        const string sheetName1 = "Bieu 1-BĐH";

        private void sb_xuatbaocao_Click(object sender, EventArgs e)
        {

        }

        private void LoadBaoCao()
        {
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;

            if (uc_BaoCao1.PreviewAccessibleName != "HoanChinh")
            {
                if (fileMaus.FileName.EndsWith(".xlsx"))
                {
                    uc_BaoCao1.LoadSetting(BaoCaoFileType.EXCEL);
                    uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);
                    if (fileMaus.FileName == "Báo cáo biểu 1_BĐH.xlsx")
                    {
                        if (lci_year.Visible)
                            Fcn_LoadSetting(new DateTime((int)nud_year.Value, 1, 1));
                        else
                            Fcn_LoadSetting(DateTime.Now);
                    }
                }
                else if (fileMaus.FileName.EndsWith(".docx"))
                {
                    uc_BaoCao1.LoadSetting(BaoCaoFileType.WORD);
                    uc_BaoCao1.RichEditControl.LoadDocument(m_Path);
                }
                else
                {
                    MessageShower.ShowWarning("Mẫu không hỗ trợ");
                    return;
                }
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang tải báo cáo!");
            if (FilesBaoCaoHangNgay.Contains(fileMaus.FileName))
            {
                LoadBaoCaoHangNgay();
            }
            else if (fileMaus.FileName == "Báo cáo biểu 2A_BĐH.xlsx")
            {
                LoadBaoCaoBieu2A();
            }
            else if (fileMaus.FileName == "Báo cáo biểu 1_BĐH.xlsx")
            {
                LoadBaoCaoBieu1();
            }
            else if (fileMaus.FileName == "_ONLINE_Báo cáo tổng hợp tất cả dự án.xlsx")
            {
                LoadBaoCaoOnlineTongHopDuAn();
            }
            else if (fileMaus.FileName == "_ONLINE_Báo cáo hàng ngày.xlsx")
            {
                LoadBaoCaoOnlineNgay();
            }
            else if (fileMaus.FileName == "_ONLINE_Báo cáo tổng hợp dự kiến thi công.xlsx")
            {
                LoadBaoCaoOnlineTongHopDuKienThiCong();
            }
            else if (fileMaus.FileName == "_ONLINE_Tổng hợp giá trị thực hiện.xlsx")
            {
                LoadBaoCaoOnlineTongHopGiaTriThucHien();
            }

            else if (fileMaus.FileName == "_ONLINE_Báo cáo hợp đồng_Thanh toán.xlsx")
            {
                LoadBaoCaoOnlineHopDongThanhToan();
            }
            else if (fileMaus.FileName == "_OFFLINE_Báo cáo hằng ngày.xlsx")
            {
                LoadBaoCaoHN();
            }
            WaitFormHelper.CloseWaitForm();
        }

        private async void LoadBaoCaoBieu2A()
        {
            if (slke_ThongTinDuAn.EditValue == null)
            {
                MessageShower.ShowError("Vui lòng chọn dự án!");
                uc_BaoCao1.PreviewSelectedIndex = 0;
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu!! Vui lòng chờ!!!!!");

            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);

            var wb = uc_BaoCao1.SpreadSheet.Document;
            var ws = wb.Worksheets[sheetName2A];

            var nbdKyTruoc = de_StartDatePrevPeriod.DateTime.Date;
            var nbdKyNay = de_StartDateCrPeriod.DateTime.Date;
            var nbdKySau = de_StartDateNextPeriod.DateTime.Date;


            var nktKyTruoc = de_EndDatePrevPeriod.DateTime.Date;
            var nktKyNay = de_EndDateCrPeriod.DateTime.Date;
            var nktKySau = de_EndDateNextPeriod.DateTime.Date;

            ws.Range[nameDuAn].SetValue($"Dự án: {slke_ThongTinDuAn.Text}".ToUpper());
            ws.Range[nameTieuDe].SetValue($"Báo cáo khối lượng thực hiện từ {nbdKyNay.ToShortDateString()} đến {nktKyNay.ToShortDateString()}");

            if (slke_ThongTinDuAn.EditValue == null)
            {
                MessageShower.ShowError("Vui lòng chọn dự án!");
                uc_BaoCao1.PreviewSelectedIndex = 0;
                WaitFormHelper.CloseWaitForm();
                return;
            }

            string conditionDMCT = null;
            if (cbe_LocCongTac.SelectedIndex == (int)CongTacBaoCaoTypeEnum.CHILAYMAGOP)
            {
                conditionDMCT = $"dmct.CodeGop IS NOT NULL";
            }

            List<CongTacTheoGiaiDoanExtension> dtTong = new List<CongTacTheoGiaiDoanExtension>();
            List<KLHN> KLTCsFull = new List<KLHN>();
            List<DateTime> dates = new List<DateTime>()
            {
                nbdKyTruoc,
                nktKyTruoc,
                nbdKyNay,
                nktKyNay,
                nbdKySau,
                nktKySau
            };
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            ws.Rows[6][dic[TDKH.COL_LuyKeKyTruoc]].Value = $"Lũy kế kỳ trước (Từ khởi công đến {nktKyTruoc.ToShortDateString()})";
            ws.Rows[6][dic[TDKH.COL_ThucHienKyNay]].Value = $"Thực hiện kỳ này (Từ {nbdKyNay.ToShortDateString()} đến {nktKyTruoc.ToShortDateString()})";

            if (rb_off.Checked)
            {
                dtTong = TDKHHelper.GetCongTacsModel(slke_ThongTinDuAn.EditValue.ToString(), null, null, conditionDMCT: conditionDMCT, whereCondition: "cttk.Code IS NOT NULL");
                var codes = dtTong.Select(x => x.Code);
                KLTCsFull = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, codes, dates);


            }
            else
            {
                var result = await CusHttpClient.InstanceCustomer
                    .MPostAsJsonAsync<CongTacWithKLHN>(RouteAPI.TongDuAn_GetCongTacTDKHWithKLHN,
                     new KLHNRequest()
                     {
                         CodeDuAn = slke_ThongTinDuAn.EditValue.ToString(),

                         type = TypeKLHN.CongTac,
                         ConditionDMCT = conditionDMCT,
                         IsGetPrjHaveOwner = false,
                         dates = dates,
                     });

                if (!result.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError("Không thể tải dự án từ server");
                    WaitFormHelper.CloseWaitForm();
                    return;
                }

                dtTong = result.Dto.Cttks;
                KLTCsFull = result.Dto.KLHNs;
            }

            var dfnTongCong = ws.DefinedNames.Single(x => x.Name == name2ATongCong);
            var dfnDVTH = ws.DefinedNames.Single(x => x.Name == name2APhanTheoDonVi);

            DataTable dtDataTongCong = DataTableCreateHelper.BaoCaoBieu2A_BDH();
            DataTable dtDataDVTH = DataTableCreateHelper.BaoCaoBieu2A_BDH();

            var lsData = new List<dfnDt>()
            {
                new dfnDt {dfn = dfnDVTH, dt = dtDataDVTH},
                new dfnDt {dfn = dfnTongCong, dt = dtDataTongCong}
            };

            foreach (var item in lsData)
            {
                var dfn = item.dfn;
                if (dfn.Range.RowCount > 2)
                {
                    ws.Rows.Remove(dfn.Range.TopRowIndex + 1, dfn.Range.RowCount - 2);
                }
                item.FirstRowInd = dfn.Range.TopRowIndex;


            }

            var grs = dtTong.Where(x => x.CodeDVTH.HasValue()).GroupBy(x => x.CodeDVTH);

            //var crRowInd = dfnDVTH.Range.TopRowIndex;
            //int rowTongInd = crRowInd;
            //int STTDVTH = 0;

            DataTable dtData = null;
            int indInList = 0;

            foreach (var grDVTH in grs)
            {
                var fstDVTH = grDVTH.First();
                if (fstDVTH.CodeNhaThau is null)
                {

                    indInList = 0;

                    dtData = lsData[indInList].dt;
                    DataRow newRowDVTH = dtData.NewRow();
                    dtData.Rows.Add(newRowDVTH);

                    //var crRowDVTHInd = ++crRowInd;
                    lsData[indInList].Index += 1;


                    newRowDVTH[TDKH.COL_STT] = lsData[indInList].Index;

                    //newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                    newRowDVTH[TDKH.COL_DanhMucCongTac] = fstDVTH.TenDVTH.ToUpper();
                    newRowDVTH[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVTong;

                }
                else
                {
                    indInList = 1;
                    dtData = lsData[indInList].dt;
                    lsData[indInList].Index += 1;

                }

                var itemData = lsData[indInList];
                int STTDVTH = itemData.Index;

                var grsCongTrinh = grDVTH.GroupBy(x => x.CodeDVTH);
                int STTCTrinh = 0;
                foreach (var grCTrinh in grsCongTrinh)
                {
                    DataRow newRowCtrinh = dtData.NewRow();
                    dtData.Rows.Add(newRowCtrinh);

                    //var crRowCtrInd = ++crRowInd;

                    var fstCtr = grCTrinh.First();
                    newRowCtrinh[TDKH.COL_STT] = $"{STTDVTH}.{++STTCTrinh}";
                    //newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                    newRowCtrinh[TDKH.COL_DanhMucCongTac] = $"CTR: {fstCtr.TenCongTrinh}";
                    newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;


                    var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                    int STTHM = 0;

                    int sorid = 0;
                    foreach (var grHM in grsHM)
                    {
                        //int sttWholeHM = 0;
                        DataRow newRowHM = dtData.NewRow();
                        dtData.Rows.Add(newRowHM);

                        //var crRowHMInd = ++crRowInd;

                        var fstHM = grHM.First();
                        newRowHM[TDKH.COL_STT] = $"{STTDVTH}.{STTCTrinh}.{++STTHM}";
                        //newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                        newRowHM[TDKH.COL_DanhMucCongTac] = $"HM: {fstHM.TenHangMuc}";
                        newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;

                        var grsPhanTuyen = grHM.GroupBy(x => x.indPT)
                            .OrderBy(x => x.Key);

                        foreach (var grPhanTuyen in grsPhanTuyen)
                        {
                            var fstPT = grPhanTuyen.First();

                            string codePT = fstPT.CodePhanTuyen;
                            bool isPhanTuyen = codePT.HasValue();
                            int? crRowPTInd = null;
                            if (isPhanTuyen)
                            {
                                DataRow newRowPT = dtData.NewRow();
                                dtData.Rows.Add(newRowPT);

                                crRowPTInd = itemData.FirstRowInd + dtData.Rows.Count;

                                //newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                                newRowPT[TDKH.COL_DanhMucCongTac] = $"*T: {fstPT.TenPhanTuyen}";
                                newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                            }

                            int STTCTac = 0;

                            if (cbe_LocCongTac.SelectedIndex == (int)CongTacBaoCaoTypeEnum.CHILAYMAGOP)
                            {
                                var grsGop = grPhanTuyen.GroupBy(x => new { x.TenGopCongTac, x.DonVi });

                                foreach (var grGop in grsGop)
                                {
                                    //++crRowInd;
                                    var codesCTacInGop = grGop.Select(x => x.Code);

                                    //var klhnFullInGop = KLTCsFull.Where(x => codesCTacInGop.Contains(x.CodeCha));


                                    var fstGop = grGop.First();
                                    DataRow newRowGop = dtData.NewRow();
                                    dtData.Rows.Add(newRowGop);

                                    int crRowInd = itemData.FirstRowInd + dtData.Rows.Count;


                                    //newRowGop[TDKH.COL_STT] = ++STTCTac;
                                    //newRowGop[TDKH.COL_DanhMucCongTac] = fstGop.TenGopCongTac;
                                    //newRowGop[TDKH.COL_DonVi] = fstGop.DonVi;
                                    //newRowGop[TDKH.COL_KhoiLuongHopDongChiTiet] = grGop.Sum(x => x.KhoiLuongHopDongChiTiet);
                                    //newRowGop[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{grGop.Min(x => x.NgayBatDau).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}";
                                    //newRowGop[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{grGop.Max(x => x.NgayKetThuc).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}";
                                    //newRowGop[TDKH.COL_LuyKeKyTruoc] = KLFromBeginOfPrj.Sum(x => x.KLTCInRange) ?? 0;
                                    //newRowGop[TDKH.COL_ThucHienKyNay] = KLInRange.Sum(x => x.KLTCInRange) ?? 0;
                                    //newRowGop[TDKH.COL_KeHoachKyNay] = KLInRange.Sum(x => x.KLKHInRange) ?? 0;

                                    //newRowGop[TDKH.COL_KhoiLuongDaThiCong] = MultiBriefInGop.Sum(x => x.Briefs.Sum(y => y.KLTCInRange)) ?? 0;
                                    //newRowGop[TDKH.COL_PhanTramThucHien] = $"{MyConstant.PrefixFormula}({dic[TDKH.COL_KeHoachKyNay]}{crRowInd + 1}>0);{dic[TDKH.COL_ThucHienKyNay]}{crRowInd + 1}/{dic[TDKH.COL_KeHoachKyNay]}{crRowInd + 1};\"\"";
                                    //newRowGop[TDKH.COL_KhoiLuongConLai] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1} - {dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}";
                                    //newRowGop[TDKH.COL_KeHoachKySau] = KLNextRange.Sum(x => x.KLKHInRange) ?? 0;
                                    //newRowGop[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                }
                            }
                            else
                            {
                                foreach (var ctac in grPhanTuyen)
                                {
                                    //++crRowInd;

                                    //var klhnFullInGop = KLTCsFull.Where(x => codesCTacInGop.Contains(x.CodeCha));

                                    var klhnFullInGop = KLTCsFull.Where(x => ctac.Code == x.ParentCode);

                                    DataRow newRowGop = dtData.NewRow();
                                    dtData.Rows.Add(newRowGop);

                                    var StartDatePrevPeriod = klhnFullInGop.Single(x => x.Ngay == nbdKyTruoc);
                                    var EndDatePrevPeriod = klhnFullInGop.Single(x => x.Ngay == nktKyTruoc);
                                    var StartDateCrPeriod = klhnFullInGop.Single(x => x.Ngay == nbdKyNay);
                                    var EndDateCrPeriod = klhnFullInGop.Single(x => x.Ngay == nktKyNay);
                                    var StartDateNextPeriod = klhnFullInGop.Single(x => x.Ngay == nbdKySau);
                                    var EndDateNextPeriod = klhnFullInGop.Single(x => x.Ngay == nktKySau);

                                    int crRowInd = itemData.FirstRowInd + dtData.Rows.Count;

                                    newRowGop[TDKH.COL_STT] = ++STTCTac;
                                    newRowGop[TDKH.COL_DanhMucCongTac] = ctac.TenCongTac;
                                    newRowGop[TDKH.COL_DonVi] = ctac.DonVi;
                                    newRowGop[TDKH.COL_KhoiLuongHopDongChiTiet] = ctac.KhoiLuongHopDongChiTiet;
                                    newRowGop[TDKH.COL_NgayBatDau] = $"{MyConstant.PrefixDate}{ctac.NgayBatDau.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}";
                                    newRowGop[TDKH.COL_NgayKetThuc] = $"{MyConstant.PrefixDate}{ctac.NgayKetThuc.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}";
                                    newRowGop[TDKH.COL_LuyKeKyTruoc] = EndDatePrevPeriod.LuyKeKhoiLuongThiCong??0;
                                    newRowGop[TDKH.COL_ThucHienKyNay] = (EndDateCrPeriod.LuyKeKhoiLuongThiCong ?? 0) - (StartDateCrPeriod.LuyKeKhoiLuongThiCong??0);
                                    newRowGop[TDKH.COL_KeHoachKyNay] = (EndDateCrPeriod.LuyKeKhoiLuongKeHoach ?? 0)- (StartDateCrPeriod.LuyKeKhoiLuongKeHoach ?? 0);

                                    newRowGop[TDKH.COL_KhoiLuongDaThiCong] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_LuyKeKyTruoc]}{crRowInd + 1}+{dic[TDKH.COL_ThucHienKyNay]}{crRowInd + 1}";
                                    newRowGop[TDKH.COL_PhanTramThucHien] = $"{MyConstant.PrefixFormula}IF({dic[TDKH.COL_KeHoachKyNay]}{crRowInd + 1} = 0; \"\";{dic[TDKH.COL_ThucHienKyNay]}{crRowInd + 1}/{dic[TDKH.COL_KeHoachKyNay]}{crRowInd + 1})";
                                    newRowGop[TDKH.COL_KhoiLuongConLai] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd + 1} - {dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1}";
                                    newRowGop[TDKH.COL_KeHoachKySau] = (EndDateNextPeriod.LuyKeKhoiLuongKeHoach ??0) - (StartDateNextPeriod.LuyKeKhoiLuongKeHoach ?? 0);
                                    newRowGop[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                }
                            }


                            if (isPhanTuyen)
                            {
                                DataRow newRowPT = dtData.NewRow();
                                dtData.Rows.Add(newRowPT);

                                //++crRowInd;

                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                //newRowPT[TDKH.COL_Code] = codePT;
                                //newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;

                                newRowPT[TDKH.COL_DanhMucCongTac] = $"{MyConstant.PrefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                                newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                //newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";
                            }
                        }
                    }
                }
            }

            foreach (var item in lsData)
            {
                var dfn = item.dfn;
                var dt = item.dt;

                if (dt.Rows.Count == 0)
                    continue;

                ws.Rows.Insert(dfn.Range.BottomRowIndex, dt.Rows.Count, RowFormatMode.FormatAsNext);
                ws.Import(dt, false, dfn.Range.TopRowIndex + 1, 0);
                SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfn.Range);
                SpreadsheetHelper.FormatRowsInRange(dfn.Range, dic[TDKH.COL_TypeRow]);
                WaitFormHelper.CloseWaitForm();
            }


        }
        private void Fcn_LoadSetting(DateTime Now)
        {
            var wb = uc_BaoCao1.SpreadSheet.Document;
            var ws = wb.Worksheets[sheetName1];
            wb.BeginUpdate();
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            DateTime Last = new DateTime(Now.Year - 1, 12, 31);
            ws.Rows[6][dic["LuyKeNamTruoc"]].SetValueFromText($"Lũy kế thực hiện đến {Last.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}");
            //ws.Rows[6][dic["LuyKeKyTruoc"]].SetValueFromText($"Lũy kế thực hiện từ đầu năm đến hết kỳ trước (Từ " +
            //    $"ngày {de_StartDatePrevPeriod.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} đến {de_EndDatePrevPeriod.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)})");
            ws.Rows[6][dic["ThucHienKyNay"]].SetValueFromText($"Thực hiện kỳ này từ " +
                $"ngày {de_StartDateCrPeriod.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} đến {de_EndDateCrPeriod.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}");
            ws.Rows[6][dic["KeHoachKySau"]].SetValueFromText($"Kế hoạch kỳ sau từ " +
                $"ngày {de_StartDateNextPeriod.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} đến {de_EndDateNextPeriod.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}");
            ws.Rows[6][dic["LuyKeTuDauNam"]].SetValueFromText($"Lũy kế thực hiện từ đầu năm {Now.Year}");
            ws.Rows[6][dic["KeHoachNamNay"]].SetValueFromText($"Kế hoạch cả năm {Now.Year}");
            ws.Range["ThongTinNam"].SetValue($"Kế hoạch thi công năm {Now.Year}(nêu từng đơn vị): Nêu cả kế hoạch giá trị SX, NT, TT, các công việc chính triển khai");
            ws.Range["ThongTinNamNext"].SetValue($"Kế hoạch đảm bảo vật tư, vật liệu, XMTB, vốn thi công {Now.Year} cho Ban điều hành trực tiếp thi công:");
            wb.EndUpdate();
        }
        private async void LoadBaoCaoBieu1()
        {
            if (slke_ThongTinDuAn.EditValue == null)
            {
                MessageShower.ShowError("Vui lòng chọn dự án!");
                uc_BaoCao1.PreviewSelectedIndex = 0;
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu!! Vui lòng chờ!!!!!");
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);

            var wb = uc_BaoCao1.SpreadSheet.Document;
            var ws = wb.Worksheets[sheetName1];

            var nbdKyTruoc = de_StartDatePrevPeriod.DateTime.Date;
            var nbdKyNay = de_StartDateCrPeriod.DateTime.Date;
            var nbdKySau = de_StartDateNextPeriod.DateTime.Date;


            var nktKyTruoc = de_EndDatePrevPeriod.DateTime.Date;
            var nktKyNay = de_EndDateCrPeriod.DateTime.Date;
            var nktKySau = de_EndDateNextPeriod.DateTime.Date;
            DateTime Now = DateTime.Now;
            if (lci_year.Visible)
                Now = new DateTime((int)nud_year.Value - 1, 12, 31);
            else
                Now = new DateTime(de_begin.DateTime.Year - 1, 12, 31);
            int crYear = nbdKyTruoc.Year;
            DateTime lastDatePrevYear = new DateTime(crYear - 1, 12, 31);
            DateTime lastDateCrYear = new DateTime(crYear, 12, 31);
            DateTime fstDateCrYear = new DateTime(crYear, 1, 1);


            ws.Range[nameDuAn].SetValue($"Dự án: {slke_ThongTinDuAn.Text}".ToUpper());
            ws.Range[nameTieuDe].SetValue($"Báo cáo khối lượng thực hiện từ {nbdKyNay.ToShortDateString()} đến {nktKyNay.ToShortDateString()}".ToUpper());
            if (lci_year.Visible)
                Fcn_LoadSetting(new DateTime((int)nud_year.Value, 1, 1));
            else
                Fcn_LoadSetting(DateTime.Now);
            if (slke_ThongTinDuAn.EditValue == null)
            {
                MessageShower.ShowError("Vui lòng chọn dự án!");
                uc_BaoCao1.PreviewSelectedIndex = 0;
                WaitFormHelper.CloseWaitForm();
                return;
            }

            string conditionDMCT = null;
            if (cbe_LocCongTac.SelectedIndex == (int)CongTacBaoCaoTypeEnum.CHILAYMAGOP)
            {
                conditionDMCT = $"dmct.CodeGop IS NOT NULL";
            }

            //string conditionDVTH = (type == Bieu2AType.TongCong) ? "cttk.CodeNhaThau IS NOT NULL" : "cttk.CodeNhaThau IS NULL";

            //TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);
            List<CongTacTheoGiaiDoanExtension> dtTong = new List<CongTacTheoGiaiDoanExtension>();
            List<KLHN> KLTCsFull = new List<KLHN>();
            DataTable dtHopDong = null;
            List<DateTime> dates = new List<DateTime>()
                {
                    nbdKyTruoc,
                    nktKyTruoc,
                    nbdKyNay,
                    nktKyNay,
                    nbdKySau,
                    nktKySau,
                    lastDatePrevYear,
                    lastDateCrYear,
                    //fstDateCrYear,
                };
            wb.BeginUpdate();
            if (rb_off.Checked)
            {
                dtTong = TDKHHelper.GetCongTacsModel(slke_ThongTinDuAn.EditValue.ToString(), null, null, conditionDMCT: conditionDMCT, whereCondition: "cttk.Code IS NOT NULL");
                string dbString = $"SELECT Dot.Code FROM {MyConstant.TBL_HopDong_DotHopDong} Dot" +
                    $" LEFT JOIN {TDKH.TBL_GiaiDoanThucHien} GD ON GD.Code=Dot.CodeGiaiDoan WHERE GD.CodeDuAn='{slke_ThongTinDuAn.EditValue}' AND Dot.NgayKetThuc <='{Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
                DataTable dtDot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                string[] lstcodedot = dtDot.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                dbString = $"SELECT SUM(PL.DonGia*HT.KyNayHopDongNghiemThu) AS KyNayHopDongNghiemThu," +
                    $"SUM(PL.DonGia*HT.LuyKeDenHetKyTruoc) AS LuyKeDenHetKyTruoc," +
                    $"SUM(PL.DonGia*HT.LuyKeDenHetKyNay) AS LuyKeDenHetKyNay," +
                    $"TOTAL(PL.DonGia*HTNext.KyNayHopDongNghiemThu) AS KyNayHopDongNghiemThuNext," +
                    $"SUM(PL.DonGia*HT.DangDo) AS DangDo," +
                    $"cttk.Code " +
$"FROM {MyConstant.TBL_hopdongAB_HT} HT " +
$" LEFT JOIN {MyConstant.TBL_hopdongAB_HT} HTNext " +
$"ON (HTNext.CodeDB=HT.CodeDB AND HTNext.CodeDot IN ({MyFunction.fcn_Array2listQueryCondition(lstcodedot)}))" +
$"INNER JOIN {MyConstant.TBL_HopDong_DoBoc} DB " +
$"ON DB.Code = HT.CodeDB " +
$"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} PL " +
$"ON PL.Code = DB.CodePL" +
$" JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk ON cttk.Code=PL.CodeCongTacTheoGiaiDoan " +
$"LEFT JOIN {TDKH.TBL_GiaiDoanThucHien} GD ON GD.Code=cttk.CodeGiaiDoan" +
//$"LEFT JOIN {MyConstant.TBL_HopDong_DoBoc} DBNext ON DBNext.Code = HTNext.CodeDB" +
//$" LEFT JOIN {MyConstant.TBL_HopDong_DotHopDong} Dot ON DBNext.CodeDot=Dot.Code AND " +
$" WHERE GD.CodeDuAn='{slke_ThongTinDuAn.EditValue}' GROUP BY cttk.Code ";
                dtHopDong = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                var codesDMCTacGiaoThau = dtTong.Where(x => x.Code.HasValue()).Select(x => x.CodeCongTac).ToList();
                dbString = $"SELECT CodeCongTac, TOTAL(KhoiLuongToanBo) AS KhoiLuongDaGiao FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE CodeCongTac IN ({MyFunction.fcn_Array2listQueryCondition(codesDMCTacGiaoThau)}) AND CodeNhaThau IS NULL";
                DataTable dtSum = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                foreach (var ctac in dtTong.Where(x => x.CodeNhaThau != null))
                {
                    ctac.KhoiLuongDaGiao = dtSum.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == ctac.CodeCongTac).Sum(x => (double)x["KhoiLuongDaGiao"]);
                }

                var codes = dtTong.Select(x => x.Code);

                //var KLHNKyNays = MyFunction.Fcn_CalKLKHModel(TypeKLHN.CongTac, codes, nbdKyNay, nktKyNay, ignoreKLKH: true, ignoreKLNhanThau: true);
                //var KLHNKyTruocs = MyFunction.Fcn_CalKLKHModel(TypeKLHN.CongTac, codes, nbdKyTruoc, nktKyTruoc, ignoreKLKH: false, ignoreKLNhanThau: true);
                //var KLHNKySaus = MyFunction.Fcn_CalKLKHModel(TypeKLHN.CongTac, codes, nbdKySau, nktKySau, ignoreKLKH: false, ignoreKLNhanThau: true);


                KLTCsFull = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, codes, dates);
                //var KLKHsFull = MyFunction.Fcn_CalKLKHModel(TypeKLHN.CongTac, codes, ignoreKLKH: false, ignoreKLNhanThau: true);
            }
            else
            {
                var result = await CusHttpClient.InstanceCustomer
                    .MPostAsJsonAsync<CongTacWithKLHN>(RouteAPI.TongDuAn_GetCongTacTDKHWithKLHN,
                     new KLHNRequest()
                     {
                         CodeDuAn = slke_ThongTinDuAn.EditValue.ToString(),
                         ConditionDMCT = conditionDMCT,

                         IsGetPrjHaveOwner = false,
                         dates = dates
                     });

                if (!result.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError("Không thể tải dự án từ server");
                    WaitFormHelper.CloseWaitForm();
                    wb.EndUpdate();
                    return;
                }

                dtTong = result.Dto.Cttks;
                KLTCsFull = result.Dto.KLHNs;

                //var grCtacs = dtTong.GroupBy(x => x.Code);

            }
            


            var dfnTrucThuoc = ws.DefinedNames.Single(x => x.Name == nameBieu1TrucThuoc);
            var dfnTuTH = ws.DefinedNames.Single(x => x.Name == nameBieu1DonViTuThucHien);
            var dfnKhongTrucThuoc = ws.DefinedNames.Single(x => x.Name == nameBieu1ThauPhuKhongTrucThuoc);
            var dfnBillChung = ws.DefinedNames.Single(x => x.Name == nameBieu1BillChung);
            var dfnChuaPhanKhai = ws.DefinedNames.Single(x => x.Name == nameBieu1ChuaPhanKhai);

            DataTable dtDataTrucThuoc = DataTableCreateHelper.BaoCaoBieu1_BDH();
            DataTable dtDataTuThucHien = DataTableCreateHelper.BaoCaoBieu1_BDH();
            DataTable dtDataKhongTrucThuoc = DataTableCreateHelper.BaoCaoBieu1_BDH();
            DataTable dtDataBillChung = DataTableCreateHelper.BaoCaoBieu1_BDH();


            var lsData = new List<dfnDt>()
            {
                new dfnDt {dfn = dfnBillChung, dt = dtDataBillChung},
                new dfnDt {dfn = dfnKhongTrucThuoc, dt = dtDataKhongTrucThuoc},
                new dfnDt {dfn = dfnTuTH, dt = dtDataTuThucHien},
                new dfnDt {dfn = dfnTrucThuoc, dt = dtDataTrucThuoc},
            };
            string[] colsSum = new string[]
{
                TDKH.COL_GiaTriHopDongWithCPDP,
                TDKH.COL_GiaTriHopDongWithoutCPDP,
                TDKH.COL_LuyKeHetNamTruoc,
                TDKH.COL_LuyKeKyTruoc,
                TDKH.COL_ThucHienKyNay,
                TDKH.COL_KLThanhToan,
                TDKH.COL_LuyKeTuDauNam,
                TDKH.COL_KhoiLuongDaThiCong,
                TDKH.COL_KeHoachKySau,
                TDKH.COL_KeHoachNamNay,
                $"NT_{TDKH.COL_LuyKeHetNamTruoc}",
                $"NT_{TDKH.COL_LuyKeKyTruoc}",
                $"NT_{TDKH.COL_KLThanhToan}",
                $"NT_{TDKH.COL_LuyKeTuDauNam}",
                $"NT_{TDKH.COL_KhoiLuongDaThiCong}",
                $"NT_{TDKH.COL_KeHoachKySau}",
                $"NT_{TDKH.COL_KeHoachNamNay}",
                $"TT_{TDKH.COL_LuyKeHetNamTruoc}",
                $"TT_{TDKH.COL_LuyKeKyTruoc}",
                $"TT_{TDKH.COL_KLThanhToan}",
                $"TT_{TDKH.COL_LuyKeTuDauNam}",
                $"TT_{TDKH.COL_KhoiLuongDaThiCong}",
                $"TT_{TDKH.COL_KeHoachKySau}",
                $"TT_{TDKH.COL_KeHoachNamNay}",
                TDKH.COL_DangDo
};
            foreach (var item in lsData)
            {
                var dfn = item.dfn;
                if (dfn.Range.RowCount > 3)
                {
                    ws.Rows.Remove(dfn.Range.TopRowIndex + 1, dfn.Range.RowCount - 3);
                }

                item.FirstRowInd = dfn.Range.TopRowIndex;
            }

            var grs = dtTong.Where(x => x.CodeDVTH.HasValue()).GroupBy(x => x.CodeDVTH);

            //var crRowInd = dfnTrucThuoc.Range.TopRowIndex;
            //int rowTongInd = crRowInd;
            //int STTDVTH = 0;

            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            DataTable dtData = null;
            int indInList = 0;
            Dictionary<string, double> CodesChuaChiaHet = new Dictionary<string, double>();
            var crRowInd = 0;
            string prefixFormula = MyConstant.PrefixFormula;
            int fstNhaThau = 0;
            int fstInd = 0;
            string forGiaTri = string.Empty;
            string forSum = string.Empty;
            foreach (var grDVTH in grs)
            {
                var fstDVTH = grDVTH.First();
                DataRow newRowDVTH = null;
                if (fstDVTH.CodeNhaThau is null)
                {

                    if (fstDVTH.CodeTongThau != null)
                        indInList = 2;
                    else if (fstDVTH.DonViTrucThuoc)
                        indInList = 3;
                    else
                        indInList = 1;

                    dtData = lsData[indInList].dt;
                    newRowDVTH = dtData.NewRow();
                    dtData.Rows.Add(newRowDVTH);
                    lsData[indInList].Index += 1;


                    newRowDVTH[TDKH.COL_STT] = lsData[indInList].Index;
                    //newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                    newRowDVTH[TDKH.COL_DanhMucCongTac] = fstDVTH.TenDVTH.ToUpper();
                    newRowDVTH[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVTong;
                }
                else
                {
                    indInList = 0;
                    dtData = lsData[indInList].dt;
                    lsData[indInList].Index += 1;
                }

                var itemData = lsData[indInList];

                int STTDVTH = itemData.Index;
                crRowInd = crRowInd = fstNhaThau = fstInd = itemData.FirstRowInd + 1;
                var grsCongTrinh = grDVTH.GroupBy(x => x.CodeDVTH);
                int STTCTrinh = 0;
                fstNhaThau = itemData.FirstRowInd + 1 + dtData.Rows.Count;
                if (newRowDVTH != null)
                    newRowDVTH[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{ itemData.FirstRowInd + 1})";
                foreach (var grCTrinh in grsCongTrinh)
                {
                    DataRow newRowCtrinh = dtData.NewRow();
                    dtData.Rows.Add(newRowCtrinh);

                    //var crRowCtrInd = ++crRowInd;

                    var fstCtr = grCTrinh.First();
                    newRowCtrinh[TDKH.COL_STT] = $"{STTDVTH}.{++STTCTrinh}";
                    //newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                    newRowCtrinh[TDKH.COL_DanhMucCongTac] = $"CTR: {fstCtr.TenCongTrinh}";
                    newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                    var crRowCtrInd = crRowInd = itemData.FirstRowInd + 1 + dtData.Rows.Count;
                    var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                    newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{fstNhaThau})";
                    int STTHM = 0;

                    //int sorid = 0;
                    foreach (var grHM in grsHM)
                    {
                        //int sttWholeHM = 0;
                        DataRow newRowHM = dtData.NewRow();
                        dtData.Rows.Add(newRowHM);

                        //var crRowHMInd = ++crRowInd;

                        var fstHM = grHM.First();
                        newRowHM[TDKH.COL_STT] = $"{STTDVTH}.{STTCTrinh}.{++STTHM}";
                        //newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                        newRowHM[TDKH.COL_DanhMucCongTac] = $"HM: {fstHM.TenHangMuc}";
                        newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                        newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd})";
                        var grsPhanTuyen = grHM.GroupBy(x => x.indPT)
                            .OrderBy(x => x.Key);
                        var crRowHMInd = crRowInd = itemData.FirstRowInd + 1 + dtData.Rows.Count;
                        foreach (var grPhanTuyen in grsPhanTuyen)
                        {
                            var fstPT = grPhanTuyen.First();

                            string codePT = fstPT.CodePhanTuyen;
                            bool isPhanTuyen = codePT.HasValue();
                            int? crRowPTInd = null;
                            if (isPhanTuyen)
                            {
                                DataRow newRowPT = dtData.NewRow();
                                dtData.Rows.Add(newRowPT);

                                //crRowPTInd = ++crRowInd;
                                crRowPTInd = itemData.FirstRowInd + dtData.Rows.Count;

                                //newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_PhanTuyen;
                                newRowPT[TDKH.COL_DanhMucCongTac] = $"*T: {fstPT.TenPhanTuyen}";
                                newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                                newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd})";
                            }

                            int STTCTac = 0;
                            int? crRowNhomInd = null;
                            if (cbe_LocCongTac.SelectedIndex == (int)CongTacBaoCaoTypeEnum.CHILAYMAGOP)
                            {
                                var grsGop = grPhanTuyen.GroupBy(x => new { x.TenGopCongTac, x.DonVi });

                                foreach (var grGop in grsGop)
                                {
                                    //++crRowInd;
                                    var codesCTacInGop = grGop.Select(x => x.Code);

                                    var klhnFullInGop = KLTCsFull.Where(x => codesCTacInGop.Contains(x.ParentCode));

                                    var klhnKyTruocInGop = klhnFullInGop.Where(x => codesCTacInGop.Contains(x.ParentCode) && x.Ngay >= nbdKyTruoc && x.Ngay <= nktKyTruoc);
                                    var klhnKyNayInGop = klhnFullInGop.Where(x => codesCTacInGop.Contains(x.ParentCode) && x.Ngay >= nbdKyNay && x.Ngay <= nktKyNay);
                                    var klhnKySauInGop = klhnFullInGop.Where(x => codesCTacInGop.Contains(x.ParentCode) && x.Ngay >= nbdKySau && x.Ngay <= nktKySau);

                                    var kltcFullInGop = KLTCsFull.Where(x => codesCTacInGop.Contains(x.ParentCode));




                                    var fstGop = grGop.First();
                                    DataRow newRowGop = dtData.NewRow();
                                    dtData.Rows.Add(newRowGop);

                                    var KLChuaChia = grGop.Sum(x => x.KhoiLuongConLai * x.DonGia);
                                    if (KLChuaChia > 0)
                                        CodesChuaChiaHet.Add($"{fstGop.CodeGop}_{fstGop.Code}_{fstGop.DonVi}", KLChuaChia);

                                    newRowGop[TDKH.COL_CodeGop] = fstGop.CodeGop;
                                    newRowGop[TDKH.COL_Code] = fstGop.Code;

                                    newRowGop[TDKH.COL_STT] = ++STTCTac;
                                    newRowGop[TDKH.COL_DanhMucCongTac] = fstGop.TenCongTac;
                                    newRowGop[TDKH.COL_DonVi] = fstGop.DonVi;

                                    var GiaTriHĐ = grGop.Sum(ctac => ctac.KhoiLuongHopDongChiTiet * ctac.DonGia);
                                    newRowGop[TDKH.COL_GiaTriHopDongWithCPDP]
                                        = newRowGop[TDKH.COL_GiaTriHopDongWithoutCPDP]
                                        = GiaTriHĐ;

                                    newRowGop[TDKH.COL_LuyKeHetNamTruoc] = klhnFullInGop.Where(x => x.Ngay <= lastDatePrevYear).Sum(x => x.ThanhTienKeHoach) ?? 0;
                                    newRowGop[TDKH.COL_LuyKeKyTruoc] = klhnKyTruocInGop.Sum(x => x.ThanhTienThiCong) ?? 0;
                                    newRowGop[TDKH.COL_ThucHienKyNay] = klhnKyNayInGop.Sum(x => x.ThanhTienThiCong) ?? 0;

                                    newRowGop[TDKH.COL_LuyKeTuDauNam] = klhnFullInGop.Where(x => x.Ngay >= fstDateCrYear).Sum(x => x.ThanhTienThiCong) ?? 0;
                                    newRowGop[TDKH.COL_KhoiLuongDaThiCong] = klhnFullInGop.Sum(x => x.ThanhTienThiCong) ?? 0;



                                    newRowGop[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                    crRowNhomInd = crRowInd = itemData.FirstRowInd + dtData.Rows.Count;
                                    newRowGop[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd)})";

                                }
                            }
                            else
                            {
                                foreach (CongTacTheoGiaiDoanExtension ctac in grPhanTuyen)
                                {
                                    var klhnFullInGop = KLTCsFull.Where(x => x.ParentCode == ctac.Code);
                                    //continue;
                                    if (ctac.KhoiLuongConLai * ctac.DonGia > 0)
                                        CodesChuaChiaHet.Add($"{ctac.CodeGop}_{ctac.Code}_{ctac.DonVi}", ctac.KhoiLuongConLai * ctac.DonGia);


                                    DataRow newRowGop = dtData.NewRow();
                                    dtData.Rows.Add(newRowGop);

                                    newRowGop[TDKH.COL_Code] = ctac.Code;
                                    newRowGop[TDKH.COL_STT] = ++STTCTac;
                                    newRowGop[TDKH.COL_DanhMucCongTac] = ctac.TenCongTac;
                                    newRowGop[TDKH.COL_DonVi] = ctac.DonVi;

                                    var GiaTriHĐ = ctac.KhoiLuongHopDongChiTiet * ctac.DonGia; ;
                                    newRowGop[TDKH.COL_GiaTriHopDongWithCPDP]
                                        = newRowGop[TDKH.COL_GiaTriHopDongWithoutCPDP]
                                        = GiaTriHĐ;

                                    var LastDatePrevYear = klhnFullInGop.Single(x => x.Ngay == lastDatePrevYear);
                                    var LastDateCurrentYear = klhnFullInGop.Single(x => x.Ngay == lastDateCrYear);
                                    var StartDatePrevPeriod = klhnFullInGop.Single(x => x.Ngay == nbdKyTruoc);
                                    var EndDatePrevPeriod = klhnFullInGop.Single(x => x.Ngay == nktKyTruoc);
                                    var StartDateCrPeriod = klhnFullInGop.Single(x => x.Ngay == nbdKyNay);
                                    var EndDateCrPeriod = klhnFullInGop.Single(x => x.Ngay == nktKyNay);
                                    var StartDateNextPeriod = klhnFullInGop.Single(x => x.Ngay == nbdKySau);
                                    var EndDateNextPeriod = klhnFullInGop.Single(x => x.Ngay == nktKySau);


                                    var crrowIndCt = crRowInd = itemData.FirstRowInd + dtData.Rows.Count;
                                    newRowGop[TDKH.COL_LuyKeHetNamTruoc] = LastDatePrevYear.LuyKeThanhTienThiCong ?? 0;
                                    newRowGop[TDKH.COL_LuyKeKyTruoc] = (EndDateCrPeriod.LuyKeThanhTienThiCong??0)-(LastDatePrevYear.LuyKeThanhTienThiCong ?? 0);
                                    newRowGop[TDKH.COL_ThucHienKyNay] = (EndDateCrPeriod.LuyKeThanhTienThiCong?? 0) -(StartDateCrPeriod.LuyKeThanhTienThiCongKyTruoc);

                                    newRowGop[TDKH.COL_LuyKeTuDauNam] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_LuyKeKyTruoc]}{crRowInd + 1} + {dic[TDKH.COL_ThucHienKyNay]}{crRowInd + 1}";
                                    newRowGop[TDKH.COL_KhoiLuongDaThiCong] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_LuyKeHetNamTruoc]}{crRowInd + 1} + {dic[TDKH.COL_LuyKeTuDauNam]}{crRowInd + 1}";
                                    newRowGop[TDKH.COL_KeHoachKySau] = EndDateNextPeriod?.LuyKeThanhTienKeHoach - (StartDateNextPeriod.LuyKeThanhTienKeHoachKyTruoc ?? 0);
                                    newRowGop[TDKH.COL_KeHoachNamNay] = LastDateCurrentYear?.LuyKeThanhTienKeHoach - (LastDatePrevYear.LuyKeThanhTienKeHoachKyTruoc ?? 0); ;

                                    newRowGop[$"NT_{TDKH.COL_LuyKeTuDauNam}"] = newRowGop[TDKH.COL_LuyKeTuDauNam];
                                    newRowGop[$"NT_{TDKH.COL_KeHoachKySau}"] = newRowGop[TDKH.COL_KeHoachKySau];
                                    newRowGop[$"NT_{TDKH.COL_KeHoachNamNay}"] = newRowGop[TDKH.COL_KeHoachNamNay];
                                    newRowGop[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowNhomInd ?? crRowPTInd ?? crRowHMInd)})";

                                    newRowGop[TDKH.COL_DangDo] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_KhoiLuongDaThiCong]}{crRowInd + 1} - {dic[$"NT_{TDKH.COL_KhoiLuongDaThiCong}"]}{crRowInd + 1}";
                                    if (dtHopDong != null && dtHopDong.Rows.Count > 0)
                                    {
                                        DataRow[] RowCT = dtHopDong.AsEnumerable().Where(x => x["Code"].ToString() == ctac.Code).ToArray();
                                        if (RowCT.Any())
                                        {
                                            DataRow fst = RowCT.FirstOrDefault();
                                            newRowGop[$"TT_{TDKH.COL_KLThanhToan}"] = fst["KyNayHopDongNghiemThu"];
                                            newRowGop[$"NT_{TDKH.COL_LuyKeHetNamTruoc}"] = fst["KyNayHopDongNghiemThuNext"];
                                            newRowGop[$"NT_{TDKH.COL_KhoiLuongDaThiCong}"] = fst["LuyKeDenHetKyNay"];
                                            newRowGop[$"NT_{TDKH.COL_LuyKeKyTruoc}"] = fst["LuyKeDenHetKyTruoc"];
                                            if (fst[TDKH.COL_DangDo] != DBNull.Value)
                                                newRowGop[TDKH.COL_DangDo] = fst[TDKH.COL_DangDo];
                                        }
                                    }


                                    newRowGop[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                }
                            }

                            if (isPhanTuyen)
                            {
                                DataRow newRowPT = dtData.NewRow();
                                dtData.Rows.Add(newRowPT);

                                //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                newRowPT[TDKH.COL_Code] = codePT;
                                //newRowPT[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HoanThanhPhanTuyen;

                                newRowPT[TDKH.COL_DanhMucCongTac] = $"{MyConstant.PrefixFormula}{MyConstant.PrefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd + 1}";
                                newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                crRowInd = dfnKhongTrucThuoc.Range.TopRowIndex + 1 + dtData.Rows.Count;
                                foreach (string col in colsSum)
                                {
                                    forGiaTri = TDKHHelper.GetFormulaSumChild((crRowPTInd ?? 0), crRowInd - 1, dic[col], dic[TDKH.COL_RowCha]);
                                    newRowPT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                }
                                //newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd + 1})";
                            }
                        }
                        crRowInd = fstInd + dtData.Rows.Count;
                        foreach (string col in colsSum)
                        {
                            forGiaTri = TDKHHelper.GetFormulaSumChild(crRowHMInd, crRowInd - 1, dic[col], dic[TDKH.COL_RowCha]);
                            newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }
                    }
                    crRowInd = fstInd + dtData.Rows.Count;
                    foreach (string col in colsSum)
                    {
                        forGiaTri = TDKHHelper.GetFormulaSumChild(crRowCtrInd, crRowInd - 1, dic[col], dic[TDKH.COL_RowCha]);
                        newRowCtrinh[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                    }
                }
                crRowInd = fstInd + dtData.Rows.Count;
                if (newRowDVTH != null)
                {
                    foreach (string col in colsSum)
                    {
                        forGiaTri = TDKHHelper.GetFormulaSumChild(fstNhaThau, crRowInd - 1, dic[col], dic[TDKH.COL_RowCha]);
                        newRowDVTH[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                    }
                }
            }

            foreach (var item in lsData)
            {
                var dfn = item.dfn;
                var dt = item.dt;
                if (dt.Rows.Count == 0)
                    continue;

                var inddmct = ws.Range.GetColumnIndexByName(dic[TDKH.COL_DanhMucCongTac]);
                if (dfn == dfnBillChung)
                {
                    //var drs = dt.AsEnumerable().Where(x => double.Parse[])
                    /*dfnChuaPhanKhai*/
                    var dtChuaPK = item.dt.AsEnumerable().Where(x => x["TypeRow"].ToString() != MyConstant.TYPEROW_CVCha || CodesChuaChiaHet.ContainsKey($"{x["CodeGop"]}_{x["Code"]}_{x["DonVi"]}")).CopyToDataTable();
                    if (dt.Rows.Count == 0)
                        continue;


                    foreach (DataRow dr in dtChuaPK.AsEnumerable().Where(x => x["TypeRow"].ToString() == MyConstant.TYPEROW_CVCha))
                    {
                        dr[TDKH.COL_GiaTriHopDongWithCPDP]
                             = dr[TDKH.COL_GiaTriHopDongWithoutCPDP]
                             = CodesChuaChiaHet[$"{dr["CodeGop"]}_{dr["Code"]}_{dr["DonVi"]}"];
                    }

                    ws.Rows.Insert(dfnChuaPhanKhai.Range.BottomRowIndex, dtChuaPK.Rows.Count, RowFormatMode.FormatAsNext);
                    ws.Import(dtChuaPK, false, dfnChuaPhanKhai.Range.TopRowIndex + 1, 0);
                    //var inddmct = ws.Range.GetColumnIndexByName(dic[TDKH.COL_DanhMucCongTac]);
                    SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfnChuaPhanKhai.Range);
                    SpreadsheetHelper.FormatRowsInRange(dfnChuaPhanKhai.Range, dic[TDKH.COL_TypeRow]);
                    ws.Range.FromLTRB(inddmct, dfnChuaPhanKhai.Range.TopRowIndex, inddmct, dfnChuaPhanKhai.Range.BottomRowIndex).AutoFitRows();
                }

                ws.Rows.Insert(dfn.Range.BottomRowIndex, dt.Rows.Count, RowFormatMode.FormatAsNext);
                ws.Import(dt, false, dfn.Range.TopRowIndex + 1, 0);
                SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfn.Range);
                SpreadsheetHelper.FormatRowsInRange(dfn.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha]);
                ws.Range.FromLTRB(inddmct, dfn.Range.TopRowIndex, inddmct, dfn.Range.BottomRowIndex).AutoFitRows();

            }
            wb.EndUpdate();
            WaitFormHelper.CloseWaitForm();
            LoadVatTuACap();
        }

        private void LoadVatTuACap()
        {
            if (slke_ThongTinDuAn.EditValue == null)
            {
                MessageShower.ShowError("Vui lòng chọn dự án!");
                uc_BaoCao1.PreviewSelectedIndex = 0;
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang cập nhật vật tư!!! Vui lòng chờ!!!!");
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            //uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);

            var wb = uc_BaoCao1.SpreadSheet.Document;
            var ws = wb.Worksheets[sheetName1];
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());


            var nbdKyTruoc = de_StartDatePrevPeriod.DateTime.Date;
            var nbdKyNay = de_StartDateCrPeriod.DateTime.Date;
            var nbdKySau = de_StartDateNextPeriod.DateTime.Date;


            var nktKyTruoc = de_EndDatePrevPeriod.DateTime.Date;
            var nktKyNay = de_EndDateCrPeriod.DateTime.Date;
            var nktKySau = de_EndDateNextPeriod.DateTime.Date;

            int crYear = nbdKyTruoc.Year;
            DateTime lastDatePrevYear = new DateTime(crYear - 1, 12, 31);
            DateTime lastDateCrYear = new DateTime(crYear, 12, 31);
            DateTime fstDateCrYear = new DateTime(crYear, 1, 1);
            wb.BeginUpdate();
            var dfn = ws.DefinedNames.Single(x => x.Name == nameBieu1VatTuACap);
            if (dfn.Range.RowCount > 3)
            {
                ws.Rows.Remove(dfn.Range.TopRowIndex + 1, dfn.Range.RowCount - 3);
            }


            string dbString = $@"SELECT xvt.Code, HD.GiaTriHD,YCVT.CodeKHVT,YCVT.CodeHd,YCVT.CodeTDKH,YCVT.Code AS ID, COALESCE(YCVT.CodeHangMuc, cttk.CodeHangMuc) AS CodeHangMuc, da.Code AS CodeDuAn, da.TenDuAn AS TenDuAn,
hm.CodeCongTrinh,hm.Ten as TenHangMuc,ctrinh.Ten as TenCongTrinh,Tuyen.Ten as TenTuyen,Tuyen.Code as CodePhanTuyen, null as CodeNhom, 
COALESCE(cttk.VatTu, YCVT.TenVatTu) AS TenVatTu, COALESCE(cttk.MaVatLieu, YCVT.MaVatTu) AS MaVatTu, COALESCE(cttk.DonVi, YCVT.DonVi) AS DonVi, xvthn.Ngay, xvthn.KhoiLuong, xvthn.DonGia
FROM {Server.Tbl_QLVT_XuatVatTu} xvt
LEFT JOIN {Server.Tbl_QLVT_XuatVatTu_KhoiLuongHangNgay} xvthn
ON xvt.Code = xvthn.CodeCha
LEFT JOIN Tbl_QLVT_YeuCauVatTu YCVT 
ON xvt.CodeDeXuat = YCVT.Code
LEFT JOIN Tbl_HopDong_TongHopACap HD
ON HD.CodeYeuCauVatTu=YCVT.Code
LEFT JOIN Tbl_TDKH_KHVT_VatTu cttk 
ON cttk.Code=YCVT.CodeKHVT
LEFT JOIN Tbl_TDKH_PhanTuyen Tuyen 
ON Tuyen.Code=cttk.CodePhanTuyen
LEFT JOIN Tbl_ThongTinHangMuc hm 
ON hm.Code = COALESCE(YCVT.CodeHangMuc, cttk.CodeHangMuc)
LEFT JOIN Tbl_ThongTinCongTrinh ctrinh 
ON ctrinh.Code=hm.CodeCongTrinh
LEFT JOIN Tbl_ThongTinDuAn da 
ON ctrinh.CodeDuAn = da.Code 
WHERE da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' AND xvt.ACapB = 1";

            var dtTong = DataProvider.InstanceTHDA.ExecuteQueryModel<KhoiLuongXuatVatTu>(dbString);
            DataTable dtData = DataTableCreateHelper.BaoCaoBieu1_BDH();

            var grs = dtTong.AsEnumerable().GroupBy(x => x.CodeCongTrinh);

            int STTCTrinh = 0;
            foreach (var grCTrinh in grs)
            {
                DataRow newRowCtrinh = dtData.NewRow();
                dtData.Rows.Add(newRowCtrinh);

                //var crRowCtrInd = ++crRowInd;

                var fstCtr = grCTrinh.First();
                newRowCtrinh[TDKH.COL_STT] = ++STTCTrinh;
                //newRowCtrinh[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_CONGTRINH;
                newRowCtrinh[TDKH.COL_DanhMucCongTac] = $"CTR: {fstCtr.TenCongTrinh}";
                newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;

                var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                int STTHM = 0;

                //int sorid = 0;
                foreach (var grHM in grsHM)
                {
                    //int sttWholeHM = 0;
                    DataRow newRowHM = dtData.NewRow();
                    dtData.Rows.Add(newRowHM);

                    //var crRowHMInd = ++crRowInd;

                    var fstHM = grHM.First();
                    newRowHM[TDKH.COL_STT] = $"{STTCTrinh}.{++STTHM}";
                    //newRowHM[TDKH.COL_MaHieuCongTac] = MyConstant.CONST_TYPE_HANGMUC;
                    newRowHM[TDKH.COL_DanhMucCongTac] = $"HM: {fstHM.TenHangMuc}";
                    newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;

                    int STTCTac = 0;
                    var grsCtac = grHM.GroupBy(x => x.Code);//CodeHopDongVatTuACap

                    foreach (var grCtac in grsCtac)
                    {
                        //var klhnFullInGop = ctac.Where(x => x.CodeCha == ctac.Code);

                        var klhnKyTruocInGop = grCtac.Where(x => x.Ngay >= nbdKyTruoc && x.Ngay <= nktKyTruoc);
                        var klhnKyNayInGop = grCtac.Where(x => x.Ngay >= nbdKyNay && x.Ngay <= nktKyNay);
                        var klhnKySauInGop = grCtac.Where(x => x.Ngay >= nbdKySau && x.Ngay <= nktKySau);

                        DataRow newRowGop = dtData.NewRow();
                        dtData.Rows.Add(newRowGop);
                        int crRowInd = dfn.Range.TopRowIndex + 1 + dtData.Rows.Count;
                        var ctac = grCtac.First();

                        newRowGop[TDKH.COL_Code] = ctac.Code;
                        newRowGop[TDKH.COL_STT] = ++STTCTac;
                        newRowGop[TDKH.COL_DanhMucCongTac] = ctac.TenVatTu;
                        newRowGop[TDKH.COL_DonVi] = ctac.DonVi;

                        var GiaTriHĐ = ctac.GiaTriHD;
                        newRowGop[TDKH.COL_GiaTriHopDongWithCPDP]
                            = newRowGop[TDKH.COL_GiaTriHopDongWithoutCPDP]
                            = GiaTriHĐ;

                        newRowGop[TDKH.COL_LuyKeHetNamTruoc] = grCtac.Where(x => x.Ngay <= lastDatePrevYear).Sum(x => x.GiaTri);
                        newRowGop[TDKH.COL_LuyKeKyTruoc] = klhnKyTruocInGop.Sum(x => x.GiaTri);
                        newRowGop[TDKH.COL_ThucHienKyNay] = klhnKyNayInGop.Sum(x => x.GiaTri);

                        newRowGop[TDKH.COL_LuyKeTuDauNam] = grCtac.Where(x => x.Ngay >= fstDateCrYear).Sum(x => x.GiaTri);
                        newRowGop[TDKH.COL_KhoiLuongDaThiCong] = grCtac.Sum(x => x.GiaTri);

                        newRowGop[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                    }
                }
            }


            if (dtData.Rows.Count == 0)
            {
                WaitFormHelper.CloseWaitForm();
                wb.EndUpdate();
                return;
            }

            ws.Rows.Insert(dfn.Range.BottomRowIndex, dtData.Rows.Count, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, dfn.Range.TopRowIndex + 1, 0);
            SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfn.Range);
            SpreadsheetHelper.FormatRowsInRange(dfn.Range, dic[TDKH.COL_TypeRow]);
            wb.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }

        private void LoadBaoCaoHangNgay()
        {
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            var rec = uc_BaoCao1.RichEditControl;

            if (slke_ThongTinDuAn.EditValue == null)
            {
                MessageShower.ShowError("Vui lòng chọn dự án!");
                uc_BaoCao1.PreviewSelectedIndex = 0;
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu");
            string dbString = $"SELECT \"Code\" FROM {TDKH.TBL_GiaiDoanThucHien} WHERE \"CodeDuAn\"='{slke_ThongTinDuAn.EditValue}'";
            DataTable dtgd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*, {TDKH.TBL_DanhMucCongTac}.CodeHangMuc," +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongToanBo AS KhoiLuongKeHoach,{TDKH.TBL_ChiTietCongTacTheoKy}.DonGia AS DonGiaKeHoach,{TDKH.TBL_DanhMucCongTac}.DonVi, {TDKH.TBL_DanhMucCongTac}.TenCongTac, {TDKH.TBL_DanhMucCongTac}.MaHieuCongTac " +
                $",{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc,{TDKH.TBL_DanhMucCongTac}.Code AS CodeDanhMucCongTac, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code " +
                $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {TDKH.TBL_DanhMucCongTac}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code " +
                $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code " +
                $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code " +
                $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{dtgd.Rows[0][0]}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThau IS NULL";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {Server.Tbl_ThoiTiet} tt " +
                $"WHERE tt.CodeDuAn = '{slke_ThongTinDuAn.EditValue}' " +
                $"AND tt.Ngay = '{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";

            List<Tbl_ThoiTietViewModel> tt = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThoiTietViewModel>(dbString);

            dbString = $"SELECT {TDKH.TBL_KHVT_VatTu}.Code AS CodeCongTac," +
                    $" {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.DonVi,{TDKH.TBL_KHVT_VatTu}.LoaiVatTu,{TDKH.TBL_KHVT_VatTu}.CodeToDoi,{TDKH.TBL_KHVT_VatTu}.CodeNhaThauPhu,{TDKH.TBL_KHVT_KhoiLuongHangNgay}.KhoiLuongThiCong," +
                    $"{TDKH.TBL_KHVT_KhoiLuongHangNgay}.KhoiLuongKeHoach " +
                    $"FROM {TDKH.TBL_KHVT_VatTu} " +
                    $"JOIN {TDKH.TBL_KHVT_KhoiLuongHangNgay} ON {TDKH.TBL_KHVT_KhoiLuongHangNgay}.CodeVatTu={TDKH.TBL_KHVT_VatTu}.Code " +
                    $"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
                    $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                    $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                    $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_KHVT_VatTu}.CodeNhaThau IS NULL AND {TDKH.TBL_KHVT_VatTu}.LoaiVatTu <>'Vật liệu' " +
                    $"AND {TDKH.TBL_KHVT_KhoiLuongHangNgay}.Ngay='{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' ";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KLTTHangNgay> lsSourceKHVT = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            //if (dtCongTacTheoKy.Rows.Count == 0)
            //{
            //    MessageShower.ShowError("Dự án chưa có công tác thêm vào, Vui lòng thêm đầy đủ thông tin để tạo được Báo cáo !", "Thông tin");
            //    WaitFormHelper.CloseWaitForm();
            //    return;
            //}
            List<KLTTHangNgay> lstCTTheoCK = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dtCongTacTheoKy);
            DateTime NKT = dtCongTacTheoKy.Rows.Count == 0 ? DateTime.Now : dtCongTacTheoKy.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
            DateTime MIN = dtCongTacTheoKy.Rows.Count == 0 ? DateTime.Now : dtCongTacTheoKy.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayBatDau"].ToString()));
            string[] lstcodeCT = lstCTTheoCK.Select(x => x.Code).ToArray();
            //var dtTheoNgay = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lstcodeCT);
            //List<KLTTHangNgay> KLHN = new List<KLTTHangNgay>();
            //if (dtTheoNgay == null)
            //    goto Label;
            //var TC = dtTheoNgay.Where(x => x.KhoiLuongThiCong > 0).ToArray();
            //if (TC.Count() != 0)
            //    KLHN = TC.Select(x => new KLTTHangNgay()
            //    {
            //        CodeCha = x.CodeCha,
            //        CodeCongTacTheoGiaiDoan = x.CodeCongTacTheoGiaiDoan,
            //        KhoiLuongKeHoach = x.KhoiLuongKeHoach,
            //        KhoiLuongThiCong = x.KhoiLuongThiCong,
            //        ThanhTienThiCongCustom = x.ThanhTienThiCong,
            //        DonGiaKeHoach = x.DonGiaKeHoach, 
            //        DonGiaThiCong = x.DonGiaThiCong
            //    }).ToList();
            //Label:
            //List<KLTTHangNgay> TongHop = dtTheoNgay.Select(x => new KLTTHangNgay()
            //{
            //    CodeCha = x.CodeCha,
            //    CodeCongTacTheoGiaiDoan = x.CodeCongTacTheoGiaiDoan,
            //    KhoiLuongKeHoach = x.KhoiLuongKeHoach,
            //    KhoiLuongThiCong = x.KhoiLuongThiCong,
            //    ThanhTienThiCongCustom = x.ThanhTienThiCong,
            //    DonGiaKeHoach = x.DonGiaKeHoach,
            //    DonGiaThiCong = x.DonGiaThiCong
            //}).ToList();

            //dbString = $"SELECT {TDKH.TBL_KhoiLuongCongViecHangNgay}.*, {TDKH.TBL_DanhMucCongTac}.CodeHangMuc," +
            //    $" {TDKH.TBL_ChiTietCongTacTheoKy}.DonGia AS DonGiaKeHoach,{TDKH.TBL_DanhMucCongTac}.DonVi, {TDKH.TBL_DanhMucCongTac}.TenCongTac, {TDKH.TBL_DanhMucCongTac}.MaHieuCongTac " +
            //    $",{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc,{TDKH.TBL_DanhMucCongTac}.Code AS CodeDanhMucCongTac, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
            //    $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh,{TDKH.TBL_ChiTietCongTacTheoKy}.CodeToDoi,{TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThauPhu " +
            //    $"FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
            //    $"INNER JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
            //    $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
            //    $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
            //    $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code " +
            //    $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
            //    $"ON {TDKH.TBL_DanhMucCongTac}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code " +
            //    $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
            //    $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code " +
            //    $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} " +
            //    $"ON {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code " +
            //    $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{dtgd.Rows[0][0]}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThau IS NULL AND {TDKH.TBL_KhoiLuongCongViecHangNgay}.Ngay ='{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            //DataTable dtCongTacTheoNgay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var date = de_begin.DateTime.Date;
            List<CongTacTheoGiaiDoanExtension> dtCongTacTheoNgay = TDKHHelper.GetCongTacsModel(slke_ThongTinDuAn.EditValue?.ToString(), null, null, dtgd.Rows[0][0].ToString(),
                $"cttk.CodeNhaThau IS NULL", $"(cttk.NgayBatDau <= '{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND cttk.NgayKetThuc >= '{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')");

            var KLHN = MyFunction.CalcKLHNBrief(TypeKLHN.CongTac, dtCongTacTheoNgay.Select(x => x.Code), date, date);

            DataRow[] dt30Day = dtCongTacTheoKy.AsEnumerable().Where(x => DateTime.Parse(x["NgayBatDau"].ToString()).Date > de_begin.DateTime.Date
            && DateTime.Parse(x["NgayBatDau"].ToString()).Date < de_begin.DateTime.Date.AddDays(30)).ToArray();
            dbString = $"SELECT {QLVT.TBL_QLVT_NHAPVTKLHN}.*,{QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu,{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl,{QLVT.TBL_QLVT_YEUCAUVT}.LuyKeXuatKho," +
                $"{QLVT.TBL_QLVT_YEUCAUVT}.LuyKeYeuCau,{MyConstant.TBL_THONGTINNHACUNGCAP}.Ten FROM {QLVT.TBL_QLVT_NHAPVTKLHN}" +
                $" INNER JOIN {QLVT.TBL_QLVT_NHAPVT} ON {QLVT.TBL_QLVT_NHAPVTKLHN}.CodeCha={QLVT.TBL_QLVT_NHAPVT}.Code " +
                $"INNER JOIN {QLVT.TBL_QLVT_YEUCAUVT} ON {QLVT.TBL_QLVT_YEUCAUVT}.Code={QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
                $"INNER JOIN {MyConstant.TBL_THONGTINNHACUNGCAP} ON {QLVT.TBL_QLVT_YEUCAUVT}.TenNhaCungCap={MyConstant.TBL_THONGTINNHACUNGCAP}.Code " +
                $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{dtgd.Rows[0][0]}' AND {QLVT.TBL_QLVT_NHAPVTKLHN}.Ngay='{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            DataTable dtVatLieu = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT {QLVT.TBL_QLVT_YEUCAUVT}.*,{MyConstant.TBL_THONGTINNHACUNGCAP}.Ten FROM {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"LEFT JOIN {MyConstant.TBL_THONGTINNHACUNGCAP} ON {MyConstant.TBL_THONGTINNHACUNGCAP}.Code={QLVT.TBL_QLVT_YEUCAUVT}.TenNhaCungCap" +
                $" WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{dtgd.Rows[0][0]}' AND {QLVT.TBL_QLVT_YEUCAUVT}.TrangThai='1'";
            DataTable dtVatLieuYeuCau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.*,{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.TenNhanVien,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.DateXacNhanDaChi," +
                $"{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GiaTriGiaiChi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CheckDaChi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GhiChu as GhiChuKC,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.NguoiLapTamUng" +
                $" FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}" +
                $" LEFT JOIN {MyConstant.Tbl_ChamCong_BangNhanVien} ON {MyConstant.Tbl_ChamCong_BangNhanVien}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.NguoiLapTamUng " +
                $" LEFT JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} ON {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CodeDeXuat={ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code " +
                $" WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.CodeDuAn='{slke_ThongTinDuAn.EditValue}'";
            DataTable dtKC = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.*,{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}.TenNhanVien " +
                //$" {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CheckDaChi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GhiChu as GhiChuKC,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.NguoiLapTamUng" +
                $" FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}" +
                $" LEFT JOIN {MyConstant.Tbl_ChamCong_BangNhanVien} ON {MyConstant.Tbl_ChamCong_BangNhanVien}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NguoiNhan " +
                $" INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ON {MyConstant.TBL_THONGTINCONGTRINH}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CongTrinh " +
                $" WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{slke_ThongTinDuAn.EditValue}' AND {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CheckDaThu='True' AND {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NgayThangThucHien='{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' ";
            DataTable dtKT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT {GiaoViec.TBL_CONGVIECCHA}.*,{TDKH.TBL_KhoiLuongCongViecHangNgay}.KhoiLuongThiCong,{MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
               $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
               $"FROM {GiaoViec.TBL_CONGVIECCHA} " +
               $"INNER JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
               $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeCongViecCha = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongViecCha " +
               $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
               $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code " +
               $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
               $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code " +
               $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} " +
               $"ON {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code " +
               $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn= '{slke_ThongTinDuAn.EditValue}' AND {GiaoViec.TBL_CONGVIECCHA}.TrangThai='Hoàn thành' " +
               $"AND {TDKH.TBL_KhoiLuongCongViecHangNgay}.Ngay='{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            DataTable GiaoViecCT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT file.FileDinhKem, file.Code, dvth.Code AS CodeDVTH, dvth.Ten, cvc.CodeCongViecCha, cvc.TenCongViec, cvc.LyTrinhCaoDo, " +
                $" ctrinh.Code AS CodeCongTrinh, ctrinh.Ten AS TenCongTrinh, hm.Code AS CodeHangMuc, hm.ten AS TenHangMuc " +
                $"FROM {GiaoViec.TBL_CONGVIECCHA} cvc " +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"ON cvc.CodeCongTacTheoGiaiDoan = cttk.Code " +
                $"JOIN {GiaoViec.TBL_FileDinhKem} file " +
                $"ON cvc.CodeCongViecCha = file.CodeParent " +
                $"JOIN {MyConstant.view_DonViThucHien} dvth " +
                $"ON {string.Format(FormatString.CodeDonViNhanThau, "cvc")} = dvth.Code " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm " +
                $"ON cvc.CodeHangMuc = hm.Code " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh " +
                $"ON hm.CodeCongTrinh = ctrinh.Code " +
                $"WHERE (cttk.CodeGiaiDoan IS NULL OR cttk.CodeGiaiDoan = '{dtgd.Rows[0][0]}') AND file.Ngay = '{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                $"AND ctrinh.CodeDuAn = '{slke_ThongTinDuAn.EditValue}' " +
                $"AND (file.FileDinhKem LIKE '%.jpg' OR file.FileDinhKem LIKE '%.png' OR file.FileDinhKem LIKE '%.ico' OR file.FileDinhKem LIKE '%.jpeg')";
            DataTable dtFile = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            //using (RichEditControl red_FileListNT = new RichEditControl())
            //{
            List<DonViThucHien> DVTH = GetDonViThucHiens();
            //var rec = uc_BaoCao1.RichEditControl;
            Document doc = rec.Document;

            //string m_Path = Path.Combine(BaseFrom.m_path, "Template", "FileWord", "[Mẫu] Báo cao thi công hàng ngày.docx");
            //string m_PathExcel = Path.Combine(BaseFrom.m_path, "Template", "FileExcel", "BaoCaoTongHop.xlsx");
            doc.LoadDocument(m_Path);
            Bookmark[] bms = doc.Bookmarks.Where(x => !MyConstant.BCHN_BMS_NotLoop.Contains(x.Name)).ToArray();
            doc.DefaultTableProperties.TableLayout = TableLayoutType.Fixed;

            rec.BeginUpdate();
            foreach (Bookmark bm in bms)
            {
                if (bm.Name == MyConstant.CONST_BM_HinhAnhCongTac)
                {
                    int STTDVTH = 0;

                    doc.CaretPosition = bm.Range.Start;
                    doc.Delete(bm.Range);
                    if (dtFile.Rows.Count == 0)
                        continue;

                    var section = doc.GetSection(doc.CaretPosition);
                    bool isFullTemplate = fileMaus.FileName.Contains("Full");

                    int heighDocByDoc = (int)Math.Round(((section.Page.Landscape) ? section.Page.Width : section.Page.Height) - section.Margins.Top - section.Margins.Bottom);
                    int widthDocByDoc = (int)Math.Round(((section.Page.Landscape) ? section.Page.Height : section.Page.Width) - section.Margins.Left - section.Margins.Right);

                    int offsetIndent = (int)Math.Round(Units.CentimetersToDocumentsF((float)0.2));

                    int maxPixByHeigh = Units.DocumentsToPixels(heighDocByDoc - 2 * offsetIndent, rec.DpiY);
                    //NumberingList numberingList = doc.NumberingLists.Add(0);

                    var grsDvth = dtFile.AsEnumerable().GroupBy(x => x["CodeDVTH"].ToString());

                    Paragraph appendedParagraph = null;
                    foreach (var grDvth in grsDvth)
                    {
                        if (appendedParagraph != null)
                            doc.CaretPosition = appendedParagraph.Range.Start;

                        var range = doc.InsertText(doc.CaretPosition, $"{++STTDVTH}. {grDvth.First()["Ten"]}");
                        range.UpdateBasicFormatRange(isBold: true, foreColor: Color.Blue);
                        //doc.Paragraphs.AddParagraphToList(doc.Paragraphs.Get(doc.CaretPosition), numberingList, 0);
                        appendedParagraph = doc.Paragraphs.Insert(doc.CaretPosition);
                        //doc.InsertText(doc.CaretPosition, Characters.LineBreak.ToString());

                        var grsCongTrinh = grDvth.GroupBy(x => x["CodeCongTrinh"].ToString());

                        int STTCongTrinh = 0;
                        foreach (var grCongTrinh in grsCongTrinh)
                        {
                            doc.CaretPosition = appendedParagraph.Range.Start;

                            range = doc.InsertText(doc.CaretPosition, $"{STTDVTH}.{++STTCongTrinh}. {grCongTrinh.First()["TenCongTrinh"]}");
                            range.UpdateBasicFormatRange(isBold: true, foreColor: Color.Green);
                            //doc.Paragraphs.AddParagraphToList(doc.Paragraphs.Get(doc.CaretPosition), numberingList, 1);
                            appendedParagraph = doc.Paragraphs.Insert(doc.CaretPosition);

                            var grsHM = grDvth.GroupBy(x => x["CodeHangMuc"].ToString());

                            int STTHangMuc = 0;
                            foreach (var grHM in grsCongTrinh)
                            {
                                doc.CaretPosition = appendedParagraph.Range.Start;
                                range = doc.InsertText(doc.CaretPosition, $"{STTDVTH}.{STTCongTrinh}.{++STTHangMuc}. {grHM.First()["TenHangMuc"]}");
                                range.UpdateBasicFormatRange(isBold: true, foreColor: Color.Black);
                                //doc.Paragraphs.AddParagraphToList(doc.Paragraphs.Get(doc.CaretPosition), numberingList, 2);
                                //appendedParagraph = doc.Paragraphs.Insert(doc.CaretPosition);

                                var grsCongTac = grHM.GroupBy(x => x["CodeCongViecCha"].ToString());

                                foreach (var grCTac in grsCongTac)
                                {
                                    var fi = grDvth.First();
                                    doc.InsertText(doc.CaretPosition, Characters.LineBreak.ToString());
                                    range = doc.InsertText(doc.CaretPosition, $"{fi["TenCongViec"]} [{fi["LyTrinhCaoDo"]}]");
                                    range.UpdateBasicFormatRange(isBold: false, foreColor: Color.Black);

                                    //string crPath = BaseFrom.m_FullTempathDA;

                                    //var files = grCTac.Select(x => Path.Combine(BaseFrom.m_FullTempathDA, "Resource", "Files", GiaoViec.TBL_FileDinhKem, grCTac.Key, x["Code"].ToString())).ToList();

                                    int filesCount = grCTac.Count();
                                    int soCot = Math.Min((int)nud_SoCot.Value, filesCount);
                                    int widthCellByDoc = (int)Math.Round((double)widthDocByDoc / soCot - offsetIndent * 2);
                                    int widthCellByPix = Units.DocumentsToPixels(widthCellByDoc, rec.DpiX);

                                    WordTable tblHinhAnh;

                                    int rowCount = (int)Math.Ceiling((double)grCTac.Count() / soCot);

                                    if (ce_DinhKemTenAnh.Checked)
                                        rowCount = rowCount * 2;

                                    tblHinhAnh = doc.Tables.Create(doc.CaretPosition, rowCount, soCot, AutoFitBehaviorType.FixedColumnWidth, (int)Math.Round((double)widthDocByDoc / soCot));
                                    //tblHinhAnh.Borders
                                    tblHinhAnh.BeginUpdate();
                                    foreach (TableRow r in tblHinhAnh.Rows)
                                    {
                                        foreach (TableCell cell in r.Cells)
                                        {
                                            cell.VerticalAlignment = TableCellVerticalAlignment.Bottom;

                                            if (!ce_tblBorder.Checked)
                                            {
                                                cell.Borders.Top.LineStyle = TableBorderLineStyle.None;
                                                cell.Borders.Bottom.LineStyle = TableBorderLineStyle.None;
                                                cell.Borders.Left.LineStyle = TableBorderLineStyle.None;
                                                cell.Borders.Right.LineStyle = TableBorderLineStyle.None;
                                            }
                                        }
                                    }

                                    //tblHinhAnh.TableLayout = TableLayoutType.Autofit;
                                    var drs = grCTac.ToArray();
                                    for (int ind = 0; ind < filesCount; ind++)
                                    {
                                        DataRow dr = drs[ind];
                                        string file = Path.Combine(BaseFrom.m_FullTempathDA, "Resource", "Files", GiaoViec.TBL_FileDinhKem, grCTac.Key, dr["Code"].ToString());
                                        int rowInd = (int)Math.Floor((double)ind / soCot);
                                        if (ce_DinhKemTenAnh.Checked)
                                            rowInd = rowInd * 2 + 1;

                                        int colInd = ind % soCot;
                                        TableCell cellImg = tblHinhAnh.Cell(rowInd, colInd);
                                        string fileName = Path.GetFileNameWithoutExtension(dr["FileDinhKem"].ToString());

                                        if (ce_DinhKemTenAnh.Checked)
                                        {
                                            TableCell cellName = tblHinhAnh.Cell(rowInd - 1, 0);
                                            if (ind % soCot != 0)
                                                doc.fcn_InsertTextWithAlignment(cellName.ContentRange.End, "; ", ParagraphAlignment.Left);
                                            else
                                            {
                                                tblHinhAnh.MergeCells(tblHinhAnh.Rows[rowInd - 1].FirstCell, tblHinhAnh.Rows[rowInd - 1].LastCell);
                                            }
                                            doc.fcn_InsertTextWithAlignment(cellName.ContentRange.End, fileName, ParagraphAlignment.Left);
                                        }

                                        try
                                        {
                                            Image img = FileHelper.fcn_ImageStreamDoc(picEdit: null, file);


                                            int w = widthCellByDoc;
                                            int h = (int)Math.Round((double)(img.Height * w / img.Width));

                                            if (h > maxPixByHeigh * 70 / 100.0)
                                            {
                                                h = (int)Math.Round(maxPixByHeigh * 70 / 100.0);
                                                w = (int)Math.Round((double)(img.Width * h / img.Height));
                                            }

                                            DocumentImage docImg = doc.Images.Insert(cellImg.Range.Start, (Image)(new Bitmap(img, new Size(widthCellByPix, h))));
                                        }
                                        catch
                                        {
                                            Logging.Error("Lỗi đọc file: " + fileName);
                                        }

                                        tblHinhAnh.EndUpdate();
                                    }

                                }
                            }
                        }



                        //doc.Paragraphs.Get(doc.CaretPosition).Range.UpdateBasicFormatRange(foreColor: Color.Blue);
                        appendedParagraph = doc.Paragraphs.Insert(doc.CaretPosition);
                    }

                }

                else if (bm.Name == MyConstant.CONST_SheetName_TenNguoiXuatBaoCao || bm.Name == "NguoiXuatBaoCao")
                {
                    doc.CaretPosition = bm.Range.Start;
                    doc.Delete(bm.Range);
                    doc.InsertText(doc.CaretPosition, BaseFrom.BanQuyenKeyInfo.FullName);
                }
                else if (bm.Name == MyConstant.CONST_SheetName_DateBaoCao || bm.Name == "NgayGuiBaoCao")
                {
                    doc.CaretPosition = bm.Range.Start;
                    doc.Delete(bm.Range);
                    doc.InsertText(doc.CaretPosition, $"NGÀY {de_begin.DateTime.Day} THÁNG {de_begin.DateTime.Month} NĂM {de_begin.DateTime.Year}");
                }
                else if (bm.Name == MyConstant.CONST_SheetName_GiaoThau)
                {
                    doc.CaretPosition = bm.Range.Start;
                    doc.Delete(bm.Range);
                    doc.InsertText(doc.CaretPosition, $"{DVTH.Where(x => x.IsGiaoThau).FirstOrDefault().Ten.ToUpper()} - GIAO THẦU");
                }
                else if (bm.Name == MyConstant.CONST_SheetName_DuAn)
                {
                    doc.CaretPosition = bm.Range.Start;
                    doc.Delete(bm.Range);
                    doc.InsertText(doc.CaretPosition, slke_ThongTinDuAn.Text.ToUpper());
                }
                else if (bm.Name == MyConstant.CONST_SheetName_ThoiTiet)
                {
                    doc.CaretPosition = bm.Range.Start;
                    doc.Delete(bm.Range);
                    if (tt.Count() != 0)
                    {
                        doc.InsertText(doc.CaretPosition, $"Sáng {tt.FirstOrDefault().Sang} Chiều {tt.FirstOrDefault().Chieu} Tối {tt.FirstOrDefault().Toi}");
                    }
                }
                else if (bm.Name == MyConstant.CONST_SheetName_DienThoai)
                {
                    doc.CaretPosition = bm.Range.Start;
                    doc.Delete(bm.Range);
                    doc.InsertText(doc.CaretPosition, BaseFrom.BanQuyenKeyInfo.PhoneNumber);
                }
                else if (bm.Name == MyConstant.CONST_SheetName_ThongTinMayNC)
                {

                    var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                    WordTable tbl = crCell.Table;
                    tbl.BeginUpdate();
                    int crRowInd = crCell.Row.Index;
                    if (lsSourceKHVT.Count() != 0)
                    {
                        List<KLTTHangNgay> lsSourceNC = lsSourceKHVT.Where(x => x.LoaiVatTu == "Nhân công").ToList();
                        List<KLTTHangNgay> lsSourceMTC = lsSourceKHVT.Where(x => x.LoaiVatTu == "Máy thi công").ToList();
                        if (lsSourceNC.Count() != 0)
                        {
                            double SoNhanCong = (double)lsSourceNC.Sum(x => x.KhoiLuongThiCong);
                            string donVi = lsSourceNC.FirstOrDefault().DonVi;
                            doc.Replace(tbl.Rows[1][1].ContentRange, $"Nhân công: {Fcn_TinhToan(SoNhanCong).ToString("N1")} {donVi}");

                        }
                        if (lsSourceMTC.Count() != 0)
                        {
                            var CrMTC = lsSourceMTC.GroupBy(x => x.TenCongTac);
                            string TenMay = "";
                            int STT = 1;
                            foreach (var item in CrMTC)
                            {
                                double SoMay = Math.Round((double)item.Sum(x => x.KhoiLuongThiCong), 2);
                                TenMay += $"{STT++}. {item.FirstOrDefault().TenCongTac}: {SoMay.ToString("N1")}({item.FirstOrDefault().DonVi})\r\n ";
                            }

                            doc.Replace(tbl.Rows[1][0].ContentRange, TenMay);


                        }

                        if (tt.Count() != 0)
                        {
                            var f = tt.First();
                            doc.Replace(tbl.Rows[3][1].ContentRange, f.Sang);
                            doc.Replace(tbl.Rows[3][2].ContentRange, f.Chieu);
                            doc.Replace(tbl.Rows[3][3].ContentRange, f.Toi);
                        }
                    }
                    int IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();
                }
                else if (bm.Name == MyConstant.CONST_SheetName_NhanLuc)
                {
                    List<DonViThucHien> NhanThau = DVTH.Where(x => !x.IsGiaoThau).ToList();
                    int i = 1;

                    var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                    WordTable tbl = crCell.Table;
                    tbl.BeginUpdate();
                    int crRowInd = crCell.Row.Index;

                    foreach (DonViThucHien item in NhanThau)
                    {
                        DataRow[] Crow = dt.AsEnumerable().Where(x => x[item.ColCodeFK].ToString() == item.Code && x["LoaiVatTu"].ToString() == "Nhân công").ToArray();
                        if (Crow.Count() != 0)
                        {
                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: true);

                            List<KLTTHangNgay> KHVTNhanThau = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(Crow.CopyToDataTable());
                            double SoNC = KHVTNhanThau.Sum(x => x.KhoiLuongThiCong) ?? 0;
                            double SoNCKH = KHVTNhanThau.Sum(x => x.KhoiLuongKeHoach) ?? 0;
                            string TenMay = "";
                            int STT = 1;
                            if (SoNC > 0)
                                KHVTNhanThau.ForEach(x => TenMay += $"{STT++}. {x.TenCongTac}: {Fcn_TinhToan((double)x.KhoiLuongThiCong)}({x.DonVi})\n");

                            doc.Replace(RowInSert[(int)NhanLucEnum.STT].ContentRange, (i - 1).ToString());
                            doc.Replace(RowInSert[(int)NhanLucEnum.TenDonViThucHien].ContentRange, $"{item.Ten}:\n{TenMay}");
                            doc.Replace(RowInSert[(int)NhanLucEnum.KeHoach].ContentRange, SoNCKH.ToString("N0"));
                            doc.Replace(RowInSert[(int)NhanLucEnum.ThucTe].ContentRange, SoNC.ToString("N0"));
                        }
                    }
                    int IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();

                }
                else if (bm.Name == MyConstant.CONST_SheetName_MayTC)
                {
                    List<DonViThucHien> NhanThau = DVTH.Where(x => !x.IsGiaoThau).ToList();
                    int i = 1;

                    var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                    WordTable tbl = crCell.Table;
                    tbl.BeginUpdate();
                    int crRowInd = crCell.Row.Index;

                    foreach (DonViThucHien item in NhanThau)
                    {
                        DataRow[] Crow = dt.AsEnumerable().Where(x => x[item.ColCodeFK].ToString() == item.Code && x["LoaiVatTu"].ToString() == "Máy thi công").ToArray();
                        if (Crow.Count() != 0)
                        {
                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: true);

                            List<KLTTHangNgay> KHVTNhanThau = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(Crow.CopyToDataTable());
                            double SoNC = KHVTNhanThau.Sum(x => x.KhoiLuongThiCong) ?? 0;
                            double SoNCKH = KHVTNhanThau.Sum(x => x.KhoiLuongKeHoach) ?? 0;
                            string TenMay = "";
                            int STT = 1;
                            KHVTNhanThau.Where(x => x.KhoiLuongThiCong > 0).ToList().ForEach(x => TenMay += $"{STT++}. {x.TenCongTac}: {Fcn_TinhToan((double)x.KhoiLuongThiCong)}({x.DonVi})\n");

                            doc.Replace(RowInSert[(int)NhanLucEnum.STT].ContentRange, (i - 1).ToString());
                            doc.Replace(RowInSert[(int)NhanLucEnum.TenDonViThucHien].ContentRange, $"{item.Ten}:\n{TenMay}");
                            doc.Replace(RowInSert[(int)NhanLucEnum.KeHoach].ContentRange, SoNCKH.ToString("N0"));
                            doc.Replace(RowInSert[(int)NhanLucEnum.ThucTe].ContentRange, SoNC.ToString("N0"));
                        }
                    }
                    int IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();
                }
                else if (bm.Name == MyConstant.CONST_SheetName_CongTacTrongNgay)
                {
                    bool isFullTemplate = fileMaus.FileName.Contains("Full");

                    Dictionary<CTTrongNgay, int> dic = new Dictionary<CTTrongNgay, int>();

                    foreach (CTTrongNgay item in Enum.GetValues(typeof(CTTrongNgay)))
                    {
                        dic.Add(item, (int)item);
                    }
                    if (!isFullTemplate)
                    {
                        dic[CTTrongNgay.TyLeHoanThanh] = (int)CTTrongNgay.KLConLai + 1;
                    }

                    int STTDVTH = 0;
                    double KLKHKN = 0, KLKHHT = 0;
                    var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                    WordTable tbl = crCell.Table;
                    tbl.BeginUpdate();

                    int crRowInd = crCell.Row.Index;

                    List<TableRow> rowsDVTH = new List<TableRow>();

                    foreach (DonViThucHien item in DVTH.Where(x => !x.IsGiaoThau).OrderBy(x => x.LoaiGiaoNhanThau))
                    {
                        if (lstCTTheoCK.Count() == 0)
                            continue;
                        var Crows = dtCongTacTheoNgay.AsEnumerable().Where(x => x.GetValueByPropName(item.ColCodeFK)?.ToString() == item.Code).ToArray();
                        if (Crows.Count() == 0)
                            continue;

                        TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                        RowInSert.Range.UpdateBasicFormatRange(isBold: true);

                        doc.Replace(RowInSert[dic[CTTrongNgay.STT]].ContentRange, $"{++STTDVTH}");
                        doc.Replace(RowInSert[dic[CTTrongNgay.Ten]].ContentRange, item.Ten.ToUpper());


                        var crRowDVTH = RowInSert;
                        rowsDVTH.Add(crRowDVTH);
                        double TTHopDongDVTH = 0;
                        double TTThiCongDVTH = 0;
                        double TTLuyKeDVTH = 0;


                        var CrCongTrinh = Crows.GroupBy(x => x.CodeCongTrinh);

                        int STTCongTrinh = 0;
                        foreach (var Ctrinh in CrCongTrinh)
                        {
                            RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_CongTrinh);

                            doc.Replace(RowInSert[dic[CTTrongNgay.STT]].ContentRange, $"{STTDVTH}.{++STTCongTrinh}");
                            doc.Replace(RowInSert[dic[CTTrongNgay.Ten]].ContentRange, Ctrinh.First().TenCongTrinh);

                            var CrHangMuc = Ctrinh.GroupBy(x => x.CodeHangMuc);

                            int STTHangMuc = 0;
                            foreach (var CHM in CrHangMuc)
                            {
                                RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_HangMuc);

                                doc.Replace(RowInSert[dic[CTTrongNgay.STT]].ContentRange, $"{STTDVTH}.{STTCongTrinh}.{++STTHangMuc}");
                                doc.Replace(RowInSert[dic[CTTrongNgay.Ten]].ContentRange, CHM.First().TenHangMuc);

                                var CrCongtac = CHM.GroupBy(x => x.Code);
                                foreach (var Ctac in CrCongtac)
                                {
                                    RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                    RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyConstant.color_Nomal);

                                    //long.TryParse(Ctac.First().DonGia.ToString(), out long dongia);
                                    var fstCtac = Ctac.First();
                                    var klhnInCtac = KLHN.SingleOrDefault(x => x.ParentCode == fstCtac.Code);

                                    //var crCTDate = Ctac.FirstOrDefault(x => x.dtNgay == de_begin.DateTime.Date);

                                    doc.Replace(RowInSert[dic[CTTrongNgay.Ten]].ContentRange, fstCtac.TenCongTac);
                                    doc.Replace(RowInSert[dic[CTTrongNgay.DonVi]].ContentRange, fstCtac.DonVi);

                                    if (isFullTemplate)
                                    {
                                        doc.Replace(RowInSert[dic[CTTrongNgay.DonGiaHopDong]].ContentRange, fstCtac.DonGia.ToString("N0"));
                                        doc.Replace(RowInSert[dic[CTTrongNgay.DonGiaThiCong]].ContentRange, (klhnInCtac.DonGiaNgayTinhLuyKe ?? 0).ToString("N0"));
                                    }

                                    double KLHopDong = fstCtac.KhoiLuongHopDongChiTiet;
                                    double KLThiCong = klhnInCtac?.KLTCInRange ?? 0;
                                    double KLLuyKe = (klhnInCtac?.LuyKeKLTCFirstDate - klhnInCtac?.KLTCFirstDate) ?? 0;
                                    double KLConLai = KLHopDong - KLLuyKe;


                                    long TTHopDong = fstCtac.ThanhTienHopDong;
                                    double TTThiCong = klhnInCtac?.TTTCInRange ?? 0;
                                    double TTLuyKe = (klhnInCtac?.LuyKeTTTCFirstDate - klhnInCtac?.TTTCFirstDate) ?? 0;
                                    double TTConLai = TTHopDong - TTLuyKe;

                                    double TyLe = (isFullTemplate) ? ((TTHopDong == 0) ? 0 : (double)TTLuyKe * 100.0 / (double)TTHopDong)
                                        : ((TTHopDong == 0) ? 0 : (double)KLLuyKe * 100.0 / (double)KLHopDong);

                                    doc.Replace(RowInSert[dic[CTTrongNgay.KLHopDong]].ContentRange, KLHopDong.ToN2String());
                                    doc.Replace(RowInSert[dic[CTTrongNgay.KLThiCong]].ContentRange, KLThiCong.ToN2String());
                                    doc.Replace(RowInSert[dic[CTTrongNgay.KLLuyKeThiCong]].ContentRange, KLLuyKe.ToN2String());
                                    doc.Replace(RowInSert[dic[CTTrongNgay.KLConLai]].ContentRange, KLConLai.ToN2String());

                                    if (isFullTemplate)
                                    {
                                        doc.Replace(RowInSert[dic[CTTrongNgay.TTHopDong]].ContentRange, TTHopDong.ToN0String());
                                        doc.Replace(RowInSert[dic[CTTrongNgay.TTThiCong]].ContentRange, TTThiCong.ToN0String());
                                        doc.Replace(RowInSert[dic[CTTrongNgay.TTLuyKeThiCong]].ContentRange, TTLuyKe.ToN0String());
                                        doc.Replace(RowInSert[dic[CTTrongNgay.TTConLai]].ContentRange, TTConLai.ToN0String());
                                    }

                                    doc.Replace(RowInSert[dic[CTTrongNgay.TyLeHoanThanh]].ContentRange, TyLe.ToN2String());


                                    TTHopDongDVTH += TTHopDong;
                                    TTThiCongDVTH += TTThiCong;
                                    TTLuyKeDVTH += TTLuyKe;
                                }
                            }
                        }

                        if (isFullTemplate)
                        {
                            double TTConLaiDVTH = TTHopDongDVTH - TTLuyKeDVTH;
                            double TyLeDVTH = (TTHopDongDVTH == 0) ? 0 : (double)TTLuyKeDVTH * 100.0 / (double)TTHopDongDVTH;

                            doc.Replace(crRowDVTH[dic[CTTrongNgay.TTHopDong]].ContentRange, TTHopDongDVTH.ToN0String());
                            doc.Replace(crRowDVTH[dic[CTTrongNgay.TTThiCong]].ContentRange, TTThiCongDVTH.ToN0String());
                            doc.Replace(crRowDVTH[dic[CTTrongNgay.TTLuyKeThiCong]].ContentRange, TTLuyKeDVTH.ToN0String());


                            doc.Replace(crRowDVTH[dic[CTTrongNgay.TTConLai]].ContentRange, TTConLaiDVTH.ToN0String());

                            doc.Replace(crRowDVTH[dic[CTTrongNgay.TyLeHoanThanh]].ContentRange, TyLeDVTH.ToN2String());
                        }
                    }
                    foreach (var row in rowsDVTH)
                    {
                        row.Cells.ForEach(x => x.BackgroundColor = Color.LightBlue);
                    }

                    int IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();

                }
                else if (bm.Name == MyConstant.CONST_SheetName_CongTacHoanThanh)
                {
                    //ws = wb.Worksheets[bm.Name];
                    //CellRange CongTacHoanThanh;
                    //int i = 1;
                    int STTDVTH = 0;

                    double KLTC = 0;
                    var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                    WordTable tbl = crCell.Table;
                    tbl.BeginUpdate();
                    int crRowInd = crCell.Row.Index;

                    foreach (DonViThucHien item in DVTH.Where(x => !x.IsGiaoThau).OrderBy(x => x.LoaiGiaoNhanThau))
                    {
                        if (GiaoViecCT.Rows.Count == 0)
                            continue;
                        DataRow[] Crow = GiaoViecCT.AsEnumerable().Where(x => x[item.ColCodeFK].ToString() == item.Code && DateTime.Parse(x["NgayDuyet"].ToString()).Date == de_begin.DateTime.Date).ToArray();
                        if (Crow.Count() == 0)
                            continue;
                        //CongTacHoanThanh = ws.Range[bm.Name];
                        //doc.CaretPosition
                        TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                        RowInSert.Range.UpdateBasicFormatRange(isBold: true);

                        doc.Replace(RowInSert[(int)CT30Ngay.STT].ContentRange, $"{++STTDVTH}");
                        doc.Replace(RowInSert[(int)CTHTEnum.Ten].ContentRange, item.Ten.ToUpper());

                        var CrCongTrinh = Crow.GroupBy(x => x["CodeCongTrinh"]);

                        int STTCongTrinh = 0;
                        foreach (var Ctrinh in CrCongTrinh)
                        {
                            RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_CongTrinh);

                            doc.Replace(RowInSert[(int)CT30Ngay.STT].ContentRange, $"{STTDVTH}.{++STTCongTrinh}");
                            doc.Replace(RowInSert[(int)CTHTEnum.Ten].ContentRange, Ctrinh.FirstOrDefault()["TenCongTrinh"].ToString());

                            var CrHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"]);

                            int STTHangMuc = 0;
                            foreach (var CHM in CrHangMuc)
                            {

                                RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_HangMuc);

                                doc.Replace(RowInSert[(int)CT30Ngay.STT].ContentRange, $"{STTDVTH}.{STTCongTrinh}.{++STTHangMuc}");
                                doc.Replace(RowInSert[(int)CTHTEnum.Ten].ContentRange, CHM.FirstOrDefault()["TenHangMuc"].ToString());

                                var CrCongtac = CHM.GroupBy(x => x["Code"]);
                                foreach (var Ctac in CrCongtac)
                                {
                                    RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                    RowInSert.Range.UpdateBasicFormatRange(isBold: false);

                                    doc.Replace(RowInSert[(int)CTHTEnum.Ten].ContentRange, Ctac.FirstOrDefault()["TenCongViec"].ToString());
                                    doc.Replace(RowInSert[(int)CTHTEnum.DonVi].ContentRange, Ctac.FirstOrDefault()["DonVi"].ToString());
                                    doc.Replace(RowInSert[(int)CTHTEnum.NguoiYeuCau].ContentRange, Ctac.FirstOrDefault()["FullNameSend"].ToString());
                                    doc.Replace(RowInSert[(int)CTHTEnum.NguoiDuyet].ContentRange, Ctac.FirstOrDefault()["FullNameApprove"].ToString());
                                    doc.Replace(RowInSert[(int)CTHTEnum.LyTrinh].ContentRange, Ctac.FirstOrDefault()["LyTrinhCaoDo"].ToString());
                                    doc.Replace(RowInSert[(int)CTHTEnum.ThoiGianDuyet].ContentRange, Ctac.FirstOrDefault()["NgayDuyet"].ToString());

                                    KLTC = Ctac.FirstOrDefault()["KhoiLuongThiCong"] == DBNull.Value ? 0 : double.Parse(Ctac.FirstOrDefault()["KhoiLuongThiCong"].ToString());
                                    doc.Replace(RowInSert[(int)CTHTEnum.KLThiCong].ContentRange, KLTC.ToString("N2"));
                                }

                            }

                        }

                    }
                    int IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();
                }
                else if (bm.Name == MyConstant.CONST_SheetName_CongTac30Ngay)
                {
                    int STTDVTH = 0;

                    double KLKHKN = 0, KLKHHT = 0;
                    var crCell = doc.Tables.GetTableCell(bm.Range.Start);

                    WordTable tbl = crCell.Table;
                    tbl.BeginUpdate();
                    int crRowInd = crCell.Row.Index;


                    foreach (DonViThucHien item in DVTH.Where(x => !x.IsGiaoThau).OrderBy(x => x.LoaiGiaoNhanThau))
                    {
                        if (lstCTTheoCK.Count() == 0)
                            continue;
                        DataRow[] Crow = dt30Day.AsEnumerable().Where(x => x[item.ColCodeFK].ToString() == item.Code).ToArray();
                        if (Crow.Count() == 0)
                            continue;

                        TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                        RowInSert.Range.UpdateBasicFormatRange(isBold: true);

                        doc.Replace(RowInSert[(int)CT30Ngay.STT].ContentRange, $"{++STTDVTH}");
                        doc.Replace(RowInSert[(int)CT30Ngay.Ten].ContentRange, item.Ten.ToUpper());

                        var CrCongTrinh = Crow.GroupBy(x => x["CodeCongTrinh"]);

                        int STTCongTrinh = 0;
                        foreach (var Ctrinh in CrCongTrinh)
                        {
                            RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_CongTrinh);

                            doc.Replace(RowInSert[(int)CT30Ngay.STT].ContentRange, $"{STTDVTH}.{++STTCongTrinh}");
                            doc.Replace(RowInSert[(int)CT30Ngay.Ten].ContentRange, Ctrinh.FirstOrDefault()["TenCongTrinh"].ToString());

                            var CrHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"]);

                            int STTHangMuc = 0;
                            foreach (var CHM in CrHangMuc)
                            {
                                RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_HangMuc);

                                doc.Replace(RowInSert[(int)CT30Ngay.STT].ContentRange, $"{STTDVTH}.{STTCongTrinh}.{++STTHangMuc}");
                                doc.Replace(RowInSert[(int)CT30Ngay.Ten].ContentRange, CHM.FirstOrDefault()["TenHangMuc"].ToString());

                                var CrCongtac = CHM.GroupBy(x => x["Code"]);
                                foreach (var Ctac in CrCongtac)
                                {
                                    RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                    RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyConstant.color_Nomal);

                                    long.TryParse(Ctac.FirstOrDefault()["DonGiaKeHoach"].ToString(), out long dongia);

                                    doc.Replace(RowInSert[(int)CT30Ngay.Ten].ContentRange, Ctac.FirstOrDefault()["TenCongTac"].ToString());
                                    doc.Replace(RowInSert[(int)CT30Ngay.DonVi].ContentRange, Ctac.FirstOrDefault()["DonVi"].ToString());
                                    doc.Replace(RowInSert[(int)CT30Ngay.DonGia].ContentRange, dongia.ToString("N0"));
                                    doc.Replace(RowInSert[(int)CT30Ngay.LyTrinh].ContentRange, Ctac.FirstOrDefault()["LyTrinhCaoDo"].ToString());


                                    var kls = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, new string[] { Ctac.Key.ToString() }, de_begin.DateTime.Date, de_begin.DateTime.AddDays(30).Date);

                                    KLKHKN = (double)kls.Sum(x => x.KhoiLuongKeHoach);
                                    KLKHHT = (double)kls.Sum(x => x.KhoiLuongThiCong);

                                    doc.Replace(RowInSert[(int)CT30Ngay.KeHoach].ContentRange, KLKHKN.ToN2String());
                                    doc.Replace(RowInSert[(int)CT30Ngay.ThucHien].ContentRange, KLKHHT.ToN2String());

                                    doc.Replace(RowInSert[(int)CT30Ngay.ThanhTienKeHoach].ContentRange, (KLKHKN * dongia).ToString("N0"));

                                    if (KLKHKN != 0)
                                        doc.Replace(RowInSert[(int)CT30Ngay.TyLe].ContentRange, $"{(KLKHHT / KLKHKN * 100).ToString("N2")} %");
                                }
                            }
                        }
                    }
                    int IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();

                }
                else if (bm.Name == MyConstant.CONST_SheetName_DeXuatVatLieu)
                {
                    int i = 2;
                    int STT = 1;


                    var crCell = doc.Tables.GetTableCell(doc.Bookmarks[MyConstant.CONST_BCHN_BM_VatTuThucHien].Range.Start);
                    WordTable tbl = crCell.Table;
                    tbl.BeginUpdate();
                    int crRowInd = crCell.Row.Index;

                    if (dtVatLieu.Rows.Count != 0)
                    {
                        foreach (DataRow row in dtVatLieu.Rows)
                        {
                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyColor.Normal);

                            double.TryParse(row["HopDongKl"].ToString(), out double KLHD);
                            double.TryParse(row["KhoiLuong"].ToString(), out double KLDeXuat);
                            double.TryParse(row["LuyKeYeuCau"].ToString(), out double KLNhap);
                            double.TryParse(row["LuyKeXuatKho"].ToString(), out double LuyKeXuatKho);

                            doc.Replace(RowInSert[(int)VatTuEnum.STT].ContentRange, $"{STT++}");
                            doc.Replace(RowInSert[(int)VatTuEnum.Ten].ContentRange, row["TenVatTu"].ToString());
                            doc.Replace(RowInSert[(int)VatTuEnum.DonVi].ContentRange, row["DonVi"].ToString());
                            doc.Replace(RowInSert[(int)VatTuEnum.NhaCungCap].ContentRange, row["Ten"].ToString());
                            doc.Replace(RowInSert[(int)VatTuEnum.KhoiLuongHopDong].ContentRange, KLHD.ToString("N2"));
                            doc.Replace(RowInSert[(int)VatTuEnum.KLDeXuat].ContentRange, KLDeXuat.ToString("N2"));
                            doc.Replace(RowInSert[(int)VatTuEnum.KLNhap].ContentRange, KLNhap.ToString("N2"));
                            doc.Replace(RowInSert[(int)VatTuEnum.KLDaXuat].ContentRange, LuyKeXuatKho.ToString("N2"));
                        }


                    }
                    int IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();

                    crCell = doc.Tables.GetTableCell(doc.Bookmarks[MyConstant.CONST_BCHN_BM_VatTuDeXuat].Range.Start);
                    tbl = crCell.Table;
                    tbl.BeginUpdate();

                    crRowInd = crCell.Row.Index;
                    if (dtVatLieuYeuCau.Rows.Count != 0)
                    {
                        STT = 1;
                        foreach (DataRow row in dtVatLieuYeuCau.Rows)
                        {
                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyColor.Normal);

                            double.TryParse(row["HopDongKl"].ToString(), out double KLHD);
                            double.TryParse(row["YeuCauDotNay"].ToString(), out double KLDeXuat);
                            //double.TryParse(row["LuyKeYeuCau"].ToString(), out double KLNhap);
                            //double.TryParse(row["LuyKeXuatKho"].ToString(), out double LuyKeXuatKho);

                            doc.Replace(RowInSert[(int)VatTuEnum.STT].ContentRange, $"{STT++}");
                            doc.Replace(RowInSert[(int)VatTuEnum.Ten].ContentRange, row["TenVatTu"].ToString());
                            doc.Replace(RowInSert[(int)VatTuEnum.DonVi].ContentRange, row["DonVi"].ToString());
                            doc.Replace(RowInSert[(int)VatTuEnum.NhaCungCap].ContentRange, row["Ten"].ToString());
                            doc.Replace(RowInSert[(int)VatTuEnum.KhoiLuongHopDong].ContentRange, KLHD.ToString("N2"));
                            doc.Replace(RowInSert[(int)VatTuEnum.KLDeXuat].ContentRange, KLDeXuat.ToString("N2"));
                            //doc.Replace(RowInSert[(int)VatTuEnum.KLNhap].ContentRange, KLNhap.ToString("N2"));
                            //doc.Replace(RowInSert[(int)VatTuEnum.KLDaXuat].ContentRange, LuyKeXuatKho.ToString("N2"));
                        }

                    }
                    IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();

                }
                else if (bm.Name == MyConstant.CONST_SheetName_KhoanChi)
                {
                    int i = 2;
                    int STT = 1;

                    DataRow[] CrowKC = dtKC.AsEnumerable().Where(x => x["CheckDaChi"].ToString() == "True" && DateTime.Parse(x["DateXacNhanDaChi"].ToString()).Date == de_begin.DateTime.Date).ToArray();
                    DataRow[] CrowDX = dtKC.AsEnumerable().Where(x => x["TrangThai"].ToString() == "1" && x["NguonThuChi"].ToString() == "1").ToArray();

                    int crRowInd = 0;
                    var crCell = doc.Tables.GetTableCell(doc.Bookmarks[MyConstant.CONST_BCHN_BM_ChiTietKhoanChi].Range.Start);

                    WordTable tbl = crCell.Table;
                    tbl.BeginUpdate();
                    crRowInd = crCell.Row.Index;
                    if (CrowKC.Count() != 0)
                    {
                        foreach (var row in CrowKC)
                        {
                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyColor.Normal);

                            if (!long.TryParse(row["GiaTriGiaiChi"].ToString(), out long GiaTri))
                                GiaTri = 0;

                            doc.Replace(RowInSert[(int)ThuChiEnum.STT].ContentRange, $"{STT++}");
                            doc.Replace(RowInSert[(int)ThuChiEnum.NoiDung].ContentRange, row["NoiDungUng"].ToString());
                            doc.Replace(RowInSert[(int)ThuChiEnum.SoTien].ContentRange, GiaTri.ToString("N2"));
                            doc.Replace(RowInSert[(int)ThuChiEnum.NguoiNhan].ContentRange, row["TenNhanVien"].ToString());
                            doc.Replace(RowInSert[(int)ThuChiEnum.GhiChu].ContentRange, row["GhiChuKC"].ToString());
                        }
                    }
                    int IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();

                    crCell = doc.Tables.GetTableCell(doc.Bookmarks[MyConstant.CONST_BCHN_BM_KhoanChiDeXuat].Range.Start);

                    tbl = crCell.Table;
                    tbl.BeginUpdate();
                    crRowInd = crCell.Row.Index;
                    if (CrowDX.Count() != 0)
                    {

                        STT = 1;
                        foreach (var row in CrowDX)
                        {

                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyColor.Normal);

                            if (!long.TryParse(row["GiaTriDotNay"].ToString(), out long giatri))
                                giatri = 0;

                            doc.Replace(RowInSert[(int)ThuChiEnum.STT].ContentRange, $"{STT++}");
                            doc.Replace(RowInSert[(int)ThuChiEnum.NoiDung].ContentRange, row["NoiDungUng"].ToString());
                            doc.Replace(RowInSert[(int)ThuChiEnum.SoTien].ContentRange, giatri.ToString("N2"));
                            doc.Replace(RowInSert[(int)ThuChiEnum.NguoiNhan].ContentRange, row["TenNhanVien"].ToString());
                            doc.Replace(RowInSert[(int)ThuChiEnum.GhiChu].ContentRange, row["GhiChu"].ToString());
                        }
                    }
                    IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();
                }
                else if (bm.Name == MyConstant.CONST_SheetName_KhoanThu)
                {
                    int i = 2;
                    int STT = 0;

                    DataRow[] CrowDX = dtKC.AsEnumerable().Where(x => x["TrangThai"].ToString() == "1" && x["NguonThuChi"].ToString() == "2").ToArray();
                    int crRowInd = 0;

                    var crCell = doc.Tables.GetTableCell(doc.Bookmarks[MyConstant.CONST_BCHN_BM_ChiTietKhoanThu].Range.Start);

                    WordTable tbl = crCell.Table;
                    tbl.BeginUpdate();
                    crRowInd = crCell.Row.Index;

                    if (dtKT.Rows.Count != 0)
                    {
                        foreach (DataRow row in dtKT.Rows)
                        {
                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyColor.Normal);

                            if (!long.TryParse(row["ThucTeThu"].ToString(), out long thucTeThu))
                                thucTeThu = 0;

                            doc.Replace(RowInSert[(int)ThuChiEnum.STT].ContentRange, $"{STT++}");
                            doc.Replace(RowInSert[(int)ThuChiEnum.NoiDung].ContentRange, row["NoiDungThu"].ToString());
                            doc.Replace(RowInSert[(int)ThuChiEnum.SoTien].ContentRange, thucTeThu.ToString("N2"));
                            doc.Replace(RowInSert[(int)ThuChiEnum.NguoiNhan].ContentRange, row["TenNhanVien"].ToString());
                        }


                    }
                    int IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();

                    crCell = doc.Tables.GetTableCell(doc.Bookmarks[MyConstant.CONST_BCHN_BM_KhoanThuDeXuat].Range.Start);

                    tbl = crCell.Table;
                    tbl.BeginUpdate();
                    crRowInd = crCell.Row.Index;
                    if (CrowDX.Count() != 0)
                    {

                        STT = 1;
                        foreach (var row in CrowDX)
                        {
                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyColor.Normal);

                            if (!long.TryParse(row["GiaTriDotNay"].ToString(), out long giatri))
                                giatri = 0;

                            doc.Replace(RowInSert[(int)ThuChiEnum.STT].ContentRange, $"{STT++}");
                            doc.Replace(RowInSert[(int)ThuChiEnum.NoiDung].ContentRange, row["NoiDungUng"].ToString());
                            doc.Replace(RowInSert[(int)ThuChiEnum.SoTien].ContentRange, giatri.ToString("N2"));
                            doc.Replace(RowInSert[(int)ThuChiEnum.NguoiNhan].ContentRange, row["TenNhanVien"].ToString());
                            doc.Replace(RowInSert[(int)ThuChiEnum.GhiChu].ContentRange, row["GhiChu"].ToString());
                        }

                    }
                    IndBm = crCell.Row.Index;
                    crCell.Row.Delete();
                    tbl.Rows[IndBm - 1].Delete();
                    tbl.EndUpdate();
                }

            }

            rec.EndUpdate();

            WaitFormHelper.CloseWaitForm();
            //MessageShower.ShowInformation("Xuất báo cáo thành công!", "Thông tin");

            //string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
            //doc.SaveDocument(Path.Combine(PathSave, $"Báo cáo hằng ngày_{DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.docx"), DocumentFormat.OpenXml);
            //rec_XemTruoc.LoadDocument(Path.Combine(PathSave, $"Báo cáo hằng ngày_{time}.docx"));

            //}

        }

        private void slke_ChonMauBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            if (FilesBaoCaoHangNgay.Contains(fileMaus.FileName))
            {
                lcg_TheoKy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                lcg_TheoKy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcg_KySau.Visibility = lcg_KyTruoc.Visibility = lci_CachLoc.Visibility = lci_LocCongTac.Visibility = lci_NgayXuat.Visibility = lcg_ThongSoCaiDat.Visibility = lci_SoCotHinhAnh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; ;
                lcg_CaiDatChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (fileMaus.FileName == "_OFFLINE_Báo cáo hằng ngày.xlsx")
                {
                    lcg_KySau.Visibility = lcg_KyTruoc.Visibility = lci_CachLoc.Visibility = lci_LocCongTac.Visibility = lci_NgayXuat.Visibility = lcg_ThongSoCaiDat.Visibility = lci_SoCotHinhAnh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcg_CaiDatChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    de_EndDateCrPeriod.DateTime = de_StartDateCrPeriod.DateTime = DateTime.Now.AddDays(-1);
                }
            }




            if (uc_BaoCao1.PreviewSelectedIndex != 0)
                uc_BaoCao1.PreviewSelectedIndex = 0;
            else
                LoadBaoCao();
        }

        private void rg_XemTruoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void de_begin_EditValueChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
        }

        private void slke_ThongTinDuAn_EditValueChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
        }

        private void nud_SoCot_ValueChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;

        }

        private void ce_DinhKemTenAnh_CheckedChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;

        }

        private void ce_tblBorder_CheckedChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;

        }
        private void Args_Showing_XuatFile(object sender, XtraMessageShowingArgs e)
        {
            e.Form.Appearance.FontStyleDelta = FontStyle.Bold;
            e.Form.Appearance.FontSizeDelta = 2;
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string Name = fileMaus?.FileNameWithoutExtension ?? "Báo cáo hàng ngày";
            string fileName = $"{Name}_{DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}";
            XtraFolderBrowserDialog Xtra = new XtraFolderBrowserDialog();
            string PathSave = "";
            if (Xtra.ShowDialog() == DialogResult.OK)
            {
                PathSave = Xtra.SelectedPath;
            }
            else
                return;
            foreach (var control in e.Form.Controls)
            {
                SimpleButton button = control as SimpleButton;
                if (button != null)
                {
                    button.ImageOptions.SvgImageSize = new Size(16, 16);
                    // button.Height = 25;
                    switch (button.DialogResult.ToString())
                    {
                        case ("OK"):
                            button.Text = "File PDF";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                WaitFormHelper.ShowWaitForm("Đang tải dữ liệu!! Vui lòng chờ!!!");
                                uc_BaoCao1.SpreadSheet.Document.Unit = DevExpress.Office.DocumentUnit.Inch;

                                // Access page margins.
                                DevExpress.Spreadsheet.Margins pageMargins = uc_BaoCao1.SpreadSheet.ActiveWorksheet.ActiveView.Margins;

                                // Specify page margins.
                                pageMargins.Left = 0.25F;
                                pageMargins.Top = 0.75F;
                                pageMargins.Right = 0.25F;
                                pageMargins.Bottom = 0.75F;
                                uc_BaoCao1.SpreadSheet.ActiveWorksheet.ActiveView.Orientation = PageOrientation.Landscape;
                                uc_BaoCao1.SpreadSheet.ActiveWorksheet.ActiveView.PaperKind = PaperKind.A4;
                                //uc_BaoCao1.SpreadSheet.ActiveWorksheet.ActiveView.
                                uc_BaoCao1.SpreadSheet.ActiveWorksheet.PrintOptions.FitToWidth = 1;
                                uc_BaoCao1.SpreadSheet.ActiveWorksheet.PrintOptions.FitToHeight = 0;
                                uc_BaoCao1.SpreadSheet.ActiveWorksheet.PrintOptions.FitToPage = true;

                                uc_BaoCao1.SpreadSheet.ExportToPdf(Path.Combine(PathSave, $"{fileName}.pdf"));
                                WaitFormHelper.CloseWaitForm();
                                //fileName = spsheet.Options.Save.CurrentFileName;
                                DialogResult dialogResult = MessageShower.ShowYesNoQuestion("File lưu thành công. Bạn có muốn mở file luôn hay không ???", "Thông báo");
                                if (dialogResult == DialogResult.Yes)
                                {
                                    System.Diagnostics.Process.Start(Path.Combine(PathSave, $"{fileName}.pdf"));
                                }
                                //uc_BaoCao1.SpreadSheet.ShowRibbonPrintPreview();

                            };
                            break;
                        case ("Yes"):
                            button.Text = "File Excel";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                var spsheet = uc_BaoCao1.SpreadSheet;
                                //spsheet.Options.Save.CurrentFileName = $"{fileName}.xlsx";
                                spsheet.Document.SaveDocument(Path.Combine(PathSave, $"{fileName}.xlsx"), DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                                DialogResult dialogResult = MessageShower.ShowYesNoQuestion("File lưu thành công. Bạn có muốn mở file luôn hay không ???", "Thông báo");
                                if (dialogResult == DialogResult.Yes)
                                {
                                    System.Diagnostics.Process.Start(Path.Combine(PathSave, $"{fileName}.xlsx"));
                                }
                            };
                            break;
                        default:
                            button.Text = "Thoát";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) => { e.Form.Close(); };
                            break;
                    }
                }
            }
        }
        private void uc_BaoCao1_CustomExportMau(object sender, EventArgs e)
        {
            if (uc_BaoCao1.PreviewAccessibleName == "HoanChinh")
            {
                var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
                string Name = fileMaus?.FileNameWithoutExtension ?? "Báo cáo hàng ngày";
                string fileName = $"{Name}_{DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}";
                if (uc_BaoCao1._type == BaoCaoFileType.WORD)
                {
                    var rec = uc_BaoCao1.RichEditControl;
                    rec.Options.DocumentSaveOptions.CurrentFileName = $"{fileName}.docx";
                    rec.SaveDocumentAs();
                    fileName = rec.Options.DocumentSaveOptions.CurrentFileName;
                    DialogResult dialogResult = MessageShower.ShowYesNoQuestion("File lưu thành công. Bạn có muốn mở file luôn hay không ???", "Thông báo");
                    if (dialogResult == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
                else
                {
                    XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                    args.Caption = "Lựa chọn loại File muốn xuất";
                    args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Yes, DialogResult.Cancel };
                    args.Showing += Args_Showing_XuatFile;
                    DevExpress.XtraEditors.XtraMessageBox.Show(args);               
                }
            }
        }

        private void uc_BaoCao1_CustomPreviewIndexChanged(object sender, EventArgs e)
        {
            LoadBaoCao();
        }

        private void de_StartDatePrevPeriod_EditValueChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
        }

        private void de_EndDatePrevPeriod_EditValueChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
        }

        private void de_StartDateCrPeriod_EditValueChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
        }

        private void de_EndDateCrPeriod_EditValueChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
        }

        private void de_StartDateNextPeriod_EditValueChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
        }

        private void de_EndDateNextPeriod_EditValueChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
        }

        private void cbe_LocCongTac_SelectedIndexChanged(object sender, EventArgs e)
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;
        }
        private void LoadDate()
        {
            uc_BaoCao1.PreviewSelectedIndex = 0;

            lci_year.Visibility
                = lci_Month.Visibility
                = lci_week.Visibility
                = lci_startDateInWeek.Visibility
                = lci_startDateInMonthYear.Visibility
                = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;


            int year = (int)nud_year.Value;
            int month = monthEdit1.SelectedIndex + 1;

            DateTime fstDateInMonth = new DateTime(year, month, 1);
            DateTime lstDateInMonth = fstDateInMonth.AddMonths(1).AddDays(-1);
            int week = cbbe_Week.SelectedIndex + 1;


            DateTime fstDateInYear = new DateTime(year, 1, 1);
            DateTime lstDateInYear = fstDateInMonth.AddYears(1).AddDays(-1);

            DateTime startDate, endDate;

            switch (cbe_CachLocKyTH.SelectedIndex)
            {
                case (int)PeriodFilterType.CUSTOM:
                    lcg_KyTruoc.Enabled = lcg_KyNay.Enabled = lcg_KySau.Enabled = true;

                    return;
                case (int)PeriodFilterType.WEEKLY:
                    lcg_KyTruoc.Enabled = lcg_KyNay.Enabled = lcg_KySau.Enabled = false;

                    lci_year.Visibility
                        = lci_Month.Visibility
                        = lci_week.Visibility
                        = lci_startDateInWeek.Visibility
                        = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    DayOfWeek startDOW = (DayOfWeek)(cbbe_NgayBatDauMoiTuan.SelectedIndex);
                    DateTimeHelper.CalcNthWeekStartEndDate(year, month, week, startDOW, out startDate, out endDate);

                    de_StartDateCrPeriod.DateTime = startDate;
                    de_EndDateCrPeriod.DateTime = endDate;

                    de_StartDatePrevPeriod.DateTime = startDate.AddDays(-7);
                    de_EndDatePrevPeriod.DateTime = endDate.AddDays(-7);

                    de_StartDateNextPeriod.DateTime = startDate.AddDays(7);
                    de_EndDateNextPeriod.DateTime = endDate.AddDays(7);

                    break;
                case (int)PeriodFilterType.MONTHLY:
                    lcg_KyTruoc.Enabled = lcg_KyNay.Enabled = lcg_KySau.Enabled = false;

                    lci_year.Visibility
                        = lci_Month.Visibility
                        = lci_startDateInMonthYear.Visibility
                        = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    de_NBDThangNam.EditValueChanged -= de_NBDThangNam_EditValueChanged;
                    de_NBDThangNam.EditValue = de_NBDThangNam.Properties.MinValue = fstDateInMonth;
                    de_NBDThangNam.Properties.MaxValue = lstDateInMonth;
                    de_NBDThangNam.EditValueChanged += de_NBDThangNam_EditValueChanged;

                    startDate = de_NBDThangNam.DateTime;
                    endDate = startDate.AddMonths(1).AddDays(-1);

                    de_StartDateCrPeriod.DateTime = startDate;
                    de_EndDateCrPeriod.DateTime = endDate;

                    de_StartDatePrevPeriod.DateTime = startDate.AddMonths(-1);
                    de_EndDatePrevPeriod.DateTime = endDate.AddMonths(-1);

                    de_StartDateNextPeriod.DateTime = startDate.AddMonths(1);
                    de_EndDateNextPeriod.DateTime = endDate.AddMonths(1);

                    break;
                case (int)PeriodFilterType.YEARLY:
                    lcg_KyTruoc.Enabled = lcg_KyNay.Enabled = lcg_KySau.Enabled = false;

                    lci_year.Visibility
                        //= lci_Month.Visibility
                        = lci_startDateInMonthYear.Visibility
                        = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    de_NBDThangNam.EditValueChanged -= de_NBDThangNam_EditValueChanged;
                    de_NBDThangNam.EditValue = de_NBDThangNam.Properties.MinValue = fstDateInYear;
                    de_NBDThangNam.Properties.MaxValue = lstDateInYear;
                    de_NBDThangNam.EditValueChanged += de_NBDThangNam_EditValueChanged;

                    startDate = de_NBDThangNam.DateTime;
                    endDate = startDate.AddYears(1).AddDays(-1);

                    de_StartDateCrPeriod.DateTime = startDate;
                    de_EndDateCrPeriod.DateTime = endDate;

                    de_StartDatePrevPeriod.DateTime = startDate.AddYears(-1);
                    de_EndDatePrevPeriod.DateTime = endDate.AddYears(-1);

                    de_StartDateNextPeriod.DateTime = startDate.AddYears(1);
                    de_EndDateNextPeriod.DateTime = endDate.AddYears(1);

                    break;

                default:
                    return;
            }
            if (slke_ChonMauBaoCao.Text == "Báo cáo biểu 1_BĐH")
            {

                Fcn_LoadSetting(DateTime.Now);
            }


        }

        private async void LoadBaoCaoOnlineTongHopDuAn()
        {
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);

            var wb = uc_BaoCao1.SpreadSheet.Document;
            var ws = wb.Worksheets[0];

            var nbd = de_StartDateCrPeriod.DateTime.Date;
            var nkt = de_EndDateCrPeriod.DateTime.Date;

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


            string[] colsSum =
            {
            TDKH.COL_KhoiLuongHopDongChiTiet,
            TDKH.COL_GTSXTrongKy,
            TDKH.COL_GTSXTrongThang,
            TDKH.COL_GTSXTrongNam,
            TDKH.COL_GTSXDaThiCong,
            };

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
                    if (dtData.Columns.Contains(col))
                        newRowOwner[col] = $"{MyConstant.PrefixFormula}SUM({dic[col]}{crIndRowOwner + 2}:{dic[col]}{crIndRow + 1})";

                }

            }
            ws.Rows.Insert(dfnData.Range.BottomRowIndex, dtData.Rows.Count, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, dfnData.Range.TopRowIndex + 2, 0);
            SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfnData.Range);
            SpreadsheetHelper.FormatRowsInRange(dfnData.Range, dic[TDKH.COL_TypeRow]);
        }

        private async void LoadBaoCaoOnlineNgay()
        {
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);

            var wb = uc_BaoCao1.SpreadSheet.Document;
            var ws = wb.Worksheets[0];

            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            var dfnData = ws.DefinedNames.Single(x => x.Name == nameData);

            string time = "";

            string[] colsSum =
{
          TDKH.COL_GiaTriHopDong,
          TDKH.COL_GTSXTrongKy,
          TDKH.COL_GTSXFromBeginOfPrj,
          TDKH.COL_GTNTTrongKy,
          TDKH.COL_GTNTFromBeginOfPrj,
          TDKH.COL_GTDangDo,
      };


            time = $"BẢNG TỔNG HỢP GIÁ TRỊ THỰC HIỆN NGÀY {de_begin.DateTime.ToShortDateString()}";


            ws.Range[nameTime].SetValue(time);

            WaitFormHelper.ShowWaitForm("Đang tính toán");

            var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<List<KLHNBriefViewModel>>(RouteAPI.TongDuAn_GetBaoCaoNgay,
                new KLHNRequest()
                {
                    type = TypeKLHN.CongTac,
                    dates = new List<DateTime>() { de_begin.DateTime }
                });

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Không thể lấy thông tin dự án từ server!");
                uc_BaoCao1.PreviewSelectedIndex = 0;
                return;
            }

            var dto = result.Dto;

            var grs = dto.GroupBy(x => x.TenCumDuAn);



            DataTable dtData = DataTableCreateHelper.BaoCaoNgayOnline();
            int STTOwner = 0;
            var rowTong = ws.Rows[dfnData.Range.TopRowIndex];
            List<int> lsSum = new List<int>();
            foreach (var gr in grs)
            {
                var fstR = gr.First();
                ++STTOwner;

                DataRow newRowOwner = dtData.NewRow();
                dtData.Rows.Add(newRowOwner);
                int crIndRowOwner = dfnData.Range.TopRowIndex + dtData.Rows.Count;

                newRowOwner[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                newRowOwner[TDKH.COL_DanhMucCongTac] = fstR.TenCumDuAn;
                newRowOwner[TDKH.COL_STT] = STTOwner.ToRoman();
                newRowOwner[TDKH.COL_GTSXTyLe] = $"{MyConstant.PrefixFormula}IF({dic[TDKH.COL_GiaTriHopDong]}{crIndRowOwner + 1} = 0; \"-\"; {dic[TDKH.COL_GTSXFromBeginOfPrj]}{crIndRowOwner + 1}/{dic[TDKH.COL_GiaTriHopDong]}{crIndRowOwner + 1})";

                lsSum.Add(crIndRowOwner);


                int sttDA = 0;
                foreach (var fstDA in gr)
                {
                    ++sttDA;


                    DataRow newRowDA = dtData.NewRow();
                    dtData.Rows.Add(newRowDA);
                    int crIndRowCha = dfnData.Range.TopRowIndex + dtData.Rows.Count;

                    newRowDA[TDKH.COL_STT] = sttDA;
                    newRowDA[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                    newRowDA[TDKH.COL_DanhMucCongTac] = fstDA.TenDuAn;

                    newRowDA[TDKH.COL_GTSXTrongKy] = fstDA.TTTCInDate;
                    newRowDA[TDKH.COL_GTSXFromBeginOfPrj] = fstDA.TTTCFromBeginOfPrj;
                    newRowDA[TDKH.COL_GTSXTyLe] = $"{MyConstant.PrefixFormula}IF({dic[TDKH.COL_GiaTriHopDong]}{crIndRowCha + 1} = 0; \"-\"; {dic[TDKH.COL_GTSXFromBeginOfPrj]}{crIndRowCha + 1}/{dic[TDKH.COL_GiaTriHopDong]}{crIndRowCha + 1})";


                    newRowDA[TDKH.COL_GTNTTrongKy] = fstDA.GTNTInDate;
                    newRowDA[TDKH.COL_GTNTFromBeginOfPrj] = fstDA.GTNTFromBeginOfPrj;
                    newRowDA[TDKH.COL_GTDangDo] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_GTSXFromBeginOfPrj]}{crIndRowCha + 1} - {dic[TDKH.COL_GTNTFromBeginOfPrj]}{crIndRowCha + 1}";
                }

                int crIndRow = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                foreach (var col in colsSum)
                {
                    if (dtData.Columns.Contains(col))
                        newRowOwner[col] = $"{MyConstant.PrefixFormula}SUM({dic[col]}{crIndRowOwner + 2}:{dic[col]}{crIndRow + 1})";


                }

            }

            var newRow = dtData.NewRow();
            dtData.Rows.InsertAt(newRow, 0);
            newRow[TDKH.COL_DanhMucCongTac] = "Tổng";

            foreach (var col in colsSum)
            {
                if (lsSum.Any())
                {
                    newRow[col] = $"{MyConstant.PrefixFormula}SUM({string.Join(";", lsSum.Select(x => $"{dic[col]}{x + 1}"))})";
                }
            }
            ws.Rows.Insert(dfnData.Range.BottomRowIndex, dtData.Rows.Count, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, dfnData.Range.TopRowIndex, 0);
            SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfnData.Range);
            SpreadsheetHelper.FormatRowsInRange(dfnData.Range, dic[TDKH.COL_TypeRow]);
            ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;
            WaitFormHelper.CloseWaitForm();
        }

        private async void LoadBaoCaoOnlineTongHopDuKienThiCong()
        {

            string[] colsSumPrivate =
            {
                    TDKH.COL_DBC_KhoiLuongToanBo,
                    TDKH.COL_KhoiLuongGiaoKhoan
                };
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);

            var wb = uc_BaoCao1.SpreadSheet.Document;
            try
            {
                wb.BeginUpdate();
                var ws = wb.Worksheets[0];

                var nbd = de_StartDateCrPeriod.DateTime.Date;
                var nkt = de_EndDateCrPeriod.DateTime.Date;

                var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
                var dfnData = ws.DefinedNames.Single(x => x.Name == nameData);

                string time = "";


                //if (cbe_CachLocKyTH.SelectedIndex == (int)PickDateType.TuDo)
                time = $"(Từ {nbd.ToShortDateString()} đến ngày {nkt.ToShortDateString()})";


                int year = nbd.Year;
                int month = nbd.Month;

                ws.Range[nameTime].SetValue(time);
                //ws.Range[nameTitle].SetValue($"KẾT QUẢ THỰC HIỆN CÁC DỰ ÁN NĂM {year}");

                //if (dfnData.Range.RowCount > 3)

                var nbdInMonth = new DateTime(year, month, 1);
                var nktInMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                var nbdInYear = new DateTime(year, 1, 1);
                var nKTInYear = nbdInYear.AddYears(1).AddDays(-1);

                var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<CongTacWithKLHN>(RouteAPI.TongDuAn_GetCongTacTDKHWithKLHN,
                    new KLHNRequest()
                    {
                        typeLayKhoiLuong = 2,
                        ignoreKLNhanThau = true,
                        dateCalcLuyKe = nbd,
                        dateBD = nbd,
                        dateKT = nkt
                    });

                if (!result.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError("Không thể lấy thông tin dự án từ server!");
                    uc_BaoCao1.PreviewSelectedIndex = 0;
                    return;
                }

                var cttks = result.Dto.Cttks;
                var klhns = result.Dto.KLHNBriefs;

                var grs = cttks.GroupBy(x => x.Owner);
                DataTable dtData = DataTableCreateHelper.BaoCaoTongHopDuKienThiCong();

                int STTOwner = 0;
                double sumKLTB = 0, sumKLGK = 0;
                List<int> indsOwner = new List<int>();


                foreach (var gr in grs)
                {
                    var fstR = gr.First();
                    ++STTOwner;

                    DataRow newRowOwner = dtData.NewRow();
                    dtData.Rows.Add(newRowOwner);

                    newRowOwner[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                    newRowOwner[TDKH.COL_DanhMucCongTac] = $"Dự án của {fstR.Owner}";
                    newRowOwner[TDKH.COL_STT] = STTOwner.ToRoman();
                    int crIndRowOwner = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                    indsOwner.Add(crIndRowOwner);

                    int sttDA = 0;
                    var grsDA = gr.GroupBy(x => x.CodeDuAn);

                    foreach (var grDA in grsDA)
                    {
                        ++sttDA;

                        var fstDA = grDA.First();

                        DataRow newRowDA = dtData.NewRow();
                        dtData.Rows.Add(newRowDA);
                        int crRowIndDA = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                        newRowDA[TDKH.COL_STT] = sttDA;
                        newRowDA[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                        newRowDA[TDKH.COL_DanhMucCongTac] = fstDA.TenDuAn;

                        var codesCTacGiaoThau = grDA.Where(x => x.CodeNhaThau != null).Select(x => x.Code);
                        var KLHNGiaoThauInDA = klhns.Where(x => codesCTacGiaoThau.Contains(x.ParentCode));



                        newRowDA[TDKH.COL_DBC_KhoiLuongToanBo] = KLHNGiaoThauInDA.Sum(x => x.TTKHInRange);

                        var grsNhanThau = grDA.Where(x => x.CodeNhaThau is null).GroupBy(x => new { x.CodeDVTH });



                        int isFirst = 0;
                        foreach (var grDVTH in grsNhanThau)
                        {
                            var fstDVTH = grDVTH.First();
                            if (isFirst > 1)
                            {
                                newRowDA = dtData.NewRow();
                                dtData.Rows.Add(newRowDA);


                                newRowDA[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;

                            }
                            else
                            {
                                isFirst++;
                            }
                            var codesCTacDVTH = grDVTH.Select(x => x.Code);

                            var KLHNsInDVTH = klhns.Where(x => codesCTacDVTH.Contains(x.ParentCode));
                            newRowDA[TDKH.COL_KhoiLuongGiaoKhoan] = KLHNsInDVTH.Sum(x => x.TTKHInRange);
                            newRowDA[TDKH.COL_NguoiDaiDien] = $"{fstDVTH.TenDVTH} ({fstDVTH.DaiDienDVTH})";

                            int crRowInd = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                            newRowDA[TDKH.COL_KhoiLuongConLai] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowIndDA + 1} - {dic[TDKH.COL_KhoiLuongGiaoKhoan]}{crRowInd + 1}";
                        }

                        if (isFirst > 1)
                        {
                            newRowDA[TDKH.COL_STT] = newRowDA[TDKH.COL_DanhMucCongTac]
                                = newRowDA[TDKH.COL_DBC_KhoiLuongToanBo] = $"{MyConstant.PrefixMerge}{crRowIndDA}";
                        }

                    }
                    int crIndRow = dfnData.Range.TopRowIndex + dtData.Rows.Count;

                    foreach (var col in colsSumPrivate)
                    {
                        newRowOwner[col] = $"{MyConstant.PrefixFormula}SUM({dic[col]}{crIndRowOwner + 2}:{dic[col]}{crIndRow + 1})";
                    }
                }
                ws.Rows.Insert(dfnData.Range.BottomRowIndex, dtData.Rows.Count, RowFormatMode.FormatAsPrevious);
                ws.Import(dtData, false, dfnData.Range.TopRowIndex + 1, 0);
                SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfnData.Range);
                SpreadsheetHelper.FormatRowsInRange(dfnData.Range, dic[TDKH.COL_TypeRow]);

                int fstInd = dfnData.Range.TopRowIndex;
                foreach (var col in colsSumPrivate)
                {
                    double val = 0;
                    foreach (int ind in indsOwner)
                    {
                        val += ws.Rows[ind][dic[col]].Value.NumericValue;
                    }


                    ws.Rows[fstInd][dic[col]].SetValue(val);
                }

            }
            catch (Exception ex)
            {
                //var err = CusLogHelper.GetLogStringFromException(ex);
                MessageShower.ShowError($"Lỗi tải file dự kiến thi công\r\n{ex.Message}");
                uc_BaoCao1.PreviewSelectedIndex = 0;


            }
            finally
            {
                wb.EndUpdate();
            }
        }

        private async void LoadBaoCaoOnlineTongHopGiaTriThucHien()
        {

            string[] colsSumPrivate =
            {
                    TDKH.COL_DBC_KhoiLuongToanBo,
                    TDKH.COL_GiaoKhoanBanDau,
                    TDKH.COL_GiaoKhoanBoSung,
                    TDKH.COL_GiaoKhoanCatGiam,
                    TDKH.COL_KhoiLuongConLai,
                };
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);

            var wb = uc_BaoCao1.SpreadSheet.Document;
            try
            {
                wb.BeginUpdate();
                var ws = wb.Worksheets[0];

                var nbd = de_StartDateCrPeriod.DateTime.Date;
                var nkt = de_EndDateCrPeriod.DateTime.Date;

                var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
                var dfnData = ws.DefinedNames.Single(x => x.Name == nameData);

                string time = "";


                //if (cbe_CachLocKyTH.SelectedIndex == (int)PickDateType.TuDo)
                time = $"(Từ {nbd.ToShortDateString()} đến ngày {nkt.ToShortDateString()})";


                int year = nbd.Year;
                int month = nbd.Month;

                ws.Range[nameTime].SetValue(time);
                //ws.Range[nameTitle].SetValue($"KẾT QUẢ THỰC HIỆN CÁC DỰ ÁN NĂM {year}");

                //if (dfnData.Range.RowCount > 3)

                var nbdInMonth = new DateTime(year, month, 1);
                var nktInMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                var nbdInYear = new DateTime(year, 1, 1);
                var nKTInYear = nbdInYear.AddYears(1).AddDays(-1);

                var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<CongTacWithKLHN>(RouteAPI.TongDuAn_GetCongTacTDKHWithKLHN,
                    new KLHNRequest()
                    {
                        typeLayKhoiLuong = 1,
                        ignoreKLNhanThau = true,
                        dateCalcLuyKe = nbd,
                        dateBD = nbd,
                        dateKT = nkt
                    });

                if (!result.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError("Không thể lấy thông tin dự án từ server!");
                    uc_BaoCao1.PreviewSelectedIndex = 0;
                    return;
                }

                var cttks = result.Dto.Cttks;
                var klhns = result.Dto.KLHNBriefs;

                var grs = cttks.GroupBy(x => x.Owner);
                DataTable dtData = DataTableCreateHelper.BaoCaoTongHopGiaTriThucHien();

                int STTOwner = 0;
                double sumKLTB = 0, sumKLGK = 0;
                List<int> indsOwner = new List<int>();


                foreach (var gr in grs)
                {
                    var fstR = gr.First();
                    ++STTOwner;

                    DataRow newRowOwner = dtData.NewRow();
                    dtData.Rows.Add(newRowOwner);

                    newRowOwner[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                    newRowOwner[TDKH.COL_DanhMucCongTac] = $"Dự án của {fstR.Owner}";
                    newRowOwner[TDKH.COL_STT] = STTOwner.ToRoman();
                    int crIndRowOwner = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                    indsOwner.Add(crIndRowOwner);

                    int sttDA = 0;
                    var grsDA = gr.GroupBy(x => x.CodeDuAn);

                    foreach (var grDA in grsDA)
                    {
                        ++sttDA;

                        var fstDA = grDA.First();

                        DataRow newRowDA = dtData.NewRow();
                        dtData.Rows.Add(newRowDA);
                        int crRowIndDA = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                        newRowDA[TDKH.COL_STT] = sttDA;
                        newRowDA[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                        newRowDA[TDKH.COL_DanhMucCongTac] = fstDA.TenDuAn;

                        //var codesCTacGiaoThau = grDA.Where(x => x.CodeNhaThau != null).Select(x => x.Code);
                        //var KLHNGiaoThauInDA = klhns.Where(x => codesCTacGiaoThau.Contains(x.Code));
                        //newRowDA[TDKH.COL_DBC_KhoiLuongToanBo] = KLHNGiaoThauInDA.Sum(x => x.TTKHInRange);

                        var grsNhanThau = grDA.Where(x => x.CodeNhaThau is null).GroupBy(x => new { x.CodeDVTH });



                        int isFirst = 0;
                        foreach (var grDVTH in grsNhanThau)
                        {
                            var fstDVTH = grDVTH.First();
                            if (isFirst > 1)
                            {
                                newRowDA = dtData.NewRow();
                                dtData.Rows.Add(newRowDA);


                                newRowDA[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;

                            }
                            else
                            {
                                isFirst++;// = false;
                            }
                            var codesCTacDVTH = grDVTH.Select(x => x.Code);

                            var KLHNsInDVTH = klhns.Where(x => codesCTacDVTH.Contains(x.ParentCode));

                            double KLKH = KLHNsInDVTH.Sum(x => x.TTKHInRange ?? 0);
                            double KLBS = KLHNsInDVTH.Sum(x => x.TTBSInRange ?? 0);
                            double KLTC = KLHNsInDVTH.Sum(x => x.TTTCInRange ?? 0);

                            newRowDA[TDKH.COL_NguoiDaiDien] = $"{fstDVTH.TenDVTH} ({fstDVTH.DaiDienDVTH})";
                            newRowDA[TDKH.COL_DBC_KhoiLuongToanBo] = KLKH;
                            newRowDA[TDKH.COL_GiaoKhoanBanDau] = KLKH - KLBS;
                            newRowDA[TDKH.COL_KhoiLuongThiCong] = KLTC;
                            if (KLBS > 0)
                                newRowDA[TDKH.COL_GiaoKhoanBoSung] = KLBS;
                            else if (KLBS < 0)
                                newRowDA[TDKH.COL_GiaoKhoanCatGiam] = -KLBS;

                            int crRowInd = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                            if (KLKH != 0)
                                newRowDA[TDKH.COL_PhanTramThucHien] = $"IF({dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} = 0;\"\";{MyConstant.PrefixFormula}{dic[TDKH.COL_KhoiLuongThiCong]}{crRowInd + 1}/{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1})";


                            newRowDA[TDKH.COL_KhoiLuongConLai] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_DBC_KhoiLuongToanBo]}{crRowInd + 1} - {dic[TDKH.COL_KhoiLuongThiCong]}{crRowInd + 1}";
                            newRowDA[TDKH.COL_KhoiLuongConLaiTong] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_GiaoKhoanBanDau]}{crRowInd + 1} - {dic[TDKH.COL_KhoiLuongThiCong]}{crRowInd + 1}";

                            if (KLKH <= KLTC)
                            {
                                newRowDA[TDKH.COL_Dat] = "ĐẠT";
                            }
                            else
                            {
                                newRowDA[TDKH.COL_KhongDat] = "KHÔNG ĐẠT";

                            }
                        }

                        if (isFirst > 1)
                        {
                            newRowDA[TDKH.COL_STT] = newRowDA[TDKH.COL_DanhMucCongTac]
                                 = $"{MyConstant.PrefixMerge}{crRowIndDA}";
                        }

                    }
                    int crIndRow = dfnData.Range.TopRowIndex + 1 + dtData.Rows.Count;

                    foreach (var col in colsSumPrivate)
                    {
                        newRowOwner[col] = $"{MyConstant.PrefixFormula}SUM({dic[col]}{crIndRowOwner + 2}:{dic[col]}{crIndRow + 1})";
                    }
                }
                ws.Rows.Insert(dfnData.Range.BottomRowIndex, dtData.Rows.Count, RowFormatMode.FormatAsPrevious);
                ws.Import(dtData, false, dfnData.Range.TopRowIndex + 1, 0);
                SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfnData.Range);
                SpreadsheetHelper.FormatRowsInRange(dfnData.Range, dic[TDKH.COL_TypeRow]);

                int fstInd = dfnData.Range.TopRowIndex;
                foreach (var col in colsSumPrivate)
                {
                    double val = 0;
                    foreach (int ind in indsOwner)
                    {
                        val += ws.Rows[ind][dic[col]].Value.NumericValue;
                    }


                    ws.Rows[fstInd][dic[col]].SetValue(val);
                }

            }
            catch (Exception ex)
            {
                //var err = CusLogHelper.GetLogStringFromException(ex);
                MessageShower.ShowError($"Lỗi tải file dự kiến thi công\r\n{ex.Message}");
                uc_BaoCao1.PreviewSelectedIndex = 0;


            }
            finally
            {
                wb.EndUpdate();
            }
        }
        private async void LoadBaoCaoHN()
        {
            if (slke_ThongTinDuAn.EditValue == null)
            {
                MessageShower.ShowError("Vui lòng chọn dự án!");
                uc_BaoCao1.PreviewSelectedIndex = 0;
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu hằng ngày!");
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);
            var wb = uc_BaoCao1.SpreadSheet.Document;
            wb.BeginUpdate();
            var ws = wb.Worksheets[0];
            var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            var dfnData = ws.DefinedNames.Single(x => x.Name == "Data"); ;
            DataTable dtData = DataTableCreateHelper.BangKeHoachHN();
            DevExpress.Spreadsheet.CellRange DuAn = ws.Range["DuAn"];
            DevExpress.Spreadsheet.CellRange Date = ws.Range["Date"];
            DevExpress.Spreadsheet.CellRange SANG = ws.Range["SANG"];
            DevExpress.Spreadsheet.CellRange CHIEU = ws.Range["CHIEU"];
            DevExpress.Spreadsheet.CellRange TOI = ws.Range["TOI"];
            DevExpress.Spreadsheet.CellRange TenCty = ws.Range["TenCongTy"];
            DevExpress.Spreadsheet.CellRange TenCty1 = ws.Range["TenCongTy1"];
            DevExpress.Spreadsheet.CellRange DataMau = wb.Worksheets[1].Range["DataMau"];
            var nbdKyNay = de_StartDateCrPeriod.DateTime.Date;
            var nktKyNay = de_EndDateCrPeriod.DateTime.Date;
            string dbString = $"SELECT * FROM {Server.Tbl_ThoiTiet} tt " +
           $"WHERE tt.CodeDuAn = '{slke_ThongTinDuAn.EditValue}' " +
           $"AND tt.Ngay = '{nktKyNay.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";

            List<Tbl_ThoiTietViewModel> tt = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThoiTietViewModel>(dbString);
            ws.Rows[DuAn.TopRowIndex][DuAn.LeftColumnIndex].SetValueFromText($"DỰ ÁN THÀNH PHẦN: {slke_ThongTinDuAn.Text}".ToUpper());
            if (nbdKyNay == nktKyNay)
                ws.Rows[Date.TopRowIndex][Date.LeftColumnIndex].SetValueFromText($"NGÀY: {nbdKyNay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}".ToUpper());
            else
                ws.Rows[Date.TopRowIndex][Date.LeftColumnIndex].SetValueFromText($"TỪ: {nbdKyNay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} ĐẾN {nktKyNay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)}".ToUpper());
            if (tt.Any())
            {
                ws.Rows[SANG.TopRowIndex][SANG.LeftColumnIndex].SetValueFromText($"Sáng: {tt.FirstOrDefault().Sang}");
                ws.Rows[CHIEU.TopRowIndex][CHIEU.LeftColumnIndex].SetValueFromText($"Chiều: {tt.FirstOrDefault().Chieu}");
                ws.Rows[TOI.TopRowIndex][TOI.LeftColumnIndex].SetValueFromText($"Tối: {tt.FirstOrDefault().Toi}");
            }
            List<string> TT = new List<string>();
            TT=cbe_TrangThai.Properties.Items.Where(x => x.CheckState == CheckState.Checked).Any()?
                cbe_TrangThai.Properties.Items.Where(x => x.CheckState == CheckState.Checked).Select(x=>x.Description).ToList():new List<string>();
            //foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in cbe_TrangThai.Properties.Items)
            //{
            //    if (item.CheckState == CheckState.Checked)
            //        TT.Add(item.Description);
            //}
            dbString = $"SELECT \"Ten\" FROM {MyConstant.TBL_THONGTINNHATHAU} WHERE \"CodeDuAn\"='{slke_ThongTinDuAn.EditValue}' ";
            DataTable dtnhathauchinh = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            TenCty.SetValueFromText($"{dtnhathauchinh.Rows[0][0]}".ToUpper());
            TenCty1.SetValueFromText($"{dtnhathauchinh.Rows[0][0]}".ToUpper());
            dbString = $"SELECT COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi," +
           $"ncttp.Code as CodeNhomTP,ncttd.Code as CodeNhomTD," +
           $"hm.TenPhanTuyen as TenTuyen,hm.CodePhanTuyen as CodePhanTuyen," +
           $" hm.Code as CodeHangMuc,hm.Ten as TenHangMuc,ctrinh.Code as CodeCongTrinh,ctrinh.Ten as TenCongTrinh," +
           $"cttktp.Code as CodeTP," +
           $"cttktd.Code as CodeTD, " +
           $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
           $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
           $"cttktp.KhoiLuongHopDongChiTiet as KhoiLuongTP," +
           $"cttktp.DonGia as DonGiaTP,cttktp.DonGiaThiCong as DonGiaThiCongTP," +
           $"cttktd.KhoiLuongHopDongChiTiet as KhoiLuongTD," +
           $"cttktd.DonGia as DonGiaTD,cttktd.DonGiaThiCong as DonGiaThiCongTD," +
           $"cttktp.CodeNhaThauPhu as TP,cttktd.CodeToDoi as TD, \r\n" +
           $"hm.CodeCongTrinh, \r\n" +
           $"ctrinh.CodeDuAn, \r\n" +
           $"nt.Ten AS TenNhaThau, \r\n" +
           $"tdtc.Ten AS TenTD, \r\n" +
           $"ntp.Ten AS TenTP," +
           $"cttk.* \r\n" +
           $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
           $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
           $"ON cttk.CodeCongTac = dmct.Code \r\n" +
           $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttktp\r\n" +
           $"ON cttk.CodeCongTac = cttktp.CodeCongTac AND cttktp.CodeNhaThauPhu IS NOT NULL\r\n" +
           $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttktd\r\n" +
           $"ON cttk.CodeCongTac = cttktd.CodeCongTac AND cttktd.CodeToDoi IS NOT NULL\r\n" +
           $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
           $"ON cttk.CodeNhom = nct.Code \r\n" +
           $"LEFT JOIN {TDKH.TBL_NhomCongTac} ncttp\r\n" +
           $"ON cttktp.CodeNhom = ncttp.Code \r\n" +
           $"LEFT JOIN {TDKH.TBL_NhomCongTac} ncttd\r\n" +
           $"ON cttktd.CodeNhom = ncttd.Code \r\n" +
           $"INNER JOIN {MyConstant.view_HangMucWithPhanTuyen} hm\r\n" +
           $"ON (hm.Code = dmct.CodeHangMuc AND ((dmct.CodePhanTuyen IS NOT NULL AND hm.CodePhanTuyen = dmct.CodePhanTuyen) " +
           $"OR (dmct.CodePhanTuyen IS NULL AND hm.CodePhanTuyen IS NULL)))\r\n" +
           $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
           $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
           $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
           $"ON ctrinh.CodeDuAn = da.Code \r\n" +
           $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt\r\n" +
           $"ON cttk.CodeNhaThau = nt.Code \r\n" +
           $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
           $"ON cttktd.CodeToDoi = tdtc.Code \r\n" +
           $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp\r\n" +
           $"ON cttktp.CodeNhaThauPhu = ntp.Code \r\n" +
           $"WHERE da.Code = '{slke_ThongTinDuAn.EditValue}' \r\n" +
           $"AND cttk.CodeNhaThau IS NOT NULL " +
           (TT.Any()?$"AND cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(TT.ToArray())})" :"")+
           $"ORDER BY ctrinh.SortId ASC, hm.SortId ASC, cttk.SortId ASC\r\n";
            Dictionary<int, string> dicNhaThau = new Dictionary<int, string>();
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            var codesNhomTD = dtCongTacTheoKy.AsEnumerable().Where(x => x["CodeNhomTD"] != DBNull.Value)
                              .Select(x => x["CodeNhomTD"].ToString()).Distinct();
            var codesNhomTP = dtCongTacTheoKy.AsEnumerable().Where(x => x["CodeNhomTP"] != DBNull.Value)
                              .Select(x => x["CodeNhomTP"].ToString()).Distinct();
            var lstCodeNhom = codesNhomTD.Concat(codesNhomTP);
            dbString = $"SELECT NHOM.* " +
                $" FROM {TDKH.TBL_NhomCongTac} NHOM " +
                $" WHERE NHOM.Code IN ({MyFunction.fcn_Array2listQueryCondition(lstCodeNhom)}) ";
            DataTable dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dtCongTacTheoKy.AddIndPhanTuyenNhom();
            //NBD
            //var nbdKyTruoc = de_StartDatePrevPeriod.DateTime.Date;
            //nkt
            //var nktKyTruoc = de_EndDatePrevPeriod.DateTime.Date;
            string[] lstGr = new string[] { "TP", "TD" };
            bool SumNhom = false;
            string prefixFormula = MyConstant.PrefixFormula;
            //List<KLHN> rowKLHNAllNhom = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, lstCodeNhom, dateBD: nbdKyNay.Date, dateKT: nktKyNay.Date).Where(x => x.KhoiLuongThiCong.HasValue).ToList();
            List<KLHNBriefViewModel> rowKLHNAllNhom = MyFunction.CalcKLHNBrief(TypeKLHN.Nhom, lstCodeNhom, dateBD: nbdKyNay.Date, dateKT: nktKyNay.Date);
            List<KLHNBriefViewModel> rowKLHNAll = new List<KLHNBriefViewModel>();
            if (!ce_Nhom.Checked)
            {
                var codesTD = dtCongTacTheoKy.AsEnumerable().Where(x => x["CodeTD"] != DBNull.Value)
                            .Select(x => x["CodeTD"].ToString()).Distinct();
                var codesTP = dtCongTacTheoKy.AsEnumerable().Where(x => x["CodeTP"] != DBNull.Value)
                                  .Select(x => x["CodeTP"].ToString()).Distinct();
                var lstCode = codesTD.Concat(codesTP);
                //rowKLHNAll = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lstCode, dateBD: nbdKyNay.Date, dateKT: nktKyNay.Date).Where(x => x.KhoiLuongThiCong.HasValue).ToList();
                rowKLHNAll = MyFunction.CalcKLHNBrief(TypeKLHN.CongTac, lstCode, dateBD: nbdKyNay.Date, dateKT: nktKyNay.Date);
            }
            var crRowInd = dfnData.Range.TopRowIndex;
            int fstInd = dfnData.Range.TopRowIndex;
            int fstNhaThau = dfnData.Range.TopRowIndex;
            string forGiaTri = string.Empty;
            string forSum = string.Empty;

            List<int> RowCtrinh = new List<int>();
            string[] colsSum = new string[]
{
                TDKH.COL_GiaTriHD,
                TDKH.COL_GiaTriLKKT,
                TDKH.COL_GiaTriThucHien,
                TDKH.COL_GiaTriLKKN,
                TDKH.COL_GiaTriConLai
};
            int STTNt = 0;
            foreach (var item in lstGr)
            {
                var grnt = dtCongTacTheoKy.AsEnumerable().Where(x => x[item] != DBNull.Value).GroupBy(x => x[item].ToString());
                foreach (var nt in grnt)
                {
                    var fstDVTH = nt.FirstOrDefault();
                    DataRow newRowDVTH = dtData.NewRow();
                    dtData.Rows.Add(newRowDVTH);
                    newRowDVTH[TDKH.COL_STT] = ++STTNt;
                    newRowDVTH[TDKH.COL_DanhMucCongTac] = fstDVTH[$"Ten{item}"].ToString().ToUpper();
                    newRowDVTH[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVTong;
                    fstNhaThau = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                    newRowDVTH[TDKH.COL_TyLe] = $"{prefixFormula}IF({dic[TDKH.COL_GiaTriHD]}{fstNhaThau }=0;0;{dic[TDKH.COL_GiaTriLKKN]}{fstNhaThau }/{dic[TDKH.COL_GiaTriHD]}{fstNhaThau })";
                    var grCongTrinh = nt.GroupBy(x => x["CodeCongTrinh"]);
                    int STTCTrinh = 0;
                    RowCtrinh.Clear();
                    forSum = string.Empty;
                    dicNhaThau.Add(fstNhaThau, fstDVTH[$"Ten{item}"].ToString().ToUpper());
                    foreach (var ctrinh in grCongTrinh)
                    {
                        DataRow newRowCtrinh = dtData.NewRow();
                        dtData.Rows.Add(newRowCtrinh);
                        var fstCtr = ctrinh.First();
                        newRowCtrinh[TDKH.COL_STT] = $"{STTNt}.{++STTCTrinh}";
                        forSum += $"+{newRowCtrinh[TDKH.COL_STT]}";
                        newRowCtrinh[TDKH.COL_DanhMucCongTac] = $"CTR: {fstCtr["TenCongTrinh"]}";
                        newRowCtrinh[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                        newRowCtrinh[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{fstNhaThau})";
                        var crRowCtrInd = crRowInd = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                        RowCtrinh.Add(crRowCtrInd);
                        newRowCtrinh[TDKH.COL_TyLe] = $"{prefixFormula}IF({dic[TDKH.COL_GiaTriHD]}{crRowCtrInd }=0;0;{dic[TDKH.COL_GiaTriLKKN]}{crRowCtrInd }/{dic[TDKH.COL_GiaTriHD]}{crRowCtrInd })";
                        var grsHM = ctrinh.GroupBy(x => x["CodeHangMuc"]);
                        int STTHM = 0;
                        foreach (var grHM in grsHM)
                        {
                            DataRow newRowHM = dtData.NewRow();
                            dtData.Rows.Add(newRowHM);

                            var fstHM = grHM.First();
                            newRowHM[TDKH.COL_STT] = $"{STTNt}.{STTCTrinh}.{++STTHM}";
                            newRowHM[TDKH.COL_DanhMucCongTac] = $"HM: {fstHM["TenHangMuc"]}";
                            newRowHM[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HangMuc;
                            newRowHM[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowCtrInd})";
                            var crRowHMInd = crRowInd = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                            newRowHM[TDKH.COL_TyLe] = $"{prefixFormula}IF({dic[TDKH.COL_GiaTriHD]}{crRowHMInd }=0;0;{dic[TDKH.COL_GiaTriLKKN]}{crRowHMInd }/{dic[TDKH.COL_GiaTriHD]}{crRowHMInd })";
                            var grsPhanTuyen = grHM.GroupBy(x => (int)x["IndPT"])
                                           .OrderBy(x => x.Key);
                            foreach (var grPhanTuyen in grsPhanTuyen)
                            {
                                var fstTuyen = grPhanTuyen.First();
                                string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : $"{fstTuyen["CodePhanTuyen"]}";
                                int? crRowPTInd = null;
                                string codePT = fstTuyen["CodePhanTuyen"].ToString();
                                DataRow newRowPT = null;
                                if (fstTuyen["CodePhanTuyen"] != DBNull.Value)
                                {
                                    newRowPT = dtData.NewRow();
                                    dtData.Rows.Add(newRowPT);
                                    crRowPTInd = crRowInd = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                                    newRowPT[TDKH.COL_DanhMucCongTac] = $"*T: {fstTuyen["TenTuyen"]}";
                                    newRowPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_PhanTuyen;
                                    newRowPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowHMInd})";
                                    newRowPT[TDKH.COL_TyLe] = $"{prefixFormula}IF({dic[TDKH.COL_GiaTriHD]}{crRowPTInd }=0;0;{dic[TDKH.COL_GiaTriLKKN]}{crRowPTInd }/{dic[TDKH.COL_GiaTriHD]}{crRowPTInd })";
                                }

                                var grTuyenNhom = grPhanTuyen.GroupBy(x => (int)x["IndNhom"]).OrderBy(x => x.Key);
                                int STTNhom = 0;
                                foreach (var NhomTuyen in grTuyenNhom)
                                {
                                    var fstNhom = NhomTuyen.First();
                                    string crCodeNhom = (fstNhom["CodeNhom"] == DBNull.Value) ? null : $"{fstNhom["CodeNhom"]}";
                                    SumNhom = false;
                                    DataRow drNhom = null;
                                    DataRow newRowNhom = null;
                                    int? crRowNhomInd = null;
                                    bool IsNhom = false;
                                    if (fstNhom["CodeNhom"] != DBNull.Value && fstNhom[$"CodeNhom{item}"] != DBNull.Value)
                                    {
                                        IsNhom = true;
                                        drNhom = dtNhom.AsEnumerable().Single(x => x["Code"].ToString() == fstNhom[$"CodeNhom{item}"].ToString());
                                        newRowNhom = dtData.NewRow();
                                        dtData.Rows.Add(newRowNhom);
                                        newRowNhom[TDKH.COL_STT] = $"{STTNt}.{STTCTrinh}.{STTHM}.{++STTNhom}";
                                        newRowNhom[TDKH.COL_DanhMucCongTac] = $"*:{ drNhom["Ten"]}";
                                        crRowNhomInd = crRowInd = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                                        newRowNhom[TDKH.COL_Code] = drNhom["Code"];
                                        newRowNhom[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd)})";
                                        newRowNhom[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                                        if (drNhom["KhoiLuongKeHoach"] != DBNull.Value)
                                        {
                                            SumNhom = true;
                                            newRowNhom[TDKH.COL_DonVi] = drNhom["DonVi"];
                                            newRowNhom[TDKH.COL_KhoiLuongHopDongChiTiet] = drNhom["KhoiLuongHopDongChiTiet"];
                                            if (gr_DonGia.SelectedIndex == 0)
                                                newRowNhom[TDKH.COL_DonGia] = drNhom["DonGiaThiCong"];
                                            else
                                                newRowNhom[TDKH.COL_DonGia] = drNhom["DonGia"];
                                            newRowNhom[TDKH.COL_Code] = drNhom["Code"];
                                            newRowNhom[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowPTInd ?? crRowHMInd)})";
                                            newRowNhom[TDKH.COL_TypeRow] = MyConstant.TYPEROW_Nhom;
                                            if (rowKLHNAllNhom.Any())
                                            {
                                                //List<KLHN> THNhom = rowKLHNAllNhom.Where(x => x.ParentCode == drNhom["Code"].ToString()).ToList();
                                                //if (THNhom.Any())
                                                //{
                                                //    newRowNhom[TDKH.COL_ThucHienKyNay] = THNhom.Sum(x => x.KhoiLuongThiCong);
                                                //    KLHN LKNhom = THNhom.Where(x => x.Ngay == nbdKyNay.Date).SingleOrDefault();
                                                //    newRowNhom[TDKH.COL_LuyKeKyTruoc] = (LKNhom?.LuyKeKhoiLuongThiCong - LKNhom?.KhoiLuongThiCong) ?? 0;
                                                //}     
                                                KLHNBriefViewModel THNhom = rowKLHNAllNhom.Where(x => x.ParentCode == drNhom["Code"].ToString()).FirstOrDefault();
                                                newRowNhom[TDKH.COL_ThucHienKyNay] = THNhom.KLTCInRange;
                                                newRowNhom[TDKH.COL_LuyKeKyTruoc] = THNhom.LuyKeKLTCKyTruoc;
                                            }
                                            newRowNhom[TDKH.COL_LuyKeKyNay] = $"{prefixFormula}{dic[TDKH.COL_LuyKeKyTruoc]}{crRowInd}+{dic[TDKH.COL_ThucHienKyNay]}{crRowInd}";
                                            newRowNhom[TDKH.COL_ConLai] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd }-{dic[TDKH.COL_LuyKeKyNay]}{crRowInd }";
                                            newRowNhom[TDKH.COL_GiaTriHD] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd }*{dic[TDKH.COL_DonGia]}{crRowInd}";
                                            newRowNhom[TDKH.COL_GiaTriLKKT] = $"{prefixFormula}{dic[TDKH.COL_LuyKeKyTruoc]}{crRowInd }*{dic[TDKH.COL_DonGia]}{crRowInd}";
                                            newRowNhom[TDKH.COL_GiaTriThucHien] = $"{prefixFormula}{dic[TDKH.COL_ThucHienKyNay]}{crRowInd}*{dic[TDKH.COL_DonGia]}{crRowInd }";
                                            newRowNhom[TDKH.COL_GiaTriLKKN] = $"{prefixFormula}{dic[TDKH.COL_LuyKeKyNay]}{crRowInd }*{dic[TDKH.COL_DonGia]}{crRowInd }";
                                            newRowNhom[TDKH.COL_GiaTriConLai] = $"{prefixFormula}{dic[TDKH.COL_ConLai]}{crRowInd }*{dic[TDKH.COL_DonGia]}{crRowInd }";
                                            newRowNhom[TDKH.COL_TyLe] = $"{prefixFormula}IF({dic[TDKH.COL_GiaTriHD]}{crRowInd }=0;0;{dic[TDKH.COL_GiaTriLKKN]}{crRowInd }/{dic[TDKH.COL_GiaTriHD]}{crRowInd })";
                                        }
                                    }
                                    if (!ce_Nhom.Checked)
                                    {
                                        var grCongTac = NhomTuyen.Where(x => x[$"Code{item}"] != DBNull.Value).GroupBy(x => x[$"Code{item}"].ToString());
                                        int STTCTac = 0;
                                        foreach (var ctac in grCongTac)
                                        {
                                            var fstCtac = ctac.FirstOrDefault();
                                            DataRow newRowCTac = dtData.NewRow();
                                            dtData.Rows.Add(newRowCTac);
                                            var crrowIndCt = crRowInd = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                                            newRowCTac[TDKH.COL_STT] = ++STTCTac;
                                            newRowCTac[TDKH.COL_DanhMucCongTac] = fstCtac["TenCongTac"];
                                            newRowCTac[TDKH.COL_DonVi] = fstCtac["DonVi"];
                                            newRowCTac[TDKH.COL_KhoiLuongHopDongChiTiet] = fstCtac[$"KhoiLuong{item}"];
                                            if (gr_DonGia.SelectedIndex == 0)
                                                newRowCTac[TDKH.COL_DonGia] = fstCtac[$"DonGiaThiCong{item}"];
                                            else
                                                newRowCTac[TDKH.COL_DonGia] = fstCtac[$"DonGia{item}"];
                                            newRowCTac[TDKH.COL_Code] = ctac.Key;
                                            newRowCTac[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{(crRowNhomInd ?? crRowPTInd ?? crRowHMInd)})";
                                            newRowCTac[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                                            if (rowKLHNAll.Any())
                                            {
                                                //List<KLHNBriefViewModel> THCtac = rowKLHNAll.Where(x => x.ParentCode == ctac.Key).ToList();
                                                //if (THCtac.Any())
                                                //{
                                                //    newRowCTac[TDKH.COL_ThucHienKyNay] = THCtac.Sum(x => x.KhoiLuongThiCong);
                                                //    KLHN LKCtac = THCtac.Where(x => x.Ngay == nbdKyNay.Date).SingleOrDefault();
                                                //    newRowCTac[TDKH.COL_LuyKeKyTruoc] = (LKCtac?.LuyKeKhoiLuongThiCong - LKCtac?.KhoiLuongThiCong) ?? 0;
                                                //}                
                                                KLHNBriefViewModel THCtac = rowKLHNAll.Where(x => x.ParentCode == ctac.Key).FirstOrDefault();
                                                newRowCTac[TDKH.COL_ThucHienKyNay] = THCtac.KLTCInRange;
                                                //KLHN LKCtac = THCtac.Where(x => x.Ngay == nbdKyNay.Date).SingleOrDefault();
                                                newRowCTac[TDKH.COL_LuyKeKyTruoc] = THCtac.LuyKeKLTCKyTruoc;
                                            }
                                            newRowCTac[TDKH.COL_LuyKeKyNay] = $"{prefixFormula}{dic[TDKH.COL_LuyKeKyTruoc]}{crRowInd}+{dic[TDKH.COL_ThucHienKyNay]}{crRowInd}";
                                            newRowCTac[TDKH.COL_ConLai] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd }-{dic[TDKH.COL_LuyKeKyNay]}{crRowInd }";
                                            newRowCTac[TDKH.COL_GiaTriHD] = $"{prefixFormula}{dic[TDKH.COL_KhoiLuongHopDongChiTiet]}{crRowInd }*{dic[TDKH.COL_DonGia]}{crRowInd}";
                                            newRowCTac[TDKH.COL_GiaTriLKKT] = $"{prefixFormula}{dic[TDKH.COL_LuyKeKyTruoc]}{crRowInd }*{dic[TDKH.COL_DonGia]}{crRowInd}";
                                            newRowCTac[TDKH.COL_GiaTriThucHien] = $"{prefixFormula}{dic[TDKH.COL_ThucHienKyNay]}{crRowInd}*{dic[TDKH.COL_DonGia]}{crRowInd }";
                                            newRowCTac[TDKH.COL_GiaTriLKKN] = $"{prefixFormula}{dic[TDKH.COL_LuyKeKyNay]}{crRowInd }*{dic[TDKH.COL_DonGia]}{crRowInd }";
                                            newRowCTac[TDKH.COL_GiaTriConLai] = $"{prefixFormula}{dic[TDKH.COL_ConLai]}{crRowInd }*{dic[TDKH.COL_DonGia]}{crRowInd }";
                                            newRowCTac[TDKH.COL_TyLe] = $"{prefixFormula}IF({dic[TDKH.COL_GiaTriHD]}{crRowInd }=0;0;{dic[TDKH.COL_GiaTriLKKN]}{crRowInd }/{dic[TDKH.COL_GiaTriHD]}{crRowInd })";
                                        }
                                    }
                                    if (!SumNhom && newRowNhom != null)
                                    {
                                        foreach (string col in colsSum)
                                        {
                                            forGiaTri = TDKHHelper.GetFormulaSumChild((crRowNhomInd ?? 0), crRowInd, dic[col], dic[TDKH.COL_RowCha]);
                                            newRowNhom[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                        }
                                    }
                                }
                                if (fstTuyen["CodePhanTuyen"] != DBNull.Value)
                                {
                                    DataRow newRowHTPT = dtData.NewRow();
                                    dtData.Rows.Add(newRowHTPT);


                                    //newRowPT[TDKH.COL_STT] = $"{STTCTrinh}.{STTHM}.{STT}";
                                    newRowHTPT[TDKH.COL_Code] = codePT;
                                    newRowHTPT[TDKH.COL_DanhMucCongTac] = $"{prefixFormula}\"HT \" & {dic[TDKH.COL_DanhMucCongTac]}{crRowPTInd}";
                                    newRowHTPT[TDKH.COL_TypeRow] = MyConstant.TYPEROW_HTPhanTuyen;
                                    newRowHTPT[TDKH.COL_RowCha] = $"{prefixFormula}ROW(A{crRowPTInd})";

                                    crRowInd = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                                    foreach (string col in colsSum)
                                    {
                                        forGiaTri = TDKHHelper.GetFormulaSumChild((crRowPTInd ?? 0), crRowInd - 1, dic[col], dic[TDKH.COL_RowCha]);
                                        newRowPT[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                                    }
                                }

                            }
                            crRowInd = fstInd + dtData.Rows.Count;
                            foreach (string col in colsSum)
                            {
                                forGiaTri = TDKHHelper.GetFormulaSumChild(crRowHMInd, crRowInd - 1, dic[col], dic[TDKH.COL_RowCha]);
                                newRowHM[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                            }

                        }
                        crRowInd = fstInd + dtData.Rows.Count;
                        foreach (string col in colsSum)
                        {
                            forGiaTri = TDKHHelper.GetFormulaSumChild(crRowCtrInd, crRowInd - 1, dic[col], dic[TDKH.COL_RowCha]);
                            newRowCtrinh[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        }
                    }
                    crRowInd = fstInd + dtData.Rows.Count;
                    foreach (string col in colsSum)
                    {
                        forGiaTri = TDKHHelper.GetFormulaSumChild(fstNhaThau, crRowInd - 1, dic[col], dic[TDKH.COL_RowCha]);
                        newRowDVTH[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                    }
                    var rowTong = dtData.NewRow();
                    dtData.Rows.Add(rowTong);
                    rowTong[TDKH.COL_DanhMucCongTac] = $"Các hạng mục ({forSum.Remove(0, 1)}) ";
                    rowTong[TDKH.COL_STT] = "A";
                    rowTong[TDKH.COL_TypeRow] = MyConstant.TYPEROW_SUMTT;

                    var rowChiPhi = dtData.NewRow();
                    dtData.Rows.Add(rowChiPhi);
                    rowChiPhi[TDKH.COL_DanhMucCongTac] = $"Chi phí dự phòng cho khối lượng phát sinh và chi phí dự phòng trượt giá (%): {se_ChiPhi.Value}%) ";
                    rowChiPhi[TDKH.COL_STT] = "B";
                    rowChiPhi[TDKH.COL_TypeRow] = MyConstant.TYPEROW_SUMTT;
                    rowChiPhi[TDKH.COL_GiaTriHD] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_GiaTriHD]}{fstInd + dtData.Rows.Count - 1}*{se_ChiPhi.Value}%";
                    rowChiPhi[TDKH.COL_GiaTriConLai] = $"{MyConstant.PrefixFormula}{dic[TDKH.COL_GiaTriHD]}{fstInd + dtData.Rows.Count}";

                    var rowGiamGia = dtData.NewRow();
                    dtData.Rows.Add(rowGiamGia);
                    rowGiamGia[TDKH.COL_DanhMucCongTac] = $"Giảm giá (C)=((A)+(B))*{se_GiamGia.Value}%";
                    rowGiamGia[TDKH.COL_STT] = "C";
                    rowGiamGia[TDKH.COL_TypeRow] = MyConstant.TYPEROW_SUMTT;

                    var LamTron = dtData.NewRow();
                    dtData.Rows.Add(LamTron);
                    LamTron[TDKH.COL_DanhMucCongTac] = $"GIÁ HỢP ĐỒNG (D)=(A)+(B)-(C) (làm tròn)";
                    LamTron[TDKH.COL_STT] = "D";
                    LamTron[TDKH.COL_TypeRow] = MyConstant.TYPEROW_SUMTT;

                    foreach (string col in colsSum)
                    {
                        forGiaTri = string.Empty;
                        RowCtrinh.ForEach(x => forGiaTri += $"+{dic[col]}{x}");
                        rowTong[col] = $"{MyConstant.PrefixFormula}{forGiaTri}";
                        rowGiamGia[col] = $"{MyConstant.PrefixFormula}({dic[col]}{fstInd + dtData.Rows.Count - 2}+{dic[col]}{fstInd + dtData.Rows.Count - 3})*{se_GiamGia.Value}%";
                        LamTron[col] = $"{MyConstant.PrefixFormula}ROUND({dic[col]}{fstInd + dtData.Rows.Count - 2}+{dic[col]}{fstInd + dtData.Rows.Count - 3}-{dic[col]}{fstInd + dtData.Rows.Count - 1};3)";
                    }
                }
            }
            int numRow = dtData.Rows.Count;
            ws.Rows.Insert(dfnData.Range.BottomRowIndex, numRow + 1, RowFormatMode.FormatAsNext);
            ws.Import(dtData, false, dfnData.Range.TopRowIndex, 0);
            //ws.Columns[dic[TDKH.COL_DanhMucCongTac]].Alignment.WrapText = true;
            SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfnData.Range);
            SpreadsheetHelper.FormatRowsInRange(dfnData.Range, dic[TDKH.COL_TypeRow], dic[TDKH.COL_RowCha], dic[TDKH.COL_Code], Visible: ce_AnCongTrinhTuyen.Checked);
            wb.EndUpdate();
            if (dicNhaThau.Any())
            {
                wb.BeginUpdate();
                DevExpress.Spreadsheet.CellRange DataTong = ws.Range["DataTong"];
                int RowIn = DataTong.TopRowIndex;
                ws.Rows.Insert(DataTong.TopRowIndex + 2, dicNhaThau.Count(), RowFormatMode.FormatAsNext);
                int STT = 1;
                foreach (var item in dicNhaThau)
                {
                    Row Crow = ws.Rows[RowIn++];
                    Crow[dic[TDKH.COL_STT]].SetValue(STT++);
                    Crow[dic[TDKH.COL_DanhMucCongTac]].SetValueFromText(item.Value);
                    Crow[dic[TDKH.COL_DonVi]].Formula = $"={dic[TDKH.COL_GiaTriHD]}{item.Key + dicNhaThau.Count()}";
                    Crow[dic[TDKH.COL_LuyKeKyTruoc]].Formula = $"={dic[TDKH.COL_GiaTriLKKT]}{item.Key + dicNhaThau.Count()}";
                    Crow[dic[TDKH.COL_ThucHienKyNay]].Formula = $"={dic[TDKH.COL_GiaTriThucHien]}{item.Key + dicNhaThau.Count()}";
                    Crow[dic[TDKH.COL_LuyKeKyNay]].Formula = $"={dic[TDKH.COL_GiaTriLKKN]}{item.Key + dicNhaThau.Count()}";
                    Crow[dic[TDKH.COL_ConLai]].Formula = $"={dic[TDKH.COL_TyLe]}{item.Key + dicNhaThau.Count()}";
                }
                DataTong = ws.Range["DataTong"];
                ws.Rows.Remove(DataTong.BottomRowIndex - 2, 3);
                //DataTong = ws.Range["DataTong"];
                //DataTong.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);
                wb.EndUpdate();
            }
            //Thuyết minh
            wb.BeginUpdate();
            DevExpress.Spreadsheet.CellRange ThuyetMinh = ws.Range["ThuyetMinh"];
            dbString = $"SELECT TM.* FROM {MyConstant.TBL_THONGTINDUAN_ThuyetMinh} TM WHERE" +
                $" TM.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' AND (TM.Ngay" +
                $"='{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' OR TM.Ngay IS NULL) " +
            $"GROUP BY\r\n\tTM.Code ORDER BY TM.SortId ASC";
            List<ThuyetMinhDuAn> TM = DataProvider.InstanceTHDA.ExecuteQueryModel<ThuyetMinhDuAn>(dbString);
            string lstCodeHA = string.Concat(TM.Select(x => x.FileDinhKem).Distinct().ToArray());

            //dbString = $"SELECT " +
            //   $"TM.Code,TM.FileDinhKem as Ten FROM {MyConstant.TBL_THONGTINDUAN_FileDinhKem} TM" +
            //   $" WHERE TM.Code IN ({MyFunction.fcn_Array2listQueryCondition(lstCodeHA.Split(','))})";
            dbString = $"SELECT " +
   $"TM.Code,TM.FileDinhKem as Ten,TM.GhiChu FROM {MyConstant.TBL_THONGTINDUAN_FileDinhKem} TM" +
   $" LEFT JOIN {MyConstant.TBL_THONGTINDUAN} DA ON DA.Code=TM.CodeParent " +
   $" WHERE" +
   $" DA.Code='{SharedControls.slke_ThongTinDuAn.EditValue}' " +
   $"AND TM.FileDinhKem LIKE '%.jpg' OR TM.FileDinhKem LIKE '%.jpeg' OR TM.FileDinhKem LIKE '%.png' ";
            List<MayThiCongExtensionViewModel> HA = DataProvider.InstanceTHDA.ExecuteQueryModel<MayThiCongExtensionViewModel>(dbString);
            List<MayThiCongExtensionViewModel> HATM = HA.Where(x => lstCodeHA.Contains(x.Code)).ToList();

            List<ThuyetMinhDuAn> TMCon = TM.Where(x => !string.IsNullOrEmpty(x.CodeParent)).ToList();
            int RowIndexTM = ThuyetMinh.TopRowIndex;
            var grChinh = TM.Where(x => x.CodeParent is null).GroupBy(x => x.Code);
            int STTTM = 1;
            double SizeRowPre = 0;
            foreach(var itemchinh in grChinh)
            {
                Row Crow = ws.Rows[RowIndexTM++];
                Crow.Font.Bold = true;
                ws.Rows.Insert(RowIndexTM, 1, RowFormatMode.FormatAsNext);

                Crow["A"].SetValueFromText($"{STTTM++}");
                Crow["B"].SetValueFromText($"{itemchinh.FirstOrDefault().NoiDung}");
                SizeRowPre = Crow.Height;
                if (Crow.Height > SizeRowPre && Crow.Height < 1700)
                {
                    Crow.Height = Crow.Height / 3.6;
                }
                ws.Range[$"B{RowIndexTM}:P{RowIndexTM}"].Merge();
                //ws.Range[$"B{RowIndexTM}:P{RowIndexTM}"].Alignment.WrapText = true;
                ThuyetMinhDuAn New = TMCon.Where(x => x.CodeParent == itemchinh.Key).SingleOrDefault();
                Crow = ws.Rows[RowIndexTM++];
                Crow.Font.Bold = false;
                ws.Rows.Insert(RowIndexTM, 1, RowFormatMode.FormatAsNext);
                SizeRowPre = Crow.Height;
                Crow["B"].SetValueFromText($"{New.NoiDung}");
                if (Crow.Height > SizeRowPre&& Crow.Height < 1700)
                {
                    Crow.Height = Crow.Height / 3.6;
                }
                ws.Range[$"B{RowIndexTM}:P{RowIndexTM}"].Merge();
                //ws.Range[$"B{RowIndexTM}:P{RowIndexTM}"].Alignment.WrapText = true;
                if (HATM.Any())
                {
                    List<MayThiCongExtensionViewModel> HATMNew = HATM.Where(x => itemchinh.FirstOrDefault().FileDinhKem.Contains(x.Code)).Any()?
                        HATM.Where(x => itemchinh.FirstOrDefault().FileDinhKem.Contains(x.Code)).ToList():new List<MayThiCongExtensionViewModel>();
                    if (HATMNew.Any())
                    {
                        int soCot = 2;
                        int filesCount = HATMNew.Count();
                        int rowCount = (int)Math.Ceiling((double)filesCount / soCot);
                        ws.Rows.Insert(RowIndexTM, rowCount * DataMau.RowCount+ rowCount, RowFormatMode.FormatAsNext);
                        var drs = HATMNew.ToArray();
                        for (int ind = 1; ind <= filesCount; ind++)
                        {
                            MayThiCongExtensionViewModel dr = drs[ind - 1];
                            string file = Path.Combine(BaseFrom.m_FullTempathDA, "Resource", "Files", MyConstant.TBL_THONGTINDUAN_FileDinhKem,SharedControls.slke_ThongTinDuAn.EditValue.ToString(), dr.Code);
                            Image img = FileHelper.fcn_ImageStreamDoc(picEdit: null, file);
                            var src = SpreadsheetImageSource.FromImage(img);
                            int rowInd = (int)Math.Floor((double)ind / soCot);
                            int colInd = ind % soCot;
                            string fileName =ce_GhiChu.Checked&&ce_DinhKemTenAnh.Checked?
                                $"{dr.Ten.Substring(0, dr.Ten.LastIndexOf("."))}:{dr.GhiChu}":(ce_GhiChu.Checked?dr.GhiChu:
                                (ce_DinhKemTenAnh.Checked ? dr.Ten.Substring(0, dr.Ten.LastIndexOf(".")):string.Empty)) ;                            
                            if (colInd > 0)
                            {
                                Crow = ws.Rows[RowIndexTM++];
                                Crow.CopyFrom(DataMau, PasteSpecial.All);
                                var targetCell = ws.Range[$"B{RowIndexTM}:F{RowIndexTM + DataMau.RowCount - 2}"];
                                ws.Pictures.AddPicture(src, targetCell);
                                RowIndexTM = RowIndexTM + DataMau.RowCount;
                                ws.Rows[RowIndexTM - 2]["B"].SetValueFromText(fileName);
                                if (!ce_DinhKemTenAnh.Checked && !ce_GhiChu.Checked)
                                    ws.Rows[RowIndexTM - 2].Visible = false;
                            }
                            else
                            {
                                var targetCell = ws.Range[$"G{RowIndexTM - 2}:K{RowIndexTM - DataMau.RowCount}"];
                                ws.Pictures.AddPicture(src, targetCell);
                                ws.Rows[RowIndexTM - 2]["G"].SetValueFromText(fileName);
                                if (!ce_DinhKemTenAnh.Checked && !ce_GhiChu.Checked)
                                    ws.Rows[RowIndexTM - 2].Visible = false;
                            }
                        }
                    }
                }
            }
            if (ce_XuatFileDuAn.Checked)
            {
                if (HA.Count() != HATM.Count())
                {
                    List<MayThiCongExtensionViewModel> HADa = HA.Where(x => !lstCodeHA.Contains(x.Code)).ToList();
                    Row Crow = ws.Rows[RowIndexTM++];
                    Crow.Font.Bold = true;
                    ws.Rows.Insert(RowIndexTM, 1, RowFormatMode.FormatAsNext);
                    Crow["A"].SetValueFromText($"{STTTM}");
                    Crow["B"].SetValueFromText($"Hình ảnh Dự án");
                    int soCot = 2;
                    int filesCount = HADa.Count();
                    int rowCount = (int)Math.Ceiling((double)filesCount / soCot);
                    ws.Rows.Insert(RowIndexTM, rowCount * DataMau.RowCount+ rowCount, RowFormatMode.FormatAsNext);
                    var drs = HADa.ToArray();
                    for (int ind = 1; ind <= filesCount; ind++)
                    {
                        MayThiCongExtensionViewModel dr = drs[ind - 1];
                        string file = Path.Combine(BaseFrom.m_FullTempathDA, "Resource", "Files", MyConstant.TBL_THONGTINDUAN_FileDinhKem, SharedControls.slke_ThongTinDuAn.EditValue.ToString(), dr.Code);
                        Image img = FileHelper.fcn_ImageStreamDoc(picEdit: null, file);
                        var src = SpreadsheetImageSource.FromImage(img);
                        int rowInd = (int)Math.Floor((double)ind / soCot);
                        int colInd = ind % soCot;
                        string fileName = ce_GhiChu.Checked && ce_DinhKemTenAnh.Checked ?
                             $"{dr.Ten.Substring(0, dr.Ten.LastIndexOf("."))}:{dr.GhiChu}" : (ce_GhiChu.Checked ? dr.GhiChu :
                             (ce_DinhKemTenAnh.Checked ? dr.Ten.Substring(0, dr.Ten.LastIndexOf(".")) : string.Empty));
                        if (colInd > 0)
                        {
                            Crow = ws.Rows[RowIndexTM++];
                            Crow.CopyFrom(DataMau, PasteSpecial.All);
                            var targetCell = ws.Range[$"B{RowIndexTM}:F{RowIndexTM + DataMau.RowCount - 2}"];
                            ws.Pictures.AddPicture(src, targetCell);
                            RowIndexTM = RowIndexTM + DataMau.RowCount;
                            ws.Rows[RowIndexTM - 2]["B"].SetValueFromText(fileName);
                            if (!ce_DinhKemTenAnh.Checked && !ce_GhiChu.Checked)
                                ws.Rows[RowIndexTM - 2].Visible = false;
                        }
                        else
                        {
                            var targetCell = ws.Range[$"G{RowIndexTM - 2}:K{RowIndexTM - DataMau.RowCount}"];
                            ws.Pictures.AddPicture(src, targetCell);
                            ws.Rows[RowIndexTM - 2]["G"].SetValueFromText(fileName);
                            if (!ce_DinhKemTenAnh.Checked && !ce_GhiChu.Checked)
                                ws.Rows[RowIndexTM - 2].Visible = false;
                        }
                    }
                }
            }
            ThuyetMinh = ws.Range["ThuyetMinh"];
            ws.Rows.Remove(ThuyetMinh.BottomRowIndex - 1, 2);
            //ws.Rows[ThuyetMinh.BottomRowIndex].Visible = false;
            wb.EndUpdate();
            //
            WaitFormHelper.ShowWaitForm("Đang tải hình ảnh thi công!");
            //Hình ảnh
            dbString = $"SELECT \"Code\" FROM {TDKH.TBL_GiaiDoanThucHien} WHERE \"CodeDuAn\"='{slke_ThongTinDuAn.EditValue}'";
            DataTable dtgd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = rg_CachLayAnh.SelectedIndex == 0 ? $"SELECT file.FileDinhKem, file.Code, dvth.Code AS CodeDVTH, dvth.Ten, cttk.Code as CodeCongViecCha, dmct.TenCongTac as TenCongViec, cttk.LyTrinhCaoDo, " +
            $" ctrinh.Code AS CodeCongTrinh, ctrinh.Ten AS TenCongTrinh, hm.Code AS CodeHangMuc, hm.ten AS TenHangMuc " +
            $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
            $"JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
            $"ON cttk.CodeCongTac = dmct.Code " +
            $"JOIN {TDKH.TBL_ChiTietCongTacTheoKyFileDinhKem} file " +
            $"ON cttk.Code = file.CodeParent " +
            $"JOIN {MyConstant.view_DonViThucHien} dvth " +
            $"ON {string.Format(FormatString.CodeDonViNhanThau, "cttk")} = dvth.Code " +
            $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm " +
            $"ON dmct.CodeHangMuc = hm.Code " +
            $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh " +
            $"ON hm.CodeCongTrinh = ctrinh.Code " +
            $"WHERE (cttk.CodeGiaiDoan IS NULL OR cttk.CodeGiaiDoan = '{dtgd.Rows[0][0]}') " +
            $"AND (file.Ngay IS NULL " +
            $"OR (file.Ngay >= '{nbdKyNay.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND file.Ngay <= '{nktKyNay.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')) " +
            $"AND ctrinh.CodeDuAn = '{slke_ThongTinDuAn.EditValue}' " +
            $"AND (file.FileDinhKem LIKE '%.jpg' OR file.FileDinhKem LIKE '%.png' OR file.FileDinhKem LIKE '%.ico' OR file.FileDinhKem LIKE '%.jpeg')" :

            $"SELECT file.FileDinhKem, file.Code, dvth.Code AS CodeDVTH, dvth.Ten, cttk.Code as CodeCongViecCha, cttk.Ten as TenCongViec, NULL AS LyTrinhCaoDo , " +
            $" ctrinh.Code AS CodeCongTrinh, ctrinh.Ten AS TenCongTrinh, hm.Code AS CodeHangMuc, hm.ten AS TenHangMuc " +
            $"FROM {TDKH.TBL_NhomCongTac} cttk " +
            $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} dmct " +
            $"ON cttk.Code = dmct.CodeNhom " +
            $"JOIN {TDKH.TBL_NhomFileDinhKem} file " +
            $"ON cttk.Code = file.CodeParent " +
            $"JOIN {MyConstant.view_DonViThucHien} dvth " +
            $"ON {string.Format(FormatString.CodeDonViNhanThau, "dmct")} = dvth.Code " +
            $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm " +
            $"ON cttk.CodeHangMuc = hm.Code " +
            $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh " +
            $"ON hm.CodeCongTrinh = ctrinh.Code " +
            $"WHERE (cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(lstCodeNhom)}) ) " +
            $"AND (file.Ngay IS NULL " +
            $"OR (file.Ngay >= '{nbdKyNay.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND file.Ngay <= '{nktKyNay.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')) " +
            $"AND ctrinh.CodeDuAn = '{slke_ThongTinDuAn.EditValue}' " +
            $"AND (file.FileDinhKem LIKE '%.jpg' OR file.FileDinhKem LIKE '%.png' OR file.FileDinhKem LIKE '%.ico' OR file.FileDinhKem LIKE '%.jpeg') GROUP BY file.Code";
            DataTable dtFile = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string Tbl = rg_CachLayAnh.SelectedIndex == 0 ? TDKH.TBL_ChiTietCongTacTheoKyFileDinhKem : TDKH.TBL_NhomFileDinhKem;
            DevExpress.Spreadsheet.CellRange HinhAnh = ws.Range["HinhAnh"];

            int RowIndex = HinhAnh.TopRowIndex;
            var grsDvth = dtFile.AsEnumerable().GroupBy(x => x["CodeDVTH"].ToString());
            int STTDVTH = 0;
            wb.BeginUpdate();
            foreach (var grDvth in grsDvth)
            {
                Row Crow = ws.Rows[RowIndex++];
                Crow.Font.Bold = true;
                Crow.Font.Color = Color.Blue;
                ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
                Crow["A"].SetValueFromText($"{++STTDVTH}. {grDvth.First()["Ten"]}");
                var grsCongTrinh = grDvth.GroupBy(x => x["CodeCongTrinh"].ToString());
                int STTCongTrinh = 0;
                foreach (var grCongTrinh in grsCongTrinh)
                {
                    Crow = ws.Rows[RowIndex++];
                    Crow.Font.Bold = true;
                    Crow.Font.Color = MyConstant.color_Row_CongTrinh;
                    ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
                    Crow["A"].SetValueFromText($"{STTDVTH}.{++STTCongTrinh}. {grCongTrinh.First()["TenCongTrinh"]}");
                    var grsHM = grDvth.GroupBy(x => x["CodeHangMuc"].ToString());
                    int STTHangMuc = 0;
                    foreach (var grHM in grsCongTrinh)
                    {
                        Crow = ws.Rows[RowIndex++];
                        Crow.Font.Bold = true;
                        Crow.Font.Color = MyConstant.color_Row_HangMuc;
                        ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
                        Crow["A"].SetValueFromText($"{STTDVTH}.{STTCongTrinh}.{++STTHangMuc}. {grHM.First()["TenHangMuc"]}");
                        var grsCongTac = grHM.GroupBy(x => x["CodeCongViecCha"].ToString());

                        foreach (var grCTac in grsCongTac)
                        {
                            var fi = grDvth.First();
                            Crow = ws.Rows[RowIndex++];
                            Crow.Font.Bold = false;
                            Crow.Font.Color = Color.Black;
                            ws.Rows.Insert(RowIndex, 1, RowFormatMode.FormatAsNext);
                            Crow["A"].SetValueFromText($"{fi["TenCongViec"]} [{fi["LyTrinhCaoDo"]}]");
                            int filesCount = grCTac.Count();
                            int soCot = (int)nud_SoCot.Value;
                            int rowCount = (int)Math.Ceiling((double)grCTac.Count() / soCot);
                            ws.Rows.Insert(RowIndex, rowCount * DataMau.RowCount, RowFormatMode.FormatAsNext);
                            var drs = grCTac.ToArray();
                            for (int ind = 1; ind <= filesCount; ind++)
                            {
                                DataRow dr = drs[ind - 1];
                                string file = Path.Combine(BaseFrom.m_FullTempathDA, "Resource", "Files", Tbl, grCTac.Key, dr["Code"].ToString());
                                Image img = FileHelper.fcn_ImageStreamDoc(picEdit: null, file);
                                var src = SpreadsheetImageSource.FromImage(img);
                                int rowInd = (int)Math.Floor((double)ind / soCot);
                                int colInd = ind % soCot;
                                string fileName = dr["FileDinhKem"].ToString().Substring(0, dr["FileDinhKem"].ToString().LastIndexOf(".")); ;
                                if (colInd > 0)
                                {
                                    Crow = ws.Rows[RowIndex++];
                                    Crow.CopyFrom(DataMau, PasteSpecial.All);
                                    var targetCell = ws.Range[$"B{RowIndex}:F{RowIndex + DataMau.RowCount - 2}"];
                                    ws.Pictures.AddPicture(src, targetCell);
                                    RowIndex = RowIndex + DataMau.RowCount;
                                    ws.Rows[RowIndex - 2]["B"].SetValueFromText(fileName);
                                    if (!ce_DinhKemTenAnh.Checked)
                                        ws.Rows[RowIndex - 2].Visible = false;
                                }
                                else
                                {
                                    var targetCell = ws.Range[$"G{RowIndex - 2}:K{RowIndex - DataMau.RowCount}"];
                                    ws.Pictures.AddPicture(src, targetCell);
                                    ws.Rows[RowIndex - 2]["G"].SetValueFromText(fileName);
                                    if (!ce_DinhKemTenAnh.Checked)
                                        ws.Rows[RowIndex - 2].Visible = false;
                                }
                            }
                        }




                    }
                }
            }
            wb.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }
        private async void LoadBaoCaoOnlineHopDongThanhToan()
        {

            string[] colsSumPrivate =
            {
                    TDKH.COL_KhoiLuongThiCong,
                    TDKH.COL_GiaTriHopDongWithCPDP,
                };
            var fileMaus = slke_ChonMauBaoCao.GetSelectedDataRow() as FileViewModel;
            string m_Path = fileMaus.FilePath;
            uc_BaoCao1.SpreadSheet.LoadDocument(m_Path);

            var wb = uc_BaoCao1.SpreadSheet.Document;
            try
            {
                wb.BeginUpdate();
                var ws = wb.Worksheets[0];


                var dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
                var dfnData = ws.DefinedNames.Single(x => x.Name == nameData);

                string time = "";


                //if (cbe_CachLocKyTH.SelectedIndex == (int)PickDateType.TuDo)
                time = $"(Lũy kế đến ngày {de_begin.DateTime.ToShortDateString()})";


                ws.Range[nameTime].SetValue(time);

                var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<CongTacWithKLHN>(RouteAPI.TongDuAn_GetCongTacTDKHWithKLHN,
                    new KLHNRequest()
                    {
                        typeLayKhoiLuong = 0,
                        dateCalcLuyKe = de_begin.DateTime.Date
                    });

                if (!result.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError("Không thể lấy thông tin dự án từ server!");
                    uc_BaoCao1.PreviewSelectedIndex = 0;
                    return;
                }

                var cttks = result.Dto.Cttks;
                var klhns = result.Dto.KLHNBriefs;

                var grs = cttks.GroupBy(x => x.Owner);
                DataTable dtData = DataTableCreateHelper.BaoCaoTongHopHopDongThanhToan();

                int STTOwner = 0;
                double sumKLTB = 0, sumKLGK = 0;
                List<int> indsOwner = new List<int>();


                foreach (var gr in grs)
                {
                    var fstR = gr.First();
                    ++STTOwner;

                    DataRow newRowOwner = dtData.NewRow();
                    dtData.Rows.Add(newRowOwner);

                    newRowOwner[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CongTrinh;
                    newRowOwner[TDKH.COL_DanhMucCongTac] = $"Dự án của {fstR.Owner}";
                    newRowOwner[TDKH.COL_STT] = STTOwner.ToRoman();
                    int crIndRowOwner = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                    indsOwner.Add(crIndRowOwner);

                    int sttDA = 0;
                    var grsDA = gr.GroupBy(x => x.CodeDuAn);

                    foreach (var grDA in grsDA)
                    {
                        ++sttDA;

                        var fstDA = grDA.First();

                        var codesDA = grDA.Select(x => x.Code);
                        var KLHNInDA = klhns.Where(x => codesDA.Contains(x.ParentCode));

                        DataRow newRowDA = dtData.NewRow();
                        dtData.Rows.Add(newRowDA);
                        int crRowIndDA = dfnData.Range.TopRowIndex + dtData.Rows.Count;
                        newRowDA[TDKH.COL_STT] = sttDA;
                        newRowDA[TDKH.COL_TypeRow] = MyConstant.TYPEROW_CVCha;
                        newRowDA[TDKH.COL_DanhMucCongTac] = fstDA.TenDuAn;
                        newRowDA[TDKH.COL_KhoiLuongThiCong] = KLHNInDA.Sum(x => x.TTTCFromBeginOfPrj);
                        newRowDA[TDKH.COL_GiaTriHopDongWithCPDP] = grDA.Sum(x => x.KhoiLuongHopDongChiTiet * x.DonGia);

                        //var codesCTacGiaoThau = grDA.Where(x => x.CodeNhaThau != null).Select(x => x.Code);
                        //newRowDA[TDKH.COL_DBC_KhoiLuongToanBo] = KLHNGiaoThauInDA.Sum(x => x.TTKHInRange);



                    }
                    int crIndRow = dfnData.Range.TopRowIndex + 1 + dtData.Rows.Count;

                    foreach (var col in colsSumPrivate)
                    {
                        newRowOwner[col] = $"{MyConstant.PrefixFormula}SUM({dic[col]}{crIndRowOwner + 2}:{dic[col]}{crIndRow + 1})";
                    }
                }
                ws.Rows.Insert(dfnData.Range.BottomRowIndex, dtData.Rows.Count, RowFormatMode.FormatAsPrevious);
                ws.Import(dtData, false, dfnData.Range.TopRowIndex + 1, 0);
                SpreadsheetHelper.ReplaceAllFormulaAfterImport(dfnData.Range);
                SpreadsheetHelper.FormatRowsInRange(dfnData.Range, dic[TDKH.COL_TypeRow]);

                int fstInd = dfnData.Range.TopRowIndex;
                foreach (var col in colsSumPrivate)
                {
                    double val = 0;
                    foreach (int ind in indsOwner)
                    {
                        val += ws.Rows[ind][dic[col]].Value.NumericValue;
                    }


                    ws.Rows[fstInd][dic[col]].SetValue(val);
                }

            }
            catch (Exception ex)
            {
                //var err = CusLogHelper.GetLogStringFromException(ex);
                MessageShower.ShowError($"Lỗi tải file dự kiến thi công\r\n{ex.Message}");
                uc_BaoCao1.PreviewSelectedIndex = 0;


            }
            finally
            {
                wb.EndUpdate();
            }
        }

        private void cbe_CachLocKyTH_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadDate();
        }

        private void monthEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDate();
        }

        private void nud_year_ValueChanged(object sender, EventArgs e)
        {
            LoadDate();

        }

        private void cbbe_Week_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDate();

        }

        private void cbbe_NgayBatDauMoiTuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDate();

        }

        private void de_NBDThangNam_EditValueChanged(object sender, EventArgs e)
        {
            LoadDate();

        }

        private async void rb_onl_CheckedChanged(object sender, EventArgs e)
        {
            LoadMauBaoCao();
            await Fcn_UpdateDA();
        }

        private void uc_BaoCao1_Load(object sender, EventArgs e)
        {

        }
    }
}

