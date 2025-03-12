using DevExpress.Utils.CommonDialogs;
using DevExpress.XtraEditors;

using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace PhanMemQuanLyThiCong.ChatBox.Views
{
    public partial class AddFile : DevExpress.XtraEditors.XtraForm
    {
        private IChatService _chatService;
        private List<string> files = new List<string>();

        public List<string> Files  = new List<string>();
        private string fileChose = String.Empty;
        public AddFile()
        {
            _chatService = ConfigUnity.Container.Resolve<IChatService>();
            InitializeComponent();
        }

        private void btn_themmoifile_Click(object sender, EventArgs e)
        {
            //Mở file để chọn các file ảnh gửi 
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //Tao duong dan file sao luu//
                    foreach (string file in dlg.FileNames)
                    {
                        if (!Files.Contains(file))
                        {
                            Files.Add(file);
                        }
                    }

                };
                check_filedachon.DataSource = Files;
               
            }
        }

        private void check_filedachon_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            var file = check_filedachon.SelectedItem as string;
            if (file != null)
            {
                bool check = check_filedachon.GetItemChecked(check_filedachon.SelectedIndex);
                if (check)
                {
                    fileChose = file;
                }
            }
        }

        private void btn_xoafile_Click(object sender, EventArgs e)
        {
            files.Remove(fileChose);
            check_filedachon.DataSource = files;
        }

        private void btn_huybofile_Click(object sender, EventArgs e)
        {
            files = new List<string>();
            Files = new List<string>();
            this.Close();
        }

        private void AddFile_Load(object sender, EventArgs e)
        {
            //if (ConnextService.files.Count > 0)
            //{
            //    check_filedachon.DataSource= files = ConnextService.files;
            //}
        }
    }
}