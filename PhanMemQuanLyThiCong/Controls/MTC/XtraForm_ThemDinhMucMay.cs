using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model.MayThiCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.MTC
{
    public partial class XtraForm_ThemDinhMucMay : DevExpress.XtraEditors.XtraForm
    {
        private string _CodeChiTiet { get; set; }

        public delegate void DE_TransDanhSachDinhMuc(List<MTC_ChiTietDinhMuc> CTDM, string Code, string ColCode);
        public DE_TransDanhSachDinhMuc de_TranDanhSachDM;
        public string _Code;//codetheogiaidoan hay codethucong
        public string _ColCode;//codetheogiaidoan hay codeThuCong
        public string _Date;//Ngày thêm định mức
        //List<MTC_ChiTietHangNgay> _ChiTiet;
        public XtraForm_ThemDinhMucMay(string Code,string ColCode,string Date)
        {
            InitializeComponent();
            _Code = Code;
            _ColCode = ColCode;
            _Date = Date;
        }
        private void Fcn_UpdateData()
        {
            List<MTC_ChiTietDinhMuc> MTC = new List<MTC_ChiTietDinhMuc>();
            //string dbString = $"SELECT * FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} " +
            //    $"WHERE {_ColCode}='{_Code}' AND \"NgayThang\"='{_Date}'";
            //List<MTC_ChiTietHangNgay> ChiTiet = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
            //_ChiTiet = ChiTiet;
            //string LstCode = MyFunction.fcn_Array2listQueryCondition(ChiTiet.Select(x => x.CodeDinhMuc).ToArray());
            //         string Db_String = $"SELECT May.Ten as TenMay,CTDM.DonVi,CTDM.MucTieuThu,CTDM.GhiChu," +
            //$"DM.DinhMucCongViec,MayDA.Code as CodeMay,False as Chon,CTDM.Code " +
            //$"  FROM {MyConstant.TBL_MTC_DANHSACHMAY} May" +
            //$" LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUC} CTDM ON CTDM.CodeMay=May.Code " +
            //$" LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUCTHAMKHAO} DM ON DM.Code=CTDM.CodeDinhMuc" +
            //$" LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} MayDA ON MayDA.CodeMay=May.Code WHERE MayDA.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            //         List<MTC_ChiTietDinhMuc> CTDM = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietDinhMuc>(Db_String); 
            string Db_String = $"SELECT May.Ten as TenMay," +
   $"MayDA.Code as CodeMay,May.Code " +
   $"  FROM {MyConstant.TBL_MTC_DANHSACHMAY} May" +
   $" LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} MayDA ON MayDA.CodeMay=May.Code" +
   $" WHERE MayDA.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            List<MTC_ChiTietDinhMuc> DanhSachMay = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietDinhMuc>(Db_String);
            string LstCode = MyFunction.fcn_Array2listQueryCondition(DanhSachMay.Select(x => x.Code).ToArray());

            Db_String = $"SELECT CTDM.DonVi,CTDM.MucTieuThu,CTDM.GhiChu," +
   $"DM.DinhMucCongViec,CTDM.CodeMay,CTDM.Code " +
   $" FROM {MyConstant.TBL_MTC_CHITIETDINHMUC} CTDM" +
   $" LEFT JOIN {MyConstant.TBL_MTC_CHITIETDINHMUCTHAMKHAO} DM ON DM.Code=CTDM.CodeDinhMuc" +
   $" WHERE CTDM.CodeMay IN ({LstCode})";
            List<MTC_ChiTietDinhMuc> CTDM = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietDinhMuc>(Db_String);

            foreach (var item in DanhSachMay)
            {
                MTC.Add(new MTC_ChiTietDinhMuc
                {
                    Code = item.CodeMay,
                    CodeMay = "0",
                    DinhMucCongViec = item.TenMay
                });
                if (CTDM.Count() != 0)
                {
                    List<MTC_ChiTietDinhMuc> CTDMNew = CTDM.Where(x => x.CodeMay == item.Code).ToList();
                    foreach(var Crow in CTDMNew)
                    {
                        MTC.Add(new MTC_ChiTietDinhMuc
                        {
                            Code = Crow.Code,
                            CodeMay = item.CodeMay,
                            DinhMucCongViec = Crow.DinhMucCongViec,
                            DonVi = Crow.DonVi,
                            MucTieuThu = Crow.MucTieuThu,
                            GhiChu = Crow.GhiChu
                        });
                    }
                }
            }
            tl_TenDinhMuc.DataSource = MTC;
            tl_TenDinhMuc.RefreshDataSource();
            tl_TenDinhMuc.Refresh();
            tl_TenDinhMuc.ExpandAll();
        }
        private void sb_Save_Click(object sender, EventArgs e)
        {
            List<MTC_ChiTietDinhMuc> CTDM = tl_TenDinhMuc.DataSource as List<MTC_ChiTietDinhMuc>;
            List<MTC_ChiTietDinhMuc> CTDMNew = CTDM.Where(x => x.Chon).ToList();
            if (CTDMNew.Count == 0)
            {
                this.Close();
                return;
            }
            de_TranDanhSachDM(CTDMNew, _Code,_ColCode);
            this.Close();
        }

        private void XtraForm_ThemDinhMucMay_Load(object sender, EventArgs e)
        {
            Fcn_UpdateData();
        }

        private void tl_TenDinhMuc_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Red;
            }    
        }

        private void tl_TenDinhMuc_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.Level == 0&& object.Equals(e.CellValue,(double)0))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void sb_ChonAll_Click(object sender, EventArgs e)
        {
            tl_TenDinhMuc.CheckAll();
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            tl_TenDinhMuc.UncheckAll();
        }

        private void tl_TenDinhMuc_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                e.Node.ParentNode.Checked = true;
            }
        }
    }
}