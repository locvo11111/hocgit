using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PM360.Common.Validate
{
    public class CustomValidationTextEditRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            TextEdit edit = control as TextEdit;
            if (edit == null || string.IsNullOrEmpty(edit.Text))
                return false;         
            return true;
        }
    }

    public class CustomValidationTextEditColumnNameExcelRule : CustomValidationTextEditRule
    {
        public override bool Validate(Control control, object value)
        {
            TextEdit edit = control as TextEdit;
            if (Regex.IsMatch(edit.Text, @"[^a-zA-Z]+") || edit.Text.Length > 3)
                return false;    

            if (edit == null || string.IsNullOrEmpty(edit.Text))
                return false;
            return base.Validate(control, value);
        }
    }
}