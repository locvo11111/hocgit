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
    public partial class XtraFormLuaChonDuyet : DevExpress.XtraEditors.XtraForm
    {
        public XtraFormLuaChonDuyet()
        {
            InitializeComponent();
        }
        public bool _CheckPhieuDuyet { get { return ce_PhieuDuyet.Checked; }  }
        public bool Duyet1Buoc { get; set; } = false;
        public bool DuyetTheoQuyTrinh { get; set; } = false;
        public bool CancelSelect { get; set; } = false;

        private void sb_Cancel_Click(object sender, EventArgs e)
        {
            CancelSelect = true;
            this.Close();
        }

        private void sb_DuyetTheoQuyTrinh_Click(object sender, EventArgs e)
        {
            DuyetTheoQuyTrinh = true;
            this.Close();
        }

        private void sb_Duyet1Buoc_Click(object sender, EventArgs e)
        {
            Duyet1Buoc = true;
            this.Close();
        }

        private void XtraFormLuaChonDuyet_Load(object sender, EventArgs e)
        {
            Duyet1Buoc = DuyetTheoQuyTrinh = CancelSelect = false;
        }

        private void XtraFormLuaChonDuyet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Duyet1Buoc && !DuyetTheoQuyTrinh)
                CancelSelect = true;
        }
    }
}