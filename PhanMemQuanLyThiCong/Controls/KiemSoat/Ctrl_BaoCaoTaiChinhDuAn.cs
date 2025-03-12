using DevExpress.Utils;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Ctrl_BaoCaoTaiChinhDuAn : DevExpress.XtraEditors.XtraUserControl
    {

        #region Custom Properties

        private bool _IsBrief = false;
        [DisplayName("IsBrief")]
        public bool IsBrief
        {
            get
            {
                return _IsBrief;
            }

            set
            {
                    _IsBrief = value;

                   if (value)
                   {
                        scc_Main23.PanelVisibility = SplitPanelVisibility.Panel1;
                        scc_Main12.PanelVisibility = SplitPanelVisibility.Panel1;
                        scc_DuAn_TuThucHien.PanelVisibility = SplitPanelVisibility.Panel1;
                    //lci_luachoncuaSo.Visibility
                    //    = lci_ChiTietCongTac.Visibility
                    //    = lci_ChiTietHaoPhi.Visibility     
                    //    = lci_TyLePhanTram.Visibility
                    //    = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcg_footerGr.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lci_XemChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    
                   }
                   else
                {
                    scc_Main23.PanelVisibility = SplitPanelVisibility.Both;
                    scc_Main12.PanelVisibility = SplitPanelVisibility.Both;
                    scc_DuAn_TuThucHien.PanelVisibility = SplitPanelVisibility.Both;
                    //     lci_luachoncuaSo.Visibility
                    //= lci_ChiTietCongTac.Visibility
                    //= lci_ChiTietHaoPhi.Visibility
                    //= lci_TyLePhanTram.Visibility
                    //= DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    lcg_footerGr.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    lci_XemChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

            }
        }
        #endregion
        public Ctrl_BaoCaoTaiChinhDuAn()
        {
            InitializeComponent();
        }




        public void LoadDongTien()
        {
            if (cbb_ChonCuaSo.SelectedIndex == 1)
            {
                ce_ChiTietCongTac.Checked = ce_ChiTietHaoPhi.Checked = false;
                scc_TuThucHien.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaThau.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_ToDoi.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaCungCap.PanelVisibility = SplitPanelVisibility.Panel2;
                WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Dự án, Vui lòng chờ!");
                ctrl_DuAn.SeriThanhTienThiCong = false;
                ctrl_DuAn.Legend = (DefaultBoolean)Check;
                ctrl_DuAn.SeriThanhTienHangNgay = true;
                ctrl_DuAn.FcnLoadDataGrid(TypeDVTH.DuAn, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                ctrl_DuAn.Fcn_SetRowHeight(2 * ctrl_DuAn.Height - gc_DuAn.Height);
                scc_DuAn_TuThucHien.PanelVisibility = SplitPanelVisibility.Panel1;
                scc_Main23.PanelVisibility = SplitPanelVisibility.Panel1;
                scc_Main12.PanelVisibility = SplitPanelVisibility.Panel1;
                WaitFormHelper.CloseWaitForm();
            }
            else
                cbb_ChonCuaSo.SelectedIndex = 1;

        }
        private void Fcn_UpdateCheck()
        {
            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Toàn dự án, Vui lòng chờ!");
                    ctrl_ChiTietCongTacTTH.DataSource =ce_ChiTietCongTac.Checked?TongHopHelper.GetAllThanhTienChiTietCongTac(TypeDVTH.TuThucHien): TongHopHelper.GetAllThanhTienHaoPhiCongTac(TypeDVTH.TuThucHien);
                    ctrl_ChiTietCongTacNhaThau.DataSource =ce_ChiTietCongTac.Checked? TongHopHelper.GetAllThanhTienChiTietCongTac(TypeDVTH.NhaThau): TongHopHelper.GetAllThanhTienHaoPhiCongTac(TypeDVTH.NhaThau);
                    ctrl_ChiTietCongTacToDoi.DataSource =ce_ChiTietCongTac.Checked? TongHopHelper.GetAllThanhTienChiTietCongTac(TypeDVTH.ToDoiThiCong): TongHopHelper.GetAllThanhTienHaoPhiCongTac(TypeDVTH.ToDoiThiCong);
                    ctrl_ChiTietCongTacNhaThauPhu.DataSource =ce_ChiTietCongTac.Checked?TongHopHelper.GetAllThanhTienChiTietCongTac(TypeDVTH.NhaThauPhu): TongHopHelper.GetAllThanhTienHaoPhiCongTac(TypeDVTH.NhaThauPhu);
                    ctrl_ChiTietCongTacNCC.Type = true;
                    ctrl_ChiTietCongTacNCC.DataSource = TongHopHelper.GetAllThanhTienChiTietVatLieu(TypeDVTH.NhaCungCap);
                    ctrl_ChiTietCongTacTTH.ExpandAll();
                    ctrl_ChiTietCongTacNhaThau.ExpandAll();
                    ctrl_ChiTietCongTacToDoi.ExpandAll();
                    ctrl_ChiTietCongTacNhaThauPhu.ExpandAll();
                    ctrl_ChiTietCongTacNCC.ExpandAll();
                    WaitFormHelper.CloseWaitForm();

                    break;
                case 2:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà thầu Tự thực hiện, Vui lòng chờ!");
                    ctrl_ChiTietCongTacTTH.DataSource = ce_ChiTietCongTac.Checked ? TongHopHelper.GetAllThanhTienChiTietCongTac(TypeDVTH.TuThucHien) : TongHopHelper.GetAllThanhTienHaoPhiCongTac(TypeDVTH.TuThucHien);
                    ctrl_ChiTietCongTacTTH.ExpandAll();
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 3:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Giao thầu, Vui lòng chờ!");
                    ctrl_ChiTietCongTacNhaThau.DataSource = ce_ChiTietCongTac.Checked ? TongHopHelper.GetAllThanhTienChiTietCongTac(TypeDVTH.NhaThau) : TongHopHelper.GetAllThanhTienHaoPhiCongTac(TypeDVTH.NhaThau);
                    ctrl_ChiTietCongTacNhaThau.ExpandAll();
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 4:

                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà thầu phụ, Vui lòng chờ!");
                    ctrl_ChiTietCongTacNhaThauPhu.DataSource = ce_ChiTietCongTac.Checked ? TongHopHelper.GetAllThanhTienChiTietCongTac(TypeDVTH.NhaThauPhu) : TongHopHelper.GetAllThanhTienHaoPhiCongTac(TypeDVTH.NhaThauPhu);
                    ctrl_ChiTietCongTacNhaThau.ExpandAll();
                    WaitFormHelper.CloseWaitForm();

                    break;
                case 5:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Tổ đội, Vui lòng chờ!");
                    ctrl_ChiTietCongTacToDoi.DataSource = ce_ChiTietCongTac.Checked ? TongHopHelper.GetAllThanhTienChiTietCongTac(TypeDVTH.ToDoiThiCong) : TongHopHelper.GetAllThanhTienHaoPhiCongTac(TypeDVTH.ToDoiThiCong);
                    ctrl_ChiTietCongTacToDoi.ExpandAll();
                    WaitFormHelper.CloseWaitForm();

                    break;
                case 6:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà cung cấp, Vui lòng chờ!");
                    ctrl_ChiTietCongTacNCC.Type = true;
                    ctrl_ChiTietCongTacNCC.DataSource = TongHopHelper.GetAllThanhTienChiTietVatLieu(TypeDVTH.NhaCungCap);
                    ctrl_ChiTietCongTacNCC.ExpandAll();
                    WaitFormHelper.CloseWaitForm();

                    break;
                default:
                    break;
            }





        }
        private void scc_Main23_SizeChanged(object sender, EventArgs e)
        {
            scc_Main23.SplitterPosition = scc_Main23.Height * 2 / 3;
        }

        private void scc_Main12_SizeChanged(object sender, EventArgs e)
        {
            scc_Main12.SplitterPosition = scc_Main12.Height / 2;
        }

        private void scc_ToDoi_NCC_SizeChanged(object sender, EventArgs e)
        {
            scc_ToDoi_NCC.SplitterPosition = scc_ToDoi_NCC.Width / 2;
        }

        private void scc_NhaThau_NTP_SizeChanged(object sender, EventArgs e)
        {
            scc_NhaThau_NTP.SplitterPosition = scc_NhaThau_NTP.Width / 2;
        }

        private void scc_DuAn_TuThucHien_SizeChanged(object sender, EventArgs e)
        {
            scc_DuAn_TuThucHien.SplitterPosition = scc_DuAn_TuThucHien.Width / 2;
        }

        private void cbb_ChonCuaSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ce_ChiTietCongTac.Checked = ce_ChiTietHaoPhi.Checked = false;
            scc_TuThucHien.PanelVisibility = SplitPanelVisibility.Panel2;
            scc_NhaThau.PanelVisibility = SplitPanelVisibility.Panel2;
            scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Panel2;
            scc_ToDoi.PanelVisibility = SplitPanelVisibility.Panel2;
            scc_NhaCungCap.PanelVisibility = SplitPanelVisibility.Panel2;
            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Toàn dự án, Vui lòng chờ!");
                    ctrl_NhaThau.SeriThanhTienHangNgay = false;
                    ctrl_NhaThauPhu.SeriThanhTienHangNgay = false;
                    ctrl_ToDoi.SeriThanhTienHangNgay = false;
                    ctrl_TuThucHien.SeriThanhTienHangNgay = false;
                    ctrl_NCC.SeriThanhTienHangNgay = false;
                    ctrl_NCC.SeriThanhTienKeHoach = false;
                    ctrl_NhaThau.SeriThanhTienThiCong = true;
                    ctrl_NhaThauPhu.SeriThanhTienThiCong = true;
                    ctrl_ToDoi.SeriThanhTienThiCong = true;
                    ctrl_TuThucHien.SeriThanhTienThiCong = true;
                    ctrl_NCC.SeriThanhTienThiCong = true;
                    ctrl_DuAn.SeriThanhTienThiCong = false;

                    ctrl_DuAn.Legend = (DefaultBoolean)Check;
                    ctrl_TuThucHien.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThau.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThauPhu.Legend = (DefaultBoolean)Check;
                    ctrl_ToDoi.Legend = (DefaultBoolean)Check;
                    ctrl_NCC.Legend = (DefaultBoolean)Check;

                    ctrl_NhaThau.FcnLoadDataGrid(TypeDVTH.NhaThau, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                    ctrl_NhaThauPhu.FcnLoadDataGrid(TypeDVTH.NhaThauPhu, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                    ctrl_ToDoi.FcnLoadDataGrid(TypeDVTH.ToDoiThiCong, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                    ctrl_TuThucHien.FcnLoadDataGrid(TypeDVTH.TuThucHien, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                    ctrl_NCC.FcnLoadDataGrid(TypeDVTH.NhaCungCap, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);

                    ctrl_DuAn.SeriThanhTienHangNgay = true;
                    ctrl_DuAn.FcnLoadDataGrid(TypeDVTH.DuAn, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                    WaitFormHelper.CloseWaitForm();



                    scc_Main23.PanelVisibility = SplitPanelVisibility.Both;
                    scc_Main12.PanelVisibility = SplitPanelVisibility.Both;
                    scc_NhaThau_NTP.PanelVisibility = SplitPanelVisibility.Both;
                    scc_ToDoi_NCC.PanelVisibility = SplitPanelVisibility.Both;
                    scc_DuAn_TuThucHien.PanelVisibility = SplitPanelVisibility.Both;
 
                    break;
                case 1:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Dự án, Vui lòng chờ!");
                    ctrl_DuAn.SeriThanhTienThiCong = false;
                    ctrl_DuAn.Legend = (DefaultBoolean)Check;
                    ctrl_DuAn.SeriThanhTienHangNgay = true;
                    ctrl_DuAn.FcnLoadDataGrid(TypeDVTH.DuAn, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                    ctrl_DuAn.Fcn_SetRowHeight(2*ctrl_DuAn.Height- gc_DuAn.Height);
                    scc_DuAn_TuThucHien.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_Main23.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_Main12.PanelVisibility = SplitPanelVisibility.Panel1;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 2:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà thầu Tự thực hiện, Vui lòng chờ!");
                    ctrl_TuThucHien.SeriThanhTienHangNgay = false;
                    ctrl_TuThucHien.SeriThanhTienThiCong = true;
                    ctrl_TuThucHien.Legend = (DefaultBoolean)Check;
                    ctrl_TuThucHien.FcnLoadDataGrid(TypeDVTH.TuThucHien, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);

                    scc_DuAn_TuThucHien.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_Main23.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_Main12.PanelVisibility = SplitPanelVisibility.Panel1;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 3:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Giao thầu, Vui lòng chờ!");
                    ctrl_NhaThau.SeriThanhTienHangNgay = false;
                    ctrl_NhaThau.SeriThanhTienThiCong = true;
                    ctrl_NhaThau.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThau.FcnLoadDataGrid(TypeDVTH.NhaThau, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);

                    scc_NhaThau_NTP.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_Main23.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_Main12.PanelVisibility = SplitPanelVisibility.Panel2;
                    WaitFormHelper.CloseWaitForm();
                    break;
                case 4:

                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà thầu phụ, Vui lòng chờ!");
                    ctrl_NhaThauPhu.SeriThanhTienHangNgay = false;
                    ctrl_NhaThauPhu.SeriThanhTienThiCong = true;
                    ctrl_NhaThauPhu.Legend = (DefaultBoolean)Check;
                    ctrl_NhaThauPhu.FcnLoadDataGrid(TypeDVTH.NhaThauPhu, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                    WaitFormHelper.CloseWaitForm();
                    scc_NhaThau_NTP.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_Main23.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_Main12.PanelVisibility = SplitPanelVisibility.Panel2;

                    break;        
                case 5:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Tổ đội, Vui lòng chờ!");
                    ctrl_ToDoi.SeriThanhTienHangNgay = false;
                    ctrl_ToDoi.SeriThanhTienThiCong = true;
                    ctrl_ToDoi.Legend = (DefaultBoolean)Check;
                    ctrl_ToDoi.FcnLoadDataGrid(TypeDVTH.ToDoiThiCong, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                    WaitFormHelper.CloseWaitForm();
                    scc_ToDoi_NCC.PanelVisibility = SplitPanelVisibility.Panel1;
                    scc_Main23.PanelVisibility = SplitPanelVisibility.Panel2;

                    break;              
                case 6:
                    WaitFormHelper.ShowWaitForm("Đang cập nhật dữ liệu Nhà cung cấp, Vui lòng chờ!");
                    ctrl_NCC.SeriThanhTienHangNgay = false;
                    ctrl_NCC.SeriThanhTienKeHoach = false;
                    ctrl_NCC.SeriThanhTienThiCong = true;
                    ctrl_NCC.Legend = (DefaultBoolean)Check;
                    ctrl_NCC.FcnLoadDataGrid(TypeDVTH.NhaCungCap, (double)double.Parse(Ce_LoiNhuan.Text) / 100, SoNgay);
                    WaitFormHelper.CloseWaitForm();
                    scc_ToDoi_NCC.PanelVisibility = SplitPanelVisibility.Panel2;
                    scc_Main23.PanelVisibility = SplitPanelVisibility.Panel2;
                    break;
                default:
                    break;
            }
        }
        private void sb_CapNhap_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhật theo cài đặt, Vui lòng chờ!");
            LoadDongTien();
            WaitFormHelper.CloseWaitForm();
            MessageShower.ShowInformation("Cập nhập thành công!");
        }

        public string TextLoiNhuan { get { return Ce_LoiNhuan.Text; } set { Ce_LoiNhuan.Text = value; } }

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
            {
                se_songay.Value = 1;
                se_songay.Enabled = true;
            }
        }
        public int SoNgay { get { if (Index == 2) return 0; else return (int)se_songay.Value; } set { se_songay.Value = value; } }
        public bool TrangThai { get { return se_songay.Enabled; } set { se_songay.Enabled = value; } }
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

        private void ce_ChiTietCongTac_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_ChiTietCongTac.Checked)
            {
                ce_ChiTietHaoPhi.Checked = false;
                Fcn_UpdateCheck();
                scc_TuThucHien.PanelVisibility = SplitPanelVisibility.Both;
                scc_NhaThau.PanelVisibility = SplitPanelVisibility.Both;
                scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Both;
                scc_ToDoi.PanelVisibility = SplitPanelVisibility.Both;
                scc_NhaCungCap.PanelVisibility = SplitPanelVisibility.Both;
            }
            else
            {
                //scc_Duan.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_TuThucHien.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaThau.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_ToDoi.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaCungCap.PanelVisibility = SplitPanelVisibility.Panel2;

            }

        }

        private void scc_TuThucHien_SizeChanged(object sender, EventArgs e)
        {
            scc_TuThucHien.SplitterPosition = scc_TuThucHien.Width / 2;
        }

        private void scc_NhaThau_SizeChanged(object sender, EventArgs e)
        {
            scc_NhaThau.SplitterPosition = scc_NhaThau.Width / 2;
        }

        private void scc_NhaThauPhu_SizeChanged(object sender, EventArgs e)
        {
            scc_NhaThauPhu.SplitterPosition = scc_NhaThauPhu.Width / 2;
        }

        private void scc_ToDoi_SizeChanged(object sender, EventArgs e)
        {
            scc_ToDoi.SplitterPosition = scc_ToDoi.Width / 2;
        }

        private void scc_NhaCungCap_SizeChanged(object sender, EventArgs e)
        {
            scc_NhaCungCap.SplitterPosition = scc_NhaCungCap.Width / 2;
        }

        private void ce_ChiTietHaoPhi_CheckedChanged(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu", "Vui Lòng chờ!");
            if (ce_ChiTietHaoPhi.Checked)
            {
                ce_ChiTietCongTac.Checked = false;
                Fcn_UpdateCheck();
                scc_TuThucHien.PanelVisibility = SplitPanelVisibility.Both;
                scc_NhaThau.PanelVisibility = SplitPanelVisibility.Both;
                scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Both;
                scc_ToDoi.PanelVisibility = SplitPanelVisibility.Both;
                scc_NhaCungCap.PanelVisibility = SplitPanelVisibility.Both;
            }
            else
            {
                scc_TuThucHien.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaThau.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaThauPhu.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_ToDoi.PanelVisibility = SplitPanelVisibility.Panel2;
                scc_NhaCungCap.PanelVisibility = SplitPanelVisibility.Panel2;
            }
            WaitFormHelper.CloseWaitForm();
        }

        private void bt_XemChiTiet_Click(object sender, EventArgs e)
        {
            SharedControls.xtraTab_KiemSoat.SelectedTabPage = SharedControls.xtraTab_BaoCaoLoiNhuan;

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

        private void ce_ChiTiet_CheckedChanged(object sender, EventArgs e)
        {

        }
        //private void ce_HangNgay_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ce_HangNgay.Checked)
        //    {
        //        ctrl_DuAn.SeriThanhTienHangNgay = true;
        //        ctrl_DuAn.FcnLoadDataGrid(TypeDVTH.DuAn, (double)double.Parse(Ce_LoiNhuan.Text) / 100);
        //    }
        //    else
        //    {
        //        ctrl_DuAn.SeriThanhTienHangNgay = false;
        //        ctrl_DuAn.FcnLoadDataGrid(TypeDVTH.DuAn, (double)double.Parse(Ce_LoiNhuan.Text) / 100);
        //    }
        //}
    }
}
