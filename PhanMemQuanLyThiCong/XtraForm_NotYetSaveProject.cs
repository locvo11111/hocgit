using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Helper;
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
    public partial class XtraForm_NotYetSaveProject : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DE_OPENBACKUP(string path);
        public DE_OPENBACKUP OpenBackup;

        bool _isFstTime = false;
        public XtraForm_NotYetSaveProject(bool fstTime = false)
        {
            InitializeComponent();
            _isFstTime = fstTime;
        }

        private void LoadAllFile(bool isFstTime = false)
        {
            string pathBackupFile = BaseFrom.m_UnsavedPath;
            if (Directory.Exists(pathBackupFile))
            {

                string[] dirs = Directory.GetFiles(pathBackupFile);
                if (dirs.Length > 0)
                {
                    if (isFstTime)
                    {
                        MessageShower.ShowInformation("Bạn có các dự án chưa được lưu lại. Vui lòng chọn để tiếp tục làm việc hoặc Xóa các dự án để GIẢI PHÓNG BỘ NHỚ");
                    }
                    List<UnsavedProject> lsUnsaved = new List<UnsavedProject>();
                    int stt = 0;
                    foreach (string dir in dirs)
                    {
                        lsUnsaved.Add(new UnsavedProject()
                        {
                            STT = ++stt,
                            FullPath = dir,
                            Modified = Directory.GetLastWriteTime(dir)
                        });
                    }
                    tl_Unsaved.DataSource = lsUnsaved;
                }
                else if (isFstTime)
                {
                    Close();
                }

            }
        }

        private void bt_DeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageShower.ShowYesNoQuestion("Tất cả File chưa lưu sẽ bị xóa?") == DialogResult.Yes)
            {
                try
                {
                    string pathBackupFile = $"{BaseFrom.m_UnsavedPath}";
                    if (Directory.Exists(pathBackupFile))
                    {
                        MyFunction.DirectoryDelete(pathBackupFile);
                    }
                    MessageShower.ShowInformation("Đã xóa tất cả!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageShower.ShowError($"Lỗi xóa dự án: {ex.Message}_Inner: {ex.InnerException?.Message}");
                }

                LoadAllFile();
            }
        }

        private void bt_Open_Click(object sender, EventArgs e)
        {
            UnsavedProject up = tl_Unsaved.GetFocusedRow() as UnsavedProject;
            if (up is null)
            {
                MessageShower.ShowWarning("Vui lòng chọn Dự án muốn mở!");
                return;
            }

            OpenBackup(up.FullPath);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void repoBt_Xoa_Click(object sender, EventArgs e)
        {

        }

        private void XtraForm_NotYetSaveProject_Load(object sender, EventArgs e)
        {
            LoadAllFile(_isFstTime);
        }
    }
}