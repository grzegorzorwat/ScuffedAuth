using BaseLibrary.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace ScuffedAuth.Middlewares.Authentication
{
    public class ResponseAuthenticateResultVisitor : IResponseVisitor<AuthenticateResult>
    {
        private readonly IServiceProvider _services;

        public ResponseAuthenticateResultVisitor(IServiceProvider services)
        {
            _services = services;
        }

        public AuthenticateResult VisitErrorResponse(ErrorResponse response)
        {
            return AuthenticateResult.Fail(response.Message);
        }

        public AuthenticateResult VisitErrorResponse<PayloadType>(ErrorResponse<PayloadType> response)
        {
            return AuthenticateResult.Fail(response.Payload?.ToString() ?? "Failed to authenticate.");
        }

        public AuthenticateResult VisitRedirectResponse(RedirectResponse response)
        {
            return AuthenticateResult.NoResult();
        }

        public AuthenticateResult VisitSuccessResponse(SuccessResponse response)
        {
            return SuccessResult(Array.Empty<Claim>());
        }

        public AuthenticateResult VisitSuccessResponse<PayloadType>(SuccessResponse<PayloadType> response)
        {
            var claimsMapper = _services.GetService<IClaimsMapper<PayloadType>>();
            var claims = claimsMapper?.Map(response.Payload) ?? Array.Empty<Claim>();
            return SuccessResult(claims);
        }

        private AuthenticateResult SuccessResult(Claim[] claims)
        {
            var claimsIdentity = new ClaimsIdentity(claims, nameof(GrantTypesAuthenticationHandler));
            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity),
                AuthenticationSchemeConstants.GrantTypesAuthenticationScheme));
        }
    }
}
