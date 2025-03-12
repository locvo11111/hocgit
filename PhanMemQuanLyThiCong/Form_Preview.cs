using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit;
//using DevExpress.XtraPdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MWORD = DevExpress.XtraRichEdit.API.Native;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_Preview : Form
    {

       
            public Form_Preview()
        {
            InitializeComponent();
        }

        private void Form_Preview_Load(object sender, EventArgs e)
        {

        }


        //private void xemTruocWord()
        //{
        //    //RichEditControl word = new RichEditControl();
        //    word.Dock = DockStyle.Fill;
        //    Form_Preview fm_xt = new Form_Preview();
        //    fm_xt.Controls.Add(word);
        //    try
        //    {
        //        word.LoadDocument($@"{m_path}\FileWord\[Mẫu] BC tiến độ thi công tháng.doc");
        //        fm_xt.ShowDialog();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageShower.ShowInformation("Lỗi");
        //    }
        //}
    }
}
