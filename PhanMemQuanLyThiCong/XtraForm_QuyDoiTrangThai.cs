using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
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
    public partial class XtraForm_QuyDoiTrangThai : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_QuyDoiTrangThai(List<QuyDoiTrangThaiModel> dic)
        {
            InitializeComponent();
            treeList1.DataSource = dic;
        }

        private void XtraForm_QuyDoiTrangThai_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                MessageShower.ShowWarning("Vui lòng chọn các trạng thái hệ thống tương ứng và và bấm \"HOÀN TẤT\"");
                e.Cancel = true;
            }
        }

        private void XtraForm_QuyDoiTrangThai_Load(object sender, EventArgs e)
        {
            
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}