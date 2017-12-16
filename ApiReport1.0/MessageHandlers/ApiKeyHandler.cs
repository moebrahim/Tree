using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ApiReport1._0.MessageHandlers
{
    public class ApiKeyHandler: DelegatingHandler
    {
        private const string YourApiKey = "abmosh";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            bool isValidApiKey = false;
            IEnumerable<string> lsHeaders;
            var CheckApiKeyExist = request.Headers.TryGetValues("API_KEY", out lsHeaders);
            if (CheckApiKeyExist)
            {
                if (lsHeaders.FirstOrDefault().Equals(YourApiKey))
                {
                    isValidApiKey = true;
                } 
            }
            if (!isValidApiKey)
            {
                return request.CreateResponse(HttpStatusCode.Forbidden, "Bad Api Key");

            }
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}