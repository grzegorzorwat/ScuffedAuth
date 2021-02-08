using Authentication;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using OAuth.Model;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ScuffedAuth.Middlewares.Authentication
{
    public class GrantTypesAuthenticationHandler : AuthenticationHandler<GrantTypesAuthenticationSchemeOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationFactory _authenticationFactory;
        private readonly IResponseVisitor<AuthenticateResult> _responseAuthenticateResultVisitor;

        public GrantTypesAuthenticationHandler(IOptionsMonitor<GrantTypesAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor,
            AuthenticationFactory authorizationFactory,
            IResponseVisitor<AuthenticateResult> responseAuthenticateResultVisitor) : base(options, logger, encoder, clock)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationFactory = authorizationFactory;
            _responseAuthenticateResultVisitor = responseAuthenticateResultVisitor;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext is not null)
                {
                    var grantType = GetGrantType();
                    var authorization = _authenticationFactory.GetAuthentication(grantType);
                    var response = await authorization.Authenticate(httpContext.Request.Headers[HeaderNames.Authorization]);
                    return response.Accept(_responseAuthenticateResultVisitor);
                }

                return AuthenticateResult.Fail("Failed to authenticate.");
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }
        }

        private GrantTypes GetGrantType()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext is not null
                && httpContext.Request.Query.ContainsKey("grant_type"))
            {
                if (Enum.TryParse(httpContext.Request.Query["grant_type"], out GrantTypes grantType))
                {
                    return grantType;
                }
            }

            return GrantTypes.unidentified;
        }
    }
}
