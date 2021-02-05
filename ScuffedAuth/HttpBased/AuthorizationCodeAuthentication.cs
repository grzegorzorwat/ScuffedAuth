using Authorization.AuthorizationEndpoint;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http;
using System;

namespace ScuffedAuth.HttpBased
{
    public class AuthorizationCodeAuthentication : IAuthorizationCodeAuthentication
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationCodeAuthentication(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Response Authenticate()
        {
            if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated != true)
            {
                var request = _httpContextAccessor.HttpContext?.Request;

                if(request != null)
                {
                    return RedirectResponseFactory.With("/Identity/Account/Login",
                        "ReturnUrl",
                        $"{request.Path}{request.QueryString}");
                }

                throw new Exception("Could not authenticate based on HttpContext");
            }

            return null!;
        }
    }
}
