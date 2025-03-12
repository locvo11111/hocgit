using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Common.Validate
{
    public class CustomValidateEmail : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            string text = control.Text;
            string regexPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            if (Regex.Match(text, regexPattern) == Match.Empty)
            {
                //control.BackColor = Color.Red;
                return false;
            }
            return true;
        }
    }

    public class CustomValidateEmailOrUserName : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            string text = control.Text;
            if (text.Contains(" "))
                return false;
            //string regexPattern = @"^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";
            //if (Regex.Match(text, regexPattern) == Match.Empty)
            //{
            //    //control.BackColor = Color.Red;
            //    return false;
            //}
            return true;
        }
    }

    public class CustomValidatePhoneNumber : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            string text = control.Text;
            string regexPattern = @"\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})";
            if (Regex.Match(text, regexPattern) == Match.Empty)
            {
                ////control.BackColor = Color.Red;
                return false;
            }
            return true;
        }
    }


    public class CustomValidatePassword : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            string text = control.Text;
            string regexPattern = @"(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";//Minimum eight characters, at least one letter and one number and 1 special character
            if (Regex.Match(text, regexPattern) == Match.Empty)
            {
                ////control.BackColor = Color.Red;
                return false;
            }
            return true;
        }
    }

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

    public class CustomValidationDateEditRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            DateEdit edit = control as DateEdit;
            if (edit == null || edit.DateTime == default)
                return false;
            return true;
        }
    }

    public class CustomValidationControlEmptyRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            //TextEdit edit = control as TextEdit;
            if (control == null || string.IsNullOrEmpty(control.Text))
                return false;
            return true;
        }
    }

    public class CustomValidationRichTextBoxRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            RichTextBox edit = control as RichTextBox;
            if (edit == null || string.IsNullOrEmpty(edit.Text))
                return false;
            return true;
        }
    }

    public class CustomValidationSLKERule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            SearchLookUpEdit edit = control as SearchLookUpEdit;
            if (edit == null || edit.EditValue is null)
                return false;
            return true;
        }
    }
}
