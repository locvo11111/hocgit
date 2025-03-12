using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
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
    public partial class XtraForm_wordPreview : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_wordPreview()
        {
            InitializeComponent();
        }

        public RichEditControl GetRec()
        {
            return richEditControl1;
        }
        public void AddWord(RichEditControl richEdit)
        {
            this.Controls.Add(richEdit);
            richEdit.MenuManager = ribbonControl1;
        }
    }
}