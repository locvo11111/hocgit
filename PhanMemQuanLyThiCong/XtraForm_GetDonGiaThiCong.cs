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

    public partial class XtraForm_GetDonGiaThiCong : DevExpress.XtraEditors.XtraForm
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

        Worksheet _ws;
        Dictionary<string, string> _dic;
        string _tbl, _colCodeFK;
        CellRange _range;

        public XtraForm_GetDonGiaThiCong()
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

            if (_ws.Name != TDKH.SheetName_KeHoachKinhPhi)
            {
                //_dic = dicKPVL_All;
                _tbl = TDKH.TBL_KHVT_VatTu;
                BandCongTac.Caption = "Vật tư";
                col_MaHieu.Caption = "Mã vật tư";
                col_TenCongTac.Caption = "Vật tư";
            }


            rg_CongTac.SelectedIndexChanged -= rg_CongTac_SelectedIndexChanged;
            rg_TangGiam.SelectedIndexChanged -= rg_TangGiam_SelectedIndexChanged;
            rg_type.SelectedIndexChanged -= rg_type_SelectedIndexChanged;
            rg_source.SelectedIndexChanged -= rg_source_SelectedIndexChanged;
            //rg_source.SelectedIndexChanged -= rg_source_SelectedIndexChanged;
            rg_CongTac.SelectedIndex = 0;
            rg_TangGiam.SelectedIndex = 1;
            rg_type.SelectedIndex = 0;
            rg_source.SelectedIndex = 0;
            //rg_source.SelectedIndex = 0;
            rg_CongTac.SelectedIndexChanged += rg_CongTac_SelectedIndexChanged;
            rg_TangGiam.SelectedIndexChanged += rg_TangGiam_SelectedIndexChanged;
            rg_type.SelectedIndexChanged += rg_type_SelectedIndexChanged;
            rg_source.SelectedIndexChanged += rg_source_SelectedIndexChanged;


            //rg_source.SelectedIndexChanged += rg_source_SelectedIndexChanged;
            LoadCongTac();
        }

        public void LoadCongTac()
        {
            //List<string> cons = new List<string>();
            List<string> lsCongTac = new List<string>();

            //IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            //Worksheet _ws = wb.Worksheets.ActiveWorksheet;
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
                case nameof(LoaiCongTac.All):
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
            List<string> sources = new List<string>();
            string dbString;

            if (_ws.Name == TDKH.SheetName_KeHoachKinhPhi)
            {
                dbString = $"SELECT cttk.Code, cttk.DonGia, cttk.DonGiaThiCong, COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, " +
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
                dbString = $"SELECT *, vt.MaVatLieu AS MaHieuCongTac, vt.VatTu AS TenCongTac " +
                    $"FROM {_tbl} vt " +
                    $"WHERE vt.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCongTac)}) " +
                    $"GROUP BY vt.Code";
            }

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string colDes = rg_type.GetAccessibleName();
            string colSource = rg_source.GetAccessibleName();
            if (colDes == "DonGiaThiCong")
            {
                col_KHMoi.Visible = col_ChenhLechKeHoach.Visible = false;
                col_ThiCongMoi.Visible = col_ChenhLechTC.Visible = true;
                //colSource = "DonGia";
            }
            else
            {
                col_KHMoi.Visible = col_ChenhLechKeHoach.Visible = true;
                col_ThiCongMoi.Visible = col_ChenhLechTC.Visible = false;
                //colSource = "DonGiaThiCong";

            }


            dt.Columns.Add($"{colDes}Moi", typeof(double));
            dt.Columns.Add($"ChenhLech{colDes}", typeof(double), $"[{colDes}Moi] - [{colDes}]");

            int coef = (rg_TangGiam.GetAccessibleName() == "Increase") ? 1 : -1;

            foreach (DataRow dr in dt.Rows)
            {
                double.TryParse(dr[colSource].ToString(), out double DonGiaOld);
                dr[$"{colDes}Moi"] = (double)Math.Round(DonGiaOld*(1 + coef*(int)nud.Value/100.0));
            }

            tl_CongTac.DataSource = dt;
        }

        private void tl_CongTac_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

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

            //string source = rg_TypeDonGia.GetAccessibleName();
            //string text = string.Empty;
            //switch (source)
            //{
            //    case nameof(TypeDonGia.All):
            //        text = "\"ĐƠN GIÁ KẾ HOẠCH\" và \"ĐƠN GIÁ THI CÔNG\"";
            //        break;
            //    case nameof(TypeDonGia.DonGiaKeHoach):
            //        text = "\"ĐƠN GIÁ KẾ HOẠCH\"";
            //        break;
            //    case nameof(TypeDonGia.DonGiaThiCong):
            //        text = "\"ĐƠN GIÁ THI CÔNG\"";
            //        break;
            //    default:
            //        MessageShower.ShowError("Lỗi chọn đơn giá");
            //        tl_CongTac.DataSource = null;
            //        return;
            //}
            string colDes = rg_type.GetAccessibleName();
            string colSource = rg_source.GetAccessibleName();
            string col, colSheet;
            if (colDes == "DonGiaThiCong")
            {
                col = "ĐƠN GIÁ THI CÔNG";
                colSheet = TDKH.COL_DonGiaThiCong;
            } 
            else
            {
                col = "ĐƠN GIÁ KẾ HOẠCH";
                colSheet = TDKH.COL_DonGia;


            }


            if (MessageShower.ShowYesNoQuestion($"{col} sẽ được cập nhật?") == DialogResult.Yes)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[colDes] = dr[$"{colDes}Moi"];
                }     

                WaitFormHelper.ShowWaitForm("Đang cập nhật");
                DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, _tbl);
                var dic = _dic;
                var colummCode = _ws.Columns[dic[TDKH.COL_Code]];
                var columnRowCha = _ws.Columns[dic[TDKH.COL_RowCha]];
////              _ws.Workbook.History.IsEnabled = false;
                _ws.Workbook.BeginUpdate();

                foreach (DataRow dr in dt.Rows)
                {
                    string code = (string)dr["Code"];

                    var inds = colummCode.Search(code, MyConstant.MySearchOptions).Select(x => x.RowIndex);
                    if (inds.Count() > 1)
                    {
                        var dr1 = MessageShower.ShowYesNoQuestion($"Lỗi cập nhật đơn giá {dr["TenCongTac"]}, vẫn tiếp tục cập nhật các công tác còn lại!");
                        if (dr1 == DialogResult.Yes)
                        {
                            continue;
                        }
                        else
                            break;
                            
                    }

                    var ind = inds.Single();

                    

                    var crRow = _ws.Rows[ind];
                    //crRow[dic[TDKH.COL_DonGia]].SetValueFromText(dr["DonGia"].ToString());
                    string dgStr = dr[colDes].ToString();
                    crRow[dic[colSheet]].SetValueFromText(dgStr);

                    if (_ws.Name != TDKH.SheetName_KeHoachKinhPhi)
                    {
                        var indsCon = columnRowCha.Search((ind+1).ToString(), MyConstant.MySearchOptions).Select(x => x.RowIndex).ToArray();
                        List<string> codeCons = new List<string>();
                        foreach (var indCon in indsCon)
                        {
                            Row crRowCon = _ws.Rows[indCon];
                            string codeCon = crRowCon[_dic[TDKH.COL_Code]].Value.ToString();
                            crRowCon[dic[colSheet]].SetValueFromText(dgStr);

                            codeCons.Add(codeCon);
                        }
                        string query = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET {colDes} = '{dgStr}' WHERE Code IN ({MyFunction.fcn_Array2listQueryCondition(codeCons)})";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(query);
                    }
                }


                _ws.Workbook.EndUpdate();
////              _ws.Workbook.History.IsEnabled = false;
                WaitFormHelper.CloseWaitForm();
                Close();
            }
        }

        private void nud_ValueChanged(object sender, EventArgs e)
        {
            LoadCongTac();
        }

        private void rg_TangGiam_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCongTac();

        }

        private void rg_source_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadCongTac();

        }

        private void rg_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCongTac();
        }
    }
}