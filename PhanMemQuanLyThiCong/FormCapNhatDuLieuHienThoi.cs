using Dapper;
using DevExpress.Utils.Extensions;
using Microsoft.AspNetCore.SignalR.Client;
using PhanMemQuanLyThiCong.ChatBox;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.Dto;
using VChatCore.ViewModels.SyncSqlite;
//using VChatCore.Model;

namespace PhanMemQuanLyThiCong
{
    public partial class FormCapNhatDuLieuHienThoi : Form
    {

        //DataProvider m_db = new DataProvider("");
        public FormCapNhatDuLieuHienThoi()
        {
            InitializeComponent();
        }

        private void btnThoatDeSau_Click(object sender, EventArgs e)
        {
            // Tắt cập nhật dữ liệu hiện thời
            this.Close();
        }

        private void bt_Upload2Server_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhật");
            if (Task.Run(async () => await SharedProjectHelper.UploadAllSQLiteData2ServerIn1Post(cb_OnlyChanged.Checked)).Result)
            {
                //if (await SharedProjectHelper.AddUserToHubGiaoViec())
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowInformation("Tải lên hoàn tất");
                //else
                //    MessageShower.ShowWarning("Tải lên hoàn tất! LỖI khởi tạo nhóm chat!");

            }
            else
            {
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowError("Tải lên không thành công!\r\nVui lòng tải lên toàn bộ dữ liệu (Bỏ dấu check tải dữ liệu thay đổi) hoặc kiểm tra lại server!");
            }
        }


        private void btnCapNhatNgay_Click(object sender, EventArgs e)
        {
            //MessageShower.ShowYesNoCancelQuestionWithCustomText("")
        }

        private void btnCapNhatDanhSachDuAn_Click(object sender, EventArgs e)
        {
            DialogResult = MessageShower.ShowYesNoCancelQuestionWithCustomText("Vui lòng chọn hình thức cập nhật danh sách dự án!", "Lựa chọn",
                "Cập nhật vào file Tổng dự án hiện tại (Giữ nguyên các dự án cũ)",
                "Lưu lại file dự án hiện tại và cập nhật dữ liệu vào file mới");

            if (DialogResult != DialogResult.Cancel) 
            {
                this.Close();
            }
        }
    }
}
