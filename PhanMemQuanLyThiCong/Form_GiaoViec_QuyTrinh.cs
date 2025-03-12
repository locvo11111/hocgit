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
    public partial class Form_GiaoViec_QuyTrinh : Form
    {
        //DataProvider m_db = new DataProvider("");
        string m_codeDauViec;
        int m_type;
        string m_codeCV;
        List<string> lsCVLinked = new List<string>(); // toàn bộ công việc đã link với công việc hiện tại (CVHT chọn từ combobox)

        const int CONST_ChiCoCongViecOTruoc = 0; //Chỉ có công việc trước
        const int CONST_ChiCoCongViecOSau = 1; //Chỉ có công việc trước
        const int CONST_CongViecCaTruocVaSau = 2; //Chỉ có công việc trước
        const int CONST_KhongCoCongViecCaTruocVaSau = 3; //Chỉ có công việc trước
        //List<string> lsCVTiepTheo = new List<string>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathDb"></param>
        /// <param name="codeDauViec"></param>
        /// <param name="type">Loại: Thêm mới (0x00), Chỉnh sửa (0x01)</param>
        /// <param name="CodeCV">Cần nhập khi chỉnh sửa</param>
        public Form_GiaoViec_QuyTrinh(string codeDauViec, int type, string CodeCV)
        {
            InitializeComponent();
            m_codeDauViec = codeDauViec;
            m_type = type;
            //DataProvider.InstanceTHDA.changePath(pathDb);
            m_codeCV = CodeCV;
            ////Lấy danh sách Công việc ở đầu cuối
            //string dbString = $"SELECT \"CodeCongViecConHienTai\", \"CodeCongViecConTiepTheo\" FROM {GiaoViec.TBL_QUYTRINHTHUCHIEN}";
            //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //foreach ()
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
            if (m_type == MyConstant.CONST_GV_QTTH_THEMMOI)
            {
                //Cập nhật vào combobox
                string dbString = $"SELECT * FROM {GiaoViec.TBL_CONGVIECCHA} WHERE \"CodeDauMuc\" = '{m_codeDauViec}'";
                DataTable dtCVCha = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                Dictionary<string, string> Dic_CongViecCha = new Dictionary<string, string>();
                foreach (DataRow r in dtCVCha.Rows)
                {
                    Dic_CongViecCha.Add(r["CodeCongViecCha"].ToString(), r["TenCongViec"].ToString());
                }
                cbb_TenCongViecChaHienTai.ValueMember = "Key";
                cbb_TenCongViecChaHienTai.DisplayMember = "Value";
                cbb_TenCongViecChaHienTai.DataSource = Dic_CongViecCha.ToList();

                cbb_CVChaTiepTheo.ValueMember = "Key";
                cbb_CVChaTiepTheo.DisplayMember = "Value";
                cbb_CVChaTiepTheo.DataSource = Dic_CongViecCha.ToList();
            }
            else //Chỉnh sửa
            {
                cbb_CVConTiepTheo.SelectedIndexChanged -= cbb_CVConTiepTheo_SelectedIndexChanged;
                cbb_TenCongViecConHienTai.SelectedIndexChanged -= cbb_TenCongViecConHienTai_SelectedIndexChanged;

                string dbString = $"SELECT * FROM {GiaoViec.TBL_QUYTRINHTHUCHIEN} WHERE \"Code\" = '{m_codeCV}'";
                DataTable dtQT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                if (dtQT.Rows.Count <= 0)
                {
                    MessageShower.ShowInformation("Không thể tải công việc");
                    this.Close();
                    return;
                }

                dbString = $"SELECT \"TenCongViec\" FROM {GiaoViec.TBL_CONGVIECCHA} WHERE \"CodeCongViecCha\" = '{dtQT.Rows[0]["CodeCongViecChaHienTai"]}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                if (dt.Rows.Count <= 0)
                {
                    MessageShower.ShowInformation("Không thể tải công việc");
                    this.Close();
                    return;
                }

                cbb_TenCongViecChaHienTai.Text = dt.Rows[0][0].ToString();

                dbString = $"SELECT \"TenCongViec\" FROM {GiaoViec.TBL_CONGVIECCHA} WHERE \"CodeCongViecCha\" = '{dtQT.Rows[0]["CodeCongViecChaTiepTheo"]}'";
                dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                if (dt.Rows.Count <= 0)
                {
                    MessageShower.ShowInformation("Không thể tải công việc");
                    this.Close();
                    return;
                }

                cbb_CVChaTiepTheo.Text = dt.Rows[0][0].ToString();
                string cvConHT = dtQT.Rows[0]["CodeCongViecConHienTai"].ToString();
                string cvConTT = dtQT.Rows[0]["CodeCongViecConTiepTheo"].ToString();
                
                dbString = $"SELECT \"TenCongViec\", \"Bắt đầu\", \"Kết thúc\" FROM {GiaoViec.TBL_CONGVIECCON} WHERE \"CodeCongViecCon\" = '{cvConHT}'";
                dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                if (dt.Rows.Count <= 0)
                {
                    MessageShower.ShowInformation("Không thể tải công việc");
                    this.Close();
                    return;
                }

                cbb_TenCongViecConHienTai.DataSource = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>(cvConHT, dt.Rows[0][0].ToString()) };
                cbb_TenCongViecConHienTai.SelectedIndex = 0;
                dtp_GV_TGBatDau.ValueChanged -= dtp_GV_TGBatDau_ValueChanged;
                dtp_GV_TGBatDau.Value = DateTime.Parse(dt.Rows[0]["Bắt đầu"].ToString());
                dtp_GV_TGBatDau.ValueChanged += dtp_GV_TGBatDau_ValueChanged;

                dtp_GV_TGKetThuc.Value = DateTime.Parse(dt.Rows[0]["Kết thúc"].ToString());


                dbString = $"SELECT \"TenCongViec\", \"Bắt đầu\", \"Kết thúc\" FROM {GiaoViec.TBL_CONGVIECCON} WHERE \"CodeCongViecCon\" = '{cvConTT}'";
                dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                if (dt.Rows.Count <= 0)
                {
                    MessageShower.ShowInformation("Không thể tải công việc");
                    this.Close();
                    return;
                }

                cbb_CVConTiepTheo.DataSource = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>(cvConTT, dt.Rows[0][0].ToString()) };
                cbb_CVConTiepTheo.SelectedIndex = 0;
                cbb_GV_NTT_TrangThai.Text = dtQT.Rows[0]["TrangThai"].ToString();
                nud_soNgaySoVoiCongTacTruoc.Value = (int)dtQT.Rows[0]["SoNgay"];
                nud_SoNgayThucHienCVTT.Value = (DateTime.Parse(dt.Rows[0]["Kết thúc"].ToString()).Date- DateTime.Parse(dt.Rows[0]["Bắt đầu"].ToString()).Date).Days;

                gr_CVTT.Enabled = true;
                int ret = fcn_FindCVLinkedTo(dtQT.Rows[0]["CodeCongViecConHienTai"].ToString());

                if (ret == CONST_ChiCoCongViecOTruoc || ret == CONST_CongViecCaTruocVaSau)
                {
                    pn_thoiGianCVHT.Enabled = false;
                }
                else
                    pn_thoiGianCVHT.Enabled = true;


                cbb_TenCongViecChaHienTai.Enabled = false;
                cbb_TenCongViecConHienTai.Enabled = false;
                cbb_CVConTiepTheo.Enabled = false;
                cbb_CVChaTiepTheo.Enabled = false;
                bt_HoanTat.Enabled = true;

            }
        }



        private void cbb_TenCongViecChaHienTai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbb = sender as ComboBox;
            if (cbb.SelectedIndex < 0)
                return;
            fcn_UpdateCongTacCon(cbb_TenCongViecConHienTai, cbb_TenCongViecChaHienTai.SelectedValue.ToString());
        }

        private void fcn_UpdateCongTacCon(ComboBox cbbCVCon, string codeCVCha)
        {
            //string codeCVCha = cbb_TenCongViecChaHienTai.SelectedValue.ToString();
            if (cbbCVCon == cbb_TenCongViecConHienTai)
                cbbCVCon.SelectedIndexChanged -= cbb_TenCongViecConHienTai_SelectedIndexChanged;
            else if (cbbCVCon == cbb_CVConTiepTheo)
                cbbCVCon.SelectedIndexChanged -= cbb_CVConTiepTheo_SelectedIndexChanged;



            string dbString = $"SELECT * FROM {GiaoViec.TBL_CONGVIECCON} WHERE \"CodeCongViecCha\" = '{codeCVCha}'";
            DataTable dtCVCon = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            Dictionary<string, string> Dic_CongViecCon = new Dictionary<string, string>();
            foreach (DataRow r in dtCVCon.Rows)
            {
                Dic_CongViecCon.Add(r["CodeCongViecCon"].ToString(), r["TenCongViec"].ToString());
            }
            cbbCVCon.DataSource = Dic_CongViecCon.ToList();
            cbbCVCon.SelectedIndex = -1;
            if (cbbCVCon == cbb_TenCongViecConHienTai)
                cbbCVCon.SelectedIndexChanged += cbb_TenCongViecConHienTai_SelectedIndexChanged;
            else if (cbbCVCon == cbb_CVConTiepTheo)
                cbbCVCon.SelectedIndexChanged += cbb_CVConTiepTheo_SelectedIndexChanged;
        }

        private void cbb_CVChaTiepTheo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbb = sender as ComboBox;
            if (cbb.SelectedIndex < 0)
                return;

            fcn_UpdateCongTacCon(cbb_CVConTiepTheo, cbb_CVChaTiepTheo.SelectedValue.ToString());
        }

        private void cbb_TenCongViecConHienTai_SelectedIndexChanged(object sender, EventArgs e)
        {
            bt_HoanTat.Enabled = false;

            ComboBox cbb = sender as ComboBox;
            if (cbb.SelectedIndex < 0)
            {
                gr_CVTT.Enabled = false;
                return;
            }


            if (m_type == MyConstant.CONST_GV_QTTH_THEMMOI)
            {
                lsCVLinked.Clear();
                cbb_CVConTiepTheo.SelectedIndex = -1;
                string CVHT = cbb.SelectedValue as string;//CÔng việc hiện tại
                string dbString = $"SELECT \"Bắt đầu\", \"Kết thúc\" FROM {GiaoViec.TBL_CONGVIECCON} WHERE \"CodeCongViecCon\"='{CVHT}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dt.Rows.Count == 0)
                {
                    MessageShower.ShowInformation("Không thể tải chi tiết công việc");
                    cbb.SelectedIndex = -1;
                    return;
                }
                dtp_GV_TGBatDau.ValueChanged -= dtp_GV_TGBatDau_ValueChanged;
                dtp_GV_TGBatDau.Value = DateTime.Parse(dt.Rows[0]["Bắt đầu"].ToString());
                dtp_GV_TGBatDau.ValueChanged -= dtp_GV_TGBatDau_ValueChanged;

                dtp_GV_TGKetThuc.Value = DateTime.Parse(dt.Rows[0]["Kết thúc"].ToString());

                int ret = fcn_FindCVLinkedTo(CVHT);

                if (ret == CONST_ChiCoCongViecOTruoc || ret == CONST_CongViecCaTruocVaSau)
                {
                    pn_thoiGianCVHT.Enabled = false;
                }
                else
                    pn_thoiGianCVHT.Enabled = true;
                gr_CVTT.Enabled = true;
            }

        }

        private int fcn_FindCVLinkedTo(string CVHienTai)
        {
            //int ret = -1; //Không có công tác trước và sau
            bool ret1 = fcn_FindCVLinkedToBack(CVHienTai);
            bool ret2 = fcn_FindCVLinkedToFront(CVHienTai);

            if (ret1 && ret2)
                return CONST_CongViecCaTruocVaSau;
            else if (ret1)
                return CONST_ChiCoCongViecOSau;
            else if (ret2)
                return CONST_ChiCoCongViecOTruoc;
            else
                return CONST_KhongCoCongViecCaTruocVaSau;
        }

        private bool fcn_FindCVLinkedToBack(string CVHienTai)
        {
            string dbString = $"SELECT \"CodeCongViecConTiepTheo\" FROM {GiaoViec.TBL_QUYTRINHTHUCHIEN} WHERE \"CodeCongViecConHienTai\"='{CVHienTai}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            bool ret = false; //Không có công việc nào link vào sau
            foreach (DataRow r in dt.Rows)
            {
                ret = true;//Có công việc link vào sau
                string cv = r[0].ToString();
                lsCVLinked.Add(cv);
                fcn_FindCVLinkedToBack(cv);
            } 
            return ret;
        }

        private bool fcn_FindCVLinkedToFront(string CVHienTai)
        {
            string dbString = $"SELECT \"CodeCongViecConHienTai\" FROM {GiaoViec.TBL_QUYTRINHTHUCHIEN} WHERE \"CodeCongViecConTiepTheo\"='{CVHienTai}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            bool ret = false;
            foreach (DataRow r in dt.Rows)
            {
                ret = true;
                string cv = r[0].ToString();
                lsCVLinked.Add(cv);
                fcn_FindCVLinkedToFront(cv);
            }
            return ret;
        }

        private void bt_HoanTat_Click(object sender, EventArgs e)
        {

            
            string cvConHienTai = cbb_TenCongViecConHienTai.SelectedValue.ToString();
            string cvConTiepTheo = cbb_CVConTiepTheo.SelectedValue.ToString();
            int type = ((string)cbb_GV_NTT_BatDauSoVoiCongTacTruoc.Text == "Bắt đầu")?1:0;
            int soNgaySoVoiCVHT = (int)nud_soNgaySoVoiCongTacTruoc.Value;

            if (m_type == MyConstant.CONST_GV_QTTH_THEMMOI)
            {
                string cvChaHienTai = cbb_TenCongViecChaHienTai.SelectedValue.ToString();
                string cvChaTiepTheo = cbb_CVChaTiepTheo.SelectedValue.ToString();
                if (cbb_TenCongViecChaHienTai.SelectedIndex < 0 || cbb_CVConTiepTheo.SelectedIndex < 0)
                {
                    MessageShower.ShowInformation("Vui lòng chọn đầy đủ công việc hiện tại và công việc tiếp theo");
                    return;
                }
                string dbString = $"INSERT INTO {GiaoViec.TBL_QUYTRINHTHUCHIEN} " +
                    $"(\"Code\", \"CodeDauViec\",\"CodeCongViecChaHienTai\", \"CodeCongViecConHienTai\", " +
                    $"\"CodeCongViecChaTiepTheo\", \"CodeCongViecConTiepTheo\", \"LoaiTuongQuan\", \"SoNgay\", \"TrangThai\") " +
                    $"VALUES ('{Guid.NewGuid()}', '{m_codeDauViec}', '{cvChaHienTai}', " +
                    $"'{cvConHienTai}', '{cvChaTiepTheo}', '{cvConTiepTheo}', '{type}', '{soNgaySoVoiCVHT}', '{cbb_GV_NTT_TrangThai.Text}')";

                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);


            }
            else
            {
                string dbString = $"UPDATE {GiaoViec.TBL_QUYTRINHTHUCHIEN} SET \"LoaiTuongQuan\"='{type}', \"SoNgay\"='{soNgaySoVoiCVHT}', \"TrangThai\"='{cbb_GV_NTT_TrangThai.Text}', \"SoNgay\"='{nud_soNgaySoVoiCongTacTruoc.Value}' WHERE \"Code\"='{m_codeCV}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }

            string dbString1 = $"UPDATE {GiaoViec.TBL_CONGVIECCON} SET " +
                $"\"Bắt đầu\" = '{dtp_NgayBDCVTT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}', " +
                $"\"Kết thúc\" = '{dtp_NgayKTCVTT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                $"WHERE \"CodeCongViecCon\"='{cvConTiepTheo}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString1);

            if (pn_thoiGianCVHT.Enabled)
            {

                string dbString = $"UPDATE {GiaoViec.TBL_CONGVIECCON} SET " +
                    $"\"Bắt đầu\" = '{dtp_GV_TGBatDau.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}', " +
                    $"\"Kết thúc\" = '{dtp_GV_TGKetThuc.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                    $"WHERE \"CodeCongViecCon\"='{cvConHienTai}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                MyFunction.fcn_CapNhatNgayChoToanBoCongViecLienQuan(cvConHienTai, dtp_GV_TGBatDau.Value.Date, dtp_GV_TGKetThuc.Value.Date);


            }


            this.Close();
        }

  
        private void cbb_CVConTiepTheo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bt_HoanTat.Enabled = false;
            //if (cbb_TenCongViecConHienTai.SelectedValue.)
            ComboBox cbb = sender as ComboBox;
            if (cbb.SelectedIndex < 0)
            {
                return;
            }

            string CVSelected = cbb.SelectedValue.ToString();

            if (CVSelected == cbb_TenCongViecConHienTai.SelectedValue.ToString())
            {
                MessageShower.ShowInformation("Không thể chọn trùng với công việc hiện tại");
                cbb.SelectedIndex = -1;
                return;
            }

            if (lsCVLinked.Contains(CVSelected))
            {
                MessageShower.ShowInformation("Đã tồn tại liên kết tới công việc hiện tại! Vui lòng chọn công việc khác");
                cbb.SelectedIndex = -1;
                return;
            }

            ////Kiểm tra xem có đứng sau cv nào không
            //string dbString = $"SELECT \"CodeCongViecConTiepTheo\" FROM {GiaoViec.TBL_QUYTRINHTHUCHIEN} WHERE \"CodeCongViecConHienTai\"='{CVSelected}'";
            //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            //bool isACVHT = dt.Rows.Count > 0; //Kiểm tra xem có từng là CV Hiện tại nào không

            //Kiểm tra xem có đứng trước cv nào không

            string dbString = $"SELECT \"CodeCongViecConHienTai\" FROM {GiaoViec.TBL_QUYTRINHTHUCHIEN} WHERE \"CodeCongViecConTiepTheo\"='{CVSelected}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            bool isACVTT = dt.Rows.Count > 0; //Kiểm tra xem có từng là CV Hiện tại nào không

            if (isACVTT)
            {
                MessageShower.ShowInformation("Công việc này đã liên kết vào sau 1 công việc khác");
                cbb.SelectedIndex = -1;
                return;
            }
            bt_HoanTat.Enabled = true;
            fcn_CapNhatNgayUocTinhCVTT();
    

        }

        private void dtp_GV_TGBatDau_ValueChanged(object sender, EventArgs e)
        {
            
            if (dtp_GV_TGKetThuc.Value < dtp_GV_TGBatDau.Value)
            {
                MessageShower.ShowInformation("Ngày bắt đầu không thể nhỏ hơn ngày kết thúc");
                dtp_GV_TGBatDau.Value = dtp_GV_TGKetThuc.Value;
            }

            int days = (dtp_GV_TGKetThuc.Value - dtp_GV_TGBatDau.Value).Days;

            nud_soNgayCVHT.Value = days;
        }

        private void nud_soNgayCVHT_ValueChanged(object sender, EventArgs e)
        {
            dtp_GV_TGKetThuc.Value = dtp_GV_TGBatDau.Value.AddDays(((int)nud_soNgayCVHT.Value));
            fcn_CapNhatNgayUocTinhCVTT();
        }

        private void fcn_CapNhatNgayUocTinhCVTT()
        {

            switch (cbb_GV_NTT_BatDauSoVoiCongTacTruoc.Text.ToString())
            {
                case "Bắt đầu":
                    dtp_NgayBDCVTT.Value = dtp_GV_TGBatDau.Value.AddDays((int)nud_soNgaySoVoiCongTacTruoc.Value);
                    break;
                case "Trước so với kết thúc":
                    dtp_NgayBDCVTT.Value = dtp_GV_TGKetThuc.Value.AddDays((int)(-nud_soNgaySoVoiCongTacTruoc.Value));
                    break;
                default:
                    dtp_NgayBDCVTT.Value = dtp_GV_TGKetThuc.Value.AddDays((int)(nud_soNgaySoVoiCongTacTruoc.Value));
                    break;
            }

            dtp_NgayKTCVTT.Value = dtp_NgayBDCVTT.Value.AddDays((int)nud_SoNgayThucHienCVTT.Value);
        }

        private void dtp_GV_TGKetThuc_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_GV_TGKetThuc.Value < dtp_GV_TGBatDau.Value)
            {
                MessageShower.ShowInformation("Ngày bắt đầu không thể nhỏ hơn ngày kết thúc");
                dtp_GV_TGKetThuc.Value = dtp_GV_TGBatDau.Value;
            }

            int days = (dtp_GV_TGKetThuc.Value - dtp_GV_TGBatDau.Value).Days;

            nud_soNgayCVHT.Value = days;
        }

        private void nud_soNgaySoVoiCongTacTruoc_ValueChanged(object sender, EventArgs e)
        {
            fcn_CapNhatNgayUocTinhCVTT();

        }

        private void nud_SoNgayThucHienCVTT_ValueChanged(object sender, EventArgs e)
        {
            fcn_CapNhatNgayUocTinhCVTT();

        }

        private void cbb_GV_NTT_BatDauSoVoiCongTacTruoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            fcn_CapNhatNgayUocTinhCVTT();

        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_GiaoViec_QuyTrinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageShower.ShowInformation("Bạn có muốn đóng form này không?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.No)
            //    e.Cancel = true;
        }
    }
}
