using AutoMapper.Internal;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraScheduler.Native;
using log4net;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VChatCore.Model;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model;
using System.Collections.Generic;

namespace PhanMemQuanLyThiCong.Common.API
{
    /// <summary>
    ///
    /// </summary>
    public class BaseAPI
    {
        /// <summary>
        /// Defines the Logging
        /// </summary>
        public static readonly ILog Logging = LogManager.GetLogger("PM360Application");

        ///// <summary>
        ///// Defines the client
        ///// </summary>
        //protected static System.Net.Http.HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAPI"/> class.
        /// </summary>
        protected BaseAPI()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// The InitData
        /// </summary>
        /*private static void InitData()
        {
            //if (client == null || client.DefaultRequestHeaders.Authorization == null)
            //{
            //    try
            //    {
            //        client = new System.Net.Http.HttpClient();

            //        string UriAPI = AppSettings.ApiUrl;

            //        client.BaseAddress = new System.Uri(UriAPI);
            //        // Setting content type.
            //        client.DefaultRequestHeaders.Accept.Clear();
       
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //        // Setting timeout.
            //        client.Timeout = TimeSpan.FromMinutes(Convert.ToDouble(3));
            //    }
            //    catch (Exception ex)
            //    {
            //        Logging.Error(ex.Message, ex);
            //    }
            //}
        }*/

        ///// <summary>
        ///// The InitData
        ///// </summary>
        //private static void InitDataThoiTiet()
        //{
        //    if (HttpClie == null || client.DefaultRequestHeaders.Authorization == null)
        //    {
        //        try
        //        {
        //            client = new System.Net.Http.HttpClient();

        //            client.BaseAddress = new System.Uri(AppSettings.ApiUrlThoiTiet);
        //            // Setting content type.
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            // Setting timeout.
        //            client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
        //        }
        //        catch (Exception ex)
        //        {
        //            Logging.Error(ex.Message, ex);
        //        }
        //    }
        //}

        ///// <summary>
        ///// The InitCheckAPIOld
        ///// </summary>
        //private static void InitCheckAPIOld()
        //{
        //    if (client == null || client.DefaultRequestHeaders.Authorization == null)
        //    {
        //        try
        //        {
        //            client = new System.Net.Http.HttpClient();
        //            client.BaseAddress = new System.Uri(AppSettings.ApiUrlKeyOld);

        //            // Setting content type.
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            // Setting timeout.
        //            client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
        //        }
        //        catch (Exception ex)
        //        {
        //            Logging.Error(ex.Message, ex);
        //        }
        //    }
        //}

        /// <summary>
        /// The GetLocalIPAddress
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// The PostRequest
        /// </summary>
        /// <param name="requestUri">The requestUri<see cref="string"/></param>
        /// <param name="requestObj">The requestObj<see cref="object"/></param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/></returns>
        protected static async Task<HttpResponseMessage> PostRequest(string requestUri, object requestObj)
        {
            // Initialization.
            //InitData();
            HttpResponseMessage response = new HttpResponseMessage();
            response = await CusHttpClient.InstanceTBT.PostAsJsonAsync(requestUri, requestObj);
            Logging.Info(response);
            return response;
        }

        /// <summary>
        /// The PostSyncRequest
        /// </summary>
        /// <param name="requestUri">The requestUri<see cref="string"/></param>
        /// <param name="requestObj">The requestObj<see cref="object"/></param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/></returns>
        protected static HttpResponseMessage PostSyncRequest(string requestUri, object requestObj)
        {
            // Initialization.
            //InitData();
            HttpResponseMessage response = new HttpResponseMessage();
            response = CusHttpClient.InstanceTBT.PostAsJsonAsync(requestUri, requestObj).Result;
            Logging.Info(response);
            return response;
        }

        /// <summary>
        /// The PostSyncRequest
        /// </summary>
        /// <param name="requestUri">The requestUri<see cref="string"/></param>
        /// <param name="requestObj">The requestObj<see cref="object"/></param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/></returns>
        protected static async Task<HttpResponseMessage> PostSyncRequestOld(string requestUri, object requestObj)
        {
            // Initialization.
            //InitCheckAPIOld();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await CusHttpClient.InstanceTBT.PostAsJsonAsync(requestUri, requestObj);
                Logging.Info(response);
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        /// <summary>
        /// The DeleteRequest
        /// </summary>
        /// <param name="requestUri">The requestUri<see cref="string"/></param>
        /// <param name="requestObj">The requestObj<see cref="object"/></param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/></returns>
        protected static async Task<HttpResponseMessage> DeleteRequest(string requestUri, object requestObj)
        {
            // Initialization.
            //InitData();
            HttpResponseMessage response = new HttpResponseMessage();
            response = await CusHttpClient.InstanceTBT.PostAsJsonAsync(requestUri, requestObj).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// The GetRequest
        /// </summary>
        /// <param name="requestUri">The requestUri<see cref="string"/></param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/></returns>
        protected static async Task<HttpResponseMessage> GetRequest(string requestUri)
        {
            // Initialization.
            //InitData();
            HttpResponseMessage response = new HttpResponseMessage();
            response = await CusHttpClient.InstanceTBT.GetAsync(requestUri).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// The GetRequestNotAsync
        /// </summary>
        /// <param name="requestUri">The requestUri<see cref="string"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        protected static HttpResponseMessage GetRequestNotAsync(string requestUri)
        {
            // Initialization.
            //InitData();
            HttpResponseMessage response = new HttpResponseMessage();
            response = CusHttpClient.InstanceTBT.GetAsync(requestUri).Result;
            return response;
        }

        /// <summary>
        /// The PostRequest
        /// </summary>
        /// <param name="requestUri">The requestUri<see cref="string"/></param>
        /// <param name="requestObj">The requestObj<see cref="object"/></param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/></returns>
        protected static async Task<HttpResponseMessage> PutRequest(string requestUri, object requestObj)
        {
            // Initialization.
            //InitData();
            HttpResponseMessage response = new HttpResponseMessage();
            response = await CusHttpClient.InstanceTBT.PutAsJsonAsync(requestUri, requestObj);
            Logging.Info(response);
            return response;
        }

        /// <summary>
        /// The GetRequestThoiTiet
        /// </summary>
        /// <param name="requestUri">The requestUri<see cref="string"/></param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/></returns>
        protected static async Task<HttpResponseMessage> GetRequestThoiTiet(string requestUri)
        {

            //InitDataThoiTiet();
            HttpResponseMessage response = new HttpResponseMessage();
            response = await CusHttpClient.InstanceThoiTiet.GetAsync(requestUri).ConfigureAwait(false);
            return response;
        }

    }

    /// <summary>
    /// Defines the <see cref="UtilAPI{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UtilAPI<T> : BaseAPI
    {
        /// <summary>
        /// The Login
        /// </summary>
        /// <param name="requestObj">The requestObj<see cref="{T}"/></param>
        /// <param name="api">The api<see cref="string"/></param>
        /// <returns>The <see cref="Task{ResultMessage{T}}"/></returns>
        public static async Task<ResultMessage<T>> Login(T requestObj, string api)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();
            try
            {
                HttpResponseMessage response = await PostRequest(api, requestObj);
                // Verification
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result);
                    // Releasing.
                    response.Dispose();
                }

                Logging.Info(responseObj);
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="requestObj">The requestObj<see cref="{T}"/></param>
        /// <param name="api">The api<see cref="string"/></param>
        /// <returns>The <see cref="Task{T[]}"/></returns>
        public static async Task<T[]> Get(T requestObj, string api)
        {
            T[] responseObj = new T[1];
            try
            {
                HttpResponseMessage response = await PostRequest(api, requestObj);
                // Verification
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<T[]>(result);
                    // Releasing.
                    response.Dispose();
                }
                Logging.Info(responseObj);
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="api">The api<see cref="string"/></param>
        /// <returns>The <see cref="Task{ResultMessage{T}}"/></returns>
        public static async Task<ResultMessage<T>> Get(string api)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();

            try
            {
                HttpResponseMessage response = await GetRequest(api);
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result);
                    // Releasing.
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }

        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="requestObj">The requestObj<see cref="{T1}"/></param>
        /// <param name="api">The api<see cref="string"/></param>
        /// <returns>The <see cref="Task{ResultMessage{T}}"/></returns>
        public static async Task<ResultMessage<T>> Post<T1>(T1 requestObj, string api)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();
            try
            {
                //Logging.InfoFormat("Param {0}", JsonConvert.SerializeObject(requestObj));
                HttpResponseMessage response = await PostRequest(api, requestObj);
                // Verification
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result);
                    // Releasing.
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }

        /// <summary>
        /// The Put
        /// </summary>
        /// <param name="requestObj">The requestObj<see cref="{T1}"/></param>
        /// <param name="api">The api<see cref="string"/></param>
        /// <returns>The <see cref="Task{ResultMessage{T}}"/></returns>
        public static async Task<ResultMessage<T>> Put<T1>(T1 requestObj, string api)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();
            try
            {
                HttpResponseMessage response = await PutRequest(api, requestObj);
                // Verification
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result);
                    // Releasing.
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }

        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="requestObj">The requestObj<see cref="{T}"/></param>
        /// <param name="api">The api<see cref="string"/></param>
        /// <returns>The <see cref="Task{ResultMessage{T}}"/></returns>
        public static async Task<ResultMessage<T>> Post(T requestObj, string api)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();
            try
            {
                HttpResponseMessage response = await PostRequest(api, requestObj);
                // Verification
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result);
                    // Releasing.
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="requestObj">The requestObj<see cref="{T}"/></param>
        /// <param name="api">The api<see cref="string"/></param>
        /// <returns>The <see cref="Task{ResultMessage{T}}"/></returns>
        public static async Task<ResultMessage<T>> Delete(T requestObj, string api)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();
            try
            {
                HttpResponseMessage response = await PostRequest(api, requestObj);
                // Verification
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result);
                    // Releasing.
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="requestObj">The requestObj<see cref="{T1}"/></param>
        /// <param name="api">The api<see cref="string"/></param>
        /// <returns>The <see cref="Task{ResultMessage{T}}"/></returns>
        public static async Task<ResultMessage<T>> Delete<T1>(T1 requestObj, string api)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();
            try
            {
                HttpResponseMessage response = await PostRequest(api, requestObj);
                // Verification
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result);
                    // Releasing.
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }

        /// <summary>
        /// The Put
        /// </summary>
        /// <param name="requestObj">The requestObj<see cref="{T}"/></param>
        /// <param name="api">The api<see cref="string"/></param>
        /// <returns>The <see cref="Task{ResultMessage{T}}"/></returns>
        public static async Task<ResultMessage<T>> Put(T requestObj, string api)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();
            try
            {
                HttpResponseMessage response = await PutRequest(api, requestObj);
                // Verification
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result);
                    // Releasing.
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                responseObj.MESSAGE_CONTENT = ex.Message;
                responseObj.MESSAGE_TYPECODE = false;
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }


        public static async Task<List<T>> GetThoiTiet(string api)
        {
            var responseObj = new List<T>();
            try
            {
                HttpResponseMessage response = await GetRequestThoiTiet(api);
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<List<T>>(result);
                    // Releasing.
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
            }
            return responseObj;
        }

        public static async Task<List<T>> CheckKeyOld(string api)
        {
            var responseObj = new List<T>();
            //try
            //{
            //    HttpResponseMessage response = await GetRequestCheckKeyOld(api);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        // Reading Response.
            //        string result = response.Content.ReadAsStringAsync().Result;
            //        responseObj = JsonConvert.DeserializeObject<List<T>>(result);
            //        // Releasing.
            //        response.Dispose();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logging.Error(ex.Message, ex);
            //}
            return responseObj;
        }

        //public static bool RegisterKey(string api, RegisterOldViewModel model)
        //{
        //    bool result = false;
        //    try
        //    {
        //        HttpResponseMessage response = PostSyncRequestOld(api, model);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Error(ex.Message, ex);
        //        return false;
        //    }
        //    return result;
        //}
        //public static bool RegisterKey(string api, RegisterOldViewModel model)
        //{
        //    bool result = false;
        //    try
        //    {
        //        api = AppSettings.UrlChat + api;
        //        OrderAddVm orderAddVm = new OrderAddVm()
        //        {
        //            CustumerAddress = model.address,
        //            CustumerEmail = model.email,
        //            CustumerName = model.name,
        //            CustumerPhoneNumber = model.phone,
        //            CustumerProvince = model.province_id.ToString(),
        //            Department = model.department_id.ToString(),
        //            ProductCategoryId = 1,
        //            PcName= AppSettings.PCName,
        //        };

        //        HttpResponseMessage response = PostSyncRequestOld(api, orderAddVm);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Error(ex.Message, ex);
        //        return false;
        //    }
        //    return result;
        //}
        public static async Task<ResultMessage<AppUserViewModel>> RegisterKey(string api, RegisterOldViewModel model)
        {
            ResultMessage<AppUserViewModel> responseObj = new ResultMessage<AppUserViewModel>();
            try
            {
                //api = AppSettings.UrlChat + api;
                OrderAddVm orderAddVm = new OrderAddVm()
                {
                    CustumerAddress = model.address,
                    CustumerEmail = model.email,
                    CustumerName = model.name,
                    CustumerPhoneNumber = model.phone,
                    CustumerProvince = model.province_id.ToString(),
                    Department = model.department_id.ToString(),
                    ProductCategoryId = int.Parse(AppSettings.CategoryCode),
                    PcName = AppSettings.PCName,
                    Password = model.password,
                };

                HttpResponseMessage response = await PostSyncRequestOld(api, orderAddVm);
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<ResultMessage<AppUserViewModel>>(result);
                    // Releasing.
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
                return new ResultMessage<AppUserViewModel>();
            }
            return responseObj;
        }
        public async static Task<ResultMessage<bool>> ActivatedCode(string serino)
        {
            ResultMessage<bool> resultMessage = new ResultMessage<bool>();
            try
            {
                KeyRequestInfoViewModel sysUser = new KeyRequestInfoViewModel();
                sysUser.SerialNo = serino;
                sysUser.CategoryCode = AppSettings.CategoryCode;
                sysUser.UserName = AppSettings.UserName;
                sysUser.PCName = AppSettings.PCName;
                sysUser.SerialHDD = AppSettings.SerialHDD;
                return await UtilAPI<bool>.Post(sysUser, RouteAPI.ACTIVE_KEY);
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
                return resultMessage;
            }
        }
    }
}
