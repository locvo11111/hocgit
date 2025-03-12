using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Windows.Forms;

namespace PM360.Common.Validate
{
    public class CustomValidationComboboxRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            ComboBoxEdit edit = control as ComboBoxEdit;
            if (edit == null || edit.SelectedIndex == -1)
                return false;
            return true;
        }
    }
}