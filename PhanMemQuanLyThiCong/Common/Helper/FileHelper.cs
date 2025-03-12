//using ;
using DevExpress.CodeParser;
using DevExpress.LookAndFeel;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Implementation;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpreadsheet;
//using log4net;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class FileHelper
    {

        public static byte[] GetBytes(string path)
        {
            if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
            string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(path)}";
            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Copy(path, newPath);
            var bytes = File.ReadAllBytes(newPath);
            File.Delete(newPath);
            return bytes;
        }
        public static void fcn_spSheetStreamDocument(SpreadsheetControl spSheet, string path)
        {
            if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
            string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(path)}";
            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Copy(path, newPath);
            FileStream fs = new FileStream(newPath, FileMode.Open, FileAccess.Read);
            //spSheet.CreateNewDocument();
            spSheet.LoadDocument (fs);                    
            fs.Close();
            fs.Dispose();
            if (spSheet.Document.Options.Culture.Name != "vi-VN")
                spSheet.Document.Options.Culture = MyConstant.culture;
            File.Delete(newPath);
            GC.Collect();
        }
        
        public static void fcn_spSheetStreamDocument(Workbook spSheet, string path)
        {
            if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
            string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(path)}";
            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Copy(path, newPath);
            FileStream fs = new FileStream(newPath, FileMode.Open, FileAccess.Read);
            //spSheet.CreateNewDocument();
            spSheet.LoadDocument (fs);                    
            fs.Close();
            fs.Dispose();
            spSheet.Options.Culture = MyConstant.culture;
            File.Delete(newPath);
            GC.Collect();
        }
        
        

        public static void fcn_wordStreamDocument(RichEditControl word, string path)
        {
            if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
            string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(path)}";
            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Copy(path, newPath);
            FileStream fs = new FileStream(newPath, FileMode.Open, FileAccess.Read);
            word.LoadDocument(fs);
            fs.Close();
            fs.Dispose();
            //word.Document.Options.Culture = MyConstant.culture;
            File.Delete(newPath);
        }
        public static void fcn_PdfStreamDoc(PdfViewer pdfview, string path)
        {
            if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
            string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(path)}";
            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Copy(path, newPath);
            FileStream fs = new FileStream(newPath, FileMode.Open, FileAccess.Read);
            pdfview.LoadDocument(fs);
            fs.Close();
            fs.Dispose();
            //word.Document.Options.Culture = MyConstant.culture;
            File.Delete(newPath);
        }
        

        public static void fcn_ImageStreamDoc(PictureBox pic, string path)
        {
            if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
            string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(path)}";
            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Copy(path, newPath);
            FileStream fs = new FileStream(newPath, FileMode.Open, FileAccess.Read);
            pic.Image = Image.FromStream(fs);
            fs.Close();
            fs.Dispose();
            //word.Document.Options.Culture = MyConstant.culture;
            File.Delete(newPath);
        }
        public static Image fcn_ImageStreamDoc(PictureEdit picEdit, string path)
        {
            if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
            string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(path)}";
            if (File.Exists(newPath))
                File.Delete(newPath);

            Image img = null;
            if (!File.Exists(path))
            {
                AlertShower.ShowInfo("Không tìm thấy file xem trước");
                img = Properties.Resources.NoImage;
            }
            else
            {
                File.Copy(path, newPath);
                FileStream fs = new FileStream(newPath, FileMode.Open, FileAccess.Read);
                img = Image.FromStream(fs);
                fs.Close();
                fs.Dispose();
                //word.Document.Options.Culture = MyConstant.culture;
                File.Delete(newPath);
            }
            if (picEdit != null)
                picEdit.Image = img;
            return img;
        }

        public static Image fcn_ImageStreamDoc(string path)
        {
            try
            {

                if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                    Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
                string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(path)}";
                if (File.Exists(newPath))
                    File.Delete(newPath);

                Image img = null;
                if (!File.Exists(path))
                {
                    //AlertShower.ShowInfo("Không tìm thấy file xem trước");
                    img = Properties.Resources._2_MTC;
                }
                else
                {
                    File.Copy(path, newPath);
                    FileStream fs = new FileStream(newPath, FileMode.Open, FileAccess.Read);
                    img = Image.FromStream(fs);
                    fs.Close();
                    fs.Dispose();
                    File.Delete(newPath);
                }
                return img;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<ByteArrayContent> ToByteArrayContent(this string file)
        {
            if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
            string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(file)}";
            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Copy(file, newPath);
            ByteArrayContent fileContent;// = byte

            using (FileStream fs = File.OpenRead(newPath))
            {
                var streamContent = new StreamContent(fs);
                File.Delete(newPath);
                fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());
            }

            return fileContent;
        }

        public static string CalculateFileMD5(string filename)
        {
            if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
                Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
            string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(filename)}";
            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Copy(filename, newPath);

            string md5String = "";
            using (var stream = File.OpenRead(newPath))
            {

                md5String = CalculateStreamMD5(stream);
                
            }

            File.Delete(newPath);
            return md5String;
        }

        public static string CalculateStreamMD5(FileStream stream)
        {
            string md5String = "";
            using (var md5 = MD5.Create())
            {
                md5String = BitConverter.ToString(md5.ComputeHash(stream));
            }
            return md5String;
        }
        public static string RemoveInvalidChars(this string filename)
        {
            return string.Concat(filename.Split(Path.GetInvalidFileNameChars()));
        }
        public static void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            PrintableComponentLink link = new PrintableComponentLink()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = true,                  
                PaperKind = PaperKind.A4,
                Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20),
                
            };
            //link.CreateReportHeaderArea += link_CreateReportHeaderArea;
            link.PrintingSystemBase.Document.AutoFitToPagesWidth = 1;        
            WaitFormHelper.CloseWaitForm();
            link.ShowRibbonPreview(lookAndFeel);
        }
        //public static async Task<ByteArrayContent> ReadByteArrayContent(string filename)
        //{
        //    if (!Directory.Exists($@"{BaseFrom.m_path}\TempFile"))
        //        Directory.CreateDirectory($@"{BaseFrom.m_path}\TempFile");
        //    string newPath = $@"{BaseFrom.m_path}\TempFile\{Path.GetFileName(filename)}";
        //    if (File.Exists(newPath))
        //        File.Delete(newPath);
        //    File.Copy(filename, newPath);

        //    FileStream fs = File.OpenRead(newPath);
        //    string Content = Path.GetFileName(newPath);
        //    var content = new StreamContent(fs);
        //    var fileContent = new ByteArrayContent(await content.ReadAsByteArrayAsync());

        //    File.Delete(newPath);

        //    return fileContent;
        //}

    }
}
