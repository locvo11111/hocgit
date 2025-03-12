using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using PhanMemQuanLyThiCong.Common.Constant;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Ctrl_BaoCaoDongTien : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_BaoCaoDongTien()
        {
            InitializeComponent();
        }

        private void Ctrl_BaoCaoDongTien_Load(object sender, EventArgs e)
        {


        }
        public void LoadDongTien()
        {
            Type = TypeBaoCao.DongTien;
            de_Begin.DateTime = DateTime.Now;
            de_End.DateTime = DateTime.Now.AddDays(15);
            if (cbb_ChonCuaSo.SelectedIndex == 1)
            {
                if (Type == TypeBaoCao.DongTien)
                {
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Giao thầu, Vui lòng chờ!");
                    ctrl_NhaThau.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThau.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_NhaThau.FcnLoadDataGrid(TypeDVTH.NhaThau, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                    WaitFormHelper.CloseWaitForm();
                }
                else
                {
                    WaitFormHelper.ShowWaitForm("Đang cập nhật khối lượng Giao thầu, Vui lòng chờ!");
                    gtt_NhaThau.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThau, TypeBaoCao.KhoiLuong);
                    gtt_NhaThau.ExpandAll();
                    WaitFormHelper.CloseWaitForm();
                }

            }
            else
                cbb_ChonCuaSo.SelectedIndex = 1;
        }

        public void LoadKhoiLuong()
        {
            Type = TypeBaoCao.KhoiLuong;
            if (cbb_ChonCuaSo.SelectedIndex == 1)
            {
                if (Type == TypeBaoCao.DongTien)
                {
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Giao thầu, Vui lòng chờ!");
                    gtt_NhaThau.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThau, TypeBaoCao.DongTien);
                    gtt_NhaThau.ExpandAll();
                    ctrl_NhaThau.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThau.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_NhaThau.FcnLoadDataGrid(TypeDVTH.NhaThau, SoNgay,de_Begin.DateTime.Date, de_End.DateTime.Date);
                    WaitFormHelper.CloseWaitForm();
                }
                else
                {
                    WaitFormHelper.ShowWaitForm("Đang cập nhật khối lượng Giao thầu, Vui lòng chờ!");
                    gtt_NhaThau.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThau, TypeBaoCao.KhoiLuong);
                    gtt_NhaThau.ExpandAll();
                    WaitFormHelper.CloseWaitForm();
                }

            }
            else
                cbb_ChonCuaSo.SelectedIndex = 1;

            scc_NhaThau.PanelVisibility = SplitPanelVisibility.Panel1;
            scc_ToDoi.PanelVisibility = SplitPanelVisibility.Panel1;
            scc_NCC.PanelVisibility = SplitPanelVisibility.Panel1;
            scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Panel1;
            ce_KhoiLuong.Checked = true;
            ce_BieuDo.Checked = false;
            ce_BieuDo.Enabled = false;
            ce_XuHuong.Enabled = false;
            lc_CaiDat.Visibility =lci_CheckNgay.Visibility=DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lc_SoNgay.Visibility =lci_Begin.Visibility=lci_End.Visibility= DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lc_ChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        }
        private void Fcn_UpdateKL()
        {
            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhập dữ liệu chi tiết cho toàn Dự án, Vui lòng chờ!");
                        gtt_NhaCungCap.Type = true;
                        gtt_NhaThau.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThau, TypeBaoCao.DongTien);
                        gtt_NhaThauPhu.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThauPhu, TypeBaoCao.DongTien);
                        gtt_ToDoiThiCong.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.ToDoiThiCong, TypeBaoCao.DongTien);
                        gtt_NhaCungCap.DataSource = TongHopHelper.GetAllThanhTienVatLieuTest(TypeDVTH.NhaCungCap, TypeBaoCao.DongTien);
                        gtt_NhaThau.ExpandAll();
                        gtt_NhaThauPhu.ExpandAll();
                        gtt_ToDoiThiCong.ExpandAll();
                        gtt_NhaCungCap.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }
                    break;
                case 1:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu chi tiết Giao thầu, Vui lòng chờ!");
                        gtt_NhaThau.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThau, TypeBaoCao.DongTien);
                        gtt_NhaThau.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }
                    break;
                case 2:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu chi tiết Nhà thầu phụ, Vui lòng chờ!");
                        gtt_NhaThauPhu.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThauPhu, TypeBaoCao.DongTien);
                        gtt_NhaThauPhu.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }

                    break;
                case 3:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu chi tiết Tổ đội, Vui lòng chờ!");
                        gtt_ToDoiThiCong.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.ToDoiThiCong, TypeBaoCao.DongTien);
                        gtt_ToDoiThiCong.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }

                    break;
                case 4:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu chi tiết Nhà cung cấp, Vui lòng chờ!");
                        gtt_NhaCungCap.Type = true;
                        gtt_NhaCungCap.DataSource = TongHopHelper.GetAllThanhTienVatLieuTest(TypeDVTH.NhaCungCap, TypeBaoCao.DongTien);
                        gtt_NhaCungCap.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }
                    break;
                default:
                    break;
            }
        }
        private void cbb_ChonCuaSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ce_ChonNgay.Checked = false;
            lci_Begin.Enabled = lci_End.Enabled = false;
            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        scc_NhaThau.PanelVisibility = SplitPanelVisibility.Panel2;
                        scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Panel2;
                        scc_ToDoi.PanelVisibility = SplitPanelVisibility.Panel2;
                        scc_NCC.PanelVisibility = SplitPanelVisibility.Panel2;
                        ce_KhoiLuong.Checked = false;
                        WaitFormHelper.ShowWaitForm("Đang cập nhập dữ liệu cho toàn Dự án, Vui lòng chờ!");
                        gtt_NhaCungCap.Type = true;
                        gtt_NhaThau.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThau, TypeBaoCao.DongTien);
                        gtt_NhaThauPhu.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThauPhu, TypeBaoCao.DongTien);
                        gtt_ToDoiThiCong.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.ToDoiThiCong, TypeBaoCao.DongTien);
                        gtt_NhaCungCap.DataSource = TongHopHelper.GetAllThanhTienVatLieuTest(TypeDVTH.NhaCungCap, TypeBaoCao.DongTien);

                        ctrl_NhaCungCap.SeriLuyKeThanhTienKeHoach = false;
                        ctrl_NhaCungCap.SeriThanhTienKeHoach = false;

                        ctrl_NhaThau.Legend = (DefaultBoolean)Check;
                        ctrl_NhaThauPhu.Legend = (DefaultBoolean)Check;
                        ctrl_ToDoi.Legend = (DefaultBoolean)Check;
                        ctrl_NhaThau.SeriXuHuong = ce_XuHuong.Checked;
                        ctrl_NhaThauPhu.SeriXuHuong = ce_XuHuong.Checked;
                        ctrl_ToDoi.SeriXuHuong = ce_XuHuong.Checked;
                        ctrl_NhaCungCap.SeriXuHuong = ce_XuHuong.Checked;
                        ctrl_NhaCungCap.Legend = (DefaultBoolean)Check;

                        ctrl_NhaThau.FcnLoadDataGrid(TypeDVTH.NhaThau, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                        ctrl_NhaThauPhu.FcnLoadDataGrid(TypeDVTH.NhaThauPhu, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                        ctrl_ToDoi.FcnLoadDataGrid(TypeDVTH.ToDoiThiCong, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                        ctrl_NhaCungCap.FcnLoadDataGrid(TypeDVTH.NhaCungCap, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);

                        gtt_NhaThau.ExpandAll();
                        gtt_NhaThauPhu.ExpandAll();
                        gtt_ToDoiThiCong.ExpandAll();
                        gtt_NhaCungCap.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }
                    else
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhập khối lượng cho toàn Dự án, Vui lòng chờ!");
                        gtt_NhaCungCap.Type = true;
                        gtt_NhaThau.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThau, TypeBaoCao.KhoiLuong);
                        gtt_NhaThauPhu.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThauPhu, TypeBaoCao.KhoiLuong);
                        gtt_ToDoiThiCong.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.ToDoiThiCong, TypeBaoCao.KhoiLuong);
                        gtt_NhaCungCap.DataSource = TongHopHelper.GetAllThanhTienVatLieuTest(TypeDVTH.NhaCungCap, TypeBaoCao.KhoiLuong);

                        gtt_NhaThau.ExpandAll();
                        gtt_NhaThauPhu.ExpandAll();
                        gtt_ToDoiThiCong.ExpandAll();
                        gtt_NhaCungCap.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Both;
                    spcc_KiemSoatChung_Row1.PanelVisibility = SplitPanelVisibility.Both;
                    spcc_KiemSoatChung_Row2.PanelVisibility = SplitPanelVisibility.Both;
                    spcc_KiemSoatChung_Row2.SplitterPosition = spcc_KiemSoatChung_Row2.Width / 2;
                    break;
                case 1:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        ce_KhoiLuong.Checked = false;
                        scc_NhaThau.PanelVisibility = SplitPanelVisibility.Panel2;
                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Giao thầu, Vui lòng chờ!");
                        ctrl_NhaThau.Legend = (DefaultBoolean)Check;
                        ctrl_NhaThau.SeriXuHuong = ce_XuHuong.Checked;
                        ctrl_NhaThau.FcnLoadDataGrid(TypeDVTH.NhaThau, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                        WaitFormHelper.CloseWaitForm();
                    }
                    else
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhật khối lượng Giao thầu, Vui lòng chờ!");
                        gtt_NhaThau.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThau, TypeBaoCao.KhoiLuong);
                        gtt_NhaThau.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Panel1;
                    spcc_KiemSoatChung_Row1.PanelVisibility = SplitPanelVisibility.Panel1;
                    break;
                case 2:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        ce_KhoiLuong.Checked = false;
                        scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Panel2;
                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà thầu phụ, Vui lòng chờ!");
                        ctrl_NhaThauPhu.Legend = (DefaultBoolean)Check;
                        ctrl_NhaThauPhu.SeriXuHuong = ce_XuHuong.Checked;
                        ctrl_NhaThauPhu.FcnLoadDataGrid(TypeDVTH.NhaThauPhu, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                        WaitFormHelper.CloseWaitForm();
                    }
                    else
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhật khối lượng Nhà thầu phụ, Vui lòng chờ!");
                        gtt_NhaThauPhu.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.NhaThauPhu, TypeBaoCao.KhoiLuong);
                        gtt_NhaThauPhu.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Panel1;
                    spcc_KiemSoatChung_Row1.PanelVisibility = SplitPanelVisibility.Panel2;

                    break;
                case 3:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        ce_KhoiLuong.Checked = false;
                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Tổ đội, Vui lòng chờ!");
                        scc_ToDoi.PanelVisibility = SplitPanelVisibility.Panel2;
                        ctrl_ToDoi.Legend = (DefaultBoolean)Check;
                        ctrl_ToDoi.SeriXuHuong = ce_XuHuong.Checked;
                        ctrl_ToDoi.FcnLoadDataGrid(TypeDVTH.ToDoiThiCong, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                        WaitFormHelper.CloseWaitForm();
                    }
                    else
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhật khối lượng Tổ đội, Vui lòng chờ!");
                        gtt_ToDoiThiCong.DataSource = TongHopHelper.GetAllThanhTienTheoDonViThucHien(TypeDVTH.ToDoiThiCong, TypeBaoCao.KhoiLuong);
                        gtt_ToDoiThiCong.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Panel2;
                    spcc_KiemSoatChung_Row2.PanelVisibility = SplitPanelVisibility.Panel1;

                    break;
                case 4:
                    if (Type == TypeBaoCao.DongTien)
                    {
                        ce_KhoiLuong.Checked = false;
                        WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà cung cấp, Vui lòng chờ!");
                        scc_NCC.PanelVisibility = SplitPanelVisibility.Panel2;
                        ctrl_NhaCungCap.SeriLuyKeThanhTienKeHoach = false;
                        ctrl_NhaCungCap.SeriThanhTienKeHoach = false;
                        ctrl_NhaCungCap.Legend = (DefaultBoolean)Check;
                        ctrl_NhaCungCap.SeriXuHuong = ce_XuHuong.Checked;
                        ctrl_NhaCungCap.FcnLoadDataGrid(TypeDVTH.NhaCungCap, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                        WaitFormHelper.CloseWaitForm();
                    }
                    else
                    {
                        WaitFormHelper.ShowWaitForm("Đang cập nhật khối lượng Nhà cung cấp, Vui lòng chờ!");
                        gtt_NhaCungCap.Type = true;
                        gtt_NhaCungCap.DataSource = TongHopHelper.GetAllThanhTienVatLieuTest(TypeDVTH.NhaCungCap, TypeBaoCao.KhoiLuong);
                        gtt_NhaCungCap.ExpandAll();
                        WaitFormHelper.CloseWaitForm();
                    }
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Panel2;
                    spcc_KiemSoatChung_Row2.PanelVisibility = SplitPanelVisibility.Panel2;
                    break;
                default:
                    break;
            }

            
        }

        private void spcc_KiemSoatChung_Row1_SplitterMoved(object sender, EventArgs e)
        {
            spcc_KiemSoatChung_Row2.SplitterPosition = spcc_KiemSoatChung_Row1.SplitterPosition;
        }

        private void spcc_KiemSoatChung_Row2_SplitterMoved(object sender, EventArgs e)
        {
            spcc_KiemSoatChung_Row1.SplitterPosition = spcc_KiemSoatChung_Row2.SplitterPosition;
        }

        private void spcc_KiemSoatChung_Main_SizeChanged(object sender, EventArgs e)
        {
            spcc_KiemSoatChung_Main.SplitterPosition = spcc_KiemSoatChung_Main.Height / 2;
            spcc_KiemSoatChung_Row1.SplitterPosition = spcc_KiemSoatChung_Row1.Width / 2;
            spcc_KiemSoatChung_Row2.SplitterPosition = spcc_KiemSoatChung_Row2.Width / 2;
        }

        private void ce_KhoiLuong_CheckedChanged(object sender, EventArgs e)
        {
            if (!ce_BieuDo.Checked)
            {
                ce_KhoiLuong.Checked = true;
                return;
            }

            if (ce_KhoiLuong.Checked)
            {
                Fcn_UpdateKL();
                scc_NhaThau.PanelVisibility = SplitPanelVisibility.Both;
                scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Both;
                scc_ToDoi.PanelVisibility = SplitPanelVisibility.Both;
                scc_NCC.PanelVisibility = SplitPanelVisibility.Both;
            }
            else
            {
                scc_NhaThau.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_ToDoi.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NCC.PanelVisibility = SplitPanelVisibility.Panel2;
            }
        }

        private void ce_BieuDo_CheckedChanged(object sender, EventArgs e)
        {
            if (!ce_KhoiLuong.Checked)
            {
                ce_BieuDo.Checked = true;
                return;
            }
            if (ce_BieuDo.Checked)
            {

                scc_NhaThau.PanelVisibility = SplitPanelVisibility.Both;
                scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Both;
                scc_ToDoi.PanelVisibility = SplitPanelVisibility.Both;
                scc_NCC.PanelVisibility = SplitPanelVisibility.Both;
            }
            else
            {
                scc_NhaThau.PanelVisibility = SplitPanelVisibility.Panel1;
                scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Panel1;
                scc_ToDoi.PanelVisibility = SplitPanelVisibility.Panel1;
                scc_NCC.PanelVisibility = SplitPanelVisibility.Panel1;
            }
        }

        private void scc_NhaThau_SplitterMoved(object sender, EventArgs e)
        {
        }

        private void scc_NhaThauPhu_SplitterMoved(object sender, EventArgs e)
        {
        }

        private void scc_NhaThau_SizeChanged(object sender, EventArgs e)
        {
            scc_NhaThau.SplitterPosition =scc_NhaThau.Width/3;
        }

        private void scc_NhaThauPhu_SizeChanged(object sender, EventArgs e)
        {
            scc_NhaThauPhu.SplitterPosition = scc_NhaThauPhu.Width / 3;
        }

        private void sb_CapNhap_Click(object sender, EventArgs e)
        {
            if (CheckNgay)
            {
                navigationPane1.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Default;
                Fcn_UpdateNgayBDKT();
                WaitFormHelper.ShowWaitForm("Đang cập nhập bảng tổng hợp, Vui lòng chờ!");
                tongHopCongTacVatLieu.Fcn_Update(de_Begin.DateTime.Date, de_End.DateTime.Date);
                WaitFormHelper.CloseWaitForm();

            }
        }
        public event EventHandler sb_capNhap_Click
        {
            add
            {
                sb_CapNhap.Click += value;
            }
            remove
            {
                sb_CapNhap.Click -= value;
            }
        }
        public int SoNgay { get { if (Index == 2) return 0; else return (int)se_songay.Value; } set { se_songay.Value = value; } }
        public bool TrangThai { get { return se_songay.Enabled; } set { se_songay.Enabled = value; } }
        public  TypeBaoCao Type { get; set; }
        public int Check
        {
            get
            {
                if (ce_ChiTiet.Checked)
                    return 0;
                else return 1;
            }
            set
            {
                if (value == 0) ce_ChiTiet.Checked = true;
                else ce_ChiTiet.Checked = false;
            }
        }
        public int Index { get { return rg_CaiDat.SelectedIndex; } set { rg_CaiDat.SelectedIndex = value; } }
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

        private void ce_ChonNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_ChonNgay.Checked)
            {
                lci_Begin.Enabled = lci_End.Enabled = true;
                de_Begin.DateTime = de_End.DateTime = DateTime.Now;
                switch (cbb_ChonCuaSo.SelectedIndex)
                {
                    case 0:
                    case 1:
                        if (Type == TypeBaoCao.DongTien)
                        {
                            WaitFormHelper.ShowWaitForm("Đang cập nhập dữ liệu cho toàn Dự án, Vui lòng chờ!");
                            string dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
    $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
    $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
    $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
                            DataTable dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            dbString = $"SELECT *FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
                            DataTable dtTheoChuKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            if (dtTheoChuKy.Rows.Count == 0)
                                break;
                            DateTime Min_KH = dtTheoChuKy.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                            DateTime Max_KH = dtTheoChuKy.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                            if (dttc.Rows.Count != 0)
                            {
                                de_End.DateTime = Max_KH;
                                de_Begin.DateTime = Min_KH;
                                DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                                de_End.DateTime = Max_KH >= Max_TC ? Max_KH : Max_TC;
                                DateTime Min_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                                de_Begin.DateTime = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
                            }
                            WaitFormHelper.CloseWaitForm();
                        }
                        break;
                    case 2:
                        if (Type == TypeBaoCao.DongTien)
                        {
                            WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà thầu phụ, Vui lòng chờ!");
                            string dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
    $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
    $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
    $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThauPhu IS NOT NULL ";
                            DataTable dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            dbString = $"SELECT *FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'  AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThauPhu IS NOT NULL ";
                            DataTable dtTheoChuKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            if (dtTheoChuKy.Rows.Count == 0)
                                break;
                            DateTime Min_KH = dtTheoChuKy.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                            DateTime Max_KH = dtTheoChuKy.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                            if (dttc.Rows.Count != 0)
                            {
                                de_End.DateTime = Max_KH;
                                de_Begin.DateTime = Min_KH;
                                DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                                de_End.DateTime = Max_KH >= Max_TC ? Max_KH : Max_TC;
                                DateTime Min_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                                de_Begin.DateTime = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
                            }
                            WaitFormHelper.CloseWaitForm();
                        }

                        break;
                    case 3:
                        if (Type == TypeBaoCao.DongTien)
                        {
                            WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Tổ đội, Vui lòng chờ!");
                            string dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
    $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
    $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
    $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeToDoi IS NOT NULL ";
                            DataTable dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            dbString = $"SELECT *FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeToDoi IS NOT NULL";
                            DataTable dtTheoChuKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                            if (dtTheoChuKy.Rows.Count == 0)
                                break;
                            DateTime Min_KH = dtTheoChuKy.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                            DateTime Max_KH = dtTheoChuKy.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                            if (dttc.Rows.Count != 0)
                            {
                                de_End.DateTime = Max_KH;
                                de_Begin.DateTime = Min_KH;
                                DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                                de_End.DateTime = Max_KH >= Max_TC ? Max_KH : Max_TC;
                                DateTime Min_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                                de_Begin.DateTime = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
                            }
                            WaitFormHelper.CloseWaitForm();
                        }

                        break;
                    default:
                        break;
                }
            }
            else
                lci_Begin.Enabled = lci_End.Enabled = false;
        }
        public bool CheckNgay { get { return ce_ChonNgay.Checked; }}
        private void Fcn_UpdateNgayBDKT()
        {
            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:
                    WaitFormHelper.ShowWaitForm("Đang cập nhập dữ liệu cho toàn Dự án, Vui lòng chờ!");
                    ctrl_NhaCungCap.SeriLuyKeThanhTienKeHoach = false;
                    ctrl_NhaCungCap.SeriThanhTienKeHoach = false;

                    ctrl_NhaThau.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThauPhu.Legend = (DefaultBoolean)Check;
                    ctrl_ToDoi.Legend = (DefaultBoolean)Check;
                    ctrl_NhaCungCap.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThau.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_NhaThauPhu.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_NhaCungCap.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_ToDoi.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_NhaThau.FcnLoadDataGrid(TypeDVTH.NhaThau, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                    ctrl_NhaThauPhu.FcnLoadDataGrid(TypeDVTH.NhaThauPhu, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                    ctrl_ToDoi.FcnLoadDataGrid(TypeDVTH.ToDoiThiCong, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                    ctrl_NhaCungCap.FcnLoadDataGrid(TypeDVTH.NhaCungCap, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);

                    gtt_NhaThau.ExpandAll();
                    gtt_NhaThauPhu.ExpandAll();
                    gtt_ToDoiThiCong.ExpandAll();
                    gtt_NhaCungCap.ExpandAll();
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 1:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Giao thầu, Vui lòng chờ!");
                    ctrl_NhaThau.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThau.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_NhaThau.FcnLoadDataGrid(TypeDVTH.NhaThau, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 2:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà thầu phụ, Vui lòng chờ!");
                    ctrl_NhaThauPhu.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThauPhu.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_NhaThauPhu.FcnLoadDataGrid(TypeDVTH.NhaThauPhu, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 3:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Tổ đội, Vui lòng chờ!");
                    ctrl_ToDoi.Legend = (DefaultBoolean)Check;
                    ctrl_ToDoi.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_ToDoi.FcnLoadDataGrid(TypeDVTH.ToDoiThiCong, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                    WaitFormHelper.CloseWaitForm();

                    break;
                case 4:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà cung cấp, Vui lòng chờ!");
                    ctrl_NhaCungCap.SeriLuyKeThanhTienKeHoach = false;
                    ctrl_NhaCungCap.SeriThanhTienKeHoach = false;
                    ctrl_NhaCungCap.Legend = (DefaultBoolean)Check;
                    ctrl_NhaCungCap.SeriXuHuong = ce_XuHuong.Checked;
                    ctrl_NhaCungCap.FcnLoadDataGrid(TypeDVTH.NhaCungCap, SoNgay, de_Begin.DateTime.Date, de_End.DateTime.Date);
                    WaitFormHelper.CloseWaitForm();
                    break;
                default:
                    break;
            }


        }

        private void sb_XuatTongHop_Click(object sender, EventArgs e)
        {
            tongHopCongTacVatLieu.Export();
        }

        private void ce_XuHuong_CheckedChanged(object sender, EventArgs e)
        {
            ctrl_NhaThau.SeriXuHuong = ce_XuHuong.Checked;
            ctrl_NhaThauPhu.SeriXuHuong = ce_XuHuong.Checked;
            ctrl_NhaCungCap.SeriXuHuong = ce_XuHuong.Checked;
            ctrl_ToDoi.SeriXuHuong = ce_XuHuong.Checked;
        }
    }
}
