using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model.HopDong;
using PhanMemQuanLyThiCong.Common.Helper;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_TaoMoiHopDong : DevExpress.XtraEditors.XtraForm
    {
        public static string m_tennhathau, m_cbb_remove, m_codenhathau, m_range, m_codeHD,m_CodeDot;
        Dictionary<Control, string> thongtinhopdong = new Dictionary<Control, string>();
        Dictionary<string, string> LoaiHD = new Dictionary<string, string>();

        private void lUE_ChiTietBenGiao_EditValueChanged(object sender, EventArgs e)
        {
            List<Infor> Infor = LUE_HopDongBenGiao.Properties.DataSource as List<Infor>;
            if (Infor == null|| lUE_ChiTietBenGiao.EditValue==null)
                return;
            string codenhathau = lUE_ChiTietBenGiao.EditValue.ToString();
            if (Infor.FindAll(x => x.Code == codenhathau).FirstOrDefault() != null)
            {
                List<Infor> NewInfor = Infor.FindAll(x => x.Code == codenhathau);
                LUE_HopDongBenGiao.Properties.DataSource = NewInfor;
                LUE_HopDongBenGiao.EditValue = NewInfor.FirstOrDefault().CodeHD;
                LUE_HopDongBenGiao.Enabled = true;
            }
            else
            {
                LUE_HopDongBenGiao.EditValue = null;
                LUE_HopDongBenGiao.Enabled = false;
            }
        }

        private void tE_HopDongBenNhan_EditValueChanged(object sender, EventArgs e)
        {
            string queryStr = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"CodeDuAn\" = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string sohopdong = string.Format("{0, 0:D2}", dt.Rows.Count + 1) + "-" + DateTime.Now.Date.ToString("MM/dd/yyyy") + MyConstant.DIC_TaomoiSHD[m_range];
            if (tE_SoHopDong.Text == "")
            {
                tE_SoHopDong.Text = sohopdong;
            }
        }
        private void Fcn_LoadThongTinGrid()
        {
            foreach (var item in thongtinhopdong)
            {
                if (item.Key == dE_Begin || item.Key == dE_End)
                    continue;
                item.Key.Text = "";
            }
            lue_LoaiHD.ItemIndex = 0;
            string queryStr = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"CodeDonViThucHien\"='{m_codenhathau}'";
            DataTable dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            List<TongHopHopDong> TH = new List<TongHopHopDong>();
            List<Infor> InforMeCon = new List<Infor>();
            int stt = 1;
            foreach(DataRow row in dt_HD.Rows)
            {
                TongHopHopDong newHD = new TongHopHopDong();
                newHD.Code = row["Code"].ToString();
                newHD.STT = stt++;
                newHD.TenHopDong = row["TenHopDong"].ToString();
                newHD.SoHopDong = row["SoHopDong"].ToString();
                newHD.GiaTriHopDong =long.Parse(row["GiaTriHopDong"].ToString());
                TH.Add(newHD);
                queryStr = $"SELECT \"CodeCon\" FROM {MyConstant.Tbl_TAOMOIHOPDONG_MECON} WHERE \"CodeMe\"='{row["Code"]}'";
                DataTable dt_con = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                string lst = MyFunction.fcn_Array2listQueryCondition(dt_con.AsEnumerable().Select(x => x["CodeCon"].ToString()).ToArray());
                queryStr = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"Code\" IN ({lst})";
                DataTable dt_tencon = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                queryStr = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG_MECON} WHERE \"CodeCon\"='{row["Code"]}'";
                DataTable dt_me = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                string lstme = MyFunction.fcn_Array2listQueryCondition(dt_me.AsEnumerable().Select(x => x["CodeMe"].ToString()).ToArray());
                queryStr = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"Code\" IN ({lstme})";
                DataTable dt_tenme = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                string CodeCon = "", CodeMe = "";
                foreach(DataRow crcon in dt_tencon.Rows)
                {
                    CodeCon+=$",{crcon["Code"]}";
                    InforMeCon.Add(new Infor
                    {
                        Code= crcon["Code"].ToString(),
                        Ten= crcon["TenHopDong"].ToString()
                    });
                }
                if(CodeCon!="")
                    newHD.HopDongCon = CodeCon.Remove(0, 1);
                foreach (DataRow crme in dt_tenme.Rows)
                {
                    CodeMe += $",{crme["Code"]}";
                    InforMeCon.Add(new Infor
                    {
                        Code = crme["Code"].ToString(),
                        Ten = crme["TenHopDong"].ToString()
                    });
                }
                if (CodeMe != "")
                    newHD.HopDongMe = CodeMe.Remove(0, 1);
            }
            rCCBE_HopDongMeCon.DataSource = InforMeCon;
            gc_ChiTietHopDong.DataSource = TH;
        }
        private void sb_Ok_Click(object sender, EventArgs e)
        {
            if (!sb_Ok.Enabled)
                return;
            string dbString = "",code=Guid.NewGuid().ToString(),codeTH=Guid.NewGuid().ToString();
            tE_GiaTriHopDong.Text = tE_GiaTriHopDong.Text == "" ? "0" : tE_GiaTriHopDong.Text;
            Infor BenGiao = lUE_ChiTietBenGiao.GetSelectedDataRow() as Infor;
            if (BenGiao == null)
            {
                MessageShower.ShowWarning("Vui lòng điền đầy đủ thông tin!", "Cảnh báo");
                return;
            }
            if (LUE_HopDongBenGiao.EditValue == null&&m_range==MyConstant.TBL_THONGTINNHATHAU)
            {
                if(BenGiao.ColCode!="CodeDuAn")
                    dbString = $"INSERT INTO {MyConstant.Tbl_TAOMOIHOPDONG} (\"CodeLoaiHopDong\",\"CodeDuAn\",\"CodeNhaThau\",\"GiaTriHopDong\",\"TrangThai\",\"NgayBatDau\",\"NgayKetThuc\",\"Code\",\"TenHopDong\",\"SoHopDong\",\"CodeDonViThucHien\",\"CodeBenGiao\",'{BenGiao.ColCode}') " +
    $"VALUES ('{lue_LoaiHD.EditValue}','{SharedControls.slke_ThongTinDuAn.EditValue}','{m_codenhathau}','{tE_GiaTriHopDong.Text}','{"Đang thực hiện"}','{dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{code}',@TenHD,@SoHd,,'{m_codenhathau}','{lUE_ChiTietBenGiao.EditValue}','{lUE_ChiTietBenGiao.EditValue}')";
                else
                    dbString = $"INSERT INTO {MyConstant.Tbl_TAOMOIHOPDONG} (\"CodeLoaiHopDong\",\"CodeNhaThau\",\"GiaTriHopDong\",\"TrangThai\",\"NgayBatDau\",\"NgayKetThuc\",\"Code\",\"TenHopDong\",\"SoHopDong\",\"CodeDonViThucHien\",\"CodeBenGiao\",'{BenGiao.ColCode}') " +
        $"VALUES ('{lue_LoaiHD.EditValue}','{m_codenhathau}','{tE_GiaTriHopDong.Text}','{"Đang thực hiện"}','{dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'," +
        $"'{code}',@TenHD,@SoHD,'{m_codenhathau}','{lUE_ChiTietBenGiao.EditValue}','{lUE_ChiTietBenGiao.EditValue}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { tE_HopDongBenNhan.Text, tE_SoHopDong.Text });
            }
            else if(m_range != MyConstant.TBL_THONGTINNHATHAU)
            {
                dbString = $"INSERT INTO {MyConstant.Tbl_TAOMOIHOPDONG} (\"CodeLoaiHopDong\",\"CodeDuAn\",'{MyConstant.DIC_TaomoiHD[m_range]}',\"GiaTriHopDong\",\"TrangThai\",\"NgayBatDau\",\"NgayKetThuc\",\"Code\"," +
                    $"\"TenHopDong\",\"SoHopDong\",\"CodeDonViThucHien\",\"CodeBenGiao\",'{BenGiao.ColCode}') " +
$"VALUES ('{lue_LoaiHD.EditValue}','{SharedControls.slke_ThongTinDuAn.EditValue}','{m_codenhathau}','{tE_GiaTriHopDong.Text}','{"Đang thực hiện"}','{dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'," +
$"'{dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{code}',@TenHD,@SoHd,'{m_codenhathau}','{lUE_ChiTietBenGiao.EditValue}','{lUE_ChiTietBenGiao.EditValue}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { tE_HopDongBenNhan.Text, tE_SoHopDong.Text });
                if (LUE_HopDongBenGiao.EditValue != null)
                {
                    Infor CodeBenGiao = LUE_HopDongBenGiao.GetSelectedDataRow() as Infor;
                    dbString = $"INSERT INTO {MyConstant.Tbl_TAOMOIHOPDONG_MECON} (\"Code\",\"CodeMe\",\"CodeCon\") VALUES ('{Guid.NewGuid()}','{CodeBenGiao.CodeHD}','{code}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
            string CodeDot = Guid.NewGuid().ToString();
            m_CodeDot = CodeDot;
            dbString = $"INSERT INTO {MyConstant.TBL_HopDong_DotHopDong} (\"TrangThai\",\"Code\",\"CodeGiaiDoan\",\"Ten\",\"NgayBatDau\",\"NgayKetThuc\",\"CodeHd\")" +
                $" VALUES ('Đang thực hiện','{CodeDot}','{SharedControls.cbb_DBKH_ChonDot.SelectedValue}','Đợt 1','{dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}','{code}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            dbString = $"INSERT INTO '{MyConstant.TBL_Tonghopdanhsachhopdong}' (\"CodeHopDong\",\"Code\",\"CodeDuAn\") VALUES ('{code}','{codeTH}','{SharedControls.slke_ThongTinDuAn.EditValue}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            dbString = $"INSERT INTO '{MyConstant.TBL_ThongtinphulucHD}' (\"PhatSinh\",\"Code\",\"CodeLoaiHd\",\"CodeHd\",\"TenPl\") VALUES ('{0}','{Guid.NewGuid()}','{MyConstant.DIC_LOAIHOPDONG[LoaiHD[m_range]]}','{codeTH}','{"Phụ lục 1"}')";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            Form_PhuLucHopDong PLHD = new Form_PhuLucHopDong();
            PLHD.Fcn_UpdatePropoties(code, CodeDot, tE_HopDongBenNhan.Text);
            PLHD.ShowDialog();
            long ThanhTien = MyFunction.Fcn_UpdateGiaTriHopDong(code);
            tE_GiaTriHopDong.Text = ThanhTien == 0 ? "0" : ThanhTien.ToString();
            Form_ChiTietHopDong CTHD = new Form_ChiTietHopDong();
            CTHD.Fcn_UpdatePropoties(tE_HopDongBenNhan.Text, code,double.Parse(tE_GiaTriHopDong.Text));
            CTHD.ShowDialog();
            Fcn_LoadThongTinGrid();
            MessageShower.ShowInformation("Tạo hợp đồng thành công!", "");

        }

        private void rIBE_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult rs=MessageShower.ShowYesNoQuestion("Bạn có muốn xóa hợp đồng này không?", "Cảnh báo!");
            if (rs == DialogResult.No)
                return;
            TongHopHopDong TH = gv_ChiTietHopDong.GetFocusedRow() as TongHopHopDong;
            //string dbString = $"DELETE FROM {MyConstant.Tbl_TAOMOIHOPDONG_MECON} WHERE \"CodeMe\"='{TH.Code}' OR \"CodeCon\"='{TH.Code}' ";
            //DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            //dbString = $"DELETE FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"Code\"='{TH.Code}'";
            //DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            DuAnHelper.DeleteDataRows(MyConstant.Tbl_TAOMOIHOPDONG, new string[] {TH.Code});
            gv_ChiTietHopDong.DeleteSelectedRows();
        }
        
        private void rIBE_Chinh_Click(object sender, EventArgs e)
        {
            TongHopHopDong TH = gv_ChiTietHopDong.GetFocusedRow() as TongHopHopDong;
            fcn_loaddulieuhopdongdatao(false, TH.Code);
            sb_Cancel.Text = "Lưu nội dung thay đổi";
            sb_Ok.Enabled = false;
            m_codeHD = TH.Code;
            string dbString = $"SELECT \"Code\" FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"CodeHd\"='{TH.Code}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            m_CodeDot = dt.Rows[0][0].ToString();
            sb_Cancel.Click += (ss, ee) =>
            {
                if (sb_Cancel.Text != "Thoát")
                {
                    TH = gv_ChiTietHopDong.GetFocusedRow() as TongHopHopDong;
                    if(TH is null)
                    {
                        Fcn_LoadDaTa(m_tennhathau, m_range, m_codenhathau);
                        sb_Ok.Enabled = true;
                        sb_Cancel.Text = "Thoát";
                        return;
                    }
                    Fcn_UpdateChinhSua(m_codeHD);
                    Fcn_LoadDaTa(m_tennhathau, m_range, m_codenhathau);
                    sb_Ok.Enabled = true;
                    sb_Cancel.Text = "Thoát";
                }
            };
        }
        private void Fcn_UpdateChinhSua(string code)
        {
            string dbString = ""; string condition = "";

            foreach (var item in MyConstant.DIC_TaomoiHD)
            {
                if (MyConstant.DIC_TaomoiHD[m_range] != item.Value)
                    condition += $",'{item.Value}'=NULL";
            }
            dbString = $"UPDATE {MyConstant.Tbl_TAOMOIHOPDONG} SET \"CodeBenGiao\"=NULL";
            dbString += condition + $" WHERE \"Code\"='{code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            Infor BenGiao = lUE_ChiTietBenGiao.GetSelectedDataRow() as Infor;
            if (LUE_HopDongBenGiao.EditValue != null)
            {
                dbString = $"UPDATE {MyConstant.Tbl_TAOMOIHOPDONG} SET \"CodeLoaiHopDong\"='{lue_LoaiHD.EditValue}',\"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}',\"GiaTriHopDong\"='{tE_GiaTriHopDong.Text}','{BenGiao.ColCode}'='{lUE_ChiTietBenGiao.EditValue}', " +
         $"\"NgayBatDau\"='{dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}',\"NgayKetThuc\"='{dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}',\"TenHopDong\"=@TenHD,\"SoHopDong\"=@SoHD,\"CodeBenGiao\"='{lUE_ChiTietBenGiao.EditValue}',\"CodeDonViThucHien\"='{m_codenhathau}','{MyConstant.DIC_TaomoiHD[m_range]}'= '{m_codenhathau}' WHERE \"Code\"='{code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { tE_HopDongBenNhan.Text, tE_SoHopDong.Text });
                dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi_Hopdongmecon}";
                DataTable dt_mecon = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                bool check = false;
                foreach (DataRow row in dt_mecon.Rows)
                {
                    if (row["CodeCon"].ToString() == code)
                    {
                        dbString = $"UPDATE {MyConstant.Tbl_TAOMOIHOPDONG_MECON} SET \"CodeMe\"='{LUE_HopDongBenGiao.EditValue}'WHERE \"CodeCon\"='{code}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        check = true;
                    }
                    else if (row["CodeMe"].ToString() == code && row["CodeCon"].ToString() == LUE_HopDongBenGiao.EditValue.ToString())
                    {
                        dbString = $"UPDATE {MyConstant.Tbl_TAOMOIHOPDONG_MECON} SET \"CodeMe\"='{LUE_HopDongBenGiao.EditValue}',\"CodeCon\"='{code}'WHERE \"CodeMe\"='{code}'";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        check = true;
                    }
                }
                if (!check)
                {
                    dbString = $"INSERT INTO {MyConstant.Tbl_TAOMOIHOPDONG_MECON} (\"Code\",\"CodeMe\",\"CodeCon\") VALUES ('{Guid.NewGuid().ToString()}','{LUE_HopDongBenGiao.EditValue}','{code}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
            else
            {
                if (BenGiao.ColCode != "CodeDuAn")
                    dbString = $"UPDATE {MyConstant.Tbl_TAOMOIHOPDONG} SET \"CodeLoaiHopDong\"='{lue_LoaiHD.EditValue}',\"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}',\"GiaTriHopDong\"='{tE_GiaTriHopDong.Text}','{BenGiao.ColCode}'='{lUE_ChiTietBenGiao.EditValue}', " +
         $"\"NgayBatDau\"='{dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}',\"NgayKetThuc\"='{dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}',\"TenHopDong\"=@TenHD,\"SoHopDong\"=@SoHD,\"CodeBenGiao\"='{lUE_ChiTietBenGiao.EditValue}',\"CodeDonViThucHien\"='{m_codenhathau}',\"CodeNhaThau\"= '{m_codenhathau}' WHERE \"Code\"='{code}'";
                else
                    dbString = $"UPDATE {MyConstant.Tbl_TAOMOIHOPDONG} SET \"CodeLoaiHopDong\"='{lue_LoaiHD.EditValue}',\"GiaTriHopDong\"='{tE_GiaTriHopDong.Text}','{BenGiao.ColCode}'='{lUE_ChiTietBenGiao.EditValue}', " +
$"\"NgayBatDau\"='{dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}',\"NgayKetThuc\"='{dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}',\"TenHopDong\"=@TenHD,\"SoHopDong\"=@SoHD,\"CodeBenGiao\"='{lUE_ChiTietBenGiao.EditValue}',\"CodeDonViThucHien\"='{m_codenhathau}',\"CodeNhaThau\"= '{m_codenhathau}' WHERE \"Code\"='{code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { tE_HopDongBenNhan.Text, tE_SoHopDong.Text });
            }
            MessageShower.ShowInformation("Cập nhập thay đổi thành công!", "Thông báo");
        }
        private void rIBE_XemTruoc_Click(object sender, EventArgs e)
        {
            TongHopHopDong TH = gv_ChiTietHopDong.GetFocusedRow() as TongHopHopDong;
            fcn_loaddulieuhopdongdatao(true, TH.Code);
            m_codeHD = TH.Code;
            sb_Ok.Enabled = false;
            sb_Cancel.Text = "Thoát chế độ xem trước";
        }
        private void fcn_loaddulieuhopdongdatao(bool method, string Code)
        {
            string dbString = $"SELECT *  FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"Code\"='{Code}'";
            DataTable dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT \"CodeMe\" FROM {MyConstant.Tbl_TAOMOIHOPDONG_MECON} WHERE \"CodeCon\"='{Code}'";
            DataTable dt_me = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            lue_LoaiHD.EditValue = dt_HD.Rows[0]["CodeLoaiHopDong"].ToString();
            lue_LoaiHD.Enabled = method ? false : true;
            if (dt_me.Rows.Count != 0)
            {
                dbString = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"Code\"='{dt_me.AsEnumerable().FirstOrDefault()[0].ToString()}'";
                DataTable dt_tenme = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                LUE_HopDongBenGiao.EditValue = dt_tenme.AsEnumerable().FirstOrDefault()["Code"].ToString();
            }
            else
                LUE_HopDongBenGiao.EditValue = null;
            lUE_ChiTietBenGiao.EditValue = dt_HD.AsEnumerable().FirstOrDefault()["CodeBenGiao"].ToString();
            foreach (var item in thongtinhopdong)
            {
                if (dt_HD.Columns.Contains(item.Value))
                {
                    if (item.Key == dE_Begin)
                        dE_Begin.DateTime = DateTime.Parse(dt_HD.AsEnumerable().FirstOrDefault()[item.Value].ToString());
                    else if (item.Key == dE_End)
                        dE_End.DateTime = DateTime.Parse(dt_HD.AsEnumerable().FirstOrDefault()[item.Value].ToString());
                    else
                        item.Key.Text = dt_HD.AsEnumerable().FirstOrDefault()[item.Value].ToString();
                }

                if (method)
                {
                    item.Key.Enabled = false;
                    lUE_ChiTietBenGiao.Enabled = false;
                    LUE_HopDongBenGiao.Enabled = false;
                }
                else
                {
                    item.Key.Enabled = true;
                    lUE_ChiTietBenGiao.Enabled = true;
                    LUE_HopDongBenGiao.Enabled = true;
                }
            }

        }

        private void btn_ThemThanhToan_Click(object sender, EventArgs e)
        {
            if(sb_Cancel.Text == "Thoát")
            {
                MessageShower.ShowError("Vui lòng tạo mới hợp đồng trước khi thêm chi tiết!");
                return;
            }
                
            Form_ChiTietHopDong CTHD = new Form_ChiTietHopDong();
            CTHD.Fcn_UpdatePropoties(tE_HopDongBenNhan.Text, m_codeHD, double.Parse(tE_GiaTriHopDong.Text));
            CTHD.ShowDialog();
        }

        private void sb_AddPhuLuc_Click(object sender, EventArgs e)
        {
            if (sb_Cancel.Text == "Thoát")
            {
                MessageShower.ShowError("Vui lòng tạo mới hợp đồng trước khi thêm phụ lục!");
                return;
            }
            Form_PhuLucHopDong PLHD = new Form_PhuLucHopDong();
            PLHD.Fcn_UpdatePropoties(m_codeHD, m_CodeDot, tE_HopDongBenNhan.Text);
            PLHD.ShowDialog();
            long ThanhTien = MyFunction.Fcn_UpdateGiaTriHopDong(m_codeHD);
            tE_GiaTriHopDong.Text = ThanhTien == 0 ? "0" : ThanhTien.ToString();
        }

        private void LUE_HopDongBenGiao_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void sb_Cancel_Click(object sender, EventArgs e)
        {
            if (sb_Cancel.Text == "Thoát")
                this.Close();
            else if (sb_Cancel.Text == "Thoát chế độ xem trước")
            {
                foreach (var item in thongtinhopdong)
                    item.Key.Enabled = true;
                lue_LoaiHD.Enabled = true;
                lue_LoaiHD.ItemIndex = 0;
                lUE_ChiTietBenGiao.Enabled = true;
                LUE_HopDongBenGiao.Enabled = true;
                foreach (var item in thongtinhopdong)
                {
                    if (item.Key == dE_Begin || item.Key == dE_End)
                        continue;
                    item.Key.Text = "";
                }
                lUE_ChiTietBenGiao.EditValue = null;
                LUE_HopDongBenGiao.EditValue = null;
                sb_Cancel.Text = "Thoát";
                sb_Ok.Enabled = true;
            }
        }

        public Form_TaoMoiHopDong()
        {
            InitializeComponent();
            thongtinhopdong = new Dictionary<Control, string>
            {
                {tE_GiaTriHopDong,"GiaTriHopDong" },
                {tE_HopDongBenNhan,"TenHopDong" },
                {tE_SoHopDong,"SoHopDong" },
                {dE_Begin,"NgayBatDau"},
                {dE_End,"NgayKetThuc"}
            };         
            LoaiHD = new Dictionary<string, string>
            {
            { MyConstant.TBL_THONGTINNHATHAU,"HỢP ĐỒNG CHÍNH A_B"},
            { MyConstant.TBL_THONGTINNHATHAUPHU,"HỢP ĐỒNG THẦU PHỤ B_B" },
            { MyConstant.TBL_THONGTINNHACUNGCAP,"HỢP ĐỒNG CUNG CẤP" },
            { MyConstant.TBL_THONGTINTODOITHICONG,"HỢP ĐỒNG TỔ ĐỘI" },
            };

        }
        public void Fcn_LoadDaTa(string TenNhaThau,string range,string CodeNhaThau)
        {           
            m_codenhathau = CodeNhaThau;
            m_tennhathau = TenNhaThau;
            m_range = range;
            lc_DuAn.Text = SharedControls.slke_ThongTinDuAn.Text;
            lc_DonViThucHien.Text = TenNhaThau;
            if (range != MyConstant.TBL_THONGTINNHACUNGCAP)
            {
                string dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
$"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
$"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
$"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
$"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
                if (range != MyConstant.TBL_THONGTINNHATHAU)
                    dbString += $"AND {TDKH.TBL_ChiTietCongTacTheoKy}.{MyConstant.DIC_TaomoiHD[range]} IS NOT NULL ";
                DataTable dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                dbString = $"SELECT *FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'  AND {TDKH.TBL_ChiTietCongTacTheoKy}.{MyConstant.DIC_TaomoiHD[range]} IS NOT NULL ";
                DataTable dtTheoChuKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dtTheoChuKy.Rows.Count == 0)
                {
                    dE_Begin.DateTime = DateTime.Now;
                    dE_End.DateTime = dE_Begin.DateTime.AddDays(30);
                    goto Label;
                }
                DateTime Min_KH = dtTheoChuKy.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                DateTime Max_KH = dtTheoChuKy.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                if (dttc.Rows.Count != 0)
                {
                    dE_End.DateTime = Max_KH;
                    dE_Begin.DateTime = Min_KH;
                    DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                    dE_End.DateTime = Max_KH >= Max_TC ? Max_KH : Max_TC;
                    DateTime Min_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                    dE_Begin.DateTime = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
                }
            }
            else
            {
                dE_Begin.DateTime = DateTime.Now;
                dE_End.DateTime = dE_Begin.DateTime.AddDays(30);
            }
            Label:
            List<Infor> Infor_CTBenGiao = new List<Infor>();
            List<Infor> Infor_HDMe = new List<Infor>();
            string queryStr = "";
            DataTable dt = null;
            queryStr = $"SELECT *  FROM {MyConstant.Tbl_HopDong_LoaiHopDong}";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            List<Infor> Infor_LoaiHD = DatatableHelper.fcn_DataTable2List<Infor>(dt);
            lue_LoaiHD.Properties.DataSource = Infor_LoaiHD;
            lue_LoaiHD.EditValue = Infor_LoaiHD.FirstOrDefault().Code; ;
            if (m_range != MyConstant.TBL_THONGTINNHATHAU)
            {
                queryStr = $"SELECT *  FROM {MyConstant.TBL_THONGTINNHATHAU} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                string lst = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
                queryStr = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"CodeNhaThau\" IN ({lst}) AND \"CodeNhaThau\"=\"CodeDonViThucHien\"";
                dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                foreach (DataRow row in dt.Rows)
                {
                    Infor_HDMe.Add(new Infor
                    {
                        Code = row["CodeNhaThau"].ToString(),
                        Ten = row["TenHopDong"].ToString(),
                        CodeHD = row["Code"].ToString()
                    });
                }
                LUE_HopDongBenGiao.Properties.DataSource = Infor_HDMe;
            }
            else
                LUE_HopDongBenGiao.Enabled = false;
            foreach (var item in MyConstant.DIC_TaomoiHD)
            {
                if (item.Key != m_range && item.Key != MyConstant.TBL_THONGTINHANGMUC && item.Key != MyConstant.TBL_THONGTINDUAN)
                {
                    if (item.Key == MyConstant.TBL_THONGTINCONGTRINH)
                    {
                        if (m_range != MyConstant.TBL_THONGTINCONGTRINH && m_range != MyConstant.TBL_THONGTINNHATHAU)
                            continue;
                        Infor_CTBenGiao.Add(new Infor
                        {
                            Code = SharedControls.slke_ThongTinDuAn.EditValue.ToString(),
                            Ten = SharedControls.slke_ThongTinDuAn.Text,
                            Decription = "Dự Án",
                            ColCode = "CodeDuAn"
                        });

                        queryStr = $"SELECT \"Code\",\"Ten\"  FROM {item.Key} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                        dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                        foreach (DataRow row in dt.Rows)
                        {
                            Infor_CTBenGiao.Add(new Infor
                            {
                                Code = row["Code"].ToString(),
                                Ten = row["Ten"].ToString(),
                                Decription = SharedControls.slke_ThongTinDuAn.Text,
                                ColCode = "CodeCongTrinh"
                            });
                            queryStr = $"SELECT \"Code\",\"Ten\"  FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{row["Code"]}'";
                            DataTable dt_hm = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                            foreach (DataRow rows in dt_hm.Rows)
                            {
                                Infor_CTBenGiao.Add(new Infor
                                {
                                    Code = rows["Code"].ToString(),
                                    Ten = rows["Ten"].ToString(),
                                    Decription = row["Ten"].ToString(),
                                    ColCode = "CodeHangMuc"
                                });
                            }
                        }
                        continue;
                    }
                    else if (item.Key == MyConstant.TBL_THONGTINNHATHAU)
                    {
                        //goto NextCode;
                        queryStr = $"SELECT * FROM {item.Key} WHERE \"CodeDuAn\"<>\"Code\" AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                        dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                        foreach (DataRow row in dt.Rows)
                        {
                            Infor_CTBenGiao.Add(new Infor
                            {
                                Code = row["Code"].ToString(),
                                Ten = row["Ten"].ToString(),
                                Decription = MyConstant.DIC_TenNhathau[item.Key],
                                ColCode = "CodeNhaThau"
                            });
                        }
                    }
                }

            }
            lUE_ChiTietBenGiao.Properties.DataSource = Infor_CTBenGiao;
            lUE_ChiTietBenGiao.EditValue = Infor_CTBenGiao.FirstOrDefault().Code;
            if (Infor_CTBenGiao.FindAll(x => x.ColCode == "CodeNhaThau").FirstOrDefault() != null)
            {
                lc_HopDongThu_Chi.Text = "Hợp đồng chi";
                lc_HopDongThu_Chi.ForeColor = Color.Green;
            }
            Fcn_LoadThongTinGrid();
        }
    }
}