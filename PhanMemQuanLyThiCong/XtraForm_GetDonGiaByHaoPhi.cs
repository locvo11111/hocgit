using DevExpress.DataAccess.DataFederation;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
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

    public partial class XtraForm_GetDonGiaByHaoPhi : DevExpress.XtraEditors.XtraForm
    {
        public enum LoaiCongTac
        {
            CongTacChuotPhai,
            CongTacDaChon,
            All
        }

        public enum SourceDG
        {
            All,
            NhanCong,
            VatLieu,
            MayThiCong
        }

        public enum TypeDonGia
        {
            All,
            DonGiaKeHoach,
            DonGiaThiCong
        }

        public XtraForm_GetDonGiaByHaoPhi()
        {
            InitializeComponent();

            rg_CongTac.SelectedIndexChanged -= rg_CongTac_SelectedIndexChanged;
            rg_TypeDonGia.SelectedIndexChanged -= rg_TypeDonGia_SelectedIndexChanged;
            rg_CotLayDonGia.SelectedIndexChanged -= rg_CotLayDonGia_SelectedIndexChanged;

            rg_CongTac.SelectedIndex = 0;
            rg_TypeDonGia.SelectedIndex = 0;
            rg_CotLayDonGia.SelectedIndex = 0;
            //rg_source.SelectedIndex = 0;
            rg_CongTac.SelectedIndexChanged += rg_CongTac_SelectedIndexChanged;
            rg_TypeDonGia.SelectedIndexChanged += rg_TypeDonGia_SelectedIndexChanged;
            rg_CotLayDonGia.SelectedIndexChanged += rg_CotLayDonGia_SelectedIndexChanged;
            LoadCongTac();
        }

        public void LoadCongTac()
        {
            //List<string> cons = new List<string>();
            List<string> lsCongTac = new List<string>();

            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            Worksheet ws = wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi];
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            //var range = ws.Range[TDKH.RANGE_KeHoach];
            string acn = rg_CongTac.GetAccessibleName();

            //List công tác
            switch (acn)
            {
                case nameof(LoaiCongTac.CongTacChuotPhai):
                    //Cell cell = ws.SelectedCell[0];
                    Row CrRow = ws.Rows[ws.SelectedCell.TopRowIndex];
                    lsCongTac.Add(CrRow[dic[TDKH.COL_Code]].Value.ToString());
                    break;
                case nameof(LoaiCongTac.CongTacDaChon):
                case nameof(LoaiCongTac.All):
                    CellRange range = ws.Range[TDKH.RANGE_KeHoach];

                    int? indHM = TDKHHelper.FindIndHangMuc(range, dic);

                    if (indHM is null)
                    {
                        MessageShower.ShowWarning("Không tìm thấy hạng mục công tác!");
                        tl_CongTac.DataSource = null;
                        return;
                    }

                    for (int i = indHM.Value + 1; i <= range.BottomRowIndex; i++)
                    {
                        CrRow = ws.Rows[i];
                        string typeRow = CrRow[dic[TDKH.COL_TypeRow]].Value.ToString();

                        if (typeRow == MyConstant.TYPEROW_HangMuc || typeRow == MyConstant.TYPEROW_CongTrinh)
                            break;

                        if (typeRow == MyConstant.TYPEROW_CVCha)
                        {
                            string code = CrRow[dic[TDKH.COL_Code]].Value.ToString();
                            bool isChecked = bool.TryParse(CrRow[0].Value.ToString(), out bool cchecked);
                            if (cchecked || acn == nameof(LoaiCongTac.All))
                            {
                                lsCongTac.Add(code);
                            }
                        }
                    }
                    break;
                default:
                    MessageShower.ShowError("Lỗi chọn loại công tác");
                    tl_CongTac.DataSource = null;
                    return;
            }

            if (!lsCongTac.Any())
            {
                MessageShower.ShowWarning("Không có công tác được chọn");
                tl_CongTac.DataSource = null;
                return;
            }

            //cons.Add($"cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(cons)})");
            //Condition LoaiVatTu
            List<string> sources = new List<string>() { "TBT" };
            List<string> dgdvs = new List<string>();
            if (ce_SourceVL.Checked)
            {
                dgdvs.Add("cttk.DonGiaVatLieuDocVao");
                sources.Add("Vật liệu");
            }
            if (ce_SourceNC.Checked)
            {
                dgdvs.Add("cttk.DonGiaNhanCongDocVao");

                sources.Add("Nhân công");
            }

            if (ce_SourceMTC.Checked)
            {
                dgdvs.Add("cttk.DonGiaMayDocVao");

                sources.Add("Máy thi công");
            }
            string conSource = "", conLimit = "";

            //if (sources.Any())
            //{
                conSource = $"AND LoaiVatTu IN ({MyFunction.fcn_Array2listQueryCondition(sources)})";
            //}    
            //else
            //{
            //    conLimit = "LIMIT 0";
            //}    

                //string condition = string.Join(" AND ", cons);
                string dbString = $"SELECT cttk.Code, cttk.DonGia, cttk.DonGiaThiCong,COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, " +
                $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac, hm.Ten AS TenHangMuc, " +
                $"TOTAL(ROUND(hp.DonGia*hp.HeSo*hp.DinhMuc, 0)) AS DonGiaDinhMucGocMoi, " +
                $"TOTAL(ROUND(hp.DonGiaThiCong*hp.HeSoNguoiDung*hp.DinhMucNguoiDung, 0)) AS DonGiaDinhMucGiaoThauMoi, " +
                ((dgdvs.Any()) ? string.Join(" + ", dgdvs) : "0.0") + " AS DonGiaDocVaoMoi \r\n"+
                $" FROM " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
                $"ON cttk.CodeCongTac = dmct.Code " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm " +
                $"ON COALESCE(cttk.CodeHangMuc, dmct.CodeHangMuc) = hm.Code " +
                $"LEFT JOIN {MyConstant.view_HaoPhiVatTu} hp " +
                $"ON cttk.Code = hp.CodeCongTac AND hp.PhanTichKeHoach = 1 AND IsCha = 0 {conSource} " +
                $"WHERE cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCongTac)})\r\n" +
                $"GROUP BY cttk.Code\r\n" +
            $"ORDER BY cttk.SortId";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["DonGia"] == DBNull.Value)
                    dr["DonGia"] = 0;
                
                if (dr["DonGiaThiCong"] == DBNull.Value)
                    dr["DonGiaThiCong"] = 0;
            }    

            dt.Columns.Add("ChenhLechKeHoach", typeof(long));
            dt.Columns.Add("ChenhLechThiCong", typeof(long));
            dt.Columns.Add("DonGiaKeHoachMoi", typeof(long));
            dt.Columns.Add("DonGiaThiCongMoi", typeof(long));

            string col = rg_CotLayDonGia.GetAccessibleName();

            dt.Columns["DonGiaKeHoachMoi"].Expression = dt.Columns["DonGiaThiCongMoi"].Expression = $"[{col}Moi]";

            //dt.Columns.Add("DonGiaThiCongMoi", typeof(long));

            dt.Columns["ChenhLechKeHoach"].Expression = "[DonGiaKeHoachMoi] - [DonGia]";
            dt.Columns["ChenhLechThiCong"].Expression = "[DonGiaThiCongMoi] - [DonGiaThiCong]";

            tl_CongTac.DataSource = dt;
        }

        private void tl_CongTac_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var crNode = e.Node;
            if (crNode == null)
            {
                ctrl_HaoPhiVatTu.Clear();
                return; 
            }

            ctrl_HaoPhiVatTu.pushData(Common.Enums.TypeKLHN.CongTac, (string)crNode.GetValue("Code"), crNode.GetValue("TenCongTac").ToString());
        }

        private void rg_CongTac_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCongTac();
        }

        private void rg_source_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCongTac();
        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_UpDate_Click(object sender, EventArgs e)
        {
            DataTable dt = tl_CongTac.DataSource as DataTable;
            if (dt is null || dt.Rows.Count == 0)
                this.Close();

            string source = rg_TypeDonGia.GetAccessibleName();
            string text = string.Empty;
            switch (source)
            {
                case nameof(TypeDonGia.All):
                    text = "\"ĐƠN GIÁ KẾ HOẠCH\" và \"ĐƠN GIÁ THI CÔNG\"";
                    break;
                case nameof(TypeDonGia.DonGiaKeHoach):
                    text = "\"ĐƠN GIÁ KẾ HOẠCH\"";
                    break;
                case nameof(TypeDonGia.DonGiaThiCong):
                    text = "\"ĐƠN GIÁ THI CÔNG\"";
                    break;
                default:
                    MessageShower.ShowError("Lỗi chọn đơn giá");
                    tl_CongTac.DataSource = null;
                    return;
            }

            if (MessageShower.ShowYesNoQuestion($"{text} sẽ được cập nhật?") == DialogResult.Yes)
            {


                switch (source)
                {
                    case nameof(TypeDonGia.All):
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["DonGia"] = dr["DonGiaKeHoachMoi"];
                            dr["DonGiaThiCong"] = dr["DonGiaThiCongMoi"];
                        }
                        break;
                    case nameof(TypeDonGia.DonGiaKeHoach):
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["DonGia"] = dr["DonGiaKeHoachMoi"];
                        }
                        break;
                    case nameof(TypeDonGia.DonGiaThiCong):
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["DonGiaThiCong"] = dr["DonGiaThiCongMoi"];
                        }
                        break;
                    default:
                        return;
                }

                WaitFormHelper.ShowWaitForm("Đang cập nhật");
;                DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, TDKH.TBL_ChiTietCongTacTheoKy);
                IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
                Worksheet ws = wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi];
                Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
                var colummCode = ws.Columns[dic[TDKH.COL_Code]];
////              wb.History.IsEnabled = false;
                wb.BeginUpdate();

                foreach (DataRow dr in dt.Rows) 
                {
                    string code = (string)dr["Code"];
                    var ind = colummCode.Search(code, MyConstant.MySearchOptions).Single().RowIndex;
                    var crRow = ws.Rows[ind];
                    crRow[dic[TDKH.COL_DonGia]].SetValueFromText(dr["DonGia"].ToString());
                    crRow[dic[TDKH.COL_DonGiaThiCong]].SetValueFromText(dr["DonGiaThiCong"].ToString());
                }

                wb.EndUpdate();

                try
                {
////              wb.History.IsEnabled = true;

                }
                catch (Exception) { }
                WaitFormHelper.CloseWaitForm();
                Close();
            }
        }

        private void ce_sourceAll_CheckedChanged(object sender, EventArgs e)
        {
            ce_SourceVL.CheckedChanged -= ce_sourceTP_CheckedChanged;
            ce_SourceNC.CheckedChanged -= ce_sourceTP_CheckedChanged;
            ce_SourceMTC.CheckedChanged -= ce_sourceTP_CheckedChanged;
            ce_SourceVL.Checked = ce_SourceNC.Checked = ce_SourceMTC.Checked = ce_sourceAll.Checked;
            ce_SourceVL.CheckedChanged += ce_sourceTP_CheckedChanged;
            ce_SourceNC.CheckedChanged += ce_sourceTP_CheckedChanged;
            ce_SourceMTC.CheckedChanged += ce_sourceTP_CheckedChanged;
            LoadCongTac();
        }

        private void ce_sourceTP_CheckedChanged(object sender, EventArgs e)
        {
            ce_sourceAll.CheckedChanged -= ce_sourceAll_CheckedChanged;
            if (ce_SourceVL.Checked == ce_SourceNC.Checked == ce_SourceMTC.Checked)
            {
                ce_sourceAll.Checked = ce_SourceVL.Checked;
            }
            else
                ce_sourceAll.EditValue = null;
            ce_sourceAll.CheckedChanged += ce_sourceAll_CheckedChanged;
            LoadCongTac();

        }

        private void rg_CotLayDonGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCongTac();

        }

        private void rg_TypeDonGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCongTac();

        }
    }
}