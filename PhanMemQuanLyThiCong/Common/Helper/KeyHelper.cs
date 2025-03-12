using log4net;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.API;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Constant;
//using PhanMemQuanLyThiCong.Helpers;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class KeyHelper
    {
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyMemory(Byte[] dest, ref SystemTime src, int size_t);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyMemory1(ref SystemTime dest, Byte[] src, int size_t);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyMemory3(Byte[] dest, ref Int32 src, int size_t);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyMemory4(ref Int32 dest, Byte[] src, int size_t);

        [DllImport("kernel32.dll")]
        public static extern void GetSystemTime(out SystemTime lpSystemTime);

        [DllImport("SDX.dll")]
        private static extern int SDX_Find();

        [DllImport("SDX.dll")]
        private static extern int SDX_Open(int mode, Int32 uid, ref Int32 hid);

        [DllImport("SDX.dll")]
        private static extern int SDX_Read(int handle, int block_index, byte[] buffer512);

        [DllImport("SDX.dll")]
        private static extern int SDX_Write(int handle, int block_index, String buffer512);

        [DllImport("SDX.dll")]
        private static extern void SDX_Close(int handle1);

        private static int ret_SDX = 0;
        private static Int32 hid_SDX = 0;
        private static Int32 uid_SDX = 0;
        private static Int32 handle_SDX = 0;

        //Declare variable
        private static byte[] buffer_SD = new byte[1024];

        private static uint[] tempHID_SD = new uint[16];
        private static ushort handle_SD = 0;
        private static ushort p1_SD = 0;
        private static ushort p2_SD = 0;
        private static ushort p3_SD = 0;
        private static ushort p4_SD = 0;
        private static ushort p5_SD = 20;
        private static uint lp1_SD = 0;
        private static uint lp2_SD = 0;
        private static ulong ret_SD = 1;
        private static SystemTime st_SD;
        public object Root = AppDomain.CurrentDomain.BaseDirectory;
        private static Securedongle.SecuredongleControl SD = new Securedongle.SecuredongleControl();

        private static string _apiUrl;

        private static string _localApiUrl;

        private static int _lenghData_0 = 0;
        private static int _lenghData_1 = 0;
        private static int _lenghData_2 = 0;
        private static int _lenghData_3 = 0;
        private static int _countBlock = 0;

        private string _token;

        private string _tempifo = string.Empty;

        public static readonly ILog Logging = LogManager.GetLogger("QLTCApplication");

        /// <summary>
        ///
        /// </summary>
        protected KeyHelper()
        {
            //log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Get info key store API
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<KeyInfoViewModel>> ActGetInfoKeyStoreAsync(string serialNo)
        {
            KeyInfoViewModel sysUser = new KeyInfoViewModel();
            sysUser.SerialNo = CryptoHelper.Base64Encode(serialNo);
            sysUser.CategoryCode = AppSettings.CategoryCode;

            ResultMessage<KeyInfoViewModel> apiResponse = await UtilAPI<KeyInfoViewModel>.Post(sysUser, _apiUrl + RouteAPI.KEYSTORE_INFO);
            return apiResponse;
        }


        /// <summary>
        /// WriteDataBlockIndex
        /// </summary>
        /// <param name="block"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool WriteDataBlockIndex(int block, string zipData)
        {
            bool isWrite = false;
            try
            {
                byte[] temp = new byte[512];
                int m, sze, indx, tmp;
                Random rand = new Random();

                // Create a 512-byte Full Map with randomized content
                for (m = 0; m < 512; m++)
                {
                    if ((rand.Next(2) % 2) == 0)
                    {
                        tmp = (rand.Next(26) % 26) + 65;
                        temp[m] = (byte)tmp;
                    }
                    else
                    {
                        tmp = (rand.Next(26) % 26) + 97;
                        temp[m] = (byte)tmp;
                    }
                }

                // Calculate where to put the data, and put it there
                for (m = 0; m < zipData.Length; m++)
                {
                    sze = m * (zipData.Length - 1);
                    indx = sze % 512;
                    temp[indx] = (byte)zipData[m];
                }

                String stringData = Encoding.GetEncoding(1252).GetString(temp);

                ret_SDX = SDX_Write(handle_SDX, block, stringData);

                if (ret_SDX < 0)
                {
                    Logging.Error(String.Format("Error Writing data: {0:X}", ret_SDX));
                }
                else isWrite = true;
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return isWrite;
        }

        /// <summary>
        /// ReadDataBlockIndex
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public static bool ReadDataBlockIndex(int block, int lengData, out string dataRead)
        {
            bool isWrite = false;
            dataRead = string.Empty;

            try
            {
                // Read from Block index
                byte[] retbuff = new byte[512];
                ret_SDX = SDX_Read(handle_SDX, block, retbuff);

                string zipData = "";
                int m, sze, indx;
                for (m = 0; m < lengData; m++)
                {
                    sze = m * (lengData - 1);
                    indx = sze % 512;
                    zipData = zipData + (char)retbuff[indx];
                }
                dataRead = zipData;
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return isWrite;
        }




        /// <summary>
        /// Get info Command API
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<List<CommandViewModel>>> ActSysCommandAsync()
        {
            ResultMessage<List<CommandViewModel>> apiResponse = await UtilAPI<List<CommandViewModel>>.Get(_apiUrl + RouteAPI.SYSINFO_COMMAND);
            return apiResponse;
        }
        /// <summary>
        /// Get info TypeAccount
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<List<TypeAccountViewModel>>> ActSysTypeAccountAsync()
        {
            ResultMessage<List<TypeAccountViewModel>> apiResponse = await UtilAPI<List<TypeAccountViewModel>>.Get(_apiUrl + RouteAPI.SYSINFO_TYPEACCOUNT);
            return apiResponse;
        }
        /// <summary>
        /// Get info FunctionType API
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<List<FunctionTypeViewModel>>> ActSysFunctionTypeAsync()
        {
            ResultMessage<List<FunctionTypeViewModel>> apiResponse = await UtilAPI<List<FunctionTypeViewModel>>.Get(_apiUrl + RouteAPI.SYSINFO_FUNCTIONTYPE);
            return apiResponse;
        }

        /// <summary>
        /// Get info Function API
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<List<FunctionViewModel>>> ActSysFunctionAsync()
        {
            ResultMessage<List<FunctionViewModel>> apiResponse = await UtilAPI<List<FunctionViewModel>>.Get(_apiUrl + RouteAPI.SYSINFO_FUNCTION);
            return apiResponse;
        }

        /// <summary>
        /// Get info key store API
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<KeyInfoViewModel>> ActSysKeyStoreAsync(string serialNo)
        {
            RequestDTO sysUser = new RequestDTO();
            sysUser.SerialNo = CryptoHelper.Base64Encode(serialNo);
            sysUser.CategoryCode = AppSettings.CategoryCode;

            ResultMessage<KeyInfoViewModel> apiResponse = await UtilAPI<KeyInfoViewModel>.Post(sysUser, _apiUrl + RouteAPI.SYSINFO_KEYSTORE);
            return apiResponse;
        }
        /// <summary>
        /// Get info appUser
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<List<AppUserViewModel>>> ActSysAppUserAsync(string serialNo)
        {
            RequestDTO sysUser = new RequestDTO();
            sysUser.SerialNo = CryptoHelper.Base64Encode(serialNo);
            sysUser.CategoryCode = AppSettings.CategoryCode;
            ResultMessage<List<AppUserViewModel>> apiResponse = await UtilAPI<List<AppUserViewModel>>.Post(sysUser, _apiUrl + RouteAPI.SYSINFO_APPUSER);
            return apiResponse;
        }
        /// <summary>
        /// Get info Permission API
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<List<PermissionKeyStoreViewModel>>> ActSysPermissionAsync(string serialNo)
        {
            RequestDTO sysUser = new RequestDTO();
            sysUser.SerialNo = CryptoHelper.Base64Encode(serialNo);
            sysUser.CategoryCode = AppSettings.CategoryCode;

            ResultMessage<List<PermissionKeyStoreViewModel>> apiResponse = await UtilAPI<List<PermissionKeyStoreViewModel>>.Post(sysUser, _apiUrl + RouteAPI.SYSINFO_PERMISSION);
            return apiResponse;
        }

        /// <summary>
        /// Get info UserApprove API
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<List<UserInKeyViewModel>>> ActSysUserApproveAsync(string serialNo)
        {
            RequestDTO sysUser = new RequestDTO();
            sysUser.SerialNo = CryptoHelper.Base64Encode(serialNo);
            sysUser.CategoryCode = AppSettings.CategoryCode;

            ResultMessage<List<UserInKeyViewModel>> apiResponse = await UtilAPI<List<UserInKeyViewModel>>.Post(sysUser, _apiUrl + RouteAPI.SYSINFO_USERAPPROVER);
            return apiResponse;
        }


        /// <summary>
        /// ActSysGroupAsync
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<List<AppGroupViewModel>>> ActSysGroupAsync()
        {
            _apiUrl = AppSettings.UrlAPI;
            ResultMessage<List<AppGroupViewModel>> apiResponse = await UtilAPI<List<AppGroupViewModel>>.Get(_localApiUrl + RouteAPI.APPGROUP_GETALL);
            return apiResponse;
        }


        /// <summary>
        /// ActGetallAsync (Lấy dữ liệu từ web xuống)
        /// </summary>
        /// <typeparam name="Tmodel"> Kiểu dữ liệu lầy về</typeparam>
        /// <param name="Url">Url Api</param>
        /// <returns>Kiểu dữ liệu Tmodel</returns>
        public static async Task<ResultMessage<List<Tmodel>>> ActGetallAsync<Tmodel>(string Url)
        {
            _localApiUrl = AppSettings.UrlAPI;

            ResultMessage<List<Tmodel>> apiResponse = await UtilAPI<List<Tmodel>>.Get(_localApiUrl + Url);
            return apiResponse;
        }
        /// <summary>
        /// ActGetByKeyAsync (lấy dữ liệu  1 bản ghi)
        /// </summary>
        /// <typeparam name="Tmodel"></typeparam>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static async Task<ResultMessage<Tmodel>> ActGetByKeyAsync<Tmodel, Tkey>(Tkey key, string Url)
        {
            _localApiUrl = AppSettings.UrlAPI;
            ResultMessage<Tmodel> apiResponse = await UtilAPI<Tmodel>.Post(key, _localApiUrl + Url);
            return apiResponse;
        }
        /// <summary>
        /// ActAddAsync (Thêm mới)
        /// </summary>
        /// <typeparam name="Tmodel"> Kiểu dữ liệu đẩy lên</typeparam>
        /// <param name="model">Model được đẩy đi</param>
        /// <param name="Url">Url Api</param>
        /// <returns></returns>
        public static async Task<ResultMessage<bool>> ActAddAsync<Tmodel>(Tmodel model, string Url)
        {
            _localApiUrl = AppSettings.UrlAPI;
            ResultMessage<bool> apiResponse = await UtilAPI<bool>.Post(model, _localApiUrl + Url);
            return apiResponse;
        }
        /// <summary>
        /// ActUpdateAsync (Cập nhật dữ liệu)
        /// </summary>
        /// <typeparam name="Tmodel"> Kiểu dữ liệu</typeparam>
        /// <param name="model">Model được đẩy đi</param>
        /// <param name="Url">Url Api</param>
        /// <returns></returns>
        public static async Task<ResultMessage<bool>> ActUpdateAsync<Tmodel>(Tmodel model, string Url)
        {
            _localApiUrl = AppSettings.UrlAPI;
            ResultMessage<bool> apiResponse = await UtilAPI<bool>.Put(model, _localApiUrl + Url);
            return apiResponse;
        }
        /// <summary>
        /// ActDeleteAsync (Xóa bản ghi theo key)
        /// </summary>
        /// <param name="key">key truyền vào</param>
        /// <param name="Url">Url Api</param>
        /// <returns></returns>
        public static async Task<ResultMessage<bool>> ActDeleteAsync<Tkey>(Tkey key, string Url)
        {
            _localApiUrl = AppSettings.UrlAPI;
            ResultMessage<bool> apiResponse = await UtilAPI<bool>.Delete<Tkey>(key, _localApiUrl + Url);
            return apiResponse;
        }
        /// <summary>
        /// ActSysGroupAsync
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<bool>> ActDeleteGroupAsync(int id)
        {
            _localApiUrl = AppSettings.UrlAPI;
            ResultMessage<bool> apiResponse = await UtilAPI<bool>.Delete(id, _localApiUrl + RouteAPI.APPGROUP_DELETE);
            return apiResponse;
        }

        /// <summary>
        /// ActUpdateGroupAsync
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<bool>> ActUpdateGroupAsync(AppGroupViewModel model)
        {
            _localApiUrl = AppSettings.UrlAPI;
            ResultMessage<bool> apiResponse = await UtilAPI<bool>.Put(model, _localApiUrl + RouteAPI.APPGROUP_UPDATE);
            return apiResponse;
        }

        /// <summary>
        /// ActDetailGroupAsync
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultMessage<AppGroupViewModel>> ActDetailGroupAsync(int id)
        {
            _localApiUrl = AppSettings.UrlAPI;
            ResultMessage<AppGroupViewModel> apiResponse = await UtilAPI<AppGroupViewModel>.Put(id, _localApiUrl + RouteAPI.APPGROUP_UPDATE);
            return apiResponse;
        }


    }
}
