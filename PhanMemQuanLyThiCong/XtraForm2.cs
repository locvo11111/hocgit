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
    public partial class XtraForm2 : DevExpress.XtraEditors.XtraForm
    {        
        public XtraForm2(string name,string DonVi=default)
        {
            InitializeComponent();
            this.Text = name;
            textEdit2.Enabled = true;
            if (DonVi != default)
            {
                textEdit2.Text = DonVi;
                textEdit2.Enabled = false;
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

        }
    }
}