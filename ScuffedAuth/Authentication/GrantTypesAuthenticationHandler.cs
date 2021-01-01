using Authentication;
using Authorization;
using Authorization.TokenEndpoint;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ScuffedAuth.Authentication
{
    public class GrantTypesAuthenticationHandler : AuthenticationHandler<GrantTypesAuthenticationSchemeOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationFactory _authenticationFactory;

        public GrantTypesAuthenticationHandler(IOptionsMonitor<GrantTypesAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor,
            AuthenticationFactory authorizationFactory) : base(options, logger, encoder, clock)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationFactory = authorizationFactory;
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

                    if (httpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var header))
                    {
                        var response = await authorization.Authenticate(header);

                        if (response.Success)
                        {
                            var claims = new[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, response.Client.Id)
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, nameof(GrantTypesAuthenticationHandler));
                            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name));
                        }

                        return AuthenticateResult.Fail(response.Message);
                    }
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
                && httpContext.Request.Query.ContainsKey(nameof(TokenRequest.GrantType)))
            {
                if (Enum.TryParse(httpContext.Request.Query[nameof(TokenRequest.GrantType)], out GrantTypes grantType))
                {
                    return grantType;
                }
            }

            return GrantTypes.unidentified;
        }
    }
}
