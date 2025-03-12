using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit;
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
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;

namespace PhanMemQuanLyThiCong
{
    public partial class FormLichHen : Form
    {
        public FormLichHen()
        {
            InitializeComponent();
        }
        private void fcn_init()
        {
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            string queryStr = $"SELECT *  FROM {MyConstant.Tbl_ChamCong_LichHen}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            int result = dt.Rows.Count;
            DateTime date = dateTimePicker1.Value;
            string ngayhen = date.Day.ToString();
            string thanghen = date.Month.ToString();
            string namhen = date.Year.ToString();
            DateTime now = DateTime.Now.Date;
            string ngayhientai = now.Day.ToString();
            string thanghientai= now.Month.ToString();
            string namhientai = now.Year.ToString();
            bool contains = dt.AsEnumerable().Any(row => ngayhen == row.Field<String>("Ngay"));
            bool contains1 = dt.AsEnumerable().Any(row => thanghen == row.Field<String>("Thang"));
            bool contains2 = dt.AsEnumerable().Any(row => namhen == row.Field<String>("Nam"));
            if (contains == false)
            {
                string dbstring = $"INSERT INTO {MyConstant.Tbl_ChamCong_LichHen} (\"Ngay\",\"Thang\",\"Nam\",\"NoiDung\") VALUES ('{ngayhen}','{thanghen}','{namhen}','{tb_NoiDungLichHen.Text}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);

            }    
            if(contains==true && contains1==true && contains2==true)
            {
                MessageShower.ShowError("Lịch hẹn đã tồn tại, Vui lòng sửa lại lịch hẹn hoặc xóa lịch hẹn hiện tại!", "Cảnh Báo");
            }    
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string queryStr = $"SELECT *  FROM {MyConstant.Tbl_ChamCong_LichHen}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            int result = dt.Rows.Count;
            DateTime date = dateTimePicker1.Value;
            string ngayhen = date.Day.ToString();
            string thanghen = date.Month.ToString();
            string namhen = date.Year.ToString();
            bool contains = dt.AsEnumerable().Any(row => ngayhen == row.Field<String>("Ngay"));
            bool contains1 = dt.AsEnumerable().Any(row => thanghen == row.Field<String>("Thang"));
            bool contains2 = dt.AsEnumerable().Any(row => namhen == row.Field<String>("Nam"));
            if (contains == true && contains1 == true && contains2 == true)
            {
                string xoahen = $"DELETE FROM {MyConstant.Tbl_ChamCong_LichHen} WHERE \"Ngay\"='{ngayhen}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(xoahen);
                tb_NoiDungLichHen.Text = "";
            }    
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string queryStr = $"SELECT *  FROM {MyConstant.Tbl_ChamCong_LichHen}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            int result = dt.Rows.Count;
            DateTime date = dateTimePicker1.Value;
            string ngayhen = date.Day.ToString();
            string thanghen = date.Month.ToString();
            bool contains1 = dt.AsEnumerable().Any(row => thanghen == row.Field<String>("Thang"));
            if (result != 0)
            {
                for (int i = 0; i < result; i++)
                {
                    string check = dt.Rows[i][0].ToString();//kiem tra database co ngay duoc chon chua
                    if (contains1 == true && check == ngayhen)
                    {
                        int checklap = i;
                        tb_NoiDungLichHen.Text = dt.Rows[checklap][3].ToString();
                        //richTextBox1.Text = dt.Rows[checklap][1].ToString();
                        break;
                    }
                    else
                    {
                        tb_NoiDungLichHen.Text = "";
                    }
                }         
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            string queryStr = $"SELECT *  FROM {MyConstant.Tbl_ChamCong_LichHen}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            int result = dt.Rows.Count;
            DateTime date = dateTimePicker1.Value;
            string ngayhen = date.Day.ToString();
            string thanghen = date.Month.ToString();
            string namhen = date.Year.ToString();
            bool contains = dt.AsEnumerable().Any(row => ngayhen == row.Field<String>("Ngay"));
            bool contains1 = dt.AsEnumerable().Any(row => thanghen == row.Field<String>("Thang"));
            bool contains2 = dt.AsEnumerable().Any(row => namhen == row.Field<String>("Nam"));
            if (contains == true && contains1 == true && contains2 == true)
            {
                string dbString = $"UPDATE  {MyConstant.Tbl_ChamCong_LichHen} SET \"NoiDung\"='{tb_NoiDungLichHen.Text}' WHERE \"Ngay\"='{ngayhen}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
        }
    }
}
