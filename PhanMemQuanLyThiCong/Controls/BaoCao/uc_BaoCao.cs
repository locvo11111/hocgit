using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpreadsheet;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.BaoCao
{
    public partial class uc_BaoCao : DevExpress.XtraEditors.XtraUserControl
    {
        public BaoCaoFileType _type = BaoCaoFileType.WORD;
        //string _path = string.Empty;

        public uc_BaoCao()
        {
            InitializeComponent();
        }

        public SpreadsheetControl SpreadSheet
        {
            get
            {
                return spSheet;
            }
        }

        public RichEditControl RichEditControl
        {
            get
            {
                return rec;
            }
        }

        #region CUSTOM PROPERTIES
        public int PreviewSelectedIndex
        {
            get
            {
                return rg_XemTruoc.SelectedIndex;
            }
            set
            {
                rg_XemTruoc.SelectedIndex = value;
            }
        }

        public string PreviewAccessibleName
        {
            get
            {
                return rg_XemTruoc.GetAccessibleName();
            }
        }
        #endregion

        public void LoadSetting(BaoCaoFileType type)
        {
            _type = type;
            if (_type == BaoCaoFileType.WORD)
            {
                lci_Excel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lci_Word.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lci_Excel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lci_Word.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }


        public void Export(string path)
        {

        }


        public event EventHandler CustomPreviewIndexChanged
        {
            add
            {
                rg_XemTruoc.SelectedIndexChanged += value;
            }

            remove
            {
                rg_XemTruoc.SelectedIndexChanged -= value;

            }
        }

        public event EventHandler CustomExportMau
        {
            add
            {
                sb_xuatbaocao.Click += value;
            }

            remove
            {
                sb_xuatbaocao.Click -= value;

            }
        }
    }
}
