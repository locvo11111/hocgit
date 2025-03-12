using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Windows.Forms;

namespace PM360.Common.Validate
{
    public class CustomValidationPasswordRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            TextEdit edit = control as TextEdit;
            if (edit == null || edit.Text.Length < 6)
                return false;         
            return true;
        }
    }
}