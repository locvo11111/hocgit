using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong.Common
{
    public class UserApiAuthenticationHandler : DelegatingHandler
    {
        DelegatingHandler _innerHandler;
        public UserApiAuthenticationHandler(HttpClientHandler handler)
        {
            InnerHandler = handler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
          HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", MSETTING.Default.TokenTBT);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
