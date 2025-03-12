using log4net;
using System;

namespace PhanMemQuanLyThiCong.Common.Logging
{
    public static class Logging
    {
        private static readonly ILog Log = LogManager.GetLogger("ForAllApplication");

        public static void Error(string message, Exception e, string currentMethod, string subFolder = "default")
        {
            if (e != null)
                Log.Error($"{currentMethod}: {message}", e);
            else
                Log.Error($"{currentMethod}: {message}");
        }

        public static void Error(string message, string currentMethod, string subFolder = "default")
        {
            Log.Error($"{currentMethod}: {message}");
        }

        public static void Info(string message, string currentMethod, string subFolder = "default")
        {
            Log.Info($"{currentMethod}: {message}");
        }

        public static void Warn(string message, string currentMethod, string subFolder = "default")
        {
            Log.Warn($"{currentMethod}: {message}");
        }
    }
}