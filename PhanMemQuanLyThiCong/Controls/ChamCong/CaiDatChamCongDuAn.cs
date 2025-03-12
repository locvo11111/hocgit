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
    public partial class CaiDatChamCongDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        public CaiDatChamCongDuAn()
        {
            InitializeComponent();
        }
        private bool EditValue { get; set; } = false;
        public void Fcn_Update()
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu cài đặt phần mềm", "Vui Lòng chờ!");
            EditValue = false;
            caiDatKyHieuChamCong.Fcn_Update();
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATHESO} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            List<HeSoChamCong> HeSo = DataProvider.InstanceTHDA.ExecuteQueryModel<HeSoChamCong>(dbString);
            heSoChamCongHienTruong.DataSource = HeSo.Where(x => x.LoaiCaiDat == 1).ToList();
            heSoChamCongVanPhong.DataSource = HeSo.Where(x => x.LoaiCaiDat == 2).ToList();
            heSoChamCongCongNhat.DataSource = HeSo.Where(x => x.LoaiCaiDat == 3).ToList();
            heSoChamCongNhanCongKhoan.DataSource = HeSo.Where(x => x.LoaiCaiDat == 4).ToList();
            EditValue = true;
            WaitFormHelper.CloseWaitForm();
        }
        private void splitContainerControl3_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl3.SplitterPosition = splitContainerControl3.Width / 2;
        }

        private void splitContainerControl4_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl4.SplitterPosition = splitContainerControl4.Width / 2;
        }

        private void splitContainerControl1_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Height / 3;
        }

        private void splitContainerControl2_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
        }

        private void Thu7SpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!EditValue)
                return;
            SpinEdit sp = sender as SpinEdit;
            int LEN = sp.Name.Length;
            string FieldName = sp.Name.Remove(LEN-2, 2);
            string dbString = $"UPDATE {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATHESO} SET" +
                $" {FieldName}=@Value WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            dbString += dataLayoutControlHienTruong.Controls.Contains(sp) ? "AND \"LoaiCaiDat\"='1'" :
                (dataLayoutControlVanPhong.Controls.Contains(sp)? "AND \"LoaiCaiDat\"='2'":
                (dataLayoutControlCongNhat.Controls.Contains(sp)? "AND \"LoaiCaiDat\"='3'":"AND \"LoaiCaiDat\"='4'"));
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString,parameter:new object[] { sp.EditValue });
        }

        private void Thu7SpinEdit_ValueChanged(object sender, EventArgs e)
        {

        }

        private void sb_MacDinh_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang đặt dữ liệu mặc định", "Vui Lòng chờ!");
            List<HeSoChamCong> HeSo = heSoChamCongHienTruong.DataSource as List<HeSoChamCong>;
            HeSo.ForEach(x => { x.CodeDuAn = null; x.Code = "1"; });
            DataTable dt = DatatableHelper.fcn_ObjToDataTable(HeSo);
            DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATHESO, isCompareTime: false);

            HeSo = heSoChamCongVanPhong.DataSource as List<HeSoChamCong>;
            HeSo.ForEach(x => { x.CodeDuAn = null; x.Code = "2"; });
            dt = DatatableHelper.fcn_ObjToDataTable(HeSo);
            DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATHESO, isCompareTime: false);    
            
            HeSo = heSoChamCongCongNhat.DataSource as List<HeSoChamCong>;
            HeSo.ForEach(x => { x.CodeDuAn = null; x.Code = "3"; });
            dt = DatatableHelper.fcn_ObjToDataTable(HeSo);
            DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATHESO, isCompareTime: false);     
            
            HeSo = heSoChamCongNhanCongKhoan.DataSource as List<HeSoChamCong>;
            HeSo.ForEach(x => { x.CodeDuAn = null; x.Code = "4"; });
            dt = DatatableHelper.fcn_ObjToDataTable(HeSo);
            DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATHESO, isCompareTime: false);
            WaitFormHelper.CloseWaitForm();
            MessageShower.ShowInformation("Đặt cài đặt mặc định thành công!!!!!!");
        }
    }
}
