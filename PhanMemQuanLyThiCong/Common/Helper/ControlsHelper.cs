using DevExpress.XtraDataLayout;
using DevExpress.XtraLayout;
using PhanMemQuanLyThiCong.Model.Excel;
using System.Collections.Generic;
using System.Linq;


namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class ControlsHelper
    {
        public static void SetTextForControlByTagName(LayoutControlGroup dataLayout, List<InfoReadExcel> lstValues, string TenGroup)
        {
            var lstControls = dataLayout.Items.OfType<LayoutControlItem>().Where(x => x.Name.Contains(TenGroup))?.ToList();
            if (lstControls.Any())
            {
                foreach (var item in lstValues.Where(x=>x.FieldName.Contains(TenGroup)).ToList())
                {
                    var exitControl = lstControls.Find(x => x.Name.Contains(item.FieldName));
                    if (exitControl != null)
                    {
                        exitControl.Control.Text = item.ExcelColumn;
                    }
                }
            }
        }
    }
}
