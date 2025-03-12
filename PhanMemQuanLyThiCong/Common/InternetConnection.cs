using System.Runtime.InteropServices;

namespace PhanMemQuanLyThiCong.Common
{
    public class InternetConnection
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsConnectedToInternet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
    }
}