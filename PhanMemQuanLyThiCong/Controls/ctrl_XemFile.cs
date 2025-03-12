using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Function;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class ctrl_XemFile : UserControl
    {
        List<FileViewModel> _files = new List<FileViewModel>();
        public ctrl_XemFile()
        {
            InitializeComponent();
        }

        public void LoadFiles(IEnumerable<FileViewModel> files)
        {
            if (files == null)
            {
                scroll_ListFile.Controls.Clear();
                pn_xemTruocFile.Controls.Clear();
                return;
            }
            _files = files.ToList();
            foreach (Control ctrl in scroll_ListFile.Controls)
            {
                ctrl.Dispose();
            }
            scroll_ListFile.Controls.Clear();
            
            
            foreach (Control ctrl in pn_xemTruocFile.Controls)
            {
                ctrl.Dispose();
            }
            pn_xemTruocFile.Controls.Clear();

            //_files = files.Where(f => f.EndsWith(".jpg")).ToArray();
            int x = 0, y = 0;
            for (int i = 0; i < _files.Count(); i++)
            {
                FileViewModel file = _files[i];
                PictureEdit pic = new PictureEdit();
                pic.Name = $"{this.Name}pic_{i}";
                pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                pic.Width = 110;
                pic.Height = 65;
                pic.Location = new Point(x, y);
                pic.Cursor = Cursors.Hand;
                pic.ToolTip = file.FileNameDisplay;

                scroll_ListFile.Controls.Add(pic);
                pic.Click += fileIcon_Click;
                x = x + 120;

                //Image = "";
                string extension = Path.GetExtension(file.FileNameInDb??file.FileName);



                if (CommonConstants.GetFileDocExt().Contains(extension))
                {
                    pic.Image = (Image)Properties.Resources.word;

                }
                else if (CommonConstants.GetFileExcelExt().Contains(extension))
                {
                    pic.Image = (Image)Properties.Resources.word;

                }
                else if (CommonConstants.GetFilePdfExt().Contains(extension))
                {
                    pic.Image = (Image)Properties.Resources.xls;
                }
                else if (CommonConstants.GetFileCsvExt().Contains(extension))
                {
                    pic.Image = (Image)Properties.Resources.csv;
                }
                else if (CommonConstants.GetFileImgExt().Contains(extension))
                {
                    pic.Image = (Image)Properties.Resources.pdf;
                }

                else pic.Image = (Image)Properties.Resources.NotSp;
                
            }
        }

        private void fileIcon_Click(object sender, EventArgs args)
        {
            PictureEdit pic = sender as PictureEdit;
            //ComboBox cbb = sender as ComboBox;

            //if (cbb.SelectedIndex < 0)
            //    return;
            FileViewModel file = _files[int.Parse(pic.Name.Split('_').Last())];
            if (!File.Exists(file.FilePath))
            {
                MessageShower.ShowInformation("Không tìm thấy file xem trước");
                return;
            }

            if (MyFunction.xemTruocFileCoBan(file, pn_xemTruocFile) != 0)
            {
                MessageShower.ShowInformation("File đã chọn không hỗ trợ xem trước");
                return;
            }
            pn_xemTruocFile.Controls[0].MouseMove += pn_xemTruocFile_MouseMove;
        }

        private void pn_xemTruocFile_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Y > ((sender as Control).Height - scroll_ListFile.Height))
                scroll_ListFile.Visible = true;
            else
                scroll_ListFile.Visible = false;
        }
    }
}
