using System;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Common
{
    public static class MyExtenstions
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
    }
}