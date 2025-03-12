using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.ChamCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.ChamCong
{
    public partial class CaiDatGioLamChamCong : DevExpress.XtraEditors.XtraUserControl
    {
        public CaiDatGioLamChamCong()
        {
            InitializeComponent();

        }
        private bool EditValue { get; set; } = false;
        private List<CaiDaGioLamChamCong> GioLamAll { get; set; }
        public void Fcn_Update(bool Load=false)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu cài đặt phần mềm", "Vui Lòng chờ!");
            EditValue = false;
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            List<CaiDaGioLamChamCong> GioLam = DataProvider.InstanceTHDA.ExecuteQueryModel<CaiDaGioLamChamCong>(dbString);
            if(GioLam.FirstOrDefault()?.NgayBatDau is null&&Load)
            {
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);
                GioLam.Where(x => x.MuaHe_Dong == "Mùa hè").ToList().ForEach(x => { x.NgayBatDau = firstDay;x.NgayKetThuc = firstDay.AddMonths(6); });
                GioLam.Where(x => x.MuaHe_Dong == "Mùa đông").ToList().ForEach(x => { x.NgayBatDau = firstDay.AddMonths(6).AddDays(1); x.NgayKetThuc =lastDay; });
                DataTable dt = DatatableHelper.fcn_ObjToDataTable(GioLam);
                DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM,isCompareTime:false);
                WaitFormHelper.CloseWaitForm();
                return;
            }
            GioLamAll = GioLam;
            rg_BuoiCongTruong.SelectedIndex = rg_BuoiVanPhong.SelectedIndex = 0;
            caiDaGioLamChamCongMuaHe_CT.DataSource = GioLam.Where(x => x.CongTruong_VanPhong == "Công trường" &&x.MuaHe_Dong=="Mùa hè"&& x.BuoiSang_Chieu_Toi == "Sáng").ToList();
            caiDaGioLamChamCongMuaDong_CT.DataSource = GioLam.Where(x => x.CongTruong_VanPhong == "Công trường" &&x.MuaHe_Dong=="Mùa đông"&& x.BuoiSang_Chieu_Toi == "Sáng").ToList();
            caiDaGioLamChamCongMuaHe_VP.DataSource = GioLam.Where(x => x.CongTruong_VanPhong == "Văn phòng" &&x.MuaHe_Dong=="Mùa hè"&& x.BuoiSang_Chieu_Toi == "Sáng").ToList();
            caiDaGioLamChamCongMuaDong_VP.DataSource = GioLam.Where(x => x.CongTruong_VanPhong == "Công trường" &&x.MuaHe_Dong=="Mùa đông"&& x.BuoiSang_Chieu_Toi == "Sáng").ToList();
            EditValue = true;
            WaitFormHelper.CloseWaitForm();
        }
        private void splitContainerControl1_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Height / 2;
        }

        private void NgayKetThucDateEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void splitContainerControl2_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (splitContainerControl2.Width-rg_BuoiCongTruong.Width) / 2;
        }

        private void splitContainerControl3_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl3.SplitterPosition = (splitContainerControl3.Width - rg_BuoiVanPhong.Width) / 2;
        }

        private void rg_BuoiCongTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!EditValue)
                return;
            caiDaGioLamChamCongMuaHe_CT.DataSource = GioLamAll.Where(x => x.CongTruong_VanPhong == "Công trường" && x.MuaHe_Dong == "Mùa hè" && x.BuoiSang_Chieu_Toi == rg_BuoiCongTruong.Properties.Items[rg_BuoiCongTruong.SelectedIndex].Description).ToList();
            caiDaGioLamChamCongMuaDong_CT.DataSource = GioLamAll.Where(x => x.CongTruong_VanPhong == "Công trường" && x.MuaHe_Dong == "Mùa đông" && x.BuoiSang_Chieu_Toi == rg_BuoiCongTruong.Properties.Items[rg_BuoiCongTruong.SelectedIndex].Description).ToList();
        }

        private void rg_BuoiVanPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!EditValue)
                return;
            caiDaGioLamChamCongMuaHe_VP.DataSource = GioLamAll.Where(x => x.CongTruong_VanPhong == "Công trường" && x.MuaHe_Dong == "Mùa hè" && x.BuoiSang_Chieu_Toi == rg_BuoiVanPhong.Properties.Items[rg_BuoiVanPhong.SelectedIndex].Description).ToList();
            caiDaGioLamChamCongMuaDong_VP.DataSource = GioLamAll.Where(x => x.CongTruong_VanPhong == "Công trường" && x.MuaHe_Dong == "Mùa đông" && x.BuoiSang_Chieu_Toi == rg_BuoiVanPhong.Properties.Items[rg_BuoiVanPhong.SelectedIndex].Description).ToList();
        }

        private void GioVaoSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            //if (!EditValue)
            //    return;
            //Control sp = sender as Control;
            //List<CaiDaGioLamChamCong> GioLam = new List<CaiDaGioLamChamCong>();
            //if (dtlo_MuaHeCT.Controls.Contains(sp))
            //{

            //}
        }

        private void caiDaGioLamChamCongMuaHe_CT_CurrentItemChanged(object sender, EventArgs e)
        {
            if (!EditValue)
                return;
            BindingSource Source = sender as BindingSource;
            List<CaiDaGioLamChamCong> GioLam=Source.DataSource as List<CaiDaGioLamChamCong>;
            DataTable dt = DatatableHelper.fcn_ObjToDataTable(GioLam);
            DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM, isCompareTime: false);
        }

        private void sb_MacDinh_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu cài đặt phần mềm", "Vui Lòng chờ!");
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable GioLam = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            int i = 1;
            foreach(DataRow row in GioLam.Rows)
            {
                row["Code"] = i++.ToString();
                row["CodeDuAn"] = null;
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(GioLam, DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM, isCompareTime: false);
            WaitFormHelper.CloseWaitForm();
            MessageShower.ShowInformation("Đặt cài đặt thành mặc định thành công!!!!!!");
        }
    }
}
