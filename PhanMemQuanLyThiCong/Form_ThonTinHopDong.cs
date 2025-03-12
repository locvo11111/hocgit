using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Model;
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
    public partial class Form_ThonTinHopDong : Form
    {
        string _codeCVCon;
        //string _dbpath;
        string m_path;
        string m_tblDbSaveFile = GiaoViec.TBL_THONGTINHOPDONG;
        double tongKL;
        //DataProvider m_db = new DataProvider("");
        
        public Form_ThonTinHopDong(string title, string codeCVCon)
        {
            InitializeComponent();
            _codeCVCon = codeCVCon;
            m_path = $@"{BaseFrom.m_tempPath}\{BaseFrom.m_crTempDATH}\Hợp đồng\{codeCVCon}";
        }

        private void Form_ThonTinHopDong_Load(object sender, EventArgs e)
        {
            string dbString = $"SELECT \"TenCongViec\", \"KhoiLuongHopDong\",\"DonVi\" FROM {GiaoViec.TBL_CONGVIECCON} WHERE \"CodeCongViecCon\"='{_codeCVCon}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count >0)
            {
                lb_TenCV.Text = dt.Rows[0]["TenCongViec"].ToString();
                lb_KLTong.Text = dt.Rows[0]["KhoiLuongHopDong"].ToString();
                lb_DonVi.Text = dt.Rows[0]["DonVi"].ToString();
                tongKL = double.Parse(lb_KLTong.Text);

            }
            fcn_loadData();
        }

        private void fcn_loadData()
        {
            dtg_luachon.DataSource = null;
            string queryStr = $"SELECT * FROM {m_tblDbSaveFile} WHERE \"CodeCongViecCon\"='{_codeCVCon}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);

            dtg_luachon.DataSource = dt;
            dtg_luachon.Columns["Xemtrước"].DisplayIndex = dtg_luachon.Columns.Count - 1;
            dtg_luachon.Columns["Thaythế"].DisplayIndex = dtg_luachon.Columns.Count - 1;
            dtg_luachon.Columns["Xoa"].DisplayIndex = dtg_luachon.Columns.Count - 1;
            foreach (DataGridViewRow row in dtg_luachon.Rows)
            {
                row.Cells["Stt"].Value = row.Index + 1;
            }
            fcn_UpdateKhoiLuong();
        }

        private void dtg_HopDong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dtg_luachon.Rows[e.RowIndex].IsNewRow)
                return;

            int hang = e.RowIndex;
            int cot = e.ColumnIndex;
            int sohang = dtg_luachon.Rows.Count;
            string tenfile = dtg_luachon.Rows[hang].Cells["FileDinhKem"].Value.ToString();
            string duongdantoifile = $@"{m_path}\{tenfile}";
            string extension = Path.GetExtension(duongdantoifile);
            try
            {
                string Task = dtg_luachon.Columns[cot].HeaderText;
                string Code = dtg_luachon.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                if (Task == "Xoa")
                {

                    if (MessageShower.ShowYesNoQuestion("Bạn có chắc muốn xóa không?", "Đang xóa...") == DialogResult.Yes)
                    {
                        dtg_luachon.Rows.RemoveAt(e.RowIndex);
                        //    File.Delete($@"{m_path}\{tenfile}");
                        //    string xoafile = $"DELETE FROM {m_tblDbSaveFile} WHERE \"Code\"='{Code}'";
                        //    DataProvider.InstanceTHDA.ExecuteQuery(xoafile);
                        //    fcn_loadData();
                    }
                }
                if (Task == "Thay thế File")
                {
                    m_openDialog.Title = "THƯ MỤC FILE ĐÍNH KÈM ";

                    if (m_openDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName;
                        fileName = m_openDialog.FileName;
                        string tenhienthi = m_openDialog.SafeFileName;
                        DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Bạn có muốn thay thế file này không ?", "Thay thế file");
                        if (dialogResult == DialogResult.Yes)
                        {
                            string crFile = dtg_luachon.Rows[e.RowIndex].Cells["FileDinhKem"].Value.ToString();
                            if (File.Exists($@"{m_path}\{crFile}"))
                            {
                                File.Delete($@"{m_path}\{crFile}");
                                //MessageShower.ShowInformation("File này đã tồn tại, vui lòng chọn file khác hoặc đổi tên file!");
                                //return;
                            }

                            if (File.Exists($@"{m_path}\Temp\{crFile}"))
                                File.Delete($@"{m_path}\Temp\{crFile}");


                            if (File.Exists($@"{m_path}\{tenhienthi}") || File.Exists($@"{m_path}\Temp\{tenhienthi}"))
                            {
                                MessageShower.ShowInformation("File này đã tồn tại, vui lòng chọn file khác hoặc đổi tên file!");
                                return;
                            }
                            else
                            {
                                if (!Directory.Exists($@"{m_path}\Temp"))
                                {
                                    Directory.CreateDirectory($@"{m_path}\Temp");
                                }
                                    
                                File.Copy(fileName, $@"{m_path}\Temp\{tenhienthi}");
                                dtg_luachon.Rows[e.RowIndex].Cells["FileDinhKem"].Value = tenhienthi;
                            }    
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            //do something else
                            return;
                        }

                        // MessageShower.ShowInformation(fileName);
                    }

                }
                if (Task == "Xem trước")
                {
                    int rowIndex = e.RowIndex;//bo sung sau khi da du thong tin
                    string fullPathFile = (File.Exists($@"{m_path}\{tenfile}")) ? $@"{m_path}\{tenfile}" : $@"{m_path}\Temp\{tenfile}";


                    if (CommonConstants.GetFileDocExt().Contains(extension))
                    {
                        MyFunction.xemTruocWord(new FileViewModel { FilePath = fullPathFile });
                    }
                    else if (CommonConstants.GetFileExcelExt().Contains(extension) || CommonConstants.GetFileCsvExt().Contains(extension))
                    {
                        MyFunction.xemTruocEXECL(new FileViewModel { FilePath = fullPathFile });

                    }
                    else if (CommonConstants.GetFilePdfExt().Contains(extension))
                    {
                        MyFunction.xemTruocPDF(new FileViewModel { FilePath = fullPathFile });
                    }
                    else if (CommonConstants.GetFileImgExt().Contains(extension))
                    {
                        MyFunction.xemTruocHINHANH(new FileViewModel { FilePath = fullPathFile });
                    }
                }

            }
            catch
            {

            }
        }

        private void dtg_luachon_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow r in dtg_luachon.Rows)
            {
                if (r.IsNewRow)
                    continue;
                if (string.IsNullOrEmpty(r.Cells["FileDinhKem"].Value.ToString()))
                    r.Cells["ThayThế"].Value = "Thêm mới File";

                r.Cells["Stt"].Value = r.Index + 1;

            }
        }

        private void bt_LuuThayDoi_Click(object sender, EventArgs e)
        {
            fcn_LuuThayDoi();
            fcn_loadData();
        }

        private void fcn_LuuThayDoi()
        {
            dtg_luachon.EndEdit();
            try
            {
                if (Directory.Exists($@"{m_path}\Temp"))
                    MyFunction.DirectoryCopy($@"{m_path}\Temp\", m_path, false);
                MyFunction.DirectoryDelete($@"{m_path}\Temp\");
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                MessageShower.ShowInformation("Lỗi thao tác file, vui lòng đóng các file và thư mục tạm!");
                return;
            }

            foreach (DataGridViewRow r in dtg_luachon.Rows)
            {
                if (r.IsNewRow)
                    continue;

                if (string.IsNullOrEmpty(r.Cells["Code"].Value.ToString()))
                    r.Cells["Code"].Value = Guid.NewGuid().ToString();

                r.Cells["CodeCongViecCon"].Value = _codeCVCon;
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource((dtg_luachon.DataSource as DataTable), m_tblDbSaveFile);
        }

        private void bt_LuuVaThoat_Click(object sender, EventArgs e)
        {
            fcn_LuuThayDoi();
            this.FormClosing -= Form_ThonTinHopDong_FormClosing;
            this.Close();
        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_ThonTinHopDong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageShower.ShowYesNoQuestion("Mọi thay đổi chưa lưu sẽ bị xóa! Bạn có muốn thoát không?", "Cảnh báo") == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }    
        }

        private void dtg_luachon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dtg_luachon.Columns[e.ColumnIndex].HeaderText == "KhoiLuongChiTiet")
            {
                double db;
                if (!double.TryParse(dtg_luachon.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out db))
                {
                    MessageShower.ShowInformation("Vui lòng nhập số thực cho ô này!");
                    dtg_luachon.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    return;
                }
                else
                {
                    if (db > tongKL)
                        dtg_luachon.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Red;
                }    
            }
        }

        private void fcn_UpdateKhoiLuong()
        {
            dtg_luachon.EndEdit();
            double tongKLTT = 0;
            foreach (DataGridViewRow r in dtg_luachon.Rows)
            {
                if (r.IsNewRow)
                    continue;

                double crKL;
                double.TryParse(r.Cells["KhoiLuongChiTiet"].Value.ToString(), out crKL);
                if (crKL > tongKL)
                {
                    r.Cells["KhoiLuongChiTiet"].Style.ForeColor = Color.Red;
                }
                tongKLTT += crKL;
            }

            lb_CanhBao.Text = $"Khối lượng: {tongKLTT}";
            lb_CanhBao.ForeColor = (tongKLTT > tongKL) ? Color.Red:Color.Blue;
        }

    }
}
