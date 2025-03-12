using DevExpress.Utils;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Report;
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
    public partial class Form_BaoCaoVatLieu : DevExpress.XtraEditors.XtraForm
    {
        public static string[] LoaiVL = { "Thủ công","Tiến độ kế hoạch","Hợp đồng","Kế hoạch vật tư"};
        public Form_BaoCaoVatLieu()
        {
            InitializeComponent();
            dv_BaoCao.Hide();
        }
        public int Check
        {
            get
            {
                if (ce_ChiTiet.Checked)
                    return 0;
                else return 1;
            }
            set
            {
                if (value == 0) ce_ChiTiet.Checked = true;
                else ce_ChiTiet.Checked = false;
            }
        }

        private void sb_xuatbaocao_Click(object sender, EventArgs e)
        {
            string dbString = $"DELETE FROM Tbl_BaoCaoVatLieu";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);
            dbString = $"DELETE FROM Tbl_LoaiVatLieu";
            DataProvider.InstanceBaoCao.ExecuteNonQuery(dbString);

            dbString = $"SELECT * FROM Tbl_LoaiVatLieu";
            DataTable dt_LoaiVL = DataProvider.InstanceBaoCao.ExecuteQuery(dbString);  
            
            dbString = $"SELECT * FROM Tbl_BaoCaoVatLieu";
            DataTable dt_BaoCao = DataProvider.InstanceBaoCao.ExecuteQuery(dbString);
            DataTable VL=new DataTable();
            DataTable VLHN=new DataTable();
            double KLNhap = 0,KLKH=0,SumKLNhap;
            string Code="";
            string ngaybatdau =de_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string ngayketthuc = de_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            foreach (string item in LoaiVL)
            {
                KLKH = 0;
                SumKLNhap = 0;
                Code = Guid.NewGuid().ToString();
                if (item=="Thủ công")
                {
                    dbString = $"SELECT {QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl,{QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu,{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_NHAPVT}.Code " +
                        $"FROM {QLVT.TBL_QLVT_YEUCAUVT} " +
                        $"INNER JOIN {QLVT.TBL_QLVT_NHAPVT} " +
                        $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code={QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
                        $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                        $"AND {QLVT.TBL_QLVT_YEUCAUVT}.CodeKHVT IS NULL AND {QLVT.TBL_QLVT_YEUCAUVT}.CodeHd IS NULL AND {QLVT.TBL_QLVT_YEUCAUVT}.CodeTDKH IS NULL ";
                    VL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                }
                else if(item == "Hợp đồng")
                {
                    dbString = $"SELECT {QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl,{QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu,{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_NHAPVT}.Code " +
    $"FROM {QLVT.TBL_QLVT_YEUCAUVT} " +
    $"INNER JOIN {QLVT.TBL_QLVT_NHAPVT} " +
    $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code={QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
    $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
    $"AND {QLVT.TBL_QLVT_YEUCAUVT}.CodeHd IS NOT NULL ";
                    VL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                }
                else if(item == "Tiến độ kế hoạch")
                {
                    dbString = $"SELECT {QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl,{QLVT.TBL_QLVT_YEUCAUVT}.CodeKHVT,{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu,{QLVT.TBL_QLVT_NHAPVT}.Code " +
    $"FROM {QLVT.TBL_QLVT_YEUCAUVT} " +
    $"INNER JOIN {QLVT.TBL_QLVT_NHAPVT} " +
    $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code={QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
    $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
    $"AND {QLVT.TBL_QLVT_YEUCAUVT}.CodeKHVT IS NOT NULL";
                    VL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    foreach (DataRow row in VL.Rows)
                    {
                        KLKH += double.Parse(row["HopDongKl"].ToString());
                        dbString = $"SELECT * FROM {TDKH.TBL_KHVT_KhoiLuongHangNgay} WHERE \"CodeVatTu\"='{row["CodeKHVT"]}' AND \"Ngay\">='{ngaybatdau}' AND \"Ngay\"<='{ngayketthuc}' ";
                        VLHN = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        KLNhap = VLHN.Rows.Count == 0 ? 0 : VLHN.AsEnumerable().Sum(x => double.Parse(x["KhoiLuongKeHoach"].ToString()));
                        SumKLNhap += KLNhap;
                        DataRow NewRow = dt_BaoCao.NewRow();
                        NewRow["Code"] = Guid.NewGuid().ToString();
                        NewRow["CodeLoaiVL"] = Code;
                        NewRow["TenVatLieu"] = row["TenVatTu"];
                        NewRow["DonVi"] = row["DonVi"];
                        NewRow["KhoiLuongKeHoach"] = row["HopDongKl"];
                        NewRow["KhoiLuongThucNhap"] =Math.Round(KLNhap, 4);
                        dt_BaoCao.Rows.Add(NewRow);
                    }
                    DataRow NewLoai = dt_LoaiVL.NewRow();
                    NewLoai["Code"] = Code;
                    NewLoai["Ten"] = item;
                    NewLoai["TongKeHoach"] =Math.Round(KLKH, 4);
                    NewLoai["TongThucNhap"] =Math.Round(SumKLNhap, 4);
                    dt_LoaiVL.Rows.Add(NewLoai);
                    continue;
                }
                else
                {
                    dbString = $"SELECT {QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl,{QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu,{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_NHAPVT}.Code " +
$"FROM {QLVT.TBL_QLVT_YEUCAUVT} " +
$"INNER JOIN {QLVT.TBL_QLVT_NHAPVT} " +
$"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code={QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
$"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
$"AND {QLVT.TBL_QLVT_YEUCAUVT}.CodeTDKH IS NOT NULL";
                    VL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                }
                foreach (DataRow row in VL.Rows)
                {
                    KLKH += row["HopDongKl"]==DBNull.Value?0: double.Parse(row["HopDongKl"].ToString());
                    dbString = $"SELECT * FROM {QLVT.TBL_QLVT_NHAPVTKLHN} WHERE \"CodeCha\"='{row["Code"]}' AND \"Ngay\">='{ngaybatdau}' AND \"Ngay\"<='{ngayketthuc}' ";
                    VLHN = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    KLNhap = VLHN.Rows.Count == 0 ? 0 : VLHN.AsEnumerable().Sum(x => double.Parse(x["KhoiLuong"].ToString()));
                    SumKLNhap += KLNhap;
                    DataRow NewRow = dt_BaoCao.NewRow();
                    NewRow["Code"] = Guid.NewGuid().ToString();
                    NewRow["CodeLoaiVL"] = Code;
                    NewRow["DonVi"] = row["DonVi"];
                    NewRow["TenVatLieu"] = row["TenVatTu"];
                    NewRow["KhoiLuongKeHoach"] = row["HopDongKl"];
                    NewRow["KhoiLuongThucNhap"] =Math.Round(KLNhap, 4);
                    dt_BaoCao.Rows.Add(NewRow);
                }
                if (VL.Rows.Count == 0)
                {
                    DataRow NewRow = dt_BaoCao.NewRow();
                    NewRow["Code"] = Guid.NewGuid().ToString();
                    NewRow["CodeLoaiVL"] = Code;
                    NewRow["TenVatLieu"] = "";
                    NewRow["DonVi"] = "";
                    NewRow["KhoiLuongKeHoach"] = 0;
                    NewRow["KhoiLuongThucNhap"] = 0;
                    dt_BaoCao.Rows.Add(NewRow);
                }
                DataRow New = dt_LoaiVL.NewRow();
                New["Code"] = Code;
                New["Ten"] = item;
                New["TongKeHoach"] =Math.Round(KLKH, 4);
                New["TongThucNhap"] =Math.Round(SumKLNhap, 4);
                dt_LoaiVL.Rows.Add(New);
            }
            BaoCaoVatLieu Baocao = new BaoCaoVatLieu();
            DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dt_LoaiVL, "Tbl_LoaiVatLieu");
            DataProvider.InstanceBaoCao.UpdateDataTableFromSqliteSource(dt_BaoCao, "Tbl_BaoCaoVatLieu");
            Baocao.Fcn_Setting((DefaultBoolean)Check,de_Begin.DateTime,de_End.DateTime);
            dv_BaoCao.UseAsyncDocumentCreation = DefaultBoolean.True;
            dv_BaoCao.RequestDocumentCreation = true;
            dv_BaoCao.DocumentSource = Baocao;
            dv_BaoCao.InitiateDocumentCreation();
            dv_BaoCao.Visible = true;
            MessageShower.ShowInformation("Xuất báo cáo thành công!", "Thông tin");


        }
    }
}