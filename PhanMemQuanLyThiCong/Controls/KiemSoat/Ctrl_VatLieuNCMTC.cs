using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Ctrl_VatLieuNCMTC : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_VatLieuNCMTC()
        {
            InitializeComponent();
        }
        public void Fcn_LoadDVTH()
        {
            ctrl_DonViThucHienDuAn.DVTHChanged -= ctrl_DonViThucHienDuAn_DVTHChanged;
            DuAnHelper.GetDonViThucHiens(ctrl_DonViThucHienDuAn);
            ctrl_DonViThucHienDuAn.DVTHChanged += ctrl_DonViThucHienDuAn_DVTHChanged;
        }

        private enum LoaiVatLieu
        {
            VatLieu,
            NhanCong,
            MayThiCong,
            LKVatLieu,
            LKNhanCong,
            LKMayThiCong
              
        }
        public void Fcn_Update()
        {
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            de_Begin.DateTime = DateTime.Now;
            de_End.DateTime=DateTime.Now.AddDays(15);
            if (cbb_ChonCuaSo.SelectedIndex == 1)
            {
                WaitFormHelper.ShowWaitForm("Đang cập nhật Vật liệu, Vui lòng chờ!");
                var VLNew = GetDataHangNgay(DVTH, "Vật liệu", de_Begin.DateTime, de_End.DateTime);
                ctrl_ChartKhoiLuongThanhTienVL.PushData(VLNew, SoNgay,DVTH.IsGiaoThau);
                scc_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Panel1;
                scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel1;
                WaitFormHelper.CloseWaitForm();
            }
            else
                cbb_ChonCuaSo.SelectedIndex = 1;


        }
        private void Fcn_LoadDongTien()
        {
            DonViThucHien DVTH =ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu toàn Dự án, Vui lòng chờ!");
                    gridTenGiaTriMTC.TypeVLMTCNC = true;
                    gridTenGiaTriVL.TypeVLMTCNC = true;
                    gridTenGiaTriNC.TypeVLMTCNC = true;
                    gridTenGiaTriVL.DataSource = GetAllThanhTienVatLieu(DVTH, "Vật liệu");
                    gridTenGiaTriVL.ExpandAll();
                    gridTenGiaTriMTC.DataSource = GetAllThanhTienVatLieu(DVTH, "Máy thi công");
                    gridTenGiaTriMTC.ExpandAll();
                    gridTenGiaTriNC.DataSource = GetAllThanhTienVatLieu(DVTH, "Nhân công");
                    gridTenGiaTriNC.ExpandAll();
                    scc_VL.PanelVisibility = SplitPanelVisibility.Both;
                    scc_MTC.PanelVisibility = SplitPanelVisibility.Both;
                    scc_NC.PanelVisibility = SplitPanelVisibility.Both;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 1:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Vật liệu, Vui lòng chờ!");
                    gridTenGiaTriVL.TypeVLMTCNC = true;
                    gridTenGiaTriVL.DataSource = GetAllThanhTienVatLieu(DVTH, "Vật liệu");
                    gridTenGiaTriVL.ExpandAll();
                    scc_VL.PanelVisibility = SplitPanelVisibility.Both;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 2:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Nhân công, Vui lòng chờ!");
                    gridTenGiaTriNC.TypeVLMTCNC = true;
                    gridTenGiaTriNC.DataSource = GetAllThanhTienVatLieu(DVTH, "Nhân công");
                    gridTenGiaTriNC.ExpandAll();
                    scc_NC.PanelVisibility = SplitPanelVisibility.Both;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 3:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Máy thi công, Vui lòng chờ!");
                    gridTenGiaTriMTC.TypeVLMTCNC = true;
                    gridTenGiaTriMTC.DataSource = GetAllThanhTienVatLieu(DVTH, "Máy thi công");
                    gridTenGiaTriMTC.ExpandAll();
                    scc_MTC.PanelVisibility = SplitPanelVisibility.Both;
                    WaitFormHelper.CloseWaitForm();
                    break;
                default:
                    break;
            }
        }
        private void Fcn_LoadBieuDo()
        {
            ce_HienChiTiet.Checked = false;
            ce_BieuDo.Checked = true;
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu toàn Dự án, Vui lòng chờ!");
                    var VL = GetDataHangNgay(DVTH, "Vật liệu", de_Begin.DateTime, de_End.DateTime);
                    var MTC = GetDataHangNgay(DVTH, "Máy thi công", de_Begin.DateTime, de_End.DateTime);
                    var NC = GetDataHangNgay(DVTH, "Nhân công", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienVL.PushData(VL,SoNgay, DVTH.IsGiaoThau);
                    ctrl_ChartKhoiLuongThanhTienMTC.PushData(MTC, SoNgay, DVTH.IsGiaoThau);
                    ctrl_ChartKhoiLuongThanhTienNC.PushData(NC, SoNgay, DVTH.IsGiaoThau);
                    scc_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_MTC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_NC.PanelVisibility = SplitPanelVisibility.Panel2;

                    scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Both;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Both;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 1:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Vật liệu, Vui lòng chờ!");
                    var VLNew = GetDataHangNgay(DVTH, "Vật liệu", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienVL.PushData(VLNew, SoNgay, DVTH.IsGiaoThau);
                    scc_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel1;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 2:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Nhân công, Vui lòng chờ!");
                    var NC_new = GetDataHangNgay(DVTH, "Nhân công", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienNC.PushData(NC_new, SoNgay, DVTH.IsGiaoThau);
                    scc_NC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel2;
                    WaitFormHelper.CloseWaitForm();

                    break;
                case 3:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Máy thi công, Vui lòng chờ!");
                    var MTC_new = GetDataHangNgay(DVTH, "Máy thi công", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienMTC.PushData(MTC_new, SoNgay, DVTH.IsGiaoThau);
                    scc_MTC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel1;
                    WaitFormHelper.CloseWaitForm();
                    break;
                default:
                    break;
            }
        }
        private static List<KLHN> GetDataHangNgay(DonViThucHien DVTH, string LoaiVatTu,DateTime NBD=default,DateTime NKT=default)
        {
            if (DVTH is null)
                return null;
            List<KLHN> dtKLHN = new List<KLHN>();

            string dbString = $"SELECT {TDKH.TBL_KHVT_VatTu}.*,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCongTac,{TDKH.TBL_KHVT_VatTu}.DonGia AS DonGiaKeHoach," +
$" {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,  " +
$"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
$"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
$"FROM {TDKH.TBL_KHVT_VatTu} " +
$"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
$"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
$"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
$"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_KHVT_VatTu}.{DVTH.ColCodeFK}='{DVTH.Code}' AND {TDKH.TBL_KHVT_VatTu}.LoaiVatTu='{LoaiVatTu}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lstcode = dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            DataTable dtCon = new DataTable();
            if(NBD!=default)
                return MyFunction.Fcn_CalKLKHNew(TypeKLHN.VatLieu, lstcode,dateBD:NBD,dateKT:NKT);
            else
                return MyFunction.Fcn_CalKLKHNewWithoutKeHoach(TypeKLHN.VatLieu, lstcode);
        }
        private static List<KLTTHangNgay> GetAllThanhTienVatLieu(DonViThucHien DVTH, string LoaiVatTu)
        {
            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();
            string dbString = $"SELECT {TDKH.TBL_KHVT_VatTu}.*,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCongTac,{TDKH.TBL_KHVT_VatTu}.DonGia AS DonGiaKeHoach," +
$" {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,  " +
$"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
$"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
$"FROM {TDKH.TBL_KHVT_VatTu} " +
$"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
$"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
$"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
$"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_KHVT_VatTu}.{DVTH.ColCodeFK}='{DVTH.Code}' AND {TDKH.TBL_KHVT_VatTu}.LoaiVatTu='{LoaiVatTu}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataTable dtCon = new DataTable();
            List<KLTTHangNgay> lsSourceKHVTCon = new List<KLTTHangNgay>();
            List<KLTTHangNgay> lsSourceKHVT = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            string[] lstcode = dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();

            var dtTheoNgay = MyFunction.Fcn_CalKLKHModel(TypeKLHN.VatLieu, lstcode, dateKT:DateTime.Now.Date);


            if (lsSourceKHVT.Count() == 0)
                return null;
            var grsCTrinh = lsSourceKHVT.GroupBy(x => x.CodeCongTrinh);
            foreach (var grCTrinh in grsCTrinh)
            {
                string CodeCT = Guid.NewGuid().ToString();
                lsOut.Add
                    (
                        new KLTTHangNgay()
                        {
                            ParentCode = "0",
                            Code = CodeCT,
                            TenCongTac = grCTrinh.First().TenCongTrinh,
                        }
                    );
                var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                foreach (var grHM in grsHM)
                {
                    string CodeHM = Guid.NewGuid().ToString();
                    lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = CodeCT,
                                Code = CodeHM,
                                TenCongTac = grHM.First().TenHangMuc,
                            }
                        );
                    var grsCTacs = grHM.GroupBy(x => x.CodeCha);
                    foreach (var grCTac in grsCTacs)
                    {
                        string CodeCongtac = Guid.NewGuid().ToString();
                        long TTTC = 0;
                        lsOut.Add
                     (
                         new KLTTHangNgay()
                         {
                             ParentCode = CodeHM,
                             Code = CodeCongtac,
                             TenCongTac = grCTac.First().TenCongTac,
                         }
                     );
                        if (dtTheoNgay!=null)
                        {
                            if (DVTH.Table == MyConstant.TBL_THONGTINNHATHAU)
                            {
                                List<KLTTHangNgay> Crowcon = lsSourceKHVTCon.Where(x => x.CodeHangMuc == grHM.Key && x.MaCongTac == grCTac.First().MaCongTac && x.DonVi == grCTac.First().DonVi && x.TenCongTac == grCTac.First().TenCongTac).ToList();
                                if (Crowcon.Count() != 0)
                                {
                                    string[] CodeGoc = Crowcon.Select(x => x.CodeCha).ToArray();
                                    List<KLTTHangNgay> CrowconHN = dtTheoNgay.Where(x => CodeGoc.Contains(x.CodeCha)).ToList();
                                    TTTC = CrowconHN.Any() ? (long)CrowconHN.Sum(x => x.ThanhTienThiCong) : 0;
                                }

                            }
                            else
                            {
                                TTTC = dtTheoNgay.Where(x => x.CodeCha == grCTac.Key).Any() ? (long)dtTheoNgay.Where(x => x.CodeCha == grCTac.Key).Sum(x => x.ThanhTienThiCong) : 0;
                            }
                        }

                        lsOut.Add
                         (
                             new KLTTHangNgay()
                             {
                                 ParentCode = CodeCongtac,
                                 Code = Guid.NewGuid().ToString(),
                                 TenCongTac = "1. Kế hoạch",
                                 GiaTri = grCTac.First().ThanhTienKeHoach
                             }
                         );
                        lsOut.Add
                           (
                               new KLTTHangNgay()
                               {
                                   ParentCode = CodeCongtac,
                                   Code = Guid.NewGuid().ToString(),
                                   TenCongTac = "2. Thi công",
                                   GiaTri = TTTC
                               }
                           );

                        lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = CodeCongtac,
                                Code = Guid.NewGuid().ToString(),
                                TenCongTac = "3. Còn lại",
                                GiaTri = grCTac.First().ThanhTienKeHoach - TTTC
                            }
                        );
                    }
                }
            }
                return lsOut;
        }

        private void ctrl_DonViThucHienDuAn_DVTHChanged(object sender, EventArgs e)
        {
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            if (DVTH is null)
                return;
            WaitFormHelper.ShowWaitForm("Đang cập nhập dữ liệu", "Vui Lòng chờ!");
            //if(ce_HienChiTiet.Checked)
            //    Fcn_LoadDongTien();
            Fcn_LoadBieuDo();
            WaitFormHelper.CloseWaitForm();
        }

        private void scc_VL_SizeChanged(object sender, EventArgs e)
        {
            scc_VL.SplitterPosition = scc_VL.Width / 3;
        }

        private void scc_MTC_SizeChanged(object sender, EventArgs e)
        {
            scc_MTC.SplitterPosition = scc_MTC.Width / 3;
        }

        private void scc_NC_SizeChanged(object sender, EventArgs e)
        {
            scc_NC.SplitterPosition = scc_NC.Width / 3;
        }

        private void scc_MTC_VL_SizeChanged(object sender, EventArgs e)
        {
            scc_MTC_VL.SplitterPosition = scc_MTC_VL.Height / 2;
        }

        private void scc_NCMTC_SizeChanged(object sender, EventArgs e)
        {
            scc_NCMTC.SplitterPosition = scc_NCMTC.Height*2 / 3;
        }

        private void ce_HienChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            if (!ce_BieuDo.Checked)
            {
                ce_HienChiTiet.Checked = true;
                return;
            }

            if (ce_HienChiTiet.Checked)
            {
                WaitFormHelper.ShowWaitForm("Đang cập nhập dữ liệu", "Vui Lòng chờ!");
                Fcn_LoadDongTien();
                WaitFormHelper.CloseWaitForm();
            }
            else
            {
                scc_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_MTC.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NC.PanelVisibility = SplitPanelVisibility.Panel2;
            }
        }

        private void ce_BieuDo_CheckedChanged(object sender, EventArgs e)
        {
            if (!ce_HienChiTiet.Checked)
            {
                ce_BieuDo.Checked = true;
                return;
            }
            if (ce_BieuDo.Checked)
            {

                scc_VL.PanelVisibility = SplitPanelVisibility.Both;
                scc_MTC.PanelVisibility = SplitPanelVisibility.Both;
                scc_NC.PanelVisibility = SplitPanelVisibility.Both;
            }
            else
            {
                scc_VL.PanelVisibility = SplitPanelVisibility.Panel1;
                scc_MTC.PanelVisibility = SplitPanelVisibility.Panel1;
                scc_NC.PanelVisibility = SplitPanelVisibility.Panel1;
            }
        }
        private void Fcn_UpdateCheckNgay()
        {
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu toàn Dự án, Vui lòng chờ!");
                    var VL = GetDataHangNgay(DVTH, "Vật liệu",de_Begin.DateTime,de_End.DateTime);
                    var MTC = GetDataHangNgay(DVTH, "Máy thi công", de_Begin.DateTime, de_End.DateTime);
                    var NC = GetDataHangNgay(DVTH, "Nhân công", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienVL.PushData(VL, SoNgay, DVTH.IsGiaoThau);
                    ctrl_ChartKhoiLuongThanhTienMTC.PushData(MTC, SoNgay, DVTH.IsGiaoThau);
                    ctrl_ChartKhoiLuongThanhTienNC.PushData(NC, SoNgay, DVTH.IsGiaoThau);
                    scc_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_MTC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_NC.PanelVisibility = SplitPanelVisibility.Panel2;

                    scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Both;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Both;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 1:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Vật liệu, Vui lòng chờ!");
                    var VLNew = GetDataHangNgay(DVTH, "Vật liệu", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienVL.PushData(VLNew, SoNgay, DVTH.IsGiaoThau);
                    scc_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel1;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 2:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Nhân công, Vui lòng chờ!");
                    var NC_new = GetDataHangNgay(DVTH, "Nhân công", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienNC.PushData(NC_new, SoNgay, DVTH.IsGiaoThau);
                    scc_NC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel2;
                    WaitFormHelper.CloseWaitForm();

                    break;
                case 3:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Máy thi công, Vui lòng chờ!");
                    var MTC_new = GetDataHangNgay(DVTH, "Máy thi công", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienMTC.PushData(MTC_new, SoNgay, DVTH.IsGiaoThau);
                    scc_MTC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel1;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 4:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Dữ liệu tổng hợp, Vui lòng chờ!");
                    ce_HienChiTiet.Enabled = ce_BieuDo.Enabled = false;
                    lci_ChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lci_Tonghop.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    var VLTH = GetDataHangNgay(DVTH, "Vật liệu", de_Begin.DateTime, de_End.DateTime);
                    var MTCTH = GetDataHangNgay(DVTH, "Máy thi công", de_Begin.DateTime, de_End.DateTime);
                    var NCTH = GetDataHangNgay(DVTH, "Nhân công", de_Begin.DateTime, de_End.DateTime);
                    List<Chart_KhoiLuongThanhTien> VLLK = PushData(VLTH, SoNgay, false, DVTH.IsGiaoThau);
                    List<Chart_KhoiLuongThanhTien> NCLK = PushData(NCTH, SoNgay, false, DVTH.IsGiaoThau);
                    List<Chart_KhoiLuongThanhTien> MTCLK = PushData(MTCTH, SoNgay, false, DVTH.IsGiaoThau);
                    List<KLHN> All = new List<KLHN>();
                    All.AddRange(VLTH);
                    All.AddRange(MTCTH);
                    All.AddRange(NCTH);
                    List<Chart_KhoiLuongThanhTien> TH = PushData(All, SoNgay, true, DVTH.IsGiaoThau);
                    cc_BieuDoTongHop.Series[0].DataSource = TH;
                    cc_BieuDoTongHop.Series[1].DataSource = TH;
                    cc_BieuDoTongHop.Series[2].DataSource = VLLK;
                    cc_BieuDoTongHop.Series[5].DataSource = VLLK;
                    cc_BieuDoTongHop.Series[3].DataSource = NCLK;
                    cc_BieuDoTongHop.Series[6].DataSource = NCLK;
                    cc_BieuDoTongHop.Series[4].DataSource = MTCLK;
                    cc_BieuDoTongHop.Series[7].DataSource = MTCLK;
                    WaitFormHelper.CloseWaitForm();
                    break;
                default:
                    break;
            }
        }
        private void cbb_ChonCuaSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ce_HienChiTiet.Checked = false;
            ce_BieuDo.Checked = true;
            ce_HienChiTiet.Enabled = ce_BieuDo.Enabled = true;
            lci_Tonghop.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lci_ChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            ce_ChonNgay.Checked = false;
            lci_Begin.Enabled = lci_End.Enabled = false;
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            switch (cbb_ChonCuaSo.SelectedIndex)
            {                
                case 0:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu toàn Dự án, Vui lòng chờ!");
                    var VL = GetDataHangNgay(DVTH, "Vật liệu", de_Begin.DateTime, de_End.DateTime);
                    var MTC = GetDataHangNgay(DVTH, "Máy thi công", de_Begin.DateTime, de_End.DateTime);
                    var NC = GetDataHangNgay(DVTH, "Nhân công", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienVL.PushData(VL, SoNgay, DVTH.IsGiaoThau);
                    ctrl_ChartKhoiLuongThanhTienMTC.PushData(MTC, SoNgay, DVTH.IsGiaoThau);
                    ctrl_ChartKhoiLuongThanhTienNC.PushData(NC, SoNgay, DVTH.IsGiaoThau);
                    scc_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_MTC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_NC.PanelVisibility = SplitPanelVisibility.Panel2;

                    scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Both;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Both;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 1:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Vật liệu, Vui lòng chờ!");
                    var VLNew = GetDataHangNgay(DVTH, "Vật liệu", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienVL.PushData(VLNew, SoNgay, DVTH.IsGiaoThau);
                    scc_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel1;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 2:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Nhân công, Vui lòng chờ!");
                    var NC_new = GetDataHangNgay(DVTH, "Nhân công", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienNC.PushData(NC_new, SoNgay, DVTH.IsGiaoThau);
                    scc_NC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel2;
                    WaitFormHelper.CloseWaitForm();

                    break;
                case 3:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Máy thi công, Vui lòng chờ!");
                    var MTC_new = GetDataHangNgay(DVTH, "Máy thi công", de_Begin.DateTime, de_End.DateTime);
                    ctrl_ChartKhoiLuongThanhTienMTC.PushData(MTC_new, SoNgay, DVTH.IsGiaoThau);
                    scc_MTC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_MTC_VL.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_NCMTC.PanelVisibility = SplitPanelVisibility.Panel1;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 4:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật Dữ liệu tổng hợp, Vui lòng chờ!");
                    ce_HienChiTiet.Enabled = ce_BieuDo.Enabled = false;
                    lci_ChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lci_Tonghop.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    var VLTH = GetDataHangNgay(DVTH, "Vật liệu", de_Begin.DateTime, de_End.DateTime);
                    var MTCTH = GetDataHangNgay(DVTH, "Máy thi công", de_Begin.DateTime, de_End.DateTime);
                    var NCTH = GetDataHangNgay(DVTH, "Nhân công", de_Begin.DateTime, de_End.DateTime);
                    List<Chart_KhoiLuongThanhTien> VLLK = PushData(VLTH, SoNgay, false, DVTH.IsGiaoThau);
                    List<Chart_KhoiLuongThanhTien> NCLK = PushData(NCTH, SoNgay, false, DVTH.IsGiaoThau);
                    List<Chart_KhoiLuongThanhTien> MTCLK = PushData(MTCTH, SoNgay, false, DVTH.IsGiaoThau);
                    List<KLHN> All = new List<KLHN>();
                    All.AddRange(VLTH);
                    All.AddRange(MTCTH);
                    All.AddRange(NCTH);
                    List<Chart_KhoiLuongThanhTien> TH = PushData(All, SoNgay, true, DVTH.IsGiaoThau);
                    cc_BieuDoTongHop.Series[0].DataSource = TH;
                    cc_BieuDoTongHop.Series[1].DataSource = TH;
                    cc_BieuDoTongHop.Series[2].DataSource = VLLK;
                    cc_BieuDoTongHop.Series[5].DataSource = VLLK;
                    cc_BieuDoTongHop.Series[3].DataSource = NCLK;
                    cc_BieuDoTongHop.Series[6].DataSource = NCLK;
                    cc_BieuDoTongHop.Series[4].DataSource = MTCLK;
                    cc_BieuDoTongHop.Series[7].DataSource = MTCLK;
                    WaitFormHelper.CloseWaitForm();
                    break;
                default:
                    break;
            }
        }

        private void Ctrl_VatLieuNCMTC_Load(object sender, EventArgs e)
        {
            //DuAnHelper.GetDonViThucHiens(ctrl_DonViThucHienDuAn);
        }
        public bool CheckNgay { get { return ce_ChonNgay.Checked; } }
        public int SoNgay { get { if (Index == 2) return 0; else return (int)se_songay.Value; } set { se_songay.Value = value; } }
        public bool TrangThai { get { return se_songay.Enabled; } set { se_songay.Enabled = value; } }
        public int Index { get { return rg_CaiDat.SelectedIndex; } set { rg_CaiDat.SelectedIndex = value; } }
        public void Fcn_LoadDuLieuCapNhap()
        {
            if (ce_ChonNgay.Checked)
                Fcn_UpdateCheckNgay();
            else
                Fcn_Update();
        }
        private void Sb_Updae_Click(object sender, EventArgs e)
        {


        }
        public event EventHandler sb_capNhap_Click
        {
            add
            {
                Sb_Updae.Click += value;
            }
            remove
            {
                Sb_Updae.Click -= value;
            }
        }

        private void rg_CaiDat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = rg_CaiDat.SelectedIndex;
            if (index != 0)
            {
                se_songay.Enabled = false;
                if (index == 1)
                    se_songay.Value = 7;
            }
            else
                se_songay.Enabled = true;
        }
        public List<Chart_KhoiLuongThanhTien> PushData(List<KLHN> lsSourcete, int Ngay,bool All,bool IsGiaoThau)
        {
            if (lsSourcete.Count() == 0)
            {

                return null;
            }
            if (lsSourcete.Count == 0)
                return null;
            List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();
            List<Chart_KhoiLuongThanhTien> lsOutCondition = new List<Chart_KhoiLuongThanhTien>();
            var grsDate = lsSourcete.GroupBy(x => x.Ngay).OrderBy(x => x.Key);
            double luyKeTTKeHoach = 0;
            double luyKeTTThiCong = 0;
            foreach (var item in grsDate)
            {
                double ThanhTienKH = (double)item.Sum(x => x.ThanhTienKeHoach);
                double ThanhTienTC = (double) item.Sum(x => x.ThanhTienThiCong);
                luyKeTTKeHoach += ThanhTienKH;
                luyKeTTThiCong += ThanhTienTC;
                lsOut.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    LuyKeThanhTienKeHoach = (long)Math.Round(luyKeTTKeHoach),
                    LuyKeThanhTienThiCong = (long)Math.Round(luyKeTTThiCong),
                    ThanhTienKeHoach = (long)Math.Round(ThanhTienKH),
                    ThanhTienThiCong = (long)Math.Round(ThanhTienTC)

                });
            }
            DateTime Min = lsOut.Min(x => x.Date);
            DateTime Max = lsOut.Max(x => x.Date);
            XYDiagram diagram = (XYDiagram)cc_BieuDoTongHop.Diagram;
            if (All)
            {
                if (Ngay >= 1)
                {
                    diagram.AxisX.WholeRange.SideMarginsValue = 1;
                    diagram.AxisX.WholeRange.SetMinMaxValues(Min, Max);
                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
                    diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
                    diagram.AxisX.DateTimeScaleOptions.GridSpacing = Ngay;
                }
            }



            if (Ngay == 1)
                lsOutCondition = lsOut;
            else if (Ngay > 1)
            {
                long LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == Min).FirstOrDefault().LuyKeThanhTienKeHoach;
                long LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == Min).FirstOrDefault().LuyKeThanhTienThiCong;
                long ThanhTienKeHoach = 0;
                long ThanhTienThiCong = 0;
                lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = Min,
                    LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                    LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                    ThanhTienKeHoach = LuyKeThanhTienKeHoach,
                    ThanhTienThiCong = LuyKeThanhTienThiCong

                });
                for (DateTime i = Min.AddDays(Ngay); i <= Max; i = i.AddDays(Ngay))
                {
                    LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : lsOut.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThanhTienKeHoach;
                    LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeThanhTienThiCong : lsOut.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThanhTienThiCong;
                    DateTime Pre = i.AddDays(-Ngay);
                    ThanhTienKeHoach = lsOut.FindAll(x => x.Date > Pre && x.Date <= i).Sum(x => x.ThanhTienKeHoach);
                    ThanhTienThiCong = lsOut.FindAll(x => x.Date > Pre && x.Date <= i).Sum(x => x.ThanhTienThiCong);
                    lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = i,
                        LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                        LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                        ThanhTienKeHoach = ThanhTienKeHoach,
                        ThanhTienThiCong = ThanhTienThiCong

                    });
                    if (i.AddDays(Ngay) >= Max)
                    {
                        LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : lsOut.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienKeHoach;
                        LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienThiCong : lsOut.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienThiCong;
                        ThanhTienKeHoach = lsOut.FindAll(x => x.Date > i && x.Date <= Max).Sum(x => x.ThanhTienKeHoach);
                        ThanhTienThiCong = lsOut.FindAll(x => x.Date > i && x.Date <= Max).Sum(x => x.ThanhTienThiCong);
                        lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = Max,
                            LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                            LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                            ThanhTienKeHoach = ThanhTienKeHoach,
                            ThanhTienThiCong = ThanhTienThiCong

                        });
                        return lsOutCondition;
                    }

                }
            }
            else
            {
                if (All)
                {
                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                    diagram.AxisX.DateTimeScaleOptions.GridSpacing = 1;
                    diagram.AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Manual;
                    diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month;
                }
                List<DateTime> Month = new List<DateTime>();
                int tongThang = (Max.Year - Min.Year) * 12 + Max.Month - Min.Month;
                long LuyKeThanhTienKeHoach = 0;
                long LuyKeThanhTienThiCong = 0;
                long ThanhTienKeHoach = 0;
                long ThanhTienThiCong = 0;
                for (int i = 0; i <= tongThang; i++)
                {
                    DateTime Add = Min.AddMonths(i);
                    DateTime Begin = new DateTime(Add.Year, Add.Month, 1);
                    DateTime MaxDay = new DateTime(Add.Year, Add.Month, DateTime.DaysInMonth(Add.Year, Add.Month));
                    if (Add.Month == Max.Month && Add.Year == Max.Year)
                    {
                        LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : lsOut.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienKeHoach;
                        LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienThiCong : lsOut.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienThiCong;
                        ThanhTienKeHoach = lsOut.FindAll(x => x.Date >= Begin && x.Date <= Max).Sum(x => x.ThanhTienKeHoach);
                        ThanhTienThiCong = lsOut.FindAll(x => x.Date >= Begin && x.Date <= Max).Sum(x => x.ThanhTienThiCong);
                        lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = Begin,
                            LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                            LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                            ThanhTienKeHoach = ThanhTienKeHoach,
                            ThanhTienThiCong = ThanhTienThiCong

                        });
                        return lsOutCondition;
                    }
                    LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : lsOut.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThanhTienKeHoach;
                    LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeThanhTienThiCong : lsOut.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThanhTienThiCong;
                    ThanhTienKeHoach = lsOut.FindAll(x => x.Date >= Begin && x.Date <= MaxDay).Sum(x => x.ThanhTienKeHoach);
                    ThanhTienThiCong = lsOut.FindAll(x => x.Date >= Begin && x.Date <= MaxDay).Sum(x => x.ThanhTienThiCong);
                    lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = Begin,
                        LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                        LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                        ThanhTienKeHoach = ThanhTienKeHoach,
                        ThanhTienThiCong = ThanhTienThiCong

                    });
                }
            }
            cc_BieuDoTongHop.DataSource = lsOutCondition;
            return lsOutCondition;

        }
        private void ce_ChonNgay_CheckedChanged(object sender, EventArgs e)
        {
            string dbString = "";
            DataTable dttc = new DataTable();
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            if (ce_ChonNgay.Checked)
            {
                lci_Begin.Enabled = lci_End.Enabled = true;
                de_Begin.DateTime = de_End.DateTime = DateTime.Now;
                WaitFormHelper.ShowWaitForm("Đang cập nhập dữ liệu cho toàn Dự án, Vui lòng chờ!");
                dbString = $"SELECT {TDKH.TBL_KHVT_VatTu}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
                $"FROM {TDKH.TBL_KHVT_VatTu} " +
                $"LEFT JOIN {TDKH.TBL_KHVT_KhoiLuongHangNgay} " +
                $"ON {TDKH.TBL_KHVT_VatTu}.Code = {TDKH.TBL_KHVT_KhoiLuongHangNgay}.CodeVatTu " +
                $"WHERE {TDKH.TBL_KHVT_VatTu}.{DVTH.ColCodeFK} = '{DVTH.Code}' GROUP BY {TDKH.TBL_KHVT_VatTu}.Code  ";
                dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dttc.Rows.Count != 0)
                {
                    DateTime Min_KH = dttc.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                    DateTime Max_KH = dttc.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                    de_End.DateTime = Max_KH;
                    de_Begin.DateTime = Min_KH;
                    DateTime Max_TC = dttc.AsEnumerable().Where(x => x["MaxNgayThiCong"] != DBNull.Value).Any()?
                        dttc.AsEnumerable().Where(x => x["MaxNgayThiCong"] != DBNull.Value).Max(x => DateTime.Parse(x["MaxNgayThiCong"].ToString())):default;
                    //DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCo;ng"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default
                    de_End.DateTime = Max_KH >= Max_TC ? Max_KH : Max_TC;
                    //DateTime Min_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                    DateTime Min_TC = dttc.AsEnumerable().Where(x => x["MinNgayThiCong"] != DBNull.Value).Any()?
                        dttc.AsEnumerable().Where(x => x["MinNgayThiCong"] != DBNull.Value).Min(x => DateTime.Parse(x["MinNgayThiCong"].ToString())):default;
                    de_Begin.DateTime = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
                }
                WaitFormHelper.CloseWaitForm();
//                switch (cbb_ChonCuaSo.SelectedIndex)
//                {
//                    case 0:

//                        break;
//                    case 1:
//                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Giao thầu, Vui lòng chờ!");
//                        dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
//$"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
//$"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
//$"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
//$"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThau IS NOT NULL ";
//                        dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
//                        if (dttc.Rows.Count != 0)
//                        {
//                            DateTime Min_KH = dttc.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
//                            DateTime Max_KH = dttc.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
//                            de_End.DateTime = Max_KH;
//                            de_Begin.DateTime = Min_KH;
//                            DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
//                            de_End.DateTime = Max_KH >= Max_TC ? Max_KH : Max_TC;
//                            DateTime Min_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
//                            de_Begin.DateTime = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
//                        }
//                        WaitFormHelper.CloseWaitForm();
//                        break;
//                    case 2:
//                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà thầu phụ, Vui lòng chờ!");
//                        dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
//$"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
//$"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
//$"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
//$"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThauPhu IS NOT NULL ";
//                        dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
//                        if (dttc.Rows.Count != 0)
//                        {
//                            DateTime Min_KH = dttc.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
//                            DateTime Max_KH = dttc.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
//                            de_End.DateTime = Max_KH;
//                            de_Begin.DateTime = Min_KH;
//                            DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
//                            de_End.DateTime = Max_KH >= Max_TC ? Max_KH : Max_TC;
//                            DateTime Min_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
//                            de_Begin.DateTime = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
//                        }
//                        WaitFormHelper.CloseWaitForm();
//                        break;
//                    case 3:
//                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Tổ đội, Vui lòng chờ!");
//                        dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
//$"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
//$"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
//$"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
//$"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeToDoi IS NOT NULL ";
//                        dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
//                        if (dttc.Rows.Count != 0)
//                        {
//                            DateTime Min_KH = dttc.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
//                            DateTime Max_KH = dttc.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
//                            de_End.DateTime = Max_KH;
//                            de_Begin.DateTime = Min_KH;
//                            DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
//                            de_End.DateTime = Max_KH >= Max_TC ? Max_KH : Max_TC;
//                            DateTime Min_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
//                            de_Begin.DateTime = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
//                        }
//                        WaitFormHelper.CloseWaitForm();
//                        break;
//                    default:
//                        break;
//                }
            }
            else
                lci_Begin.Enabled = lci_End.Enabled = false;
        }
    }
}
