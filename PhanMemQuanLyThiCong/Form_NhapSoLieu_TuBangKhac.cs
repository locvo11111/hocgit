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
    public partial class Form_NhapSoLieu_TuBangKhac : Form
    {
        public Form_NhapSoLieu_TuBangKhac()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
           // Thoát fom đọc file làm tiến độ
            this.Close();
        }

        private void btn_DocTuExcel_Click(object sender, EventArgs e)
        {
            // Đọc file excel làm tiến độ
            //FrmDocFileExcelKhoiLuong_GiaTri DocFileExCelTienDo = new FrmDocFileExcelKhoiLuong_GiaTri(null);
            //DocFileExCelTienDo.ShowDialog();
        }

        
    }
}
