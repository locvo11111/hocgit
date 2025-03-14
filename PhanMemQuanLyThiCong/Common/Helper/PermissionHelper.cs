﻿using DevExpress.XtraEditors;
using log4net;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.API;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Controls;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
//using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class PermissionHelper
    {
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

        private static int _lenghData_0 = 0;
        private static int _lenghData_1 = 0;
        private static int _lenghData_2 = 0;
        private static int _lenghData_3 = 0;
        private static int _countBlock = 0;

        public object Root = AppDomain.CurrentDomain.BaseDirectory;

        public static readonly ILog Logging = LogManager.GetLogger("PM360Application");
        //private static string _apiUrl;

        /// <summary>
        ///
        /// </summary>
        protected PermissionHelper()
        {
            //log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Kiểm tra có key mềm đăng ký tồn tại trên hệ thống hay chưa
        /// </summary>
        /// <returns></returns>
        public static bool CheckExitsKeyWindow()
        {
            bool isExits = false;
            try
            {
                List<string> lstKeyMems = new List<string>();
                List<string> lstDriveLetters = new List<string>();
                foreach (DriveInfo di in DriveInfo.GetDrives())
                {
                    lstDriveLetters.Add(di.Name);
                }
                foreach (var item in lstDriveLetters)
                {
                    string path = $"{item}Windows\\{MyConstant.KeyWin}\\key.txt";
                    if (File.Exists(path))
                    {
                        StreamReader sr = new StreamReader(path);
                        string key_code = sr.ReadToEnd();
                        if (!string.IsNullOrEmpty(key_code))
                            lstKeyMems.Add(key_code);
                        sr.Close();
                        break;
                    }
                }
                if (lstKeyMems.Any()) isExits = true;
                return isExits;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// SetKeyToWindow
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool SetKeyToWindow(string key)
        {
            try
            {
                List<string> lstDriveLetters = new List<string>();
                foreach (DriveInfo di in DriveInfo.GetDrives())
                {
                    lstDriveLetters.Add(di.Name);
                }
                foreach (var item in lstDriveLetters)
                {
                    DirectoryInfo di;
                    string folder = $"{item}Windows";
                    string path = $"{item}\\Windows\\{MyConstant.KeyWin}\\key.txt";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    if (!File.Exists(path))
                    {
                        if (!Directory.Exists(folder))
                        {
                            try
                            {
                                di = Directory.CreateDirectory(folder);
                                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        folder += $"\\{MyConstant.KeyWin}";
                        if (!Directory.Exists(folder))
                        {
                            try
                            {
                                di = Directory.CreateDirectory(folder);
                                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        string pathFile = $"{folder}\\key.txt";
                        if (!File.Exists(pathFile))
                        {
                            try
                            {
                                using (StreamWriter sw = File.CreateText(pathFile))
                                {
                                    sw.WriteLine(key);
                                    sw.Close();
                                }
                                FileInfo f = new FileInfo(pathFile);
                                f.Attributes = FileAttributes.Hidden;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region kết nối với hệ thống  mới

        public static async Task<ResultMessage<KeyInfoViewModel>> CheckKeyInfoAsync()
        {
            List<string> lstKeys = new List<string>();
            List<string> lstKeyMems = new List<string>();



            bool isTypeKhoaCung = false;
            ResultMessage<KeyInfoViewModel> apiResponse = new ResultMessage<KeyInfoViewModel>();
            try
            {
//#if DEBUG
//                #region giả lập
//                lstKeys.Add("-2146811743");
//                goto BEGINCheck;
//                #endregion
//#endif
                Logging.Info("Start Check");
                try
                {
                    SDX_Find();
                }
                catch (Exception)
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"SDX.dll"))
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"SDX.dll");
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\x86\SDX.dll", AppDomain.CurrentDomain.BaseDirectory + @"SDX.dll");
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"SDX.lib"))
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"SDX.lib");
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\x86\SDX.lib", AppDomain.CurrentDomain.BaseDirectory + @"SDX.lib");
                }
                //Find SDX
                int plugCount = SDX_Find();
                Logging.Info($"Count plug: {plugCount}");
                for (int i = 1; i <= plugCount; i++)
                {
//#if DEBUG
//                    #region Giả Lập
//                    lstKeys.Add("711366252");
//                    goto BEGINCheck;
//                    #endregion
//#endif
                    uid_SDX = 1059366484;
                    ret_SDX = SDX_Open(i, uid_SDX, ref hid_SDX);
                    if ((uint)ret_SDX > 0U)
                    {
                        uid_SDX = 1399454627;
                        ret_SDX = SDX_Open(i, uid_SDX, ref hid_SDX);
                        if (hid_SDX == 0) continue;
                        lstKeys.Add(hid_SDX.ToString());
                    }
                }
                if (lstKeys.Any()) isTypeKhoaCung = true;
                Logging.Info($"Plug: {lstKeys.Count}");
                //Find khóa mềm
                if (!isTypeKhoaCung)
                {
                    List<string> lstDriveLetters = new List<string>();
                    foreach (DriveInfo di in DriveInfo.GetDrives())
                    {
                        lstDriveLetters.Add(di.Name);
                    }
                    foreach (var item in lstDriveLetters)
                    {
                        string path = $"{item}Windows\\{MyConstant.KeyWin}\\key.txt";
                        if (File.Exists(path))
                        {
                            StreamReader sr = new StreamReader(path);
                            string key_code = sr.ReadLine();
                            lstKeyMems.Add(key_code);
                            sr.Close();
                            break;
                        }
                    }
                }

                // Check internet connected
                BEGINCheck:

                var crSerialNo = BaseFrom.BanQuyenKeyInfo.SerialNo;

                if (BaseFrom.IsFullAccess && (lstKeys.Contains(crSerialNo) || lstKeyMems.Contains(crSerialNo)))
                {
                    return new ResultMessage<KeyInfoViewModel>()
                    {
                        MESSAGE_TYPECODE = true,
                        STATUS_CODE = 360
                    };
                }

                //if (BaseFrom.IsFullAccess && BaseFrom.BanQuyenKeyInfo.TypeCode == TypeStatus.KHOACUNG && lstKeys.Contains(crSerialNo))
                //{
                //    return new ResultMessage<KeyInfoViewModel>()
                //    {
                //        MESSAGE_TYPECODE = true,
                //        STATUS_CODE = 360
                //    };
                //}

                bool _isConnectedToInternet = InternetConnection.IsConnectedToInternet();
                if (_isConnectedToInternet)
                {
                    //_apiUrl = ConfigHelper.GetByKeyEncrypt("UriAPI");
                    foreach (var key in lstKeys)
                    {

                        apiResponse = await GetInfoKeyStoreAsync(key);
                        if (apiResponse.MESSAGE_TYPECODE)
                        {
                            // Xu ly keyinfo //
                            SetInfoKey(apiResponse.Dto);

                            return apiResponse;
                        }
                    }

                    //if (BaseFrom.IsFullAccess && BaseFrom.BanQuyenKeyInfo.TypeCode == TypeStatus.KHOAMEM && lstKeyMems.Contains(crSerialNo))
                    //{
                    //    return new ResultMessage<KeyInfoViewModel>()
                    //    {
                    //        MESSAGE_TYPECODE = true,
                    //        STATUS_CODE = 360
                    //    };
                    //}


                    foreach (var key in lstKeyMems)
                    {
                        apiResponse = await GetInfoKeyStoreAsync(key);
                        if (apiResponse.MESSAGE_TYPECODE)
                        {
                            SetInfoKey(apiResponse.Dto);
                            return apiResponse;
                        }
                    }
                }
                else
                {
                    if (!isTypeKhoaCung) return apiResponse;
                    else
                    {
                        //Đọc thông tin khóa khi mất mạng //
                        var keyInfo = ReadDataKey(out isTypeKhoaCung);
                        if (keyInfo != null)
                            SetInfoKey(keyInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
                apiResponse.MESSAGE_TYPECODE = false;
                apiResponse.MESSAGE_CONTENT = "Lỗi không tìm thấy thông tin khóa phần mềm";
            }
            return apiResponse;
        }

        public static async Task<ResultMessage<KeyInfoViewModel>> GetInfoKeyStoreAsync(string serialNo)
        {
            ResultMessage<KeyInfoViewModel> resultMessage = new ResultMessage<KeyInfoViewModel>();
            KeyRequestInfoViewModel sysUser = new KeyRequestInfoViewModel();
            sysUser.SerialNo = CryptoHelper.Base64Encode(serialNo);
            sysUser.CategoryCode = CryptoHelper.Base64Encode(AppSettings.CategoryCode);
            sysUser.UserName = AppSettings.UserName;
            sysUser.PCName = AppSettings.PCName;
            sysUser.SerialHDD = AppSettings.SerialHDD;
            Logging.Info(CryptoHelper.Base64Encode(serialNo));
            var apiResponse = await CusHttpClient.InstanceTBT.MPostAsJsonAsync<KeyInfoViewModel>(RouteAPI.KEYSTORE_INFO, sysUser);
            if (apiResponse.MESSAGE_TYPECODE)
            {
                resultMessage.MESSAGE_TYPECODE = apiResponse.MESSAGE_TYPECODE;
                apiResponse.Dto.CategoryCode = AppSettings.CategoryCode;
                resultMessage.Dto = apiResponse.Dto;

            }
            return resultMessage;
        }

        public static ResultMessage<KeyInfoViewModel> GetInfo(string serialNo)
        {
            ResultMessage<KeyInfoViewModel> resultMessage = new ResultMessage<KeyInfoViewModel>();

            KeyRequestInfoViewModel sysUser = new KeyRequestInfoViewModel();
            sysUser.SerialNo = serialNo;
            sysUser.CategoryCode = AppSettings.CategoryCode;
            sysUser.UserName = AppSettings.UserName;
            sysUser.PCName = AppSettings.PCName;
            sysUser.SerialHDD = AppSettings.SerialHDD;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(RouteAPI.KEYSTORE_INFO);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //  write your json content here
                    string json = JsonConvert.SerializeObject(sysUser);
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<ResultMessage<KeyInfoViewModel>>(result);
                }
            }
            catch (Exception)
            {
                return resultMessage;
            }
        }

        public static void SetInfoKey(KeyInfoViewModel keyInfo)
        {
            var _banQuyenKeyInfo = new BanQuyenKeyInfo();
            if (keyInfo != null)
            {
                _banQuyenKeyInfo.Address = keyInfo.Address;
                _banQuyenKeyInfo.ServerName = keyInfo.ServerName;
                _banQuyenKeyInfo.ServerIP = keyInfo.ServerIP;
                _banQuyenKeyInfo.CategoryCode = AppSettings.CategoryCode;
                _banQuyenKeyInfo.Company = keyInfo.Company;
                _banQuyenKeyInfo.DatabaseName = keyInfo.DatabaseName;
                _banQuyenKeyInfo.DateNow = keyInfo.DateNow;
                _banQuyenKeyInfo.Department = keyInfo.Department;
                _banQuyenKeyInfo.Email = keyInfo.Email;
                _banQuyenKeyInfo.EndDate = keyInfo.EndDate;
                _banQuyenKeyInfo.StartDate = keyInfo.StartDate;
                _banQuyenKeyInfo.FullName = keyInfo.FullName;
                _banQuyenKeyInfo.Name = keyInfo.Name;
                _banQuyenKeyInfo.Gender = keyInfo.Gender;
                _banQuyenKeyInfo.UrlAPI = keyInfo.UrlAPI;
                _banQuyenKeyInfo.IsDateLimit = keyInfo.IsDateLimit;
                _banQuyenKeyInfo.PhoneNumber = keyInfo.PhoneNumber;
                _banQuyenKeyInfo.StartSeverDate = keyInfo.StartSeverDate;
                _banQuyenKeyInfo.EndSeverDate = keyInfo.EndSeverDate;
                _banQuyenKeyInfo.UserName = keyInfo.UserName;
                _banQuyenKeyInfo.PassWord = keyInfo.PassWord;
                _banQuyenKeyInfo.TypeCode = keyInfo.TypeCode;
                _banQuyenKeyInfo.Status = keyInfo.Status;
                _banQuyenKeyInfo.KeyCode = keyInfo.KeyCode;
                _banQuyenKeyInfo.UserId = keyInfo.UserId;
                _banQuyenKeyInfo.SerialNo = keyInfo.SerialNo;
                _banQuyenKeyInfo.LimitUser = keyInfo.LimitUser;
                _banQuyenKeyInfo.LimitUserExternal = keyInfo.LimitUserExternal;
                _banQuyenKeyInfo.SubscriptionTypeId = keyInfo.SubscriptionTypeId;

                CusHttpClient.InstanceCustomer.BaseAddress = keyInfo.UrlAPI;

                if (_banQuyenKeyInfo.TypeCode == TypeStatus.KHOAMEM)
                {
                    if (keyInfo.IsDateLimit)
                    {
                        if (keyInfo.EndDate.HasValue)
                        {
                            _banQuyenKeyInfo.LimitDate = 0;
                            if (keyInfo.EndDate.Value.Date.CompareTo(keyInfo.DateNow.Date) > 0)
                                _banQuyenKeyInfo.LimitDate = (int)(keyInfo.EndDate.Value.Date - keyInfo.DateNow.Date).TotalDays;
                        }
                        else
                            _banQuyenKeyInfo.LimitDate = 0;
                    }
                }
            }
            BaseFrom.BanQuyenKeyInfo = _banQuyenKeyInfo;

            MSETTING.Default.TokenTBT = keyInfo.Token;
            MSETTING.Default.Save();
            
        }

        public static async Task<bool> ValidateCustomerServer()
        {
            var keyInfo = BaseFrom.BanQuyenKeyInfo;

            #region checkTBTToken
            var response = await CusHttpClient.InstanceTBT.MGetAsync<AppUserViewModel>(RouteAPI.USER_VALIDATETOKEN);
            if (response.MESSAGE_TYPECODE)
            {
                Debug.WriteLine("Xác thực hệ thống TBT thành công");
            }
            else
            {
                var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<LoginResponse>(RouteAPI.USER_LOGIN,
                
                new LoginRequest()
                {
                    UserName = keyInfo.Email,
                    Password = keyInfo.PhoneNumber,
                    RememberMe = true
                });

                if (!res.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowInformation("Không thể xác thực tài khoản người dùng với server chủ!");
                    return false;
                }
            }
            #endregion;

            CusHttpClient.InstanceCustomer.BaseAddress = keyInfo.UrlAPI;
            
            return true;
        }

        public static bool WriteDataKey(KeyInfoViewModel info)
        {
            bool isWrite = false;
            try
            {
                int plugCount = SDX_Find();
                for (int i = 1; i <= plugCount; i++)
                {
                    uid_SDX = 1059366484;
                    ret_SDX = SDX_Open(i, uid_SDX, ref hid_SDX);
                    if ((uint)ret_SDX > 0U)
                    {
                        uid_SDX = 1399454627;
                        ret_SDX = SDX_Open(i, uid_SDX, ref hid_SDX);
                        if (hid_SDX == 0) continue;
                        int ret = ret_SDX;
                        string dataWrite = JsonConvert.SerializeObject(info);
                        string zipData = CryptoHelper.CompressString(dataWrite);
                        if (zipData.Length > 512 * 4)
                        {
                            Logging.Error($"Data vượt quá {512 * 4} byte");
                            return false;
                        }

                        //_lenghData = zipData.Length;
                        _countBlock = (int)Math.Ceiling((double)zipData.Length / 512);
                        var lstDatas = CryptoHelper.Split(zipData, 512);
                        for (int m = 0; m < _countBlock; m++)
                        {
                            handle_SDX = ret;
                            switch (m)
                            {
                                case 1:
                                    _lenghData_1 = lstDatas.ElementAt(m).Length;
                                    break;

                                case 2:
                                    _lenghData_2 = lstDatas.ElementAt(m).Length;
                                    break;

                                case 3:
                                    _lenghData_3 = lstDatas.ElementAt(m).Length;
                                    break;

                                default:
                                    _lenghData_0 = lstDatas.ElementAt(m).Length;
                                    break;
                            }
                            WriteDataBlockIndex(m, lstDatas.ElementAt(m));
                            isWrite = true;
                        }
                        SDX_Close(handle_SDX);
                        SaveSetting();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
                isWrite = false;
            }
            return isWrite;
        }/// <summary>

         /// ReadDataKey
         /// </summary>
         /// <param name="isSuccess"></param>
         /// <returns></returns>
        public static KeyInfoViewModel ReadDataKey(out bool isSuccess)
        {
            isSuccess = false;
            KeyInfoViewModel keyInfo = new KeyInfoViewModel();
            ReadSetting();
            //if (_lenghData == 0) return keyInfo;
            try
            {
                int plugCount = SDX_Find();
                for (int i = 1; i <= plugCount; i++)
                {
                    uid_SDX = 1059366484;
                    ret_SDX = SDX_Open(i, uid_SDX, ref hid_SDX);
                    if ((uint)ret_SDX > 0U)
                    {
                        uid_SDX = 1399454627;
                        ret_SDX = SDX_Open(i, uid_SDX, ref hid_SDX);
                        if (hid_SDX == 0) continue;
                        int ret = ret_SDX;
                        string dataRead = string.Empty;
                        string dataBlockIndex = string.Empty;
                        for (int m = 0; m < _countBlock; m++)
                        {
                            handle_SDX = ret;
                            switch (m)
                            {
                                case 0:
                                    ReadDataBlockIndex(m, _lenghData_0, out dataBlockIndex);
                                    dataRead += dataBlockIndex;
                                    break;

                                case 1:
                                    ReadDataBlockIndex(m, _lenghData_1, out dataBlockIndex);
                                    dataRead += dataBlockIndex;
                                    break;

                                case 2:
                                    ReadDataBlockIndex(m, _lenghData_2, out dataBlockIndex);
                                    dataRead += dataBlockIndex;
                                    break;

                                case 3:
                                    ReadDataBlockIndex(m, _lenghData_3, out dataBlockIndex);
                                    dataRead += dataBlockIndex;
                                    break;
                            }
                        }
                        string unzipData = CryptoHelper.DecompressString(dataRead);
                        keyInfo = JsonConvert.DeserializeObject<KeyInfoViewModel>(unzipData);
                        isSuccess = true;
                        SDX_Close(handle_SDX);
                    }
                }
            }
            catch (Exception ex)
            {
                keyInfo = new KeyInfoViewModel();
                Logging.Error(ex.Message, ex);
            }
            return keyInfo;
        }

        /// <summary>
        /// The SaveSetting
        /// </summary>
        private static void SaveSetting()
        {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            MSETTING.Default.TempLengh_0 = CryptoHelper.Base64Encode(_lenghData_0.ToString());
            MSETTING.Default.TempLengh_1 = CryptoHelper.Base64Encode(_lenghData_1.ToString());
            MSETTING.Default.TempLengh_2 = CryptoHelper.Base64Encode(_lenghData_2.ToString());
            MSETTING.Default.TempLengh_3 = CryptoHelper.Base64Encode(_lenghData_3.ToString());
            MSETTING.Default.TempCountBlock = CryptoHelper.Base64Encode(_countBlock.ToString());
            MSETTING.Default.Save();
        }

        /// <summary>
        /// The SaveSetting
        /// </summary>
        private static void ReadSetting()
        {
            string text = MSETTING.Default.TempCountBlock;
            _countBlock = int.Parse(CryptoHelper.DecodeFrom64(text));
            string textData = MSETTING.Default.TempLengh_0;
            if (!string.IsNullOrEmpty(textData))
            {
                try
                {
                    _lenghData_0 = int.Parse(CryptoHelper.DecodeFrom64(textData));
                }
                catch (Exception ex)
                {
                    Logging.Error(ex.Message, ex);
                    _lenghData_0 = 0;
                }
            }
            textData = MSETTING.Default.TempLengh_1;
            if (!string.IsNullOrEmpty(textData))
            {
                try
                {
                    _lenghData_1 = int.Parse(CryptoHelper.DecodeFrom64(textData));
                }
                catch (Exception ex)
                {
                    Logging.Error(ex.Message, ex);
                    _lenghData_1 = 0;
                }
            }
            textData = MSETTING.Default.TempLengh_2;
            if (!string.IsNullOrEmpty(textData))
            {
                try
                {
                    _lenghData_2 = int.Parse(CryptoHelper.DecodeFrom64(textData));
                }
                catch (Exception ex)
                {
                    Logging.Error(ex.Message, ex);
                    _lenghData_2 = 0;
                }
            }
            textData = MSETTING.Default.TempLengh_3;
            if (!string.IsNullOrEmpty(textData))
            {
                try
                {
                    _lenghData_3 = int.Parse(CryptoHelper.DecodeFrom64(textData));
                }
                catch (Exception ex)
                {
                    Logging.Error(ex.Message, ex);
                    _lenghData_3 = 0;
                }
            }
        }/// <summary>

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

        public static async Task<ResultMessage<bool>> RemoveData()
        {
            ResultMessage<bool> apiResponse = new ResultMessage<bool>();
            if (BaseFrom.IsFullAccess && BaseFrom.BanQuyenKeyInfo != null)
            {
                KeyRequestInfoViewModel sysUser = new KeyRequestInfoViewModel();
                sysUser.SerialNo = CryptoHelper.Base64Encode(BaseFrom.BanQuyenKeyInfo.SerialNo);
                sysUser.CategoryCode = AppSettings.CategoryCode;
                sysUser.UserName = AppSettings.UserName;
                sysUser.PCName = AppSettings.PCName;
                sysUser.SerialHDD = AppSettings.SerialHDD;
                apiResponse = await UtilAPI<bool>.Post(sysUser, RouteAPI.LOGOUT_KEY);
            }
            else
                apiResponse.MESSAGE_TYPECODE = true;
            return apiResponse;
        }

        public static async Task<ResultMessage<bool>> RemoveDataPlug()
        {
            ResultMessage<bool> apiResponse = new ResultMessage<bool>();
            if (BaseFrom.BanQuyenKeyInfo != null)
            {
                KeyRequestInfoViewModel sysUser = new KeyRequestInfoViewModel();
                sysUser.SerialNo = CryptoHelper.Base64Encode(BaseFrom.BanQuyenKeyInfo.SerialNo);
                sysUser.CategoryCode = AppSettings.CategoryCode;
                sysUser.UserName = AppSettings.UserName;
                sysUser.PCName = AppSettings.PCName;
                sysUser.SerialHDD = AppSettings.SerialHDD;
                apiResponse = await UtilAPI<bool>.Post(sysUser, RouteAPI.LOGOUT_PLUG);
            }
            else
                apiResponse.MESSAGE_TYPECODE = true;
            return apiResponse;
        }
#endregion kết nối với hệ thống  mới
    }
}
