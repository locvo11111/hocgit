using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
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
    public partial class XtraForm_XemGiaoViecTongThe : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_XemGiaoViecTongThe()
        {
            InitializeComponent();
        }

        private void XtraForm_XemGiaoViecTongThe_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            bool isThiCong = rg_GV_DauViec.GetDescription() == "Thi công";
            colTenDonViThucHien.Visible = isThiCong;


            string dbString = (isThiCong)
                ? $"SELECT {GiaoViec.TBL_CONGVIECCHA}.*, " +
                    $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHM, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                    $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, " +
                    $"{MyConstant.TBL_THONGTINDUAN}.Code AS CodeDuAn, {MyConstant.TBL_THONGTINDUAN}.TenDuAn, " +
                    $"{MyConstant.view_DonViThucHien}.Ten AS TenDonViThucHien " +
                    $"FROM {GiaoViec.TBL_CONGVIECCHA} " +
                    $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                    $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code " +
                    $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                    $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code " +
                    $"JOIN {MyConstant.TBL_THONGTINDUAN} " +
                    $"ON {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code " +
                    $"JOIN {MyConstant.view_DonViThucHien} " +
                    $"ON ((COALESCE(CodeNhaThau, CodeNhaThauPhu, CodeToDoi)) = {MyConstant.view_DonViThucHien}.Code) " +
                    $"ORDER BY {MyConstant.TBL_THONGTINDUAN}.CreatedOn DESC"

                : $"SELECT {GiaoViec.TBL_CONGVIECCHA}.*, " +
                    $"{GiaoViec.TBL_DauViecNho}.Code AS CodeHM, {GiaoViec.TBL_DauViecNho}.DauViec AS TenHangMuc, " +
                    $"{GiaoViec.TBL_DauViecLon}.Code AS CodeCongTrinh, {GiaoViec.TBL_DauViecLon}.DauViec AS TenCongTrinh, " +
                    $"{MyConstant.TBL_THONGTINDUAN}.Code AS CodeDuAn, {MyConstant.TBL_THONGTINDUAN}.TenDuAn " +
                    $"FROM {GiaoViec.TBL_CONGVIECCHA} " +
                    $"JOIN {GiaoViec.TBL_DauViecNho} " +
                    $"ON {GiaoViec.TBL_CONGVIECCHA}.CodeDauMuc = {GiaoViec.TBL_DauViecNho}.Code " +
                    $"JOIN {GiaoViec.TBL_DauViecLon} " +
                    $"ON {GiaoViec.TBL_DauViecNho}.CodeDauViecLon = {GiaoViec.TBL_DauViecLon}.Code " +
                    $"JOIN {MyConstant.TBL_THONGTINDUAN} " +
                    $"ON {GiaoViec.TBL_DauViecLon}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code " +
                    $"ORDER BY {MyConstant.TBL_THONGTINDUAN}.CreatedOn DESC";/* +
                    $"JOIN {MyConstant.view_DonViThucHien} " +
                    $"ON ((COALESCE(CodeNhaThau, CodeNhaThauPhu, CodeToDoi)) = {MyConstant.view_DonViThucHien}.Code)";*/

            var dts = DataProvider.InstanceTHDA.ExecuteQueryModel<ThongTinCongTacChaExtensionViewModel>(dbString);

            var grDAs = dts.GroupBy(x => new { x.CodeDuAn, x.TenDuAn });

            var listAdd = new List<ThongTinCongTacChaExtensionViewModel>();

            foreach (var grDA in grDAs)
            {
                dts.Add(new ThongTinCongTacChaExtensionViewModel()
                {
                    CodeCongViecCha = grDA.Key.CodeDuAn,
                    MaDinhMuc = "DỰ ÁN",
                    TenCongViec = grDA.Key.TenDuAn,
                    NgayBatDau = grDA.Min(x => x.NgayBatDau),
                    NgayKetThuc = grDA.Max(x => x.NgayKetThuc),
                    NgayBatDauThiCong = grDA.Min(x => x.NgayBatDauThiCong),
                    NgayKetThucThiCong = grDA.Max(x => x.NgayKetThucThiCong),
                });

                var grCTs = grDA.GroupBy(x => new { x.CodeCongTrinh, x.TenCongTrinh });

                foreach (var grCT in grCTs)
                {
                    dts.Add(new ThongTinCongTacChaExtensionViewModel()
                    {
                        CodeCongViecCha = grCT.Key.CodeCongTrinh,
                        MaDinhMuc = isThiCong ? "CÔNG TRÌNH" : "",
                        ParentCode = grDA.Key.CodeDuAn,
                        TenCongViec = grCT.Key.TenCongTrinh,
                        NgayBatDau = grCT.Min(x => x.NgayBatDau),
                        NgayKetThuc = grCT.Max(x => x.NgayKetThuc),
                        NgayBatDauThiCong = grCT.Min(x => x.NgayBatDauThiCong),
                        NgayKetThucThiCong = grCT.Max(x => x.NgayKetThucThiCong),
                    });

                    var grHMs = grCT.GroupBy(x => new { x.CodeHM, x.TenHangMuc });

                    foreach (var grHM in grHMs)
                    {
                        dts.Add(new ThongTinCongTacChaExtensionViewModel()
                        {
                            CodeCongViecCha = grHM.Key.CodeHM,
                            MaDinhMuc = isThiCong ? "HẠNG MỤC" : "",
                            ParentCode = grCT.Key.CodeCongTrinh,
                            TenCongViec = grHM.Key.TenHangMuc,
                            NgayBatDau = grHM.Min(x => x.NgayBatDau),
                            NgayKetThuc = grHM.Max(x => x.NgayKetThuc),
                            NgayBatDauThiCong = grHM.Min(x => x.NgayBatDauThiCong),
                            NgayKetThucThiCong = grHM.Max(x => x.NgayKetThucThiCong),
                        });

                        foreach (var CT in grHM.AsEnumerable())
                        {
                            CT.ParentCode = grHM.Key.CodeHM;
                        }
                    }
                }
            }

            tl_GiaoViec.DataSource = new BindingList<ThongTinCongTacChaExtensionViewModel>(dts);
        }

        private void tl_GiaoViec_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            var node = e.Node;
            
            if (node == null) return;

            if (node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = MyConstant.color_Row_DienGiai;
            }
            else if (node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
            }
            else if (node.Level == 2)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
            }
        }

        private void rg_GV_DauViec_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void tl_GiaoViec_DataSourceChanged(object sender, EventArgs e)
        {
            tl_GiaoViec.ExpandAll();
        }

        private void bt_Export_Click(object sender, EventArgs e)
        {
            tl_GiaoViec.ShowRibbonPrintPreview();
        }
    }
}