using DevExpress.XtraSplashScreen;
using DevExpress.XtraSpreadsheet.Model;
using PhanMemQuanLyThiCong.Common.MLogging;
using PhanMemQuanLyThiCong.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class WaitFormHelper
    {
        private static Dictionary<string, TBTStaticWaitForm> dicWaitFormDescriptionCaption =
            new Dictionary<string, TBTStaticWaitForm>();
        public static void ShowWaitForm(string Description = null, string caption = "Vui lòng chờ", Color? cl = null, [CallerMemberName] string memberName = "")
        {
            //Logging.Info($"SHOW \"{Description}\" FROM \"{memberName}\"");
            if (!cl.HasValue)
            {
                cl = Color.Black;
            }



            //SplashScreenManager sp = new DevExpress.XtraSplashScreen.SplashScreenManager(SharedControls.Form, typeof(global::PhanMemQuanLyThiCong.WaitForm1), true, true);
            //SplashScreenManager sp = SplashScreenManager.Default;
            //SplashScreenManager.
            //SplashScreenManager.ShowForm(typeof(WaitForm1));

            //sp.ActiveSplashFormTypeInfo = typeof(WaitForm1);
            //if (sp is null)
            //{
            //    SplashScreenManager sp = new DevExpress.XtraSplashScreen.SplashScreenManager(SharedControls.Form, typeof(global::PhanMemQuanLyThiCong.WaitForm1), true, true);

            //    //return;
            //}
            foreach (var item in dicWaitFormDescriptionCaption.ToArray())
            {

                if (item.Key == memberName)
                    continue;
                try
                {
                    item.Value.SplashScreenManager.CloseWaitForm();
                }
                catch (InvalidOperationException ex) 
                {
                    item.Value.SplashScreenManager.Dispose();
                    dicWaitFormDescriptionCaption.Remove(item.Key);
                    continue; 
                }
            }

            if (Description != null)
            {
                if (!dicWaitFormDescriptionCaption.ContainsKey(memberName))
                {
                    SplashScreenManager sp1 = new DevExpress.XtraSplashScreen.SplashScreenManager(SharedControls.Form, typeof(global::PhanMemQuanLyThiCong.WaitForm1), true, true);

                    dicWaitFormDescriptionCaption.Add(memberName,
                        new TBTStaticWaitForm(sp1, Description, caption));
                }
                else
                {
                    dicWaitFormDescriptionCaption[memberName].Description = Description;
                    dicWaitFormDescriptionCaption[memberName].Caption = caption;

                }


                var sp = dicWaitFormDescriptionCaption[memberName].SplashScreenManager;

                if (!sp.IsSplashFormVisible)
                    sp.ShowWaitForm();
                sp.SetWaitFormCaption(caption);
                sp.SetWaitFormDescription(Description);
                //dicWaitFormDescriptionCaption[memberName].SplashScre.SetWaitFormCaption(caption);
                //SplashScreenManager.Default.SetWaitFormDescription(Description);
            }
            else
            {
                if (dicWaitFormDescriptionCaption.Any())
                {
                    var last = dicWaitFormDescriptionCaption.Last();
                    if (!last.Value.SplashScreenManager.IsSplashFormVisible)
                    {
                        last.Value.SplashScreenManager.ShowWaitForm();

                    }    
                }
            }
        }

        public static void CloseWaitForm([CallerMemberName] string memberName = "")
        {
            if (dicWaitFormDescriptionCaption.ContainsKey(memberName))
            {
                //Logging.Info($"CLOSED \"{dicWaitFormDescriptionCaption[memberName].Key}\" FROM \"{memberName}\"");
                var dt = dicWaitFormDescriptionCaption[memberName];
                if (dt.SplashScreenManager.IsSplashFormVisible)
                    dt.SplashScreenManager.CloseWaitForm();
                dt.SplashScreenManager.Dispose();
                dicWaitFormDescriptionCaption.Remove(memberName);
            }

            SplashScreenManager sp = SplashScreenManager.Default;
            //if (sp is null)
            //    return;

            //if (!dicWaitFormDescriptionCaption.Any())
            //{
            //    if (sp?.IsSplashFormVisible == true)
            //    {
            //        try
            //        {
            //            sp.CloseWaitForm();

            //        }
            //        catch
            //        {
            //            MLogging.Logging.Error("Lỗi đóng waitform");
            //        }
            //    }
            //}
            //else
            //{
            //    var last = dicWaitFormDescriptionCaption.Last();
            //    if (sp?.IsSplashFormVisible == false)
            //        sp.ShowWaitForm();
            //    sp.SetWaitFormCaption(last.Value.Value);
            //    sp.SetWaitFormDescription(last.Value.Key);
            //}
        }

        public static void CloseWaitFormBeforeShowMessage(string memberName)
        {
            
            if (dicWaitFormDescriptionCaption.ContainsKey(memberName))
            {
                //Logging.Info($"CLOSED \"{dicWaitFormDescriptionCaption[memberName].Key}\" FROM \"{memberName}\"");
                var dt = dicWaitFormDescriptionCaption[memberName];
                if (dt.SplashScreenManager.IsSplashFormVisible)
                    dt.SplashScreenManager.CloseWaitForm();
                dt.SplashScreenManager.Dispose();
                dicWaitFormDescriptionCaption.Remove(memberName);
            }

            foreach (var item in dicWaitFormDescriptionCaption)
            {
                try
                {
                    item.Value.SplashScreenManager.CloseWaitForm();
                }
                catch { continue; }
            }


        }

        public static Dictionary<string, TBTStaticWaitForm> GetDic()
        {
            return dicWaitFormDescriptionCaption;
        }

        public static void ForceCloseWaitFormAndClearDic()
        {
            //CloseWaitFormBeforeShowMessage();

            foreach (var item in dicWaitFormDescriptionCaption.Values)
            {
                var sp = item.SplashScreenManager;
                if (sp?.IsSplashFormVisible == true)
                {
                    try
                    {
                        sp.CloseWaitForm();
                    }
                    catch
                    {
                        MLogging.Logging.Error("Lỗi đóng waitform");
                    }
                }
            }
            dicWaitFormDescriptionCaption.Clear();
            foreach(var value in  dicWaitFormDescriptionCaption.Values)
            {
                value.SplashScreenManager.Dispose();

            }   
            //dicWaitFormDescriptionCaption.Clear();
            //SplashScreenManager sp = SplashScreenManager.Default;


            //if (sp?.IsSplashFormVisible == true)
            //{
            //    try
            //    {
            //        sp.CloseWaitForm();

            //    }
            //    catch
            //    {
            //        MLogging.Logging.Error("Lỗi đóng waitform");
            //    }
            //}
        }
    }
}
