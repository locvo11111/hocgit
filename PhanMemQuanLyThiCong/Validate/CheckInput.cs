using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Validate
{
    public class CheckInput
    {
        /// <summary>
        /// Kiểm tra và thông báo nếu người dùng bỏ trống trường đó
        /// </summary>
        /// <param name="lb"></param>
        /// <param name="val"></param>
        public static bool CheckTextbox(Label lb, TextEdit textBox)
        {
            if (Obligatory(textBox.Text))
            {
                lb.Visible = true;
                textBox.Focus();
                textBox.TabIndex = 0;
                return false;
            }
            else
            {
                lb.Visible = false;
                return true;
            }
        }
        public static bool Obligatory(string val)
        {
            if(val.Length==0)
            {
                return true;

            }
            return false;
        }
    }
}