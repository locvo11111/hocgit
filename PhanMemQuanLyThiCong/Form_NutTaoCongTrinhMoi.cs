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
    public partial class Form_NutTaoCongTrinhMoi : Form
    {
        public Form_NutTaoCongTrinhMoi()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
           // Đóng cửa sổ tạo mới công trình
            this.Close();
        }
    }
}
