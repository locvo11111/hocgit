using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.HopDong;
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
    public partial class Form_ChiTietHopDong : DevExpress.XtraEditors.XtraForm
    {
        public static string m_CodeHD="";
        public static double m_GiaTriHopDong=0;
        public Form_ChiTietHopDong()
        {
            InitializeComponent();
        }

        private void sb_ThemGiaiNgan_Click(object sender, EventArgs e)
        {
            int index = gr_GiaiNgan.SelectedIndex;
            string dbString = "";
            bool PhanTram = rgPhanTram_SoTien.SelectedIndex == 0 ? true : false;
            bool TheoThang = rg_TheoThangNgay.SelectedIndex == 0 ? true : false;
            switch (index)
            {
                case 0:    
                case 3:   
                case 6:
                    dbString = $"INSERT INTO {MyConstant.Tbl_CHITIETHOPDONG} (\"Code\",\"GiaTri\",\"CodeHopDong\",\"Ngay\",\"Loai\",\"IsPhanTram\") " +
                        $"VALUES ('{Guid.NewGuid()}','{text_GiaiNgan.Text}','{m_CodeHD}','{cc_Lich.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{index}','{PhanTram}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    break;
                case 1: 
                case 4: 
                case 5:
                    dbString = $"INSERT INTO {MyConstant.Tbl_CHITIETHOPDONG} (\"Code\",\"GiaTri\",\"CodeHopDong\",\"Loai\",\"IsPhanTram\") " +
    $"VALUES ('{Guid.NewGuid()}','{text_GiaiNgan.Text}','{m_CodeHD}','{index}','{PhanTram}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    break;
                default:
                    if(TheoThang)
                        dbString = $"INSERT INTO {MyConstant.Tbl_CHITIETHOPDONG} (\"Code\",\"CodeHopDong\",\"Ngay\",\"Loai\",\"TheoThang\") " +
$"VALUES ('{Guid.NewGuid()}','{m_CodeHD}','{cc_Lich.DateTime.Day}','{2}','{TheoThang}')";
                    else
                        dbString = $"INSERT INTO {MyConstant.Tbl_CHITIETHOPDONG} (\"Code\",\"CodeHopDong\",\"Ngay\",\"Loai\",\"TheoThang\") " +
$"VALUES ('{Guid.NewGuid()}','{m_CodeHD}','{cc_Lich.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{2}','{TheoThang}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    break;
            }
            gr_GiaiNgan.SelectedIndex = 0;
            Fcn_LoadGrid(m_CodeHD, m_GiaTriHopDong);
        }

        public void Fcn_UpdatePropoties(string TenHopDong, string CodeHD,double GiaTriHopDong)
        {
            lc_TenHopDong.Text =$"Chi tiết thanh toán: {TenHopDong}";
            m_CodeHD = CodeHD;
            m_GiaTriHopDong = GiaTriHopDong;
            Fcn_LoadGrid(CodeHD,GiaTriHopDong);

        }
        private void Fcn_LoadGrid(string CodeHD,double GiaTriHopDong)
        {
            string queryStr = $"SELECT * FROM {MyConstant.Tbl_CHITIETHOPDONG} WHERE \"CodeHopDong\"='{CodeHD}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            List<ChiTietHopDong> CTHD = DuAnHelper.ConvertToList<ChiTietHopDong>(dt);
            if (CTHD.Count() == 0)
                return;
            int stt = 1;
            CTHD.ForEach(x => x.GiaTriHopDong = GiaTriHopDong);
            CTHD.ForEach(x => x.STT = stt++);
            gc_TongHop.DataSource = CTHD;
        }
        private void Fcn_UpdateSelect(List<ChiTietHopDong> CTHD)
        {

        }
        private void gr_GiaiNgan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gr_GiaiNgan.SelectedIndex;
            switch (index)
            {
                case 0:
                case 1:  
                case 3:         
                case 4:             
                case 5:             
                case 6:
                    rg_TheoThangNgay.SelectedIndex = -1;
                    rg_TheoThangNgay.Enabled = false;
                    text_GiaiNgan.Enabled = true;
                    rgPhanTram_SoTien.Enabled = true;
                    rgPhanTram_SoTien.SelectedIndex = 0;
                    if (index == 3 || index == 4 || index == 5 || index == 6)
                    {
                        string queryStr = $"SELECT * FROM {MyConstant.Tbl_CHITIETHOPDONG} WHERE \"CodeHopDong\"='{m_CodeHD}' AND \"Loai\"='{index}'";
                        DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                        if (dt.Rows.Count != 0)
                        {
                            MessageShower.ShowError("Đã tồn tại nội dung bạn muốn chọn vui lòng xóa nội dung đã tồn tại trước khi tạo mới!");
                            gr_GiaiNgan.SelectedIndex = 0;
                        }
                    }
                    //break;
                    //rg_TheoThangNgay.SelectedIndex = -1;
                    //rg_TheoThangNgay.Enabled = false;
                    //text_GiaiNgan.Enabled = true;
                    //rgPhanTram_SoTien.Enabled = true;
                    //rgPhanTram_SoTien.SelectedIndex = 0;
                    break;
                default:
                    rg_TheoThangNgay.SelectedIndex = 0;
                    rg_TheoThangNgay.Enabled = true;
                    text_GiaiNgan.Enabled = false;
                    rgPhanTram_SoTien.Enabled = false;
                    rgPhanTram_SoTien.SelectedIndex = -1;
                    break;
            }
        }

        private void text_GiaiNgan_EditValueChanged(object sender, EventArgs e)
        {
            if (text_GiaiNgan.Text =="")
                return;
            int index = rgPhanTram_SoTien.SelectedIndex;
            if (!double.TryParse(text_GiaiNgan.Text, out double phantram))
            {
                text_GiaiNgan.Text = "";
                return;
            }
            if (index == 0)
            {
                text_GiaiNgan.ForeColor = double.Parse(text_GiaiNgan.Text) >= 100 ? Color.Red : default;
            }
            else
            {
                text_GiaiNgan.ForeColor = double.Parse(text_GiaiNgan.Text) >= m_GiaTriHopDong ? Color.Red : default;
            }

        }

        private void riBE_Xoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa thông tin này không?");
            if (rs == DialogResult.Yes)
            {
                string code =(string)gv_TongHop.GetRowCellValue(gv_TongHop.FocusedRowHandle, "Code");
                gv_TongHop.DeleteSelectedRows();
                string db_string = $"DELETE FROM {MyConstant.Tbl_CHITIETHOPDONG} WHERE \"Code\"='{code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(db_string);
            }
        }

        private void rgPhanTram_SoTien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgPhanTram_SoTien.SelectedIndex == 0)
                lc_SoTien.Text = "Phần trăm ";
            else
                lc_SoTien.Text = "Số tiền ";
        }
    }
}