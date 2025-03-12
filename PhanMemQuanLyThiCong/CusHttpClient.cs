//using DevExpress.LookAndFeel.Design;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.MLogging;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Model;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PhanMemQuanLyThiCong.Form_CaiDatDotHopDong;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong
{
    class CusHttpClient
    {
        //public HttpClient client = new HttpClient(new UserApiAuthenticationHandler(new HttpClientHandler()));//, new Handler2(), new Handler3());
        //static MemoryCache userCach = new MemoryCache("UserCaching");
        private string _url = "";
        private int? _timeout = null;
        public string BaseAddress
        {
            get { return _url;  }
            set { _url = value; }
        }

        private static CusHttpClient _instanceTBT;
        public static CusHttpClient InstanceTBT
        {
            get { if (_instanceTBT == null) _instanceTBT = new CusHttpClient(AppSettings.UrlAPI); return _instanceTBT; }
            private set { CusHttpClient._instanceCustomer = value; }
        }

        private static CusHttpClient _instanceCustomer;
        public static CusHttpClient InstanceCustomer
        {
            get { if (_instanceCustomer == null)
                {
                    _instanceCustomer = new CusHttpClient("");
                    _instanceCustomer._timeout = 15; //In Minutes
                }
                        
                        return CusHttpClient._instanceCustomer; }
            private set { CusHttpClient._instanceCustomer = value; }
        }
        
        private static CusHttpClient _instanceThoiTiet;
        public static CusHttpClient InstanceThoiTiet
        {
            get { if (_instanceThoiTiet == null) _instanceThoiTiet = new CusHttpClient(""); return CusHttpClient._instanceThoiTiet; }
            private set { CusHttpClient._instanceThoiTiet = value; }
        }

        public CusHttpClient(string API)
        {
            _url = API;
        }

        //public void ChangeUrl(string newUrl)
        //{
        //    _url = newUrl;
        //}

        public async Task<ResultMessage<T>> MGetAsync<T>(string api)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();

            using (HttpClient client = new HttpClient(new UserApiAuthenticationHandler(new HttpClientHandler())))
            {

                try
                {
                    client.BaseAddress = new System.Uri(_url);
                    if (_timeout.HasValue)
                        client.Timeout = TimeSpan.FromMinutes(_timeout.Value);
                    //Logging.InfoFormat("Param {0}", JsonConvert.SerializeObject(requestObj));
                    Logging.Info($"GET {api}");

                    using (HttpResponseMessage response = await client.GetAsync(api))
                    {
                        using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                        {

                            byte[] chunk = new byte[200];
                            responseStream.Read(chunk, 0, 200);
                            string result = BitConverter.ToString(chunk);
                            responseStream.Position = 0;

                            if (!response.IsSuccessStatusCode)
                            {
                                Logging.Info(result);

                                //AlertShower.ShowInfo(result);
                            }

                            // Verification
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                responseObj.STATUS_CODE = (int)HttpStatusCode.Unauthorized;
                                responseObj.MESSAGE_TYPECODE = false;
                                responseObj.MESSAGE_CONTENT = "Lỗi xác thực";

                                TongHopHelper.AppUnauthorize();

                            }
                            else if (response.IsSuccessStatusCode)
                            {
                                // Reading Response.
                                //responseObj.MESSAGE_TYPECODE = true;

                                using (StreamReader reader = new StreamReader(responseStream))
                                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                                {
                                    JsonSerializer serializer = new JsonSerializer();
                                    responseObj = serializer.Deserialize<ResultMessage<T>>(jsonReader);
                                }

                                Logging.Info(result);
                            }
                            else
                            {
                                try
                                {
                                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result) ?? responseObj;
                                }
                                catch
                                {
                                    responseObj.MESSAGE_TYPECODE = false;

                                }

                                if (!responseObj.MESSAGE_CONTENT.HasValue())
                                {
                                    responseObj.MESSAGE_CONTENT = "Lỗi kết nối đến server.";
                                }

                                Logging.Error(result);
                            }
                            // Releasing.
                        }
                    }
                }
                catch (Exception ex)
                {
                    string err = $"Exception: {ex.Message}\r\nInnerException: {ex.InnerException?.Message}";
                    Logging.Error(err);
                    AlertShower.ShowInfo(err);
                    responseObj.MESSAGE_CONTENT = err;
                }
            }
            //if (responseObj.STATUS_CODE == (int)HttpStatusCode.Unauthorized)
            //    MessageShower.ShowInformation("Hết phiên đăng nhập. Vui lòng đăng nhập lại");

            return responseObj;
        }

        public async Task<ResultMessage<T>> MPostAsJsonAsync<T>(string api, object val)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();
            using (HttpClient client = new HttpClient(new UserApiAuthenticationHandler(new HttpClientHandler())))
            {

                try
                {
                    client.BaseAddress = new System.Uri(_url);
                    if (_timeout.HasValue)
                        client.Timeout = TimeSpan.FromMinutes(_timeout.Value);
                    //Logging.InfoFormat("Param {0}", JsonConvert.SerializeObject(requestObj));
                    Logging.Info($"POST TO {api}");
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new System.Uri(Path.Combine(client.BaseAddress.AbsoluteUri, api)),
                        Content = new StringContent(JsonConvert.SerializeObject(val), Encoding.UTF8, "application/json"),
                    };
                    using (HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                        {

                            //byte[] chunk = new byte[200];
                            //responseStream.Read(chunk, 0, 200);
                            //string result = BitConverter.ToString(chunk);
                            //responseStream.Position = 0;
                            //if (!response.IsSuccessStatusCode)
                            //{
                            //    Logging.Info(result);
                            //}

                            // Verification
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                responseObj.STATUS_CODE = (int)HttpStatusCode.Unauthorized;
                                responseObj.MESSAGE_TYPECODE = false;
                                responseObj.MESSAGE_CONTENT = "Lỗi xác thực";

                                TongHopHelper.AppUnauthorize();

                            }
                            else if (response.IsSuccessStatusCode)
                            {
                                // Reading Response.
                                //responseObj.MESSAGE_TYPECODE = true;

                                using (StreamReader reader = new StreamReader(responseStream))
                                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                                {
                                    JsonSerializer serializer = new JsonSerializer();
                                    responseObj = serializer.Deserialize<ResultMessage<T>>(jsonReader);
                                }

                                //Logging.Info(result);
                            }
                            else
                            {
                                try
                                {
                                    responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>("result") ?? responseObj;
                                }
                                catch
                                {
                                    responseObj.MESSAGE_TYPECODE = false;

                                }

                                if (!responseObj.MESSAGE_CONTENT.HasValue())
                                {
                                    responseObj.MESSAGE_CONTENT = "Lỗi kết nối đến server.";
                                }

                                Logging.Error("result");
                            }
                            // Releasing.
                        }
                    }
                }
                catch (Exception ex)
                {
                    string err = $"Exception: {ex.Message}\r\nInnerException: {ex.InnerException?.Message}";
                    Logging.Error(err);
                    responseObj.MESSAGE_CONTENT = err;
                }
            }
            //if (responseObj.STATUS_CODE == (int)HttpStatusCode.Unauthorized)
            //    MessageShower.ShowInformation("Hết phiên đăng nhập. Vui lòng đăng nhập lại");

            return responseObj;
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync(string requestUri, object requestObj)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            using (HttpClient client = new HttpClient(new UserApiAuthenticationHandler(new HttpClientHandler())))
            {
                try
                {
                    client.BaseAddress = new System.Uri(_url);
                    if (_timeout.HasValue)
                        client.Timeout = TimeSpan.FromMinutes(_timeout.Value);
                    res = await client.PostAsJsonAsync(requestUri, requestObj);
                }
                catch
                {
                    
                }
            }
            return res;
        }
        public async Task<HttpResponseMessage> PutAsJsonAsync(string requestUri, object requestObj)
        {
            HttpResponseMessage res = new HttpResponseMessage();

            using (HttpClient client = new HttpClient(new UserApiAuthenticationHandler(new HttpClientHandler())))
            {
                try
                {
                    client.BaseAddress = new System.Uri(_url);
                    if (_timeout.HasValue)
                        client.Timeout = TimeSpan.FromMinutes(_timeout.Value);
                    res = await client.PutAsJsonAsync(requestUri, requestObj);
                }
                catch
                {

                }

            }
            return res;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            HttpResponseMessage res = new HttpResponseMessage();

            using (HttpClient client = new HttpClient(new UserApiAuthenticationHandler(new HttpClientHandler())))
            {
                try
                {
                    client.BaseAddress = new System.Uri(_url);
                    if (_timeout.HasValue)
                        client.Timeout = TimeSpan.FromMinutes(_timeout.Value);
                    res = await client.GetAsync(requestUri);
                }
                catch
                {

                }

            }
            return res;
        }
        public async Task<ResultMessage<T>> PostAsync<T>(string api, HttpContent val)
        {
            ResultMessage<T> responseObj = new ResultMessage<T>();
            using (HttpClient client = new HttpClient(new UserApiAuthenticationHandler(new HttpClientHandler())))
            {

                try
                {
                    client.BaseAddress = new System.Uri(_url);
                    if (_timeout.HasValue)
                        client.Timeout = TimeSpan.FromMinutes(_timeout.Value);

                    //Logging.InfoFormat("Param {0}", JsonConvert.SerializeObject(requestObj));
                    Logging.Info($"POST TO {api}");
                    HttpResponseMessage response = await client.PostAsync(api, val);
                    string result = response.Content.ReadAsStringAsync().Result;

                    // Verification
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        responseObj.STATUS_CODE = (int)HttpStatusCode.Unauthorized;
                        responseObj.MESSAGE_TYPECODE = false;
                        responseObj.MESSAGE_CONTENT = "Lỗi xác thực";

                        TongHopHelper.AppUnauthorize();
                    }
                    else if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        //responseObj.MESSAGE_TYPECODE = true;

                        responseObj = JsonConvert.DeserializeObject<ResultMessage<T>>(result) ?? responseObj;
                        Logging.Info(result);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(responseObj.MESSAGE_CONTENT))
                        {
                            MessageShower.ShowError(responseObj.MESSAGE_CONTENT);
                        }
                        responseObj.MESSAGE_TYPECODE = false;
                        Logging.Error(result);
                    }
                    // Releasing.
                    response.Dispose();
                }
                catch (Exception ex)
                {
                    Logging.Error(ex.Message, ex.Message);
                }
            }
            //if (responseObj.STATUS_CODE == (int)HttpStatusCode.Unauthorized)
            //    MessageShower.ShowInformation("Hết phiên đăng nhập. Vui lòng đăng nhập lại");

            return responseObj;
        }
    }
}
