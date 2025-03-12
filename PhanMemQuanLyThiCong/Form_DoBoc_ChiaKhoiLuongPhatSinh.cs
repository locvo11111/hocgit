using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_DoBoc_ChiaKhoiLuongPhatSinh : Form
    {
        DataTable _dtCongTac, _drNoCTHM;
        public Form_DoBoc_ChiaKhoiLuongPhatSinh(DataTable dt)//dt: code, Tên, Khối lượng hợp đồng, Khối lượng toàn bộ, Code công trình, Code hạng mục, Kỳ phát sinh
        {
            InitializeComponent();
            _drNoCTHM = dt;
            _dtCongTac = dt.Clone();
            
            
            string crCodeCT = "", crCodeHM = "";
            foreach (DataRow dr in dt.Rows)
            {
                string CodeCT = dr["CodeCongTrinh"].ToString();
                string CodeHM = dr["CodeHangMuc"].ToString();
                if (CodeCT != crCodeCT)
                {
                    string queryString = $"SELECT \"Ten\" FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"Code\" = '{CodeCT}'";
                    DataTable dtCongTrinh = DataProvider.InstanceTHDA.ExecuteQuery(queryString);

                    DataRow newrow = _dtCongTac.NewRow();
                    newrow["MaHieu"] = MyConstant.CONST_TYPE_CONGTRINH;
                    newrow["TenCongTac"] = dtCongTrinh.Rows[0]["Ten"].ToString();
                    _dtCongTac.Rows.Add(newrow);

                }
                
                if (CodeHM != crCodeHM)
                {
                    string queryString = $"SELECT \"Ten\" FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"Code\" = '{CodeHM}'";
                    DataTable dtHangMuc = DataProvider.InstanceTHDA.ExecuteQuery(queryString);

                    DataRow newrow = _dtCongTac.NewRow();
                    newrow["MaHieu"] = MyConstant.CONST_TYPE_HANGMUC;
                    newrow["TenCongTac"] = dtHangMuc.Rows[0]["Ten"].ToString();
                    _dtCongTac.Rows.Add(newrow);
                }

                _dtCongTac.Rows.Add(dr.ItemArray);
                crCodeCT = CodeCT;
                crCodeHM = CodeHM;
            }

        }

        private void Form_DoBoc_ChiaKhoiLuongPhatSinh_Load(object sender, EventArgs e)
        {
            //    string crCodeCT ="", crCodeHM = "";

            //    foreach (DataRow dr in _dtCongTac.Rows)
            //    {
            //        string CodeCT = dr["CodeCongTrinh"].ToString();
            //        string CodeHM = dr["CodeCongTrinh"].ToString();


            //    }


            //    for (int i = _dtCongTac.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow dr = _dtCongTac.Rows[i];

            //        string CodeCT = dr["CodeCongTrinh"].ToString();
            //        string CodeHM = dr["CodeCongTrinh"].ToString();

            //        //if (CodeHM.)

            //        crCodeCT = dr["CodeCongTrinh"].ToString();
            //        crCodeHM = dr["CodeCongTrinh"].ToString();
            //    }

            dgv_KhoiLuongChiTiet.DataSource = _dtCongTac;
            foreach (DataGridViewColumn col in dgv_KhoiLuongChiTiet.Columns)
            {
                if (col.Name.Contains('_'))
                {
                    col.HeaderText = col.Name.Split('_').First();
                }
            }

            string dbString = $"SELECT * FROM {TDKH.TBL_SoLanPhatSinh} WHERE \"CodeDuAn\" = '{Properties.Settings.Default.DuAnHienTai}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            cbb_soLanPhatSinh.DataSource = dt.AsEnumerable().ToDictionary(x => x["Code"], x => x["Ten"]).ToList();
            
        }

        private void bt_Luu_Click(object sender, EventArgs e)
        {
            //foreach (DataRow dr in _drNoCTHM.Rows)
            //{

            //    string codeCongTac = dr["Code"].ToString();
            //    string dbString = $"SELECT * FROM {TDKH.TBL_KhoiLuongPhatSinh} WHERE \"CodeCongTac\" = '{dr["Code"]}' " +
            //        $"AND \"CodePhatSinh\" = '{cbb_soLanPhatSinh.SelectedValue}'";
            //    DataTable dtPhatSinh = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //    //DataRow row;
            //    double KhoiLuongDu = double.Parse(dr["Khối lượng dư"].ToString());
            //    if (KhoiLuongDu < 0)
            //    {
            //        continue;
            //    }
            //    if (dtPhatSinh.Rows.Count == 0)
            //    {
            //        DataRow Newrow = dtPhatSinh.NewRow();
            //        dtPhatSinh.Rows.Add(Newrow);
            //        Newrow["Code"] = Guid.NewGuid().ToString();
            //        Newrow["CodeCongTac"] = dr["Code"];
            //        Newrow["CodePhatSinh"] = Guid.NewGuid().ToString();
            //        Newrow["KhoiLuong"] = dr["Khối lượng dư"].ToString();
            //    }
            //    else
            //    {
            //        dtPhatSinh.Rows[0]["KhoiLuong"] = double.Parse(dtPhatSinh.Rows[0]["KhoiLuong"].ToString()) + dr["Khối lượng dư"].ToString();
            //    }

            //    DataProvider.InstanceTHDA.UpdateDataTable(dtPhatSinh, TDKH.TBL_KhoiLuongPhatSinh);

            //    dbString = $"UPDATE {TDKH.TBL_ChiTietCongTacTheoKy} SET \"KhoiLuongToanBo\" = '{dr["KhoiLuongHopDong"]}', " +
            //        $"\"KhoiLuongToanBo_Iscongthucmacdinh\" = '0', \"KhoiLuongToanBo_CongThuc\" = ''" +
            //        $"WHERE \"Code\" = '{dr["Code"]}'";

            //    if (DataProvider.InstanceTHDA.ExecuteNonQuery(dbString) != 1)
            //    {
            //        MessageShower.ShowInformation("Lỗi cập nhật khối lượng công tác");
            //    }
            //}
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void cbb_soLanPhatSinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bt_huy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
