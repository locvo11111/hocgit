using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class ControlHelper
    {

        public static Control FindControl(this Control root, string text)
        {
            if (root == null) throw new ArgumentNullException("root");
            foreach (Control child in root.Controls)
            {
                if (child.Text == text) return child;
                Control found = FindControl(child, text);
                if (found != null) return found;
            }
            return null;
        }

        public static XtraForm CreateFormToShowControl(this XtraUserControl control, string caption, bool isEnableResize = true,
            FormStartPosition startPosition = FormStartPosition.CenterParent, FormWindowState state = FormWindowState.Normal)
        {
            var form = new XtraForm();
            form.Text = caption;
            control.Dock = DockStyle.Fill;
            form.Size = new System.Drawing.Size(control.Size.Width + FormControlConstant.FormWidthContentOffset, control.Size.Height + FormControlConstant.FormHeighContentOffset);
            control.Parent = form;
            form.StartPosition = FormStartPosition.CenterParent;
            if (!isEnableResize)
            {
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.MaximizeBox = false;
            }
            control.Disposed += (sender, args) => { form.BeginInvoke(new Action(() => { form.Close(); })); };
            form.FormClosing += (sender, args) =>
            {
                if (!control.IsDisposed)
                {
                    args.Cancel = true; control.Dispose();

                }
            };
            form.StartPosition = startPosition;
            form.WindowState = state;
            return form;
        }

        public static string GetDescription(this RadioGroup rg)
        {
            try
            {
                return rg.Properties.Items[rg.SelectedIndex].Description;
            }
            catch
            {
                return null;
            }
        }

        public static string GetAccessibleName(this RadioGroup rg)
        {
            try
            {
                return rg.Properties.Items[rg.SelectedIndex].AccessibleName;
            }
            catch
            {
                return null;
            }
        }
    }
    public class FormControlConstant
    {
        public const int FormWidthContentOffset = 2; //Pixel
        public const int FormHeighContentOffset = 32; //Pixel

    }
}
