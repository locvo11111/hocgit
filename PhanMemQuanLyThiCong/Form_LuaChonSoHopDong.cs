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
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_LuaChonSoHopDong : Form
    {
        string m_tencot1, m_tenhopdong, m_tennoidungung="";
        int m_thanhtien = 0,m_giatrihopdong=0;
        public delegate void DE__TRUYENDATA(string tennoidungung,string tenhopdong,int thanhtien,int giatrihopdong);
        public DE__TRUYENDATA m_TruyenData;
        public Form_LuaChonSoHopDong(string TENCOT1)
        {
            InitializeComponent();
            m_tencot1 = TENCOT1;
        }
        private void update()
        {
            //dữ liệu phần chọn hợp đồng
            if(m_tencot1=="HỢP ĐỒNG CUNG CẤP")
            {
                capnhapsolieu("77",false,0,true);
                m_tenhopdong = dataGridView1.Rows[0].Cells[0].Value.ToString();
                m_giatrihopdong = Int32.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
            }
            if (m_tencot1 == "HỢP ĐỒNG VẬT TƯ")
            {
                capnhapsolieu("77",true,0,true);
            }
            if(m_tencot1 == "HỢP ĐỒNG KHÁC")
            {
                capnhapsolieu("77", true, 0, false);
                string queryDbStr = $"SELECT \"Code\" FROM {MyConstant.TBL_ThongTinPL} WHERE \"CodeLoaiHd\"!='{"77"}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryDbStr);
                string queryStr = $"SELECT \"ThanhTien\" FROM {MyConstant.TBL_ThongTinHopDong} WHERE \"CodePl\"='{dt.Rows[0][0].ToString()}' ";
                DataTable dt_thanhtien = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                foreach(DataRow row in dt_thanhtien.Rows)
                {
                    m_giatrihopdong = Int32.Parse(row[0].ToString()) + m_giatrihopdong;
                }
                m_tenhopdong = "HỢP ĐỒNG KHÁC";
                //dataGridView1.Rows.Add("HỢP ĐỒNG KHÁC", m_giatrihopdong);
                DataTable dt_hopdong = new DataTable();
                dt_hopdong.Columns.Add("TenHopDong");
                dt_hopdong.Columns.Add("GiaTriHopDong");
                dt_hopdong.Rows.Add("HỢP ĐỒNG KHÁC", m_giatrihopdong);
                dataGridView1.DataSource = dt_hopdong;
            }
        }
        private void capnhapsolieu(string codetenloaipl,bool methold,int vitri,bool luachon)
        {
            string queryDbStr;
            if (methold)
                 queryDbStr = $"SELECT \"CodeHd\",\"Code\" FROM {MyConstant.TBL_ThongTinPL} WHERE \"CodeLoaiHd\"='{codetenloaipl}'";
            else
                 queryDbStr = $"SELECT \"CodeHd\",\"Code\" FROM {MyConstant.TBL_ThongTinPL} WHERE \"CodeLoaiHd\"!='{codetenloaipl}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryDbStr);
            string queryStr = $"SELECT \"TenHopDong\",\"GiaTriHopDong\" FROM {MyConstant.TBL_Tonghopdanhsachhopdong} WHERE ";
            string condition = "";  
            string queryStr1 = $"SELECT \"TenCongViec\",\"ThanhTienThiCong\" FROM {MyConstant.TBL_ThongTinHopDong} WHERE \"CodePl\"='{dt.Rows[vitri][1].ToString()}' ";
            foreach (DataRow row in dt.Rows)
            {
                condition += $"OR \"Code\"='{row[0]}'";
            }
            queryStr += condition.Remove(0, 3);
            DataTable dt1 = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            DataTable dt_Tencongtac = DataProvider.InstanceTHDA.ExecuteQuery(queryStr1);
            if(luachon)
                dataGridView1.DataSource = dt1;
            dataGridView2.DataSource = dt_Tencongtac;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            //string task = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (m_tencot1 == "HỢP ĐỒNG CUNG CẤP")
            {
                capnhapsolieu("77", false, e.RowIndex,false);
                m_tenhopdong = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                m_giatrihopdong = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            string name = "";
            int result = dataGridView2.Rows.Count;
            for (int i = 0; i < result; i++)
            {
                if (Convert.ToBoolean(dataGridView2.Rows[i].Cells["check"].Value) == true)
                {
                    name += ","+dataGridView2.Rows[i].Cells[1].Value.ToString();
                    m_thanhtien = m_thanhtien + Int32.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                }
            }
            m_tennoidungung += name.Remove(0, 1);
            DialogResult dialogResult = MessageShower.ShowYesNoQuestion("Bạn muốn thay đổi tên nội dung ứng?", "Lưu File");
            if (dialogResult == DialogResult.Yes)
            {
                string BM = DevExpress.XtraEditors.XtraInputBox.Show("Nhập tên nội dung ứng", "Tạo tên nội dung ứng", "");
                if (BM == "")
                    return;
                m_tennoidungung = BM;
            }
            m_TruyenData( m_tennoidungung,m_tenhopdong, m_thanhtien,m_giatrihopdong);
            //adddata("Tbl_bangluachonchuyendoi");
            this.Close();
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dataGridView2.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells["check"]).Value = true;
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dataGridView2.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells["check"]).Value = false;
            }
        }

        private void Form_LuaChonSoHopDong_Load(object sender, EventArgs e)
        {
            update();
        }
    }
}
