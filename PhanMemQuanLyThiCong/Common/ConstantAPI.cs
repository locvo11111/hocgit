using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Securety;
using System;
using System.Configuration;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;


namespace PhanMemQuanLyThiCong.Common
{
    /// <summary>
    /// Get Parametor from Appsetting file config.
    /// </summary>
    public static class AppSettings
    {
        public static string UrlUpdateChecker
        {
            get
            {
                return "http://103.237.145.75:1111/StaticFiles/";
            }
        }
        /// <summary>
        /// API Url
        /// </summary>
        public static string UrlAPI
        {
            //get { return "http://localhost:5002/api/"; }

            //get { return "http://45.118.146.38:5005/api/"; }
#if DEBUG
            //get { return "http://localhost:5002/api/"; }
            //get { return "http://45.118.146.38:5005/api/"; }
            get { return "http://103.237.145.75:6001/api/"; }

#else
            //get { return "http://45.118.146.38:5005/api/"; }
            get { return "http://103.237.145.75:6001/api/"; }

#endif
            //get { return DataUtil.DecryptString(MSETTING.Default.UriAPI); }
        }
        //public static string UrlChat
        //{
        //    get { return BaseFrom.BanQuyenKeyInfo.UrlAPI; }
        //    //get { return DataUtil.DecryptString(MSETTING.Default.UriChat); }
        //}
        //public static string ApiUrlThoiTiet
        //{
        //    get { return DataUtil.DecryptString(MSETTING.Default.ThoiTietAPI); }
        //}

        //public static string ApiUrlKeyOld
        //{
        //    get { return DataUtil.DecryptString(MSETTING.Default.KeyOldAPI); }
        //}

        /// <summary>
        /// CategoryCode
        /// </summary>
        public static string CategoryCode
        {
            get
            {
                try
                {
                    return DataUtil.DecryptString(MSETTING.Default.CategoryCode);
                }
                catch (Exception ex)
                {
                    return "Mg==";
                }
            }
        }

        /// <summary>
        /// User name
        /// </summary>
        public static string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Password
        /// </summary>
        public static string Password
        {
            get;
            set;
        }

        /// <summary>
        /// SerialNo
        /// </summary>
        public static string SerialNo
        {
            get;
            set;

        }

        public static string PCName { get { return Environment.MachineName; } }

        public static string SerialHDD
        {
            get
            {
                return BaseFrom.SerialNumberHDD;
            }
        }
    }
}
