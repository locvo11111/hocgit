using DevExpress.Utils.Drawing;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraSpreadsheet;
using PhanMemQuanLyThiCong.ChatBox;
//using PM360.Common;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
//using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;

namespace PhanMemQuanLyThiCong.Function
{
    public static class ChatFunction
    {
        private static string api = ConnextService.UriChat;
        private static string projectDirectory = $@"{BaseFrom.m_tempPath}\word\";
        public static int xemTruocFileCoBan(string filename,Control parent = null, string api = null, byte[] content = null)
        {
            if (parent != null)
            {
                foreach (Control ctrl in parent.Controls)
                {
                    ctrl.Dispose();
                }
            }
            string extension = Path.GetExtension(filename);


            if (CommonConstants.GetFileDocExt().Contains(extension))
            {
                ChatFunction.xemTruocWord(filename, parent, api, content);
            }
            else if (CommonConstants.GetFileExcelExt().Contains(extension))
            {
                ChatFunction.xemTruocEXECL(filename, parent, api, content);
            }
            else if (CommonConstants.GetFilePdfExt().Contains(extension))
            {
                ChatFunction.xemTruocPDF(filename, parent, api, content);
            }
            else if (CommonConstants.GetFileImgExt().Contains(extension))
            {
                ChatFunction.xemTruocHINHANH(filename, parent, api, content);
            }

            else return 1;

            return 0;
        }

        public static void xemTruocWord(string filename, Control parent = null, string api = null, byte[] content = null)
        {
            RichEditControl word = new RichEditControl();
            word.Dock = DockStyle.Fill;
            word.ReadOnly = true;
            
            try
            {
                Fcn_wordStreamDocument(filename, word, api, content);
            }
            catch
            {
                MessageShower.ShowInformation("Lỗi tải file");
            }

            if (parent != null)
                parent.Controls.Add(word);
            else
            {
                using (Form fm_xt = new Form())
                {
                    fm_xt.Controls.Add(word);
                    fm_xt.WindowState = FormWindowState.Maximized;
                    fm_xt.Text = "Xem trước: " + Path.GetFileName(filename);
                    fm_xt.ShowDialog();
                }
            }
        }

        private async static void Fcn_wordStreamDocument(string filename, RichEditControl word, string api = null, byte[] content = null)
        {
            try
            {
                if (content != null)
                {
                    word.LoadDocument(content);
                    return;
                }
                if (api != null)
                {
                    string urlFile = api;// $@"{api}{filename}";
                    HttpClient client = new HttpClient();
                    var res = await client.GetStreamAsync(urlFile);
                    word.LoadDocument(res);
                    res.Close();
                    res.Dispose();
                }
                else
                {
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    word.LoadDocument(fs);
                    fs.Close();
                    fs.Dispose();
                };
            }
            catch (Exception ex)
            {
                MessageShower.ShowError("Lỗi xem trước file. Kiểm tra loại file!");

            }

        }


        public static void xemTruocEXECL(string filename, Control parent = null, string api = null, byte[] content = null)
        {
            SpreadsheetControl spsheet = new SpreadsheetControl();
            spsheet.Dock = DockStyle.Fill;

            spsheet.ReadOnly = true;

            try
            {
                Fcn_spSheetStreamDocument(spsheet, filename, api, content);
            }
            catch
            {
                MessageShower.ShowInformation("Lỗi tải file");
            }

            if (parent != null)
                parent.Controls.Add(spsheet);
            else
            {
                using (Form fm_xt = new Form())
                {
                    fm_xt.Controls.Add(spsheet);
                    fm_xt.WindowState = FormWindowState.Maximized;
                    fm_xt.Text = "Xem trước: " + Path.GetFileName(filename);
                    fm_xt.ShowDialog();
                }
            }
        }

        private async static void Fcn_spSheetStreamDocument(SpreadsheetControl spsheet, string filename, string api = null, byte[] content = null)
        {
            try
            {
                if (content != null)
                {
                    spsheet.LoadDocument(content);
                    return;
                }
                if (api != null)
                {
                    string urlFile = api;// api;// $@"{api}{filename}";
                    HttpClient client = new HttpClient();
                    var res = await client.GetStreamAsync(urlFile);
                    spsheet.LoadDocument(res);
                    res.Close();
                    res.Dispose();
                }
                else
                {
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    spsheet.LoadDocument(fs);
                    fs.Close();
                    fs.Dispose();
                };
            }
            catch (Exception ex)
            {
                MessageShower.ShowError("Lỗi xem trước file. Kiểm tra loại file!");

            }
        }

        public static void xemTruocPDF(string filename, Control parent = null, string api = null, byte[] content = null)
        {
            PdfViewer pdf = new PdfViewer();
            pdf.Dock = DockStyle.Fill;
            pdf.ReadOnly = true;

            try
            {
                Fcn_PdfStreamDoc(pdf, filename, api, content);
            }
            catch
            {
                MessageShower.ShowInformation("Lỗi tải file");
            }

            if (parent != null)
                parent.Controls.Add(pdf);
            else
            {
                using (Form fm_xt = new Form())
                {
                    fm_xt.Controls.Add(pdf);
                    fm_xt.WindowState = FormWindowState.Maximized;
                    fm_xt.Text = "Xem trước: " + Path.GetFileName(filename);
                    fm_xt.ShowDialog();
                }
            }
            
        }

        private async static void Fcn_PdfStreamDoc(PdfViewer pdf, string filename, string api = null, byte[] content = null)
        {
            try
            {
                if (content != null)
                {
                    pdf.LoadDocument(new MemoryStream(content));
                    return;
                }
                if (api != null)
                {
                    string urlFile = api;// $@"{api}{filename}";
                    HttpClient client = new HttpClient();
                    var res = await client.GetStreamAsync(urlFile);
                    pdf.LoadDocument(res);
                    res.Close();
                    res.Dispose();
                }
                else
                {
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    pdf.LoadDocument(fs);
                    fs.Close();
                    fs.Dispose();
                };
            }
            catch (Exception ex)
            {
                MessageShower.ShowError("Lỗi xem trước file. Kiểm tra loại file!");

            }
        }

        public static void xemTruocHINHANH(string filename, Control parent = null, string api = null, byte[] content = null)
        {

            PictureBox hinh = new PictureBox();

            hinh.SizeMode = PictureBoxSizeMode.StretchImage;
            hinh.Dock = DockStyle.Fill;
            try
            {

                Fcn_ImageStreamDoc(hinh, filename, api, content);
            }
            catch (Exception ex)

            {
                MessageShower.ShowInformation("Lỗi tải file");
            }

            if (parent == null)
            {
                using (Form fm_xt = new Form())
                {
                    fm_xt.Controls.Add(hinh);
                    fm_xt.ShowDialog();
                }
            }
            else
            {
                parent.Controls.Add(hinh);
            }
        }

        private async static void Fcn_ImageStreamDoc(PictureBox hinh, string filename, string api = null, byte[] content = null)
        {

            try
            {
                if (content != null)
                {
                    hinh.Image = Image.FromStream(new MemoryStream(content));
                    return;
                }
                if (api != null)
                {
                    string urlFile = api;// $@"{api}{filename}";
                    HttpClient client = new HttpClient();
                    var res = await client.GetStreamAsync(urlFile);
                    hinh.Image = Image.FromStream(res);
                    res.Close();
                    res.Dispose();
                }
                else
                {
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    hinh.Image = Image.FromStream(fs);
                    fs.Close();
                    fs.Dispose();
                };
            }
            catch (Exception ex)
            {
                MessageShower.ShowError("Lỗi xem trước file. Kiểm tra loại file!");

            }
        }
    }
}
