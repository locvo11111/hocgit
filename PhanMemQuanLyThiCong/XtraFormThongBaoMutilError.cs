using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraFormThongBaoMutilError : DevExpress.XtraEditors.XtraForm
    {
        public XtraFormThongBaoMutilError(string Caption = "Thông báo lỗi")
        {
            InitializeComponent();
            Text = Caption;
        }
        public string Description { get; set; }
        private void sb_Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void XtraFormThongBaoMutilError_Load(object sender, EventArgs e)
        {
            me_ThongTin.Text = Description;
        }
    }
}