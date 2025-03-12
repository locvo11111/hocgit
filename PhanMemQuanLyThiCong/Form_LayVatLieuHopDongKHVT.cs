using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
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
    public partial class Form_LayVatLieuHopDongKHVT : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DE__TRUYENDATA(List<KeHoachVatTu> KHVT);
        public DE__TRUYENDATA m_TruyenData;
        public Form_LayVatLieuHopDongKHVT()
        {
            InitializeComponent();
        }
        private void Fcn_LoadData()
        {
            var ls = SharedControls.slke_ThongTinDuAn.Properties.DataSource as List<KeyValuePair<string, string>>;
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, false, true);
            lUE_ToChucCaNhan.Properties.DataSource = Infor;
            lUE_ToChucCaNhan.EditValue = Infor.FirstOrDefault().ID;
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, QLVT.TBL_QLVT_KHVT_CongTrinh, QLVT.TBL_QLVT_KHVT_HangMuc, false);
            string lstCode = MyFunction.fcn_Array2listQueryCondition(dtHM.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            string dbString = $"SELECT * FROM {QLVT.TBL_QLVT_KHVT} WHERE \"CodeHangMuc\" IN ({lstCode})";
            DataTable VL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KeHoachVatTu> KHVT = DuAnHelper.ConvertToList<KeHoachVatTu>(VL);
            gc_TimKiemVatLieu.DataSource = KHVT;
        }

        private void sB_All_Click(object sender, EventArgs e)
        {
            List<KeHoachVatTu> KHVT = (gv_TimKiemYeuCau.DataSource as List<KeHoachVatTu>);
            KHVT.ForEach(x => x.Chon = true);

            //foreach (DataRow row in (gc_TimKiemVatLieu.DataSource as DataTable).Rows)
            //    row["Chon"] = true;
            gc_TimKiemVatLieu.RefreshDataSource();
            gc_TimKiemVatLieu.Refresh();
        }

        private void sB_Cancel_Click(object sender, EventArgs e)
        {
            List<KeHoachVatTu> KHVT = (gv_TimKiemYeuCau.DataSource as List<KeHoachVatTu>);
            KHVT.ForEach(x => x.Chon = false);

            //foreach (DataRow row in (gc_TimKiemVatLieu.DataSource as DataTable).Rows)
            //    row["Chon"] = true;
            gc_TimKiemVatLieu.RefreshDataSource();
            gc_TimKiemVatLieu.Refresh();
        }

        private void sB_ok_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu", "Vui Lòng chờ!");
            List<KeHoachVatTu> KHVT = (gv_TimKiemYeuCau.DataSource as List<KeHoachVatTu>).FindAll(x => x.Chon);
            KHVT.ForEach(x => x.Index = rg_Select.SelectedIndex);
            KHVT.ForEach(x => x.CodeHangMuc = lue_HangMuc.EditValue.ToString());
            m_TruyenData(KHVT);
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }

        private void lUE_ToChucCaNhan_EditValueChanged(object sender, EventArgs e)
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\"='{lUE_ToChucCaNhan.EditValue}'";
            DataTable dtHM = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<Infor> InFor = DuAnHelper.ConvertToList<Infor>(dtHM);
            lue_HangMuc.Properties.DataSource = InFor;
            lue_HangMuc.EditValue = InFor.FirstOrDefault().Code;
        }

        private void Form_LayVatLieuHopDongKHVT_Load(object sender, EventArgs e)
        {
            Fcn_LoadData();
        }
    }
}