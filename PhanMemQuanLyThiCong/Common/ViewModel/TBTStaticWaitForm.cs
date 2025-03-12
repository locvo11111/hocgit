using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class TBTStaticWaitForm
    {
        public TBTStaticWaitForm(SplashScreenManager splashScreenManager, string caption, string description)
        {
            SplashScreenManager = splashScreenManager;
            Caption = caption;
            Description = description;
        }
        public SplashScreenManager SplashScreenManager;
        public string Caption;
        public string Description { get; set; }
    }
}
