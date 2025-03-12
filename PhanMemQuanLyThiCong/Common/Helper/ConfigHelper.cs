using PhanMemQuanLyThiCong.Common.Securety;
using System;
using System.Configuration;
using System.Management;

namespace PhanMemQuanLyThiCong.Common.Helper
{

    public class ConfigHelper
    {
        public static string GetByKey(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        public static int NumberDecimalDigits(string key = "NumberDecimalDigits")
        {
            try
            {
                return int.Parse(GetByKey(key));
            }
            catch (Exception)
            {
                return 5;
            }
        }
        public static bool CheckHideDienDaiTienDo(string key = "HideDienDaiTienDo")
        {
            try
            {
                return bool.Parse(GetByKey(key));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void SaveSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static string GetByKeyEncrypt(string key)
        {
            return DataUtil.DecryptData(ConfigurationManager.AppSettings[key].ToString());
        }

        /// <summary>
        /// GetSerialHDD
        /// </summary>
        /// <returns></returns>
        public static string GetSerialHDD()
        {
            string serialNo = string.Empty;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    try
                    {
                        if (wmi_HD["SerialNumber"] != null)
                            serialNo = wmi_HD["SerialNumber"].ToString();
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }

                }
                searcher.Dispose();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return serialNo;
        }
    }

}