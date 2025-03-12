using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraRichEdit.API.Native;
using PhanMemQuanLyThiCong.Common.Constant;
using DevExpress.Spreadsheet;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using PhanMemQuanLyThiCong.Common.Helper;
using VChatCore.ViewModels.SyncSqlite;
using System.IO;
using PhanMemQuanLyThiCong.Model.Excel;
using DevExpress.Office.Utils;
using DocumentFormat = DevExpress.XtraRichEdit.DocumentFormat;
using PhanMemQuanLyThiCong.Properties;
using PhanMemQuanLyThiCong.Common.Enums;
using DevExpress.Utils.Extensions;
using DevExpress.Office;
using System.Drawing.Imaging;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
using DevExpress.Export.Xl;
using PhanMemQuanLyThiCong.Controls;
using WordTable = DevExpress.XtraRichEdit.API.Native.Table;
using PM360.Common.Helper;
using PhanMemQuanLyThiCong.Common.MLogging;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_XuatBaoCaoTongHop : DevExpress.XtraEditors.XtraForm
    {
        public enum LsTongHopEnum
        {
            STT,
            Ten,
            DonVi,
            KLKHKy,
            KLTCKyNay,
            KLKHUntilNow,
            KLTCUntilNow,
            DonGiaKeHoach,
            ThanhTienTheoKy,
            PercentKyNay,
            PercentCurrent,
        }

        public enum ThuChiTamUngEnum
        {
            STT,
            NoiDung,
            DaTamUng,
            DaChi,
            ConLai,
            DaGiaiChi,
            ChuaHoanUng
        }

        public enum VatTuXNTKEnum
        {
            STT,
            TenVatTu, 
            DonVi,
            KLNhapKyNay,
            KLXuatKho,
            KLTonKho,
            ThanhTienNhap,
            ThanhTienXuat,
            ThanhTienTonKho,
        }

        public enum SoLuongNhanLucTKEnum //Nhân lực trong kỳ
        {
            STT,
            NhanLuc,
            KLTHKyNay,
            KLTCUntilNow,
            ThanhTienKyNay,
            ThanhTienLuyKe
        }

        public enum MayMocTHTKEnum //Máy móc thực hiện trong kỳ
        {
            STT,
            MayMoc,
            KLTHKyNay,
            KLTCUntilNow,
            ThanhTienKyNay,
            ThanhTienLuyKe
        }

        public Form_XuatBaoCaoTongHop()
        {
            InitializeComponent();
        }
        public void Fcn_Update()
        {
            List<Tbl_ThongTinDuAnViewModel> lst = SharedControls.slke_ThongTinDuAn.Properties.DataSource as List<Tbl_ThongTinDuAnViewModel>;
            List<Tbl_ThongTinDuAnViewModel> Manage = lst.FindAll(x => x.Code == "Manage").ToList();
            if (Manage.Count() != 0)
                lst.Remove(Manage.SingleOrDefault());
            slke_ThongTinDuAn.Properties.DataSource = lst;
            //FileHelper.fcn_spSheetStreamDocument(spreadsheetControl1, Path.Combine(BaseFrom.m_path, "Template", "FileExcel", "BaoCaoTongHop.xlsx"));
            de_begin.DateTime = de_End.DateTime = DateTime.Now;
        }
        private void GetAllThanhTienVatLieu(Document doc, WordTable tbl)
        {
            //Worksheet ws = Insert.Worksheet;
            //int Index = Insert.BottomRowIndex;
            DateTime NBDBaoCao = de_begin.DateTime;
            DateTime NKTBaoCao = de_End.DateTime;
            string  dbString = $"SELECT \"Code\" FROM {TDKH.TBL_GiaiDoanThucHien} WHERE \"CodeDuAn\"='{slke_ThongTinDuAn.EditValue}'";
            DataTable dtgd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT {MyConstant.TBL_THONGTINNHACUNGCAP}.Code AS CodeDVTH, {MyConstant.TBL_THONGTINNHACUNGCAP}.Ten AS TenDVTH, " +
   $"{QLVT.TBL_QLVT_NHAPVT}.Code AS CodeCongTac,{QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu as TenCongTac,{QLVT.TBL_QLVT_YEUCAUVT}.MaVatTu as MaCongTac," +
   $"{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl as KhoiLuongHDVatLieu,{QLVT.TBL_QLVT_YEUCAUVT}.DonGiaHienTruong as DonGiaHDVatLieu , " +
   $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
   $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, " +
   $"{QLVT.TBL_QLVT_NHAPVTKLHN}.KhoiLuong AS KhoiLuongVatLieu,{QLVT.TBL_QLVT_NHAPVTKLHN}.DonGia AS DonGiaVatLieu,{QLVT.TBL_QLVT_NHAPVTKLHN}.Ngay " +
   $"FROM {MyConstant.TBL_THONGTINNHACUNGCAP} " +
   $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
   $"ON {QLVT.TBL_QLVT_YEUCAUVT}.TenNhaCungCap = {MyConstant.TBL_THONGTINNHACUNGCAP}.Code " +
   $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVT} " +
   $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code = {QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
   $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVTKLHN} ON " +
   $"{QLVT.TBL_QLVT_NHAPVTKLHN}.CodeCha = {QLVT.TBL_QLVT_NHAPVT}.Code " +
   $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
   $"ON {MyConstant.TBL_THONGTINHANGMUC}.Code = {QLVT.TBL_QLVT_YEUCAUVT}.CodeHangMuc " +
   $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
   $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
   $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{dtgd.Rows[0][0]}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KLTTHangNgay> lsSourceQLVC = new List<KLTTHangNgay>();
            lsSourceQLVC = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            string lstCodeNhap =MyFunction.fcn_Array2listQueryCondition(lsSourceQLVC.Select(x => x.CodeCha).Distinct().ToArray());
            dbString = $"SELECT {QLVT.TBL_QLVT_XUATVTKLHN}.*,{QLVT.TBL_QLVT_XUATVTKLHN}.KhoiLuong AS KhoiLuongVatLieu,{QLVT.TBL_QLVT_XUATVTKLHN}.DonGia AS DonGiaVatLieu," +
                $" {QLVT.TBL_QLVT_XUATVT}.CodeNhapVT AS CodeCongTac FROM {QLVT.TBL_QLVT_XUATVTKLHN} LEFT JOIN {QLVT.TBL_QLVT_XUATVT} ON {QLVT.TBL_QLVT_XUATVT}.Code={QLVT.TBL_QLVT_XUATVTKLHN}.CodeCha" +
                $" WHERE {QLVT.TBL_QLVT_XUATVT}.CodeNhapVT IN ({lstCodeNhap}) ";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KLTTHangNgay> lsSourceXuat = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            if (lsSourceQLVC.Count() == 0)
                return;
            var CrNCC = lsSourceQLVC.GroupBy(x => x.CodeDVTH);
            //CellRange Range = ws.Range[RangeWs];
            //ws.Rows.Insert(Range.BottomRowIndex-1,CrNCC.Count(), RowFormatMode.FormatAsNext);
            int STT = 0;
            double KLTCKN = 0, KLTCHT = 0;
            long TTTCKN = 0, TTTCHT = 0;
            //Row RowCop = ws.Rows[1];
            int crRowInd = tbl.LastRow.Index;
            foreach (var item in CrNCC)
            {
                TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                
                //Row Crow = ws.Rows[Index++];
                RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{++STT}");
                RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, item.First().TenDVTH);
                RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyColor.Normal);

                var CrCongTrinh = item.GroupBy(x => x.CodeCongTrinh);
                int STTCtrinh = 0;
                foreach (var Ctrinh in CrCongTrinh)
                {
                    RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                    RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_CongTrinh);

                    RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{STT}.{++STTCtrinh}");

                    RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, Ctrinh.First().TenCongTrinh);
                    var CrHangMuc = Ctrinh.GroupBy(x => x.CodeHangMuc);

                    int STTHM = 0;
                    foreach (var CHM in CrHangMuc)
                    {
                        RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                        RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_HangMuc);

                        RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{STT}.{STTCtrinh}.{++STTHM}");
                        RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, Ctrinh.First().TenHangMuc);

                        var CrCtac = CHM.GroupBy(x => x.ParentCode);
                        //ws.Rows.Insert(Index, CrCtac.Count(), RowFormatMode.FormatAsPrevious);
                        int STTCTac = 0;
                        foreach (var Ctac in CrCtac)
                        {
                            RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyConstant.color_Nomal);

                            RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{STT}.{STTCtrinh}.{STTHM}.{++STTCTac}");
                            RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, Ctrinh.First().TenHangMuc);
                            RowInSert[(int)VatTuXNTKEnum.DonVi].SetValue(doc, Ctrinh.First().DonVi);
  
                            KLTCKN = (double)Ctac.Where(x => x.Ngay.HasValue && x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao).Sum(x => x.KhoiLuongVatLieu);
                            TTTCKN = (long)Ctac.Where(x => x.Ngay.HasValue && x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao).Sum(x => x.ThanhTienVatLieu);
                            KLTCHT = (double)lsSourceXuat.Where(x =>x.CodeCha==item.First().CodeCha &&x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.KhoiLuongVatLieu);
                            TTTCHT = (long)lsSourceXuat.Where(x => x.CodeCha == item.First().CodeCha && x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.ThanhTienVatLieu);

                            RowInSert[(int)VatTuXNTKEnum.KLNhapKyNay].SetValue(doc, KLTCKN.ToString("N2"));
                            RowInSert[(int)VatTuXNTKEnum.KLXuatKho].SetValue(doc, KLTCHT.ToString("N2"));
                            RowInSert[(int)VatTuXNTKEnum.ThanhTienNhap].SetValue(doc, TTTCKN.ToString("N2"));
                            RowInSert[(int)VatTuXNTKEnum.ThanhTienXuat].SetValue(doc, TTTCHT.ToString("N2"));

                            RowInSert[(int)VatTuXNTKEnum.KLTonKho].SetValue(doc, (KLTCKN - KLTCHT).ToString("N2"));
                            RowInSert[(int)VatTuXNTKEnum.ThanhTienTonKho].SetValue(doc, (TTTCKN - TTTCHT).ToString("N2"));
                        }

                    }

                }
            }
        }
        private void GetAllThanhTienVatLieuTest(string LoaiVatTu, Document doc, WordTable tbl)
        {
            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();
            DateTime NBDBaoCao = de_begin.DateTime;
            DateTime NKTBaoCao = de_End.DateTime;
            string dbString = $"SELECT {TDKH.TBL_KHVT_KhoiLuongHangNgay}.*,{MyConstant.view_DonViThucHien}.Code as CodeDVTH,{MyConstant.view_DonViThucHien}.Ten as TenDVTH,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCongTac,{TDKH.TBL_KHVT_KhoiLuongHangNgay}.Code AS CodeHangNgay,{TDKH.TBL_KHVT_VatTu}.DonGia AS DonGiaKeHoach," +
                $" {TDKH.TBL_KHVT_VatTu}.DonGiaThiCong, {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,{TDKH.TBL_KHVT_VatTu}.DonVi,  " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
                $"FROM {TDKH.TBL_KHVT_KhoiLuongHangNgay} " +
                $"JOIN {TDKH.TBL_KHVT_VatTu} ON {TDKH.TBL_KHVT_VatTu}.Code={TDKH.TBL_KHVT_KhoiLuongHangNgay}.CodeVatTu " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"LEFT JOIN {MyConstant.view_DonViThucHien} " +
                $"ON ((COALESCE({TDKH.TBL_KHVT_VatTu}.CodeNhaThau, {TDKH.TBL_KHVT_VatTu}.CodeNhaThauPhu, {TDKH.TBL_KHVT_VatTu}.CodeToDoi)) = {MyConstant.view_DonViThucHien}.Code) "+
                $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_KHVT_VatTu}.LoaiVatTu='{LoaiVatTu}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<KLTTHangNgay> lsSource = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            List<KLTTHangNgay> lsSourceNT = lsSource.Where(x => x.NhaCungCap == null).ToList();
            List<KLTTHangNgay> lsSourceNCC = lsSource.Where(x => x.NhaCungCap != null).ToList();
            var lsCon = new List<KLTTHangNgay>();
            List<Tbl_ThongTinNhaCungCapViewModel> _nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
            foreach (KLTTHangNgay item in lsSourceNCC)
            {
                var NccsVM = MyFunction.TryGetObjFromJson<NhaCungCapHangNgayViewModel>(item.NhaCungCap);
                if (!NccsVM.Any())
                    continue;
                Dictionary<string, string> _DicNccs = _nccs.ToDictionary(x => x.Code, x => x.Ten);
                if (_DicNccs is null)
                {
                    var nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                    _DicNccs = nccs.ToDictionary(x => x.Code, x => x.Ten);
                }
                foreach (var ncc in NccsVM)
                {
                    if (!_DicNccs.Keys.Contains(ncc.Code))
                        continue;
                    lsCon.Add(new KLTTHangNgay()
                    {
                        ParentCode = item.CodeCha,
                        TenCongTac = item.TenCongTac,
                        DonVi = item.DonVi,
                        TenNhaCC = _DicNccs[ncc.Code],
                        Code = Guid.NewGuid().ToString(),
                        CodeNhaCungCap = ncc.Code,
                        KhoiLuongThiCong = ncc.KhoiLuong,
                        DonGiaThiCong = ncc.DonGia,
                        CodeHangMuc = item.CodeHangMuc,
                        CodeCongTrinh=item.CodeCongTrinh,
                        TenCongTrinh=item.TenCongTrinh,
                        TenHangMuc=item.TenHangMuc,
                        Ngay=item.Ngay
                        

                    });
                }
            }
            int STT = 0;
            double KLTCKN = 0, KLTCHT = 0;
            long TTTCKN = 0, TTTCHT = 0;
            int crRowInd = tbl.LastRow.Index;
            var CrowNhaThau = lsSourceNT.GroupBy(x => x.CodeDVTH);
            foreach (var item in CrowNhaThau)
            {

                TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);

                //Row Crow = ws.Rows[Index++];
                RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{++STT}");
                RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, item.First().TenDVTH);
                RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: Color.Black);

                var CrCongTrinh = item.GroupBy(x => x.CodeCongTrinh);
                int STTCtrinh = 0;

                foreach (var Ctrinh in CrCongTrinh)
                {
                    RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                    RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_CongTrinh);

                    RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{STT}.{++STTCtrinh}");

                    RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, Ctrinh.First().TenCongTrinh);

                    var CrHangMuc = Ctrinh.GroupBy(x => x.CodeHangMuc);
                    int STTHM = 0;
                    foreach (var CHM in CrHangMuc)
                    {
                        RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                        RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_HangMuc);

                        RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{STT}.{STTCtrinh}.{++STTHM}");
                        RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, Ctrinh.First().TenHangMuc);

                        var CrCtac = CHM.GroupBy(x => x.ParentCode);
                        int STTCTac = 0;
                        foreach (var Ctac in CrCtac)
                        {
                            RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyConstant.color_Nomal);

                            RowInSert[(int)SoLuongNhanLucTKEnum.STT].SetValue(doc, $"{STT}.{STTCtrinh}.{STTHM}.{++STTCTac}");
                            RowInSert[(int)SoLuongNhanLucTKEnum.NhanLuc].SetValue(doc, Ctac.First().TenCongTac);

                            KLTCKN = (double)Ctac.Where(x => x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao).Sum(x => x.KhoiLuongThiCong);
                            TTTCKN = (long)Ctac.Where(x => x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao).Sum(x => x.ThanhTienThiCong);
                            KLTCHT = (double)Ctac.Where(x => x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.KhoiLuongThiCong);
                            TTTCHT = (long)Ctac.Where(x => x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.ThanhTienThiCong);

                            RowInSert[(int)SoLuongNhanLucTKEnum.KLTHKyNay].SetValue(doc, KLTCKN.ToString("N2"));
                            RowInSert[(int)SoLuongNhanLucTKEnum.KLTCUntilNow].SetValue(doc, KLTCHT.ToString("N2"));
                            RowInSert[(int)SoLuongNhanLucTKEnum.ThanhTienKyNay].SetValue(doc, TTTCKN.ToString("N2"));
                            RowInSert[(int)SoLuongNhanLucTKEnum.ThanhTienLuyKe].SetValue(doc, TTTCHT.ToString("N2"));

                        }

                    }

                }
            }
            if (lsCon.Count() == 0)
                return;
            var CrNCC = lsCon.GroupBy(x => x.CodeNhaCungCap);

            foreach (var item in CrNCC)
            {

                TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);

                //Row Crow = ws.Rows[Index++];
                RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{++STT}");
                RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, item.First().TenNhaCC);
                RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: Color.Black);

                var CrCongTrinh = item.GroupBy(x => x.CodeCongTrinh);
                int STTCtrinh = 0;

                foreach (var Ctrinh in CrCongTrinh)
                {
                    RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                    RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_CongTrinh);

                    RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{STT}.{++STTCtrinh}");

                    RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, Ctrinh.First().TenCongTrinh);

                    var CrHangMuc = Ctrinh.GroupBy(x => x.CodeHangMuc);
                    int STTHM = 0;
                    foreach (var CHM in CrHangMuc)
                    {
                        RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                        RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_HangMuc);

                        RowInSert[(int)VatTuXNTKEnum.STT].SetValue(doc, $"{STT}.{STTCtrinh}.{++STTHM}");
                        RowInSert[(int)VatTuXNTKEnum.TenVatTu].SetValue(doc, Ctrinh.First().TenHangMuc);

                        var CrCtac = CHM.GroupBy(x => x.ParentCode);
                        int STTCTac = 0;
                        foreach (var Ctac in CrCtac)
                        {
                            RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyConstant.color_Nomal);

                            RowInSert[(int)SoLuongNhanLucTKEnum.STT].SetValue(doc, $"{STT}.{STTCtrinh}.{STTHM}.{++STTCTac}");
                            RowInSert[(int)SoLuongNhanLucTKEnum.NhanLuc].SetValue(doc, Ctac.First().TenCongTac);
                            //RowInSert[(int)VatTuXNTKEnum.DonVi].SetValue(doc, Ctrinh.First().DonVi);

                            KLTCKN = (double)Ctac.Where(x => x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao).Sum(x => x.KhoiLuongThiCong);
                            TTTCKN = (long)Ctac.Where(x => x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao).Sum(x => x.ThanhTienThiCong);
                            KLTCHT = (double)Ctac.Where(x => x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.KhoiLuongThiCong);
                            TTTCHT = (long)Ctac.Where(x => x.Ngay.Value.Date >= NBDBaoCao.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.ThanhTienThiCong);

                            RowInSert[(int)SoLuongNhanLucTKEnum.KLTHKyNay].SetValue(doc, KLTCKN.ToString("N2"));
                            RowInSert[(int)SoLuongNhanLucTKEnum.KLTCUntilNow].SetValue(doc, KLTCHT.ToString("N2"));
                            RowInSert[(int)SoLuongNhanLucTKEnum.ThanhTienKyNay].SetValue(doc, TTTCKN.ToString("N2"));
                            RowInSert[(int)SoLuongNhanLucTKEnum.ThanhTienLuyKe].SetValue(doc, TTTCHT.ToString("N2"));

                            //RowInSert[(int)SoLuongNhanLucTKEnum.KLTonKho].SetValue(doc, (KLTCKN - KLTCHT).ToString("N2"));
                            //RowInSert[(int)SoLuongNhanLucTKEnum.ThanhTienTonKho].SetValue(doc, (TTTCKN - TTTCHT).ToString("N2"));
                        }

                    }

                }
            }

        }
        private List<KLHN> Fcn_Update(string CodeDVTH, string colCode)
        {
            string[] lsCodeCongTac;
            string dbString = "";
        
            dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {colCode}='{CodeDVTH}'";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            var dtTheoNgay = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(TypeKLHN.CongTac, lsCodeCongTac);
            return dtTheoNgay;
        }
        private List<KLHN> Fcn_UpdateGoc(string CodeDVTH, string colCode, string CodeGD)
        {
            return null;
        }
        private void Fcn_UpdateBieuDo(string CodeGD)
        {
            string dbString = $"SELECT \"Code\",\"Ten\" FROM {MyConstant.TBL_THONGTINNHATHAU} WHERE \"CodeDuAn\"='{slke_ThongTinDuAn.EditValue}' AND \"CodeDuAn\"<>\"Code\"";
            DataTable dt_NhaThau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            var dtTheoNgay = Fcn_Update(dt_NhaThau.Rows[0]["Code"].ToString(), "CodeNhaThau");
            //DataTable dtTheoNgayGoc = Fcn_UpdateGoc(dt_NhaThau.Rows[0]["Code"].ToString(), "CodeNhaThau",CodeGD);
            if (dtTheoNgay == null)
                return;
            dbString = $"SELECT *  FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeGiaiDoan\"='{CodeGD}'";
            DataTable Dt_ChuKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lstcode = Dt_ChuKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            //dbString = $"SELECT *  FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" IN ({lstcode})";
            var dtTheoNgayTC = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.CongTac, lstcode);
            if (dtTheoNgayTC.Count() == 0)
                goto Label;
            Label:
            List<Chart_KhoiLuongThanhTien> Source = PushData(dtTheoNgay, 0.1, dt_NhaThau.Rows[0]["Code"].ToString(), "CodeNhaThau");
            cc_VatTu.DataSource = Source;

        }
        private List<Chart_KhoiLuongThanhTien> PushData(List<KLHN> lsSourcete, double TyLe, string CodeToChucCaNhan, string colCode)
        {
            string dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GiaTriGiaiChi as Chi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.DateXacNhanDaChi as Date FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
                $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} " +
                $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CodeDeXuat " +
                $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.ToChucCaNhanNhanChiPhiTamUng='{CodeToChucCaNhan}'";
            string dbStringThu = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NgayThangThucHien as Date,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.ThucTeThu as Thu  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
    $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} " +
    $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CodeDeXuat " +
    $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.ToChucCaNhanNhanChiPhiTamUng='{CodeToChucCaNhan}'";
            if (colCode == "DuAn")
            {
                dbString = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.GiaTriGiaiChi as Chi,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.DateXacNhanDaChi as Date FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
$"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} " +
$"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI}.CodeDeXuat " +
$"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.CodeDuAn='{slke_ThongTinDuAn.EditValue}'";
                dbStringThu = $"SELECT {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.NgayThangThucHien as Date,{ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.ThucTeThu as Thu  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
                    $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} " +
                    $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code={ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CodeDeXuat " +
                    $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.CodeDuAn='{slke_ThongTinDuAn.EditValue}'";
            }

            DataTable dt_kc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


            DataTable dt_kt = DataProvider.InstanceTHDA.ExecuteQuery(dbStringThu);

            List<Chart_KhoiLuongThanhTien> SourceThu = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kt);
            List<Chart_KhoiLuongThanhTien> SourceChi = DuAnHelper.ConvertToList<Chart_KhoiLuongThanhTien>(dt_kc);
            List<Chart_KhoiLuongThanhTien> SumThuChi = new List<Chart_KhoiLuongThanhTien>(); ;

            List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();
            var grsDate = lsSourcete.GroupBy(x => x.Ngay).OrderBy(x => x.Key);
            long luyKeTTKeHoach = 0;
            long luyKeTTThiCong = 0;
            long luyKeTTThiCongThu = 0;
            long luyKeThu = 0;
            long luyKeChi = 0;
            long ThanhTienThu = 0;
            long ThanhTienChi = 0;
            if (lsSourcete.Count() == 0)
            {
                if (SourceThu.Count() == 0 && SourceChi.Count() == 0)
                {
                    cc_VatTu.DataSource = null;
                    return null;
                }
                else
                {
                    if (SourceThu.Count() != 0)
                        SumThuChi.AddRange(SourceThu);
                    if (SourceChi.Count() != 0)
                        SumThuChi.AddRange(SourceChi);
                    var crDateChi = SumThuChi.GroupBy(x => x.Date).OrderBy(x => x.Key);
                    foreach (var item in crDateChi)
                    {
                        ThanhTienThu = item.Sum(x => x.Thu);
                        ThanhTienChi = item.Sum(x => x.Chi);
                        luyKeThu += ThanhTienThu;
                        luyKeChi += ThanhTienChi;
                        lsOut.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = DateTime.Parse(item.Key.ToString()),
                            LuyKeThu = luyKeThu,
                            LuyKeChi = luyKeChi,
                            Thu = ThanhTienThu,
                            Chi = ThanhTienChi
                        });

                    }
                    if (crDateChi.Count() == 1)
                    {
                        lsOut.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = crDateChi.First().Key.AddDays(5),
                            LuyKeThu = luyKeThu,
                            LuyKeChi = luyKeChi
                        });
                        lsOut.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = crDateChi.First().Key.AddDays(-5),
                            LuyKeThu = 0,
                            LuyKeChi = 0
                        });
                    }
                }
                if (colCode == "DuAn")
                {

                }
                return lsOut;

            }
            Chart_KhoiLuongThanhTien[] crThu;
            Chart_KhoiLuongThanhTien[] crChi;
            DateTime MaxNgay = grsDate.Max(x => x.Key);
            DateTime MinNgay = grsDate.Min(x => x.Key);

            crThu = SourceThu.Where(x => x.Date < MinNgay).ToArray();
            crChi = SourceChi.Where(x => x.Date < MinNgay).ToArray();
            if (crChi.Count() != 0)
                SumThuChi.AddRange(crChi);
            if (crThu.Count() != 0)
                SumThuChi.AddRange(crThu);
            if (SumThuChi.Count() != 0)
            {
                var crDateChi = SumThuChi.GroupBy(x => x.Date).OrderBy(x => x.Key);
                foreach (var item in crDateChi)
                {
                    ThanhTienThu = item.Sum(x => x.Thu);
                    ThanhTienChi = item.Sum(x => x.Chi);
                    luyKeThu += ThanhTienThu;
                    luyKeChi += ThanhTienChi;
                    lsOut.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = DateTime.Parse(item.Key.ToString()),
                        LuyKeThanhTienKeHoach = luyKeTTKeHoach,
                        LuyKeThanhTienThiCong = luyKeTTThiCong,
                        ThuThiCong = luyKeTTThiCongThu,
                        TyLe = TyLe,
                        LuyKeThu = luyKeThu,
                        LuyKeChi = luyKeChi,
                        Thu = ThanhTienThu,
                        Chi = ThanhTienChi
                    });

                }
            }
            foreach (var item in grsDate)
            {
                crThu = SourceThu.Where(x => x.Date == item.Key).ToArray();
                crChi = SourceChi.Where(x => x.Date == item.Key).ToArray();
                ThanhTienChi = ThanhTienThu = 0;
                if (crThu.Count() != 0)
                {
                    ThanhTienThu = crThu.AsEnumerable().Sum(x => x.Thu);
                    luyKeThu += ThanhTienThu;
                }
                if (crChi.Count() != 0)
                {
                    ThanhTienChi = crChi.AsEnumerable().Sum(x => x.Chi);
                    luyKeChi += ThanhTienChi;
                }


                long ThanhTienKH =(long)item.Sum(x => x.ThanhTienKeHoach);
                long ThanhTienTC =(long) item.Sum(x => x.ThanhTienThiCong);
                long ThanhTienTCThu =(long) item.Sum(x => x.ThanhTienThiCong);
                luyKeTTKeHoach += ThanhTienKH;
                luyKeTTThiCong += ThanhTienTC;
                luyKeTTThiCongThu += ThanhTienTCThu;

                lsOut.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    LuyKeThanhTienKeHoach = luyKeTTKeHoach,
                    LuyKeThanhTienThiCong = luyKeTTThiCong,
                    ThuThiCong = luyKeTTThiCongThu,
                    TyLe = TyLe,
                    LuyKeChi = luyKeChi,
                    LuyKeThu = luyKeThu,
                    ThanhTienKeHoach = ThanhTienKH,
                    ThanhTienThiCong = ThanhTienTC,
                    Thu = ThanhTienThu,
                    Chi = ThanhTienChi

                });
            }
            SumThuChi.Clear();
            crThu = SourceThu.Where(x => x.Date > MaxNgay).ToArray();
            crChi = SourceChi.Where(x => x.Date > MaxNgay).ToArray();
            if (crChi.Count() != 0)
                SumThuChi.AddRange(crChi);
            if (crThu.Count() != 0)
                SumThuChi.AddRange(crThu);
            if (SumThuChi.Count() != 0)
            {
                var crDateChi = SumThuChi.GroupBy(x => x.Date).OrderBy(x => x.Key);
                foreach (var item in crDateChi)
                {
                    ThanhTienThu = item.Sum(x => x.Thu);
                    ThanhTienChi = item.Sum(x => x.Chi);
                    luyKeThu += ThanhTienThu;
                    luyKeChi += ThanhTienChi;
                    lsOut.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = DateTime.Parse(item.Key.ToString()),
                        LuyKeThanhTienKeHoach = luyKeTTKeHoach,
                        LuyKeThanhTienThiCong = luyKeTTThiCong,
                        ThuThiCong = luyKeTTThiCongThu,
                        TyLe = TyLe,
                        LuyKeThu = luyKeThu,
                        LuyKeChi = luyKeChi,
                        Thu = ThanhTienThu,
                        Chi = ThanhTienChi
                    });

                }
            }
            if (colCode == "DuAn")
            {

            }

            return lsOut;
        }
        private  List<DonViThucHien> GetDonViThucHiens()
        {
            string codeDA =slke_ThongTinDuAn.EditValue as string;

            string[] tbls =
            {
                MyConstant.TBL_THONGTINNHATHAU,
                MyConstant.TBL_THONGTINNHATHAUPHU,
                MyConstant.TBL_THONGTINTODOITHICONG,
            };
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
        private void sb_xuatbaocao_Click(object sender, EventArgs e)
        {
            if (slke_ThongTinDuAn.EditValue == null)
            {
                MessageShower.ShowError("Vui lòng chọn dự án!");
                return;
            }
            string PathSave = "";

            if (xtraFolderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                PathSave = xtraFolderBrowserDialog1.SelectedPath;
            }
            else
                return;
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu");

            string dbString = $"SELECT * FROM {GiaoViec.TBL_CONGVIECCHA}  " +
                $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code " +
                $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code " +
                $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code WHERE {MyConstant.TBL_THONGTINDUAN}.Code='{slke_ThongTinDuAn.EditValue}'";
            DataTable dt_GiaoViec = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            double CTH = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Chưa thực hiện").ToArray().Count();
            double DTH = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Đang thực hiện").ToArray().Count();
            double DXD = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Đang xét duyệt").ToArray().Count();
            double KT = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Đề nghị kiểm tra").ToArray().Count();
            double HT = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Hoàn thành").ToArray().Count();
            double DHD = (double)dt_GiaoViec.AsEnumerable().Where(x => x["TrangThai"].ToString() == "Dừng hoạt động").ToArray().Count();
            //double Sum = (CTH + DTH + DXD + KT + HT + DHD) == 0 ? 1 : (CTH + DTH + DXD + KT + HT + DHD);
            Dictionary<string, double> TrangThai = new Dictionary<string, double>();
            TrangThai.Add("Chưa thực hiện", CTH );
            TrangThai.Add("Đang thực hiện", DTH );
            TrangThai.Add("Đang xét duyệt", DXD );
            TrangThai.Add("Đề nghị kiểm tra", KT);
            TrangThai.Add("Hoàn thành", HT);
            TrangThai.Add("Dừng hoạt động", DHD);
            dbString = $"SELECT \"Code\" FROM {TDKH.TBL_GiaiDoanThucHien} WHERE \"CodeDuAn\"='{slke_ThongTinDuAn.EditValue}'";
            DataTable dtgd= DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            Fcn_UpdateBieuDo(dtgd.Rows[0][0].ToString());
            using (RichEditControl red_FileListNT = new RichEditControl())
            {
                //List<DonViThucHien> DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.DataSource as List<DonViThucHien>;
                List<DonViThucHien> DVTH = GetDonViThucHiens();
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
                $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} " +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThau = {MyConstant.TBL_THONGTINNHATHAU}.Code " +
                $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} " +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeToDoi = {MyConstant.TBL_THONGTINTODOITHICONG}.Code " +
                $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} " +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThauPhu = {MyConstant.TBL_THONGTINNHATHAUPHU}.Code " +
                $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{dtgd.Rows[0][0]}' ";
                DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dtCongTacTheoKy.Rows.Count == 0)
                {
                    MessageShower.ShowError("Dự án chưa có công tác thêm vào, Vui lòng thêm đầy đủ thông tin để tạo được Báo cáo !", "Thông tin");
                    WaitFormHelper.CloseWaitForm();
                    return;
                }
                List<KLTTHangNgay> lstCTTheoCK = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dtCongTacTheoKy);
                DateTime NBDBaoCao = de_begin.DateTime;
                DateTime NKTBaoCao = de_End.DateTime;
                DateTime NKT = dtCongTacTheoKy.Rows.Count == 0 ? DateTime.Now : dtCongTacTheoKy.AsEnumerable().Where(x=>x["NgayKetThuc"]!=DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                DateTime MIN = dtCongTacTheoKy.Rows.Count == 0 ? DateTime.Now : dtCongTacTheoKy.AsEnumerable().Where(x=>x["NgayBatDau"]!=DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                string[] lstcodeCT =lstCTTheoCK.Select(x => x.Code).ToArray();
                //DataTable dtTheoNgay = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lstcodeCT,dateKT: NKT,ignoreKLKH:true);
                var dtTheoNgay = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.CongTac, lstcodeCT);
                //dtTheoNgay.Columns["ThanhTienThiCong"].Expression = "[DonGiaThiCong]*[KhoiLuongThiCong]";
                //List<KLTTHangNgay> KLHN = new List<KLTTHangNgay>();
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
                List<KLTTHangNgay> TongHop = dtTheoNgay.Select(x => new KLTTHangNgay()
                {
                    CodeCha = x.ParentCode,
                    Ngay=x.Ngay,
                    CodeCongTacTheoGiaiDoan = x.CodeCongTacTheoGiaiDoan,
                    KhoiLuongKeHoach = x.KhoiLuongKeHoach,
                    KhoiLuongThiCong = x.KhoiLuongThiCong,
                    ThanhTienThiCongCustom = x.ThanhTienThiCong,
                    DonGiaKeHoach = x.DonGiaKeHoach,
                    DonGiaThiCong = x.DonGiaThiCong
                }).ToList();

                Document doc = red_FileListNT.Document;
                //string Path = $@"{BaseFrom.m_templatePath}\FileWord\[Mẫu] Báo cáo dự án.docx";
                string m_Path = Path.Combine(BaseFrom.m_path, "Template", "FileWord", "[Mẫu] Báo cáo dự án.docx");
                //string m_PathExcel = Path.Combine(BaseFrom.m_path, "Template", "FileExcel", "BaoCaoTongHop.xlsx");
                doc.LoadDocument(m_Path);
                Bookmark[] bms = doc.Bookmarks.Where(x => !MyConstant.BCHN_BMS_NotLoop.Contains(x.Name)).ToArray();
                doc.DefaultTableProperties.TableLayout = TableLayoutType.Fixed;

                Worksheet ws;
                DataRow[] dt30Day = dtCongTacTheoKy.AsEnumerable().Where(x => DateTime.Parse(x["NgayKetThuc"].ToString()).Date > NKTBaoCao.Date).ToArray();
                dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} WHERE \"Code\"='{slke_ThongTinDuAn.EditValue}' ";
                DataTable dtDA = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                long TTDA = 0, TTDATH = 0;
                red_FileListNT.BeginUpdate();
                
                foreach (Bookmark bm in bms)
                {
                    if (bm.Name == MyConstant.CONST_ListCongTac)
                    {
                        //ws = wb.Worksheets[MyConstant.CONST_SheetName_BaoCaoDuAn];
                        //CellRange LstCongTac;
                        //Row RowCop = ws.Rows[1];
                        //int i = 3;
                        int STT = 0;

                        var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                        WordTable tbl = crCell.Table;
                        tbl.BeginUpdate();
                        int crRowInd = crCell.Row.Index;
                        

                        double KLKHKN = 0, KLTCKN = 0, KLTCHT = 0, KLKHHT = 0;
                        foreach (DonViThucHien item in DVTH.OrderBy(x => x.LoaiGiaoNhanThau))
                        {
                            if (lstCTTheoCK.Count() == 0)
                                continue;
                            DataRow[] Crow = dtCongTacTheoKy.AsEnumerable().Where(x => x[item.ColCodeFK].ToString() == item.Code).ToArray();
                            if (Crow.Count() == 0)
                                continue;
                            //LstCongTac = ws.Range[MyConstant.CONST_ListCongTac];
                            //Row RowInSert = ws.Rows[i++];
                            var CrCongTrinh = Crow.GroupBy(x => x["CodeCongTrinh"]);
                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);

                            //ws.Rows.Insert(LstCongTac.BottomRowIndex, Crow.Count() + CrCongTrinh.Count() + 1, RowFormatMode.FormatAsNext);
                            RowInSert.Range.UpdateBasicFormatRange(isBold: true);
                            doc.Replace(RowInSert[(int)LsTongHopEnum.STT].ContentRange, $"{++STT}");
                            doc.Replace(RowInSert[(int)LsTongHopEnum.Ten].ContentRange, item.Ten.ToUpper());

                            int STTCtrinh = 0;
                            foreach (var Ctrinh in CrCongTrinh)
                            {
                                RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_CongTrinh);

                                doc.Replace(RowInSert[(int)LsTongHopEnum.STT].ContentRange, $"{STT}.{++STTCtrinh}");
                                doc.Replace(RowInSert[(int)LsTongHopEnum.Ten].ContentRange, Ctrinh.FirstOrDefault()["TenCongTrinh"].ToString());

                                var CrHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"]);
                                //ws.Rows.Insert(i, CrHangMuc.Count(), RowFormatMode.FormatAsPrevious);
                                int STTHMuc = 0;
                               
                                foreach (var CHM in CrHangMuc)
                                {
                                    RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                    RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_HangMuc);

                                    doc.Replace(RowInSert[(int)LsTongHopEnum.STT].ContentRange, $"{STT}.{STTCtrinh}.{++STTHMuc}");
                                    doc.Replace(RowInSert[(int)LsTongHopEnum.Ten].ContentRange, CHM.FirstOrDefault()["TenHangMuc"].ToString());

                                    var CrCongtac = CHM.GroupBy(x => x["Code"].ToString());
                                    int STTCTac = 0;
                                    foreach (var Ctac in CrCongtac)
                                    {
                                        var KLHNHaveDate = dtTheoNgay is null?null:dtTheoNgay.Where(x => x.KhoiLuongThiCong != null&&x.ParentCode==Ctac.Key).ToList();
                                        RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                        RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyConstant.color_Nomal);

                                        //RowInSert.CopyFrom(RowCop, PasteSpecial.All);
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.STT].ContentRange, $"{STT}.{STTCtrinh}.{STTHMuc}.{++STTCTac}");
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.Ten].ContentRange, Ctac.FirstOrDefault()["TenCongTac"].ToString());
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.DonVi].ContentRange, Ctac.FirstOrDefault()["DonVi"].ToString());

                                        KLKHKN = (double)TongHop.Where(x => x.CodeCha == Ctac.Key.ToString() && x.Ngay >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao.Date && x.KhoiLuongKeHoach != null).Sum(x => x.KhoiLuongKeHoach);
                                        KLKHHT = TongHop.Where(x => x.CodeCha == Ctac.Key.ToString() && x.Ngay.Value.Date >= MIN.Date && x.Ngay.Value.Date <= DateTime.Now.Date && x.KhoiLuongThiCong != null).Any()?(double)TongHop.Where(x => x.CodeCha == Ctac.Key.ToString() && x.Ngay.Value.Date >= MIN.Date && x.Ngay.Value.Date <= DateTime.Now.Date&&x.KhoiLuongThiCong!=null).Sum(x => x.KhoiLuongThiCong):0;

                                        KLTCKN = KLHNHaveDate is null ?0:(double)KLHNHaveDate.Where(x =>x.Ngay.Date >= NBDBaoCao.Date && x.Ngay.Date <= NKTBaoCao.Date).Sum(x => x.KhoiLuongThiCong);
                                        KLTCHT = KLHNHaveDate is null ? 0 : (double)KLHNHaveDate.Where(x => x.Ngay.Date >= MIN.Date && x.Ngay.Date <= DateTime.Now.Date).Sum(x => x.KhoiLuongThiCong);

                                        //if (item.LoaiGiaoNhanThau == "Giao thầu")
                                        //{

                                        //}
                                        //else
                                        //{
                                        //    KLTCKN = (double)KLHNHaveDate.Where(x => x.CodeCha == Ctac.Key.ToString() && x.Ngay >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao.Date).Sum(x => x.KhoiLuongThiCong);
                                        //    KLTCHT = (double)KLHNHaveDate.Where(x => x.CodeCha == Ctac.Key.ToString() && x.Ngay.Value.Date >= MIN.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.KhoiLuongThiCong);
                                        //}
                                        long.TryParse(Ctac.FirstOrDefault()["DonGiaKeHoach"].ToString(), out long DGKH);

                                        doc.Replace(RowInSert[(int)LsTongHopEnum.KLKHKy].ContentRange, KLKHKN.ToString("N2"));
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.KLTCKyNay].ContentRange, KLTCKN.ToString("N2"));
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.KLKHUntilNow].ContentRange, KLKHHT.ToString("N2"));
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.KLTCUntilNow].ContentRange, KLTCHT.ToString("N2"));
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.DonGiaKeHoach].ContentRange, DGKH.ToString());
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.ThanhTienTheoKy].ContentRange, (DGKH*KLKHKN).ToString("N0"));
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.PercentKyNay].ContentRange, (KLKHKN == 0) ? "0%" : $"{(KLTCKN*100/KLKHKN).ToString("N2")}%");
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.PercentCurrent].ContentRange, (KLKHHT == 0) ? "0%" : $"{(KLTCHT*100/ KLKHHT).ToString("N2")}%");
                                    }
                                }

                            }

                        }

                        crCell.Row.Delete();
                        tbl.EndUpdate();


                        crCell = doc.Tables.GetTableCell(doc.Bookmarks[MyConstant.CONST_ListCongTacKyToi].Range.Start);
                        tbl = crCell.Table;
                        tbl.BeginUpdate();
                        crRowInd = crCell.Row.Index;
                        STT = 0;
                        foreach (DonViThucHien item in DVTH.OrderBy(x => x.LoaiGiaoNhanThau))
                        {
                            if (dt30Day.Count() == 0)
                                continue;
                            DataRow[] Crow = dt30Day.Where(x => x[item.ColCodeFK].ToString() == item.Code).ToArray();
                            if (Crow.Count() == 0)
                                continue;

                            var CrCongTrinh = Crow.GroupBy(x => x["CodeCongTrinh"]);

                            TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
   
                            RowInSert.Range.UpdateBasicFormatRange(isBold: true);
                            doc.Replace(RowInSert[(int)LsTongHopEnum.STT].ContentRange, $"{++STT}");
                            doc.Replace(RowInSert[(int)LsTongHopEnum.Ten].ContentRange, item.Ten.ToUpper());
                            int STTCtrinh = 0;
                            foreach (var Ctrinh in CrCongTrinh)
                            {
                                RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_CongTrinh);

                                doc.Replace(RowInSert[(int)LsTongHopEnum.STT].ContentRange, $"{STT}.{++STTCtrinh}");
                                doc.Replace(RowInSert[(int)LsTongHopEnum.Ten].ContentRange, Ctrinh.FirstOrDefault()["TenCongTrinh"].ToString());

                                var CrHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"]);
                                int STTHMuc = 0;

                                foreach (var CHM in CrHangMuc)
                                {
                                    RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                    RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyConstant.color_Row_HangMuc);

                                    doc.Replace(RowInSert[(int)LsTongHopEnum.STT].ContentRange, $"{STT}.{STTCtrinh}.{++STTHMuc}");
                                    doc.Replace(RowInSert[(int)LsTongHopEnum.Ten].ContentRange, CHM.FirstOrDefault()["TenHangMuc"].ToString());

                                    var CrCongtac = CHM.GroupBy(x => x["Code"]);
                                    int STTCTac = 0;
                                    foreach (var Ctac in CrCongtac)
                                    {
                                        RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                        RowInSert.Range.UpdateBasicFormatRange(isBold: false, foreColor: MyConstant.color_Nomal);


                                        long.TryParse(Ctac.FirstOrDefault()["DonGiaKeHoach"].ToString(), out long DGKH);
                                        //RowInSert.CopyFrom(RowCop, PasteSpecial.All);
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.STT].ContentRange, $"{STT}.{STTCtrinh}.{STTHMuc}.{++STTCTac}");
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.Ten].ContentRange, Ctac.FirstOrDefault()["TenCongTac"].ToString());
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.DonVi].ContentRange, Ctac.FirstOrDefault()["DonVi"].ToString());
                                        KLKHKN = (double)TongHop.Where(x => x.CodeCha == Ctac.Key.ToString() && x.Ngay >= NKTBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao.AddDays(30).Date).Sum(x => x.KhoiLuongKeHoach);
                                        //KLKHHT = (double)TongHop.Where(x => x.CodeCongTac == Ctac.Key.ToString() && x.Ngay.Value.Date >= MIN.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.KhoiLuongThiCong);
                                        //if (item.LoaiGiaoNhanThau == "Giao thầu")
                                        //{
                                        //    KLTCKN = (double)KLHN.Where(x => x.CodeDanhMucCongTac == Ctac.FirstOrDefault()["CodeGoc"].ToString() && x.Ngay >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao.Date).Sum(x => x.KhoiLuongThiCong);
                                        //    KLTCHT = (double)KLHN.Where(x => x.CodeDanhMucCongTac == Ctac.FirstOrDefault()["CodeGoc"].ToString() && x.Ngay.Value.Date >= MIN.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.KhoiLuongThiCong);
                                        //}
                                        //else
                                        //{
                                        //    KLTCKN = (double)KLHN.Where(x => x.CodeCongTac == Ctac.Key.ToString() && x.Ngay >= NBDBaoCao.Date && x.Ngay.Value.Date <= NKTBaoCao.Date).Sum(x => x.KhoiLuongThiCong);
                                        //    KLTCHT = (double)KLHN.Where(x => x.CodeCongTac == Ctac.Key.ToString() && x.Ngay.Value.Date >= MIN.Date && x.Ngay.Value.Date <= DateTime.Now.Date).Sum(x => x.KhoiLuongThiCong);
                                        //}
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.KLKHKy].ContentRange, KLKHKN.ToString("N2"));

                                        //RowInSert["E"].SetValue(KLTCKN);
                                        //RowInSert["F"].SetValue(KLTCHT);
                                        //RowInSert["G"].SetValue(Ctac.FirstOrDefault()["DonGiaKeHoach"]);
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.DonGiaKeHoach].ContentRange, DGKH.ToString());
                                        doc.Replace(RowInSert[(int)LsTongHopEnum.ThanhTienTheoKy].ContentRange, (DGKH * KLKHKN).ToString("N0"));
                                        //RowInSert["K"].SetValue(KLKHHT);
                                    }
                                }
                            }
                        }
                        crCell.Row.Delete();

                        tbl.EndUpdate();
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_TyLeHT)
                    {
                        long TTKH = (long)lstCTTheoCK.Where(x => x.CodeNhaThau != null).Sum(x => x.ThanhTienKeHoach);
                        long TTTC = (long)Math.Round(dtTheoNgay.Sum(x => x.ThanhTienThiCong)??0);
                        double TyLe = TTTC == 0 ? 0 : Math.Round((double)TTTC / TTKH, 2) * 100;
                        doc.Replace(bm.Range, $"{TyLe}%");
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_ChiPhi)
                    {

                        doc.Replace(bm.Range, lstCTTheoCK.Where(x => x.CodeNhaThau != null).Sum(x => x.KinhPhiTheoTienDo).ToString());
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_ChiPhiDuKien)
                    {
                        doc.Replace(bm.Range, lstCTTheoCK.Where(x => x.CodeNhaThau != null).Sum(x => x.KinhPhiDuKien).ToString());
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_CTDTH)
                    {

                        doc.Replace(bm.Range, TrangThai["Đang thực hiện"].ToString());
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_SoNgay)
                    {
                        double songay = (DateTime.Parse(dtDA.Rows[0]["NgayKetThuc"].ToString()) - DateTime.Parse(dtDA.Rows[0]["NgayBatDau"].ToString())).TotalDays;
                        double songaythuchien = (de_End.DateTime.Date - DateTime.Parse(dtDA.Rows[0]["NgayBatDau"].ToString()).Date).TotalDays;
                        doc.Replace(bm.Range, $"{songaythuchien}/{songay}");
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_CTHT)
                    {
                        doc.Replace(bm.Range, TrangThai["Hoàn thành"].ToString());
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_CTCTH)
                    {
                        doc.Replace(bm.Range, TrangThai["Chưa thực hiện"].ToString());
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_CTDA)
                    {
                        doc.Replace(bm.Range, lstCTTheoCK.Where(x => x.CodeNhaThau != null).Count().ToString());
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_CTDungHĐ)
                    {
                        doc.Replace(bm.Range, TrangThai["Dừng hoạt động"].ToString());
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_CVKT)
                    {
                        doc.Replace(bm.Range, TrangThai["Đề nghị kiểm tra"].ToString());
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_CTXD)
                    {
                        doc.Replace(bm.Range, TrangThai["Đang xét duyệt"].ToString());
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_ThongTinNhaThau)
                    {
                        //ws = wb.Worksheets[MyConstant.CONST_SheetName_ThongTinNhaThau];
                        long TTKH = 0, TTTC = 0;
                        List<DonViThucHien> NhanThau = DVTH.Where(x => x.Table != MyConstant.TBL_THONGTINNHATHAU).ToList();
                        //CellRange RangeNhaThau = ws.Range[MyConstant.CONST_SheetName_ThongTinNhaThau];
                        //CellRange RangeTenNhaThau = ws.Range[MyConstant.CONST_SheetName_TenNhaThau];
                        //ws.Rows.Insert(RangeNhaThau.BottomRowIndex, (NhanThau.Count() - 1) * 3, RowFormatMode.FormatAsPrevious);
                        int STT = 0;

                        var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                        WordTable tbl = crCell.Table;
                        tbl.BeginUpdate();
                        int crRowInd = crCell.Row.Index;
                        doc.Delete(crCell.ContentRange);
                        int firstIndNhanThau = crCell.Row.Index;

                        bool Insert = false;
                        foreach (DonViThucHien item in DVTH.OrderBy(x => x.LoaiGiaoNhanThau))
                        {
                            dbString = $"SELECT* FROM {item.Table} WHERE \"Code\"='{item.Code}'";
                            DataTable dtNhaThau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            TTKH = 0;
                            TTTC = 0;
                            if (item.LoaiGiaoNhanThau == "Giao thầu")
                            {
                                dbString = $"SELECT* FROM {MyConstant.TBL_THONGTINDUAN} WHERE \"Code\"='{slke_ThongTinDuAn.EditValue}'";
                                var dtDuAn = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThongTinDuAnViewModel>(dbString);
                                tbl.Rows[1][1].SetValue(doc, dtDuAn.FirstOrDefault().TenDuAn);
                                tbl.Rows[4][1].SetValue(doc, dtDuAn.FirstOrDefault().DiaChi);
                                tbl.Rows[3][1].SetValue(doc, item.Ten);
                                tbl.Rows[5][1].SetValue(doc, dtNhaThau.Rows[0]["MaSoThue"].ToString());
                                tbl.Rows[6][1].SetValue(doc, dtNhaThau.Rows[0]["DaiDien"].ToString());
                                tbl.Rows[6][3].SetValue(doc, dtNhaThau.Rows[0]["DienThoai"].ToString());
                                if (lstCTTheoCK.Count() == 0)
                                    continue;
                                List<KLTTHangNgay> Nhathau = lstCTTheoCK.Where(x => x.CodeNhaThau != null).ToList();
                                lstcodeCT = Nhathau.Select(x => x.Code).ToArray();
                                TTKH =TTDA= (long)Nhathau.Sum(x => x.ThanhTienKeHoach);
                                TTTC =TTDATH= dtTheoNgay is null ? 0 : (long)dtTheoNgay.Where(x=>lstcodeCT.Contains(x.ParentCode)).Sum(x => x.ThanhTienThiCong);
                                tbl.Rows[2][1].SetValue(doc, TTKH.ToString("N2"));
                                tbl.Rows[2][3].SetValue(doc, TTTC.ToString("N2"));

                            }
                            else
                            {
                                if (lstCTTheoCK.Count() == 0)
                                    goto Label;
                                DataRow[] Crow = dtCongTacTheoKy.AsEnumerable().Where(x => x[item.ColCodeFK].ToString() == item.Code).ToArray();
                                if (Crow.Count() == 0)
                                    goto Label;
                                TTKH = (long)Crow.Where(x => x["KhoiLuongKeHoach"] != DBNull.Value && x["DonGiaKeHoach"] != DBNull.Value).Sum(x => double.Parse(x["KhoiLuongKeHoach"].ToString()) * double.Parse(x["DonGiaKeHoach"].ToString()));
                                if (dtTheoNgay != null)
                                {
                                    lstcodeCT = Crow.Select(x => x["Code"].ToString()).ToArray();
                                    List<KLHN> CrKLHN = dtTheoNgay is null ? null : dtTheoNgay.Where(x => lstcodeCT.Contains(x.ParentCode)).ToList();
                                    TTTC = CrKLHN is null ? 0 : (long)CrKLHN.Sum(x => x.ThanhTienThiCong);
                                }

                                Label:
                                if (!Insert)
                                {
                                    Insert = true;
                                    tbl.Rows[firstIndNhanThau][1].SetValue(doc, item.Ten);
                                    tbl.Rows[firstIndNhanThau + 1][1].SetValue(doc, dtNhaThau.Rows[0]["DiaChi"].ToString());
                                    tbl.Rows[firstIndNhanThau + 1][3].SetValue(doc, dtNhaThau.Rows[0]["DienThoai"].ToString());
                                    tbl.Rows[firstIndNhanThau + 2][1].SetValue(doc, TTKH.ToString("N2"));
                                    tbl.Rows[firstIndNhanThau + 2][3].SetValue(doc, TTTC.ToString("N2"));
                                }
                                else
                                {
                                    DocumentPosition start = crCell.Row.Range.Start;
                                    DocumentPosition end = tbl.Rows[firstIndNhanThau + 2].Range.End;

                                    doc.Copy(doc.CreateRange(crCell.Row.Range.Start, end.ToInt() - start.ToInt()));
                                    //TableRow RowInSert = tbl.Rows.InsertAfter(tbl.LastRow.Index);

                                    int crInd = tbl.LastRow.Index + 1;
                                    doc.CaretPosition = tbl.Range.End;
                                    doc.Paste();
                                    tbl.Rows[crInd][1].SetValue(doc, item.Ten);
                                    tbl.Rows[crInd + 1][1].SetValue(doc, dtNhaThau.Rows[0]["DiaChi"].ToString());
                                    tbl.Rows[crInd + 1][3].SetValue(doc, dtNhaThau.Rows[0]["DienThoai"].ToString());
                                    tbl.Rows[crInd + 2][1].SetValue(doc, TTKH.ToString("N2"));
                                    tbl.Rows[crInd + 2][3].SetValue(doc, TTTC.ToString("N2"));
                                }
                            }
                        }
                        tbl.EndUpdate();

                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_VatLieu)
                    {
                        //ws = wb.Worksheets[MyConstant.CONST_SheetName_VatLieu];
                        var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                        WordTable tbl = crCell.Table;
                        tbl.BeginUpdate();
                        crCell.Row.Delete();

                        GetAllThanhTienVatLieu(doc, tbl);
                        tbl.EndUpdate();
                        ////CellRange VL = ws.Range[MyConstant.CONST_SheetName_VatLieu];
                        //ws.Rows.Remove(VL.BottomRowIndex - 1, 2);
                        //ws.Rows[1].Delete();
                        //doc.CaretPosition = bm.Range.Start;
                        //doc.Delete(bm.Range);
                        //ExcelHTML html = TongHopHelper.fcn_ExportHTML(VL);
                        //doc.InsertHtmlText(doc.CaretPosition, html.ecd.GetString(html.stream.ToArray()));
                        //doc.Delete(doc.CreateRange(doc.CaretPosition, 1));


                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_NhanCong)
                    {
                        var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                        WordTable tbl = crCell.Table;
                        tbl.BeginUpdate();
                        crCell.Row.Delete();
                        GetAllThanhTienVatLieuTest("Nhân công", doc, tbl);
                        tbl.EndUpdate();
                        
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_MayThiCong)
                    {
                        var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                        WordTable tbl = crCell.Table;
                        tbl.BeginUpdate();
                        crCell.Row.Delete();
                        GetAllThanhTienVatLieuTest("Máy thi công", doc, tbl);
                        tbl.EndUpdate();
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_TenDuAn)
                    {
                        doc.CaretPosition = bm.Range.Start;
                        doc.Delete(bm.Range);
                        //doc.Replace(bm.Range, slke_ThongTinDuAn.Text.ToUpper());
                        doc.InsertText(doc.CaretPosition, slke_ThongTinDuAn.Text.ToUpper());

                    }     
                    else if (bm.Name == MyConstant.CONST_SheetName_TienDuAn)
                    {
                        //long Tongvon = dtDA.Rows[0]["TongVon"]!=DBNull.Value?long.Parse(dtDA.Rows[0]["TongVon"].ToString()):0;
                        doc.CaretPosition = bm.Range.Start;
                        doc.Delete(bm.Range);
                        //doc.Replace(bm.Range, slke_ThongTinDuAn.Text.ToUpper());
                        doc.InsertText(doc.CaretPosition, TTDA.ToString("N0"));

                    }     
                    else if (bm.Name == MyConstant.CONST_SheetName_ThanhTienTCDA)
                    {
                        //long Tongvon = dtDA.Rows[0]["TongVon"]!=DBNull.Value?long.Parse(dtDA.Rows[0]["TongVon"].ToString()):0;
                        doc.CaretPosition = bm.Range.Start;
                        doc.Delete(bm.Range);
                        //doc.Replace(bm.Range, slke_ThongTinDuAn.Text.ToUpper());
                        doc.InsertText(doc.CaretPosition, TTDATH.ToString("N0"));

                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_ThuChiTamUng)
                    {
                        //ws = wb.Worksheets[MyConstant.CONST_SheetName_ThuChiTamUng];
                        dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} WHERE \"CodeDuAn\"='{slke_ThongTinDuAn.EditValue}'";
                        DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        string lst = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
                        dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} WHERE \"CodeDeXuat\" IN ({lst})";
                        dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        List<KhoanChi> KC = DuAnHelper.ConvertToList<KhoanChi>(dt);
                        int STT = 0;
                        //CellRange ThuChi = ws.Range[bm.Name];

                        var crCell = doc.Tables.GetTableCell(bm.Range.Start);
                        WordTable tbl = crCell.Table;
                        tbl.BeginUpdate();
                        int crRowInd = crCell.Row.Index - 1;
                        crCell.Row.Delete();

                        if (KC.Count() != 0)
                        {

                            int i = 2;
                            foreach (KhoanChi item in KC)
                            {
                                TableRow RowInSert = tbl.Rows.InsertAfter(crRowInd++);
                                RowInSert.Range.UpdateBasicFormatRange(isBold: true, foreColor: MyColor.Normal);

                                RowInSert[(int)ThuChiTamUngEnum.STT].SetValue(doc, $"{STT++}");
                                RowInSert[(int)ThuChiTamUngEnum.NoiDung].SetValue(doc, item.NoiDungUng);
                                RowInSert[(int)ThuChiTamUngEnum.DaTamUng].SetValue(doc, item.GiaTriTamUngThucTe.ToString("N2"));
                                RowInSert[(int)ThuChiTamUngEnum.DaChi].SetValue(doc, item.GiaTriGiaiChi.ToString("N2"));
                                RowInSert[(int)ThuChiTamUngEnum.ChuaHoanUng].SetValue(doc, item.GiaTriGiaiChi.ToString("N2"));
                            }
                        }
                        tbl.EndUpdate();

                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_TenNhaThauChinh)
                    {
                        //doc.Delete(bm.Range);

                        doc.Replace(bm.Range, DVTH.Where(x => x.IsGiaoThau).FirstOrDefault().Ten.ToUpper());

                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_DateBaoCao)
                    {
                        doc.CaretPosition = bm.Range.Start;
                        doc.Delete(bm.Range);
                        //doc.Replace(bm.Range, slke_ThongTinDuAn.Text.ToUpper());
                        doc.InsertText(doc.CaretPosition, $"(Báo cáo từ ngày {NBDBaoCao.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} đến ngày {NKTBaoCao.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)})");
                        ////doc.Delete(bm.Range);
                        //doc.Replace(bm.Range, $"(Báo cáo từ ngày {NBDBaoCao.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} đến ngày {NKTBaoCao.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)})");

                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_DateHienTai)
                    {
                        doc.Replace(bm.Range, $"Ngày {DateTime.Now.Day} tháng {DateTime.Now.Month} năm {DateTime.Now.Year}");

                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_TenBieuDo)
                    {
                        //doc.Replace(bm.Range, $"Biểu đồ chi phí {DVTH.Where(x => x.IsGiaoThau).FirstOrDefault().Ten}");
                        doc.CaretPosition = bm.Range.Start;
                        doc.Delete(bm.Range);
                        //doc.Replace(bm.Range, slke_ThongTinDuAn.Text.ToUpper());
                        doc.InsertText(doc.CaretPosition, $"Biểu đồ chi phí {DVTH.Where(x => x.IsGiaoThau).FirstOrDefault().Ten}");
                        
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_BieuDo)
                    {
                        doc.CaretPosition = bm.Range.Start;
                        doc.Delete(bm.Range);
                        MemoryStream st = new MemoryStream();
                        DevExpress.XtraPrinting.HtmlExportOptions options = new DevExpress.XtraPrinting.HtmlExportOptions();
                        cc_VatTu.ExportToImage(st, ImageFormat.Png);
                        Image check = Image.FromStream(st);

                        doc.Images.Insert(doc.CaretPosition, check);
                        //ExcelHTML html = new ExcelHTML() { stream = st, Encode = options.CharacterSet };

                        //doc.InsertText(doc.CaretPosition, Characters.LineBreak.ToString());
                        //doc.InsertHtmlText(doc.CaretPosition, html.Encode);
                        //doc.Delete(doc.CreateRange(doc.CaretPosition, 1));
                    }
                    else if (bm.Name == MyConstant.CONST_SheetName_HinhAnh)
                    {

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
                                    $"WHERE (cttk.CodeGiaiDoan IS NULL OR cttk.CodeGiaiDoan = '{dtgd.Rows[0][0]}') AND (file.Ngay IS NULL " +
                                    $"OR (file.Ngay >= '{de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND file.Ngay <= '{de_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')) " +
                                    $"AND ctrinh.CodeDuAn = '{slke_ThongTinDuAn.EditValue}' " +
                                    $"AND (file.FileDinhKem LIKE '%.jpg' OR file.FileDinhKem LIKE '%.png' OR file.FileDinhKem LIKE '%.ico' OR file.FileDinhKem LIKE '%.jpeg')";
                        DataTable dtFile = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                        int STTDVTH = 0;

                        doc.CaretPosition = bm.Range.Start;
                        doc.Delete(bm.Range);
                        if (dtFile.Rows.Count == 0)
                            continue;

                        var section = doc.GetSection(doc.CaretPosition);
                        int withDocByDoc = (int)Math.Round(Units.InchesToDocumentsF(MyConstant.width_A4) - section.Margins.Left - section.Margins.Right);
                        int offsetIndent = (int)Math.Round(Units.CentimetersToDocumentsF((float)0.2));
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

                                        WordTable tblHinhAnh;

                                        int rowCount = (int)Math.Ceiling((double)grCTac.Count() / soCot);

                                        if (ce_DinhKemTenAnh.Checked)
                                            rowCount = rowCount * 2;

                                        tblHinhAnh = doc.Tables.Create(doc.CaretPosition, rowCount, soCot, AutoFitBehaviorType.FixedColumnWidth, withDocByDoc);
                                        //tblHinhAnh.Borders
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
                                            string fileName = dr["FileDinhKem"].ToString();

                                            if (ce_DinhKemTenAnh.Checked)
                                            {
                                                TableCell cellName = tblHinhAnh.Cell(rowInd - 1, colInd);
                                                doc.fcn_InsertTextWithAlignment(cellName.ContentRange.Start, fileName, ParagraphAlignment.Left);
                                            }

                                            try
                                            {
                                                Image img = FileHelper.fcn_ImageStreamDoc(picEdit: null, file);

                                                int widthCellByDoc = (int)Math.Round((double)withDocByDoc / soCot - offsetIndent * 2);
                                                int widthCellByPix = Units.DocumentsToPixels(widthCellByDoc, red_FileListNT.DpiX);

                                                int h = h = (int)Math.Round((double)(img.Height * widthCellByPix / img.Width));
                                                DocumentImage docImg = doc.Images.Insert(cellImg.Range.Start, (Image)(new Bitmap(img, new Size(widthCellByPix, h))));
                                            }
                                            catch
                                            {
                                                Logging.Error("Lỗi đọc file: " + fileName);
                                            }

                                        }


                                    }
                                }
                            }



                            //doc.Paragraphs.Get(doc.CaretPosition).Range.UpdateBasicFormatRange(foreColor: Color.Blue);
                            appendedParagraph = doc.Paragraphs.Insert(doc.CaretPosition);
                        }

                    }
                }

                red_FileListNT.EndUpdate();
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowInformation("Xuất báo cáo thành công!", "Thông tin");

                string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
                doc.SaveDocument(Path.Combine(PathSave, $"Báo cáo dự án_{time}.docx"), DocumentFormat.OpenXml);
                rec_XemTruoc.LoadDocument(Path.Combine(PathSave, $"Báo cáo dự án_{time}.docx"));
                //doc.SaveDocument(Path.Combine(BaseFrom.m_path, "locvo.docx"), DocumentFormat.OpenXml);



            }
        }
    }
}