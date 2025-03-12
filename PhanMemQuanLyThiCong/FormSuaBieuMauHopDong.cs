using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class FormSuaBieuMauHopDong : Form
    {
       // Đăt biến
        string m_path;
        public FormSuaBieuMauHopDong()
        {
            InitializeComponent();
           // Tạo đường dẫn file Word - Excel
            m_path = Application.StartupPath;
        }
        
        private void cbo_SHD_ChonLoaiHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Thoát khi không tìm được
            if (cbo_SHD_ChonLoaiHopDong.SelectedIndex < 0)
                return;
            // Tạo đường dẫn thư mục file
            string filePath = $@"{m_path}\FileWord\{cbo_SHD_ChonLoaiHopDong.SelectedItem.ToString()}.doc";
            //Điều kiện mở thoát file
            if (File.Exists(filePath))
            {
                word_suabieumau.LoadDocument(filePath);
            }
            // Thông báo file không tồn tại
            else
            {
                MessageShower.ShowInformation("File không tồn tại");
            }
        }
    }
}
