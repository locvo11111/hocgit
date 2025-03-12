using DevExpress.XtraEditors;
using DevExpress.XtraWaitForm;
using PhanMemQuanLyThiCong.Common.MLogging;
using PhanMemQuanLyThiCong.IRepositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class MessageShower
    {
        static string _yesString = "", _noString = "", _okString = "";
        public static void ShowError(string message, string caption = "Cảnh báo lỗi", string currentMethod = null, Exception exception = null, [CallerMemberName] string memberName = "")
        {
            Logging.Error(message, exception, currentMethod);

            WaitFormHelper.CloseWaitFormBeforeShowMessage(memberName);
                XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            WaitFormHelper.ShowWaitForm();

        }

        public static void ShowInformation(string message, string caption = "Thông báo", string currentMethod = null, [CallerMemberName] string memberName = "")
        {
            WaitFormHelper.CloseWaitFormBeforeShowMessage(memberName);

            XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            WaitFormHelper.ShowWaitForm();

        }

        public static DialogResult ShowOkCancelInformation(string message, string caption = "Thông báo", string currentMethod = null, [CallerMemberName] string memberName = "")
        {
            //Logging.Logging.Info(message, currentMethod);
            WaitFormHelper.CloseWaitFormBeforeShowMessage(memberName);

            DialogResult dr = XtraMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            WaitFormHelper.ShowWaitForm();

            return dr;// == DialogResult.OK ? true : false;
        }

        public static void ShowWarning(string message, string caption = "Lưu ý", string currentMethod = null, [CallerMemberName] string memberName = "")
        {
            WaitFormHelper.CloseWaitFormBeforeShowMessage(memberName);

            XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            WaitFormHelper.ShowWaitForm();
        }

        public static DialogResult ShowYesNoQuestion(string message, string caption = "Lựa chọn", string currentMethod = null, [CallerMemberName] string memberName = "")
        {
            //Logging.Logging.Info(message, currentMethod);
            WaitFormHelper.CloseWaitFormBeforeShowMessage(memberName);

            DialogResult dr = XtraMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            WaitFormHelper.ShowWaitForm();
            return dr;
        }
        
        public static DialogResult ShowYesNoCancelQuestion(string message, string caption = "Lựa chọn", string currentMethod = null, [CallerMemberName] string memberName = "")
        {
            //Logging.Logging.Info(message, currentMethod);
            WaitFormHelper.CloseWaitFormBeforeShowMessage(memberName);

            DialogResult dr = XtraMessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            WaitFormHelper.ShowWaitForm();
            return dr;
        }

        public static DialogResult ShowYesNoCancelQuestionWithCustomText(string message, string caption, string yesString, string Nostring, bool isDangerous = false, [CallerMemberName] string memberName = "")
        {
            WaitFormHelper.CloseWaitFormBeforeShowMessage(memberName);

            _yesString = yesString;
            _noString = Nostring;
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.Showing += Args_Showing;

            args.Caption = caption;
            args.Text = message;

            if (isDangerous)
            {
                args.Appearance.ForeColor = Color.Red;
                args.Icon = SystemIcons.Warning;
            }

            args.Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel };
            DialogResult dr = XtraMessageBox.Show(args);

            WaitFormHelper.ShowWaitForm();
            return dr;
        }
        public static DialogResult ShowYesOKNoCancelQuestionWithCustomText(string message, string caption, string yesString, string OkString, string Nostring, bool isDangerous = false, [CallerMemberName] string memberName = "")
        {
            WaitFormHelper.CloseWaitFormBeforeShowMessage(memberName);

            _yesString = yesString;
            _noString = Nostring;
            _okString = OkString;
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.Showing += Args4Button_Showing;
            args.Caption = caption;
            args.Text = message;
            if (isDangerous)
            {
                args.Appearance.ForeColor = Color.Red;
                args.Icon = SystemIcons.Warning;
            }
            args.Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.OK, DialogResult.No, DialogResult.Cancel };
            DialogResult dr = XtraMessageBox.Show(args);

            WaitFormHelper.ShowWaitForm();
            return dr;
        }

        private static void Args_Showing(object sender, XtraMessageShowingArgs e)
        {
            e.Buttons[DialogResult.Yes].Text = _yesString;
            e.Buttons[DialogResult.No].Text = _noString;
            e.Buttons[DialogResult.Cancel].Text = "Hủy";

            e.Buttons[DialogResult.Yes].AutoSize 
                = e.Buttons[DialogResult.No].AutoSize 
                = e.Buttons[DialogResult.Cancel].AutoSize = true;

            e.Buttons[DialogResult.Yes].MinimumSize = 
            e.Buttons[DialogResult.No].MinimumSize = 
            e.Buttons[DialogResult.Cancel].MinimumSize = new Size(50, 0);
            
        }

        private static void Args4Button_Showing(object sender, XtraMessageShowingArgs e)
        {

            e.Buttons[DialogResult.Yes].Text = _yesString;
            e.Buttons[DialogResult.No].Text = _noString;
            e.Buttons[DialogResult.OK].Text = _okString;
            e.Buttons[DialogResult.Cancel].Text = "Hủy";

            e.Buttons[DialogResult.Yes].AutoSize
                = e.Buttons[DialogResult.OK].AutoSize
                = e.Buttons[DialogResult.No].AutoSize
                = e.Buttons[DialogResult.Cancel].AutoSize = true;

            e.Buttons[DialogResult.Yes].MinimumSize =
            e.Buttons[DialogResult.OK].MinimumSize =
            e.Buttons[DialogResult.No].MinimumSize =
            e.Buttons[DialogResult.Cancel].MinimumSize = new Size(50, 0);

        }
    }
}
