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
    public partial class FormChiaSeDuLieuLenHeThongDungChung : Form
    {
        public FormChiaSeDuLieuLenHeThongDungChung()
        {
            InitializeComponent();
        }

        private void btnThoatDeSau_Click(object sender, EventArgs e)
        {
           // Thoát chương trình cập nhật dữ liệu hiện thời
            this.Close();
        }
    }
}
