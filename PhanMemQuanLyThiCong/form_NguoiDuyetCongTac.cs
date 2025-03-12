using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class form_NguoiDuyetCongTac : Form
    {
        string _codeCVCon = "";
        //DataProvider m_db = new DataProvider("");
        //DataProvider m_dbFromServer = new DataProvider("");

        public form_NguoiDuyetCongTac(string codeCVCon)
        {
            InitializeComponent();
            _codeCVCon = codeCVCon;
            //DataProvider.InstanceTHDA.changePath(pathdb);
            //DataProvider.InstanceServer.changePath(pathServerDb);
        }

        private void form_NguoiDuyetCongTac_Load(object sender, EventArgs e)
        {
            string dbString = $"SELECT * FROM \"{MyConstant.TBL_FROMSERVER_USER}\"";

            DataTable dt = DataProvider.InstanceServer.ExecuteQuery(dbString);
            Dictionary<string, string> dicUser = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                dicUser.Add(r["Code"].ToString(), $"{r["FullName"]} ({r["Email"]})");
            }
            //Control[] ctrls = this.Controls.Find("cbb_NguoiDuyetBuoc", false);
            //foreach (Control ctrl in ctrls)
            //{
            //    if (ctrl is ComboBox)
            //        ((ComboBox)ctrl).DataSource = dicUser.ToList();
            //}


            dbString = $"SELECT * FROM {GiaoViec.TBL_CONGVIECCON} WHERE \"CodeCongViecCon\" = '{_codeCVCon}'";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            
            if (dt.Rows.Count > 0)
            {
                DataRow rowCV= dt.NewRow();
                rowCV.ItemArray = dt.Rows[0].ItemArray;

                lb_TenCongTac.Text = dt.Rows[0]["TenCongViec"].ToString();
                dbString = $"SELECT \"CodeDauMuc\" FROM {GiaoViec.TBL_CONGVIECCHA} WHERE \"CodeCongViecCha\" = '{dt.Rows[0]["CodeCongViecCha"]}'";
                dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                if (dt.Rows.Count > 0)
                {
                    dbString = $"SELECT \"QuyTrinh\" FROM {GiaoViec.TBL_DauViecLon} WHERE \"Code\" = '{dt.Rows[0]["CodeDauMuc"]}'";
                    dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (dt.Rows.Count > 0)
                    {
                        string[] lsStep;
                        if (dt.Rows[0]["QuyTrinh"].ToString() == MyConstant.CONST_LoaiQT_QTMH)
                            lsStep = MyConstant.lsQTMH;
                        else
                            lsStep = MyConstant.lsQTTH;

                        for (int i =1; i<=4; i++)
                        {
                            Label lb = this.Controls.Find($"lb_Buoc{i}", false).FirstOrDefault() as Label;
                            lb.Text = lsStep[i - 1];
                            ComboBox cbb = this.Controls.Find($"cbb_NguoiDuyetBuoc{i}", false).FirstOrDefault() as ComboBox;
                            cbb.DataSource = dicUser.ToList();
                            cbb.SelectedValue = rowCV[$"Người duyệt bước {i}"];
                        }    
                        //cbb_GV_TrangThai.Text = crRow.Cells["TrangThai"].Value.ToString();
                    }
                    else
                    {
                        MessageShower.ShowInformation("Không thể lấy thông tin đầu việc của công tác này");
                    }
                }
                else
                {
                    MessageShower.ShowInformation("Không thể lấy thông tin công tác cha của công tác này");
                }
            }
            
        }

        private void bt_Luu_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 4; i++)
            {
                Label lb = this.Controls.Find($"lb_Buoc{i}", false).FirstOrDefault() as Label;
                ComboBox cbb = this.Controls.Find($"cbb_NguoiDuyetBuoc{i}", false).FirstOrDefault() as ComboBox;
                if (cbb.SelectedIndex >= 0)
                {
                    string dbString = $"UPDATE {GiaoViec.TBL_CONGVIECCON} SET \"Người duyệt bước {i}\"='{cbb.SelectedValue}' WHERE \"CodeCongViecCon\"='{_codeCVCon}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
            this.Close();
        }
    }
}
