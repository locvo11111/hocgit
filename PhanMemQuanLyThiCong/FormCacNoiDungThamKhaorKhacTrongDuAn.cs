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
    public partial class FormCacNoiDungThamKhaorKhacTrongDuAn : Form
    {
        public FormCacNoiDungThamKhaorKhacTrongDuAn()
        {
            InitializeComponent();
        }

        private void FormCacNoiDungThamKhaorKhacTrongDuAn_FormClosed(object sender, FormClosedEventArgs e)
        {
            spsheet_TC_TieuChuanDuAn.Dispose();
            spsheet_VL_VatLieuBoXung.Dispose();
            GC.Collect();
        }
    }
}
