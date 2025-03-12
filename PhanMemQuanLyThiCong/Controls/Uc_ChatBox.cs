using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Urcs
{
    //[Obfuscation(ApplyToMembers = false, Exclude = false, Feature = "-rename")]
    public partial class Uc_ChatBox : UserControl
    {
        public Uc_ChatBox()
        {
        ////[Obfuscation(ApplyToMembers = false, Exclude = false, Feature = "-rename")]
            InitializeComponent();
        }

        public void RegisterMessageEvent()
        {
            messagesView1.btn_Reset.PerformClick();
        }
    }
}
