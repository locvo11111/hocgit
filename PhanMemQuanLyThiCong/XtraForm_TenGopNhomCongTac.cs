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
    public partial class XtraForm_TenGopNhomCongTac : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_TenGopNhomCongTac()
        {
            InitializeComponent();
            radioGroup1.SelectedIndexChanged -= radioGroup1_SelectedIndexChanged;
            radioGroup1.SelectedIndex = 0;
            radioGroup1.SelectedIndexChanged += radioGroup1_SelectedIndexChanged;

            radioGroup1_SelectedIndexChanged(null, null);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tbl = radioGroup1.GetAccessibleName();
            if (tbl is null)
                treeList1.DataSource = null;
            else
            {
                treeList1.DataSource = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {tbl} WHERE CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'");
          
            }
        }

        private void treeList1_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            var oldVal = treeList1.ActiveEditor.OldEditValue.ToString().Trim();
            var newVal = treeList1.ActiveEditor.OldEditValue.ToString().Trim();

            var dt = treeList1.DataSource as DataTable;
            var dr = treeList1.GetFocusedDataRow();
            if (oldVal != newVal)
            {
                string fieldName = treeList1.FocusedColumn.FieldName;
                if (fieldName == "Ten")
                {
                    if (dt.AsEnumerable().Any(x => x["Ten"].ToString() == newVal))
                    {
                        MessageShower.ShowWarning($"Tên bạn mới nhập đã tồn tại");
                        treeList1.ActiveEditor.EditValue = oldVal;
                        return;
                    }
                    else if (newVal.Contains(" "))
                    {
                        MessageShower.ShowWarning($"Tên gộp không được chứa khoảng trắng");
                        treeList1.ActiveEditor.EditValue = oldVal;
                        return;
                    }

                    var ret = MessageShower.ShowYesNoQuestion($"Bạn có muốn đổi tên gộp \"{oldVal}\" thanh \"{newVal}\" không?");

                    if (ret != DialogResult.Yes)
                        return;
                }

                DataProvider.InstanceTHDA.ExecuteNonQuery($"UPDATE {radioGroup1.GetAccessibleName()} SET {fieldName} = @NewVal WHERE Code = '{dr["Code"]}'", parameter: new string[] { newVal });

                var ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
                var dicDboc = MyFunction.fcn_getDicOfColumn(ws.Range[TDKH.RANGE_DoBocChuan]);

                ws.Workbook.BeginUpdate();
                foreach (var item in ws.Columns[dicDboc[TDKH.COL_TenGop]].Search(oldVal, MyConstant.MySearchOptions))
                {
                    item.SetValue(newVal);
                }
                ws.Workbook.EndUpdate();

            }
        }

        private void treeList1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            var oldVal = treeList1.ActiveEditor.OldEditValue.ToString().Trim();
            var newVal = treeList1.ActiveEditor.EditValue.ToString().Trim();

            var dt = treeList1.DataSource as DataTable;
            var dr = treeList1.GetFocusedDataRow();
            e.Valid = true;
            if (oldVal != newVal)
            {
                string fieldName = treeList1.FocusedColumn.FieldName;
                if (fieldName == "Ten")
                {
                    if (dt.AsEnumerable().Any(x => x["Ten"].ToString() == newVal))
                    {
                        e.Valid = false;
                        e.ErrorText = "Tên bạn mới nhập đã tồn tại";
                        //MessageShower.ShowWarning($"Tên bạn mới nhập đã tồn tại");
                        //treeList1.ActiveEditor.EditValue = oldVal;
                        return;
                    }
                    else if (newVal.Contains(" "))
                    {
                        e.Valid = false;
                        e.ErrorText = "Tên gộp không được chứa khoảng trắng!";
                        //MessageShower.ShowWarning($"Tên gộp không được chứa khoảng trắng");
                        //treeList1.ActiveEditor.EditValue = oldVal;
                        return;
                    }

                    var ret = MessageShower.ShowYesNoQuestion($"Bạn có muốn đổi tên gộp \"{oldVal}\" thanh \"{newVal}\" không?");

                    if (ret != DialogResult.Yes)
                        return;

                    DataProvider.InstanceTHDA.ExecuteNonQuery($"UPDATE {radioGroup1.GetAccessibleName()} SET {fieldName} = @NewVal WHERE Code = '{dr["Code"]}'", parameter: new string[] { newVal });

                    var ws = SharedControls.spsheet_TD_KH_LapKeHoach.ActiveWorksheet;
                    var dicDboc = MyFunction.fcn_getDicOfColumn(ws.Range[TDKH.RANGE_DoBocChuan]);

                    ws.Workbook.BeginUpdate();
                    foreach (var item in ws.Columns[dicDboc[TDKH.COL_TenGop]].Search(oldVal))
                    {
                        item.SetValue(newVal);
                    }
                    ws.Workbook.EndUpdate();
                }
                else
                    DataProvider.InstanceTHDA.ExecuteNonQuery($"UPDATE {radioGroup1.GetAccessibleName()} SET {fieldName} = @NewVal WHERE Code = '{dr["Code"]}'", parameter: new string[] { newVal });

            }
        }
    }
}