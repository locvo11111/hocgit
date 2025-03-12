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
    public partial class FormPhanTicTienDoTuDong_TuTinhNgay : Form
    {
        public FormPhanTicTienDoTuDong_TuTinhNgay()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            // Thoát phân tích ngày tiến độ tự động
            this.Close();
        }
    }
}
