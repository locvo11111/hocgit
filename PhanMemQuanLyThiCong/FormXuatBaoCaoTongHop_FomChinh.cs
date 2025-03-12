using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class FormXuatBaoCaoTongHop_FomChinh : Form
    {
        public FormXuatBaoCaoTongHop_FomChinh()
        {
            InitializeComponent();
        }

        private void btn_BCTH_XuatBaoCaoNhanVien_Click(object sender, EventArgs e)
        {
            // Xem xuất số liệu riêng cho mỗi cá nhân
            FormCacCongViecCaNhanThucHien CongViecNhanVien = new FormCacCongViecCaNhanThucHien();
            CongViecNhanVien.Show();
        }

        private void btn_BCTH_XuatBaoCaoCongTrinh_Click(object sender, EventArgs e)
        {
            // Xuất báo cáo thông tin toàn bộ công trình hiện tại
            Form_XuatBaoCaoTongHop BaoCaoTongHop = new Form_XuatBaoCaoTongHop();
            BaoCaoTongHop.Fcn_Update();
            BaoCaoTongHop.ShowDialog();
        }

        private void btn_BCTH_Thoat_Click(object sender, EventArgs e)
        {
            // Thoát chế độ xuất full
            this.Close();
        }

        private void btn_BCTH_XuatBaoCaoDuAn_Click(object sender, EventArgs e)
        {
            Form_XuatBaoCaoDuAn Report = new Form_XuatBaoCaoDuAn();
            Report.ShowDialog();
        }

        private void btn_BCTH_XuatBaoCaoNhanLuc_Click(object sender, EventArgs e)
        {
            Form_BaoCaoTaiChinhDuAn Report = new Form_BaoCaoTaiChinhDuAn();
            Report.ShowDialog();
        }

        private void btn_BCTH_XuatBaoCaoThuChiTamUng_Click(object sender, EventArgs e)
        {
            Form_XuatBaoCaoHopDong Report = new Form_XuatBaoCaoHopDong();
            Report.ShowDialog();
        }

        private void btn_BCTH_XuatBaoCaoVatTu_Click(object sender, EventArgs e)
        {
            Form_BaoCaoVatLieu Report = new Form_BaoCaoVatLieu();
            Report.ShowDialog();
        }

        private void btn_BCTH_XuatBaoCaoTienDo_Click(object sender, EventArgs e)
        {
            Form_BaoCaoHangNgay BaoCaoHN = new Form_BaoCaoHangNgay();
            //BaoCaoHN.Fcn_Update();
            BaoCaoHN.ShowDialog();
        }

        private void bt_XuatDAOnline_Click(object sender, EventArgs e)
        {
            var form = new XtraForm_BaoCaoDuAnOnline();
            form.ShowDialog();
            return;
        }

        private void sb_ThuyetMinh_Click(object sender, EventArgs e)
        {
            XtraForm_ThuyetMinhDuAn frm = new XtraForm_ThuyetMinhDuAn();
            frm.ShowDialog();
        }
    }
}
