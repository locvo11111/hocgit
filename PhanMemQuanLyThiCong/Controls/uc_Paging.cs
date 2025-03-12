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

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class uc_Paging : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_Paging()
        {
            InitializeComponent();
        }

        [DisplayName("Custom Number Page")]
        public int NumberPages { get; set; }
    }
}
