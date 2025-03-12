using log4net;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PhanMemQuanLyThiCong.Common.MLogging
{
    //[Obfuscation(ApplyToMembers = true, Exclude = false, Feature = "-rename")]

    public static class Logging
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Error(string message, Exception e, [CallerMemberName] string SourceMethod = "", string subFolder = "default")
        {
            if (message.Length > 2048)
                message = message.Substring(0, 2044) + "...";

            if (e != null)
                Log.Error($"{SourceMethod}: {message}", e);
            else
                Log.Error($"{SourceMethod}: {message}");
        }

        public static void Error(string message, [CallerMemberName] string SourceMethod = "", string subFolder = "default")
        {
            if (message.Length > 2048)
                message = message.Substring(0, 2044) + "...";

            Log.Error($"{SourceMethod}: {message}");
        }

        public static void Info(string message, [CallerMemberName] string SourceMethod = "", string subFolder = "default")
        {
            if (message.Length > 2048)
                message = message.Substring(0, 2044) + "...";

            Log.Info($"{SourceMethod}: {message}");
        }

        public static void Warn(string message, [CallerMemberName] string SourceMethod = "", string subFolder = "default")
        {
            if (message.Length > 2048)
                message = message.Substring(0, 2044) + "...";

            Log.Warn($"{SourceMethod}: {message}");
        }
    }
}