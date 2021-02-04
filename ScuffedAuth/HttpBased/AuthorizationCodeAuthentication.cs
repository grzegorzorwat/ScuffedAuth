using Authorization.AuthorizationEndpoint;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ScuffedAuth.HttpBased
{
    public class AuthorizationCodeAuthentication : IAuthorizationCodeAuthentication
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationCodeAuthentication(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthorizationResponse Authenticate()
        {
            if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated != true)
            {
                var request = _httpContextAccessor.HttpContext?.Request;

                if(request != null)
                {
                    string returnUrl = WebUtility.UrlEncode(request.Path + request.QueryString);
                    return AuthorizationResponse.WithKeyValue("/Identity/Account/Login", "ReturnUrl", returnUrl);
                }

                throw new Exception("Could not authenticate based on HttpContext");
            }

            return null!;
        }
    }
}
