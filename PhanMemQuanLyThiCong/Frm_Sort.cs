using Dapper;
using DevExpress.PivotGrid.SliceQueryDataSource;
using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit.Commands;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using StackExchange.Profiling.Internal;
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
    public partial class Frm_Sort : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SenData(DialogResult dialog);

        public SenData senData;

        public Frm_Sort()
        {
            InitializeComponent();
        }



        private void Frm_Sort_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_HuySort_Click(object sender, EventArgs e)
        {
            //var Hms = GetHangMucs();
            //if (!Hms.Any())
            //{
            //    MessageShower.ShowError("Không tìm thấy hạng mục để xắp xếp");
            //    this.Close();
            //    return;
            //}

            //DataTable dt = DuAnHelper.GetCurrentCongTacByHMs(Hms);

            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["SortId"] = dr["OriginalOrder"];
            //}

            //WaitFormHelper.CloseWaitForm();
            //TDKHHelper.LoadCongTacDoBoc();
            //this.Close();
        }

        private List<string> GetHangMucs()
        {
            Worksheet ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
            List<string> Hms = new List<string>();

            CellRange range = ws.Range[TDKH.RANGE_DoBocChuan];
            var dicDb = MyFunction.fcn_getDicOfColumn(range);

            int indCell = ws.SelectedCell.TopRowIndex;
            if (rb_HangMucActive.Checked)
            {
                int indColTypeRow = ws.Range.GetColumnIndexByName(dicDb[TDKH.COL_TypeRow]);

                var rowsInd = ws.Range.FromLTRB(indColTypeRow, range.TopRowIndex, indColTypeRow, indCell)
                            .Search(MyConstant.TYPEROW_HangMuc, MyConstant.MySearchOptions).Select(x => x.RowIndex).ToArray();

                if (!rowsInd.Any())
                {
                    //MessageShower.ShowError("Không tìm thấy hạng mục để xắp xếp");
                    //this.Close();
                    return Hms;
                }
                Hms.Add(ws.Rows[rowsInd.Last()][dicDb[TDKH.COL_Code]].Value.ToString());
            }
            else
            {
                int indColTypeRow = ws.Range.GetColumnIndexByName(dicDb[TDKH.COL_TypeRow]);

                Hms = ws.Range.FromLTRB(indColTypeRow, range.TopRowIndex, indColTypeRow, range.BottomRowIndex)
                            .Search(MyConstant.TYPEROW_HangMuc, MyConstant.MySearchOptions).Select(x => ws.Rows[x.RowIndex][dicDb[TDKH.COL_Code]].Value.ToString()).ToList();

            }
            return Hms;
        }
        private void btn_Apply_Click(object sender, EventArgs e)
        {
            var Hms = GetHangMucs();
            if (!Hms.Any())
            {
                MessageShower.ShowError("Không tìm thấy hạng mục để xắp xếp");
                this.Close();
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang xắp xếp công tác;");




            DataTable dt = DuAnHelper.GetCurrentCongTacByHMsDataTable(Hms);



            int countDtRows = dt.Rows.Count;
            int count = 0;

            if (rb_SortTrinhTuThiCong.Checked)
            {
                string[] lsMaCongTac = dt.AsEnumerable().Select(x => x["MaHieuCongTac"].ToString()).ToArray();
                string dbString = $"SELECT * FROM tbl_DinhMucAll WHERE MaDinhMuc IN ({MyFunction.fcn_Array2listQueryCondition(lsMaCongTac)})";
                
                DataTable dtDm = DataProvider.InstanceTBT.ExecuteQuery(dbString);
                int crTrinhTuThiCong = 0;
                //dt.DefaultView.Sort = "SortId ASC";
                //dt = dt.DefaultView.ToTable();
                foreach (DataRow dr in dt.Rows)
                {
                    string code = (string)dr["Code"];
                    DataRow drDm = dtDm.AsEnumerable().SingleOrDefault(x => (string)x["MaDinhMuc"] == (string)dr["MaHieuCongTac"]);

                    if (drDm is null || !int.TryParse(drDm["TrinhTuThiCong"].ToString(), out int TTTC))
                        dr["SortId"] = crTrinhTuThiCong;
                    else
                    {
                        dr["SortId"] = crTrinhTuThiCong = TTTC;
                    }
                }
                
                dt.DefaultView.Sort = "SortId ASC";
                dt = dt.DefaultView.ToTable();

                int ind = 0;
                List<string> updates = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    string dbstr = $"UPDATE {TDKH.TBL_ChiTietCongTacTheoKy} SET SortId = '{ind++}' WHERE Code = '{dr["Code"]}'";
                    updates.Add(dbstr);
                    //dr["SortId"] = ind++;
                }
                //dt.Columns.Remove("MaHieuCongTac");
                //foreach ()
                //DataProvider.InstanceTHDA.UpdateDataTableFrom(dt, TDKH.TBL_ChiTietCongTacTheoKy);
                if (updates.Any())
                {
                    DataProvider.InstanceTHDA.ExecuteNonQuery(string.Join(";\r\n", updates));
                }
            }

            //var grs = 

            else if (rb_SortThuTu.Checked)
            {
                TDKHHelper.LoadCongTacDoBoc(true, true);
                WaitFormHelper.CloseWaitForm();
                return;
                //Worksheet ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
                //var dic = dic;
                //var range = ws.Range[TDKH.RANGE_DoBocChuan];
                //Column columnCode = ws.Columns[dic[TDKH.COL_Code]];

                //foreach (DataRow dr in dt.Rows)
                //{
                //    var cell = columnCode.Search(dr["Code"].ToString(), MyConstant.MySearchOptions).SingleOrDefault();

                //    string CodePhanTuyen = dr["CodePhanTuyen"].ToString();
                //    string CodeNhom = dr["Nhom"].ToString();

                //    bool isPhanTuyen = CodePhanTuyen.HasValue();
                //    bool isNhom = CodeNhom.HasValue();

                //    if (cell != null)
                //        dr["CustomOrder"] = ws.Rows[cell.RowIndex][dic[TDKH.COL_CustomOrder]].Value.NumericValue;
                //    //else
                //    //    dr["SortId"] = dr["CustomOrder"];
                //}

                ////foreach (string codeHM in Hms)
                ////{
                ////    int hmInd = columnCode.Search(codeHM, MyConstant.MySearchOptions).Single().RowIndex;
                ////    var nextHmInd = SpreadsheetHelper.FindNextGreaterTypeInd(range, hmInd,dic[TDKH.COL_RowCha], dic[TDKH.COL_TypeRow]);

                ////    int  offset = 0;

                ////    for (int i = hmInd; i < nextHmInd; i++)
                ////    {

                ////    }

                ////}

                //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_ChiTietCongTacTheoKy);
            }
            else if (rad_XapXepHM.Checked)
            {
                Worksheet ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
                var dic = MyFunction.fcn_getDicOfColumn(ws.Range[TDKH.RANGE_DoBocChuan]);
                var range = ws.Range[TDKH.RANGE_DoBocChuan];
                Column columnCode = ws.Columns[dic[TDKH.COL_Code]];

                foreach (DataRow dr in dt.Rows)
                {
                    var cell = columnCode.Search(dr["Code"].ToString(), MyConstant.MySearchOptions).SingleOrDefault();


                    if (cell != null)
                    {
                        dr["SortId"] = ws.Rows[cell.RowIndex][dic[TDKH.COL_CustomOrderWholeHM]].Value.NumericValue;
                    }    
                    else
                        continue;
                }

                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_ChiTietCongTacTheoKy);
                WaitFormHelper.CloseWaitForm();

                TDKHHelper.LoadCongTacDoBoc(setSortId: true);
                return;
            }
            else
            {
                WaitFormHelper.CloseWaitForm();
                return;
            }

            WaitFormHelper.CloseWaitForm();
            TDKHHelper.LoadCongTacDoBoc();
            this.Close();

        }

        private void Frm_Sort_KeyDown(object sender, KeyEventArgs e)
        {

        }

    }
}