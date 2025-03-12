using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit;
using DevExpress.XtraPdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using MWORD = DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong;
using DevExpress.XtraSpreadsheet;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Function;

namespace PhanMemQuanLyThiCong
{
    public partial class FormLuachon_HopDong : Form
    {
        OpenFileDialog dlg = new OpenFileDialog();
        //string m_path;
        string m_path1;
        //string m_tenHoSo;
        string m_tenhopdong;
        string m_codeduan;
        string m_code;
        private void fcn_init()
        {
            string path = Directory.GetCurrentDirectory(); // Nhận đường dẫn
#if DEBUG
            m_path1 = $@"{Application.StartupPath}\..\..\..\TaiLieu\Hopdong";//congvandiden
#endif
        }
        public FormLuachon_HopDong(string codeduan, string code, string tenhopdong)
        {
            InitializeComponent();
            fcn_init();
            m_codeduan = codeduan;
            m_code = code;
            m_tenhopdong = tenhopdong;
        }
        private void xemTruocWord(string filename)
        {
            RichEditControl word = new RichEditControl();
            word.Dock = DockStyle.Fill;

            Form_Preview fm_xt = new Form_Preview();
            fm_xt.Controls.Add(word);
            try
            {
                word.LoadDocument($@"{m_path1}\{m_tenhopdong}\{filename}");
                fm_xt.ShowDialog();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                MessageShower.ShowInformation("Lỗi");
            }
        }
        private void xemTruocEXECL(string filename)
        {
            DevExpress.XtraSpreadsheet.SpreadsheetControl EX = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            EX.Dock = DockStyle.Fill;
            Form_Preview fm_xt = new Form_Preview();
            fm_xt.Controls.Add(EX);
            try
            {

                EX.LoadDocument($@"{m_path1}\{m_tenhopdong}\{filename}");
                fm_xt.ShowDialog();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                MessageShower.ShowInformation("Lỗi");
            }

        }
        private void xemTruocPDF(string filename)
        {
            PdfViewer pdf = new PdfViewer();
            pdf.Dock = DockStyle.Fill;
            Form_Preview fm_xt = new Form_Preview();
            fm_xt.Controls.Add(pdf);
            try
            {

                pdf.LoadDocument($@"{m_path1}\{m_tenhopdong}\{filename}");
                fm_xt.ShowDialog();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                MessageShower.ShowInformation("Lỗi");
            }

        }
        private void xemTruocHINHANH(string filename)
        {
            PictureBox hinh = new PictureBox();
            hinh.Dock = DockStyle.Fill;
            Form_Preview fm_xt = new Form_Preview();
            fm_xt.Controls.Add(hinh);
            try
            {

                hinh.Load($@"{m_path1}\{m_tenhopdong}\{filename}");
                fm_xt.ShowDialog();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                MessageShower.ShowInformation("Lỗi");
            }

        }
        DataTable dt = new DataTable();
        private void FormLuachon_HopDong_Load(object sender, EventArgs e)
        {
            string queryStr = $"SELECT \"Tên File Hợp Đồng\" FROM Tbl_FileHopdongdinhkem WHERE \"Code\"='{m_code}'";
            dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
            //dtg_luachon.DataSource = dt1;
            //dt.Columns.Add(" File đính kèm");
            dtg_luachon.DataSource = dt;
            lnkb_duongdan.Text = $@"{m_path1}/{m_tenhopdong}";
            //dtg_luachon.Columns[1].Width = 400;
            //DataGridViewButtonColumn link = new DataGridViewButtonColumn();
            //link.HeaderText = "";
            //link.Name = " Xóa";
            //link.UseColumnTextForButtonValue = true;
            //link.Text = " Xóa";
            //link.FlatStyle = FlatStyle.Popup;
            //link.DefaultCellStyle.BackColor = Color.Red;
            //this.dtg_luachon.Columns.Add(link);


            //DataGridViewButtonColumn link1 = new DataGridViewButtonColumn();
            //link1.HeaderText = "";
            //link1.Name = " Thay Thế";
            //link1.UseColumnTextForButtonValue = true;
            //link1.Text = " Thay Thế";
            //link1.FlatStyle = FlatStyle.Popup;
            //link1.DefaultCellStyle.BackColor = Color.Yellow;
            //this.dtg_luachon.Columns.Add(link1);

            //DataGridViewButtonColumn link2 = new DataGridViewButtonColumn();
            //link2.HeaderText = "";
            //link2.Name = " Xem Trước";
            //link2.UseColumnTextForButtonValue = true;
            //link2.Text = " Xem Trước";
            //link2.FlatStyle = FlatStyle.Popup;
            //link2.DefaultCellStyle.BackColor = Color.Green;
            //this.dtg_luachon.Columns.Add(link2);
        }

        private void bt_themfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog loc = new OpenFileDialog();
            loc.DefaultExt = "doc";
            //dlg.Filter = "File mau|*.docx;*.doc";
            loc.Filter = "doc files (*.doc)|*.doc|pdf file (*.pdf)|*.pdf|execl files (*.xls)|*.xls|All files (*.*) | *.* ";
            loc.AddExtension = false;
            loc.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
            //loc.RestoreDirectory = true;
            loc.CheckFileExists = true;
            loc.CheckPathExists = true;
            loc.InitialDirectory = $@"{m_path1}";//check lai
            if (loc.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = loc.FileName;
                string hienthitenfile;
                hienthitenfile = loc.SafeFileName;
                //int leng = sizeof(hienthitenfile);
                //string duongdan = fileName - hienthitenfile;
                DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Bạn muốn chọn file này ?", "Chọn File");
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        ////textlink.Text = fileName;
                        lnkb_duongdan.Text = $@"{m_path1}/{m_tenhopdong}";
                        //dr[0] = hienthitenfile;
                        ////dr[2] = "Xóa File";
                        ////dr[3] = "Thay Thế";
                        ////dr[4] = "Xem Trước";

                        //dt.Rows.Add(dr);
                        //dtg_luachon.DataSource = dt;
                        // File.Delete(fileName);
                        //fileName = "";
                        File.Copy(fileName, $@"{m_path1}/{m_tenhopdong}/{hienthitenfile}");
                        string themfile = $"INSERT INTO Tbl_FileHopdongdinhkem(\"CodeDuAn\",\"Code\",\"Tên File Hợp Đồng\") VALUES ('{m_codeduan}','{m_code}',@File)";
                        DataProvider.InstanceTHDA. ExecuteNonQuery(themfile, parameter: new object[] { hienthitenfile });
                        string queryStr = $"SELECT \"Tên File Hợp Đồng\" FROM Tbl_FileHopdongdinhkem WHERE \"Code\"='{m_code}'";
                        dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
                        dtg_luachon.DataSource = dt;
                    }
                    catch (Exception lỗi)
                    {
                        MessageShower.ShowInformation(lỗi.ToString());
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                    return;
                }
            }
        }

        private void dtg_luachon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int hang = e.RowIndex;
            int cot = e.ColumnIndex;
            int sohang = dtg_luachon.Rows.Count;
            string tenfile = dtg_luachon.Rows[hang].Cells[3].Value.ToString();
            string duongdantoifile = $@"{m_path1}/{m_tenhopdong}/{tenfile}";
            string extension = Path.GetExtension(duongdantoifile);
            string queryStr = $"SELECT \"Code\",\"Tên File Hợp Đồng\" FROM Tbl_FileHopdongdinhkem WHERE \"Code\"='{m_code}'";
            dt = DataProvider.InstanceTHDA. ExecuteQuery(queryStr);
            string codedong = dt.Rows[hang]["Code"].ToString();
            try
            {
                if (e.ColumnIndex <= 2)
                {
                    string Task = dtg_luachon.Rows[hang].Cells[cot].Value.ToString();
                    if (Task == "Xoa")
                    {
                        if (MessageShower.ShowYesNoQuestion("Bạn có chắc muốn xóa không?", "Đang xóa...") == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dtg_luachon.Rows.RemoveAt(rowIndex);
                            dt.Rows.RemoveAt(rowIndex);
                            //if(hang>0 && sohang==hang)
                            //{
                            //    for(int i = 0; i < hang; i++)
                            //    {
                            //        dtg_luachon.Rows[i].Cells[0].Value =(;
                            //    }

                            //}  
                            //if (hang == 0)
                            //{
                            //    for (int i = 0; i < sohang - 2; i++)
                            //    {
                            //        dtg_luachon.Rows[i].Cells[0].Value = (i + 1).ToString();
                            //        //string updatestt = $"UPDATE  {m_tblDb_Congvandiden_Filedinhkem} SET \"{	}\"='{noiDung}' WHERE \"Stt\"='{stt}'";
                            //    }
                            //}

                            //dtg_luachon.Rows[hang].Cells[0].Value = stt;
                            //dr[0] = stt;
                            File.Delete($@"{m_path1}/{m_tenhopdong}/{tenfile}");
                            string xoafile = $"DELETE FROM Tbl_FileHopdongdinhkem WHERE \"Tên File Hợp Đồng\"='{tenfile}'";
                            DataProvider.InstanceTHDA. ExecuteQuery(xoafile);

                            //dataset.Tables["Tbl_students"].Rows[rowIndex].Delete();
                            //sqlAdapter.Update(dataset, "Tbl_students");
                        }
                    }
                    if (Task == "Thay thế")
                    {

                        OpenFileDialog loc = new OpenFileDialog();
                        loc.DefaultExt = "doc";
                        //dlg.Filter = "File mau|*.docx;*.doc";
                        loc.Filter = "doc files (*.doc)|*.doc|pdf file (*.pdf)|*.pdf|execl files (*.xls)|*.xls|All files (*.*) | *.* ";
                        loc.AddExtension = false;
                        loc.Title = "THƯ MỤC FILE ĐÍNH KÈM ";
                        loc.InitialDirectory = $@"{m_path1}\FileWord";
                        loc.RestoreDirectory = true;
                        loc.CheckFileExists = true;
                        loc.CheckPathExists = true;
                        //dlg.ShowDialog();
                        //DialogResult rs = dlg.ShowDialog();
                        if (loc.ShowDialog() == DialogResult.OK)
                        {
                            string fileName;
                            fileName = loc.FileName;
                            string tenhienthi = loc.SafeFileName;
                            DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Bạn có muốn thay thế file không?", "Thay thế file");
                            if (dialogResult == DialogResult.Yes)
                            {
                                try
                                {
                                    File.Replace($@"{m_path1}\{m_tenhopdong}\{tenfile}", fileName, $@"{m_path1}\{m_tenhopdong}\{tenhienthi}");
                                    //File.Delete($@"{m_path}\FileWord\LOCVO1.doc");
                                    // File.Delete(fileName);
                                    //fileName = "";
                                    dtg_luachon.Rows[hang].Cells[3].Value = tenhienthi;
                                    string updatestt = $"UPDATE  Tbl_FileHopdongdinhkem SET \"Tên File Hợp Đồng\"='{tenhienthi}' WHERE \"Code\"='{codedong}'";
                                    DataProvider.InstanceTHDA. ExecuteQuery(updatestt);

                                }
                                catch (Exception lỗi)
                                {
                                    MessageShower.ShowInformation(lỗi.ToString());
                                }
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //do something else
                                return;
                            }

                            // MessageShower.ShowInformation(fileName);
                        }
                        else
                        {
                            dlg.Dispose();

                        }


                    }
                    if (Task == "Xem trước")
                    {
                        int rowIndex = e.RowIndex;//bo sung sau khi da du thong tin



                        if (CommonConstants.GetFileDocExt().Contains(extension))
                        {
                            xemTruocWord(tenfile);
                        }
                        else if (CommonConstants.GetFileExcelExt().Contains(extension) || CommonConstants.GetFileCsvExt().Contains(extension))
                        {
                            xemTruocEXECL(tenfile);

                        }
                        else if (CommonConstants.GetFilePdfExt().Contains(extension))
                        {
                            xemTruocPDF(tenfile);
                        }
                        else if (CommonConstants.GetFileImgExt().Contains(extension))
                        {
                            xemTruocHINHANH(tenfile);

                        }
                    }


                }

            }
            catch
            {

            }
        }
    }
}
