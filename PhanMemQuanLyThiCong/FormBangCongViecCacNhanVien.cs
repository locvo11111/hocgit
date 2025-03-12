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
    public partial class FormBangCongViecCacNhanVien : Form
    {
       //Tạo đường dẫn mở file excel
        string m_path;
        public FormBangCongViecCacNhanVien()
        {
            InitializeComponent();
           // chạy khi mở fom
            fcn_init();
        }

        private void fcn_init()
        {
            string path = Directory.GetCurrentDirectory(); // Nhận đường dẫn
            // Debug.WriteLine($"MY PATH: {path}");
            m_path = $@"{Application.StartupPath}\..\..\..\Template"; //Chạy file excel khi mở fom
            spsheet_BangChiTietGiaoViecNhanVien.LoadDocument($@"{ m_path}\FileExcel\12.gBangTienDoGiaoViecChiTietNhanVien.xls"); // Hiển thị bảng giao việc các nhân viên tại bảng công việc
            //----//
           
                        // Nhập thông tin công trình //
           // spsheet_Thongtinchinh.LoadDocument($@"{m_path}\FileExcel\1.aThongTinDuTanDayDu.xls"); // Tên lưới excel trong bảng (tự đặt ví dụ spsheet_Thongtinchinh) và tên file excel đọc vào (ví dụ:1.aThongTinCoBan.xlsx). 
                                                                                                  //
        }

        private void FormBangCongViecCacNhanVien_Load(object sender, EventArgs e)
        {

        }

        private void FormBangCongViecCacNhanVien_FormClosed(object sender, FormClosedEventArgs e)
        {
            spsheet_BangChiTietGiaoViecNhanVien.Dispose();
            GC.Collect();
        }
    }
}
