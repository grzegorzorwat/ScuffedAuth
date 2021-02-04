using System;
using System.Threading.Tasks;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationCodeGenerator _authorizationCodeGenerator;
        private readonly IAuthorizationCodesRepository _authorizationCodesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorizationCodeAuthentication _authentication;

        public AuthorizationService(IAuthorizationCodeGenerator authorizationCodeGenerator,
            IAuthorizationCodesRepository authorizationCodesRepository,
            IUnitOfWork unitOfWork,
            IAuthorizationCodeAuthentication authentication)
        {
            _authorizationCodeGenerator = authorizationCodeGenerator;
            _authorizationCodesRepository = authorizationCodesRepository;
            _unitOfWork = unitOfWork;
            _authentication = authentication;
        }

        public async Task<AuthorizationResponse> Authorize(AuthorizationServiceRequest request)
        {
            try
            {
                if (request.ResponseType != ResponseType.code)
                {
                    return AuthorizationResponse.WithError(request.Referer, "unsupported_response_type");
                }

                var client = await _authorizationCodesRepository.GetClientByIdAsync(request.ClientId);

                if (client is null)
                {
                    return AuthorizationResponse.WithError(request.Referer, "unauthorized_client");
                }

                string redirectionUri = request.RedirectUri
                    ?? client.RedirectionUri
                    ?? string.Empty;

                if (IsInvalidRedirectUri(redirectionUri))
                {
                    return AuthorizationResponse.WithError(request.Referer, "invalid_request");
                }

                var authenticationResponse = _authentication.Authenticate();

                if (authenticationResponse is not null)
                {
                    return authenticationResponse;
                }

                var authorizationCode = _authorizationCodeGenerator.Generate(client.Id, redirectionUri);
                await _authorizationCodesRepository.AddAuthorizationCode(authorizationCode);
                await _unitOfWork.Complete();
                return AuthorizationResponse.WithCode(redirectionUri, authorizationCode.Code);
            }
            catch
            {
                return AuthorizationResponse.WithError(request.Referer, "server_error");
            }
        }

        private static bool IsInvalidRedirectUri(string redirectionUri)
        {
            return string.IsNullOrEmpty(redirectionUri)
                || !(Uri.TryCreate(redirectionUri, UriKind.Absolute, out var result)
                    && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps));
        }
    }
}
