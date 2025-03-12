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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_LayHaoPhiMacDinh : DevExpress.XtraEditors.XtraForm
    {

        public enum LoaiCongTac
        {
            CongTacChuotPhai,
            CongTacDaChon,
            AllHM,
            All
        }

        Worksheet _ws;
        Dictionary<string, string> _dic;
        string _tbl, _colCodeFK;
        CellRange _range;

        public XtraForm_LayHaoPhiMacDinh()
        {
            InitializeComponent();

            _ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
            _dic = MyFunction.fcn_getDicOfColumn(_ws.GetUsedRange());
            switch (_ws.Name)
            {
                case TDKH.SheetName_KeHoachKinhPhi:
                    //_dic = dic;
                    _tbl = TDKH.TBL_ChiTietCongTacTheoKy;
                    _range = _ws.Range[TDKH.RANGE_KeHoach];
                    break;

                case TDKH.SheetName_VatLieu:
                    _range = _ws.Range[TDKH.RANGE_KeHoachVatTu_VL_TuDong];

                    break;
                case TDKH.SheetName_NhanCong:
                    _range = _ws.Range[TDKH.RANGE_KeHoachVatTu_NC_TuDong];

                    break;
                case TDKH.SheetName_MayThiCong:
                    _range = _ws.Range[TDKH.RANGE_KeHoachVatTu_MTC_TuDong];
                    break;
            }
            rg_CongTac.SelectedIndexChanged -= rg_CongTac_SelectedIndexChanged;
            //rg_source.SelectedIndexChanged -= rg_source_SelectedIndexChanged;
            rg_CongTac.SelectedIndex = 0;
            //rg_source.SelectedIndex = 0;
            rg_CongTac.SelectedIndexChanged += rg_CongTac_SelectedIndexChanged;

            rg_CongTac_SelectedIndexChanged(null, null);
        }

        private void bt_UpDate_Click(object sender, EventArgs e)
        {
            DataTable dt = tl_CongTac.DataSource as DataTable;
            
            if (dt is null || dt.Rows.Count == 0)
            {
                MessageShower.ShowInformation("Không có công tác nào được cập nhật!");
                DialogResult = DialogResult.None;
                this.Close();
            }

            if (MessageShower.ShowYesNoQuestion($"{dt.Rows.Count} Công tác sẽ được cập nhật lại hao phí mặc định?") != DialogResult.Yes)
            {
                DialogResult = DialogResult.None;
                Close();
            }
            WaitFormHelper.ShowWaitForm("Đang thêm vật tư mặc định");
            foreach (DataRow dr in dt.Rows)
            {
                MyFunction.fcn_TDKH_ThemDinhMucMacDinhChoCongTac(Common.Enums.TypeKLHN.CongTac, dr["Code"].ToString(), false);
            }
            WaitFormHelper.CloseWaitForm();

            TDKHHelper.CapNhatAllVatTuHaoPhi(CodesHangMuc: dt.AsEnumerable().Select(x => x.Field<string>("CodeHangMuc")));
            DialogResult = DialogResult.OK;
        }

        private void rg_CongTac_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<string> cons = new List<string>();
            List<string> lsCongTac = new List<string>();

            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            Worksheet _ws = wb.Worksheets.ActiveWorksheet;
            //if (_dic == null)
            //{
            //    //IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            //    //Worksheet _ws = wb.Worksheets.ActiveWorksheet;
            //    Dictionary<string, string> _dic = MyFunction.fcn_getDicOfColumn(_ws.GetUsedRange());

            //}
            string acn = rg_CongTac.GetAccessibleName();


            //List công tác
            switch (acn)
            {
                case nameof(LoaiCongTac.CongTacChuotPhai):
                    //Cell cell = ws.SelectedCell[0];
                    Row CrRow = _ws.Rows[_ws.SelectedCell.TopRowIndex];
                    lsCongTac.Add(CrRow[_dic[TDKH.COL_Code]].Value.ToString());
                    break;
                case nameof(LoaiCongTac.CongTacDaChon):
                case nameof(LoaiCongTac.AllHM):
                    int? indHM = TDKHHelper.FindIndHangMuc(_range, _dic);

                    if (indHM is null)
                    {
                        MessageShower.ShowWarning("Không tìm thấy hạng mục công tác!");
                        tl_CongTac.DataSource = null;
                        return;
                    }

                    for (int i = indHM.Value + 1; i <= _range.BottomRowIndex; i++)
                    {
                        CrRow = _ws.Rows[i];
                        string typeRow = CrRow[_dic[TDKH.COL_TypeRow]].Value.ToString();

                        if (typeRow == MyConstant.TYPEROW_HangMuc || typeRow == MyConstant.TYPEROW_CongTrinh)
                            break;

                        if (typeRow == MyConstant.TYPEROW_CVCha)
                        {
                            string code = CrRow[_dic[TDKH.COL_Code]].Value.ToString();
                            bool.TryParse(CrRow[0].Value.ToString(), out bool cchecked);
                            if (cchecked || acn == nameof(LoaiCongTac.All))
                            {
                                lsCongTac.Add(code);
                            }
                        }
                    }
                    break;
                case nameof(LoaiCongTac.All):


                    for (int i = _range.TopRowIndex + 1; i <= _range.BottomRowIndex; i++)
                    {
                        CrRow = _ws.Rows[i];
                        string typeRow = CrRow[_dic[TDKH.COL_TypeRow]].Value.ToString();

                        //if (typeRow == MyConstant.TYPEROW_HangMuc || typeRow == MyConstant.TYPEROW_CongTrinh)
                        //    break;

                        if (typeRow == MyConstant.TYPEROW_CVCha)
                        {
                            string code = CrRow[_dic[TDKH.COL_Code]].Value.ToString();
                            bool.TryParse(CrRow[0].Value.ToString(), out bool cchecked);
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

            List<string> sources = new List<string>();
            string dbString;

            if (_ws.Name == TDKH.SheetName_KeHoachKinhPhi)
            {
                dbString = $"SELECT cttk.Code, cttk.DonGia, cttk.DonGiaThiCong, COALESCE(cttk.CodeHangMuc, dmct.CodeHangMuc) AS CodeHangMuc,COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, " +
                $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac, cttk1.DonGia AS DonGiaGiaoThau " +
                $" FROM " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
                $"ON cttk.CodeCongTac = dmct.Code " +
                $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk1\r\n" +
                $"ON dmct.Code = cttk1.CodeCongTac AND cttk1.CodeNhaThau IS NOT NULL\r\n" +
                //$"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm " +
                //$"ON dmct.CodeHangMuc = hm.Code " +
                $"WHERE cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCongTac)}) " +
                $"GROUP BY cttk.Code";
            }
            else
            {
                dbString = $"SELECT *, dmct.CodeHangMuc,vt.MaVatLieu AS MaHieuCongTac, vt.VatTu AS TenCongTac " +
                    $"FROM {_tbl} vt " +
                    $"WHERE vt.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCongTac)}) " +
                    $"GROUP BY vt.Code";
            }

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            tl_CongTac.DataSource = dt;

        }
    }
}