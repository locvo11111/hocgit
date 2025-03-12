using Dapper;
using DevExpress.Xpo.DB;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
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
    public partial class XtraForm_QuyDoiDonVi : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_QuyDoiDonVi()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void XtraForm_QuyDoiDonVi_Load(object sender, EventArgs e)
        {
            var dvths = DuAnHelper.GetDonViThucHiens(ctrl_DVTH);
            ctrl_DVTH.EditValue = dvths.SingleOrDefault(x => x.Code == SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH.Code)?.CodeFk;

            var crDVTH = ctrl_DVTH.SelectedDVTH;

            if (crDVTH is null)
            {
                tl_CongTac.DataSource = null;
                ctrl_HaoPhiVatTu.Clear();
                return;
            }

            string dbString = $@"SELECT dmct.DonVi 
                FROM {Server.Tbl_TDKH_DanhMucCongTac} dmct
                JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk 
                ON dmct.Code = cttk.CodeCongTac 
                WHERE cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}'";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            cbb_DonViGoc.SelectedIndex = -1;
            cbb_DonViGoc.Properties.Items.Clear();
            cbb_DonViGoc.Properties.Items.AddRange(dt.AsEnumerable().Select(x => (string)x["DonVi"]).ToArray());
            

        }

        private void ctrl_DVTH_DVTHChanged(object sender, EventArgs e)
        {
            string dbString = $@"SELECT * 
FROM {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan}";
        }

        private void LoadCongTac()
        {
            var crDVTH = ctrl_DVTH.SelectedDVTH;

            if (crDVTH is null || cbb_DonViGoc.SelectedIndex < 0)
            {
                tl_CongTac.DataSource = null;
                ctrl_HaoPhiVatTu.Clear();
                return;
            }
            string dbString = $@"SELECT cttk.Code, dmct.DonVi, dmct.MaHieuCongTac, dmct.TenCongTac, cttk.DonVi AS DonViQuyDoi, cttk.HeSoQuyDoiDonVi, (cttk.KhoiLuongToanBo/COALESCE(cttk.HeSoQuyDoiDonVi, 1)) AS KhoiLuongGoc, (cttk.KhoiLuongToanBo*cttk.HeSoQuyDoiDonVi) AS KhoiLuongQuyDoi
FROM {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk 
JOIN {Server.Tbl_TDKH_DanhMucCongTac} dmct
ON cttk.CodeCongTac = dmct.Code
WHERE {crDVTH.ColCodeFK} = '{crDVTH.Code}'
AND dmct.DonVi = '{cbb_DonViGoc.SelectedText}'";

            var dt = DataProvider.InstanceTHDA.ExecuteQueryModel<QuyDoiDonVi>(dbString);

            foreach (var item in dt)
            {
                item.HeSoNew = (double)nud_HeSoQuyDoi.Value;
                if (!ce_ThayDoiDinhMuc.Checked)
                    item.KhoiLuongNewCustom = item.KhoiLuongGoc;
            }

            tl_CongTac.DataSource = dt;
        }

        private void cbb_DonViGoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCongTac();
        }

        private void txt_DonViQuyDoi_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void nud_HeSoQuyDoi_ValueChanged(object sender, EventArgs e)
        {
            var dt = tl_CongTac.DataSource as List<QuyDoiDonVi>;

            if (dt is null)
                return;

            foreach (var item in dt)
            {
                item.HeSoNew = (double)nud_HeSoQuyDoi.Value;
            }
            tl_CongTac.RefreshDataSource();
        }

        private void bt_UpDate_Click(object sender, EventArgs e)
        {
            var nodes = tl_CongTac.GetAllCheckedNodes();
            if (!nodes.Any())
            {
                MessageShower.ShowWarning("Chưa chọn công tác nào");
                return;
            }

            string noti = $"Đơn vị sẽ được quy đổi như sau \"1x{cbb_DonViGoc.SelectedText} = {nud_HeSoQuyDoi.Value}x{txt_DonViQuyDoi.Text}\"" +
                ((ce_ThayDoiDinhMuc.Checked) ? $"(Định mức/Hao phí sẽ được thay đổi)" : "(Không thay đổi hao phí/Định mức)");

            if (MessageShower.ShowYesNoQuestion(noti) != DialogResult.Yes)
                return;

            List<string> updates = new List<string>();
            foreach (var node in nodes)
            {
                QuyDoiDonVi row = tl_CongTac.GetDataRecordByNode(node) as QuyDoiDonVi;

                string updateKL = "";
                if (ce_DoiKhoiLuongCongTac.Checked)
                {
                    updateKL = $", KhoiLuongToanBo = '{row.KhoiLuongNew}'";
                }
                string dbString = $"UPDATE {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} " +
                    $"SET DonVi = '{txt_DonViQuyDoi.Text}', KhoiLuongToanBo_IsCongThucMacDinh = NULL, HeSoQuyDoiDonVi = '{row.HeSoNew}' {updateKL}\r\n" +
                    $"WHERE Code = '{row.Code}'";
                updates.Add(dbString);


                if (ce_DoiKhoiLuongCongTac.Checked)
                {
                    dbString = $"UPDATE {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} " +
                        $"SET KhoiLuongKeHoach = KhoiLuongKeHoach*'{row.HeSoNew}', KhoiLuongThiCong = KhoiLuongThiCong*'{row.HeSoNew}'\r\n" +
                        $"WHERE CodeCongTacTheoGiaiDoan = '{row.Code}'";

                    updates.Add(dbString);

                }

                if (ce_ThayDoiDinhMuc.Checked)
                {
                    dbString = $"UPDATE {Server.Tbl_TDKH_HaoPhiVatTu} " +
                    $"SET DinhMucNguoiDung = DinhMucNguoiDung/'{nud_HeSoQuyDoi.Value}'\r\n" +
                    $"WHERE CodeCongTac = '{row.Code}'";
                    updates.Add(dbString);

                }
            }

            DataProvider.InstanceTHDA.ExecuteNonQueryFromList(updates);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bt_SuDungDonViBanDau_Click(object sender, EventArgs e)
        {
            var nodes = tl_CongTac.GetAllCheckedNodes();
            if (!nodes.Any())
            {
                MessageShower.ShowWarning("Chưa chọn công tác nào");
                return;
            }
            var dr = MessageShower.ShowYesNoQuestion($"Khôi phục đơn vị gốc!\r\n" +
                ((ce_DoiKhoiLuongCongTac.Checked) ? $"TÍNH LẠI Khối lượng công tác" : "GIỮ NGUYÊN KHỐI LƯỢNG CÔNG TÁC") +
                ((ce_ThayDoiDinhMuc.Checked) ? $"TÍNH LẠI hao phí công tác" : "GIỮ NGUYÊN Hao phí công tác"));


            if (dr == DialogResult.No)
                return;

            var codes = MyFunction.fcn_Array2listQueryCondition(nodes.Select(x => x.GetValue("Code").ToString()));
            if (ce_ThayDoiDinhMuc.Checked)
            {
                string dbString = $"SELECT cttk.Code, cttk.HeSoQuyDoiDonVi\r\n" +
                    $"FROM {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk " +
                    //$"ON hp.CodeCongTac = cttk.Code\r\n" +
                    $"WHERE cttk.Code IN ({codes}) AND cttk.HeSoQuyDoiDonVi IS NOT NULL";

                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                List<string> updates = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    string str = $"UPDATE {Server.Tbl_TDKH_HaoPhiVatTu} SET DinhMucNguoiDung = DinhMucNguoiDung*'{row["HeSoQuyDoiDonVi"]}' WHERE CodeCongTac = '{row["Code"]}'";
                    updates.Add(str);

                    if (ce_DoiKhoiLuongCongTac.Checked)
                    {
                        str = $"UPDATE {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay}\r\n" +
                            $"SET KhoiLuongKeHoach = KhoiLuongKeHoach/'{row["HeSoQuyDoiDonVi"]}',\r\n" +
                            $"KhoiLuongThiCong = KhoiLuongThiCong/'{row["HeSoQuyDoiDonVi"]}'\r\n" +
                            $"WHERE CodeCongTacTheoGiaiDoan = '{row["Code"]}'";


                        updates.Add(str);

                    }
                }
                DataProvider.InstanceTHDA.ExecuteNonQueryFromList(updates);


            }

            if (!ce_DoiKhoiLuongCongTac.Checked)
            {
                string dbString = $"UPDATE {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan}\r\n" +
                     $"SET DonVi = NULL, HeSoQuyDoiDonVi = NULL\r\n" +
                     $"WHERE Code IN ({codes})";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            else
            {
                string dbString = $"UPDATE {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan}\r\n" +
                                     $"SET DonVi = NULL, KhoiLuongToanBo = KhoiLuongToanBo/COALESCE(HeSoQuyDoiDonVi, 1), KhoiLuongToanBo_IsCongThucMacDinh = NULL, HeSoQuyDoiDonVi = NULL\r\n" +
                $"WHERE Code IN ({codes})";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);



            }
            DialogResult = DialogResult.OK;
            

        }

        private void tl_CongTac_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var crNode = e.Node;
            if (crNode == null)
            {
                ctrl_HaoPhiVatTu.Clear();
                return;
            }

            ctrl_HaoPhiVatTu.pushData(Common.Enums.TypeKLHN.CongTac, (string)crNode.GetValue("Code"), (string)crNode.GetValue("TenCongTac"));
        }

        private void ce_DoiKhoiLuongCongTac_CheckedChanged(object sender, EventArgs e)
        {
            LoadCongTac();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkEdit1.Checked)
            {
                tl_CongTac.CheckAll();
            }

            else tl_CongTac.UncheckAll();
        }
    }
}