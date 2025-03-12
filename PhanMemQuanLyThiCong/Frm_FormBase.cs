using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class Frm_FormBase : DevExpress.XtraEditors.XtraForm
    {
        public TypeUserControl type_frorm { get; set; }
        private Uc_ThongTinBanQuyen uc_ThongTinBanQuyen;
        private Uc_DangKyPhanMem uc_DangKyPhanMem;

        public Frm_FormBase()
        {
            InitializeComponent();
            InitControl();
        }

        public Frm_FormBase(TypeUserControl typeUser)
        {
            InitializeComponent();
            type_frorm = typeUser;
            InitControl();
        }

        private void InitControl()
        {
            panelData.Controls.Clear();
            switch (type_frorm)
            {
                case TypeUserControl.DANGKYBANQUYEN:
                    this.Text = "ĐĂNG KÝ BẢN QUYỀN";
                    uc_DangKyPhanMem = new Uc_DangKyPhanMem();
                    uc_DangKyPhanMem.Dock = DockStyle.Fill;
                    panelData.Controls.Add(uc_DangKyPhanMem);
                    this.Size = new System.Drawing.Size(550, 520);
                    break;

                case TypeUserControl.THONGTINBANQUYEN:
                    this.Text = "THÔNG TIN BẢN QUYỀN";
                    uc_ThongTinBanQuyen = new Uc_ThongTinBanQuyen();
                    uc_ThongTinBanQuyen.Dock = DockStyle.Fill;
                    //this.Size = new Size(700, 500);
                    panelData.Controls.Add(uc_ThongTinBanQuyen);
                    break;

                default:
                    break;
            }
        }

        private void Frm_FormBase_Load(object sender, EventArgs e)
        {
            //var processInfo = new ProcessStartInfo("cmd.exe", "/c " + "wmic csproduct get uuid")
            //{
            //    CreateNoWindow = true,
            //    UseShellExecute = false,
            //    RedirectStandardError = true,
            //    RedirectStandardOutput = true,
            //    WorkingDirectory = @"C:\Windows\System32\"
            //};

            //StringBuilder sb = new StringBuilder();
            //Process p = Process.Start(processInfo);
            //p.OutputDataReceived += (sende, args_) => sb.AppendLine(args_.Data);
            //p.BeginOutputReadLine();
            //p.WaitForExit();
            //string textId = sb.ToString();
        }

        private void Frm_FormBase_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}