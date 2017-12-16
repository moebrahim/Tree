using ApiReport1._0.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ApiReport1._0.MessageHandlers
{
    public class AuthHandler : DelegatingHandler
    {
        string _username = "";

        private bool ValidateCredentials(AuthenticationHeaderValue authenicAuthenticationHeaderValue)
        {
            try
            {
                if (authenicAuthenticationHeaderValue != null &&
                    !string.IsNullOrEmpty(authenicAuthenticationHeaderValue.Parameter))
                {
                    string[] decodedCredentials = Encoding.ASCII.GetString(Convert.
                        FromBase64String(authenicAuthenticationHeaderValue.Parameter)).Split(new[] { ':' });
                    //now decodedCredentials[0] will contain
                    //username and decodedCredentials[1] will
                    //contain password.

                    if (decodedCredentials[0].Equals("asdf")
                        && decodedCredentials[1].Equals("lkas"))
                    {
                        _username = "John Doe";
                        return true; //request authenticated.
                    }
                }
                return false; //request not authenticated.
            }
            catch
            {
                return false;
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            //if the credentials are validated,
            //set CurrentPrincipal and Current.User
            if (ValidateCredentials(request.Headers.Authorization))
            {
                Thread.CurrentPrincipal = new TestAPIPrincipal(_username);
                HttpContext.Current.User = new TestAPIPrincipal(_username);
            }
            //Execute base.SendAsync to execute default
            //actions and once it is completed,
            //capture the response object and add
            //WWW-Authenticate header if the request
            //was marked as unauthorized.

            //Allow the request to process further down the pipeline
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized
                && !response.Headers.Contains("WwwAuthenticate"))
            {
                response.Headers.Add("WwwAuthenticate", "awe");
            }

            return response;
        }
    }
}