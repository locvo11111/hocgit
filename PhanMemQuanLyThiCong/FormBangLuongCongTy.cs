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
    public partial class FormBangLuongCongTy : Form
    {
        public FormBangLuongCongTy()
        {
            InitializeComponent();
        }

        private void FormBangLuongCongTy_FormClosed(object sender, FormClosedEventArgs e)
        {
            spsheet_BangQuyDoiLuongNgoaiGio.Dispose();
            spsheet_BangThanhToanLuong.Dispose();
            spsheet_BangTinhLuong.Dispose();
            spsheet_CongDaChamTrongThang.Dispose();
            spsheet_DanhSachNhanVien.Dispose();
            spsheet_PhieuThanhToanLuong.Dispose();
            spsheet_TamUng.Dispose();
            GC.Collect();
        }
    }
}
