using DevExpress.Pdf.Native;
using DevExpress.Utils;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraSpreadsheet.Model;
using Microsoft;
using Microsoft.Win32;
using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Securety;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Controls.Approval;
using PhanMemQuanLyThiCong.Controls.MTC;
using PhanMemQuanLyThiCong.KanbanModule;
using PhanMemQuanLyThiCong.Mapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PhanMemQuanLyThiCong
{
    static class Program
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dw1, IntPtr dw2);

        public static string Root = AppDomain.CurrentDomain.BaseDirectory;
        public static SvgImageCollection SvgImages
        {
            get;
            private set;
        }

        static Program()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;

            SvgImages = SvgImageCollection.FromResources("PhanMemQuanLyThiCong.Resources.Svg", typeof(Program).Assembly);
            //RemoveAccent();

        }

        static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            string partialName = AssemblyHelper.GetPartialName(args.Name).ToLowerInvariant();
            if (partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6")
            {
                string path = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "..\\..\\bin", partialName + ".dll");
                return Assembly.LoadFrom(path);
            }
            return null;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            Common.MLogging.Logging.Info($"N'\r\n\r\n=================*******KHỞI ĐỘNG PHẦN MỀM*******==========================\r\n", "");
            CheckduplicateApp();
            if (!TongHopHelper.IsAutoTime())
            {
                MessageShower.ShowError("Vui lòng bật chế độ đồng bộ thời gian với server để sử dụng phần mềm!");
                Environment.Exit(0);
            }

            ConfigUnity.Register();
    
            MapCofig.Mapping();

            var culture = CultureInfo.GetCultureInfo("vi-VN");

            // this may fail sometimes: (see Drachenkatze's comment below)
            // var culture = new CultureInfo("en-US");

            //Culture for any thread
            CultureInfo.DefaultThreadCurrentCulture = culture;

            //Culture for UI in any thread
            CultureInfo.DefaultThreadCurrentUICulture = culture;


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            initBaseFrom();

            if (!IsRegistryKeyAssociated())
                SetRegistryKeyAssociate();

            CheckOSWindow();

            //UpdateChecker();
            //Application.Run(new XtraForm_MayThiCong());

//#if DEBUG
//            CusHttpClient.InstanceCustomer.BaseAddress = "http://localhost:5000/api/";
//            PhanMemQuanLyThiCong.Properties.Settings.Default.TokenTBT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJpbmhidWlia0BnbWFpbC5jb20iLCJGdWxsTmFtZSI6IkLDuWkgVsSDbiBCw6xuaCIsIlNlcmlhbE5vIjoiVEJUMzYwIiwicm9sZSI6IiIsInVuaXF1ZV9uYW1lIjoiYWRtaW4xIiwibmJmIjoxNzA1MjAzMDAxLCJleHAiOjE3MDUyMjQ2MDEsImlhdCI6MTcwNTIwMzAwMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NjAwMCIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjYwMDAifQ.je3-JToaWf4QMHIaMDzoel6p1zRfMZ6ITwC1kaB9oY8";
//            PhanMemQuanLyThiCong.Properties.Settings.Default.Save();
//            Application.Run(new XtraForm_AlertTimeSetting());
//            return;
//#endif
            if (args.Length == 0)
                Application.Run(new PhanMemQuanLyThiCong360());
            else
                Application.Run(new PhanMemQuanLyThiCong360(string.Join(" ", args)));
            //Application.Run(new PhanMemQuanLyThiCong360());
            //Application.Run(new XtraForm_NhapKhoiLuongHangNgay());
        }


        
        private static void CheckduplicateApp()
        {
            //foreach (DriveInfo di in DriveInfo.GetDrives())
            //{
            //    //lstDriveLetters.Add(di.Name);
            //}
            Process crProc = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcesses();

            foreach (Process p in procs)
            {

                try
                {
                    
                   if ( p.Id != crProc.Id && (p.MainModule.FileName == crProc.MainModule.FileName))
                    {
                        MessageShower.ShowError("Phần mềm đã khởi chạy trước đó!");
                        Environment.Exit(0);
                    }

                }
                catch { continue; }
            }
        }

        public static bool IsRegistryKeyAssociated()
        {
            var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.qltc", false);
            if (key is null)
                return true;

            key.Close();

            return false;
        }

        public static void SetRegistryKeyAssociate()
        {
            RegistryKey fileReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\.qltc");
            RegistryKey AppReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Applications\\PhanMemQuanLyThiCong.exe");
            RegistryKey AppAssoc = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.qltc");
            fileReg.CreateSubKey("DefaultIcon").SetValue("", Root + "QLTC.ico");
            fileReg.CreateSubKey("PerceivedType").SetValue("", "Text");
            AppReg.CreateSubKey("shell\\open\\command").SetValue("", "\"" + Application.ExecutablePath + "\"%1");
            AppReg.CreateSubKey("shell\\edit\\command").SetValue("", "\"" + Application.ExecutablePath + "\"%1");
            AppReg.CreateSubKey("DefaultIcon").SetValue("", Root + "QLTC.ico");
            if (AppAssoc.OpenSubKey("UserChoice", false) == null)
                AppAssoc.CreateSubKey("UserChoice").SetValue("Progid", "Application\\PhanMemQuanLyThiCong.exe");
            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }

        private static void CheckOSWindow()
        {
            //If win11os
            if (Environment.OSVersion.Version.Build > 20000)
            {
                InitWin32Bit();
            }
            else
            {
                //Win 64bit
                if (IntPtr.Size == 8)
                {
                    InitWin64Bit();
                }
                else
                {
                    InitWin32Bit();
                }
            }
        }

        private static void InitWin32Bit()
        {
            if (!File.Exists(Root + @"SDX.dll"))
                File.Copy(Root + @"\x86\SDX.dll", Root + @"SDX.dll");
            if (!File.Exists(Root + @"SDX.lib"))
                File.Copy(Root + @"\x86\SDX.lib", Root + @"SDX.lib");
            if (!File.Exists(Root + @"SecureDongle_Control.dll"))
                File.Copy(Root + @"\x86\SecureDongle_Control.dll", Root + @"SecureDongle_Control.dll");
        }
        private static void InitWin64Bit()
        {
            if (!File.Exists(Root + @"SDX.dll"))
                File.Copy(Root + @"\x64\SDX.dll", Root + @"SDX.dll");
            if (!File.Exists(Root + @"SDX.lib"))
                File.Copy(Root + @"\x64\SDX.lib", Root + @"SDX.lib");
            if (!File.Exists(Root + @"SecureDongle_Control.dll"))
                File.Copy(Root + @"\x64\SecureDongle_Control.dll", Root + @"SecureDongle_Control.dll");
        }

        private static void CheckTimeSyncAutomaticaly()
        {

        }

        private static void initBaseFrom()
        {
            BaseFrom.m_tempPath = AppDomain.CurrentDomain.BaseDirectory + @"\Temp";
            BaseFrom.m_UnsavedPath = AppDomain.CurrentDomain.BaseDirectory + @"\Unsaved";
            BaseFrom.m_TempFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\TempFile";
            
            if (Directory.Exists(BaseFrom.m_TempFilePath))
            {
                MyFunction.DirectoryDelete(BaseFrom.m_TempFilePath);
            }
                Directory.CreateDirectory(BaseFrom.m_TempFilePath);
            //BaseFrom.m_templatePath = BaseFrom.m_path + @"\Template";

            if (!Directory.Exists(BaseFrom.m_tempPath))
                Directory.CreateDirectory(BaseFrom.m_tempPath);

            if (!Directory.Exists(BaseFrom.m_UnsavedPath))
                Directory.CreateDirectory(BaseFrom.m_UnsavedPath);


#if DEBUG
            BaseFrom.m_path = $@"{AppDomain.CurrentDomain.BaseDirectory}\..\..\.."; //Chạy file excel
            BaseFrom.m_resourceChatPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\..\..\ChatBox\Resource\";
#else
            BaseFrom.m_path = $@"{AppDomain.CurrentDomain.BaseDirectory}";
                                    BaseFrom.m_resourceChatPath = $@"{BaseFrom.m_path}\Resource";

#endif
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(BaseFrom.m_path, "Database"));
            DataProvider.InstanceTBT.changePath($@"{BaseFrom.m_path}\Database\DatabaseTBT.sqlite3");
            DataProvider.InstanceServer.changePath($@"{BaseFrom.m_path}\Database\ServerData.sqlite3");


            //string dbString2 = $"INSERT INTO test (so) VALUES (' @So ')";
            //DataProvider.InstanceTBT.ExecuteNonQuery(dbString, new object[] { a });

            //fcn_init();

            BaseFrom.SerialNumberHDD = ConfigHelper.GetSerialHDD();
        }

    }
}
