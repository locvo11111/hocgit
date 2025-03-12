using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Windows.Forms;

namespace PM360.Common.Validate
{
    public class CustomValidationLookupRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            LookUpEdit edit = control as LookUpEdit;
            if (edit == null || edit.EditValue == null)
                return false;
            return true;
        }
    }
}